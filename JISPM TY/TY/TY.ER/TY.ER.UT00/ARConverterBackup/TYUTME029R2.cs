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
    /// Summary description for TYUTME029R2.
    /// </summary>
    public partial class TYUTME029R2 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private string fsFilter = string.Empty;
        private int fiCount = 0;
        private string fsYYYYMM = string.Empty;
        private string fsDATE = string.Empty;

        public TYUTME029R2(string sYYYYMM, string sDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsYYYYMM = sYYYYMM;
            fsDATE = sDATE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (fiCount == 0)
            {
                _dt = (DataTable)this.DataSource;
            }

            this.MAECHUL_YAER.Text = fsYYYYMM.Substring(0, 4);
            this.MAECHUL_MONTH.Text = fsYYYYMM.Substring(4, 2);
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.YY.Text = fsDATE.Substring(0, 4);
            this.MM.Text = fsDATE.Substring(5, 2);
            this.DD.Text = fsDATE.Substring(8, 2);

            this.groupFooter1.NewPage = NewPage.After;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            fsFilter = "";
            fsFilter = fsFilter + "HWAJU = '" + _dt.Rows[fiCount]["HWAJU"].ToString() + "'";

            DataTable dt = _dt.Copy();
            dt.Clear();

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow dr in _dt.Select(fsFilter))
                    dt.Rows.Add(dr.ItemArray);

                if (_dt.Rows[fiCount]["MCETCCODE"].ToString() == "")
                {
                    TYUTME029R3 subReport = new TYUTME029R3();
                    subReport.DataSource = dt;
                    TYUTME029R3.Report = subReport;
                    //TYUTME029R3.Visible = true;
                    //TYUTME029R4.Visible = false;
                }
                else
                {
                    TYUTME029R4 subReport = new TYUTME029R4();
                    subReport.DataSource = dt;
                    TYUTME029R4.Report = subReport;
                    //TYUTME029R3.Visible = false;
                    //TYUTME029R4.Visible = true;
                }
            }

            if (fiCount < _dt.Rows.Count - 1)
            {
                fiCount++;
            }
        }
    }
}
