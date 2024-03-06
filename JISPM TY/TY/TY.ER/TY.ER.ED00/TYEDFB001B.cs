using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Text;
using System.Net;
using System.Xml;
using System.Security.Cryptography;


namespace TY.ER.ED00
{
    /// <summary>
    /// 세관EDI 자료 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.04.24 09:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_74OD2366 : 반입보고서 생성 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BF4Z352 : 처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_74OAE363 : 마스타 생성 하시겠습니까?
    ///  TY_M_UT_74OAE364 : 전송 처리하시겠습니까?
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  BTN_MAST : 마스타 생성
    ///  BTN_TRANS : 전 송
    ///  CLO : 닫기
    ///  CHK_EDI : EDI 체크 옵션
    ///  EDATE : 종료일자
    ///  EDIGJ : 공장
    ///  SDATE : 시작일자
    /// </summary>
    /// 

    //--------------------------------------------------------------
    //# 송신
    //DATA생성경로: C:\WINMATE\OUT\UPLOAD.XXX
    //프로그램경로: C:\WINMATE\BIN\HERMES.EXE -m2
    //# 송신조회
    //프로그램경로: C:\WINMATE\BIN\LINKUP.EXE C:\WINMATE\MSRS.SCF
    //# 통신설정
    //프로그램경로: C:\WINMATE\BIN\COMSETUP.EXE
    //# 입력내용
    //로그인ID    : TYS950310S
    //암호        : TYC1111
    //MSRS암호    : TYC1111
    //통신SCRIPT DERECTORY -> FTPMFCS
    //--------------------------------------------------------------

    public partial class TYEDFB001B : TYBase
    {
        System.Collections.Generic.List<object[]> DocDatas = new System.Collections.Generic.List<object[]>();

         #region  Description : DAT 변수 선언
           private TYData     DAT02_EDIGJ;      
           private TYData     DAT02_EDIJUKHA;   
           private TYData     DAT02_EDIBLMSN;   
           private TYData     DAT02_EDIBLHSN;   
           private TYData     DAT02_EDIDATE;    
           private TYData     DAT02_EDINO1;     
           private TYData     DAT02_EDINO2;     
           private TYData     DAT02_EDINO3;          
           private TYData     DAT02_EDITIME;    
           private TYData     DAT02_EDIJSGB;    
           private TYData     DAT02_EDIBANGB;   
           private TYData     DAT02_EDIHMGB;    
           private TYData     DAT02_EDIBUNHAL;  
           private TYData     DAT02_EDISINNO;   
           private TYData     DAT02_EDIIPQTY;   
           private TYData     DAT02_EDINUQTY;   
           private TYData     DAT02_EDICHASU;   
           private TYData     DAT02_EDIPOJANG;  
           private TYData     DAT02_EDICOUNT;   
           private TYData     DAT02_EDIIPHANG;  
           private TYData     DAT02_EDIHANGCHA; 
           private TYData     DAT02_EDIHWAMUL;  
           private TYData     DAT02_EDIHWAJU;   
           private TYData     DAT02_EDIRCVGB;   
           private TYData     DAT02_EDIMSG;     
           private TYData     DAT02_EDIHMNO1;
           private TYData     DAT02_EDIHMNO2;
           
           private TYData     DAT02_EDICHQTY;
           private TYData     DAT02_EDICHCNT;
           private TYData     DAT02_EDINUCNT;

           private TYData  DAT02_EDIBOLOC;
           private TYData  DAT02_EDIIPCNT;
           private TYData  DAT02_EDIPUMNM;
           private TYData  DAT02_EDILOCGB;
           private TYData  DAT02_EDIACDSAU;
           private TYData  DAT02_EDIWSANJI;
           private TYData  DAT02_EDISDATE;
           private TYData  DAT02_EDIEDATE;
           private TYData  DAT02_EDIBRCNT;
           private TYData  DAT02_EDIMULGB;
           private TYData  DAT02_EDIBLNO;
           private TYData  DAT02_EDICDATE;
           private TYData  DAT02_EDIIPSINNO;
           private TYData  DAT02_EDIBANIL;

           private TYData  DAT02_EDICHULIL;
           private TYData  DAT02_EDISAUP;
           private TYData  DAT02_EDIJUSO;
           private TYData  DAT02_EDICHTIME;
           private TYData  DAT02_EDIGJIL;
           private TYData  DAT02_EDIBGIL;
           private TYData  DAT02_EDIBOIL;
           private TYData  DAT02_EDIBOQTY;
           private TYData  DAT02_EDIBOAGMT;
           private TYData  DAT02_EDIBOAMT;
           private TYData  DAT02_EDIRATE;
           private TYData  DAT02_EDIISSEQ;
           private TYData DAT02_EDICHNO2;
           private TYData DAT02_EDICHNO3;

         #endregion

           private string fsSHA256PASS;
           private int fiTransCnt = 0;

        #region  Description : 폼 로드 이벤트
        public TYEDFB001B()
        {
            InitializeComponent();

            this.SetPopupStyle();

            #region  Description : DAT 변수 생성
            DAT02_EDIGJ = new TYData("DAT02_EDIGJ", null);
            DAT02_EDIJUKHA = new TYData("DAT02_EDIJUKHA", null);
            DAT02_EDIBLMSN = new TYData("DAT02_EDIBLMSN", null);
            DAT02_EDIBLHSN = new TYData("DAT02_EDIBLHSN", null);
            DAT02_EDIDATE = new TYData("DAT02_EDIDATE", null);
            DAT02_EDINO1 = new TYData("DAT02_EDINO1", null);
            DAT02_EDINO2 = new TYData("DAT02_EDINO2", null);
            DAT02_EDINO3 = new TYData("DAT02_EDINO3", null);
            DAT02_EDITIME = new TYData("DAT02_EDITIME", null);
            DAT02_EDIJSGB = new TYData("DAT02_EDIJSGB", null);
            DAT02_EDIBANGB = new TYData("DAT02_EDIBANGB", null);
            DAT02_EDIHMGB = new TYData("DAT02_EDIHMGB", null);
            DAT02_EDIBUNHAL = new TYData("DAT02_EDIBUNHAL", null);
            DAT02_EDISINNO = new TYData("DAT02_EDISINNO", null);
            DAT02_EDIIPQTY = new TYData("DAT02_EDIIPQTY", null);
            DAT02_EDINUQTY = new TYData("DAT02_EDINUQTY", null);
            DAT02_EDICHASU = new TYData("DAT02_EDICHASU", null);
            DAT02_EDIPOJANG = new TYData("DAT02_EDIPOJANG", null);
            DAT02_EDICOUNT = new TYData("DAT02_EDICOUNT", null);
            DAT02_EDIIPHANG = new TYData("DAT02_EDIIPHANG", null);
            DAT02_EDIHANGCHA = new TYData("DAT02_EDIHANGCHA", null);
            DAT02_EDIHWAMUL = new TYData("DAT02_EDIHWAMUL", null);
            DAT02_EDIHWAJU = new TYData("DAT02_EDIHWAJU", null);
            DAT02_EDIRCVGB = new TYData("DAT02_EDIRCVGB", null);
            DAT02_EDIMSG = new TYData("DAT02_EDIMSG", null);
            DAT02_EDIHMNO1 = new TYData("DAT02_EDIHMNO1", null);
            DAT02_EDIHMNO2 = new TYData("DAT02_EDIHMNO2", null);

            DAT02_EDICHQTY = new TYData("DAT02_EDICHQTY", null);
            DAT02_EDICHCNT = new TYData("DAT02_EDICHCNT", null);
            DAT02_EDINUCNT = new TYData("DAT02_EDINUCNT", null);

            DAT02_EDIBOLOC = new TYData("DAT02_EDIBOLOC", null);
            DAT02_EDIIPCNT = new TYData("DAT02_EDIIPCNT", null);
            DAT02_EDIPUMNM = new TYData("DAT02_EDIPUMNM", null);
            DAT02_EDILOCGB = new TYData("DAT02_EDILOCGB", null);
            DAT02_EDIACDSAU = new TYData("DAT02_EDIACDSAU", null);
            DAT02_EDIWSANJI = new TYData("DAT02_EDIWSANJI", null);
            DAT02_EDISDATE = new TYData("DAT02_EDISDATE", null);
            DAT02_EDIEDATE = new TYData("DAT02_EDIEDATE", null);
            DAT02_EDIBRCNT = new TYData("DAT02_EDIBRCNT", null);
            DAT02_EDIMULGB = new TYData("DAT02_EDIMULGB", null);
            DAT02_EDIBLNO = new TYData("DAT02_EDIBLNO", null);
            DAT02_EDICDATE = new TYData("DAT02_EDICDATE", null);
            DAT02_EDIIPSINNO = new TYData("DAT02_EDIIPSINNO", null);
            DAT02_EDIBANIL = new TYData("DAT02_EDIBANIL", null);
            
            DAT02_EDICHULIL = new TYData("DAT02_EDICHULIL", null);
            DAT02_EDISAUP = new TYData("DAT02_EDISAUP", null);
            DAT02_EDIJUSO = new TYData("DAT02_EDIJUSO", null);

            DAT02_EDICHTIME = new TYData("DAT02_EDICHTIME", null);
            DAT02_EDIGJIL = new TYData("DAT02_EDIGJIL", null);
            DAT02_EDIBGIL = new TYData("DAT02_EDIBGIL", null);
            DAT02_EDIBOIL = new TYData("DAT02_EDIBOIL", null);
            DAT02_EDIBOQTY = new TYData("DAT02_EDIBOQTY", null);
            DAT02_EDIBOAGMT = new TYData("DAT02_EDIBOAGMT", null);
            DAT02_EDIBOAMT = new TYData("DAT02_EDIBOAMT", null);
            DAT02_EDIRATE = new TYData("DAT02_EDIRATE", null);
            DAT02_EDIISSEQ = new TYData("DAT02_EDIISSEQ", null);

            DAT02_EDICHNO2 = new TYData("DAT02_EDICHNO2", null);
            DAT02_EDICHNO3 = new TYData("DAT02_EDICHNO3", null);


            #endregion

            
        }

