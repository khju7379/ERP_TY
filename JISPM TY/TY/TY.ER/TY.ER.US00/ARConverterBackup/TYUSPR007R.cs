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
    /// Summary description for TYUSPR007R.
    /// </summary>
    public partial class TYUSPR007R : DataDynamics.ActiveReports.ActiveReport
    {
     
        
     

        public TYUSPR007R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
           
        }

        private void TYUSPR007R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;          
        }
    }
}