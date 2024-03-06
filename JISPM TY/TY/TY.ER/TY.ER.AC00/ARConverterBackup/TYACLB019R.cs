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
    /// Summary description for TYACLB019R.
    /// </summary>
    public partial class TYACLB019R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sGroup = string.Empty;

        public TYACLB019R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

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

            this.line3.Visible = false;

            if (dt.Rows[_iCount - 1]["CHK"].ToString() == "집   행" || dt.Rows[_iCount - 1]["CHK"].ToString() == "잔   액")
            {
                this.line3.Visible = true;

                this.line3.LineStyle = LineStyle.Dash;
                this.line3.LineWeight = 1;

                if (dt.Rows[_iCount - 1]["CHK"].ToString() == "집   행")
                {
                    this.line3.X1 = 3.958F;
                }
                else
                {
                    this.line3.X1 = 3.438F;
                }
            }

            if (_fiCount == _iCount)
            {
                this.line3.Visible = true;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;

                this.line3.X1 = 0.07F;
            }
            else
            {
                if (dt.Rows[_iCount - 1]["CHK"].ToString() == "잔   액")
                {
                    if (dt.Rows[_iCount - 1]["P2CDAC"].ToString() != dt.Rows[_iCount]["P2CDAC"].ToString())
                    {
                        this.line3.Visible = true;

                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 3;

                        this.line3.X1 = 0.07F;
                    }
                }
            }

            // 레코드가 12이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 12)
            {
                this._rowCount = 0;

                this.line3.Visible = true;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;

                this.line3.X1 = 0.07F;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount++;
            }
        }

        private void TYACLB019R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            sGroup = "Change";

            this._rowCount = 0;
        }
    }
}