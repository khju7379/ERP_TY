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
    /// Summary description for TYUTME017R.
    /// </summary>
    public partial class TYUSME017R7 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt1 = new DataTable();
        private DataTable _dt2 = new DataTable();

        public TYUSME017R7()
        { 
            InitializeComponent();
        }

        private void TYUSME017R7_ReportStart(object sender, EventArgs e)
        {
        }

        private void TYUSME017R7_ReportEnd(object sender, EventArgs e)
        {
        }

        private void TYUSME017R7_PageStart(object sender, EventArgs e)
        {
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
        }
    }
}
