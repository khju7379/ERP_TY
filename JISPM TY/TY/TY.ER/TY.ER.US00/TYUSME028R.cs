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

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSME028R.
    /// </summary>
    public partial class TYUSME028R : GrapeCity.ActiveReports.SectionReport
    {
        private int fiCount = 0;
        private int iDataCnt = 0;
        private DataTable _dt = new DataTable();

        public TYUSME028R()
        {
            InitializeComponent();
        }

        private void TYUSME028R_DataInitialize(object sender, EventArgs e)
        {
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_dt.Rows[fiCount]["CDDESC1"].ToString() == "거래처별 소계" || _dt.Rows[fiCount]["CDDESC1"].ToString() == "총 계")
            {
                this.line3.Visible = true;
                this.line5.Visible = true;
            }
            else
            {
                this.line3.Visible = false;
                this.line5.Visible = false;
            }

            if (fiCount == _dt.Rows.Count)
            {
                this.line5.Visible = true;
            }
            else
            {
                if (iDataCnt == 24)
                {
                    this.line5.Visible = true;

                    iDataCnt = 0;

                    this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this.detail.NewPage = NewPage.None;

                    if (_dt.Rows[fiCount]["CDDESC1"].ToString() == "거래처별 소계" || _dt.Rows[fiCount]["CDDESC1"].ToString() == "총 계")
                    {
                        this.line3.Visible = true;
                        this.line5.Visible = true;
                    }
                    else
                    {
                        this.line3.Visible = false;
                        this.line5.Visible = false;
                    }
                }
            }

            fiCount++;

            iDataCnt++;
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            this.groupHeader1.NewPage = NewPage.Before;
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
        }
    }
}