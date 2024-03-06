using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 확정 CASH 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.01.08 11:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31885542 : EIS 확정 일일CASH 삭제
    ///  TY_P_AC_31886538 : EIS 확정 일일CASH 조회
    ///  TY_P_AC_3188A543 : EIS 확정 일일CASG 확정 해제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3188D544 : EIS 확정 일일CASH 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  EICHYMD : 년월일
    /// </summary>
    public partial class TYACPC013S : TYBase
    {
        public TYACPC013S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACPC013S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
            this.SetFocus(TXT01_EICHYMD);
        } 
        #endregion

        #region Description : 조회 처리
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31886538", this.TXT01_EICHYMD.GetValue().ToString().Trim());
            this.FPS91_TY_S_AC_3188D544.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Description : 삭제 처리
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            // 기존 확정 자료 삭제
            this.DbConnector.CommandClear(); // EMIHCASHF
            this.DbConnector.Attach("TY_P_AC_31885542", this.TXT01_EICHYMD.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            // 기존 확정 자료 처리 구분 세팅
            this.DbConnector.CommandClear(); //EMICASHF
            this.DbConnector.Attach("TY_P_AC_3188A543", this.TXT01_EICHYMD.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);
            this.SetFocus(TXT01_EICHYMD);
        } 
        #endregion


        #region Description : 삭제 처리시 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 삭제할 자료 체크 
            this.DbConnector.CommandClear(); // EMICASHF
            this.DbConnector.Attach("TY_P_AC_3189W554", this.TXT01_EICHYMD.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                this.SetFocus(this.TXT01_EICHYMD);
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            // 삭제할 자료 체크 
            this.DbConnector.CommandClear(); // EMICASHF
            this.DbConnector.Attach("TY_P_AC_31895546", this.TXT01_EICHYMD.GetValue().ToString());
            DataTable dt1 = this.DbConnector.ExecuteDataTable();
            if (dt1.Rows.Count == 0)
            {
                this.SetFocus(this.TXT01_EICHYMD);
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
        }
        #endregion

    }
}
