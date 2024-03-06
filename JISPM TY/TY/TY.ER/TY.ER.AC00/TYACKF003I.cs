using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금계획관리-팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.12.27 14:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CR6O389 : 자금계획 등록
    ///  TY_P_AC_31331469 : 자금계획 확인
    ///  TY_P_AC_31332470 : 자금계획 - 순번 가져오기
    ///  TY_P_AC_313A2460 : 자금계획 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_3131B466 : 부서코드를 확인하세요.
    ///  TY_M_AC_31342476 : B/L번호를 확인하세요.
    ///  TY_M_AC_31343477 : 환율을 확인하세요.
    ///  TY_M_AC_31344479 : 화폐를 입력하세요.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  A1ACHL1 : 상위계정코드１
    ///  PHAMTGN : 화폐
    ///  PHBANK : 은행
    ///  PHCDAC : 계정과목
    ///  PHCDFD : 자금항목
    ///  PHDPAC : 부　　서
    ///  PHSABUN : 사　　번
    ///  PHVEND : 거래처
    ///  PHFDGUBN : 자금구분
    ///  PHSTGBN : 상태구분
    ///  PHIPDATE : 적용일자
    ///  PHAIAMT : 외화금액
    ///  PHAWAMT : 원화금액
    ///  PHBLGUBN : 구분
    ///  PHBLNO : BL번호
    ///  PHBLSABN : 사번
    ///  PHBLSQNO : 계획번호
    ///  PHBLYYNO : 계획년도
    ///  PHLCBLNO : L/C번호
    ///  PHNOSQ : 순　번
    ///  PHRKAC : 적　　요
    ///  PHYUL : 환율
    /// </summary>
    public partial class TYACKF003I : TYBase
    {
        string fsPHDPAC  = string.Empty;
        string fsPHSABUN = string.Empty;
        string fsPHNOSQ  = string.Empty;

        #region Description : 페이지 로드
        public TYACKF003I(string sPHDPAC, string sPHSABUN, string sPHNOSQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsPHDPAC  = sPHDPAC;
            this.fsPHSABUN = sPHSABUN;
            this.fsPHNOSQ  = sPHNOSQ;
        }

        private void TYACKF003I_Load(object sender, System.EventArgs e)
        {
            // 부서코드
            this.CBH01_PHDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_PHNOSQ.SetReadOnly(true);

            this.CBH01_A1ACHL1.SetReadOnly(true);
            this.CBH01_PHCDFD.SetReadOnly(true);

            this.TXT01_PHBLGUBN.SetReadOnly(true);
            this.TXT01_PHBLSABN.SetReadOnly(true);
            this.TXT01_PHBLYYNO.SetReadOnly(true);
            this.TXT01_PHBLSQNO.SetReadOnly(true);
            this.TXT01_PHLCBLNO.SetReadOnly(true);
            this.TXT01_PHBLNO.SetReadOnly(true);

            if (string.IsNullOrEmpty(this.fsPHDPAC) && string.IsNullOrEmpty(this.fsPHSABUN) && string.IsNullOrEmpty(this.fsPHNOSQ))
            {
                this.CBH01_PHDPAC.SetReadOnly(false);
                this.CBH01_PHSABUN.SetReadOnly(false);

                UP_Page_SabunInit();

                SetStartingFocus(this.CBH01_PHDPAC.CodeText);
            }
            else
            {
                this.CBH01_PHDPAC.SetReadOnly(true);
                this.CBH01_PHSABUN.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_31331469",
                    this.fsPHDPAC,
                    this.fsPHSABUN,
                    this.fsPHNOSQ
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                SetStartingFocus(this.DTP01_PHIPDATE);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            // 등록
            if (string.IsNullOrEmpty(this.fsPHDPAC) && string.IsNullOrEmpty(this.fsPHSABUN) && string.IsNullOrEmpty(this.fsPHNOSQ))
            {
                // 등록
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2CR6O389",
                    this.CBH01_PHDPAC.GetValue().ToString(),                    // 1 부　　서
                    this.CBH01_PHSABUN.GetValue().ToString(),                   // 2 사　　번
                    this.DTP01_PHIPDATE.GetValue().ToString(),                  // 3 적용일자
                    Get_Numeric(this.TXT01_PHNOSQ.GetValue().ToString()),       // 4 순　번
                    this.CBH01_PHCDAC.GetValue().ToString(),                    // 5 계정과목
                    this.CBH01_PHCDFD.GetValue().ToString(),                    // 6 자금항목
                    this.CBO01_PHFDGUBN.GetValue().ToString(),                  // 7 자금구분
                    this.CBH01_PHVEND.GetValue().ToString(),                    // 8 거래처
                    this.CBH01_PHBANK.GetValue().ToString(),                    // 9 은행
                    Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_PHYUL.GetValue().ToString())).ToString("0000.00")), // 10 환율
                    this.CBH01_PHAMTGN.GetValue().ToString(),                   // 11 화폐
				    Get_Numeric(this.TXT01_PHAWAMT.GetValue().ToString()),      // 12 원화금액
                    Get_Numeric(this.TXT01_PHAIAMT.GetValue().ToString()),      // 13 외화금액
                    this.TXT01_PHRKAC.GetValue().ToString(),                    // 14 적요
				    this.CBO01_PHSTGBN.GetValue().ToString(),                   // 15 상태구분
				    this.TXT01_PHBLGUBN.GetValue().ToString(),                  // 16 구분
				    this.TXT01_PHBLSABN.GetValue().ToString(),                  // 17 사번
                    "",                                                         // 18 팀
				    this.TXT01_PHBLYYNO.GetValue().ToString(),                  // 19 계획년도
				    Get_Numeric(this.TXT01_PHBLSQNO.GetValue().ToString()),     // 20 계획번호
				    SetDefaultValue(this.TXT01_PHBLNO.GetValue().ToString()),   // 21 BL번호
				    SetDefaultValue(this.TXT01_PHLCBLNO.GetValue().ToString()), // 22 LC번호
                    "",                                                         // 23 품목
                    DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"), // 24 일자
                    TYUserInfo.EmpNo
                    );
            }
            else // 수정
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_313A2460",
                    this.DTP01_PHIPDATE.GetValue().ToString(),                  //  1 적용일자
                    DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"), // 2 일자
                    this.CBH01_PHCDAC.GetValue().ToString(),                    //  3 계정과목
                    this.CBH01_PHCDFD.GetValue().ToString(),                    //  4 자금항목
                    this.CBO01_PHFDGUBN.GetValue().ToString(),                  //  5 자금구분
                    this.CBH01_PHVEND.GetValue().ToString(),                    //  6 거래처
                    this.CBH01_PHBANK.GetValue().ToString(),                    //  7 은행
                    Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_PHYUL.GetValue().ToString())).ToString("0000.00")), // 8 환율
                    this.CBH01_PHAMTGN.GetValue().ToString(),                   //  9 화폐
                    Get_Numeric(this.TXT01_PHAWAMT.GetValue().ToString()),      // 10 원화금액
                    Get_Numeric(this.TXT01_PHAIAMT.GetValue().ToString()),      // 11 외화금액
                    this.TXT01_PHRKAC.GetValue().ToString(),                    // 12 적요
                    this.CBO01_PHSTGBN.GetValue().ToString(),                   // 13 상태구분
                    this.TXT01_PHBLGUBN.GetValue().ToString(),                  // 14 구분
                    "",                                                         // 15 팀
                    this.TXT01_PHBLSABN.GetValue().ToString(),                  // 16 사번
                    this.TXT01_PHBLYYNO.GetValue().ToString(),                  // 17 계획년도
                    Get_Numeric(this.TXT01_PHBLSQNO.GetValue().ToString()),     // 18 계획번호
                    SetDefaultValue(this.TXT01_PHBLNO.GetValue().ToString()),   // 19 BL번호
                    "",                                                         // 20 품목
                    SetDefaultValue(this.TXT01_PHLCBLNO.GetValue().ToString()), // 21 LC번호
                    TYUserInfo.EmpNo,
                    this.CBH01_PHDPAC.GetValue().ToString(),
                    this.CBH01_PHSABUN.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PHNOSQ.GetValue().ToString())
                    );
            }

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3139H459",
                this.DTP01_PHIPDATE.GetString(),
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["KBJKCD"].ToString() != "01")
                {
                    if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 1) != "A")
                    {
                        if (dt.Rows[0]["KBBUSEO"].ToString() != this.CBH01_PHDPAC.GetValue().ToString())
                        {
                            this.SetFocus(this.CBH01_PHDPAC.CodeText);

                            this.ShowMessage("TY_M_AC_3131B466");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }


            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3136T488",
                this.CBH01_PHCDAC.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["A1ACHL5"].ToString() != "")
                {
                    this.CBH01_A1ACHL1.SetValue(dt.Rows[0]["A1ACHL5"].ToString());
                }
                else if (dt.Rows[0]["A1ACHL4"].ToString() != "")
                {
                    this.CBH01_A1ACHL1.SetValue(dt.Rows[0]["A1ACHL4"].ToString());
                }
                else if (dt.Rows[0]["A1ACHL3"].ToString() != "")
                {
                    this.CBH01_A1ACHL1.SetValue(dt.Rows[0]["A1ACHL3"].ToString());
                }
                else if (dt.Rows[0]["A1ACHL2"].ToString() != "")
                {
                    this.CBH01_A1ACHL1.SetValue(dt.Rows[0]["A1ACHL2"].ToString());
                }
                else if (dt.Rows[0]["A1ACHL1"].ToString() != "")
                {
                    this.CBH01_A1ACHL1.SetValue(dt.Rows[0]["A1ACHL1"].ToString());
                }

                if (this.CBO01_PHFDGUBN.GetValue().ToString() == "1")
                {
                    this.CBH01_PHCDFD.SetValue(dt.Rows[0]["A1CRFD"].ToString());
                }
                else
                {
                    this.CBH01_PHCDFD.SetValue(dt.Rows[0]["A1DRFD"].ToString());
                }
            }

            if (this.TXT01_PHBLGUBN.GetValue().ToString() == "100" && this.TXT01_PHBLNO.GetValue().ToString() == "")
            {
                this.SetFocus(this.TXT01_PHBLNO);

                this.ShowMessage("TY_M_AC_31342476");
                e.Successed = false;
                return;
            }

            if (Convert.ToDouble(Get_Numeric(this.TXT01_PHAIAMT.GetValue().ToString())) > 0)
            {
                //환율
                if (Convert.ToDouble(Get_Numeric(this.TXT01_PHYUL.GetValue().ToString())) <= 0)
                {
                    this.SetFocus(this.TXT01_PHYUL);

                    this.ShowMessage("TY_M_AC_31343477");
                    e.Successed = false;
                    return;
                }

                if (this.CBH01_PHAMTGN.GetValue().ToString() == "")
                {
                    this.SetFocus(this.CBH01_PHAMTGN.CodeText);

                    this.ShowMessage("TY_M_AC_31344479");
                    e.Successed = false;
                    return;
                }

                //외화금액
                this.TXT01_PHAIAMT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_PHAIAMT.GetValue().ToString())).ToString("#0.00")));

                this.TXT01_PHAWAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString
                    (Convert.ToDouble(Get_Numeric(this.TXT01_PHAIAMT.GetValue().ToString())) *
                     Convert.ToDouble(Get_Numeric(this.TXT01_PHYUL.GetValue().ToString()))
                    )).ToString("#,##0")));
            }

            // 순번 가져오기
            if (string.IsNullOrEmpty(this.fsPHDPAC) && string.IsNullOrEmpty(this.fsPHSABUN) && string.IsNullOrEmpty(this.fsPHNOSQ))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_31332470",
                    this.CBH01_PHDPAC.GetValue().ToString(),
                    this.CBH01_PHSABUN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_PHNOSQ.SetValue(dt.Rows[0]["PHNOSQ"].ToString());
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 부서코드
        private void DTP01_PHIPDATE_ValueChanged(object sender, EventArgs e)
        {
            // 부서코드
            this.CBH01_PHDPAC.DummyValue = this.DTP01_PHIPDATE.GetValue().ToString();
        }
        #endregion

        #region Description : Page_Load 시 사번,귀속부서 세팅
        private void UP_Page_SabunInit()
        {
            //사번 조회
            this.CBH01_PHSABUN.SetValue(Employer.EmpNo.ToString().Trim());
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_PHSABUN.GetValue().ToString().Trim());  //INKIBNMF
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_PHIPDATE.GetString(), this.CBH01_PHSABUN.GetValue().ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count == 0)
            {
                //this.ShowCustomMessage("사원번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //this.SetFocus(this.CBH01_PHSABUN);
            }
            else
            {
                //this.CBH01_PHDPAC.DummyValue = this.DTP01_PHIPDATE.GetValue().ToString();
                this.CBH01_PHDPAC.SetValue(dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim());
            }
        }
        #endregion
    }
}