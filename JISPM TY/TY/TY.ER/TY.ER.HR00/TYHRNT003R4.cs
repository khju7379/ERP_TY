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
    /// Summary description for TYHRNT003R4.(2018년개정용)
    /// </summary>
    public partial class TYHRNT003R4 : GrapeCity.ActiveReports.SectionReport
    {

        public TYHRNT003R4()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.detail.NewPage = NewPage.After;
        }
    }
}
