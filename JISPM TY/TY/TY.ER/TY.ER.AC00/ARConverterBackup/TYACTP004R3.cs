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
    /// Summary description for TYACTP004R3.
    /// </summary>
    public partial class TYACTP004R3 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int _iRowCount = 0;
        
        public TYACTP004R3()
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

            if (_dt.Rows[0]["WRGUNMU"].ToString() == "1")
            {
                TAXOFFICE.Text = "울 산  세 무 서 장";
            }
            else
            {
                TAXOFFICE.Text = "영등포  세 무 서 장";
            }
            
           

            if (_dt.Rows[0]["WRJUMIN"].ToString().Length == 13)
            {   
                //WRJUMIN.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[0]["WRJUMIN"].ToString().Substring(6, 7);
                WRJUMIN.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 6) + "-*******";
            }
            else if (_dt.Rows[0]["WRJUMIN"].ToString().Length == 10)
            {   
                WRJUMIN.Text = _dt.Rows[0]["WRJUMIN"].ToString().Substring(0, 3) + "-" + _dt.Rows[0]["WRJUMIN"].ToString().Substring(3, 2) + "-" + _dt.Rows[0]["WRJUMIN"].ToString().Substring(5, 5);
            }
            else
            {
                WRJUMIN.Text = _dt.Rows[0]["WRJUMIN"].ToString();
            }

            if (_dt.Rows[0]["WECTGUBN"].ToString() == "68")
            {
                CHECK68.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "69")
            {
                CHECK69.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "63")
            {
                CHECK63.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "60")
            {
                CHECK60.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "61")
            {
                CHECK61.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "64")
            {
                CHECK64.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "71")
            {
                CHECK71.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "72")
            {
                CHECK72.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "73")
            {
                CHECK73.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "74")
            {
                CHECK74.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "75")
            {
                CHECK75.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "76")
            {
                CHECK76.Text = "✔";
            }
            else if (_dt.Rows[0]["WECTGUBN"].ToString() == "62")
            {
                CHECK62.Text = "✔";
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            _iRowCount++;
            if (_iRowCount == _dt.Rows.Count)
            {
               
            }

            if (_dt.Rows[_iRowCount-1]["WHOLDYUL"].ToString() != "")
            {
                this.WHOLDYUL.Text = string.Format("{0:#,##0}", double.Parse(_dt.Rows[_iRowCount-1]["WHOLDYUL"].ToString()));
            }
        }

        private void TYACTP004R3_ReportStart(object sender, EventArgs e)
        {

        }
    }
}
