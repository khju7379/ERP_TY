using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 제출자료 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.06.15 15:50
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
    ///  CLO : 닫기
    /// </summary>
    public partial class TYHRNT006B : TYBase
    {

        
        private string fsCOMPY;
        private string fsYEAR;
        private string fsKBSABUN;
        private string fsFixGubn;
        private string fsPopGubn;   //1-TYHRNT001S 2-TYHRNT001P

        private bool fbGonJeDoc;  //공제신고서 문서인지 구분 체크

        private TYData DAT30_YACOMPANY;
        private TYData DAT30_YAYEAR;
        private TYData DAT30_YASABUN;
        private TYData DAT30_YASEQ;
        private TYData DAT30_YADESC;
        private TYData DAT30_YAFILENAME;
        private TYData DAT30_YAFILESIZE;
        private TYData DAT30_YAFILEBYTE;
        private TYData DAT30_YAHISAB;

        private DataTable ftAttachTable;                          

        private object _TXT01_SDATE_Value;
        private object _CBH01_KBSABUN_Value;

        private string fsdata_FamilyList = string.Empty; //부양가족 주민번호 리스트                

        #region  Description : 폼 로드 이벤트
        public TYHRNT006B(string sCOMPY, string sYear, string sKBSABUN, string sFixGubn, string sPopGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsCOMPY = sCOMPY;
            fsYEAR = sYear;
            fsKBSABUN = sKBSABUN;
            fsFixGubn = sFixGubn;
            fsPopGubn = sPopGubn;

            DAT30_YACOMPANY = new TYData("DAT30_YACOMPANY", null);
            DAT30_YAYEAR = new TYData("DAT30_YAYEAR", null);
            DAT30_YASABUN = new TYData("DAT30_YASABUN", null);
            DAT30_YASEQ = new TYData("DAT30_YASEQ", null);
            DAT30_YADESC = new TYData("DAT30_YADESC", null);
            DAT30_YAFILENAME = new TYData("DAT30_YAFILENAME", null);
            DAT30_YAFILESIZE = new TYData("DAT30_YAFILESIZE", null);
            DAT30_YAFILEBYTE = new TYData("DAT30_YAFILEBYTE", null);
            DAT30_YAHISAB = new TYData("DAT30_YAHISAB", null);  
        }

        private void TYHRNT006B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            
            this.ControlFactory.Add(DAT30_YACOMPANY);
            this.ControlFactory.Add(DAT30_YAYEAR);
            this.ControlFactory.Add(DAT30_YASABUN);
            this.ControlFactory.Add(DAT30_YASEQ);
            this.ControlFactory.Add(DAT30_YADESC);
            this.ControlFactory.Add(DAT30_YAFILENAME);
            this.ControlFactory.Add(DAT30_YAFILESIZE);
            this.ControlFactory.Add(DAT30_YAFILEBYTE);
            this.ControlFactory.Add(DAT30_YAHISAB);

            ftAttachTable = UP_Set_AttachFileTable();

            fbGonJeDoc = false;

            TXT01_SDATE.SetValue(fsYEAR);
            CBH01_KBSABUN.SetValue(fsKBSABUN);

            this.BTN61_BATCH.IsAsynchronous = true;  

        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sMsg = string.Empty;

            this._TXT01_SDATE_Value = this.TXT01_SDATE.GetValue();
            this._CBH01_KBSABUN_Value = this.CBH01_KBSABUN.GetValue();

            //연말정산 국세청 자료 Table(개인별) 전체 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CDF1263", fsCOMPY,
                                                       TXT01_SDATE.GetValue().ToString(),
                                                       this.CBH01_KBSABUN.GetValue(),
                                                       "1",
                                                       ""
             );
            sMsg = this.DbConnector.ExecuteScalar().ToString();           

        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            

            try
            {
                string sXml = string.Empty;                
                byte[] _AttachFile = null;
                string filePath = string.Empty;
                int fileSize = 0;            

                //부양가족 주민번호 담기
                fsdata_FamilyList = "";
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77BB4098", fsCOMPY, this.TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey,"Y");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        fsdata_FamilyList = fsdata_FamilyList + dt.Rows[i]["WFJUMIN"].ToString().Replace("-", "").Trim() + ",";
                    }
                }                

                ftAttachTable.Clear();               

                object _objAttachFile = null;

                if (ListBox_FileName.Items.Count > 0)
                {
                    for (int i = 0; i < ListBox_FileName.Items.Count; i++)
                    {
                        filePath = ListBox_FileName.Items[i].ToString();

                        ////원본검증
                        //long result = UP_Get_VerifyCheck(filePath);

                        //if (result > 0)
                        //{
                        //    this.ShowCustomMessage("인증되지 않은 파일입니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        //    e.Successed = false;
                        //    return;
                        //}

                        _AttachFile = UP_Get_Byte(filePath);

                        _objAttachFile = _AttachFile;

                        int ArraySize = _AttachFile.GetUpperBound(0);

                        // 용량체크(1메가)            
                        if (ArraySize > 100000000)
                        {
                            this.ShowCustomMessage("첨부파일 용량은 100메가를 초과 할수 없습니다", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            e.Successed = false;
                            return;
                        }

                        fileSize = NTS_GetFileSize(filePath, "", "XML", 0);

                        if (fileSize < 0)
                        {
                            this.ShowCustomMessage("국세청에서 발급된 전자문서가 아닙니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            e.Successed = false;
                            return;
                        }

                        sXml = UP_Get_ConvertToXml(filePath, "");

                        if (sXml == "")
                        {
                            this.ShowCustomMessage("비밀번호가 설정되어 있습니다! 비밀번호를 삭제후 다시 제출해 주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            e.Successed = false;
                            return;
                        }

                        DataRow rw;
                        rw = ftAttachTable.NewRow();

                        rw["YACOMPANY"] = fsCOMPY;
                        rw["YAYEAR"] = TXT01_SDATE.GetValue().ToString();
                        rw["YASABUN"] = CBH01_KBSABUN.GetValue().ToString();
                        rw["YASEQ"] = (i + 1).ToString();
                        rw["YADESC"] = UP_Set_FileName(filePath);
                        rw["YAFILENAME"] = filePath;
                        rw["YAFILESIZE"] = ArraySize.ToString();
                        rw["YAFILEBYTE"] = _objAttachFile;
                        rw["YAHISAB"] = TYUserInfo.EmpNo;

                        ftAttachTable.Rows.Add(rw);
                    }
                }
                else
                {
                    this.ShowCustomMessage("제출자료를 선택해주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                if (!this.ShowMessage("TY_M_HR_7BLI5083"))
                {
                    e.Successed = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ShowCustomMessage(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            finally
            {
                
            }
        }
        #endregion       

        #region  Description : 찾아보기 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            ListBox_FileName.Items.Clear();

            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.Filter = "PDF Files(*.PDF)|*.PDF";
            fileDlg.Multiselect = true;
            

            if (fileDlg.ShowDialog() == DialogResult.OK)            
            {
                string[] arrayFileName = fileDlg.FileNames;

                if( arrayFileName.Length > 0)
                {
                    for( int i = 0; i < arrayFileName.Length; i++ )
                    {
                        ListBox_FileName.Items.Add(arrayFileName[i]);
                    }
                }
            }
        }
        #endregion       
        
        
        #region  Description : XML 파일 읽기 함수
        private string UP_Get_ConvertToXml(string sFilePath, string sPass)
        {
            string filePath = sFilePath;
            string password = sPass;
            string strXML = "XML";

            if (string.IsNullOrEmpty(filePath))
            {
                return "";
            }

            int fileSize = NTS_GetFileSize(filePath, password, strXML, 0);

            if (fileSize > 0)
            {
                byte[] buf = new byte[fileSize];
                fileSize = NTS_GetFileBuf(filePath, password, strXML, buf, 0);
                if (fileSize > 0)
                {
                    string strBuf = Encoding.UTF8.GetString(buf);
                    strBuf.Replace("\n", "\r\n");                    
                    strXML   =  strBuf;
                }
            }

            return strXML;
        }
        #endregion

        #region ▒▒▒ 파일 위변조 체크 함수 ▒▒▒
        private long UP_Get_VerifyCheck(string sChekFileName)
        {
            long result = 0;
            string filePath = sChekFileName;
            string strMsg = string.Empty;

            byte[] baGenTime = new byte[1024];
            byte[] baHashAlg = new byte[1024];
            byte[] baHashVal = new byte[1024];
            byte[] baCertDN = new byte[1024];

            result = DSTSPdfSigVerifyF(filePath, baGenTime, baHashAlg, baHashVal, baCertDN);

            String sGenTimeTemp = Encoding.Unicode.GetString(baGenTime);
            String sHashAlgTemp = Encoding.Unicode.GetString(baHashAlg);
            String sHashValTemp = Encoding.Unicode.GetString(baHashVal);
            String sCertDNTemp = Encoding.Unicode.GetString(baCertDN);

            String sGenTime = sGenTimeTemp.Replace('\0', ' ').Trim();
            String sHashAlg = sHashAlgTemp.Replace('\0', ' ').Trim();
            String sHashVal = sHashValTemp.Replace('\0', ' ').Trim();
            String sCertDN = sCertDNTemp.Replace('\0', ' ').Trim();

            switch (result)
            {
                case 0:
                    strMsg = String.Format("원본 파일입니다. \n\nTS시각: {0} \n해쉬알고리즘: {1} \n해쉬값: {2} \nTSA인증서: {3}", sGenTime, sHashAlg, sHashVal, sCertDN);
                    break;
                case 2101:
                    strMsg = String.Format("문서가 변조되었습니다.");
                    break;
                case 2102:
                    strMsg = String.Format("TSA 서명 검증에 실패하였습니다.");
                    break;
                case 2103:
                    strMsg = String.Format("지원하지 않는 해쉬알고리즘 입니다.");
                    break;
                case 2104:
                    strMsg = String.Format("해당 파일을 읽을 수 없습니다.");
                    break;
                case 2105:
                    strMsg = String.Format("서명검증을 위한 API 처리 오류입니다.");
                    break;
                case 2106:
                    strMsg = String.Format("타임스탬프 토큰 데이터 파싱 오류입니다.");
                    break;
                case 2107:
                    strMsg = String.Format("TSA 인증서 처리 오류입니다.");
                    break;
                case 2108:
                    strMsg = String.Format("타임스탬프가 적용되지 않은 파일입니다.");
                    break;
                case 2109:
                    strMsg = String.Format("인증서 검증에 실패 하였습니다.");
                    break;
                default:
                    strMsg = String.Format("에러코드 미정의 error - [%d]", result);
                    break;
            }

            return result;
        }
        #endregion

        #region Descrioption : 파일 이름 가져오기
        private string UP_Set_FileName(string sStr)
        {
            string sValue = "";
            int i = 0;
            int iPoint = 0;
            for (i = 0; i < sStr.Length; i++)
            {
                if (sStr.Substring(i, 1) == "\\")
                {
                    iPoint = i;
                }
            }

            for (i = iPoint + 1; i < sStr.Length; i++)
            {
                sValue = sValue + sStr.Substring(i, 1);
            }

            return sValue;
        }
        #endregion                                          
        
        #region  Description :  구조체 변수 선언
        private DataTable UP_Set_AttachFileTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("YACOMPANY", typeof(System.String));
            dt.Columns.Add("YAYEAR", typeof(System.String));
            dt.Columns.Add("YASABUN", typeof(System.String));
            dt.Columns.Add("YASEQ", typeof(System.String));

            dt.Columns.Add("YADESC", typeof(System.String));
            dt.Columns.Add("YAFILENAME", typeof(System.String));
            dt.Columns.Add("YAFILESIZE", typeof(System.String));
            dt.Columns.Add("YAFILEBYTE", typeof(System.Object));
            dt.Columns.Add("YAHISAB", typeof(System.String));            

            dt.TableName = "TableNames";

            return dt;
        }
        #endregion

        #region  Description : BTN61_BATCH InvokerStart, InvokerEnd  이벤트
        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            string sXml = string.Empty;
            string sEduGn = string.Empty;

            try
            {

                if (ftAttachTable.Rows.Count > 0)
                {
                    //첨부파일 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_76G9A826", fsCOMPY,
                                                               this._TXT01_SDATE_Value,
                                                               this._CBH01_KBSABUN_Value

                     );
                    this.DbConnector.ExecuteTranQuery();

                    //첨부파일 등록                
                    for (int i = 0; i < ftAttachTable.Rows.Count; i++)
                    {
                        DAT30_YACOMPANY.SetValue(ftAttachTable.Rows[i]["YACOMPANY"].ToString());
                        DAT30_YAYEAR.SetValue(ftAttachTable.Rows[i]["YAYEAR"].ToString());
                        DAT30_YASABUN.SetValue(ftAttachTable.Rows[i]["YASABUN"].ToString());
                        DAT30_YASEQ.SetValue(ftAttachTable.Rows[i]["YASEQ"].ToString());
                        DAT30_YADESC.SetValue(ftAttachTable.Rows[i]["YADESC"].ToString());
                        DAT30_YAFILENAME.SetValue(ftAttachTable.Rows[i]["YAFILENAME"].ToString());
                        DAT30_YAFILESIZE.SetValue(ftAttachTable.Rows[i]["YAFILESIZE"].ToString());
                        DAT30_YAFILEBYTE.SetValue(ftAttachTable.Rows[i]["YAFILEBYTE"]);
                        DAT30_YAHISAB.SetValue(ftAttachTable.Rows[i]["YAHISAB"].ToString());

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_76G94825", this.ControlFactory, "30");
                        this.DbConnector.ExecuteTranQuery();
                    }


                    e.DbConnector.CommandClear();
                    for (int i = 0; i < ftAttachTable.Rows.Count; i++)
                    {
                        sXml = UP_Get_ConvertToXml(ftAttachTable.Rows[i]["YAFILENAME"].ToString(), "");

                        //TYHomeTax.fsCOMPY = fsCOMPY;
                        //TYHomeTax._TaxYEAR = fsYEAR;
                        //TYHomeTax._TaxSABUN = fsKBSABUN;

                        //TYHomeTax.UP_Set_XmlToParsing(sXml, e);

                        //if (e.DbConnector.CommandCount > 0)
                        //    e.DbConnector.ExecuteTranQueryList();

                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowCustomMessage(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            
           
        }

        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            if (e.Successed)
            {
                //교육비중에 부양가족관리에 교육구분이 설정안되어 있는경우 자동으로 교육구분을 설정해준다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_81OHD530", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value, TYUserInfo.SecureKey, "Y");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        //교육비금액은 있는데 부양가족관리 교육구분 설정이 안되어 있으면 
                        if (Convert.ToDouble(dt.Rows[j]["NSsum"].ToString()) > 0 && dt.Rows[j]["WFEDUGN"].ToString() == "")
                        {

                            this.DbConnector.Attach("TY_P_HR_81OHJ531", dt.Rows[j]["CHKEDUGN"].ToString(),
                                                                     TYUserInfo.EmpNo,
                                                                     dt.Rows[j]["NSCOMPANY"].ToString(),
                                                                     dt.Rows[j]["NSYEAR"].ToString(),
                                                                     dt.Rows[j]["NSSABUN"].ToString(),
                                                                     TYUserInfo.SecureKey, "Y",
                                                                     dt.Rows[j]["NSresid"].ToString()
                                                                     );
                        }
                    }

                    if (this.DbConnector.CommandCount > 0)
                        this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_HR_7BLI6085");
            }
        }
        #endregion                            

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

            if (fsPopGubn == "1")
            {
                this.OpenModalPopup(new TYHRNT001P(fsCOMPY, fsYEAR, fsKBSABUN, fsFixGubn));
            }
        }
        #endregion


    }
}
