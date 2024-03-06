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
    /// Summary description for TYUSNJ018R2.
    /// </summary>
    public partial class TYUSNJ018R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable fdt;

        private double fdUSUSEDAMT = 0;
        private double fdJGHWAKAMT = 0;
        private double fdHWWKQTY = 0;
        private double fdHWJKTIME = 0;
        private double fdHWINWON = 0;

        private double fdUSUSEDAMTHAP = 0;
        private double fdJGHWAKAMTHAP = 0;
        private double fdHWWKQTYHAP = 0;
        private double fdHWJKTIMEHAP = 0;
        private double fdHWINWONHAP = 0;


        private int fiCount = 0;

        public TYUSNJ018R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {

            fdUSUSEDAMT = fdUSUSEDAMT + Convert.ToDouble(fdt.Rows[fiCount]["USUSEDAMT"].ToString());
            fdJGHWAKAMT = fdJGHWAKAMT + Convert.ToDouble(fdt.Rows[fiCount]["JGHWAKAMT"].ToString());
            fdHWWKQTY = fdHWWKQTY + Convert.ToDouble(fdt.Rows[fiCount]["HWWKQTY"].ToString());
            fdHWJKTIME = fdHWJKTIME + Convert.ToDouble(fdt.Rows[fiCount]["HWJKTIME"].ToString());
            fdHWINWON = fdHWINWON + Convert.ToDouble(fdt.Rows[fiCount]["HWINWON"].ToString());

            if (fiCount < fdt.Rows.Count - 1)
            {
                fiCount = fiCount + 1;
            }
        }

        private void TYUSNJ018R2_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            fdt = dt;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            txtUSUSEDAMT_Total.Text = String.Format("{0:#,##0}", fdUSUSEDAMTHAP);
            txtJGHWAKAMT_Total.Text = String.Format("{0:#,##0}", fdJGHWAKAMTHAP);
            txtHWWKQTY_Total.Text = String.Format("{0:#,##0.000}", fdHWWKQTYHAP);
            txtHWINWON_Total.Text = String.Format("{0:#,##0}", fdHWINWONHAP);
            txtHWJKTIME_Total.Text = String.Format("{0:#,##0}", fdHWJKTIMEHAP);

            fdUSUSEDAMTHAP = 0;
            fdJGHWAKAMTHAP = 0;
            fdHWWKQTYHAP = 0;
            fdHWJKTIMEHAP = 0;
            fdHWINWONHAP = 0;

            if (fiCount != 0)
            {
                if (fdt.Rows[fiCount - 1]["HWYYMM"].ToString() != fdt.Rows[fiCount]["HWYYMM"].ToString())
                {
                    this.groupFooter1.NewPage = NewPage.After;
                }
            }
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            txtUSUSEDAMT_Sub.Text = String.Format("{0:#,##0}", fdUSUSEDAMT);
            txtJGHWAKAMT_Sub.Text = String.Format("{0:#,##0}", fdJGHWAKAMT);
            txtHWWKQTY_Sub.Text = String.Format("{0:#,##0.000}", fdHWWKQTY);
            txtHWINWON_Sub.Text = String.Format("{0:#,##0}", fdHWINWON);
            txtHWJKTIME_Sub.Text = String.Format("{0:#,##0}", fdHWJKTIME);

            fdUSUSEDAMTHAP = fdUSUSEDAMTHAP + fdUSUSEDAMT;
            fdJGHWAKAMTHAP = fdJGHWAKAMTHAP + fdJGHWAKAMT;
            fdHWWKQTYHAP = fdHWWKQTYHAP + fdHWWKQTY;
            fdHWJKTIMEHAP = fdHWJKTIMEHAP + fdHWJKTIME;
            fdHWINWONHAP = fdHWINWONHAP + fdHWINWON;

            fdUSUSEDAMT = 0;
            fdJGHWAKAMT = 0;
            fdHWWKQTY = 0;
            fdHWJKTIME = 0;
            fdHWINWON = 0;
        }


    }
}