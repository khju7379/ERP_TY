using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 코드박스 - 투자수선예산 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B922230 : 코드박스 - 본예산 조회
    ///  TY_P_MR_2B92N231 : 코드박스 - 본예산 월 조회(투자&수선, 소모성 제외)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B930232 : 코드박스 - 코드박스 - 소모품비세목예산 조회
    ///  TY_S_MR_2B88E216 : 코드박스 - 예산(투자&수선, 소모품비, 기타) 월 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GCDDP : 사업장코드
    ///  GDATE : 일자
    /// </summary>
    public partial class TYMRGB007S : TYBase
    {
        public string fsCDAC    = string.Empty;
        public string fsCDACNM  = string.Empty;
        public string fsYESANNM = string.Empty;

        #region Description : 페이지 로드
        public TYMRGB007S(string sDATE, string sCDDP, string sCDDPNM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.TXT01_GDATE.SetValue(sDATE);

            this.TXT01_GCDDP.SetValue(sCDDP);
            this.TXT01_GCDDPNM.SetValue(sCDDPNM);
        }

        private void TYMRGB007S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_GDATE.SetReadOnly(true);
            this.TXT01_GCDDP.SetReadOnly(true);
            this.TXT01_GCDDPNM.SetReadOnly(true);

            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_GCDAC);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2B930232.Initialize();
            this.FPS91_TY_S_MR_2B88E216.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_2B922230",
               this.TXT01_GDATE.GetValue(),
               this.TXT01_GCDDP.GetValue().ToString().Substring(0, 4).ToString(),
               this.TXT01_GCDAC.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2B930232.SetValue(dt);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 본예산 이벤트
        private void FPS91_TY_S_MR_2B930232_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsCDAC   = "";
            fsCDACNM = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_2B92N231",
               this.TXT01_GDATE.GetValue(),
               this.TXT01_GCDDP.GetValue().ToString().Substring(0, 4).ToString(),
               this.FPS91_TY_S_MR_2B930232.GetValue("MMCDAC").ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2B88E216.SetValue(dt);

                fsCDAC    = this.FPS91_TY_S_MR_2B930232.GetValue("MMCDAC").ToString();
                fsCDACNM  = this.FPS91_TY_S_MR_2B930232.GetValue("A1NMAC").ToString();
                fsYESANNM = "";
            }
        }
        #endregion

        #region Description : 스프레드 본예산 월 이벤트
        private void FPS91_TY_S_MR_2B88E216_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
