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
    /// Summary description for TYUTIL014R4.
    /// </summary>
    public partial class TYUTIL014R4 : DataDynamics.ActiveReports.ActiveReport
    {
        private DataTable _dt = new DataTable();
        private int i = 0;

        public TYUTIL014R4()
        //public TYUTIL014R4(DataTable dt)
        {
            //_dt = dt;
            //this.DataSource = _dt;

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            i++;
        }
    }
}
