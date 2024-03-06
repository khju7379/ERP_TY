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
    /// Summary description for TYUSNJ018R1.
    /// </summary>
    public partial class TYUSNJ018R1 : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUSNJ018R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
           
        }

        private void TYUSNJ018R1_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        txtTITLE.Text = "( " + dt.Rows[i]["EDATE"].ToString().Substring(0, 4) + "/" + dt.Rows[i]["EDATE"].ToString().Substring(4, 2) + " ) ";
                    }

                    if (dt.Rows[i]["GUBN"].ToString() == "1")
                    {
                        txtHDANGA.Text =  dt.Rows[i]["DANGA"].ToString();
                        txtHHMWKQTY.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtHHMWKQTYNU.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYNU"].ToString()));
                        txtHHMEDUAMT.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMEDUAMT"].ToString())); 

                    }
                    else if (dt.Rows[i]["GUBN"].ToString() == "2")
                    {
                        txtYDANGA.Text = dt.Rows[i]["DANGA"].ToString();
                        txtYHMWKQTY.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtYHMWKQTYNU.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYNU"].ToString()));
                        txtYHMEDUAMT.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMEDUAMT"].ToString())); 
                    }
                    else
                    {
                        txtHMWKQTYHAP.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtHMWKQTYNUHAP.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYNU"].ToString()));
                        txtHMEDUAMTHAP.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMEDUAMT"].ToString()));
                        txtHMEDUAMTNUHAP.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMEDUAMTNU"].ToString())); 
                    }
                }
            }
        }     
        
    }
}