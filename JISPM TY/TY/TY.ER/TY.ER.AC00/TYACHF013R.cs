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
    /// Summary description for TYACHF013R.
    /// </summary>
    public partial class TYACHF013R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable _dt = new DataTable();
        private int _iRowCount = 0;
        private int _iCount = 0;
        private int _iBuseoCount = 0;

        private double fsFXMOVERAMOUNTTOTAL = 0;
        private double fsFXMINCDECAMOUNTTOTAL = 0;
        private double fsFXMAMMALAMOUNTTOTAL = 0;
        private double fsFXMJUNKIREPAMOUNTTOTAL = 0;
        private double fsSUBSUMTOTAL = 0;
        private double fsFXMREPAMOUNTTOTAL = 0;
        private double fsFXMREPAMOUNTSUMTOTAL = 0;
        private double fsFXMREPJANAMOUNTTOTAL = 0;

        public TYACHF013R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;
            string[] sDATA;

            if (_dt.Rows[_iCount]["FXMYYMM"].ToString().Substring(4, 2) == "12")
            {
                this.DATE.Text = "(" + Convert.ToDateTime((Convert.ToInt16(_dt.Rows[_iCount]["FXMYYMM"].ToString().Substring(0, 4)) + 1).ToString() + "-01-01").AddDays(-1).ToString("yyyy년 MM월 dd일") + ")";
            }
            else
            {
                this.DATE.Text = "(" + Convert.ToDateTime(_dt.Rows[_iCount]["FXMYYMM"].ToString().Substring(0, 4) + "-" + (Convert.ToInt16(_dt.Rows[_iCount]["FXMYYMM"].ToString().Substring(4, 2)) + 1).ToString() + "-01").AddDays(-1).ToString("yyyy년 MM월 dd일") + ")";
            }

            if (_iRowCount > 0)
            {
                if (((_iRowCount) % 20) == 0)
                {
                    sDATA = _dt.Rows[_iCount - 1]["ASTGUBN"].ToString().Split('-');
                    if (sDATA.Length > 1)
                    {
                        ASTGUBNNM.Text = sDATA[1];
                    }
                }
                else
                {
                    sDATA = _dt.Rows[_iCount]["ASTGUBN"].ToString().Split('-');
                    if (sDATA.Length > 1)
                    {
                        ASTGUBNNM.Text = sDATA[1];
                    }
                }
            }
            else
            {
                sDATA = _dt.Rows[_iCount]["ASTGUBN"].ToString().Split('-');
                if (sDATA.Length > 1)
                {
                    ASTGUBNNM.Text = sDATA[1];
                }
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fsFXMOVERAMOUNTTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["FXMOVERAMOUNT"].ToString());

            if (Convert.ToDouble(_dt.Rows[_iCount]["FXMINCDECAMOUNT"].ToString()) >= 0)
            {
                fsFXMINCDECAMOUNTTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["FXMINCDECAMOUNT"].ToString());
            }
            else
            {
                FXMINCDECAMOUNT.Text = "(" + string.Format("{0:#,##0}", Math.Abs(double.Parse(_dt.Rows[_iCount]["FXMINCDECAMOUNT"].ToString()))) + ")";
            }

            fsFXMAMMALAMOUNTTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["FXMAMMALAMOUNT"].ToString());
            fsFXMJUNKIREPAMOUNTTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["FXMJUNKIREPAMOUNT"].ToString());
            fsSUBSUMTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["SUBSUM"].ToString());
            fsFXMREPAMOUNTTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["FXMREPAMOUNT"].ToString());
            fsFXMREPAMOUNTSUMTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["FXMREPAMOUNTSUM"].ToString());
            fsFXMREPJANAMOUNTTOTAL += Convert.ToDouble(_dt.Rows[_iCount]["FXMREPJANAMOUNT"].ToString());

            // 한 페이지당 20줄까지 출력
            if (((_iRowCount + 1) % 20) == 0)
            {
                this.detail.NewPage = NewPage.After;
                under_line.Visible = true;
            }
            else
            {
                this.detail.NewPage = NewPage.None;
                under_line.Visible = false;
            }

            if (_dt.Rows.Count - 1 != _iCount)
            {
                _iCount++;
            }
            _iRowCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            string[] sDATA;
            sDATA = _dt.Rows[_iCount - 1]["ASTGUBN"].ToString().Split('-');
            if (sDATA.Length > 1)
            {
                FXSNAMETOTAL.Text = sDATA[1] + " 계";
            }
            //PSPAYTOTAL1.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT1"].ToString()));

            FXMOVERAMOUNTTOTAL.Text = string.Format("{0:#,##0}", fsFXMOVERAMOUNTTOTAL);
            FXMINCDECAMOUNTTOTAL.Text = string.Format("{0:#,##0}", fsFXMINCDECAMOUNTTOTAL);
            FXMAMMALAMOUNTTOTAL.Text = string.Format("{0:#,##0}", fsFXMAMMALAMOUNTTOTAL);
            FXMJUNKIREPAMOUNTTOTAL.Text = string.Format("{0:#,##0}", fsFXMJUNKIREPAMOUNTTOTAL);
            SUBSUMTOTAL.Text = string.Format("{0:#,##0}", fsSUBSUMTOTAL);
            FXMREPAMOUNTTOTAL.Text = string.Format("{0:#,##0}", fsFXMREPAMOUNTTOTAL);
            FXMREPAMOUNTSUMTOTAL.Text = string.Format("{0:#,##0}", fsFXMREPAMOUNTSUMTOTAL);
            FXMREPJANAMOUNTTOTAL.Text = string.Format("{0:#,##0}", fsFXMREPJANAMOUNTTOTAL);

            if (_iCount != _dt.Rows.Count - 1)
            {
                this.groupFooter1.NewPage = NewPage.After;
            }
            else
            {
                if (((_iRowCount + 1) % 20) == 0)
                {
                    this.groupFooter1.NewPage = NewPage.After;
                }
                else
                {
                    this.groupFooter1.NewPage = NewPage.None;
                }
            }

            fsFXMOVERAMOUNTTOTAL = 0;
            fsFXMINCDECAMOUNTTOTAL = 0;
            fsFXMAMMALAMOUNTTOTAL = 0;
            fsFXMJUNKIREPAMOUNTTOTAL = 0;
            fsSUBSUMTOTAL = 0;
            fsFXMREPAMOUNTTOTAL = 0;
            fsFXMREPAMOUNTSUMTOTAL = 0;
            fsFXMREPJANAMOUNTTOTAL = 0;

            _iRowCount = 0;
            _iBuseoCount++;
        }

    }
}
