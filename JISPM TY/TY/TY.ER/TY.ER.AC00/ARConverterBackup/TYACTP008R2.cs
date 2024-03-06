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
    /// Summary description for TYACTP008R2.
    /// </summary>
    public partial class TYACTP008R2 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        DataTable _dt2 = new DataTable();
        int _iRowCount = 0;
        string _JIDATE = string.Empty;
        string _AMT = string.Empty;

        public TYACTP008R2(DataTable dt2, string JIDATE, string AMT)
        {
            //
            // Required for Windows Form Designer support
            //
            _dt2 = dt2;
            _JIDATE = JIDATE;
            _AMT = AMT;

            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            ASMVNADDRS1.Text = _dt2.Rows[0]["ASMVNADDRS"].ToString();
            ASMVNADDRS2.Text = _dt2.Rows[0]["ASMVNADDRS"].ToString();
            ASMVNADDRS3.Text = _dt2.Rows[0]["ASMVNADDRS"].ToString();
            ASMSANGHO1.Text = _dt2.Rows[0]["ASMSANGHO"].ToString();
            ASMSANGHO2.Text = _dt2.Rows[0]["ASMSANGHO"].ToString();
            ASMSANGHO3.Text = _dt2.Rows[0]["ASMSANGHO"].ToString();
            ASMSAUPNO1.Text = _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(0, 3) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(3, 2) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(5, 5);
            ASMSAUPNO2.Text = _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(0, 3) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(3, 2) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(5, 5);
            ASMSAUPNO3.Text = _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(0, 3) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(3, 2) + "-" + _dt2.Rows[0]["ASMSAUPNO"].ToString().Substring(5, 5);
            ASMCORPNO1.Text = _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(0, 6) + "-" + _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(6, 7);
            ASMCORPNO2.Text = _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(0, 6) + "-" + _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(6, 7);
            ASMCORPNO3.Text = _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(0, 6) + "-" + _dt2.Rows[0]["ASMCORPNO"].ToString().Substring(6, 7);
            ASMTELNUM1.Text = _dt2.Rows[0]["ASMTELNUM"].ToString();
            ASMTELNUM2.Text = _dt2.Rows[0]["ASMTELNUM"].ToString();
            ASMTELNUM3.Text = _dt2.Rows[0]["ASMTELNUM"].ToString();
            AMT1.Text = _AMT;
            AMT2.Text = _AMT;
            AMT3.Text = _AMT;

            if(_dt2.Rows[0]["OOFFNAME"].ToString() == "울산")
            {
                OOFFNAME1.Text = "울산 남구청";
                OOFFNAME2.Text = "울산 남구청";
                OOFFNAME3.Text = "울산 남구청";
                LOFFNAME1.Text = "울산 남구청장 귀하";
                ROFFNAME1.Text = "울산 남구청장 귀하";
            }
            else{
                OOFFNAME1.Text = "영등포구청";
                OOFFNAME2.Text = "영등포구청";
                OOFFNAME3.Text = "영등포구청";
                LOFFNAME1.Text = "영등포구청장 귀하";
                ROFFNAME1.Text = "영등포구청장 귀하";
            }
            SANGHO1.Text = _dt2.Rows[0]["ASMSANGHO"].ToString();

            JIDATE1.Text = _dt.Rows[0]["REVYYMM"].ToString().Substring(0, 4) + "년 " + _dt.Rows[0]["REVYYMM"].ToString().Substring(4, 2) +
                           "월분 (지급일 : " + _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8,2) + "일)";
            JIDATE2.Text = _dt.Rows[0]["REVYYMM"].ToString().Substring(0, 4) + "년 " + _dt.Rows[0]["REVYYMM"].ToString().Substring(4, 2) +
                           "월분 (지급일 : " + _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8, 2) + "일)";
            JIDATE3.Text = _dt.Rows[0]["REVYYMM"].ToString().Substring(0, 4) + "년 " + _dt.Rows[0]["REVYYMM"].ToString().Substring(4, 2) +
                           "월분 (지급일 : " + _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8, 2) + "일)";

            LDATE1.Text = _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8, 2) + "일";
            LDATE2.Text = _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8, 2) + "일";
            LDATE3.Text = _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8, 2) + "일";
            RDATE1.Text = _JIDATE.Substring(0, 4) + "년 " + _JIDATE.Substring(5, 2) + "월 " + _JIDATE.Substring(8, 2) + "일";
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_iRowCount < 6)
            {
                line17.Visible = true;
                line34.Visible = true;
                line50.Visible = true;
            }
            else
            {
                line17.Visible = false;
                line34.Visible = false;
                line50.Visible = false;
            }
            _iRowCount++;
        }
    }
}
