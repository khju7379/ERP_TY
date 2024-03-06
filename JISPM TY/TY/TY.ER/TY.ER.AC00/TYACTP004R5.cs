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
    /// Summary description for TYACTP004R5.
    /// </summary>
    public partial class TYACTP004R5 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        DataTable _dt2 = new DataTable();
        int _iRowCount = 0;
        int _iPage = 0;

        public TYACTP004R5(DataTable dt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _dt = dt;
            reportFooter1.Visible = false;
            pageHeader.Visible = false;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.UPCOUNT1.Text = _dt.Rows[1]["COUNT"].ToString();
            this.UPWTOTALAMT1.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WTOTALAMT"]);
            this.UPWTAXINCOM1.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WTAXINCOM"]);
            this.UPWLOCALTAX1.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WLOCALTAX"]);
            this.UPWLSUMTAX1.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WLSUMTAX"]);

            this.DOWNCOUNT1.Text = _dt.Rows[2]["COUNT"].ToString();
            this.DOWNWTOTALAMT1.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WTOTALAMT"]);
            this.DOWNWTAXINCOM1.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WTAXINCOM"]);
            this.DOWNWLOCALTAX1.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WLOCALTAX"]);
            this.DOWNWLSUMTAX1.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WLSUMTAX"]);
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            _dt2 = this.DataSource as DataTable;
            int iMENCOUNT = 0;

            for (int i = 0; i < _dt2.Rows.Count; i++)
            {
                if (_dt2.Rows[i]["WTRADNAME"].ToString() != "")
                {
                    iMENCOUNT++;
                }
            }

            WREYY1.Text = _dt2.Rows[0]["WREYY"].ToString();

            this.MENCOUNT.Text = iMENCOUNT.ToString();
            this.TOTALCOUNT.Text = _dt.Rows[0]["COUNT"].ToString();
            this.TOTALWTOTALAMT.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WTOTALAMT"]);
            this.TOTALWTAXINCOM.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WTAXINCOM"]);
            this.TOTALWLOCALTAX.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WLOCALTAX"]);
            this.TOTALWLSUMTAX.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WLSUMTAX"]);

            this.UPCOUNT.Text = _dt.Rows[1]["COUNT"].ToString();
            this.UPWTOTALAMT.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WTOTALAMT"]);
            this.UPWTAXINCOM.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WTAXINCOM"]);
            this.UPWLOCALTAX.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WLOCALTAX"]);
            this.UPWLSUMTAX.Text = string.Format("{0:#,##0}", _dt.Rows[1]["WLSUMTAX"]);

            this.DOWNCOUNT.Text = _dt.Rows[2]["COUNT"].ToString();
            this.DOWNWTOTALAMT.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WTOTALAMT"]);
            this.DOWNWTAXINCOM.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WTAXINCOM"]);
            this.DOWNWLOCALTAX.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WLOCALTAX"]);
            this.DOWNWLSUMTAX.Text = string.Format("{0:#,##0}", _dt.Rows[2]["WLSUMTAX"]);


        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_iRowCount == 9)
            {
                this.detail.Visible = false;
                _iPage++;
            }
            if (_iPage == 1)
            {
                pageHeader.Visible = true;
                reportFooter1.Visible = true;
                TYACTP004R6 subReport = new TYACTP004R6(_dt);
                subReport.DataSource = _dt2;
                TYACTP004R6.Report = subReport;
            }
            _iRowCount++;
        }
    }
}