        private void TYEDFB001B_Load(object sender, System.EventArgs e)
        {
            #region  Description : DAT 변수 ControlFactory Add
            this.ControlFactory.Add(this.DAT02_EDIGJ);
            this.ControlFactory.Add(this.DAT02_EDIJUKHA);
            this.ControlFactory.Add(this.DAT02_EDIBLMSN);
            this.ControlFactory.Add(this.DAT02_EDIBLHSN);
            this.ControlFactory.Add(this.DAT02_EDIDATE);
            this.ControlFactory.Add(this.DAT02_EDINO1);
            this.ControlFactory.Add(this.DAT02_EDINO2);
            this.ControlFactory.Add(this.DAT02_EDINO3);
            this.ControlFactory.Add(this.DAT02_EDITIME);
            this.ControlFactory.Add(this.DAT02_EDIJSGB);
            this.ControlFactory.Add(this.DAT02_EDIBANGB);
            this.ControlFactory.Add(this.DAT02_EDIHMGB);
            this.ControlFactory.Add(this.DAT02_EDIBUNHAL);
            this.ControlFactory.Add(this.DAT02_EDISINNO);
            this.ControlFactory.Add(this.DAT02_EDIIPQTY);
            this.ControlFactory.Add(this.DAT02_EDINUQTY);
            this.ControlFactory.Add(this.DAT02_EDICHASU);
            this.ControlFactory.Add(this.DAT02_EDIPOJANG);
            this.ControlFactory.Add(this.DAT02_EDICOUNT);
            this.ControlFactory.Add(this.DAT02_EDIIPHANG);
            this.ControlFactory.Add(this.DAT02_EDIHANGCHA);
            this.ControlFactory.Add(this.DAT02_EDIHWAMUL);
            this.ControlFactory.Add(this.DAT02_EDIHWAJU);
            this.ControlFactory.Add(this.DAT02_EDIRCVGB);
            this.ControlFactory.Add(this.DAT02_EDIMSG);
            this.ControlFactory.Add(this.DAT02_EDIHMNO1);
            this.ControlFactory.Add(this.DAT02_EDIHMNO2);

            this.ControlFactory.Add(this.DAT02_EDICHQTY);
            this.ControlFactory.Add(this.DAT02_EDICHCNT);
            this.ControlFactory.Add(this.DAT02_EDINUCNT);

            this.ControlFactory.Add(this.DAT02_EDIBOLOC);
            this.ControlFactory.Add(this.DAT02_EDIIPCNT);
            this.ControlFactory.Add(this.DAT02_EDIPUMNM);
            this.ControlFactory.Add(this.DAT02_EDILOCGB);
            this.ControlFactory.Add(this.DAT02_EDIACDSAU);
            this.ControlFactory.Add(this.DAT02_EDIWSANJI);
            this.ControlFactory.Add(this.DAT02_EDISDATE); 
            this.ControlFactory.Add(this.DAT02_EDIEDATE); 
            this.ControlFactory.Add(this.DAT02_EDIBRCNT);
            this.ControlFactory.Add(this.DAT02_EDIMULGB);
            this.ControlFactory.Add(this.DAT02_EDIBLNO);
            this.ControlFactory.Add(this.DAT02_EDICDATE);
            this.ControlFactory.Add(this.DAT02_EDIIPSINNO);
            this.ControlFactory.Add(this.DAT02_EDIBANIL);
            this.ControlFactory.Add(this.DAT02_EDICHULIL);
            this.ControlFactory.Add(this.DAT02_EDISAUP);
            this.ControlFactory.Add(this.DAT02_EDIJUSO);

            this.ControlFactory.Add(this.DAT02_EDICHTIME);
            this.ControlFactory.Add(this.DAT02_EDIGJIL);
            this.ControlFactory.Add(this.DAT02_EDIBGIL);
            this.ControlFactory.Add(this.DAT02_EDIBOIL);
            this.ControlFactory.Add(this.DAT02_EDIBOQTY);
            this.ControlFactory.Add(this.DAT02_EDIBOAGMT);
            this.ControlFactory.Add(this.DAT02_EDIBOAMT);
            this.ControlFactory.Add(this.DAT02_EDIRATE);
            this.ControlFactory.Add(this.DAT02_EDIISSEQ);
            this.ControlFactory.Add(this.DAT02_EDICHNO2);
            this.ControlFactory.Add(this.DAT02_EDICHNO3);         
            
            #endregion            

            fsSHA256PASS = UP_Set_Sha256(TYEdiLayout.KniaEdiPass);

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_REC.ProcessCheck += new TButton.CheckHandler(BTN61_REC_ProcessCheck);
          
            RDB01_CHK1.Checked = true;
            RDB01_CHK2.Checked = false;
            RDB01_CHK3.Checked = false;
            DTP01_EDATE.Enabled = false;

            UP_ProGressBarClear();

            this.DTP01_SDATE.SetValue(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            
            string sSdate = DTP01_SDATE.GetString().ToString();
            string sEdate = DTP01_EDATE.GetString().ToString();
            
            if (RDB01_CHK1.Checked == true)
            {
                UP_Get_DataIPGOSF(sSdate);
                UP_Set_WebServiceDataToTrans(sSdate, sEdate, "5HJ");
            }
            else if (RDB01_CHK2.Checked == true)
            {
                UP_Get_DataCHULSF(sSdate);
                UP_Set_WebServiceDataToTrans(sSdate, sEdate, "5HO");
            }
            else if (RDB01_CHK3.Checked == true)
            {
                UP_Get_DataBOHUMF(sSdate, sEdate);
                UP_Set_WebServiceDataToTrans(sSdate, sEdate, "5HI");
            }
            string sTitleMsg = fiTransCnt.ToString() + "건이 전송되었습니다!";

            this.ShowCustomMessage(sTitleMsg, "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sTitleMsg = string.Empty;
            string sTitle = string.Empty;
            string sCnt = string.Empty;
            string sSdate = DTP01_SDATE.GetString().ToString();
            string sEdate = DTP01_EDATE.GetString().ToString();

            if (RDB01_CHK1.Checked == true)
            {
                sTitle = "화재보험 반입보고서";

                this.DbConnector.CommandClear();                
                this.DbConnector.Attach("TY_P_UT_75GHL521", sSdate);
                DataTable dk = this.DbConnector.ExecuteDataTable();
                sCnt = dk.Rows.Count.ToString();
                
            }
            else if (RDB01_CHK2.Checked == true)
            {
                sTitle = "화재보험 반출보고서";
              
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75I9T559", sSdate, "T", "T");
                DataTable dk = this.DbConnector.ExecuteDataTable();
                sCnt = dk.Rows.Count.ToString();
              
            }
            else if (RDB01_CHK3.Checked == true)
            {
                sTitle = "보험영수증";
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75J99568", sSdate, sEdate);
                DataTable dk = this.DbConnector.ExecuteDataTable();
                sCnt = dk.Rows.Count.ToString();

            }           

            sTitleMsg = sTitle + ": " + sCnt + "건이 있습니다 전송하시겠습니까?";

            if (!this.ShowCustomMessage(sTitleMsg, "확인", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion    
        
        #region  Description : 수신 버튼 이벤트
        private void BTN61_REC_Click(object sender, EventArgs e)
        {
            string sSdate = DTP01_SDATE.GetString().ToString();
            string sEdate = DTP01_EDATE.GetString().ToString();


            if (RDB01_CHK1.Checked == true)
            {                
                UP_Get_WebServiceDataToConvert(sSdate, sEdate, "5HJ");
            }
            else if (RDB01_CHK2.Checked == true)
            {
                UP_Get_WebServiceDataToConvert(sSdate, sEdate, "5HO");
            }
            else if (RDB01_CHK3.Checked == true)
            {
                UP_Get_WebServiceDataToConvert(sSdate, sEdate, "5HI");
            }            

            string sTitleMsg = fiTransCnt.ToString() + " 건이 수신되었습니다!";

            if (fiTransCnt > 0)
            {
                this.ShowCustomMessage(sTitleMsg, "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                this.ShowCustomMessage("수신된 자료가 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }            
        }

        private void BTN61_REC_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sTitleMsg = string.Empty;
            string sTitle = string.Empty;
            string sCnt = string.Empty;
            string sSdate = DTP01_SDATE.GetString().ToString();
            string sEdate = DTP01_EDATE.GetString().ToString();

            if (RDB01_CHK1.Checked == true)
            {
                sTitle = "화재보험 반입보고서";
                DataTable dk = UP_Get_DataBindingEDIIPGOSF(sSdate);
                sCnt = dk.Rows.Count.ToString();

            }
            else if (RDB01_CHK2.Checked == true)
            {
                sTitle = "화재보험 반출보고서";
                DataTable dk = UP_Get_DataBindingEDICHULSF(sSdate);
                sCnt = dk.Rows.Count.ToString();

            }
            else if (RDB01_CHK3.Checked == true)
            {
                sTitle = "보험영수증";
                DataTable dk = UP_Get_DataBindingEDIBOHUMF(sSdate, sEdate);
                sCnt = dk.Rows.Count.ToString();
            }

            sTitleMsg = sTitle + ": " + sCnt + "  건이 있습니다 수신하시겠습니까?";

            if (!this.ShowCustomMessage(sTitleMsg, "확인", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 화재보험 반입보고서 자료 생성
        private void UP_Get_DataIPGOSF(string sDate)
        {
            pgBar.Maximum = 0;
            pgBar.Minimum = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GHK520", sDate, "T");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_75GHM522", "T", sDate,
                                                                    dt.Rows[i]["EDIJUKHA"].ToString(),
                                                                    dt.Rows[i]["EDIBLMSN"].ToString(),
                                                                    dt.Rows[i]["EDIBLHSN"].ToString());
                    }
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GHL521", sDate);
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count; 

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(dk.Rows[i]["IPSINOYY"].ToString());
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString())));

                        this.DAT02_EDIGJ.SetValue("T");
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IPBLNOSEQ"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(dk.Rows[i]["IPHBLNOSEQ"].ToString());
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["CMBOGODAT"].ToString());
                        this.DAT02_EDITIME.SetValue(dk.Rows[i]["CMHITIM"].ToString());
                        this.DAT02_EDIJSGB.SetValue("9");
                        if (Convert.ToInt16(dk.Rows[i]["IPHBLNOSEQ"].ToString()) <= 0)
                        {
                            this.DAT02_EDIBANGB.SetValue(dk.Rows[i]["IPBANGUBUN"].ToString());
                        }
                        else
                        {
                            this.DAT02_EDIBANGB.SetValue(dk.Rows[i]["IPBANGUBUN"].ToString() != "20" ? "23" : dk.Rows[i]["IPBANGUBUN"].ToString());
                        }
                        this.DAT02_EDIHMGB.SetValue(dk.Rows[i]["CMPUMCODE"].ToString());
                        this.DAT02_EDIBUNHAL.SetValue("A");
                        if (dk.Rows[i]["IPBANGUBUN"].ToString() == "20")
                        {
                            this.DAT02_EDISINNO.SetValue(dk.Rows[i]["BOSAENUM"].ToString());
                            this.DAT02_EDICOUNT.SetValue(dk.Rows[i]["IPCOUNT"].ToString());
                        }
                        else
                        {
                            this.DAT02_EDISINNO.SetValue("");
                            this.DAT02_EDICOUNT.SetValue("0");
                        }
                        this.DAT02_EDIIPQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IPBSQTY"].ToString()) * 1000, 0));
                        this.DAT02_EDINUQTY.SetValue("0");
                        this.DAT02_EDICHASU.SetValue("0");
                        this.DAT02_EDIPOJANG.SetValue("VL");

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["IPIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["IPBONSUN"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["IPHWAMUL"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["IPHWAJU"].ToString());

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");
                        this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["IPSINOYY"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(dk.Rows[i]["IPSINO"].ToString());

                        this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["IPBLNO"].ToString());
                        this.DAT02_EDIBANIL.SetValue(dk.Rows[i]["CMBANIL"].ToString());

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),     
                                            this.DAT02_EDIJUKHA.GetValue(),  
                                            this.DAT02_EDIBLMSN.GetValue(),  
                                            this.DAT02_EDIBLHSN.GetValue(),  
                                            this.DAT02_EDIDATE.GetValue(),   
                                            this.DAT02_EDITIME.GetValue(),   
                                            this.DAT02_EDIJSGB.GetValue(),   
                                            this.DAT02_EDIBANGB.GetValue(),  
                                            this.DAT02_EDIHMGB.GetValue(),   
                                            this.DAT02_EDIBUNHAL.GetValue(), 
                                            this.DAT02_EDISINNO.GetValue(),  
                                            this.DAT02_EDIIPQTY.GetValue(),  
                                            this.DAT02_EDINUQTY.GetValue(),  
                                            this.DAT02_EDICHASU.GetValue(),  
                                            this.DAT02_EDIPOJANG.GetValue(), 
                                            this.DAT02_EDICOUNT.GetValue(),  
                                            this.DAT02_EDIIPHANG.GetValue(), 
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            dk.Rows[i]["IPBONSUNNM"].ToString(),
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            dk.Rows[i]["IPHWAJUNM"].ToString(),
                                            this.DAT02_EDIHWAJU.GetValue(),  
                                            dk.Rows[i]["IPHWAMULNM"].ToString(),
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),                                                
                                            this.DAT02_EDIRCVGB.GetValue(),  
                                            this.DAT02_EDIMSG.GetValue(),    
                                            this.DAT02_EDIHMNO1.GetValue(),  
                                            this.DAT02_EDIHMNO2.GetValue(),
                                            this.DAT02_EDIBLNO.GetValue(),
                                            this.DAT02_EDIBANIL.GetValue(),
                                            TYUserInfo.EmpNo
                                            });
                    }

