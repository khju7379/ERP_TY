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
    /// Summary description for TYACPO023R1.
    /// </summary>
    public partial class TYACPO023R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable Retdt = new DataTable();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _iPageCount   = 0;
        private int _iRecordCount = 0;
        private int _iCount       = 0;

        private string fsGUBUN = string.Empty;

        public TYACPO023R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this._iCount++;

            //this.line5.Visible = false;
            //this.line1.Visible = false;

            //if (_iCount - 1 < Retdt.Rows.Count)
            //{
            //    if (_iCount == 1)
            //    {
            //        fsGUBUN = Retdt.Rows[_iCount - 1]["GUBUN"].ToString();

            //        // 직위
            //        this.ESCCMPNM.Text   = Retdt.Rows[_iCount - 1]["EFJKCD"].ToString();
            //        // 성명
            //        this.EFNAME.Text   = Retdt.Rows[_iCount - 1]["EFNAME"].ToString();
            //        // 생년월일
            //        this.BIRTHDAY.Text = Retdt.Rows[_iCount - 1]["BIRTHDAY"].ToString();
            //    }
            //    else
            //    {
            //        if (fsGUBUN == Retdt.Rows[_iCount - 1]["GUBUN"].ToString())
            //        {
            //            // 직위
            //            this.ESCCMPNM.Text = "";
            //            // 성명
            //            this.EFNAME.Text = "";
            //            // 생년월일
            //            this.BIRTHDAY.Text = "";
            //        }
            //        else
            //        {
            //            this.line1.Visible = true;
            //            // 직위
            //            this.ESCCMPNM.Text = Retdt.Rows[_iCount - 1]["EFJKCD"].ToString();
            //            // 성명
            //            this.EFNAME.Text = Retdt.Rows[_iCount - 1]["EFNAME"].ToString();
            //            // 생년월일
            //            this.BIRTHDAY.Text = Retdt.Rows[_iCount - 1]["BIRTHDAY"].ToString();
            //        }
            //    }

            //    // 겸직현황
            //    this.EHHOLDJOB.Text = Retdt.Rows[_iCount - 1]["EHHOLDJOB"].ToString();

            //    // 주요경력
            //    this.EHCAREEF.Text = Retdt.Rows[_iCount - 1]["EHCAREEF"].ToString();
                

            //    // 레코드가 36이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            //    if (this._rowCount == 22)
            //    {
            //        this._rowCount = 0;
            //        this._iPageCount++;

            //        this.line5.Visible = true;

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

            //if (_iRecordCount == _iCount)
            //{
            //    this.line5.Visible = true;
            //}
        }

        private void TYACPO023R1_DataInitialize(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)this.DataSource;

            //DataRow row;

            //Retdt.Columns.Add("GUBUN",     typeof(System.String));
            //Retdt.Columns.Add("EFJKCD",    typeof(System.String));
            //Retdt.Columns.Add("EFNAME",    typeof(System.String));
            //Retdt.Columns.Add("BIRTHDAY",  typeof(System.String));
            //Retdt.Columns.Add("EHHOLDJOB", typeof(System.String));
            //Retdt.Columns.Add("EHCAREEF",  typeof(System.String));

            //foreach (DataRow dr in dt.Rows)
            //{
            //    row = Retdt.NewRow();

            //    row["GUBUN"]     = dr["GUBUN"].ToString();
            //    row["EFJKCD"]    = dr["EFJKCD"].ToString();
            //    row["EFNAME"]    = dr["EFNAME"].ToString();
            //    row["BIRTHDAY"]  = dr["BIRTHDAY"].ToString();
            //    row["EHHOLDJOB"] = dr["EHHOLDJOB"].ToString();
            //    row["EHCAREEF"]  = dr["EHCAREEF"].ToString();

            //    Retdt.Rows.Add(row);
            //}

            //_iRecordCount = Retdt.Rows.Count;
        }
    }
}