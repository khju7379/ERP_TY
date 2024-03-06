using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME013R.
    /// </summary>
    public partial class TYUTME013R : GrapeCity.ActiveReports.SectionReport
    {
        private int fiCount = 0;
        private DataTable _dt = new DataTable();

        public TYUTME013R()
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

        private void detail_Format(object sender, EventArgs e)
        {
            string sJOBDAT = _dt.Rows[fiCount]["JOBDAT"].ToString();
            string sJBJBDAT = _dt.Rows[fiCount]["JBJBDAT"].ToString();
            string sJBIANDAT = _dt.Rows[fiCount]["JBIANDAT"].ToString();
            double dDANGA = 0;
            double dMOK = Convert.ToDouble(_dt.Rows[fiCount]["MOK"].ToString());
            double dAMT1 = 0;
            double dWK_AMOUNT1 = 0;

            this.JOBDAY1.Text = sJOBDAT.Substring(6, 2);
            this.JOBDAY2.Text = UP_GetDay(sJOBDAT.Substring(6, 2));
            this.JOBMONTH.Text = UP_GetMonth(sJOBDAT.Substring(4, 2));
            this.JOBYEAR.Text = sJOBDAT.Substring(0, 4);

            this.JBJBDAY1.Text = sJBJBDAT.Substring(6, 2);
            this.JBJBDAY2.Text = UP_GetDay(sJBJBDAT.Substring(6, 2));
            this.JBJBMONTH.Text = UP_GetMonth(sJBJBDAT.Substring(4, 2));
            this.JBJBYEAR.Text = sJBJBDAT.Substring(0, 4);

            this.JBIANDAY1.Text = sJBIANDAT.Substring(6, 2);
            this.JBIANDAY2.Text = UP_GetDay(sJBIANDAT.Substring(6, 2));
            this.JBIANMONTH.Text = UP_GetMonth(sJBIANDAT.Substring(4, 2));
            this.JBIANYEAR.Text = sJBIANDAT.Substring(0, 4);

            if (_dt.Rows[fiCount]["JBVSGB"].ToString() == "1")
            {
                // 외항단가
                dDANGA = double.Parse(_dt.Rows[0]["JBMOUTAMT"].ToString());
            }
            else
            {
                // 내항단가
                dDANGA = double.Parse(_dt.Rows[0]["JBMINAMT"].ToString());
            }

            this.DANGA.Text = string.Format("{0:#,###}", dDANGA);
            dAMT1 = ((dDANGA * dMOK) / 10) * 10;
            this.AMT1.Text = string.Format("{0:#,###}", (dAMT1));

            // 할증
            if (Convert.ToDouble(_dt.Rows[fiCount]["WK_TOTTIME"].ToString()) > 12)
            {
                this.DCTIT1.Text = "((";
                this.DCTIME.Text = string.Format("{0:#,###.0}", Convert.ToDouble(_dt.Rows[fiCount]["WK_TOTTIME"].ToString()));
                this.DCTIT2.Text = "Hrs -  12 Hrs)  x";

                string sDCDANGA = string.Empty;

                // 외항선
                if (_dt.Rows[fiCount]["JBVSGB"].ToString() == "1")
                {
                    // 외항할증단가
                    sDCDANGA = string.Format("{0:#,##0.0}", Convert.ToDouble(_dt.Rows[0]["JBMHALOUTAMT"].ToString()));
                }
                else // 내항선
                {
                    // 내항할증단가
                    sDCDANGA = string.Format("{0:#,##0.0}", Convert.ToDouble(_dt.Rows[0]["JBMHALINAMT"].ToString()));
                }

                this.DCDANGA.Text = sDCDANGA + " )   x ";
                this.DCTON.Text = string.Format("{0:#,###}", Convert.ToDouble(_dt.Rows[fiCount]["MOK"].ToString()));
                this.DCTIT4.Text = "Tons = \\";
                dWK_AMOUNT1 = (((Convert.ToDouble(_dt.Rows[fiCount]["WK_TOTTIME"].ToString())) - 12) * Convert.ToDouble(sDCDANGA)) * Convert.ToDouble(_dt.Rows[fiCount]["MOK"].ToString());
                this.WK_AMOUNT1.Text = string.Format("{0:#,###}", Math.Truncate(dWK_AMOUNT1));
            }
            else
            {
                this.DCTIT1.Text = "";
                this.DCTIME.Text = "";
                this.DCTIT2.Text = "";
                this.DCDANGA.Text = "";
                this.DCTON.Text = "";
                this.DCTIT4.Text = "";
                this.WK_AMOUNT1.Text = "";
            }


            if (_dt.Rows[fiCount]["JBSUNJU"].ToString() == "Y")
            {
                this.PRHALP.Text = _dt.Rows[0]["JBMSUNHALYUL"].ToString();
                this.DEDU1.Text = "";
                this.AMT2.Text = string.Format("{0:#,###}", dAMT1 + dWK_AMOUNT1);
                //this.DEDU2.Text = "x   0.85 = ";
                this.DEDU2.Text = "x   " + string.Format("{0:#,###.##}", (1 - Convert.ToDouble(_dt.Rows[0]["JBMSUNHALYUL"].ToString()))) + " = ";
                this.AMT3.Text = string.Format("{0:#,###}", Convert.ToDouble(_dt.Rows[fiCount]["JBAMT"].ToString()));
            }
            else
            {
                if (_dt.Rows[fiCount]["JBHALIN"].ToString() == "Y")
                {
                    this.PRHALP.Text = _dt.Rows[0]["JBMHALYUL"].ToString();
                    this.DEDU1.Text = "";
                    this.AMT2.Text = string.Format("{0:#,###}", dAMT1 + dWK_AMOUNT1);
                    //this.DEDU2.Text = "x   0.9 = ";
                    this.DEDU2.Text = "x   " + string.Format("{0:#,##0.##}", (Convert.ToDouble(_dt.Rows[0]["JBMHALYUL"].ToString())) / 100) + " = ";
                    this.AMT3.Text = string.Format("{0:#,###}", Convert.ToDouble(_dt.Rows[fiCount]["JBAMT"].ToString()));
                }
                else
                {
                    this.PRHALP.Text = "";
                    this.DEDU1.Text = "";
                    this.AMT2.Text = "";
                    this.DEDU2.Text = "";
                    this.AMT3.Text = "";
                }
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["JBRATE"].ToString()) > 0)
            {
                this.JBRATE.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(_dt.Rows[fiCount]["JBRATE"].ToString()));
                this.JBRATE.Visible = true;
                this.textBox31.Visible = true;
                this.textBox32.Visible = true;
            }
            else
            {
                this.JBRATE.Visible = false;
                this.textBox31.Visible = false;
                this.textBox32.Visible = false;
            }

            if (Convert.ToDouble(_dt.Rows[fiCount]["JBDOLLAR"].ToString()) > 0)
            {
                this.JBDOLLAR.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(_dt.Rows[fiCount]["JBDOLLAR"].ToString()));
                this.JBDOLLAR.Visible = true;
                this.textBox35.Visible = true;
            }
            else
            {
                this.JBDOLLAR.Visible = false;
                this.textBox35.Visible = false;
            }

            fiCount++;

            this.detail.NewPage = NewPage.BeforeAfter;
        }

        private string UP_GetDay(string sDay)
        {
            string sRtn = string.Empty;

            if (sDay == "01" || sDay == "21" || sDay == "31")
            {
                sRtn = "St";
            }
            else if (sDay == "02" || sDay == "22")
            {
                sRtn = "Nd";
            }
            else if (sDay == "03" || sDay == "23")
            {
                sRtn = "Rd";
            }
            else
            {
                sRtn = "Th";
            }

            return sRtn;
        }

        private string UP_GetMonth(string sMonth)
        {
            string sRtn = string.Empty;

            if (sMonth == "01")
            {
                sRtn = "Jan.";
            }
            else if (sMonth == "02")
            {
                sRtn = "Feb.";
            }
            else if (sMonth == "03")
            {
                sRtn = "Mar.";
            }
            else if (sMonth == "04")
            {
                sRtn = "Apr.";
            }
            else if (sMonth == "05")
            {
                sRtn = "May.";
            }
            else if (sMonth == "06")
            {
                sRtn = "Jun.";
            }
            else if (sMonth == "07")
            {
                sRtn = "Jul.";
            }
            else if (sMonth == "08")
            {
                sRtn = "Aug.";
            }
            else if (sMonth == "09")
            {
                sRtn = "Sep.";
            }
            else if (sMonth == "10")
            {
                sRtn = "Oct.";
            }
            else if (sMonth == "11")
            {
                sRtn = "Nov.";
            }
            else if (sMonth == "12")
            {
                sRtn = "Dec.";
            }

            return sRtn;
        }


    }
}
