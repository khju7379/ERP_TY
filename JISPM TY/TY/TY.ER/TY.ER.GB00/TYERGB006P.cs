using System;
using System.Data;
using TY.Service.Library;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls.SystemForm;

namespace TY.ER.GB00
{
    public partial class TYERGB006P : TYBase
    {
        private DataTable _menuData = null;
        private string _searchWord = null;
        private TYERGB002I _owner = null;
        private string _selectedMenuNo = null;


        public TYERGB006P(TYERGB002I owner, string searchWord, DataTable menuData)
        {
            this._searchWord = searchWord;
            this._menuData = menuData.Copy();
            this._owner = owner;
            InitializeComponent();
        }

        private void TYERGB006P_Load(object sender, EventArgs e)
        {
            if (this._menuData == null)
            {
                this._selectedMenuNo = null;
                this.Close();
            }
            else
            {
                this.TXT01_SEARCHWORD.SetValue(this._searchWord);
                this.BTN61_INQ_Click(null, null);
            }

            this.FPS91_TY_S_GB_2572N059.ContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem _TabMenu_AddFavor = new ToolStripMenuItem("즐겨찾기 추가");
            _TabMenu_AddFavor.Name = "_TabMenu_AddFavor";
            _TabMenu_AddFavor.Click += new EventHandler(_TabMenu_AddFavor_Click);
            this.FPS91_TY_S_GB_2572N059.ContextMenuStrip.Items.Add(_TabMenu_AddFavor);
        }

        public string SelectedMenuNo
        {
            get { return this._selectedMenuNo; }
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MENU_NO", typeof(string));
            dt.Columns.Add("PROGRAM_NO", typeof(string));
            dt.Columns.Add("MENU_NAME", typeof(string));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            string program_No, menu_Name, description;
            foreach (DataRow dr in this._menuData.Select(string.Format("CHECK_YN = 'Y'")))
            {
                program_No = Convert.ToString(dr["PROGRAM_FULL_NAME"]);
                program_No = program_No.Substring(program_No.LastIndexOf('.') == program_No.Length - 1 ? 0 : program_No.LastIndexOf('.') + 1);
                menu_Name = Convert.ToString(dr["MENU_NAME"]);
                description = Convert.ToString(dr["DESCRIPTION"]);

                if (program_No.IndexOf(this.TXT01_SEARCHWORD.GetValue().ToString()) < 0 &&
                    menu_Name.IndexOf(this.TXT01_SEARCHWORD.GetValue().ToString()) < 0 &&
                    description.IndexOf(this.TXT01_SEARCHWORD.GetValue().ToString()) < 0)
                    continue;

                dt.Rows.Add(Convert.ToString(dr["MENU_NO"]), program_No, menu_Name, description);
            }

            this.FPS91_TY_S_GB_2572N059.SetValue(dt);
        }

        private void BTN61_CLR_Click(object sender, EventArgs e)
        {
            this.TXT01_SEARCHWORD.Initialize();
            this.BTN61_INQ_Click(null, null);
        }

        private void FPS91_TY_S_GB_2572N059_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this._selectedMenuNo = this.FPS91_TY_S_GB_2572N059.GetValue(e.Row, "MENU_NO").ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void _TabMenu_AddFavor_Click(object sender, EventArgs e)
        {
            string menuNo = this.FPS91_TY_S_GB_2572N059.GetValue("MENU_NO").ToString();
            if (menuNo != "FormWindowMain")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                    "TY_P_GB_24A2W586",
                    TYUserInfo.EmpNo,
                    menuNo
                    );
                this.DbConnector.ExecuteNonQuery();

                if(this._owner != null && this._owner.SelectedTopMenuNo == "FAVOR")
                    this._owner.BindLeftMenu();
                this.ShowMessage("TY_M_GB_24C38605");
            }
        }
    }
}
