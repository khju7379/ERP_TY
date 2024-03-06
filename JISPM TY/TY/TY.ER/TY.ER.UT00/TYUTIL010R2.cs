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

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTIL010R2.
    /// </summary>
    public partial class TYUTIL010R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdGASTTIMETOT = 0;
        private double fdDAHAPAMTTOT = 0;
        private double fdHAP = 0;
        private double fdGubn = 0;

        public TYUTIL010R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            //_dt = (DataTable)this.DataSource;

            //fdGASTTIMETOT = 0;
            //fdDAHAPAMTTOT = 0;
            //fdHAP = 0;

            //if (DAGUBUNNM1.Text == "상하단지")
            //{
            //    groupFooter1.Visible = false;
            //    groupFooter2.Visible = true;
            //}
            //else
            //{
            //    groupFooter1.Visible = true;
            //    groupFooter2.Visible = false;
            //}
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdGASTTIMETOT += Convert.ToDouble(_dt.Rows[fiCount]["GASTTIME"].ToString());
            fdDAHAPAMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["DAHAPAMT"].ToString());
            fdHAP += Convert.ToDouble(_dt.Rows[fiCount]["DAHAPAMT"].ToString());
            fiCount++;

            //if (fdGubn == 0)
            //{
            //    this.detail.NewPage = NewPage.None;
            //}
            //else
            //{
            //    this.detail.NewPage = NewPage.Before;
            //    fdGubn = 0;
            //}
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.GASTTIMETOT.Text = string.Format("{0:#,###}", fdGASTTIMETOT);
            this.DAHAPAMTTOT.Text = string.Format("{0:#,###}", fdDAHAPAMTTOT);

            //this.groupFooter1.NewPage = NewPage.After;
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            this.GASTTIMETOT1.Text = string.Format("{0:#,###}", fdGASTTIMETOT);
            this.HAP.Text = string.Format("{0:#,###}", fdHAP);
            this.UTDAHAPAMTTOT.Text = string.Format("{0:#,###}", fdHAP + Convert.ToDouble(_dt.Rows[0]["UTDAHAPAMT"].ToString()));
            this.groupFooter2.NewPage = NewPage.After;
            fdGubn = 1;
        }

        private void groupHeader2_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            fdGASTTIMETOT = 0;
            fdDAHAPAMTTOT = 0;
            fdHAP = 0;

            if (DAGUBUNNM1.Text == "상하단지")
            {
                groupFooter1.Visible = false;
                groupFooter2.Visible = true;
            }
            else
            {
                groupFooter1.Visible = true;
                groupFooter2.Visible = false;
            }

            this.groupFooter2.NewPage = NewPage.None;
        }
    }
}
