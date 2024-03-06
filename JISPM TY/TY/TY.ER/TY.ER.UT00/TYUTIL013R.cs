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
    /// Summary description for TYUTIL013R.
    /// </summary>
    public partial class TYUTIL013R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdYOGUMAIKTOT = 0;
        private double fdPSAMTTOT = 0;
        private double fdDRAMTTOT = 0;
        private double fdCLELAMTTOT = 0;
        private double fdTOTAMTTOT = 0;
        private double fdTOTAMT = 0;

        public TYUTIL013R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdTOTAMT = Convert.ToDouble(_dt.Rows[fiCount]["YOGUMAIK"].ToString()) + Convert.ToDouble(_dt.Rows[fiCount]["PSAMT"].ToString()) + Convert.ToDouble(_dt.Rows[fiCount]["DRAMT"].ToString()) + Convert.ToDouble(_dt.Rows[fiCount]["CLELAMT"].ToString());

            this.TOTAMT.Text = string.Format("{0:#,###}", fdTOTAMT); ;

            fdYOGUMAIKTOT += Convert.ToDouble(_dt.Rows[fiCount]["YOGUMAIK"].ToString());
            fdPSAMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["PSAMT"].ToString());
            fdDRAMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["DRAMT"].ToString());
            fdCLELAMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["CLELAMT"].ToString());
            fdTOTAMTTOT += fdTOTAMT;
            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.YOGUMAIKTOT.Text = string.Format("{0:#,###}", fdYOGUMAIKTOT);
            this.PSAMTTOT.Text = string.Format("{0:#,###}", fdPSAMTTOT);
            this.DRAMTTOT.Text = string.Format("{0:#,###}", fdDRAMTTOT);
            this.CLELAMTTOT.Text = string.Format("{0:#,##0}", fdCLELAMTTOT);
            this.TOTAMTTOT.Text = string.Format("{0:#,###}", fdTOTAMTTOT);
        }
    }
}
