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
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    public partial class TYACLO001I : TYBase
    {
        private string fsLOCCONTYEAR = string.Empty;
        private string fsLOCCONTNUM  = string.Empty;

        private string fsWK_GUBUN1   = string.Empty;
        private string fsWK_GUBUN2   = string.Empty;
        private string fsTAB_GUBUN   = string.Empty;

        private string fsLOCCONTSEQ  = string.Empty;
        private string fsLOCCHANGEGB = string.Empty;


        private string fsLOLRCONTSEQ   = string.Empty;
        private string fsLOLRLICONTSEQ = string.Empty;
        private string fsLOLRAMT       = string.Empty;

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

        #region Description : 페이지 로드 
        public TYACLO001I()
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

        private void TYACLO001I_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateMaster = new ToolStripMenuItem("차입금 계약관리 바로가기");
            reateMaster.Click += new EventHandler(reateMaster_ToolStripMenuItem_Click);

            ToolStripMenuItem reateDetail = new ToolStripMenuItem("차입금 계약이력관리 바로가기");
            reateDetail.Click += new EventHandler(reateDetail_ToolStripMenuItem_Click);

            this.FPS91_TY_S_AC_86FEP214.CurrentContextMenu.Items.AddRange(
            new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateMaster });

            this.FPS91_TY_S_AC_86FEP214.CurrentContextMenu.Items.AddRange(
            new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateDetail });

            // 계약관리 마스터
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);


            // 계약 이력관리
            (this.FPS91_TY_S_AC_86IHA240.Sheets[0].Columns[22].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_86IHA240, "BTN");

            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);

            this.BTN63_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN63_JPNO_CRE_ProcessCheck);
            this.BTN63_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN63_JUNPYO_CANCEL_ProcessCheck);

            UP_LOCONTMF_DETAIL_ReadOnly(false, "LOAD");

            this.TXT01_STDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy"));
            this.TXT01_EDDATE.SetValue(DateTime.Now.ToString("yyyy"));

            fsTAB_GUBUN = "MAIN";
            this.BTN61_INQ_Click(null, null);


            UP_ControlFactory();

            SetStartingFocus(this.TXT01_STDATE);
        }
        #endregion

        #region Description : 계약관리 MAIN



        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (fsTAB_GUBUN == "MAIN")
            {
                // 계약관리 메인 최종 조회
                UP_LOCONTMF_MAIN_INQ();
            }
            else if (fsTAB_GUBUN == "LOCONTMF_MASTER")
            {
                // 계약 마스터 조회
                UP_LOCONTMF_MASTER_INQ();
            }
            else if (fsTAB_GUBUN == "LOCONTMF_DETAIL")
            {
                // 계약 이력 조회
                UP_LOCONTMF_DETAIL_INQ();
            }
        }
        #endregion

        #region Description : 계약관리 메인 최종 조회
        private void UP_LOCONTMF_MAIN_INQ()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86FEN213",
                this.TXT01_STDATE.GetValue().ToString(),
                this.TXT01_EDDATE.GetValue().ToString(),
                this.CBH01_SBANK.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86FEP214.SetValue(dt);
        }
        #endregion

        #region  Description : 계약관리 바로가기 이벤트
        private void reateMaster_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fsTAB_GUBUN = "LOCONTMF_MASTER";

            this.tabControl1.SelectedIndex = 1;

            this.TXT01_LOCCONTYEAR.SetReadOnly(true);

            fsWK_GUBUN1 = "TAB";
            UP_LOCONTMF_MASTER_BTN_DISPLAY(fsWK_GUBUN1);

            this.TXT01_LOCCONTYEAR.SetValue(this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(0, 4));
            this.TXT01_LOCCONTSEQ.SetValue(this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(5, 2));

            fsLOCCONTYEAR = this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(0, 4);
            fsLOCCONTNUM  = this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(5, 2);

            // 계약 마스터 조회
            UP_LOCONTMF_MASTER_SEARCH();

            // 계약 마스터 확인
            UP_LOCONTMF_MASTER_RUN();
        }
        #endregion

        #region  Description : 계약이력관리 바로가기 이벤트
        private void reateDetail_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fsTAB_GUBUN = "LOCONTMF_DETAIL";

            this.tabControl1.SelectedIndex = 2;


            fsWK_GUBUN2 = "TAB";
            UP_LOCONTMF_MASTER_BTN_DISPLAY(fsWK_GUBUN2);

            this.TXT02_LOCCONTYEAR.SetValue(this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(0, 4));
            this.TXT02_LOCCONTSEQ.SetValue(this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(5, 2));

            fsLOCCONTYEAR = this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(0, 4);
            fsLOCCONTNUM  = this.FPS91_TY_S_AC_86FEP214.GetValue("LOCCONTNO").ToString().Substring(5, 2);

            // 계약 이력관리 조회
            UP_LOCONTMF_DETAIL_SEARCH();

            //// 계약 이력관리 확인
            //UP_LOCONTMF_DETAIL_RUN();

            // 계약 이력관리 확인
            UP_LOCONTMF_DETAIL_FINAL_RUN();
        }
        #endregion



        #endregion

        #region Description : 계약관리 마스터



        #region Description : 계약관리 마스터 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.TXT01_LOCCONTYEAR.SetReadOnly(false);

            UP_LOCONTMF_MASTER_FieldClear();

            fsWK_GUBUN1 = "NEW";
            UP_LOCONTMF_MASTER_BTN_DISPLAY(fsWK_GUBUN1);

            this.SetFocus(this.TXT01_LOCCONTYEAR);
        }
        #endregion

        #region Description : 계약관리 마스터 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN1.ToString() == "NEW")
            {
                UP_LOCONTMF_MASTER_SAV();

                this.TXT01_LOCCONTYEAR.SetReadOnly(true);
            }
            else if (fsWK_GUBUN1.ToString() == "UPT")
            {
                UP_LOCONTMF_MASTER_UPT();
            }

            // 계약 마스터 조회
            UP_LOCONTMF_MASTER_SEARCH();

            UP_LOCONTMF_MASTER_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 계약관리 마스터 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_LOCONTMF_DEL();

            // 계약 마스터 조회(단일건)
            UP_LOCONTMF_MASTER_SEARCH();

            UP_LOCONTMF_MASTER_FieldClear();

            UP_LOCONTMF_MASTER_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 계약관리 마스터 조회(여러건)
        private void UP_LOCONTMF_MASTER_INQ()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IFA233",
                this.TXT01_STDATE.GetValue().ToString(),
                this.TXT01_EDDATE.GetValue().ToString(),
                this.CBH01_SBANK.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86ID3224.SetValue(dt);
        }
        #endregion

        #region Description : 계약관리 마스터 조회(단일건)
        private void UP_LOCONTMF_MASTER_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86ID1221",
                this.TXT01_LOCCONTYEAR.GetValue().ToString(),
                this.TXT01_LOCCONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86ID3224.SetValue(dt);

            if (dt.Rows.Count <= 0)
            {
                // 계약관리 마스터 조회(여러건)
                UP_LOCONTMF_MASTER_INQ();
            }
        }
        #endregion

        #region Description : 계약관리 마스터 확인
        private void UP_LOCONTMF_MASTER_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IES232",
                this.TXT01_LOCCONTYEAR.GetValue().ToString(),
                this.TXT01_LOCCONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsWK_GUBUN1 = "UPT";
                UP_LOCONTMF_MASTER_BTN_DISPLAY(fsWK_GUBUN1);

                Timer tmr = new Timer();
                tmr.Tick += new EventHandler(tmr_Tick2);
                tmr.Interval = 100;
                tmr.Start();
            }

            //// 값 저장
            //UP_SET_Cookie1(sVSIPHANG.ToString(), sVSBONSUN.ToString());
        }
        #endregion

        #region Description : 계약관리 마스터 저장
        private void UP_LOCONTMF_MASTER_SAV()
        {
            string sLOCCONTNO = string.Empty;


            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IDL226",
                this.TXT01_LOCCONTYEAR.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_LOCCONTSEQ.SetValue(dt.Rows[0]["SEQ"].ToString());
            }

            sLOCCONTNO = Set_Fill4(this.TXT01_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOCCONTSEQ.GetValue().ToString());

            // 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86IDG225",
                                    sLOCCONTNO.ToString(),
                                    this.CBH01_LOCBANKCD.GetValue().ToString(),
                                    this.CBH01_LOCGRBK.GetValue().ToString(),
                                    this.CBO01_LOCCURRTYPE.GetValue().ToString(),
                                    this.CBO01_LOCGIGANTYPE.GetValue().ToString(),
                                    this.CBO01_LOCLOAN.GetValue().ToString(),
                                    this.CBO01_LOCLOANTYPE.GetValue().ToString(),
                                    this.CBH01_LOCCDAC.GetValue().ToString(),
                                    this.DTP01_LOCCONTDATE.GetValue().ToString(),
                                    this.DTP01_LOCENDDATE.GetValue().ToString(),
                                    this.TXT01_LOCCONTRATE.GetValue().ToString(),
                                    this.CBO01_LOCCOLLGB.GetValue().ToString(),
                                    this.CBH01_LOCCURRCD.GetValue().ToString(),
                                    this.TXT01_LOCCONTYUL.GetValue().ToString(),
                                    this.TXT01_LOCCONTAMT.GetValue().ToString(),
                                    this.TXT01_LOCCONTDAL.GetValue().ToString(),
                                    this.TXT01_LOCCREDITAMT.GetValue().ToString(),
                                    this.CBO01_LOCRATEGB.GetValue().ToString(),
                                    this.TXT01_LOCRATEDAY.GetValue().ToString(),
                                    this.CBH01_LOCDESCGB.GetValue().ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 계약관리 마스터 수정
        private void UP_LOCONTMF_MASTER_UPT()
        {
            string sLOCCONTNO = string.Empty;

            sLOCCONTNO = Set_Fill4(this.TXT01_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOCCONTSEQ.GetValue().ToString());

            // 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86IDV228",
                                    this.CBH01_LOCBANKCD.GetValue().ToString(),
                                    this.CBH01_LOCGRBK.GetValue().ToString(),
                                    this.CBO01_LOCCURRTYPE.GetValue().ToString(),
                                    this.CBO01_LOCGIGANTYPE.GetValue().ToString(),
                                    this.CBO01_LOCLOAN.GetValue().ToString(),
                                    this.CBO01_LOCLOANTYPE.GetValue().ToString(),
                                    this.CBH01_LOCCDAC.GetValue().ToString(),
                                    this.DTP01_LOCCONTDATE.GetValue().ToString(),
                                    this.DTP01_LOCENDDATE.GetValue().ToString(),
                                    this.TXT01_LOCCONTRATE.GetValue().ToString(),
                                    this.CBO01_LOCCOLLGB.GetValue().ToString(),
                                    this.CBH01_LOCCURRCD.GetValue().ToString(),
                                    this.TXT01_LOCCONTYUL.GetValue().ToString(),
                                    this.TXT01_LOCCONTAMT.GetValue().ToString(),
                                    this.TXT01_LOCCONTDAL.GetValue().ToString(),
                                    this.TXT01_LOCCREDITAMT.GetValue().ToString(),
                                    this.CBO01_LOCRATEGB.GetValue().ToString(),
                                    this.TXT01_LOCRATEDAY.GetValue().ToString(),
                                    this.CBH01_LOCDESCGB.GetValue().ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    sLOCCONTNO.ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 계약관리 마스터 삭제
        private void UP_LOCONTMF_DEL()
        {
            string sLOCCONTNO = string.Empty;

            sLOCCONTNO = Set_Fill4(this.TXT01_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT01_LOCCONTSEQ.GetValue().ToString());

            // 삭제 프로시저
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86IFG236", sLOCCONTNO.ToString());

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 계약관리 마스터 스프레드 이벤트
        private void FPS91_TY_S_AC_86ID3224_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_LOCCONTYEAR.SetValue(this.FPS91_TY_S_AC_86ID3224.GetValue("LOCCONTNO").ToString().Substring(0, 4));
            this.TXT01_LOCCONTSEQ.SetValue(this.FPS91_TY_S_AC_86ID3224.GetValue("LOCCONTNO").ToString().Substring(5, 2));

            // 계약 마스터 확인
            UP_LOCONTMF_MASTER_RUN();
        }
        #endregion

        #region Description : 계약관리 확인시 포커스
        void tmr_Tick2(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            this.SetFocus(this.CBH01_LOCGRBK.CodeText);
        }
        #endregion

        #region Description : 계약관리 마스터 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 통화유형 외화일 경우 외화금액 입력은 필수 사항
            if(this.CBO01_LOCCURRTYPE.GetValue().ToString() == "2")
            {
                if(double.Parse(Get_Numeric(this.TXT01_LOCCONTDAL.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_3CK2B843");
                    this.TXT01_LOCCONTDAL.Focus();

                    e.Successed = false;
                    return;
                }
            }

            
            DataTable dt = new DataTable();

            // 전표계정 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_88NBH624",
                this.CBH01_LOCCDAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_24RBZ877");
                this.CBH01_LOCCDAC.Focus();

                e.Successed = false;
                return;
            }
            
            if (this.CBH01_LOCBANKCD.GetValue().ToString().Length != 3)
            {
                this.ShowMessage("TY_M_AC_86IDY230");
                this.CBH01_LOCBANKCD.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                if (this.CBH01_LOCBANKCD.GetValue().ToString() != this.CBH01_LOCGRBK.GetValue().ToString().Substring(0, 3))
                {
                    this.ShowMessage("TY_M_AC_86IDZ231");
                    this.CBH01_LOCBANKCD.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (int.Parse(this.DTP01_LOCCONTDATE.GetValue().ToString()) > int.Parse(this.DTP01_LOCENDDATE.GetValue().ToString()))
            {
                this.ShowMessage("TY_M_AC_86IDR227");
                this.DTP01_LOCENDDATE.Focus();

                e.Successed = false;
                return;
            }

            // 담보가 있을 경우 채권 최고액을 입력 함
            if (this.CBO01_LOCCOLLGB.GetValue().ToString() == "Y")
            {
                if (Get_Numeric(this.TXT01_LOCCREDITAMT.GetValue().ToString()) == "0")
                {
                    this.ShowMessage("TY_M_AC_888F2524");
                    this.TXT01_LOCCREDITAMT.Focus();

                    e.Successed = false;
                    return;
                }

                // 약정금액은 채권최고액을 넘을 수 없음
                if (double.Parse(Get_Numeric(this.TXT01_LOCCONTAMT.GetValue().ToString())) > double.Parse(Get_Numeric(this.TXT01_LOCCREDITAMT.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_AC_88NDM632");
                    this.TXT01_LOCCONTAMT.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_LOCCURRTYPE.GetValue().ToString() == "1") // 원화
            {
                this.CBH01_LOCCURRCD.SetValue("");
            }
            else // 외화
            {
                if (this.CBH01_LOCCURRCD.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_86KG5249");
                    this.SetFocus(this.CBH01_LOCCURRCD.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            // 기간 유형 = 단기일 경우
            //             약정 계정과목은 211004, 211005 계정과목이 오고
            // 기간 유형 = 장기일 경우
            //             약정 계정과목은 221002, 221003 계정과목이 옴
            if (this.CBO01_LOCGIGANTYPE.GetValue().ToString() == "1") // 단기
            {
                if (this.CBH01_LOCCDAC.GetValue().ToString().Substring(0, 6) != "211004" && this.CBH01_LOCCDAC.GetValue().ToString().Substring(0, 6) != "211005")
                {
                    this.ShowMessage("TY_M_AC_883GA505");
                    this.SetFocus(this.CBH01_LOCCDAC.CodeText);

                    e.Successed = false;
                    return;
                }
            }
            else // 장기
            {
                if (this.CBH01_LOCCDAC.GetValue().ToString().Substring(0, 6) != "221002" && this.CBH01_LOCCDAC.GetValue().ToString().Substring(0, 6) != "221003")
                {
                    this.ShowMessage("TY_M_AC_883GA505");
                    this.SetFocus(this.CBH01_LOCCDAC.CodeText);

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
                // 저장하시겠습니까?
                if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 계약관리 마스터 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 실행 DATA 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IFD234",
                this.TXT01_LOCCONTYEAR.GetValue().ToString(),
                this.TXT01_LOCCONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_86IFE235");
                this.SetFocus(this.CBH01_LOCGRBK.CodeText);

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

        #region Description : 계약관리 마스터 필드 클리어
        private void UP_LOCONTMF_MASTER_FieldClear()
        {
            this.TXT01_LOCCONTYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            this.TXT01_LOCCONTSEQ.SetValue("");

            this.CBH01_LOCBANKCD.SetValue("");
            this.CBH01_LOCGRBK.SetValue("");
            this.CBO01_LOCCURRTYPE.SetValue("");
            this.CBO01_LOCGIGANTYPE.SetValue("");
            this.CBO01_LOCGIGANTYPE.SetValue("");
            this.CBO02_LOCLOAN.SetValue("");
            this.CBO01_LOCLOANTYPE.SetValue("");
            this.CBH01_LOCCDAC.SetValue("");
            this.TXT01_LOCCONTRATE.SetValue("");
            this.DTP01_LOCCONTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_LOCENDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBO01_LOCCOLLGB.SetValue("");
            this.CBH01_LOCCURRCD.SetValue("");
            this.TXT01_LOCCONTYUL.SetValue("");
            this.CBO01_LOCRATEGB.SetValue("");
            this.TXT01_LOCCONTAMT.SetValue("");
            this.TXT01_LOCCREDITAMT.SetValue("");
            this.TXT01_LOCRATEDAY.SetValue("");
            this.CBH01_LOCDESCGB.SetValue("");
        }
        #endregion

        #region Description : 계약관리 마스터 디스플레이
        private void UP_LOCONTMF_MASTER_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible  = true;
                this.BTN61_REM.Visible  = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible  = true;
                this.BTN61_REM.Visible  = true;
            }
            else
            {
                this.BTN61_SAV.Visible  = false;
                this.BTN61_REM.Visible  = false;
            }
        }
        #endregion

        #region Description : 계약관리 마스터 상환방법 텍스트박스 이벤트
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



        #endregion











        #region Description : 계약 이력관리



        #region Description : 신규 버튼
        private void BTN63_NEW_Click(object sender, EventArgs e)
        {
            UP_LOCONTMF_DETAIL_FieldClear();

            //UP_LOCONTMF_DETAIL_FieldCopy();

            fsWK_GUBUN2 = "NEW";
            UP_LOCONTMF_DETAIL_BTN_DISPLAY(fsWK_GUBUN2);

            this.SetFocus(this.CBO03_LOCCHANGEGB);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN2.ToString() == "NEW")
            {
                UP_LOCONTMF_DETAIL_SAV();
            }
            else if (fsWK_GUBUN2.ToString() == "UPT")
            {
                UP_LOCONTMF_DETAIL_UPT();
            }

            // 계약관리 마스터 조회(여러건)
            UP_LOCONTMF_DETAIL_INQ();

            this.SetFocus(this.CBO03_LOCCHANGEGB);

            fsWK_GUBUN2 = "TAB";
            UP_LOCONTMF_DETAIL_BTN_DISPLAY(fsWK_GUBUN2);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            UP_LOCONTMF_DETAIL_DEL();

            // 계약관리 마스터 조회(여러건)
            UP_LOCONTMF_DETAIL_INQ();

            this.SetFocus(this.CBO03_LOCCHANGEGB);

            fsWK_GUBUN2 = "TAB";
            UP_LOCONTMF_DETAIL_BTN_DISPLAY(fsWK_GUBUN2);
        }
        #endregion

        
        
        
        #region Description : 계약 이력관리 전표생성 버튼
        private void BTN63_JPNO_CRE_Click(object sender, EventArgs e)
        {
            string sB2DPMK     = string.Empty;
            string sB2SSID     = string.Empty;

            string sLOLRCONTNO  = string.Empty;
            string sLOLRCONTSEQ = string.Empty;
            string sLOLRNUM     = string.Empty;

            string sLOLRAMT     = string.Empty;
            string sLOLRCDAC    = string.Empty;
            string sLOLRDPAC    = string.Empty;
            string sLOLRRKAC    = string.Empty;
            string sLOLRDOLLAR  = string.Empty;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();

            // 유동성 재대체 계약번호
            sLOLRCONTNO = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString());
            // 유동성 재대체 계약순번
            sLOLRCONTSEQ = fsLOLRCONTSEQ.ToString();
            // 유동성 재대체 순번
            sLOLRNUM = this.TXT03_LOCLRQNUM3.GetValue().ToString();

            // 차입금 유동성 재대체 자료 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87JHR437", sLOLRCONTNO.ToString(),
                                                        sLOLRCONTSEQ.ToString(),
                                                        sLOLRNUM.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sLOLRAMT    = dt.Rows[0]["LOLRAMT"].ToString();
                sLOLRCDAC   = dt.Rows[0]["LOLRCDAC"].ToString();
                sLOLRDPAC   = dt.Rows[0]["LOLRDPAC"].ToString();
                sLOLRRKAC   = dt.Rows[0]["LOLRRKAC"].ToString();
                sLOLRDOLLAR = dt.Rows[0]["LOLRDOLLAR"].ToString();
            }

            dt.Clear();

            // 부서코드 가져오기
            sB2DPMK = UP_GET_INKIBNMF("BUSEO");

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
            this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP03_LOCACDATE.GetValue().ToString()));
            this.DAT02_W2NOSQ.SetValue("0");
            this.DAT02_W2NOLN.SetValue("1");
            this.DAT02_W2IDJP.SetValue("3");
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(sLOLRCDAC.ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(sLOLRDPAC.ToString());

            //관리항목 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", sLOLRCDAC.ToString(), "");

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
            this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목2
            this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목3
            this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목4
            this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목5
            this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목6
            this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), sLOLRDOLLAR.ToString()));

            this.DAT02_W2AMDR.SetValue(Get_Numeric(sLOLRAMT.ToString()));
            this.DAT02_W2AMCR.SetValue("0");

            string sLOLRWNJPNO = string.Empty;
            string sW2CDAC = string.Empty;
            string sW2NOLN     = string.Empty;

            // 유동성 대체전표 번호 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IHQ241",
                this.TXT02_LOCCONTYEAR.GetValue().ToString(),
                this.TXT02_LOCCONTSEQ.GetValue().ToString(),
                fsLOCCONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sLOLRWNJPNO = dt.Rows[0]["LOLRWNJPNO"].ToString();
            }
            dt.Clear();

            // 유동성 대체시 계정에 대한 대변계정(상대계정)을 가져와야 함
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_B5HBA340", sLOLRWNJPNO.ToString().Substring(0, 6),
                                                        sLOLRWNJPNO.ToString().Substring(6, 8),
                                                        sLOLRWNJPNO.ToString().Substring(14, 3),
                                                        sLOLRCDAC.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sW2NOLN = Set_Fill2(dt.Rows[0]["B2NOLN"].ToString());
            }
            dt.Clear();

            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(sLOLRRKAC.ToString());
            this.DAT02_W2RKCU.SetValue("");
            this.DAT02_W2WCJP.SetValue(sLOLRWNJPNO.ToString() + sW2NOLN.ToString());
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
            this.DAT02_W2DTMK.SetValue(Get_Date(this.DTP03_LOCACDATE.GetValue().ToString()));
            this.DAT02_W2NOSQ.SetValue("0");
            this.DAT02_W2NOLN.SetValue("2");
            this.DAT02_W2IDJP.SetValue("3");
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(this.CBH03_LOCCDAC.GetValue().ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(sLOLRDPAC.ToString());

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
            this.DAT02_W2VLMI1.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI1.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목2
            this.DAT02_W2VLMI2.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI2.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목3
            this.DAT02_W2VLMI3.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI3.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목4
            this.DAT02_W2VLMI4.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI4.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목5
            this.DAT02_W2VLMI5.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI5.GetValue().ToString(), sLOLRDOLLAR.ToString()));
            // 관리항목6
            this.DAT02_W2VLMI6.SetValue(UP_GET_B2VLMI(this.DAT02_W2CDMI6.GetValue().ToString(), sLOLRDOLLAR.ToString()));

            this.DAT02_W2AMDR.SetValue("0");
            this.DAT02_W2AMCR.SetValue(Get_Numeric(sLOLRAMT.ToString()));

            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(sLOLRRKAC.ToString());
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
                                                        Get_Date(this.DTP03_LOCACDATE.GetValue().ToString()),
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
                            this.TXT03_LOLRJPNO.SetValue(sJpno.ToString().Replace("-", "").ToString());

                            // 유동성 재대체 전표번호 업데이트
                            UP_ACLOLIQUIDMF_UPT_JPNO(sLOLRCONTNO, sLOLRCONTSEQ, sLOLRNUM);

                            // 계약 이력관리 전표관련 버튼 DISPLAY
                            UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(false, false, false, true);

                            // 계약 이력관리 필드 ReadOnly
                            UP_LOCONTMF_DETAIL_ReadOnly(true, "OK");

                            // 계약 이력관리 조회
                            UP_LOCONTMF_DETAIL_INQ();

                            this.ShowMessage("TY_M_AC_25O8K620");
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 계약 이력관리 전표취소 버튼
        private void BTN63_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";
            string sLOLRCONTNO  = string.Empty;
            string sLOLRCONTSEQ = string.Empty;
            string sLOLRNUM     = string.Empty;

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //미승인전표 -> 임시파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT03_LOLRJPNO.GetValue().ToString().Substring(0, 6),
                                                                 this.TXT03_LOLRJPNO.GetValue().ToString().Substring(6, 8),
                                                                 this.TXT03_LOLRJPNO.GetValue().ToString().Substring(14, 3)
                                                                 );
            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID,
                                                        this.ProgramNo, Employer.EmpNo,
                                                        "D",
                                                        this.TXT03_LOLRJPNO.GetValue().ToString().Substring(0, 6),
                                                        this.TXT03_LOLRJPNO.GetValue().ToString().Substring(6, 8),
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
                this.TXT03_LOLRJPNO.SetValue("");

                // 유동성 재대체 계약번호
                sLOLRCONTNO = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString());
                // 유동성 재대체 계약순번
                sLOLRCONTSEQ = fsLOLRCONTSEQ.ToString();
                // 유동성 재대체 순번
                sLOLRNUM = this.TXT03_LOCLRQNUM3.GetValue().ToString();


                // 유동성 재대체 전표번호 업데이트
                UP_ACLOLIQUIDMF_UPT_JPNO(sLOLRCONTNO, sLOLRCONTSEQ, sLOLRNUM);

                // 계약 이력관리 전표관련 버튼 DISPLAY
                UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(true, true, true, false);

                // 계약 이력관리 필드 ReadOnly
                UP_LOCONTMF_DETAIL_ReadOnly(false, "CANCEL");

                // 계약 이력관리 조회
                UP_LOCONTMF_DETAIL_INQ();

                this.ShowMessage("TY_M_AC_25O8K620");
            }
        }
        #endregion

        #region Description : 계약 이력관리 전표출력 버튼
        private void BTN63_PRT_Click(object sender, EventArgs e)
        {
            if (this.TXT03_LOLRJPNO.GetValue().ToString() != "")
            {
                string sJPNO = this.TXT03_LOLRJPNO.GetValue().ToString();

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




        #region Description : 계약 이력관리 조회(단일건)
        private void UP_LOCONTMF_DETAIL_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IH1238",
                this.TXT02_LOCCONTYEAR.GetValue().ToString(),
                this.TXT02_LOCCONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86IHA240.SetValue(dt);

            if (dt.Rows.Count <= 0)
            {
                // 계약관리 마스터 조회(여러건)
                UP_LOCONTMF_DETAIL_INQ();
            }
        }
        #endregion

        #region Description : 계약 이력관리 조회(여러건)
        private void UP_LOCONTMF_DETAIL_INQ()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IH4239",
                this.TXT01_STDATE.GetValue().ToString(),
                this.TXT01_EDDATE.GetValue().ToString(),
                this.CBH01_SBANK.GetValue().ToString()                
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_86IHA240.SetValue(dt);
        }
        #endregion

        #region Description : 계약 이력관리 확인
        private void UP_LOCONTMF_DETAIL_RUN()
        {
            fsLOCCHANGEGB = "";
            fsLOLRLICONTSEQ = ""; // 유동성 계약순번
            fsLOLRAMT     = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IHQ241",
                this.TXT02_LOCCONTYEAR.GetValue().ToString(),
                this.TXT02_LOCCONTSEQ.GetValue().ToString(),
                fsLOCCONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "03");

                fsWK_GUBUN2 = "UPT";
                UP_LOCONTMF_DETAIL_BTN_DISPLAY(fsWK_GUBUN2);

                fsLOLRLICONTSEQ = dt.Rows[0]["LOLRLICONTSEQ"].ToString();
                fsLOCCHANGEGB   = dt.Rows[0]["LOCCHANGEGB"].ToString();

                fsLOLRCONTSEQ   = dt.Rows[0]["LOLRCONTSEQ"].ToString();

                this.SetFocus(this.CBH03_LOCBANKCD.CodeText);

                // 계약 이력관리 최종자료 가져오기
                UP_LOCONTMF_DETAIL_FINAL_RUN();

                if (dt.Rows[0]["LOLRJPNO"].ToString() == "")
                {
                    // 계약 이력관리 전표관련 버튼 DISPLAY
                    UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(true, true, true, false);

                    // 계약 이력관리 필드 ReadOnly
                    UP_LOCONTMF_DETAIL_ReadOnly(false, "CANCEL");
                }
                else
                {
                    // 계약 이력관리 전표관련 버튼 DISPLAY
                    UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(false, false, false, true);

                    // 계약 이력관리 필드 ReadOnly
                    UP_LOCONTMF_DETAIL_ReadOnly(true, "OK");
                }
            }
        }
        #endregion

        #region Description : 계약 이력관리 최종자료 가져오기
        private void UP_LOCONTMF_DETAIL_FINAL_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IH6237",
                this.TXT02_LOCCONTYEAR.GetValue().ToString(),
                this.TXT02_LOCCONTSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");
            }
        }
        #endregion

        #region Description : 계약 이력관리 저장
        private void UP_LOCONTMF_DETAIL_SAV()
        {
            string sLOCCONTNO   = string.Empty;
            string sLOCCONTSEQ  = string.Empty;
            string sLOCLIQNUM   = string.Empty;
            string sLOCLRQNUM   = string.Empty;

            string sLOLRCONTNO  = string.Empty;
            string sLOLRCONTSEQ = string.Empty;
            string sLOLRNUM     = string.Empty;

            string sLOLRLICONTSEQ = string.Empty;

            sLOCCONTNO = Set_Fill4(this.TXT02_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOCCONTSEQ.GetValue().ToString());

            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                sLOCLIQNUM     = Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString()) + Set_Fill3(fsLOLRLICONTSEQ.ToString()) + Set_Fill3(this.TXT03_LOCLIQNUM3.GetValue().ToString());
                sLOLRLICONTSEQ = fsLOLRLICONTSEQ.ToString();
            }
            else
            {
                sLOCLIQNUM = "";
            }

            // 만기 연장시 유동성 재대체 번호 입력 함
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                this.TXT03_LOCLRQNUM1.SetValue(this.TXT03_LOCLIQNUM1.GetValue().ToString());
                this.TXT03_LOCLRQNUM2.SetValue(this.TXT03_LOCLIQNUM2.GetValue().ToString());

                // 유동성 재대체 계약번호
                sLOLRCONTNO = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString());

                DataTable dt = new DataTable();

                // 2021.03.12 추가 소스
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_B3CAP923",
                    sLOCCONTNO.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sLOCCONTSEQ     = dt.Rows[0]["LOCCONTSEQ"].ToString();
                    fsLOLRLICONTSEQ = dt.Rows[0]["LOCCONTSEQ"].ToString();
                }                

                // 유동성 재대체 계약순번
                sLOLRCONTSEQ = fsLOLRLICONTSEQ.ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_87IFQ424",
                    sLOLRCONTNO.ToString(),
                    sLOLRCONTSEQ.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 유동성 재대체 순번
                    this.TXT03_LOCLRQNUM3.SetValue(Set_Fill3(dt.Rows[0]["SEQ"].ToString()));
                    sLOLRNUM = Set_Fill3(dt.Rows[0]["SEQ"].ToString());
                }

                // 유동성 재대체 번호
                sLOCLRQNUM = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString()) + Set_Fill3(fsLOLRLICONTSEQ.ToString()) + Set_Fill3(this.TXT03_LOCLRQNUM3.GetValue().ToString());
            }

            // 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_B3CAQ924",
                                    sLOCCONTNO.ToString(),
                                    sLOCCONTSEQ.ToString(),
                                    this.CBH03_LOCBANKCD.GetValue().ToString(),
                                    this.CBH03_LOCGRBK.GetValue().ToString(),
                                    this.CBO03_LOCCURRTYPE.GetValue().ToString(),
                                    this.CBO03_LOCGIGANTYPE.GetValue().ToString(),
                                    this.CBO03_LOCLOAN.GetValue().ToString(),
                                    this.CBO03_LOCLOANTYPE.GetValue().ToString(),
                                    this.CBH03_LOCCDAC.GetValue().ToString(),
                                    this.DTP03_LOCCONTDATE.GetValue().ToString(),
                                    this.DTP03_LOCENDDATE.GetValue().ToString(),
                                    this.TXT03_LOCCONTRATE.GetValue().ToString(),
                                    this.CBO03_LOCCOLLGB.GetValue().ToString(),
                                    this.CBH03_LOCCURRCD.GetValue().ToString(),
                                    this.TXT03_LOCCONTYUL.GetValue().ToString(),
                                    this.TXT03_LOCCONTAMT.GetValue().ToString(),
                                    this.TXT03_LOCCONTDAL.GetValue().ToString(),
                                    this.TXT03_LOCCREDITAMT.GetValue().ToString(),
                                    this.CBO03_LOCRATEGB.GetValue().ToString(),
                                    this.TXT03_LOCRATEDAY.GetValue().ToString(),
                                    this.CBH03_LOCDESCGB.GetValue().ToString(),
                                    this.CBO03_LOCCHANGEGB.GetValue().ToString(),
                                    this.DTP03_LOCACDATE.GetValue().ToString(),
                                    sLOCLIQNUM.ToString(),
                                    sLOCLRQNUM.ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                                    );

            this.DbConnector.ExecuteNonQuery();

            // 만기 연장시 재대체에 등록함
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                UP_ACLORELIQUIDMF_SAV(sLOLRCONTNO.ToString(), sLOLRCONTSEQ.ToString(), sLOLRNUM.ToString(), sLOLRLICONTSEQ.ToString());
            }

            this.ShowMessage("TY_M_GB_23NAD873");
        }                                                                                                                             
        #endregion

        #region Description : 계약 이력관리 수정
        private void UP_LOCONTMF_DETAIL_UPT()
        {
            string sLOCCONTNO   = string.Empty;
            string sLOCLIQNUM   = string.Empty;
            string sLOCLRQNUM   = string.Empty;

            string sLOLRCONTNO  = string.Empty;
            string sLOLRCONTSEQ = string.Empty;
            string sLOLRNUM     = string.Empty;

            string sEXISTS      = string.Empty;

            sLOCCONTNO = Set_Fill4(this.TXT02_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOCCONTSEQ.GetValue().ToString());

            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                sLOCLIQNUM = Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString()) + Set_Fill3(fsLOLRLICONTSEQ.ToString()) + Set_Fill3(this.TXT03_LOCLIQNUM3.GetValue().ToString());
            }
            else
            {
                sLOCLIQNUM = "";
            }

            DataTable dt = new DataTable();

            // 만기 연장시 재대체에 수정함
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                if (this.TXT03_LOCLRQNUM1.GetValue().ToString() == "" && this.TXT03_LOCLRQNUM2.GetValue().ToString() == "" && this.TXT03_LOCLRQNUM3.GetValue().ToString() == "")
                {
                    sEXISTS = "";

                    this.TXT03_LOCLRQNUM1.SetValue(this.TXT03_LOCLIQNUM1.GetValue().ToString());
                    this.TXT03_LOCLRQNUM2.SetValue(this.TXT03_LOCLIQNUM2.GetValue().ToString());

                    // 유동성 재대체 계약번호
                    sLOLRCONTNO = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString());
                    // 유동성 재대체 계약순번
                    sLOLRCONTSEQ = fsLOLRLICONTSEQ.ToString();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_87IFQ424",
                        sLOLRCONTNO.ToString(),
                        sLOLRCONTSEQ.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 유동성 재대체 순번
                        this.TXT03_LOCLRQNUM3.SetValue(Set_Fill3(dt.Rows[0]["SEQ"].ToString()));
                        sLOLRNUM = Set_Fill3(dt.Rows[0]["SEQ"].ToString());
                    }

                    // 유동성 재대체 번호
                    sLOCLRQNUM = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString()) + Set_Fill3(fsLOLRLICONTSEQ.ToString()) + Set_Fill3(this.TXT03_LOCLRQNUM3.GetValue().ToString());
                }
                else
                {
                    // 유동성 재대체 계약번호
                    sLOLRCONTNO  = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString());
                    // 유동성 재대체 계약순번
                    sLOLRCONTSEQ = fsLOLRCONTSEQ.ToString();
                    // 유동성 재대체 순번
                    sLOLRNUM     = this.TXT03_LOCLRQNUM3.GetValue().ToString();

                    // 유동성 재대체 번호
                    sLOCLRQNUM = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString()) + Set_Fill3(fsLOLRCONTSEQ.ToString()) + Set_Fill3(this.TXT03_LOCLRQNUM3.GetValue().ToString());

                    sEXISTS = "EXISTS";
                }
            }

            // 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86J95243",
                                    this.CBH03_LOCBANKCD.GetValue().ToString(),
                                    this.CBH03_LOCGRBK.GetValue().ToString(),
                                    this.CBO03_LOCCURRTYPE.GetValue().ToString(),
                                    this.CBO03_LOCGIGANTYPE.GetValue().ToString(),
                                    this.CBO03_LOCLOAN.GetValue().ToString(),
                                    this.CBO03_LOCLOANTYPE.GetValue().ToString(),
                                    this.CBH03_LOCCDAC.GetValue().ToString(),
                                    this.DTP03_LOCCONTDATE.GetValue().ToString(),
                                    this.DTP03_LOCENDDATE.GetValue().ToString(),
                                    this.TXT03_LOCCONTRATE.GetValue().ToString(),
                                    this.CBO03_LOCCOLLGB.GetValue().ToString(),
                                    this.CBH03_LOCCURRCD.GetValue().ToString(),
                                    this.TXT03_LOCCONTYUL.GetValue().ToString(),
                                    this.TXT03_LOCCONTAMT.GetValue().ToString(),
                                    this.TXT03_LOCCONTDAL.GetValue().ToString(),
                                    this.TXT03_LOCCREDITAMT.GetValue().ToString(),
                                    this.CBO03_LOCRATEGB.GetValue().ToString(),
                                    this.TXT03_LOCRATEDAY.GetValue().ToString(),
                                    this.CBH03_LOCDESCGB.GetValue().ToString(),
                                    this.CBO03_LOCCHANGEGB.GetValue().ToString(),
                                    this.DTP03_LOCACDATE.GetValue().ToString(),
                                    sLOCLIQNUM.ToString(),
                                    sLOCLRQNUM.ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    sLOCCONTNO.ToString(),
                                    fsLOCCONTSEQ.ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            // 만기 연장시 재대체에 수정함
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                if (sEXISTS == "") // 유동성 재대체 등록
                {
                    UP_ACLORELIQUIDMF_SAV(sLOLRCONTNO.ToString(), sLOLRCONTSEQ.ToString(), sLOLRNUM.ToString(), fsLOLRLICONTSEQ.ToString());
                }
                else // 유동성 재대체 수정
                {
                    UP_ACLORELIQUIDMF_UPT(sLOLRCONTNO.ToString(), sLOLRCONTSEQ.ToString(), sLOLRNUM.ToString());
                }
            }

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 계약 이력관리 삭제
        private void UP_LOCONTMF_DETAIL_DEL()
        {
            string sLOCCONTNO = string.Empty;

            sLOCCONTNO = Set_Fill4(this.TXT02_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOCCONTSEQ.GetValue().ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86J97244", sLOCCONTNO.ToString(),
                                                        fsLOCCONTSEQ.ToString()
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            // 만기 연장시 재대체에 수정함
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                if (this.TXT03_LOCLRQNUM1.GetValue().ToString() != "" && this.TXT03_LOCLRQNUM2.GetValue().ToString() != "" && this.TXT03_LOCLRQNUM3.GetValue().ToString() != "")
                {
                    string sLOLRCONTNO  = string.Empty;
                    string sLOLRCONTSEQ = string.Empty;
                    string sLOLRNUM     = string.Empty;

                    // 유동성 재대체 계약번호
                    sLOLRCONTNO  = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString());
                    // 유동성 재대체 계약순번
                    sLOLRCONTSEQ = fsLOLRCONTSEQ.ToString();
                    // 유동성 재대체 순번
                    sLOLRNUM     = this.TXT03_LOCLRQNUM3.GetValue().ToString();

                    // 유동성 재대체 삭제
                    UP_ACLORELIQUIDMF_DEL(sLOLRCONTNO.ToString(), sLOLRCONTSEQ.ToString(), sLOLRNUM.ToString());
                }
            }

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 계약 이력관리 - 유동성 재대체 등록
        private void UP_ACLORELIQUIDMF_SAV(string sLOLRCONTNO, string sLOLRCONTSEQ, string sLOLRNUM, string sLOLRLICONTSEQ)
        {
            string sLOLRDATE      = string.Empty;
            string sLOLRAMT       = string.Empty;
            string sLOLRYUL       = string.Empty;
            string sLOLRDOLLAR    = string.Empty;
            string sLOLRCDAC      = string.Empty;
            string sLOLRDPAC      = string.Empty;
            string sLOLRRKAC      = string.Empty;
            string sLOLRWNJPNO    = string.Empty;
            string sLOLRLICONTNO  = string.Empty;
            string sLOLRLINUM     = string.Empty;
            string sLOLRHISAB     = string.Empty;

            // 유동성 대체 금액이 유동성 재대체 금액이 됨
            DataTable dt = new DataTable();

            // 차입금 계약 이력관리 - 유동성 재대체 잔액 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87IDM421",
                this.TXT03_LOCLIQNUM1.GetValue().ToString(),
                this.TXT03_LOCLIQNUM2.GetValue().ToString(),
                sLOLRLICONTSEQ.ToString(),
                this.TXT03_LOCLIQNUM3.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 유동성 잔액
                sLOLRAMT = dt.Rows[0]["JANAMT"].ToString();
            }

            sLOLRDATE      = Get_Date(this.DTP03_LOCACDATE.GetValue().ToString());
            sLOLRYUL       = this.TXT03_LOCCONTYUL.GetValue().ToString();
            sLOLRCDAC      = "21101901";
            sLOLRDPAC      = UP_GET_INKIBNMF("DPAC");
            sLOLRRKAC      = "유동성 재대체 전표발행";


            // 유동성 대체 전표번호
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87BAS356",
                Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()),
                Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString()),
                sLOLRLICONTSEQ.ToString(),
                Set_Fill3(this.TXT03_LOCLIQNUM3.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 유동성 대체 외화금액
                sLOLRDOLLAR = dt.Rows[0]["LOLIDOLLAR"].ToString();
                // 유동성 대체 전표번호
                sLOLRWNJPNO = dt.Rows[0]["LOLIJPNO"].ToString();
            }

            // 유동성 계약번호
            sLOLRLICONTNO  = Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString());
            // 유동성 순번
            sLOLRLINUM     = Set_Fill3(this.TXT03_LOCLIQNUM3.GetValue().ToString());
            sLOLRHISAB     = TYUserInfo.EmpNo.ToString().Trim().ToUpper();

            // 유동성 재대체 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87IFY425",
                                    sLOLRCONTNO.ToString(),
                                    sLOLRCONTSEQ.ToString(),
                                    sLOLRNUM.ToString(),
                                    sLOLRDATE.ToString(),
                                    Get_Numeric(sLOLRAMT.ToString()),
                                    Get_Numeric(sLOLRYUL.ToString()),
                                    Get_Numeric(sLOLRDOLLAR.ToString()),
                                    sLOLRCDAC.ToString(),
                                    sLOLRDPAC.ToString(),
                                    sLOLRRKAC.ToString(),
                                    sLOLRWNJPNO.ToString(),
                                    sLOLRLICONTNO.ToString(),
                                    sLOLRLICONTSEQ.ToString(),
                                    sLOLRLINUM.ToString(),
                                    "R",                                // 재대체
                                    sLOLRHISAB.ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 계약 이력관리 - 유동성 재대체 수정
        private void UP_ACLORELIQUIDMF_UPT(string sLOLRCONTNO, string sLOLRCONTSEQ, string sLOLRNUM)
        {
            string sLOLRDATE      = string.Empty;
            string sLOLRAMT       = string.Empty;
            string sLOLRYUL       = string.Empty;
            string sLOLRDOLLAR    = string.Empty;
            string sLOLRCDAC      = string.Empty;
            string sLOLRDPAC      = string.Empty;
            string sLOLRRKAC      = string.Empty;
            string sLOLRWNJPNO    = string.Empty;
            string sLOLRLICONTNO  = string.Empty;
            string sLOLRLICONTSEQ = string.Empty;
            string sLOLRLINUM     = string.Empty;
            string sLOLRHISAB     = string.Empty;

            DataTable dt = new DataTable();

            // 차입금 계약 이력관리 - 유동성 재대체 잔액 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87IDM421",
                this.TXT03_LOCLIQNUM1.GetValue().ToString(),
                this.TXT03_LOCLIQNUM2.GetValue().ToString(),
                fsLOLRLICONTSEQ.ToString(),
                this.TXT03_LOCLIQNUM3.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 유동성 잔액
                sLOLRAMT = dt.Rows[0]["JANAMT"].ToString();
            }

            sLOLRDATE = Get_Date(this.DTP03_LOCACDATE.GetValue().ToString());
            sLOLRYUL  = this.TXT03_LOCCONTYUL.GetValue().ToString();
            sLOLRCDAC = "21101901";
            sLOLRDPAC = UP_GET_INKIBNMF("DPAC");
            sLOLRRKAC = "유동성 재대체 전표발행";


            // 유동성 대체 전표번호
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_87BAS356",
                Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()),
                Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString()),
                fsLOLRLICONTSEQ.ToString(),
                Set_Fill3(this.TXT03_LOCLIQNUM3.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 유동성 대체 외화금액
                sLOLRDOLLAR = dt.Rows[0]["LOLIDOLLAR"].ToString();
                // 유동성 대체 전표번호
                sLOLRWNJPNO = dt.Rows[0]["LOLIJPNO"].ToString();
            }

            // 유동성 계약번호
            sLOLRLICONTNO  = Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString());
            // 유동성 계약순번
            sLOLRLICONTSEQ = fsLOLRLICONTSEQ.ToString();
            // 유동성 순번
            sLOLRLINUM     = Set_Fill3(this.TXT03_LOCLIQNUM3.GetValue().ToString());
            sLOLRHISAB     = TYUserInfo.EmpNo.ToString().Trim().ToUpper();

            // 유동성 재대체 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87II9427",
                                    sLOLRDATE.ToString(),
                                    Get_Numeric(sLOLRAMT.ToString()),
                                    Get_Numeric(sLOLRYUL.ToString()),
                                    Get_Numeric(sLOLRDOLLAR.ToString()),
                                    sLOLRCDAC.ToString(),
                                    sLOLRDPAC.ToString(),
                                    sLOLRRKAC.ToString(),
                                    sLOLRWNJPNO.ToString(),
                                    sLOLRLICONTNO.ToString(),
                                    sLOLRLICONTSEQ.ToString(),
                                    sLOLRLINUM.ToString(),
                                    "R",                                // 재대체
                                    sLOLRHISAB.ToString(),
                                    sLOLRCONTNO.ToString(),
                                    sLOLRCONTSEQ.ToString(),
                                    sLOLRNUM.ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 계약 이력관리 - 유동성 재대체 삭제
        private void UP_ACLORELIQUIDMF_DEL(string sLOLRCONTNO, string sLOLRCONTSEQ, string sLOLRNUM)
        {
            // 유동성 재대체 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87II2428", sLOLRCONTNO.ToString(),
                                                        sLOLRCONTSEQ.ToString(),
                                                        sLOLRNUM.ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 유동성 재대체 전표번호 업데이트
        private void UP_ACLOLIQUIDMF_UPT_JPNO(string sLOLRCONTNO, string sLOLRCONTSEQ, string sLOLRNUM)
        {
            // 유동성 재대체 전표번호 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87K8S438", this.TXT03_LOLRJPNO.GetValue().ToString(),
                                                        sLOLRCONTNO.ToString(),
                                                        sLOLRCONTSEQ.ToString(),
                                                        sLOLRNUM.ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 계약 이력관리 필드 ReadOnly
        private void UP_LOCONTMF_DETAIL_ReadOnly(bool flag, string sGUBUN)
        {
            #region Description : 원본 계약관리

            this.TXT02_LOCCONTYEAR.SetReadOnly(true);
            this.TXT02_LOCCONTSEQ.SetReadOnly(true);

            this.CBH02_LOCBANKCD.SetReadOnly(true);
            this.CBH02_LOCGRBK.SetReadOnly(true);
            this.CBO02_LOCCURRTYPE.SetReadOnly(true);
            this.CBO02_LOCGIGANTYPE.SetReadOnly(true);
            this.CBO02_LOCGIGANTYPE.SetReadOnly(true);
            this.CBO02_LOCLOAN.SetReadOnly(true);
            this.CBO02_LOCLOANTYPE.SetReadOnly(true);
            this.CBH02_LOCCDAC.SetReadOnly(true);
            this.TXT02_LOCCONTRATE.SetReadOnly(true);
            this.DTP02_LOCCONTDATE.SetReadOnly(true);
            this.DTP02_LOCENDDATE.SetReadOnly(true);
            this.CBO02_LOCCOLLGB.SetReadOnly(true);
            this.CBH02_LOCCURRCD.SetReadOnly(true);
            this.TXT02_LOCCONTYUL.SetReadOnly(true);
            this.CBO02_LOCRATEGB.SetReadOnly(true);
            this.TXT02_LOCCONTAMT.SetReadOnly(true);
            this.TXT02_LOCCREDITAMT.SetReadOnly(true);
            this.TXT02_LOCRATEDAY.SetReadOnly(true);
            this.CBH02_LOCDESCGB.SetReadOnly(true);

            #endregion

            #region Description : 수정 할 계약관리

            this.CBH03_LOCBANKCD.SetReadOnly(flag);
            this.CBH03_LOCGRBK.SetReadOnly(flag);
            this.CBO03_LOCCURRTYPE.SetReadOnly(flag);
            this.CBO03_LOCGIGANTYPE.SetReadOnly(flag);
            this.CBO03_LOCGIGANTYPE.SetReadOnly(flag);
            this.CBO03_LOCLOAN.SetReadOnly(flag);
            this.CBO03_LOCLOANTYPE.SetReadOnly(flag);
            this.CBH03_LOCCDAC.SetReadOnly(flag);
            this.TXT03_LOCCONTRATE.SetReadOnly(flag);
            this.DTP03_LOCCONTDATE.SetReadOnly(flag);
            this.DTP03_LOCENDDATE.SetReadOnly(flag);
            this.CBO03_LOCCOLLGB.SetReadOnly(flag);
            this.CBH03_LOCCURRCD.SetReadOnly(flag);
            this.TXT03_LOCCONTYUL.SetReadOnly(flag);
            this.CBO03_LOCRATEGB.SetReadOnly(flag);
            this.TXT03_LOCCONTAMT.SetReadOnly(flag);
            this.TXT03_LOCCONTDAL.SetReadOnly(flag);
            this.TXT03_LOCCREDITAMT.SetReadOnly(flag);
            this.TXT03_LOCRATEDAY.SetReadOnly(flag);
            this.CBH03_LOCDESCGB.SetReadOnly(flag);
            this.DTP03_LOCACDATE.SetReadOnly(flag);

            #endregion
        }
        #endregion

        #region Description : 계약 이력관리 필드 클리어
        private void UP_LOCONTMF_DETAIL_FieldClear()
        {
            fsLOCCHANGEGB = "";

            this.TXT02_LOCCONTSEQ.SetValue("");

            this.CBH02_LOCBANKCD.SetValue("");
            this.CBH02_LOCGRBK.SetValue("");
            this.CBO02_LOCCURRTYPE.SetValue("");
            this.CBO02_LOCGIGANTYPE.SetValue("");
            this.CBO02_LOCGIGANTYPE.SetValue("");
            this.CBO02_LOCLOAN.SetValue("");
            this.CBO02_LOCLOANTYPE.SetValue("");
            this.CBH02_LOCCDAC.SetValue("");
            this.TXT02_LOCCONTRATE.SetValue("");
            this.DTP02_LOCCONTDATE.SetValue("");
            this.DTP02_LOCENDDATE.SetValue("");
            this.CBO02_LOCCOLLGB.SetValue("");
            this.CBH02_LOCCURRCD.SetValue("");
            this.TXT02_LOCCONTYUL.SetValue("");
            this.CBO02_LOCRATEGB.SetValue("");
            this.TXT02_LOCCONTAMT.SetValue("");
            this.TXT02_LOCCREDITAMT.SetValue("");
            this.TXT02_LOCRATEDAY.SetValue("");
            this.CBH02_LOCDESCGB.SetValue("");




            this.CBH03_LOCBANKCD.SetValue("");
            this.CBH03_LOCGRBK.SetValue("");
            this.CBO03_LOCCURRTYPE.SetValue("");
            this.CBO03_LOCGIGANTYPE.SetValue("");
            this.CBO03_LOCGIGANTYPE.SetValue("");
            this.CBO03_LOCLOAN.SetValue("");
            this.CBO03_LOCLOANTYPE.SetValue("");
            this.CBH03_LOCCDAC.SetValue("");
            this.TXT03_LOCCONTRATE.SetValue("");
            this.DTP03_LOCCONTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP03_LOCENDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBO03_LOCCOLLGB.SetValue("");
            this.CBH03_LOCCURRCD.SetValue("");
            this.TXT03_LOCCONTYUL.SetValue("");
            this.CBO03_LOCRATEGB.SetValue("");
            this.TXT03_LOCCONTAMT.SetValue("");
            this.TXT03_LOCCONTDAL.SetValue("");
            this.TXT03_LOCCREDITAMT.SetValue("");
            this.TXT03_LOCRATEDAY.SetValue("");
            this.CBH03_LOCDESCGB.SetValue("");
            this.DTP03_LOCACDATE.SetValue("");
            this.TXT03_LOCLIQNUM1.SetValue("");
            this.TXT03_LOCLIQNUM2.SetValue("");
            this.TXT03_LOCLIQNUM3.SetValue("");

            this.TXT03_LOCLRQNUM1.SetValue("");
            this.TXT03_LOCLRQNUM2.SetValue("");
            this.TXT03_LOCLRQNUM3.SetValue("");

            this.TXT03_LOLRJPNO.SetValue("");

            fsLOLRLICONTSEQ = ""; // 유동성 계약순번
            fsLOLRAMT     = "";
        }
        #endregion

        #region Description : 계약 이력관리 필드 COPY
        private void UP_LOCONTMF_DETAIL_FieldCopy()
        {
            this.CBH03_LOCBANKCD.SetValue(this.CBH02_LOCBANKCD.GetValue().ToString());
            this.CBH03_LOCGRBK.SetValue(this.CBH02_LOCGRBK.GetValue().ToString());
            this.CBO03_LOCCURRTYPE.SetValue(this.CBO02_LOCCURRTYPE.GetValue().ToString());
            this.CBO03_LOCGIGANTYPE.SetValue(this.CBO02_LOCGIGANTYPE.GetValue().ToString());
            this.CBO03_LOCGIGANTYPE.SetValue(this.CBO02_LOCGIGANTYPE.GetValue().ToString());
            this.CBO03_LOCLOAN.SetValue(this.CBO02_LOCLOAN.GetValue().ToString());
            this.CBO03_LOCLOANTYPE.SetValue(this.CBO02_LOCLOANTYPE.GetValue().ToString());
            this.CBH03_LOCCDAC.SetValue(this.CBH02_LOCCDAC.GetValue().ToString());
            this.TXT03_LOCCONTRATE.SetValue(this.TXT02_LOCCONTRATE.GetValue().ToString());
            this.DTP03_LOCCONTDATE.SetValue(this.DTP02_LOCCONTDATE.GetValue().ToString());
            this.DTP03_LOCENDDATE.SetValue(this.DTP02_LOCENDDATE.GetValue().ToString());
            this.CBO03_LOCCOLLGB.SetValue(this.CBO02_LOCCOLLGB.GetValue().ToString());
            this.CBH03_LOCCURRCD.SetValue(this.CBH02_LOCCURRCD.GetValue().ToString());
            this.TXT03_LOCCONTYUL.SetValue(this.TXT02_LOCCONTYUL.GetValue().ToString());
            this.CBO03_LOCRATEGB.SetValue(this.CBO02_LOCRATEGB.GetValue().ToString());
            this.TXT03_LOCCONTAMT.SetValue(this.TXT02_LOCCONTAMT.GetValue().ToString());
            this.TXT03_LOCCONTDAL.SetValue(this.TXT02_LOCCONTDAL.GetValue().ToString());
            this.TXT03_LOCCREDITAMT.SetValue(this.TXT02_LOCCREDITAMT.GetValue().ToString());
            this.TXT03_LOCRATEDAY.SetValue(this.TXT02_LOCRATEDAY.GetValue().ToString());
            this.CBH03_LOCDESCGB.SetValue(this.CBH02_LOCDESCGB.GetValue().ToString());
            this.DTP03_LOCACDATE.SetValue("");
            this.TXT03_LOCLIQNUM1.SetValue("");
            this.TXT03_LOCLIQNUM2.SetValue("");
            this.TXT03_LOCLIQNUM3.SetValue("");

            this.TXT03_LOCLRQNUM1.SetValue("");
            this.TXT03_LOCLRQNUM2.SetValue("");
            this.TXT03_LOCLRQNUM3.SetValue("");
        }
        #endregion

        #region Description : 계약 이력관리 디스플레이
        private void UP_LOCONTMF_DETAIL_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN63_SAV.Visible = true;
                this.BTN63_REM.Visible = false;

                this.BTN63_CODEHELP.Visible = true;
                this.BTN63_CODEHELP1.Visible = true;

                // 계약 이력관리 전표관련 버튼 DISPLAY
                UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(true, false, false, false);
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN63_SAV.Visible = true;
                this.BTN63_REM.Visible = true;

                this.BTN63_CODEHELP.Visible = false;

                if (this.TXT03_LOLRJPNO.GetValue().ToString() == "")
                {
                    this.BTN63_CODEHELP1.Visible = true;
                }
                else
                {
                    this.BTN63_CODEHELP1.Visible = false;
                }

                // 계약 이력관리 전표관련 버튼 DISPLAY
                UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(true, true, true, false);
            }
            else
            {
                this.BTN63_SAV.Visible = false;
                this.BTN63_REM.Visible = false;

                this.BTN63_CODEHELP.Visible = false;
                this.BTN63_CODEHELP1.Visible = false;

                // 계약 이력관리 전표관련 버튼 DISPLAY
                UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(false, false, false, false);
            }
        }
        #endregion

        #region Description : 계약 이력관리 스프레드 이벤트
        private void FPS91_TY_S_AC_86IHA240_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Timer tmr = new Timer();
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Interval = 100;
            tmr.Start();

            this.TXT02_LOCCONTYEAR.SetValue(this.FPS91_TY_S_AC_86IHA240.GetValue("LOCCONTNO").ToString().Substring(0, 4));
            this.TXT02_LOCCONTSEQ.SetValue(this.FPS91_TY_S_AC_86IHA240.GetValue("LOCCONTNO").ToString().Substring(5, 2));
            fsLOCCONTSEQ = this.FPS91_TY_S_AC_86IHA240.GetValue("LOCCONTSEQ").ToString();

            // 계약 이력관리 확인
            UP_LOCONTMF_DETAIL_RUN();
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            this.SetFocus(this.CBO03_LOCCHANGEGB);
        } 
        #endregion

        #region Description : 계약 이력관리 스프레드 전표 출력 이벤트
        private void FPS91_TY_S_AC_86IHA240_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "22")
            {
                if (this.FPS91_TY_S_AC_86IHA240.GetValue("LOLRJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_86IHA240.GetValue("LOLRJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_86IHA240.GetValue("LOLRJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_86IHA240.GetValue("LOLRJPNO").ToString().Substring(14, 3);

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

        #region Description : 계약 이력관리 저장 ProcessCheck
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 통화유형 외화일 경우 외화금액 입력은 필수 사항
            if (this.CBO03_LOCCURRTYPE.GetValue().ToString() == "2")
            {
                if (double.Parse(Get_Numeric(this.TXT03_LOCCONTDAL.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_3CK2B843");
                    this.TXT03_LOCCONTDAL.Focus();

                    e.Successed = false;
                    return;
                }
            }

            DataTable dt = new DataTable();

            // 전표계정 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_88NBH624",
                this.CBH03_LOCCDAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_24RBZ877");
                this.CBH01_LOCCDAC.Focus();

                e.Successed = false;
                return;
            }

            // 담보가 있을 경우 채권 최고액을 입력 함
            if (this.CBO03_LOCCOLLGB.GetValue().ToString() == "Y")
            {
                // 약정금액은 채권최고액을 넘을 수 없음
                if (double.Parse(Get_Numeric(this.TXT03_LOCCONTAMT.GetValue().ToString())) > double.Parse(Get_Numeric(this.TXT03_LOCCREDITAMT.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_AC_88NDM632");
                    this.TXT03_LOCCONTAMT.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBH03_LOCBANKCD.GetValue().ToString().Length != 3)
            {
                this.ShowMessage("TY_M_AC_86IDY230");
                this.CBH03_LOCBANKCD.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                if (this.CBH03_LOCBANKCD.GetValue().ToString() != this.CBH03_LOCGRBK.GetValue().ToString().Substring(0, 3))
                {
                    this.ShowMessage("TY_M_AC_86IDZ231");
                    this.CBH03_LOCBANKCD.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO03_LOCCOLLGB.GetValue().ToString() == "Y")
            {
                if (Get_Numeric(this.TXT03_LOCCREDITAMT.GetValue().ToString()) == "0")
                {
                    this.ShowMessage("TY_M_AC_888F2524");
                    this.TXT03_LOCCREDITAMT.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 기간유형은 장기이면 실행을 여러번 할 수 있다
            // 장기이면서 실행이 2번 이상일 되었을 경우
            // 단기로 변경하고자 할때에는 오류 메세지를 띄움
            if (this.CBO02_LOCGIGANTYPE.GetValue().ToString() == "2") // 장기
            {
                if (this.CBO03_LOCGIGANTYPE.GetValue().ToString() == "1") // 단기
                {
                    string sLOACCONTNO  = string.Empty;
                    string sLOACCONTSEQ = string.Empty;

                    sLOACCONTNO  = Set_Fill4(this.TXT02_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOCCONTSEQ.GetValue().ToString());
                    sLOACCONTSEQ = Set_Fill3(fsLOCCONTSEQ.ToString());

                    // 이 계약건으로 실행이 여러건 이루어졌을 경우 확인
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_87UET486",
                        sLOACCONTNO.ToString(),
                        sLOACCONTSEQ.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count >= 2)
                    {
                        this.ShowMessage("TY_M_AC_87UEV487");
                        this.CBO03_LOCGIGANTYPE.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (int.Parse(this.DTP03_LOCCONTDATE.GetValue().ToString()) > int.Parse(this.DTP03_LOCENDDATE.GetValue().ToString()))
            {
                this.ShowMessage("TY_M_AC_86IDR227");
                this.DTP03_LOCENDDATE.Focus();

                e.Successed = false;
                return;
            }

            if (this.CBO03_LOCCURRTYPE.GetValue().ToString() == "1") // 원화
            {
                this.CBH03_LOCCURRCD.SetValue("");
            }
            else // 외화
            {
                if (this.CBH03_LOCCURRCD.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_86KG5249");
                    this.SetFocus(this.CBH03_LOCCURRCD.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            if (fsLOCCHANGEGB == "03" && this.CBO03_LOCCHANGEGB.GetValue().ToString() == "04")
            {
                this.ShowMessage("TY_M_AC_87IFG423");
                this.SetFocus(this.CBO03_LOCCHANGEGB);

                e.Successed = false;
                return;
            }

            // 만기 연장시 처리일자와 유동성 관리번호가 있어야 함
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                // 처리일자
                if (Get_Date(this.DTP03_LOCACDATE.GetValue().ToString()) == "")
                {
                    this.ShowMessage("TY_M_AC_87IDE419");
                    this.SetFocus(this.DTP03_LOCACDATE);

                    e.Successed = false;
                    return;
                }

                // 유동성 번호
                if (this.TXT03_LOCLIQNUM1.GetValue().ToString() == "" || this.TXT03_LOCLIQNUM2.GetValue().ToString() == "" || this.TXT03_LOCLIQNUM3.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_87IDE420");

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 차입금 계약 이력관리 - 유동성 재대체 잔액 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_87IDM421",
                        this.TXT03_LOCLIQNUM1.GetValue().ToString(),
                        this.TXT03_LOCLIQNUM2.GetValue().ToString(),
                        fsLOLRLICONTSEQ.ToString(),
                        this.TXT03_LOCLIQNUM3.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_AC_87IDP422");

                        e.Successed = false;
                        return;
                    }
                }

                if (fsWK_GUBUN2 == "UPT")
                {
                    string sLORENCTNO = string.Empty;

                    sLORENCTNO = Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString());

                    // 상환 DATA 존재하면 수정 불가
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_8889T522",
                        sLORENCTNO.ToString(),
                        fsLOLRLICONTSEQ.ToString(),
                        this.TXT03_LOCLIQNUM3.GetValue().ToString(),
                        "05"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_AC_8889W523");

                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                this.DTP03_LOCACDATE.SetValue("");
            }

            // 기간 유형 = 단기일 경우
            //             약정 계정과목은 211004, 211005 계정과목이 오고
            // 기간 유형 = 장기일 경우
            //             약정 계정과목은 221002, 221003 계정과목이 옴
            if (this.CBO03_LOCGIGANTYPE.GetValue().ToString() == "1") // 단기
            {
                if (this.CBH03_LOCCDAC.GetValue().ToString().Substring(0, 6) != "211004" && this.CBH03_LOCCDAC.GetValue().ToString().Substring(0, 6) != "211005")
                {
                    this.ShowMessage("TY_M_AC_883GA505");
                    this.SetFocus(this.CBH03_LOCCDAC.CodeText);

                    e.Successed = false;
                    return;
                }
            }
            else // 장기
            {
                if (this.CBH03_LOCCDAC.GetValue().ToString().Substring(0, 6) != "221002" && this.CBH03_LOCCDAC.GetValue().ToString().Substring(0, 6) != "221003")
                {
                    this.ShowMessage("TY_M_AC_883GA505");
                    this.SetFocus(this.CBH03_LOCCDAC.CodeText);

                    e.Successed = false;
                    return;
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

        #region Description : 계약 이력관리 삭제 ProcessCheck
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sLOACCONTNO = string.Empty;

            sLOACCONTNO = Set_Fill4(this.TXT02_LOCCONTYEAR.GetValue().ToString()) + Set_Fill2(this.TXT02_LOCCONTSEQ.GetValue().ToString());

            DataTable dt = new DataTable();

            // 실행 DATA 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86IFD234",
                sLOACCONTNO.ToString(),
                fsLOCCONTSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_86IFE235");
                this.SetFocus(this.CBO03_LOCCHANGEGB);

                e.Successed = false;
                return;
            }

            if (fsLOCCHANGEGB == "03" && this.CBO03_LOCCHANGEGB.GetValue().ToString() == "04")
            {
                this.ShowMessage("TY_M_AC_87IFG423");
                this.SetFocus(this.CBO03_LOCCHANGEGB);

                e.Successed = false;
                return;
            }

            // 만기 연장
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                string sLORENCTNO = string.Empty;

                sLORENCTNO = Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString());

                // 상환 DATA 존재하면 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_8889T522",
                    sLORENCTNO.ToString(),
                    fsLOLRLICONTSEQ.ToString(),
                    this.TXT03_LOCLIQNUM3.GetValue().ToString(),
                    "05"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_8889W523");

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

        #region Description : 계약 이력관리 전표생성 ProcessCheck
        private void BTN63_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsLOLRAMT = "";

            DataTable dt = new DataTable();

            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                // 차입금 계약 이력관리 - 유동성 재대체 잔액 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_87IDM421",
                    this.TXT03_LOCLIQNUM1.GetValue().ToString(),
                    this.TXT03_LOCLIQNUM2.GetValue().ToString(),
                    fsLOLRLICONTSEQ.ToString(),
                    this.TXT03_LOCLIQNUM3.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_AC_87IDP422");

                    e.Successed = false;
                    return;
                }
                else
                {
                    fsLOLRAMT = dt.Rows[0]["JANAMT"].ToString();
                }
            }

            if (fsLOCCHANGEGB == "03" && this.CBO03_LOCCHANGEGB.GetValue().ToString() == "04")
            {
                this.ShowMessage("TY_M_AC_87IFG423");
                this.SetFocus(this.CBO03_LOCCHANGEGB);

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 계약 이력관리 전표취소 ProcessCheck
        private void BTN63_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 차입금 상환관리 DATA 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_86RAF290",
                this.TXT03_LOLRJPNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_86RAJ291");

                e.Successed = false;
                return;
            }

            // 만기 연장
            if (this.CBO03_LOCCHANGEGB.GetValue().ToString() == "03")
            {
                string sLORENCTNO = string.Empty;
                
                sLORENCTNO = Set_Fill4(this.TXT03_LOCLIQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLIQNUM2.GetValue().ToString());

                // 상환 DATA 존재하면 전표 취소 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_8889T522",
                    sLORENCTNO.ToString(),
                    fsLOLRLICONTSEQ.ToString(),
                    this.TXT03_LOCLIQNUM3.GetValue().ToString(),
                    "05"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_8889W523");

                    e.Successed = false;
                    return;
                }

                string sLOLRCONTNO  = string.Empty;
                string sLOLRCONTSEQ = string.Empty;
                string sLOLRNUM     = string.Empty;

                // 유동성 재대체 계약번호
                sLOLRCONTNO  = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString());
                // 유동성 재대체 계약순번
                sLOLRCONTSEQ = fsLOLRCONTSEQ.ToString();
                // 유동성 재대체 순번
                sLOLRNUM     = this.TXT03_LOCLRQNUM3.GetValue().ToString();

                // 유동성 대체에 자료 존재하면 전표취소 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_88UAF664",
                    sLOLRCONTNO.ToString(),
                    sLOLRCONTSEQ.ToString(),
                    sLOLRNUM.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_88UAJ665");

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

        #region Description : 계약 이력관리 처리일자 이벤트
        private void DTP03_LOCACDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN63_SAV.Visible == true)
                {
                    SetFocus(this.BTN63_SAV);
                }
            }
        }
        #endregion

        #region Description : 계약 이력관리 - 계약번호 조회 코드헬프
        private void BTN63_CODEHELP_Click(object sender, EventArgs e)
        {
            TYACLO01C1 popup = new TYACLO01C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT02_LOCCONTYEAR.SetValue(popup.fsLOCCONTYEAR); // 계약년도
                this.TXT02_LOCCONTSEQ.SetValue(popup.fsLOCCONTSEQ);   // 계약번호

                // 계약 이력관리 확인
                UP_LOCONTMF_DETAIL_FINAL_RUN();

                UP_LOCONTMF_DETAIL_FieldCopy();

                this.SetFocus(this.CBO03_LOCCHANGEGB);
            }
        }
        #endregion

        #region Description : 계약 이력관리 - 유동성 대체 조회 코드헬프
        private void BTN63_CODEHELP1_Click(object sender, EventArgs e)
        {
            fsLOLRLICONTSEQ = ""; // 유동성 계약순번
            fsLOLRAMT     = "";

            if (this.TXT02_LOCCONTYEAR.GetValue().ToString() != "" && this.TXT02_LOCCONTSEQ.GetValue().ToString() != "")
            {
                TYACLO01C2 popup = new TYACLO01C2(this.TXT02_LOCCONTYEAR.GetValue().ToString(), this.TXT02_LOCCONTSEQ.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT03_LOCLIQNUM1.SetValue(popup.fsLOLICONTYEAR); // 유동성 계약년도
                    this.TXT03_LOCLIQNUM2.SetValue(popup.fsLOLICONTNO);   // 유동성 계약번호
                    this.TXT03_LOCLIQNUM3.SetValue(popup.fsLOLINUM);      // 유동성 번호

                    fsLOLRLICONTSEQ = popup.fsLOLICONTSEQ;                // 유동성 계약순번
                }
            }
        }
        #endregion

        #region Description : 계약 이력관리 전표관련 버튼 DISPLAY
        private void UP_LOCONTMF_DETAIL_JPNO_BTN_DISPLAY(bool bflag1, bool bflag2, bool bflag3, bool bflag4)
        {
            this.BTN63_SAV.Visible = bflag1;
            this.BTN63_REM.Visible = bflag2;

            this.BTN63_JPNO_CRE.Visible      = bflag3;
            this.BTN63_JUNPYO_CANCEL.Visible = bflag4;
            this.BTN63_PRT.Visible           = bflag4;
        }
        #endregion

        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fsWK_GUBUN1 = "";
            fsWK_GUBUN2 = "";

            if (tabControl1.SelectedIndex == 0) // 계약관리 조회
            {
                fsTAB_GUBUN = "MAIN";

                // 계약관리 메인 최종 조회
                UP_LOCONTMF_MAIN_INQ();
            }
            else if (tabControl1.SelectedIndex == 1) // 계약관리 마스터
            {
                this.TXT01_LOCCONTYEAR.SetReadOnly(true);

                fsTAB_GUBUN = "LOCONTMF_MASTER";

                fsWK_GUBUN1 = "TAB";
                UP_LOCONTMF_MASTER_BTN_DISPLAY(fsWK_GUBUN1);

                this.TXT01_LOCCONTYEAR.SetValue(fsLOCCONTYEAR.ToString());
                this.TXT01_LOCCONTSEQ.SetValue(fsLOCCONTNUM.ToString());

                // 계약 마스터 조회
                UP_LOCONTMF_MASTER_SEARCH();
            }
            else if (tabControl1.SelectedIndex == 2) // 계약 이력관리
            {
                this.BTN63_CODEHELP.Visible = false;

                fsTAB_GUBUN = "LOCONTMF_DETAIL";

                fsWK_GUBUN2 = "TAB";
                UP_LOCONTMF_DETAIL_BTN_DISPLAY(fsWK_GUBUN2);

                this.TXT02_LOCCONTYEAR.SetValue(fsLOCCONTYEAR.ToString());
                this.TXT02_LOCCONTSEQ.SetValue(fsLOCCONTNUM.ToString());

                // 계약 이력관리 조회
                UP_LOCONTMF_DETAIL_SEARCH();
            }            
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
        private string UP_GET_B2VLMI(string sB2CDMI, string sDOLLAR)
        {
            string sReturn = string.Empty;

            switch (sB2CDMI.ToString())
            {
                case "02": // 금융기관
                    sReturn = this.CBH03_LOCGRBK.GetValue().ToString();

                    break;

                case "07": // 계좌번호

                    sReturn = "";

                    break;

                case "12": // 차입금 관리번호

                    // 유동성 재대체 번호
                    sReturn = Set_Fill4(this.TXT03_LOCLRQNUM1.GetValue().ToString()) + Set_Fill2(this.TXT03_LOCLRQNUM2.GetValue().ToString()) + Set_Fill3(fsLOLRCONTSEQ.ToString()) + Set_Fill3(this.TXT03_LOCLRQNUM3.GetValue().ToString());

                    break;

                case "20": // 이자율

                    sReturn = "";

                    break;

                case "21": // 외화금액

                    sReturn = sDOLLAR.ToString();

                    break;

                case "30": // 외화구분

                    sReturn = this.CBH03_LOCCURRCD.GetValue().ToString();

                    break;

                case "36": // 환율

                    sReturn = "";

                    break;
            }


            return sReturn;
        }
        #endregion

        #region Description : 은행지점 이벤트
        private void CBH01_LOCGRBK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.CBH01_LOCGRBK.GetValue().ToString() != "")
                {
                    this.CBH01_LOCBANKCD.SetValue(this.CBH01_LOCGRBK.GetValue().ToString().Substring(0, 3));
                }
            }
        }

        private void CBH01_LOCGRBK_Leave(object sender, EventArgs e)
        {
            if (this.CBH01_LOCGRBK.GetValue().ToString() != "")
            {
                this.CBH01_LOCBANKCD.SetValue(this.CBH01_LOCGRBK.GetValue().ToString().Substring(0, 3));
            }
        }
        #endregion
    }
}