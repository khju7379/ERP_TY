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

namespace TY.ER.AC00
{
    /// <summary>
    /// K-GAAP 손익계산서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.01 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26112779 : K-GAAP 손익계산서 조회 및 출력
    ///  TY_P_AC_26115782 : K-GAAP 손익계산서 조회 및 출력(IS 세목 포함)
    ///  TY_P_GB_2423M259 : 코드박스-공통코드
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25V2N753 : 손익계산서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  AG1YNISDT : I/S계정세목
    ///  GBEDDATE  : 비교종료일자
    ///  GBSTDATE  : 비교시작일자
    ///  GEDDATE   : 종료일자
    ///  GSTDATE   : 시작일자
    ///  GBENO     : 비교년월-기
    ///  GCUNO     : 기준년월-기
    /// </summary>
    public partial class TYACBJ015S : TYBase
    {
        string fsGSTDATE = string.Empty;
        string fsGEDDATE = string.Empty;

        private string fsGBSTDATE = string.Empty;
        private string fsGBEDDATE = string.Empty;

        #region Description : 페이지 로드
        public TYACBJ015S()
        {
            InitializeComponent();
        }

        private void TYACBJ015S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_GCUNO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sGBSTYEAR  = string.Empty;
            string sGBSTMONTH = string.Empty;
            string sGBSTDAY   = string.Empty;
            
            string sGBEDYEAR  = string.Empty;
            string sGBEDMONTH = string.Empty;
            string sGBEDDAY   = string.Empty;
            string sYear      = string.Empty;
            string sMonth     = string.Empty;

            int iDD = 0;

