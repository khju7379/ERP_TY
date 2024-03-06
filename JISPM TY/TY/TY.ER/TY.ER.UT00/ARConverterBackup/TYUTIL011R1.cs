using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTIL011R1.
    /// </summary>
    public partial class TYUTIL011R1 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdGAELTIMETOT = 0;
        private double fdGAKYQTYTOT = 0;
        private double fdGADKQTYTOT = 0;
        private double fdGASTTIMETOT = 0;
        private double fdGASTQTYTOT = 0;
        private string fsDATE = string.Empty;

        public TYUTIL011R1(string sDATE)
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
            fdGADKQTYTOT = 0;
            fdGASTTIMETOT = 0;
            fdGASTQTYTOT = 0;
            
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdGAELTIMETOT += Convert.ToDouble(_dt.Rows[fiCount]["GAELTIME"].ToString());
            fdGAKYQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["GAKYQTY"].ToString());
            fdGADKQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["GADKQTY"].ToString());
            fdGASTTIMETOT += Convert.ToDouble(_dt.Rows[fiCount]["GASTTIME"].ToString());
            fdGASTQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["GASTQTY"].ToString());
            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.GAELTIMETOT.Text = string.Format("{0:#,###}", fdGAELTIMETOT);
            this.GAKYQTYTOT.Text = string.Format("{0:#,###}", fdGAKYQTYTOT);
            this.GADKQTYTOT.Text = string.Format("{0:#,###}", fdGADKQTYTOT);
            this.GASTTIMETOT.Text = string.Format("{0:#,###}", fdGASTTIMETOT);
            this.GASTQTYTOT.Text = string.Format("{0:#,###}", fdGASTQTYTOT);

            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
