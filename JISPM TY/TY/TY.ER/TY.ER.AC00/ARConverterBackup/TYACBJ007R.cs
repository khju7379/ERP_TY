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
    /// Summary description for TYACBJ007R.
    /// </summary>
    public partial class TYACBJ007R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private List<int> _boldRecords = new List<int>();
        
        // 한 페이지에 찍을 레코드 카운트 변수
        private int _rowCount = 0;

        private int _fiCount  = 0;
        private int _iCount   = 0;
        private double fdHeight = 0;

        private string sGUBUN   = string.Empty;
        private string sGroup   = string.Empty;
        private string sNEWCDAC = string.Empty;
        private string sOLDCDAC = string.Empty;

        public TYACBJ007R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._iCount++;

            this.line3.Visible = false;
            this.line5.Visible = false;
            this.line7.Visible = false;

            this.line10.Visible = true;
            
            if (this.sGroup == "Change")
            {
                sGroup = "";

                fdHeight = 0;

                // 새로운 페이지에 레코드를 인쇄하기 이전에 페이지를 나누어라.
                this.detail.NewPage = NewPage.Before;
            }
            else
            {
                this.groupFooter1.Visible = false;
                
                // 현재 페이지에 레코드를 인쇄해라.
                this.detail.NewPage = NewPage.None;
            }

            if (dt.Rows[_iCount - 1]["YYMM"].ToString() == "전기이월" ||
                dt.Rows[_iCount - 1]["YYMM"].ToString() == "전월이월" ||
                dt.Rows[_iCount - 1]["YYMM"].ToString() == "월     계" ||
                dt.Rows[_iCount - 1]["YYMM"].ToString() == "누     계")
            {
                if (dt.Rows[_iCount - 1]["YYMM"].ToString() == "월     계")
                {
                    this.line3.Visible = true;
                    this.line5.Visible = true;
                    if (fdHeight == 0.36 && sGUBUN == "First")
                    {
                        this.line5.Visible = false;
                    }
                    else
                    {
                        sGUBUN = "";

                        this.line5.Visible = true;
                    }
                }
                else if (dt.Rows[_iCount - 1]["YYMM"].ToString() == "누     계")
                {
                    if (fdHeight == 0.36 && sGUBUN == "First")
                    {
                        this.line5.Visible = false;
                    }
                    else
                    {
                        sGUBUN = "";

                        this.line5.Visible = false;
                    }
                }

                this.B4RKCU.Visible  = false;
                this.B4NOJP.Visible  = false;
                this.B4VLMI2.Visible = false;
                this.B4VLMI3.Visible = false;
                this.B4DPAC.Visible  = false;
                this.E6DTED.Visible  = false;

                this.detail.Height = 0.18F;

                fdHeight = fdHeight + 0.18;
            }
            else
            {
                sGUBUN = "First";

                this.B4RKCU.Visible  = true;
                this.B4NOJP.Visible  = true;
                this.B4VLMI2.Visible = true;
                this.B4VLMI3.Visible = true;
                this.B4DPAC.Visible  = true;
                this.E6DTED.Visible  = true;

                this.detail.Height = 0.36F;

                fdHeight = fdHeight + 0.36;
            }

            if (fdHeight >= 5.54)
            {
                sGUBUN = "";

                fdHeight = 0;

                this.pageFooter.Visible = true;

                this.line9.Visible = true;

                this.line9.LineStyle = LineStyle.Solid;
                this.line9.LineWeight = 3;
                
                this.line5.Visible = false;

                this.line10.Visible = false;

                this.detail.NewPage = NewPage.After;
            }

            if (_fiCount == _iCount)
            {
                this.pageFooter.Visible = false;
            }
            else
            {
                if (dt.Rows[_iCount]["YYMM"].ToString() == "월     계")
                {
                    this.line10.Visible = false;
                }
            }
        }

        private void TYACBJ007R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            this.pageFooter.Visible = false;

            this.groupFooter1.Visible = true;

            sGroup = "Change";

            this.line7.Visible = true;

            this.line7.LineStyle = LineStyle.Solid;
            this.line7.LineWeight = 3;
        }
    }
}