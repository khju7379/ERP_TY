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
    /// Summary description for TYHRKB016R.
    /// </summary>
    public partial class TYHRKB016R : DataDynamics.ActiveReports.ActiveReport
    {

        private DataTable _dtGJ = new DataTable();
        private DataTable _dtHL = new DataTable();
        private DataTable _dtGB = new DataTable();
        private DataTable _dtKL = new DataTable();
        private DataTable _dtJK = new DataTable();
        private DataTable _dtPR = new DataTable();
        private DataTable _dtSB = new DataTable();
        private DataTable _dtGY = new DataTable();
        private DataTable _dtBL = new DataTable();
        private DataTable _dtSJ = new DataTable();
        private string fsAge = string.Empty;
        private byte[] _bIMG = null;

        public TYHRKB016R(DataTable dtGJ, DataTable dtHL, DataTable dtGB, DataTable dtKL, DataTable dtJK, 
                          DataTable dtPR, DataTable dtSB, DataTable dtGY, DataTable dtBL, DataTable dtSJ, string sAge, byte[] bIMG)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _dtGJ = dtGJ;
            _dtHL = dtHL;
            _dtGB = dtGB;
            _dtKL = dtKL;
            _dtJK = dtJK;
            _dtPR = dtPR;
            _dtSB = dtSB;
            _dtGY = dtGY;
            _dtBL = dtBL;
            _dtSJ = dtSJ;
            fsAge = sAge;
            _bIMG = bIMG;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            setGJ();  //가족사항
            setHL();  //학력사항
            setGB();  //병역사항
            setKL();  //경력사항
            setJK();  //자격사항
            setPR();  //포상사항
            setSB();  //징계사항
            setGY();  //교육사항
            setBL();  //발령사항
            setSJ();  //승진사항

            if (_bIMG != null)
            {
                System.IO.Stream stream1 = new System.IO.MemoryStream(_bIMG);
                Picture.Image = Bitmap.FromStream(stream1);
            }

            DataTable dt = this.DataSource as DataTable;

            KBHANGL.Text = dt.Rows[0]["KBHANGL"].ToString() + "(당 " + fsAge + "세)";
            KBJUMIN.Text = dt.Rows[0]["KBJUMIN"].ToString().Substring(0, 6) + "-" + dt.Rows[0]["KBJUMIN"].ToString().Substring(6, 7);
            KBIDATE.Text = UP_ChangeDate("-", dt.Rows[0]["KBIDATE"].ToString());
        }

        #region Description : 가족사항 입력
        private void setGJ()
        {
            int iGJCount = _dtGJ.Rows.Count;

            if (iGJCount > 0)
            {
                GJNAME1.Text = _dtGJ.Rows[0]["GJNAME"].ToString();
                GJCODENM1.Text = _dtGJ.Rows[0]["GJCODENM"].ToString();
                BIRTHDAY1.Text = _dtGJ.Rows[0]["BIRTHDAY"].ToString();
                AGE1.Text = _dtGJ.Rows[0]["AGE"].ToString();
                GJHAKLK1.Text = _dtGJ.Rows[0]["GJHAKLK"].ToString();
                //GJJIKUP1.Text = _dtGJ.Rows[0]["GJJIKUP"].ToString();
            }
            if (iGJCount > 1)
            {
                GJNAME2.Text = _dtGJ.Rows[1]["GJNAME"].ToString();
                GJCODENM2.Text = _dtGJ.Rows[1]["GJCODENM"].ToString();
                BIRTHDAY2.Text = _dtGJ.Rows[1]["BIRTHDAY"].ToString();
                AGE2.Text = _dtGJ.Rows[1]["AGE"].ToString();
                GJHAKLK2.Text = _dtGJ.Rows[1]["GJHAKLK"].ToString();
                //GJJIKUP2.Text = _dtGJ.Rows[0]["GJJIKUP"].ToString();
            }
            if (iGJCount > 2)
            {
                GJNAME3.Text = _dtGJ.Rows[2]["GJNAME"].ToString();
                GJCODENM3.Text = _dtGJ.Rows[2]["GJCODENM"].ToString();
                BIRTHDAY3.Text = _dtGJ.Rows[2]["BIRTHDAY"].ToString();
                AGE3.Text = _dtGJ.Rows[2]["AGE"].ToString();
                GJHAKLK3.Text = _dtGJ.Rows[2]["GJHAKLK"].ToString();
                //GJJIKUP3.Text = _dtGJ.Rows[0]["GJJIKUP"].ToString();
            }
            if (iGJCount > 3)
            {
                GJNAME4.Text = _dtGJ.Rows[3]["GJNAME"].ToString();
                GJCODENM4.Text = _dtGJ.Rows[3]["GJCODENM"].ToString();
                BIRTHDAY4.Text = _dtGJ.Rows[3]["BIRTHDAY"].ToString();
                AGE4.Text = _dtGJ.Rows[3]["AGE"].ToString();
                GJHAKLK4.Text = _dtGJ.Rows[3]["GJHAKLK"].ToString();
                //GJJIKUP4.Text = _dtGJ.Rows[0]["GJJIKUP"].ToString();
            }
            if (iGJCount > 4)
            {
                GJNAME5.Text = _dtGJ.Rows[4]["GJNAME"].ToString();
                GJCODENM5.Text = _dtGJ.Rows[4]["GJCODENM"].ToString();
                BIRTHDAY5.Text = _dtGJ.Rows[4]["BIRTHDAY"].ToString();
                AGE5.Text = _dtGJ.Rows[4]["AGE"].ToString();
                GJHAKLK5.Text = _dtGJ.Rows[4]["GJHAKLK"].ToString();
                //GJJIKUP5.Text = _dtGJ.Rows[0]["GJJIKUP"].ToString();
            }
            if (iGJCount > 5)
            {
                GJNAME6.Text = _dtGJ.Rows[5]["GJNAME"].ToString();
                GJCODENM6.Text = _dtGJ.Rows[5]["GJCODENM"].ToString();
                BIRTHDAY6.Text = _dtGJ.Rows[5]["BIRTHDAY"].ToString();
                AGE6.Text = _dtGJ.Rows[5]["AGE"].ToString();
                GJHAKLK6.Text = _dtGJ.Rows[5]["GJHAKLK"].ToString();
                //GJJIKUP6.Text = _dtGJ.Rows[0]["GJJIKUP"].ToString();
            }
            if (iGJCount > 6)
            {
                GJNAME7.Text = _dtGJ.Rows[6]["GJNAME"].ToString();
                GJCODENM7.Text = _dtGJ.Rows[6]["GJCODENM"].ToString();
                BIRTHDAY7.Text = _dtGJ.Rows[6]["BIRTHDAY"].ToString();
                AGE7.Text = _dtGJ.Rows[6]["AGE"].ToString();
                GJHAKLK7.Text = _dtGJ.Rows[6]["GJHAKLK"].ToString();
                //GJJIKUP7.Text = _dtGJ.Rows[0]["GJJIKUP"].ToString();
            }
        }
        #endregion

        #region Description : 학력사항 입력
        private void setHL()
        {
            int iHLCount = _dtHL.Rows.Count;

            if (iHLCount >= 3)
            {
                HLHAKKYONM1.Text = _dtHL.Rows[0]["HLHAKKYONM"].ToString();
                HLHAKKYONM2.Text = _dtHL.Rows[1]["HLHAKKYONM"].ToString();
                HLHAKKYONM3.Text = _dtHL.Rows[2]["HLHAKKYONM"].ToString();

                HLIDATE1.Text = _dtHL.Rows[0]["HLIDATE"].ToString() + "~" + _dtHL.Rows[0]["HLJDATE"].ToString();
                HLIDATE2.Text = _dtHL.Rows[1]["HLIDATE"].ToString() + "~" + _dtHL.Rows[1]["HLJDATE"].ToString();
                HLIDATE3.Text = _dtHL.Rows[2]["HLIDATE"].ToString() + "~" + _dtHL.Rows[2]["HLJDATE"].ToString();

                HLHAKGANM1.Text = _dtHL.Rows[0]["HLHAKGANM"].ToString();
                HLHAKGANM2.Text = _dtHL.Rows[1]["HLHAKGANM"].ToString();
                HLHAKGANM3.Text = _dtHL.Rows[2]["HLHAKGANM"].ToString();

                HLJUGUBNNM1.Text = _dtHL.Rows[0]["HLJUGUBNNM"].ToString();
                HLJUGUBNNM2.Text = _dtHL.Rows[1]["HLJUGUBNNM"].ToString();
                HLJUGUBNNM3.Text = _dtHL.Rows[2]["HLJUGUBNNM"].ToString();
            }
            else
            {
                switch (iHLCount)
                {
                    case 1: 
                        HLHAKKYONM1.Text = _dtHL.Rows[0]["HLHAKKYONM"].ToString();
                        HLIDATE1.Text = _dtHL.Rows[0]["HLIDATE"].ToString() + "~" + _dtHL.Rows[0]["HLJDATE"].ToString();
                        HLHAKGANM1.Text = _dtHL.Rows[0]["HLHAKGANM"].ToString();
                        HLJUGUBNNM1.Text = _dtHL.Rows[0]["HLJUGUBNNM"].ToString();
                        break;
                    case 2: 
                        HLHAKKYONM1.Text = _dtHL.Rows[0]["HLHAKKYONM"].ToString();
                        HLHAKKYONM2.Text = _dtHL.Rows[1]["HLHAKKYONM"].ToString();

                        HLIDATE1.Text = _dtHL.Rows[0]["HLIDATE"].ToString() + "~" + _dtHL.Rows[0]["HLJDATE"].ToString();
                        HLIDATE2.Text = _dtHL.Rows[1]["HLIDATE"].ToString() + "~" + _dtHL.Rows[1]["HLJDATE"].ToString();

                        HLHAKGANM1.Text = _dtHL.Rows[0]["HLHAKGANM"].ToString();
                        HLHAKGANM2.Text = _dtHL.Rows[1]["HLHAKGANM"].ToString();

                        HLJUGUBNNM1.Text = _dtHL.Rows[0]["HLJUGUBNNM"].ToString();
                        HLJUGUBNNM2.Text = _dtHL.Rows[1]["HLJUGUBNNM"].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Description : 병역사항 입력
        private void setGB()
        {
            int iGBCount = _dtGB.Rows.Count;

            if (iGBCount > 0)
            {
                GBYJCODENM.Text = _dtGB.Rows[0]["GBYJCODENM"].ToString();
                GBGBCODENM.Text = _dtGB.Rows[0]["GBGBCODENM"].ToString();
                GBBKCODENM.Text = _dtGB.Rows[0]["GBBKCODENM"].ToString();
                GBGKCODENM.Text = _dtGB.Rows[0]["GBGKCODENM"].ToString();
                GBIDATE.Text = UP_ChangeDate("-", _dtGB.Rows[0]["GBIDATE"].ToString());
                GBJDATE.Text = UP_ChangeDate("-", _dtGB.Rows[0]["GBJDATE"].ToString());
                GBJDGUBNNM.Text = _dtGB.Rows[0]["GBJDGUBNNM"].ToString();
            }
        }
        #endregion

        #region Description : 경력사항 입력
        private void setKL()
        {
            int iKLCount = _dtKL.Rows.Count;

            if (iKLCount >= 4)
            {
                KLIDATE1.Text = _dtKL.Rows[0]["KLIDATE"].ToString() + "~" + _dtKL.Rows[0]["KLJDATE"].ToString();
                KLIDATE2.Text = _dtKL.Rows[1]["KLIDATE"].ToString() + "~" + _dtKL.Rows[1]["KLJDATE"].ToString();
                KLIDATE3.Text = _dtKL.Rows[2]["KLIDATE"].ToString() + "~" + _dtKL.Rows[2]["KLJDATE"].ToString();
                KLIDATE4.Text = _dtKL.Rows[3]["KLIDATE"].ToString() + "~" + _dtKL.Rows[3]["KLJDATE"].ToString();

                KLJIKJGNG1.Text = _dtKL.Rows[0]["KLJIKJGNG"].ToString();
                KLJIKJGNG2.Text = _dtKL.Rows[1]["KLJIKJGNG"].ToString();
                KLJIKJGNG3.Text = _dtKL.Rows[2]["KLJIKJGNG"].ToString();
                KLJIKJGNG4.Text = _dtKL.Rows[3]["KLJIKJGNG"].ToString();

                KLJIKWI1.Text = _dtKL.Rows[0]["KLJIKWI"].ToString();
                KLJIKWI2.Text = _dtKL.Rows[1]["KLJIKWI"].ToString();
                KLJIKWI3.Text = _dtKL.Rows[2]["KLJIKWI"].ToString();
                KLJIKWI4.Text = _dtKL.Rows[3]["KLJIKWI"].ToString();

                KLUPMU1.Text = _dtKL.Rows[0]["KLUPMU"].ToString();
                KLUPMU2.Text = _dtKL.Rows[1]["KLUPMU"].ToString();
                KLUPMU3.Text = _dtKL.Rows[2]["KLUPMU"].ToString();
                KLUPMU4.Text = _dtKL.Rows[3]["KLUPMU"].ToString();
            }
            else
            {
                switch (iKLCount)
                {
                    case 1:
                        KLIDATE1.Text = _dtKL.Rows[0]["KLIDATE"].ToString() + "~" + _dtKL.Rows[0]["KLJDATE"].ToString();
                        KLJIKJGNG1.Text = _dtKL.Rows[0]["KLJIKJGNG"].ToString();
                        KLJIKWI1.Text = _dtKL.Rows[0]["KLJIKWI"].ToString();
                        KLUPMU1.Text = _dtKL.Rows[0]["KLUPMU"].ToString();
                        break;
                    case 2:
                        KLIDATE1.Text = _dtKL.Rows[0]["KLIDATE"].ToString() + "~" + _dtKL.Rows[0]["KLJDATE"].ToString();
                        KLIDATE2.Text = _dtKL.Rows[1]["KLIDATE"].ToString() + "~" + _dtKL.Rows[1]["KLJDATE"].ToString();

                        KLJIKJGNG1.Text = _dtKL.Rows[0]["KLJIKJGNG"].ToString();
                        KLJIKJGNG2.Text = _dtKL.Rows[1]["KLJIKJGNG"].ToString();

                        KLJIKWI1.Text = _dtKL.Rows[0]["KLJIKWI"].ToString();
                        KLJIKWI2.Text = _dtKL.Rows[1]["KLJIKWI"].ToString();

                        KLUPMU1.Text = _dtKL.Rows[0]["KLUPMU"].ToString();
                        KLUPMU2.Text = _dtKL.Rows[1]["KLUPMU"].ToString();
                        break;
                    case 3:
                        KLIDATE1.Text = _dtKL.Rows[0]["KLIDATE"].ToString() + "~" + _dtKL.Rows[0]["KLJDATE"].ToString();
                        KLIDATE2.Text = _dtKL.Rows[1]["KLIDATE"].ToString() + "~" + _dtKL.Rows[1]["KLJDATE"].ToString();
                        KLIDATE3.Text = _dtKL.Rows[2]["KLIDATE"].ToString() + "~" + _dtKL.Rows[2]["KLJDATE"].ToString();

                        KLJIKJGNG1.Text = _dtKL.Rows[0]["KLJIKJGNG"].ToString();
                        KLJIKJGNG2.Text = _dtKL.Rows[1]["KLJIKJGNG"].ToString();
                        KLJIKJGNG3.Text = _dtKL.Rows[2]["KLJIKJGNG"].ToString();

                        KLJIKWI1.Text = _dtKL.Rows[0]["KLJIKWI"].ToString();
                        KLJIKWI2.Text = _dtKL.Rows[1]["KLJIKWI"].ToString();
                        KLJIKWI3.Text = _dtKL.Rows[2]["KLJIKWI"].ToString();

                        KLUPMU1.Text = _dtKL.Rows[0]["KLUPMU"].ToString();
                        KLUPMU2.Text = _dtKL.Rows[1]["KLUPMU"].ToString();
                        KLUPMU3.Text = _dtKL.Rows[2]["KLUPMU"].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Description : 자격사항 입력
        private void setJK()
        {
            int iJKCount = _dtJK.Rows.Count;

            if (iJKCount >= 4)
            {
                JKDESC11.Text = _dtJK.Rows[0]["JKDESC1"].ToString();
                JKDESC12.Text = _dtJK.Rows[1]["JKDESC1"].ToString();
                JKDESC13.Text = _dtJK.Rows[2]["JKDESC1"].ToString();
                JKDESC14.Text = _dtJK.Rows[3]["JKDESC1"].ToString();

                JKIDATE1.Text = _dtJK.Rows[0]["JKIDATE"].ToString();
                JKIDATE2.Text = _dtJK.Rows[1]["JKIDATE"].ToString();
                JKIDATE3.Text = _dtJK.Rows[2]["JKIDATE"].ToString();
                JKIDATE4.Text = _dtJK.Rows[3]["JKIDATE"].ToString();

                JKBUNHO1.Text = _dtJK.Rows[0]["JKBUNHO"].ToString();
                JKBUNHO2.Text = _dtJK.Rows[1]["JKBUNHO"].ToString();
                JKBUNHO3.Text = _dtJK.Rows[2]["JKBUNHO"].ToString();
                JKBUNHO4.Text = _dtJK.Rows[3]["JKBUNHO"].ToString();

                JKSHHANG1.Text = _dtJK.Rows[0]["JKSHHANG"].ToString();
                JKSHHANG2.Text = _dtJK.Rows[1]["JKSHHANG"].ToString();
                JKSHHANG3.Text = _dtJK.Rows[2]["JKSHHANG"].ToString();
                JKSHHANG4.Text = _dtJK.Rows[3]["JKSHHANG"].ToString();
            }
            else
            {
                switch (iJKCount)
                {
                    case 1:
                        JKDESC11.Text = _dtJK.Rows[0]["JKDESC1"].ToString();
                        JKIDATE1.Text = _dtJK.Rows[0]["JKIDATE"].ToString();
                        JKBUNHO1.Text = _dtJK.Rows[0]["JKBUNHO"].ToString();
                        JKSHHANG1.Text = _dtJK.Rows[0]["JKSHHANG"].ToString();
                        break;
                    case 2:
                        JKDESC11.Text = _dtJK.Rows[0]["JKDESC1"].ToString();
                        JKDESC12.Text = _dtJK.Rows[1]["JKDESC1"].ToString();

                        JKIDATE1.Text = _dtJK.Rows[0]["JKIDATE"].ToString();
                        JKIDATE2.Text = _dtJK.Rows[1]["JKIDATE"].ToString();

                        JKBUNHO1.Text = _dtJK.Rows[0]["JKBUNHO"].ToString();
                        JKBUNHO2.Text = _dtJK.Rows[1]["JKBUNHO"].ToString();

                        JKSHHANG1.Text = _dtJK.Rows[0]["JKSHHANG"].ToString();
                        JKSHHANG2.Text = _dtJK.Rows[1]["JKSHHANG"].ToString();
                        break;
                    case 3:
                        JKDESC11.Text = _dtJK.Rows[0]["JKDESC1"].ToString();
                        JKDESC12.Text = _dtJK.Rows[1]["JKDESC1"].ToString();
                        JKDESC13.Text = _dtJK.Rows[2]["JKDESC1"].ToString();

                        JKIDATE1.Text = _dtJK.Rows[0]["JKIDATE"].ToString();
                        JKIDATE2.Text = _dtJK.Rows[1]["JKIDATE"].ToString();
                        JKIDATE3.Text = _dtJK.Rows[2]["JKIDATE"].ToString();

                        JKBUNHO1.Text = _dtJK.Rows[0]["JKBUNHO"].ToString();
                        JKBUNHO2.Text = _dtJK.Rows[1]["JKBUNHO"].ToString();
                        JKBUNHO3.Text = _dtJK.Rows[2]["JKBUNHO"].ToString();

                        JKSHHANG1.Text = _dtJK.Rows[0]["JKSHHANG"].ToString();
                        JKSHHANG2.Text = _dtJK.Rows[1]["JKSHHANG"].ToString();
                        JKSHHANG3.Text = _dtJK.Rows[2]["JKSHHANG"].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Description : 포상사항 입력
        private void setPR()
        {
            int iPRCount = _dtPR.Rows.Count;

            if (iPRCount >= 5)
            {
                PRDATE1.Text = UP_ChangeDate(".", _dtPR.Rows[0]["PRDATE"].ToString());
                PRDATE2.Text = UP_ChangeDate(".", _dtPR.Rows[1]["PRDATE"].ToString());
                PRDATE3.Text = UP_ChangeDate(".", _dtPR.Rows[2]["PRDATE"].ToString());
                PRDATE4.Text = UP_ChangeDate(".", _dtPR.Rows[3]["PRDATE"].ToString());
                PRDATE5.Text = UP_ChangeDate(".", _dtPR.Rows[4]["PRDATE"].ToString());

                PRDESC1.Text = _dtPR.Rows[0]["PRDESC"].ToString();
                PRDESC2.Text = _dtPR.Rows[1]["PRDESC"].ToString();
                PRDESC3.Text = _dtPR.Rows[2]["PRDESC"].ToString();
                PRDESC4.Text = _dtPR.Rows[3]["PRDESC"].ToString();
                PRDESC5.Text = _dtPR.Rows[4]["PRDESC"].ToString();
            }
            else
            {
                switch (iPRCount)
                {
                    case 1:
                        PRDATE1.Text = UP_ChangeDate(".",_dtPR.Rows[0]["PRDATE"].ToString());
                        PRDESC1.Text = _dtPR.Rows[0]["PRDESC"].ToString();
                        break;
                    case 2:
                        PRDATE1.Text = UP_ChangeDate(".", _dtPR.Rows[0]["PRDATE"].ToString());
                        PRDATE2.Text = UP_ChangeDate(".", _dtPR.Rows[1]["PRDATE"].ToString());

                        PRDESC1.Text = _dtPR.Rows[0]["PRDESC"].ToString();
                        PRDESC2.Text = _dtPR.Rows[1]["PRDESC"].ToString();
                        break;
                    case 3:
                        PRDATE1.Text = UP_ChangeDate(".", _dtPR.Rows[0]["PRDATE"].ToString());
                        PRDATE2.Text = UP_ChangeDate(".", _dtPR.Rows[1]["PRDATE"].ToString());
                        PRDATE3.Text = UP_ChangeDate(".", _dtPR.Rows[2]["PRDATE"].ToString());

                        PRDESC1.Text = _dtPR.Rows[0]["PRDESC"].ToString();
                        PRDESC2.Text = _dtPR.Rows[1]["PRDESC"].ToString();
                        PRDESC3.Text = _dtPR.Rows[2]["PRDESC"].ToString();
                        break;
                    case 4:
                        PRDATE1.Text = UP_ChangeDate(".", _dtPR.Rows[0]["PRDATE"].ToString());
                        PRDATE2.Text = UP_ChangeDate(".", _dtPR.Rows[1]["PRDATE"].ToString());
                        PRDATE3.Text = UP_ChangeDate(".", _dtPR.Rows[2]["PRDATE"].ToString());
                        PRDATE4.Text = UP_ChangeDate(".", _dtPR.Rows[3]["PRDATE"].ToString());

                        PRDESC1.Text = _dtPR.Rows[0]["PRDESC"].ToString();
                        PRDESC2.Text = _dtPR.Rows[1]["PRDESC"].ToString();
                        PRDESC3.Text = _dtPR.Rows[2]["PRDESC"].ToString();
                        PRDESC4.Text = _dtPR.Rows[3]["PRDESC"].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Description : 징계사항 입력
        private void setSB()
        {
            int iSBCount = _dtSB.Rows.Count;

            if (iSBCount >= 5)
            {
                SBDATE1.Text = UP_ChangeDate(".",_dtSB.Rows[0]["SBDATE"].ToString());
                SBDATE2.Text = UP_ChangeDate(".", _dtSB.Rows[1]["SBDATE"].ToString());
                SBDATE3.Text = UP_ChangeDate(".", _dtSB.Rows[2]["SBDATE"].ToString());
                SBDATE4.Text = UP_ChangeDate(".", _dtSB.Rows[3]["SBDATE"].ToString());
                SBDATE5.Text = UP_ChangeDate(".", _dtSB.Rows[4]["SBDATE"].ToString());

                SBGUBUNNM1.Text = _dtSB.Rows[0]["SBGUBUNNM"].ToString();
                SBGUBUNNM2.Text = _dtSB.Rows[1]["SBGUBUNNM"].ToString();
                SBGUBUNNM3.Text = _dtSB.Rows[2]["SBGUBUNNM"].ToString();
                SBGUBUNNM4.Text = _dtSB.Rows[3]["SBGUBUNNM"].ToString();
                SBGUBUNNM5.Text = _dtSB.Rows[4]["SBGUBUNNM"].ToString();

                //SBDESC1.Text = UP_ChangeDate(".", _dtSB.Rows[0]["SBKDATE1"].ToString()) + "~" + UP_ChangeDate(".", _dtSB.Rows[0]["SBKDATE2"].ToString());
                //SBDESC2.Text = UP_ChangeDate(".", _dtSB.Rows[1]["SBKDATE1"].ToString()) + "~" + UP_ChangeDate(".", _dtSB.Rows[1]["SBKDATE2"].ToString());
                //SBDESC3.Text = UP_ChangeDate(".", _dtSB.Rows[2]["SBKDATE1"].ToString()) + "~" + UP_ChangeDate(".", _dtSB.Rows[2]["SBKDATE2"].ToString());
                //SBDESC4.Text = UP_ChangeDate(".", _dtSB.Rows[3]["SBKDATE1"].ToString()) + "~" + UP_ChangeDate(".", _dtSB.Rows[3]["SBKDATE2"].ToString());
                //SBDESC5.Text = UP_ChangeDate(".", _dtSB.Rows[4]["SBKDATE1"].ToString()) + "~" + UP_ChangeDate(".", _dtSB.Rows[4]["SBKDATE2"].ToString());

                SBDESC1.Text = _dtSB.Rows[0]["SBDESC"].ToString();
                SBDESC2.Text = _dtSB.Rows[1]["SBDESC"].ToString();
                SBDESC3.Text = _dtSB.Rows[2]["SBDESC"].ToString();
                SBDESC4.Text = _dtSB.Rows[3]["SBDESC"].ToString();
                SBDESC5.Text = _dtSB.Rows[4]["SBDESC"].ToString();
            }
            else
            {
                switch (iSBCount)
                {
                    case 1:
                        SBDATE1.Text = UP_ChangeDate(".", _dtSB.Rows[0]["SBDATE"].ToString());
                        SBGUBUNNM1.Text = _dtSB.Rows[0]["SBGUBUNNM"].ToString();
                        SBDESC1.Text = _dtSB.Rows[0]["SBDESC"].ToString();
                        break;
                    case 2:
                        SBDATE1.Text = UP_ChangeDate(".", _dtSB.Rows[0]["SBDATE"].ToString());
                        SBDATE2.Text = UP_ChangeDate(".", _dtSB.Rows[1]["SBDATE"].ToString());

                        SBGUBUNNM1.Text = _dtSB.Rows[0]["SBGUBUNNM"].ToString();
                        SBGUBUNNM2.Text = _dtSB.Rows[1]["SBGUBUNNM"].ToString();

                        SBDESC1.Text = _dtSB.Rows[0]["SBDESC"].ToString();
                        SBDESC2.Text = _dtSB.Rows[1]["SBDESC"].ToString();
                        break;
                    case 3:
                        SBDATE1.Text = UP_ChangeDate(".",_dtSB.Rows[0]["SBDATE"].ToString());
                        SBDATE2.Text = UP_ChangeDate(".", _dtSB.Rows[1]["SBDATE"].ToString());
                        SBDATE3.Text = UP_ChangeDate(".", _dtSB.Rows[2]["SBDATE"].ToString());

                        SBGUBUNNM1.Text = _dtSB.Rows[0]["SBGUBUNNM"].ToString();
                        SBGUBUNNM2.Text = _dtSB.Rows[1]["SBGUBUNNM"].ToString();
                        SBGUBUNNM3.Text = _dtSB.Rows[2]["SBGUBUNNM"].ToString();

                        SBDESC1.Text = _dtSB.Rows[0]["SBDESC"].ToString();
                        SBDESC2.Text = _dtSB.Rows[1]["SBDESC"].ToString();
                        SBDESC3.Text = _dtSB.Rows[2]["SBDESC"].ToString();
                        break;
                    case 4:
                        SBDATE1.Text = UP_ChangeDate(".",_dtSB.Rows[0]["SBDATE"].ToString());
                        SBDATE2.Text = UP_ChangeDate(".", _dtSB.Rows[1]["SBDATE"].ToString());
                        SBDATE3.Text = UP_ChangeDate(".", _dtSB.Rows[2]["SBDATE"].ToString());
                        SBDATE4.Text = UP_ChangeDate(".", _dtSB.Rows[3]["SBDATE"].ToString());

                        SBGUBUNNM1.Text = _dtSB.Rows[0]["SBGUBUNNM"].ToString();
                        SBGUBUNNM2.Text = _dtSB.Rows[1]["SBGUBUNNM"].ToString();
                        SBGUBUNNM3.Text = _dtSB.Rows[2]["SBGUBUNNM"].ToString();
                        SBGUBUNNM4.Text = _dtSB.Rows[3]["SBGUBUNNM"].ToString();

                        SBDESC1.Text = _dtSB.Rows[0]["SBDESC"].ToString();
                        SBDESC2.Text = _dtSB.Rows[1]["SBDESC"].ToString();
                        SBDESC3.Text = _dtSB.Rows[2]["SBDESC"].ToString();
                        SBDESC4.Text = _dtSB.Rows[3]["SBDESC"].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Description : 교육사항 입력
        private void setGY()
        {
            int iGYCount = _dtGY.Rows.Count;

            if (iGYCount >= 5)
            {
                GYIDATE1.Text = UP_ChangeDate(".", _dtGY.Rows[0]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[0]["GYJDATE"].ToString());
                GYIDATE2.Text = UP_ChangeDate(".", _dtGY.Rows[1]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[1]["GYJDATE"].ToString());
                GYIDATE3.Text = UP_ChangeDate(".", _dtGY.Rows[2]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[2]["GYJDATE"].ToString());
                GYIDATE4.Text = UP_ChangeDate(".", _dtGY.Rows[3]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[3]["GYJDATE"].ToString());
                GYIDATE5.Text = UP_ChangeDate(".", _dtGY.Rows[4]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[4]["GYJDATE"].ToString()); 

                GYDESC1.Text = _dtGY.Rows[0]["GYDESC"].ToString();
                GYDESC2.Text = _dtGY.Rows[1]["GYDESC"].ToString();
                GYDESC3.Text = _dtGY.Rows[2]["GYDESC"].ToString();
                GYDESC4.Text = _dtGY.Rows[3]["GYDESC"].ToString();
                GYDESC5.Text = _dtGY.Rows[4]["GYDESC"].ToString();

                GYSHHANG1.Text = _dtGY.Rows[0]["GYSHHANG"].ToString();
                GYSHHANG2.Text = _dtGY.Rows[1]["GYSHHANG"].ToString();
                GYSHHANG3.Text = _dtGY.Rows[2]["GYSHHANG"].ToString();
                GYSHHANG4.Text = _dtGY.Rows[3]["GYSHHANG"].ToString();
                GYSHHANG5.Text = _dtGY.Rows[4]["GYSHHANG"].ToString();

                GYBIGO1.Text = _dtGY.Rows[0]["GYBIGO"].ToString();
                GYBIGO2.Text = _dtGY.Rows[1]["GYBIGO"].ToString();
                GYBIGO3.Text = _dtGY.Rows[2]["GYBIGO"].ToString();
                GYBIGO4.Text = _dtGY.Rows[3]["GYBIGO"].ToString();
                GYBIGO5.Text = _dtGY.Rows[4]["GYBIGO"].ToString();
            }
            else
            {
                switch (iGYCount)
                {
                    case 1:
                        GYIDATE1.Text = UP_ChangeDate(".", _dtGY.Rows[0]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[0]["GYJDATE"].ToString());
                        GYDESC1.Text = _dtGY.Rows[0]["GYDESC"].ToString();
                        GYSHHANG1.Text = _dtGY.Rows[0]["GYSHHANG"].ToString();
                        GYBIGO1.Text = _dtGY.Rows[0]["GYBIGO"].ToString();
                        break;
                    case 2:
                        GYIDATE1.Text = UP_ChangeDate(".", _dtGY.Rows[0]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[0]["GYJDATE"].ToString());
                        GYIDATE2.Text = UP_ChangeDate(".", _dtGY.Rows[1]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[1]["GYJDATE"].ToString());

                        GYDESC1.Text = _dtGY.Rows[0]["GYDESC"].ToString();
                        GYDESC2.Text = _dtGY.Rows[1]["GYDESC"].ToString();

                        GYSHHANG1.Text = _dtGY.Rows[0]["GYSHHANG"].ToString();
                        GYSHHANG2.Text = _dtGY.Rows[1]["GYSHHANG"].ToString();

                        GYBIGO1.Text = _dtGY.Rows[0]["GYBIGO"].ToString();
                        GYBIGO2.Text = _dtGY.Rows[1]["GYBIGO"].ToString();
                        break;
                    case 3:
                        GYIDATE1.Text = UP_ChangeDate(".", _dtGY.Rows[0]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[0]["GYJDATE"].ToString());
                        GYIDATE2.Text = UP_ChangeDate(".", _dtGY.Rows[1]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[1]["GYJDATE"].ToString());
                        GYIDATE3.Text = UP_ChangeDate(".", _dtGY.Rows[2]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[2]["GYJDATE"].ToString());

                        GYDESC1.Text = _dtGY.Rows[0]["GYDESC"].ToString();
                        GYDESC2.Text = _dtGY.Rows[1]["GYDESC"].ToString();
                        GYDESC3.Text = _dtGY.Rows[2]["GYDESC"].ToString();

                        GYSHHANG1.Text = _dtGY.Rows[0]["GYSHHANG"].ToString();
                        GYSHHANG2.Text = _dtGY.Rows[1]["GYSHHANG"].ToString();
                        GYSHHANG3.Text = _dtGY.Rows[2]["GYSHHANG"].ToString();

                        GYBIGO1.Text = _dtGY.Rows[0]["GYBIGO"].ToString();
                        GYBIGO2.Text = _dtGY.Rows[1]["GYBIGO"].ToString();
                        GYBIGO3.Text = _dtGY.Rows[2]["GYBIGO"].ToString();
                        break;
                    case 4:
                        GYIDATE1.Text = UP_ChangeDate(".", _dtGY.Rows[0]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[0]["GYJDATE"].ToString());
                        GYIDATE2.Text = UP_ChangeDate(".", _dtGY.Rows[1]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[1]["GYJDATE"].ToString());
                        GYIDATE3.Text = UP_ChangeDate(".", _dtGY.Rows[2]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[2]["GYJDATE"].ToString());
                        GYIDATE4.Text = UP_ChangeDate(".", _dtGY.Rows[3]["GYIDATE"].ToString()) + "~" + UP_ChangeDate(".", _dtGY.Rows[3]["GYJDATE"].ToString());

                        GYDESC1.Text = _dtGY.Rows[0]["GYDESC"].ToString();
                        GYDESC2.Text = _dtGY.Rows[1]["GYDESC"].ToString();
                        GYDESC3.Text = _dtGY.Rows[2]["GYDESC"].ToString();
                        GYDESC4.Text = _dtGY.Rows[3]["GYDESC"].ToString();

                        GYSHHANG1.Text = _dtGY.Rows[0]["GYSHHANG"].ToString();
                        GYSHHANG2.Text = _dtGY.Rows[1]["GYSHHANG"].ToString();
                        GYSHHANG3.Text = _dtGY.Rows[2]["GYSHHANG"].ToString();
                        GYSHHANG4.Text = _dtGY.Rows[3]["GYSHHANG"].ToString();

                        GYBIGO1.Text = _dtGY.Rows[0]["GYBIGO"].ToString();
                        GYBIGO2.Text = _dtGY.Rows[1]["GYBIGO"].ToString();
                        GYBIGO3.Text = _dtGY.Rows[2]["GYBIGO"].ToString();
                        GYBIGO4.Text = _dtGY.Rows[3]["GYBIGO"].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Description : 발령사항 입력
        private void setBL()
        {
            int iBLCount = _dtBL.Rows.Count;

            if (iBLCount > 0)
            {
                BLDATE1.Text = _dtBL.Rows[0]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[0]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[0]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM1.Text = _dtBL.Rows[0]["BLCODENM"].ToString();
                BLBUSEONM1.Text = _dtBL.Rows[0]["BLBUSEONM"].ToString();
                BLJJCDNM1.Text = _dtBL.Rows[0]["BLJJCDNM"].ToString();
                BLHOBN1.Text = _dtBL.Rows[0]["BLHOBN"].ToString();
                BLBIGO1.Text = _dtBL.Rows[0]["BLBIGO"].ToString();
            }
            if (iBLCount > 1)
            {
                BLDATE2.Text = _dtBL.Rows[1]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[1]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[1]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM2.Text = _dtBL.Rows[1]["BLCODENM"].ToString();
                BLBUSEONM2.Text = _dtBL.Rows[1]["BLBUSEONM"].ToString();
                BLJJCDNM2.Text = _dtBL.Rows[1]["BLJJCDNM"].ToString();
                BLHOBN2.Text = _dtBL.Rows[1]["BLHOBN"].ToString();
                BLBIGO2.Text = _dtBL.Rows[1]["BLBIGO"].ToString();
            }
            if (iBLCount > 2)
            {
                BLDATE3.Text = _dtBL.Rows[2]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[2]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[2]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM3.Text = _dtBL.Rows[2]["BLCODENM"].ToString();
                BLBUSEONM3.Text = _dtBL.Rows[2]["BLBUSEONM"].ToString();
                BLJJCDNM3.Text = _dtBL.Rows[2]["BLJJCDNM"].ToString();
                BLHOBN3.Text = _dtBL.Rows[2]["BLHOBN"].ToString();
                BLBIGO3.Text = _dtBL.Rows[2]["BLBIGO"].ToString();
            }
            if (iBLCount > 3)
            {
                BLDATE4.Text = _dtBL.Rows[3]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[3]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[3]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM4.Text = _dtBL.Rows[3]["BLCODENM"].ToString();
                BLBUSEONM4.Text = _dtBL.Rows[3]["BLBUSEONM"].ToString();
                BLJJCDNM4.Text = _dtBL.Rows[3]["BLJJCDNM"].ToString();
                BLHOBN4.Text = _dtBL.Rows[3]["BLHOBN"].ToString();
                BLBIGO4.Text = _dtBL.Rows[3]["BLBIGO"].ToString();
            }
            if (iBLCount > 4)
            {
                BLDATE5.Text = _dtBL.Rows[4]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[4]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[4]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM5.Text = _dtBL.Rows[4]["BLCODENM"].ToString();
                BLBUSEONM5.Text = _dtBL.Rows[4]["BLBUSEONM"].ToString();
                BLJJCDNM5.Text = _dtBL.Rows[4]["BLJJCDNM"].ToString();
                BLHOBN5.Text = _dtBL.Rows[4]["BLHOBN"].ToString();
                BLBIGO5.Text = _dtBL.Rows[4]["BLBIGO"].ToString();
            }
            if (iBLCount > 5)
            {
                BLDATE6.Text = _dtBL.Rows[5]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[5]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[5]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM6.Text = _dtBL.Rows[5]["BLCODENM"].ToString();
                BLBUSEONM6.Text = _dtBL.Rows[5]["BLBUSEONM"].ToString();
                BLJJCDNM6.Text = _dtBL.Rows[5]["BLJJCDNM"].ToString();
                BLHOBN6.Text = _dtBL.Rows[5]["BLHOBN"].ToString();
                BLBIGO6.Text = _dtBL.Rows[5]["BLBIGO"].ToString();
            }
            if (iBLCount > 6)
            {
                BLDATE7.Text = _dtBL.Rows[6]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[6]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[6]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM7.Text = _dtBL.Rows[6]["BLCODENM"].ToString();
                BLBUSEONM7.Text = _dtBL.Rows[6]["BLBUSEONM"].ToString();
                BLJJCDNM7.Text = _dtBL.Rows[6]["BLJJCDNM"].ToString();
                BLHOBN7.Text = _dtBL.Rows[6]["BLHOBN"].ToString();
                BLBIGO7.Text = _dtBL.Rows[6]["BLBIGO"].ToString();
            }
            if (iBLCount > 7)
            {
                BLDATE8.Text = _dtBL.Rows[7]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[7]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[7]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM8.Text = _dtBL.Rows[7]["BLCODENM"].ToString();
                BLBUSEONM8.Text = _dtBL.Rows[7]["BLBUSEONM"].ToString();
                BLJJCDNM8.Text = _dtBL.Rows[7]["BLJJCDNM"].ToString();
                BLHOBN8.Text = _dtBL.Rows[7]["BLHOBN"].ToString();
                BLBIGO8.Text = _dtBL.Rows[7]["BLBIGO"].ToString();
            }
            if (iBLCount > 8)
            {
                BLDATE9.Text = _dtBL.Rows[8]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[8]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[8]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM9.Text = _dtBL.Rows[8]["BLCODENM"].ToString();
                BLBUSEONM9.Text = _dtBL.Rows[8]["BLBUSEONM"].ToString();
                BLJJCDNM9.Text = _dtBL.Rows[8]["BLJJCDNM"].ToString();
                BLHOBN9.Text = _dtBL.Rows[8]["BLHOBN"].ToString();
                BLBIGO9.Text = _dtBL.Rows[8]["BLBIGO"].ToString();
            }
            if (iBLCount > 9)
            {
                BLDATE10.Text = _dtBL.Rows[9]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[9]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[9]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM10.Text = _dtBL.Rows[9]["BLCODENM"].ToString();
                BLBUSEONM10.Text = _dtBL.Rows[9]["BLBUSEONM"].ToString();
                BLJJCDNM10.Text = _dtBL.Rows[9]["BLJJCDNM"].ToString();
                BLHOBN10.Text = _dtBL.Rows[9]["BLHOBN"].ToString();
                BLBIGO10.Text = _dtBL.Rows[9]["BLBIGO"].ToString();
            }
            if (iBLCount > 10)
            {
                BLDATE11.Text = _dtBL.Rows[10]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[10]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[10]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM11.Text = _dtBL.Rows[10]["BLCODENM"].ToString();
                BLBUSEONM11.Text = _dtBL.Rows[10]["BLBUSEONM"].ToString();
                BLJJCDNM11.Text = _dtBL.Rows[10]["BLJJCDNM"].ToString();
                BLHOBN11.Text = _dtBL.Rows[10]["BLHOBN"].ToString();
                BLBIGO11.Text = _dtBL.Rows[10]["BLBIGO"].ToString();
            }
            if (iBLCount > 11)
            {
                BLDATE12.Text = _dtBL.Rows[11]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[11]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[11]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM12.Text = _dtBL.Rows[11]["BLCODENM"].ToString();
                BLBUSEONM12.Text = _dtBL.Rows[11]["BLBUSEONM"].ToString();
                BLJJCDNM12.Text = _dtBL.Rows[11]["BLJJCDNM"].ToString();
                BLHOBN12.Text = _dtBL.Rows[11]["BLHOBN"].ToString();
                BLBIGO12.Text = _dtBL.Rows[11]["BLBIGO"].ToString();
            }
            if (iBLCount > 12)
            {
                BLDATE13.Text = _dtBL.Rows[12]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[12]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[12]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM13.Text = _dtBL.Rows[12]["BLCODENM"].ToString();
                BLBUSEONM13.Text = _dtBL.Rows[12]["BLBUSEONM"].ToString();
                BLJJCDNM13.Text = _dtBL.Rows[12]["BLJJCDNM"].ToString();
                BLHOBN13.Text = _dtBL.Rows[12]["BLHOBN"].ToString();
                BLBIGO13.Text = _dtBL.Rows[12]["BLBIGO"].ToString();
            }
            if (iBLCount > 13)
            {
                BLDATE14.Text = _dtBL.Rows[13]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[13]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[13]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM14.Text = _dtBL.Rows[13]["BLCODENM"].ToString();
                BLBUSEONM14.Text = _dtBL.Rows[13]["BLBUSEONM"].ToString();
                BLJJCDNM14.Text = _dtBL.Rows[13]["BLJJCDNM"].ToString();
                BLHOBN14.Text = _dtBL.Rows[13]["BLHOBN"].ToString();
                BLBIGO14.Text = _dtBL.Rows[13]["BLBIGO"].ToString();
            }
            if (iBLCount > 14)
            {
                BLDATE15.Text = _dtBL.Rows[14]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[14]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[14]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM15.Text = _dtBL.Rows[14]["BLCODENM"].ToString();
                BLBUSEONM15.Text = _dtBL.Rows[14]["BLBUSEONM"].ToString();
                BLJJCDNM15.Text = _dtBL.Rows[14]["BLJJCDNM"].ToString();
                BLHOBN15.Text = _dtBL.Rows[14]["BLHOBN"].ToString();
                BLBIGO15.Text = _dtBL.Rows[14]["BLBIGO"].ToString();
            }
            if (iBLCount > 15)
            {
                BLDATE16.Text = _dtBL.Rows[15]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[15]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[15]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM16.Text = _dtBL.Rows[15]["BLCODENM"].ToString();
                BLBUSEONM16.Text = _dtBL.Rows[15]["BLBUSEONM"].ToString();
                BLJJCDNM16.Text = _dtBL.Rows[15]["BLJJCDNM"].ToString();
                BLHOBN16.Text = _dtBL.Rows[15]["BLHOBN"].ToString();
                BLBIGO16.Text = _dtBL.Rows[15]["BLBIGO"].ToString();
            }
            if (iBLCount > 16)
            {
                BLDATE17.Text = _dtBL.Rows[16]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[16]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[16]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM17.Text = _dtBL.Rows[16]["BLCODENM"].ToString();
                BLBUSEONM17.Text = _dtBL.Rows[16]["BLBUSEONM"].ToString();
                BLJJCDNM17.Text = _dtBL.Rows[16]["BLJJCDNM"].ToString();
                BLHOBN17.Text = _dtBL.Rows[16]["BLHOBN"].ToString();
                BLBIGO17.Text = _dtBL.Rows[16]["BLBIGO"].ToString();
            }
            if (iBLCount > 17)
            {
                BLDATE18.Text = _dtBL.Rows[17]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[17]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[17]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM18.Text = _dtBL.Rows[17]["BLCODENM"].ToString();
                BLBUSEONM18.Text = _dtBL.Rows[17]["BLBUSEONM"].ToString();
                BLJJCDNM18.Text = _dtBL.Rows[17]["BLJJCDNM"].ToString();
                BLHOBN18.Text = _dtBL.Rows[17]["BLHOBN"].ToString();
                BLBIGO18.Text = _dtBL.Rows[17]["BLBIGO"].ToString();
            }
            if (iBLCount > 18)
            {
                BLDATE19.Text = _dtBL.Rows[18]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[18]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[18]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM19.Text = _dtBL.Rows[18]["BLCODENM"].ToString();
                BLBUSEONM19.Text = _dtBL.Rows[18]["BLBUSEONM"].ToString();
                BLJJCDNM19.Text = _dtBL.Rows[18]["BLJJCDNM"].ToString();
                BLHOBN19.Text = _dtBL.Rows[18]["BLHOBN"].ToString();
                BLBIGO19.Text = _dtBL.Rows[18]["BLBIGO"].ToString();
            }
            if (iBLCount > 19)
            {
                BLDATE20.Text = _dtBL.Rows[19]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[19]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[19]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM20.Text = _dtBL.Rows[19]["BLCODENM"].ToString();
                BLBUSEONM20.Text = _dtBL.Rows[19]["BLBUSEONM"].ToString();
                BLJJCDNM20.Text = _dtBL.Rows[19]["BLJJCDNM"].ToString();
                BLHOBN20.Text = _dtBL.Rows[19]["BLHOBN"].ToString();
                BLBIGO20.Text = _dtBL.Rows[19]["BLBIGO"].ToString();
            }
            if (iBLCount > 20)
            {
                BLDATE21.Text = _dtBL.Rows[20]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[20]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[20]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM21.Text = _dtBL.Rows[20]["BLCODENM"].ToString();
                BLBUSEONM21.Text = _dtBL.Rows[20]["BLBUSEONM"].ToString();
                BLJJCDNM21.Text = _dtBL.Rows[20]["BLJJCDNM"].ToString();
                BLHOBN21.Text = _dtBL.Rows[20]["BLHOBN"].ToString();
                BLBIGO21.Text = _dtBL.Rows[20]["BLBIGO"].ToString();
            }
            if (iBLCount > 21)
            {
                BLDATE22.Text = _dtBL.Rows[21]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[21]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[21]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM22.Text = _dtBL.Rows[21]["BLCODENM"].ToString();
                BLBUSEONM22.Text = _dtBL.Rows[21]["BLBUSEONM"].ToString();
                BLJJCDNM22.Text = _dtBL.Rows[21]["BLJJCDNM"].ToString();
                BLHOBN22.Text = _dtBL.Rows[21]["BLHOBN"].ToString();
                BLBIGO22.Text = _dtBL.Rows[21]["BLBIGO"].ToString();
            }
            if (iBLCount > 22)
            {
                BLDATE23.Text = _dtBL.Rows[22]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[22]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[22]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM23.Text = _dtBL.Rows[22]["BLCODENM"].ToString();
                BLBUSEONM23.Text = _dtBL.Rows[22]["BLBUSEONM"].ToString();
                BLJJCDNM23.Text = _dtBL.Rows[22]["BLJJCDNM"].ToString();
                BLHOBN23.Text = _dtBL.Rows[22]["BLHOBN"].ToString();
                BLBIGO23.Text = _dtBL.Rows[22]["BLBIGO"].ToString();
            }
            if (iBLCount > 23)
            {
                BLDATE24.Text = _dtBL.Rows[23]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[23]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[23]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM24.Text = _dtBL.Rows[23]["BLCODENM"].ToString();
                BLBUSEONM24.Text = _dtBL.Rows[23]["BLBUSEONM"].ToString();
                BLJJCDNM24.Text = _dtBL.Rows[23]["BLJJCDNM"].ToString();
                BLHOBN24.Text = _dtBL.Rows[23]["BLHOBN"].ToString();
                BLBIGO24.Text = _dtBL.Rows[23]["BLBIGO"].ToString();
            }
            if (iBLCount > 24)
            {
                BLDATE25.Text = _dtBL.Rows[24]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[24]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[24]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM25.Text = _dtBL.Rows[24]["BLCODENM"].ToString();
                BLBUSEONM25.Text = _dtBL.Rows[24]["BLBUSEONM"].ToString();
                BLJJCDNM25.Text = _dtBL.Rows[24]["BLJJCDNM"].ToString();
                BLHOBN25.Text = _dtBL.Rows[24]["BLHOBN"].ToString();
                BLBIGO25.Text = _dtBL.Rows[24]["BLBIGO"].ToString();
            }
            if (iBLCount > 25)
            {
                BLDATE26.Text = _dtBL.Rows[25]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[25]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[25]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM26.Text = _dtBL.Rows[25]["BLCODENM"].ToString();
                BLBUSEONM26.Text = _dtBL.Rows[25]["BLBUSEONM"].ToString();
                BLJJCDNM26.Text = _dtBL.Rows[25]["BLJJCDNM"].ToString();
                BLHOBN26.Text = _dtBL.Rows[25]["BLHOBN"].ToString();
                BLBIGO26.Text = _dtBL.Rows[25]["BLBIGO"].ToString();
            }
            if (iBLCount > 26)
            {
                BLDATE27.Text = _dtBL.Rows[26]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[26]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[26]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM27.Text = _dtBL.Rows[26]["BLCODENM"].ToString();
                BLBUSEONM27.Text = _dtBL.Rows[26]["BLBUSEONM"].ToString();
                BLJJCDNM27.Text = _dtBL.Rows[26]["BLJJCDNM"].ToString();
                BLHOBN27.Text = _dtBL.Rows[26]["BLHOBN"].ToString();
                BLBIGO27.Text = _dtBL.Rows[26]["BLBIGO"].ToString();
            }
            if (iBLCount > 27)
            {
                BLDATE28.Text = _dtBL.Rows[27]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[27]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[27]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM28.Text = _dtBL.Rows[27]["BLCODENM"].ToString();
                BLBUSEONM28.Text = _dtBL.Rows[27]["BLBUSEONM"].ToString();
                BLJJCDNM28.Text = _dtBL.Rows[27]["BLJJCDNM"].ToString();
                BLHOBN28.Text = _dtBL.Rows[27]["BLHOBN"].ToString();
                BLBIGO28.Text = _dtBL.Rows[27]["BLBIGO"].ToString();
            }
            if (iBLCount > 28)
            {
                BLDATE29.Text = _dtBL.Rows[28]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[28]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[28]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM29.Text = _dtBL.Rows[28]["BLCODENM"].ToString();
                BLBUSEONM29.Text = _dtBL.Rows[28]["BLBUSEONM"].ToString();
                BLJJCDNM29.Text = _dtBL.Rows[28]["BLJJCDNM"].ToString();
                BLHOBN29.Text = _dtBL.Rows[28]["BLHOBN"].ToString();
                BLBIGO29.Text = _dtBL.Rows[28]["BLBIGO"].ToString();
            }
            if (iBLCount > 29)
            {
                BLDATE30.Text = _dtBL.Rows[29]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[29]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[29]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM30.Text = _dtBL.Rows[29]["BLCODENM"].ToString();
                BLBUSEONM30.Text = _dtBL.Rows[29]["BLBUSEONM"].ToString();
                BLJJCDNM30.Text = _dtBL.Rows[29]["BLJJCDNM"].ToString();
                BLHOBN30.Text = _dtBL.Rows[29]["BLHOBN"].ToString();
                BLBIGO30.Text = _dtBL.Rows[29]["BLBIGO"].ToString();
            }
            if (iBLCount > 30)
            {
                BLDATE31.Text = _dtBL.Rows[30]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[30]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[30]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM31.Text = _dtBL.Rows[30]["BLCODENM"].ToString();
                BLBUSEONM31.Text = _dtBL.Rows[30]["BLBUSEONM"].ToString();
                BLJJCDNM31.Text = _dtBL.Rows[30]["BLJJCDNM"].ToString();
                BLHOBN31.Text = _dtBL.Rows[30]["BLHOBN"].ToString();
                BLBIGO31.Text = _dtBL.Rows[30]["BLBIGO"].ToString();
            }
            if (iBLCount > 31)
            {
                BLDATE32.Text = _dtBL.Rows[31]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[31]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[31]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM32.Text = _dtBL.Rows[31]["BLCODENM"].ToString();
                BLBUSEONM32.Text = _dtBL.Rows[31]["BLBUSEONM"].ToString();
                BLJJCDNM32.Text = _dtBL.Rows[31]["BLJJCDNM"].ToString();
                BLHOBN32.Text = _dtBL.Rows[31]["BLHOBN"].ToString();
                BLBIGO32.Text = _dtBL.Rows[31]["BLBIGO"].ToString();
            }
            if (iBLCount > 32)
            {
                BLDATE33.Text = _dtBL.Rows[32]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[32]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[32]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM33.Text = _dtBL.Rows[32]["BLCODENM"].ToString();
                BLBUSEONM33.Text = _dtBL.Rows[32]["BLBUSEONM"].ToString();
                BLJJCDNM33.Text = _dtBL.Rows[32]["BLJJCDNM"].ToString();
                BLHOBN33.Text = _dtBL.Rows[32]["BLHOBN"].ToString();
                BLBIGO33.Text = _dtBL.Rows[32]["BLBIGO"].ToString();
            }
            if (iBLCount > 33)
            {
                BLDATE34.Text = _dtBL.Rows[33]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[33]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[33]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM34.Text = _dtBL.Rows[33]["BLCODENM"].ToString();
                BLBUSEONM34.Text = _dtBL.Rows[33]["BLBUSEONM"].ToString();
                BLJJCDNM34.Text = _dtBL.Rows[33]["BLJJCDNM"].ToString();
                BLHOBN34.Text = _dtBL.Rows[33]["BLHOBN"].ToString();
                BLBIGO34.Text = _dtBL.Rows[33]["BLBIGO"].ToString();
            }
            if (iBLCount > 34)
            {
                BLDATE35.Text = _dtBL.Rows[34]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[34]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[34]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM35.Text = _dtBL.Rows[34]["BLCODENM"].ToString();
                BLBUSEONM35.Text = _dtBL.Rows[34]["BLBUSEONM"].ToString();
                BLJJCDNM35.Text = _dtBL.Rows[34]["BLJJCDNM"].ToString();
                BLHOBN35.Text = _dtBL.Rows[34]["BLHOBN"].ToString();
                BLBIGO35.Text = _dtBL.Rows[34]["BLBIGO"].ToString();
            }
            if (iBLCount > 35)
            {
                BLDATE36.Text = _dtBL.Rows[35]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[35]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[35]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM36.Text = _dtBL.Rows[35]["BLCODENM"].ToString();
                BLBUSEONM36.Text = _dtBL.Rows[35]["BLBUSEONM"].ToString();
                BLJJCDNM36.Text = _dtBL.Rows[35]["BLJJCDNM"].ToString();
                BLHOBN36.Text = _dtBL.Rows[35]["BLHOBN"].ToString();
                BLBIGO36.Text = _dtBL.Rows[35]["BLBIGO"].ToString();
            }
            if (iBLCount > 36)
            {
                BLDATE37.Text = _dtBL.Rows[36]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[36]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[36]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM37.Text = _dtBL.Rows[36]["BLCODENM"].ToString();
                BLBUSEONM37.Text = _dtBL.Rows[36]["BLBUSEONM"].ToString();
                BLJJCDNM37.Text = _dtBL.Rows[36]["BLJJCDNM"].ToString();
                BLHOBN37.Text = _dtBL.Rows[36]["BLHOBN"].ToString();
                BLBIGO37.Text = _dtBL.Rows[36]["BLBIGO"].ToString();
            }
            if (iBLCount > 37)
            {
                BLDATE38.Text = _dtBL.Rows[37]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[37]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[37]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM38.Text = _dtBL.Rows[37]["BLCODENM"].ToString();
                BLBUSEONM38.Text = _dtBL.Rows[37]["BLBUSEONM"].ToString();
                BLJJCDNM38.Text = _dtBL.Rows[37]["BLJJCDNM"].ToString();
                BLHOBN38.Text = _dtBL.Rows[37]["BLHOBN"].ToString();
                BLBIGO38.Text = _dtBL.Rows[37]["BLBIGO"].ToString();
            }
            if (iBLCount > 38)
            {
                BLDATE39.Text = _dtBL.Rows[38]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[38]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[38]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM39.Text = _dtBL.Rows[38]["BLCODENM"].ToString();
                BLBUSEONM39.Text = _dtBL.Rows[38]["BLBUSEONM"].ToString();
                BLJJCDNM39.Text = _dtBL.Rows[38]["BLJJCDNM"].ToString();
                BLHOBN39.Text = _dtBL.Rows[38]["BLHOBN"].ToString();
                BLBIGO39.Text = _dtBL.Rows[38]["BLBIGO"].ToString();
            }
            if (iBLCount > 39)
            {
                BLDATE40.Text = _dtBL.Rows[39]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[39]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[39]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM40.Text = _dtBL.Rows[39]["BLCODENM"].ToString();
                BLBUSEONM40.Text = _dtBL.Rows[39]["BLBUSEONM"].ToString();
                BLJJCDNM40.Text = _dtBL.Rows[39]["BLJJCDNM"].ToString();
                BLHOBN40.Text = _dtBL.Rows[39]["BLHOBN"].ToString();
                BLBIGO40.Text = _dtBL.Rows[39]["BLBIGO"].ToString();
            }
            if (iBLCount > 40)
            {
                BLDATE41.Text = _dtBL.Rows[40]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[40]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[40]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM41.Text = _dtBL.Rows[40]["BLCODENM"].ToString();
                BLBUSEONM41.Text = _dtBL.Rows[40]["BLBUSEONM"].ToString();
                BLJJCDNM41.Text = _dtBL.Rows[40]["BLJJCDNM"].ToString();
                BLHOBN41.Text = _dtBL.Rows[40]["BLHOBN"].ToString();
                BLBIGO41.Text = _dtBL.Rows[40]["BLBIGO"].ToString();
            }
            if (iBLCount > 41)
            {
                BLDATE42.Text = _dtBL.Rows[41]["BLDATE"].ToString().Substring(0, 4) + "-" + _dtBL.Rows[41]["BLDATE"].ToString().Substring(4, 2) + "-" + _dtBL.Rows[41]["BLDATE"].ToString().Substring(6, 2);
                BLCODENM42.Text = _dtBL.Rows[41]["BLCODENM"].ToString();
                BLBUSEONM42.Text = _dtBL.Rows[41]["BLBUSEONM"].ToString();
                BLJJCDNM42.Text = _dtBL.Rows[41]["BLJJCDNM"].ToString();
                BLHOBN42.Text = _dtBL.Rows[41]["BLHOBN"].ToString();
                BLBIGO42.Text = _dtBL.Rows[41]["BLBIGO"].ToString();
            }
        }
        #endregion

        #region Description : 승진사항 입력
        private void setSJ()
        {
            int iSJCount = _dtSJ.Rows.Count;

            if (iSJCount > 0)
            {
                SJDATE1.Text = UP_ChangeDate("-", _dtSJ.Rows[0]["BLDATE"].ToString());
                SJCODENM1.Text = _dtSJ.Rows[0]["BLCODENM"].ToString();
                SJBSTEAMNM1.Text = _dtSJ.Rows[0]["BLBUSEONM"].ToString();
                SJJJCDNM1.Text = _dtSJ.Rows[0]["BLJJCDNM"].ToString();
                SJHOBN1.Text = _dtSJ.Rows[0]["BLHOBN"].ToString();
                SJBIGO1.Text = _dtSJ.Rows[0]["BLBIGO"].ToString();
            }
            if (iSJCount > 1)
            {
                SJDATE2.Text = UP_ChangeDate("-", _dtSJ.Rows[1]["BLDATE"].ToString());
                SJCODENM2.Text = _dtSJ.Rows[1]["BLCODENM"].ToString();
                SJBSTEAMNM2.Text = _dtSJ.Rows[1]["BLBUSEONM"].ToString();
                SJJJCDNM2.Text = _dtSJ.Rows[1]["BLJJCDNM"].ToString();
                SJHOBN2.Text = _dtSJ.Rows[1]["BLHOBN"].ToString();
                SJBIGO2.Text = _dtSJ.Rows[1]["BLBIGO"].ToString();
            }
            if (iSJCount > 2)
            {
                SJDATE3.Text = UP_ChangeDate("-", _dtSJ.Rows[2]["BLDATE"].ToString());
                SJCODENM3.Text = _dtSJ.Rows[2]["BLCODENM"].ToString();
                SJBSTEAMNM3.Text = _dtSJ.Rows[2]["BLBUSEONM"].ToString();
                SJJJCDNM3.Text = _dtSJ.Rows[2]["BLJJCDNM"].ToString();
                SJHOBN3.Text = _dtSJ.Rows[2]["BLHOBN"].ToString();
                SJBIGO3.Text = _dtSJ.Rows[2]["BLBIGO"].ToString();
            }
            if (iSJCount > 3)
            {
                SJDATE4.Text = UP_ChangeDate("-", _dtSJ.Rows[3]["BLDATE"].ToString());
                SJCODENM4.Text = _dtSJ.Rows[3]["BLCODENM"].ToString();
                SJBSTEAMNM4.Text = _dtSJ.Rows[3]["BLBUSEONM"].ToString();
                SJJJCDNM4.Text = _dtSJ.Rows[3]["BLJJCDNM"].ToString();
                SJHOBN4.Text = _dtSJ.Rows[3]["BLHOBN"].ToString();
                SJBIGO4.Text = _dtSJ.Rows[3]["BLBIGO"].ToString();
            }
            if (iSJCount > 4)
            {
                SJDATE5.Text = UP_ChangeDate("-", _dtSJ.Rows[4]["BLDATE"].ToString());
                SJCODENM5.Text = _dtSJ.Rows[4]["BLCODENM"].ToString();
                SJBSTEAMNM5.Text = _dtSJ.Rows[4]["BLBUSEONM"].ToString();
                SJJJCDNM5.Text = _dtSJ.Rows[4]["BLJJCDNM"].ToString();
                SJHOBN5.Text = _dtSJ.Rows[4]["BLHOBN"].ToString();
                SJBIGO5.Text = _dtSJ.Rows[4]["BLBIGO"].ToString();
            }
            if (iSJCount > 5)
            {
                SJDATE6.Text = UP_ChangeDate("-", _dtSJ.Rows[5]["BLDATE"].ToString());
                SJCODENM6.Text = _dtSJ.Rows[5]["BLCODENM"].ToString();
                SJBSTEAMNM6.Text = _dtSJ.Rows[5]["BLBUSEONM"].ToString();
                SJJJCDNM6.Text = _dtSJ.Rows[5]["BLJJCDNM"].ToString();
                SJHOBN6.Text = _dtSJ.Rows[5]["BLHOBN"].ToString();
                SJBIGO6.Text = _dtSJ.Rows[5]["BLBIGO"].ToString();
            }
            if (iSJCount > 6)
            {
                SJDATE7.Text = UP_ChangeDate("-", _dtSJ.Rows[6]["BLDATE"].ToString());
                SJCODENM7.Text = _dtSJ.Rows[6]["BLCODENM"].ToString();
                SJBSTEAMNM7.Text = _dtSJ.Rows[6]["BLBUSEONM"].ToString();
                SJJJCDNM7.Text = _dtSJ.Rows[6]["BLJJCDNM"].ToString();
                SJHOBN7.Text = _dtSJ.Rows[6]["BLHOBN"].ToString();
                SJBIGO7.Text = _dtSJ.Rows[6]["BLBIGO"].ToString();
            }
            if (iSJCount > 7)
            {
                SJDATE8.Text = UP_ChangeDate("-", _dtSJ.Rows[7]["BLDATE"].ToString());
                SJCODENM8.Text = _dtSJ.Rows[7]["BLCODENM"].ToString();
                SJBSTEAMNM8.Text = _dtSJ.Rows[7]["BLBUSEONM"].ToString();
                SJJJCDNM8.Text = _dtSJ.Rows[7]["BLJJCDNM"].ToString();
                SJHOBN8.Text = _dtSJ.Rows[7]["BLHOBN"].ToString();
                SJBIGO8.Text = _dtSJ.Rows[7]["BLBIGO"].ToString();
            }
            if (iSJCount > 8)
            {
                SJDATE9.Text = UP_ChangeDate("-", _dtSJ.Rows[8]["BLDATE"].ToString());
                SJCODENM9.Text = _dtSJ.Rows[8]["BLCODENM"].ToString();
                SJBSTEAMNM9.Text = _dtSJ.Rows[8]["BLBUSEONM"].ToString();
                SJJJCDNM9.Text = _dtSJ.Rows[8]["BLJJCDNM"].ToString();
                SJHOBN9.Text = _dtSJ.Rows[8]["BLHOBN"].ToString();
                SJBIGO9.Text = _dtSJ.Rows[8]["BLBIGO"].ToString();
            }
        }
        #endregion

        #region Description : 날짜 형식 변환
        private string UP_ChangeDate(string Gubn, string Date)
        {
            string rtnValue = string.Empty;

            if (Date.Length == 8)
            {
                if (Gubn == ".")
                {
                    rtnValue = Date.Substring(2, 2) + "." + Date.Substring(4, 2) + "." + Date.Substring(6, 2);
                }
                else
                {
                    rtnValue = Date.Substring(0, 4) + "-" + Date.Substring(4, 2) + "-" + Date.Substring(6, 2);
                }
            }
            else
            {
                rtnValue = Date;
            }

            return rtnValue;
        }
        #endregion
    }
}
