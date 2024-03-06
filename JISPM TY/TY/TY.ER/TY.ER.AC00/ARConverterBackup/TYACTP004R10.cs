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
    /// Summary description for TYACTP004R10.
    /// </summary>
    public partial class TYACTP004R10 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        int _iRowCount = 0;
        int _iNum = 1;
        public TYACTP004R10()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            detail.Visible = false;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;
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
                            WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(6, 7);
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
                    WRJUMIN.Text = _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[_iRowCount]["WRJUMIN"].ToString().Substring(6, 7);
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
                    WMONTH2.Text = string.Format("{0:0#}", (iDate - 1));
                }
            }
            else
            {
                WMONTH1.Text = "";
                WMONTH2.Text = "";
            }
            if (_iRowCount > 17)
            {   
                detail.Visible = true;
            }
            _iRowCount++;
        }
    }
}
