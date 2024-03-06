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
    /// Summary description for TYUTPR007R.
    /// </summary>
    public partial class TYUTPR007R : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUTPR007R()
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
