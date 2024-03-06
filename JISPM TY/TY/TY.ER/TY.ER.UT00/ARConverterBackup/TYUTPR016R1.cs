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
    /// Summary description for TYUTPR016R1.
    /// </summary>
    public partial class TYUTPR016R1 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private double fsCHMTHAP = 0;
        private int fiCount = 0;

        public TYUTPR016R1()
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
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT1"].ToString()) > 0)
            {
                this.CHMT1.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT1"].ToString()));
            }
            else
            {
                this.CHMT1.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT2"].ToString()) > 0)
            {
                this.CHMT2.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT2"].ToString()));
            }
            else
            {
                this.CHMT2.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT3"].ToString()) > 0)
            {
                this.CHMT3.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT3"].ToString()));
            }
            else
            {
                this.CHMT3.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT4"].ToString()) > 0)
            {
                this.CHMT4.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT4"].ToString()));
            }
            else
            {
                this.CHMT4.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT5"].ToString()) > 0)
            {
                this.CHMT5.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT5"].ToString()));
            }
            else
            {
                this.CHMT5.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT6"].ToString()) > 0)
            {
                this.CHMT6.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT6"].ToString()));
            }
            else
            {
                this.CHMT6.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT7"].ToString()) > 0)
            {
                this.CHMT7.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT7"].ToString()));
            }
            else
            {
                this.CHMT7.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT8"].ToString()) > 0)
            {
                this.CHMT8.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT8"].ToString()));
            }
            else
            {
                this.CHMT8.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT9"].ToString()) > 0)
            {
                this.CHMT9.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT9"].ToString()));
            }
            else
            {
                this.CHMT9.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT10"].ToString()) > 0)
            {
                this.CHMT10.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT10"].ToString()));
            }
            else
            {
                this.CHMT10.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT11"].ToString()) > 0)
            {
                this.CHMT11.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT11"].ToString()));
            }
            else
            {
                this.CHMT11.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT12"].ToString()) > 0)
            {
                this.CHMT12.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT12"].ToString()));
            }
            else
            {
                this.CHMT12.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT13"].ToString()) > 0)
            {
                this.CHMT13.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT13"].ToString()));
            }
            else
            {
                this.CHMT13.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT14"].ToString()) > 0)
            {
                this.CHMT14.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT14"].ToString()));
            }
            else
            {
                this.CHMT14.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT15"].ToString()) > 0)
            {
                this.CHMT15.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT15"].ToString()));
            }
            else
            {
                this.CHMT15.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT16"].ToString()) > 0)
            {
                this.CHMT16.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT16"].ToString()));
            }
            else
            {
                this.CHMT16.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT17"].ToString()) > 0)
            {
                this.CHMT17.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT17"].ToString()));
            }
            else
            {
                this.CHMT17.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT18"].ToString()) > 0)
            {
                this.CHMT18.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT18"].ToString()));
            }
            else
            {
                this.CHMT18.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT19"].ToString()) > 0)
            {
                this.CHMT19.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT19"].ToString()));
            }
            else
            {
                this.CHMT19.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT20"].ToString()) > 0)
            {
                this.CHMT20.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT20"].ToString()));
            }
            else
            {
                this.CHMT20.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT21"].ToString()) > 0)
            {
                this.CHMT21.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT21"].ToString()));
            }
            else
            {
                this.CHMT21.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT22"].ToString()) > 0)
            {
                this.CHMT22.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT22"].ToString()));
            }
            else
            {
                this.CHMT22.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT23"].ToString()) > 0)
            {
                this.CHMT23.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT23"].ToString()));
            }
            else
            {
                this.CHMT23.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT24"].ToString()) > 0)
            {
                this.CHMT24.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT24"].ToString()));
            }
            else
            {
                this.CHMT24.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT25"].ToString()) > 0)
            {
                this.CHMT25.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT25"].ToString()));
            }
            else
            {
                this.CHMT25.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT26"].ToString()) > 0)
            {
                this.CHMT26.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT26"].ToString()));
            }
            else
            {
                this.CHMT26.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT27"].ToString()) > 0)
            {
                this.CHMT27.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT27"].ToString()));
            }
            else
            {
                this.CHMT27.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT28"].ToString()) > 0)
            {
                this.CHMT28.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT28"].ToString()));
            }
            else
            {
                this.CHMT28.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT29"].ToString()) > 0)
            {
                this.CHMT29.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT29"].ToString()));
            }
            else
            {
                this.CHMT29.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT30"].ToString()) > 0)
            {
                this.CHMT30.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT30"].ToString()));
            }
            else
            {
                this.CHMT30.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT31"].ToString()) > 0)
            {
                this.CHMT31.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT31"].ToString()));
            }
            else
            {
                this.CHMT31.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMT32"].ToString()) > 0)
            {
                this.CHMT32.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMT32"].ToString()));
            }
            else
            {
                this.CHMT32.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMTHAP"].ToString()) > 0)
            {
                this.CHMTHAP.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMTHAP"].ToString()));
            }
            else
            {
                this.CHMTHAP.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["IPMT"].ToString()) > 0)
            {
                this.IPMT.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["IPMT"].ToString()));
            }
            else
            {
                this.IPMT.Text = "";
            }
            if (Convert.ToDouble(_dt.Rows[fiCount]["JEGO"].ToString()) > 0)
            {
                this.JEGO.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["JEGO"].ToString()));
            }
            else
            {
                this.JEGO.Text = "";
            }
            fiCount++;
        }
    }
}
