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
    /// Summary description for TYACBJ008R.
    /// </summary>
    public partial class TYACBJ008R : GrapeCity.ActiveReports.SectionReport
    {

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount = 0;
        private int _idistinctCount = 0;
        private int _totalRowCount = 0;

        private string _Page = "";

        private DataTable dt = new DataTable();

        private string fsSDATE = "";
        private string fsEDATE = "";



        public TYACBJ008R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

            txtSDATE.Text = Convert.ToString(fsSDATE).Substring(0, 4) + "-" +
                             Convert.ToString(fsSDATE).Substring(4, 2);

            txtEDATE.Text = Convert.ToString(fsEDATE).Substring(0, 4) + "-" +
                             Convert.ToString(fsEDATE).Substring(4, 2);

        }

        private void detail_Format(object sender, EventArgs e)
        {

            if (this._rowCount == 21)
            {
                this._rowCount = 0;

                this.line2.LineStyle = LineStyle.Solid;
                this.line2.LineWeight = 2;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                //this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount++;

                this.line2.LineStyle = LineStyle.Dash;
                this.line2.LineWeight = 1;
            }

            if (dt.Rows[_iCount]["YYMM"].ToString() == "999999")
            {
                txtDTAC.Text = "";
                txtCMAC.Text = "";
                txtNOSQ.Text = "";
                txtNOLN.Text = "";
                txtB4VLMI1.Text = "";
                txtB4VLMI2.Text = "";

                this.line2.LineStyle = LineStyle.Solid;
                this.line2.LineWeight = 2;

            }
            else
            {
                this.line2.LineStyle = LineStyle.Dash;
                this.line2.LineWeight = 1;
            }

            this._iCount++;
        }

        private void TYACBJ008R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                fsSDATE = dt.Rows[0]["CFROMB4DTAC"].ToString();
                fsEDATE = dt.Rows[0]["CTOB4DTAC"].ToString();

                this._totalRowCount = dt.Rows.Count;
            }
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            //관리항목1
            this._rowCount = 0;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            //계정
            this._rowCount = 0;
        }


    }
}
