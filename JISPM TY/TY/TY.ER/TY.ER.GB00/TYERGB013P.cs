using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;



namespace TY.ER.AC00
{
    /// <summary>
    /// 전자세금계산서 발행 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.04.18 14:18
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
    /// 
    ///  # 필드사전 정보 ####
    /// </summary>
    
    public partial class TYERGB013P : TYBase
    {
        private string fsSMSUrl;
        
        public TYERGB013P(string sSMSUrl)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsSMSUrl = sSMSUrl;
        }
        
        private void TYERGB013P_Load(object sender, System.EventArgs e)
        {
            webB1.Navigate(fsSMSUrl);            
        }

        private void TYERGB013P_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
