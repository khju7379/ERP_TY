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
    /// Summary description for TYUTPR024R.
    /// </summary>
    public partial class TYUTPR024R : DataDynamics.ActiveReports.ActiveReport
    {
        private string fsIRUN;
        private string fsSDATE;
        private string fsEDATE;
        private DataTable _dt = new DataTable();

        public TYUTPR024R(string sIRUM, string sSDATE, string sEDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsIRUN = sIRUM;
            fsSDATE = sSDATE;
            fsEDATE = sEDATE;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            this.IRUM.Text = fsIRUN;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.CONTENT.Text = fsSDATE.Substring(0, 4) + "년 " + fsSDATE.Substring(4, 2) + "월 " + fsSDATE.Substring(6, 2) + "일 부터 " +
                                fsEDATE.Substring(0, 4) + "년 " + fsEDATE.Substring(4, 2) + "월 " + fsEDATE.Substring(6, 2) + "일 까지";

            this.YYYY.Text = System.DateTime.Now.ToString("yyyy");
            this.MM.Text = System.DateTime.Now.ToString("MM");
            this.DD.Text = System.DateTime.Now.ToString("dd");

            this.COMTQTYTOT.Text = string.Format("{0:#,##0.000}", _dt.Compute("SUM(COMTQTY)", null));
            this.COKLQTYTOT.Text = string.Format("{0:#,##0.000}", _dt.Compute("SUM(COKLQTY)", null));
            this.COOVQTYTOT.Text = string.Format("{0:#,##0.000}", _dt.Compute("SUM(COOVQTY)", null));
            this.COOVKLQTYTOT.Text = string.Format("{0:#,##0.000}", _dt.Compute("SUM(COOVKLQTY)", null));
        }
    }
}
