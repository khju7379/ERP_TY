using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using System.Data;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACBJ031R.
    /// </summary>
    public partial class TYACBJ031R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sNEWCDDESC2 = string.Empty;
        private string sOLDCDDESC2 = string.Empty;

        public TYACBJ031R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.CDDESC2.Font = new Font("바탕체", 9, FontStyle.Regular);

            this.line3.Visible = false;

            this.line5.LineStyle = LineStyle.Dash;
            this.line5.LineWeight = 1;


            //sNEWCDDESC2 = dt.Rows[_iCount - 1]["CDDESC2"].ToString();

            //this.CDDESC2.Text = sNEWCDDESC2;
            //this.CDCODE.Text = dt.Rows[_iCount - 1]["CDCODE"].ToString();

            //if (sOLDCDDESC2 == "")
            //{
            //    sOLDCDDESC2 = sNEWCDDESC2.ToString();
            //}
            //else
            //{
            //    if (sNEWCDDESC2 == sOLDCDDESC2)
            //    {
            //        this.CDDESC2.Text = "";
            //        this.CDCODE.Text = "";
            //    }
            //    else
            //    {
            //        sOLDCDDESC2 = sNEWCDDESC2.ToString();
            //    }
            //}

            if (dt.Rows[_iCount - 1]["CDDESC2"].ToString() == "합   계" || dt.Rows[_iCount - 1]["CDDESC2"].ToString() == "총   계")
            {
                this.CDDESC2.Font = new Font("바탕체", 9, FontStyle.Bold);

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 1;
            }

            if (_fiCount != _iCount) 
            {
                if (dt.Rows[_iCount]["CDDESC2"].ToString() == "합   계" || dt.Rows[_iCount]["CDDESC2"].ToString() == "총   계")
                {
                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 1;
                }
            }
            else
            {
                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }

            // 레코드가 22이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 22)
            {
                this._rowCount = 0;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;

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

        private void TYACBJ031R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}