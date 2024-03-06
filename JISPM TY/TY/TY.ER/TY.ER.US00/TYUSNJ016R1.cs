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
    /// Summary description for TYUSNJ016R1.
    /// </summary>
    public partial class TYUSNJ016R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable fdt;

        private double fdHWWKQTY = 0;
        private double fdHWHSBJAMT = 0;
        private int fiCount = 0;

        public TYUSNJ016R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this.groupFooter1.NewPage = NewPage.After;            

            if (fdt.Rows[fiCount]["ROWNUM"].ToString() != "")
            {
                fdHWWKQTY = fdHWWKQTY + Convert.ToDouble(fdt.Rows[fiCount]["HWWKQTY"].ToString());
                fdHWHSBJAMT = fdHWHSBJAMT + Convert.ToDouble(fdt.Rows[fiCount]["HWHSBJAMT"].ToString());

                string sStyleString = "font-family: 굴림체; font-size: 10.25pt; font-weight: normal; text-align: right; ddo-char-set: 129";

                txtxHWWKQTYHAP.Style = sStyleString;
                txtHWHSBJAMTHAP.Style = sStyleString;

            }
            else
            {
                string sStyleString = "font-family: 굴림체; font-size: 11pt; font-weight: bold; text-align: right; ddo-char-set: 129";
                txtxHWWKQTYHAP.Style = sStyleString;
                txtHWHSBJAMTHAP.Style = sStyleString;

            }

            fiCount = fiCount + 1;

        }

        private void TYUSNJ016R1_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            fdt = dt;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            txtxHWWKQTYHAP.Text = String.Format("{0:#,##0.000}", fdHWWKQTY);
            txtHWHSBJAMTHAP.Text = String.Format("{0:#,##0}", fdHWHSBJAMT);

            this.groupFooter1.NewPage = NewPage.After;

            fdHWWKQTY = 0;
            fdHWHSBJAMT = 0;

        }
    }
}