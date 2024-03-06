using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 승호생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.30 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_522E0249 : 승호생성 인사기본사항 조회
    ///  TY_P_HR_522ED250 : 승호파일 등록
    ///  TY_P_HR_522EE251 : 승호파일 삭제
    ///  TY_P_HR_522EF252 : 승호파일 기준년월 이상 존재 유무
    ///  TY_P_HR_522EG253 : 승호파일 발련번호 존재 유무
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2CDB0166 : 취소 하시겠습니까?
    ///  TY_M_AC_2CDB1167 : 취소 되었습니다!
    ///  TY_M_AC_2CDB1168 : 취소 작업에 실패했습니다!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GGUBUN : 구분
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYHRPR001B : TYBase
    {
        #region Description : 폼 로드
        public TYHRPR001B()
        {
            InitializeComponent();
        }

        private void TYHRPR001B_Load(object sender, System.EventArgs e)
        {
            // 처리 체크
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_YYYYMM.SetValue(System.DateTime.Now.ToString("yyyyMM"));
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_522EE251",
                                            this.DTP01_YYYYMM.GetString().Substring(0, 6), CBH01_KBSABUN.GetValue().ToString());
                    this.DbConnector.ExecuteTranQuery();

                    string sQuery = string.Empty;
                    string sKBCOMPANY = string.Empty;
                    string sKBSABUN = string.Empty;
                    string sKBHANGL = string.Empty;
                    string sKBHANJA = string.Empty;
                    string sKBJKCD = string.Empty;
                    string sKBHOBN = string.Empty;
                    string sKBBUSEO = string.Empty;
                    string sKBSOSOK = string.Empty;
                    string sKBIDATE = string.Empty;
                    string sKBJUMIN = string.Empty;
                    string sKBBALCD = string.Empty;
                    string sHLHAKKYO = string.Empty;
                    string sHLHAKGA = string.Empty;
                    string sJKCODE = string.Empty;
                    string sWKDATE = string.Empty;
                    string sWKDATE1 = string.Empty;
                    string sISUNGYY = string.Empty;
                    string sISUNGMM = string.Empty;
                    string sSSUNGYY = string.Empty;
                    string sSSUNGMM = string.Empty;
                    string sSNISUNG = string.Empty;
                    string sSNSSUNG = string.Empty;
                    string sSNSDATE = string.Empty;
                    string sSNGJGUBN = string.Empty;
                    string sSNCHHOBN = string.Empty;
                    string sSNCHJKCD = string.Empty;
                    string sKB_IPSAIL = string.Empty;

                    this.DbConnector.CommandClear();

                    // 인사기본사항 조회
                    this.DbConnector.Attach
                    (
                    "TY_P_HR_522E0249",
                    DTP01_YYYYMM.GetString().Substring(0, 6) + "01",
                    CBH01_KBSABUN.GetValue().ToString()
                    );

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sKBCOMPANY = dt.Rows[i]["KBCOMPANY"].ToString();  // 1 회사
                            sKBSABUN = dt.Rows[i]["KBSABUN"].ToString();    // 2 사번
                            sKBHANGL = dt.Rows[i]["KBHANGL"].ToString();    // 3 이름
                            sKBHANJA = dt.Rows[i]["KBHANJA"].ToString();    // 4 한자
                            sKBJKCD = dt.Rows[i]["KBJKCD"].ToString();     // 5 직급
                            sKBHOBN = dt.Rows[i]["KBHOBN"].ToString();     // 6 호봉
                            sKBBUSEO = dt.Rows[i]["KBBUSEO"].ToString();    // 7 부서
                            sKBSOSOK = dt.Rows[i]["KBSOSOK"].ToString();    // 8 소속
                            sKBIDATE = dt.Rows[i]["KBIDATE"].ToString();    // 9 입사일자
                            sKB_IPSAIL = dt.Rows[i]["KBIDATE"].ToString();    // 9 입사일자
                            sKBJUMIN = dt.Rows[i]["KBJUMIN"].ToString();    // 10 주민등록(생년월일)
                            sKBBALCD = dt.Rows[i]["KBBALCD"].ToString();    // 11 발령코드
                            sHLHAKKYO = dt.Rows[i]["HLHAKKYO"].ToString();   // 12 학교코드
                            sHLHAKGA = dt.Rows[i]["HLHAKGA"].ToString();    // 13 학과코드
                            sJKCODE = dt.Rows[i]["JKCODE"].ToString();     // 14 자격코드
                            sWKDATE = dt.Rows[i]["WKDATE"].ToString();     // 15 신규입사일
                            sWKDATE1 = dt.Rows[i]["WKDATE1"].ToString();    // 16 승진일자
                            sISUNGYY = dt.Rows[i]["ISUNGYY"].ToString();    // 17 근속년
                            sISUNGMM = dt.Rows[i]["ISUNGMM"].ToString();    // 18 근속월
                            sSSUNGYY = dt.Rows[i]["SSUNGYY"].ToString();    // 19 승진근속년
                            sSSUNGMM = dt.Rows[i]["SSUNGMM"].ToString();    // 20 승진근소월

                            if (sKBHOBN.Trim() != "")  //호봉자료가 있어야 처리
                            {
                                if (sWKDATE == "" && sWKDATE1 == "")
                                {
                                    sWKDATE = sKBIDATE;
                                }

                                if (sWKDATE1.ToString().Trim() == "" || sWKDATE1.ToString().Trim() == "0")
                                {
                                    sSNSDATE = sWKDATE;
                                }
                                else
                                {
                                    sSNSDATE = sWKDATE1;
                                }

                                sSNISUNG = Set_Fill2(sISUNGYY) + Set_Fill2(sISUNGMM); //근속년월
                                sSNSSUNG = Set_Fill2(sSSUNGYY) + Set_Fill2(sSSUNGMM); //승진근속년월

                                if (sKBJKCD.Trim().ToString() == "01")
                                {
                                    sSNGJGUBN = "0";  // 6개월미만자
                                    sSNCHHOBN = sKBHOBN.Trim();
                                }
                                else if (int.Parse(sSNISUNG.ToString().Trim()) >= 6 && int.Parse(sSNISUNG.ToString().Trim()) <= 99)
                                {
                                    sSNGJGUBN = "1";  // 6개월 이상 1년미만 근무자 --> 1호봉
                                    sSNCHHOBN = sKBHOBN.Trim().Substring(0, 2) + Set_Fill2(Convert.ToString((int.Parse(sKBHOBN.Trim().Substring(2, 2)) + int.Parse(sSNGJGUBN.ToString().Trim()))));
                                }
                                else if (int.Parse(sSNISUNG.ToString().Trim()) >= 100)
                                {
                                    sSNGJGUBN = "2";  // 1년이상근무자 --> 2호봉
                                    sSNCHHOBN = sKBHOBN.Trim().Substring(0, 2) + Set_Fill2(Convert.ToString((int.Parse(sKBHOBN.Trim().Substring(2, 2)) + int.Parse(sSNGJGUBN.ToString().Trim()))));
                                }
                                else
                                {
                                    sSNGJGUBN = "0";  // 6개월미만자
                                    sSNCHHOBN = sKBHOBN.Trim();
                                }

                                this.DbConnector.CommandClear();

                                this.DbConnector.Attach("TY_P_HR_522ED250",
                                                        sKBCOMPANY.ToString(),                      // 1 VARCHAR2(1)           공　　장
                                                        DTP01_YYYYMM.GetString().Substring(0, 6),   // 2 VARCHAR2(6)           년    월
                                                        sKBSABUN.ToString().ToUpper(),              // 3 VARCHAR2(6)           사　　번
                                                        sKBHANGL.ToString(),                        // 4 VARCHAR2(10),         한　　글
                                                        sKBHANJA.ToString(),                        // 5 VARCHAR2(10),         한　　자
                                                        sKBJKCD.ToString().ToUpper(),               // 6 VARCHAR2(2),          직　　급
                                                        sKBHOBN.ToString().ToUpper(),               // 7 VARCHAR2(4),          호　　봉
                                                        sKBBUSEO.ToString().ToUpper(),              // 8 VARCHAR2(6),          부　　서
                                                        sKBSOSOK.ToString().ToUpper(),              // 9 VARCHAR2(6),          소　　속
                                                        sKBIDATE.ToString(),                        // 10 NUMERIC(8,0),        입사일자
                                                        sSNISUNG.ToString(),                        // 11 NUMERIC(6,0),        근속기간
                                                        sSNSDATE.ToString(),                        // 12 NUMERIC(8,0),        승진일자
                                                        sSNSSUNG.ToString(),                        // 13 NUMERIC(6,0),        승진근속
                                                        sKBJUMIN.ToString(),                        // 14 VARCHAR2(8),         생년월일
                                                        sHLHAKKYO.ToString().ToUpper(),             // 15 VARCHAR2(6),         학교코드
                                                        sHLHAKGA.ToString().ToUpper(),              // 16 VARCHAR2(4),         학과코드
                                                        sJKCODE.ToString().ToUpper(),               // 17 VARCHAR2(4),         자격코드
                                                        "",                                         // 18 VARCHAR2(1),         승진구분
                                                        "",                                         // 19 VARCHAR2(1),         승진확정
                                                        sSNGJGUBN.ToString().ToUpper(),             // 20 NUMERIC(1,0),        승호구분
                                                        "0",                                        // 21 NUMERIC(1,0),        특별승호
                                                        sSNCHJKCD.ToString().ToUpper(),             // 22 VARCHAR2(2),         승진직급코드
                                                        sSNCHHOBN.ToString().ToUpper(),             // 23 VARCHAR2(4),         변환호봉코드
                                                        "",                                         // 24 VARCHAR2(2),         학력코드
                                                        "",                                         // 25 VARCHAR2(4),         발령번호-년도
                                                        "0",                                        // 26 NUMERIC(3,0),        발령번호-순번
                                                        "",
                                                        "0",
                                                        TYUserInfo.EmpNo
                                                        );

                                this.DbConnector.ExecuteNonQuery();
                            }

                            
                        }
                        this.ShowMessage("TY_M_GB_26E30875");
                    }
                }
                else if (this.CBO01_GGUBUN.GetValue().ToString() == "N")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_522EE251", 
                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),"");
                    this.DbConnector.ExecuteTranQuery();

                    this.ShowMessage("TY_M_AC_2CDB1167");
                }
            }
            catch
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "Y")
                {
                    this.ShowMessage("TY_M_GB_26E31876");
                }
                else if (this.CBO01_GGUBUN.GetValue().ToString() == "N")
                {
                    this.ShowMessage("TY_M_AC_2CDB1168");
                }
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            //승호파일 기준년월 이상 존재유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                    "TY_P_HR_522EF252",
                                    this.DTP01_YYYYMM.GetString().Substring(0,6)
                                    );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowCustomMessage("기준년월 이후 자료가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //승호파일 발령번호 존재 유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                    "TY_P_HR_522EG253",
                                    this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                    );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowCustomMessage("발령사항이 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            // 인사기본사항 조회
            this.DbConnector.Attach
            (
            "TY_P_HR_522E0249",
            DTP01_YYYYMM.GetString().Substring(0, 6) + "01",
            CBH01_KBSABUN.GetValue().ToString()
            );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["KBHOBN"].ToString().Trim() == "")
                    {
                        this.ShowCustomMessage(dt.Rows[i]["KBHANGL"].ToString() +" 호봉 자료를 확인하세요!" , "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "Y")
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "N")
            {
                if (!this.ShowMessage("TY_M_AC_2CDB0166"))
                {
                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {   
            this.Close();
        }
        #endregion
    }
}
