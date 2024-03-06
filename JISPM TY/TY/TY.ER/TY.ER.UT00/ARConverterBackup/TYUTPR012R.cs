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
    /// Summary description for TYUTPR012R.
    /// </summary>
    public partial class TYUTPR012R : DataDynamics.ActiveReports.ActiveReport
    {
        private double fdTMMTQTYHJ = 0;
        private double fdTMCOVQTYHJ = 0;
        private double fdTMCUQTYHJ = 0;
        private double fdTMCHCHQTYHJ = 0;
        private double fdTMCHOVQTYHJ = 0;

        private double fdTMMTQTYSUM = 0;
        private double fdTMCOVQTYSUM = 0;
        private double fdTMCUQTYSUM = 0;
        private double fdTMCHCHQTYSUM = 0;
        private double fdTMCHOVQTYSUM = 0;

        private int fiCount = 0;
        private DataTable _dt = new DataTable();

        public TYUTPR012R()
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
            fdTMMTQTYHJ += Convert.ToDouble(_dt.Rows[fiCount]["TMMTQTY"].ToString());
            fdTMCOVQTYHJ += Convert.ToDouble(_dt.Rows[fiCount]["TMCOVQTY"].ToString());
            fdTMCUQTYHJ += Convert.ToDouble(_dt.Rows[fiCount]["TMCUQTY"].ToString());
            fdTMCHCHQTYHJ += Convert.ToDouble(_dt.Rows[fiCount]["TMCHCHQTY"].ToString());
            fdTMCHOVQTYHJ += Convert.ToDouble(_dt.Rows[fiCount]["TMCHOVQTY"].ToString());

            fdTMMTQTYSUM += Convert.ToDouble(_dt.Rows[fiCount]["TMMTQTY"].ToString());
            fdTMCOVQTYSUM += Convert.ToDouble(_dt.Rows[fiCount]["TMCOVQTY"].ToString());
            fdTMCUQTYSUM += Convert.ToDouble(_dt.Rows[fiCount]["TMCUQTY"].ToString());
            fdTMCHCHQTYSUM += Convert.ToDouble(_dt.Rows[fiCount]["TMCHCHQTY"].ToString());
            fdTMCHOVQTYSUM += Convert.ToDouble(_dt.Rows[fiCount]["TMCHOVQTY"].ToString());

            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.TMMTQTYHJ.Text = string.Format("{0:#,##0.000}", fdTMMTQTYHJ);
            this.TMCOVQTYHJ.Text = string.Format("{0:#,##0.000}", fdTMCOVQTYHJ);
            this.TMCUQTYHJ.Text = string.Format("{0:#,##0.000}", fdTMCUQTYHJ);
            this.TMCHCHQTYHJ.Text = string.Format("{0:#,##0.000}", fdTMCHCHQTYHJ);
            this.TMCHOVQTYHJ.Text = string.Format("{0:#,##0.000}", fdTMCHOVQTYHJ);

            fdTMMTQTYHJ = 0;
            fdTMCOVQTYHJ = 0;
            fdTMCUQTYHJ = 0;
            fdTMCHCHQTYHJ = 0;
            fdTMCHOVQTYHJ = 0;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.TMMTQTYSUM.Text = string.Format("{0:#,##0.000}", fdTMMTQTYSUM);
            this.TMCOVQTYSUM.Text = string.Format("{0:#,##0.000}", fdTMCOVQTYSUM);
            this.TMCUQTYSUM.Text = string.Format("{0:#,##0.000}", fdTMCUQTYSUM);
            this.TMCHCHQTYSUM.Text = string.Format("{0:#,##0.000}", fdTMCHCHQTYSUM);
            this.TMCHOVQTYSUM.Text = string.Format("{0:#,##0.000}", fdTMCHOVQTYSUM);
        }
    }
}
