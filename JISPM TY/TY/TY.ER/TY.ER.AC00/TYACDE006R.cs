using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.SectionReportModel;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACDE006R.
    /// </summary>
    public partial class TYACDE006R : GrapeCity.ActiveReports.SectionReport
    {
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        // 중복 제거
        private string _BKDESC = "";

        private string _A1ABAC = "";
        private string _E3NOAC = "";
        private string _E3DTET = "";
        private string _E3DTSV = "";

        public TYACDE006R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (this._rowCount == 0)
            {
                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;

                this._BKDESC = this.BKDESC.Text;
                this._A1ABAC = this.A1ABAC.Text;
                this._E3NOAC = this.E3NOAC.Text;
                this._E3DTET = this.E3DTET.Text;
                this._E3DTSV = this.E3DTSV.Text;
            }
            else
            {
                // 중복 제거
                if (this._BKDESC == this.BKDESC.Text)
                {
                    this.BKDESC.Text = "";

                    if (this._A1ABAC == this.A1ABAC.Text)
                    {
                        this.A1ABAC.Text = "";
                    }
                    else
                    {
                        this._A1ABAC = this.A1ABAC.Text;
                    }

                    if (this._E3NOAC == this.E3NOAC.Text)
                    {
                        this.E3NOAC.Text = "";
                    }
                    else
                    {
                        this._E3NOAC = this.E3NOAC.Text;
                    }

                    if (this._E3DTET == this.E3DTET.Text)
                    {
                        this.E3DTET.Text = "";
                    }
                    else
                    {
                        this._E3DTET = this.E3DTET.Text;
                    }

                    if (this._E3DTSV == this.E3DTSV.Text)
                    {
                        this.E3DTSV.Text = "";
                    }
                    else
                    {
                        this._E3DTSV = this.E3DTSV.Text;
                    }
                }
                else
                {
                    this._BKDESC = this.BKDESC.Text;
                    this._A1ABAC = this.A1ABAC.Text;
                    this._E3NOAC = this.E3NOAC.Text;
                    this._E3DTET = this.E3DTET.Text;
                    this._E3DTSV = this.E3DTSV.Text;

                    //if (this._A1ABAC == this.A1ABAC.Text)
                    //{
                    //    this.A1ABAC.Text = "";
                    //}
                    //else
                    //{
                    //    this._A1ABAC = this.A1ABAC.Text;
                    //}

                    //if (this._E3NOAC == this.E3NOAC.Text)
                    //{
                    //    this.E3NOAC.Text = "";
                    //}
                    //else
                    //{
                    //    this._E3NOAC = this.E3NOAC.Text;
                    //}

                    //if (this._E3DTET == this.E3DTET.Text)
                    //{
                    //    this.E3DTET.Text = "";
                    //}
                    //else
                    //{
                    //    this._E3DTET = this.E3DTET.Text;
                    //}

                    //if (this._E3DTSV == this.E3DTSV.Text)
                    //{
                    //    this.E3DTSV.Text = "";
                    //}
                    //else
                    //{
                    //    this._E3DTSV = this.E3DTSV.Text;
                    //}
                }

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;

                // 페이지푸터를 보여라
                this.pageFooter.Visible = true;
            }

            // 레코드가 30이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            this._rowCount = (this._rowCount == 30 ? 0 : this._rowCount + 1);
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            // 소계를 찍을때는 변수값을 0으로 초기화
            this._rowCount = 0;

            this._BKDESC = "";
            this._A1ABAC = "";
            this._E3NOAC = "";
            this._E3DTET = "";
            this._E3DTSV = "";

            // 페이지푸터를 안 보이게 함
            this.pageFooter.Visible = false;
        }
    }
}