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
    /// Summary description for TYUTPR011R.
    /// </summary>
    public partial class TYUTPR011R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdSV01HAP = 0;
        private double fdSV02HAP = 0;
        private double fdSV03HAP = 0;
        private double fdSV04HAP = 0;
        private double fdSV05HAP = 0;
        private double fdSV06HAP = 0;
        private double fdSV07HAP = 0;
        private double fdSV08HAP = 0;
        private double fdSV09HAP = 0;
        private double fdSV10HAP = 0;
        private double fdSV11HAP = 0;
        private double fdSV12HAP = 0;
        private double fdTOTAL = 0;
        private double fdBIYUL = 0;

        public TYUTPR011R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (fiCount == 0)
            {
                _dt = (DataTable)this.DataSource;
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdSV01HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV01"].ToString());
            fdSV02HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV02"].ToString());
            fdSV03HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV03"].ToString());
            fdSV04HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV04"].ToString());
            fdSV05HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV05"].ToString());
            fdSV06HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV06"].ToString());
            fdSV07HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV07"].ToString());
            fdSV08HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV08"].ToString());
            fdSV09HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV09"].ToString());
            fdSV10HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV10"].ToString());
            fdSV11HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV11"].ToString());
            fdSV12HAP += Convert.ToDouble(_dt.Rows[fiCount]["SV12"].ToString());
            fdTOTAL += Convert.ToDouble(_dt.Rows[fiCount]["HAP"].ToString());

            if (Convert.ToDouble(_dt.Rows[fiCount]["TNCAPA"].ToString()) != 0)
            {
                double dTmp = Convert.ToDouble(_dt.Rows[fiCount]["HAP"].ToString()) / Convert.ToDouble(_dt.Rows[fiCount]["CAPA"].ToString()) / Convert.ToDouble(_dt.Rows[fiCount]["BIYUL"].ToString());
                this.BIYUL.Text = string.Format("{0:#,##0.00}", dTmp);
                fdBIYUL += dTmp;
            }
            else
            {
                this.BIYUL.Text = "";
            }

            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.SV01HAP.Text = string.Format("{0:#,##0.000}", fdSV01HAP);
            this.SV02HAP.Text = string.Format("{0:#,##0.000}", fdSV02HAP);
            this.SV03HAP.Text = string.Format("{0:#,##0.000}", fdSV03HAP);
            this.SV04HAP.Text = string.Format("{0:#,##0.000}", fdSV04HAP);
            this.SV05HAP.Text = string.Format("{0:#,##0.000}", fdSV05HAP);
            this.SV06HAP.Text = string.Format("{0:#,##0.000}", fdSV06HAP);
            this.SV07HAP.Text = string.Format("{0:#,##0.000}", fdSV07HAP);
            this.SV08HAP.Text = string.Format("{0:#,##0.000}", fdSV08HAP);
            this.SV09HAP.Text = string.Format("{0:#,##0.000}", fdSV09HAP);
            this.SV10HAP.Text = string.Format("{0:#,##0.000}", fdSV10HAP);
            this.SV11HAP.Text = string.Format("{0:#,##0.000}", fdSV11HAP);
            this.SV12HAP.Text = string.Format("{0:#,##0.000}", fdSV12HAP);
            this.TOTAL.Text = string.Format("{0:#,##0.000}", fdTOTAL);
            this.BIYULHAP.Text = string.Format("{0:#,##0}", fdBIYUL);

            fdSV01HAP = 0;
            fdSV02HAP = 0;
            fdSV03HAP = 0;
            fdSV04HAP = 0;
            fdSV05HAP = 0;
            fdSV06HAP = 0;
            fdSV07HAP = 0;
            fdSV08HAP = 0;
            fdSV09HAP = 0;
            fdSV10HAP = 0;
            fdTOTAL = 0;
            fdBIYUL = 0;
        }
    }
}
