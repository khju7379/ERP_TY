using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSNJ010R4.
    /// </summary>
    public partial class TYUSPR001R1 : GrapeCity.ActiveReports.SectionReport
    {
        private string fsGubn;  //1:내부용 2:외부용

        public TYUSPR001R1(string sGubn)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fsGubn = sGubn;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this.groupFooter1.NewPage = NewPage.After;            


        }

        private void TYUSPR001R1_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;

        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {

            if (Convert.ToDouble(txtJGHANGCHAQTY1HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY1HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY2HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY2HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY3HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY3HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY4HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY4HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY5HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY5HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY6HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY6HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY7HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY7HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY8HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY8HAP.Text = "";
            }
            if (Convert.ToDouble(txtJGHANGCHAQTY9HAP.Text) <= 0)
            {
                txtJGHANGCHAQTY9HAP.Text = "";
            }

            if (fsGubn != "1")  //외부용이면 보관료시작일를 안보여준다
            {
                txtBoganSdate.Visible = false;
                txtJGJESTDAT1.Visible = false;
                txtJGJESTDAT2.Visible = false;
                txtJGJESTDAT3.Visible = false;
                txtJGJESTDAT4.Visible = false;
                txtJGJESTDAT5.Visible = false;
                txtJGJESTDAT6.Visible = false;
                txtJGJESTDAT7.Visible = false;
                txtJGJESTDAT8.Visible = false;
                txtJGJESTDAT9.Visible = false;

                line41.Visible = false;
                line42.Visible = false;
                line43.Visible = false;
                line44.Visible = false;
                line45.Visible = false;
                line46.Visible = false;
                line47.Visible = false;
                line48.Visible = false;
                line49.Visible = false;
                line50.Visible = false;
                line51.Visible = false;
                line52.Visible = false;
                line53.Visible = false;
            }
        }
    }
}