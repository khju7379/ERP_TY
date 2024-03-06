using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 계정 과목 코드 등록 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012.03.19 15:43
    /// </summary>
    public partial class TYERAC001S : TYBase
    {
        public TYERAC001S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYERAC001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_23N3M888", this.ControlFactory, "01");
            this.FPS92_TY_S_AC_23N3Q894.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Description : 신규
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYERAC001I(string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS92_TY_S_AC_23N3Q894.GetDataSourceInclude(TSpread.TActionType.Remove, "A1CDAC");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        } 
        #endregion

        #region Description : 삭제
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3K882", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        } 
        #endregion

        #region Description : 그리드 선택 처리
        private void FPS92_TY_S_AC_23N3Q894_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYERAC001I(this.FPS92_TY_S_AC_23N3Q894.GetValue("A1CDAC").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        } 
        #endregion
    }
}
