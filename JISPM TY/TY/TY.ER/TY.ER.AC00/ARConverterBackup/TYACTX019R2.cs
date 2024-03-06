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
    /// Summary description for TYACTX019R.
    /// </summary>
    public partial class TYACTX019R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();

        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;

        public TYACTX019R2(DataTable dt, string sYEAR, string sRPTGUBN, string sCONFGB)
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

        private void reportHeader1_Format(object sender, EventArgs e)
        {   
            DataTable dttmp = this.DataSource as DataTable;

            if (_sRPTGUBN == "1")
            {
                if (_sCONFGB == "1")
                {
                    this.STEDDATE.Value = "(1월 1일 ~ 3월 31일)";
                    this.SINGODATE.Value = _sYEAR + "년 3월 31일";
                    this.ASCONFGB01.Value = "✔";
                }
                else
                {
                    this.STEDDATE.Value = "(4월 1일 ~ 6월 30일)";
                    this.SINGODATE.Value = _sYEAR + "년 6월 30일";
                    this.ASCONFGB02.Value = "✔";
                }
            }
            else
            {
                if (_sCONFGB == "1")
                {
                    this.STEDDATE.Value = "(7월 1일 ~ 년 9월 30일)";
                    this.SINGODATE.Value = _sYEAR + "년 9월 30일";
                    this.ASCONFGB01.Value = "✔";
                }
                else
                {
                    this.STEDDATE.Value = "(10월 1일 ~ 12월 31일)";
                    this.SINGODATE.Value = _sYEAR + "년 12월 31일";
                    this.ASCONFGB02.Value = "✔";
                }
            }
            try
            {
                if (Convert.ToInt32(dttmp.Rows[0]["VS04SC25TAX"].ToString()) < 0)
                {
                    this.YUNGSE.Value = "✔";
                }
            }
            catch
            {
            }

            this.VSRPTGUBN.Value = _sRPTGUBN;
            this.VSYEAR.Value = _sYEAR;

            string[] sASSAUPNO = _dt.Rows[0]["ASMSAUPNO"].ToString().Split('-');

            this.ASMSAUPNO.Value = _dt.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSAUPNO01.Value = sASSAUPNO[0];
            this.ASMSAUPNO02.Value = sASSAUPNO[1];
            this.ASMSAUPNO03.Value = sASSAUPNO[2];
            this.ASMSANGHO.Value = _dt.Rows[0]["ASMSANGHO"].ToString();
            this.ASMVNADDRS.Value = _dt.Rows[0]["ASMVNADDRS"].ToString();
            this.ASMTELNUM.Value = _dt.Rows[0]["ASMTELNUM"].ToString();
            this.ASMNAMENM.Value = _dt.Rows[0]["ASMNAMENM"].ToString();
            this.ASMCORPNO.Value = _dt.Rows[0]["ASMCORPNO"].ToString();
            this.ASMTAXAREA.Value = _dt.Rows[0]["ASMTAXAREA"].ToString();
            this.SINGONM.Value = "(주)태영인더스트리";
            this.VS05EV26BUS.Value = _dt.Rows[0]["CHULUPTAE"].ToString();
            this.VS05EV26NAM.Value = _dt.Rows[0]["CHULMEVENT"].ToString();
        }
    }
}
