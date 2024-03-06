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
    /// Summary description for TYHRSB001R.
    /// </summary>
    public partial class TYHRSB001R : DataDynamics.ActiveReports.ActiveReport
    {
        string fsSABUN = string.Empty;
        string fsHANGL = string.Empty;
        string fsYYMM  = string.Empty;

        double dGIBUNAMT    = 0;
        double dBONUSAMT = 0;
        double dTIMESIDEAMT = 0;
        double dWORKAMT     = 0;
        double dOTAMT       = 0;
        double dGIGUBHAP    = 0;
        double dNOJOAMT     = 0;
        double dSANGJOAMT   = 0;
        double dGOYOUNGAMT  = 0;
        double dGONGJAEHAP  = 0;
        double dCHAINHAP    = 0;
        double dSOGUBAMT    = 0;


        double dGIBUNAMT_HAP1    = 0;
        double dBONUSAMT_HAP1    = 0;
        double dTIMESIDEAMT_HAP1 = 0;
        double dWORKAMT_HAP1     = 0;
        double dOTAMT_HAP1       = 0;
        double dGIGUBHAP_HAP1    = 0;
        double dNOJOAMT_HAP1     = 0;
        double dSANGJOAMT_HAP1   = 0;
        double dGOYOUNGAMT_HAP1  = 0;
        double dGONGJAEHAP_HAP1  = 0;
        double dCHAINHAP_HAP1    = 0;
        double dSOGUBAMT_HAP1    = 0;

        double dGIBUNAMT_HAP2    = 0;
        double dBONUSAMT_HAP2 = 0;
        double dTIMESIDEAMT_HAP2 = 0;
        double dWORKAMT_HAP2     = 0;
        double dOTAMT_HAP2       = 0;
        double dGIGUBHAP_HAP2    = 0;
        double dNOJOAMT_HAP2     = 0;
        double dSANGJOAMT_HAP2   = 0;
        double dGOYOUNGAMT_HAP2  = 0;
        double dGONGJAEHAP_HAP2  = 0;
        double dCHAINHAP_HAP2    = 0;
        double dSOGUBAMT_HAP2    = 0;


        double dGIBUNAMT_TOT1    = 0;
        double dBONUSAMT_TOT1 = 0;
        double dTIMESIDEAMT_TOT1 = 0;
        double dWORKAMT_TOT1     = 0;
        double dOTAMT_TOT1       = 0;
        double dGIGUBHAP_TOT1    = 0;
        double dNOJOAMT_TOT1     = 0;
        double dSANGJOAMT_TOT1   = 0;
        double dGOYOUNGAMT_TOT1  = 0;
        double dGONGJAEHAP_TOT1  = 0;
        double dCHAINHAP_TOT1    = 0;
        double dSOGUBAMT_TOT1    = 0;

        double dGIBUNAMT_TOT2    = 0;
        double dBONUSAMT_TOT2 = 0;
        double dTIMESIDEAMT_TOT2 = 0;
        double dWORKAMT_TOT2     = 0;
        double dOTAMT_TOT2       = 0;
        double dGIGUBHAP_TOT2    = 0;
        double dNOJOAMT_TOT2     = 0;
        double dSANGJOAMT_TOT2   = 0;
        double dGOYOUNGAMT_TOT2  = 0;
        double dGONGJAEHAP_TOT2  = 0;
        double dCHAINHAP_TOT2    = 0;
        double dSOGUBAMT_TOT2    = 0;

        int _iCount     = 0;
        int _iPage      = 0;
        int _iRecordCnt = 0;

        string fsChange = string.Empty;

        DataTable _dt  = new DataTable();

        public TYHRSB001R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _iRecordCnt = 1;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            if (_iPage == 0)
            {
                //this.KBBUSEONM.Text = _dt.Rows[_iPage]["KBBUSEONM"].ToString();
                //this.GIDATE.Text = _dt.Rows[_iPage]["GIDATE"].ToString();
                //this.GIYOILNM.Text = _dt.Rows[_iPage]["GIYOILNM"].ToString();
                //this.GIHUMUCDNM.Text = _dt.Rows[_iPage]["GIHUMUCDNM"].ToString();
            }
            _iPage++;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            int i = 0;

            if (_iCount >= 0)
            {
                if (fsSABUN != _dt.Rows[_iCount]["SABUN"].ToString())
                {
                    this.SABUN.Text = _dt.Rows[_iCount]["SABUN"].ToString();
                    this.HANGL.Text = _dt.Rows[_iCount]["HANGL"].ToString();

                    fsSABUN = _dt.Rows[_iCount]["SABUN"].ToString();

                    this.YYMM.Text = "";

                    //if (_iCount != 0)
                    //{
                    //    this.GIBUNAMT.Text    = string.Format("{0:#,##0}", dGIBUNAMT_HAP);
                    //    this.TIMESIDEAMT.Text = string.Format("{0:#,##0}", dTIMESIDEAMT_HAP);
                    //    this.WORKAMT.Text     = string.Format("{0:#,##0}", dWORKAMT_HAP);
                    //    this.OTAMT.Text       = string.Format("{0:#,##0}", dGIBUNAMT_HAP);
                    //    this.GIGUBHAP.Text    = string.Format("{0:#,##0}", dGIBUNAMT_HAP + dTIMESIDEAMT_HAP + dWORKAMT_HAP + dGIBUNAMT_HAP);
                    //    this.NOJOAMT.Text     = string.Format("{0:#,##0}", dNOJOAMT_HAP);
                    //    this.SANGJOAMT.Text   = string.Format("{0:#,##0}", dSANGJOAMT_HAP);
                    //    this.GOYOUNGAMT.Text  = string.Format("{0:#,##0}", dGOYOUNGAMT_HAP);
                    //    this.GONGJAEHAP.Text  = string.Format("{0:#,##0}", dNOJOAMT_HAP + dSANGJOAMT_HAP + dGOYOUNGAMT_HAP);
                    //    this.CHAINHAP.Text    = string.Format("{0:#,##0}", (dGIBUNAMT_HAP + dTIMESIDEAMT_HAP + dWORKAMT_HAP + dGIBUNAMT_HAP) - (dNOJOAMT_HAP + dSANGJOAMT_HAP + dGOYOUNGAMT_HAP));
                    //    this.SOGUBAMT.Text    = string.Format("{0:#,##0}", dSOGUBAMT_HAP);
                    //}

                    //dGIBUNAMT_HAP    = 0;
                    //dTIMESIDEAMT_HAP = 0;
                    //dWORKAMT_HAP     = 0;
                    //dOTAMT_HAP       = 0;
                    //dGIGUBHAP_HAP    = 0;
                    //dNOJOAMT_HAP     = 0;
                    //dSANGJOAMT_HAP   = 0;
                    //dGOYOUNGAMT_HAP  = 0;
                    //dGONGJAEHAP_HAP  = 0;
                    //dCHAINHAP_HAP    = 0;
                    //dSOGUBAMT_HAP    = 0;
                }
                else
                {
                    this.SABUN.Text = "";
                    this.HANGL.Text = "";
                }

                // 그룹에서 = 소급 - 기존 => 소급금액 구함
                if (_dt.Rows[_iCount]["SEQ"].ToString() == "1") // 합계
                {
                    dGIBUNAMT_HAP1    = dGIBUNAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["GIBUNAMT"].ToString());
                    dBONUSAMT_HAP1     = dBONUSAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["BONUSAMT"].ToString());
                    dTIMESIDEAMT_HAP1 = dTIMESIDEAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["TIMESIDEAMT"].ToString());
                    dWORKAMT_HAP1     = dWORKAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["WORKAMT"].ToString());
                    dOTAMT_HAP1       = dOTAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["OTAMT"].ToString());
                    dGIGUBHAP_HAP1    = dGIGUBHAP_HAP1 + double.Parse(_dt.Rows[_iCount]["GIGUBHAP"].ToString());
                    dNOJOAMT_HAP1     = dNOJOAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["NOJOAMT"].ToString());
                    dSANGJOAMT_HAP1   = dSANGJOAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["SANGJOAMT"].ToString());
                    dGOYOUNGAMT_HAP1  = dGOYOUNGAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["GOYOUNGAMT"].ToString());
                    dGONGJAEHAP_HAP1  = dGONGJAEHAP_HAP1 + double.Parse(_dt.Rows[_iCount]["GONGJAEHAP"].ToString());
                    dCHAINHAP_HAP1    = dCHAINHAP_HAP1 + double.Parse(_dt.Rows[_iCount]["CHAINHAP"].ToString());
                    dSOGUBAMT_HAP1    = dSOGUBAMT_HAP1 + double.Parse(_dt.Rows[_iCount]["SOGUBAMT"].ToString());
                }
                else // 합계
                {
                    dGIBUNAMT_HAP2    = dGIBUNAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["GIBUNAMT"].ToString());
                    dBONUSAMT_HAP2 = dBONUSAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["BONUSAMT"].ToString());
                    dTIMESIDEAMT_HAP2 = dTIMESIDEAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["TIMESIDEAMT"].ToString());
                    dWORKAMT_HAP2     = dWORKAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["WORKAMT"].ToString());
                    dOTAMT_HAP2       = dOTAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["OTAMT"].ToString());
                    dGIGUBHAP_HAP2    = dGIGUBHAP_HAP2 + double.Parse(_dt.Rows[_iCount]["GIGUBHAP"].ToString());
                    dNOJOAMT_HAP2     = dNOJOAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["NOJOAMT"].ToString());
                    dSANGJOAMT_HAP2   = dSANGJOAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["SANGJOAMT"].ToString());
                    dGOYOUNGAMT_HAP2  = dGOYOUNGAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["GOYOUNGAMT"].ToString());
                    dGONGJAEHAP_HAP2  = dGONGJAEHAP_HAP2 + double.Parse(_dt.Rows[_iCount]["GONGJAEHAP"].ToString());
                    dCHAINHAP_HAP2    = dCHAINHAP_HAP2 + double.Parse(_dt.Rows[_iCount]["CHAINHAP"].ToString());
                    dSOGUBAMT_HAP2    = dSOGUBAMT_HAP2 + double.Parse(_dt.Rows[_iCount]["SOGUBAMT"].ToString());
                }

                if (fsYYMM != _dt.Rows[_iCount]["YYMM"].ToString())
                {
                    this.YYMM.Text = _dt.Rows[_iCount]["YYMM"].ToString();

                    fsYYMM = _dt.Rows[_iCount]["YYMM"].ToString();
                }
                else
                {
                    this.YYMM.Text = "";
                }

                dGIBUNAMT    = double.Parse(_dt.Rows[_iCount]["GIBUNAMT"].ToString());
                dBONUSAMT   = double.Parse(_dt.Rows[_iCount]["BONUSAMT"].ToString());
                dTIMESIDEAMT = double.Parse(_dt.Rows[_iCount]["TIMESIDEAMT"].ToString());
                dWORKAMT     = double.Parse(_dt.Rows[_iCount]["WORKAMT"].ToString());
                dOTAMT       = double.Parse(_dt.Rows[_iCount]["OTAMT"].ToString());
                dGIGUBHAP    = double.Parse(_dt.Rows[_iCount]["GIGUBHAP"].ToString());
                dNOJOAMT     = double.Parse(_dt.Rows[_iCount]["NOJOAMT"].ToString());
                dSANGJOAMT   = double.Parse(_dt.Rows[_iCount]["SANGJOAMT"].ToString());
                dGOYOUNGAMT  = double.Parse(_dt.Rows[_iCount]["GOYOUNGAMT"].ToString());
                dGONGJAEHAP  = double.Parse(_dt.Rows[_iCount]["GONGJAEHAP"].ToString());
                dCHAINHAP    = double.Parse(_dt.Rows[_iCount]["CHAINHAP"].ToString());
                dSOGUBAMT    = double.Parse(_dt.Rows[_iCount]["SOGUBAMT"].ToString());


                this.GIBUNAMT.Text    = string.Format("{0:#,##0}", dGIBUNAMT);
                this.BONUSAMT.Text    = string.Format("{0:#,##0}", dBONUSAMT);
                this.TIMESIDEAMT.Text = string.Format("{0:#,##0}", dTIMESIDEAMT);
                this.WORKAMT.Text     = string.Format("{0:#,##0}", dWORKAMT);
                this.OTAMT.Text       = string.Format("{0:#,##0}", dOTAMT);
                this.GIGUBHAP.Text    = string.Format("{0:#,##0}", dGIGUBHAP);
                this.SOGUBAMT.Text    = string.Format("{0:#,##0}", dSOGUBAMT);

            }

            _iCount++;

            _iRecordCnt++;

            this.line7.Visible = false;

            if (_iRecordCnt == 22)
            {
                this.line7.Visible = true;

                _iRecordCnt = 1;
            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            _iRecordCnt++;

            this.line10.LineWeight = 1;

            this.line10.Visible = true;

            this.line7.Visible = false;

            if (_iRecordCnt == 22)
            {
                this.line7.Visible = false;

                this.line10.LineWeight = 3;

                _iRecordCnt = 1;

                fsChange = "Group";
            }

            this.TITLE.Text          = "소급 합계";
            this.GIBUNAMTHAP.Text    = string.Format("{0:#,##0}", dGIBUNAMT_HAP1 - dGIBUNAMT_HAP2);
            this.BONUSAMTHAP.Text = string.Format("{0:#,##0}", dBONUSAMT_HAP1 - dBONUSAMT_HAP2);
            this.TIMESIDEAMTHAP.Text = string.Format("{0:#,##0}", dTIMESIDEAMT_HAP1 - dTIMESIDEAMT_HAP2);
            this.WORKAMTHAP.Text     = string.Format("{0:#,##0}", dWORKAMT_HAP1 - dWORKAMT_HAP2);
            this.OTAMTHAP.Text       = string.Format("{0:#,##0}", dOTAMT_HAP1 - dOTAMT_HAP2);
            this.SOGIGUBHAP.Text     = string.Format("{0:#,##0}", dGIGUBHAP_HAP1 - dGIGUBHAP_HAP2);
            this.SOGUBAMTHAP.Text    = string.Format("{0:#,##0}", dSOGUBAMT_HAP1 - dSOGUBAMT_HAP2);

            dGIBUNAMT_TOT1 = dGIBUNAMT_TOT1 + dGIBUNAMT_HAP1;
            dBONUSAMT_TOT1 = dBONUSAMT_TOT1 + dBONUSAMT_HAP1;
            dTIMESIDEAMT_TOT1 = dTIMESIDEAMT_TOT1 + dTIMESIDEAMT_HAP1;
            dWORKAMT_TOT1 = dWORKAMT_TOT1 + dWORKAMT_HAP1;
            dOTAMT_TOT1 = dOTAMT_TOT1 + dOTAMT_HAP1;
            dGIGUBHAP_TOT1 = dGIGUBHAP_TOT1  + dGIGUBHAP_HAP1;
            dNOJOAMT_TOT1     = dNOJOAMT_HAP1;
            dSANGJOAMT_TOT1   = dSANGJOAMT_HAP1;
            dGOYOUNGAMT_TOT1  = dGOYOUNGAMT_HAP1;
            dGONGJAEHAP_TOT1  = dGONGJAEHAP_HAP1;
            dCHAINHAP_TOT1    = dCHAINHAP_HAP1;
            dSOGUBAMT_TOT1    = dSOGUBAMT_TOT1 + dSOGUBAMT_HAP1;

            dGIBUNAMT_TOT2 = dGIBUNAMT_TOT2 + dGIBUNAMT_HAP2;
            dBONUSAMT_TOT2 = dBONUSAMT_TOT2 + dBONUSAMT_HAP2;
            dTIMESIDEAMT_TOT2 = dTIMESIDEAMT_TOT2 + dTIMESIDEAMT_HAP2;
            dWORKAMT_TOT2 = dWORKAMT_TOT2 + dWORKAMT_HAP2;
            dOTAMT_TOT2 = dOTAMT_TOT2 + dOTAMT_HAP2;
            dGIGUBHAP_TOT2 = dGIGUBHAP_TOT2 + dGIGUBHAP_HAP2;
            dNOJOAMT_TOT2     = dNOJOAMT_HAP2;
            dSANGJOAMT_TOT2   = dSANGJOAMT_HAP2;
            dGOYOUNGAMT_TOT2  = dGOYOUNGAMT_HAP2;
            dGONGJAEHAP_TOT2  = dGONGJAEHAP_HAP2;
            dCHAINHAP_TOT2    = dCHAINHAP_HAP2;
            dSOGUBAMT_TOT2    = dSOGUBAMT_TOT2  + dSOGUBAMT_HAP2;

            dGIBUNAMT_HAP1    = 0;
            dBONUSAMT_HAP1 = 0;
            dTIMESIDEAMT_HAP1 = 0;
            dWORKAMT_HAP1     = 0;
            dOTAMT_HAP1       = 0;
            dGIGUBHAP_HAP1    = 0;
            dNOJOAMT_HAP1     = 0;
            dSANGJOAMT_HAP1   = 0;
            dGOYOUNGAMT_HAP1  = 0;
            dGONGJAEHAP_HAP1  = 0;
            dCHAINHAP_HAP1    = 0;
            dSOGUBAMT_HAP1    = 0;

            dGIBUNAMT_HAP2    = 0;
            dBONUSAMT_HAP2 = 0;
            dTIMESIDEAMT_HAP2 = 0;
            dWORKAMT_HAP2     = 0;
            dOTAMT_HAP2       = 0;
            dGIGUBHAP_HAP2    = 0;
            dNOJOAMT_HAP2     = 0;
            dSANGJOAMT_HAP2   = 0;
            dGOYOUNGAMT_HAP2  = 0;
            dGONGJAEHAP_HAP2  = 0;
            dCHAINHAP_HAP2    = 0;
            dSOGUBAMT_HAP2    = 0;
        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            this.line7.Visible = false;

            this.TITLE_TOT.Text      = "소급 총합계";
            this.GIBUNAMTTOT.Text    = string.Format("{0:#,##0}", dGIBUNAMT_TOT1 - dGIBUNAMT_TOT2);
            this.BONUSAMTTOT.Text = string.Format("{0:#,##0}", dBONUSAMT_TOT1 - dBONUSAMT_TOT2);
            this.TIMESIDEAMTTOT.Text = string.Format("{0:#,##0}", dTIMESIDEAMT_TOT1 - dTIMESIDEAMT_TOT2);
            this.WORKAMTTOT.Text     = string.Format("{0:#,##0}", dWORKAMT_TOT1 - dWORKAMT_TOT2);
            this.OTAMTTOT.Text       = string.Format("{0:#,##0}", dOTAMT_TOT1 - dOTAMT_TOT2);
            this.SOGIGUBTOT.Text     = string.Format("{0:#,##0}", dGIGUBHAP_TOT1 - dGIGUBHAP_TOT2);
            this.SOGUBAMTTOT.Text    = string.Format("{0:#,##0}", dSOGUBAMT_TOT1 - dSOGUBAMT_TOT2);
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
            if (fsChange == "")
            {
                this.line7.Visible = true;
            }
            else
            {
                this.line7.Visible = false;

                fsChange = "";
            }
        }
    }
}