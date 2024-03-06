using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.AC00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 전자세금계산서 메일 재발송 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.06.02 17:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_UT_762HS716 : 재발송 하시겠습니까?
    ///  TY_M_UT_762HS717 : 재발송 되었습니다
    /// 
    ///  # 필드사전 정보 ####
    ///  BILL_SEND : 계산서발행
    ///  CLO : 닫기
    ///  BYR_EMAIL : 공급받는자 메일
    ///  CONVERSATION_ID : 전자세금계산서 ID
    /// </summary>
    public partial class TYUTME24C1 : TYBase
    {
        private string fsCONVERSATION_ID;
        private string fsBYR_MAIL;
        private string fsID;
        private string fsPass;
        private string fsSTATUS;

        #region  Description : 폼 로드 이벤트
        public TYUTME24C1(string sSTATUS, string sCONVERSATION_ID, string sBYR_MAIL, string sID, string sPass)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsSTATUS = sSTATUS;
            fsCONVERSATION_ID = sCONVERSATION_ID;
            fsBYR_MAIL = sBYR_MAIL;
            fsID = sID;
            fsPass = sPass;

        }

        private void TYUTME24C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BILL_SEND.ProcessCheck += new TButton.CheckHandler(BTN61_BILL_SEND_ProcessCheck);
            
            TXT01_CONVERSATION_ID.SetValue(fsCONVERSATION_ID);
            TXT01_BYR_EMAIL.SetValue(fsBYR_MAIL);

            this.SetStartingFocus(TXT01_BYR_EMAIL);
        }
        #endregion

        #region  Description : 재발송 버튼 이벤트
        private void BTN61_BILL_SEND_Click(object sender, EventArgs e)
        {
            string sUrl = "http://192.168.100.32:10000/callSB_V3/XXSB_DTI_SEND_EMAIL.asp?CONVERSATION_ID=" + TXT01_CONVERSATION_ID.GetValue().ToString() + "&EMAIL=" + TXT01_BYR_EMAIL.GetValue().ToString() + "&ID="+fsID+"&PASS="+fsPass+"&STATUS=" + fsSTATUS + "";

            if ((new TYERGB013P(sUrl)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
            }
            this.ShowMessage("TY_M_UT_762HS717");
        }
        private void BTN61_BILL_SEND_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_UT_762HS716"))
            {
                e.Successed = false;
                return;
            }
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
