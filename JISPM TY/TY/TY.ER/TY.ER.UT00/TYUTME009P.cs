using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 보험영수증 상황보고서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.28 14:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_79SET702 : 보험영수증 상황보고서 보험료 일자 조회
    ///  TY_P_UT_79SEU703 : 보험영수증 상황보고서 일자별 건수 조회
    ///  TY_P_UT_79SEU704 : 보험영수증 상황보고서 기간별 총건수 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTME009P : TYBase
    {
        #region Description : 폼 로드
        public TYUTME009P()
        {
            InitializeComponent();
        }

        private void TYUTME009P_Load(object sender, System.EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            sEDDATE = DateTime.Now.ToString("yyyy-MM-dd");

            this.DTP01_STDATE.SetValue(sSTDATE.Substring(0, 8) + "26");
            this.DTP01_EDDATE.SetValue(sEDDATE.Substring(0, 8) + "25");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_7BSGK130.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_7BSGJ129", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7BSGK130.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_UT_7BSGK130.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_7BSGK130.GetValue(i, "ISCHULIL").ToString() == "계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_7BSGK130.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_UT_7BSGK130.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();

                if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
                {
                    #region Description : 상황보고서 출력

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_79SET702", this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString()
                                                                );

                    dt = QueryDataSetReport(this.DbConnector.ExecuteDataTable());

                    if (dt.Rows.Count > 0)
                    {
                        SectionReport rpt = new TYUTME009R();
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;
                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_2422N250");
                    }

                    #endregion
                }
                else
                {
                    #region Description : 월별 보험 영수증 출력

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_797D9553", Get_Date(this.DTP01_EDDATE.GetString()).ToString().Substring(4, 2),
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        SectionReport rpt = new TYUTME011R();

                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_2422N250");
                    }

                    #endregion
                }

            }
        }
        #endregion

        #region Descriptoin : 데이터테이블 변경
        private DataTable QueryDataSetReport(DataTable dt)
        {
            string sSql = string.Empty;
            string sISCHULIL = string.Empty;
            double dCOUNT = 0;
            double dISGAMAMT = 0;
            double dISISAMT = 0;
            double dHAPCOUNT = 0;
            double dHAPISGAMAMT = 0;
            double dHAPISISAMT = 0;

            DataTable retDt = new DataTable();

            retDt.Columns.Add("YY", typeof(System.String));
            retDt.Columns.Add("MM", typeof(System.String));
            retDt.Columns.Add("DD", typeof(System.String));
            retDt.Columns.Add("COUNT", typeof(System.String));
            retDt.Columns.Add("ISGAMAMT", typeof(System.String));
            retDt.Columns.Add("ISISAMT", typeof(System.String));
            retDt.Columns.Add("COUNT1", typeof(System.String));
            retDt.Columns.Add("ISGAMAMT1", typeof(System.String));
            retDt.Columns.Add("ISISAMT1", typeof(System.String));

            // 출고일자별 영수증 출력
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sISCHULIL = "";
                dCOUNT = 0;
                dISGAMAMT = 0;
                dISISAMT = 0;
                dHAPCOUNT = 0;
                dHAPISGAMAMT = 0;
                dHAPISISAMT = 0;

                // 출고일자
                sISCHULIL = dt.Rows[i]["ISCHULIL"].ToString();

                // 출고일자별 건수 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_79SEU703", sISCHULIL);

                DataTable dtTmp = this.DbConnector.ExecuteDataTable();

                if (dtTmp.Rows.Count > 0)
                {
                    dCOUNT = double.Parse(Get_Numeric(dtTmp.Rows[0]["COUNT"].ToString()));
                    dISGAMAMT = double.Parse(Get_Numeric(dtTmp.Rows[0]["ISGAMAMT"].ToString()));
                    dISISAMT = double.Parse(Get_Numeric(dtTmp.Rows[0]["ISISAMT"].ToString()));
                }

                // 기간별 총건수 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_79SEU704", this.DTP01_STDATE.GetString(),
                                                            sISCHULIL);

                dtTmp = this.DbConnector.ExecuteDataTable();

                if (dtTmp.Rows.Count > 0)
                {
                    dHAPCOUNT = double.Parse(Get_Numeric(dtTmp.Rows[0]["COUNT1"].ToString()));
                    dHAPISGAMAMT = double.Parse(Get_Numeric(dtTmp.Rows[0]["ISGAMAMT1"].ToString()));
                    dHAPISISAMT = double.Parse(Get_Numeric(dtTmp.Rows[0]["ISISAMT1"].ToString()));
                }

                DataRow row = retDt.NewRow();
                // 년
                row["YY"] = sISCHULIL.Substring(0, 4).ToString();
                // 월
                row["MM"] = sISCHULIL.Substring(4, 2).ToString();
                // 일
                row["DD"] = sISCHULIL.Substring(6, 2).ToString();
                // 일자별건수       
                row["COUNT"] = dCOUNT;
                // 일자별 보험금액
                row["ISGAMAMT"] = dISGAMAMT;
                // 일자별 보험료
                row["ISISAMT"] = dISISAMT;
                // 일자별건수
                row["COUNT1"] = dHAPCOUNT;
                // 일자별 보험금액
                row["ISGAMAMT1"] = dHAPISGAMAMT;
                // 일자별 보험료
                row["ISISAMT1"] = dHAPISISAMT;

                retDt.Rows.Add(row);
            }
            return retDt;
        }
        #endregion
    }
}
