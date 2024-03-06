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
    /// Summary description for TYACTP004R8.
    /// </summary>
    public partial class TYACTP004R8 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        int _iRowCount = 0;

        public TYACTP004R8()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_iRowCount < 16)
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
