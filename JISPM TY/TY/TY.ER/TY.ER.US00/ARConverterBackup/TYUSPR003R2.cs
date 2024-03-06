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
    /// Summary description for TYUSPR003R2.
    /// </summary>
    public partial class TYUSPR003R2 : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUSPR003R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this.groupFooter1.NewPage = NewPage.After;            

           

        }

        private void TYUSPR003R2_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

        }     

        private void groupFooter1_Format(object sender, EventArgs e)
        {
           

        }
    }
}