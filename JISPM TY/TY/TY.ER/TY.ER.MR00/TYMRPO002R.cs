using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.MR00
{
    /// <summary>
    /// Summary description for TYMRPO002S.
    /// </summary>
    public partial class TYMRPO002R : GrapeCity.ActiveReports.SectionReport
    {

        private DataTable Retdt = new DataTable();


        private int _rowCount = 0;
        private int _iCount = 0;
        private int _pageCount = 1;         //페이지 카운터
        private int iState = 0;
        private string sSANGHO = "";

        public TYMRPO002R(string name)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            sSANGHO = name;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            if (this._pageCount == 1)
            {
                // 20줄 출력시 새 페이지 생성 후 카운터 초기화
                if (this._rowCount == 20)
                {
                    this._rowCount = 0;

                    this.detail.NewPage = NewPage.Before;
                    this._pageCount++;
                }
                else
                {
                    this._rowCount++;

                    this.detail.NewPage = NewPage.None;
                }
            }
            else
            {
                // 25줄 출력시 새 페이지 생성 후 카운터 초기화
                if (this._rowCount == 25)
                {
                    this._rowCount = 0;

                    this.detail.NewPage = NewPage.Before;
                    this._pageCount++;
                }
                else
                {
                    this._rowCount++;

                    this.detail.NewPage = NewPage.None;
                }
            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.SANGHO.Text = sSANGHO.ToString();
            // 첫 번째 페이지인 경우
            if (this._pageCount == 1)
            {
                // 11~20줄이면 새 페이지 생성 후 reportFooter 출력
                if (this._rowCount >= 11 && this._rowCount <= 20)
                {
                    this.reportFooter1.NewPage = NewPage.Before;
                    this._pageCount++;
                    this.iState = 1;
                }
            }
            // 첫 번째 페이지가 아닌경우
            else
            {
                // 17~20줄이면 새 페이지 생성 후 reportFooter 출력
                if (this._rowCount >= 17 && this._rowCount <= 25)
                {
                    this.reportFooter1.NewPage = NewPage.Before;
                    this._pageCount++;
                    this.iState = 1;
                }
            }
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            // reportFooter 출력 페이지에 데이터가 없으면 pageHeader 숨김
            if (this.iState == 1)
            {
                this.pageHeader.Height = 0.0f;
                this.pageHeader.Visible = false;
            }
        }
    }
}
