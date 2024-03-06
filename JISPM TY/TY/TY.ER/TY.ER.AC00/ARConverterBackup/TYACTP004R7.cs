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
    /// Summary description for TYACTP004R7.
    /// </summary>
    public partial class TYACTP004R7 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        DataTable _dt2 = new DataTable();
        int _iRowCount = 0;
        int _iPage = 0;

        public TYACTP004R7(DataTable dt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _dt = dt;
            reportFooter1.Visible = false;
            pageHeader.Visible = false;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
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

            this.MENCOUNT.Text = iMENCOUNT.ToString();
            this.TOTALCOUNT.Text = _dt.Rows[0]["COUNT"].ToString();
            this.TOTALWTOTALAMT.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WTOTALAMT"]);
            this.TOTALWINCOMAMT.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WINCOMAMT"]);
            this.TOTALWTAXINCOM.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WTAXINCOM"]);
            this.TOTALWLOCALTAX.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WLOCALTAX"]);
            this.TOTALWLSUMTAX.Text = string.Format("{0:#,##0}", _dt.Rows[0]["WLSUMTAX"]);
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.SWREYY.Text = _dt2.Rows[0]["WREYY"].ToString();
            this.SASMSAUPNO.Text = _dt2.Rows[0]["ASMSAUPNO"].ToString();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_iRowCount == 15)
            {
                this.detail.Visible = false;
                _iPage++;
            }
            _iRowCount++;
            if (_iPage == 1)
            {
                reportFooter1.Visible = true;
                TYACTP004R8 subReport = new TYACTP004R8();
                subReport.DataSource = _dt2;
                TYACTP004R8.Report = subReport;
                pageHeader.Visible = true;
            }
        }

        
    }
}
