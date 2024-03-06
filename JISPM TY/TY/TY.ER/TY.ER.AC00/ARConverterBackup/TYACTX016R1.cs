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
    /// Summary description for TYACTX016R.
    /// </summary>
    public partial class TYACTX016R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private DataTable _dt2 = new DataTable();

        private int _RowCount = 0;

        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;

        private double _fiNowPage = 1;

        public TYACTX016R1(DataTable dt, DataTable dt2, string sYEAR, string sRPTGUBN, string sCONFGB)
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
        }

        private void TYACTX016R1_ReportStart(object sender, EventArgs e)
        {
            this.pageHeader.Visible = false;
            this.pageFooter.Visible = false;

            DataTable dt = this.DataSource as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 5)
                {
                    TYACTX016R2 subReport = new TYACTX016R2();
                    subReport.DataSource = dt;
                    TYACTX016R2.Report = subReport;
                }
            }
        }

        private void reportHeader1_Format(object sender, EventArgs e)
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

            this.S7RPTGUBN.Value = _sRPTGUBN;
            this.S7YEAR.Value = _sYEAR;
            this.S7RPTGUBN2.Value = _sRPTGUBN;
            this.S7YEAR2.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSAUPNO2.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt2.Rows[0]["ASMSANGHO"].ToString();
            this.ASMNAMENM.Value = _dt2.Rows[0]["ASMNAMENM"].ToString();
            this.ASMVNADDRS.Value = _dt2.Rows[0]["ASMVNADDRS"].ToString();
            this.ASMUPTAE.Value = _dt2.Rows[0]["CHULUPTAE"].ToString();
            this.ASMEVENT.Value = _dt2.Rows[0]["CHULMEVENT"].ToString();

            this.CNT1.Value = _dt.Rows[0]["CNT"].ToString();
            this.S7FORGIAMT1.Value = _dt.Rows[0]["S7FORGIAMT"].ToString();
            this.S7WONHAAMT1.Value = _dt.Rows[0]["S7WONHAAMT"].ToString();

            this.CNT2.Value = _dt.Rows[1]["CNT"].ToString();
            this.S7FORGIAMT2.Value = _dt.Rows[1]["S7FORGIAMT"].ToString();
            this.S7WONHAAMT2.Value = _dt.Rows[1]["S7WONHAAMT"].ToString();
            this.S7FORGIAMT4.Value = _dt.Rows[1]["S7FORGIAMT"].ToString();
            this.S7WONHAAMT4.Value = _dt.Rows[1]["S7WONHAAMT"].ToString();

            this.CNT3.Value = _dt.Rows[2]["CNT"].ToString();
            this.S7FORGIAMT3.Value = _dt.Rows[2]["S7FORGIAMT"].ToString();
            this.S7WONHAAMT3.Value = _dt.Rows[2]["S7WONHAAMT"].ToString();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (_RowCount > 12)
            {
                this.pageFooter.Visible = true;
            }
            else
            {
                this.pageFooter.Visible = false;
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            _RowCount++;

            detail.Visible = true;

            if (_RowCount > 12)
            {
                detail.Visible = false;
            }
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
            this.PAGENUM.Value = _fiNowPage;
            _fiNowPage++;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            if (_RowCount > 12)
            {
                this.pageHeader.Visible = true;

            }
        }
    }
}
