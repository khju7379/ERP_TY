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
    /// 차량 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.10.08 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_9A8D8292 : 차량 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_9A8D9293 : 차량 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  CHNUMBER : 차량번호
    ///  CHNUMBER1 : 트럭번호1
    /// </summary>
    public partial class TYUSGA01C4 : TYBase
    {
        private string fsCHNUMBER = string.Empty;

        public string fsTRNUMNO2 = string.Empty;
        public string fsTRNUMNO1 = string.Empty;
        public string fsTRHWAJU1 = string.Empty;
        public string fsTRUNSONG = string.Empty;
        public string fsTRHYUNGT = string.Empty;
        public string fsTRCOUNT = string.Empty;

        #region Description : 폼 로드
        public TYUSGA01C4(string sCHNUMBER)
        {
            InitializeComponent();

            fsCHNUMBER = sCHNUMBER;
        }

        private void TYUSGA01C4_Load(object sender, System.EventArgs e)
        {   
            this.TXT01_CHNUMBER.SetValue(fsCHNUMBER);

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
            this.DbConnector.Attach("TY_P_US_9A8D8292",
                                    TXT01_CHNUMBER.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_9A8D9293.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                if (this.TXT01_CHNUMBER.GetValue().ToString() != "")
                {
                    SetFocus(FPS91_TY_S_US_9A8D9293);
                }
                else
                {
                    SetFocus(this.TXT01_CHNUMBER);
                }
            }
            else
            {
                SetFocus(this.TXT01_CHNUMBER);
            }
        }
        #endregion

        #region Description : 스프레드 선택
        private void FPS91_TY_S_US_9A8D9293_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }

        private void FPS91_TY_S_US_9A8D9293_KeyPress(object sender, KeyPressEventArgs e)
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
            fsTRNUMNO2 = this.FPS91_TY_S_US_9A8D9293.GetValue("TRNUMNO2").ToString();   // 차량번호1
            fsTRNUMNO1 = this.FPS91_TY_S_US_9A8D9293.GetValue("TRNUMNO1").ToString();   // 차량번호2
            fsTRHWAJU1 = this.FPS91_TY_S_US_9A8D9293.GetValue("TRHWAJU1").ToString();   // 출고화주1
            fsTRUNSONG = this.FPS91_TY_S_US_9A8D9293.GetValue("TRUNSONG").ToString();   // 차량소속
            fsTRHYUNGT = this.FPS91_TY_S_US_9A8D9293.GetValue("TRHYUNGT").ToString();   // 차량형태
            fsTRCOUNT = this.FPS91_TY_S_US_9A8D9293.GetValue("TRCOUNT").ToString();     // 게이트 허용

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
