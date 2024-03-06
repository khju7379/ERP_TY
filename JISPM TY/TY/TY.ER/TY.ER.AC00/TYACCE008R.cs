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
    /// Summary description for TYACCE008R.
    /// </summary>
    public partial class TYACCE008R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;
        //private double _Count = 0;

        public TYACCE008R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            //// 레코드가 21이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            //if (this._rowCount == 21)
            //{
            //    this._rowCount = 0;

            //    this.line3.LineStyle = LineStyle.Solid;
            //    this.line3.LineWeight = 3;
            //}
            //else
            //{
            //    this._rowCount = this._rowCount + 1;

            //    this.line3.LineStyle = LineStyle.Dash;
            //    this.line3.LineWeight = 1;
            //}

            if (dt.Rows[_iCount - 1]["B7NOMK"].ToString() == "소계" || dt.Rows[_iCount - 1]["B7NOMK"].ToString() == "총계")
            {
                // 폰트 글자 및 굵기 바꾸기
                this.B7NOMK.Font = new Font("굴림", 10, FontStyle.Bold);
                this.CNT.Font = new Font("굴림", 10, FontStyle.Bold);
                this.TOTAMT.Font = new Font("굴림", 10, FontStyle.Bold);
                this.CNT1.Font = new Font("굴림", 10, FontStyle.Bold);
                this.TOTAMT1.Font = new Font("굴림", 10, FontStyle.Bold);
                this.YUL.Font = new Font("굴림", 10, FontStyle.Bold);

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;
            }
            else
            {
                this.B7NOMK.Font = new Font("굴림", 9);
                this.CNT.Font = new Font("굴림", 9);
                this.TOTAMT.Font = new Font("굴림", 9);
                this.CNT1.Font = new Font("굴림", 9);
                this.TOTAMT1.Font = new Font("굴림", 9);
                this.YUL.Font = new Font("굴림", 9);
            }

            // 레코드가 25이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 25)
            {
                this._rowCount = 0;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;
            }
            else
            {
                if (_fiCount != _iCount)
                {
                    if (dt.Rows[_iCount]["B7NOMK"].ToString() == "소계")
                    {
                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 3;
                    }
                    else if (dt.Rows[_iCount - 1]["B7NOMK"].ToString() == "소계" || dt.Rows[_iCount - 1]["B7NOMK"].ToString() == "총계")
                    {
                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 3;
                    }
                    else
                    {
                        this.line3.LineStyle = LineStyle.Dash;
                        this.line3.LineWeight = 1;
                    }

                    this._rowCount = this._rowCount + 1;
                }
                else
                {
                    this.line3.LineStyle = LineStyle.Solid;
                    this.line3.LineWeight = 3;
                }
            }
        }

        private void TYACCE008R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                //int record = 0;

                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (Convert.ToString(dr["B7NOMK"]) == "소계")
                //    {
                //        // 현재 레코드
                //        if (!this._boldRecords.Contains(record))
                //            this._boldRecords.Add(record);

                //        // 다음 레코드
                //        if (!this._boldRecords.Contains(record + 1))
                //            this._boldRecords.Add(record + 1);
                //    }

                //    if (Convert.ToString(dr["B7NOMK"]) == "총계")
                //    {
                //        // 현재 레코드
                //        if (!this._boldRecords.Contains(record))
                //            this._boldRecords.Add(record);
                //    }

                //    record = int.TryParse(Convert.ToString(dr["RECORD"]), out record) ? record : 0;
                //}

                //// 현재 레코드
                //if (!this._boldRecords.Contains(record))
                //    this._boldRecords.Add(record);

                _fiCount = dt.Rows.Count;
            }
        }
    }
}