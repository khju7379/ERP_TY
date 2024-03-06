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
    /// Summary description for TYACSE005R2.
    /// </summary>
    public partial class TYACSE005R2 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private DataTable _dt2 = new DataTable();
        private string _sDATE = string.Empty;
        private string _sPreValue = string.Empty;
        private int _iCount = 0;
        private int _i = 0;
        public TYACSE005R2(DataTable dt, string sDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _dt2 = dt;
            _sDATE = sDATE;
        }

        private void TYACSE005R2_ReportStart(object sender, EventArgs e)
        {
            //this.pageHeader.Visible = false;
            //this.pageFooter.Visible = false;
            _dt = this.DataSource as DataTable;
            TYACSE005R3 subReport = new TYACSE005R3();
            subReport.DataSource = _dt2;
            TYACSE005R3.Report = subReport;

        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            this.YYMMDD.Text = _sDATE.Substring(0, 4) + "년 " + _sDATE.Substring(4, 2) + "월 " + _sDATE.Substring(6, 2) + "일 현재";
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_sPreValue == this.VNSANGHO.Value.ToString())
            {
                this.VNSANGHO.Value = "";
                _iCount = 0;
            }
            else
            {
                _sPreValue = this.VNSANGHO.Value.ToString();
                _iCount++;

            }
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.reportFooter1.NewPage = NewPage.Before;
        }
    }
}
