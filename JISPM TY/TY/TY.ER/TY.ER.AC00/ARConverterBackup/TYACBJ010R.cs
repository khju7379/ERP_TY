using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.SectionReportModel;
using System.Data;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACBJ010R.
    /// </summary>
    public partial class TYACBJ010R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable Retdt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private string _sA1LVAC  = string.Empty;
        private string _sA1TAG10 = string.Empty;

        private int _iA1LVAC2 = 0;
        private int _iA1LVAC3 = 0;
        private int _iA1LVAC4 = 0;
        private int _iA1LVAC5 = 0;
        private int _iA1LVAC6 = 0;

        private double _fd33070200_HC = 0;
        private double _fd33070200_OC = 0;

        private double _fd당기대변금액 = 0;
        private double _fd전기대변금액 = 0;

        private double _fd당기부채자본금액 = 0;
        private double _fd전기부채자본금액 = 0;

        private double _d당기부채자본금액 = 0;
        private double _d전기부채자본금액 = 0;

        // 당기
        private double _fdHRDRAMT = 0;

        // 전기
        private double _fdORDRAMT = 0;

        // 당기
        private double _fdHRCRAMT = 0;

        // 전기
        private double _fdORCRAMT = 0;

        private int _iPageCount   = 0;
        private int _iRecordCount = 0;
        private int _iCount       = 0;

        private string _fsGSTDATE = string.Empty;
        private string _fsGEDDATE = string.Empty;

        private string sA1CDAC      = string.Empty;
        private string sA1LVAC      = string.Empty;
        private string sNEW_A1TAG10 = string.Empty;

        public TYACBJ010R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //sA1CDAC = "";
            //sA1LVAC = "";
            //sNEW_A1TAG10 = "";

            this._iCount++;
            this.line1.Visible = false;
            this.line3.Visible = false;
            this.line5.Visible = false;

            this.line6.Visible = false;
            this.line7.Visible = false;

            this.A1ABAC.Alignment = TextAlignment.Left;

            _fd당기대변금액 = 0;
            _fd전기대변금액 = 0;

            this.GSTDATE.Text = _fsGSTDATE.ToString();
            this.GEDDATE.Text = _fsGEDDATE.ToString();

            if (_iCount - 1 < Retdt.Rows.Count)
            {
                this.A1ABAC.Font = new Font("바탕체", 9); 

                sA1CDAC  = Convert.ToString(Retdt.Rows[_iCount - 1]["A1CDAC"]);

                sA1LVAC  = Convert.ToString(Retdt.Rows[_iCount - 1]["A1LVAC"]);

                sNEW_A1TAG10 = Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]);

                // 계정코드
                if (sA1LVAC.ToString() == "2")
                {
                    this._iA1LVAC2++;

                    this._iA1LVAC3 = 0;

                    this._iA1LVAC5 = 0;

                    if (this._iA1LVAC2 == 1)
                    {
                        this._sA1LVAC = "Ⅰ";
                    }
                    else if (this._iA1LVAC2 == 2)
                    {
                        this._sA1LVAC = "Ⅱ";
                    }
                    else if (this._iA1LVAC2 == 3)
                    {
                        this._sA1LVAC = "Ⅲ";
                    }
                    else if (this._iA1LVAC2 == 4)
                    {
                        this._sA1LVAC = "Ⅳ";
                    }
                    else if (this._iA1LVAC2 == 5)
                    {
                        this._sA1LVAC = "Ⅴ";
                    }
                    else if (this._iA1LVAC2 == 6)
                    {
                        this._sA1LVAC = "Ⅵ";
                    }

                    this.A1ABAC.Text = this._sA1LVAC.ToString() + ".     " + Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();

                }
                else if (sA1LVAC.ToString() == "3")
                {
                    this._iA1LVAC3++;

                    this._iA1LVAC5 = 0;

                    this._sA1LVAC = "  (" + Convert.ToString(this._iA1LVAC3) + ")";
                    this.A1ABAC.Text = this._sA1LVAC.ToString() + "   " + Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();
                }
                else if (sA1LVAC.ToString() == "5")
                {
                    if (Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]) != "2")
                    {
                        this._iA1LVAC5++;

                        this._sA1LVAC = "        " + Convert.ToString(this._iA1LVAC5) + ".";

                        this.A1ABAC.Text = this._sA1LVAC.ToString() + "   " + Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();
                    }
                    else
                    {
                        this.A1ABAC.Text = "              " + Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();
                    }
                }
                else if (sA1LVAC.ToString() == "6")
                {
                    this._iA1LVAC6++;

                    this._sA1LVAC = "           " + Convert.ToString(this._iA1LVAC6) + ".";

                    this.A1ABAC.Text = this._sA1LVAC.ToString() + "   " + Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();
                }
                else if (sA1LVAC.ToString() == "9")
                {
                    this._iA1LVAC6++;

                    this._sA1LVAC = "           ";

                    this.A1ABAC.Text = this._sA1LVAC.ToString() + "   " + Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();
                }
                else
                {
                    this._sA1LVAC = "";

                    this._iA1LVAC2 = 0;
                    this._iA1LVAC3 = 0;
                    this._iA1LVAC4 = 0;
                    this._iA1LVAC5 = 0;
                    this._iA1LVAC6 = 0;

                    this.A1ABAC.Font = new Font("바탕체", 10, FontStyle.Bold); 

                    // 계정과목
                    this.A1ABAC.Text = Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();
                }

                // 계정과목 정렬
                if (Convert.ToString(sA1LVAC) == "1" && Convert.ToString(Retdt.Rows[_iCount - 1]["A1YNBS"]) == "Y")
                {
                    this.A1ABAC.Alignment = TextAlignment.Center;
                }

                this.HDRAMT.Text = "";
                this.HCRAMT.Text = "";

                this.ODRAMT.Text = "";
                this.OCRAMT.Text = "";

                // 충당금 구분 = 1이면서 충당금 계정에 따른 값이 없을 경우
                if (Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]) == "1" && Convert.ToString(Retdt.Rows[_iCount - 1]["DSP"]) == "C")
                {
                    this.HDRAMT.Text = "";
                    this.ODRAMT.Text = "";
                }
                else
                {
                    this.HDRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["HDRAMT"].ToString()));
                    this.ODRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["ODRAMT"].ToString()));
                }

                // 충당금 계정일 경우
                if (Convert.ToString(_sA1TAG10) == "1" && Convert.ToString(sNEW_A1TAG10) == "2")
                {
                    _sA1TAG10 = sNEW_A1TAG10;

                    // 당기 = (A1TAG10 = '1'인 차변값) - (A1TAG10 = '2'인 차변값)
                    this.HCRAMT.Text = string.Format("{0:#,###}", _fdHRDRAMT - double.Parse(Retdt.Rows[_iCount - 1]["HDRAMT"].ToString()));
                    // 전기 = (A1TAG10 = '1'인 차변값) - (A1TAG10 = '2'인 차변값)
                    this.OCRAMT.Text = string.Format("{0:#,###}", _fdORDRAMT - double.Parse(Retdt.Rows[_iCount - 1]["ODRAMT"].ToString()));

                    this._fdHRDRAMT = 0;

                    this._fdORDRAMT = 0;

                    this._fdHRCRAMT = double.Parse(this.HCRAMT.Text);

                    this._fdORCRAMT = double.Parse(this.OCRAMT.Text);
                }
                else if (Convert.ToString(_sA1TAG10) == "2" && Convert.ToString(sNEW_A1TAG10) == "2")
                {
                    _sA1TAG10 = sNEW_A1TAG10;

                    // 당기 = (A1TAG10 = '2'인 대변값) - (A1TAG10 = '2'인 차변값)
                    this.HCRAMT.Text = string.Format("{0:#,###}", _fdHRCRAMT - double.Parse(Retdt.Rows[_iCount - 1]["HDRAMT"].ToString()));
                    // 전기 = (A1TAG10 = '2'인 대변값) - (A1TAG10 = '2'인 차변값)
                    this.OCRAMT.Text = string.Format("{0:#,###}", _fdORCRAMT - double.Parse(Retdt.Rows[_iCount - 1]["ODRAMT"].ToString()));

                    //this._fdHRDRAMT = 0;

                    //this._fdORDRAMT = 0;

                    //this._fdHRCRAMT = 0;

                    //this._fdORCRAMT = 0;
                }
                else
                {
                    this._fdHRDRAMT = 0;

                    this._fdORDRAMT = 0;

                    this._fdHRCRAMT = 0;

                    this._fdORCRAMT = 0;

                    _sA1TAG10 = sNEW_A1TAG10;

                    // 충당금 구분 = 1이면서 충당금 계정에 따른 값이 없을 경우
                    if (Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]) == "1" && Convert.ToString(Retdt.Rows[_iCount - 1]["DSP"]) == "C")
                    {
                        this.HCRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["HDRAMT"].ToString()));
                        this.OCRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["ODRAMT"].ToString()));
                    }
                    else
                    {
                        // 당기
                        this.HCRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["HCRAMT"].ToString()));
                        // 전기
                        this.OCRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["OCRAMT"].ToString()));
                    }

                    // 당기
                    this._fdHRDRAMT = double.Parse(Retdt.Rows[_iCount - 1]["HDRAMT"].ToString());

                    // 전기
                    this._fdORDRAMT = double.Parse(Retdt.Rows[_iCount - 1]["ODRAMT"].ToString());

                    // 당기
                    this._fdHRCRAMT = double.Parse(Retdt.Rows[_iCount - 1]["HCRAMT"].ToString());

                    // 전기
                    this._fdORCRAMT = double.Parse(Retdt.Rows[_iCount - 1]["OCRAMT"].ToString());
                }


                // 충당금 계정일 경우 라인 보이게 함
                if (Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]) == "2")
                {
                    this.line1.Visible = true;
                    this.line3.Visible = true;
                }

                // 계정별 소계
                if (Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]) == "10")
                {
                    this.line6.Visible = true;
                    this.line7.Visible = true;
                }

                if (this._iPageCount == 0)
                {
                    // 레코드가 36이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
                    if (this._rowCount == 36)
                    {
                        this._rowCount = 0;
                        this._iPageCount++;

                        this.line5.Visible = true;

                        // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                        this.detail.NewPage = NewPage.After;
                    }
                    else
                    {
                        this._rowCount++;

                        // 현재 페이지에 레코드를 인쇄해라.
                        this.detail.NewPage = NewPage.None;
                    }
                }
                else
                {
                    // 레코드가 41이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
                    if (this._rowCount == 41)
                    {
                        this._rowCount = 0;
                        this._iPageCount++;

                        this.line5.Visible = true;

                        // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                        this.detail.NewPage = NewPage.After;
                    }
                    else
                    {
                        this._rowCount++;

                        // 현재 페이지에 레코드를 인쇄해라.
                        this.detail.NewPage = NewPage.None;
                    }
                }
            }
            else
            {
                this.detail.Visible = false;
            }

            if (_iRecordCount == _iCount)
            {
                this.line5.Visible = true;
            }
        }

        private void TYACBJ010R_DataInitialize(object sender, EventArgs e)
        {
            string sNEWA1CDAC = string.Empty;
            string sOLDA1CDAC = string.Empty;

            string sNEWA1ABAC = string.Empty;
            string sOLDA1ABAC = string.Empty;

            double dOUT_HCRAMT = 0;
            double dOUT_OCRAMT = 0;

            string sCUNO = string.Empty;
            string sFDT  = string.Empty;
            string sBENO = string.Empty;
            string sTDT  = string.Empty;
            string sGBN  = string.Empty;

            double dHCRAMT = 0;
            double dOCRAMT = 0;

            _fd당기대변금액     = 0;
            _fd전기대변금액     = 0;

            _d당기부채자본금액  = 0;
            _d전기부채자본금액  = 0;

            _fd당기부채자본금액 = 0;
            _fd전기부채자본금액 = 0;


            DataTable dt = (DataTable)this.DataSource;

            DataTable ConDt = new DataTable();

            ConDt = dt;

            if (dt != null)
            {                
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["A1CDAC"]) == "20000000" || Convert.ToString(dr["A1CDAC"]) == "30000000")
                    {
                        this._d당기부채자본금액 = this._d당기부채자본금액 + double.Parse(dr["HCRAMT"].ToString());
                        this._d전기부채자본금액 = this._d전기부채자본금액 + double.Parse(dr["OCRAMT"].ToString());
                    }

                    if (Convert.ToString(dr["A1CDAC"]) == "36200200")  // 33070200 (당기순이익)
                    {
                        this._fd33070200_HC = this._fd33070200_HC + double.Parse(dr["HCRAMT"].ToString());
                        this._fd33070200_OC = this._fd33070200_OC + double.Parse(dr["OCRAMT"].ToString());
                    }
                }

                this._fd당기부채자본금액 = this._d당기부채자본금액 + this._fd33070200_HC;

                this._fd전기부채자본금액 = this._d전기부채자본금액 + this._fd33070200_OC;

                DataRow row;

                Retdt.Columns.Add("GSTDATE", typeof(System.String));
                Retdt.Columns.Add("GEDDATE", typeof(System.String));
                Retdt.Columns.Add("A1CDAC",  typeof(System.String));
                Retdt.Columns.Add("A1LVAC",  typeof(System.String));
                Retdt.Columns.Add("A1ABAC",  typeof(System.String));
                Retdt.Columns.Add("A1TAG10", typeof(System.String));
                Retdt.Columns.Add("A1YNBS",  typeof(System.String));
                Retdt.Columns.Add("A1YNTB",  typeof(System.String));
                Retdt.Columns.Add("HDRAMT",  typeof(System.String));
                Retdt.Columns.Add("HCRAMT",  typeof(System.String));
                Retdt.Columns.Add("ODRAMT",  typeof(System.String));
                Retdt.Columns.Add("OCRAMT",  typeof(System.String));
                Retdt.Columns.Add("CUNO",    typeof(System.String));
                Retdt.Columns.Add("FDT",     typeof(System.String));
                Retdt.Columns.Add("TDT",     typeof(System.String));
                Retdt.Columns.Add("GBN",     typeof(System.String));
                Retdt.Columns.Add("DSP",     typeof(System.String)); // A1TAG10 = 1이면서 충당금계정이 0일 경우 대변에 DISPLAY하기위한 필드

                for (int i = 0; i <= ConDt.Rows.Count - 1; i++)
                {
                    _fd당기대변금액 = 0;
                    _fd전기대변금액 = 0;

                    sCUNO = ConDt.Rows[i]["CUNO"].ToString();
                    sFDT  = ConDt.Rows[i]["FDT"].ToString();
                    sBENO = ConDt.Rows[i]["BENO"].ToString();
                    sTDT  = ConDt.Rows[i]["TDT"].ToString();
                    sGBN  = ConDt.Rows[i]["GBN"].ToString();

                    sNEWA1CDAC = Convert.ToString(ConDt.Rows[i]["A1CDAC"]);
                    sNEWA1ABAC = ConDt.Rows[i]["A1ABAC"].ToString();

                    // 충당금 계정일 경우
                    //if (Convert.ToString(ConDt.Rows[i]["A1TAG10"]) == "2")
                    //{
                    //    // 당기 = 이전차변 - 현재차변
                    //    _fd당기대변금액 = double.Parse(ConDt.Rows[i-1]["HDRAMT"].ToString()) - double.Parse(ConDt.Rows[i]["HDRAMT"].ToString());
                    //    // 전기 = 이전차변 - 현재차변
                    //    _fd전기대변금액 = double.Parse(ConDt.Rows[i-1]["ODRAMT"].ToString()) - double.Parse(ConDt.Rows[i]["ODRAMT"].ToString());
                    //}
                    //else
                    if (sNEWA1CDAC.ToString() == "36200200")   // 33070200 (당기순이익)
                    {
                        _fd당기대변금액 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());

                        _fd전기대변금액 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                    }
                    else if (sNEWA1CDAC.ToString() == "30000000" || sNEWA1CDAC.ToString() == "33000000") // || sNEWA1CDAC.ToString() == "33070000")
                    {
                        _fd당기대변금액 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString()) + _fd33070200_HC;

                        _fd전기대변금액 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString()) + _fd33070200_OC;
                    }
                    else
                    {
                        _fd당기대변금액 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());

                        _fd전기대변금액 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                    }

                    if (
                          (ConDt.Rows[i]["A1YNBS"].ToString() != "Y") ||
                          (
                        //dr["A1TAG10"].ToString() != "2" &&
                             double.Parse(ConDt.Rows[i]["HDRAMT"].ToString()) == 0 && this._fd당기대변금액 == 0 &&
                             double.Parse(ConDt.Rows[i]["ODRAMT"].ToString()) == 0 && this._fd전기대변금액 == 0
                          )
                       )
                    {
                    }
                    else
                    {
                        dOUT_HCRAMT = double.Parse(ConDt.Rows[i]["OUT_HCRAMT"].ToString());
                        dOUT_OCRAMT = double.Parse(ConDt.Rows[i]["OUT_OCRAMT"].ToString());

                        _fsGSTDATE = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                        _fsGEDDATE = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";

                        if (ConDt.Rows[i]["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                        {
                            if (sOLDA1CDAC == "")
                            {
                                sOLDA1CDAC = sNEWA1CDAC.ToString();
                                sOLDA1ABAC = sNEWA1ABAC.ToString();
                            }

                            if (sOLDA1CDAC != "" && (sNEWA1CDAC != sOLDA1CDAC))
                            {
                                // 레벨 = 1, A1TAG10 = 10, A1YNBS = Y

                                row = Retdt.NewRow();

                                row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                                row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                                row["A1CDAC"]  = sOLDA1CDAC.ToString();
                                row["A1LVAC"]  = "1";
                                row["A1ABAC"]  = sOLDA1ABAC.ToString() + "총계";
                                row["A1TAG10"] = "10";
                                row["A1YNBS"]  = "Y";
                                row["A1YNTB"]  = "";
                                row["HDRAMT"]  = "0";
                                row["HCRAMT"]  = string.Format("{0:#,###}", Convert.ToString(dHCRAMT));
                                row["ODRAMT"]  = "0";
                                row["OCRAMT"]  = string.Format("{0:#,###}", Convert.ToString(dOCRAMT));
                                row["CUNO"]    = sCUNO.ToString();
                                row["FDT"]     = sFDT.ToString();
                                row["TDT"]     = sTDT.ToString();
                                row["GBN"]     = sGBN.ToString();
                                row["DSP"]     = "";

                                Retdt.Rows.Add(row);

                                sOLDA1CDAC = sNEWA1CDAC.ToString();
                                sOLDA1ABAC = sNEWA1ABAC.ToString();
                            }
                        }

                        row = Retdt.NewRow();

                        row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                        row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                        row["A1CDAC"]  = ConDt.Rows[i]["A1CDAC"].ToString();
                        row["A1LVAC"]  = ConDt.Rows[i]["A1LVAC"].ToString();
                        row["A1ABAC"]  = ConDt.Rows[i]["A1ABAC"].ToString();
                        row["A1TAG10"] = ConDt.Rows[i]["A1TAG10"].ToString();
                        row["A1YNBS"]  = ConDt.Rows[i]["A1YNBS"].ToString();
                        row["A1YNTB"]  = ConDt.Rows[i]["A1YNTB"].ToString();

                        row["HDRAMT"]  = string.Format("{0:#,###}", ConDt.Rows[i]["HDRAMT"].ToString());

                        if (ConDt.Rows[i]["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                        {
                            dHCRAMT = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());

                            row["HCRAMT"] = "0";
                        }
                        else
                        {
                            row["HCRAMT"] = string.Format("{0:#,###}", ConDt.Rows[i]["HCRAMT"].ToString());
                        }

                        row["ODRAMT"] = string.Format("{0:#,###}", ConDt.Rows[i]["ODRAMT"].ToString());

                        if (ConDt.Rows[i]["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                        {
                            dOCRAMT = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());

                            row["OCRAMT"] = "0";
                        }
                        else
                        {
                            row["OCRAMT"] = string.Format("{0:#,###}", ConDt.Rows[i]["OCRAMT"].ToString());
                        }

                        row["CUNO"] = sCUNO.ToString();
                        row["FDT"]  = sFDT.ToString();
                        row["TDT"]  = sTDT.ToString();
                        row["GBN"]  = sGBN.ToString();

                        if (ConDt.Rows[i]["A1TAG10"].ToString() == "1")
                        {
                            if (ConDt.Rows[i + 1]["A1TAG10"].ToString() == "2")
                            {
                                if (double.Parse(ConDt.Rows[i + 1]["HDRAMT"].ToString()) == 0 &&
                                    double.Parse(ConDt.Rows[i + 1]["ODRAMT"].ToString()) == 0)
                                {
                                    row["DSP"] = "C";
                                }
                                else
                                {
                                    row["DSP"] = "";
                                }
                            }
                            else
                            {
                                row["DSP"] = "";
                            }
                        }
                        else
                        {
                            row["DSP"] = "";
                        }
                        

                        Retdt.Rows.Add(row);
                    }
                }

                // 원본소스

                //foreach (DataRow dr in dt.Rows)
                //{
                //    sCUNO = dr["CUNO"].ToString();
                //    sFDT = dr["FDT"].ToString();
                //    sBENO = dr["BENO"].ToString();
                //    sTDT = dr["TDT"].ToString();
                //    sGBN = dr["GBN"].ToString();

                //    sNEWA1CDAC = Convert.ToString(dr["A1CDAC"]);
                //    sNEWA1ABAC = dr["A1ABAC"].ToString();

                //    // 충당금 계정일 경우
                //    if (Convert.ToString(dr["A1TAG10"]) == "2")
                //    {
                //        // 당기
                //        _fd당기대변금액 = _fdHRDRAMT - double.Parse(dr["HDRAMT"].ToString());
                //        // 전기
                //        _fd전기대변금액 = _fdORDRAMT - double.Parse(dr["ODRAMT"].ToString());
                //    }
                //    else if (sNEWA1CDAC.ToString() == "33070200")
                //    {
                //        _fd당기대변금액 = double.Parse(dr["HCRAMT"].ToString());

                //        _fd전기대변금액 = double.Parse(dr["OCRAMT"].ToString());
                //    }
                //    else if (sNEWA1CDAC.ToString() == "30000000" || sNEWA1CDAC.ToString() == "33000000" || sNEWA1CDAC.ToString() == "33070000")
                //    {
                //        _fd당기대변금액 = double.Parse(dr["HCRAMT"].ToString()) + _fd33070200_HC;

                //        _fd전기대변금액 = double.Parse(dr["OCRAMT"].ToString()) + _fd33070200_OC;
                //    }
                //    else
                //    {
                //        _fd당기대변금액 = double.Parse(dr["HCRAMT"].ToString());

                //        _fd전기대변금액 = double.Parse(dr["OCRAMT"].ToString());
                //    }

                //    if (
                //          (dr["A1YNBS"].ToString() != "Y") ||
                //          (
                //        //dr["A1TAG10"].ToString() != "2" &&
                //             double.Parse(dr["HDRAMT"].ToString()) == 0 && this._fd당기대변금액 == 0 &&
                //             double.Parse(dr["ODRAMT"].ToString()) == 0 && this._fd전기대변금액 == 0
                //          )
                //       )
                //    {
                //    }
                //    else
                //    {
                //        dOUT_HCRAMT = double.Parse(dr["OUT_HCRAMT"].ToString());
                //        dOUT_OCRAMT = double.Parse(dr["OUT_OCRAMT"].ToString());

                //        _fsGSTDATE = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                //        _fsGEDDATE = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";

                //        if (dr["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                //        {
                //            if (sOLDA1CDAC == "")
                //            {
                //                sOLDA1CDAC = sNEWA1CDAC.ToString();
                //                sOLDA1ABAC = sNEWA1ABAC.ToString();
                //            }

                //            if (sOLDA1CDAC != "" && (sNEWA1CDAC != sOLDA1CDAC))
                //            {
                //                // 레벨 = 1, A1TAG10 = 10, A1YNBS = Y

                //                row = Retdt.NewRow();

                //                row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                //                row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                //                row["A1CDAC"] = sOLDA1CDAC.ToString();
                //                row["A1LVAC"] = "1";
                //                row["A1ABAC"] = sOLDA1ABAC.ToString() + "총계";
                //                row["A1TAG10"] = "10";
                //                row["A1YNBS"] = "Y";
                //                row["A1YNTB"] = "";
                //                row["HDRAMT"] = "0";
                //                row["HCRAMT"] = string.Format("{0:#,###}", Convert.ToString(dHCRAMT));
                //                row["ODRAMT"] = "0";
                //                row["OCRAMT"] = string.Format("{0:#,###}", Convert.ToString(dOCRAMT));
                //                row["CUNO"] = sCUNO.ToString();
                //                row["FDT"] = sFDT.ToString();
                //                row["TDT"] = sTDT.ToString();
                //                row["GBN"] = sGBN.ToString();

                //                Retdt.Rows.Add(row);

                //                sOLDA1CDAC = sNEWA1CDAC.ToString();
                //                sOLDA1ABAC = sNEWA1ABAC.ToString();
                //            }
                //        }

                //        row = Retdt.NewRow();

                //        row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                //        row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                //        row["A1CDAC"] = dr["A1CDAC"].ToString();
                //        row["A1LVAC"] = dr["A1LVAC"].ToString();
                //        row["A1ABAC"] = dr["A1ABAC"].ToString();
                //        row["A1TAG10"] = dr["A1TAG10"].ToString();
                //        row["A1YNBS"] = dr["A1YNBS"].ToString();
                //        row["A1YNTB"] = dr["A1YNTB"].ToString();

                //        row["HDRAMT"] = string.Format("{0:#,###}", dr["HDRAMT"].ToString());

                //        if (dr["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                //        {
                //            dHCRAMT = double.Parse(dr["HCRAMT"].ToString());

                //            row["HCRAMT"] = "0";
                //        }
                //        else
                //        {
                //            row["HCRAMT"] = string.Format("{0:#,###}", dr["HCRAMT"].ToString());
                //        }

                //        row["ODRAMT"] = string.Format("{0:#,###}", dr["ODRAMT"].ToString());

                //        if (dr["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                //        {
                //            dOCRAMT = double.Parse(dr["OCRAMT"].ToString());

                //            row["OCRAMT"] = "0";
                //        }
                //        else
                //        {
                //            row["OCRAMT"] = string.Format("{0:#,###}", dr["OCRAMT"].ToString());
                //        }

                //        row["CUNO"] = sCUNO.ToString();
                //        row["FDT"] = sFDT.ToString();
                //        row["TDT"] = sTDT.ToString();
                //        row["GBN"] = sGBN.ToString();

                //        Retdt.Rows.Add(row);
                //    }
                //}

                #region Description : 당기 순이익

                row = Retdt.NewRow();

                row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                row["A1CDAC"]  = "";
                row["A1LVAC"]  = "9";
                row["A1ABAC"]  = "(당기순이익 : ";
                row["A1TAG10"] = "9";
                row["A1YNBS"]  = "Y";
                row["A1YNTB"]  = "";
                row["HDRAMT"]  = "0";
                row["HCRAMT"]  = "0";
                row["ODRAMT"]  = "0";
                row["OCRAMT"]  = "0";
                row["CUNO"]    = sCUNO.ToString();
                row["FDT"]     = sFDT.ToString();
                row["TDT"]     = sTDT.ToString();
                row["GBN"]     = sGBN.ToString();
                row["DSP"]     = "";

                Retdt.Rows.Add(row);

                #endregion

                #region Description : 당기

                row = Retdt.NewRow();

                row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                row["A1CDAC"]  = "";
                row["A1LVAC"]  = "9";
                row["A1ABAC"]  = "당기 : " + string.Format("{0:#,###}", dOUT_HCRAMT) + "원";
                row["A1TAG10"] = "9";
                row["A1YNBS"]  = "Y";
                row["A1YNTB"]  = "";
                row["HDRAMT"]  = "0";
                row["HCRAMT"]  = "0";
                row["ODRAMT"]  = "0";
                row["OCRAMT"]  = "0";
                row["CUNO"]    = sCUNO.ToString();
                row["FDT"]     = sFDT.ToString();
                row["TDT"]     = sTDT.ToString();
                row["GBN"]     = sGBN.ToString();
                row["DSP"]     = "";

                Retdt.Rows.Add(row);

                #endregion

                #region Description : 전기

                row = Retdt.NewRow();

                row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                row["A1CDAC"]  = "";
                row["A1LVAC"]  = "9";
                row["A1ABAC"]  = "전기 : " + string.Format("{0:#,###}", dOUT_OCRAMT) + "원)";
                row["A1TAG10"] = "9";
                row["A1YNBS"]  = "Y";
                row["A1YNTB"]  = "";
                row["HDRAMT"]  = "0";
                row["HCRAMT"]  = "0";
                row["ODRAMT"]  = "0";
                row["OCRAMT"]  = "0";
                row["CUNO"]    = sCUNO.ToString();
                row["FDT"]     = sFDT.ToString();
                row["TDT"]     = sTDT.ToString();
                row["GBN"]     = sGBN.ToString();
                row["DSP"]     = "";

                Retdt.Rows.Add(row);

                #endregion

                // 레벨 = 1, A1TAG10 = 10

                row = Retdt.NewRow();

                row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                row["A1CDAC"]  = sOLDA1CDAC.ToString();
                row["A1LVAC"]  = "1";
                row["A1ABAC"]  = sOLDA1ABAC.ToString() + "총계";
                row["A1TAG10"] = "10";
                row["A1YNBS"]  = "Y";
                row["A1YNTB"]  = "";
                row["HDRAMT"]  = "0";
                row["HCRAMT"]  = Convert.ToString(dHCRAMT);
                row["ODRAMT"]  = "0";
                row["OCRAMT"]  = Convert.ToString(dOCRAMT);
                row["CUNO"]    = sCUNO.ToString();
                row["FDT"]     = sFDT.ToString();
                row["TDT"]     = sTDT.ToString();
                row["GBN"]     = sGBN.ToString();
                row["DSP"]     = "";

                Retdt.Rows.Add(row);

                row = Retdt.NewRow();

                row["GSTDATE"] = "제 " + sCUNO.ToString() + " 기  :" + sFDT.ToString().Substring(0, 4) + " 년 " + sFDT.ToString().Substring(4, 2) + " 월 " + sFDT.ToString().Substring(6, 2) + " 현재";
                row["GEDDATE"] = "제 " + sBENO.ToString() + " 기  :" + sTDT.ToString().Substring(0, 4) + " 년 " + sTDT.ToString().Substring(4, 2) + " 월 " + sTDT.ToString().Substring(6, 2) + " 현재";
                row["A1CDAC"]  = sOLDA1CDAC.ToString();
                row["A1LVAC"]  = "1";
                row["A1ABAC"]  = "부채와 자본 총계";
                row["A1TAG10"] = "10";
                row["A1YNBS"]  = "Y";
                row["A1YNTB"]  = "";
                row["HDRAMT"]  = "0";
                row["HCRAMT"]  = Convert.ToString(_fd당기부채자본금액);
                row["ODRAMT"]  = "0";
                row["OCRAMT"]  = Convert.ToString(_fd전기부채자본금액);
                row["CUNO"]    = sCUNO.ToString();
                row["FDT"]     = sFDT.ToString();
                row["TDT"]     = sTDT.ToString();
                row["GBN"]     = sGBN.ToString();
                row["DSP"]     = "";

                Retdt.Rows.Add(row);
            }

            _iRecordCount = Retdt.Rows.Count;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            this.GSTDATE.Text = _fsGSTDATE.ToString();
            this.GEDDATE.Text = _fsGEDDATE.ToString();
        }        
    }
}