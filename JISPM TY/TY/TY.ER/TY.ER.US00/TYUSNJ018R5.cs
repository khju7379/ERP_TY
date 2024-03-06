using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSNJ018R5.
    /// </summary>
    public partial class TYUSNJ018R5 : GrapeCity.ActiveReports.SectionReport
    {
        public TYUSNJ018R5()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {


        }

        private void TYUSNJ018R5_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)  //선내
                    {
                        txtSOTITLE.Text = dt.Rows[i]["SOTITLE"].ToString();
                        if (dt.Rows[i]["SOTITLE"].ToString() != "")
                        {
                            txtHWYYMMTITLE.Text = "( " + dt.Rows[i]["SDATE"].ToString().Substring(0, 4) + "/" + dt.Rows[i]["SDATE"].ToString().Substring(4, 2) + " ) ~ ( " +
                                                   dt.Rows[i]["EDATE"].ToString().Substring(0, 4) + "/" + dt.Rows[i]["EDATE"].ToString().Substring(4, 2) + " )";
                        }
                        else
                        {
                            txtHWYYMMTITLE.Text = "( " + dt.Rows[i]["EDATE"].ToString().Substring(0, 4) + "/" + dt.Rows[i]["EDATE"].ToString().Substring(4, 2) + " )";
                        }
                        txtGUBN.Text = dt.Rows[i]["GUBN"].ToString();
                        txtHMWKQTY.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtHMWKQTYNU.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYNU"].ToString()));
                        txtHMNOIMAMT.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMNOIMAMT"].ToString()));
                        txtHMNOIMAMTNU.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMNOIMAMTNU"].ToString()));
                        txtDANGA.Text = String.Format("{0:#,##0.0}", Convert.ToDouble(dt.Rows[i]["DANGA"].ToString()));
                        txtHMTJCDAMT.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMTJCDAMT"].ToString()));
                        txtHMTJCDAMTNU.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMTJCDAMTNU"].ToString()));
                    }

                    if (i == 1) //연안
                    {
                        txtGUBN1.Text = dt.Rows[i]["GUBN"].ToString();
                        txtHMWKQTY1.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtHMWKQTYNU1.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYNU"].ToString()));
                        txtHMNOIMAMT1.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMNOIMAMT"].ToString()));
                        txtHMNOIMAMTNU1.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMNOIMAMTNU"].ToString()));
                        txtDANGA1.Text = String.Format("{0:#,##0.0}", Convert.ToDouble(dt.Rows[i]["DANGA"].ToString()));
                        txtHMTJCDAMT1.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMTJCDAMT"].ToString()));
                        txtHMTJCDAMTNU1.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMTJCDAMTNU"].ToString()));
                    }

                    if (i == 2) //합계
                    {
                        txtHMWKQTYHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTY"].ToString()));
                        txtHMWKQTYNUHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMWKQTYNU"].ToString()));
                        txtHMNOIMAMTHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMNOIMAMT"].ToString()));
                        txtHMNOIMAMTNUHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMNOIMAMTNU"].ToString()));
                        txtHMTJCDAMTHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMTJCDAMT"].ToString()));
                        txtHMTJCDAMTNUHAP.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["HMTJCDAMTNU"].ToString()));
                    }

                }
            }
        }

    }
}