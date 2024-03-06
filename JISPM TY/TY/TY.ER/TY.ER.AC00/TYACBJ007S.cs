using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;

namespace TY.ER.AC00
{
    /// <summary>
    /// 보조부 출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.26 14:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27Q30296 : 보조부 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27R12300 : 보조부 출력
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GEDCDAC : 계정코드
    ///  GSTCDAC : 계정코드
    ///  GVEND : 거래처
    ///  GAMOUNT : 현금
    ///  GSEMOK : 계정세목
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACBJ007S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ007S()
        {
            InitializeComponent();
        }

        private void TYACBJ007S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.CBH01_GSTCDAC.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            // 원본(21.09.30 수정 전)
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_27Q30296",
            //    this.CBH01_GSTCDAC.GetValue(),
            //    this.CBH01_GEDCDAC.GetValue(),
            //    this.DTP01_GSTDATE.GetString().ToString(),
            //    this.DTP01_GEDDATE.GetString().ToString(),
            //    this.CBH01_GVEND.GetValue(),
            //    this.CBH01_GCDDP.GetValue(),
            //    this.CBO01_GSEMOK.GetValue()
            //    );

            // 수정본(21.09.30 수정 후)
            this.DbConnector.Attach
                (
                "TY_P_AC_B9UEE595",
                this.CBH01_GSTCDAC.GetValue(),
                this.CBH01_GEDCDAC.GetValue(),
                this.DTP01_GSTDATE.GetString().ToString(),
                this.DTP01_GEDDATE.GetString().ToString(),
                this.CBH01_GVEND.GetValue(),
                this.CBH01_GCDDP.GetValue(),
                this.CBO01_GSEMOK.GetValue()
                );
            
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_27R12300.SetValue(UP_ConvertDt(dt,"조회"));

