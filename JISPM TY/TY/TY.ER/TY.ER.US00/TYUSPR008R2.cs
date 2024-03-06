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
    /// Summary description for TYUSPR008R2.
    /// </summary>
    public partial class TYUSPR008R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable fdt;

        private double fdCHMTQTY = 0;
        private double fdCHMTQTYTOTAL = 0;
        private double fdCOUNT = 0;
        private int fiCount = 0;

        public TYUSPR008R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            line3.Visible = true;
            line5.Visible = false;

            //this.groupFooter1.NewPage = NewPage.After;
            if (fdt.Rows[fiCount]["CHGOKJONGNM"].ToString() == "" && fdt.Rows[fiCount]["CHHANGCHA"].ToString() == "")
            {
                line3.Visible = false;

                string sStyleString = "font-family: 굴림체; font-size: 11pt; font-weight: bold; text-align: right; ddo-char-set: 129";

                txtCHGOKJONG.Text = "";
                txtCHGOKJONGNM.Text = fdt.Rows[fiCount]["CHGOKJONGCODE"].ToString();

                txtCHGOKJONGNM.Style = sStyleString;
                txtCHMTQTY.Style = sStyleString;
                txtCOUNT.Style = sStyleString;
            }
            else if (fdt.Rows[fiCount]["CHHANGCHA"].ToString() != "")
            {
                fdCHMTQTY = fdCHMTQTY + Convert.ToDouble(fdt.Rows[fiCount]["CHMTQTY"].ToString());
                fdCOUNT = fdCOUNT + Convert.ToDouble(fdt.Rows[fiCount]["COUNT"].ToString());

                string sStyleString = "font-family: 굴림체; font-size: 10.25pt; font-weight: normal; text-align: right; ddo-char-set: 129";

                txtCHGOKJONGNM.Style = sStyleString;
                txtCHMTQTY.Style = sStyleString;
                txtCOUNT.Style = sStyleString;
            }
            else
            {
                string sStyleString = "font-family: 굴림체; font-size: 11pt; font-weight: bold; text-align: right; ddo-char-set: 129";
                txtCHGOKJONGNM.Style = sStyleString;
                txtCHMTQTY.Style = sStyleString;
                txtCOUNT.Style = sStyleString;
            }

            fiCount = fiCount + 1;
        }

        private void TYUSPR008R2_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            fdt = dt;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            //txtCHMTQTYHAP.Text = String.Format("{0:#,##0.000}", fdCHMTQTY);
            //txtCOUNTHAP.Text = Convert.ToString(fdCOUNT);

            line3.Visible = false;
            line5.Visible = true;

            fdCHMTQTYTOTAL = fdCHMTQTYTOTAL + fdCHMTQTY;

            this.groupFooter1.NewPage = NewPage.After;

            fdCHMTQTY = 0;
            fdCOUNT = 0;
        }


    }
}