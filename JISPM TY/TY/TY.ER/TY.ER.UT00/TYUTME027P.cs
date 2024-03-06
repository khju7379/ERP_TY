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
    /// 매출전표 집계표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.10.10 11:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_7AHED817 : 매출전표 집계표 임시파일 체크
    ///  TY_P_UT_7AHED818 : 매출전표 집계표 임시파일 삭제
    ///  TY_P_UT_7AHEM819 : 매출전표 집계표 전표건수 조회(T)
    ///  TY_P_UT_7AHEO820 : 매출전표 집계표 전표건수 조회
    ///  TY_P_UT_7AHES822 : 매출전표 집계표 전표번호 최소값 조회(T)
    ///  TY_P_UT_7AHET823 : 매출전표 집계표 전표번호 최소값 조회
    ///  TY_P_UT_7AHEU825 : 매출전표 집계표 전표번호 최대값 조회(T)
    ///  TY_P_UT_7AHEV826 : 매출전표 집계표 전표번호 최대값 조회
    ///  TY_P_UT_7AHF1827 : 매출전표 집계표 임시파일 생성
    ///  TY_P_UT_7AHFB828 : 매출전표 집계표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_7AIHL843 : 매출전표 집계표 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  KBSABUN : 사번
    ///  DATE : 일자
    ///  VSBRANCH : 사업장
    /// </summary>
    public partial class TYUTME027P : TYBase
    {
        #region Description : 폼 로드
        public TYUTME027P()
        {
            InitializeComponent();
        }

        private void TYUTME027P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_DATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.TXT01_VSBRANCH);
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                string sMAXB2NOSQ = "0";
                string sMINB2NOSQ = "0";
                string sCOUNT = "0";

                DataTable dt = new DataTable();

                //임시디비 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7AHED818");
                this.DbConnector.ExecuteNonQuery();

                if (this.TXT01_VSBRANCH.GetValue().ToString() == "T")
                {
                    // 전표번호 최소값 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7AHES822", this.DTP01_DATE.GetString(),
                                                                this.TXT01_VSBRANCH.GetValue().ToString(),
                                                                this.CBH01_KBSABUN.GetValue().ToString());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sMINB2NOSQ = dt.Rows[0]["B2NOSQ"].ToString();
                    }

                    // 전표번호 최대값 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7AHEU825", this.DTP01_DATE.GetString(),
                                                                this.TXT01_VSBRANCH.GetValue().ToString(),
                                                                this.CBH01_KBSABUN.GetValue().ToString());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sMAXB2NOSQ = dt.Rows[0]["B2NOSQ"].ToString();
                    }

                    // 전표 건수 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7AHEM819", this.DTP01_DATE.GetString(),
                                                                this.TXT01_VSBRANCH.GetValue().ToString(),
                                                                this.CBH01_KBSABUN.GetValue().ToString(),
                                                                this.DTP01_DATE.GetString());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sCOUNT = dt.Rows[0]["COUNT"].ToString();
                    }
                }
                else
                {
                    // 전표번호 최소값 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7AHET823", Get_Date(this.DTP01_DATE.GetValue().ToString()),
                                                                this.TXT01_VSBRANCH.GetValue().ToString().Substring(0, 1),
                                                                this.CBH01_KBSABUN.GetValue().ToString());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sMINB2NOSQ = dt.Rows[0]["B2NOSQ"].ToString();
                    }

                    // 전표번호 최대값 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7AHEV826", Get_Date(this.DTP01_DATE.GetValue().ToString()),
                                                                this.TXT01_VSBRANCH.GetValue().ToString().Substring(0, 1),
                                                                this.CBH01_KBSABUN.GetValue().ToString());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sMAXB2NOSQ = dt.Rows[0]["B2NOSQ"].ToString();
                    }

                    // 전표 건수 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7AHEO820", Get_Date(this.DTP01_DATE.GetValue().ToString()),
                                                                this.TXT01_VSBRANCH.GetValue().ToString().Substring(0, 1).ToString(),
                                                                this.CBH01_KBSABUN.GetValue().ToString(),
                                                                this.DTP01_DATE.GetString());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sCOUNT = dt.Rows[0]["COUNT"].ToString();
                    }
                }
                if (sMINB2NOSQ != "0" && sMAXB2NOSQ != "0" && sCOUNT != "0")
                {
                    string sVSBRANCH = this.TXT01_VSBRANCH.GetValue().ToString();
                    string sVSBRANCHNM = string.Empty;

                    if (sVSBRANCH == "S0" || sVSBRANCH == "S1" || sVSBRANCH == "S4")
                    {
                        sVSBRANCH = "S";
                        sVSBRANCHNM = "SILO 사업부";
                    }
                    else if (sVSBRANCH == "T")
                    {
                        sVSBRANCHNM = "T/TML 사업부";
                    }
                    else if (sVSBRANCH == "B1")
                    {
                        sVSBRANCHNM = "자원팀";
                    }
                    else if (sVSBRANCH == "B2")
                    {
                        sVSBRANCHNM = "화학팀";
                    }

                    // 생성 프로지서 호출 TY_P_UT_7AHF1827
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7AHF1827", sVSBRANCH,
                                                                sVSBRANCHNM,
                                                                this.DTP01_DATE.GetString(),
                                                                this.CBH01_KBSABUN.GetValue().ToString(),
                                                                sCOUNT,
                                                                "");
                    this.DbConnector.ExecuteScalar();

                    this.ShowMessage("TY_M_MR_2BF50354");
                }
                else
                {
                    // 등록되지않은 코드
                    this.ShowCustomMessage("등록되지 않은 코드입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7AHFB828");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTME027R();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_VSBRANCH.GetValue().ToString() != "T" && this.TXT01_VSBRANCH.GetValue().ToString() != "S0" &&
                this.TXT01_VSBRANCH.GetValue().ToString() != "S1" && this.TXT01_VSBRANCH.GetValue().ToString() != "S4" &&
                this.TXT01_VSBRANCH.GetValue().ToString() != "B" && this.TXT01_VSBRANCH.GetValue().ToString() != "B1" &&
                this.TXT01_VSBRANCH.GetValue().ToString() != "B2" && this.TXT01_VSBRANCH.GetValue().ToString() != "B5")
            {
                this.ShowCustomMessage("사업장을 확인하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                e.Successed = false;
                return;
            }
            else
            {
                if (!this.ShowMessage("TY_M_MR_2BF50353"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion        
    }
}
