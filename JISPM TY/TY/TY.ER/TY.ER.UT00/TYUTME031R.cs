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
    /// Summary description for TYUTME022R.
    /// </summary>
    public partial class TYUTME031R : GrapeCity.ActiveReports.SectionReport
    {
        private int fiCount = 0;
        private int fiMaxCnt = 0;
        private DataTable _dt = new DataTable();

        public TYUTME031R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            //this.detail.NewPage = NewPage.After;
        }

        private void groupHeader2_Format(object sender, EventArgs e)
        {
            //this.detail.NewPage = NewPage.After;
        }

        private void groupHeader3_Format(object sender, EventArgs e)
        {
            //this.detail.NewPage = NewPage.After;
        }

        private void groupHeader4_Format(object sender, EventArgs e)
        {
            //this.detail.NewPage = NewPage.After;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = (DataTable)this.DataSource;
            fiMaxCnt = _dt.Rows.Count;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (fiCount > 0)
            {
                if (_dt.Rows[fiCount - 1]["EDBYYMM"].ToString() != _dt.Rows[fiCount]["EDBYYMM"].ToString())
                {
                    this.detail.NewPage = NewPage.Before;
                }
                else
                {
                    if (_dt.Rows[fiCount - 1]["NUM"].ToString() != _dt.Rows[fiCount]["NUM"].ToString())
                    {
                        this.detail.NewPage = NewPage.Before;
                    }
                    else
                    {
                        if (_dt.Rows[fiCount - 1]["EDBHWAMUL"].ToString() != _dt.Rows[fiCount]["EDBHWAMUL"].ToString())
                        {
                            this.detail.NewPage = NewPage.Before;
                        }
                        else
                        {
                            this.detail.NewPage = NewPage.None;
                        }

                        if (_dt.Rows[fiCount - 1]["EDBCNT"].ToString() != _dt.Rows[fiCount]["EDBCNT"].ToString())
                        {
                            this.detail.NewPage = NewPage.Before;
                        }
                        else
                        {
                            this.detail.NewPage = NewPage.None;
                        }
                    }
                }
            }

            fiCount++;
        }
    }
}
