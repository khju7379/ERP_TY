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
    /// Summary description for TYHRKB017R3.
    /// </summary>
    public partial class TYHRKB017R3 : DataDynamics.ActiveReports.ActiveReport
    {

        DataTable _dt = new DataTable();
        DataTable _dtM = new DataTable();

        public TYHRKB017R3(DataTable dtM)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _dtM = dtM;       
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            txtKBJUMIN.Text = _dt.Rows[0]["KBJUMIN"].ToString().Substring(0, 6) + "-" + _dt.Rows[0]["KBJUMIN"].ToString().Substring(6, 7);

            txtBeSdate.Text = _dt.Rows[0]["PSWKEDATE"].ToString().Substring(0, 4) + "-" + "01-01";

            lblreson1.Visible = false;
            lblreson2.Visible = false;
            lblreson3.Visible = false;
            lblreson4.Visible = false;
            lblreson5.Visible = false;
            lblreson6.Visible = false;

            //퇴직금 생성구분 1-중간정산 3-촉탁 4-중도퇴사 5-퇴직 6-기타
            if (_dt.Rows[0]["KBJKCD"].ToString() == "01")
            {                
                lblreson4.Visible = true;
            }
            else
            {
                if (_dt.Rows[0]["PSGUBN"].ToString() == "1" || _dt.Rows[0]["PSGUBN"].ToString() == "3" || _dt.Rows[0]["PSGUBN"].ToString() == "4"
                     || _dt.Rows[0]["PSGUBN"].ToString() == "5" || _dt.Rows[0]["PSGUBN"].ToString() == "6")
                {
                    lblreson6.Visible = true;
                }
                else
                {
                    //2-정년퇴직
                    lblreson1.Visible = true;
                }
            }
              
            if (_dt.Rows[0]["KBJKCD"].ToString() != "01")
            {
                lblexeCheck1.Visible = false;
                lblexeCheck2.Visible = true;
            }
            else
            {
                lblexeCheck1.Visible = true;
                lblexeCheck2.Visible = false;
            }

            
            lblISDATE.Text = _dt.Rows[0]["PSPAYDATE"].ToString().Replace("-", "").Substring(0, 4) + "년 " + _dt.Rows[0]["PSPAYDATE"].ToString().Replace("-", "").Substring(4, 2) + "월 " +
                             _dt.Rows[0]["PSPAYDATE"].ToString().Replace("-", "").Substring(6, 2) + "일"; 
                

         }     


    }
}
