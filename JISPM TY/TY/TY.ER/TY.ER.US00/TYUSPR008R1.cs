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

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSPR008R1.
    /// </summary>
    public partial class TYUSPR008R1 : GrapeCity.ActiveReports.SectionReport
    {

        public TYUSPR008R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this.groupFooter1.NewPage = NewPage.After;
        }

        private void TYUSPR008R1_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}