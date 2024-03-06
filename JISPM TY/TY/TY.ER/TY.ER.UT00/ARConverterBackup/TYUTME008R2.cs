using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME008R2.
    /// </summary>
    public partial class TYUTME008R2 : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUTME008R2()
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

        private void TYUTME008R2_ReportStart(object sender, EventArgs e)
        {

        }
    }
}
