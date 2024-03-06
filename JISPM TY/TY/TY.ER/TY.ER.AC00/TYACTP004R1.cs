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
    /// Summary description for TYACTP004R1.
    /// </summary>
    public partial class TYACTP004R1 : GrapeCity.ActiveReports.SectionReport
    {
        private int _iRowCount = 0;
        private DataTable _dt = new DataTable();

        public TYACTP004R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;
            DATE.Text = string.Format("{0:yyyy년 MM월 dd일}", System.DateTime.Now);
            WREYY.Text = _dt.Rows[0]["WREYY"].ToString() + " 년";
            int IWREMM = Convert.ToInt32(_dt.Rows[0]["WREMM"].ToString());

            if (_dt.Rows[0]["WNATIVEGB"].ToString() == "1")
            {
                WNATIVEGB2.Text = "✔";
            }
            else
            {
                WNATIVEGB1.Text = "✔";
            }

            if (IWREMM > 9)
            {
                BUNGI4.Text = "✔";
            }
            else if (IWREMM > 6)
            {
                BUNGI3.Text = "✔";
            }
            else if (IWREMM > 3)
            {
                BUNGI2.Text = "✔";
            }
            else
            {
                BUNGI1.Text = "✔";
            }

            if (_dt.Rows[0]["WRJUMIN"].ToString().Length == 13)
            {
                //WRJUMIN.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[0]["WRJUMIN"].ToString().Substring(6, 7);
                WRJUMIN.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 6) + "-*******";
            }
            else
            {
                WRJUMIN.Text = _dt.Rows[0]["WRJUMIN"].ToString();
            }
            if (_dt.Rows[0]["WTRADTEL"].ToString().Length == 10)
            {
                WTRADTEL.Text = _dt.Rows[0]["WTRADTEL"].ToString().Substring(0, 3) + "-" + _dt.Rows[0]["WTRADTEL"].ToString().Substring(3, 3) + "-" + _dt.Rows[0]["WTRADTEL"].ToString().Substring(6, 4);
            }
            else if (_dt.Rows[0]["WTRADTEL"].ToString().Length == 11)
            {
                WTRADTEL.Text = _dt.Rows[0]["WTRADTEL"].ToString().Substring(0, 3) + "-" + _dt.Rows[0]["WTRADTEL"].ToString().Substring(3, 4) + "-" + _dt.Rows[0]["WTRADTEL"].ToString().Substring(7, 4);
            }
            else
            {
                WTRADTEL.Text = _dt.Rows[0]["WTRADTEL"].ToString();
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_dt.Rows[_iRowCount]["WMONTH"].ToString() != "")
            {
                int iDate = Convert.ToInt32(_dt.Rows[_iRowCount]["WMONTH"].ToString());

                WMONTH1.Text = iDate.ToString() + " 월";
                if (iDate == 1)
                {
                    WMONTH2.Text = "12 월";
                }
                else
                {
                    WMONTH2.Text = (iDate - 1).ToString() + " 월";
                }
            }
            else
            {
                WMONTH1.Text = "";
                WMONTH2.Text = "";
            }
            if (_iRowCount == _dt.Rows.Count - 1)
            {
                line16.Visible = false;
            }
            _iRowCount++;
        }
    }
}
