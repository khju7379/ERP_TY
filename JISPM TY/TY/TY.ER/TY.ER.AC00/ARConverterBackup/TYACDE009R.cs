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
    /// Summary description for TYACDE010R.
    /// </summary>
    public partial class TYACDE009R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        // 중복 제거
        private string _BKDESC = "";

        private string _A1ABAC = "";
        private string _E3NOAC = "";
        private string _E3DTET = "";
        private string _E3DTSV = "";

        public TYACDE009R()
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
                }

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }

            int RECORD = int.TryParse(Convert.ToString(this.Fields["RECORD"].Value), out RECORD) ? RECORD : 0;

            this.line3.LineStyle = this._boldRecords.Contains(RECORD) ? LineStyle.Solid : LineStyle.Dash;
            this.line3.LineWeight = this._boldRecords.Contains(RECORD) ? 3 : 1;

            // 레코드가 24이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if(this._rowCount == 24)
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

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            // 소계를 찍을때는 변수값을 0으로 초기화
            this._rowCount = 0;

            this._BKDESC = "";
            this._A1ABAC = "";
            this._E3NOAC = "";
            this._E3DTET = "";
            this._E3DTSV = "";
        }

        private void TYACDE009R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
            if (dt != null)
            {
                string sBKDESC = string.Empty;
                int record = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    if (sBKDESC == "")
                    {
                        sBKDESC = Convert.ToString(dr["BKDESC"]);
                    }
                    else
                    {
                        if (sBKDESC != Convert.ToString(dr["BKDESC"]))
                        {
                            sBKDESC = Convert.ToString(dr["BKDESC"]);

                            record = int.TryParse(Convert.ToString(dr["RECORD"]), out record) ? record : 0;

                            // 이전 레코드
                            if (!this._boldRecords.Contains(record - 1))
                                this._boldRecords.Add(record - 1);

                            ////현재 레코드
                            //if (!this._boldRecords.Contains(record))
                            //    this._boldRecords.Add(record);
                        }
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