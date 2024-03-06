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
    /// Summary description for TYACPO023R2.
    /// </summary>
    public partial class TYACPO023R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _iPageCount   = 0;
        private int _iRecordCount = 0;
        private int _iCount       = 0;

        private string fsGUBUN = string.Empty;

        public TYACPO023R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.detail.NewPage = NewPage.None;

            this.line3.Visible = true;

            this.EPCABAC1.Width = 0.756F;

            this.line5.X2 = 0.22F;

            if (dt.Rows[_iCount - 1]["EPCABAC2"].ToString() == "")
            {
                this.line3.Visible = false;

                this.EPCABAC1.Width = 1.633F;
            }

            if (_iCount - 1 < dt.Rows.Count)
            {
                if (_iCount > 1)
                {
                    if (dt.Rows[_iCount - 2]["EPCABAC1"].ToString() == dt.Rows[_iCount - 1]["EPCABAC1"].ToString())
                    {
                        this.EPCABAC1.Text = "";
                    }
                }

                if (_iRecordCount != _iCount)
                {
                    if (dt.Rows[_iCount - 1]["EPCABAC1"].ToString() == dt.Rows[_iCount]["EPCABAC1"].ToString())
                    {
                        this.line5.X2 = 1.15F;
                    }
                }
            }
        }

        private void TYACPO023R2_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _iRecordCount = dt.Rows.Count;
            }
        }
    }
}