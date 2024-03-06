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
    /// Summary description for TYUTME030R1.
    /// </summary>
    public partial class TYUTME030R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable fdt;
        private string fsDATE;
        private int fiCount = 0;

        public TYUTME030R1(string sDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsDATE = sDATE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            fdt = this.DataSource as DataTable;
            this.DATE.Text = fsDATE.Substring(0, 4) + "년 " + fsDATE.Substring(4, 2) + "월 " + fsDATE.Substring(6, 2) + "일";
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.M1IPHANG1.Text = fdt.Rows[fiCount]["M1IPHANG"].ToString().Substring(0, 4) + "년 " + fdt.Rows[fiCount]["M1IPHANG"].ToString().Substring(5, 2) + "월 " + fdt.Rows[fiCount]["M1IPHANG"].ToString().Substring(8, 2) + "일";
            this.M1EDIPGO.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(fdt.Rows[fiCount]["M1EDIPGO"].ToString())) + " M/T (" + string.Format("{0:#,##0.000}", Convert.ToDouble(fdt.Rows[fiCount]["M1EDBBLS"].ToString())) + " BBLS)";

            fiCount++;

            if ((fiCount % 2) == 0)
            {
                line43.Visible = false;
            }
            else
            {
                line43.Visible = true;
            }
        }
    }
}
