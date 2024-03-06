using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Document.Section;
using System.Data;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACSE002R2.
    /// </summary>
    public partial class TYACSE002R2 : GrapeCity.ActiveReports.SectionReport
    {

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount = 0;
        private int _totalRowCount = 0;
        private string fsGUBUNM = string.Empty;

        private DataTable dt = new DataTable();

        public TYACSE002R2()
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

            this.GIBUNYUL.Text = string.Format("{0:#0.##}", double.Parse(dt.Rows[_iCount]["GIBUNYUL"].ToString())) + "%";

            if (double.Parse(dt.Rows[_iCount]["GIBUNYUL"].ToString()) == 0)
            {
                this.GIBUNYUL.Visible = false;
            }
            else
            {
                this.GIBUNYUL.Visible = true;
            }

            this._iCount++;

            if (fsGUBUNM == "")
            {
                this.GUBUNM.Visible = true;
            }
            else
            {
                this.GUBUNM.Visible = false;
            }

            this.line5.Visible = false;
            this.AACUSTNM1.Visible = false;
            this.ADJUSTSTOK.Visible = true;

            

            this.AACUSTNM.Alignment = TextAlignment.Left;

            this.AACUSTNM.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);
            this.ADLEDJUAMT.Font = new Font("바탕체", 8, System.Drawing.FontStyle.Regular);

            if (this._totalRowCount == this._iCount) // 마지막 레코드
            {
                this.line12.Visible = false;

                this.line5.Visible = true;
                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 2;

                this.GIBUNYUL.Visible = false;
                this.AACUSTNM.Visible = false;
                this.ADJUSTSTOK.Visible = false;
                this.AACUSTNM1.Visible = true;

                this.AACUSTNM1.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);
                this.ADLEDJUAMT.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);
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

                    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    //this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._rowCount++;
                }


                if (dt.Rows[_iCount]["RUM"].ToString() == "0" && dt.Rows[_iCount]["GUBUN"].ToString() == "H")
                {
                    this.line5.Visible = true;
                    this.line5.X2 = 1.91f;
                    this.line5.LineStyle = LineStyle.Dot;
                    this.line5.LineWeight = 0.5f;
                }

                if (dt.Rows[_iCount - 1]["RUM"].ToString() == "0" && dt.Rows[_iCount - 1]["GUBUN"].ToString() == "H")
                {
                    fsGUBUNM = "";

                    this.GIBUNYUL.Visible = false;
                    this.ADJUSTSTOK.Visible = false;

                    this.AACUSTNM.Alignment = TextAlignment.Center;

                    this.AACUSTNM.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);
                    this.ADLEDJUAMT.Font = new Font("바탕체", 9, System.Drawing.FontStyle.Bold);

                    this.line5.Visible = true;
                    this.line5.X2 = 0.26f;
                    this.line5.LineStyle = LineStyle.Dot;
                    this.line5.LineWeight = 0.5f;
                }

                //if (dt.Rows[_iCount - 1]["AACNCDAC"].ToString() != dt.Rows[_iCount]["AACNCDAC"].ToString())
                //{
                //    this.line5.Visible    = true;
                //    this.line5.X2         = 0.26f;
                //    this.line5.LineStyle  = LineStyle.Dot;
                //    this.line5.LineWeight = 0.5f;
                //}

                if (dt.Rows[_iCount]["GUBUN"].ToString() == "T")
                {
                    this.GIBUNYUL.Visible = false;
                    this.ADJUSTSTOK.Visible = false;

                    this.line5.Visible = true;
                    this.line5.X2 = 0.26f;
                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 2;
                }
            }
        }

        private void TYACSE002R2_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                this._totalRowCount = dt.Rows.Count;
            }
        }
    }
}