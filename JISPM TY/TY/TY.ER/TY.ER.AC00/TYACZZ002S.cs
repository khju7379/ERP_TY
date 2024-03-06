using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

using System.IO;
using System.Windows.Forms; 

namespace TY.ER.AC00
{
    /// <summary>
    /// 미승인화면 테스트 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.19 14:31
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
    ///  INQ : 조회
    ///  B2CDAC : 계정코드
    ///  B2CDBK : 은행코드
    ///  B2DPMK : 작성부서
    ///  B2NOAC : 계좌번호
    ///  VNCODE : 거래처코드
    ///  CDCODE : CODE
    /// </summary>
    public partial class TYACZZ002S : TYBase
    {
        //TYCodeBox CBH10_CBHTEST_CUSTUM;
        //TYCodeBox CBH10_B2DPAC;

        bool _Isloaded = false;

        TYCodeBox CBH10_VNCODE;
        TYCodeBox CBH11_VNCODE;
        TYCodeBox CBH12_VNCODE;
        TYCodeBox CBH13_VNCODE;
        TYCodeBox CBH14_VNCODE;
        TYCodeBox CBH15_VNCODE;

        TYCodeBox CBH10_B2CDBK;
        TYCodeBox CBH11_B2CDBK;
        TYCodeBox CBH12_B2CDBK;
        TYCodeBox CBH13_B2CDBK;
        TYCodeBox CBH14_B2CDBK;
        TYCodeBox CBH15_B2CDBK;

        TYCodeBox CBH10_B2NOAC;
        TYCodeBox CBH11_B2NOAC;
        TYCodeBox CBH12_B2NOAC;
        TYCodeBox CBH13_B2NOAC;
        TYCodeBox CBH14_B2NOAC;
        TYCodeBox CBH15_B2NOAC;

        TYCodeBox CBH10_B2DPMK;
        TYCodeBox CBH11_B2DPMK;
        TYCodeBox CBH12_B2DPMK;
        TYCodeBox CBH13_B2DPMK;
        TYCodeBox CBH14_B2DPMK;
        TYCodeBox CBH15_B2DPMK;
        
        TYTextBox TXT10_V1DATE;
        TYTextBox TXT11_V1DATE;
        TYTextBox TXT12_V1DATE;
        TYTextBox TXT13_V1DATE;
        TYTextBox TXT14_V1DATE;
        TYTextBox TXT15_V1DATE;

        TYTextBox TXT10_V1AMT;
        TYTextBox TXT11_V1AMT;
        TYTextBox TXT12_V1AMT;
        TYTextBox TXT13_V1AMT;
        TYTextBox TXT14_V1AMT;
        TYTextBox TXT15_V1AMT;
        
        TYCodeBox CBH10_B2BUDGET;
        TYCodeBox CBH11_B2BUDGET;
        TYCodeBox CBH12_B2BUDGET;
        TYCodeBox CBH13_B2BUDGET;
        TYCodeBox CBH14_B2BUDGET;
        TYCodeBox CBH15_B2BUDGET;

        //TYTextButtonBox TXT21_TXTTEST;


        public IControlFactory _SAMPLECTRL = null;
        public Control _TControlRefer = null;
        public IControlFactory _SAMPLECTRL2 = null;
        public Control _TControlRefer2 = null;
        public IControlFactory _SAMPLECTRL3 = null;
        public Control _TControlRefer3 = null;

        public IControlFactory _SAMPLECTRL4 = null;
        public Control _TControlRefer4 = null;

        public IControlFactory _SAMPLECTRL5 = null;
        public Control _TControlRefer5 = null;
        public IControlFactory _SAMPLECTRL6 = null;
        public Control _TControlRefer6 = null;

        //private TYData DAT20_A1SEQ;
        //private TYData DAT20_A1FILENAME;
        //private TYData DAT20_A1FILESIZE;
        //private TYData DAT20_A1IMG;


        public TYACZZ002S()
        {
            InitializeComponent();

            UP_Set_Control();

            //this.TXT21_TXTTEST = new TYTextButtonBox();
            //this.TXT21_TXTTEST.Name = "TXT21_TXTTEST";
            //this.TXT21_TXTTEST.TextBox.Width = 80;  
            //this.TXT21_TXTTEST.Button.Click += new EventHandler(TXT21_TXTTEST_Button_Click);
            //this.TXT21_TXTTEST.KeyDown += new System.Windows.Forms.KeyEventHandler(TXT21_TXTTEST_KeyDown);
            //this.PAN11_TEST.AddControl("02", "테스트2", this.TXT21_TXTTEST);

            //this.DAT20_A1SEQ = new TYData("DAT20_A1SEQ", null);
            //this.DAT20_A1FILENAME = new TYData("DAT20_A1FILENAME", null);
            //this.DAT20_A1FILESIZE = new TYData("DAT20_A1FILESIZE", null);
            //this.DAT20_A1IMG = new TYData("DAT20_A1IMG",  null);

        }

