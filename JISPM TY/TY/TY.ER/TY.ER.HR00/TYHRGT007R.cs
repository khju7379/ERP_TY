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

namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRGT007R.
    /// </summary>
    public partial class TYHRGT007R : GrapeCity.ActiveReports.SectionReport
    {
        int _iCount = 0;
        int _iPage = 0;
        DataTable _dt = new DataTable();

        public TYHRGT007R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            if (_iPage == 0)
            {
                this.KBBUSEONM.Text = _dt.Rows[_iPage]["KBBUSEONM"].ToString();
                this.GIDATE.Text = _dt.Rows[_iPage]["GIDATE"].ToString();
                this.GIYOILNM.Text = _dt.Rows[_iPage]["GIYOILNM"].ToString();
                this.GIHUMUCDNM.Text = _dt.Rows[_iPage]["GIHUMUCDNM"].ToString();
            }
            _iPage++;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            int i = 0;
            if (_iCount > 0)
            {
                this.KBBUSEONM.Text = _dt.Rows[_iCount]["KBBUSEONM"].ToString();
                this.GIDATE.Text = _dt.Rows[_iCount]["GIDATE"].ToString();
                this.GIYOILNM.Text = _dt.Rows[_iCount]["GIYOILNM"].ToString();
                this.GIHUMUCDNM.Text = _dt.Rows[_iCount]["GIHUMUCDNM"].ToString();

                //if (_dt.Rows[_iCount - 1]["KBBUSEO"].ToString() != _dt.Rows[_iCount]["KBBUSEO"].ToString())
                //{   
                //    this.detail.NewPage = NewPage.Before;
                //}
                //else
                //{   
                //    this.detail.NewPage = NewPage.None;
                //}
                //if (_dt.Rows[_iCount - 1]["GIDATE"].ToString() != _dt.Rows[_iCount]["GIDATE"].ToString())
                //{   
                //    this.detail.NewPage = NewPage.Before;
                //}
                //else
                //{   
                //    this.detail.NewPage = NewPage.None;
                //}

                if (_dt.Rows[_iCount - 1]["KBBUSEO"].ToString() != _dt.Rows[_iCount]["KBBUSEO"].ToString() || _dt.Rows[_iCount - 1]["GIDATE"].ToString() != _dt.Rows[_iCount]["GIDATE"].ToString())
                {
                    this.detail.NewPage = NewPage.Before;
                }
                else
                {
                    this.detail.NewPage = NewPage.None;
                }

                if (_iCount < _dt.Rows.Count - 1)
                {
                    line7.Visible = false;

                    if (_dt.Rows[_iCount]["KBBUSEO"].ToString() != _dt.Rows[_iCount + 1]["KBBUSEO"].ToString())
                    {
                        line7.Visible = true;
                    }
                    if (_dt.Rows[_iCount]["GIDATE"].ToString() != _dt.Rows[_iCount + 1]["GIDATE"].ToString())
                    {
                        line7.Visible = true;
                    }
                }
                else
                {
                    line7.Visible = true;
                }
            }
            _iCount++;
        }
    }
}
