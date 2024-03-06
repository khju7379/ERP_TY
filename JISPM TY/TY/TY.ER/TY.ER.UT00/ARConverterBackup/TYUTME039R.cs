using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME039R.
    /// </summary>
    public partial class TYUTME039R : DataDynamics.ActiveReports.ActiveReport
    {
        private int fiSEQ = 1;

        public TYUTME039R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            DataTable dt = this.DataSource as DataTable;

            this.M3ENHMAMTOT.Text = string.Format("{0:#,##0}", dt.Compute("SUM(M3ENHMAM)", null));
            this.M3SEAMTTOT.Text = string.Format("{0:#,##0}", dt.Compute("SUM(M3SEAMT)", null));
        }
    }
}