            fsGSTDATE = this.DTP01_GSTDATE.GetValue().ToString() + "01";
            fsGEDDATE = this.DTP01_GEDDATE.GetValue().ToString();
            sYear     = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4);
            sMonth    = this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));

            fsGEDDATE = fsGEDDATE + Convert.ToString(iDD);

            fsGBSTDATE = "";
            fsGBEDDATE = "";

            int iDANGGISU = 0;
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            DataTable dz = new DataTable();

            // 당기기수 가져오기
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_GB_2423M259",
                "FS",
                "",
                ""
                );

            dz = this.DbConnector.ExecuteDataTable();

            if (dz.Rows.Count > 0)
            {
                iDANGGISU = int.Parse(dz.Rows[0][1].ToString()) - 1;

                this.TXT01_GCUNO.SetValue(Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - iDANGGISU));
            }

            if (this.TXT01_GBENO.GetValue().ToString() == "")
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.TXT01_GCUNO.GetValue().ToString()) - 1));

                sSTDATE = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "0101";

                sEDDATE = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "1231";

                this.DTP01_GBSTDATE.SetValue(sSTDATE.ToString());
                this.DTP01_GBEDDATE.SetValue(sEDDATE.ToString());

                fsGBSTDATE = sSTDATE.ToString();
                fsGBEDDATE = sEDDATE.ToString();
            }
            else
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.DTP01_GBSTDATE.GetValue().ToString().Substring(0, 4)) - iDANGGISU));

                fsGBSTDATE = this.DTP01_GBSTDATE.GetValue().ToString() + "01";

                sYear  = this.DTP01_GBEDDATE.GetValue().ToString().Substring(0, 4);
                sMonth = this.DTP01_GBEDDATE.GetValue().ToString().Substring(4, 2);

                // 해당월 마지막 일자 가져오기
                iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));

                fsGBEDDATE = this.DTP01_GBEDDATE.GetValue().ToString() + Convert.ToString(iDD);
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26112779",
                this.TXT01_GCUNO.GetValue(),
                fsGSTDATE.ToString(),
                fsGEDDATE.ToString(),
                this.TXT01_GBENO.GetValue(),
                fsGBSTDATE.ToString(),
                fsGBEDDATE.ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DataTable ISdt  = new DataTable();

                DataTable Retdt = new DataTable();

                Retdt = UP_ConvertDt(dt, ISdt, "");

                // I/S 계정세목
                if (this.CKB01_AG1YNISDT.GetValue().ToString() == "Y")
                {
                    DataTable RetISdt = new DataTable();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_26115782",
                        this.TXT01_GCUNO.GetValue(),
                        fsGSTDATE.ToString(),
                        fsGEDDATE.ToString(),
                        this.TXT01_GBENO.GetValue(),
                        fsGBSTDATE.ToString(),
                        fsGBEDDATE.ToString()
                        );

                    ISdt = this.DbConnector.ExecuteDataTable();

                    if (ISdt.Rows.Count > 0)
                    {
                        RetISdt = UP_ConvertDt(Retdt, ISdt, "Y");

                        string sA1ABAC = string.Empty;
                        string sA1LVAC = string.Empty;

                        int iA1LVAC1 = 0;
                        int iA1LVAC2 = 0;
                        int iA1LVAC3 = 0;

                        // 구조만 복사
                        DataTable FinalISdt = RetISdt.Clone();

                        // 정렬 후 FinalISdt 데이터테이블에 넣음.
                        foreach (DataRow dr in RetISdt.Select("", "A1CDAC ASC"))
                        {
                            if (dr["A1LVAC"].ToString() == "1")
                            {
                                iA1LVAC1++;

                                iA1LVAC2 = 0;

                                iA1LVAC3 = 0;

                                if (iA1LVAC1 == 1)
                                {
                                    sA1LVAC = "Ⅰ";
                                }
                                else if (iA1LVAC1 == 2)
                                {
                                    sA1LVAC = "Ⅱ";
                                }
                                else if (iA1LVAC1 == 3)
                                {
                                    sA1LVAC = "Ⅲ";
                                }
                                else if (iA1LVAC1 == 4)
                                {
                                    sA1LVAC = "Ⅳ";
                                }
                                else if (iA1LVAC1 == 5)
                                {
                                    sA1LVAC = "Ⅴ";
                                }
                                else if (iA1LVAC1 == 6)
                                {
                                    sA1LVAC = "Ⅵ";
                                }
                                else if (iA1LVAC1 == 7)
                                {
                                    sA1LVAC = "Ⅶ";
                                }
                                else if (iA1LVAC1 == 8)
                                {
                                    sA1LVAC = "Ⅷ";
                                }
                                else if (iA1LVAC1 == 9)
                                {
                                    sA1LVAC = "IX";
                                }
                                else if (iA1LVAC1 == 10)
                                {
                                    sA1LVAC = "Ⅹ";
                                }
                                else if (iA1LVAC1 == 11)
                                {
                                    sA1LVAC = "XI";
                                }

                                sA1ABAC = sA1LVAC.ToString() + ".     " + dr["A1ABAC"].ToString();

                            }
                            else if (dr["A1LVAC"].ToString() == "2")
                            {
                                iA1LVAC2++;

                                iA1LVAC3 = 0;

                                sA1LVAC = "  (" + Convert.ToString(iA1LVAC2) + ")";
                                sA1ABAC = sA1LVAC.ToString() + "   " + dr["A1ABAC"].ToString();
                            }
                            else
                            {
                                iA1LVAC3++;

                                sA1LVAC = "     " + Convert.ToString(iA1LVAC3) + ".";

                                sA1ABAC = sA1LVAC.ToString() + "   " + dr["A1ABAC"].ToString();
                            }

                            dr["GSTDATE"] = dr["GSTDATE"].ToString();
                            dr["GEDDATE"] = dr["GEDDATE"].ToString();
                            dr["A1CDAC"]  = dr["A1CDAC"].ToString();
                            dr["A1ABAC"]  = sA1ABAC.ToString();
                            dr["A1LVAC"]  = dr["A1LVAC"].ToString();
                            dr["A1TAG01"] = dr["A1TAG01"].ToString();
                            dr["HDRAMT"]  = dr["HDRAMT"].ToString();
                            dr["HCRAMT"]  = dr["HCRAMT"].ToString();
                            dr["ODRAMT"]  = dr["ODRAMT"].ToString();
                            dr["OCRAMT"]  = dr["OCRAMT"].ToString();
                            dr["CUNO"]    = dr["CUNO"].ToString();
                            dr["BENO"]    = dr["BENO"].ToString();

                            FinalISdt.Rows.Add(dr.ItemArray);
                        }

                        this.FPS91_TY_S_AC_25V2N753.SetValue(FinalISdt);
                    }
                }
                else
                {
                    this.FPS91_TY_S_AC_25V2N753.SetValue(Retdt);
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sGBSTYEAR  = string.Empty;
            string sGBSTMONTH = string.Empty;
            string sGBSTDAY   = string.Empty;

            string sGBEDYEAR  = string.Empty;
            string sGBEDMONTH = string.Empty;
            string sGBEDDAY   = string.Empty;
            string sYear      = string.Empty;
            string sMonth     = string.Empty;

            int iDD = 0;

            fsGSTDATE = this.DTP01_GSTDATE.GetValue().ToString() + "01";
            fsGEDDATE = this.DTP01_GEDDATE.GetValue().ToString();
            sYear     = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4);
            sMonth    = this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));

            fsGEDDATE = fsGEDDATE + Convert.ToString(iDD);

            fsGBSTDATE = "";
            fsGBEDDATE = "";

            int iDANGGISU = 0;
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            DataTable dz = new DataTable();

            // 당기기수 가져오기
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_GB_2423M259",
                "FS",
                "",
                ""
                );

            dz = this.DbConnector.ExecuteDataTable();

            if (dz.Rows.Count > 0)
            {
                iDANGGISU = int.Parse(dz.Rows[0][1].ToString()) - 1;

                this.TXT01_GCUNO.SetValue(Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - iDANGGISU));
            }

            if (this.TXT01_GBENO.GetValue().ToString() == "")
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.TXT01_GCUNO.GetValue().ToString()) - 1));

                sSTDATE = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "0101";

                sEDDATE = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "1231";

                this.DTP01_GBSTDATE.SetValue(sSTDATE.ToString());
                this.DTP01_GBEDDATE.SetValue(sEDDATE.ToString());

                fsGBSTDATE = sSTDATE.ToString();
                fsGBEDDATE = sEDDATE.ToString();
            }
            else
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.DTP01_GBSTDATE.GetValue().ToString().Substring(0, 4)) - iDANGGISU));

                fsGBSTDATE = this.DTP01_GBSTDATE.GetValue().ToString() + "01";

                sYear  = this.DTP01_GBEDDATE.GetValue().ToString().Substring(0, 4);
                sMonth = this.DTP01_GBEDDATE.GetValue().ToString().Substring(4, 2);

                // 해당월 마지막 일자 가져오기
                iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));

                fsGBEDDATE = this.DTP01_GBEDDATE.GetValue().ToString() + Convert.ToString(iDD);
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26112779",
                this.TXT01_GCUNO.GetValue(),
                fsGSTDATE.ToString(),
                fsGEDDATE.ToString(),
                this.TXT01_GBENO.GetValue(),
                fsGBSTDATE.ToString(),
                fsGBEDDATE.ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DataTable ISdt = new DataTable();

                DataTable Retdt = new DataTable();

                Retdt = UP_ConvertDt(dt, ISdt, "");

                // I/S 계정세목
                if (this.CKB01_AG1YNISDT.GetValue().ToString() == "Y")
                {
                    DataTable RetISdt = new DataTable();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_26115782",
                        this.TXT01_GCUNO.GetValue(),
                        fsGSTDATE.ToString(),
                        fsGEDDATE.ToString(),
                        this.TXT01_GBENO.GetValue(),
                        fsGBSTDATE.ToString(),
                        fsGBEDDATE.ToString()
                        );

                    ISdt = this.DbConnector.ExecuteDataTable();

                    if (ISdt.Rows.Count > 0)
                    {
                        RetISdt = UP_ConvertDt(Retdt, ISdt, "Y");

                        string sA1ABAC = string.Empty;
                        string sA1LVAC = string.Empty;

                        int iA1LVAC1 = 0;
                        int iA1LVAC2 = 0;
                        int iA1LVAC3 = 0;

                        // 구조만 복사
                        DataTable FinalISdt = RetISdt.Clone();

                        // 정렬 후 FinalISdt 데이터테이블에 넣음.
                        foreach (DataRow dr in RetISdt.Select("", "A1CDAC ASC"))
                        {
                            if (dr["A1LVAC"].ToString() == "1")
                            {
                                iA1LVAC1++;

                                iA1LVAC2 = 0;

                                iA1LVAC3 = 0;

                                if (iA1LVAC1 == 1)
                                {
                                    sA1LVAC = "Ⅰ";
                                }
                                else if (iA1LVAC1 == 2)
                                {
                                    sA1LVAC = "Ⅱ";
                                }
                                else if (iA1LVAC1 == 3)
                                {
                                    sA1LVAC = "Ⅲ";
                                }
                                else if (iA1LVAC1 == 4)
                                {
                                    sA1LVAC = "Ⅳ";
                                }
                                else if (iA1LVAC1 == 5)
                                {
                                    sA1LVAC = "Ⅴ";
                                }
                                else if (iA1LVAC1 == 6)
                                {
                                    sA1LVAC = "Ⅵ";
                                }
                                else if (iA1LVAC1 == 7)
                                {
                                    sA1LVAC = "Ⅶ";
                                }
                                else if (iA1LVAC1 == 8)
                                {
                                    sA1LVAC = "Ⅷ";
                                }
                                else if (iA1LVAC1 == 9)
                                {
                                    sA1LVAC = "IX";
                                }
                                else if (iA1LVAC1 == 10)
                                {
                                    sA1LVAC = "Ⅹ";
                                }
                                else if (iA1LVAC1 == 11)
                                {
                                    sA1LVAC = "XI";
                                }

                                sA1ABAC = sA1LVAC.ToString() + ".     " + dr["A1ABAC"].ToString();

                            }
                            else if (dr["A1LVAC"].ToString() == "2")
                            {
                                iA1LVAC2++;

                                iA1LVAC3 = 0;

                                sA1LVAC = "  (" + Convert.ToString(iA1LVAC2) + ")";
                                sA1ABAC = sA1LVAC.ToString() + "   " + dr["A1ABAC"].ToString();
                            }
                            else
                            {
                                iA1LVAC3++;

                                sA1LVAC = "     " + Convert.ToString(iA1LVAC3) + ".";

                                sA1ABAC = sA1LVAC.ToString() + "   " + dr["A1ABAC"].ToString();
                            }

                            dr["GSTDATE"] = dr["GSTDATE"].ToString();
                            dr["GEDDATE"] = dr["GEDDATE"].ToString();
                            dr["A1CDAC"]  = dr["A1CDAC"].ToString();
                            dr["A1ABAC"]  = sA1ABAC.ToString();
                            dr["A1LVAC"]  = dr["A1LVAC"].ToString();
                            dr["A1TAG01"] = dr["A1TAG01"].ToString();
                            dr["HDRAMT"]  = dr["HDRAMT"].ToString();
                            dr["HCRAMT"]  = dr["HCRAMT"].ToString();
                            dr["ODRAMT"]  = dr["ODRAMT"].ToString();
                            dr["OCRAMT"]  = dr["OCRAMT"].ToString();
                            dr["CUNO"]    = dr["CUNO"].ToString();
                            dr["BENO"]    = dr["BENO"].ToString();

                            FinalISdt.Rows.Add(dr.ItemArray);
                        }

                        SectionReport ISrpt = new TYACBJ015R();

                        ISrpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                        (new TYERGB001P(ISrpt, FinalISdt)).ShowDialog();
                    }
                }
                else
                {
                    SectionReport rpt = new TYACBJ015R();

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, Retdt)).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt, DataTable ISdt, string sGUBUN)
        {
            string sA1CDAC  = string.Empty;
            string sA1ABAC  = string.Empty;
            string sA1LVAC  = string.Empty;
            string sGSTDATE = string.Empty;
            string sGEDDATE = string.Empty;

            int iA1LVAC1 = 0;
            int iA1LVAC2 = 0;
            int iA1LVAC3 = 0;

            double dHR매출액     = 0; // (1)
            double dHR매출원가   = 0; // (2)
            double dHR매출총이익 = 0; // (3) = (1) - (2)

            double dHR판관비     = 0; // (4)
            double dHR영업이익   = 0; // (5) = (3) - (4)

            double dHR영업외수익            = 0; // (6)
            double dHR영업외비용            = 0; // (7)
            double dHR법인세비용차감전순이익 = 0; // (8) = (5) + (6) - (7)

            double dHR법인세비용 = 0; // (9)
            double dHR당기순이익 = 0; // (10) = (8) - (9)

            double dOR매출액     = 0; // (1)
            double dOR매출원가   = 0; // (2)
            double dOR매출총이익 = 0; // (3) = (1) - (2)

            double dOR판관비     = 0; // (4)
            double dOR영업이익   = 0; // (5) = (3) - (4)

            double dOR영업외수익            = 0; // (6)
            double dOR영업외비용            = 0; // (7)
            double dOR법인세비용차감전순이익 = 0; // (8) = (5) + (6) - (7)

            double dOR법인세비용 = 0; // (9)
            double dOR당기순이익 = 0; // (10) = (8) - (9)

            DataTable Retdt = new DataTable();

            DataTable ConDt = new DataTable();

            if (sGUBUN.ToString() == "Y")
            {
                ConDt = ISdt;
            }
            else
            {
                ConDt = dt;
            }

            DataRow row;

            Retdt.Columns.Add("GSTDATE", typeof(System.String));
            Retdt.Columns.Add("GEDDATE", typeof(System.String));
            Retdt.Columns.Add("A1CDAC",  typeof(System.String));
            Retdt.Columns.Add("A1ABAC",  typeof(System.String));
            Retdt.Columns.Add("A1LVAC",  typeof(System.String));
            Retdt.Columns.Add("A1TAG01", typeof(System.String));
            Retdt.Columns.Add("HDRAMT",  typeof(System.String));
            Retdt.Columns.Add("HCRAMT",  typeof(System.String));
            Retdt.Columns.Add("ODRAMT",  typeof(System.String));
            Retdt.Columns.Add("OCRAMT",  typeof(System.String));
            Retdt.Columns.Add("CUNO",    typeof(System.String));
            Retdt.Columns.Add("BENO",    typeof(System.String));

            if (sGUBUN.ToString() == "Y")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sGSTDATE = "제 " + this.TXT01_GCUNO.GetValue().ToString() + " 기  :" + this.fsGSTDATE.ToString().Substring(0, 4) + " 년 " + this.fsGSTDATE.ToString().Substring(4, 2) + " 월 " + this.fsGSTDATE.ToString().Substring(6, 2) + "일부터 ";
                    sGSTDATE = sGSTDATE + this.fsGEDDATE.ToString().Substring(0, 4) + " 년 " + this.fsGEDDATE.ToString().Substring(4, 2) + " 월 " + this.fsGEDDATE.ToString().Substring(6, 2) + "일까지";

                    sGEDDATE = "제 " + this.TXT01_GBENO.GetValue().ToString() + " 기  :" + this.fsGBSTDATE.ToString().Substring(0, 4) + " 년 " + this.fsGBSTDATE.ToString().Substring(4, 2) + " 월 " + this.fsGBSTDATE.ToString().Substring(6, 2) + "일부터 ";
                    sGEDDATE = sGEDDATE + this.fsGBEDDATE.ToString().Substring(0, 4) + " 년 " + this.fsGBEDDATE.ToString().Substring(4, 2) + " 월 " + this.fsGBEDDATE.ToString().Substring(6, 2) + "일까지";

                    row = Retdt.NewRow();

                    row["GSTDATE"] = sGSTDATE.ToString();
                    row["GEDDATE"] = sGEDDATE.ToString();
                    row["A1CDAC"]  = dr["A1CDAC"].ToString();
                    row["A1ABAC"]  = dr["A1ABAC"].ToString();
                    row["A1LVAC"]  = dr["A1LVAC"].ToString();
                    row["A1TAG01"] = dr["A1TAG01"].ToString();
                    row["HDRAMT"]  = dr["HDRAMT"].ToString();
                    row["HCRAMT"]  = dr["HCRAMT"].ToString();
                    row["ODRAMT"]  = dr["ODRAMT"].ToString();
                    row["OCRAMT"]  = dr["OCRAMT"].ToString();
                    row["CUNO"]    = this.TXT01_GCUNO.GetValue().ToString();
                    row["BENO"]    = this.TXT01_GBENO.GetValue().ToString();

                    Retdt.Rows.Add(row);
                }
            }

            for (int i = 0; i <= ConDt.Rows.Count - 1; i++)
            {
                sA1CDAC  = "";
                sGSTDATE = "";
                sGEDDATE = "";
                sA1LVAC  = "";

                sA1CDAC = ConDt.Rows[i]["A1CDAC"].ToString();

                if (sA1CDAC.ToString() == "41000000")
                {
                    dHR매출액 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());
                    dOR매출액 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }
                else if (sA1CDAC.ToString() == "42000000")
                {
                    dHR매출원가 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());
                    dOR매출원가 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }
                else if (sA1CDAC.ToString() == "43000000")
                {
                    dHR매출총이익 = dHR매출액 - dHR매출원가;
                    dOR매출총이익 = dOR매출액 - dOR매출원가;
                }
                else if (sA1CDAC.ToString() == "44000000")
                {
                    dHR판관비 = dHR판관비 + double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());
                    dOR판관비 = dOR판관비 + double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }
                else if (sA1CDAC.ToString() == "45000000")
                {
                    dHR영업이익 = dHR매출총이익 - dHR판관비;
                    dOR영업이익 = dOR매출총이익 - dOR판관비;
                }
                else if (sA1CDAC.ToString() == "46000000")
                {
                    dHR영업외수익 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());
                    dOR영업외수익 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }
                else if (sA1CDAC.ToString() == "47000000")
                {
                    dHR영업외비용 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());
                    dOR영업외비용 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }
                else if (sA1CDAC.ToString() == "55000000")
                {
                    dHR법인세비용차감전순이익 = dHR영업이익 + dHR영업외수익 - dHR영업외비용;
                    dOR법인세비용차감전순이익 = dOR영업이익 + dOR영업외수익 - dOR영업외비용;
                }
                else if (sA1CDAC.ToString() == "56000000")
                {
                    dHR법인세비용 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());
                    dOR법인세비용 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }
                else if (sA1CDAC.ToString() == "57000000")
                {
                    dHR당기순이익 = dHR법인세비용차감전순이익 - dHR법인세비용;
                    dOR당기순이익 = dOR법인세비용차감전순이익 - dOR법인세비용;
                }

                if (
                      (sA1CDAC.ToString() != "43000000" && sA1CDAC.ToString() != "45000000" &&
                       sA1CDAC.ToString() != "55000000" && sA1CDAC.ToString() != "57000000") 
                       &&
                      (
                       double.Parse(ConDt.Rows[i]["HDRAMT"].ToString()) == 0 && double.Parse(ConDt.Rows[i]["HCRAMT"].ToString()) == 0 &&
                       double.Parse(ConDt.Rows[i]["ODRAMT"].ToString()) == 0 && double.Parse(ConDt.Rows[i]["OCRAMT"].ToString()) == 0
                      )
                   )
                {
                }
                else
                {
                    sGSTDATE = "제 " + this.TXT01_GCUNO.GetValue().ToString() + " 기  :" + this.fsGSTDATE.ToString().Substring(0, 4) + " 년 " + this.fsGSTDATE.ToString().Substring(4, 2) + " 월 " + this.fsGSTDATE.ToString().Substring(6, 2) + "일부터 ";
                    sGSTDATE = sGSTDATE + this.fsGEDDATE.ToString().Substring(0, 4) + " 년 " + this.fsGEDDATE.ToString().Substring(4, 2) + " 월 " + this.fsGEDDATE.ToString().Substring(6, 2) + "일까지";

                    sGEDDATE = "제 " + this.TXT01_GBENO.GetValue().ToString() + " 기  :" + this.fsGBSTDATE.ToString().Substring(0, 4) + " 년 " + this.fsGBSTDATE.ToString().Substring(4, 2) + " 월 " + this.fsGBSTDATE.ToString().Substring(6, 2) + "일부터 ";
                    sGEDDATE = sGEDDATE + this.fsGBEDDATE.ToString().Substring(0, 4) + " 년 " + this.fsGBEDDATE.ToString().Substring(4, 2) + " 월 " + this.fsGBEDDATE.ToString().Substring(6, 2) + "일까지";

                    row = Retdt.NewRow();

                    row["GSTDATE"] = sGSTDATE.ToString();
                    row["GEDDATE"] = sGEDDATE.ToString();
                    row["A1CDAC"]  = sA1CDAC.ToString();

                    if (this.CKB01_AG1YNISDT.GetValue().ToString() == "Y")
                    {
                        row["A1ABAC"] = ConDt.Rows[i]["A1ABAC"].ToString();
                    }
                    else
                    {
                        // 계정코드
                        if (ConDt.Rows[i]["A1LVAC"].ToString() == "1")
                        {
                            iA1LVAC1++;

                            iA1LVAC2 = 0;

                            iA1LVAC3 = 0;

                            if (iA1LVAC1 == 1)
                            {
                                sA1LVAC = "Ⅰ";
                            }
                            else if (iA1LVAC1 == 2)
                            {
                                sA1LVAC = "Ⅱ";
                            }
                            else if (iA1LVAC1 == 3)
                            {
                                sA1LVAC = "Ⅲ";
                            }
                            else if (iA1LVAC1 == 4)
                            {
                                sA1LVAC = "Ⅳ";
                            }
                            else if (iA1LVAC1 == 5)
                            {
                                sA1LVAC = "Ⅴ";
                            }
                            else if (iA1LVAC1 == 6)
                            {
                                sA1LVAC = "Ⅵ";
                            }
                            else if (iA1LVAC1 == 7)
                            {
                                sA1LVAC = "Ⅶ";
                            }
                            else if (iA1LVAC1 == 8)
                            {
                                sA1LVAC = "Ⅷ";
                            }
                            else if (iA1LVAC1 == 9)
                            {
                                sA1LVAC = "IX";
                            }
                            else if (iA1LVAC1 == 10)
                            {
                                sA1LVAC = "Ⅹ";
                            }
                            else if (iA1LVAC1 == 11)
                            {
                                sA1LVAC = "XI";
                            }

                            sA1ABAC = sA1LVAC.ToString() + ".     " + ConDt.Rows[i]["A1ABAC"].ToString();

                        }
                        else if (ConDt.Rows[i]["A1LVAC"].ToString() == "2")
                        {
                            iA1LVAC2++;

                            iA1LVAC3 = 0;

                            sA1LVAC = "  (" + Convert.ToString(iA1LVAC2) + ")";
                            sA1ABAC = sA1LVAC.ToString() + "   " + ConDt.Rows[i]["A1ABAC"].ToString();
                        }
                        else
                        {
                            iA1LVAC3++;

                            sA1LVAC = "     " + Convert.ToString(iA1LVAC3) + ".";

                            sA1ABAC = sA1LVAC.ToString() + "   " + ConDt.Rows[i]["A1ABAC"].ToString();
                        }

                        row["A1ABAC"] = sA1ABAC.ToString();
                    }

                    row["A1LVAC"]  = ConDt.Rows[i]["A1LVAC"].ToString();
                    row["A1TAG01"] = ConDt.Rows[i]["A1TAG01"].ToString();
                    row["HDRAMT"]  = string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["HDRAMT"].ToString()));
                    row["ODRAMT"]  = string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["ODRAMT"].ToString()));

                    if (sA1CDAC.ToString() == "43000000")
                    {
                        row["HCRAMT"] = string.Format("{0:#,###}", dHR매출총이익);
                        row["OCRAMT"] = string.Format("{0:#,###}", dOR매출총이익);
                    }
                    else if (sA1CDAC.ToString() == "45000000")
                    {
                        row["HCRAMT"] = string.Format("{0:#,###}", dHR영업이익);
                        row["OCRAMT"] = string.Format("{0:#,###}", dOR영업이익);
                    }
                    else if (sA1CDAC.ToString() == "55000000")
                    {
                        row["HCRAMT"] = string.Format("{0:#,###}", dHR법인세비용차감전순이익);
                        row["OCRAMT"] = string.Format("{0:#,###}", dOR법인세비용차감전순이익);
                    }
                    else if (sA1CDAC.ToString() == "57000000")
                    {
                        row["HCRAMT"] = string.Format("{0:#,###}", dHR당기순이익);
                        row["OCRAMT"] = string.Format("{0:#,###}", dOR당기순이익);
                    }
                    else
                    {
                        row["HCRAMT"] = string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["HCRAMT"].ToString()));
                        row["OCRAMT"] = string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["OCRAMT"].ToString()));
                    }

                    row["CUNO"] = this.TXT01_GCUNO.GetValue().ToString();
                    row["BENO"] = this.TXT01_GBENO.GetValue().ToString();

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion
    }
}