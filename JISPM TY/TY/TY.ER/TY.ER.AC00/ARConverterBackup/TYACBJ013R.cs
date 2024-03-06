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
    /// Summary description for TYACBJ013R.
    /// </summary>
    public partial class TYACBJ013R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        //private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sGUBUN   = string.Empty;
        private string sGroup   = string.Empty;
        private string sNEWCDAC = string.Empty;
        private string sOLDCDAC = string.Empty;

        public TYACBJ013R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.line5.LineStyle = LineStyle.Dash;
            this.line5.LineWeight = 1;

            if (dt.Rows[_iCount - 1]["GUBUN"].ToString() == "HAP")
            {
                this.AMT1.Font    = new Font("굴림", 9, FontStyle.Bold);
                this.TMAMDR.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.TMAMOUT.Font = new Font("굴림", 9, FontStyle.Bold);
                this.TMCDAC.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.A1ABAC.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.TMAMIN.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.TMAMCR.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT2.Font    = new Font("굴림", 9, FontStyle.Bold);

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }
        }

        private void TYACBJ013R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}