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
    /// Summary description for TYUTME006R.
    /// </summary>
    public partial class TYUTME006R : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private double fdISGAMAMTSUB = 0;
        private double fdISISAMTSUB = 0;

        public TYUTME006R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            _dt =  this.DataSource as DataTable;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (fiCount > _dt.Rows.Count-1)
            {
                fiCount = _dt.Rows.Count-1;
            }
            this.ISCHULIL.Text = "(" + _dt.Rows[fiCount]["ISCHULIL"].ToString().Substring(0, 4) + "/" + _dt.Rows[fiCount]["ISCHULIL"].ToString().Substring(4, 2) + "/" + _dt.Rows[fiCount]["ISCHULIL"].ToString().Substring(6, 2) + ")";
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdISGAMAMTSUB += Convert.ToDouble(_dt.Rows[fiCount]["ISGAMAMT"].ToString());
            fdISISAMTSUB += Convert.ToDouble(_dt.Rows[fiCount]["ISISAMT"].ToString());

            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {

            this.ISGAMAMTSUB.Text = string.Format("{0:#,##0}", fdISGAMAMTSUB);
            this.ISISAMTSUB.Text = string.Format("{0:#,##0}", fdISISAMTSUB);

            fdISGAMAMTSUB = 0;
            fdISISAMTSUB = 0;

            if (fiCount == _dt.Rows.Count)
            {
                this.groupFooter1.NewPage = NewPage.None;
            }
            else
            {
                this.groupFooter1.NewPage = NewPage.After;
            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.ISGAMAMTGRAND.Text = string.Format("{0:#,##0}", _dt.Compute("SUM(ISGAMAMT)", null));
            this.ISISAMTGRAND.Text = string.Format("{0:#,##0}", _dt.Compute("SUM(ISISAMT)", null));
        }
    }
}
