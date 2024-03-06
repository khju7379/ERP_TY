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
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    public partial class TYUTIN005I : TYBase
    {
        private int fiRow = 0;

        string fsVSYEAR    = string.Empty;
        string fsVSBRANCH  = string.Empty;
        string fsVSRPTGUBN = string.Empty;
        string fsVSCONFGB  = string.Empty;

        private string fsPOPUP     = string.Empty;

        private string fsTAB_GUBUN = string.Empty;
        private string fsVSJUBAN   = string.Empty;
        private string fsVESLGLOS  = string.Empty;
        private string fsVSHMGB    = string.Empty;


        private string fsWK_GUBUN1 = string.Empty;
        private string fsWK_GUBUN2 = string.Empty;
        private string fsWK_GUBUN3 = string.Empty;
        private string fsWK_GUBUN4 = string.Empty;
        private string fsWK_GUBUN5 = string.Empty;

        private string fsIPHANG   = string.Empty;
        private string fsBONSUN   = string.Empty;
        private string fsHWAJU    = string.Empty;
        private string fsHWAMUL   = string.Empty;
        private string fsBLNO     = string.Empty;
        private string fsMSN      = string.Empty;
        private string fsHSN      = string.Empty;
        private string fsCUSTIL   = string.Empty;
        private string fsCHASU    = string.Empty;



        private string fsCMSHQTY  = string.Empty;
        private string fsCMBLQTY  = string.Empty;
        private string fsCMHWAPE  = string.Empty;
        private string fsCMFACT   = string.Empty;
        private string fsCMHMDATE = string.Empty;

        private string fsSVBIJUNG = string.Empty;
        private string fsSVMOGB   = string.Empty;
        private string fsSHOREQTY = string.Empty;
        
        private string fsCOOVAM   = string.Empty;

        private string fsSEQCH    = string.Empty;
        private string fsSEQGB    = string.Empty;
        private string fsPRESEQCH = string.Empty;


        private string fsJUNIPMTQTY = string.Empty;
        private string fsJUNIPBSQTY = string.Empty;
        private string fsSVMTQTY    = string.Empty;
        private string fsSVKLQTY    = string.Empty;
        private string fsIPBLNOSEQ  = string.Empty;
        private string fsIPHBLNOSEQ = string.Empty;
        private string fsIPMTQTY    = string.Empty;
        private string fsIPBSQTY    = string.Empty;

        private string fsCSCUQTY    = string.Empty;
        private string fsCSACTHJ    = string.Empty;

        #region Description : 페이지 로드 
        public TYUTIN005I()
        {
            InitializeComponent();
        }

        public TYUTIN005I(string sCSIPHANG, string sCSBONSUN, string sCSHWAJU, string sCSHWAMUL, string sCSBLNO, string sCSMSNSEQ, string sCSHSNSEQ, string sCSCUSTIL, string sCSCHASU, string sPOPUP)
        {
            InitializeComponent();

            fsIPHANG   = sCSIPHANG;
            fsBONSUN   = sCSBONSUN;
            fsHWAJU    = sCSHWAJU;
            fsHWAMUL   = sCSHWAMUL;
            fsBLNO     = sCSBLNO;
            fsMSN      = sCSMSNSEQ;
            fsHSN      = sCSHSNSEQ;
            fsCUSTIL   = sCSCUSTIL;
            fsCHASU    = sCSCHASU;

            fsPOPUP    = sPOPUP;
        }

        private void TYUTIN005I_Load(object sender, System.EventArgs e)
        {
            // 선박입항관리
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            //this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(BTN61_EDIT_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            // 입고화물관리
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            //this.BTN62_EDIT.ProcessCheck += new TButton.CheckHandler(BTN62_EDIT_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            // SURVEY REPORT 관리
            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            //this.BTN63_EDIT.ProcessCheck += new TButton.CheckHandler(BTN63_EDIT_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);

            // B/L별 입고 관리
            this.BTN64_SAV.ProcessCheck += new TButton.CheckHandler(BTN64_SAV_ProcessCheck);
            //this.BTN64_EDIT.ProcessCheck += new TButton.CheckHandler(BTN64_EDIT_ProcessCheck);
            this.BTN64_REM.ProcessCheck += new TButton.CheckHandler(BTN64_REM_ProcessCheck);


            // 통관 관리
            this.BTN65_SAV.ProcessCheck += new TButton.CheckHandler(BTN65_SAV_ProcessCheck);
            //this.BTN65_EDIT.ProcessCheck += new TButton.CheckHandler(BTN65_EDIT_ProcessCheck);
            this.BTN65_REM.ProcessCheck += new TButton.CheckHandler(BTN65_REM_ProcessCheck);

            // 고지팀만 INVENTORY메뉴(입항~통관까지) 보이게 하고
            // 부두는 입항관리만 보이게 함
            if (TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0107-M" || TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0310-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0304-F" || TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0150-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0287-M" || TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0311-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0403-M" || TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0391-F" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0419-M" )
            {
                fsTAB_GUBUN = "INVENTORY";

                this.MTB01_CMMAECH.SetReadOnly(true);

                this.CBO01_SVTANKNO.SetValue("");

                if (fsPOPUP.ToString() == "EDI")
                {
                    this.DTP01_CSIPHANG.SetValue(fsIPHANG);
                    this.CBH01_CSBONSUN.SetValue(fsBONSUN);
                    this.CBH01_CSHWAJU.SetValue(fsHWAJU);
                    this.CBH01_CSHWAMUL.SetValue(fsHWAMUL);
                    this.TXT01_CSBLNO.SetValue(fsBLNO);
                    this.TXT01_CSMSNSEQ.SetValue(fsMSN);
                    this.TXT01_CSHSNSEQ.SetValue(fsHSN);
                    this.DTP01_CSCUSTIL.SetValue(fsCUSTIL);
                    this.TXT01_CSCHASU.SetValue(fsCHASU);

                    tabControl1.SelectedIndex = 5;

                    UP_UTICUSTF_RUN();
                }

                this.DTP01_STIPHANG.SetValue(DateTime.Now.AddDays(-10).ToString("yyyyMMdd"));

                // INVENTORY 전체 조회
                UP_INVENTORY_SEARCH();

                SetStartingFocus(this.DTP01_STIPHANG);
            }
            else if (TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0259-F")
            {
                this.tabControl1.TabPages.Remove(this.tabPage4);
                this.tabControl1.TabPages.Remove(this.tabPage5);
                this.tabControl1.TabPages.Remove(this.tabPage6);
            }
            else
            {
                this.tabControl1.TabPages.Remove(this.tabPage1);
                this.tabControl1.TabPages.Remove(this.tabPage3);
                this.tabControl1.TabPages.Remove(this.tabPage4);
                this.tabControl1.TabPages.Remove(this.tabPage5);
                this.tabControl1.TabPages.Remove(this.tabPage6);

                fsTAB_GUBUN = "UTIVESLF";
            }

            CKB01_VEWHARF1.SetReadOnly(true);
            CKB01_VEWHARF2.SetReadOnly(true);
            CKB01_VEWHARF3.SetReadOnly(true);
            CKB01_VEWHARF4.SetReadOnly(true);
        }
        #endregion

        #region Description : INVENTORY

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (fsTAB_GUBUN == "INVENTORY")
            {
                // INVENTORY 전체 조회
                UP_INVENTORY_SEARCH();
            }
            else if (fsTAB_GUBUN == "UTIVESLF")
            {
                // 입항관리 전체 조회
                UP_UTIVESLF_SEARCH();
            }
            else if (fsTAB_GUBUN == "UTICMDTF")
            {
                // 입고화물관리 전체 조회
                UP_UTICMDTF_SEARCH();
            }
            else if (fsTAB_GUBUN == "UTISURVF")
            {
                // SURVEY REPORT 전체 조회
                UP_UTISURVF_SEARCH();
            }
            else if (fsTAB_GUBUN == "UTIIPGOF")
            {
                // B/L별 입고관리 전체 조회
                UP_UTIIPGOF_SEARCH();
            }
            else if (fsTAB_GUBUN == "UTICUSTF")
            {
                // 통관관리 전체 조회
                UP_UTICUSTF_SEARCH();
            }
        }
        #endregion

        #region Description : INVENTORY 조회
        private void UP_INVENTORY_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66H9A290",
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString(),
                this.CBH01_SBONSUN.GetValue().ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString(),
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString(),
                this.CBH01_SBONSUN.GetValue().ToString(),
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString(),
                this.CBH01_SBONSUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_66GIB289.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_66GIB289.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_66GIB289.GetValue(i, "VESL_DATA").ToString() == "VESL")
                    {
                        this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 7].Text = "";
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 7].BackColor = Color.Thistle;
                    }

                    if (this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "선적" ||
                        this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "재선적")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 8].BackColor = Color.LightSeaGreen;
                    }
                    else
                    {
                        if (this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "")
                        {
                            if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMSHQTY").ToString()) != 0)
                            {
                                // 특정 ROW 색깔 입히기
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 8].BackColor = Color.Thistle;
                            }
                        }
                    }

                    if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "SURVEY_DATA").ToString()) != 0)
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 9].BackColor = Color.SkyBlue;
                    }
                    else
                    {
                        if (this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "선적" ||
                            this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "재선적")
                        {
                            if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "SURVEY_DATA").ToString()) == 0)
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 9].Text = "";
                            }
                            else
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 9].BackColor = Color.SkyBlue;
                            }
                        }
                        else
                        {
                            if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "SURVEY_DATA").ToString()) == 0)
                            {
                                if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMSHQTY").ToString()) != 0)
                                {
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 9].Text = "";
                                    // 특정 ROW 색깔 입히기
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 9].BackColor = Color.Thistle;
                                }
                                else
                                {
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 9].Text = "";
                                }
                            }
                            else
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 9].BackColor = Color.SkyBlue;
                            }
                        }
                    }

                    if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "IPGO_DATA").ToString()) != 0)
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 10].BackColor = Color.SkyBlue;
                    }
                    else
                    {
                        if (this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "선적" ||
                            this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "재선적")
                        {
                            if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "IPGO_DATA").ToString()) == 0)
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 10].Text = "";
                            }
                            else
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 10].BackColor = Color.SkyBlue;
                            }
                        }
                        else
                        {
                            if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "IPGO_DATA").ToString()) == 0)
                            {
                                if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMSHQTY").ToString()) != 0)
                                {
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 10].Text = "";
                                    // 특정 ROW 색깔 입히기
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 10].BackColor = Color.Thistle;
                                }
                                else
                                {
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 10].Text = "";
                                }
                            }
                            else
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 10].BackColor = Color.SkyBlue;
                            }
                        }
                    }

                    if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CUST_DATA").ToString()) != 0)
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 11].BackColor = Color.SkyBlue;
                    }
                    else
                    {
                        if (this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "선적" ||
                            this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMDT_DATA").ToString() == "재선적")
                        {
                            if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CUST_DATA").ToString()) == 0)
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 11].Text = "";
                            }
                            else
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 11].BackColor = Color.SkyBlue;
                            }
                        }
                        else
                        {
                            if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CUST_DATA").ToString()) == 0)
                            {
                                if (double.Parse(this.FPS91_TY_S_UT_66GIB289.GetValue(i, "CMSHQTY").ToString()) != 0)
                                {
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 11].Text = "";

                                    // 특정 ROW 색깔 입히기
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 11].BackColor = Color.Thistle;
                                }
                                else
                                {
                                    this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 11].Text = "";
                                }
                            }
                            else
                            {
                                this.FPS91_TY_S_UT_66GIB289.ActiveSheet.Cells[i, 11].BackColor = Color.SkyBlue;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : INVENTORY 스프레드 이벤트
        private void FPS91_TY_S_UT_66GIB289_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "7")
            {
                this.tabControl1.SelectedIndex = 1;

                this.DTP01_VSIPHANG.SetReadOnly(true);
                this.CBH01_VSBONSUN.SetReadOnly(true);

                this.DTP01_VSIPHANG.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("IPHANG").ToString());
                this.CBH01_VSBONSUN.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("BONSUN").ToString());

                // 입항관리 확인
                UP_UTIVESLF_RUN(this.DTP01_VSIPHANG.GetValue().ToString(), this.CBH01_VSBONSUN.GetValue().ToString());

                // 입항관리 조회
                UP_UTIVESLF_TAB_SEARCH();
            }
            else if (e.Column.ToString() == "8")
            {
                this.tabControl1.SelectedIndex = 2;

                this.DTP01_CMIPHANG.SetReadOnly(true);
                this.CBH01_CMBONSUN.SetReadOnly(true);
                this.CBH01_CMHWAJU.SetReadOnly(true);
                this.CBH01_CMHWAMUL.SetReadOnly(true);

                this.DTP01_CMIPHANG.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("IPHANG").ToString());
                this.CBH01_CMBONSUN.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("BONSUN").ToString());
                this.CBH01_CMHWAJU.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAJU").ToString());
                this.CBH01_CMHWAMUL.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAMUL").ToString());

                // 입고화물관리 확인
                UP_UTICMDTF_RUN();

                // 입고화물관리 조회
                UP_UTICMDTF_TAB_SEARCH();
            }
            else if (e.Column.ToString() == "9")
            {
                this.tabControl1.SelectedIndex = 3;

                this.DTP01_SVIPHANG.SetReadOnly(true);
                this.CBH01_SVBONSUN.SetReadOnly(true);
                this.CBH01_SVHWAJU.SetReadOnly(true);
                this.CBH01_SVHWAMUL.SetReadOnly(true);

                this.DTP01_SVIPHANG.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("IPHANG").ToString());
                this.CBH01_SVBONSUN.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("BONSUN").ToString());
                this.CBH01_SVHWAJU.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAJU").ToString());
                this.CBH01_SVHWAMUL.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAMUL").ToString());

                // SURVEY REPORT 조회
                UP_UTISURVF_TAB_SEARCH(this.DTP01_SVIPHANG.GetValue().ToString(),
                                       this.CBH01_SVBONSUN.GetValue().ToString(),
                                       this.CBH01_SVHWAJU.GetValue().ToString(),
                                       this.CBH01_SVHWAMUL.GetValue().ToString(),
                                       ""
                                       );
            }
            else if (e.Column.ToString() == "10")
            {
                this.tabControl1.SelectedIndex = 4;

                this.DTP01_IPIPHANG.SetReadOnly(true);
                this.CBH01_IPBONSUN.SetReadOnly(true);
                this.CBH01_IPHWAJU.SetReadOnly(true);
                this.CBH01_IPHWAMUL.SetReadOnly(true);

                this.DTP01_IPIPHANG.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("IPHANG").ToString());
                this.CBH01_IPBONSUN.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("BONSUN").ToString());
                this.CBH01_IPHWAJU.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAJU").ToString());
                this.CBH01_IPHWAMUL.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAMUL").ToString());

                // B/L별 입고관리 조회
                UP_UTIIPGOF_TAB_SEARCH(this.DTP01_IPIPHANG.GetValue().ToString(),
                                       this.CBH01_IPBONSUN.GetValue().ToString(),
                                       this.CBH01_IPHWAJU.GetValue().ToString(),
                                       this.CBH01_IPHWAMUL.GetValue().ToString(),
                                       ""
                                       );
            }
            else if (e.Column.ToString() == "11")
            {
                this.tabControl1.SelectedIndex = 5;

                this.DTP01_CSIPHANG.SetReadOnly(true);
                this.CBH01_CSBONSUN.SetReadOnly(true);
                this.CBH01_CSHWAJU.SetReadOnly(true);
                this.CBH01_CSHWAMUL.SetReadOnly(true);

                this.DTP01_CSIPHANG.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("IPHANG").ToString());
                this.CBH01_CSBONSUN.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("BONSUN").ToString());
                this.CBH01_CSHWAJU.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAJU").ToString());
                this.CBH01_CSHWAMUL.SetValue(this.FPS91_TY_S_UT_66GIB289.GetValue("HWAMUL").ToString());

                // 통관관리 조회
                UP_UTICUSTF_TAB_SEARCH(this.DTP01_CSIPHANG.GetValue().ToString(),
                                       this.CBH01_CSBONSUN.GetValue().ToString(),
                                       this.CBH01_CSHWAJU.GetValue().ToString(),
                                       this.CBH01_CSHWAMUL.GetValue().ToString(),
                                       "",
                                       "",
                                       ""
                                       );
            }
        }
        #endregion

        #endregion

        #region Description : 입항관리

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsVSJUBAN  = "";
            fsVESLGLOS = "";

            UP_UTIVESLF_FieldClear();

            fsWK_GUBUN1 = "NEW";

            UP_UTIVESLF_BTN_DISPLAY(fsWK_GUBUN1);

            // FOCUS
            Timer tmr = new Timer();

            tmr.Tick += delegate
            {
                tmr.Stop();
                this.SetFocus(this.DTP01_VSIPHANG);
            };

            tmr.Interval = 100;
            tmr.Start();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN1.ToString() == "NEW")
            {
                UP_UTIVESLF_SAV();

                this.DTP01_VSIPHANG.SetReadOnly(true);
                this.CBH01_VSBONSUN.SetReadOnly(true);
            }
            else if (fsWK_GUBUN1.ToString() == "UPT")
            {
                UP_UTIVESLF_UPT();
            }

            // 선박사양관리 접안가능부두 저장
            UP_UTIVESSF_UPT();

            // 입항관리 전체 조회
            UP_UTIVESLF_SEARCH();

            // 입항관리 조회
            UP_UTIVESLF_TAB_SEARCH();

            UP_UTIVESLF_BTN_DISPLAY("");

            // 값 저장
            UP_SET_Cookie1(this.DTP01_VSIPHANG.GetValue().ToString(), this.CBH01_VSBONSUN.GetValue().ToString());
        }
        #endregion

        //#region Description : 수정 버튼
        //private void BTN61_EDIT_Click(object sender, EventArgs e)
        //{
        //    UP_UTIVESLF_UPT();

        //    // 입항관리 조회
        //    UP_UTIVESLF_TAB_SEARCH();

        //    // 값 저장
        //    UP_SET_Cookie1(this.DTP01_VSIPHANG.GetValue().ToString(), this.CBH01_VSBONSUN.GetValue().ToString());
        //}
        //#endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_UTIVESLF_DEL();

            this.DTP01_VSIPHANG.SetReadOnly(false);
            this.CBH01_VSBONSUN.SetReadOnly(false);

            // 입항관리 전체 조회
            UP_UTIVESLF_SEARCH();

            // 입항관리 조회
            UP_UTIVESLF_TAB_SEARCH();

            UP_UTIVESLF_FieldClear();

            UP_UTIVESLF_BTN_DISPLAY("");

            this.DTP01_VSIPHANG.Focus();
        }
        #endregion

        #region Description : 선박 사양관리 확인
        private void UP_UTIVESSF_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66LGI330",
                this.CBH01_VSBONSUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsVESLGLOS = dt.Rows[0]["VESLGLOS"].ToString();
                this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion

        #region Description : 입항관리 전체 조회
        private void UP_UTIVESLF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66GDU269",
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_66LC6317.SetValue(dt);
        }
        #endregion

        #region Description : 입항관리 조회
        private void UP_UTIVESLF_TAB_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66S9Y411",
                this.DTP01_VSIPHANG.GetValue().ToString(),
                this.CBH01_VSBONSUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_674DA548.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_UT_674DA548.SetValue(dt);
            }
        }
        #endregion

        #region Description : 입항관리 확인
        private void UP_UTIVESLF_RUN(string sVSIPHANG, string sVSBONSUN)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66LC1314",
                sVSIPHANG.ToString(),
                sVSBONSUN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsVSJUBAN  = dt.Rows[0]["VSJUBAN"].ToString();
                fsVESLGLOS = dt.Rows[0]["VESLGLOS"].ToString();
                this.CurrentDataTableRowMapping(dt, "01");

                fsWK_GUBUN1 = "UPT";

                UP_UTIVESLF_BTN_DISPLAY(fsWK_GUBUN1);

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.CBH01_VSCREW.CodeText);
                };

                tmr.Interval = 100;
                tmr.Start();
                
            }

            // 값 저장
            UP_SET_Cookie1(sVSIPHANG.ToString(), sVSBONSUN.ToString());
        }
        #endregion

        #region Description : 입항관리 저장
        private void UP_UTIVESLF_SAV()
        {
            DataTable dt = new DataTable();

            string sVSBONSUN  = string.Empty;
            string sProcedure = string.Empty;
            string sVESLAJET  = string.Empty;

            string sIPTIM = string.Empty;
            string sCHTIM = string.Empty;

            if (this.CBH01_VESLAJET.GetValue().ToString() != "")
            {
                sVESLAJET = this.CBH01_VESLAJET.GetValue().ToString().Substring(0, 2);
            }
            else
            {
                sVESLAJET = "";
            }

            sIPTIM = Set_Fill2(TXT01_VSIPTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSIPTIM2.GetValue().ToString());
            sCHTIM = Set_Fill2(TXT01_VSCHTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSCHTIM2.GetValue().ToString());
            // 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66LHE338",
                                    Get_Date(DTP01_VSIPHANG.GetValue().ToString()),                 // 입항일자
                                    CBH01_VSBONSUN.GetValue().ToString().ToUpper(),					// 본선
                                    Get_Numeric(TXT01_VESLGLOS.GetValue().ToString()),				// G/T
                                    sVESLAJET.ToString(),                                           // 대리점
                                    Get_Numeric(Get_Date(this.MTB01_VSIPHANG2.GetValue().ToString().Trim())),	// 외항입항일자
                                    sIPTIM.ToString(),									            // 입항시간
                                    Get_Numeric(Get_Date(this.MTB01_VSCHDAT.GetValue().ToString().Trim())),   // 출항일자
                                    sCHTIM.ToString(),									            // 출항시간
                                    CBH01_VSVSGB.GetValue().ToString(),								// 선박구분
                                    CBH01_VSHMGB.GetValue().ToString(),								// 화물구분
                                    CBH01_VSJUBAN.GetValue().ToString(),							// 접안장소
                                    CBH01_VSCREW.GetValue().ToString().ToUpper(),					// 선원국적
                                    Get_Numeric(TXT01_VSCUSTOMS.GetValue().ToString()),				// 입항세관
                                    TXT01_VSBANIP.GetValue().ToString(),							// 반입근거번호
                                    TXT01_VSJUKHA.GetValue().ToString(),							// 적하목록번호
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                    // 작성사번
                                    );

            this.DbConnector.ExecuteNonQuery();

            sVSBONSUN = this.CBH01_VSBONSUN.GetValue().ToString();

            if (sVSBONSUN != "PP1" && sVSBONSUN != "PP2" && sVSBONSUN != "PP3" && sVSBONSUN != "PP4" &&
                sVSBONSUN != "PP5" && sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3" && sVSBONSUN != "TK7")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_UT_66GE8274",
                    this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                    this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                    CBH01_VSJUBAN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    // 등록
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_66LHM341",
                                            this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                                            this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                                            this.CBH01_VSJUBAN.GetValue().ToString(),
                                            "1",
                                            fsVESLGLOS.ToString(),
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                            );

                    this.DbConnector.ExecuteNonQuery();

                }
                else
                {
                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_66LHH340",
                                            "1",
                                            fsVESLGLOS.ToString(),
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                            this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                                            this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                                            CBH01_VSJUBAN.GetValue().ToString()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }

                
            }

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 입항관리 수정
        private void UP_UTIVESLF_UPT()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            string sVSBONSUN  = string.Empty;
            string sProcedure = string.Empty;
            string sVESLAJET  = string.Empty;

            string sIPTIM = string.Empty;
            string sCHTIM = string.Empty;

            if (this.CBH01_VESLAJET.GetValue().ToString() != "")
            {
                sVESLAJET = this.CBH01_VESLAJET.GetValue().ToString().Substring(0, 2);
            }
            else
            {
                sVESLAJET = "";
            }

            sIPTIM = Set_Fill2(TXT01_VSIPTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSIPTIM2.GetValue().ToString());
            sCHTIM = Set_Fill2(TXT01_VSCHTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSCHTIM2.GetValue().ToString());

            // 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66LHF339",
                                    Get_Numeric(TXT01_VESLGLOS.GetValue().ToString()),				// G/T
                                    sVESLAJET.ToString(),                                           // 대리점
                                    Get_Numeric(Get_Date(this.MTB01_VSIPHANG2.GetValue().ToString().Trim())),	// 외항입항일자
                                    sIPTIM.ToString(),									            // 입항시간
                                    Get_Numeric(Get_Date(this.MTB01_VSCHDAT.GetValue().ToString().Trim())),   // 출항일자
                                    sCHTIM.ToString(),									            // 출항시간
                                    CBH01_VSVSGB.GetValue().ToString(),								// 선박구분
                                    CBH01_VSHMGB.GetValue().ToString(),								// 화물구분
                                    CBH01_VSJUBAN.GetValue().ToString(),							// 접안장소
                                    CBH01_VSCREW.GetValue().ToString().ToUpper(),					// 선원국적
                                    Get_Numeric(TXT01_VSCUSTOMS.GetValue().ToString()),				// 입항세관
                                    TXT01_VSBANIP.GetValue().ToString(),							// 반입근거번호
                                    TXT01_VSJUKHA.GetValue().ToString(),							// 적하목록번호
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                   // 작성사번
                                    Get_Date(DTP01_VSIPHANG.GetValue().ToString()),                 // 입항일자
                                    CBH01_VSBONSUN.GetValue().ToString().ToUpper()                  // 본선
                                    );

            this.DbConnector.ExecuteNonQuery();

            sVSBONSUN = this.CBH01_VSBONSUN.GetValue().ToString();

            if (sVSBONSUN != "PP1" && sVSBONSUN != "PP2" && sVSBONSUN != "PP3" && sVSBONSUN != "PP4" &&
                sVSBONSUN != "PP5" && sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3" && sVSBONSUN != "TK7")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_66GE8274",
                    this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                    this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                    fsVSJUBAN.ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    // 부두 접안파일 업데이트(-)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_66GE1276", "1",
                                                                fsVESLGLOS.ToString(),
                                                                TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                                                                fsVSJUBAN.ToString()
                                                                );
                    this.DbConnector.ExecuteNonQuery();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66GE8274",
                        this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                        this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                        this.CBH01_VSJUBAN.GetValue().ToString()
                        );

                    dt2 = this.DbConnector.ExecuteDataTable();

                    if (dt2.Rows.Count > 0)
                    {
                        // 수정
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_66LHH340",
                                                "1",
                                                fsVESLGLOS.ToString(),
                                                TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                                                CBH01_VSJUBAN.GetValue().ToString()
                                                );

                        this.DbConnector.ExecuteNonQuery();
                    }
                    else
                    {
                        // 등록
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_66LHM341",
                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                                                this.CBH01_VSJUBAN.GetValue().ToString(),
                                                "1",
                                                fsVESLGLOS.ToString(),
                                                TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                                );

                        this.DbConnector.ExecuteNonQuery();
                    }
                }
            }

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 입항관리 삭제
        private void UP_UTIVESLF_DEL()
        {
            DataTable dt = new DataTable();

            string sVSBONSUN = string.Empty;

            // 삭제 프로시저
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66GEE279",
                                    this.DTP01_VSIPHANG.GetValue().ToString(),
                                    this.CBH01_VSBONSUN.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            sVSBONSUN = this.CBH01_VSBONSUN.GetValue().ToString();

            if (sVSBONSUN != "PP1" && sVSBONSUN != "PP2" && sVSBONSUN != "PP3" && sVSBONSUN != "PP4" &&
                sVSBONSUN != "PP5" && sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3" && sVSBONSUN != "TK7")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_66GE8274",
                    this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                    this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                    fsVSJUBAN.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 부두 접안파일 업데이트(-)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_66GE1276", "1",
                                                                fsVESLGLOS.ToString(),
                                                                TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(0, 4),
                                                                this.DTP01_VSIPHANG.GetValue().ToString().Substring(4, 2),
                                                                fsVSJUBAN.ToString()
                                                                );

                    this.DbConnector.ExecuteNonQuery();
                }
            }

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Descriptoin : 선박사양관리 접안가능부두 업데이트
        private void UP_UTIVESSF_UPT()
        {
            string sVEWHARF1 = "N";
            string sVEWHARF2 = "N";
            string sVEWHARF3 = "N";
            string sVEWHARF4 = "N";

            string sVSBONSUN = this.CBH01_VSBONSUN.GetValue().ToString();

            if (sVSBONSUN != "AD5" && sVSBONSUN != "BR2" && sVSBONSUN != "BR3" && sVSBONSUN != "CON" &&
                sVSBONSUN != "CO0" && sVSBONSUN != "PPP" && sVSBONSUN != "PP1" && sVSBONSUN != "PP2" &&
                sVSBONSUN != "SA6" && sVSBONSUN != "SM8" && sVSBONSUN != "TKP" && sVSBONSUN != "TK0" &&
                sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3" && sVSBONSUN != "TK4" &&
                sVSBONSUN != "TK5" && sVSBONSUN != "TK7" && sVSBONSUN != "TST" && sVSBONSUN != "Z52" && 
                sVSBONSUN != "Z55" && sVSBONSUN != "111" && sVSBONSUN != "222")
            {
                if (this.CBH01_VSJUBAN.GetValue().ToString() == "1")
                {
                    sVEWHARF1 = "Y";
                    sVEWHARF2 = "Y";
                    sVEWHARF3 = "Y";
                    sVEWHARF4 = "N";
                }
                else if (this.CBH01_VSJUBAN.GetValue().ToString() == "2" || this.CBH01_VSJUBAN.GetValue().ToString() == "4")
                {
                    sVEWHARF1 = "N";
                    sVEWHARF2 = "Y";
                    sVEWHARF3 = "N";
                    sVEWHARF4 = "Y";
                }
                else if (this.CBH01_VSJUBAN.GetValue().ToString() == "3")
                {
                    sVEWHARF1 = "N";
                    sVEWHARF2 = "Y";
                    sVEWHARF3 = "Y";
                    sVEWHARF4 = "N";
                }
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B97GY543",
                                        sVEWHARF1,
                                        sVEWHARF2,
                                        sVEWHARF3,
                                        sVEWHARF4,
                                        sVSBONSUN
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
        }
        #endregion

        #region Description : 입항관리 스프레드
        private void FPS91_TY_S_UT_674DA548_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_MESSAGE1.SetValue("");

            this.DTP01_VSIPHANG.SetReadOnly(true);
            this.CBH01_VSBONSUN.SetReadOnly(true);

            this.DTP01_VSIPHANG.SetValue(this.FPS91_TY_S_UT_674DA548.GetValue("VSIPHANG").ToString());
            this.CBH01_VSBONSUN.SetValue(this.FPS91_TY_S_UT_674DA548.GetValue("VSBONSUN").ToString());

            // 입항관리 확인
            UP_UTIVESLF_RUN(this.DTP01_VSIPHANG.GetValue().ToString(), this.CBH01_VSBONSUN.GetValue().ToString());
        }
        #endregion

        #region Description : 입항관리 전체 - 스프레드
        private void FPS91_TY_S_UT_66LC6317_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_VSIPHANG.SetReadOnly(true);
            this.CBH01_VSBONSUN.SetReadOnly(true);

            this.DTP01_VSIPHANG.SetValue(this.FPS91_TY_S_UT_66LC6317.GetValue("VSIPHANG").ToString());
            this.CBH01_VSBONSUN.SetValue(this.FPS91_TY_S_UT_66LC6317.GetValue("VSBONSUN").ToString());

            // 입항관리 조회
            UP_UTIVESLF_TAB_SEARCH();

            // 입항관리 확인
            UP_UTIVESLF_RUN(this.DTP01_VSIPHANG.GetValue().ToString(), this.CBH01_VSBONSUN.GetValue().ToString());
        }
        #endregion

        #region Description : 입항관리 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsWK_GUBUN1.ToString() == "NEW")
            {
                // 선박사양관리 확인
                UP_UTIVESSF_RUN();                

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_66LC1314",
                    this.DTP01_VSIPHANG.GetValue().ToString(),
                    this.CBH01_VSBONSUN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.DTP01_VSIPHANG.Focus();

                    e.Successed = false;
                    return;
                }
            }

            string sVSBONSUN = string.Empty;

            sVSBONSUN = this.CBH01_VSBONSUN.GetValue().ToString();

            if (sVSBONSUN != "PP1" && sVSBONSUN != "PP2" && sVSBONSUN != "PP3" && sVSBONSUN != "PP4" &&
                sVSBONSUN != "PP5" && sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3" && sVSBONSUN != "TK7")
            {
                // 접안장소 
                if (Get_Date(this.CBH01_VSJUBAN.GetValue().ToString().Trim()) == "")
                {
                    this.ShowMessage("TY_M_UT_B1MD6443");
                    this.CBH01_VSJUBAN.Focus();

                    e.Successed = false;
                    return;
                }

                // 외항 입항일자 
                if (Get_Date(this.MTB01_VSIPHANG2.GetValue().ToString().Trim()) == "")
                {
                    this.ShowMessage("TY_M_UT_66LFT319");
                    this.MTB01_VSIPHANG2.Focus();

                    e.Successed = false;
                    return;
                }

                // 이안일자 
                if (Get_Date(this.MTB01_VSCHDAT.GetValue().ToString().Trim()) == "")
                {
                    this.ShowMessage("TY_M_UT_66LFT320");
                    this.MTB01_VSIPHANG2.Focus();

                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_66GDZ272",
                    this.DTP01_VSIPHANG.GetValue().ToString(),
                    this.CBH01_VSBONSUN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (TXT01_VSIPTIM1.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_66LFU321");
                        this.TXT01_VSIPTIM1.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (TXT01_VSIPTIM2.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_66LFU322");
                        this.TXT01_VSIPTIM2.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 이안일자
                    if (Get_Date(MTB01_VSCHDAT.GetValue().ToString()) != "" && TXT01_VSCHTIM1.GetValue().ToString() == "" && TXT01_VSCHTIM2.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_66LFV323");
                        this.TXT01_VSCHTIM1.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 이안일자
                    if (Get_Date(MTB01_VSCHDAT.GetValue().ToString()) == "" && TXT01_VSCHTIM1.GetValue().ToString() != "" && TXT01_VSCHTIM2.GetValue().ToString() != "")
                    {
                        this.ShowMessage("TY_M_UT_66LFV324");
                        this.MTB01_VSCHDAT.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 접안시간(시)
                if (Int64.Parse(Set_Fill2(TXT01_VSIPTIM1.GetValue().ToString())) > 24)
                {
                    this.ShowMessage("TY_M_UT_66LFV325");
                    this.TXT01_VSIPTIM1.Focus();

                    e.Successed = false;
                    return;
                }

                // 접안시간(분)
                if (Int64.Parse(Set_Fill2(TXT01_VSIPTIM2.GetValue().ToString())) > 59)
                {
                    this.ShowMessage("TY_M_UT_66LFV325");
                    this.TXT01_VSIPTIM2.Focus();

                    e.Successed = false;
                    return;
                }

                if (Int64.Parse(Get_Date(DTP01_VSIPHANG.GetValue().ToString())) > Int64.Parse(Get_Date(MTB01_VSCHDAT.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_UT_66LFV324");
                    this.MTB01_VSCHDAT.Focus();

                    e.Successed = false;
                    return;
                }

                // 이안시간(시)
                if (Int64.Parse(Set_Fill2(TXT01_VSCHTIM1.GetValue().ToString())) > 24)
                {
                    this.ShowMessage("TY_M_UT_66LFV323");
                    this.TXT01_VSCHTIM1.Focus();

                    e.Successed = false;
                    return;
                }

                // 이안시간(분)
                if (Int64.Parse(Set_Fill2(TXT01_VSCHTIM2.GetValue().ToString())) > 59)
                {
                    this.ShowMessage("TY_M_UT_66LFV323");
                    this.TXT01_VSCHTIM2.Focus();

                    e.Successed = false;
                    return;
                }
                string sIPTIM = string.Empty;
                string sCHTIM = string.Empty;
                sIPTIM = Set_Fill2(TXT01_VSIPTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSIPTIM2.GetValue().ToString());
                sCHTIM = Set_Fill2(TXT01_VSCHTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSCHTIM2.GetValue().ToString());

                if (Get_Date(DTP01_VSIPHANG.GetValue().ToString()) == Get_Date(MTB01_VSCHDAT.GetValue().ToString()))
                {
                    if (Int64.Parse(sIPTIM.ToString()) >= Int64.Parse(sCHTIM.ToString()))
                    {
                        this.ShowMessage("TY_M_UT_66LFV325");
                        this.TXT01_VSIPTIM1.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 황성환 대리 요청 이안일자 - 입항일자 > 한달 이상인 경우 체크 (2019-05-30)
                string sVSIPHANG = DTP01_VSIPHANG.GetString().ToString();
                string sVSCHDAT = MTB01_VSCHDAT.GetValue().ToString().Replace("-","");

                DateTime dt_VSIPHANG = Convert.ToDateTime(sVSIPHANG.Substring(0, 4) + "-" + sVSIPHANG.Substring(4, 2) + "-" + sVSIPHANG.Substring(6, 2));
                DateTime dt_VSCHDAT = Convert.ToDateTime(sVSCHDAT.Substring(0, 4) + "-" + sVSCHDAT.Substring(4, 2) + "-" + sVSCHDAT.Substring(6, 2));

                TimeSpan ts = dt_VSCHDAT - dt_VSIPHANG;
                if (ts.Days > 30)
                {
                    this.ShowCustomMessage("입항일자와 이안일자가 30일이상 차이납니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_VSCHDAT);
                    return;
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        //#region Description : 입항관리 수정 ProcessCheck
        //private void BTN61_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    DataTable dt = new DataTable();

        //    string sVSBONSUN = string.Empty;

        //    sVSBONSUN = this.CBH01_VSBONSUN.GetValue().ToString();

        //    if (sVSBONSUN != "PP1" && sVSBONSUN != "PP2" && sVSBONSUN != "PP3" && sVSBONSUN != "PP4" &&
        //        sVSBONSUN != "PP5" && sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3")
        //    {
        //        // 외항 입항일자 
        //        if (Get_Date(this.MTB01_VSIPHANG2.GetValue().ToString()) == "")
        //        {
        //            this.ShowMessage("TY_M_UT_66LFT319");
        //            this.MTB01_VSIPHANG2.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // 이안일자 
        //        if (Get_Date(this.MTB01_VSCHDAT.GetValue().ToString()) == "")
        //        {
        //            this.ShowMessage("TY_M_UT_66LFT320");
        //            this.MTB01_VSIPHANG2.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_UT_66GDZ272",
        //            this.DTP01_VSIPHANG.GetValue().ToString(),
        //            this.CBH01_VSBONSUN.GetValue().ToString()
        //            );

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            if (TXT01_VSIPTIM1.GetValue().ToString() == "")
        //            {
        //                this.ShowMessage("TY_M_UT_66LFU321");
        //                this.TXT01_VSIPTIM1.Focus();

        //                e.Successed = false;
        //                return;
        //            }

        //            if (TXT01_VSIPTIM2.GetValue().ToString() == "")
        //            {
        //                this.ShowMessage("TY_M_UT_66LFU322");
        //                this.TXT01_VSIPTIM2.Focus();

        //                e.Successed = false;
        //                return;
        //            }

        //            // 이안일자
        //            if (Get_Date(MTB01_VSCHDAT.GetValue().ToString()) != "" && TXT01_VSCHTIM1.GetValue().ToString() == "" && TXT01_VSCHTIM2.GetValue().ToString() == "")
        //            {
        //                this.ShowMessage("TY_M_UT_66LFV323");
        //                this.TXT01_VSCHTIM1.Focus();

        //                e.Successed = false;
        //                return;
        //            }

        //            // 이안일자
        //            if (Get_Date(MTB01_VSCHDAT.GetValue().ToString()) == "" && TXT01_VSCHTIM1.GetValue().ToString() != "" && TXT01_VSCHTIM2.GetValue().ToString() != "")
        //            {
        //                this.ShowMessage("TY_M_UT_66LFV324");
        //                this.MTB01_VSCHDAT.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 접안시간(시)
        //        if (Int64.Parse(Set_Fill2(TXT01_VSIPTIM1.GetValue().ToString())) > 24)
        //        {
        //            this.ShowMessage("TY_M_UT_66LFV325");
        //            this.TXT01_VSIPTIM1.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // 접안시간(분)
        //        if (Int64.Parse(Set_Fill2(TXT01_VSIPTIM2.GetValue().ToString())) > 59)
        //        {
        //            this.ShowMessage("TY_M_UT_66LFV325");
        //            this.TXT01_VSIPTIM2.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (Int64.Parse(Get_Date(DTP01_VSIPHANG.GetValue().ToString())) > Int64.Parse(Get_Date(MTB01_VSCHDAT.GetValue().ToString())))
        //        {
        //            this.ShowMessage("TY_M_UT_66LFV324");
        //            this.MTB01_VSCHDAT.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // 이안시간(시)
        //        if (Int64.Parse(Set_Fill2(TXT01_VSCHTIM1.GetValue().ToString())) > 24)
        //        {
        //            this.ShowMessage("TY_M_UT_66LFV323");
        //            this.TXT01_VSCHTIM1.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // 이안시간(분)
        //        if (Int64.Parse(Set_Fill2(TXT01_VSCHTIM2.GetValue().ToString())) > 59)
        //        {
        //            this.ShowMessage("TY_M_UT_66LFV323");
        //            this.TXT01_VSCHTIM2.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //        string sIPTIM = string.Empty;
        //        string sCHTIM = string.Empty;
        //        sIPTIM = Set_Fill2(TXT01_VSIPTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSIPTIM2.GetValue().ToString());
        //        sCHTIM = Set_Fill2(TXT01_VSCHTIM1.GetValue().ToString()) + Set_Fill2(TXT01_VSCHTIM2.GetValue().ToString());

        //        if (Get_Date(DTP01_VSIPHANG.GetValue().ToString()) == Get_Date(MTB01_VSCHDAT.GetValue().ToString()))
        //        {
        //            if (Int64.Parse(sIPTIM.ToString()) >= Int64.Parse(sCHTIM.ToString()))
        //            {
        //                this.ShowMessage("TY_M_UT_66LFV325");
        //                this.TXT01_VSIPTIM1.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }
        //    }

        //    // 수정하시겠습니까?
        //    if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }
        //}
        //#endregion

        #region Description : 입항관리 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66GDZ272",
                this.DTP01_VSIPHANG.GetValue().ToString(),
                this.CBH01_VSBONSUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_66GE0273");
                this.DTP01_VSIPHANG.Focus();

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

        #region Description : 입항관리 필드 클리어
        private void UP_UTIVESLF_FieldClear()
        {
            this.DTP01_VSIPHANG.SetReadOnly(false);
            this.CBH01_VSBONSUN.SetReadOnly(false);

            this.DTP01_VSIPHANG.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.MTB01_VSIPHANG2.SetValue("");
            this.MTB01_VSCHDAT.SetValue("");

            this.CBH01_VSBONSUN.SetValue("");

            // G/T량
            this.TXT01_VESLGLOS.SetValue("");
            // 대리점코드 + 대리
            this.CBH01_VESLAJET.SetValue("");
            // 선박길이
            this.TXT01_VESLLOGN.SetValue("");
            // 선박국적 
            this.CBH01_VESLFLAG.SetValue("");
            // 호출번호 
            this.TXT01_VESLCALL.SetValue("");
            // 선박번호
            this.TXT01_VEBONSON.SetValue("");
            // 선원국적코드 
            this.CBH01_VSCREW.SetValue("");
            // 선원국적명
            this.CBH01_VSCREW.SetValue("");
            // 접안코드
            this.CBH01_VSJUBAN.SetValue("");
            // 외항입항일
            this.TXT01_VSIPTIM1.SetValue("");
            this.TXT01_VSIPTIM2.SetValue("");
            // 이안시간
            this.TXT01_VSCHTIM1.SetValue("");
            this.TXT01_VSCHTIM2.SetValue("");
            // 선박구분
            this.CBH01_VSVSGB.SetValue("");
            // 화물구분
            this.CBH01_VSHMGB.SetValue("");
            // 입항세관
            this.TXT01_VSCUSTOMS.SetValue("");
            // 반입근거번호
            this.TXT01_VSBANIP.SetValue("");
            // 적하목록번호
            this.TXT01_VSJUKHA.SetValue(""); 
        }
        #endregion

        #region Description : 입항관리 디스플레이
        private void UP_UTIVESLF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible  = true;
                //this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible  = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible  = true;
                //this.BTN61_EDIT.Visible = true;
                this.BTN61_REM.Visible  = true;
            }
            else
            {
                this.BTN61_SAV.Visible  = false;
                //this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible  = false;
            }
        }
        #endregion

        #region Description : 적하목록 텍스트박스 이벤트
        private void TXT01_VSJUKHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
                //else if (this.BTN61_EDIT.Visible == true)
                //{
                //    SetFocus(this.BTN61_EDIT);
                //}
            }
        }
        #endregion

        #endregion

        #region Description : 입고화물관리

        #region Description : 입고화물관리 클리어 버튼
        private void BTN62_UTTCLEAR_Click(object sender, EventArgs e)
        {
            // 조회
            string sSTDATE    = string.Empty;
            string sEDDATE    = string.Empty;
            string sSBONSUN   = string.Empty;
            string sSHWAJU    = string.Empty;
            string sSHWAMUL   = string.Empty;

            // 입고화물관리 내용
            string sIPHANG    = string.Empty;
            string sBONSUN    = string.Empty;
            string sHWAJU     = string.Empty;
            string sHWAMUL    = string.Empty;
            string sCMBANIL   = string.Empty;
            string sCMBOGODAT = string.Empty;


            sSTDATE    = this.DTP01_STIPHANG.GetValue().ToString();
            sEDDATE    = this.DTP01_EDIPHANG.GetValue().ToString();
            sSBONSUN   = this.CBH01_SBONSUN.GetValue().ToString();
            sSHWAJU    = this.CBH01_SHWAJU.GetValue().ToString();
            sSHWAMUL   = this.CBH01_SHWAMUL.GetValue().ToString();

            sIPHANG    = this.DTP01_CMIPHANG.GetValue().ToString();
            sBONSUN    = this.CBH01_CMBONSUN.GetValue().ToString();
            sHWAJU     = this.CBH01_CMHWAJU.GetValue().ToString();
            sHWAMUL    = this.CBH01_CMHWAMUL.GetValue().ToString();
            //sCMBANIL   = this.DTP01_CMBANIL.GetValue().ToString();
            sCMBOGODAT = this.DTP01_CMBOGODAT.GetValue().ToString();

            UP_UTICMDTF_FieldClear("CLEAR");
            //this.Initialize_Controls("01");

            this.DTP01_STIPHANG.SetValue(sSTDATE.ToString());
            this.DTP01_EDIPHANG.SetValue(sEDDATE.ToString());
            this.CBH01_SBONSUN.SetValue(sSBONSUN.ToString());
            this.CBH01_SHWAJU.SetValue(sSHWAJU.ToString());
            this.CBH01_SHWAMUL.SetValue(sSHWAMUL.ToString());

            this.DTP01_CMIPHANG.SetValue(sIPHANG.ToString());
            this.CBH01_CMBONSUN.SetValue(sBONSUN.ToString());
            this.CBH01_CMHWAJU.SetValue(sHWAJU.ToString());
            this.CBH01_CMHWAMUL.SetValue(sHWAMUL.ToString());
            //this.DTP01_CMBANIL.SetValue(sCMBANIL.ToString());
            this.DTP01_CMBOGODAT.SetValue(sCMBOGODAT.ToString());

            this.FPS91_TY_S_UT_674DV550.Initialize();

            UP_UTICMDTF_SEARCH();

            UP_UTICMDTF_BTN_DISPLAY("NEW");

            this.SetFocus(this.CBH01_CMHWAJU.CodeText);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            UP_UTICMDTF_FieldClear("NEW");

            fsWK_GUBUN2 = "NEW";

            fsCMHMDATE = "0";

            UP_UTICMDTF_BTN_DISPLAY(fsWK_GUBUN2);

            this.SetFocus(this.CBH01_CMHWAJU.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN2.ToString() == "NEW")
            {
                UP_UTICMDTF_SAV();

                this.DTP01_CMIPHANG.SetReadOnly(true);
                this.CBH01_CMBONSUN.SetReadOnly(true);
                this.CBH01_CMHWAJU.SetReadOnly(true);
                this.CBH01_CMHWAMUL.SetReadOnly(true);
            }
            else if (fsWK_GUBUN2.ToString() == "UPT")
            {
                UP_UTICMDTF_UPT();
            }

            // 입고화물관리 조회
            UP_UTICMDTF_TAB_SEARCH();

            // 입고화물관리 전체 조회
            UP_UTICMDTF_SEARCH();

            UP_UTICMDTF_BTN_DISPLAY("TAB");

            // 값 저장
            UP_SET_Cookie2(this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString(),
                           this.CBH01_CMHWAJU.GetValue().ToString(), this.CBH01_CMHWAMUL.GetValue().ToString());
        }
        #endregion

        //#region Description : 수정 버튼
        //private void BTN62_EDIT_Click(object sender, EventArgs e)
        //{
        //    UP_UTICMDTF_UPT();

        //    // 입고화물관리 조회
        //    UP_UTICMDTF_TAB_SEARCH();

        //    // 값 저장
        //    UP_SET_Cookie2(this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString(),
        //                   this.CBH01_CMHWAJU.GetValue().ToString(), this.CBH01_CMHWAMUL.GetValue().ToString());

        //}
        //#endregion

        #region Description : 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            UP_UTICMDTF_DEL();



            this.DTP01_CMIPHANG.SetReadOnly(false);
            this.CBH01_CMBONSUN.SetReadOnly(false);
            this.CBH01_CMHWAJU.SetReadOnly(false);
            this.CBH01_CMHWAMUL.SetReadOnly(false);

            // 입항관리 조회
            UP_UTICMDTF_TAB_SEARCH();

            // 입고화물관리 전체 조회
            UP_UTICMDTF_SEARCH();

            UP_UTICMDTF_BTN_DISPLAY("TAB");

            //UP_UTICMDTF_FieldClear();

            this.DTP01_CMIPHANG.Focus();
        }
        #endregion

        #region Description : 입고화물관리 TAB 조회
        private void UP_UTICMDTF_TAB_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66SAD412",
                this.DTP01_CMIPHANG.GetValue().ToString(),
                this.CBH01_CMBONSUN.GetValue().ToString(),
                this.CBH01_CMHWAJU.GetValue().ToString(),
                this.CBH01_CMHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_674DV550.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_UT_674DV550.SetValue(dt);
            }
        }
        #endregion

        #region Description : 입고화물관리 전체 조회
        private void UP_UTICMDTF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66NFT376",
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString(),
                this.CBH01_SBONSUN.GetValue().ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_66NGJ381.SetValue(dt);
        }
        #endregion

        #region Description : 입고화물관리 확인
        private void UP_UTICMDTF_RUN()
        {
            // FOCUS
            Timer tmr = new Timer();

            tmr.Tick += delegate
            {
                tmr.Stop();
                this.SetFocus(this.DTP01_CMBANIL);
            };

            tmr.Interval = 100;
            tmr.Start();

            fsCMHMDATE = "0";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66SAJ413",
                this.DTP01_CMIPHANG.GetValue().ToString(),
                this.CBH01_CMBONSUN.GetValue().ToString(),
                this.CBH01_CMHWAJU.GetValue().ToString(),
                this.CBH01_CMHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsWK_GUBUN2 = "UPT";

                UP_UTICMDTF_BTN_DISPLAY(fsWK_GUBUN2);

                fsCMHMDATE = dt.Rows[0]["CMHMDATE"].ToString();
            }

            // 값 저장
            UP_SET_Cookie2(this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString(),
                           this.CBH01_CMHWAJU.GetValue().ToString(),  this.CBH01_CMHWAMUL.GetValue().ToString());
        }
        #endregion

        #region Description : 입고화물관리 저장
        private void UP_UTICMDTF_SAV()
        {
            string sHOSTR    = string.Empty;
            string sHOEND    = string.Empty;
            string sPUSTR    = string.Empty;
            string sPUEND    = string.Empty;
            string sCMHYUKGB = string.Empty;

            sHOSTR = Set_Fill2(this.TXT01_CMHOSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOSTR2.GetValue().ToString());
            sHOEND = Set_Fill2(this.TXT01_CMHOEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOEND2.GetValue().ToString());
            sPUSTR = Set_Fill2(this.TXT01_CMPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUSTR2.GetValue().ToString());
            sPUEND = Set_Fill2(this.TXT01_CMPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUEND2.GetValue().ToString());

            if (this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() == "NO")
            {
                sCMHYUKGB = this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper();
            }
            else
            {
                sCMHYUKGB = this.TXT01_CMHYUKGB.GetValue().ToString();
            }

            // 등록
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_66SEW433",
            this.DbConnector.Attach("TY_P_UT_91EFK497",
                                    Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()),                       // 입항일자    
							        this.CBH01_CMBONSUN.GetValue().ToString().ToUpper(), 			           // 본선        
							        this.CBH01_CMHWAJU.GetValue().ToString().ToUpper(),                        // 화주        
							        this.CBH01_CMHWAMUL.GetValue().ToString().ToUpper(),                       // 화물        
							        Get_Date(this.DTP01_CMBANIL.GetValue().ToString()),                        // 입고일자    
							        this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper(),                       // 입고구분    
							        this.CBO01_CMCUSTGB.GetValue().ToString().ToUpper(),                       // 선상구분    
							        Get_Numeric(this.TXT01_CMBLQTY.GetValue().ToString()),                     // B/L         
							        Get_Numeric(this.TXT01_CMOBQTY.GetValue().ToString()),                     // O/B         
							        Get_Numeric(this.TXT01_CMSHQTY.GetValue().ToString()),                     // SHORE량     
							        Get_Numeric(this.TXT01_CMBBQTY.GetValue().ToString()),                     // BBLS량      
                                    Get_Numeric(this.TXT01_CMKLQTY.GetValue().ToString()),                     // K/L량       
							        Get_Numeric(this.TXT01_CMHAAM.GetValue().ToString()),                      // 하역비      
							        Get_Numeric(this.TXT01_CMIPAM.GetValue().ToString()),                      // 화물료      
							        Get_Date(Get_Numeric(this.MTB01_CMMAECH.GetValue().ToString())),           // 청구일자    
							        sHOSTR.ToString(),                                                         // HOST시작시간
							        sHOEND.ToString(),                                                         // HOST종료시간
							        sPUSTR.ToString(),                                                         // PUMP시작시간
							        sPUEND.ToString(),                                                         // PUMP종료시간
							        Get_Numeric(this.TXT01_CMTONH.GetValue().ToString()),                      // TON/H       
							        this.CBH01_CMINGUB.GetValue().ToString(),                                  // 보험등급    
							        Get_Numeric(this.TXT01_CMINGONG.GetValue().ToString()),                    // 보험공제일수
							        this.TXT01_CMINNO.GetValue().ToString(),                                   // 보험번호    
							        Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString()),                      // 보험반입일자
							        this.CBH01_CMBANSG.GetValue().ToString(),                                  // 보험반입사고
							        Set_TankNo(this.TXT01_CMTANO1.GetValue().ToString()),                      // 탱크1       
							        Set_TankNo(this.TXT01_CMTANO2.GetValue().ToString()),                      // 탱크2       
							        Set_TankNo(this.TXT01_CMTANO3.GetValue().ToString()),                      // 탱크3       
							        Set_TankNo(this.TXT01_CMTANO4.GetValue().ToString()),                      // 탱크4       
							        Set_TankNo(this.TXT01_CMTANO5.GetValue().ToString()),                      // 탱크5       
							        Set_TankNo(this.TXT01_CMTANO6.GetValue().ToString()),                      // 탱크6       
							        Set_TankNo(this.TXT01_CMTANO7.GetValue().ToString()),                      // 탱크7       
							        Set_TankNo(this.TXT01_CMTANO8.GetValue().ToString()),                      // 탱크8       
							        Set_TankNo(this.TXT01_CMTANO9.GetValue().ToString()),                      // 탱크9       
							        Set_TankNo(this.TXT01_CMTANO10.GetValue().ToString()),                     // 탱크10      
                                    this.CBO01_CMPUMCODE.GetValue().ToString().Substring(0, 2).ToString(),     // 품목코드    
							        sCMHYUKGB.ToString(),                                                      // 취급월      
							        Get_Numeric(this.TXT01_CMHYQTY.GetValue().ToString()),                     // 취급량      
                                    Get_Numeric(this.TXT01_CMSEBBLS.GetValue().ToString()),                    // 보안료BBLS량      
                                    Get_Numeric(this.TXT01_CMSETRANSBBLS.GetValue().ToString()),               // 환적화물BBLS량      
                                    Get_Numeric(this.TXT01_CMSEAMT.GetValue().ToString()),                     // 항만시설보안료      
							        TYUserInfo.EmpNo.ToString().Trim().ToUpper()                               // 작성사번    
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }                                                                                                                             
        #endregion

        #region Description : 입고화물관리 수정
        private void UP_UTICMDTF_UPT()
        {
            string sHOSTR = string.Empty;
            string sHOEND = string.Empty;
            string sPUSTR = string.Empty;
            string sPUEND = string.Empty;
            string sCMHYUKGB = string.Empty;

            sHOSTR = Set_Fill2(this.TXT01_CMHOSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOSTR2.GetValue().ToString());
            sHOEND = Set_Fill2(this.TXT01_CMHOEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOEND2.GetValue().ToString());
            sPUSTR = Set_Fill2(this.TXT01_CMPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUSTR2.GetValue().ToString());
            sPUEND = Set_Fill2(this.TXT01_CMPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUEND2.GetValue().ToString());

            if (this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() == "NO")
            {
                sCMHYUKGB = this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper();
            }
            else
            {
                sCMHYUKGB = this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper();
            }

            // 수정
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_66SEZ434",
            this.DbConnector.Attach("TY_P_UT_91EFM498",
                                    Get_Date(this.DTP01_CMBANIL.GetValue().ToString()),                        // 입고일자    
                                    this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper(),                       // 입고구분    
                                    this.CBO01_CMCUSTGB.GetValue().ToString().ToUpper(),                       // 선상구분    
                                    Get_Numeric(this.TXT01_CMBLQTY.GetValue().ToString()),                     // B/L         
                                    Get_Numeric(this.TXT01_CMOBQTY.GetValue().ToString()),                     // O/B         
                                    Get_Numeric(this.TXT01_CMSHQTY.GetValue().ToString()),                     // SHORE량     
                                    Get_Numeric(this.TXT01_CMBBQTY.GetValue().ToString()),                     // BBLS량      
                                    Get_Numeric(this.TXT01_CMKLQTY.GetValue().ToString()),                     // K/L량       
                                    Get_Numeric(this.TXT01_CMHAAM.GetValue().ToString()),                      // 하역비      
                                    Get_Numeric(this.TXT01_CMIPAM.GetValue().ToString()),                      // 화물료      
                                    Get_Date(Get_Numeric(this.MTB01_CMMAECH.GetValue().ToString())),           // 청구일자    
                                    sHOSTR.ToString(),                                                         // HOST시작시간
                                    sHOEND.ToString(),                                                         // HOST종료시간
                                    sPUSTR.ToString(),                                                         // PUMP시작시간
                                    sPUEND.ToString(),                                                         // PUMP종료시간
                                    Get_Numeric(this.TXT01_CMTONH.GetValue().ToString()),                      // TON/H       
                                    this.CBH01_CMINGUB.GetValue().ToString(),                                  // 보험등급    
                                    Get_Numeric(this.TXT01_CMINGONG.GetValue().ToString()),                    // 보험공제일수
                                    this.TXT01_CMINNO.GetValue().ToString(),                                   // 보험번호    
                                    Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString()),                      // 보험반입일자
                                    this.CBH01_CMBANSG.GetValue().ToString(),                                  // 보험반입사고
                                    Set_TankNo(this.TXT01_CMTANO1.GetValue().ToString()),                      // 탱크1       
                                    Set_TankNo(this.TXT01_CMTANO2.GetValue().ToString()),                      // 탱크2       
                                    Set_TankNo(this.TXT01_CMTANO3.GetValue().ToString()),                      // 탱크3       
                                    Set_TankNo(this.TXT01_CMTANO4.GetValue().ToString()),                      // 탱크4       
                                    Set_TankNo(this.TXT01_CMTANO5.GetValue().ToString()),                      // 탱크5       
                                    Set_TankNo(this.TXT01_CMTANO6.GetValue().ToString()),                      // 탱크6       
                                    Set_TankNo(this.TXT01_CMTANO7.GetValue().ToString()),                      // 탱크7       
                                    Set_TankNo(this.TXT01_CMTANO8.GetValue().ToString()),                      // 탱크8       
                                    Set_TankNo(this.TXT01_CMTANO9.GetValue().ToString()),                      // 탱크9       
                                    Set_TankNo(this.TXT01_CMTANO10.GetValue().ToString()),                     // 탱크10      
                                    this.CBO01_CMPUMCODE.GetValue().ToString().Substring(0, 2).ToString(),     // 품목코드    
                                    sCMHYUKGB.ToString(),                                                      // 취급월      
                                    Get_Numeric(this.TXT01_CMHYQTY.GetValue().ToString()),                     // 취급량  
                                    Get_Numeric(this.TXT01_CMSEBBLS.GetValue().ToString()),                    // 보안료BBLS량      
                                    Get_Numeric(this.TXT01_CMSETRANSBBLS.GetValue().ToString()),               // 환적화물BBLS량      
                                    Get_Numeric(this.TXT01_CMSEAMT.GetValue().ToString()),                     // 항만시설보안료 
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                              // 작성사번
                                    Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()),                       // 입항일자
                                    this.CBH01_CMBONSUN.GetValue().ToString().ToUpper(), 			           // 본선        
                                    this.CBH01_CMHWAJU.GetValue().ToString().ToUpper(),                        // 화주        
                                    this.CBH01_CMHWAMUL.GetValue().ToString().ToUpper()                        // 화물        
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 입고화물관리 삭제
        private void UP_UTICMDTF_DEL()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66SF2435", Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()), // 입항일자
                                                        this.CBH01_CMBONSUN.GetValue().ToString().ToUpper(), // 본선
                                                        this.CBH01_CMHWAJU.GetValue().ToString().ToUpper(),  // 화주
                                                        this.CBH01_CMHWAMUL.GetValue().ToString().ToUpper()  // 화물
                                                        );

            this.DbConnector.Attach("TY_P_UT_81HDD477", Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()), // 입항일자
                                                        this.CBH01_CMBONSUN.GetValue().ToString().ToUpper(), // 본선
                                                        this.CBH01_CMHWAJU.GetValue().ToString().ToUpper(),  // 화주
                                                        this.CBH01_CMHWAMUL.GetValue().ToString().ToUpper()  // 화물
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 입고화물관리 이벤트
        private void FPS91_TY_S_UT_674DV550_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_MESSAGE2.SetValue("");

            this.DTP01_CMIPHANG.SetReadOnly(true);
            this.CBH01_CMBONSUN.SetReadOnly(true);
            this.CBH01_CMHWAJU.SetReadOnly(true);
            this.CBH01_CMHWAMUL.SetReadOnly(true);

            this.DTP01_CMIPHANG.SetValue(this.FPS91_TY_S_UT_674DV550.GetValue("CMIPHANG").ToString());
            this.CBH01_CMBONSUN.SetValue(this.FPS91_TY_S_UT_674DV550.GetValue("CMBONSUN").ToString());
            this.CBH01_CMHWAJU.SetValue(this.FPS91_TY_S_UT_674DV550.GetValue("CMHWAJU").ToString());
            this.CBH01_CMHWAMUL.SetValue(this.FPS91_TY_S_UT_674DV550.GetValue("CMHWAMUL").ToString());

            // 입고화물관리 확인
            UP_UTICMDTF_RUN();
        }
        #endregion

        #region Description : 입고화물관리 전체 스프레드 이벤트
        private void FPS91_TY_S_UT_66NGJ381_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_CMIPHANG.SetReadOnly(true);
            this.CBH01_CMBONSUN.SetReadOnly(true);
            this.CBH01_CMHWAJU.SetReadOnly(true);
            this.CBH01_CMHWAMUL.SetReadOnly(true);

            this.DTP01_CMIPHANG.SetValue(this.FPS91_TY_S_UT_66NGJ381.GetValue("CMIPHANG").ToString());
            this.CBH01_CMBONSUN.SetValue(this.FPS91_TY_S_UT_66NGJ381.GetValue("CMBONSUN").ToString());
            this.CBH01_CMHWAJU.SetValue(this.FPS91_TY_S_UT_66NGJ381.GetValue("CMHWAJU").ToString());
            this.CBH01_CMHWAMUL.SetValue(this.FPS91_TY_S_UT_66NGJ381.GetValue("CMHWAMUL").ToString());

            // 입고화물관리 전체 조회
            UP_UTICMDTF_TAB_SEARCH();

            // 입고화물관리 확인
            UP_UTICMDTF_RUN();
        }
        #endregion

        #region Description : 입항조회 버튼
        private void BTN62_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            TYUTGB003S popup = new TYUTGB003S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_CMIPHANG.SetValue(popup.fsIPHANG); // 입항일자
                this.CBH01_CMBONSUN.SetValue(popup.fsBONSUN); // 본선명

                // 값 저장
                UP_SET_Cookie2(this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString(),
                               this.CBH01_CMHWAJU.GetValue().ToString(),  this.CBH01_CMHWAMUL.GetValue().ToString());

                SetFocus(this.CBH01_CMHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 선박 입/출항조회 버튼
        private void BTN62_UTTCODEHELP6_Click(object sender, EventArgs e)
        {
            TYUTGB020S popup = new TYUTGB020S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_CMBANIL.SetValue(popup.fsCKJBIPHANG); // 반입일자

                this.TXT01_CMHOSTR1.SetValue(popup.fsCKHOSTHH);
                this.TXT01_CMHOSTR2.SetValue(popup.fsCKHOSTMM);
                this.TXT01_CMHOEND1.SetValue(popup.fsCKHOENHH);
                this.TXT01_CMHOEND2.SetValue(popup.fsCKHOENMM);

                this.TXT01_CMPUSTR1.SetValue(popup.fsCKPUSTHH);
                this.TXT01_CMPUSTR2.SetValue(popup.fsCKPUSTMM);
                this.TXT01_CMPUEND1.SetValue(popup.fsCKPUENHH);
                this.TXT01_CMPUEND2.SetValue(popup.fsCKPUENMM);

                this.TXT01_CMBLQTY.SetValue(popup.fsCKBLQTY);
                this.TXT01_CMOBQTY.SetValue(popup.fsCKOBQTY);
                this.TXT01_CMTONH.SetValue(popup.fsCKTONH);

                SetFocus(this.TXT01_CMBLQTY);
            }
        }
        #endregion

        #region Description : 입고화물관리 저장 ProcessCheck
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.TXT01_MESSAGE2.SetValue("");

            fsVSHMGB = "";

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            if (fsWK_GUBUN2.ToString() == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_66SAJ413",
                    this.DTP01_CMIPHANG.GetValue().ToString(),
                    this.CBH01_CMBONSUN.GetValue().ToString(),
                    this.CBH01_CMHWAJU.GetValue().ToString(),
                    this.CBH01_CMHWAMUL.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.CBH01_CMHWAJU.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (Get_Date(this.DTP01_CMBANIL.GetValue().ToString()) == "")
            {
                this.ShowMessage("TY_M_UT_7B3BM931");
                this.DTP01_CMBANIL.Focus();

                e.Successed = false;
                return;
            }

            if (Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString()) == "")
            {
                this.DTP01_CMBOGODAT.SetValue(Set_Date(this.DTP01_CMBANIL.GetValue().ToString()));
            }

            if (this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() != "NO" && this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() != "")
            {
                if (this.TXT01_CMHYUKGB.GetValue().ToString().Length != 6)
                {
                    this.ShowMessage("TY_M_UT_66SD2414");
                    this.TXT01_CMHYUKGB.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "1" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "2" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "3" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "4" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "5" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "6" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "7" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "8" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "9" &&
                           this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "0")
                        {
                            this.ShowMessage("TY_M_UT_66SD3415");
                            this.TXT01_CMHYUKGB.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                if (this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper() == "S")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_CMHYQTY.GetValue().ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_UT_66SD4416");
                        this.TXT01_CMHYQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBO01_CMIPGOGB.GetValue().ToString() == "")
                {
                    this.TXT01_CMHYQTY.SetValue(this.TXT01_CMSHQTY.GetValue().ToString());
                }
            }




            if (this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() == "NO")
            {
                this.TXT01_CMHYQTY.SetValue("0");
            }

            if (this.CBO01_CMCUSTGB.GetValue().ToString().ToUpper() == "Y")
            {
                if (Get_Numeric(this.TXT01_CMOBQTY.GetValue().ToString()) == "0")
                {
                    this.ShowMessage("TY_M_UT_66SDB417");
                    this.TXT01_CMOBQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 입고구분
            if (this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper() == "S" && this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper() == "T")
            {
                if (Get_Numeric(Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString().Substring(0, 6).ToString()).Trim()) != "0")
                {
                    if (Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString()) != Get_Date(this.DTP01_CMBANIL.GetValue().ToString()))
                    {
                        this.ShowMessage("TY_M_UT_66SDD418");
                        this.DTP01_CMBANIL.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_UT_66SDD419");
                    this.DTP01_CMBOGODAT.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                string sHOSTR = string.Empty;
                string sHOEND = string.Empty;
                string sPUSTR = string.Empty;
                string sPUEND = string.Empty;
                
                sHOSTR = Set_Fill2(this.TXT01_CMHOSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOSTR2.GetValue().ToString());
                sHOEND = Set_Fill2(this.TXT01_CMHOEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOEND2.GetValue().ToString());
                sPUSTR = Set_Fill2(this.TXT01_CMPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUSTR2.GetValue().ToString());
                sPUEND = Set_Fill2(this.TXT01_CMPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUEND2.GetValue().ToString());

                // HOSE 작업시작시간
                if (Int32.Parse(sHOSTR) > 2459)
                {
                    this.ShowMessage("TY_M_UT_66SDE420");
                    this.TXT01_CMHOSTR1.Focus();

                    e.Successed = false;
                    return;
                }

                // HOSE 작업종료시간
                if (Int32.Parse(sHOEND) > 2459)
                {
                    this.ShowMessage("TY_M_UT_66SDE421");
                    this.TXT01_CMHOEND1.Focus();

                    e.Successed = false;
                    return;
                }

                // PUMP 작업시작시간
                if (Int32.Parse(sPUSTR) > 2459)
                {
                    this.ShowMessage("TY_M_UT_66SDE422");
                    this.TXT01_CMPUSTR1.Focus();

                    e.Successed = false;
                    return;
                }

                // PUMP 작업종료시간
                if (Int32.Parse(sPUEND) > 2459)
                {
                    this.ShowMessage("TY_M_UT_66SDF423");
                    this.TXT01_CMPUEND1.Focus();

                    e.Successed = false;
                    return;
                }

                // 보험등급
                if (this.CBH01_CMINGUB.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_66SDF424");
                    SetFocus(this.CBH01_CMINGUB.CodeText);

                    e.Successed = false;
                    return;
                }
                

                // 탱크번호
                if (this.TXT01_CMTANO1.GetValue().ToString() == "" && this.TXT01_CMTANO2.GetValue().ToString() == "" && this.TXT01_CMTANO3.GetValue().ToString() == "" &&
                    this.TXT01_CMTANO4.GetValue().ToString() == "" && this.TXT01_CMTANO5.GetValue().ToString() == "" && this.TXT01_CMTANO6.GetValue().ToString() == "" &&
                    this.TXT01_CMTANO7.GetValue().ToString() == "" && this.TXT01_CMTANO8.GetValue().ToString() == "" && this.TXT01_CMTANO9.GetValue().ToString() == "" &&
                    this.TXT01_CMTANO10.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_66SDG425");
                    this.TXT01_CMTANO1.Focus();

                    e.Successed = false;
                    return;
                }

                // 탱크번호1
                if (this.TXT01_CMTANO1.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO1.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO1.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호2
                if (this.TXT01_CMTANO2.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO2.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO2.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호3
                if (this.TXT01_CMTANO3.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO3.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO3.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호4
                if (this.TXT01_CMTANO4.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO4.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO4.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호5
                if (this.TXT01_CMTANO5.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO5.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO5.Focus();

                        e.Successed = false;
                        return;
                    }
                }


                // 탱크번호6
                if (this.TXT01_CMTANO6.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO6.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO6.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호7
                if (this.TXT01_CMTANO7.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO7.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO7.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호8
                if (this.TXT01_CMTANO8.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO8.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO8.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호9
                if (this.TXT01_CMTANO9.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO9.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO9.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 탱크번호10
                if (this.TXT01_CMTANO10.GetValue().ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_66SDH426",
                        this.TXT01_CMTANO10.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66SDG425");
                        this.TXT01_CMTANO10.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 반입사고
                if (this.CBH01_CMBANSG.GetValue().ToString().ToUpper() == "")
                {
                    this.ShowMessage("TY_M_UT_66SDJ427");
                    this.CBH01_CMBANSG.Focus();

                    e.Successed = false;
                    return;
                }

                if (Get_Numeric(Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString().Substring(0, 6).ToString()).Trim()) != "0")
                {
                    if (Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString()) != Get_Date(this.DTP01_CMBANIL.GetValue().ToString()))
                    {
                        // 20180115 고지파트 박동근 차장 수정요청
                        // 수정시 입고일자와 반입일자가 일치하지 않을 경우
                        // 반입일자와 입고일자 동일하게 되도록 요청

                        this.DTP01_CMBOGODAT.SetValue(this.DTP01_CMIPHANG.GetValue().ToString());

                        //this.ShowMessage("TY_M_UT_66SDD418");
                        //this.DTP01_CMBANIL.Focus();

                        //e.Successed = false;
                        //return;
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_UT_66SDD419");
                    this.DTP01_CMBOGODAT.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_CMKLQTY.GetValue().ToString())) != 0)
            {
                string sKESAN = string.Empty;

                sKESAN =
                    (
                    double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                    / double.Parse(Set_Numeric2(this.TXT01_SVMTQTY.GetValue().ToString(), 3))
                    * double.Parse(Set_Numeric2(this.TXT01_SVKLQTY.GetValue().ToString(), 3))
                    ).ToString("0.00000000");

                sKESAN =
                    (
                    ((double.Parse(this.TXT01_CMKLQTY.GetValue().ToString())
                    / 0.158984) * 100)).ToString();

                sKESAN = UP_DotDelete(sKESAN);

                sKESAN = Convert.ToString(double.Parse(sKESAN) / 100);

                this.TXT01_CMBBQTY.SetValue(sKESAN);
            }

            // 입항 파일의 화물 구분 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66SDL428",
                Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()),
                this.CBH01_CMBONSUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_66SDM429");
                this.DTP01_CMIPHANG.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                fsVSHMGB = dt.Rows[0][0].ToString();
            }

            // 선박입항파일의 접안시간 및 이안시간을 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66SDO430",
                Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()),
                this.CBH01_CMBONSUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_66SDP431");
                this.DTP01_CMIPHANG.Focus();

                e.Successed = false;
                return;
            }

            if (this.CBH01_CMINGUB.GetValue().ToString() == "20")
            {
                string sFirst = string.Empty;
                string sLast  = string.Empty;

                // 보험번호 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66SE8432");

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sFirst = dt.Rows[0][0].ToString().Substring(0, 2).ToString();
                    sLast  = dt.Rows[0][0].ToString().Substring(3, 3).ToString();
                }

                if (sLast == "999")
                {
                    sFirst = Convert.ToString(Int32.Parse(sFirst) + 1);
                    sLast = "001";
                }
                else
                {
                    sLast = Set_Fill3(Convert.ToString(Int32.Parse(sLast) + 1));
                }

                this.TXT01_CMINNO.SetValue(sFirst.ToString() + "-" + sLast.ToString());
            }
            else
            {
                this.TXT01_CMINNO.SetValue("");
            }

            // 박동근 차장
            // 등록시 반입일자는 입고일자와 일치하도록 해달라고 하였음
            this.DTP01_CMBOGODAT.SetValue(this.DTP01_CMBANIL.GetValue().ToString());

            #region Description : 하역료 계산

            string sCMHAAM = string.Empty;
            // 하역료 계산
            // 입항일자 기준으로 생성이 된다.
            // 1. 이전 요율 계약에 해당되는 화주면 이전 요율로 적용하고
            // 2. 이전 요율 계약에 해당되는 화주가 아니면 현재 요율로 적용한다.

            string sCMSHQTY = string.Empty;

            if (this.CBO01_CMCUSTGB.GetValue().ToString() == "Y" && this.CBH01_CMHWAJU.GetValue().ToString() != "HYH")
            {
                sCMSHQTY = Get_Numeric(this.TXT01_CMOBQTY.GetValue().ToString());
            }
            else
            {
                sCMSHQTY = Get_Numeric(this.TXT01_CMSHQTY.GetValue().ToString());
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66TED458",
                                    this.DTP01_CMIPHANG.GetValue().ToString(),
                                    this.CBH01_CMHWAJU.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0) // 1번 경우
            {
                this.TXT01_CMHAAM.SetValue(UP_HAYUKAMT_COMPUTE(Get_Numeric(sCMSHQTY.ToString()),
                                                               dt.Rows[0]["CHYMBASAMT"].ToString(),
                                                               dt.Rows[0]["CHYM1QTY"].ToString(),
                                                               dt.Rows[0]["CHYM2QTY"].ToString(),
                                                               dt.Rows[0]["CHYM3QTY"].ToString(),
                                                               dt.Rows[0]["CHYM4QTY"].ToString(),
                                                               dt.Rows[0]["CHYM5QTY"].ToString(),
                                                               dt.Rows[0]["CHYM1AMT"].ToString(),
                                                               dt.Rows[0]["CHYM2AMT"].ToString(),
                                                               dt.Rows[0]["CHYM3AMT"].ToString(),
                                                               dt.Rows[0]["CHYM4AMT"].ToString(),
                                                               dt.Rows[0]["CHYM5AMT"].ToString()
                                                               ));

                sCMHAAM = this.TXT01_CMHAAM.GetValue().ToString();
            }
            else // 2번 경우
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66TEE459", this.DTP01_CMIPHANG.GetValue().ToString());

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.TXT01_CMHAAM.SetValue(UP_HAYUKAMT_COMPUTE(Get_Numeric(sCMSHQTY.ToString()), 
                                                                   dt1.Rows[0]["CHYMBASAMT"].ToString(),
                                                                   dt1.Rows[0]["CHYM1QTY"].ToString(),
                                                                   dt1.Rows[0]["CHYM2QTY"].ToString(),
                                                                   dt1.Rows[0]["CHYM3QTY"].ToString(),
                                                                   dt1.Rows[0]["CHYM4QTY"].ToString(),
                                                                   dt1.Rows[0]["CHYM5QTY"].ToString(),
                                                                   dt1.Rows[0]["CHYM1AMT"].ToString(),
                                                                   dt1.Rows[0]["CHYM2AMT"].ToString(),
                                                                   dt1.Rows[0]["CHYM3AMT"].ToString(),
                                                                   dt1.Rows[0]["CHYM4AMT"].ToString(),
                                                                   dt1.Rows[0]["CHYM5AMT"].ToString()
                                                                   ));

                    sCMHAAM = this.TXT01_CMHAAM.GetValue().ToString();
                }
                else // 2번 경우
                {
                    this.ShowMessage("TY_M_UT_66TEH460");
                    this.DTP01_CMIPHANG.Focus();

                    e.Successed = false;
                    return;

                }
            }

            #endregion

            #region Description : 화물료 계산

            string sMOK = string.Empty;
            string sNA  = string.Empty;

            // 몫
            sMOK = Convert.ToString(UP_DotDelete(Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_CMBBQTY.GetValue().ToString())) / 10))).ToString();

            // 나머지
            sNA = Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_CMBBQTY.GetValue().ToString())) - (Decimal.Parse(sMOK.ToString()) * 10));

            if (Decimal.Parse(sNA.ToString()) != 0)
            {
                sMOK = Convert.ToString((Int64.Parse(sMOK.ToString()) + 1) * 10).ToString();
            }
            else
            {
                sMOK = Convert.ToString((Int64.Parse(sMOK.ToString())) * 10).ToString();
            }

            string sVSHMGB = string.Empty;
            string sVSVSGB = string.Empty;
            string sCMIPAM = string.Empty;

            // 화물료 구분 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66U8Y472", this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sVSHMGB = dt.Rows[0]["VSHMGB"].ToString();
                sVSVSGB = dt.Rows[0]["VSVSGB"].ToString();
            }

            decimal dCHYMINAMT  = 0;
            decimal dCHYMOUTAMT = 0;
            double  dCHYMHALYUL = 0;
            decimal dCHYMSEAMT = 0; //항만시설 보안료 단가

            // 화물료 단가 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66U8W471", this.DTP01_CMIPHANG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dCHYMINAMT  = decimal.Parse(dt.Rows[0]["CHYMINAMT"].ToString());
                dCHYMOUTAMT = decimal.Parse(dt.Rows[0]["CHYMOUTAMT"].ToString());
                dCHYMHALYUL = double.Parse(dt.Rows[0]["CHYMHALYUL"].ToString());
                dCHYMSEAMT = decimal.Parse(dt.Rows[0]["CHYMSEAMT"].ToString());
            }

            // 20171121 수정 전 소스
            //if (sVSHMGB.ToString() == "3") // 내항
            //{
            //    sCMIPAM = Convert.ToString(Decimal.Parse(sMOK.ToString()) * dCHYMINAMT).ToString();
            //}
            //else // 외항
            //{
            //    sCMIPAM = Convert.ToString(Decimal.Parse(sMOK.ToString()) * dCHYMOUTAMT).ToString();
            //}

            //// 내항일 경우 70% 할인 임
            //if (sVSHMGB.ToString() == "3") // 내항
            //{
            //    if (int.Parse(Get_Date(this.DTP01_CMIPHANG.GetValue().ToString())) >= 20180101)
            //    {
            //        if (dCHYMHALYUL != 0)
            //        {
            //            sCMIPAM = Convert.ToString((double.Parse(sCMIPAM.ToString()) * ((100-dCHYMHALYUL) / 100))).ToString();
            //        }
            //    }
            //}

            // 20171121 수정 후 소스
            if (sVSVSGB.ToString() == "2") // 내항
            {
                sCMIPAM = Convert.ToString(Decimal.Parse(sMOK.ToString()) * dCHYMINAMT).ToString();
            }
            else // 외항
            {
                sCMIPAM = Convert.ToString(Decimal.Parse(sMOK.ToString()) * dCHYMOUTAMT).ToString();
            }

            // 내항일 경우 70% 할인 임
            if (sVSVSGB.ToString() == "2") // 내항
            {
                if (int.Parse(Get_Date(this.DTP01_CMIPHANG.GetValue().ToString())) >= 20180101)
                {
                    if (dCHYMHALYUL != 0)
                    {
                        sCMIPAM = Convert.ToString((double.Parse(sCMIPAM.ToString()) * ((100 - dCHYMHALYUL) / 100))).ToString();
                    }
                }
            }

            sCMIPAM = UP_DotDelete(Convert.ToString(Decimal.Parse(sCMIPAM.ToString()) / 10));
            sCMIPAM = Convert.ToString(Decimal.Parse(sCMIPAM.ToString()) * 10);
            
            if (fsCMHMDATE == "0")
            {
                // 화물료
                this.TXT01_CMIPAM.SetValue(sCMIPAM.ToString());
            }

            #endregion

            if (this.CBH01_CMBONSUN.GetValue().ToString() == "PP1" || this.CBH01_CMBONSUN.GetValue().ToString() == "PP2" ||
                this.CBH01_CMBONSUN.GetValue().ToString() == "PP3" || this.CBH01_CMBONSUN.GetValue().ToString() == "PP4" ||
                this.CBH01_CMBONSUN.GetValue().ToString() == "PP5" || this.CBH01_CMBONSUN.GetValue().ToString() == "TK1" ||
                this.CBH01_CMBONSUN.GetValue().ToString() == "TK2" || this.CBH01_CMBONSUN.GetValue().ToString() == "TK3" ||
                this.CBH01_CMBONSUN.GetValue().ToString() == "TK7" || this.CBH01_CMBONSUN.GetValue().ToString() == "PIP" || 
                this.CBH01_CMBONSUN.GetValue().ToString() == "CON" || this.CBH01_CMBONSUN.GetValue().ToString() == "SA6")
            {
                this.TXT01_CMHAAM.SetValue("0");

                if (fsCMHMDATE == "0")
                {
                    this.TXT01_CMIPAM.SetValue("0");
                }
            }

            #region Description : 항만시설보안료 계산

            string sCMSEAMT = string.Empty;
            string sCMSEBBLS = string.Empty;
            string sCMSETRASBBLS = string.Empty;
            string sTEMP = string.Empty;

            // 환적화물 K/L 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_91EEJ495", this.DTP01_CMIPHANG.GetString(),
                                                        this.CBH01_CMBONSUN.GetValue().ToString(),
                                                        this.CBH01_CMHWAJU.GetValue().ToString(),
                                                        this.CBH01_CMHWAMUL.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 환적화물 Bbls 계산
                sCMSETRASBBLS = Convert.ToString((((double.Parse(dt.Rows[0]["IPKLQTY"].ToString()) / 0.158984) * 1000)));
                sCMSETRASBBLS = UP_DotDelete(sCMSETRASBBLS);
                sCMSETRASBBLS = Convert.ToString(double.Parse(sCMSETRASBBLS) / 1000);

                this.TXT01_CMSETRANSBBLS.SetValue(sCMSETRASBBLS);
                this.TXT01_CMSEBBLS.SetValue(decimal.Parse(Get_Numeric(this.TXT01_CMBBQTY.GetValue().ToString())) - decimal.Parse(Get_Numeric(this.TXT01_CMSETRANSBBLS.GetValue().ToString())));
            }
            else
            {
                this.TXT01_CMSEBBLS.SetValue(this.TXT01_CMBBQTY.GetValue().ToString());
                this.TXT01_CMSETRANSBBLS.SetValue("0");
            }

            // 입항관리 선박구분:2(내항선), 접안장소:2(2부두)가 아닌경우 항만시설 보안료 계산
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_91EDM492", this.DTP01_CMIPHANG.GetString(),
                                                        this.CBH01_CMBONSUN.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VSVSGB"].ToString() != "2" && dt.Rows[0]["VSJUBAN"].ToString() != "2")
                {
                    // 항만보안료 추가 계산 보안료 * 0.05 20190101 입항부터 적용
                    if (Convert.ToInt32(DTP01_CMIPHANG.GetString()) >= 20190101)
                    {
                        sCMSEBBLS = Convert.ToString(UP_DotDelete(Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_CMSEBBLS.GetValue().ToString())) / 10))).ToString();

                        sTEMP = Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_CMSEBBLS.GetValue().ToString())) - (Decimal.Parse(sCMSEBBLS.ToString()) * 10));

                        if (Decimal.Parse(sTEMP.ToString()) != 0)
                        {
                            sCMSEBBLS = Convert.ToString(Int64.Parse(sCMSEBBLS.ToString()) + 1).ToString();
                        }
                        else
                        {
                            sCMSEBBLS = Convert.ToString(Int64.Parse(sCMSEBBLS.ToString())).ToString();
                        }

                        sCMSEAMT = Convert.ToString((Decimal.Parse(sCMSEBBLS.ToString()) * dCHYMSEAMT));
                        sCMSEAMT = UP_DotDelete(Convert.ToString(Decimal.Parse(sCMSEAMT.ToString()) / 10));
                        sCMSEAMT = Convert.ToString(Decimal.Parse(sCMSEAMT.ToString()) * 10);

                        this.TXT01_CMSEAMT.SetValue(sCMSEAMT);

                        if (this.CBH01_CMBONSUN.GetValue().ToString() == "PP1" || this.CBH01_CMBONSUN.GetValue().ToString() == "PP2" ||
                            this.CBH01_CMBONSUN.GetValue().ToString() == "PP3" || this.CBH01_CMBONSUN.GetValue().ToString() == "PP4" ||
                            this.CBH01_CMBONSUN.GetValue().ToString() == "PP5" || this.CBH01_CMBONSUN.GetValue().ToString() == "TK1" ||
                            this.CBH01_CMBONSUN.GetValue().ToString() == "TK2" || this.CBH01_CMBONSUN.GetValue().ToString() == "TK3" ||
                            this.CBH01_CMBONSUN.GetValue().ToString() == "TK7" || this.CBH01_CMBONSUN.GetValue().ToString() == "PIP" || 
                            this.CBH01_CMBONSUN.GetValue().ToString() == "CON" || this.CBH01_CMBONSUN.GetValue().ToString() == "SA6")
                        {
                            this.TXT01_CMSEBBLS.SetValue("0");
                            this.TXT01_CMSETRANSBBLS.SetValue("0");
                            this.TXT01_CMSEAMT.SetValue("0");
                        }
                    }
                }
                else
                {
                    this.TXT01_CMSEBBLS.SetValue("0");
                    this.TXT01_CMSETRANSBBLS.SetValue("0");
                    this.TXT01_CMSEAMT.SetValue("0");
                }
            }

            #endregion

            // 저장 하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        //#region Description : 입고화물관리 수정 ProcessCheck
        //private void BTN62_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    fsVSHMGB = "";

        //    DataTable dt = new DataTable();
        //    DataTable dt1 = new DataTable();

        //    if (this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() != "NO" && this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() != "")
        //    {
        //        if (this.TXT01_CMHYUKGB.GetValue().ToString().Length != 6)
        //        {
        //            this.ShowMessage("TY_M_UT_66SD2414");
        //            this.TXT01_CMHYUKGB.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //        else
        //        {
        //            for (int i = 0; i < 6; i++)
        //            {
        //                if (this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "1" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "2" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "3" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "4" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "5" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "6" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "7" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "8" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "9" &&
        //                   this.TXT01_CMHYUKGB.GetValue().ToString().Substring(i, 1) != "0")
        //                {
        //                    this.ShowMessage("TY_M_UT_66SD3415");
        //                    this.TXT01_CMHYUKGB.Focus();

        //                    e.Successed = false;
        //                    return;
        //                }
        //            }
        //        }

        //        if (this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper() == "S")
        //        {
        //            if (double.Parse(Get_Numeric(this.TXT01_CMHYQTY.GetValue().ToString())) == 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SD4416");
        //                this.TXT01_CMHYQTY.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }
        //        else if (this.CBO01_CMIPGOGB.GetValue().ToString() == "")
        //        {
        //            this.TXT01_CMHYQTY.SetValue(this.TXT01_CMSHQTY.GetValue().ToString());
        //        }
        //    }




        //    if (this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() == "NO")
        //    {
        //        this.TXT01_CMHYQTY.Text = "0";
        //    }

        //    if (this.CBO01_CMCUSTGB.GetValue().ToString().ToUpper() == "Y")
        //    {
        //        if (Get_Numeric(this.TXT01_CMOBQTY.GetValue().ToString()) == "0")
        //        {
        //            this.ShowMessage("TY_M_UT_66SDB417");
        //            this.TXT01_CMOBQTY.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }

        //    // 입고구분
        //    if (this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper() == "S" && this.CBO01_CMIPGOGB.GetValue().ToString().ToUpper() == "T")
        //    {
        //        if (Get_Numeric(Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString().Substring(0, 6).ToString()).Trim()) != "0")
        //        {
        //            if (Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString()) != Get_Date(this.DTP01_CMBANIL.GetValue().ToString()))
        //            {
        //                this.ShowMessage("TY_M_UT_66SDD418");
        //                this.DTP01_CMBANIL.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            this.ShowMessage("TY_M_UT_66SDD419");
        //            this.DTP01_CMBOGODAT.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        string sHOSTR = string.Empty;
        //        string sHOEND = string.Empty;
        //        string sPUSTR = string.Empty;
        //        string sPUEND = string.Empty;

        //        sHOSTR = Set_Fill2(this.TXT01_CMHOSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOSTR2.GetValue().ToString());
        //        sHOEND = Set_Fill2(this.TXT01_CMHOEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMHOEND2.GetValue().ToString());
        //        sPUSTR = Set_Fill2(this.TXT01_CMPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUSTR2.GetValue().ToString());
        //        sPUEND = Set_Fill2(this.TXT01_CMPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUEND2.GetValue().ToString());

        //        // HOSE 작업시작시간
        //        if (Int32.Parse(sHOSTR) > 2459)
        //        {
        //            this.ShowMessage("TY_M_UT_66SDE420");
        //            this.TXT01_CMHOSTR1.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // HOSE 작업종료시간
        //        if (Int32.Parse(sHOEND) > 2459)
        //        {
        //            this.ShowMessage("TY_M_UT_66SDE421");
        //            this.TXT01_CMHOEND1.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // PUMP 작업시작시간
        //        if (Int32.Parse(sPUSTR) > 2459)
        //        {
        //            this.ShowMessage("TY_M_UT_66SDE422");
        //            this.TXT01_CMPUSTR1.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // PUMP 작업종료시간
        //        if (Int32.Parse(sPUEND) > 2459)
        //        {
        //            this.ShowMessage("TY_M_UT_66SDF423");
        //            this.TXT01_CMPUEND1.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // 보험등급
        //        if (this.CBH01_CMINGUB.GetValue().ToString() == "")
        //        {
        //            this.ShowMessage("TY_M_UT_66SDF424");
        //            this.CBH01_CMINGUB.Focus();

        //            e.Successed = false;
        //            return;
        //        }


        //        // 탱크번호
        //        if (this.TXT01_CMTANO1.GetValue().ToString() == "" && this.TXT01_CMTANO2.GetValue().ToString() == "" && this.TXT01_CMTANO3.GetValue().ToString() == "" &&
        //            this.TXT01_CMTANO4.GetValue().ToString() == "" && this.TXT01_CMTANO5.GetValue().ToString() == "" && this.TXT01_CMTANO6.GetValue().ToString() == "" &&
        //            this.TXT01_CMTANO7.GetValue().ToString() == "" && this.TXT01_CMTANO8.GetValue().ToString() == "" && this.TXT01_CMTANO9.GetValue().ToString() == "" &&
        //            this.TXT01_CMTANO10.GetValue().ToString() == "")
        //        {
        //            this.ShowMessage("TY_M_UT_66SDG425");
        //            this.TXT01_CMTANO1.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        // 탱크번호1
        //        if (this.TXT01_CMTANO1.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO1.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO1.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호2
        //        if (this.TXT01_CMTANO2.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO2.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO2.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호3
        //        if (this.TXT01_CMTANO3.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO3.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO3.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호4
        //        if (this.TXT01_CMTANO4.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO4.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO4.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호5
        //        if (this.TXT01_CMTANO5.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO5.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO5.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }


        //        // 탱크번호6
        //        if (this.TXT01_CMTANO6.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO6.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO6.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호7
        //        if (this.TXT01_CMTANO7.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO7.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO7.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호8
        //        if (this.TXT01_CMTANO8.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO8.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO8.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호9
        //        if (this.TXT01_CMTANO9.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO9.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO9.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 탱크번호10
        //        if (this.TXT01_CMTANO10.GetValue().ToString() != "")
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_66SDH426",
        //                this.TXT01_CMTANO10.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_66SDG425");
        //                this.TXT01_CMTANO10.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        // 반입사고
        //        if (this.CBH01_CMBANSG.GetValue().ToString().ToUpper() == "")
        //        {
        //            this.ShowMessage("TY_M_UT_66SDJ427");
        //            this.CBH01_CMBANSG.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (Get_Numeric(Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString().Substring(0, 6).ToString()).Trim()) != "0")
        //        {
        //            if (Get_Date(this.DTP01_CMBOGODAT.GetValue().ToString()) != Get_Date(this.DTP01_CMBANIL.GetValue().ToString()))
        //            {
        //                this.ShowMessage("TY_M_UT_66SDD418");
        //                this.DTP01_CMBANIL.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            this.ShowMessage("TY_M_UT_66SDD419");
        //            this.DTP01_CMBOGODAT.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }

        //    // 입항 파일의 화물 구분 체크
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_66SDL428",
        //        Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()),
        //        this.CBH01_CMBONSUN.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count <= 0)
        //    {
        //        this.ShowMessage("TY_M_UT_66SDM429");
        //        this.DTP01_CMIPHANG.Focus();

        //        e.Successed = false;
        //        return;
        //    }
        //    else
        //    {
        //        fsVSHMGB = dt.Rows[0][0].ToString();
        //    }

        //    // 선박입항파일의 접안시간 및 이안시간을 체크
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_66SDO430",
        //        Get_Date(this.DTP01_CMIPHANG.GetValue().ToString()),
        //        this.CBH01_CMBONSUN.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        this.ShowMessage("TY_M_UT_66SDP431");
        //        this.DTP01_CMIPHANG.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if (this.CBH01_CMINGUB.GetValue().ToString() == "20")
        //    {
        //        string sFirst = string.Empty;
        //        string sLast = string.Empty;

        //        // 보험번호 업데이트
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach("TY_P_UT_66SE8432");

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            sFirst = dt.Rows[0][0].ToString().Substring(0, 2).ToString();
        //            sLast = dt.Rows[0][0].ToString().Substring(3, 3).ToString();
        //        }

        //        if (sLast == "999")
        //        {
        //            sFirst = Convert.ToString(Int32.Parse(sFirst) + 1);
        //            sLast = "001";
        //        }
        //        else
        //        {
        //            sLast = Set_Fill3(Convert.ToString(Int32.Parse(sLast) + 1));
        //        }

        //        this.TXT01_CMINNO.SetValue(sFirst.ToString() + "-" + sLast.ToString());
        //    }
        //    else
        //    {
        //        this.TXT01_CMINNO.SetValue("");
        //    }

        //    // 박동근 차장
        //    // 등록시 반입일자는 입고일자와 일치하도록 해달라고 하였음
        //    this.DTP01_CMBOGODAT.SetValue(this.DTP01_CMBANIL.GetValue().ToString());

        //    #region Description : 하역료 계산

        //    string sCMHAAM = string.Empty;
        //    // 하역료 계산
        //    // 입항일자 기준으로 생성이 된다.
        //    // 1. 이전 요율 계약에 해당되는 화주면 이전 요율로 적용하고
        //    // 2. 이전 요율 계약에 해당되는 화주가 아니면 현재 요율로 적용한다.

        //    string sCMSHQTY = string.Empty;

        //    if (this.CBO01_CMCUSTGB.GetValue().ToString() == "Y" && this.CBH01_CMHWAJU.GetValue().ToString() != "HYH")
        //    {
        //        sCMSHQTY = Get_Numeric(this.TXT01_CMOBQTY.GetValue().ToString());
        //    }
        //    else
        //    {
        //        sCMSHQTY = Get_Numeric(this.TXT01_CMSHQTY.GetValue().ToString());
        //    }

        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach("TY_P_UT_66TED458",
        //                            this.DTP01_CMIPHANG.GetValue().ToString(),
        //                            this.CBH01_CMHWAJU.GetValue().ToString()
        //                            );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0) // 1번 경우
        //    {
        //        this.TXT01_CMHAAM.SetValue(UP_HAYUKAMT_COMPUTE(Get_Numeric(sCMSHQTY.ToString()),
        //                                                       dt.Rows[0]["CHYMBASAMT"].ToString(),
        //                                                       dt.Rows[0]["CHYM1QTY"].ToString(),
        //                                                       dt.Rows[0]["CHYM2QTY"].ToString(),
        //                                                       dt.Rows[0]["CHYM3QTY"].ToString(),
        //                                                       dt.Rows[0]["CHYM4QTY"].ToString(),
        //                                                       dt.Rows[0]["CHYM5QTY"].ToString(),
        //                                                       dt.Rows[0]["CHYM1AMT"].ToString(),
        //                                                       dt.Rows[0]["CHYM2AMT"].ToString(),
        //                                                       dt.Rows[0]["CHYM3AMT"].ToString(),
        //                                                       dt.Rows[0]["CHYM4AMT"].ToString(),
        //                                                       dt.Rows[0]["CHYM5AMT"].ToString()
        //                                                       ));

        //        sCMHAAM = this.TXT01_CMHAAM.GetValue().ToString();
        //    }
        //    else // 2번 경우
        //    {
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach("TY_P_UT_66TEE459", this.DTP01_CMIPHANG.GetValue().ToString());

        //        dt1 = this.DbConnector.ExecuteDataTable();

        //        if (dt1.Rows.Count > 0)
        //        {
        //            this.TXT01_CMHAAM.SetValue(UP_HAYUKAMT_COMPUTE(Get_Numeric(sCMSHQTY.ToString()),
        //                                                           dt1.Rows[0]["CHYMBASAMT"].ToString(),
        //                                                           dt1.Rows[0]["CHYM1QTY"].ToString(),
        //                                                           dt1.Rows[0]["CHYM2QTY"].ToString(),
        //                                                           dt1.Rows[0]["CHYM3QTY"].ToString(),
        //                                                           dt1.Rows[0]["CHYM4QTY"].ToString(),
        //                                                           dt1.Rows[0]["CHYM5QTY"].ToString(),
        //                                                           dt1.Rows[0]["CHYM1AMT"].ToString(),
        //                                                           dt1.Rows[0]["CHYM2AMT"].ToString(),
        //                                                           dt1.Rows[0]["CHYM3AMT"].ToString(),
        //                                                           dt1.Rows[0]["CHYM4AMT"].ToString(),
        //                                                           dt1.Rows[0]["CHYM5AMT"].ToString()
        //                                                           ));

        //            sCMHAAM = this.TXT01_CMHAAM.GetValue().ToString();
        //        }
        //        else // 2번 경우
        //        {
        //            this.ShowMessage("TY_M_UT_66TEH460");
        //            this.DTP01_CMIPHANG.Focus();

        //            e.Successed = false;
        //            return;

        //        }
        //    }

        //    #endregion

        //    #region Description : 화물료 계산

        //    string sMOK = string.Empty;
        //    string sNA = string.Empty;

        //    // 몫
        //    sMOK = Convert.ToString(UP_DotDelete(Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_CMBBQTY.GetValue().ToString())) / 10))).ToString();

        //    // 나머지
        //    sNA = Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_CMBBQTY.GetValue().ToString())) - (Decimal.Parse(sMOK.ToString()) * 10));

        //    if (Decimal.Parse(sNA.ToString()) != 0)
        //    {
        //        sMOK = Convert.ToString(Int64.Parse(sMOK.ToString()) + 1).ToString();
        //    }

        //    string sVSHMGB = string.Empty;
        //    string sCMIPAM = string.Empty;

        //    // 화물료 구분 가져오기
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach("TY_P_UT_66U8Y472", this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString());

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        sVSHMGB = dt.Rows[0]["VSHMGB"].ToString();
        //    }

        //    decimal dCHYMINAMT = 0;
        //    decimal dCHYMOUTAMT = 0;

        //    // 화물료 단가 가져오기
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach("TY_P_UT_66U8W471", this.DTP01_CMIPHANG.GetValue().ToString());

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        dCHYMINAMT = decimal.Parse(dt.Rows[0]["CHYMINAMT"].ToString());
        //        dCHYMOUTAMT = decimal.Parse(dt.Rows[0]["CHYMOUTAMT"].ToString());
        //    }

        //    if (sVSHMGB.ToString() == "3") // 내항
        //    {
        //        sCMIPAM = Convert.ToString(Decimal.Parse(sMOK.ToString()) * dCHYMINAMT).ToString();
        //    }
        //    else // 외항
        //    {
        //        sCMIPAM = Convert.ToString(Decimal.Parse(sMOK.ToString()) * dCHYMOUTAMT).ToString();
        //    }

        //    sCMIPAM = UP_DotDelete(Convert.ToString(Decimal.Parse(sCMIPAM.ToString()) / 10));
        //    sCMIPAM = Convert.ToString(Decimal.Parse(sCMIPAM.ToString()) * 10);

        //    // 화물료
        //    this.TXT01_CMIPAM.SetValue(sCMIPAM.ToString());

        //    #endregion

        //    if (this.CBH01_CMBONSUN.GetValue().ToString() == "PP1" || this.CBH01_CMBONSUN.GetValue().ToString() == "PP2" ||
        //        this.CBH01_CMBONSUN.GetValue().ToString() == "PP3" || this.CBH01_CMBONSUN.GetValue().ToString() == "PP4" ||
        //        this.CBH01_CMBONSUN.GetValue().ToString() == "PP5" || this.CBH01_CMBONSUN.GetValue().ToString() == "TK1" ||
        //        this.CBH01_CMBONSUN.GetValue().ToString() == "TK2" || this.CBH01_CMBONSUN.GetValue().ToString() == "TK3" ||
        //        this.CBH01_CMBONSUN.GetValue().ToString() == "PIP" || this.CBH01_CMBONSUN.GetValue().ToString() == "CON")
        //    {
        //        this.TXT01_CMHAAM.SetValue("0");
        //        this.TXT01_CMIPAM.SetValue("0");
        //    }

        //    // 수정 하시겠습니까?
        //    if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }
        //}
        //#endregion

        #region Description : 입고화물관리 삭제 ProcessCheck
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 화물료 전표생성 체크
            if (this.TXT01_CMHMDATE.GetValue().ToString() != "0")
            {
                this.ShowMessage("TY_M_UT_ACTGX249");
                this.DTP01_CMIPHANG.Focus();

                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_ACTGV247",
                this.DTP01_CMIPHANG.GetValue().ToString(),
                this.CBH01_CMBONSUN.GetValue().ToString(),
                this.CBH01_CMHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_ACTGX249");
                this.DTP01_CMIPHANG.Focus();

                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66SGN448",
                this.DTP01_CMIPHANG.GetValue().ToString(),
                this.CBH01_CMBONSUN.GetValue().ToString(),
                this.CBH01_CMHWAJU.GetValue().ToString(),
                this.CBH01_CMHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_66SGP449");
                this.DTP01_CMIPHANG.Focus();

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

        #region Description : 하역료 계산
        private string UP_HAYUKAMT_COMPUTE(string sCMSHQTY,  string sCHYMBASAMT,
                                           string sCHYM1QTY, string sCHYM2QTY, string sCHYM3QTY, string sCHYM4QTY, string sCHYM5QTY,
                                           string sCHYM1AMT, string sCHYM2AMT, string sCHYM3AMT, string sCHYM4AMT, string sCHYM5AMT)
        {
            double dCMSHQTY    = 0;
            double dCHYMBASAMT = 0;

            double dCHYM1QTY   = 0;
            double dCHYM2QTY   = 0;
            double dCHYM3QTY   = 0;
            double dCHYM4QTY   = 0;
            double dCHYM5QTY   = 0;

            double dCHYM1AMT   = 0;
            double dCHYM2AMT   = 0;
            double dCHYM3AMT   = 0;
            double dCHYM4AMT   = 0;
            double dCHYM5AMT   = 0;

            dCMSHQTY    = double.Parse(sCMSHQTY.ToString());
            dCHYMBASAMT = double.Parse(sCHYMBASAMT.ToString());

            dCHYM1QTY = double.Parse(sCHYM1QTY.ToString());
            dCHYM2QTY = double.Parse(sCHYM2QTY.ToString());
            dCHYM3QTY = double.Parse(sCHYM3QTY.ToString());
            dCHYM4QTY = double.Parse(sCHYM4QTY.ToString());
            dCHYM5QTY = double.Parse(sCHYM5QTY.ToString());

            dCHYM1AMT = double.Parse(sCHYM1AMT.ToString());
            dCHYM2AMT = double.Parse(sCHYM2AMT.ToString());
            dCHYM3AMT = double.Parse(sCHYM3AMT.ToString());
            dCHYM4AMT = double.Parse(sCHYM4AMT.ToString());
            dCHYM5AMT = double.Parse(sCHYM5AMT.ToString());


            string sHAYUKAMT = string.Empty;

            sHAYUKAMT = "0";

            // 1구간
            if (dCHYM1AMT != 0)
            {
                if (dCMSHQTY > dCHYM1QTY)
                {
                    sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + (dCHYM1QTY * dCHYM1AMT))));
                }
                else
                {
                    sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + (dCMSHQTY * dCHYM1AMT))));
                }
            }

            // 2구간
            if (dCHYM2AMT != 0)
            {
                if ((dCMSHQTY > dCHYM2QTY) && (dCMSHQTY > dCHYM1QTY))
                {
                    if (dCHYM2QTY == 0 && dCHYM2AMT != 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM1QTY) * dCHYM2AMT))));
                    }
                    else
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCHYM2QTY - dCHYM1QTY) * dCHYM2AMT))));
                    }
                }
                else
                {
                    if (dCMSHQTY - dCHYM1QTY >= 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM1QTY) * dCHYM2AMT))));
                    }
                }
            }

            // 3구간
            if (dCHYM3AMT != 0)
            {
                if ((dCMSHQTY > dCHYM3QTY) && (dCMSHQTY > dCHYM2QTY))
                {
                    if (dCHYM3QTY == 0 && dCHYM3AMT != 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM2QTY) * dCHYM3AMT))));
                    }
                    else
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCHYM3QTY - dCHYM2QTY) * dCHYM3AMT))));
                    }
                }
                else
                {
                    if (dCMSHQTY - dCHYM2QTY >= 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM2QTY) * dCHYM3AMT))));
                    }
                }
            }

            // 4구간
            if (dCHYM4AMT != 0)
            {
                if ((dCMSHQTY > dCHYM4QTY) && (dCMSHQTY > dCHYM3QTY))
                {
                    if (dCHYM4QTY == 0 && dCHYM4AMT != 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM3QTY) * dCHYM4AMT))));
                    }
                    else
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCHYM4QTY - dCHYM3QTY) * dCHYM4AMT))));
                    }
                }
                else
                {
                    if (dCMSHQTY - dCHYM3QTY >= 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM3QTY) * dCHYM4AMT))));
                    }
                }
            }

            // 5구간
            if (dCHYM5AMT != 0)
            {
                if ((dCMSHQTY > dCHYM5QTY) && (dCMSHQTY > dCHYM4QTY))
                {
                    if (dCHYM5QTY == 0 && dCHYM5AMT != 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM4QTY) * dCHYM5AMT))));
                    }
                    else
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCHYM5QTY - dCHYM4QTY) * dCHYM5AMT))));
                    }
                }
                else
                {
                    if (dCMSHQTY - dCHYM4QTY >= 0)
                    {
                        sHAYUKAMT = Convert.ToString(UP_DotDelete(Convert.ToString(double.Parse(sHAYUKAMT) + ((dCMSHQTY - dCHYM4QTY) * dCHYM5AMT))));
                    }
                }
            }

            sHAYUKAMT = Convert.ToString(Decimal.Parse(UP_DotDelete(sHAYUKAMT.ToString())) / 10).ToString();
            sHAYUKAMT = Convert.ToString(Decimal.Parse(UP_DotDelete(sHAYUKAMT.ToString())) * 10).ToString();

            sHAYUKAMT = Convert.ToString(Decimal.Parse(Get_Numeric(sHAYUKAMT.ToString())) + Decimal.Parse(sCHYMBASAMT.ToString())).ToString();

            return sHAYUKAMT;
        }
        #endregion

        #region Description : 품명코드 텍스트박스 이벤트
        private void CBO01_CMPUMCODE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN62_SAV.Visible == true)
                {
                    SetFocus(this.BTN62_SAV);
                }
                //else if (this.BTN62_EDIT.Visible == true)
                //{
                //    SetFocus(this.BTN62_EDIT);
                //}
            }
        }
        #endregion

        #region Description : 입고화물관리 필드 클리어
        private void UP_UTICMDTF_FieldClear(string sGUBUN)
        {
            this.DTP01_CMIPHANG.SetReadOnly(false);
            this.CBH01_CMBONSUN.SetReadOnly(false);
            this.CBH01_CMHWAJU.SetReadOnly(false);
            this.CBH01_CMHWAMUL.SetReadOnly(false);

            this.DTP01_CMBANIL.SetValue("");
            this.DTP01_CMBOGODAT.SetValue("");
            this.MTB01_CMMAECH.SetValue("");

            this.TXT01_CMHAAM.SetValue("");
            this.TXT01_CMIPAM.SetValue("");

            // 반입사고
            this.CBH01_CMBANSG.SetValue("OK");


            if (sGUBUN.ToString() == "CLEAR")
            {
                // KL
                this.TXT01_CMKLQTY.SetValue("");
                // 입고구분
                this.CBO01_CMIPGOGB.SetValue("");
                // 선상구분
                this.CBO01_CMCUSTGB.SetValue("");
                // B/L량
                this.TXT01_CMBLQTY.SetValue("");
                // O/B량
                this.TXT01_CMOBQTY.SetValue("");
                // SHORE량       
                this.TXT01_CMSHQTY.SetValue("");
                // Bbls량
                this.TXT01_CMBBQTY.SetValue("");
                // 하역비
                this.TXT01_CMHAAM.SetValue("");
                // 화물료
                this.TXT01_CMIPAM.SetValue("");
                // HOSE시작시간
                this.TXT01_CMHOSTR1.SetValue("");
                this.TXT01_CMHOSTR2.SetValue("");
                // HOSE종료시간
                this.TXT01_CMHOEND1.SetValue("");
                this.TXT01_CMHOEND2.SetValue("");
                // PUMP시작시간
                this.TXT01_CMPUSTR1.SetValue("");
                this.TXT01_CMPUSTR2.SetValue("");
                // PUMP종료시간
                this.TXT01_CMPUEND1.SetValue("");
                this.TXT01_CMPUEND2.SetValue("");
                // TON/H
                this.TXT01_CMTONH.SetValue("");
                // 보험등급
                this.CBH01_CMINGUB.SetValue("");
                // 공제일수
                this.TXT01_CMINGONG.SetValue("");
                // 보험번호
                this.TXT01_CMINNO.SetValue("");
                // 반입사고
                this.CBH01_CMBANSG.SetValue("");
                // 입고탱크1
                this.TXT01_CMTANO1.SetValue("");
                // 입고탱크2
                this.TXT01_CMTANO2.SetValue("");
                // 입고탱크3
                this.TXT01_CMTANO3.SetValue("");
                // 입고탱크4
                this.TXT01_CMTANO4.SetValue("");
                // 입고탱크5
                this.TXT01_CMTANO5.SetValue("");
                // 입고탱크6
                this.TXT01_CMTANO6.SetValue("");
                // 입고탱크7
                this.TXT01_CMTANO7.SetValue("");
                // 입고탱크8
                this.TXT01_CMTANO8.SetValue("");
                // 입고탱크9
                this.TXT01_CMTANO9.SetValue("");
                // 입고탱크10
                this.TXT01_CMTANO10.SetValue("");
                // 구분
                this.TXT01_VSGBNM.SetValue("");
                // 취급년월
                this.TXT01_CMHYUKGB.SetValue("");
                // 취급량
                this.TXT01_CMHYQTY.SetValue("");
            }
        }
        #endregion

        #region Description : 입고화물관리 디스플레이
        private void UP_UTICMDTF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN62_SAV.Visible = true;
                //this.BTN62_EDIT.Visible = false;
                this.BTN62_REM.Visible = false;

                this.BTN62_UTTCODEHELP1.Visible = true;
                this.BTN62_UTTCODEHELP6.Visible = true;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN62_SAV.Visible = true;
                //this.BTN62_EDIT.Visible = true;
                this.BTN62_REM.Visible = true;
                this.BTN62_UTTCODEHELP1.Visible = false;
                this.BTN62_UTTCODEHELP6.Visible = true;
            }
            else
            {
                this.BTN62_SAV.Visible = false;
                //this.BTN62_EDIT.Visible = false;
                this.BTN62_REM.Visible = false;
                this.BTN62_UTTCODEHELP1.Visible = false;
                this.BTN62_UTTCODEHELP6.Visible = false;
            }
        }
        #endregion

        #endregion

        #region Description : SURVEY 관리

        #region Description : 확인 버튼
        private void BTN63_OK_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            

            if (fsWK_GUBUN3.ToString() != "NEW")
            {
                UP_UTISURVF_RUN();
            }
            else
            {
                int iTank_Cnt = 0;

                // SURVEY - 검정사 가져오기(화주, 화물이 동일하면서 제일 마지막에 등록된 데이터 가져오기)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_77RCJ282",
                    this.CBH01_SVHWAJU.GetValue().ToString(),
                    this.CBH01_SVHWAMUL.GetValue().ToString(),
                    this.DTP01_SVIPHANG.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_SVGUMJUNG.SetValue(dt.Rows[0]["SVGUMJUNG"].ToString());
                }

                if (this.DTP01_SVIPHANG.GetValue().ToString() != "" && this.CBH01_SVBONSUN.GetValue().ToString() != "" &&
                    this.CBH01_SVHWAJU.GetValue().ToString() != "" && this.CBH01_SVHWAMUL.GetValue().ToString() != "")
                {
                    this.CBO01_SVTANKNO.SetReadOnly(false);

                    this.CBO01_SVTANKNO.Initialize();

                    // 콤보박스에 바인딩
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_671D7507",
                        this.DTP01_SVIPHANG.GetValue().ToString(),
                        this.CBH01_SVBONSUN.GetValue().ToString(),
                        this.CBH01_SVHWAJU.GetValue().ToString(),
                        this.CBH01_SVHWAMUL.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        iTank_Cnt = dt.Rows.Count;

                        // 콤보박스에 바인드
                        this.CBO01_SVTANKNO.DataBind(dt, false);
                    }
                    else
                    {
                        UP_UTISURVF_BTN_DISPLAY("TAB");

                        this.ShowMessage("TY_M_UT_674IV562");
                        this.DTP01_SVIPHANG.Focus();

                        return;
                    }

                    // 입고화물관리 입고 구분 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_678HL686",
                        this.DTP01_SVIPHANG.GetValue().ToString(),
                        this.CBH01_SVBONSUN.GetValue().ToString(),
                        this.CBH01_SVHWAJU.GetValue().ToString(),
                        this.CBH01_SVHWAMUL.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        UP_UTISURVF_BTN_DISPLAY("TAB");

                        this.ShowMessage("TY_M_UT_678HL688");
                        this.DTP01_SVIPHANG.Focus();

                        return;
                    }

                    // 입고화물관리에서 펌프시간 및 SHORE량 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_675B3566",
                        this.DTP01_SVIPHANG.GetValue().ToString(),
                        this.CBH01_SVBONSUN.GetValue().ToString(),
                        this.CBH01_SVHWAJU.GetValue().ToString(),
                        this.CBH01_SVHWAMUL.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_COPUSTR11.SetValue(dt.Rows[0]["CMPUSTR"].ToString().Substring(0, 2).ToString());
                        this.TXT01_COPUSTR12.SetValue(dt.Rows[0]["CMPUSTR"].ToString().Substring(2, 2).ToString());

                        this.TXT01_COPUEND11.SetValue(dt.Rows[0]["CMPUEND"].ToString().Substring(0, 2).ToString());
                        this.TXT01_COPUEND12.SetValue(dt.Rows[0]["CMPUEND"].ToString().Substring(2, 2).ToString());

                        this.TXT01_SVTEMP.SetValue("15");

                        int iCnt = 0;

                        // 입고화물관리에서 펌프시간 및 SHORE량 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_674HO560",
                            this.DTP01_SVIPHANG.GetValue().ToString(),
                            this.CBH01_SVBONSUN.GetValue().ToString(),
                            this.CBH01_SVHWAJU.GetValue().ToString(),
                            this.CBH01_SVHWAMUL.GetValue().ToString(),
                            ""
                            );

                        dt1 = this.DbConnector.ExecuteDataTable();

                        if (dt1.Rows.Count > 0)
                        {
                            iCnt = dt1.Rows.Count;
                        }

                        // 입고화물관리에 TANK가 한개이면 MT,KL량 자동으로 가져오기
                        if (iCnt == 0)
                        {
                            fsCMSHQTY = dt.Rows[0]["CMSHQTY"].ToString();

                            this.TXT01_SVMTQTY.SetValue(dt.Rows[0]["CMSHQTY"].ToString());
                            this.TXT01_SVKLQTY.SetValue(dt.Rows[0]["CMKLQTY"].ToString());

                            //if (iTank_Cnt == 1)
                            //{
                            //    fsCMSHQTY = dt.Rows[0]["CMSHQTY"].ToString();

                            //    this.TXT01_SVMTQTY.SetValue(dt.Rows[0]["CMSHQTY"].ToString());
                            //    this.TXT01_SVKLQTY.SetValue(dt.Rows[0]["CMKLQTY"].ToString());
                            //}
                        }
                        else
                        {
                            // 20171103 고지파트 요청 : 처음은 무조건 수량 다 가져옴
                            // 2기이상일 때는 MT,KL = (입고화물관리의 수량 - 이전 탱크수의 수량의 합계)

                            fsCMSHQTY = dt.Rows[0]["CMSHQTY"].ToString();

                            double dCMSHQTY = 0;
                            double dCMKLQTY = 0;

                            double dSVMTQTY = 0;
                            double dSVKLQTY = 0;

                            dCMSHQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["CMSHQTY"].ToString())));
                            dCMKLQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["CMKLQTY"].ToString())));

                            // SURVEY의 MT,KL합계량 가져오기
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_UT_7B3BZ932",
                                this.DTP01_SVIPHANG.GetValue().ToString(),
                                this.CBH01_SVBONSUN.GetValue().ToString(),
                                this.CBH01_SVHWAJU.GetValue().ToString(),
                                this.CBH01_SVHWAMUL.GetValue().ToString()
                                );

                            dt2 = this.DbConnector.ExecuteDataTable();

                            if (dt2.Rows.Count > 0)
                            {
                                dSVMTQTY = double.Parse(String.Format("{0,9:N3}", dCMSHQTY - double.Parse(Get_Numeric(dt2.Rows[0]["SVMTQTY"].ToString()))));

                                if (dCMKLQTY != 0)
                                {
                                    dSVKLQTY = double.Parse(String.Format("{0,9:N3}", dCMKLQTY - double.Parse(Get_Numeric(dt2.Rows[0]["SVKLQTY"].ToString()))));
                                }
                            }

                            this.TXT01_SVMTQTY.SetValue(dSVMTQTY.ToString());
                            this.TXT01_SVKLQTY.SetValue(dSVKLQTY.ToString());
                        }

                        // 할증시간 가져오기
                        UP_GET_OVERTIME();

                        this.CBO01_SVTANKNO.Focus();
                    }
                }
            }
        }
        #endregion

        #region Description : 할증시간 가져오기
        private void UP_GET_OVERTIME()
        {
            DataTable dt3 = new DataTable();

            string sWEEK = string.Empty;
            // 할증시간
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_676D3576",
                Get_Date(this.DTP01_SVIPHANG.GetValue().ToString()).Substring(0, 4),
                Get_Date(this.DTP01_SVIPHANG.GetValue().ToString()).Substring(4, 2),
                Get_Date(this.DTP01_SVIPHANG.GetValue().ToString()).Substring(6, 2)
                );

            dt3 = this.DbConnector.ExecuteDataTable();

            if (dt3.Rows.Count > 0)
            {
                sWEEK = dt3.Rows[0]["UYYOILCD"].ToString();

                if (dt3.Rows[0]["UYHUMUCD"].ToString() != "")
                {
                    // 회사지정휴무(창립기념일,회사휴무일,근로자의날)
                    if (dt3.Rows[0]["UYHUMUCD"].ToString() != "10" && dt3.Rows[0]["UYHUMUCD"].ToString() != "20" && dt3.Rows[0]["UYHUMUCD"].ToString() != "04")
                    {
                        sWEEK = "1";
                    }
                }
            }

            string sSTHH = string.Empty;
            string sEDHH = string.Empty;

            string sIPHANG = Get_Date(this.DTP01_SVIPHANG.GetValue().ToString());

            // 20181212 박선형 주임 요청
            // 할증시간
            // 하절기 4 ~ 10월말까지 (18시 ~ 9시)
            // 동절기 11,12,1,2,3월말까지(17시 ~ 9시)
            if (int.Parse(sIPHANG.ToString()) > 20181231)
            {
                switch (int.Parse(sIPHANG.Substring(4, 2)))
                {
                    case 1:
                        sSTHH = "1700";
                        break;

                    case 2:
                        sSTHH = "1700";
                        break;

                    case 3:
                        sSTHH = "1700";
                        break;

                    case 4:
                        sSTHH = "1800";
                        break;

                    case 5:
                        sSTHH = "1800";
                        break;

                    case 6:
                        sSTHH = "1800";
                        break;

                    case 7:
                        sSTHH = "1800";
                        break;

                    case 8:
                        sSTHH = "1800";
                        break;

                    case 9:
                        sSTHH = "1800";
                        break;

                    case 10:
                        sSTHH = "1800";
                        break;

                    case 11:
                        sSTHH = "1700";
                        break;

                    case 12:
                        sSTHH = "1700";
                        break;
                    default:

                        break;
                }
            }
            // 할증시간
            // 하절기 3 ~ 10월말까지 (18시 ~ 9시)
            // 동절기 11,12,1,2월말까지(17시 ~ 9시)
            else if (int.Parse(sIPHANG.ToString()) >= 20160125)
            {
                switch (int.Parse(sIPHANG.Substring(4, 2)))
                {
                    case 1:
                        sSTHH = "1700";
                        break;

                    case 2:
                        sSTHH = "1700";
                        break;

                    case 3:
                        sSTHH = "1800";
                        break;

                    case 4:
                        sSTHH = "1800";
                        break;

                    case 5:
                        sSTHH = "1800";
                        break;

                    case 6:
                        sSTHH = "1800";
                        break;

                    case 7:
                        sSTHH = "1800";
                        break;

                    case 8:
                        sSTHH = "1800";
                        break;

                    case 9:
                        sSTHH = "1800";
                        break;

                    case 10:
                        sSTHH = "1800";
                        break;

                    case 11:
                        sSTHH = "1700";
                        break;

                    case 12:
                        sSTHH = "1700";
                        break;
                    default:

                        break;
                }
            }

            sEDHH = "0900";

            // 할증 시간 계산
            UP_HALTIME_COMPUTE(sWEEK, sSTHH, sEDHH);
        }
        #endregion

        #region Description : 펌프시간 가져오기
        private void BTN63_UTTCODEHELP7_Click(object sender, EventArgs e)
        {
            DataTable dt3 = new DataTable();

            // 입고화물관리에서 펌프시간
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_675B3566",
                this.DTP01_SVIPHANG.GetValue().ToString(),
                this.CBH01_SVBONSUN.GetValue().ToString(),
                this.CBH01_SVHWAJU.GetValue().ToString(),
                this.CBH01_SVHWAMUL.GetValue().ToString()
                );

            dt3 = this.DbConnector.ExecuteDataTable();

            if (dt3.Rows.Count > 0)
            {
                this.TXT01_COPUSTR11.SetValue(dt3.Rows[0]["CMPUSTR"].ToString().Substring(0, 2).ToString());
                this.TXT01_COPUSTR12.SetValue(dt3.Rows[0]["CMPUSTR"].ToString().Substring(2, 2).ToString());

                this.TXT01_COPUEND11.SetValue(dt3.Rows[0]["CMPUEND"].ToString().Substring(0, 2).ToString());
                this.TXT01_COPUEND12.SetValue(dt3.Rows[0]["CMPUEND"].ToString().Substring(2, 2).ToString());
            }

            // 할증시간 가져오기
            UP_GET_OVERTIME();
        }
        #endregion

        #region Description : SURVEY관리 클리어 버튼
        private void BTN63_UTTCLEAR_Click(object sender, EventArgs e)
        {
            // 조회
            string sSTDATE  = string.Empty;
            string sEDDATE  = string.Empty;
            string sSBONSUN = string.Empty;
            string sSHWAJU  = string.Empty;
            string sSHWAMUL = string.Empty;

            // 입고화물관리 내용
            string sIPHANG  = string.Empty;
            string sBONSUN  = string.Empty;
            string sHWAJU   = string.Empty;
            string sHWAMUL  = string.Empty;            


            sSTDATE  = this.DTP01_STIPHANG.GetValue().ToString();
            sEDDATE  = this.DTP01_EDIPHANG.GetValue().ToString();
            sSBONSUN = this.CBH01_SBONSUN.GetValue().ToString();
            sSHWAJU  = this.CBH01_SHWAJU.GetValue().ToString();
            sSHWAMUL = this.CBH01_SHWAMUL.GetValue().ToString();

            sIPHANG  = this.DTP01_SVIPHANG.GetValue().ToString();
            sBONSUN  = this.CBH01_SVBONSUN.GetValue().ToString();
            sHWAJU   = this.CBH01_SVHWAJU.GetValue().ToString();
            sHWAMUL  = this.CBH01_SVHWAMUL.GetValue().ToString();

            UP_UTISURVF_FieldClear("CLEAR");
            //this.Initialize_Controls("01");

            this.DTP01_STIPHANG.SetValue(sSTDATE.ToString());
            this.DTP01_EDIPHANG.SetValue(sEDDATE.ToString());
            this.CBH01_SBONSUN.SetValue(sSBONSUN.ToString());
            this.CBH01_SHWAJU.SetValue(sSHWAJU.ToString());
            this.CBH01_SHWAMUL.SetValue(sSHWAMUL.ToString());

            this.DTP01_SVIPHANG.SetValue(sIPHANG.ToString());
            this.CBH01_SVBONSUN.SetValue(sBONSUN.ToString());
            this.CBH01_SVHWAJU.SetValue(sHWAJU.ToString());
            this.CBH01_SVHWAMUL.SetValue(sHWAMUL.ToString());

            this.FPS91_TY_S_UT_674HK559.Initialize();

            // SURVEY REPORT 전체 조회
            UP_UTISURVF_SEARCH();

            UP_UTISURVF_BTN_DISPLAY("NEW");

            SetFocus(this.CBH01_SVHWAMUL.CodeText);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN63_NEW_Click(object sender, EventArgs e)
        {
            fsSVBIJUNG = "";
            fsSVMOGB   = "";
            fsCMSHQTY  = "0";

            this.CBO01_SVTANKNO.Initialize();

            fsWK_GUBUN3 = "NEW";
            UP_UTISURVF_FieldClear(fsWK_GUBUN3);

            UP_UTISURVF_BTN_DISPLAY(fsWK_GUBUN3);

            SetFocus(this.CBH01_SVHWAMUL.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            string sGUBUN = string.Empty;

            if (fsWK_GUBUN3.ToString() == "NEW")
            {
                sGUBUN = "ADD";
            }
            else if (fsWK_GUBUN3.ToString() == "UPT")
            {
                sGUBUN = "UPT";
            }

            UP_UTISURVF_COMPUTE();

            // PUMP시작시간
            string sCOPUSTR1  = Set_Fill2(this.TXT01_COPUSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR12.GetValue().ToString());
            // PUMP종료시간
            string sCOPUEND1  = Set_Fill2(this.TXT01_COPUEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND12.GetValue().ToString());

            // PUMP시작시간
            string sCOPUSTR2 = Set_Fill2(this.TXT01_COPUSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR22.GetValue().ToString());
            // PUMP종료시간
            string sCOPUEND2 = Set_Fill2(this.TXT01_COPUEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND22.GetValue().ToString());

            // PUMP시작시간3
            string sCOPUSTR3 = Set_Fill2(this.TXT01_COPUSTR31.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR32.GetValue().ToString());
            // PUMP종료시간
            string sCOPUEND3 = Set_Fill2(this.TXT01_COPUEND31.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND32.GetValue().ToString());

            // 할증시작시간1
            string sCOOVSTR1 = Set_Fill2(this.TXT01_COOVSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR12.GetValue().ToString());
            // 할증종료시간1
            string sCOOVEND1 = Set_Fill2(this.TXT01_COOVEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString());

            // 할증시작시간2
            string sCOOVSTR2 = Set_Fill2(this.TXT01_COOVSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR22.GetValue().ToString());
            // 할증종료시간2
            string sCOOVEND2 = Set_Fill2(this.TXT01_COOVEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString());

            // 할증시작시간3
            string sCOOVSTR3 = Set_Fill2(this.TXT01_COOVSTR31.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR32.GetValue().ToString());
            // 할증종료시간2
            string sCOOVEND3 = Set_Fill2(this.TXT01_COOVEND31.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND32.GetValue().ToString());

            UP_UTISURVF_Batch(sGUBUN.ToString(),
                              Get_Date(this.DTP01_SVIPHANG.GetValue().ToString()),
                              this.CBH01_SVBONSUN.GetValue().ToString(),
                              this.CBH01_SVHWAJU.GetValue().ToString(),
                              this.CBH01_SVHWAMUL.GetValue().ToString(),
                              this.CBH01_SVHWAMUL.GetText().ToString(),
                              this.CBH01_SVGUMJUNG.GetValue().ToString(),
						      Set_TankNo(this.CBO01_SVTANKNO.GetValue().ToString()),
						      Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString()),
                              Get_Numeric(this.TXT01_SVKLQTY.GetValue().ToString()),
						      sCOPUSTR1,
						      sCOPUEND1,
                              sCOPUSTR2,
                              sCOPUEND2,
                              sCOPUSTR3,
                              sCOPUEND3,
                              this.TXT01_COWKTIME.GetValue().ToString(),
						      sCOOVSTR1,
						      sCOOVEND1,
                              sCOOVSTR2,
                              sCOOVEND2,
                              sCOOVSTR3,
                              sCOOVEND3,
                              this.TXT01_COOWKTIME.GetValue().ToString(),
						      Get_Numeric(this.TXT01_COOVQTY.GetValue().ToString()),
						      this.TXT01_COCONTNO.GetValue().ToString(),
                              Get_Numeric(this.TXT01_SVBIJUNG.GetValue().ToString()),
                              Get_Numeric(this.TXT01_SVVCF.GetValue().ToString()),
                              Get_Numeric(this.TXT01_SVTEMP.GetValue().ToString()),
                              this.TXT01_SVTEMPGB.GetValue().ToString(),
                              this.TXT01_SVKESANGB.GetValue().ToString(),
                              this.TXT01_SVMOGB.GetValue().ToString(),
                              this.TXT01_SVMOTA.GetValue().ToString(),
						      Get_Numeric(fsCOOVAM.ToString()),
                              this.CBH01_SVHWAJU.GetText().ToString(),
                              Get_Date(DateTime.Now.ToString("yyyy-MM-dd").ToString()),
                              TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                              );

            // SURVEY 조회
            UP_UTISURVF_TAB_SEARCH(this.DTP01_SVIPHANG.GetValue().ToString(),
                                   this.CBH01_SVBONSUN.GetValue().ToString(),
                                   this.CBH01_SVHWAJU.GetValue().ToString(),
                                   this.CBH01_SVHWAMUL.GetValue().ToString(),
                                   ""
                                   );

            // SURVEY REPORT 전체 조회
            UP_UTISURVF_SEARCH();

            UP_UTISURVF_BTN_DISPLAY("TAB");

            // 값 저장
            UP_SET_Cookie2(this.DTP01_SVIPHANG.GetValue().ToString(), this.CBH01_SVBONSUN.GetValue().ToString(),
                           this.CBH01_SVHWAJU.GetValue().ToString(), this.CBH01_SVHWAMUL.GetValue().ToString());
        }
        #endregion

        //#region Description : 수정 버튼
        //private void BTN63_EDIT_Click(object sender, EventArgs e)
        //{
        //    UP_UTISURVF_COMPUTE();

        //    // PUMP시작시간
        //    string sCOPUSTR = Set_Fill2(this.TXT01_COPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR2.GetValue().ToString());
        //    // PUMP종료시간
        //    string sCOPUEND = Set_Fill2(this.TXT01_COPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND2.GetValue().ToString());
        //    // 할증시작시간1
        //    string sCOOVSTR1 = Set_Fill2(this.TXT01_COOVSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR12.GetValue().ToString());
        //    // 할증종료시간1
        //    string sCOOVEND1 = Set_Fill2(this.TXT01_COOVEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString());

        //    // 할증시작시간2
        //    string sCOOVSTR2 = Set_Fill2(this.TXT01_COOVSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR22.GetValue().ToString());
        //    // 할증종료시간2
        //    string sCOOVEND2 = Set_Fill2(this.TXT01_COOVEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString());

        //    UP_UTISURVF_Batch("UPT",
        //                      Get_Date(this.DTP01_SVIPHANG.GetValue().ToString()),
        //                      this.CBH01_SVBONSUN.GetValue().ToString(),
        //                      this.CBH01_SVHWAJU.GetValue().ToString(),
        //                      this.CBH01_SVHWAMUL.GetValue().ToString(),
        //                      this.CBH01_SVHWAMUL.GetText().ToString(),
        //                      this.CBH01_SVGUMJUNG.GetValue().ToString(),
        //                      Set_TankNo(this.CBO01_SVTANKNO.GetValue().ToString()),
        //                      Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString()),
        //                      Get_Numeric(this.TXT01_SVKLQTY.GetValue().ToString()),
        //                      sCOPUSTR,
        //                      sCOPUEND,
        //                      sCOOVSTR1,
        //                      sCOOVEND1,
        //                      sCOOVSTR2,
        //                      sCOOVEND2,
        //                      Get_Numeric(this.TXT01_COOVQTY.GetValue().ToString()),
        //                      this.TXT01_COCONTNO.GetValue().ToString(),
        //                      Get_Numeric(this.TXT01_SVBIJUNG.GetValue().ToString()),
        //                      Get_Numeric(this.TXT01_SVVCF.GetValue().ToString()),
        //                      Get_Numeric(this.TXT01_SVTEMP.GetValue().ToString()),
        //                      this.TXT01_SVTEMPGB.GetValue().ToString(),
        //                      this.TXT01_SVKESANGB.GetValue().ToString(),
        //                      this.TXT01_SVMOGB.GetValue().ToString(),
        //                      this.TXT01_SVMOTA.GetValue().ToString(),
        //                      Get_Numeric(fsCOOVAM.ToString()),
        //                      this.CBH01_SVHWAJU.GetText().ToString(),
        //                      Get_Date(DateTime.Now.ToString("yyyy-MM-dd").ToString()),
        //                      TYUserInfo.EmpNo.ToString().Trim().ToUpper()
        //                      );

        //    UP_UTISURVF_TAB_SEARCH(this.DTP01_SVIPHANG.GetValue().ToString(),
        //                               this.CBH01_SVBONSUN.GetValue().ToString(),
        //                               this.CBH01_SVHWAJU.GetValue().ToString(),
        //                               this.CBH01_SVHWAMUL.GetValue().ToString(),
        //                               ""
        //                               );

        //    UP_UTISURVF_BTN_DISPLAY("TAB");
        //}
        //#endregion

        #region Description : 삭제 버튼
        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            UP_UTISURVF_COMPUTE();

            UP_UTISURVF_DELETE();

            UP_UTISURVF_TAB_SEARCH(this.DTP01_SVIPHANG.GetValue().ToString(),
                                   this.CBH01_SVBONSUN.GetValue().ToString(),
                                   this.CBH01_SVHWAJU.GetValue().ToString(),
                                   this.CBH01_SVHWAMUL.GetValue().ToString(),
                                   ""
                                   );

            // SURVEY REPORT 전체 조회
            UP_UTISURVF_SEARCH();

            UP_UTISURVF_BTN_DISPLAY("TAB");
        }
        #endregion

        #region Description : BATCH 처리
        private void UP_UTISURVF_Batch(string sWORKGN,    string sSVIPHANG,  string sSVBONSUN,
                                       string sSVHWAJU,   string sSVHWAMUL,  string sHMDESC1,
                                       string sSVGUMJUNG, string sSVTANKNO,  string sSVMTQTY,
                                       string sSVKLQTY,   string sCOPUSTR1,  string sCOPUEND1,
                                       string sCOPUSTR2,  string sCOPUEND2,  string sCOPUSTR3,
                                       string sCOPUEND3,  string sCOWKTIME,  string sCOOVSTR1,
                                       string sCOOVEND1,  string sCOOVSTR2,  string sCOOVEND2,
                                       string sCOOVSTR3,  string sCOOVEND3,  string sCOOWKTIME,
                                       string sCOOVQTY,   string sCOCONTNO,  string sSVBIJUNG,
                                       string sSVVCF,     string sSVTEMP,    string sSVTEMPGB,
                                       string sSVKESANGB, string sSVMOGB,    string sSVMOTA,
                                       string sCOOVAM,    string sHJDESC1,   string sDATE,
                                       string sSABUN)
        {
            string sKBHANGL = string.Empty;
			string sVSHMGB  = string.Empty;
			string sCHECK   = string.Empty;
			string sWATER   = string.Empty;
			string sJSSEQ   = string.Empty;
			string sGUBUN   = string.Empty;

            DataTable dt = new DataTable();

            // UTISURVF
            if (sWORKGN.ToString() == "ADD")
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677FJ645",
                                        sSVIPHANG.ToString(),
                                        sSVBONSUN.ToString(),
                                        sSVHWAJU.ToString(),
                                        sSVHWAMUL.ToString(),
                                        sSVTANKNO.ToString(),
                                        sSVGUMJUNG.ToString(),
                                        sSVMTQTY.ToString(),
                                        sSVKLQTY.ToString(),
                                        sSVBIJUNG.ToString(),
                                        sSVVCF.ToString(),
                                        sSVTEMP.ToString(),
                                        sSVTEMPGB.ToString(),
                                        sSVKESANGB.ToString(),
                                        sSVMOGB.ToString(),
                                        sSVMOTA.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677FG644",
                                        sSVGUMJUNG.ToString(),
                                        sSVMTQTY.ToString(),
                                        sSVKLQTY.ToString(),
                                        sSVBIJUNG.ToString(),
                                        sSVVCF.ToString(),
                                        sSVTEMP.ToString(),
                                        sSVTEMPGB.ToString(),
                                        sSVKESANGB.ToString(),
                                        sSVMOGB.ToString(),
                                        sSVMOTA.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        sSVIPHANG.ToString(),
                                        sSVBONSUN.ToString(),
                                        sSVHWAJU.ToString(),
                                        sSVHWAMUL.ToString(),
                                        sSVTANKNO.ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // 화물비중화일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677D4619",
                sSVTANKNO.ToString().Trim(),
                sSVHWAMUL.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            // UTAHMBDF
            if (sGUBUN == "INS")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DD620",
                                        sSVTANKNO,
                                        sSVHWAMUL,
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DE621",
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG,
                                        sSVTANKNO.ToString().Trim(),
                                        sSVHWAMUL
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // 화물비중 파일 Check
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677DN623",
                sSVHWAMUL.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            // UTAHMBJF
            if (sGUBUN == "INS")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DR631",
                                        sSVHWAMUL,
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DQ630",
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG,
                                        sSVHWAMUL
                                        );

                this.DbConnector.ExecuteNonQuery();
            }




            if (sSVTANKNO.ToString().Trim() != "5007")
            {
                // TANK 지시 번호 자동부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677DT632",
                    sDATE
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sGUBUN = "UPT";
                }
                else
                {
                    sGUBUN = "INS";
                }

                // UTAJINOF
                if (sGUBUN == "INS")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677DX634",
                                            sDATE
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677DY635",
                                            sDATE
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }

                // 사원명을 가져옴
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677ES637",
                    TYUserInfo.EmpNo.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sKBHANGL = dt.Rows[0]["KBHANGL"].ToString();
                }

                // 탱크 지시순번을 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677DT632",
                    sDATE
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sJSSEQ = dt.Rows[0]["JSSEQ"].ToString();
                }

                // 탱크지시 (일자,지시순번)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677F9639",
                    sDATE.ToString(),
                    sJSSEQ
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sGUBUN = "UPT";
                }
                else
                {
                    sGUBUN = "INS";
                }

                // UTATKJIF
                if (sGUBUN == "INS")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677F5641",
                                            sDATE,
                                            sJSSEQ,
                                            sSVHWAMUL,
                                            sHMDESC1,
                                            sSABUN,
                                            sKBHANGL,
                                            sSVTANKNO,
                                            sSVBIJUNG,
                                            sSVVCF
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677FD643",
                                            sSVHWAMUL,
                                            sHMDESC1,
                                            sSABUN,
                                            sKBHANGL,
                                            sSVTANKNO,
                                            sSVBIJUNG,
                                            sSVVCF,
                                            sDATE,
                                            sJSSEQ
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }

            // UTICOMEF(매출입고할증파일) 저장
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677FK646",
                sSVIPHANG,
                sSVBONSUN,
                sSVHWAJU,
                sSVHWAMUL,
                sSVTANKNO.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            // UTICOMEF
            if (sGUBUN == "UPT")
            {
                if (fsSVMOGB.ToString() == "" || sSVMOGB.ToString() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677FO648",
                                            sSVMTQTY,
                                            sSVKLQTY,
                                            sCOCONTNO,
                                            sCOPUSTR1,
                                            sCOPUEND1,
                                            sCOPUSTR2,
                                            sCOPUEND2,
                                            sCOPUSTR3,
                                            sCOPUEND3,
                                            sCOWKTIME,
                                            sCOOVSTR1,
                                            sCOOVEND1,
                                            sCOOVSTR2,
                                            sCOOVEND2,
                                            sCOOVSTR3,
                                            sCOOVEND3,
                                            sCOOWKTIME,
                                            sCOOVQTY,
                                            sCOOVAM,
                                            sSABUN,
                                            sSVIPHANG,
                                            sSVBONSUN,
                                            sSVHWAJU,
                                            sSVHWAMUL,
                                            sSVTANKNO.ToString().Trim()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                    
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677FR650",
                                        sSVIPHANG,
                                        sSVBONSUN,
                                        sSVHWAJU,
                                        sSVHWAMUL,
                                        sSVTANKNO,
                                        sSVMTQTY,
                                        sSVKLQTY,
                                        sCOCONTNO,
                                        sCOPUSTR1,
                                        sCOPUEND1,
                                        sCOPUSTR2,
                                        sCOPUEND2,
                                        sCOPUSTR3,
                                        sCOPUEND3,
                                        sCOWKTIME,
                                        sCOOVSTR1,
                                        sCOOVEND1,
                                        sCOOVSTR2,
                                        sCOOVEND2,
                                        sCOOVSTR3,
                                        sCOOVEND3,
                                        sCOOWKTIME,
                                        sCOOVQTY,
                                        sCOOVAM,
                                        sSABUN
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            if (sSVHWAMUL.ToUpper() == "A12")
            {
                sCHECK = "*";
            }

            // 화물구분을 가져옴
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677FT651",
                sSVIPHANG,
                sSVBONSUN
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sVSHMGB = dt.Rows[0]["VSHMGB"].ToString();
            }


            // UTITANKF
            if (sWORKGN.ToString() == "ADD")
            {
                // 화주정보 추가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C1B9E996",
                                        sSVIPHANG,
                                        sSVBONSUN,
                                        sSVHWAJU,
                                        sHJDESC1,
                                        sSVHWAMUL,
                                        sVSHMGB,
                                        sSVBIJUNG,
                                        sSVVCF,
                                        sSVTEMP,
                                        sSVTEMPGB,
                                        sSVKESANGB,
                                        sSABUN,
                                        sSVTANKNO.ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();

                // 원본
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_UT_677G2654",
                //                        sSVIPHANG,
                //                        sSVBONSUN,
                //                        sSVHWAMUL,
                //                        sVSHMGB,
                //                        sSVBIJUNG,
                //                        sSVVCF,
                //                        sSVTEMP,
                //                        sSVTEMPGB,
                //                        sSVKESANGB,
                //                        sSABUN,
                //                        sSVTANKNO.ToString().Trim()
                //                        );

                //this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677G5655",
                                        sSVBIJUNG,
                                        sSVVCF,
                                        sSVTEMP,
                                        sSVTEMPGB,
                                        sSVKESANGB,
                                        sSABUN,
                                        sSVTANKNO.ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }



            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_675B7565",
                sSVTANKNO.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sWATER = dt.Rows[0]["TNWATER"].ToString();
            }

            // UTISENDF
            if (sCHECK == "*")
            {
                // 8.자동화에 자료 전달
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677GD656",
                    sSVTANKNO.ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677GG658",
                                            sSVBIJUNG,
                                            sWATER,
                                            sSVHWAJU,
                                            sHJDESC1,
                                            sSVHWAMUL,
                                            sHMDESC1,
                                            sSVTANKNO.ToString().Trim()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677GF657",
                                            sSVTANKNO,
                                            sSVBIJUNG,
                                            sWATER,
                                            sSVHWAJU,
                                            sHJDESC1,
                                            sSVHWAMUL,
                                            sHMDESC1
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }

            if (sSVTANKNO.ToString().Trim() != "5007")
            {
                /* 오라클 자동화 업데이트 */
                UP_Save_Oracle(sDATE, sJSSEQ);
            }

            // 메세지
            if (sWORKGN.ToString() == "ADD")
            {
                this.ShowMessage("TY_M_GB_23NAD873");
            }
            else
            {
                this.ShowMessage("TY_M_MR_2BD3Z286");
            }
        }
        #endregion

        #region Description : 삭제 메소드
        private void UP_UTISURVF_DELETE()
        {
            // SURVEY REPORT 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_678G0681",
                                    Get_Date(this.DTP01_SVIPHANG.GetValue().ToString()),
                                    this.CBH01_SVBONSUN.GetValue().ToString(),
                                    this.CBH01_SVHWAJU.GetValue().ToString(),
                                    this.CBH01_SVHWAMUL.GetValue().ToString(),
                                    this.CBO01_SVTANKNO.GetValue().ToString().Trim()
                                    );

            this.DbConnector.ExecuteNonQuery();

            // 매출입고할증 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_678G3683",
                                    Get_Date(this.DTP01_SVIPHANG.GetValue().ToString()),
                                    this.CBH01_SVBONSUN.GetValue().ToString(),
                                    this.CBH01_SVHWAJU.GetValue().ToString(),
                                    this.CBH01_SVHWAMUL.GetValue().ToString(),
                                    this.CBO01_SVTANKNO.GetValue().ToString().Trim()
                                    );

            this.DbConnector.ExecuteNonQuery();

            // SURVEY REPORT 삭제시 UTITANKF 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_678GA684",
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    this.CBO01_SVTANKNO.GetValue().ToString().Trim()
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 오라클 자동화 메소드
        private void UP_Save_Oracle(string sJISIIL, string sJSSEQ)
        {
            string sJISIHT = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677GK659",
                sJISIIL.ToString(),
                sJSSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sJISIHT = dt.Rows[i]["JISIHT"].ToString();

                    if (sJISIHT.ToString() == "6")
                    {
                        // 오라클 - 비중 변경 메소드
                        UP_ChangeDensity
                            (
                            dt.Rows[i]["JISIHM"].ToString(),
                            dt.Rows[i]["JISIHMNM"].ToString(),
                            dt.Rows[i]["JIVCF"].ToString(),
                            dt.Rows[i]["JIWCF"].ToString(),
                            dt.Rows[i]["JISITK"].ToString(),
                            dt.Rows[i]["JISIHJ"].ToString(),
                            dt.Rows[i]["JISIHJNM"].ToString()
                            );
                    }					
                }
            }
        }
        #endregion

        #region Description : 오라클 - 비중 변경 메소드
        private void UP_ChangeDensity(string sJISIHM,  string sJISIHMNM, string sJIVCF,
                                      string sJIWCF,   string sJISITK,   string sJISIHJ,
                                      string sJISIHJNM)
        {
            string sGUBUN  = string.Empty;
			string sHWAMUL = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677GU663",
                sJISIHM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            if (sJISIHM.ToString() == "A27")
            {
                sHWAMUL = "무수초산";
            }
            else
            {
                sHWAMUL = sJISIHMNM.ToString();
            }

            // 오라클 HMBJ
            if (sGUBUN == "INS")
            {
                // 오라클 HMBJ 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677GY666",
                                        sJISIHM.ToString(),
                                        sHWAMUL.ToString(),
                                        sJIWCF.ToString(),
                                        sJIVCF.ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 오라클 HMBJ 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677GW665",
                                        sHWAMUL.ToString(),
                                        sJIWCF.ToString(),
                                        sJIVCF.ToString(),
                                        sJISIHM.ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            
            sGUBUN = "";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677HL669",
                sJISITK.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (sHWAMUL.Length > 14)
                    {
                        sHWAMUL = sHWAMUL.Substring(0, 15).ToString();
                    }

                    // 오라클 TKST 화물 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677HN670",
                                            sJISIHM.ToString(),
                                            sHWAMUL.ToString(),
                                            sJIWCF.ToString(),
                                            sJIVCF.ToString(),
                                            sJISITK.ToString()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Description : SURVEY REPORT 조회
        private void UP_UTISURVF_TAB_SEARCH(string sSVIPHANG, string sSVBONSUN,
                                            string sSVHWAJU,  string sSVHWAMUL, string sSVTANKNO)
        {
            this.FPS91_TY_S_UT_674HK559.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_674HO560",
                sSVIPHANG.ToString(),
                sSVBONSUN.ToString(),
                sSVHWAJU.ToString(),
                sSVHWAMUL.ToString(),
                sSVTANKNO.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_674HK559.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_UT_674HK559.SetValue(dt);
            }
        }
        #endregion

        #region Description : SURVEY REPORT 전체 조회
        private void UP_UTISURVF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_671H7527",
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString(),
                this.CBH01_SBONSUN.GetValue().ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_671H3528.SetValue(dt);
        }
        #endregion

        #region Description : SURVERY REPORT 확인
        private void UP_UTISURVF_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_671HM529",
                this.DTP01_SVIPHANG.GetValue().ToString(),
                this.CBH01_SVBONSUN.GetValue().ToString(),
                this.CBH01_SVHWAJU.GetValue().ToString(),
                this.CBH01_SVHWAMUL.GetValue().ToString(),
                this.CBO01_SVTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsSVBIJUNG = dt.Rows[0]["SVBIJUNG"].ToString();
                fsSVMOGB   = dt.Rows[0]["SVMOGB"].ToString();

                this.CurrentDataTableRowMapping(dt, "01");

                fsWK_GUBUN3 = "UPT";

                UP_UTISURVF_BTN_DISPLAY(fsWK_GUBUN3);

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.TXT01_SVMTQTY);
                };

                tmr.Interval = 100;
                tmr.Start();
            }

            if (this.CBH01_SVBONSUN.GetValue().ToString().Substring(0, 2) == "TK")
            {
                this.TXT01_COOVQTY.SetReadOnly(false);
            }
            else
            {
                this.TXT01_COOVQTY.SetReadOnly(true);
            }

            // 값 저장
            UP_SET_Cookie3(this.DTP01_SVIPHANG.GetValue().ToString(), this.CBH01_SVBONSUN.GetValue().ToString(),
                           this.CBH01_SVHWAJU.GetValue().ToString(),  this.CBH01_SVHWAMUL.GetValue().ToString());
        }
        #endregion
        
        #region Description : 할증시간 처리
        private void UP_HALTIME_COMPUTE(string sWEEK, string sSTHH, string sEDHH)
        {
            string sCOPUSTR = string.Empty;
            string sCOPUEND = string.Empty;

            // 펌프시작 시간
            sCOPUSTR = Set_Fill2(this.TXT01_COPUSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR12.GetValue().ToString());
            // 펌프종료 시간
            sCOPUEND = Set_Fill2(this.TXT01_COPUEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND12.GetValue().ToString());

            if (sWEEK.ToString() == "1" || sWEEK.ToString() == "7") // 무조건 할증
            {
                this.TXT01_COOVSTR11.SetValue(this.TXT01_COPUSTR11.GetValue().ToString());
                this.TXT01_COOVSTR12.SetValue(this.TXT01_COPUSTR12.GetValue().ToString());

                this.TXT01_COOVEND11.SetValue(this.TXT01_COPUEND11.GetValue().ToString());
                this.TXT01_COOVEND12.SetValue(this.TXT01_COPUEND12.GetValue().ToString());
            }
            else
            {
                // sEDHH = 09시, sSTHH = 17,18시

                // 펌프 시작시작 <= 할증 종료시간 && 펌프 종료시간 < 할증 종료시간
                if (int.Parse(sCOPUSTR.ToString()) <= int.Parse(sEDHH.ToString()) && int.Parse(sCOPUEND.ToString()) <= int.Parse(sEDHH.ToString()))
                {
                    this.TXT01_COOVSTR11.SetValue(this.TXT01_COPUSTR11.GetValue().ToString());
                    this.TXT01_COOVSTR12.SetValue(this.TXT01_COPUSTR12.GetValue().ToString());

                    this.TXT01_COOVEND11.SetValue(this.TXT01_COPUEND11.GetValue().ToString());
                    this.TXT01_COOVEND12.SetValue(this.TXT01_COPUEND12.GetValue().ToString());
                }
                // 펌프 시작시작 <= 할증 종료시간 && 펌프 종료시간 > 할증 시작시간
                else if (int.Parse(sCOPUSTR.ToString()) <= int.Parse(sEDHH.ToString()) && int.Parse(sCOPUEND.ToString()) > int.Parse(sSTHH.ToString()))
                {
                    this.TXT01_COOVSTR11.SetValue(sSTHH.ToString().Substring(0,2));
                    this.TXT01_COOVSTR12.SetValue("00");

                    this.TXT01_COOVEND11.SetValue(sCOPUEND.ToString().Substring(0, 2));
                    this.TXT01_COOVEND12.SetValue(sCOPUEND.ToString().Substring(2, 2));
                }
                // 펌프 시작시작 <= 할증 종료시간 && 펌프 종료시간 > 할증 종료시간
                else if (int.Parse(sCOPUSTR.ToString()) <= int.Parse(sEDHH.ToString()) && int.Parse(sCOPUEND.ToString()) > int.Parse(sEDHH.ToString()))
                {
                    this.TXT01_COOVSTR11.SetValue(this.TXT01_COPUSTR11.GetValue().ToString());
                    this.TXT01_COOVSTR12.SetValue(this.TXT01_COPUSTR12.GetValue().ToString());

                    this.TXT01_COOVEND11.SetValue("09");
                    this.TXT01_COOVEND12.SetValue("00");
                }
                // 펌프 시작시작 > 할증 시작시간 && 펌프 종료시간 > 할증 시작시간
                else if (int.Parse(sCOPUSTR.ToString()) > int.Parse(sSTHH.ToString()) && int.Parse(sCOPUEND.ToString()) > int.Parse(sSTHH.ToString()))
                {
                    this.TXT01_COOVSTR11.SetValue(this.TXT01_COPUSTR11.GetValue().ToString());
                    this.TXT01_COOVSTR12.SetValue(this.TXT01_COPUSTR12.GetValue().ToString());

                    this.TXT01_COOVEND11.SetValue(this.TXT01_COPUEND11.GetValue().ToString());
                    this.TXT01_COOVEND12.SetValue(this.TXT01_COPUEND12.GetValue().ToString());
                }
                // 펌프 시작시작 >= 할증 시작시간 && 펌프 종료시간 < 할증 종료시간
                else if (int.Parse(sCOPUSTR.ToString()) >= int.Parse(sSTHH.ToString()) && int.Parse(sCOPUEND.ToString()) < int.Parse(sEDHH.ToString()))
                {
                    this.TXT01_COOVSTR11.SetValue(this.TXT01_COPUSTR11.GetValue().ToString());
                    this.TXT01_COOVSTR12.SetValue(this.TXT01_COPUSTR12.GetValue().ToString());

                    this.TXT01_COOVEND11.SetValue(this.TXT01_COPUEND11.GetValue().ToString());
                    this.TXT01_COOVEND12.SetValue(this.TXT01_COPUEND12.GetValue().ToString());
                }
                // 펌프 시작시작 <= 할증 시작시간 && 펌프 종료시간 > 할증 종료시간 && 펌프 종료시간 <= 할증 시작시간
                else if ((int.Parse(sCOPUSTR.ToString()) <= int.Parse(sEDHH.ToString())) &&
                         ((int.Parse(sCOPUEND.ToString()) > int.Parse(sEDHH.ToString()) && int.Parse(sCOPUEND.ToString()) <= int.Parse(sSTHH.ToString()))))
                {
                    this.TXT01_COOVSTR11.SetValue(this.TXT01_COPUSTR11.GetValue().ToString());
                    this.TXT01_COOVSTR12.SetValue(this.TXT01_COPUSTR12.GetValue().ToString());

                    this.TXT01_COOVEND11.SetValue(sEDHH.ToString().Substring(0, 2));
                    this.TXT01_COOVEND12.SetValue(sEDHH.ToString().Substring(2, 2));
                }
                // 펌프 시작시작 > 할증 종료시간 && 펌프 종료시간 >= 할증 시작시간
                else if (int.Parse(sCOPUSTR.ToString()) > int.Parse(sEDHH.ToString()) && int.Parse(sCOPUEND.ToString()) >= int.Parse(sSTHH.ToString()))
                {
                    this.TXT01_COOVSTR11.SetValue(sSTHH.ToString().Substring(0,2));
                    this.TXT01_COOVSTR12.SetValue("00");

                    this.TXT01_COOVEND11.SetValue(sCOPUEND.ToString().Substring(0, 2));
                    this.TXT01_COOVEND12.SetValue(sCOPUEND.ToString().Substring(2, 2));
                }
                // 펌프 시작시작 >= 할증 시작시간 && 펌프 종료시간 > 할증 종료시간
                else if (int.Parse(sCOPUSTR.ToString()) >= int.Parse(sSTHH.ToString()) && int.Parse(sCOPUEND.ToString()) > int.Parse(sEDHH.ToString()))
                {
                    this.TXT01_COOVSTR11.SetValue(this.TXT01_COPUSTR11.GetValue().ToString());
                    this.TXT01_COOVSTR12.SetValue(this.TXT01_COPUSTR12.GetValue().ToString());

                    this.TXT01_COOVEND11.SetValue(sEDHH.ToString().Substring(0, 2));
                    this.TXT01_COOVEND12.SetValue(sEDHH.ToString().Substring(2, 2));
                }
            }
        }
        #endregion

        #region Description : 계산
        private void UP_UTISURVF_COMPUTE()
        {
            string sAMOUNT  = string.Empty;
            string sAMOUNT1 = string.Empty;
            
            string sCNHANDAM = string.Empty;
            string sCNHANDDA = string.Empty;
            string sCNHANDHP = string.Empty;
            string sCNIPAM   = string.Empty;
            string sCNCHAM   = string.Empty;
            string sCNIPDA   = string.Empty;
            string sCNCHDA   = string.Empty;
            string sCNIPHP   = string.Empty;
            string sCNCHHP   = string.Empty;
            string sCNHANDOV = string.Empty;            


            sAMOUNT  = "0";
            sAMOUNT1 = "0";

            fsCMHWAPE = "";

            // 반올림
            fsCMFACT = Convert.ToString(double.Parse(Set_Numeric2(this.TXT01_SVKLQTY.GetValue().ToString(), 3)) / double.Parse(Set_Numeric2(this.TXT01_SVMTQTY.GetValue().ToString(), 3))).ToString();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            // 할증량
            if (double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3)) != 0)
            {
                string sKESAN = string.Empty;
                string sKESAN1 = string.Empty;

                sKESAN =
                    (
                    double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                    / double.Parse(Set_Numeric2(this.TXT01_SVMTQTY.GetValue().ToString(), 3))
                    * double.Parse(Set_Numeric2(this.TXT01_SVKLQTY.GetValue().ToString(), 3))
                    ).ToString("0.00000000");

                sKESAN1 = Convert.ToString(double.Parse(Set_Numeric2(sKESAN.ToString(), 3))).ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_676G5597",
                    this.TXT01_COCONTNO.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 취급료
                    sCNHANDAM = Get_Numeric(dt.Rows[0]["CNHANDAM"].ToString());
                    // 취급료단위
                    sCNHANDDA = dt.Rows[0]["CNHANDDA"].ToString().ToUpper();
                    // 취급료화폐
                    sCNHANDHP = dt.Rows[0]["CNHANDHP"].ToString().ToUpper();
                    // 입고금액
                    sCNIPAM   = dt.Rows[0]["CNIPAM"].ToString().ToUpper();
                    // 출고금액
                    sCNCHAM   = dt.Rows[0]["CNCHAM"].ToString().ToUpper();
                    // 입고단위
                    sCNIPDA   = dt.Rows[0]["CNIPDA"].ToString().ToUpper();
                    // 출고단위
                    sCNCHDA   = dt.Rows[0]["CNCHDA"].ToString().ToUpper();
                    // 입고화폐
                    sCNIPHP   = dt.Rows[0]["CNIPHP"].ToString().ToUpper();
                    // 출고화폐
                    sCNCHHP   = dt.Rows[0]["CNCHHP"].ToString().ToUpper();
                    // 취급료할증율
                    sCNHANDOV = dt.Rows[0]["CNHANDOV"].ToString().ToUpper();

                    if (sCNHANDAM.ToString() != "0")
                    {
                        if (sCNHANDDA.ToString() == "MT")
                        {
                            sAMOUNT = Convert.ToString(Set_Numeric2
                                (Convert.ToString
                                (Decimal.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                * Decimal.Parse(Set_Numeric2(sCNHANDAM.ToString(), 2))), 3));
                        }
                        else
                        {
                            sAMOUNT = Convert.ToString(Set_Numeric2
                                (Convert.ToString
                                (Decimal.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                * Decimal.Parse(Set_Numeric2(sCNHANDAM.ToString(), 2))), 3));
                        }                        

                        fsCMHWAPE = sCNHANDHP.ToString();
                    }
                    else
                    {
                        
                        if (Get_Numeric(sCNIPAM.ToString()) != "0" && Get_Numeric(sCNCHAM.ToString()) != "0")
                        {
                            if (sCNIPDA.ToString().ToUpper() == "MT")
                            {
                                sAMOUNT =
                                    (
                                    double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                    ).ToString("0.000");
                            }
                            else
                            {
                                sAMOUNT =
                                    (
                                    double.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                    ).ToString("0.000");
                            }

                            fsCMHWAPE = sCNIPHP.ToString();
                        }
                        else
                        {
                            if (Get_Numeric(sCNIPAM.ToString()) == "0")
                            {
                                if (sCNCHDA.ToString().ToUpper() == "MT")
                                {
                                    if (Get_Numeric(sCNCHAM.ToString()) != "0")
                                    {
                                        sAMOUNT =
                                            (
                                            double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNCHAM.ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                            ).ToString("0.000");
                                    }
                                }
                                else
                                {
                                    if (Get_Numeric(sCNCHAM.ToString()) != "0")
                                    {
                                        sAMOUNT =
                                            (
                                            double.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNCHAM.ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                            ).ToString("0.000");
                                    }
                                }

                                fsCMHWAPE = sCNCHHP.ToString();
                            }
                            else
                            {
                                if (sCNIPDA.ToString().ToUpper() == "MT")
                                {
                                    if (Get_Numeric(sCNHANDOV.ToString()) != "0")
                                    {
                                        if (Get_Numeric(sCNIPAM.ToString()) != "0")
                                        {
                                            sAMOUNT =
                                                (
                                                double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                                ).ToString("0.000");
                                        }
                                    }
                                    else
                                    {
                                        if (Get_Numeric(sCNIPAM.ToString()) != "0")
                                        {
                                            sAMOUNT =
                                                (
                                                double.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                                ).ToString("0.000");
                                        }
                                    }

                                    fsCMHWAPE = sCNIPHP.ToString();
                                }
                            }
                        }
                    }
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677BJ618",
                this.DTP01_SVIPHANG.GetValue().ToString(),
                this.CBH01_SVBONSUN.GetValue().ToString(),
                this.CBH01_SVHWAJU.GetValue().ToString(),
                this.CBH01_SVHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsCMHWAPE = dt.Rows[0]["CMHWAPE"].ToString();
            }

            if (fsCMHWAPE.ToString() == "" || fsCMHWAPE.ToString() == "2")
            {
                fsCOOVAM = sAMOUNT.ToString();
            }
            else
            {
                sAMOUNT1 = UP_DotDelete(sAMOUNT.ToString()).ToString();
                fsCOOVAM = sAMOUNT1.ToString();
            }

            fsCOOVAM = UP_DotDelete(Convert.ToString(double.Parse(fsCOOVAM.ToString()) * 100));
            fsCOOVAM = Convert.ToString(double.Parse(fsCOOVAM.ToString()) / 100);

            this.TXT01_COOVAM.SetValue(fsCOOVAM.ToString());
        }
        #endregion

        #region Description : SURVEY REPORT 스프레드 이벤트
        private void FPS91_TY_S_UT_674HK559_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_MESSAGE3.SetValue("");

            this.DTP01_SVIPHANG.SetReadOnly(true);
            this.CBH01_SVBONSUN.SetReadOnly(true);
            this.CBH01_SVHWAJU.SetReadOnly(true);
            this.CBH01_SVHWAMUL.SetReadOnly(true);
            this.CBO01_SVTANKNO.SetReadOnly(true);

            this.DTP01_SVIPHANG.SetValue(this.FPS91_TY_S_UT_674HK559.GetValue("SVIPHANG").ToString());
            this.CBH01_SVBONSUN.SetValue(this.FPS91_TY_S_UT_674HK559.GetValue("SVBONSUN").ToString());
            this.CBH01_SVHWAJU.SetValue(this.FPS91_TY_S_UT_674HK559.GetValue("SVHWAJU").ToString());
            this.CBH01_SVHWAMUL.SetValue(this.FPS91_TY_S_UT_674HK559.GetValue("SVHWAMUL").ToString());

            // 탱크번호 가져오기
            UP_GET_SVTANKNO(this.FPS91_TY_S_UT_674HK559.GetValue("SVTANKNO").ToString());

            // SURVEY REPORT 확인
            UP_UTISURVF_RUN();
        }
        #endregion

        #region Description : SURVEY REPORT 전체-스프레드 이벤트
        private void FPS91_TY_S_UT_671H3528_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_SVIPHANG.SetReadOnly(true);
            this.CBH01_SVBONSUN.SetReadOnly(true);
            this.CBH01_SVHWAJU.SetReadOnly(true);
            this.CBH01_SVHWAMUL.SetReadOnly(true);
            this.CBO01_SVTANKNO.SetReadOnly(true);

            this.DTP01_SVIPHANG.SetValue(this.FPS91_TY_S_UT_671H3528.GetValue("SVIPHANG").ToString());
            this.CBH01_SVBONSUN.SetValue(this.FPS91_TY_S_UT_671H3528.GetValue("SVBONSUN").ToString());
            this.CBH01_SVHWAJU.SetValue(this.FPS91_TY_S_UT_671H3528.GetValue("SVHWAJU").ToString());
            this.CBH01_SVHWAMUL.SetValue(this.FPS91_TY_S_UT_671H3528.GetValue("SVHWAMUL").ToString());

            // 탱크번호 가져오기
            UP_GET_SVTANKNO(this.FPS91_TY_S_UT_671H3528.GetValue("SVTANKNO").ToString());
            

            // SURVEY REPORT 조회
            UP_UTISURVF_TAB_SEARCH(this.DTP01_SVIPHANG.GetValue().ToString(),
                                   this.CBH01_SVBONSUN.GetValue().ToString(),
                                   this.CBH01_SVHWAJU.GetValue().ToString(),
                                   this.CBH01_SVHWAMUL.GetValue().ToString(),
                                   ""
                                   );

            // SURVEY REPORT 확인
            UP_UTISURVF_RUN();
        }
        #endregion

        #region Description : 탱크번호 가져오기
        private void UP_GET_SVTANKNO(string sSVTANKNO)
        {
            DataTable dt = new DataTable();

            this.CBO01_SVTANKNO.Initialize();

            // 콤보박스에 바인딩
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_674HT561",
                sSVTANKNO.ToString(),
                sSVTANKNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            // 콤보박스에 바인드
            this.CBO01_SVTANKNO.DataBind(dt, false);
        }
        #endregion

        #region Description : 입항조회 버튼
        private void BTN63_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            TYUTGB003S popup = new TYUTGB003S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_SVIPHANG.SetValue(popup.fsIPHANG); // 입항일자
                this.CBH01_SVBONSUN.SetValue(popup.fsBONSUN); // 본선명

                // 값 저장
                UP_SET_Cookie3(this.DTP01_SVIPHANG.GetValue().ToString(), this.CBH01_SVBONSUN.GetValue().ToString(),
                               this.CBH01_SVHWAJU.GetValue().ToString(),  this.CBH01_SVHWAMUL.GetValue().ToString());

                SetFocus(this.CBH01_SVHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 입고조회 버튼
        private void BTN63_UTTCODEHELP2_Click(object sender, EventArgs e)
        {
            TYUTGB004S popup = new TYUTGB004S(this.DTP01_SVIPHANG.GetValue().ToString(), this.CBH01_SVBONSUN.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_SVHWAJU.SetValue(popup.fsHWAJU);   // 화주
                this.CBH01_SVHWAMUL.SetValue(popup.fsHWAMUL); // 화물

                // 값 저장
                UP_SET_Cookie3(this.DTP01_SVIPHANG.GetValue().ToString(), this.CBH01_SVBONSUN.GetValue().ToString(),
                               this.CBH01_SVHWAJU.GetValue().ToString(),  this.CBH01_SVHWAMUL.GetValue().ToString());

                SetFocus(this.CBH01_SVHWAMUL.CodeText);
            }
        }
        #endregion

        #region Description : 계약번호 텍스트박스 이벤트
        private void TXT01_COCONTNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB005S popup = new TYUTGB005S(this.CBH01_SVHWAJU.GetValue().ToString(), this.CBH01_SVHWAMUL.GetValue().ToString(), this.CBO01_SVTANKNO.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_COCONTNO.SetValue(popup.fsCONTNO); // 계약번호

                    this.TXT01_SVBIJUNG.Focus();
                }
            }
        }
        #endregion

        #region Description : 계약번호 버튼
        private void BTN61_CONTNO_Click(object sender, EventArgs e)
        {
            TYUTGB005S popup = new TYUTGB005S(this.CBH01_SVHWAJU.GetValue().ToString(), this.CBH01_SVHWAMUL.GetValue().ToString(), this.CBO01_SVTANKNO.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_COCONTNO.SetValue(popup.fsCONTNO); // 계약번호

                this.TXT01_SVBIJUNG.Focus();
            }
        }
        #endregion

        #region Description : 화물 이벤트(KeyFocus)
        private void CBH01_SVHWAMUL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN63_OK.Visible == true)
                {
                    SetFocus(this.BTN63_OK);
                }
            }
        }
        #endregion

        #region Description : 검정사 이벤트(KeyFocus)
        private void CBH01_SVGUMJUNG_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : 탱크번호 이벤트
        private void CBO01_SVTANKNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // UTITANKF테이블에서 비중 및 VCF값 가져오기
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_675B7565",
                    this.CBO01_SVTANKNO.GetValue().ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_SVBIJUNG.SetValue(dt.Rows[0]["TNBIJUNG"].ToString());
                    this.TXT01_SVVCF.SetValue(dt.Rows[0]["TNVCF"].ToString());
                }

                // SURVEY - 계약번호 가져오기(계약일자가 제일 마지막인 계약번호 가져오기)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_77RBC281",
                    this.CBH01_SVHWAJU.GetValue().ToString(),
                    this.CBH01_SVHWAMUL.GetValue().ToString(),
                    this.CBO01_SVTANKNO.GetValue().ToString().Trim(),
                    this.DTP01_SVIPHANG.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_COCONTNO.SetValue(dt.Rows[0]["CNCONTNO"].ToString());
                }

                if (this.CBH01_SVBONSUN.GetValue().ToString().Substring(0, 2) == "TK")
                {
                    this.TXT01_COOVQTY.SetReadOnly(false);
                }
                else
                {
                    this.TXT01_COOVQTY.SetReadOnly(true);
                }
            }
        }

        private void CBO01_SVTANKNO_Leave(object sender, EventArgs e)
        {
        }
        #endregion

        #region Description : SURVEY 이벤트
        private void TXT01_SVMTQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_SVKLQTY);
            }
        }

        private void TXT01_SVKLQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_COCONTNO);
            }
        }

        private void TXT01_COCONTNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_SVBIJUNG);
            }
        }

        private void TXT01_SVBIJUNG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_SVVCF);
            }
        }

        private void TXT01_SVVCF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_COPUSTR11);
            }
        }

        private void TXT01_COPUEND12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_COPUSTR21);
            }
        }

        private void TXT01_COPUEND22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_COPUSTR31);
            }
        }

        private void TXT01_COPUEND32_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.CBH01_SVBONSUN.GetValue().ToString().Substring(0, 2) == "TK")
                {
                    SetFocus(this.TXT01_COOVQTY);
                }
                else
                {
                    SetFocus(this.TXT01_COOVSTR11);
                }
            }
        }

        private void TXT01_COOVQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_COOVSTR11);
            }
        }

        private void TXT01_COOVEND12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_COOVSTR21);
            }
        }

        private void TXT01_COOVEND22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_COOVSTR31);
            }
        }

        private void TXT01_COOVEND32_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // 작업시간 및 할증작업시간 계산
                UP_WORK_Compute();

                SetFocus(this.CBH01_SVGUMJUNG.CodeText);
            }
        }

        private void TXT01_SVMOGB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_SVMOTA);
            }
        }

        private void TXT01_SVMOTA_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : SURVEY REPORT 저장 ProcessCheck
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string  sCHAI     = string.Empty;
            decimal dCMSHQTY  = 0;
            decimal dSHOREQTY = 0;
            decimal dSVMTQTY  = 0;

            this.TXT01_MESSAGE3.SetValue("");

            DataTable dt  = new DataTable();
            DataTable dt1 = new DataTable();

            if (fsWK_GUBUN3.ToString() == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_671HM529",
                    this.DTP01_SVIPHANG.GetValue().ToString(),
                    this.CBH01_SVBONSUN.GetValue().ToString(),
                    this.CBH01_SVHWAJU.GetValue().ToString(),
                    this.CBH01_SVHWAMUL.GetValue().ToString(),
                    this.CBO01_SVTANKNO.GetValue().ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.CBO01_SVTANKNO.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString())) < 0)
            {
                this.ShowMessage("TY_M_UT_676EQ588");
                this.TXT01_SVMTQTY.Focus();

                e.Successed = false;
                return;
            }

            // 화면 MT량
            fsSHOREQTY = Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString());

            if (fsWK_GUBUN3.ToString() == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_676E1580",
                    this.DTP01_SVIPHANG.GetValue().ToString(),
                    this.CBH01_SVBONSUN.GetValue().ToString(),
                    this.CBH01_SVHWAJU.GetValue().ToString(),
                    this.CBH01_SVHWAMUL.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (Decimal.Parse(Get_Numeric(dt.Rows[0]["CMSHQTY"].ToString())) <
                        (Decimal.Parse(Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString())) +
                        Decimal.Parse(Get_Numeric(dt.Rows[0]["SVMTQTY"].ToString()))))
                    {
                        this.TXT01_MESSAGE3.SetValue("[안내] - 입고 파일의 SHORE량과 SURVEY의 MT량이 일치하지 않습니다.");
                        this.TXT01_SVMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                    else if (Decimal.Parse(Get_Numeric(dt.Rows[0]["CMSHQTY"].ToString())) >
                             (Decimal.Parse(Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString())) +
                              Decimal.Parse(Get_Numeric(dt.Rows[0]["SVMTQTY"].ToString()))))
                    {
                        dCMSHQTY = Decimal.Parse(Get_Numeric(dt.Rows[0]["CMSHQTY"].ToString()));
                        dSHOREQTY = Decimal.Parse(Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString()));
                        dSVMTQTY = Decimal.Parse(Get_Numeric(dt.Rows[0]["SVMTQTY"].ToString()));

                        sCHAI = Convert.ToString(dCMSHQTY - (dSHOREQTY + dSVMTQTY)).ToString();

                        this.TXT01_MESSAGE3.SetValue("[안내] - SURVEY의 MT량을 " + sCHAI + "만큼 더 입력하십시요.");
                    }
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_676E4581",
                        this.DTP01_SVIPHANG.GetValue().ToString(),
                        this.CBH01_SVBONSUN.GetValue().ToString(),
                        this.CBH01_SVHWAJU.GetValue().ToString(),
                        this.CBH01_SVHWAMUL.GetValue().ToString()
                        );

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count > 0)
                    {
                        dCMSHQTY = Decimal.Parse(Get_Numeric(dt1.Rows[0][0].ToString()));
                        dSHOREQTY = Decimal.Parse(Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString()));

                        if (dCMSHQTY > dSHOREQTY)
                        {
                            sCHAI = Convert.ToString(dCMSHQTY - dSHOREQTY).ToString();

                            this.TXT01_MESSAGE3.SetValue("[안내] - SURVEY의 MT량을 " + sCHAI + "만큼 더 입력하십시요");
                        }
                        else if (dCMSHQTY < dSHOREQTY)
                        {
                            sCHAI = Convert.ToString(dSHOREQTY - dCMSHQTY).ToString();

                            this.TXT01_MESSAGE3.SetValue("[안내] - SURVEY의 MT량이 " + sCHAI + "만큼 더 입력되었습니다.");
                        }
                    }
                }
            }

            string sCOPUSTR1 = string.Empty;
            string sCOPUEND1 = string.Empty;

            string sCOPUSTR2 = string.Empty;
            string sCOPUEND2 = string.Empty;

            string sCOPUSTR3 = string.Empty;
            string sCOPUEND3 = string.Empty;

            sCOPUSTR1 = Set_Fill2(this.TXT01_COPUSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR12.GetValue().ToString());
            sCOPUEND1 = Set_Fill2(this.TXT01_COPUEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND12.GetValue().ToString());

            sCOPUSTR2 = Set_Fill2(this.TXT01_COPUSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR22.GetValue().ToString());
            sCOPUEND2 = Set_Fill2(this.TXT01_COPUEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND22.GetValue().ToString());

            sCOPUSTR3 = Set_Fill2(this.TXT01_COPUSTR31.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR32.GetValue().ToString());
            sCOPUEND3 = Set_Fill2(this.TXT01_COPUEND31.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND32.GetValue().ToString());

            // 시간 유효성 체크
            // PUMP 시작시간1
            if ((double.Parse(sCOPUSTR1.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR11.Focus();

                e.Successed = false;
                return;
            }
            
            if ((double.Parse(sCOPUSTR1.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR12.Focus();

                e.Successed = false;
                return;
            }
            // PUMP 종료시간1
            if ((double.Parse(sCOPUEND1.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND11.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUEND1.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND12.Focus();

                e.Successed = false;
                return;
            }
            // PUMP 시작시간2
            if ((double.Parse(sCOPUSTR2.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR21.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUSTR2.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR22.Focus();

                e.Successed = false;
                return;
            }
            // PUMP 종료시간2
            if ((double.Parse(sCOPUEND2.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND21.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUEND2.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND22.Focus();

                e.Successed = false;
                return;
            }
            // PUMP 시작시간3
            if ((double.Parse(sCOPUSTR3.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR31.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUSTR3.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR32.Focus();

                e.Successed = false;
                return;
            }
            // PUMP 종료시간3
            if ((double.Parse(sCOPUEND3.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND31.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUEND3.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND32.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUSTR1.ToString()) > 2459) || (double.Parse(sCOPUSTR1.ToString()) < 0))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR11.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUEND1.ToString()) > 2459) || (double.Parse(sCOPUEND1.ToString()) < 0))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND11.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUSTR2.ToString()) > 2459) || (double.Parse(sCOPUSTR2.ToString()) < 0))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR21.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUEND2.ToString()) > 2459) || (double.Parse(sCOPUEND2.ToString()) < 0))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND21.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUSTR3.ToString()) > 2459) || (double.Parse(sCOPUSTR3.ToString()) < 0))
            {
                this.ShowMessage("TY_M_UT_676ES590");
                this.TXT01_COPUSTR31.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOPUEND3.ToString()) > 2459) || (double.Parse(sCOPUEND3.ToString()) < 0))
            {
                this.ShowMessage("TY_M_UT_676ES592");
                this.TXT01_COPUEND31.Focus();

                e.Successed = false;
                return;
            }

            // 계약번호 존재유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_676G5597",
                this.TXT01_COCONTNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_676GA598");
                this.TXT01_COCONTNO.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                if (this.CBH01_SVHWAMUL.GetValue().ToString() != dt.Rows[0]["CNHWAMUL"].ToString())
                {
                    this.ShowMessage("TY_M_UT_676GD600");
                    this.TXT01_COCONTNO.Focus();

                    e.Successed = false;
                    return;
                }

                if (this.CBO01_SVTANKNO.GetValue().ToString().Trim() != dt.Rows[0]["CNTANKNO"].ToString().Trim())
                {
                    this.ShowMessage("TY_M_UT_676GD601");
                    this.CBO01_SVTANKNO.Focus();

                    e.Successed = false;
                    return;
                }

                if (Int64.Parse(dt.Rows[0]["CNCONTEN"].ToString()) < Int64.Parse(Get_Date(this.DTP01_SVIPHANG.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_UT_676GG602");
                    this.CBO01_SVTANKNO.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 온도구분
            if (this.TXT01_SVTEMPGB.GetValue().ToString() != "")
            {
                if ((this.TXT01_SVTEMPGB.GetValue().ToString() != "C") && (this.TXT01_SVTEMPGB.GetValue().ToString() != "F"))
                {
                    this.ShowMessage("TY_M_UT_676GH603");

                    e.Successed = false;
                    return;
                }
            }

            // 이고 구분
            if (this.TXT01_SVMOGB.GetValue().ToString() != "")
            {
                // 이고 탱크
                if (this.TXT01_SVMOTA.GetValue().ToString() != "")
                {
                    // 탱크제원 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_676GL604",
                        this.CBO01_SVTANKNO.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_676GM605");

                        e.Successed = false;
                        return;
                    }
                }
            }


            string sCOOVSTR1 = string.Empty;
            string sCOOVEND1 = string.Empty;

            string sCOOVSTR2 = string.Empty;
            string sCOOVEND2 = string.Empty;

            string sCOOVSTR3 = string.Empty;
            string sCOOVEND3 = string.Empty;

            sCOOVSTR1 = Set_Fill2(this.TXT01_COOVSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR12.GetValue().ToString());
            sCOOVEND1 = Set_Fill2(this.TXT01_COOVEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString());

            sCOOVSTR2 = Set_Fill2(this.TXT01_COOVSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR22.GetValue().ToString());
            sCOOVEND2 = Set_Fill2(this.TXT01_COOVEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString());

            sCOOVSTR3 = Set_Fill2(this.TXT01_COOVSTR31.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR32.GetValue().ToString());
            sCOOVEND3 = Set_Fill2(this.TXT01_COOVEND31.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND32.GetValue().ToString());

            // KL량
            if (Double.Parse(Set_Numeric2(this.TXT01_SVKLQTY.GetValue().ToString(), 3)) < 0)
            {
                this.ShowMessage("TY_M_UT_676GS610");

                e.Successed = false;
                return;
            }

            // 시간 유효성 체크
            // 할증 시작시간1
            if ((double.Parse(sCOOVSTR1.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR11.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOOVSTR1.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR12.Focus();

                e.Successed = false;
                return;
            }
            // 할증 종료시간1
            if ((double.Parse(sCOOVEND1.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVEND11.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOOVEND1.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVEND12.Focus();

                e.Successed = false;
                return;
            }
            // 할증 시작시간2
            if ((double.Parse(sCOOVSTR2.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR21.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOOVSTR2.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR22.Focus();

                e.Successed = false;
                return;
            }
            // 할증 종료시간2
            if ((double.Parse(sCOOVEND2.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVEND21.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOOVEND2.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVEND22.Focus();

                e.Successed = false;
                return;
            }
            // 할증 시작시간3
            if ((double.Parse(sCOOVSTR3.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR31.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOOVSTR3.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR32.Focus();

                e.Successed = false;
                return;
            }
            // 할증 종료시간3
            if ((double.Parse(sCOOVEND3.ToString().Substring(0, 2)) > 24))
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVEND31.Focus();

                e.Successed = false;
                return;
            }

            if ((double.Parse(sCOOVEND3.ToString().Substring(2, 2)) > 59))
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVEND32.Focus();

                e.Successed = false;
                return;
            }

            // 할증시작시간1
            if ((Int32.Parse(sCOOVSTR1.ToString()) > 2459) || Int32.Parse(sCOOVSTR1.ToString()) < 0)
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR11.Focus();

                e.Successed = false;
                return;
            }

            // 할증종료시간1
            if ((Int32.Parse(sCOOVEND1.ToString()) > 2459) || Int32.Parse(sCOOVEND1.ToString()) < 0)
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVSTR11.Focus();

                e.Successed = false;
                return;
            }

            // 할증시작시간2
            if ((Int32.Parse(sCOOVSTR2.ToString()) > 2459) || Int32.Parse(sCOOVSTR2.ToString()) < 0)
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR21.Focus();

                e.Successed = false;
                return;
            }

            // 할증종료시간2
            if ((Int32.Parse(sCOOVEND2.ToString()) > 2459) || Int32.Parse(sCOOVEND2.ToString()) < 0)
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVSTR21.Focus();

                e.Successed = false;
                return;
            }

            // 할증시작시간3
            if ((Int32.Parse(sCOOVSTR3.ToString()) > 2459) || Int32.Parse(sCOOVSTR3.ToString()) < 0)
            {
                this.ShowMessage("TY_M_UT_676GR608");
                this.TXT01_COOVSTR31.Focus();

                e.Successed = false;
                return;
            }

            // 할증종료시간2
            if ((Int32.Parse(sCOOVEND3.ToString()) > 2459) || Int32.Parse(sCOOVEND3.ToString()) < 0)
            {
                this.ShowMessage("TY_M_UT_676GR609");
                this.TXT01_COOVSTR31.Focus();

                e.Successed = false;
                return;
            }


            // 작업시간 및 할증작업시간 구하기

            string sSTDATE    = string.Empty;
            string sEDDATE    = string.Empty;
            string sStartTime = string.Empty;
            string sEndTime   = string.Empty;

            double dAppTime   = 0;

            sSTDATE = this.DTP01_SVIPHANG.GetValue().ToString();
            sEDDATE = this.DTP01_SVIPHANG.GetValue().ToString();

            //if (this.TXT01_COPUEND11.GetValue().ToString() == "24")
            //{
            //    if (int.Parse(Set_Fill2(this.TXT01_COPUEND12.GetValue().ToString())) >= 1)
            //    {
            //        this.ShowMessage("TY_M_UT_676ES590");
            //        this.TXT01_COPUEND12.Focus();

            //        e.Successed = false;
            //        return;
            //    }
            //}


            //if (this.TXT01_COPUEND21.GetValue().ToString() == "24")
            //{
            //    if (int.Parse(Set_Fill2(this.TXT01_COPUEND22.GetValue().ToString())) >= 1)
            //    {
            //        this.ShowMessage("TY_M_UT_676ES590");
            //        this.TXT01_COPUEND22.Focus();

            //        e.Successed = false;
            //        return;
            //    }
            //}

            //if (this.TXT01_COPUEND31.GetValue().ToString() == "24")
            //{
            //    if (int.Parse(Set_Fill2(this.TXT01_COPUEND32.GetValue().ToString())) >= 1)
            //    {
            //        this.ShowMessage("TY_M_UT_676ES590");
            //        this.TXT01_COPUEND32.Focus();

            //        e.Successed = false;
            //        return;
            //    }
            //}


            //if (this.TXT01_COOVEND11.GetValue().ToString() == "24")
            //{
            //    if (int.Parse(Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString())) >= 1)
            //    {
            //        this.ShowMessage("TY_M_UT_676GR608");
            //        this.TXT01_COOVEND12.Focus();

            //        e.Successed = false;
            //        return;
            //    }
            //}

            //if (this.TXT01_COOVEND21.GetValue().ToString() == "24")
            //{
            //    if (int.Parse(Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString())) >= 1)
            //    {
            //        this.ShowMessage("TY_M_UT_676GR608");
            //        this.TXT01_COOVEND22.Focus();

            //        e.Successed = false;
            //        return;
            //    }
            //}

            //if (this.TXT01_COOVEND31.GetValue().ToString() == "24")
            //{
            //    if (int.Parse(Set_Fill2(this.TXT01_COOVEND32.GetValue().ToString())) >= 1)
            //    {
            //        this.ShowMessage("TY_M_UT_676GR608");
            //        this.TXT01_COOVEND32.Focus();

            //        e.Successed = false;
            //        return;
            //    }
            //}

            // 작업시간 및 할증작업시간 계산
            UP_WORK_Compute();

            // 할증량
            if (double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3)) > double.Parse(Set_Numeric2(this.TXT01_SVMTQTY.GetValue().ToString(), 3)))
            {
                this.ShowMessage("TY_M_UT_676GQ607");
                this.TXT01_COOVQTY.Focus();

                e.Successed = false;
                return;
            }

            // 계산방법
            if (this.TXT01_SVKESANGB.GetValue().ToString() != "1" && this.TXT01_SVKESANGB.GetValue().ToString() != "2" && this.TXT01_SVKESANGB.GetValue().ToString() != "3" &&
                this.TXT01_SVKESANGB.GetValue().ToString() != "4" && this.TXT01_SVKESANGB.GetValue().ToString() != "5" && this.TXT01_SVKESANGB.GetValue().ToString() != "6")
            {
                this.ShowMessage("TY_M_UT_676GP606");

                e.Successed = false;
                return;
            }

            fsCOOVAM = this.TXT01_COOVAM.GetValue().ToString();

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 작업시간 및 할증작업시간 계산
        private void UP_WORK_Compute()
        {
            // 1. 20180131 박동근 차장님 요청
            //    탱크로리가 아닌 경우만 작업시간, 할증작업시간, 할증량 계산 안함
            //    할증량을 수동으로 수정함
            if (this.CBH01_SVBONSUN.GetValue().ToString().Substring(0, 2) != "TK")
            {
                decimal dSVMTQTY = 0;

                // 작업시간 및 할증작업시간 구하기

                string sSTDATE = string.Empty;
                string sEDDATE = string.Empty;
                string sStartTime = string.Empty;
                string sEndTime = string.Empty;

                string sENDATE = string.Empty;
                
                double dAppTime = 0;

                sSTDATE = this.DTP01_SVIPHANG.GetValue().ToString();
                sEDDATE = this.DTP01_SVIPHANG.GetValue().ToString();

                sENDATE = this.DTP01_SVIPHANG.GetValue().ToString();



                #region Description : 작업시간 구하기

                sStartTime = "";
                sEndTime = "";
                string sParamDate = string.Empty;

                DateTime dt_End = new DateTime();

                sStartTime = "";
                sEndTime = "";

                if (Set_Fill2(this.TXT01_COPUSTR11.GetValue().ToString()) == "24")
                {
                    sStartTime = "00" + Set_Fill2(this.TXT01_COPUSTR12.GetValue().ToString());
                }
                else
                {
                    sStartTime = Set_Fill2(this.TXT01_COPUSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR12.GetValue().ToString());
                }

                if (Set_Fill2(this.TXT01_COPUEND11.GetValue().ToString()) == "24")
                {
                    sEndTime = "00" + Set_Fill2(this.TXT01_COPUEND12.GetValue().ToString());
                }
                else
                {
                    sEndTime = Set_Fill2(this.TXT01_COPUEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND12.GetValue().ToString());
                }

                if (sStartTime.ToString() != "0000" || sEndTime.ToString() != "0000")
                {
                    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
                    {
                        sParamDate = sEDDATE.ToString().Substring(0, 4) + "-" +
                                     sEDDATE.ToString().Substring(4, 2) + "-" +
                                     sEDDATE.ToString().Substring(6, 2);

                        dt_End = Convert.ToDateTime(sParamDate.ToString());

                        dt_End = dt_End.AddDays(+1);

                        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
                    }

                    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
                    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);
                    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;
                    
                    dAppTime = Convert.ToDouble(timeSpan.TotalMinutes.ToString());
                }
                else if (Set_Fill2(this.TXT01_COPUSTR11.GetValue().ToString()) == "00" && Set_Fill2(this.TXT01_COPUSTR12.GetValue().ToString()) == "00" &&
                        Set_Fill2(this.TXT01_COPUEND11.GetValue().ToString()) == "24" && Set_Fill2(this.TXT01_COPUEND12.GetValue().ToString()) == "00")
                {
                    dAppTime = dAppTime + 1440;
                }

                sStartTime = "";
                sEndTime = "";

                if (Set_Fill2(this.TXT01_COPUSTR21.GetValue().ToString()) == "24")
                {
                    sStartTime = "00" + Set_Fill2(this.TXT01_COPUSTR22.GetValue().ToString());
                }
                else
                {
                    sStartTime = Set_Fill2(this.TXT01_COPUSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR22.GetValue().ToString());
                }

                if (Set_Fill2(this.TXT01_COPUEND21.GetValue().ToString()) == "24")
                {
                    sEndTime = "00" + Set_Fill2(this.TXT01_COPUEND22.GetValue().ToString());
                }
                else
                {
                    sEndTime = Set_Fill2(this.TXT01_COPUEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND22.GetValue().ToString());
                }

                if (sStartTime.ToString() != "0000" || sEndTime.ToString() != "0000")
                {
                    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
                    {
                        sParamDate = sENDATE.ToString().Substring(0, 4) + "-" +
                                     sENDATE.ToString().Substring(4, 2) + "-" +
                                     sENDATE.ToString().Substring(6, 2);

                        dt_End = Convert.ToDateTime(sParamDate.ToString());

                        dt_End = dt_End.AddDays(+1);

                        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
                    }
                    // 수정 테스트중 시작
                    if(sStartTime == "0000")
                    {
                        sSTDATE = sENDATE;
                    }
                    // 수정 테스트중 종료

                    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
                    
                    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);
                    
                    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

                    dAppTime = dAppTime + Convert.ToDouble(timeSpan.TotalMinutes.ToString());
                }
                else if (Set_Fill2(this.TXT01_COPUSTR21.GetValue().ToString()) == "00" && Set_Fill2(this.TXT01_COPUSTR22.GetValue().ToString()) == "00" &&
                        Set_Fill2(this.TXT01_COPUEND21.GetValue().ToString()) == "24" && Set_Fill2(this.TXT01_COPUEND22.GetValue().ToString()) == "00")
                {
                    dAppTime = dAppTime + 1440;
                }

                sStartTime = "";
                sEndTime = "";

                if (Set_Fill2(this.TXT01_COPUSTR31.GetValue().ToString()) == "24")
                {
                    sStartTime = "00" + Set_Fill2(this.TXT01_COPUSTR32.GetValue().ToString());
                }
                else
                {
                    sStartTime = Set_Fill2(this.TXT01_COPUSTR31.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR32.GetValue().ToString());
                }

                if (Set_Fill2(this.TXT01_COPUEND31.GetValue().ToString()) == "24")
                {
                    sEndTime = "00" + Set_Fill2(this.TXT01_COPUEND32.GetValue().ToString());
                }
                else
                {
                    sEndTime = Set_Fill2(this.TXT01_COPUEND31.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND32.GetValue().ToString());
                }

                if (sStartTime.ToString() != "0000" || sEndTime.ToString() != "0000")
                {
                    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
                    {
                        sParamDate = sENDATE.ToString().Substring(0, 4) + "-" +
                                     sENDATE.ToString().Substring(4, 2) + "-" +
                                     sENDATE.ToString().Substring(6, 2);

                        dt_End = Convert.ToDateTime(sParamDate.ToString());

                        dt_End = dt_End.AddDays(+1);

                        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
                    }
                    // 수정 테스트중 시작
                    if (sStartTime == "0000")
                    {
                        sSTDATE = sENDATE;
                    }
                    // 수정 테스트중 종료

                    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
                    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);

                    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

                    dAppTime = dAppTime + Convert.ToDouble(timeSpan.TotalMinutes.ToString());
                }
                else if (Set_Fill2(this.TXT01_COPUSTR31.GetValue().ToString()) == "00" && Set_Fill2(this.TXT01_COPUSTR32.GetValue().ToString()) == "00" &&
                        Set_Fill2(this.TXT01_COPUEND31.GetValue().ToString()) == "24" && Set_Fill2(this.TXT01_COPUEND32.GetValue().ToString()) == "00")
                {
                    dAppTime = dAppTime + 1440;
                }

                this.TXT01_COWKTIME.SetValue(Convert.ToString(dAppTime));

                #endregion

                #region Description : 할증작업시간 구하기

                dAppTime = 0;

                sStartTime = "";
                sEndTime = "";
                // 수정 테스트중 시작
                sSTDATE = this.DTP01_SVIPHANG.GetValue().ToString();
                sEDDATE = this.DTP01_SVIPHANG.GetValue().ToString();

                sENDATE = this.DTP01_SVIPHANG.GetValue().ToString();
                // 수정 테스트중 종료

                if (Set_Fill2(this.TXT01_COOVSTR11.GetValue().ToString()) == "24")
                {
                    sStartTime = "00" + Set_Fill2(this.TXT01_COOVSTR12.GetValue().ToString());
                }
                else
                {
                    sStartTime = Set_Fill2(this.TXT01_COOVSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR12.GetValue().ToString());
                }

                if (Set_Fill2(this.TXT01_COOVEND11.GetValue().ToString()) == "24")
                {
                    sEndTime = "00" + Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString());
                }
                else
                {
                    sEndTime = Set_Fill2(this.TXT01_COOVEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString());
                }

                if (sStartTime.ToString() != "0000" || sEndTime.ToString() != "0000")
                {
                    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
                    {
                        sParamDate = sEDDATE.ToString().Substring(0, 4) + "-" +
                                     sEDDATE.ToString().Substring(4, 2) + "-" +
                                     sEDDATE.ToString().Substring(6, 2);

                        dt_End = Convert.ToDateTime(sParamDate.ToString());

                        dt_End = dt_End.AddDays(+1);

                        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
                    }

                    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
                    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);

                    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

                    dAppTime = Convert.ToDouble(timeSpan.TotalMinutes.ToString());
                }
                else if (Set_Fill2(this.TXT01_COOVSTR11.GetValue().ToString()) == "00" && Set_Fill2(this.TXT01_COOVSTR12.GetValue().ToString()) == "00" &&
                        Set_Fill2(this.TXT01_COOVEND11.GetValue().ToString()) == "24" && Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString()) == "00")
                {
                    dAppTime = dAppTime + 1440;
                }

                sStartTime = "";
                sEndTime = "";

                if (Set_Fill2(this.TXT01_COOVSTR21.GetValue().ToString()) == "24")
                {
                    sStartTime = "00" + Set_Fill2(this.TXT01_COOVSTR22.GetValue().ToString());
                }
                else
                {
                    sStartTime = Set_Fill2(this.TXT01_COOVSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR22.GetValue().ToString());
                }

                if (Set_Fill2(this.TXT01_COOVEND21.GetValue().ToString()) == "24")
                {
                    sEndTime = "00" + Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString());
                }
                else
                {
                    sEndTime = Set_Fill2(this.TXT01_COOVEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString());
                }

                if (sStartTime.ToString() != "0000" || sEndTime.ToString() != "0000")
                {
                    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
                    {
                        sParamDate = sENDATE.ToString().Substring(0, 4) + "-" +
                                     sENDATE.ToString().Substring(4, 2) + "-" +
                                     sENDATE.ToString().Substring(6, 2);

                        dt_End = Convert.ToDateTime(sParamDate.ToString());

                        dt_End = dt_End.AddDays(+1);

                        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
                    }
                    // 수정 테스트중 시작
                    if (sStartTime == "0000")
                    {
                        sSTDATE = sENDATE;
                    }
                    // 수정 테스트중 종료

                    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
                    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);

                    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

                    dAppTime = dAppTime + Convert.ToDouble(timeSpan.TotalMinutes.ToString());
                }
                else if (Set_Fill2(this.TXT01_COOVSTR21.GetValue().ToString()) == "00" && Set_Fill2(this.TXT01_COOVSTR22.GetValue().ToString()) == "00" &&
                        Set_Fill2(this.TXT01_COOVEND21.GetValue().ToString()) == "24" && Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString()) == "00")
                {
                    dAppTime = dAppTime + 1440;
                }

                sStartTime = "";
                sEndTime = "";

                if (Set_Fill2(this.TXT01_COOVSTR31.GetValue().ToString()) == "24")
                {
                    sStartTime = "00" + Set_Fill2(this.TXT01_COOVSTR32.GetValue().ToString());
                }
                else
                {
                    sStartTime = Set_Fill2(this.TXT01_COOVSTR31.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR32.GetValue().ToString());
                }

                if (Set_Fill2(this.TXT01_COOVEND31.GetValue().ToString()) == "24")
                {
                    sEndTime = "00" + Set_Fill2(this.TXT01_COOVEND32.GetValue().ToString());
                }
                else
                {
                    sEndTime = Set_Fill2(this.TXT01_COOVEND31.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND32.GetValue().ToString());
                }

                if (sStartTime.ToString() != "0000" || sEndTime.ToString() != "0000")
                {
                    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
                    {
                        sParamDate = sENDATE.ToString().Substring(0, 4) + "-" +
                                     sENDATE.ToString().Substring(4, 2) + "-" +
                                     sENDATE.ToString().Substring(6, 2);

                        dt_End = Convert.ToDateTime(sParamDate.ToString());

                        dt_End = dt_End.AddDays(+1);

                        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
                    }
                    // 수정 테스트중 시작
                    if (sStartTime == "0000")
                    {
                        sSTDATE = sENDATE;
                    }
                    // 수정 테스트중 종료

                    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
                    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);

                    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

                    dAppTime = dAppTime + Convert.ToDouble(timeSpan.TotalMinutes.ToString());
                }
                else if (Set_Fill2(this.TXT01_COOVSTR31.GetValue().ToString()) == "00" && Set_Fill2(this.TXT01_COOVSTR32.GetValue().ToString()) == "00" &&
                        Set_Fill2(this.TXT01_COOVEND31.GetValue().ToString()) == "24" && Set_Fill2(this.TXT01_COOVEND32.GetValue().ToString()) == "00")
                {
                    dAppTime = dAppTime + 1440;
                }

                this.TXT01_COOWKTIME.SetValue(Convert.ToString(dAppTime));

                #endregion

                // 할증량 구하기
                // 할증량 = (MT * 총할증작업시간) * 총작업시간
                decimal dCOWKTIME = 0;
                decimal dCOOWKTIME = 0;
                decimal dCOOVQTY = 0;

                // 총작업시간
                dCOWKTIME = decimal.Parse(Get_Numeric(this.TXT01_COWKTIME.GetValue().ToString()));
                // 할증작업시간
                dCOOWKTIME = decimal.Parse(Get_Numeric(this.TXT01_COOWKTIME.GetValue().ToString()));



                dSVMTQTY = decimal.Parse(Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString()));

                // 총할증작업시간
                if (dCOOWKTIME != 0)
                {
                    // 20180314 박동근 차장님 요청(소숫점 4째 자리에서 반올림)
                    dCOOVQTY = (Math.Round(((dSVMTQTY * dCOOWKTIME) / dCOWKTIME), 3) * 1000);
                    dCOOVQTY = decimal.Parse(UP_DotDelete(Convert.ToString(dCOOVQTY)));

                    // 할증량 구하기
                    dCOOVQTY = dCOOVQTY / 1000;
                }

                // 할증량
                this.TXT01_COOVQTY.SetValue(Convert.ToString(dCOOVQTY));
            }
        }
        #endregion

        //#region Description : SURVEY REPORT 수정 ProcessCheck
        //private void BTN63_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    string sCHAI = string.Empty;

        //    // 화면 MT량
        //    fsSHOREQTY = Get_Numeric(this.TXT01_SVMTQTY.GetValue().ToString());

        //    DataTable dt = new DataTable();
        //    DataTable dt1 = new DataTable();

        //    string sCOPUSTR = string.Empty;
        //    string sCOPUEND = string.Empty;

        //    sCOPUSTR = Set_Fill2(this.TXT01_COPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUSTR2.GetValue().ToString());
        //    sCOPUEND = Set_Fill2(this.TXT01_COPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_COPUEND2.GetValue().ToString());

        //    if (double.Parse(Set_Numeric2(this.TXT01_SVMTQTY.GetValue().ToString(), 3)) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_676EQ588");
        //        this.TXT01_SVMTQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if ((double.Parse(sCOPUSTR.ToString()) > 2459) || (double.Parse(sCOPUSTR.ToString()) < 0))
        //    {
        //        this.ShowMessage("TY_M_UT_676ES590");
        //        this.TXT01_COPUSTR1.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if ((double.Parse(sCOPUEND.ToString()) > 2459) || (double.Parse(sCOPUEND.ToString()) < 0))
        //    {
        //        this.ShowMessage("TY_M_UT_676ES592");
        //        this.TXT01_COPUEND1.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 계약번호 존재유무 체크
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_676G5597",
        //        this.TXT01_COCONTNO.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count <= 0)
        //    {
        //        this.ShowMessage("TY_M_UT_676GA598");
        //        this.TXT01_COCONTNO.Focus();

        //        e.Successed = false;
        //        return;
        //    }
        //    else
        //    {
        //        if (this.CBH01_SVHWAMUL.GetValue().ToString() != dt.Rows[0]["CNHWAMUL"].ToString())
        //        {
        //            this.ShowMessage("TY_M_UT_676GD600");
        //            this.TXT01_COCONTNO.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (this.CBO01_SVTANKNO.GetValue().ToString() != dt.Rows[0]["CNTANKNO"].ToString())
        //        {
        //            this.ShowMessage("TY_M_UT_676GD601");
        //            this.CBO01_SVTANKNO.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (Int64.Parse(dt.Rows[0]["CNCONTEN"].ToString()) < Int64.Parse(Get_Date(this.DTP01_SVIPHANG.GetValue().ToString())))
        //        {
        //            this.ShowMessage("TY_M_UT_676GG602");
        //            this.CBO01_SVTANKNO.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }

        //    // 온도구분
        //    if (this.TXT01_SVTEMPGB.GetValue().ToString() != "")
        //    {
        //        if ((this.TXT01_SVTEMPGB.GetValue().ToString() != "C") && (this.TXT01_SVTEMPGB.GetValue().ToString() != "F"))
        //        {
        //            this.ShowMessage("TY_M_UT_676GH603");

        //            e.Successed = false;
        //            return;
        //        }
        //    }

        //    // 이고 구분
        //    if (this.TXT01_SVMOGB.GetValue().ToString() != "")
        //    {
        //        // 이고 탱크
        //        if (this.TXT01_SVMOTA.GetValue().ToString() != "")
        //        {
        //            // 탱크제원 체크
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_676GL604",
        //                this.CBO01_SVTANKNO.GetValue().ToString()
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count <= 0)
        //            {
        //                this.ShowMessage("TY_M_UT_676GM605");

        //                e.Successed = false;
        //                return;
        //            }
        //        }
        //    }


        //    string sCOOVSTR1 = string.Empty;
        //    string sCOOVEND1 = string.Empty;

        //    string sCOOVSTR2 = string.Empty;
        //    string sCOOVEND2 = string.Empty;

        //    sCOOVSTR1 = Set_Fill2(this.TXT01_COOVSTR11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR12.GetValue().ToString());
        //    sCOOVEND1 = Set_Fill2(this.TXT01_COOVEND11.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND12.GetValue().ToString());

        //    sCOOVSTR2 = Set_Fill2(this.TXT01_COOVSTR21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR22.GetValue().ToString());
        //    sCOOVEND2 = Set_Fill2(this.TXT01_COOVEND21.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND22.GetValue().ToString());

        //    // KL량
        //    if (Double.Parse(Set_Numeric2(this.TXT01_SVKLQTY.GetValue().ToString(), 3)) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_676GS610");

        //        e.Successed = false;
        //        return;
        //    }

        //    // 할증시작시간1
        //    if ((Int32.Parse(sCOOVSTR1.ToString()) > 2459) || Int32.Parse(sCOOVSTR1.ToString()) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_676GR608");
        //        this.TXT01_COOVSTR11.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 할증종료시간1
        //    if ((Int32.Parse(sCOOVEND1.ToString()) > 2459) || Int32.Parse(sCOOVEND1.ToString()) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_676GR609");
        //        this.TXT01_COOVSTR21.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 할증시작시간2
        //    if ((Int32.Parse(sCOOVSTR2.ToString()) > 2459) || Int32.Parse(sCOOVSTR2.ToString()) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_676GR608");
        //        this.TXT01_COOVSTR11.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 할증종료시간2
        //    if ((Int32.Parse(sCOOVEND2.ToString()) > 2459) || Int32.Parse(sCOOVEND2.ToString()) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_676GR609");
        //        this.TXT01_COOVSTR21.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 할증량
        //    if (double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3)) > double.Parse(Set_Numeric2(this.TXT01_SVMTQTY.GetValue().ToString(), 3)))
        //    {
        //        this.ShowMessage("TY_M_UT_676GQ607");
        //        this.TXT01_COOVQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 계산방법
        //    if (this.TXT01_SVKESANGB.GetValue().ToString() != "1" && this.TXT01_SVKESANGB.GetValue().ToString() != "2" && this.TXT01_SVKESANGB.GetValue().ToString() != "3" &&
        //        this.TXT01_SVKESANGB.GetValue().ToString() != "4" && this.TXT01_SVKESANGB.GetValue().ToString() != "5" && this.TXT01_SVKESANGB.GetValue().ToString() != "6")
        //    {
        //        this.ShowMessage("TY_M_UT_676GP606");

        //        e.Successed = false;
        //        return;
        //    }

        //    fsCOOVAM = this.TXT01_COOVAM.GetValue().ToString();

        //    // 수정하시겠습니까?
        //    if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }
        //}
        //#endregion

        #region Description : SURVEY REPORT 삭제 ProcessCheck
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // BL별 입고파일 자료 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_676H4612",
                this.DTP01_SVIPHANG.GetValue().ToString(),
                this.CBH01_SVBONSUN.GetValue().ToString(),
                this.CBH01_SVHWAJU.GetValue().ToString(),
                this.CBH01_SVHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_676H7613");
                this.DTP01_SVIPHANG.Focus();

                e.Successed = false;
                return;
            }

            fsCOOVAM = this.TXT01_COOVAM.GetValue().ToString();

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : SURVEY REPORT 필드 클리어
        private void UP_UTISURVF_FieldClear(string sGUBUN)
        {
            this.DTP01_SVIPHANG.SetReadOnly(false);
            this.CBH01_SVBONSUN.SetReadOnly(false);
            this.CBH01_SVHWAJU.SetReadOnly(false);
            this.CBH01_SVHWAMUL.SetReadOnly(false);

            if (sGUBUN.ToString() == "CLEAR")
            {
                this.TXT01_COCONTNO.SetValue("");
                this.TXT01_SVBIJUNG.SetValue("");
                this.TXT01_SVVCF.SetValue("");

                this.TXT01_COWKTIME.SetValue("");
                this.TXT01_COOWKTIME.SetValue("");

                // MT량
                this.TXT01_SVMTQTY.SetValue("");
                // KL량
                this.TXT01_SVKLQTY.SetValue("");
                
                // PUMP시작시간1
                this.TXT01_COPUSTR11.SetValue("");
                this.TXT01_COPUSTR12.SetValue("");
                // PUMP종료시간1
                this.TXT01_COPUEND11.SetValue("");
                this.TXT01_COPUEND12.SetValue("");


                // PUMP시작시간2
                this.TXT01_COPUSTR21.SetValue("");
                this.TXT01_COPUSTR22.SetValue("");
                // PUMP종료시간2
                this.TXT01_COPUEND21.SetValue("");
                this.TXT01_COPUEND22.SetValue("");


                // PUMP시작시간3
                this.TXT01_COPUSTR31.SetValue("");
                this.TXT01_COPUSTR32.SetValue("");
                // PUMP종료시간3
                this.TXT01_COPUEND31.SetValue("");
                this.TXT01_COPUEND32.SetValue("");



                // 할증시작시간1
                this.TXT01_COOVSTR11.SetValue("");
                this.TXT01_COOVSTR12.SetValue("");
                // 할증종료시간1
                this.TXT01_COOVEND11.SetValue("");
                this.TXT01_COOVEND12.SetValue("");

                // 할증시작시간2
                this.TXT01_COOVSTR21.SetValue("");
                this.TXT01_COOVSTR22.SetValue("");
                // 할증종료시간2
                this.TXT01_COOVEND21.SetValue("");
                this.TXT01_COOVEND22.SetValue("");


                // 할증시작시간3
                this.TXT01_COOVSTR31.SetValue("");
                this.TXT01_COOVSTR32.SetValue("");
                // 할증종료시간3
                this.TXT01_COOVEND31.SetValue("");
                this.TXT01_COOVEND32.SetValue("");

                // 할증량
                this.TXT01_COOVQTY.SetValue("");
                // 계약번호
                //this.TXT01_COCONTNO.SetValue("");
                // W.C.F
                //this.TXT01_SVBIJUNG.SetValue("");
                // V.C
                //this.TXT01_SVVCF.SetValue("");
                // 온도
                this.TXT01_SVTEMP.SetValue("");
                // 온도구분
                //this.TXT01_SVTEMPGB.SetValue("");
                // 계산방법
                //this.TXT01_SVKESANGB.SetValue("");
                // 이고구분
                this.TXT01_SVMOGB.SetValue("");
                // 이고탱크
                this.TXT01_SVMOTA.SetValue("");
                // 할증금액
                this.TXT01_COOVAM.SetValue("");
                // 검정사코드
                this.CBH01_SVGUMJUNG.SetValue("");
            }
            if (this.TXT01_SVKESANGB.GetValue().ToString() == "")
            {
                this.TXT01_SVKESANGB.SetValue("1");
            }
        }
        #endregion

        #region Description : SURVEY REPORT 디스플레이
        private void UP_UTISURVF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN63_SAV.Visible = true;
                //this.BTN63_EDIT.Visible = false;
                this.BTN63_REM.Visible = false;

                this.BTN63_OK.Visible = true;

                this.BTN63_UTTCODEHELP1.Visible = true;
                this.BTN63_UTTCODEHELP2.Visible = true;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN63_SAV.Visible = true;
                //this.BTN63_EDIT.Visible = true;
                this.BTN63_REM.Visible = true;

                this.BTN63_OK.Visible = false;

                this.BTN63_UTTCODEHELP1.Visible = false;
                this.BTN63_UTTCODEHELP2.Visible = false;
            }
            else
            {
                this.BTN63_SAV.Visible = false;
                //this.BTN63_EDIT.Visible = false;
                this.BTN63_REM.Visible = false;
                this.BTN63_OK.Visible = false;

                this.BTN63_UTTCODEHELP1.Visible = false;
                this.BTN63_UTTCODEHELP2.Visible = false;
            }
        }
        #endregion

        #endregion

        #region Description : B/L별 입고 관리

        #region Description : BL별 입고관리 클리어 버튼
        private void BTN64_UTTCLEAR_Click(object sender, EventArgs e)
        {
            // 조회
            string sSTDATE  = string.Empty;
            string sEDDATE  = string.Empty;
            string sSBONSUN = string.Empty;
            string sSHWAJU  = string.Empty;
            string sSHWAMUL = string.Empty;

            // 입고화물관리 내용
            string sIPHANG  = string.Empty;
            string sBONSUN  = string.Empty;
            string sHWAJU   = string.Empty;
            string sHWAMUL  = string.Empty;

            string sBLNO    = string.Empty;
            string sMSN     = string.Empty;
            string sHSN     = string.Empty;
            string sCMBOGODAT1 = string.Empty;


            sSTDATE  = this.DTP01_STIPHANG.GetValue().ToString();
            sEDDATE  = this.DTP01_EDIPHANG.GetValue().ToString();
            sSBONSUN = this.CBH01_SBONSUN.GetValue().ToString();
            sSHWAJU  = this.CBH01_SHWAJU.GetValue().ToString();
            sSHWAMUL = this.CBH01_SHWAMUL.GetValue().ToString();

            sIPHANG  = this.DTP01_IPIPHANG.GetValue().ToString();
            sBONSUN  = this.CBH01_IPBONSUN.GetValue().ToString();
            sHWAJU   = this.CBH01_IPHWAJU.GetValue().ToString();
            sHWAMUL  = this.CBH01_IPHWAMUL.GetValue().ToString();
            sBLNO    = this.TXT01_IPBLNO.GetValue().ToString();
            sMSN     = this.TXT01_IPMSNSEQ.GetValue().ToString();
            sHSN     = this.TXT01_IPHSNSEQ.GetValue().ToString();
            sCMBOGODAT1 = this.DTP01_CMBOGODAT1.GetValue().ToString();

            UP_UTIIPGOF_FieldClear("CLEAR");
            //this.Initialize_Controls("01");

            this.DTP01_STIPHANG.SetValue(sSTDATE.ToString());
            this.DTP01_EDIPHANG.SetValue(sEDDATE.ToString());
            this.CBH01_SBONSUN.SetValue(sSBONSUN.ToString());
            this.CBH01_SHWAJU.SetValue(sSHWAJU.ToString());
            this.CBH01_SHWAMUL.SetValue(sSHWAMUL.ToString());

            this.DTP01_IPIPHANG.SetValue(sIPHANG.ToString());
            this.CBH01_IPBONSUN.SetValue(sBONSUN.ToString());
            this.CBH01_IPHWAJU.SetValue(sHWAJU.ToString());
            this.CBH01_IPHWAMUL.SetValue(sHWAMUL.ToString());

            this.TXT01_IPBLNO.SetValue(sBLNO.ToString());
            this.TXT01_IPMSNSEQ.SetValue(sMSN.ToString());
            this.TXT01_IPHSNSEQ.SetValue(sHSN.ToString());
            this.DTP01_CMBOGODAT1.SetValue(sCMBOGODAT1.ToString());

            this.FPS91_TY_S_UT_67FGN788.Initialize();

            // B/L별입고관리 전체 조회
            UP_UTIIPGOF_SEARCH();

            UP_UTIIPGOF_BTN_DISPLAY("NEW");

            SetFocus(this.TXT01_IPBLNO);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN64_NEW_Click(object sender, EventArgs e)
        {
            fsWK_GUBUN4 = "NEW";

            UP_UTIIPGOF_FieldClear(fsWK_GUBUN4);

            UP_GET_UTIVESLF();

            UP_UTIIPGOF_BTN_DISPLAY(fsWK_GUBUN4);

            UP_UTIIPGOF_Compute();

            this.TXT01_IPBLNO.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN64_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN4.ToString() == "NEW")
            {
                UP_UTIIPGOF_SAV();
            }
            else if (fsWK_GUBUN4.ToString() == "UPT")
            {
                UP_UTIIPGOF_UPT();
            }

            // 항만시설 보안료 재계산
            UP_UTICMDTF_Compute();

            // B/L별 입고관리 조회
            UP_UTIIPGOF_TAB_SEARCH(this.DTP01_IPIPHANG.GetValue().ToString(),
                                   this.CBH01_IPBONSUN.GetValue().ToString(),
                                   this.CBH01_IPHWAJU.GetValue().ToString(),
                                   this.CBH01_IPHWAMUL.GetValue().ToString(),
                                   ""
                                   );

            // B/L별 입고관리 전체 조회
            UP_UTIIPGOF_SEARCH();

            UP_UTIIPGOF_BTN_DISPLAY("");
        }
        #endregion

        //#region Description : 수정 버튼
        //private void BTN64_EDIT_Click(object sender, EventArgs e)
        //{
        //    UP_UTIIPGOF_UPT();

        //    UP_UTIIPGOF_BTN_DISPLAY("");
        //}
        //#endregion

        #region Description : 삭제 버튼
        private void BTN64_REM_Click(object sender, EventArgs e)
        {
            UP_UTIIPGOF_DEL();

            // B/L별 입고관리 조회
            UP_UTIIPGOF_TAB_SEARCH(this.DTP01_IPIPHANG.GetValue().ToString(),
                                   this.CBH01_IPBONSUN.GetValue().ToString(),
                                   this.CBH01_IPHWAJU.GetValue().ToString(),
                                   this.CBH01_IPHWAMUL.GetValue().ToString(),
                                   ""
                                   );

            // B/L별 입고관리 전체 조회
            UP_UTIIPGOF_SEARCH();

            UP_UTIIPGOF_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 입항조회 버튼
        private void BTN64_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            TYUTGB003S popup = new TYUTGB003S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_IPIPHANG.SetValue(popup.fsIPHANG); // 입항일자
                this.CBH01_IPBONSUN.SetValue(popup.fsBONSUN); // 본선명

                // 값 저장
                UP_SET_Cookie4(this.DTP01_IPIPHANG.GetValue().ToString(), this.CBH01_IPBONSUN.GetValue().ToString(),
                               this.CBH01_IPHWAJU.GetValue().ToString(),  this.CBH01_IPHWAMUL.GetValue().ToString(),
                               this.TXT01_IPBLNO.GetValue().ToString(),   this.TXT01_IPMSNSEQ.GetValue().ToString(),
                               this.TXT01_IPHSNSEQ.GetValue().ToString());

                SetFocus(this.CBH01_IPHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 입고조회 버튼
        private void BTN64_UTTCODEHELP2_Click(object sender, EventArgs e)
        {
            TYUTGB004S popup = new TYUTGB004S(this.DTP01_IPIPHANG.GetValue().ToString(), this.CBH01_IPBONSUN.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_IPHWAJU.SetValue(popup.fsHWAJU);   // 화주
                this.CBH01_IPHWAMUL.SetValue(popup.fsHWAMUL); // 화물

                // 값 저장
                UP_SET_Cookie4(this.DTP01_IPIPHANG.GetValue().ToString(), this.CBH01_IPBONSUN.GetValue().ToString(),
                               this.CBH01_IPHWAJU.GetValue().ToString(), this.CBH01_IPHWAMUL.GetValue().ToString(),
                               this.TXT01_IPBLNO.GetValue().ToString(), this.TXT01_IPMSNSEQ.GetValue().ToString(),
                               this.TXT01_IPHSNSEQ.GetValue().ToString());

                SetFocus(this.TXT01_IPBLNO);
            }
        }
        #endregion

        #region Description : 보세운송반입 버튼
        private void BTN64_UTTCODEHELP3_Click(object sender, EventArgs e)
        {
            TYUTIN030I popup = new TYUTIN030I(this.DTP01_IPIPHANG.GetValue().ToString(), this.CBH01_IPBONSUN.GetValue().ToString(),
                                              this.CBH01_IPHWAJU.GetValue().ToString(),  this.CBH01_IPHWAMUL.GetValue().ToString(),
                                              this.TXT01_IPMSNSEQ.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 적하목록, 반입일자 가져오기
        private void UP_GET_UTIVESLF()
        {
            if (this.DTP01_IPIPHANG.GetValue().ToString() == "")
            {
                this.DTP01_IPIPHANG.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            
            this.TXT01_IPSINOYY.SetValue(Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()).Substring(0, 4).ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67FIC795",
                this.DTP01_IPIPHANG.GetValue().ToString(),
                this.CBH01_IPBONSUN.GetValue().ToString(),
                this.CBH01_IPHWAJU.GetValue().ToString(),
                this.CBH01_IPHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_VSJUKHA1.SetValue(dt.Rows[0]["VSJUKHA"].ToString());
                this.DTP01_CMBOGODAT1.SetValue(dt.Rows[0]["CMBOGODAT"].ToString());
            }
        }
        #endregion

        #region Description : B/L별 입고관리 조회
        private void UP_UTIIPGOF_TAB_SEARCH(string sIPIPHANG, string sIPBONSUN,
                                            string sIPHWAJU,  string sIPHWAMUL, string sIPBLNO)
        {
            this.FPS91_TY_S_UT_67FGN788.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67FGD787",
                sIPIPHANG.ToString(),
                sIPBONSUN.ToString(),
                sIPHWAJU.ToString(),
                sIPHWAMUL.ToString(),
                sIPBLNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_67FGN788.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_UT_67FGN788.SetValue(dt);
            }
        }
        #endregion

        #region Description : B/L별 입고관리 전체 조회
        private void UP_UTIIPGOF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67FGB786",
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString(),
                this.CBH01_SBONSUN.GetValue().ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_67FGX792.SetValue(dt);
        }
        #endregion

        #region Description : B/L별 입고관리 확인
        private void UP_UTIIPGOF_RUN()
        {
            fsJUNIPMTQTY = "0";
            fsJUNIPBSQTY = "0";
            fsPRESEQCH = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67FH0793",
                this.DTP01_IPIPHANG.GetValue().ToString(),
                this.CBH01_IPBONSUN.GetValue().ToString(),
                this.CBH01_IPHWAJU.GetValue().ToString(),
                this.CBH01_IPHWAMUL.GetValue().ToString(),
                this.TXT01_IPBLNO.GetValue().ToString(),
                this.TXT01_IPMSNSEQ.GetValue().ToString(),
                this.TXT01_IPHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsJUNIPMTQTY = dt.Rows[0]["IPMTQTY"].ToString();
                fsJUNIPBSQTY = dt.Rows[0]["IPBSQTY"].ToString();
                fsPRESEQCH = dt.Rows[0]["IPSINO"].ToString();

                this.CurrentDataTableRowMapping(dt, "01");

                fsWK_GUBUN4 = "UPT";

                UP_UTIIPGOF_BTN_DISPLAY(fsWK_GUBUN4);

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.TXT01_IPMTQTY);
                };

                tmr.Interval = 100;
                tmr.Start();
            }

            // 값 저장
            UP_SET_Cookie4(this.DTP01_IPIPHANG.GetValue().ToString(), this.CBH01_IPBONSUN.GetValue().ToString(),
                           this.CBH01_IPHWAJU.GetValue().ToString(),  this.CBH01_IPHWAMUL.GetValue().ToString(),
                           this.TXT01_IPBLNO.GetValue().ToString(),   this.TXT01_IPMSNSEQ.GetValue().ToString(),
                           this.TXT01_IPHSNSEQ.GetValue().ToString());
        }
        #endregion

        #region Description : B/L별 입고관리 저장
        private void UP_UTIIPGOF_SAV()
        {
            this.TXT01_IPSINO.SetValue(fsSEQCH.ToString());

            // 입고번호 체크
            if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() == "1")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_67JD3845",
                                        Get_Numeric(this.TXT01_IPSINOYY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_IPSINO.GetValue().ToString()));

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if(dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_67JD6846");
                    this.TXT01_IPSINOYY.Focus();

                    return;
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67JF6862",
                                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),                       // 입항일자
                                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(), 				  	   // 본선
                                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                        // 화주
                                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),                       // 화물
                                    this.TXT01_IPBLNO.GetValue().ToString().ToUpper(),                         // B/L번호
                                    Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString()),                    // MBL 일련번호
                                    Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString()),                    // HBL 일련번호
                                    this.TXT01_IPPOJANG.GetValue().ToString(),                                 // 포장단위
                                    Get_Numeric(this.TXT01_IPCOUNT.GetValue().ToString()),                     // 반입갯수
                                    SetDefaultValue(this.TXT01_IPHBLNO.GetValue().ToString()),                 // H-BLNO
                                    Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()),                     // M/T량
                                    Get_Numeric(this.TXT01_IPKLQTY.GetValue().ToString()),                     // K/L량
                                    Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString()),                     // B/S량
                                    "0",                                                                       // 출고량
                                    "0",                                                                       // 통관량
                                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                        // 통관화주
                                    Get_Numeric(this.TXT01_IPSINOYY.GetValue().ToString()),                    // 입고번호년도
                                    Get_Numeric(this.TXT01_IPSINO.GetValue().ToString()),                      // 입고번호
                                    this.TXT01_IPCHANGGO.GetValue().ToString(),                                // 창고구분

                                    this.CBH01_IPHWAMULGB.GetValue().ToString().ToUpper(),                     // 화물구분
                                    this.CBH01_IPBANGUBUN.GetValue().ToString(),                               // 반입구분
                                    this.CBO01_IPCHGUBN.GetValue().ToString(),                                 // 분할반출체크
                                    Get_Numeric(Get_Date(this.TXT01_IPCHDATE.GetValue().ToString())),          // 분할반출일자
                                    Get_Numeric(this.TXT01_IPCHCHQTY.GetValue().ToString()),                   // 분할반출수량
                                    Get_Numeric(this.TXT01_IPJNHSNSEQ.GetValue().ToString()),                  // 이전 HSN
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                       // 작성사번    
                                    );

            
            if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() == "1")
            {
                if (fsSEQCH.ToString() != "1") // 업데이트
                {
                    this.DbConnector.Attach("TY_P_UT_67JF7864",
                                            fsSEQCH.ToString(),
                                            "I",
                                            fsSEQGB.ToString(),
                                            this.TXT01_IPSINOYY.GetValue().ToString()
                                            );
                }
                else // 저장
                {
                    this.DbConnector.Attach("TY_P_UT_67JF6863",
                                            "I",
                                            fsSEQGB.ToString(),
                                            this.TXT01_IPSINOYY.GetValue().ToString(),
                                            fsSEQCH.ToString()
                                            );
                }
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : B/L별 입고관리 수정
        private void UP_UTIIPGOF_UPT()
        {
            // 입고번호 체크
            if (fsPRESEQCH != "" && fsPRESEQCH != Get_Numeric(this.TXT01_IPSINO.GetValue().ToString()))
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_67JD3845",
                                        Get_Numeric(this.TXT01_IPSINOYY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_IPSINO.GetValue().ToString()));

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_67JD6846");
                    this.TXT01_IPSINOYY.Focus();

                    return;
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67JF1865",
                                    this.TXT01_IPPOJANG.GetValue().ToString(),                                 // 포장단위
                                    Get_Numeric(this.TXT01_IPCOUNT.GetValue().ToString()),                     // 반입갯수
                                    SetDefaultValue(this.TXT01_IPHBLNO.GetValue().ToString()),                 // H-BLNO
                                    Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()),                     // M/T량
                                    Get_Numeric(this.TXT01_IPKLQTY.GetValue().ToString()),                     // K/L량
                                    Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString()),                     // B/S량
                                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                        // 통관화주
                                    Get_Numeric(this.TXT01_IPSINOYY.GetValue().ToString()),                    // 입고번호년도
                                    Get_Numeric(this.TXT01_IPSINO.GetValue().ToString()),                      // 입고번호
                                    this.TXT01_IPCHANGGO.GetValue().ToString(),                                // 창고구분
                                    this.CBH01_IPHWAMULGB.GetValue().ToString().ToUpper(),                     // 화물구분
                                    this.CBH01_IPBANGUBUN.GetValue().ToString(),                               // 반입구분
                                    this.CBO01_IPCHGUBN.GetValue().ToString(),                                 // 분할반출체크
                                    Get_Numeric(Get_Date(this.TXT01_IPCHDATE.GetValue().ToString())),          // 분할반출일자
                                    Get_Numeric(this.TXT01_IPCHCHQTY.GetValue().ToString()),                   // 분할반출수량
                                    Get_Numeric(this.TXT01_IPJNHSNSEQ.GetValue().ToString()),                  // 이전 HSN
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                              // 작성사번
                                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),                       // 입항일자
                                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(), 				  	   // 본선
                                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                        // 화주
                                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),                       // 화물
                                    this.TXT01_IPBLNO.GetValue().ToString().ToUpper(),                         // B/L번호
                                    Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString()),                    // MBL 일련번호
                                    Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString())                     // HBL 일련번호
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : B/L별 입고관리 삭제
        private void UP_UTIIPGOF_DEL()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67JFN867",
                                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),                       // 입항일자
                                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(), 				  	   // 본선
                                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                        // 화주
                                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),                       // 화물
                                    this.TXT01_IPBLNO.GetValue().ToString().ToUpper(),                         // B/L번호
                                    Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString()),                    // MBL 일련번호
                                    Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString())                     // HBL 일련번호
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : B/L별 입고관리 SHORE 및 BL량 자동 계산
        private void UP_UTIIPGOF_Compute()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            // BL별 입고관리 존재체크(입항,본선,화주,화물)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_7C6GR198",
                this.DTP01_IPIPHANG.GetValue().ToString(),
                this.CBH01_IPBONSUN.GetValue().ToString(),
                this.CBH01_IPHWAJU.GetValue().ToString(),
                this.CBH01_IPHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                double dIPMTQTY = 0;
                double dIPBSQTY = 0;

                double dCMSHQTY = 0;
                double dCMBLQTY = 0;

                // 처음 등록이 아니면 입고관리의 SHORE량 - BL별입고관리에 등록된 SHORE량 합계
                //                               BL량    - BL별입고관리에 등록된 BL량    합계

                // BL별 입고관리 SHORE, BL량 가져오기(입항,본선,화주,화물)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_7C6GZ199",
                    this.DTP01_IPIPHANG.GetValue().ToString(),
                    this.CBH01_IPBONSUN.GetValue().ToString(),
                    this.CBH01_IPHWAJU.GetValue().ToString(),
                    this.CBH01_IPHWAMUL.GetValue().ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    dIPMTQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt1.Rows[0]["IPMTQTY"].ToString())));
                    dIPBSQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt1.Rows[0]["IPBSQTY"].ToString())));
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_675B3566",
                    this.DTP01_IPIPHANG.GetValue().ToString(),
                    this.CBH01_IPBONSUN.GetValue().ToString(),
                    this.CBH01_IPHWAJU.GetValue().ToString(),
                    this.CBH01_IPHWAMUL.GetValue().ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    dCMSHQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt1.Rows[0]["CMSHQTY"].ToString())));
                    dCMBLQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt1.Rows[0]["CMBLQTY"].ToString())));

                    dIPMTQTY = double.Parse(String.Format("{0,9:N3}", dCMSHQTY - dIPMTQTY));
                    dIPBSQTY = double.Parse(String.Format("{0,9:N3}", dCMBLQTY - dIPBSQTY));

                    this.TXT01_IPMTQTY.SetValue(dIPMTQTY.ToString());
                    this.TXT01_IPBSQTY.SetValue(dIPBSQTY.ToString());
                }
            }
            else
            {
                // 처음 등록이면 입고관리의 SHORE량과 BL량을 가져옴
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_675B3566",
                    this.DTP01_IPIPHANG.GetValue().ToString(),
                    this.CBH01_IPBONSUN.GetValue().ToString(),
                    this.CBH01_IPHWAJU.GetValue().ToString(),
                    this.CBH01_IPHWAMUL.GetValue().ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.TXT01_IPMTQTY.SetValue(dt1.Rows[0]["CMSHQTY"].ToString());
                    this.TXT01_IPBSQTY.SetValue(dt1.Rows[0]["CMBLQTY"].ToString());
                }
            }
        }
        #endregion

        #region Description : B/L별 입고관리 스프레드 이벤트
        private void FPS91_TY_S_UT_67FGN788_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_MESSAGE4.SetValue("");

            this.DTP01_IPIPHANG.SetReadOnly(true);
            this.CBH01_IPBONSUN.SetReadOnly(true);
            this.CBH01_IPHWAJU.SetReadOnly(true);
            this.CBH01_IPHWAMUL.SetReadOnly(true);
            this.TXT01_IPBLNO.SetReadOnly(true);
            this.TXT01_IPMSNSEQ.SetReadOnly(true);
            this.TXT01_IPHSNSEQ.SetReadOnly(true);

            this.DTP01_IPIPHANG.SetValue(this.FPS91_TY_S_UT_67FGN788.GetValue("IPIPHANG").ToString());
            this.CBH01_IPBONSUN.SetValue(this.FPS91_TY_S_UT_67FGN788.GetValue("IPBONSUN").ToString());
            this.CBH01_IPHWAJU.SetValue(this.FPS91_TY_S_UT_67FGN788.GetValue("IPHWAJU").ToString());
            this.CBH01_IPHWAMUL.SetValue(this.FPS91_TY_S_UT_67FGN788.GetValue("IPHWAMUL").ToString());
            this.TXT01_IPBLNO.SetValue(this.FPS91_TY_S_UT_67FGN788.GetValue("IPBLNO").ToString());
            this.TXT01_IPMSNSEQ.SetValue(this.FPS91_TY_S_UT_67FGN788.GetValue("IPMSNSEQ").ToString());
            this.TXT01_IPHSNSEQ.SetValue(this.FPS91_TY_S_UT_67FGN788.GetValue("IPHSNSEQ").ToString());

            // B/L별 입고관리 확인
            UP_UTIIPGOF_RUN();
        }
        #endregion

        #region Description : B/L별 입고관리 전체 스프레드 이벤트
        private void FPS91_TY_S_UT_67FGX792_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_IPIPHANG.SetReadOnly(true);
            this.CBH01_IPBONSUN.SetReadOnly(true);
            this.CBH01_IPHWAJU.SetReadOnly(true);
            this.CBH01_IPHWAMUL.SetReadOnly(true);
            this.TXT01_IPBLNO.SetReadOnly(true);
            this.TXT01_IPMSNSEQ.SetReadOnly(true);
            this.TXT01_IPHSNSEQ.SetReadOnly(true);

            this.DTP01_IPIPHANG.SetValue(this.FPS91_TY_S_UT_67FGX792.GetValue("IPIPHANG").ToString());
            this.CBH01_IPBONSUN.SetValue(this.FPS91_TY_S_UT_67FGX792.GetValue("IPBONSUN").ToString());
            this.CBH01_IPHWAJU.SetValue(this.FPS91_TY_S_UT_67FGX792.GetValue("IPHWAJU").ToString());
            this.CBH01_IPHWAMUL.SetValue(this.FPS91_TY_S_UT_67FGX792.GetValue("IPHWAMUL").ToString());
            this.TXT01_IPBLNO.SetValue(this.FPS91_TY_S_UT_67FGX792.GetValue("IPBLNO").ToString());
            this.TXT01_IPMSNSEQ.SetValue(this.FPS91_TY_S_UT_67FGX792.GetValue("IPMSNSEQ").ToString());
            this.TXT01_IPHSNSEQ.SetValue(this.FPS91_TY_S_UT_67FGX792.GetValue("IPHSNSEQ").ToString());

            // B/L별 입고관리 조회
            UP_UTIIPGOF_TAB_SEARCH(this.DTP01_IPIPHANG.GetValue().ToString(),
                                   this.CBH01_IPBONSUN.GetValue().ToString(),
                                   this.CBH01_IPHWAJU.GetValue().ToString(),
                                   this.CBH01_IPHWAMUL.GetValue().ToString(),
                                   ""
                                   );

            // B/L별 입고관리 확인
            UP_UTIIPGOF_RUN();
        }
        #endregion

        #region Description : B/L별 입고관리 저장 ProcessCheck
        private void BTN64_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sCHAI       = string.Empty;
            string sIPBLNOSEQ  = string.Empty;
            string sIPHBLNOSEQ = string.Empty;

            this.TXT01_MESSAGE4.SetValue("");

            int i = 0;

            DataTable dt  = new DataTable();
            DataTable dt1 = new DataTable();

            if (fsWK_GUBUN4.ToString() == "NEW")
            {
                UP_GET_UTIVESLF();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67FH0793",
                    this.DTP01_IPIPHANG.GetValue().ToString(),
                    this.CBH01_IPBONSUN.GetValue().ToString(),
                    this.CBH01_IPHWAJU.GetValue().ToString(),
                    this.CBH01_IPHWAMUL.GetValue().ToString(),
                    this.TXT01_IPBLNO.GetValue().ToString(),
                    this.TXT01_IPMSNSEQ.GetValue().ToString(),
                    this.TXT01_IPHSNSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.TXT01_IPBLNO.Focus();

                    e.Successed = false;
                    return;
                }
            }

            fsSVMTQTY = "0";
            fsSVKLQTY = "0";

            if (double.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_UT_67JBP827");
                this.TXT01_IPBSQTY.Focus();

                e.Successed = false;
                return;
            }

            // SURVEY FILE의 M/T, K/L량 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67JDH850",
                Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(), // 본선
                this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),  // 화주
                this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper()  // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (SetDefaultValue(dt.Rows[0][2].ToString()).Trim() == "0")
                {
                    this.ShowMessage("TY_M_UT_67JDJ851");
                    this.DTP01_IPIPHANG.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    fsSVMTQTY = SetDefaultValue(dt.Rows[0][0].ToString()).Trim();
                    fsSVKLQTY = SetDefaultValue(dt.Rows[0][1].ToString()).Trim();

                    if (double.Parse(fsSVMTQTY.ToString()) == 0)
                    {
                        this.ShowMessage("TY_M_UT_67JDJ852");
                        this.DTP01_IPIPHANG.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                /* 입항,본선,화주,화물로 등록시
                 * UTICMDTF의 SHORE량과 UTISURVF의 MT량 합이 일치하지 않으면
                 * 등록이 안됨
                 */

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67JDM854",
                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(), // 본선
                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),  // 화주
                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper()  // 화물
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    fsCMSHQTY = SetDefaultValue(dt.Rows[0][0].ToString()).Trim();
                }

                if (fsSVMTQTY.ToString() != fsCMSHQTY.ToString())
                {
                    this.ShowMessage("TY_M_UT_67JDN855");
                    this.DTP01_IPIPHANG.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.ShowMessage("TY_M_UT_67JDK853");
                this.DTP01_IPIPHANG.Focus();

                e.Successed = false;
                return;
            }




            if (fsWK_GUBUN4.ToString() == "NEW")
            {
                //if (this.TXT01_IPSINOYY.GetValue().ToString() != DateTime.Now.ToString("yyyyMMdd").Substring(0, 4).ToString())
                //{
                //    this.ShowMessage("TY_M_UT_67I9F806");
                //    this.TXT01_IPSINOYY.Focus();

                //    e.Successed = false;
                //    return;
                //}

                // 등록 수정시 입항+입고화일에 자료가 존재하면서 B/L번호와 HB SEQ가 화면상의 내용이 같으면 에러 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67I9K809",
                    this.DTP01_IPIPHANG.GetValue().ToString().Trim(),
                    this.CBH01_IPBONSUN.GetValue().ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        sIPBLNOSEQ = dt.Rows[i]["IPBLNOSEQ"].ToString();
                        sIPHBLNOSEQ = dt.Rows[i]["IPHBLNOSEQ"].ToString();

                        if ((this.TXT01_IPMSNSEQ.GetValue().ToString().Trim() == sIPBLNOSEQ.ToString().Trim()) && (this.TXT01_IPHSNSEQ.GetValue().ToString().Trim() == sIPHBLNOSEQ.ToString().Trim()))
                        {
                            this.ShowMessage("TY_M_UT_67I9P810");
                            this.TXT01_IPMSNSEQ.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }
            }


            // MT량
            if (double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_UT_67IAT812");
                this.TXT01_IPMTQTY.Focus();

                e.Successed = false;
                return;
            }

            // 보세운송반입일때 반입포장형태및 갯수입력 받음
            if (this.CBH01_IPBANGUBUN.GetValue().ToString() == "20")
            {
                if (this.TXT01_IPPOJANG.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_67IAV813");
                    this.TXT01_IPPOJANG.Focus();

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_IPPOJANG.GetValue().ToString().ToUpper() != "VL")
                {
                    if (this.TXT01_IPCOUNT.GetValue().ToString().ToUpper() == "")
                    {
                        this.ShowMessage("TY_M_UT_67IAW814");
                        this.TXT01_IPCOUNT.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 창고구분
            if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() != "" && this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() != "1")
            {
                this.ShowMessage("TY_M_UT_67JAX824");
                this.TXT01_IPCHANGGO.Focus();

                e.Successed = false;
                return;
            }

            if (Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString().ToUpper()) == "0")
            {
                this.ShowMessage("TY_M_UT_67JBP827");
                this.TXT01_IPBSQTY.Focus();

                e.Successed = false;
                return;
            }

            if (this.CBO01_IPCHGUBN.GetValue().ToString().ToUpper() != "Y" && this.CBO01_IPCHGUBN.GetValue().ToString().ToUpper() != "")
            {
                this.ShowMessage("TY_M_UT_67JBT829");
                this.CBO01_IPCHGUBN.Focus();

                e.Successed = false;
                return;
            }

            /*****************************************************
             ' IPHSNSEQ 값이 1이상일 경우는 B/L분할일 경우다.    *
             ' B/L분할 반출일 경우                               *
             ' 이전HSN의 MT량보다 분할된 MT량의 합이 클 수 없다. *
             '****************************************************/
            if (this.CBH01_IPBANGUBUN.GetValue().ToString().ToUpper() == "23")
            {
                if (Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString().ToUpper()) == "0")
                {
                    this.ShowMessage("TY_M_UT_67JBV830");
                    this.TXT01_IPHSNSEQ.Focus();

                    e.Successed = false;
                    return;
                }

                if (this.CBO01_IPCHGUBN.GetValue().ToString().ToUpper() == "")
                {
                    this.ShowMessage("TY_M_UT_67JBV831");
                    this.CBO01_IPCHGUBN.Focus();

                    e.Successed = false;
                    return;
                }

                if (Get_Date(this.TXT01_IPCHDATE.GetValue().ToString().ToUpper()) == "")
                {
                    this.ShowMessage("TY_M_UT_67JBW832");
                    this.TXT01_IPCHDATE.Focus();

                    e.Successed = false;
                    return;
                }

                if (Get_Numeric(this.TXT01_IPCHCHQTY.GetValue().ToString().ToUpper()) == "0")
                {
                    this.ShowMessage("TY_M_UT_67JBW833");
                    this.TXT01_IPCHDATE.Focus();

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_IPJNHSNSEQ.GetValue().ToString().ToUpper() == "")
                {
                    this.ShowMessage("TY_M_UT_67JBW834");
                    this.TXT01_IPCHDATE.Focus();

                    e.Successed = false;
                    return;
                }

                if (double.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString())) != double.Parse(Get_Numeric(this.TXT01_IPCHCHQTY.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_UT_67JBY836");
                    this.TXT01_IPCHDATE.Focus();

                    e.Successed = false;
                    return;
                }

                double dPre_IPBSQTY = 0;
                double dPre_IPMTQTY = 0;

                //이전 HSN에 입력된 량의 합계구하기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67JBZ838",
                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),      // 입항일자
                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),      // 본선
                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),       // 화주
                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),      // 화물
                    this.TXT01_IPBLNO.GetValue().ToString(),                  // B/L번호
                    Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString()),   // MBL 일련번호
                    Get_Numeric(this.TXT01_IPJNHSNSEQ.GetValue().ToString())  // 이전 HSN번호
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {                
                    dPre_IPBSQTY = double.Parse(String.Format("{0,9:N3}", dt.Rows[0]["IPBSQTY"].ToString()));
                    dPre_IPMTQTY = double.Parse(String.Format("{0,9:N3}", dt.Rows[0]["IPMTQTY"].ToString()));
                }
                else
                {
                    this.ShowMessage("TY_M_UT_67JC2839");
                    this.TXT01_IPJNHSNSEQ.Focus();

                    e.Successed = false;
                    return;
                }



                double dNext_IPBSQTY = 0;
                double dNext_IPMTQTY = 0;

                // B/L분할 된 데이터
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67JCR840",
                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),               // 입항일자
                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),               // 본선
                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                // 화주
                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),               // 화물
                    this.TXT01_IPBLNO.GetValue().ToString().ToUpper(),                 // B/L번호
                    Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString().ToUpper()),  // MBL 일련번호
                    Get_Numeric(this.TXT01_IPJNHSNSEQ.GetValue().ToString().ToUpper()) // 이전 HSN번호
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (fsWK_GUBUN4.ToString() == "NEW")
                    {
                        dNext_IPBSQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPBSQTY"].ToString()) + double.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString()))));
                        dNext_IPMTQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPMTQTY"].ToString()) + double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()))));
                    }
                    else
                    {
                        dNext_IPBSQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPBSQTY"].ToString()) - double.Parse(Get_Numeric(fsJUNIPBSQTY.ToString())) + double.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString()))));
                        dNext_IPMTQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPMTQTY"].ToString()) - double.Parse(Get_Numeric(fsJUNIPMTQTY.ToString())) + double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()))));
                    }
                }
                else
                {
                    dNext_IPBSQTY = double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString().ToUpper())));
                    dNext_IPMTQTY = double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString().ToUpper())));
                }

                if (dPre_IPMTQTY < dNext_IPMTQTY)
                {
                    this.ShowMessage("TY_M_UT_67JCT841");
                    this.TXT01_IPMTQTY.Focus();

                    e.Successed = false;
                    return;
                }

                if (dPre_IPBSQTY < dNext_IPBSQTY)
                {
                    this.ShowMessage("TY_M_UT_67JCU842");
                    this.TXT01_IPBSQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }




            fsSEQCH = "";
            fsSEQGB = "";

            if (fsWK_GUBUN4.ToString() == "NEW")
            {
                this.CBH01_IPACTHJ.SetValue(this.CBH01_IPHWAJU.GetValue().ToString());

                string sSEQGB = string.Empty;

                // 반출입순차번호 부여
                if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() == "1")
                {
                    if (this.TXT01_IPSINOYY.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_67JCX843");
                        this.TXT01_IPBSQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() == "1")
                        {
                            fsSEQGB = "11011055";
                        }
                        else
                        {
                            fsSEQGB = "11006057";
                        }

                        // 반출입순차번호 부여
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_67JD1844",
                            fsSEQGB.ToString(),
                            Get_Numeric(this.TXT01_IPSINOYY.GetValue().ToString())
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            fsSEQCH = dt.Rows[0]["SEQCH"].ToString();
                            this.TXT01_IPSINO.SetValue(fsSEQCH.ToString());
                        }
                        else
                        {
                            fsSEQCH = "1";
                            this.TXT01_IPSINO.SetValue(fsSEQCH.ToString());
                        }
                    }
                }
                else
                {
                    fsSEQCH = "0";
                }

                // 반출입순차번호 부여
                if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() == "1")
                {
                    string sIPSINO = string.Empty;

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_67JD3845",
                        Get_Numeric(this.TXT01_IPSINOYY.GetValue().ToString()),
                        Get_Numeric(this.TXT01_IPSINO.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        sIPSINO = "EXITS";
                    }
                    else
                    {
                        this.ShowMessage("TY_M_UT_67JD6846");
                        this.TXT01_IPSINOYY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                // 수정 및 삭제
                if (double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_IPCHQTY.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_UT_67JD7847");
                    this.TXT01_IPSINOYY.Focus();

                    e.Successed = false;
                    return;
                }
            }

            fsCMSHQTY = "0";
            fsCMBLQTY = "0";
            fsIPMTQTY = "0";
            fsIPBSQTY = "0";

            this.TXT01_MESSAGE4.SetValue("");

            if (Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString()) == "0")
            {
                // 입고 화물 파일의 SHORE량,B/L량과 입고 파일의 M/T량,K/L량을 비교함
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67JD1848",
                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),               // 입항일자
                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),               // 본선
                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                // 화주
                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper()                // 화물
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsCMSHQTY = SetDefaultValue(dt.Rows[0]["CMSHQTY"].ToString()).Trim();
                    fsCMBLQTY = SetDefaultValue(dt.Rows[0]["CMBLQTY"].ToString()).Trim();
                    fsIPMTQTY = SetDefaultValue(dt.Rows[0]["IPMTQTY"].ToString()).Trim();
                    fsIPBSQTY = SetDefaultValue(dt.Rows[0]["IPBSQTY"].ToString()).Trim();

                    
                    decimal dCMSHQTY    = 0;
                    decimal dIPMTQTY    = 0;
                    decimal dJUNIPMTQTY = 0;

                    decimal dCMBLQTY    = 0;
                    decimal dIPBSQTY    = 0;
                    decimal dJUNIPBSQTY = 0;

                    if (fsWK_GUBUN4.ToString() == "NEW")
                    {
                        if (Decimal.Parse(Get_Numeric(fsCMSHQTY.ToString())) != Decimal.Parse(Get_Numeric(fsIPMTQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())))
                        {
                            dCMSHQTY = decimal.Parse(fsCMSHQTY.ToString());
                            dIPMTQTY = decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()));
                            dJUNIPMTQTY = decimal.Parse(Get_Numeric(fsIPMTQTY.ToString()));

                            if (dCMSHQTY > dIPMTQTY + dJUNIPMTQTY)
                            {
                                sCHAI = Convert.ToString(dCMSHQTY - (dIPMTQTY + dJUNIPMTQTY)).ToString();
                                this.TXT01_MESSAGE4.SetValue("[안내] - 입고 화물의 SHORE량이 B/L별 입고화물의 M/T량보다   " + sCHAI + "만큼 많습니다." + sCHAI + "만큼 더 등록하십시요.");
                            }
                            else if (dCMSHQTY < dIPMTQTY + dJUNIPMTQTY)
                            {
                                sCHAI = Convert.ToString((dIPMTQTY + dJUNIPMTQTY) - dCMSHQTY).ToString();
                                this.TXT01_MESSAGE4.SetValue("[안내] - B/L별 입고화물의 M/T량이 입고 화물의 SHORE량보다   " + sCHAI + "만큼 많습니다." + sCHAI + "만큼 빼십시요.");
                            }
                        }

                        if (Decimal.Parse(Get_Numeric(fsCMBLQTY.ToString())) < Decimal.Parse(Get_Numeric(fsIPBSQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString())))
                        {
                            this.ShowMessage("TY_M_UT_67JDD849");
                            this.TXT01_IPKLQTY.Focus();

                            e.Successed = false;
                            return;
                        }

                        if (Decimal.Parse(fsCMBLQTY.ToString()) != Decimal.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString())) + Decimal.Parse(Get_Numeric(fsIPBSQTY.ToString())))
                        {
                            dCMBLQTY = decimal.Parse(fsCMBLQTY.ToString());
                            dIPBSQTY = decimal.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString()));
                            dJUNIPBSQTY = decimal.Parse(Get_Numeric(fsIPBSQTY.ToString()));

                            if (dCMBLQTY > dIPBSQTY + dJUNIPBSQTY)
                            {
                                sCHAI = Convert.ToString(dCMBLQTY - (dIPBSQTY + dJUNIPBSQTY)).ToString();
                                this.TXT01_MESSAGE4.SetValue("입고 화물의 B/L량이 B/L별 입고화물의 적하중량보다   " + sCHAI + "만큼 많습니다.");
                            }
                            else if (dCMBLQTY < dIPBSQTY + dJUNIPBSQTY)
                            {
                                sCHAI = Convert.ToString((dIPBSQTY + dJUNIPBSQTY) - dCMBLQTY).ToString();
                                this.TXT01_MESSAGE4.SetValue("B/L별 입고화물의 적하중량이 입고 화물의 B/L량보다   " + sCHAI + "만큼 많습니다.");
                            }
                        }
                    }
                    else
                    {
                        if (Decimal.Parse(Get_Numeric(fsCMSHQTY.ToString())) < Decimal.Parse(Get_Numeric(fsIPMTQTY.ToString())) - Decimal.Parse(Get_Numeric(fsJUNIPMTQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())))
                        {
                            this.TXT01_MESSAGE4.SetValue("입고 화물파일의 SHORE량과 입고 파일의 MT량이 일치 하지 않습니다.");
                        }

                        if (Decimal.Parse(Get_Numeric(fsCMBLQTY.ToString())) < Decimal.Parse(Get_Numeric(fsIPBSQTY.ToString())) - Decimal.Parse(Get_Numeric(fsJUNIPBSQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString())))
                        {
                            this.TXT01_MESSAGE4.SetValue("입고 화물파일의 B/L량과 입고 파일의 K/L량이 일치 하지 않습니다.");
                        }
                    }
                }
            }

            this.TXT01_IPKLQTY.SetValue(String.Format("{0,9:N3}", (
                double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()))
                / double.Parse(fsSVMTQTY.ToString())
                * double.Parse(fsSVKLQTY.ToString()))));


            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        //#region Description : B/L별 입고관리 수정 ProcessCheck
        //private void BTN64_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    string sCHAI = string.Empty;
        //    string sIPBLNOSEQ = string.Empty;
        //    string sIPHBLNOSEQ = string.Empty;

        //    int i = 0;

        //    DataTable dt = new DataTable();
        //    DataTable dt1 = new DataTable();

        //    fsSVMTQTY = "0";
        //    fsSVKLQTY = "0";

        //    // SURVEY FILE의 M/T, K/L량 확인
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_67JDH850",
        //        Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()), // 입항일자
        //        this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(), // 본선
        //        this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),  // 화주
        //        this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper()  // 화물
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        if (SetDefaultValue(dt.Rows[0][2].ToString()).Trim() == "0")
        //        {
        //            this.ShowMessage("TY_M_UT_67JDJ851");
        //            this.DTP01_IPIPHANG.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //        else
        //        {
        //            fsSVMTQTY = SetDefaultValue(dt.Rows[0][0].ToString()).Trim();
        //            fsSVKLQTY = SetDefaultValue(dt.Rows[0][1].ToString()).Trim();

        //            if (double.Parse(fsSVMTQTY.ToString()) == 0)
        //            {
        //                this.ShowMessage("TY_M_UT_67JDJ852");
        //                this.DTP01_IPIPHANG.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }

        //        /* 입항,본선,화주,화물로 등록시
        //         * UTICMDTF의 SHORE량과 UTISURVF의 MT량 합이 일치하지 않으면
        //         * 등록이 안됨
        //         */

        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_UT_67JDM854",
        //            Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()), // 입항일자
        //            this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(), // 본선
        //            this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),  // 화주
        //            this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper()  // 화물
        //            );

        //        dt1 = this.DbConnector.ExecuteDataTable();

        //        if (dt1.Rows.Count > 0)
        //        {
        //            fsCMSHQTY = SetDefaultValue(dt.Rows[0][0].ToString()).Trim();
        //        }

        //        if (fsSVMTQTY.ToString() != fsCMSHQTY.ToString())
        //        {
        //            this.ShowMessage("TY_M_UT_67JDN855");
        //            this.DTP01_IPIPHANG.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        this.ShowMessage("TY_M_UT_67JDK853");
        //        this.DTP01_IPIPHANG.Focus();

        //        e.Successed = false;
        //        return;
        //    }



        //    // MT량
        //    if (double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())) == 0)
        //    {
        //        this.ShowMessage("TY_M_UT_67IAT812");
        //        this.TXT01_IPMTQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 보세운송반입일때 반입포장형태및 갯수입력 받음
        //    if (this.CBH01_IPBANGUBUN.GetValue().ToString() == "20")
        //    {
        //        if (this.TXT01_IPPOJANG.GetValue().ToString() == "")
        //        {
        //            this.ShowMessage("TY_M_UT_67IAV813");
        //            this.TXT01_IPPOJANG.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (this.TXT01_IPPOJANG.GetValue().ToString().ToUpper() != "VL")
        //        {
        //            if (this.TXT01_IPCOUNT.GetValue().ToString().ToUpper() == "")
        //            {
        //                this.ShowMessage("TY_M_UT_67IAW814");
        //                this.TXT01_IPCOUNT.Focus();

        //                e.Successed = false;
        //                return;
        //            }
        //        }
        //    }

        //    // 창고구분
        //    if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() != "" && this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() != "1")
        //    {
        //        this.ShowMessage("TY_M_UT_67JAX824");
        //        this.TXT01_IPCHANGGO.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if (Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString().ToUpper()) == "0")
        //    {
        //        this.ShowMessage("TY_M_UT_67JBP827");
        //        this.TXT01_IPBSQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if (this.CBO01_IPCHGUBN.GetValue().ToString().ToUpper() != "Y" && this.CBO01_IPCHGUBN.GetValue().ToString().ToUpper() != "")
        //    {
        //        this.ShowMessage("TY_M_UT_67JBT829");
        //        this.CBO01_IPCHGUBN.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    /*****************************************************
        //     ' IPHSNSEQ 값이 1이상일 경우는 B/L분할일 경우다.    *
        //     ' B/L분할 반출일 경우                               *
        //     ' 이전HSN의 MT량보다 분할된 MT량의 합이 클 수 없다. *
        //     '****************************************************/
        //    if (this.CBH01_IPBANGUBUN.GetValue().ToString().ToUpper() == "23")
        //    {
        //        if (Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString().ToUpper()) == "0")
        //        {
        //            this.ShowMessage("TY_M_UT_67JBV830");
        //            this.TXT01_IPHSNSEQ.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (this.CBO01_IPCHGUBN.GetValue().ToString().ToUpper() == "")
        //        {
        //            this.ShowMessage("TY_M_UT_67JBV831");
        //            this.CBO01_IPCHGUBN.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (Get_Date(this.TXT01_IPCHDATE.GetValue().ToString().ToUpper()) == "")
        //        {
        //            this.ShowMessage("TY_M_UT_67JBW832");
        //            this.TXT01_IPCHDATE.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (Get_Numeric(this.TXT01_IPCHCHQTY.GetValue().ToString().ToUpper()) == "0")
        //        {
        //            this.ShowMessage("TY_M_UT_67JBW833");
        //            this.TXT01_IPCHDATE.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (this.TXT01_IPJNHSNSEQ.GetValue().ToString().ToUpper() == "")
        //        {
        //            this.ShowMessage("TY_M_UT_67JBW834");
        //            this.TXT01_IPCHDATE.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (double.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString())) != double.Parse(Get_Numeric(this.TXT01_IPCHCHQTY.GetValue().ToString())))
        //        {
        //            this.ShowMessage("TY_M_UT_67JBY836");
        //            this.TXT01_IPCHDATE.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        double dPre_IPBSQTY = 0;
        //        double dPre_IPMTQTY = 0;

        //        //이전 HSN에 입력된 량의 합계구하기
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_UT_67JBZ838",
        //            Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),      // 입항일자
        //            this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),      // 본선
        //            this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),       // 화주
        //            this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),      // 화물
        //            this.TXT01_IPBLNO.GetValue().ToString(),                  // B/L번호
        //            Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString()),   // MBL 일련번호
        //            Get_Numeric(this.TXT01_IPJNHSNSEQ.GetValue().ToString())  // 이전 HSN번호
        //            );

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            dPre_IPBSQTY = double.Parse(String.Format("{0,9:N3}", dt.Rows[0]["IPBSQTY"].ToString()));
        //            dPre_IPMTQTY = double.Parse(String.Format("{0,9:N3}", dt.Rows[0]["IPMTQTY"].ToString()));
        //        }
        //        else
        //        {
        //            this.ShowMessage("TY_M_UT_67JC2839");
        //            this.TXT01_IPJNHSNSEQ.Focus();

        //            e.Successed = false;
        //            return;
        //        }



        //        double dNext_IPBSQTY = 0;
        //        double dNext_IPMTQTY = 0;

        //        // B/L분할 된 데이터
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_UT_67JCR840",
        //            Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),               // 입항일자
        //            this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),               // 본선
        //            this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                // 화주
        //            this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),               // 화물
        //            this.TXT01_IPBLNO.GetValue().ToString().ToUpper(),                 // B/L번호
        //            Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString().ToUpper()),  // MBL 일련번호
        //            Get_Numeric(this.TXT01_IPJNHSNSEQ.GetValue().ToString().ToUpper()) // 이전 HSN번호
        //            );

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            dNext_IPBSQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPBSQTY"].ToString()) - double.Parse(Get_Numeric(fsJUNIPBSQTY.ToString())) + double.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString()))));
        //            dNext_IPMTQTY = double.Parse(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPMTQTY"].ToString()) - double.Parse(Get_Numeric(fsJUNIPMTQTY.ToString())) + double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()))));
        //        }
        //        else
        //        {
        //            dNext_IPBSQTY = double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString().ToUpper())));
        //            dNext_IPMTQTY = double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString().ToUpper())));
        //        }

        //        if (dPre_IPMTQTY < dNext_IPMTQTY)
        //        {
        //            this.ShowMessage("TY_M_UT_67JCT841");
        //            this.TXT01_IPMTQTY.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (dPre_IPBSQTY < dNext_IPBSQTY)
        //        {
        //            this.ShowMessage("TY_M_UT_67JCU842");
        //            this.TXT01_IPBSQTY.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }




        //    // 수정 및 삭제
        //    if (double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_IPCHQTY.GetValue().ToString())))
        //    {
        //        this.ShowMessage("TY_M_UT_67JD7847");
        //        this.TXT01_IPSINOYY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    fsCMSHQTY = "0";
        //    fsCMBLQTY = "0";
        //    fsIPMTQTY = "0";
        //    fsIPBSQTY = "0";

        //    this.TXT01_MESSAGE4.SetValue("");

        //    if (Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString()) == "0")
        //    {
        //        // 입고 화물 파일의 SHORE량,B/L량과 입고 파일의 M/T량,K/L량을 비교함
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_UT_67JD1848",
        //            Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),               // 입항일자
        //            this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),               // 본선
        //            this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),                // 화주
        //            this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper()                // 화물
        //            );

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            fsCMSHQTY = SetDefaultValue(dt.Rows[0]["CMSHQTY"].ToString()).Trim();
        //            fsCMBLQTY = SetDefaultValue(dt.Rows[0]["CMBLQTY"].ToString()).Trim();
        //            fsIPMTQTY = SetDefaultValue(dt.Rows[0]["IPMTQTY"].ToString()).Trim();
        //            fsIPBSQTY = SetDefaultValue(dt.Rows[0]["IPBSQTY"].ToString()).Trim();


        //            decimal dCMSHQTY = 0;
        //            decimal dIPMTQTY = 0;
        //            decimal dJUNIPMTQTY = 0;

        //            if (Decimal.Parse(Get_Numeric(fsCMSHQTY.ToString())) < Decimal.Parse(Get_Numeric(fsIPMTQTY.ToString())) - Decimal.Parse(Get_Numeric(fsJUNIPMTQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())))
        //            {
        //                this.TXT01_MESSAGE4.SetValue("입고 화물파일의 SHORE량과 입고 파일의 MT량이 일치 하지 않습니다.");

        //                e.Successed = false;
        //                return;
        //            }

        //            if (Decimal.Parse(Get_Numeric(fsCMBLQTY.ToString())) < Decimal.Parse(Get_Numeric(fsIPBSQTY.ToString())) - Decimal.Parse(Get_Numeric(fsJUNIPBSQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_IPBSQTY.GetValue().ToString())))
        //            {
        //                this.TXT01_MESSAGE4.SetValue("입고 화물파일의 B/L량과 입고 파일의 K/L량이 일치 하지 않습니다.");

        //                e.Successed = false;
        //                return;
        //            }
        //        }
        //    }

        //    this.TXT01_IPKLQTY.Text = String.Format("{0,9:N3}", (
        //        double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()))
        //        / double.Parse(fsSVMTQTY.ToString())
        //        * double.Parse(fsSVKLQTY.ToString())));

        //    // 수정하시겠습니까?
        //    if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }
        //}
        //#endregion

        #region Description : B/L별 입고관리 삭제 ProcessCheck
        private void BTN64_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt  = new DataTable();
            DataTable dt1 = new DataTable();

            if (this.CBH01_IPBANGUBUN.GetValue().ToString().ToUpper() == "23")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67JDT857",
                    Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),      // 입항일자
                    this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),      // 본선
                    this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),       // 화주
                    this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),      // 화물
                    this.TXT01_IPBLNO.GetValue().ToString(),                  // B/L번호
                    Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString()),   // MBL 일련번호
                    Get_Numeric(this.TXT01_IPJNHSNSEQ.GetValue().ToString())  // 이전 HSN번호
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_67JDV858");
                    this.TXT01_IPHSNSEQ.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 통관데이터 자료 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67JDW859",
                Get_Date(this.DTP01_IPIPHANG.GetValue().ToString()),    // 입항일자
                this.CBH01_IPBONSUN.GetValue().ToString().ToUpper(),    // 본선
                this.CBH01_IPHWAJU.GetValue().ToString().ToUpper(),     // 화주
                this.CBH01_IPHWAMUL.GetValue().ToString().ToUpper(),    // 화물
                this.TXT01_IPBLNO.GetValue().ToString(),                // B/L번호
                Get_Numeric(this.TXT01_IPMSNSEQ.GetValue().ToString()), // MBL 일련번호
                Get_Numeric(this.TXT01_IPHSNSEQ.GetValue().ToString())  // HSN번호
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_67JDY861");
                this.TXT01_IPHSNSEQ.Focus();

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

        #region Description : B/L별 입고관리 필드 클리어
        private void UP_UTIIPGOF_FieldClear(string sGUBUN)
        {
            this.DTP01_IPIPHANG.SetReadOnly(false);
            this.CBH01_IPBONSUN.SetReadOnly(false);
            this.CBH01_IPHWAJU.SetReadOnly(false);
            this.CBH01_IPHWAMUL.SetReadOnly(false);
            this.TXT01_IPBLNO.SetReadOnly(false);
            this.TXT01_IPMSNSEQ.SetReadOnly(false);
            this.TXT01_IPHSNSEQ.SetReadOnly(false);

            this.TXT01_IPCHQTY.SetValue("");
            this.TXT01_IPPAQTY.SetValue("");

            this.TXT01_IPSINO.SetValue("");

            if (sGUBUN == "CLEAR")
            {
                this.TXT01_VSJUKHA1.SetValue("");
                this.TXT01_IPMTQTY.SetValue("");
                this.TXT01_IPKLQTY.SetValue("");
                this.TXT01_IPBSQTY.SetValue("");
                this.TXT01_IPCHQTY.SetValue("");
                this.TXT01_IPPAQTY.SetValue("");
                this.TXT01_IPPOJANG.SetValue("");
                this.TXT01_IPCOUNT.SetValue("");
                this.TXT01_IPSINOYY.SetValue("");
                this.TXT01_IPHBLNO.SetValue("");
                this.CBH01_IPHWAMULGB.SetValue("");
                this.CBH01_IPBANGUBUN.SetValue("");
                this.TXT01_IPCHANGGO.SetValue("");
                this.CBH01_IPACTHJ.SetValue("");
                this.CBO01_IPCHGUBN.SetValue("");
                this.TXT01_IPCHDATE.SetValue("");
                this.TXT01_IPCHCHQTY.SetValue("");
                this.TXT01_IPJNHSNSEQ.SetValue("");
            }
        }
        #endregion

        #region Description : B/L별 입고관리 디스플레이
        private void UP_UTIIPGOF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN64_SAV.Visible  = true;
                //this.BTN64_EDIT.Visible = false;
                this.BTN64_REM.Visible  = false;

                this.BTN64_UTTCODEHELP1.Visible = true;
                this.BTN64_UTTCODEHELP2.Visible = true;
                this.BTN64_UTTCODEHELP3.Visible = true;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN64_SAV.Visible = true;
                //this.BTN64_EDIT.Visible = true;
                this.BTN64_REM.Visible  = true;

                this.BTN64_UTTCODEHELP1.Visible = false;
                this.BTN64_UTTCODEHELP2.Visible = false;

                if (this.CBH01_IPBANGUBUN.GetValue().ToString() == "20")
                {
                    this.BTN64_UTTCODEHELP3.Visible = true;
                }
                else
                {
                    this.BTN64_UTTCODEHELP3.Visible = false;
                }
            }
            else
            {
                this.BTN64_SAV.Visible  = false;
                //this.BTN64_EDIT.Visible = false;
                this.BTN64_REM.Visible  = false;

                this.BTN64_UTTCODEHELP1.Visible = false;
                this.BTN64_UTTCODEHELP2.Visible = false;
                this.BTN64_UTTCODEHELP3.Visible = false;
            }
        }
        #endregion

        #region Description : 입고화물관리 항만시설 보안료 계산
        private void UP_UTICMDTF_Compute()
        {
            string sCMBBQTY = "0";          // BBLS
            string sCMSEBBLS = "0";         // 보안료 BBLS
            string sCMSETRANSBBLS = "0";    // 환적화물 BBLS
            string sCMSEAMT = "0";          // 보안료
            decimal dCHYMSEAMT = 0;         // 보안료 단가
            string sTEMP;

            DataTable dt = new DataTable();

            if (Convert.ToInt32(DTP01_IPIPHANG.GetString()) >= 20190101)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_91EDM492", this.DTP01_IPIPHANG.GetString(),
                                                            this.CBH01_IPBONSUN.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    // 입항관리 선박구분:2(내항선), 접안장소:2(2부두)가 아닌경우 항만시설 보안료 계산
                    if (dt.Rows[0]["VSVSGB"].ToString() != "2" && dt.Rows[0]["VSJUBAN"].ToString() != "2")
                    {

                        // 입고화물관리 조회
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_66SAJ413", this.DTP01_IPIPHANG.GetValue().ToString(),
                                                                    this.CBH01_IPBONSUN.GetValue().ToString(),
                                                                    this.CBH01_IPHWAJU.GetValue().ToString(),
                                                                    this.CBH01_IPHWAMUL.GetValue().ToString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // Bbls 량 확인
                            sCMBBQTY = dt.Rows[0]["CMBBQTY"].ToString();
                        }
                        // b/l별 입고관리 환적화물 K/L량 조회

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_91EEJ495", this.DTP01_IPIPHANG.GetString(),
                                                                    this.CBH01_IPBONSUN.GetValue().ToString(),
                                                                    this.CBH01_IPHWAJU.GetValue().ToString(),
                                                                    this.CBH01_IPHWAMUL.GetValue().ToString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 환적화물 Bbls 량 계산
                            sCMSETRANSBBLS = Convert.ToString((((double.Parse(dt.Rows[0]["IPKLQTY"].ToString()) / 0.158984) * 1000)));
                            sCMSETRANSBBLS = UP_DotDelete(sCMSETRANSBBLS);
                            sCMSETRANSBBLS = Convert.ToString(double.Parse(sCMSETRANSBBLS) / 1000);

                            // 보안료; Bbls 량 계산
                            sCMSEBBLS = Convert.ToString(double.Parse(sCMBBQTY) - double.Parse(sCMSETRANSBBLS));
                        }

                        // 보안료 단가 조회
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_66U8W471", this.DTP01_IPIPHANG.GetValue().ToString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            dCHYMSEAMT = decimal.Parse(dt.Rows[0]["CHYMSEAMT"].ToString());
                        }
                        // 보안료 계산
                        string sCMSEBBLSTMEP = Convert.ToString(UP_DotDelete(Convert.ToString(Decimal.Parse(Get_Numeric(sCMSEBBLS)) / 10))).ToString();

                        // 수정 필요
                        sTEMP = Convert.ToString(Decimal.Parse(Get_Numeric(sCMSEBBLS)) - (Decimal.Parse(sCMSEBBLSTMEP.ToString()) * 10));

                        if (Decimal.Parse(sTEMP.ToString()) != 0)
                        {
                            sCMSEBBLSTMEP = Convert.ToString(Int64.Parse(sCMSEBBLSTMEP.ToString()) + 1).ToString();
                        }
                        else
                        {
                            sCMSEBBLSTMEP = Convert.ToString(Int64.Parse(sCMSEBBLSTMEP.ToString())).ToString();
                        }

                        sCMSEAMT = Convert.ToString((Decimal.Parse(sCMSEBBLSTMEP.ToString()) * dCHYMSEAMT));
                        sCMSEAMT = UP_DotDelete(Convert.ToString(Decimal.Parse(sCMSEAMT.ToString()) / 10));
                        sCMSEAMT = Convert.ToString(Decimal.Parse(sCMSEAMT.ToString()) * 10);

                        if (this.CBH01_IPBONSUN.GetValue().ToString() == "PP1" || this.CBH01_IPBONSUN.GetValue().ToString() == "PP2" ||
                            this.CBH01_IPBONSUN.GetValue().ToString() == "PP3" || this.CBH01_IPBONSUN.GetValue().ToString() == "PP4" ||
                            this.CBH01_IPBONSUN.GetValue().ToString() == "PP5" || this.CBH01_IPBONSUN.GetValue().ToString() == "TK1" ||
                            this.CBH01_IPBONSUN.GetValue().ToString() == "TK2" || this.CBH01_IPBONSUN.GetValue().ToString() == "TK3" ||
                            this.CBH01_IPBONSUN.GetValue().ToString() == "TK7" || this.CBH01_IPBONSUN.GetValue().ToString() == "PIP" || 
                            this.CBH01_IPBONSUN.GetValue().ToString() == "CON" || this.CBH01_IPBONSUN.GetValue().ToString() == "SA6")
                        {
                            sCMSEBBLS = "0";
                            sCMSETRANSBBLS = "0";
                            sCMSEAMT = "0";
                        }

                        // 입고화물관리 업데이트  
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_91GF1511",
                                                sCMSEBBLS,
                                                sCMSETRANSBBLS,
                                                sCMSEAMT,
                                                TYUserInfo.EmpNo.ToString().Trim().ToUpper(),             
                                                this.DTP01_IPIPHANG.GetString(),
                                                this.CBH01_IPBONSUN.GetValue().ToString(),
                                                this.CBH01_IPHWAJU.GetValue().ToString(),
                                                this.CBH01_IPHWAMUL.GetValue().ToString()
                                                );

                        this.DbConnector.ExecuteNonQuery();

                    }
                }
            }
        }
        #endregion

        #endregion

        #region Description : 통관 관리

        #region Description : 통관관리 클리어 버튼
        private void BTN65_UTTCLEAR_Click(object sender, EventArgs e)
        {
            // 조회
            string sSTDATE      = string.Empty;
            string sEDDATE      = string.Empty;
            string sSBONSUN     = string.Empty;
            string sSHWAJU      = string.Empty;
            string sSHWAMUL     = string.Empty;

            // 입고화물관리 내용
            string sIPHANG      = string.Empty;
            string sBONSUN      = string.Empty;
            string sHWAJU       = string.Empty;
            string sHWAMUL      = string.Empty;

            string sBLNO        = string.Empty;
            string sMSN         = string.Empty;
            string sHSN         = string.Empty;
            string sCSCUSTIL    = string.Empty;
            string sCSCHASU     = string.Empty;
            string sCSCHENDIL   = string.Empty;
            string sEDAEXTSDATE = string.Empty;
            string sEDAEXTEDATE = string.Empty;

            sSTDATE      = this.DTP01_STIPHANG.GetValue().ToString();
            sEDDATE      = this.DTP01_EDIPHANG.GetValue().ToString();
            sSBONSUN     = this.CBH01_SBONSUN.GetValue().ToString();
            sSHWAJU      = this.CBH01_SHWAJU.GetValue().ToString();
            sSHWAMUL     = this.CBH01_SHWAMUL.GetValue().ToString();

            sIPHANG      = this.DTP01_CSIPHANG.GetValue().ToString();
            sBONSUN      = this.CBH01_CSBONSUN.GetValue().ToString();
            sHWAJU       = this.CBH01_CSHWAJU.GetValue().ToString();
            sHWAMUL      = this.CBH01_CSHWAMUL.GetValue().ToString();
            sBLNO        = this.TXT01_CSBLNO.GetValue().ToString();
            sMSN         = this.TXT01_CSMSNSEQ.GetValue().ToString();
            sHSN         = this.TXT01_CSHSNSEQ.GetValue().ToString();

            sCSCUSTIL    = this.DTP01_CSCUSTIL.GetValue().ToString();
            sCSCHASU     = this.TXT01_CSCHASU.GetValue().ToString();
            sCSCHENDIL   = this.DTP01_CSCHENDIL.GetValue().ToString();
            sEDAEXTSDATE = this.DTP01_EDAEXTSDATE.GetValue().ToString();
            sEDAEXTEDATE = this.DTP01_EDAEXTEDATE.GetValue().ToString();

            UP_UTICUSTF_FieldClear("CLEAR");

            this.DTP01_STIPHANG.SetValue(sSTDATE.ToString());
            this.DTP01_EDIPHANG.SetValue(sEDDATE.ToString());
            this.CBH01_SBONSUN.SetValue(sSBONSUN.ToString());
            this.CBH01_SHWAJU.SetValue(sSHWAJU.ToString());
            this.CBH01_SHWAMUL.SetValue(sSHWAMUL.ToString());

            this.DTP01_CSIPHANG.SetValue(sIPHANG.ToString());
            this.CBH01_CSBONSUN.SetValue(sBONSUN.ToString());
            this.CBH01_CSHWAJU.SetValue(sHWAJU.ToString());
            this.CBH01_CSHWAMUL.SetValue(sHWAMUL.ToString());
            this.TXT01_CSBLNO.SetValue(sBLNO.ToString());
            this.TXT01_CSMSNSEQ.SetValue(sMSN.ToString());
            this.TXT01_CSHSNSEQ.SetValue(sHSN.ToString());
            this.DTP01_CSCUSTIL.SetValue(sCSCUSTIL.ToString());
            this.TXT01_CSCHASU.SetValue(sCSCHASU.ToString());
            this.DTP01_CSCHENDIL.SetValue(sCSCHENDIL.ToString());
            this.DTP01_EDAEXTSDATE.SetValue(sEDAEXTSDATE.ToString());
            this.DTP01_EDAEXTEDATE.SetValue(sEDAEXTEDATE.ToString());

            this.FPS91_TY_S_UT_684FW965.Initialize();

            // 통관관리 전체 조회
            UP_UTICUSTF_SEARCH();

            UP_UTICUSTF_BTN_DISPLAY("NEW");

            SetFocus(this.TXT01_CSBLNO);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN65_NEW_Click(object sender, EventArgs e)
        {
            fsCSCUQTY = "0";
            fsCSACTHJ = "";

            fsWK_GUBUN5 = "NEW";
            UP_UTICUSTF_FieldClear(fsWK_GUBUN5);

            // 입고 파일에서 입고사항을 가져옴
            UP_GET_UTIIPGOF();

            UP_UTICUSTF_BTN_DISPLAY(fsWK_GUBUN5);

            this.TXT01_CSBLNO.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN65_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN5.ToString() == "NEW")
            {
                UP_UTICUSTF_SAV();
            }
            else
            {
                UP_UTICUSTF_UPT();
            }

            // 통관관리 조회
            UP_UTICUSTF_TAB_SEARCH(this.DTP01_CSIPHANG.GetValue().ToString(),
                                   this.CBH01_CSBONSUN.GetValue().ToString(),
                                   this.CBH01_CSHWAJU.GetValue().ToString(),
                                   this.CBH01_CSHWAMUL.GetValue().ToString(),
                                   this.TXT01_CSBLNO.GetValue().ToString(),
                                   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                                   this.TXT01_CSHSNSEQ.GetValue().ToString()
                                   );

            // 통관관리 전체 조회
            UP_UTICUSTF_SEARCH();

            UP_UTICUSTF_BTN_DISPLAY("");
        }
        #endregion

        //#region Description : 수정 버튼
        //private void BTN65_EDIT_Click(object sender, EventArgs e)
        //{
        //    UP_UTICUSTF_UPT();

        //    // 통관관리 조회
        //    UP_UTICUSTF_TAB_SEARCH(this.DTP01_CSIPHANG.GetValue().ToString(),
        //                           this.CBH01_CSBONSUN.GetValue().ToString(),
        //                           this.CBH01_CSHWAJU.GetValue().ToString(),
        //                           this.CBH01_CSHWAMUL.GetValue().ToString(),
        //                           this.TXT01_CSBLNO.GetValue().ToString(),
        //                           this.TXT01_CSMSNSEQ.GetValue().ToString(),
        //                           this.TXT01_CSHSNSEQ.GetValue().ToString()
        //                           );

        //    UP_UTICUSTF_BTN_DISPLAY("");
        //}
        //#endregion

        #region Description : 삭제 버튼
        private void BTN65_REM_Click(object sender, EventArgs e)
        {
            UP_UTICUSTF_DEL();

            // 통관관리 조회
            UP_UTICUSTF_TAB_SEARCH(this.DTP01_CSIPHANG.GetValue().ToString(),
                                   this.CBH01_CSBONSUN.GetValue().ToString(),
                                   this.CBH01_CSHWAJU.GetValue().ToString(),
                                   this.CBH01_CSHWAMUL.GetValue().ToString(),
                                   this.TXT01_CSBLNO.GetValue().ToString(),
                                   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                                   this.TXT01_CSHSNSEQ.GetValue().ToString()
                                   );

            // 통관관리 전체 조회
            UP_UTICUSTF_SEARCH();

            UP_UTICUSTF_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 통관관리 조회
        private void UP_UTICUSTF_TAB_SEARCH(string sCSIPHANG, string sCSBONSUN, string sCSHWAJU,
                                            string sCSHWAMUL, string sCSBLNO,   string sCSMSNSEQ,
                                            string sCSHSNSEQ)
        {
            this.FPS91_TY_S_UT_684FW965.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_684FR964",
                sCSIPHANG.ToString(),
                sCSBONSUN.ToString(),
                sCSHWAJU.ToString(),
                sCSHWAMUL.ToString(),
                sCSBLNO.ToString(),
                sCSMSNSEQ.ToString(),
                sCSHSNSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_684FW965.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_UT_684FW965.SetValue(dt);
            }
        }
        #endregion

        #region Description : 통관관리 전체 조회
        private void UP_UTICUSTF_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_684FJ962",
                this.DTP01_STIPHANG.GetValue().ToString(),
                this.DTP01_EDIPHANG.GetValue().ToString(),
                this.CBH01_SBONSUN.GetValue().ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_684FK963.SetValue(dt);
        }
        #endregion

        #region Description : 통관관리 확인
        private void UP_UTICUSTF_RUN()
        {
            fsCSCUQTY = "0";
            fsCSACTHJ = "";

            DataTable dt  = new DataTable();

            // 입고 파일에서 입고사항을 가져옴
            UP_GET_UTIIPGOF();

            // 입고 파일에서 입고사항을 가져옴
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_684GG967",
                this.DTP01_CSIPHANG.GetValue().ToString(),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_684H2969");
                this.DTP01_CSIPHANG.Focus();

                return;
            }


            // 통관관리 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_684GA966",
                this.DTP01_CSIPHANG.GetValue().ToString(),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString(),
                this.DTP01_CSCUSTIL.GetValue().ToString(),
                this.TXT01_CSCHASU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsCSCUQTY = String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["CSCUQTY"].ToString()));                
                fsCSACTHJ = dt.Rows[0]["CSACTHJ"].ToString();

                this.CurrentDataTableRowMapping(dt, "01");

                fsWK_GUBUN5 = "UPT";

                UP_UTICUSTF_BTN_DISPLAY(fsWK_GUBUN5);

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.TXT01_CSCUQTY);
                };

                tmr.Interval = 100;
                tmr.Start();
            }

            // 값 저장
            UP_SET_Cookie5(this.DTP01_CSIPHANG.GetValue().ToString(),     this.CBH01_CSBONSUN.GetValue().ToString(),
                               this.CBH01_CSHWAJU.GetValue().ToString(),  this.CBH01_CSHWAMUL.GetValue().ToString(),
                               this.TXT01_CSBLNO.GetValue().ToString(),   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                               this.TXT01_CSHSNSEQ.GetValue().ToString(), this.DTP01_CSCUSTIL.GetValue().ToString(),
                               this.TXT01_CSCHASU.GetValue().ToString());

            
        }
        #endregion

        #region Description : 통관파일 저장
        private void UP_UTICUSTF_SAV()
        {
            // 통관파일 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_867DW184",
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString()),          // HSN
                                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),             // 통관일자
                                    this.TXT01_CSCHASU.GetValue().ToString(),                        // 통관차수
                                    Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()),           // 통관량
                                    Get_Numeric(this.TXT01_CSCHQTY.GetValue().ToString()),           // 출고량
                                    Get_Numeric(this.TXT01_CSCOSTUS.GetValue().ToString()),          // 감정가($)
                                    Get_Numeric(this.TXT01_CSCOSTWO.GetValue().ToString()),          // 감정가(\)
                                    this.TXT01_CSSINNO.GetValue().ToString(),                        // 신고사항(신고번호)
                                    Get_Numeric(this.TXT01_CSSINQTY.GetValue().ToString()),          // 신고수량
                                    this.CBH01_CSSINNM.GetValue().ToString(),                        // 관세사
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 통관화주
                                    this.CBH01_CSBANGB.GetValue().ToString().ToUpper(),              // 반출구분
                                    Get_Date(this.DTP01_CSCHENDIL.GetValue().ToString()),            // 출고종료일자
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                     // 작성사번
                                    );

            //this.DbConnector.ExecuteNonQuery();

            // B/L별 입고파일 통관량 및 통관화주 업데이트
            //this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_689C0999",
                                    this.TXT01_IPPAQTY5.GetValue().ToString(),                       // 통관량
                                    this.CBH01_CSACTHJ.GetValue().ToString(),                        // 통관화주
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString())           // HSN
                                    );

            //this.DbConnector.ExecuteNonQuery();

            // 통관화주파일 등록
            //this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_689CM001",
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 통관화주
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 재고화주
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString()),          // HSN
                                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),             // 통관일자
                                    this.TXT01_CSCHASU.GetValue().ToString(),                        // 통관차수
                                    "",                                                              // 양수화주
                                    "",                                                              // 양도화주
                                    "0",                                                             // 양수일자
                                    "0",                                                             // 양도차수
                                    "0",                                                             // 양수순번
                                    "0",                                                             // 양도량
                                    "0",                                                             // 양수량
                                    "0",                                                             // 양수분양도량
                                    "0",                                                             // 양수출고량
                                    Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()),           // 통관량
                                    "0",                                                             // 출고량
                                    Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()),           // 재고량
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()                     // 작성사번
                                    );

            //this.DbConnector.ExecuteNonQuery();

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 통관파일 수정
        private void UP_UTICUSTF_UPT()
        {
            // 통관파일 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_867DX185",
                                    Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()),           // 통관량
                                    Get_Numeric(this.TXT01_CSCOSTUS.GetValue().ToString()),          // 감정가($)
                                    Get_Numeric(this.TXT01_CSCOSTWO.GetValue().ToString()),          // 감정가(\)
                                    this.TXT01_CSSINNO.GetValue().ToString(),                        // 신고사항(신고번호)
                                    Get_Numeric(this.TXT01_CSSINQTY.GetValue().ToString()),          // 신고수량
                                    this.CBH01_CSSINNM.GetValue().ToString(),                        // 관세사
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 통관화주
                                    this.CBH01_CSBANGB.GetValue().ToString().ToUpper(),              // 반출구분
                                    Get_Date(this.DTP01_CSCHENDIL.GetValue().ToString()),            // 출고종료일자
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                    // 작성사번
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString()),          // HSN
                                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),             // 통관일자
                                    this.TXT01_CSCHASU.GetValue().ToString()                         // 통관차수
                                    );

            //this.DbConnector.ExecuteNonQuery();

            // B/L별 입고파일 통관량 및 통관화주 업데이트
            //this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_689C0999",
                                    this.TXT01_IPPAQTY5.GetValue().ToString(),                       // 통관량
                                    this.CBH01_CSACTHJ.GetValue().ToString(),                        // 통관화주        
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString())           // HSN
                                    );

            //this.DbConnector.ExecuteNonQuery();

            // 통관화주파일 수정
            //this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_689CU002",
                                    Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()),           // 통관량
                                    Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()),           // 재고량
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                    // 작성사번
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 통관화주
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 재고화주
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString()),          // HSN
                                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),             // 통관일자
                                    this.TXT01_CSCHASU.GetValue().ToString(),                        // 통관차수
                                    "",                                                              // 양수화주
                                    "",                                                              // 양도화주
                                    "0",                                                             // 양수일자
                                    "0",                                                             // 양도차수
                                    "0"                                                              // 양수순번
                                    );

            //this.DbConnector.ExecuteNonQuery();

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 통관파일 삭제
        private void UP_UTICUSTF_DEL()
        {
            // 통관파일 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_689DH005",
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString()),          // HSN
                                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),             // 통관일자
                                    this.TXT01_CSCHASU.GetValue().ToString()                         // 통관차수
                                    );

            //this.DbConnector.ExecuteNonQuery();

            // B/L별 입고파일 통관량 및 통관화주 업데이트
            //this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_689C0999",
                                    this.TXT01_IPPAQTY5.GetValue().ToString(),                       // 통관량
                                    this.CBH01_CSACTHJ.GetValue().ToString(),                        // 통관화주
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString())           // HSN
                                    );

            //this.DbConnector.ExecuteNonQuery();

            // 통관화주파일 삭제
            //this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_689DH004",
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 통관화주
                                    this.CBH01_CSACTHJ.GetValue().ToString().ToUpper(),              // 재고화주
                                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),             // 입항일자
                                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(),             // 본선
                                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),              // 화주
                                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(),             // 화물
                                    this.TXT01_CSBLNO.GetValue().ToString(),                         // B/L번호
                                    Get_Numeric(this.TXT01_CSMSNSEQ.GetValue().ToString()),          // MSN
                                    Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString()),          // HSN
                                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),             // 통관일자
                                    this.TXT01_CSCHASU.GetValue().ToString(),                        // 통관차수
                                    "",                                                              // 양수화주
                                    "",                                                              // 양도화주
                                    "0",                                                             // 양수일자
                                    "0",                                                             // 양도차수
                                    "0"                                                              // 양수순번
                                    );

            //this.DbConnector.ExecuteNonQuery();

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : B/L별 입고파일 내용 가져오기
        private void UP_GET_UTIIPGOF()
        {
            DataTable dt = new DataTable();

            // 입고 파일에서 입고사항을 가져옴
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_684GG967",
                this.DTP01_CSIPHANG.GetValue().ToString(),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // M/T량
                this.TXT01_IPMTQTY5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPMTQTY5"].ToString())));
                // K/L량
                this.TXT01_IPKLQTY5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPKLQTY5"].ToString())));
                // 통관량
                this.TXT01_IPPAQTY5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPPAQTY5"].ToString())));
                // 미통관잔량
                this.TXT01_IPPAJAN5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPPAJAN5"].ToString())));
                // 출고량
                this.TXT01_IPCHQTY5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPCHQTY5"].ToString())));
                // 통관잔량       
                this.TXT01_IPJAQTY5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPJAQTY5"].ToString())));
                // 적하중량
                this.TXT01_IPBSQTY5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPBSQTY5"].ToString())));
                // 반입번호
                this.TXT01_IPSINO5.SetValue(dt.Rows[0]["IPSINO5"].ToString());


                // 이전 HSN에 대한 데이터 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_684GQ968",
                    this.DTP01_CSIPHANG.GetValue().ToString(),
                    this.CBH01_CSBONSUN.GetValue().ToString(),
                    this.CBH01_CSHWAJU.GetValue().ToString(),
                    this.CBH01_CSHWAMUL.GetValue().ToString(),
                    this.TXT01_CSBLNO.GetValue().ToString(),
                    this.TXT01_CSMSNSEQ.GetValue().ToString(),
                    this.TXT01_CSHSNSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // B/L분할수량
                    this.TXT01_JUNIPMTQTY5.SetValue(String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPMTQTY"].ToString())));
                }

                double dIPMTQTY = 0;
                double dIPPAQTY = 0;
                double dJUNIPMTQTY = 0;

                dIPMTQTY = double.Parse(Get_Numeric(this.TXT01_IPMTQTY5.GetValue().ToString()));
                dIPPAQTY = double.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString()));
                dJUNIPMTQTY = double.Parse(Get_Numeric(this.TXT01_JUNIPMTQTY5.GetValue().ToString()));

                // 미통관잔량 = MT입고량 - 통관량 - BL분할 수량	
                this.TXT01_IPPAJAN5.SetValue(String.Format("{0,9:N3}", dIPMTQTY - dIPPAQTY - dJUNIPMTQTY));
            }
        }
        #endregion

        #region Description : 통관관리 저장 ProcessCheck
        private void BTN65_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.TXT01_MESSAGE5.SetValue("");

            DataTable dt = new DataTable();

            #region Description : 신규 - 저장

            if (fsWK_GUBUN5.ToString() == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_684GA966",
                    this.DTP01_CSIPHANG.GetValue().ToString(),
                    this.CBH01_CSBONSUN.GetValue().ToString(),
                    this.CBH01_CSHWAJU.GetValue().ToString(),
                    this.CBH01_CSHWAMUL.GetValue().ToString(),
                    this.TXT01_CSBLNO.GetValue().ToString(),
                    this.TXT01_CSMSNSEQ.GetValue().ToString(),
                    this.TXT01_CSHSNSEQ.GetValue().ToString(),
                    this.DTP01_CSCUSTIL.GetValue().ToString(),
                    this.TXT01_CSCHASU.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.TXT01_CSBLNO.Focus();

                    e.Successed = false;
                    return;
                }

                // 통관차수 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_685FS976",
                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_CSBONSUN.GetValue().ToString().ToUpper(), // 본선
                    this.CBH01_CSHWAJU.GetValue().ToString().ToUpper(),  // 화주
                    this.CBH01_CSHWAMUL.GetValue().ToString().ToUpper(), // 화물
                    this.TXT01_CSBLNO.GetValue().ToString(),             // B/L번호
                    this.TXT01_CSMSNSEQ.GetValue().ToString(),           // MSN
                    this.TXT01_CSHSNSEQ.GetValue().ToString(),           // HSN
                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString())  // 통관일자
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_CSCHASU.SetValue(dt.Rows[0]["CSCHASU"].ToString());
                }
            }

            #endregion

            if (int.Parse(Get_Date(this.DTP01_CSIPHANG.GetValue().ToString())) > int.Parse(Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_81BHQ436");
                this.DTP01_CSCUSTIL.Focus();

                e.Successed = false;
                return;
            }


            // 수정 삭제 공통

            string sIPMTQTY = string.Empty;
            
            // 입고 파일에서 입고사항을 가져옴
            UP_GET_UTIIPGOF();

            // BL별 입고관리 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_684GG967",
                this.DTP01_CSIPHANG.GetValue().ToString(),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_684H2969");
                this.DTP01_CSIPHANG.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                // 아래의 사항들 체크해야 함
                // 1. 처음 등록 
                // 2. 처음 등록 후 수정
                // 3. 두번째 등록
                // 4. 두번째 등록 후 수정

                string sCSCUQTY = string.Empty;

                DataTable dt5 = new DataTable();

                // BL별 입고관리의 SHORE량을 넘을 수 없음.
                
                sIPMTQTY = String.Format("{0,9:N3}", double.Parse(dt.Rows[0]["IPMTQTY5"].ToString()));


                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_81BNG437",
                    this.DTP01_CSIPHANG.GetValue().ToString(),
                    this.CBH01_CSBONSUN.GetValue().ToString(),
                    this.CBH01_CSHWAJU.GetValue().ToString(),
                    this.CBH01_CSHWAMUL.GetValue().ToString(),
                    this.TXT01_CSBLNO.GetValue().ToString(),
                    this.TXT01_CSMSNSEQ.GetValue().ToString(),
                    this.TXT01_CSHSNSEQ.GetValue().ToString()
                    );

                dt5 = this.DbConnector.ExecuteDataTable();

                if (dt5.Rows.Count > 0)
                {
                    // 이전에 입력된 총 통관량(수정전 통관량도 포함 됨)
                    sCSCUQTY = String.Format("{0,9:N3}", double.Parse(dt5.Rows[0]["CSCUQTY"].ToString()));

                    // B/L별 입고관리의 MT량 < 통관관리에 등록된 통관량 - 수정전 통관량 + 입력한 통관량
                    if (Decimal.Parse(Get_Numeric(sIPMTQTY.ToString())) <
                        (Decimal.Parse(Get_Numeric(sCSCUQTY.ToString())) - 
                         Decimal.Parse(Get_Numeric(fsCSCUQTY.ToString())) + 
                         Decimal.Parse(String.Format("{0,9:N3}", double.Parse(this.TXT01_CSCUQTY.GetValue().ToString()))))
                        )
                    {
                        this.ShowMessage("TY_M_UT_81BNL438");
                        this.DTP01_CSIPHANG.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }


            /* 입항,본선,화주,화물로 등록시
			 * UTICMDTF의 SHORE량과 UTIIPGOF의 MT량 합이 일치하지 않으면
			 * 등록이 안됨
			 */


            sIPMTQTY = "0";

            string sCMSHQTY = string.Empty;
            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_685G0977",
                this.DTP01_CSIPHANG.GetValue().ToString(),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCMSHQTY = dt.Rows[0]["CMSHQTY"].ToString();
            }

            if (Get_Numeric(this.TXT01_CSHSNSEQ.GetValue().ToString()) == "0")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_685G3978",
                    this.DTP01_CSIPHANG.GetValue().ToString(),
                    this.CBH01_CSBONSUN.GetValue().ToString(),
                    this.CBH01_CSHWAJU.GetValue().ToString(),
                    this.CBH01_CSHWAMUL.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sIPMTQTY = dt.Rows[0]["IPMTQTY"].ToString();
                }

                if (sIPMTQTY.ToString() != sCMSHQTY.ToString())
                {
                    this.ShowMessage("TY_M_UT_685G6979");
                    this.DTP01_CSIPHANG.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_CSSINQTY.GetValue().ToString())) < 0)
            {
                this.ShowMessage("TY_M_UT_689BF993");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < 0)
            {
                this.ShowMessage("TY_M_UT_685H4981");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            if (Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < Decimal.Parse(Get_Numeric(this.TXT01_CSCHQTY.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_685H4981");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            if ((Decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(this.TXT01_JUNIPMTQTY5.GetValue().ToString())))
              < Decimal.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(fsCSCUQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_685HD982");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            // 신고번호 
            if (this.TXT01_CSSINNO.GetValue().ToString() == "")
            {
                this.TXT01_IPSINO5.SetValue(this.TXT01_CSSINNO.GetValue().ToString());
            }

            // 통관량
            if (fsWK_GUBUN5.ToString() == "NEW")
            {
                this.TXT01_IPPAQTY5.SetValue(Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()))).ToString());
            }
            else
            {
                this.TXT01_IPPAQTY5.SetValue(Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(fsCSCUQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()))).ToString());
            }

            // 미통관잔량 = MT입고량 - 통관량 - BL분할 수량
            this.TXT01_IPPAJAN5.SetValue(String.Format("{0,9:N3}", Decimal.Parse(this.TXT01_IPMTQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_IPPAQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_JUNIPMTQTY5.GetValue().ToString())));
            // 통관잔량 계산
            this.TXT01_IPJAQTY5.SetValue(Convert.ToString(Decimal.Parse(this.TXT01_IPPAQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_IPCHQTY5.GetValue().ToString())).ToString());


            string sCJCUQTY = string.Empty;
            string sCJCHQTY = string.Empty;

            sCJCUQTY = "0";
            sCJCHQTY = "0";

            // 통관화주파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_685HN984",
                Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),
                this.TXT01_CSCHASU.GetValue().ToString(),
                this.CBH01_CSACTHJ.GetValue().ToString(),
                this.CBH01_CSACTHJ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCJCUQTY = dt.Rows[0]["CJCUQTY"].ToString();
                sCJCHQTY = dt.Rows[0]["CJCHQTY"].ToString();
            }








            if (fsWK_GUBUN5.ToString() == "UPT")
            {
                // 양수도 자료 존재 시 수정불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_689AN991",
                    Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),
                    this.CBH01_CSBONSUN.GetValue().ToString(),
                    this.CBH01_CSHWAJU.GetValue().ToString(),
                    this.CBH01_CSHWAMUL.GetValue().ToString(),
                    this.TXT01_CSBLNO.GetValue().ToString(),
                    this.TXT01_CSMSNSEQ.GetValue().ToString(),
                    this.TXT01_CSHSNSEQ.GetValue().ToString(),
                    Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),
                    this.TXT01_CSCHASU.GetValue().ToString(),
                    this.CBH01_CSACTHJ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_689AP992");
                    this.TXT01_CSCUQTY.Focus();

                    e.Successed = false;
                    return;
                }

                // 양수도가 존재할 수 있으므로 통관화주 파일 변경 불가
                if (fsCSACTHJ.ToString() != this.CBH01_CSACTHJ.GetValue().ToString())
                {
                    this.ShowMessage("TY_M_UT_685HY987");
                    this.TXT01_CSCUQTY.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    //if (double.Parse(Get_Numeric(sCJCHQTY.ToString())) > 0)
                    //{
                    //    this.ShowMessage("TY_M_UT_689E4006");
                    //    this.TXT01_CSCUQTY.Focus();

                    //    e.Successed = false;
                    //    return;
                    //}

                    if (Decimal.Parse(sCJCUQTY.ToString()) - Decimal.Parse(fsCSCUQTY.ToString())
                        + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < Decimal.Parse(sCJCHQTY.ToString()))
                    {
                        this.ShowMessage("TY_M_UT_685HU986");
                        this.TXT01_CSCUQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        //#region Description : 통관관리 수정 ProcessCheck
        //private void BTN65_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    DataTable dt = new DataTable();

        //    // 수정 삭제 공통

        //    // 입고 파일에서 입고사항을 가져옴
        //    UP_GET_UTIIPGOF();

        //    // 입고파일 존재 체크
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_684GG967",
        //        this.DTP01_CSIPHANG.GetValue().ToString(),
        //        this.CBH01_CSBONSUN.GetValue().ToString(),
        //        this.CBH01_CSHWAJU.GetValue().ToString(),
        //        this.CBH01_CSHWAMUL.GetValue().ToString(),
        //        this.TXT01_CSBLNO.GetValue().ToString(),
        //        this.TXT01_CSMSNSEQ.GetValue().ToString(),
        //        this.TXT01_CSHSNSEQ.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count <= 0)
        //    {
        //        this.ShowMessage("TY_M_UT_684H2969");
        //        this.DTP01_CSIPHANG.Focus();

        //        e.Successed = false;
        //        return;
        //    }


        //    /* 입항,본선,화주,화물로 등록시
        //     * UTICMDTF의 SHORE량과 UTIIPGOF의 MT량 합이 일치하지 않으면
        //     * 등록이 안됨
        //     */

        //    string sCMSHQTY = string.Empty;
        //    string sIPMTQTY = string.Empty;

        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_685G0977",
        //        this.DTP01_CSIPHANG.GetValue().ToString(),
        //        this.CBH01_CSBONSUN.GetValue().ToString(),
        //        this.CBH01_CSHWAJU.GetValue().ToString(),
        //        this.CBH01_CSHWAMUL.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        sCMSHQTY = dt.Rows[0]["CMSHQTY"].ToString();
        //    }

        //    if (this.TXT01_CSHSNSEQ.GetValue().ToString() == "0")
        //    {
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_UT_685G3978",
        //            this.DTP01_CSIPHANG.GetValue().ToString(),
        //            this.CBH01_CSBONSUN.GetValue().ToString(),
        //            this.CBH01_CSHWAJU.GetValue().ToString(),
        //            this.CBH01_CSHWAMUL.GetValue().ToString()
        //            );

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            sIPMTQTY = dt.Rows[0]["IPMTQTY"].ToString();
        //        }

        //        if (sIPMTQTY.ToString() != sCMSHQTY.ToString())
        //        {
        //            this.ShowMessage("TY_M_UT_685G6979");
        //            this.DTP01_CSIPHANG.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }

        //    if (double.Parse(Get_Numeric(this.TXT01_CSSINQTY.GetValue().ToString())) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_689BF993");
        //        this.TXT01_CSCUQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if (double.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < 0)
        //    {
        //        this.ShowMessage("TY_M_UT_685H4981");
        //        this.TXT01_CSCUQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if (Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < Decimal.Parse(Get_Numeric(this.TXT01_CSCHQTY.GetValue().ToString())))
        //    {
        //        this.ShowMessage("TY_M_UT_685H4981");
        //        this.TXT01_CSCUQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    if ((Decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(this.TXT01_JUNIPMTQTY5.GetValue().ToString())))
        //      < Decimal.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(fsCSCUQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())))
        //    {
        //        this.ShowMessage("TY_M_UT_685HD982");
        //        this.TXT01_CSCUQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 신고번호 
        //    if (this.TXT01_CSSINNO.GetValue().ToString() == "")
        //    {
        //        this.TXT01_IPSINO5.SetValue(this.TXT01_CSSINNO.GetValue().ToString());
        //    }

        //    // 통관량
        //    this.TXT01_IPPAQTY5.SetValue(Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(fsCSCUQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString()))).ToString());
        //    // 미통관잔량 = MT입고량 - 통관량 - BL분할 수량
        //    this.TXT01_IPPAJAN5.SetValue(String.Format("{0,9:N3}", Decimal.Parse(this.TXT01_IPMTQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_IPPAQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_JUNIPMTQTY5.GetValue().ToString())));
        //    // 통관잔량 계산
        //    this.TXT01_IPJAQTY5.SetValue(Convert.ToString(Decimal.Parse(this.TXT01_IPPAQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_IPCHQTY5.GetValue().ToString())).ToString());


        //    string sCJCUQTY = string.Empty;
        //    string sCJCHQTY = string.Empty;

        //    sCJCUQTY = "0";
        //    sCJCHQTY = "0";

        //    // 통관화주파일
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_685HN984",
        //        Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),
        //        this.CBH01_CSBONSUN.GetValue().ToString(),
        //        this.CBH01_CSHWAJU.GetValue().ToString(),
        //        this.CBH01_CSHWAMUL.GetValue().ToString(),
        //        this.TXT01_CSBLNO.GetValue().ToString(),
        //        this.TXT01_CSMSNSEQ.GetValue().ToString(),
        //        this.TXT01_CSHSNSEQ.GetValue().ToString(),
        //        Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),
        //        this.TXT01_CSCHASU.GetValue().ToString(),
        //        this.CBH01_CSACTHJ.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        sCJCUQTY = dt.Rows[0]["CJCUQTY"].ToString();
        //        sCJCHQTY = dt.Rows[0]["CJCHQTY"].ToString();
        //    }

        //    // 양수도 자료 존재 시 수정불가
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_689AN991",
        //        Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),
        //        this.CBH01_CSBONSUN.GetValue().ToString(),
        //        this.CBH01_CSHWAJU.GetValue().ToString(),
        //        this.CBH01_CSHWAMUL.GetValue().ToString(),
        //        this.TXT01_CSBLNO.GetValue().ToString(),
        //        this.TXT01_CSMSNSEQ.GetValue().ToString(),
        //        this.TXT01_CSHSNSEQ.GetValue().ToString(),
        //        Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),
        //        this.TXT01_CSCHASU.GetValue().ToString(),
        //        this.CBH01_CSACTHJ.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        this.ShowMessage("TY_M_UT_689AP992");
        //        this.TXT01_CSCUQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }

        //    // 양수도가 존재할 수 있으므로 통관화주 파일 변경 불가
        //    if (fsCSACTHJ.ToString() != this.CBH01_CSACTHJ.GetValue().ToString())
        //    {
        //        this.ShowMessage("TY_M_UT_685HY987");
        //        this.TXT01_CSCUQTY.Focus();

        //        e.Successed = false;
        //        return;
        //    }
        //    else
        //    {
        //        if (double.Parse(Get_Numeric(sCJCHQTY.ToString())) > 0)
        //        {
        //            this.ShowMessage("TY_M_UT_689E4006");
        //            this.TXT01_CSCUQTY.Focus();

        //            e.Successed = false;
        //            return;
        //        }

        //        if (Decimal.Parse(sCJCUQTY.ToString()) - Decimal.Parse(fsCSCUQTY.ToString())
        //            + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < Decimal.Parse(sCJCHQTY.ToString()))
        //        {
        //            this.ShowMessage("TY_M_UT_685HU986");
        //            this.TXT01_CSCUQTY.Focus();

        //            e.Successed = false;
        //            return;
        //        }
        //    }

        //    // 수정하시겠습니까?
        //    if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }
        //}
        //#endregion

        #region Description : 통관관리 삭제 ProcessCheck
        private void BTN65_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 수정 삭제 공통

            // 입고 파일에서 입고사항을 가져옴
            UP_GET_UTIIPGOF();

            // 입고파일 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_684GG967",
                this.DTP01_CSIPHANG.GetValue().ToString(),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_684H2969");
                this.DTP01_CSIPHANG.Focus();

                e.Successed = false;
                return;
            }


            /* 입항,본선,화주,화물로 등록시
			 * UTICMDTF의 SHORE량과 UTIIPGOF의 MT량 합이 일치하지 않으면
			 * 등록이 안됨
			 */

            string sCMSHQTY = string.Empty;
            string sIPMTQTY = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_685G0977",
                this.DTP01_CSIPHANG.GetValue().ToString(),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCMSHQTY = dt.Rows[0]["CMSHQTY"].ToString();
            }

            if (this.TXT01_CSHSNSEQ.GetValue().ToString() == "0")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_685G3978",
                    this.DTP01_CSIPHANG.GetValue().ToString(),
                    this.CBH01_CSBONSUN.GetValue().ToString(),
                    this.CBH01_CSHWAJU.GetValue().ToString(),
                    this.CBH01_CSHWAMUL.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sIPMTQTY = dt.Rows[0]["IPMTQTY"].ToString();
                }

                if (sIPMTQTY.ToString() != sCMSHQTY.ToString())
                {
                    this.ShowMessage("TY_M_UT_685G6979");
                    this.DTP01_CSIPHANG.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_CSSINQTY.GetValue().ToString())) < 0)
            {
                this.ShowMessage("TY_M_UT_689BF993");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < 0)
            {
                this.ShowMessage("TY_M_UT_685H4981");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            if (Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())) < Decimal.Parse(Get_Numeric(this.TXT01_CSCHQTY.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_685H4981");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            if ((Decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(this.TXT01_JUNIPMTQTY5.GetValue().ToString())))
              < Decimal.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(fsCSCUQTY.ToString())) + Decimal.Parse(Get_Numeric(this.TXT01_CSCUQTY.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_685HD982");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            // 통관량
            this.TXT01_IPPAQTY5.SetValue(Convert.ToString(Decimal.Parse(Get_Numeric(this.TXT01_IPPAQTY5.GetValue().ToString())) - Decimal.Parse(Get_Numeric(fsCSCUQTY.ToString()))).ToString());
            // 미통관잔량 = MT입고량 - 통관량 - BL분할 수량
            this.TXT01_IPPAJAN5.SetValue(String.Format("{0,9:N3}", Decimal.Parse(this.TXT01_IPMTQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_IPPAQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_JUNIPMTQTY5.GetValue().ToString())));
            // 통관잔량 계산
            this.TXT01_IPJAQTY5.SetValue(Convert.ToString(Decimal.Parse(this.TXT01_IPPAQTY5.GetValue().ToString()) - Decimal.Parse(this.TXT01_IPCHQTY5.GetValue().ToString())).ToString());


            string sCJCUQTY = string.Empty;
            string sCJCHQTY = string.Empty;

            sCJCUQTY = "0";
            sCJCHQTY = "0";

            // 통관화주파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_685HN984",
                Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),
                this.TXT01_CSCHASU.GetValue().ToString(),
                this.CBH01_CSACTHJ.GetValue().ToString(),
                this.CBH01_CSACTHJ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCJCUQTY = dt.Rows[0]["CJCUQTY"].ToString();
                sCJCHQTY = dt.Rows[0]["CJCHQTY"].ToString();
            }

            // 양수도 자료 존재 시 수정불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_689AN991",
                Get_Date(this.DTP01_CSIPHANG.GetValue().ToString()),
                this.CBH01_CSBONSUN.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.CBH01_CSHWAMUL.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSMSNSEQ.GetValue().ToString(),
                this.TXT01_CSHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_CSCUSTIL.GetValue().ToString()),
                this.TXT01_CSCHASU.GetValue().ToString(),
                this.CBH01_CSACTHJ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_689AP992");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }

            // 양수도가 존재할 수 있으므로 통관화주 파일 변경 불가
            if (fsCSACTHJ.ToString() != this.CBH01_CSACTHJ.GetValue().ToString())
            {
                this.ShowMessage("TY_M_UT_685HY987");
                this.TXT01_CSCUQTY.Focus();

                e.Successed = false;
                return;
            }
            else
            {
                if (double.Parse(Get_Numeric(sCJCHQTY.ToString())) > 0)
                {
                    this.ShowMessage("TY_M_UT_689E4006");
                    this.TXT01_CSCUQTY.Focus();

                    e.Successed = false;
                    return;
                }

                if (Decimal.Parse(sCJCUQTY.ToString()) - Decimal.Parse(fsCSCUQTY.ToString()) < Decimal.Parse(sCJCHQTY.ToString()))
                {
                    this.ShowMessage("TY_M_UT_685HU986");
                    this.TXT01_CSCUQTY.Focus();

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




        #region Description : 통관관리 스프레드 이벤트
        private void FPS91_TY_S_UT_684FW965_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_MESSAGE5.SetValue("");

            this.DTP01_CSIPHANG.SetReadOnly(true);
            this.CBH01_CSBONSUN.SetReadOnly(true);
            this.CBH01_CSHWAJU.SetReadOnly(true);
            this.CBH01_CSHWAMUL.SetReadOnly(true);
            this.TXT01_CSBLNO.SetReadOnly(true);
            this.TXT01_CSMSNSEQ.SetReadOnly(true);
            this.TXT01_CSHSNSEQ.SetReadOnly(true);
            this.DTP01_CSCUSTIL.SetReadOnly(true);
            this.TXT01_CSCHASU.SetReadOnly(true);

            this.DTP01_CSIPHANG.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSIPHANG").ToString());
            this.CBH01_CSBONSUN.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSBONSUN").ToString());
            this.CBH01_CSHWAJU.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSHWAJU").ToString());
            this.CBH01_CSHWAMUL.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSHWAMUL").ToString());
            this.TXT01_CSBLNO.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSBLNO").ToString());
            this.TXT01_CSMSNSEQ.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSMSNSEQ").ToString());
            this.TXT01_CSHSNSEQ.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSHSNSEQ").ToString());
            this.DTP01_CSCUSTIL.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSCUSTIL").ToString());
            this.TXT01_CSCHASU.SetValue(this.FPS91_TY_S_UT_684FW965.GetValue("CSCHASU").ToString());

            Timer tmr = new Timer();
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Interval = 100;
            tmr.Start();

            // 통관관리 확인
            UP_UTICUSTF_RUN();
        }
        #endregion

        void tmr_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            this.TXT01_CSCUQTY.Focus();
        } 

        #region Description : 통관관리 전체 스프레드 이벤트
        private void FPS91_TY_S_UT_684FK963_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_CSIPHANG.SetReadOnly(true);
            this.CBH01_CSBONSUN.SetReadOnly(true);
            this.CBH01_CSHWAJU.SetReadOnly(true);
            this.CBH01_CSHWAMUL.SetReadOnly(true);
            this.TXT01_CSBLNO.SetReadOnly(true);
            this.TXT01_CSMSNSEQ.SetReadOnly(true);
            this.TXT01_CSHSNSEQ.SetReadOnly(true);
            this.DTP01_CSCUSTIL.SetReadOnly(true);
            this.TXT01_CSCHASU.SetReadOnly(true);

            this.DTP01_CSIPHANG.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSIPHANG").ToString());
            this.CBH01_CSBONSUN.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSBONSUN").ToString());
            this.CBH01_CSHWAJU.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSHWAJU").ToString());
            this.CBH01_CSHWAMUL.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSHWAMUL").ToString());
            this.TXT01_CSBLNO.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSBLNO").ToString());
            this.TXT01_CSMSNSEQ.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSMSNSEQ").ToString());
            this.TXT01_CSHSNSEQ.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSHSNSEQ").ToString());
            this.DTP01_CSCUSTIL.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSCUSTIL").ToString());
            this.TXT01_CSCHASU.SetValue(this.FPS91_TY_S_UT_684FK963.GetValue("CSCHASU").ToString());

            // 통관관리 조회
            UP_UTICUSTF_TAB_SEARCH(this.DTP01_CSIPHANG.GetValue().ToString(),
                                   this.CBH01_CSBONSUN.GetValue().ToString(),
                                   this.CBH01_CSHWAJU.GetValue().ToString(),
                                   this.CBH01_CSHWAMUL.GetValue().ToString(),
                                   this.TXT01_CSBLNO.GetValue().ToString(),
                                   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                                   this.TXT01_CSHSNSEQ.GetValue().ToString()
                                   );

            // 통관관리 확인
            UP_UTICUSTF_RUN();
        }
        #endregion

        #region Description : 입항조회 버튼
        private void BTN65_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            TYUTGB003S popup = new TYUTGB003S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_CSIPHANG.SetValue(popup.fsIPHANG); // 입항일자
                this.CBH01_CSBONSUN.SetValue(popup.fsBONSUN); // 본선명

                // 값 저장
                UP_SET_Cookie5(this.DTP01_CSIPHANG.GetValue().ToString(), this.CBH01_CSBONSUN.GetValue().ToString(),
                               this.CBH01_CSHWAJU.GetValue().ToString(),  this.CBH01_CSHWAMUL.GetValue().ToString(),
                               this.TXT01_CSBLNO.GetValue().ToString(),   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                               this.TXT01_CSHSNSEQ.GetValue().ToString(), this.DTP01_CSCUSTIL.GetValue().ToString(),
                               this.TXT01_CSCHASU.GetValue().ToString());

                SetFocus(this.CBH01_CSHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 입고조회 버튼
        private void BTN65_UTTCODEHELP2_Click(object sender, EventArgs e)
        {
            TYUTGB004S popup = new TYUTGB004S(this.DTP01_CSIPHANG.GetValue().ToString(), this.CBH01_CSBONSUN.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_CSHWAJU.SetValue(popup.fsHWAJU);   // 화주
                this.CBH01_CSHWAMUL.SetValue(popup.fsHWAMUL); // 화물

                // 값 저장
                UP_SET_Cookie5(this.DTP01_CSIPHANG.GetValue().ToString(), this.CBH01_CSBONSUN.GetValue().ToString(),
                               this.CBH01_CSHWAJU.GetValue().ToString(),  this.CBH01_CSHWAMUL.GetValue().ToString(),
                               this.TXT01_CSBLNO.GetValue().ToString(),   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                               this.TXT01_CSHSNSEQ.GetValue().ToString(), this.DTP01_CSCUSTIL.GetValue().ToString(),
                               this.TXT01_CSCHASU.GetValue().ToString());

                SetFocus(this.TXT01_CSBLNO);
            }
        }
        #endregion

        #region Description : B/L별 입고조회 버튼
        private void BTN65_UTTCODEHELP5_Click(object sender, EventArgs e)
        {
            TYUTGB006S popup = new TYUTGB006S(this.DTP01_CSIPHANG.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_CSIPHANG.SetValue(popup.fsIPHANG); // 입항일자
                this.CBH01_CSBONSUN.SetValue(popup.fsBONSUN); // 본선
                this.CBH01_CSHWAJU.SetValue(popup.fsHWAJU);   // 화주
                this.CBH01_CSHWAMUL.SetValue(popup.fsHWAMUL); // 화물
                this.TXT01_CSBLNO.SetValue(popup.fsBLNO);     // BL번호
                this.TXT01_CSMSNSEQ.SetValue(popup.fsMSNSEQ); // MSN번호
                this.TXT01_CSHSNSEQ.SetValue(popup.fsHSNSEQ); // HSN번호

                // 입고 파일에서 입고사항을 가져옴
                UP_GET_UTIIPGOF();

                // 값 저장
                UP_SET_Cookie5(this.DTP01_CSIPHANG.GetValue().ToString(), this.CBH01_CSBONSUN.GetValue().ToString(),
                               this.CBH01_CSHWAJU.GetValue().ToString(),  this.CBH01_CSHWAMUL.GetValue().ToString(),
                               this.TXT01_CSBLNO.GetValue().ToString(),   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                               this.TXT01_CSHSNSEQ.GetValue().ToString(), this.DTP01_CSCUSTIL.GetValue().ToString(),
                               this.TXT01_CSCHASU.GetValue().ToString());

                SetFocus(this.DTP01_CSCUSTIL);
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_UTICUSTF_FieldClear(string sGUBUN)
        {
            this.DTP01_CSIPHANG.SetReadOnly(false);
            this.CBH01_CSBONSUN.SetReadOnly(false);
            this.CBH01_CSHWAJU.SetReadOnly(false);
            this.CBH01_CSHWAMUL.SetReadOnly(false);
            this.TXT01_CSBLNO.SetReadOnly(false);
            this.TXT01_CSMSNSEQ.SetReadOnly(false);
            this.TXT01_CSHSNSEQ.SetReadOnly(false);
            this.DTP01_CSCUSTIL.SetReadOnly(false);
            this.TXT01_CSCHASU.SetReadOnly(false);

            if (sGUBUN.ToString() == "NEW")
            {
                this.DTP01_CSCUSTIL.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                this.DTP01_CSCHENDIL.SetValue("");

                this.DTP01_EDAEXTSDATE.SetValue("");
                this.DTP01_EDAEXTEDATE.SetValue("");

                this.DTP01_EDADATE.SetValue("");

                // 적하중량
                this.TXT01_IPBSQTY5.SetValue("0");
                // K/L량
                this.TXT01_IPKLQTY5.SetValue("0");
                // B/L분할수량
                this.TXT01_JUNIPMTQTY5.SetValue("0");
                // M/T량
                this.TXT01_IPMTQTY5.SetValue("0");
                // 통관량
                this.TXT01_IPPAQTY5.SetValue("0");
                // 미통관잔량
                this.TXT01_IPPAJAN5.SetValue("0");
                // 출고량
                this.TXT01_IPCHQTY5.SetValue("0");
                // 통관잔량       
                this.TXT01_IPJAQTY5.SetValue("0");
                // 반입번호
                this.TXT01_IPSINO5.SetValue("0");

                // 출고량
                this.TXT01_CSCHQTY.SetValue("0");
                // 재고량
                this.TXT01_CSJGQTY.SetValue("0");
            }
            else
            {
                // 적하중량
                this.TXT01_IPBSQTY5.SetValue("0");
                // K/L량
                this.TXT01_IPKLQTY5.SetValue("0");
                // B/L분할수량
                this.TXT01_JUNIPMTQTY5.SetValue("0");
                // M/T량
                this.TXT01_IPMTQTY5.SetValue("0");
                // 통관량
                this.TXT01_IPPAQTY5.SetValue("0");
                // 미통관잔량
                this.TXT01_IPPAJAN5.SetValue("0");
                // 출고량
                this.TXT01_IPCHQTY5.SetValue("0");
                // 통관잔량       
                this.TXT01_IPJAQTY5.SetValue("0");
                // 반입번호
                this.TXT01_IPSINO5.SetValue("0");

                this.TXT01_CSCUQTY.SetValue("");
                this.TXT01_CSCHQTY.SetValue("");
                this.TXT01_CSJGQTY.SetValue("");
                this.TXT01_CSCOSTUS.SetValue("");
                this.TXT01_CSCOSTWO.SetValue("");
                this.TXT01_CSSINNO.SetValue("");
                this.CBH01_CSSINNM.SetValue("");
                this.TXT01_CSSINQTY.SetValue("");
                this.CBH01_CSACTHJ.SetValue("");
                this.CBH01_CSBANGB.SetValue("");
                this.TXT01_EDAAPVALNO.SetValue("");
            }
        }
        #endregion

        #region Description : 통관관리 디스플레이
        private void UP_UTICUSTF_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN65_SAV.Visible = true;
                //this.BTN65_EDIT.Visible = false;
                this.BTN65_REM.Visible = false;

                this.BTN65_UTTCODEHELP1.Visible = true;
                this.BTN65_UTTCODEHELP2.Visible = true;
                this.BTN65_UTTCODEHELP5.Visible = true;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN65_SAV.Visible = true;
                //this.BTN65_EDIT.Visible = true;
                this.BTN65_REM.Visible = true;

                this.BTN65_UTTCODEHELP1.Visible = false;
                this.BTN65_UTTCODEHELP2.Visible = false;
                this.BTN65_UTTCODEHELP5.Visible = false;
            }
            else
            {
                this.BTN65_SAV.Visible = false;
                //this.BTN65_EDIT.Visible = false;
                this.BTN65_REM.Visible = false;

                this.BTN65_UTTCODEHELP1.Visible = false;
                this.BTN65_UTTCODEHELP2.Visible = false;
                this.BTN65_UTTCODEHELP5.Visible = false;
            }
        }
        #endregion

        #region Description : 출고종료일자 이벤트
        private void DTP01_CSBANEDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN65_SAV.Visible == true)
                {
                    SetFocus(this.BTN65_SAV);
                }
                //else if (this.BTN65_EDIT.Visible == true)
                //{
                //    SetFocus(this.BTN65_EDIT);
                //}
            }
        }
        #endregion

        #endregion






        #region Description : 필드변수에 저장
        private void UP_Set_Field()
        {            
        }
        #endregion

        #region Description : 필터
        private string UP_BUNJA_Filter(string sBUNJA)
        {
            string sValue = "";
            for (int i = 0; i < sBUNJA.Length; i++)
            {
                if (sBUNJA.Substring(i, 1) != "/")
                {
                    sValue = sValue + sBUNJA.Substring(i, 1);
                }
                else
                {
                    break;
                }
            }

            return sValue;
        }

        private string UP_BUNMO_Filter(string sBUNMO)
        {
            string sValue = "";

            int iLength = 0;


            for (int i = 0; i < sBUNMO.Length; i++)
            {
                if (sBUNMO.Substring(i, 1) == "/")
                {
                    iLength = i + 1;

                    break;
                }
            }

            for (int j = iLength; j < sBUNMO.Length; j++)
            {
                sValue = sValue + sBUNMO.Substring(j, 1);
            }

            return sValue;
        }
        #endregion

        #region Description : 쿠키 불러오기
        private void UP_Cookie_Load()
        {
            if (TYCookie.Chk == "Cookie")
            {
            }
            else
            {
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            //TYCookie.Save(this.TXT01_VSYEAR.GetValue().ToString(), this.CBO01_VSBRANCH.GetValue().ToString(), this.CBO01_VSCONFGB.GetValue().ToString());
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // 입항 및 통관관리 조회
            {
                fsTAB_GUBUN = "INVENTORY";

                // INVENTORY 전체 조회
                UP_INVENTORY_SEARCH();
            }
            else if (tabControl1.SelectedIndex == 1) // 입항관리
            {
                fsTAB_GUBUN = "UTIVESLF";

                UP_GET_Cookie1();

                UP_UTIVESLF_TAB_SEARCH();

                UP_UTIVESLF_BTN_DISPLAY("TAB");

                // 입항관리 전체 조회
                UP_UTIVESLF_SEARCH();

                this.TXT01_MESSAGE1.SetValue("");
            }
            else if (tabControl1.SelectedIndex == 2) // 입고화물관리
            {
                fsTAB_GUBUN = "UTICMDTF";

                UP_GET_Cookie2();

                UP_UTICMDTF_TAB_SEARCH();

                // 입고화물관리 전체 조회
                UP_UTICMDTF_SEARCH();

                UP_UTICMDTF_BTN_DISPLAY("TAB");

                this.TXT01_MESSAGE2.SetValue("");
            }
            else if (tabControl1.SelectedIndex == 3) // SURVEY 관리
            {
                fsTAB_GUBUN = "UTISURVF";

                UP_GET_Cookie3();

                UP_UTISURVF_TAB_SEARCH(this.DTP01_SVIPHANG.GetValue().ToString(),
                                       this.CBH01_SVBONSUN.GetValue().ToString(),
                                       this.CBH01_SVHWAJU.GetValue().ToString(),
                                       this.CBH01_SVHWAMUL.GetValue().ToString(),
                                       ""
                                       );

                // SURVEY REPORT 전체 조회
                UP_UTISURVF_SEARCH();

                UP_UTISURVF_BTN_DISPLAY("TAB");

                this.TXT01_MESSAGE3.SetValue("");
            }
            else if (tabControl1.SelectedIndex == 4) // B/L별 입고관리
            {
                fsTAB_GUBUN = "UTIIPGOF";

                UP_GET_Cookie4();

                UP_UTIIPGOF_TAB_SEARCH(this.DTP01_IPIPHANG.GetValue().ToString(),
                                       this.CBH01_IPBONSUN.GetValue().ToString(),
                                       this.CBH01_IPHWAJU.GetValue().ToString(),
                                       this.CBH01_IPHWAMUL.GetValue().ToString(),
                                       this.TXT01_IPBLNO.GetValue().ToString()
                                       );

                // B/L별 입고관리 전체 조회
                UP_UTIIPGOF_SEARCH();

                UP_UTIIPGOF_BTN_DISPLAY("TAB");

                this.TXT01_MESSAGE4.SetValue("");
            }
            else if (tabControl1.SelectedIndex == 5) // 통관관리
            {
                fsTAB_GUBUN = "UTICUSTF";

                UP_GET_Cookie5();

                UP_UTICUSTF_TAB_SEARCH(this.DTP01_CSIPHANG.GetValue().ToString(),
                                       this.CBH01_CSBONSUN.GetValue().ToString(),
                                       this.CBH01_CSHWAJU.GetValue().ToString(),
                                       this.CBH01_CSHWAMUL.GetValue().ToString(),
                                       this.TXT01_CSBLNO.GetValue().ToString(),
                                       this.TXT01_CSMSNSEQ.GetValue().ToString(),
                                       this.TXT01_CSHSNSEQ.GetValue().ToString()
                                       );

                // 통관관리 전체 조회
                UP_UTICUSTF_SEARCH();

                UP_UTICUSTF_BTN_DISPLAY("TAB");

                this.TXT01_MESSAGE5.SetValue("");
            }
        }
        #endregion

        #region Description : 입항관리 값 저장하기
        private void UP_SET_Cookie1(string sIPHANG, string sBONSUN)
        {
            fsIPHANG = sIPHANG.ToString();
            fsBONSUN = sBONSUN.ToString();
        }
        #endregion

        #region Description : 입항관리 값 가져오기
        private void UP_GET_Cookie1()
        {
            this.DTP01_VSIPHANG.SetValue(fsIPHANG);
            this.CBH01_VSBONSUN.SetValue(fsBONSUN);

            this.DTP01_VSIPHANG.SetReadOnly(true);
            this.CBH01_VSBONSUN.SetReadOnly(true);
        }
        #endregion


        #region Description : 입고화물관리 값 저장하기
        private void UP_SET_Cookie2(string sIPHANG, string sBONSUN, string sHWAJU, string sHWAMUL)
        {
            fsIPHANG = sIPHANG.ToString();
            fsBONSUN = sBONSUN.ToString();
            fsHWAJU  = sHWAJU.ToString();
            fsHWAMUL = sHWAMUL.ToString();
        }
        #endregion

        #region Description : 입고화물관리 값 가져오기
        private void UP_GET_Cookie2()
        {
            this.DTP01_CMIPHANG.SetValue(fsIPHANG);
            this.CBH01_CMBONSUN.SetValue(fsBONSUN);
            this.CBH01_CMHWAJU.SetValue(fsHWAJU);
            this.CBH01_CMHWAMUL.SetValue(fsHWAMUL);

            this.DTP01_CMIPHANG.SetReadOnly(true);
            this.CBH01_CMBONSUN.SetReadOnly(true);
            this.CBH01_CMHWAJU.SetReadOnly(true);
            this.CBH01_CMHWAMUL.SetReadOnly(true);
        }
        #endregion


        #region Description : SURVEY REPORT 값 저장하기
        private void UP_SET_Cookie3(string sIPHANG, string sBONSUN, string sHWAJU, string sHWAMUL)
        {
            fsIPHANG = sIPHANG.ToString();
            fsBONSUN = sBONSUN.ToString();
            fsHWAJU = sHWAJU.ToString();
            fsHWAMUL = sHWAMUL.ToString();
        }
        #endregion

        #region Description : SURVEY REPORT 값 가져오기
        private void UP_GET_Cookie3()
        {
            this.DTP01_SVIPHANG.SetValue(fsIPHANG);
            this.CBH01_SVBONSUN.SetValue(fsBONSUN);
            this.CBH01_SVHWAJU.SetValue(fsHWAJU);
            this.CBH01_SVHWAMUL.SetValue(fsHWAMUL);

            this.DTP01_SVIPHANG.SetReadOnly(true);
            this.CBH01_SVBONSUN.SetReadOnly(true);
            this.CBH01_SVHWAJU.SetReadOnly(true);
            this.CBH01_SVHWAMUL.SetReadOnly(true);
        }
        #endregion

        #region Description : B/L별 입고관리 값 저장하기
        private void UP_SET_Cookie4(string sIPHANG, string sBONSUN, string sHWAJU, string sHWAMUL,
                                    string sBLNO,   string sMSN,    string sHSN)
        {
            fsIPHANG = sIPHANG.ToString();
            fsBONSUN = sBONSUN.ToString();
            fsHWAJU  = sHWAJU.ToString();
            fsHWAMUL = sHWAMUL.ToString();
            fsBLNO   = sBLNO.ToString();
            fsMSN    = sMSN.ToString();
            fsHSN    = sHSN.ToString();
        }
        #endregion

        #region Description : B/L별 입고관리 값 가져오기
        private void UP_GET_Cookie4()
        {
            this.DTP01_IPIPHANG.SetValue(fsIPHANG);
            this.CBH01_IPBONSUN.SetValue(fsBONSUN);
            this.CBH01_IPHWAJU.SetValue(fsHWAJU);
            this.CBH01_IPHWAMUL.SetValue(fsHWAMUL);
            this.TXT01_IPBLNO.SetValue(fsBLNO);
            this.TXT01_IPMSNSEQ.SetValue(fsMSN);
            this.TXT01_IPHSNSEQ.SetValue(fsHSN);


            this.DTP01_IPIPHANG.SetReadOnly(true);
            this.CBH01_IPBONSUN.SetReadOnly(true);
            this.CBH01_IPHWAJU.SetReadOnly(true);
            this.CBH01_IPHWAMUL.SetReadOnly(true);
            this.TXT01_IPBLNO.SetReadOnly(true);
            this.TXT01_IPMSNSEQ.SetReadOnly(true);
            this.TXT01_IPHSNSEQ.SetReadOnly(true);
        }
        #endregion

        #region Description : 통관관리 값 저장하기
        private void UP_SET_Cookie5(string sIPHANG, string sBONSUN, string sHWAJU, string sHWAMUL,
                                    string sBLNO,   string sMSN,    string sHSN,   string sCUSTIL,
                                    string sCHASU)
        {
            fsIPHANG = sIPHANG.ToString();
            fsBONSUN = sBONSUN.ToString();
            fsHWAJU  = sHWAJU.ToString();
            fsHWAMUL = sHWAMUL.ToString();
            fsBLNO   = sBLNO.ToString();
            fsMSN    = sMSN.ToString();
            fsHSN    = sHSN.ToString();
            fsCUSTIL = sCUSTIL.ToString();
            fsCHASU  = sCHASU.ToString();
        }
        #endregion

        #region Description : 통관관리 값 가져오기
        private void UP_GET_Cookie5()
        {
            this.DTP01_CSIPHANG.SetValue(fsIPHANG);
            this.CBH01_CSBONSUN.SetValue(fsBONSUN);
            this.CBH01_CSHWAJU.SetValue(fsHWAJU);
            this.CBH01_CSHWAMUL.SetValue(fsHWAMUL);
            this.TXT01_CSBLNO.SetValue(fsBLNO);
            this.TXT01_CSMSNSEQ.SetValue(fsMSN);
            this.TXT01_CSHSNSEQ.SetValue(fsHSN);
            this.DTP01_CSCUSTIL.SetValue(fsCUSTIL);
            this.TXT01_CSCHASU.SetValue(fsCHASU);


            this.DTP01_CSIPHANG.SetReadOnly(true);
            this.CBH01_CSBONSUN.SetReadOnly(true);
            this.CBH01_CSHWAJU.SetReadOnly(true);
            this.CBH01_CSHWAMUL.SetReadOnly(true);
            this.TXT01_CSBLNO.SetReadOnly(true);
            this.TXT01_CSMSNSEQ.SetReadOnly(true);
            this.TXT01_CSHSNSEQ.SetReadOnly(true);
            this.DTP01_CSCUSTIL.SetReadOnly(true);
            this.TXT01_CSCHASU.SetReadOnly(true);
        }
        #endregion

        #region Description : 이전 HSN Enter 이벤트
        private void TXT01_IPJNHSNSEQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN64_SAV.Visible == true)
                {
                    SetFocus(this.BTN64_SAV);
                }
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_SET_Cookie(string sIPHANG, string sBONSUN, string sHWAJU, string sHWAMUL)
        {
            // 입항관리 확인
            UP_SET_Cookie1(this.DTP01_VSIPHANG.GetValue().ToString(), this.CBH01_VSBONSUN.GetValue().ToString());

            // 값 저장
            UP_SET_Cookie2(this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString(),
                           this.CBH01_CMHWAJU.GetValue().ToString(),  this.CBH01_CMHWAMUL.GetValue().ToString());


            // 값 저장
            UP_SET_Cookie3(this.DTP01_SVIPHANG.GetValue().ToString(), this.CBH01_SVBONSUN.GetValue().ToString(),
                           this.CBH01_SVHWAJU.GetValue().ToString(),  this.CBH01_SVHWAMUL.GetValue().ToString());

            // 값 저장
            UP_SET_Cookie4(this.DTP01_IPIPHANG.GetValue().ToString(), this.CBH01_IPBONSUN.GetValue().ToString(),
                           this.CBH01_IPHWAJU.GetValue().ToString(),  this.CBH01_IPHWAMUL.GetValue().ToString(),
                           this.TXT01_IPBLNO.GetValue().ToString(),   this.TXT01_IPMSNSEQ.GetValue().ToString(),
                           this.TXT01_IPHSNSEQ.GetValue().ToString());

            // 값 저장
            UP_SET_Cookie5(this.DTP01_CSIPHANG.GetValue().ToString(), this.CBH01_CSBONSUN.GetValue().ToString(),
                           this.CBH01_CSHWAJU.GetValue().ToString(),  this.CBH01_CSHWAMUL.GetValue().ToString(),
                           this.TXT01_CSBLNO.GetValue().ToString(),   this.TXT01_CSMSNSEQ.GetValue().ToString(),
                           this.TXT01_CSHSNSEQ.GetValue().ToString(), this.DTP01_CSCUSTIL.GetValue().ToString(),
                           this.TXT01_CSCHASU.GetValue().ToString());
        }
        #endregion

        #region Description : 본선 이벤트
        private void CBH01_VSBONSUN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsWK_GUBUN1.ToString() == "NEW")
                {
                    // 선박사양관리 확인
                    UP_UTIVESSF_RUN();
                }
            }
        }
        #endregion

        //#region Description : 외항입항일 이벤트
        //private void MTB01_VSIPHANG2_Leave(object sender, EventArgs e)
        //{
        //    //if (this.MTB01_VSIPHANG2.GetValue().ToString().Trim().Replace("-", "").Length == 8)
        //    //{
        //    //    this.SetFocus(this.TXT01_VSIPTIM1);
        //    //}

        //    //this.SetFocus(this.TXT01_VSIPTIM1);
        //}
        //#endregion

        //private void MTB01_VSIPHANG2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (this.MTB01_VSIPHANG2.GetValue().ToString().Trim().Replace("-", "").Length == 8)
        //    {
        //        if (e.KeyChar == '\r')
        //        {
        //            this.SetFocus(this.TXT01_VSIPTIM1);
        //        }
        //    }
        //}

        //private void MTB01_VSIPHANG2_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (Convert.ToInt32(e.KeyCode) == 13)
        //    {
        //        this.SetFocus(this.TXT01_VSIPTIM1);
        //    }
        //}

        //private void CBH01_VSJUBAN_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        // FOCUS
        //        Timer tmr = new Timer();

        //        tmr.Tick += delegate
        //        {
        //            tmr.Stop();
        //            this.SetFocus(this.MTB01_VSIPHANG2);
        //        };

        //        tmr.Interval = 100;
        //        tmr.Start();
        //    }
        //}
    }
}