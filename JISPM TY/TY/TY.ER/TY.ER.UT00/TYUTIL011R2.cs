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
    /// Summary description for TYUTIL011R2.
    /// </summary>
    public partial class TYUTIL011R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdGAELTIMETOT = 0;
        private double fdGAKYQTYTOT = 0;
        private double fdGASTTIMETOT = 0;
        private double fdGASTQTYTOT = 0;
        private string fsDATE = string.Empty;

        private double fdHAPELAMT = 0;
        private double fdHAPKYAMT = 0;
        private double fdHAPSTAMT = 0;

        public TYUTIL011R2(string sDATE)
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

            fdGAELTIMETOT = 0;
            fdGAKYQTYTOT = 0;
            fdGASTTIMETOT = 0;
            fdGASTQTYTOT = 0;


            fdHAPELAMT = 0;
            fdHAPKYAMT = 0;
            fdHAPSTAMT = 0;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdGAELTIMETOT += Convert.ToDouble(_dt.Rows[fiCount]["GAELTIME"].ToString());
            fdGAKYQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["GAKYQTY"].ToString());
            fdGASTTIMETOT += Convert.ToDouble(_dt.Rows[fiCount]["GASTTIME"].ToString());
            fdGASTQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["GASTQTY"].ToString());

            fdHAPELAMT = Convert.ToDouble(_dt.Rows[fiCount]["HAPELAMT"].ToString());
            fdHAPKYAMT = Convert.ToDouble(_dt.Rows[fiCount]["HAPKYAMT"].ToString());
            fdHAPSTAMT = Convert.ToDouble(_dt.Rows[fiCount]["HAPSTAMT"].ToString());

            fiCount++;


        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.GAELTIMETOT.Text = string.Format("{0:#,###}", fdGAELTIMETOT);
            this.GAKYQTYTOT.Text = string.Format("{0:#,###}", fdGAKYQTYTOT);
            this.GASTTIMETOT.Text = string.Format("{0:#,###}", fdGASTTIMETOT);
            this.GASTQTYTOT.Text = string.Format("{0:#,###}", fdGASTQTYTOT);
            this.DATOTAMT.Text = string.Format("{0:#,###}", fdHAPELAMT + fdHAPKYAMT + fdHAPSTAMT);

            this.groupFooter1.NewPage = NewPage.After;
        }


    }
}
