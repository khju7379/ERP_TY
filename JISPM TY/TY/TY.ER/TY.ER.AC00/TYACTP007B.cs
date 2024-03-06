using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 자료 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.09.23 15:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_49ID7923 : 원천세 근로소득 세부내역 생성 조회
    ///  TY_P_AC_49IF2933 : 원천세 연말정산 세부내역 생성 조회
    ///  TY_P_AC_49NH1051 : 원천세 퇴직소득 세부내역 등록
    ///  TY_P_AC_49NH1052 : 원천세 연말정산 세부내역 등록
    ///  TY_P_AC_49NH3053 : 원천세 일용항운노조 세부내역 등록
    ///  TY_P_AC_49NH7049 : 원천세 근로소득 세부내역 등록
    ///  TY_P_AC_49NH9050 : 원천세 집계표 등록
    ///  TY_P_AC_49NIH054 : 원천세 근로소득 세부내역 삭제
    ///  TY_P_AC_49O9H055 : 원천세 연말정산 세부내역 삭제
    ///  TY_P_AC_49O9J056 : 원천세 퇴직소득 세부내역 삭제
    ///  TY_P_AC_49O9K057 : 원천세 일용항운노조 세부내역 삭제
    ///  TY_P_AC_49O9L058 : 원천세 집계표 삭제
    ///  TY_P_AC_49PEW073 : 원천세 집계표 생성(SP)
    ///  TY_P_AC_4A2EP124 : 원천세 환급세액 삭제
    ///  TY_P_AC_4AEA3174 : 원천세 일용항운노조 세부내역 등록(자동)
    ///  TY_P_AC_4AEA5175 : 원천세 일용항운노조 쌍용호 세부내역 조회(자동)
    ///  TY_P_AC_4AEAA176 : 원천세 일용항운노조 쌍용호소급 세부내역 조회(자동)
    ///  TY_P_AC_4AEAA177 : 원천세 일용항운노조 명부 조회(자동)
    ///  TY_P_AC_4AEHK178 : 원천세 항운노조 상용호 처리(SP)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_3CR9M876 : 부가세 옵션 자료가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  VNGUBUN : 구분
    ///  WABRANCH : 지점구분
    ///  S1CHK12 : 전체
    ///  W1CHK1 : 근로소득
    ///  W1CHK2 : 퇴직소득
    ///  W1CHK3 : 사업소득
    ///  W1CHK4 : 기타소득
    ///  W1CHK5 : 이자소득
    ///  W1CHK6 : 배당소득
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP007B : TYBase
    {
        public TYACTP007B()
        {
            InitializeComponent();
        }
     
        #region Description : Page_Load
        private void TYACTP007B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_WREYYMM);

            this.TXT01_W1CHK1.ReadOnly = true;
            this.TXT01_W1CHK2.ReadOnly = true;
            this.TXT01_W1CHK3.ReadOnly = true;
            this.TXT01_W1CHK4.ReadOnly = true;
            this.TXT01_W1CHK5.ReadOnly = true;
            this.TXT01_W1CHK6.ReadOnly = true;
            this.TXT01_W1CHK8.ReadOnly = true;

            this.RB_ATTAXGUBN1.Checked = true;
        }
        #endregion

        #region Description : 처리버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int iCCNT = 0;
            int iCHK1 = 0;

            string sOUTMSG = string.Empty;
            string sOUTMSG1 = string.Empty;
            string sVNGUBUN = string.Empty;

            string sBRANCH = string.Empty;

            this.TXT01_W1CHK1.SetValue(""); // s근로소득
            this.TXT01_W1CHK2.SetValue(""); // s퇴직소득
            this.TXT01_W1CHK3.SetValue(""); // s사업소득
            this.TXT01_W1CHK4.SetValue(""); // s기타소득
            this.TXT01_W1CHK5.SetValue(""); // s이자소득
            this.TXT01_W1CHK6.SetValue(""); // s배당소득
            this.TXT01_W1CHK8.SetValue(""); // s배당소득

            this.TXT01_W1CHK1.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK2.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK3.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK4.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK5.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK6.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK8.TextAlign = HorizontalAlignment.Center;
                        
            string s처리구분 = string.Empty;

            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                s처리구분 = "Y"; //생성
            }
            else
            {
                s처리구분 = "N"; //취소
            }

            sBRANCH = "1";


            if (s처리구분 == "Y")
            {
                #region Description : 생성
                // 1.근로소득 , 2.퇴직소득 , 3.사업소득 , 4.기타소득 , 5.이자소득 , 6.배당소득 , 7 = 주민세 추가소득
                // 집계표 , 원천세 환급세액 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_49O9L058", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WSUMMARYTF 
                this.DbConnector.ExecuteScalar();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_BBQBN796", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WSUMMARYSF 
                this.DbConnector.ExecuteScalar();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4A2EP124", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WREFUNDEDF
                this.DbConnector.ExecuteScalar();

                // 1.근로소득 (간이세액 - 급.상여, 중도퇴사, 일용근로 항운노조, 연말정산)
                #region Description : 1-1. 근로소득 세부 생성(간이세액 - 급.상여)

                // ------- 근로소득 세부내역 (급.상여)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_49NIH054", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                this.DbConnector.CommandClear(); 
                this.DbConnector.Attach("TY_P_AC_67PI1909", Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                DataTable dt_S1 = this.DbConnector.ExecuteDataTable();

                if (dt_S1.Rows.Count != 0)
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dt_S1.Rows.Count; i++)
                    {
                        // WAEARNEDF 생성
                        this.DbConnector.Attach("TY_P_AC_49NH7049", sBRANCH.ToString(),
                                                                    dt_S1.Rows[i]["PYDATE"].ToString(),
                                                                    dt_S1.Rows[i]["PYJIDATE"].ToString(),
                                                                    dt_S1.Rows[i]["KBBSTEAM"].ToString(),
                                                                    dt_S1.Rows[i]["INCOMGB"].ToString(),
                                                                    dt_S1.Rows[i]["PYGUBUN"].ToString(),
                                                                    dt_S1.Rows[i]["KBGUNMU"].ToString(),
                                                                    Get_Numeric(dt_S1.Rows[i]["CNT"].ToString()),
                                                                    Get_Numeric(dt_S1.Rows[i]["PAYAMT"].ToString()),
                                                                    Get_Numeric(dt_S1.Rows[i]["INCOMAMT"].ToString()),
                                                                    Get_Numeric(dt_S1.Rows[i]["LOCALAMT"].ToString())); // 저장
                        iCHK1 = iCHK1 + 1;
                        iCCNT = iCCNT + 1;

                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

                // 특별수당(성과급)
                if (Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(4, 2) == "01")
                {
                    if (sBRANCH.ToString() == "1")
                    {
                        // ------- 근로소득 세부내역 (종업원분)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_526EB296", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                        this.DbConnector.ExecuteScalar();

                        string sWKYYMM = string.Empty;
                        sWKYYMM = Convert.ToString(int.Parse(Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(0, 4)) - 1) + "12";

                        this.DbConnector.CommandClear(); 
                        //this.DbConnector.Attach("TY_P_AC_525GJ289", sWKYYMM, this.CBO01_WABRANCH.GetValue().ToString(), "T");
                        this.DbConnector.Attach("TY_P_AC_67PHY908", sWKYYMM, "T1");
                        DataTable dt_S2 = this.DbConnector.ExecuteDataTable();

                        if (dt_S2.Rows.Count != 0)
                        {
                            this.DbConnector.CommandClear();
                            for (int i = 0; i < dt_S2.Rows.Count; i++)
                            {
                                this.DbConnector.Attach("TY_P_AC_526B1293", sBRANCH.ToString(),
                                                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString()),
                                                                            dt_S2.Rows[i]["PYJIDATE"].ToString(),
                                                                            dt_S2.Rows[i]["KBBSTEAM"].ToString(),
                                                                            dt_S2.Rows[i]["INCOMGB"].ToString(),
                                                                            dt_S2.Rows[i]["PYGUBUN"].ToString(),
                                                                            dt_S2.Rows[i]["KBGUNMU"].ToString(),
                                                                            Get_Numeric(dt_S2.Rows[i]["CNT"].ToString()),
                                                                            Get_Numeric(dt_S2.Rows[i]["PAYAMT"].ToString()),
                                                                            Get_Numeric(dt_S2.Rows[i]["INCOMAMT"].ToString()),
                                                                            Get_Numeric(dt_S2.Rows[i]["LOCALAMT"].ToString())); // 저장
                                iCHK1 = iCHK1 + 1;
                                iCCNT = iCCNT + 1;

                            }
                            this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                }
                #endregion

                #region Description : 1-2. 근로소득 세부 생성(중도퇴사)

                // ------- 원천세 중도퇴사 세부내역 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_BBUGW837", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WMDRETIREMF
                this.DbConnector.ExecuteScalar();

                // 1-2-1. 중도퇴사 세부내역 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_BBUGH836", sBRANCH.ToString(),
                                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString()),
                                                            TYUserInfo.EmpNo.ToString(),
                                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString())
                                                            ); // 저장
                this.DbConnector.ExecuteTranQueryList();

                // 1-2-1. 중도퇴사 근로소득 등록
                this.DbConnector.CommandClear();
                // 22.03.07 수정전 소스
                //this.DbConnector.Attach("TY_P_AC_BBUGG835", sBRANCH.ToString(),
                //                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString()),
                //                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString())
                //                                            ); // 저장

                // 22.03.07 수정 후 소스
                this.DbConnector.Attach("TY_P_AC_C37BC127", sBRANCH.ToString(),
                                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString()),
                                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString())
                                                            ); // 저장
                this.DbConnector.ExecuteTranQueryList();

                #endregion

                #region Description : 1-3. 근로소득 세부 생성(연말정산)
                // ------- 연말 정산
                this.DbConnector.CommandClear(); // WYEARTAXMF 
                this.DbConnector.Attach("TY_P_AC_49O9H055", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                if (Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(4, 2) == "02")
                {
                    string sYEAR = string.Empty;
                    sYEAR = Convert.ToString(int.Parse(Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(0, 4)) - 1);

                    // DB2 - 인사 연말정산 NTS_WONCHUNF
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_BBAAZ732", sBRANCH.ToString(),
                                                                Get_Date(this.DTP01_WREYYMM.GetValue().ToString()),
                                                                Get_Date(this.DTP01_WREYYMM.GetValue().ToString())
                                                                ); // 저장
                    this.DbConnector.ExecuteTranQueryList();
                    
                }
                #endregion

                #region Description : 1-4. 근로소득 세부 생성(일용근로 항운노조)

                string sWREYYMM = string.Empty;

                if (Convert.ToInt32(Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(0, 4)) >= 2016)
                {
                    sWREYYMM = Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(0, 6);
                }
                else
                {
                    if (Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(4, 2) == "01")
                    {
                        sWREYYMM = Convert.ToString(int.Parse(Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(0, 4)) - 1) + "12";
                    }
                    else
                    {
                        sWREYYMM = Convert.ToString(int.Parse(Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Substring(0, 6)) - 1);
                    }
                }

                this.DbConnector.CommandClear(); // WEVARHANGF (삭제)
                this.DbConnector.Attach("TY_P_AC_49O9K057", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                if (sBRANCH.ToString() == "1")
                {
                    this.DbConnector.CommandClear(); // WEVARHANGF (생성)
                    
                    if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
                    {
                        //항운노조 소급분 미포함
                        this.DbConnector.Attach("TY_P_AC_5B2GR063",
                                                 sBRANCH.ToString(),
                                                 Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Replace("-", "").Substring(0, 6),
                                                 Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Replace("-", "").Substring(0, 6),
                                                 TYUserInfo.SecureKey,
                                                 TYUserInfo.SecureKey,"Y",
                                                 TYUserInfo.SecureKey, "Y",
                                                 sWREYYMM.Substring(0, 4),
                                                 sWREYYMM, 
                                                 sWREYYMM
                                                 );
                    }
                    else
                    {
                        //항운노조 소급분 포함
                        this.DbConnector.Attach("TY_P_AC_4AEA3174",
                                                 sBRANCH.ToString(),
                                                 Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Replace("-", "").Substring(0, 6),
                                                 Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Replace("-", "").Substring(0, 6),
                                                 TYUserInfo.SecureKey,
                                                 TYUserInfo.SecureKey,
                                                 "Y",
                                                 TYUserInfo.SecureKey,
                                                 "Y",
                                                 sWREYYMM.Substring(0, 4),
                                                 sWREYYMM, sWREYYMM,
                                                 TYUserInfo.SecureKey, "Y",
                                                 TYUserInfo.SecureKey, "Y",
                                                 sWREYYMM.Substring(0, 4),
                                                 sWREYYMM
                                                 );
                    }
                    this.DbConnector.ExecuteScalar();

                    // 쌍용호 생성
                    DataTable table = DataSetConvert(sWREYYMM);

                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            this.DbConnector.CommandClear(); // WEVARHANGF (생성)
                            this.DbConnector.Attach("TY_P_AC_4AEHK178",
                                                     sBRANCH.ToString(),
                                                     Get_Date(this.DTP01_WREYYMM.GetValue().ToString()).Replace("-", "").Substring(0, 6),
                                                     table.Rows[i]["HNYYMM"].ToString().Trim(),  //sWREYYMM
                                                     table.Rows[i]["JUMIN"].ToString().Trim(),
                                                     Get_Numeric(table.Rows[i]["HNIMGUM"].ToString().Trim()),
                                                     Get_Numeric(table.Rows[i]["HNKGSAE"].ToString().Trim()),
                                                     Get_Numeric(table.Rows[i]["HNJMSAE"].ToString().Trim()),
                                                     TYUserInfo.SecureKey,
                                                     sOUTMSG1.ToString()
                                                     );

                            iCHK1 = iCHK1 + 1;
                            iCCNT = iCCNT + 1;

                            this.DbConnector.ExecuteScalar();
                        }
                    }
                }

                #endregion

                #region Description : 2.퇴직소득 세부 생성
                // 퇴직소득
                this.DbConnector.CommandClear(); // WRETIRINCOMEF 삭제 
                this.DbConnector.Attach("TY_P_AC_49O9J056", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();
                // 
                this.DbConnector.CommandClear(); // 생성
                this.DbConnector.Attach("TY_P_AC_49NH1051", sBRANCH.ToString(),
                                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString()),
                                                            TYUserInfo.EmpNo.ToString(),
                                                            Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                #endregion

                #region Description : 3.사업소득 생성 , 4.기타소득 생성 , 5.이자소득 생성 , 6.배당소득 생성 , 7 = 주민세 추가소득 생성 --> 최종집계표 생성

                // 집계표 - 생성 (SP)
                this.DbConnector.CommandClear();
                
                // 소득세와 지방세 로직 분리 전 SP
                //this.DbConnector.Attach("TY_P_AC_49PEW073", "C", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()), sOUTMSG.ToString());

                // 소득세와 지방세 로직 분리 후 SP
                this.DbConnector.Attach("TY_P_AC_BBBBS737", "C", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()), sOUTMSG.ToString());

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                {
                    // 소득 확인
                    this.DbConnector.CommandClear(); // 인사 연말정산 WONCHUNF 
                    this.DbConnector.Attach("TY_P_AC_BBABQ735", Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                    DataTable dt_S5 = this.DbConnector.ExecuteDataTable();

                    if (dt_S5.Rows.Count > 0)
                    {
                        this.TXT01_W1CHK1.SetValue("Ⅹ"); // s근로
                        this.TXT01_W1CHK2.SetValue("Ⅹ"); // s퇴직
                        this.TXT01_W1CHK3.SetValue("Ⅹ"); // s사업
                        this.TXT01_W1CHK4.SetValue("Ⅹ"); // s기타
                        this.TXT01_W1CHK5.SetValue("Ⅹ"); // s이자
                        this.TXT01_W1CHK6.SetValue("Ⅹ"); // s배당
                        this.TXT01_W1CHK8.SetValue("Ⅹ"); // s주민 추가소득

                        for (int i = 0; i < dt_S5.Rows.Count; i++)
                        {
                            if (int.Parse(Get_Numeric(dt_S5.Rows[i]["W1CHK1"].ToString())) > 0)
                            {
                                this.TXT01_W1CHK1.SetValue("○"); // s근로
                            }

                            if (int.Parse(Get_Numeric(dt_S5.Rows[i]["W1CHK2"].ToString())) > 0)
                            {
                                this.TXT01_W1CHK2.SetValue("○"); // s퇴직
                            }

                            if (int.Parse(Get_Numeric(dt_S5.Rows[i]["W1CHK3"].ToString())) > 0)
                            {
                                this.TXT01_W1CHK3.SetValue("○"); // s사업
                            }

                            if (int.Parse(Get_Numeric(dt_S5.Rows[i]["W1CHK4"].ToString())) > 0)
                            {
                                this.TXT01_W1CHK4.SetValue("○"); // s기타
                            }

                            if (int.Parse(Get_Numeric(dt_S5.Rows[i]["W1CHK5"].ToString())) > 0)
                            {
                                this.TXT01_W1CHK5.SetValue("○"); // s이자
                            }

                            if (int.Parse(Get_Numeric(dt_S5.Rows[i]["W1CHK6"].ToString())) > 0)
                            {
                                this.TXT01_W1CHK6.SetValue("○"); // s배당
                            }

                            if (int.Parse(Get_Numeric(dt_S5.Rows[i]["W1CHK8"].ToString())) > 0)
                            {
                                this.TXT01_W1CHK8.SetValue("○"); // s주민 추가소득
                            }
                        }
                    }
                    else
                    {
                        this.TXT01_W1CHK1.SetValue("Ⅹ"); // s근로
                        this.TXT01_W1CHK2.SetValue("Ⅹ"); // s퇴직
                        this.TXT01_W1CHK3.SetValue("Ⅹ"); // s사업
                        this.TXT01_W1CHK4.SetValue("Ⅹ"); // s기타
                        this.TXT01_W1CHK5.SetValue("Ⅹ"); // s이자
                        this.TXT01_W1CHK6.SetValue("Ⅹ"); // s배당
                        this.TXT01_W1CHK8.SetValue("Ⅹ"); // s주민 추가소득
                    }
                }
                else
                {
                    this.TXT01_W1CHK1.SetValue("Ⅹ"); // s근로
                    this.TXT01_W1CHK2.SetValue("Ⅹ"); // s퇴직
                    this.TXT01_W1CHK3.SetValue("Ⅹ"); // s사업
                    this.TXT01_W1CHK4.SetValue("Ⅹ"); // s기타
                    this.TXT01_W1CHK5.SetValue("Ⅹ"); // s이자
                    this.TXT01_W1CHK6.SetValue("Ⅹ"); // s배당
                    this.TXT01_W1CHK8.SetValue("Ⅹ"); // s주민 추가소득
                }

                #endregion

                if (iCCNT > 0)
                {
                    this.ShowCustomMessage("처리완료 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    this.ShowCustomMessage("생성자료 없음.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                } 
                #endregion
            }
            else
            {
                #region Description : 삭제
                // 집계표 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_49O9L058", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WSUMMARYTF 
                this.DbConnector.ExecuteScalar();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_BBQBN796", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WSUMMARYSF 
                this.DbConnector.ExecuteScalar();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4A2EP124", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WREFUNDEDF
                this.DbConnector.ExecuteScalar();

                #region Description : 1.근로소득 삭제
                // ------- 근로소득 세부내역 (급.상여)
                this.DbConnector.CommandClear(); // WAEARNEDF 
                this.DbConnector.Attach("TY_P_AC_49NIH054", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                // ------- 연말 정산
                this.DbConnector.CommandClear(); // WYEARTAXMF 
                this.DbConnector.Attach("TY_P_AC_49O9H055", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                // ------- 원천세 중도퇴사 세부내역 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_BBUGW837", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString())); // WMDRETIREMF
                this.DbConnector.ExecuteScalar();


                // ------- 퇴직소득
                this.DbConnector.CommandClear(); // WRETIRINCOMEF 삭제 
                this.DbConnector.Attach("TY_P_AC_49O9J056", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                // ------- 일용항운노조
                this.DbConnector.CommandClear(); // WEVARHANGF 
                this.DbConnector.Attach("TY_P_AC_49O9K057", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                // ------- 원천세 종업원분 집계 
                this.DbConnector.CommandClear(); // WEMPLOYEF 
                this.DbConnector.Attach("TY_P_AC_BBBBJ736", sBRANCH.ToString(), Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
                this.DbConnector.ExecuteScalar();

                #endregion


                this.TXT01_W1CHK1.SetValue(""); // s근로
                this.TXT01_W1CHK2.SetValue(""); // s퇴직
                this.TXT01_W1CHK3.SetValue(""); // s사업
                this.TXT01_W1CHK4.SetValue(""); // s기타
                this.TXT01_W1CHK5.SetValue(""); // s이자
                this.TXT01_W1CHK6.SetValue(""); // s배당
                this.TXT01_W1CHK8.SetValue(""); // s주민 추가소득

                this.ShowCustomMessage("삭제 처리완료 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                #endregion
            }

        }
        #endregion

        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.TXT01_W1CHK1.SetValue(""); // s근로
            this.TXT01_W1CHK2.SetValue(""); // s퇴직
            this.TXT01_W1CHK3.SetValue(""); // s사업
            this.TXT01_W1CHK4.SetValue(""); // s기타
            this.TXT01_W1CHK5.SetValue(""); // s이자
            this.TXT01_W1CHK6.SetValue(""); // s배당
            this.TXT01_W1CHK8.SetValue(""); // s주민 추가소득
        }
        #endregion

        #region Description : 라디오버튼 이벤트
        private void RB_ATTAXGUBN1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                this.RB_ATTAXGUBN2.Checked = false;
            }
        }

        private void RB_ATTAXGUBN2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_ATTAXGUBN2.Checked == true)
            {
                this.RB_ATTAXGUBN1.Checked = false;
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        #region Description : 쌍용호 처리
        private DataTable DataSetConvert(string sWREYYMM)
        {

            int iCOUNT = 0;
            int i = 0;
            int j = 0;

            string sSql = string.Empty;
            string sHYYYMM = string.Empty;
            string sHYVSNAME = string.Empty;
            double dAMOUNT = 0;
            double dILDUNG = 0;
            double dNOJO = 0;
            double dNOJOILDUNG = 0;

            DataTable Table = new DataTable();

            DataRow row1;
            DataRow row2;

            Table.Columns.Add("HNYYMM", typeof(System.String));
            Table.Columns.Add("HUNAME", typeof(System.String));
            Table.Columns.Add("JUMIN", typeof(System.String));
            Table.Columns.Add("HUJUSO", typeof(System.String));
            Table.Columns.Add("HMTEL01", typeof(System.String));
            Table.Columns.Add("HMTEL02", typeof(System.String));
            Table.Columns.Add("HNIMGUM", typeof(System.String));
            Table.Columns.Add("HNKGSAE", typeof(System.String));
            Table.Columns.Add("HNJMSAE", typeof(System.String));

            this.DbConnector.CommandClear(); // HUIYNVSF
            this.DbConnector.Attach("TY_P_AC_4AEA5175", sWREYYMM, sWREYYMM); 
            DataTable dz = this.DbConnector.ExecuteDataTable();

            if (dz.Rows.Count > 0)
            {
                for (i = 0; i < dz.Rows.Count; i++)
                {
                    iCOUNT = 0;
                    sHYYYMM = dz.Rows[i]["HYYYMM"].ToString().Trim(); 
                    sHYVSNAME = dz.Rows[i]["HYVSNAME"].ToString().Trim();
                    dAMOUNT = double.Parse(dz.Rows[i]["AMOUNT"].ToString().Trim());
                    dILDUNG = double.Parse(dz.Rows[i]["ILDUNG"].ToString().Trim());
                    dNOJO = double.Parse(dz.Rows[i]["NOJO"].ToString().Trim()); 
                    dNOJOILDUNG = double.Parse(dz.Rows[i]["NOJOILDUNG"].ToString().Trim()); 

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_4AEAA177", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sHYYYMM, dz.Rows[i]["HYWKINWON"].ToString().Trim());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (j = 0; j < dt.Rows.Count; j++)
                        {
                            row1 = Table.NewRow();

                            row1["HNYYMM"] = sHYYYMM.ToString();
                            row1["HUNAME"] = dt.Rows[j]["HUNAME"].ToString();
                            row1["JUMIN"] = dt.Rows[j]["HUBIRTH"].ToString();
                            row1["HUJUSO"] = dt.Rows[j]["HUJUSO"].ToString();
                            row1["HMTEL01"] = dt.Rows[j]["HMTEL01"].ToString();
                            row1["HMTEL02"] = dt.Rows[j]["HMTEL02"].ToString();
                            row1["HNKGSAE"] = "0";
                            row1["HNJMSAE"] = "0";

                            iCOUNT = iCOUNT + 1;
                            if (iCOUNT == 1)
                            {
                                row1["HNIMGUM"] = dILDUNG;
                            }
                            else
                            {
                                row1["HNIMGUM"] = dAMOUNT;
                            }

                            Table.Rows.Add(row1);
                        }
                    }
                }
            }


            if (this.CBO01_INQOPTION.GetValue().ToString() != "1")
            {
                // 소급분 HUIYNVSF
                this.DbConnector.CommandClear();
                // 원본소스 21.12.08
                //this.DbConnector.Attach("TY_P_AC_4AEAA176", sWREYYMM);

                // 수정소스 21.12.08
                this.DbConnector.Attach("TY_P_AC_BC89T895", sWREYYMM);
                DataTable dv = this.DbConnector.ExecuteDataTable();

                if (dv.Rows.Count > 0)
                {
                    for (i = 0; i < dv.Rows.Count; i++)
                    {
                        iCOUNT = 0;
                        sHYYYMM = dv.Rows[i]["HYYYMM"].ToString().Trim();
                        sHYVSNAME = dv.Rows[i]["HYVSNAME"].ToString().Trim();
                        dAMOUNT = double.Parse(dv.Rows[i]["AMOUNT"].ToString().Trim());
                        dILDUNG = double.Parse(dv.Rows[i]["ILDUNG"].ToString().Trim());
                        dNOJO = double.Parse(dv.Rows[i]["NOJO"].ToString().Trim());
                        dNOJOILDUNG = double.Parse(dv.Rows[i]["NOJOILDUNG"].ToString().Trim());

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_4AEAA177", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y",  sHYYYMM, dv.Rows[i]["HYWKINWON"].ToString().Trim());
                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            for (j = 0; j < dt.Rows.Count; j++)
                            {
                                row2 = Table.NewRow();

                                row2["HNYYMM"] = sHYYYMM.ToString();
                                row2["HUNAME"] = dt.Rows[j]["HUNAME"].ToString();
                                row2["JUMIN"] = dt.Rows[j]["HUBIRTH"].ToString();
                                row2["HUJUSO"] = dt.Rows[j]["HUJUSO"].ToString();
                                row2["HMTEL01"] = dt.Rows[j]["HMTEL01"].ToString();
                                row2["HMTEL02"] = dt.Rows[j]["HMTEL02"].ToString();
                                row2["HNKGSAE"] = "0";
                                row2["HNJMSAE"] = "0";

                                iCOUNT = iCOUNT + 1;
                                if (iCOUNT == 1)
                                {
                                    row2["HNIMGUM"] = dILDUNG;
                                }
                                else
                                {
                                    row2["HNIMGUM"] = dAMOUNT;
                                }

                                Table.Rows.Add(row2);
                            }
                        }
                    }
                }
            }          
            
            return Table;
        }
        #endregion

    }
}
