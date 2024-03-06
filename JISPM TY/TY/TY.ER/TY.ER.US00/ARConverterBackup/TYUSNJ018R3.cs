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
    /// Summary description for TYUSNJ018R3.
    /// </summary>
    public partial class TYUSNJ018R3 : DataDynamics.ActiveReports.ActiveReport
    {
        public TYUSNJ018R3()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {

         
        }

        private void TYUSNJ018R3_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)  //선내
                    {
                        
                        txtHWYYMMTITLE.Text = "( " + dt.Rows[i]["EDATE"].ToString().Substring(0, 4) + "년 " + dt.Rows[i]["EDATE"].ToString().Substring(4, 2) + "월 분 )";
                        
                        txtHMWKQTY.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtHMWKQTYHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYHAP"].ToString()));

                        txtHMWKAMT.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKAMT"].ToString()));
                        txtHMWKAMTHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKAMTHAP"].ToString()));

                        txtDANGA.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[i]["DANGA"].ToString()));

                        txtHMWKQTYTOTAL.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtHMWKQTYHAPTOTAL.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYHAP"].ToString()));

                        txtHMWKAMTTOTAL.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKAMT"].ToString()));
                        txtHMWKAMTHAPTOTAL.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKAMTHAP"].ToString()));
                    }


                }
            }
        }
      
    }
}