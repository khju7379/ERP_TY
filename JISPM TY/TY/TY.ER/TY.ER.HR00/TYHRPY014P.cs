using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여변동사항 상세조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.25 14:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53PG4889 : 급여변동사항 상세내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53PG5890 : 급여변동사항 상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    ///  PAYJIDATE : 지급일자
    ///  PAYYYMM : 급여년월
    /// </summary>
    public partial class TYHRPY014P : TYBase
    {
        private string fsPYGUBN;
        private string fsPYDATE;
        private string fsPYJIDATE;
        private string fsPYSABUN;

        #region  Description : 폼 로드 이벤트
        public TYHRPY014P(string sPYDATE, string sPYGUBN, string sPYJIDATE, string sPYSABUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsPYGUBN = sPYGUBN;
            fsPYDATE = sPYDATE;
            fsPYJIDATE = sPYJIDATE;
            fsPYSABUN = sPYSABUN;
        }

        private void TYHRPY014P_Load(object sender, System.EventArgs e)
        {
            this.CBH01_PAYGUBN.SetValue(fsPYGUBN);
            this.DTP01_PAYYYMM.SetValue(fsPYDATE);
            this.DTP01_PAYJIDATE.SetValue(fsPYJIDATE);
            this.CBH01_KBSABUN.SetValue(fsPYSABUN);

            this.UP_DataBinding();
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK; 
            this.Close();
        }
        #endregion

        #region  Description : 조회 이벤트
        private void UP_DataBinding()
        {
            string sPrePAYYYMM = string.Empty;
            string sPrePAYJIDATE = string.Empty;

            //직전 급여정보 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A4LGQ319", this.CBH01_PAYGUBN.GetValue().ToString(), this.DTP01_PAYYYMM.GetString().Substring(0, 6), this.DTP01_PAYJIDATE.GetString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sPrePAYYYMM = dt.Rows[0]["PAYYYMM"].ToString();
                sPrePAYJIDATE = dt.Rows[0]["PAYJIDATE"].ToString();
            }
            else
            {
                sPrePAYYYMM = this.DTP01_PAYYYMM.GetString().Substring(0, 6);
                sPrePAYJIDATE = this.DTP01_PAYJIDATE.GetString();
            }


            this.FPS91_TY_S_HR_53PG5890.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53PG4889", DTP01_PAYYYMM.GetString().ToString().Substring(0,6),
                                                        CBH01_PAYGUBN.GetValue(),
                                                        DTP01_PAYJIDATE.GetString().ToString(),
                                                        sPrePAYYYMM,
                                                        CBH01_PAYGUBN.GetValue(),
                                                        sPrePAYJIDATE,
                                                        CBH01_KBSABUN.GetValue()
                                                        );

            this.FPS91_TY_S_HR_53PG5890.SetValue(UP_DatatableChange(this.DbConnector.ExecuteDataTable()));

            for (int i = 0; i < this.FPS91_TY_S_HR_53PG5890.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_53PG5890.GetValue(i, "PSDNAME").ToString() == "소  계")
                {
                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_HR_53PG5890.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
                else if (this.FPS91_TY_S_HR_53PG5890.GetValue(i, "PAYGUBN").ToString() == "차인지급액")
                {
                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_HR_53PG5890.ActiveSheet.Rows[i].BackColor = Color.FromArgb(242, 231, 147);
                }
            }
        }
        #endregion

        #region Description : 그리드 소계/합계 추가
        private DataTable UP_DatatableChange(DataTable dt)
        {
            DataTable rtnDt = new DataTable();

            DataRow row;

            double dJUNWOLAMT_HAP = 0;
            double dDANGWOLAMT_HAP = 0;
            double dGAP_HAP = 0;

            double dPYJUNWOLAMT_HAP = 0;
            double dPYDANGWOLAMT_HAP = 0;
            double dPYGAP_HAP = 0;

            rtnDt.Columns.Add("PAYGUBN", typeof(System.String));
            rtnDt.Columns.Add("PIPAYCODE", typeof(System.String));
            rtnDt.Columns.Add("PSDNAME", typeof(System.String));
            rtnDt.Columns.Add("JUNWOLAMT", typeof(System.String));
            rtnDt.Columns.Add("DANGWOLAMT", typeof(System.String));
            rtnDt.Columns.Add("GAP", typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (i > 0)
                {
                    if (dt.Rows[i - 1]["PAYGUBN"].ToString() != dt.Rows[i]["PAYGUBN"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["PAYGUBN"] = DBNull.Value;
                        row["PIPAYCODE"] = DBNull.Value;
                        row["PSDNAME"] = "소  계";
                        row["JUNWOLAMT"] = string.Format("{0:#,##0.0}", dJUNWOLAMT_HAP);
                        row["DANGWOLAMT"] = string.Format("{0:#,##0.0}", dDANGWOLAMT_HAP);
                        row["GAP"] = string.Format("{0:#,##0.0}", dGAP_HAP);

                        rtnDt.Rows.Add(row);

                        dPYJUNWOLAMT_HAP = dJUNWOLAMT_HAP;
                        dPYDANGWOLAMT_HAP = dDANGWOLAMT_HAP;
                        dPYGAP_HAP = dGAP_HAP;
                        
                        dJUNWOLAMT_HAP = double.Parse(dt.Rows[i]["JUNWOLAMT"].ToString());
                        dDANGWOLAMT_HAP = double.Parse(dt.Rows[i]["DANGWOLAMT"].ToString());
                        dGAP_HAP = double.Parse(dt.Rows[i]["GAP"].ToString()); 
                    }
                    else
                    {
                        dJUNWOLAMT_HAP = dJUNWOLAMT_HAP + double.Parse(dt.Rows[i]["JUNWOLAMT"].ToString());
                        dDANGWOLAMT_HAP = dDANGWOLAMT_HAP + double.Parse(dt.Rows[i]["DANGWOLAMT"].ToString());
                        dGAP_HAP = dGAP_HAP + double.Parse(dt.Rows[i]["GAP"].ToString());
                    }
                }
                else
                {
                    dJUNWOLAMT_HAP = double.Parse(dt.Rows[i]["JUNWOLAMT"].ToString());
                    dDANGWOLAMT_HAP = double.Parse(dt.Rows[i]["DANGWOLAMT"].ToString());
                    dGAP_HAP = double.Parse(dt.Rows[i]["GAP"].ToString());
                }

                row = rtnDt.NewRow();

                row["PAYGUBN"] = dt.Rows[i]["PAYGUBN"].ToString();
                row["PIPAYCODE"] = dt.Rows[i]["PIPAYCODE"].ToString();
                row["PSDNAME"] = dt.Rows[i]["PSDNAME"].ToString();
                row["JUNWOLAMT"] = dt.Rows[i]["JUNWOLAMT"].ToString();
                row["DANGWOLAMT"] = dt.Rows[i]["DANGWOLAMT"].ToString();
                row["GAP"] = dt.Rows[i]["GAP"].ToString();

                rtnDt.Rows.Add(row);
            }

            row = rtnDt.NewRow();

            row["PAYGUBN"] = DBNull.Value;
            row["PIPAYCODE"] = DBNull.Value;
            row["PSDNAME"] = "소  계";
            row["JUNWOLAMT"] = string.Format("{0:#,##0.0}",  dJUNWOLAMT_HAP);
            row["DANGWOLAMT"] = string.Format("{0:#,##0.0}", dDANGWOLAMT_HAP);
            row["GAP"] = string.Format("{0:#,##0.0}", dGAP_HAP);

            rtnDt.Rows.Add(row);

            row = rtnDt.NewRow();

            row["PAYGUBN"] = "차인지급액";
            row["PIPAYCODE"] = DBNull.Value;
            row["PSDNAME"] = DBNull.Value;
            row["JUNWOLAMT"] = string.Format("{0:#,##0.0}", dPYJUNWOLAMT_HAP - dJUNWOLAMT_HAP);
            row["DANGWOLAMT"] = string.Format("{0:#,##0.0}", dPYDANGWOLAMT_HAP - dDANGWOLAMT_HAP);
            row["GAP"] = string.Format("{0:#,##0.0}", dPYGAP_HAP - dGAP_HAP);

            rtnDt.Rows.Add(row);

            return rtnDt;
        }
        #endregion
    }
}
