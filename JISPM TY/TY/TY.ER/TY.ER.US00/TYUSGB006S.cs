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
    /// 코드박스 - 입고조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.05.05 00:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_95GG1578 : 코드박스 - 입고조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_95GG2579 : 코드박스 - 입고조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  STHANGCHA : 항 차
    /// </summary>
    public partial class TYUSGB006S : TYBase
    {
        private string fsHANGCHA = string.Empty;

        public string fsIBHANGCHA = string.Empty;
        public string fsIBGOKJONG = string.Empty;
        public string fsIBHWAJU = string.Empty;
        public string fsIBBLNO = string.Empty;
        public string fsIBBLMSN = string.Empty;
        public string fsIBBLHSN = string.Empty;
        public string fsIBHMNO1 = string.Empty;
        public string fsIBHMNO2 = string.Empty;

        #region Descriptoin : 폼 로드
        public TYUSGB006S(string sHANGCHA)
        {
            InitializeComponent();

            fsHANGCHA = sHANGCHA;
        }

        private void TYUSGB006S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_STHANGCHA.SetValue(fsHANGCHA);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Descriptoin : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Descriptoin : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_95GG1578",
                                    this.CBH01_STHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_95GG2579.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                SetFocus(this.FPS91_TY_S_US_95GG2579);
            }
            else
            {
                SetFocus(this.CBH01_STHANGCHA.CodeText);
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_95GG2579_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }

        private void FPS91_TY_S_US_95GG2579_KeyPress(object sender, KeyPressEventArgs e)
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
            fsIBHANGCHA = this.FPS91_TY_S_US_95GG2579.GetValue("IBHANGCHA").ToString();
            fsIBGOKJONG = this.FPS91_TY_S_US_95GG2579.GetValue("IBGOKJONG").ToString();
            fsIBHWAJU = this.FPS91_TY_S_US_95GG2579.GetValue("IBHWAJU").ToString();
            fsIBBLNO = this.FPS91_TY_S_US_95GG2579.GetValue("IBBLNO").ToString();
            fsIBBLMSN = this.FPS91_TY_S_US_95GG2579.GetValue("IBBLMSN").ToString();
            fsIBBLHSN = this.FPS91_TY_S_US_95GG2579.GetValue("IBBLHSN").ToString();
            fsIBHMNO1 = this.FPS91_TY_S_US_95GG2579.GetValue("IBHMNO1").ToString();
            fsIBHMNO2 = this.FPS91_TY_S_US_95GG2579.GetValue("IBHMNO2").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
