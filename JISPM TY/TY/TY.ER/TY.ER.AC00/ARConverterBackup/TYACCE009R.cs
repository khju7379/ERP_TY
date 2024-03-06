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
    /// Summary description for TYACCE009R.
    /// </summary>
    public partial class TYACCE009R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        public TYACCE009R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            int RECORD = int.TryParse(Convert.ToString(this.Fields["RECORD"].Value), out RECORD) ? RECORD : 0;

            this.line3.LineStyle = this._boldRecords.Contains(RECORD) ? LineStyle.Solid : LineStyle.Dash;
            this.line3.LineWeight = this._boldRecords.Contains(RECORD) ? 3 : 1;

            if (Convert.ToString(this.Fields["BUNHO"].Value) == "총계")
            {
                // 폰트 글자 및 굵기 바꾸기
                this.BUNHO.Font  = new Font("굴림", 10, FontStyle.Bold);
                this.B7AMSE.Font = new Font("굴림", 10, FontStyle.Bold);
                this.B7CGSE.Font = new Font("굴림", 10, FontStyle.Bold);
                this.TOTAMT.Font = new Font("굴림", 10, FontStyle.Bold);
            }
            else
            {
                this.BUNHO.Font  = new Font("굴림", 9);
                this.B7AMSE.Font = new Font("굴림", 9);
                this.B7CGSE.Font = new Font("굴림", 9);
                this.TOTAMT.Font = new Font("굴림", 9);

                // 레코드가 25이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
                if (this._rowCount == 25)
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
        }

        private void TYACCE009R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                int record = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["BUNHO"]) == "총계")
                    {
                        // 현재 레코드
                        if (!this._boldRecords.Contains(record))
                            this._boldRecords.Add(record);

                        // 현재 레코드
                        if (!this._boldRecords.Contains(record + 1))
                            this._boldRecords.Add(record + 1);
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