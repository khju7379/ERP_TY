using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using System.IO;
using System.Runtime.InteropServices;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 국세청 자료 변환 및 전송 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.10.31 08:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    /// </summary>
    public partial class TYACTP019B : TYBase
    {
        [DllImport("fcrypt_e.dll", EntryPoint = "DSFC_EncryptFile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int DSFC_EncryptFile(int hWnd, string inputPath, string outputPath, string password, int option);

        [DllImport("fcrypt_e.dll", EntryPoint = "DSFC_EncryptData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int DSFC_EncryptData(int hWnd, byte[] inputData, int dataLen, string outputPath, string password, int option);

        // test 시 사용 
        //[DllImport("d:\\fcrypt_e.dll", EntryPoint = "DSFC_EncryptFile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //private static extern int DSFC_EncryptFile(int hWnd, string inputPath, string outputPath, string password, int option);

        //[DllImport("d:\\fcrypt_e.dll", EntryPoint = "DSFC_EncryptData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //private static extern int DSFC_EncryptData(int hWnd, byte[] inputData, int dataLen, string outputPath, string password, int option);

        
        private static string _assemblyName;

        #region Description : Page_Load
        public TYACTP019B()
        {
            InitializeComponent();
        }

        private void TYACTP019B_Load(object sender, System.EventArgs e)
        {
            _assemblyName = CurrentSystem.DeployUrl;

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_SAV.Visible = false;
        }
        
        #endregion

        #region Description : 암호화전 체크 올리기 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.textBox3.Text.Trim().Length <= 6)
            {
                string sOUTMSG = "비밀번호값을 입력하세요(6자리 이상)";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox3);
                e.Successed = false;
                return;
            }

            if (this.textBox3.Text.Trim() == "")
            {
                string sOUTMSG = "비밀번호값을 입력하세요";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox3);
                e.Successed = false;
                return;
            }

            if (this.textBox4.Text.Trim() == "")
            {
                string sOUTMSG = "비밀번호 확인값을 입력하세요";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox4);
                e.Successed = false;
                return;
            }

            if (this.textBox3.Text != this.textBox4.Text )
            {
                string sOUTMSG = "비밀번호와 비밀번호 확인 값이 일치하지 않습니다. 비밀번호를 다시 입력하세요";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox3);
                e.Successed = false;
                return;
            }

            //원자료 삭제
            string sSourceFile = this.OFD01_SOURCEFILE.FileName;
            if (!File.Exists(sSourceFile))
            {
                string sOUTMSG = sSourceFile + " 화일이 존재 하지 않습니다.(확인요망)";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox2);
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description :  암호화 작업
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //if (this.SFD01_TARGETFILE.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //    return;

            int handle = 0;
            string password = this.textBox4.Text;
            int option = 1;

            // 기존화일 존재시 삭제후 재생성하기 위함
            string sTarFile = this.textBox5.Text;
            if (File.Exists(sTarFile))
            {
                File.Delete(sTarFile);
            }

            this.SFD01_TARGETFILE.FileName = this.textBox5.Text;

            this.textBox1.AppendText("DSFC_EncryptFile(); Start ====================\r\n");
            // @. 호출부
            int ret = DSFC_EncryptFile(handle, this.OFD01_SOURCEFILE.FileName, this.SFD01_TARGETFILE.FileName, password, option);
            this.textBox1.AppendText(string.Format("DSFC_EncryptFile(); return code : {0}\r\n", ret));

            this.textBox1.AppendText("DSFC_EncryptData(); End ====================\r\n");
            //this.textBox1.AppendText(password); // 삭제

            // 데이터 파일 읽기
            Stream stream = this.OFD01_SOURCEFILE.OpenFile();
            BinaryReader r = new BinaryReader(stream);
            int dataLen = (int)stream.Length;
            byte[] buf = r.ReadBytes((int)stream.Length);
            stream.Close();


            //// @. 호출부
            //ret = DSFC_EncryptData(handle, buf, dataLen, "denc_"+this.SFD01_TARGETFILE.FileName + ".txt.denc", password, option);
            //this.textBox1.AppendText(string.Format("DSFC_EncryptData(); return code : {0}\r\n", ret));

            this.textBox3.Text = "";
            this.textBox4.Text = "";

            //원자료 삭제
            //string sSourceFile = this.OFD01_SOURCEFILE.FileName;
            //if (File.Exists(sSourceFile))
            //{
            //    File.Delete(sSourceFile);
            //}

            string sOUTMSG = "자료 암호화 생성 완료";
            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            this.SetFocus(this.textBox5);

        } 
        #endregion

        #region Description :  화일찾기
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            if (this.OFD01_SOURCEFILE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.textBox2.Text = this.OFD01_SOURCEFILE.FileName;
            string sSOURCE = this.OFD01_SOURCEFILE.FileName;

            if (sSOURCE != "")
            {
                int iCNT = sSOURCE.LastIndexOf("\\");
                this.BTN61_SAV.Visible = true;
                this.textBox5.Text = sSOURCE.Insert(iCNT + 1, "enc_");

                this.textBox1.Text = "";
            }
        } 
        #endregion


        #region Description : 국체청 프로그램 실행
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sExeFile = @"C:\ers\ers_won\WEOK1800.exe";
            if (File.Exists(sExeFile))
            {
                System.Diagnostics.Process program = System.Diagnostics.Process.Start(@"C:\ers\ers_won\WEOK1800.exe");
            }
            else
            {
                string sOUTMSG = "국세청 홈페이지에서 다운받아서 설치하세요";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox5);
            }

        } 
        #endregion

        #region Description : 국체청 프로그램 실행(지급명세서)
        private void BTN61_GIVE_Click(object sender, EventArgs e)
        {
            string sExeFile = @"C:\eos\eos_tr\wwok1620.exe";
            if (File.Exists(sExeFile))
            {
                System.Diagnostics.Process program = System.Diagnostics.Process.Start(@"C:\eos\eos_tr\wwok1620.exe");
            }
            else
            {
                string sOUTMSG = "국세청 홈페이지에서 다운받아서 설치하세요(지급명세서)";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox5);
            }
        }
        #endregion

        #region Description : 국체청 프로그램 실행(일용근로)
        private void BTN61_DAILY_Click(object sender, EventArgs e)
        {
            string sExeFile = @"C:\eos\eos_day_tr\SKSWKA04.exe";
            if (File.Exists(sExeFile))
            {
                System.Diagnostics.Process program = System.Diagnostics.Process.Start(@"C:\eos\eos_day_tr\SKSWKA04.exe");
            }
            else
            {
                string sOUTMSG = "국세청 홈페이지에서 다운받아서 설치하세요(일용근로)";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.textBox5);
            }
        }
        #endregion



     }
}
