using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTPR002R.
    /// </summary>
    public partial class TYUTPR002R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;

        public TYUTPR002R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (Convert.ToDouble(_dt.Rows[fiCount]["IPMTQTY"].ToString()) != 0)
            {
                this.IPMTQTY.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["IPMTQTY"].ToString()));
            }
            else
            {
                this.IPMTQTY.Text = "";
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["IPKLQTY"].ToString()) != 0)
            {
                this.IPKLQTY.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["IPKLQTY"].ToString()));
            }
            else
            {
                this.IPKLQTY.Text = "";
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["CHMTQTY"].ToString()) != 0)
            {
                this.CHMTQTY.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHMTQTY"].ToString()));
            }
            else
            {
                this.CHMTQTY.Text = "";
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["CHKLQTY"].ToString()) != 0)
            {
                this.CHKLQTY.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["CHKLQTY"].ToString()));
            }
            else
            {
                this.CHKLQTY.Text = "";
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["JEJEMT"].ToString()) != 0)
            {
                this.JEJEMT.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["JEJEMT"].ToString()));
            }
            else
            {
                this.JEJEMT.Text = "";
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["JEJEKL"].ToString()) != 0)
            {
                this.JEJEKL.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["JEJEKL"].ToString()));
            }
            else
            {
                this.JEJEKL.Text = "";
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["JEOVMT"].ToString()) != 0)
            {
                this.JEOVMT.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["JEOVMT"].ToString()));
            }
            else
            {
                this.JEOVMT.Text = "";
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["JEOVKL"].ToString()) != 0)
            {
                this.JEOVKL.Text = string.Format("{0:#,##0.000}", Convert.ToDouble(_dt.Rows[fiCount]["JEOVKL"].ToString()));
            }
            else
            {
                this.JEOVKL.Text = "";
            }

            fiCount++;
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {

            this.groupFooter2.NewPage = NewPage.After;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
