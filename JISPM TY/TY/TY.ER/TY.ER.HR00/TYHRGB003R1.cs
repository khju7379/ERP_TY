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
    /// Summary description for TYHRGB003R1.
    /// </summary>
    public partial class TYHRGB003R1 : GrapeCity.ActiveReports.SectionReport
    {

        public TYHRGB003R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
