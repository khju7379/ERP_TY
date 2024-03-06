using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.AT00
{
    /// <summary>
    /// Summary description for TYATKB006R.
    /// </summary>
    public partial class TYATKB006R1 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable[] _dtDet;
        private DataTable _dtOre = new DataTable();
        private int fiCount = 0;

        public TYATKB006R1(DataTable[] dtDet, DataTable dtOre)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _dtDet = dtDet;
            _dtOre = dtOre;
        }

        private void TYATKB006R1_ReportStart(object sender, EventArgs e)
        {
            
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            if (_dtOre.Rows.Count > 0)
            {
                TYATKB006R2 subReport1 = new TYATKB006R2();
                subReport1.DataSource = _dtOre;
                TYATKB006R2.Report = subReport1;
            }

            // 내역 subreport
            if (fiCount < _dtDet.Length)
            {
                TYATKB006R3 subReport2 = new TYATKB006R3();
                subReport2.DataSource = _dtDet[fiCount];
                TYATKB006R3.Report = subReport2;
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.detail.NewPage = NewPage.BeforeAfter;
            fiCount++;
        }
    }
}
