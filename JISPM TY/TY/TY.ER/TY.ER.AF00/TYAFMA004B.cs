using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 손익자료 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.09.27 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_3992G619 : EIS 계열사별 주주현황 삭제
    ///  TY_P_AC_3992H620 : EIS 계열사별 임원현황 삭제
    ///  TY_P_AC_3992K621 : EIS 계열사별 임원현황 겸직 및 경력 삭제
    ///  TY_P_AC_3992L622 : EIS 계열사별 임원현황 복사
    ///  TY_P_AC_3992M623 : EIS 계열사별 임원현황 겸직 및 경력 복사
    ///  TY_P_AC_3992M624 : EIS 계열사별 주주현황 복사
    ///  TY_P_AC_3994S625 : EIS 계열사 손익계획 생성(SP)
    ///  TY_P_AC_39B5E676 : EIS 계열사 주요매출처 생성(TG)
    ///  TY_P_AC_39B5I680 : EIS 계열사 주요매출처 생성(TG)-취소
    ///  TY_P_AC_39B5K681 : EIS 계열사 주요매출처 생성(TG - 태영)-취소
    ///  TY_P_AC_39B5Q683 : EIS 계열사 주요매출처 조회-생성(TG)
    ///  TY_P_AC_39B5T684 : EIS 계열사 주요매출처 삭제(TG - 태영)-생성
    ///  TY_P_AC_39B5V685 : EIS 계열사 주요매출처 생성(TG - 태영)-생성
    ///  TY_P_AC_39C42698 : EIS 계열사 주요매출처 생성 - 삭제(TH)
    ///  TY_P_AC_39C44699 : EIS 계열사 주요매출처 생성 - 생성(TH)
    ///  TY_P_AC_39G2M768 : EIS 계열사 주요매출처 생성 - 삭제(TS)
    ///  TY_P_AC_39G2P769 : EIS 계열사 주요매출처 생성 - 생성(TS)
    ///  TY_P_AC_39H43808 : EIS 계열사 기타매출처 생성(TG - 태영)-삭제
    ///  TY_P_AC_39R4M897 : EIS 계열사 계정원장 생성(SP)
    ///  TY_P_AC_3AF24052 : EIS 계열사 계정원장 생성(SP -TG)
    ///  TY_P_AC_3AA4F034 : EIS 계열사 차입금 생성(SP)
    ///  TY_P_AC_3AB12043 : EIS 계열사 차입금 세부생성(SP)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  ESBMCUST : 계열사구분
    ///  GOKCR : 생성구분
    ///  EPBOGY : 년 계획생성
    ///  EPINSA : 인사정보
    ///  EPLCGB : 손익생성
    ///  EPSALE : 주요매출처
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYAFMA004B : TYBase
    {
        private string fsCompanyCode;

        public TYAFMA004B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYAFMA004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESBMCUST.SetValue(fsCompanyCode);
                this.CBH01_ESBMCUST.SetReadOnly(true);
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.DTP01_GSTYYMM);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESBMCUST.CodeText);
            }

            if (fsCompanyCode == "TL")
            {
                this.CKB01_EPFACI.Visible = false;
                this.CKB01_EPSALE.Visible = false;
                this.CKB01_EPBOGY.Visible = false;
                this.CKB01_EPLCGB.Visible = false;
                this.CKB01_EPBORR.Visible = false;
                this.CKB01_ECSCASH.Visible = false;

            }

            this.TXT01_ECSISSU.SetReadOnly(true);
            this.TXT01_EPFACI.SetReadOnly(true);
            this.TXT01_EPINSA.SetReadOnly(true);
            this.TXT01_EPSALE.SetReadOnly(true);
            this.TXT01_TEPCTGB.SetReadOnly(true);
            this.TXT01_TEPLCGB.SetReadOnly(true);
            this.TXT01_EPBORR.SetReadOnly(true);
            this.TXT01_ECSCASH.SetReadOnly(true);

            if (this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) == "2013")
            {
                this.CKB01_EPBOGY.Visible = false; // 2013년 계획정보는 이미등록 되어 있어 사용안함
            };

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

        }
        #endregion

        #region Description : 생성 처리
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int iCCNT = 0;
            string sOUTMSG = string.Empty;
            string sELMYYMM = string.Empty;

            this.TXT01_ECSISSU.Text = ""; // s이슈정보
            this.TXT01_TEPLCGB.Text = ""; // s손익
            this.TXT01_TEPCTGB.Text = ""; // s계획

            this.TXT01_EPINSA.Text = ""; // s인사정보
            this.TXT01_EPSALE.Text = ""; // s주요매출
            this.TXT01_ECSCASH.Text = ""; // s자금수지
            

            string s이슈정보 = this.CKB01_ECSISSU.GetValue().ToString(); // s이슈정보
            string s주요시설 = this.CKB01_EPFACI.GetValue().ToString(); // s주요시설 현황
            string 인사정보 = this.CKB01_EPINSA.GetValue().ToString(); // s인사정보
            string 주요매출 = this.CKB01_EPSALE.GetValue().ToString(); // s주요매출

            string s계획 = this.CKB01_EPBOGY.GetValue().ToString(); // s계획
            string s손익처리 = this.CKB01_EPLCGB.GetValue().ToString();

            string s차입금 = this.CKB01_EPBORR.GetValue().ToString();
            string s자금수지 = this.CKB01_ECSCASH.GetValue().ToString();

            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();

            if (s이슈정보 == "A") { iCCNT = iCCNT + 1; };
            if (s주요시설 == "A") { iCCNT = iCCNT + 1; };
            if (인사정보 == "A") { iCCNT = iCCNT + 1; };
            if (주요매출 == "A") { iCCNT = iCCNT + 1; };
            if (s계획 == "Y") { iCCNT = iCCNT + 1; };
            if (s손익처리 == "A") { iCCNT = iCCNT + 1; };
            if (s차입금 == "A") { iCCNT = iCCNT + 1; };
            if (s자금수지 == "A") { iCCNT = iCCNT + 1; };

            #region Description : EIS 이슈정보 생성  (전월 복사 : 경영이슈 현황)
            if (s이슈정보 == "A")
            {
                string sYYMM = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;

                if (this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4)) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sYEAR = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4);
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2)) - 1));
                }

                sYYMM = sYEAR + sMONTH;

                this.DbConnector.CommandClear();

                if (s전체처리구분 == "D")
                {
                    //삭제(MASTER ,내역)
                    this.DbConnector.Attach("TY_P_AC_3BB10243", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString());
                    this.DbConnector.Attach("TY_P_AC_3BE5K300", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString());
                }
                else
                {
                    //삭제(MASTER ,내역)
                    this.DbConnector.Attach("TY_P_AC_3BB10243", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString());
                    this.DbConnector.Attach("TY_P_AC_3BE5K300", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString());

                    //복사(MASTER ,내역)
                    this.DbConnector.Attach("TY_P_AC_3BB13244", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    this.DbConnector.Attach("TY_P_AC_3BE5L301", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                }
                this.DbConnector.ExecuteTranQueryList();

                this.TXT01_ECSISSU.Text = "OK-정상적으로 처리되었습니다";
            }
            #endregion

            #region Description : EIS 주요시설 현황 (전월 복사 : 주요시설 현황)
            if (s주요시설 == "A")
            {
                string sYYMM = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;

                if (this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4)) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sYEAR = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4);
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2)) - 1));
                }

                sYYMM = sYEAR + sMONTH;

                this.DbConnector.CommandClear();

                if (s전체처리구분 == "D")
                {
                    //삭제
                    this.DbConnector.Attach("TY_P_AC_3B147167", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString());
                }
                else
                {
                    //삭제
                    this.DbConnector.Attach("TY_P_AC_3B147167", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString());

                    //복사
                    this.DbConnector.Attach("TY_P_AC_3B146166", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                }
                this.DbConnector.ExecuteTranQueryList();

                this.TXT01_EPFACI.Text = "OK-정상적으로 처리되었습니다";
            }
            #endregion

            #region Description : EIS 인사정보 생성 처리 (전월 복사 : 주주현황 ,  임원현황) , (해당월 생성 : 인원현황)
            if (인사정보 == "A")
            {
                string sYYMM = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;

                if (this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4)) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sYEAR = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4);
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2)) - 1));
                }

                sYYMM = sYEAR + sMONTH;

                if (s전체처리구분 == "D")
                {
                    // 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3992G619", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString()); // 주주현황
                    this.DbConnector.Attach("TY_P_AC_3992H620", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString()); // 임원현황
                    this.DbConnector.Attach("TY_P_AC_3992K621", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString()); // 임원 겸직 및 경력사항

                    if (this.CBH01_ESBMCUST.GetValue().ToString() != "TL") // 티와이스틸 인원현황 별도등록 됨
                    {
                        this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString()); // 인원현황
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }
                else
                {
                    // 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3992G619", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString()); // 주주현황
                    this.DbConnector.Attach("TY_P_AC_3992H620", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString()); // 임원현황
                    this.DbConnector.Attach("TY_P_AC_3992K621", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue().ToString()); // 임원 겸직 및 경력사항

                    // 복사
                    this.DbConnector.Attach("TY_P_AC_3992L622", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString()); // 임원현황
                    this.DbConnector.Attach("TY_P_AC_3992M623", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString()); // 임원 겸직 및 경력사항
                    this.DbConnector.Attach("TY_P_AC_3992M624", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString()); // 주주현황

                    this.DbConnector.ExecuteTranQueryList();

                    // 인원현황 
                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TG") // 태영그레인 터미널
                    {
                        UP_TG_ORGCD();
                    }

                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TS") // 태영 GLS
                    {
                        UP_TS_ORGCD();
                    }

                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TH") // 태영호라이즌
                    {
                        UP_TH_ORGCD();
                    }

                }

                this.TXT01_EPINSA.Text = "OK-정상적으로 처리되었습니다";
            }
            #endregion

            #region Description : EIS 주요매출 생성 처리
            if (주요매출 == "A")
            {
                string sGUBUN = string.Empty;
                string sYYMM_AGO = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;

                sYEAR = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4);
                sMONTH = this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2);

                if (sMONTH.ToString() == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(sYEAR) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(sMONTH) - 1));
                }

                sYYMM_AGO = sYEAR.ToString() + sMONTH.ToString();

                if (s전체처리구분 == "A")
                {
                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TG") // 그레인
                    {
                        // 태영그레인 - 생성 (SP)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_39B5E676",
                            this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                            sOUTMSG.ToString()
                            );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                        if (sOUTMSG.ToString().Substring(0, 1) == "I")
                        {
                            UP_TY_ESCUSTMF_UPT(); // 태그영레인 터미널 복사 처리 (192.168.100.8 --> 192.168.100.2)

                            this.TXT01_EPSALE.Text = "OK-정상적으로 처리되었습니다";
                        }
                        else
                        {
                            sOUTMSG = "ER-태영그레인터미널  처리중 ERROR 발생";
                        }
                    }
                    else if (this.CBH01_ESBMCUST.GetValue().ToString() == "TH") // 호라이즌
                    {
                        // 삭제
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39C42698", "TH", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        // 생성
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39C44699", "TH", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        this.TXT01_EPSALE.Text = "OK-정상적으로 처리되었습니다";

                    }
                    else if (this.CBH01_ESBMCUST.GetValue().ToString() == "TS") // GLS
                    {
                        // 삭제
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39G2M768", "TS", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        // 생성
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39G2P769", "TS", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        this.TXT01_EPSALE.Text = "OK-정상적으로 처리되었습니다";

                    }
                }  // 삭제
                else
                {
                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TG") // 그레인
                    {
                        // 태영그레인 - 취소
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39B5I680", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        // 태영 - 취소
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39B5K681", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        // 태영 - 기타매출처 삭제
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39H43808", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        this.TXT01_EPSALE.Text = "OK-정상적으로 처리되었습니다";
                    }
                    else if (this.CBH01_ESBMCUST.GetValue().ToString() == "TH") // 호라이즌
                    {
                        // 삭제
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39C42698", "TH", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        this.TXT01_EPSALE.Text = "OK-정상적으로 처리되었습니다";
                    }
                    else if (this.CBH01_ESBMCUST.GetValue().ToString() == "TS") // 호라이즌
                    {
                        // 삭제
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39G2M768", "TS", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                        this.DbConnector.ExecuteNonQuery();

                        this.TXT01_EPSALE.Text = "OK-정상적으로 처리되었습니다";
                    }
                }
            }
            #endregion

            #region Description : EIS 계획 생성 처리 (태영그레인 터미널 : 192.168.100.8 - TYSCMLIB.ESUBMANAGF)
            if (s계획 == "Y")
            {
                // ---------------------------------------------------------------------------------------------------------------------------
                // 손익관련 계획금액은 1년에 한번 엑셀 자료 등록하므로 예산자료 가지고 처리 하지않음(예산 자료엔 매출,당기순이익 관련 자료가 없음)
                ////  SP
                //if (this.CBH01_ESBMCUST.GetValue().ToString() != "TG")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_AC_3994S625",
                //        this.DTP01_GSTYYMM.GetValue(),
                //        this.CBH01_ESBMCUST.GetValue().ToString(),
                //        "PL", // 계획 PL , 실적 US 
                //        s전체처리구분,  // 생성 A , 취소 D 
                //        sOUTMSG.ToString()
                //        );
                //}
                //else // 그레인터미널
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_AC_3AF2J054",
                //        this.DTP01_GSTYYMM.GetValue(),
                //        this.CBH01_ESBMCUST.GetValue().ToString(),
                //        "PL", // 계획 PL , 실적 US 
                //        s전체처리구분,  // 생성 A , 취소 D 
                //        sOUTMSG.ToString()
                //        );
                //}

                //sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                sOUTMSG = "OK-0000 정상처리";

                this.TXT01_TEPCTGB.Text = sOUTMSG;
            }
            #endregion

            #region Description : EIS 계정원장 실적 생성 처리 (태영그레인 터미널 : 192.168.100.8 - TYSCMLIB.ESMBPMMF)
            if (s손익처리 == "A")
            {
                //  SP
                if (this.CBH01_ESBMCUST.GetValue().ToString() != "TG")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_39R4M897",
                        this.DTP01_GSTYYMM.GetValue(),
                        this.CBH01_ESBMCUST.GetValue().ToString(),
                        "US", // 계획 PL , 실적 US 
                        s전체처리구분, // 생성 A , 취소 D 
                        sOUTMSG.ToString()
                        );
                }
                else  // 그레인터미널
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3AF24052",
                        this.DTP01_GSTYYMM.GetValue(),
                        this.CBH01_ESBMCUST.GetValue().ToString(),
                        "US", // 계획 PL , 실적 US 
                        s전체처리구분, // 생성 A , 취소 D 
                        sOUTMSG.ToString()
                        );
                }

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPLCGB.Text = sOUTMSG;
            }
            #endregion

            #region Description : EIS 차입금 생성 처리 (태영그레인 터미널 : 192.168.100.8 - TYSCMLIB.EMSLOANMF , TYSCMLIB.EMSLOANSF)
            if (s차입금 == "A")
            {
                if (this.CBH01_ESBMCUST.GetValue().ToString() != "TG")
                {
                    //  SP
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3AA4F034",
                        s전체처리구분, // 생성 A , 취소 D 
                        this.CBH01_ESBMCUST.GetValue().ToString(),
                        this.DTP01_GSTYYMM.GetValue(),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    // 차입금 세부자료 생성(관리카드용)
                    //if (sOUTMSG.Substring(0, 2) == "OK")
                    //{
                    //    //  SP
                    //    this.DbConnector.CommandClear();
                    //    this.DbConnector.Attach
                    //        (
                    //        "TY_P_AC_3AB12043",
                    //        s전체처리구분, // 생성 A , 취소 D 
                    //        this.CBH01_ESBMCUST.GetValue().ToString(),
                    //        this.DTP01_GSTYYMM.GetValue(),
                    //        sOUTMSG.ToString()
                    //        );
                    //}

                    //sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }
                else  // 그레인터미널
                {
                    //  SP
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3AH4T072",
                        s전체처리구분, // 생성 A , 취소 D 
                        this.CBH01_ESBMCUST.GetValue().ToString(),
                        this.DTP01_GSTYYMM.GetValue(),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        UP_TY_EMSLOANMF_UPT();  // 태그영레인 터미널 복사 처리 (192.168.100.8 --> 192.168.100.2)
                    }
                    else
                    {
                        sOUTMSG = "ER-태영그레인터미널  처리중 ERROR 발생";
                    }

                    // 차입금 세부자료 생성(관리카드용)
                    //if (sOUTMSG.Substring(0, 2) == "OK")
                    //{
                    //    //  SP
                    //    this.DbConnector.CommandClear();
                    //    this.DbConnector.Attach
                    //        (
                    //        "TY_P_AC_3AH4W073",
                    //        s전체처리구분, // 생성 A , 취소 D 
                    //        this.CBH01_ESBMCUST.GetValue().ToString(),
                    //        this.DTP01_GSTYYMM.GetValue(),
                    //        sOUTMSG.ToString()
                    //        );
                    //}

                    //sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }

                this.TXT01_EPBORR.Text = sOUTMSG;
            }
            #endregion

            #region Description : EIS 자금수지 생성 처리 (추가)
            if (s자금수지 == "A")
            {
                if (s전체처리구분 == "D")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A81K001", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteTranQuery();

                    sOUTMSG = "OK-정상적으로 처리되었습니다.";
                }
                else
                {

                    double dEAFSAMMWon = 0;
                    double dEAFNEMMWon = 0;

                    double dEAFSAMM = 0;
                    double dEAFNEMM = 0;


                    double dCHAIP_IN1 = 0; //차입금 상환
                    double dCHAIP_IN2 = 0; //차입금 상환
                    double dCHAIP_UP1 = 0;  //차입금 증가
                    double dCHAIP_UP2 = 0;  //차입금 증가

                    double dUPFUND1 = 0;  //증자
                    double dUPFUND2 = 0;  //증자

                    double dJUNWOLTOTAL1 = 0; //전월이월
                    double dIPTOTAL1 = 0; //수입계
                    double dCHTOTAL1 = 0; //지출계

                    double dJUNWOLTOTAL2 = 0; //전월이월(예상)
                    double dIPTOTAL2 = 0; //수입계(예상)
                    double dCHTOTAL2 = 0; //지출계(예상)

                    double dLastEAFSAMM = 0;
                    double dLastEAFNEMM = 0;

                    System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A81K001", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteTranQuery();


                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A81I998", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //원단위 금액
                            dEAFSAMMWon = Convert.ToDouble(dt.Rows[i]["EAFSAMM"].ToString());
                            dEAFNEMMWon = Convert.ToDouble(dt.Rows[i]["EAFNEMM"].ToString());

                            //100만원으로 정리
                            dEAFSAMM = Math.Floor(Convert.ToDouble(dt.Rows[i]["EAFSAMM"].ToString()) / 1000000) * 1000000;
                            dEAFNEMM = Math.Floor(Convert.ToDouble(dt.Rows[i]["EAFNEMM"].ToString()) / 1000000) * 1000000;

                            dLastEAFSAMM = dEAFSAMM;
                            dLastEAFNEMM = dEAFNEMM;

                            if (dt.Rows[i]["EAFSEQN"].ToString() == "1000")  //전월이월
                            {
                                dJUNWOLTOTAL1 = dEAFSAMM;
                                dJUNWOLTOTAL2 = dEAFNEMM;
                            }

                            if (dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //원가 row
                            {
                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "2" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //수입
                                {
                                    dIPTOTAL1 = dIPTOTAL1 + dEAFSAMMWon;
                                    dIPTOTAL2 = dIPTOTAL2 + dEAFNEMMWon;
                                }

                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "3" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //지출
                                {
                                    dCHTOTAL1 = dCHTOTAL1 + dEAFSAMMWon;
                                    dCHTOTAL2 = dCHTOTAL2 + dEAFNEMMWon;
                                }

                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "4" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //차입금 상환
                                {
                                    dCHAIP_IN1 = dCHAIP_IN1 + dEAFSAMMWon;
                                    dCHAIP_IN2 = dCHAIP_IN2 + dEAFNEMMWon;
                                }

                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "5" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //차입금 증가
                                {
                                    dCHAIP_UP1 = dCHAIP_UP1 + dEAFSAMMWon;
                                    dCHAIP_UP2 = dCHAIP_UP2 + dEAFNEMMWon;
                                }


                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "6" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //증자
                                {
                                    dUPFUND1 = dUPFUND1 + dEAFSAMMWon;
                                    dUPFUND2 = dUPFUND2 + dEAFNEMMWon;
                                }

                            }
                            else //집계 row
                            {
                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "2" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "2")  //수입계
                                {
                                    dLastEAFSAMM = Math.Floor(dIPTOTAL1 / 1000000) * 1000000;
                                    dLastEAFNEMM = Math.Floor(dIPTOTAL2 / 1000000) * 1000000;
                                }

                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "3" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "2")  //지출계
                                {
                                    dLastEAFSAMM = Math.Floor(dCHTOTAL1 / 1000000) * 1000000;
                                    dLastEAFNEMM = Math.Floor(dCHTOTAL2 / 1000000) * 1000000;
                                }
                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "4" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "1")  //자금과부족
                                {

                                    dLastEAFSAMM = dJUNWOLTOTAL1 + (Math.Floor(dIPTOTAL1 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL1 / 1000000) * 1000000);
                                    dLastEAFNEMM = dJUNWOLTOTAL2 + (Math.Floor(dIPTOTAL2 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL2 / 1000000) * 1000000);
                                }

                                if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "9" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "1")  //차월이월금
                                {
                                    double dAmt1 = dJUNWOLTOTAL1 + (Math.Floor(dIPTOTAL1 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL1 / 1000000) * 1000000);
                                    double dAmt2 = dJUNWOLTOTAL2 + (Math.Floor(dIPTOTAL2 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL2 / 1000000) * 1000000);

                                    dLastEAFSAMM = dAmt1 - (Math.Floor(dCHAIP_IN1 / 1000000) * 1000000) + (Math.Floor(dCHAIP_UP1 / 1000000) * 1000000) + (Math.Floor(dUPFUND1 / 1000000) * 1000000);
                                    dLastEAFNEMM = dAmt2 - (Math.Floor(dCHAIP_IN2 / 1000000) * 1000000) + (Math.Floor(dCHAIP_UP2 / 1000000) * 1000000) + (Math.Floor(dUPFUND2 / 1000000) * 1000000);
                                }
                            }

                            datas.Add(new object[] {dt.Rows[i]["EAFSUBGN"].ToString(),  //1
                                                dt.Rows[i]["EAFYYMM"].ToString(),  //2
                                                dt.Rows[i]["EAFSEQN"].ToString(),  //3
                                                dt.Rows[i]["EAFTINM"].ToString(),  //4
                                                dt.Rows[i]["EAFLEVE"].ToString(),  //5
                                                dLastEAFSAMM.ToString(),  //6
                                                dLastEAFNEMM.ToString(),  //7
                                                ""   //8
                                               });
                        }

                        if (datas.Count > 0)
                        {
                            this.DbConnector.CommandClear();
                            foreach (object[] data in datas)
                            {
                                this.DbConnector.Attach("TY_P_AC_3A81K999", data);
                            }
                        }
                        this.DbConnector.ExecuteTranQueryList();
                    }

                    sOUTMSG = "OK-정상적으로 처리되었습니다.";                  
                }

                this.TXT01_ECSCASH.Text = sOUTMSG;
            }
            #endregion

            if (iCCNT > 0)
            {
                sOUTMSG = "처리완료 되었습니다.";
            }
            else
            {
                sOUTMSG = "처리할구분을 선택하세요.";
            }

            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        } 
        #endregion

        #region Description : 태그영레인 터미널  ESCUSTMF 업데이트 192.168.100.8 --> 192.168.100.2 (주요매출 생성 처리)
        private void UP_TY_ESCUSTMF_UPT()
        {
            int i = 0;

            // 태영-전체삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39B5T684", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteNonQuery();

            DataTable dt = new DataTable();

            // 태영그레인 터미널 192.168.100.8 --> 192.168.100.2 복사
            // 태영그레인 터미널 주요매출 생성 처리(22.01.26일 수정)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39B5Q683", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteNonQuery();

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    for (i = 0; i < dt.Rows.Count; i++)
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_AC_39B5V685",
            //            dt.Rows[i]["ESCSUBGN"].ToString(),
            //            dt.Rows[i]["ESCVEND"].ToString(),
            //            dt.Rows[i]["ESCYYMM"].ToString(),
            //            dt.Rows[i]["ESCHWAMUL1"].ToString(),
            //            dt.Rows[i]["ESCHWAMUL2"].ToString(),
            //            dt.Rows[i]["ESCHWAMUL3"].ToString(),
            //            dt.Rows[i]["ESCVOLME"].ToString(),
            //            dt.Rows[i]["ESCMAEAMT"].ToString(),
            //            dt.Rows[i]["ESCMAEVN1"].ToString(),
            //            dt.Rows[i]["ESCMAEVN2"].ToString(),
            //            dt.Rows[i]["ESCMAEVN3"].ToString(),
            //            dt.Rows[i]["ESCMAEVN4"].ToString(),
            //            dt.Rows[i]["ESCMAEVN5"].ToString(),
            //            dt.Rows[i]["ESCMAEVN6"].ToString(),
            //            dt.Rows[i]["ESCMAEVN7"].ToString(),
            //            dt.Rows[i]["ESCMAEVN8"].ToString(),
            //            dt.Rows[i]["ESCMAEVN9"].ToString(),
            //            dt.Rows[i]["ESCMAEVNT"].ToString()
            //            );
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
                
            //}
        }
        #endregion

        #region Description : 태그영레인 터미널  EMSLOANMF 업데이트 192.168.100.8 --> 192.168.100.2 (차입금 MASTER)
        private void UP_TY_EMSLOANMF_UPT()
        {
            int i = 0;

            // 태영-전체삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3AV9D149", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteNonQuery();

            DataTable dt = new DataTable();

            // 태영그레인 터미널 192.168.100.8 --> 192.168.100.2 복사
            // 22.01.26일 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3AV9A147", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteNonQuery();

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.DbConnector.CommandClear();
            //    for (i = 0; i < dt.Rows.Count; i++)
            //    {
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_AC_3A75B977",
            //               dt.Rows[i]["ESLOCUST"].ToString(),    //-- 계열사구분
            //               dt.Rows[i]["ESLOYYMM"].ToString(),    //-- 년월
            //               dt.Rows[i]["ESLOCDAC"].ToString(),    //-- 계정과목
            //               dt.Rows[i]["ESLOVEVL"].ToString(),    //-- LEVEL
            //               dt.Rows[i]["ESLOJM00"].ToString(),    //-- 전월잔액
            //               dt.Rows[i]["ESLOJMBYL"].ToString(),   //-- 전월비율
            //               dt.Rows[i]["ESLOREPAY"].ToString(),   //-- 당월상환액
            //               dt.Rows[i]["ESLOCAPIT"].ToString(),   //-- 당월차입액
            //               dt.Rows[i]["ESLOMMJMT"].ToString(),   //-- 당월잔액
            //               dt.Rows[i]["ESLOMMBYL"].ToString()    //-- 당월비율
            //            );
            //    }
            //    this.DbConnector.ExecuteTranQueryList();
            //}
        }
        #endregion


        #region  Description : 태영그레인 자금생성 확정분 생성
        private void UP_TG_FoundFixData(string sYYMM)
        {
            string sGubun = string.Empty;

            string sEAFSUBGN = "TG";
            string sEAFYYMM = sYYMM;
            string sEAFSEQN = string.Empty;
            string sEAFTINM = string.Empty;
            string sEAFLEVE = string.Empty;
            double dEAFSAMM = 0;
            double dEAFNEMM = 0;

            double dJunWolTotal = 0;

            double dIPTotal = 0;
            double dCHTotal = 0;

            double dChaOutTotal = 0;
            double dChaUpTotal = 0;
            double dUpTotal = 0;

            DateTime dTime = new DateTime();
            dTime = Convert.ToDateTime(sYYMM.ToString().Substring(0, 4) + "-" + sYYMM.ToString().Substring(4, 2) + "-01");
            dTime = dTime.AddMonths(-1);
            string sDate = dTime.Year.ToString() + Set_Fill2(dTime.Month.ToString()) + Set_Fill2(dTime.Day.ToString());

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            //192.168.100.8 -> 192.168.100.2 이관
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3BF90305");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                dEAFNEMM = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sGubun = dt.Rows[i]["EDCODE"].ToString().Substring(0, 1);

                    sEAFSEQN = dt.Rows[i]["EDCODE"].ToString();
                    sEAFTINM = dt.Rows[i]["EDDESC1"].ToString();


                    if (sGubun == "1") //전월이월
                    {
                        sEAFLEVE = "1";
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9E306", sEAFSUBGN, sYYMM);
                        dJunWolTotal = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        dEAFSAMM = dJunWolTotal;
                    }

                    if (sGubun == "2") //수입
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "1");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //수입 누적
                        dIPTotal = dIPTotal + dEAFSAMM;
                    }

                    if (sGubun == "3") //지출
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "2");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //지출 누적
                        dCHTotal = dCHTotal + dEAFSAMM;
                    }

                    if (sGubun == "4") //차입금상환
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "3");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //차입금상환 누적
                        dChaOutTotal = dChaOutTotal + dEAFSAMM;
                    }

                    if (sGubun == "5") //차입금증가
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "4");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //차입금상환 누적
                        dChaUpTotal = dChaUpTotal + dEAFSAMM;
                    }

                    if (sGubun == "6") //증자
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "5");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        dUpTotal = dUpTotal + dEAFSAMM;
                    }

                    datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });

                }//for..end

                //수입누계
                sEAFSEQN = "2999";
                sEAFTINM = "수입계";
                sEAFLEVE = "2";
                dEAFSAMM = dIPTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
                //지출누계
                sEAFSEQN = "3999";
                sEAFTINM = "지출계";
                sEAFLEVE = "2";
                dEAFSAMM = dCHTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
                //자금과부족
                sEAFSEQN = "4000";
                sEAFTINM = "자금과부족";
                sEAFLEVE = "1";
                dEAFSAMM = dJunWolTotal + dIPTotal - dCHTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
                //차월이월금 = 자금과부족 - 차입금상환 + 차입금증가 + 증자
                sEAFSEQN = "9999";
                sEAFTINM = "차월이월금";
                sEAFLEVE = "1";
                dEAFSAMM = (dJunWolTotal + dIPTotal - dCHTotal) - dChaOutTotal + dChaUpTotal + dUpTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_AC_3A12K936", data);
                }
            }
            this.DbConnector.ExecuteTranQueryList();

        }
        #endregion

        #region Description : EIS 마감 년월 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            string s이슈정보 = this.CKB01_ECSISSU.GetValue().ToString(); // s이슈정보
            string s주요시설 = this.CKB01_EPFACI.GetValue().ToString(); // s주요시설 현황
            string 인사정보 = this.CKB01_EPINSA.GetValue().ToString(); // s인사정보
            string s자금수지 = this.CKB01_ECSCASH.GetValue().ToString(); // s자금수지

            
            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();

            // 생성일때 전월 자료 존재 유무 확인

            #region Description : EIS 경영이슈 생성 처리 (전월 체크)
            if (s이슈정보 == "A")
            {
                string sYYMM = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;
                int iCnt = 0;

                if (this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4)) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sYEAR = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4);
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2)) - 1));
                }

                sYYMM = sYEAR + sMONTH;

                if (s전체처리구분 != "D")
                {
                    //경영이슈 전월 존재 체크
                    this.DbConnector.CommandClear(); //ESISSUEF (전월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3BBCD242", sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_EPFACI.Text = sYYMM.ToString().Substring(0, 4) + " 년" + sYYMM.ToString().Substring(4, 2) + " 월 자료가 없습니다! (복사 불가)";

                        this.ShowMessage("TY_M_AC_3B151169"); // 이전월 자료가 없습니다.. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }
            #endregion 

            #region Description : EIS 주요시설 생성 처리 (전월 체크)
            if (s주요시설 == "A")
            {
                string sYYMM = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;
                int iCnt = 0;

                if (this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4)) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sYEAR = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4);
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2)) - 1));
                }

                sYYMM = sYEAR + sMONTH;

                if (s전체처리구분 != "D")
                {
                    //주요시설 존재 체크
                    this.DbConnector.CommandClear(); //EDINSTALLF (전월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B14N168", sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_EPFACI.Text = sYYMM.ToString().Substring(0, 4) + " 년" + sYYMM.ToString().Substring(4, 2) + " 월 자료가 없습니다! (복사 불가)";

                        this.ShowMessage("TY_M_AC_3B151169"); // 이전월 자료가 없습니다.. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }
            #endregion 

            #region Description : EIS 인사정보 생성 처리 (전월 체크)
            if (인사정보 == "A")
            {
                string sYYMM = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;
                int iCnt = 0;

                if (this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4)) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sYEAR = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4);
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2)) - 1));
                }

                sYYMM = sYEAR + sMONTH;

                this.DbConnector.CommandClear();

                if (s전체처리구분 != "D")
                {
                    //주주현황 존재 체크
                    this.DbConnector.CommandClear(); //EDSTHOLDLISTF (전월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B15A170", sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_EPINSA.Text = sYYMM.ToString().Substring(0, 4) + " 년" + sYYMM.ToString().Substring(4, 2) + " 월 주주현황 자료가 없습니다! (복사 불가)";

                        this.ShowMessage("TY_M_AC_3B151169"); // 이전월 자료가 없습니다.. (처리 불가)
                        e.Successed = false;
                        return;
                    }

                    //임원현황 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EDOFFICERLISTF (전월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B15B171", sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_EPINSA.Text = sYYMM.ToString().Substring(0, 4) + " 년" + sYYMM.ToString().Substring(4, 2) + " 월 임원현황 자료가 없습니다! (복사 불가)";

                        this.ShowMessage("TY_M_AC_3B151169"); // 이전월 자료가 없습니다.. (처리 불가)
                        e.Successed = false;
                        return;
                    }

                    //임원겸직 및 경력사항 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EDOFFICERHOLDF (전월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B15C172", sYYMM.ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_EPINSA.Text = sYYMM.ToString().Substring(0, 4) + " 년" + sYYMM.ToString().Substring(4, 2) + " 월 임원겸직 및 경력사항 자료가 없습니다! (복사 불가)";

                        this.ShowMessage("TY_M_AC_3B151169"); // 이전월 자료가 없습니다.. (처리 불가)
                        e.Successed = false;
                        return;
                    }

                }


            }
            #endregion

            #region Description : EIS 자금수지 생성 처리 (확정 체크)
            if (s자금수지 == "A")
            {
                //해당월 확정시 작업금지
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3CD2P754", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0, 6));

                Int32 iCnt = Convert.ToInt32(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_GB_3A82W005");
                    e.Successed = false;
                    return;
                }
            }
            #endregion

        }
        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion


        #region Description : 계열사 코드 변경시 처리
        private void CBH01_ESBMCUST_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_ESBMCUST.GetValue().ToString() == "TL")
            {
                this.CKB01_EPFACI.Visible = false;
                this.CKB01_EPSALE.Visible = false;
                this.CKB01_EPBOGY.Visible = false;
                this.CKB01_EPLCGB.Visible = false;
                this.CKB01_EPBORR.Visible = false;
                this.CKB01_ECSCASH.Visible = false;
            }
            else
            {
                this.CKB01_EPFACI.Visible = true;
                this.CKB01_EPSALE.Visible = true;
                this.CKB01_EPBOGY.Visible = true;
                this.CKB01_EPLCGB.Visible = true;
                this.CKB01_EPBORR.Visible = true;
                this.CKB01_ECSCASH.Visible = true;
            }

        } 
        #endregion


        #region Description : 계열사 인원생성 함수
        #region 태영호라이즌
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
            this.DbConnector.Attach("TY_P_HR_34F4H503", this.DTP01_GSTYYMM.GetString().Substring(0, 6));
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
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
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
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
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

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
                                                       iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

        } 
	    #endregion

        #region GLS
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

            //string sA5EPPRIOR_ORG_CD = "";
            //string sA5EPPRIOR_ORG_CDNM = "";
            //Int16 iA5EPLIST01 = 0;
            //Int16 iA5EPLIST02 = 0;
            //Int16 iA5EPLIST03 = 0;
            //Int16 iA5EPLIST04 = 0;
            //Int16 iA5EPLIST05 = 0;
            //Int16 iA5EPLIST06 = 0;
            //Int16 iA5EPLIST07 = 0;
            //Int16 iA5EPLIST08 = 0;
            //Int16 iA5EPLIST09 = 0;
            //Int16 iA5EPLIST10 = 0;
            //Int16 iA5EPLIST11 = 0;
            //Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_34F49502", this.DTP01_GSTYYMM.GetString().Substring(0, 6));
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

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);

            //this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
            //                                           iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

        } 
        #endregion

        #region 그레인
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

            //string sBEPPRIOR_ORG_CD = "";
            //string sBEPPRIOR_ORG_CDNM = "";
            //Int16 iBEPLIST01 = 0;
            //Int16 iBEPLIST02 = 0;
            //Int16 iBEPLIST03 = 0;
            //Int16 iBEPLIST04 = 0;
            //Int16 iBEPLIST05 = 0;
            //Int16 iBEPLIST06 = 0;
            //Int16 iBEPLIST07 = 0;
            //Int16 iBEPLIST08 = 0;
            //Int16 iBEPLIST09 = 0;
            //Int16 iBEPLIST10 = 0;
            //Int16 iBEPLIST11 = 0;
            //Int16 iBEPLIST12 = 0;

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
            this.DbConnector.Attach("TY_P_HR_34F34500", this.DTP01_GSTYYMM.GetString().Substring(0, 6));
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
                    else if (sSaupCode == "4")
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

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
                                                       iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.CBH01_ESBMCUST.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

        }
        #endregion
        #endregion
    }
}
