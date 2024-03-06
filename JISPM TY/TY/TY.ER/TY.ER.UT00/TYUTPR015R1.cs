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
    /// Summary description for TYUTPR015R1.
    /// </summary>
    public partial class TYUTPR015R1 : GrapeCity.ActiveReports.SectionReport
    {
        private double fdCHMT1SUM = 0;
        private double fdCHMT2SUM = 0;
        private double fdCHMT3SUM = 0;
        private double fdCHMT4SUM = 0;
        private double fdCHMT5SUM = 0;
        private double fdCHMT6SUM = 0;
        private double fdCHMT7SUM = 0;
        private double fdCHMT8SUM = 0;
        private double fdCHMT9SUM = 0;
        private double fdCHMT10SUM = 0;
        private double fdCHMT11SUM = 0;
        private double fdCHMT12SUM = 0;
        private double fdCHMT13SUM = 0;
        private double fdCHMT14SUM = 0;
        private double fdCHMT15SUM = 0;
        private double fdCHMT16SUM = 0;

        private double fdCHMTHAPSUM = 0;
        private double fdIPMTSUM = 0;
        private double fdJEGOSUM = 0;

        private int fiCount = 0;
        private DataTable _dt = new DataTable();

        public TYUTPR015R1()
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
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT1"].ToString()) > 0)
            {
                this.CHMT1.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT1"].ToString()));
            }
            else
            {
                this.CHMT1.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT2"].ToString()) > 0)
            {
                this.CHMT2.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT2"].ToString()));
            }
            else
            {
                this.CHMT2.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT3"].ToString()) > 0)
            {
                this.CHMT3.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT3"].ToString()));
            }
            else
            {
                this.CHMT3.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT4"].ToString()) > 0)
            {
                this.CHMT4.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT4"].ToString()));
            }
            else
            {
                this.CHMT4.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT5"].ToString()) > 0)
            {
                this.CHMT5.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT5"].ToString()));
            }
            else
            {
                this.CHMT5.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT6"].ToString()) > 0)
            {
                this.CHMT6.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT6"].ToString()));
            }
            else
            {
                this.CHMT6.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT7"].ToString()) > 0)
            {
                this.CHMT7.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT7"].ToString()));
            }
            else
            {
                this.CHMT7.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT8"].ToString()) > 0)
            {
                this.CHMT8.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT8"].ToString()));
            }
            else
            {
                this.CHMT8.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT9"].ToString()) > 0)
            {
                this.CHMT9.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT9"].ToString()));
            }
            else
            {
                this.CHMT9.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT10"].ToString()) > 0)
            {
                this.CHMT10.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT10"].ToString()));
            }
            else
            {
                this.CHMT10.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT11"].ToString()) > 0)
            {
                this.CHMT11.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT11"].ToString()));
            }
            else
            {
                this.CHMT11.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT12"].ToString()) > 0)
            {
                this.CHMT12.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT12"].ToString()));
            }
            else
            {
                this.CHMT12.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT13"].ToString()) > 0)
            {
                this.CHMT13.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT13"].ToString()));
            }
            else
            {
                this.CHMT13.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT14"].ToString()) > 0)
            {
                this.CHMT14.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT14"].ToString()));
            }
            else
            {
                this.CHMT14.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT15"].ToString()) > 0)
            {
                this.CHMT15.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT15"].ToString()));
            }
            else
            {
                this.CHMT15.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT16"].ToString()) > 0)
            {
                this.CHMT16.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT16"].ToString()));
            }
            else
            {
                this.CHMT16.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMTHAP"].ToString()) > 0)
            {
                this.CHMTHAP.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMTHAP"].ToString()));
            }
            else
            {
                this.CHMTHAP.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["IPMT"].ToString()) > 0)
            {
                this.IPMT.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["IPMT"].ToString()));
            }
            else
            {
                this.IPMT.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["JEGO"].ToString()) > 0)
            {
                this.JEGO.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["JEGO"].ToString()));
            }
            else
            {
                this.JEGO.Text = "";
            }


            fdCHMT1SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT1"].ToString());
            fdCHMT2SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT2"].ToString());
            fdCHMT3SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT3"].ToString());
            fdCHMT4SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT4"].ToString());
            fdCHMT5SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT5"].ToString());
            fdCHMT6SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT6"].ToString());
            fdCHMT7SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT7"].ToString());
            fdCHMT8SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT8"].ToString());
            fdCHMT9SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT9"].ToString());
            fdCHMT10SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT10"].ToString());
            fdCHMT11SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT11"].ToString());
            fdCHMT12SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT12"].ToString());
            fdCHMT13SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT13"].ToString());
            fdCHMT14SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT14"].ToString());
            fdCHMT15SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT15"].ToString());
            fdCHMT16SUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMT16"].ToString());

            fdCHMTHAPSUM += Convert.ToDouble(_dt.Rows[fiCount]["CHMTHAP"].ToString());
            fdIPMTSUM += Convert.ToDouble(_dt.Rows[fiCount]["IPMT"].ToString());

            fiCount++;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.CHMT1SUM.Text = string.Format("{0:#,##0.000}", fdCHMT1SUM);
            this.CHMT2SUM.Text = string.Format("{0:#,##0.000}", fdCHMT2SUM);
            this.CHMT3SUM.Text = string.Format("{0:#,##0.000}", fdCHMT3SUM);
            this.CHMT4SUM.Text = string.Format("{0:#,##0.000}", fdCHMT4SUM);
            this.CHMT5SUM.Text = string.Format("{0:#,##0.000}", fdCHMT5SUM);
            this.CHMT6SUM.Text = string.Format("{0:#,##0.000}", fdCHMT6SUM);
            this.CHMT7SUM.Text = string.Format("{0:#,##0.000}", fdCHMT7SUM);
            this.CHMT8SUM.Text = string.Format("{0:#,##0.000}", fdCHMT8SUM);
            this.CHMT9SUM.Text = string.Format("{0:#,##0.000}", fdCHMT9SUM);
            this.CHMT10SUM.Text = string.Format("{0:#,##0.000}", fdCHMT10SUM);
            this.CHMT11SUM.Text = string.Format("{0:#,##0.000}", fdCHMT11SUM);
            this.CHMT12SUM.Text = string.Format("{0:#,##0.000}", fdCHMT12SUM);
            this.CHMT13SUM.Text = string.Format("{0:#,##0.000}", fdCHMT13SUM);
            this.CHMT14SUM.Text = string.Format("{0:#,##0.000}", fdCHMT14SUM);
            this.CHMT15SUM.Text = string.Format("{0:#,##0.000}", fdCHMT15SUM);
            this.CHMT16SUM.Text = string.Format("{0:#,##0.000}", fdCHMT16SUM);

            this.CHMTHAPSUM.Text = string.Format("{0:#,##0.000}", fdCHMTHAPSUM);
            this.IPMTSUM.Text = string.Format("{0:#,##0.000}", fdIPMTSUM);
        }
    }
}
