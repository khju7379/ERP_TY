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
    /// Summary description for TYUTIL007R.
    /// </summary>
    public partial class TYUTIL007R : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private int fiDCount = 0;
        private double fdPRJUNMTTOT = 0;
        private double fdPRIPMTTOT = 0;
        private double fdPRCHMTTOT = 0;
        private double fdPRJEMTTOT = 0;

        public TYUTIL007R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (fiCount == 0)
            {
                _dt = (DataTable)this.DataSource;
            }
            else
            {
                if (fiCount < _dt.Rows.Count && fiDCount != 0)
                {
                    if (_dt.Rows[fiCount - 1]["PEHWAMULNM"].ToString() != _dt.Rows[fiCount]["PEHWAMULNM"].ToString())
                    {
                        this.PEHWAMULNM.Text = _dt.Rows[fiCount - 1]["PEHWAMULNM"].ToString();
                        this.PRGUBUN.Text = _dt.Rows[fiCount - 1]["PRGUBUN"].ToString();
                    }
                }
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdPRJUNMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["PRJUNMT"].ToString());
            fdPRIPMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["PRIPMT"].ToString());
            fdPRCHMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["PRCHMT"].ToString());
            fdPRJEMTTOT += Convert.ToDouble(_dt.Rows[fiCount]["PRJEMT"].ToString());
            fiCount++;
            fiDCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.PRJUNMTTOT.Text = string.Format("{0:#,##0.000}", fdPRJUNMTTOT);
            this.PRIPMTTOT.Text = string.Format("{0:#,##0.000}", fdPRIPMTTOT);
            this.PRCHMTTOT.Text = string.Format("{0:#,##0.000}", fdPRCHMTTOT);
            this.PRJEMTTOT.Text = string.Format("{0:#,##0.000}", fdPRJEMTTOT);

            fdPRJUNMTTOT = 0;
            fdPRIPMTTOT = 0;
            fdPRCHMTTOT = 0;
            fdPRJEMTTOT = 0;

            fiDCount = 0;

            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
