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
    /// Summary description for TYUTME022R2.
    /// </summary>
    public partial class TYUTME022R2 : DataDynamics.ActiveReports.ActiveReport
    {
        private int fiCount = 0;
        private int fiMaxCnt = 0;
        private DataTable _dt = new DataTable();

        public TYUTME022R2()
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
                //if (_dt.Rows[fiCount - 1]["NUM"].ToString() != _dt.Rows[fiCount]["NUM"].ToString())
                //{
                //    this.detail.NewPage = NewPage.Before;
                //}
                //else
                //{
                //    if (_dt.Rows[fiCount - 1]["EDBHWAMUL"].ToString() != _dt.Rows[fiCount]["EDBHWAMUL"].ToString())
                //    {
                //        this.detail.NewPage = NewPage.Before;
                //    }
                //    else
                //    {
                //        this.detail.NewPage = NewPage.None;
                //    }

                //    if (_dt.Rows[fiCount - 1]["EDBCNT"].ToString() != _dt.Rows[fiCount]["EDBCNT"].ToString())
                //    {
                //        this.detail.NewPage = NewPage.Before;
                //    }
                //    else
                //    {
                //        this.detail.NewPage = NewPage.None;
                //    }

                //    if (_dt.Rows[fiCount - 1]["EDBHWAJU"].ToString() != _dt.Rows[fiCount]["EDBHWAJU"].ToString())
                //    {
                //        this.detail.NewPage = NewPage.Before;
                //    }
                //    else
                //    {
                //        this.detail.NewPage = NewPage.None;
                //    }
                //}

                if (_dt.Rows[fiCount]["EDBSEQ"].ToString() == "1")
                {
                    this.detail.NewPage = NewPage.Before;
                }
                else
                {
                    this.detail.NewPage = NewPage.None;
                }
            }

            fiCount++;
        }
    }
}
