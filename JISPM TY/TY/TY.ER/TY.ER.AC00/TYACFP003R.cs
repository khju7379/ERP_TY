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
    /// Summary description for TYACFP003R.
    /// </summary>
    public partial class TYACFP003R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        // 페이지 변수
        private int _PageCout = 1;

        string fsM1RKAC = string.Empty;
        string fsGUBUN = string.Empty;

        private string sGroup = string.Empty;

        public TYACFP003R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (this.sGroup == "Change")
            {
                sGroup = "";

                if (Convert.ToString(this.Fields["COMPANY"].Value) == "")
                {
                    this.detail.NewPage = NewPage.None;
                }
                else
                {
                    this.detail.NewPage = NewPage.Before;
                }
            }
            else
            {
                this.groupFooter1.Visible = false;

                this.detail.NewPage = NewPage.None;
            }

            //if (this._rowCount == 0)
            //{
            //    //this.COMPANY.Visible = true;                
            //}
            //else
            //{
            //    if (fsGUBUN == "HAP")
            //    {
            //        //this.COMPANY.Visible = true;

            //    }
            //    else
            //    {
            //        //this.COMPANY.Visible = false;
            //        this.M1GUBN.Visible = true;
            //    }
            //}

            fsM1RKAC = Convert.ToString(this.Fields["M1RKAC"].Value);

            if (fsM1RKAC == "소계" || fsM1RKAC == "총계")
            {
                fsGUBUN = "HAP";
                // 폰트 글자 및 굵기 바꾸기
                this.M1RKAC.Font = new Font("굴림", 10, FontStyle.Bold);
                this.M1AMT.Font = new Font("굴림", 10, FontStyle.Bold);
            }
            else
            {
                fsGUBUN = "";
                // 폰트 글자 및 굵기 바꾸기
                this.M1RKAC.Font = new Font("굴림", 9);
                this.M1AMT.Font = new Font("굴림", 9);
            }

            // 레코드가 7이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1

            if (this._PageCout == 1)
            {
                if (this._rowCount == 13)
                {
                    this._PageCout++;
                    this._rowCount = 0;

                    //this.line11.Visible = false;

                    this.line3.Visible = true;

                    this.line3.LineStyle = LineStyle.Solid;
                    this.line3.LineWeight = 3;

                    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._rowCount++;

                    if (fsM1RKAC == "소계")
                    {
                        fsGUBUN = "HAP";

                        this.line3.Visible = true;

                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 1;
                    }
                    else if (fsM1RKAC == "총계")
                    {
                        fsGUBUN = "";

                        //this.line11.Visible = false;

                        this.line3.Visible = true;

                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 3;
                    }
                    else
                    {
                        //this.line11.Visible = false;

                        this.line3.Visible = true;

                        this.line3.LineStyle = LineStyle.Dash;
                        this.line3.LineWeight = 1;
                    }

                    // 현재 페이지에 레코드를 인쇄해라.
                    //this.detail.NewPage = NewPage.None;
                }
            }
            else
            {
                if (this._rowCount == 17)
                {
                    this._PageCout++;
                    this._rowCount = 0;

                    //this.line11.Visible = false;

                    this.line3.Visible = true;

                    this.line3.LineStyle = LineStyle.Solid;
                    this.line3.LineWeight = 3;

                    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._rowCount++;

                    if (fsM1RKAC == "소계")
                    {
                        fsGUBUN = "HAP";

                        this.line3.Visible = true;

                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 3;
                    }
                    else if (fsM1RKAC == "총계")
                    {
                        fsGUBUN = "";

                        //this.line11.Visible = false;

                        this.line3.Visible = true;

                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 3;
                    }
                    else
                    {
                        //this.line11.Visible = false;

                        this.line3.Visible = true;

                        this.line3.LineStyle = LineStyle.Dash;
                        this.line3.LineWeight = 1;
                    }

                    // 현재 페이지에 레코드를 인쇄해라.
                    //this.detail.NewPage = NewPage.None;
                }
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            sGroup = "Change";
            this._rowCount = 0;

            this.line3.Visible = true;

            this.line3.LineStyle = LineStyle.Solid;
            this.line3.LineWeight = 3;
        }

        private void TYACFP003R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                //결재라인 사장, 대표사장 표시 
                if (Convert.ToDecimal(dt.Rows[0]["M1DTED"].ToString().Replace("-", "")) >= 20201201)
                {
                    lbl_GRADE1.Text = "대표 사장";
                    lbl_GRADE3.Text = "사   장";
                }
                else
                {
                    lbl_GRADE1.Text = "사   장";
                    lbl_GRADE3.Text = "부 사 장";
                }
            }
        }
    }
}