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
    /// Summary description for TYUSKB002R.
    /// </summary>
    public partial class TYUSKB002R : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();

        public TYUSKB002R()
        {
            InitializeComponent();
        }

        private void TYUSKB002R_DataInitialize(object sender, EventArgs e)
        {
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
        }
    }
}