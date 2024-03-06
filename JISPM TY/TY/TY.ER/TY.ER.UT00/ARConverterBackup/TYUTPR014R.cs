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
    /// Summary description for TYUTPR014R.
    /// </summary>
    public partial class TYUTPR014R : DataDynamics.ActiveReports.ActiveReport
    {
        private double dfJUBAN1TOT = 0;
        private double dfCMSHQTY1TOT = 0;
        private double dfJUBAN2TOT = 0;
        private double dfCMSHQTY2TOT = 0;
        private double dfJUBAN3TOT = 0;
        private double dfCMSHQTY3TOT = 0;
        private double dfJUBAN4TOT = 0;
        private double dfCMSHQTY4TOT = 0;
        private double dfJUBANSUM = 0;
        private double dfCMSHQTYSUM = 0;
        private double dfJUBANSUMTOT = 0;
        private double dfCMSHQTYSUMTOT = 0;

        private int fiCount = 0;
        private DataTable _dt = new DataTable();

        public TYUTPR014R()
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
            dfJUBAN1TOT += Convert.ToDouble(_dt.Rows[fiCount]["JUBAN1"].ToString());
            dfCMSHQTY1TOT += Convert.ToDouble(_dt.Rows[fiCount]["CMSHQTY1"].ToString());
            dfJUBAN2TOT += Convert.ToDouble(_dt.Rows[fiCount]["JUBAN2"].ToString());
            dfCMSHQTY2TOT += Convert.ToDouble(_dt.Rows[fiCount]["CMSHQTY2"].ToString());
            dfJUBAN3TOT += Convert.ToDouble(_dt.Rows[fiCount]["JUBAN3"].ToString());
            dfCMSHQTY3TOT += Convert.ToDouble(_dt.Rows[fiCount]["CMSHQTY3"].ToString());
            dfJUBAN4TOT += Convert.ToDouble(_dt.Rows[fiCount]["JUBAN4"].ToString());
            dfCMSHQTY4TOT += Convert.ToDouble(_dt.Rows[fiCount]["CMSHQTY4"].ToString());
            dfJUBANSUM = dfJUBAN1TOT + dfJUBAN2TOT + dfJUBAN3TOT + dfJUBAN4TOT;
            dfCMSHQTYSUM = dfCMSHQTY1TOT + dfCMSHQTY2TOT + dfCMSHQTY3TOT + dfCMSHQTY4TOT;
            dfJUBANSUMTOT += dfJUBANSUM;
            dfCMSHQTYSUMTOT += dfCMSHQTYSUM;

            this.JUBANSUM.Text = string.Format("{0:#,##0}", dfJUBANSUM);
            this.CMSHQTYSUM.Text = string.Format("{0:#,##0.000}", dfCMSHQTYSUM);

            fiCount++;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.JUBAN1TOT.Text = string.Format("{0:#,##0}", dfJUBAN1TOT);
            this.CMSHQTY1TOT.Text = string.Format("{0:#,##0.000}", dfCMSHQTY1TOT);
            this.JUBAN2TOT.Text = string.Format("{0:#,##0}", dfJUBAN2TOT);
            this.CMSHQTY2TOT.Text = string.Format("{0:#,##0.000}", dfCMSHQTY2TOT);
            this.JUBAN3TOT.Text = string.Format("{0:#,##0}", dfJUBAN3TOT);
            this.CMSHQTY3TOT.Text = string.Format("{0:#,##0.000}", dfCMSHQTY3TOT);
            this.JUBAN4TOT.Text = string.Format("{0:#,##0}", dfJUBAN4TOT);
            this.CMSHQTY4TOT.Text = string.Format("{0:#,##0.000}", dfCMSHQTY4TOT);
            this.JUBANSUMTOT.Text = string.Format("{0:#,##0}", dfJUBANSUMTOT);
            this.CMSHQTYSUMTOT.Text = string.Format("{0:#,##0.000}", dfCMSHQTYSUMTOT);
        }
    }
}
