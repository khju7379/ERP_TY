using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.10 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25A6A252 : 받을어음 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25A7G279 : 받을어음 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  E6CDCL : 거래처코드
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  E6NONR : 어음번호
    /// </summary>
    public partial class TYACEI001S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI001S()
        {
            InitializeComponent();
        }

        private void TYACEI001S_Load(object sender, System.EventArgs e)
        {           

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.SetStartingFocus(DTP01_STDATE);  
        }
        #endregion

        #region Description : 조회 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_25A6A252", this.ControlFactory, "01");              

            this.FPS91_TY_S_AC_25A7G279.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25B6N351", dt);
            this.DbConnector.Attach("TY_P_AC_25F8N481", dt);            
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }        
        #endregion

        #region Description : FPS91_TY_S_AC_25A7G279_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_25A7G279_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACEI001I(this.FPS91_TY_S_AC_25A7G279.GetValue("E6NONR").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACEI001I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_25A7G279.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_AC_25A7G279.GetDataSourceInclude(TSpread.TActionType.Remove, "E6NONR");

            DataTable dtChk = this.FPS91_TY_S_AC_25A7G279.GetDataSourceInclude(TSpread.TActionType.Remove, "E6NONR","E6IDBG", "E6JPNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dtChk.Rows.Count; i++)
                {
                    if (dtChk.Rows[i]["E6IDBG"].ToString() != "10")
                    {
                        this.ShowMessage("TY_M_AC_25G3B502");
                        e.Successed = false;
                        return;
                    }
                    if (dtChk.Rows[i]["E6JPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_GB_25F8V482");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion
    }
}
