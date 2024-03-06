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
    /// Summary description for TYACBJ027R.
    /// </summary>
    public partial class TYACBJ027R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();

        public TYACBJ027R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYACBJ027R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
            if (dt != null)
            {
                string B4CMAC;
                int record;

                foreach (DataRow dr in dt.Rows)
                {
                    B4CMAC = Convert.ToString(dr["B4CMAC"]);
                    record = int.TryParse(Convert.ToString(dr["RECORD"]), out record) ? record : 0;

                    if (B4CMAC == "당일합계" || B4CMAC == "누 계" || B4CMAC == "합 계")
                    {
                        if (!this._boldRecords.Contains(record - 1))
                            this._boldRecords.Add(record - 1);
                        if (!this._boldRecords.Contains(record))
                            this._boldRecords.Add(record);
                    }
                }
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            int RECORD = int.TryParse(Convert.ToString(this.Fields["RECORD"].Value), out RECORD) ? RECORD : 0;

            this.line2.LineStyle = this._boldRecords.Contains(RECORD) ? LineStyle.Solid : LineStyle.Dash;
            this.line2.LineWeight = this._boldRecords.Contains(RECORD) ? 3 : 1;
        }
    }
}