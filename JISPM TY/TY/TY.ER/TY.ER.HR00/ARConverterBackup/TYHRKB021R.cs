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
    /// Summary description for TYHRKB021R.
    /// </summary>
    public partial class TYHRKB021R : DataDynamics.ActiveReports.ActiveReport
    {

        private int _fiCount = 0;
        private int _fiHuHakCnt = 0;

        private string  fsTitle = "";

        public TYHRKB021R(DataTable dt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;

                txtINWONTOTAL.Text = Convert.ToString(_fiCount) + " 명";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["HKJUGUBN"].ToString() == "5")
                    {
                        _fiHuHakCnt += 1;                        
                    }

                    if (dt.Rows[i]["HKSHLGUBN"].ToString() == "5")
                    {
                        fsTitle = "대학교 및 전문대 학자금 수혜 명단";
                    }
                    else
                    {
                        fsTitle = "고등학교 학자금 수혜 명단";
                    }
                }

                if (_fiHuHakCnt > 0)
                {
                    txtHUHAKINWON.Text = "휴 학:" + Convert.ToString(_fiHuHakCnt) + " 명";
                }

                LBL01_TITLE.Text = fsTitle;

            }
        }

        private void detail_Format(object sender, EventArgs e)
        {

        }

      
    }
}
