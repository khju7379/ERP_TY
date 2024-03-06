﻿using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Drawing;
using System.Data;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACBJ014R.
    /// </summary>
    public partial class TYACBJ014R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable Retdt = new DataTable();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _iPageCount = 0;
        private int _iRecordCount = 0;
        private int _iCount = 0;

        private string _fsGSTDATE = string.Empty;
        private string _fsGEDDATE = string.Empty;

        public TYACBJ014R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;
            this.line1.Visible = false;
            this.line3.Visible = false;
            this.line5.Visible = false;

            this.line6.Visible = false;
            this.line7.Visible = false;

            this.GSTDATE.Text = _fsGSTDATE.ToString();
            this.GEDDATE.Text = _fsGEDDATE.ToString();

            if (_iCount - 1 < Retdt.Rows.Count)
            {
                // 계정코드
                if (Retdt.Rows[_iCount - 1]["A1LVAC"].ToString().ToString() == "1")
                {
                    this.A1ABAC.Alignment =  TextAlignment.Center;

                    this.A1ABAC.Font = new Font("바탕체", 10, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    this.A1ABAC.Alignment = TextAlignment.Left;

                    this.A1ABAC.Font = new Font("바탕체", 9);
                }

                // 계정과목
                this.A1ABAC.Text = Retdt.Rows[_iCount - 1]["A1ABAC"].ToString();

                // 당기 차변
                if (Retdt.Rows[_iCount - 1]["HDRAMT"].ToString() == "")
                {
                    this.HDRAMT.Text = "";
                }
                else
                {
                    this.HDRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["HDRAMT"].ToString()));
                }

                if (Retdt.Rows[_iCount - 1]["HCRAMT"].ToString() == "")
                {
                    this.HCRAMT.Text = "";
                }
                else
                {
                    // 당기 대변
                    this.HCRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["HCRAMT"].ToString()));
                }

                if (Retdt.Rows[_iCount - 1]["ODRAMT"].ToString() == "")
                {
                    this.ODRAMT.Text = "";
                }
                else
                {
                    // 전기 차변
                    this.ODRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["ODRAMT"].ToString()));
                }

                if (Retdt.Rows[_iCount - 1]["OCRAMT"].ToString() == "")
                {
                    this.OCRAMT.Text = "";
                }
                else
                {
                    // 전기 대변
                    this.OCRAMT.Text = string.Format("{0:#,###}", double.Parse(Retdt.Rows[_iCount - 1]["OCRAMT"].ToString()));
                }


                // 충당금 계정일 경우 라인 보이게 함
                if (Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]) == "2")
                {
                    this.line1.Visible = true;
                    this.line3.Visible = true;
                }

                // 계정별 소계
                if (Convert.ToString(Retdt.Rows[_iCount - 1]["A1TAG10"]) == "10")
                {
                    this.line6.Visible = true;
                    this.line7.Visible = true;
                }

                if (this._iPageCount == 0)
                {
                    // 레코드가 36이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
                    if (this._rowCount == 36)
                    {
                        this._rowCount = 0;
                        this._iPageCount++;

                        this.line5.Visible = true;

                        // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                        this.detail.NewPage = NewPage.After;
                    }
                    else
                    {
                        this._rowCount++;

                        // 현재 페이지에 레코드를 인쇄해라.
                        this.detail.NewPage = NewPage.None;
                    }
                }
                else
                {
                    // 레코드가 41이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
                    if (this._rowCount == 41)
                    {
                        this._rowCount = 0;
                        this._iPageCount++;

                        this.line5.Visible = true;

                        // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                        this.detail.NewPage = NewPage.After;
                    }
                    else
                    {
                        this._rowCount++;

                        // 현재 페이지에 레코드를 인쇄해라.
                        this.detail.NewPage = NewPage.None;
                    }
                }
            }
            else
            {
                this.detail.Visible = false;
            }

            if (_iRecordCount == _iCount)
            {
                this.line5.Visible = true;
            }
        }

        private void TYACBJ014R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            DataRow row;

            Retdt.Columns.Add("GSTDATE", typeof(System.String));
            Retdt.Columns.Add("GEDDATE", typeof(System.String));
            Retdt.Columns.Add("A1CDAC", typeof(System.String));
            Retdt.Columns.Add("A1LVAC", typeof(System.String));
            Retdt.Columns.Add("A1ABAC", typeof(System.String));
            Retdt.Columns.Add("A1TAG10", typeof(System.String));
            Retdt.Columns.Add("HDRAMT", typeof(System.String));
            Retdt.Columns.Add("HCRAMT", typeof(System.String));
            Retdt.Columns.Add("ODRAMT", typeof(System.String));
            Retdt.Columns.Add("OCRAMT", typeof(System.String));
            Retdt.Columns.Add("CUNO", typeof(System.String));
            Retdt.Columns.Add("BENO", typeof(System.String));

            foreach (DataRow dr in dt.Rows)
            {
                _fsGSTDATE = dr["GSTDATE"].ToString();
                _fsGEDDATE = dr["GEDDATE"].ToString();

                row = Retdt.NewRow();

                row["GSTDATE"] = dr["A1CDAC"].ToString();
                row["GEDDATE"] = dr["A1LVAC"].ToString();
                row["A1CDAC"] = dr["A1CDAC"].ToString();
                row["A1ABAC"] = dr["A1ABAC"].ToString();
                row["A1LVAC"] = dr["A1LVAC"].ToString();
                row["A1TAG10"] = dr["A1TAG10"].ToString();
                row["HDRAMT"] = dr["HDRAMT"].ToString();
                row["HCRAMT"] = dr["HCRAMT"].ToString();
                row["ODRAMT"] = dr["ODRAMT"].ToString();
                row["OCRAMT"] = dr["OCRAMT"].ToString();
                row["CUNO"] = dr["CUNO"].ToString();
                row["BENO"] = dr["BENO"].ToString();

                Retdt.Rows.Add(row);
            }

            _iRecordCount = Retdt.Rows.Count;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            this.GSTDATE.Text = _fsGSTDATE.ToString();
            this.GEDDATE.Text = _fsGEDDATE.ToString();
        }
    }
}