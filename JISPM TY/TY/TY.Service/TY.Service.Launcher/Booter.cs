using System;
using System.Deployment.Application;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility;
using Shoveling2010.SmartClient.SystemUtility.Component;
using Shoveling2010.SmartClient.SystemUtility.Controls.SystemForm;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.Data;
using System.Configuration;
using Microsoft.Win32;
using System.Security.AccessControl;
using TY.Service.Library;

namespace TY.Service.Launcher
{
    static class Booter
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                ///////////////////////
                try
                {
                    TYCasInstaller.Setting();
                }
                catch { }
                ///////////////////////

                ThreadStart start = new ThreadStart((new Program()).Starting);
                Thread thread = new Thread(start);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("프로젝트 환경설정 파일이 없거나 혹은 잘못되었습니다.\r\n\r\n오류내용:{0}", e.Message));
            }
        }
    }

    #region class TYFormWindowStart - 태영 전용 FormWindowStart
    ///// <summary>
    ///// 태영 전용 FormWindowStart
    ///// </summary>
    //internal class TYFormWindowStart : FormWindowStart
    //{
    //    private const int AUTO_LOGOFF_MINUTE = 1;

    //    private int _autoLogOffCount;
    //    private bool _isActivate;
    //    private bool _isRunning;
    //    private delegate void AutoLogOffChecker();
    //    private AutoLogOffChecker _autoLogOffChecker;
    //    private Thread _autoLogOffThread;

    //    public TYFormWindowStart()
    //        : base()
    //    {
    //        this._autoLogOffCount = 0;
    //        this._isActivate = true;
    //        this._isRunning = true;

    //        this.Activated += new EventHandler(TYFormWindowStart_Activated);
    //        this.Deactivate += new EventHandler(TYFormWindowStart_Deactivate);

    //        this._autoLogOffChecker = new AutoLogOffChecker(this.AutoLogOffCheck);
    //        this._autoLogOffThread = new Thread(new ThreadStart(this.AutoLogOff));
    //        this._autoLogOffThread.Start();
    //    }

    //    private void TYFormWindowStart_Activated(object sender, EventArgs e)
    //    {
    //        this._isActivate = true;
    //        this._autoLogOffCount = 0;
    //    }

    //    private void TYFormWindowStart_Deactivate(object sender, EventArgs e)
    //    {
    //        this._isActivate = false;
    //    }

    //    private void AutoLogOff()
    //    {
    //        for (; ; )
    //        {
    //            Thread.Sleep(1000);

    //            if (!this._isRunning || this.Disposing || this.IsDisposed)
    //                break;

    //            if (!this._isActivate)
    //                this.Invoke(this._autoLogOffChecker);
    //        }
    //    }

    //    private void AutoLogOffCheck()
    //    {
    //        this._autoLogOffCount++;
    //        if (this._autoLogOffCount < AUTO_LOGOFF_MINUTE * 60)
    //            return;
    //        this._isRunning = false;
    //        this._autoLogOffThread.Abort();
    //        this.Close();
    //        ThreadStart start = new ThreadStart(this.ReStarting);
    //        Thread thread = new Thread(start);
    //        thread.SetApartmentState(ApartmentState.STA);
    //        thread.Start();
    //    }

    //    private void ReStarting()
    //    {
    //        try
    //        {
    //            Employer.IsLogon = LogOnStatus.LogOut;

    //            ThreadStart start = new ThreadStart((new TY.Service.Launcher.Program()).Starting);
    //            Thread thread = new Thread(start);
    //            thread.SetApartmentState(ApartmentState.STA);
    //            thread.Start();
    //        }
    //        catch (Exception e)
    //        {
    //            LocalCapturer.ExceptionCatch(e);
    //        }
    //    }
    //}  
    #endregion

    public class Program
    {
        private Panel _MDIBody = null;
        private int _closeButtonWidth = 14;
        private string _userIDRegisteryKey = "TY_ERP_ID";

        public void Starting()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                UnhandledException unhandled = new UnhandledException();
                unhandled.Run();

                CurrentSystem.Run("SYS201103C");

                if (ApplicationDeployment.IsNetworkDeployed &&
                    ApplicationDeployment.CurrentDeployment.ActivationUri != null &&
                    !string.IsNullOrEmpty(ApplicationDeployment.CurrentDeployment.ActivationUri.Query))
                {
                    System.Collections.Specialized.NameValueCollection queryString = HttpUtility.ParseQueryString(ApplicationDeployment.CurrentDeployment.ActivationUri.Query);
                    if (queryString != null && !string.IsNullOrEmpty(queryString["ID"]))
                    {
                        string systemNo = CurrentSystem.SystemNo;
                        string virtualprogram = CurrentSystem.VirtualProgram;
                        string logonprocedure = CurrentSystem.LogonProcedure;
                        string id = HttpUtility.UrlDecode(queryString["ID"]);

                        DbConnector dbConnector = new DbConnector(virtualprogram);
                        dbConnector.Attach(logonprocedure, id, systemNo);

                        DataTable logInfo = dbConnector.ExecuteDataTable();

                        if (logInfo.Rows.Count > 0)
                        {
                            DataRow row = logInfo.Rows[0];
                            Employer.LogOn(id, Convert.ToString(row["PASSWORD"]));
                            if (Employer.IsLogon == LogOnStatus.LogOn)
                            {
                                try
                                {
                                    RegisteryKey.SetValue(this._userIDRegisteryKey, id);
                                }
                                catch { }
                            }
                        }
                    }
                }

                if (Employer.IsLogon != LogOnStatus.LogOn)
                {
                    Login login = new Login();
                    login.StartPosition = FormStartPosition.CenterScreen;
                    login.ShowDialog();
                }

                if (Employer.IsLogon == LogOnStatus.LogOn)
                {
                    FormWindowStart start = new FormWindowStart();
                    //TYFormWindowStart start = new TYFormWindowStart();
                    start.StartPosition = FormStartPosition.CenterScreen;
                    start.Icon = global::TY.Service.Launcher.Properties.Resources.태영ERP;

                    start.UCMM_MDIBody.BorderStyle = BorderStyle.None;
                    start.UCMM_TabMenuStrip.BackColor = Color.FromArgb(231, 234, 237);
                    ToolStripItem[] ucmm_TabMenuStripItems = start.UCMM_TabMenuStrip.Items.Find("_TabMenu_TBugReport", false);
                    if (ucmm_TabMenuStripItems.Length > 0)
                    {
                        ucmm_TabMenuStripItems[0].Text = "요구사항 요청";
                    }
                    start.UCMM_ProgressBar.BackColor = Color.FromArgb(42, 59, 87);

                    //Font tyCommonFont = new Font("맑은 고딕", 9.0F);
                    Font tyCommonFontBold = new Font("굴림", 9.0F, FontStyle.Bold);
                    start.UCMM_Menu.Visible = false;
                    start.UCMM_ToolBarStrip.Height = 65;
                    start.UCMM_TabControlsBackGroundPanel.BackgroundImage = global::TY.Service.Launcher.Properties.Resources.tab_bg_h20_;
                    start.UCMM_TabControlsBackGroundPanel.BackColor = this.GetColor("F4F4F6");    //임시                    
                    start.UCMM_StatusBar.BackColor = this.GetColor("1B1B1B");
                    start.UCMM_StatusMessage.BackColor = this.GetColor("1B1B1B");
                    start.UCMM_StatusMessage.ForeColor = Color.FromArgb(255, 255, 255);
                    start.UCMM_SplitContainer.BackColor = this.GetColor("F3F4F8");
                    start.UCMM_SplitContainer.Panel2MinSize = 400;
                    start.UCMM_MDIBody.BackColor = this.GetColor("FCFCFC");

                    //-----왼쪽 메뉴-----
                    start.UCMM_LeftMenuVisible = true;
                    start.UCMM_LeftMenuDepth = 1;
                    //start.UCMM_TabControls.Font = tyCommonFont;
                    //start.UCMM_TabControlsBackGroundPanel.BackgroundImage = 
                    start.UCMM_LeftMenuInfo.MENU.BackColor = this.GetColor("F3F4F8");
                    start.UCMM_LeftMenuInfo.MENU_OUTER.BackColor = this.GetColor("C0C0C0");
                    start.UCMM_LeftMenuInfo.MENU_INNER.BackColor = this.GetColor("C0C0C0");
                    start.UCMM_LeftMenuInfo.MENU_TOP.BackgroundImage = global::TY.Service.Launcher.Properties.Resources.leftmenu_title_bg_h19_;
                    start.UCMM_LeftMenuInfo.MENU_TOP.Height = 18;
                    start.UCMM_LeftMenuInfo.MENU_TITLE.Height = 18;
                    start.UCMM_LeftMenuInfo.MENU_TITLE.Font = tyCommonFontBold;
                    start.UCMM_LeftMenuInfo.MENU_BTN.Location = new Point(start.UCMM_LeftMenuInfo.MENU_BTN.Location.X, 4);
                    start.UCMM_LeftMenuInfo.MENU_BTN.Image = global::TY.Service.Launcher.Properties.Resources.leftmenu_btn_close;
                    start.UCMM_LeftMenuInfo.MENU_BTN.BackColor = Color.Transparent;
                    start.UCMM_LeftMenuOpen = global::TY.Service.Launcher.Properties.Resources.leftmenu_btn_open;
                    start.UCMM_LeftMenuOpenHover = global::TY.Service.Launcher.Properties.Resources.leftmenu_btn_open;
                    start.UCMM_LeftMenuClose = global::TY.Service.Launcher.Properties.Resources.leftmenu_btn_close;
                    start.UCMM_LeftMenuCloseHover = global::TY.Service.Launcher.Properties.Resources.leftmenu_btn_close;
                    start.UCMM_LeftMenuInfo.DOCKING.BackColor = this.GetColor("F9FAFE");
                    start.UCMM_LeftMenuInfo.DOCKING.Panel1.BackColor = this.GetColor("F9FAFE");
                    start.UCMM_LeftMenuInfo.DOCKING.Panel1.Padding = new Padding(0);
                    start.UCMM_LeftMenuInfo.MENU_BODY.Margin = new Padding(0);
                    start.UCMM_LeftMenuInfo.MENU_BODY.Padding = new Padding(0);
                    start.UCMM_LeftMenuInfo.MENU_TREE.BackColor = this.GetColor("F8FBFE");
                    start.UCMM_LeftMenuInfo.MENU_TREE.ShowPlusMinus = true;
                    start.UCMM_LeftMenuInfo.LeftMenuTreeRootVisible = false;
                    ImageList treeMenuImageList = new ImageList();
                    treeMenuImageList.Images.Add(global::TY.Service.Launcher.Properties.Resources.leftmenu_icon_01);
                    treeMenuImageList.Images.Add(global::TY.Service.Launcher.Properties.Resources.leftmenu_icon_01);
                    treeMenuImageList.Images.Add(global::TY.Service.Launcher.Properties.Resources.leftmenu_icon_02);
                    start.UCMM_LeftMenuInfo.MENU_TREE.ImageList = treeMenuImageList;
                    //start.UCMM_LeftMenuInfo.MENU_TREE.Font = tyCommonFont;
                    //start.UCMM_LeftMenuInfo.DOCKING.Panel2.Font = tyCommonFont;
                    start.UCMM_LeftMenuInfo.MENU_BOTTM.Font = tyCommonFontBold;
                    start.UCMM_LeftMenuInfo.MENU_BOTTM.BackgroundImage = global::TY.Service.Launcher.Properties.Resources.leftmenu_off_bg_h31_;
                    start.UCMM_LeftMenuInfo.MENU_TOOLBAR.BackColor = this.GetColor("D8D8D8");
                    start.UCMM_LeftMenuInfo.MENU_TOOLBAR.BackgroundImage = null;
                    start.UCMM_LeftMenuInfo.MENU_TOOLBAR.Padding = new Padding(1);
                    start.UCMM_LeftMenuInfo.MENU_STRIP.BackgroundImage = global::TY.Service.Launcher.Properties.Resources.leftmenu_off_bg_h31_;
                    ImageList leftMenuImageList = new ImageList();
                    Image leftMenuImage;
                    for (int i = 0; i < 100; i++)
                    {
                        leftMenuImage = global::TY.Service.Launcher.Properties.Resources.leftmenu_icon;
                        leftMenuImageList.Images.Add(leftMenuImage);
                    }
                    leftMenuImageList.ImageSize = new Size(24, 24);
                    start.UCMM_LeftMenuImageList = leftMenuImageList;

                    start.UCMM_TabControls.ControlAdded += new ControlEventHandler(UCMM_TabControls_ControlAdded);
                    start.UCMM_TabControls.MouseMove += new MouseEventHandler(UCMM_TabControls_MouseMove);
                    start.UCMM_TabControls.MouseClick += new MouseEventHandler(UCMM_TabControls_MouseClick);
                    this._MDIBody = start.UCMM_MDIBody;
                    Application.Run(start);
                }
            }
            catch (Exception e)
            {
                LocalCapturer.ExceptionCatch(e);
            }
        }

        private void UCMM_TabControls_MouseMove(object sender, MouseEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl == null || tabControl.TabPages.Count < 2)
                return;

            Point p = e.Location;
            for (int i = 1; i < tabControl.TabPages.Count; i++)
            {
                Rectangle r = tabControl.GetTabRect(i);
                r.Offset(tabControl.GetTabRect(i).Width - this._closeButtonWidth, 2);
                r.Width = this._closeButtonWidth;
                r.Height = tabControl.Height - 4;

                if (r.Contains(p))
                {
                    Cursor.Current = Cursors.Hand;
                }
            }
        }

        private void UCMM_TabControls_MouseClick(object sender, MouseEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl == null || e.Button != MouseButtons.Left || tabControl.TabPages.Count < 2)
                return;

            Point p = e.Location;
            for (int i = 1; i < tabControl.TabPages.Count; i++)
            {
                Rectangle r = tabControl.GetTabRect(i);
                r.Offset(tabControl.GetTabRect(i).Width - this._closeButtonWidth, 2);
                r.Width = this._closeButtonWidth;
                r.Height = tabControl.Height - 4;

                if (!r.Contains(p))
                    continue;

                TTabPage tabPage = (TTabPage)tabControl.TabPages[i];
                for (int j = this._MDIBody.Controls.Count - 1; j >= 0; j--)
                {
                    string findno = (this._MDIBody.Controls[i] is FormBase) ?
                        ((FormBase)this._MDIBody.Controls[i]).ProgramNo :
                        ((InternetViewer)this._MDIBody.Controls[i]).ProgramNo;

                    if (!findno.Equals(tabPage.ProgramNo))
                        continue;

                    Form form = (Form)this._MDIBody.Controls[i];
                    form.Close();
                    this._MDIBody.Controls.Remove(form);
                    break;
                }
                tabControl.TabPages.Remove(tabPage);

                tabControl.SelectedIndex = 0;
                for (int j = 0; j < this._MDIBody.Controls.Count; j++)
                {
                    string findno = (this._MDIBody.Controls[j] is FormBase) ?
                        ((FormBase)this._MDIBody.Controls[j]).ProgramNo :
                        ((InternetViewer)this._MDIBody.Controls[j]).ProgramNo;

                    Form form = (Form)this._MDIBody.Controls[j];
                    if (findno.Equals(((TTabPage)tabControl.TabPages[0]).ProgramNo))
                    {
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;
                        form.Show();
                    }
                    else
                        form.Hide();
                }

                break;
            }
        }

        private void UCMM_TabControls_ControlAdded(object sender, ControlEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TTabPage tabPage = e.Control as TTabPage;

            if (tabControl == null || tabPage == null)
                return;

            if (tabControl.TabPages.IndexOf(tabPage) == 0)
                return;

            tabPage.Text += " x";
        }

        private System.Drawing.Color GetColor(string RRGGBB)
        {
            try
            {
                string RR = "FF";
                string GG = "FF";
                string BB = "FF";

                if (RRGGBB.Length == 6)
                {
                    RR = RRGGBB.Substring(0, 2);
                    GG = RRGGBB.Substring(2, 2);
                    BB = RRGGBB.Substring(4, 2);
                }

                int R = Int32.Parse(RR, System.Globalization.NumberStyles.HexNumber);
                int G = Int32.Parse(GG, System.Globalization.NumberStyles.HexNumber);
                int B = Int32.Parse(BB, System.Globalization.NumberStyles.HexNumber);

                return System.Drawing.Color.FromArgb(R, G, B);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
    internal static class TYCasInstaller
    {
        private const string REGISTERY_HOME = "SOFTWARE\\Wow6432Node\\Shoveling2010\\ClientSetting\\";
        private const string REGISTERY_WEBL = "Client_WebServiceUrl";

        public static void Setting()
        {
            //string codeGroupName = ConfigurationManager.AppSettings.Get("CodeGroupName");
            //string codeGroupNote = ConfigurationManager.AppSettings.Get("CodeGroupNote");
            string webServiceUrl = ConfigurationManager.AppSettings.Get("WebServiceUrl");

            RegistryKey home = Registry.CurrentUser.OpenSubKey(REGISTERY_HOME,
                RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);

            if (home == null)
            {
                //Registry.LocalMachine.CreateSubKey(REGISTERY_HOME);
                //home = Registry.LocalMachine.OpenSubKey(REGISTERY_HOME,
                //    RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);
                home = Registry.CurrentUser.CreateSubKey(REGISTERY_HOME, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }

            //object value = Registry.LocalMachine.OpenSubKey(REGISTERY_HOME).GetValue(REGISTERY_WEBL);
            //object value =null;
            //foreach(string valueName in home.GetValueNames())
            //{
            //    if (valueName == REGISTERY_HOME)
            //    {
            //        value = home.GetValue(REGISTERY_WEBL);
            //    }
            //}
            object value = home.GetValue(REGISTERY_WEBL);

            if (value == null)
                home.SetValue(REGISTERY_WEBL, webServiceUrl, RegistryValueKind.String);
        }
    }
}
