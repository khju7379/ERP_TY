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
    /// Summary description for TYACSE003R.
    /// </summary>
    public partial class TYACSE003R : GrapeCity.ActiveReports.SectionReport
    {   
        public TYACSE003R()
        {   
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            DataTable dt = this.DataSource as DataTable;
            double iAICOSTACQHAP = 0;
            double iARYLASTAMTHAP = 0;
            double iDANGGI_AMTHAP = 0;
            double iARTFIXDAMTHAP = 0;
            double iMISANG_AMTHAP = 0;

            this.YYMMDD.Text = String.Format("{0:yyyy년 MM월 dd일 현재}", System.DateTime.Now);
            this.SUBNM.Text = "(주)태영인더스트리";

            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                iAICOSTACQHAP += Convert.ToDouble(dt.Rows[i]["AICOSTACQ"]);
                iARYLASTAMTHAP += Convert.ToDouble(dt.Rows[i]["ARYLASTAMT"]);
                iDANGGI_AMTHAP += Convert.ToDouble(dt.Rows[i]["DANGGI_AMT"]);
                iARTFIXDAMTHAP += Convert.ToDouble(dt.Rows[i]["ARTFIXDAMT"]);
                iMISANG_AMTHAP += Convert.ToDouble(dt.Rows[i]["MISANG_AMT"]);
            }

            this.AICOSTACQHAP.Value = iAICOSTACQHAP.ToString();
            this.ARYLASTAMTHAP.Value = iARYLASTAMTHAP.ToString();
            this.DANGGI_AMTHAP.Value = iDANGGI_AMTHAP.ToString();
            this.ARTFIXDAMTHAP.Value = iARTFIXDAMTHAP.ToString();
            this.MISANG_AMTHAP.Value = iMISANG_AMTHAP.ToString();
        }
    }
}
