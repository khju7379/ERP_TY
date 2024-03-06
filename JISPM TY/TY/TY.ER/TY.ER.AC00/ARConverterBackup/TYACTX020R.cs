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
    /// Summary description for TYACTX020R.
    /// </summary>
    public partial class TYACTX020R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();

        private string _sYEAR    = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB  = string.Empty;

        private int _RowCount = 0;
        private int _fiCount = 0;
        private int _pageCount = 1;

        private double fdMAECHUL_AMT_HAP = 0;
        private double fdMAECHUL_TAX_HAP = 0;
        private double fdMAEIP_AMT_HAP   = 0;
        private double fdMAEIP_TAX_HAP   = 0;
        private double fdGASANSE_HAP     = 0;
        private double fdGONGJAESE_HAP   = 0;
        private double fdNAPBUSE_HAP     = 0;
        private double fdYOUNGSEYUL_HAP  = 0;

        public TYACTX020R(DataTable dt, string sYEAR, string sRPTGUBN, string sCONFGB, string sAOGENNUM)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this._dt = dt;

            this._sYEAR    = sYEAR;
            this._sRPTGUBN = sRPTGUBN;
            this._sCONFGB  = sCONFGB;

            this.AOGENNUM.Value = sAOGENNUM.ToString();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (_sRPTGUBN == "1")
            {
                if (_sCONFGB == "1")
                {
                    this.SINGODATE.Value = "(신고기간:" + _sYEAR + "년 1기 1월 1일 ~ 3월 31일)";

                }
                else
                {
                    this.SINGODATE.Value = "(신고기간:" + _sYEAR + "년 1기 4월 1일 ~ 6월 30일)";
                }
            }
            else
            {
                if (_sCONFGB == "1")
                {
                    this.SINGODATE.Value = "(신고기간:" + _sYEAR + "년 2기 7월 1일 ~ 9월 30일)";

                }
                else
                {
                    this.SINGODATE.Value = "(신고기간:" + _sYEAR + "년 2기 10월 1일 ~ 12월 31일)";
                }
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            _RowCount++;

            if (_dt.Rows[_RowCount - 1]["VSBRANCH"].ToString() == "3")
            {
                this.MAECHUL_AMT.Value = "";
                this.MAECHUL_TAX.Value = "";
                this.MAEIP_AMT.Value   = "";
                this.MAEIP_TAX.Value   = "";
                this.GASANSE.Value     = "";
                this.GONGJAESE.Value   = "";
                this.NAPBUSE.Value     = "";
                this.YOUNGSEYUL.Value  = "";

                this.MAECHUL_AMT_HAP.Value = _dt.Rows[_RowCount - 1]["MAECHUL_AMT"].ToString();
                this.MAECHUL_TAX_HAP.Value = _dt.Rows[_RowCount - 1]["MAECHUL_TAX"].ToString();
                this.MAEIP_AMT_HAP.Value   = _dt.Rows[_RowCount - 1]["MAEIP_AMT"].ToString();
                this.MAEIP_TAX_HAP.Value   = _dt.Rows[_RowCount - 1]["MAEIP_TAX"].ToString();
                this.GASANSE_HAP.Value     = _dt.Rows[_RowCount - 1]["GASANSE"].ToString();
                this.GONGJAESE_HAP.Value   = _dt.Rows[_RowCount - 1]["GONGJAESE"].ToString();
                this.NAPBUSE_HAP.Value     = _dt.Rows[_RowCount - 1]["NAPBUSE"].ToString();
                this.YOUNGSEYUL_HAP.Value  = _dt.Rows[_RowCount - 1]["YOUNGSEYUL"].ToString();
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
    }
}
