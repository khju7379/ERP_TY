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



namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRGB005R.
    /// </summary>
    public partial class TYHRGB005R : GrapeCity.ActiveReports.SectionReport
    {
        string fsSDATE = string.Empty;
        string fsEDATE = string.Empty;

        public TYHRGB005R(string sSDATE, string sEDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsSDATE = sSDATE;
            fsEDATE = sEDATE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.CISDATE.Text = fsSDATE;
            this.CIEDATE.Text = fsEDATE;
        }
    }
}
