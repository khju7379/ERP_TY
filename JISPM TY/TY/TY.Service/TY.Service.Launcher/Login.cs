using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;

namespace TY.Service.Launcher
{
    public partial class Login : Form
    {
        private string _userIDRegisteryKey = "TY_ERP_ID";

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                string savedUserID = null;
                try
                {
                    savedUserID = RegisteryKey.GetValue(this._userIDRegisteryKey);
                }
                catch { savedUserID = null; }

                if (!string.IsNullOrEmpty(savedUserID))
                {
                    this.CHK01_SAVEID.Checked = true;
                    this.TXT01_USER_ID.SetValue(savedUserID);
                    this.TXT01_PASSWORD.Focus();
                }
                else
                    this.TXT01_USER_ID.Focus();

                try
                {
                    RegisteryKey.SetValue(this._userIDRegisteryKey, this.TXT01_USER_ID.GetValue().ToString());
                }
                catch
                {
                    this.CHK01_SAVEID.SetValue("N");
                    this.CHK01_SAVEID.SetReadOnly(true);
                }
            }
            catch (Exception ex)
            {
                LocalCapturer.ExceptionCatch(ex);
            }
        }

        private void BTN01_LOGIN_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TXT01_USER_ID.IsEmpty)
                {
                    this.TXT01_USER_ID.Focus();
                    MessageBox.Show("사용자 ID를 입력하세요.");
                    return;
                }

                if (this.TXT01_PASSWORD.IsEmpty)
                {
                    this.TXT01_PASSWORD.Focus();
                    MessageBox.Show("비밀번호를 입력하세요.");
                    return;
                }


                //--------------------신 그룹웨어 체크  시작--------------------------//


                string password = EncryptionManager.EncryptBase64(System.Configuration.ConfigurationManager.AppSettings.Get("PassKey"), this.TXT01_PASSWORD.GetValue().ToString());

                //string password = EncryptionManager.DecryptBase64(System.Configuration.ConfigurationManager.AppSettings.Get("PassKey"), "hVHh2zOJhcKApSi1CMHgYw==");
                //MessageBox.Show(password);                
                //return;

                DbConnector dbConnector = new DbConnector(CurrentSystem.VirtualProgram);
                dbConnector.Attach("TY_P_GB_CBOEU257", "TYI", this.TXT01_USER_ID.GetValue().ToString());
                DataTable ds = dbConnector.ExecuteDataTable();
                if (ds.Rows.Count > 0)
                {
                    if (password != ds.Rows[0]["PASSWORD"].ToString().Trim())
                    {
                        MessageBox.Show("그룹웨어 비밀번호가 틀립니다.");
                        this.TXT01_PASSWORD.SetValue("");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("아이디를 확인하세요.");
                    this.TXT01_USER_ID.SetValue("");
                    return;
                }

                //오라클 인증정보 조회
                password = "";
                dbConnector.Attach("TYQ0000003", this.TXT01_USER_ID.GetValue().ToString(), CurrentSystem.SystemNo);
                DataTable ods = dbConnector.ExecuteDataTable();
                if (ods.Rows.Count > 0)
                {
                    password = ods.Rows[0]["PASSWORD"].ToString().Trim();
                }

                //그룹웨어 인증이 되면 ERP 인증은 강제 인증한다.
                LogOnStatus status = Employer.LogOn(
                  this.TXT01_USER_ID.GetValue().ToString(),
                  password
                  );
                //--------------------신 그룹웨어 체크  종료--------------------------//

                //LogOnStatus status = Employer.LogOn(
                //    this.TXT01_USER_ID.GetValue().ToString(),
                //    this.TXT01_PASSWORD.GetValue().ToString()
                //    );

                switch (status)
                {
                    case LogOnStatus.LogOn:
                        TYUserInfo.Reset();
                        if (this.CHK01_SAVEID.Checked)
                        {
                            try
                            {
                                RegisteryKey.SetValue(this._userIDRegisteryKey, this.TXT01_USER_ID.GetValue().ToString());
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                RegisteryKey.Remove(this._userIDRegisteryKey);
                            }
                            catch { }
                        }
                        this.Close();
                        break;
                    case LogOnStatus.NotFoundID:
                        MessageBox.Show("아이디를 확인하세요.");
                        this.TXT01_USER_ID.SetValue("");
                        break;
                    case LogOnStatus.MisMatchPWD:
                        MessageBox.Show("비밀번호가 틀립니다.");
                        this.TXT01_PASSWORD.SetValue("");
                        break;
                    case LogOnStatus.Exception:
                        MessageBox.Show("시스템 오류가 발생하였습니다.");
                        break;
                }
            }
            catch (Exception ex)
            {
                LocalCapturer.ExceptionCatch(ex);
            }
        }

        private void Login_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("로그인 창을 닫으시겠습니까?", "경고", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void TXT01_USER_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.TXT01_PASSWORD.Focus();
            else if (e.KeyCode == Keys.Escape)
                this.Login_MouseDoubleClick(null, null);
        }

        private void TXT01_PASSWORD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.BTN01_LOGIN_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.Login_MouseDoubleClick(null, null);
        }

     

       
    }
}
