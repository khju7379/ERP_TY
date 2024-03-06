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
    /// Summary description for TYUTSU002R1.
    /// </summary>
    public partial class TYUTSU002R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;

        private double fdBOKAMTSUM1 = 0;
        private double fdHANDAMTSUM1 = 0;
        private double fdHAYAMTSUM1 = 0;
        private double fdJUBAMTSUM1 = 0;
        private double fdMAECHULSUM1 = 0;
        private double fdINSRAMTSUM1 = 0;
        private double fdHMAMTSUM1 = 0;
        private double fdTOTALSUM1 = 0;

        private double fdBOKAMTSUM2 = 0;
        private double fdHANDAMTSUM2 = 0;
        private double fdHAYAMTSUM2 = 0;
        private double fdJUBAMTSUM2 = 0;
        private double fdMAECHULSUM2 = 0;
        private double fdINSRAMTSUM2 = 0;
        private double fdHMAMTSUM2 = 0;
        private double fdTOTALSUM2 = 0;

        public TYUTSU002R1()
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
            fdBOKAMTSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["BOKAMT"].ToString());
            fdHANDAMTSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["HANDAMT"].ToString());
            fdHAYAMTSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["HAYAMT"].ToString());
            fdJUBAMTSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["JUBAMT"].ToString());
            fdMAECHULSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["MAECHUL"].ToString());
            fdINSRAMTSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["INSRAMT"].ToString());
            fdHMAMTSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["HMAMT"].ToString());

            fdTOTALSUM1 += Convert.ToDouble(_dt.Rows[fiCount]["TOTAL"].ToString());

            fdBOKAMTSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["BOKAMT"].ToString());
            fdHANDAMTSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["HANDAMT"].ToString());
            fdHAYAMTSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["HAYAMT"].ToString());
            fdJUBAMTSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["JUBAMT"].ToString());
            fdMAECHULSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["MAECHUL"].ToString());
            fdINSRAMTSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["INSRAMT"].ToString());
            fdHMAMTSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["HMAMT"].ToString());
            fdTOTALSUM2 += Convert.ToDouble(_dt.Rows[fiCount]["TOTAL"].ToString());

            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.BOKAMTSUM1.Text = string.Format("{0:#,##0}", fdBOKAMTSUM1);
            this.HANDAMTSUM1.Text = string.Format("{0:#,##0}", fdHANDAMTSUM1);
            this.HAYAMTSUM1.Text = string.Format("{0:#,##0}", fdHAYAMTSUM1);
            this.JUBAMTSUM1.Text = string.Format("{0:#,##0}", fdJUBAMTSUM1);
            this.MAECHULSUM1.Text = string.Format("{0:#,##0}", fdMAECHULSUM1);
            this.INSRAMTSUM1.Text = string.Format("{0:#,##0}", fdINSRAMTSUM1);
            this.HMAMTSUM1.Text = string.Format("{0:#,##0}", fdHMAMTSUM1);
            this.TOTALSUM1.Text = string.Format("{0:#,##0}", fdTOTALSUM1);

            fdBOKAMTSUM1 = 0;
            fdHANDAMTSUM1 = 0;
            fdHAYAMTSUM1 = 0;
            fdJUBAMTSUM1 = 0;
            fdMAECHULSUM1 = 0;
            fdINSRAMTSUM1 = 0;
            fdHMAMTSUM1 = 0;
            fdTOTALSUM1 = 0;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.BOKAMTSUM2.Text = string.Format("{0:#,##0}", fdBOKAMTSUM2);
            this.HANDAMTSUM2.Text = string.Format("{0:#,##0}", fdHANDAMTSUM2);
            this.HAYAMTSUM2.Text = string.Format("{0:#,##0}", fdHAYAMTSUM2);
            this.JUBAMTSUM2.Text = string.Format("{0:#,##0}", fdJUBAMTSUM2);
            this.MAECHULSUM2.Text = string.Format("{0:#,##0}", fdMAECHULSUM2);
            this.INSRAMTSUM2.Text = string.Format("{0:#,##0}", fdINSRAMTSUM2);
            this.HMAMTSUM2.Text = string.Format("{0:#,##0}", fdHMAMTSUM2);
            this.TOTALSUM2.Text = string.Format("{0:#,##0}", fdTOTALSUM2);
        }
    }
}
