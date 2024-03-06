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
    /// Summary description for TYACCE005R.
    /// </summary>
    public partial class TYACCE005R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        // 중복 제거
        private string _B7DTOC = "";

        private double _dHAP_B7AMSE = 0;
        private double _dHAP_B7CGSE = 0;
        private double _dHAP_TOTAMT = 0;

        public TYACCE005R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (this._rowCount == 0)
            {
                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;
            }
            else
            {
                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }

            this._B7DTOC = Convert.ToString(this.Fields["B7DTOC"].Value);

            int RECORD = int.TryParse(Convert.ToString(this.Fields["RECORD"].Value), out RECORD) ? RECORD : 0;

            this.line3.LineStyle = this._boldRecords.Contains(RECORD) ? LineStyle.Solid : LineStyle.Dash;
            this.line3.LineWeight = this._boldRecords.Contains(RECORD) ? 3 : 1;

            if (_B7DTOC == "소계")
            {
                _dHAP_B7AMSE = _dHAP_B7AMSE + double.Parse(Convert.ToString(this.Fields["B7AMSE"].Value));
                _dHAP_B7CGSE = _dHAP_B7CGSE + double.Parse(Convert.ToString(this.Fields["B7CGSE"].Value));
                _dHAP_TOTAMT = _dHAP_TOTAMT + double.Parse(Convert.ToString(this.Fields["TOTAMT"].Value));

                this.TOT_B7AMSE.Text = "";
                this.TOT_B7CGSE.Text = "";
                this.TOT_TOTAMT.Text = "";

                this.TOT_B7AMSE.Text = string.Format("{0:#,##0}", _dHAP_B7AMSE);
                this.TOT_B7CGSE.Text = string.Format("{0:#,##0}", _dHAP_B7CGSE);
                this.TOT_TOTAMT.Text = string.Format("{0:#,##0}", _dHAP_TOTAMT);
            }

            // 레코드가 24이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if(this._rowCount == 24)
            {
                this._rowCount = 0;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;
            }
            else
            {
                this._rowCount = this._rowCount + 1;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            // 소계를 찍을때는 변수값을 0으로 초기화
            this._rowCount = 0;
        }

        private void TYACCE005R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
            if (dt != null)
            {
                string sB7DTOC = string.Empty;
                int record = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["B7DTOC"]) == "소계")
                    {
                        //// 이전 레코드
                        //if (!this._boldRecords.Contains(record - 1))
                        //    this._boldRecords.Add(record - 1);

                        //현재 레코드
                        if (!this._boldRecords.Contains(record))
                            this._boldRecords.Add(record);

                        //현재 레코드
                        if (!this._boldRecords.Contains(record+1))
                            this._boldRecords.Add(record+1);
                    }

                    record = int.TryParse(Convert.ToString(dr["RECORD"]), out record) ? record : 0;
                }

                // 현재 레코드
                if (!this._boldRecords.Contains(record))
                    this._boldRecords.Add(record);
            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            //this.TOT_B7AMSE.Text = string.Format("{0:#,##0}", _dHAP_B7AMSE);
            //this.TOT_B7CGSE.Text = string.Format("{0:#,##0}", _dHAP_B7CGSE);
            //this.TOT_TOTAMT.Text = string.Format("{0:#,##0}", _dHAP_TOTAMT);

        }
    }
}