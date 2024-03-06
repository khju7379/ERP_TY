using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using System.Windows.Forms;
using System.Drawing;

namespace TY.ER.UT00
{
    /// <summary>
    /// TANKLORRY 할증내역 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.18 19:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_78IH0454 : TANKLORRY 출고할증 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_78IH6455 : TANKLORRY 출고할증 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHDOCHK : 도착지
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR023S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR023S()
        {
            InitializeComponent();
        }

        private void TYUTPR023S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.CBH01_CHHWAJU.CodeText);

            UP_Spread_Load();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //string sSDATE = string.Empty;
            //string sEDATE = string.Empty;

            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                this.FPS91_TY_S_UT_78IH6455.Initialize();

                DataTable dt = new DataTable();

                string sPSid = "";

                if (this.CBO01_OVGUBUN.GetValue().ToString() == "A")
                {
                    sPSid = "TY_P_UT_78IH0454";
                }
                else if (this.CBO01_OVGUBUN.GetValue().ToString() == "O")
                {
                    sPSid = "TY_P_UT_91OB5573";
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sPSid, this.CBH01_CHHWAJU.GetValue().ToString(),
                                                this.CBH01_CHDOCHK.GetValue().ToString(),
                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                this.DTP01_STDATE.GetString(),
                                                this.DTP01_EDDATE.GetString());

                dt = UP_DataTableChange(this.DbConnector.ExecuteDataTable());

                this.FPS91_TY_S_UT_78IH6455.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_UT_78IH6455.ActiveSheet.RowCount; i++)
                {

                    if (this.FPS91_TY_S_UT_78IH6455.GetValue(i, "CHCHULIL").ToString() == "합   계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_UT_78IH6455.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }

                    if (this.FPS91_TY_S_UT_78IH6455.GetValue(i, "HMGUBUN").ToString() == "공휴일")
                    {
                        this.FPS91_TY_S_UT_78IH6455_Sheet1.Cells[i, 0].ForeColor = Color.Blue;
                    }
                }

                UP_Spread_Load();
            }
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sPSid = "";

            if (this.CBO01_OVGUBUN.GetValue().ToString() == "A")
            {
                sPSid = "TY_P_UT_78IH0454";
            }
            else if (this.CBO01_OVGUBUN.GetValue().ToString() == "O")
            {
                sPSid = "TY_P_UT_91OB5573";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sPSid, this.CBH01_CHHWAJU.GetValue().ToString(),
                                            this.CBH01_CHDOCHK.GetValue().ToString(),
                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                            this.DTP01_STDATE.GetString(),
                                            this.DTP01_EDDATE.GetString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR023R(this.TXT01_REFER.GetValue().ToString(),
                                                  this.DTP01_STDATE.GetString(),
                                                  this.DTP01_EDDATE.GetString());

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_UT_78IH6455_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 14, 1, 2);
            this.FPS91_TY_S_UT_78IH6455_Sheet1.AddColumnHeaderSpanCell(0, 16, 1, 2);

            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[0, 14].Value = "출고량";
            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[0, 16].Value = "할증량";

            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[1, 14].Value = "MT";
            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[1, 15].Value = "KL";
            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[1, 16].Value = "MT";
            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[1, 17].Value = "KL";

            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_78IH6455_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        private DataTable UP_DataTableChange(DataTable dt)
        {
            DataTable retDt = dt;

            if (dt.Rows.Count > 0)
            {
                DataRow row = retDt.NewRow();

                row["CHCHULIL"] = "합   계";
                row["CHCARNO"] = "";
                row["CHHWAJU"] = "";
                row["VNSANGHO"] = "";
                row["CHHWAMUL"] = "";
                row["CHHWAMULNM"] = "";
                row["CHDOCHK"] = "";
                row["CDDESC1"] = "";
                row["CHCHSTR"] = 0;
                row["CHCHEND"] = 0;
                row["CHCHTIM"] = "";
                row["CHOVSTR"] = 0;
                row["CHOVEND"] = 0;
                row["CHOVTIM"] = "";

                row["CHMTQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(CHMTQTY)", null));
                row["CHKLQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(CHKLQTY)", null));
                row["CHOVQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(CHOVQTY)", null));
                row["CHOVKLQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(CHOVKLQTY)", null));

                retDt.Rows.Add(row);
            }

            return retDt;
        }
    }
}
