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
    /// Summary description for TYACPO023R6.
    /// </summary>
    public partial class TYACPO023R6 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable Retdt = new DataTable();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _iPageCount = 0;
        private int _iRecordCount = 0;
        private int _iCount = 0;

        private string fsGUBUN = string.Empty;

        public TYACPO023R6()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this._iCount++;

            //if (_iCount - 1 < Retdt.Rows.Count)
            //{
            //    // 주주명
            //    this.ESHOLDNAME.Text  = Retdt.Rows[_iCount - 1]["ESHOLDNAME"].ToString();
            //    // 생년월일
            //    this.ESBIRTH.Text     = Retdt.Rows[_iCount - 1]["ESBIRTH"].ToString();
            //    // 보유주식수
            //    this.ESSTOCKCNT.Text  = string.Format("{0:#,##0}", double.Parse(Retdt.Rows[_iCount - 1]["ESSTOCKCNT"].ToString()));
            //    // 지분율
            //    this.ESSTOCKRATE.Text = string.Format("{0:#0.##}", Retdt.Rows[_iCount - 1]["ESSTOCKRATE"].ToString()) + "%";
            //    // 주소
            //    this.ESJUSO.Text      = Retdt.Rows[_iCount - 1]["ESJUSO"].ToString();


            //    // 레코드가 36이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            //    if (this._rowCount == 22)
            //    {
            //        this._rowCount = 0;
            //        this._iPageCount++;

            //        // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
            //        this.detail.NewPage = NewPage.After;
            //    }
            //    else
            //    {
            //        this._rowCount++;

            //        // 현재 페이지에 레코드를 인쇄해라.
            //        this.detail.NewPage = NewPage.None;
            //    }
            //}
            //else
            //{
            //    this.detail.Visible = false;
            //}
        }

        private void TYACPO023R6_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            _iRecordCount = dt.Rows.Count;
        }
    }
}