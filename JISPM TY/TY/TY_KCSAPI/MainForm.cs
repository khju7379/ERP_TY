using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TY_KCSAPI
{
    public partial class MainForm : Form
    {
        private string[] farg;

        public MainForm(string[] arg)
        {
            InitializeComponent();

            farg = arg;
        }

        #region  Description : MainForm_Load 이벤트
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {                
                string str = string.Empty;

                TYBase.ConstCompany = "";  //회사구분
                TYBase.ConstTransModul = "";  //전송구분
                TYBase.ConstKCSAPI4LoginId = "";    //관세청 등록 사용자 
                TYBase.ConstKCSAPI4DocUserId = "";  //관세청 문서 사용자

                if (farg.Length > 0)
                {
                    for (int i = 1; i < farg.Length; i++)
                    {
                        string[] Pramarg = farg[i].ToString().Split(',');

                        TYBase.ConstCompany = Pramarg[0].ToString();  //회사구분
                        TYBase.ConstTransModul = Pramarg[1].ToString();  //전송구분
                        TYBase.ConstKCSAPI4LoginId = Pramarg[2].ToString();  //관세청 등록 사용자 
                        TYBase.ConstKCSAPI4DocUserId = Pramarg[3].ToString();  //관세청 문서 사용자
                    }
                }

                if(TYBase.ConstCompany == "" && TYBase.ConstTransModul == "")
                {
                    MessageBox.Show("호출 경로가 잘되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    this.UP_GET_TransModulCall();
                }                
            }
            catch
            {
                this.Close();
            }           

        }
        #endregion

        #region  Description : 전송 모듈 호출 이벤트
        private void UP_GET_TransModulCall()
        {
            if (TYBase.ConstTransModul == "RX")
            {
                this.Btn_Send_Click(null, null);  //전송
            }
            else
            {
                this.Btn_Recive_Click(null, null); //수신
            }
        }
        #endregion

        #region  Description : 수신 이벤트
        private void Btn_Recive_Click(object sender, EventArgs e)
        {
            try
            {
                this.UP_KCSAPI4Recive_Docment();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }
        #endregion

        #region  Description : KCSAPI4 수신 이벤트
        private void UP_KCSAPI4Recive_Docment()
        {
            
            //공인인증서 로그인
            string Login = TYBase.LoginSecuMdle(TYBase.ConstKCSAPI4LoginId, TYBase.ConstKCSAPI4DocUserId);

            if (Login.Trim().Substring(0, 4) == "C200")
            {
                //통관관련 목록 수신
                string ret = TYBase.RcpnDocLstCscl(TYBase.ConstKCSAPI4LoginId, TYBase.ConstKCSAPI4DocUserId);

                if (ret != "" && ret.Substring(0, 4) != "C200")
                {                   
                    MessageBox.Show(ret, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                //KCSAPI4 폴더 오픈 및 파일 읽기
                this.UP_Get_KCSAPI4FileOpenRead();
            }
        }
        #endregion

        #region  Description : KCSAPI4 폴더 오픈 및 파일 읽기
        private void UP_Get_KCSAPI4FileOpenRead()
        {
            try
            {
                string sDirDate = DateTime.Now.ToString("yyyyMMdd");

                string[] Getfiles = System.IO.Directory.GetFiles(TYBase.ConstKCSAPIPath + "\\" + sDirDate + "\\Rcv\\", "*.txt");

                if (Getfiles.Length > 0)
                {
                    pgBar.Visible = true;

                    pgBar.Minimum = 0;
                    pgBar.Maximum = 0;
                    pgBar.Value = 0;
                }
                else
                {
                    MessageBox.Show("수신 할 자료가 존재하지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }

                foreach (string file in Getfiles)
                {
                    //파일존재 체크
                    if (System.IO.File.Exists(file))
                    {
                        UP_KCSAPI4DataReceiveToConvert(file);
                    }
                }
            }
            catch
            {
            }
            finally
            {

                string LoginOut = TYBase.LogoutSecuMdle();

                pgBar.Visible = false;                
            }
        }
        #endregion

        #region  Description : KCSAPI4 변환 이벤트
        private void UP_KCSAPI4DataReceiveToConvert(string sFileName)
        {
            string sRevDataA = string.Empty;
            string sConversationID = string.Empty;
            string sDocCode = string.Empty;
            string[] arrayResult;
            string ret = string.Empty;

            TXT01_AFFILENAME.Text = sFileName;

            //수신파일 변환
            if (TXT01_AFFILENAME.Text.Trim() != "")
            {
                //파일 읽기
                StreamReader file = new StreamReader(TXT01_AFFILENAME.Text.Trim(), Encoding.Default);
                sRevDataA = file.ReadToEnd();
                file.Close();

                if (sRevDataA.Length > 0)
                {
                    arrayResult = sRevDataA.Split('\n');
                    if (arrayResult.Length > 0)
                    {
                        pgBar.Maximum = arrayResult.Length;

                        for (int i = 0; i < arrayResult.Length - 1; i++)
                        {
                            string[] arrayDoc = arrayResult[i].ToString().Split(',');
                            //xml문서번호
                            sConversationID = arrayDoc[0].ToString();
                            //문서코드
                            sDocCode = arrayDoc[1].ToString();

                            //문서번호에 해당하는 XML파일 수신
                            ret = TYBase.RcpnDocCscl(TYBase.ConstKCSAPI4LoginId, TYBase.ConstKCSAPI4DocUserId, sDocCode, sConversationID);

                        }
                    } //if (arrayResult.Length > 0)..end                    
                }
            }
        }
        #endregion

        #region  Description : 전송 이벤트
        private void Btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                this.UP_KCSAPI4_DataToTrans();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }
        #endregion

        #region  Description : KCSAPI4 XML 송신 이벤트
        private void UP_KCSAPI4_DataToTrans()
        {
            int iSendTotal = 0;
            int iOkCnt = 0;
            int iErrorCnt = 0;

            int iPoint = 0;
            string sDocCode = string.Empty;
            string ret = string.Empty;
            string sFileName = string.Empty;
            string sConversationID = string.Empty;

            pgBar.Minimum = 0;
            pgBar.Maximum = 0;
            pgBar.Value = 0;
            pgBar.Refresh();

            string[] _FileList = System.IO.Directory.GetFiles(TYBase.ConstKCSAPIPath + "\\upload\\");

            if (_FileList.Length > 0)
            {
                //xml 파일 총건수
                iSendTotal = _FileList.Length;

                pgBar.Maximum = iSendTotal;

                //공인인증서 로그인
                string Login = TYBase.LoginSecuMdle(TYBase.ConstKCSAPI4LoginId, TYBase.ConstKCSAPI4DocUserId);

                if (Login.Trim().Substring(0, 4) == "C200")
                {

                    for (int i = 0; i < _FileList.Length; i++)
                    {
                        sFileName = _FileList[i].ToString();
                        iPoint = sFileName.IndexOf(".");
                        sConversationID = sFileName.Substring(iPoint - 18, 18);

                        sDocCode = TYBase.UP_Get_XmlToDocCode(sFileName);

                        //통관관련 문서 송신
                        ret = TYBase.TrsmDocCscl(TYBase.ConstKCSAPI4LoginId, TYBase.ConstKCSAPI4DocUserId, sDocCode, sConversationID, sFileName);

                        //정상처리가 아니면
                        if (ret != "" && ret.Substring(0, 4) != "C200")
                        {
                            iErrorCnt += 1;
                        }
                        else
                        {
                            iOkCnt += 1;
                        }

                        pgBar.Value = pgBar.Value + 1;
                        pgBar.Refresh();
                    }
                    //공인인증서 로그인아웃
                    string LoginOut = TYBase.LogoutSecuMdle();

                    string sShowMsg = "전송총건수:" + iSendTotal.ToString() + "  정상건수:" + iOkCnt.ToString() + "   오류건수:" + iErrorCnt.ToString();

                    MessageBox.Show(sShowMsg, "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Login, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }

        }
        #endregion

       
    }
}
