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
    /// Summary description for TYACEI012R.
    /// </summary>
    public partial class TYACEI012R : GrapeCity.ActiveReports.SectionReport
    {
        private List<int> _boldRecords = new List<int>();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _totalRowCount = 0;
       

        public TYACEI012R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (Convert.ToString(this.Fields["E6SAUP"].Value) == "")
                txtE6SAUP.Text = "전체";

            txtE6KIJUNDATE.Text = Convert.ToString(this.Fields["E6KIJUNDATE"].Value).Substring(0, 4) + "-" +
                                  Convert.ToString(this.Fields["E6KIJUNDATE"].Value).Substring(4, 2) + "-" +
                                  Convert.ToString(this.Fields["E6KIJUNDATE"].Value).Substring(6, 2);
        }

        private void detail_Format(object sender, EventArgs e)
        {

            this.line2.LineStyle = this._boldRecords.Contains(this._rowCount) || this._totalRowCount % 28 == 27 ? LineStyle.Solid : LineStyle.Dash;
            this.line2.LineWeight = this._boldRecords.Contains(this._rowCount) || this._totalRowCount % 28 == 27 ? 3 : 1;

            //// 레코드가 19이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            if (this._totalRowCount % 28 == 27)
                this.detail.NewPage = NewPage.After;
            else
                this.detail.NewPage = NewPage.None;

            this._rowCount++;
            this._totalRowCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            if (this._totalRowCount % 28 == 27)
                this.groupFooter1.NewPage = NewPage.After;
            else
                this.groupFooter1.NewPage = NewPage.None;

            this._totalRowCount++;
        }

        private void TYACEI012R_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                int iE7DTED1 = 0;
                int iE7DTED2 = 0;

                
                DataRow dr1, dr2;
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    
                    dr1 = dt.Rows[i];
                    dr2 = dt.Rows[i+1];
                    iE7DTED1 = int.TryParse(Convert.ToString(dr1["E6DTED"]), out iE7DTED1) ? iE7DTED1 : 0;
                    iE7DTED2 = int.TryParse(Convert.ToString(dr2["E6DTED"]), out iE7DTED2) ? iE7DTED2 : 0;


                    if (iE7DTED1 != iE7DTED2)
                        this._boldRecords.Add(i);                  

                }
                
                this._boldRecords.Add(dt.Rows.Count - 1);
            }

        }
    }
}
