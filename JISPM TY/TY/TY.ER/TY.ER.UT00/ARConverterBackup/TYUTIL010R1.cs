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
    /// Summary description for TYUTIL010R1.
    /// </summary>
    public partial class TYUTIL010R1 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdGASTTIMETOT = 0;
        private double fdGASTQTYTOT = 0;
        private double fdDAHAPAMTTOT = 0;

        public TYUTIL010R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            fdGASTTIMETOT = 0;
            fdGASTQTYTOT = 0;
            fdDAHAPAMTTOT = 0;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdGASTTIMETOT += Convert.ToDouble(_dt.Rows[fiCount]["GASTTIME"].ToString());
            fdGASTQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["GASTQTY"].ToString());
            fdDAHAPAMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["DAHAPAMT"].ToString());
            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.GASTTIMETOT.Text = string.Format("{0:#,###}", fdGASTTIMETOT);
            this.GASTQTYTOT.Text = string.Format("{0:#,##0.000}", fdGASTQTYTOT);
            this.DAHAPAMTTOT.Text = string.Format("{0:#,###}", fdDAHAPAMTTOT);

            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
