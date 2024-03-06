using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRCS004R.
    /// </summary>
    public partial class TYHRCS004R : DataDynamics.ActiveReports.ActiveReport
    {
        private int ficount = 1;

        public TYHRCS004R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            PAGENUM.Text = ficount.ToString();
            ficount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
