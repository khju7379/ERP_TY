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
    /// Summary description for TYUTME027R.
    /// </summary>
    public partial class TYUTME027R : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private double fdGONGHAP;
        private int fiCount = 0;

        public TYUTME027R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            this.TMAMDRHAP1.Text = string.Format("{0:#,###}", _dt.Compute("SUM(TMAMDR)", null));
            this.TMAMDRHAP2.Text = string.Format("{0:#,###}", _dt.Compute("SUM(TMAMDR)", null));
            this.TMAMDRHAP3.Text = string.Format("{0:#,###}", _dt.Compute("SUM(TMAMDR)", null));
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_dt.Rows[fiCount]["TMCDAC"].ToString().Substring(0,2) == "41")
            {
                fdGONGHAP += Convert.ToDouble(_dt.Rows[fiCount]["TMAMDR"].ToString());
            }

            fiCount++;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.GONGHAP.Text = string.Format("{0:#,###}", fdGONGHAP);
        }

        private void TYUTME027R_DataInitialize(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            if (_dt != null)
            {
                //결재라인 사장, 대표사장 표시 
                if (Convert.ToDecimal(_dt.Rows[0]["TMDTMK"].ToString()) >= 20201201)
                {
                    lbl_GRADE1.Text = "대표 사장";
                    lbl_GRADE3.Text = "사   장";
                }
                else
                {
                    lbl_GRADE1.Text = "사   장";
                    lbl_GRADE3.Text = "부 사 장";
                }
            }
        }
    }
}
