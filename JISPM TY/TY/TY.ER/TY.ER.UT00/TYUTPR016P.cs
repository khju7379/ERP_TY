using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 통관기준-월간출고현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.11 19:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_77BIK126 : 월간출고현황(통관기준) 임시파일 삭제
    ///  TY_P_UT_77BIM127 : 월간출고현황(통관기준) 재고량 조회
    ///  TY_P_UT_77BIO128 : 월간출고현황(통관기준) CUST-CHECK
    ///  TY_P_UT_77BIP129 : 월간출고현황(통관기준) CHUL-CHECK
    ///  TY_P_UT_77BIQ130 : 월간출고현황(통관기준) 임시파일 등록
    ///  TY_P_UT_77BIS131 : 월간출고현황(통관기준) CUST-READ
    ///  TY_P_UT_77BJ0134 : 월간출고현황(통관기준) 출고화주 체크
    ///  TY_P_UT_77BJ2135 : 월간출고현황(통관기준) 도착지별 내역 체크
    ///  TY_P_UT_77BJ4136 : 월간출고현황(통관기준) 도착지별 내역 조회
    ///  TY_P_UT_77BJ5137 : 월간출고현황(통관기준) 임시파일 삭제(화주X)
    ///  TY_P_UT_77BJ6132 : 월간출고현황(통관기준) CHUL-READ
    ///  TY_P_UT_77BJ8133 : 월간출고현황(통관기준) 전일재고조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_73D99886 : 전표 처리중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  PRGUBN : 구분
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  CJJEQTY : 재고량
    ///  REFER : 참조인
    /// </summary>
    public partial class TYUTPR016P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR016P()
        {
            InitializeComponent();
        }

        private void TYUTPR016P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.LBL51_CJJEQTY.Visible = false;
            this.TXT01_CJJEQTY.Visible = false;

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Descriptoin : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (CBO01_PRGUBN.GetValue().ToString() == "M")
            {

                if (UP_Create_Temp())
                {
                    this.ShowMessage("TY_M_MR_2BF50354");
                }
                else
                {
                }
            }
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Descriptoin : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                if (CBO01_PRGUBN.GetValue().ToString() == "M") // 월간출고
                {
                    TXT01_CJJEQTY.Text = "0";
                    dt = QueryDataSetReport();

                    if (dt.Rows.Count > 0)
                    {
                        SectionReport rpt = new TYUTPR016R1();
                        // 가로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_2422N250");
                    }
                }
                else if (CBO01_PRGUBN.GetValue().ToString() == "C") // 확인서
                {
                    if (this.TXT01_REFER.GetValue().ToString() == "")
                    {
                        this.ShowCustomMessage("참조인을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    else
                    {
                        UP_Create_Temp();
                        //QueryDataSetReport1();
                        dt = QueryDataSetJego();

                        if (dt.Rows.Count > 0)
                        {
                            SectionReport rpt = new TYUTPR015R2();
                            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else
                        {
                            this.ShowMessage("TY_M_AC_2422N250");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                string aa = str;
            }
        }
        #endregion

        #region Description : 임시파일 생성
        private bool UP_Create_Temp()
        {
            try
            {
                string sJUNJEGO = string.Empty;
                string sVNCODE = string.Empty;
                string sTEHWAJU = string.Empty;

                double dJEMTQTY = 0;
                double dCSCUQTY = 0;
                double dCHMTQTY = 0;
                double dJUNJEGO = 0;

                // 대표거래처코드 조회
                sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 임시파일 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_77BIK126", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());
                this.DbConnector.ExecuteNonQueryList();

                DataTable dt = new DataTable();

                // 대표거래처코드 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81HE4478", this.CBH01_CHHWAJU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sTEHWAJU = dt.Rows[0]["VNRPCODE"].ToString();
                }

                if (this.CBO01_GGUBUN.GetValue().ToString() == "N")
                {
                    // 월간출고현황(통관기준) 재고량 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_77BIM127", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["JEMTQTY"].ToString() != "")
                        {
                            dJEMTQTY = Convert.ToDouble(dt.Rows[0]["JEMTQTY"].ToString());
                        }
                        else
                        {
                            dJEMTQTY = 0;
                        }
                    }

                    // 월간출고현황(통관기준) 통관량 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_77BIO128", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                this.DTP01_STDATE.GetString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["CSCUQTY"].ToString() != "")
                        {
                            dCSCUQTY = Convert.ToDouble(dt.Rows[0]["CSCUQTY"].ToString());
                        }
                        else
                        {
                            dCSCUQTY = 0;
                        }
                    }

                    // 월간출고현황(통관기준) 출고량 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_77BIP129", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                this.DTP01_STDATE.GetString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["CHMTQTY"].ToString() != "")
                        {
                            dCHMTQTY = Convert.ToDouble(dt.Rows[0]["CHMTQTY"].ToString());
                        }
                        else
                        {
                            dCHMTQTY = 0;
                        }
                    }

                    // 전일 재고를 구하는 로직
                    dJUNJEGO = dJEMTQTY - dCSCUQTY + dCHMTQTY;
                    sJUNJEGO = (dJUNJEGO).ToString("0.000");

                    // 전일 재고량을 임시파일에 저장
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_77BIQ130", sTEHWAJU,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                "0",
                                                                "",
                                                                "0.000",
                                                                "0.000",
                                                                sJUNJEGO,
                                                                "A",
                                                                System.DateTime.Now.ToString("yyyyMMdd"),
                                                                System.DateTime.Now.ToString("HHmmssff"),
                                                                TYUserInfo.EmpNo
                                                                );

                    this.DbConnector.ExecuteNonQueryList();

                    //TY_P_UT_77BIS131

                    // 통관화주 통관량을 입고량에 저장
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_79E9P586", TYUserInfo.EmpNo,
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                );

                    this.DbConnector.ExecuteNonQueryList();


                    // 월간출고현황(통관기준) 출고량에 저장
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_79E9S587", TYUserInfo.EmpNo,
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                );

                    this.DbConnector.ExecuteNonQueryList();
                }
                else
                {
                    // 월간출고현황(통관기준-양수도포함) 재고량 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B7EAQ446", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["JEMTQTY"].ToString() != "")
                        {
                            dJEMTQTY = Convert.ToDouble(dt.Rows[0]["JEMTQTY"].ToString());
                        }
                        else
                        {
                            dJEMTQTY = 0;
                        }
                    }

                    // 월간출고현황(통관기준-양수도포함) 통관량 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B7EAW447", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                this.DTP01_STDATE.GetString(),
                                                                sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                this.DTP01_STDATE.GetString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["CSCUQTY"].ToString() != "")
                        {
                            dCSCUQTY = Convert.ToDouble(dt.Rows[0]["CSCUQTY"].ToString());
                        }
                        else
                        {
                            dCSCUQTY = 0;
                        }
                    }

                    // 월간출고현황(통관기준-양수도포함) 출고량 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B7EAY448", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                this.DTP01_STDATE.GetString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["CHMTQTY"].ToString() != "")
                        {
                            dCHMTQTY = Convert.ToDouble(dt.Rows[0]["CHMTQTY"].ToString());
                        }
                        else
                        {
                            dCHMTQTY = 0;
                        }
                    }

                    // 전일 재고를 구하는 로직
                    dJUNJEGO = dJEMTQTY - dCSCUQTY + dCHMTQTY;
                    sJUNJEGO = (dJUNJEGO).ToString("0.000");




                    // 전일 재고량을 임시파일에 저장
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_77BIQ130", sTEHWAJU,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                "0",
                                                                "",
                                                                "0.000",
                                                                "0.000",
                                                                sJUNJEGO,
                                                                "A",
                                                                System.DateTime.Now.ToString("yyyyMMdd"),
                                                                System.DateTime.Now.ToString("HHmmssff"),
                                                                TYUserInfo.EmpNo
                                                                );

                    this.DbConnector.ExecuteNonQueryList();

                    // 재고화주 통관량&양수량을 입고량에 저장
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B7EB6449", TYUserInfo.EmpNo,
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                );

                    this.DbConnector.ExecuteNonQueryList();


                    // 월간출고현황(재고화주기준) 출고량에 저장
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B7EB0450", TYUserInfo.EmpNo,
                                                                this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                );

                    this.DbConnector.ExecuteNonQueryList();
                }



                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport()
        {
            string sSql = string.Empty;
            string sTEDATE = string.Empty;
            string sTECHHJ = string.Empty;
            string sCHHJ1 = string.Empty;
            string sCHHJ2 = string.Empty;
            string sCHHJ3 = string.Empty;
            string sCHHJ4 = string.Empty;
            string sCHHJ5 = string.Empty;
            string sCHHJ6 = string.Empty;
            string sCHHJ7 = string.Empty;
            string sCHHJ8 = string.Empty;
            string sCHHJ9 = string.Empty;
            string sCHHJ10 = string.Empty;
            string sCHHJ11 = string.Empty;
            string sCHHJ12 = string.Empty;
            string sCHHJ13 = string.Empty;
            string sCHHJ14 = string.Empty;
            string sCHHJ15 = string.Empty;
            string sCHHJ16 = string.Empty;
            string sCHHJ17 = string.Empty;
            string sCHHJ18 = string.Empty;
            string sCHHJ19 = string.Empty;
            string sCHHJ20 = string.Empty;
            string sCHHJ21 = string.Empty;
            string sCHHJ22 = string.Empty;
            string sCHHJ23 = string.Empty;
            string sCHHJ24 = string.Empty;
            string sCHHJ25 = string.Empty;
            string sCHHJ26 = string.Empty;
            string sCHHJ27 = string.Empty;
            string sCHHJ28 = string.Empty;
            string sCHHJ29 = string.Empty;
            string sCHHJ30 = string.Empty;
            string sCHHJ31 = string.Empty;
            string sCHHJ32 = string.Empty;

            string sVNSANGHO1 = string.Empty;
            string sVNSANGHO2 = string.Empty;
            string sVNSANGHO3 = string.Empty;
            string sVNSANGHO4 = string.Empty;
            string sVNSANGHO5 = string.Empty;
            string sVNSANGHO6 = string.Empty;
            string sVNSANGHO7 = string.Empty;
            string sVNSANGHO8 = string.Empty;
            string sVNSANGHO9 = string.Empty;
            string sVNSANGHO10 = string.Empty;
            string sVNSANGHO11 = string.Empty;
            string sVNSANGHO12 = string.Empty;
            string sVNSANGHO13 = string.Empty;
            string sVNSANGHO14 = string.Empty;
            string sVNSANGHO15 = string.Empty;
            string sVNSANGHO16 = string.Empty;
            string sVNSANGHO17 = string.Empty;
            string sVNSANGHO18 = string.Empty;
            string sVNSANGHO19 = string.Empty;
            string sVNSANGHO20 = string.Empty;
            string sVNSANGHO21 = string.Empty;
            string sVNSANGHO22 = string.Empty;
            string sVNSANGHO23 = string.Empty;
            string sVNSANGHO24 = string.Empty;
            string sVNSANGHO25 = string.Empty;
            string sVNSANGHO26 = string.Empty;
            string sVNSANGHO27 = string.Empty;
            string sVNSANGHO28 = string.Empty;
            string sVNSANGHO29 = string.Empty;
            string sVNSANGHO30 = string.Empty;
            string sVNSANGHO31 = string.Empty;
            string sVNSANGHO32 = string.Empty;
            double dTECHMT1 = 0;
            double dTECHMT2 = 0;
            double dTECHMT3 = 0;
            double dTECHMT4 = 0;
            double dTECHMT5 = 0;
            double dTECHMT6 = 0;
            double dTECHMT7 = 0;
            double dTECHMT8 = 0;
            double dTECHMT9 = 0;
            double dTECHMT10 = 0;
            double dTECHMT11 = 0;
            double dTECHMT12 = 0;
            double dTECHMT13 = 0;
            double dTECHMT14 = 0;
            double dTECHMT15 = 0;
            double dTECHMT16 = 0;
            double dTECHMT17 = 0;
            double dTECHMT18 = 0;
            double dTECHMT19 = 0;
            double dTECHMT20 = 0;
            double dTECHMT21 = 0;
            double dTECHMT22 = 0;
            double dTECHMT23 = 0;
            double dTECHMT24 = 0;
            double dTECHMT25 = 0;
            double dTECHMT26 = 0;
            double dTECHMT27 = 0;
            double dTECHMT28 = 0;
            double dTECHMT29 = 0;
            double dTECHMT30 = 0;
            double dTECHMT31 = 0;
            double dTECHMT32 = 0;
            double dTEIPMT = 0;
            double dJEGO = 0;
            double dJUNJEGO = 0;

            double dHAPTECHMT1 = 0;
            double dHAPTECHMT2 = 0;
            double dHAPTECHMT3 = 0;
            double dHAPTECHMT4 = 0;
            double dHAPTECHMT5 = 0;
            double dHAPTECHMT6 = 0;
            double dHAPTECHMT7 = 0;
            double dHAPTECHMT8 = 0;
            double dHAPTECHMT9 = 0;
            double dHAPTECHMT10 = 0;
            double dHAPTECHMT11 = 0;
            double dHAPTECHMT12 = 0;
            double dHAPTECHMT13 = 0;
            double dHAPTECHMT14 = 0;
            double dHAPTECHMT15 = 0;
            double dHAPTECHMT16 = 0;
            double dHAPTECHMT17 = 0;
            double dHAPTECHMT18 = 0;
            double dHAPTECHMT19 = 0;
            double dHAPTECHMT20 = 0;
            double dHAPTECHMT21 = 0;
            double dHAPTECHMT22 = 0;
            double dHAPTECHMT23 = 0;
            double dHAPTECHMT24 = 0;
            double dHAPTECHMT25 = 0;
            double dHAPTECHMT26 = 0;
            double dHAPTECHMT27 = 0;
            double dHAPTECHMT28 = 0;
            double dHAPTECHMT29 = 0;
            double dHAPTECHMT30 = 0;
            double dHAPTECHMT31 = 0;
            double dHAPTECHMT32 = 0;
            double dHAPCHULGO = 0;
            double dHAPTEIPMT = 0;
            double dHAPJEGO = 0;
            double dcount = 0;
            string sCHECK = string.Empty;

            string[] sArray_CHHJ = new string[50];

            int iCOUNT = 0;

            string sSDATE = string.Empty;
            string sEDATE = string.Empty;
            string sDATE = string.Empty;

            string CHECK = "TRUE";

            sSDATE = this.DTP01_STDATE.GetString().Substring(0, 4) + "/" + this.DTP01_STDATE.GetString().Substring(4, 2) + "/" + this.DTP01_STDATE.GetString().Substring(6, 2);

            sEDATE = this.DTP01_EDDATE.GetString().Substring(0, 4) + "/" + this.DTP01_EDDATE.GetString().Substring(4, 2) + "/" + this.DTP01_EDDATE.GetString().Substring(6, 2);

            sDATE = "(" + sSDATE + "-" + sEDATE + ")";

            DataTable retDt = new DataTable();

            retDt.Columns.Add("DATE", typeof(System.String));
            retDt.Columns.Add("CHHJ1", typeof(System.String));
            retDt.Columns.Add("CHHJ2", typeof(System.String));
            retDt.Columns.Add("CHHJ3", typeof(System.String));
            retDt.Columns.Add("CHHJ4", typeof(System.String));
            retDt.Columns.Add("CHHJ5", typeof(System.String));
            retDt.Columns.Add("CHHJ6", typeof(System.String));
            retDt.Columns.Add("CHHJ7", typeof(System.String));
            retDt.Columns.Add("CHHJ8", typeof(System.String));
            retDt.Columns.Add("CHHJ9", typeof(System.String));
            retDt.Columns.Add("CHHJ10", typeof(System.String));
            retDt.Columns.Add("CHHJ11", typeof(System.String));
            retDt.Columns.Add("CHHJ12", typeof(System.String));
            retDt.Columns.Add("CHHJ13", typeof(System.String));
            retDt.Columns.Add("CHHJ14", typeof(System.String));
            retDt.Columns.Add("CHHJ15", typeof(System.String));
            retDt.Columns.Add("CHHJ16", typeof(System.String));
            retDt.Columns.Add("CHHJ17", typeof(System.String));
            retDt.Columns.Add("CHHJ18", typeof(System.String));
            retDt.Columns.Add("CHHJ19", typeof(System.String));
            retDt.Columns.Add("CHHJ20", typeof(System.String));
            retDt.Columns.Add("CHHJ21", typeof(System.String));
            retDt.Columns.Add("CHHJ22", typeof(System.String));
            retDt.Columns.Add("CHHJ23", typeof(System.String));
            retDt.Columns.Add("CHHJ24", typeof(System.String));
            retDt.Columns.Add("CHHJ25", typeof(System.String));
            retDt.Columns.Add("CHHJ26", typeof(System.String));
            retDt.Columns.Add("CHHJ27", typeof(System.String));
            retDt.Columns.Add("CHHJ28", typeof(System.String));
            retDt.Columns.Add("CHHJ29", typeof(System.String));
            retDt.Columns.Add("CHHJ30", typeof(System.String));
            retDt.Columns.Add("CHHJ31", typeof(System.String));
            retDt.Columns.Add("CHHJ32", typeof(System.String));

            retDt.Columns.Add("CHMT1", typeof(System.String));
            retDt.Columns.Add("CHMT2", typeof(System.String));
            retDt.Columns.Add("CHMT3", typeof(System.String));
            retDt.Columns.Add("CHMT4", typeof(System.String));
            retDt.Columns.Add("CHMT5", typeof(System.String));
            retDt.Columns.Add("CHMT6", typeof(System.String));
            retDt.Columns.Add("CHMT7", typeof(System.String));
            retDt.Columns.Add("CHMT8", typeof(System.String));
            retDt.Columns.Add("CHMT9", typeof(System.String));
            retDt.Columns.Add("CHMT10", typeof(System.String));
            retDt.Columns.Add("CHMT11", typeof(System.String));
            retDt.Columns.Add("CHMT12", typeof(System.String));
            retDt.Columns.Add("CHMT13", typeof(System.String));
            retDt.Columns.Add("CHMT14", typeof(System.String));
            retDt.Columns.Add("CHMT15", typeof(System.String));
            retDt.Columns.Add("CHMT16", typeof(System.String));
            retDt.Columns.Add("CHMT17", typeof(System.String));
            retDt.Columns.Add("CHMT18", typeof(System.String));
            retDt.Columns.Add("CHMT19", typeof(System.String));
            retDt.Columns.Add("CHMT20", typeof(System.String));
            retDt.Columns.Add("CHMT21", typeof(System.String));
            retDt.Columns.Add("CHMT22", typeof(System.String));
            retDt.Columns.Add("CHMT23", typeof(System.String));
            retDt.Columns.Add("CHMT24", typeof(System.String));
            retDt.Columns.Add("CHMT25", typeof(System.String));
            retDt.Columns.Add("CHMT26", typeof(System.String));
            retDt.Columns.Add("CHMT27", typeof(System.String));
            retDt.Columns.Add("CHMT28", typeof(System.String));
            retDt.Columns.Add("CHMT29", typeof(System.String));
            retDt.Columns.Add("CHMT30", typeof(System.String));
            retDt.Columns.Add("CHMT31", typeof(System.String));
            retDt.Columns.Add("CHMT32", typeof(System.String));

            retDt.Columns.Add("CHMTHAP", typeof(System.String));

            retDt.Columns.Add("IPMT", typeof(System.String));
            retDt.Columns.Add("JEGO", typeof(System.String));
            retDt.Columns.Add("JUNJEGO", typeof(System.String));
            retDt.Columns.Add("HWAJU", typeof(System.String));
            retDt.Columns.Add("HWAMUL", typeof(System.String));
            retDt.Columns.Add("SDATE", typeof(System.String));
            //retDt.Columns.Add("CHECK", typeof(System.String));

            try
            {
                int i = 0;
                string sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                for (i = 0; i < 50; i++)
                {
                    sArray_CHHJ[i] = "";
                }

                // 전일 재고를 가져옴

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_77BJ8133", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dJEGO = double.Parse(Get_Numeric(dt.Rows[0]["TEJEMT"].ToString()));
                    dJUNJEGO = dJEGO;
                }

                // 도착지별 화주를 가져옴

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_77BJ0134", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    sArray_CHHJ[i + 1] = dt.Rows[i]["TECHHJ"].ToString();
                    switch (i)
                    {
                        case 0:
                            sCHHJ1 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO1 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 1:
                            sCHHJ2 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO2 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 2:
                            sCHHJ3 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO3 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 3:
                            sCHHJ4 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO4 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 4:
                            sCHHJ5 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO5 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 5:
                            sCHHJ6 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO6 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 6:
                            sCHHJ7 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO7 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 7:
                            sCHHJ8 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO8 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 8:
                            sCHHJ9 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO9 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 9:
                            sCHHJ10 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO10 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 10:
                            sCHHJ11 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO11 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 11:
                            sCHHJ12 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO12 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 12:
                            sCHHJ13 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO13 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 13:
                            sCHHJ14 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO14 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 14:
                            sCHHJ15 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO15 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 15:
                            sCHHJ16 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO16 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 16:
                            sCHHJ17 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO17 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 17:
                            sCHHJ18 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO18 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 18:
                            sCHHJ19 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO19 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 19:
                            sCHHJ20 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO20 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 20:
                            sCHHJ21 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO21 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 21:
                            sCHHJ22 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO22 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 22:
                            sCHHJ23 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO23 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 23:
                            sCHHJ24 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO24 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 24:
                            sCHHJ25 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO25 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 25:
                            sCHHJ26 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO26 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 26:
                            sCHHJ27 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO27 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 27:
                            sCHHJ28 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO28 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 28:
                            sCHHJ29 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO29 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 29:
                            sCHHJ30 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO30 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 30:
                            sCHHJ31 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO31 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                        case 31:
                            sCHHJ32 = dt.Rows[i]["TECHHJ"].ToString();
                            sVNSANGHO32 = dt.Rows[i]["VNSANGHO"].ToString();
                            break;
                    }
                }

                DataRow row = retDt.NewRow();

                row["DATE"] = "";
                row["CHHJ1"] = sVNSANGHO1.ToString();
                row["CHHJ2"] = sVNSANGHO2.ToString();
                row["CHHJ3"] = sVNSANGHO3.ToString();
                row["CHHJ4"] = sVNSANGHO4.ToString();
                row["CHHJ5"] = sVNSANGHO5.ToString();
                row["CHHJ6"] = sVNSANGHO6.ToString();
                row["CHHJ7"] = sVNSANGHO7.ToString();
                row["CHHJ8"] = sVNSANGHO8.ToString();
                row["CHHJ9"] = sVNSANGHO9.ToString();
                row["CHHJ10"] = sVNSANGHO10.ToString();
                row["CHHJ11"] = sVNSANGHO11.ToString();
                row["CHHJ12"] = sVNSANGHO12.ToString();
                row["CHHJ13"] = sVNSANGHO13.ToString();
                row["CHHJ14"] = sVNSANGHO14.ToString();
                row["CHHJ15"] = sVNSANGHO15.ToString();
                row["CHHJ16"] = sVNSANGHO16.ToString();
                row["CHHJ17"] = sVNSANGHO17.ToString();
                row["CHHJ18"] = sVNSANGHO18.ToString();
                row["CHHJ19"] = sVNSANGHO19.ToString();
                row["CHHJ20"] = sVNSANGHO20.ToString();
                row["CHHJ21"] = sVNSANGHO21.ToString();
                row["CHHJ22"] = sVNSANGHO22.ToString();
                row["CHHJ23"] = sVNSANGHO23.ToString();
                row["CHHJ24"] = sVNSANGHO24.ToString();
                row["CHHJ25"] = sVNSANGHO25.ToString();
                row["CHHJ26"] = sVNSANGHO26.ToString();
                row["CHHJ27"] = sVNSANGHO27.ToString();
                row["CHHJ28"] = sVNSANGHO28.ToString();
                row["CHHJ29"] = sVNSANGHO29.ToString();
                row["CHHJ30"] = sVNSANGHO30.ToString();
                row["CHHJ31"] = sVNSANGHO31.ToString();
                row["CHHJ32"] = sVNSANGHO32.ToString();

                row["CHMT1"] = 0;
                row["CHMT2"] = 0;
                row["CHMT3"] = 0;
                row["CHMT4"] = 0;
                row["CHMT5"] = 0;
                row["CHMT6"] = 0;
                row["CHMT7"] = 0;
                row["CHMT8"] = 0;
                row["CHMT9"] = 0;
                row["CHMT10"] = 0;
                row["CHMT11"] = 0;
                row["CHMT12"] = 0;
                row["CHMT13"] = 0;
                row["CHMT14"] = 0;
                row["CHMT15"] = 0;
                row["CHMT16"] = 0;
                row["CHMT17"] = 0;
                row["CHMT18"] = 0;
                row["CHMT19"] = 0;
                row["CHMT20"] = 0;
                row["CHMT21"] = 0;
                row["CHMT22"] = 0;
                row["CHMT23"] = 0;
                row["CHMT24"] = 0;
                row["CHMT25"] = 0;
                row["CHMT26"] = 0;
                row["CHMT27"] = 0;
                row["CHMT28"] = 0;
                row["CHMT29"] = 0;
                row["CHMT30"] = 0;
                row["CHMT31"] = 0;
                row["CHMT32"] = 0;

                row["CHMTHAP"] = 0;
                row["IPMT"] = 0;
                row["JEGO"] = 0;
                row["JUNJEGO"] = 0;
                row["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                row["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                row["SDATE"] = sDATE.ToString();
                //row["CHECK"] = "1";

                retDt.Rows.Add(row);


                // 전일재고
                DataRow row1 = retDt.NewRow();

                row1["DATE"] = "";
                row1["CHHJ1"] = sVNSANGHO1.ToString();
                row1["CHHJ2"] = sVNSANGHO2.ToString();
                row1["CHHJ3"] = sVNSANGHO3.ToString();
                row1["CHHJ4"] = sVNSANGHO4.ToString();
                row1["CHHJ5"] = sVNSANGHO5.ToString();
                row1["CHHJ6"] = sVNSANGHO6.ToString();
                row1["CHHJ7"] = sVNSANGHO7.ToString();
                row1["CHHJ8"] = sVNSANGHO8.ToString();
                row1["CHHJ9"] = sVNSANGHO9.ToString();
                row1["CHHJ10"] = sVNSANGHO10.ToString();
                row1["CHHJ11"] = sVNSANGHO11.ToString();
                row1["CHHJ12"] = sVNSANGHO12.ToString();
                row1["CHHJ13"] = sVNSANGHO13.ToString();
                row1["CHHJ14"] = sVNSANGHO14.ToString();
                row1["CHHJ15"] = sVNSANGHO15.ToString();
                row1["CHHJ16"] = sVNSANGHO16.ToString();
                row1["CHHJ17"] = sVNSANGHO17.ToString();
                row1["CHHJ18"] = sVNSANGHO18.ToString();
                row1["CHHJ19"] = sVNSANGHO19.ToString();
                row1["CHHJ20"] = sVNSANGHO20.ToString();
                row1["CHHJ21"] = sVNSANGHO21.ToString();
                row1["CHHJ22"] = sVNSANGHO22.ToString();
                row1["CHHJ23"] = sVNSANGHO23.ToString();
                row1["CHHJ24"] = sVNSANGHO24.ToString();
                row1["CHHJ25"] = sVNSANGHO25.ToString();
                row1["CHHJ26"] = sVNSANGHO26.ToString();
                row1["CHHJ27"] = sVNSANGHO27.ToString();
                row1["CHHJ28"] = sVNSANGHO28.ToString();
                row1["CHHJ29"] = sVNSANGHO29.ToString();
                row1["CHHJ30"] = sVNSANGHO30.ToString();
                row1["CHHJ31"] = sVNSANGHO31.ToString();
                row1["CHHJ32"] = sVNSANGHO32.ToString();

                row1["CHMT1"] = 0;
                row1["CHMT2"] = 0;
                row1["CHMT3"] = 0;
                row1["CHMT4"] = 0;
                row1["CHMT5"] = 0;
                row1["CHMT6"] = 0;
                row1["CHMT7"] = 0;
                row1["CHMT8"] = 0;
                row1["CHMT9"] = 0;
                row1["CHMT10"] = 0;
                row1["CHMT11"] = 0;
                row1["CHMT12"] = 0;
                row1["CHMT13"] = 0;
                row1["CHMT14"] = 0;
                row1["CHMT15"] = 0;
                row1["CHMT16"] = 0;
                row1["CHMT17"] = 0;
                row1["CHMT18"] = 0;
                row1["CHMT19"] = 0;
                row1["CHMT20"] = 0;
                row1["CHMT21"] = 0;
                row1["CHMT22"] = 0;
                row1["CHMT23"] = 0;
                row1["CHMT24"] = 0;
                row1["CHMT25"] = 0;
                row1["CHMT26"] = 0;
                row1["CHMT27"] = 0;
                row1["CHMT28"] = 0;
                row1["CHMT29"] = 0;
                row1["CHMT30"] = 0;
                row1["CHMT31"] = 0;
                row1["CHMT32"] = 0;

                row1["CHMTHAP"] = 0;
                row1["IPMT"] = 0;
                row1["JEGO"] = dJEGO;
                row1["JUNJEGO"] = dJUNJEGO;
                row1["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                row1["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                row1["SDATE"] = sDATE.ToString();
                //row1["CHECK"] = "1";

                retDt.Rows.Add(row1);

                string sCHHJ = string.Empty;
                string DATE = string.Empty;
                string sYY = string.Empty;
                string sMM = string.Empty;
                string sDD = string.Empty;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_79EFD597", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    sTEDATE = dt1.Rows[j]["TEDATE"].ToString();

                    dTECHMT1 = 0;
                    dTECHMT2 = 0;
                    dTECHMT3 = 0;
                    dTECHMT4 = 0;
                    dTECHMT5 = 0;
                    dTECHMT6 = 0;
                    dTECHMT7 = 0;
                    dTECHMT8 = 0;
                    dTECHMT9 = 0;
                    dTECHMT10 = 0;
                    dTECHMT11 = 0;
                    dTECHMT12 = 0;
                    dTECHMT13 = 0;
                    dTECHMT14 = 0;
                    dTECHMT15 = 0;
                    dTECHMT16 = 0;
                    dTECHMT17 = 0;
                    dTECHMT18 = 0;
                    dTECHMT19 = 0;
                    dTECHMT20 = 0;
                    dTECHMT21 = 0;
                    dTECHMT22 = 0;
                    dTECHMT23 = 0;
                    dTECHMT24 = 0;
                    dTECHMT25 = 0;
                    dTECHMT26 = 0;
                    dTECHMT27 = 0;
                    dTECHMT28 = 0;
                    dTECHMT29 = 0;
                    dTECHMT30 = 0;
                    dTECHMT31 = 0;
                    dTECHMT32 = 0;
                    dTEIPMT = 0;
                    DATE = "";

                    for (i = 0; i < 33; i++)
                    {
                        if (i > 0)
                        {
                            if (sArray_CHHJ[i].ToString() == "")
                            {
                                break;
                            }
                        }

                        switch (i)
                        {
                            case 0:
                                sCHHJ = "";
                                break;

                            case 1:
                                sCHHJ = sCHHJ1.ToString();
                                break;

                            case 2:
                                sCHHJ = sCHHJ2.ToString();
                                break;

                            case 3:
                                sCHHJ = sCHHJ3.ToString();
                                break;

                            case 4:
                                sCHHJ = sCHHJ4.ToString();
                                break;

                            case 5:
                                sCHHJ = sCHHJ5.ToString();
                                break;

                            case 6:
                                sCHHJ = sCHHJ6.ToString();
                                break;

                            case 7:
                                sCHHJ = sCHHJ7.ToString();
                                break;

                            case 8:
                                sCHHJ = sCHHJ8.ToString();
                                break;

                            case 9:
                                sCHHJ = sCHHJ9.ToString();
                                break;

                            case 10:
                                sCHHJ = sCHHJ10.ToString();
                                break;

                            case 11:
                                sCHHJ = sCHHJ11.ToString();
                                break;

                            case 12:
                                sCHHJ = sCHHJ12.ToString();
                                break;

                            case 13:
                                sCHHJ = sCHHJ13.ToString();
                                break;

                            case 14:
                                sCHHJ = sCHHJ14.ToString();
                                break;

                            case 15:
                                sCHHJ = sCHHJ15.ToString();
                                break;

                            case 16:
                                sCHHJ = sCHHJ16.ToString();
                                break;

                            case 17:
                                sCHHJ = sCHHJ17.ToString();
                                break;

                            case 18:
                                sCHHJ = sCHHJ18.ToString();
                                break;

                            case 19:
                                sCHHJ = sCHHJ19.ToString();
                                break;

                            case 20:
                                sCHHJ = sCHHJ20.ToString();
                                break;

                            case 21:
                                sCHHJ = sCHHJ21.ToString();
                                break;

                            case 22:
                                sCHHJ = sCHHJ22.ToString();
                                break;

                            case 23:
                                sCHHJ = sCHHJ23.ToString();
                                break;

                            case 24:
                                sCHHJ = sCHHJ24.ToString();
                                break;

                            case 25:
                                sCHHJ = sCHHJ25.ToString();
                                break;

                            case 26:
                                sCHHJ = sCHHJ26.ToString();
                                break;

                            case 27:
                                sCHHJ = sCHHJ27.ToString();
                                break;

                            case 28:
                                sCHHJ = sCHHJ28.ToString();
                                break;

                            case 29:
                                sCHHJ = sCHHJ29.ToString();
                                break;

                            case 30:
                                sCHHJ = sCHHJ30.ToString();
                                break;

                            case 31:
                                sCHHJ = sCHHJ31.ToString();
                                break;

                            case 32:
                                sCHHJ = sCHHJ32.ToString();
                                break;
                        }


                        // 도착지별 내역을 가져옴
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_77BJ4136", sVNCODE,
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                    sCHHJ,
                                                                    sTEDATE.ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            CHECK = "TRUE";

                            DATE = sTEDATE.ToString();

                            if (sCHHJ.ToString() == "")
                            {
                                dTEIPMT = Convert.ToDouble(dt.Rows[x]["TEIPMT"].ToString());
                            }

                            switch (i)
                            {
                                case 1:
                                    dTECHMT1 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT1 = dHAPTECHMT1 + dTECHMT1;
                                    break;

                                case 2:
                                    dTECHMT2 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT2 = dHAPTECHMT2 + dTECHMT2;
                                    break;

                                case 3:
                                    dTECHMT3 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT3 = dHAPTECHMT3 + dTECHMT3;
                                    break;

                                case 4:
                                    dTECHMT4 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT4 = dHAPTECHMT4 + dTECHMT4;
                                    break;

                                case 5:
                                    dTECHMT5 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT5 = dHAPTECHMT5 + dTECHMT5;
                                    break;

                                case 6:
                                    dTECHMT6 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT6 = dHAPTECHMT6 + dTECHMT6;
                                    break;

                                case 7:
                                    dTECHMT7 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT7 = dHAPTECHMT7 + dTECHMT7;
                                    break;

                                case 8:
                                    dTECHMT8 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT8 = dHAPTECHMT8 + dTECHMT8;
                                    break;

                                case 9:
                                    dTECHMT9 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT9 = dHAPTECHMT9 + dTECHMT9;
                                    break;

                                case 10:
                                    dTECHMT10 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT10 = dHAPTECHMT10 + dTECHMT10;
                                    break;

                                case 11:
                                    dTECHMT11 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT11 = dHAPTECHMT11 + dTECHMT11;
                                    break;

                                case 12:
                                    dTECHMT12 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT12 = dHAPTECHMT12 + dTECHMT12;
                                    break;

                                case 13:
                                    dTECHMT13 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT13 = dHAPTECHMT13 + dTECHMT13;
                                    break;

                                case 14:
                                    dTECHMT14 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT14 = dHAPTECHMT14 + dTECHMT14;
                                    break;

                                case 15:
                                    dTECHMT15 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT15 = dHAPTECHMT15 + dTECHMT15;
                                    break;

                                case 16:
                                    dTECHMT16 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT16 = dHAPTECHMT16 + dTECHMT16;
                                    break;

                                case 17:
                                    dTECHMT17 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT17 = dHAPTECHMT17 + dTECHMT17;
                                    break;

                                case 18:
                                    dTECHMT18 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT18 = dHAPTECHMT18 + dTECHMT18;
                                    break;

                                case 19:
                                    dTECHMT19 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT19 = dHAPTECHMT19 + dTECHMT19;
                                    break;

                                case 20:
                                    dTECHMT20 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT20 = dHAPTECHMT20 + dTECHMT20;
                                    break;

                                case 21:
                                    dTECHMT21 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT21 = dHAPTECHMT21 + dTECHMT21;
                                    break;

                                case 22:
                                    dTECHMT22 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT22 = dHAPTECHMT22 + dTECHMT22;
                                    break;

                                case 23:
                                    dTECHMT23 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT23 = dHAPTECHMT23 + dTECHMT23;
                                    break;

                                case 24:
                                    dTECHMT24 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT24 = dHAPTECHMT24 + dTECHMT24;
                                    break;

                                case 25:
                                    dTECHMT25 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT25 = dHAPTECHMT25 + dTECHMT25;
                                    break;

                                case 26:
                                    dTECHMT26 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT26 = dHAPTECHMT26 + dTECHMT26;
                                    break;

                                case 27:
                                    dTECHMT27 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT27 = dHAPTECHMT27 + dTECHMT27;
                                    break;

                                case 28:
                                    dTECHMT28 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT28 = dHAPTECHMT28 + dTECHMT28;
                                    break;

                                case 29:
                                    dTECHMT29 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT29 = dHAPTECHMT29 + dTECHMT29;
                                    break;

                                case 30:
                                    dTECHMT30 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT30 = dHAPTECHMT30 + dTECHMT30;
                                    break;

                                case 31:
                                    dTECHMT31 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT31 = dHAPTECHMT31 + dTECHMT31;
                                    break;

                                case 32:
                                    dTECHMT32 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                    dHAPTECHMT32 = dHAPTECHMT32 + dTECHMT32;
                                    break;
                            }
                        }
                    }//end for

                    if (CHECK == "TRUE")
                    {
                        dcount = dcount + 1;
                        // 입고량 누계
                        dHAPTEIPMT = dHAPTEIPMT + dTEIPMT;
                        // 재고량
                        dJEGO = dJEGO + dTEIPMT -
                            (dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6
                            + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12
                            + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16 + dTECHMT17 + dTECHMT18
                            + dTECHMT19 + dTECHMT20 + dTECHMT21 + dTECHMT22 + dTECHMT23 + dTECHMT24
                            + dTECHMT25 + dTECHMT26 + dTECHMT27 + dTECHMT28 + dTECHMT29 + dTECHMT30
                            + dTECHMT31 + dTECHMT32);

                        DataRow row2 = retDt.NewRow();

                        row2["DATE"] = DATE.ToString();
                        row2["CHHJ1"] = sVNSANGHO1.ToString();
                        row2["CHHJ2"] = sVNSANGHO2.ToString();
                        row2["CHHJ3"] = sVNSANGHO3.ToString();
                        row2["CHHJ4"] = sVNSANGHO4.ToString();
                        row2["CHHJ5"] = sVNSANGHO5.ToString();
                        row2["CHHJ6"] = sVNSANGHO6.ToString();
                        row2["CHHJ7"] = sVNSANGHO7.ToString();
                        row2["CHHJ8"] = sVNSANGHO8.ToString();
                        row2["CHHJ9"] = sVNSANGHO9.ToString();
                        row2["CHHJ10"] = sVNSANGHO10.ToString();
                        row2["CHHJ11"] = sVNSANGHO11.ToString();
                        row2["CHHJ12"] = sVNSANGHO12.ToString();
                        row2["CHHJ13"] = sVNSANGHO13.ToString();
                        row2["CHHJ14"] = sVNSANGHO14.ToString();
                        row2["CHHJ15"] = sVNSANGHO15.ToString();
                        row2["CHHJ16"] = sVNSANGHO16.ToString();
                        row2["CHHJ17"] = sVNSANGHO17.ToString();
                        row2["CHHJ18"] = sVNSANGHO18.ToString();
                        row2["CHHJ19"] = sVNSANGHO19.ToString();
                        row2["CHHJ20"] = sVNSANGHO20.ToString();
                        row2["CHHJ21"] = sVNSANGHO21.ToString();
                        row2["CHHJ22"] = sVNSANGHO22.ToString();
                        row2["CHHJ23"] = sVNSANGHO23.ToString();
                        row2["CHHJ24"] = sVNSANGHO24.ToString();
                        row2["CHHJ25"] = sVNSANGHO25.ToString();
                        row2["CHHJ26"] = sVNSANGHO26.ToString();
                        row2["CHHJ27"] = sVNSANGHO27.ToString();
                        row2["CHHJ28"] = sVNSANGHO28.ToString();
                        row2["CHHJ29"] = sVNSANGHO29.ToString();
                        row2["CHHJ30"] = sVNSANGHO30.ToString();
                        row2["CHHJ31"] = sVNSANGHO31.ToString();
                        row2["CHHJ32"] = sVNSANGHO32.ToString();
                        row2["CHMT1"] = dTECHMT1;
                        row2["CHMT2"] = dTECHMT2;
                        row2["CHMT3"] = dTECHMT3;
                        row2["CHMT4"] = dTECHMT4;
                        row2["CHMT5"] = dTECHMT5;
                        row2["CHMT6"] = dTECHMT6;
                        row2["CHMT7"] = dTECHMT7;
                        row2["CHMT8"] = dTECHMT8;
                        row2["CHMT9"] = dTECHMT9;
                        row2["CHMT10"] = dTECHMT10;
                        row2["CHMT11"] = dTECHMT11;
                        row2["CHMT12"] = dTECHMT12;
                        row2["CHMT13"] = dTECHMT13;
                        row2["CHMT14"] = dTECHMT14;
                        row2["CHMT15"] = dTECHMT15;
                        row2["CHMT16"] = dTECHMT16;
                        row2["CHMT17"] = dTECHMT17;
                        row2["CHMT18"] = dTECHMT18;
                        row2["CHMT19"] = dTECHMT19;
                        row2["CHMT20"] = dTECHMT20;
                        row2["CHMT21"] = dTECHMT21;
                        row2["CHMT22"] = dTECHMT22;
                        row2["CHMT23"] = dTECHMT23;
                        row2["CHMT24"] = dTECHMT24;
                        row2["CHMT25"] = dTECHMT25;
                        row2["CHMT26"] = dTECHMT26;
                        row2["CHMT27"] = dTECHMT27;
                        row2["CHMT28"] = dTECHMT28;
                        row2["CHMT29"] = dTECHMT29;
                        row2["CHMT30"] = dTECHMT30;
                        row2["CHMT31"] = dTECHMT31;
                        row2["CHMT32"] = dTECHMT32;

                        row2["CHMTHAP"] = (dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6 + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12 + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16 + dTECHMT17 + dTECHMT18 + dTECHMT19 + dTECHMT20 + dTECHMT21 + dTECHMT22 + dTECHMT23 + dTECHMT24 + dTECHMT25 + dTECHMT26 + dTECHMT27 + dTECHMT28 + dTECHMT29 + dTECHMT30 + dTECHMT31 + dTECHMT32);
                        row2["IPMT"] = dTEIPMT;
                        row2["JEGO"] = dJEGO;
                        row2["JUNJEGO"] = dJUNJEGO;
                        row2["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                        row2["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                        row2["SDATE"] = sDATE.ToString();
                        //row2["CHECK"] = sCHECK;

                        retDt.Rows.Add(row2);
                    }

                }// end for
                // 총 출고합
                dHAPCHULGO = dHAPTECHMT1 + dHAPTECHMT2 + dHAPTECHMT3 + dHAPTECHMT4 + dHAPTECHMT5 + dHAPTECHMT6
                            + dHAPTECHMT7 + dHAPTECHMT8 + dHAPTECHMT9 + dHAPTECHMT10 + dHAPTECHMT11 + dHAPTECHMT12
                            + dHAPTECHMT13 + dHAPTECHMT14 + dHAPTECHMT15 + dHAPTECHMT16 + dHAPTECHMT17 + dHAPTECHMT18
                            + dHAPTECHMT19 + dHAPTECHMT20 + dHAPTECHMT21 + dHAPTECHMT22 + dHAPTECHMT23 + dHAPTECHMT24
                            + dHAPTECHMT25 + dHAPTECHMT26 + dHAPTECHMT27 + dHAPTECHMT28 + dHAPTECHMT29 + dHAPTECHMT30
                            + dHAPTECHMT31 + dHAPTECHMT32;

                // 재고  = 전월재고 + 총 입고량  - 총 출고량
                dHAPJEGO = dJUNJEGO + dHAPTEIPMT - dHAPCHULGO;

                DataRow row3 = retDt.NewRow();

                row3["DATE"] = "계";
                row3["CHHJ1"] = sVNSANGHO1.ToString();
                row3["CHHJ2"] = sVNSANGHO2.ToString();
                row3["CHHJ3"] = sVNSANGHO3.ToString();
                row3["CHHJ4"] = sVNSANGHO4.ToString();
                row3["CHHJ5"] = sVNSANGHO5.ToString();
                row3["CHHJ6"] = sVNSANGHO6.ToString();
                row3["CHHJ7"] = sVNSANGHO7.ToString();
                row3["CHHJ8"] = sVNSANGHO8.ToString();
                row3["CHHJ9"] = sVNSANGHO9.ToString();
                row3["CHHJ10"] = sVNSANGHO10.ToString();
                row3["CHHJ11"] = sVNSANGHO11.ToString();
                row3["CHHJ12"] = sVNSANGHO12.ToString();
                row3["CHHJ13"] = sVNSANGHO13.ToString();
                row3["CHHJ14"] = sVNSANGHO14.ToString();
                row3["CHHJ15"] = sVNSANGHO15.ToString();
                row3["CHHJ16"] = sVNSANGHO16.ToString();
                row3["CHHJ17"] = sVNSANGHO17.ToString();
                row3["CHHJ18"] = sVNSANGHO18.ToString();
                row3["CHHJ19"] = sVNSANGHO19.ToString();
                row3["CHHJ20"] = sVNSANGHO20.ToString();
                row3["CHHJ21"] = sVNSANGHO21.ToString();
                row3["CHHJ22"] = sVNSANGHO22.ToString();
                row3["CHHJ23"] = sVNSANGHO23.ToString();
                row3["CHHJ24"] = sVNSANGHO24.ToString();
                row3["CHHJ25"] = sVNSANGHO25.ToString();
                row3["CHHJ26"] = sVNSANGHO26.ToString();
                row3["CHHJ27"] = sVNSANGHO27.ToString();
                row3["CHHJ28"] = sVNSANGHO28.ToString();
                row3["CHHJ29"] = sVNSANGHO29.ToString();
                row3["CHHJ30"] = sVNSANGHO30.ToString();
                row3["CHHJ31"] = sVNSANGHO31.ToString();
                row3["CHHJ32"] = sVNSANGHO32.ToString();
                row3["CHMT1"] = dHAPTECHMT1;
                row3["CHMT2"] = dHAPTECHMT2;
                row3["CHMT3"] = dHAPTECHMT3;
                row3["CHMT4"] = dHAPTECHMT4;
                row3["CHMT5"] = dHAPTECHMT5;
                row3["CHMT6"] = dHAPTECHMT6;
                row3["CHMT7"] = dHAPTECHMT7;
                row3["CHMT8"] = dHAPTECHMT8;
                row3["CHMT9"] = dHAPTECHMT9;
                row3["CHMT10"] = dHAPTECHMT10;
                row3["CHMT11"] = dHAPTECHMT11;
                row3["CHMT12"] = dHAPTECHMT12;
                row3["CHMT13"] = dHAPTECHMT13;
                row3["CHMT14"] = dHAPTECHMT14;
                row3["CHMT15"] = dHAPTECHMT15;
                row3["CHMT16"] = dHAPTECHMT16;
                row3["CHMT17"] = dHAPTECHMT17;
                row3["CHMT18"] = dHAPTECHMT18;
                row3["CHMT19"] = dHAPTECHMT19;
                row3["CHMT20"] = dHAPTECHMT20;
                row3["CHMT21"] = dHAPTECHMT21;
                row3["CHMT22"] = dHAPTECHMT22;
                row3["CHMT23"] = dHAPTECHMT23;
                row3["CHMT24"] = dHAPTECHMT24;
                row3["CHMT25"] = dHAPTECHMT25;
                row3["CHMT26"] = dHAPTECHMT26;
                row3["CHMT27"] = dHAPTECHMT27;
                row3["CHMT28"] = dHAPTECHMT28;
                row3["CHMT29"] = dHAPTECHMT29;
                row3["CHMT30"] = dHAPTECHMT30;
                row3["CHMT31"] = dHAPTECHMT31;
                row3["CHMT32"] = dHAPTECHMT32;

                row3["CHMTHAP"] = (dHAPTECHMT1 + dHAPTECHMT2 + dHAPTECHMT3 + dHAPTECHMT4 + dHAPTECHMT5 + dHAPTECHMT6 + dHAPTECHMT7
                                  + dHAPTECHMT8 + dHAPTECHMT9 + dHAPTECHMT10 + dHAPTECHMT11 + dHAPTECHMT12 + dHAPTECHMT13 + dHAPTECHMT14
                                  + dHAPTECHMT15 + dHAPTECHMT16 + dHAPTECHMT17 + dHAPTECHMT18 + dHAPTECHMT19 + dHAPTECHMT20 + dHAPTECHMT21
                                  + dHAPTECHMT22 + dHAPTECHMT23 + dHAPTECHMT24 + dHAPTECHMT25 + dHAPTECHMT26 + dHAPTECHMT27 + dHAPTECHMT28
                                  + dHAPTECHMT29 + dHAPTECHMT30 + dHAPTECHMT31 + dHAPTECHMT32);

                row3["IPMT"] = dHAPTEIPMT;
                row3["JEGO"] = dHAPJEGO;
                row3["JUNJEGO"] = dJUNJEGO;
                row3["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                row3["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                row3["SDATE"] = sDATE.ToString();
                //row3["CHECK"] = sCHECK;

                retDt.Rows.Add(row3);

                // 재고 확인서 재고
                TXT01_CJJEQTY.Text = Convert.ToString(dJEGO);
            }
            catch
            {
            }
            return retDt;
        }
        #endregion

        #region Description : 재고 확인서
        private DataTable QueryDataSetJego()
        {

            string sYY = string.Empty;
            string sMM = string.Empty;
            string sDD = string.Empty;

            sYY = this.DTP01_EDDATE.GetString().Substring(0, 4);
            sMM = this.DTP01_EDDATE.GetString().Substring(4, 2);
            sDD = this.DTP01_EDDATE.GetString().Substring(6, 2);

            DataTable retDt = new DataTable();

            retDt.Columns.Add("YY", typeof(System.String));
            retDt.Columns.Add("MM", typeof(System.String));
            retDt.Columns.Add("DD", typeof(System.String));
            retDt.Columns.Add("HWAJU", typeof(System.String));
            retDt.Columns.Add("IRUM", typeof(System.String));
            retDt.Columns.Add("HWAMUL", typeof(System.String));
            retDt.Columns.Add("JEGO", typeof(System.String));

            try
            {
                DataRow row = retDt.NewRow();

                row["YY"] = sYY.ToString();
                row["MM"] = sMM.ToString();
                row["DD"] = sDD.ToString();
                row["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                row["IRUM"] = this.TXT01_REFER.GetValue().ToString();
                row["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();

                string sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 재고량 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_79EG0598", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    row["JEGO"] = dt.Rows[0]["TEJEMT"].ToString();
                }

                retDt.Rows.Add(row);
            }
            catch
            {
            }

            return retDt;
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport1()
        {

            double dTECHMT1 = 0;
            double dTECHMT2 = 0;
            double dTECHMT3 = 0;
            double dTECHMT4 = 0;
            double dTECHMT5 = 0;
            double dTECHMT6 = 0;
            double dTECHMT7 = 0;
            double dTECHMT8 = 0;
            double dTECHMT9 = 0;
            double dTECHMT10 = 0;
            double dTECHMT11 = 0;
            double dTECHMT12 = 0;
            double dTECHMT13 = 0;
            double dTECHMT14 = 0;
            double dTECHMT15 = 0;
            double dTECHMT16 = 0;
            double dTECHMT17 = 0;
            double dTECHMT18 = 0;
            double dTECHMT19 = 0;
            double dTECHMT20 = 0;
            double dTECHMT21 = 0;
            double dTECHMT22 = 0;
            double dTECHMT23 = 0;
            double dTECHMT24 = 0;
            double dTECHMT25 = 0;
            double dTECHMT26 = 0;
            double dTECHMT27 = 0;
            double dTECHMT28 = 0;
            double dTECHMT29 = 0;
            double dTECHMT30 = 0;
            double dTECHMT31 = 0;
            double dTECHMT32 = 0;
            double dTEIPMT = 0;
            double dJEGO = 0;
            double dJUNJEGO = 0;

            string sCHECK = string.Empty;

            string sSDATE = string.Empty;
            string sEDATE = string.Empty;
            string sDATE = string.Empty;

            string CHECK = "TRUE";

            sSDATE = this.DTP01_STDATE.GetString().Substring(0, 4) + "/" + this.DTP01_STDATE.GetString().Substring(4, 2) + "/" + this.DTP01_STDATE.GetString().Substring(6, 2);

            sEDATE = this.DTP01_EDDATE.GetString().Substring(0, 4) + "/" + this.DTP01_EDDATE.GetString().Substring(4, 2) + "/" + this.DTP01_EDDATE.GetString().Substring(6, 2);

            sDATE = "(" + sSDATE + "-" + sEDATE + ")";

            DataTable retDt = new DataTable();

            int i = 0;

            try
            {
                string sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 전일 재고를 가져옴

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_77BJ8133", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dJEGO = double.Parse(Get_Numeric(dt.Rows[0]["TEJEMT"].ToString()));
                    dJUNJEGO = dJEGO;
                }

                string sCHHJ = string.Empty;
                string DATE = string.Empty;
                string sYY = string.Empty;
                string sMM = string.Empty;
                string sDD = string.Empty;


                for (int j = int.Parse(this.DTP01_STDATE.GetString()); j < int.Parse(this.DTP01_EDDATE.GetString()) + 1; j++)
                {

                    if (Convert.ToString(j).Substring(6, 2).ToString() == "32")
                    {
                        sDD = "01";
                        sYY = Convert.ToString(j).Substring(0, 4).ToString();
                        sMM = Convert.ToString(j).Substring(4, 2).ToString();
                        if (sMM == "12")
                        {
                            sMM = "01";
                            sYY = Convert.ToString(int.Parse(sYY.ToString()) + 1);
                        }
                        else
                        {
                            sMM = Set_Fill2(Convert.ToString(int.Parse(sMM.ToString()) + 1));
                        }
                        j = int.Parse(sYY.ToString() + sMM.ToString() + sDD.ToString());
                    }

                    // 도착지별 내역을 가져옴
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_77BJ2135", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                j);

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        CHECK = "TRUE";
                    }
                    else
                    {
                        CHECK = "FALSE";
                    }

                    if (CHECK == "TRUE")
                    {
                        for (i = 0; i < 32; i++)
                        {
                            // 도착지별 내역을 가져옴
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_UT_77BJ4136", sVNCODE,
                                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                        sCHHJ,
                                                                        j);

                            dt = this.DbConnector.ExecuteDataTable();

                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                dTEIPMT = Convert.ToDouble(dt.Rows[x]["TEIPMT"].ToString());

                                switch (i)
                                {
                                    case 0:
                                        dTECHMT1 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 1:
                                        dTECHMT2 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 2:
                                        dTECHMT3 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 3:
                                        dTECHMT4 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 4:
                                        dTECHMT5 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 5:
                                        dTECHMT6 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 6:
                                        dTECHMT7 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 7:
                                        dTECHMT8 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 8:
                                        dTECHMT9 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 9:
                                        dTECHMT10 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 10:
                                        dTECHMT11 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 11:
                                        dTECHMT12 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 12:
                                        dTECHMT13 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 13:
                                        dTECHMT14 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 14:
                                        dTECHMT15 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 15:
                                        dTECHMT16 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 16:
                                        dTECHMT17 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 17:
                                        dTECHMT18 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 18:
                                        dTECHMT19 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 19:
                                        dTECHMT20 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 20:
                                        dTECHMT21 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 21:
                                        dTECHMT22 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 22:
                                        dTECHMT23 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 23:
                                        dTECHMT24 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 24:
                                        dTECHMT25 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 25:
                                        dTECHMT26 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 26:
                                        dTECHMT27 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 27:
                                        dTECHMT28 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 28:
                                        dTECHMT29 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 29:
                                        dTECHMT30 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 30:
                                        dTECHMT31 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;

                                    case 31:
                                        dTECHMT32 = double.Parse(dt.Rows[x]["TECHMT"].ToString());
                                        break;
                                }
                            }
                        }
                    }

                    if (CHECK == "TRUE")
                    {
                        // 재고량
                        dJEGO = dJEGO + dTEIPMT -
                            (dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6
                            + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12
                            + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16 + dTECHMT17 + dTECHMT18
                            + dTECHMT19 + dTECHMT20 + dTECHMT21 + dTECHMT22 + dTECHMT23 + dTECHMT24
                            + dTECHMT25 + dTECHMT26 + dTECHMT27 + dTECHMT28 + dTECHMT29 + dTECHMT30
                            + dTECHMT31 + dTECHMT32);
                    }

                }

                // 재고 확인서 재고
                TXT01_CJJEQTY.Text = Convert.ToString(dJEGO);
            }
            catch
            {
            }
            return retDt;
        }
        #endregion
    }
}
