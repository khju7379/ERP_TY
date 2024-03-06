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
    /// Summary description for TYUTIL012R.
    /// </summary>
    public partial class TYUTIL012R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdCLAMTTOT1 = 0;
        private double fdPSAMTTOT1 = 0;
        private double fdDMAMTTOT1 = 0;
        private double fdCLELAMTTOT1 = 0;
        private double fdTOTAMTTOT1 = 0;

        private double fdCLAMTTOT2 = 0;
        private double fdPSAMTTOT2 = 0;
        private double fdDMAMTTOT2 = 0;
        private double fdCLELAMTTOT2 = 0;
        private double fdTOTAMTTOT2 = 0;
        private string fsDATE = string.Empty;

        public TYUTIL012R(string sDATE)
        {
            fsDATE = sDATE;
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            DAYYMM.Text = fsDATE;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdCLAMTTOT1 += Convert.ToDouble(_dt.Rows[fiCount]["CLAMT"].ToString());
            fdPSAMTTOT1 += Convert.ToDouble(_dt.Rows[fiCount]["PSAMT"].ToString());
            fdDMAMTTOT1 += Convert.ToDouble(_dt.Rows[fiCount]["DMAMT"].ToString());
            fdCLELAMTTOT1 += Convert.ToDouble(_dt.Rows[fiCount]["CLELAMT"].ToString());
            fdTOTAMTTOT1 += Convert.ToDouble(_dt.Rows[fiCount]["TOTAMT"].ToString());

            fdCLAMTTOT2 += Convert.ToDouble(_dt.Rows[fiCount]["CLAMT"].ToString());
            fdPSAMTTOT2 += Convert.ToDouble(_dt.Rows[fiCount]["PSAMT"].ToString());
            fdDMAMTTOT2 += Convert.ToDouble(_dt.Rows[fiCount]["DMAMT"].ToString());
            fdCLELAMTTOT2 += Convert.ToDouble(_dt.Rows[fiCount]["CLELAMT"].ToString());
            fdTOTAMTTOT2 += Convert.ToDouble(_dt.Rows[fiCount]["TOTAMT"].ToString());

            fiCount++;
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            this.CLAMTTOT1.Text = string.Format("{0:#,###}", fdCLAMTTOT1);
            this.PSAMTTOT1.Text = string.Format("{0:#,###}", fdPSAMTTOT1);
            this.DMAMTTOT1.Text = string.Format("{0:#,###}", fdDMAMTTOT1);
            this.CLELAMTTOT1.Text = string.Format("{0:#,##0}", fdCLELAMTTOT1);
            this.TOTAMTTOT1.Text = string.Format("{0:#,###}", fdTOTAMTTOT1);

            fdCLAMTTOT1 = 0;
            fdPSAMTTOT1 = 0;
            fdDMAMTTOT1 = 0;
            fdCLELAMTTOT1 = 0;
            fdTOTAMTTOT1 = 0;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.CLAMTTOT2.Text = string.Format("{0:#,###}", fdCLAMTTOT2);
            this.PSAMTTOT2.Text = string.Format("{0:#,###}", fdPSAMTTOT2);
            this.DMAMTTOT2.Text = string.Format("{0:#,###}", fdDMAMTTOT2);
            this.CLELAMTTOT2.Text = string.Format("{0:#,##0}", fdCLELAMTTOT2);
            this.TOTAMTTOT2.Text = string.Format("{0:#,###}", fdTOTAMTTOT2);

            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
