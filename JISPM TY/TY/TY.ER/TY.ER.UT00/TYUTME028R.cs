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

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME028R.
    /// </summary>
    public partial class TYUTME028R : GrapeCity.ActiveReports.SectionReport
    {
        private int fiSEQ = 1;

        public TYUTME028R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.SEQ.Text = fiSEQ.ToString();

            fiSEQ++;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            DataTable dt = this.DataSource as DataTable;

            this.AMOUNTTOT.Text = string.Format("{0:#,##0}", dt.Compute("SUM(AMOUNT)", null));
            this.VATTOT.Text = string.Format("{0:#,##0}", dt.Compute("SUM(VAT)", null));
            this.TOTTOT.Text = string.Format("{0:#,##0}", dt.Compute("SUM(TOT)", null));
        }
    }
}
