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
    /// Summary description for TYACMF003R2.
    /// </summary>
    public partial class TYACMF003R2 : GrapeCity.ActiveReports.SectionReport
    {
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount = 0;
        private int _idistinctCount = 0;
        private int _totalRowCount = 0;
        private int _iSubTotalCount = 0;

        private string _Page = "";

        private DataTable dt = new DataTable();

        private double fdJunilJanAmt = 0;
        private double fdInAmt = 0;
        private double fdOutAmt = 0;

        private double fdTotalJunilJanAmt = 0;
        private double fdTotalInAmt = 0;
        private double fdTotalOutAmt = 0;
       

        public TYACMF003R2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (Convert.ToString(this.Fields["GUBN"].Value) == "")
                txtGUBUN.Text = "전체";

            txtB1DATE.Text = Convert.ToString(this.Fields["DATE"].Value).Substring(0, 4) + "-" +
                             Convert.ToString(this.Fields["DATE"].Value).Substring(4, 2) + "-" +
                             Convert.ToString(this.Fields["DATE"].Value).Substring(6, 2);
        }

        private void detail_Format(object sender, EventArgs e)
        {
            line5.Visible = false; 

            if (this._Page == "Change")
            {
                //line5.Visible = true;
                _Page = "";

                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                //this.detail.NewPage = NewPage.Before;
            }
            else
            {
                // 현재 페이지에 레코드를 인쇄해라.
                //this.detail.NewPage = NewPage.None;
            }

            //if (this._iCount != 0)
            //{
            //    if (dt.Rows[_iCount]["B1CDBK"].ToString() == dt.Rows[_iCount + 1]["B1CDBK"].ToString() ||
            //        dt.Rows[_iCount]["B1NOAC"].ToString() == dt.Rows[_iCount + 1]["B1NOAC"].ToString())
            //    {
            //        txtB1CDBKNM.Text = "";
            //        txtB1NOAC.Text = "";
            //    }
            //}

            fdJunilJanAmt += Convert.ToDouble(dt.Rows[_iCount]["JUNILJANAMT"].ToString());
            fdInAmt += Convert.ToDouble(dt.Rows[_iCount]["INAMT"].ToString());
            fdOutAmt += Convert.ToDouble(dt.Rows[_iCount]["OUTAMT"].ToString());

            this._iCount++;

            if (this._totalRowCount == this._iCount)
            {
                this.line2.Visible = true; 
                this.line2.LineStyle = LineStyle.Solid;
                this.line2.LineWeight = 2;

                txtSUBTOTALJAN.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt + fdInAmt - fdOutAmt);

                fdTotalJunilJanAmt += fdJunilJanAmt;
                fdTotalInAmt += fdInAmt;
                fdTotalOutAmt += fdOutAmt;

                txtTOTALJAN.Text = string.Format("{0:#,##0.00}", fdTotalJunilJanAmt + fdTotalInAmt - fdTotalOutAmt);

                UP_Col_Distinct(_idistinctCount);
            }
            else
            {
                UP_Col_Distinct(_idistinctCount);

                this._idistinctCount++;

                if (this._rowCount == 29)
                {
                    this._rowCount = 0;

                    this.line2.Visible = true; 
                    this.line2.LineStyle = LineStyle.Solid;
                    this.line2.LineWeight = 2;

                    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    //this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._rowCount++;                                       

                    if (dt.Rows[_iCount - 1]["H1CDAC"].ToString() != dt.Rows[_iCount]["H1CDAC"].ToString())
                    {
                        this._rowCount++;

                        this.line2.Visible = true; 
                        this.line2.LineStyle = LineStyle.Solid;
                        this.line2.LineWeight = 2;

                        txtSUBTOTALJAN.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt + fdInAmt - fdOutAmt);

                        fdTotalJunilJanAmt += fdJunilJanAmt; 
                        fdTotalInAmt +=  fdInAmt;
                        fdTotalOutAmt += fdOutAmt;

                        fdJunilJanAmt = 0;
                        fdInAmt = 0;
                        fdOutAmt = 0;
                    }
                    else
                    {
                        this.line2.Visible = false; 
                        this.line2.LineStyle = LineStyle.Dash;
                        this.line2.LineWeight = 1;
                    }
                }
            }

            
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {           
            _Page  = "Change";
            //this._rowCount = 0;

            this.detail.NewPage = NewPage.None;

            if (dt.Rows[_iCount - 1]["H1CDAC"].ToString() == "11100309")
            {
                lblSubTotal.Text = "외화당좌예금 소계";
            }
            else
            {
                lblSubTotal.Text = "외화보통예금 소계";
            }

            //if (_iSubTotalCount == 0)
            //{
            //    lblSubTotal.Text = "외화당좌예금 소계";
            //}
            //else
            //{
            //    lblSubTotal.Text = "외화보통예금 소계";
            //}

            _iSubTotalCount++;
        }

        private void TYACMF003R2_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                this._totalRowCount = dt.Rows.Count;  
            }

        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            
        }

        private void UP_Col_Distinct(int iindex)
        {
            if (iindex == 0)
            {
                txtH1EXGB.Text = dt.Rows[iindex]["H1EXGB"].ToString();
                txtH1NMBK.Text = dt.Rows[iindex]["H1NMBK"].ToString();
                txtH1NOAC.Text = dt.Rows[iindex]["H1NOAC"].ToString();
            }
            else
            {
                if (dt.Rows[iindex - 1]["H1EXGB"].ToString() != dt.Rows[iindex]["H1EXGB"].ToString() || 
                    dt.Rows[iindex - 1]["H1CDBK"].ToString() != dt.Rows[iindex]["H1CDBK"].ToString() ||
                    dt.Rows[iindex - 1]["H1NOAC"].ToString() != dt.Rows[iindex]["H1NOAC"].ToString())
                {
                    txtH1EXGB.Text = dt.Rows[iindex]["H1EXGB"].ToString();
                    txtH1NMBK.Text = dt.Rows[iindex]["H1NMBK"].ToString();
                    txtH1NOAC.Text = dt.Rows[iindex]["H1NOAC"].ToString();
                }
                else
                {
                    txtH1EXGB.Text = "";
                    txtH1NMBK.Text = "";
                    txtH1NOAC.Text = "";
                }
            }
        }

    }
}
