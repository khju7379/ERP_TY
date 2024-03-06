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
    /// 코드박스 - 통관조회 프로그램입니다.
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
    ///  TY_P_US_96IIJ901 : 코드박스 - 통관조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_96IIL904 : 코드박스 - 통관조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  JCHANGCHA : 항차
    /// </summary>
    public partial class TYUSGB007S : TYBase
    {
        private string fsHANGCHA = string.Empty;

        public string fsJCHANGCHA = string.Empty;
        public string fsJCGOKJONG = string.Empty;
        public string fsJCHWAJU = string.Empty;
        public string fsJCBLNO = string.Empty;
        public string fsJCBLMSN = string.Empty;
        public string fsJCBLHSN = string.Empty;
        public string fsJCCUSTIL = string.Empty;
        public string fsJCCUSTCH = string.Empty;
        public string fsYDSEQ = string.Empty;

        // USIJECSNF
        public string fsJCWNHWAJU = string.Empty;
        public string fsJCYDHWAJU = string.Empty;
        public string fsJCYSDATE = string.Empty;
        public string fsJCYSSEQ = string.Empty;
        public string fsJCYDSEQ = string.Empty;

        #region Description : 폼 로드
        public TYUSGB007S(string sHANGCHA)
        {
            InitializeComponent();

            fsHANGCHA = sHANGCHA;
        }

        private void TYUSGB007S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_JCHANGCHA.SetValue(fsHANGCHA);

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
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_96IIJ901",
                                    this.CBH01_JCHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_96IIL904.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                SetFocus(this.FPS91_TY_S_US_96IIL904);
            }
            else
            {
                SetFocus(this.CBH01_JCHANGCHA.CodeText);
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_96IIL904_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }

        private void FPS91_TY_S_US_96IIL904_KeyPress(object sender, KeyPressEventArgs e)
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
            fsJCHANGCHA = this.FPS91_TY_S_US_96IIL904.GetValue("JCHANGCHA").ToString();     // 항차   txtYNHANGCHA
            fsJCGOKJONG = this.FPS91_TY_S_US_96IIL904.GetValue("JCGOKJONG").ToString();     // 곡종   txtYNGOKJONG
            fsJCHWAJU = this.FPS91_TY_S_US_96IIL904.GetValue("JCHWAJU").ToString();         // 양수화주 txtYNHWAJU
            fsJCBLNO = this.FPS91_TY_S_US_96IIL904.GetValue("JCBLNO").ToString();           // B/L 번호   txtYNBLNO
            fsJCBLMSN = this.FPS91_TY_S_US_96IIL904.GetValue("JCBLMSN").ToString();         // MSN  txtYNBLMSN
            fsJCBLHSN = this.FPS91_TY_S_US_96IIL904.GetValue("JCBLHSN").ToString();         // HSN  txtYNBLHSN
            fsJCCUSTIL = this.FPS91_TY_S_US_96IIL904.GetValue("JCCUSTIL").ToString();       // 통관일자 txtYNCUSTIL
            fsJCCUSTCH = this.FPS91_TY_S_US_96IIL904.GetValue("JCSEQ").ToString();             // 통관차수 txtYNCUSTCH

            fsJCYDHWAJU = this.FPS91_TY_S_US_96IIL904.GetValue("JCYDHWAJU").ToString();     // 양도화주 txtJCYDHWAJU
            fsJCYSDATE = this.FPS91_TY_S_US_96IIL904.GetValue("JCYSDATE").ToString();       // 양수일자 txtJCYSDATE
            fsJCYSSEQ = this.FPS91_TY_S_US_96IIL904.GetValue("JCYSSEQ").ToString();         // 양수순번 txtJCYSSEQ
            fsJCYDSEQ = this.FPS91_TY_S_US_96IIL904.GetValue("JCYDSEQ").ToString();         // 양도순번 txtJCYDSEQ
            fsJCWNHWAJU = this.FPS91_TY_S_US_96IIL904.GetValue("JCWNHWAJU").ToString();     // 원화주  txtYNYNHWAJU

            fsYDSEQ = (Convert.ToDouble(fsJCYDSEQ) + 1).ToString();                       // 양도순번   txtYNYDSEQ

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
