﻿using System;
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
    /// Summary description for TYUSME032R.
    /// </summary>
    public partial class TYUSME032R : DataDynamics.ActiveReports.ActiveReport
    {
        public TYUSME032R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYUSME032R_DataInitialize(object sender, EventArgs e)
        {
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            this.groupHeader1.NewPage = NewPage.Before;
        }
    }
}