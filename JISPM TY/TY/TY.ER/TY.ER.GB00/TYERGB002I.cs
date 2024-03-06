using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls.SystemForm;
using TY.Service.Library;

namespace TY.ER.GB00
{
    /// <summary>
    /// 상단 툴바 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// </summary>
    public partial class TYERGB002I : Shoveling2010.SmartClient.SystemUtility.ControlBase
    {
        private string _selectedTopMenuNo;
        private System.Windows.Forms.ContextMenuStrip _TreeViewMenuStrip = null;
        private ToolStripMenuItem _TreeViewMenuStrip_Favor = null;
        private Label _Label_ProgramNo = null;

        private Control[] _tmpControls;


        /// <summary>
        /// 상단 툴바
        /// </summary>
        public TYERGB002I()
        {
            InitializeComponent();
       }

        private void TYERGB002I_Load(object sender, EventArgs e)
        {
            this.LBL01_USERINFO.Text = string.Format("{0} {1}", TYUserInfo.DeptName, TYUserInfo.UserName);

            //상단 메뉴 초기화
            ToolStripButton tmpToolStripButton;
            ToolStripSeparator tmpToolStripSeparator;
            DataRow[] topMenus = this.OwnerMDI.UCMM_MenuData.Select(string.Format("LEVEL_NO = '{0}'", this.OwnerMDI.UCMM_LeftMenuDepth - 1));

            foreach (DataRow dr in topMenus)
            {
                tmpToolStripButton = new ToolStripButton();
                tmpToolStripButton.AutoSize = false;
                //tmpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
                tmpToolStripButton.TextAlign = ContentAlignment.MiddleCenter;
                tmpToolStripButton.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);                
                tmpToolStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
                tmpToolStripButton.Margin = new System.Windows.Forms.Padding(0);
                tmpToolStripButton.Name = string.Format("TSB01_{0}", dr["MENU_NO"]);
                tmpToolStripButton.Size = new System.Drawing.Size(100, 32);
                tmpToolStripButton.Tag = dr["MENU_NO"];
                tmpToolStripButton.Text = Convert.ToString(dr["MENU_NAME"]);
                tmpToolStripButton.Click += new EventHandler(TopMenuToolStripButton_Click);
                tmpToolStripButton.MouseEnter += new EventHandler(tmpToolStripButton_MouseEnter);
                tmpToolStripButton.MouseLeave += new EventHandler(tmpToolStripButton_MouseLeave);
                //tmpToolStripButton.Image = global::TY.Service.Launcher.Properties.Resources.leftmenu_icon;
                //tmpToolStripButton.ImageAlign = ContentAlignment.MiddleLeft;
                this.TSP01_TOPMENU.Items.Add(tmpToolStripButton);

                this.TSP01_TOPMENU.Items.Add(this.GetSeparator());
            }

            tmpToolStripButton = new ToolStripButton();
            tmpToolStripButton.AutoSize = false;
            tmpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tmpToolStripButton.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            tmpToolStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            tmpToolStripButton.Margin = new System.Windows.Forms.Padding(0);
            tmpToolStripButton.Name = "TSB01_FAVOR";
            tmpToolStripButton.Size = new System.Drawing.Size(100, 32);
            tmpToolStripButton.Tag = "FAVOR";
            tmpToolStripButton.Text = "즐겨찾기";
            tmpToolStripButton.Click += new EventHandler(TopMenuToolStripButton_Click);
            tmpToolStripButton.MouseEnter += new EventHandler(tmpToolStripButton_MouseEnter);
            tmpToolStripButton.MouseLeave += new EventHandler(tmpToolStripButton_MouseLeave);
            this.TSP01_TOPMENU.Items.Add(tmpToolStripButton);

            this.TSP01_TOPMENU.Items.Add(this.GetSeparator());

            //탭메뉴 즐겨찾기 초기화
            tmpToolStripSeparator = new ToolStripSeparator();
            tmpToolStripSeparator.Size = new System.Drawing.Size(219, 6);
            this.OwnerMDI.UCMM_TabMenuStrip.Items.Add(tmpToolStripSeparator);

