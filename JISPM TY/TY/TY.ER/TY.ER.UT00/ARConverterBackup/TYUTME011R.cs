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
    /// Summary description for TYUTME011R.
    /// </summary>
    public partial class TYUTME011R : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdCOUNTTOT = 0;
        private double fdISGAMAMTTOT = 0;
        private double fdISISAMTTOT = 0;

        public TYUTME011R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdCOUNTTOT += Convert.ToDouble(_dt.Rows[fiCount]["COUNT"].ToString());
            fdISGAMAMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["ISGAMAMT"].ToString());
            fdISISAMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["ISISAMT"].ToString());

            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.COUNTTOT.Text = string.Format("{0:#,##0}", fdCOUNTTOT);
            this.ISGAMAMTTOT.Text = string.Format("{0:#,##0}", fdISGAMAMTTOT);
            this.ISISAMTTOT.Text = string.Format("{0:#,##0}", fdISISAMTTOT);

            fdCOUNTTOT = 0;
            fdISGAMAMTTOT = 0;
            fdISISAMTTOT = 0;

            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
