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
    /// Summary description for TYACLB012R.
    /// </summary>
    public partial class TYACLB012R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sGroup = string.Empty;

        public TYACLB012R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.line5.Visible = false;

            if (_fiCount == _iCount)
            {
                this.B2HISAB.Font   = new Font("굴림", 9, FontStyle.Bold);
                this.Y3CNT.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.Y3AMT.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.Y3CARDCNT.Font = new Font("굴림", 9, FontStyle.Bold);
                this.CARDAMT.Font   = new Font("굴림", 9, FontStyle.Bold);
                this.CARD.Font      = new Font("굴림", 9, FontStyle.Bold);

                this.line5.Visible = true;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }
            else
            {
                if (dt.Rows[_iCount]["B2HISAB"].ToString() == "합   계")
                {
                    this.line5.Visible = true;

                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 1;
                }
            }

            // 레코드가 26이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 25)
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
                this._rowCount++;

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }
        }

        private void TYACLB012R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}