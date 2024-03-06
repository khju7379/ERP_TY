using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 입고기준-월간출고현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.29 16:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_761FG709 : 월간출고현황(입고기준) 임시파일 삭제
    ///  TY_P_UT_761G2710 : 월간출고현황(입고기준) 임시파일 삭제(화주X)
    ///  TY_P_UT_761GD711 : 월간출고현황(입고기준) 처리 조회
    ///  TY_P_UT_761GE712 : 월간출고현황(입고기준) 처리 등록
    ///  TY_P_UT_767M1728 : 월간출고현황(입고기준) 출고화주 체크
    ///  TY_P_UT_767M4729 : 월간출고현황(입고기준) 도착지별 내역 조회
    ///  TY_P_UT_767M8726 : 월간출고현황(입고기준) 전일재고조회
    ///  TY_P_UT_767M9727 : 월간출고현황(입고기준) 도착지별 화주 조회
    ///  TY_P_UT_767MT732 : 월간출고현황(입고기준) 도착지별 내역조회(상세)
    ///  TY_P_UT_767MV733 : 월간출고현황(입고기준) 입고량 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_71BDP399 : 처리 중 오류가 발생하였습니다.
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
    ///  REFER : 참조인
    /// </summary>
    public partial class TYUTPR015P : TYBase
    {
        #region Descriptoin : 폼 로드
        public TYUTPR015P()
        {
            InitializeComponent();
        }

        private void TYUTPR015P_Load(object sender, System.EventArgs e)
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
                UP_Create_Temp();

                // 20180118 수정
                //UP_Create_Temp_upt();
            }

            this.ShowMessage("TY_M_MR_2BF50354");
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

                if (CBO01_PRGUBN.GetValue().ToString() ==  "M") // 월간출고
                {
                    TXT01_CJJEQTY.Text = "0";

                    dt = QueryDataSetReport();

                    // 20180118 수정
                    //dt = QueryDataSetReport_upt();

                    if (dt.Rows.Count > 0)
                    {
                        ActiveReport rpt = new TYUTPR015R1();
                        // 가로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
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

                        dt = QueryDataSetJego();

                        // 20180118 수정
                        //UP_Create_Temp_upt();

                        // 20180118 수정
                        //dt = QueryDataSetJego_upt();
                        



                        

                        if (dt.Rows.Count > 0)
                        {
                            ActiveReport rpt = new TYUTPR015R2();
                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
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
        private void UP_Create_Temp()
        {
            try
            {
                string sVNCODE = string.Empty;

                // 대표거래처코드 조회
                sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 임시파일 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_761FG709", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());
                this.DbConnector.ExecuteNonQueryList();

                DataTable dt = new DataTable();

                //TY_P_UT_761GD711

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_761GD711", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_761GE712", dt.Rows[i]["TEHWAJU"].ToString(),
                                                                        dt.Rows[i]["TEHWAMUL"].ToString(),
                                                                        dt.Rows[i]["TEDATE"].ToString(),
                                                                        dt.Rows[i]["TECHHJ"].ToString(),
                                                                        dt.Rows[i]["TECHMT"].ToString(),
                                                                        dt.Rows[i]["TEIPMT"].ToString(),
                                                                        dt.Rows[i]["TEJEMT"].ToString(),
                                                                        "A",
                                                                        System.DateTime.Now.ToString("yyyyMMdd"),
                                                                        System.DateTime.Now.ToString("HHmmssff"),
                                                                        TYUserInfo.EmpNo
                                                                        );

                    }
                    this.DbConnector.ExecuteNonQueryList();
                }
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
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
            double dTEIPMT = 0;
            double dJEGO = 0;

            int iCOUNT = 0;
            double dcount = 0;
            string sCHECK = string.Empty;

            string sSDATE = string.Empty;
            string sEDATE = string.Empty;
            string sDATE = string.Empty;

            string CHECK = "TRUE";
            string CHECK1 = "FALSE";

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

            retDt.Columns.Add("CHMTHAP", typeof(System.String));

            retDt.Columns.Add("IPMT", typeof(System.String));
            retDt.Columns.Add("JEGO", typeof(System.String));
            retDt.Columns.Add("HWAJU", typeof(System.String));
            retDt.Columns.Add("HWAMUL", typeof(System.String));
            retDt.Columns.Add("SDATE", typeof(System.String));
            retDt.Columns.Add("CHECK", typeof(System.String));

            try
            {
                string sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 전일 재고를 가져옴

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_767M8726", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dJEGO = double.Parse(Get_Numeric(dt.Rows[0]["TEJEMT"].ToString()));
                }

                // 도착지별 화주를 가져옴

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_767M9727", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
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
                row["CHMTHAP"] = 0;
                row["IPMT"] = 0;
                row["JEGO"] = 0;
                row["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                row["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                row["SDATE"] = sDATE.ToString();
                row["CHECK"] = "1";

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
                row1["CHMTHAP"] = 0;
                row1["IPMT"] = 0;
                row1["JEGO"] = dJEGO;
                row1["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                row1["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                row1["SDATE"] = sDATE.ToString();
                row1["CHECK"] = "1";

                retDt.Rows.Add(row1);

                string sCHHJ = string.Empty;
                string DATE = string.Empty;

                // 출고화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_767M1728", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 도착지별 내역 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_767M4729", sVNCODE,
                                                                this.CBH01_CHHWAMUL.GetValue().ToString());

                    dt = this.DbConnector.ExecuteDataTable();


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
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
                        dTEIPMT = 0;
                        DATE = "";

                        if (dcount > 30)
                        {
                            sCHECK = "2";
                        }
                        else if (dcount > 60)
                        {
                            sCHECK = "3";
                        }
                        else if (dcount > 90)
                        {
                            sCHECK = "4";
                        }
                        else if (dcount > 120)
                        {
                            sCHECK = "5";
                        }
                        else if (dcount > 150)
                        {
                            sCHECK = "6";
                        }
                        else if (dcount > 180)
                        {
                            sCHECK = "7";
                        }
                        else if (dcount > 210)
                        {
                            sCHECK = "8";
                        }
                        else if (dcount > 240)
                        {
                            sCHECK = "9";
                        }
                        else if (dcount > 270)
                        {
                            sCHECK = "10";
                        }
                        else
                        {
                            sCHECK = "1";
                        }

                        dTEIPMT = double.Parse(dt.Rows[i]["TEIPMT"].ToString());
                        dTECHMT1 = double.Parse(dt.Rows[i]["TECHMT"].ToString());

                        dJEGO = dJEGO + dTEIPMT - dTECHMT1;

                        DataRow row3 = retDt.NewRow();

                        dcount = dcount + 1;

                        row3["DATE"] = dt.Rows[i]["TEDATE"].ToString();
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
                        row3["CHMT1"] = dTECHMT1;
                        row3["CHMT2"] = dTECHMT2;
                        row3["CHMT3"] = dTECHMT3;
                        row3["CHMT4"] = dTECHMT4;
                        row3["CHMT5"] = dTECHMT5;
                        row3["CHMT6"] = dTECHMT6;
                        row3["CHMT7"] = dTECHMT7;
                        row3["CHMT8"] = dTECHMT8;
                        row3["CHMT9"] = dTECHMT9;
                        row3["CHMT10"] = dTECHMT10;
                        row3["CHMT11"] = dTECHMT11;
                        row3["CHMT12"] = dTECHMT12;
                        row3["CHMT13"] = dTECHMT13;
                        row3["CHMT14"] = dTECHMT14;
                        row3["CHMT15"] = dTECHMT15;
                        row3["CHMT16"] = dTECHMT16;
                        row3["CHMTHAP"] = dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6 + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12 + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16;
                        row3["IPMT"] = dTEIPMT;
                        row3["JEGO"] = dJEGO;
                        row3["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                        row3["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                        row3["SDATE"] = sDATE.ToString();
                        row3["CHECK"] = sCHECK;

                        retDt.Rows.Add(row3);
                    }
                }
                else
                {
                    for (int j = int.Parse(this.DTP01_STDATE.GetString()); j < int.Parse(this.DTP01_EDDATE.GetString()) + 1; j++)
                    {
                        string sSTDATE = string.Empty;
                        string sYY = string.Empty;
                        string sMM = string.Empty;

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
                        dTEIPMT = 0;
                        DATE = "";

                        if (dcount > 30)
                        {
                            sCHECK = "2";
                        }
                        else if (dcount > 60)
                        {
                            sCHECK = "3";
                        }
                        else if (dcount > 90)
                        {
                            sCHECK = "4";
                        }
                        else if (dcount > 120)
                        {
                            sCHECK = "5";
                        }
                        else if (dcount > 150)
                        {
                            sCHECK = "6";
                        }
                        else if (dcount > 180)
                        {
                            sCHECK = "7";
                        }
                        else if (dcount > 210)
                        {
                            sCHECK = "8";
                        }
                        else if (dcount > 240)
                        {
                            sCHECK = "9";
                        }
                        else if (dcount > 270)
                        {
                            sCHECK = "10";
                        }
                        else
                        {
                            sCHECK = "1";
                        }

                        CHECK = "TRUE";
                        CHECK1 = "FALSE";

                        for (int i = 0; i < 16; i++)
                        {
                            if (i == 0)
                            {
                                sCHHJ = sCHHJ1.ToString();
                            }
                            else if (i == 1)
                            {
                                sCHHJ = sCHHJ2.ToString();
                            }
                            else if (i == 2)
                            {
                                sCHHJ = sCHHJ3.ToString();
                            }
                            else if (i == 3)
                            {
                                sCHHJ = sCHHJ4.ToString();
                            }
                            else if (i == 4)
                            {
                                sCHHJ = sCHHJ5.ToString();
                            }
                            else if (i == 5)
                            {
                                sCHHJ = sCHHJ6.ToString();
                            }
                            else if (i == 6)
                            {
                                sCHHJ = sCHHJ7.ToString();
                            }
                            else if (i == 7)
                            {
                                sCHHJ = sCHHJ8.ToString();
                            }
                            else if (i == 8)
                            {
                                sCHHJ = sCHHJ9.ToString();
                            }
                            else if (i == 9)
                            {
                                sCHHJ = sCHHJ10.ToString();
                            }
                            else if (i == 10)
                            {
                                sCHHJ = sCHHJ11.ToString();
                            }
                            else if (i == 11)
                            {
                                sCHHJ = sCHHJ12.ToString();
                            }
                            else if (i == 12)
                            {
                                sCHHJ = sCHHJ13.ToString();
                            }
                            else if (i == 13)
                            {
                                sCHHJ = sCHHJ14.ToString();
                            }
                            else if (i == 14)
                            {
                                sCHHJ = sCHHJ15.ToString();
                            }
                            else if (i == 15)
                            {
                                sCHHJ = sCHHJ16.ToString();
                            }

                            if (sCHHJ != "")
                            {
                                // 도착지별 내역을 가져옴
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_UT_767MT732", sVNCODE,
                                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                            sCHHJ.ToString(),
                                                                            j);

                                dt = this.DbConnector.ExecuteDataTable();

                                if (dt.Rows.Count > 0)
                                {
                                    CHECK1 = "TRUE";
                                    DATE = Convert.ToString(j);
                                    if (i == 0)
                                    {
                                        dTECHMT1 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 1)
                                    {
                                        dTECHMT2 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 2)
                                    {
                                        dTECHMT3 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 3)
                                    {
                                        dTECHMT4 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 4)
                                    {
                                        dTECHMT5 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 5)
                                    {
                                        dTECHMT6 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 6)
                                    {
                                        dTECHMT7 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 7)
                                    {
                                        dTECHMT8 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 8)
                                    {
                                        dTECHMT9 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 9)
                                    {
                                        dTECHMT10 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 10)
                                    {
                                        dTECHMT11 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 11)
                                    {
                                        dTECHMT12 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 12)
                                    {
                                        dTECHMT13 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 13)
                                    {
                                        dTECHMT14 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 14)
                                    {
                                        dTECHMT15 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                    else if (i == 15)
                                    {
                                        dTECHMT16 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                    }
                                }
                                else
                                {
                                    if (CHECK1 != "TRUE")
                                    {
                                        CHECK = "FALSE";
                                    }
                                }
                            }

                            // 입고량을 가져옴
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_UT_767MV733", sVNCODE,
                                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                        j);

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                CHECK1 = "TRUE";
                                DATE = Convert.ToString(j);
                                dTEIPMT = double.Parse(dt.Rows[0]["TEIPMT"].ToString());
                            }
                            else
                            {
                                if (CHECK1 != "TRUE")
                                {
                                    CHECK = "FALSE";
                                }
                            }
                        }
                        // 재고량
                        dJEGO = dJEGO + dTEIPMT -
                            (dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6
                            + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12
                            + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16);

                        if (CHECK == "TRUE" || CHECK1 == "TRUE")
                        {
                            DataRow row2 = retDt.NewRow();

                            dcount = dcount + 1;

                            row2["DATE"] = Convert.ToString(j);
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
                            row2["CHMTHAP"] = dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6 + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12 + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16;
                            row2["IPMT"] = dTEIPMT;
                            row2["JEGO"] = dJEGO;
                            row2["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                            row2["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                            row2["SDATE"] = sDATE.ToString();
                            row2["CHECK"] = sCHECK;

                            retDt.Rows.Add(row2);
                        }
                        if (Convert.ToString(j).Substring(6, 2).ToString() == "31")
                        {
                            sYY = Convert.ToString(j).Substring(0, 4).ToString();
                            sMM = Convert.ToString(j).Substring(4, 2).ToString();
                            if (Convert.ToString(j).Substring(4, 2).ToString() == "12")
                            {
                                sYY = Convert.ToString(int.Parse(sYY) + 1);
                                sMM = "01";
                            }
                            else
                            {
                                sMM = Set_Fill2(Convert.ToString(int.Parse(sMM) + 1));
                            }

                            sSTDATE = sYY + sMM + "00";
                            j = int.Parse(sSTDATE);
                        }
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




        #region Description : 임시파일 생성(20180118 수정)
        private void UP_Create_Temp_upt()
        {
            try
            {
                string sVNCODE = string.Empty;

                // 대표거래처코드 조회
                sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 임시파일 삭제
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_81G8U460", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                this.DbConnector.ExecuteNonQueryList();

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81G8V461", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_761GE712", dt.Rows[i]["TEHWAJU"].ToString(),
                                                                    dt.Rows[i]["TEHWAMUL"].ToString(),
                                                                    dt.Rows[i]["TEDATE"].ToString(),
                                                                    dt.Rows[i]["TECHHJ"].ToString(),
                                                                    dt.Rows[i]["TECHMT"].ToString(),
                                                                    dt.Rows[i]["TEIPMT"].ToString(),
                                                                    dt.Rows[i]["TEJEMT"].ToString(),
                                                                    "A",
                                                                    System.DateTime.Now.ToString("yyyyMMdd"),
                                                                    System.DateTime.Now.ToString("HHmmssff"),
                                                                    TYUserInfo.EmpNo
                                                                    );

                    }
                    this.DbConnector.ExecuteNonQueryList();
                }
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 데이터셋 변경(20180118 수정)
        private DataTable QueryDataSetReport_upt()
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

            string sTEHWAMUL = string.Empty;

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
            double dTEIPMT = 0;
            double dJEGO = 0;

            int iCOUNT = 0;
            double dcount = 0;
            string sCHECK = string.Empty;

            string sSDATE = string.Empty;
            string sEDATE = string.Empty;
            string sDATE = string.Empty;

            string CHECK = "TRUE";
            string CHECK1 = "FALSE";

            sSDATE = this.DTP01_STDATE.GetString().Substring(0, 4) + "/" + this.DTP01_STDATE.GetString().Substring(4, 2) + "/" + this.DTP01_STDATE.GetString().Substring(6, 2);

            sEDATE = this.DTP01_EDDATE.GetString().Substring(0, 4) + "/" + this.DTP01_EDDATE.GetString().Substring(4, 2) + "/" + this.DTP01_EDDATE.GetString().Substring(6, 2);

            sDATE = "(" + sSDATE + "-" + sEDATE + ")";

            DataTable retDt = new DataTable();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

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

            retDt.Columns.Add("CHMTHAP", typeof(System.String));

            retDt.Columns.Add("IPMT", typeof(System.String));
            retDt.Columns.Add("JEGO", typeof(System.String));
            retDt.Columns.Add("HWAJU", typeof(System.String));
            retDt.Columns.Add("HWAMUL", typeof(System.String));
            retDt.Columns.Add("SDATE", typeof(System.String));
            retDt.Columns.Add("CHECK", typeof(System.String));

            int k = 0;

            try
            {
                string sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 전일 재고를 가져옴

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81G9A462", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    for (k = 0; k < dt1.Rows.Count; k++)
                    {
                        sTEDATE = "";
                        sTECHHJ = "";

                        sCHHJ1 = "";
                        sCHHJ2 = "";
                        sCHHJ3 = "";
                        sCHHJ4 = "";
                        sCHHJ5 = "";
                        sCHHJ6 = "";
                        sCHHJ7 = "";
                        sCHHJ8 = "";
                        sCHHJ9 = "";
                        sCHHJ10 = "";
                        sCHHJ11 = "";
                        sCHHJ12 = "";
                        sCHHJ13 = "";
                        sCHHJ14 = "";
                        sCHHJ15 = "";
                        sCHHJ16 = "";

                        sVNSANGHO1 = "";
                        sVNSANGHO2 = "";
                        sVNSANGHO3 = "";
                        sVNSANGHO4 = "";
                        sVNSANGHO5 = "";
                        sVNSANGHO6 = "";
                        sVNSANGHO7 = "";
                        sVNSANGHO8 = "";
                        sVNSANGHO9 = "";
                        sVNSANGHO10 = "";
                        sVNSANGHO11 = "";
                        sVNSANGHO12 = "";
                        sVNSANGHO13 = "";
                        sVNSANGHO14 = "";
                        sVNSANGHO15 = "";
                        sVNSANGHO16 = "";

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
                        dTEIPMT   = 0;
                        dJEGO     = 0;

                        sTEHWAMUL = "";

                        dJEGO     = double.Parse(Get_Numeric(dt1.Rows[k]["TEJEMT"].ToString()));
                        sTEHWAMUL = dt1.Rows[k]["TEHWAMUL"].ToString();


                        // 도착지별 화주를 가져옴

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_767M9727", sVNCODE,
                                                                    sTEHWAMUL.ToString());

                        dt = this.DbConnector.ExecuteDataTable();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
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
                        row["CHMTHAP"] = 0;
                        row["IPMT"] = 0;
                        row["JEGO"] = 0;
                        row["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                        row["HWAMUL"] = sTEHWAMUL.ToString();
                        row["SDATE"] = sDATE.ToString();
                        row["CHECK"] = "1";

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
                        row1["CHMTHAP"] = 0;
                        row1["IPMT"] = 0;
                        row1["JEGO"] = dJEGO;
                        row1["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                        row1["HWAMUL"] = sTEHWAMUL.ToString();
                        row1["SDATE"] = sDATE.ToString();
                        row1["CHECK"] = "1";

                        retDt.Rows.Add(row1);

                        string sCHHJ = string.Empty;
                        string DATE = string.Empty;

                        // 출고화주 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_767M1728", sVNCODE,
                                                                    sTEHWAMUL.ToString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 도착지별 내역 조회
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_UT_767M4729", sVNCODE,
                                                                        sTEHWAMUL.ToString());

                            dt = this.DbConnector.ExecuteDataTable();


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
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
                                dTEIPMT = 0;
                                DATE = "";

                                if (dcount > 30)
                                {
                                    sCHECK = "2";
                                }
                                else if (dcount > 60)
                                {
                                    sCHECK = "3";
                                }
                                else if (dcount > 90)
                                {
                                    sCHECK = "4";
                                }
                                else if (dcount > 120)
                                {
                                    sCHECK = "5";
                                }
                                else if (dcount > 150)
                                {
                                    sCHECK = "6";
                                }
                                else if (dcount > 180)
                                {
                                    sCHECK = "7";
                                }
                                else if (dcount > 210)
                                {
                                    sCHECK = "8";
                                }
                                else if (dcount > 240)
                                {
                                    sCHECK = "9";
                                }
                                else if (dcount > 270)
                                {
                                    sCHECK = "10";
                                }
                                else
                                {
                                    sCHECK = "1";
                                }

                                dTEIPMT = double.Parse(dt.Rows[i]["TEIPMT"].ToString());
                                dTECHMT1 = double.Parse(dt.Rows[i]["TECHMT"].ToString());

                                dJEGO = dJEGO + dTEIPMT - dTECHMT1;

                                DataRow row3 = retDt.NewRow();

                                dcount = dcount + 1;

                                row3["DATE"] = dt.Rows[i]["TEDATE"].ToString();
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
                                row3["CHMT1"] = dTECHMT1;
                                row3["CHMT2"] = dTECHMT2;
                                row3["CHMT3"] = dTECHMT3;
                                row3["CHMT4"] = dTECHMT4;
                                row3["CHMT5"] = dTECHMT5;
                                row3["CHMT6"] = dTECHMT6;
                                row3["CHMT7"] = dTECHMT7;
                                row3["CHMT8"] = dTECHMT8;
                                row3["CHMT9"] = dTECHMT9;
                                row3["CHMT10"] = dTECHMT10;
                                row3["CHMT11"] = dTECHMT11;
                                row3["CHMT12"] = dTECHMT12;
                                row3["CHMT13"] = dTECHMT13;
                                row3["CHMT14"] = dTECHMT14;
                                row3["CHMT15"] = dTECHMT15;
                                row3["CHMT16"] = dTECHMT16;
                                row3["CHMTHAP"] = dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6 + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12 + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16;
                                row3["IPMT"] = dTEIPMT;
                                row3["JEGO"] = dJEGO;
                                row3["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                                row3["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                                row3["SDATE"] = sDATE.ToString();
                                row3["CHECK"] = sCHECK;

                                retDt.Rows.Add(row3);
                            }
                        }
                        else
                        {
                            for (int j = int.Parse(this.DTP01_STDATE.GetString()); j < int.Parse(this.DTP01_EDDATE.GetString()) + 1; j++)
                            {
                                string sSTDATE = string.Empty;
                                string sYY = string.Empty;
                                string sMM = string.Empty;

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
                                dTEIPMT = 0;
                                DATE = "";

                                if (dcount > 30)
                                {
                                    sCHECK = "2";
                                }
                                else if (dcount > 60)
                                {
                                    sCHECK = "3";
                                }
                                else if (dcount > 90)
                                {
                                    sCHECK = "4";
                                }
                                else if (dcount > 120)
                                {
                                    sCHECK = "5";
                                }
                                else if (dcount > 150)
                                {
                                    sCHECK = "6";
                                }
                                else if (dcount > 180)
                                {
                                    sCHECK = "7";
                                }
                                else if (dcount > 210)
                                {
                                    sCHECK = "8";
                                }
                                else if (dcount > 240)
                                {
                                    sCHECK = "9";
                                }
                                else if (dcount > 270)
                                {
                                    sCHECK = "10";
                                }
                                else
                                {
                                    sCHECK = "1";
                                }

                                CHECK = "TRUE";
                                CHECK1 = "FALSE";

                                for (int i = 0; i < 16; i++)
                                {
                                    if (i == 0)
                                    {
                                        sCHHJ = sCHHJ1.ToString();
                                    }
                                    else if (i == 1)
                                    {
                                        sCHHJ = sCHHJ2.ToString();
                                    }
                                    else if (i == 2)
                                    {
                                        sCHHJ = sCHHJ3.ToString();
                                    }
                                    else if (i == 3)
                                    {
                                        sCHHJ = sCHHJ4.ToString();
                                    }
                                    else if (i == 4)
                                    {
                                        sCHHJ = sCHHJ5.ToString();
                                    }
                                    else if (i == 5)
                                    {
                                        sCHHJ = sCHHJ6.ToString();
                                    }
                                    else if (i == 6)
                                    {
                                        sCHHJ = sCHHJ7.ToString();
                                    }
                                    else if (i == 7)
                                    {
                                        sCHHJ = sCHHJ8.ToString();
                                    }
                                    else if (i == 8)
                                    {
                                        sCHHJ = sCHHJ9.ToString();
                                    }
                                    else if (i == 9)
                                    {
                                        sCHHJ = sCHHJ10.ToString();
                                    }
                                    else if (i == 10)
                                    {
                                        sCHHJ = sCHHJ11.ToString();
                                    }
                                    else if (i == 11)
                                    {
                                        sCHHJ = sCHHJ12.ToString();
                                    }
                                    else if (i == 12)
                                    {
                                        sCHHJ = sCHHJ13.ToString();
                                    }
                                    else if (i == 13)
                                    {
                                        sCHHJ = sCHHJ14.ToString();
                                    }
                                    else if (i == 14)
                                    {
                                        sCHHJ = sCHHJ15.ToString();
                                    }
                                    else if (i == 15)
                                    {
                                        sCHHJ = sCHHJ16.ToString();
                                    }

                                    if (sCHHJ != "")
                                    {
                                        // 도착지별 내역을 가져옴
                                        this.DbConnector.CommandClear();
                                        this.DbConnector.Attach("TY_P_UT_767MT732", sVNCODE,
                                                                                    sTEHWAMUL.ToString(),
                                                                                    sCHHJ.ToString(),
                                                                                    j);

                                        dt = this.DbConnector.ExecuteDataTable();

                                        if (dt.Rows.Count > 0)
                                        {
                                            CHECK1 = "TRUE";
                                            DATE = Convert.ToString(j);
                                            if (i == 0)
                                            {
                                                dTECHMT1 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 1)
                                            {
                                                dTECHMT2 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 2)
                                            {
                                                dTECHMT3 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 3)
                                            {
                                                dTECHMT4 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 4)
                                            {
                                                dTECHMT5 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 5)
                                            {
                                                dTECHMT6 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 6)
                                            {
                                                dTECHMT7 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 7)
                                            {
                                                dTECHMT8 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 8)
                                            {
                                                dTECHMT9 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 9)
                                            {
                                                dTECHMT10 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 10)
                                            {
                                                dTECHMT11 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 11)
                                            {
                                                dTECHMT12 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 12)
                                            {
                                                dTECHMT13 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 13)
                                            {
                                                dTECHMT14 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 14)
                                            {
                                                dTECHMT15 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                            else if (i == 15)
                                            {
                                                dTECHMT16 = double.Parse(dt.Rows[i]["TECHMT"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (CHECK1 != "TRUE")
                                            {
                                                CHECK = "FALSE";
                                            }
                                        }
                                    }

                                    // 입고량을 가져옴
                                    this.DbConnector.CommandClear();
                                    this.DbConnector.Attach("TY_P_UT_767MV733", sVNCODE,
                                                                                sTEHWAMUL.ToString(),
                                                                                j);

                                    dt = this.DbConnector.ExecuteDataTable();

                                    if (dt.Rows.Count > 0)
                                    {
                                        CHECK1 = "TRUE";
                                        DATE = Convert.ToString(j);
                                        dTEIPMT = double.Parse(dt.Rows[0]["TEIPMT"].ToString());
                                    }
                                    else
                                    {
                                        if (CHECK1 != "TRUE")
                                        {
                                            CHECK = "FALSE";
                                        }
                                    }
                                }
                                // 재고량
                                dJEGO = dJEGO + dTEIPMT -
                                    (dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6
                                    + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12
                                    + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16);

                                if (CHECK == "TRUE" || CHECK1 == "TRUE")
                                {
                                    DataRow row2 = retDt.NewRow();

                                    dcount = dcount + 1;

                                    row2["DATE"] = Convert.ToString(j);
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
                                    row2["CHMTHAP"] = dTECHMT1 + dTECHMT2 + dTECHMT3 + dTECHMT4 + dTECHMT5 + dTECHMT6 + dTECHMT7 + dTECHMT8 + dTECHMT9 + dTECHMT10 + dTECHMT11 + dTECHMT12 + dTECHMT13 + dTECHMT14 + dTECHMT15 + dTECHMT16;
                                    row2["IPMT"] = dTEIPMT;
                                    row2["JEGO"] = dJEGO;
                                    row2["HWAJU"] = this.CBH01_CHHWAJU.GetText().ToString();
                                    row2["HWAMUL"] = this.CBH01_CHHWAMUL.GetText().ToString();
                                    row2["SDATE"] = sDATE.ToString();
                                    row2["CHECK"] = sCHECK;

                                    retDt.Rows.Add(row2);
                                }
                                if (Convert.ToString(j).Substring(6, 2).ToString() == "31")
                                {
                                    sYY = Convert.ToString(j).Substring(0, 4).ToString();
                                    sMM = Convert.ToString(j).Substring(4, 2).ToString();
                                    if (Convert.ToString(j).Substring(4, 2).ToString() == "12")
                                    {
                                        sYY = Convert.ToString(int.Parse(sYY) + 1);
                                        sMM = "01";
                                    }
                                    else
                                    {
                                        sMM = Set_Fill2(Convert.ToString(int.Parse(sMM) + 1));
                                    }

                                    sSTDATE = sYY + sMM + "00";
                                    j = int.Parse(sSTDATE);
                                }
                            }
                        }
                        // 재고 확인서 재고
                        TXT01_CJJEQTY.Text = Convert.ToString(dJEGO);
                    }
                }
            }
            catch
            {
            }
            return retDt;
        }
        #endregion

        #region Description : 재고 확인서(20180118 수정)
        private DataTable QueryDataSetJego_upt()
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
                string sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                // 재고량 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81G9M465", sVNCODE,
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow row = retDt.NewRow();

                    row["YY"]     = sYY.ToString();
                    row["MM"]     = sMM.ToString();
                    row["DD"]     = sDD.ToString();
                    row["HWAJU"]  = this.CBH01_CHHWAJU.GetText().ToString();
                    row["IRUM"]   = this.TXT01_REFER.GetValue().ToString();
                    row["HWAMUL"] = dt.Rows[0]["TEHWAMUL"].ToString();

                    row["JEGO"]   = dt.Rows[0]["TEJEMT"].ToString();

                    retDt.Rows.Add(row);
                }
            }
            catch
            {
            }

            return retDt;
        }
        #endregion

    }
}
