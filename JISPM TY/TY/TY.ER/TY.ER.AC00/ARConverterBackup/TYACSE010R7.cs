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
    /// Summary description for TYACSE010R7.
    /// </summary>
    public partial class TYACSE010R7 : GrapeCity.ActiveReports.SectionReport
    {
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;
        private int _iCount = 0;
        private int _totalRowCount = 0;
        private string fsGUBUNM = string.Empty;

        private DataTable dt = new DataTable();

        public TYACSE010R7()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            DATE.Text = Convert.ToString(this.Fields["DATE"].Value).Substring(0, 4) + "년  " +
                        Convert.ToString(this.Fields["DATE"].Value).Substring(4, 2) + "월  " +
                        Convert.ToString(this.Fields["DATE"].Value).Substring(6, 2) + "일  현재";

            if (Convert.ToString(this.Fields["DTDBGUBN"].Value).Trim() == "A") // 채권
            {
                TITLE.Text = "매 출 채 권 명 세 서 ";

                LBL01.Text = "외상매출금";
                LBL02.Text = "받을어음";
                LBL03.Text = "미수금";
            }
            else  // 채무
            {
                TITLE.Text = "매 입 채 무 명 세 서 ";

                LBL01.Text = "외상매입금";
                LBL02.Text = "미지급금";
                LBL03.Text = "지급어음";
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (this._rowCount == 0)
            {
                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;
            }
            else
            {
                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }

            this._iCount++;

            this.line5.Visible = false;            

            this.VNSANGHO.Font = new Font("바탕체", 8, FontStyle.Regular);
            this.TOTAMT01.Font = new Font("바탕체", 8, FontStyle.Regular);
            this.TOTAMT02.Font = new Font("바탕체", 8, FontStyle.Regular);
            this.TOTAMT03.Font = new Font("바탕체", 8, FontStyle.Regular);
            this.TOTAMT04.Font = new Font("바탕체", 8, FontStyle.Regular);

            if (this._totalRowCount == this._iCount) // 마지막 레코드
            {
                this.line5.Visible    = true;
                this.line5.LineStyle  = LineStyle.Solid;
                this.line5.LineWeight = 2;

                this.VNSANGHO.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.TOTAMT01.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.TOTAMT02.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.TOTAMT03.Font = new Font("바탕체", 9, FontStyle.Bold);
                this.TOTAMT04.Font = new Font("바탕체", 9, FontStyle.Bold);

            }
            else
            {
                fsGUBUNM = "true";

                if (this._rowCount == 20)
                {
                    this._rowCount = 0;

                    this.line5.Visible = true;
                    this.line5.X2 = 0.26f;
                    this.line5.LineStyle = LineStyle.Solid;
                    this.line5.LineWeight = 2;

                    //// 새로운 페이지에 레코드를 인쇄한 후에 페이지를 나누어라.
                    ////this.detail.NewPage = NewPage.After;
                }
                else
                {
                    this._rowCount++;
                }

                // 합계 처리
                if (dt.Rows[_iCount]["AHCRCUST"].ToString() == "999999")
                {
                    this.line5.Visible    = true;
                    this.line5.X2         = 0.26f;
                    this.line5.LineStyle  = LineStyle.Solid;
                    this.line5.LineWeight = 2;
                }
            }
        }

        private void TYACSE010R7_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                this._totalRowCount = dt.Rows.Count;  
            }
        }
    }
}