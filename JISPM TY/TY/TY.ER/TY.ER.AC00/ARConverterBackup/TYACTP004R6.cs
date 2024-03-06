using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.SectionReportModel;
using System.Data;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACTP004R6.
    /// </summary>
    public partial class TYACTP004R6 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        DataTable _dt2 = new DataTable();
        int _iRowCount = 0;

        public TYACTP004R6(DataTable dt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _dt = dt;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            _dt2 = this.DataSource as DataTable;
            int iMENCOUNT = 0;

            for (int i = 0; i < _dt2.Rows.Count; i++)
            {
                if (_dt2.Rows[i]["WTRADNAME"].ToString() != "")
                {
                    iMENCOUNT++;
                }
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_iRowCount < 10)
            {
                this.detail.Visible = false;
            }
            else
            {
                this.detail.Visible = true;
            }
            _iRowCount++;
            
        }

        
    }
}
