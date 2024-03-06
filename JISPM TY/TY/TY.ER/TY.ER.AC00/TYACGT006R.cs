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
    /// Summary description for TYACGT006R.
    /// </summary>
    public partial class TYACGT006R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        // 거래처별 건수
        private int _iGUNSU = 0;

        public TYACGT006R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.line5.Visible = false;

            this._iGUNSU++;

            // 레코드가 21이상이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount >= 21)
            {
                this._rowCount = 0;

                this.line5.Visible = true;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount = this._rowCount + 2;

                // 페이지 스킵 속성도 가지고 있기 때문에 none으로 해줌.
                this.detail.NewPage = NewPage.None;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.line3.Visible = true;

            if (this._rowCount >= 21)
            {
                this._rowCount = 0;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.groupFooter1.NewPage = NewPage.After;
            }
            else
            {
                this.line3.LineStyle = LineStyle.Dash;
                this.line3.LineWeight = 1;

                this._rowCount++;
                this.groupFooter1.NewPage = NewPage.None;
            }
        }
    }
}