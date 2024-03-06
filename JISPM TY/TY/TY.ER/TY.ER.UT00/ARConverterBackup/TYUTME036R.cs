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
    /// Summary description for TYUTME036R.
    /// </summary>
    public partial class TYUTME036R : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable fdt;

        public TYUTME036R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            fdt = this.DataSource as DataTable;

            this.HMCHARGEAMTTOT.Text = string.Format("{0:#,##0}", fdt.Compute("SUM(HMCHARGEAMT)", null));
            this.HMSECHARGEAMTTOT.Text = string.Format("{0:#,##0}", fdt.Compute("SUM(HMSECHARGEAMT)", null));
            this.HMTOTALAMTTOT.Text = string.Format("{0:#,##0}", fdt.Compute("SUM(HMTOTALAMT)", null));
        }
    }
}
