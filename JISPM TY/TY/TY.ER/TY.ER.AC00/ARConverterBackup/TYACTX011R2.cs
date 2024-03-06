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
    /// Summary description for TYACTX011R2.
    /// </summary>
    public partial class TYACTX011R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private int _RowCount = 0;
        private int _fiCount = 0;
        private int _pageCount = 1;

        public TYACTX011R2()
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
                if (this._fiCount == 19)
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
                if (this._fiCount == 14)
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
    }
}
