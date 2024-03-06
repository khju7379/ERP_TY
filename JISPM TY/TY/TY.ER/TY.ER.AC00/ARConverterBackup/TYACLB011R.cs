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
    /// Summary description for TYACLB011R.
    /// </summary>
    public partial class TYACLB011R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sGroup = string.Empty;

        public TYACLB011R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.line5.Visible = false;

            this.Y2DPSBNM.Font = new Font("굴림", 9, FontStyle.Regular);
            
            this.AMT1.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT2.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT3.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT4.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT5.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT6.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT7.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT8.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT9.Font     = new Font("굴림", 9, FontStyle.Regular);
            this.AMT10.Font    = new Font("굴림", 9, FontStyle.Regular);
            this.AMT11.Font    = new Font("굴림", 9, FontStyle.Regular);
            this.AMT12.Font    = new Font("굴림", 9, FontStyle.Regular);
            this.HAP.Font      = new Font("굴림", 9, FontStyle.Regular);

            if (dt.Rows[_iCount - 1]["Y2DPSBNM"].ToString() == "소   계" || dt.Rows[_iCount - 1]["Y2DPSBNM"].ToString() == "사업부 소계" ||
                dt.Rows[_iCount - 1]["Y2DPSBNM"].ToString() == "계")
            {
                this.Y2DPSBNM.Font = new Font("굴림", 9, FontStyle.Bold);

                this.AMT1.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT2.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT3.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT4.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT5.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT6.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT7.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT8.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT9.Font     = new Font("굴림", 9, FontStyle.Bold);
                this.AMT10.Font    = new Font("굴림", 9, FontStyle.Bold);
                this.AMT11.Font    = new Font("굴림", 9, FontStyle.Bold);
                this.AMT12.Font    = new Font("굴림", 9, FontStyle.Bold);
                this.HAP.Font      = new Font("굴림", 9, FontStyle.Bold);

                this.line5.Visible = true;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 1;
            }

            if (_fiCount == _iCount)
            {
                this.line5.Visible = true;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }
            else
            {
                if (dt.Rows[_iCount]["Y2DPSBNM"].ToString() == "소   계")
                {
                    this.line5.Visible = true;

                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 1;
                }
            }

            // 레코드가 20이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 25)
            {
                this._rowCount = 0;

                this.line5.Visible = true;

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

        private void TYACLB011R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}