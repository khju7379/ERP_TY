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
    /// Summary description for TYACEI013R.
    /// </summary>
    public partial class TYACEI013R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        public TYACEI013R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }

        private void detail_Format(object sender, EventArgs e)
        {
            int RECORD = int.TryParse(Convert.ToString(this.Fields["NUM"].Value), out RECORD) ? RECORD : 0;

            this.line2.LineStyle = this._boldRecords.Contains(RECORD) ? LineStyle.Solid : LineStyle.Dash;
            this.line2.LineWeight = this._boldRecords.Contains(RECORD) ? 3 : 1;

            // 레코드가 29이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 29)
            {
                this._rowCount = 0;

                this.line2.LineStyle = LineStyle.Solid;
                this.line2.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount = this._rowCount + 1;

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }
        }

        private void TYACEI013R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                int record = 0;

                int iCount = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    record = int.TryParse(Convert.ToString(dr["NUM"]), out record) ? record : 0;

                    if (iCount == 29)
                    {
                        // 현재 레코드
                        if (!this._boldRecords.Contains(record))
                            this._boldRecords.Add(record);

                        iCount = 0;
                    }
                    else
                    {
                        iCount = iCount + 1;
                    }
                }

                // 현재 레코드
                if (!this._boldRecords.Contains(record))
                    this._boldRecords.Add(record);
            }

        }
    }
}
