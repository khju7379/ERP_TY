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
    /// Summary description for TYUTSU001R.
    /// </summary>
    public partial class TYUTSU001R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdT9BOKAMT = 0;
        private double fdT9HANDAMT = 0;
        private double fdT9HAYAMT = 0;
        private double fdT9JUBAMT = 0;
        private double fdTOTAL = 0;
        private double fdT9SUBVAT = 0;
        private double fdT9INSRAMT = 0;
        private double fdINSRAMT = 0;

        public TYUTSU001R()
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
            fdT9BOKAMT += Convert.ToDouble(_dt.Rows[fiCount]["T9BOKAMT"].ToString());
            fdT9HANDAMT += Convert.ToDouble(_dt.Rows[fiCount]["T9HANDAMT"].ToString());
            fdT9HAYAMT += Convert.ToDouble(_dt.Rows[fiCount]["T9HAYAMT"].ToString());
            fdT9JUBAMT += Convert.ToDouble(_dt.Rows[fiCount]["T9JUBAMT"].ToString());
            fdTOTAL += Convert.ToDouble(_dt.Rows[fiCount]["TOTAL"].ToString());
            fdT9SUBVAT += Convert.ToDouble(_dt.Rows[fiCount]["T9SUBVAT"].ToString());
            fdT9INSRAMT += Convert.ToDouble(_dt.Rows[fiCount]["T9INSRAMT"].ToString());
            fdINSRAMT += Convert.ToDouble(_dt.Rows[fiCount]["INSRAMT"].ToString());

            fiCount++;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.T9BOKAMTTOT.Text = string.Format("{0:#,###}", fdT9BOKAMT);
            this.T9HANDAMTTOT.Text = string.Format("{0:#,###}", fdT9HANDAMT);
            this.T9HAYAMTTOT.Text = string.Format("{0:#,###}", fdT9HAYAMT);
            this.T9JUBAMTTOT.Text = string.Format("{0:#,###}", fdT9JUBAMT);
            this.TOTALTOT.Text = string.Format("{0:#,###}", fdTOTAL);
            this.T9SUBVATTOT.Text = string.Format("{0:#,###}", fdT9SUBVAT);
            this.T9INSRAMTTOT.Text = string.Format("{0:#,###}", fdT9INSRAMT);
            this.INSRAMTTOT.Text = string.Format("{0:#,###}", fdINSRAMT);
        }
    }
}
