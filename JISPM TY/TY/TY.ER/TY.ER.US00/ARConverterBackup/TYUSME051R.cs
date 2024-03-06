using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSME051R.
    /// </summary>
    public partial class TYUSME051R : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUSME051R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
