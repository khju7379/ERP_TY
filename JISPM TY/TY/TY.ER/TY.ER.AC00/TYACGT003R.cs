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
    /// Summary description for TYACGT003R.
    /// </summary>
    public partial class TYACGT003R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;

        public TYACGT003R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;
            this.line3.Visible = false;

            // 레코드가 11이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 11)
            {
                this._rowCount = 0;

                this.line3.Visible = true;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount++;

                this.detail.NewPage = NewPage.None;
            }

            if (_fiCount == _iCount)
            {
                this.line3.Visible = true;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;
            }
        }

        private void TYACGT003R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}