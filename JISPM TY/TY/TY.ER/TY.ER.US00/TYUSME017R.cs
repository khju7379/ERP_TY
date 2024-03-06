using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUTME017R.
    /// </summary>
    public partial class TYUSME017R : GrapeCity.ActiveReports.SectionReport
    {
        private int fiCount = 0;
        private DataTable _dt = new DataTable();

        public TYUSME017R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            SAUPNO1.Text = _dt.Rows[fiCount]["SAUP1"].ToString() + "-" + _dt.Rows[fiCount]["SAUP2"].ToString() + "-" + _dt.Rows[fiCount]["SAUP3"].ToString();
            YYMMDD1.Text = _dt.Rows[fiCount]["YYMMDD"].ToString().Substring(0, 4) + "/" + _dt.Rows[fiCount]["YYMMDD"].ToString().Substring(4, 2) + "/" + _dt.Rows[fiCount]["YYMMDD"].ToString().Substring(6, 2);
            AMT1.Text = string.Format("{0:#,###}", double.Parse(_dt.Rows[fiCount]["AMT"].ToString()));
            VAT1.Text = string.Format("{0:#,###}", double.Parse(_dt.Rows[fiCount]["VAT"].ToString()));

            MONTH1.Text = _dt.Rows[fiCount]["MMDD"].ToString().Substring(0, 2);
            DAY1.Text = _dt.Rows[fiCount]["MMDD"].ToString().Substring(2, 2);


            SAUPNO2.Text = _dt.Rows[fiCount]["SAUP1"].ToString() + "-" + _dt.Rows[fiCount]["SAUP2"].ToString() + "-" + _dt.Rows[fiCount]["SAUP3"].ToString();
            YYMMDD2.Text = _dt.Rows[fiCount]["YYMMDD"].ToString().Substring(0, 4) + "/" + _dt.Rows[fiCount]["YYMMDD"].ToString().Substring(4, 2) + "/" + _dt.Rows[fiCount]["YYMMDD"].ToString().Substring(6, 2);
            AMT2.Text = string.Format("{0:#,###}", double.Parse(_dt.Rows[fiCount]["AMT"].ToString()));
            VAT2.Text = string.Format("{0:#,###}", double.Parse(_dt.Rows[fiCount]["VAT"].ToString()));

            MONTH2.Text = _dt.Rows[fiCount]["MMDD"].ToString().Substring(0, 2);
            DAY2.Text = _dt.Rows[fiCount]["MMDD"].ToString().Substring(2, 2);

            fiCount++;
        }
    }
}
