using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 출고대장 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.21 11:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73LBS023 : 출고대장 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73LBT025 : 출고대장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR001S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR001S()
        {
            InitializeComponent();
        }

        private void TYUTPR001S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {   
            this.FPS91_TY_S_UT_73LBT025.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBH01_CHHWAJU.GetValue().ToString() != "")
            {
                 // 대표거래처 코드 가져오기
                string sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                this.DbConnector.Attach("TY_P_UT_73LBS023", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sHWAJU,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_75PJ0615", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                                                            );
            }

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_MTTOTAL.SetValue(dt.Compute("SUM(CHMTQTY)", "").ToString());
                this.TXT01_KLTOTAL.SetValue(dt.Compute("SUM(CHKLQTY)", "").ToString());
            }
            else
            {
                this.TXT01_MTTOTAL.SetValue("0");
                this.TXT01_KLTOTAL.SetValue("0");
            }

            this.FPS91_TY_S_UT_73LBT025.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = this.DTP01_STDATE.GetString();
            string sEDDATE = this.DTP01_EDDATE.GetString();
            string sDATE = "(" + sSTDATE.Substring(0, 4) + "/" + sSTDATE.Substring(4, 2) + "/" + sSTDATE.Substring(6, 2) + "-" + sEDDATE.Substring(0, 4) + "/" + sEDDATE.Substring(4, 2) + "/" + sEDDATE.Substring(6, 2) + ")";

            this.DbConnector.CommandClear();

            if (this.CBH01_CHHWAJU.GetValue().ToString() != "")
            {
                // 대표거래처 코드 가져오기
                string sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                this.DbConnector.Attach("TY_P_UT_73LG4040", sDATE,
                                                            sSTDATE,
                                                            sEDDATE,
                                                            sHWAJU,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_75PJ2616", sDATE,
                                                            sSTDATE,
                                                            sEDDATE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                                                            );
            }

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                string sCHMTQTY = string.Empty;
                string sCHKLQTY = string.Empty;

                sCHMTQTY = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CHMTQTY)", null).ToString()));
                sCHKLQTY = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CHKLQTY)", null).ToString()));

                ActiveReport rpt = new TYUTPR001R(sCHMTQTY, sCHKLQTY);
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