                    pgBar.Value = pgBar.Value + 1;
                    pgBar.Refresh();
                }

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_UT_75GHM523", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion                         

        #region  Description : 화재보험 반출보고서 자료 생성
        private void UP_Get_DataCHULSF(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75I9T559", sDate, "T", "T");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_75I9V560", "T", sDate,
                                                                    dt.Rows[i]["VSJUKHA"].ToString(),
                                                                    dt.Rows[i]["CHMSNSEQ"].ToString(),
                                                                    dt.Rows[i]["CHHSNSEQ"].ToString(),
                                                                    dt.Rows[i]["CSSINNO"].ToString()
                                                                    );
                    }
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75I9T559", sDate, "T", "T");
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count; 

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue("T");
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["CHCHULIL"].ToString());
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["CHMSNSEQ"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(Set_Fill4(dk.Rows[i]["CHHSNSEQ"].ToString()));

                        this.DAT02_EDISINNO.SetValue(dk.Rows[i]["CSSINNO"].ToString());

                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(dk.Rows[i]["CHCHULIL"].ToString().Substring(0, 4));
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(dk.Rows[i]["CHCHULIL"].ToString().Substring(0, 4)))));

                        this.DAT02_EDITIME.SetValue(Set_Fill4(dk.Rows[i]["CHCHEND"].ToString()) + "00");
                        this.DAT02_EDIJSGB.SetValue("9");
                        this.DAT02_EDIBANGB.SetValue(dk.Rows[i]["CSBANGB"].ToString());

                        this.DAT02_EDICHASU.SetValue(dk.Rows[i]["EDICHASU"].ToString());

                        //분할유무 판단
                        this.DAT02_EDIBUNHAL.SetValue(UP_Get_BunHalCheck(dk.Rows[i]["CSCUQTY"].ToString(),
                                                                         dk.Rows[i]["CSSINQTY"].ToString(),
                                                                         dk.Rows[i]["PRECHMTQTY"].ToString(),
                                                                         dk.Rows[i]["CHMTQTY"].ToString()
                                                                         ));
                        if (this.DAT02_EDIBUNHAL.GetValue().ToString() == "L")
                        {
                            double dCHMTQTY = Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) - (Convert.ToDouble(dk.Rows[i]["PRECHMTQTY"].ToString()) + Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString()));
                            dCHMTQTY = dCHMTQTY + Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString());

                            this.DAT02_EDICHQTY.SetValue(Math.Round(dCHMTQTY * 1000, 0));
                        }
                        else
                        {
                            this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString()) * 1000, 0));
                        }
                        this.DAT02_EDICHCNT.SetValue("0");

                        this.DAT02_EDINUQTY.SetValue(Convert.ToString(Math.Round(Convert.ToDouble(dk.Rows[i]["PRECHMTQTY"].ToString()) * 1000, 0) +
                                                       Convert.ToDouble(this.DAT02_EDICHQTY.GetValue().ToString()))
                                                     );
                        this.DAT02_EDINUCNT.SetValue("0");

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["CHIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["CHBONSUN"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["CHHWAMUL"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["CHHWAJU"].ToString());

                        this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["CHBLNO"].ToString());

                        this.DAT02_EDIIPQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IPBSQTY"].ToString()) * 1000, 0));

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");

                        this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["IPSINOYY"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString())));

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),
                                                this.DAT02_EDIDATE.GetValue(),
                                                this.DAT02_EDIJUKHA.GetValue(),
                                                this.DAT02_EDIBLMSN.GetValue(),
                                                this.DAT02_EDIBLHSN.GetValue(),
                                                this.DAT02_EDISINNO.GetValue(),
                                                this.DAT02_EDITIME.GetValue(),
                                                this.DAT02_EDIJSGB.GetValue(),
                                                this.DAT02_EDIBANGB.GetValue(),
                                                this.DAT02_EDIBUNHAL.GetValue(),
                                                this.DAT02_EDINO1.GetValue(),
                                                this.DAT02_EDINO2.GetValue(),
                                                this.DAT02_EDINO3.GetValue(),
                                                this.DAT02_EDICHQTY.GetValue(),
                                                this.DAT02_EDICHCNT.GetValue(),
                                                this.DAT02_EDINUQTY.GetValue(),
                                                this.DAT02_EDINUCNT.GetValue(),
                                                this.DAT02_EDICHASU.GetValue(),
                                                this.DAT02_EDIIPHANG.GetValue(),
                                                this.DAT02_EDIHANGCHA.GetValue(),
                                                dk.Rows[i]["CHBONSUNNM"].ToString(),
                                                this.DAT02_EDIHWAMUL.GetValue(),
                                                dk.Rows[i]["CHHWAMULNM"].ToString(),
                                                this.DAT02_EDIHWAJU.GetValue(),
                                                dk.Rows[i]["CHHWAJUNM"].ToString(),
                                                this.DAT02_EDIBLNO.GetValue(),
                                                dk.Rows[i]["CMBANIL"].ToString(),
                                                dk.Rows[i]["CMHITIM"].ToString(),
                                                this.DAT02_EDIIPQTY.GetValue(),
                                                this.DAT02_EDIRCVGB.GetValue(),
                                                this.DAT02_EDIMSG.GetValue(),
                                                this.DAT02_EDIHMNO1.GetValue(),
                                                this.DAT02_EDIHMNO2.GetValue(),
                                                TYUserInfo.EmpNo
                                            });
                    }

                    pgBar.Value = pgBar.Value + 1;
                    pgBar.Refresh();
                }

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_UT_75IAA564", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion

        #region  Description : 화재보험 보험영수증 자료 생성
        private void UP_Get_DataBOHUMF(string sSDate, string sEDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75J99568", sSDate, sEDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_75J93569", "T",
                                                                    dt.Rows[i]["EDNIMPSIGN"].ToString(),
                                                                    dt.Rows[i]["ISCHULIL"].ToString().Substring(0,4),
                                                                    String.Format("{0:D6}", Convert.ToInt64(dt.Rows[i]["ISSEQ"].ToString()))
                                                                    );
                    }
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75J99568", sSDate, sEDate);
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue("T");
                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(dk.Rows[i]["ISCHULIL"].ToString().Substring(0,4));
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["ISSEQ"].ToString())));

                        this.DAT02_EDICHULIL.SetValue(dk.Rows[i]["ISCHULIL"].ToString());
                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["ISIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["ISBONSUN"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["ISHWAMUL"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["ISHWAJU"].ToString());
                        this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["ISBLNO"].ToString());                        
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IPMSNSEQ"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(Set_Fill4(dk.Rows[i]["IPHSNSEQ"].ToString()));
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["ISBANIPIL"].ToString());

                        this.DAT02_EDITIME.SetValue(dk.Rows[i]["CMHITIM"].ToString());
                        this.DAT02_EDIJSGB.SetValue("9");
                        this.DAT02_EDIBANGB.SetValue("");
                        this.DAT02_EDIHMGB.SetValue("");
                        this.DAT02_EDIBUNHAL.SetValue(dk.Rows[i]["EDIBUNHAL"].ToString());
                        this.DAT02_EDISINNO.SetValue("");

                        this.DAT02_EDIIPQTY.SetValue(dk.Rows[i]["IPBSQTY"].ToString());

                        this.DAT02_EDINUQTY.SetValue("0");
                        this.DAT02_EDICHASU.SetValue("0");
                        this.DAT02_EDIPOJANG.SetValue("");
                        this.DAT02_EDICOUNT.SetValue("0");
                        this.DAT02_EDISAUP.SetValue(dk.Rows[i]["VNSAUPNO"].ToString());
                        this.DAT02_EDIJUSO.SetValue(dk.Rows[i]["VNJUSO"].ToString());

                        this.DAT02_EDICHNO2.SetValue(dk.Rows[i]["EDINO2"].ToString());
                        this.DAT02_EDICHNO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(dk.Rows[i]["EDINO3"].ToString())));

                        this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["IPSINOYY"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString())));
                        this.DAT02_EDICHTIME.SetValue(dk.Rows[i]["EDITIME"].ToString());

                        this.DAT02_EDIGJIL.SetValue("0");

                        this.DAT02_EDIBGIL.SetValue(dk.Rows[i]["ISJANGIL"].ToString());

                        if (dk.Rows[i]["ISJANGIL"].ToString() == "M")
                        {
                            this.DAT02_EDIBOIL.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["ISJANGIL"].ToString()) * 30, 0).ToString());
                        }
                        else
                        {
                            this.DAT02_EDIBOIL.SetValue(dk.Rows[i]["ISJANGIL"].ToString());
                        }
                        this.DAT02_EDIBOQTY.SetValue(dk.Rows[i]["ISCHQTY"].ToString());
                        this.DAT02_EDIBOAGMT.SetValue(dk.Rows[i]["ISGAMAMT"].ToString());

                        this.DAT02_EDIBOAMT.SetValue(dk.Rows[i]["ISISAMT"].ToString());
                        this.DAT02_EDIRATE.SetValue(dk.Rows[i]["ISRATE"].ToString());
                        this.DAT02_EDIISSEQ.SetValue(dk.Rows[i]["ISSEQ"].ToString());

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");

                        datas.Add(new object[] { 
                                                this.DAT02_EDIGJ.GetValue(),
                                                this.DAT02_EDINO1.GetValue(),
                                                this.DAT02_EDINO2.GetValue(),
                                                this.DAT02_EDINO3.GetValue(),
                                                this.DAT02_EDICHULIL.GetValue(),
                                                this.DAT02_EDIIPHANG.GetValue(),
                                                this.DAT02_EDIHANGCHA.GetValue(),
                                                this.DAT02_EDIHWAMUL.GetValue(),
                                                this.DAT02_EDIHWAJU.GetValue(),
                                                this.DAT02_EDIBLNO.GetValue(),
                                                this.DAT02_EDIJUKHA.GetValue(),
                                                this.DAT02_EDIBLMSN.GetValue(),
                                                this.DAT02_EDIBLHSN.GetValue(),
                                                this.DAT02_EDIDATE.GetValue(),
                                                this.DAT02_EDITIME.GetValue(),
                                                this.DAT02_EDIJSGB.GetValue(),
                                                this.DAT02_EDIBANGB.GetValue(),
                                                this.DAT02_EDIHMGB.GetValue(),
                                                this.DAT02_EDIBUNHAL.GetValue(),
                                                this.DAT02_EDISINNO.GetValue(),
                                                this.DAT02_EDIIPQTY.GetValue(),
                                                this.DAT02_EDINUQTY.GetValue(),
                                                this.DAT02_EDICHASU.GetValue(),
                                                this.DAT02_EDIPOJANG.GetValue(),
                                                this.DAT02_EDICOUNT.GetValue(),
                                                dk.Rows[i]["VSDESC1"].ToString(),
                                                dk.Rows[i]["HMDESC1"].ToString(),
                                                dk.Rows[i]["VNSANGHO"].ToString(),
                                                this.DAT02_EDISAUP.GetValue(),       
                                                this.DAT02_EDIJUSO.GetValue(),
                                                this.DAT02_EDICHNO2.GetValue(),
                                                this.DAT02_EDICHNO3.GetValue(),
                                                this.DAT02_EDIHMNO1.GetValue(),
                                                this.DAT02_EDIHMNO2.GetValue(),
                                                this.DAT02_EDICHTIME.GetValue(),
                                                this.DAT02_EDIGJIL.GetValue(),
                                                this.DAT02_EDIBGIL.GetValue(),
                                                this.DAT02_EDIBOIL.GetValue(),
                                                this.DAT02_EDIBOQTY.GetValue(),
                                                this.DAT02_EDIBOAGMT.GetValue(),
                                                this.DAT02_EDIBOAMT.GetValue(),
                                                this.DAT02_EDIRATE.GetValue(),
                                                this.DAT02_EDIISSEQ.GetValue(),
                                                this.DAT02_EDIRCVGB.GetValue(),
                                                this.DAT02_EDIMSG.GetValue(),
                                                TYUserInfo.EmpNo
                                            });
                    }

                    pgBar.Value = pgBar.Value + 1;
                    pgBar.Refresh();
                }

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_UT_75J9F570", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion
      
        #region  Description : 반출보고서 순번 생성
        private string UP_Get_ChulSeq(string sYear)
        {
            string sSEQCH = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75I9X561", sYear);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sSEQCH = Convert.ToString(Convert.ToInt16(dt.Rows[0]["SEQCH"].ToString()) + 1);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75I9Y562", sSEQCH, TYUserInfo.EmpNo, sYear);
                this.DbConnector.ExecuteTranQuery();
            }
            else
            {
                sSEQCH = "1";
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75I9Y563", sYear, sSEQCH, TYUserInfo.EmpNo);
                this.DbConnector.ExecuteTranQuery();
            }

            return sSEQCH;
        }
        #endregion

        #region  Description : 반출보고서 분할 구분 체크
        private string UP_Get_BunHalCheck(string sCSCUQTY, string sCSSINQTY, string sPRECHMTQTY, string sCHMTQTY)
        {
            string sBUNHALGN = string.Empty;

            double dCSCUQTY_JanQty = Convert.ToDouble(sCSCUQTY) - (Convert.ToDouble(sPRECHMTQTY) + Convert.ToDouble(sCHMTQTY));
            double dCSSINQTY_JanQty = Convert.ToDouble(sCSSINQTY) - (Convert.ToDouble(sPRECHMTQTY) + Convert.ToDouble(sCHMTQTY));

            if (dCSCUQTY_JanQty <= 0 || dCSSINQTY_JanQty <= 0)
            {
                if (Convert.ToInt16(this.DAT02_EDICHASU.GetValue().ToString()) == 1)
                {
                    sBUNHALGN = "A";
                }
                else
                {
                    sBUNHALGN = "L";
                }
            }
            else
            {
                sBUNHALGN = "P";
            }

            return sBUNHALGN;
        }
        #endregion

        #region  Description : 화재보험 반입보고서 DataBing 조회
        private DataTable UP_Get_DataBindingEDIIPGOSF (string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75IGB566", sDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion

        #region  Description : 화재보험 반출보고서 DataBing 조회
        private DataTable UP_Get_DataBindingEDICHULSF(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75IGE567", sDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion

        #region  Description : 화재보험 보험영수증 DataBing 조회
        private DataTable UP_Get_DataBindingEDIBOHUMF(string sSDate, string sEDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75JBD571", sSDate, sEDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion

        #region  Description :  화재보험  자료 전송
        private void UP_Set_WebServiceDataToTrans(string sSDate, string sEDate, string sDOC)
        {
            string result = string.Empty;

            DataTable dt = new DataTable();

            fiTransCnt = 0;

            KniaWebRef.InsWebServiceImplService svc = new KniaWebRef.InsWebServiceImplService();

            try
            {
                if (sDOC == "5HJ")
                {
                    dt = UP_Get_DataBindingEDIIPGOSF(sSDate);
                }
                else if (sDOC == "5HO")
                {
                    dt = UP_Get_DataBindingEDICHULSF(sSDate);
                }
                else
                {
                    dt = UP_Get_DataBindingEDIBOHUMF(sSDate, sEDate);
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (sDOC)
                        {
                            case "5HJ":
                                result = svc.saveBondedGoodsInInfo(TYEdiLayout.KniaEdiID, fsSHA256PASS, dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString(), UP_Set_BondedGoodsInInfoToXml(dt, i));
                                break;
                            case "5HO":
                                result = svc.saveBondedGoodsOutInfo(TYEdiLayout.KniaEdiID, fsSHA256PASS, dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString(), UP_Set_saveBondedGoodsOutInfoToXml(dt, i));
                                break;
                            case "5HI":
                                result = svc.saveBondedGoodsPremiumInfo(TYEdiLayout.KniaEdiID, fsSHA256PASS, dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString(), UP_Set_saveBondedGoodsPremiumInfoToXml(dt, i));
                                break;
                        }
                        if (result == "OK")
                        {
                            fiTransCnt += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowCustomMessage(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                svc.Dispose();
            }
        }
        #endregion

        #region  Description :  화재보험 수신 자료 변환
        private void UP_Get_WebServiceDataToConvert(string sSDate, string sEDate, string sDOC)
        {

            string sValue = string.Empty;
            string result = string.Empty;
            string sInNoStr = string.Empty;
            string sProId = string.Empty;

            string sStatusCode = string.Empty;
            string sSinno = string.Empty;
            string sMsg = string.Empty;

            UP_ProGressBarClear();

            DataTable dt = new DataTable();

            DocDatas.Clear();

            //JSON 포맷의 String (상태값 : “R”=”접수, “A”=”승인”, “D”=”기각”, “H”=”보류”)
            //ex) [{"상태":"A","번호":"016100531500000009","담당자":"BWT 물품","처리일시":"20150508", "기각사유":"20150508"}, {"상태":"A","번호":"016100531500000009","담당자":"BWT 물품","처리일시":"20150508", "기각사유":"20150508"}]

            KniaWebRef.InsWebServiceImplService svc = new KniaWebRef.InsWebServiceImplService();

            try
            {
                if (sDOC == "5HJ")
                {
                    dt = UP_Get_DataBindingEDIIPGOSF(sSDate);
                    sProId = "TY_P_UT_75I9M558";
                }
                else if (sDOC == "5HO")
                {
                    dt = UP_Get_DataBindingEDICHULSF(sSDate);
                    sProId = "TY_P_UT_75IDG565";
                }
                else
                {
                    dt = UP_Get_DataBindingEDIBOHUMF(sSDate, sEDate);
                    sProId = "TY_P_UT_75JG7577";
                }

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //if (sDOC == "5HJ")
                        //{
                        //    sInNoStr += dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString() + ",";
                        //}
                        //else if (sDOC == "5HO")
                        //{
                        //    sInNoStr += dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString() + ",";
                        //}
                        //else
                        //{
                        //    sInNoStr += dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString() + ",";
                        //}

                        sInNoStr += dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString() + ",";
                    }

                    sInNoStr = sInNoStr.Substring(0, sInNoStr.Length - 1);
                }

                string[] arrayInNoStr = sInNoStr.Split(',');

                if (arrayInNoStr.Length > 0)
                {
                    if (sDOC == "5HJ")
                    {
                        result = svc.getBondedGoodsInResult(TYEdiLayout.KniaEdiID, fsSHA256PASS, arrayInNoStr);
                    }
                    else if (sDOC == "5HO")
                    {
                        result = svc.getBondedGoodsOutResult(TYEdiLayout.KniaEdiID, fsSHA256PASS, arrayInNoStr);
                    }
                    else
                    {
                        result = svc.getBondedGoodsPremiumResult(TYEdiLayout.KniaEdiID, fsSHA256PASS, arrayInNoStr);
                    }
                }

                if (result.Length > 0)
                {
                    result = result.Replace("[", "");
                    result = result.Replace("]", "");

                    UP_Process_DocConvert(result);
                }

                if (DocDatas.Count > 0)
                {
                    pgBar.Maximum = DocDatas.Count;

                    this.DbConnector.CommandClear();
                    foreach (object[] data in DocDatas)
                    {
                        this.DbConnector.Attach(sProId, data);

                        pgBar.Value = pgBar.Value + 1;
                        pgBar.Refresh();                        
                    }

                    this.DbConnector.ExecuteNonQueryList();
                }

            }
            catch (Exception ex)
            {
                this.ShowCustomMessage(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                svc.Dispose();
            }
            
        }
        #endregion
        
        #region  Description :  Json 파일 변환 함수
        private void UP_Process_DocConvert(string result)
        {
            
            int iPoint = 0;
            string sValue = string.Empty;            
            string[] arrayInResult;

            string sStatusCode = string.Empty;
            string sSinno = string.Empty;
            string sMsg = string.Empty;

            fiTransCnt = 0;

            result = result.Replace("\"", "");

             string[] arrayResultMsg = result.Split('}');

             if (arrayResultMsg.Length > 0)
             {
                 for (int i = 0; i < arrayResultMsg.Length - 1; i++)
                 {
                     iPoint = arrayResultMsg[i].ToString().IndexOf("{");
                     arrayResultMsg[i] = arrayResultMsg[i].ToString().Substring(iPoint, arrayResultMsg[i].Length - iPoint);

                     arrayInResult = arrayResultMsg[i].Split(',');

                     sStatusCode = "";
                     sSinno = "";
                     sMsg = "";
                     
                         for (int j = 0; j < arrayInResult.Length; j++)
                         {
                             if (arrayInResult[j].ToString() != "")
                             {
                                 iPoint = arrayInResult[j].ToString().IndexOf(":");
                                 sValue = arrayInResult[j].ToString().Substring(iPoint + 1, arrayInResult[j].ToString().Length - (iPoint + 1));
                                 switch (j)
                                 {
                                     case 0:
                                         sStatusCode = sValue;
                                         break;
                                     case 1:
                                         sSinno = sValue;
                                         break;
                                     case 2:
                                         break;
                                     case 3:
                                         break;
                                     case 4:
                                         sMsg = sValue;
                                         break;
                                 }
                             }
                         }
                     

                     //접수처리 A: 승인 R:접수 D: 기각 H: 보류
                     if (sStatusCode == "A" || sStatusCode == "R")
                     {
                         DocDatas.Add(new object[] { "Y", sMsg, TYUserInfo.EmpNo, sSinno });
                     }
                     else if (sStatusCode == "E" ||  sStatusCode == "D" || sStatusCode == "H")
                     {
                         DocDatas.Add(new object[] { "E", sMsg, TYUserInfo.EmpNo, sSinno });
                     }

                     fiTransCnt += 1;
                 }
             }            
        }
        #endregion          

        #region  Description :  화재보험 Xml Text 생성
        private string UP_Set_saveBondedGoodsPremiumInfoToXml(DataTable dt, int i)
        {
            string strXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

            strXml += "<InsuranceReport>";
            //<!--문서형태구분 -->
            strXml += "<DocumentTypeCode>5HI</DocumentTypeCode>";
            //<!-- 신고번호(일련번호) -->
            strXml += "<DocumentReferenceID>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString() + "</DocumentReferenceID>";
            //<!-- 전자문서의 기능(9:원본, 35:재전송) -->
            strXml += "<DocumentFunctionTypeCode>" + dt.Rows[i]["EDIJSGB"].ToString() + "</DocumentFunctionTypeCode>";
            //<!-- 납부자상호-->
            strXml += "<PayerName>" + dt.Rows[i]["EDIHWAJUNM"].ToString() + "</PayerName>";
            //<!--보험계약자상호 -->
            strXml += "<InsuredName>" + dt.Rows[i]["EDIHWAJUNM"].ToString() + "</InsuredName>";
            //<!-- 보험계약자 사업자등록번호(주민등록) -->
            strXml += "<InsuredCode>" + dt.Rows[i]["EDISAUP"].ToString() + "</InsuredCode>";
            //<!-- 보험계약자주소 -->
            strXml += "<InsuredAddress>" + dt.Rows[i]["EDIJUSO"].ToString() + "</InsuredAddress>";
            //<!--창고명-->
            strXml += "<WarehouseName>(주)태영인더스트리</WarehouseName>";
            //<!-- 창고코드 -->
            strXml += "<WarehouseCode>" + dt.Rows[i]["EDINO1"].ToString() + "</WarehouseCode>";
            //<!-- 장치위치 -->
            strXml += "<ClerkName>UTT</ClerkName>";
            //<!-- 입항일자 -->
            strXml += "<DateArrive>" + dt.Rows[i]["EDIIPHANG"].ToString() + "</DateArrive>";
            //<!-- 계약일자 -->
            strXml += "<DateCarryIn>" + dt.Rows[i]["EDIDATE"].ToString() + "</DateCarryIn>";
            //<!-- 계약종료일자 -->
            strXml += "<DateCarryOut>" + dt.Rows[i]["EDICHULIL"].ToString() + "</DateCarryOut>";
            //<!-- 공제일수 -->
            strXml += "<CutOffDayw>0</CutOffDayw>";
            //<!-- 보관일수 -->
            strXml += "<DateIssue>" + dt.Rows[i]["EDIBOIL"].ToString() + "</DateIssue>";
            //<!-- 계약일시 -->
            strXml += "<CarryTimeIn>" + dt.Rows[i]["EDIDATE"].ToString() + dt.Rows[i]["EDITIME"].ToString() + "</CarryTimeIn>";
            //<!-- 계약종료일시 -->
            strXml += "<CarryTimeOut>" + dt.Rows[i]["EDICHULIL"].ToString() + dt.Rows[i]["EDICHTIME"].ToString() + "</CarryTimeOut>";
            //<!-- 자료상태구분 -->
            //strXml += "<DataState>I</DataState>";
            //<!-- 보험료증감여부 -->
            strXml += "<InsGubun>N</InsGubun>";
            //<!-- 선명 -->	
            strXml += "<ShipNo>" + dt.Rows[i]["EDIBONSUNNM"].ToString() + "</ShipNo>";
            //<!-- 분할반출구분 -->
            strXml += "<PartialType>" + dt.Rows[i]["EDIBUNHAL"].ToString() + "</PartialType>";
            //<!-- 반입수량 -->	
            strXml += "<InQty>" + dt.Rows[i]["EDIIPQTY"].ToString() + "</InQty>";
            //<!-- 보관증권수 -->
            strXml += "<DepositInsNo>0</DepositInsNo>";
            //<!--B/L NO -->
            strXml += "<BlNo>" + dt.Rows[i]["EDIBLNO"].ToString() + "</BlNo>";
            //<!-- 품명 -->
            strXml += "<GoodsName>" + dt.Rows[i]["EDIHWAMULNM"].ToString() + "</GoodsName>";
            //<!-- 보험일 -->
            strXml += "<Term>" + dt.Rows[i]["EDIBOIL"].ToString() + "</Term>";
            //<!-- 포장단위 -->
            strXml += "<PackageCode>CT</PackageCode>";
            //<!-- 수량 -->
            strXml += "<PackageNo>" + dt.Rows[i]["EDIBOQTY"].ToString() + "</PackageNo>";
            //<!-- 보험가입금액 -->
            strXml += "<AdmissionFee>" + dt.Rows[i]["EDIBOAGMT"].ToString() + "</AdmissionFee>";
            //<!-- 보험료 -->
            strXml += "<Premium>" + dt.Rows[i]["EDIBOAMT"].ToString() + "</Premium>";
            //<!-- 요율 -->	
            strXml += "<PremiumRate>" + dt.Rows[i]["EDIRATE"].ToString() + "</PremiumRate>";
            //<!-- 화물관리번호 -->
            strXml += "<Msrn>" + dt.Rows[i]["EDIJUKHA"].ToString() + dt.Rows[i]["EDIBLMSN"].ToString() + dt.Rows[i]["EDIBLHSN"].ToString() + "</Msrn>";
            //<!-- 반입신고번호 -->	
            strXml += "<InNo>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDIHMNO1"].ToString() + String.Format("{0:D6}", Convert.ToInt64(dt.Rows[i]["EDIHMNO2"].ToString())) + "</InNo>";
            //<!-- 전송일시 -->
            strXml += "<SendTime>" + dt.Rows[i]["EDICURRENTTIME"].ToString() + "</SendTime>";
            //<!-- 반출신고번호 -->
            if (Convert.ToInt16(dt.Rows[i]["EDICHNO2"].ToString().Substring(0, 4)) > 2017)
            {
                strXml += "<OutNo>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDICHNO2"].ToString() + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[i]["EDICHNO3"].ToString())) + "</OutNo>";
            }
            else
            {
                strXml += "<OutNo>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDICHNO2"].ToString().Substring(2,2) + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[i]["EDICHNO3"].ToString())) + "</OutNo>";
            }

            strXml += "</InsuranceReport>";

            return strXml;
        }

        private string UP_Set_saveBondedGoodsOutInfoToXml(DataTable dt, int i)
        {
            string strXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

            strXml += "<CarryOutReport>";
            //<!--문서형태구분 -->
            strXml += "<TypeCode>5HO</TypeCode>";
            //	<!-- 제출번호(세관등록번호(8) + 반입일자(8)) -->
            strXml += "<referenceID>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDIDATE"].ToString() + "</referenceID>";
            //<!-- 전자문서의 기능(9:원본, 35:재전송) -->
            strXml += "<FunctionTypeCode>" + dt.Rows[i]["EDIJSGB"].ToString() + "</FunctionTypeCode>";
            //<!-- 계약종료일자 -->
            strXml += "<CancelDate>" + dt.Rows[i]["EDIDATE"].ToString() + "</CancelDate>";
            //<!--창고명-->
            strXml += "<WarehouseName>(주)태영인더스트리</WarehouseName>";
            //<!-- 창고코드 -->
            strXml += "<WarehouseCode>" + dt.Rows[i]["EDINO1"].ToString() + "</WarehouseCode>";
            //<!-- 화물관리번호 -->
            strXml += "<Msrn>" + dt.Rows[i]["EDIJUKHA"].ToString() + dt.Rows[i]["EDIBLMSN"].ToString() + dt.Rows[i]["EDIBLHSN"].ToString() + "</Msrn>";
            //<!--B/L NO -->
            strXml += "<BlNo>" + dt.Rows[i]["EDIBLNO"].ToString() + "</BlNo>";
            //<!--SERIAL NO -->
            strXml += "<SerialNo>" + dt.Rows[i]["EDIDATE"].ToString().Substring(2, 2) + dt.Rows[i]["EDINO3"].ToString() + "</SerialNo>";
            //<!-- 장치위치 -->
            strXml += "<ClerkName>UTT</ClerkName>";
            //<!-- 화주명 -->
            strXml += "<OwnerName>" + dt.Rows[i]["EDIHWAJUNM"].ToString() + "</OwnerName>";
            //<!-- 선명 -->
            strXml += "<ShipNo>" + dt.Rows[i]["EDIHANGCHANM"].ToString() + "</ShipNo>";
            //<!-- 포장단위 -->
            strXml += "<PackageCode>VL</PackageCode>";
            //<!-- 포장갯수 -->
            strXml += "<PackageNo>" + dt.Rows[i]["EDICHCNT"].ToString() + "</PackageNo>";
            //<!-- 중량단위 -->
            strXml += "<WeightUnit>KG</WeightUnit>";
            //<!-- 중량 -->
            strXml += "<Weight>" + dt.Rows[i]["EDICHQTY"].ToString() + "</Weight>";
            //<!-- 분할반출차수 -->
            strXml += "<PartialExportNo>" + dt.Rows[i]["EDICHASU"].ToString() + "</PartialExportNo>";
            //<!-- 자료상태구분 -->
            //strXml += "<DataState>I</DataState>";
            //<!-- 화물반출유형 -->
            strXml += "<CarryOutType>" + dt.Rows[i]["EDIBANGB"].ToString() + "</CarryOutType>";
            //<!-- 분할반출구분 -->
            strXml += "<PartialType>" + dt.Rows[i]["EDIBUNHAL"].ToString() + "</PartialType>";
            //<!-- 반출기간연장구분 -->
            //strXml += "<DelayType>1</DelayType>";
            //<!-- 화물관리번호 변경 유무 -->
            strXml += "<OmsrnGubn>0</OmsrnGubn>";
            //<!-- 품명 -->
            strXml += "<GoodsName>" + dt.Rows[i]["EDIHWAMULNM"].ToString() + "</GoodsName>";
            //<!-- 전송일시 -->
            strXml += "<BondSendDt>" + dt.Rows[i]["EDICHTIME"].ToString() + "</BondSendDt>";
            //<!-- 입항일자 -->	
            strXml += "<DateArrive>" + dt.Rows[i]["EDIIPHANG"].ToString() + "</DateArrive>";
            //<!-- 반입일자 -->
            strXml += "<DateCarryIn>" + dt.Rows[i]["EDIPDATE"].ToString() + "</DateCarryIn>";
            //<!-- 반입일시 -->
            strXml += "<CarryTimeIn>" + dt.Rows[i]["EDIPDATE"].ToString() + dt.Rows[i]["EDIPTIME"].ToString() + "</CarryTimeIn>";
            //<!-- 반출일자 -->
            strXml += "<DateCarryOut>" + dt.Rows[i]["EDIDATE"].ToString() + "</DateCarryOut>";
            //<!-- 반출일시 -->
            strXml += "<CarryTimeOut>" + dt.Rows[i]["EDIDATE"].ToString() + dt.Rows[i]["EDITIME"].ToString() + "</CarryTimeOut>";
            //<!-- 반입신고번호 -->	
            strXml += "<InNo>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDIHMNO1"].ToString() + String.Format("{0:D6}", Convert.ToInt64(dt.Rows[i]["EDIHMNO2"].ToString())) + "</InNo>";
            //<!-- 반출신고번호 -->
            strXml += "<OutNo>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDINO2"].ToString() + dt.Rows[i]["EDINO3"].ToString() + "</OutNo>";
            //<!-- 반출근거번호 -->
            strXml += "<BaseNo>" + dt.Rows[i]["EDISINNO"].ToString() + "</BaseNo>";
            //<!-- 반입수량 -->
            strXml += "<DelayNo>" + dt.Rows[i]["EDIIPQTY"].ToString() + "</DelayNo>";
            //<!-- 원화물관리번호 -->
            //strXml += "<Omsrn>" + dt.Rows[i]["EDIIPQTY"].ToString() + "</Omsrn>";
            strXml += "</CarryOutReport>";

            return strXml;
        }

        private string UP_Set_BondedGoodsInInfoToXml(DataTable dt, int i)
        {
            string strXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

            strXml += "<CarryInReport>";
            //<!--문서형태구분 -->
            strXml += "<TypeCode>5HJ</TypeCode>";
            //	<!-- 제출번호(세관등록번호(8) + 반입일자(8)) -->
            strXml += "<referenceID>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDIDATE"].ToString() + "</referenceID>";
            //<!-- 전자문서의 기능(9:원본, 35:재전송) -->
            strXml += "<FunctionTypeCode>" + dt.Rows[i]["EDIJSGB"].ToString() + "</FunctionTypeCode>";
            //<!-- 계약일자 -->
            strXml += "<SubscriptionDate>" + dt.Rows[i]["EDIDATE"].ToString() + "</SubscriptionDate>";
            //<!--창고명-->
            strXml += "<WarehouseName>(주)태영인더스트리</WarehouseName>";
            //<!-- 창고코드 -->
            strXml += "<WarehouseCode>" + dt.Rows[i]["EDINO1"].ToString() + "</WarehouseCode>";
            //<!-- 화물관리번호 -->
            strXml += "<Msrn>" + dt.Rows[i]["EDIJUKHA"].ToString() + dt.Rows[i]["EDIBLMSN"].ToString() + dt.Rows[i]["EDIBLHSN"].ToString() + "</Msrn>";
            //<!--B/L NO -->
            strXml += "<BlNo>" + dt.Rows[i]["EDIBLNO"].ToString() + "</BlNo>";
            //<!--SERIAL NO -->
            strXml += "<SerialNo>" + dt.Rows[i]["EDIHMNO1"].ToString() + dt.Rows[i]["EDINO3"].ToString() + "</SerialNo>";
            //<!-- 장치위치 -->
            strXml += "<ClerkName>UTT</ClerkName>";
            //<!-- 화주명 -->
            strXml += "<OwnerName>" + dt.Rows[i]["EDIHWAJUNM"].ToString() + "</OwnerName>";
            //<!-- 선명 -->
            strXml += "<ShipNo>" + dt.Rows[i]["EDIHANGCHANM"].ToString() + "</ShipNo>";
            //<!-- 반입사고 유형 -->
            strXml += " <AccidentType>OK</AccidentType>";
            //<!-- 포장단위 -->
            strXml += "<PackageCode>" + dt.Rows[i]["EDIPOJANG"].ToString() + "</PackageCode>";
            //<!-- 포장갯수 -->
            strXml += "<PackageNo>" + dt.Rows[i]["EDICOUNT"].ToString() + "</PackageNo>";
            //<!-- 중량단위 -->
            strXml += "<WeightUnit>KG</WeightUnit>";
            //<!-- 중량 -->
            strXml += "<Weight>" + dt.Rows[i]["EDIIPQTY"].ToString() + "</Weight>";
            //<!-- 반입사고 갯수 -->
            //strXml += "<AccidentPkgNo>0</AccidentPkgNo>";
            //<!-- 반입사고 중량 -->
            //strXml += "<AccidentWeight>0</AccidentWeight>";
            //<!-- 분할반입차수 -->
            //strXml += "<PartialEntryNo>2</PartialEntryNo>";
            //<!-- 자료상태구분 -->
            //strXml += "<DataState>I</DataState>";
            //<!-- 화물반입유형 -->
            strXml += "<CarryInType>20</CarryInType>";
            //<!-- 분할반입구분 -->
            strXml += "<PartialType>A</PartialType>";
            //<!-- 용도구분 -->
            //strXml += "<ProcedureType>P</ProcedureType>";
            //<!-- 품명 -->
            strXml += "<GoodsName>" + dt.Rows[i]["EDIHWAMULNM"].ToString() + "</GoodsName>";
            //<!-- 전송일시 -->
            strXml += "<BondSendDt>" + dt.Rows[i]["EDICHTIME"].ToString() + "</BondSendDt>";
            //<!-- 입항일자 -->
            strXml += "<DateArrive>" + dt.Rows[i]["EDIIPHANG"].ToString() + "</DateArrive>";
            //<!-- 반입일자 -->	
            strXml += "<DateCarryIn>" + dt.Rows[i]["EDIBANIL"].ToString() + "</DateCarryIn>";
            //<!-- 반입일시 -->
            strXml += "<CarryTimeIn>" + dt.Rows[i]["EDIBANIL"].ToString() + dt.Rows[i]["EDITIME"].ToString() + "</CarryTimeIn>";
            //<!-- 반입신고번호 -->
            strXml += "<InNo>" + dt.Rows[i]["EDINO1"].ToString() + dt.Rows[i]["EDIHMNO1"].ToString() + dt.Rows[i]["EDINO3"].ToString() + "</InNo>";
            //<!-- 반입근거번호 -->
            strXml += "<BaseNo>" + dt.Rows[i]["EDISINNO"].ToString() + "</BaseNo>";
            strXml += "</CarryInReport>";

            return strXml;
        }
        #endregion

        #region  Description : CheckedChanged 이벤트
        private void RDB01_CHK1_CheckedChanged(object sender, EventArgs e)
        {
            DTP01_EDATE.Enabled = false;
        }

        private void RDB01_CHK2_CheckedChanged(object sender, EventArgs e)
        {
            DTP01_EDATE.Enabled = false;
        }

        private void RDB01_CHK3_CheckedChanged(object sender, EventArgs e)
        {
            DTP01_EDATE.Enabled = true;

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-26"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-25"));

        }
        #endregion

        #region  Description : UP_ProGressBarClear 이벤트
        private void UP_ProGressBarClear()
        {
            pgBar.Maximum = 0;
            pgBar.Minimum = 0;
        }
        #endregion


        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion










    }
}
