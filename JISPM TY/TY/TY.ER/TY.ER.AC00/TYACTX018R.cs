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
    /// Summary description for TYACTX018R.
    /// </summary>
    public partial class TYACTX018R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();

        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;

        public TYACTX018R(DataTable dt, string sYEAR, string sRPTGUBN, string sCONFGB)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this._dt = dt;

            this._sYEAR = sYEAR;
            this._sRPTGUBN = sRPTGUBN;
            this._sCONFGB = sCONFGB;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {

            if (_sCONFGB == "1")
            {
                this.CONFGB1.Value = "■";
                this.CONFGB2.Value = "□";
            }
            else
            {
                this.CONFGB1.Value = "□";
                this.CONFGB2.Value = "■";
            }

            this.RPTGUBN.Value = _sRPTGUBN;
            this.YEAR.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt.Rows[0]["ASMSANGHO"].ToString();
            this.ASMVNADDRS.Value = _dt.Rows[0]["ASMVNADDRS"].ToString();
            this.ASMUPTAE.Value = _dt.Rows[0]["CHULUPTAE"].ToString();
            this.ASMEVENT.Value = _dt.Rows[0]["CHULMEVENT"].ToString();
            this.ASMNAMENM.Value = _dt.Rows[0]["ASMNAMENM"].ToString();

            DataTable dt = this.DataSource as DataTable;

            this.AMT1.Value = dt.Rows[0]["AMT"].ToString();
            this.AMT2.Value = dt.Rows[1]["AMT"].ToString();
            this.AMT3.Value = dt.Rows[2]["AMT"].ToString();
            this.AMT4.Value = dt.Rows[3]["AMT"].ToString();
            this.AMT5.Value = dt.Rows[4]["AMT"].ToString();

            this.SUMAMT1.Value = dt.Rows[5]["AMT"].ToString();
            this.SUMAMT2.Value = dt.Rows[4]["AMT"].ToString();
            this.SUMAMT3.Value = dt.Rows[6]["AMT"].ToString();
        }
    }
}
