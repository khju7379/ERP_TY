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
    /// Summary description for TYUSNJ010R1.
    /// </summary>
    public partial class TYUSNJ010R1 : DataDynamics.ActiveReports.ActiveReport
    {
        public TYUSNJ010R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            
        }

        private void TYUSNJ010R1_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
           
        }     

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            
        }

        
    }
}