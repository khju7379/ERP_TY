﻿using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME029R1.
    /// </summary>
    public partial class TYUTME029R1 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int fiCount = 0;
        private string fsYYYYMM = string.Empty;
        private string fsDATE = string.Empty;

        private double fdMCENBOAMT = 0;
        private double fdMCENCHAMT = 0;
        private double fdMCENIPOVAM = 0;
        private double fdMCENCHOVAM = 0;
        private double fdMCENBOJAMT = 0;
        private double fdMCUTILITY = 0;
        private double fdBOCHTOT = 0;
        private double fdHYAMT = 0;
        private double fdHMAMT = 0;
        private double fdHMSEAMT = 0;
        private double fdGITATOT = 0;
        private double fdM2ENINAM = 0;
        private double fdMCETOT = 0;

        private double fdMCENBOVAT = 0;
        private double fdMCENCHVAT = 0;
        private double fdMCENIPOVVAT = 0;
        private double fdMCENCHOVVAT = 0;
        private double fdMCENBOJVAT = 0;
        private double fdMCUTILITYVAT = 0;
        private double fdBOCHVATTOT = 0;
        private double fdHYVAT = 0;
        private double fdHMVAT = 0;
        private double fdGITAVATTOT = 0;
        private double fdMCEVATTOT = 0;

        public TYUTME029R1(string sYYYYMM, string sDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsYYYYMM = sYYYYMM;
            fsDATE = sDATE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (fiCount == 0)
            {
                _dt = (DataTable)this.DataSource;
            }

            this.MAECHUL_YAER.Text = fsYYYYMM.Substring(0, 4);
            this.MAECHUL_MONTH.Text = fsYYYYMM.Substring(4, 2);
        }

        private void detail_Format(object sender, EventArgs e)
        {
            fdMCENBOAMT = 0;
            fdMCENCHAMT = 0;
            fdMCENIPOVAM = 0;
            fdMCENCHOVAM = 0;
            fdMCENBOJAMT = 0;
            fdMCUTILITY = 0;
            fdBOCHTOT = 0;
            fdHYAMT = 0;
            fdHMAMT = 0;
            fdHMSEAMT = 0;
            fdGITATOT = 0;
            fdM2ENINAM = 0;
            fdMCETOT = 0;

            fdMCENBOVAT = 0;
            fdMCENCHVAT = 0;
            fdMCENIPOVVAT = 0;
            fdMCENCHOVVAT = 0;
            fdMCENBOJVAT = 0;
            fdMCUTILITYVAT = 0;
            fdBOCHVATTOT = 0;
            fdHYVAT = 0;
            fdHMVAT = 0;
            fdGITAVATTOT = 0;
            fdMCEVATTOT = 0;

            fdMCENBOAMT = Convert.ToDouble(_dt.Rows[fiCount]["MCENBOAMT"].ToString());
            fdMCENCHAMT = Convert.ToDouble(_dt.Rows[fiCount]["MCENCHAMT"].ToString());
            fdMCENIPOVAM = Convert.ToDouble(_dt.Rows[fiCount]["MCENIPOVAM"].ToString());
            fdMCENCHOVAM = Convert.ToDouble(_dt.Rows[fiCount]["MCENCHOVAM"].ToString());
            fdMCENBOJAMT = Convert.ToDouble(_dt.Rows[fiCount]["MCENBOJAMT"].ToString());
            fdMCUTILITY = Convert.ToDouble(_dt.Rows[fiCount]["MCUTILITY"].ToString());
            
            // 보관취급료 공급가 합계
            fdBOCHTOT = fdMCENBOAMT + fdMCENCHAMT + fdMCENIPOVAM + fdMCENCHOVAM + fdMCENBOJAMT + fdMCUTILITY;


            // 하역료 공급가
            fdHYAMT = Convert.ToDouble(_dt.Rows[fiCount]["HYAMT"].ToString());
            
            // 화물료 공급가
            fdHMAMT = Convert.ToDouble(_dt.Rows[fiCount]["HMAMT"].ToString());

            // 보안료 공급가
            fdHMSEAMT = Convert.ToDouble(_dt.Rows[fiCount]["HMSEAMT"].ToString());

            // 보험료 공급가
            fdM2ENINAM = Convert.ToDouble(_dt.Rows[fiCount]["M2ENINAM"].ToString());

            // 기타 공급가 합계
            fdGITATOT = fdHMAMT + fdM2ENINAM + fdHMSEAMT;

            // 공급가 합계(보관취급료 + 하역료 + 기타)
            fdMCETOT = fdBOCHTOT + fdHYAMT + fdGITATOT;

            fdMCENBOVAT = Math.Truncate(fdMCENBOAMT / 10);
            fdMCENCHVAT = Math.Truncate(fdMCENCHAMT / 10);
            fdMCENIPOVVAT = Math.Truncate(fdMCENIPOVAM / 10);
            fdMCENCHOVVAT = Math.Truncate(fdMCENCHOVAM / 10);
            fdMCENBOJVAT = Math.Truncate(fdMCENBOJAMT / 10);
            fdMCUTILITYVAT = Math.Truncate(fdMCUTILITY / 10);

            // 보관취급료 부가세 합계
            fdBOCHVATTOT = fdMCENBOVAT + fdMCENCHVAT + fdMCENIPOVVAT + fdMCENCHOVVAT + fdMCENBOJVAT + fdMCUTILITYVAT;

            // 하역료 부가세 합계
            fdHYVAT = Math.Truncate(fdHYAMT / 10);

            // 화물료 부가세 합계
            fdHMVAT = 0;

            // 기타 부가세
            fdGITAVATTOT = fdHMVAT;

            // 부가세 합계(보관취급료 + 화역료 + 기타)
            fdMCEVATTOT = fdBOCHVATTOT + fdHYVAT + fdGITAVATTOT;

            // 보관취급료 공급가 합계
            this.BOCHTOT.Text = string.Format("{0:#,###}", fdBOCHTOT);
            // 기타 공급가 합계
            this.GITATOT.Text = string.Format("{0:#,###}", fdGITATOT);
            // 총 공급가 합계
            this.MCETOT.Text = string.Format("{0:#,###}", fdMCETOT);

            this.MCENBOVAT.Text = string.Format("{0:#,###}", fdMCENBOVAT);
            this.MCENCHVAT.Text = string.Format("{0:#,###}", fdMCENCHVAT);
            this.MCENIPOVVAT.Text = string.Format("{0:#,###}", fdMCENIPOVVAT);
            this.MCENCHOVVAT.Text = string.Format("{0:#,###}", fdMCENCHOVVAT);
            this.MCENBOJVAT.Text = string.Format("{0:#,###}", fdMCENBOJVAT);
            this.MCUTILITYVAT.Text = string.Format("{0:#,###}", fdMCUTILITYVAT);
            this.BOCHVATTOT.Text = string.Format("{0:#,###}", fdBOCHVATTOT);
            this.HYVAT.Text = string.Format("{0:#,###}", fdHYVAT);
            this.HYVATTOT.Text = string.Format("{0:#,###}", fdHYVAT);
            this.HMVAT.Text = string.Format("{0:#,###}", fdHMVAT);
            this.GITAVATTOT.Text = string.Format("{0:#,###}", fdGITAVATTOT);
            this.MCEVATTOT.Text = string.Format("{0:#,###}", fdMCEVATTOT);

            this.MCENBOTOT.Text = string.Format("{0:#,###}", fdMCENBOAMT + fdMCENBOVAT);
            this.MCENCHTOT.Text = string.Format("{0:#,###}", fdMCENCHAMT + fdMCENCHVAT);
            this.MCENIPOVTOT.Text = string.Format("{0:#,###}", fdMCENIPOVAM + fdMCENIPOVVAT);
            this.MCENCHOVTOT.Text = string.Format("{0:#,###}", fdMCENCHOVAM + fdMCENCHOVVAT);
            this.MCENBOJTOT.Text = string.Format("{0:#,###}", fdMCENBOJAMT + fdMCENBOJVAT);
            this.MCUTILITYTOT.Text = string.Format("{0:#,###}", fdMCUTILITY + fdMCUTILITYVAT);
            this.BOCHTOTTOT.Text = string.Format("{0:#,###}", fdBOCHTOT + fdBOCHVATTOT);
            this.HYTOT.Text = string.Format("{0:#,###}", fdHYAMT + fdHYVAT);
            this.HYTOTTOT.Text = string.Format("{0:#,###}", fdHYAMT + fdHYVAT);
            this.HMTOT.Text = string.Format("{0:#,###}", fdHMAMT + fdHMVAT);
            this.GITATOTTOT.Text = string.Format("{0:#,###}", fdGITATOT + fdGITAVATTOT);
            this.MCETOTTOT.Text = string.Format("{0:#,###}", fdMCETOT + fdMCEVATTOT);

            this.BOCHHYTOT.Text = string.Format("{0:#,###}", fdBOCHTOT + fdHYAMT);
            this.BOCHHYVATTOT.Text = string.Format("{0:#,###}", fdBOCHVATTOT + fdHYVAT);
            this.BOCHHYTOTTOT.Text = string.Format("{0:#,###}", fdBOCHTOT + fdBOCHVATTOT + fdHYAMT + fdHYVAT);

            this.TOTAL.Text = "￦" + string.Format("{0:#,###}", fdMCETOT + fdMCEVATTOT);

            fiCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            //this.PRJEMTTOT.Text = string.Format("{0:#,##0.000}", fdPRJEMTTOT);

            this.YY.Text = fsDATE.Substring(0, 4);
            this.MM.Text = fsDATE.Substring(5, 2);
            this.DD.Text = fsDATE.Substring(8, 2);

            this.groupFooter1.NewPage = NewPage.After;
        }
    }
}
