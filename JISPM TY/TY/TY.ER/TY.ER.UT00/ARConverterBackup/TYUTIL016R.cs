using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTIL016R.
    /// </summary>
    public partial class TYUTIL016R : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int ficount = 0;

        public TYUTIL016R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
            
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            string sFilter = "VNSANGHO = '" + _dt.Rows[ficount]["VNSANGHO"].ToString() + "' ";

            LPUSAMTTOT.Text = string.Format("{0:#,##0}", double.Parse(_dt.Compute("Sum(LPUSAMT)", sFilter).ToString()));
            LPELAMTTOT.Text = string.Format("{0:#,##0}", double.Parse(_dt.Compute("Sum(LPELAMT)", sFilter).ToString()));
            LPREPAIRAMTTOT.Text = string.Format("{0:#,##0}", double.Parse(_dt.Compute("Sum(LPREPAIRAMT)", sFilter).ToString()));
            LPGITAAMTTOT.Text = string.Format("{0:#,##0}", double.Parse(_dt.Compute("Sum(LPGITAAMT)", sFilter).ToString()));
            LPTOTAMTTOT.Text = string.Format("{0:#,##0}", double.Parse(_dt.Compute("Sum(LPTOTAMT)", sFilter).ToString()));

            // 202001월 부터 부팀장 -> 파트장
            if (Convert.ToDouble(_dt.Rows[0]["YYMM"].ToString().Replace("-", "")) >= 202001)
            {
                label16.Text = "파트장";
            }
            else
            {
                label16.Text = "부팀장";
            }
            // 202101월 부터 LPG -> LNG
            if (Convert.ToDouble(_dt.Rows[0]["YYMM"].ToString().Replace("-", "")) >= 202101)
            {
                LBL01_TITLE.Text = "LNG 사용 현황";
                label11.Text = "PO LNG 사용비용";
            }
            else
            {
                LBL01_TITLE.Text = "LPG 사용 현황";
                label11.Text = "PO LPG 사용비용";
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            ficount++;
        }
    }
}
