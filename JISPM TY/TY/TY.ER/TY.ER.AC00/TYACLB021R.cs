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
    /// Summary description for TYACLB021R.
    /// </summary>
    public partial class TYACLB021R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;

        private string sGroup = string.Empty;

        public TYACLB021R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.line5.LineStyle = LineStyle.Dash;
            this.line5.LineWeight = 1;

            this.S_NM.Font = new Font("굴림", 9, FontStyle.Regular);
            this.PART_NM.Font = new Font("굴림", 9, FontStyle.Regular);

            this.AMT1.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT2.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT3.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT4.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT5.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT31.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT32.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT33.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT34.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT35.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT11.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT12.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT13.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT14.Font = new Font("굴림", 9, FontStyle.Regular);
            this.AMT15.Font = new Font("굴림", 9, FontStyle.Regular);

            this.SILJUK.Font = new Font("굴림", 9, FontStyle.Regular);
            this.CARD.Font = new Font("굴림", 9, FontStyle.Regular);

            if (dt.Rows[_iCount - 1]["S_NM"].ToString() == "소   계" || dt.Rows[_iCount - 1]["S_NM"].ToString() == "총   계")
            {
                this.S_NM.Font = new Font("굴림", 9, FontStyle.Bold);
                this.PART_NM.Font = new Font("굴림", 9, FontStyle.Bold);

                this.AMT1.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT2.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT3.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT4.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT5.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT31.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT32.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT33.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT34.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT35.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT11.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT12.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT13.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT14.Font = new Font("굴림", 9, FontStyle.Bold);
                this.AMT15.Font = new Font("굴림", 9, FontStyle.Bold);

                this.SILJUK.Font = new Font("굴림", 9, FontStyle.Bold);
                this.CARD.Font = new Font("굴림", 9, FontStyle.Bold);
            }

            if (_fiCount == _iCount)
            {
                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }

            // 레코드가 8이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 8)
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

        private void TYACLB021R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}