            ToolStripMenuItem _TabMenu_AddFavor = new ToolStripMenuItem("즐겨찾기 추가");
            _TabMenu_AddFavor.Name = "_TabMenu_AddFavor";
            _TabMenu_AddFavor.Click += new EventHandler(_TabMenu_AddFavor_Click);
            this.OwnerMDI.UCMM_TabMenuStrip.Items.Add(_TabMenu_AddFavor);

            //트리메뉴 즐겨찾기 초기화
            this._TreeViewMenuStrip = new ContextMenuStrip();
            this._TreeViewMenuStrip_Favor = new ToolStripMenuItem();
            this._TreeViewMenuStrip_Favor.Name = "_TreeViewMenuStrip_RemoveFavor";
            this._TreeViewMenuStrip_Favor.Click += new EventHandler(this._TreeViewMenuStrip_Favor_Click);
            this._TreeViewMenuStrip.Items.Add(this._TreeViewMenuStrip_Favor);
            this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TREE.ContextMenuStrip = this._TreeViewMenuStrip;

            //하단 프로그램 정보 초기화
            this._Label_ProgramNo  = new Label();
            this.OwnerMDI.UCMM_StatusBar.Controls.Add(this._Label_ProgramNo );
            this.OwnerMDI.UCMM_StatusBar.SuspendLayout();
            this._Label_ProgramNo.BringToFront();
            this._Label_ProgramNo.Size = new Size(300, 16);
            this._Label_ProgramNo.Location = new Point(this.OwnerMDI.Width - this._Label_ProgramNo.Width, 0);
            this._Label_ProgramNo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._Label_ProgramNo.BackColor = this.OwnerMDI.UCMM_StatusMessage.BackColor;
            this._Label_ProgramNo.ForeColor = this.OwnerMDI.UCMM_StatusMessage.ForeColor;
            this._Label_ProgramNo.Font = new Font("굴림", 9.0F, FontStyle.Bold);
            this._Label_ProgramNo.TextAlign = ContentAlignment.BottomRight;
            this.OwnerMDI.UCMM_StatusBar.ResumeLayout();
            this.OwnerMDI.UCMM_TabControls.SelectedIndexChanged += new EventHandler(UCMM_TabControls_SelectedIndexChanged);
            this.UCMM_TabControls_SelectedIndexChanged(null, null);
        }

