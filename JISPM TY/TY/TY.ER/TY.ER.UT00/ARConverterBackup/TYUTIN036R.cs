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
    /// Summary description for TYUTIN036R.
    /// </summary>
    public partial class TYUTIN036R : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUTIN036R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            DataTable dt = this.DataSource as DataTable;

            this.INFORDATE.Text = dt.Rows[0]["INFORDATE"].ToString().Substring(0, 4) + "년 " + dt.Rows[0]["INFORDATE"].ToString().Substring(4, 2) + "월 " + dt.Rows[0]["INFORDATE"].ToString().Substring(6, 2) + "일";
            this.NOWDATE.Text = dt.Rows[0]["NOWDATE"].ToString().Substring(0, 4) + "년 " + dt.Rows[0]["NOWDATE"].ToString().Substring(4, 2) + "월 " + dt.Rows[0]["NOWDATE"].ToString().Substring(6, 2) + "일";
        }
    }
}
