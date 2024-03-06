using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    public partial class TYACLO002I : TYBase
    {
        private bool _Isloaded         = false;

        private string fsLOCCONTYEAR   = string.Empty;
        private string fsLOCCONTNUM    = string.Empty;

        private string fsWK_GUBUN1     = string.Empty;
        private string fsWK_GUBUN2     = string.Empty;
        private string fsWK_GUBUN3     = string.Empty;

        private string fsTAB_GUBUN     = string.Empty;

        private string fsLOCCONTSEQ    = string.Empty;

        private string fsLOACCONTSEQ   = string.Empty;

        private string fsLOLICONTSEQ   = string.Empty;
        private string fsLOLIACCONTSEQ = string.Empty;

        private string fsSTATUS        = string.Empty;

        private string fsLORECONTSEQ   = string.Empty;
        private string fsLORECONTNO    = string.Empty;

        private string fsLOACAMT       = string.Empty;
        private string fsLOACDOLLAR    = string.Empty;
        private string fsLOLIAMT       = string.Empty;
        private string fsLOREAMT       = string.Empty;
        private string fsLOACGUBN      = string.Empty;
        private string fsLOACGUBN_03   = string.Empty;

        private TYData DAT02_W2SSID;
        private TYData DAT02_W2DPMK;
        private TYData DAT02_W2DTMK;
        private TYData DAT02_W2NOSQ;
        private TYData DAT02_W2NOLN;
        private TYData DAT02_W2IDJP;
        private TYData DAT02_W2NOJP;
        private TYData DAT02_W2CDAC;
        private TYData DAT02_W2DTAC;
        private TYData DAT02_W2DTLI;
        private TYData DAT02_W2DPAC;
        private TYData DAT02_W2CDMI1;
        private TYData DAT02_W2VLMI1;
        private TYData DAT02_W2CDMI2;
        private TYData DAT02_W2VLMI2;
        private TYData DAT02_W2CDMI3;
        private TYData DAT02_W2VLMI3;
        private TYData DAT02_W2CDMI4;
        private TYData DAT02_W2VLMI4;
        private TYData DAT02_W2CDMI5;
        private TYData DAT02_W2VLMI5;
        private TYData DAT02_W2CDMI6;
        private TYData DAT02_W2VLMI6;
        private TYData DAT02_W2AMDR;
        private TYData DAT02_W2AMCR;
        private TYData DAT02_W2CDFD;
        private TYData DAT02_W2AMFD;
        private TYData DAT02_W2RKAC;
        private TYData DAT02_W2RKCU;
        private TYData DAT02_W2WCJP;
        private TYData DAT02_W2PRGB;
        private TYData DAT02_W2HIGB;
        private TYData DAT02_W2HISAB;
        private TYData DAT02_W2GUBUN;
        private TYData DAT02_W2TXAMT;
        private TYData DAT02_W2TXVAT;
        private TYData DAT02_W2HWAJU;

        private DataSet fsDs = new DataSet();

        #region Description : 페이지 로드 
        public TYACLO002I()
        {
            InitializeComponent();

            this.DAT02_W2SSID  = new TYData("DAT02_W2SSID", null);
            this.DAT02_W2DPMK  = new TYData("DAT02_W2DPMK", null);
            this.DAT02_W2DTMK  = new TYData("DAT02_W2DTMK", null);
            this.DAT02_W2NOSQ  = new TYData("DAT02_W2NOSQ", null);
            this.DAT02_W2NOLN  = new TYData("DAT02_W2NOLN", null);
            this.DAT02_W2IDJP  = new TYData("DAT02_W2IDJP", null);
            this.DAT02_W2NOJP  = new TYData("DAT02_W2NOJP", null);
            this.DAT02_W2CDAC  = new TYData("DAT02_W2CDAC", null);
            this.DAT02_W2DTAC  = new TYData("DAT02_W2DTAC", null);
            this.DAT02_W2DTLI  = new TYData("DAT02_W2DTLI", null);
            this.DAT02_W2DPAC  = new TYData("DAT02_W2DPAC", null);
            this.DAT02_W2CDMI1 = new TYData("DAT02_W2CDMI1", null);
            this.DAT02_W2VLMI1 = new TYData("DAT02_W2VLMI1", null);
            this.DAT02_W2CDMI2 = new TYData("DAT02_W2CDMI2", null);
            this.DAT02_W2VLMI2 = new TYData("DAT02_W2VLMI2", null);
            this.DAT02_W2CDMI3 = new TYData("DAT02_W2CDMI3", null);
            this.DAT02_W2VLMI3 = new TYData("DAT02_W2VLMI3", null);
            this.DAT02_W2CDMI4 = new TYData("DAT02_W2CDMI4", null);
            this.DAT02_W2VLMI4 = new TYData("DAT02_W2VLMI4", null);
            this.DAT02_W2CDMI5 = new TYData("DAT02_W2CDMI5", null);
            this.DAT02_W2VLMI5 = new TYData("DAT02_W2VLMI5", null);
            this.DAT02_W2CDMI6 = new TYData("DAT02_W2CDMI6", null);
            this.DAT02_W2VLMI6 = new TYData("DAT02_W2VLMI6", null);
            this.DAT02_W2AMDR  = new TYData("DAT02_W2AMDR", null);
            this.DAT02_W2AMCR  = new TYData("DAT02_W2AMCR", null);
            this.DAT02_W2CDFD  = new TYData("DAT02_W2CDFD", null);
            this.DAT02_W2AMFD  = new TYData("DAT02_W2AMFD", null);
            this.DAT02_W2RKAC  = new TYData("DAT02_W2RKAC", null);
            this.DAT02_W2RKCU  = new TYData("DAT02_W2RKCU", null);
            this.DAT02_W2WCJP  = new TYData("DAT02_W2WCJP", null);
            this.DAT02_W2PRGB  = new TYData("DAT02_W2PRGB", null);
            this.DAT02_W2HIGB  = new TYData("DAT02_W2HIGB", null);
            this.DAT02_W2HISAB = new TYData("DAT02_W2HISAB", null);
            this.DAT02_W2GUBUN = new TYData("DAT02_W2GUBUN", null);
            this.DAT02_W2TXAMT = new TYData("DAT02_W2TXAMT", null);
            this.DAT02_W2TXVAT = new TYData("DAT02_W2TXVAT", null);
            this.DAT02_W2HWAJU = new TYData("DAT02_W2HWAJU", null);
        }

        private void TYACLO002I_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateACTION = new ToolStripMenuItem("차입금 실행관리 바로가기");
            reateACTION.Click += new EventHandler(reateACTION_ToolStripMenuItem_Click);

            this.FPS91_TY_S_AC_87HDV410.CurrentContextMenu.Items.AddRange(
            new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateACTION });






            ToolStripMenuItem reateLIQUID = new ToolStripMenuItem("차입금 유동성관리 바로가기");
            reateLIQUID.Click += new EventHandler(reateLIQUID_ToolStripMenuItem_Click);

            this.FPS91_TY_S_AC_87HDV410.CurrentContextMenu.Items.AddRange(
            new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateLIQUID });






            ToolStripMenuItem reateREPAY = new ToolStripMenuItem("차입금 상환관리 바로가기");
            reateREPAY.Click += new EventHandler(reateREPAY_ToolStripMenuItem_Click);

            this.FPS91_TY_S_AC_87HDV410.CurrentContextMenu.Items.AddRange(
            new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateREPAY });


            // 실행관리
            (this.FPS91_TY_S_AC_86PE9273.Sheets[0].Columns[21].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_86PE9273, "BTN");

            this.CBH01_LOACGRBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_LOACGRBK_CodeBoxDataBinded);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_CRE_ProcessCheck);
            this.BTN61_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_JUNPYO_CANCEL_ProcessCheck);

            this.CBH01_LOACDPAC.DummyValue = DateTime.Now.ToString("yyyy-MM-dd");





            // 유동성관리
            (this.FPS91_TY_S_AC_87BAJ355.Sheets[0].Columns[20].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_87BAJ355, "BTN");

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            this.BTN62_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN62_JPNO_CRE_ProcessCheck);
            this.BTN62_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN62_JUNPYO_CANCEL_ProcessCheck);

            this.CBH02_LOLIDPAC.DummyValue = DateTime.Now.ToString("yyyy-MM-dd");





            // 상환관리
            (this.FPS91_TY_S_AC_875F6331.Sheets[0].Columns[18].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_875F6331, "BTN");

            this.CBH03_LOREGRBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH03_LOREGRBK_CodeBoxDataBinded);

            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);

            this.BTN63_FIX.ProcessCheck += new TButton.CheckHandler(BTN63_FIX_ProcessCheck);

            this.BTN63_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN63_JPNO_CRE_ProcessCheck);
            this.BTN63_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN63_JUNPYO_CANCEL_ProcessCheck);
            
            this.CBH03_LOREDPAC.DummyValue = DateTime.Now.ToString("yyyy-MM-dd");



            //UP_LOCONTMF_DETAIL_ReadOnly();

            this.TXT01_STDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy"));
            this.TXT01_EDDATE.SetValue(DateTime.Now.ToString("yyyy"));

            UP_ControlFactory();

            UP_LABEL_DISPLAY(false, 0);

            fsTAB_GUBUN = "MAIN";
            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_STDATE);
        }
        #endregion

        #region Description : 차입금 관리 MAIN

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (fsTAB_GUBUN == "MAIN")
            {
                // 차입금 이력 조회
                UP_LOAN_MAIN_INQ();
            }
            else if (fsTAB_GUBUN == "LOACTIONMF")
            {
                // 실행관리 조회(여러건)
                UP_LOACTIONMF_INQ();
            }
            else if (fsTAB_GUBUN.ToString() == "LOLIQUIDMF") // 유동성관리 조회
            {
                // 유동성관리 조회(여러건)
                UP_LOLIQUIDMF_INQ();
            }
            else if (fsTAB_GUBUN == "LOREPAYMF")
            {
                // 상환관리 조회(여러건)
                UP_LOREPAYMF_INQ();
            }
        }
        #endregion

        #region Description : 차입금 이력조회
        private void UP_LOAN_MAIN_INQ()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_87HD7409", Get_Date(this.TXT01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.TXT01_EDDATE.GetValue().ToString()),
                                                        this.TXT01_LOCCONTNO.GetValue().ToString(),
                                                        this.CBH01_SBANK.GetValue().ToString()
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_87HDV410.SetValue(dt);

            string sSTATUSNM = string.Empty;

            for (int i = 0; i < this.FPS91_TY_S_AC_87HDV410.ActiveSheet.RowCount; i++)
            {
                sSTATUSNM = this.FPS91_TY_S_AC_87HDV410.GetValue(i, "STATUSNM").ToString();

                if (sSTATUSNM.ToString() == "실행" || sSTATUSNM.ToString() == "상환" || sSTATUSNM.ToString() == "유동성 대체" || sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_87HDV410_Sheet1.Cells[i, 3].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (sSTATUSNM.ToString() == "실행")
                {
                    this.FPS91_TY_S_AC_87HDV410_Sheet1.Cells[i, 3].ForeColor = Color.Blue;
                }
                else if (sSTATUSNM.ToString() == "상환")
                {
                    this.FPS91_TY_S_AC_87HDV410_Sheet1.Cells[i, 3].ForeColor = Color.Red;
                }
                else if (sSTATUSNM.ToString() == "유동성 대체")
                {
                    this.FPS91_TY_S_AC_87HDV410_Sheet1.Cells[i, 3].ForeColor = Color.LimeGreen;
                }
                else if (sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_87HDV410_Sheet1.Cells[i, 3].ForeColor = Color.Peru;
                }
            }
        }
        #endregion


        #region  Description : 차입금 실행관리 바로가기 이벤트
        private void reateACTION_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 실행금액
            if (double.Parse(Get_Numeric(this.FPS91_TY_S_AC_87HDV410.GetValue("LOACAMT").ToString())) != 0)
            {
                fsTAB_GUBUN = "LOACTIONMF";

                this.tabControl1.SelectedIndex = 1;

                this.TXT01_LOCCONTYEAR.SetReadOnly(true);

                fsWK_GUBUN1 = "TAB";
                UP_LOACTIONMF_BTN_DISPLAY(fsWK_GUBUN1);






                fsLOACCONTSEQ = "";

                // 관리번호
                this.TXT01_LOACCONTYEAR.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(0, 4));
                this.TXT01_LOACCONTSEQ.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(5, 2));
                this.TXT01_LOACNUM.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(8, 3));

                // 계약번호
                this.TXT01_LOCCONTYEAR.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(0, 4));
                this.TXT01_LOCCONTSEQ.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(5, 2));

                fsLOACCONTSEQ = this.FPS91_TY_S_AC_87HDV410.GetValue("SEQ").ToString();



                // 실행관리 조회(단일건)
                UP_LOACTIONMF_SEARCH();

                // 실행관리 확인
                UP_LOACTIONMF_RUN();
            }
        }
        #endregion




        #region  Description : 차입금 유동성관리 바로가기 이벤트
        private void reateLIQUID_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 대체금액
            if (double.Parse(Get_Numeric(this.FPS91_TY_S_AC_87HDV410.GetValue("LOLIAMT").ToString())) != 0)
            {
                fsTAB_GUBUN = "LOLIQUIDMF";

                this.tabControl1.SelectedIndex = 2;

                this.TXT01_LOCCONTYEAR.SetReadOnly(true);

                fsWK_GUBUN2 = "TAB";
                UP_LOLIQUIDMF_BTN_DISPLAY(fsWK_GUBUN2);







                fsLOLIACCONTSEQ = "";
                fsLOLICONTSEQ = "";

                // 유동성 관리번호
                this.TXT02_LOLICONTYEAR.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(0, 4));
                this.TXT02_LOLICONTSEQ.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(5, 2));
                this.TXT02_LOLINUM.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(8, 3));

                fsLOLICONTSEQ = this.FPS91_TY_S_AC_87HDV410.GetValue("SEQ").ToString();

                // 실행 관리번호
                this.TXT02_LOACCONTYEAR.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CTNO").ToString());
                this.TXT02_LOACCONTSEQ.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CTSEQ").ToString());
                this.TXT02_LOACNUM.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CTNUM").ToString());

                fsLOLIACCONTSEQ = this.FPS91_TY_S_AC_87HDV410.GetValue("CTSEQ").ToString();


                // 유동성관리 조회(단일건)
                UP_LOLIQUIDMF_SEARCH();

                // 유동성관리 확인
                UP_LOLIQUIDMF_RUN();
            }
        }
        #endregion



        #region  Description : 차입금 상환관리 바로가기 이벤트
        private void reateREPAY_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 상환금액
            if (double.Parse(Get_Numeric(this.FPS91_TY_S_AC_87HDV410.GetValue("LOREAMT").ToString())) != 0)
            {
                fsTAB_GUBUN = "LOREPAYMF";

                this.tabControl1.SelectedIndex = 3;

                this.TXT01_LOCCONTYEAR.SetReadOnly(true);

                fsWK_GUBUN3 = "TAB";
                UP_LOREPAYMF_BTN_DISPLAY(fsWK_GUBUN3);






                fsLORECONTSEQ = "";

                this.TXT03_LORECONTYEAR.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(0, 4));
                this.TXT03_LORECONTSEQ.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(5, 2));
                this.TXT03_LORENUM.SetValue(this.FPS91_TY_S_AC_87HDV410.GetValue("CONTNO").ToString().Substring(8, 3));

                fsLORECONTSEQ = this.FPS91_TY_S_AC_87HDV410.GetValue("SEQ").ToString();

                // 상환관리 조회(단일건)
                UP_LOREPAYMF_SEARCH();

                // 상환관리 확인
                UP_LOREPAYMF_RUN();


                // 상환 확정금액
                this.TXT03_MAAMOUNT.SetValue(Set_Numeric2(this.FPS91_TY_S_AC_87HDV410.GetValue("LOREAMT").ToString(), 0));

                // 상환 내역관리 확인
                UP_LOREPAYNF_RUN();
            }
        }
        #endregion

        #endregion








        #region Description : 차입금 실행관리

        #region Description : 실행관리 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            // 실행금액
            fsLOACAMT    = "0";
            fsLOACDOLLAR = "0";
            fsLOACGUBN   = "S";

            UP_LOACTIONMF_FieldClear();

            // 실행관리 필드 ReadOnly
            UP_LOACTIONMF_ReadOnly(false);

            fsWK_GUBUN1 = "NEW";
            UP_LOACTIONMF_BTN_DISPLAY(fsWK_GUBUN1);

            // 귀속부서 가져오기
            this.CBH01_LOACDPAC.SetValue(UP_GET_INKIBNMF("TEAM"));

            this.SetFocus(this.DTP01_LOACDATE);
        }
        #endregion

        #region Description : 실행관리 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN1.ToString() == "NEW")
            {
                UP_LOACTIONMF_SAV();

                this.TXT01_LOCCONTYEAR.SetReadOnly(true);
            }
            else if (fsWK_GUBUN1.ToString() == "UPT")
            {
                UP_LOACTIONMF_UPT();
            }

            // 실행관리 조회(단일건)
            UP_LOACTIONMF_SEARCH();

            UP_LOACTIONMF_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 실행관리 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_LOACTIONMF_DEL();

            // 실행관리 조회(여러건)
            UP_LOACTIONMF_INQ();

            UP_LOACTIONMF_FieldClear();

            UP_LOACTIONMF_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 실행관리 전표발행 버튼
        private void BTN61_JPNO_CRE_Click(object sender, EventArgs e)
        {
            string sB2DPMK = "";
            string sB2SSID = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            int iCnt = 0;

            // 부서코드 가져오기
            sB2DPMK = UP_GET_INKIBNMF("BUSEO");

            DataTable dt;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            this.DbConnector.CommandClear();

            #region Description : 차변 회계처리

            this.DAT02_W2SSID.SetValue(sB2SSID.ToString());
            this.DAT02_W2DPMK.SetValue(sB2DPMK.ToString());
            this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP01_LOACDATE.GetValue().ToString()));
            this.DAT02_W2NOSQ.SetValue("0");
            this.DAT02_W2NOLN.SetValue("1");
            this.DAT02_W2IDJP.SetValue("3");
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(this.CBH01_LOACCDAC.GetValue().ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(this.CBH01_LOACDPAC.GetValue().ToString());
            
            //관리항목 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH01_LOACCDAC.GetValue().ToString(), "");

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI1.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI2.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI3.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI4.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI5.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI6.SetValue("");
                }
            }
            dt.Clear();

            // 관리항목1
            this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목2
            this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목3
            this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목4
            this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목5
            this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목6
            this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), "LOACTIONMF", "", ""));

            this.DAT02_W2AMDR.SetValue(Get_Numeric(this.TXT01_LOACAMT.GetValue().ToString()));
            this.DAT02_W2AMCR.SetValue("0");

            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(this.TXT01_LOACRKAC.GetValue().ToString());
            this.DAT02_W2RKCU.SetValue("");
            this.DAT02_W2WCJP.SetValue("");
            this.DAT02_W2PRGB.SetValue("");
            this.DAT02_W2HIGB.SetValue("A");
            this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
            this.DAT02_W2GUBUN.SetValue("");
            this.DAT02_W2TXAMT.SetValue("0");
            this.DAT02_W2TXVAT.SetValue("0");
            this.DAT02_W2HWAJU.SetValue("");

            datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                    this.DAT02_W2DPMK.GetValue().ToString(),
                                    this.DAT02_W2DTMK.GetValue().ToString(),
                                    this.DAT02_W2NOSQ.GetValue().ToString(),
                                    this.DAT02_W2NOLN.GetValue().ToString(),
                                    this.DAT02_W2IDJP.GetValue().ToString(),
                                    this.DAT02_W2NOJP.GetValue().ToString(),
                                    this.DAT02_W2CDAC.GetValue().ToString(),
                                    this.DAT02_W2DTAC.GetValue().ToString(),
                                    this.DAT02_W2DTLI.GetValue().ToString(),
                                    this.DAT02_W2DPAC.GetValue().ToString(),
                                    this.DAT02_W2CDMI1.GetValue().ToString(),
                                    this.DAT02_W2VLMI1.GetValue().ToString(),
                                    this.DAT02_W2CDMI2.GetValue().ToString(),
                                    this.DAT02_W2VLMI2.GetValue().ToString(),
                                    this.DAT02_W2CDMI3.GetValue().ToString(),
                                    this.DAT02_W2VLMI3.GetValue().ToString(),
                                    this.DAT02_W2CDMI4.GetValue().ToString(),
                                    this.DAT02_W2VLMI4.GetValue().ToString(),
                                    this.DAT02_W2CDMI5.GetValue().ToString(),
                                    this.DAT02_W2VLMI5.GetValue().ToString(),
                                    this.DAT02_W2CDMI6.GetValue().ToString(),
                                    this.DAT02_W2VLMI6.GetValue().ToString(),
                                    this.DAT02_W2AMDR.GetValue().ToString(),
                                    this.DAT02_W2AMCR.GetValue().ToString(),
                                    this.DAT02_W2CDFD.GetValue().ToString(),
                                    this.DAT02_W2AMFD.GetValue().ToString(),
                                    this.DAT02_W2RKAC.GetValue().ToString(),
                                    this.DAT02_W2RKCU.GetValue().ToString(),
                                    this.DAT02_W2WCJP.GetValue().ToString(),
                                    this.DAT02_W2PRGB.GetValue().ToString(),
                                    this.DAT02_W2HIGB.GetValue().ToString(),
                                    this.DAT02_W2HISAB.GetValue().ToString(),
                                    this.DAT02_W2GUBUN.GetValue().ToString(),
                                    this.DAT02_W2TXAMT.GetValue().ToString(),
                                    this.DAT02_W2TXVAT.GetValue().ToString(),
                                    this.DAT02_W2HWAJU.GetValue().ToString()});

            #endregion

            #region Description : 대변 회계처리

            this.DAT02_W2SSID.SetValue(sB2SSID.ToString());
            this.DAT02_W2DPMK.SetValue(sB2DPMK.ToString());
            this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP01_LOACDATE.GetValue().ToString()));
            this.DAT02_W2NOSQ.SetValue("0");
            this.DAT02_W2NOLN.SetValue("2");
            this.DAT02_W2IDJP.SetValue("3");
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(this.CBH01_LOCCDAC.GetValue().ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(this.CBH01_LOACDPAC.GetValue().ToString());

            //관리항목 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH01_LOCCDAC.GetValue().ToString(), "");

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI1.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI2.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI3.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI4.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI5.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI6.SetValue("");
                }
            }
            dt.Clear();

            // 관리항목1
            this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목2
            this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목3
            this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목4
            this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목5
            this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), "LOACTIONMF", "", ""));
            // 관리항목6
            this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), "LOACTIONMF", "", ""));

            this.DAT02_W2AMDR.SetValue("0");
            this.DAT02_W2AMCR.SetValue(Get_Numeric(this.TXT01_LOACAMT.GetValue().ToString()));

            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(this.TXT01_LOACRKAC.GetValue().ToString());
            this.DAT02_W2RKCU.SetValue("");
            this.DAT02_W2WCJP.SetValue("");
            this.DAT02_W2PRGB.SetValue("");
            this.DAT02_W2HIGB.SetValue("A");
            this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
            this.DAT02_W2GUBUN.SetValue("");
            this.DAT02_W2TXAMT.SetValue("0");
            this.DAT02_W2TXVAT.SetValue("0");
            this.DAT02_W2HWAJU.SetValue("");

            datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                    this.DAT02_W2DPMK.GetValue().ToString(),
                                    this.DAT02_W2DTMK.GetValue().ToString(),
                                    this.DAT02_W2NOSQ.GetValue().ToString(),
                                    this.DAT02_W2NOLN.GetValue().ToString(),
                                    this.DAT02_W2IDJP.GetValue().ToString(),
                                    this.DAT02_W2NOJP.GetValue().ToString(),
                                    this.DAT02_W2CDAC.GetValue().ToString(),
                                    this.DAT02_W2DTAC.GetValue().ToString(),
                                    this.DAT02_W2DTLI.GetValue().ToString(),
                                    this.DAT02_W2DPAC.GetValue().ToString(),
                                    this.DAT02_W2CDMI1.GetValue().ToString(),
                                    this.DAT02_W2VLMI1.GetValue().ToString(),
                                    this.DAT02_W2CDMI2.GetValue().ToString(),
                                    this.DAT02_W2VLMI2.GetValue().ToString(),
                                    this.DAT02_W2CDMI3.GetValue().ToString(),
                                    this.DAT02_W2VLMI3.GetValue().ToString(),
                                    this.DAT02_W2CDMI4.GetValue().ToString(),
                                    this.DAT02_W2VLMI4.GetValue().ToString(),
                                    this.DAT02_W2CDMI5.GetValue().ToString(),
                                    this.DAT02_W2VLMI5.GetValue().ToString(),
                                    this.DAT02_W2CDMI6.GetValue().ToString(),
                                    this.DAT02_W2VLMI6.GetValue().ToString(),
                                    this.DAT02_W2AMDR.GetValue().ToString(),
                                    this.DAT02_W2AMCR.GetValue().ToString(),
                                    this.DAT02_W2CDFD.GetValue().ToString(),
                                    this.DAT02_W2AMFD.GetValue().ToString(),
                                    this.DAT02_W2RKAC.GetValue().ToString(),
                                    this.DAT02_W2RKCU.GetValue().ToString(),
                                    this.DAT02_W2WCJP.GetValue().ToString(),
                                    this.DAT02_W2PRGB.GetValue().ToString(),
                                    this.DAT02_W2HIGB.GetValue().ToString(),
                                    this.DAT02_W2HISAB.GetValue().ToString(),
                                    this.DAT02_W2GUBUN.GetValue().ToString(),
                                    this.DAT02_W2TXAMT.GetValue().ToString(),
                                    this.DAT02_W2TXVAT.GetValue().ToString(),
                                    this.DAT02_W2HWAJU.GetValue().ToString()});

            #endregion

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();

                foreach (object[] data in datas)
                {
                    // 미승인 임사피일에 등록
                    this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                }
            }

            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID,
                                                        this.ProgramNo,
                                                        Employer.EmpNo,
                                                        "A",
                                                        sB2DPMK.ToString(),
                                                        Get_Date(this.DTP01_LOACDATE.GetValue().ToString()),
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        Employer.EmpNo
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            //전표 생성 함수 호출
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);

                DataTable dtresult = this.DbConnector.ExecuteDataTable();

                if (dtresult.Rows.Count > 0)
                {
                    if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                    {
                        //전표번호 받아오기
                        string sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                        if (sJpno.Trim() != "")
                        {
                            // 전표번호
                            this.TXT01_LOACJPNO.SetValue(sJpno.ToString().Replace("-", "").ToString());

                            // 실행관리 전표번호 업데이트
                            UP_LOACTIONMF_UPT_JPNO();

                            // 실행관리 전표관련 버튼 DISPLAY
                            UP_LOACTIONMF_JPNO_BTN_DISPLAY(false, false, false, true);

                            // 실행관리 필드 ReadOnly
                            UP_LOACTIONMF_ReadOnly(true);

                            // 실행관리 조회
                            UP_LOACTIONMF_INQ();

                            this.ShowMessage("TY_M_AC_25O8K620");
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 실행관리 전표취소 버튼
        private void BTN61_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //미승인전표 -> 임시파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT01_LOACJPNO.GetValue().ToString().Substring(0,  6),
                                                                 this.TXT01_LOACJPNO.GetValue().ToString().Substring(6,  8),
                                                                 this.TXT01_LOACJPNO.GetValue().ToString().Substring(14, 3)
                                                                 );
            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID,
                                                        this.ProgramNo, Employer.EmpNo,
                                                        "D",
                                                        this.TXT01_LOACJPNO.GetValue().ToString().Substring(0, 6),
                                                        this.TXT01_LOACJPNO.GetValue().ToString().Substring(6, 8),
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        Employer.EmpNo);

            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, ""); // SP CALL
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                // 전표번호
                this.TXT01_LOACJPNO.SetValue("");

                // 실행관리 전표번호 업데이트
                UP_LOACTIONMF_UPT_JPNO();

                // 실행관리 전표관련 버튼 DISPLAY
                UP_LOACTIONMF_JPNO_BTN_DISPLAY(true, true, true, false);

                // 실행관리 필드 ReadOnly
                UP_LOACTIONMF_ReadOnly(false);

                // 실행관리 조회
                UP_LOACTIONMF_INQ();

                this.ShowMessage("TY_M_AC_25O8K620");
            }
        }
        #endregion

        #region Description : 실행관리 전표출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            if (this.TXT01_LOACJPNO.GetValue().ToString() != "")
            {
                string sJPNO = this.TXT01_LOACJPNO.GetValue().ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2AU2M916",
                    sJPNO.Substring(0, 6),
                    sJPNO.Substring(6, 8),
                    sJPNO.Substring(14, 3), // 시작 번호
                    sJPNO.Substring(14, 3)  // 종료 번호
                    );

                if (Convert.ToDouble(sJPNO.Substring(6, 4)) > 2014)
                {
                    SectionReport rpt = new TYACBJ0012R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
                else
                {
                    SectionReport rpt = new TYACBJ001R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Description : 실행관리 조회(여러건)
        private void UP_LOACTIONMF_INQ()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86PE7272",
                this.TXT01_STDATE.GetValue().ToString(),
                this.TXT01_EDDATE.GetValue().ToString(),
                this.TXT01_LOCCONTNO.GetValue().ToString(),
                this.CBH01_SBANK.GetValue().ToString(),
                this.MTB01_SDATE.GetValue().ToString().Replace("-", "").Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86PE9273.SetValue(dt);

            if (dt.Rows.Count <= 0)
            {
                // 실행관리 조회(단일건)
                UP_LOACTIONMF_SEARCH();
            }
        }
        #endregion

        #region Description : 실행관리 조회(단일건)
        private void UP_LOACTIONMF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86PG5275",
                this.TXT01_LOACCONTYEAR.GetValue().ToString(),
                this.TXT01_LOACCONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86PE9273.SetValue(dt);

            //if (dt.Rows.Count <= 0)
            //{
            //    // 실행관리 조회(여러건)
            //    UP_LOACTIONMF_INQ();
            //}
        }
        #endregion

        #region Description : 실행관리 확인
        private void UP_LOACTIONMF_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86PGG276",
                this.TXT01_LOACCONTYEAR.GetValue().ToString(),
                this.TXT01_LOACCONTSEQ.GetValue().ToString(),
                fsLOACCONTSEQ.ToString(),
                this.TXT01_LOACNUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsWK_GUBUN1 = "UPT";
                UP_LOACTIONMF_BTN_DISPLAY(fsWK_GUBUN1);

                // 실행금액
                fsLOACAMT    = dt.Rows[0]["LOACAMT"].ToString();
                fsLOACDOLLAR = dt.Rows[0]["LOACDOLLAR"].ToString();
                fsLOACGUBN   = dt.Rows[0]["LOACGUBN"].ToString();

                if (dt.Rows[0]["LOACJPNO"].ToString() == "")
                {
                    // 실행관리 전표관련 버튼 DISPLAY
                    UP_LOACTIONMF_JPNO_BTN_DISPLAY(true, true, true, false);

                    // 실행관리 필드 ReadOnly
                    UP_LOACTIONMF_ReadOnly(false);
                }
                else
                {
                    // 실행관리 전표관련 버튼 DISPLAY
                    UP_LOACTIONMF_JPNO_BTN_DISPLAY(false,false, false, true);

                    // 실행관리 필드 ReadOnly
                    UP_LOACTIONMF_ReadOnly(true);
                }

                this.SetFocus(this.CBH01_LOCBANKCD.CodeText);
            }
        }
        #endregion

        #region Description : 실행관리 저장
        private void UP_LOACTIONMF_SAV()
        {
            string sLOACCONTNO = string.Empty;

            sLOACCONTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86PDF271",
                sLOACCONTNO.ToString(),
                fsLOACCONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_LOACNUM.SetValue(dt.Rows[0]["SEQ"].ToString());
            }

            // 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86PDD268",
                                    sLOACCONTNO.ToString(),
                                    fsLOACCONTSEQ.ToString(),
                                    this.TXT01_LOACNUM.GetValue().ToString(),
                                    this.DTP01_LOACDATE.GetValue().ToString(),
                                    this.TXT01_LOACAMT.GetValue().ToString(),
                                    this.TXT01_LOACRATE.GetValue().ToString(),
                                    this.CBH01_LOACDPAC.GetValue().ToString(),
                                    this.TXT01_LOACYUL.GetValue().ToString(),
                                    this.TXT01_LOACDOLLAR.GetValue().ToString(),
                                    this.CBH01_LOACCDAC.GetValue().ToString(),
                                    this.CBH01_LOACGRBK.GetValue().ToString(),
                                    this.CBH01_LOACNOAC.GetValue().ToString(),
                                    this.TXT01_LOACRKAC.GetValue().ToString(),
                                    this.CBO01_LOACGUBN.GetValue().ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 실행관리 수정
        private void UP_LOACTIONMF_UPT()
        {
            string sLOACCONTNO = string.Empty;

            sLOACCONTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

            // 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86PDD269",
                                    this.DTP01_LOACDATE.GetValue().ToString(),
                                    this.TXT01_LOACAMT.GetValue().ToString(),
                                    this.TXT01_LOACRATE.GetValue().ToString(),
                                    this.CBH01_LOACDPAC.GetValue().ToString(),
                                    this.TXT01_LOACYUL.GetValue().ToString(),
                                    this.TXT01_LOACDOLLAR.GetValue().ToString(),
                                    this.CBH01_LOACCDAC.GetValue().ToString(),
                                    this.CBH01_LOACGRBK.GetValue().ToString(),
                                    this.CBH01_LOACNOAC.GetValue().ToString(),
                                    this.TXT01_LOACRKAC.GetValue().ToString(),
                                    this.CBO01_LOACGUBN.GetValue().ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                   // 작성사번
                                    sLOACCONTNO.ToString(),
                                    fsLOACCONTSEQ.ToString(),
                                    this.TXT01_LOACNUM.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 실행관리 삭제
        private void UP_LOACTIONMF_DEL()
        {
            string sLOACCONTNO = string.Empty;

            sLOACCONTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

            // 삭제 프로시저
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86PDE270", sLOACCONTNO.ToString(),
                                                        fsLOACCONTSEQ.ToString(),
                                                        this.TXT01_LOACNUM.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 실행관리 전표번호 업데이트
        private void UP_LOACTIONMF_UPT_JPNO()
        {
            string sLOACCONTNO = string.Empty;

            sLOACCONTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

            // 전표번호 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86QH3281",
                                    this.TXT01_LOACJPNO.GetValue().ToString(),
                                    sLOACCONTNO.ToString(),
                                    fsLOACCONTSEQ.ToString(),
                                    this.TXT01_LOACNUM.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 실행관리 스프레드 이벤트
        private void FPS91_TY_S_AC_86PE9273_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsLOACCONTSEQ = "";

            // 관리번호
            this.TXT01_LOACCONTYEAR.SetValue(this.FPS91_TY_S_AC_86PE9273.GetValue("LOACCONTNO").ToString().Substring(0, 4));
            this.TXT01_LOACCONTSEQ.SetValue(this.FPS91_TY_S_AC_86PE9273.GetValue("LOACCONTNO").ToString().Substring(5, 2));
            this.TXT01_LOACNUM.SetValue(this.FPS91_TY_S_AC_86PE9273.GetValue("LOACCONTNO").ToString().Substring(8, 3));

            // 계약번호
            this.TXT01_LOCCONTYEAR.SetValue(this.FPS91_TY_S_AC_86PE9273.GetValue("LOACCONTNO").ToString().Substring(0, 4));
            this.TXT01_LOCCONTSEQ.SetValue(this.FPS91_TY_S_AC_86PE9273.GetValue("LOACCONTNO").ToString().Substring(5, 2));

            fsLOACCONTSEQ = this.FPS91_TY_S_AC_86PE9273.GetValue("LOACCONTSEQ").ToString();

            // 실행관리 확인
            UP_LOACTIONMF_RUN();
        }
        #endregion

        #region Description : 실행관리 스프레드 전표 출력 이벤트
        private void FPS91_TY_S_AC_86PE9273_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "21")
            {
                if (this.FPS91_TY_S_AC_86PE9273.GetValue("LOACJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_86PE9273.GetValue("LOACJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_86PE9273.GetValue("LOACJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_86PE9273.GetValue("LOACJPNO").ToString().Substring(14, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        SectionReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        SectionReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }
            }
        }
        #endregion

        #region Description : 실행관리 - 계약조회 코드헬프
        private void BTN61_CODEHELP_Click(object sender, EventArgs e)
        {
            TYACLO01C1 popup = new TYACLO01C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fsLOCCONTSEQ  = "";
                fsLOACCONTSEQ = "";

                this.TXT01_LOCCONTYEAR.SetValue(popup.fsLOCCONTYEAR); // 계약년도
                this.TXT01_LOCCONTSEQ.SetValue(popup.fsLOCCONTSEQ);   // 계약번호
                fsLOCCONTSEQ = popup.fsLOCCONTNUM.ToString();         // 계약순번

                this.TXT01_LOACCONTYEAR.SetValue(popup.fsLOCCONTYEAR); // 계약년도
                this.TXT01_LOACCONTSEQ.SetValue(popup.fsLOCCONTSEQ);   // 계약번호
                fsLOACCONTSEQ = popup.fsLOCCONTNUM.ToString();         // 계약순번

                // 계약 최종 DATA 확인
                UP_LOCONTMF_FINAL_RUN(this.TXT01_LOCCONTYEAR.GetValue().ToString(), this.TXT01_LOCCONTSEQ.GetValue().ToString(), "LOACTIONMF");
            }
        }
        #endregion

        #region Description : 실행일자 이벤트
        private void DTP01_LOACDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_LOACDPAC.DummyValue = this.DTP01_LOACDATE.GetString();
        }
        #endregion

        #region Description : 실행관리 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sLOACAMT    = string.Empty;
            string sLOACDOLLAR = string.Empty;

            DataTable dt = new DataTable();

            // 계약번호 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IHQ241",
                this.TXT01_LOACCONTYEAR.GetValue().ToString(),
                this.TXT01_LOACCONTSEQ.GetValue().ToString(),
                fsLOACCONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_MR_2BE2E304");
                this.TXT01_LOACNUM.Focus();

                e.Successed = false;
                return;
            }

            // 전표계정 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_88NBH624",
                this.CBH01_LOACCDAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_24RBZ877");
                this.CBH01_LOACCDAC.Focus();

                e.Successed = false;
                return;
            }

            // 은행지점 체크
            if (this.CBH01_LOACGRBK.GetValue().ToString() != this.CBH01_LOCGRBK.GetValue().ToString())
            {
                this.ShowMessage("TY_M_AC_86IDZ231");
                this.CBH01_LOACGRBK.Focus();

                e.Successed = false;
                return;
            }

            if (fsWK_GUBUN1.ToString() == "NEW")
            {
                // 차입유형이 일반일 경우 실행(돈 빌리는거)은 한번만 가능
                if (this.CBH01_LOCLOANTYPE.GetValue().ToString() == "A") // 일반
                {
                    string sLOACCONTNO  = string.Empty;
                    string sLOACCONTSEQ = string.Empty;

                    sLOACCONTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());
                    sLOACCONTSEQ = Set_Fill3(fsLOACCONTSEQ.ToString());

                    // 이 계약건으로 실행이 여러건 이루어졌을 경우 확인
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_87UET486",
                        sLOACCONTNO.ToString(),
                        sLOACCONTSEQ.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count >= 1)
                    {
                        this.ShowMessage("TY_M_AC_87UF9488");
                        this.TXT01_LOACRKAC.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (fsWK_GUBUN1.ToString() == "UPT")
            {
                // 전표번호 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_86PGG276",
                    this.TXT01_LOACCONTYEAR.GetValue().ToString(),
                    this.TXT01_LOACCONTSEQ.GetValue().ToString(),
                    fsLOACCONTSEQ.ToString(),
                    this.TXT01_LOACNUM.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LOACJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_GB_25F8V482");
                        this.TXT01_LOACRKAC.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }


            // 20190415 수정전 소스
            //// 계약관리의 약정금액보다 실행금액이 초과할 수 없다.
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_86QD4280",
            //    this.TXT01_LOACCONTYEAR.GetValue().ToString(),
            //    this.TXT01_LOACCONTSEQ.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    sLOACAMT    = dt.Rows[0]["LOACAMT"].ToString();
            //    sLOACDOLLAR = dt.Rows[0]["LOACDOLLAR"].ToString();
            //}

            string sCONTNO = string.Empty;

            sCONTNO = this.TXT01_LOACCONTYEAR.GetValue().ToString() + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

            // 20190415 수정후 소스
            // 계약관리의 약정금액보다 잔액(실행-상환) + 실행금액이 초과할 수 없다.
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_94FEI371",
                sCONTNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sLOACAMT = dt.Rows[0]["JANAMT"].ToString();
            }
            
            if(fsWK_GUBUN1.ToString() == "NEW")
            {
                if (this.CBH01_LOCCURRTYPE.GetValue().ToString() == "1") // 원화
                {
                    if (double.Parse(Get_Numeric(this.TXT01_LOCCONTAMT.GetValue().ToString())) < (double.Parse(Get_Numeric(sLOACAMT.ToString())) + double.Parse(Get_Numeric(this.TXT01_LOACAMT.GetValue().ToString()))))
                    {
                        this.ShowMessage("TY_M_AC_86QBS279");
                        this.TXT01_LOACAMT.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 20190415 수정전 소스
                //else // 외화
                //{
                //    if (double.Parse(Get_Numeric(this.TXT01_LOCCONTDAL.GetValue().ToString())) < (double.Parse(Get_Numeric(sLOACDOLLAR.ToString())) + double.Parse(Get_Numeric(this.TXT01_LOACDOLLAR.GetValue().ToString()))))
                //    {
                //        this.ShowMessage("TY_M_AC_86QBS279");
                //        this.TXT01_LOACDOLLAR.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
            }
            else
            {
                if (this.CBH01_LOCCURRTYPE.GetValue().ToString() == "1") // 원화
                {
                    if (double.Parse(Get_Numeric(this.TXT01_LOCCONTAMT.GetValue().ToString())) < (double.Parse(Get_Numeric(sLOACAMT.ToString())) - double.Parse(Get_Numeric(fsLOACAMT.ToString())) + double.Parse(Get_Numeric(this.TXT01_LOACAMT.GetValue().ToString()))))
                    {
                        this.ShowMessage("TY_M_AC_86QBS279");
                        this.TXT01_LOACAMT.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 20190415 수정전 소스
                //else // 외화
                //{
                //    if (double.Parse(Get_Numeric(this.TXT01_LOCCONTDAL.GetValue().ToString())) < (double.Parse(Get_Numeric(sLOACDOLLAR.ToString())) - double.Parse(Get_Numeric(fsLOACDOLLAR.ToString())) + double.Parse(Get_Numeric(this.TXT01_LOACDOLLAR.GetValue().ToString()))))
                //    {
                //        this.ShowMessage("TY_M_AC_86QBS279");
                //        this.TXT01_LOACDOLLAR.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
            }

            if (double.Parse(Get_Numeric(this.TXT01_LOACYUL.GetValue().ToString())) != 0)
            {
                if (double.Parse(Get_Numeric(this.TXT01_LOACDOLLAR.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    this.TXT01_LOACDOLLAR.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (double.Parse(Get_Numeric(this.TXT01_LOACDOLLAR.GetValue().ToString())) != 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    this.TXT01_LOACDOLLAR.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 약정금액과 실행금액 비교
            if (this.CBH01_LOCCURRTYPE.GetValue().ToString() == "1") // 원화
            {
                if (double.Parse(Get_Numeric(this.TXT01_LOCCONTAMT.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_LOACAMT.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_AC_86QBS279");
                    this.TXT01_LOACAMT.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (double.Parse(Get_Numeric(this.TXT01_LOCCONTDAL.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_LOACDOLLAR.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_AC_86QBS279");
                    this.TXT01_LOACDOLLAR.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 차입금 은행과 연결된 계좌번호 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86R95282",
                this.CBH01_LOACGRBK.GetValue().ToString(),
                this.CBH01_LOACNOAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_86R9J284");
                SetFocus(this.CBH01_LOACGRBK.CodeText);

                e.Successed = false;
                return;
            }

            if (fsWK_GUBUN1.ToString() == "UPT")
            {
                string sLORENCTNO = string.Empty;

                sLORENCTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

                // 상환 DATA 존재하면 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_8889T522",
                    sLORENCTNO.ToString(),
                    fsLOACCONTSEQ.ToString(),
                    this.TXT01_LOACNUM.GetValue().ToString(),
                    "01"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_8889W523");

                    e.Successed = false;
                    return;
                }
            }
            

            if (fsWK_GUBUN1.ToString() == "NEW")
            {
                // 저장하시겠습니까?
                if (!this.ShowMessage("TY_M_GB_23NAD871"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                // 수정하시겠습니까?
                if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 실행관리 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 계약번호 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IHQ241",
                this.TXT01_LOACCONTYEAR.GetValue().ToString(),
                this.TXT01_LOACCONTSEQ.GetValue().ToString(),
                fsLOACCONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_MR_2BE2E304");
                this.TXT01_LOACNUM.Focus();

                e.Successed = false;
                return;
            }

            string sLORENCTNO = string.Empty;

            sLORENCTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

            // 상환 DATA 존재하면 삭제 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_8889T522",
                sLORENCTNO.ToString(),
                fsLOACCONTSEQ.ToString(),
                this.TXT01_LOACNUM.GetValue().ToString(),
                "01"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_8889W523");

                e.Successed = false;
                return;
            }


            // 전표번호 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86PGG276",
                this.TXT01_LOACCONTYEAR.GetValue().ToString(),
                this.TXT01_LOACCONTSEQ.GetValue().ToString(),
                fsLOACCONTSEQ.ToString(),
                this.TXT01_LOACNUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LOACJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    this.TXT01_LOACRKAC.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 실행관리 전표생성 ProcessCheck
        private void BTN61_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.fsLOACGUBN.ToString() == "D")
            {
                this.ShowMessage("TY_M_AC_94G90377");
                this.TXT01_LOACRKAC.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                string sLOACAMT = string.Empty;
                string sLOACDOLLAR = string.Empty;

                DataTable dt = new DataTable();

                // 차입유형이 일반일 경우 실행(돈 빌리는거)은 한번만 가능
                if (this.CBH01_LOCLOANTYPE.GetValue().ToString() == "A") // 일반
                {
                    string sLOACCONTNO = string.Empty;
                    string sLOACCONTSEQ = string.Empty;

                    sLOACCONTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());
                    sLOACCONTSEQ = Set_Fill3(fsLOACCONTSEQ.ToString());

                    // 이 계약건으로 실행이 여러건 이루어졌을 경우 확인
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_87UET486",
                        sLOACCONTNO.ToString(),
                        sLOACCONTSEQ.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count >= 1)
                    {
                        this.ShowMessage("TY_M_AC_87UF9488");
                        this.TXT01_LOACRKAC.Focus();

                        e.Successed = false;
                        return;
                    }
                }



                // 20190415 수정전 소스
                //// 계약관리의 약정금액보다 실행금액이 초과할 수 없다.
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_AC_86QD4280",
                //    this.TXT01_LOACCONTYEAR.GetValue().ToString(),
                //    this.TXT01_LOACCONTSEQ.GetValue().ToString()
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    sLOACAMT    = dt.Rows[0]["LOACAMT"].ToString();

                //    sLOACDOLLAR = dt.Rows[0]["LOACDOLLAR"].ToString();
                //}

                string sCONTNO = string.Empty;

                sCONTNO = this.TXT01_LOACCONTYEAR.GetValue().ToString() + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

                sLOACAMT = "0";

                // 20190415 수정후 소스
                // 계약관리의 약정금액보다 잔액(실행-상환) + 실행금액이 초과할 수 없다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_94FEI371",
                    sCONTNO.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sLOACAMT = dt.Rows[0]["JANAMT"].ToString();
                }

                if (this.CBH01_LOCCURRTYPE.GetValue().ToString() == "1") // 원화
                {
                    if (double.Parse(Get_Numeric(this.TXT01_LOCCONTAMT.GetValue().ToString())) < (double.Parse(Get_Numeric(sLOACAMT.ToString())) - double.Parse(Get_Numeric(fsLOACAMT.ToString())) + double.Parse(Get_Numeric(this.TXT01_LOACAMT.GetValue().ToString()))))
                    {
                        this.ShowMessage("TY_M_AC_86QBS279");
                        this.TXT01_LOACAMT.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 20190415 수정전 소스
                //else // 외화
                //{
                //    if (double.Parse(Get_Numeric(this.TXT01_LOCCONTDAL.GetValue().ToString())) < (double.Parse(sLOACDOLLAR.ToString()) - double.Parse(fsLOACDOLLAR.ToString()) + double.Parse(Get_Numeric(this.TXT01_LOACDOLLAR.GetValue().ToString()))))
                //    {
                //        this.ShowMessage("TY_M_AC_86QBS279");
                //        this.TXT01_LOACDOLLAR.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
            }

            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 실행관리 전표취소 ProcessCheck
        private void BTN61_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.fsLOACGUBN.ToString() == "D")
            {
                this.ShowMessage("TY_M_AC_94G90377");
                this.TXT01_LOACRKAC.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                DataTable dt = new DataTable();

                // 차입금 상환관리 DATA 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_86RAF290",
                    this.TXT01_LOACJPNO.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_86RAJ291");

                    e.Successed = false;
                    return;
                }

                string sLORENCTNO = string.Empty;

                sLORENCTNO = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString());

                // 상환 DATA 존재하면 전표 취소 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_8889T522",
                    sLORENCTNO.ToString(),
                    fsLOACCONTSEQ.ToString(),
                    this.TXT01_LOACNUM.GetValue().ToString(),
                    "01"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_8889W523");

                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_AC_25O8K619"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 실행관리 필드 클리어
        private void UP_LOACTIONMF_FieldClear()
        {
            fsLOACCONTSEQ = "";

            // 계약사항 필드 클리어
            this.TXT01_LOCCONTYEAR.SetValue("");
            this.TXT01_LOCCONTSEQ.SetValue("");

            this.CBH01_LOCBANKCD.SetValue("");
            this.CBH01_LOCGRBK.SetValue("");
            this.CBH01_LOCCURRTYPE.SetValue("");
            this.CBO01_LOCGIGANTYPE.SetValue("");
            this.CBO01_LOCGIGANTYPE.SetValue("");
            this.CBH01_LOCLOANTYPE.SetValue("");
            this.CBH01_LOCLOAN.SetValue("");
            this.CBH01_LOCCDAC.SetValue("");
            this.TXT01_LOCCONTRATE.SetValue("");
            this.DTP01_LOCCONTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_LOCENDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBO01_LOCCOLLGB.SetValue("");
            this.CBH01_LOCCURRCD.SetValue("");
            this.TXT01_LOCCONTYUL.SetValue("");
            this.CBO01_LOCRATEGB.SetValue("");
            this.TXT01_LOCCONTAMT.SetValue("");
            this.TXT01_LOCCONTDAL.SetValue("");
            this.TXT01_LOCCREDITAMT.SetValue("");
            this.TXT01_LOCRATEDAY.SetValue("");
            this.CBH01_LOCDESCGB.SetValue("");

            // 실행관리 필드 클리어
            this.TXT01_LOACCONTYEAR.SetValue("");
            this.TXT01_LOACCONTSEQ.SetValue("");
            this.TXT01_LOACNUM.SetValue("");

            this.DTP01_LOACDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_LOACAMT.SetValue("");
            this.TXT01_LOACRATE.SetValue("");
            this.CBH01_LOACDPAC.SetValue("");
            this.TXT01_LOACYUL.SetValue("");
            this.TXT01_LOACDOLLAR.SetValue("");
            this.CBH01_LOACCDAC.SetValue("");
            this.CBH01_LOACGRBK.SetValue("");
            this.CBH01_LOACNOAC.SetValue("");
            this.TXT01_LOACRKAC.SetValue("");
            this.TXT01_LOACJPNO.SetValue("");
        }
        #endregion

        #region Description : 실행관리 필드 ReadOnly
        private void UP_LOACTIONMF_ReadOnly(bool bFalse)
        {
            this.TXT01_LOACNUM.SetReadOnly(bFalse);

            this.DTP01_LOACDATE.SetReadOnly(bFalse);
            this.TXT01_LOACAMT.SetReadOnly(bFalse);
            this.TXT01_LOACRATE.SetReadOnly(bFalse);
            this.CBH01_LOACDPAC.SetReadOnly(bFalse);
            this.TXT01_LOACYUL.SetReadOnly(bFalse);
            this.TXT01_LOACDOLLAR.SetReadOnly(bFalse);
            this.CBH01_LOACCDAC.SetReadOnly(bFalse);
            this.CBH01_LOACGRBK.SetReadOnly(bFalse);
            this.CBH01_LOACNOAC.SetReadOnly(bFalse);
            this.TXT01_LOACRKAC.SetReadOnly(bFalse);
            this.CBO01_LOACGUBN.SetReadOnly(bFalse);
        }
        #endregion

        #region Description : 실행관리 버튼 디스플레이
        private void UP_LOACTIONMF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible  = true;
                this.BTN61_REM.Visible  = false;

                // 실행관리 전표관련 버튼 DISPLAY
                UP_LOACTIONMF_JPNO_BTN_DISPLAY(true, false, false, false);

                this.BTN61_CODEHELP.Visible = true;

                this.TXT01_LOACNUM.SetReadOnly(false);
                this.CBH01_LOACNOAC.SetReadOnly(true);
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible  = true;
                this.BTN61_REM.Visible  = true;

                this.TXT01_LOACNUM.SetReadOnly(true);
                this.CBH01_LOACNOAC.SetReadOnly(false);

                // 실행관리 전표관련 버튼 DISPLAY
                UP_LOACTIONMF_JPNO_BTN_DISPLAY(true, true, false, false);

                this.BTN61_CODEHELP.Visible = false;
            }
            else
            {
                this.BTN61_SAV.Visible  = false;
                this.BTN61_REM.Visible  = false;

                // 실행관리 전표관련 버튼 DISPLAY
                UP_LOACTIONMF_JPNO_BTN_DISPLAY(false, false, false, false);

                this.BTN61_CODEHELP.Visible = false;

                this.TXT01_LOACNUM.SetReadOnly(true);
                this.CBH01_LOACNOAC.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 실행관리 전표관련 버튼 DISPLAY
        private void UP_LOACTIONMF_JPNO_BTN_DISPLAY(bool bflag1, bool bflag2, bool bflag3, bool bflag4)
        {
            this.BTN61_SAV.Visible = bflag1;
            this.BTN61_REM.Visible = bflag2;

            this.BTN61_JPNO_CRE.Visible      = bflag3;
            this.BTN61_JUNPYO_CANCEL.Visible = bflag4;
            this.BTN61_PRT.Visible           = bflag4;
        }
        #endregion

        #region Description : 실행관리 상환방법 텍스트박스 이벤트
        private void CBH01_LOCDESCGB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        #region Description : 실행관리 계좌번호 이벤트
        private void CBH01_LOACGRBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = string.Empty;

            if (this.CBH01_LOACGRBK.GetValue().ToString().Length >= 3)
            {
                groupCode = this.CBH01_LOACGRBK.GetValue().ToString().Substring(0, 3).ToString();
            }

            this.CBH01_LOACNOAC.DummyValue = groupCode;
            this.CBH01_LOACNOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_LOACNOAC.Initialize();
        }
        #endregion

        #region Description : 실행관리 적요 이벤트
        private void TXT01_LOACRKAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        #endregion







        #region Description : 차입금 유동성 대체 관리

        #region Description : 유동성 대체 관리 신규 버튼
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            // 대체금액
            fsLOLIAMT = "0";

            UP_LOLIQUIDMF_FieldClear();

            // 유동성관리 필드 ReadOnly
            UP_LOLIQUIDMF_ReadOnly(false);

            // 귀속부서 가져오기
            this.CBH02_LOLIDPAC.SetValue(UP_GET_INKIBNMF("TEAM"));

            fsWK_GUBUN2 = "NEW";
            UP_LOLIQUIDMF_BTN_DISPLAY(fsWK_GUBUN2);
        }
        #endregion

        #region Description : 유동성 대체 관리 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN2.ToString() == "NEW")
            {
                UP_LOLIQUIDMF_SAV();
            }
            else if (fsWK_GUBUN2.ToString() == "UPT")
            {
                UP_LOLIQUIDMF_UPT();
            }

            if (fsSTATUS == "재대체")
            {
                UP_LORELIQUIDMF_UPT();
            }

            // 유동성관리 조회(단일건)
            UP_LOLIQUIDMF_SEARCH();

            fsWK_GUBUN2 = "";
            UP_LOLIQUIDMF_BTN_DISPLAY(fsWK_GUBUN2);
        }
        #endregion

        #region Description : 유동성 대체 관리 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            UP_LOLIQUIDMF_DEL();

            // 유동성관리 조회(여러건)
            UP_LOLIQUIDMF_INQ();

            UP_LOLIQUIDMF_FieldClear();

            fsWK_GUBUN2 = "";
            UP_LOLIQUIDMF_BTN_DISPLAY(fsWK_GUBUN2);
        }
        #endregion

        #region Description : 유동성 대체 관리 전표생성 버튼
        private void BTN62_JPNO_CRE_Click(object sender, EventArgs e)
        {
            string sB2DPMK = "";
            string sB2SSID = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            int iCnt = 0;

            // 부서코드 가져오기
            sB2DPMK = UP_GET_INKIBNMF("BUSEO");

            DataTable dt = new DataTable();
            DataTable dt_REPAY = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            this.DbConnector.CommandClear();




            int iW2NOLN = 0;

            
            #region Description : 차변 회계처리

            iW2NOLN = iW2NOLN + 1;

            this.DAT02_W2SSID.SetValue(sB2SSID.ToString());
            this.DAT02_W2DPMK.SetValue(sB2DPMK.ToString());
            this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP02_LOLIDATE.GetValue().ToString()));
            this.DAT02_W2NOSQ.SetValue("0");
            this.DAT02_W2NOLN.SetValue(Convert.ToString(iW2NOLN));
            this.DAT02_W2IDJP.SetValue("3");
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(this.CBH02_LOCCDAC.GetValue().ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(this.CBH02_LOLIDPAC.GetValue().ToString());

            // 계약관리의 약정계정과목 관리항목 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH02_LOCCDAC.GetValue().ToString(), "");

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI1.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI2.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI3.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI4.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI5.SetValue("");
                }

                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI6.SetValue("");
                }
            }
            dt.Clear();

            // 관리항목1
            this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목2
            this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목3
            this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목4
            this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목5
            this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목6
            this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), "LOLIQUIDMF", "", ""));

            this.DAT02_W2AMDR.SetValue(Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString()));
            this.DAT02_W2AMCR.SetValue("0");

            string sJPNO       = string.Empty;
            string sW2NOLN     = string.Empty;

            string sLOLICONTNO = string.Empty;

            string sProcedure  = string.Empty;

            sLOLICONTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            // 실행 또는 유동성 대체번호 가져오기
            if (fsSTATUS == "실행")
            {
                sProcedure = "TY_P_AC_D13EP462";
            }
            else
            {
                sProcedure = "TY_P_AC_D14AY473";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure, sLOLICONTNO.ToString(),
                                                fsLOLICONTSEQ.ToString(),
                                                this.TXT02_LOLINUM.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sJPNO = dt.Rows[0]["JPNO"].ToString();
            }
            dt.Clear();

            // 실행의 계정에 대한 대변계정(상대계정)을 가져와야 함
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_B5HBA340", sJPNO.ToString().Substring(0, 6),
                                                        sJPNO.ToString().Substring(6, 8),
                                                        sJPNO.ToString().Substring(14, 3),
                                                        this.CBH02_LOCCDAC.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sW2NOLN = Set_Fill2(dt.Rows[0]["B2NOLN"].ToString());
            }
            dt.Clear();


            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(this.TXT02_LOLIRKAC.GetValue().ToString());
            this.DAT02_W2RKCU.SetValue("");
            this.DAT02_W2WCJP.SetValue(sJPNO.ToString() + sW2NOLN.ToString());
            this.DAT02_W2PRGB.SetValue("");
            this.DAT02_W2HIGB.SetValue("A");
            this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
            this.DAT02_W2GUBUN.SetValue("");
            this.DAT02_W2TXAMT.SetValue("0");
            this.DAT02_W2TXVAT.SetValue("0");
            this.DAT02_W2HWAJU.SetValue("");

            datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                        this.DAT02_W2DPMK.GetValue().ToString(),
                        this.DAT02_W2DTMK.GetValue().ToString(),
                        this.DAT02_W2NOSQ.GetValue().ToString(),
                        this.DAT02_W2NOLN.GetValue().ToString(),
                        this.DAT02_W2IDJP.GetValue().ToString(),
                        this.DAT02_W2NOJP.GetValue().ToString(),
                        this.DAT02_W2CDAC.GetValue().ToString(),
                        this.DAT02_W2DTAC.GetValue().ToString(),
                        this.DAT02_W2DTLI.GetValue().ToString(),
                        this.DAT02_W2DPAC.GetValue().ToString(),
                        this.DAT02_W2CDMI1.GetValue().ToString(),
                        this.DAT02_W2VLMI1.GetValue().ToString(),
                        this.DAT02_W2CDMI2.GetValue().ToString(),
                        this.DAT02_W2VLMI2.GetValue().ToString(),
                        this.DAT02_W2CDMI3.GetValue().ToString(),
                        this.DAT02_W2VLMI3.GetValue().ToString(),
                        this.DAT02_W2CDMI4.GetValue().ToString(),
                        this.DAT02_W2VLMI4.GetValue().ToString(),
                        this.DAT02_W2CDMI5.GetValue().ToString(),
                        this.DAT02_W2VLMI5.GetValue().ToString(),
                        this.DAT02_W2CDMI6.GetValue().ToString(),
                        this.DAT02_W2VLMI6.GetValue().ToString(),
                        this.DAT02_W2AMDR.GetValue().ToString(),
                        this.DAT02_W2AMCR.GetValue().ToString(),
                        this.DAT02_W2CDFD.GetValue().ToString(),
                        this.DAT02_W2AMFD.GetValue().ToString(),
                        this.DAT02_W2RKAC.GetValue().ToString(),
                        this.DAT02_W2RKCU.GetValue().ToString(),
                        this.DAT02_W2WCJP.GetValue().ToString(),
                        this.DAT02_W2PRGB.GetValue().ToString(),
                        this.DAT02_W2HIGB.GetValue().ToString(),
                        this.DAT02_W2HISAB.GetValue().ToString(),
                        this.DAT02_W2GUBUN.GetValue().ToString(),
                        this.DAT02_W2TXAMT.GetValue().ToString(),
                        this.DAT02_W2TXVAT.GetValue().ToString(),
                        this.DAT02_W2HWAJU.GetValue().ToString()});

            #endregion

            #region Description : 대변 회계처리

            iW2NOLN = iW2NOLN + 1;

            this.DAT02_W2SSID.SetValue(sB2SSID.ToString());
            this.DAT02_W2DPMK.SetValue(sB2DPMK.ToString());
            this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP02_LOLIDATE.GetValue().ToString()));
            this.DAT02_W2NOSQ.SetValue("0");
            this.DAT02_W2NOLN.SetValue(Convert.ToString(iW2NOLN));
            this.DAT02_W2IDJP.SetValue("3");
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(this.CBH02_LOLICDAC.GetValue().ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(this.CBH02_LOLIDPAC.GetValue().ToString());

            // 유동성 대체관리 상대계정과목 관리항목
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH02_LOLICDAC.GetValue().ToString(), "");

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI1.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI2.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI3.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI4.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI5.SetValue("");
                }
                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                {
                    this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                }
                else
                {
                    this.DAT02_W2CDMI6.SetValue("");
                }
            }
            dt.Clear();

            // 관리항목1
            this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목2
            this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목3
            this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목4
            this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목5
            this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), "LOLIQUIDMF", "", ""));
            // 관리항목6
            this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), "LOLIQUIDMF", "", ""));

            this.DAT02_W2AMDR.SetValue("0");
            this.DAT02_W2AMCR.SetValue(Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString()));

            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(this.TXT03_LORERKAC.GetValue().ToString());
            this.DAT02_W2RKCU.SetValue("");
            this.DAT02_W2WCJP.SetValue("");
            this.DAT02_W2PRGB.SetValue("");
            this.DAT02_W2HIGB.SetValue("A");
            this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
            this.DAT02_W2GUBUN.SetValue("");
            this.DAT02_W2TXAMT.SetValue("0");
            this.DAT02_W2TXVAT.SetValue("0");
            this.DAT02_W2HWAJU.SetValue("");

            datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                        this.DAT02_W2DPMK.GetValue().ToString(),
                        this.DAT02_W2DTMK.GetValue().ToString(),
                        this.DAT02_W2NOSQ.GetValue().ToString(),
                        this.DAT02_W2NOLN.GetValue().ToString(),
                        this.DAT02_W2IDJP.GetValue().ToString(),
                        this.DAT02_W2NOJP.GetValue().ToString(),
                        this.DAT02_W2CDAC.GetValue().ToString(),
                        this.DAT02_W2DTAC.GetValue().ToString(),
                        this.DAT02_W2DTLI.GetValue().ToString(),
                        this.DAT02_W2DPAC.GetValue().ToString(),
                        this.DAT02_W2CDMI1.GetValue().ToString(),
                        this.DAT02_W2VLMI1.GetValue().ToString(),
                        this.DAT02_W2CDMI2.GetValue().ToString(),
                        this.DAT02_W2VLMI2.GetValue().ToString(),
                        this.DAT02_W2CDMI3.GetValue().ToString(),
                        this.DAT02_W2VLMI3.GetValue().ToString(),
                        this.DAT02_W2CDMI4.GetValue().ToString(),
                        this.DAT02_W2VLMI4.GetValue().ToString(),
                        this.DAT02_W2CDMI5.GetValue().ToString(),
                        this.DAT02_W2VLMI5.GetValue().ToString(),
                        this.DAT02_W2CDMI6.GetValue().ToString(),
                        this.DAT02_W2VLMI6.GetValue().ToString(),
                        this.DAT02_W2AMDR.GetValue().ToString(),
                        this.DAT02_W2AMCR.GetValue().ToString(),
                        this.DAT02_W2CDFD.GetValue().ToString(),
                        this.DAT02_W2AMFD.GetValue().ToString(),
                        this.DAT02_W2RKAC.GetValue().ToString(),
                        this.DAT02_W2RKCU.GetValue().ToString(),
                        this.DAT02_W2WCJP.GetValue().ToString(),
                        this.DAT02_W2PRGB.GetValue().ToString(),
                        this.DAT02_W2HIGB.GetValue().ToString(),
                        this.DAT02_W2HISAB.GetValue().ToString(),
                        this.DAT02_W2GUBUN.GetValue().ToString(),
                        this.DAT02_W2TXAMT.GetValue().ToString(),
                        this.DAT02_W2TXVAT.GetValue().ToString(),
                        this.DAT02_W2HWAJU.GetValue().ToString()});


            #endregion
                

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();

                foreach (object[] data in datas)
                {
                    // 미승인 임사피일에 등록
                    this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                }
            }

            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID,
                                                        this.ProgramNo,
                                                        Employer.EmpNo,
                                                        "A",
                                                        sB2DPMK.ToString(),
                                                        Get_Date(this.DTP02_LOLIDATE.GetValue().ToString()),
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        Employer.EmpNo
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            //전표 생성 함수 호출
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);

                DataTable dtresult = this.DbConnector.ExecuteDataTable();

                if (dtresult.Rows.Count > 0)
                {
                    if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                    {
                        //전표번호 받아오기
                        string sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();

                        if (sJpno.Trim() != "")
                        {
                            // 전표번호
                            this.TXT02_LOLIJPNO.SetValue(sJpno.ToString().Replace("-", "").ToString());

                            // 유동성관리 전표번호 업데이트
                            UP_LOLIQUIDMF_UPT_JPNO();

                            // 유동성관리 전표관련 버튼 DISPLAY
                            UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(false, false, false, true);

                            // 유동성관리 필드 ReadOnly
                            UP_LOLIQUIDMF_ReadOnly(true);

                            // 유동성관리 조회
                            UP_LOLIQUIDMF_INQ();

                            this.ShowMessage("TY_M_AC_25O8K620");
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 전표취소 버튼
        private void BTN62_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //미승인전표 -> 임시파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT02_LOLIJPNO.GetValue().ToString().Substring(0, 6),
                                                                 this.TXT02_LOLIJPNO.GetValue().ToString().Substring(6, 8),
                                                                 this.TXT02_LOLIJPNO.GetValue().ToString().Substring(14, 3)
                                                                 );
            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID,
                                                        this.ProgramNo, Employer.EmpNo,
                                                        "D",
                                                        this.TXT02_LOLIJPNO.GetValue().ToString().Substring(0, 6),
                                                        this.TXT02_LOLIJPNO.GetValue().ToString().Substring(6, 8),
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        Employer.EmpNo);

            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, ""); // SP CALL
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                // 전표번호
                this.TXT02_LOLIJPNO.SetValue("");

                // 유동성관리 전표번호 업데이트
                UP_LOLIQUIDMF_UPT_JPNO();

                // 유동성관리 전표관련 버튼 DISPLAY
                UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(true, true, true, false);

                // 유동성관리 필드 ReadOnly
                UP_LOLIQUIDMF_ReadOnly(false);

                // 유동성관리 조회
                UP_LOLIQUIDMF_INQ();

                this.ShowMessage("TY_M_AC_25O8K620");
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 전표출력 버튼
        private void BTN62_PRT_Click(object sender, EventArgs e)
        {
            if (this.TXT02_LOLIJPNO.GetValue().ToString() != "")
            {
                string sJPNO = this.TXT02_LOLIJPNO.GetValue().ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2AU2M916",
                    sJPNO.Substring(0, 6),
                    sJPNO.Substring(6, 8),
                    sJPNO.Substring(14, 3), // 시작 번호
                    sJPNO.Substring(14, 3)  // 종료 번호
                    );

                if (Convert.ToDouble(sJPNO.Substring(6, 4)) > 2014)
                {
                    SectionReport rpt = new TYACBJ0012R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
                else
                {
                    SectionReport rpt = new TYACBJ001R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 조회(여러건)
        private void UP_LOLIQUIDMF_INQ()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87BAH353",
                this.TXT01_STDATE.GetValue().ToString(),
                this.TXT01_EDDATE.GetValue().ToString(),
                this.TXT01_LOCCONTNO.GetValue().ToString(),
                this.CBH01_SBANK.GetValue().ToString(),
                this.MTB01_SDATE.GetValue().ToString().Replace("-", "").Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_87BAJ355.SetValue(dt);

            if (dt.Rows.Count <= 0)
            {
                // 유동성관리 조회(단일건)
                UP_LOLIQUIDMF_SEARCH();
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 조회(단일건)
        private void UP_LOLIQUIDMF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87BAJ354",
                this.TXT02_LOLICONTYEAR.GetValue().ToString(),
                this.TXT02_LOLICONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_87BAJ355.SetValue(dt);
        }
        #endregion

        #region Description : 유동성 대체 관리 확인
        private void UP_LOLIQUIDMF_RUN()
        {
            fsSTATUS = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87BAS356",
                this.TXT02_LOLICONTYEAR.GetValue().ToString(),
                this.TXT02_LOLICONTSEQ.GetValue().ToString(),
                fsLOLICONTSEQ.ToString(),
                this.TXT02_LOLINUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");

                // 유동성 대체금액
                fsLOLIAMT = Get_Numeric(dt.Rows[0]["LOLIAMT"].ToString());

                fsSTATUS = dt.Rows[0]["STATUS"].ToString();

                // 계약 최종 DATA 확인
                UP_LOCONTMF_FINAL_RUN(this.TXT02_LOACCONTYEAR.GetValue().ToString(), this.TXT02_LOACCONTSEQ.GetValue().ToString(), "LOLIQUIDMF");



                fsWK_GUBUN2 = "UPT";
                UP_LOLIQUIDMF_BTN_DISPLAY(fsWK_GUBUN2);



                if (dt.Rows[0]["LOLIJPNO"].ToString() == "")
                {
                    // 유동성관리 전표관련 버튼 DISPLAY
                    UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(true, true, true, false);

                    // 유동성관리 필드 ReadOnly
                    UP_LOLIQUIDMF_ReadOnly(false);

                    this.SetFocus(this.DTP02_LOLIDATE);
                }
                else
                {
                    // 유동성관리 전표관련 버튼 DISPLAY
                    UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(false, false, false, true);

                    // 유동성관리 필드 ReadOnly
                    UP_LOLIQUIDMF_ReadOnly(true);
                }
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 저장
        private void UP_LOLIQUIDMF_SAV()
        {
            string sLOLICONTNO    = string.Empty;

            string sLOLIACCONTNO  = string.Empty;
            string sLOLIACCONTSEQ = string.Empty;
            string sLOLIACNUM     = string.Empty;

            string sLOLILRCONTNO  = string.Empty;
            string sLOLILRCONTSEQ = string.Empty;
            string sLOLILRNUM     = string.Empty;

            sLOLICONTNO   = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());
            
            if (fsSTATUS == "실행")
            {
                sLOLIACCONTNO  = Set_Fill4(this.TXT02_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOACCONTSEQ.GetValue().ToString());
                sLOLIACCONTSEQ = fsLOLIACCONTSEQ;
                sLOLIACNUM     = this.TXT02_LOACNUM.GetValue().ToString();

                sLOLILRCONTNO  = "0";
                sLOLILRCONTSEQ = "0";
                sLOLILRNUM     = "0";
            }
            else
            {
                sLOLIACCONTNO  = "0";
                sLOLIACCONTSEQ = "0";
                sLOLIACNUM     = "0";

                sLOLILRCONTNO  = Set_Fill4(this.TXT02_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOACCONTSEQ.GetValue().ToString());
                sLOLILRCONTSEQ = fsLOLIACCONTSEQ;
                sLOLILRNUM     = this.TXT02_LOACNUM.GetValue().ToString();
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87BBM358",
                sLOLICONTNO.ToString(),
                fsLOLICONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT02_LOLINUM.SetValue(dt.Rows[0]["SEQ"].ToString());
            }

            // 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87BBN359",
                                    sLOLICONTNO.ToString(),
                                    fsLOLICONTSEQ.ToString(),
                                    this.TXT02_LOLINUM.GetValue().ToString(),
                                    this.DTP02_LOLIDATE.GetValue().ToString(),
                                    Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString()),
                                    Get_Numeric(this.TXT02_LOLIYUL.GetValue().ToString()),
                                    Get_Numeric(this.TXT02_LOLIDOLLAR.GetValue().ToString()),
                                    this.CBH02_LOLICDAC.GetValue().ToString(),
                                    this.CBH02_LOLIDPAC.GetValue().ToString(),
                                    this.TXT02_LOLIRKAC.GetValue().ToString(),
                                    sLOLIACCONTNO.ToString(),
                                    sLOLIACCONTSEQ.ToString(),
                                    sLOLIACNUM.ToString(),
                                    sLOLILRCONTNO.ToString(),
                                    sLOLILRCONTSEQ.ToString(),
                                    sLOLILRNUM.ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 유동성 대체 관리 수정
        private void UP_LOLIQUIDMF_UPT()
        {
            string sLOLICONTNO    = string.Empty;
            
            string sLOLIACCONTNO  = string.Empty;
            string sLOLIACCONTSEQ = string.Empty;
            string sLOLIACNUM     = string.Empty;

            string sLOLILRCONTNO  = string.Empty;
            string sLOLILRCONTSEQ = string.Empty;
            string sLOLILRNUM     = string.Empty;

            sLOLICONTNO   = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            if (fsSTATUS == "실행")
            {
                sLOLIACCONTNO  = Set_Fill4(this.TXT02_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOACCONTSEQ.GetValue().ToString());
                sLOLIACCONTSEQ = fsLOLIACCONTSEQ;
                sLOLIACNUM     = this.TXT02_LOACNUM.GetValue().ToString();

                sLOLILRCONTNO  = "0";
                sLOLILRCONTSEQ = "0";
                sLOLILRNUM     = "0";
            }
            else
            {
                sLOLIACCONTNO  = "0";
                sLOLIACCONTSEQ = "0";
                sLOLIACNUM     = "0";

                sLOLILRCONTNO  = Set_Fill4(this.TXT02_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOACCONTSEQ.GetValue().ToString());
                sLOLILRCONTSEQ = fsLOLIACCONTSEQ;
                sLOLILRNUM     = this.TXT02_LOACNUM.GetValue().ToString();
            } 
            

            // 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87BBQ360",
                                    this.DTP02_LOLIDATE.GetValue().ToString(),
                                    Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString()),
                                    Get_Numeric(this.TXT02_LOLIYUL.GetValue().ToString()),
                                    Get_Numeric(this.TXT02_LOLIDOLLAR.GetValue().ToString()),
                                    this.CBH02_LOLICDAC.GetValue().ToString(),
                                    this.CBH02_LOLIDPAC.GetValue().ToString(),
                                    this.TXT02_LOLIRKAC.GetValue().ToString(),
                                    sLOLIACCONTNO.ToString(),
                                    sLOLIACCONTSEQ.ToString(),
                                    sLOLIACNUM.ToString(),
                                    sLOLILRCONTNO.ToString(),
                                    sLOLILRCONTSEQ.ToString(),
                                    sLOLILRNUM.ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    sLOLICONTNO.ToString(),
                                    fsLOLICONTSEQ.ToString(),
                                    this.TXT02_LOLINUM.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 유동성 대체 관리 삭제
        private void UP_LOLIQUIDMF_DEL()
        {
            string sLOLICONTNO = string.Empty;

            sLOLICONTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            // 삭제 프로시저
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87BBS361", sLOLICONTNO.ToString(),
                                                        fsLOLICONTSEQ.ToString(),
                                                        this.TXT02_LOLINUM.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 유동성 대체 관리 전표번호 업데이트
        private void UP_LOLIQUIDMF_UPT_JPNO()
        {
            string sLOLICONTNO = string.Empty;

            sLOLICONTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            // 유동성관리 마스터 전표번호 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87BDE367",
                                    this.TXT02_LOLIJPNO.GetValue().ToString(),
                                    sLOLICONTNO.ToString(),
                                    fsLOLICONTSEQ.ToString(),
                                    this.TXT02_LOLINUM.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 유동성 재대체 - 재대체 구분 업데이트
        private void UP_LORELIQUIDMF_UPT()
        {
            string sLOLRCONTNO  = string.Empty;
            string sLOLRCONTSEQ = string.Empty;
            string sLOLRNUM     = string.Empty;

            string sLOLRGUBN    = string.Empty;

            sLOLRCONTNO  = Set_Fill4(this.TXT02_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOACCONTSEQ.GetValue().ToString());
            sLOLRCONTSEQ = fsLOLIACCONTSEQ;
            sLOLRNUM     = this.TXT02_LOACNUM.GetValue().ToString();

            sLOLRGUBN = "D";

            // 삭제 프로시저
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_88TH8656", sLOLRGUBN.ToString(),
                                                        sLOLRCONTNO.ToString(),
                                                        sLOLRCONTSEQ.ToString(),
                                                        sLOLRNUM.ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 유동성 대체 관리 스프레드 이벤트
        private void FPS91_TY_S_AC_87BAJ355_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsLOLIACCONTSEQ = "";
            fsLOLICONTSEQ   = "";

            // 유동성 관리번호
            this.TXT02_LOLICONTYEAR.SetValue(this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLICONTNO").ToString().Substring(0, 4));
            this.TXT02_LOLICONTSEQ.SetValue(this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLICONTNO").ToString().Substring(5, 2));
            this.TXT02_LOLINUM.SetValue(this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLICONTNO").ToString().Substring(8, 3));

            fsLOLICONTSEQ = this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLICONTSEQ").ToString();

            // 실행 관리번호
            this.TXT02_LOACCONTYEAR.SetValue(this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIACCONTNO").ToString());
            this.TXT02_LOACCONTSEQ.SetValue(this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIACCONTSEQ").ToString());
            this.TXT02_LOACNUM.SetValue(this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIACNUM").ToString());

            // 이부분 바꿔야 함
            fsLOLIACCONTSEQ = this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIACCONTSEQ").ToString();

            // 유동성관리 확인
            UP_LOLIQUIDMF_RUN();
        }
        #endregion

        #region Description : 유동성 대체 관리 저장 ProcessCheck
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sLOACCONTNO = string.Empty;
            sLOACCONTNO = Set_Fill4(this.TXT02_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOACCONTSEQ.GetValue().ToString());

            DataTable dt = new DataTable();

            // 전표계정 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_88NBH624",
                this.CBH02_LOLICDAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_24RBZ877");
                this.SetFocus(this.CBH02_LOLICDAC.CodeText);

                e.Successed = false;
                return;
            }

            // 계정과목 체크
            if (this.CBH02_LOCCURRTYPE.GetValue().ToString() == "1")
            {
                if (this.CBH02_LOLICDAC.GetValue().ToString() != "21101901")
                {
                    this.ShowMessage("TY_M_AC_88NFA633");
                    this.SetFocus(this.CBH02_LOLICDAC.CodeText);

                    e.Successed = false;
                    return;
                }
                
            }
            else
            {
                if (this.CBH02_LOLICDAC.GetValue().ToString() != "21101902")
                {
                    this.ShowMessage("TY_M_AC_88NFA633");
                    this.SetFocus(this.CBH02_LOLICDAC.CodeText);

                    e.Successed = false;
                    return;
                }
            }


            // 계약의 마지막 번호에 해당하는 데이터 확인(장기인지 확인)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IH6237",
                this.TXT02_LOACCONTYEAR.GetValue().ToString(),
                this.TXT02_LOACCONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LOCGIGANTYPE"].ToString() != "2")
                {
                    this.ShowMessage("TY_M_AC_87BCY364");

                    this.SetFocus(this.DTP02_LOLIDATE);

                    e.Successed = false;
                    return;
                }
            }

            // 계약이 장기이면서 실행 관리번호로 잔액이 남아 있는지 확인
            string sProcedure = string.Empty;

            // 20190411 수정전 소스
            //if (fsSTATUS == "실행")
            //{
            //    sProcedure = "TY_P_AC_87BD1365";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88THZ657";
            //}

            // 20190411 수정 후 소스
            if (fsSTATUS == "실행")
            {
                sProcedure = "TY_P_AC_94BBO340";
            }
            else
            {
                sProcedure = "TY_P_AC_94BBL339";
            }

            // 실행과 유동성 재대체에 따른 쿼리 바꿔야 함
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                sLOACCONTNO.ToString(),
                fsLOLIACCONTSEQ.ToString(),
                this.TXT02_LOACNUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_87BD4366");

                this.SetFocus(this.DTP02_LOLIDATE);

                e.Successed = false;
                return;
            }
            else
            {
                string sJANAMT = string.Empty;

                sJANAMT = Get_Numeric(dt.Rows[0]["JANAMT"].ToString());

                if (fsWK_GUBUN2.ToString() == "NEW")
                {
                    // 잔액이 유동성 대체금액보다 많아야 함
                    if (double.Parse(Get_Numeric(sJANAMT.ToString())) < double.Parse(Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString())))
                    {
                        this.ShowMessage("TY_M_AC_87CHY378");

                        this.SetFocus(this.TXT02_LOLIAMT);

                        e.Successed = false;
                        return;
                    }
                }
                else if (fsWK_GUBUN2.ToString() == "UPT")
                {
                    string sLOLIAMT = string.Empty;

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_87BAS356",
                        this.TXT02_LOLICONTYEAR.GetValue().ToString(),
                        this.TXT02_LOLICONTSEQ.GetValue().ToString(),
                        fsLOLICONTSEQ.ToString(),
                        this.TXT02_LOLINUM.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sLOLIAMT = Get_Numeric(dt.Rows[0]["LOLIAMT"].ToString());
                    }

                    // 잔액이 유동성 대체금액보다 많아야 함
                    if (double.Parse(Get_Numeric(sJANAMT.ToString())) - double.Parse(Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString())) < 0)
                    {
                        this.ShowMessage("TY_M_AC_87CHY378");

                        this.SetFocus(this.TXT02_LOLIAMT);

                        e.Successed = false;
                        return;
                    }
                }
            }


            if (fsWK_GUBUN2.ToString() == "UPT")
            {
                string sLORENCTNO = string.Empty;

                sLORENCTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

                // 상환 DATA 존재하면 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_8889T522",
                    sLORENCTNO.ToString(),
                    fsLOLICONTSEQ.ToString(),
                    this.TXT02_LOLINUM.GetValue().ToString(),
                    "02"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_8889W523");

                    e.Successed = false;
                    return;
                }

                string sLOLICONTNO = string.Empty;

                sLOLICONTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

                // 유동성 재대체에 자료가 존재하면 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_8889P520",
                    sLOLICONTNO.ToString(),
                    fsLOLICONTSEQ.ToString(),
                    this.TXT02_LOLINUM.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_8889R521");

                    e.Successed = false;
                    return;
                }

                // 전표번호가 존재하면 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_87BAS356",
                    this.TXT02_LOLICONTYEAR.GetValue().ToString(),
                    this.TXT02_LOLICONTSEQ.GetValue().ToString(),
                    fsLOLICONTSEQ.ToString(),
                    this.TXT02_LOLINUM.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LOLIJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_AC_86IFE235");

                        this.SetFocus(this.DTP02_LOLIDATE);

                        e.Successed = false;
                        return;
                    }
                }
            }


            if (fsWK_GUBUN2.ToString() == "NEW")
            {
                // 저장하시겠습니까?
                if (!this.ShowMessage("TY_M_GB_23NAD871"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                // 저장하시겠습니까?
                if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 삭제 ProcessCheck
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 전표번호가 존재하면 삭제 불가
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87BAS356",
                this.TXT02_LOLICONTYEAR.GetValue().ToString(),
                this.TXT02_LOLICONTSEQ.GetValue().ToString(),
                fsLOLICONTSEQ.ToString(),
                this.TXT02_LOLINUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LOLIJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_AC_86IFE235");

                    this.SetFocus(this.DTP02_LOLIDATE);

                    e.Successed = false;
                    return;
                }
            }

            string sLORENCTNO = string.Empty;

            sLORENCTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            // 상환 DATA 존재하면 삭제 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_8889T522",
                sLORENCTNO.ToString(),
                fsLOLICONTSEQ.ToString(),
                this.TXT02_LOLINUM.GetValue().ToString(),
                "02"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_8889W523");

                e.Successed = false;
                return;
            }

            string sLOLICONTNO = string.Empty;

            sLOLICONTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            // 유동성 재대체에 자료가 존재하면 삭제 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_8889P520",
                sLOLICONTNO.ToString(),
                fsLOLICONTSEQ.ToString(),
                this.TXT02_LOLINUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_8889R521");

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 전표생성 ProcessCheck
        private void BTN62_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sLOACCONTNO = string.Empty;
            sLOACCONTNO = Set_Fill4(this.TXT02_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOACCONTSEQ.GetValue().ToString());

            // 대체금액이 0원이면 전표생성 불가
            if (double.Parse(Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_AC_87BBZ363");

                this.SetFocus(this.TXT02_LOLIAMT);

                e.Successed = false;
                return;
            }

            string sJANAMT = string.Empty;
            string sLOLIAMT = string.Empty;

            DataTable dt = new DataTable();

            // 계약이 장기이면서 실행 관리번호로 잔액이 남아 있는지 확인

            string sProcedure = string.Empty;

            // 20190411 수정전 소스
            //if (fsSTATUS == "실행")
            //{
            //    sProcedure = "TY_P_AC_87BD1365";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88THZ657";
            //}

            // 20190411 수정 후 소스
            if (fsSTATUS == "실행")
            {
                sProcedure = "TY_P_AC_94BBO340";
            }
            else
            {
                sProcedure = "TY_P_AC_94BBL339";
            }

            // 실행과 유동성 재대체에 따른 쿼리 바꿔야 함
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                sLOACCONTNO.ToString(),
                fsLOLIACCONTSEQ.ToString(),
                this.TXT02_LOACNUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_87BD4366");

                this.SetFocus(this.DTP02_LOLIDATE);

                e.Successed = false;
                return;
            }
            else
            {
                sJANAMT = Get_Numeric(dt.Rows[0]["JANAMT"].ToString());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_87BAS356",
                    this.TXT02_LOLICONTYEAR.GetValue().ToString(),
                    this.TXT02_LOLICONTSEQ.GetValue().ToString(),
                    fsLOLICONTSEQ.ToString(),
                    this.TXT02_LOLINUM.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sLOLIAMT = Get_Numeric(dt.Rows[0]["LOLIAMT"].ToString());
                }

                // 20180830 수정전 소스
                //// 잔액이 유동성 대체금액보다 많아야 함
                //if (double.Parse(Get_Numeric(sJANAMT.ToString())) < double.Parse(Get_Numeric(sJANAMT.ToString())) + double.Parse(Get_Numeric(sLOLIAMT.ToString())) - double.Parse(Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString())))
                //{
                //    this.ShowMessage("TY_M_AC_87CHY378");

                //    this.SetFocus(this.TXT02_LOLIAMT);

                //    e.Successed = false;
                //    return;
                //}

                // 20180830 수정후 소스
                // 잔액이 유동성 대체금액보다 많아야 함
                if (double.Parse(Get_Numeric(sJANAMT.ToString())) - double.Parse(Get_Numeric(this.TXT02_LOLIAMT.GetValue().ToString())) < 0)
                {
                    this.ShowMessage("TY_M_AC_87CHY378");

                    this.SetFocus(this.TXT02_LOLIAMT);

                    e.Successed = false;
                    return;
                }

            }

            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 전표취소 ProcessCheck
        private void BTN62_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 차입금 상환관리 DATA 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86RAF290",
                this.TXT02_LOLIJPNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_86RAJ291");

                e.Successed = false;
                return;
            }

            string sLORENCTNO = string.Empty;

            sLORENCTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            // 상환 DATA 존재하면 전표취소 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_8889T522",
                sLORENCTNO.ToString(),
                fsLOLICONTSEQ.ToString(),
                this.TXT02_LOLINUM.GetValue().ToString(),
                "02"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_8889W523");

                e.Successed = false;
                return;
            }

            string sLOLICONTNO = string.Empty;

            sLOLICONTNO = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString());

            // 유동성 재대체에 자료가 존재하면 전표취소 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_8889P520",
                sLOLICONTNO.ToString(),
                fsLOLICONTSEQ.ToString(),
                this.TXT02_LOLINUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_8889R521");

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_25O8K619"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 코드헬프 - 유동성 대체 조회
        private void BTN62_CODEHELP1_Click(object sender, EventArgs e)
        {
            TYACLO02C1 popup = new TYACLO02C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fsLOLIACCONTSEQ = "";
                fsLOLICONTSEQ   = "";

                this.TXT02_LOACCONTYEAR.SetValue(popup.fsLOACCONTYEAR); // 계약년도
                this.TXT02_LOACCONTSEQ.SetValue(popup.fsLOACCONTNO);    // 계약번호
                this.TXT02_LOACNUM.SetValue(popup.fsLOACNUM);           // 실행또는 재대체순번

                fsLOLIACCONTSEQ = popup.fsLOACCONTSEQ;                  // 실행번호

                this.TXT02_LOLICONTYEAR.SetValue(popup.fsLOACCONTYEAR); // 유동성재대체 계약년도
                this.TXT02_LOLICONTSEQ.SetValue(popup.fsLOACCONTNO);    // 유동성재대체 계약번호
                
                fsLOLICONTSEQ = popup.fsLOACCONTSEQ;                    // 유동성재대체 번호

                fsSTATUS      = popup.fsSTATUS;                         // 상태

                if (fsSTATUS == "실행")
                {
                    this.DTP02_LOACDATE.SetValue(popup.fsLOACDATE);     // 실행일자
                    this.TXT02_LOACAMT.SetValue(popup.fsLOACAMT);       // 실행금액

                    this.DTP02_LOLRDATE.SetValue("");                   // 재대체 일자
                    this.TXT02_LOLRAMT.SetValue("");                    // 재대체 금액
                }
                else
                {
                    this.DTP02_LOACDATE.SetValue("");                   // 실행일자
                    this.TXT02_LOACAMT.SetValue("");                    // 실행금액

                    this.DTP02_LOLRDATE.SetValue(popup.fsLOACDATE);     // 재대체 일자
                    this.TXT02_LOLRAMT.SetValue(popup.fsLOACAMT);       // 재대체 금액
                }

                // 계약 최종 DATA 확인
                UP_LOCONTMF_FINAL_RUN(this.TXT02_LOACCONTYEAR.GetValue().ToString(), this.TXT02_LOACCONTSEQ.GetValue().ToString(), "LOLIQUIDMF");

                if (this.CBH02_LOCCURRTYPE.GetValue().ToString() == "1")
                {
                    this.CBH02_LOLICDAC.SetValue("21101901");
                }
                else
                {
                    this.CBH02_LOLICDAC.SetValue("21101902");
                }
            }

        }
        #endregion

        #region Description : 유동성 대체 관리 버튼 디스플레이
        private void UP_LOLIQUIDMF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN62_CODEHELP1.Visible = true;

                this.BTN62_SAV.Visible = true;
                this.BTN62_REM.Visible = false;

                // 상환관리 전표관련 버튼 DISPLAY
                UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(true, false, false, false);

                this.TXT02_LOLINUM.SetReadOnly(false);
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN62_CODEHELP1.Visible = false;

                this.BTN62_SAV.Visible = true;
                this.BTN62_REM.Visible = true;

                // 상환관리 전표관련 버튼 DISPLAY
                UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(true, true, true, false);

                this.TXT02_LOLINUM.SetReadOnly(true);
            }
            else
            {
                this.BTN62_CODEHELP1.Visible = false;

                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                // 상환관리 전표관련 버튼 DISPLAY
                UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(false, false, false, false);

                this.TXT02_LOLINUM.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 유동성 대체 관리 전표관련 버튼 DISPLAY
        private void UP_LOLIQUIDMF_JPNO_BTN_DISPLAY(bool bflag1, bool bflag2, bool bflag3, bool bflag4)
        {
            this.BTN62_SAV.Visible = bflag1;
            this.BTN62_REM.Visible = bflag2;

            this.BTN62_JPNO_CRE.Visible      = bflag3;
            this.BTN62_JUNPYO_CANCEL.Visible = bflag4;
            this.BTN62_PRT.Visible           = bflag4;
        }
        #endregion

        #region Description : 유동성 대체 관리 필드 ReadOnly
        private void UP_LOLIQUIDMF_ReadOnly(bool bFalse)
        {
            // 계약사항 필드 클리어
            this.TXT02_LOACCONTYEAR.SetReadOnly(true);
            this.TXT02_LOACCONTSEQ.SetReadOnly(true);
            this.TXT02_LOACNUM.SetReadOnly(true);

            this.DTP02_LOLIDATE.SetReadOnly(bFalse);

            this.TXT02_LOLIAMT.SetReadOnly(bFalse);
            this.TXT02_LOLIYUL.SetReadOnly(bFalse);
            this.TXT02_LOLIDOLLAR.SetReadOnly(bFalse);
            this.CBH02_LOLICDAC.SetReadOnly(bFalse);
            this.CBH02_LOLIDPAC.SetReadOnly(bFalse);
            this.TXT02_LOLIRKAC.SetReadOnly(bFalse);
        }
        #endregion

        #region Description : 유동성 대체 관리 필드 클리어
        private void UP_LOLIQUIDMF_FieldClear()
        {
            fsLOLICONTSEQ = "";

            // 계약사항 필드 클리어
            this.TXT02_LOACCONTYEAR.SetValue("");
            this.TXT02_LOACCONTSEQ.SetValue("");
            this.TXT02_LOACNUM.SetValue("");

            this.CBH02_LOCBANKCD.SetValue("");
            this.CBH02_LOCGRBK.SetValue("");
            this.CBH02_LOCCURRTYPE.SetValue("");
            this.CBO02_LOCGIGANTYPE.SetValue("");
            this.CBO02_LOCGIGANTYPE.SetValue("");
            this.CBH02_LOCLOANTYPE.SetValue("");
            this.CBH02_LOCLOAN.SetValue("");
            this.CBH02_LOCCDAC.SetValue("");
            this.TXT02_LOCCONTRATE.SetValue("");
            this.DTP02_LOCCONTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP02_LOCENDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBO02_LOCCOLLGB.SetValue("");
            this.CBH02_LOCCURRCD.SetValue("");
            this.TXT02_LOCCONTYUL.SetValue("");
            this.CBO02_LOCRATEGB.SetValue("");
            this.TXT02_LOCCONTAMT.SetValue("");
            this.TXT02_LOCCREDITAMT.SetValue("");
            this.CBH02_LOCDESCGB.SetValue("");

            // 유동성관리 필드 클리어
            this.TXT02_LOLICONTYEAR.SetValue("");
            this.TXT02_LOLICONTSEQ.SetValue("");
            this.TXT02_LOLINUM.SetValue("");

            this.DTP02_LOLIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT02_LOLIAMT.SetValue("");
            this.CBH02_LOLIDPAC.SetValue("");
            this.TXT02_LOLIYUL.SetValue("");
            this.TXT02_LOLIDOLLAR.SetValue("");
            this.CBH02_LOLICDAC.SetValue("");
            this.TXT02_LOLIRKAC.SetValue("");
            this.TXT02_LOLIJPNO.SetValue("");
        }
        #endregion

        #region Description : 유동성 대체 관리 대체일자 이벤트
        private void DTP02_LOLIDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH02_LOLIDPAC.DummyValue = this.DTP02_LOLIDATE.GetString();
        }
        #endregion

        #region Description : 유동성 대체 관리 귀속부서 이벤트
        private void CBH02_LOLIDPAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetFocus(this.TXT02_LOLIRKAC);
        }
        #endregion

        #region Description : 유동성 대체 관리 적요 이벤트
        private void TXT02_LOLIRKAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN62_SAV.Visible == true)
                {
                    SetFocus(this.BTN62_SAV);
                }
            }
        }
        #endregion

        #endregion
        





        #region Description : 차입금 상환관리

        #region Description : 계약내용 조회
        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            fsDs.Clear();

            // 상환금액
            fsLOREAMT = "0";

            UP_LOREPAYMF_FieldClear();

            fsLOACGUBN_03 = "";

            this.FPS91_TY_S_AC_86SAY296.Initialize();
            this.FPS91_TY_S_AC_874H2324.Initialize();

            string sProcedure = string.Empty;

            if (this.CBO03_LOACGUBN.GetValue().ToString() == "S") // 상환
            {
                this.FPS91_TY_S_AC_86SAY296_Sheet1.ColumnHeader.Cells[0, 0].Value = "계약번호";
                this.FPS91_TY_S_AC_86SAY296_Sheet1.ColumnHeader.Cells[0, 1].Value = "은행명";
                this.FPS91_TY_S_AC_86SAY296_Sheet1.ColumnHeader.Cells[0, 2].Value = "잔액";

                sProcedure = "TY_P_AC_94BBX343";
            }
            else // 대환
            {
                this.FPS91_TY_S_AC_86SAY296_Sheet1.ColumnHeader.Cells[0, 0].Value = "관리번호";
                this.FPS91_TY_S_AC_86SAY296_Sheet1.ColumnHeader.Cells[0, 1].Value = "은행명";
                this.FPS91_TY_S_AC_86SAY296_Sheet1.ColumnHeader.Cells[0, 2].Value = "잔액";

                sProcedure = "TY_P_AC_94GGL378";
            }

            DataTable dt = new DataTable();

            // 20180828 수정전 소스
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_86SAY295",
            //    this.TXT03_STYEAR.GetValue().ToString(),
            //    this.TXT03_EDYEAR.GetValue().ToString(),
            //    this.CBH03_SBANK.GetValue().ToString()
            //    );

            //// 20180828 수정후 소스
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_88SD2646",
            //    this.TXT03_STYEAR.GetValue().ToString(),
            //    this.TXT03_EDYEAR.GetValue().ToString(),
            //    this.CBH03_SBANK.GetValue().ToString()
            //    );

            // 20190411 수정후 소스
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                this.TXT03_STYEAR.GetValue().ToString(),
                this.TXT03_EDYEAR.GetValue().ToString(),
                this.CBH03_SBANK.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86SAY296.SetValue(dt);

            this.FPS91_TY_S_AC_86SAY296.Focus();
        }
        #endregion

        #region Description : 상황관리 신규 버튼
        private void BTN63_NEW_Click(object sender, EventArgs e)
        {
            fsDs.Clear();

            // 계약내용 초기화
            this.FPS91_TY_S_AC_86SAY296.Initialize();

            // 상환내역 스프레드 초기화
            this.FPS91_TY_S_AC_874H2324.Initialize();

            // 상환조회 스프레드 초기화
            this.FPS91_TY_S_AC_875F6331.Initialize();

            this.TXT03_STYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            this.TXT03_EDYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            // 상환금액
            fsLOREAMT = "0";

            UP_LOREPAYMF_FieldClear();

            // 상환관리 필드 ReadOnly
            UP_LOREPAYMF_ReadOnly(false);

            fsWK_GUBUN3 = "NEW";
            UP_LOREPAYMF_BTN_DISPLAY(fsWK_GUBUN3);

            // 귀속부서 가져오기
            this.CBH03_LOREDPAC.SetValue(UP_GET_INKIBNMF("TEAM"));

            this.SetFocus(this.CBO03_LOACGUBN);
        }
        #endregion

        #region Description : 상환관리 저장 버튼
        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN3.ToString() == "NEW")
            {
                // 수정된 데이터셋 가져오기
                fsDs = ((TButton.ClickEventCheckArgs )e).ArgData as DataSet;

                UP_LOREPAYMF_SAV();

                this.TXT01_LOCCONTYEAR.SetReadOnly(true);
            }
            else if (fsWK_GUBUN3.ToString() == "UPT")
            {
                // 수정된 데이터셋 가져오기
                fsDs = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                UP_LOREPAYMF_UPT();
            }

            fsWK_GUBUN3 = "";
            UP_LOREPAYMF_BTN_DISPLAY(fsWK_GUBUN3);

            // 계약내용 스프레드 초기화
            this.FPS91_TY_S_AC_86SAY296.Initialize();

            // 상환 조회 스프레드 초기화
            this.FPS91_TY_S_AC_875F6331.Initialize();

            // 상환 내역 조회 스프레드 초기화
            this.FPS91_TY_S_AC_874H2324.Initialize();

            // 상환관리 조회(단일건)
            UP_LOREPAYMF_SEARCH();
        }
        #endregion

        #region Description : 상환관리 삭제 버튼
        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            // 상환관리 삭제
            UP_LOREPAYMF_DEL();

            fsWK_GUBUN3 = "";
            UP_LOREPAYMF_BTN_DISPLAY(fsWK_GUBUN3);

            // 계약내용 스프레드 초기화
            this.FPS91_TY_S_AC_86SAY296.Initialize();

            // 상환 조회 스프레드 초기화
            this.FPS91_TY_S_AC_875F6331.Initialize();

            // 상환 내역 조회 스프레드 초기화
            this.FPS91_TY_S_AC_874H2324.Initialize();

            // 실행관리 조회(여러건)
            UP_LOREPAYMF_INQ();
        }
        #endregion

        #region Description : 상환관리 전표 생성 버튼
        private void BTN63_JPNO_CRE_Click(object sender, EventArgs e)
        {
            string sB2DPMK = "";
            string sB2SSID = "";
            string sLORENCHGB = string.Empty;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            int iCnt = 0;

            // 부서코드 가져오기
            sB2DPMK = UP_GET_INKIBNMF("BUSEO");

            DataTable dt = new DataTable();
            DataTable dt_REPAY = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            string sW2CDAC    = string.Empty;
            string sRE_W2CDAC = string.Empty;
            string sRE_W2NOLN = string.Empty;
            string sGRBK      = string.Empty;
            string sLORECONTNO = string.Empty;

            int iW2NOLN = 0;

            // 차입금 상환관리 내역 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_875FM332",
                this.TXT03_LORECONTYEAR.GetValue().ToString(),
                this.TXT03_LORECONTSEQ.GetValue().ToString(),
                fsLORECONTSEQ.ToString(),
                this.TXT03_LORENUM.GetValue().ToString()
                );

            dt_REPAY = this.DbConnector.ExecuteDataTable();

            if (dt_REPAY.Rows.Count > 0)
            {
                for (int i = 0; i < dt_REPAY.Rows.Count; i++)
                {
                    sLORECONTNO = dt_REPAY.Rows[i]["LORENCTNO"].ToString() + Set_Fill3(dt_REPAY.Rows[i]["LORENCTSEQ"].ToString()) + Set_Fill3(dt_REPAY.Rows[i]["LORENCTNUM"].ToString());

                    sLORENCHGB = dt_REPAY.Rows[i]["LORENCHGB"].ToString();

                    #region Description : 차변 회계처리

                    iW2NOLN = iW2NOLN + 1;

                    sW2CDAC    = "";
                    sRE_W2CDAC = "";

                    if (this.CBO03_LOREACGUBN.GetValue().ToString() == "S") // 상환
                    {
                        sGRBK = this.CBH03_LOREGRBK.GetValue().ToString();
                    }
                    else // 대환
                    {
                        // 은행 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_94IFV395", dt_REPAY.Rows[i]["LORENWNJPNO"].ToString(),
                                                                    dt_REPAY.Rows[i]["LORENWNJPNO"].ToString(),
                                                                    dt_REPAY.Rows[i]["LORENWNJPNO"].ToString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 실행, 유동성, 유동성 대체의 계정에 대한 차변 계정임
                            sGRBK = dt.Rows[0]["GRBK"].ToString();
                        }
                    }


                    // 차변 계정 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_88R91636", dt_REPAY.Rows[i]["LORENWNJPNO"].ToString(),
                                                                dt_REPAY.Rows[i]["LORENWNJPNO"].ToString(),
                                                                dt_REPAY.Rows[i]["LORENWNJPNO"].ToString());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 실행, 유동성, 유동성 대체의 계정에 대한 차변 계정임
                        sW2CDAC = dt.Rows[0]["CDAC"].ToString();
                    }

                    if (sLORENCHGB.ToString() == "00") // 실행
                    {
                        // 실행, 유동성, 유동성 대체의 계정에 대한 대변계정(상대계정)을 가져와야 함
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_8CS9V365", dt_REPAY.Rows[i]["LORENWNJPNO"].ToString().Substring(0, 6),
                                                                    dt_REPAY.Rows[i]["LORENWNJPNO"].ToString().Substring(6, 8),
                                                                    dt_REPAY.Rows[i]["LORENWNJPNO"].ToString().Substring(14, 3),
                                                                    sW2CDAC.ToString());

                        dt = this.DbConnector.ExecuteDataTable();
                    }
                    else // 유동성 대체, 유동성 재대체
                    {
                        // 실행, 유동성, 유동성 대체의 계정에 대한 대변계정(상대계정)을 가져와야 함
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_B5HBA340", dt_REPAY.Rows[i]["LORENWNJPNO"].ToString().Substring(0, 6),
                                                                    dt_REPAY.Rows[i]["LORENWNJPNO"].ToString().Substring(6, 8),
                                                                    dt_REPAY.Rows[i]["LORENWNJPNO"].ToString().Substring(14, 3),
                                                                    sW2CDAC.ToString());

                        dt = this.DbConnector.ExecuteDataTable();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        // 실행, 유동성, 유동성 대체의 계정에 대한 대변 계정을 가져옴
                        sRE_W2CDAC = dt.Rows[0]["B2CDAC"].ToString();
                        sRE_W2NOLN = Set_Fill2(dt.Rows[0]["B2NOLN"].ToString());
                    }


                    this.DAT02_W2SSID.SetValue(sB2SSID.ToString());
                    this.DAT02_W2DPMK.SetValue(sB2DPMK.ToString());
                    this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP03_LOREDATE.GetValue().ToString()));
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(Convert.ToString(iW2NOLN));
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sRE_W2CDAC.ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH03_LOREDPAC.GetValue().ToString());

                    // 계약관리의 약정계정과목 관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH03_LOCCDAC.GetValue().ToString(), "");

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI1.SetValue("");
                        }

                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI2.SetValue("");
                        }

                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI3.SetValue("");
                        }

                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI4.SetValue("");
                        }

                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI5.SetValue("");
                        }

                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI6.SetValue("");
                        }
                    }
                    dt.Clear();

                    // 관리항목1
                    this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목2
                    this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목3
                    this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목4
                    this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목5
                    this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목6
                    this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));

                    this.DAT02_W2AMDR.SetValue(Get_Numeric(dt_REPAY.Rows[i]["LORENAMOUNT"].ToString()));
                    this.DAT02_W2AMCR.SetValue("0");

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");
                    this.DAT02_W2RKAC.SetValue(this.TXT03_LORERKAC.GetValue().ToString());
                    this.DAT02_W2RKCU.SetValue("");
                    this.DAT02_W2WCJP.SetValue(dt_REPAY.Rows[i]["LORENWNJPNO"].ToString() + sRE_W2NOLN.ToString());
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                    this.DAT02_W2GUBUN.SetValue("");
                    this.DAT02_W2TXAMT.SetValue("0");
                    this.DAT02_W2TXVAT.SetValue("0");
                    this.DAT02_W2HWAJU.SetValue("");

                    datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                this.DAT02_W2DPMK.GetValue().ToString(),
                                this.DAT02_W2DTMK.GetValue().ToString(),
                                this.DAT02_W2NOSQ.GetValue().ToString(),
                                this.DAT02_W2NOLN.GetValue().ToString(),
                                this.DAT02_W2IDJP.GetValue().ToString(),
                                this.DAT02_W2NOJP.GetValue().ToString(),
                                this.DAT02_W2CDAC.GetValue().ToString(),
                                this.DAT02_W2DTAC.GetValue().ToString(),
                                this.DAT02_W2DTLI.GetValue().ToString(),
                                this.DAT02_W2DPAC.GetValue().ToString(),
                                this.DAT02_W2CDMI1.GetValue().ToString(),
                                this.DAT02_W2VLMI1.GetValue().ToString(),
                                this.DAT02_W2CDMI2.GetValue().ToString(),
                                this.DAT02_W2VLMI2.GetValue().ToString(),
                                this.DAT02_W2CDMI3.GetValue().ToString(),
                                this.DAT02_W2VLMI3.GetValue().ToString(),
                                this.DAT02_W2CDMI4.GetValue().ToString(),
                                this.DAT02_W2VLMI4.GetValue().ToString(),
                                this.DAT02_W2CDMI5.GetValue().ToString(),
                                this.DAT02_W2VLMI5.GetValue().ToString(),
                                this.DAT02_W2CDMI6.GetValue().ToString(),
                                this.DAT02_W2VLMI6.GetValue().ToString(),
                                this.DAT02_W2AMDR.GetValue().ToString(),
                                this.DAT02_W2AMCR.GetValue().ToString(),
                                this.DAT02_W2CDFD.GetValue().ToString(),
                                this.DAT02_W2AMFD.GetValue().ToString(),
                                this.DAT02_W2RKAC.GetValue().ToString(),
                                this.DAT02_W2RKCU.GetValue().ToString(),
                                this.DAT02_W2WCJP.GetValue().ToString(),
                                this.DAT02_W2PRGB.GetValue().ToString(),
                                this.DAT02_W2HIGB.GetValue().ToString(),
                                this.DAT02_W2HISAB.GetValue().ToString(),
                                this.DAT02_W2GUBUN.GetValue().ToString(),
                                this.DAT02_W2TXAMT.GetValue().ToString(),
                                this.DAT02_W2TXVAT.GetValue().ToString(),
                                this.DAT02_W2HWAJU.GetValue().ToString()});

                    #endregion

                    if (this.CBO03_LOREACGUBN.GetValue().ToString() == "S") // 상환
                    {
                        #region Description : 대변 회계처리

                        iW2NOLN = iW2NOLN + 1;

                        this.DAT02_W2SSID.SetValue(sB2SSID.ToString());
                        this.DAT02_W2DPMK.SetValue(sB2DPMK.ToString());
                        this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP03_LOREDATE.GetValue().ToString()));
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(Convert.ToString(iW2NOLN));
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(this.CBH03_LORECDAC.GetValue().ToString());
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH03_LOREDPAC.GetValue().ToString());

                        // 상환관리 상대계정과목 관리항목
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH03_LORECDAC.GetValue().ToString(), "");

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI1.SetValue("");
                            }
                            if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI2.SetValue("");
                            }
                            if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI3.SetValue("");
                            }
                            if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI4.SetValue("");
                            }
                            if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI5.SetValue("");
                            }
                            if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI6.SetValue("");
                            }
                        }
                        dt.Clear();

                        sGRBK = this.CBH03_LOREGRBK.GetValue().ToString();

                        sLORECONTNO = Set_Fill4(this.TXT03_LORECONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT03_LORECONTSEQ.GetValue().ToString()) + Set_Fill3(fsLORECONTSEQ.ToString()) + Set_Fill3(this.TXT03_LORENUM.GetValue().ToString());

                        // 관리항목1
                        this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                        // 관리항목2
                        this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                        // 관리항목3
                        this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                        // 관리항목4
                        this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                        // 관리항목5
                        this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                        // 관리항목6
                        this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));

                        this.DAT02_W2AMDR.SetValue("0");
                        this.DAT02_W2AMCR.SetValue(Get_Numeric(dt_REPAY.Rows[i]["LORENAMOUNT"].ToString()));

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(this.TXT03_LORERKAC.GetValue().ToString());
                        this.DAT02_W2RKCU.SetValue("");
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                        this.DAT02_W2GUBUN.SetValue("");
                        this.DAT02_W2TXAMT.SetValue("0");
                        this.DAT02_W2TXVAT.SetValue("0");
                        this.DAT02_W2HWAJU.SetValue("");

                        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                this.DAT02_W2DPMK.GetValue().ToString(),
                                this.DAT02_W2DTMK.GetValue().ToString(),
                                this.DAT02_W2NOSQ.GetValue().ToString(),
                                this.DAT02_W2NOLN.GetValue().ToString(),
                                this.DAT02_W2IDJP.GetValue().ToString(),
                                this.DAT02_W2NOJP.GetValue().ToString(),
                                this.DAT02_W2CDAC.GetValue().ToString(),
                                this.DAT02_W2DTAC.GetValue().ToString(),
                                this.DAT02_W2DTLI.GetValue().ToString(),
                                this.DAT02_W2DPAC.GetValue().ToString(),
                                this.DAT02_W2CDMI1.GetValue().ToString(),
                                this.DAT02_W2VLMI1.GetValue().ToString(),
                                this.DAT02_W2CDMI2.GetValue().ToString(),
                                this.DAT02_W2VLMI2.GetValue().ToString(),
                                this.DAT02_W2CDMI3.GetValue().ToString(),
                                this.DAT02_W2VLMI3.GetValue().ToString(),
                                this.DAT02_W2CDMI4.GetValue().ToString(),
                                this.DAT02_W2VLMI4.GetValue().ToString(),
                                this.DAT02_W2CDMI5.GetValue().ToString(),
                                this.DAT02_W2VLMI5.GetValue().ToString(),
                                this.DAT02_W2CDMI6.GetValue().ToString(),
                                this.DAT02_W2VLMI6.GetValue().ToString(),
                                this.DAT02_W2AMDR.GetValue().ToString(),
                                this.DAT02_W2AMCR.GetValue().ToString(),
                                this.DAT02_W2CDFD.GetValue().ToString(),
                                this.DAT02_W2AMFD.GetValue().ToString(),
                                this.DAT02_W2RKAC.GetValue().ToString(),
                                this.DAT02_W2RKCU.GetValue().ToString(),
                                this.DAT02_W2WCJP.GetValue().ToString(),
                                this.DAT02_W2PRGB.GetValue().ToString(),
                                this.DAT02_W2HIGB.GetValue().ToString(),
                                this.DAT02_W2HISAB.GetValue().ToString(),
                                this.DAT02_W2GUBUN.GetValue().ToString(),
                                this.DAT02_W2TXAMT.GetValue().ToString(),
                                this.DAT02_W2TXVAT.GetValue().ToString(),
                                this.DAT02_W2HWAJU.GetValue().ToString()});


                        #endregion
                    }
                }

                if (this.CBO03_LOREACGUBN.GetValue().ToString() == "D") // 대환
                {
                    #region Description : 대변 회계처리

                    iW2NOLN = iW2NOLN + 1;

                    this.DAT02_W2SSID.SetValue(sB2SSID.ToString());
                    this.DAT02_W2DPMK.SetValue(sB2DPMK.ToString());
                    this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP03_LOREDATE.GetValue().ToString()));
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(Convert.ToString(iW2NOLN));
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(this.CBH03_LORECDAC.GetValue().ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH03_LOREDPAC.GetValue().ToString());

                    // 상환관리 상대계정과목 관리항목
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH03_LORECDAC.GetValue().ToString(), "");

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI1.SetValue("");
                        }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI2.SetValue("");
                        }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI3.SetValue("");
                        }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI4.SetValue("");
                        }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI5.SetValue("");
                        }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        {
                            this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                        }
                        else
                        {
                            this.DAT02_W2CDMI6.SetValue("");
                        }
                    }
                    dt.Clear();

                    sGRBK = this.CBH03_LOREGRBK.GetValue().ToString();

                    sLORECONTNO = Set_Fill4(this.TXT03_LORECONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT03_LORECONTSEQ.GetValue().ToString()) + Set_Fill3(fsLORECONTSEQ.ToString()) + Set_Fill3(this.TXT03_LORENUM.GetValue().ToString());

                    // 관리항목1
                    this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목2
                    this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목3
                    this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목4
                    this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목5
                    this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));
                    // 관리항목6
                    this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), "LOREPAYMF", sGRBK.ToString(), sLORECONTNO.ToString()));

                    this.DAT02_W2AMDR.SetValue("0");
                    this.DAT02_W2AMCR.SetValue(Get_Numeric(this.TXT03_LOREAMT.GetValue().ToString()));

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");
                    this.DAT02_W2RKAC.SetValue(this.TXT03_LORERKAC.GetValue().ToString());
                    this.DAT02_W2RKCU.SetValue("");
                    this.DAT02_W2WCJP.SetValue("");
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                    this.DAT02_W2GUBUN.SetValue("");
                    this.DAT02_W2TXAMT.SetValue("0");
                    this.DAT02_W2TXVAT.SetValue("0");
                    this.DAT02_W2HWAJU.SetValue("");

                    datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                this.DAT02_W2DPMK.GetValue().ToString(),
                                this.DAT02_W2DTMK.GetValue().ToString(),
                                this.DAT02_W2NOSQ.GetValue().ToString(),
                                this.DAT02_W2NOLN.GetValue().ToString(),
                                this.DAT02_W2IDJP.GetValue().ToString(),
                                this.DAT02_W2NOJP.GetValue().ToString(),
                                this.DAT02_W2CDAC.GetValue().ToString(),
                                this.DAT02_W2DTAC.GetValue().ToString(),
                                this.DAT02_W2DTLI.GetValue().ToString(),
                                this.DAT02_W2DPAC.GetValue().ToString(),
                                this.DAT02_W2CDMI1.GetValue().ToString(),
                                this.DAT02_W2VLMI1.GetValue().ToString(),
                                this.DAT02_W2CDMI2.GetValue().ToString(),
                                this.DAT02_W2VLMI2.GetValue().ToString(),
                                this.DAT02_W2CDMI3.GetValue().ToString(),
                                this.DAT02_W2VLMI3.GetValue().ToString(),
                                this.DAT02_W2CDMI4.GetValue().ToString(),
                                this.DAT02_W2VLMI4.GetValue().ToString(),
                                this.DAT02_W2CDMI5.GetValue().ToString(),
                                this.DAT02_W2VLMI5.GetValue().ToString(),
                                this.DAT02_W2CDMI6.GetValue().ToString(),
                                this.DAT02_W2VLMI6.GetValue().ToString(),
                                this.DAT02_W2AMDR.GetValue().ToString(),
                                this.DAT02_W2AMCR.GetValue().ToString(),
                                this.DAT02_W2CDFD.GetValue().ToString(),
                                this.DAT02_W2AMFD.GetValue().ToString(),
                                this.DAT02_W2RKAC.GetValue().ToString(),
                                this.DAT02_W2RKCU.GetValue().ToString(),
                                this.DAT02_W2WCJP.GetValue().ToString(),
                                this.DAT02_W2PRGB.GetValue().ToString(),
                                this.DAT02_W2HIGB.GetValue().ToString(),
                                this.DAT02_W2HISAB.GetValue().ToString(),
                                this.DAT02_W2GUBUN.GetValue().ToString(),
                                this.DAT02_W2TXAMT.GetValue().ToString(),
                                this.DAT02_W2TXVAT.GetValue().ToString(),
                                this.DAT02_W2HWAJU.GetValue().ToString()});


                    #endregion
                }
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();

                foreach (object[] data in datas)
                {
                    // 미승인 임사피일에 등록
                    this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                }
            }

            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID,
                                                        this.ProgramNo,
                                                        Employer.EmpNo,
                                                        "A",
                                                        sB2DPMK.ToString(),
                                                        Get_Date(this.DTP03_LOREDATE.GetValue().ToString()),
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        Employer.EmpNo
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            //전표 생성 함수 호출
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);

                DataTable dtresult = this.DbConnector.ExecuteDataTable();

                if (dtresult.Rows.Count > 0)
                {
                    if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                    {
                        //전표번호 받아오기
                        string sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();

                        if (sJpno.Trim() != "")
                        {
                            // 전표번호
                            this.TXT03_LOREJPNO.SetValue(sJpno.ToString().Replace("-", "").ToString());

                            // 상환관리 전표번호 업데이트
                            UP_LOREPAYMF_UPT_JPNO("CREATE");

                            // 대환일 경우에만 차입금 실행에 전표번호 업데이트함
                            if (this.CBO03_LOREACGUBN.GetValue().ToString() == "D")
                            {
                                UP_LOACTIONMF_UPDATE_JPNO();
                            }

                            // 상환관리 전표관련 버튼 DISPLAY
                            UP_LOREPAYMF_JPNO_BTN_DISPLAY(false, false, false, true);

                            // 상환관리 확정 버튼 DISPLAY
                            UP_LOREPAYMF_FIX_BTN_DISPLAY(false, false);

                            // 상환관리 필드 ReadOnly
                            UP_LOREPAYMF_ReadOnly(true);

                            // 상환관리 조회
                            UP_LOREPAYMF_INQ();

                            this.ShowMessage("TY_M_AC_25O8K620");
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 상환관리 전표 취소 버튼
        private void BTN63_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //미승인전표 -> 임시파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT03_LOREJPNO.GetValue().ToString().Substring(0, 6),
                                                                 this.TXT03_LOREJPNO.GetValue().ToString().Substring(6, 8),
                                                                 this.TXT03_LOREJPNO.GetValue().ToString().Substring(14, 3)
                                                                 );
            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID,
                                                        this.ProgramNo, Employer.EmpNo,
                                                        "D",
                                                        this.TXT03_LOREJPNO.GetValue().ToString().Substring(0, 6),
                                                        this.TXT03_LOREJPNO.GetValue().ToString().Substring(6, 8),
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        Employer.EmpNo);

            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, ""); // SP CALL
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                // 전표번호
                this.TXT03_LOREJPNO.SetValue("");

                // 상환관리 전표번호 업데이트
                UP_LOREPAYMF_UPT_JPNO("DEL");

                // 대환일 경우에만 차입금 실행에 전표번호 클리어함
                if (this.CBO03_LOREACGUBN.GetValue().ToString() == "D")
                {
                    UP_LOACTIONMF_UPDATE_JPNO();
                }

                // 상환관리 전표관련 버튼 DISPLAY
                UP_LOREPAYMF_JPNO_BTN_DISPLAY(true, true, true, false);

                // 상환관리 확정 버튼 DISPLAY
                UP_LOREPAYMF_FIX_BTN_DISPLAY(true, false);

                // 상환관리 필드 ReadOnly
                UP_LOREPAYMF_ReadOnly(false);

                // 상환관리 조회
                UP_LOREPAYMF_INQ();

                this.ShowMessage("TY_M_AC_25O8K620");
            }
        }
        #endregion

        #region Description : 상환관리 전표 출력 버튼
        private void BTN63_PRT_Click(object sender, EventArgs e)
        {
            if (this.TXT03_LOREJPNO.GetValue().ToString() != "")
            {
                string sJPNO = this.TXT03_LOREJPNO.GetValue().ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2AU2M916",
                    sJPNO.Substring(0, 6),
                    sJPNO.Substring(6, 8),
                    sJPNO.Substring(14, 3), // 시작 번호
                    sJPNO.Substring(14, 3)  // 종료 번호
                    );

                if (Convert.ToDouble(sJPNO.Substring(6, 4)) > 2014)
                {
                    SectionReport rpt = new TYACBJ0012R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
                else
                {
                    SectionReport rpt = new TYACBJ001R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Description : 상환관리 확정 버튼
        private void BTN63_FIX_Click(object sender, EventArgs e)
        {
            // 상환관리 확정 버튼 DISPLAY
            UP_LOREPAYMF_FIX_BTN_DISPLAY(false, true);

            // 상환금액
            this.TXT03_LOREAMT.SetValue(Set_Numeric2(this.TXT03_MAAMOUNT.GetValue().ToString(), 0));

            // 상환 확정 금액 열 Lock
            for (int i = 0; i < this.FPS91_TY_S_AC_874H2324.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 10].Locked = true;
            }


            // 수정된 데이터셋 가져오기
            fsDs = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;


            this.SetFocus(this.DTP03_LOREDATE);
        }
        #endregion

        #region Description : 상환관리 확정취소 버튼
        private void BTN63_FIX_CANCEL_Click(object sender, EventArgs e)
        {
            // 상환관리 확정 버튼 DISPLAY
            UP_LOREPAYMF_FIX_BTN_DISPLAY(true, false);

            this.TXT03_LOREAMT.SetValue("");

            // 상환 확정 금액 열 Lock
            for (int i = 0; i < this.FPS91_TY_S_AC_874H2324.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 10].Locked = false;
            }
        }
        #endregion

        #region Description : 상환관리 조회(여러건)
        private void UP_LOREPAYMF_INQ()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86RHC292",
                this.TXT01_STDATE.GetValue().ToString(),
                this.TXT01_EDDATE.GetValue().ToString(),
                this.TXT01_LOCCONTNO.GetValue().ToString(),
                this.CBH01_SBANK.GetValue().ToString(),
                this.MTB01_SDATE.GetValue().ToString().Replace("-", "").Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_875F6331.SetValue(dt);

            if (dt.Rows.Count <= 0)
            {
                // 상환관리 조회(단일건)
                UP_LOREPAYMF_SEARCH();
            }
        }
        #endregion

        #region Description : 상환관리 조회(단일건)
        private void UP_LOREPAYMF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86RHD293",
                this.TXT03_LORECONTYEAR.GetValue().ToString(),
                this.TXT03_LORECONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_875F6331.SetValue(dt);
        }
        #endregion

        #region Description : 상환관리 확인
        private void UP_LOREPAYMF_RUN()
        {
            this.BTN63_CODEHELP3.Visible = false;
            this.TXT03_LOACAMT.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86RHE294",
                this.TXT03_LORECONTYEAR.GetValue().ToString(),
                this.TXT03_LORECONTSEQ.GetValue().ToString(),
                fsLORECONTSEQ.ToString(),
                this.TXT03_LORENUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "03");

                fsWK_GUBUN3 = "UPT";

                UP_LOREPAYMF_BTN_DISPLAY(fsWK_GUBUN3);

                // 상환금액
                fsLOREAMT = Get_Numeric(dt.Rows[0]["LOREAMT"].ToString());

                if (dt.Rows[0]["LOREJPNO"].ToString() == "")
                {
                    // 상환관리 전표관련 버튼 DISPLAY
                    UP_LOREPAYMF_JPNO_BTN_DISPLAY(true, true, true, false);

                    // 상환관리 필드 ReadOnly
                    UP_LOREPAYMF_ReadOnly(false);
                }
                else
                {
                    // 상환관리 전표관련 버튼 DISPLAY
                    UP_LOREPAYMF_JPNO_BTN_DISPLAY(false, false, false, true);

                    // 상환관리 필드 ReadOnly
                    UP_LOREPAYMF_ReadOnly(true);
                }

                this.SetFocus(this.DTP01_LOACDATE);
            }
        }
        #endregion

        #region Description : 상환 내역관리 확인
        private void UP_LOREPAYNF_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_875FM332",
                this.TXT03_LORECONTYEAR.GetValue().ToString(),
                this.TXT03_LORECONTSEQ.GetValue().ToString(),
                fsLORECONTSEQ.ToString(),
                this.TXT03_LORENUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_874H2324.SetValue(dt);

            string sSTATUSNM = string.Empty;

            for (int i = 0; i < this.FPS91_TY_S_AC_874H2324.ActiveSheet.RowCount; i++)
            {
                sSTATUSNM = this.FPS91_TY_S_AC_874H2324.GetValue(i, "STATUSNM").ToString();

                if (sSTATUSNM.ToString() == "실행" || sSTATUSNM.ToString() == "상환" || sSTATUSNM.ToString() == "유동성 대체" || sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (sSTATUSNM.ToString() == "실행")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.Blue;
                }
                else if (sSTATUSNM.ToString() == "상환")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                }
                else if (sSTATUSNM.ToString() == "유동성 대체")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.LimeGreen;
                }
                else if (sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.Peru;
                }
            }

            // 상환 확정 금액 열 Lock
            for (int i = 0; i < this.FPS91_TY_S_AC_874H2324.ActiveSheet.RowCount; i++)
            {
                //this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 10].Locked = false;
            }

            // 상환관리 확정 버튼 DISPLAY
            UP_LOREPAYMF_FIX_BTN_DISPLAY(true, false);

            this.SetFocus(this.TXT03_MAAMOUNT);
        }
        #endregion

        #region Description : 상환관리 저장
        private void UP_LOREPAYMF_SAV()
        {
            int i          = 0;
            int iLORENDNUM = 0;

            string sLORECONTNO = string.Empty;

            sLORECONTNO = Set_Fill4(this.TXT03_LORECONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT03_LORECONTSEQ.GetValue().ToString());

            DataTable dt = new DataTable();

            //// 차입금 상환관리 SEQ 가져오기
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_873EV308",
            //    sLORECONTNO.ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.TXT03_LORECONTSEQ.SetValue(dt.Rows[0]["SEQ"].ToString());
            //    fsLORECONTSEQ = dt.Rows[0]["SEQ"].ToString();
            //}


            // 차입금 상환관리 순번 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_873EV308",
                sLORECONTNO.ToString(),
                fsLORECONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT03_LORENUM.SetValue(dt.Rows[0]["SEQ"].ToString());
            }

            double dLORENBALAMT = 0;
            double dLORENAMOUNT = 0;
            double dLORENREAMT  = 0;

            // 20190418 수정전 소스

            //// 상환관리 마스터 등록
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_873F3310",
            //                        sLORECONTNO.ToString(),
            //                        fsLORECONTSEQ.ToString(),
            //                        this.TXT03_LORENUM.GetValue().ToString(),
            //                        Get_Date(this.DTP03_LOREDATE.GetValue().ToString()),
            //                        Get_Numeric(this.TXT03_LOREAMT.GetValue().ToString()),
            //                        Get_Numeric(this.TXT03_LOCCONTRATE.GetValue().ToString()),
            //                        this.CBH03_LOREDPAC.GetValue().ToString(),
            //                        Get_Numeric(this.TXT03_LOREYUL.GetValue().ToString()),
            //                        Get_Numeric(this.TXT03_LOREDOLLAR.GetValue().ToString()),
            //                        this.CBH03_LORECDAC.GetValue().ToString(),
            //                        this.CBH03_LOREGRBK.GetValue().ToString(),
            //                        this.CBH03_LORENOAC.GetValue().ToString(),
            //                        this.TXT03_LORERKAC.GetValue().ToString(),
            //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
            //                        );



            // 20190418 수정후 소스

            // 상환관리 마스터 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_94IDB390",
                                    sLORECONTNO.ToString(),
                                    fsLORECONTSEQ.ToString(),
                                    this.TXT03_LORENUM.GetValue().ToString(),
                                    this.TXT03_LOREACCTNO.GetValue().ToString(),
                                    this.TXT03_LOREACCTSEQ.GetValue().ToString(),
                                    this.TXT03_LOREACCTNUM.GetValue().ToString(),
                                    this.CBO03_LOREACGUBN.GetValue().ToString(),
                                    Get_Date(this.DTP03_LOREDATE.GetValue().ToString()),
                                    Get_Numeric(this.TXT03_LOREAMT.GetValue().ToString()),
                                    Get_Numeric(this.TXT03_LOCCONTRATE.GetValue().ToString()),
                                    this.CBH03_LOREDPAC.GetValue().ToString(),
                                    Get_Numeric(this.TXT03_LOREYUL.GetValue().ToString()),
                                    Get_Numeric(this.TXT03_LOREDOLLAR.GetValue().ToString()),
                                    this.CBH03_LORECDAC.GetValue().ToString(),
                                    this.CBH03_LOREGRBK.GetValue().ToString(),
                                    this.CBH03_LORENOAC.GetValue().ToString(),
                                    this.TXT03_LORERKAC.GetValue().ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                                    );

            for (i = 0; i < fsDs.Tables[0].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(fsDs.Tables[0].Rows[i]["LORENAMOUNT"].ToString())) > 0)
                {
                    iLORENDNUM = i + 1;

                    dLORENBALAMT = double.Parse(Get_Numeric(fsDs.Tables[0].Rows[i]["LORENBALAMT"].ToString()));
                    dLORENAMOUNT = double.Parse(Get_Numeric(fsDs.Tables[0].Rows[i]["LORENAMOUNT"].ToString()));
                    dLORENREAMT  = double.Parse(Get_Numeric(fsDs.Tables[0].Rows[i]["LORENREAMT"].ToString()));

                    // 상환액
                    dLORENREAMT = dLORENREAMT + dLORENAMOUNT;

                    // 잔액
                    dLORENBALAMT = dLORENBALAMT - dLORENAMOUNT;

                    // 상환관리 내역 등록
                    this.DbConnector.Attach("TY_P_AC_875CT326",
                                            sLORECONTNO.ToString(),
                                            fsLORECONTSEQ.ToString(),
                                            this.TXT03_LORENUM.GetValue().ToString(),
                                            Convert.ToString(iLORENDNUM),
                                            fsDs.Tables[0].Rows[i]["LORENCHGB"].ToString(),
                                            Get_Date(fsDs.Tables[0].Rows[i]["LORENACDATE"].ToString()),
                                            Get_Date(fsDs.Tables[0].Rows[i]["LORENACRATE"].ToString()),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENACAMT"].ToString()),
                                            Convert.ToString(dLORENREAMT),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENLIAMT"].ToString()),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENLRAMT"].ToString()),
                                            Convert.ToString(dLORENBALAMT),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENAMOUNT"].ToString()),
                                            fsDs.Tables[0].Rows[i]["LORENWNJPNO"].ToString(),
                                            fsDs.Tables[0].Rows[i]["LORENCTNO"].ToString(),
                                            fsDs.Tables[0].Rows[i]["LORENCTSEQ"].ToString(),
                                            fsDs.Tables[0].Rows[i]["LORENCTNUM"].ToString(),
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                                            );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 상환관리 수정
        private void UP_LOREPAYMF_UPT()
        {
            int i          = 0;

            string sLORECONTNO = string.Empty;

            sLORECONTNO = Set_Fill4(this.TXT03_LORECONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT03_LORECONTSEQ.GetValue().ToString());

            // 20190418 수정전 소스

            //// 상환관리 마스터 수정
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_873F1314",
            //                        this.DTP03_LOREDATE.GetValue().ToString(),
            //                        Get_Numeric(this.TXT03_LOREAMT.GetValue().ToString()),
            //                        Get_Numeric(this.TXT03_LOCCONTRATE.GetValue().ToString()),
            //                        this.CBH03_LOREDPAC.GetValue().ToString(),
            //                        Get_Numeric(this.TXT03_LOREYUL.GetValue().ToString()),
            //                        Get_Numeric(this.TXT03_LOREDOLLAR.GetValue().ToString()),
            //                        this.CBH03_LORECDAC.GetValue().ToString(),
            //                        this.CBH03_LOREGRBK.GetValue().ToString(),
            //                        this.CBH03_LORENOAC.GetValue().ToString(),
            //                        this.TXT03_LORERKAC.GetValue().ToString(),
            //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                   // 작성사번
            //                        sLORECONTNO.ToString(),
            //                        fsLORECONTSEQ.ToString(),
            //                        this.TXT03_LORENUM.GetValue().ToString()
            //                        );


            
            // 20190418 수정후 소스

            // 상환관리 마스터 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_94IDC391",
                                    this.TXT03_LOREACCTNO.GetValue().ToString(),
                                    this.TXT03_LOREACCTSEQ.GetValue().ToString(),
                                    this.TXT03_LOREACCTNUM.GetValue().ToString(),
                                    this.CBO03_LOREACGUBN.GetValue().ToString(),
                                    this.DTP03_LOREDATE.GetValue().ToString(),
                                    Get_Numeric(this.TXT03_LOREAMT.GetValue().ToString()),
                                    Get_Numeric(this.TXT03_LOCCONTRATE.GetValue().ToString()),
                                    this.CBH03_LOREDPAC.GetValue().ToString(),
                                    Get_Numeric(this.TXT03_LOREYUL.GetValue().ToString()),
                                    Get_Numeric(this.TXT03_LOREDOLLAR.GetValue().ToString()),
                                    this.CBH03_LORECDAC.GetValue().ToString(),
                                    this.CBH03_LOREGRBK.GetValue().ToString(),
                                    this.CBH03_LORENOAC.GetValue().ToString(),
                                    this.TXT03_LORERKAC.GetValue().ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                   // 작성사번
                                    sLORECONTNO.ToString(),
                                    fsLORECONTSEQ.ToString(),
                                    this.TXT03_LORENUM.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteTranQuery();

            DataTable dt = new DataTable();

            string sLORENAMOUNT = string.Empty;

            for (i = 0; i < fsDs.Tables[0].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(fsDs.Tables[0].Rows[i]["LORENAMOUNT"].ToString())) > 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_87GCT395",
                        this.TXT03_LORECONTYEAR.GetValue().ToString(),
                        this.TXT03_LORECONTSEQ.GetValue().ToString(),
                        fsLORECONTSEQ.ToString(),
                        this.TXT03_LORENUM.GetValue().ToString(),
                        fsDs.Tables[0].Rows[i]["LORECONTNO"].ToString().Substring(12, 2)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sLORENAMOUNT = dt.Rows[0]["LORENAMOUNT"].ToString();
                    }

                    // 상환관리 내역 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_875CV327",
                                            fsDs.Tables[0].Rows[i]["LORENCHGB"].ToString(),
                                            Get_Date(fsDs.Tables[0].Rows[i]["LORENACDATE"].ToString()),
                                            Get_Date(fsDs.Tables[0].Rows[i]["LORENACRATE"].ToString()),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENACAMT"].ToString()),
                                            sLORENAMOUNT.ToString(),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENAMOUNT"].ToString()),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENLIAMT"].ToString()),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENLRAMT"].ToString()),
                                            sLORENAMOUNT.ToString(),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENAMOUNT"].ToString()),
                                            Get_Numeric(fsDs.Tables[0].Rows[i]["LORENAMOUNT"].ToString()),
                                            fsDs.Tables[0].Rows[i]["LORENWNJPNO"].ToString(),
                                            fsDs.Tables[0].Rows[i]["LORENCTNO"].ToString(),
                                            fsDs.Tables[0].Rows[i]["LORENCTSEQ"].ToString(),
                                            fsDs.Tables[0].Rows[i]["LORENCTNUM"].ToString(),
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                   // 작성사번
                                            sLORECONTNO.ToString(),
                                            fsLORECONTSEQ.ToString(),
                                            this.TXT03_LORENUM.GetValue().ToString(),
                                            fsDs.Tables[0].Rows[i]["LORECONTNO"].ToString().Substring(12, 2)
                                            );
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 상환관리 삭제
        private void UP_LOREPAYMF_DEL()
        {
            string sLORECONTNO = string.Empty;

            sLORECONTNO = Set_Fill4(this.TXT03_LORECONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT03_LORECONTSEQ.GetValue().ToString());

            this.DbConnector.CommandClear();

            // 상환관리 마스터 삭제 프로시저
            this.DbConnector.Attach("TY_P_AC_873F8312", sLORECONTNO.ToString(),
                                                        fsLORECONTSEQ.ToString(),
                                                        this.TXT03_LORENUM.GetValue().ToString()
                                                        );

            // 상환관리 내역 삭제 프로시저
            this.DbConnector.Attach("TY_P_AC_875CW328", sLORECONTNO.ToString(),
                                                        fsLORECONTSEQ.ToString(),
                                                        this.TXT03_LORENUM.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 상환관리 전표번호 업데이트
        private void UP_LOREPAYMF_UPT_JPNO(string sGUBUN)
        {
            string sLORECONTNO = string.Empty;

            sLORECONTNO = Set_Fill4(this.TXT03_LORECONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT03_LORECONTSEQ.GetValue().ToString());

            // 상환관리 마스터 전표번호 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_876GP337",
                                    this.TXT03_LOREJPNO.GetValue().ToString(),
                                    sLORECONTNO.ToString(),
                                    fsLORECONTSEQ.ToString(),
                                    this.TXT03_LORENUM.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            DataTable dt = new DataTable();

            // 차입금 상환관리 내역 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_875FM332",
                this.TXT03_LORECONTYEAR.GetValue().ToString(),
                this.TXT03_LORECONTSEQ.GetValue().ToString(),
                fsLORECONTSEQ.ToString(),
                this.TXT03_LORENUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                int iW2NOLN = 0;
                string sLORENJPNO = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iW2NOLN = iW2NOLN + 1;

                    if (sGUBUN.ToString() == "CREATE") // 전표 생성
                    {
                        sLORENJPNO = this.TXT03_LOREJPNO.GetValue().ToString() + Set_Fill2(Convert.ToString(iW2NOLN * 2));
                    }
                    else // 전표 취소
                    {
                        sLORENJPNO = this.TXT03_LOREJPNO.GetValue().ToString();
                    }

                    // 상환관리 내역 전표번호 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_876GQ338",
                                            sLORENJPNO.ToString(),
                                            sLORECONTNO.ToString(),
                                            fsLORECONTSEQ.ToString(),
                                            this.TXT03_LORENUM.GetValue().ToString(),
                                            dt.Rows[i]["LORECONTNO"].ToString().Substring(12, 2)
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Description : 실행관리 전표번호 업데이트
        private void UP_LOACTIONMF_UPDATE_JPNO()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_94IEP393",
                                    this.TXT03_LOREJPNO.GetValue().ToString(),
                                    this.TXT03_LOREACCTNO.GetValue().ToString(),
                                    this.TXT03_LOREACCTSEQ.GetValue().ToString(),
                                    this.TXT03_LOREACCTNUM.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteTranQueryList();
        }
        #endregion

        #region Description : 상환관리 저장 ProcessCheck
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_874H2324.GetDataSourceInclude(TSpread.TActionType.Update, "LOCCONTNO", "LORENCHGB", "LORENACDATE", "LORENACRATE", "LORENACAMT", "LORENREAMT", "LORENLIAMT", "LORENLRAMT", "LORENBALAMT", "LORENAMOUNT", "LORENCTNO", "LORENCTSEQ", "LORENCTNUM", "LORENWNJPNO", "LORECONTNO"));


            DataTable dt = new DataTable();

            // 전표계정 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_88NBH624",
                this.CBH03_LORECDAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_24RBZ877");
                this.CBH03_LORECDAC.Focus();

                e.Successed = false;
                return;
            }

            // 20180911 이성재 = 다른 은행에 상환할수 있으므로 체크 빼달라고 요청
            // 은행지점 체크
            //if (this.CBH03_LOREGRBK.GetValue().ToString() != this.CBH03_LOCGRBK.GetValue().ToString())
            //{
            //    this.ShowMessage("TY_M_AC_86IDZ231");
            //    this.CBH03_LOREGRBK.Focus();

            //    e.Successed = false;
            //    return;
            //}

            if (fsWK_GUBUN3.ToString() == "UPT")
            {
                // 전표번호 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_86RHE294",
                    this.TXT03_LORECONTYEAR.GetValue().ToString(),
                    this.TXT03_LORECONTSEQ.GetValue().ToString(),
                    fsLORECONTSEQ.ToString(),
                    this.TXT03_LORENUM.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LOREJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_GB_25F8V482");
                        this.TXT03_LORERKAC.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (double.Parse(Get_Numeric(this.TXT03_LOREYUL.GetValue().ToString())) != 0)
            {
                if (double.Parse(Get_Numeric(this.TXT03_LOREDOLLAR.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    this.TXT03_LOREDOLLAR.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (double.Parse(Get_Numeric(this.TXT03_LOREDOLLAR.GetValue().ToString())) != 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    this.TXT03_LOREDOLLAR.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 여기부터 할 차례(20190417)

            if (this.CBO03_LOREACGUBN.GetValue().ToString() == "S") // 상환
            {
                // 약정금액과 상환금액 비교
                if (double.Parse(Get_Numeric(this.TXT03_LOCCONTAMT.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT03_LOREAMT.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_AC_86QBS279");
                    this.TXT03_LOREAMT.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else // 대환
            {
                // 약정금액과 상환금액 비교
                if (double.Parse(Get_Numeric(this.TXT03_LOACAMT.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT03_LOREAMT.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_AC_86QBS279");
                    this.TXT03_LOREAMT.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 차입금 은행과 연결된 계좌번호 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86R95282",
                this.CBH03_LOREGRBK.GetValue().ToString(),
                this.CBH03_LORENOAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_86R9J284");
                SetFocus(this.CBH01_LOACGRBK.CodeText);

                e.Successed = false;
                return;
            }



            if (fsWK_GUBUN3.ToString() == "NEW")
            {
                // 저장하시겠습니까?
                if (!this.ShowMessage("TY_M_GB_23NAD871"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                // 수정하시겠습니까?
                if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
                {
                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 상환관리 삭제 ProcessCheck
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_86IFD234",
            //    this.TXT02_STYEAR.GetValue().ToString(),
            //    this.TXT02_LOCCONTSEQ.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_86IFE235");
                //this.SetFocus(this.CBO03_LOCCHANGEGB);

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 상환관리 확정  ProcessCheck
        private void BTN63_FIX_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sLORENAMOUNT = string.Empty;

            DataSet retDs = new DataSet();
            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("LOCCONTNO",   typeof(System.String));
            Condt.Columns.Add("LORENCHGB",   typeof(System.String));
            Condt.Columns.Add("LORENACDATE", typeof(System.String));
            Condt.Columns.Add("LORENACRATE", typeof(System.String));
            Condt.Columns.Add("LORENACAMT",  typeof(System.Double));
            Condt.Columns.Add("LORENREAMT",  typeof(System.String));
            Condt.Columns.Add("LORENLIAMT",  typeof(System.String));
            Condt.Columns.Add("LORENLRAMT",  typeof(System.String));
            Condt.Columns.Add("LORENBALAMT", typeof(System.String));
            Condt.Columns.Add("LORENAMOUNT", typeof(System.String));
            Condt.Columns.Add("LORENCTNO",   typeof(System.String));
            Condt.Columns.Add("LORENCTSEQ",  typeof(System.String));
            Condt.Columns.Add("LORENCTNUM",  typeof(System.String));
            Condt.Columns.Add("LORENWNJPNO", typeof(System.String));
            Condt.Columns.Add("LORECONTNO",  typeof(System.String));


            int i = 0;

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_874H2324.GetDataSourceInclude(TSpread.TActionType.All, "LOCCONTNO", "LORENCHGB", "LORENACDATE", "LORENACRATE", "LORENACAMT", "LORENREAMT", "LORENLIAMT", "LORENLRAMT", "LORENBALAMT", "LORENAMOUNT", "LORENCTNO", "LORENCTSEQ", "LORENCTNUM", "LORENWNJPNO", "LORECONTNO"));


            DataTable dt = new DataTable();

            // 상환 확정 금액은 잔액을 넘을 수가 없다.
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LORENAMOUNT"].ToString())) > 0)
                {
                    if (fsWK_GUBUN3.ToString() == "NEW")
                    {
                        if ((double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LORENBALAMT"].ToString()))) < double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LORENAMOUNT"].ToString())))
                        {
                            this.ShowMessage("TY_M_AC_876EA336");

                            e.Successed = false;
                            return;
                        }
                    }
                    else if (fsWK_GUBUN3.ToString() == "UPT")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_87GCT395",
                            this.TXT03_LORECONTYEAR.GetValue().ToString(),
                            this.TXT03_LORECONTSEQ.GetValue().ToString(),
                            fsLORECONTSEQ.ToString(),
                            this.TXT03_LORENUM.GetValue().ToString(),
                            ds.Tables[0].Rows[i]["LORECONTNO"].ToString().Substring(12, 2)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sLORENAMOUNT = dt.Rows[0]["LORENAMOUNT"].ToString();
                        }

                        if ((double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LORENBALAMT"].ToString())) + double.Parse(sLORENAMOUNT)) < double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LORENAMOUNT"].ToString())))
                        {
                            this.ShowMessage("TY_M_AC_876EA336");

                            e.Successed = false;
                            return;
                        }
                    }

                    // 유동성 대체일 경우 잔액을 모두 상환함
                    if (ds.Tables[0].Rows[i]["LORENCHGB"].ToString() == "02")
                    {
                        if (double.Parse(ds.Tables[0].Rows[i]["LORENACAMT"].ToString()) != double.Parse(ds.Tables[0].Rows[i]["LORENAMOUNT"].ToString()))
                        {
                            this.ShowMessage("TY_M_AC_87UCR483");

                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            double dLORENAMOUNT = 0;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LORENAMOUNT"].ToString())) > 0)
                {
                    dLORENAMOUNT = dLORENAMOUNT + double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LORENAMOUNT"].ToString()));

                    row = Condt.NewRow();

                    row["LOCCONTNO"]   = ds.Tables[0].Rows[i]["LOCCONTNO"].ToString();
                    row["LORENCHGB"]   = ds.Tables[0].Rows[i]["LORENCHGB"].ToString();
                    row["LORENACDATE"] = ds.Tables[0].Rows[i]["LORENACDATE"].ToString();
                    row["LORENACRATE"] = ds.Tables[0].Rows[i]["LORENACRATE"].ToString();
                    row["LORENACAMT"]  = ds.Tables[0].Rows[i]["LORENACAMT"].ToString();
                    row["LORENREAMT"]  = ds.Tables[0].Rows[i]["LORENREAMT"].ToString();
                    row["LORENLIAMT"]  = ds.Tables[0].Rows[i]["LORENLIAMT"].ToString();
                    row["LORENLRAMT"]  = ds.Tables[0].Rows[i]["LORENLRAMT"].ToString();
                    row["LORENBALAMT"] = ds.Tables[0].Rows[i]["LORENBALAMT"].ToString();
                    row["LORENAMOUNT"] = ds.Tables[0].Rows[i]["LORENAMOUNT"].ToString();
                    row["LORENCTNO"]   = ds.Tables[0].Rows[i]["LORENCTNO"].ToString();
                    row["LORENCTSEQ"]  = ds.Tables[0].Rows[i]["LORENCTSEQ"].ToString();
                    row["LORENCTNUM"]  = ds.Tables[0].Rows[i]["LORENCTNUM"].ToString();
                    row["LORENWNJPNO"] = ds.Tables[0].Rows[i]["LORENWNJPNO"].ToString();

                    Condt.Rows.Add(row);
                }
            }

            Condt.TableName = "TableNames";
            retDs.Tables.Add(Condt);

            // 여기부터 해야 함(20180824)

            if (double.Parse(Get_Numeric(this.TXT03_MAAMOUNT.GetValue().ToString())) != dLORENAMOUNT)
            {
                this.ShowMessage("TY_M_AC_874HT325");
                this.TXT03_MAAMOUNT.Focus();

                e.Successed = false;
                return;
            }

            e.ArgData = retDs;
        }
        #endregion

        #region Description : 상환관리 전표생성 ProcessCheck
        private void BTN63_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 상환관리 전표취소 ProcessCheck
        private void BTN63_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //DataTable dt = new DataTable();

            //// 차입금 상환관리 DATA 존재 체크
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_86RAF290",
            //    this.TXT03_LOREJPNO.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.ShowMessage("TY_M_AC_86RAJ291");

            //    e.Successed = false;
            //    return;
            //}

            if (!this.ShowMessage("TY_M_AC_25O8K619"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 상환관리 계약조회 스프레드 이벤트
        private void FPS91_TY_S_AC_86SAY296_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sLORECONTNO = string.Empty;

            // 상환/대환 구분
            fsLOACGUBN_03 = "";
            fsLOACGUBN_03 = this.FPS91_TY_S_AC_86SAY296.GetValue("GUBUN").ToString();

            this.CBO03_LOREACGUBN.SetValue(this.FPS91_TY_S_AC_86SAY296.GetValue("GUBUN").ToString());

            if (this.FPS91_TY_S_AC_86SAY296.GetValue("GUBUN").ToString() == "S") // 상환
            {
                sLORECONTNO = this.FPS91_TY_S_AC_86SAY296.GetValue("LOCCONTNO").ToString().Replace("-", "");

                this.TXT03_LORECONTYEAR.SetValue(sLORECONTNO.ToString().Substring(0, 4));
                this.TXT03_LORECONTSEQ.SetValue(sLORECONTNO.ToString().Substring(4, 2));

                DataTable dt = new DataTable();

                // 계약관리 마지막 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_873FK315",
                    sLORECONTNO.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsLORECONTSEQ = dt.Rows[0]["LOCCONTSEQ"].ToString();
                }

                // 차입금 계약내용 가져오기(계약의 마지막 내용 가져오기)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_86IHQ241",
                    sLORECONTNO.ToString().Substring(0, 4),
                    sLORECONTNO.ToString().Substring(4, 2),
                    fsLORECONTSEQ.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "03");
                }
            }
            else // 대환
            {
                sLORECONTNO = this.FPS91_TY_S_AC_86SAY296.GetValue("LOCCONTNO").ToString().Replace("-", "");
            }

            // 차입금 상환할 내용 DISPLAY
            UP_LOREPAYMF_Items_Display(sLORECONTNO, this.FPS91_TY_S_AC_86SAY296.GetValue("STATUSNM").ToString());

            Timer tmr = new Timer();
            tmr.Tick += new EventHandler(tmr_Tick1);
            tmr.Interval = 100;
            tmr.Start();

        }

        void tmr_Tick1(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            this.TXT03_MAAMOUNT.Focus();
        }
        #endregion

        #region Description : 상환관리 조회 스프레드 이벤트
        private void FPS91_TY_S_AC_875F6331_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Timer tmr = new Timer();
            //tmr.Tick += new EventHandler(tmr_Tick);
            //tmr.Interval = 100;
            //tmr.Start();

            this.TXT03_LORECONTYEAR.SetValue(this.FPS91_TY_S_AC_875F6331.GetValue("LORECONTNO").ToString().Substring(0, 4));
            this.TXT03_LORECONTSEQ.SetValue(this.FPS91_TY_S_AC_875F6331.GetValue("LORECONTNO").ToString().Substring(5, 2));
            this.TXT03_LORENUM.SetValue(this.FPS91_TY_S_AC_875F6331.GetValue("LORECONTNO").ToString().Substring(8, 3));
            fsLORECONTSEQ = this.FPS91_TY_S_AC_875F6331.GetValue("LORECONTSEQ").ToString();

            // 상환관리 확인
            UP_LOREPAYMF_RUN();

            // 상환 확정금액
            this.TXT03_MAAMOUNT.SetValue(Set_Numeric2(this.FPS91_TY_S_AC_875F6331.GetValue("LOREAMT").ToString(), 0));
            
            // 상환 내역관리 확인
            UP_LOREPAYNF_RUN();
        }
        #endregion

        #region Description : 상환관리 스프레드 전표 출력 이벤트
        private void FPS91_TY_S_AC_875F6331_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "18")
            {
                if (this.FPS91_TY_S_AC_875F6331.GetValue("LOREJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_875F6331.GetValue("LOREJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_875F6331.GetValue("LOREJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_875F6331.GetValue("LOREJPNO").ToString().Substring(14, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        SectionReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        SectionReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }
            }
        }
        #endregion

        #region Description : 차입금 상환할 내용 DISPLAY
        private void UP_LOREPAYMF_Items_Display(string sCONTNO, string sSTATUS)
        {
            int i = 0;

            // 상환관리 확정 버튼 DISPLAY
            UP_LOREPAYMF_FIX_BTN_DISPLAY(true, false);

            string sProcedure = string.Empty;

            if (this.CBO03_LOREACGUBN.GetValue().ToString() == "S") // 상환
            {
                sProcedure = "TY_P_AC_88UCQ668";
            }
            else // 대환
            {
                if (sSTATUS.ToString() == "실행")
                {
                    sProcedure = "TY_P_AC_94HGP385";
                }
                else if (sSTATUS.ToString() == "유동성 대체")
                {
                    sProcedure = "TY_P_AC_94HGZ386";
                }
                else if (sSTATUS.ToString() == "유동성 재대체")
                {
                    sProcedure = "TY_P_AC_94HH1387";
                }
            }

            DataTable dt = new DataTable();

            // 20180830 수정전 소스
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_874H8323",
            //    sCONTNO.ToString()
            //    );

            // 20180830 수정후 소스
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                sCONTNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (this.CBO03_LOREACGUBN.GetValue().ToString() == "S") // 상환
            {
                this.FPS91_TY_S_AC_874H2324.SetValue(dt);
            }
            else // 대환
            {
                if (this.FPS91_TY_S_AC_874H2324.ActiveSheet.RowCount == 0)
                {
                    this.FPS91_TY_S_AC_874H2324.SetValue(dt);
                }
                else
                {
                    int iPosition = 0;

                    iPosition = this.FPS91_TY_S_AC_874H2324.ActiveSheet.RowCount;

                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        this.FPS91_TY_S_AC_874H2324_Sheet1.AddRows(iPosition + i, 1);

                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LOCCONTNO",   dt.Rows[i]["LOCCONTNO"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENCHGB",   dt.Rows[i]["LORENCHGB"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "STATUSNM",    dt.Rows[i]["STATUSNM"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENACDATE", Set_Date(dt.Rows[i]["LORENACDATE"].ToString()));
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENACRATE", dt.Rows[i]["LORENACRATE"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENACAMT",  dt.Rows[i]["LORENACAMT"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENREAMT",  dt.Rows[i]["LORENREAMT"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENLIAMT",  dt.Rows[i]["LORENLIAMT"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENLRAMT",  dt.Rows[i]["LORENLRAMT"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENBALAMT", dt.Rows[i]["LORENBALAMT"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENAMOUNT", dt.Rows[i]["LORENAMOUNT"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENCTNO",   dt.Rows[i]["LORENCTNO"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENCTSEQ",  dt.Rows[i]["LORENCTSEQ"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENCTNUM",  dt.Rows[i]["LORENCTNUM"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORENWNJPNO", dt.Rows[i]["LORENWNJPNO"].ToString());
                        this.FPS91_TY_S_AC_874H2324.SetValue(iPosition + i, "LORECONTNO",  "");
                    }
                }
            }




            string sSTATUSNM = string.Empty;

            for (i = 0; i < this.FPS91_TY_S_AC_874H2324.ActiveSheet.RowCount; i++)
            {
                sSTATUSNM = this.FPS91_TY_S_AC_874H2324.GetValue(i, "STATUSNM").ToString();

                if (sSTATUSNM.ToString() == "실행" || sSTATUSNM.ToString() == "상환" || sSTATUSNM.ToString() == "유동성 대체" || sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (sSTATUSNM.ToString() == "실행")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.Blue;
                }
                else if (sSTATUSNM.ToString() == "상환")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                }
                else if (sSTATUSNM.ToString() == "유동성 대체")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.LimeGreen;
                }
                else if (sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_874H2324_Sheet1.Cells[i, 2].ForeColor = Color.Peru;
                }
            }
        }
        #endregion

        #region Description : 상환관리 계좌번호 이벤트
        private void CBH03_LOREGRBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = string.Empty;

            if (this.CBH03_LOREGRBK.GetValue().ToString().Length >= 3)
            {
                groupCode = this.CBH03_LOREGRBK.GetValue().ToString().Substring(0, 3).ToString();
            }

            this.CBH03_LORENOAC.DummyValue = groupCode;
            this.CBH03_LORENOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH03_LORENOAC.Initialize();
        }
        #endregion

        #region Description : 상환관리 계약 종료년도 이벤트
        private void TXT03_EDYEAR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.BTN63_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 상환관리 적요 이벤트
        private void TXT03_LORERKAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.BTN63_SAV);
            }
        }
        #endregion

        #region Description : 상환일자 이벤트
        private void DTP03_LOREDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH03_LOREDPAC.DummyValue = this.DTP03_LOREDATE.GetString();
        }
        #endregion

        #region Description : 상환관리 필드 클리어
        private void UP_LOREPAYMF_FieldClear()
        {
            fsLORECONTSEQ = "";

            // 계약사항 필드 클리어
            this.TXT03_LOREACCTNO.SetValue("");
            this.TXT03_LOREACCTSEQ.SetValue("");
            this.TXT03_LOREACCTNUM.SetValue("");

            this.CBH03_LOCBANKCD.SetValue("");
            this.CBH03_LOCGRBK.SetValue("");
            this.CBH03_LOCCDAC.SetValue("");
            this.TXT03_LOCCONTRATE.SetValue("");
            this.DTP03_LOCCONTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP03_LOCENDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBH03_LOCCURRCD.SetValue("");
            this.TXT03_LOCCONTYUL.SetValue("");
            this.CBO03_LOCRATEGB.SetValue("");
            this.TXT03_LOCCONTAMT.SetValue("");
            this.TXT03_LOCCREDITAMT.SetValue("");
            this.CBH03_LOCDESCGB.SetValue("");
            this.CBH03_LOCLOANTYPE.SetValue("");

            this.TXT03_LOACAMT.SetValue("");

            // 필드 클리어
            this.TXT03_LORECONTYEAR.SetValue("");
            this.TXT03_LORECONTSEQ.SetValue("");
            this.TXT03_LORENUM.SetValue("");

            this.DTP03_LOREDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT03_LOREAMT.SetValue("");
            this.CBH03_LOREDPAC.SetValue("");
            this.TXT03_LOREYUL.SetValue("");
            this.TXT03_LOREDOLLAR.SetValue("");
            this.CBH03_LORECDAC.SetValue("");
            this.CBH03_LOREGRBK.SetValue("");
            this.CBH03_LORENOAC.SetValue("");
            this.TXT03_LORERKAC.SetValue("");
            this.TXT03_LOREJPNO.SetValue("");

            this.TXT03_MAAMOUNT.SetValue("");
        }
        #endregion

        #region Description : 상환관리 필드 ReadOnly
        private void UP_LOREPAYMF_ReadOnly(bool bFalse)
        {
            this.TXT03_LORENUM.SetReadOnly(bFalse);

            this.DTP03_LOREDATE.SetReadOnly(bFalse);
            //this.TXT03_LOREAMT.SetReadOnly(bFalse);
            this.CBH03_LOREDPAC.SetReadOnly(bFalse);
            this.TXT03_LOREYUL.SetReadOnly(bFalse);
            this.TXT03_LOREDOLLAR.SetReadOnly(bFalse);
            this.CBH03_LORECDAC.SetReadOnly(bFalse);
            this.CBH03_LOREGRBK.SetReadOnly(bFalse);
            this.CBH03_LORENOAC.SetReadOnly(bFalse);
            this.TXT03_LORERKAC.SetReadOnly(bFalse);
        }
        #endregion

        #region Description : 상환관리 버튼 디스플레이
        private void UP_LOREPAYMF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN63_INQ.Visible = true;

                this.BTN63_SAV.Visible = true;
                this.BTN63_REM.Visible = false;

                // 상환관리 전표관련 버튼 DISPLAY
                UP_LOREPAYMF_JPNO_BTN_DISPLAY(true, false, false, false);

                this.TXT03_LORENUM.SetReadOnly(false);
                this.CBH03_LORENOAC.SetReadOnly(true);
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN63_INQ.Visible = false;

                this.BTN63_SAV.Visible = true;
                this.BTN63_REM.Visible = true;

                // 상환관리 전표관련 버튼 DISPLAY
                UP_LOREPAYMF_JPNO_BTN_DISPLAY(true, true, true, false);

                this.TXT03_LORENUM.SetReadOnly(true);
                this.CBH03_LORENOAC.SetReadOnly(false);
            }
            else
            {
                this.BTN63_INQ.Visible = false;

                this.BTN63_SAV.Visible = false;
                this.BTN63_REM.Visible = false;

                // 상환관리 전표관련 버튼 DISPLAY
                UP_LOREPAYMF_JPNO_BTN_DISPLAY(false,false, false, false);

                this.TXT03_LORENUM.SetReadOnly(true);
                this.CBH03_LORENOAC.SetReadOnly(true);
            }

            // 상환관리 확정 버튼 DISPLAY
            UP_LOREPAYMF_FIX_BTN_DISPLAY(false, false);
        }
        #endregion

        #region Description : 상환관리 전표관련 버튼 DISPLAY
        private void UP_LOREPAYMF_JPNO_BTN_DISPLAY(bool bflag1, bool bflag2, bool bflag3, bool bflag4)
        {
            this.BTN63_SAV.Visible = bflag1;
            this.BTN63_REM.Visible = bflag2;

            this.BTN63_JPNO_CRE.Visible      = bflag3;
            this.BTN63_JUNPYO_CANCEL.Visible = bflag4;
            this.BTN63_PRT.Visible           = bflag4;
        }
        #endregion

        #region Description : 상환관리 확정 버튼 DISPLAY
        private void UP_LOREPAYMF_FIX_BTN_DISPLAY(bool bflag1, bool bflag2)
        {
            this.BTN63_FIX.Visible = bflag1;
            this.BTN63_FIX_CANCEL.Visible = bflag2;
        }
        #endregion

        #region Description : 대환 - 실행 조회
        private void BTN63_CODEHELP3_Click(object sender, EventArgs e)
        {
            TYACLO02C2 popup = new TYACLO02C2();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT03_LORECONTYEAR.SetValue("");
                this.TXT03_LORECONTSEQ.SetValue("");

                this.TXT03_LORECONTYEAR.SetValue(Set_Fill4(popup.fsLOREACCTNO.Substring(0, 4)));
                this.TXT03_LORECONTSEQ.SetValue(Set_Fill2(popup.fsLOREACCTNO.Substring(4, 2)));

                this.TXT03_LOREACCTNO.SetValue(popup.fsLOREACCTNO);   // 실행 계약번호(년월)
                this.TXT03_LOREACCTSEQ.SetValue(popup.fsLOREACCTSEQ); // 실행 계약번호(번호)
                this.TXT03_LOREACCTNUM.SetValue(popup.fsLOREACCTNUM); // 실행순번

                DataTable dt = new DataTable();

                // 차입금 계약내용 가져오기(계약의 마지막 내용 가져오기)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_94HE0384",
                    this.TXT03_LOREACCTNO.GetValue().ToString(),
                    this.TXT03_LOREACCTSEQ.GetValue().ToString(),
                    this.TXT03_LOREACCTNUM.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //this.CurrentDataTableRowMapping(dt, "03");

                    this.CBH03_LOCBANKCD.SetValue(dt.Rows[0]["LOCBANKCD"].ToString());

                    this.CBH03_LOCGRBK.SetValue(dt.Rows[0]["LOCGRBK"].ToString());
                    this.CBH03_LOCCDAC.SetValue(dt.Rows[0]["LOCCDAC"].ToString());
                    this.TXT03_LOCCONTRATE.SetValue(dt.Rows[0]["LOCCONTRATE"].ToString());
                    this.DTP03_LOCCONTDATE.SetValue(dt.Rows[0]["LOCCONTDATE"].ToString());
                    this.DTP03_LOCENDDATE.SetValue(dt.Rows[0]["LOCENDDATE"].ToString());
                    this.CBH03_LOCCURRCD.SetValue(dt.Rows[0]["LOCCURRCD"].ToString());
                    this.TXT03_LOCCONTYUL.SetValue(dt.Rows[0]["LOCCONTYUL"].ToString());
                    this.CBO03_LOCRATEGB.SetValue(dt.Rows[0]["LOCRATEGB"].ToString());
                    this.TXT03_LOCCONTAMT.SetValue(dt.Rows[0]["LOCCONTAMT"].ToString());
                    this.TXT03_LOCCREDITAMT.SetValue(dt.Rows[0]["LOCCREDITAMT"].ToString());
                    this.CBH03_LOCDESCGB.SetValue(dt.Rows[0]["LOCDESCGB"].ToString());
                    this.CBH03_LOCLOANTYPE.SetValue(dt.Rows[0]["LOCLOANTYPE"].ToString());
                    this.TXT03_LOACAMT.SetValue(dt.Rows[0]["AMT"].ToString());
                }

                fsLORECONTSEQ = "";

                // 계약관리 마지막 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_873FK315",
                    popup.fsLOREACCTNO.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsLORECONTSEQ = dt.Rows[0]["LOCCONTSEQ"].ToString();
                }
            }
        }
        #endregion

        #region Description : 상환 및 대환 이벤트
        private void CBO03_LOACGUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            fsDs.Clear();

            // 상환금액
            fsLOREAMT = "0";

            this.FPS91_TY_S_AC_86SAY296.Initialize();
            this.FPS91_TY_S_AC_874H2324.Initialize();

            UP_LOREPAYMF_FieldClear();

            if (this.CBO03_LOACGUBN.GetValue().ToString() == "S")
            {
                this.BTN63_CODEHELP3.Visible = false;
            }
            else
            {
                this.BTN63_CODEHELP3.Visible = true;
            }
        }
        #endregion

        #endregion
        






        #region Description : 메인 및 공통

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fsWK_GUBUN1 = "";
            fsWK_GUBUN2 = "";
            fsWK_GUBUN3 = "";

            this.DTP02_LOACDATE.SetReadOnly(true);
            this.TXT02_LOACAMT.SetReadOnly(true);

            this.TXT03_LOACAMT.SetReadOnly(true);

            if (tabControl1.SelectedIndex == 0) // 차입금 이력 조회
            {
                UP_LABEL_DISPLAY(false, 0);

                // 차입금 이력 조회
                UP_LOAN_MAIN_INQ();
            }
            else if (tabControl1.SelectedIndex == 1) // 차입금 실행 관리
            {
                UP_LABEL_DISPLAY(true, 1);

                this.BTN61_CODEHELP.Visible = false;

                this.TXT01_LOCCONTYEAR.SetReadOnly(true);

                fsTAB_GUBUN = "LOACTIONMF";

                fsWK_GUBUN1 = "TAB";
                UP_LOACTIONMF_BTN_DISPLAY(fsWK_GUBUN1);

                this.TXT01_LOCCONTYEAR.SetValue(fsLOCCONTYEAR.ToString());
                this.TXT01_LOCCONTSEQ.SetValue(fsLOCCONTNUM.ToString());

                // 실행관리 조회(여러건)
                UP_LOACTIONMF_INQ();
            }
            else if (tabControl1.SelectedIndex == 2) // 유동성 관리
            {
                UP_LABEL_DISPLAY(true, 2);

                this.BTN62_CODEHELP1.Visible = false;

                fsTAB_GUBUN = "LOLIQUIDMF";

                fsWK_GUBUN2 = "TAB";
                UP_LOLIQUIDMF_BTN_DISPLAY(fsWK_GUBUN2);

                // 유동성관리 조회(여러건)
                UP_LOLIQUIDMF_INQ();
            }
            else if (tabControl1.SelectedIndex == 3) // 상환 관리
            {
                UP_LABEL_DISPLAY(true, 3);

                fsTAB_GUBUN = "LOREPAYMF";

                fsWK_GUBUN3 = "TAB";
                UP_LOREPAYMF_BTN_DISPLAY(fsWK_GUBUN3);


                // 상환관리 조회(여러건)
                UP_LOREPAYMF_INQ();
            }
        }
        #endregion

        #region Description : 일자 DISPLAY
        private void UP_LABEL_DISPLAY(bool bflag, int i)
        {
            this.LBL51_SDATE.Visible = bflag;
            this.MTB01_SDATE.Visible = bflag;

            if (i == 1)
            {
                this.LBL51_SDATE.Text = "실행일자";
            }
            else if (i == 2)
            {
                this.LBL51_SDATE.Text = "대체일자";
            }
            else if (i == 3)
            {
                this.LBL51_SDATE.Text = "상환일자";
            }
        }
        #endregion

        #region Description : 조회시 일자 텍스트박스 이벤트
        private void MTB01_SDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.BTN61_INQ);

                if (fsTAB_GUBUN.ToString() == "MAIN")            // 메인(차입금 이력조회) 
                {
                    // 차입금 이력 조회
                    UP_LOAN_MAIN_INQ();
                }
                else if (fsTAB_GUBUN.ToString() == "LOACTIONMF") // 실행관리 조회
                {
                    // 실행관리 조회(단일건)
                    UP_LOACTIONMF_SEARCH();
                }
                else if (fsTAB_GUBUN.ToString() == "LOLIQUIDMF") // 유동성관리 조회
                {
                    // 유동성관리 조회
                    UP_LOLIQUIDMF_SEARCH();
                }
                else if (fsTAB_GUBUN.ToString() == "LOREPAYMF")  // 상환관리 조회
                {
                    // 상환관리 조회
                    UP_LOREPAYMF_INQ();
                }
            }
        }
        #endregion

        #region Description : 계약 최종 DATA 확인
        private void UP_LOCONTMF_FINAL_RUN(string sLOCCONTYEAR, string sLOCCONTSEQ, string sGUBUN)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IH6237",
                sLOCCONTYEAR.ToString(),
                sLOCCONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (sGUBUN == "LOACTIONMF")
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    // 약정 이자율 => 실행 이자율
                    this.TXT01_LOACRATE.SetValue(dt.Rows[0]["LOCCONTRATE"].ToString());

                    this.SetFocus(this.DTP01_LOACDATE);
                }
                else if (sGUBUN == "LOLIQUIDMF")
                {
                    this.CurrentDataTableRowMapping(dt, "02");

                    this.SetFocus(this.DTP02_LOLIDATE);

                    this.DTP02_LOACDATE.SetReadOnly(true);
                    this.TXT02_LOACAMT.SetReadOnly(true);
                }
            }
        }
        #endregion

        #region Description : 귀속부서 및 부서코드 가져오기
        private string UP_GET_INKIBNMF(string sGUBUN)
        {
            string sReturn = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_77CF4145",
                TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (sGUBUN == "DPAC")
                {
                    sReturn = dt.Rows[0]["KBBUSEO"].ToString();
                }
                else
                {
                    sReturn = dt.Rows[0]["KBBSTEAM"].ToString();
                }
            }

            return sReturn;
        }
        #endregion

        #region Description : 귀속부서 및 부서코드 가져오기
        private string UP_GET_B2VLMI(string sB2CDMI, string sGUBUN, string sGRBK, string sCONTNO)
        {
            string sReturn = string.Empty;

            switch (sB2CDMI.ToString())
            {
                case "02": // 금융기관
                    if (sGUBUN == "LOACTIONMF")
                    {
                        sReturn = this.CBH01_LOACGRBK.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOLIQUIDMF")
                    {
                        sReturn = this.CBH02_LOCGRBK.GetValue().ToString();
                    }
                    else if(sGUBUN == "LOREPAYMF")
                    {
                        sReturn = sGRBK.ToString();
                    }
                    
                    break;

                case "07": // 계좌번호

                    if (sGUBUN == "LOACTIONMF")
                    {
                        sReturn = this.CBH01_LOACNOAC.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOLIQUIDMF")
                    {
                        sReturn = "";
                    }
                    else if (sGUBUN == "LOREPAYMF")
                    {
                        sReturn = this.CBH03_LORENOAC.GetValue().ToString();
                    }

                    break;

                case "12": // 차입금 관리번호

                    if (sGUBUN == "LOACTIONMF")
                    {
                        // 실행 관리번호
                        sReturn = Set_Fill4(this.TXT01_LOACCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOACCONTSEQ.GetValue().ToString()) + Set_Fill3(fsLOACCONTSEQ.ToString()) + Set_Fill3(this.TXT01_LOACNUM.GetValue().ToString());
                    }
                    else if (sGUBUN == "LOLIQUIDMF")
                    {
                        // 유동성 대체 관리번호
                        sReturn = Set_Fill4(this.TXT02_LOLICONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOLICONTSEQ.GetValue().ToString()) + Set_Fill3(fsLOLICONTSEQ.ToString()) + Set_Fill3(this.TXT02_LOLINUM.GetValue().ToString());
                    }
                    else if (sGUBUN == "LOREPAYMF")
                    {
                        // 상환 관리번호
                        sReturn = sCONTNO.ToString();
                    }

                    break;

                case "20": // 이자율

                    if (sGUBUN == "LOACTIONMF")
                    {
                        sReturn = this.TXT01_LOACRATE.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOLIQUIDMF")
                    {
                        sReturn = this.TXT02_LOCCONTRATE.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOREPAYMF")
                    {
                        sReturn = this.TXT03_LOCCONTRATE.GetValue().ToString();
                    }

                    break;

                case "21": // 외화금액

                    if (sGUBUN == "LOACTIONMF")
                    {
                        sReturn = this.TXT01_LOACDOLLAR.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOLIQUIDMF")
                    {
                        sReturn = this.TXT02_LOLIDOLLAR.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOREPAYMF")
                    {
                        sReturn = this.TXT03_LOREDOLLAR.GetValue().ToString();
                    }

                    break;

                case "30": // 외화구분

                    if (sGUBUN == "LOACTIONMF")
                    {
                        sReturn = this.CBH01_LOCCURRCD.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOLIQUIDMF")
                    {
                        sReturn = this.CBH02_LOCCURRCD.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOREPAYMF")
                    {
                        sReturn = this.CBH03_LOCCURRCD.GetValue().ToString();
                    }

                    break;

                case "36": // 환율

                    if (sGUBUN == "LOACTIONMF")
                    {
                        sReturn = this.TXT01_LOACYUL.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOLIQUIDMF")
                    {
                        sReturn = this.TXT02_LOLIYUL.GetValue().ToString();
                    }
                    else if (sGUBUN == "LOREPAYMF")
                    {
                        sReturn = this.TXT03_LOREYUL.GetValue().ToString();
                    }

                    break;
            }
            

            return sReturn;
        }
        #endregion

        #region Description : 전표 ControlFactory
        private void UP_ControlFactory()
        {
            this.ControlFactory.Add(this.DAT02_W2SSID);
            this.ControlFactory.Add(this.DAT02_W2DPMK);
            this.ControlFactory.Add(this.DAT02_W2DTMK);
            this.ControlFactory.Add(this.DAT02_W2NOSQ);
            this.ControlFactory.Add(this.DAT02_W2NOLN);
            this.ControlFactory.Add(this.DAT02_W2IDJP);
            this.ControlFactory.Add(this.DAT02_W2NOJP);
            this.ControlFactory.Add(this.DAT02_W2CDAC);
            this.ControlFactory.Add(this.DAT02_W2DTAC);
            this.ControlFactory.Add(this.DAT02_W2DTLI);
            this.ControlFactory.Add(this.DAT02_W2DPAC);
            this.ControlFactory.Add(this.DAT02_W2CDMI1);
            this.ControlFactory.Add(this.DAT02_W2VLMI1);
            this.ControlFactory.Add(this.DAT02_W2CDMI2);
            this.ControlFactory.Add(this.DAT02_W2VLMI2);
            this.ControlFactory.Add(this.DAT02_W2CDMI3);
            this.ControlFactory.Add(this.DAT02_W2VLMI3);
            this.ControlFactory.Add(this.DAT02_W2CDMI4);
            this.ControlFactory.Add(this.DAT02_W2VLMI4);
            this.ControlFactory.Add(this.DAT02_W2CDMI5);
            this.ControlFactory.Add(this.DAT02_W2VLMI5);
            this.ControlFactory.Add(this.DAT02_W2CDMI6);
            this.ControlFactory.Add(this.DAT02_W2VLMI6);
            this.ControlFactory.Add(this.DAT02_W2AMDR);
            this.ControlFactory.Add(this.DAT02_W2AMCR);
            this.ControlFactory.Add(this.DAT02_W2CDFD);
            this.ControlFactory.Add(this.DAT02_W2AMFD);
            this.ControlFactory.Add(this.DAT02_W2RKAC);
            this.ControlFactory.Add(this.DAT02_W2RKCU);
            this.ControlFactory.Add(this.DAT02_W2WCJP);
            this.ControlFactory.Add(this.DAT02_W2PRGB);
            this.ControlFactory.Add(this.DAT02_W2HIGB);
            this.ControlFactory.Add(this.DAT02_W2HISAB);
            this.ControlFactory.Add(this.DAT02_W2GUBUN);
            this.ControlFactory.Add(this.DAT02_W2TXAMT);
            this.ControlFactory.Add(this.DAT02_W2TXVAT);
            this.ControlFactory.Add(this.DAT02_W2HWAJU); 
        }
        #endregion

        #region Description : 유동성관리 스프레드 전표 출력 이벤트
        private void FPS91_TY_S_AC_87BAJ355_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "20")
            {
                if (this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_87BAJ355.GetValue("LOLIJPNO").ToString().Substring(14, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        SectionReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        SectionReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }
            }
        }
        #endregion        

        #endregion
    }
}