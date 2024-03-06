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
    /// Summary description for TYUSNJ018R4.
    /// </summary>
    public partial class TYUSNJ018R4 : DataDynamics.ActiveReports.ActiveReport
    {
        private double fdHYAMT = 0;
        private double fdHBAMT = 0;
        private double fdHWYQTY1 = 0;

        private double fdHWBQTY = 0; //육상출고
        private double fdHWYQTY = 0; //양하


        public TYUSNJ018R4()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {

         
        }

        private void TYUSNJ018R4_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //양하
                    if (dt.Rows[i]["HWHANGCHA"].ToString() != "")
                    {
                        fdHWYQTY = fdHWYQTY + Convert.ToDouble(dt.Rows[i]["HWWKQTY"].ToString());
                    }
                    else
                    {
                        //육상출고
                        if (dt.Rows[i]["HWGOKJONG"].ToString() == "20" || dt.Rows[i]["HWGOKJONG"].ToString() == "40")
                        {
                            fdHWYQTY1 = fdHWYQTY1 + Convert.ToDouble(dt.Rows[i]["HWWKQTY"].ToString());
                        }
                        else
                        {
                            fdHWBQTY = fdHWBQTY + Convert.ToDouble(dt.Rows[i]["HWWKQTY"].ToString());
                        }
                    }

                    if (dt.Rows[i]["HWGOKJONG"].ToString() == "20" || dt.Rows[i]["HWGOKJONG"].ToString() == "40")
                    {
                        fdHBAMT = fdHBAMT + Convert.ToDouble(dt.Rows[i]["JGHWAKAMT"].ToString());
                    }
                    else
                    {
                        fdHYAMT = fdHYAMT + Convert.ToDouble(dt.Rows[i]["JGHWAKAMT"].ToString()) + Convert.ToDouble(dt.Rows[i]["USUSEDAMT"].ToString());
                    }
                }

                txtHWBQTY.Text = String.Format("{0:#,##0}", fdHWBQTY);
                txtHWYQTY.Text = String.Format("{0:#,##0}", fdHWYQTY);
                txtHQTYHAP.Text = String.Format("{0:#,##0}", fdHWBQTY + fdHWYQTY);

                txtHWYQTY1.Text = String.Format("{0:#,##0}", fdHWYQTY1);
                txtHWYQTY1HAP.Text = String.Format("{0:#,##0}", fdHWYQTY1);

                txtHWBQTY_TOTAL.Text = String.Format("{0:#,##0}", fdHWBQTY);
                txtHWYQTY_TOTAL.Text = String.Format("{0:#,##0}", fdHWYQTY + fdHWYQTY1);

                txtHAPTOTAL.Text = String.Format("{0:#,##0}", fdHWBQTY + fdHWYQTY + fdHWYQTY1);

                txtHYAMT.Text = String.Format("{0:#,##0}", fdHYAMT);
                txtHBAMT.Text = String.Format("{0:#,##0}", fdHBAMT);
                txtHYBAMT_TOTAL.Text = String.Format("{0:#,##0}", fdHYAMT+ fdHBAMT);

            }
        }
      
    }
}