using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미승인전표 다운 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.18 09:19
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29I4Y176 : 미승인전표 다운
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29I4Z177 : 미승인전표다운
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    /// </summary>
    public partial class TYACZZ001S : TYBase
    {
        public TYACZZ001S()
        {
            InitializeComponent();
        }

        private void TYACZZ001S_Load(object sender, System.EventArgs e)
        {
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29I4Y176");
            this.FPS91_TY_S_AC_29I4Z177.SetValue(this.DbConnector.ExecuteDataTable());
            
            this.ShowMessage("TY_M_AC_25O8K620");

        }
    }
}
