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
    /// Summary description for TYACTX017R.
    /// </summary>
    public partial class TYACTX017R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private DataTable _dt2 = new DataTable();

        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;

        private int _RowCount = 0;
        private int _fiCount = 0;
        private int _pageCount = 1;

        public TYACTX017R(DataTable dt, string sYEAR, string sRPTGUBN, string sCONFGB)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this._dt = dt;
            

            this._sYEAR = sYEAR;
            this._sRPTGUBN = sRPTGUBN;
            this._sCONFGB = sCONFGB;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            
        }

        private void detail_Format(object sender, EventArgs e)
        {
            _RowCount++;
            this._dt2 = this.DataSource as DataTable;
            if (_dt2.Rows[_RowCount - 1]["S8JPNO"].ToString() == "1")
            {
                this.NUMBER.Value = "";
                this.S8DOCNUM.Value = "";
                this.S8ISSUER.Value = "";
                this.S8DATE.Value = "";
                this.S8SHIPDT.Value = "";
                this.FRDESC.Value = "";
                this.S8CXCHAN.Value = "";

                this.S8SBFORAMT.Value = "";
                this.S8SBWONAMT.Value = "";
                this.S8REFORAMT.Value = "";
                this.S8REWONAMT.Value = "";

                this.NUMBER_HAP.Value = _dt2.Rows[_RowCount - 1]["NUMBER"].ToString();
                this.S8SBFORAMT_HAP.Value = _dt2.Rows[_RowCount - 1]["S8SBFORAMT"].ToString();
                this.S8SBWONAMT_HAP.Value = _dt2.Rows[_RowCount - 1]["S8SBWONAMT"].ToString();
                this.S8REFORAMT_HAP.Value = _dt2.Rows[_RowCount - 1]["S8REFORAMT"].ToString();
                this.S8REWONAMT_HAP.Value = _dt2.Rows[_RowCount - 1]["S8REWONAMT"].ToString();
            }

            if (this._pageCount == 1)
            {
                if (this._fiCount == 12)
                {
                    this._fiCount = 0;

                    this.detail.NewPage = NewPage.Before;
                    this._fiCount++;
                    this._pageCount++;
                }
                else
                {
                    this._fiCount++;

                    this.detail.NewPage = NewPage.None;
                }
            }
            else
            {
                if (this._fiCount == 18)
                {
                    this._fiCount = 0;

                    this.detail.NewPage = NewPage.Before;
                    this._fiCount++;
                    this._pageCount++;
                }
                else
                {
                    this._fiCount++;

                    this.detail.NewPage = NewPage.None;
                }
            }
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            if (_sRPTGUBN == "1")
            {
                if (_sCONFGB == "1")
                {
                    this.STEDDATE.Value = _sYEAR + "년 1월 1일 ~ " + _sYEAR + "년 3월 31일";
                    this.DATE.Value = _sYEAR + "년 3월 31일";
                }
                else
                {
                    this.STEDDATE.Value = _sYEAR + "년 4월 1일 ~ " + _sYEAR + "년 6월 30일";
                    this.DATE.Value = _sYEAR + "년 6월 30일";
                }
            }
            else
            {
                if (_sCONFGB == "1")
                {
                    this.STEDDATE.Value = _sYEAR + "년 7월 1일 ~ " + _sYEAR + "년 9월 30일";
                    this.DATE.Value = _sYEAR + "년 9월 30일";
                }
                else
                {
                    this.STEDDATE.Value = _sYEAR + "년 10월 1일 ~ " + _sYEAR + "년 12월 31일";
                    this.DATE.Value = _sYEAR + "년 12월 31일";
                }
            }

            this.S8RPTGUBN.Value = _sRPTGUBN;
            this.S8YEAR.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt.Rows[0]["ASMSANGHO"].ToString();
            this.ASMVNADDRS.Value = _dt.Rows[0]["ASMVNADDRS"].ToString();
            this.ASMUPTAE.Value = _dt.Rows[0]["CHULUPTAE"].ToString() + "(" + _dt.Rows[0]["CHULMEVENT"].ToString() + ")";
            this.ASMTELNUM.Value = "☎)" + _dt.Rows[0]["ASMTELNUM"].ToString();
            this.ASMNAMENM.Value = _dt.Rows[0]["ASMNAMENM"].ToString();
            this.AOZEROTAXNM.Value = _dt.Rows[0]["AOZEROTAXNM"].ToString();
        }
    }
}
