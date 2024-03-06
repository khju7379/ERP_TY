using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRPY12R3.
    /// </summary>
    public partial class TYHRPY12R3 : DataDynamics.ActiveReports.ActiveReport
    {
        string fsYYMM = string.Empty;
        string fsGUBUNNM = string.Empty;

        public TYHRPY12R3(string sYYMM, string sGUBUNNM)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsYYMM = sYYMM;
            fsGUBUNNM = sGUBUNNM;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.YEAR.Text = fsYYMM.Substring(0, 4);
            this.MONTH.Text = fsYYMM.Substring(4, 2);
            this.GUBUN.Text = fsGUBUNNM;
        }
    }
}
