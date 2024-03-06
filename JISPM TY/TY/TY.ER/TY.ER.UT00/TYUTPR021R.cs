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



namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTPR021R.
    /// </summary>
    public partial class TYUTPR021R : GrapeCity.ActiveReports.SectionReport
    {
        private string fsTITLE = string.Empty;

        public TYUTPR021R(string sTITLE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsTITLE = sTITLE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.TITLE.Text = fsTITLE;
        }
    }
}
