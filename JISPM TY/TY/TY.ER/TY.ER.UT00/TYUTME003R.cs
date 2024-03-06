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

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME003R.
    /// </summary>
    public partial class TYUTME003R : GrapeCity.ActiveReports.SectionReport
    {

        public TYUTME003R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            DataTable dt = this.DataSource as DataTable;

            this.COUNTHAP.Text = string.Format("{0:#,##0}", dt.Compute("SUM(COUNT)", null));
            this.IMISAMTHAP.Text = string.Format("{0:#,##0}", dt.Compute("SUM(IMISAMT)", null));
        }
    }
}
