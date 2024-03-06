using System;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.GB00
{
    /// <summary>
    /// 패스워드 변경 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012.04.13 09:04
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
    ///  TY_M_GB_24DAE625 : 패스워드를 변경하시겠습니까?
    ///  TY_M_GB_24DAE626 : 변경할 패스워드가 일치하지 않습니다.
    ///  TY_M_GB_24DAH628 : 패스워드가 변경되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CONFIRM : 확인
    ///  CHGPASS1 : 패스워드
    ///  CHGPASS2 : 패스워드확인
    /// </summary>
    public partial class TYERGB005P : TYBase
    {
        public TYERGB005P()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYERGB005P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_CONFIRM.ProcessCheck += new TButton.CheckHandler(BTN61_CONFIRM_ProcessCheck);
        }

        private void BTN61_CONFIRM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.TXT01_CHGPASS1.GetValue().Equals(this.TXT01_CHGPASS2.GetValue()))
            {
                this.ShowMessage("TY_M_GB_24DAE626");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_24DAE625"))
            {
                e.Successed = false;
                return;
            }
        }

        private void BTN61_CONFIRM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_24DAT630", this.TXT01_CHGPASS1.GetValue(), TYUserInfo.EmpNo);
            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_24DAH628");
            this.Close();
        }

        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
