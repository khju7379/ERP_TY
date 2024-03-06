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
    /// 입고화물 현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.25 15:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_75PEA602 : 입고화물 현황 조회(TK,PP 제외)
    ///  TY_P_UT_75PEB605 : 입고화물 현황 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  PRGUBN : 구분
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR020P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR020P()
        {
            InitializeComponent();
        }

        private void TYUTPR020P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                string sSDATE = string.Empty;
                string sEDATE = string.Empty;
                string sDATE = string.Empty;

                sSDATE = this.DTP01_STDATE.GetString();
                sEDATE = this.DTP01_EDDATE.GetString();

                sSDATE = sSDATE.Substring(0, 4) + "/" + sSDATE.Substring(4, 2) + "/" + sSDATE.Substring(6, 2);
                sEDATE = sEDATE.Substring(0, 4) + "/" + sEDATE.Substring(4, 2) + "/" + sEDATE.Substring(6, 2);

                sDATE = "(" + sSDATE + "~" + sEDATE + ")";
                                
                this.DbConnector.CommandClear();

                if (this.CBO01_PRGUBN.GetValue().ToString() == "0")
                {
                    this.DbConnector.Attach("TY_P_UT_75PEB605", sDATE,
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString());
                }
                else
                {
                    this.DbConnector.Attach("TY_P_UT_75PEA602", sDATE,
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString());
                }

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUTPR020R();

                    //가로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            catch
            {

            }			
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                string sSDATE = string.Empty;
                string sEDATE = string.Empty;
                string sDATE = string.Empty;

                sSDATE = this.DTP01_STDATE.GetString();
                sEDATE = this.DTP01_EDDATE.GetString();

                sSDATE = sSDATE.Substring(0, 4) + "/" + sSDATE.Substring(4, 2) + "/" + sSDATE.Substring(6, 2);
                sEDATE = sEDATE.Substring(0, 4) + "/" + sEDATE.Substring(4, 2) + "/" + sEDATE.Substring(6, 2);

                sDATE = "(" + sSDATE + "~" + sEDATE + ")";

                this.FPS91_TY_S_UT_7AVBK914.Initialize();

                this.DbConnector.CommandClear();

                if (this.CBO01_PRGUBN.GetValue().ToString() == "0")
                {
                    this.DbConnector.Attach("TY_P_UT_7B998969", this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString());
                }
                else
                {
                    this.DbConnector.Attach("TY_P_UT_7B99B970", this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString());
                }

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_7AVBK914.SetValue(dt);
            }
            catch
            {

            }
        }
        #endregion
    }
}
