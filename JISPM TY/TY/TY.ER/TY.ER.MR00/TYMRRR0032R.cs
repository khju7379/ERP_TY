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
using TY.ER.MR00;

namespace TY.ER.MR00
{
    /// <summary>
    /// Summary description for TYMRRR0032S.
    /// </summary>
    public partial class TYMRRR0032R : GrapeCity.ActiveReports.SectionReport
    {

        private DataTable Retdt = new DataTable();

        private int _rowCount = 0;
        private int _iRecordCount = 0;
        private int _iCount = 0;
        private int _pageCount = 1;         //페이지 카운터
        private string sSTRRM1100 = "";
        private string sEDRRM1100 = "";

        public TYMRRR0032R(string STDATE, string EDDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            sSTRRM1100 = STDATE.ToString();
            sEDRRM1100 = EDDATE.ToString();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            // 첫번째 페이지인 경우
            if (this._pageCount == 1)
            {
                if (this._rowCount == 23)
                {
                    this._rowCount = 0;

                    this.detail.NewPage = NewPage.After;
                    this._pageCount++;
                }
                else
                {
                    this._rowCount++;

                    this.detail.NewPage = NewPage.None;
                    //this.pageFooter.Visible = false;

                }
            }
            // 첫번째 페이지가 아닌경우
            else
            {
                if (this._rowCount == 26)
                {
                    this._rowCount = 0;

                    this.detail.NewPage = NewPage.After;
                    this._pageCount++;
                }
                else
                {
                    this._rowCount++;

                    this.detail.NewPage = NewPage.None;
                    //this.pageFooter.Visible = false;

                }
            }
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            this.STRRM1100.Text = sSTRRM1100.Substring(0, 4) + "-" + sSTRRM1100.Substring(4, 2) + "-" + sSTRRM1100.Substring(6, 2);
            this.EDRRM1100.Text = sEDRRM1100.Substring(0, 4) + "-" + sEDRRM1100.Substring(4, 2) + "-" + sEDRRM1100.Substring(6, 2);
        }
    }
}
