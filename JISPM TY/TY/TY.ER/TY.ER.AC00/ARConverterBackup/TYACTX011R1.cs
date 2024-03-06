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
    /// Summary description for TYACTX011R1.
    /// </summary>
    public partial class TYACTX011R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private DataTable _dt2 = new DataTable();

        private int _RowCount = 0;
        
        private string _sYEAR = string.Empty;
        private string _sRPTGUBN = string.Empty;
        private string _sCONFGB = string.Empty;
        
        private double _fiNowPage = 1;

        public TYACTX011R1(DataTable dt, DataTable dt2, string sYEAR, string sRPTGUBN, string sCONFGB)
        {
            this._dt = dt;
            this._dt2 = dt2;

            this._sYEAR = sYEAR;
            this._sRPTGUBN = sRPTGUBN;
            this._sCONFGB = sCONFGB;
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYACTX011R1_ReportStart(object sender, EventArgs e)
		{
            this.pageHeader1.Visible = false;
            this.pageFooter1.Visible = false;

            DataTable dt= this.DataSource as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 5)
                {
                    TYACTX011R2 subReport = new TYACTX011R2();
                    subReport.DataSource = dt;
                    TYACTX011R2.Report = subReport;
                }
            }
		}

        private void detail_Format(object sender, EventArgs e)
        {
            _RowCount++;

            detail.Visible = true;

            if (_RowCount > 5)
            {
                detail.Visible = false;
            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            if (_RowCount > 5)
            {
                this.pageHeader1.Visible = true;
                
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

            this.S1RPTGUBN.Value = _sRPTGUBN;
            this.S1YEAR.Value = _sYEAR;
            this.S1RPTGUBN2.Value = _sRPTGUBN;
            this.S1YEAR2.Value = _sYEAR;

            this.ASMSAUPNO.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSAUPNO2.Value = _dt2.Rows[0]["ASMSAUPNO"].ToString();
            this.ASMSANGHO.Value = _dt2.Rows[0]["ASMSANGHO"].ToString();
            this.ASMNAMENM.Value = _dt2.Rows[0]["ASMNAMENM"].ToString();
            this.ASMVNADDRS.Value = _dt2.Rows[0]["ASMVNADDRS"].ToString();

            this.VNCODE_CNT1.Value = _dt.Rows[0]["VNCODE_CNT"].ToString();
            this.MAESU_CNT1.Value = _dt.Rows[0]["MAESU_CNT"].ToString();
            this.HAP_AMT1.Value = _dt.Rows[0]["HAP_AMT"].ToString();
            this.HAP_VAT1.Value = _dt.Rows[0]["HAP_VAT"].ToString();

            this.VNCODE_CNT2.Value = _dt.Rows[1]["VNCODE_CNT"].ToString();
            this.MAESU_CNT2.Value = _dt.Rows[1]["MAESU_CNT"].ToString();
            this.HAP_AMT2.Value = _dt.Rows[1]["HAP_AMT"].ToString();
            this.HAP_VAT2.Value = _dt.Rows[1]["HAP_VAT"].ToString();

            this.VNCODE_CNT3.Value = _dt.Rows[2]["VNCODE_CNT"].ToString();
            this.MAESU_CNT3.Value = _dt.Rows[2]["MAESU_CNT"].ToString();
            this.HAP_AMT3.Value = _dt.Rows[2]["HAP_AMT"].ToString();
            this.HAP_VAT3.Value = _dt.Rows[2]["HAP_VAT"].ToString();

            this.VNCODE_CNT4.Value = _dt.Rows[3]["VNCODE_CNT"].ToString();
            this.MAESU_CNT4.Value = _dt.Rows[3]["MAESU_CNT"].ToString();
            this.HAP_AMT4.Value = _dt.Rows[3]["HAP_AMT"].ToString();
            this.HAP_VAT4.Value = _dt.Rows[3]["HAP_VAT"].ToString();

            this.VNCODE_CNT5.Value = _dt.Rows[4]["VNCODE_CNT"].ToString();
            this.MAESU_CNT5.Value = _dt.Rows[4]["MAESU_CNT"].ToString();
            this.HAP_AMT5.Value = _dt.Rows[4]["HAP_AMT"].ToString();
            this.HAP_VAT5.Value = _dt.Rows[4]["HAP_VAT"].ToString();

            this.VNCODE_CNT6.Value = _dt.Rows[5]["VNCODE_CNT"].ToString();
            this.MAESU_CNT6.Value = _dt.Rows[5]["MAESU_CNT"].ToString();
            this.HAP_AMT6.Value = _dt.Rows[5]["HAP_AMT"].ToString();
            this.HAP_VAT6.Value = _dt.Rows[5]["HAP_VAT"].ToString();

            this.VNCODE_CNT7.Value = _dt.Rows[6]["VNCODE_CNT"].ToString();
            this.MAESU_CNT7.Value = _dt.Rows[6]["MAESU_CNT"].ToString();
            this.HAP_AMT7.Value = _dt.Rows[6]["HAP_AMT"].ToString();
            this.HAP_VAT7.Value = _dt.Rows[6]["HAP_VAT"].ToString();
            
        }

        private void pageHeader1_Format(object sender, EventArgs e)
        {
            if (_RowCount > 5)
            {
                this.pageFooter1.Visible = true;
            }
            else
            {
                this.pageFooter1.Visible = false;
            }
        }

        private void pageFooter1_Format(object sender, EventArgs e)
        {
            this.PAGENUM.Value = _fiNowPage;
            _fiNowPage++;
        }
    }
}
