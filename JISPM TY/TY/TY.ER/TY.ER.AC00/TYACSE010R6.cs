using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.Drawing;
using System.Data;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACSE010R6.
    /// </summary>
    public partial class TYACSE010R6 : GrapeCity.ActiveReports.SectionReport
    {

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount = 0;
        private int _totalRowCount = 0;
        private string fsGUBUNM = string.Empty;

        private DataTable dt = new DataTable();

        public TYACSE010R6()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            DATE.Text = Convert.ToString(this.Fields["DATE"].Value).Substring(0, 4) + "년  " +
                        Convert.ToString(this.Fields["DATE"].Value).Substring(4, 2) + "월  " +
                        Convert.ToString(this.Fields["DATE"].Value).Substring(6, 2) + "일  현재";
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


            this._iCount++;

            if (fsGUBUNM == "")
            {
                this.A1NMAC.Visible = true;
            }
            else
            {
                this.A1NMAC.Visible = false;
            }

            this.line5.Visible = false;
            this.ANBSRKAC1.Visible = false;

            this.ANBSRKAC.Alignment = TextAlignment.Left;
            this.A1NMAC.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);
            this.VNSANGHO.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);
            this.ANBSRKAC.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);
            this.ANBSSAMT.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);
            this.ANBSMAMT.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);
            this.ANBSJAMT.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);

            if (this._totalRowCount == this._iCount) // 마지막 레코드
            {
                this.line12.Visible = false;
                this.line15.Visible = false;

                this.line5.Visible = true;
                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 2;

                this.ANBSRKAC.Visible = false;
                this.VNSANGHO.Visible = false;

                this.ANBSRKAC1.Visible = true;
                this.ANBSRKAC1.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);
                this.ANBSSAMT.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);
                this.ANBSMAMT.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);
                this.ANBSJAMT.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);

            }
            else
            {
                fsGUBUNM = "true";

                if (this._rowCount == 20)
                {
                    this._rowCount = 0;

                    this.line5.Visible = true;
                    this.line5.X2 = 0.26f;
                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 2;

                    //// 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    ////this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._rowCount++;
                }

                if (dt.Rows[_iCount]["RUM"].ToString() == "0" && dt.Rows[_iCount]["GUBUN"].ToString() == "H")
                {
                    this.line5.Visible = true;
                    this.line5.X2 = 3.007f;
                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 0.5f;
                }

                //if (dt.Rows[_iCount - 1]["RUM"].ToString() == "0" && dt.Rows[_iCount - 1]["GUBUN"].ToString() == "H")
                //{
                //    fsGUBUNM = "";
                //}

                //if (dt.Rows[_iCount - 1]["ANBSCUST"].ToString() != dt.Rows[_iCount]["ANBSCUST"].ToString())
                //{
                //    fsGUBUNM = ""; //

                //    this.line5.Visible = true;
                //    this.line5.X2 = 0.26f;
                //    this.line5.LineStyle = LineStyle.Solid;
                //    this.line5.LineWeight = 1;
                //}


                // 합계 처리
                if (dt.Rows[_iCount]["GUBUN"].ToString() == "T")
                {
                    this.line5.Visible = true;
                    this.line5.X2 = 0.26f;
                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 2;
                }
            }
        }

        private void TYACSE010R6_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                this._totalRowCount = dt.Rows.Count;
            }
        }
    }
}