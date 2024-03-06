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
    public partial class TYUTIN012I : TYBase
    {
        private int fiRow         = 0;

        private string fsMVAUTOGB = string.Empty;
        private string fsMVMOQTY  = string.Empty;
        private string fsGUBUN    = string.Empty;

        #region Description : 페이지 로드 
        public TYUTIN012I()
        {
            InitializeComponent();
        }

        private void TYUTIN012I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BUTTON_Visible("");

            this.DTP01_STIPHANG.SetValue(System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            this.DTP01_EDIPHANG.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STIPHANG);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_SEARCH();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsMVMOQTY = "0";

            UP_TXTBOX_ReadOnly("NEW");
            UP_BUTTON_Visible("NEW");

            UP_FieldClear("");

            fsGUBUN = "NEW";

            SetFocus(this.TXT01_MVTANKNO);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "UPT")
            {
                UP_Del();
            }

            UP_Save();

            UP_TXTBOX_ReadOnly("");
            UP_BUTTON_Visible("");

            SetFocus(this.TXT01_MVTANKNO);

            if (Get_Date(this.DTP01_STIPHANG.GetValue().ToString()) != "" &&
                Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()) != "")
            {
                this.BTN61_INQ_Click(null, null);
            }

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_Del();

            UP_TXTBOX_ReadOnly("");
            UP_BUTTON_Visible("");

            UP_FieldClear("DEL");

            SetFocus(this.TXT01_MVTANKNO);

            if (Get_Date(this.DTP01_STIPHANG.GetValue().ToString()) != "" &&
                Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()) != "")
            {
                this.BTN61_INQ_Click(null, null);
            }

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : DRUM 포장 전체 조회 메소드
        private void UP_SEARCH()
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            if (Get_Date(this.DTP01_STIPHANG.GetValue().ToString()) == "")
            {
                sSTDATE = "19800101";
            }
            else
            {
                sSTDATE = Get_Date(this.DTP01_STIPHANG.GetValue().ToString());
            }

            if (Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()) == "")
            {
                sEDDATE = Get_Date(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                sEDDATE = Get_Date(this.DTP01_EDIPHANG.GetValue().ToString());
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_7BEEG990",
                sSTDATE.ToString(),
                sEDDATE.ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString(),
                this.TXT01_GATANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6CDBM091.SetValue(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sBlank = string.Empty;

                sBlank = "0";

                if (i != 0)
                {
                    if ((this.FPS91_TY_S_UT_6CDBM091.GetValue(i, "MVIPHANG1").ToString() == this.FPS91_TY_S_UT_6CDBM091.GetValue(i - 1, "MVIPHANG1").ToString()) &&
                        (this.FPS91_TY_S_UT_6CDBM091.GetValue(i, "MVBONSUN1").ToString() == this.FPS91_TY_S_UT_6CDBM091.GetValue(i - 1, "MVBONSUN1").ToString()) &&
                        (this.FPS91_TY_S_UT_6CDBM091.GetValue(i, "MVHWAJU1").ToString() == this.FPS91_TY_S_UT_6CDBM091.GetValue(i - 1, "MVHWAJU1").ToString()) &&
                        (this.FPS91_TY_S_UT_6CDBM091.GetValue(i, "MVHWAMUL1").ToString() == this.FPS91_TY_S_UT_6CDBM091.GetValue(i - 1, "MVHWAMUL1").ToString()))
                    {

                        this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 0].Text = "";
                        this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 1].Text = "";
                        this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 2].Text = "";
                        this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 3].Text = "";
                        this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 4].Text = "";
                        this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 5].Text = "";
                        this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 6].Text = "";
                    }
                }

                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 7].Font  = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 8].Font  = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 9].Font  = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 12].Font = new Font("굴림", 9, FontStyle.Bold);

                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 7].BackColor  = Color.SkyBlue;
                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 8].BackColor  = Color.SkyBlue;
                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 9].BackColor  = Color.SkyBlue;
                this.FPS91_TY_S_UT_6CDBM091.ActiveSheet.Cells[i, 12].BackColor = Color.SkyBlue;

            }
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            fsMVMOQTY  = "0";
            fsMVAUTOGB = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDDA094",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_MVBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_MVHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_MVHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_MVTANKNO.GetValue().ToString().Trim(),    // 탱크번호
                Get_Date(this.DTP01_MVMVIL.GetValue().ToString()),   // 이고일자
                this.TXT01_MVMVTANK.GetValue().ToString().Trim()     // 이고탱크번호
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsMVMOQTY = dt.Rows[0]["MVMOQTY"].ToString();

                fsMVAUTOGB = dt.Rows[0]["MVAUTOGB"].ToString();

                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "UPT";

                UP_BUTTON_Visible(fsGUBUN);

                UP_TXTBOX_ReadOnly("UPT");

                SetFocus(this.TXT01_MVMOQTY);
            }
        }
        #endregion

        #region Description : 저장 메소드
        private void UP_Save()
        {
            DataTable dt = new DataTable();

            #region Description : 입고화물관리

            string sCMTANO1  = string.Empty;
            string sCMTANO2  = string.Empty;
            string sCMTANO3  = string.Empty;
            string sCMTANO4  = string.Empty;
            string sCMTANO5  = string.Empty;
            string sCMTANO6  = string.Empty;
            string sCMTANO7  = string.Empty;
            string sCMTANO8  = string.Empty;
            string sCMTANO9  = string.Empty;
            string sCMTANO10 = string.Empty;

            string sMVMVTANK = string.Empty;

            sMVMVTANK = Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString().Trim());

            // 입고화물관리 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDEM101",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCMTANO1  = dt.Rows[0]["CMTANO1"].ToString();
                sCMTANO2  = dt.Rows[0]["CMTANO2"].ToString();
                sCMTANO3  = dt.Rows[0]["CMTANO3"].ToString();
                sCMTANO4  = dt.Rows[0]["CMTANO4"].ToString();
                sCMTANO5  = dt.Rows[0]["CMTANO5"].ToString();
                sCMTANO6  = dt.Rows[0]["CMTANO6"].ToString();
                sCMTANO7  = dt.Rows[0]["CMTANO7"].ToString();
                sCMTANO8  = dt.Rows[0]["CMTANO8"].ToString();
                sCMTANO9  = dt.Rows[0]["CMTANO9"].ToString();
                sCMTANO10 = dt.Rows[0]["CMTANO10"].ToString();
            }
            
            

            if (Set_TankNo(sCMTANO1.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO2.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO3.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO4.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO5.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO6.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO7.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO8.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO9.ToString())  != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO10.ToString()) != sMVMVTANK.ToString()
                )
            {
                if (sCMTANO1.ToString() == "")
                {
                    sCMTANO1 = sMVMVTANK.ToString();
                }
                else if (sCMTANO2.ToString() == "")
                {
                    sCMTANO2 = sMVMVTANK.ToString();
                }

                else if (sCMTANO3.ToString() == "")
                {
                    sCMTANO3 = sMVMVTANK.ToString();
                }
                else if (sCMTANO4.ToString() == "")
                {
                    sCMTANO4 = sMVMVTANK.ToString();
                }
                else if (sCMTANO5.ToString() == "")
                {
                    sCMTANO5 = sMVMVTANK.ToString();
                }
                else if (sCMTANO6.ToString() == "")
                {
                    sCMTANO6 = sMVMVTANK.ToString();
                }
                else if (sCMTANO7.ToString() == "")
                {
                    sCMTANO7 = sMVMVTANK.ToString();
                }
                else if (sCMTANO8.ToString() == "")
                {
                    sCMTANO8 = sMVMVTANK.ToString();
                }
                else if (sCMTANO9.ToString() == "")
                {
                    sCMTANO9 = sMVMVTANK.ToString();
                }
                else if (sCMTANO10.ToString() == "")
                {
                    sCMTANO10 = sMVMVTANK.ToString();
                }
            }
            #endregion



            #region Description : 탱크제원 관리

            string sTNTANKNO = string.Empty;
			string sTNHWAMUL = string.Empty;
			string sTNHMGB   = string.Empty;
			string sTNBIJUNG = string.Empty;
			string sTNVCF    = string.Empty;
			string sTNTEMP   = string.Empty;
			string sTNTEMPGB = string.Empty;
			string sTNKESAN  = string.Empty;
			string sTNIPHANG = string.Empty;
			string sTNBONSUN = string.Empty;

			string sTTHWAMUL = string.Empty;
			string sTTHMGB   = string.Empty;
			string sTTBIJUNG = string.Empty;
			string sTTVCF    = string.Empty;
			string sTTTEMP   = string.Empty;
			string sTTTEMPGB = string.Empty;
			string sTTKESAN  = string.Empty;
			string sTTIPHANG = string.Empty;
			string sTTBONSUN = string.Empty;
			string sSql      = string.Empty;

            // 탱크제원 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6CDEZ104", this.TXT01_MVTANKNO.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sTNTANKNO = dt.Rows[0]["TNTANKNO"].ToString(); // 탱크번호
                sTNHWAMUL = dt.Rows[0]["TNHWAMUL"].ToString(); // 화물
                sTNHMGB   = dt.Rows[0]["TNHMGB"].ToString();   // 화물구분
                sTNBIJUNG = dt.Rows[0]["TNBIJUNG"].ToString(); // W.C.F
                sTNVCF    = dt.Rows[0]["TNVCF"].ToString();    // V.C
                sTNTEMP   = dt.Rows[0]["TNTEMP"].ToString();   // 온도
                sTNTEMPGB = dt.Rows[0]["TNTEMPGB"].ToString(); // 온도구분
                sTNKESAN  = dt.Rows[0]["TNKESAN"].ToString();  // 계산방법
                sTNIPHANG = dt.Rows[0]["TNIPHANG"].ToString(); // 입항일자
                sTNBONSUN = dt.Rows[0]["TNBONSUN"].ToString(); // 본선

                // 이동후 탱크파일 처리 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6CDEZ104", this.TXT01_MVMVTANK.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sTTHWAMUL = dt.Rows[0]["TNHWAMUL"].ToString(); // 화물
                    sTTHMGB   = dt.Rows[0]["TNHMGB"].ToString();   // 화물구분
                    sTTBIJUNG = dt.Rows[0]["TNBIJUNG"].ToString(); // W.C.F
                    sTTVCF    = dt.Rows[0]["TNVCF"].ToString();    // V.C
                    sTTTEMP   = dt.Rows[0]["TNTEMP"].ToString();   // 온도
                    sTTTEMPGB = dt.Rows[0]["TNTEMPGB"].ToString(); // 온도구분
                    sTTKESAN  = dt.Rows[0]["TNKESAN"].ToString();  // 계산방법
                    sTTIPHANG = dt.Rows[0]["TNIPHANG"].ToString(); // 입항일자
                    sTTBONSUN = dt.Rows[0]["TNBONSUN"].ToString(); // 본선
				}
            }
            #endregion



            #region Description : SURVEY 관리

            string sSVBIJUNG_AGO = string.Empty;
            string sSVVCF_AGO    = string.Empty;
            string sSVTEMP_AGO   = string.Empty;
            string sSVTEMPGB_AGO = string.Empty;
            string sSVMTQTY_AGO  = string.Empty;
            string sSVKLQTY_AGO  = string.Empty;

            double dMVOQTY_AGO    = 0  ;
			double dSVMTQTY_AGO   = 0  ;
			double dSVKLQTY_AGO   = 0  ;
			string sOLDMVOQTY_AGO = "0";

            string sMVOQTY_AGO   = string.Empty;

            // SURVEY파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDHN109",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_MVTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVBIJUNG_AGO  = dt.Rows[0]["SVBIJUNG"].ToString(); // 비중
                sSVVCF_AGO     = dt.Rows[0]["SVVCF"].ToString();    // VCF
                sSVTEMP_AGO    = dt.Rows[0]["SVTEMP"].ToString();   // 입고온도
                sSVTEMPGB_AGO  = dt.Rows[0]["SVTEMPGB"].ToString(); // 입고온도구분
                sSVMTQTY_AGO   = dt.Rows[0]["SVMTQTY"].ToString();  // 입고MT량
                sSVKLQTY_AGO   = dt.Rows[0]["SVKLQTY"].ToString();  // 입고KL량
                sOLDMVOQTY_AGO = fsMVMOQTY.ToString();              // 수정전 이고량


                sMVOQTY_AGO =
                          (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AGO.ToString())))
                        * (double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString())))
                           / double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AGO.ToString()))))).ToString("0.0000000");

                dMVOQTY_AGO  = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sMVOQTY_AGO.ToString())));
                dSVKLQTY_AGO = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AGO.ToString()))) - dMVOQTY_AGO;
                dSVMTQTY_AGO = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AGO.ToString()))) - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString())));
            }

            sSVKLQTY_AGO = Convert.ToString(Math.Round(dSVKLQTY_AGO, 3));

            string sHMBD_GUBUN = string.Empty;

            // 화물 비중파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDI2110",
                this.TXT01_MVMVTANK.GetValue().ToString().Trim(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                sHMBD_GUBUN = "INS";
            }
            else
            {
                sHMBD_GUBUN = "UPT";
            }
            

            // 이동후
            string sSVBIJUNG_AFT = string.Empty;
            string sSVVCF_AFT    = string.Empty;
            string sSVTEMP_AFT   = string.Empty;
            string sSVTEMPGB_AFT = string.Empty;
            string sSVMTQTY_AFT  = string.Empty;
            string sSVKLQTY_AFT  = string.Empty;

            double dMVOQTY_AFT    = 0;
            double dSVMTQTY_AFT   = 0;
            double dSVKLQTY_AFT   = 0;
            string sOLDMVOQTY_AFT = "0";

            string sMVOQTY_AFT   = string.Empty;

            string sSURVEY_GUBUN = string.Empty;

            // 이동후 SURVEY파일 처리
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDHN109",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVBIJUNG_AFT  = dt.Rows[0]["SVBIJUNG"].ToString(); // 비중
                sSVVCF_AFT     = dt.Rows[0]["SVVCF"].ToString();    // VCF
                sSVTEMP_AFT    = dt.Rows[0]["SVTEMP"].ToString();   // 입고온도
                sSVTEMPGB_AFT  = dt.Rows[0]["SVTEMPGB"].ToString(); // 입고온도구분
                sSVMTQTY_AFT   = dt.Rows[0]["SVMTQTY"].ToString();  // 입고MT량
                sSVKLQTY_AFT   = dt.Rows[0]["SVKLQTY"].ToString();  // 입고KL량
                sOLDMVOQTY_AFT = fsMVMOQTY.ToString();              // 수정전 이고량

                if (double.Parse(Get_Numeric(sSVKLQTY_AFT.ToString())) == 0 ||
                        double.Parse(Get_Numeric(sSVMTQTY_AFT.ToString())) == 0)
                {
                    sMVOQTY_AFT = (double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString()))) / double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVBIJUNG_AFT.ToString())))).ToString("0.0000000");
                }
                else
                {
                    sMVOQTY_AFT =
                             (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AFT.ToString())))
                              * (double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString())))
                               / double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AFT.ToString()))))).ToString("0.0000000");
                }

                dMVOQTY_AFT = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sMVOQTY_AFT.ToString())));
                dSVKLQTY_AFT = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AFT.ToString()))) + dMVOQTY_AFT;
                dSVMTQTY_AFT = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AFT.ToString()))) + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString())));

                sSURVEY_GUBUN = "UPT";

                sSVKLQTY_AFT = Convert.ToString(Math.Round(dSVKLQTY_AFT, 3));
            }
            else
            {
                sSVBIJUNG_AFT = sSVBIJUNG_AGO; // 비중
                sSVVCF_AFT    = sSVVCF_AGO;    // VCF
                sSVTEMP_AFT   = sSVTEMP_AGO;   // 입고온도
                sSVTEMPGB_AFT = sSVTEMPGB_AGO; // 입고온도구분
                dSVMTQTY_AFT  = double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString())));
                sSVKLQTY_AFT  = Convert.ToString(Math.Round(dMVOQTY_AGO, 3));

                sSURVEY_GUBUN = "INS";
            }

            

            #endregion



            #region Description : DB 저장

            // 탱크제원 수정
            this.DbConnector.CommandClear();

            if (this.CBO01_MVAUTOGB.GetValue().ToString() == "Y")
            {
                this.DbConnector.Attach("TY_P_UT_6CDGM105",
                                        sTTHWAMUL.ToString(),
                                        sTTHMGB.ToString(),
                                        Get_Numeric(sTTBIJUNG.ToString()),
                                        Get_Numeric(sTTVCF.ToString()),
                                        Get_Numeric(sTTTEMP.ToString()),
                                        sTTTEMPGB.ToString(),
                                        sTTKESAN.ToString(),
                                        sTTIPHANG.ToString(),
                                        sTTBONSUN.ToString(),
                                        this.CBH01_MVHWAMUL.GetValue().ToString(),
                                        sTNHMGB.ToString(),
                                        Get_Numeric(sTNBIJUNG.ToString()),
                                        Get_Numeric(sTNVCF.ToString()),
                                        Get_Numeric(sTNTEMP.ToString()),
                                        sTNTEMPGB.ToString(),
                                        sTNKESAN.ToString(),
                                        sTNIPHANG.ToString(),
                                        sTNBONSUN.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_7C7IC217",
                                        Get_Numeric(sTTTEMP.ToString()),
                                        sTTTEMPGB.ToString(),
                                        sTTKESAN.ToString(),
                                        sTTIPHANG.ToString(),
                                        sTTBONSUN.ToString(),
                                        Get_Numeric(sTNTEMP.ToString()),
                                        sTNTEMPGB.ToString(),
                                        sTNKESAN.ToString(),
                                        sTNIPHANG.ToString(),
                                        sTNBONSUN.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                                        );
            }

            // 이고관리 등록
            this.DbConnector.Attach("TY_P_UT_6CDGW107",
                                    Set_TankNo(this.TXT01_MVTANKNO.GetValue().ToString()),
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                    Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString()),
                                    Get_Date(this.DTP01_MVMVIL.GetValue().ToString()),
                                    this.TXT01_MVIPQTY.GetValue().ToString(),
                                    this.TXT01_MVJEQTY.GetValue().ToString(),
                                    this.TXT01_MVMOQTY.GetValue().ToString(),
                                    this.CBO01_MVAUTOGB.GetValue().ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                    );

            // 입고화물관리 수정
            this.DbConnector.Attach("TY_P_UT_6CDEU103",
                                    sCMTANO1.ToString(),
                                    sCMTANO2.ToString(),
                                    sCMTANO3.ToString(),
                                    sCMTANO4.ToString(),
                                    sCMTANO5.ToString(),
                                    sCMTANO6.ToString(),
                                    sCMTANO7.ToString(),
                                    sCMTANO8.ToString(),
                                    sCMTANO9.ToString(),
                                    sCMTANO10.ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper()
                                    );

            // 화물 비중파일 등록
            if (sHMBD_GUBUN == "INS")
            {
                this.DbConnector.Attach("TY_P_UT_6CDID112",
                                        Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString()),
                                        this.CBH01_MVHWAMUL.GetValue().ToString(),
                                        this.CBH01_MVHWAMUL.GetText().ToString(),
                                        sSVVCF_AGO.ToString(),
                                        sSVBIJUNG_AGO.ToString()
                                        );
            }

            // 등록시 SURVEY 이전탱크 업데이트
            this.DbConnector.Attach("TY_P_UT_8159C394",
                                    "G",
                                    Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString()),
                                    sSVKLQTY_AGO.ToString(),
                                    Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString()),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                    this.TXT01_MVTANKNO.GetValue().ToString().Trim()
                                    );

            // SURVEY 이고탱크 업데이트
            if (sSURVEY_GUBUN == "INS")
            {                
                // SURVEY 이고탱크 저장
                this.DbConnector.Attach("TY_P_UT_6CEEV116",
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                    Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString()),
                                    sSVBIJUNG_AFT.ToString(),
                                    sSVVCF_AFT.ToString(),
                                    sSVTEMP_AFT.ToString(),
                                    sSVTEMPGB_AFT.ToString(),
                                    "T",
                                    Set_TankNo(this.TXT01_MVTANKNO.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString()),
                                    sSVKLQTY_AFT.ToString(),
                                    "0",
                                    "0",
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                    );
            }
            else
            {
                // 등록시 SURVEY 이고탱크 수정
                this.DbConnector.Attach("TY_P_UT_8159Y395",
                                    "T",
                                    Set_TankNo(this.TXT01_MVTANKNO.GetValue().ToString()),
                                    sSVKLQTY_AFT.ToString(),
                                    Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString()),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                    this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                                    );
            }

            this.DbConnector.ExecuteTranQueryList();

            #endregion
        }
        #endregion

        #region Description : 삭제 메소드
        private void UP_Del()
        {
            DataTable dt = new DataTable();

            #region Description : 입고화물관리

            string sCMTANO1 = string.Empty;
            string sCMTANO2 = string.Empty;
            string sCMTANO3 = string.Empty;
            string sCMTANO4 = string.Empty;
            string sCMTANO5 = string.Empty;
            string sCMTANO6 = string.Empty;
            string sCMTANO7 = string.Empty;
            string sCMTANO8 = string.Empty;
            string sCMTANO9 = string.Empty;
            string sCMTANO10 = string.Empty;

            string sMVMVTANK = string.Empty;

            sMVMVTANK = Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString().Trim());

            // 입고화물관리 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDEM101",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCMTANO1 = dt.Rows[0]["CMTANO1"].ToString();
                sCMTANO2 = dt.Rows[0]["CMTANO2"].ToString();
                sCMTANO3 = dt.Rows[0]["CMTANO3"].ToString();
                sCMTANO4 = dt.Rows[0]["CMTANO4"].ToString();
                sCMTANO5 = dt.Rows[0]["CMTANO5"].ToString();
                sCMTANO6 = dt.Rows[0]["CMTANO6"].ToString();
                sCMTANO7 = dt.Rows[0]["CMTANO7"].ToString();
                sCMTANO8 = dt.Rows[0]["CMTANO8"].ToString();
                sCMTANO9 = dt.Rows[0]["CMTANO9"].ToString();
                sCMTANO10 = dt.Rows[0]["CMTANO10"].ToString();
            }



            if (Set_TankNo(sCMTANO1.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO2.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO3.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO4.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO5.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO6.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO7.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO8.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO9.ToString()) != sMVMVTANK.ToString() &&
                Set_TankNo(sCMTANO10.ToString()) != sMVMVTANK.ToString()
                )
            {
                if (sCMTANO10.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO10 = "";
                }
                else if (sCMTANO9.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO9 = "";
                }

                else if (sCMTANO8.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO8 = "";
                }
                else if (sCMTANO7.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO7 = "";
                }
                else if (sCMTANO6.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO6 = "";
                }
                else if (sCMTANO5.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO5 = "";
                }
                else if (sCMTANO4.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO4 = "";
                }
                else if (sCMTANO3.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO3 = "";
                }
                else if (sCMTANO2.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO2 = "";
                }
                else if (sCMTANO1.ToString() == sMVMVTANK.ToString())
                {
                    sCMTANO1 = "";
                }
            }
            #endregion



            #region Description : 탱크제원관리

            string sTNTANKNO = string.Empty;
			string sTNHWAMUL = string.Empty;
			string sTNHMGB   = string.Empty;
			string sTNBIJUNG = string.Empty;
			string sTNVCF    = string.Empty;
			string sTNTEMP   = string.Empty;
			string sTNTEMPGB = string.Empty;
			string sTNKESAN  = string.Empty;
			string sTNIPHANG = string.Empty;
			string sTNBONSUN = string.Empty;

			string sTTHWAMUL = string.Empty;
			string sTTHMGB   = string.Empty;
			string sTTBIJUNG = string.Empty;
			string sTTVCF    = string.Empty;
			string sTTTEMP   = string.Empty;
			string sTTTEMPGB = string.Empty;
			string sTTKESAN  = string.Empty;
			string sTTIPHANG = string.Empty;
			string sTTBONSUN = string.Empty;
			string sSql      = string.Empty;

            // 탱크제원 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6CDEZ104", this.TXT01_MVTANKNO.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sTNTANKNO = dt.Rows[0]["TNTANKNO"].ToString(); //탱크번호
                sTNHWAMUL = dt.Rows[0]["TNHWAMUL"].ToString(); // 화물
                sTNHMGB   = dt.Rows[0]["TNHMGB"].ToString();   // 화물구분
                sTNBIJUNG = dt.Rows[0]["TNBIJUNG"].ToString(); // W.C.F
                sTNVCF    = dt.Rows[0]["TNVCF"].ToString();    // V.C
                sTNTEMP   = dt.Rows[0]["TNTEMP"].ToString();   // 온도
                sTNTEMPGB = dt.Rows[0]["TNTEMPGB"].ToString(); // 온도구분
                sTNKESAN  = dt.Rows[0]["TNKESAN"].ToString();  // 계산방법
                sTNIPHANG = dt.Rows[0]["TNIPHANG"].ToString(); // 입항일자
                sTNBONSUN = dt.Rows[0]["TNBONSUN"].ToString(); // 본선

                // 이동후 탱크파일 처리 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6CDEZ104", this.TXT01_MVMVTANK.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sTTHWAMUL = dt.Rows[0]["TTHM"].ToString(); // 화물
                    sTTHMGB   = dt.Rows[0]["TTHMGB"].ToString();   // 화물구분
                    sTTBIJUNG = dt.Rows[0]["TTBIJUNG"].ToString(); // W.C.F
                    sTTVCF    = dt.Rows[0]["TTVCF"].ToString();    // V.C
                    sTTTEMP   = dt.Rows[0]["TTTEMP"].ToString();   // 온도
                    sTTTEMPGB = dt.Rows[0]["TTTEMPGB"].ToString(); // 온도구분
                    sTTKESAN  = dt.Rows[0]["TNKESAN"].ToString();  // 계산방법
                    sTTIPHANG = dt.Rows[0]["TTIPHANG"].ToString(); // 입항일자
                    sTTBONSUN = dt.Rows[0]["TTBONSUN"].ToString(); // 본선
				}
            }

            #endregion



            #region Description : SURVEY 관리

            string sSVBIJUNG_AGO = string.Empty;
            string sSVVCF_AGO    = string.Empty;
            string sSVTEMP_AGO   = string.Empty;
            string sSVTEMPGB_AGO = string.Empty;
            string sSVMTQTY_AGO  = string.Empty;
            string sSVKLQTY_AGO  = string.Empty;

            string sSVMTQTY_AGO1 = string.Empty;
            string sSVKLQTY_AGO1 = string.Empty;

            double dMVOQTY_AGO    = 0  ;
			double dSVMTQTY_AGO   = 0  ;
			double dSVKLQTY_AGO   = 0  ;
			string sOLDMVOQTY_AGO = "0";

            string sMVOQTY_AGO   = string.Empty;

            // SURVEY파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDHN109",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_MVTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVBIJUNG_AGO  = dt.Rows[0]["SVBIJUNG"].ToString(); // 비중
                sSVVCF_AGO     = dt.Rows[0]["SVVCF"].ToString();    // VCF
                sSVTEMP_AGO    = dt.Rows[0]["SVTEMP"].ToString();   // 입고온도
                sSVTEMPGB_AGO  = dt.Rows[0]["SVTEMPGB"].ToString(); // 입고온도구분
                sSVMTQTY_AGO   = dt.Rows[0]["SVMTQTY"].ToString();  // 입고MT량
                sSVKLQTY_AGO   = dt.Rows[0]["SVKLQTY"].ToString();  // 입고KL량
                sOLDMVOQTY_AGO = fsMVMOQTY.ToString();              // 수정전 이고량

                if (double.Parse(Get_Numeric(sSVMTQTY_AGO.ToString())) == 0)
                {
                    // 이동후 SURVEY파일 처리
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_6CDHN109",
                        Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                        this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                        this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                        this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                        this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sSVMTQTY_AGO1  = dt.Rows[0]["SVMTQTY"].ToString();  // 입고MT량
                        sSVKLQTY_AGO1  = dt.Rows[0]["SVKLQTY"].ToString();  // 입고KL량                        
                        sOLDMVOQTY_AGO = fsMVMOQTY.ToString();              // 수정전 이고량
                    }

                    sMVOQTY_AGO =
                          (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AGO1.ToString())))
                        * (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sOLDMVOQTY_AGO.ToString())))
                           / double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AGO1.ToString()))))).ToString("0.0000000");

                    dMVOQTY_AGO  = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sMVOQTY_AGO.ToString())));
                    dSVKLQTY_AGO = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AGO.ToString()))) + dMVOQTY_AGO;

                    dSVMTQTY_AGO = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AGO.ToString()))) + double.Parse(String.Format("{0,9:N3}", Get_Numeric(sOLDMVOQTY_AGO.ToString())));

                }
                else
                {
                    sMVOQTY_AGO =
                          (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AGO.ToString())))
                        * (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sOLDMVOQTY_AGO.ToString())))
                           / double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AGO.ToString()))))).ToString("0.0000000");

                    dMVOQTY_AGO  = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sMVOQTY_AGO.ToString())));
                    dSVKLQTY_AGO = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AGO.ToString()))) + dMVOQTY_AGO;

                    dSVMTQTY_AGO = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AGO.ToString()))) + double.Parse(String.Format("{0,9:N3}", Get_Numeric(sOLDMVOQTY_AGO.ToString())));
                }
            }

            sSVKLQTY_AGO = Convert.ToString(Math.Round(dSVKLQTY_AGO, 3));


            // 이동후
            string sSVBIJUNG_AFT  = string.Empty;
            string sSVVCF_AFT     = string.Empty;
            string sSVTEMP_AFT    = string.Empty;
            string sSVTEMPGB_AFT  = string.Empty;
            string sSVMTQTY_AFT   = string.Empty;
            string sSVKLQTY_AFT   = string.Empty;

            double dMVOQTY_AFT    = 0;
            double dSVMTQTY_AFT   = 0;
            double dSVKLQTY_AFT   = 0;
            string sOLDMVOQTY_AFT = "0";

            string sMVOQTY_AFT    = string.Empty;

            string sSURVEY_GUBUN  = string.Empty;

            // 이동후 SURVEY파일 처리
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDHN109",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVBIJUNG_AFT  = dt.Rows[0]["SVBIJUNG"].ToString(); // 비중
                sSVVCF_AFT     = dt.Rows[0]["SVVCF"].ToString();    // VCF
                sSVTEMP_AFT    = dt.Rows[0]["SVTEMP"].ToString();   // 입고온도
                sSVTEMPGB_AFT  = dt.Rows[0]["SVTEMPGB"].ToString(); // 입고온도구분
                sSVMTQTY_AFT   = dt.Rows[0]["SVMTQTY"].ToString();  // 입고MT량
                sSVKLQTY_AFT   = dt.Rows[0]["SVKLQTY"].ToString();  // 입고KL량
                sOLDMVOQTY_AFT = fsMVMOQTY.ToString();              // 이후 이고량

                if (double.Parse(Get_Numeric(sSVMTQTY_AFT.ToString())) == double.Parse(Get_Numeric(sOLDMVOQTY_AFT.ToString())))
                {
                    sSURVEY_GUBUN = "DEL";
                }
                else
                {
                    sSURVEY_GUBUN = "UPT";
                }
                
                sMVOQTY_AFT =
                            (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AFT.ToString())))
                            * (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sOLDMVOQTY_AFT.ToString())))
                            / double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AFT.ToString()))))).ToString("0.0000000");
                

                dMVOQTY_AFT  = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sMVOQTY_AFT.ToString())));
                dSVKLQTY_AFT = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY_AFT.ToString()))) - dMVOQTY_AFT;
                dSVMTQTY_AFT = double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY_AFT.ToString()))) - double.Parse(String.Format("{0,9:N3}", Get_Numeric(sOLDMVOQTY_AFT.ToString())));
               
            }

            sSVKLQTY_AFT = Convert.ToString(Math.Round(dSVKLQTY_AFT, 3));

            #endregion



            #region Description : DB에 저장

            this.DbConnector.CommandClear();

            // 입고화물관리 수정
            this.DbConnector.Attach("TY_P_UT_6CDEU103",
                                    sCMTANO1.ToString(),
                                    sCMTANO2.ToString(),
                                    sCMTANO3.ToString(),
                                    sCMTANO4.ToString(),
                                    sCMTANO5.ToString(),
                                    sCMTANO6.ToString(),
                                    sCMTANO7.ToString(),
                                    sCMTANO8.ToString(),
                                    sCMTANO9.ToString(),
                                    sCMTANO10.ToString(),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper()
                                    );

            // 탱크제원 수정
            if (fsMVAUTOGB.ToString() == "Y")
            {
                this.DbConnector.Attach("TY_P_UT_6CDGQ106",
                                        sTTHWAMUL.ToString(),
                                        sTTHMGB.ToString(),
                                        Get_Numeric(sTTBIJUNG.ToString()),
                                        Get_Numeric(sTTVCF.ToString()),
                                        Get_Numeric(sTTTEMP.ToString()),
                                        sTTTEMPGB.ToString(),
                                        sTTKESAN.ToString(),
                                        sTTIPHANG.ToString(),
                                        sTTBONSUN.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_7C8A0218",
                                        Get_Numeric(sTTTEMP.ToString()),
                                        sTTTEMPGB.ToString(),
                                        sTTKESAN.ToString(),
                                        sTTIPHANG.ToString(),
                                        sTTBONSUN.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                                        );
            }


            // 이고관리 삭제
            this.DbConnector.Attach("TY_P_UT_6CDH0108",
                                    this.TXT01_MVTANKNO.GetValue().ToString().Trim(),
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                    this.TXT01_MVMVTANK.GetValue().ToString().Trim(),
                                    Get_Date(this.DTP01_MVMVIL.GetValue().ToString())
                                    );

            // 삭제시 SURVEY 이전탱크 업데이트
            this.DbConnector.Attach("TY_P_UT_815AL396",
                                    "G",
                                    Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString()),
                                    sSVKLQTY_AGO.ToString(),
                                    Get_Numeric(sOLDMVOQTY_AGO.ToString()),
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                    this.TXT01_MVTANKNO.GetValue().ToString().Trim()
                                    );


            // SURVEY 이후탱크 업데이트
            if (sSURVEY_GUBUN == "UPT")
            {
                // 삭제시 SURVEY 이고탱크 업데이트
                this.DbConnector.Attach("TY_P_UT_815AN397",
                                        "T",
                                        Set_TankNo(this.TXT01_MVTANKNO.GetValue().ToString()),
                                        sSVKLQTY_AFT.ToString(),
                                        Get_Numeric(sOLDMVOQTY_AGO.ToString()),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                        this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                        this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                        this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                        this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                                        );
            }
            else
            {
                // SURVEY 이고탱크 삭제
                this.DbConnector.Attach("TY_P_UT_6CEFH118",
                                        Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                                        this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                                        this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                                        this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                                        this.TXT01_MVMVTANK.GetValue().ToString().Trim()
                                        );
            }

            this.DbConnector.ExecuteTranQueryList();

            #endregion
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsGUBUN.ToString() == "NEW")
            {
                // 동일 자료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CDDA094",
                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_MVBONSUN.GetValue().ToString(),           // 본선
                    this.CBH01_MVHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_MVHWAMUL.GetValue().ToString(),           // 화물
                    this.TXT01_MVTANKNO.GetValue().ToString().Trim(),    // 탱크번호
                    Get_Date(this.DTP01_MVMVIL.GetValue().ToString()),   // 이고일자
                    this.TXT01_MVMVTANK.GetValue().ToString().Trim()     // 이고탱크번호
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.TXT01_MVMVTANK.Focus();

                    e.Successed = false;
                    return;
                }

                // 이고관리 등록시 이고일자 이후 자료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_7B88K957",
                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_MVBONSUN.GetValue().ToString(),           // 본선
                    this.CBH01_MVHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_MVHWAMUL.GetValue().ToString(),           // 화물
                    this.TXT01_MVTANKNO.GetValue().ToString().Trim(),    // 탱크번호
                    Get_Date(this.DTP01_MVMVIL.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B88L958");
                    this.TXT01_MVMVTANK.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (fsGUBUN == "UPT")
            {
                // 이고 탱크 존재 유무
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CFA0123",
                    Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                    this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                    this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                    this.TXT01_MVMVTANK.GetValue().ToString().Trim(),
                    Get_Date(this.DTP01_MVMVIL.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_6CFA1124");
                    SetFocus(this.TXT01_MVMOQTY);

                    e.Successed = false;
                    return;
                }
            }

            // 탱크번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66SDH426", this.TXT01_MVTANKNO.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_676GD601");
                SetFocus(this.TXT01_MVTANKNO);

                e.Successed = false;
                return;
            }

            // 이고탱크번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66SDH426", this.TXT01_MVMVTANK.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_676GD601");
                SetFocus(this.TXT01_MVMVTANK);

                e.Successed = false;
                return;
            }

            if (int.Parse(Get_Date(this.DTP01_MVMVIL.GetValue().ToString())) < int.Parse(Get_Date(this.DTP01_MVIPHANG.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_77DE9158");
                SetFocus(this.DTP01_MVMVIL);

                e.Successed = false;
                return;
            }

            if (Set_TankNo(this.TXT01_MVTANKNO.GetValue().ToString()) == Set_TankNo(this.TXT01_MVMVTANK.GetValue().ToString()))
            {
                this.ShowMessage("TY_M_UT_6CDDL095");
                SetFocus(this.TXT01_MVMVTANK);

                e.Successed = false;
                return;
            }

            // 입고화물관리 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDEM101",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKBY439");
                SetFocus(this.TXT01_MVMVTANK);

                e.Successed = false;
                return;
            }

            // SURVEY파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CDHN109",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_MVTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCN443");
                SetFocus(this.TXT01_MVMVTANK);

                e.Successed = false;
                return;
            }

            // 이고량 > 재고량 + 이전이고량
            if (double.Parse(Get_Numeric(this.TXT01_MVMOQTY.GetValue().ToString())) > double.Parse((   double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_MVJEQTY.GetValue().ToString())))
                                                                                                     + double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMVMOQTY.ToString())))).ToString("0.000")))
            {
                this.ShowMessage("TY_M_UT_6CDDP098");
                SetFocus(this.TXT01_MVMOQTY);

                e.Successed = false;
                return;
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 이고 탱크 존재 유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CFA0123",
                Get_Date(this.DTP01_MVIPHANG.GetValue().ToString()),
                this.CBH01_MVBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_MVHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_MVMVTANK.GetValue().ToString().Trim(),
                Get_Date(this.DTP01_MVMVIL.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_6CFA1124");
                SetFocus(this.TXT01_MVMOQTY);

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

        #region Description : 필드 클리어
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN != "DEL")
            {
                this.DTP01_MVIPHANG.SetValue("");  // 입항일자
                this.CBH01_MVBONSUN.SetValue("");  // 본선
                this.CBH01_MVHWAJU.SetValue("");   // 화주
                this.CBH01_MVHWAMUL.SetValue("");  // 화물
                this.TXT01_MVTANKNO.SetValue("");  // 탱크번호
                this.DTP01_MVMVIL.SetValue("");    // 이고일자
                this.TXT01_MVMVTANK.SetValue("");  // 이고탱크
                this.CBO01_MVAUTOGB.SetValue("Y"); // TANK 모니터링 적용
            }

            this.TXT01_MVIPQTY.SetValue("");  // 입고량
            this.TXT01_MVJEQTY.SetValue("");  // 재고량
            this.TXT01_MVMOQTY.SetValue("");  // 이고량
        }
        #endregion

        #region Description : 버튼 Visible
        private void UP_BUTTON_Visible(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;

                this.BTN61_CHTANK.Visible = true;
            }
            else if (sGUBUN.ToString() == "SAV" || sGUBUN.ToString() == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                this.BTN61_CHTANK.Visible = false;
            }
            else if(sGUBUN.ToString() == "")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN61_CHTANK.Visible = false;
            }
        }
        #endregion

        #region Description : TEXTBOX - ReadOnly
        private void UP_TXTBOX_ReadOnly(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.TXT01_MVTANKNO.SetReadOnly(false);
                this.DTP01_MVMVIL.SetReadOnly(false);

                this.TXT01_MVMVTANK.SetReadOnly(false);
            }
            else if (sGUBUN.ToString() == "SAV" || sGUBUN.ToString() == "UPT")
            {
                this.TXT01_MVTANKNO.SetReadOnly(true);
                this.DTP01_MVMVIL.SetReadOnly(true);

                this.TXT01_MVMVTANK.SetReadOnly(true);
            }
            else if (sGUBUN.ToString() == "")
            {
                this.TXT01_MVTANKNO.SetReadOnly(true);
                this.DTP01_MVMVIL.SetReadOnly(true);

                this.TXT01_MVMVTANK.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 출고탱크 입고량 가져오기
        private void UP_GET_SVMTQTY()
        {
            //this.TXT01_MVCHTANK.SetValue("");

            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_6BTGX906",
            //    this.DTP01_MVIPHANG.GetValue().ToString(), // 입항일자
            //    this.CBH01_MVBONSUN.GetValue().ToString(), // 본선
            //    this.CBH01_MVHWAJU.GetValue().ToString(),  // 화주
            //    this.CBH01_MVHWAMUL.GetValue().ToString(), // 화물
            //    this.TXT01_MVCHTANK.GetValue().ToString()  // 출고탱크
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    fsSVMTQTY = dt.Rows[0]["SVMTQTY"].ToString();
            //}
        }
        #endregion

        #region Description : 포장수량 텍스트박스 이벤트
        private void TXT01_MVDRQTY_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : 출고탱크 이벤트
        private void BTN61_CHTANK_Click(object sender, EventArgs e)
        {
            TYUTGB016S popup = new TYUTGB016S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_MVIPHANG.SetValue(popup.fsIPHANG);
                this.CBH01_MVBONSUN.SetValue(popup.fsBONSUN);
                this.CBH01_MVHWAJU.SetValue(popup.fsHWAJU);
                this.CBH01_MVHWAMUL.SetValue(popup.fsHWAMUL);

                this.TXT01_MVTANKNO.SetValue(popup.fsTANKNO); // 출고탱크
                this.TXT01_MVIPQTY.SetValue(popup.fsSVMTQTY); // 입 고 량
                this.TXT01_MVJEQTY.SetValue(popup.fsSVJGQTY); // 재 고 량

                SetFocus(this.DTP01_MVMVIL);
            }
        }

        private void TXT01_MVTANKNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB016S popup = new TYUTGB016S();

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.DTP01_MVIPHANG.SetValue(popup.fsIPHANG);
                    this.CBH01_MVBONSUN.SetValue(popup.fsBONSUN);
                    this.CBH01_MVHWAJU.SetValue(popup.fsHWAJU);
                    this.CBH01_MVHWAMUL.SetValue(popup.fsHWAMUL);

                    this.TXT01_MVTANKNO.SetValue(popup.fsTANKNO); // 출고탱크
                    this.TXT01_MVIPQTY.SetValue(popup.fsSVMTQTY); // 입 고 량
                    this.TXT01_MVJEQTY.SetValue(popup.fsSVJGQTY); // 재 고 량

                    SetFocus(this.DTP01_MVMVIL);
                }
            }
        }            
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_6CDBM091_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_MVIPHANG.SetValue(this.FPS91_TY_S_UT_6CDBM091.GetValue("MVIPHANG1").ToString());    // 입항일자
            this.CBH01_MVBONSUN.SetValue(this.FPS91_TY_S_UT_6CDBM091.GetValue("MVBONSUN1").ToString());    // 본선
            this.CBH01_MVHWAJU.SetValue(this.FPS91_TY_S_UT_6CDBM091.GetValue("MVHWAJU1").ToString());      // 화주
            this.CBH01_MVHWAMUL.SetValue(this.FPS91_TY_S_UT_6CDBM091.GetValue("MVHWAMUL1").ToString());    // 화물
            this.TXT01_MVTANKNO.SetValue(this.FPS91_TY_S_UT_6CDBM091.GetValue("MVTANKNO").ToString());     // 탱크번호
            this.DTP01_MVMVIL.SetValue(this.FPS91_TY_S_UT_6CDBM091.GetValue("MVMVIL").ToString());         // 이고일자
            this.TXT01_MVMVTANK.SetValue(this.FPS91_TY_S_UT_6CDBM091.GetValue("MVMVTANK").ToString());     // 이고탱크번호
            

            // 확인
            UP_RUN();
        }
        #endregion

        #region Description : 탱크번호 이벤트
        private void TXT01_GATANK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion

        #region Description : TANK 모니터링 적용 이벤트
        private void CBO01_MVAUTOGB_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}