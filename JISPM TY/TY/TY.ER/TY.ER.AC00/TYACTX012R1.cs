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
    /// Summary description for TYACTX012R1.
    /// </summary>
    public partial class TYACTX012R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private DataTable _dt2 = new DataTable();

        private int _RowCount = 0;

        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;
        private string _sStdate = string.Empty;
        private string _sEddate = string.Empty;
        private string _sTAXGUBN = string.Empty;


        private double _fiNowPage = 1;

        public TYACTX012R1(DataTable dt, DataTable dt2, string sYEAR, string sRPTGUBN, string sCONFGB, string sStdate, string sEddate, string sTAXGUBN)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this._dt = dt;
            this._dt2 = dt2;

            this._sYEAR = sYEAR;
            this._sRPTGUBN = sRPTGUBN;
            this._sCONFGB = sCONFGB;
            this._sStdate = sStdate;
            this._sEddate = sEddate;
            this._sTAXGUBN = sTAXGUBN;
        }

        private void TYACTX012R1_ReportStart(object sender, EventArgs e)
        {
            this.pageHeader.Visible = false;
            this.pageFooter.Visible = false;

            DataTable dt = this.DataSource as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 5)
                {
                    TYACTX012R2 subReport = new TYACTX012R2();
                    subReport.DataSource = dt;
                    TYACTX012R2.Report = subReport;
                }
            }
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            if (_sTAXGUBN == "1")
            {
                this.STEDDATE.Value = _sStdate.Substring(0, 4) + "년 " + _sStdate.Substring(4, 2) + "월 " + _sStdate.Substring(6, 2) + "일 ~ " + _sEddate.Substring(0, 4) + "년 " + _sEddate.Substring(4, 2) + "월 " + _sEddate.Substring(6, 2) + "일";

                if (_sRPTGUBN == "1")
                {
                    if (_sCONFGB == "1")
                    {
                        this.DATE.Value = _sYEAR + "년 3월 31일";
                    }
                    else
                    {
                        this.DATE.Value = _sYEAR + "년 6월 30일";
                    }
                }
                else
                {
                    if (_sCONFGB == "1")
                    {
                        this.DATE.Value = _sYEAR + "년 9월 30일";
                    }
                    else
                    {
                        this.DATE.Value = _sYEAR + "년 12월 31일";
                    }
                }
            }
            else
            {
                if (_sRPTGUBN == "1")
                {
                    if (_sCONFGB == "1")
                    {
                        this.STEDDATE.Value = _sYEAR + "년 1월 1일 ~ " + _sYEAR + "년 3월 31일";
                        this.DATE.Value = _sYEAR + "년 3월 31일";
                    }
                    else
                    {
                        this.STEDDATE.Value = _sYEAR + "년 4월 1일 ~ " + _sYEAR + "년 6월 30일";
                        this.DATE.Value = _sYEAR + "년 6월 30일";
                    }
                }
                else
                {
                    if (_sCONFGB == "1")
                    {
                        this.STEDDATE.Value = _sYEAR + "년 7월 1일 ~ " + _sYEAR + "년 9월 30일";
                        this.DATE.Value = _sYEAR + "년 9월 30일";
                    }
                    else
                    {
                        this.STEDDATE.Value = _sYEAR + "년 10월 1일 ~ " + _sYEAR + "년 12월 31일";
                        this.DATE.Value = _sYEAR + "년 12월 31일";
                    }
                }
            }

            this.S1RPTGUBN.Value = _sRPTGUBN;
            this.S1YEAR.Value = _sYEAR;
            this.S1RPTGUBN2.Value = _sRPTGUBN;
            this.S1YEAR2.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSAUPNO2.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt2.Rows[0]["ASMSANGHO"].ToString();
            this.ASMNAMENM.Value = _dt2.Rows[0]["ASMNAMENM"].ToString();
            this.ASMVNADDRS.Value = _dt2.Rows[0]["ASMVNADDRS"].ToString();

            this.VNCODE_CNT1.Value = _dt.Rows[0]["VNCODE_CNT"].ToString();
            this.MAESU_CNT1.Value = _dt.Rows[0]["MAESU_CNT"].ToString();
            this.HAP_AMT1.Value = _dt.Rows[0]["HAP_AMT"].ToString();

            this.VNCODE_CNT2.Value = _dt.Rows[3]["VNCODE_CNT"].ToString();
            this.MAESU_CNT2.Value = _dt.Rows[3]["MAESU_CNT"].ToString();
            this.HAP_AMT2.Value = _dt.Rows[3]["HAP_AMT"].ToString();

            this.VNCODE_CNT3.Value = _dt.Rows[6]["VNCODE_CNT"].ToString();
            this.MAESU_CNT3.Value = _dt.Rows[6]["MAESU_CNT"].ToString();
            this.HAP_AMT3.Value = _dt.Rows[6]["HAP_AMT"].ToString();

        }

        private void detail_Format(object sender, EventArgs e)
        {
            _RowCount++;

            detail.Visible = true;

            if (_RowCount > 5)
            {
                detail.Visible = false;
            }
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (_RowCount > 5)
            {
                this.pageFooter.Visible = true;
            }
            else
            {
                this.pageFooter.Visible = false;
            }
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
            this.PAGENUM.Value = _fiNowPage;
            _fiNowPage++;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            if (_RowCount > 5)
            {
                this.pageHeader.Visible = true;

            }
        }
    }
}