        void TXT21_TXTTEST_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
                this.CBH01_B2CDBK.ShowPopupHelper();
        }

        private void TYACZZ002S_Load(object sender, System.EventArgs e)
        {

            //this.ControlFactory.Add(this.DAT20_A1SEQ);
            //this.ControlFactory.Add(this.DAT20_A1FILENAME);
            //this.ControlFactory.Add(this.DAT20_A1FILESIZE);
            //this.ControlFactory.Add(this.DAT20_A1IMG);


            this.PAN11_TEST.Initialize();
            this.PAN11_TEST.SetCurCode("02");

            //UP_Set_Control();
            this.PAN10_VLMI1.Initialize();
            this.PAN10_VLMI2.Initialize();
            this.PAN10_VLMI3.Initialize();
            this.PAN10_VLMI4.Initialize();
            this.PAN10_VLMI5.Initialize();
            this.PAN10_VLMI6.Initialize();

            this.CBH10_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH10_B2CDBK_CodeBoxDataBinded);

            this.CBH11_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH11_B2CDBK_CodeBoxDataBinded);
            this.CBH12_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH12_B2CDBK_CodeBoxDataBinded);
            this.CBH13_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH13_B2CDBK_CodeBoxDataBinded);
            this.CBH14_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH14_B2CDBK_CodeBoxDataBinded);
            this.CBH15_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH15_B2CDBK_CodeBoxDataBinded);

            this.CBH01_B2CDBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_B2CDBK_CodeBoxDataBinded);

            //CBH01_B2CDAC.SetValue("");
            //CBH01_B2CDAC.SetValue("11100302");  
 

            //this.CBH13_B2BUDGET.SetIPopupHelper(new TYAZBJ01C1());

            //this.SetStartingFocus(this.TXT02_CDCODE);
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            
            this.PAN10_VLMI1.Initialize();
            this.PAN10_VLMI2.Initialize();
            this.PAN10_VLMI3.Initialize();
            this.PAN10_VLMI4.Initialize();
            this.PAN10_VLMI5.Initialize();
            this.PAN10_VLMI6.Initialize();

            //TY_P_AC_23N3M888
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH01_B2CDAC.GetValue(), "");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["A1CDMI1"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["A1CDMI1"].ToString().Trim().Substring(0, 2) == "02")
                    {
                        this.PAN10_VLMI1.SetCurCode("02");
                        this.PAN10_VLMI1.SetValue("126004"); 

                    }
                    else if (dt.Rows[0]["A1CDMI1"].ToString().Trim().Substring(0, 2) == "11")
                    {
                        this.PAN10_VLMI1.SetCurCode("11");                        
                    }
                    else
                    {
                        this.PAN10_VLMI1.SetCurCode(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                    }
                    
                }
                if (dt.Rows[0]["A1CDMI2"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["A1CDMI2"].ToString().Trim().Substring(0, 2) == "11")
                    {
                        this.PAN10_VLMI2.SetCurCode("11");                        
                    }
                    else
                    {
                        this.PAN10_VLMI2.SetCurCode(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                        this.PAN10_VLMI2.SetValue("140-000-984537");
                    }
                    
                }
                if (dt.Rows[0]["A1CDMI3"].ToString().Trim() != "")
                {
                    this.PAN10_VLMI3.SetCurCode(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                }
                if (dt.Rows[0]["A1CDMI4"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["A1CDMI4"].ToString().Trim().Substring(0, 2) == "35")
                    {
                        
                        this.PAN10_VLMI4.SetCurCode(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                        this.CBH13_B2BUDGET.SetIPopupHelper(new TYAZBJ01C1());
                    }
                    else
                    {
                        this.PAN10_VLMI4.SetCurCode(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                    }
                }
                //if (dt.Rows[0]["A1CDMI5"].ToString().Trim() != "")
                //{
                //    this.PAN10_VLMI5.SetCurCode(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                //}
                //if (dt.Rows[0]["A1CDMI6"].ToString().Trim() != "")
                //{
                //    this.PAN10_VLMI6.SetCurCode(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                //}
                //if (dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2) == "07")
                //{
                //    sCode = dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2);
                //    sCodeName = dt.Rows[0]["A1CDMI1"].ToString().Substring(3, dt.Rows[0]["A1CDMI1"].ToString().Length - 2);
                //    this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2NOAC);
                //}

            }
            //TYItemBox 테스트
            //this.CBH10_B2DPAC = new TYCodeBox();
            //this.CBH10_B2DPAC.Name = "CBH10_B2DPAC";
            //this.CBH10_B2DPAC.DummyValue = "20120913";
            //this.PAN10_TEST.AddControl("05", "귀속부서", CBH10_B2DPAC);

            //this.CBH10_CBHTEST_CUSTUM = new TYCodeBox();
            //this.CBH10_CBHTEST_CUSTUM.Name = "CBH10_CBHTEST_CUSTUM";
            //this.CBH10_CBHTEST_CUSTUM.DummyValue = new string[] { "21101006", "120952", "12131213" };
            //this.PAN10_TEST.AddControl("02", "테스트2", CBH10_CBHTEST_CUSTUM);

          

        }

        private void UP_Set_Control()
        {
            string sCode = "";
            string sCodeName = "";

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_23S15942", "");
            //DataTable dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        sCode = dt.Rows[i]["A2CDMI"].ToString();
            //        sCodeName = dt.Rows[i]["A2NMCD"].ToString();
            //        if (sCode == "01")
            //        {
            //            this.CBH10_VNCODE = new TYCodeBox();
            //            this.CBH10_VNCODE.Name = "CBH10_VNCODE";
            //            this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_VNCODE);
            //            this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH10_VNCODE);
            //            this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH10_VNCODE);
            //            this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH10_VNCODE);
            //            this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH10_VNCODE);
            //            this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH10_VNCODE);
            //        }
            //        if (sCode == "02")
            //        {
            //            this.CBH10_B2CDBK = new TYCodeBox();
            //            this.CBH10_B2CDBK.Name = "CBH10_B2CDBK";
            //            this.CBH10_B2CDBK.DummyValue = "BK";
            //            this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //        }
            //        if (sCode == "03")
            //        {
            //            this.CBH10_B2DPMK = new TYCodeBox();
            //            this.CBH10_B2DPMK.Name = "CBH10_B2DPMK";
            //            this.CBH10_B2DPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");   
            //            this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2DPMK);
            //            this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH10_B2DPMK);
            //            this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH10_B2DPMK);
            //            this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH10_B2DPMK);
            //            this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH10_B2DPMK);
            //            this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH10_B2DPMK);
            //        }
            //        if (sCode == "07")
            //        {
            //            this.CBH10_B2NOAC = new TYCodeBox();
            //            this.CBH10_B2NOAC.Name = "CBH10_B2NOAC";
            //            this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2NOAC);
            //            this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH10_B2NOAC);
            //            this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH10_B2NOAC);
            //            this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH10_B2NOAC);
            //            this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH10_B2NOAC);
            //            this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH10_B2NOAC);
            //        }
            //        if (sCode == "11")
            //        {
            //            this.CBH10_B2CDBK = new TYCodeBox();
            //            this.CBH10_B2CDBK.Name = "CBH10_B2CDBK";
            //            this.CBH10_B2CDBK.DummyValue = "TX";
            //            this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //            this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH10_B2CDBK);
            //        }
            //        if (sCode == "14")
            //        {
            //            this.TXT10_V1DATE = new TYTextBox();
            //            this.TXT10_V1DATE.Name = "TXT10_V1DATE";
            //            this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_V1DATE);
            //            this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT10_V1DATE);
            //            this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT10_V1DATE);
            //            this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT10_V1DATE);
            //            this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT10_V1DATE);
            //            this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT10_V1DATE);
            //        }
            //        if (sCode == "15")
            //        {
            //            this.TXT10_V1AMT = new TYTextBox();
            //            this.TXT10_V1AMT.Name = "TXT10_V1AMT";
            //            this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_V1AMT);
            //            this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT10_V1AMT);
            //            this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT10_V1AMT);
            //            this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT10_V1AMT);
            //            this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT10_V1AMT);
            //            this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT10_V1AMT);
            //        }

            //    }
            //}

            sCode = "01";
            sCodeName = "거래처";
            if (sCode == "01")
            {
                this.CBH10_VNCODE = new TYCodeBox();
                this.CBH10_VNCODE.Name = "CBH10_VNCODE";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_VNCODE);
                this.CBH11_VNCODE = new TYCodeBox();
                this.CBH11_VNCODE.Name = "CBH11_VNCODE";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_VNCODE);
                this.CBH12_VNCODE = new TYCodeBox();
                this.CBH12_VNCODE.Name = "CBH12_VNCODE";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_VNCODE);
                this.CBH13_VNCODE = new TYCodeBox();
                this.CBH13_VNCODE.Name = "CBH13_VNCODE";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_VNCODE);
                this.CBH14_VNCODE = new TYCodeBox();
                this.CBH14_VNCODE.Name = "CBH14_VNCODE";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_VNCODE);
                this.CBH15_VNCODE = new TYCodeBox();
                this.CBH15_VNCODE.Name = "CBH15_VNCODE";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_VNCODE);
            }
            sCode = "02";
            sCodeName = "금융기관";
            if (sCode == "02")
            {
                this.CBH10_B2CDBK = new TYCodeBox();
                this.CBH10_B2CDBK.Name = "CBH10_B2CDBK";
                this.CBH10_B2CDBK.DummyValue = "BK";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2CDBK, this.CBH10_B2CDBK.DummyValue);
                this.CBH11_B2CDBK = new TYCodeBox();
                this.CBH11_B2CDBK.Name = "CBH11_B2CDBK";
                this.CBH11_B2CDBK.DummyValue = "BK";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_B2CDBK, this.CBH11_B2CDBK.DummyValue);
                this.CBH12_B2CDBK = new TYCodeBox();
                this.CBH12_B2CDBK.Name = "CBH12_B2CDBK";
                this.CBH12_B2CDBK.DummyValue = "BK";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_B2CDBK, this.CBH12_B2CDBK.DummyValue);
                this.CBH13_B2CDBK = new TYCodeBox();
                this.CBH13_B2CDBK.Name = "CBH13_B2CDBK";
                this.CBH13_B2CDBK.DummyValue = "BK";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_B2CDBK, this.CBH13_B2CDBK.DummyValue);
                this.CBH14_B2CDBK = new TYCodeBox();
                this.CBH14_B2CDBK.Name = "CBH14_B2CDBK";
                this.CBH14_B2CDBK.DummyValue = "BK";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_B2CDBK, this.CBH14_B2CDBK.DummyValue);
                this.CBH15_B2CDBK = new TYCodeBox();
                this.CBH15_B2CDBK.Name = "CBH15_B2CDBK";
                this.CBH15_B2CDBK.DummyValue = "BK";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_B2CDBK, this.CBH15_B2CDBK.DummyValue);
            }
            sCode = "03";
            sCodeName = "귀속부서";
            if (sCode == "03")
            {
                this.CBH10_B2DPMK = new TYCodeBox();
                this.CBH10_B2DPMK.Name = "CBH10_B2DPMK";
                this.CBH10_B2DPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2DPMK);
                this.CBH11_B2DPMK = new TYCodeBox();
                this.CBH11_B2DPMK.Name = "CBH11_B2DPMK";
                this.CBH11_B2DPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_B2DPMK);
                this.CBH12_B2DPMK = new TYCodeBox();
                this.CBH12_B2DPMK.Name = "CBH12_B2DPMK";
                this.CBH12_B2DPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_B2DPMK);
                this.CBH13_B2DPMK = new TYCodeBox();
                this.CBH13_B2DPMK.Name = "CBH13_B2DPMK";
                this.CBH13_B2DPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_B2DPMK);
                this.CBH14_B2DPMK = new TYCodeBox();
                this.CBH14_B2DPMK.Name = "CBH14_B2DPMK";
                this.CBH14_B2DPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_B2DPMK);
                this.CBH15_B2DPMK = new TYCodeBox();
                this.CBH15_B2DPMK.Name = "CBH15_B2DPMK";
                this.CBH15_B2DPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_B2DPMK);
            }
            sCode = "07";
            sCodeName = "계좌번호";
            if (sCode == "07")
            {
                this.CBH10_B2NOAC = new TYCodeBox();
                this.CBH10_B2NOAC.Name = "CBH10_B2NOAC";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2NOAC);
                this.CBH11_B2NOAC = new TYCodeBox();
                this.CBH11_B2NOAC.Name = "CBH11_B2NOAC";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_B2NOAC);
                this.CBH12_B2NOAC = new TYCodeBox();
                this.CBH12_B2NOAC.Name = "CBH12_B2NOAC";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_B2NOAC);
                this.CBH13_B2NOAC = new TYCodeBox();
                this.CBH13_B2NOAC.Name = "CBH13_B2NOAC";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_B2NOAC);
                this.CBH14_B2NOAC = new TYCodeBox();
                this.CBH14_B2NOAC.Name = "CBH14_B2NOAC";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_B2NOAC);
                this.CBH15_B2NOAC = new TYCodeBox();
                this.CBH15_B2NOAC.Name = "CBH15_B2NOAC";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_B2NOAC);
            }
            sCode = "11";
            sCodeName = "세무구분";
            if (sCode == "11")
            {
                this.CBH10_B2CDBK.DummyValue = "TX";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2CDBK, this.CBH10_B2CDBK.DummyValue);
                this.CBH11_B2CDBK.DummyValue = "TX";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_B2CDBK, this.CBH11_B2CDBK.DummyValue);
                this.CBH12_B2CDBK.DummyValue = "TX";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_B2CDBK, this.CBH12_B2CDBK.DummyValue);
                this.CBH13_B2CDBK.DummyValue = "TX";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_B2CDBK, this.CBH13_B2CDBK.DummyValue);
                this.CBH14_B2CDBK.DummyValue = "TX";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_B2CDBK, this.CBH14_B2CDBK.DummyValue);
                this.CBH15_B2CDBK.DummyValue = "TX";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_B2CDBK, this.CBH15_B2CDBK.DummyValue);
            }
            sCode = "15";
            sCodeName = "거래일자";
            if (sCode == "15")
            {
                this.TXT10_V1DATE = new TYTextBox();
                this.TXT10_V1DATE.Name = "TXT10_V1DATE";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_V1DATE);
                this.TXT11_V1DATE = new TYTextBox();
                this.TXT11_V1DATE.Name = "TXT11_V1DATE";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT11_V1DATE);
                this.TXT12_V1DATE = new TYTextBox();
                this.TXT12_V1DATE.Name = "TXT12_V1DATE";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT12_V1DATE);
                this.TXT13_V1DATE = new TYTextBox();
                this.TXT13_V1DATE.Name = "TXT13_V1DATE";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT13_V1DATE);
                this.TXT14_V1DATE = new TYTextBox();
                this.TXT14_V1DATE.Name = "TXT14_V1DATE";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT14_V1DATE);
                this.TXT15_V1DATE = new TYTextBox();
                this.TXT15_V1DATE.Name = "TXT15_V1DATE";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT15_V1DATE);
            }
            sCode = "14";
            sCodeName = "공급가액";
            if (sCode == "14")
            {
                this.TXT10_V1AMT = new TYTextBox();
                this.TXT10_V1AMT.Name = "TXT10_V1AMT";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_V1AMT);
                this.TXT11_V1AMT = new TYTextBox();
                this.TXT11_V1AMT.Name = "TXT11_V1AMT";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT11_V1AMT);
                this.TXT12_V1AMT = new TYTextBox();
                this.TXT12_V1AMT.Name = "TXT12_V1AMT";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT12_V1AMT);
                this.TXT13_V1AMT = new TYTextBox();
                this.TXT13_V1AMT.Name = "TXT13_V1AMT";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT13_V1AMT);
                this.TXT14_V1AMT = new TYTextBox();
                this.TXT14_V1AMT.Name = "TXT14_V1AMT";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT14_V1AMT);
                this.TXT15_V1AMT = new TYTextBox();
                this.TXT15_V1AMT.Name = "TXT15_V1AMT";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT15_V1AMT);
            }
            sCode = "35";
            sCodeName = "예산세목";
            if (sCode == "35")
            {
                this.CBH10_B2BUDGET = new TYCodeBox();
                this.CBH10_B2BUDGET.Name = "CBH10_B2BUDGET";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_B2BUDGET);
                this.CBH11_B2BUDGET = new TYCodeBox();
                this.CBH11_B2BUDGET.Name = "CBH11_B2BUDGET";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_B2BUDGET);
                this.CBH12_B2BUDGET = new TYCodeBox();
                this.CBH12_B2BUDGET.Name = "CBH12_B2BUDGET";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_B2BUDGET);
                this.CBH13_B2BUDGET = new TYCodeBox();
                this.CBH13_B2BUDGET.Name = "CBH13_B2BUDGET";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_B2BUDGET);
                this.CBH14_B2BUDGET = new TYCodeBox();
                this.CBH14_B2BUDGET.Name = "CBH14_B2BUDGET";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_B2BUDGET);
                this.CBH15_B2BUDGET = new TYCodeBox();
                this.CBH15_B2BUDGET.Name = "CBH15_B2BUDGET";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_B2BUDGET);
            }

        }

        private void CBH10_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH10_B2CDBK.GetValue().ToString();
            this.CBH11_B2NOAC.DummyValue = groupCode;
            this.CBH11_B2NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH11_B2NOAC.Initialize();
        }

        private void CBH11_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH11_B2CDBK.GetValue().ToString();
            this.CBH11_B2NOAC.DummyValue = groupCode;
            this.CBH11_B2NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH11_B2NOAC.Initialize();
        }
        private void CBH12_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH12_B2CDBK.GetValue().ToString();
            this.CBH12_B2NOAC.DummyValue = groupCode;
            this.CBH12_B2NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH12_B2NOAC.Initialize();
        }
        private void CBH13_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH13_B2CDBK.GetValue().ToString();
            this.CBH13_B2NOAC.DummyValue = groupCode;
            this.CBH13_B2NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH13_B2NOAC.Initialize();
        }
        private void CBH14_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH14_B2CDBK.GetValue().ToString();
            this.CBH14_B2NOAC.DummyValue = groupCode;
            this.CBH14_B2NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH14_B2NOAC.Initialize();
        }
        private void CBH15_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {

        }

        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            //TXT02_CDCODE.SetValue(this.PAN10_VLMI1.GetCurCode().ToString());  //cdmi1 인데스 01
            //TXT01_CDCODE.SetValue(this.PAN10_VLMI1.GetValue().ToString());  //값    

            //this.CBH13_B2BUDGET.DummyValue = new string[] { "20120131", "S10000", this.CBH01_B2CDAC.GetValue().ToString() };      

            //this.CBH10_B2CDBK.SetValue("126004");
            //this.CBH11_B2NOAC.DummyValue = "126004";
            //this.CBH11_B2NOAC.SetValue("140-000-984537");

            //this.PAN10_VLMI2.SetCurCode("07");
            //this.PAN10_VLMI1.SetValue("126004");
            //this.PAN10_VLMI2.SetValue("140-000-984537");

            //TXT02_CDCODE.SetReadOnly(false);

            //TXT02_CDCODE.Enabled = true; 

            //CBH01_B2CDAC.Initialize(); 
            //CBH01_B2CDAC.SetValue("11100302");  

            //TXT02_CDCODE.SetReadOnly(false);  

            textBox1.Text = _SAMPLECTRL.GetValue().ToString();  

  
        }

        private void CBH01_B2CDAC_TextChanged(object sender, EventArgs e)
        {
            //if (CBH01_B2CDAC.GetValue().ToString().Trim().Length == 8)
            //{
            //    this.BTN61_INQ_Click(null, null);
            //}
            //if (CBH01_B2CDAC.GetValue().ToString().Length >= 8)
            //{
            //    TXT02_CDCODE.SetValue("DDD");
            //}
            //else
            //{
            //    TXT02_CDCODE.SetValue("");
            //}
           
        }



        private void PAN10_VLMI2_Click(object sender, EventArgs e)
        {

        }

        private void PAN10_VLMI4_Click(object sender, EventArgs e)
        {
           //this.CBH13_B2BUDGET.DummyValue = new string[] { "20120131", "S10000", "42413301" };
        }

        private void PAN10_VLMI4_Enter(object sender, EventArgs e)
        {
            this.CBH13_B2BUDGET.DummyValue = new string[] { "20120131", "S10000", this.CBH01_B2CDAC.GetValue().ToString() };      
        }

        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            //TXT02_CDCODE.SetReadOnly(true);  

            //TXT02_CDCODE.Enabled = false; 

            byte[] _AttachFile = null;

            object _objAttachFile = null;

            string filePath = "C:\\Users\\Administrator\\Downloads\\main.jpg";

            _AttachFile = UP_Get_Byte(filePath);

            _objAttachFile = _AttachFile;

            //this.DAT20_A1SEQ.SetValue("1");
            //this.DAT20_A1FILENAME.SetValue("main.jpg");
            //this.DAT20_A1FILESIZE.SetValue(_AttachFile.Length.ToString());
            //this.DAT20_A1IMG.SetValue(_objAttachFile);

            this.ControlFactory.Add(new TData("DAT20_A1SEQ", "1"));
            this.ControlFactory.Add(new TData("DAT20_A1FILENAME", "main.jpg"));
            this.ControlFactory.Add(new TData("DAT20_A1FILESIZE", _AttachFile.Length.ToString()));
            this.ControlFactory.Add(new TData("DAT20_A1IMG", _objAttachFile));
            
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY2B9AH242", "1", "main.jpg", _AttachFile.Length.ToString(), _AttachFile);
            this.DbConnector.Attach("TY2B9AH242", this.ControlFactory, "20");
            this.DbConnector.ExecuteTranQuery();

            //FileStream stream = null;

            //byte[] _AttachFile = null;

            //string sfilename = "";

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY2B9AS243");
            //DataTable DT = this.DbConnector.ExecuteDataTable();

          

            //_AttachFile = DT.Rows[0]["A1IMG"] as byte[];

            //int ArraySize = _AttachFile.GetUpperBound(0);
            //stream = new FileStream(sfilename, FileMode.OpenOrCreate, FileAccess.Write);
            //stream.Write(_AttachFile, 0, ArraySize + 1);

            //stream.Close();

            PBX01_IMG.SetValue("c:\\main.jpg");
            

        }

        #region Description : 첨부파일 byte 변환
        public static byte[] UP_Get_Byte(string filePath)
        {
            //FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //BinaryReader reader = new BinaryReader(stream);

            //byte[] _AttachFile = reader.ReadBytes((int)stream.Length);

            //reader.Close();
            //stream.Close();

            //return _AttachFile;

            FileInfo file = new FileInfo(filePath);
            FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            byte[] rawAssembly = new byte[(int)stream.Length];
            stream.Read(rawAssembly, 0, rawAssembly.Length);
            return rawAssembly; // <= byte[] 임
        }
        #endregion        


        //private void TXT21_TXTTEST_Button_Click(object sender, EventArgs e)
        //{
        //    //CBH01_B2CDBK.SetValue(this.TXT21_TXTTEST.GetValue());

        //    this.CBH01_B2CDBK.ShowPopupHelper();
        //}

        void CBH01_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            //TXT21_TXTTEST.SetValue(this.CBH01_B2CDBK.GetValue());
            //TXT21_TXTTEST.TextBox.SetValue(this.CBH01_B2CDBK.GetText());
        }

        private void CBH01_B2CDAC_CodeBoxDataBinded(object sender, EventArgs e)
        {
            //if (CBH01_B2CDAC.GetValue().ToString().Length >= 8)
            //{
            //    TXT02_CDCODE.SetValue("DDD");
            //}
            //else
            //{
            //    TXT02_CDCODE.SetValue("");
            //}

            string sControlName1 = "";

            UP_Control_Clear();

            if (CBH01_B2CDAC.GetValue().ToString().Trim().Length == 8)
            {

                this.DynamicRemoveTControl(_TControlRefer);
                this.DynamicRemoveTControl(_TControlRefer2);
                this.DynamicRemoveTControl(_TControlRefer3);
                this.DynamicRemoveTControl(_TControlRefer4);
                this.DynamicRemoveTControl(_TControlRefer5);
                this.DynamicRemoveTControl(_TControlRefer6);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH01_B2CDAC.GetValue(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["A1CDMI1"].ToString().Trim() != "")
                    {
                        sControlName1 = UP_Control_Name(dt.Rows[0]["A1CDMI1"].ToString().Trim().Substring(0, 2));
                        if (sControlName1 != "")
                        {
                            this.DynamicCreateTControl(ref _SAMPLECTRL, ref _TControlRefer, sControlName1, this.LBL51_A2CDMI, this.panel1, 200, 20);                             
                        }
                    }
                    if (dt.Rows[0]["A1CDMI2"].ToString().Trim() != "")
                    {
                        sControlName1 = UP_Control_Name(dt.Rows[0]["A1CDMI2"].ToString().Trim().Substring(0, 2));
                        if (sControlName1 != "")
                        {
                            this.DynamicCreateTControl(ref _SAMPLECTRL2, ref _TControlRefer2, sControlName1, this.LBL51_A2CDMI2, this.panel2, 200, 20);
                        }                        
                    }
                    if (dt.Rows[0]["A1CDMI3"].ToString().Trim() != "")
                    {
                        sControlName1 = UP_Control_Name(dt.Rows[0]["A1CDMI3"].ToString().Trim().Substring(0, 2));
                        if (sControlName1 != "")
                        {
                            this.DynamicCreateTControl(ref _SAMPLECTRL3, ref _TControlRefer3, sControlName1, this.LBL51_A2CDMI3, this.panel3, 200, 20);
                        }
                    }
                    if (dt.Rows[0]["A1CDMI4"].ToString().Trim() != "")
                    {
                        sControlName1 = UP_Control_Name(dt.Rows[0]["A1CDMI4"].ToString().Trim().Substring(0, 2));
                        if (sControlName1 != "")
                        {
                            this.DynamicCreateTControl(ref _SAMPLECTRL4, ref _TControlRefer4, sControlName1, this.LBL51_A2CDMI4, this.panel4, 200, 20);
                        }
                    }
                    if (dt.Rows[0]["A1CDMI5"].ToString().Trim() != "")
                    {
                        sControlName1 = UP_Control_Name(dt.Rows[0]["A1CDMI5"].ToString().Trim().Substring(0, 2));
                        if (sControlName1 != "")
                        {
                            this.DynamicCreateTControl(ref _SAMPLECTRL5, ref _TControlRefer5, sControlName1, this.LBL51_A2CDMI5, this.panel5, 200, 20);
                        }
                    }
                    if (dt.Rows[0]["A1CDMI6"].ToString().Trim() != "")
                    {
                        sControlName1 = UP_Control_Name(dt.Rows[0]["A1CDMI6"].ToString().Trim().Substring(0, 2));
                        if (sControlName1 != "")
                        {
                            this.DynamicCreateTControl(ref _SAMPLECTRL6, ref _TControlRefer6, sControlName1, this.LBL51_A2CDMI6, this.panel6, 200, 20);
                        }
                    }

                }
                this.ResourceFactory.ControlFactory.Clear();
                this.ResourceFactory.IControlFactoryCollection(this.Controls);
                this.ResourceFactory.IControlFactoryCollectionTabIndexSorting();
            }
        }

        private void DynamicCreateTControl(ref IControlFactory factory, ref Control refer, string name, TLabel label, Control parent, int width, int height)
        {
            string prifix = name.Substring(0, 3);
            string factoryname = name.Substring(6);
            switch (prifix)
            {
                case "CBH":
                    refer = new TCodeBox();
                    ((TCodeBox)refer).CreatingIPopupHelper += new TCodeBox.TCodeBoxEventHandler(TYACZZ002S_CreatingIPopupHelper);
                    break;
                case "DTP":
                    refer = new TDatePicker();                    
                    break;
                case "TXT":
                    refer = new TTextBox();
                    break;
            }            
            
            refer.Name = name;
            refer.Parent = parent;
            refer.Size = new System.Drawing.Size(width, height);
            refer.Location = new System.Drawing.Point(0, 0);
            refer.Visible = true;            

            factory = (IControlFactory)refer;
            factory.FactoryName = this.ResourceFactory.Fields[factoryname].Field_Name;
            factory.FactoryOption = this.ResourceFactory.Fields[factoryname].Options;
            factory.ControlSetting();

            label.IsCreated = false;
            label.FactoryName = this.ResourceFactory.Fields[factoryname].Field_Name;
            label.FactoryOption = this.ResourceFactory.Fields[factoryname].Options;
            label.ControlSetting();
        }

        void TYACZZ002S_CreatingIPopupHelper(object sender, EventArgs e)
        {
            TCodeBox codebox = (TCodeBox)sender;
            codebox.DummyValue = new string[] { "33", "A50100", "20121231","3", "11100501", "", "", "" , "" };
            //MessageBox.Show("eee");
        }

        private void DynamicRemoveTControl(Control refer)
        {
            if (refer != null)
            {
                string prifix = refer.Name.Substring(0, 3);
                switch (prifix)
                {
                    case "CBH":
                        ((TCodeBox)refer).Visible = false;
                        break;
                    default:
                        refer.Visible = false;
                        break;
                }

                refer = null;
            }
        }

        private string UP_Control_Name(string sNum)
        {
            string sControlname = "";

            switch (sNum)
            {
                case "01":
                    sControlname = "CBH10_VALUE01";
                    break;
                case "02":
                    sControlname = "CBH10_B2INDX";
                    break;
                case "03":
                    sControlname = "CBH10_VALUE03";
                    break;
                case "04":
                    sControlname = "TXT10_VALUE04";
                    break;
                case "05":
                    sControlname = "CBH10_VALUE05";
                    break;
                case "06":
                    sControlname = "TXT10_VALUE06";
                    break;
                case "07":
                    sControlname = "CBH10_VALUE07";
                    break;
                case "08":
                    sControlname = "TXT10_VALUE08";
                    break;
                case "09":
                    sControlname = "CBH10_VALUE09";
                    break;
                case "10":
                    sControlname = "CBH10_VALUE10";
                    break;
                case "11":
                    sControlname = "CBH10_VALUE11";
                    break;
                case "12":
                    sControlname = "TXT10_VALUE12";
                    break;
                case "13":
                    sControlname = "TXT10_VALUE13";
                    break;
                case "14":
                    sControlname = "TXT10_VALUE14";
                    break;
                case "15":
                    sControlname = "TXT10_VALUE15";
                    break;
                case "16":
                    sControlname = "TXT10_VALUE16";
                    break;
                case "17":
                    sControlname = "TXT10_VALUE17";
                    break;
                case "18":
                    sControlname = "TXT10_VALUE18";
                    break;
                case "19":
                    sControlname = "TXT10_VALUE19";
                    break;
                case "20":
                    sControlname = "TXT10_VALUE20";
                    break;
                case "21":
                    sControlname = "TXT10_VALUE21";
                    break;
                case "23":
                    sControlname = "TXT10_VALUE23";
                    break;
                case "26":
                    sControlname = "CBH10_VALUE26";
                    break;
                case "27":
                    sControlname = "TXT10_VALUE27";
                    break;
                case "28":
                    sControlname = "TXT10_VALUE28";
                    break;
                case "29":
                    sControlname = "CBH10_VALUE29";
                    break;
                case "31":
                    sControlname = "TXT10_VALUE31";
                    break;
                case "32":
                    sControlname = "CBH10_VALUE32";
                    break;
                case "33":
                    sControlname = "TXT10_VALUE33";
                    break;
                case "34":
                    sControlname = "CBH10_VALUE34";
                    break;
                case "35":
                    sControlname = "CBH10_VALUE35";
                    break;
                case "36":
                    sControlname = "TXT10_VALUE36";
                    break;
                case "41":
                    sControlname = "CBH10_VALUE41";
                    break;
                case "42":
                    sControlname = "CBH10_VALUE42";
                    break;
                case "44":
                    sControlname = "CBH10_VALUE44";
                    break;

            }


            return sControlname;

        }

        private void UP_Control_Clear()
        {
            this.LBL51_A2CDMI.Text = "";
            this.panel1.Controls.Clear();

            this.LBL51_A2CDMI2.Text = "";
            this.panel2.Controls.Clear();

            this.LBL51_A2CDMI3.Text = "";
            this.panel3.Controls.Clear();

            this.LBL51_A2CDMI4.Text = "";
            this.panel4.Controls.Clear();

            this.LBL51_A2CDMI5.Text = "";
            this.panel5.Controls.Clear();

            this.LBL51_A2CDMI6.Text = "";
            this.panel6.Controls.Clear();  

        }

        private void CBH01_B2CDBK_ButtonClickBefore(object sender, EventArgs e)
        {

        }

        private void BTN64_INQ_Click(object sender, EventArgs e)
        {
            //파일 불러와서 보여주기

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY2B9AS243");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            FileStream stream = null;

            byte[] _AttachFile = null;

            try
            {

                string fileName = "c:\\temp\\"+dt.Rows[0]["A1FILENAME"].ToString();

                _AttachFile = dt.Rows[0]["A1IMG"] as byte[]; 

                int ArraySize = _AttachFile.GetUpperBound(0);

                stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(_AttachFile, 0, ArraySize + 1);

                PBX01_IMG.SetValue(_AttachFile); 
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }


        }

    }
}
