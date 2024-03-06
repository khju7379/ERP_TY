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

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME029R.
    /// </summary>
    public partial class TYUTME029R : GrapeCity.ActiveReports.SectionReport
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
        private double fdHYHMTOT = 0;
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
        private double fdHYHMVATTOT = 0;
        private double fdMCEVATTOT = 0;

        public TYUTME029R(string sYYYYMM, string sDATE)
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
            fdMCENBOAMT = Convert.ToDouble(_dt.Rows[fiCount]["MCENBOAMT"].ToString());
            fdMCENCHAMT = Convert.ToDouble(_dt.Rows[fiCount]["MCENCHAMT"].ToString());
            fdMCENIPOVAM = Convert.ToDouble(_dt.Rows[fiCount]["MCENIPOVAM"].ToString());
            fdMCENCHOVAM = Convert.ToDouble(_dt.Rows[fiCount]["MCENCHOVAM"].ToString());
            fdMCENBOJAMT = Convert.ToDouble(_dt.Rows[fiCount]["MCENBOJAMT"].ToString());
            fdMCUTILITY = Convert.ToDouble(_dt.Rows[fiCount]["MCUTILITY"].ToString());
            fdBOCHTOT = fdMCENBOAMT + fdMCENCHAMT + fdMCENIPOVAM + fdMCENCHOVAM + fdMCENBOJAMT + fdMCUTILITY;
            fdHYAMT = Convert.ToDouble(_dt.Rows[fiCount]["HYAMT"].ToString());
            fdHMAMT = Convert.ToDouble(_dt.Rows[fiCount]["HMAMT"].ToString());
            fdHYHMTOT = fdHYAMT + fdHMAMT;
            fdM2ENINAM = Convert.ToDouble(_dt.Rows[fiCount]["M2ENINAM"].ToString());
            fdMCETOT = fdBOCHTOT + fdHYHMTOT + fdM2ENINAM;

            fdMCENBOVAT = fdMCENBOAMT / 10;
            fdMCENCHVAT = fdMCENCHAMT / 10;
            fdMCENIPOVVAT = fdMCENIPOVAM / 10;
            fdMCENCHOVVAT = fdMCENCHOVAM / 10;
            fdMCENBOJVAT = fdMCENBOJAMT / 10;
            fdMCUTILITYVAT = fdMCUTILITY / 10;
            fdBOCHVATTOT = fdMCENBOVAT + fdMCENCHVAT + fdMCENIPOVVAT + fdMCENCHOVVAT + fdMCENBOJVAT + fdMCUTILITYVAT;
            fdHYVAT = fdHYAMT / 10;
            fdHMVAT = fdHMAMT / 10;
            fdHYHMVATTOT = fdHYVAT + fdHMVAT;
            fdMCEVATTOT = fdBOCHVATTOT + fdHYHMVATTOT;

            this.BOCHTOT.Text = string.Format("{0:#,###}", fdBOCHTOT);
            this.HYHMTOT.Text = string.Format("{0:#,###}", fdHYHMTOT);
            this.MCETOT.Text = string.Format("{0:#,###}", fdMCETOT);

            this.MCENBOVAT.Text = string.Format("{0:#,###}", fdMCENBOVAT);
            this.MCENCHVAT.Text = string.Format("{0:#,###}", fdMCENCHVAT);
            this.MCENIPOVVAT.Text = string.Format("{0:#,###}", fdMCENIPOVVAT);
            this.MCENCHOVVAT.Text = string.Format("{0:#,###}", fdMCENCHOVVAT);
            this.MCENBOJVAT.Text = string.Format("{0:#,###}", fdMCENBOJVAT);
            this.MCUTILITYVAT.Text = string.Format("{0:#,###}", fdMCUTILITYVAT);
            this.BOCHVATTOT.Text = string.Format("{0:#,###}", fdBOCHVATTOT);
            this.HYVAT.Text = string.Format("{0:#,###}", fdHYVAT);
            this.HMVAT.Text = string.Format("{0:#,###}", fdHMVAT);
            this.HYHMVATTOT.Text = string.Format("{0:#,###}", fdHYHMVATTOT);
            this.MCEVATTOT.Text = string.Format("{0:#,###}", fdMCEVATTOT);

            this.MCENBOTOT.Text = string.Format("{0:#,###}", fdMCENBOAMT + fdMCENBOVAT);
            this.MCENCHTOT.Text = string.Format("{0:#,###}", fdMCENCHAMT + fdMCENCHVAT);
            this.MCENIPOVTOT.Text = string.Format("{0:#,###}", fdMCENIPOVAM + fdMCENIPOVVAT);
            this.MCENCHOVTOT.Text = string.Format("{0:#,###}", fdMCENCHOVAM + fdMCENCHOVVAT);
            this.MCENBOJTOT.Text = string.Format("{0:#,###}", fdMCENBOJAMT + fdMCENBOJVAT);
            this.MCUTILITYTOT.Text = string.Format("{0:#,###}", fdMCUTILITY + fdMCUTILITYVAT);
            this.BOCHTOTTOT.Text = string.Format("{0:#,###}", fdBOCHTOT + fdBOCHVATTOT);
            this.HYTOT.Text = string.Format("{0:#,###}", fdHYAMT + fdHYVAT);
            this.HMTOT.Text = string.Format("{0:#,###}", fdHMAMT + fdHMVAT);
            this.HYHMTOTTOT.Text = string.Format("{0:#,###}", fdHYHMTOT + fdHYHMVATTOT);
            this.MCETOTTOT.Text = string.Format("{0:#,###}", fdMCETOT + fdMCEVATTOT);

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
