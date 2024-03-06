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
    /// Summary description for TYHRPY12R1.
    /// </summary>
    public partial class TYHRPY12R4 : GrapeCity.ActiveReports.SectionReport
    {
        string fsYYMM = string.Empty;
        string fsGUBUNNM = string.Empty;

        public TYHRPY12R4(string sYYMM, string sGUBUNNM)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsYYMM = sYYMM;
            fsGUBUNNM = sGUBUNNM;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.YEAR.Text = fsYYMM.Substring(0, 4);
            this.MONTH.Text = fsYYMM.Substring(4, 2);
            this.GUBUN.Text = fsGUBUNNM;
        }
    }
}
