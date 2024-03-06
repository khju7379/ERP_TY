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
    /// Summary description for TYACSE005R.
    /// </summary>
    public partial class TYACSE005R1 : GrapeCity.ActiveReports.SectionReport
    {
        private string _sDATE = string.Empty;
        public TYACSE005R1(string sDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _sDATE = sDATE;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            DataTable dt = this.DataSource as DataTable;
            double dASPOSSCNT_HAP = 0;
            double dASSTCOKAMT_HAP = 0;

            this.YYMMDD.Text = _sDATE.Substring(0, 4) + "년 " + _sDATE.Substring(4, 2) + "월 " + _sDATE.Substring(6, 2) + "일 현재";
            this.SUBNM.Text = "(주)태영인더스트리";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dASPOSSCNT_HAP += Convert.ToDouble(dt.Rows[i]["ASPOSSCNT"]);
                dASSTCOKAMT_HAP += Convert.ToDouble(dt.Rows[i]["ASSTCOKAMT"]);
            }

            this.ASPOSSCNT_HAP.Value = dASPOSSCNT_HAP.ToString();
            this.ASSTCOKAMT_HAP.Value = dASSTCOKAMT_HAP.ToString();
        }
    }
}