        private void UCMM_TabControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            TTabPage seletedTab = this.OwnerMDI.UCMM_TabControls.SelectedTab as TTabPage;
            if (seletedTab != null)
                this._Label_ProgramNo.Text = string.Format("{0}:{1}", 
                    seletedTab.ProgramNo, 
                    (seletedTab.MenuName.LastIndexOf(" x") == seletedTab.MenuName.Length - 2 ? seletedTab.MenuName.Remove(seletedTab.MenuName.Length - 2) : seletedTab.MenuName));
        }

        #region 상단메뉴 관련

        private void tmpToolStripButton_MouseEnter(object sender, EventArgs e)
        {
            ToolStripButton tmpTopMenu = sender as ToolStripButton;
            if (tmpTopMenu != null)
                tmpTopMenu.ForeColor = (tmpTopMenu.ForeColor == Color.White ? Color.White : Color.FromArgb(161, 60, 84));   //A13C54
        }

        private void tmpToolStripButton_MouseLeave(object sender, EventArgs e)
        {
            foreach (ToolStripItem toolStripItem in this.TSP01_TOPMENU.Items)
                toolStripItem.ForeColor = (toolStripItem.ForeColor == Color.White ? Color.White : Color.Black);
        }

        private void TopMenuToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem toolStripItem in this.TSP01_TOPMENU.Items)
            {
                toolStripItem.BackColor = Color.Transparent;
                toolStripItem.ForeColor = Color.Black;
            }

            ToolStripButton tmpTopMenu = sender as ToolStripButton;
            if (tmpTopMenu != null)
            {
                this._selectedTopMenuNo = Convert.ToString(tmpTopMenu.Tag);
                tmpTopMenu.BackColor = Color.FromArgb(161, 60, 84);
                tmpTopMenu.ForeColor = Color.White;
                this.BindLeftMenu();

                this._TreeViewMenuStrip_Favor.Text =
                    this._selectedTopMenuNo == "FAVOR"
                    ? "즐겨찾기 제거"
                    : "즐겨찾기 추가";
            }
        }
        
        #endregion

        #region 좌측 메뉴 관련

        internal string SelectedTopMenuNo
        {
            get { return this._selectedTopMenuNo; }
        }

        internal void BindLeftMenu()
        {
            if (this._selectedTopMenuNo == "FAVOR")
            {
                DataTable tmpMenus = this.GetFavorMenuData(this.OwnerMDI.UCMM_MenuData);
                this.OwnerMDI.UCMM_CreateLeftUserMenu(tmpMenus);
                this.OwnerMDI.UCMM_CreateLeftUserMenu(tmpMenus);
                if (tmpMenus.Rows.Count == 0)
                {
                    this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TITLE.Text = "등록된 메뉴가 없습니다.";
                    this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TREE.Initialize();

                    this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TREE.Tag = null;
                    this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TREE.SetValue(new object[] { "등록된 메뉴가 없습니다.", "NO FAVOR ", new DataTable() });
                }
            }
            else
            {
                this.OwnerMDI.UCMM_LeftTOPMENU_NO = this._selectedTopMenuNo;
                this.OwnerMDI.UCMM_LeftTOPMENU_NO = this._selectedTopMenuNo;
                //this.OwnerMDI.UCMM_CreateLeftUserMenu(this.GetMenuData(this.OwnerMDI.UCMM_MenuData, Convert.ToString(tmpTopMenu.Tag)));
                //this.OwnerMDI.UCMM_CreateLeftUserMenu(this.GetMenuData(this.OwnerMDI.UCMM_MenuData, Convert.ToString(tmpTopMenu.Tag)));

                //try
                //{
                //    ((PictureBox)this.OwnerMDI.UCMM_LeftMenuInfo.MENU_BOTTM.Controls[0].Controls[0].Controls[1]).SizeMode = PictureBoxSizeMode.StretchImage;
                //}
                //catch { }
            }

            Control middleMenu ;
            Control[] tmpControls = this.GetChildControls(this.OwnerMDI.UCMM_LeftMenuInfo.MENU_BOTTM);

            //좌측메뉴정보 전역변수에 저장
            _tmpControls = tmpControls;

            for (int i = 0; i < tmpControls.Length; i++)
            {
                middleMenu = tmpControls[i];
                middleMenu.Click += new EventHandler(middleMenu_Click);
            }

            if (tmpControls.Length > 0)
            {
                this.middleMenu_Click(tmpControls[0], null);
                tmpControls[2].ForeColor = Color.White;
            }
        }

        private void middleMenu_Click(object sender, EventArgs e)
        {
            //2020.03.12 : 좌측메뉴 단위업무에 글자를 무조건 검정색으로 전부 바꾼다.          
            if (_tmpControls.Length > 0)
            {
                for (int i = 0; i < _tmpControls.Length; i++)
                {
                    _tmpControls[i].ForeColor = Color.Black;
                }
            }

            foreach (Control tmpControl in this.OwnerMDI.UCMM_LeftMenuInfo.MENU_BOTTM.Controls)
            {
                tmpControl.ForeColor = Color.Black;

                if (tmpControl.Controls.Count > 0)
                {
                    tmpControl.Controls[0].BackgroundImage = this.OwnerMDI.UCMM_LeftMenuInfo.MENU_STRIP.BackgroundImage;

                }
            }

            Control senderControl = sender as Control;
            Panel middleMenu = null;

            if (senderControl != null)
            {
                if (senderControl is Panel)
                {
                    if (senderControl.BackgroundImage == null && senderControl.Controls.Count > 0)
                    {
                        middleMenu = senderControl.Controls[0] as Panel;
                    }
                    else
                    {
                        middleMenu = sender as Panel;
                    }
                }
                else
                {
                    middleMenu = senderControl.Parent as Panel;
                    //선택한 메뉴만 글자를 흰색으로 바꾼다
                    senderControl.ForeColor = Color.White;
                }
            }

            middleMenu.BackgroundImage = global::TY.ER.GB00.Properties.Resources.leftmenu_on_bg_h31_;

            //Control c = sender as Control;
            //c.ForeColor = (c.ForeColor == Color.Black ? Color.White : Color.Black);
        }

        private Control[] GetChildControls(Control control)
        {
            List<Control> rtnValue = new List<Control>();

            foreach(Control child in control.Controls)
            {
                rtnValue.Add(child);
                rtnValue.AddRange(this.GetChildControls(child));
            }

            return rtnValue.ToArray();
        }

        private DataTable GetMenuData(DataTable menuData, string menuNo)
        {
            DataTable rtnValue = null;
            DataTable tmpTable = null;
            rtnValue = menuData.Clone();
            using (tmpTable = menuData.Clone())
            {
                foreach (DataRow dr in this.GetChildMenuDatas(menuData, menuNo))
                    tmpTable.Rows.Add(dr.ItemArray);

                foreach (DataRow dr in tmpTable.Select("", "LEVEL_NO ASC"))
                    rtnValue.Rows.Add(dr.ItemArray);
            }

            return rtnValue;
        }

        private DataRow[] GetChildMenuDatas(DataTable menuData, string menuNo)
        {
            List<DataRow> rtnValue = new List<DataRow>();

            foreach (DataRow dr in menuData.Select(string.Format("UP_MENU_NO = '{0}'", menuNo)))
            {
                rtnValue.Add(dr);
                foreach (DataRow childMenu in this.GetChildMenuDatas(menuData, Convert.ToString(dr["MENU_NO"])))
                    rtnValue.Add(childMenu);
            }

            return rtnValue.ToArray();
        }

        private DataTable GetFavorMenuData(DataTable menuData)
        {
            DataTable rtnValue = null;
            DataTable tmpTable = null;
            rtnValue = menuData.Clone();
            using (tmpTable = menuData.Clone())
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_GB_24A32588", TYUserInfo.EmpNo);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                DataRow parentMenu;
                DataRow[] favorMenus;
                int levelNo;

                foreach (DataRow dr in dt.Rows)
                {
                    favorMenus = menuData.Select(string.Format("MENU_NO = '{0}'", dr["MENU_NO"]));
                    if (favorMenus.Length > 0)
                    {
                        levelNo = int.TryParse(Convert.ToString(favorMenus[0]["LEVEL_NO"]), out levelNo) ? levelNo : -1;
                        if (levelNo >= 0)
                        {
                            tmpTable.Rows.Add(
                                levelNo + 1,
                                favorMenus[0]["MENU_NO"],
                                favorMenus[0]["MENU_NAME"],
                                favorMenus[0]["UP_MENU_NO"],
                                favorMenus[0]["PROGRAM_FULL_NAME"],
                                favorMenus[0]["DESCRIPTION"],
                                favorMenus[0]["OPTIONS"],
                                favorMenus[0]["CHECK_YN"]);

                            foreach (DataRow tmpParentMenu in this.GetParentMenuDatas(menuData, Convert.ToString(favorMenus[0]["UP_MENU_NO"])))
                            {
                                if (tmpTable.Select(string.Format("MENU_NO = '{0}'", tmpParentMenu["MENU_NO"])).Length == 0)
                                {

                                    parentMenu = tmpTable.NewRow();
                                    parentMenu.ItemArray = tmpParentMenu.ItemArray;
                                    levelNo = int.TryParse(Convert.ToString(tmpParentMenu["LEVEL_NO"]), out levelNo) ? levelNo : -1;
                                    parentMenu["LEVEL_NO"] = levelNo + 1;

                                    tmpTable.Rows.InsertAt(parentMenu, tmpTable.Rows.Count - 1);
                                }
                            }
                        }
                    }
                }

                foreach (DataRow dr in tmpTable.Select("", "LEVEL_NO ASC"))
                {
                    rtnValue.Rows.Add(dr.ItemArray);
                }
            }

            return rtnValue;
        }

        private DataRow[] GetParentMenuDatas(DataTable menuData, string upMenuNo)
        {
            List<DataRow> rtnValue = new List<DataRow>();

            DataRow[] tmpRows = menuData.Select(string.Format("MENU_NO = '{0}'", upMenuNo));
            if (tmpRows.Length > 0)
            {
                rtnValue.Insert(0, tmpRows[0]);
                if (Convert.ToString(tmpRows[0]["LEVEL_NO"]) != "0")
                    rtnValue.InsertRange(0, this.GetParentMenuDatas(menuData, Convert.ToString(tmpRows[0]["UP_MENU_NO"])));
            }

            return rtnValue.ToArray();
        }

        #endregion

        #region 즐겨찾기 관련

        private void _TreeViewMenuStrip_Favor_Click(object sender, EventArgs e)
        {
            if (this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TREE.SelectedNode == null ||
                this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TREE.SelectedNode.Nodes.Count > 0)
                return;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach(
                    (this._selectedTopMenuNo == "FAVOR" ? "TY_P_GB_24A2Y587" : "TY_P_GB_24A2W586"),
                    TYUserInfo.EmpNo,
                    this.OwnerMDI.UCMM_LeftMenuInfo.MENU_TREE.SelectedNodeName
                    );

            this.DbConnector.ExecuteNonQuery();

            if (this._selectedTopMenuNo == "FAVOR")
                this.BindLeftMenu();
            this.ShowMessage(this._selectedTopMenuNo == "FAVOR" ? "TY_M_GB_24C38606" : "TY_M_GB_24C38605");
        }

        private void _TabMenu_AddFavor_Click(object sender, EventArgs e)
        {
            string menuNo = ((TTabPage)this.OwnerMDI.UCMM_TabControls.TabPages[this.OwnerMDI.UCMM_TabControls.SelectedIndex]).MenuNo;
            if (menuNo != "FormWindowMain")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                    "TY_P_GB_24A2W586",
                    TYUserInfo.EmpNo,
                    menuNo
                    );
                this.DbConnector.ExecuteNonQuery();

                if (this._selectedTopMenuNo == "FAVOR")
                    this.BindLeftMenu();
                this.ShowMessage("TY_M_GB_24C38605");
            }
        }

        #endregion

        private void BTN62_HOM_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taeyoung.co.kr");
        }

        private void BTN62_INFO_Click(object sender, EventArgs e)
        {

        }

        private void BTN62_PWC_Click(object sender, EventArgs e)
        {
            TYERGB005P pwdchg = new TYERGB005P();
            pwdchg.ShowDialog();
        }

        private void BTN62_LOGOUT_Click(object sender, EventArgs e)
        {
            TYERGB004P logout = new TYERGB004P();
            logout.OwnerMDI = this.OwnerMDI;
            logout.ShowDialog();
        }

        private void BTN01_MENUSEARCH_Click(object sender, EventArgs e)
        {
            TYERGB006P menuSearchPopup = new TYERGB006P(this, this.TXT01_SEARCHWORD.Text.ToString(), this.OwnerMDI.UCMM_MenuData);
            if (this.OpenModalPopup(menuSearchPopup) != DialogResult.OK || string.IsNullOrEmpty(menuSearchPopup.SelectedMenuNo))
                return;

            this.OwnerMDI.Find_Program(menuSearchPopup.SelectedMenuNo);
        }

        private void TXT01_SEARCHWORD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Shift)
                return;

            this.BTN01_MENUSEARCH_Click(null, null);
        }

        private ToolStripItem GetSeparator()
        {
            ToolStripButton rtnValue = new ToolStripButton();
            rtnValue.AutoSize = false;
            rtnValue.Height = 32;
            rtnValue.Width = 2;
            rtnValue.Enabled = false;
            rtnValue.BackgroundImage = global::TY.ER.GB00.Properties.Resources.topmenu_line_h32_;
            return rtnValue;
        }

        private DialogResult OpenModalPopup(Form form)
        {
            DialogResult rtnValue = System.Windows.Forms.DialogResult.Cancel;
            Form tmpForm = this.FindForm();
            if (form is TYBase)
                ((TYBase)form).OwnerMDI = this.OwnerMDI;
            if (this.OwnerMDI != null && this.OwnerMDI.ParentForm != null)
                tmpForm = this.OwnerMDI.ParentForm;
            tmpForm.Opacity = 0.8;
            rtnValue = form.ShowDialog();
            tmpForm.Opacity = 1.0;
            return rtnValue;
        }

        
    }
}
