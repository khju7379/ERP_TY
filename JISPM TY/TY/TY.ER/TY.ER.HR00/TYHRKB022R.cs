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

namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRKB022R.
    /// </summary>
    public partial class TYHRKB022R : GrapeCity.ActiveReports.SectionReport
    {

        public TYHRKB022R(DataTable dt, string sMEMO)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            txtMEMO.Text = sMEMO;

            if (dt.Rows.Count > 0)
            {
                txtYear.Text = dt.Rows[0]["INMPYDATE"].ToString().Substring(0, 4);
            }
            else
            {
                txtYear.Text = "";
            }
        }
    }
}
