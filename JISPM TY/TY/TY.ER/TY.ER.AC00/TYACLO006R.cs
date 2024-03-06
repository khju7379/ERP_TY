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
    /// Summary description for TYACLO006R.
    /// </summary>
    public partial class TYACLO006R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private string fsDATE = string.Empty;

        public TYACLO006R(string sDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsDATE = sDATE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
            this.DATE.Text = "(" + fsDATE.Substring(0, 4) + "-" + fsDATE.Substring(4, 2) + ")";
            this.TITLE1.Text = (Convert.ToInt32(fsDATE.Substring(0, 4)) - 1).ToString() + "년";
            this.TITLE2.Text = fsDATE.Substring(0, 4) + "년 증(감)";
            this.TITLE3.Text = fsDATE.Substring(0, 4) + "년 " + fsDATE.Substring(4, 2) + "월 잔액";
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (fiCount + 1 < _dt.Rows.Count)
            {
                if (fiCount > 0)
                {
                    if (_dt.Rows[fiCount]["BJDESC1"].ToString() == _dt.Rows[fiCount - 1]["BJDESC1"].ToString())
                    {
                        this.BJDESC1.Visible = false;
                    }
                    else
                    {
                        this.BJDESC1.Visible = true;
                    }
                }

                if ((_dt.Rows[fiCount]["BJDESC1"].ToString()) !=
                        (_dt.Rows[fiCount + 1]["BJDESC1"].ToString()))
                {
                    underline.X1 = 0F;
                }
                else
                {
                    underline.X1 = 1.185F;
                }

            }
            else
            {
                this.BJDESC1.Visible = true;
            }

            if (_dt.Rows[fiCount]["LOCGRBK"].ToString() == "9999999")
            {
                this.LOCLOANTYPENM.Visible = false;
            }
            else
            {
                this.LOCLOANTYPENM.Visible = true;
            }

            fiCount++;
        }
    }
}
