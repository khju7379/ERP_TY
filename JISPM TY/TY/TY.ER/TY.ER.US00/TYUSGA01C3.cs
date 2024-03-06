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
    /// 재고 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.10.02 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_9A8IZ303 : 수동계근 재고 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_9A8J0304 : 수동계근 재고 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  CHGOKJONG : 곡종
    ///  CHHWAJU : 화주
    /// </summary>
    public partial class TYUSGA01C3 : TYBase
    {
        private string fsCHHWAJU = string.Empty;

        public string fsJCHWAJU = string.Empty;
        public string fsJCHWAJUNM = string.Empty;
        public string fsJCGOKJONG = string.Empty;
        public string fsJCHANGCHA = string.Empty;
        public string fsJCCUSTIL = string.Empty;
        public string fsJCSEQ = string.Empty;
        public string fsJCBLMSN = string.Empty;
        public string fsJCBLHSN = string.Empty;
        public string fsJCBLNO = string.Empty;
        public string fsJCYDHWAJU = string.Empty;
        public string fsJCHMNO1 = string.Empty;
        public string fsJCHMNO2 = string.Empty;
        public string fsJCYSDATE = string.Empty;
        public string fsJCYSSEQ = string.Empty;
        public string fsJCYDSEQ = string.Empty;
        public string fsJCWNHWAJU = string.Empty;
        public string fsJCWONSAN = string.Empty;

        #region Description : 폼 로드
        public TYUSGA01C3(string sCHHWAJU)
        {
            InitializeComponent();

            fsCHHWAJU = sCHHWAJU;
        }

        private void TYUSGA01C3_Load(object sender, System.EventArgs e)
        {
            this.CBH01_CHHWAJU.SetValue(fsCHHWAJU);
            //SetStartingFocus(this.CBH01_CHHWAJU);

            BTN61_INQ_Click(null, null);
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
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9A8IZ303",
                                    CBH01_CHHWAJU.GetValue().ToString(),
                                    CBH01_CHGOKJONG.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_9A8J0304.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                SetFocus(FPS91_TY_S_US_9A8J0304);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                
                SetFocus(this.CBH01_CHHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 스프레드 선택
        private void FPS91_TY_S_US_9A8J0304_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }

        private void FPS91_TY_S_US_9A8J0304_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_Get_Data();
            }
        }
        #endregion

        #region Description : 데이터 선택
        private void UP_Get_Data()
        {
            fsJCHWAJU = this.FPS91_TY_S_US_9A8J0304.GetValue("JCHWAJU").ToString();     // 화주
            fsJCHWAJUNM = this.FPS91_TY_S_US_9A8J0304.GetValue("JCHWAJUNM").ToString(); // 화주명
            fsJCGOKJONG = this.FPS91_TY_S_US_9A8J0304.GetValue("JCGOKJONG").ToString(); // 곡종
            fsJCHANGCHA = this.FPS91_TY_S_US_9A8J0304.GetValue("JCHANGCHA").ToString(); // 항차
            fsJCCUSTIL = this.FPS91_TY_S_US_9A8J0304.GetValue("JCCUSTIL").ToString();   // 통관일자
            fsJCSEQ = this.FPS91_TY_S_US_9A8J0304.GetValue("JCSEQ").ToString();         // 통관차수
            fsJCBLMSN = this.FPS91_TY_S_US_9A8J0304.GetValue("JCBLMSN").ToString();     // MSN
            fsJCBLHSN = this.FPS91_TY_S_US_9A8J0304.GetValue("JCBLHSN").ToString();     // HSN
            fsJCBLNO = this.FPS91_TY_S_US_9A8J0304.GetValue("JCBLNO").ToString();       // B/L번호
            fsJCYDHWAJU = this.FPS91_TY_S_US_9A8J0304.GetValue("JCYDHWAJU").ToString(); // 양도화주
            fsJCHMNO1 = this.FPS91_TY_S_US_9A8J0304.GetValue("JCHMNO1").ToString();     // 화물번호1
            fsJCHMNO2 = this.FPS91_TY_S_US_9A8J0304.GetValue("JCHMNO2").ToString();     // 화물번호2
            fsJCYSDATE = this.FPS91_TY_S_US_9A8J0304.GetValue("JCYSDATE").ToString();   // 양수일자
            fsJCYSSEQ = this.FPS91_TY_S_US_9A8J0304.GetValue("JCYSSEQ").ToString();     // 양수순번
            fsJCYDSEQ = this.FPS91_TY_S_US_9A8J0304.GetValue("JCYDSEQ").ToString();     // 양도차수
            fsJCWNHWAJU = this.FPS91_TY_S_US_9A8J0304.GetValue("JCWNHWAJU").ToString(); // 원화주
            fsJCWONSAN = this.FPS91_TY_S_US_9A8J0304.GetValue("JCWONSAN").ToString();   // 원산지
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
