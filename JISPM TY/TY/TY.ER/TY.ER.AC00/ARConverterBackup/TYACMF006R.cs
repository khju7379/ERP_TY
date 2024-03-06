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
    /// Summary description for TYACMF006R.
    /// </summary>
    public partial class TYACMF006R : GrapeCity.ActiveReports.SectionReport
    {
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount = 0;
        private int _idistinctCount = 0;
        private int _totalRowCount = 0;

        private string _Page = "";

        private DataTable dt = new DataTable();

        //원화
        private double fdJunilJanAmt = 0;
        private double fdInAmt = 0;
        private double fdOutAmt = 0;
        //외화
        private double fdJunilJanAmt_USD = 0;
        private double fdInAmt_USD = 0;
        private double fdOutAmt_USD = 0;

        private double fdJunilJanAmt_JPY = 0;
        private double fdInAmt_JPY = 0;
        private double fdOutAmt_JPY = 0;

        private double fdJunilJanAmt_EUR = 0;
        private double fdInAmt_EUR = 0;
        private double fdOutAmt_EUR = 0;

        private double fdTotalJunilJanAmt = 0;
        private double fdTotalInAmt = 0;
        private double fdTotalOutAmt = 0;

        private double fdfusJunilJanAmt = 0;
        private double fdfusInAmt = 0;
        private double fdfusOutAmt = 0;

        private double fdfusJunilJanAmt_USD = 0;
        private double fdfusInAmt_USD = 0;
        private double fdfusOutAmt_USD = 0;

        private double fdfusJunilJanAmt_JPY = 0;
        private double fdfusInAmt_JPY = 0;
        private double fdfusOutAmt_JPY = 0;

        private double fdfusJunilJanAmt_EUR = 0;
        private double fdfusInAmt_EUR = 0;
        private double fdfusOutAmt_EUR = 0;

        private string fsA1CDAC = "";

        private float flsubTitleHeight = 0;
       

        public TYACMF006R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {          

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

          
            //레코드 인쇄
            if (dt.Rows[_iCount]["GBN"].ToString() != "5" && dt.Rows[_iCount]["GBN"].ToString() != "6")
            {
                //원화                
                txtJUNILJANAMT.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[_iCount]["WONJUNAMT"].ToString()));
                txtINAMT.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[_iCount]["WONIPAMT"].ToString()));
                txtOUTAMT.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[_iCount]["WONCHAMT"].ToString()));
                txtTOTALJAN.Text = string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[_iCount]["WONJANAMT"].ToString()));
            }
            else
            {
                //외화
                txtJUNILJANAMT.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[_iCount]["JUNAMT"].ToString()));
                txtINAMT.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[_iCount]["IPAMT"].ToString()));
                txtOUTAMT.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[_iCount]["CHAMT"].ToString()));
                txtTOTALJAN.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[_iCount]["JANAMT"].ToString()));
            }

            //외화별 소계 금액 집계
            if (dt.Rows[_iCount]["GBN"].ToString() != "5" && dt.Rows[_iCount]["GBN"].ToString() != "6")
            {
                fdJunilJanAmt += Convert.ToDouble(dt.Rows[_iCount]["WONJUNAMT"].ToString());
                fdInAmt += Convert.ToDouble(dt.Rows[_iCount]["WONIPAMT"].ToString());
                fdOutAmt += Convert.ToDouble(dt.Rows[_iCount]["WONCHAMT"].ToString());

                //단기대여금 소계
                if (dt.Rows[_iCount]["GBN"].ToString() == "1" && Convert.ToDouble(dt.Rows[_iCount]["A1CDAC"].ToString()) > 11100700 )
                {
                    fdTotalJunilJanAmt += Convert.ToDouble(dt.Rows[_iCount]["WONJUNAMT"].ToString());
                    fdTotalInAmt += Convert.ToDouble(dt.Rows[_iCount]["WONIPAMT"].ToString());
                    fdTotalOutAmt += Convert.ToDouble(dt.Rows[_iCount]["WONCHAMT"].ToString());
                }

                //가용자금 소계
                if (dt.Rows[_iCount]["A1CDAC"].ToString() == "11100100" || dt.Rows[_iCount]["A1CDAC"].ToString() == "11100200" ||
                    dt.Rows[_iCount]["A1CDAC"].ToString().Substring(0, 6) == "111003")
                {
                    fdfusJunilJanAmt += Convert.ToDouble(dt.Rows[_iCount]["WONJUNAMT"].ToString());
                    fdfusInAmt += Convert.ToDouble(dt.Rows[_iCount]["WONIPAMT"].ToString());
                    fdfusOutAmt += Convert.ToDouble(dt.Rows[_iCount]["WONCHAMT"].ToString());
                }

            }
            else
            {
                //소계
                if (dt.Rows[_iCount]["IHGUBN"].ToString() == "USD")
                {
                    fdJunilJanAmt_USD += Convert.ToDouble(dt.Rows[_iCount]["JUNAMT"].ToString());
                    fdInAmt_USD += Convert.ToDouble(dt.Rows[_iCount]["IPAMT"].ToString());
                    fdOutAmt_USD += Convert.ToDouble(dt.Rows[_iCount]["CHAMT"].ToString());
                }
                else if (dt.Rows[_iCount]["IHGUBN"].ToString() == "JPY")
                {
                    fdJunilJanAmt_JPY += Convert.ToDouble(dt.Rows[_iCount]["JUNAMT"].ToString());
                    fdInAmt_JPY += Convert.ToDouble(dt.Rows[_iCount]["IPAMT"].ToString());
                    fdOutAmt_JPY += Convert.ToDouble(dt.Rows[_iCount]["CHAMT"].ToString());
                }
                else if (dt.Rows[_iCount]["IHGUBN"].ToString() == "EUR")
                {
                    fdJunilJanAmt_EUR += Convert.ToDouble(dt.Rows[_iCount]["JUNAMT"].ToString());
                    fdInAmt_EUR += Convert.ToDouble(dt.Rows[_iCount]["IPAMT"].ToString());
                    fdOutAmt_EUR += Convert.ToDouble(dt.Rows[_iCount]["CHAMT"].ToString());
                }      
                //외화잔액합계용
                if (dt.Rows[_iCount]["IHGUBN"].ToString() == "USD")
                {
                    fdfusJunilJanAmt_USD += Convert.ToDouble(dt.Rows[_iCount]["JUNAMT"].ToString());
                    fdfusInAmt_USD += Convert.ToDouble(dt.Rows[_iCount]["IPAMT"].ToString());
                    fdfusOutAmt_USD += Convert.ToDouble(dt.Rows[_iCount]["CHAMT"].ToString());
                }
                else if (dt.Rows[_iCount]["IHGUBN"].ToString() == "JPY")
                {
                    fdfusJunilJanAmt_JPY += Convert.ToDouble(dt.Rows[_iCount]["JUNAMT"].ToString());
                    fdfusInAmt_JPY += Convert.ToDouble(dt.Rows[_iCount]["IPAMT"].ToString());
                    fdfusOutAmt_JPY += Convert.ToDouble(dt.Rows[_iCount]["CHAMT"].ToString());
                }
                else if (dt.Rows[_iCount]["IHGUBN"].ToString() == "EUR")
                {
                    fdfusJunilJanAmt_EUR += Convert.ToDouble(dt.Rows[_iCount]["JUNAMT"].ToString());
                    fdfusInAmt_EUR += Convert.ToDouble(dt.Rows[_iCount]["IPAMT"].ToString());
                    fdfusOutAmt_EUR += Convert.ToDouble(dt.Rows[_iCount]["CHAMT"].ToString());
                }      

            }

            this._iCount++;           

            if (this._totalRowCount == this._iCount)
            {
                //this.line2.LineStyle = LineStyle.Solid;
                //this.line2.LineWeight = 3;

                UP_SubTotal();

                //단기상환 소계
                txtTOTALJUNILJANAMT.Text = string.Format("{0:#,##0}", fdTotalJunilJanAmt);
                txtTOTALINAMT.Text = string.Format("{0:#,##0}", fdTotalInAmt);
                txtTOTALOUTAMT.Text = string.Format("{0:#,##0}", fdTotalOutAmt);
                
                txtHAPTOTALJAN.Text = string.Format("{0:#,##0}", fdTotalJunilJanAmt + fdTotalInAmt - fdTotalOutAmt);

                UP_Col_Distinct(_idistinctCount);
            }
            else
            {
                UP_Col_Distinct(_idistinctCount);

                this._idistinctCount++;

                if (this._rowCount == 18)
                {
                    this._rowCount = 0;

                    //this.line2.LineStyle = LineStyle.Solid;
                    //this.line2.LineWeight = 3;                    

                    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    //this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._rowCount++;                                       

                    if (dt.Rows[_iCount - 1]["A1CDAC"].ToString() != dt.Rows[_iCount]["A1CDAC"].ToString())
                    {
                        //this._rowCount++;

                        //this._rowCount = this._rowCount + 4;

                        //this.line2.LineStyle = LineStyle.Solid;
                        //this.line2.LineWeight = 3;

                        UP_SubTotal();

                        //if (Convert.ToDouble(dt.Rows[_iCount - 1]["DATE"].ToString().Trim()) >= 20130101)
                        //{
                        //    fsA1CDAC = "11100501";
                        //}
                        //else
                        //{
                        //    fsA1CDAC = "11100600";
                        //}

                        fsA1CDAC = "11100501";

                        if (dt.Rows[_iCount - 1]["A1CDAC"].ToString() == fsA1CDAC)
                        {
                            groupFooter1.Visible = true;

                            //당좌차월금액 더하기
                            fdfusJunilJanAmt += Convert.ToDouble(dt.Rows[_iCount]["FUSFUNDAMT"].ToString());
                            //가용금액 소계
                            txtFUSJUNILJANAMT.Text = string.Format("{0:#,##0}", fdfusJunilJanAmt);
                            txtFUSINAMT.Text = string.Format("{0:#,##0}", fdfusInAmt);
                            txtFUSOUTAMT.Text = string.Format("{0:#,##0}", fdfusOutAmt);
                            txtFUSTOTLJAN.Text = string.Format("{0:#,##0}", fdfusJunilJanAmt + fdfusInAmt - fdfusOutAmt);
                            //원화자금소계

                            fdfusJunilJanAmt -= Convert.ToDouble(dt.Rows[_iCount]["FUSFUNDAMT"].ToString());

                            txtWONJUNILJANAMT.Text = string.Format("{0:#,##0}", fdfusJunilJanAmt);
                            txtWONINAMT.Text = string.Format("{0:#,##0}", fdfusInAmt);
                            txtWONOUTAMT.Text = string.Format("{0:#,##0}", fdfusOutAmt);
                            txtWONTOTALJAN.Text = string.Format("{0:#,##0}", fdfusJunilJanAmt + fdfusInAmt - fdfusOutAmt);

                            //외화잔액합계
                            txtIHJUNILJANAMT_USD.Text = string.Format("{0:#,##0.00}", fdfusJunilJanAmt_USD);
                            txtIHINAMT_USD.Text = string.Format("{0:#,##0.00}", fdfusInAmt_USD);
                            txtIHOUTAMT_USD.Text = string.Format("{0:#,##0.00}", fdfusOutAmt_USD);
                            txtIHTOTALJAN_USD.Text = string.Format("{0:#,##0.00}", fdfusJunilJanAmt_USD + fdfusInAmt_USD - fdfusOutAmt_USD);

                            txtIHJUNILJANAMT_EUR.Text = string.Format("{0:#,##0.00}", fdfusJunilJanAmt_EUR);
                            txtIHINAMT_EUR.Text = string.Format("{0:#,##0.00}", fdfusInAmt_EUR);
                            txtIHOUTAMT_EUR.Text = string.Format("{0:#,##0.00}", fdfusOutAmt_EUR);
                            txtIHTOTALJAN_EUR.Text = string.Format("{0:#,##0.00}", fdfusJunilJanAmt_EUR + fdfusInAmt_EUR - fdfusOutAmt_EUR);

                            txtIHJUNILJANAMT_JPY.Text = string.Format("{0:#,##0.00}", fdfusJunilJanAmt_JPY);
                            txtIHINAMT_JPY.Text = string.Format("{0:#,##0.00}", fdfusInAmt_JPY);
                            txtIHOUTAMT_JPY.Text = string.Format("{0:#,##0.00}", fdfusOutAmt_JPY);
                            txtIHTOTALJAN_JPY.Text = string.Format("{0:#,##0.00}", fdfusJunilJanAmt_JPY + fdfusInAmt_JPY - fdfusOutAmt_JPY);                            
                        }
                        else
                        {
                            groupFooter1.Visible = false;
                        }

                        fdJunilJanAmt = 0;
                        fdInAmt = 0;
                        fdOutAmt = 0;

                        fdJunilJanAmt_USD = 0;
                        fdInAmt_USD = 0;
                        fdOutAmt_USD = 0;

                        fdJunilJanAmt_EUR = 0;
                        fdInAmt_EUR = 0;
                        fdOutAmt_EUR = 0;

                        fdJunilJanAmt_JPY = 0;
                        fdInAmt_JPY = 0;
                        fdOutAmt_JPY = 0;                    

                    }
                    else
                    {
                        this.line2.LineStyle = LineStyle.Dash;
                        this.line2.LineWeight = 1;
                    }
                }
            }

            
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {           
           
        }

        private void TYACMF006R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            groupFooter1.Visible = false;

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
                txtA1NMAC.Text = dt.Rows[iindex]["A1NMAC"].ToString();
                txtCDDESC2.Text = dt.Rows[iindex]["CDDESC2"].ToString();
            }
            else
            {
                if (dt.Rows[iindex - 1]["A1CDAC"].ToString() != dt.Rows[iindex]["A1CDAC"].ToString() )                    
                {
                    txtA1NMAC.Text = dt.Rows[iindex]["A1NMAC"].ToString();
                    txtCDDESC2.Text = dt.Rows[iindex]["CDDESC2"].ToString();
                }
                else
                {
                    txtA1NMAC.Text = "";
                    txtCDDESC2.Text = dt.Rows[iindex]["CDDESC2"].ToString();
                }
            }
        }

        private void UP_SubTotal()
        {

            txtSUBJUNILJANAMT.Text = string.Format("{0:#,##0}", fdJunilJanAmt);
            txtSUBINAMT.Text = string.Format("{0:#,##0}", fdInAmt);
            txtSUBOUTAMT.Text = string.Format("{0:#,##0}", fdOutAmt);
            txtSUBTOTALJAN.Text = string.Format("{0:#,##0}", fdJunilJanAmt + fdInAmt - fdOutAmt);

            flsubTitleHeight = flsubTitleHeight + 0.33F;

            if (fdJunilJanAmt_USD <= 0 && fdInAmt_USD <= 0 && fdOutAmt_USD <= 0)
            {
                lbl_USD.Visible = false;
                txtSUBJUNILJANAMT_USD.Visible = false;
                txtSUBINAMT_USD.Visible = false;
                txtSUBOUTAMT_USD.Visible = false;
                txtSUBTOTALJAN_USD.Visible = false;
            }
            else
            {
                flsubTitleHeight = flsubTitleHeight + 0.33F;

                lbl_USD.Visible = true;
                txtSUBJUNILJANAMT_USD.Visible = true;
                txtSUBINAMT_USD.Visible = true;
                txtSUBOUTAMT_USD.Visible = true;
                txtSUBTOTALJAN_USD.Visible = true;

                txtSUBJUNILJANAMT_USD.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt_USD);
                txtSUBINAMT_USD.Text = string.Format("{0:#,##0.00}", fdInAmt_USD);
                txtSUBOUTAMT_USD.Text = string.Format("{0:#,##0.00}", fdOutAmt_USD);
                txtSUBTOTALJAN_USD.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt_USD + fdInAmt_USD - fdOutAmt_USD);
            }

            if (fdJunilJanAmt_EUR <= 0 && fdInAmt_EUR <= 0 && fdOutAmt_EUR <= 0)
            {
                lbl_EUR.Visible = false;
                txtSUBJUNILJANAMT_EUR.Visible = false;
                txtSUBINAMT_EUR.Visible = false;
                txtSUBOUTAMT_EUR.Visible = false;
                txtSUBTOTALJAN_EUR.Visible = false;
            }
            else
            {
                flsubTitleHeight = flsubTitleHeight + 0.33F;

                lbl_EUR.Visible = true;
                txtSUBJUNILJANAMT_EUR.Visible = true;
                txtSUBINAMT_EUR.Visible = true;
                txtSUBOUTAMT_EUR.Visible = true;
                txtSUBTOTALJAN_EUR.Visible = true;

                txtSUBJUNILJANAMT_EUR.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt_EUR);
                txtSUBINAMT_EUR.Text = string.Format("{0:#,##0.00}", fdInAmt_EUR);
                txtSUBOUTAMT_EUR.Text = string.Format("{0:#,##0.00}", fdOutAmt_EUR);
                txtSUBTOTALJAN_EUR.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt_EUR + fdInAmt_EUR - fdOutAmt_EUR);
            }

            if (fdJunilJanAmt_JPY <= 0 && fdInAmt_JPY <= 0 && fdOutAmt_JPY <= 0)
            {
                lbl_JPY.Visible = false;
                txtSUBJUNILJANAMT_JPY.Visible = false;
                txtSUBINAMT_JPY.Visible = false;
                txtSUBOUTAMT_JPY.Visible = false;
                txtSUBTOTALJAN_JPY.Visible = false;
            }
            else
            {
                flsubTitleHeight = flsubTitleHeight + 0.33F;

                lbl_JPY.Visible = true;
                txtSUBJUNILJANAMT_JPY.Visible = true;
                txtSUBINAMT_JPY.Visible = true;
                txtSUBOUTAMT_JPY.Visible = true;
                txtSUBTOTALJAN_JPY.Visible = true;

                txtSUBJUNILJANAMT_JPY.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt_JPY);
                txtSUBINAMT_JPY.Text = string.Format("{0:#,##0.00}", fdInAmt_JPY);
                txtSUBOUTAMT_JPY.Text = string.Format("{0:#,##0.00}", fdOutAmt_JPY);
                txtSUBTOTALJAN_JPY.Text = string.Format("{0:#,##0.00}", fdJunilJanAmt_JPY + fdInAmt_JPY - fdOutAmt_JPY);
            }         
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            _Page = "Change";

            groupFooter2.Height = flsubTitleHeight;

            if (flsubTitleHeight <= 0.33F)
            {
                line9.Y1 = 0.314F;
                line9.Y2 = 0.314F;
            }
            else if (flsubTitleHeight <= 0.66F)
            {
                line9.Y1 = 0.634F;
                line9.Y2 = 0.634F;
            }
            else if (flsubTitleHeight <= 0.99F)
            {
                line9.Y1 = 0.964F;
                line9.Y2 = 0.964F;
            }
            else
            {
                line9.Y1 = 1.304F;
                line9.Y2 = 1.304F;
            }

            flsubTitleHeight = 0;

            this.detail.NewPage = NewPage.None;
        }
    }
}
