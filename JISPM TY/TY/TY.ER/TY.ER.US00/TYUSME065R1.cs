﻿using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSME065R1.
    /// </summary>
    public partial class TYUSME065R1 : GrapeCity.ActiveReports.SectionReport
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