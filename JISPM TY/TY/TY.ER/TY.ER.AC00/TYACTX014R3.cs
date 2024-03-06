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
    /// Summary description for TYACTX014R3.
    /// </summary>
    public partial class TYACTX014R3 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private DataTable _dt2 = new DataTable();

        private int _RowCount = 0;

        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;

        public TYACTX014R3(DataTable dt, DataTable dt2, string sYEAR, string sRPTGUBN, string sCONFGB)
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

        private void TYACTX014R3_ReportStart(object sender, EventArgs e)
        {
            this.pageHeader.Visible = false;
            this.pageFooter.Visible = false;

            DataTable dt = this.DataSource as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 5)
                {
                    TYACTX014R2 subReport = new TYACTX014R2();
                    subReport.DataSource = dt;
                    TYACTX014R2.Report = subReport;
                }
            }
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (_RowCount > 15)
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

            if (_RowCount > 15)
            {
                detail.Visible = false;
            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            if (_RowCount > 15)
            {
                this.pageHeader.Visible = true;

            }
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            if (_sCONFGB == "1")
            {
                this.S4CONFGB.Value = "예정";
                this.S4CONFGB2.Value = "예정";
            }
            else
            {
                this.S4CONFGB.Value = "확정";
                this.S4CONFGB2.Value = "확정";
            }

            this.S4RPTGUBN.Value = _sRPTGUBN;
            this.S4YEAR.Value = _sYEAR;
            this.S4RPTGUBN2.Value = _sRPTGUBN;
            this.S4YEAR2.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSAUPNO2.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt2.Rows[0]["ASMSANGHO"].ToString();
            this.ASMNAMENM.Value = _dt2.Rows[0]["ASMNAMENM"].ToString();
            this.ASMCORPNO.Value = _dt2.Rows[0]["ASMCORPNO"].ToString();

            this.GUNSU_CNT1.Value = _dt.Rows[0]["GUNSU_CNT"].ToString();
            this.HAP_AMT1.Value = _dt.Rows[0]["HAP_AMT"].ToString();
            this.HAP_VAT1.Value = _dt.Rows[0]["HAP_VAT"].ToString();

            this.GUNSU_CNT2.Value = _dt.Rows[1]["GUNSU_CNT"].ToString();
            this.HAP_AMT2.Value = _dt.Rows[1]["HAP_AMT"].ToString();
            this.HAP_VAT2.Value = _dt.Rows[1]["HAP_VAT"].ToString();


            //this.GUNSU_CNT3.Value = _dt.Rows[2]["GUNSU_CNT"].ToString();
            //this.HAP_AMT3.Value = _dt.Rows[2]["HAP_AMT"].ToString();
            //this.HAP_VAT3.Value = _dt.Rows[2]["HAP_VAT"].ToString();

            //this.GUNSU_CNT4.Value = _dt.Rows[3]["GUNSU_CNT"].ToString();
            //this.HAP_AMT4.Value = _dt.Rows[3]["HAP_AMT"].ToString();
            //this.HAP_VAT4.Value = _dt.Rows[3]["HAP_VAT"].ToString();

            this.GUNSU_CNT5.Value = _dt.Rows[2]["GUNSU_CNT"].ToString();
            this.HAP_AMT5.Value = _dt.Rows[2]["HAP_AMT"].ToString();
            this.HAP_VAT5.Value = _dt.Rows[2]["HAP_VAT"].ToString();
        }
    }
}
