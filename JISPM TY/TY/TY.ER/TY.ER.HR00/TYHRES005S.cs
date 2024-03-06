using System;
using System.Drawing;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 직급별 인원현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.31 18:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28V6H709 : EIS 인사 기본사항 조회
    ///  TY_P_HR_28V6J710 : EIS 직급별 인원현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_28V6K711 : EIS 직급별 인원현황 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRES005S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYHRES005S()
        {
            InitializeComponent();
        }

        private void TYHRES005S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.CBH01_ESPLCMPY.CodeText);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.CBH01_ESPLCMPY.GetValue().ToString() == "TY")
            {
                //UP_TY_ORGCD();
                //신인사용
                UP_TY_NewORGCD();
            }

            if (this.CBH01_ESPLCMPY.GetValue().ToString() == "TG")
            {
                UP_TG_ORGCD();
            }

            if (this.CBH01_ESPLCMPY.GetValue().ToString() == "TS")
            {
                UP_TS_ORGCD();
            }

            if (this.CBH01_ESPLCMPY.GetValue().ToString() == "TH")
            {
                UP_TH_ORGCD();
            }
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_28V6J710",  this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue());
            this.FPS91_TY_S_HR_28V6K711.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_28V6K711.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_28V6K711, "EPPRIOR_ORG_CDNM", "총  원", SumRowType.Total);
            }
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 인원생성 함수

        private void UP_TY_NewORGCD()  //태영
        {
            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;

            string sBEPPRIOR_ORG_CD = "";
            string sBEPPRIOR_ORG_CDNM = "";
            Int16 iBEPLIST01 = 0;
            Int16 iBEPLIST02 = 0;
            Int16 iBEPLIST03 = 0;
            Int16 iBEPLIST04 = 0;
            Int16 iBEPLIST05 = 0;
            Int16 iBEPLIST06 = 0;
            Int16 iBEPLIST07 = 0;
            Int16 iBEPLIST08 = 0;
            Int16 iBEPLIST09 = 0;
            Int16 iBEPLIST10 = 0;
            Int16 iBEPLIST11 = 0;
            Int16 iBEPLIST12 = 0;

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5CVEG367", this.DTP01_GSTYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 2);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "T0")
                    {
                        sTEPPRIOR_ORG_CD = "T00000";
                        sTEPPRIOR_ORG_CDNM = "UTT";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "010" && sJCCD != "020" && sJCCD != "030" && sJCCD != "040" && sJCCD != "260" && sJCCD != "270"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "150")  //운영대리
                        {
                            iTEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "160")  //운영주임
                        {
                            iTEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "170")  //부주임
                        {
                            iTEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "180")  //운영사원
                        {
                            iTEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "S0")
                    {
                        sSEPPRIOR_ORG_CD = "S00000";
                        sSEPPRIOR_ORG_CDNM = "SILO";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "010" && sJCCD != "020" && sJCCD != "030" && sJCCD != "040" && sJCCD != "260" && sJCCD != "270"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "150")  //운영대리
                        {
                            iSEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "160")  //운영주임
                        {
                            iSEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "170")  //부주임
                        {
                            iSEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "180")  //운영사원
                        {
                            iSEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "B0")
                    {
                        sBEPPRIOR_ORG_CD = "B00000";
                        sBEPPRIOR_ORG_CDNM = "무  역";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "010" && sJCCD != "020" && sJCCD != "030" && sJCCD != "040" && sJCCD != "260" && sJCCD != "270"))
                        {
                            iBEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iBEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iBEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iBEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iBEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iBEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iBEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iBEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "150")  //운영대리
                        {
                            iBEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "160")  //운영주임
                        {
                            iBEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "170")  //부주임
                        {
                            iBEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "180")  //운영사원
                        {
                            iBEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "A1" || sSaupCode == "A5" || sSaupCode == "C0")
                    {
                        sAEPPRIOR_ORG_CD = "A10000";
                        sAEPPRIOR_ORG_CDNM = "경영지원";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "010" && sJCCD != "020" && sJCCD != "030" && sJCCD != "040" && sJCCD != "260" && sJCCD != "270"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "150")  //운영대리
                        {
                            iAEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "160")  //운영주임
                        {
                            iAEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "170")  //부주임
                        {
                            iAEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "180")  //운영사원
                        {
                            iAEPLIST11 += 1;
                        }
                    }
                    //else if (sSaupCode == "A5" || sSaupCode == "C0")
                    //{
                    //    sA5EPPRIOR_ORG_CD = "A50000";
                    //    sA5EPPRIOR_ORG_CDNM = "기획재무";
                    //    //임원
                    //    if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                    //    {
                    //        iA5EPLIST01 += 1;
                    //    }
                    //    if (sJKCD == "1A") //부장
                    //    {
                    //        iA5EPLIST02 += 1;
                    //    }
                    //    if (sJKCD == "1B") //차장
                    //    {
                    //        iA5EPLIST03 += 1;
                    //    }
                    //    if (sJKCD == "2A") //과장
                    //    {
                    //        iA5EPLIST04 += 1;
                    //    }
                    //    if (sJKCD == "2B") //대리
                    //    {
                    //        iA5EPLIST05 += 1;
                    //    }
                    //    if (sJKCD == "2C") //주임
                    //    {
                    //        iA5EPLIST07 += 1;
                    //    }
                    //    if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                    //    {
                    //        iA5EPLIST10 += 1;
                    //    }
                    //    if (sJKCD == "6C")  //계약직
                    //    {
                    //        iA5EPLIST12 += 1;
                    //    }
                    //    //운영직
                    //    if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                    //    {
                    //        iA5EPLIST06 += 1;
                    //    }
                    //    if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                    //    {
                    //        iA5EPLIST08 += 1;
                    //    }
                    //    if (sJKCD == "3D" && sJCCD == "17")  //부주임
                    //    {
                    //        iA5EPLIST09 += 1;
                    //    }
                    //    if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                    //    {
                    //        iA5EPLIST11 += 1;
                    //    }
                    //}


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "010" || sJCCD == "020" || sJCCD == "030" || sJCCD == "040"))
                    {

                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "260" || sJCCD == "270"))
                    {
                        if (sSABUN == "0016-M")
                        {
                            sZ0EPPRIOR_ORG_CD = "Z00000";
                            sZ0EPPRIOR_ORG_CDNM = "경영진";

                            iZ0EPLIST01 += 1;
                        }
                        else
                        {
                            sZ1EPPRIOR_ORG_CD = "Z10000";
                            sZ1EPPRIOR_ORG_CDNM = "비상근";

                            iZ1EPLIST01 += 1;
                        }
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);

            //this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sBEPPRIOR_ORG_CD, sBEPPRIOR_ORG_CDNM, iBEPLIST01, iBEPLIST02, iBEPLIST03, iBEPLIST04,
            //                                            iBEPLIST05, iBEPLIST06, iBEPLIST07, iBEPLIST08, iBEPLIST09, iBEPLIST10, iBEPLIST11, iBEPLIST12, Employer.EmpNo);

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);

            //this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
            //                                           iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null);
        }

        private void UP_TY_ORGCD()  //태영
        {
            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;

            string sBEPPRIOR_ORG_CD = "";
            string sBEPPRIOR_ORG_CDNM = "";
            Int16 iBEPLIST01 = 0;
            Int16 iBEPLIST02 = 0;
            Int16 iBEPLIST03 = 0;
            Int16 iBEPLIST04 = 0;
            Int16 iBEPLIST05 = 0;
            Int16 iBEPLIST06 = 0;
            Int16 iBEPLIST07 = 0;
            Int16 iBEPLIST08 = 0;
            Int16 iBEPLIST09 = 0;
            Int16 iBEPLIST10 = 0;
            Int16 iBEPLIST11 = 0;
            Int16 iBEPLIST12 = 0;

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28V6H709", this.DTP01_GSTYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 2);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "T0")
                    {
                        sTEPPRIOR_ORG_CD = "T00000";
                        sTEPPRIOR_ORG_CDNM = "UTT";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iTEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iTEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iTEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iTEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "S0")
                    {
                        sSEPPRIOR_ORG_CD = "S00000";
                        sSEPPRIOR_ORG_CDNM = "SILO";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iSEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iSEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iSEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iSEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "B0")
                    {
                        sBEPPRIOR_ORG_CD = "B00000";
                        sBEPPRIOR_ORG_CDNM = "무  역";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iBEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iBEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iBEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iBEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iBEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iBEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iBEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iBEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iBEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iBEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iBEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iBEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "A1" || sSaupCode == "A5" || sSaupCode == "C0")
                    {
                        sAEPPRIOR_ORG_CD = "A10000";
                        sAEPPRIOR_ORG_CDNM = "경영지원";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iAEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iAEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iAEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iAEPLIST11 += 1;
                        }
                    }
                    //else if (sSaupCode == "A5" || sSaupCode == "C0")
                    //{
                    //    sA5EPPRIOR_ORG_CD = "A50000";
                    //    sA5EPPRIOR_ORG_CDNM = "기획재무";
                    //    //임원
                    //    if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                    //    {
                    //        iA5EPLIST01 += 1;
                    //    }
                    //    if (sJKCD == "1A") //부장
                    //    {
                    //        iA5EPLIST02 += 1;
                    //    }
                    //    if (sJKCD == "1B") //차장
                    //    {
                    //        iA5EPLIST03 += 1;
                    //    }
                    //    if (sJKCD == "2A") //과장
                    //    {
                    //        iA5EPLIST04 += 1;
                    //    }
                    //    if (sJKCD == "2B") //대리
                    //    {
                    //        iA5EPLIST05 += 1;
                    //    }
                    //    if (sJKCD == "2C") //주임
                    //    {
                    //        iA5EPLIST07 += 1;
                    //    }
                    //    if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                    //    {
                    //        iA5EPLIST10 += 1;
                    //    }
                    //    if (sJKCD == "6C")  //계약직
                    //    {
                    //        iA5EPLIST12 += 1;
                    //    }
                    //    //운영직
                    //    if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                    //    {
                    //        iA5EPLIST06 += 1;
                    //    }
                    //    if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                    //    {
                    //        iA5EPLIST08 += 1;
                    //    }
                    //    if (sJKCD == "3D" && sJCCD == "17")  //부주임
                    //    {
                    //        iA5EPLIST09 += 1;
                    //    }
                    //    if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                    //    {
                    //        iA5EPLIST11 += 1;
                    //    }
                    //}


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {
                        if (sSABUN == "0016-M")
                        {
                            sZ0EPPRIOR_ORG_CD = "Z00000";
                            sZ0EPPRIOR_ORG_CDNM = "경영진";

                            iZ0EPLIST01 += 1;
                        }
                        else
                        {
                            sZ1EPPRIOR_ORG_CD = "Z10000";
                            sZ1EPPRIOR_ORG_CDNM = "비상근";

                            iZ1EPLIST01 += 1;
                        }
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            
            //this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sBEPPRIOR_ORG_CD, sBEPPRIOR_ORG_CDNM, iBEPLIST01, iBEPLIST02, iBEPLIST03, iBEPLIST04,
            //                                            iBEPLIST05, iBEPLIST06, iBEPLIST07, iBEPLIST08, iBEPLIST09, iBEPLIST10, iBEPLIST11, iBEPLIST12, Employer.EmpNo);

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            
            //this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
            //                                           iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null); 
        }

        private void UP_TH_ORGCD() //태영호라이즌
        {

            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;        

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_34F4H503", this.DTP01_GSTYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 6);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "300000")
                    {
                        sTEPPRIOR_ORG_CD = "300000";
                        sTEPPRIOR_ORG_CDNM = "운영팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D" )  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "303000")
                    {
                        sSEPPRIOR_ORG_CD = "303000";
                        sSEPPRIOR_ORG_CDNM = "영업팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "200000")
                    {
                        sAEPPRIOR_ORG_CD = "200000";
                        sAEPPRIOR_ORG_CDNM = "관리팀";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                    }
                    else if (sSaupCode == "304000")
                    {
                        sA5EPPRIOR_ORG_CD = "304000";
                        sA5EPPRIOR_ORG_CDNM = "공무안전팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iA5EPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iA5EPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iA5EPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iA5EPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iA5EPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iA5EPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D" )  //사원
                        {
                            iA5EPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iA5EPLIST12 += 1;
                        }
                    }


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {

                        sZ1EPPRIOR_ORG_CD = "Z10000";
                        sZ1EPPRIOR_ORG_CDNM = "비상근";

                        iZ1EPLIST01 += 1;
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
                                                       iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null); 

        }

        private void UP_TS_ORGCD() //태영GLS
        {
            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;         

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_34F49502", this.DTP01_GSTYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 1);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "3")
                    {
                        sTEPPRIOR_ORG_CD = "300000";
                        sTEPPRIOR_ORG_CDNM = "운영팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "4")
                    {
                        sSEPPRIOR_ORG_CD = "400000";
                        sSEPPRIOR_ORG_CDNM = "영업팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "2" || sSaupCode == "1")
                    {
                        sAEPPRIOR_ORG_CD = "200000";
                        sAEPPRIOR_ORG_CDNM = "관리팀";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                    }                


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {

                        sZ1EPPRIOR_ORG_CD = "Z10000";
                        sZ1EPPRIOR_ORG_CDNM = "비상근";

                        iZ1EPLIST01 += 1;
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            
            //this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
            //                                           iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null); 



        }

        private void UP_TG_ORGCD() //태영그레인
        {

            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;

            string sBEPPRIOR_ORG_CD = "";
            string sBEPPRIOR_ORG_CDNM = "";
            Int16 iBEPLIST01 = 0;
            Int16 iBEPLIST02 = 0;
            Int16 iBEPLIST03 = 0;
            Int16 iBEPLIST04 = 0;
            Int16 iBEPLIST05 = 0;
            Int16 iBEPLIST06 = 0;
            Int16 iBEPLIST07 = 0;
            Int16 iBEPLIST08 = 0;
            Int16 iBEPLIST09 = 0;
            Int16 iBEPLIST10 = 0;
            Int16 iBEPLIST11 = 0;
            Int16 iBEPLIST12 = 0;

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_34F34500", this.DTP01_GSTYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 1);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "3")
                    {
                        sTEPPRIOR_ORG_CD = "300000";
                        sTEPPRIOR_ORG_CDNM = "운영부";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }
                       
                    }
                    else if (sSaupCode == "2")
                    {
                        sSEPPRIOR_ORG_CD = "200000";
                        sSEPPRIOR_ORG_CDNM = "영업부";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }
                        
                    }                   
                    else if (sSaupCode == "1")
                    {
                        sAEPPRIOR_ORG_CD = "100000";
                        sAEPPRIOR_ORG_CDNM = "관리부";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }                   
                    }
                    else if (sSaupCode == "4" )
                    {
                        sA5EPPRIOR_ORG_CD = "400000";
                        sA5EPPRIOR_ORG_CDNM = "안전관리부";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iA5EPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iA5EPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iA5EPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iA5EPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iA5EPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iA5EPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iA5EPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iA5EPLIST12 += 1;
                        }                       
                    }


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {
                       
                            sZ1EPPRIOR_ORG_CD = "Z10000";
                            sZ1EPPRIOR_ORG_CDNM = "비상근";

                            iZ1EPLIST01 += 1;
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
                                                       iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESPLCMPY.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null); 

        }
        #endregion

        
    }
}
