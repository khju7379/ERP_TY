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
    /// Summary description for TYUTIL014R3.
    /// </summary>
    public partial class TYUTIL014R3 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private DataTable _dtJilso = new DataTable();
        private string fsFilter = string.Empty;
        private int fiCount = 0;

        public TYUTIL014R3(DataTable dtJilso)
        {
            _dtJilso = dtJilso;
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }



        private void detail_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;

            fsFilter = "";
            fsFilter = fsFilter + "JLHWAJU = '" + _dt.Rows[fiCount]["DAHWAJU"].ToString() + "'";

            if (_dt.Rows[fiCount]["DAHWAJU"].ToString() == "HPC")
            {
                label93.Text = "LPG 사용료(VCU)";
                label6.Text = "(LPG CHARGE)";
                label94.Text = "LPG 사용비용";
            }
            else
            {
                label93.Text = "LNG 사용료(VCU)";
                label6.Text = "(LNG CHARGE)";
                label94.Text = "LNG 사용비용";
            }

            DataTable dt = _dtJilso.Copy();
            dt.Clear();
            //DataTable dt = new DataTable();


            if (_dtJilso.Rows.Count > 0)
            {
                foreach (DataRow dr in _dtJilso.Select(fsFilter))
                    dt.Rows.Add(dr.ItemArray);

                TYUTIL014R4 subReport = new TYUTIL014R4();
                subReport.DataSource = dt;
                TYUTIL014R4.Report = subReport;
            }

            if (fiCount < _dt.Rows.Count - 1)
            {
                fiCount++;
            }
        }
    }
}
