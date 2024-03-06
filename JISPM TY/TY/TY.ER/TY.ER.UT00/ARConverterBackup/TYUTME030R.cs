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
    /// Summary description for TYUTME030R.
    /// </summary>
    public partial class TYUTME030R : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUTME030R()
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
