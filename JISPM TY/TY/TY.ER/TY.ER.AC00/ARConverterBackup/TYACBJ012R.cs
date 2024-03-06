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
    /// Summary description for TYACBJ012R.
    /// </summary>
    public partial class TYACBJ012R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sGUBUN   = string.Empty;
        private string sGroup   = string.Empty;
        private string sNEWCDAC = string.Empty;
        private string sOLDCDAC = string.Empty;

        public TYACBJ012R()
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

            this.line5.Visible = true;

            this.line5.LineStyle = LineStyle.Dash;
            this.line5.LineWeight = 1;

            if (this.sGroup == "Change")
            {
                sGroup = "";

                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;
            }
            else
            {
                this.groupFooter1.Visible = false;

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }

            if (dt.Rows[_iCount - 1]["DATE"].ToString() == "월     계")
            {
                if (this._rowCount == 0)
                {
                    this.line3.Visible = false;
                }
                else
                {
                    this.line3.Visible = true;

                    this.line3.LineStyle = LineStyle.Solid;
                    this.line3.LineWeight = 3;

                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 3;
                }
            }
            else if (dt.Rows[_iCount - 1]["DATE"].ToString() == "누     계")
            {
                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }

            //if (this._rowCount == 17)
            //{
            //    this._rowCount = 0;

            //    this.line5.LineStyle = LineStyle.Solid;
            //    this.line5.LineWeight = 3;
            //}
            //else
            //{
            //    this._rowCount = this._rowCount + 1;
            //}

            if (_fiCount == _iCount)
            {
                this.pageFooter.Visible = false;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }
            else
            {
                if (this._rowCount == 28)
                {
                    this._rowCount = 0;

                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 3;
                }
                else
                {
                    // 다음 데이터가 월계인 경우 점선라인이 안 나오도록 함.
                    if (dt.Rows[_iCount]["DATE"].ToString() == "월     계")
                    {
                        this.line5.Visible = false;
                    }

                    this._rowCount = this._rowCount + 1;
                }
            }
        }

        private void TYACBJ012R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this._rowCount = 0;

            this.pageFooter.Visible = false;

            this.groupFooter1.Visible = true;

            sGroup = "Change";
        }
    }
}