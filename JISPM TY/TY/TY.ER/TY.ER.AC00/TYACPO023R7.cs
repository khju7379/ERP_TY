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
    /// Summary description for TYACPO023R7.
    /// </summary>
    public partial class TYACPO023R7 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable ftdt = new DataTable();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _iPageCount = 0;
        private int _iRecordCount = 0;
        private int _iCount = 0;
        private string fsESISTITLE = string.Empty;

        private string fsGUBUN = string.Empty;

        public TYACPO023R7()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.line5.Visible = true;

            if (_iCount - 1 < ftdt.Rows.Count)
            {
                //if (ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString() != fsESISTITLE)
                //{
                //    fsESISTITLE = ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString();
                //}
                //else
                //{
                //    fsESISTITLE = "";
                //}

                //if (_iCount != ftdt.Rows.Count)
                //{
                //    if (fsESISTITLE == ftdt.Rows[_iCount]["ESISTITLE"].ToString())
                //    {
                //        this.line5.Visible = false;
                //    }
                //}


                if (_iCount != ftdt.Rows.Count)
                {
                    if (ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString() != ftdt.Rows[_iCount]["ESISTITLE"].ToString())
                    {
                        fsESISTITLE = ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString();
                    }
                    else
                    {
                        fsESISTITLE = "";

                        if (_iCount == 1)
                        {
                            fsESISTITLE = ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString();
                        }
                    }

                    if (ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString() == ftdt.Rows[_iCount]["ESISTITLE"].ToString())
                    {
                        this.line5.Visible = false;
                    }
                }
                else
                {
                    if (ftdt.Rows[_iCount - 2]["ESISTITLE"].ToString() == ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString())
                    {
                        fsESISTITLE = "";
                    }
                    else
                    {
                        fsESISTITLE = ftdt.Rows[_iCount - 1]["ESISTITLE"].ToString();
                    }
                }


                this.ESISTITLE.Text = fsESISTITLE.ToString();

                // 레코드가 36이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
                if (this._rowCount == 12)
                {
                    this._rowCount = 0;
                    this._iPageCount++;

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
                this.detail.Visible = false;
            }
        }

        private void TYACPO023R7_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            _iRecordCount = dt.Rows.Count;

            ftdt = dt;
        }
    }
}