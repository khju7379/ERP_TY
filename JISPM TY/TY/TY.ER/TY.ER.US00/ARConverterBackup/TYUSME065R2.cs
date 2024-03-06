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
    /// Summary description for TYUSME065R2.
    /// </summary>
    public partial class TYUSME065R2 : DataDynamics.ActiveReports.ActiveReport
    {
        public TYUSME065R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYUSME065R2_DataInitialize(object sender, EventArgs e)
        {
        }

        private void groupHeader2_Format(object sender, EventArgs e)
        {
            this.groupHeader2.NewPage = NewPage.Before;
        }
    }
}