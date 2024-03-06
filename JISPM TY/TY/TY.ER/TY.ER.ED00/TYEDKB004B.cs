using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;


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

    public partial class TYEDKB004B : TYBase
    {
        


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
           private TYData DAT02_EDICDATE;
           private TYData DAT02_EDIIPSINNO;
         #endregion

         private DataTable fsSendMigTable;
         private DataTable fsSendMastTable;         

        #region  Description : 폼 로드 이벤트
        public TYEDKB004B()
        {
            InitializeComponent();

            //this.SetPopupStyle();

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
            #endregion

            
        }

        private void TYEDKB004B_Load(object sender, System.EventArgs e)
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
            #endregion

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_BTN_MAST.ProcessCheck += new TButton.CheckHandler(BTN61_BTN_MAST_ProcessCheck);
            this.BTN61_BTN_TRANS.ProcessCheck += new TButton.CheckHandler(BTN61_BTN_TRANS_ProcessCheck);
            
            UP_SetLockCheck();

            DTP01_EDATE.Visible = false;
            label1.Visible = false;

            //UTT, SILO 메뉴 결정
            if (CBO01_EDIGJ.GetValue().ToString() == "S")
            {
                RDB01_CHK4.Visible = false;
                RDB01_CHK5.Visible = false;
                RDB01_CHK7.Visible = false;
                RDB01_CHK8.Visible = false;
                RDB01_CHK9.Visible = false;
                RDB01_CHK10.Visible = true;
                RDB01_CHK11.Visible = false;
                

            }
            else
            {
                RDB01_CHK4.Visible = true;
                RDB01_CHK5.Visible = true;
                RDB01_CHK7.Visible = true;
                RDB01_CHK8.Visible = true;
                RDB01_CHK9.Visible = true;
                RDB01_CHK10.Visible = false;
                RDB01_CHK11.Visible = true;
                
            }

            fsSendMigTable = UP_SetDataTable();
            fsSendMastTable = UP_SetMasterDataTable();

            this.UP_DirFileDelete_KCSAPI4();

            RDB01_CHK1.Checked = true;
            RDB01_CHK8.Checked = false;

            pgBar.Minimum = 0;
            pgBar.Maximum = 0;

            this.DTP01_SDATE.SetValue(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));            

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {           

            fsSendMastTable.Clear();
            fsSendMigTable.Clear();

            string sSdate = DTP01_SDATE.GetString().ToString();
            string sEdate = DTP01_EDATE.GetString().ToString();

            pgBar.Minimum = 0;
            pgBar.Maximum = 0;            
            
            if (RDB01_CHK1.Checked == true)  //반입보고서
            {
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Get_DataIPGOF_UTT(sSdate);                    
                }
                else
                {
                    UP_Get_DataIPGOF_SILO(sSdate, "10");
                }

                UP_Set_MigDataIPGOF_Common(sSdate);
            }
            else if (RDB01_CHK2.Checked == true)  //반출보고서
            {
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Get_DataCHULF_UTT(sSdate);                    
                }
                else
                {
                    UP_Get_DataCHULF_SILO(sSdate);
                }

                UP_Set_MigDataCHULF_Common(sSdate);
            }
            else if (RDB01_CHK3.Checked == true)  //체화통보
            {
            }
            else if (RDB01_CHK4.Checked == true)  //내국화물반입
            {

                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Get_DataHAIPGOF_UTT(sSdate, sEdate);                    
                }
                else
                {
                    //UP_Get_DataHAIPGOF_SILO(sSdate, sEdate);                    
                }

                UP_Set_MigDataHAIPGOF_UTT(sSdate, sEdate);
            }
            else if (RDB01_CHK5.Checked == true) //내국화물반출
            {

                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Get_DataHBCHULF_UTT(sSdate, sEdate);                    
                }
                else
                {
                    //UP_Get_DataHBCHULF_SILO(sSdate, sEdate);
                }

                UP_Set_MigDataHBCHULF_UTT(sSdate, sEdate);
            }
            else if (RDB01_CHK6.Checked == true)  //반출입정정신고
            {
                UP_Set_MigDataREIPCHF_Common(sSdate);
            }
            else if (RDB01_CHK7.Checked == true)  //bl반출신고
            {
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Get_DataBLBunHal_UTT(sSdate);
                    UP_Set_MigDataCHULF_Common(sSdate);
                }
            }
            else if (RDB01_CHK8.Checked == true)  //보세운송+반송화물
            {
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Get_DataBoSaeCHULF_UTT(sSdate);
                    UP_Set_MigDataCHULF_Common(sSdate);
                }
            }
            else if (RDB01_CHK9.Checked == true)  //반출기간연장
            {
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Set_MigDataEXTENDF_UTT(sSdate);
                }
            }
            else if (RDB01_CHK10.Checked == true)  //BL분할 반입(silo)
            {
                if (this.CBO01_EDIGJ.GetValue().ToString() == "S")
                {
                   UP_Get_DataIPGOF_SILO(sSdate, "23");
                   UP_Set_MigDataIPGOF_Common(sSdate);
                }
            }
            else if (RDB01_CHK11.Checked == true)  //반출통고목록보고서
            {
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    UP_Set_MigDataCHNOTEMF_Common(sSdate);
                }
            }

            this.ShowMessage("TY_M_GB_26E30875");
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sTitleMsg = string.Empty;
            string sTitle = string.Empty;
            string sCnt = string.Empty;
            string sSdate = DTP01_SDATE.GetString().ToString();
            string sEdate = DTP01_EDATE.GetString().ToString();

            int iCnt = 0;

            this.UP_DirFileDelete_KCSAPI4();

            if (RDB01_CHK1.Checked == true)
            {
                sTitle = "반입보고서";
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")  //UTT
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74PIB378", sSdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }
                    sCnt = iCnt.ToString();
                }
                else
                {
                    //SILO
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_88RDY639", sSdate, "10");
                    DataTable dk = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }
                    sCnt = iCnt.ToString();
                }
            }
            else if (RDB01_CHK2.Checked == true)
            {
                sTitle = "반출보고서";
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")  //UTT
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_758D5409", sSdate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString());
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_A4A9X234", this.CBO01_EDIGJ.GetValue().ToString(), sSdate, sSdate, "");
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["EDMRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }

                    sCnt = iCnt.ToString();
                }
                else
                {
                    //SILO
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_88RHS643", sSdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }
                    sCnt = iCnt.ToString();
                }

            }
            else if (RDB01_CHK3.Checked == true)
            {
                sTitle = "체화예정보고서";
            }
            else if (RDB01_CHK4.Checked == true)
            {
                sTitle = "내국반입신고서";
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T") //UTT
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_75A8Z423", this.CBO01_EDIGJ.GetValue().ToString(), sSdate, sEdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }
                    sCnt = iCnt.ToString();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_A3Q9X157", sSdate, sEdate, "43");
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }
                    sCnt = iCnt.ToString();
                }
            }
            else if (RDB01_CHK5.Checked == true)
            {
                sTitle = "내국반출신고서";
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T") //UTT
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_75AGV435", sSdate, sSdate, sEdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y" && Convert.ToDouble(dk.Rows[i]["NOWTOTALQTY"].ToString()) >= Convert.ToDouble(dk.Rows[i]["CSCUQTY"].ToString()))
                        {
                            iCnt += 1;
                        }
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_75AGV435", sSdate, sSdate, sEdate);
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                        {
                            iCnt += 1;
                        }
                    }

                    sCnt = iCnt.ToString();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_A3QFP160", sSdate, sSdate, sEdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y" )
                        {
                            iCnt += 1;
                        }
                    }
                    sCnt = iCnt.ToString();
                }
            }
            else if (RDB01_CHK6.Checked == true)
            {
                sTitle = "반출입정정신고서";

                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_75BI3455", this.CBO01_EDIGJ.GetValue().ToString(), sSdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    sCnt = dk.Rows.Count.ToString();
                }
            }
            else if (RDB01_CHK7.Checked == true)
            {
                sTitle = "BL반출신고";

                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_75BBH449", sSdate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString(), sSdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    sCnt = dk.Rows.Count.ToString();
                }
            }
            else if (RDB01_CHK8.Checked == true)
            {
                sTitle = "보세운송+반송화물";
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_75BBF448", sSdate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString());
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    sCnt = dk.Rows.Count.ToString();
                }
            }
            else if (RDB01_CHK9.Checked == true)
            {
                sTitle = "반출기간연장신청서";
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_8688W192", this.CBO01_EDIGJ.GetValue().ToString(), sSdate);                    
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    sCnt = dk.Rows.Count.ToString();
                }
            }
            else if (RDB01_CHK11.Checked == true)  //반출통고목록보고서
            {
                sTitle = "반출통고목록보고서";
                if (this.CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_A6192598", sSdate, sSdate);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    sCnt = dk.Rows.Count.ToString();
                }
            }


            sTitleMsg = sTitle + ": " + sCnt + "건이 있습니다 자료 생성하시겠습니까?";

            if (sCnt == "0")
            {
                this.ShowCustomMessage("생성할 자료가 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }
            else
            {
                if (!this.ShowCustomMessage(sTitleMsg, "확인", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region  Description : 마스타 버튼 이벤트
        private void BTN61_BTN_MAST_Click(object sender, EventArgs e)
        {
            Int16 iRowIndex = 0;
            Int16 iLenRevUNH;
            Int16 iLenRevBGM;
            Int16 iLenRevUNT;

            Int16 iMigCnt = 0;

            string sData = string.Empty;
            string sSendStr = string.Empty;
                 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73VGC181");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                TYEdiLayout.MASTHEAD2 = dt.Rows[0]["EDNSTMASTID"].ToString();
                TYEdiLayout.MASTHEAD3 = dt.Rows[0]["EDNSDMASTID"].ToString();
                TYEdiLayout.MASTHEAD_DATE = dt.Rows[0]["SYSDATE"].ToString();
                TYEdiLayout.MASTHEAD_TIME1 = dt.Rows[0]["SYSTIME"].ToString().Substring(0,2);
                TYEdiLayout.MASTHEAD_TIME2 = dt.Rows[0]["SYSTIME"].ToString().Substring(2,2);
            }         

            TYEdiLayout.MASTHEAD = TYEdiLayout.MASTHEAD1 + TYEdiLayout.MASTHEAD2 + TYEdiLayout.MASTHEAD3 +
                                   TYEdiLayout.MASTHEAD_DATE + TYEdiLayout.MASTHEAD4 + TYEdiLayout.MASTHEAD_TIME1 +
                                   TYEdiLayout.MASTHEAD_TIME2 + TYEdiLayout.MASTHEAD5;

            iRowIndex += 1;
            UP_SetDataTable_MastRowAdd(iRowIndex, TYEdiLayout.MASTHEAD);

            for (int i = 0; i < fsSendMigTable.Rows.Count; i++)
            {
                sData = fsSendMigTable.Rows[i][0].ToString().Replace("#", "");

                iLenRevUNH = Convert.ToInt16(sData.IndexOf("UNH", 0).ToString());
                iLenRevBGM = Convert.ToInt16(sData.IndexOf("BGM", iLenRevUNH + 1).ToString());
                iLenRevUNT = Convert.ToInt16(sData.IndexOf("UNT", iLenRevBGM + 1).ToString());

                //UNH
                iRowIndex += 1;
                sSendStr = sData.Substring(iLenRevUNH, iLenRevBGM - iLenRevUNH);
                UP_SetDataTable_MastRowAdd(iRowIndex, sSendStr);

                //BGM
                iRowIndex += 1;
                sSendStr = sData.Substring(iLenRevBGM, iLenRevUNT - iLenRevBGM);
                UP_SetDataTable_MastRowAdd(iRowIndex, sSendStr);

                //UNT
                iRowIndex += 1;
                sSendStr = sData.Substring(iLenRevUNT, sData.Length - iLenRevUNT);                
                UP_SetDataTable_MastRowAdd(iRowIndex, sSendStr);      
          
                iMigCnt += 1;
            }

            iRowIndex += 1;
            TYEdiLayout.MASTTAIL_CNT = Set_Fill3(Get_Numeric(iMigCnt.ToString()));
            TYEdiLayout.MASTTAIL = TYEdiLayout.MASTTAIL1 + TYEdiLayout.MASTTAIL_CNT + TYEdiLayout.MASTTAIL2;

            UP_SetDataTable_MastRowAdd(iRowIndex, TYEdiLayout.MASTTAIL);

            this.ShowMessage("TY_M_UT_74RDZ393");
        }

        private void BTN61_BTN_MAST_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (fsSendMigTable.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_UT_74OAE363") )
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 전송 버튼 이벤트
        private void BTN61_BTN_TRANS_Click(object sender, EventArgs e)
        {
            //string sFileName = "upload.xxx";
            //string sSendStr = string.Empty;

            ////기존 파일 삭제
            //System.IO.File.Delete(Get_WinmdatePath() + "\\out\\" + sFileName);
            //System.IO.File.Delete(Get_WinmdatePath() + "\\outsave\\" + sFileName);

            //if (fsSendMastTable.Rows.Count > 0)
            //{
            //    StreamWriter sw = new StreamWriter(Get_WinmdatePath() + "\\out\\" + sFileName, false, Encoding.Default);

            //    for (int i = 0; i < fsSendMastTable.Rows.Count; i++)
            //    {
            //        sSendStr = sSendStr + fsSendMastTable.Rows[i]["USRFLD"].ToString();
            //    }
            //    sw.WriteLine(sSendStr);
            //    sw.Close();
            //}

            //if (System.IO.File.Exists(Get_WinmdatePath() + "\\out\\" + sFileName))
            //{
            //    System.IO.File.Copy(Get_WinmdatePath() + "\\out\\" + sFileName, Get_WinmdatePath() + "\\outSAVE\\" + sFileName);
            //}

            //UP_WinmateSend();

            //fsSendMastTable.Clear();
            //fsSendMigTable.Clear();            

            try
            {
                //this.UP_KCSAPI4_DataToTrans();

                this.UP_KCSAPIModulCall();


            }
            catch (Exception ex)
            {
                this.ShowCustomMessage(ex.Message , "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            finally
            {
                //전송한 파일 이동
                UP_DirFileMove_KCSAPI4();


                this.ShowMessage("TY_M_UT_74RE0394");
            }            
        }

        private void BTN61_BTN_TRANS_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            //KCSAPI4 설치 여부 판단
            if (!System.IO.Directory.Exists(ConstKCSAPIPath) || !System.IO.File.Exists(ConstKCSAPIPath + "\\KCSAPI4.dll"))
            {
                this.ShowCustomMessage("KCSAPI4 모듈이 설치되어 있지않습니다! 전산실에 문의하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //전송파일 존재 확인
            string[] _FileList = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\upload\\");
            if (_FileList.Length <= 0)
            {
                this.ShowMessage("TY_M_UT_74RDS392");
                e.Successed = false;
                return;
            }

            //if (fsSendMastTable.Rows.Count <= 0)
            //{
            //    this.ShowMessage("TY_M_UT_74RDS392");
            //    e.Successed = false;
            //    return;
            //}

            if (!this.ShowMessage("TY_M_UT_74OAE364"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 반입보고서 자료 생성 UTT
        private void UP_Get_DataIPGOF_UTT(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74OD2366", sDate, this.CBO01_EDIGJ.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_74PI3377", this.CBO01_EDIGJ.GetValue().ToString(), sDate,
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
            this.DbConnector.Attach("TY_P_UT_74PIB378", sDate);
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
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString())));

                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IPBLNOSEQ"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(dk.Rows[i]["IPHBLNOSEQ"].ToString());
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["CMBOGODAT"].ToString());
                        
                        //2018.07.24 실제전송하는 시간으로 수정( 박동근 차장 요청)                        
                        this.DAT02_EDITIME.SetValue(dk.Rows[i]["CMHITIM"].ToString());
                        //this.DAT02_EDITIME.SetValue(DateTime.Now.ToString("CMHITIM").ToString());

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

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),     
                                            this.DAT02_EDIJUKHA.GetValue(),  
                                            this.DAT02_EDIBLMSN.GetValue(),  
                                            this.DAT02_EDIBLHSN.GetValue(),  
                                            this.DAT02_EDIDATE.GetValue(),   
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),    
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
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            this.DAT02_EDIHWAJU.GetValue(),  
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
                        this.DbConnector.Attach("TY_P_UT_74Q9F379", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }           

        }
        #endregion

        #region  Description : 반입보고서 Mig 자료 생성(UTT,SILO 공통)
        private void UP_Set_MigDataIPGOF_Common(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_72RDC787",  this.CBO01_EDIGJ.GetValue().ToString(), sDate, "");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        TYEdiLayout.BGM_02011 = dt.Rows[i]["EDINO1"].ToString();
                        TYEdiLayout.BGM_02012 = dt.Rows[i]["EDINO2"].ToString().Substring(2, 2);
                        TYEdiLayout.UNH_0101 = dt.Rows[i]["EDINO3"].ToString();

                        TYEdiLayout.BGM_02013 = TYEdiLayout.UNH_0101;
                        TYEdiLayout.UNT_0201 = TYEdiLayout.UNH_0101;
                        TYEdiLayout.BGM_0202 = string.Format("{0,-2:G}", dt.Rows[i]["EDIJSGB"].ToString().Trim());              //전송구분
                        TYEdiLayout.DTM_01021 = dt.Rows[i]["EDIDATE"].ToString();        //반입일자

                        TYEdiLayout.DTM_01022 = dt.Rows[i]["EDITIME"].ToString();          //반입시간
                        TYEdiLayout.GIS_0101 = string.Format("{0,-2:G}", dt.Rows[i]["EDIHMGB"].ToString().Trim());              //화물종류

                        TYEdiLayout.GIS2_0101 = string.Format("{0,-2:G}", dt.Rows[i]["EDIBANGB"].ToString().Trim());              //반입유형
                        TYEdiLayout.GIS5_0101 = dt.Rows[i]["EDIBUNHAL"].ToString();              //분할반입구분

                        TYEdiLayout.RFF_0102 = dt.Rows[i]["EDIJUKHA"].ToString();       //적하목록
                        TYEdiLayout.RFF_0103 = string.Format("{0,-4:G}", dt.Rows[i]["EDIBLMSN"].ToString().Trim());             //MSN

                        if (Convert.ToInt16(Get_Numeric(dt.Rows[i]["EDIBLHSN"].ToString().Trim())) > 0)
                        {
                            TYEdiLayout.RFF_0104 =  Set_Fill4(dt.Rows[i]["EDIBLHSN"].ToString().Trim());
                        }
                        else
                        {
                            TYEdiLayout.RFF_0104 = "    ";
                        }

                        if (dt.Rows[i]["EDISINNO"].ToString().Trim() != "")
                        {
                            TYEdiLayout.RFF2_0101 = string.Format("{0,-20:G}", dt.Rows[i]["EDISINNO"].ToString().Trim());
                        }
                        else
                        {
                            TYEdiLayout.RFF2_0101 = "                    ";
                        }

                        if (TYEdiLayout.RFF2_0101.Trim() != "")
                        {
                            TYEdiLayout.BANIP_AREA = TYEdiLayout.RFF2_PRN1 + TYEdiLayout.RFF2_PRN2 + TYEdiLayout.RFF2_PRN3 + TYEdiLayout.RFF2_PRN4 + TYEdiLayout.RFF2_0101 + TYEdiLayout.RFF2_PRN5;
                        }

                        TYEdiLayout.GID_0101 = string.Format("{0,-10:G}", dt.Rows[i]["EDICOUNT"].ToString().Trim());        //반입개수
                        TYEdiLayout.GID_0102 = string.Format("{0,-2:G}", dt.Rows[i]["EDIPOJANG"].ToString().Trim());             //포장종류
                        TYEdiLayout.FTX_0201 = "OKY";

                        TYEdiLayout.MEA_0202 = string.Format("{0,-16:G}", string.Format("{0:###0.000}", double.Parse(dt.Rows[i]["EDIIPQTY"].ToString())));

                        UP_Set_MigData_IPGOAdd();

                        UP_Set_XmlData_IPGOAdd();
                    }
                }
            }
        }
        #endregion

        #region  Description : 반입 미그 데이타 조합
        private void UP_Set_MigData_IPGOAdd()
        {
            int iCnt = 0;

            UP_Set_MigDataMix_CUSCAR();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.UNH_PRN + TYEdiLayout.BGM_PRN + TYEdiLayout.DTM_PRN + TYEdiLayout.GIS2_PRN + TYEdiLayout.GIS5_PRN +
                                    TYEdiLayout.RFF_PRN + TYEdiLayout.BANIP_AREA + TYEdiLayout.GID_PRN + TYEdiLayout.FTX_PRN + TYEdiLayout.MEA_PRN + TYEdiLayout.UNT_PRN;

            string sChar = "'";
            string[] Temps = TYEdiLayout.TEMP_REC1.Split(new string[] { sChar }, StringSplitOptions.None);
            iCnt = Temps.Length - 1;

            TYEdiLayout.UNT_0101 = string.Format("{0,-6:G}", iCnt.ToString());

            UP_Set_MigDataMix_CUSCAR();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.UNH_PRN + TYEdiLayout.BGM_PRN + TYEdiLayout.DTM_PRN + TYEdiLayout.GIS2_PRN + TYEdiLayout.GIS5_PRN +
                                    TYEdiLayout.RFF_PRN + TYEdiLayout.BANIP_AREA + TYEdiLayout.GID_PRN + TYEdiLayout.FTX_PRN + TYEdiLayout.MEA_PRN + TYEdiLayout.UNT_PRN;

            //공백제거
            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1.Replace(" ", "");

            UP_SetDataTable_RowAdd(TYEdiLayout.TEMP_REC1);

        }
        private void UP_Set_MigDataMix_CUSCAR()
        {
             //----------------(35) ---------------------'
             TYEdiLayout.UNH_PRN = TYEdiLayout.UNH_PRN1 + TYEdiLayout.UNH_PRN2 + TYEdiLayout.UNH_0101 + 
                                   TYEdiLayout.UNH_PRN3 + TYEdiLayout.UNH_PRN4 + TYEdiLayout.UNH_PRN5 + 
                                   TYEdiLayout.UNH_PRN6 + TYEdiLayout.UNH_PRN7 + TYEdiLayout.UNH_PRN8 + 
                                   TYEdiLayout.UNH_PRN9 + TYEdiLayout.UNH_PRN10 + TYEdiLayout.UNH_PRN11;
             //----------------(29) 신고번호 ---------------------//
             TYEdiLayout.BGM_PRN = TYEdiLayout.BGM_PRN1 + TYEdiLayout.BGM_PRN2 + TYEdiLayout.BGM_PRN3 + TYEdiLayout.BGM_PRN4 + 
                                   TYEdiLayout.BGM_02011 + TYEdiLayout.BGM_02012 + TYEdiLayout.BGM_02013 +
                                   TYEdiLayout.BGM_PRN5 + TYEdiLayout.BGM_0202 + TYEdiLayout.BGM_PRN6;
             //----------------(26) 반입일시 ---------------------//
             TYEdiLayout.DTM_PRN = TYEdiLayout.DTM_PRN1 + TYEdiLayout.DTM_PRN2 + TYEdiLayout.DTM_PRN3 + TYEdiLayout.DTM_PRN4 + 
                                   TYEdiLayout.DTM_01021 + TYEdiLayout.DTM_01022 + 
                                   TYEdiLayout.DTM_PRN5 + TYEdiLayout.DTM_PRN6 + TYEdiLayout.DTM_PRN7;
             //----------------(32) 신고세관 ---------------------//
             TYEdiLayout.LOC_PRN = TYEdiLayout.LOC_PRN1 + TYEdiLayout.LOC_PRN2 + TYEdiLayout.LOC_PRN3 + TYEdiLayout.LOC_PRN4 + TYEdiLayout.LOC_PRN5 + 
                                   TYEdiLayout.LOC_PRN6 + TYEdiLayout.LOC_PRN7 + TYEdiLayout.LOC_PRN8 + TYEdiLayout.LOC_PRN9 + TYEdiLayout.LOC_PRN10 + 
                                   TYEdiLayout.LOC_PRN11 + TYEdiLayout.LOC_PRN12 + TYEdiLayout.LOC_PRN13 + TYEdiLayout.LOC_PRN14 + TYEdiLayout.LOC_PRN15 + 
                                   TYEdiLayout.LOC_PRN16;
             //----------------(15) 품목분류 ---------------------//
             TYEdiLayout.GIS_PRN = TYEdiLayout.GIS_PRN1 + TYEdiLayout.GIS_PRN2 + TYEdiLayout.GIS_0101 + TYEdiLayout.GIS_PRN3 + TYEdiLayout.GIS_PRN4 + TYEdiLayout.GIS_PRN5 +
                                   TYEdiLayout.GIS_PRN6 + TYEdiLayout.GIS_PRN7;
             //----------------(15) 화물반입유형 ---------------------//
             TYEdiLayout.GIS2_PRN = TYEdiLayout.GIS2_PRN1 + TYEdiLayout.GIS2_PRN2 + TYEdiLayout.GIS2_0101 + 
                                    TYEdiLayout.GIS2_PRN3 + TYEdiLayout.GIS2_PRN4 + TYEdiLayout.GIS2_PRN5 +
                                    TYEdiLayout.GIS2_PRN6 + TYEdiLayout.GIS2_PRN7;
             //----------------(14) 분할반입구분 ---------------------//
             TYEdiLayout.GIS5_PRN = TYEdiLayout.GIS5_PRN1 + TYEdiLayout.GIS5_PRN2 + TYEdiLayout.GIS5_0101 + 
                                    TYEdiLayout.GIS5_PRN3 + TYEdiLayout.GIS5_PRN4 + TYEdiLayout.GIS5_PRN5 + 
                                    TYEdiLayout.GIS5_PRN6 + TYEdiLayout.GIS5_PRN7;
             //----------------(28) 화물관리번호 ---------------------//
             TYEdiLayout.RFF_PRN = TYEdiLayout.RFF_PRN1 + TYEdiLayout.RFF_PRN2 + TYEdiLayout.RFF_PRN3 + TYEdiLayout.RFF_PRN4 + 
                                   TYEdiLayout.RFF_0102 + TYEdiLayout.RFF_PRN5 + TYEdiLayout.RFF_0103 + TYEdiLayout.RFF_PRN6 + 
                                   TYEdiLayout.RFF_0104 + TYEdiLayout.RFF_PRN7;
             //----------------(29) 반입근거번호 ---------------------//
             if(  TYEdiLayout.RFF2_0101.Trim()  !=  "")
             {
                TYEdiLayout.RFF2_PRN = TYEdiLayout.RFF2_PRN1 + TYEdiLayout.RFF2_PRN2 + TYEdiLayout.RFF2_PRN3 + 
                                       TYEdiLayout.RFF2_PRN4 + TYEdiLayout.RFF2_0101 + TYEdiLayout.RFF2_PRN5;
             }
             //----------------(17) 반입갯수/포장종류 ---------------------//
             TYEdiLayout.GID_PRN = TYEdiLayout.GID_PRN1 + TYEdiLayout.GID_PRN2 + TYEdiLayout.GID_PRN3 +
                                   TYEdiLayout.GID_0102 + TYEdiLayout.GID_PRN4 + TYEdiLayout.GID_0101 +
                                   TYEdiLayout.GID_PRN5;
             //----------------(14) 반입사고유형 ---------------------//
             TYEdiLayout.FTX_PRN = TYEdiLayout.FTX_PRN1 + TYEdiLayout.FTX_PRN2 + TYEdiLayout.FTX_PRN3 +
                                   TYEdiLayout.FTX_PRN4 + TYEdiLayout.FTX_PRN5 + TYEdiLayout.FTX_PRN6 +
                                   TYEdiLayout.FTX_0201 + TYEdiLayout.FTX_PRN7;
             //----------------(27) 반입중량 ---------------------//
             TYEdiLayout.MEA_PRN = TYEdiLayout.MEA_PRN1 + TYEdiLayout.MEA_PRN2 + TYEdiLayout.MEA_PRN3 + 
                                   TYEdiLayout.MEA_PRN4 + TYEdiLayout.MEA_PRN5 + TYEdiLayout.MEA_PRN6 + 
                                   TYEdiLayout.MEA_PRN7 + TYEdiLayout.MEA_0202 + TYEdiLayout.MEA_PRN8;
             //----------------(26) ---------------------//
             TYEdiLayout.UNT_PRN = TYEdiLayout.UNT_PRN1 + TYEdiLayout.UNT_PRN2 + TYEdiLayout.UNT_0101 + 
                                   TYEdiLayout.UNT_PRN3 + TYEdiLayout.UNT_0201 + TYEdiLayout.UNT_PRN4 + 
                                   TYEdiLayout.UNT_PRN5;
        }
        #endregion

        #region  Description : 반입보고서 XML 데이타 조합
        private void UP_Set_XmlData_IPGOAdd()
        {

            string xml = TYEdiLayout.UP_Get_XmlGOVCBR632( TYEdiLayout.BGM_0202, 
                                                          TYEdiLayout.BGM_02011 + TYEdiLayout.BGM_02012 + TYEdiLayout.UNH_0101,
                                                          "GOVCBR632", 
                                                          "", 
                                                          "", 
                                                          TYEdiLayout.GIS2_0101, 
                                                          TYEdiLayout.GIS5_0101, 
                                                          "", 
                                                          TYEdiLayout.RFF2_0101,
                                                          TYEdiLayout.MEA_0202, 
                                                          TYEdiLayout.GID_0101, 
                                                          TYEdiLayout.FTX_0201, 
                                                          "", 
                                                          "", 
                                                          TYEdiLayout.GID_0102,
                                                          TYEdiLayout.RFF_0102 + TYEdiLayout.RFF_0103 + TYEdiLayout.RFF_0104.Trim(),
                                                          TYEdiLayout.DTM_01021+TYEdiLayout.DTM_01022, 
                                                          "", 
                                                          ""
                                                         );
            string sFileName = TYEdiLayout.DTM_01021 + TYEdiLayout.BGM_02011 + TYEdiLayout.BGM_02012 + TYEdiLayout.UNH_0101 + ".xml";

            this.UP_Set_XmlFileCreate(xml, sFileName);           
        }
        #endregion

        #region  Description : 반출보고서 자료 생성
        private void UP_Get_DataCHULF_UTT(string sDate)
        {
            DataTable dt = new DataTable();

            dt.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_758D5409",  sDate, this.CBO01_EDIGJ.GetValue().ToString(),this.CBO01_EDIGJ.GetValue().ToString());
            dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_758DH410", this.CBO01_EDIGJ.GetValue().ToString(), sDate,
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

            dt.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4A9X234", this.CBO01_EDIGJ.GetValue().ToString(), sDate, sDate, "");
            dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDMRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_758DH410", dt.Rows[i]["EDMGJ"].ToString(),
                                                                    dt.Rows[i]["EDMDATE"].ToString(),
                                                                    dt.Rows[i]["EDMJUKHA"].ToString(),
                                                                    dt.Rows[i]["EDMBLMSN"].ToString(),
                                                                    dt.Rows[i]["EDMBLHSN"].ToString(),
                                                                    dt.Rows[i]["EDMSINNO"].ToString()
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
            this.DbConnector.Attach("TY_P_UT_758D5409", sDate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString());
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
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
                            
                            double dCHMTQTY = Math.Round(Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) - (Convert.ToDouble(dk.Rows[i]["PRECHMTQTY"].ToString()) + Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString())), 3);

                            dCHMTQTY = dCHMTQTY + Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString());

                            this.DAT02_EDICHQTY.SetValue(Math.Round(dCHMTQTY * 1000, 0));
                        }
                        else
                        {
                            if (this.DAT02_EDIBUNHAL.GetValue().ToString() == "A")
                            {
                                this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) * 1000, 0));
                                this.DAT02_EDICHASU.SetValue("0");
                            }
                            else
                            {
                                this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString()) * 1000, 0));
                            }
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
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),    
                                            this.DAT02_EDITIME.GetValue(),   
                                            this.DAT02_EDIJSGB.GetValue(),   
                                            this.DAT02_EDIBANGB.GetValue(),  
                                            this.DAT02_EDIBUNHAL.GetValue(),                                            
                                            this.DAT02_EDICHQTY.GetValue(),  
                                            this.DAT02_EDICHCNT.GetValue(),  
                                            this.DAT02_EDINUQTY.GetValue(), 
                                            this.DAT02_EDINUCNT.GetValue(),  
                                            this.DAT02_EDICHASU.GetValue(),                                             
                                            this.DAT02_EDIIPHANG.GetValue(), 
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            this.DAT02_EDIHWAJU.GetValue(),  
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
            }

            //반출수기관리 등록 자료도 같이 신고
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4A9X234", this.CBO01_EDIGJ.GetValue().ToString(), sDate, sDate, "");
            DataTable dm = this.DbConnector.ExecuteDataTable();
            if (dm.Rows.Count > 0)
            {
                
                for (int i = 0; i < dm.Rows.Count; i++)
                {
                    if (dm.Rows[i]["EDMRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue(dm.Rows[i]["EDMGJ"].ToString());
                        this.DAT02_EDIDATE.SetValue(dm.Rows[i]["EDMDATE"].ToString());
                        this.DAT02_EDIJUKHA.SetValue(dm.Rows[i]["EDMJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(dm.Rows[i]["EDMBLMSN"].ToString());
                        this.DAT02_EDIBLHSN.SetValue(dm.Rows[i]["EDMBLHSN"].ToString());

                        this.DAT02_EDISINNO.SetValue(dm.Rows[i]["EDMSINNO"].ToString());

                        this.DAT02_EDINO1.SetValue(dm.Rows[i]["EDMNO1"].ToString());
                        this.DAT02_EDINO2.SetValue(dm.Rows[i]["EDMNO2"].ToString());
                        this.DAT02_EDINO3.SetValue(dm.Rows[i]["EDMNO3"].ToString());

                        this.DAT02_EDITIME.SetValue(dm.Rows[i]["EDMTIME"].ToString());
                        this.DAT02_EDIJSGB.SetValue(dm.Rows[i]["EDMJSGB"].ToString());
                        this.DAT02_EDIBANGB.SetValue(dm.Rows[i]["EDMBANGB"].ToString());

                        this.DAT02_EDICHASU.SetValue(dm.Rows[i]["EDMCHASU"].ToString());

                        //분할유무 판단
                        this.DAT02_EDIBUNHAL.SetValue(dm.Rows[i]["EDMBUNHAL"].ToString());
                        this.DAT02_EDICHQTY.SetValue(dm.Rows[i]["EDMCHQTY"].ToString());
                        this.DAT02_EDICHCNT.SetValue(dm.Rows[i]["EDMCHCNT"].ToString());

                        this.DAT02_EDINUQTY.SetValue(dm.Rows[i]["EDMNUQTY"].ToString());
                        this.DAT02_EDINUCNT.SetValue(dm.Rows[i]["EDMNUCNT"].ToString());

                        this.DAT02_EDIIPHANG.SetValue(dm.Rows[i]["EDMIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dm.Rows[i]["EDMHANGCHA"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dm.Rows[i]["EDMHWAMUL"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dm.Rows[i]["EDMHWAJU"].ToString());

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");

                        this.DAT02_EDIHMNO1.SetValue(dm.Rows[i]["EDMHMNO1"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(dm.Rows[i]["EDMHMNO2"].ToString());

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),     
                                                this.DAT02_EDIDATE.GetValue(),   
                                                this.DAT02_EDIJUKHA.GetValue(),  
                                                this.DAT02_EDIBLMSN.GetValue(),  
                                                this.DAT02_EDIBLHSN.GetValue(),                                              
                                                this.DAT02_EDISINNO.GetValue(),  
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),    
                                            this.DAT02_EDITIME.GetValue(),   
                                            this.DAT02_EDIJSGB.GetValue(),   
                                            this.DAT02_EDIBANGB.GetValue(),  
                                            this.DAT02_EDIBUNHAL.GetValue(),                                            
                                            this.DAT02_EDICHQTY.GetValue(),  
                                            this.DAT02_EDICHCNT.GetValue(),  
                                            this.DAT02_EDINUQTY.GetValue(), 
                                            this.DAT02_EDINUCNT.GetValue(),  
                                            this.DAT02_EDICHASU.GetValue(),                                             
                                            this.DAT02_EDIIPHANG.GetValue(), 
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            this.DAT02_EDIHWAJU.GetValue(),  
                                            this.DAT02_EDIRCVGB.GetValue(),  
                                            this.DAT02_EDIMSG.GetValue(),    
                                            this.DAT02_EDIHMNO1.GetValue(),  
                                            this.DAT02_EDIHMNO2.GetValue(),
                                            TYUserInfo.EmpNo
                                            });
                    }

                   
                }
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_UT_758G0422", data);
                }
                this.DbConnector.ExecuteTranQueryList();
            }
            
        }
        #endregion

        #region  Description : 반출보고서 Mig 자료 생성
        private void UP_Set_MigDataCHULF_Common(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75BE6452", this.CBO01_EDIGJ.GetValue().ToString(), sDate, "");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TYEdiLayout.CHGIS2_0101 = dt.Rows[i]["EDIBUNHAL"].ToString();
                    TYEdiLayout.CHCNT_0101 = string.Format("{0:##0}", double.Parse(dt.Rows[i]["EDICHASU"].ToString()));
                    TYEdiLayout.CHMEA2_0101 = string.Format("{0:############0.0}", double.Parse(dt.Rows[i]["EDINUQTY"].ToString())); 

                    TYEdiLayout.CHBGM_02011 = dt.Rows[i]["EDINO1"].ToString();
                    TYEdiLayout.CHBGM_02012 = dt.Rows[i]["EDINO2"].ToString().Substring(2,2);
                    TYEdiLayout.CHUNT_0201 = dt.Rows[i]["EDINO3"].ToString();
                    TYEdiLayout.CHUNH_0101 = TYEdiLayout.CHUNT_0201;
                    TYEdiLayout.CHBGM_02013 = TYEdiLayout.CHUNT_0201;

                    TYEdiLayout.CHBGM_0202 = dt.Rows[i]["EDIJSGB"].ToString();       
                    TYEdiLayout.CHDTM_01021 = dt.Rows[i]["EDIDATE"].ToString();                        
                    TYEdiLayout.CHDTM_01022 = dt.Rows[i]["EDITIME"].ToString();
                    TYEdiLayout.CHGIS_0101 = dt.Rows[i]["EDIBANGB"].ToString();

                    TYEdiLayout.CHMEA3_0101 = string.Format("{0:#######0}", double.Parse(dt.Rows[i]["EDICHCNT"].ToString())); 
                    TYEdiLayout.CHGID_0101 = dt.Rows[i]["EDICHCNT"].ToString(); 
                    TYEdiLayout.CHRFF_0102 = dt.Rows[i]["EDIJUKHA"].ToString();
                    TYEdiLayout.CHRFF_0103 =  Set_Fill4(dt.Rows[i]["EDIBLMSN"].ToString());

                    if (Convert.ToInt16(dt.Rows[i]["EDIBLHSN"].ToString()) > 0)
                    {
                        TYEdiLayout.CHRFF_0104 = Set_Fill4(dt.Rows[i]["EDIBLHSN"].ToString());
                    }
                    else
                    {
                        TYEdiLayout.CHRFF_0104 = "";
                    }
                    TYEdiLayout.CHMEA_0202 = string.Format("{0:###########0.000}", double.Parse(dt.Rows[i]["EDICHQTY"].ToString()));

                    TYEdiLayout.CHRFF2_0101 = "";

                    if (dt.Rows[i]["EDIBANGB"].ToString() == "50" || dt.Rows[i]["EDIBANGB"].ToString() == "51" ||
                        dt.Rows[i]["EDIBANGB"].ToString() == "71" || dt.Rows[i]["EDIBANGB"].ToString() == "60" || dt.Rows[i]["EDIBANGB"].ToString() == "63")
                    {
                        TYEdiLayout.CHRFF2_0101 = dt.Rows[i]["EDISINNO"].ToString();
                    }

                    UP_Set_MigData_CHULAdd();

                    UP_Set_XmlData_CHULAdd();
                }
            }
        }
        #endregion

        #region  Description : 반출 미그 데이타 조합
        private void UP_Set_MigData_CHULAdd()
        {
            int iCnt = 0;

            UP_Set_MigDataMix_CUSBRR();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.CHUNH_PRN + TYEdiLayout.CHBGM_PRN + TYEdiLayout.CHDTM_PRN + TYEdiLayout.CHGIS_PRN + TYEdiLayout.CHGIS2_PRN +
                                    TYEdiLayout.CHRFF_PRN + TYEdiLayout.BANCHUL_AREA + TYEdiLayout.CHGID_PRN + TYEdiLayout.CHMEA_PRN + TYEdiLayout.NU_AREA + TYEdiLayout.CHUNT_PRN;

            string sChar = "'";
            string[] Temps = TYEdiLayout.TEMP_REC1.Split(new string[] { sChar }, StringSplitOptions.None);
            iCnt = Temps.Length - 1;

            TYEdiLayout.CHUNT_0101 = string.Format("{0,-6:G}", iCnt.ToString());

            UP_Set_MigDataMix_CUSBRR();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.CHUNH_PRN + TYEdiLayout.CHBGM_PRN + TYEdiLayout.CHDTM_PRN + TYEdiLayout.CHGIS_PRN + TYEdiLayout.CHGIS2_PRN +
                                    TYEdiLayout.CHRFF_PRN + TYEdiLayout.BANCHUL_AREA + TYEdiLayout.CHGID_PRN + TYEdiLayout.CHMEA_PRN + TYEdiLayout.NU_AREA + TYEdiLayout.CHUNT_PRN;
            
            //공백제거
            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1.Replace(" ", "");

            UP_SetDataTable_RowAdd(TYEdiLayout.TEMP_REC1);
        }

        private void UP_Set_MigDataMix_CUSBRR()
        {
             //----------------(35) ---------------------'
             TYEdiLayout.CHUNH_PRN = TYEdiLayout.CHUNH_PRN1 + TYEdiLayout.CHUNH_PRN2 + TYEdiLayout.CHUNH_0101 + 
                                     TYEdiLayout.CHUNH_PRN3 + TYEdiLayout.CHUNH_PRN4 + TYEdiLayout.CHUNH_PRN5 + 
                                     TYEdiLayout.CHUNH_PRN6 + TYEdiLayout.CHUNH_PRN7 + TYEdiLayout.CHUNH_PRN8 + 
                                     TYEdiLayout.CHUNH_PRN9 + TYEdiLayout.CHUNH_PRN10 + TYEdiLayout.CHUNH_PRN11;
             //----------------(29) 신고번호 ---------------------'
             TYEdiLayout.CHBGM_PRN = TYEdiLayout.CHBGM_PRN1 + TYEdiLayout.CHBGM_PRN2 + TYEdiLayout.CHBGM_PRN3 + TYEdiLayout.CHBGM_PRN4 + 
                                     TYEdiLayout.CHBGM_02011 + TYEdiLayout.CHBGM_02012 + TYEdiLayout.CHBGM_02013 + 
                                     TYEdiLayout.CHBGM_PRN5 + TYEdiLayout.CHBGM_0202 + TYEdiLayout.CHBGM_PRN6;
             //----------------(26) 반입일시 ---------------------'
             TYEdiLayout.CHDTM_PRN = TYEdiLayout.CHDTM_PRN1 + TYEdiLayout.CHDTM_PRN2 + TYEdiLayout.CHDTM_PRN3 + TYEdiLayout.CHDTM_PRN4 + 
                                     TYEdiLayout.CHDTM_01021 + TYEdiLayout.CHDTM_01022 + TYEdiLayout.CHDTM_PRN5 + 
                                     TYEdiLayout.CHDTM_PRN6 + TYEdiLayout.CHDTM_PRN7;
             //----------------(32) 신고세관 ---------------------'
             TYEdiLayout.CHLOC_PRN = TYEdiLayout.CHLOC_PRN1 + TYEdiLayout.CHLOC_PRN2 + TYEdiLayout.CHLOC_PRN3 + 
                                     TYEdiLayout.CHLOC_PRN4 + TYEdiLayout.CHLOC_PRN5 + TYEdiLayout.CHLOC_PRN6 + 
                                     TYEdiLayout.CHLOC_PRN7 + TYEdiLayout.CHLOC_PRN8 + TYEdiLayout.CHLOC_PRN9 + 
                                     TYEdiLayout.CHLOC_PRN10 + TYEdiLayout.CHLOC_PRN11 + TYEdiLayout.CHLOC_PRN12 + 
                                     TYEdiLayout.CHLOC_PRN13 + TYEdiLayout.CHLOC_PRN14 + TYEdiLayout.CHLOC_PRN15 + 
                                     TYEdiLayout.CHLOC_PRN16;
             //----------------(15) 품목분류 ---------------------'
             TYEdiLayout.CHGIS_PRN = TYEdiLayout.CHGIS_PRN1 + TYEdiLayout.CHGIS_PRN2 + TYEdiLayout.CHGIS_0101 +
                                     TYEdiLayout.CHGIS_PRN3 + TYEdiLayout.CHGIS_PRN4 + TYEdiLayout.CHGIS_PRN5 + 
                                     TYEdiLayout.CHGIS_PRN6 + TYEdiLayout.CHGIS_PRN7;
             //----------------(15) 화물반입유형 ---------------------'
             TYEdiLayout.CHGIS2_PRN = TYEdiLayout.CHGIS2_PRN1 + TYEdiLayout.CHGIS2_PRN2 + TYEdiLayout.CHGIS2_0101 + 
                                      TYEdiLayout.CHGIS2_PRN3 + TYEdiLayout.CHGIS2_PRN4 + TYEdiLayout.CHGIS2_PRN5 + 
                                      TYEdiLayout.CHGIS2_PRN6 + TYEdiLayout.CHGIS2_PRN7;
             //----------------(28) 화물관리번호 ---------------------'
             TYEdiLayout.CHRFF_PRN = TYEdiLayout.CHRFF_PRN1 + TYEdiLayout.CHRFF_PRN2 + TYEdiLayout.CHRFF_PRN3 + TYEdiLayout.CHRFF_PRN4 + 
                                     TYEdiLayout.CHRFF_0102 + TYEdiLayout.CHRFF_PRN5 + TYEdiLayout.CHRFF_0103 + TYEdiLayout.CHRFF_PRN6 + 
                                     TYEdiLayout.CHRFF_0104 + TYEdiLayout.CHRFF_PRN7;
             //----------------(17) 반입갯수/포장종류 ---------------------'
             TYEdiLayout.CHGID_PRN = TYEdiLayout.CHGID_PRN1 + TYEdiLayout.CHGID_PRN2 + TYEdiLayout.CHGID_PRN3 + TYEdiLayout.CHGID_PRN4 + 
                                     TYEdiLayout.CHGID_0101 + TYEdiLayout.CHGID_PRN5;
             //----------------(27) 반입중량 ---------------------'
             TYEdiLayout.CHMEA_PRN = TYEdiLayout.CHMEA_PRN1 + TYEdiLayout.CHMEA_PRN2 + TYEdiLayout.CHMEA_PRN3 + 
                                     TYEdiLayout.CHMEA_PRN4 + TYEdiLayout.CHMEA_PRN5 + TYEdiLayout.CHMEA_PRN6 + 
                                     TYEdiLayout.CHMEA_PRN7 + TYEdiLayout.CHMEA_0202 + TYEdiLayout.CHMEA_PRN8;
             //----------------(26) ---------------------'
             TYEdiLayout.CHUNT_PRN = TYEdiLayout.CHUNT_PRN1 + TYEdiLayout.CHUNT_PRN2 + TYEdiLayout.CHUNT_0101 + 
                                     TYEdiLayout.CHUNT_PRN3 + TYEdiLayout.CHUNT_0201 + TYEdiLayout.CHUNT_PRN4 + 
                                     TYEdiLayout.CHUNT_PRN5;
 
             //NU_REC
             //---------------(31) 반입사고중량 ---------------------'
             TYEdiLayout.CHMEA2_PRN = TYEdiLayout.CHMEA2_PRN1 + TYEdiLayout.CHMEA2_PRN2 + TYEdiLayout.CHMEA2_PRN3 + 
                                      TYEdiLayout.CHMEA2_PRN4 + TYEdiLayout.CHMEA2_PRN5 + TYEdiLayout.CHMEA2_PRN6 + 
                                      TYEdiLayout.CHMEA2_PRN7 + TYEdiLayout.CHMEA2_PRN8 + TYEdiLayout.CHMEA2_PRN9 + 
                                      TYEdiLayout.CHMEA2_PRN10 + TYEdiLayout.CHMEA2_0101 + TYEdiLayout.CHMEA2_PRN11;
             //---------------(24) 반입사고갯수 ---------------------'
             TYEdiLayout.CHMEA3_PRN = TYEdiLayout.CHMEA3_PRN1 + TYEdiLayout.CHMEA3_PRN2 + TYEdiLayout.CHMEA3_PRN3 + 
                                      TYEdiLayout.CHMEA3_PRN4 + TYEdiLayout.CHMEA3_PRN5 + TYEdiLayout.CHMEA3_PRN6 + 
                                      TYEdiLayout.CHMEA3_PRN7 + TYEdiLayout.CHMEA3_PRN8 + TYEdiLayout.CHMEA3_PRN9 + 
                                      TYEdiLayout.CHMEA3_PRN10 + TYEdiLayout.CHMEA3_0101 + TYEdiLayout.CHMEA3_PRN11;
             //----------------(10) 분할반입차수 ---------------------'
             TYEdiLayout.CHCNT_PRN = TYEdiLayout.CHCNT_PRN1 + TYEdiLayout.CHCNT_PRN2 + TYEdiLayout.CHCNT_PRN3 +
                                     TYEdiLayout.CHCNT_PRN4 + TYEdiLayout.CHCNT_0101 + TYEdiLayout.CHCNT_PRN5;

             //전량반출이 아닌경우
             if(   TYEdiLayout.CHGIS2_0101 == "A")
             {
                 TYEdiLayout.NU_AREA = "";
             }
             else
             {
                 TYEdiLayout.NU_AREA = TYEdiLayout.CHCNT_PRN;
             }

             //----------------(29) 반입근거번호 ---------------------'
             TYEdiLayout.CHRFF2_PRN = TYEdiLayout.CHRFF2_PRN1 + TYEdiLayout.CHRFF2_PRN2 + TYEdiLayout.CHRFF2_PRN3 +
                                      TYEdiLayout.CHRFF2_PRN4 + TYEdiLayout.CHRFF2_0101 + TYEdiLayout.CHRFF2_PRN5;
             //통과화물 선적반출이 아닌경우
             if( TYEdiLayout.CHGIS_0101 != "70" )
             {
                 TYEdiLayout.BANCHUL_AREA = TYEdiLayout.CHRFF2_PRN;
             } 
        }
        #endregion

        #region  Description : 내국반입보고서 자료 생성 UTT
        private void UP_Get_DataHAIPGOF_UTT(string sSDate, string sEdate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75A8Z423", this.CBO01_EDIGJ.GetValue().ToString(), sSDate,sEdate );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_75A92424", this.CBO01_EDIGJ.GetValue().ToString(),
                                                                    dt.Rows[i]["CMBOGODAT"].ToString(),
                                                                    dt.Rows[i]["VSJUKHA"].ToString(),
                                                                    Set_Fill4(dt.Rows[i]["IPBLNOSEQ"].ToString()),
                                                                    dt.Rows[i]["IPHBLNOSEQ"].ToString(),
                                                                    dt.Rows[i]["IPIPHANG"].ToString(),
                                                                    dt.Rows[i]["IPBONSUN"].ToString(),
                                                                    dt.Rows[i]["IPHWAJU"].ToString(),
                                                                    dt.Rows[i]["IPHWAMUL"].ToString(),
                                                                    dt.Rows[i]["IPBLNO"].ToString()
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
            this.DbConnector.Attach("TY_P_UT_75A8Z423", this.CBO01_EDIGJ.GetValue().ToString(), sSDate, sEdate);
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IPBLNOSEQ"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(dk.Rows[i]["IPHBLNOSEQ"].ToString());
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["CMBOGODAT"].ToString());

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["IPIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["IPBONSUN"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["IPHWAMUL"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["IPHWAJU"].ToString());
                        this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["IPBLNO"].ToString());

                        this.DAT02_EDIJSGB.SetValue("9");
                        this.DAT02_EDIBOLOC.SetValue("Y");
                        this.DAT02_EDIIPCNT.SetValue("1");
                        this.DAT02_EDIPUMNM.SetValue(dk.Rows[i]["CDDESC1"].ToString());

                        this.DAT02_EDILOCGB.SetValue("UTT");
                        this.DAT02_EDIACDSAU.SetValue("송유관이송");
                        this.DAT02_EDIWSANJI.SetValue("");

                        this.DAT02_EDISDATE.SetValue(dk.Rows[i]["CMBOGODAT"].ToString());

                        //1년증가
                        DateTime dTimeDay = Convert.ToDateTime(dk.Rows[i]["CMBOGODAT"].ToString().Substring(0, 4) + "-" + dk.Rows[i]["CMBOGODAT"].ToString().Substring(4, 2) + "-" + dk.Rows[i]["CMBOGODAT"].ToString().Substring(6, 2));
                        dTimeDay = dTimeDay.AddDays(363);
                        string sEDIEDATE = Convert.ToString(dTimeDay.Year) + Convert.ToString(Set_Fill2(dTimeDay.Month.ToString())) + Convert.ToString(Set_Fill2(dTimeDay.Day.ToString()));
                        this.DAT02_EDIEDATE.SetValue(sEDIEDATE);

                        this.DAT02_EDIBRCNT.SetValue("0");
                        this.DAT02_EDIIPQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IPBSQTY"].ToString()) * 1000, 0));
                        this.DAT02_EDIMULGB.SetValue("A");

                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(dk.Rows[i]["IPSINOYY"].ToString().Substring(0, 4));
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString())));

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");

                        this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["IPSINOYY"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString())));

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),
                                            this.DAT02_EDIJUKHA.GetValue(),
                                            this.DAT02_EDIBLMSN.GetValue(),
                                            this.DAT02_EDIBLHSN.GetValue(),
                                            this.DAT02_EDIDATE.GetValue(),
                                            this.DAT02_EDIIPHANG.GetValue(),
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            this.DAT02_EDIHWAJU.GetValue(),
                                            this.DAT02_EDIHWAMUL.GetValue(),
                                            this.DAT02_EDIBLNO.GetValue(),
                                            this.DAT02_EDIJSGB.GetValue(),
                                            this.DAT02_EDIBOLOC.GetValue(),
                                            this.DAT02_EDIIPCNT.GetValue(),
                                            this.DAT02_EDIPUMNM.GetValue(),
                                            this.DAT02_EDILOCGB.GetValue(),
                                            this.DAT02_EDIACDSAU.GetValue(),
                                            this.DAT02_EDIWSANJI.GetValue(),
                                            this.DAT02_EDISDATE.GetValue(),
                                            this.DAT02_EDIEDATE.GetValue(),
                                            this.DAT02_EDIBRCNT.GetValue(),
                                            this.DAT02_EDIIPQTY.GetValue(),
                                            this.DAT02_EDIMULGB.GetValue(),
                                            this.DAT02_EDINO1.GetValue(),
                                            this.DAT02_EDINO2.GetValue(),
                                            this.DAT02_EDINO3.GetValue(),
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
                        this.DbConnector.Attach("TY_P_UT_75A98425", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion

        #region  Description : 내국반입보고서 자료 생성 SILO
        private void UP_Get_DataHAIPGOF_SILO(string sSDate, string sEdate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_A3Q9X157", sSDate, sEdate, "43");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_75A92424", this.CBO01_EDIGJ.GetValue().ToString(),
                                                                    dt.Rows[i]["IBDATE"].ToString(),
                                                                    dt.Rows[i]["IHJUKHANO"].ToString(),
                                                                    Set_Fill4(dt.Rows[i]["IBBLMSN"].ToString()),
                                                                    dt.Rows[i]["IBBLHSN"].ToString(),
                                                                    dt.Rows[i]["IHIPHANG"].ToString(),
                                                                    dt.Rows[i]["IBHANGCHA"].ToString(),
                                                                    dt.Rows[i]["IBHWAJU"].ToString(),
                                                                    dt.Rows[i]["IBGOKJONG"].ToString(),
                                                                    dt.Rows[i]["IBBLNO"].ToString()
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
            this.DbConnector.Attach("TY_P_US_A3Q9X157", sSDate, sEdate, "43");
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["IHJUKHANO"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IBBLMSN"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(dk.Rows[i]["IBBLHSN"].ToString());
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["IBDATE"].ToString());

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["IHIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["IBHANGCHA"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["IBGOKJONG"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["IBHWAJU"].ToString());
                        this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["IBBLNO"].ToString());

                        this.DAT02_EDIJSGB.SetValue("9");
                        this.DAT02_EDIBOLOC.SetValue("Y");
                        this.DAT02_EDIIPCNT.SetValue("1");
                        this.DAT02_EDIPUMNM.SetValue(dk.Rows[i]["IBGOKJONGNM"].ToString());

                        this.DAT02_EDILOCGB.SetValue("SILO");
                        this.DAT02_EDIACDSAU.SetValue("내국화물");
                        this.DAT02_EDIWSANJI.SetValue("");

                        this.DAT02_EDISDATE.SetValue(dk.Rows[i]["IBDATE"].ToString());

                        //1년증가
                        DateTime dTimeDay = Convert.ToDateTime(dk.Rows[i]["IBDATE"].ToString().Substring(0, 4) + "-" + dk.Rows[i]["IBDATE"].ToString().Substring(4, 2) + "-" + dk.Rows[i]["IBDATE"].ToString().Substring(6, 2));
                        dTimeDay = dTimeDay.AddDays(363);
                        string sEDIEDATE = Convert.ToString(dTimeDay.Year) + Convert.ToString(Set_Fill2(dTimeDay.Month.ToString())) + Convert.ToString(Set_Fill2(dTimeDay.Day.ToString()));
                        this.DAT02_EDIEDATE.SetValue(sEDIEDATE);

                        this.DAT02_EDIBRCNT.SetValue("0");
                        this.DAT02_EDIIPQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IBBEJNQTY"].ToString()) * 1000, 0));
                        this.DAT02_EDIMULGB.SetValue("A");

                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(dk.Rows[i]["IBHMNO1"].ToString().Substring(0, 4));
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(dk.Rows[i]["IBHMNO2"].ToString())));

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");

                        this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["IBHMNO1"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["IBHMNO2"].ToString())));

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),
                                            this.DAT02_EDIJUKHA.GetValue(),
                                            this.DAT02_EDIBLMSN.GetValue(),
                                            this.DAT02_EDIBLHSN.GetValue(),
                                            this.DAT02_EDIDATE.GetValue(),
                                            this.DAT02_EDIIPHANG.GetValue(),
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            this.DAT02_EDIHWAJU.GetValue(),
                                            this.DAT02_EDIHWAMUL.GetValue(),
                                            this.DAT02_EDIBLNO.GetValue(),
                                            this.DAT02_EDIJSGB.GetValue(),
                                            this.DAT02_EDIBOLOC.GetValue(),
                                            this.DAT02_EDIIPCNT.GetValue(),
                                            this.DAT02_EDIPUMNM.GetValue(),
                                            this.DAT02_EDILOCGB.GetValue(),
                                            this.DAT02_EDIACDSAU.GetValue(),
                                            this.DAT02_EDIWSANJI.GetValue(),
                                            this.DAT02_EDISDATE.GetValue(),
                                            this.DAT02_EDIEDATE.GetValue(),
                                            this.DAT02_EDIBRCNT.GetValue(),
                                            this.DAT02_EDIIPQTY.GetValue(),
                                            this.DAT02_EDIMULGB.GetValue(),
                                            this.DAT02_EDINO1.GetValue(),
                                            this.DAT02_EDINO2.GetValue(),
                                            this.DAT02_EDINO3.GetValue(),
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
                        this.DbConnector.Attach("TY_P_UT_75A98425", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion

        #region  Description :  내국반입보고서 Mig 자료 생성
        private void UP_Set_MigDataHAIPGOF_UTT(string sSDate, string sEdate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75AC0426", this.CBO01_EDIGJ.GetValue().ToString(), sSDate, sEdate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TYEdiLayout.BRBGM_02011 = dt.Rows[i]["EDINO1"].ToString();     
                    TYEdiLayout.BRBGM_02012 = dt.Rows[i]["EDINO2"].ToString().Substring(2,2);  
                    TYEdiLayout.BRUNH_0101 = dt.Rows[i]["EDINO3"].ToString();   
                    TYEdiLayout.BRBGM_02013 = TYEdiLayout.BRUNH_0101;
                    TYEdiLayout.BRUNT_0201 = TYEdiLayout.BRUNH_0101;


                    TYEdiLayout.BRBGM_0202 = dt.Rows[i]["EDIJSGB"].ToString();              //전송구분          
                    TYEdiLayout.BRGIS_0101 = dt.Rows[i]["EDIBOLOC"].ToString();             //보세구역부호         
                    TYEdiLayout.BRPAC_0201 = dt.Rows[i]["EDIIPCNT"].ToString();             //반입신고건수

                    TYEdiLayout.BRCST_02011 = dt.Rows[i]["EDINO1"].ToString();           //반입신고번호
                    TYEdiLayout.BRCST_02012 = dt.Rows[i]["EDINO2"].ToString().Substring(2,2);
                    TYEdiLayout.BRCST_02013 = dt.Rows[i]["EDINO3"].ToString();

                    TYEdiLayout.BRFTX1_0201 = dt.Rows[i]["EDIPUMNM"].ToString();                          //품명          
                    TYEdiLayout.BRFTX2_0201 = dt.Rows[i]["EDILOCGB"].ToString();                          //장치위치           
                    TYEdiLayout.BRFTX3_0201 = dt.Rows[i]["EDIACDSAU"].ToString();                          //장치사유

                    TYEdiLayout.BRDTM1_0102 = dt.Rows[i]["EDISDATE"].ToString();         //반입일시
                    TYEdiLayout.BRDTM2_0102 = dt.Rows[i]["EDIEDATE"].ToString();          //종료일시          
                    TYEdiLayout.BRMEA1_0202 = dt.Rows[i]["EDIBRCNT"].ToString();              //반입개수          
                    TYEdiLayout.BRMEA2_0202 =  string.Format("{0:###########0.000}", double.Parse(dt.Rows[i]["EDIIPQTY"].ToString()));       //반입중량          
                    TYEdiLayout.BRNAD_0201 = StringTransfer(dt.Rows[i]["VNSANGHO"].ToString(), 98);      //상호          
                    TYEdiLayout.BRNAD_0202 = StringTransfer(dt.Rows[i]["VNIRUM"].ToString(), 98) ;     //성명          
                    TYEdiLayout.BRNAD1_0201 = StringTransfer(dt.Rows[i]["VNJUSO"].ToString(), 100);    //주소          
                    //TYEdiLayout.BRNAD1_0203 = Replace(TYEdiLayout.BRNAD_0203, "?", "")          
                    //TYEdiLayout.BRNAD_0202 = Replace(TYEdiLayout.BRNAD_0202, "?", "")                                       
                    TYEdiLayout.BRRFF_0101 = dt.Rows[i]["EDIMULGB"].ToString();              //반입물품종류부호

                    UP_Set_MigData_HAIPGOFAdd();

                    UP_Set_XmlData_HAIPGOFAdd();
                }
            }
        }
        #endregion

        #region  Description : 내국반입 미그 데이타 조합
        private void UP_Set_MigData_HAIPGOFAdd()
        {
            int iCnt = 0;

            UP_Set_MigDataMix_CUSWBR5HA();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.BRUNH_PRN + TYEdiLayout.BRBGM_PRN + TYEdiLayout.BRGIS_PRN + TYEdiLayout.BRPAC_PRN + TYEdiLayout.BRCST_PRN + TYEdiLayout.BRFTX_PRN1 +
                                    TYEdiLayout.BRFTX_PRN2 + TYEdiLayout.BRFTX_PRN3 + TYEdiLayout.BRDTM1_PRN + TYEdiLayout.BRDTM2_PRN + TYEdiLayout.BRMEA1_PRN +
                                    TYEdiLayout.BRMEA2_PRN + TYEdiLayout.BRNAD_PRN + TYEdiLayout.BRNAD1_PRN + TYEdiLayout.BRRFF_PRN + TYEdiLayout.BRUNT_PRN;


            string sChar = "'";
            string[] Temps = TYEdiLayout.TEMP_REC1.Split(new string[] { sChar }, StringSplitOptions.None);
            iCnt = Temps.Length - 1;

            TYEdiLayout.BRUNT_0101 = string.Format("{0,-6:G}", iCnt.ToString());

            UP_Set_MigDataMix_CUSWBR5HA();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.BRUNH_PRN + TYEdiLayout.BRBGM_PRN + TYEdiLayout.BRGIS_PRN + TYEdiLayout.BRPAC_PRN + TYEdiLayout.BRCST_PRN + TYEdiLayout.BRFTX_PRN1 +
                                    TYEdiLayout.BRFTX_PRN2 + TYEdiLayout.BRFTX_PRN3 + TYEdiLayout.BRDTM1_PRN + TYEdiLayout.BRDTM2_PRN + TYEdiLayout.BRMEA1_PRN +
                                    TYEdiLayout.BRMEA2_PRN + TYEdiLayout.BRNAD_PRN + TYEdiLayout.BRNAD1_PRN + TYEdiLayout.BRRFF_PRN + TYEdiLayout.BRUNT_PRN;

            //공백제거
            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1.Replace(" ", "");

            UP_SetDataTable_RowAdd(TYEdiLayout.TEMP_REC1);
        }

        private void UP_Set_MigDataMix_CUSWBR5HA()
        {
                //----------------(35) 전자문서참조번호---------------------//
                TYEdiLayout.BRUNH_PRN = TYEdiLayout.BRUNH_PRN1 + TYEdiLayout.BRUNH_PRN2 + TYEdiLayout.BRUNH_0101 + 
                                        TYEdiLayout.BRUNH_PRN3 + TYEdiLayout.BRUNH_PRN4 + TYEdiLayout.BRUNH_PRN5 + 
                                        TYEdiLayout.BRUNH_PRN6 + TYEdiLayout.BRUNH_PRN7 + TYEdiLayout.BRUNH_PRN8 + 
                                        TYEdiLayout.BRUNH_PRN9 + TYEdiLayout.BRUNH_PRN10 + TYEdiLayout.BRUNH_PRN11;
                //----------------(29) 신고번호 ---------------------//
                TYEdiLayout.BRBGM_PRN = TYEdiLayout.BRBGM_PRN1 + TYEdiLayout.BRBGM_PRN2 + TYEdiLayout.BRBGM_PRN3 + TYEdiLayout.BRBGM_PRN4 +
                                        TYEdiLayout.BRBGM_02011 + TYEdiLayout.BRBGM_02012 + TYEdiLayout.BRBGM_02013 + 
                                        TYEdiLayout.BRBGM_PRN5 + TYEdiLayout.BRBGM_0202 + TYEdiLayout.BRBGM_PRN6;
             
                //----------------(15) 보세구역구분 ---------------------//
                TYEdiLayout.BRGIS_PRN = TYEdiLayout.BRGIS_PRN1 + TYEdiLayout.BRGIS_PRN2 + TYEdiLayout.BRGIS_0101 + TYEdiLayout.BRGIS_PRN3 +
                                        TYEdiLayout.BRGIS_PRN4 + TYEdiLayout.BRGIS_PRN5 + TYEdiLayout.BRGIS_PRN6 + TYEdiLayout.BRGIS_PRN7;
             
                //----------------(15) 반입신고건수 ---------------------//
                TYEdiLayout.BRPAC_PRN = TYEdiLayout.BRPAC_PRN1 + TYEdiLayout.BRPAC_PRN2 + TYEdiLayout.BRPAC_0201 + TYEdiLayout.BRPAC_PRN3;
             
                //----------------(15) 반입신고번호 ---------------------//
                TYEdiLayout.BRCST_PRN = TYEdiLayout.BRCST_PRN1 + TYEdiLayout.BRCST_PRN2 + TYEdiLayout.BRCST_PRN3 + 
                                        TYEdiLayout.BRCST_02011 + TYEdiLayout.BRCST_02012 + TYEdiLayout.BRCST_02013 + 
                                        TYEdiLayout.BRCST_PRN4 + TYEdiLayout.BRCST_PRN5 + TYEdiLayout.BRCST_PRN6 + 
                                        TYEdiLayout.BRCST_PRN7 + TYEdiLayout.BRCST_PRN8;
             
                //----------------(14) 품명 ---------------------//
                TYEdiLayout.BRFTX_PRN1 = TYEdiLayout.BRFTX1_PRN1 + TYEdiLayout.BRFTX1_PRN2 + TYEdiLayout.BRFTX1_PRN3 + 
                                         TYEdiLayout.BRFTX1_PRN4 + TYEdiLayout.BRFTX1_PRN5 + TYEdiLayout.BRFTX1_PRN6 + 
                                         TYEdiLayout.BRFTX1_0201 + TYEdiLayout.BRFTX1_PRN7;
              
                //----------------(14) 장치위치 ---------------------//
                TYEdiLayout.BRFTX_PRN2 = TYEdiLayout.BRFTX2_PRN1 + TYEdiLayout.BRFTX2_PRN2 + TYEdiLayout.BRFTX2_PRN3 + 
                                         TYEdiLayout.BRFTX2_PRN4 + TYEdiLayout.BRFTX2_PRN5 + TYEdiLayout.BRFTX2_PRN6 + 
                                         TYEdiLayout.BRFTX2_0201 + TYEdiLayout.BRFTX2_PRN7;
              
                //----------------(14) 장치사유 ---------------------//
                TYEdiLayout.BRFTX_PRN3 = TYEdiLayout.BRFTX3_PRN1 + TYEdiLayout.BRFTX3_PRN2 + TYEdiLayout.BRFTX3_PRN3 + 
                                         TYEdiLayout.BRFTX3_PRN4 + TYEdiLayout.BRFTX3_PRN5 + TYEdiLayout.BRFTX3_PRN6 + 
                                         TYEdiLayout.BRFTX3_0201 + TYEdiLayout.BRFTX3_PRN7;
             
                //----------------(26) 반입일자 ---------------------//
                TYEdiLayout.BRDTM1_PRN = TYEdiLayout.BRDTM1_PRN1 + TYEdiLayout.BRDTM1_PRN2 + TYEdiLayout.BRDTM1_PRN3 +
                                         TYEdiLayout.BRDTM1_PRN4 + TYEdiLayout.BRDTM1_0102 + TYEdiLayout.BRDTM1_PRN5 + 
                                         TYEdiLayout.BRDTM1_PRN6 + TYEdiLayout.BRDTM1_PRN7;
           
                //----------------(26) 반입종료일자 ---------------------//
                TYEdiLayout.BRDTM2_PRN = TYEdiLayout.BRDTM2_PRN1 + TYEdiLayout.BRDTM2_PRN2 + TYEdiLayout.BRDTM2_PRN3 + TYEdiLayout.BRDTM2_PRN4 + 
                                         TYEdiLayout.BRDTM2_0102 + TYEdiLayout.BRDTM2_PRN5 + TYEdiLayout.BRDTM2_PRN6 + TYEdiLayout.BRDTM2_PRN7;
              
                //----------------(27) 반입개수 ---------------------//
                TYEdiLayout.BRMEA1_PRN = TYEdiLayout.BRMEA1_PRN1 + TYEdiLayout.BRMEA1_PRN2 + TYEdiLayout.BRMEA1_PRN3 + 
                                         TYEdiLayout.BRMEA1_PRN4 + TYEdiLayout.BRMEA1_PRN5 + TYEdiLayout.BRMEA1_PRN6 + 
                                         TYEdiLayout.BRMEA1_PRN7 + TYEdiLayout.BRMEA1_0202 + TYEdiLayout.BRMEA1_PRN8;
              
                //----------------(27) 반입중량 ---------------------//
                TYEdiLayout.BRMEA2_PRN = TYEdiLayout.BRMEA2_PRN1 + TYEdiLayout.BRMEA2_PRN2 + TYEdiLayout.BRMEA2_PRN3 + 
                                         TYEdiLayout.BRMEA2_PRN4 + TYEdiLayout.BRMEA2_PRN5 + TYEdiLayout.BRMEA2_PRN6 + 
                                         TYEdiLayout.BRMEA2_PRN7 + TYEdiLayout.BRMEA2_0202 + TYEdiLayout.BRMEA2_PRN8;
              
                //----------------(27) 화주 ---------------------//
                TYEdiLayout.BRNAD_PRN = TYEdiLayout.BRNAD_PRN1 + TYEdiLayout.BRNAD_PRN2 + TYEdiLayout.BRNAD_PRN3 + TYEdiLayout.BRNAD_PRN4 + 
                                        TYEdiLayout.BRNAD_PRN5 + TYEdiLayout.BRNAD_0201 + TYEdiLayout.BRNAD_PRN6 + TYEdiLayout.BRNAD_0202 + 
                                        TYEdiLayout.BRNAD_PRN7 + TYEdiLayout.BRNAD_0203 + TYEdiLayout.BRNAD_PRN8;
             
                //----------------(27) 화주 ---------------------//
                TYEdiLayout.BRNAD1_PRN = TYEdiLayout.BRNAD1_PRN1 + TYEdiLayout.BRNAD1_PRN2 + TYEdiLayout.BRNAD1_PRN3 + TYEdiLayout.BRNAD1_PRN4 + 
                                         TYEdiLayout.BRNAD1_PRN5 + TYEdiLayout.BRNAD1_0201 + TYEdiLayout.BRNAD1_PRN6 + TYEdiLayout.BRNAD1_0202 + 
                                         TYEdiLayout.BRNAD1_PRN7;
                           
                //----------------(29) 반입물품종류부호 ---------------------//
                TYEdiLayout.BRRFF_PRN = TYEdiLayout.BRRFF_PRN1 + TYEdiLayout.BRRFF_PRN2 + TYEdiLayout.BRRFF_PRN3 + 
                                        TYEdiLayout.BRRFF_PRN4 + TYEdiLayout.BRRFF_0101 + TYEdiLayout.BRRFF_PRN5;
 
                //----------------(26) ---------------------//
                TYEdiLayout.BRUNT_PRN = TYEdiLayout.BRUNT_PRN1 + TYEdiLayout.BRUNT_PRN2 + TYEdiLayout.BRUNT_0101 + 
                                        TYEdiLayout.BRUNT_PRN3 + TYEdiLayout.BRUNT_0201 + TYEdiLayout.BRUNT_PRN4 + 
                                        TYEdiLayout.BRUNT_PRN5;
        }
        #endregion

        #region  Description : 내국반입 XML 데이타 조합
        private void UP_Set_XmlData_HAIPGOFAdd()
        {
            string xml = TYEdiLayout.UP_Get_XmlGOVCBR5HA(    TYEdiLayout.BRBGM_0202,         //전자문서 기능
                                                             TYEdiLayout.BRCST_02011 + TYEdiLayout.BRCST_02012 + TYEdiLayout.BRCST_02013,                  //제출번호
                                                             TYEdiLayout.BRPAC_0201, //반입신고건수
                                                             "GOVCBR5HA",               //문서형태구분
                                                             TYEdiLayout.BRGIS_0101,        //보세구역구분(A:자율관리보세구역, B:비자율관리보세구역)
                                                             TYEdiLayout.BRCST_02011 + TYEdiLayout.BRCST_02012 + TYEdiLayout.BRCST_02013,          //반입신고번호
                                                             TYEdiLayout.BRFTX3_0201,    //장치사유
                                                             TYEdiLayout.BRRFF_0101,                  //반입물품종류부호
                                                             TYEdiLayout.BRFTX1_0201,             //품명
                                                             TYEdiLayout.BRMEA1_0202,                 //반입개수
                                                             TYEdiLayout.BRMEA2_0202,                     //반입중량
                                                             "",                     //원산지
                                                             TYEdiLayout.BRNAD_0201,                   //화주 상호
                                                             "",            //화주 도로명코드 
                                                             "",                            //화주 상세주소
                                                             "",                      //화주 우편번호
                                                             "",                  //화주 건물관리번호
                                                             TYEdiLayout.BRNAD1_0201,              //화주 기본주소 
                                                             TYEdiLayout.BRNAD_0202,                            //화주 성명
                                                             "VL",               //포장부호
                                                             TYEdiLayout.BRDTM1_0102,                 //반입일자
                                                             TYEdiLayout.BRFTX2_0201,                   //장치위치
                                                             TYEdiLayout.BRDTM2_0102                //장치기간 종료일                                                           
                                                         );
            string sFileName = TYEdiLayout.BRDTM1_0102+TYEdiLayout.BRCST_02011 + TYEdiLayout.BRCST_02012 + TYEdiLayout.BRCST_02013 + ".xml";

            this.UP_Set_XmlFileCreate(xml, sFileName);
        }
        #endregion

        #region  Description : 내국반출보고서 자료 생성 UTT
        private void UP_Get_DataHBCHULF_UTT(string sSDate, string sEdate)
        {

            DataTable dk = new DataTable();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75AGV435", sSDate, sSDate, sEdate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y" && dt.Rows[i]["EDIDATE"].ToString() != "")
                    {
                        this.DbConnector.Attach("TY_P_UT_75AGY436", this.CBO01_EDIGJ.GetValue().ToString(),
                                                                    dt.Rows[i]["EDIDATE"].ToString(),
                                                                    dt.Rows[i]["VSJUKHA"].ToString(),
                                                                    Set_Fill4(dt.Rows[i]["IPBLNOSEQ"].ToString()),
                                                                    Set_Fill4(dt.Rows[i]["IPHBLNOSEQ"].ToString()),
                                                                    dt.Rows[i]["EDISINNO"].ToString()
                                                                    );
                    }
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();
            
            dk.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75AGV435", sSDate, sSDate, sEdate);
            dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {

                        if (Convert.ToDouble(dk.Rows[i]["NOWTOTALQTY"].ToString()) >= Convert.ToDouble(dk.Rows[i]["CSCUQTY"].ToString()))
                        {
                            this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                            this.DAT02_EDIDATE.SetValue(sEdate);
                            this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                            this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IPBLNOSEQ"].ToString()));

                            this.DAT02_EDIBLHSN.SetValue(Set_Fill4(dk.Rows[i]["IPHBLNOSEQ"].ToString()));

                            this.DAT02_EDISINNO.SetValue(dk.Rows[i]["EDISINNO"].ToString());
                            this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["CHIPHANG"].ToString());
                            this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["CHBONSUN"].ToString());
                            this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["CHHWAJU"].ToString());
                            this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["CHHWAMUL"].ToString());
                            this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["CHBLNO"].ToString());

                            this.DAT02_EDIJSGB.SetValue("9");
                            this.DAT02_EDIBOLOC.SetValue("Y");
                            this.DAT02_EDIIPCNT.SetValue("1");
                            this.DAT02_EDIPUMNM.SetValue(dk.Rows[i]["CDDESC1"].ToString());                            
                            this.DAT02_EDIACDSAU.SetValue("내국물품반출");
                            this.DAT02_EDICDATE.SetValue(this.DAT02_EDIDATE.GetValue().ToString());

                            this.DAT02_EDIBRCNT.SetValue("0");
                            this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IPBSQTY"].ToString()) * 1000, 0));
                            this.DAT02_EDIMULGB.SetValue("A");

                            this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                            //this.DAT02_EDINO2.SetValue(dk.Rows[i]["IPSINOYY"].ToString().Substring(0, 4));

                            this.DAT02_EDINO2.SetValue(this.DAT02_EDIDATE.GetValue().ToString().Substring(0, 4) );

                            this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(this.DAT02_EDIDATE.GetValue().ToString().Substring(0,4)))));                            

                            this.DAT02_EDIIPSINNO.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString() + dk.Rows[i]["IPSINOYY"].ToString().Substring(2, 2) + 
                                                           String.Format("{0:D8}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString()))
                                                           );                         

                            this.DAT02_EDIRCVGB.SetValue("");
                            this.DAT02_EDIMSG.SetValue("");

                            this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["IPSINOYY"].ToString());
                            this.DAT02_EDIHMNO2.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["IPSINO"].ToString())));

                            datas.Add(new object[] {   this.DAT02_EDIGJ.GetValue(),
                                                       this.DAT02_EDIDATE.GetValue(),
                                                       this.DAT02_EDIJUKHA.GetValue(),
                                                       this.DAT02_EDIBLMSN.GetValue(),
                                                       this.DAT02_EDIBLHSN.GetValue(),
                                                       this.DAT02_EDISINNO.GetValue(), 
                                                       this.DAT02_EDIIPHANG.GetValue(),
                                                       this.DAT02_EDIHANGCHA.GetValue(),
                                                       this.DAT02_EDIHWAJU.GetValue(),
                                                       this.DAT02_EDIHWAMUL.GetValue(),
                                                       this.DAT02_EDIBLNO.GetValue(),
                                                       this.DAT02_EDIJSGB.GetValue(),
                                                       this.DAT02_EDIBOLOC.GetValue(),
                                                       this.DAT02_EDIIPCNT.GetValue(),
                                                       this.DAT02_EDIPUMNM.GetValue(),
                                                       this.DAT02_EDIACDSAU.GetValue(),
                                                       this.DAT02_EDICDATE.GetValue(),
                                                       this.DAT02_EDIBRCNT.GetValue(),
                                                       this.DAT02_EDICHQTY.GetValue(),
                                                       this.DAT02_EDIMULGB.GetValue(),
                                                       this.DAT02_EDINO1.GetValue(),
                                                       this.DAT02_EDINO2.GetValue(),
                                                       this.DAT02_EDINO3.GetValue(),
                                                       this.DAT02_EDIIPSINNO.GetValue(),
                                                       this.DAT02_EDIRCVGB.GetValue(),
                                                       this.DAT02_EDIMSG.GetValue(),
                                                       this.DAT02_EDIHMNO1.GetValue(),
                                                       this.DAT02_EDIHMNO2.GetValue(),
                                                       TYUserInfo.EmpNo
                                            });
                        }
                       
                    }

                    pgBar.Value = pgBar.Value + 1;
                    pgBar.Refresh();
                }                
            }

            //내국반출수기관리 등록된 자료 포함
            dk.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4EBU269", this.CBO01_EDIGJ.GetValue().ToString(), sSDate, sEdate, "");
            dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                    this.DAT02_EDIDATE.SetValue(dk.Rows[i]["EDHDATE"].ToString());
                    this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["EDHJUKHA"].ToString());
                    this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["EDHBLMSN"].ToString()));

                    this.DAT02_EDIBLHSN.SetValue(Set_Fill4(dk.Rows[i]["EDHBLHSN"].ToString()));

                    this.DAT02_EDISINNO.SetValue(dk.Rows[i]["EDHSINNO"].ToString());
                    this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["EDHIPHANG"].ToString());
                    this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["EDHBONSUN"].ToString());
                    this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["EDHHWAJU"].ToString());
                    this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["EDHHWAMUL"].ToString());
                    this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["EDHBLNO"].ToString());

                    this.DAT02_EDIJSGB.SetValue(dk.Rows[i]["EDHJSGB"].ToString());
                    this.DAT02_EDIBOLOC.SetValue(dk.Rows[i]["EDHBOLOC"].ToString());
                    this.DAT02_EDIIPCNT.SetValue(dk.Rows[i]["EDHIPCNT"].ToString());
                    this.DAT02_EDIPUMNM.SetValue(dk.Rows[i]["EDHPUMNM"].ToString());
                    this.DAT02_EDIACDSAU.SetValue(dk.Rows[i]["EDHACDSAU"].ToString());
                    this.DAT02_EDICDATE.SetValue(dk.Rows[i]["EDHCDATE"].ToString());

                    this.DAT02_EDIBRCNT.SetValue(dk.Rows[i]["EDHBRCNT"].ToString());
                    this.DAT02_EDICHQTY.SetValue(dk.Rows[i]["EDHCHQTY"].ToString());
                    this.DAT02_EDIMULGB.SetValue(dk.Rows[i]["EDHMULGB"].ToString());

                    this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDHNO1"].ToString());
                    this.DAT02_EDINO2.SetValue(dk.Rows[i]["EDHNO2"].ToString());
                    this.DAT02_EDINO3.SetValue(dk.Rows[i]["EDHNO3"].ToString());

                    this.DAT02_EDIIPSINNO.SetValue(dk.Rows[i]["EDHIPSINNO"].ToString());

                    this.DAT02_EDIRCVGB.SetValue("");
                    this.DAT02_EDIMSG.SetValue("");

                    this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["EDHHMNO1"].ToString());
                    this.DAT02_EDIHMNO2.SetValue(dk.Rows[i]["EDHHMNO2"].ToString());

                    datas.Add(new object[] {   this.DAT02_EDIGJ.GetValue(),
                                                       this.DAT02_EDIDATE.GetValue(),
                                                       this.DAT02_EDIJUKHA.GetValue(),
                                                       this.DAT02_EDIBLMSN.GetValue(),
                                                       this.DAT02_EDIBLHSN.GetValue(),
                                                       this.DAT02_EDISINNO.GetValue(), 
                                                       this.DAT02_EDIIPHANG.GetValue(),
                                                       this.DAT02_EDIHANGCHA.GetValue(),
                                                       this.DAT02_EDIHWAJU.GetValue(),
                                                       this.DAT02_EDIHWAMUL.GetValue(),
                                                       this.DAT02_EDIBLNO.GetValue(),
                                                       this.DAT02_EDIJSGB.GetValue(),
                                                       this.DAT02_EDIBOLOC.GetValue(),
                                                       this.DAT02_EDIIPCNT.GetValue(),
                                                       this.DAT02_EDIPUMNM.GetValue(),
                                                       this.DAT02_EDIACDSAU.GetValue(),
                                                       this.DAT02_EDICDATE.GetValue(),
                                                       this.DAT02_EDIBRCNT.GetValue(),
                                                       this.DAT02_EDICHQTY.GetValue(),
                                                       this.DAT02_EDIMULGB.GetValue(),
                                                       this.DAT02_EDINO1.GetValue(),
                                                       this.DAT02_EDINO2.GetValue(),
                                                       this.DAT02_EDINO3.GetValue(),
                                                       this.DAT02_EDIIPSINNO.GetValue(),
                                                       this.DAT02_EDIRCVGB.GetValue(),
                                                       this.DAT02_EDIMSG.GetValue(),
                                                       this.DAT02_EDIHMNO1.GetValue(),
                                                       this.DAT02_EDIHMNO2.GetValue(),
                                                       TYUserInfo.EmpNo
                                            });
                }
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_UT_75AHD440", data);
                }
                this.DbConnector.ExecuteTranQueryList();
            }

        }
        #endregion

        #region  Description : 내국반출보고서 자료 생성 SILO
        private void UP_Get_DataHBCHULF_SILO(string sSDate, string sEdate)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_A3QFP160", sSDate, sSDate, sEdate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y" && dt.Rows[i]["EDIDATE"].ToString() != "")
                    {
                        this.DbConnector.Attach("TY_P_UT_75AGY436", this.CBO01_EDIGJ.GetValue().ToString(),
                                                                    dt.Rows[i]["EDIDATE"].ToString(),
                                                                    dt.Rows[i]["IHJUKHANO"].ToString(),
                                                                    Set_Fill4(dt.Rows[i]["CHBLMSN"].ToString()),
                                                                    Set_Fill4(dt.Rows[i]["CHBLHSN"].ToString()),
                                                                    dt.Rows[i]["EDISINNO"].ToString()
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
            this.DbConnector.Attach("TY_P_US_A3QFP160", sSDate, sSDate, sEdate);
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dk.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {

                        
                            this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                            this.DAT02_EDIDATE.SetValue(sEdate);
                            this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["IHJUKHANO"].ToString());
                            this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["CHBLMSN"].ToString()));
                            this.DAT02_EDIBLHSN.SetValue(Set_Fill4(dk.Rows[i]["CHBLHSN"].ToString()));
                            this.DAT02_EDISINNO.SetValue(dk.Rows[i]["EDISINNO"].ToString());

                            this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["IHIPHANG"].ToString());

                            this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["CHHANGCHA"].ToString());
                            this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["CHHWAJU"].ToString());
                            this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["CHGOKJONG"].ToString());
                            this.DAT02_EDIBLNO.SetValue(dk.Rows[i]["CHBLNO"].ToString());

                            this.DAT02_EDIJSGB.SetValue("9");
                            this.DAT02_EDIBOLOC.SetValue("Y");
                            this.DAT02_EDIIPCNT.SetValue("1");
                            this.DAT02_EDIPUMNM.SetValue(dk.Rows[i]["CHGOKJONGNM"].ToString());
                            this.DAT02_EDIACDSAU.SetValue("내국물품반출");
                            this.DAT02_EDICDATE.SetValue(this.DAT02_EDIDATE.GetValue().ToString());

                            this.DAT02_EDIBRCNT.SetValue("0");
                            this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IBBEJNQTY"].ToString()) * 1000, 0));
                            this.DAT02_EDIMULGB.SetValue("A");

                            this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                            this.DAT02_EDINO2.SetValue(dk.Rows[i]["CHHMNO1"].ToString().Substring(0, 4));
                            this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(this.DAT02_EDIDATE.GetValue().ToString().Substring(0, 4)))));

                            this.DAT02_EDIIPSINNO.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString() + dk.Rows[i]["CHHMNO1"].ToString().Substring(2, 2) +
                                                           String.Format("{0:D8}", Convert.ToInt64(dk.Rows[i]["CHHMNO2"].ToString()))
                                                           );

                            this.DAT02_EDIRCVGB.SetValue("");
                            this.DAT02_EDIMSG.SetValue("");

                            this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["CHHMNO1"].ToString());
                            this.DAT02_EDIHMNO2.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["CHHMNO2"].ToString())));

                            datas.Add(new object[] {   this.DAT02_EDIGJ.GetValue(),
                                                       this.DAT02_EDIDATE.GetValue(),
                                                       this.DAT02_EDIJUKHA.GetValue(),
                                                       this.DAT02_EDIBLMSN.GetValue(),
                                                       this.DAT02_EDIBLHSN.GetValue(),
                                                       this.DAT02_EDISINNO.GetValue(), 
                                                       this.DAT02_EDIIPHANG.GetValue(),
                                                       this.DAT02_EDIHANGCHA.GetValue(),
                                                       this.DAT02_EDIHWAJU.GetValue(),
                                                       this.DAT02_EDIHWAMUL.GetValue(),
                                                       this.DAT02_EDIBLNO.GetValue(),
                                                       this.DAT02_EDIJSGB.GetValue(),
                                                       this.DAT02_EDIBOLOC.GetValue(),
                                                       this.DAT02_EDIIPCNT.GetValue(),
                                                       this.DAT02_EDIPUMNM.GetValue(),
                                                       this.DAT02_EDIACDSAU.GetValue(),
                                                       this.DAT02_EDICDATE.GetValue(),
                                                       this.DAT02_EDIBRCNT.GetValue(),
                                                       this.DAT02_EDICHQTY.GetValue(),
                                                       this.DAT02_EDIMULGB.GetValue(),
                                                       this.DAT02_EDINO1.GetValue(),
                                                       this.DAT02_EDINO2.GetValue(),
                                                       this.DAT02_EDINO3.GetValue(),
                                                       this.DAT02_EDIIPSINNO.GetValue(),
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
                        this.DbConnector.Attach("TY_P_UT_75AHD440", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion

        #region  Description :  내국반출보고서 Mig 자료 생성
        private void UP_Set_MigDataHBCHULF_UTT(string sSDate, string sEdate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75B8H443", this.CBO01_EDIGJ.GetValue().ToString(), sSDate, sEdate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                      TYEdiLayout.BRCHBGM_02011 = dt.Rows[i]["EDINO1"].ToString();
                      TYEdiLayout.BRCHBGM_02012 = dt.Rows[i]["EDINO2"].ToString().Substring(2, 2);
                      TYEdiLayout.BRCHUNH_0101 = dt.Rows[i]["EDINO3"].ToString();
                      TYEdiLayout.BRCHBGM_02013 = TYEdiLayout.BRCHUNH_0101;
                      TYEdiLayout.BRCHUNT_0201 = TYEdiLayout.BRCHUNH_0101;
                      TYEdiLayout.BRCHBGM_0202 = dt.Rows[i]["EDIJSGB"].ToString();                          //전송구분                    
                      TYEdiLayout.BRCHGIS_0101 = dt.Rows[i]["EDIBOLOC"].ToString();             //보세구역부호          
                      TYEdiLayout.BRCHPAC_0201 = dt.Rows[i]["EDIIPCNT"].ToString();          //반출신고건수

                      TYEdiLayout.BRCHCST_02011 = dt.Rows[i]["EDINO1"].ToString();      //반출신고번호
                      TYEdiLayout.BRCHCST_02012 = dt.Rows[i]["EDINO2"].ToString().Substring(2, 2);
                      TYEdiLayout.BRCHCST_02013 = dt.Rows[i]["EDINO3"].ToString();
                      TYEdiLayout.BRCHFTX1_0201 = dt.Rows[i]["EDIPUMNM"].ToString();                        //품명           
                      TYEdiLayout.BRCHFTX3_0201 = dt.Rows[i]["EDIACDSAU"].ToString();                        //반출사유                    
                      TYEdiLayout.BRCHDTM1_0102 = dt.Rows[i]["EDICDATE"].ToString();      //반출일시          
                      TYEdiLayout.BRCHMEA1_0202 = dt.Rows[i]["EDIBRCNT"].ToString();                       //반출개수                    
                      TYEdiLayout.BRCHMEA2_0202 = string.Format("{0:###########0.000}", double.Parse(dt.Rows[i]["EDICHQTY"].ToString())); //반출중량

                      TYEdiLayout.BRCHNAD_0201 = StringTransfer(dt.Rows[i]["VNSANGHO"].ToString(), 98);      //상호   
                      TYEdiLayout.BRCHNAD_0202 = StringTransfer(dt.Rows[i]["VNIRUM"].ToString(), 98);      //상호   
                      TYEdiLayout.BRCHNAD1_0201 = StringTransfer(dt.Rows[i]["VNJUSO"].ToString(), 100);     //주소

                      TYEdiLayout.BRCHRFF1_0101 = dt.Rows[i]["EDIMULGB"].ToString();              //반출물품종류부호          
                      TYEdiLayout.BRCHRFF2_02011 = dt.Rows[i]["EDIIPSINNO"].ToString().Substring(0,8);                 //반입번호
                      TYEdiLayout.BRCHRFF2_02012 = dt.Rows[i]["EDIIPSINNO"].ToString().Substring(8,2);
                      TYEdiLayout.BRCHRFF2_02013 = dt.Rows[i]["EDIIPSINNO"].ToString().Substring(10,8);                

                      UP_Set_MigData_HBCHULFAdd();

                      UP_Set_XmlData_HBCHULFAdd();
                }
            }
        }
        #endregion

        #region  Description : 내국반출 미그 데이타 조합
        private void UP_Set_MigData_HBCHULFAdd()
        {
            int iCnt = 0;

            UP_Set_MigDataMix_CUSWBR5HB();

             TYEdiLayout.TEMP_REC1 = TYEdiLayout.BRCHUNH_PRN + TYEdiLayout.BRCHBGM_PRN + TYEdiLayout.BRCHGIS_PRN + TYEdiLayout.BRCHPAC_PRN + TYEdiLayout.BRCHCST_PRN + TYEdiLayout.BRCHFTX_PRN1 + 
                                     TYEdiLayout.BRCHFTX_PRN3 + TYEdiLayout.BRCHDTM1_PRN + TYEdiLayout.BRCHMEA1_PRN + TYEdiLayout.BRCHMEA2_PRN + TYEdiLayout.BRCHNAD_PRN + TYEdiLayout.BRCHNAD1_PRN + 
                                     TYEdiLayout.BRCHRFF1_PRN + TYEdiLayout.BRCHRFF2_PRN + TYEdiLayout.BRCHUNT_PRN;



            string sChar = "'";
            string[] Temps = TYEdiLayout.TEMP_REC1.Split(new string[] { sChar }, StringSplitOptions.None);
            iCnt = Temps.Length - 1;

            TYEdiLayout.BRCHUNT_0101 = string.Format("{0,-6:G}", iCnt.ToString());

            UP_Set_MigDataMix_CUSWBR5HB();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.BRCHUNH_PRN + TYEdiLayout.BRCHBGM_PRN + TYEdiLayout.BRCHGIS_PRN + TYEdiLayout.BRCHPAC_PRN + TYEdiLayout.BRCHCST_PRN + TYEdiLayout.BRCHFTX_PRN1 +
                                    TYEdiLayout.BRCHFTX_PRN3 + TYEdiLayout.BRCHDTM1_PRN + TYEdiLayout.BRCHMEA1_PRN + TYEdiLayout.BRCHMEA2_PRN + TYEdiLayout.BRCHNAD_PRN + TYEdiLayout.BRCHNAD1_PRN +
                                    TYEdiLayout.BRCHRFF1_PRN + TYEdiLayout.BRCHRFF2_PRN + TYEdiLayout.BRCHUNT_PRN;

            //공백제거
            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1.Replace(" ", "");

            UP_SetDataTable_RowAdd(TYEdiLayout.TEMP_REC1);
        }

        private void UP_Set_MigDataMix_CUSWBR5HB()
        {
             //----------------(35) 전자문서참조번호---------------------//
             TYEdiLayout.BRCHUNH_PRN = TYEdiLayout.BRCHUNH_PRN1 + TYEdiLayout.BRCHUNH_PRN2 + TYEdiLayout.BRCHUNH_0101 + 
                                       TYEdiLayout.BRCHUNH_PRN3 + TYEdiLayout.BRCHUNH_PRN4 + TYEdiLayout.BRCHUNH_PRN5 + 
                                       TYEdiLayout.BRCHUNH_PRN6 + TYEdiLayout.BRCHUNH_PRN7 + TYEdiLayout.BRCHUNH_PRN8 + 
                                       TYEdiLayout.BRCHUNH_PRN9 + TYEdiLayout.BRCHUNH_PRN10 + TYEdiLayout.BRCHUNH_PRN11;
             //----------------(29) 신고번호 ---------------------//
             TYEdiLayout.BRCHBGM_PRN = TYEdiLayout.BRCHBGM_PRN1 + TYEdiLayout.BRCHBGM_PRN2 + TYEdiLayout.BRCHBGM_PRN3 + TYEdiLayout.BRCHBGM_PRN4 + 
                                       TYEdiLayout.BRCHBGM_02011 + TYEdiLayout.BRCHBGM_02012 + TYEdiLayout.BRCHBGM_02013 + 
                                       TYEdiLayout.BRCHBGM_PRN5 + TYEdiLayout.BRCHBGM_0202 + TYEdiLayout.BRCHBGM_PRN6;
             
             //----------------(15) 보세구역구분 ---------------------//
             TYEdiLayout.BRCHGIS_PRN = TYEdiLayout.BRCHGIS_PRN1 + TYEdiLayout.BRCHGIS_PRN2 + TYEdiLayout.BRCHGIS_0101 + 
                                       TYEdiLayout.BRCHGIS_PRN3 + TYEdiLayout.BRCHGIS_PRN4 + TYEdiLayout.BRCHGIS_PRN5 + 
                                       TYEdiLayout.BRCHGIS_PRN6 + TYEdiLayout.BRCHGIS_PRN7;
               
             //----------------(15) 보세구역구분 ---------------------//
             TYEdiLayout.BRCHPAC_PRN = TYEdiLayout.BRCHPAC_PRN1 + TYEdiLayout.BRCHPAC_PRN2 + TYEdiLayout.BRCHPAC_0201 + 
                                       TYEdiLayout.BRCHPAC_PRN3;
             
             //----------------(15) 반출신고번호 ---------------------//
             TYEdiLayout.BRCHCST_PRN = TYEdiLayout.BRCHCST_PRN1 + TYEdiLayout.BRCHCST_PRN2 + TYEdiLayout.BRCHCST_PRN3 + 
                                       TYEdiLayout.BRCHCST_02011 + TYEdiLayout.BRCHCST_02012 + TYEdiLayout.BRCHCST_02013 + 
                                       TYEdiLayout.BRCHCST_PRN4 + TYEdiLayout.BRCHCST_PRN5 + TYEdiLayout.BRCHCST_PRN6 + 
                                       TYEdiLayout.BRCHCST_PRN7 + TYEdiLayout.BRCHCST_PRN8;
             
             //----------------(14) 품명 ---------------------//
             TYEdiLayout.BRCHFTX_PRN1 = TYEdiLayout.BRCHFTX1_PRN1 + TYEdiLayout.BRCHFTX1_PRN2 + TYEdiLayout.BRCHFTX1_PRN3 + 
                            TYEdiLayout.BRCHFTX1_PRN4 + TYEdiLayout.BRCHFTX1_PRN5 + TYEdiLayout.BRCHFTX1_PRN6 + 
                            TYEdiLayout.BRCHFTX1_0201 + TYEdiLayout.BRCHFTX1_PRN7;
              
             //----------------(14) 반출사유 ---------------------//
             TYEdiLayout.BRCHFTX_PRN3 = TYEdiLayout.BRCHFTX3_PRN1 + TYEdiLayout.BRCHFTX3_PRN2 + TYEdiLayout.BRCHFTX3_PRN3 + 
                                        TYEdiLayout.BRCHFTX3_PRN4 + TYEdiLayout.BRCHFTX3_PRN5 + TYEdiLayout.BRCHFTX3_PRN6 + 
                                        TYEdiLayout.BRCHFTX3_0201 + TYEdiLayout.BRCHFTX3_PRN7;
             
             //----------------(26) 반출일자 ---------------------//
             TYEdiLayout.BRCHDTM1_PRN = TYEdiLayout.BRCHDTM1_PRN1 + TYEdiLayout.BRCHDTM1_PRN2 + TYEdiLayout.BRCHDTM1_PRN3 + 
                                        TYEdiLayout.BRCHDTM1_PRN4 + TYEdiLayout.BRCHDTM1_0102 + TYEdiLayout.BRCHDTM1_PRN5 + 
                                        TYEdiLayout.BRCHDTM1_PRN6 + TYEdiLayout.BRCHDTM1_PRN7;
           
             //----------------(27) 반출개수 ---------------------//
             TYEdiLayout.BRCHMEA1_PRN = TYEdiLayout.BRCHMEA1_PRN1 + TYEdiLayout.BRCHMEA1_PRN2 + TYEdiLayout.BRCHMEA1_PRN3 + 
                                        TYEdiLayout.BRCHMEA1_PRN4 + TYEdiLayout.BRCHMEA1_PRN5 + TYEdiLayout.BRCHMEA1_PRN6 + 
                                        TYEdiLayout.BRCHMEA1_PRN7 + TYEdiLayout.BRCHMEA1_0202 + TYEdiLayout.BRCHMEA1_PRN8;
              
             //----------------(27) 반출중량 ---------------------//
             TYEdiLayout.BRCHMEA2_PRN = TYEdiLayout.BRCHMEA2_PRN1 + TYEdiLayout.BRCHMEA2_PRN2 + TYEdiLayout.BRCHMEA2_PRN3 + 
                                        TYEdiLayout.BRCHMEA2_PRN4 + TYEdiLayout.BRCHMEA2_PRN5 + TYEdiLayout.BRCHMEA2_PRN6 + 
                                        TYEdiLayout.BRCHMEA2_PRN7 + TYEdiLayout.BRCHMEA2_0202 + TYEdiLayout.BRCHMEA2_PRN8;
              
             //----------------(27) 화주 ---------------------//
             TYEdiLayout.BRCHNAD_PRN = TYEdiLayout.BRCHNAD_PRN1 + TYEdiLayout.BRCHNAD_PRN2 + TYEdiLayout.BRCHNAD_PRN3 + 
                                       TYEdiLayout.BRCHNAD_PRN4 + TYEdiLayout.BRCHNAD_PRN5 + TYEdiLayout.BRCHNAD_0201 + 
                                       TYEdiLayout.BRCHNAD_PRN6 + TYEdiLayout.BRCHNAD_0202 + TYEdiLayout.BRCHNAD_PRN7 + 
                                       TYEdiLayout.BRCHNAD_0203 + TYEdiLayout.BRCHNAD_PRN8;
               
             //----------------(27) 화주 ---------------------//
             TYEdiLayout.BRCHNAD1_PRN = TYEdiLayout.BRCHNAD1_PRN1 + TYEdiLayout.BRCHNAD1_PRN2 + TYEdiLayout.BRCHNAD1_PRN3 + 
                                        TYEdiLayout.BRCHNAD1_PRN4 + TYEdiLayout.BRCHNAD1_PRN5 + TYEdiLayout.BRCHNAD1_0201 + 
                                        TYEdiLayout.BRCHNAD1_PRN6 + TYEdiLayout.BRCHNAD1_0202 + TYEdiLayout.BRCHNAD1_PRN7;
              
             //----------------(29) 반출물품종류부호 ---------------------//
             TYEdiLayout.BRCHRFF1_PRN = TYEdiLayout.BRCHRFF1_PRN1 + TYEdiLayout.BRCHRFF1_PRN2 + TYEdiLayout.BRCHRFF1_PRN3 + 
                                        TYEdiLayout.BRCHRFF1_PRN4 + TYEdiLayout.BRCHRFF1_0101 + TYEdiLayout.BRCHRFF1_PRN5;
                
             //----------------(29) 반입신고번호 ---------------------//
             TYEdiLayout.BRCHRFF2_PRN = TYEdiLayout.BRCHRFF2_PRN1 + TYEdiLayout.BRCHRFF2_PRN2 + TYEdiLayout.BRCHRFF2_PRN3 + 
                                        TYEdiLayout.BRCHRFF2_PRN4 + TYEdiLayout.BRCHRFF2_02011 + TYEdiLayout.BRCHRFF2_02012 + 
                                        TYEdiLayout.BRCHRFF2_02013 + TYEdiLayout.BRCHRFF2_PRN5;
 
             //----------------(26) 전자문서 참조번호---------------------//
             TYEdiLayout.BRCHUNT_PRN = TYEdiLayout.BRCHUNT_PRN1 + TYEdiLayout.BRCHUNT_PRN2 + TYEdiLayout.BRCHUNT_0101 +
                                       TYEdiLayout.BRCHUNT_PRN3 + TYEdiLayout.BRCHUNT_0201 + TYEdiLayout.BRCHUNT_PRN4 +
                                       TYEdiLayout.BRCHUNT_PRN5;
        }
        #endregion

        #region  Description : 내국반출 XML 데이타 조합
        private void UP_Set_XmlData_HBCHULFAdd()
        {

            string xml = TYEdiLayout.UP_Get_XmlGOVCBR5HB(    TYEdiLayout.BRCHBGM_0202,         //전자문서 기능
                                                             TYEdiLayout.BRCHBGM_02011+TYEdiLayout.BRCHBGM_02012+TYEdiLayout.BRCHUNH_0101,                   //제출번호
                                                             TYEdiLayout.BRCHPAC_0201,  //반입신고건수
                                                             "GOVCBR5HB",             //문서형태구분
                                                             TYEdiLayout.BRCHGIS_0101,          //보세구역구분(A:자율관리보세구역, B:비자율관리보세구역)
                                                             TYEdiLayout.BRCHBGM_02011+TYEdiLayout.BRCHBGM_02012+TYEdiLayout.BRCHUNH_0101,           //반출신고번호
                                                             TYEdiLayout.BRCHFTX3_0201,    //반출사유
                                                             TYEdiLayout.BRCHRFF1_0101,                   //반출물품종류부호
                                                             TYEdiLayout.BRCHFTX1_0201,                //품명
                                                             TYEdiLayout.BRCHMEA1_0202,                   //반출개수
                                                             TYEdiLayout.BRCHMEA2_0202,                     //반출중량
                                                             TYEdiLayout.BRCHNAD_0201,                   //화주 상호
                                                             "",            //화주 도로명코드 
                                                             "",                            //화주 상세주소
                                                             "",                      //화주 우편번호
                                                             "",                  //화주 건물관리번호
                                                             TYEdiLayout.BRCHNAD1_0201,              //화주 기본주소 
                                                             TYEdiLayout.BRCHNAD_0202,                            //화주 성명
                                                             TYEdiLayout.BRCHRFF2_02011+TYEdiLayout.BRCHRFF2_02012+TYEdiLayout.BRCHRFF2_02013,               //반입신고번호
                                                             TYEdiLayout.BRCHDTM1_0102                //반출일자                                                       
                                                         );
            string sFileName = TYEdiLayout.BRCHDTM1_0102+TYEdiLayout.BRCHBGM_02011 + TYEdiLayout.BRCHBGM_02012 + TYEdiLayout.BRCHUNH_0101 + ".xml";

            this.UP_Set_XmlFileCreate(xml, sFileName);
        }
        #endregion

        #region  Description : 반출보고서 분할 구분 체크
        private string UP_Get_BunHalCheck(string sCSCUQTY, string sCSSINQTY, string sPRECHMTQTY, string sCHMTQTY)
        {
            string sBUNHALGN = string.Empty;

            double dCSCUQTY_JanQty =  Math.Round( Convert.ToDouble(sCSCUQTY) - (Convert.ToDouble(sPRECHMTQTY) + Convert.ToDouble(sCHMTQTY)), 3);
            double dCSSINQTY_JanQty = Math.Round( Convert.ToDouble(sCSSINQTY) - (Convert.ToDouble(sPRECHMTQTY) + Convert.ToDouble(sCHMTQTY)), 3) ;

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

        #region  Description : 보세운송+반송화물 반출보고서 자료 생성
        private void UP_Get_DataBoSaeCHULF_UTT(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75BBF448", sDate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_758DH410", this.CBO01_EDIGJ.GetValue().ToString(), sDate,
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
            this.DbConnector.Attach("TY_P_UT_75BBF448", sDate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString());
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["CHCHULIL"].ToString());
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["CHMSNSEQ"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(dk.Rows[i]["CHHSNSEQ"].ToString());

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
                            double dCHMTQTY = Math.Round( Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) - (Convert.ToDouble(dk.Rows[i]["PRECHMTQTY"].ToString()) + Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString())), 3);
                            dCHMTQTY = dCHMTQTY + Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString());

                            this.DAT02_EDICHQTY.SetValue(Math.Round(dCHMTQTY * 1000, 0));
                        }
                        else
                        {
                            if (this.DAT02_EDIBUNHAL.GetValue().ToString() == "A")
                            {
                                this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) * 1000, 0));
                                this.DAT02_EDICHASU.SetValue("0");
                            }
                            else
                            {
                                this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CHMTQTY"].ToString()) * 1000, 0));
                            }
                        }
                        this.DAT02_EDICHCNT.SetValue("0");

                        if (this.DAT02_EDIBUNHAL.GetValue().ToString() == "A")
                        {
                            this.DAT02_EDINUQTY.SetValue(Convert.ToString(Math.Round(Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) * 1000, 0)));
                        }
                        else
                        {
                            this.DAT02_EDINUQTY.SetValue(Convert.ToString(Math.Round(Convert.ToDouble(dk.Rows[i]["PRECHMTQTY"].ToString()) * 1000, 0) +
                                                           Convert.ToDouble(this.DAT02_EDICHQTY.GetValue().ToString()))
                                                         );
                        }
                        this.DAT02_EDINUCNT.SetValue("0");

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["CHIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["CHBONSUN"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["CHHWAMUL"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["CHHWAJU"].ToString());

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
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),    
                                            this.DAT02_EDITIME.GetValue(),   
                                            this.DAT02_EDIJSGB.GetValue(),   
                                            this.DAT02_EDIBANGB.GetValue(),  
                                            this.DAT02_EDIBUNHAL.GetValue(),                                            
                                            this.DAT02_EDICHQTY.GetValue(),  
                                            this.DAT02_EDICHCNT.GetValue(),  
                                            this.DAT02_EDINUQTY.GetValue(), 
                                            this.DAT02_EDINUCNT.GetValue(),  
                                            this.DAT02_EDICHASU.GetValue(),                                             
                                            this.DAT02_EDIIPHANG.GetValue(), 
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            this.DAT02_EDIHWAJU.GetValue(),  
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
                        this.DbConnector.Attach("TY_P_UT_758G0422", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion

        #region  Description : BL분할 반입보고서 자료 생성
        private void UP_Get_DataBLBunHal_UTT(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75BBH449", sDate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString(), sDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y" && dt.Rows[i]["EDIDATE"].ToString() != "")
                    {
                        this.DbConnector.Attach("TY_P_UT_758DH410", this.CBO01_EDIGJ.GetValue().ToString(), 
                                                                    dt.Rows[i]["EDIDATE"].ToString(),
                                                                    dt.Rows[i]["EDIJUKHA"].ToString(),
                                                                    dt.Rows[i]["EDIBLMSN"].ToString(),
                                                                    dt.Rows[i]["EDIBLHSN"].ToString(),
                                                                    dt.Rows[i]["EDISINNO"].ToString()
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
            this.DbConnector.Attach("TY_P_UT_75BBH449", sDate, this.CBO01_EDIGJ.GetValue().ToString(), this.CBO01_EDIGJ.GetValue().ToString(), sDate);
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {

                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                        this.DAT02_EDIDATE.SetValue(sDate);
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["VSJUKHA"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IPBLNOSEQ"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(Set_Fill4(dk.Rows[i]["IPHBLNOSEQ"].ToString()));
                        this.DAT02_EDISINNO.SetValue(dk.Rows[i]["CSSINNO"].ToString());

                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(sDate.Substring(0, 4));
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(sDate.Substring(0, 4)))));

                        this.DAT02_EDITIME.SetValue(dk.Rows[i]["CHTIME"].ToString());
                        this.DAT02_EDIJSGB.SetValue("9");
                        this.DAT02_EDIBANGB.SetValue(dk.Rows[i]["CSBANGB"].ToString());

                        this.DAT02_EDICHASU.SetValue(dk.Rows[i]["EDICHASU"].ToString());

                        //분할유무 판단
                        this.DAT02_EDIBUNHAL.SetValue(UP_Get_BunHalCheck(dk.Rows[i]["CSCUQTY"].ToString(),
                                                                         dk.Rows[i]["CSSINQTY"].ToString(),
                                                                         "0",
                                                                         dk.Rows[i]["CSCUQTY"].ToString()
                                                                         ));
                        if (this.DAT02_EDIBUNHAL.GetValue().ToString() == "L")
                        {
                            double dCHMTQTY = Math.Round( Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) - (0 + Convert.ToDouble(dk.Rows[i]["CSCUQTY"].ToString())), 3) ;
                            dCHMTQTY = dCHMTQTY + Convert.ToDouble(dk.Rows[i]["CSCUQTY"].ToString());

                            this.DAT02_EDICHQTY.SetValue(Math.Round(dCHMTQTY * 1000, 0));
                        }
                        else
                        {
                            if (this.DAT02_EDIBUNHAL.GetValue().ToString() == "A")
                            {
                                this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CSSINQTY"].ToString()) * 1000, 0));
                                this.DAT02_EDICHASU.SetValue("0");
                            }
                            else
                            {
                                this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CSCUQTY"].ToString()) * 1000, 0));
                            }
                        }
                        this.DAT02_EDICHCNT.SetValue("0");

                        this.DAT02_EDINUQTY.SetValue(Convert.ToString(Math.Round( Convert.ToDouble("0") * 1000, 0) +
                                                                                  Convert.ToDouble(this.DAT02_EDICHQTY.GetValue().ToString()))
                                                     );
                        this.DAT02_EDINUCNT.SetValue("0");

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["IPIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["IPBONSUN"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["IPHWAMUL"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["IPHWAJU"].ToString());

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
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),    
                                            this.DAT02_EDITIME.GetValue(),   
                                            this.DAT02_EDIJSGB.GetValue(),   
                                            this.DAT02_EDIBANGB.GetValue(),  
                                            this.DAT02_EDIBUNHAL.GetValue(),                                            
                                            this.DAT02_EDICHQTY.GetValue(),  
                                            this.DAT02_EDICHCNT.GetValue(),  
                                            this.DAT02_EDINUQTY.GetValue(), 
                                            this.DAT02_EDINUCNT.GetValue(),  
                                            this.DAT02_EDICHASU.GetValue(),                                             
                                            this.DAT02_EDIIPHANG.GetValue(), 
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            this.DAT02_EDIHWAJU.GetValue(),  
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
                        this.DbConnector.Attach("TY_P_UT_758G0422", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion       

        #region  Description :  반출입 정정보고서 Mig 자료 생성
        private void UP_Set_MigDataREIPCHF_Common(string sDate )
        {
            string arrayREIPCHSF = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75BI3455", this.CBO01_EDIGJ.GetValue().ToString(), sDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                      TYEdiLayout.REBGM_0101  = dt.Rows[i]["EDIRECHIP"].ToString();
                      TYEdiLayout.REBGM_02011 = dt.Rows[i]["EDIRENO1"].ToString();       //N01
                      TYEdiLayout.REBGM_02012 = dt.Rows[i]["EDIRENO2"].ToString().Substring(2, 2);  //N02
                      TYEdiLayout.REBGM_02013 = dt.Rows[i]["EDIRENO3"].ToString();       //N03
                      TYEdiLayout.REUNH_0101 =  dt.Rows[i]["EDIRENO3"].ToString();         //N03

                      TYEdiLayout.REGIS_0101 = dt.Rows[i]["EDIREGIS"].ToString();        //신청구분    
                      TYEdiLayout.REDTM_01021 = dt.Rows[i]["EDIREDATE"].ToString();      //신청일자    
                    
                    TYEdiLayout.REQTY_0101 = dt.Rows[i]["EDIRECHASU"].ToString();     //정정차수    

                      TYEdiLayout.REFTX_0201 = dt.Rows[i]["EDIREFTX"].ToString();        //정정사유
                      TYEdiLayout.RENAD_0102 = dt.Rows[i]["EDIRENAD"].ToString();       //신청자성명
                      TYEdiLayout.RERFF_0102 = dt.Rows[i]["EDIREJUKHA"].ToString();      //적하목록번호
                      TYEdiLayout.RERFF_0103 = dt.Rows[i]["EDIREBLMSN"].ToString();      //msn
                      if (dt.Rows[i]["EDIREBLHSN"].ToString() != "")
                      {
                          TYEdiLayout.RERFF_0104 = Set_Fill4(dt.Rows[i]["EDIREBLHSN"].ToString());      //hsn    
                      }
            
                      TYEdiLayout.REUNT_0102 = dt.Rows[i]["EDIRENO3"].ToString();

                      if( dt.Rows[i]["EDIREGIS"].ToString() != "03" )
                      {

                           this.DbConnector.CommandClear();
                           this.DbConnector.Attach("TY_P_UT_73VBJ173", dt.Rows[i]["EDIGJ"].ToString(),
                                                                       dt.Rows[i]["EDIRENO1"].ToString(),
                                                                       dt.Rows[i]["EDIRENO2"].ToString(),
                                                                       dt.Rows[i]["EDIRENO3"].ToString(),
                                                                       dt.Rows[i]["EDIRECHASU"].ToString()
                                                                       );
                           DataTable detail = this.DbConnector.ExecuteDataTable();
                           if( detail.Rows.Count > 0 )
                           {                               
                               for (int j = 0; j < detail.Rows.Count; j++)
                               {
                                   if (j <= detail.Rows.Count - 1)
                                   {
                                       arrayREIPCHSF = arrayREIPCHSF + detail.Rows[j]["EDISRELIN"].ToString() + ";" + detail.Rows[j]["EDISREERRFT"].ToString() + ";" + detail.Rows[j]["EDISRECHAFT"].ToString();
                                   }
                                   else
                                   {
                                       arrayREIPCHSF = arrayREIPCHSF + detail.Rows[j]["EDISRELIN"].ToString() + ";" + detail.Rows[j]["EDISREERRFT"].ToString() + ";" + detail.Rows[j]["EDISRECHAFT"].ToString() + ",";
                                   }
                               }
                           }
                      }

                      UP_Set_MigData_REIPCHFAdd(arrayREIPCHSF);

                      UP_Set_XmlData_REIPCHFAdd(arrayREIPCHSF);

                      arrayREIPCHSF = "";
                }
            }
        }
        #endregion

        #region  Description : 반출입정정신고 미그 데이타 조합
        private void UP_Set_MigData_REIPCHFAdd(string arrayREIPCHSF)
        {
            int iCnt = 0;

            UP_Set_MigDataMix_CUSDMR();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.REUNH_PRN + TYEdiLayout.REBGM_PRN + TYEdiLayout.REGIS_PRN + TYEdiLayout.REDTM_PRN + TYEdiLayout.RELOC_PRN +
                                    TYEdiLayout.REQTY_PRN + TYEdiLayout.REFTX_PRN + TYEdiLayout.RENAD_PRN + TYEdiLayout.RERFF_PRN;

            UP_Set_MigDataMix_ITEMADD(arrayREIPCHSF);
            
            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1 + TYEdiLayout.REUNS_PRN + TYEdiLayout.REUNT_PRN; 

            string sChar = "'";
            string[] Temps = TYEdiLayout.TEMP_REC1.Split(new string[] { sChar }, StringSplitOptions.None);
            iCnt = Temps.Length - 1;

            TYEdiLayout.REUNT_0101 = string.Format("{0,-6:G}", iCnt.ToString());

            UP_Set_MigDataMix_CUSDMR();

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.REUNH_PRN + TYEdiLayout.REBGM_PRN + TYEdiLayout.REGIS_PRN + TYEdiLayout.REDTM_PRN + TYEdiLayout.RELOC_PRN +
                                    TYEdiLayout.REQTY_PRN + TYEdiLayout.REFTX_PRN + TYEdiLayout.RENAD_PRN + TYEdiLayout.RERFF_PRN;

            UP_Set_MigDataMix_ITEMADD(arrayREIPCHSF);

            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1 + TYEdiLayout.REUNS_PRN + TYEdiLayout.REUNT_PRN; 

            //공백제거
            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1.Replace(" ", "");

            UP_SetDataTable_RowAdd(TYEdiLayout.TEMP_REC1);
        }

        private void UP_Set_MigDataMix_ITEMADD(string arrayREIPCHSF)
        {
            if (arrayREIPCHSF.Length > 0)
            {
                string[] ArrayLine = arrayREIPCHSF.Split(',');

                if (ArrayLine.Length > 0)
                {
                    for (int i = 0; i < ArrayLine.Length; i++)
                    {
                        string[] ArrayFiled = ArrayLine[i].Split(';');

                        if (ArrayFiled.Length > 0)
                        {
                            for (int j = 0; j < ArrayFiled.Length; j++)
                            {
                                if (j == 0)
                                {
                                    TYEdiLayout.RELIN_0101 = ArrayFiled[j].ToString();
                                }
                                else if (j == 1)
                                {
                                    TYEdiLayout.REFTX1_0101 = ArrayFiled[j].ToString();
                                }
                                else if (j == 2)
                                {
                                    TYEdiLayout.REFTX2_0101 = ArrayFiled[j].ToString();
                                }
                            }
                        }

                        //---항목번호-------//
                        TYEdiLayout.RELIN_PRN = TYEdiLayout.RELIN_PRN1 + TYEdiLayout.RELIN_PRN2 + TYEdiLayout.RELIN_PRN3 +
                                                TYEdiLayout.RELIN_PRN4 + TYEdiLayout.RELIN_0101 + TYEdiLayout.RELIN_PRN5;
                        //---정정전내역-------//
                        TYEdiLayout.REFTX1_PRN = TYEdiLayout.REFTX1_PRN1 + TYEdiLayout.REFTX1_PRN2 + TYEdiLayout.REFTX1_PRN3 +
                                                 TYEdiLayout.REFTX1_PRN4 + TYEdiLayout.REFTX1_PRN5 + TYEdiLayout.REFTX1_PRN6 +
                                                 TYEdiLayout.REFTX1_0101 + TYEdiLayout.REFTX1_PRN7;
                        //---정정후내역-------//
                        TYEdiLayout.REFTX2_PRN = TYEdiLayout.REFTX2_PRN1 + TYEdiLayout.REFTX2_PRN2 + TYEdiLayout.REFTX2_PRN3 +
                                                 TYEdiLayout.REFTX2_PRN4 + TYEdiLayout.REFTX2_PRN5 + TYEdiLayout.REFTX2_PRN6 +
                                                 TYEdiLayout.REFTX2_0101 + TYEdiLayout.REFTX2_PRN7;

                        TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1 + TYEdiLayout.RELIN_PRN + TYEdiLayout.REFTX1_PRN + TYEdiLayout.REFTX2_PRN;
                    }
                }
            }
        }

        private void UP_Set_MigDataMix_CUSDMR()
        {
              //---발신인참조번호-------//
             TYEdiLayout.REUNH_PRN = TYEdiLayout.REUNH_PRN1 + TYEdiLayout.REUNH_PRN2 + TYEdiLayout.REUNH_0101 + 
                                     TYEdiLayout.REUNH_PRN3 + TYEdiLayout.REUNH_PRN4 + TYEdiLayout.REUNH_PRN5 + 
                                     TYEdiLayout.REUNH_PRN6 + TYEdiLayout.REUNH_PRN7 + TYEdiLayout.REUNH_PRN8 + 
                                     TYEdiLayout.REUNH_PRN9 + TYEdiLayout.REUNH_PRN10 + TYEdiLayout.REUNH_PRN11;
             //---반출입신고번호-------//
             TYEdiLayout.REBGM_PRN = TYEdiLayout.REBGM_PRN1 + TYEdiLayout.REBGM_PRN2 + TYEdiLayout.REBGM_0101 + TYEdiLayout.REBGM_PRN3 + 
                                     TYEdiLayout.REBGM_02011 + TYEdiLayout.REBGM_02012 + TYEdiLayout.REBGM_02013 + 
                                     TYEdiLayout.REBGM_PRN4;
             //---신청구분-------//
             TYEdiLayout.REGIS_PRN = TYEdiLayout.REGIS_PRN1 + TYEdiLayout.REGIS_PRN2 + TYEdiLayout.REGIS_0101 + 
                                     TYEdiLayout.REGIS_PRN3 + TYEdiLayout.REGIS_PRN4 + TYEdiLayout.REGIS_PRN5 + 
                                     TYEdiLayout.REGIS_PRN6 + TYEdiLayout.REGIS_PRN7;
             //---신청일자-------//
             TYEdiLayout.REDTM_PRN = TYEdiLayout.REDTM_PRN1 + TYEdiLayout.REDTM_PRN2 + TYEdiLayout.REDTM_PRN3 + TYEdiLayout.REDTM_PRN4 + 
                                     TYEdiLayout.REDTM_01021 + TYEdiLayout.REDTM_PRN5 + TYEdiLayout.REDTM_PRN6 + TYEdiLayout.REDTM_PRN7;
             
             //---신고세관/과-------//
             TYEdiLayout.RELOC_PRN = TYEdiLayout.RELOC_PRN1 + TYEdiLayout.RELOC_PRN2 + TYEdiLayout.RELOC_PRN3 + TYEdiLayout.RELOC_PRN4 + TYEdiLayout.RELOC_PRN5 + 
                                     TYEdiLayout.RELOC_PRN6 + TYEdiLayout.RELOC_PRN7 + TYEdiLayout.RELOC_PRN8 + TYEdiLayout.RELOC_PRN9 + TYEdiLayout.RELOC_PRN10 + 
                                     TYEdiLayout.RELOC_PRN11 + TYEdiLayout.RELOC_PRN12 + TYEdiLayout.RELOC_PRN13 + TYEdiLayout.RELOC_PRN14 + TYEdiLayout.RELOC_PRN15 + 
                                     TYEdiLayout.RELOC_PRN16;
             //---정정차수-------//
             TYEdiLayout.REQTY_PRN = TYEdiLayout.REQTY_PRN1 + TYEdiLayout.REQTY_PRN2 + TYEdiLayout.REQTY_PRN3 + TYEdiLayout.REQTY_PRN4 + 
                                     TYEdiLayout.REQTY_0101 + TYEdiLayout.REQTY_PRN5;
 
             //---정정사유-------//
             TYEdiLayout.REFTX_PRN = TYEdiLayout.REFTX_PRN1 + TYEdiLayout.REFTX_PRN2 + TYEdiLayout.REFTX_PRN3 + 
                                     TYEdiLayout.REFTX_PRN4 + TYEdiLayout.REFTX_PRN5 + TYEdiLayout.REFTX_PRN6 + 
                                     TYEdiLayout.REFTX_0201 + TYEdiLayout.REFTX_PRN7;
             //---신청자------//
             TYEdiLayout.RENAD_PRN = TYEdiLayout.RENAD_PRN1 + TYEdiLayout.RENAD_PRN2 + TYEdiLayout.RENAD_PRN3 + 
                                     TYEdiLayout.RENAD_PRN4 + TYEdiLayout.RENAD_PRN5 + TYEdiLayout.RENAD_0102 + 
                                     TYEdiLayout.RENAD_PRN6;
             //---화물관리번호------//
             TYEdiLayout.RERFF_PRN = TYEdiLayout.RERFF_PRN1 + TYEdiLayout.RERFF_PRN2 + TYEdiLayout.RERFF_PRN3 + 
                                     TYEdiLayout.RERFF_PRN4 + TYEdiLayout.RERFF_0102 + TYEdiLayout.RERFF_PRN5 + 
                                     TYEdiLayout.RERFF_0103 + TYEdiLayout.RERFF_PRN6 + TYEdiLayout.RERFF_0104 + 
                                     TYEdiLayout.RERFF_PRN7;            
           
             //---부분제어-------//
             TYEdiLayout.REUNS_PRN = TYEdiLayout.REUNS_PRN1 + TYEdiLayout.REUNS_PRN2 + TYEdiLayout.REUNS_PRN3 + TYEdiLayout.REUNS_PRN4;
        
             //---전자문서내의전송항목수/전자문서참조번호-------//
             TYEdiLayout.REUNT_PRN = TYEdiLayout.REUNT_PRN1 + TYEdiLayout.REUNT_PRN2 + TYEdiLayout.REUNT_0101 + 
                                     TYEdiLayout.REUNT_PRN3 + TYEdiLayout.REUNT_0102 + TYEdiLayout.REUNT_PRN4;
        }
        #endregion    
   
        #region  Description : 반출입정정신청서 XML 데이타 조합
        private void UP_Set_XmlData_REIPCHFAdd(string sAmendmentList)
        {
            //if (sAmendmentList.Length > 0)
            //{
                string[] _ArrayItem = sAmendmentList.Split(',');

            string sTypeCode = string.Empty;
            string xml = string.Empty;

            if (TYEdiLayout.REBGM_0101 == "5LC" || TYEdiLayout.REBGM_0101 == "5LD")
            {
                sTypeCode = TYEdiLayout.REBGM_0101 == "5LC" ? "GOVCBR5LC" : "GOVCBR5LD";

                xml = TYEdiLayout.UP_Get_XmlGOVCBR5LCD(TYEdiLayout.REBGM_02011 + TYEdiLayout.REBGM_02012 + TYEdiLayout.REBGM_02013,                   //제출번호  
                                                              TYEdiLayout.REDTM_01021,        //신청일자                              
                                                              sTypeCode,             //문서형태구분
                                                              TYEdiLayout.REQTY_0101,            //신청차수
                                                              TYEdiLayout.REGIS_0101,          //신청구분(01:정정, 03:삭제)
                                                              TYEdiLayout.REFTX_0201,               //정정사유  
                                                              TYEdiLayout.RENAD_0102,        //신청자 성명
                                                              TYEdiLayout.RERFF_0102 + TYEdiLayout.RERFF_0103 + TYEdiLayout.RERFF_0104, //화물관리번호
                                                              _ArrayItem             //정정항목
                                                            );
            }
            else
            {
                sTypeCode = TYEdiLayout.REBGM_0101 == "004" ? "GOVCBR004" : "GOVCBR005";

                xml = TYEdiLayout.UP_Get_XmlGOVCBR0045(TYEdiLayout.REBGM_02011 + TYEdiLayout.REBGM_02012 + TYEdiLayout.REBGM_02013,                   //제출번호  
                                              TYEdiLayout.REDTM_01021,        //신청일자                              
                                              sTypeCode,             //문서형태구분
                                              TYEdiLayout.REQTY_0101,            //신청차수
                                              TYEdiLayout.REGIS_0101,          //신청구분(01:정정, 03:삭제)
                                              TYEdiLayout.REFTX_0201,               //정정사유  
                                              TYEdiLayout.RENAD_0102,        //신청자 성명                                              
                                              _ArrayItem             //정정항목
                                            );

            }

            string sFileName = TYEdiLayout.REDTM_01021+TYEdiLayout.REBGM_02011 + TYEdiLayout.REBGM_02012 + TYEdiLayout.REBGM_02013 + ".xml";

                this.UP_Set_XmlFileCreate(xml, sFileName);             
            //}           
        }
        #endregion

        #region  Description : 반출기간연장신청서 Mig 자료 생성
        private void UP_Set_MigDataEXTENDF_UTT(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_8688W192", this.CBO01_EDIGJ.GetValue().ToString(), sDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TYEdiLayout.EXUNT_0201 = dt.Rows[i]["EDINO3"].ToString();
                    TYEdiLayout.EXUNH_0101 = TYEdiLayout.EXUNT_0201;

                    TYEdiLayout.EXBGM_02011 = dt.Rows[i]["EDISAUPNO"].ToString();
                    TYEdiLayout.EXBGM_02012 = dt.Rows[i]["EDIYEAR"].ToString().Substring(2, 2);
                    TYEdiLayout.EXBGM_02013 = dt.Rows[i]["EDISEQ"].ToString();

                    TYEdiLayout.EXBGM_0202 = dt.Rows[i]["EDIJSGB"].ToString();                    
                    TYEdiLayout.EXGIS_0101 = dt.Rows[i]["EDIAPPGUBN"].ToString();
                    TYEdiLayout.EXGIS2_0101 = dt.Rows[i]["EDIEXTGUBN"].ToString();
                    TYEdiLayout.EXDTM_0101 = dt.Rows[i]["EDIDATE"].ToString();

                    TYEdiLayout.EXDTM1_0101 = dt.Rows[i]["EDIEXTSDATE"].ToString();
                    TYEdiLayout.EXDTM2_0101 = dt.Rows[i]["EDIEXTEDATE"].ToString();

                    TYEdiLayout.EXLOC_0101 = dt.Rows[i]["EDIWHNUMBER"].ToString();

                    TYEdiLayout.EXFTX_0101 = dt.Rows[i]["EDIREASON"].ToString();
                    TYEdiLayout.EXNAD_0101 = dt.Rows[i]["EDIAPPNAME"].ToString();

                    TYEdiLayout.EXRFF_0102 = dt.Rows[i]["EDIJUKHA"].ToString();
                    TYEdiLayout.EXRFF_0103 = dt.Rows[i]["EDIBLMSN"].ToString();
                    TYEdiLayout.EXRFF_0104 = dt.Rows[i]["EDIBLHSN"].ToString();

                    TYEdiLayout.EXRFF2_0101 = dt.Rows[i]["EDICSSINNO"].ToString();

                    UP_Set_MigData_CHEXTENDAdd();

                    UP_Set_XmlData_CHEXTENDAdd();

                }
            }
        }
        #endregion

        #region  Description : 반출기간연장신청서 Mig 자료 생성
        private void UP_Set_MigData_CHEXTENDAdd()
        {
            int iCnt = 0;

            UP_Set_MigDataMix_EXCUSDMR();

            if (TYEdiLayout.EXGIS2_0101 == "99")
            {
                TYEdiLayout.TEMP_REC1 = TYEdiLayout.EXUNH_PRN + TYEdiLayout.EXBGM_PRN + TYEdiLayout.EXGIS_PRN + TYEdiLayout.EXGIS2_PRN + TYEdiLayout.EXDTM_PRN +
                                        TYEdiLayout.EXDTM1_PRN + TYEdiLayout.EXDTM2_PRN + TYEdiLayout.EXLOC_PRN + TYEdiLayout.EXLOC1_PRN + TYEdiLayout.EXFTX_PRN +
                                        TYEdiLayout.EXNAD_PRN + TYEdiLayout.EXRFF_PRN + TYEdiLayout.EXRFF2_PRN + TYEdiLayout.EXUNS_PRN + TYEdiLayout.EXUNT_PRN;
            }
            else
            {
                TYEdiLayout.TEMP_REC1 = TYEdiLayout.EXUNH_PRN + TYEdiLayout.EXBGM_PRN + TYEdiLayout.EXGIS_PRN + TYEdiLayout.EXGIS2_PRN + TYEdiLayout.EXDTM_PRN +
                                        TYEdiLayout.EXLOC_PRN + TYEdiLayout.EXLOC1_PRN + TYEdiLayout.EXFTX_PRN +
                                        TYEdiLayout.EXNAD_PRN + TYEdiLayout.EXRFF_PRN + TYEdiLayout.EXRFF2_PRN + TYEdiLayout.EXUNS_PRN + TYEdiLayout.EXUNT_PRN;
            }

            string sChar = "'";
            string[] Temps = TYEdiLayout.TEMP_REC1.Split(new string[] { sChar }, StringSplitOptions.None);
            iCnt = Temps.Length - 1;

            TYEdiLayout.EXUNT_0101 = string.Format("{0,-6:G}", iCnt.ToString());

            UP_Set_MigDataMix_EXCUSDMR();

            if (TYEdiLayout.EXGIS2_0101 == "99")
            {
                TYEdiLayout.TEMP_REC1 = TYEdiLayout.EXUNH_PRN + TYEdiLayout.EXBGM_PRN + TYEdiLayout.EXGIS_PRN + TYEdiLayout.EXGIS2_PRN + TYEdiLayout.EXDTM_PRN +
                                        TYEdiLayout.EXDTM1_PRN + TYEdiLayout.EXDTM2_PRN + TYEdiLayout.EXLOC_PRN + TYEdiLayout.EXLOC1_PRN + TYEdiLayout.EXFTX_PRN +
                                        TYEdiLayout.EXNAD_PRN + TYEdiLayout.EXRFF_PRN + TYEdiLayout.EXRFF2_PRN + TYEdiLayout.EXUNS_PRN + TYEdiLayout.EXUNT_PRN;
            }
            else
            {
                TYEdiLayout.TEMP_REC1 = TYEdiLayout.EXUNH_PRN + TYEdiLayout.EXBGM_PRN + TYEdiLayout.EXGIS_PRN + TYEdiLayout.EXGIS2_PRN + TYEdiLayout.EXDTM_PRN +
                                        TYEdiLayout.EXLOC_PRN + TYEdiLayout.EXLOC1_PRN + TYEdiLayout.EXFTX_PRN +
                                        TYEdiLayout.EXNAD_PRN + TYEdiLayout.EXRFF_PRN + TYEdiLayout.EXRFF2_PRN + TYEdiLayout.EXUNS_PRN + TYEdiLayout.EXUNT_PRN;
            }

            //공백제거
            TYEdiLayout.TEMP_REC1 = TYEdiLayout.TEMP_REC1.Replace(" ", "");

            UP_SetDataTable_RowAdd(TYEdiLayout.TEMP_REC1);

        }

        private void UP_Set_MigDataMix_EXCUSDMR()
        {
             //----------------(35) ---------------------'
            TYEdiLayout.EXUNH_PRN = TYEdiLayout.EXUNH_PRN1 + TYEdiLayout.EXUNH_PRN2 + TYEdiLayout.EXUNH_0101 + TYEdiLayout.EXUNH_PRN3 +
                                  TYEdiLayout.EXUNH_PRN4 + TYEdiLayout.EXUNH_PRN5 + TYEdiLayout.EXUNH_PRN6 + TYEdiLayout.EXUNH_PRN7 +
                                  TYEdiLayout.EXUNH_PRN8 + TYEdiLayout.EXUNH_PRN9 + TYEdiLayout.EXUNH_PRN10 + TYEdiLayout.EXUNH_PRN11;

            //----------------(29) 신고번호 ---------------------//
            TYEdiLayout.EXBGM_PRN = TYEdiLayout.EXBGM_PRN1 + TYEdiLayout.EXBGM_PRN2 + TYEdiLayout.EXBGM_PRN3 + TYEdiLayout.EXBGM_PRN4 +
                                     TYEdiLayout.EXBGM_02011 + TYEdiLayout.EXBGM_02012 + TYEdiLayout.EXBGM_02013 +
                                     TYEdiLayout.EXBGM_PRN5 + TYEdiLayout.EXBGM_0202 + TYEdiLayout.EXBGM_PRN6;

            //---------------- 신청구분 ---------------------//
            TYEdiLayout.EXGIS_PRN = TYEdiLayout.EXGIS_PRN1 + TYEdiLayout.EXGIS_PRN2 + TYEdiLayout.EXGIS_0101 + 
                                    TYEdiLayout.EXGIS_PRN3 + TYEdiLayout.EXGIS_PRN4 + TYEdiLayout.EXGIS_PRN5 +
                                    TYEdiLayout.EXGIS_PRN6 + TYEdiLayout.EXGIS_PRN7;

            //*-------------------------- 연장기간구분 --------------*
            TYEdiLayout.EXGIS2_PRN = TYEdiLayout.EXGIS2_PRN1 + TYEdiLayout.EXGIS2_PRN2 + TYEdiLayout.EXGIS2_0101 + 
                                 TYEdiLayout.EXGIS2_PRN3 + TYEdiLayout.EXGIS2_PRN4 + TYEdiLayout.EXGIS2_PRN5 +
                                 TYEdiLayout.EXGIS2_PRN6 + TYEdiLayout.EXGIS2_PRN7;
            //*-------------------------- 신청일자 ------------------*
            TYEdiLayout.EXDTM_PRN = TYEdiLayout.EXDTM_PRN1 + TYEdiLayout.EXDTM_PRN2 + TYEdiLayout.EXDTM_PRN3 + TYEdiLayout.EXDTM_PRN4 +
                                    TYEdiLayout.EXDTM_0101 + TYEdiLayout.EXDTM_PRN6 + TYEdiLayout.EXDTM_PRN7 + TYEdiLayout.EXDTM_PRN8;

            //*-------------------------- 연장기간시작일자 ------------------*
            TYEdiLayout.EXDTM1_PRN = TYEdiLayout.EXDTM1_PRN1 + TYEdiLayout.EXDTM1_PRN2 + TYEdiLayout.EXDTM1_PRN3 + TYEdiLayout.EXDTM1_PRN4 +
                                     TYEdiLayout.EXDTM1_0101 + TYEdiLayout.EXDTM1_PRN6 + TYEdiLayout.EXDTM1_PRN7 + TYEdiLayout.EXDTM1_PRN8;

            //*-------------------------- 연장기간종료일자 ------------------*
             TYEdiLayout.EXDTM2_PRN =  TYEdiLayout.EXDTM2_PRN1 + TYEdiLayout.EXDTM2_PRN2 + TYEdiLayout.EXDTM2_PRN3 + TYEdiLayout.EXDTM2_PRN4 +
                                       TYEdiLayout.EXDTM2_0101 + TYEdiLayout.EXDTM2_PRN6 + TYEdiLayout.EXDTM2_PRN7 + TYEdiLayout.EXDTM2_PRN8;
             
             //*-------------------------- 장치장소 ------------------*        
             TYEdiLayout.EXLOC_PRN = TYEdiLayout.EXLOC_PRN1 + TYEdiLayout.EXLOC_PRN2 + TYEdiLayout.EXLOC_PRN3 +
                                      TYEdiLayout.EXLOC_PRN4 + TYEdiLayout.EXLOC_0101 + TYEdiLayout.EXLOC_PRN6 +
                                      TYEdiLayout.EXLOC_PRN7 + TYEdiLayout.EXLOC_PRN8 + TYEdiLayout.EXLOC_PRN9 + TYEdiLayout.EXLOC_PRN10;

             //*-------------------------- 신고세관 ------------------*        
             TYEdiLayout.EXLOC1_PRN = TYEdiLayout.EXLOC1_PRN1 + TYEdiLayout.EXLOC1_PRN2 + TYEdiLayout.EXLOC1_PRN3 + TYEdiLayout.EXLOC1_PRN4 +
                                      TYEdiLayout.EXLOC1_PRN5 + TYEdiLayout.EXLOC1_PRN6 + TYEdiLayout.EXLOC1_PRN7 + TYEdiLayout.EXLOC1_PRN8 +
                                      TYEdiLayout.EXLOC1_PRN9 + TYEdiLayout.EXLOC1_PRN10 + TYEdiLayout.EXLOC1_PRN11 + TYEdiLayout.EXLOC1_PRN12 +
                                      TYEdiLayout.EXLOC1_PRN13 + TYEdiLayout.EXLOC1_PRN14 + TYEdiLayout.EXLOC1_PRN15 + TYEdiLayout.EXLOC1_PRN16;

            //*-------------------------- 연장사유 --------------*
             TYEdiLayout.EXFTX_PRN = TYEdiLayout.EXFTX_PRN1 + TYEdiLayout.EXFTX_PRN2 + TYEdiLayout.EXFTX_PRN3 + TYEdiLayout.EXFTX_PRN4 +
                                     TYEdiLayout.EXFTX_PRN5 + TYEdiLayout.EXFTX_PRN6 + TYEdiLayout.EXFTX_0101 + TYEdiLayout.EXFTX_PRN7;

            //*--------------------------신청자 ------------------*
             TYEdiLayout.EXNAD_PRN = TYEdiLayout.EXNAD_PRN1 + TYEdiLayout.EXNAD_PRN2 + TYEdiLayout.EXNAD_PRN3 + TYEdiLayout.EXNAD_PRN4 +
                                     TYEdiLayout.EXNAD_PRN5 + TYEdiLayout.EXNAD_0101 + TYEdiLayout.EXNAD_PRN6;

            //*-------------------------- 화물관리번호 --------------*
            TYEdiLayout.EXRFF_PRN = TYEdiLayout.EXRFF_PRN1 + TYEdiLayout.EXRFF_PRN2 + TYEdiLayout.EXRFF_PRN3 +
                                    TYEdiLayout.EXRFF_PRN4 + TYEdiLayout.EXRFF_0102 + TYEdiLayout.EXRFF_PRN5 +
                                    TYEdiLayout.EXRFF_0103 + TYEdiLayout.EXRFF_PRN6 + TYEdiLayout.EXRFF_0104 +
                                    TYEdiLayout.EXRFF_PRN7;

            //*--------------------------- 수입신고번호 --------------*
            TYEdiLayout.EXRFF2_PRN = TYEdiLayout.EXRFF2_PRN1 + TYEdiLayout.EXRFF2_PRN2 + TYEdiLayout.EXRFF2_PRN3 +
                                     TYEdiLayout.EXRFF2_PRN4 + TYEdiLayout.EXRFF2_0101 + TYEdiLayout.EXRFF2_PRN5;

            //*----------------전자문서내 세부사항부분과요약부분 --------------*
            TYEdiLayout.EXUNS_PRN = TYEdiLayout.EXUNS_PRN1 + TYEdiLayout.EXUNS_PRN2 + TYEdiLayout.EXUNS_PRN3 +
                                    TYEdiLayout.EXUNS_PRN4;

            //*-----------------전자문서의종료를나타내는전송항목-----------------------------*
            TYEdiLayout.EXUNT_PRN = TYEdiLayout.EXUNT_PRN1 + TYEdiLayout.EXUNT_PRN2 + TYEdiLayout.EXUNT_0101 + 
                                    TYEdiLayout.EXUNT_PRN3 + TYEdiLayout.EXUNT_0201 + TYEdiLayout.EXUNT_PRN4;
        }
        #endregion

        #region  Description : 반출기간연장신청서 XML 데이타 조합
        private void UP_Set_XmlData_CHEXTENDAdd()
        {
            string xml = TYEdiLayout.UP_Get_XmlGOVCBR5HM("110D9",                      //신고세관
                                                          TYEdiLayout.EXBGM_0202,         //전자문서 기능(9:원본, 35:재전송)
                                                          TYEdiLayout.EXBGM_02011+TYEdiLayout.EXBGM_02012+TYEdiLayout.EXBGM_02013,                   //제출번호
                                                          TYEdiLayout.EXDTM_0101,        //신청일자
                                                          "GOVCBR5HM",             //문서형태구분
                                                          TYEdiLayout.EXGIS_0101,  //신청구분
                                                          TYEdiLayout.EXFTX_0101,                 //연장사유
                                                          TYEdiLayout.EXGIS2_0101,          //연장기간구분(A:30일, B:60일. Z:연장기간입력)
                                                          TYEdiLayout.EXDTM1_0101,       //연장기간시작일
                                                          TYEdiLayout.EXDTM2_0101,          //연장기간종료일
                                                          TYEdiLayout.EXLOC_0101,             //장치장소
                                                          TYEdiLayout.EXRFF2_0101,      //수입신고번호
                                                          TYEdiLayout.EXNAD_0101,           //신청자 성명
                                                          TYEdiLayout.EXRFF_0102 + TYEdiLayout.EXRFF_0103 + TYEdiLayout.EXRFF_0104   //화물관리번호
                                                         );
            string sFileName = TYEdiLayout.EXDTM_0101 + TYEdiLayout.EXBGM_02011 + TYEdiLayout.EXBGM_02012 + TYEdiLayout.EXBGM_02013 + ".xml";

            this.UP_Set_XmlFileCreate(xml, sFileName);
        }
        #endregion

        #region  Description : 반출보고서 순번 생성
        private string UP_Get_ChulSeq(string sYear)
        {
            string sSEQCH = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_758E2416", sYear);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sSEQCH =  Convert.ToString( Convert.ToInt16(dt.Rows[0]["SEQCH"].ToString()) + 1);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_758E1419", sSEQCH, sYear);
                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                sSEQCH = "1";
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_758E4420", sYear, sSEQCH);
                this.DbConnector.ExecuteNonQuery();
            }

            return sSEQCH;
        }
        #endregion

        #region  Description : 반출보고서 XML 데이타 조합
        private void UP_Set_XmlData_CHULAdd()
        {           
            string xml = TYEdiLayout.UP_Get_XmlGOVCBR6NB("9", TYEdiLayout.CHBGM_02011 + TYEdiLayout.CHBGM_02012 + TYEdiLayout.CHBGM_02013, TYEdiLayout.CHCNT_0101, TYEdiLayout.CHMEA_0202,
                                                         TYEdiLayout.CHGIS_0101, TYEdiLayout.CHGIS2_0101, "", TYEdiLayout.CHRFF2_0101, TYEdiLayout.CHMEA3_0101, "", TYEdiLayout.CHRFF_0102 + TYEdiLayout.CHRFF_0103 + TYEdiLayout.CHRFF_0104,
                                                         TYEdiLayout.CHDTM_01021 + TYEdiLayout.CHDTM_01022                                                          
                                                         );
            string sFileName = TYEdiLayout.CHDTM_01021 + TYEdiLayout.CHBGM_02011 + TYEdiLayout.CHBGM_02012 + TYEdiLayout.CHBGM_02013 + ".xml";

            this.UP_Set_XmlFileCreate(xml, sFileName);
        }
        #endregion

        #region  Description : 반출통고목록보고서 Mig 자료 생성
        private void UP_Set_MigDataCHNOTEMF_Common(string sDate)
        {
            string sEDNJUKHA = string.Empty;



            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A6192598", sDate, sDate);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 클리어
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDNRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_A619B599",  TYUserInfo.EmpNo,
                                                                    dt.Rows[i]["EDNDATE"].ToString(),
                                                                    dt.Rows[i]["EDNSEQ"].ToString(),
                                                                    dt.Rows[i]["EDNJSGB"].ToString()
                                                                    );
                    }
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }                

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sEDNJUKHA = dt.Rows[i]["EDNBLHSN"].ToString() != "0000" ? dt.Rows[i]["EDNJUKHA"].ToString() + dt.Rows[i]["EDNBLMSN"].ToString() + dt.Rows[i]["EDNBLHSN"].ToString() : dt.Rows[i]["EDNJUKHA"].ToString() + dt.Rows[i]["EDNBLMSN"].ToString();

                    string xml = TYEdiLayout.UP_Get_XmlGOVCBR5II( "110D9",          //신고세관/과
                                                                  dt.Rows[i]["EDNJSGB"].ToString(),  //전자문서 기능(9:원본, 35:재전송)
                                                                  sEDNJUKHA, //-화물관리번호
                                                                  dt.Rows[i]["EDNCHDATE"].ToString(), //반출통고일자
                                                                  dt.Rows[i]["EDNIPQTY"].ToString(),  //반입중량
                                                                  dt.Rows[i]["EDNCNT"].ToString(),          //반입개수
                                                                  "GOVCBR5II",                             //문서형태구분
                                                                  dt.Rows[i]["EDNDHDATE"].ToString(),           //체화예정일자
                                                                  dt.Rows[i]["EDNBOLOC"].ToString(),          //반입장소
                                                                  dt.Rows[i]["EDNHWAJUNM"].ToString(),           //  화주명
                                                                  dt.Rows[i]["EDNLOADCODE"].ToString(),                     //화주 도로명코드
                                                                  dt.Rows[i]["EDNDETAILADDR"].ToString(),                     //화주 상세주소
                                                                  dt.Rows[i]["EDNPOST"].ToString(),                     //화주 우편번호
                                                                  dt.Rows[i]["EDNBUILDNUM"].ToString(),                     //화주 건물관리번호
                                                                  dt.Rows[i]["EDNADDR"].ToString(),                     //화주 기본주소
                                                                  dt.Rows[i]["EDNHJBIRTH"].ToString(),                     //화주 생년월일
                                                                  dt.Rows[i]["EDNTEL"].ToString(),                     //화주 전화번호
                                                                  dt.Rows[i]["EDNBLNO"].ToString()                     //B/L번호
                                         );

                    string sFileName = sEDNJUKHA + ".xml";

                    this.UP_Set_XmlFileCreate(xml, sFileName); 
                }
            }

        }
        #endregion

        #region  Description : Winmate 송신 이벤트
        private void UP_WinmateSend()
        {
            System.Diagnostics.Process ps = new System.Diagnostics.Process();

            ps.StartInfo.FileName = "hermes.exe"; // 프로그램 파일명            
            ps.StartInfo.Arguments = "-m2"; ; // 넘길 파라미터
            ps.StartInfo.UseShellExecute = true;
            ps.StartInfo.WorkingDirectory = Get_WinmdatePath() + "\\BIN\\"; //프로그램이 있는 디렉토리 위치
            ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            //실행에 필요한 설정 후 외부 프로그램을 실행.
            ps.Start();

            return;
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

            string[] _FileList = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\upload\\");            

            if (_FileList.Length > 0)
            {               
                //xml 파일 총건수
                iSendTotal = _FileList.Length;

                pgBar.Maximum = iSendTotal;                

                //공인인증서 로그인
                string Login = LoginSecuMdle(Get_KCSAPI4LoginId(), Get_KCSAPI4DocUserId());                

                if (Login.Trim().Substring(0,4) == "C200")
                {                   

                    for (int i = 0; i < _FileList.Length; i++)
                    {
                        sFileName = _FileList[i].ToString();
                        iPoint = sFileName.IndexOf(".");
                        sConversationID = sFileName.Substring(iPoint - 18, 18);

                        sDocCode = UP_Get_XmlToDocCode(sFileName);
                        
                        //통관관련 문서 송신
                        ret = TrsmDocCscl(Get_KCSAPI4LoginId(), Get_KCSAPI4DocUserId(), sDocCode, sConversationID, sFileName);

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
                    string LoginOut = LogoutSecuMdle();

                    //전송한 파일 이동
                    UP_DirFileMove_KCSAPI4();

                    string sShowMsg = "전송총건수:" + iSendTotal.ToString() + "  정상건수:" + iOkCnt.ToString() + "   오류건수:" + iErrorCnt.ToString();

                    this.ShowCustomMessage(sShowMsg, "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    this.ShowCustomMessage(Login, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }            

        }
        #endregion

        #region  Description : KCSAPI4 upload 파일 일괄 삭제 및 이동 이벤트
        private void UP_DirFileDelete_KCSAPI4()
        {            
            string sFileName = string.Empty;

            if (System.IO.Directory.Exists(ConstKCSAPIPath + "\\upload") != true)
            {
                System.IO.Directory.CreateDirectory(ConstKCSAPIPath + "\\upload");
            }

            string[] _FileList = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\upload\\");

            if (_FileList.Length > 0)
            {
                for (int i = 0; i < _FileList.Length; i++)
                {
                    sFileName = _FileList[i].ToString();

                    System.IO.File.Delete(sFileName);
                }
            }
        }

        private void UP_DirFileMove_KCSAPI4()
        {
            int j = 0;
            int iPoint = 0;
            int iIndex = 0;
            string sPathFileName = string.Empty;
            string sFileName = string.Empty;
            string[] _FileList = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\upload\\");

            if (System.IO.Directory.Exists(ConstKCSAPIPath + "\\uploadsave") != true)
            {
                System.IO.Directory.CreateDirectory(ConstKCSAPIPath + "\\uploadsave");
            }            

            if (_FileList.Length > 0)
            {
                for (int i = 0; i < _FileList.Length; i++)
                {
                    sFileName = _FileList[i].ToString();
                    sPathFileName = sFileName;

                    for (; ; )
                    {
                        j = Convert.ToInt16(sPathFileName.IndexOf("\\", iPoint).ToString());

                        if (j < 0)
                        {
                            break;
                        }
                        else
                        {
                            iPoint = j + 1;
                            iIndex = j;
                        }
                    }

                    sFileName = sPathFileName.Substring(iIndex + 1, sPathFileName.Length - (iIndex + 1));

                    System.IO.File.Delete(ConstKCSAPIPath + "\\uploadsave\\" + sFileName);

                    System.IO.File.Move(sPathFileName, ConstKCSAPIPath + "\\uploadsave\\" + sFileName);
                }
            }

        }
        #endregion       

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : DataTable ROW 추가
        private void UP_SetDataTable_RowAdd(string sStr)
        {
            DataRow row;

            row = fsSendMigTable.NewRow();
            row["MIGSTR"] = sStr;

            fsSendMigTable.Rows.Add(row);                        
        }

        private void UP_SetDataTable_MastRowAdd(Int16 iSeq, string sStr)
        {
            DataRow row;

            row = fsSendMastTable.NewRow();
            row["USRSEQ"] = iSeq;
            row["USRFLD"] = sStr;

            fsSendMastTable.Rows.Add(row);
        }
        #endregion

        #region  Description : DataTable 만들기
        private DataTable UP_SetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MIGSTR", typeof(System.String));
            return dt;
        }

        private DataTable UP_SetMasterDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("USRSEQ", typeof(System.Int16));
            dt.Columns.Add("USRFLD", typeof(System.String));
            return dt;
        }
        #endregion

        #region  Description : XML 파일 생성 함수
        private void UP_Set_XmlFileCreate(string sXml, string sFileName)
        {           
            if (System.IO.Directory.Exists(ConstKCSAPIPath + "\\upload") != true)
            {
                System.IO.Directory.CreateDirectory(ConstKCSAPIPath + "\\upload");
            }

            StreamWriter sw = new StreamWriter(ConstKCSAPIPath + "\\upload\\" + sFileName, false, Encoding.UTF8);
            sw.WriteLine(sXml);
            sw.Close();
        }    
        #endregion
        
        #region  Description : 반입보고서 자료 생성 SILO
        private void UP_Get_DataIPGOF_SILO(string sDate, string sGubn)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_88RDY639", sDate, sGubn);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_91H94520", this.CBO01_EDIGJ.GetValue().ToString(), sDate,
                                                                    dt.Rows[i]["EDIJUKHA"].ToString(),
                                                                    dt.Rows[i]["EDIBLMSN"].ToString(),
                                                                    dt.Rows[i]["EDIBLHSN"].ToString(),
                                                                    dt.Rows[i]["IBHMNO1"].ToString(),
                                                                    dt.Rows[i]["IBHMNO2"].ToString()
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
            this.DbConnector.Attach("TY_P_US_88RDY639", sDate, sGubn);
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(dk.Rows[i]["IBHMNO1"].ToString());
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(dk.Rows[i]["IBHMNO2"].ToString())));

                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["IHJUKHANO"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["IBBLMSN"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(dk.Rows[i]["IBBLHSN"].ToString());
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["IBDATE"].ToString());

                        //2018.07.24 실제전송하는 시간으로 수정( 박동근 차장 요청)                        
                        //this.DAT02_EDITIME.SetValue(dk.Rows[i]["CMHITIM"].ToString());
                        this.DAT02_EDITIME.SetValue(DateTime.Now.ToString("HHmmss").ToString());

                        this.DAT02_EDIJSGB.SetValue("9");
                        this.DAT02_EDIBANGB.SetValue(dk.Rows[i]["IBGUBUN"].ToString());
                        this.DAT02_EDIHMGB.SetValue("10");

                        if (Convert.ToInt16(dk.Rows[i]["IBBEJNCNT"].ToString()) == 1 && Convert.ToDouble(dk.Rows[i]["IBBEJNQTYNUQTY"].ToString()) >= Convert.ToDouble(dk.Rows[i]["JKBEJNQTY"].ToString()))
                        {
                            this.DAT02_EDIBUNHAL.SetValue("A");
                            this.DAT02_EDICHASU.SetValue("0");
                            this.DAT02_EDINUQTY.SetValue("0");
                        }
                        else if (Convert.ToDouble(dk.Rows[i]["IBBEJNQTYNUQTY"].ToString()) < Convert.ToDouble(dk.Rows[i]["JKBEJNQTY"].ToString()))
                        {
                            this.DAT02_EDIBUNHAL.SetValue("P");
                            this.DAT02_EDICHASU.SetValue(dk.Rows[i]["IBBEJNCNT"].ToString());
                            this.DAT02_EDINUQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IBBEJNQTYNUQTY"].ToString()) * 1000, 0));
                        }
                        else if (Convert.ToDouble(dk.Rows[i]["IBBEJNQTYNUQTY"].ToString()) >= Convert.ToDouble(dk.Rows[i]["JKBEJNQTY"].ToString()))
                        {
                            this.DAT02_EDIBUNHAL.SetValue("L");
                            this.DAT02_EDICHASU.SetValue(dk.Rows[i]["IBBEJNCNT"].ToString());
                            this.DAT02_EDINUQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IBBEJNQTYNUQTY"].ToString()) * 1000, 0));
                        }

                        this.DAT02_EDISINNO.SetValue("");
                        this.DAT02_EDICOUNT.SetValue("0");

                        this.DAT02_EDIIPQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["IBBEJNQTY"].ToString()) * 1000, 0));
                        
                        this.DAT02_EDIPOJANG.SetValue("VR");

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["IHIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["IBHANGCHA"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["IBGOKJONG"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["IBHWAJU"].ToString());

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");
                        this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["IBHMNO1"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(dk.Rows[i]["IBHMNO2"].ToString());

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),     
                                            this.DAT02_EDIJUKHA.GetValue(),  
                                            this.DAT02_EDIBLMSN.GetValue(),  
                                            this.DAT02_EDIBLHSN.GetValue(),  
                                            this.DAT02_EDIDATE.GetValue(),   
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),    
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
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            this.DAT02_EDIHWAJU.GetValue(),  
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
                        this.DbConnector.Attach("TY_P_UT_74Q9F379", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion

        #region  Description : 반출보고서 자료 생성 SILO
        private void UP_Get_DataCHULF_SILO(string sDate)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_88RHS643", sDate );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수 안된 자료 삭제
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_758DH410", this.CBO01_EDIGJ.GetValue().ToString(), sDate,
                                                                    dt.Rows[i]["IHJUKHANO"].ToString(),
                                                                    dt.Rows[i]["CHBLMSN"].ToString(),
                                                                    dt.Rows[i]["CHBLHSN"].ToString(),
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
            this.DbConnector.Attach("TY_P_US_88RHS643", sDate);
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                pgBar.Maximum = dk.Rows.Count;

                for (int i = 0; i < dk.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() != "Y")
                    {
                        this.DAT02_EDIGJ.SetValue(this.CBO01_EDIGJ.GetValue().ToString());
                        this.DAT02_EDIDATE.SetValue(dk.Rows[i]["CHCHULDAT"].ToString());
                        this.DAT02_EDIJUKHA.SetValue(dk.Rows[i]["IHJUKHANO"].ToString());
                        this.DAT02_EDIBLMSN.SetValue(Set_Fill4(dk.Rows[i]["CHBLMSN"].ToString()));
                        this.DAT02_EDIBLHSN.SetValue(Set_Fill4(dk.Rows[i]["CHBLHSN"].ToString()));

                        this.DAT02_EDISINNO.SetValue(dk.Rows[i]["CSSINNO"].ToString());

                        this.DAT02_EDINO1.SetValue(dk.Rows[i]["EDNIMPSIGN"].ToString());
                        this.DAT02_EDINO2.SetValue(dk.Rows[i]["CHCHULDAT"].ToString().Substring(0, 4));
                        this.DAT02_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(dk.Rows[i]["CHCHULDAT"].ToString().Substring(0, 4)))));

                        this.DAT02_EDITIME.SetValue(Set_Fill4(dk.Rows[i]["CHCHTIME"].ToString()) + "00");
                        this.DAT02_EDIJSGB.SetValue("9");
                        this.DAT02_EDIBANGB.SetValue(dk.Rows[i]["CSBANGB"].ToString());
                        this.DAT02_EDICHASU.SetValue(dk.Rows[i]["CHASU"].ToString());
                        this.DAT02_EDIBUNHAL.SetValue(dk.Rows[i]["BUNHAL"].ToString());

                        this.DAT02_EDICHQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["SINGOMTQTY"].ToString()) * 1000, 0));
                        this.DAT02_EDICHCNT.SetValue("0");
                        this.DAT02_EDINUQTY.SetValue(Math.Round(Convert.ToDouble(dk.Rows[i]["CHMTQTYTOTAL"].ToString()) * 1000, 0));
                        this.DAT02_EDINUCNT.SetValue("0");

                        this.DAT02_EDIIPHANG.SetValue(dk.Rows[i]["IHIPHANG"].ToString());
                        this.DAT02_EDIHANGCHA.SetValue(dk.Rows[i]["CHHANGCHA"].ToString());
                        this.DAT02_EDIHWAMUL.SetValue(dk.Rows[i]["CHGOKJONG"].ToString());
                        this.DAT02_EDIHWAJU.SetValue(dk.Rows[i]["CHHWAJU"].ToString());

                        this.DAT02_EDIRCVGB.SetValue("");
                        this.DAT02_EDIMSG.SetValue("");

                        this.DAT02_EDIHMNO1.SetValue(dk.Rows[i]["CSHMNO1"].ToString());
                        this.DAT02_EDIHMNO2.SetValue(String.Format("{0:D6}", Convert.ToInt64(dk.Rows[i]["CSHMNO2"].ToString())));

                        datas.Add(new object[] { this.DAT02_EDIGJ.GetValue(),     
                                             this.DAT02_EDIDATE.GetValue(),   
                                             this.DAT02_EDIJUKHA.GetValue(),  
                                             this.DAT02_EDIBLMSN.GetValue(),  
                                             this.DAT02_EDIBLHSN.GetValue(),                                              
                                             this.DAT02_EDISINNO.GetValue(),  
                                            this.DAT02_EDINO1.GetValue(),    
                                            this.DAT02_EDINO2.GetValue(),    
                                            this.DAT02_EDINO3.GetValue(),    
                                            this.DAT02_EDITIME.GetValue(),   
                                            this.DAT02_EDIJSGB.GetValue(),   
                                            this.DAT02_EDIBANGB.GetValue(),  
                                            this.DAT02_EDIBUNHAL.GetValue(),                                            
                                            this.DAT02_EDICHQTY.GetValue(),  
                                            this.DAT02_EDICHCNT.GetValue(),  
                                            this.DAT02_EDINUQTY.GetValue(), 
                                            this.DAT02_EDINUCNT.GetValue(),  
                                            this.DAT02_EDICHASU.GetValue(),                                             
                                            this.DAT02_EDIIPHANG.GetValue(), 
                                            this.DAT02_EDIHANGCHA.GetValue(),
                                            this.DAT02_EDIHWAMUL.GetValue(), 
                                            this.DAT02_EDIHWAJU.GetValue(),  
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
                        this.DbConnector.Attach("TY_P_UT_758G0422", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

        }
        #endregion

        #region  Description : CBO01_EDIGJ_SelectedIndexChanged
        private void CBO01_EDIGJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_EDIGJ.GetValue().ToString() == "S")
            {
                RDB01_CHK4.Visible = false;
                RDB01_CHK5.Visible = false;
                RDB01_CHK7.Visible = false;
                RDB01_CHK8.Visible = false;
                RDB01_CHK9.Visible = false;
                RDB01_CHK10.Visible = true;
                RDB01_CHK11.Visible = false;
                
            }
            else
            {
                RDB01_CHK4.Visible = true;
                RDB01_CHK5.Visible = true;
                RDB01_CHK7.Visible = true;
                RDB01_CHK8.Visible = true;
                RDB01_CHK9.Visible = true;
                RDB01_CHK10.Visible = false;
                RDB01_CHK11.Visible = true;
                
            }

            if (RDB01_CHK4.Checked == true || RDB01_CHK4.Checked == true)
            {
                DTP01_EDATE.Visible = true;
                label1.Visible = true;

            }

        }
        #endregion

        #region  Description : EDI 체크 옵션 SelectedIndexChanged
        private void RDB01_CHK1_CheckedChanged(object sender, EventArgs e)
        {
            //반입보고서
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }

        private void RDB01_CHK2_CheckedChanged(object sender, EventArgs e)
        {
            //반출보고서
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }

        private void RDB01_CHK3_CheckedChanged(object sender, EventArgs e)
        {
            //체화보고서
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }
        private void RDB01_CHK4_CheckedChanged(object sender, EventArgs e)
        {
            //내국반입보고서
            DTP01_EDATE.Visible = true;
            label1.Visible = true;
        }       

        private void RDB01_CHK5_CheckedChanged(object sender, EventArgs e)
        {
            //내국반출보고서
            DTP01_EDATE.Visible = true;
            label1.Visible = true;

        }

        private void RDB01_CHK6_CheckedChanged(object sender, EventArgs e)
        {
            //반출입정정신고서
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }

        private void RDB01_CHK7_CheckedChanged(object sender, EventArgs e)
        {
            //BL반출신고
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }

        private void RDB01_CHK8_CheckedChanged(object sender, EventArgs e)
        {
            //보세운송+반송화물
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }

        private void RDB01_CHK9_CheckedChanged(object sender, EventArgs e)
        {
            //반출기간연장
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }

        private void RDB01_CHK10_CheckedChanged(object sender, EventArgs e)
        {
            //BL분할반입
            DTP01_EDATE.Visible = false;
            label1.Visible = false;

        }
        #endregion

        #region  Description : 전송 모듈 호출 이벤트
        private void UP_KCSAPIModulCall()
        {
            

            ProcessStartInfo ps = new ProcessStartInfo();

            ps.FileName = "TY_KCSAPI.exe"; // 프로그램 파일명                            
            ps.UseShellExecute = false;
            ps.RedirectStandardOutput = true;
            ps.CreateNoWindow = true;            
            ps.Arguments = "TY,RX," + Get_KCSAPI4LoginId() + "," + Get_KCSAPI4DocUserId();  // 회사구분, 호출구분(전송(RX),수신(TX))

            //ps.WorkingDirectory = ConstKCSAPIPath + "\\"; //프로그램이 있는 디렉토리 위치
            ps.WorkingDirectory = Application.StartupPath + "\\"; //프로그램이 있는 디렉토리 위치
            ps.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            using (Process process = Process.Start(ps))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                   string sArguments = reader.ReadToEnd();
                }
            }

            return;
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
