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
    /// 코드박스 - B/L별 입고관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.06.18 18:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_9BCHG503 : B/L별 입고관리 B/L분할 사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_US_9BCHG503 : B/L 분할사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    /// </summary>
    public partial class TYUSGB008S : TYBase
    {
        private string fsIBHANGCHA = string.Empty;
        private string fsIBGOKJONG = string.Empty;
        private string fsIBBLNO = string.Empty;
        private string fsIBBLMSN = string.Empty;

        public string fsIBHWAJU = string.Empty;
        public string fsIBBLHSN = string.Empty;
        public string fsIBBLSEQ = string.Empty;
        public string fsIBBEJNQTY = string.Empty;
        public string fsIBHWAKQTY = string.Empty;
        public string fsIBHMNO1 = string.Empty;
        public string fsIBHMNO2 = string.Empty;

        #region Description : 폼 로드
        public TYUSGB008S(string sIBHANGCHA, string sIBGOKJONG, string sIBBLNO, string sIBBLMSN)
        {
            InitializeComponent();

            fsIBHANGCHA = sIBHANGCHA;
            fsIBGOKJONG = sIBGOKJONG;
            fsIBBLNO = sIBBLNO;
            fsIBBLMSN = sIBBLMSN;
        }

        private void TYUSGB008S_Load(object sender, System.EventArgs e)
        {
            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.FPS91_TY_S_US_9BCHG504);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_9BCHG504.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_9BCHG503",
                                    fsIBHANGCHA,
                                    fsIBGOKJONG,
                                    fsIBBLNO,
                                    fsIBBLMSN
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_US_9BCHG504.SetValue(dt);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_9BCHG504_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }
        #endregion

        #region Description : 스프레드 엔터키 입력
        private void FPS91_TY_S_US_9BCHG504_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_Get_Data();
            }
        }
        #endregion

        #region Descriptoin : 데이터 가져오기
        private void UP_Get_Data()
        {
            fsIBHWAJU = this.FPS91_TY_S_US_9BCHG504.GetValue("IBHWAJU").ToString();
            fsIBBLHSN = this.FPS91_TY_S_US_9BCHG504.GetValue("IBBLHSN").ToString();
            fsIBBLSEQ = this.FPS91_TY_S_US_9BCHG504.GetValue("IBBLSEQ").ToString();
            fsIBBEJNQTY = this.FPS91_TY_S_US_9BCHG504.GetValue("IBBEJNQTY").ToString();
            fsIBHWAKQTY = this.FPS91_TY_S_US_9BCHG504.GetValue("IBHWAKQTY").ToString();
            fsIBHMNO1 = this.FPS91_TY_S_US_9BCHG504.GetValue("IBHMNO1").ToString();
            fsIBHMNO2 = this.FPS91_TY_S_US_9BCHG504.GetValue("IBHMNO2").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
