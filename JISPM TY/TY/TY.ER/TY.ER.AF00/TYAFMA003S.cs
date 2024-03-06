using System;
using System.Data;
using System.Collections.Generic;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 품목별매출현황 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37A2X065 : EIS 품목별매출현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37A35071 : EIS 품목별매출현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ERSCDDP : 사업부
    ///  ERSYYMM : 년월
    /// </summary>
    public partial class TYAFMA003S : TYBase
    {
        private string fsCompanyCode;

        #region Description : 폼 로드 이벤트
        public TYAFMA003S()
        {
            InitializeComponent();
        }

        private void TYAFMA003S_Load(object sender, System.EventArgs e)
        {
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESCSUBGN.SetValue(fsCompanyCode);
                this.CBH01_ESCSUBGN.SetReadOnly(true);
            }

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG" ||
                this.CBH01_ESCSUBGN.GetValue().ToString() == "TH" ||
                this.CBH01_ESCSUBGN.GetValue().ToString() == "TS")
            {
                // 버튼 숨김
                this.BTN62_INQ.Visible = false;
                this.BTN62_NEW.Visible = false;
                this.BTN62_REM.Visible = false;

                this.BTN63_INQ.Visible = false;
            }

            //this.DTP01_ESCYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            //this.DTP01_ESCYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            UP_start_dsp_month();

            tabControl_Enable();

            UP_Spread_Title(this.DTP01_ESCYYMM.GetValue().ToString());

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.DTP01_ESCYYMM);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESCSUBGN.CodeText);
            }
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sEDCYYMM_TWO = string.Empty;
            string sEDCYYMM_ONE = string.Empty;

            string sSTEDCYYMM = string.Empty;
            string sEDEDCYYMM = string.Empty;

            sEDCYYMM_TWO = Convert.ToString(int.Parse(this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 4)) - 2);
            sEDCYYMM_ONE = Convert.ToString(int.Parse(this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 4)) - 1);

            sSTEDCYYMM = this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 4) + "01";
            sEDEDCYYMM = this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 6);

            this.FPS91_TY_S_AC_39BBA656.Initialize();
            this.FPS91_TY_S_AC_39C2D697.Initialize();
            this.FPS91_TY_S_AC_39G4V778.Initialize();

            DataTable dt = new DataTable();

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG")
            {
                string sYYMM = string.Empty;

                // 태영그레인DB
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C23S508",
                    this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 4)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sYYMM = dt.Rows[0]["ESCYYMM"].ToString();
                }
                else
                {
                    sYYMM = this.DTP01_ESCYYMM.GetValue().ToString();
                }

                UP_Spread_Title(sYYMM.ToString());
            }
            else
            {
                UP_Spread_Title(this.DTP01_ESCYYMM.GetValue().ToString());
            }

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG")
            {
                // 태영그레인DB
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39A4O642",
                    sEDCYYMM_TWO.ToString(), // 2년전
                    sEDCYYMM_ONE.ToString(), // 1년전
                    sSTEDCYYMM.ToString(),
                    sEDEDCYYMM.ToString(),
                    this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 6),
                    this.CBH01_ESCSUBGN.GetValue(),
                    this.CBH01_ESCSUBGN.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_39BBA656.SetValue(dt);

                double dBIYUL  = 0;
                double dBIYUL1 = 0;
                double dBIYUL2 = 0;

                for (int i = 0; i < this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_39BBA656.GetValue(i, "VNSANGHO").ToString() != "기타")
                    {
                        dBIYUL = double.Parse(string.Format("{0:###0.0}",  dBIYUL  + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL").ToString()))));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL_ONE").ToString()))));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL_TWO").ToString()))));

                        //dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL").ToString()))));
                        //dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL1").ToString()))));
                        //dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL2").ToString()))));
                    }

                    if (this.FPS91_TY_S_AC_39BBA656.GetValue(i, "VNSANGHO").ToString() == "기타")
                    {
                        //if (dBIYUL == 0)
                        //{
                        //    dBIYUL = 100;
                        //}

                        //if (dBIYUL1 == 0)
                        //{
                        //    dBIYUL1 = 100;
                        //}

                        //if (dBIYUL2 == 0)
                        //{
                        //    dBIYUL2 = 100;
                        //}

                        this.FPS91_TY_S_AC_39BBA656.SetValue(i, "BIYUL",     Convert.ToString(100 - dBIYUL));
                        this.FPS91_TY_S_AC_39BBA656.SetValue(i, "BIYUL_ONE", Convert.ToString(100 - dBIYUL1));
                        this.FPS91_TY_S_AC_39BBA656.SetValue(i, "BIYUL_TWO", Convert.ToString(100 - dBIYUL2));
                    }
                }

                if (this.FPS91_TY_S_AC_39BBA656.CurrentRowCount > 0)
                {
                    double dESCMAEAMT  = 0;
                    double dESCMAEAMT1 = 0;
                    double dESCMAEAMT2 = 0;

                    dBIYUL  = 0;
                    dBIYUL1 = 0;
                    dBIYUL2 = 0;

                    for (int i = 0; i < this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount; i++)
                    {
                        dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL").ToString())));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL_ONE").ToString())));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "BIYUL_TWO").ToString())));

                        dESCMAEAMT  = double.Parse(string.Format("{0:###0.0}", dESCMAEAMT  + double.Parse(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "ESCMAEAMT").ToString())));
                        dESCMAEAMT1 = double.Parse(string.Format("{0:###0.0}", dESCMAEAMT1 + double.Parse(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "ESCMAEAMT_ONE").ToString())));
                        dESCMAEAMT2 = double.Parse(string.Format("{0:###0.0}", dESCMAEAMT2 + double.Parse(this.FPS91_TY_S_AC_39BBA656.GetValue(i, "ESCMAEAMT_TWO").ToString())));
                    }

                    this.FPS91_TY_S_AC_39BBA656_Sheet1.AddRows(this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount, 1);

                    this.FPS91_TY_S_AC_39BBA656.ActiveSheet.Cells[this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1, 2].Text = "계";

                    this.FPS91_TY_S_AC_39BBA656.SetValue(this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1, "BIYUL",     Convert.ToString(dBIYUL));
                    this.FPS91_TY_S_AC_39BBA656.SetValue(this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1, "BIYUL_ONE", Convert.ToString(dBIYUL1));
                    this.FPS91_TY_S_AC_39BBA656.SetValue(this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1, "BIYUL_TWO", Convert.ToString(dBIYUL2));

                    this.FPS91_TY_S_AC_39BBA656.SetValue(this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1, "ESCMAEAMT",     Convert.ToString(dESCMAEAMT));
                    this.FPS91_TY_S_AC_39BBA656.SetValue(this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1, "ESCMAEAMT_ONE", Convert.ToString(dESCMAEAMT1));
                    this.FPS91_TY_S_AC_39BBA656.SetValue(this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1, "ESCMAEAMT_TWO", Convert.ToString(dESCMAEAMT2));

                    this.FPS91_TY_S_AC_39BBA656.ActiveSheet.Rows[this.FPS91_TY_S_AC_39BBA656.ActiveSheet.RowCount - 1].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TH") // 호라이즌
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39C2D696",
                    sSTEDCYYMM.ToString(),
                    sEDEDCYYMM.ToString(),
                    sEDCYYMM_ONE.ToString(), // 1년전
                    sEDCYYMM_TWO.ToString()  // 2년전
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_39C2D697.SetValue(dt);

                double dBIYUL  = 0;
                double dBIYUL1 = 0;
                double dBIYUL2 = 0;

                for (int i = 0; i < this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_39C2D697.GetValue(i, "VNSANGHO").ToString() != "기타")
                    {
                        dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL").ToString()))));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL1").ToString()))));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL2").ToString()))));

                        //dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL").ToString()))));
                        //dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL1").ToString()))));
                        //dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL2").ToString()))));
                    }

                    if (this.FPS91_TY_S_AC_39C2D697.GetValue(i, "VNSANGHO").ToString() == "기타")
                    {
                        //if (dBIYUL == 0)
                        //{
                        //    dBIYUL = 100;
                        //}

                        //if (dBIYUL1 == 0)
                        //{
                        //    dBIYUL1 = 100;
                        //}

                        //if (dBIYUL2 == 0)
                        //{
                        //    dBIYUL2 = 100;
                        //}

                        this.FPS91_TY_S_AC_39C2D697.SetValue(i, "BIYUL",  Convert.ToString(100 - dBIYUL));
                        this.FPS91_TY_S_AC_39C2D697.SetValue(i, "BIYUL1", Convert.ToString(100 - dBIYUL1));
                        this.FPS91_TY_S_AC_39C2D697.SetValue(i, "BIYUL2", Convert.ToString(100 - dBIYUL2));
                    }
                }

                if (this.FPS91_TY_S_AC_39C2D697.CurrentRowCount > 0)
                {
                    double dEDSMAEAMT  = 0;
                    double dEDSMAEAMT1 = 0;
                    double dEDSMAEAMT2 = 0;

                    dBIYUL  = 0;
                    dBIYUL1 = 0;
                    dBIYUL2 = 0;

                    for (int i = 0; i < this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount; i++)
                    {
                        dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL").ToString())));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL1").ToString())));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "BIYUL2").ToString())));

                        dEDSMAEAMT  = double.Parse(string.Format("{0:###0.0}", dEDSMAEAMT  + double.Parse(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "EDSMAEAMT").ToString())));
                        dEDSMAEAMT1 = double.Parse(string.Format("{0:###0.0}", dEDSMAEAMT1 + double.Parse(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "EDSMAEAMT1").ToString())));
                        dEDSMAEAMT2 = double.Parse(string.Format("{0:###0.0}", dEDSMAEAMT2 + double.Parse(this.FPS91_TY_S_AC_39C2D697.GetValue(i, "EDSMAEAMT2").ToString())));
                    }

                    this.FPS91_TY_S_AC_39C2D697_Sheet1.AddRows(this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount, 1);

                    this.FPS91_TY_S_AC_39C2D697.ActiveSheet.Cells[this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1, 1].Text = "계";

                    this.FPS91_TY_S_AC_39C2D697.SetValue(this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1, "BIYUL",  Convert.ToString(dBIYUL));
                    this.FPS91_TY_S_AC_39C2D697.SetValue(this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1, "BIYUL1", Convert.ToString(dBIYUL1));
                    this.FPS91_TY_S_AC_39C2D697.SetValue(this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1, "BIYUL2", Convert.ToString(dBIYUL2));

                    this.FPS91_TY_S_AC_39C2D697.SetValue(this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1, "EDSMAEAMT",  Convert.ToString(dEDSMAEAMT));
                    this.FPS91_TY_S_AC_39C2D697.SetValue(this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1, "EDSMAEAMT1", Convert.ToString(dEDSMAEAMT1));
                    this.FPS91_TY_S_AC_39C2D697.SetValue(this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1, "EDSMAEAMT2", Convert.ToString(dEDSMAEAMT2));

                    this.FPS91_TY_S_AC_39C2D697.ActiveSheet.Rows[this.FPS91_TY_S_AC_39C2D697.ActiveSheet.RowCount - 1].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TS") // GLS
            {
                string s전년_시작 = string.Empty;
                string s전년_종료 = string.Empty;
                string s전전년_시작 = string.Empty;
                string s전전년_종료 = string.Empty;
                string sEDMONTH = string.Empty;

                sEDMONTH = this.DTP01_ESCYYMM.GetString().ToString().Substring(4, 2);
                s전년_시작 = sEDCYYMM_ONE + "01";
                s전년_종료 = sEDCYYMM_ONE + sEDMONTH;
                s전전년_시작 = sEDCYYMM_TWO + "01";
                s전전년_종료 = sEDCYYMM_TWO + sEDMONTH;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39G4U775",
                    sSTEDCYYMM.ToString(), sEDEDCYYMM.ToString(), // 해당년
                    s전년_시작.ToString(), s전년_종료.ToString(), // 1년전
                    s전전년_시작.ToString(), s전전년_종료.ToString()  // 2년전
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_39G4V778.SetValue(dt);

                double dBIYUL  = 0;
                double dBIYUL1 = 0;
                double dBIYUL2 = 0;

                for (int i = 0; i < this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_39G4V778.GetValue(i, "EDDESC").ToString() != "기타")
                    {
                        dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL").ToString()))));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL1").ToString()))));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL2").ToString()))));

                        //dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL").ToString()))));
                        //dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL1").ToString()))));
                        //dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL2").ToString()))));
                    }

                    if (this.FPS91_TY_S_AC_39G4V778.GetValue(i, "EDDESC").ToString() == "기타")
                    {
                        //if (dBIYUL == 0)
                        //{
                        //    dBIYUL = 100;
                        //}

                        //if (dBIYUL1 == 0)
                        //{
                        //    dBIYUL1 = 100;
                        //}

                        //if (dBIYUL2 == 0)
                        //{
                        //    dBIYUL2 = 100;
                        //}

                        this.FPS91_TY_S_AC_39G4V778.SetValue(i, "BIYUL",  Convert.ToString(100 - dBIYUL));
                        this.FPS91_TY_S_AC_39G4V778.SetValue(i, "BIYUL1", Convert.ToString(100 - dBIYUL1));
                        this.FPS91_TY_S_AC_39G4V778.SetValue(i, "BIYUL2", Convert.ToString(100 - dBIYUL2));
                    }
                }

                if (this.FPS91_TY_S_AC_39G4V778.CurrentRowCount > 0)
                {
                    double dAMT  = 0;
                    double dAMT1 = 0;
                    double dAMT2 = 0;

                    dBIYUL  = 0;
                    dBIYUL1 = 0;
                    dBIYUL2 = 0;

                    for (int i = 0; i < this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount; i++)
                    {
                        dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL").ToString())));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL1").ToString())));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "BIYUL2").ToString())));

                        dAMT  = double.Parse(string.Format("{0:###0.0}", dAMT  + double.Parse(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "AMT").ToString())));
                        dAMT1 = double.Parse(string.Format("{0:###0.0}", dAMT1 + double.Parse(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "AMT1").ToString())));
                        dAMT2 = double.Parse(string.Format("{0:###0.0}", dAMT2 + double.Parse(this.FPS91_TY_S_AC_39G4V778.GetValue(i, "AMT2").ToString())));
                    }

                    this.FPS91_TY_S_AC_39G4V778_Sheet1.AddRows(this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount, 1);

                    this.FPS91_TY_S_AC_39G4V778.ActiveSheet.Cells[this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1, 1].Text = "계";

                    this.FPS91_TY_S_AC_39G4V778.SetValue(this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1, "BIYUL",  Convert.ToString(dBIYUL));
                    this.FPS91_TY_S_AC_39G4V778.SetValue(this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1, "BIYUL1", Convert.ToString(dBIYUL1));
                    this.FPS91_TY_S_AC_39G4V778.SetValue(this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1, "BIYUL2", Convert.ToString(dBIYUL2));

                    this.FPS91_TY_S_AC_39G4V778.SetValue(this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1, "AMT",  Convert.ToString(dAMT));
                    this.FPS91_TY_S_AC_39G4V778.SetValue(this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1, "AMT1", Convert.ToString(dAMT1));
                    this.FPS91_TY_S_AC_39G4V778.SetValue(this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1, "AMT2", Convert.ToString(dAMT2));

                    this.FPS91_TY_S_AC_39G4V778.ActiveSheet.Rows[this.FPS91_TY_S_AC_39G4V778.ActiveSheet.RowCount - 1].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

            this.DTP01_ESCYYMM.Focus();
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title(string sDATE)
        {
            string sTwo_Years_Ago = string.Empty;
            string sYears_Ago = string.Empty;
            string sNow_Date = string.Empty;

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG")
            {
                sTwo_Years_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 2) + "년";
                sYears_Ago     = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "년";
                sNow_Date      = sDATE.Substring(0, 4) + "년" + sDATE.Substring(4, 2) + "월 누계";

                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeaderRowCount = 3;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(0, 0, 3, 1);
                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(0, 1, 3, 1);
                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(0, 2, 3, 1);
                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(0, 3, 3, 1);
                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 6);

                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(1, 4, 1, 2);
                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(1, 6, 1, 2);
                this.FPS91_TY_S_AC_39BBA656_Sheet1.AddColumnHeaderSpanCell(1, 8, 1, 2);

                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[0, 0].Value = "기준년월";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[0, 1].Value = "거 래 처";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[0, 2].Value = "거래처명";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[0, 3].Value = "주요취급화물";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[0, 4].Value = "매 출 액";

                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[1, 4].Value = sNow_Date;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[1, 6].Value = sYears_Ago;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[1, 8].Value = sTwo_Years_Ago;

                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 4].Value = "금액";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 5].Value = "비율";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 6].Value = "금액";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 7].Value = "비율";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 8].Value = "금액";
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 9].Value = "비율";

                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39BBA656_Sheet1.ColumnHeader.Cells[2, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                for (int i = 0; i < this.FPS91_TY_S_AC_39BBA656_Sheet1.RowCount; i++)
                {
                    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                    GeneralCellType tmpCellType = new GeneralCellType();
                    tmpCellType.FormatString = "#,###";

                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 4].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 5].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 6].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 7].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 8].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 9].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39BBA656_Sheet1.Cells[i, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TH")
            {
                sTwo_Years_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 2) + "년";
                sYears_Ago     = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "년";
                sNow_Date      = sDATE.Substring(0, 4) + "년" + sDATE.Substring(4, 2) + "월 누계";

                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeaderRowCount = 2;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_AC_39C2D697_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 4);
                this.FPS91_TY_S_AC_39C2D697_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 4);
                this.FPS91_TY_S_AC_39C2D697_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 4);
                
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[0, 0].Value = sNow_Date;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[0, 4].Value = sYears_Ago;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[0, 8].Value = sTwo_Years_Ago;

                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 0].Value = "거래처";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 1].Value = "거래처명";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 2].Value = "금액";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 3].Value = "비율";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 4].Value = "거래처";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 5].Value = "거래처명";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 6].Value = "금액";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 7].Value = "비율";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 8].Value = "거래처";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 9].Value = "거래처명";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 10].Value = "금액";
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 11].Value = "비율";

                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39C2D697_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                for (int i = 0; i < this.FPS91_TY_S_AC_39C2D697_Sheet1.RowCount; i++)
                {
                    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                    GeneralCellType tmpCellType = new GeneralCellType();
                    tmpCellType.FormatString = "#,###";

                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 2].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 3].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 6].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 7].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 10].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 11].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39C2D697_Sheet1.Cells[i, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TS")
            {
                sTwo_Years_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 2) + "년 " + sDATE.Substring(4, 2) + "월 누계";
                sYears_Ago     = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "년 " + sDATE.Substring(4, 2) + "월 누계";
                sNow_Date      = sDATE.Substring(0, 4) + "년 " + sDATE.Substring(4, 2) + "월 누계";

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeaderRowCount = 3;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_AC_39G4V778_Sheet1.AddColumnHeaderSpanCell(0, 0, 3, 1);
                this.FPS91_TY_S_AC_39G4V778_Sheet1.AddColumnHeaderSpanCell(0, 1, 3, 1);
                this.FPS91_TY_S_AC_39G4V778_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 6);

                this.FPS91_TY_S_AC_39G4V778_Sheet1.AddColumnHeaderSpanCell(1, 2, 1, 2);
                this.FPS91_TY_S_AC_39G4V778_Sheet1.AddColumnHeaderSpanCell(1, 4, 1, 2);
                this.FPS91_TY_S_AC_39G4V778_Sheet1.AddColumnHeaderSpanCell(1, 6, 1, 2);

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[0, 0].Value = "주요품목";
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[0, 1].Value = "주요품목명";

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[0, 2].Value = "매 출 액";

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[1, 2].Value = sNow_Date;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[1, 4].Value = sYears_Ago;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[1, 6].Value = sTwo_Years_Ago;

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 2].Value = "금액";
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 3].Value = "비율";
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 4].Value = "금액";
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 5].Value = "비율";
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 6].Value = "금액";
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 7].Value = "비율";

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39G4V778_Sheet1.ColumnHeader.Cells[2, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                for (int i = 0; i < this.FPS91_TY_S_AC_39G4V778_Sheet1.RowCount; i++)
                {
                    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                    GeneralCellType tmpCellType = new GeneralCellType();
                    tmpCellType.FormatString = "#,###";

                    this.FPS91_TY_S_AC_39G4V778_Sheet1.Cells[i, 2].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39G4V778_Sheet1.Cells[i, 4].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39G4V778_Sheet1.Cells[i, 6].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_39G4V778_Sheet1.Cells[i, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39G4V778_Sheet1.Cells[i, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39G4V778_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TL")
            {
                sTwo_Years_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 2) + "년 누계";
                sYears_Ago     = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "년 누계";
                sNow_Date      = sDATE.Substring(0, 4) + "년 " + sDATE.Substring(4, 2) + "월 누계";

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeaderRowCount = 3;
                this.FPS91_TY_S_AC_39N29826_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_AC_39N29826_Sheet1.AddColumnHeaderSpanCell(0, 0, 3, 1);
                this.FPS91_TY_S_AC_39N29826_Sheet1.AddColumnHeaderSpanCell(0, 1, 3, 1);
                this.FPS91_TY_S_AC_39N29826_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 6);

                this.FPS91_TY_S_AC_39N29826_Sheet1.AddColumnHeaderSpanCell(1, 2, 1, 2);
                this.FPS91_TY_S_AC_39N29826_Sheet1.AddColumnHeaderSpanCell(1, 4, 1, 2);
                this.FPS91_TY_S_AC_39N29826_Sheet1.AddColumnHeaderSpanCell(1, 6, 1, 2);

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[0, 0].Value = "주요품목";
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[0, 1].Value = "주요품목명";

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[0, 2].Value = "매 출 액";

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[1, 2].Value = sNow_Date;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[1, 4].Value = sYears_Ago;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[1, 6].Value = sTwo_Years_Ago;

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 2].Value = "금액";
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 3].Value = "비율";
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 4].Value = "금액";
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 5].Value = "비율";
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 6].Value = "금액";
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 7].Value = "비율";

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_39N29826_Sheet1.ColumnHeader.Cells[2, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                for (int i = 0; i < this.FPS91_TY_S_AC_39N29826_Sheet1.RowCount; i++)
                {
                    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                    GeneralCellType tmpCellType = new GeneralCellType();
                    tmpCellType.FormatString = "#,###";

                    this.FPS91_TY_S_AC_39N29826_Sheet1.Cells[i, 2].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39N29826_Sheet1.Cells[i, 4].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_39N29826_Sheet1.Cells[i, 6].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_39N29826_Sheet1.Cells[i, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39N29826_Sheet1.Cells[i, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_39N29826_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
            }
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYAFMA003B(this.CBH01_ESCSUBGN.GetValue().ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 계열사 주요매출처관리 조회 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_39BBZ657.Initialize();

            DataTable dt = new DataTable();

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG") // 그레인
            {
                // 태영그레인DB
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39A4A640",
                    this.DTP01_ESCYYMM.GetValue().ToString(),
                    this.CBH01_ESCSUBGN.GetValue().ToString(),
                    this.CBH01_ESCVEND.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_39BBZ657.SetValue(dt);
            }

            this.DTP01_ESCYYMM.Focus();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG") // 그레인
            {
                if ((new TYAFMA003I(this.DTP01_ESCYYMM.GetValue().ToString(), this.CBH01_ESCVEND.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN62_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG") // 그레인
            {
                // 삭제
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 태영
                        this.DbConnector.Attach("TY_P_AC_39A50644", this.CBH01_ESCSUBGN.GetValue().ToString(),
                                                                    ds.Tables[0].Rows[i]["ESCVEND"].ToString(),
                                                                    this.DTP01_ESCYYMM.GetValue().ToString()
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();

                    this.DbConnector.CommandClear();
                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 태영그레인
                        this.DbConnector.Attach("TY_P_AC_39A52646", this.CBH01_ESCSUBGN.GetValue().ToString(),
                                                                    ds.Tables[0].Rows[i]["ESCVEND"].ToString(),
                                                                    this.DTP01_ESCYYMM.GetValue().ToString()
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region Description : 티와이스틸 조회 버튼
        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            string sEDCYYMM_TWO = string.Empty;
            string sEDCYYMM_ONE = string.Empty;

            string sSTEDCYYMM = string.Empty;
            string sEDEDCYYMM = string.Empty;

            string s전년_시작 = string.Empty;
            string s전년_종료 = string.Empty;
            string s전전년_시작 = string.Empty;
            string s전전년_종료 = string.Empty;
            string sEDMONTH = string.Empty;


            DataTable dt = new DataTable();

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TL") // 티와이스틸
            {
                sEDCYYMM_TWO = Convert.ToString(int.Parse(this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 4)) - 2);
                sEDCYYMM_ONE = Convert.ToString(int.Parse(this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 4)) - 1);

                sSTEDCYYMM = this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 4) + "01";
                sEDEDCYYMM = this.DTP01_ESCYYMM.GetString().ToString().Substring(0, 6);

                sEDMONTH = this.DTP01_ESCYYMM.GetString().ToString().Substring(4, 2);

                UP_Spread_Title(this.DTP01_ESCYYMM.GetValue().ToString());

                s전년_시작 = sEDCYYMM_ONE + "01";
                s전년_종료 = sEDCYYMM_ONE + "12";

                s전전년_시작 = sEDCYYMM_TWO + "01";
                s전전년_종료 = sEDCYYMM_TWO + "12";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39N28825",
                    sSTEDCYYMM.ToString(),sEDEDCYYMM.ToString(), // 해당년
                    s전년_시작.ToString(),s전년_종료.ToString(), // 1년전
                    s전전년_시작.ToString(), s전전년_종료.ToString()  // 2년전
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_39N29826.SetValue(dt);

                double dBIYUL  = 0;
                double dBIYUL1 = 0;
                double dBIYUL2 = 0;

                for (int i = 0; i < this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_39N29826.GetValue(i, "EDDESC").ToString() != "기타")
                    {
                        dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL").ToString()))));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL1").ToString()))));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(Get_Numeric(this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL2").ToString()))));

                        //dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL").ToString()))));
                        //dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL1").ToString()))));
                        //dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(string.Format("{0:###0.0}", this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL2").ToString()))));
                    }

                    if (this.FPS91_TY_S_AC_39N29826.GetValue(i, "EDDESC").ToString() == "기타")
                    {
                        //if (dBIYUL == 0)
                        //{
                        //    dBIYUL = 100;
                        //}

                        //if (dBIYUL1 == 0)
                        //{
                        //    dBIYUL1 = 100;
                        //}

                        //if (dBIYUL2 == 0)
                        //{
                        //    dBIYUL2 = 100;
                        //}

                        this.FPS91_TY_S_AC_39N29826.SetValue(i, "BIYUL",  Convert.ToString(100 - dBIYUL));
                        this.FPS91_TY_S_AC_39N29826.SetValue(i, "BIYUL1", Convert.ToString(100 - dBIYUL1));
                        this.FPS91_TY_S_AC_39N29826.SetValue(i, "BIYUL2", Convert.ToString(100 - dBIYUL2));
                    }
                }

                if (this.FPS91_TY_S_AC_39N29826.CurrentRowCount > 0)
                {
                    double dAMT  = 0;
                    double dAMT1 = 0;
                    double dAMT2 = 0;

                    dBIYUL  = 0;
                    dBIYUL1 = 0;
                    dBIYUL2 = 0;

                    for (int i = 0; i < this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount; i++)
                    {
                        dBIYUL  = double.Parse(string.Format("{0:###0.0}", dBIYUL  + double.Parse(this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL").ToString())));
                        dBIYUL1 = double.Parse(string.Format("{0:###0.0}", dBIYUL1 + double.Parse(this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL1").ToString())));
                        dBIYUL2 = double.Parse(string.Format("{0:###0.0}", dBIYUL2 + double.Parse(this.FPS91_TY_S_AC_39N29826.GetValue(i, "BIYUL2").ToString())));

                        dAMT  = double.Parse(string.Format("{0:###0.0}", dAMT  + double.Parse(this.FPS91_TY_S_AC_39N29826.GetValue(i, "AMT").ToString())));
                        dAMT1 = double.Parse(string.Format("{0:###0.0}", dAMT1 + double.Parse(this.FPS91_TY_S_AC_39N29826.GetValue(i, "AMT1").ToString())));
                        dAMT2 = double.Parse(string.Format("{0:###0.0}", dAMT2 + double.Parse(this.FPS91_TY_S_AC_39N29826.GetValue(i, "AMT2").ToString())));
                    }

                    this.FPS91_TY_S_AC_39N29826_Sheet1.AddRows(this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount, 1);

                    this.FPS91_TY_S_AC_39N29826.ActiveSheet.Cells[this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1, 1].Text = "계";

                    this.FPS91_TY_S_AC_39N29826.SetValue(this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1, "BIYUL",  Convert.ToString(dBIYUL));
                    this.FPS91_TY_S_AC_39N29826.SetValue(this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1, "BIYUL1", Convert.ToString(dBIYUL1));
                    this.FPS91_TY_S_AC_39N29826.SetValue(this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1, "BIYUL2", Convert.ToString(dBIYUL2));

                    this.FPS91_TY_S_AC_39N29826.SetValue(this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1, "AMT",  Convert.ToString(dAMT));
                    this.FPS91_TY_S_AC_39N29826.SetValue(this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1, "AMT1", Convert.ToString(dAMT1));
                    this.FPS91_TY_S_AC_39N29826.SetValue(this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1, "AMT2", Convert.ToString(dAMT2));

                    this.FPS91_TY_S_AC_39N29826.ActiveSheet.Rows[this.FPS91_TY_S_AC_39N29826.ActiveSheet.RowCount - 1].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

            this.DTP01_ESCYYMM.Focus();
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG") // 그레인
            {
                // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

                this.DbConnector.CommandClear(); // TY_P_AC_27H64059
                this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_ESCYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_ESCYYMM.GetValue().ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }

                // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

                ds.Tables.Add(this.FPS91_TY_S_AC_39BBZ657.GetDataSourceInclude(TSpread.TActionType.Remove, "ESCVEND"));

                // 삭제
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_GB_23NAD870");
                    e.Successed = false;
                    return;
                }

                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }
            }            

            e.ArgData = ds;

        }
        #endregion

        #region Description : 주요매출처 스프레드 더블클릭
        private void FPS91_TY_S_AC_39BBZ657_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYAFMA003I(this.DTP01_ESCYYMM.GetValue().ToString(), this.FPS91_TY_S_AC_39BBZ657.GetValue("ESCVEND").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetStartingFocus(this.DTP01_ESCYYMM);

            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG") // 그레인
            {
                if (tabControl1.SelectedIndex == 0) // 물량 조회
                {
                    // 버튼 보임
                    this.BTN61_INQ.Visible    = true;
                    this.BTN61_CREATE.Visible = true;

                    this.BTN62_INQ.Visible = false;
                    this.BTN62_NEW.Visible = false;
                    this.BTN62_REM.Visible = false;

                    this.BTN63_INQ.Visible = false;

                    this.BTN61_INQ_Click(null, null);
                }
                else // 계획 관리
                {
                    this.FPS91_TY_S_AC_39BBZ657.Initialize();

                    //// 버튼 보임
                    this.LBL51_ESCSUBGN.Visible = true;
                    this.CBH01_ESCSUBGN.Visible = true;

                    this.LBL51_ESCVEND.Visible  = true;
                    this.CBH01_ESCVEND.Visible  = true;

                    this.BTN62_INQ.Visible      = true;
                    this.BTN62_NEW.Visible      = true;
                    this.BTN62_REM.Visible      = true;

                    this.BTN61_INQ.Visible    = false;
                    this.BTN61_CREATE.Visible = false;

                    this.BTN63_INQ.Visible = false;

                    this.BTN62_INQ_Click(null, null);
                }
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TL") // 티와이스틸
            {
                this.BTN63_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 계열사 코드박스 이벤트
        private void CBH01_ESCSUBGN_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG" ||
                this.CBH01_ESCSUBGN.GetValue().ToString() == "TH" ||
                this.CBH01_ESCSUBGN.GetValue().ToString() == "TS")
            {
                this.BTN61_INQ.Visible    = true;
                this.BTN61_CREATE.Visible = true;

                // 버튼 숨김
                this.BTN62_INQ.Visible = false;
                this.BTN62_NEW.Visible = false;
                this.BTN62_REM.Visible = false;

                this.BTN63_INQ.Visible = false;
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TL")
            {
                this.BTN61_INQ.Visible    = false;
                this.BTN61_CREATE.Visible = false;

                // 버튼 숨김
                this.BTN62_INQ.Visible = false;
                this.BTN62_NEW.Visible = false;
                this.BTN62_REM.Visible = false;


                this.BTN63_INQ.Visible = true;
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG")
            {
                this.BTN61_INQ.Visible    = false;
                this.BTN61_CREATE.Visible = false;

                // 버튼 숨김
                this.BTN62_INQ.Visible = true;
                this.BTN62_NEW.Visible = true;
                this.BTN62_REM.Visible = true;


                this.BTN63_INQ.Visible = false;
            }

            tabControl_Enable();

            this.FPS91_TY_S_AC_39BBA656.Initialize();
            this.FPS91_TY_S_AC_39BBZ657.Initialize();
            this.FPS91_TY_S_AC_39C2D697.Initialize();
            this.FPS91_TY_S_AC_39G4V778.Initialize();
            this.FPS91_TY_S_AC_39N29826.Initialize();

            UP_Spread_Title(this.DTP01_ESCYYMM.GetValue().ToString());
        }
        #endregion

        #region Description : 탭 컨트롤 이벤트
        private void tabControl_Enable()
        {
            if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TG")
            {
                // 그레인
                if (!this.tabControl1.TabPages.Contains(this.tabPage1))
                    this.tabControl1.TabPages.Add(this.tabPage1);

                if (!this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Add(this.tabPage2);

                // 호라이즌
                if (this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Remove(this.tabPage3);

                // 티와이스틸
                if (this.tabControl1.TabPages.Contains(this.tabPage4))
                    this.tabControl1.TabPages.Remove(this.tabPage4);

                // GLS
                if (this.tabControl1.TabPages.Contains(this.tabPage5))
                    this.tabControl1.TabPages.Remove(this.tabPage5);
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TH")
            {
                // 그레인
                if (this.tabControl1.TabPages.Contains(this.tabPage1))
                    this.tabControl1.TabPages.Remove(this.tabPage1);

                if (this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Remove(this.tabPage2);

                // 호라이즌
                if (!this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Add(this.tabPage3);

                // 티와이스틸
                if (this.tabControl1.TabPages.Contains(this.tabPage4))
                    this.tabControl1.TabPages.Remove(this.tabPage4);

                // GLS
                if (this.tabControl1.TabPages.Contains(this.tabPage5))
                    this.tabControl1.TabPages.Remove(this.tabPage5);
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TS")
            {
                // 그레인
                if (this.tabControl1.TabPages.Contains(this.tabPage1))
                    this.tabControl1.TabPages.Remove(this.tabPage1);

                if (this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Remove(this.tabPage2);

                // 호라이즌
                if (this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Remove(this.tabPage3);

                // 티와이스틸
                if (this.tabControl1.TabPages.Contains(this.tabPage4))
                    this.tabControl1.TabPages.Remove(this.tabPage4);

                // GLS
                if (!this.tabControl1.TabPages.Contains(this.tabPage5))
                    this.tabControl1.TabPages.Add(this.tabPage5);
            }
            else if (this.CBH01_ESCSUBGN.GetValue().ToString() == "TL")
            {
                // 그레인
                if (this.tabControl1.TabPages.Contains(this.tabPage1))
                    this.tabControl1.TabPages.Remove(this.tabPage1);

                if (this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Remove(this.tabPage2);

                // 호라이즌
                if (this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Remove(this.tabPage3);

                // 티와이스틸
                if (!this.tabControl1.TabPages.Contains(this.tabPage4))
                    this.tabControl1.TabPages.Add(this.tabPage4);

                // GLS
                if (this.tabControl1.TabPages.Contains(this.tabPage5))
                    this.tabControl1.TabPages.Remove(this.tabPage5);
            }
        }
        #endregion

        #region Description : EIS 계열사 최종 마감 월 조회
        private void UP_start_dsp_month()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C94Q662");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.DTP01_ESCYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_ESCYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion
    }
}