                // 특정 ROW 잠금
                for (int i = 0; i < this.FPS91_TY_S_AC_27R12300.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_27R12300.GetValue(i, "YYMM").ToString() == "전월이월" ||
                        this.FPS91_TY_S_AC_27R12300.GetValue(i, "YYMM").ToString() == "전기이월")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_27R12300.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                    }
                    else if (this.FPS91_TY_S_AC_27R12300.GetValue(i, "YYMM").ToString() == "월     계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_27R12300.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_27R12300.GetValue(i, "YYMM").ToString() == "누     계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_27R12300.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }

                for (int i = 0; i < this.FPS91_TY_S_AC_27R12300.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_27R12300.GetValue(i, "B4NOJP").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_27R12300_Sheet1.Cells[i, 8].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            // 원본(21.09.30 수정 전)
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_27Q30296",
            //    this.CBH01_GSTCDAC.GetValue(),
            //    this.CBH01_GEDCDAC.GetValue(),
            //    this.DTP01_GSTDATE.GetValue(),
            //    this.DTP01_GEDDATE.GetValue(),
            //    this.CBH01_GVEND.GetValue(),
            //    this.CBH01_GCDDP.GetValue(),
            //    this.CBO01_GSEMOK.GetValue()
            //    );

            // 수정본(21.09.30 수정 후)
            this.DbConnector.Attach
                (
                "TY_P_AC_B9UEE595",
                this.CBH01_GSTCDAC.GetValue(),
                this.CBH01_GEDCDAC.GetValue(),
                this.DTP01_GSTDATE.GetString().ToString(),
                this.DTP01_GEDDATE.GetString().ToString(),
                this.CBH01_GVEND.GetValue(),
                this.CBH01_GCDDP.GetValue(),
                this.CBO01_GSEMOK.GetValue()
                );

            SectionReport rpt = new TYACBJ007R();

            // 가로 출력
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {

                (new TYERGB001P(rpt, UP_ConvertDt(dt,"출력"))).ShowDialog();
            }
        }
        #endregion        

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt, string sGUBUN)
        {
            string sNEWCDAC = string.Empty;
            string sOLDCDAC = string.Empty;

            string sNEWA1ABAC = string.Empty;
            string sOLDA1ABAC = string.Empty;

            string sNEWMonth = string.Empty;
            string sOLDMonth = string.Empty;

            string sNEWDATE = string.Empty;
            string sOLDDATE = string.Empty;

            string sA1TAG01 = string.Empty;

            double dB4AMDR = 0;
            double dB4AMCR = 0;
            double dB4AMJAN = 0;

            double dWOLHAP_DR = 0;
            double dWOLHAP_CR = 0;

            double dTOTALHAP_DR = 0;
            double dTOTALHAP_CR = 0;
            double dTOTALHAPAMJAN = 0;

            DataTable Finaldt = new DataTable();

            DataRow row;

            Finaldt.Columns.Add("CDAC", typeof(System.String));
            Finaldt.Columns.Add("A1ABAC", typeof(System.String));
            Finaldt.Columns.Add("YYMM", typeof(System.String));
            Finaldt.Columns.Add("NOSQ", typeof(System.String));
            Finaldt.Columns.Add("NOLN", typeof(System.String));
            Finaldt.Columns.Add("B4RKAC", typeof(System.String));
            Finaldt.Columns.Add("KGHANGL", typeof(System.String));
            Finaldt.Columns.Add("B4NOJP", typeof(System.String));
            Finaldt.Columns.Add("B4VLMI1", typeof(System.String));
            Finaldt.Columns.Add("B4SAUP1", typeof(System.String));
            Finaldt.Columns.Add("B4VLMI2", typeof(System.String));
            Finaldt.Columns.Add("B4SAUP2", typeof(System.String));
            Finaldt.Columns.Add("B4VLMI3", typeof(System.String));
            Finaldt.Columns.Add("B4SAUP3", typeof(System.String));
            Finaldt.Columns.Add("B4DPAC", typeof(System.String));
            Finaldt.Columns.Add("B4AMDR", typeof(System.String));
            Finaldt.Columns.Add("B4AMCR", typeof(System.String));
            Finaldt.Columns.Add("B4RKCU", typeof(System.String));
            Finaldt.Columns.Add("E6DTED", typeof(System.String));
            Finaldt.Columns.Add("AMJAN", typeof(System.String));
            Finaldt.Columns.Add("FROMDATE", typeof(System.String));
            Finaldt.Columns.Add("TODATE", typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dB4AMDR = double.Parse(Get_Numeric(dt.Rows[i]["B4AMDR"].ToString()));
                dB4AMCR = double.Parse(Get_Numeric(dt.Rows[i]["B4AMCR"].ToString()));
                dB4AMJAN = double.Parse(Get_Numeric(dt.Rows[i]["AMJAN"].ToString()));

                sNEWMonth = dt.Rows[i]["DTAC"].ToString().Substring(0, 6);
                sNEWDATE = dt.Rows[i]["DTAC"].ToString();

                sNEWCDAC = dt.Rows[i]["CDAC"].ToString();
                sNEWA1ABAC = dt.Rows[i]["A1ABAC"].ToString();

                if (i == 0)
                {
                    row = Finaldt.NewRow();

                    row["CDAC"] = sNEWCDAC.ToString();
                    row["A1ABAC"] = sNEWA1ABAC.ToString();

                    sA1TAG01 = dt.Rows[i]["A1TAG01"].ToString();

                    // 부서별 출력시 전기이월,전월이월은 출력하지 않음.
                    if (this.CBH01_GCDDP.GetValue().ToString() == "")
                    {
                        if (dt.Rows[i]["NOSQ"].ToString() == "0")
                        {
                            if (dt.Rows[i]["CFROMB4DTAC"].ToString().Substring(4, 2) == "01")
                            {
                                row["YYMM"] = "전기이월";
                            }
                            else
                            {
                                row["YYMM"] = "전월이월";
                            }

                            row["NOSQ"] = "";
                            row["NOLN"] = "";
                        }
                    }
                    else
                    {
                        row["YYMM"] = sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2);

                        row["NOSQ"] = string.Format("{0:000}", dt.Rows[i]["NOSQ"].ToString());
                        row["NOLN"] = string.Format("{0:00}", dt.Rows[i]["NOLN"].ToString());
                    }
                    row["B4RKAC"] = dt.Rows[i]["B4RKAC"].ToString();
                    row["KGHANGL"] = dt.Rows[i]["KGHANGL"].ToString();
                    row["B4NOJP"] = dt.Rows[i]["B4NOJP"].ToString();
                    row["B4VLMI1"] = dt.Rows[i]["B4VLMI1"].ToString();
                    row["B4SAUP1"] = dt.Rows[i]["B4SAUP1"].ToString();
                    row["B4VLMI2"] = dt.Rows[i]["B4VLMI2"].ToString();
                    row["B4SAUP2"] = dt.Rows[i]["B4SAUP2"].ToString();
                    row["B4VLMI3"] = dt.Rows[i]["B4VLMI3"].ToString();
                    row["B4SAUP3"] = dt.Rows[i]["B4SAUP3"].ToString();
                    row["B4DPAC"] = dt.Rows[i]["B4DPAC"].ToString();
                    row["B4RKCU"] = dt.Rows[i]["B4RKCU"].ToString();
                    row["E6DTED"] = dt.Rows[i]["E6DTED"].ToString();
                    row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dB4AMDR));
                    row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dB4AMCR));
                    row["AMJAN"] = string.Format("{0:#,###}", Convert.ToString(dB4AMJAN));
                    row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
                    row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

                    Finaldt.Rows.Add(row);

                    // 부서별 출력시 전기이월,전월이월은 출력하지 않음.
                    if (this.CBH01_GCDDP.GetValue().ToString() == "")
                    {
                        if (dt.Rows[i]["NOSQ"].ToString() == "0")
                        {
                            // 월계
                            dWOLHAP_DR = 0;
                            dWOLHAP_CR = 0;
                        }
                    }
                    else
                    {
                        // 월계
                        dWOLHAP_DR = dWOLHAP_DR + dB4AMDR;
                        dWOLHAP_CR = dWOLHAP_CR + dB4AMCR;
                    }

                    // 누계
                    dTOTALHAP_DR = dTOTALHAP_DR + dB4AMDR;
                    dTOTALHAP_CR = dTOTALHAP_CR + dB4AMCR;
                    dTOTALHAPAMJAN = dB4AMJAN;

                    sOLDCDAC = dt.Rows[i]["CDAC"].ToString();
                    sOLDA1ABAC = dt.Rows[i]["A1ABAC"].ToString();

                    sOLDMonth = dt.Rows[i]["DTAC"].ToString().Substring(0, 6);
                    sOLDDATE = dt.Rows[i]["DTAC"].ToString();
                }
                else
                {
                    if (sNEWCDAC.ToString() != sOLDCDAC.ToString())
                    {
                        // 월계
                        row = Finaldt.NewRow();

                        if (sGUBUN == "조회")
                        {
                            row["CDAC"] = "";
                            row["A1ABAC"] = "";
                        }
                        else
                        {
                            row["CDAC"] = sOLDCDAC.ToString();
                            row["A1ABAC"] = sOLDA1ABAC.ToString();
                        }
                        row["YYMM"] = "월     계";
                        row["NOSQ"] = "";
                        row["NOLN"] = "";
                        row["B4RKAC"] = "";
                        row["KGHANGL"] = "";
                        row["B4NOJP"] = "";
                        row["B4VLMI1"] = "";
                        row["B4SAUP1"] = "";
                        row["B4VLMI2"] = "";
                        row["B4SAUP2"] = "";
                        row["B4VLMI3"] = "";
                        row["B4SAUP3"] = "";
                        row["B4DPAC"] = "";
                        row["B4RKCU"] = "";
                        row["E6DTED"] = "";
                        row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dWOLHAP_DR));
                        row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dWOLHAP_CR));
                        if (sA1TAG01.ToString() == "C")
                        {
                            row["AMJAN"] = dWOLHAP_CR - dWOLHAP_DR;
                        }
                        else
                        {
                            row["AMJAN"] = dWOLHAP_DR - dWOLHAP_CR;
                        }
                        row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
                        row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

                        Finaldt.Rows.Add(row);

                        // 누계
                        row = Finaldt.NewRow();

                        if (sGUBUN == "조회")
                        {
                            row["CDAC"] = "";
                            row["A1ABAC"] = "";

                        }
                        else
                        {
                            row["CDAC"] = sOLDCDAC.ToString();
                            row["A1ABAC"] = sOLDA1ABAC.ToString();
                        }
                        row["YYMM"] = "누     계";
                        row["NOSQ"] = "";
                        row["NOLN"] = "";
                        row["B4RKAC"] = "";
                        row["KGHANGL"] = "";
                        row["B4NOJP"] = "";
                        row["B4VLMI1"] = "";
                        row["B4SAUP1"] = "";
                        row["B4VLMI2"] = "";
                        row["B4SAUP2"] = "";
                        row["B4VLMI3"] = "";
                        row["B4SAUP3"] = "";
                        row["B4DPAC"] = "";
                        row["B4RKCU"] = "";
                        row["E6DTED"] = "";
                        row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dTOTALHAP_DR));
                        row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dTOTALHAP_CR));
                        row["AMJAN"] = string.Format("{0:#,###}", Convert.ToString(dTOTALHAPAMJAN));
                        row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
                        row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

                        Finaldt.Rows.Add(row);



                        #region Description : 변수 초기화

                        // 월계
                        dWOLHAP_DR = 0;
                        dWOLHAP_CR = 0;

                        // 누계
                        dTOTALHAP_DR = 0;
                        dTOTALHAP_CR = 0;
                        dTOTALHAPAMJAN = 0;

                        sOLDCDAC = "";
                        sOLDA1ABAC = "";

                        sOLDMonth = "";
                        sOLDDATE = "";

                        #endregion



                        row = Finaldt.NewRow();

                        row["CDAC"] = sNEWCDAC.ToString();
                        row["A1ABAC"] = sNEWA1ABAC.ToString();

                        sA1TAG01 = dt.Rows[i]["A1TAG01"].ToString();

                        // 부서별 출력시 전기이월,전월이월은 출력하지 않음.
                        if (this.CBH01_GCDDP.GetValue().ToString() == "")
                        {
                            if (dt.Rows[i]["NOSQ"].ToString() == "0")
                            {
                                if (dt.Rows[i]["CFROMB4DTAC"].ToString().Substring(4, 2) == "01")
                                {
                                    row["YYMM"] = "전기이월";
                                }
                                else
                                {
                                    row["YYMM"] = "전월이월";
                                }

                                row["NOSQ"] = "";
                                row["NOLN"] = "";
                            }
                            else
                            {
                                if (sNEWDATE.ToString() == sOLDDATE.ToString())
                                {
                                    //row["YYMM"] = "";
                                    row["YYMM"] = sGUBUN == "조회" ? sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2) : "";
                                }
                                else
                                {
                                    row["YYMM"] = sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2);
                                }

                                row["NOSQ"] = string.Format("{0:000}", dt.Rows[i]["NOSQ"].ToString());
                                row["NOLN"] = string.Format("{0:00}", dt.Rows[i]["NOLN"].ToString());
                            }
                        }
                        else
                        {
                            if (sNEWDATE.ToString() == sOLDDATE.ToString())
                            {
                                //row["YYMM"] = "";
                                row["YYMM"] = sGUBUN == "조회" ? sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2) : "";
                            }
                            else
                            {
                                row["YYMM"] = sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2);
                            }

                            row["NOSQ"] = string.Format("{0:000}", dt.Rows[i]["NOSQ"].ToString());
                            row["NOLN"] = string.Format("{0:00}", dt.Rows[i]["NOLN"].ToString());
                        }
                        row["B4RKAC"] = dt.Rows[i]["B4RKAC"].ToString();
                        row["KGHANGL"] = dt.Rows[i]["KGHANGL"].ToString();
                        row["B4NOJP"] = dt.Rows[i]["B4NOJP"].ToString();
                        row["B4VLMI1"] = dt.Rows[i]["B4VLMI1"].ToString();
                        row["B4SAUP1"] = dt.Rows[i]["B4SAUP1"].ToString();
                        row["B4VLMI2"] = dt.Rows[i]["B4VLMI2"].ToString();
                        row["B4SAUP2"] = dt.Rows[i]["B4SAUP2"].ToString();
                        row["B4VLMI3"] = dt.Rows[i]["B4VLMI3"].ToString();
                        row["B4SAUP3"] = dt.Rows[i]["B4SAUP3"].ToString();
                        row["B4DPAC"] = dt.Rows[i]["B4DPAC"].ToString();
                        row["B4RKCU"] = dt.Rows[i]["B4RKCU"].ToString();
                        row["E6DTED"] = dt.Rows[i]["E6DTED"].ToString();
                        row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dB4AMDR));
                        row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dB4AMCR));
                        row["AMJAN"] = string.Format("{0:#,###}", Convert.ToString(dB4AMJAN));
                        row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
                        row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

                        Finaldt.Rows.Add(row);

                        // 부서별 출력시 전기이월,전월이월은 출력하지 않음.
                        if (this.CBH01_GCDDP.GetValue().ToString() == "")
                        {
                            if (dt.Rows[i]["NOSQ"].ToString() == "0")
                            {
                                // 월계
                                dWOLHAP_DR = 0;
                                dWOLHAP_CR = 0;
                            }
                            else
                            {
                                // 월계
                                dWOLHAP_DR = dWOLHAP_DR + dB4AMDR;
                                dWOLHAP_CR = dWOLHAP_CR + dB4AMCR;
                            }
                        }
                        else
                        {
                            // 월계
                            dWOLHAP_DR = dWOLHAP_DR + dB4AMDR;
                            dWOLHAP_CR = dWOLHAP_CR + dB4AMCR;
                        }

                        // 누계
                        dTOTALHAP_DR = dTOTALHAP_DR + dB4AMDR;
                        dTOTALHAP_CR = dTOTALHAP_CR + dB4AMCR;
                        dTOTALHAPAMJAN = dB4AMJAN;

                        sOLDCDAC = dt.Rows[i]["CDAC"].ToString();
                        sOLDA1ABAC = dt.Rows[i]["A1ABAC"].ToString();

                        sOLDMonth = dt.Rows[i]["DTAC"].ToString().Substring(0, 6);
                        sOLDDATE = dt.Rows[i]["DTAC"].ToString();
                    }
                    else // 계정과목 같을 경우
                    {
                        if (sNEWMonth.ToString() != sOLDMonth.ToString())
                        {
                            // 월계
                            row = Finaldt.NewRow();

                            if (sGUBUN == "조회")
                            {
                                row["CDAC"] = "";
                                row["A1ABAC"] = "";
                            }
                            else
                            {
                                row["CDAC"] = sOLDCDAC.ToString();
                                row["A1ABAC"] = sOLDA1ABAC.ToString();
                            }
                            row["YYMM"] = "월     계";
                            row["NOSQ"] = "";
                            row["NOLN"] = "";
                            row["B4RKAC"] = "";
                            row["KGHANGL"] = "";
                            row["B4NOJP"] = "";
                            row["B4VLMI1"] = "";
                            row["B4SAUP1"] = "";
                            row["B4VLMI2"] = "";
                            row["B4SAUP2"] = "";
                            row["B4VLMI3"] = "";
                            row["B4SAUP3"] = "";
                            row["B4DPAC"] = "";
                            row["B4RKCU"] = "";
                            row["E6DTED"] = "";
                            row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dWOLHAP_DR));
                            row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dWOLHAP_CR));

                            if (sA1TAG01.ToString() == "C")
                            {
                                row["AMJAN"] = dWOLHAP_CR - dWOLHAP_DR;
                            }
                            else
                            {
                                row["AMJAN"] = dWOLHAP_DR - dWOLHAP_CR;
                            }

                            row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
                            row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

                            Finaldt.Rows.Add(row);

                            // 월계
                            dWOLHAP_DR = 0;
                            dWOLHAP_CR = 0;

                            //// 누계
                            //row = Finaldt.NewRow();

                            //if (sGUBUN == "조회")
                            //{
                            //    row["CDAC"]   = "";
                            //    row["A1ABAC"] = "";
                            //}
                            //else
                            //{
                            //    row["CDAC"]   = sOLDCDAC.ToString();
                            //    row["A1ABAC"] = sOLDA1ABAC.ToString();
                            //}
                            //row["YYMM"]     = "누     계";
                            //row["NOSQ"]     = "";
                            //row["NOLN"]     = "";
                            //row["B4RKAC"]   = "";
                            //row["KGHANGL"]  = "";
                            //row["B4NOJP"]   = "";
                            //row["B4VLMI1"]  = "";
                            //row["B4VLMI2"]  = "";
                            //row["B4VLMI3"]  = "";
                            //row["B4DPAC"]   = "";
                            //row["B4RKCU"]   = "";
                            //row["E6DTED"]   = "";
                            //row["B4AMDR"]   = string.Format("{0:#,###}", Convert.ToString(dTOTALHAP_DR));
                            //row["B4AMCR"]   = string.Format("{0:#,###}", Convert.ToString(dTOTALHAP_CR));
                            //row["AMJAN"]    = string.Format("{0:#,###}", Convert.ToString(dTOTALHAPAMJAN));
                            //row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
                            //row["TODATE"]   = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

                            //Finaldt.Rows.Add(row);
                        }

                        row = Finaldt.NewRow();

                        if (sGUBUN == "조회")
                        {
                            //row["CDAC"]   = "";
                            //row["A1ABAC"] = "";

                            row["CDAC"] = sOLDCDAC.ToString();
                            row["A1ABAC"] = sOLDA1ABAC.ToString();


                        }
                        else
                        {
                            row["CDAC"] = sOLDCDAC.ToString();
                            row["A1ABAC"] = sOLDA1ABAC.ToString();
                        }

                        // 부서별 출력시 전기이월,전월이월은 출력하지 않음.
                        if (this.CBH01_GCDDP.GetValue().ToString() == "")
                        {
                            if (dt.Rows[i]["NOSQ"].ToString() == "0")
                            {
                                if (dt.Rows[i]["CFROMB4DTAC"].ToString().Substring(4, 2) == "01")
                                {
                                    row["YYMM"] = "전기이월";
                                }
                                else
                                {
                                    row["YYMM"] = "전월이월";
                                }

                                row["NOSQ"] = "";
                                row["NOLN"] = "";
                            }
                            else
                            {
                                if (sNEWDATE.ToString() == sOLDDATE.ToString())
                                {
                                    //row["YYMM"] = "";
                                    row["YYMM"] = sGUBUN == "조회" ? sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2) : "";
                                }
                                else
                                {
                                    row["YYMM"] = sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2);
                                }

                                row["NOSQ"] = string.Format("{0:000}", dt.Rows[i]["NOSQ"].ToString());
                                row["NOLN"] = string.Format("{0:00}", dt.Rows[i]["NOLN"].ToString());
                            }
                        }
                        else
                        {
                            if (sNEWDATE.ToString() == sOLDDATE.ToString())
                            {
                                //row["YYMM"] = "";
                                row["YYMM"] = sGUBUN == "조회" ? "" : sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2);
                            }
                            else
                            {
                                row["YYMM"] = sNEWDATE.Substring(4, 2) + "/" + sNEWDATE.Substring(6, 2);
                            }

                            row["NOSQ"] = string.Format("{0:000}", dt.Rows[i]["NOSQ"].ToString());
                            row["NOLN"] = string.Format("{0:00}", dt.Rows[i]["NOLN"].ToString());
                        }

                        row["B4RKAC"] = dt.Rows[i]["B4RKAC"].ToString();
                        row["KGHANGL"] = dt.Rows[i]["KGHANGL"].ToString();
                        row["B4NOJP"] = dt.Rows[i]["B4NOJP"].ToString();
                        row["B4VLMI1"] = dt.Rows[i]["B4VLMI1"].ToString();
                        row["B4SAUP1"] = dt.Rows[i]["B4SAUP1"].ToString();
                        row["B4VLMI2"] = dt.Rows[i]["B4VLMI2"].ToString();
                        row["B4SAUP2"] = dt.Rows[i]["B4SAUP2"].ToString();
                        row["B4VLMI3"] = dt.Rows[i]["B4VLMI3"].ToString();
                        row["B4SAUP3"] = dt.Rows[i]["B4SAUP3"].ToString();
                        row["B4DPAC"] = dt.Rows[i]["B4DPAC"].ToString();
                        row["B4RKCU"] = "";
                        row["E6DTED"] = "";
                        row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dB4AMDR));
                        row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dB4AMCR));
                        row["AMJAN"] = string.Format("{0:#,###}", Convert.ToString(dTOTALHAPAMJAN + dB4AMJAN));
                        row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
                        row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

                        Finaldt.Rows.Add(row);

                        // 월계
                        dWOLHAP_DR = dWOLHAP_DR + dB4AMDR;
                        dWOLHAP_CR = dWOLHAP_CR + dB4AMCR;

                        sOLDCDAC = dt.Rows[i]["CDAC"].ToString();
                        sOLDA1ABAC = dt.Rows[i]["A1ABAC"].ToString();

                        sOLDMonth = dt.Rows[i]["DTAC"].ToString().Substring(0, 6);
                        sOLDDATE = dt.Rows[i]["DTAC"].ToString();

                        // 누계
                        dTOTALHAP_DR = dTOTALHAP_DR + dB4AMDR;
                        dTOTALHAP_CR = dTOTALHAP_CR + dB4AMCR;
                        dTOTALHAPAMJAN = dTOTALHAPAMJAN + dB4AMJAN;
                    }
                }
            }

            // 월계
            row = Finaldt.NewRow();

            if (sGUBUN == "조회")
            {
                row["CDAC"] = "";
                row["A1ABAC"] = "";
            }
            else
            {
                row["CDAC"] = sOLDCDAC.ToString();
                row["A1ABAC"] = sOLDA1ABAC.ToString();
            }
            row["YYMM"] = "월     계";
            row["NOSQ"] = "";
            row["NOLN"] = "";
            row["B4RKAC"] = "";
            row["KGHANGL"] = "";
            row["B4NOJP"] = "";
            row["B4VLMI1"] = "";
            row["B4SAUP1"] = "";
            row["B4VLMI2"] = "";
            row["B4SAUP2"] = "";
            row["B4VLMI3"] = "";
            row["B4SAUP3"] = "";
            row["B4DPAC"] = "";
            row["B4RKCU"] = "";
            row["E6DTED"] = "";
            row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dWOLHAP_DR));
            row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dWOLHAP_CR));
            if (sA1TAG01.ToString() == "C")
            {
                row["AMJAN"] = dWOLHAP_CR - dWOLHAP_DR;
            }
            else
            {
                row["AMJAN"] = dWOLHAP_DR - dWOLHAP_CR;
            }
            row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
            row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

            Finaldt.Rows.Add(row);

            // 월계
            dWOLHAP_DR = 0;
            dWOLHAP_CR = 0;

            // 누계
            row = Finaldt.NewRow();

            if (sGUBUN == "조회")
            {
                row["CDAC"] = "";
                row["A1ABAC"] = "";
            }
            else
            {
                row["CDAC"] = sOLDCDAC.ToString();
                row["A1ABAC"] = sOLDA1ABAC.ToString();
            }
            row["YYMM"] = "누     계";
            row["NOSQ"] = "";
            row["NOLN"] = "";
            row["B4RKAC"] = "";
            row["KGHANGL"] = "";
            row["B4NOJP"] = "";
            row["B4VLMI1"] = "";
            row["B4SAUP1"] = "";
            row["B4VLMI2"] = "";
            row["B4SAUP2"] = "";
            row["B4VLMI3"] = "";
            row["B4SAUP3"] = "";
            row["B4DPAC"] = "";
            row["B4RKCU"] = "";
            row["E6DTED"] = "";
            row["B4AMDR"] = string.Format("{0:#,###}", Convert.ToString(dTOTALHAP_DR));
            row["B4AMCR"] = string.Format("{0:#,###}", Convert.ToString(dTOTALHAP_CR));
            row["AMJAN"] = string.Format("{0:#,###}", Convert.ToString(dTOTALHAPAMJAN));
            row["FROMDATE"] = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GSTDATE.GetValue().ToString().Substring(6, 2);
            row["TODATE"] = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2) + "/" + this.DTP01_GEDDATE.GetValue().ToString().Substring(6, 2);

            Finaldt.Rows.Add(row);

            return Finaldt;
        }
        #endregion

        #region Description : DTP01_GSTDATE_ValueChanged 이벤트
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = this.DTP01_GSTDATE.GetValue(); 
        }
        #endregion

        #region Description : FPS91_TY_S_AC_27R12300_ButtonClicked 이벤트
        private void FPS91_TY_S_AC_27R12300_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string sB2DPMK = "";
            string sB2DTMK = "";
            string sB2NOSQ = "";

            if (e.Column.ToString() == "8") // 설정전표 
            {
                sB2DPMK = this.FPS91_TY_S_AC_27R12300.GetValue("B4NOJP").ToString().Substring(0, 6);
                sB2DTMK = this.FPS91_TY_S_AC_27R12300.GetValue("B4NOJP").ToString().Substring(6, 8);
                sB2NOSQ = this.FPS91_TY_S_AC_27R12300.GetValue("B4NOJP").ToString().Substring(14, 3);

                if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}