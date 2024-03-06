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
    /// Summary description for TYHRNT002R2.
    /// </summary>
    public partial class TYHRNT002R2 : GrapeCity.ActiveReports.SectionReport
    {
        int ficount = 0;

        public TYHRNT002R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (ficount == 0)
            {
                this.detail.Visible = false;
            }
            else
            {
                this.detail.Visible = true;
            }
            ficount++;
        }
    }
}
