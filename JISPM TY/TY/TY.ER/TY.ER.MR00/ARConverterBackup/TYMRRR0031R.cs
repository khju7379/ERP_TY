using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using TY.ER.MR00;

namespace TY.ER.MR00
{
    /// <summary>
    /// Summary description for TYMRRR003R.
    /// </summary>
    public partial class TYMRRR0031R : DataDynamics.ActiveReports.ActiveReport
    {

        private DataTable Retdt = new DataTable();

        private int _rowCount = 0;
        private int _iRecordCount = 0;
        private int _iCount = 0;
        private int _pageCount = 1;         //페이지 카운터
        private string sSTRRM1100 = "";
        private string sEDRRM1100 = "";

        public TYMRRR0031R(string STDATE, string EDDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            sSTRRM1100 = STDATE.ToString();
            sEDRRM1100 = EDDATE.ToString();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            // 첫번째 페이지인 경우
            if (this._pageCount == 1)
            {
                if (this._rowCount == 21)
                {
                    this._rowCount = 0;

                    this.detail.NewPage = NewPage.After;
                    this._pageCount++;
                }
                else
                {
                    // 현재의 행이 마지막 행이 아닌경우 실행
                    if (_iCount < _iRecordCount)
                    {
                        // 현재 행의 상호명과 다음 행의 상호명이 다른경우 실행
                        if (Retdt.Rows[_iCount - 1]["RRN1100"].ToString() != Retdt.Rows[_iCount]["RRN1100"].ToString())
                        {
                            this.detail.NewPage = NewPage.After;  //새로운 페이지 생성
                            this._rowCount = 0;
                            this._pageCount++;
                        }
                        else
                        {
                            this._rowCount++;

                            this.detail.NewPage = NewPage.None;
                        }
                    }
                }
            }
            // 첫번째 페이지가 아닌경우
            else
            {
                if (this._rowCount == 24)
                {
                    this._rowCount = 0;

                    this.detail.NewPage = NewPage.After;
                }
                else
                {
                    // 현재의 행이 마지막 행이 아닌경우 실행
                    if (_iCount < _iRecordCount)
                    {
                        // 현재 행의 상호명과 다음 행의 상호명이 다른경우 실행
                        if (Retdt.Rows[_iCount - 1]["RRN1100"].ToString() != Retdt.Rows[_iCount]["RRN1100"].ToString())
                        {
                            this.detail.NewPage = NewPage.After; //새로운 페이지 생성
                            this._rowCount = 0;
                        }
                        else
                        {
                            this._rowCount++;

                            this.detail.NewPage = NewPage.None;
                        }
                    }
                }
            }

        }
        private void TYMRRR003R_DataInitialize(object sender, EventArgs e)
        {
            Retdt = (DataTable)this.DataSource;

            if (Retdt != null)
            {
                _iRecordCount = Retdt.Rows.Count;
            }
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            this.STRRM1100.Text = sSTRRM1100.Substring(0, 4) + "-" + sSTRRM1100.Substring(4, 2) + "-" + sSTRRM1100.Substring(6, 2);
            this.EDRRM1100.Text = sEDRRM1100.Substring(0, 4) + "-" + sEDRRM1100.Substring(4, 2) + "-" + sEDRRM1100.Substring(6, 2);
        }
    }
}
