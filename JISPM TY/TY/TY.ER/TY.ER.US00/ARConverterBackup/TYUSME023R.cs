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
    /// Summary description for TYUSME023R.
    /// </summary>
    public partial class TYUSME023R : DataDynamics.ActiveReports.ActiveReport
    {
        public TYUSME023R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYUSME023R_DataInitialize(object sender, EventArgs e)
        {
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            this.groupHeader1.NewPage = NewPage.Before;
        }
    }
}