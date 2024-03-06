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
    /// Summary description for TYUTPR001R.
    /// </summary>
    public partial class TYUTPR001R : GrapeCity.ActiveReports.SectionReport
    {

        private string fsCHMTQTY = string.Empty;
        private string fsCHKLQTY = string.Empty;

        public TYUTPR001R(string sCHMTQTY, string sCHKLQTY)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsCHMTQTY = sCHMTQTY;
            fsCHKLQTY = sCHKLQTY;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.CHMTQTYHAP.Text = string.Format("{0:#,##0.000}", fsCHMTQTY);
            this.CHKLQTYHAP.Text = string.Format("{0:#,##0.000}", fsCHKLQTY);
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
