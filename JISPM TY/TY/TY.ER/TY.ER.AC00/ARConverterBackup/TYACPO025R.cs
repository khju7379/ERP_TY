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
    /// Summary description for TYACPO025R.
    /// </summary>
    public partial class TYACPO025R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _iPageCount   = 0;
        private int _iRecordCount = 0;
        private int _iCount       = 0;

        private string fsCDACNM  = string.Empty;
        private string fsEDDESC1 = string.Empty;

        public TYACPO025R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.detail.NewPage = NewPage.None;

            this.line9.Visible = true;
            this.line3.Visible = true;

            this.EDDESC1.Visible = true;

            this.line5.X2 = 1.3F;

            this.CDACNM.Width = 0.937F;

            

            if (dt.Rows[_iCount - 1]["ESLSCDAC"].ToString() == "9999")
            {
                this.line9.Visible = false;
            }

            if (dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0,1) == "3" ||
                dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0,1) == "4" ||
                dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0,1) == "5")
            {
                this.line3.Visible = false;
                this.EDDESC1.Visible = false;

                this.CDACNM.Width = 2.07F;
            }

            if (_iCount - 1 < dt.Rows.Count)
            {
                if (_iCount > 0)
                {
                    if (_iRecordCount != _iCount)
                    {
                        if (dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0, 1) != dt.Rows[_iCount]["CDACNM"].ToString().Substring(0, 1))
                        {
                            this.line5.X2 = 0.22F;
                        }

                        if (dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0, 1) == "4")
                        {
                            if (dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0, 1) == dt.Rows[_iCount]["CDACNM"].ToString().Substring(0, 1))
                            {
                                this.line5.X2 = 2.437F;
                            }
                        }

                        if (fsCDACNM != dt.Rows[_iCount - 1]["CDACNM"].ToString())
                        {
                            fsCDACNM = dt.Rows[_iCount - 1]["CDACNM"].ToString();
                        }
                        else
                        {
                            this.CDACNM.Text = "";
                        }

                        if (dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0, 1) == "1" ||
                            dt.Rows[_iCount - 1]["CDACNM"].ToString().Substring(0, 1) == "2")
                        {
                            if (dt.Rows[_iCount - 1]["ESLSCDAC"].ToString() != "9999")
                            {
                                if (fsEDDESC1 != dt.Rows[_iCount - 1]["EDDESC1"].ToString())
                                {
                                    fsEDDESC1 = dt.Rows[_iCount - 1]["EDDESC1"].ToString();

                                    this.line5.X2 = 2.437F;
                                }
                                else
                                {
                                    if (dt.Rows[_iCount - 1]["EDDESC1"].ToString().Substring(0, 1) == dt.Rows[_iCount]["EDDESC1"].ToString().Substring(0, 1))
                                    {
                                        this.line5.X2 = 2.437F;
                                    }

                                    this.EDDESC1.Text = "";
                                }

                                
                            }
                        }
                    }
                    else
                    {
                        this.line5.X2 = 0.22F;
                    }
                }
            }
        }

        private void TYACPO025R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _iRecordCount = dt.Rows.Count;
            }
        }
    }
}