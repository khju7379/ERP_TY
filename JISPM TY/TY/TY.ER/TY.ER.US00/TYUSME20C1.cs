using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.US00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
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
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYUSME20C1 : TYBase
    {
        public string fsUTDATE    = string.Empty;
	    public string fsUTMCYYMM  = string.Empty;
	    public string fsUTHANGCHA = string.Empty;
	    public string fsUTSEQ     = string.Empty;
	    public string fsUTGOKJONG = string.Empty;
	    public string fsUTHWAJU   = string.Empty;
	    public string fsUTBLMSN   = string.Empty;
	    public string fsUTBLHSN   = string.Empty;
	    public string fsUTYDHWAJU = string.Empty;
	    public string fsUTYSHWAJU = string.Empty;
	    public string fsUTCHHWAJU = string.Empty;
	    public string fsUTYSDATE  = string.Empty;
	    public string fsUTYSSEQ   = string.Empty;
	    public string fsUTYDSEQ   = string.Empty;
        public string fsUTMAECH   = string.Empty;

        #region Description : 페이지 로드
        public TYUSME20C1(string sUTMAECH)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsUTMAECH = sUTMAECH.ToString();
        }

        private void TYUSME20C1_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_92LAS838.Initialize();

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (fsUTMAECH.ToString() == "시설사용료")
            {
                sProcedure = "TY_P_US_92LAP829";
            }
            else if (fsUTMAECH.ToString() == "하역료")
            {
                sProcedure = "TY_P_US_92LAP830";
            }
            else if (fsUTMAECH.ToString() == "보관료")
            {
                sProcedure = "TY_P_US_92LAS836";
            }
            else if (fsUTMAECH.ToString() == "조출료")
            {
                sProcedure = "TY_P_US_92LAQ831";
            }
            else if (fsUTMAECH.ToString() == "난작업비")
            {
                sProcedure = "TY_P_US_92LAQ832";
            }
            else if (fsUTMAECH.ToString() == "이송료")
            {
                sProcedure = "TY_P_US_92LAQ833";
            }
            else if (fsUTMAECH.ToString() == "소급분-시설사용료")
            {
                sProcedure = "TY_P_US_92LAR834";
            }
            else if (fsUTMAECH.ToString() == "소급분-하역료")
            {
                sProcedure = "TY_P_US_92LAR835";
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               sProcedure.ToString(),
               Get_Date(this.DTP01_STDATE.GetValue().ToString()),
               Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
               this.CBH01_SHWAJU.GetValue().ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_92LAS838.SetValue(dt);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_US_92LAS838_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsUTDATE    = "";
	        fsUTMCYYMM  = "";
	        fsUTHANGCHA = "";
	        fsUTSEQ     = "";
	        fsUTGOKJONG = "";
	        fsUTHWAJU   = "";
	        fsUTBLMSN   = "";
	        fsUTBLHSN   = "";
	        fsUTYDHWAJU = "";
	        fsUTYSHWAJU = "";
	        fsUTCHHWAJU = "";
	        fsUTYSDATE  = "";
	        fsUTYSSEQ   = "";
            fsUTYDSEQ   = "";

            fsUTDATE    = this.FPS91_TY_S_US_92LAS838.GetValue("UTDATE").ToString();
            fsUTMCYYMM  = this.FPS91_TY_S_US_92LAS838.GetValue("UTMCYYMM").ToString();
            fsUTHANGCHA = this.FPS91_TY_S_US_92LAS838.GetValue("UTHANGCHA").ToString();
            fsUTSEQ     = this.FPS91_TY_S_US_92LAS838.GetValue("UTSEQ").ToString();
            fsUTGOKJONG = this.FPS91_TY_S_US_92LAS838.GetValue("UTGOKJONG").ToString();
            fsUTHWAJU   = this.FPS91_TY_S_US_92LAS838.GetValue("UTHWAJU").ToString();
            fsUTBLMSN   = this.FPS91_TY_S_US_92LAS838.GetValue("UTBLMSN").ToString();
            fsUTBLHSN   = this.FPS91_TY_S_US_92LAS838.GetValue("UTBLHSN").ToString();
            fsUTYDHWAJU = this.FPS91_TY_S_US_92LAS838.GetValue("UTYDHWAJU").ToString();
            fsUTYSHWAJU = this.FPS91_TY_S_US_92LAS838.GetValue("UTYSHWAJU").ToString();
            fsUTCHHWAJU = this.FPS91_TY_S_US_92LAS838.GetValue("UTCHHWAJU").ToString();
            fsUTYSDATE  = this.FPS91_TY_S_US_92LAS838.GetValue("UTYSDATE").ToString();
            fsUTYSSEQ   = this.FPS91_TY_S_US_92LAS838.GetValue("UTYSSEQ").ToString();
            fsUTYDSEQ   = this.FPS91_TY_S_US_92LAS838.GetValue("UTYDSEQ").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}