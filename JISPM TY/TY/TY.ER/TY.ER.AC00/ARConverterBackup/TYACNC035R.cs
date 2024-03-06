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
    /// Summary description for TYACNC035R.
    /// </summary>
    public partial class TYACNC035R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount   = 0;
        private int _idistinctCount = 0;
        private int _totalRowCount = 0;

        private string sGUBUN   = string.Empty;
        private string fsGroup  = string.Empty;
        private string sNEWCDAC = string.Empty;
        private string sOLDCDAC = string.Empty;

        // 중복 제거
        private string _THNM = string.Empty;

        public TYACNC035R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.line25.Visible = true;
            this.shape1.Visible = false;

        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.DATE.Text = "(" + dt.Rows[0]["DATE"].ToString().Substring(0, 4) + "년 " + dt.Rows[0]["DATE"].ToString().Substring(4, 2) + "월 말)";
        }

        private void detail_Format(object sender, EventArgs e)
        {

            this._iCount++;

            //// 레코드가 21이면 레코드를 0으로 바꾸거나 0이 아니면 레코드카운트 + 1
            //if (this._rowCount == 35)
            //{
            //    this._rowCount = 0;

            //    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
            //    this.detail.NewPage = NewPage.After;
            //}
            //else
            //{
            //    this._rowCount++;

            //    // 현재 페이지에 레코드를 인쇄해라.
            //    this.detail.NewPage = NewPage.None;
            //}


            if (this._totalRowCount == this._iCount - 1)
            {
                UP_Col_Distinct(_idistinctCount);
            }
            else
            {
                UP_Col_Distinct(_idistinctCount);

                this._idistinctCount++;

                if (this._rowCount == 35)
                {
                    this._rowCount = 0;

                    // 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    this.detail.NewPage = NewPage.After;
                }
                else
                {

                    this._rowCount++;

                    // 현재 페이지에 레코드를 인쇄해라.
                    this.detail.NewPage = NewPage.None;

                }
            }

        }

        private void TYACNC035R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _totalRowCount = dt.Rows.Count;
            }
        }

        #region Description : 중복제거
        private void UP_Col_Distinct(int iindex)
        {
            if (iindex == 0)
            {
                THNM.Text = dt.Rows[iindex]["THNM"].ToString();

                this.line25.X2 = 1.747f;
                this.line25.X1 = 11.43f;

            }
            else
            {
                if (dt.Rows[iindex]["AJDCDAC"].ToString() == "")
                {
                    this.line25.X2 = 0.57f;
                    this.line25.X1 = 11.43f;
                    this.shape1.Visible = true;

                    //if (dt.Rows[iindex]["THNM"].ToString().Trim() == "투하자금 합계")
                    //{
                    //    //긴것
                    //    this.shape1.Height = 0.181F;
                    //    this.shape1.Left = 0.58F;
                    //    this.shape1.Top = 0F;
                    //    this.shape1.Width = 10.87F;
                    //}
                    //else
                    //{
                    //    //짤은거
                    //    this.shape1.Height = 0.181F;
                    //    this.shape1.Left = 1.747F;
                    //    this.shape1.Top = 0.01100001F;
                    //    this.shape1.Width = 9.683001F;
                    //}
                }
                else
                {
                    this.line25.X2 = 1.747f;
                    this.line25.X1 = 11.43f;
                    this.shape1.Visible = false;
                }

                if (dt.Rows[iindex]["THNM"].ToString() != dt.Rows[iindex - 1]["THNM"].ToString())
                {
                    THNM.Text = dt.Rows[iindex]["THNM"].ToString();
                }
                else
                {
                    THNM.Text = "";
                }

                if (dt.Rows[iindex]["THNM"].ToString().Trim() == "투하자금 합계")
                {
                    this.line15.Visible = false;
                    // 긴것
                    this.shape1.Height = 0.181F;
                    this.shape1.Left = 0.58F;
                    this.shape1.Top = 0F;
                    this.shape1.Width = 10.87F;
                }
                else
                {
                    this.line15.Visible = true;
                }
            }
        } 
        #endregion


    }
}