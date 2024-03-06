using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출수기 통관재고 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.04.10 13:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A4AGD252 : 반출수기 통관재고 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A4AGG253 : 반출수기 통관재고 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  CSHWAJU : 화주
    ///  CSHWAMUL : 화물
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    ///  VSJUKHA : 적하목록번호
    /// </summary>
    public partial class TYEDKB13C1 : TYBase
    {
        public string fsCSIPHANG;
        public string fsCSBONSUN;
        public string fsCSHWAJU;
        public string fsCSHWAMUL;
        public string fsVSJUKHA;
        public string fsCSBLNO;
        public string fsCSMSNSEQ;
        public string fsCSHSNSEQ;
        public string fsCSSINNO;

        public string fsIPSINOYY;
        public string fsIPSINO;
        public string fsCHASU;


        #region  Description : 폼 로드 이벤트
        public TYEDKB13C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYEDKB13C1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            BTN61_INQ_Click(null, null);

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_A4AGG253.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4AGD252", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.TXT01_VSJUKHA.GetValue().ToString(), this.CBH01_CSHWAJU.GetValue(), this.CBH01_CSHWAMUL.GetValue() );
            this.FPS91_TY_S_UT_A4AGG253.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_A4AGG253_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_A4AGG253_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            fsCSIPHANG = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSIPHANG").ToString();
            fsCSBONSUN = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSBONSUN").ToString();
            fsCSHWAJU = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSHWAJU").ToString();
            fsCSHWAMUL = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSHWAMUL").ToString();
            fsVSJUKHA = this.FPS91_TY_S_UT_A4AGG253.GetValue("VSJUKHA").ToString();
            fsCSBLNO = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSBLNO").ToString();
            fsCSMSNSEQ = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSMSNSEQ").ToString();
            fsCSHSNSEQ = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSHSNSEQ").ToString();
            fsCSSINNO = this.FPS91_TY_S_UT_A4AGG253.GetValue("CSSINNO").ToString();
            fsCHASU = this.FPS91_TY_S_UT_A4AGG253.GetValue("CHASU").ToString();

            string[] sSINO = this.FPS91_TY_S_UT_A4AGG253.GetValue("IPSINO").ToString().Split('-');

            fsIPSINOYY = sSINO[0].ToString();
            fsIPSINO = sSINO[1].ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion



    }
}
