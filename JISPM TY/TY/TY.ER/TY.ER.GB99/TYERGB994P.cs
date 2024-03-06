using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Windows.Forms;

namespace TY.ER.GB99
{
    public partial class TYERGB994P : TYBase, IPopupHelper
    {
        private bool _Isloaded = false;

        private string fsB7CDAC;
        private string fsA6CDBK;
        private string fsA6NOAC;

        public string fsSJJPNO;
        public string fsB7AMAT;
        public string fsB7CRDT;
        public string fsB7DTAC;

        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        public TYERGB994P()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }
        
        private void TYERGB994P_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

                
            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if(value != null&&value.Length >2)
                {
                    this.fsB7CDAC = value[0];
                    this.fsA6CDBK = value[1];
                    this.fsA6NOAC = value[2];
                }
            }
        

            this.CBH01_A6CDBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_A6CDBK_CodeBoxDataBinded);

            this.CBH01_B7CDAC.SetValue(this.fsB7CDAC);
            this.CBH01_A6CDBK.SetValue(this.fsA6CDBK);
            this.CBH01_A6NOAC.SetValue(this.fsA6NOAC);

            this.CBH01_B7CDAC.SetReadOnly(true);
            this.CBH01_A6CDBK.SetReadOnly(true);
            this.CBH01_A6NOAC.SetReadOnly(true);

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C92938", this.fsA6CDBK, this.fsA6NOAC, this.fsB7CDAC);
            this.FPS91_TY_S_AC_29C96939.SetValue(this.DbConnector.ExecuteDataTable());
        }

        private void CBH01_A6CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_A6CDBK.GetValue().ToString();
            this.CBH01_A6NOAC.DummyValue = groupCode;
            this.CBH01_A6NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_A6NOAC.Initialize();
        }

        private void FPS91_TY_S_AC_29C96939_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }

        private void FPS91_TY_S_AC_29C96939_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //int row = (e == null ? 0 : FPS91_TY_S_AC_29C96939.ActiveSheet.ActiveRowIndex);

                //fsSJJPNO = this.FPS91_TY_S_AC_29C96939.GetValue(row, "SJJPNO").ToString();
                //fsB7AMAT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7AMAT").ToString();
                //fsB7CRDT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B2VLMI1").ToString();
                //fsB7DTAC = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7DTAC").ToString();

                //this.Close();
                this.ConfirmEventInterface();
            }
        }

        ////필수..시작
        public void ConfirmEventInterface()
        {
            int row = FPS91_TY_S_AC_29C96939.ActiveSheet.ActiveRowIndex;

            fsSJJPNO = this.FPS91_TY_S_AC_29C96939.GetValue(row, "SJJPNO").ToString();
            fsB7AMAT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7AMAT").ToString();
            fsB7CRDT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B2VLMI1").ToString();
            fsB7DTAC = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7DTAC").ToString();

            string code = fsSJJPNO;
            string name = fsB7AMAT;

            this._SelectedDataRow = this.FPS91_TY_S_AC_29C96939.GetDataRow(row);

            if (this._TComboHelper != null)
            {
                this._TComboHelper.SetValue(code, name);
                this._TComboHelper.BindedDataRow = _SelectedDataRow;
            }

            this.Close();
        }

        public DataTable GetDataSource(params string[] parameters)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C92938", this.fsA6CDBK, this.fsA6NOAC, this.fsB7CDAC);
            return this.DbConnector.ExecuteDataTable();
        }

        
        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.CBH01_A6CDBK.Initialize();
            this.CBH01_A6NOAC.Initialize();
            this.CBH01_B7CDAC.Initialize();
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYERGB994P_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }
        //필수...끝
    }
}
