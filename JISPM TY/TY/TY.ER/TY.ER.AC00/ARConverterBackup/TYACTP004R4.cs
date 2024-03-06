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
    /// Summary description for TYACTP004R4.
    /// </summary>
    public partial class TYACTP004R4 : GrapeCity.ActiveReports.SectionReport
    {
        private int _iRowCount = 0;
        private DataTable _dt = new DataTable();

        public TYACTP004R4()
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

            if (_dt.Rows[0]["WCOUNTRY"].ToString() == "KR")
            {
                GUJU1.Text = "■";
                GUJU2.Text = "□";
            }
            else
            {
                GUJU1.Text = "□";
                GUJU2.Text = "■";
            }

            if (_dt.Rows[0]["ASMSAUPNO"].ToString() == "610-81-10449")
            {
                txtAREA.Text = "울산 세무서장";
            }
            else if (_dt.Rows[0]["ASMSAUPNO"].ToString() == "105-82-16181")
            {
                txtAREA.Text = "영등포 세무서장";
            }

            if (_dt.Rows[0]["WGBINCOM"].ToString() == "111")
            {
                

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
                WRYYMMDD.Text = "";
            }
            else if (_dt.Rows[0]["WGBINCOM"].ToString() == "211")
            {
                WRJUMIN.Text = "";
                WRYYMMDD.Text = "";
            }

            if (_dt.Rows[0]["WTRUSTYN"].ToString() == "Y")
            {
                WTRUSTYN1.Text = "■";
                WTRUSTYN2.Text = "□";
            }
            else if (_dt.Rows[0]["WTRUSTYN"].ToString() == "N")
            {
                WTRUSTYN1.Text = "□";
                WTRUSTYN2.Text = "■";
            }

            
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_dt.Rows[_iRowCount]["WREYYMM"].ToString() != "")
            {
                if (_dt.Rows[_iRowCount]["WINCOME"].ToString() == "A50")
                {
                    WINCOM.Text = "이자";
                }
                else if (_dt.Rows[_iRowCount]["WINCOME"].ToString() == "A60")
                {
                    WINCOM.Text = "배당";
                    WINTRATE.Text = "";
                }
            }
            else
            {
                WINCOM.Text = "";
            }

            _iRowCount++;
        }
    }
}
