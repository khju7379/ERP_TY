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
    /// Summary description for TYUTME036R.
    /// </summary>
    public partial class TYUTME036R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable fdt;

        public TYUTME036R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            fdt = this.DataSource as DataTable;

            this.HMCHARGEAMTTOT.Text = string.Format("{0:#,##0}", fdt.Compute("SUM(HMCHARGEAMT)", null));
            this.HMSECHARGEAMTTOT.Text = string.Format("{0:#,##0}", fdt.Compute("SUM(HMSECHARGEAMT)", null));
            this.HMTOTALAMTTOT.Text = string.Format("{0:#,##0}", fdt.Compute("SUM(HMTOTALAMT)", null));
        }
    }
}
