using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여지급관리 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.18 10:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CIA9857 : 급여지급관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CIA0859 : 급여지급관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  PAYGUBN : 급여구분
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY006P : TYBase
    {
        public string fsPAYGUBN = string.Empty;
        public string fsPAYYYMM = string.Empty;
        public string fsPAYJIDATE = string.Empty;
        
        #region  Description : 폼로드 이벤트
        public TYHRPY006P()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYHRPY006P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-2).ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            this.FPS91_TY_S_HR_4CIA0859.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_75VGZ687", this.DTP01_SDATE.GetString().Substring(0, 6), this.DTP01_EDATE.GetString().Substring(0, 6), this.CBH01_PAYGUBN.GetValue());
            this.FPS91_TY_S_HR_4CIA0859.SetValue(this.DbConnector.ExecuteDataTable());

        }

        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_SDATE.GetString();
            string EDDATE = this.DTP01_EDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.DTP01_SDATE.Focus();
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 스프레드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_4CIA0859_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsPAYGUBN = this.FPS91_TY_S_HR_4CIA0859.GetValue("PAYGUBN").ToString();
            fsPAYYYMM = this.FPS91_TY_S_HR_4CIA0859.GetValue("PAYYYMM").ToString();
            fsPAYJIDATE = this.FPS91_TY_S_HR_4CIA0859.GetValue("PAYJIDATE").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

    }
}
