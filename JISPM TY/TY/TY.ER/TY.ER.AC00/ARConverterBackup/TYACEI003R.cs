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
    /// Summary description for TYACEI003R.
    /// </summary>
    public partial class TYACEI003R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private bool fbFirstPage = false; 
        

        public TYACEI003R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();


        }

        private void detail_Format(object sender, EventArgs e)
        {
            //if (this._rowCount == 0)
            //{
            //    // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
            //    this.detail.NewPage = NewPage.Before;
            //}
            //else
            //{
            //    // 현재 페이지에 레코드를 인쇄해라.
            //    this.detail.NewPage = NewPage.None;
            //}
            //this.line3.LineStyle = this._boldRecords.Contains(RECORD) ? LineStyle.Solid : LineStyle.Dash;
            //this.line3.LineWeight = this._boldRecords.Contains(RECORD) ? 3 : 1;


            // 레코드가 30이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (fbFirstPage == true)
            {
                if (this._rowCount == 30)
                {
                    this._rowCount = 0;

                    this.line3.LineStyle = LineStyle.Solid;
                    this.line3.LineWeight = 3;

                    this.detail.NewPage = NewPage.After; 
                }
                else
                {
                    this._rowCount = this._rowCount + 1;

                    this.line3.LineStyle = LineStyle.Dash;
                    this.line3.LineWeight = 1;

                    this.detail.NewPage = NewPage.None;
                }
            }
            else
            {
                if (this._rowCount == 25)
                {
                    this._rowCount = 0;

                    this.line3.LineStyle = LineStyle.Solid;
                    this.line3.LineWeight = 3;

                    this.detail.NewPage = NewPage.After;

                    fbFirstPage = true; 
                }
                else
                {
                    this._rowCount = this._rowCount + 1;

                    this.line3.LineStyle = LineStyle.Dash;
                    this.line3.LineWeight = 1;

                    this.detail.NewPage = NewPage.None;
                }
            }
            
           
           
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            // 소계를 찍을때는 변수값을 0으로 초기화
            //this._rowCount = 0;
        }

        private void TYACEI003R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
            if (dt != null)
            {
                
            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            //this.TOT_B7AMSE.Text = string.Format("{0:#,##0}", _dHAP_B7AMSE);
            //this.TOT_B7CGSE.Text = string.Format("{0:#,##0}", _dHAP_B7CGSE);
            //this.TOT_TOTAMT.Text = string.Format("{0:#,##0}", _dHAP_TOTAMT);

        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            //txtDATE.Text = Convert.ToString(this.Fields["SDATE"].Value).Substring(0, 4) + "-" +
            //               Convert.ToString(this.Fields["SDATE"].Value).Substring(4, 2) + "-" +
            //               Convert.ToString(this.Fields["SDATE"].Value).Substring(6, 2) + " ~ " +
            //               Convert.ToString(this.Fields["EDATE"].Value).Substring(0, 4) + "-" +
            //               Convert.ToString(this.Fields["EDATE"].Value).Substring(4, 2) + "-" +
            //               Convert.ToString(this.Fields["EDATE"].Value).Substring(6, 2);

        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            txtDATE.Text = Convert.ToString(this.Fields["SDATE"].Value).Substring(0, 4) + "-" +
                           Convert.ToString(this.Fields["SDATE"].Value).Substring(4, 2) + "-" +
                           Convert.ToString(this.Fields["SDATE"].Value).Substring(6, 2) + " ~ " +
                           Convert.ToString(this.Fields["EDATE"].Value).Substring(0, 4) + "-" +
                           Convert.ToString(this.Fields["EDATE"].Value).Substring(4, 2) + "-" +
                           Convert.ToString(this.Fields["EDATE"].Value).Substring(6, 2);
        }      

       
    }
}