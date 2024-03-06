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
    /// Summary description for TYUSNJ011R1.
    /// </summary>
    public partial class TYUSNJ011R1 : DataDynamics.ActiveReports.ActiveReport
    {
        public TYUSNJ011R1(string sTitGubn)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            if (sTitGubn == "1")
            {
                lblGOKJONG.Text = "인   원";
                txtINWONHAP.Visible = true;
            }
            else
            {
                txtINWONHAP.Visible = false;
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this.groupFooter1.NewPage = NewPage.After;            

           

        }

        private void TYUSNJ011R1_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

        }     

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            txtINWONHAP.Text = txtINWON.Text + " 명";
        }
    }
}