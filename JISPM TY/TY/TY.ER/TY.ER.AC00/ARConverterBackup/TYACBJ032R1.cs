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
    /// Summary description for TYACBJ032R1.
    /// </summary>
    public partial class TYACBJ032R1 : GrapeCity.ActiveReports.SectionReport
    {
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount = 0;
        private int _idistinctCount = 0;
        private int _totalRowCount = 0;

        private string _Page = "";

        private DataTable dt = new DataTable();

        //private double fdSubB4AMDR  = 0;
        private double fdTotalB4AMDR = 0;

        private float _dHeight = 0;
       
        public TYACBJ032R1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {          

            txtSDATE.Text = Convert.ToString(this.Fields["SDATE"].Value).Substring(0, 4) + "-" +
                             Convert.ToString(this.Fields["SDATE"].Value).Substring(4, 2) + "-" +
                             Convert.ToString(this.Fields["SDATE"].Value).Substring(6, 2);

            txtEDATE.Text = Convert.ToString(this.Fields["EDATE"].Value).Substring(0, 4) + "-" +
                             Convert.ToString(this.Fields["EDATE"].Value).Substring(4, 2) + "-" +
                             Convert.ToString(this.Fields["EDATE"].Value).Substring(6, 2);
            
        }

        private void detail_Format(object sender, EventArgs e)
        {
            
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

            //fdSubB4AMDR += Convert.ToDouble(dt.Rows[_iCount]["B4AMDR"].ToString());

            this._iCount++;           

            if (this._totalRowCount == this._iCount)
            {
                //this.line2.LineStyle = LineStyle.Solid;
                //this.line2.LineWeight = 3;

                UP_Col_Distinct(_idistinctCount);
            }
            else
            {
                UP_Col_Distinct(_idistinctCount);

                this._idistinctCount++;

                if (this._dHeight == 62.0f)
                {
                    this._rowCount = 0;

                    _dHeight = 0;

                    this.line2.LineStyle = LineStyle.Solid;
                    this.line2.LineWeight = 2;                    

                    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    //this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._dHeight = float.Parse(string.Format("{0:#,###}", Convert.ToString(_dHeight + 2.0f)));

                    this._rowCount++;
                   
                    this.line2.LineStyle = LineStyle.Dash;
                    this.line2.LineWeight = 1;
                   
                }
            }

            
        }
       

        private void TYACBJ032R1_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;            

            if (dt != null)
            {
                this._totalRowCount = dt.Rows.Count;  
            }
        }

        private void UP_Col_Distinct(int iindex)
        {
            if (iindex == 0)
            {
                txtB4VLMI2NM.Text = dt.Rows[iindex]["B4VLMI2NM"].ToString();
            }
            else
            {
                if (dt.Rows[iindex - 1]["B4VLMI2"].ToString() != dt.Rows[iindex]["B4VLMI2"].ToString())                    
                {
                    txtB4VLMI2NM.Text = dt.Rows[iindex]["B4VLMI2NM"].ToString();
                }
                else
                {
                    txtB4VLMI2NM.Text = "";
                }
            }
        }
       

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            this._dHeight = float.Parse(string.Format("{0:#,###}", Convert.ToString(_dHeight + 2.0f)));

            _Page = "Change";            

            this.detail.NewPage = NewPage.None;

            this._rowCount++;
        }
    }
}
