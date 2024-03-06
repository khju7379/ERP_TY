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
    /// Summary description for TYACLB014R.
    /// </summary>
    public partial class TYACLB014R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount = 0;
        private int _iCount = 0;

        private string sGroup = string.Empty;

        public TYACLB014R()
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

            this.line5.LineStyle = LineStyle.Solid;
            this.line5.LineWeight = 1;

            this.label20.Visible = true;
            this.label21.Visible = true;
            this.label22.Visible = true;
            this.label23.Visible = true;

            this.A1ABAC.Font = new Font("바탕체", 9, FontStyle.Regular);

            this.YSAMT01.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT02.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT03.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT04.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT05.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT06.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT07.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT08.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT09.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT10.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT11.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMT12.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMTSANG.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSAMTHA.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.YSHAP.Font = new Font("바탕체", 9, FontStyle.Regular);

            this.USAMT01.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT02.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT03.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT04.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT05.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT06.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT07.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT08.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT09.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT10.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT11.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMT12.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMTSANG.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USAMTHA.Font = new Font("바탕체", 9, FontStyle.Regular);
            this.USHAP.Font = new Font("바탕체", 9, FontStyle.Regular);

            this.TOTAL.Font = new Font("바탕체", 9, FontStyle.Regular);

            if (dt.Rows[_iCount - 1]["A1ABAC"].ToString() == "부 서 계")
            {
                this.label20.Visible = false;
                this.label21.Visible = false;
                this.label22.Visible = false;
                this.label23.Visible = false;

                this.A1ABAC.Font = new Font("바탕체", 9, FontStyle.Bold);

                this.YSAMT01.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT02.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT03.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT04.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT05.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT06.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT07.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT08.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT09.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT10.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT11.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMT12.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMTSANG.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSAMTHA.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.YSHAP.Font = new Font("바탕체", 9, FontStyle.Bold);

                this.USAMT01.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT02.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT03.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT04.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT05.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT06.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT07.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT08.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT09.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT10.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT11.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMT12.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMTSANG.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USAMTHA.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.USHAP.Font = new Font("바탕체", 9, FontStyle.Bold);

                this.TOTAL.Font = new Font("바탕체", 9, FontStyle.Bold);
            }

            if (_fiCount == _iCount)
            {
                this.line5.Visible = true;

                this.line5.LineStyle = LineStyle.Solid;
                this.line5.LineWeight = 3;
            }

            // 레코드가 22이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._rowCount == 5)
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
            }
        }

        private void TYACLB014R_DataInitialize(object sender, EventArgs e)
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