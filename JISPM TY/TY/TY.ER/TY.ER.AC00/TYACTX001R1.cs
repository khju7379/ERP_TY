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
    /// Summary description for TYACTX001R1.
    /// </summary>
    public partial class TYACTX001R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private string sTITLE = string.Empty;

        private int _fiCount = 0;

        // 거래처별 건수
        private int _iGUNSU = 0;
        private int _iGUNSU_TOTAL = 0;

        public TYACTX001R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            LBL01_TITLE.Text = sTITLE.ToString() + " 세금계산서 내역 명세서";
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._fiCount++;

            if (_fiCount - 1 <= dt.Rows.Count)
            {
                this.line5.Visible = false;

                this._iGUNSU++;
                this._iGUNSU_TOTAL++;

                // 레코드가 17이상이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
                if (this._rowCount >= 24)
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
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.line3.Visible = true;

            if (this._rowCount >= 24)
            {
                this.GUNSU.Text = Convert.ToString(_iGUNSU);

                _iGUNSU = 0;

                this._rowCount = 0;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.groupFooter1.NewPage = NewPage.After;
            }
            else
            {
                this.GUNSU.Text = Convert.ToString(_iGUNSU);

                _iGUNSU = 0;

                this.line3.LineStyle = LineStyle.Dash;
                this.line3.LineWeight = 1;

                this._rowCount++;
                this.groupFooter1.NewPage = NewPage.None;
            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.GUNSU_TOTAL.Text = Convert.ToString(_iGUNSU_TOTAL);
        }

        private void TYACTX001R1_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                if (dt.Rows[0]["B4VLMI2"].ToString() == "11103101")
                {
                    sTITLE = "매입";
                }
                else
                {
                    sTITLE = "매출";
                }
            }
        }
    }
}