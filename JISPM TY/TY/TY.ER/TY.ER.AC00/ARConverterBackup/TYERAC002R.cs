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
    /// Summary description for TYERAC002R.
    /// </summary>
    public partial class TYERAC002R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private DataTable dtrpt = new DataTable();

        private double fdSDRTotal = 0;
        private double fdSCRTotal = 0;
        private double fdHDAMTTotal = 0;
        private double fdHCAMTTotal = 0;
        private double fdHDSUMTotal = 0;
        private double fdHCSUMTotal = 0;

        private int _fiCount = 0;
        private int _rowCount = 0;

        public TYERAC002R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //this.PrintWidth = 11.7F - this.PageSettings.Margins.Left - this.PageSettings.Margins.Right;
        }

        private void detail_Format(object sender, EventArgs e)
        {

            _rowCount++;

            if (this._rowCount == 25)
            {
                this.line6.LineStyle = LineStyle.Solid;
                this.line6.LineWeight = 2;

                this._rowCount = 0;
            }
            else
            {
                this.line6.LineStyle = LineStyle.Dash;
                this.line6.LineWeight = 1;
            }
        }

        private void TYERAC002R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;

                dtrpt = UP_SuTotal_ds(dt);

                this.DataSource = dtrpt;
            }
        }      

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            line6.Visible = false;
            
            txtSDRTOTAL.Text = string.Format("{0:#,##0}", (fdSDRTotal));
            txtHDAMTTOTAL.Text = string.Format("{0:#,##0}", (fdHDAMTTotal));
            txtHDSUMTOTAL.Text = string.Format("{0:#,##0}", (fdHDSUMTotal));
            txtSCRTOTAL.Text = string.Format("{0:#,##0}", (fdSCRTotal));
            txtHCAMTTOTAL.Text = string.Format("{0:#,##0}", (fdHCAMTTotal));
            txtHCSUMTOTAL.Text = string.Format("{0:#,##0}", (fdHCSUMTotal));
 
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            txtKIJUNDATE.Text = Convert.ToString(this.Fields["YYMM"].Value).Substring(0, 4) + "년" +
                                Convert.ToString(this.Fields["YYMM"].Value).Substring(4, 2) + "월 기준";

        }


        #region Description : 합계 계산
        private DataTable UP_SuTotal_ds(DataTable ds)
        {
            string sFilter = "";

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = ds.Clone();

            int nNum = ds.Rows.Count;

            if (nNum > 0)
            {

                foreach (DataRow dr in ds.Select("A1YNTB='Y'", "C2CDAC ASC"))
                    table.Rows.Add(dr.ItemArray);

                sFilter = "A1TAG02 =  'Y' ";

                fdSDRTotal = Convert.ToDouble(ds.Compute("Sum(SDR)", sFilter).ToString());
                fdSCRTotal = Convert.ToDouble(ds.Compute("Sum(SDR)", sFilter).ToString());
                fdHDAMTTotal = Convert.ToDouble(ds.Compute("Sum(HDAMT)", sFilter).ToString());
                fdHCAMTTotal = Convert.ToDouble(ds.Compute("Sum(HCAMT)", sFilter).ToString());
                fdHDSUMTotal = Convert.ToDouble(ds.Compute("Sum(HDSUM)", sFilter).ToString());
                fdHCSUMTotal = Convert.ToDouble(ds.Compute("Sum(HCSUM)", sFilter).ToString());
            }

            return table;
        }
        #endregion

    }
}
