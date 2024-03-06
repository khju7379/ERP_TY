﻿using System;
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
    /// Summary description for TYACFP006R.
    /// </summary>
    public partial class TYACFP006R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;

        private string sGroup = string.Empty;

        public TYACFP006R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.line5.Visible = false;

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

            this.B7VENDNM.Font = new Font("굴림", 9, FontStyle.Regular);
            this.B7AMJN.Font = new Font("굴림", 9, FontStyle.Regular);

            if (dt.Rows[_iCount - 1]["B7VENDNM"].ToString() == "[소     계]" || dt.Rows[_iCount - 1]["B7VENDNM"].ToString() == "*** 총합계 ***")
            {
                this.B7VENDNM.Font = new Font("굴림", 9, FontStyle.Bold);
                this.B7AMJN.Font = new Font("굴림", 9, FontStyle.Bold);

                this.line5.Visible = true;
            }

            if (_fiCount != _iCount)
            {
                if (dt.Rows[_iCount]["B7VENDNM"].ToString() == "[총     계]")
                {
                    this.line5.Visible = true;
                }
            }
            else
            {
                this.line5.Visible = true;
            }

            // 레코드가 20이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 19)
            {
                this._rowCount = 0;

                this.line5.Visible = true;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount++;

                // 현재 페이지에 레코드를 인쇄해라.
                //this.detail.NewPage = NewPage.None;
            }
        }

        private void TYACFP006R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this._rowCount = 0;

            sGroup = "Change";
        }
    }
}
