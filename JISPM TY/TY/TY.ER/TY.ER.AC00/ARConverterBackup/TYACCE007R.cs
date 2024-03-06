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
    /// Summary description for TYACCE007R.
    /// </summary>
    public partial class TYACCE007R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private double _Count = 0;

        public TYACCE007R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
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

                this.line3.LineStyle = LineStyle.Dash;
                this.line3.LineWeight = 1;
            }

            // 마지막 데이터일 경우 선 굵게
            if (_Count == double.Parse(Convert.ToString(this.Fields["RECORD"].Value)))
            {
                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;
            }
        }

        private void TYACCE007R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
            if (dt != null)
            {
                _Count = dt.Rows.Count;
            }
        }
    }
}