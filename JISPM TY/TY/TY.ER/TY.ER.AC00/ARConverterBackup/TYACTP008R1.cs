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
    /// Summary description for TYACTP008R1.
    /// </summary>
    public partial class TYACTP008R1 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        DataTable _dt2 = new DataTable();
        int _iRowCount = 0;
        string _JIDATE = string.Empty;

        public TYACTP008R1(DataTable dt2, string JIDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            _dt2 = dt2;
            _JIDATE = JIDATE;

            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            ASMVNADDRS.Text = _dt2.Rows[0]["ASMVNADDRS"].ToString();
            ASMSANGHO.Text = _dt2.Rows[0]["ASMSANGHO"].ToString();
            ASMNAMENM.Text = _dt2.Rows[0]["ASMNAMENM"].ToString();
            ASMSAUPNO.Text = _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(0, 3) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(3, 2) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(5, 5);
            ASMCORPNO.Text = _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(0, 6) + "-" + _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(6, 7);
            ASMTELNUM.Text = _dt2.Rows[0]["ASMTELNUM"].ToString();
            if (_dt2.Rows[0]["OOFFNAME"].ToString() == "울산")
            {
                OFFNAME.Text = "울산광역시 남 구 청 장";
            }
            else
            {
                OFFNAME.Text = "서울특별시 영등포구청장";
            }
            DATE.Text = _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8, 2) + "일";
            YYMM.Text = _dt.Rows[0]["REVYYMM"].ToString().Substring(0, 4) + "년 " + _dt.Rows[0]["REVYYMM"].ToString().Substring(4, 2) + "월분";
        }

        private void detail_Format(object sender, EventArgs e)
        {   
            if (_dt.Rows[_iRowCount]["INCOMGB"].ToString() == "S01")
            {
                INCOMGBNM.Text = (Convert.ToInt32(_JIDATE.Substring(0, 4)) - 1).ToString() + "년귀속 연말정산";
            }
            else if (_dt.Rows[_iRowCount]["INCOMGB"].ToString() == "S02")
            {
                INCOMGBNM.Text = (Convert.ToInt32(_JIDATE.Substring(0, 4)) - 1).ToString() + "년귀속 연말정산\n이월환급세액";
            }
            else if (_dt.Rows[_iRowCount]["INCOMGB"].ToString() == "S13")
            {
                INCOMGBNM.Text = "수 정 신 고\n(" + (Convert.ToInt32(_JIDATE.Substring(0, 4)) - 1).ToString() + "년12월귀속분)";
            }
            else
            {
                INCOMGBNM.Text = _dt.Rows[_iRowCount]["INCOMGBNM"].ToString();
            }

            if (_iRowCount < 10)
            {
                line4.Visible = true;
            }
            else
            {
                line4.Visible = false;
            }
            _iRowCount++;
            
        }
    }
}
