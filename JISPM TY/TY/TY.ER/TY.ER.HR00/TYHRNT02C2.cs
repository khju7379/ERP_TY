using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.IO;


namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 첨부파일 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.28 08:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77S8Z291 : 연말정산 첨부파일 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_77S8Z293 : 연말정산 첨부파일 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYHRNT02C2 : TYBase
    {
        private string fsYACOMPANY;

        #region  Description : 폼 로드 이벤트
        public TYHRNT02C2(string sYACOMPANY)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsYACOMPANY = sYACOMPANY;
        }

        private void TYHRNT02C2_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));
            
        }
        #endregion      

        #region  Description : 연말정산 대상자 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sPreYear = Convert.ToString(Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) - 1);

            this.DbConnector.CommandClear();
            //연말정산 대상자 
            this.DbConnector.Attach("TY_P_HR_7B9BF975", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue(), TYUserInfo.EmpNo, CBO01_GOKCR.GetValue().ToString());
            if (CBO01_GOKCR.GetValue().ToString() == "1" || CBO01_GOKCR.GetValue().ToString() == "3")
            {
                //부양가족 복사
                this.DbConnector.Attach("TY_P_HR_7CC8H224", fsYACOMPANY, sPreYear, CBH01_KBSABUN.GetValue(), TYUserInfo.EmpNo, TYUserInfo.SecureKey, "Y");
            }
            else
            {
                //부양가족 복사(본인만 복사)
                this.DbConnector.Attach("TY_P_HR_82NBD627", fsYACOMPANY, sPreYear, CBH01_KBSABUN.GetValue(), TYUserInfo.EmpNo, TYUserInfo.SecureKey, "Y");
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CBO01_GOKCR.GetValue().ToString() == "2" && CBH01_KBSABUN.GetValue().ToString() == "" )
            {
                this.ShowCustomMessage("중도퇴사는 반드시 사번을 입력해야합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77LD4260", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue(), CBO01_GOKCR.GetValue().ToString(), "", "", "");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("이미 대상자가 생성되어 있습니다! 삭제 후 다시 생성하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            } 

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

       

       
    }
}
