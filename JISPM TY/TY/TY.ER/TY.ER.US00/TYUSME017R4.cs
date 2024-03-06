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
    /// Summary description for TYUTME017R.
    /// </summary>
    public partial class TYUSME017R4 : GrapeCity.ActiveReports.SectionReport
    {
        private int fiMaxCnt = 0;
        private DataTable _dt = new DataTable();

        public TYUSME017R4()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
            fiMaxCnt = _dt.Rows.Count;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //if (fiCount > 0)
            //{
            //    if (_dt.Rows[fiCount - 1]["VNSAUPJA"].ToString() != _dt.Rows[fiCount]["VNSAUPJA"].ToString())
            //    {
            //        this.detail.NewPage = NewPage.Before;
            //    }
            //    else
            //    {
            //        this.detail.NewPage = NewPage.None;
            //    }
            //}

            //fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
