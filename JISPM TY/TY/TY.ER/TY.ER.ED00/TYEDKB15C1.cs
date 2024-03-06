using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출통고목록 재고 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.05.26 15:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A5QH2574 : 입항일자로부터 5개월 이상된 미통관 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A5QH5578 : 입항일자로부터 5개월 이상된 미통관분 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  IPHWAJU : 화주
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYEDKB15C1 : TYBase
    {
           
           public string fsEDNJUKHA;
           public string fsEDNBLMSN;
           public string fsEDNBLHSN;
           public string fsEDNIPHANG;
           public string fsEDNBONSUN;
           public string fsEDNHWAJU;
           public string fsEDNHWAMUL;
           public string fsEDNBLNO;
           public string fsEDNIPQTY;
           public string fsEDNHWAJUNAME;
           public string fsEDNADDR;

        #region  Description : 폼 로드 이벤트
        public TYEDKB15C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYEDKB15C1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_EDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_A5QH5578.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A5QH2574", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), CBH01_IPHWAJU.GetValue());
            this.FPS91_TY_S_UT_A5QH5578.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_A5QH5578_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_A5QH5578_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {           
            fsEDNJUKHA = this.FPS91_TY_S_UT_A5QH5578.GetValue("VSJUKHA").ToString();
            fsEDNBLMSN = this.FPS91_TY_S_UT_A5QH5578.GetValue("IPMSNSEQ").ToString();
            fsEDNBLHSN = this.FPS91_TY_S_UT_A5QH5578.GetValue("IPHSNSEQ").ToString();
            fsEDNIPHANG = this.FPS91_TY_S_UT_A5QH5578.GetValue("IPIPHANG").ToString();
            fsEDNBONSUN = this.FPS91_TY_S_UT_A5QH5578.GetValue("IPBONSUN").ToString();
            fsEDNHWAJU    = this.FPS91_TY_S_UT_A5QH5578.GetValue("IPHWAJU").ToString();
            fsEDNHWAJUNAME = this.FPS91_TY_S_UT_A5QH5578.GetValue("HJDESC1").ToString();
            fsEDNHWAMUL = this.FPS91_TY_S_UT_A5QH5578.GetValue("IPHWAMUL").ToString();
            fsEDNBLNO   = this.FPS91_TY_S_UT_A5QH5578.GetValue("IPBLNO").ToString();
            fsEDNIPQTY = Convert.ToString(Convert.ToDouble(this.FPS91_TY_S_UT_A5QH5578.GetValue("CSJEQTY").ToString()) * 1000);
            fsEDNADDR = this.FPS91_TY_S_UT_A5QH5578.GetValue("VNNEWADD").ToString();

            BTN61_CLO_Click(null, null);
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        

    }
}
