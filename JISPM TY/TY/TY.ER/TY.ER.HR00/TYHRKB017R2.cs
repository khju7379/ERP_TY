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

namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRKB017R2.
    /// </summary>
    public partial class TYHRKB017R2 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        DataTable _dtM = new DataTable();
        DataTable _dtS = new DataTable();
        DataTable _dtY = new DataTable();
        DataTable _dtBeSu = new DataTable();

        public TYHRKB017R2(DataTable dtM, DataTable dtS, DataTable dtY, DataTable dtBeSu)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _dtM = dtM;
            _dtS = dtS;
            _dtY = dtY;
            _dtBeSu = dtBeSu;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            string sPSYYMMDD = string.Empty;
            string sPSSERVPAYAMTYY = string.Empty;

            string sTextStrY = "";
            string sTextStrM = "";
            string sTextStrD = "";
            string sBAESU = "";
            string sWkComDate = "";

            this.PSWKDATE.Text = UP_DateChange(_dt.Rows[0]["PSWKSDATE"].ToString()) + " ~ " + UP_DateChange(_dt.Rows[0]["PSWKEDATE"].ToString());

            if (_dt.Rows[0]["PSYEAR"].ToString() != "0")
            {
                sPSYYMMDD = _dt.Rows[0]["PSYEAR"].ToString() + "년 ";
            }
            if (_dt.Rows[0]["PSMONTH"].ToString() != "0")
            {
                sPSYYMMDD += _dt.Rows[0]["PSMONTH"].ToString() + "개월 ";
            }
            if (_dt.Rows[0]["PSDAY"].ToString() != "0")
            {
                sPSYYMMDD += _dt.Rows[0]["PSDAY"].ToString() + "일 ";
            }

            this.PSYYMMDD.Text = "( " + sPSYYMMDD + ")";

            if (_dtBeSu.Rows.Count > 0)
            {
                for (int i = 0; i < _dtBeSu.Rows.Count; i++)
                {
                    sTextStrY = Convert.ToInt16(_dtBeSu.Rows[i]["PXYEAR"].ToString()) > 0 ? _dtBeSu.Rows[i]["PXYEAR"].ToString() + "년 " : "    ";
                    sTextStrM = Convert.ToInt16(_dtBeSu.Rows[i]["PXMONTH"].ToString()) > 0 ? _dtBeSu.Rows[i]["PXMONTH"].ToString() + "개월 " : "    ";
                    sTextStrD = Convert.ToInt16(_dtBeSu.Rows[i]["PXDAY"].ToString()) > 0 ? _dtBeSu.Rows[i]["PXDAY"].ToString() + "일 " : "    ";
                    sBAESU = Convert.ToDouble(_dtBeSu.Rows[i]["PXRATENUM"].ToString()) > 0 ? _dtBeSu.Rows[i]["PXRATENUM"].ToString() + " 배수 " : "";

                    sWkComDate = "   ( " + Set_Date(_dtBeSu.Rows[i]["PXOVSDATE"].ToString()) + " ~ " + Set_Date(_dtBeSu.Rows[i]["PXOVEDATE"].ToString()) + " )";

                    sPSSERVPAYAMTYY += string.Format("{0:#,###}", double.Parse(_dt.Rows[0]["PSAVGTOTAL"].ToString())) + " X " + sTextStrY + sTextStrM + sTextStrD + " X " + sBAESU + sWkComDate + "\n";
                }
            }

            UP_setM();
            UP_setS();
            UP_setY();

            PSSERVPAYAMTYY.Text = sPSSERVPAYAMTYY;
        }

        #region Description : 급여내역
        private void UP_setM()
        {
            int icount = _dtM.Rows.Count;

            string sSdate = string.Empty;
            string sEdate = string.Empty;

            if (icount > 0)
            {
                PKPAYYYMM1.Text = UP_DateChange(_dtM.Rows[0]["PKPAYYYMM"].ToString());
                PKPAYAMOUNT1.Text = string.Format("{0:#,###}", double.Parse(_dtM.Rows[0]["PKPAYAMOUNT"].ToString()));

                sSdate = _dtM.Rows[0]["PKPAYYYMM"].ToString();
                sEdate = _dtM.Rows[0]["PKPAYYYMM"].ToString();
            }
            if (icount > 1)
            {
                PKPAYYYMM2.Text = UP_DateChange(_dtM.Rows[1]["PKPAYYYMM"].ToString());
                PKPAYAMOUNT2.Text = string.Format("{0:#,###}", double.Parse(_dtM.Rows[1]["PKPAYAMOUNT"].ToString()));
                sEdate = _dtM.Rows[1]["PKPAYYYMM"].ToString();
            }
            if (icount > 2)
            {
                PKPAYYYMM3.Text = UP_DateChange(_dtM.Rows[2]["PKPAYYYMM"].ToString());
                PKPAYAMOUNT3.Text = string.Format("{0:#,###}", double.Parse(_dtM.Rows[2]["PKPAYAMOUNT"].ToString()));
                sEdate = _dtM.Rows[2]["PKPAYYYMM"].ToString();
            }
            M1DATE.Text = "(" + UP_DateChange(sSdate) + "~" + UP_DateChange(sEdate) + ")";
        }
        #endregion

        #region Description : 상여내역
        private void UP_setS()
        {
            int icount = _dtS.Rows.Count;

            string sSdate = string.Empty;
            string sEdate = string.Empty;

            double dAMTTOTAL = 0;

            if (icount > 0)
            {
                PKPAYGUBNNM1.Text = _dtS.Rows[0]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS1.Text = UP_DateChange(_dtS.Rows[0]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS1.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[0]["PKPAYAMOUNT"].ToString()));
                PKMEMO1.Text = _dtS.Rows[0]["PKMEMO"].ToString();
                dAMTTOTAL = double.Parse(_dtS.Rows[0]["PKPAYAMOUNT"].ToString());
                sSdate = _dtS.Rows[0]["PKPAYYYMM"].ToString();
                sEdate = _dtS.Rows[0]["PKPAYYYMM"].ToString();
            }
            if (icount > 1)
            {
                PKPAYGUBNNM2.Text = _dtS.Rows[1]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS2.Text = UP_DateChange(_dtS.Rows[1]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS2.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[1]["PKPAYAMOUNT"].ToString()));
                PKMEMO2.Text = _dtS.Rows[1]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[1]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[1]["PKPAYYYMM"].ToString();
            }
            if (icount > 2)
            {
                PKPAYGUBNNM3.Text = _dtS.Rows[2]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS3.Text = UP_DateChange(_dtS.Rows[2]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS3.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[2]["PKPAYAMOUNT"].ToString()));
                PKMEMO3.Text = _dtS.Rows[2]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[2]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[2]["PKPAYYYMM"].ToString();
            }
            if (icount > 3)
            {
                PKPAYGUBNNM4.Text = _dtS.Rows[3]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS4.Text = UP_DateChange(_dtS.Rows[3]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS4.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[3]["PKPAYAMOUNT"].ToString()));
                PKMEMO4.Text = _dtS.Rows[3]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[3]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[3]["PKPAYYYMM"].ToString();
            }
            if (icount > 4)
            {
                PKPAYGUBNNM5.Text = _dtS.Rows[4]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS5.Text = UP_DateChange(_dtS.Rows[4]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS5.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[4]["PKPAYAMOUNT"].ToString()));
                PKMEMO5.Text = _dtS.Rows[4]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[4]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[4]["PKPAYYYMM"].ToString();
            }
            if (icount > 5)
            {
                PKPAYGUBNNM6.Text = _dtS.Rows[5]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS6.Text = UP_DateChange(_dtS.Rows[5]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS6.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[5]["PKPAYAMOUNT"].ToString()));
                PKMEMO6.Text = _dtS.Rows[5]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[5]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[5]["PKPAYYYMM"].ToString();
            }
            if (icount > 6)
            {
                PKPAYGUBNNM7.Text = _dtS.Rows[6]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS7.Text = UP_DateChange(_dtS.Rows[6]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS7.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[6]["PKPAYAMOUNT"].ToString()));
                PKMEMO7.Text = _dtS.Rows[6]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[6]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[6]["PKPAYYYMM"].ToString();
            }
            if (icount > 7)
            {
                PKPAYGUBNNM8.Text = _dtS.Rows[7]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS8.Text = UP_DateChange(_dtS.Rows[7]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS8.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[7]["PKPAYAMOUNT"].ToString()));
                PKMEMO8.Text = _dtS.Rows[7]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[7]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[7]["PKPAYYYMM"].ToString();
            }
            if (icount > 8)
            {
                PKPAYGUBNNM9.Text = _dtS.Rows[8]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS9.Text = UP_DateChange(_dtS.Rows[8]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS9.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[8]["PKPAYAMOUNT"].ToString()));
                PKMEMO9.Text = _dtS.Rows[8]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[8]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[8]["PKPAYYYMM"].ToString();
            }
            if (icount > 9)
            {
                PKPAYGUBNNM10.Text = _dtS.Rows[9]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS10.Text = UP_DateChange(_dtS.Rows[9]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS10.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[9]["PKPAYAMOUNT"].ToString()));
                PKMEMO10.Text = _dtS.Rows[9]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[9]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[9]["PKPAYYYMM"].ToString();
            }
            if (icount > 10)
            {
                PKPAYGUBNNM11.Text = _dtS.Rows[10]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS11.Text = UP_DateChange(_dtS.Rows[10]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS11.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[10]["PKPAYAMOUNT"].ToString()));
                PKMEMO11.Text = _dtS.Rows[10]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[10]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[10]["PKPAYYYMM"].ToString();
            }
            if (icount > 11)
            {
                PKPAYGUBNNM12.Text = _dtS.Rows[11]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS12.Text = UP_DateChange(_dtS.Rows[11]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS12.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[11]["PKPAYAMOUNT"].ToString()));
                PKMEMO12.Text = _dtS.Rows[11]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[11]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[11]["PKPAYYYMM"].ToString();
            }
            if (icount > 12)
            {
                PKPAYGUBNNM13.Text = _dtS.Rows[12]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS13.Text = UP_DateChange(_dtS.Rows[12]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS13.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[12]["PKPAYAMOUNT"].ToString()));
                PKMEMO13.Text = _dtS.Rows[12]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[12]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[12]["PKPAYYYMM"].ToString();
            }
            if (icount > 13)
            {
                PKPAYGUBNNM14.Text = _dtS.Rows[13]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS14.Text = UP_DateChange(_dtS.Rows[13]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS14.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[13]["PKPAYAMOUNT"].ToString()));
                PKMEMO14.Text = _dtS.Rows[13]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[13]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[13]["PKPAYYYMM"].ToString();
            }
            if (icount > 14)
            {
                PKPAYGUBNNM15.Text = _dtS.Rows[14]["PKPAYGUBNNM"].ToString();
                PKPAYYYMMS15.Text = UP_DateChange(_dtS.Rows[14]["PKPAYYYMM"].ToString());
                PKPAYAMOUNTS15.Text = string.Format("{0:#,###}", double.Parse(_dtS.Rows[14]["PKPAYAMOUNT"].ToString()));
                PKMEMO15.Text = _dtS.Rows[14]["PKMEMO"].ToString();
                dAMTTOTAL += double.Parse(_dtS.Rows[14]["PKPAYAMOUNT"].ToString());
                sEdate = _dtS.Rows[14]["PKPAYYYMM"].ToString();
            }

            SDATE.Text = "(" + UP_DateChange(sSdate) + "~" + UP_DateChange(sEdate) + ")";

            if (dAMTTOTAL > 0)
            {
                PSAVGBONUSAMT.Text = string.Format("{0:#,###}", dAMTTOTAL) + " / 12 = " + string.Format("{0:#,###}", double.Parse(_dt.Rows[0]["PSAVGBONUS"].ToString()));
            }
        }
        #endregion

        #region Description : 연차내역
        private void UP_setY()
        {
            int icount = _dtY.Rows.Count;

            if (icount > 0)
            {

                //PKPAYAMOUNTY.Text = string.Format("{0:#,###}", double.Parse(_dtY.Rows[0]["PKPAYAMOUNT"].ToString()));

                //PKPAYYYY.Text = "(" + _dtY.Rows[0]["PKPAYYYMM"].ToString().Substring(0, 4) + ")";
            }
            else
            {
                label18.Text = "";
            }
        }
        #endregion

        #region Description : 날짜형식 변환
        private string UP_DateChange(string sDate)
        {
            string rtnDate = string.Empty;

            if (sDate.Length == 8)
            {
                rtnDate = sDate.Substring(0, 4) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(6, 2);
            }
            else if (sDate.Length == 6)
            {
                rtnDate = sDate.Substring(0, 4) + "/" + sDate.Substring(4, 2);
            }

            return rtnDate;
        }
        #endregion

        #region Description : 계산 및 원단위 절사
        private double UP_COMP(double dAMOUNT, int iDiv)
        {
            string sAMT = string.Empty;
            double dAMT = 0;

            if (iDiv != 0)
            {
                dAMT = double.Parse(string.Format("{0:###0}", dAMOUNT)) / iDiv;
                dAMT = dAMT / 10;

                sAMT = UP_DotDelete(Convert.ToString(dAMT));

                dAMT = double.Parse(sAMT) * 10;
            }
            else
            {
                dAMT = dAMOUNT;
            }

            return dAMT;
        }
        #endregion

        #region Description : 소수점 이하 절삭하는 함수
        protected string UP_DotDelete(string sStr)
        {
            string sValue = "";
            for (int i = 0; i < sStr.Length; i++)
            {
                if (sStr.Substring(i, 1) != ".")
                {
                    sValue = sValue + sStr.Substring(i, 1);
                }
                else
                {
                    break;
                }
            }
            return sValue;
        }
        #endregion

        #region Description : 날짜 포맷 맞추기
        protected string Set_Date(string sStr)
        {
            if (sStr.Length == 8)
            {
                sStr = sStr.Substring(0, 4) + "-" + sStr.Substring(4, 2) + "-" + sStr.Substring(6, 2);
            }
            else
            {
                sStr = "";
            }
            return sStr;
        }
        #endregion
    }
}
