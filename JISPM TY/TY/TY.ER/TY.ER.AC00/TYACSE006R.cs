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
    /// Summary description for TYACSE006R.
    /// </summary>
    public partial class TYACSE006R : GrapeCity.ActiveReports.SectionReport
    {

        public TYACSE006R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = this.DataSource as DataTable;
                string sDate = dt.Rows[0]["AOCRYYMM"].ToString().Substring(0, 4) + "/" + dt.Rows[0]["AOCRYYMM"].ToString().Substring(4, 2) + "/01";
                //DateTime dDate = Convert.ToDateTime(sDate).AddDays(-1).AddMonths(1);// 마지막 전일 구하기
                DateTime dDate = Convert.ToDateTime(sDate).AddMonths(1).AddDays(-1);  // 마지막 일자 구하기

                sDate = dDate.ToString().Substring(0, 4) + "년 " + dDate.ToString().Substring(5, 2) + "월 " + dDate.ToString().Substring(8, 2) + "일";
                YYMMDD01.Text = sDate;
                YYMMDD02.Text = sDate;
                YYMMDD03.Text = sDate;
                DATE.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

                DataTable dt2 = new DataTable();

                dt2.Columns.Add("NAME", typeof(System.String));
                dt2.Columns.Add("AOASUMAMT", typeof(System.String));

                DataRow row;


                // ------   채권  ------
                //외상 매출금
                if (Convert.ToDouble(dt.Rows[0]["AOA1SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "외상 매출금";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOA1SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }
                //선급금
                if (Convert.ToDouble(dt.Rows[0]["AOA5SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "선급금";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOA5SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }
                //받을어음
                if (Convert.ToDouble(dt.Rows[0]["AOA4SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "받을어음";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOA4SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }
                //미수금
                if (Convert.ToDouble(dt.Rows[0]["AOA6SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "미수금";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOA6SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }
                int iCount = 4 - dt2.Rows.Count;
                int i;
                for (i = 0; i < iCount; i++)
                {
                    row = dt2.NewRow();
                    row["NAME"] = DBNull.Value;
                    row["AOASUMAMT"] = DBNull.Value;
                    dt2.Rows.Add(row);
                }

                // ------   채무  ------
                //외상매입금
                if (Convert.ToDouble(dt.Rows[0]["AOB1SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "외상매입금";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOB1SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }
                //미지급금
                if (Convert.ToDouble(dt.Rows[0]["AOB3SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "미지급금";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOB3SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }
                //지급어음
                if (Convert.ToDouble(dt.Rows[0]["AOB5SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "지급어음";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOB5SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }
                //선수금
                if (Convert.ToDouble(dt.Rows[0]["AOB4SUMAMT"].ToString()) > 0)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "선수금";
                    row["AOASUMAMT"] = String.Format("{0:#,##0}", dt.Rows[0]["AOB4SUMAMT"].ToString());
                    dt2.Rows.Add(row);
                }

                iCount = 8 - dt2.Rows.Count;
                for (i = 0; i < iCount; i++)
                {
                    row = dt2.NewRow();
                    row["NAME"] = "";
                    row["AOASUMAMT"] = "";
                    dt2.Rows.Add(row);
                }

                // ------   출력 Seting  ------

                this.NAME1.Text = dt2.Rows[0]["NAME"].ToString();
                this.NAME2.Text = dt2.Rows[1]["NAME"].ToString();
                this.NAME3.Text = dt2.Rows[2]["NAME"].ToString();
                this.NAME4.Text = dt2.Rows[3]["NAME"].ToString();
                this.NAME5.Text = dt2.Rows[4]["NAME"].ToString();
                this.NAME6.Text = dt2.Rows[5]["NAME"].ToString();
                this.NAME7.Text = dt2.Rows[6]["NAME"].ToString();
                this.NAME8.Text = dt2.Rows[7]["NAME"].ToString();

                if (dt2.Rows[0]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT1.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[0]["AOASUMAMT"].ToString()));
                }
                if (dt2.Rows[1]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT2.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[1]["AOASUMAMT"].ToString()));
                }
                if (dt2.Rows[2]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT3.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[2]["AOASUMAMT"].ToString()));
                }
                if (dt2.Rows[3]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT4.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[3]["AOASUMAMT"].ToString()));
                }
                if (dt2.Rows[4]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT5.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[4]["AOASUMAMT"].ToString()));
                }
                if (dt2.Rows[5]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT6.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[5]["AOASUMAMT"].ToString()));
                }
                if (dt2.Rows[6]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT7.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[6]["AOASUMAMT"].ToString()));
                }
                if (dt2.Rows[7]["AOASUMAMT"].ToString() != "")
                {
                    this.AOASUMAMT8.Text = String.Format("{0:#,##0}", Convert.ToDouble(dt2.Rows[7]["AOASUMAMT"].ToString()));
                }

            }
            catch
            {
            }
        }
    }
}
