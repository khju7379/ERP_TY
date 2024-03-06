using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSME026R.
    /// </summary>
    public partial class TYUSME026R : DataDynamics.ActiveReports.ActiveReport
    {

        public TYUSME026R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if(TMREPORTNM16.Text.Trim() == "")
            {
                label16.Text = "";
            }

            if (TMREPORTNM17.Text.Trim() == "")
            {
                label17.Text = "";
            }

            if (TMREPORTNM18.Text.Trim() == "")
            {
                label18.Text = "";
            }

            if (TMREPORTNM19.Text.Trim() == "")
            {
                label19.Text = "";
            }

            if (TMREPORTNM20.Text.Trim() == "")
            {
                label20.Text = "";
            }




            
            
            
            if (TMREPORTNM21.Text.Trim() == "")
            {
                label21.Text = "";
            }

            if (TMREPORTNM22.Text.Trim() == "")
            {
                label22.Text = "";
            }

            if (TMREPORTNM23.Text.Trim() == "")
            {
                label23.Text = "";
            }

            if (TMREPORTNM24.Text.Trim() == "")
            {
                label24.Text = "";
            }

            if (TMREPORTNM25.Text.Trim() == "")
            {
                label25.Text = "";
            }

            if (TMREPORTNM26.Text.Trim() == "")
            {
                label26.Text = "";
            }

            if (TMREPORTNM27.Text.Trim() == "")
            {
                label27.Text = "";
            }
        }
    }
}
