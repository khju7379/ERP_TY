using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 구ERP 접속화면 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.01.09 11:50
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
    public partial class TYACZZ001P : TYBase
    {
        public TYACZZ001P()
        {
            InitializeComponent();
        }

        private void TYACZZ001P_Load(object sender, System.EventArgs e)
        {
           //string sSabun = TYUserInfo.EmpNo;
            

            //webB1.Navigate(sUrl);
            
            //webB1.Navigate(sUrl+sSabun, true);
        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            string sSabun = TYUserInfo.EmpNo;

            string sUrl = "http://biz.taeyoung.co.kr/Taeyoung_Main/LoginERP.aspx?SABUN=";

            //webB1.Navigate(sUrl);

            webB1.Navigate(sUrl + sSabun, true);
        }
    }
}
