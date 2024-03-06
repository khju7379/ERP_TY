using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 코드박스 - 모선스케줄조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.03.15 13:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_93FDE135 : 모선스케줄 조회 - SILO
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_93FDF136 : 모선스케줄 조회 - SILO
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUSGB003S : TYBase
    {
        public string fsSHDATE = string.Empty;
        public string fsSHSEQ = string.Empty;
        public string fsSHETAULSAN = string.Empty;
        public string fsSHETAULTIME = string.Empty;
        public string fsSHULSANQTY = string.Empty;
        public string fsSHSOSOK = string.Empty;
        public string fsSKDESC1 = string.Empty;
        public string fsSHGOKJONG = string.Empty;
        public string fsSHWONSAN = string.Empty;
        public string fsSHAGENT = string.Empty;
        public string fsSHSURVEY = string.Empty;
        public string fsSHETCD_S = string.Empty;
        public string fsSHETCD_E = string.Empty;

        #region Descripton : 폼 로드
        public TYUSGB003S()
        {
            InitializeComponent();
        }

        private void TYUSGB003S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Descripton : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Descripton : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_US_93FDE135",
               this.DTP01_STDATE.GetString(),
               this.DTP01_EDDATE.GetString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_93FDF136.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                SetFocus(this.FPS91_TY_S_US_93FDF136);
            }
            else
            {
                SetFocus(this.DTP01_STDATE);
            }
        }
        #endregion

        #region Description : 그리드 더블 클릭
        private void FPS91_TY_S_US_93FDF136_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }
        
        private void FPS91_TY_S_US_93FDF136_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_Get_Data();
            }
        }
        #endregion

        #region Description : 스프레드 데이터 가져오기
        private void UP_Get_Data()
        {
            fsSHDATE = this.FPS91_TY_S_US_93FDF136.GetValue("SHDATE").ToString();
            fsSHSEQ = this.FPS91_TY_S_US_93FDF136.GetValue("SHSEQ").ToString();
            fsSHETAULSAN = this.FPS91_TY_S_US_93FDF136.GetValue("SHETAULSAN").ToString();
            fsSHETAULTIME = this.FPS91_TY_S_US_93FDF136.GetValue("SHETAULTIME").ToString();
            fsSHULSANQTY = this.FPS91_TY_S_US_93FDF136.GetValue("SHULSANQTY").ToString();
            fsSHSOSOK = this.FPS91_TY_S_US_93FDF136.GetValue("SHSOSOK").ToString();
            fsSKDESC1 = this.FPS91_TY_S_US_93FDF136.GetValue("SKDESC1").ToString();
            fsSHGOKJONG = this.FPS91_TY_S_US_93FDF136.GetValue("SHGOKJONG").ToString();
            fsSHWONSAN = this.FPS91_TY_S_US_93FDF136.GetValue("SHWONSAN").ToString();
            fsSHAGENT = this.FPS91_TY_S_US_93FDF136.GetValue("SHAGENT").ToString();
            fsSHSURVEY = this.FPS91_TY_S_US_93FDF136.GetValue("SHSURVEY").ToString();
            fsSHETCD_S = this.FPS91_TY_S_US_93FDF136.GetValue("SHETCD_S").ToString();
            fsSHETCD_E = this.FPS91_TY_S_US_93FDF136.GetValue("SHETCD_E").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
