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
    /// Summary description for TYACHF005R.
    /// </summary>
    public partial class TYACHF005R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;
        private double fdHeight = 0;

        private string sGroup = string.Empty;
        private string sNEWCDAC = string.Empty;
        private string sOLDCDAC = string.Empty;

        public TYACHF005R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYACHF005R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.pageFooter.Visible = false;

            this.groupFooter1.Visible = true;

            sGroup = "Change";

            this.line13.Visible = true;

            this.line13.LineStyle = LineStyle.Solid;
            this.line13.LineWeight = 3;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            if (this.sGroup == "Change")
            {
                sGroup = "";

                fdHeight = 0;

                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;
            }
            else
            {
                this.groupFooter1.Visible = false;

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }
        }
    }
}
