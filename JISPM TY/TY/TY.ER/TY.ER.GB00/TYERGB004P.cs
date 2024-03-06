using System;
using System.Reflection;
using System.Text;
using System.Threading;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;

namespace TY.ER.GB00
{
    /// <summary>
    /// 로그아웃 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// </summary>
    public partial class TYERGB004P : TYBase
    {
        /// <summary>
        /// 로그아웃 팝업
        /// </summary>
        public TYERGB004P()
        {
            InitializeComponent();
        }

        private void TYERGB004P_Load(object sender, System.EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("시스템을 종료합니다.\r\n\r\n");
            builder.Append("경고: 시스템 종료 시 모든 프로그램이 종료됩니다.\r\n");
            builder.Append("미 완료된 업무는 없는지 다시한번 확인하시기 바랍니다.");

            this.RTB01_TEXT.SetValue(builder.ToString());
            this.RTB01_TEXT.SetReadOnly(true);
        }

        private void BTN61_LOGOUT_Click(object sender, System.EventArgs e)
        {
            this.SystemClose(true);
        }

        private void BTN61_SHUTDOWN_Click(object sender, System.EventArgs e)
        {
            this.SystemClose(false);
        }

        private void BTN61_CANCEL_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void SystemClose(bool restart)
        {
            this.Session.Clear();

            if (this.OwnerMDI.CloseOK != null)
            {
                Type type = this.OwnerMDI.CloseOK.GetType();
                type.InvokeMember("", BindingFlags.InvokeMethod, null, this.OwnerMDI.CloseOK, null);
            }
            else
            {
                if (this.OwnerMDI.ParentForm != null)
                    this.OwnerMDI.ParentForm.Close();

                if (restart)
                {
                    ThreadStart start = new ThreadStart(this.ReStarting);
                    Thread thread = new Thread(start);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
            }
        }

        private void ReStarting()
        {
            try
            {
                Employer.IsLogon = LogOnStatus.LogOut;

                ThreadStart start = new ThreadStart((new TY.Service.Launcher.Program()).Starting);
                Thread thread = new Thread(start);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception e)
            {
                LocalCapturer.ExceptionCatch(e);
            }
        }
    }
}
