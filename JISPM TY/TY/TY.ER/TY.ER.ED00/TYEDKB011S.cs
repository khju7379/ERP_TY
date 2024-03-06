using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Diagnostics;

namespace TY.ER.ED00
{
    /// <summary>
    /// 세관MSRS 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.04.05 13:50
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
    ///  INQ : 조회
    /// </summary>
    public partial class TYEDKB011S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB011S()
        {
            InitializeComponent();
        }

        private void TYEDKB011S_Load(object sender, System.EventArgs e)
        {

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\WINMATE\BIN\LINKUP.EXE ", @"C:\WINMATE\MSRS.SCR");
            this.Close();
            return; 
        }
        #endregion
    }
}
