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
    /// Summary description for TYUTPR013R.
    /// </summary>
    public partial class TYUTPR013R : DataDynamics.ActiveReports.ActiveReport
    {
        private double fdSVMTQTYSUM = 0;
        private double fdSVCHULQTYSUM = 0;
        private double fdJEGOSUM = 0;

        private double fdSVMTQTYTOT = 0;
        private double fdSVCHULQTYTOT = 0;
        private double fdJEGOTOT = 0;

        private int fiCount = 0;
        private DataTable _dt = new DataTable();

        public TYUTPR013R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
            this.DATE.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdSVMTQTYSUM += Convert.ToDouble(_dt.Rows[fiCount]["SVMTQTY"].ToString());
            fdSVCHULQTYSUM += Convert.ToDouble(_dt.Rows[fiCount]["SVCHULQTY"].ToString());
            fdJEGOSUM += Convert.ToDouble(_dt.Rows[fiCount]["JEGO"].ToString());

            fdSVMTQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["SVMTQTY"].ToString());
            fdSVCHULQTYTOT += Convert.ToDouble(_dt.Rows[fiCount]["SVCHULQTY"].ToString());
            fdJEGOTOT += Convert.ToDouble(_dt.Rows[fiCount]["JEGO"].ToString());

            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.SVMTQTYSUM.Text = string.Format("{0:#,##0.000}", fdSVMTQTYSUM);
            this.SVCHULQTYSUM.Text = string.Format("{0:#,##0.000}", fdSVCHULQTYSUM);
            this.JEGOSUM.Text = string.Format("{0:#,##0.000}", fdJEGOSUM);

            fdSVMTQTYSUM = 0;
            fdSVCHULQTYSUM = 0;
            fdJEGOSUM = 0;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.SVMTQTYTOT.Text = string.Format("{0:#,##0.000}", fdSVMTQTYTOT);
            this.SVCHULQTYTOT.Text = string.Format("{0:#,##0.000}", fdSVCHULQTYTOT);
            this.JEGOTOT.Text = string.Format("{0:#,##0.000}", fdJEGOTOT);
        }
    }
}
