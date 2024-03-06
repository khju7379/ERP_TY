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
    /// Summary description for TYACLO003R2.
    /// </summary>
    public partial class TYACLO009R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private string fsDATE = string.Empty;

        public TYACLO009R(string sDTAE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsDATE = sDTAE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
            this.DATE.Text = fsDATE;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (fiCount + 1 < _dt.Rows.Count)
            {
                if (fiCount > 0)
                {
                    if (_dt.Rows[fiCount]["LOCGIGANTYPE"].ToString() == _dt.Rows[fiCount - 1]["LOCGIGANTYPE"].ToString())
                    {
                        this.LOCGIGANTYPENM.Visible = false;
                    }
                    else
                    {
                        this.LOCGIGANTYPENM.Visible = true;
                    }

                    if (_dt.Rows[fiCount]["LOCLOAN"].ToString() == _dt.Rows[fiCount - 1]["LOCLOAN"].ToString())
                    {
                        this.USDESC.Visible = false;
                    }
                    else
                    {
                        this.USDESC.Visible = true;
                    }

                    if (_dt.Rows[fiCount]["LOCLOANTYPE"].ToString() == _dt.Rows[fiCount - 1]["LOCLOANTYPE"].ToString())
                    {
                        this.TYDESC.Visible = false;
                    }
                    else
                    {
                        this.TYDESC.Visible = true;
                    }
                }

                if ((_dt.Rows[fiCount]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount]["LOCGIGANTYPE"].ToString() + _dt.Rows[fiCount]["LOCLOAN"].ToString() + _dt.Rows[fiCount]["LOCLOANTYPE"].ToString()) ==
                            (_dt.Rows[fiCount + 1]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCGIGANTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCLOAN"].ToString() + _dt.Rows[fiCount + 1]["LOCLOANTYPE"].ToString()))
                {
                    // 차입유형
                    underline.X1 = 1.271F;
                }
                else if ((_dt.Rows[fiCount]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount]["LOCGIGANTYPE"].ToString() + _dt.Rows[fiCount]["LOCLOAN"].ToString()) ==
                            (_dt.Rows[fiCount + 1]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCGIGANTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCLOAN"].ToString()))
                {
                    // 차입용도
                    underline.X1 = 0.864F;
                }
                else if ((_dt.Rows[fiCount]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount]["LOCGIGANTYPE"].ToString()) ==
                            (_dt.Rows[fiCount + 1]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCGIGANTYPE"].ToString()))
                {
                    // 기간유형
                    underline.X1 = 0F;
                }
                else if ((_dt.Rows[fiCount]["LOCCURRTYPE"].ToString()) !=
                        (_dt.Rows[fiCount + 1]["LOCCURRTYPE"].ToString()))
                {
                    // 통화유형
                    underline.X1 = 0F;
                }
                else
                {
                    underline.X1 = 0;
                }

            }
            else
            {
                if (_dt.Rows[fiCount]["CUDESC"].ToString() == "총 계")
                {   
                    this.LOCGIGANTYPENM.Text = "총 계";
                }

                this.LOCGIGANTYPENM.Visible = true;
                this.USDESC.Visible = true;
                this.TYDESC.Visible = true;
            }

            fiCount++;
        }
    }
}
