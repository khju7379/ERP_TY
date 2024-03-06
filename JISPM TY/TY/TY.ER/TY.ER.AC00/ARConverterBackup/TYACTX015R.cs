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
    /// Summary description for TYACTX015R.
    /// </summary>
    public partial class TYACTX015R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        
        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;

        public TYACTX015R(DataTable dt, string sYEAR, string sRPTGUBN, string sCONFGB)
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
            this.S5RPTGUBN.Value = _sRPTGUBN;
            this.S5YEAR.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt.Rows[0]["ASMSANGHO"].ToString();
            this.ASMNAMENM.Value = _dt.Rows[0]["ASMNAMENM"].ToString();
        }

        private void detail_Format(object sender, EventArgs e)
        {

        }

        private void pageFooter_Format(object sender, EventArgs e)
        {

        }
    }
}
