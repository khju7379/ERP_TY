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
    /// Summary description for TYACBJ028R.
    /// </summary>
    public partial class TYACBJ028R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sGUBUN   = string.Empty;
        private string fsGroup  = string.Empty;
        private string sNEWCDAC = string.Empty;
        private string sOLDCDAC = string.Empty;

        public TYACBJ028R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            if (this.fsGroup == "Change")
            {
                fsGroup = "";

                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;
            }
            else
            {
                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }

            this.DATE.Font    = new Font("굴림", 9, FontStyle.Regular);
            this.B4AMDR.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.B4AMCR.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.B4AMJAN.Font = new Font("굴림", 9, FontStyle.Regular);

            this.line3.Visible = false;

            this.line3.LineStyle = LineStyle.Dash;
            this.line3.LineWeight = 1;

            this.line5.LineStyle = LineStyle.Dash;
            this.line5.LineWeight = 1;

            if (dt.Rows[_iCount - 1]["DATE"].ToString() == "전월잔고" ||
                dt.Rows[_iCount - 1]["DATE"].ToString() == "일   계"  ||
                dt.Rows[_iCount - 1]["DATE"].ToString() == "월   계"  ||
                dt.Rows[_iCount - 1]["DATE"].ToString() == "누   계")
            {
                this.DATE.Font    = new Font("굴림", 9, FontStyle.Bold);
                this.B4AMDR.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.B4AMCR.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.B4AMJAN.Font = new Font("굴림", 9, FontStyle.Bold);

                if (dt.Rows[_iCount - 1]["DATE"].ToString() == "일   계" ||
                    dt.Rows[_iCount - 1]["DATE"].ToString() == "월   계" ||
                    dt.Rows[_iCount - 1]["DATE"].ToString() == "누   계")
                {
                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 3;
                }
            }

            if (_fiCount != _iCount)
            {
                if (dt.Rows[_iCount]["DATE"].ToString() == "일   계")
                {
                    if (this._rowCount != 0)
                    {
                        this.line5.LineStyle = LineStyle.Solid;
                        this.line5.LineWeight = 3;
                    }
                }
            }

            // 레코드가 20이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 20)
            {
                this._rowCount = 0;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount++;
            }
        }

        private void TYACBJ028R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            fsGroup = "Change";
        }
    }
}