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
    /// 거래처별 매출현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.17 17:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_699DO131 : 거래처별 매출현황 출력(화주X)
    ///  TY_P_UT_699DS132 : 거래처별 매출현황 출력(화주O)
    ///  TY_P_UT_6AHHZ392 : 거래처별 매출현황 임시파일 삭제
    ///  TY_P_UT_6AHI0398 : 거래처별 매출현황 보관 취급료 조회
    ///  TY_P_UT_6AHI4393 : 거래처별 매출현황 임시파일 생성(화주O)
    ///  TY_P_UT_6AHI5395 : 거래처별 매출현황 임시파일 생성(화주X)
    ///  TY_P_UT_6AIA0401 : 거래처별 매출현황 임시파일 존재유무
    ///  TY_P_UT_6AIA8400 : 거래처별 매출현황 임시파일 수정(보관 취급료)
    ///  TY_P_UT_6AIAB402 : 거래처별 매출현황 임시파일 등록(보관 취급료)
    ///  TY_P_UT_6AIAD403 : 거래처별 매출현황 하역료 조회
    ///  TY_P_UT_6AIAE404 : 거래처별 매출현황 임시파일 수정(하역료)
    ///  TY_P_UT_6AIAH405 : 거래처별 매출현황 임시파일 등록(하역료)
    ///  TY_P_UT_6AIB4406 : 거래처별 매출현황 수정세금계산서 조회
    ///  TY_P_UT_6AIBA407 : 거래처별 매출현황 임시파일 수정(세금계산서)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CMHWAJU : 화주
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUTSU001P : TYBase
    {
        #region Description : 페이지 로드
        public TYUTSU001P()
        {
            InitializeComponent();
        }

        private void TYUTSU001P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Descriptoin : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // 임시파일 생성
            UP_GetBatch();

            this.FPS91_TY_S_UT_7ABHJ758.Initialize();

            string sVNCODE = this.CBH01_CMHWAJU.GetValue().ToString();

            if (sVNCODE != "")
            {
                // 대표거래처 코드 가져오기
                sVNCODE = Get_VNCODE(sVNCODE);

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_7ABHW759", sVNCODE);

                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_7ABHW760");

                dt = this.DbConnector.ExecuteDataTable();
            }

            this.FPS91_TY_S_UT_7ABHJ758.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            // 임시파일 생성
            UP_GetBatch();

            UP_Print();
        }

        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_SDATE.GetString();
            string EDDATE = this.DTP01_EDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.DTP01_SDATE.Focus();
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 임시파일 생성
        private void UP_GetBatch()
        {
            try
            {
                string sMCHWAJU = string.Empty;
                string sVNCODE = this.CBH01_CMHWAJU.GetValue().ToString();
                string sBIGO = this.DTP01_EDATE.GetString();
                bool bResult;

                DataTable dt = new DataTable();

                //재고 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6AHHZ392");

                this.DbConnector.ExecuteNonQuery();

                if (sVNCODE != "")
                {
                    // 대표거래처 코드 가져오기
                    sVNCODE = Get_VNCODE(sVNCODE);
                }

                // READ_PROC
                if (sVNCODE != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6AHI4393", sBIGO,
                                                                this.DTP01_SDATE.GetString(),
                                                                this.DTP01_EDATE.GetString(),
                                                                sVNCODE);
                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6AHI5395", sBIGO,
                                                                this.DTP01_SDATE.GetString(),
                                                                this.DTP01_EDATE.GetString());
                    this.DbConnector.ExecuteNonQuery();
                }

                // MECH-PROC(TYI, TYC 화주로 보관료, 취급료 임시파일에 저장 
                // 2018-11-08 윤현진 주임 요청으로 전표번호가 존재하는 자료만 조회되도록 수정 TY_P_UT_6AHI0398
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6AHI0398", this.DTP01_SDATE.GetString(),
                                                            this.DTP01_EDATE.GetString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sMCHWAJU = dt.Rows[i]["T9HWAJU"].ToString();

                        // TEMP 파일 체크
                        bResult = TEMP_CHECK(sMCHWAJU);

                        // UPDATE
                        if (bResult == true)
                        {
                            this.DbConnector.Attach("TY_P_UT_6AIA8400", dt.Rows[i]["T9BOKAMT"].ToString(),
                                                                        dt.Rows[i]["T9HANDAMT"].ToString(),
                                                                        dt.Rows[i]["T9SUBTOT"].ToString(),
                                                                        dt.Rows[i]["T9SUBVAT"].ToString(),
                                                                        sMCHWAJU,
                                                                        "1");
                        }
                        // INSERT
                        else
                        {
                            this.DbConnector.Attach("TY_P_UT_6AIAB402", "1",
                                                                        sMCHWAJU,
                                                                        dt.Rows[i]["T9BOKAMT"].ToString(),
                                                                        dt.Rows[i]["T9HANDAMT"].ToString(),
                                                                        dt.Rows[i]["T9SUBTOT"].ToString(),
                                                                        dt.Rows[i]["T9SUBVAT"].ToString(),
                                                                        sBIGO);
                        }
                    }
                    this.DbConnector.ExecuteNonQueryList();
                }

                // MEC1_PROC(TYI, TYC 화주로 하역료 임시파일에 저장)
                // 2018-11-08 윤현진 주임 요청으로 전표번호가 존재하는 자료만 조회되도록 수정 TY_P_UT_6AIAD403
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6AIAD403", this.DTP01_SDATE.GetString(),
                                                            this.DTP01_EDATE.GetString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sMCHWAJU = dt.Rows[i]["T9HWAJU"].ToString();

                        // TEMP 파일 체크
                        bResult = TEMP_CHECK(sMCHWAJU);

                        // UPDATE
                        if (bResult == true)
                        {
                            this.DbConnector.Attach("TY_P_UT_6AIAE404", "1",
                                                                        sMCHWAJU,
                                                                        dt.Rows[i]["T9HAYAMT"].ToString(),
                                                                        dt.Rows[i]["T9SUBTOT"].ToString(),
                                                                        dt.Rows[i]["T9SUBVAT"].ToString());
                        }
                        // INSERT
                        else
                        {
                            this.DbConnector.Attach("TY_P_UT_6AIAH405", "1",
                                                                        sMCHWAJU,
                                                                        dt.Rows[i]["T9HAYAMT"].ToString(),
                                                                        dt.Rows[i]["T9SUBTOT"].ToString(),
                                                                        dt.Rows[i]["T9SUBVAT"].ToString(),
                                                                        sBIGO);
                        }
                    }
                    this.DbConnector.ExecuteNonQueryList();
                }

                // 수정세금계산서
                if (sVNCODE != "")
                {
                    this.DbConnector.CommandClear();


                    this.DbConnector.Attach("TY_P_UT_6AIB4406", this.DTP01_SDATE.GetString(),
                                                                this.DTP01_EDATE.GetString(),
                                                                sVNCODE);

                    dt = this.DbConnector.ExecuteDataTable();
                }
                else
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_7ABGI753", this.DTP01_SDATE.GetString(),
                                                                this.DTP01_EDATE.GetString());

                    dt = this.DbConnector.ExecuteDataTable();
                }

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_6AIBA407", dt.Rows[i]["T9BOKAMT"].ToString(),
                                                                    dt.Rows[i]["T9HANDAMT"].ToString(),
                                                                    dt.Rows[i]["T9HAYAMT"].ToString(),
                                                                    dt.Rows[i]["T9JUBAMT"].ToString(),
                                                                    dt.Rows[i]["T9SUBTOT"].ToString(),
                                                                    dt.Rows[i]["T9SUBVAT"].ToString(),
                                                                    dt.Rows[i]["T9INSRAMT"].ToString(),
                                                                    dt.Rows[i]["UTHWAJU"].ToString(),
                                                                    dt.Rows[i]["T9GUBUN"].ToString());

                    }
                    this.DbConnector.ExecuteNonQueryList();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region Description : 임시파일 체크
        private bool TEMP_CHECK(string sMCHWAJU)
        {
            bool bRtn = false;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6AIA0401", sMCHWAJU);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = true;
            }

            return bRtn;
        }
        #endregion

        #region Description : 출력 함수
        private void UP_Print()
        {
            string sVNCODE = this.CBH01_CMHWAJU.GetValue().ToString();
            string sSDATE = this.DTP01_SDATE.GetString().Substring(0, 4) + "/" + this.DTP01_SDATE.GetString().Substring(4, 2) + "/" + this.DTP01_SDATE.GetString().Substring(6, 2);
            string sEDATE = this.DTP01_EDATE.GetString().Substring(0, 4) + "/" + this.DTP01_EDATE.GetString().Substring(4, 2) + "/" + this.DTP01_EDATE.GetString().Substring(6, 2);

            string sDATE = "(" + sSDATE + "~" + sEDATE + ")";

            DataTable dt = new DataTable();

            if (sVNCODE != "")
            {
                // 대표거래처 코드 가져오기
                sVNCODE = Get_VNCODE(sVNCODE);

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_699DS132", sDATE,
                                                            sVNCODE);

                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_699DO131", sDATE);

                dt = this.DbConnector.ExecuteDataTable();
            }

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTSU001R();
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }

        }
        #endregion
    }
}
