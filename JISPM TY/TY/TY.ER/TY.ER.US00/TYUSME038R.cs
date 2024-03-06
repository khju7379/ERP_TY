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

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSME038R.
    /// </summary>
    public partial class TYUSME038R : GrapeCity.ActiveReports.SectionReport
    {
        private int fiCount = 0;
        private int iPageCnt = 0;

        private DataTable _dt = new DataTable();

        public TYUSME038R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void TYUSME038R_DataInitialize(object sender, EventArgs e)
        {
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_dt.Rows[fiCount]["TMDATE"].ToString() == "소 계")
            {
                this.textBox3.Text = "";

                if (iPageCnt == 0)
                {
                    this.line6.Visible = false;
                }
                else
                {
                    this.line6.Visible = true;
                    this.line6.LineStyle = LineStyle.Dash;
                }

                this.line5.Visible = true;

            }
            else
            {
                if (_dt.Rows[fiCount]["TMDATE"].ToString() == "총 계")
                {
                    this.line6.Visible = false;
                    this.line5.Visible = true;
                }
                else
                {
                    if (iPageCnt == 25)
                    {
                        this.line6.Visible = false;
                        this.line5.Visible = true;
                    }
                    else
                    {
                        this.line6.Visible = false;
                        this.line5.Visible = false;
                    }
                }
            }

            fiCount++;

            if (iPageCnt == 25)
            {
                iPageCnt = 0;
            }
            else
            {
                iPageCnt++;
            }
        }
    }
}