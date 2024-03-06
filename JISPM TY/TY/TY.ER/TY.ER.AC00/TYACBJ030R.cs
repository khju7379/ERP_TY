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
    /// Summary description for TYACBJ030R.
    /// </summary>
    public partial class TYACBJ030R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;

        private string sGUBUN = string.Empty;
        private string fsGroup = string.Empty;
        private string sNEWCDAC = string.Empty;
        private string sOLDCDAC = string.Empty;

        public TYACBJ030R()
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
            this.line5.Visible = false;

            this.line6.Visible = true;

            this.line6.LineStyle = LineStyle.Dash;
            this.line6.LineWeight = 1;

            if (_fiCount != _iCount)
            {
                if (dt.Rows[_iCount]["ABAC"].ToString() == "합   계")
                {
                    this.line6.Visible = false;
                }
            }
            else
            {
                this.line3.Visible = true;
                this.line5.Visible = true;

                this.line6.Visible = false;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }

            // 레코드가 10이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 12)
            {
                this._rowCount = 0;

                this.line6.Visible = false;

                this.line5.Visible = true;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount++;

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }
        }

        private void TYACBJ030R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}