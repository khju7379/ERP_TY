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
    /// Summary description for TYACLB017R.
    /// </summary>
    public partial class TYACLB017R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;

        private string sGroup = string.Empty;

        public TYACLB017R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            if (this.sGroup == "Change")
            {
                sGroup = "";

                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;
            }
            else
            {
                this.groupFooter1.Visible = false;

                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }

            this.line3.LineStyle = LineStyle.Dash;
            this.line3.LineWeight = 1;

            this.T2CDAC.Font = new Font("굴림", 9, FontStyle.Regular);
            this.T2SEQ.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.A1ABAC.Font = new Font("굴림", 9, FontStyle.Regular);
            this.T1RKAC.Font = new Font("굴림", 9, FontStyle.Regular);
            
            this.AMT01.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT02.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT03.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT04.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT05.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT06.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT07.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT08.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT09.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT10.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT11.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.AMT12.Font  = new Font("굴림", 9, FontStyle.Regular);
            this.HAP.Font    = new Font("굴림", 9, FontStyle.Regular);

            if (dt.Rows[_iCount - 1]["T2CDAC"].ToString() == "계 정 계" || dt.Rows[_iCount - 1]["T2CDAC"].ToString() == "부 서 계" ||
                dt.Rows[_iCount - 1]["T2CDAC"].ToString() == "총     계")
            {
                this.T2CDAC.Font = new Font("굴림", 9, FontStyle.Bold);
                this.T2SEQ.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.A1ABAC.Font = new Font("굴림", 9, FontStyle.Bold);
                this.T1RKAC.Font = new Font("굴림", 9, FontStyle.Bold);

                this.AMT01.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT02.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT03.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT04.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT05.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT06.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT07.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT08.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT09.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT10.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT11.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.AMT12.Font  = new Font("굴림", 9, FontStyle.Bold);
                this.HAP.Font    = new Font("굴림", 9, FontStyle.Bold);
            }

            if (_fiCount == _iCount)
            {
                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;
            }
            else
            {
                if (dt.Rows[_iCount - 1]["T2CDAC"].ToString() == "부 서 계")
                {
                    if (dt.Rows[_iCount - 1]["CDDESC1"].ToString() != dt.Rows[_iCount]["CDDESC1"].ToString())
                    {
                        this.line3.LineStyle = LineStyle.Solid;
                        this.line3.LineWeight = 3;
                    }
                }
            }

            // 레코드가 8이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 8)
            {
                this._rowCount = 0;

                this.line3.LineStyle = LineStyle.Solid;
                this.line3.LineWeight = 3;

                // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                this.detail.NewPage = NewPage.After;
            }
            else
            {
                this._rowCount++;
            }
        }

        private void TYACLB017R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            sGroup = "Change";

            this._rowCount = 0;
        }
    }
}