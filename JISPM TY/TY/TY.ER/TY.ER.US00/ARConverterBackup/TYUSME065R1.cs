using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSME065R1.
    /// </summary>
    public partial class TYUSME065R1 : DataDynamics.ActiveReports.ActiveReport
    {
        public TYUSME065R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYUSME065R1_DataInitialize(object sender, EventArgs e)
        {
        }

        private void groupHeader2_Format(object sender, EventArgs e)
        {
            this.groupHeader2.NewPage = NewPage.Before;
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            this.groupHeader1.NewPage = NewPage.Before;
        }
    }
}