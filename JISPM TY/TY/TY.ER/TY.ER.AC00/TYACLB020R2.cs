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
    /// Summary description for TYACLB020R2.
    /// </summary>
    public partial class TYACLB020R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;
        private string fsEXISTS = string.Empty;

        private string sGroup = string.Empty;

        public TYACLB020R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            if (dt.Rows[_iCount - 1]["CHK"].ToString() == "1")
            {
                this.line5.Visible = false;
            }
            else
            {
                this.line5.Visible = true;
            }

            this.line5.LineStyle = LineStyle.Dash;
            this.line5.LineWeight = 1;


            this.A1ABAC.Font = new Font("바탕체", 8, FontStyle.Regular);

            this.AMT01.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT02.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT03.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT04.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT05.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT06.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT07.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT08.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT09.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT10.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT11.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.AMT12.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.HAP.Font = new Font("바탕체", 9, FontStyle.Regular);

            if (dt.Rows[_iCount - 1]["A1ABAC"].ToString() == "총 계")
            {
                fsEXISTS = "EXISTS";

                this.A1ABAC.Font = new Font("바탕체", 8, FontStyle.Bold);

                this.AMT01.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT02.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT03.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT04.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT05.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT06.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT07.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT08.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT09.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT10.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT11.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT12.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.HAP.Font = new Font("바탕체", 9, FontStyle.Bold);
            }

            if (fsEXISTS != "")
            {
                this.A1ABAC.Font = new Font("바탕체", 8, FontStyle.Bold);

                this.AMT01.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT02.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT03.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT04.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT05.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT06.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT07.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT08.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT09.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT10.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT11.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.AMT12.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.HAP.Font = new Font("바탕체", 9, FontStyle.Bold);
            }

            if (_fiCount == _iCount)
            {
                this.line5.Visible = true;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }

            // 레코드가 26이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
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

        private void TYACLB020R2_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}