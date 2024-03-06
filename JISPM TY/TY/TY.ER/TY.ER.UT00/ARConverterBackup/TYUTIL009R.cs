using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTIL009R.
    /// </summary>
    public partial class TYUTIL009R : DataDynamics.ActiveReports.ActiveReport
    {
        private string fsJLQTYTOT = string.Empty;
        private string fsJLAMTTOT = string.Empty;

        public TYUTIL009R(string sJLQTYTOT, string sJLAMTTOT)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsJLQTYTOT = sJLQTYTOT;
            fsJLAMTTOT = sJLAMTTOT;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.JLQTYTOT.Text = fsJLQTYTOT;
            this.JLAMTTOT.Text = fsJLAMTTOT;
        }
    }
}
