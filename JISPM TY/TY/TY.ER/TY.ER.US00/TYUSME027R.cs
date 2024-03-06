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



namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSME027R.
    /// </summary>
    public partial class TYUSME027R : GrapeCity.ActiveReports.SectionReport
    {

        public TYUSME027R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            //this.groupHeader1.NewPage = NewPage.After;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
