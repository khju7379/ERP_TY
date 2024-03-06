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
    public partial class TYHRNT001B : TYBase
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

        NTS_A102Y struct_A102Y = new NTS_A102Y();  //보험료(보장성,장애인보장성)
        NTS_A102M struct_A102M = new NTS_A102M();  //보험료(보장성,장애인보장성)_상세내역
        NTS_B101Y struct_B101Y = new NTS_B101Y();  //의료비
        NTS_B101D struct_B101D = new NTS_B101D();  //의료비_상세내역

        NTS_B201Y struct_B201Y = new NTS_B201Y();  //실손의료보험

        NTS_C102Y struct_C102Y = new NTS_C102Y();  //교육비
        NTS_C102M struct_C102M = new NTS_C102M();  //교육비_상세내역
        NTS_C202Y struct_C202Y = new NTS_C202Y();  //교육비(직업훈련비)
        NTS_C202M struct_C202M = new NTS_C202M();  //교육비(직업훈련비)_상세내역
        NTS_C301Y struct_C301Y = new NTS_C301Y();  //교육비(교복구입비)
        NTS_C301M struct_C301M = new NTS_C301M();  //교육비(교복구입비)_상세내역
        NTS_C401Y struct_C401Y = new NTS_C401Y();  //교육비(학자금대출)
        NTS_C401M struct_C401M = new NTS_C401M();  //교육비(학자금대출)_상세내역

        NTS_D101Y struct_D101Y = new NTS_D101Y();  //개인연금저축
        NTS_D101M struct_D101M = new NTS_D101M();  //개인연금저축_상세내역

        NTS_E102Y struct_E102Y = new NTS_E102Y();  //연금저축 + 상세내역 
        NTS_F102Y struct_F102Y = new NTS_F102Y();  //퇴직연금 + 상세내역 

        NTS_G106Y struct_G106Y = new NTS_G106Y();  //신용카드+직불카드
        NTS_G106M struct_G106M = new NTS_G106M();  //신용카드+직불카드_상세내역 

        NTS_G108Y struct_G108Y = new NTS_G108Y();  //신용카드+직불카드(2020년이후)
        NTS_G108M struct_G108M = new NTS_G108M();  //신용카드+직불카드_상세내역(2020년이후)

        NTS_G110Y struct_G110Y = new NTS_G110Y();  //신용카드+직불카드(2021년이후)
        NTS_G110M struct_G110M = new NTS_G110M();  //신용카드+직불카드_상세내역(2021년이후)

        NTS_G206M struct_G206M = new NTS_G206M();  //현금영수증_상세내역 

        NTS_G208M struct_G208M = new NTS_G208M();  //현금영수증_상세내역 (2020년이후)
        NTS_G210M struct_G210M = new NTS_G210M();  //현금영수증_상세내역 (2021,2022년이후)

        NTS_J101Y struct_J101Y = new NTS_J101Y();  //주택임차차입금 원리금상환액
        NTS_J101M struct_J101M = new NTS_J101M();  //주택임차차입금 원리금상환액_상세내역 

        NTS_J203Y struct_J203Y = new NTS_J203Y();  //장기주택저당차입금 이자상환액
        NTS_J203M struct_J203M = new NTS_J203M();  //장기주택저당차입금 이자상환액_상세내역 

        NTS_J301Y struct_J301Y = new NTS_J301Y();  //주택마련저축
        NTS_J301M struct_J301M = new NTS_J301M();  //주택마련저축_상세내역 

        NTS_J401Y struct_J401Y = new NTS_J401Y();  //목돈 안드는 전세 이자상환액
        NTS_J401M struct_J401M = new NTS_J401M();  //목돈 안드는 전세 이자상환액_상세내역 

        NTS_K101M struct_K101M = new NTS_K101M();  //소기업소상공인 공제부금

        NTS_L102Y struct_L102Y = new NTS_L102Y();  //기부금
        NTS_L102D struct_L102D = new NTS_L102D();  //기부금_상세내역

        NTS_N101Y struct_N101Y = new NTS_N101Y();  //장기집합투자증권저축
        NTS_N101M struct_N101M = new NTS_N101M();  //장기집합투자증권저축_상세내역

        NTS_O101M  struct_O101M = new NTS_O101M();  //건강보험료
        NTS_P102M struct_P102M = new NTS_P102M();   //국민연금
        
        NTS_J501Y struct_J501Y = new NTS_J501Y();   //월세액(2020년이후)

        NTS_A101Y struct_A101Y = new NTS_A101Y();   //공제신고서 마스타, 인적공제        

        NTS_Doc_B101Y struct_Doc_B101Y = new NTS_Doc_B101Y();   //공제신고서 연금저축등 소득.세액 명세
        NTS_Doc_B101R struct_Doc_B101R = new NTS_Doc_B101R();   //공제신고서 퇴직연금 공제명세
        NTS_Doc_B101P struct_Doc_B101P = new NTS_Doc_B101P();   //공제신고서 연금저축 공제명세
        NTS_Doc_B101H struct_Doc_B101H = new NTS_Doc_B101H();   //공제신고서 주택마련저축 공제명세
        NTS_Doc_B101L struct_Doc_B101L = new NTS_Doc_B101L();   //공제신고서 장기집합저축 공제명세

        
        NTS_Main struct_Main = new NTS_Main();

        System.Collections.Generic.List<object[]> data_A102Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_A102M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_B101Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_B101D = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C102Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C102M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C202Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C202M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C301Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C301M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C401Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_C401M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_D101Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_D101M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_E102Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_F102Y = new System.Collections.Generic.List<object[]>();

        System.Collections.Generic.List<object[]> data_G106Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_G106M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_G206M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J101Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J101M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J203Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J203M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J301Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J301M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J401Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J401M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_K101M = new System.Collections.Generic.List<object[]>();

        System.Collections.Generic.List<object[]> data_L102Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_L102D = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_N101Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_N101M = new System.Collections.Generic.List<object[]>();
        
        System.Collections.Generic.List<object[]> data_O101M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_P102M = new System.Collections.Generic.List<object[]>();

        System.Collections.Generic.List<object[]> data_A101Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_A101M = new System.Collections.Generic.List<object[]>(); //공제신고서 인적공제

        System.Collections.Generic.List<object[]> data_Doc_B101Y = new System.Collections.Generic.List<object[]>(); //공제신고서 연금저축등 소득.세액 명세 마스타
        System.Collections.Generic.List<object[]> data_Doc_B101R = new System.Collections.Generic.List<object[]>(); //공제신고서 퇴직연금 공제명세
        System.Collections.Generic.List<object[]> data_Doc_B101P = new System.Collections.Generic.List<object[]>(); //공제신고서 연금저축 공제명세
        System.Collections.Generic.List<object[]> data_Doc_B101H = new System.Collections.Generic.List<object[]>(); //공제신고서 주택마련저축 공제명세
        System.Collections.Generic.List<object[]> data_Doc_B101L = new System.Collections.Generic.List<object[]>(); //공제신고서 장기집합저축 공제명세        

        System.Collections.Generic.List<object[]> data_G108Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_G108M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_G110Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_G110M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_J501Y = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_G208M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_G210M = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_B201Y = new System.Collections.Generic.List<object[]>();

        private object _TXT01_SDATE_Value;
        private object _CBH01_KBSABUN_Value;

        private string fsdata_FamilyList = string.Empty; //부양가족 주민번호 리스트                

        #region  Description : 폼 로드 이벤트
        public TYHRNT001B(string sCOMPY, string sYear, string sKBSABUN, string sFixGubn, string sPopGubn)
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

        private void TYHRNT001B_Load(object sender, System.EventArgs e)
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

            //string sXml = string.Empty;

            //if (ftAttachTable.Rows.Count > 0)
            //{
            //    //첨부파일 삭제
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_HR_76G9A826", fsCOMPY,
            //                                               TXT01_SDATE.GetValue().ToString(),
            //                                               this.CBH01_KBSABUN.GetValue()

            //     );
            //    this.DbConnector.ExecuteTranQuery();

            //    //첨부파일 등록                
            //    for (int i = 0; i < ftAttachTable.Rows.Count; i++)
            //    {
            //        DAT30_YACOMPANY.SetValue(ftAttachTable.Rows[i]["YACOMPANY"].ToString());
            //        DAT30_YAYEAR.SetValue(ftAttachTable.Rows[i]["YAYEAR"].ToString());
            //        DAT30_YASABUN.SetValue(ftAttachTable.Rows[i]["YASABUN"].ToString());
            //        DAT30_YASEQ.SetValue(ftAttachTable.Rows[i]["YASEQ"].ToString());
            //        DAT30_YADESC.SetValue(ftAttachTable.Rows[i]["YADESC"].ToString());
            //        DAT30_YAFILENAME.SetValue(ftAttachTable.Rows[i]["YAFILENAME"].ToString());
            //        DAT30_YAFILESIZE.SetValue(ftAttachTable.Rows[i]["YAFILESIZE"].ToString());
            //        DAT30_YAFILEBYTE.SetValue(ftAttachTable.Rows[i]["YAFILEBYTE"]);
            //        DAT30_YAHISAB.SetValue(ftAttachTable.Rows[i]["YAHISAB"].ToString());

            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_76G94825", this.ControlFactory, "30");
            //        this.DbConnector.ExecuteTranQuery();
            //    }

            //    for (int i = 0; i < ftAttachTable.Rows.Count; i++)
            //    {
            //        UP_Struct_Clear();

            //        sXml = UP_Get_ConvertToXml(ftAttachTable.Rows[i]["YAFILENAME"].ToString(), "");

            //        //Xml 파싱
            //        UP_Set_XmlToParsing(sXml);

            //        this.UP_NTS_DataProcess();
            //    }
            //}           

            //this.ShowMessage("TY_M_MR_2BF50354");

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

                UP_DataList_Clear();

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

        #region  Description : 연말정산 영수증 자료 DataBase 처리 함수
        private void UP_NTS_DataProcess(TButton.ClickEventCheckArgs e)
        {
            //삭제
            #region  Description : 연말정산 공제신고서 자료 삭제 처리
                       
            //공제신고서 마스타
            if (data_A101Y.Count > 0)
            {
                for (int i = 0; i < data_A101Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76QBM945", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                             data_A101Y[i][3].ToString(), data_A101Y[i][4].ToString());  //공제신고서(A101Y)
                }
            }
            //공제신고서 인적공제
            if (data_A101M.Count > 0)
            {
                for (int i = 0; i < data_A101M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76QD1950", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                             data_A101M[i][3].ToString(), data_A101M[i][4].ToString());  //공제신고서 인적공제(A101M)
                }
            }

            //연금저축등 소득.세액 공제명세 마스타
            if (data_Doc_B101Y.Count > 0)
            {
                for (int i = 0; i < data_Doc_B101Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76SD5972", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                             data_Doc_B101Y[i][3].ToString(), data_Doc_B101Y[i][4].ToString(), data_Doc_B101Y[i][5].ToString()); 
                }
            }
            //연금저축등 소득.세액 공제명세(퇴직연금)
            if (data_Doc_B101R.Count > 0)
            {
                for (int i = 0; i < data_Doc_B101R.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76S9S968", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                             data_Doc_B101R[i][3].ToString(), data_Doc_B101R[i][4].ToString(), data_Doc_B101R[i][5].ToString());
                }
            }

            //연금저축등 소득.세액 공제명세(연금저축)
            if (data_Doc_B101P.Count > 0)
            {
                for (int i = 0; i < data_Doc_B101P.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76S9S968", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                             data_Doc_B101P[i][3].ToString(), data_Doc_B101P[i][4].ToString(), data_Doc_B101P[i][5].ToString()); 
                }
            }
            //연금저축등 소득.세액 공제명세(주택마련저축)
            if (data_Doc_B101H.Count > 0)
            {
                for (int i = 0; i < data_Doc_B101H.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76S9S970", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                             data_Doc_B101H[i][3].ToString(), data_Doc_B101H[i][4].ToString(), data_Doc_B101H[i][5].ToString()); 
                }
            }
            //연금저축등 소득.세액 공제명세(장기집합투자)
            if (data_Doc_B101L.Count > 0)
            {
                for (int i = 0; i < data_Doc_B101L.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76SD0971", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                             data_Doc_B101L[i][3].ToString(), data_Doc_B101L[i][4].ToString(), data_Doc_B101L[i][5].ToString()); 
                }
            }


            #endregion

            #region  Description : 연말정산 영수증 자료 삭제 처리
            //e.DbConnector.CommandClear();

            
            if (data_A102Y.Count > 0)
            {
                for( int i = 0; i < data_A102Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76JEJ860", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                                data_A102Y[i][3].ToString(), data_A102Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_A102Y[i][5].ToString());  //보험료(A102Y)
                }
            }
            if (data_A102M.Count > 0)
            {
                for (int i = 0; i < data_A102M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76JFA862", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                                data_A102M[i][3].ToString(), data_A102M[i][4].ToString(), data_A102M[i][5].ToString());  //보험료(A102M)
                }
            }
            if (data_B101Y.Count > 0)
            {
                for (int i = 0; i < data_B101Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76JFU864", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                                data_B101Y[i][3].ToString(), data_B101Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_B101Y[i][5].ToString());  //의료비(B101Y)
                }
            }
            if (data_B101D.Count > 0)
            {
                for (int i = 0; i < data_B101D.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76JG4865", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                               data_B101D[i][3].ToString(), data_B101D[i][4].ToString(), data_B101D[i][5].ToString());  //의료비(B101D)
                }
            }
            if (data_C102Y.Count > 0)
            {
                for (int i = 0; i < data_C102Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76JGK868", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                               data_C102Y[i][3].ToString(), data_C102Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_C102Y[i][5].ToString());  //교육비(C102Y)
                }
            }
            if (data_C102M.Count > 0)
            {
                for (int i = 0; i < data_C102M.Count; i++)
                {

                    e.DbConnector.Attach("TY_P_HR_76JGX869", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                                data_C102M[i][3].ToString(), data_C102M[i][4].ToString(), data_C102M[i][5].ToString());  //교육비(C102M)
                }
            }
            if (data_C202Y.Count > 0)
            {
                for (int i = 0; i < data_C202Y.Count; i++)
                {

                    e.DbConnector.Attach("TY_P_HR_76JHB872", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                                data_C202Y[i][3].ToString(), data_C202Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_C202Y[i][5].ToString());    //교육비(C202Y)_직업훈련비
                }
            }
            if (data_C202M.Count > 0)
            {
                for (int i = 0; i < data_C202M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76JHB873", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                               data_C202M[i][3].ToString(), data_C202M[i][4].ToString(), data_C202M[i][5].ToString());  //교육비(C202M)_직업훈련비 상세내역
                }
            }
            if (data_C301Y.Count > 0)
            {
                for (int i = 0; i < data_C301Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76K84877", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                           data_C301Y[i][3].ToString(), data_C301Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_C301Y[i][5].ToString());    //교육비(C301Y)_교복구입비
                }
            }
            if (data_C301M.Count > 0)
            {
                for (int i = 0; i < data_C301M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76K84878", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                               data_C301M[i][3].ToString(), data_C301M[i][4].ToString(), data_C301M[i][5].ToString());  //교육비(C301M)_교복구입비 상세내역
                }
            }

            if (data_C401Y.Count > 0)
            {
                for (int i = 0; i < data_C401Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_7C5AC172", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                               data_C401Y[i][3].ToString(), data_C401Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_C401Y[i][5].ToString());  //교육비_학자금대출(C401Y)
                }
            }
            if (data_C401M.Count > 0)
            {
                for (int i = 0; i < data_C401M.Count; i++)
                {

                    e.DbConnector.Attach("TY_P_HR_7C5AD173", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                                data_C401M[i][3].ToString(), data_C401M[i][4].ToString(), data_C401M[i][5].ToString());  //교육비_학자금대출(C401M)
                }
            }

            if (data_D101Y.Count > 0)
            {
                for (int i = 0; i < data_D101Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76K8Q881", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_D101Y[i][3].ToString(), data_D101Y[i][4].ToString(), TYUserInfo.SecureKey, "Y",  data_D101Y[i][5].ToString());  //개인연금저축(D101Y)
                }
            }
            if (data_D101M.Count > 0)
            {
                for (int i = 0; i < data_D101M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76K8Q882", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                    data_D101M[i][3].ToString(), data_D101M[i][4].ToString(), data_D101M[i][5].ToString()); //개인연금저축(D101M)_ 상세내역
                }
            }
            if (data_E102Y.Count > 0)
            {
                for (int i = 0; i < data_E102Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76K98885", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                            data_E102Y[i][3].ToString(), data_E102Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_E102Y[i][5].ToString());  //연금저축(E102Y)
                }
            }
            if (data_F102Y.Count > 0)
            {
                for (int i = 0; i < data_F102Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KA2890", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_F102Y[i][3].ToString(), data_F102Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_F102Y[i][5].ToString()); //퇴직연금(F102Y)
                }
            }
            if (data_G106Y.Count > 0)
            {
                for (int i = 0; i < data_G106Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KD2892", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_G106Y[i][3].ToString(), data_G106Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_G106Y[i][5].ToString());//신용카드+직불카드(G106Y)
                }
            }
            if (data_G106M.Count > 0)
            {
                for (int i = 0; i < data_G106M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KD3893", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_G106M[i][3].ToString(), data_G106M[i][4].ToString(), data_G106M[i][5].ToString());//신용카드+직불카드상세내역(G106M)
                }
            }
            if (data_G206M.Count > 0)
            {
                for (int i = 0; i < data_G206M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KFA895", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_G206M[i][3].ToString(), data_G206M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_G206M[i][5].ToString()); //현금영수증상세내역(G206M)
                }
            }
            if (data_J101Y.Count > 0)
            {
                for (int i = 0; i < data_J101Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KFT898", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_J101Y[i][3].ToString(), data_J101Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_J101Y[i][5].ToString());//주택임차차입금 원리금상환액(J101Y)
                }
            }
            if (data_J101M.Count > 0)
            {
                for (int i = 0; i < data_J101M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KFT899", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_J101M[i][3].ToString(), data_J101M[i][4].ToString(), data_J101M[i][5].ToString());//주택임차차입금 원리금상환액 상세내역 (J101M)
                }
            }
            if (data_J203Y.Count > 0)
            {
                for (int i = 0; i < data_J203Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KH1904", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                 data_J203Y[i][3].ToString(), data_J203Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_J203Y[i][5].ToString()); //장기주택저당차입금 이자상환액(J203Y)
                }
            }
            if (data_J203M.Count > 0)
            {
                for (int i = 0; i < data_J203M.Count; i++)
                {

                    e.DbConnector.Attach("TY_P_HR_76KH2905", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_J203M[i][3].ToString(), data_J203M[i][4].ToString(), data_J203M[i][5].ToString()); //장기주택저당차입금 이자상환액 상세내역 (J203M)
                }
            }
            if (data_J301Y.Count > 0)
            {
                for (int i = 0; i < data_J301Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KHS910", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_J301Y[i][3].ToString(), data_J301Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_J301Y[i][5].ToString()); //주택마련저축(J301Y)
                }
            }
            if (data_J301M.Count > 0)
            {
                for (int i = 0; i < data_J301M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KHS909", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_J301M[i][3].ToString(), data_J301M[i][4].ToString(), data_J301M[i][5].ToString());//주택마련저축 상세내역 (J301M)
                }
            }
            if (data_J401Y.Count > 0)
            {
                for (int i = 0; i < data_J401Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KI0912", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_J401Y[i][3].ToString(), data_J401Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_J401Y[i][5].ToString()); //목돈 안드는 전세 이자상환액(J401Y)
                }
            }
            if (data_J401M.Count > 0)
            {
                for (int i = 0; i < data_J401M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76KI1913", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_J401M[i][3].ToString(), data_J401M[i][4].ToString(), data_J401M[i][5].ToString()); //목돈 안드는 전세 이자상환액 상세내역 (J401M)
                }
            }
            if (data_K101M.Count > 0)
            {
                for (int i = 0; i < data_K101M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76LA3916", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_K101M[i][3].ToString(), data_K101M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_K101M[i][5].ToString());  //소기업소상공인 공제부금 상세내역 (K101M)
                }
            }
            if (data_L102Y.Count > 0)
            {
                for (int i = 0; i < data_L102Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76LAQ918", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_L102Y[i][3].ToString(), data_L102Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_L102Y[i][5].ToString()); //기부금(L102Y)
                }
            }
            if (data_L102D.Count > 0)
            {
                for (int i = 0; i < data_L102D.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76LAS920", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_L102D[i][3].ToString(), data_L102D[i][4].ToString(), data_L102D[i][5].ToString()); //기부금 상세내역 (L102D)
                }
            }
            if (data_N101Y.Count > 0)
            {
                for (int i = 0; i < data_N101Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76LBK925", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_N101Y[i][3].ToString(), data_N101Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_N101Y[i][5].ToString());   //장기집합투자증권저축(N101Y)
                }
            }
            if (data_N101M.Count > 0)
            {
                for (int i = 0; i < data_N101M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76LBM927", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_N101M[i][3].ToString(), data_N101M[i][4].ToString(), data_N101M[i][5].ToString());   //장기집합투자증권저축 상세내역 (N101M)
                }
            }
            if (data_O101M.Count > 0)
            {
                for (int i = 0; i < data_O101M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76GHW852", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_O101M[i][3].ToString(), data_O101M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_O101M[i][5].ToString());  //건강보험료(O101M)
                }
            }
            if (data_P102M.Count > 0)
            {
                for (int i = 0; i < data_P102M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_76GHX853", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_P102M[i][3].ToString(), data_P102M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_P102M[i][5].ToString());    //국민연금(P102M)
                }
            }

            if (data_G108Y.Count > 0)
            {
                for (int i = 0; i < data_G108Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_ACMBN213", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_G108Y[i][3].ToString(), data_G108Y[i][4].ToString(), data_G108Y[i][5].ToString());//신용카드+직불카드(G108Y)
                }
            }
            if (data_G108M.Count > 0)
            {
                for (int i = 0; i < data_G108M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_ACMBO214", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_G108M[i][3].ToString(), data_G108M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_G108M[i][5].ToString());//신용카드+직불카드상세내역(G108M)
                }
            }
            
            if (data_G110Y.Count > 0)
            {
                for (int i = 0; i < data_G110Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_BCUE9975", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_G110Y[i][3].ToString(), data_G110Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_G110Y[i][5].ToString());//신용카드+직불카드상세내역(G109Y)
                }
            }

            //if (data_G110M.Count > 0)
            //{
            //    for (int i = 0; i < data_G110M.Count; i++)
            //    {
            //        e.DbConnector.Attach("TY_P_HR_BCSBX950", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
            //             data_G110M[i][3].ToString(), data_G110M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_G110M[i][5].ToString());//신용카드+직불카드상세내역(G109M)
            //    }
            //}


            if (data_G208M.Count > 0)
            {
                for (int i = 0; i < data_G208M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_ACMDZ220", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_G208M[i][3].ToString(), data_G208M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_G208M[i][5].ToString());//현금영수증상세내역(G208M)
                }
            }

            if (data_G210M.Count > 0)
            {
                for (int i = 0; i < data_G210M.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_BCSC2952", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                         data_G210M[i][3].ToString(), data_G210M[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_G210M[i][5].ToString());//현금영수증상세내역(G209M)
                }
            }

            if (data_J501Y.Count > 0)
            {
                for (int i = 0; i < data_J501Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_ACMBQ215", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                        data_J501Y[i][3].ToString(), data_J501Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_J501Y[i][5].ToString()); //월세액(J501Y)
                }
            }

            if (data_B201Y.Count > 0)
            {
                for (int i = 0; i < data_B201Y.Count; i++)
                {
                    e.DbConnector.Attach("TY_P_HR_ACO8P235", fsCOMPY, this._TXT01_SDATE_Value, this._CBH01_KBSABUN_Value,
                                                                data_B201Y[i][3].ToString(), data_B201Y[i][4].ToString(), TYUserInfo.SecureKey, "Y", data_B201Y[i][5].ToString());  //실손보험(B201Y)
                }
            }

            //e.DbConnector.ExecuteTranQueryList();
            #endregion


            #region  Description : 연말정산 공제신고서 자료 처리

            if (data_A101Y.Count > 0)
            {
                foreach (object[] data in data_A101Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76QBN946", data);  //공제신고서 마스타(A101Y)
                }
            }

            if (data_A101M.Count > 0)
            {
                foreach (object[] data in data_A101M)
                {
                    e.DbConnector.Attach("TY_P_HR_76QD9949", data);  //공제신고서 인적공제(A101M)
                }
            }

             //연금저축등 소득.세액 공제명세 마스타
            if (data_Doc_B101Y.Count > 0)
            {
                foreach (object[] data in data_Doc_B101Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76SD0973", data);
                }
            }
            //연금저축등 소득.세액 공제명세(퇴직연금)
            if (data_Doc_B101R.Count > 0)
            {
                foreach (object[] data in data_Doc_B101R)
                {
                    e.DbConnector.Attach("TY_P_HR_76SD1974", data);
                }
            }

            //연금저축등 소득.세액 공제명세(연금저축)
            if (data_Doc_B101P.Count > 0)
            {
                foreach (object[] data in data_Doc_B101P)
                {
                    e.DbConnector.Attach("TY_P_HR_76SD3975", data);
                }
            }
            //연금저축등 소득.세액 공제명세(주택마련저축)
            if (data_Doc_B101H.Count > 0)
            {
                foreach (object[] data in data_Doc_B101H)
                {
                    e.DbConnector.Attach("TY_P_HR_76SD5976", data);
                }
            }
            //연금저축등 소득.세액 공제명세(장기집합투자)
            if (data_Doc_B101L.Count > 0)
            {
                foreach (object[] data in data_Doc_B101L)
                {
                    e.DbConnector.Attach("TY_P_HR_76SDA977", data);
                }
            }

            #endregion

            #region  Description : 연말정산 영수증 자료 등록 처리
            //e.DbConnector.CommandClear();                      

            if (data_A102Y.Count > 0)
            {
                foreach (object[] data in data_A102Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76JE0859", data);  //보험료(A102Y)
                }
            }

            if (data_A102M.Count > 0)
            {
                foreach (object[] data in data_A102M)
                {
                    e.DbConnector.Attach("TY_P_HR_76JFA861", data);  //보험료(A102M)_상세내역
                }
            }

            if (data_B101Y.Count > 0)
            {
                foreach (object[] data in data_B101Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76JFT863", data);  //의료비(B101Y)
                }
            }

            if (data_B101D.Count > 0)
            {
                foreach (object[] data in data_B101D)
                {
                    e.DbConnector.Attach("TY_P_HR_76JG5866", data);  //의료비(B101D)_상세내역
                }
            }

            if (data_C102Y.Count > 0)
            {
                foreach (object[] data in data_C102Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76JGK867", data);  //교육비(C102Y)
                }
            }

            if (data_C102M.Count > 0)
            {
                foreach (object[] data in data_C102M)
                {
                    e.DbConnector.Attach("TY_P_HR_76JGY870", data);  //교육비(C102M)_상세내역
                }
            }

            if (data_C202Y.Count > 0)
            {
                foreach (object[] data in data_C202Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76JHB874", data);  //교육비(C202Y)_직업훈련비
                }
            }

            if (data_C202M.Count > 0)
            {
                foreach (object[] data in data_C202M)
                {
                    e.DbConnector.Attach("TY_P_HR_76JHD875", data);  //교육비(C202M)_직업훈련비_상세내역
                }
            }

            if (data_C301Y.Count > 0)
            {
                foreach (object[] data in data_C301Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76K83876", data);  //교육비(C301Y)_교복구입비
                }
            }

            if (data_C301M.Count > 0)
            {
                foreach (object[] data in data_C301M)
                {
                    e.DbConnector.Attach("TY_P_HR_76K8A879", data);  //교육비(C301M)_교복구입비상세내역
                }
            }

            if (data_C401Y.Count > 0)
            {
                foreach (object[] data in data_C401Y)
                {
                    e.DbConnector.Attach("TY_P_HR_7C5AE174", data);  //교육비_학자금대출(C401Y)
                }
            }

            if (data_C401M.Count > 0)
            {
                foreach (object[] data in data_C401M)
                {
                    e.DbConnector.Attach("TY_P_HR_7C5AF175", data);  //교육비_학자금대출(C401M)_상세내역
                }
            }

            if (data_D101Y.Count > 0)
            {
                foreach (object[] data in data_D101Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76K8Q880", data);  //개인연금저축
                }
            }

            if (data_D101M.Count > 0)
            {
                foreach (object[] data in data_D101M)
                {
                    e.DbConnector.Attach("TY_P_HR_76K8R883", data);  //개인연금저축_상세내역
                }
            }

            if (data_E102Y.Count > 0)
            {
                foreach (object[] data in data_E102Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76K98884", data);  //연금저축 + 상세내역
                }
            }

            if (data_F102Y.Count > 0)
            {
                foreach (object[] data in data_F102Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76KA1889", data);  //퇴직연금 + 상세내역
                }
            }

            if (data_G106Y.Count > 0)
            {
                foreach (object[] data in data_G106Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76KD2891", data);  //신용카드+직불카드
                }
            }

            if (data_G106M.Count > 0)
            {
                foreach (object[] data in data_G106M)
                {
                    e.DbConnector.Attach("TY_P_HR_76KD4894", data);  //신용카드+직불카드_상세내역
                }
            }

            if (data_G206M.Count > 0)
            {
                foreach (object[] data in data_G206M)
                {
                    e.DbConnector.Attach("TY_P_HR_76KFB896", data);  //현금영수증_상세내역
                }
            }

            if (data_J101Y.Count > 0)
            {
                foreach (object[] data in data_J101Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76KFT897", data);  //주택임차차입금 원리금상환액
                }
            }

            if (data_J101M.Count > 0)
            {
                foreach (object[] data in data_J101M)
                {
                    e.DbConnector.Attach("TY_P_HR_76KFU900", data);  //주택임차차입금 원리금상환액_상세내역
                }
            }

            if (data_J203Y.Count > 0)
            {
                foreach (object[] data in data_J203Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76KH0903", data);  //장기주택저당차입금 이자상환액
                }
            }

            if (data_J203M.Count > 0)
            {
                foreach (object[] data in data_J203M)
                {
                    e.DbConnector.Attach("TY_P_HR_76KH2906", data);  //장기주택저당차입금 이자상환액_상세내역
                }
            }

            if (data_J301Y.Count > 0)
            {
                foreach (object[] data in data_J301Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76KHQ907", data);  //주택마련저축
                }
            }

            if (data_J301M.Count > 0)
            {
                foreach (object[] data in data_J301M)
                {
                    e.DbConnector.Attach("TY_P_HR_76KHR908", data);  //주택마련저축_상세내역
                }
            }

            if (data_J401Y.Count > 0)
            {
                foreach (object[] data in data_J401Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76KI0911", data);  //목돈 안드는 전세 이자상환액
                }
            }

            if (data_J401M.Count > 0)
            {
                foreach (object[] data in data_J401M)
                {
                    e.DbConnector.Attach("TY_P_HR_76KI1914", data);  //목돈 안드는 전세 이자상환액_상세내역
                }
            }


            if (data_K101M.Count > 0)
            {
                foreach (object[] data in data_K101M)
                {
                    e.DbConnector.Attach("TY_P_HR_76LA2915", data);  //소기업소상공인 공제부금_상세내역
                }
            }

            if (data_L102Y.Count > 0)
            {
                foreach (object[] data in data_L102Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76LAQ917", data);  //기부금
                }
            }

            if (data_L102D.Count > 0)
            {
                foreach (object[] data in data_L102D)
                {
                    e.DbConnector.Attach("TY_P_HR_76LAS919", data);  //기부금 상세내역
                }
            }

            if (data_N101Y.Count > 0)
            {
                foreach (object[] data in data_N101Y)
                {
                    e.DbConnector.Attach("TY_P_HR_76LBK924", data);  //장기집합투자증권저축
                }
            }

            if (data_N101M.Count > 0)
            {
                foreach (object[] data in data_N101M)
                {
                    e.DbConnector.Attach("TY_P_HR_76LBK926", data);  //장기집합투자증권저축 상세내역
                }
            }

            if (data_O101M.Count > 0)
            {
                foreach (object[] data in data_O101M)
                {
                    e.DbConnector.Attach("TY_P_HR_76GEN843", data);  //건강보험료(O101M)
                }
            }

            if (data_P102M.Count > 0)
            {

                foreach (object[] data in data_P102M)
                {
                    e.DbConnector.Attach("TY_P_HR_76GF4844", data);  //국민연금(P102M)
                }
            }

            if (data_G108Y.Count > 0)
            {
                foreach (object[] data in data_G108Y)
                {
                    e.DbConnector.Attach("TY_P_HR_ACMDV217", data);  //신용카드+직불카드(2020년)
                }
            }

            if (data_G108M.Count > 0)
            {
                foreach (object[] data in data_G108M)
                {
                    e.DbConnector.Attach("TY_P_HR_ACMDW218", data);  //신용카드+직불카드_상세내역(2020년)
                }
            }

            if (data_G110Y.Count > 0)
            {
                foreach (object[] data in data_G110Y)
                {
                    e.DbConnector.Attach("TY_P_HR_BCUE1976", data);  //신용카드+직불카드(2021,2022년)
                }
            }

            /*
            //if (data_G110M.Count > 0)
            //{
            //    foreach (object[] data in data_G110M)
            //    {
            //        e.DbConnector.Attach("TY_P_HR_BCSC0951", data);  //신용카드+직불카드_상세내역(2021년)
            //    }
            //}

            //if (data_G208M.Count > 0)
            //{
            //    foreach (object[] data in data_G208M)
            //    {
            //        e.DbConnector.Attach("TY_P_HR_ACMDW219", data);  //현금영수증_상세내역(2020년)
            //    }
            //}
            */

            if (data_G210M.Count > 0)
            {
                foreach (object[] data in data_G210M)
                {
                    e.DbConnector.Attach("TY_P_HR_BCSD3953", data);  //현금영수증_상세내역(2021, 2022년)
                }
            }

            if (data_J501Y.Count > 0)
            {
                foreach (object[] data in data_J501Y)
                {
                    e.DbConnector.Attach("TY_P_HR_ACMDV216", data);  //월세액(2020년)
                }
            }

            if (data_B201Y.Count > 0)
            {
                foreach (object[] data in data_B201Y)
                {
                    e.DbConnector.Attach("TY_P_HR_ACO8S236", data);  //실손보험(B201Y)
                }
            }

            //if( e.DbConnector.CommandCount > 0 )
            //  e.DbConnector.ExecuteTranQueryList();
            #endregion
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

        #region  Description : XML 파싱 함수
        private void UP_Set_XmlToParsing(string sXml)
        {
            string sform_cd = string.Empty;

            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sXml));
            // XML을 parse하기 위해 XmlDocument를 사용
            XmlDocument xmlDoc = new XmlDocument();

            // MemoryStream을 이용하여 XML문을 읽는다.
            xmlDoc.Load(ms);

            XmlElement CDoc = xmlDoc.DocumentElement;

            if (CDoc.ChildNodes.Count > 0)
            {
                for (int i = 0; i < CDoc.ChildNodes.Count; i++)
                {
                    if (CDoc.ChildNodes[i].Name == "form")
                    {
                        //서식코드 인식
                        sform_cd = CDoc.ChildNodes[i].Attributes["form_cd"].Value;

                        XmlNode CtrlNode = GetSearchNode(xmlDoc, "form", "form_cd", sform_cd);

                        struct_Main.form_cd = sform_cd;

                        UP_Set_AttributesValue(struct_Main.form_cd, "form.form_cd", struct_Main.form_cd);

                        if (struct_Main.form_cd.Substring(struct_Main.form_cd.Length - 1, 1) == "Y" || struct_Main.form_cd == "E102M" || struct_Main.form_cd == "F102M" || struct_Main.form_cd == "E103M" || struct_Main.form_cd == "F103M")
                        {

                            if (struct_Main.form_cd == "A101Y") //공제신고서
                            {
                                fbGonJeDoc = true;

                                UP_SubNode_Doc_Master(CtrlNode, sform_cd);
                            }
                            else
                            {
                                if (fbGonJeDoc)
                                {
                                    //공제신고서 내역
                                    UP_SubNode_Doc_Detail(CtrlNode, sform_cd);
                                }
                                else
                                {
                                    //연말정산 영수증
                                    UP_SubNode_Process_Y(CtrlNode, struct_Main.form_cd);
                                }
                            }
                        }
                        else
                        {
                            //연말정산 영수증
                            UP_SubNode_Process_M(CtrlNode, struct_Main.form_cd);
                        }
                    }                    
                }
            }
           
        }


        //공제신고서 마스타
        private void UP_SubNode_Doc_Master(XmlNode CtrlNode, string sform_cd)
        {
            string sAttName = string.Empty;
            string sAttValue = string.Empty;
            string sValue = string.Empty;
            string sName = string.Empty;

            if (CtrlNode.ChildNodes.Count > 0)
            {
                for (int i = 0; i < CtrlNode.ChildNodes.Count; i++)
                {
                    sName = CtrlNode.ChildNodes[i].Name;

                    string dddd = CtrlNode.ChildNodes[i].InnerXml;

                    if (CtrlNode.ChildNodes[i].Attributes.Count > 0 && CtrlNode.ChildNodes[i].Attributes != null)
                    {
                        for (int j = 0; j < CtrlNode.ChildNodes[i].Attributes.Count; j++)
                        {
                            sAttName = CtrlNode.ChildNodes[i].Attributes[j].Name;
                            sAttValue = CtrlNode.ChildNodes[i].Attributes[j].Value;

                            UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sAttName, sAttValue);

                            if (j == CtrlNode.ChildNodes[i].Attributes.Count - 1 && CtrlNode.ChildNodes[i].ParentNode.Name == "man")
                            {
                                //공제신고서 인적공제
                                //관계코드(suptFmlyRltClCd) 속성이 존재하면 인적공제 테이블에 Add 한다
                                if (UP_AttributeNameSearch(CtrlNode.ChildNodes[i], "suptFmlyRltClCd"))
                                {
                                    struct_A101Y.data_seq = (i + 1).ToString();
                                    UP_Set_StructToData("A101M");
                                }
                            }
                        }

                        if (CtrlNode.ChildNodes[i].Attributes.Count <= 0)
                        {
                            sAttValue = CtrlNode.ChildNodes[i].InnerText;

                            UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                        }
                        else
                        {
                            if (sName == "sum")
                            {
                                sAttValue = CtrlNode.ChildNodes[i].InnerText;

                                UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                            }
                        }
                    }
                    else
                    {
                        sAttValue = CtrlNode.ChildNodes[i].InnerText;

                        UP_Set_AttributesValue(struct_Main.form_cd, CtrlNode.ChildNodes[i].ParentNode.Name + "." + sName, sAttValue);
                    }

                    if (CtrlNode.ChildNodes[i].HasChildNodes && (sName == "man" || sName == "data"))
                    {
                        UP_SubNode_Doc_Master(CtrlNode.ChildNodes[i], sName);
                    }

                    if (i == CtrlNode.ChildNodes.Count - 1 && CtrlNode.ChildNodes[i].ParentNode.Name == "man")
                    {
                        UP_Set_StructToData(struct_Main.form_cd);
                    }
                }
            }

        }

        //공제신고서 내역
        private void UP_SubNode_Doc_Detail(XmlNode CtrlNode, string sform_cd)
        {
            string sAttName = string.Empty;
            string sAttValue = string.Empty;
            string sValue = string.Empty;
            string sName = string.Empty;

            if (CtrlNode.ChildNodes.Count > 0)
            {
                for (int i = 0; i < CtrlNode.ChildNodes.Count; i++)
                {
                    sName = CtrlNode.ChildNodes[i].Name;

                    string dddd = CtrlNode.ChildNodes[i].InnerXml;

                    if (CtrlNode.ChildNodes[i].Attributes.Count > 0 && CtrlNode.ChildNodes[i].Attributes != null)
                    {
                        for (int j = 0; j < CtrlNode.ChildNodes[i].Attributes.Count; j++)
                        {
                            sAttName = CtrlNode.ChildNodes[i].Attributes[j].Name;
                            sAttValue = CtrlNode.ChildNodes[i].Attributes[j].Value;

                            UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sAttName, sAttValue);
                        }

                        if (CtrlNode.ChildNodes[i].Attributes.Count <= 0)
                        {
                            sAttValue = CtrlNode.ChildNodes[i].InnerText;

                            UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                        }
                        else
                        {
                            if (sName == "sum")
                            {
                                sAttValue = CtrlNode.ChildNodes[i].InnerText;

                                UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                            }
                        }
                    }
                    else
                    {
                        sAttValue = CtrlNode.ChildNodes[i].InnerText;

                        UP_Set_AttributesValue(struct_Main.form_cd, CtrlNode.ChildNodes[i].ParentNode.Name + "." + sName, sAttValue);
                    }

                    if (CtrlNode.ChildNodes[i].HasChildNodes && (sName == "man" || sName == "data"))
                    {
                        UP_Set_StructToData(struct_Main.form_cd);

                        UP_SubNode_Doc_Detail(CtrlNode.ChildNodes[i], sName);
                    }

                    if (CtrlNode.ChildNodes[i].HasChildNodes == false && sName == "data")
                    {
                        string sNodeform_cd = struct_Main.form_cd;

                        //B101Y-연금저축등 소득.세액 공제명세
                        if (struct_Main.form_cd == "B101Y")
                        {
                            //퇴직연금
                            if (UP_AttributeNameSearch(CtrlNode.ChildNodes[i], "rtpnAccRtpnCl"))
                            {
                                sNodeform_cd = struct_Main.form_cd.Substring(0, 4) + "R";
                            }
                            //연금저축
                            if (UP_AttributeNameSearch(CtrlNode.ChildNodes[i], "pnsnSvngAccPnsnSvngCl"))
                            {
                                sNodeform_cd = struct_Main.form_cd.Substring(0, 4) + "P";
                            }
                            //주택마련
                            if (UP_AttributeNameSearch(CtrlNode.ChildNodes[i], "hsngPrptSvngSvngCl"))
                            {
                                sNodeform_cd = struct_Main.form_cd.Substring(0, 4) + "H";
                            }
                            //장기집합투자
                            if (UP_AttributeNameSearch(CtrlNode.ChildNodes[i], "ltrmCniSsfnnOrgnCd"))
                            {
                                sNodeform_cd = struct_Main.form_cd.Substring(0, 4) + "I";
                            }
                        }

                        UP_Set_StructToData(sNodeform_cd);
                    }

                }
            }

        }

        private bool UP_AttributeNameSearch(XmlNode CtrlNode, string sName)
        {
            bool result = false;

            for (int j = 0; j < CtrlNode.Attributes.Count; j++)
            {
                if (CtrlNode.Attributes[j].Name == sName)
                {
                    result = true;
                }
            }

            return result;
        }

        //연말정산 영수증 기본사항 
        private void UP_SubNode_Process_Y(XmlNode CtrlNode, string sform_cd)
        {
            string sAttName = string.Empty;
            string sAttValue = string.Empty;
            string sValue = string.Empty;
            string sName = string.Empty;

            if (CtrlNode.ChildNodes.Count > 0)
            {
                for (int i = 0; i < CtrlNode.ChildNodes.Count; i++)
                {
                        sName = CtrlNode.ChildNodes[i].Name;                        

                        if (CtrlNode.ChildNodes[i].Attributes.Count > 0 && CtrlNode.ChildNodes[i].Attributes != null)
                        {
                            for (int j = 0; j < CtrlNode.ChildNodes[i].Attributes.Count; j++)
                            {
                                sAttName = CtrlNode.ChildNodes[i].Attributes[j].Name;
                                sAttValue = CtrlNode.ChildNodes[i].Attributes[j].Value;

                                UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sAttName, sAttValue);
                            }

                            if (CtrlNode.ChildNodes[i].Attributes.Count <= 0)
                            {
                                sAttValue = CtrlNode.ChildNodes[i].InnerText;

                                UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                            }
                            else
                            {
                                if (sName == "sum")
                                {
                                    sAttValue = CtrlNode.ChildNodes[i].InnerText;

                                    UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                                }
                            }
                        }
                        else
                        {
                            sAttValue = CtrlNode.ChildNodes[i].InnerText;

                            UP_Set_AttributesValue(struct_Main.form_cd,  CtrlNode.ChildNodes[i].ParentNode.Name + "." + sName, sAttValue);
                        }

                        if (CtrlNode.ChildNodes[i].HasChildNodes && (sName == "man" || sName == "data" || sName == "sum_data"))
                        {
                            UP_SubNode_Process_Y(CtrlNode.ChildNodes[i], sName);
                        }

                        if (i == CtrlNode.ChildNodes.Count - 1 && CtrlNode.ChildNodes[i].ParentNode.Name == "data")
                        {
                            UP_Set_StructToData(struct_Main.form_cd);
                        }
                }
            }

        }

        //연말정산 영수증 상세내역(월별내역)
        private void UP_SubNode_Process_M(XmlNode CtrlNode, string sform_cd)
        {
            string sAttName = string.Empty;
            string sAttValue = string.Empty;
            string sValue = string.Empty;
            string sName = string.Empty;

            if (CtrlNode.ChildNodes.Count > 0)
            {
                for (int i = 0; i < CtrlNode.ChildNodes.Count; i++)
                {
                    sName = CtrlNode.ChildNodes[i].Name;

                    string dddd = CtrlNode.ChildNodes[i].InnerXml;

                    if (CtrlNode.ChildNodes[i].Attributes.Count > 0 && CtrlNode.ChildNodes[i].Attributes != null)
                    {
                        for (int j = 0; j < CtrlNode.ChildNodes[i].Attributes.Count; j++)
                        {
                            sAttName = CtrlNode.ChildNodes[i].Attributes[j].Name;
                            sAttValue = CtrlNode.ChildNodes[i].Attributes[j].Value;

                            UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sAttName, sAttValue);
                        }

                        if (CtrlNode.ChildNodes[i].Attributes.Count <= 0)
                        {
                            sAttValue = CtrlNode.ChildNodes[i].InnerText;

                            UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                        }
                        else
                        {
                            if (sName == "amt" || sName == "sum")
                            {
                                sAttValue = CtrlNode.ChildNodes[i].InnerText;

                                UP_Set_AttributesValue(struct_Main.form_cd, sName + "." + sName, sAttValue);
                            }
                        }
                        if ( sName == "amt")
                        {
                            UP_Set_StructToData(struct_Main.form_cd);
                        }
                    }
                    else
                    {
                        sAttValue = CtrlNode.ChildNodes[i].InnerText;

                        UP_Set_AttributesValue(struct_Main.form_cd, CtrlNode.ChildNodes[i].ParentNode.Name + "." + sName, sAttValue);
                    }

                    if (CtrlNode.ChildNodes[i].HasChildNodes && (sName == "man" || sName == "data" || sName == "sum_data"))
                    {
                        UP_SubNode_Process_M(CtrlNode.ChildNodes[i], sName);
                    }                   
                }
            }

        }
        #endregion        

        #region  Description : 속성 값 저장 함수
        private void UP_Set_AttributesValue(string sform_cd, string AttName, string strValue)
        {
            strValue = strValue.Trim();
           
            switch (sform_cd)
            {
                #region  Description : 공제신고서 마스타
                case "A101Y":  //공제신고서 마스타
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_A101Y.man_form_cd = strValue;
                            break;
                        case "man.resnoEncCntn":
                            struct_A101Y.man_resnoEncCntn = strValue;
                            break;
                        case "man.fnm":
                            struct_A101Y.man_fnm = strValue;
                            break;
                        case "man.tnm":
                            struct_A101Y.man_tnm = strValue;
                            break;
                        case "man.bsnoEncCntn":
                            struct_A101Y.man_bsnoEncCntn = strValue;
                            break;
                        case "man.hshrClCd":
                            struct_A101Y.man_hshrClCd= strValue;
                            break;
                        case "man.rsplNtnInfr":
                            struct_A101Y.man_rsplNtnInfr= strValue;
                            break;
                        case "man.dtyStrtDt":
                            struct_A101Y.man_dtyStrtDt= strValue;
                            break;
                        case "man.dtyEndDt":
                            struct_A101Y.man_dtyEndDt = strValue;
                            break;
                        case "man.reStrtDt":
                            struct_A101Y.man_reStrtDt = strValue;
                            break;
                        case "man.reEndDt":
                            struct_A101Y.man_reEndDt = strValue;
                            break;
                        case "man.rsdtClCd":
                            struct_A101Y.man_rsdtClCd = strValue;
                            break;
                        case "man.inctxWhtxTxamtMetnCd":
                            struct_A101Y.man_inctxWhtxTxamtMetnCd = strValue;
                            break;
                        case "man.inpmYn":
                            struct_A101Y.man_inpmYn = strValue;
                            break;
                        case "man.prifChngYn":
                            struct_A101Y.man_prifChngYn = strValue;
                            break;
                        case "data.npHthrWaInfeeAmt":
                             struct_A101Y.data_npHthrWaInfeeAmt	    = strValue; 
                         break;
                        case "data.npHthrWaInfeeDdcAmt":
                             struct_A101Y.data_npHthrWaInfeeDdcAmt	    = strValue;
                         break;
                        case "data.npHthrMcurWkarInfeeAmt":
                             struct_A101Y.data_npHthrMcurWkarInfeeAmt    = strValue;	  
                         break;
                        case "data.npHthrMcurWkarDdcAmt":
                             struct_A101Y.data_npHthrMcurWkarDdcAmt	    = strValue;
                         break;
                        case "data.hthrPblcPnsnInfeeAmt":
                             struct_A101Y.data_hthrPblcPnsnInfeeAmt	    = strValue;
                         break;
                        case "data.hthrPblcPnsnInfeeDdcAmt":
                             struct_A101Y.data_hthrPblcPnsnInfeeDdcAmt   = strValue;	 
                         break;
                        case "data.mcurPblcPnsnInfeeAmt":
                             struct_A101Y.data_mcurPblcPnsnInfeeAmt	   = strValue;
                         break;
                        case "data.mcurPblcPnsnInfeeDdcAmt":
                             struct_A101Y.data_mcurPblcPnsnInfeeDdcAmt = strValue;	
                         break;
                        case "data.pnsnInfeeUseAmtSum":
                             struct_A101Y.data_pnsnInfeeUseAmtSum	  = strValue;    
                         break;
                        case "data.pnsnInfeeDdcAmtSum":
                             struct_A101Y.data_pnsnInfeeDdcAmtSum	  = strValue;    
                         break;

                        case "data.hthrHifeAmt":
                           struct_A101Y.data_hthrHifeAmt = strValue;         
                           break;
                        case "data.hthrHifeDdcAmt":
                           struct_A101Y.data_hthrHifeDdcAmt = strValue;      
                         break;
                        case "data.mcurHifeAmt":
                         struct_A101Y.data_mcurHifeAmt = strValue;    	
                         break;
                        case "data.mcurHifeDdcAmt":
                         struct_A101Y.data_mcurHifeDdcAmt = strValue;      
                         break;
                        case "data.hthrUiAmt":
                         struct_A101Y.data_hthrUiAmt = strValue;    	     
                         break;
                        case "data.hthrUiDdcAmt":
                         struct_A101Y.data_hthrUiDdcAmt = strValue;    	
                         break;
                        case "data.mcurUiAmt":
                         struct_A101Y.data_mcurUiAmt = strValue;    	     
                         break;
                        case "data.mcurUiDdcAmt":
                         struct_A101Y.data_mcurUiDdcAmt = strValue;    	
                         break;
                        case "data.infeeUseAmtSum":
                         struct_A101Y.data_infeeUseAmtSum = strValue;         
                         break;
                        case "data.infeeDdcAmtSum":
                         struct_A101Y.data_infeeDdcAmtSum = strValue;         
                         break;
                        case "data.brwOrgnBrwnHsngTennLnpbSrmAmt":
                         struct_A101Y.data_brwOrgnBrwnHsngTennLnpbSrmAmt = strValue;    	
                         break;
                        case "data.brwOrgnBrwnHsngTennLnpbSrmDdcAmt":
                         struct_A101Y.data_brwOrgnBrwnHsngTennLnpbSrmDdcAmt = strValue;    	
                         break;
                        case "data.rsdtBrwnHsngTennLnpbSrmAmt":
                         struct_A101Y.data_rsdtBrwnHsngTennLnpbSrmAmt = strValue;    	       
                         break;
                        case "data.rsdtBrwnHsngTennLnpbSrmDdcAmt":
                         struct_A101Y.data_rsdtBrwnHsngTennLnpbSrmDdcAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr15BlwItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr15BlwItrAmt = strValue;    	       
                         break;
                        case "data.lthClrlLnpbYr15BlwDdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr15BlwDdcAmt = strValue;           	
                         break;
                        case "data.lthClrlLnpbYr29ItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr29ItrAmt = strValue;              	
                         break;
                        case "data.lthClrlLnpbYr29DdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr29DdcAmt = strValue;                	
                         break;
                        case "data.lthClrlLnpbY30OverItrAmt":
                         struct_A101Y.data_lthClrlLnpbY30OverItrAmt = strValue;          	
                         break;
                        case "data.lthClrlLnpbY30OverDdcAmt":
                         struct_A101Y.data_lthClrlLnpbY30OverDdcAmt = strValue;         	
                         break;
                        case "data.lthClrlLnpbYr2012AfthY15OverItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr2012AfthY15OverItrAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2012AfthY15OverDdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr2012AfthY15OverDdcAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2012EtcBrwItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr2012EtcBrwItrAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2012EtcBrwDdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr2012EtcBrwDdcAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2015AfthFxnIrItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthFxnIrItrAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2015AfthFxnIrDdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthFxnIrDdcAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2015AfthY15OverItrAmtItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthY15OverItrAmtItrAmt = strValue;         
                         break;
                        case "data.lthClrlLnpbYr2015AfthY15OverDdcAmtDdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthY15OverDdcAmtDdcAmt = strValue;         
                         break;
                        case "data.lthClrlLnpbYr2015AfthEtcBrwItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthEtcBrwItrAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2015AfthEtcBrwDdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthEtcBrwDdcAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2015AfthYr15BlwItrAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthYr15BlwItrAmt = strValue;    	
                         break;
                        case "data.lthClrlLnpbYr2015AfthYr15BlwDdcAmt":
                         struct_A101Y.data_lthClrlLnpbYr2015AfthYr15BlwDdcAmt = strValue;    	
                         break;
                        case "data.hsngFndsDdcAmtSum":
                         struct_A101Y.data_hsngFndsDdcAmtSum = strValue;         	           
                         break;
                        case "data.conbCrfwAmtLglUseAmt":
                         struct_A101Y.data_conbCrfwAmtLglUseAmt = strValue;         	   
                         break;
                        case "data.conbCrfwAmtLglDdcAmt":
                         struct_A101Y.data_conbCrfwAmtLglDdcAmt = strValue;         	   
                         break;
                        case "data.conbCrfwAmtReliOrgOthUseAmt":
                         struct_A101Y.data_conbCrfwAmtReliOrgOthUseAmt = strValue;         	
                         break;
                        case "data.conbCrfwAmtReliOrgOthDdcAmt":
                         struct_A101Y.data_conbCrfwAmtReliOrgOthDdcAmt = strValue;         	
                         break;
                        case "data.conbCrfwAmtReliOrgUseAmt":
                         struct_A101Y.data_conbCrfwAmtReliOrgUseAmt = strValue;         	   
                         break;
                        case "data.conbCrfwAmtReliOrgDdcAmt":
                         struct_A101Y.data_conbCrfwAmtReliOrgDdcAmt = strValue;         	   
                         break;
                        case "data.conbCrfwAmtUseAmtSum":
                         struct_A101Y.data_conbCrfwAmtUseAmtSum = strValue;         	   
                         break;
                        case "data.conbCrfwAmtDdcAmtSum":
                         struct_A101Y.data_conbCrfwAmtDdcAmtSum = strValue;         	   
                         break;

                        case "data.yr2000BefNtplPnsnSvngUseAmt":
                         struct_A101Y.data_yr2000BefNtplPnsnSvngUseAmt = strValue; 
                         break;
                        case "data.yr2000BefNtplPnsnSvngDdcAmt":
                         struct_A101Y.data_yr2000BefNtplPnsnSvngDdcAmt = strValue; 
                         break;
                        case "data.smceSbizUseAmt":
                         struct_A101Y.data_smceSbizUseAmt = strValue;    	        
                         break;
                        case "data.smceSbizDdcAmt":
                         struct_A101Y.data_smceSbizDdcAmt = strValue;              
                         break;
                        case "data.sbcSvngUseAmt":
                         struct_A101Y.data_sbcSvngUseAmt = strValue;  	        
                         break;
                        case "data.sbcSvngDdcAmt":
                         struct_A101Y.data_sbcSvngDdcAmt = strValue;  	        
                         break;
                        case "data.lbrrHsngPrptSvngUseAmt":
                         struct_A101Y.data_lbrrHsngPrptSvngUseAmt = strValue; 	
                         break;
                        case "data.lbrrHsngPrptSvngDdcAmt":
                         struct_A101Y.data_lbrrHsngPrptSvngDdcAmt = strValue; 	
                         break;
                        case "data.hsngSbcSynSvngUseAmt":
                         struct_A101Y.data_hsngSbcSynSvngUseAmt = strValue;        
                         break;
                        case "data.hsngSbcSynSvngDdcAmt":
                         struct_A101Y.data_hsngSbcSynSvngDdcAmt = strValue;        
                         break;
                        case "data.hsngPrptSvngIncUseAmtSum":
                         struct_A101Y.data_hsngPrptSvngIncUseAmtSum = strValue; 
                         break;
                        case "data.hsngPrptSvngIncDdcAmtSum":
                         struct_A101Y.data_hsngPrptSvngIncDdcAmtSum = strValue; 
                         break;
                        case "data.cpivAsctUseAmt2":
                         struct_A101Y.data_cpivAsctUseAmt2 = strValue; 	
                         break;
                        case "data.cpivAsctDdcAmt2":
                         struct_A101Y.data_cpivAsctDdcAmt2 = strValue; 	
                         break;
                        case "data.cpivVntUseAmt2":
                         struct_A101Y.data_cpivVntUseAmt2 = strValue; 	
                         break;
                        case "data.cpivVntDdcAmt2":
                         struct_A101Y.data_cpivVntDdcAmt2 = strValue; 	
                         break;
                        case "data.cpivAsctUseAmt1":
                         struct_A101Y.data_cpivAsctUseAmt1 = strValue; 	
                         break;
                        case "data.cpivAsctDdcAmt1":
                         struct_A101Y.data_cpivAsctDdcAmt1 = strValue; 	
                         break;
                        case "data.cpivVntUseAmt1":
                         struct_A101Y.data_cpivVntUseAmt1 = strValue; 	
                         break;
                        case "data.cpivVntDdcAmt1":
                         struct_A101Y.data_cpivVntDdcAmt1 = strValue; 	
                         break;
                        case "data.cpivAsctUseAmt0":
                         struct_A101Y.data_cpivAsctUseAmt0 = strValue; 	
                         break;
                        case "data.cpivAsctDdcAmt0":
                         struct_A101Y.data_cpivAsctDdcAmt0 = strValue; 	
                         break;
                        case "data.cpivVntUseAmt0":
                         struct_A101Y.data_cpivVntUseAmt0 = strValue; 	
                         break;
                        case "data.cpivVntDdcAmt0":
                         struct_A101Y.data_cpivVntDdcAmt0 = strValue; 	
                         break;
                        case "data.ivcpInvmUseAmtSum":
                         struct_A101Y.data_ivcpInvmUseAmtSum = strValue; 	    
                         break;
                        case "data.ivcpInvmDdcAmtSum":
                         struct_A101Y.data_ivcpInvmDdcAmtSum = strValue; 	    
                         break;
                        case "data.crdcUseAmt":
                         struct_A101Y.data_crdcUseAmt = strValue; 	           
                         break;
                        case "data.drtpCardUseAmt":
                         struct_A101Y.data_drtpCardUseAmt = strValue; 	   
                         break;
                        case "data.cshptUseAmt":
                         struct_A101Y.data_cshptUseAmt = strValue; 	   
                         break;
                        case "data.tdmrUseAmt":
                         struct_A101Y.data_tdmrUseAmt = strValue; 	           
                         break;
                        case "data.pbtUseAmt":
                         struct_A101Y.data_pbtUseAmt = strValue; 	           
                         break;
                        case "data.crdcSumUseAmt":
                         struct_A101Y.data_crdcSumUseAmt = strValue; 	   
                         break;
                        case "data.crdcSumDdcAmt":
                         struct_A101Y.data_crdcSumDdcAmt = strValue; 
                         break;

                        case "data.prsCrdcUseAmt1":
                         struct_A101Y.data_prsCrdcUseAmt1 = strValue;             
                         break;
                        case "data.tyYrPrsCrdcUseAmt":
                         struct_A101Y.data_tyYrPrsCrdcUseAmt = strValue;  	      
                         break;
                        case "data.pyrPrsAddDdcrtUseAmt":
                         struct_A101Y.data_pyrPrsAddDdcrtUseAmt = strValue;  	
                         break;
                        case "data.tyShfyPrsAddDdcrtUseAmt":
                         struct_A101Y.data_tyShfyPrsAddDdcrtUseAmt = strValue;  	
                         break;
                        case "data.emstAsctCntrUseAmt":
                         struct_A101Y.data_emstAsctCntrUseAmt = strValue;  	      
                         break;
                        case "data.emstAsctCntrDdcAmt":
                         struct_A101Y.data_emstAsctCntrDdcAmt = strValue;  	      
                         break;
                        case "data.empMntnSnmcLbrrUseAmt":
                         struct_A101Y.data_empMntnSnmcLbrrUseAmt = strValue;  	
                         break;
                        case "data.empMntnSnmcLbrrDdcAmt":
                         struct_A101Y.data_empMntnSnmcLbrrDdcAmt = strValue;  
                         break;
                        //case "data.lfhItrUseAmt":
                        // struct_A101Y.data_lfhItrUseAmt = strValue;  	
                        // break;
                        //case "data.lfhItrDdcAmt":
                        // struct_A101Y.data_lfhItrDdcAmt = strValue;  	
                        // break;
                        case "data.ltrmCniSsUseAmt":
                         struct_A101Y.data_ltrmCniSsUseAmt = strValue;  	
                         break;
                        case "data.ltrmCniSsDdcAmt":
                         struct_A101Y.data_ltrmCniSsDdcAmt = strValue;  	
                         break;                            
                        case "data.frgrLbrrEntcPupCd":
                         struct_A101Y.data_frgrLbrrEntcPupCd = strValue;  
                            break;
                        case "data.frgrLbrrLbrOfrDt":
                            struct_A101Y.data_frgrLbrrLbrOfrDt = strValue;  	
                            break;
                        case "data.frgrLbrrReExryDt":
                            struct_A101Y.data_frgrLbrrReExryDt = strValue;  	
                            break;
                        case "data.frgrLbrrReRcpnDt":
                            struct_A101Y.data_frgrLbrrReRcpnDt = strValue;  	
                            break;
                        case "data.frgrLbrrReAlfaSbmsDt":
                            struct_A101Y.data_frgrLbrrReAlfaSbmsDt = strValue;  
                            break;
                        case "data.frgrLbrrErinImnRcpnDt":
                            struct_A101Y.data_frgrLbrrErinImnRcpnDt = strValue;  
                            break;
                        case "data.frgrLbrrErinImnSbmsDt":
                            struct_A101Y.data_frgrLbrrErinImnSbmsDt = strValue;  
                            break;
                        case "data.yupSnmcReStrtDt":
                            struct_A101Y.data_yupSnmcReStrtDt = strValue;    
                            break;
                        case "data.yupSnmcReEndDt":
                            struct_A101Y.data_yupSnmcReEndDt = strValue;  	
                            break;
                        case "data.sctcHpUseAmt":
                            struct_A101Y.data_sctcHpUseAmt = strValue;  	
                            break;
                        case "data.sctcHpDdcTrgtAmt":
                            struct_A101Y.data_sctcHpDdcTrgtAmt = strValue;  	
                            break;
                        case "data.sctcHpDdcAmt":
                            struct_A101Y.data_sctcHpDdcAmt = strValue;  	
                            break;
                        case "data.rtpnUseAmt":
                            struct_A101Y.data_rtpnUseAmt = strValue;  	      
                            break;
                        case "data.rtpnDdcTrgtAmt":
                            struct_A101Y.data_rtpnDdcTrgtAmt = strValue;  	
                            break;
                        case "data.rtpnDdcAmt":
                            struct_A101Y.data_rtpnDdcAmt = strValue;  	      
                            break;
                        case "data.pnsnSvngUseAmt":
                            struct_A101Y.data_pnsnSvngUseAmt = strValue;  	
                            break;
                        case "data.pnsnSvngDdcTrgtAmt":
                            struct_A101Y.data_pnsnSvngDdcTrgtAmt = strValue;  
                            break;
                        case "data.pnsnSvngDdcAmt":
                            struct_A101Y.data_pnsnSvngDdcAmt = strValue;  	
                            break;
                        case "data.pnsnAccUseAmtSum":
                            struct_A101Y.data_pnsnAccUseAmtSum = strValue;  
                            break;
                        case "data.pnsnAccDdcTrgtAmtSum":
                            struct_A101Y.data_pnsnAccDdcTrgtAmtSum = strValue;  
                            break;
                        case "data.pnsnAccDdcAmtSum":
                            struct_A101Y.data_pnsnAccDdcAmtSum = strValue;  
	                        break;
                        case "data.cvrgInscUseAmt":
                            struct_A101Y.data_cvrgInscUseAmt = strValue;  	    
                            break;
                        case "data.cvrgInscDdcTrgtAmt2":
                            struct_A101Y.data_cvrgInscDdcTrgtAmt2 = strValue;  
                            break;
                        case "data.cvrgInscDdcAmt":
                            struct_A101Y.data_cvrgInscDdcAmt = strValue;           
                            break;
                        case "data.dsbrEuCvrgUseAmt":
                            struct_A101Y.data_dsbrEuCvrgUseAmt = strValue;         
                            break;
                        case "data.dsbrEuCvrgDdcTrgtAmt":
                            struct_A101Y.data_dsbrEuCvrgDdcTrgtAmt = strValue;     
                            break;
                        case "data.dsbrEuCvrgDdcAmt":
                            struct_A101Y.data_dsbrEuCvrgDdcAmt = strValue;         
                            break;
                        case "data.infeePymUseAmtSum":
                            struct_A101Y.data_infeePymUseAmtSum = strValue;  
                            break;
                        case "data.infeePymDdcTrgtAmtSum":
                            struct_A101Y.data_infeePymDdcTrgtAmtSum = strValue;  
                            break;
                        case "data.infeePymDdcAmtSum":
                            struct_A101Y.data_infeePymDdcAmtSum = strValue;  
                            break;
                        case "data.mdxpsPrsUseAmt":
                            struct_A101Y.data_mdxpsPrsUseAmt = strValue;  
	                        break;
                        case "data.mdxpsPrsDdcTrgtAmt":
                            struct_A101Y.data_mdxpsPrsDdcTrgtAmt = strValue;  
                            break;
                        case "data.mdxpsPrsDdcAmt":
                            struct_A101Y.data_mdxpsPrsDdcAmt = strValue;  
	                        break;
                        case "data.mdxpsOthUseAmt":
                            struct_A101Y.data_mdxpsOthUseAmt = strValue;  
	                        break;
                        case "data.mdxpsOthDdcTrgtAmt":
                            struct_A101Y.data_mdxpsOthDdcTrgtAmt = strValue;  
	                        break;
                        case "data.mdxpsOthDdcAmt":
                            struct_A101Y.data_mdxpsOthDdcAmt = strValue;  
	                        break;
                        case "data.mdxpsUseAmtSum":
                            struct_A101Y.data_mdxpsUseAmtSum = strValue;  
                            break;
                        case "data.mdxpsDdcTrgtAmtSum":
                            struct_A101Y.data_mdxpsDdcTrgtAmtSum = strValue;  
	                        break;
                        case "data.mdxpsDdcAmtSum":
                            struct_A101Y.data_mdxpsDdcAmtSum = strValue;  
	                        break;
                        case "data.scxpsPrsUseAmt":
                            struct_A101Y.data_scxpsPrsUseAmt = strValue;  
	                        break;
                        case "data.scxpsPrsDdcTrgtAmt":
                            struct_A101Y.data_scxpsPrsDdcTrgtAmt = strValue;  	
                            break;
                        case "data.scxpsPrsDdcAmt":
                            struct_A101Y.data_scxpsPrsDdcAmt = strValue;   	
                            break;
                        case "data.scxpsKidUseAmt":
                            struct_A101Y.data_scxpsKidUseAmt = strValue;   	
                            break;
                        case "data.scxpsKidDdcTrgtAmt":
                            struct_A101Y.data_scxpsKidDdcTrgtAmt = strValue;  	
                            break;
                        case "data.scxpsKidDdcAmt":
                            struct_A101Y.data_scxpsKidDdcAmt = strValue;  	
                            break;
                        case "data.scxpsStdUseAmt":
                            struct_A101Y.data_scxpsStdUseAmt = strValue;  	
                            break;
                        case "data.scxpsStdDdcTrgtAmt":
                            struct_A101Y.data_scxpsStdDdcTrgtAmt = strValue;  	
                            break;
                        case "data.scxpsStdDdcAmt":
                            struct_A101Y.data_scxpsStdDdcAmt = strValue;  	
                            break;
                        case "data.scxpsUndUseAmt":
                            struct_A101Y.data_scxpsUndUseAmt = strValue;  	
                            break;
                        case "data.scxpsUndDdcTrgtAmt":
                            struct_A101Y.data_scxpsUndDdcTrgtAmt = strValue;  
	                        break;
                        case "data.scxpsUndDdcAmt":
                            struct_A101Y.data_scxpsUndDdcAmt = strValue;  	
                            break;
                        case "data.scxpsDsbrUseAmt":
                            struct_A101Y.data_scxpsDsbrUseAmt = strValue;  	
                            break;
                        case "data.scxpsDsbrDdcTrgtAmt":
                            struct_A101Y.data_scxpsDsbrDdcTrgtAmt = strValue;  
                            break;
                        case "data.scxpsDsbrDdcAmt":
                            struct_A101Y.data_scxpsDsbrDdcAmt = strValue;  	
                            break;
                        case "data.scxpsKidCount":
                            struct_A101Y.data_scxpsKidCount = strValue;  	
                            break;
                        case "data.scxpsStdCount":
                            struct_A101Y.data_scxpsStdCount = strValue;  	
                            break;
                        case "data.scxpsUndCount":
                            struct_A101Y.data_scxpsUndCount = strValue;  	
                            break;
                        case "data.scxpsDsbrCount":
                            struct_A101Y.data_scxpsDsbrCount = strValue;  	
                            break;
                        case "data.scxpsUseAmtSum":
                            struct_A101Y.data_scxpsUseAmtSum = strValue;   	
                            break;
                        case "data.scxpsDdcTrgtAmtSum":
                            struct_A101Y.data_scxpsDdcTrgtAmtSum = strValue;  	
                            break;
                        case "data.scxpsDdcAmtSum":
                            struct_A101Y.data_scxpsDdcAmtSum = strValue;  	
                            break;
                        case "data.conb10ttswLtUseAmt":
                            struct_A101Y.data_conb10ttswLtUseAmt = strValue;  	
                            break;
                        case "data.conb10ttswLtDdcTrgtAmt":
                            struct_A101Y.data_conb10ttswLtDdcTrgtAmt = strValue;  	
                            break;
                        case "data.conb10ttswLtDdcAmt":
                            struct_A101Y.data_conb10ttswLtDdcAmt = strValue;  	        
                            break;
                        case "data.conb10excsLtUseAmt":
                            struct_A101Y.data_conb10excsLtUseAmt = strValue;  	        
                            break;
                        case "data.conb10excsLtDdcTrgtAmt":
                            struct_A101Y.data_conb10excsLtDdcTrgtAmt = strValue;  	
                            break;
                        case "data.conb10excsLtDdcAmt":
                            struct_A101Y.data_conb10excsLtDdcAmt = strValue;  	
                            break;
                        case "data.conbLglUseAmt":
                            struct_A101Y.data_conbLglUseAmt = strValue;  	
                            break;
                        case "data.conbLglDdcTrgtAmt":
                            struct_A101Y.data_conbLglDdcTrgtAmt = strValue;  	
                            break;
                        case "data.conbLglDdcAmt":
                            struct_A101Y.data_conbLglDdcAmt = strValue;  	     
                            break;
                        case "data.conbEmstAsctUseAmt":
                            struct_A101Y.data_conbEmstAsctUseAmt = strValue;  	
                            break;
                        case "data.conbEmstAsctDdcTrgtAmt":
                            struct_A101Y.data_conbEmstAsctDdcTrgtAmt = strValue;  	   
                            break;
                        case "data.conbEmstAsctDdcAmt":
                            struct_A101Y.data_conbEmstAsctDdcAmt = strValue;   	
                            break;
                        case "data.conbReliOrgOthAppnUseAmt":
                            struct_A101Y.data_conbReliOrgOthAppnUseAmt = strValue;  	
                            break;
                        case "data.conbReliOrgOthAppnDdcTrgtAmt":
                            struct_A101Y.data_conbReliOrgOthAppnDdcTrgtAmt = strValue;  
                            break;
                        case "data.conbReliOrgOthAppnDdcAmt":
                            struct_A101Y.data_conbReliOrgOthAppnDdcAmt = strValue;  
	                        break;
                        case "data.conbReliOrgAppnUseAmt":
                            struct_A101Y.data_conbReliOrgAppnUseAmt = strValue;  	
                            break;
                        case "data.conbReliOrgAppnDdcTrgtAmt":
                            struct_A101Y.data_conbReliOrgAppnDdcTrgtAmt = strValue;  	
                            break;
                        case "data.conbReliOrgAppnDdcAmt":
                            struct_A101Y.data_conbReliOrgAppnDdcAmt = strValue;  
                            break;
                        case "data.conbUseAmtSum":
                            struct_A101Y.data_conbUseAmtSum = strValue;   
                            break;
                        case "data.conbDdcTrgtAmtSum":
                            struct_A101Y.data_conbDdcTrgtAmtSum = strValue;  
                            break;
                        case "data.conbDdcAmtSum":
                            struct_A101Y.data_conbDdcAmtSum = strValue;  
                            break;
                        case "data.ovrsSurcIncFmt":
                            struct_A101Y.data_ovrsSurcIncFmt = strValue;  
                            break;
                        case "data.frgnPmtFcTxamt":
                            struct_A101Y.data_frgnPmtFcTxamt = strValue;  
                            break;
                        case "data.frgnPmtWcTxamt":
                            struct_A101Y.data_frgnPmtWcTxamt = strValue;  
                            break;
                        case "data.frgnPmtTxamtTxpNtnNm":
                            struct_A101Y.data_frgnPmtTxamtTxpNtnNm = strValue;  
                            break;
                        case "data.frgnPmtTxamtPmtDt":
                            struct_A101Y.data_frgnPmtTxamtPmtDt = strValue;  
                            break;
                        case "data.frgnPmtTxamtAlfaSbmsDt":
                            struct_A101Y.data_frgnPmtTxamtAlfaSbmsDt = strValue;  
                            break;
                        case "data.frgnPmtTxamtAbrdWkarNm":
                            struct_A101Y.data_frgnPmtTxamtAbrdWkarNm = strValue;  
                            break;
                        case "data.frgnDtyTerm":
                            struct_A101Y.data_frgnDtyTerm = strValue;  
                            break;
                        case "data.frgnPmtTxamtRfoNm":
                            struct_A101Y.data_frgnPmtTxamtRfoNm = strValue;  
                            break;
                        case "data.hsngTennLnpbUseAmt":
                            struct_A101Y.data_hsngTennLnpbUseAmt = strValue;  
                            break;
                        case "data.hsngTennLnpbDdcAmt":
                            struct_A101Y.data_hsngTennLnpbDdcAmt = strValue;  
                            break;
                        case "data.mmrUseAmt":
                            struct_A101Y.data_mmrUseAmt = strValue;  
                            break;
                        case "data.mmrDdcAmt":
                            struct_A101Y.data_mmrDdcAmt = strValue;  
                            break;
                        case "data.cd218":
                            struct_A101Y.data_cd218 = strValue;  
                            break;
                        case "data.cd219":
                            struct_A101Y.data_cd219 = strValue;  
                            break;
                        case "data.cd220":
                            struct_A101Y.data_cd220 = strValue;  
                            break;
                        case "data.cd221":
                            struct_A101Y.data_cd221 = strValue;  
                            break;
                        case "data.cd222":
                            struct_A101Y.data_cd222 = strValue;  
                            break;
                        case "data.cd223":
                            struct_A101Y.data_cd223 = strValue;  
                            break;
                        case "data.cd224":
                            struct_A101Y.data_cd224 = strValue;  
                            break;
                        case "data.cd225":
                            struct_A101Y.data_cd225 = strValue;  
                            break;
                        case "data.cd226":
                            struct_A101Y.data_cd226 = strValue;  
                            break;
                        case "data.cd227":
                            struct_A101Y.data_cd227 = strValue;  
                            break;
                        case "data.cd228":
                            struct_A101Y.data_cd228 = strValue;  
                            break;

                        //인적공제
                        case "data.suptFmlyRltClCd":
                            struct_A101Y.data_suptFmlyRltClCd = strValue;
                            break;
                        case "data.txprDscmNoCntn":
                            struct_A101Y.data_txprDscmNoCntn = strValue;
                            break;
                        case "data.nnfClCd":
                            struct_A101Y.data_nnfClCd = strValue;
                            break;
                        case "data.child":
                            struct_A101Y.data_child = strValue;
                            break;
                        case "data.txprNm":
                            struct_A101Y.data_txprNm = strValue;
                            break;
                        case "data.bscDdcClCd":
                            struct_A101Y.data_bscDdcClCd = strValue;
                            break;
                        case "data.wmnDdcClCd":
                            struct_A101Y.data_wmnDdcClCd = strValue;
                            break;
                        case "data.snprntFmlyDdcClCd":
                            struct_A101Y.data_snprntFmlyDdcClCd = strValue;
                            break;
                        case "data.sccDdcClCd":
                            struct_A101Y.data_sccDdcClCd = strValue;
                            break;
                        case "data.dsbrDdcClCd":
                            struct_A101Y.data_dsbrDdcClCd = strValue;
                            break;
                        case "data.chbtAtprDdcClCd":
                            struct_A101Y.data_chbtAtprDdcClCd = strValue;
                            break;
                        case "data.age6Lt":
                            struct_A101Y.data_age6Lt = strValue;
                            break;
                        case "data.cdVvalKrnNm":
                            struct_A101Y.data_cdVvalKrnNm = strValue;
                            break;
                        case "data.hifeDdcTrgtAmt":
                            struct_A101Y.data_hifeDdcTrgtAmt = strValue;
                            break;
                        case "data.cvrgInscDdcTrgtAmt":
                            struct_A101Y.data_cvrgInscDdcTrgtAmt = strValue;
                            break;
                        case "data.dsbrDdcTrgtAmt":
                            struct_A101Y.data_dsbrDdcTrgtAmt = strValue;
                            break;
                        case "data.mdxpsDdcTrgtAmt":
                            struct_A101Y.data_mdxpsDdcTrgtAmt = strValue;
                            break;
                        case "data.scxpsDdcTrgtAmt":
                            struct_A101Y.data_scxpsDdcTrgtAmt = strValue;
                            break;
                        case "data.crdcDdcTrgtAmt":
                            struct_A101Y.data_crdcDdcTrgtAmt = strValue;
                            break;
                        case "data.drtpCardDdcTrgtAmt":
                            struct_A101Y.data_drtpCardDdcTrgtAmt = strValue;
                            break;
                        case "data.cshptDdcTrgtAmt":
                            struct_A101Y.data_cshptDdcTrgtAmt = strValue;
                            break;
                        case "data.tdmrDdcTrgtAmt":
                            struct_A101Y.data_tdmrDdcTrgtAmt = strValue;
                            break;
                        case "data.pbtDdcTrgtAmt":
                            struct_A101Y.data_pbtDdcTrgtAmt = strValue;
                            break;
                        case "data.conbDdcTrgtAmt":
                            struct_A101Y.data_conbDdcTrgtAmt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "A102Y":  //보험료(보장성,장애인보장성)
                case "A102Y":  //보험료(보장성,장애인보장성)
                    switch (AttName)
                    {
                        case "form.form_cd":      //서식코드
                            struct_A102Y.NSform_cd = strValue;
                            break;
                        case "man.resid":    //주민번호
                            struct_A102Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":  
                            struct_A102Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_A102Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":  //증권번호
                            struct_A102Y.NSacc_no = strValue;
                            break;
                        case "man.name":   //성명
                            struct_A102Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //상호
                            struct_A102Y.NStrade_nm = strValue;
                            break;
                        case "data.goods_nm":  //보험종류
                            struct_A102Y.NSgoods_nm = strValue;
                            break;
                        case "data.insu1_resid": //주민등록번호_주피보험자
                            struct_A102Y.NSinsu1_resid = strValue;
                            break;
                        case "data.insu1_nm":   //성명_주피보험자
                            struct_A102Y.NSinsu1_nm = strValue;
                            break;
                        case "data.insu2_resid_1":  //주민등록번호_주피보험자
                            struct_A102Y.NSinsu2_resid_1 = strValue;
                            break;
                        case "data.insu2_nm_1":  //성명_주피보험자
                            struct_A102Y.NSinsu2_nm_2 = strValue;
                            break;
                        case "data.insu2_resid_2":  //주민등록번호_주피보험자
                            struct_A102Y.NSinsu2_resid_2 = strValue;
                            break;
                        case "data.insu2_nm_2":  //성명_주피보험자
                            struct_A102Y.NSinsu2_nm_2 = strValue;
                            break;
                        case "data.insu2_resid_3":  //주민등록번호_주피보험자
                            struct_A102Y.NSinsu2_resid_3 = strValue;
                            break;
                        case "data.insu2_nm_3":  //성명_주피보험자
                            struct_A102Y.NSinsu2_nm_3 = strValue;
                            break;
                        case "data.sum":  //납입금액계
                            struct_A102Y.NSsum = strValue;
                            break;                        
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "A102M":  //보험료(보장성,장애인보장성)_상세내역
                case "A102M":  //보험료(보장성,장애인보장성)_상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_A102M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_A102M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_A102M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_A102M.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_A102M.NSacc_no = strValue;
                            break;
                        case "amt.mm":
                            struct_A102M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_A102M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_A102M.NStrade_nm = strValue;
                            break;
                        case "data.goods_nm":
                            struct_A102M.NSgoods_nm = strValue;
                            break;
                        case "data.insu1_resid":
                            struct_A102M.NSinsu1_resid = strValue;
                            break;
                        case "data.insu1_nm":
                            struct_A102M.NSinsu1_nm = strValue;
                            break;
                        case "data.insu2_resid_1":
                            struct_A102M.NSinsu2_resid_1 = strValue;
                            break;
                        case "data.insu2_nm_1":
                            struct_A102M.NSinsu2_nm_2 = strValue;
                            break;
                        case "data.insu2_resid_2":
                            struct_A102M.NSinsu2_resid_2 = strValue;
                            break;
                        case "data.insu2_nm_2":
                            struct_A102M.NSinsu2_nm_2 = strValue;
                            break;
                        case "data.insu2_resid_3":
                            struct_A102M.NSinsu2_resid_3 = strValue;
                            break;
                        case "data.insu2_nm_3":
                            struct_A102M.NSinsu2_nm_3 = strValue;
                            break;
                        case "data.sum":
                            struct_A102M.NSsum = strValue;
                            break;
                        case "amt.fix_cd":
                            struct_A102M.NSfix_cd = strValue;
                            break;
                        case "amt.amt":
                            struct_A102M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : //"B101Y": 공제신고서 연금저축등 소득.세액 명세 마스타, 의료비
                case "B101Y":
                    if (fbGonJeDoc) //공제신고서 연금저축등 소득.세액 명세 마스타
                    {
                        switch (AttName)
                        {
                            case "form.form_cd":
                                struct_Doc_B101Y.form_cd = strValue;
                                break;
                            case "man.bsnoEncCntn":
                                struct_Doc_B101Y.bsnoEncCntn = strValue;
                                break;
                            case "man.resnoEncCntn":
                                struct_Doc_B101Y.resnoEncCntn = strValue;
                                break;
                            case "man.tnm":
                                struct_Doc_B101Y.tnm = strValue;
                                break;
                            case "man.fnm":
                                struct_Doc_B101Y.fnm = strValue;
                                break;
                            case "man.adr":
                                struct_Doc_B101Y.adr = strValue;
                                break;
                            case "man.pfbAdr":
                                struct_Doc_B101Y.pfbAdr = strValue;
                                break;
                            //공제신고서 퇴직연금 공제명세 
                            case "data.rtpnAccRtpnCl":
                                struct_Doc_B101R.rtpnAccRtpnCl = strValue;
                                break;
                            case "data.rtpnFnnOrgnCd":
                                struct_Doc_B101R.rtpnFnnOrgnCd = strValue;
                                break;
                            case "data.rtpnAccFnnCmp":
                                struct_Doc_B101R.rtpnAccFnnCmp = strValue;
                                break;
                            case "data.rtpnAccAccno":
                                struct_Doc_B101R.rtpnAccAccno = strValue;
                                break;
                            case "data.rtpnAccPymAmt":
                                struct_Doc_B101R.rtpnAccPymAmt = strValue;
                                break;
                            case "data.rtpnAccTxamtDdcAmt":
                                struct_Doc_B101R.rtpnAccTxamtDdcAmt = strValue;
                                break;
                            //공제신고서 연금저축 공제명세
                            case "data.pnsnSvngAccPnsnSvngCl":
                                struct_Doc_B101P.pnsnSvngAccPnsnSvngCl = strValue;
                                break;
                            case "data.pnsnSvngFnnOrgnCd":
                                struct_Doc_B101P.pnsnSvngFnnOrgnCd = strValue;
                                break;
                            case "data.pnsnSvngAccFnnCmp":
                                struct_Doc_B101P.pnsnSvngAccFnnCmp = strValue;
                                break;
                            case "data.pnsnSvngAccAccno":
                                struct_Doc_B101P.pnsnSvngAccAccno = strValue;
                                break;
                            case "data.pnsnSvngAccPymAmt":
                                struct_Doc_B101P.pnsnSvngAccPymAmt = strValue;
                                break;
                            case "data.ipnsnSvngAccNcTxamtDdcAmt":
                                struct_Doc_B101P.ipnsnSvngAccNcTxamtDdcAmt = strValue;
                                break;
                            //공제신고서 주택마련저축 공제명세  
                            case "data.hsngPrptSvngSvngCl":
                                struct_Doc_B101H.hsngPrptSvngSvngCl = strValue;
                                break;
                            case "data.jnngDt":
                                struct_Doc_B101H.jnngDt = strValue;
                                break;
                            case "data.hsngPrptSvngFnnOrgnCd":
                                struct_Doc_B101H.hsngPrptSvngFnnOrgnCd = strValue;
                                break;
                            case "data.hsngPrptSvngFnnCmp":
                                struct_Doc_B101H.hsngPrptSvngFnnCmp = strValue;
                                break;
                            case "data.hsngPrptSvngAccno":
                                struct_Doc_B101H.hsngPrptSvngAccno = strValue;
                                break;
                            case "data.hsngPrptSvngPymAmt":
                                struct_Doc_B101H.hsngPrptSvngPymAmt = strValue;
                                break;
                            case "data.hsngPrptSvngIncDdcAmt":
                                struct_Doc_B101H.hsngPrptSvngIncDdcAmt = strValue;
                                break;
                            //공제신고서 장기집합저축 공제명세
                            case "data.ltrmCniSsfnnOrgnCd":
                                struct_Doc_B101L.ltrmCniSsfnnOrgnCd = strValue;
                                break;
                            case "data.ltrmCniSsFnnCmp":
                                struct_Doc_B101L.ltrmCniSsFnnCmp = strValue;
                                break;
                            case "data.ltrmCniSsAccno":
                                struct_Doc_B101L.ltrmCniSsAccno = strValue;
                                break;
                            case "data.ltrmCniSsPymAmt":
                                struct_Doc_B101L.ltrmCniSsPymAmt = strValue;
                                break;
                            case "data.ltrmCniSsIncDdcAmt":
                                struct_Doc_B101L.ltrmCniSsIncDdcAmt = strValue;
                                break;
                            default:
                                break;
                        }
                    }
                    else  //의료비
                    {
                        switch (AttName)
                        {
                            case "form.form_cd":
                                struct_B101Y.NSform_cd = strValue;
                                break;
                            case "man.resid":  //주민등록번호
                                struct_B101Y.NSresid = strValue;
                                break;
                            case "data.dat_cd":
                                struct_B101Y.NSdat_cd = strValue;
                                break;
                            case "data.busnid":  //사업자번호
                                struct_B101Y.NSbusnid = strValue;
                                break;
                            case "man.name":   //성명
                                struct_B101Y.NSname = strValue;
                                break;
                            case "data.trade_nm":  //상호
                                struct_B101Y.NStrade_nm = strValue;
                                break;
                            case "data.sum":   //납입금액계
                                struct_B101Y.NSsum = strValue;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region  Description : "B101D":  //의료비_상세내역
                case "B101D":  //의료비_상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_B101D.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_B101D.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_B101D.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_B101D.NSbusnid = strValue;
                            break;
                        case "amt.dd":
                            struct_B101D.NSamtdd = strValue;
                            break;
                        case "man.name":
                            struct_B101D.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_B101D.NStrade_nm = strValue;
                            break;
                        case "data.sum":
                            struct_B101D.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_B101D.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "C102Y": 교육비
                case "C102Y":
                    if (fbGonJeDoc) //공제신고서 퇴직연금 공제명세
                    {
                        //switch (AttName)
                        //{
                        //    case "form.form_cd":
                        //        struct_Doc_B101Y.form_cd = strValue;
                        //        break;
                        //    case "man.bsnoEncCntn":
                        //        struct_Doc_B101Y.bsnoEncCntn = strValue;
                        //        break;
                        //    case "man.resnoEncCntn":
                        //        struct_Doc_B101Y.resnoEncCntn = strValue;
                        //        break;
                        //    case "man.tnm":
                        //        struct_Doc_B101Y.tnm = strValue;
                        //        break;
                        //    case "man.fnm":
                        //        struct_Doc_B101Y.fnm = strValue;
                        //        break;
                        //    case "man.adr":
                        //        struct_Doc_B101Y.adr = strValue;
                        //        break;
                        //    case "man.pfbAdr":
                        //        struct_Doc_B101Y.pfbAdr = strValue;
                        //        break;
                        //    default:
                        //        break;
                        //}
                    }
                    else //교육비
                    {
                        switch (AttName)
                        {
                            case "form.form_cd":
                                struct_C102Y.NSform_cd = strValue;
                                break;
                            case "man.resid":  //주민번호
                                struct_C102Y.NSresid = strValue;
                                break;
                            case "data.dat_cd":
                                struct_C102Y.NSdat_cd = strValue;
                                break;
                            case "data.busnid":  //사업자번호
                                struct_C102Y.NSbusnid = strValue;
                                break;
                            case "data.edu_tp":  //교육비종류
                                struct_C102Y.NSedu_tp = strValue;
                                break;
                            case "data.edu_cl":  //교육비구분
                                struct_C102Y.NSedu_cl = strValue;
                                break;
                            case "man.name":  //성명
                                struct_C102Y.NSname = strValue;
                                break;
                            case "data.trade_nm":  //학교명
                                struct_C102Y.NStrade_nm = strValue;
                                break;
                            case "data.sum":   ///납입금액계
                                struct_C102Y.NSsum = strValue;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion

                #region  Description : "C102M":  //교육비 상세내역
                case "C102M":  //교육비 상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_C102M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_C102M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_C102M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_C102M.NSbusnid = strValue;
                            break;
                        case "data.edu_tp":
                            struct_C102M.NSedu_tp = strValue;
                            break;
                        case "amt.mm":
                            struct_C102M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_C102M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_C102M.NStrade_nm = strValue;
                            break;
                        case "data.sum":
                            struct_C102M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_C102M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "C202Y":  //교육비(직업훈련비)
                case "C202Y":  //교육비(직업훈련비)
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_C202Y.NSform_cd = strValue;
                            break;
                        case "man.resid":  //주민번호
                            struct_C202Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_C202Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_C202Y.NSbusnid = strValue;
                            break;
                        case "data.course_cd":  //과정코드
                            struct_C202Y.NScourse_cd = strValue;
                            break;
                        case "man.name":   //성명
                            struct_C202Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //교육기관명
                            struct_C202Y.NStrade_nm = strValue;
                            break;
                        case "data.subject_nm":  //과정명
                            struct_C202Y.NSsubject_nm = strValue;
                            break;
                        case "data.sum":  //납입금액계
                            struct_C202Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "C202M":  //교육비(직업훈련비)_상세내역
                case "C202M":  //교육비(직업훈련비)_상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_C202M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_C202M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_C202M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_C202M.NSbusnid = strValue;
                            break;
                        case "data.course_cd":
                            struct_C202M.NScourse_cd = strValue;
                            break;
                        case "amt.mm":
                            struct_C202M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_C202M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_C202M.NStrade_nm = strValue;
                            break;
                        case "data.subject_nm":
                            struct_C202M.NSsubject_nm = strValue;
                            break;
                        case "data.sum":
                            struct_C202M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_C202M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "C301Y":  //교육비(교복구입비)
                case "C301Y":  //교육비(교복구입비)
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_C301Y.NSform_cd = strValue;
                            break;
                        case "man.resid":  //주민번호
                            struct_C301Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_C301Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_C301Y.NSbusnid = strValue;
                            break;
                        case "man.name":   //성명
                            struct_C301Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //교육기관명
                            struct_C301Y.NStrade_nm = strValue;
                            break;
                        case "data.sum":   //납입금액계
                            struct_C301Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "C301M":  //교육비(교복구입비)_상세내역
                case "C301M":  //교육비(교복구입비)_상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_C301M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_C301M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_C301M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_C301M.NSbusnid = strValue;
                            break;
                        case "amt.mm":
                            struct_C301M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_C301M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_C301M.NStrade_nm = strValue;
                            break;
                        case "data.sum":
                            struct_C301M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_C301M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description :"C401Y": //교육비(학자금대출)
                case "C401Y": //교육비(학자금대출)
                        switch (AttName)
                        {
                            case "form.form_cd":
                                struct_C401Y.NSform_cd = strValue;
                                break;
                            case "man.resid":   //주민번호
                                struct_C401Y.NSresid = strValue;
                                break;
                            case "data.dat_cd":
                                struct_C401Y.NSdat_cd = strValue;
                                break;
                            case "data.busnid":  //사업자번호
                                struct_C401Y.NSbusnid = strValue;
                                break;
                            case "man.name":  //성명
                                struct_C401Y.NSname = strValue;
                                break;
                            case "data.trade_nm":  //기관명
                                struct_C401Y.NStrade_nm = strValue;
                                break;
                            case "data.sum":   //납입금액계
                                struct_C401Y.NSsum = strValue;
                                break;
                            default:
                                break;
                        }                    
                    break;
                #endregion

                #region  Description :"C401M":  //교육비(학자금대출) 상세내역
                case "C401M":  //교육비(학자금대출) 상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_C401M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_C401M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_C401M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_C401M.NSbusnid = strValue;
                            break;
                        case "amt.mm":
                            struct_C401M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_C401M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_C401M.NStrade_nm = strValue;
                            break;
                        case "data.sum":
                            struct_C401M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_C401M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "D101Y":  //개인연금저축
                case "D101Y":  //개인연금저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_D101Y.NSform_cd = strValue;
                            break;
                        case "man.resid":  //주민번호
                            struct_D101Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_D101Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_D101Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":  //계좌/증권번호
                            struct_D101Y.NSacc_no = strValue.Length > 20 ? strValue.Substring(0, 20) : strValue;
                            break;
                        case "man.name":   //성명
                            struct_D101Y.NSname = strValue;
                            break;
                        case "data.trade_nm": //상호
                            struct_D101Y.NStrade_nm = strValue;
                            break;
                        case "data.start_dt":  //계약시작일
                            struct_D101Y.NSstart_dt = strValue;
                            break;
                        case "data.end_dt":  //계약종료일
                            struct_D101Y.NSend_dt = strValue;
                            break;
                        case "data.com_cd":  //금융기관코드
                            struct_D101Y.NScom_cd = strValue;
                            break;
                        case "data.sum":  //납입금액계
                            struct_D101Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "D101M":  //개인연금저축_상세내역
                case "D101M":  //개인연금저축_상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_D101M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_D101M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_D101M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_D101M.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_D101M.NSacc_no = strValue.Length > 20 ? strValue.Substring(0, 20) : strValue;
                            break;
                        case "amt.mm":
                            struct_D101M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_D101M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_D101M.NStrade_nm = strValue;
                            break;
                        case "data.start_dt":
                            struct_D101M.NSstart_dt = strValue;
                            break;
                        case "data.end_dt":
                            struct_D101M.NSend_dt = strValue;
                            break;
                        case "data.com_cd":
                            struct_D101M.NScom_cd = strValue;
                            break;
                        case "amt.fix_cd":
                            struct_D101M.NSfix_cd = strValue;
                            break;
                        case "data.sum":
                            struct_D101M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_D101M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "E102Y", "E103Y":  //연금저축
                case "E102Y":  //연금저축
                case "E103Y":  //연금저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_E102Y.NSform_cd = strValue;
                            break;
                        case "man.resid":   //주민번호
                            struct_E102Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_E102Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_E102Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":  //계좌번호
                            struct_E102Y.NSacc_no = strValue.Length > 20 ? strValue.Substring(0, 20) : strValue;
                            break;
                        case "man.name":    //성명
                            struct_E102Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //상호
                            struct_E102Y.NStrade_nm = strValue;
                            break;
                        case "data.com_cd":  //금융회사코드
                            struct_E102Y.NScom_cd = strValue;
                            break;
                        case "data.ann_tot_amt":  //당해연도납입금액
                            struct_E102Y.NSann_tot_amt = strValue;
                            break;
                        case "data.tax_year_amt":  //당해연도인출금액
                            struct_E102Y.NStax_year_amt = strValue;
                            break;
                        case "data.ddct_bs_ass_amt":  //순납입금액
                            struct_E102Y.NSddct_bs_ass_amt = strValue;
                            break;
                        case "data.isa_ann_tot_amt":  //ISA계좌 만기전환 납입금액
                            struct_E102Y.NSisa_ann_tot_amt = strValue;
                            break;
                        case "data.isa_tax_year_amt":  //ISA계좌 만기전환 인출금액
                            struct_E102Y.NSisa_tax_year_amt = strValue;
                            break;
                        case "data.isa_ddct_bs_ass_amt":  //ISA계좌 만기전환 순납입금액
                            struct_E102Y.NSisa_ddct_bs_ass_amt = strValue;
                            break;

                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "E102M", "E103M":  //연금저축
                case "E102M":  //연금저축
                case "E103M":  //연금저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_E102Y.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_E102Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_E102Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_E102Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_E102Y.NSacc_no = strValue.Length > 20 ? strValue.Substring(0, 20) : strValue;
                            break;
                        case "man.name":
                            struct_E102Y.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_E102Y.NStrade_nm = strValue;
                            break;
                        case "data.com_cd":
                            struct_E102Y.NScom_cd = strValue;
                            break;
                        case "data.ann_tot_amt":
                            struct_E102Y.NSann_tot_amt = strValue;
                            break;
                        case "data.tax_year_amt":
                            struct_E102Y.NStax_year_amt = strValue;
                            break;
                        case "data.ddct_bs_ass_amt":
                            struct_E102Y.NSddct_bs_ass_amt = strValue;
                            break;
                        case "data.isa_ann_tot_amt":  //ISA계좌 만기전환 납입금액
                            struct_E102Y.NSisa_ann_tot_amt = strValue;
                            break;
                        case "data.isa_tax_year_amt":  //ISA계좌 만기전환 인출금액
                            struct_E102Y.NSisa_tax_year_amt = strValue;
                            break;
                        case "data.isa_ddct_bs_ass_amt":  //ISA계좌 만기전환 순납입금액
                            struct_E102Y.NSisa_ddct_bs_ass_amt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "F102Y", "F103Y":  //퇴직연금
                case "F102Y":  //퇴직연금
                case "F103Y":  //퇴직연금
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_F102Y.NSform_cd = strValue;
                            break;
                        case "man.resid":   //주민번호
                            struct_F102Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_F102Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_F102Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":  //계좌번호
                            struct_F102Y.NSacc_no = strValue.Length > 20 ? strValue.Substring(0, 20) : strValue;
                            break;
                        case "man.name":  //성명
                            struct_F102Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //상호
                            struct_F102Y.NStrade_nm = strValue;
                            break;
                        case "data.com_cd":  //금융회사코드
                            struct_F102Y.NScom_cd = strValue;
                            break;
                        case "data.pension_cd":  //계좌유형
                            struct_F102Y.NSpension_cd = strValue;
                            break;
                        case "data.ann_tot_amt": //당해연도납입금액
                            struct_F102Y.NSann_tot_amt = strValue;
                            break;
                        case "data.tax_year_amt":  //당해연도인출금액
                            struct_F102Y.NStax_year_amt = strValue;
                            break;
                        case "data.ddct_bs_ass_amt":  //순납입금액
                            struct_F102Y.NSddct_bs_ass_amt = strValue;
                            break;
                        case "data.isa_ann_tot_amt":  //ISA계좌 만기전환 납입금액
                            struct_F102Y.NSisa_ann_tot_amt = strValue;
                            break;
                        case "data.isa_tax_year_amt":  //ISA계좌 만기전환 인출금액
                            struct_F102Y.NSisa_tax_year_amt = strValue;
                            break;
                        case "data.isa_ddct_bs_ass_amt":  //ISA계좌 만기전환 순납입금액
                            struct_F102Y.NSisa_ddct_bs_ass_amt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "F102M", "F103M":  //퇴직연금
                case "F102M":  //퇴직연금
                case "F103M":  //퇴직연금
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_F102Y.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_F102Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_F102Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_F102Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_F102Y.NSacc_no = strValue.Length > 20 ? strValue.Substring(0, 20) : strValue;
                            break;
                        case "man.name":
                            struct_F102Y.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_F102Y.NStrade_nm = strValue;
                            break;
                        case "data.com_cd":
                            struct_F102Y.NScom_cd = strValue;
                            break;
                        case "data.pension_cd":
                            struct_F102Y.NSpension_cd = strValue;
                            break;
                        case "data.ann_tot_amt":
                            struct_F102Y.NSann_tot_amt = strValue;
                            break;
                        case "data.tax_year_amt":
                            struct_F102Y.NStax_year_amt = strValue;
                            break;
                        case "data.ddct_bs_ass_amt":
                            struct_F102Y.NSddct_bs_ass_amt = strValue;
                            break;
                        case "data.isa_ann_tot_amt":  //ISA계좌 만기전환 납입금액
                            struct_F102Y.NSisa_ann_tot_amt = strValue;
                            break;
                        case "data.isa_tax_year_amt":  //ISA계좌 만기전환 인출금액
                            struct_F102Y.NSisa_tax_year_amt = strValue;
                            break;
                        case "data.isa_ddct_bs_ass_amt":  //ISA계좌 만기전환 순납입금액
                            struct_F102Y.NSisa_ddct_bs_ass_amt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G106Y":  //2017년 신용카드
                case "G106Y":  //2017년 신용카드
                case "G107Y":  //2018년 신용카드
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G106Y.NSform_cd = strValue;
                            break;
                        case "man.resid":   //주민번호
                            struct_G106Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G106Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_G106Y.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":  //종류
                            struct_G106Y.NSuse_place_cd = strValue;
                            break;
                        case "man.name":   //성명
                            struct_G106Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //상호
                            struct_G106Y.NStrade_nm = strValue;
                            break;
                        case "data.sum": //공제대상금액합계
                            struct_G106Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G106M":  //2017년 신용카드 상세내역
                case "G106M":  //2017년 신용카드 상세내역
                case "G107M":  //2018년 신용카드 상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G106M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_G106M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G106M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_G106M.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":
                            struct_G106M.NSuse_place_cd = strValue;
                            break;
                        case "amt.mm":
                            struct_G106M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_G106M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_G106M.NStrade_nm = strValue;
                            break;                  
                        case "data.sum":
                            struct_G106M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_G106M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G206M":  //2017년 현금영수증 상세내역
                case "G206M":  //2017년 현금영수증 상세내역
                case "G207M":  //2018년 현금영수증 상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G206M.NSform_cd = strValue;
                            break;
                        case "man.resid":  //주민번호
                            struct_G206M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G206M.NSdat_cd = strValue;
                            break;
                        case "data.use_place_cd":  //종류
                            struct_G206M.NSuse_place_cd = strValue;
                            break;
                        case "amt.mm":  //공제월
                            struct_G206M.NSamtmm = strValue;
                            break;
                        case "man.name":  //성명
                            struct_G206M.NSname = strValue;
                            break;                   
                        case "data.sum":  //공제대상금액
                            struct_G206M.NSsum = strValue;
                            break;
                        case "amt.amt":  
                            struct_G206M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G306Y":  //2017년 직불카드
                case "G306Y":  //2017년 직불카드
                case "G307Y":  //2018년 직불카드
                case "G407Y":  //2019년 제로페이
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G106Y.NSform_cd = strValue;
                            break;
                        case "man.resid":  //주민번호
                            struct_G106Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G106Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_G106Y.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":  //종류
                            struct_G106Y.NSuse_place_cd = strValue;
                            break;
                        case "man.name":         //성명
                            struct_G106Y.NSname = strValue;
                            break;
                        case "data.trade_nm":   //상호
                            struct_G106Y.NStrade_nm = strValue;
                            break;
                        case "data.sum":   //공제대상금액합계
                            struct_G106Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G306M":  //2017년 직불카드 상세내역
                case "G306M":  //2017년 직불카드 상세내역
                case "G307M":  //2018년 직불카드 상세내역
                case "G407M":  //2019년 제로페이 상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G106M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_G106M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G106M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_G106M.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":
                            struct_G106M.NSuse_place_cd = strValue;
                            break;
                        case "amt.mm":
                            struct_G106M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_G106M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_G106M.NStrade_nm = strValue;
                            break;                     
                        case "data.sum":
                            struct_G106M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_G106M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description :  "J101Y":  //주택임차차입금 원리금상환액
                case "J101Y":  //주택임차차입금 원리금상환액
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J101Y.NSform_cd = strValue;
                            break;
                        case "man.resid":       //주민번호
                            struct_J101Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J101Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":    //사업자번호
                            struct_J101Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":   //계좌번호
                            struct_J101Y.NSacc_no = strValue;
                            break;
                        case "man.name":     //성명
                            struct_J101Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //취급기관
                            struct_J101Y.NStrade_nm = strValue;
                            break;
                        case "data.goods_nm":  //상품명
                            struct_J101Y.NSgoods_nm = strValue;
                            break;
                        case "data.lend_dt":  //대출일
                            struct_J101Y.NSlend_dt = strValue;
                            break;
                        case "data.sum":   //상환액계
                            struct_J101Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J101M":  //주택임차차입금 원리금상환액
                case "J101M":  //주택임차차입금 원리금상환액
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J101M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_J101M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J101M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_J101M.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_J101M.NSacc_no = strValue;
                            break;
                        case "amt.mm":
                            struct_J101M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_J101M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_J101M.NStrade_nm = strValue;
                            break;
                        case "data.goods_nm":
                            struct_J101M.NSgoods_nm = strValue;
                            break;
                        case "data.lend_dt":
                            struct_J101M.NSlend_dt = strValue;
                            break;
                        case "amt.fix_cd":
                            struct_J101M.NSfix_cd = strValue;
                            break;
                        case "data.sum":
                            struct_J101M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_J101M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J203Y":  //장기주택저당차입금 이자상환액
                case "J203Y":  //장기주택저당차입금 이자상환액
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J203Y.NSform_cd = strValue;
                            break;
                        case "man.resid":     //주민번호
                            struct_J203Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J203Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":   //사업자번호
                            struct_J203Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":  //계좌번호
                            struct_J203Y.NSacc_no = strValue;
                            break;
                        case "data.lend_kd":   //대출종류
                            struct_J203Y.NSlend_kd = strValue;
                            break;
                        case "man.name":        //성명
                            struct_J203Y.NSname = strValue;
                            break;
                        case "data.trade_nm":   //취급기관
                            struct_J203Y.NStrade_nm = strValue;
                            break;
                        case "data.house_take_dt":    //주택취득일
                            struct_J203Y.NShouse_take_dt = strValue;
                            break;
                        case "data.mort_setup_dt":   //저당권설정일
                            struct_J203Y.NSmort_setup_dt = strValue;
                            break;
                        case "data.start_dt":        //최초차입일
                            struct_J203Y.NSstart_dt = strValue;
                            break;
                        case "data.end_dt":           //최종상환예정일
                            struct_J203Y.NSend_dt = strValue;
                            break;
                        case "data.repay_years":        //상환기간연수
                            struct_J203Y.NSrepay_years = strValue;
                            break;
                        case "data.lend_goods_nm":       //상품명
                            struct_J203Y.NSlend_goods_nm = strValue;
                            break;
                        case "data.debt":                //차입금
                            struct_J203Y.NSdebt = strValue;
                            break;
                        case "data.fixed_rate_debt":     //고정금리차입금
                            struct_J203Y.NSfixed_rate_debt = strValue;
                            break;
                        case "data.not_defer_debt":      //비거치식상환차입금
                            struct_J203Y.NSnot_defer_debt = strValue;
                            break;
                        case "data.this_year_rede_amt":       //당해년 원금상환액
                            struct_J203Y.NSthis_year_rede_amt = strValue;
                            break;
                        case "sum.ddct":                   //소득공제대상액
                            struct_J203Y.NSsumddct = strValue;
                            break;
                        case "sum.sum":                      //연간합계액
                            struct_J203Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J203M":  //장기주택저당차입금 이자상환액
                case "J203M":  //장기주택저당차입금 이자상환액
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J203M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_J203M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J203M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_J203M.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_J203M.NSacc_no = strValue;
                            break;
                        case "data.lend_kd":
                            struct_J203M.NSlend_kd = strValue;
                            break;
                        case "amt.mm":
                            struct_J203M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_J203M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_J203M.NStrade_nm = strValue;
                            break;
                        case "data.house_take_dt":
                            struct_J203M.NShouse_take_dt = strValue;
                            break;
                        case "data.mort_setup_dt":
                            struct_J203M.NSmort_setup_dt = strValue;
                            break;
                        case "data.start_dt":
                            struct_J203M.NSstart_dt = strValue;
                            break;
                        case "data.end_dt":
                            struct_J203M.NSend_dt = strValue;
                            break;
                        case "data.repay_years":
                            struct_J203M.NSrepay_years = strValue;
                            break;
                        case "data.lend_goods_nm":
                            struct_J203M.NSlend_goods_nm = strValue;
                            break;
                        case "data.debt":
                            struct_J203M.NSdebt = strValue;
                            break;
                        case "data.fixed_rate_debt":
                            struct_J203M.NSfixed_rate_debt = strValue;
                            break;
                        case "data.not_defer_debt":
                            struct_J203M.NSnot_defer_debt = strValue;
                            break;
                        case "data.this_year_rede_amt":
                            struct_J203M.NSthis_year_rede_amt = strValue;
                            break;
                        case "data.sumddct":
                            struct_J203M.NSsumddct = strValue;
                            break;
                        case "data.sum":
                            struct_J203M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_J203M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J301Y":  //주택마련저축
                case "J301Y":  //주택마련저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J301Y.NSform_cd = strValue;
                            break;
                        case "man.resid":       //주민번호
                            struct_J301Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J301Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":   //사업자번호
                            struct_J301Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":   //계좌번호
                            struct_J301Y.NSacc_no = strValue;
                            break;
                        case "data.saving_gubn":    //저축구분
                            struct_J301Y.NSsaving_gubn = strValue;
                            break;
                        case "man.name":          //성명
                            struct_J301Y.NSname = strValue;
                            break;
                        case "data.trade_nm":    //취급기관
                            struct_J301Y.NStrade_nm = strValue;
                            break;
                        case "data.goods_nm":    //저축명
                            struct_J301Y.NSgoods_nm = strValue;
                            break;
                        case "data.reg_dt":     //가입일자
                            struct_J301Y.NSreg_dt = strValue;
                            break; 
                        case "data.com_cd":     //금융기관
                            struct_J301Y.NScom_cd = strValue;
                            break;
                        case "data.sum":         //납입금액계
                            struct_J301Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J301M":  //주택마련저축
                case "J301M":  //주택마련저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J301M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_J301M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J301M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_J301M.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_J301M.NSacc_no = strValue;
                            break;
                        case "data.saving_gubn":
                            struct_J301M.NSsaving_gubn = strValue;
                            break;
                        case "amt.mm":
                            struct_J301M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_J301M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_J301M.NStrade_nm = strValue;
                            break;
                        case "data.goods_nm":
                            struct_J301M.NSgoods_nm = strValue;
                            break;
                        case "data.reg_dt":
                            struct_J301M.NSreg_dt = strValue;
                            break;
                        case "data.com_cd":
                            struct_J301M.NScom_cd = strValue;
                            break;
                        case "amt.fix_cd":
                            struct_J301M.NSfix_cd = strValue;
                            break;
                        case "data.sum":
                            struct_J301M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_J301M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J401Y":  //주택마련저축
                case "J401Y":  //주택마련저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J401Y.NSform_cd = strValue;
                            break;
                        case "man.resid":    //주민번호
                            struct_J401Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J401Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":   //사업자번호
                            struct_J401Y.NSbusnid = strValue;
                            break;
                        case "data.acc_no":   //계좌번호
                            struct_J401Y.NSacc_no = strValue;
                            break;
                        case "man.name":  //성명
                            struct_J401Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //취급기관
                            struct_J401Y.NStrade_nm = strValue;
                            break;
                        case "data.lend_dt":  //대출일자
                            struct_J401Y.NSlend_dt = strValue;
                            break;
                        case "data.lend_loan_amt":  //대출원금
                            struct_J401Y.NSlend_loan_amt = strValue;
                            break;
                        case "data.sum":   //연간합계금액
                            struct_J401Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J401M":  //주택마련저축
                case "J401M":  //주택마련저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J401M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_J401M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J401M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_J401M.NSbusnid = strValue;
                            break;
                        case "data.acc_no":
                            struct_J401M.NSacc_no = strValue;
                            break;
                        case "amt.mm":
                            struct_J401M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_J401M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_J401M.NStrade_nm = strValue;
                            break;
                        case "data.lend_dt":
                            struct_J401M.NSlend_dt = strValue;
                            break;
                        case "data.lend_loan_amt":
                            struct_J401M.NSlend_loan_amt = strValue;
                            break;
                        case "amt.fix_cd":
                            struct_J401M.NSfix_cd = strValue;
                            break;
                        case "data.sum":
                            struct_J401M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_J401M.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "K101M":  //소기업소상공인 공제부금
                case "K101M":  //소기업소상공인 공제부금
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_K101M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_K101M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_K101M.NSdat_cd = strValue;
                            break;
                        case "data.acc_no":
                            struct_K101M.NSacc_no = strValue;
                            break;
                        case "amt.mm":
                            struct_K101M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_K101M.NSname = strValue;
                            break;
                        case "data.start_dt":
                            struct_K101M.NSstart_dt = strValue;
                            break;
                        case "data.end_dt":
                            struct_K101M.NSend_dt = strValue;
                            break;
                        case "data.pay_method":
                            struct_K101M.NSpay_method = strValue;
                            break;
                        case "sum.sum":
                            struct_K101M.NSsum = strValue;
                            break;
                        case "sum.ddct":
                            struct_K101M.NSsumddct = strValue;
                            break;
                        case "amt.date":
                            struct_K101M.NSdate = strValue;
                            break;
                        case "amt.amt":
                            struct_K101M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "L102Y":  //기부금
                case "L102Y":  //기부금
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_L102Y.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_L102Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_L102Y.NSdat_cd = strValue;
                            break;
                        case "data.resid":
                            struct_L102Y.NSresid = strValue;
                            break;
                        case "data.busnid":
                            struct_L102Y.NSbusnid = strValue;
                            break;
                        case "data.donation_cd":
                            struct_L102Y.NSdonation_cd = strValue;
                            break;
                        case "man.name":
                            struct_L102Y.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_L102Y.NStrade_nm = strValue;
                            break;
                        case "data.sum":
                            struct_L102Y.NSsum = strValue;
                            break;
                        case "data.sbdy_apln_sum":
                            struct_L102Y.NSsbdy_apln_sum = strValue;
                            break;
                        case "data.conb_sum":
                            struct_L102Y.NSconb_sum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "L102D":  //기부금
                case "L102D":  //기부금
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_L102D.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_L102D.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_L102D.NSdat_cd = strValue;
                            break;
                        case "data.resid":
                            struct_L102D.NSresid = strValue;
                            break;
                        case "data.busnid":
                            struct_L102D.NSbusnid = strValue;
                            break;
                        case "data.donation_cd":
                            struct_L102D.NSdonation_cd = strValue;
                            break;
                        case "amt.dd":
                            struct_L102D.NSamtdd = strValue;
                            break;
                        case "man.name":
                            struct_L102D.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_L102D.NStrade_nm = strValue;
                            break;
                        case "data.sum":
                            struct_L102D.NSsum = strValue;
                            break;
                        case "data.sbdy_apln_sum":
                            struct_L102D.NSsbdy_apln_sum = strValue;
                            break;
                        case "data.conb_sum":
                            struct_L102D.NSconb_sum = strValue;
                            break;
                        case "amt.amt":
                            struct_L102D.NSamt = strValue;
                            break;
                        case "amt.apln":
                            struct_L102D.NSamtapln = strValue;
                            break;
                        case "amt.sum":
                            struct_L102D.NSamtsum = strValue;
                            break;

                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "N101Y":  //장기집합투자증권저축
                case "N101Y":  //장기집합투자증권저축
                case "N102Y":  //장기집합투자증권저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_N101Y.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_N101Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_N101Y.NSdat_cd = strValue;
                            break;
                        case "data.resid":
                            struct_N101Y.NSresid = strValue;
                            break;
                        case "data.busnid":
                            struct_N101Y.NSbusnid = strValue;
                            break;
                        case "data.secu_no":
                            struct_N101Y.NSsecu_no = strValue;
                            break;
                        case "man.name":
                            struct_N101Y.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_N101Y.NStrade_nm = strValue;
                            break;
                        case "data.fund_nm":
                            struct_N101Y.NSfund_nm = strValue;
                            break;
                        case "data.reg_dt":
                            struct_N101Y.NSreg_dt = strValue;
                            break;
                        case "data.ctr_term_mm_cnt":
                            struct_N101Y.NSctr_term_mm_cnt = strValue;
                            break;
                        case "data.com_cd":
                            struct_N101Y.NScom_cd = strValue;
                            break;
                        case "data.sum":
                            struct_N101Y.NSsum = strValue;
                            break;
                        case "data.ddct_bs_ass_amt":
                            struct_N101Y.NSddct_bs_ass_amt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "N101M":  //장기집합투자증권저축
                case "N101M":  //장기집합투자증권저축
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_N101M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_N101M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_N101M.NSdat_cd = strValue;
                            break;
                        case "data.resid":
                            struct_N101M.NSresid = strValue;
                            break;
                        case "data.busnid":
                            struct_N101M.NSbusnid = strValue;
                            break;
                        case "data.secu_no":
                            struct_N101M.NSsecu_no = strValue;
                            break;
                        case "amt.mm":
                            struct_N101M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_N101M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_N101M.NStrade_nm = strValue;
                            break;
                        case "data.fund_nm":
                            struct_N101M.NSfund_nm = strValue;
                            break;
                        case "data.reg_dt":
                            struct_N101M.NSreg_dt = strValue;
                            break;
                        case "data.com_cd":
                            struct_N101M.NScom_cd = strValue;
                            break;
                        case "amt.fix_cd":
                            struct_N101M.NSfix_cd = strValue;
                            break;
                        case "data.sum":
                            struct_N101M.NSsum = strValue;
                            break;
                        case "data.ddct_bs_ass_amt":
                            struct_N101M.NSddct_bs_ass_amt = strValue;
                            break;
                        case "amt.amt":
                            struct_N101M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "O101M":  //건강보험료
                case "O101M":  //건강보험료
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_O101M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_O101M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_O101M.NSdat_cd = strValue;
                            break;
                        case "man.name":
                            struct_O101M.NSname = strValue;
                            break;
                        case "sum.hi_yrs":
                            struct_O101M.NSMhi_yrs = strValue;
                            break;
                        case "sum.ltrm_yrs":
                            struct_O101M.NSMltrm_yrs = strValue;
                            break;
                        case "sum.hi_ntf":
                            struct_O101M.NSMhi_ntf = strValue;
                            break;
                        case "sum.ltrm_ntf":
                            struct_O101M.NSMltrm_ntf = strValue;
                            break;
                        case "sum.hi_pmt":
                            struct_O101M.NSMhi_pmt = strValue;
                            break;
                        case "sum.ltrm_pmt":
                            struct_O101M.NSMltrm_pmt = strValue;
                            break;
                        case "sum.sum":
                            struct_O101M.NSsum = strValue;
                            break;
                        case "amt.mm":
                            struct_O101M.NSamtmm = strValue;
                            break;
                        case "amt.hi_ntf":
                            struct_O101M.NShi_ntf = strValue;
                            break;
                        case "amt.ltrm_ntf":
                            struct_O101M.NSltrm_ntf = strValue;
                            break;
                        case "amt.hi_pmt":
                            struct_O101M.NShi_pmt = strValue;
                            break;
                        case "amt.ltrm_pmt":
                            struct_O101M.NSltrm_pmt = strValue;
                            break;
                        default:                            
                            break;
                    }
                    break;
                #endregion

                #region  Description : "P102M":  //국민연금
                case "P102M":  //국민연금
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_P102M.NSform_cd = strValue;
                            break;
                        case "data.dat_cd":
                            struct_P102M.NSdat_cd = strValue;
                            break;
                        case "man.resid":
                            struct_P102M.NSresid = strValue;
                            break;
                        case "amt.mm":
                            struct_P102M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_P102M.NSname = strValue;
                            break;                       
                        case "sum.sum":
                            struct_P102M.NSsum = strValue;
                            break;
                        case "sum.sp_ntf":
                            struct_P102M.NSsumsp_ntf = strValue;
                            break;
                        case "sum.spym":
                            struct_P102M.NSsumspym = strValue;
                            break;
                        case "sum.jlc":
                            struct_P102M.NSsumjlc = strValue;
                            break;
                        case "sum.ntf":
                            struct_P102M.NSsumntf = strValue;
                            break;
                        case "sum.pmt":
                            struct_P102M.NSsumpmt = strValue;
                            break;
                        case "amt.wrkp_ntf":
                            struct_P102M.NSwrkp_ntf = strValue;
                            break;
                        case "amt.rgn_pmt":
                            struct_P102M.NSrgn_pmt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "J501Y":  //월세액(2020년이후)
                case "J501Y":  //월세액(2020년이후)
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_J501Y.NSform_cd = strValue;
                            break;
                        case "man.resid":    //주민번호
                            struct_J501Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_J501Y.NSdat_cd = strValue;
                            break;
                        case "data.lsor_no":   //임대인번호
                            struct_J501Y.NSlsor_no = strValue;
                            break;
                        case "man.name":  //성명
                            struct_J501Y.NSname = strValue;
                            break;
                        case "data.lsor_nm":  //임대인성명
                            struct_J501Y.NSlsor_nm = strValue;
                            break;
                        case "data.start_dt":  //임대시작일
                            struct_J501Y.NSstart_dt = strValue;
                            break;
                        case "data.end_dt":  //임대종료일
                            struct_J501Y.NSend_dt = strValue;
                            break;
                        case "data.adr":  //계약주소지
                            struct_J501Y.NSadr = strValue;
                            break;
                        case "data.area":  //계약면적
                            struct_J501Y.NSarea = strValue;
                            break;
                        case "data.typeCd":  //유형코드
                            struct_J501Y.NStypeCd = strValue;
                            break;
                        case "data.sum":   //지급금액계
                            struct_J501Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G108Y":  //2020년 신용카드
                case "G108Y":  //2020년 신용카드    
                case "G308Y":  //2020년 직불카드    
                case "G408Y":  //2020년 제로페이   
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G108Y.NSform_cd = strValue;
                            break;
                        case "man.resid":   //주민번호
                            struct_G108Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G108Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_G108Y.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":  //종류
                            struct_G108Y.NSuse_place_cd = strValue;
                            break;
                        case "man.name":   //성명
                            struct_G108Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //상호
                            struct_G108Y.NStrade_nm = strValue;
                            break;
                        case "sum_data.gnrl_sum":   		//2020년 일반합계//,
                            struct_G108Y.NSgnrl_sum = strValue;
                            break;
                        case "sum_data.tdmr_sum":   		//2020년 전통시장합계//,
                            struct_G108Y.NStdmr_sum = strValue;
                            break;
                        case "sum_data.trp_sum":    		//2020년 대중교통합계//,
                            struct_G108Y.NStrp_sum = strValue;
                            break;
                        case "sum_data.isld_sum":   		//2020년 도서공연등합계//,
                            struct_G108Y.NSisld_sum = strValue;
                            break;
                        case "sum_data.tot_sum":    		//2020년 총합계//,
                            struct_G108Y.NStot_sum = strValue;                            
                            break;
                        case "sum_data.gnrl_mar_sum":  		//3월분 일반합계//,
                            struct_G108Y.NSgnrl_mar_sum = strValue;
                            break;
                        case "sum_data.tdmr_mar_sum":  		//3월분 전통시장합계//,
                            struct_G108Y.NStdmr_mar_sum = strValue;
                            break;
                        case "sum_data.trp_mar_sum":   		//3월분 대중교통합계//,
                            struct_G108Y.NStrp_mar_sum = strValue;
                            break;
                        case "sum_data.isld_mar_sum":  		//3월분 도서공연등합계//,
                            struct_G108Y.NSisld_mar_sum = strValue;
                            break;
                        case "sum_data.tot_mar_sum":   		//3월분 총합계//,
                            struct_G108Y.NStot_mar_sum = strValue;
                            break;
                        case "sum_data.gnrl_aprl_sum": 		//4~7월 일반합계//,
                            struct_G108Y.NSgnrl_aprl_sum = strValue;
                            break;
                        case "sum_data.tdmr_aprl_sum": 		//4~7월 전통시장합계//,
                            struct_G108Y.NStdmr_aprl_sum = strValue;
                            break;
                        case "sum_data.trp_aprl_sum":  		//4~7월 대중교통합계//,
                            struct_G108Y.NStrp_aprl_sum = strValue;
                            break;
                        case "sum_data.isld_aprl_sum": 		//4~7월 도서공연등합계//,
                            struct_G108Y.NSisld_aprl_sum = strValue;
                            break;
                        case "sum_data.tot_aprl_sum":  		//4~7월 총합계//,
                            struct_G108Y.NStot_aprl_sum = strValue;
                            break;
                        case "sum_data.gnrl_jan_sum":  		//그 외 일반합계//,
                            struct_G108Y.NSgnrl_jan_sum = strValue;
                            break;
                        case "sum_data.tdmr_jan_sum":  		//그 외 전통시장합계//,
                            struct_G108Y.NStdmr_jan_sum = strValue;
                            break;
                        case "sum_data.trp_jan_sum":   		//그 외 대중교통합계//,
                            struct_G108Y.NStrp_jan_sum = strValue;
                            break;
                        case "sum_data.isld_jan_sum":  		//그 외 도서공연등합계//,
                            struct_G108Y.NSisld_jan_sum = strValue;
                            break;
                        case "sum_data.tot_jan_sum":   		//그 외 총합계//,
                            struct_G108Y.NStot_jan_sum = strValue;
                            break;
                        case "data.sum": //공제대상금액합계
                            struct_G108Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G110Y":  //2022년 신용카드
                case "G109Y":  //2021년 신용카드    
                case "G309Y":  //2021년 직불카드    
                case "G409Y":  //2021년 제로페이   
                case "G110Y":  //2021년 신용카드    
                case "G310Y":  //2021년 직불카드    
                case "G410Y":  //2021년 제로페이   

                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G110Y.NSform_cd = strValue;
                            break;
                        case "man.resid":   //주민번호
                            struct_G110Y.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G110Y.NSdat_cd = strValue;
                            break;
                        case "data.busnid":  //사업자번호
                            struct_G110Y.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":  //종류
                            struct_G110Y.NSuse_place_cd = strValue;
                            break;
                        case "man.name":   //성명
                            struct_G110Y.NSname = strValue;
                            break;
                        case "data.trade_nm":  //상호
                            struct_G110Y.NStrade_nm = strValue;
                            break;
                        case "sum_data.tot_pre_year_sum":   		//2020년 사용금액 합계//,
                            struct_G110Y.NStot_pre_year_sum = strValue;
                            break;
                        case "sum_data.tot_curr_year_sum":   		//2021년 사용금액 합계//,
                            struct_G110Y.NStot_curr_year_sum = strValue;
                            break;
                        case "sum_data.tdmr_tot_pre_year_sum":   		//2021년 전통시장 사용금액 합계
                            struct_G110Y.NStdmr_tot_pre_year_sum = strValue;
                            break;
                        case "sum_data.tdmr_tot_curr_year_sum":   		//2022년 전통시장 사용금액 합계',
                            struct_G110Y.NStdmr_tot_curr_year_sum = strValue;
                            break;
                        case "sum_data.gnrl_sum":   		//2021년 일반합계//,
                            struct_G110Y.NSgnrl_sum = strValue;
                            break;
                        case "sum_data.tdmr_sum":   		//2021년 전통시장합계//,
                            struct_G110Y.NStdmr_sum = strValue;
                            break;
                        case "sum_data.trp_sum":    		//2021년 대중교통합계//,
                            struct_G110Y.NStrp_sum = strValue;
                            break;
                        case "sum_data.isld_sum":   		//2021년 도서공연등합계//,
                            struct_G110Y.NSisld_sum = strValue;
                            break;
                        case "sum_data.tot_sum":    		//2021년 총합계//,
                            struct_G110Y.NStot_sum = strValue;
                            break;
                        case "sum_data.tfhy_gnrl_sum":    		//2022년  상반기 일반합계
                            struct_G110Y.NStfhy_gnrl_sum = strValue;
                            break;
                        case "sum_data.tfhy_tdmr_sum":    		//2022년  상반기 전통시장합계',   
                            struct_G110Y.NStfhy_tdmr_sum = strValue;
                            break;
                        case "sum_data.tfhy_trp_sum":    		//2022년  상반기 대중교통합계',   
                            struct_G110Y.NStfhy_trp_sum = strValue;
                            break;
                        case "sum_data.tfhy_isld_sum":    		//2022년  상반기 도서공연등합계', 
                            struct_G110Y.NStfhy_isld_sum = strValue;
                            break;
                        case "sum_data.tfhy_tot_sum":    		//2022년  상반기 총합계',         
                            struct_G110Y.NStfhy_tot_sum = strValue;
                            break;
                        case "sum_data.shfy_gnrl_sum":    		//2022년  하반기 일반합계',       
                            struct_G110Y.NSshfy_gnrl_sum = strValue;
                            break;
                        case "sum_data.shfy_tdmr_sum":    		//2022년  하반기 전통시장합계',   
                            struct_G110Y.NSshfy_tdmr_sum = strValue;
                            break;
                        case "sum_data.shfy_trp_sum":    		//2022년  하반기 대중교통합계',   
                            struct_G110Y.NSshfy_trp_sum = strValue;
                            break;
                        case "sum_data.shfy_isld_sum":    		//2022년  하반기 도서공연등합계', 
                            struct_G110Y.NSshfy_isld_sum = strValue;
                            break;
                        case "sum_data.shfy_tot_sum":    		//2022년  하반기 총합계',
                            struct_G110Y.NSshfy_tot_sum = strValue;
                            break;
                        case "data.sum": //공제대상금액합계
                            struct_G110Y.NSsum = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion


                #region  Description : "G108M":  //2020년 신용카드 상세내역
                case "G108M":  //2020년 신용카드 상세내역
                case "G308M":  //2020년 직불카드 상세내역
                case "G408M":  //2020년 제로페이 상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G108M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_G108M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G108M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_G108M.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":
                            struct_G108M.NSuse_place_cd = strValue;
                            break;
                        case "amt.mm":
                            struct_G108M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_G108M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_G108M.NStrade_nm = strValue;
                            break;
                        case "sum_data.gnrl_sum":   		//2020년 일반합계//,
                            struct_G108M.NSgnrl_sum = strValue;
                            break;
                        case "sum_data.tdmr_sum":   		//2020년 전통시장합계//,
                            struct_G108M.NStdmr_sum = strValue;
                            break;
                        case "sum_data.trp_sum":    		//2020년 대중교통합계//,
                            struct_G108M.NStrp_sum = strValue;
                            break;
                        case "sum_data.isld_sum":   		//2020년 도서공연등합계//,
                            struct_G108M.NSisld_sum = strValue;
                            break;
                        case "sum_data.tot_sum":    		//2020년 총합계//,
                            struct_G108M.NStot_sum = strValue;
                            break;
                        case "sum_data.gnrl_mar_sum":  		//3월분 일반합계//,
                            struct_G108M.NSgnrl_mar_sum = strValue;
                            break;
                        case "sum_data.tdmr_mar_sum":  		//3월분 전통시장합계//,
                            struct_G108M.NStdmr_mar_sum = strValue;
                            break;
                        case "sum_data.trp_mar_sum":   		//3월분 대중교통합계//,
                            struct_G108M.NStrp_mar_sum = strValue;
                            break;
                        case "sum_data.isld_mar_sum":  		//3월분 도서공연등합계//,
                            struct_G108M.NSisld_mar_sum = strValue;
                            break;
                        case "sum_data.tot_mar_sum":   		//3월분 총합계//,
                            struct_G108M.NStot_mar_sum = strValue;
                            break;
                        case "sum_data.gnrl_aprl_sum": 		//4~7월 일반합계//,
                            struct_G108M.NSgnrl_aprl_sum = strValue;
                            break;
                        case "sum_data.tdmr_aprl_sum": 		//4~7월 전통시장합계//,
                            struct_G108M.NStdmr_aprl_sum = strValue;
                            break;
                        case "sum_data.trp_aprl_sum":  		//4~7월 대중교통합계//,
                            struct_G108M.NStrp_aprl_sum = strValue;
                            break;
                        case "sum_data.isld_aprl_sum": 		//4~7월 도서공연등합계//,
                            struct_G108M.NSisld_aprl_sum = strValue;
                            break;
                        case "sum_data.tot_aprl_sum":  		//4~7월 총합계//,
                            struct_G108M.NStot_aprl_sum = strValue;
                            break;
                        case "sum_data.gnrl_jan_sum":  		//그 외 일반합계//,
                            struct_G108M.NSgnrl_jan_sum = strValue;
                            break;
                        case "sum_data.tdmr_jan_sum":  		//그 외 전통시장합계//,
                            struct_G108M.NStdmr_jan_sum = strValue;
                            break;
                        case "sum_data.trp_jan_sum":   		//그 외 대중교통합계//,
                            struct_G108M.NStrp_jan_sum = strValue;
                            break;
                        case "sum_data.isld_jan_sum":  		//그 외 도서공연등합계//,
                            struct_G108M.NSisld_jan_sum = strValue;
                            break;
                        case "sum_data.tot_jan_sum":   		//그 외 총합계//,
                            struct_G108M.NStot_jan_sum = strValue;
                            break;
                        case "data.sum":
                            struct_G108M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_G108M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G109M":  //2021년 신용카드 상세내역
                case "G109M":  //2021년 신용카드 상세내역
                case "G309M":  //2021년 직불카드 상세내역
                case "G409M":  //2021년 제로페이 상세내역
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G110M.NSform_cd = strValue;
                            break;
                        case "man.resid":
                            struct_G110M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G110M.NSdat_cd = strValue;
                            break;
                        case "data.busnid":
                            struct_G110M.NSbusnid = strValue;
                            break;
                        case "data.use_place_cd":
                            struct_G110M.NSuse_place_cd = strValue;
                            break;
                        case "amt.mm":
                            struct_G110M.NSamtmm = strValue;
                            break;
                        case "man.name":
                            struct_G110M.NSname = strValue;
                            break;
                        case "data.trade_nm":
                            struct_G110M.NStrade_nm = strValue;
                            break;
                        case "sum_data.tot_pre_year_sum":   		//2020년 일반합계//,
                            struct_G110M.NSgnrl_sum = strValue;
                            break;
                        case "sum_data.tot_curr_year_sum":   		//2021년 일반합계//,
                            struct_G110M.NStdmr_sum = strValue;
                            break;
                        case "sum_data.gnrl_sum":   		//2020년 일반합계//,
                            struct_G110M.NSgnrl_sum = strValue;
                            break;
                        case "sum_data.tdmr_sum":   		//2020년 전통시장합계//,
                            struct_G110M.NStdmr_sum = strValue;
                            break;
                        case "sum_data.trp_sum":    		//2020년 대중교통합계//,
                            struct_G110M.NStrp_sum = strValue;
                            break;
                        case "sum_data.isld_sum":   		//2020년 도서공연등합계//,
                            struct_G110M.NSisld_sum = strValue;
                            break;
                        case "sum_data.tot_sum":    		//2020년 총합계//,
                            struct_G110M.NStot_sum = strValue;
                            break;                      
                        case "data.sum":
                            struct_G110M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_G110M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G208M":  //2020년 현금영수증 상세내역
                case "G208M":  //2020년 현금영수증 상세내역                
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G208M.NSform_cd = strValue;
                            break;
                        case "man.resid":  //주민번호
                            struct_G208M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G208M.NSdat_cd = strValue;
                            break;
                        case "data.use_place_cd":  //종류
                            struct_G208M.NSuse_place_cd = strValue;
                            break;
                        case "amt.mm":  //공제월
                            struct_G208M.NSamtmm = strValue;
                            break;
                        case "man.name":  //성명
                            struct_G208M.NSname = strValue;
                            break;
                        case "sum_data.gnrl_sum":   		//2020년 일반합계//,
                            struct_G208M.NSgnrl_sum = strValue;
                            break;
                        case "sum_data.tdmr_sum":   		//2020년 전통시장합계//,
                            struct_G208M.NStdmr_sum = strValue;
                            break;
                        case "sum_data.trp_sum":    		//2020년 대중교통합계//,
                            struct_G208M.NStrp_sum = strValue;
                            break;
                        case "sum_data.isld_sum":   		//2020년 도서공연등합계//,
                            struct_G208M.NSisld_sum = strValue;
                            break;
                        case "sum_data.tot_sum":    		//2020년 총합계//,
                            struct_G208M.NStot_sum = strValue;
                            break;
                        case "sum_data.gnrl_mar_sum":  		//3월분 일반합계//,
                            struct_G208M.NSgnrl_mar_sum = strValue;
                            break;
                        case "sum_data.tdmr_mar_sum":  		//3월분 전통시장합계//,
                            struct_G208M.NStdmr_mar_sum = strValue;
                            break;
                        case "sum_data.trp_mar_sum":   		//3월분 대중교통합계//,
                            struct_G208M.NStrp_mar_sum = strValue;
                            break;
                        case "sum_data.isld_mar_sum":  		//3월분 도서공연등합계//,
                            struct_G208M.NSisld_mar_sum = strValue;
                            break;
                        case "sum_data.tot_mar_sum":   		//3월분 총합계//,
                            struct_G208M.NStot_mar_sum = strValue;
                            break;
                        case "sum_data.gnrl_aprl_sum": 		//4~7월 일반합계//,
                            struct_G208M.NSgnrl_aprl_sum = strValue;
                            break;
                        case "sum_data.tdmr_aprl_sum": 		//4~7월 전통시장합계//,
                            struct_G208M.NStdmr_aprl_sum = strValue;
                            break;
                        case "sum_data.trp_aprl_sum":  		//4~7월 대중교통합계//,
                            struct_G208M.NStrp_aprl_sum = strValue;
                            break;
                        case "sum_data.isld_aprl_sum": 		//4~7월 도서공연등합계//,
                            struct_G208M.NSisld_aprl_sum = strValue;
                            break;
                        case "sum_data.tot_aprl_sum":  		//4~7월 총합계//,
                            struct_G208M.NStot_aprl_sum = strValue;
                            break;
                        case "sum_data.gnrl_jan_sum":  		//그 외 일반합계//,
                            struct_G208M.NSgnrl_jan_sum = strValue;
                            break;
                        case "sum_data.tdmr_jan_sum":  		//그 외 전통시장합계//,
                            struct_G208M.NStdmr_jan_sum = strValue;
                            break;
                        case "sum_data.trp_jan_sum":   		//그 외 대중교통합계//,
                            struct_G208M.NStrp_jan_sum = strValue;
                            break;
                        case "sum_data.isld_jan_sum":  		//그 외 도서공연등합계//,
                            struct_G208M.NSisld_jan_sum = strValue;
                            break;
                        case "sum_data.tot_jan_sum":   		//그 외 총합계//,
                            struct_G208M.NStot_jan_sum = strValue;
                            break;
                        case "data.sum":  //공제대상금액
                            struct_G208M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_G208M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : "G209M":  //2021년 현금영수증 상세내역
                case "G209M":  //2021년 현금영수증 상세내역                
                case "G210M":  //2022년 현금영수증 상세내역                
                    switch (AttName)
                    {
                        case "form.form_cd":
                            struct_G210M.NSform_cd = strValue;
                            break;
                        case "man.resid":  //주민번호
                            struct_G210M.NSresid = strValue;
                            break;
                        case "data.dat_cd":
                            struct_G210M.NSdat_cd = strValue;
                            break;
                        case "data.use_place_cd":  //종류
                            struct_G210M.NSuse_place_cd = strValue;
                            break;
                        case "amt.mm":  //공제월
                            struct_G210M.NSamtmm = strValue;
                            break;
                        case "man.name":  //성명
                            struct_G210M.NSname = strValue;
                            break;
                        case "sum_data.tot_pre_year_sum":   		//2020년 일반합계//,
                            struct_G210M.NStot_pre_year_sum = strValue;
                            break;
                        case "sum_data.tot_curr_year_sum":   		//2021년 일반합계//,
                            struct_G210M.NStot_curr_year_sum = strValue;
                            break;
                        case "sum_data.tdmr_tot_pre_year_sum":   		//2021년 전통시장 사용금액 합계',
                            struct_G210M.NStdmr_tot_pre_year_sum = strValue;
                            break;
                        case "sum_data.tdmr_tot_curr_year_sum":   		//2022년 전통시장 사용금액 합계',
                            struct_G210M.NStdmr_tot_curr_year_sum = strValue;
                            break;
                        case "sum_data.gnrl_sum":   		//2020년 일반합계//,
                            struct_G210M.NSgnrl_sum = strValue;
                            break;
                        case "sum_data.tdmr_sum":   		//2020년 전통시장합계//,
                            struct_G210M.NStdmr_sum = strValue;
                            break;
                        case "sum_data.trp_sum":    		//2020년 대중교통합계//,
                            struct_G210M.NStrp_sum = strValue;
                            break;
                        case "sum_data.isld_sum":   		//2020년 도서공연등합계//,
                            struct_G210M.NSisld_sum = strValue;
                            break;
                        case "sum_data.tot_sum":    		//2020년 총합계//,
                            struct_G210M.NStot_sum = strValue;
                            break;
                        case "sum_data.tfhy_gnrl_sum":    		//2022년  상반기 일반합계
                            struct_G210M.NStfhy_gnrl_sum = strValue;
                            break;
                        case "sum_data.tfhy_tdmr_sum":    		//2022년  상반기 전통시장합계',   
                            struct_G210M.NStfhy_tdmr_sum = strValue;
                            break;
                        case "sum_data.tfhy_trp_sum":    		//2022년  상반기 대중교통합계',   
                            struct_G210M.NStfhy_trp_sum = strValue;
                            break;
                        case "sum_data.tfhy_isld_sum":    		//2022년  상반기 도서공연등합계', 
                            struct_G210M.NStfhy_isld_sum = strValue;
                            break;
                        case "sum_data.tfhy_tot_sum":    		//2022년  상반기 총합계',         
                            struct_G210M.NStfhy_tot_sum = strValue;
                            break;
                        case "sum_data.shfy_gnrl_sum":    		//2022년  하반기 일반합계',       
                            struct_G210M.NSshfy_gnrl_sum = strValue;
                            break;
                        case "sum_data.shfy_tdmr_sum":    		//2022년  하반기 전통시장합계',   
                            struct_G210M.NSshfy_tdmr_sum = strValue;
                            break;
                        case "sum_data.shfy_trp_sum":    		//2022년  하반기 대중교통합계',   
                            struct_G210M.NSshfy_trp_sum = strValue;
                            break;
                        case "sum_data.shfy_isld_sum":    		//2022년  하반기 도서공연등합계', 
                            struct_G210M.NSshfy_isld_sum = strValue;
                            break;
                        case "sum_data.shfy_tot_sum":    		//2022년  하반기 총합계',
                            struct_G210M.NSshfy_tot_sum = strValue;
                            break;
                        case "data.sum":  //공제대상금액
                            struct_G210M.NSsum = strValue;
                            break;
                        case "amt.amt":
                            struct_G210M.NSamt = strValue;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion

                #region  Description : //"B201Y": 실손보험
                case "B201Y":                   
                        switch (AttName)
                        {
                            case "form.form_cd":
                                struct_B201Y.NSform_cd = strValue;
                                break;
                            case "man.resid":  //주민등록번호
                                struct_B201Y.NSresid = strValue;
                                break;
                            case "data.dat_cd":
                                struct_B201Y.NSdat_cd = strValue;
                                break;
                            case "data.busnid":  //사업자번호
                                struct_B201Y.NSbusnid = strValue;
                                break;
                            case "man.name":   //성명
                                struct_B201Y.NSname = strValue;
                                break;
                            case "data.trade_nm":  //상호
                                struct_B201Y.NStrade_nm = strValue;
                                break;
                            case "data.acc_no":  //증권번호
                                struct_B201Y.NSAcc_No = strValue.Replace("-", "").Trim();
                                break;
                            case "data.goods_nm":  //보험종류
                                struct_B201Y.NSGoods_nm = strValue;
                                break;
                            case "data.insu_resid":  //피보험자주민번호
                                struct_B201Y.NSInsu_resid = strValue;
                                break;
                            case "data.insu_nm":  //피보험자성명
                                struct_B201Y.NSInsu_nm = strValue;
                                break;
                            case "data.sum":   //수령금액계
                                struct_B201Y.NSsum = strValue;
                                break;
                            default:
                                break;
                        }                    
                    break;
                #endregion

                default:                    
                    break;
            }
        }
        #endregion

        #region  Description : 구조체 변수 값 -> Data List 저장
        private void UP_Set_StructToData(string sform_cd)
        {
            switch (sform_cd)
            {
                case "A101Y":
                    data_A101Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,                                                
                                                this._CBH01_KBSABUN_Value,
	                                            struct_A101Y.man_form_cd,
                                                struct_A101Y.man_resnoEncCntn,   
                                                struct_A101Y.man_fnm,
                                                struct_A101Y.man_tnm,	           
                                                struct_A101Y.man_bsnoEncCntn,
                                                struct_A101Y.man_hshrClCd,	   
                                                struct_A101Y.man_rsplNtnInfr,	   
                                                struct_A101Y.man_dtyStrtDt,	   
                                                struct_A101Y.man_dtyEndDt,	   
                                                struct_A101Y.man_reStrtDt,	   
                                                struct_A101Y.man_reEndDt,	   
                                                struct_A101Y.man_rsdtClCd,	   
                                                struct_A101Y.man_inctxWhtxTxamtMetnCd,  
                                                struct_A101Y.man_inpmYn,
                                                struct_A101Y.man_prifChngYn,
                                                struct_A101Y.data_npHthrWaInfeeAmt,	    
                                                struct_A101Y.data_npHthrWaInfeeDdcAmt,	
                                                struct_A101Y.data_npHthrMcurWkarInfeeAmt,	
                                                struct_A101Y.data_npHthrMcurWkarDdcAmt,	
                                                struct_A101Y.data_hthrPblcPnsnInfeeAmt,	
                                                struct_A101Y.data_hthrPblcPnsnInfeeDdcAmt,
                                                struct_A101Y.data_mcurPblcPnsnInfeeAmt,	
                                                struct_A101Y.data_mcurPblcPnsnInfeeDdcAmt,
                                                struct_A101Y.data_pnsnInfeeUseAmtSum,
                                                struct_A101Y.data_pnsnInfeeDdcAmtSum,
                                                struct_A101Y.data_hthrHifeAmt,       
                                                struct_A101Y.data_hthrHifeDdcAmt,    
                                                struct_A101Y.data_mcurHifeAmt,	     
                                                struct_A101Y.data_mcurHifeDdcAmt,    
                                                struct_A101Y.data_hthrUiAmt,	     
                                                struct_A101Y.data_hthrUiDdcAmt,	     
                                                struct_A101Y.data_mcurUiAmt,	     
                                                struct_A101Y.data_mcurUiDdcAmt,	     
                                                struct_A101Y.data_infeeUseAmtSum,    
                                                struct_A101Y.data_infeeDdcAmtSum,    
                                                struct_A101Y.data_brwOrgnBrwnHsngTennLnpbSrmAmt,	
                                                struct_A101Y.data_brwOrgnBrwnHsngTennLnpbSrmDdcAmt,	
                                                struct_A101Y.data_rsdtBrwnHsngTennLnpbSrmAmt,	       
                                                struct_A101Y.data_rsdtBrwnHsngTennLnpbSrmDdcAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr15BlwItrAmt,	       
                                                struct_A101Y.data_lthClrlLnpbYr15BlwDdcAmt,       	
                                                struct_A101Y.data_lthClrlLnpbYr29ItrAmt,          	
                                                struct_A101Y.data_lthClrlLnpbYr29DdcAmt,            	
                                                struct_A101Y.data_lthClrlLnpbY30OverItrAmt,      	
                                                struct_A101Y.data_lthClrlLnpbY30OverDdcAmt,     	
                                                struct_A101Y.data_lthClrlLnpbYr2012AfthY15OverItrAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2012AfthY15OverDdcAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2012EtcBrwItrAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2012EtcBrwDdcAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthFxnIrItrAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthFxnIrDdcAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthY15OverItrAmtItrAmt,
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthY15OverDdcAmtDdcAmt,
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthEtcBrwItrAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthEtcBrwDdcAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthYr15BlwItrAmt,	
                                                struct_A101Y.data_lthClrlLnpbYr2015AfthYr15BlwDdcAmt,	
                                                struct_A101Y.data_hsngFndsDdcAmtSum,	               
                                                struct_A101Y.data_conbCrfwAmtLglUseAmt,	      
                                                struct_A101Y.data_conbCrfwAmtLglDdcAmt,	      
                                                struct_A101Y.data_conbCrfwAmtReliOrgOthUseAmt,
                                                struct_A101Y.data_conbCrfwAmtReliOrgOthDdcAmt,
                                                struct_A101Y.data_conbCrfwAmtReliOrgUseAmt,	
                                                struct_A101Y.data_conbCrfwAmtReliOrgDdcAmt,	
                                                struct_A101Y.data_conbCrfwAmtUseAmtSum,
                                                struct_A101Y.data_conbCrfwAmtDdcAmtSum,
                                                struct_A101Y.data_yr2000BefNtplPnsnSvngUseAmt,	
                                                struct_A101Y.data_yr2000BefNtplPnsnSvngDdcAmt,	
                                                struct_A101Y.data_smceSbizUseAmt,   	        
                                                struct_A101Y.data_smceSbizDdcAmt,             	
                                                struct_A101Y.data_sbcSvngUseAmt, 	        
                                                struct_A101Y.data_sbcSvngDdcAmt, 	        
                                                struct_A101Y.data_lbrrHsngPrptSvngUseAmt,	
                                                struct_A101Y.data_lbrrHsngPrptSvngDdcAmt,	
                                                struct_A101Y.data_hsngSbcSynSvngUseAmt,        	
                                                struct_A101Y.data_hsngSbcSynSvngDdcAmt,        	
                                                struct_A101Y.data_hsngPrptSvngIncUseAmtSum,	
                                                struct_A101Y.data_hsngPrptSvngIncDdcAmtSum,	
                                                struct_A101Y.data_cpivAsctUseAmt2,	
                                                struct_A101Y.data_cpivAsctDdcAmt2,	
                                                struct_A101Y.data_cpivVntUseAmt2,	
                                                struct_A101Y.data_cpivVntDdcAmt2,	
                                                struct_A101Y.data_cpivAsctUseAmt1,	
                                                struct_A101Y.data_cpivAsctDdcAmt1,	
                                                struct_A101Y.data_cpivVntUseAmt1,	
                                                struct_A101Y.data_cpivVntDdcAmt1,	
                                                struct_A101Y.data_cpivAsctUseAmt0,	
                                                struct_A101Y.data_cpivAsctDdcAmt0,	
                                                struct_A101Y.data_cpivVntUseAmt0,	
                                                struct_A101Y.data_cpivVntDdcAmt0,	
                                                struct_A101Y.data_ivcpInvmUseAmtSum,	
                                                struct_A101Y.data_ivcpInvmDdcAmtSum,	
                                                struct_A101Y.data_crdcUseAmt,	        
                                                struct_A101Y.data_drtpCardUseAmt,	
                                                struct_A101Y.data_cshptUseAmt,	       
                                                struct_A101Y.data_tdmrUseAmt,	        
                                                struct_A101Y.data_pbtUseAmt,	        
                                                struct_A101Y.data_crdcSumUseAmt,	
                                                struct_A101Y.data_crdcSumDdcAmt,
                                                struct_A101Y.data_prsCrdcUseAmt1,            
                                                struct_A101Y.data_tyYrPrsCrdcUseAmt,	     
                                                struct_A101Y.data_pyrPrsAddDdcrtUseAmt,	     
                                                struct_A101Y.data_tyShfyPrsAddDdcrtUseAmt,
                                                struct_A101Y.data_emstAsctCntrUseAmt,	      
                                                struct_A101Y.data_emstAsctCntrDdcAmt,	      
                                                struct_A101Y.data_empMntnSnmcLbrrUseAmt,	
                                                struct_A101Y.data_empMntnSnmcLbrrDdcAmt,	
                                                //struct_A101Y.data_lfhItrUseAmt,	
                                                //struct_A101Y.data_lfhItrDdcAmt,	
                                                struct_A101Y.data_ltrmCniSsUseAmt,	
                                                struct_A101Y.data_ltrmCniSsDdcAmt,
                                                struct_A101Y.data_frgrLbrrEntcPupCd,	
                                                struct_A101Y.data_frgrLbrrLbrOfrDt,	
                                                struct_A101Y.data_frgrLbrrReExryDt,	
                                                struct_A101Y.data_frgrLbrrReRcpnDt,	
                                                struct_A101Y.data_frgrLbrrReAlfaSbmsDt,	
                                                struct_A101Y.data_frgrLbrrErinImnRcpnDt,	
                                                struct_A101Y.data_frgrLbrrErinImnSbmsDt,	
                                                struct_A101Y.data_yupSnmcReStrtDt,     
                                                struct_A101Y.data_yupSnmcReEndDt,	
                                                struct_A101Y.data_sctcHpUseAmt,	       
                                                struct_A101Y.data_sctcHpDdcTrgtAmt,	
                                                struct_A101Y.data_sctcHpDdcAmt,	       
                                                struct_A101Y.data_rtpnUseAmt,	       
                                                struct_A101Y.data_rtpnDdcTrgtAmt,	
                                                struct_A101Y.data_rtpnDdcAmt,	       
                                                struct_A101Y.data_pnsnSvngUseAmt,	
                                                struct_A101Y.data_pnsnSvngDdcTrgtAmt,	
                                                struct_A101Y.data_pnsnSvngDdcAmt,	
                                                struct_A101Y.data_pnsnAccUseAmtSum,	
                                                struct_A101Y.data_pnsnAccDdcTrgtAmtSum,
                                                struct_A101Y.data_pnsnAccDdcAmtSum,	 
                                                struct_A101Y.data_cvrgInscUseAmt,	 
                                                struct_A101Y.data_cvrgInscDdcTrgtAmt2,   
                                                struct_A101Y.data_cvrgInscDdcAmt,        
                                                struct_A101Y.data_dsbrEuCvrgUseAmt,      
                                                struct_A101Y.data_dsbrEuCvrgDdcTrgtAmt,  
                                                struct_A101Y.data_dsbrEuCvrgDdcAmt,      
                                                struct_A101Y.data_infeePymUseAmtSum,     
                                                struct_A101Y.data_infeePymDdcTrgtAmtSum, 
                                                struct_A101Y.data_infeePymDdcAmtSum,    
                                                struct_A101Y.data_mdxpsPrsUseAmt,	
                                                struct_A101Y.data_mdxpsPrsDdcTrgtAmt,	 
                                                struct_A101Y.data_mdxpsPrsDdcAmt,	
                                                struct_A101Y.data_mdxpsOthUseAmt,	
                                                struct_A101Y.data_mdxpsOthDdcTrgtAmt,	
                                                struct_A101Y.data_mdxpsOthDdcAmt,	
                                                struct_A101Y.data_mdxpsUseAmtSum,	
                                                struct_A101Y.data_mdxpsDdcTrgtAmtSum,	
                                                struct_A101Y.data_mdxpsDdcAmtSum,	
                                                struct_A101Y.data_scxpsPrsUseAmt,	
                                                struct_A101Y.data_scxpsPrsDdcTrgtAmt,	
                                                struct_A101Y.data_scxpsPrsDdcAmt, 	
                                                struct_A101Y.data_scxpsKidUseAmt, 	
                                                struct_A101Y.data_scxpsKidDdcTrgtAmt,	
                                                struct_A101Y.data_scxpsKidDdcAmt,	
                                                struct_A101Y.data_scxpsStdUseAmt,	
                                                struct_A101Y.data_scxpsStdDdcTrgtAmt,	
                                                struct_A101Y.data_scxpsStdDdcAmt,	
                                                struct_A101Y.data_scxpsUndUseAmt,	
                                                struct_A101Y.data_scxpsUndDdcTrgtAmt,	
                                                struct_A101Y.data_scxpsUndDdcAmt,	
                                                struct_A101Y.data_scxpsDsbrUseAmt,	
                                                struct_A101Y.data_scxpsDsbrDdcTrgtAmt,	
                                                struct_A101Y.data_scxpsDsbrDdcAmt,	
                                                struct_A101Y.data_scxpsKidCount,	
                                                struct_A101Y.data_scxpsStdCount,	
                                                struct_A101Y.data_scxpsUndCount,	
                                                struct_A101Y.data_scxpsDsbrCount,	
                                                struct_A101Y.data_scxpsUseAmtSum,	
                                                struct_A101Y.data_scxpsDdcTrgtAmtSum,	
                                                struct_A101Y.data_scxpsDdcAmtSum,	
                                                struct_A101Y.data_conb10ttswLtUseAmt,	
                                                struct_A101Y.data_conb10ttswLtDdcTrgtAmt,	
                                                struct_A101Y.data_conb10ttswLtDdcAmt,	        
                                                struct_A101Y.data_conb10excsLtUseAmt,	        
                                                struct_A101Y.data_conb10excsLtDdcTrgtAmt,	
                                                struct_A101Y.data_conb10excsLtDdcAmt,	
                                                struct_A101Y.data_conbLglUseAmt,	
                                                struct_A101Y.data_conbLglDdcTrgtAmt,	 
                                                struct_A101Y.data_conbLglDdcAmt,	
                                                struct_A101Y.data_conbEmstAsctUseAmt,	 
                                                struct_A101Y.data_conbEmstAsctDdcTrgtAmt,	
                                                struct_A101Y.data_conbEmstAsctDdcAmt, 	        
                                                struct_A101Y.data_conbReliOrgOthAppnUseAmt,	
                                                struct_A101Y.data_conbReliOrgOthAppnDdcTrgtAmt,	
                                                struct_A101Y.data_conbReliOrgOthAppnDdcAmt,	
                                                struct_A101Y.data_conbReliOrgAppnUseAmt,	
                                                struct_A101Y.data_conbReliOrgAppnDdcTrgtAmt,	
                                                struct_A101Y.data_conbReliOrgAppnDdcAmt,	
                                                struct_A101Y.data_conbUseAmtSum,	
                                                struct_A101Y.data_conbDdcTrgtAmtSum,	
                                                struct_A101Y.data_conbDdcAmtSum,	
                                                struct_A101Y.data_ovrsSurcIncFmt,	
                                                struct_A101Y.data_frgnPmtFcTxamt,	
                                                struct_A101Y.data_frgnPmtWcTxamt,	    
                                                struct_A101Y.data_frgnPmtTxamtTxpNtnNm,	
                                                struct_A101Y.data_frgnPmtTxamtPmtDt,	      
                                                struct_A101Y.data_frgnPmtTxamtAlfaSbmsDt,	
                                                struct_A101Y.data_frgnPmtTxamtAbrdWkarNm,	
                                                struct_A101Y.data_frgnDtyTerm,         
                                                struct_A101Y.data_frgnPmtTxamtRfoNm,	
                                                struct_A101Y.data_hsngTennLnpbUseAmt,	
                                                struct_A101Y.data_hsngTennLnpbDdcAmt,	
                                                struct_A101Y.data_mmrUseAmt,	
                                                struct_A101Y.data_mmrDdcAmt,	
                                                struct_A101Y.data_cd218,	
                                                struct_A101Y.data_cd219,	
                                                struct_A101Y.data_cd220,	
                                                struct_A101Y.data_cd221,	
                                                struct_A101Y.data_cd222,	
                                                struct_A101Y.data_cd223,	
                                                struct_A101Y.data_cd224,	
                                                struct_A101Y.data_cd225,	
                                                struct_A101Y.data_cd226,	
                                                struct_A101Y.data_cd227,
                                                struct_A101Y.data_cd228,
                                                TYUserInfo.EmpNo
                                  });
                    break;
                case "A101M":  
                    data_A101M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,                                                
                                                this._CBH01_KBSABUN_Value,
 	                                            struct_A101Y.man_form_cd,
                                                struct_A101Y.man_resnoEncCntn,   
                                                struct_A101Y.data_seq,
	                                            struct_A101Y.data_suptFmlyRltClCd,
                                                struct_A101Y.data_txprDscmNoCntn,
                                                struct_A101Y.data_nnfClCd,
                                                struct_A101Y.data_child,
                                                struct_A101Y.data_txprNm,
                                                struct_A101Y.data_bscDdcClCd,
                                                struct_A101Y.data_wmnDdcClCd,
                                                struct_A101Y.data_snprntFmlyDdcClCd,
                                                struct_A101Y.data_sccDdcClCd,
                                                struct_A101Y.data_dsbrDdcClCd,
                                                struct_A101Y.data_chbtAtprDdcClCd,
                                                struct_A101Y.data_age6Lt,
                                                struct_A101Y.data_cdVvalKrnNm,
                                                struct_A101Y.data_hifeDdcTrgtAmt,
                                                struct_A101Y.data_cvrgInscDdcTrgtAmt,
                                                struct_A101Y.data_dsbrDdcTrgtAmt,
                                                struct_A101Y.data_mdxpsDdcTrgtAmt,
                                                struct_A101Y.data_scxpsDdcTrgtAmt,
                                                struct_A101Y.data_crdcDdcTrgtAmt,
                                                struct_A101Y.data_drtpCardDdcTrgtAmt,
                                                struct_A101Y.data_cshptDdcTrgtAmt,
                                                struct_A101Y.data_tdmrDdcTrgtAmt,
                                                struct_A101Y.data_pbtDdcTrgtAmt,
                                                struct_A101Y.data_conbDdcTrgtAmt,                                      
                                                TYUserInfo.EmpNo
                                  });
                    break;
                    
                case "A102Y":  //보험료(보장성,장애인보장성)
                    if (UP_Get_FamilyCheck(struct_A102Y.NSresid))
                    {
                        data_A102Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,                                                
                                                this._CBH01_KBSABUN_Value,
	                                            struct_A102Y.NSform_cd	,
	                                            struct_A102Y.NSdat_cd	,
	                                            struct_A102Y.NSresid	        ,
                                                TYUserInfo.SecureKey,
	                                            struct_A102Y.NSbusnid	,
	                                            struct_A102Y.NSacc_no	,
	                                            struct_A102Y.NSname	        ,
	                                            struct_A102Y.NStrade_nm	,
	                                            struct_A102Y.NSgoods_nm	,
	                                            struct_A102Y.NSinsu1_resid	,
	                                            struct_A102Y.NSinsu1_nm	,
	                                            struct_A102Y.NSinsu2_resid_1	,
	                                            struct_A102Y.NSinsu2_nm_1	,
	                                            struct_A102Y.NSinsu2_resid_2	,
	                                            struct_A102Y.NSinsu2_nm_2	,
	                                            struct_A102Y.NSinsu2_resid_3	,
	                                            struct_A102Y.NSinsu2_nm_3	,
	                                            struct_A102Y.NSsum	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "A102M": //보험료(보장성,장애인보장성)
                    if (UP_Get_FamilyCheck(struct_A102M.NSresid))
                    {
                        data_A102M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_A102M.NSform_cd	,
	                                            struct_A102M.NSdat_cd	,
	                                            struct_A102M.NSresid	        ,
	                                            struct_A102M.NSbusnid	,
	                                            struct_A102M.NSacc_no	,
                                                struct_A102M.NSamtmm	,
	                                            struct_A102M.NSname	        ,
	                                            struct_A102M.NStrade_nm	,
	                                            struct_A102M.NSgoods_nm	,
	                                            struct_A102M.NSinsu1_resid	,
	                                            struct_A102M.NSinsu1_nm	,
	                                            struct_A102M.NSinsu2_resid_1	,
	                                            struct_A102M.NSinsu2_nm_1	,
	                                            struct_A102M.NSinsu2_resid_2	,
	                                            struct_A102M.NSinsu2_nm_2	,
	                                            struct_A102M.NSinsu2_resid_3	,
	                                            struct_A102M.NSinsu2_nm_3	,
	                                            struct_A102M.NSsum	 ,
                                                struct_A102M.NSfix_cd,
                                                struct_A102M.NSamt	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "B101Y":
                    if (fbGonJeDoc)
                    {
                        //공제신고서 B101Y-연금저축등 소득.세액 공제명세 MASTER
                        data_Doc_B101Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,
	                                            struct_Doc_B101Y.tnm	,
	                                            struct_Doc_B101Y.fnm	     ,
	                                            struct_Doc_B101Y.adr	,
	                                            struct_Doc_B101Y.pfbAdr	 ,
                                                TYUserInfo.EmpNo
                                  });

                    }
                    else
                    {
                        if (UP_Get_FamilyCheck(struct_B101Y.NSresid))
                        {
                            //의료비
                            data_B101Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_B101Y.NSform_cd	,
	                                            struct_B101Y.NSdat_cd	,
	                                            struct_B101Y.NSresid	        ,
                                                TYUserInfo.SecureKey,
	                                            struct_B101Y.NSbusnid	,
	                                            struct_B101Y.NSname	     ,
	                                            struct_B101Y.NStrade_nm	,
                                                struct_B101Y.NSMdCode,
                                                struct_B101Y.NScnt,
                                                struct_B101Y.NSmdxPrsGn,
	                                            struct_B101Y.NSsum	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }
                    }
                    break;
                case "B101R":  //공제신고서 B101Y-연금저축등 소득.세액 공제명세(퇴직연금)
                    if (fbGonJeDoc)
                    {
                        if (struct_Doc_B101R.rtpnAccRtpnCl != "" && struct_Doc_B101R.rtpnFnnOrgnCd != "")
                        {
                            data_Doc_B101R.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,	                                    
	                                            struct_Doc_B101R.rtpnAccRtpnCl  ,
	                                            struct_Doc_B101R.rtpnFnnOrgnCd	,
	                                            struct_Doc_B101R.rtpnAccFnnCmp	 ,
                                                struct_Doc_B101R.rtpnAccAccno	 ,
                                                struct_Doc_B101R.rtpnAccPymAmt	 ,
                                                struct_Doc_B101R.rtpnAccTxamtDdcAmt	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }

                    }
                    break;
                case "B101P": //공제신고서 B101Y-연금저축등 소득.세액 공제명세(연금저축)
                    if (fbGonJeDoc)
                    {
                        if (struct_Doc_B101P.pnsnSvngAccPnsnSvngCl != "" && struct_Doc_B101P.pnsnSvngFnnOrgnCd != "")
                        {
                            data_Doc_B101P.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,	                                            
	                                            struct_Doc_B101P.pnsnSvngAccPnsnSvngCl	     ,
	                                            struct_Doc_B101P.pnsnSvngFnnOrgnCd	,
	                                            struct_Doc_B101P.pnsnSvngAccFnnCmp	 ,
                                                struct_Doc_B101P.pnsnSvngAccAccno	 ,
                                                struct_Doc_B101P.pnsnSvngAccPymAmt	 ,
                                                struct_Doc_B101P.ipnsnSvngAccNcTxamtDdcAmt	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }
                    }
                    break;
                case "B101H": //공제신고서 B101Y-연금저축등 소득.세액 공제명세(주택마련저축)
                    if (fbGonJeDoc)
                    {
                        if (struct_Doc_B101H.hsngPrptSvngSvngCl != "" && struct_Doc_B101H.hsngPrptSvngFnnOrgnCd != "")
                        {
                            data_Doc_B101H.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
                                                struct_Doc_B101Y.resnoEncCntn   ,
	                                            struct_Doc_B101H.hsngPrptSvngSvngCl	     ,
	                                            struct_Doc_B101H.jnngDt	,
	                                            struct_Doc_B101H.hsngPrptSvngFnnOrgnCd	 ,
                                                struct_Doc_B101H.hsngPrptSvngFnnCmp	 ,
                                                struct_Doc_B101H.hsngPrptSvngAccno	 ,
                                                struct_Doc_B101H.hsngPrptSvngPymAmt	 ,
                                                struct_Doc_B101H.hsngPrptSvngIncDdcAmt	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }
                    }
                    break;
                case "B101I": //공제신고서 B101Y-연금저축등 소득.세액 공제명세(장기집합투자)
                    if (fbGonJeDoc)
                    {
                        if (struct_Doc_B101L.ltrmCniSsfnnOrgnCd != "")
                        {
                            data_Doc_B101L.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_Doc_B101Y.form_cd	,
	                                            struct_Doc_B101Y.bsnoEncCntn	,
	                                            struct_Doc_B101Y.resnoEncCntn   ,	                                            
	                                            struct_Doc_B101L.ltrmCniSsfnnOrgnCd	     ,
	                                            struct_Doc_B101L.ltrmCniSsFnnCmp	,
	                                            struct_Doc_B101L.ltrmCniSsAccno	 ,
                                                struct_Doc_B101L.ltrmCniSsPymAmt	 ,
                                                struct_Doc_B101L.ltrmCniSsIncDdcAmt	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }
                    }
                    break;
                case "B101D":  //의료비
                    if (UP_Get_FamilyCheck(struct_B101D.NSresid))
                    {
                        data_B101D.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_B101D.NSform_cd	,
	                                            struct_B101D.NSdat_cd	,
	                                            struct_B101D.NSresid	        ,
	                                            struct_B101D.NSbusnid	,
                                                struct_B101D.NSamtdd	 ,
	                                            struct_B101D.NSname	     ,
	                                            struct_B101D.NStrade_nm	,
	                                            struct_B101D.NSsum	 ,
                                                struct_B101D.NSamt	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "C102Y":
                    if (fbGonJeDoc)
                    {

                    }
                    else
                    {
                        if (UP_Get_FamilyCheck(struct_C102Y.NSresid))
                        {
                            data_C102Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C102Y.NSform_cd	,
	                                            struct_C102Y.NSdat_cd	,
	                                            struct_C102Y.NSresid	        ,
                                                TYUserInfo.SecureKey,
	                                            struct_C102Y.NSbusnid	,
	                                            struct_C102Y.NSname	     ,
	                                            struct_C102Y.NStrade_nm	,
                                                struct_C102Y.NSedu_tp,
                                                struct_C102Y.NSedu_cl,
	                                            struct_C102Y.NSsum	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }
                    }
                    break;
                case "C102M":
                    if (UP_Get_FamilyCheck(struct_C102M.NSresid))
                    {
                        data_C102M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C102M.NSform_cd	,
	                                            struct_C102M.NSdat_cd	,
	                                            struct_C102M.NSresid	        ,
	                                            struct_C102M.NSbusnid	,
                                                struct_C102M.NSedu_tp,
                                                struct_C102M.NSamtmm,
	                                            struct_C102M.NSname	     ,
	                                            struct_C102M.NStrade_nm	,                                                
	                                            struct_C102M.NSsum	 ,
                                                struct_C102M.NSamt	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "C202Y":
                    if (UP_Get_FamilyCheck(struct_C202Y.NSresid))
                    {
                        data_C202Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C202Y.NSform_cd	,
	                                            struct_C202Y.NSdat_cd	,
	                                            struct_C202Y.NSresid	        ,
                                                TYUserInfo.SecureKey,
	                                            struct_C202Y.NSbusnid	,
                                                struct_C202Y.NScourse_cd,
	                                            struct_C202Y.NSname	     ,
	                                            struct_C202Y.NStrade_nm	,                                                
                                                struct_C202Y.NSsubject_nm	,                                                
	                                            struct_C202Y.NSsum	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "C202M":
                    if (UP_Get_FamilyCheck(struct_C202M.NSresid))
                    {
                        data_C202M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C202M.NSform_cd	,
	                                            struct_C202M.NSdat_cd	,
	                                            struct_C202M.NSresid	        ,
	                                            struct_C202M.NSbusnid	,
                                                struct_C202M.NScourse_cd,                                                
                                                struct_C202M.NSamtmm,
	                                            struct_C202M.NSname	     ,
	                                            struct_C202M.NStrade_nm	,        
                                                struct_C202M.NSsubject_nm	,                                                
	                                            struct_C202M.NSsum	 ,
                                                struct_C202M.NSamt	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "C301Y":
                    if (UP_Get_FamilyCheck(struct_C301Y.NSresid))
                    {
                        data_C301Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C301Y.NSform_cd	,
	                                            struct_C301Y.NSdat_cd	,
	                                            struct_C301Y.NSresid	        ,
                                                TYUserInfo.SecureKey,
	                                            struct_C301Y.NSbusnid	,
	                                            struct_C301Y.NSname	     ,
	                                            struct_C301Y.NStrade_nm	,                                                
	                                            struct_C301Y.NSsum	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "C301M":
                    if (UP_Get_FamilyCheck(struct_C301M.NSresid))
                    {
                        data_C301M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C301M.NSform_cd	,
	                                            struct_C301M.NSdat_cd	,
	                                            struct_C301M.NSresid	        ,
	                                            struct_C301M.NSbusnid	,
                                                struct_C301M.NSamtmm	,
	                                            struct_C301M.NSname	     ,
	                                            struct_C301M.NStrade_nm	,                                                
	                                            struct_C301M.NSsum	 ,
                                                struct_C301M.NSamt	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "C401Y":                    
                        if (UP_Get_FamilyCheck(struct_C401Y.NSresid))
                        {
                            data_C401Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C401Y.NSform_cd	,
	                                            struct_C401Y.NSdat_cd	,
	                                            struct_C401Y.NSresid    ,
                                                TYUserInfo.SecureKey,
	                                            struct_C401Y.NSbusnid	,
	                                            struct_C401Y.NSname	     ,
	                                            struct_C401Y.NStrade_nm	,
	                                            struct_C401Y.NSsum	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }                    
                    break;
                case "C401M":
                    if (UP_Get_FamilyCheck(struct_C401M.NSresid))
                    {
                        data_C401M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_C401M.NSform_cd	,
	                                            struct_C401M.NSdat_cd	,
	                                            struct_C401M.NSresid	        ,
	                                            struct_C401M.NSbusnid	,
                                                struct_C401M.NSamtmm,
	                                            struct_C401M.NSname	     ,
	                                            struct_C401M.NStrade_nm	,                                                
	                                            struct_C401M.NSsum	 ,
                                                struct_C401M.NSamt	 ,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "D101Y":
                    if (fbGonJeDoc)
                    {
                    }
                    else
                    {
                        if (UP_Get_FamilyCheck(struct_D101Y.NSresid))
                        {
                            data_D101Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_D101Y.NSform_cd	,
	                                            struct_D101Y.NSdat_cd	,
	                                            struct_D101Y.NSresid    ,
                                                TYUserInfo.SecureKey,
	                                            struct_D101Y.NSbusnid	,
                                                struct_D101Y.NSacc_no,
	                                            struct_D101Y.NSname	     ,
	                                            struct_D101Y.NStrade_nm	,
                                                struct_D101Y.NSstart_dt,
                                                struct_D101Y.NSend_dt,
                                                struct_D101Y.NScom_cd,
	                                            struct_D101Y.NSsum	 ,
                                                TYUserInfo.EmpNo
                                  });
                        }
                    }
                    break;
                case "D101M":
                    if (UP_Get_FamilyCheck(struct_D101M.NSresid))
                    {
                        data_D101M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_D101M.NSform_cd	,
	                                            struct_D101M.NSdat_cd	,
	                                            struct_D101M.NSresid	        ,
	                                            struct_D101M.NSbusnid	,
                                                struct_D101M.NSacc_no ,
                                                struct_D101M.NSamtmm,
	                                            struct_D101M.NSname	     ,
	                                            struct_D101M.NStrade_nm	,
                                                struct_D101M.NSstart_dt,
                                                struct_D101M.NSend_dt,
                                                struct_D101M.NScom_cd,
                                                struct_D101M.NSfix_cd,
	                                            struct_D101M.NSsum	 ,
                                                struct_D101M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "E102Y":
                case "E103Y":
                    if (UP_Get_FamilyCheck(struct_E102Y.NSresid))
                    {
                    data_E102Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_E102Y.NSform_cd	,
	                                            struct_E102Y.NSdat_cd	,
	                                            struct_E102Y.NSresid	        ,
                                                TYUserInfo.SecureKey,
	                                            struct_E102Y.NSbusnid	,
                                                struct_E102Y.NSacc_no ,
	                                            struct_E102Y.NSname	     ,
	                                            struct_E102Y.NStrade_nm	,
                                                struct_E102Y.NScom_cd,
	                                            struct_E102Y.NSann_tot_amt	 ,
                                                struct_E102Y.NStax_year_amt	 ,
                                                struct_E102Y.NSddct_bs_ass_amt	 ,
                                                struct_E102Y.NSisa_ann_tot_amt,
                                                struct_E102Y.NSisa_tax_year_amt,
                                                struct_E102Y.NSisa_ddct_bs_ass_amt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "E102M":
                case "E103M":
                    if (UP_Get_FamilyCheck(struct_E102Y.NSresid))
                    {
                        data_E102Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_E102Y.NSform_cd	,
	                                            struct_E102Y.NSdat_cd	,
	                                            struct_E102Y.NSresid	        ,
	                                            struct_E102Y.NSbusnid	,
                                                struct_E102Y.NSacc_no,
	                                            struct_E102Y.NSname	     ,
	                                            struct_E102Y.NStrade_nm	,
                                                struct_E102Y.NScom_cd,
	                                            struct_E102Y.NSann_tot_amt	 ,
                                                struct_E102Y.NStax_year_amt	 ,
                                                struct_E102Y.NSddct_bs_ass_amt	 ,
                                                struct_E102Y.NSisa_ann_tot_amt,
                                                struct_E102Y.NSisa_tax_year_amt,
                                                struct_E102Y.NSisa_ddct_bs_ass_amt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "F102Y":
                case "F103Y":
                    if (UP_Get_FamilyCheck(struct_F102Y.NSresid))
                    {
                        data_F102Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_F102Y.NSform_cd	,
	                                            struct_F102Y.NSdat_cd	,
	                                            struct_F102Y.NSresid	        ,
                                                TYUserInfo.SecureKey,
	                                            struct_F102Y.NSbusnid	,
                                                struct_F102Y.NSacc_no ,
	                                            struct_F102Y.NSname	     ,
	                                            struct_F102Y.NStrade_nm	,
                                                struct_F102Y.NScom_cd,
                                                struct_F102Y.NSpension_cd,
	                                            struct_F102Y.NSann_tot_amt	 ,
                                                struct_F102Y.NStax_year_amt	 ,
                                                struct_F102Y.NSddct_bs_ass_amt	 ,
                                                struct_F102Y.NSisa_ann_tot_amt,
                                                struct_F102Y.NSisa_tax_year_amt,
                                                struct_F102Y.NSisa_ddct_bs_ass_amt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "F102M":
                case "F103M":
                    if (UP_Get_FamilyCheck(struct_F102Y.NSresid))
                    {
                        data_F102Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_F102Y.NSform_cd	,
	                                            struct_F102Y.NSdat_cd	,
	                                            struct_F102Y.NSresid	        ,
	                                            struct_F102Y.NSbusnid	,
                                                struct_F102Y.NSacc_no,
	                                            struct_F102Y.NSname	     ,
	                                            struct_F102Y.NStrade_nm	,
                                                struct_F102Y.NScom_cd,
                                                struct_F102Y.NSpension_cd,
	                                            struct_F102Y.NSann_tot_amt	 ,
                                                struct_F102Y.NStax_year_amt	 ,
                                                struct_F102Y.NSddct_bs_ass_amt	 ,
                                                struct_F102Y.NSisa_ann_tot_amt,
                                                struct_F102Y.NSisa_tax_year_amt,
                                                struct_F102Y.NSisa_ddct_bs_ass_amt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G106Y":
                case "G107Y":                
                    if (UP_Get_FamilyCheck(struct_G106Y.NSresid))
                    {
                        data_G106Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G106Y.NSform_cd, 
                                                struct_G106Y.NSdat_cd,  
                                                struct_G106Y.NSresid,   
                                                TYUserInfo.SecureKey,
                                                struct_G106Y.NSbusnid,  
                                                struct_G106Y.NSuse_place_cd,
                                                struct_G106Y.NSname,        
                                                struct_G106Y.NStrade_nm,                                                   
                                                struct_G106Y.NSsum,                   
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G106M":
                case "G107M":                
                    if (UP_Get_FamilyCheck(struct_G106M.NSresid))
                    {
                        data_G106M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G106M.NSform_cd, 
                                                struct_G106M.NSdat_cd,  
                                                struct_G106M.NSresid,   
                                                struct_G106M.NSbusnid,  
                                                struct_G106M.NSuse_place_cd,
                                                struct_G106M.NSamtmm,
                                                struct_G106M.NSname,        
                                                struct_G106M.NStrade_nm,                                                  
                                                struct_G106M.NSsum,                
                                                struct_G106M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "G206M":
                case "G207M":
                    if (UP_Get_FamilyCheck(struct_G206M.NSresid))
                    {
                     data_G206M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G206M.NSform_cd, 
                                                struct_G206M.NSdat_cd,  
                                                struct_G206M.NSresid,       
                                                TYUserInfo.SecureKey,
                                                struct_G206M.NSuse_place_cd,
                                                struct_G206M.NSamtmm,
                                                struct_G206M.NSname,                                                                                                        
                                                struct_G206M.NSsum,                
                                                struct_G206M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G306Y":
                case "G307Y":                
                case "G407Y": 
                    if (UP_Get_FamilyCheck(struct_G106Y.NSresid))
                    {
                        data_G106Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G106Y.NSform_cd, 
                                                struct_G106Y.NSdat_cd,  
                                                struct_G106Y.NSresid,   
                                                struct_G106Y.NSbusnid,  
                                                struct_G106Y.NSuse_place_cd,
                                                struct_G106Y.NSname,        
                                                struct_G106Y.NStrade_nm,                                                 
                                                struct_G106Y.NSsum,                   
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G306M":
                case "G307M":                
                    if (UP_Get_FamilyCheck(struct_G106M.NSresid))
                    {
                        data_G106M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G106M.NSform_cd, 
                                                struct_G106M.NSdat_cd,  
                                                struct_G106M.NSresid,   
                                                struct_G106M.NSbusnid,  
                                                struct_G106M.NSuse_place_cd,
                                                struct_G106M.NSamtmm,
                                                struct_G106M.NSname,        
                                                struct_G106M.NStrade_nm,                                                   
                                                struct_G106M.NSsum,                
                                                struct_G106M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J101Y":
                    if (UP_Get_FamilyCheck(struct_J101Y.NSresid))
                    {
                        data_J101Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J101Y.NSform_cd, 
                                                struct_J101Y.NSdat_cd,  
                                                struct_J101Y.NSresid,   
                                                TYUserInfo.SecureKey,
                                                struct_J101Y.NSbusnid,  
                                                struct_J101Y.NSacc_no,
                                                struct_J101Y.NSname,        
                                                struct_J101Y.NStrade_nm,    
                                                struct_J101Y.NSgoods_nm, 	         
                                                struct_J101Y.NSlend_dt, 	 
                                                struct_J101Y.NSsum,                   
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J101M":
                    if (UP_Get_FamilyCheck(struct_J101M.NSresid))
                    {
                        data_J101M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J101M.NSform_cd, 
                                                struct_J101M.NSdat_cd,  
                                                struct_J101M.NSresid,   
                                                struct_J101M.NSbusnid,  
                                                struct_J101M.NSacc_no,
                                                struct_J101M.NSamtmm,
                                                struct_J101M.NSname,        
                                                struct_J101M.NStrade_nm,    
                                                struct_J101M.NSgoods_nm, 	         
                                                struct_J101M.NSlend_dt, 	 
                                                struct_J101M.NSfix_cd,
                                                struct_J101M.NSsum,                   
                                                struct_J101M.NSamt,                   
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J203Y":
                    if (UP_Get_FamilyCheck(struct_J203Y.NSresid))
                    {
                        data_J203Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J203Y.NSform_cd,
                                                struct_J203Y.NSdat_cd,
                                                struct_J203Y.NSresid,
                                                TYUserInfo.SecureKey,
                                                struct_J203Y.NSbusnid,
                                                struct_J203Y.NSacc_no,
                                                struct_J203Y.NSlend_kd,
                                                struct_J203Y.NSname,
                                                struct_J203Y.NStrade_nm,
                                                struct_J203Y.NShouse_take_dt,
                                                struct_J203Y.NSmort_setup_dt,
                                                struct_J203Y.NSstart_dt,
                                                struct_J203Y.NSend_dt,
                                                struct_J203Y.NSrepay_years,
                                                struct_J203Y.NSlend_goods_nm,
                                                struct_J203Y.NSdebt,
                                                struct_J203Y.NSfixed_rate_debt,
                                                struct_J203Y.NSnot_defer_debt,
                                                struct_J203Y.NSthis_year_rede_amt,
                                                struct_J203Y.NSsumddct,
                                                struct_J203Y.NSsum,                                                
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J203M":
                    if (UP_Get_FamilyCheck(struct_J203M.NSresid))
                    {
                        data_J203M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J203M.NSform_cd,
                                                struct_J203M.NSdat_cd,
                                                struct_J203M.NSresid,
                                                struct_J203M.NSbusnid,
                                                struct_J203M.NSacc_no,
                                                struct_J203M.NSlend_kd,
                                                struct_J203M.NSamtmm,
                                                struct_J203M.NSname,
                                                struct_J203M.NStrade_nm,
                                                struct_J203M.NShouse_take_dt,
                                                struct_J203M.NSmort_setup_dt,
                                                struct_J203M.NSstart_dt,
                                                struct_J203M.NSend_dt,
                                                struct_J203M.NSrepay_years,
                                                struct_J203M.NSlend_goods_nm,
                                                struct_J203M.NSdebt,
                                                struct_J203M.NSfixed_rate_debt,
                                                struct_J203M.NSnot_defer_debt,
                                                struct_J203M.NSthis_year_rede_amt,
                                                struct_J203M.NSsumddct,
                                                struct_J203M.NSsum,                                                
                                                struct_J203M.NSamt,                                                
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "J301Y":
                    if (UP_Get_FamilyCheck(struct_J301Y.NSresid))
                    {
                        data_J301Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J301Y.NSform_cd,
                                                struct_J301Y.NSdat_cd,
                                                struct_J301Y.NSresid,
                                                TYUserInfo.SecureKey,
                                                struct_J301Y.NSbusnid,
                                                struct_J301Y.NSacc_no,
                                                struct_J301Y.NSsaving_gubn,
                                                struct_J301Y.NSname,
                                                struct_J301Y.NStrade_nm,
                                                struct_J301Y.NSgoods_nm,
                                                struct_J301Y.NSreg_dt,
                                                struct_J301Y.NScom_cd,
                                                struct_J301Y.NSsum,                                                
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J301M":
                    if (UP_Get_FamilyCheck(struct_J301M.NSresid))
                    {
                        data_J301M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J301M.NSform_cd,
                                                struct_J301M.NSdat_cd,
                                                struct_J301M.NSresid,
                                                struct_J301M.NSbusnid,
                                                struct_J301M.NSacc_no,
                                                struct_J301M.NSsaving_gubn,
                                                struct_J301M.NSamtmm,
                                                struct_J301M.NSname,
                                                struct_J301M.NStrade_nm,
                                                struct_J301M.NSgoods_nm,
                                                struct_J301M.NSreg_dt,
                                                struct_J301M.NScom_cd,
                                                struct_J301M.NSfix_cd,
                                                struct_J301M.NSsum,                                                
                                                struct_J301M.NSamt,                                                
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J401Y":
                    if (UP_Get_FamilyCheck(struct_J401Y.NSresid))
                    {
                        data_J401Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J401Y.NSform_cd,
                                                struct_J401Y.NSdat_cd,
                                                struct_J401Y.NSresid,
                                                TYUserInfo.SecureKey,
                                                struct_J401Y.NSbusnid,
                                                struct_J401Y.NSacc_no,
                                                struct_J401Y.NSname,
                                                struct_J401Y.NStrade_nm,
                                                struct_J401Y.NSlend_dt,
                                                struct_J401Y.NSlend_loan_amt,
                                                struct_J401Y.NSsum,                                                
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J401M":
                    if (UP_Get_FamilyCheck(struct_J401M.NSresid))
                    {
                        data_J401M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J401M.NSform_cd,
                                                struct_J401M.NSdat_cd,
                                                struct_J401M.NSresid,
                                                struct_J401M.NSbusnid,
                                                struct_J401M.NSacc_no,
                                                struct_J401M.NSamtmm,
                                                struct_J401M.NSname,
                                                struct_J401M.NStrade_nm,
                                                struct_J401M.NSlend_dt,
                                                struct_J401M.NSlend_loan_amt,
                                                struct_J401M.NSfix_cd,
                                                struct_J401M.NSsum,
                                                struct_J401M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "K101M":
                    if (UP_Get_FamilyCheck(struct_K101M.NSresid))
                    {
                        data_K101M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_K101M.NSform_cd,
                                                struct_K101M.NSdat_cd,
                                                struct_K101M.NSresid,   
                                                TYUserInfo.SecureKey,
                                                struct_K101M.NSacc_no,
                                                struct_K101M.NSamtmm,
                                                struct_K101M.NSname,
                                                struct_K101M.NSstart_dt,
                                                struct_K101M.NSend_dt,
                                                struct_K101M.NSpay_method,                                                
                                                struct_K101M.NSsum,
                                                struct_K101M.NSsumddct,
                                                struct_K101M.NSdate,
                                                struct_K101M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "L102Y":
                    if (UP_Get_FamilyCheck(struct_L102Y.NSresid))
                    {
                        if (struct_L102Y.NSdonation_cd.Trim() == "20")  //정치자금 기부금
                        {
                            struct_L102Y.NSbusnid = struct_L102Y.NSbusnid.Replace("*", "");
                            struct_L102Y.NSbusnid = struct_L102Y.NSbusnid.Replace("-", "");

                            struct_L102Y.NStrade_nm = struct_L102Y.NStrade_nm.Replace("*", "");
                        }
                                data_L102Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_L102Y.NSform_cd,
                                                struct_L102Y.NSdat_cd,
                                                struct_L102Y.NSresid,     
                                                TYUserInfo.SecureKey,
                                                struct_L102Y.NSbusnid,
                                                struct_L102Y.NSdonation_cd,
                                                struct_L102Y.NSname,
                                                struct_L102Y.NStrade_nm,
                                                struct_L102Y.NSsum,
                                                struct_L102Y.NSsbdy_apln_sum,                                                
                                                struct_L102Y.NSconb_sum,
                                                TYUserInfo.EmpNo
                                  });
                        
                    }
                    break;

                case "L102D":
                    if (UP_Get_FamilyCheck(struct_L102D.NSresid))
                    {
                        data_L102D.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_L102D.NSform_cd,
                                                struct_L102D.NSdat_cd,
                                                struct_L102D.NSresid,                                                
                                                struct_L102D.NSbusnid,
                                                struct_L102D.NSdonation_cd,
                                                struct_L102D.NSamtdd,
                                                struct_L102D.NSname,
                                                struct_L102D.NStrade_nm,
                                                struct_L102D.NSsum,
                                                struct_L102D.NSsbdy_apln_sum,                                                
                                                struct_L102D.NSconb_sum,
                                                struct_L102D.NSamt,
                                                struct_L102D.NSamtapln,                                                
                                                struct_L102D.NSamtsum,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "N101Y":
                case "N102Y":
                    if (UP_Get_FamilyCheck(struct_N101Y.NSresid))
                    {
                        data_N101Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_N101Y.NSform_cd,
                                                struct_N101Y.NSdat_cd,
                                                struct_N101Y.NSresid,    
                                                TYUserInfo.SecureKey,
                                                struct_N101Y.NSbusnid,
                                                struct_N101Y.NSsecu_no,
                                                struct_N101Y.NSname,
                                                struct_N101Y.NStrade_nm,
                                                struct_N101Y.NSfund_nm,
                                                struct_N101Y.NSreg_dt,
                                                struct_N101Y.NSctr_term_mm_cnt,
                                                struct_N101Y.NScom_cd,
                                                struct_N101Y.NSsum,
                                                struct_N101Y.NSddct_bs_ass_amt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "N101M":
                    if (UP_Get_FamilyCheck(struct_N101M.NSresid))
                    {
                        data_N101M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_N101M.NSform_cd,
                                                struct_N101M.NSdat_cd,
                                                struct_N101M.NSresid,                                                
                                                struct_N101M.NSbusnid,
                                                struct_N101M.NSsecu_no,
                                                struct_N101M.NSamtmm,
                                                struct_N101M.NSname,
                                                struct_N101M.NStrade_nm,
                                                struct_N101M.NSfund_nm,
                                                struct_N101M.NSreg_dt,                                                
                                                struct_N101M.NScom_cd,
                                                struct_N101M.NSfix_cd,
                                                struct_N101M.NSsum,
                                                struct_N101M.NSddct_bs_ass_amt,
                                                struct_N101M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "O101M":
                    if (UP_Get_FamilyCheck(struct_O101M.NSresid))
                    {
                        data_O101M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
                                                struct_O101M.NSform_cd,
                                                struct_O101M.NSdat_cd,
                                                struct_O101M.NSresid,
                                                TYUserInfo.SecureKey,
                                                struct_O101M.NSamtmm,
                                                struct_O101M.NSname,
                                                struct_O101M.NSMhi_yrs,
                                                struct_O101M.NSMltrm_yrs,
                                                struct_O101M.NSMhi_ntf,
                                                struct_O101M.NSMltrm_ntf,
                                                struct_O101M.NSMhi_pmt,
                                                struct_O101M.NSMltrm_pmt,
                                                struct_O101M.NSsum,
                                                struct_O101M.NShi_ntf,
                                                struct_O101M.NSltrm_ntf,
                                                struct_O101M.NShi_pmt,
                                                struct_O101M.NSltrm_pmt,
                                                TYUserInfo.EmpNo

                                  });
                    }
                    break;
                case "P102M":
                    if (UP_Get_FamilyCheck(struct_P102M.NSresid))
                    {
                        data_P102M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
                                                struct_P102M.NSform_cd,
                                                struct_P102M.NSdat_cd,
                                                struct_P102M.NSresid,
                                                TYUserInfo.SecureKey,
                                                struct_P102M.NSamtmm,
                                                struct_P102M.NSname,
                                                struct_P102M.NSsum, 
                                                struct_P102M.NSsumsp_ntf,
                                                struct_P102M.NSsumspym,    
                                                struct_P102M.NSsumjlc,     
                                                struct_P102M.NSsumntf,     
                                                struct_P102M.NSsumpmt,     
                                                struct_P102M.NSwrkp_ntf,
                                                struct_P102M.NSrgn_pmt,  
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G108Y":
                case "G308Y":
                case "G408Y":
                    if (UP_Get_FamilyCheck(struct_G108Y.NSresid))
                    {
                        data_G108Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G108Y.NSform_cd, 
                                                struct_G108Y.NSdat_cd,  
                                                struct_G108Y.NSresid,   
                                                struct_G108Y.NSbusnid,  
                                                struct_G108Y.NSuse_place_cd,
                                                struct_G108Y.NSname,        
                                                struct_G108Y.NStrade_nm,           
                                                struct_G108Y.NSgnrl_sum,
	                                            struct_G108Y.NStdmr_sum,
	                                            struct_G108Y.NStrp_sum,
	                                            struct_G108Y.NSisld_sum,
	                                            struct_G108Y.NStot_sum,
	                                            struct_G108Y.NSgnrl_mar_sum,
	                                            struct_G108Y.NStdmr_mar_sum,
	                                            struct_G108Y.NStrp_mar_sum,
	                                            struct_G108Y.NSisld_mar_sum,
	                                            struct_G108Y.NStot_mar_sum,
	                                            struct_G108Y.NSgnrl_aprl_sum,
	                                            struct_G108Y.NStdmr_aprl_sum,
	                                            struct_G108Y.NStrp_aprl_sum,
	                                            struct_G108Y.NSisld_aprl_sum,
	                                            struct_G108Y.NStot_aprl_sum,
	                                            struct_G108Y.NSgnrl_jan_sum,
	                                            struct_G108Y.NStdmr_jan_sum,
	                                            struct_G108Y.NStrp_jan_sum,
	                                            struct_G108Y.NSisld_jan_sum,
	                                            struct_G108Y.NStot_jan_sum,
                                                struct_G108Y.NSsum,                   
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G109Y":
                case "G309Y":
                case "G409Y":
                case "G110Y":
                case "G310Y":
                case "G410Y":
                    if (UP_Get_FamilyCheck(struct_G110Y.NSresid))
                    {
                        data_G110Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G110Y.NSform_cd, 
                                                struct_G110Y.NSdat_cd,  
                                                struct_G110Y.NSresid,   
                                                TYUserInfo.SecureKey,
                                                struct_G110Y.NSbusnid,  
                                                struct_G110Y.NSuse_place_cd,
                                                struct_G110Y.NSname,        
                                                struct_G110Y.NStrade_nm,                                                           
                                                struct_G110Y.NStot_pre_year_sum,
                                                struct_G110Y.NStot_curr_year_sum,
                                                struct_G110Y.NStdmr_tot_pre_year_sum,
                                                struct_G110Y.NStdmr_tot_curr_year_sum,
                                                struct_G110Y.NSgnrl_sum,
	                                            struct_G110Y.NStdmr_sum,
	                                            struct_G110Y.NStrp_sum,
	                                            struct_G110Y.NSisld_sum,
	                                            struct_G110Y.NStot_sum,
                                                struct_G110Y.NStfhy_gnrl_sum,
                                                struct_G110Y.NStfhy_tdmr_sum,
                                                struct_G110Y.NStfhy_trp_sum,
                                                struct_G110Y.NStfhy_isld_sum,
                                                struct_G110Y.NStfhy_tot_sum,
                                                struct_G110Y.NSshfy_gnrl_sum,
                                                struct_G110Y.NSshfy_tdmr_sum,
                                                struct_G110Y.NSshfy_trp_sum,
                                                struct_G110Y.NSshfy_isld_sum,
                                                struct_G110Y.NSshfy_tot_sum,
                                                struct_G110Y.NSsum,                   
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G108M":
                case "G308M":
                case "G408M":
                    if (UP_Get_FamilyCheck(struct_G108M.NSresid))
                    {
                        data_G108M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G108M.NSform_cd, 
                                                struct_G108M.NSdat_cd,  
                                                struct_G108M.NSresid,   
                                                TYUserInfo.SecureKey,
                                                struct_G108M.NSbusnid,  
                                                struct_G108M.NSuse_place_cd,
                                                struct_G108M.NSamtmm,
                                                struct_G108M.NSname,        
                                                struct_G108M.NStrade_nm,     
                                                struct_G108M.NSgnrl_sum,
	                                            struct_G108M.NStdmr_sum,
	                                            struct_G108M.NStrp_sum,
	                                            struct_G108M.NSisld_sum,
	                                            struct_G108M.NStot_sum,
	                                            struct_G108M.NSgnrl_mar_sum,
	                                            struct_G108M.NStdmr_mar_sum,
	                                            struct_G108M.NStrp_mar_sum,
	                                            struct_G108M.NSisld_mar_sum,
	                                            struct_G108M.NStot_mar_sum,
	                                            struct_G108M.NSgnrl_aprl_sum,
	                                            struct_G108M.NStdmr_aprl_sum,
	                                            struct_G108M.NStrp_aprl_sum,
	                                            struct_G108M.NSisld_aprl_sum,
	                                            struct_G108M.NStot_aprl_sum,
	                                            struct_G108M.NSgnrl_jan_sum,
	                                            struct_G108M.NStdmr_jan_sum,
	                                            struct_G108M.NStrp_jan_sum,
	                                            struct_G108M.NSisld_jan_sum,
	                                            struct_G108M.NStot_jan_sum,
                                                struct_G108M.NSsum,                
                                                struct_G108M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G109M":
                case "G309M":
                case "G409M":
                    if (UP_Get_FamilyCheck(struct_G110M.NSresid))
                    {
                        data_G110M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G110M.NSform_cd, 
                                                struct_G110M.NSdat_cd,  
                                                struct_G110M.NSresid,   
                                                TYUserInfo.SecureKey,
                                                struct_G110M.NSbusnid,  
                                                struct_G110M.NSuse_place_cd,
                                                struct_G110M.NSamtmm,
                                                struct_G110M.NSname,        
                                                struct_G110M.NStrade_nm,
                                                struct_G110M.NStot_pre_year_sum,
                                                struct_G110M.NStot_curr_year_sum,
                                                struct_G110M.NSgnrl_sum,
	                                            struct_G110M.NStdmr_sum,
	                                            struct_G110M.NStrp_sum,
	                                            struct_G110M.NSisld_sum,
	                                            struct_G110M.NStot_sum,	                                         
                                                struct_G110M.NSsum,                
                                                struct_G110M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;                
                case "G208M":
                    if (UP_Get_FamilyCheck(struct_G208M.NSresid))
                    {
                        data_G208M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G208M.NSform_cd, 
                                                struct_G208M.NSdat_cd,  
                                                struct_G208M.NSresid,         
                                                TYUserInfo.SecureKey,
                                                struct_G208M.NSuse_place_cd,
                                                struct_G208M.NSamtmm,
                                                struct_G208M.NSname,        
                                                struct_G208M.NSgnrl_sum,
	                                            struct_G208M.NStdmr_sum,
	                                            struct_G208M.NStrp_sum,
	                                            struct_G208M.NSisld_sum,
	                                            struct_G208M.NStot_sum,
	                                            struct_G208M.NSgnrl_mar_sum,
	                                            struct_G208M.NStdmr_mar_sum,
	                                            struct_G208M.NStrp_mar_sum,
	                                            struct_G208M.NSisld_mar_sum,
	                                            struct_G208M.NStot_mar_sum,
	                                            struct_G208M.NSgnrl_aprl_sum,
	                                            struct_G208M.NStdmr_aprl_sum,
	                                            struct_G208M.NStrp_aprl_sum,
	                                            struct_G208M.NSisld_aprl_sum,
	                                            struct_G208M.NStot_aprl_sum,
	                                            struct_G208M.NSgnrl_jan_sum,
	                                            struct_G208M.NStdmr_jan_sum,
	                                            struct_G208M.NStrp_jan_sum,
	                                            struct_G208M.NSisld_jan_sum,
	                                            struct_G208M.NStot_jan_sum,                                                
                                                struct_G208M.NSsum,                
                                                struct_G208M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "G209M":
                case "G210M":
                    if (UP_Get_FamilyCheck(struct_G210M.NSresid))
                    {
                        data_G210M.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_G210M.NSform_cd, 
                                                struct_G210M.NSdat_cd,  
                                                struct_G210M.NSresid,         
                                                TYUserInfo.SecureKey,
                                                struct_G210M.NSuse_place_cd,
                                                struct_G210M.NSamtmm,
                                                struct_G210M.NSname,        
                                                struct_G210M.NStot_pre_year_sum,
                                                struct_G210M.NStot_curr_year_sum,
                                                struct_G210M.NStdmr_tot_pre_year_sum,
                                                struct_G210M.NStdmr_tot_curr_year_sum,
                                                struct_G210M.NSgnrl_sum,
	                                            struct_G210M.NStdmr_sum,
	                                            struct_G210M.NStrp_sum,
	                                            struct_G210M.NSisld_sum,
	                                            struct_G210M.NStot_sum,
                                                struct_G210M.NStfhy_gnrl_sum,
                                                struct_G210M.NStfhy_tdmr_sum,
                                                struct_G210M.NStfhy_trp_sum,
                                                struct_G210M.NStfhy_isld_sum,
                                                struct_G210M.NStfhy_tot_sum,
                                                struct_G210M.NSshfy_gnrl_sum,
                                                struct_G210M.NSshfy_tdmr_sum,
                                                struct_G210M.NSshfy_trp_sum,
                                                struct_G210M.NSshfy_isld_sum,
                                                struct_G210M.NSshfy_tot_sum,
                                                struct_G210M.NSsum,                
                                                struct_G210M.NSamt,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;

                case "J501Y":
                    if (UP_Get_FamilyCheck(struct_J501Y.NSresid))
                    {
                        data_J501Y.Add(new object[] {
                                                fsCOMPY,
                                                this._TXT01_SDATE_Value,
                                                this._CBH01_KBSABUN_Value,
	                                            struct_J501Y.NSform_cd,
                                                struct_J501Y.NSdat_cd,
                                                struct_J501Y.NSresid,
                                                TYUserInfo.SecureKey,
                                                struct_J501Y.NSlsor_no,
                                                TYUserInfo.SecureKey,
                                                struct_J501Y.NSname,
                                                struct_J501Y.NSlsor_nm,
                                                struct_J501Y.NSstart_dt,
                                                struct_J501Y.NSend_dt,
                                                struct_J501Y.NSadr,
                                                struct_J501Y.NSarea,
                                                struct_J501Y.NStypeCd,
                                                struct_J501Y.NSsum,
                                                TYUserInfo.EmpNo
                                  });
                    }
                    break;
                case "B201Y":                   
                    if (UP_Get_FamilyCheck(struct_B201Y.NSresid))
                    {
                        //실손보험
                        data_B201Y.Add(new object[] {
                                            fsCOMPY,
                                            this._TXT01_SDATE_Value,
                                            this._CBH01_KBSABUN_Value,
	                                        struct_B201Y.NSform_cd	,
	                                        struct_B201Y.NSdat_cd	,
	                                        struct_B201Y.NSresid    ,
                                            TYUserInfo.SecureKey,
	                                        struct_B201Y.NSbusnid	,
	                                        struct_B201Y.NSAcc_No   ,
	                                        struct_B201Y.NSname	,
                                            struct_B201Y.NStrade_nm,
                                            struct_B201Y.NSGoods_nm,
                                            struct_B201Y.NSInsu_resid,
                                            struct_B201Y.NSInsu_nm,
	                                        struct_B201Y.NSsum	 ,
                                            TYUserInfo.EmpNo
                                });
                    }                    
                    break;
                default:
                    break;
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
               

        #region  Description : XML 노드 파싱관련 함수

        //▒▒▒ GetSearchNode ▒▒▒
        private XmlNode GetSearchNode(XmlDocument OrgX, string NodeName, string AttName, string strValue)
        {
            XmlNodeList XList = OrgX.GetElementsByTagName(NodeName);
            XmlNode XNode = null;
            if (XList.Count > 0)
            {
                for (int i = 0; i < XList.Count; i++)
                {
                    try
                    {
                        if (XList[i].Attributes[AttName].Value == strValue)
                        {
                            XNode = XList[i];
                            break;
                        }
                    }
                    catch (NullReferenceException e)
                    {

                    }
                }
            }
            return XNode;
        }

        
        // ▒▒▒ GetAttValue ▒▒▒
        private string GetAttValue(XmlNode XNode, string AttName)
        {
            string RetValue = "";
            try
            {
                RetValue = XNode.Attributes[AttName].Value;
            }
            catch (NullReferenceException e)
            {
                RetValue = "";
            }
            return RetValue;
        }       
        #endregion

        #region  Description :  구조체 변수 Clear
        private void UP_Struct_Clear()
        {
            struct_A102Y.NSform_cd = "";
            struct_A102Y.NSdat_cd= "";
            struct_A102Y.NSresid= "";
            struct_A102Y.NSbusnid= "";
            struct_A102Y.NSacc_no= "";
            struct_A102Y.NSname= "";
            struct_A102Y.NStrade_nm= "";
            struct_A102Y.NSgoods_nm= "";
            struct_A102Y.NSinsu1_resid= "";
            struct_A102Y.NSinsu1_nm= "";
            struct_A102Y.NSinsu2_resid_1= "";
            struct_A102Y.NSinsu2_nm_1= "";
            struct_A102Y.NSinsu2_resid_2= "";
            struct_A102Y.NSinsu2_nm_2= "";
            struct_A102Y.NSinsu2_resid_3= "";
            struct_A102Y.NSinsu2_nm_3= "";
            struct_A102Y.NSsum = "";

            struct_A102M.NSform_cd  = "";
            struct_A102M.NSdat_cd = "";
            struct_A102M.NSresid = "";
            struct_A102M.NSbusnid = "";
            struct_A102M.NSacc_no = "";
            struct_A102M.NSamtmm = "";
            struct_A102M.NSname = "";
            struct_A102M.NStrade_nm = "";
            struct_A102M.NSgoods_nm = "";

            struct_A102M.NSinsu1_resid = "";
            struct_A102M.NSinsu1_nm = "";

            struct_A102M.NSinsu2_resid_1 = "";
            struct_A102M.NSinsu2_nm_1 = "";

            struct_A102M.NSinsu2_resid_2 = "";
            struct_A102M.NSinsu2_nm_2 = "";

            struct_A102M.NSinsu2_resid_3 = "";
            struct_A102M.NSinsu2_nm_3 = "";
            struct_A102M.NSsum = "";
            struct_A102M.NSfix_cd = "";
            struct_A102M.NSamt = "";

            struct_B101Y.NSform_cd= "";
            struct_B101Y.NSdat_cd= "";
            struct_B101Y.NSresid= "";
            struct_B101Y.NSbusnid= "";
            struct_B101Y.NSname= "";
            struct_B101Y.NStrade_nm= "";
            struct_B101Y.NSMdCode = "1";
            struct_B101Y.NScnt = "0";
            struct_B101Y.NSmdxPrsGn = "N";
            struct_B101Y.NSsum = "";

            struct_B101D.NSform_cd= "";
            struct_B101D.NSdat_cd= "";
            struct_B101D.NSresid= "";
            struct_B101D.NSbusnid= "";
            struct_B101D.NSamtdd= "";
            struct_B101D.NSname= "";
            struct_B101D.NStrade_nm= "";
            struct_B101D.NSsum = "";
            struct_B101D.NSamt = "";

            struct_C102Y.NSform_cd = "";
            struct_C102Y.NSdat_cd = "";
            struct_C102Y.NSresid = "";
            struct_C102Y.NSbusnid = "";
            struct_C102Y.NSname = "";
            struct_C102Y.NStrade_nm = "";
            struct_C102Y.NSedu_tp = "";
            struct_C102Y.NSedu_cl = "";
            struct_C102Y.NSsum = "";

            struct_C102M.NSform_cd = "";
            struct_C102M.NSdat_cd = "";
            struct_C102M.NSresid = "";
            struct_C102M.NSbusnid = "";
            struct_C102M.NSedu_tp = "";
            struct_C102M.NSamtmm = "";
            struct_C102M.NSname = "";
            struct_C102M.NStrade_nm = "";
            struct_C102M.NSsum = "";
            struct_C102M.NSamt = "";

            struct_C202Y.NSform_cd = "";
            struct_C202Y.NSdat_cd = "";
            struct_C202Y.NSresid = "";
            struct_C202Y.NSbusnid = "";
            struct_C202Y.NScourse_cd = "";
            struct_C202Y.NSname = "";
            struct_C202Y.NStrade_nm = "";
            struct_C202Y.NSsubject_nm = "";
            struct_C202Y.NSsum = "";

            struct_C202M.NSform_cd = "";
            struct_C202M.NSdat_cd = "";
            struct_C202M.NSresid = "";
            struct_C202M.NSbusnid = "";
            struct_C202M.NScourse_cd = "";
            struct_C202M.NSamtmm = "";
            struct_C202M.NSname = "";
            struct_C202M.NStrade_nm = "";
            struct_C202M.NSsubject_nm = "";
            struct_C202M.NSsum = "";
            struct_C202M.NSamt = "";

            struct_C301Y.NSform_cd = "";
            struct_C301Y.NSdat_cd = "";
            struct_C301Y.NSresid = "";
            struct_C301Y.NSbusnid = "";
            struct_C301Y.NSname = "";
            struct_C301Y.NStrade_nm = "";
            struct_C301Y.NSsum = "";

            struct_C301M.NSform_cd = "";
            struct_C301M.NSdat_cd = "";
            struct_C301M.NSresid = "";
            struct_C301M.NSbusnid = "";
            struct_C301M.NSamtmm = "";
            struct_C301M.NSname = "";
            struct_C301M.NStrade_nm = "";
            struct_C301M.NSsum = "";
            struct_C301M.NSamt = "";

            struct_D101Y.NSform_cd = "";
            struct_D101Y.NSdat_cd = "";
            struct_D101Y.NSresid = "";
            struct_D101Y.NSbusnid = "";
            struct_D101Y.NSacc_no = "";
            struct_D101Y.NSname = "";
            struct_D101Y.NStrade_nm = "";
            struct_D101Y.NSstart_dt = "";
            struct_D101Y.NSend_dt = "";
            struct_D101Y.NScom_cd = "";
            struct_D101Y.NSsum = "";

            struct_D101M.NSform_cd = "";
            struct_D101M.NSdat_cd = "";
            struct_D101M.NSresid = "";
            struct_D101M.NSbusnid = "";
            struct_D101M.NSacc_no = "";
            struct_D101M.NSamtmm = "";
            struct_D101M.NSname = "";
            struct_D101M.NStrade_nm = "";
            struct_D101M.NSstart_dt = "";
            struct_D101M.NSend_dt = "";
            struct_D101M.NScom_cd = "";
            struct_D101M.NSfix_cd = "";
            struct_D101M.NSsum = "";
            struct_D101M.NSamt = "";

            struct_E102Y.NSform_cd = "";
            struct_E102Y.NSdat_cd = "";
            struct_E102Y.NSresid = "";
            struct_E102Y.NSbusnid = "";
            struct_E102Y.NSacc_no = "";
            struct_E102Y.NSname = "";
            struct_E102Y.NStrade_nm = "";
            struct_E102Y.NScom_cd = "";
            struct_E102Y.NSann_tot_amt = "";
            struct_E102Y.NStax_year_amt = "";
            struct_E102Y.NSddct_bs_ass_amt = "";
            struct_E102Y.NSisa_ann_tot_amt = "";
            struct_E102Y.NSisa_tax_year_amt = "";
            struct_E102Y.NSisa_ddct_bs_ass_amt = "";


            struct_F102Y.NSform_cd = "";
            struct_F102Y.NSdat_cd = "";
            struct_F102Y.NSresid = "";
            struct_F102Y.NSbusnid = "";
            struct_F102Y.NSacc_no = "";
            struct_F102Y.NSname = "";
            struct_F102Y.NStrade_nm = "";
            struct_F102Y.NScom_cd = "";
            struct_F102Y.NSpension_cd = "";
            struct_F102Y.NSann_tot_amt = "";
            struct_F102Y.NStax_year_amt = "";
            struct_F102Y.NSddct_bs_ass_amt = "";
            struct_F102Y.NSisa_ann_tot_amt = "";
            struct_F102Y.NSisa_tax_year_amt = "";
            struct_F102Y.NSisa_ddct_bs_ass_amt = "";


            struct_G106Y.NSform_cd = "";
            struct_G106Y.NSdat_cd = "";
            struct_G106Y.NSresid = "";
            struct_G106Y.NSbusnid = "";
            struct_G106Y.NSuse_place_cd = "";

            struct_G106Y.NSname = "";
            struct_G106Y.NStrade_nm = "";         
            struct_G106Y.NSsum = "";


            struct_G106M.NSdat_cd = "";
            struct_G106M.NSresid = "";
            struct_G106M.NSbusnid = "";
            struct_G106M.NSuse_place_cd = "";
            struct_G106M.NSamtmm = "";

            struct_G106M.NSname = "";
            struct_G106M.NStrade_nm = "";          

            struct_G106M.NSsum = "";
            struct_G106M.NSamt = "";

            struct_G206M.NSform_cd = "";
            struct_G206M.NSdat_cd = "";
            struct_G206M.NSresid = "";
            struct_G206M.NSuse_place_cd = "";
            struct_G206M.NSamtmm = "";

            struct_G206M.NSname = "";          

            struct_G206M.NSsum = "";
            struct_G206M.NSamt = "";


            struct_J101Y.NSform_cd = "";
            struct_J101Y.NSdat_cd = "";
            struct_J101Y.NSresid = "";
            struct_J101Y.NSbusnid = "";
            struct_J101Y.NSacc_no = "";
            struct_J101Y.NSname = "";
            struct_J101Y.NStrade_nm = "";

            struct_J101Y.NSgoods_nm = "";
            struct_J101Y.NSlend_dt = "";
            struct_J101Y.NSsum = "";

            struct_J101M.NSform_cd = "";
            struct_J101M.NSdat_cd = "";
            struct_J101M.NSresid = "";
            struct_J101M.NSbusnid = "";
            struct_J101M.NSacc_no = "";
            struct_J101M.NSamtmm = "";
            struct_J101M.NSname = "";
            struct_J101M.NStrade_nm = "";
            struct_J101M.NSgoods_nm = "";
            struct_J101M.NSlend_dt = "";
            struct_J101M.NSfix_cd = "";
            struct_J101M.NSsum = "";
            struct_J101M.NSamt = "";

            struct_J203Y.NSform_cd = "";
            struct_J203Y.NSdat_cd = "";
            struct_J203Y.NSresid = "";
            struct_J203Y.NSbusnid = "";
            struct_J203Y.NSacc_no = "";
            struct_J203Y.NSlend_kd = "";
            struct_J203Y.NSname = "";
            struct_J203Y.NStrade_nm = "";

            struct_J203Y.NShouse_take_dt = "";
            struct_J203Y.NSmort_setup_dt = "";
            struct_J203Y.NSstart_dt = "";
            struct_J203Y.NSend_dt = "";
            struct_J203Y.NSrepay_years = "";
            struct_J203Y.NSlend_goods_nm = "";
            struct_J203Y.NSdebt = "";
            struct_J203Y.NSfixed_rate_debt = "";
            struct_J203Y.NSnot_defer_debt = "";
            struct_J203Y.NSthis_year_rede_amt = "";
            struct_J203Y.NSsumddct = "";

            struct_J203Y.NSsum = "";


            struct_J203M.NSform_cd = "";
            struct_J203M.NSdat_cd = "";
            struct_J203M.NSresid = "";
            struct_J203M.NSbusnid = "";
            struct_J203M.NSacc_no = "";
            struct_J203M.NSlend_kd = "";
            struct_J203M.NSamtmm = "";
            struct_J203M.NSname = "";
            struct_J203M.NStrade_nm = "";

            struct_J203M.NShouse_take_dt = "";
            struct_J203M.NSmort_setup_dt = "";
            struct_J203M.NSstart_dt = "";
            struct_J203M.NSend_dt = "";
            struct_J203M.NSrepay_years = "";
            struct_J203M.NSlend_goods_nm = "";
            struct_J203M.NSdebt = "";
            struct_J203M.NSfixed_rate_debt = "";
            struct_J203M.NSnot_defer_debt = "";
            struct_J203M.NSthis_year_rede_amt = "";
            struct_J203M.NSsumddct = "";

            struct_J203M.NSsum = "";
            struct_J203M.NSamt = "";

            struct_J301Y.NSform_cd = "";
            struct_J301Y.NSdat_cd = "";
            struct_J301Y.NSresid = "";
            struct_J301Y.NSbusnid = "";
            struct_J301Y.NSacc_no = "";

            struct_J301Y.NSsaving_gubn = "";
            struct_J301Y.NSname = "";
            struct_J301Y.NStrade_nm = "";

            struct_J301Y.NSgoods_nm = "";
            struct_J301Y.NSreg_dt = "";
            struct_J301Y.NScom_cd = "";
            struct_J301Y.NSsum = "";

            struct_J301M.NSform_cd = "";
            struct_J301M.NSdat_cd = "";
            struct_J301M.NSresid = "";
            struct_J301M.NSbusnid = "";
            struct_J301M.NSacc_no = "";
            struct_J301M.NSsaving_gubn = "";
            struct_J301M.NSamtmm = "";
            struct_J301M.NSname = "";
            struct_J301M.NStrade_nm = "";
            struct_J301M.NSgoods_nm = "";
            struct_J301M.NSreg_dt = "";
            struct_J301M.NScom_cd = "";
            struct_J301M.NSfix_cd = "";
            struct_J301M.NSsum = "";
            struct_J301M.NSamt = "";


            struct_J401Y.NSform_cd = "";
            struct_J401Y.NSdat_cd = "";
            struct_J401Y.NSresid = "";
            struct_J401Y.NSbusnid = "";
            struct_J401Y.NSacc_no = "";

            struct_J401Y.NSname = "";
            struct_J401Y.NStrade_nm = "";
            struct_J401Y.NSlend_dt = "";
            struct_J401Y.NSlend_loan_amt = "";
            struct_J401Y.NSsum = "";

            struct_J401M.NSform_cd = "";
            struct_J401M.NSdat_cd = "";
            struct_J401M.NSresid = "";
            struct_J401M.NSbusnid = "";
            struct_J401M.NSacc_no = "";
            struct_J401M.NSamtmm = "";

            struct_J401M.NSname = "";
            struct_J401M.NStrade_nm = "";
            struct_J401M.NSlend_dt = "";
            struct_J401M.NSlend_loan_amt = "";
            struct_J401M.NSfix_cd = "";

            struct_J401M.NSsum = "";
            struct_J401M.NSamt = "";

            struct_K101M.NSform_cd = "";
            struct_K101M.NSdat_cd = "";
            struct_K101M.NSresid = "";
            struct_K101M.NSacc_no = "";
            struct_K101M.NSamtmm = "";

            struct_K101M.NSname = "";
            struct_K101M.NSstart_dt = "";
            struct_K101M.NSend_dt = "";
            struct_K101M.NSpay_method = "";

            struct_K101M.NSsum = "";
            struct_K101M.NSsumddct = "";
            struct_K101M.NSdate = "";
            struct_K101M.NSamt = "";


            struct_L102Y.NSform_cd = "";
            struct_L102Y.NSdat_cd = "";
            struct_L102Y.NSresid = "";
            struct_L102Y.NSbusnid = "";
            struct_L102Y.NSdonation_cd = "";

            struct_L102Y.NSname = "";
            struct_L102Y.NStrade_nm = "";
            struct_L102Y.NSsum = "";
            struct_L102Y.NSsbdy_apln_sum = "";
            struct_L102Y.NSconb_sum = "";

            struct_L102D.NSform_cd = "";
            struct_L102D.NSdat_cd = "";
            struct_L102D.NSresid = "";
            struct_L102D.NSbusnid = "";
            struct_L102D.NSdonation_cd = "";
            struct_L102D.NSamtdd = "";

            struct_L102D.NSname = "";
            struct_L102D.NStrade_nm = "";

            struct_L102D.NSsum = "";
            struct_L102D.NSsbdy_apln_sum = "";
            struct_L102D.NSconb_sum = "";

            struct_L102D.NSamt = "";
            struct_L102D.NSamtapln = "";
            struct_L102D.NSamtsum = "";

            struct_N101Y.NSform_cd = "";
            struct_N101Y.NSdat_cd = "";
            struct_N101Y.NSresid = "";
            struct_N101Y.NSbusnid = "";
            struct_N101Y.NSsecu_no = "";

            struct_N101Y.NSname = "";
            struct_N101Y.NStrade_nm = "";
            struct_N101Y.NSfund_nm = "";
            struct_N101Y.NSreg_dt = "";
            struct_N101Y.NSctr_term_mm_cnt = "";
            struct_N101Y.NScom_cd = "";

            struct_N101Y.NSsum = "";
            struct_N101Y.NSddct_bs_ass_amt = "";

            struct_N101M.NSform_cd = "";
            struct_N101M.NSdat_cd = "";
            struct_N101M.NSresid = "";
            struct_N101M.NSbusnid = "";
            struct_N101M.NSsecu_no = "";
            struct_N101M.NSamtmm = "";

            struct_N101M.NSname = "";
            struct_N101M.NStrade_nm = "";
            struct_N101M.NSfund_nm = "";
            struct_N101M.NSreg_dt = "";
            struct_N101M.NScom_cd = "";
            struct_N101M.NSfix_cd = "";

            struct_N101M.NSsum = "";
            struct_N101M.NSddct_bs_ass_amt = "";
            struct_N101M.NSamt = "";


            struct_O101M.NSform_cd = "";
            struct_O101M.NSdat_cd = "";
            struct_O101M.NSresid = "";
            struct_O101M.NSamtmm = "";

            struct_O101M.NSname = "";

            struct_O101M.NSMhi_yrs = "";
            struct_O101M.NSMltrm_yrs = "";
            struct_O101M.NSMhi_ntf = "";
            struct_O101M.NSMltrm_ntf = "";
            struct_O101M.NSMhi_pmt = "";
            struct_O101M.NSMltrm_pmt = "";

            struct_O101M.NSsum = "";
            struct_O101M.NShi_ntf = "";
            struct_O101M.NSltrm_ntf = "";
            struct_O101M.NShi_pmt = "";
            struct_O101M.NSltrm_pmt = "";


            struct_P102M.NSform_cd = "";
            struct_P102M.NSdat_cd = "";
            struct_P102M.NSresid = "";
            struct_P102M.NSamtmm = "";

            struct_P102M.NSname = "";

            struct_P102M.NSsum = "";
            struct_P102M.NSsumsp_ntf = "";
            struct_P102M.NSsumspym = "";
            struct_P102M.NSsumjlc = "";
            struct_P102M.NSsumntf = "";
            struct_P102M.NSsumpmt = "";
            struct_P102M.NSwrkp_ntf = "";
            struct_P102M.NSrgn_pmt = "";

            struct_G108Y.NSform_cd = "";
            struct_G108Y.NSdat_cd = "";
            struct_G108Y.NSresid = "";
            struct_G108Y.NSbusnid = "";
            struct_G108Y.NSuse_place_cd = "";
            struct_G108Y.NSname = "";
            struct_G108Y.NStrade_nm = "";
            struct_G108Y.NSgnrl_sum = "";
            struct_G108Y.NStdmr_sum = "";
            struct_G108Y.NStrp_sum = "";
            struct_G108Y.NSisld_sum = "";
            struct_G108Y.NStot_sum = "";
            struct_G108Y.NSgnrl_mar_sum = "";
            struct_G108Y.NStdmr_mar_sum = "";
            struct_G108Y.NStrp_mar_sum = "";
            struct_G108Y.NSisld_mar_sum = "";
            struct_G108Y.NStot_mar_sum = "";
            struct_G108Y.NSgnrl_aprl_sum = "";
            struct_G108Y.NStdmr_aprl_sum = "";
            struct_G108Y.NStrp_aprl_sum = "";
            struct_G108Y.NSisld_aprl_sum = "";
            struct_G108Y.NStot_aprl_sum = "";
            struct_G108Y.NSgnrl_jan_sum = "";
            struct_G108Y.NStdmr_jan_sum = "";
            struct_G108Y.NStrp_jan_sum = "";
            struct_G108Y.NSisld_jan_sum = "";
            struct_G108Y.NStot_jan_sum = "";
            struct_G108Y.NSsum = "";

            struct_G108M.NSform_cd = "";
            struct_G108M.NSdat_cd = "";
            struct_G108M.NSresid = "";
            struct_G108M.NSbusnid = "";
            struct_G108M.NSuse_place_cd = "";
            struct_G108M.NSamtmm = "";
            struct_G108M.NSname = "";
            struct_G108M.NStrade_nm = "";
            struct_G108M.NSgnrl_sum = "";
            struct_G108M.NStdmr_sum = "";
            struct_G108M.NStrp_sum = "";
            struct_G108M.NSisld_sum = "";
            struct_G108M.NStot_sum = "";
            struct_G108M.NSgnrl_mar_sum = "";
            struct_G108M.NStdmr_mar_sum = "";
            struct_G108M.NStrp_mar_sum = "";
            struct_G108M.NSisld_mar_sum = "";
            struct_G108M.NStot_mar_sum = "";
            struct_G108M.NSgnrl_aprl_sum = "";
            struct_G108M.NStdmr_aprl_sum = "";
            struct_G108M.NStrp_aprl_sum = "";
            struct_G108M.NSisld_aprl_sum = "";
            struct_G108M.NStot_aprl_sum = "";
            struct_G108M.NSgnrl_jan_sum = "";
            struct_G108M.NStdmr_jan_sum = "";
            struct_G108M.NStrp_jan_sum = "";
            struct_G108M.NSisld_jan_sum = "";
            struct_G108M.NStot_jan_sum = "";
            struct_G108M.NSsum = "";
            struct_G108M.NSamt = "";

            struct_G110Y.NSform_cd = "";
            struct_G110Y.NSdat_cd = "";
            struct_G110Y.NSresid = "";
            struct_G110Y.NSbusnid = "";
            struct_G110Y.NSuse_place_cd = "";
            struct_G110Y.NSname = "";
            struct_G110Y.NStrade_nm = "";
            struct_G110Y.NStot_pre_year_sum = "";
            struct_G110Y.NStot_curr_year_sum = "";
            struct_G110Y.NStdmr_tot_pre_year_sum = "";
            struct_G110Y.NStdmr_tot_curr_year_sum = "";

            
            struct_G110Y.NStdmr_sum = "";
            struct_G110Y.NStrp_sum = "";
            struct_G110Y.NSisld_sum = "";
            struct_G110Y.NStot_sum = "";           
            struct_G110Y.NSsum = "";

            struct_G110Y.NStfhy_gnrl_sum = "";
            struct_G110Y.NStfhy_tdmr_sum = "";
            struct_G110Y.NStfhy_trp_sum = "";
            struct_G110Y.NStfhy_isld_sum = "";
            struct_G110Y.NStfhy_tot_sum = "";
            struct_G110Y.NSshfy_gnrl_sum = "";
            struct_G110Y.NSshfy_tdmr_sum = "";
            struct_G110Y.NSshfy_trp_sum = "";
            struct_G110Y.NSshfy_isld_sum = "";
            struct_G110Y.NSshfy_tot_sum = "";

            struct_G110M.NSform_cd = "";
            struct_G110M.NSdat_cd = "";
            struct_G110M.NSresid = "";
            struct_G110M.NSbusnid = "";
            struct_G110M.NSuse_place_cd = "";
            struct_G110M.NSamtmm = "";
            struct_G110M.NSname = "";
            struct_G110M.NStot_pre_year_sum = "";
            struct_G110M.NStot_curr_year_sum = "";
            struct_G110M.NStdmr_tot_pre_year_sum = "";
            struct_G110M.NStdmr_tot_curr_year_sum = "";

            struct_G110M.NStrade_nm = "";
            struct_G110M.NSgnrl_sum = "";
            struct_G110M.NStdmr_sum = "";
            struct_G110M.NStrp_sum = "";
            struct_G110M.NSisld_sum = "";
            struct_G110M.NStot_sum = "";

            struct_G110M.NStfhy_gnrl_sum = "";
            struct_G110M.NStfhy_tdmr_sum = "";
            struct_G110M.NStfhy_trp_sum = "";
            struct_G110M.NStfhy_isld_sum = "";
            struct_G110M.NStfhy_tot_sum = "";
            struct_G110M.NSshfy_gnrl_sum = "";
            struct_G110M.NSshfy_tdmr_sum = "";
            struct_G110M.NSshfy_trp_sum = "";
            struct_G110M.NSshfy_isld_sum = "";
            struct_G110M.NSshfy_tot_sum = "";

            struct_G110M.NSsum = "";
            struct_G110M.NSamt = "";

            struct_G208M.NSform_cd = "";
            struct_G208M.NSdat_cd = "";
            struct_G208M.NSresid = "";
            struct_G208M.NSbusnid = "";
            struct_G208M.NSuse_place_cd = "";
            struct_G208M.NSamtmm = "";
            struct_G208M.NSname = "";
            struct_G208M.NSgnrl_sum = "";
            struct_G208M.NStdmr_sum = "";
            struct_G208M.NStrp_sum = "";
            struct_G208M.NSisld_sum = "";
            struct_G208M.NStot_sum = "";
            struct_G208M.NSgnrl_mar_sum = "";
            struct_G208M.NStdmr_mar_sum = "";
            struct_G208M.NStrp_mar_sum = "";
            struct_G208M.NSisld_mar_sum = "";
            struct_G208M.NStot_mar_sum = "";
            struct_G208M.NSgnrl_aprl_sum = "";
            struct_G208M.NStdmr_aprl_sum = "";
            struct_G208M.NStrp_aprl_sum = "";
            struct_G208M.NSisld_aprl_sum = "";
            struct_G208M.NStot_aprl_sum = "";
            struct_G208M.NSgnrl_jan_sum = "";
            struct_G208M.NStdmr_jan_sum = "";
            struct_G208M.NStrp_jan_sum = "";
            struct_G208M.NSisld_jan_sum = "";
            struct_G208M.NStot_jan_sum = "";
            struct_G208M.NSsum = "";
            struct_G208M.NSamt = "";

            struct_G210M.NSform_cd = "";
            struct_G210M.NSdat_cd = "";
            struct_G210M.NSresid = "";
            struct_G210M.NSbusnid = "";
            struct_G210M.NSuse_place_cd = "";
            struct_G210M.NSamtmm = "";
            struct_G210M.NSname = "";
            struct_G210M.NStot_pre_year_sum = "";
            struct_G210M.NStot_curr_year_sum = "";            
            struct_G210M.NSgnrl_sum = "";
            struct_G210M.NStdmr_sum = "";
            struct_G210M.NStrp_sum = "";
            struct_G210M.NSisld_sum = "";
            struct_G210M.NStot_sum = "";
            struct_G210M.NSsum = "";
            struct_G210M.NSamt = "";


            struct_J501Y.NSform_cd = "";
            struct_J501Y.NSdat_cd = "";
            struct_J501Y.NSresid = "";
            struct_J501Y.NSlsor_no = "";
            struct_J501Y.NSname = "";
            struct_J501Y.NSlsor_nm = "";
            struct_J501Y.NSstart_dt = "";
            struct_J501Y.NSend_dt = "";
            struct_J501Y.NSadr = "";
            struct_J501Y.NSarea = "";
            struct_J501Y.NStypeCd = "";
            struct_J501Y.NSsum = "";


            struct_B201Y.NSform_cd = "";
            struct_B201Y.NSdat_cd = "";
            struct_B201Y.NSresid = "";
            struct_B201Y.NSbusnid = "";
            struct_B201Y.NSAcc_No = "";
            struct_B201Y.NSname = "";
            struct_B201Y.NStrade_nm = "";
            struct_B201Y.NSGoods_nm = "";
            struct_B201Y.NSInsu_resid = "";
            struct_B201Y.NSInsu_nm = "";
            struct_B201Y.NSsum = "";
            
            struct_A101Y.man_form_cd = "";
            struct_A101Y.man_resnoEncCntn = "";
            struct_A101Y.man_fnm = "";
            struct_A101Y.man_tnm = "";
            struct_A101Y.man_bsnoEncCntn = "";
            struct_A101Y.man_hshrClCd = "";
            struct_A101Y.man_rsplNtnInfr = "";
            struct_A101Y.man_dtyStrtDt = "";
            struct_A101Y.man_dtyEndDt = "";
            struct_A101Y.man_reStrtDt = "";
            struct_A101Y.man_reEndDt = "";
            struct_A101Y.man_rsdtClCd = "";
            struct_A101Y.man_inctxWhtxTxamtMetnCd = "";
            struct_A101Y.man_inpmYn = "";
            struct_A101Y.man_prifChngYn = "";

            //주,현 근무지 연금보험료
            struct_A101Y.data_npHthrWaInfeeAmt = "";
            struct_A101Y.data_npHthrWaInfeeDdcAmt = "";
            struct_A101Y.data_npHthrMcurWkarInfeeAmt = "";
            struct_A101Y.data_npHthrMcurWkarDdcAmt = "";
            struct_A101Y.data_hthrPblcPnsnInfeeAmt = "";
            struct_A101Y.data_hthrPblcPnsnInfeeDdcAmt = "";
            struct_A101Y.data_mcurPblcPnsnInfeeAmt = "";
            struct_A101Y.data_mcurPblcPnsnInfeeDdcAmt = "";
            struct_A101Y.data_pnsnInfeeUseAmtSum = "";
            struct_A101Y.data_pnsnInfeeDdcAmtSum = "";

            //특별소득공제
            struct_A101Y.data_hthrHifeAmt = "";
            struct_A101Y.data_hthrHifeDdcAmt = "";
            struct_A101Y.data_mcurHifeAmt = "";
            struct_A101Y.data_mcurHifeDdcAmt = "";
            struct_A101Y.data_hthrUiAmt = "";
            struct_A101Y.data_hthrUiDdcAmt = "";
            struct_A101Y.data_mcurUiAmt = "";
            struct_A101Y.data_mcurUiDdcAmt = "";
            struct_A101Y.data_infeeUseAmtSum = "";
            struct_A101Y.data_infeeDdcAmtSum = "";
            struct_A101Y.data_brwOrgnBrwnHsngTennLnpbSrmAmt = "";
            struct_A101Y.data_brwOrgnBrwnHsngTennLnpbSrmDdcAmt = "";
            struct_A101Y.data_rsdtBrwnHsngTennLnpbSrmAmt = "";
            struct_A101Y.data_rsdtBrwnHsngTennLnpbSrmDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr15BlwItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr15BlwDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr29ItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr29DdcAmt = "";
            struct_A101Y.data_lthClrlLnpbY30OverItrAmt = "";
            struct_A101Y.data_lthClrlLnpbY30OverDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2012AfthY15OverItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2012AfthY15OverDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2012EtcBrwItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2012EtcBrwDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthFxnIrItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthFxnIrDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthY15OverItrAmtItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthY15OverDdcAmtDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthEtcBrwItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthEtcBrwDdcAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthYr15BlwItrAmt = "";
            struct_A101Y.data_lthClrlLnpbYr2015AfthYr15BlwDdcAmt = "";
            struct_A101Y.data_hsngFndsDdcAmtSum = "";
            struct_A101Y.data_conbCrfwAmtLglUseAmt = "";
            struct_A101Y.data_conbCrfwAmtLglDdcAmt = "";
            struct_A101Y.data_conbCrfwAmtReliOrgOthUseAmt = "";
            struct_A101Y.data_conbCrfwAmtReliOrgOthDdcAmt = "";
            struct_A101Y.data_conbCrfwAmtReliOrgUseAmt = "";
            struct_A101Y.data_conbCrfwAmtReliOrgDdcAmt = "";
            struct_A101Y.data_conbCrfwAmtUseAmtSum = "";
            struct_A101Y.data_conbCrfwAmtDdcAmtSum = "";

            struct_A101Y.data_yr2000BefNtplPnsnSvngUseAmt = "";
            struct_A101Y.data_yr2000BefNtplPnsnSvngDdcAmt = "";
            struct_A101Y.data_smceSbizUseAmt = "";
            struct_A101Y.data_smceSbizDdcAmt = "";
            struct_A101Y.data_sbcSvngUseAmt = "";
            struct_A101Y.data_sbcSvngDdcAmt = "";
            struct_A101Y.data_lbrrHsngPrptSvngUseAmt = "";
            struct_A101Y.data_lbrrHsngPrptSvngDdcAmt = "";
            struct_A101Y.data_hsngSbcSynSvngUseAmt = "";
            struct_A101Y.data_hsngSbcSynSvngDdcAmt = "";
            struct_A101Y.data_hsngPrptSvngIncUseAmtSum = "";
            struct_A101Y.data_hsngPrptSvngIncDdcAmtSum = "";
            struct_A101Y.data_cpivAsctUseAmt2 = "";
            struct_A101Y.data_cpivAsctDdcAmt2 = "";
            struct_A101Y.data_cpivVntUseAmt2 = "";
            struct_A101Y.data_cpivVntDdcAmt2 = "";
            struct_A101Y.data_cpivAsctUseAmt1 = "";
            struct_A101Y.data_cpivAsctDdcAmt1 = "";
            struct_A101Y.data_cpivVntUseAmt1 = "";
            struct_A101Y.data_cpivVntDdcAmt1 = "";
            struct_A101Y.data_cpivAsctUseAmt0 = "";
            struct_A101Y.data_cpivAsctDdcAmt0 = "";
            struct_A101Y.data_cpivVntUseAmt0 = "";
            struct_A101Y.data_cpivVntDdcAmt0 = "";
            struct_A101Y.data_ivcpInvmUseAmtSum = "";
            struct_A101Y.data_ivcpInvmDdcAmtSum = "";
            struct_A101Y.data_crdcUseAmt = "";
            struct_A101Y.data_drtpCardUseAmt = "";
            struct_A101Y.data_cshptUseAmt = "";
            struct_A101Y.data_tdmrUseAmt = "";
            struct_A101Y.data_pbtUseAmt = "";
            struct_A101Y.data_crdcSumUseAmt = "";
            struct_A101Y.data_crdcSumDdcAmt = "";

            struct_A101Y.data_prsCrdcUseAmt1 = "";
            struct_A101Y.data_tyYrPrsCrdcUseAmt = "";
            struct_A101Y.data_pyrPrsAddDdcrtUseAmt = "";
            struct_A101Y.data_tyShfyPrsAddDdcrtUseAmt = "";

            struct_A101Y.data_emstAsctCntrUseAmt = "";
            struct_A101Y.data_emstAsctCntrDdcAmt = "";
            struct_A101Y.data_empMntnSnmcLbrrUseAmt = "";
            struct_A101Y.data_empMntnSnmcLbrrDdcAmt = "";
            //struct_A101Y.data_lfhItrUseAmt = "";	
            //struct_A101Y.data_lfhItrDdcAmt = "";	
            struct_A101Y.data_ltrmCniSsUseAmt = "";
            struct_A101Y.data_ltrmCniSsDdcAmt = "";

            //세액감면 
            struct_A101Y.data_frgrLbrrEntcPupCd = "";
            struct_A101Y.data_frgrLbrrLbrOfrDt = "";
            struct_A101Y.data_frgrLbrrReExryDt = "";
            struct_A101Y.data_frgrLbrrReRcpnDt = "";
            struct_A101Y.data_frgrLbrrReAlfaSbmsDt = "";
            struct_A101Y.data_frgrLbrrErinImnRcpnDt = "";
            struct_A101Y.data_frgrLbrrErinImnSbmsDt = "";
            struct_A101Y.data_yupSnmcReStrtDt = "";
            struct_A101Y.data_yupSnmcReEndDt = "";
            struct_A101Y.data_sctcHpUseAmt = "";
            struct_A101Y.data_sctcHpDdcTrgtAmt = "";
            struct_A101Y.data_sctcHpDdcAmt = "";
            struct_A101Y.data_rtpnUseAmt = "";
            struct_A101Y.data_rtpnDdcTrgtAmt = "";
            struct_A101Y.data_rtpnDdcAmt = "";
            struct_A101Y.data_pnsnSvngUseAmt = "";
            struct_A101Y.data_pnsnSvngDdcTrgtAmt = "";
            struct_A101Y.data_pnsnSvngDdcAmt = "";
            struct_A101Y.data_pnsnAccUseAmtSum = "";
            struct_A101Y.data_pnsnAccDdcTrgtAmtSum = "";
            struct_A101Y.data_pnsnAccDdcAmtSum = "";
            struct_A101Y.data_cvrgInscUseAmt = "";
            struct_A101Y.data_cvrgInscDdcTrgtAmt2 = "";
            struct_A101Y.data_cvrgInscDdcAmt = "";
            struct_A101Y.data_dsbrEuCvrgUseAmt = "";
            struct_A101Y.data_dsbrEuCvrgDdcTrgtAmt = "";
            struct_A101Y.data_dsbrEuCvrgDdcAmt = "";
            struct_A101Y.data_infeePymUseAmtSum = "";
            struct_A101Y.data_infeePymDdcTrgtAmtSum = "";
            struct_A101Y.data_infeePymDdcAmtSum = "";
            struct_A101Y.data_mdxpsPrsUseAmt = "";
            struct_A101Y.data_mdxpsPrsDdcTrgtAmt = "";
            struct_A101Y.data_mdxpsPrsDdcAmt = "";
            struct_A101Y.data_mdxpsOthUseAmt = "";
            struct_A101Y.data_mdxpsOthDdcTrgtAmt = "";
            struct_A101Y.data_mdxpsOthDdcAmt = "";
            struct_A101Y.data_mdxpsUseAmtSum = "";
            struct_A101Y.data_mdxpsDdcTrgtAmtSum = "";
            struct_A101Y.data_mdxpsDdcAmtSum = "";
            struct_A101Y.data_scxpsPrsUseAmt = "";
            struct_A101Y.data_scxpsPrsDdcTrgtAmt = "";
            struct_A101Y.data_scxpsPrsDdcAmt = "";
            struct_A101Y.data_scxpsKidUseAmt = "";
            struct_A101Y.data_scxpsKidDdcTrgtAmt = "";
            struct_A101Y.data_scxpsKidDdcAmt = "";
            struct_A101Y.data_scxpsStdUseAmt = "";
            struct_A101Y.data_scxpsStdDdcTrgtAmt = "";
            struct_A101Y.data_scxpsStdDdcAmt = "";
            struct_A101Y.data_scxpsUndUseAmt = "";
            struct_A101Y.data_scxpsUndDdcTrgtAmt = "";
            struct_A101Y.data_scxpsUndDdcAmt = "";
            struct_A101Y.data_scxpsDsbrUseAmt = "";
            struct_A101Y.data_scxpsDsbrDdcTrgtAmt = "";
            struct_A101Y.data_scxpsDsbrDdcAmt = "";
            struct_A101Y.data_scxpsKidCount = "";
            struct_A101Y.data_scxpsStdCount = "";
            struct_A101Y.data_scxpsUndCount = "";
            struct_A101Y.data_scxpsDsbrCount = "";
            struct_A101Y.data_scxpsUseAmtSum = "";
            struct_A101Y.data_scxpsDdcTrgtAmtSum = "";
            struct_A101Y.data_scxpsDdcAmtSum = "";
            struct_A101Y.data_conb10ttswLtUseAmt = "";
            struct_A101Y.data_conb10ttswLtDdcTrgtAmt = "";
            struct_A101Y.data_conb10ttswLtDdcAmt = "";
            struct_A101Y.data_conb10excsLtUseAmt = "";
            struct_A101Y.data_conb10excsLtDdcTrgtAmt = "";
            struct_A101Y.data_conb10excsLtDdcAmt = "";
            struct_A101Y.data_conbLglUseAmt = "";
            struct_A101Y.data_conbLglDdcTrgtAmt = "";
            struct_A101Y.data_conbLglDdcAmt = "";
            struct_A101Y.data_conbEmstAsctUseAmt = "";
            struct_A101Y.data_conbEmstAsctDdcTrgtAmt = "";
            struct_A101Y.data_conbEmstAsctDdcAmt = "";
            struct_A101Y.data_conbReliOrgOthAppnUseAmt = "";
            struct_A101Y.data_conbReliOrgOthAppnDdcTrgtAmt = "";
            struct_A101Y.data_conbReliOrgOthAppnDdcAmt = "";
            struct_A101Y.data_conbReliOrgAppnUseAmt = "";
            struct_A101Y.data_conbReliOrgAppnDdcTrgtAmt = "";
            struct_A101Y.data_conbReliOrgAppnDdcAmt = "";
            struct_A101Y.data_conbUseAmtSum = "";
            struct_A101Y.data_conbDdcTrgtAmtSum = "";
            struct_A101Y.data_conbDdcAmtSum = "";
            struct_A101Y.data_ovrsSurcIncFmt = "";
            struct_A101Y.data_frgnPmtFcTxamt = "";
            struct_A101Y.data_frgnPmtWcTxamt = "";
            struct_A101Y.data_frgnPmtTxamtTxpNtnNm = "";
            struct_A101Y.data_frgnPmtTxamtPmtDt = "";
            struct_A101Y.data_frgnPmtTxamtAlfaSbmsDt = "";
            struct_A101Y.data_frgnPmtTxamtAbrdWkarNm = "";
            struct_A101Y.data_frgnDtyTerm = "";
            struct_A101Y.data_frgnPmtTxamtRfoNm = "";
            struct_A101Y.data_hsngTennLnpbUseAmt = "";
            struct_A101Y.data_hsngTennLnpbDdcAmt = "";
            struct_A101Y.data_mmrUseAmt = "";
            struct_A101Y.data_mmrDdcAmt = "";
            struct_A101Y.data_cd218 = "";
            struct_A101Y.data_cd219 = "";
            struct_A101Y.data_cd220 = "";
            struct_A101Y.data_cd221 = "";
            struct_A101Y.data_cd222 = "";
            struct_A101Y.data_cd223 = "";
            struct_A101Y.data_cd224 = "";
            struct_A101Y.data_cd225 = "";
            struct_A101Y.data_cd226 = "";
            struct_A101Y.data_cd227 = "";
            struct_A101Y.data_cd228 = "";

            //인적공제
            struct_A101Y.data_seq = "";
            struct_A101Y.data_suptFmlyRltClCd = "";
            struct_A101Y.data_txprDscmNoCntn = "";
            struct_A101Y.data_nnfClCd = "";
            struct_A101Y.data_child = "";
            struct_A101Y.data_txprNm = "";
            struct_A101Y.data_bscDdcClCd = "";
            struct_A101Y.data_wmnDdcClCd = "";
            struct_A101Y.data_snprntFmlyDdcClCd = "";
            struct_A101Y.data_sccDdcClCd = "";
            struct_A101Y.data_dsbrDdcClCd = "";
            struct_A101Y.data_chbtAtprDdcClCd = "";
            struct_A101Y.data_age6Lt = "";
            struct_A101Y.data_cdVvalKrnNm = "";
            struct_A101Y.data_hifeDdcTrgtAmt = "";
            struct_A101Y.data_cvrgInscDdcTrgtAmt = "";
            struct_A101Y.data_dsbrDdcTrgtAmt = "";
            struct_A101Y.data_mdxpsDdcTrgtAmt = "";
            struct_A101Y.data_scxpsDdcTrgtAmt = "";
            struct_A101Y.data_crdcDdcTrgtAmt = "";
            struct_A101Y.data_drtpCardDdcTrgtAmt = "";
            struct_A101Y.data_cshptDdcTrgtAmt = "";
            struct_A101Y.data_tdmrDdcTrgtAmt = "";
            struct_A101Y.data_pbtDdcTrgtAmt = "";
            struct_A101Y.data_conbDdcTrgtAmt = "";

            struct_Doc_B101Y.form_cd = "";
            struct_Doc_B101Y.bsnoEncCntn = "";
            struct_Doc_B101Y.resnoEncCntn = "";
            struct_Doc_B101Y.tnm = "";
            struct_Doc_B101Y.fnm = "";
            struct_Doc_B101Y.adr = "";
            struct_Doc_B101Y.pfbAdr = "";

            struct_Doc_B101R.form_cd = "";
            struct_Doc_B101R.bsnoEncCntn = "";
            struct_Doc_B101R.resnoEncCntn = "";

            struct_Doc_B101R.rtpnAccRtpnCl = "";
            struct_Doc_B101R.rtpnFnnOrgnCd = "";
            struct_Doc_B101R.rtpnAccFnnCmp = "";
            struct_Doc_B101R.rtpnAccAccno = "";
            struct_Doc_B101R.rtpnAccPymAmt = "";
            struct_Doc_B101R.rtpnAccTxamtDdcAmt = "";

            struct_Doc_B101P.form_cd = "";
            struct_Doc_B101P.bsnoEncCntn = "";
            struct_Doc_B101P.resnoEncCntn = "";

            struct_Doc_B101P.pnsnSvngAccPnsnSvngCl = "";
            struct_Doc_B101P.pnsnSvngFnnOrgnCd = "";
            struct_Doc_B101P.pnsnSvngAccFnnCmp = "";
            struct_Doc_B101P.pnsnSvngAccAccno = "";
            struct_Doc_B101P.pnsnSvngAccPymAmt = "";
            struct_Doc_B101P.ipnsnSvngAccNcTxamtDdcAmt = "";

            struct_Doc_B101H.form_cd = "";
            struct_Doc_B101H.bsnoEncCntn = "";
            struct_Doc_B101H.resnoEncCntn = "";

            struct_Doc_B101H.hsngPrptSvngSvngCl = "";
            struct_Doc_B101H.jnngDt = "";
            struct_Doc_B101H.hsngPrptSvngFnnOrgnCd = "";
            struct_Doc_B101H.hsngPrptSvngFnnCmp = "";
            struct_Doc_B101H.hsngPrptSvngAccno = "";
            struct_Doc_B101H.hsngPrptSvngPymAmt = "";
            struct_Doc_B101H.hsngPrptSvngIncDdcAmt = "";

            struct_Doc_B101L.form_cd = "";
            struct_Doc_B101L.bsnoEncCntn = "";
            struct_Doc_B101L.resnoEncCntn = "";

            struct_Doc_B101L.ltrmCniSsfnnOrgnCd = "";
            struct_Doc_B101L.ltrmCniSsFnnCmp = "";
            struct_Doc_B101L.ltrmCniSsAccno = "";
            struct_Doc_B101L.ltrmCniSsPymAmt = "";
            struct_Doc_B101L.ltrmCniSsIncDdcAmt = "";

        }

        #endregion

        #region  Description :  Data List 변수 Clear
        private void UP_DataList_Clear()
        {
            data_A102Y.Clear();
            data_A102M.Clear();
            data_B101Y.Clear();
            data_B101D.Clear();
            data_C102Y.Clear();
            data_C102M.Clear();
            data_C202Y.Clear();
            data_C202M.Clear();

            data_C301Y.Clear(); //교육비(교복구입비)
            data_C301M.Clear();  //교육비(교복구입비)_상세내역
            data_D101Y.Clear();  //개인연금저축
            data_D101M.Clear();  //개인연금저축_상세내역
            data_E102Y.Clear();  //연금저축
            data_F102Y.Clear(); //퇴직연금 + 상세내역 

            data_G106Y.Clear();  //신용카드
            data_G106M.Clear(); //신용카드_상세내역 
            data_G206M.Clear(); //현금영수증_상세내역 
            data_J101Y.Clear();
            data_J101M.Clear();
            data_J203Y.Clear();
            data_J203M.Clear();
            data_J301Y.Clear();
            data_J301M.Clear();
            data_J401Y.Clear();
            data_J401M.Clear();
            data_K101M.Clear();

            data_L102Y.Clear();
            data_L102D.Clear();
            data_N101Y.Clear();
            data_N101M.Clear();

            data_O101M.Clear();
            data_P102M.Clear();

            data_A101Y.Clear();
            data_A101M.Clear();

            data_Doc_B101Y.Clear();
            data_Doc_B101R.Clear();
            data_Doc_B101P.Clear();
            data_Doc_B101H.Clear();
            data_Doc_B101L.Clear();

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
                        UP_Struct_Clear();

                        sXml = UP_Get_ConvertToXml(ftAttachTable.Rows[i]["YAFILENAME"].ToString(), "");

                        //Xml 파싱
                        UP_Set_XmlToParsing(sXml);
 
                        this.UP_NTS_DataProcess(e);
                    }

                    if (e.DbConnector.CommandCount > 0)
                        e.DbConnector.ExecuteTranQueryList();

                   

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

        #region  Description : 부양가족에 동일 주민번호가 존재하는 체크
        private bool UP_Get_FamilyCheck(string sJuminNum )
        {
            bool bResult = false;

            string dd = string.Empty;

            if (fsdata_FamilyList.Length > 0)
            {
                string[] arrayJumin = fsdata_FamilyList.Split(',');

                for (int i = 0; i < arrayJumin.Length; i++)
                {
                    if (arrayJumin[i].ToString() == sJuminNum)
                    {
                        bResult = true;
                        return bResult;
                    }
                }
            }

            bResult = true;

            return bResult;
        }
        #endregion
        

        #region  Description :  구조체 변수 선언

        public struct NTS_Main
        {
            public string form_cd;
        }

        public struct NTS_A102Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSname;
            public string NStrade_nm;
            public string NSgoods_nm;

            public string NSinsu1_resid;
            public string NSinsu1_nm;

            public string NSinsu2_resid_1;
            public string NSinsu2_nm_1;

            public string NSinsu2_resid_2;
            public string NSinsu2_nm_2;

            public string NSinsu2_resid_3;
            public string NSinsu2_nm_3;
            public string NSsum;
        }

        public struct NTS_A102M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSamtmm;
            public string NSname;
            public string NStrade_nm;
            public string NSgoods_nm;

            public string NSinsu1_resid;
            public string NSinsu1_nm;

            public string NSinsu2_resid_1;
            public string NSinsu2_nm_1;

            public string NSinsu2_resid_2;
            public string NSinsu2_nm_2;

            public string NSinsu2_resid_3;
            public string NSinsu2_nm_3;
            public string NSsum;
            public string NSfix_cd;
            public string NSamt;

        }

        public struct NTS_B101Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSname;
            public string NStrade_nm;

            public string NSMdCode;
            public string NScnt;
            public string NSmdxPrsGn;

            public string NSsum;
        }

        public struct NTS_B101D
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSamtdd;
            public string NSname;
            public string NStrade_nm;
            public string NSsum;
            public string NSamt;
        }

        public struct NTS_C102Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSedu_tp;
            public string NSedu_cl;
            public string NSname;
            public string NStrade_nm;
            public string NSsum;
        }

        public struct NTS_C102M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSedu_tp;
            public string NSamtmm;

            public string NSname;
            public string NStrade_nm;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_C202Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NScourse_cd;
            public string NSname;
            public string NStrade_nm;
            public string NSsubject_nm;
            public string NSsum;
        }

        public struct NTS_C202M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NScourse_cd;
            public string NSamtmm;
            public string NSname;
            public string NStrade_nm;
            public string NSsubject_nm;
            public string NSsum;
            public string NSamt;
        }

        public struct NTS_C301Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSname;
            public string NStrade_nm;
            public string NSsum;
        }

        public struct NTS_C301M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSamtmm;
            public string NSname;
            public string NStrade_nm;
            public string NSsum;
            public string NSamt;
        }

        public struct NTS_C401Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSname;
            public string NStrade_nm;
            public string NSsum;
        }

        public struct NTS_C401M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSamtmm;

            public string NSname;
            public string NStrade_nm;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_D101Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSname;
            public string NStrade_nm;
            public string NSstart_dt;
            public string NSend_dt;
            public string NScom_cd;
            public string NSsum;
        }

        public struct NTS_D101M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSamtmm;
            public string NSname;
            public string NStrade_nm;
            public string NSstart_dt;
            public string NSend_dt;
            public string NScom_cd;
            public string NSfix_cd;
            public string NSsum;
            public string NSamt;
        }

        public struct NTS_E102Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSname;
            public string NStrade_nm;
            public string NScom_cd;
            public string NSann_tot_amt;
            public string NStax_year_amt;
            public string NSddct_bs_ass_amt;
            public string NSisa_ann_tot_amt;
            public string NSisa_tax_year_amt;
            public string NSisa_ddct_bs_ass_amt;
        }

        public struct NTS_F102Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSname;
            public string NStrade_nm;
            public string NScom_cd;
            public string NSpension_cd;
            public string NSann_tot_amt;
            public string NStax_year_amt;
            public string NSddct_bs_ass_amt;
            public string NSisa_ann_tot_amt;
            public string NSisa_tax_year_amt;
            public string NSisa_ddct_bs_ass_amt;
        }

        public struct NTS_G106Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;

            public string NSname;
            public string NStrade_nm;
         
            public string NSsum;
        }

        public struct NTS_G106M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;
            public string NSamtmm;

            public string NSname;
            public string NStrade_nm;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_G206M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSuse_place_cd;
            public string NSamtmm;

            public string NSname;        

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_J101Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSname;
            public string NStrade_nm;

            public string NSgoods_nm;
            public string NSlend_dt;
            public string NSsum;
        }
        public struct NTS_J101M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSamtmm;
            public string NSname;
            public string NStrade_nm;
            public string NSgoods_nm;
            public string NSlend_dt;
            public string NSfix_cd;
            public string NSsum;
            public string NSamt;
        }

        public struct NTS_J203Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSlend_kd;
            public string NSname;
            public string NStrade_nm;

            public string NShouse_take_dt;
            public string NSmort_setup_dt;
            public string NSstart_dt;
            public string NSend_dt;
            public string NSrepay_years;
            public string NSlend_goods_nm;
            public string NSdebt;
            public string NSfixed_rate_debt;
            public string NSnot_defer_debt;
            public string NSthis_year_rede_amt;
            public string NSsumddct;

            public string NSsum;
        }

        public struct NTS_J203M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSlend_kd;
            public string NSamtmm;
            public string NSname;
            public string NStrade_nm;

            public string NShouse_take_dt;
            public string NSmort_setup_dt;
            public string NSstart_dt;
            public string NSend_dt;
            public string NSrepay_years;
            public string NSlend_goods_nm;
            public string NSdebt;
            public string NSfixed_rate_debt;
            public string NSnot_defer_debt;
            public string NSthis_year_rede_amt;
            public string NSsumddct;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_J301Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;

            public string NSsaving_gubn;
            public string NSname;
            public string NStrade_nm;

            public string NSgoods_nm;
            public string NSreg_dt;
            public string NScom_cd;
            public string NSsum;
        }

        public struct NTS_J301M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSsaving_gubn;
            public string NSamtmm;
            public string NSname;
            public string NStrade_nm;
            public string NSgoods_nm;
            public string NSreg_dt;
            public string NScom_cd;
            public string NSfix_cd;
            public string NSsum;
            public string NSamt;
        }

        public struct NTS_J401Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;

            public string NSname;
            public string NStrade_nm;
            public string NSlend_dt;
            public string NSlend_loan_amt;
            public string NSsum;
        }

        public struct NTS_J401M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSacc_no;
            public string NSamtmm;

            public string NSname;
            public string NStrade_nm;
            public string NSlend_dt;
            public string NSlend_loan_amt;
            public string NSfix_cd;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_K101M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSacc_no;
            public string NSamtmm;

            public string NSname;
            public string NSstart_dt;
            public string NSend_dt;
            public string NSpay_method;

            public string NSsum;
            public string NSsumddct;
            public string NSdate;
            public string NSamt;
        }

        public struct NTS_L102Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSdonation_cd;

            public string NSname;
            public string NStrade_nm;
            public string NSsum;
            public string NSsbdy_apln_sum;
            public string NSconb_sum;
        }
        public struct NTS_L102D
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSdonation_cd;
            public string NSamtdd;

            public string NSname;
            public string NStrade_nm;

            public string NSsum;
            public string NSsbdy_apln_sum;
            public string NSconb_sum;

            public string NSamt;
            public string NSamtapln;
            public string NSamtsum;
        }

        public struct NTS_N101Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSsecu_no;

            public string NSname;
            public string NStrade_nm;
            public string NSfund_nm;
            public string NSreg_dt;
            public string NSctr_term_mm_cnt;
            public string NScom_cd;

            public string NSsum;
            public string NSddct_bs_ass_amt;
        }

        public struct NTS_N101M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSsecu_no;
            public string NSamtmm;

            public string NSname;
            public string NStrade_nm;
            public string NSfund_nm;
            public string NSreg_dt;
            public string NScom_cd;
            public string NSfix_cd;

            public string NSsum;
            public string NSddct_bs_ass_amt;
            public string NSamt;
        }

        public struct NTS_O101M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSamtmm;

            public string NSname;

            public string NSMhi_yrs;
            public string NSMltrm_yrs;
            public string NSMhi_ntf;
            public string NSMltrm_ntf;
            public string NSMhi_pmt;
            public string NSMltrm_pmt;

            public string NSsum;
            public string NShi_ntf;
            public string NSltrm_ntf;
            public string NShi_pmt;
            public string NSltrm_pmt;
        }

        public struct NTS_P102M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSamtmm;

            public string NSname;

            public string NSsum;
            public string NSsumsp_ntf;
            public string NSsumspym;
            public string NSsumjlc;
            public string NSsumntf;
            public string NSsumpmt;
            public string NSwrkp_ntf;
            public string NSrgn_pmt;
        }

        //공제신고서 마스타
        public struct NTS_A101Y
        {
            public string man_form_cd;
            public string man_resnoEncCntn;   
       	    public string man_fnm;
	        public string man_tnm;	           
	        public string man_bsnoEncCntn;
	        public string man_hshrClCd;	   
	        public string man_rsplNtnInfr;	   
	        public string man_dtyStrtDt;	   
	        public string man_dtyEndDt;	   
	        public string man_reStrtDt;	   
	        public string man_reEndDt;	   
	        public string man_rsdtClCd;	   
	        public string man_inctxWhtxTxamtMetnCd;  
	        public string man_inpmYn;
            public string man_prifChngYn;

            //주,현 근무지 연금보험료
            public string data_npHthrWaInfeeAmt;	    
	        public string data_npHthrWaInfeeDdcAmt;	
	        public string data_npHthrMcurWkarInfeeAmt;	
	        public string data_npHthrMcurWkarDdcAmt;	
	        public string data_hthrPblcPnsnInfeeAmt;	
	        public string data_hthrPblcPnsnInfeeDdcAmt;
	        public string data_mcurPblcPnsnInfeeAmt;	
	        public string data_mcurPblcPnsnInfeeDdcAmt;
	        public string data_pnsnInfeeUseAmtSum;
            public string data_pnsnInfeeDdcAmtSum;
	
            //특별소득공제
	        public string data_hthrHifeAmt;       
	        public string data_hthrHifeDdcAmt;    
	        public string data_mcurHifeAmt;	     
	        public string data_mcurHifeDdcAmt;    
	        public string data_hthrUiAmt;	     
	        public string data_hthrUiDdcAmt;	     
	        public string data_mcurUiAmt;	     
	        public string data_mcurUiDdcAmt;	     
	        public string data_infeeUseAmtSum;    
	        public string data_infeeDdcAmtSum;    
	        public string data_brwOrgnBrwnHsngTennLnpbSrmAmt;	
	        public string data_brwOrgnBrwnHsngTennLnpbSrmDdcAmt;	
	        public string data_rsdtBrwnHsngTennLnpbSrmAmt;	       
	        public string data_rsdtBrwnHsngTennLnpbSrmDdcAmt;	
	        public string data_lthClrlLnpbYr15BlwItrAmt;	       
	        public string data_lthClrlLnpbYr15BlwDdcAmt;       	
	        public string data_lthClrlLnpbYr29ItrAmt;          	
	        public string data_lthClrlLnpbYr29DdcAmt;            	
	        public string data_lthClrlLnpbY30OverItrAmt;      	
	        public string data_lthClrlLnpbY30OverDdcAmt;     	
	        public string data_lthClrlLnpbYr2012AfthY15OverItrAmt;	
	        public string data_lthClrlLnpbYr2012AfthY15OverDdcAmt;	
	        public string data_lthClrlLnpbYr2012EtcBrwItrAmt;	
	        public string data_lthClrlLnpbYr2012EtcBrwDdcAmt;	
	        public string data_lthClrlLnpbYr2015AfthFxnIrItrAmt;	
	        public string data_lthClrlLnpbYr2015AfthFxnIrDdcAmt;	
	        public string data_lthClrlLnpbYr2015AfthY15OverItrAmtItrAmt;
	        public string data_lthClrlLnpbYr2015AfthY15OverDdcAmtDdcAmt;
	        public string data_lthClrlLnpbYr2015AfthEtcBrwItrAmt;	
	        public string data_lthClrlLnpbYr2015AfthEtcBrwDdcAmt;	
	        public string data_lthClrlLnpbYr2015AfthYr15BlwItrAmt;	
	        public string data_lthClrlLnpbYr2015AfthYr15BlwDdcAmt;	
	        public string data_hsngFndsDdcAmtSum;	               
	        public string data_conbCrfwAmtLglUseAmt;	      
	        public string data_conbCrfwAmtLglDdcAmt;	      
	        public string data_conbCrfwAmtReliOrgOthUseAmt;
	        public string data_conbCrfwAmtReliOrgOthDdcAmt;
	        public string data_conbCrfwAmtReliOrgUseAmt;	
	        public string data_conbCrfwAmtReliOrgDdcAmt;	
	        public string data_conbCrfwAmtUseAmtSum;
            public string data_conbCrfwAmtDdcAmtSum;	

            public string data_yr2000BefNtplPnsnSvngUseAmt;	
	        public string data_yr2000BefNtplPnsnSvngDdcAmt;	
	        public string data_smceSbizUseAmt;   	        
	        public string data_smceSbizDdcAmt;             	
	        public string data_sbcSvngUseAmt; 	        
	        public string data_sbcSvngDdcAmt; 	        
	        public string data_lbrrHsngPrptSvngUseAmt;	
	        public string data_lbrrHsngPrptSvngDdcAmt;	
	        public string data_hsngSbcSynSvngUseAmt;        	
	        public string data_hsngSbcSynSvngDdcAmt;        	
	        public string data_hsngPrptSvngIncUseAmtSum;	
	        public string data_hsngPrptSvngIncDdcAmtSum;	
	        public string data_cpivAsctUseAmt2;	
	        public string data_cpivAsctDdcAmt2;	
	        public string data_cpivVntUseAmt2;	
	        public string data_cpivVntDdcAmt2;	
	        public string data_cpivAsctUseAmt1;	
	        public string data_cpivAsctDdcAmt1;	
	        public string data_cpivVntUseAmt1;	
	        public string data_cpivVntDdcAmt1;	
	        public string data_cpivAsctUseAmt0;	
	        public string data_cpivAsctDdcAmt0;	
	        public string data_cpivVntUseAmt0;	
	        public string data_cpivVntDdcAmt0;	
	        public string data_ivcpInvmUseAmtSum;	
	        public string data_ivcpInvmDdcAmtSum;	
	        public string data_crdcUseAmt;	        
	        public string data_drtpCardUseAmt;	
	        public string data_cshptUseAmt;	       
	        public string data_tdmrUseAmt;	        
	        public string data_pbtUseAmt;	        
	        public string data_crdcSumUseAmt;	
	        public string data_crdcSumDdcAmt;

            public string data_prsCrdcUseAmt1;            
	        public string data_tyYrPrsCrdcUseAmt;	     
	        public string data_pyrPrsAddDdcrtUseAmt;	     
	        public string data_tyShfyPrsAddDdcrtUseAmt;	

	        public string data_emstAsctCntrUseAmt;	      
	        public string data_emstAsctCntrDdcAmt;	      
	        public string data_empMntnSnmcLbrrUseAmt;	
	        public string data_empMntnSnmcLbrrDdcAmt;	
	        //public string data_lfhItrUseAmt;	
	        //public string data_lfhItrDdcAmt;	
	        public string data_ltrmCniSsUseAmt;	
	        public string data_ltrmCniSsDdcAmt;

            //세액감면 
	        public string data_frgrLbrrEntcPupCd;	
	        public string data_frgrLbrrLbrOfrDt;	
	        public string data_frgrLbrrReExryDt;	
	        public string data_frgrLbrrReRcpnDt;	
	        public string data_frgrLbrrReAlfaSbmsDt;	
	        public string data_frgrLbrrErinImnRcpnDt;	
	        public string data_frgrLbrrErinImnSbmsDt;	
	        public string data_yupSnmcReStrtDt;     
	        public string data_yupSnmcReEndDt;	
	        public string data_sctcHpUseAmt;	       
	        public string data_sctcHpDdcTrgtAmt;	
	        public string data_sctcHpDdcAmt;	       
	        public string data_rtpnUseAmt;	       
	        public string data_rtpnDdcTrgtAmt;	
	        public string data_rtpnDdcAmt;	       
	        public string data_pnsnSvngUseAmt;	
	        public string data_pnsnSvngDdcTrgtAmt;	
	        public string data_pnsnSvngDdcAmt;	
	        public string data_pnsnAccUseAmtSum;	
	        public string data_pnsnAccDdcTrgtAmtSum;
	        public string data_pnsnAccDdcAmtSum;	 
	        public string data_cvrgInscUseAmt;	 
	        public string data_cvrgInscDdcTrgtAmt2;   
	        public string data_cvrgInscDdcAmt;        
	        public string data_dsbrEuCvrgUseAmt;      
	        public string data_dsbrEuCvrgDdcTrgtAmt;  
	        public string data_dsbrEuCvrgDdcAmt;      
	        public string data_infeePymUseAmtSum;     
	        public string data_infeePymDdcTrgtAmtSum; 
	        public string data_infeePymDdcAmtSum;    
	        public string data_mdxpsPrsUseAmt;	
	        public string data_mdxpsPrsDdcTrgtAmt;	 
	        public string data_mdxpsPrsDdcAmt;	
	        public string data_mdxpsOthUseAmt;	
	        public string data_mdxpsOthDdcTrgtAmt;	
	        public string data_mdxpsOthDdcAmt;	
	        public string data_mdxpsUseAmtSum;	
	        public string data_mdxpsDdcTrgtAmtSum;	
	        public string data_mdxpsDdcAmtSum;	
	        public string data_scxpsPrsUseAmt;	
	        public string data_scxpsPrsDdcTrgtAmt;	
	        public string data_scxpsPrsDdcAmt; 	
	        public string data_scxpsKidUseAmt; 	
	        public string data_scxpsKidDdcTrgtAmt;	
	        public string data_scxpsKidDdcAmt;	
	        public string data_scxpsStdUseAmt;	
	        public string data_scxpsStdDdcTrgtAmt;	
	        public string data_scxpsStdDdcAmt;	
	        public string data_scxpsUndUseAmt;	
	        public string data_scxpsUndDdcTrgtAmt;	
	        public string data_scxpsUndDdcAmt;	
	        public string data_scxpsDsbrUseAmt;	
	        public string data_scxpsDsbrDdcTrgtAmt;	
	        public string data_scxpsDsbrDdcAmt;	
	        public string data_scxpsKidCount;	
	        public string data_scxpsStdCount;	
	        public string data_scxpsUndCount;	
	        public string data_scxpsDsbrCount;	
	        public string data_scxpsUseAmtSum;	
	        public string data_scxpsDdcTrgtAmtSum;	
	        public string data_scxpsDdcAmtSum;	
	        public string data_conb10ttswLtUseAmt;	
	        public string data_conb10ttswLtDdcTrgtAmt;	
	        public string data_conb10ttswLtDdcAmt;	        
	        public string data_conb10excsLtUseAmt;	        
	        public string data_conb10excsLtDdcTrgtAmt;	
	        public string data_conb10excsLtDdcAmt;	
	        public string data_conbLglUseAmt;	
	        public string data_conbLglDdcTrgtAmt;	 
	        public string data_conbLglDdcAmt;	
	        public string data_conbEmstAsctUseAmt;	 
	        public string data_conbEmstAsctDdcTrgtAmt;	
	        public string data_conbEmstAsctDdcAmt; 	        
	        public string data_conbReliOrgOthAppnUseAmt;	
	        public string data_conbReliOrgOthAppnDdcTrgtAmt;	
	        public string data_conbReliOrgOthAppnDdcAmt;	
	        public string data_conbReliOrgAppnUseAmt;	
	        public string data_conbReliOrgAppnDdcTrgtAmt;	
	        public string data_conbReliOrgAppnDdcAmt;	
	        public string data_conbUseAmtSum;	
	        public string data_conbDdcTrgtAmtSum;	
	        public string data_conbDdcAmtSum;	
	        public string data_ovrsSurcIncFmt;	
	        public string data_frgnPmtFcTxamt;	
	        public string data_frgnPmtWcTxamt;	    
	        public string data_frgnPmtTxamtTxpNtnNm;	
	        public string data_frgnPmtTxamtPmtDt;	      
	        public string data_frgnPmtTxamtAlfaSbmsDt;	
	        public string data_frgnPmtTxamtAbrdWkarNm;	
	        public string data_frgnDtyTerm;         
	        public string data_frgnPmtTxamtRfoNm;	
	        public string data_hsngTennLnpbUseAmt;	
	        public string data_hsngTennLnpbDdcAmt;	
	        public string data_mmrUseAmt;	
	        public string data_mmrDdcAmt;	
	        public string data_cd218;	
	        public string data_cd219;	
	        public string data_cd220;	
	        public string data_cd221;	
	        public string data_cd222;	
	        public string data_cd223;	
	        public string data_cd224;	
	        public string data_cd225;	
	        public string data_cd226;	
	        public string data_cd227;
            public string data_cd228;

            //인적공제
            public string data_seq;
            public string data_suptFmlyRltClCd;
            public string data_txprDscmNoCntn;
            public string data_nnfClCd;
            public string data_child;
            public string data_txprNm;
            public string data_bscDdcClCd;
            public string data_wmnDdcClCd;
            public string data_snprntFmlyDdcClCd;
            public string data_sccDdcClCd;
            public string data_dsbrDdcClCd;
            public string data_chbtAtprDdcClCd;
            public string data_age6Lt;
            public string data_cdVvalKrnNm;
            public string data_hifeDdcTrgtAmt;
            public string data_cvrgInscDdcTrgtAmt;
            public string data_dsbrDdcTrgtAmt;
            public string data_mdxpsDdcTrgtAmt;
            public string data_scxpsDdcTrgtAmt;
            public string data_crdcDdcTrgtAmt;
            public string data_drtpCardDdcTrgtAmt;
            public string data_cshptDdcTrgtAmt;
            public string data_tdmrDdcTrgtAmt;
            public string data_pbtDdcTrgtAmt;
            public string data_conbDdcTrgtAmt;

        }

        //공제신고서 연금저축등 소득.세액 명세 마스타
        public struct NTS_Doc_B101Y
        {
            public string  form_cd;
	        public string  bsnoEncCntn;
	        public string  resnoEncCntn;
	        public string  tnm;
	        public string  fnm;
	        public string  adr;
            public string pfbAdr;
        }

        //공제신고서 퇴직연금 공제명세
        public struct NTS_Doc_B101R
        {
            public string form_cd;
            public string bsnoEncCntn;
            public string resnoEncCntn;
            
            public string rtpnAccRtpnCl;
	        public string rtpnFnnOrgnCd;
	        public string rtpnAccFnnCmp;
	        public string rtpnAccAccno;
	        public string rtpnAccPymAmt;
            public string rtpnAccTxamtDdcAmt;
        }

        //공제신고서 연금저축 공제명세
        public struct NTS_Doc_B101P
        {
            public string form_cd;
            public string bsnoEncCntn;
            public string resnoEncCntn;
            
            public string pnsnSvngAccPnsnSvngCl;
	        public string pnsnSvngFnnOrgnCd;	
	        public string pnsnSvngAccFnnCmp;	
	        public string pnsnSvngAccAccno;	
	        public string pnsnSvngAccPymAmt;
            public string ipnsnSvngAccNcTxamtDdcAmt;
        }

        //공제신고서주택마련저축 공제명세
        public struct NTS_Doc_B101H
        {
            public string form_cd;
            public string bsnoEncCntn;
            public string resnoEncCntn;
            
            public string hsngPrptSvngSvngCl;
	        public string jnngDt;
	        public string hsngPrptSvngFnnOrgnCd;
	        public string hsngPrptSvngFnnCmp;	
	        public string hsngPrptSvngAccno;	
	        public string hsngPrptSvngPymAmt;
            public string hsngPrptSvngIncDdcAmt;
        }

        //공제신고서 장기집합저축 공제명세
        public struct NTS_Doc_B101L
        {
            public string form_cd;
            public string bsnoEncCntn;
            public string resnoEncCntn;
                      
	        public string ltrmCniSsfnnOrgnCd;
	        public string ltrmCniSsFnnCmp;	
	        public string ltrmCniSsAccno;	
	        public string ltrmCniSsPymAmt;
            public string ltrmCniSsIncDdcAmt;
        }


        public struct NTS_G108Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;

            public string NSname;
            public string NStrade_nm;

            public string NSgnrl_sum;		
	        public string NStdmr_sum;		
	        public string NStrp_sum;		
	        public string NSisld_sum;		 
	        public string NStot_sum;		
	        public string NSgnrl_mar_sum;		 
	        public string NStdmr_mar_sum;		 
	        public string NStrp_mar_sum;		 
	        public string NSisld_mar_sum;		 
	        public string NStot_mar_sum;		
	        public string NSgnrl_aprl_sum;		
	        public string NStdmr_aprl_sum;		
	        public string NStrp_aprl_sum;		
	        public string NSisld_aprl_sum;		
	        public string NStot_aprl_sum;		
	        public string NSgnrl_jan_sum;		
	        public string NStdmr_jan_sum;		
	        public string NStrp_jan_sum;		
	        public string NSisld_jan_sum;
            public string NStot_jan_sum;	

            public string NSsum;
        }

        public struct NTS_G108M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;
            public string NSamtmm;

            public string NSname;
            public string NStrade_nm;

            public string NSgnrl_sum;
            public string NStdmr_sum;
            public string NStrp_sum;
            public string NSisld_sum;
            public string NStot_sum;
            public string NSgnrl_mar_sum;
            public string NStdmr_mar_sum;
            public string NStrp_mar_sum;
            public string NSisld_mar_sum;
            public string NStot_mar_sum;
            public string NSgnrl_aprl_sum;
            public string NStdmr_aprl_sum;
            public string NStrp_aprl_sum;
            public string NSisld_aprl_sum;
            public string NStot_aprl_sum;
            public string NSgnrl_jan_sum;
            public string NStdmr_jan_sum;
            public string NStrp_jan_sum;
            public string NSisld_jan_sum;
            public string NStot_jan_sum;	

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_G110Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;

            public string NSname;
            public string NStrade_nm;

            public string NStot_pre_year_sum;
            public string NStot_curr_year_sum;

            public string NStdmr_tot_pre_year_sum;
            public string NStdmr_tot_curr_year_sum;

            public string NSgnrl_sum;
            public string NStdmr_sum;
            public string NStrp_sum;
            public string NSisld_sum;
            public string NStot_sum;

            public string NStfhy_gnrl_sum;
	        public string NStfhy_tdmr_sum;
            public string NStfhy_trp_sum;
            public string NStfhy_isld_sum;
            public string NStfhy_tot_sum;
            public string NSshfy_gnrl_sum;
            public string NSshfy_tdmr_sum;
            public string NSshfy_trp_sum;
            public string NSshfy_isld_sum;
            public string NSshfy_tot_sum;

            public string NSsum;
        }

        public struct NTS_G110M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;
            public string NSamtmm;

            public string NSname;
            public string NStrade_nm;

            public string NStot_pre_year_sum;
            public string NStot_curr_year_sum;
            public string NStdmr_tot_pre_year_sum;
            public string NStdmr_tot_curr_year_sum;


            public string NSgnrl_sum;
            public string NStdmr_sum;
            public string NStrp_sum;
            public string NSisld_sum;
            public string NStot_sum;

            public string NStfhy_gnrl_sum;
            public string NStfhy_tdmr_sum;
            public string NStfhy_trp_sum;
            public string NStfhy_isld_sum;
            public string NStfhy_tot_sum;
            public string NSshfy_gnrl_sum;
            public string NSshfy_tdmr_sum;
            public string NSshfy_trp_sum;
            public string NSshfy_isld_sum;
            public string NSshfy_tot_sum;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_G208M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;
            public string NSamtmm;

            public string NSname;            

            public string NSgnrl_sum;
            public string NStdmr_sum;
            public string NStrp_sum;
            public string NSisld_sum;
            public string NStot_sum;
            public string NSgnrl_mar_sum;
            public string NStdmr_mar_sum;
            public string NStrp_mar_sum;
            public string NSisld_mar_sum;
            public string NStot_mar_sum;
            public string NSgnrl_aprl_sum;
            public string NStdmr_aprl_sum;
            public string NStrp_aprl_sum;
            public string NSisld_aprl_sum;
            public string NStot_aprl_sum;
            public string NSgnrl_jan_sum;
            public string NStdmr_jan_sum;
            public string NStrp_jan_sum;
            public string NSisld_jan_sum;
            public string NStot_jan_sum;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_G210M
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSuse_place_cd;
            public string NSamtmm;

            public string NSname;

            public string NStot_pre_year_sum;
            public string NStot_curr_year_sum;
            public string NStdmr_tot_pre_year_sum;
            public string NStdmr_tot_curr_year_sum;

            public string NSgnrl_sum;
            public string NStdmr_sum;
            public string NStrp_sum;
            public string NSisld_sum;
            public string NStot_sum;

            public string NStfhy_gnrl_sum;
	        public string NStfhy_tdmr_sum;
            public string NStfhy_trp_sum;
            public string NStfhy_isld_sum;
            public string NStfhy_tot_sum;
            public string NSshfy_gnrl_sum;
            public string NSshfy_tdmr_sum;
            public string NSshfy_trp_sum;
            public string NSshfy_isld_sum;
            public string NSshfy_tot_sum;

            public string NSsum;
            public string NSamt;
        }

        public struct NTS_J501Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSlsor_no;

            public string NSname;
            public string NSlsor_nm;

            public string NSstart_dt;
            public string NSend_dt;
            public string NSadr;
            public string NSarea;
            public string NStypeCd;

            public string NSsum;
        }

        public struct NTS_B201Y
        {
            public string NSform_cd;
            public string NSdat_cd;
            public string NSresid;
            public string NSbusnid;
            public string NSAcc_No;

            public string NSname;
            public string NStrade_nm;

            public string NSGoods_nm;
            public string NSInsu_resid;
            public string NSInsu_nm;           

            public string NSsum;
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
