using TY.Service.Library;
using Shoveling2010.SmartClient.SystemUtility;
using System.Windows.Forms;
using TY.Service.Library.Controls;

namespace TY.ER.GB00
{
    public partial class TYERGB008S : ControlBase
    {
        TYData DAT02_EMP_NO;

        public TYERGB008S()
        {
            InitializeComponent();

            //this.DAT02_EMP_NO = new TYData("DAT02_EMP_NO", TYUserInfo.EmpNo);

            this.DAT02_EMP_NO = new TYData("DAT02_EMP_NO", "");

            this.FPS91_TY_S_GB_25T4D693.AutoResizeRate = false;
            this.FPS92_TY_S_GB_25UBB717.AutoResizeRate = false;
        }

        private void TYERGB008S_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT02_EMP_NO);

            this.BTN61_INQ_Click(null, null);
            this.BTN62_INQ_Click(null, null);
        }

        private void BTN61_INQ_Click(object sender, System.EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_25T3Z692", this.ControlFactory, "01");
            this.FPS91_TY_S_GB_25T4D693.SetValue(this.DbConnector.ExecuteDataTable());
        }

        private void FPS91_TY_S_GB_25T4D693_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string bbs_No = this.FPS91_TY_S_GB_25T4D693.GetValue(e.Row, "BBS_NO").ToString();

            if (string.IsNullOrEmpty(bbs_No))
                return;

            this.OpenModalPopup(new TYERGB009P(bbs_No));
            this.BTN61_INQ_Click(null, null);
        }

        private void BTN62_INQ_Click(object sender, System.EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_25UB9715", this.ControlFactory, "02");
            this.FPS92_TY_S_GB_25UBB717.SetValue(this.DbConnector.ExecuteDataTable());
        }

        private void FPS92_TY_S_GB_25UBB717_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string claim_No = this.FPS92_TY_S_GB_25UBB717.GetValue(e.Row, "CLAIM_NO").ToString();

            if (string.IsNullOrEmpty(claim_No))
                return;

            this.OpenModalPopup(new TYERGB010P(claim_No));
        }

        private DialogResult OpenModalPopup(Form form)
        {
            DialogResult rtnValue = System.Windows.Forms.DialogResult.Cancel;
            Form tmpForm = this.ParentForm;
            if (tmpForm != null && tmpForm.ParentForm != null)
                tmpForm = tmpForm.ParentForm;
            tmpForm.Opacity = 0.8;
            rtnValue = form.ShowDialog();
            tmpForm.Opacity = 1.0;
            return rtnValue;
        }
    }
}
