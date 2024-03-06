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
    /// Summary description for TYACLO003R.
    /// </summary>
    public partial class TYACLO003R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private string fsDATE = string.Empty;

        public TYACLO003R(string sDTAE)
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
                    if (_dt.Rows[fiCount]["LOCCURRTYPE"].ToString() == _dt.Rows[fiCount - 1]["LOCCURRTYPE"].ToString())
                    {
                        this.CUDESC.Visible = false;
                    }
                    else
                    {
                        this.CUDESC.Visible = true;
                    }

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
                    underline.X1 = 1.86F;
                }
                else if ((_dt.Rows[fiCount]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount]["LOCGIGANTYPE"].ToString() + _dt.Rows[fiCount]["LOCLOAN"].ToString()) ==
                            (_dt.Rows[fiCount + 1]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCGIGANTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCLOAN"].ToString()))
                {
                    // 차입용도
                    underline.X1 = 1.35F;
                }
                else if ((_dt.Rows[fiCount]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount]["LOCGIGANTYPE"].ToString()) ==
                            (_dt.Rows[fiCount + 1]["LOCCURRTYPE"].ToString() + _dt.Rows[fiCount + 1]["LOCGIGANTYPE"].ToString()))
                {
                    // 기간유형
                    underline.X1 = 0.434F;
                }
                else if ((_dt.Rows[fiCount]["LOCCURRTYPE"].ToString()) !=
                            (_dt.Rows[fiCount + 1]["LOCCURRTYPE"].ToString()))
                {
                    // 통화유형
                    underline.X1 = 0F;
                }
                else
                {
                    underline.X1 = 0.434F;
                }

            }
            else
            {
                this.CUDESC.Visible = true;
                this.LOCGIGANTYPENM.Visible = true;
                this.USDESC.Visible = true;
                this.TYDESC.Visible = true;
            }

            fiCount++;
        }
    }
}
