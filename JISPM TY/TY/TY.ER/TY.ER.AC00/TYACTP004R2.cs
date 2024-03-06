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
    /// Summary description for TYACTP004R2.
    /// </summary>
    public partial class TYACTP004R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int _iRowCount = 0;

        public TYACTP004R2()
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

            WTRADNAME1.Text = "";
            WRJUMIN1.Text = "";
            WADDRESS1.Text = "";
            WTRADNAME2.Text = "";
            WRJUMIN2.Text = "";
            WADDRESS2.Text = "";

            if (_dt.Rows[0]["WRJUMIN"].ToString().Length == 13)
            {
                WTRADNAME2.Text = _dt.Rows[0]["WTRADNAME"].ToString();
                //WRJUMIN2.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[0]["WRJUMIN"].ToString().Substring(6, 7);
                WRJUMIN2.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 6) + "-*******";
                WADDRESS2.Text = _dt.Rows[0]["WADDRESS"].ToString();
            }
            else if (_dt.Rows[0]["WRJUMIN"].ToString().Length == 10)
            {
                WTRADNAME2.Text = _dt.Rows[0]["WTRADNAME"].ToString();
                WRJUMIN2.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 3) + "-" + _dt.Rows[0]["WRJUMIN"].ToString().Substring(3, 2) + "-" + _dt.Rows[0]["WRJUMIN"].ToString().Substring(5, 5);
                WADDRESS2.Text = _dt.Rows[0]["WADDRESS"].ToString();
            }
            else if (_dt.Rows[0]["WRJUMIN"].ToString().Length == 14)
            {
                WTRADNAME2.Text = _dt.Rows[0]["WTRADNAME"].ToString();
                WRJUMIN2.Text = _dt.Rows[0]["WRJUMIN"].ToString();
                WADDRESS2.Text = _dt.Rows[0]["WADDRESS"].ToString();
            }
            else
            {
                WTRADNAME2.Text = _dt.Rows[0]["WTRADNAME"].ToString();
                WRJUMIN2.Text = _dt.Rows[0]["WRJUMIN"].ToString();
                WADDRESS2.Text = _dt.Rows[0]["WADDRESS"].ToString();
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_iRowCount == _dt.Rows.Count - 1)
            {
                line17.Visible = false;
            }
            _iRowCount++;
        }
    }
}
