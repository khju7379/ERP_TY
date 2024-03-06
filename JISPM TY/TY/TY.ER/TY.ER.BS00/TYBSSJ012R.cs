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



namespace TY.ER.BS00
{
    /// <summary>
    /// Summary description for TYBSSJ012R.
    /// </summary>
    public partial class TYBSSJ012R : GrapeCity.ActiveReports.SectionReport
    {
        private string fsPREYEAR = string.Empty;

        public TYBSSJ012R(string sPREYEAR)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsPREYEAR = sPREYEAR;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            this.PREYEAR.Text = fsPREYEAR;
        }
    }
}
