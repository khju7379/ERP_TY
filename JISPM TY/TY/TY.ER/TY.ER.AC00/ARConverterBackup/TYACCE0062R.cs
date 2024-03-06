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
    /// Summary description for TYACCE0062R.
    /// </summary>
    public partial class TYACCE0062R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        // 중복 제거
        private string _A6NMSA = "";

        private double _dHAP_TOTAMT = 0;

        public TYACCE0062R()
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

            this._A6NMSA = Convert.ToString(this.Fields["A6NMSA"].Value);

            int RECORD = int.TryParse(Convert.ToString(this.Fields["RECORD"].Value), out RECORD) ? RECORD : 0;

            this.line3.LineStyle = this._boldRecords.Contains(RECORD) ? LineStyle.Solid : LineStyle.Dash;
            this.line3.LineWeight = this._boldRecords.Contains(RECORD) ? 3 : 1;

            if (_A6NMSA == "소계")
            {
                // 폰트 글자 및 굵기 바꾸기
                this.A6NMSA.Font = new Font("굴림", 10, FontStyle.Bold);
                this.TOTAMT.Font = new Font("굴림", 10, FontStyle.Bold);

                _dHAP_TOTAMT = _dHAP_TOTAMT + double.Parse(Convert.ToString(this.Fields["TOTAMT"].Value));

                this.TOT_TOTAMT.Text = "";

                this.TOT_TOTAMT.Text = string.Format("{0:#,##0}", _dHAP_TOTAMT);
            }
            else
            {
                // 폰트 글자 및 굵기 바꾸기
                this.A6NMSA.Font = new Font("굴림", 9);
                this.TOTAMT.Font = new Font("굴림", 9);
            }

            // 레코드가 25이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if(this._rowCount == 25)
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

        private void TYACCE0062R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
            if (dt != null)
            {
                string sB7DTOC = string.Empty;
                int record = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["A6NMSA"]) == "소계")
                    {
                        // 현재 레코드
                        if (!this._boldRecords.Contains(record))
                            this._boldRecords.Add(record);

                        // 다음 레코드
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
    }
}