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
    /// Summary description for TYHRGB005R.
    /// </summary>
    public partial class TYHRGB005R : DataDynamics.ActiveReports.ActiveReport
    {
        string fsSDATE = string.Empty;
        string fsEDATE = string.Empty;

        public TYHRGB005R(string sSDATE, string sEDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsSDATE = sSDATE;
            fsEDATE = sEDATE; 
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            this.CISDATE.Text = fsSDATE;
            this.CIEDATE.Text = fsEDATE;
        }
    }
}
