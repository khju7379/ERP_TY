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

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSPR009R1.
    /// </summary>
    public partial class TYUSPR009R1 : GrapeCity.ActiveReports.SectionReport
    {
        private string fsPRGUBN = string.Empty;
        private DataTable _dt = new DataTable();

        private Int32 fiCnt = 0;

        public TYUSPR009R1(string sPRGUBN)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsPRGUBN = sPRGUBN;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            if (fsPRGUBN == "N")
            {
                label15.Text = "배 정 량  :";
            }
            else
            {
                label15.Text = "양 수 량  :";
            }

        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (fiCnt < _dt.Rows.Count)
            {
                fiCnt = fiCnt + 1;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            txtGAMQTY.Text = String.Format("{0:#,##0.000}", Math.Round(Convert.ToDouble(String.Format("{0:0.000}", Convert.ToDouble(_dt.Rows[fiCnt - 1]["JGBEJNQTY"].ToString()))) -
                                                           Convert.ToDouble(String.Format("{0:0.000}", Convert.ToDouble(_dt.Rows[fiCnt - 1]["JGHWAKQTY"].ToString()))), 4));
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
            txtPRTDATE.Text = _dt.Rows[fiCnt - 1]["MONTHLASTDAY"].ToString();
        }


    }
}
