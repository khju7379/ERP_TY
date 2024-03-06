using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.BS00
{
    /// <summary>
    /// Summary description for TYBSSJ012R.
    /// </summary>
    public partial class TYBSSJ012R : DataDynamics.ActiveReports.ActiveReport
    {
        private string fsPREYEAR = string.Empty;

        public TYBSSJ012R(string sPREYEAR)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsPREYEAR = sPREYEAR;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            this.PREYEAR.Text = fsPREYEAR;
        }
    }
}
