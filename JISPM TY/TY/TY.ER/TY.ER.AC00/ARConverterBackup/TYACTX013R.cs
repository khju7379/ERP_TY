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
    /// Summary description for TYACTX013R.
    /// </summary>
    public partial class TYACTX013R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();

        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;

        public TYACTX013R(DataTable dt, string sYEAR, string sRPTGUBN, string sCONFGB)
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
            DataTable dt = this.DataSource as DataTable;

            if (_sRPTGUBN == "1")
            {
                if (_sCONFGB == "1")
                {
                    this.STEDDATE.Value = "(" + _sYEAR + "년 1월 1일 ~ " + _sYEAR + "년 3월 31일)";
                    this.DATE.Value = _sYEAR + "년 3월 31일";
                }
                else
                {
                    this.STEDDATE.Value = "(" + _sYEAR + "년 4월 1일 ~ " + _sYEAR + "년 6월 30일)";
                    this.DATE.Value = this.DATE.Value = _sYEAR + "년 6월 30일";
                }
            }
            else
            {
                if (_sCONFGB == "1")
                {
                    this.STEDDATE.Value = "(" + _sYEAR + "년 7월 1일 ~ " + _sYEAR + "년 9월 30일)";
                    this.DATE.Value = this.DATE.Value = _sYEAR + "년 9월 30일";
                }
                else
                {
                    this.STEDDATE.Value = "(" + _sYEAR + "년 10월 1일 ~ " + _sYEAR + "년 12월 31일)";
                    this.DATE.Value = this.DATE.Value = _sYEAR + "년 12월 31일";
                }
            }
            this.S3RPTGUBN.Value = _sRPTGUBN;
            this.S3YEAR.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt.Rows[0]["ASMSANGHO"].ToString();
            this.ASMSANGHO1.Value = _dt.Rows[0]["ASMSANGHO"].ToString();
            this.ASMNAMENM.Value = _dt.Rows[0]["ASMNAMENM"].ToString();
            this.ASMUPTAE.Value = _dt.Rows[0]["CHULUPTAE"].ToString();
            this.ASMEVENT.Value = _dt.Rows[0]["CHULMEVENT"].ToString();
            this.ASMTAXAREA.Value = _dt.Rows[0]["ASMTAXAREA"].ToString();

            this.GUNSU_CNT1.Value = dt.Rows[0]["GUNSU_CNT"].ToString();
            this.HAP_AMT1.Value = dt.Rows[0]["HAP_AMT"].ToString();
            this.HAP_VAT1.Value = dt.Rows[0]["HAP_VAT"].ToString();

            this.GUNSU_CNT2.Value = dt.Rows[1]["GUNSU_CNT"].ToString();
            this.HAP_AMT2.Value = dt.Rows[1]["HAP_AMT"].ToString();
            this.HAP_VAT2.Value = dt.Rows[1]["HAP_VAT"].ToString();

            this.GUNSU_CNT3.Value = dt.Rows[2]["GUNSU_CNT"].ToString();
            this.HAP_AMT3.Value = dt.Rows[2]["HAP_AMT"].ToString();
            this.HAP_VAT3.Value = dt.Rows[2]["HAP_VAT"].ToString();

            this.GUNSU_CNT4.Value = dt.Rows[3]["GUNSU_CNT"].ToString();
            this.HAP_AMT4.Value = dt.Rows[3]["HAP_AMT"].ToString();
            this.HAP_VAT4.Value = dt.Rows[3]["HAP_VAT"].ToString();

            this.GUNSU_CNT5.Value = dt.Rows[4]["GUNSU_CNT"].ToString();
            this.HAP_AMT5.Value = dt.Rows[4]["HAP_AMT"].ToString();
            this.HAP_VAT5.Value = dt.Rows[4]["HAP_VAT"].ToString();
        }
    }
}
