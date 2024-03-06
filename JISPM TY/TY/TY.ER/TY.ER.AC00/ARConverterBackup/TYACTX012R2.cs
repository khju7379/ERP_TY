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
    /// Summary description for TYACTX012R2.
    /// </summary>
    public partial class TYACTX012R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private int _RowCount = 0;
        private int _fiCount = 0;
        private int _pageCount = 1;

        public TYACTX012R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            _RowCount++;

            detail.Visible = false;

            if (_RowCount > 5)
            {
                detail.Visible = true;
            }
            if (this._pageCount == 1)
            {   
                if (this._fiCount == 18)
                {
                    this._fiCount = 0;

                    this.detail.NewPage = NewPage.Before;
                    this._fiCount++;
                    this._pageCount++;
                }
                else
                {
                    this._fiCount++;

                    this.detail.NewPage = NewPage.None;
                }
            }
            else
            {
                if (this._fiCount == 13)
                {
                    this._fiCount = 0;

                    this.detail.NewPage = NewPage.Before;
                    this._fiCount++;
                    this._pageCount++;
                }
                else
                {
                    this._fiCount++;

                    this.detail.NewPage = NewPage.None;
                }
            }
        }

        private void TYACTX012R2_ReportStart(object sender, EventArgs e)
        {

        }
    }
}
