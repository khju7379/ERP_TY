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
    /// Summary description for TYUSME031R.
    /// </summary>
    public partial class TYUSME031R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable fdt;

        private int fiCount = 0;
        private string sDATE = string.Empty;

        public TYUSME031R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYUSME031R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            fdt = dt;

            sDATE = fdt.Rows[fiCount]["DATE"].ToString();

            this.TITLE2.Text = "(" + sDATE.Substring(0, 4) + "/" + sDATE.Substring(4, 2) + "/" + sDATE.Substring(6, 2) + "일 기준)";

            fiCount = fiCount + 1;
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            this.groupHeader1.NewPage = NewPage.Before;
        }
    }
}