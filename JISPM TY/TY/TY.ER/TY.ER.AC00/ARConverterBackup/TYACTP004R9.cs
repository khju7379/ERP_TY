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
    /// Summary description for TYACTP004R9.
    /// </summary>
    public partial class TYACTP004R9 : GrapeCity.ActiveReports.SectionReport
    {
        private int _iRowCount = 0;
        private int _iNum = 1;
        private DataTable _dt = new DataTable();
        private DataTable _dt2 = new DataTable();

        public TYACTP004R9(DataTable dt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this.pageHeader.Visible = false;
            _dt2 = dt;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            int iMANCOUNT = 0;
            int iCOUNT = 0;
            _dt = this.DataSource as DataTable;

            WTOTALAMTHAP.Text = string.Format("{0:#,###}",_dt2.Rows[0]["WTOTALAMT"]);
            WTAXINCOMHAP.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WTAXINCOM"]);
            WLOCALTAXHAP.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WLOCALTAX"]);
            textBox1.Text = _dt.Rows[0]["ASMNAMENM"].ToString();
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                if (_dt.Rows[i]["WRJUMIN"].ToString() != "")
                {
                    iCOUNT++;
                }
                if (i != 0)
                { 
                    if (_dt.Rows[i]["WRJUMIN"].ToString() != _dt.Rows[i - 1]["WRJUMIN"].ToString())
                    {
                        iMANCOUNT++;
                    }
                }
            }

            COUNT.Text = iCOUNT.ToString() + " 건";
            MANCOUNT.Text = (iMANCOUNT).ToString() + " 명";

            int iBungi = Convert.ToInt32(_dt.Rows[0]["WMONTH"].ToString());

            if (iBungi < 4)
            {
                this.BUNGI1.Text = "✔";
                this.SBUNGI1.Text = "✔";
            }
            else if (iBungi < 7)
            {
                this.BUNGI2.Text = "✔";
                this.SBUNGI2.Text = "✔";
            }
            else if (iBungi < 10)
            {
                this.BUNGI3.Text = "✔";
                this.SBUNGI3.Text = "✔";
            }
            else
            {
                this.BUNGI4.Text = "✔";
                this.SBUNGI4.Text = "✔";
            }
            this.WREYY.Text = _dt.Rows[0]["WREYYMM"].ToString().Substring(0, 4);
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.SWREYY.Text = _dt.Rows[0]["WREYYMM"].ToString().Substring(0,4);
            this.SASMSAUPNO.Text = _dt.Rows[0]["ASMSAUPNO"].ToString();
        }

        private void detail_Format(object sender, EventArgs e)
        {

            if (_iRowCount != 0)
            {
                if (_dt.Rows[_iRowCount]["WRJUMIN"].ToString() != "")
                {
                    if (_dt.Rows[_iRowCount]["WRJUMIN"].ToString() != _dt.Rows[_iRowCount - 1]["WRJUMIN"].ToString())
                    {
                        this.NUM.Text = _iNum.ToString();
                        this.WTRADNAME.Text = _dt.Rows[_iRowCount]["WTRADNAME"].ToString();
                        this.WTRADTEL.Text = _dt.Rows[_iRowCount]["WTRADTEL"].ToString();
                        this.WNATIVEGB.Text = _dt.Rows[_iRowCount]["WNATIVEGB"].ToString();
                        if (_dt.Rows[_iRowCount]["WRJUMIN"].ToString().Length == 13)
                        {
                            //WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(6, 7);
                            WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(0, 6) + "-*******";
                        }
                        else
                        {
                            WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString();
                        }
                        _iNum++;
                    }
                    else
                    {
                        this.NUM.Text = "";
                        this.WTRADNAME.Text = "";
                        this.WTRADTEL.Text = "";
                        this.WNATIVEGB.Text = "";
                        this.WRJUMIN.Text = "";
                    }
                }
                else
                {
                    this.NUM.Text = "";
                    this.WTRADNAME.Text = "";
                    this.WTRADTEL.Text = "";
                    this.WNATIVEGB.Text = "";
                    this.WRJUMIN.Text = "";
                }
            }
            else
            {
                this.NUM.Text = _iNum.ToString();
                this.WTRADNAME.Text = _dt.Rows[_iRowCount]["WTRADNAME"].ToString();
                this.WTRADTEL.Text = _dt.Rows[_iRowCount]["WTRADTEL"].ToString();
                this.WNATIVEGB.Text = _dt.Rows[_iRowCount]["WNATIVEGB"].ToString();
                if (_dt.Rows[_iRowCount]["WRJUMIN"].ToString().Length == 13)
                {
                    //WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(6, 7);
                    WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(0, 6) + "-*******";
                }
                else
                {
                    WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString();
                }
                _iNum++;
            }

            if (_dt.Rows[_iRowCount]["WMONTH"].ToString() != "")
            {
                int iDate = Convert.ToInt32(_dt.Rows[_iRowCount]["WMONTH"].ToString());

                //WMONTH1.Text = iDate.ToString();
                WMONTH1.Text = string.Format("{0:0#}", iDate);
                if (iDate == 1)
                {
                    WMONTH2.Text = "12";
                }
                else
                {
                    WMONTH2.Text = string.Format("{0:0#}", (iDate-1));
                }
            }
            else
            {
                WMONTH1.Text = "";
                WMONTH2.Text = "";
            }
            if (_iRowCount > 16)
            {
                TYACTP004R10 subReport = new TYACTP004R10();
                subReport.DataSource = _dt;
                TYACTP004R10.Report = subReport;
                pageHeader.Visible = true;
                detail.Visible = false;
            }
            _iRowCount++;
        }
    }
}
