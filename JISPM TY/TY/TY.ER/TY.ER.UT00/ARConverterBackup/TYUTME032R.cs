using System;
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
    /// Summary description for TYUTME017R.
    /// </summary>
    public partial class TYUTME032R : DataDynamics.ActiveReports.ActiveReport
    {
        private int fiCount = 0;
        private int fiMaxCnt = 0;
        private DataTable _dt = new DataTable();

        public TYUTME032R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
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
                if (_dt.Rows[fiCount - 1]["EDHYYMM"].ToString() != _dt.Rows[fiCount]["EDHYYMM"].ToString())
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
                        this.detail.NewPage = NewPage.None;
                    }

                    if (_dt.Rows[fiCount - 1]["EDHIPHANG"].ToString() != _dt.Rows[fiCount]["EDHIPHANG"].ToString())
                    {
                        this.detail.NewPage = NewPage.Before;
                    }
                    else
                    {
                        this.detail.NewPage = NewPage.None;
                    }
                }
            }

            fiCount++;
        }
    }
}
