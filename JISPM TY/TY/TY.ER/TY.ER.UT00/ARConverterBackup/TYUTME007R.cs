﻿using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME007R.
    /// </summary>
    public partial class TYUTME007R : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUTME007R()
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
