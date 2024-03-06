using System;
using System.Data;
using System.Text;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Web;
using System.Web.UI; 
using System.Security.Cryptography;
using ThoughtWorks.QRCode.Codec;

using System.Windows.Forms;


namespace TY.ER.GB99
{
    /// <summary>
    /// 인사 자기소개 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.11.12 10:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_3BCB0258 : 인사 기본사항 조회
    ///  TY_P_HR_3BCA1255 : 인사 자기소개 입력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  SAV : 저장
    ///  KBSABUN : 사번
    ///  KBINTRO : 자기소개
    /// </summary>
    public partial class TYERGB991I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYERGB991I()
        {
            InitializeComponent();
            this.SetPopupStyle(); 

            
        }

        private void TYERGB991I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.CBH01_KBSABUN.SetValue(TYUserInfo.EmpNo);

            UP_Search();

            this.SetStartingFocus(this.TXT01_KBCOMTEL);
        }
        #endregion

        #region  Description : 사번 조회
        private void UP_Search()
        {
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_3BCB0258", this.CBH01_KBSABUN.GetValue());

            //this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.CBH01_KBSABUN.GetValue());
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", CBH01_KBSABUN.GetValue(), TYUserInfo.SecureKey, "Y");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_KBBUSEO.SetValue(dt.Rows[0]["KBBUSEONM"].ToString());
                if (dt.Rows[0]["KBJKCD"].ToString() == "01")
                {
                    this.LBL51_KBBUSEO.Visible = false;
                    this.TXT01_KBBUSEO.Visible = false;                     
                }
                else
                {
                    this.LBL51_KBBUSEO.Visible = true;
                    this.TXT01_KBBUSEO.Visible = true;
                }
                
                //this.TXT01_KBJKCD.SetValue(dt.Rows[0]["KBJKCDNM"].ToString());
                this.TXT01_KBJKCD.SetValue(dt.Rows[0]["KBJJCDNM"].ToString());
                this.TXT01_KBJUSO.SetValue(dt.Rows[0]["KBCOMJUSO"].ToString());
                this.TXT01_KBMAILID.SetValue(dt.Rows[0]["KBMAILID"].ToString());
                this.TXT01_KBSOSOK.SetValue(dt.Rows[0]["KBSOSOKNM"].ToString());
                
                this.TXT01_KBCOMFAX.SetValue(dt.Rows[0]["KBCOMFAX"].ToString());
                this.TXT01_KBCOMTEL.SetValue(dt.Rows[0]["KBCOMTEL"].ToString());
                this.TXT01_KBMOBILE.SetValue(dt.Rows[0]["KBMOBILE"].ToString());

                this.TXT01_KBINTRO.SetValue(dt.Rows[0]["KBINTRO"].ToString());
            }

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_3BCA1255",this.TXT01_KBMOBILE.GetValue(), this.TXT01_KBCOMTEL.GetValue(),  this.TXT01_KBCOMFAX.GetValue(), this.TXT01_KBINTRO.GetValue(), this.CBH01_KBSABUN.GetValue());
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false; 
                return;
            }
        }
        #endregion

        #region Description : 닫기 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        #endregion

        #region Description : QRCODE생성 이벤트
        private void BTN61_QRCODE_Click(object sender, EventArgs e)
        {
            // 생성할 이미지 사이즈 정함.
            int sizeNum = 1;
            int level = 0;
            string imgnm = "";

            string sKBJKCD = ""; 
            // 다른건 입력 안해도 이름은 꼭 넣어라.
            string sSabun = this.CBH01_KBSABUN.GetValue().ToString(); 
            string userName = this.CBH01_KBSABUN.GetText().ToString();  


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_3BCB0258", this.CBH01_KBSABUN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sKBJKCD = dt.Rows[0]["KBJKCD"].ToString();
            }                        

            // MECARD 포맷으로 변경한다.
            StringBuilder cardeFormat = new StringBuilder();

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {

                imgnm = sSabun + "_nc.png";
                //cardeFormat.Append("MECARD:"); // 카드형태 선언
                cardeFormat.Append("BEGIN:VCARD\nVERSION:3.0\n"); // 카드형태 선언 
                cardeFormat.AppendFormat("N:{0}\n", userName.Trim()); // 이름
                level = 13;
                if (sKBJKCD != "01")
                {
                    cardeFormat.AppendFormat("ORG:{0}\n", "(주)태영인더스트리/" + TXT01_KBBUSEO.GetValue().ToString()); // 팀
                }
                else
                {
                    cardeFormat.AppendFormat("ORG:{0}\n", "(주)태영인더스트리/" + TXT01_KBSOSOK.GetValue().ToString()); // 소속
                }
                cardeFormat.AppendFormat("TEL;TYPE=CELL:{0}\n", TXT01_KBMOBILE.GetValue().ToString()); // 휴대폰
                cardeFormat.AppendFormat("TEL;TYPE=VOICE,MSG,WORK:{0}\n", TXT01_KBCOMTEL.GetValue().ToString()); // 전화번호
                cardeFormat.AppendFormat("TEL;TYPE=FAX,WORK:{0}\n", TXT01_KBCOMFAX.GetValue().ToString()); // FAX
                //cardeFormat.AppendFormat("ADR;TYPE=WORK:{0}\n", TXT01_KBJUSO.GetValue().ToString()); // 주소
                cardeFormat.AppendFormat("EMAIL;TYPE=WORK,postal,parcel:{0}\n", TXT01_KBMAILID.GetValue().ToString()); // 이메일
                cardeFormat.AppendFormat("URL;TYPE=WORK:{0}\n", "http://www.taeyoung.co.kr"); // URL
                cardeFormat.Append("END:VCARD");     

                //level = 13;

                ////|TY|차번4자리|모선명|양수인(선진 또는 하림)|원료코드|중량|
                //string sStr = "|TY|" + "4019" + "|" + "AEOLIAN ARROW" + "|" + "101007" + "|" + "12" + "|" + "29070" + "|";

                //cardeFormat.Append(sStr); 

            }
            else
            {
                imgnm = sSabun + "_url.png";
                level = 6;               
                
                string sPram = System.Web.HttpUtility.UrlEncode(DesEncrypt(sSabun));

                cardeFormat.Append("http://tync.taeyoung.co.kr/?sabun=" + sPram);                

            }

            // QRCodeEncoder 인스턴스 생성
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            // 인코딩모드는 바이트로 설정.
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            // QRCode 사이즈 지정.
            qrCodeEncoder.QRCodeScale = sizeNum;
            // 버전을 지정.(주의 : MECOARD로 만들려면 9이상으로 지정한다.)
            //qrCodeEncoder.QRCodeVersion = 10;
            

            qrCodeEncoder.QRCodeVersion = level;

            // 에러 보정 레벨을 지정.
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            
            //switch (cbCorrectionLevel.SelectedValue.ToString())
            //{
            //    case "H":
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            //        break;
            //    case "Q":
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            //        break;
            //    case "L":
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            //        break;
            //    case "M":
            //    default:
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //        break;
            //}

            System.Drawing.Image qrImage;            
            
            string data = cardeFormat.ToString();
            try
            {
                // QRCode 이미지를 생성해 줌.
                qrImage = qrCodeEncoder.Encode(data);

                pBox.Image = qrImage;
            }
            catch (Exception ex)
            {
                
            }

        }
        #endregion

        #region Description : PC저장 이벤트
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            saveFileDialog1.Title = "Save";
            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                saveFileDialog1.FileName = this.CBH01_KBSABUN.GetValue().ToString()+"_nc"; 
            }
            else
            {
                saveFileDialog1.FileName = this.CBH01_KBSABUN.GetValue().ToString() + "_url"; 
            }
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pBox.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pBox.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pBox.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        this.pBox.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }

                fs.Close();
            }
        }
        #endregion

        #region Description : 암호화 함수
        public string UP_Encode(string sabun)
        {
            string[] tmp;

            if (sabun.Substring(0, 1) != "C")
            {
                tmp = sabun.Split('-');
                tmp[0] = (Convert.ToInt32(tmp[0]) * 13).ToString();
            }
            else
            {
                tmp = sabun.Substring(1, 5).Split('-'); 
                tmp[0] = "C" + (Convert.ToInt32(tmp[0]) * 13).ToString();
            }

            if (tmp[1] == "M") tmp[1] = "NQx90";
            else tmp[1] = "B53Mn";

            return tmp[0] + "A" + tmp[1];
        }
        #endregion

        //암호화
        public string DesEncrypt(string str)
        {
            byte[] iv = { 16, 29, 51, 112, 210, 78, 98, 186 };
            byte[] key = { 57, 129, 125, 118, 233, 60, 13, 94, 153, 156, 188, 9, 109, 20, 138, 7, 31, 221, 215, 91, 241, 82, 254, 189 };

            string encryptStr = string.Empty;

            byte[] bytIn = null;
            byte[] bytOut = null;
            MemoryStream ms = null;
            TripleDESCryptoServiceProvider tcs = null;
            ICryptoTransform ct = null;
            CryptoStream cs = null;

            try
            {

                bytIn = System.Text.Encoding.UTF8.GetBytes(str);

                ms = new MemoryStream();

                tcs = new TripleDESCryptoServiceProvider();

                ct = tcs.CreateEncryptor(key, iv);

                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

                cs.Write(bytIn, 0, bytIn.Length);

                cs.FlushFinalBlock();

                bytOut = ms.ToArray();

                encryptStr = System.Convert.ToBase64String(bytOut, 0, bytOut.Length);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (cs != null) { cs.Clear(); cs = null; }
                if (ct != null) { ct.Dispose(); ct = null; }
                if (tcs != null) { tcs.Clear(); tcs = null; }
                if (ms != null) { ms = null; }
            }

            return encryptStr;

        }

        //복호화
        public string DesDecrypt(string str)
        {
            byte[] iv = { 16, 29, 51, 112, 210, 78, 98, 186 };
            byte[] key = { 57, 129, 125, 118, 233, 60, 13, 94, 153, 156, 188, 9, 109, 20, 138, 7, 31, 221, 215, 91, 241, 82, 254, 189 };

            string decryptStr = string.Empty;

            byte[] bytIn = null;
            MemoryStream ms = null;
            TripleDESCryptoServiceProvider tcs = null;
            CryptoStream cs = null;
            ICryptoTransform ct = null;
            StreamReader sr = null;

            try
            {

                bytIn = System.Convert.FromBase64String(str);
                ms = new MemoryStream(bytIn, 0, bytIn.Length);
                tcs = new TripleDESCryptoServiceProvider();
                ct = tcs.CreateDecryptor(key, iv);
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                sr = new StreamReader(cs);

                decryptStr = sr.ReadToEnd();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (sr != null) { sr.Close(); sr = null; }
                if (cs != null) { cs.Clear(); cs = null; }
                if (ct != null) { ct.Dispose(); ct = null; }
                if (tcs != null) { tcs.Clear(); tcs = null; }
                if (ms != null) { ms.Close(); ms = null; }
            }

            return decryptStr;
        }
               

    }
}
