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
    /// Summary description for TYUTPR033R.
    /// </summary>
    public partial class TYUTPR033R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;

        public TYUTPR033R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

            _dt = (DataTable)this.DataSource;

            if (fiCount < _dt.Rows.Count)
            {
                if (_dt.Rows[fiCount]["WKTYPE2"].ToString() == "")
                {
                    HEADER2.Visible = true;
                    DETAIL2.Visible = true;
                }
                if (_dt.Rows[fiCount]["WKTYPE3"].ToString() == "")
                {
                    HEADER3.Visible = true;
                    DETAIL3.Visible = true;
                }
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fiCount++;
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {

        }
    }
}
