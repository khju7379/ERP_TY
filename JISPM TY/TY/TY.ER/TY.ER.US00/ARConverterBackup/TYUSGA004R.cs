﻿using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSGA004R.
    /// </summary>
    public partial class TYUSGA004R : DataDynamics.ActiveReports.ActiveReport
    {
        private System.Drawing.Image fqrImage;

        public TYUSGA004R(System.Drawing.Image qrImage)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fqrImage = qrImage;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.PIC_QRCODE.Image = fqrImage;
        }
    }
}
