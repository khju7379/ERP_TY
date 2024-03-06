using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.HR00
{
    

    /// <summary>
    /// Summary description for TYHRNT002R1.
    /// </summary>
    public partial class TYHRNT002R1 : DataDynamics.ActiveReports.ActiveReport
    {        
        private DataTable fdt;
        private DataTable _dt;

        public TYHRNT002R1(DataTable dFamy)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fdt = dFamy;            
        }

        private void TYHRNT002R1_ReportStart(object sender, EventArgs e)
        {

            DataTable dt = this.DataSource as DataTable;

            _dt = dt;

            if (dt != null)
            {
                TYHRNT002R2 subReport = new TYHRNT002R2();
                subReport.DataSource = fdt;
                TYHRNT002R2.Report = subReport;
            }
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            // 인적공제 합계
            if (fdt.Rows.Count > 0)
            {
                txtWFKIBON.Text = fdt.Rows[0]["WFKIBON"].ToString();
                txtWFBUNYO.Text = fdt.Rows[0]["WFBUNYO"].ToString();
                txtWFSPARENT.Text = fdt.Rows[0]["WFSPARENT"].ToString();
                txtWFKYUNG70.Text = fdt.Rows[0]["WFKYUNG70"].ToString();
                txtWFJANG.Text = fdt.Rows[0]["WFJANG"].ToString();
                txtWFCHULSAN.Text = fdt.Rows[0]["WFCHULSAN"].ToString();
                txtWFJANYE.Text = fdt.Rows[0]["WFJANYE"].ToString();
                txtWFTAXBOHUM.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXBOHUM"].ToString()));
                txtWFBOHUM.Text = string.Format("{0:#,###}", Convert.ToDouble(fdt.Rows[0]["WFPROBOHUM"].ToString()));
                txtWFTAXPROBOHUM.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXPROBOHUM"].ToString()));
                txtWFPROBOHUM.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFPROBOHUM"].ToString()));
                txtWFTAXOBJBOHUM.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXOBJBOHUM"].ToString()));
                txtWFOBJBOHUM.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFOBJBOHUM"].ToString()));
                
                txtWFTAXMEDBH.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXMEDBHTOTAL"].ToString()));
                txtWFMEDBH.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFMEDBHTOTAL"].ToString()));

                txtWFTAXGYOUK.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXGYOUK"].ToString()));
                txtWFGYOUK.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFGYOUK"].ToString()));
                txtWFTAXCARD.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXCARD"].ToString()));
                txtWFCARD.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFCARD"].ToString()));
                txtWFTAXDEBCARD.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXDEBCARD"].ToString()));
                txtWFDEBCARD.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFDEBCARD"].ToString()));
                txtWFTAXCASH.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXCASH"].ToString()));
                txtWFTAXMARKET.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXMARKET"].ToString()));
                txtWFMARKET.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFMARKET"].ToString()));
                txtWFTAXPUBTRANS.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXPUBTRANS"].ToString()));
                txtWFPUBTRANS.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFPUBTRANS"].ToString()));
                txtWFTAXFUND.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFTAXFUND"].ToString()));
                txtWFFUND.Text = string.Format("{0:#,##0}", Convert.ToDouble(fdt.Rows[0]["WFFUND"].ToString()));



            }

            this.reportHeader1.NewPage = NewPage.After;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (_dt.Rows.Count > 0)
            {
                label172.Text = (Convert.ToInt16(_dt.Rows[0]["WIYEAR"].ToString()) - 2).ToString() + "년 출자·투자분";
                label173.Text = (Convert.ToInt16(_dt.Rows[0]["WIYEAR"].ToString()) - 1).ToString() + "년 출자·투자분";
                label174.Text = (Convert.ToInt16(_dt.Rows[0]["WIYEAR"].ToString())).ToString() + "년 출자·투자분";
            }

            this.detail.NewPage = NewPage.After;
        }
    }
}
