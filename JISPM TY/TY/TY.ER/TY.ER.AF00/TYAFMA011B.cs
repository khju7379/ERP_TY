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
    public partial class TYAFMA011B : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYAFMA011B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYAFMA011B_Load(object sender, System.EventArgs e)
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
                this.CKB01_ECSFACI.Visible = false;
            }

            this.TXT01_ECSISSU.SetReadOnly(true);
            this.TXT01_ECSFACI.SetReadOnly(true);
            this.TXT01_ECSINSA.SetReadOnly(true);
            this.TXT01_ECSSALE.SetReadOnly(true);
            this.TXT01_ECSPLAN.SetReadOnly(true);
            this.TXT01_ECSRESU.SetReadOnly(true);
            this.TXT01_ECSLOAN.SetReadOnly(true);
            this.TXT01_ECSCASH.SetReadOnly(true);

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
            this.TXT01_ECSFACI.Text = ""; // s시설정보
            this.TXT01_ECSINSA.Text = ""; // s인사정보
            this.TXT01_ECSSALE.Text = ""; // s매출정보
            this.TXT01_ECSPLAN.Text = ""; // s계획정보
            this.TXT01_ECSRESU.Text = ""; // s손익정보
            this.TXT01_ECSLOAN.Text = ""; // s차입금정보
            this.TXT01_ECSCASH.Text = ""; // s자금수지


            string s이슈정보 = this.CKB01_ECSISSU.GetValue().ToString(); // s이슈정보
            string s시설정보 = this.CKB01_ECSFACI.GetValue().ToString(); // s시설정보
            string s인사정보 = this.CKB01_ECSINSA.GetValue().ToString(); // s인사정보
            string s매출정보 = this.CKB01_ECSSALE.GetValue().ToString(); // s매출정보
            string s계획정보 = this.CKB01_ECSPLAN.GetValue().ToString(); // s계획정보
            string s손익정보 = this.CKB01_ECSRESU.GetValue().ToString(); // s손익정보
            string s차입정보 = this.CKB01_ECSLOAN.GetValue().ToString(); // s차입금정보
            string s자금수지 = this.CKB01_ECSCASH.GetValue().ToString(); // s자금수지

            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();

            if (s이슈정보 == "A") { iCCNT = iCCNT + 1; };
            if (s시설정보 == "A") { iCCNT = iCCNT + 1; };
            if (s인사정보 == "A") { iCCNT = iCCNT + 1; };
            if (s매출정보 == "A") { iCCNT = iCCNT + 1; };
            if (s계획정보 == "A") { iCCNT = iCCNT + 1; };
            if (s손익정보 == "A") { iCCNT = iCCNT + 1; };
            if (s차입정보 == "A") { iCCNT = iCCNT + 1; };
            if (s자금수지 == "A") { iCCNT = iCCNT + 1; };


            #region Description : EIS 계획금액 확정( 백만단위 정리) 계획정보
            if (s계획정보 == "A")
            {
                sOUTMSG = "";

                if (s전체처리구분 == "D")
                {
                    // 기존 자료 삭제(년단위 삭제)
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_AC_39OAG841", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetValue());
                    //this.DbConnector.ExecuteNonQuery();

                    this.TXT01_ECSPLAN.Text = "OK-0000 정상처리";
                }
                else
                {
                    UP_TY_ESUBMANAGF_UPT();
                }

                //this.TXT01_ECSRESU.Text = "OK-정상적으로 처리되었습니다";
            }
            #endregion

            #region Description : EIS 계정원장 확정(손익정보) --> 백만단위 정리 (그레인 자료 COPY 192.168.100.8 --> 192.168.100.2)
            if (s손익정보 == "A")
            {
                if (s전체처리구분 == "D")
                {
                    // 기존 자료 삭제(월단위 삭제)
                    sOUTMSG = "";
                    string sConnID = string.Empty;

                    if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
                    {
                        sConnID = "TY_P_AC_3BFBH313";
                    }
                    else
                    {
                        sConnID = "TY_P_AC_3BFBA311";
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach( sConnID, this.DTP01_GSTYYMM.GetValue(), this.CBH01_ESBMCUST.GetValue(), "US", "D" , sOUTMSG.ToString());
                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
                    {
                        // 기존 자료 전체삭제(년) ECFBPMMF
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39OAG841", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4));
                        this.DbConnector.ExecuteNonQuery();

                        // TYGT 그레인 자료 읽어서 192.168.100.8 --> 192.168.100.2(인더스트리 DB에 저장)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_39OAH842", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4));
                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count != 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_AC_39OAH843",
                                                      dt.Rows[i]["ECFMCUST"].ToString(),
                                                      dt.Rows[i]["ECFMYYHD"].ToString(),
                                                      dt.Rows[i]["ECFMCDAC"].ToString(),
                                                      dt.Rows[i]["ECFMCDNM"].ToString(),
                                                      dt.Rows[i]["ECFMAM00"].ToString(),
                                                      dt.Rows[i]["ECFMAM01"].ToString(),
                                                      dt.Rows[i]["ECFMAM02"].ToString(),
                                                      dt.Rows[i]["ECFMAM03"].ToString(),
                                                      dt.Rows[i]["ECFMAM04"].ToString(),
                                                      dt.Rows[i]["ECFMAM05"].ToString(),
                                                      dt.Rows[i]["ECFMAM06"].ToString(),
                                                      dt.Rows[i]["ECFMAM07"].ToString(),
                                                      dt.Rows[i]["ECFMAM08"].ToString(),
                                                      dt.Rows[i]["ECFMAM09"].ToString(),
                                                      dt.Rows[i]["ECFMAM10"].ToString(),
                                                      dt.Rows[i]["ECFMAM11"].ToString(),
                                                      dt.Rows[i]["ECFMAM12"].ToString(),
                                                      dt.Rows[i]["ECFMTAG01"].ToString(),
                                                      dt.Rows[i]["ECFMLVA01"].ToString());
                                this.DbConnector.ExecuteTranQueryList();
                                //this.DbConnector.ExecuteNonQuery();
                            }
                        }

                    }

                    this.TXT01_ECSRESU.Text = "OK-정상적으로 처리되었습니다";

                }
                else
                {
                    UP_TY_ECFBPMMF_UPT();
                }

            }
            #endregion

            #region Description : EIS 자금수지 확정
            if (s자금수지 == "A")
            {
                sOUTMSG = "";

                if (s전체처리구분 == "D")
                {
                    //삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A81K001", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteTranQueryList();
                }
                else
                {
                    UP_TY_EDAFFUNDMF_UPT();
                }
                this.TXT01_ECSCASH.Text = "OK-정상적으로 처리되었습니다";
            }
            #endregion

            if (iCCNT > 0)
            {
                if (s전체처리구분 == "D")
                {
                    // 삭제 처리
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3B79T216",
                                             "N", // 이슈정보
                                             "N", // 시설정보
                                             "N", // 인사정보
                                             "N", // 매출정보
                                             "N", // 계획정보
                                             "N", // 손익정보
                                             "N", // 차입금정보
                                             "N", // 자금수지
                                             TYUserInfo.EmpNo.ToString(),
                                             this.CBH01_ESBMCUST.GetValue(),
                                             this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteTranQueryList();

                    if (s이슈정보 == "A") { this.TXT01_ECSISSU.Text = "OK-처리완료"; };
                    if (s시설정보 == "A") { this.TXT01_ECSFACI.Text = "OK-처리완료"; };
                    if (s인사정보 == "A") { this.TXT01_ECSINSA.Text = "OK-처리완료"; };
                    if (s매출정보 == "A") { this.TXT01_ECSSALE.Text = "OK-처리완료"; };
                    if (s차입정보 == "A") { this.TXT01_ECSLOAN.Text = "OK-처리완료"; };
                    if (s자금수지 == "A") { this.TXT01_ECSCASH.Text = "OK-처리완료"; };
                }
                else
                {
                    // 생성 처리
                    this.DbConnector.CommandClear();
                    if (fsCompanyCode == "TS") // 태영 GLS 이슈정보 없음
                    {
                        this.DbConnector.Attach("TY_P_AC_3B79T216",
                                                 "N", // 이슈정보
                                                 "Y", // 시설정보
                                                 "Y", // 인사정보
                                                 "Y", // 매출정보
                                                 "Y", // 계획정보
                                                 "Y", // 손익정보
                                                 "Y", // 차입금정보
                                                 "Y", // 자금수지
                                                 TYUserInfo.EmpNo.ToString(),
                                                 this.CBH01_ESBMCUST.GetValue(),
                                                 this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    }
                    else if (fsCompanyCode == "TL") // 티와이스트리 시설정보 없음
                    {
                        this.DbConnector.Attach("TY_P_AC_3B79T216",
                                                 "Y", // 이슈정보
                                                 "N", // 시설정보
                                                 "Y", // 인사정보
                                                 "Y", // 매출정보
                                                 "Y", // 계획정보
                                                 "Y", // 손익정보
                                                 "Y", // 차입금정보
                                                 "Y", // 자금수지
                                                 TYUserInfo.EmpNo.ToString(),
                                                 this.CBH01_ESBMCUST.GetValue(),
                                                 this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_AC_3B79T216",
                                                "Y", // 이슈정보
                                                "Y", // 시설정보
                                                "Y", // 인사정보
                                                "Y", // 매출정보
                                                "Y", // 계획정보
                                                "Y", // 손익정보
                                                "Y", // 차입금정보
                                                "Y", // 자금수지
                                                TYUserInfo.EmpNo.ToString(),
                                                this.CBH01_ESBMCUST.GetValue(),
                                                this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    }

                    this.DbConnector.ExecuteTranQueryList();

                    if (s이슈정보 == "A") { this.TXT01_ECSISSU.Text = "OK-처리완료"; };
                    if (s시설정보 == "A") { this.TXT01_ECSFACI.Text = "OK-처리완료"; };
                    if (s인사정보 == "A") { this.TXT01_ECSINSA.Text = "OK-처리완료"; };
                    if (s매출정보 == "A") { this.TXT01_ECSSALE.Text = "OK-처리완료"; };
                    if (s차입정보 == "A") { this.TXT01_ECSLOAN.Text = "OK-처리완료"; };
                    if (s자금수지 == "A") { this.TXT01_ECSCASH.Text = "OK-처리완료"; };
                }

                sOUTMSG = "처리완료 되었습니다.";
            }
            else
            {
                sOUTMSG = "처리할구분을 선택하세요.";
            }

            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        } 
        #endregion


        #region Description : 계획자료 확정(계획정보) (UP_TY_ESUBMANAGF_UPT) -- 백만단위 정리
        private void UP_TY_ESUBMANAGF_UPT()
        {
            string sOUTMSG = string.Empty;
            string sConnID = string.Empty;

            if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
            {
                sConnID = "TY_P_AC_3AF3J057";
            }
            else
            {
                sConnID = "TY_P_AC_3A72I974";
            }

            // 상위계정 계산 및 백만단위 정리
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sConnID,
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4),
                this.CBH01_ESBMCUST.GetValue(),
                "PL",
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.TXT01_ECSPLAN.Text = sOUTMSG;
            }
            else
            {

                #region 그레인터미널 COPY
                if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
                {
                    // 기존 자료 전체삭제(년)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3BFBT314", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4));
                    this.DbConnector.ExecuteNonQuery();

                    // TYGT 그레인 (ESUBMANAGF)자료 읽어서 192.168.100.8 --> 192.168.100.2(인더스트리 DB에 저장)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3BFBV316", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4));
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_3BFBU315",
                                                    dt.Rows[i]["ESMCUST"].ToString(), dt.Rows[i]["ESMYYHD"].ToString(),
                                                    dt.Rows[i]["ESMCDAC"].ToString(), dt.Rows[i]["ESMCDNM"].ToString(),
                                                    dt.Rows[i]["ESMPL01"].ToString(), dt.Rows[i]["ESMPL02"].ToString(),
                                                    dt.Rows[i]["ESMPL03"].ToString(), dt.Rows[i]["ESMPL04"].ToString(),
                                                    dt.Rows[i]["ESMPL05"].ToString(), dt.Rows[i]["ESMPL06"].ToString(),
                                                    dt.Rows[i]["ESMPL07"].ToString(), dt.Rows[i]["ESMPL08"].ToString(),
                                                    dt.Rows[i]["ESMPL09"].ToString(), dt.Rows[i]["ESMPL10"].ToString(),
                                                    dt.Rows[i]["ESMPL11"].ToString(), dt.Rows[i]["ESMPL12"].ToString(),
                                                    dt.Rows[i]["ESMUS01"].ToString(), dt.Rows[i]["ESMUS02"].ToString(),
                                                    dt.Rows[i]["ESMUS03"].ToString(), dt.Rows[i]["ESMUS04"].ToString(),
                                                    dt.Rows[i]["ESMUS05"].ToString(), dt.Rows[i]["ESMUS06"].ToString(),
                                                    dt.Rows[i]["ESMUS07"].ToString(), dt.Rows[i]["ESMUS08"].ToString(),
                                                    dt.Rows[i]["ESMUS09"].ToString(), dt.Rows[i]["ESMUS10"].ToString(),
                                                    dt.Rows[i]["ESMUS11"].ToString(), dt.Rows[i]["ESMUS12"].ToString(),
                                                    dt.Rows[i]["ESMLVAC"].ToString(), dt.Rows[i]["ESMTAG1"].ToString());
                            this.DbConnector.ExecuteTranQueryList();

                        }
                    }
                } 
                #endregion

                this.TXT01_ECSPLAN.Text = sOUTMSG;
            }

        }
        #endregion

        #region Description : 계정원장 확정(손익정보) (UP_TY_ECFBPMMF_UPT) -- 백만단위 정리 (그레인 자료 COPY 192.168.100.8 --> 192.168.100.2)
        private void UP_TY_ECFBPMMF_UPT()
        {
            string sOUTMSG = string.Empty;
            string sConnID = string.Empty;

            if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
            {
                sConnID = "TY_P_AC_39OAA839";
            }
            else
            {
                sConnID = "TY_P_AC_39N5B836";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sConnID,
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4),
                this.CBH01_ESBMCUST.GetValue(),
                "US",
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.TXT01_ECSRESU.Text = sOUTMSG;
            }
            else
            {
                if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
                {
                    // 기존 자료 전체삭제(년) ECFBPMMF
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39OAG841", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetValue().ToString().Substring(0,4));
                    this.DbConnector.ExecuteNonQuery();

                    // TYGT 그레인 자료 읽어서 192.168.100.8 --> 192.168.100.2(인더스트리 DB에 저장)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39OAH842", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4));
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_39OAH843",
                                                  dt.Rows[i]["ECFMCUST"].ToString(),
                                                  dt.Rows[i]["ECFMYYHD"].ToString(),
                                                  dt.Rows[i]["ECFMCDAC"].ToString(),
                                                  dt.Rows[i]["ECFMCDNM"].ToString(),
                                                  dt.Rows[i]["ECFMAM00"].ToString(),
                                                  dt.Rows[i]["ECFMAM01"].ToString(),
                                                  dt.Rows[i]["ECFMAM02"].ToString(),
                                                  dt.Rows[i]["ECFMAM03"].ToString(),
                                                  dt.Rows[i]["ECFMAM04"].ToString(),
                                                  dt.Rows[i]["ECFMAM05"].ToString(),
                                                  dt.Rows[i]["ECFMAM06"].ToString(),
                                                  dt.Rows[i]["ECFMAM07"].ToString(),
                                                  dt.Rows[i]["ECFMAM08"].ToString(),
                                                  dt.Rows[i]["ECFMAM09"].ToString(),
                                                  dt.Rows[i]["ECFMAM10"].ToString(),
                                                  dt.Rows[i]["ECFMAM11"].ToString(),
                                                  dt.Rows[i]["ECFMAM12"].ToString(),
                                                  dt.Rows[i]["ECFMTAG01"].ToString(),
                                                  dt.Rows[i]["ECFMLVA01"].ToString());
                            this.DbConnector.ExecuteTranQueryList();
                            //this.DbConnector.ExecuteNonQuery();
                        }
                    }
                }

                //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                this.TXT01_ECSRESU.Text = sOUTMSG;
            }

        }
        #endregion

        #region Description : 자금수지 확정 (UP_TY_EDAFFUNDMF_UPT)
        private void UP_TY_EDAFFUNDMF_UPT()
        {
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

            // 확정자료 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3A81K001", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQuery();

            // 미확정 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3A81I998", this.CBH01_ESBMCUST.GetValue(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
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
                            dIPTOTAL1 = dIPTOTAL1 + dEAFSAMM;
                            dIPTOTAL2 = dIPTOTAL2 + dEAFNEMM;
                        }

                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "3" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //지출
                        {
                            dCHTOTAL1 = dCHTOTAL1 + dEAFSAMM;
                            dCHTOTAL2 = dCHTOTAL2 + dEAFNEMM;
                        }

                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "4" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //차입금 상환
                        {
                            dCHAIP_IN1 = dCHAIP_IN1 + dEAFSAMM;
                            dCHAIP_IN2 = dCHAIP_IN1 + dEAFNEMM;
                        }

                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "5" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //차입금 증가
                        {
                            dCHAIP_UP1 = dCHAIP_UP1 + dEAFSAMM;
                            dCHAIP_UP2 = dCHAIP_UP2 + dEAFNEMM;
                        }

                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "6" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //증자
                        {
                            dUPFUND1 = dUPFUND1 + dEAFSAMM;
                            dUPFUND2 = dUPFUND2 + dEAFNEMM;
                        }

                    }
                    else //집계 row
                    {
                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "2" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "2")  //수입계
                        {
                            dLastEAFSAMM = dIPTOTAL1;
                            dLastEAFNEMM = dIPTOTAL2;
                        }

                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "3" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "2")  //지출계
                        {
                            dLastEAFSAMM = dCHTOTAL1;
                            dLastEAFNEMM = dCHTOTAL2;
                        }
                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "4" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "1")  //자금과부족
                        {
                            dLastEAFSAMM = dJUNWOLTOTAL1 + dIPTOTAL1 - dCHTOTAL1;
                            dLastEAFNEMM = dJUNWOLTOTAL2 + dIPTOTAL2 - dCHTOTAL2;
                        }

                        if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "9" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "1")  //차월이월금
                        {
                            dLastEAFSAMM = (dJUNWOLTOTAL1 + dIPTOTAL1 - dCHTOTAL1) - dCHAIP_IN1 + dCHAIP_UP1 + dUPFUND1;
                            dLastEAFNEMM = (dJUNWOLTOTAL2 + dIPTOTAL2 - dCHTOTAL2) - dCHAIP_IN2 + dCHAIP_UP2 + dUPFUND2;
                        }
                    }

                    datas.Add(new object[] {dt.Rows[i]["EAFSUBGN"].ToString(),  //1
                                            dt.Rows[i]["EAFYYMM"].ToString(),  //2
                                            dt.Rows[i]["EAFSEQN"].ToString(),  //3
                                            dt.Rows[i]["EAFTINM"].ToString(),  //4
                                            dt.Rows[i]["EAFLEVE"].ToString(),  //5
                                            dLastEAFSAMM.ToString(),  //6
                                            dLastEAFNEMM.ToString(),  //7
                                            TYUserInfo.EmpNo.ToString()   //8
                                           });
                }

                // 저장
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

        }
        #endregion


        #region Description : EIS 마감 년월 및 각종 자료 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //
            #region Description : 마감 확정 체크
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
            #endregion
            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            int iCnt = 0;

            string s이슈정보 = this.CKB01_ECSISSU.GetValue().ToString(); // s이슈정보
            string s시설정보 = this.CKB01_ECSFACI.GetValue().ToString(); // s시설정보
            string s인사정보 = this.CKB01_ECSINSA.GetValue().ToString(); // s인사정보
            string s매출정보 = this.CKB01_ECSSALE.GetValue().ToString(); // s매출정보
            string s계획정보 = this.CKB01_ECSPLAN.GetValue().ToString(); // s계획정보
            string s손익정보 = this.CKB01_ECSRESU.GetValue().ToString(); // s손익정보
            string s차입정보 = this.CKB01_ECSLOAN.GetValue().ToString(); // s차입금정보
            string s자금수지 = this.CKB01_ECSCASH.GetValue().ToString(); // s자금수지 (미확정자료: EDAFFUNDMF 존재 확인후 --> 확정생성: EFAFFUNDMF)

            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();

            // 생성일때 전월 자료 존재 유무 확인
            if (s전체처리구분 != "D")
            {
                #region Description : EIS 이슈정보 생성 존재 체크
                if (s이슈정보 == "A")
                {
                    //이슈정보 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //ESISSUEF (년월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3BBCD242", this.DTP01_GSTYYMM.GetValue().ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSISSU.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 자료가 없습니다! ";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
                #endregion


                #region Description : EIS 주요시설 생성 존재 체크
                if (s시설정보 == "A")
                {
                    //주요시설 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EDINSTALLF (년월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B14N168", this.DTP01_GSTYYMM.GetValue().ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSFACI.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 자료가 없습니다! ";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
                #endregion

                #region Description : EIS 인사정보 생성 존재 체크
                if (s인사정보 == "A")
                {
                    //인원현황 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EIORGEMPMF (년월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B74K226", this.DTP01_GSTYYMM.GetValue().ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSINSA.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 인원현황 자료가 없습니다!";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }

                    //주주현황 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EDSTHOLDLISTF (년월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B15A170", this.DTP01_GSTYYMM.GetValue().ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSINSA.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 주주현황 자료가 없습니다!";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }

                    //임원현황 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EDOFFICERLISTF (년월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B15B171", this.DTP01_GSTYYMM.GetValue().ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSINSA.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 임원현황 자료가 없습니다! ";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }

                    //임원겸직 및 경력사항 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EDOFFICERHOLDF (년월 , 계열사 구분)
                    this.DbConnector.Attach("TY_P_AC_3B15C172", this.DTP01_GSTYYMM.GetValue().ToString(), this.CBH01_ESBMCUST.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSINSA.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 임원겸직 및 경력사항 자료가 없습니다!";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
                #endregion

                #region Description : EIS 주요매출처 생성 존재 체크
                if (s매출정보 == "A")
                {
                    // 주요 매출처 존재 체크 (각 계열사 마다 주요매출처 정보가 틀려 별도로 체크 )

                    // 1. 태영호라이즌
                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TH")
                    {
                        iCnt = 0;
                        this.DbConnector.CommandClear(); //EDSBTHMF (계열사 구분 , 년월)
                        this.DbConnector.Attach("TY_P_AC_3B74Q227", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString());
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt == 0)
                        {
                            this.TXT01_ECSSALE.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 주요매출처 자료가 없습니다!";

                            this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                            e.Successed = false;
                            return;
                        }
                    }

                    // 2. 태영GLS
                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TS")
                    {
                        iCnt = 0;
                        this.DbConnector.CommandClear(); //EDSBTSMF (계열사 구분 , 년월)
                        this.DbConnector.Attach("TY_P_AC_3B74R228", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString());
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt == 0)
                        {
                            this.TXT01_ECSSALE.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 주요매출처 자료가 없습니다!";

                            this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                            e.Successed = false;
                            return;
                        }
                    }

                    // 3. 태영그레인터미널
                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TG")
                    {
                        iCnt = 0;
                        this.DbConnector.CommandClear(); //ESCUSTMF (계열사 구분 , 년월)
                        this.DbConnector.Attach("TY_P_AC_3B74S229", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString());
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt == 0)
                        {
                            this.TXT01_ECSSALE.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 주요매출처 자료가 없습니다!";

                            this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                            e.Successed = false;
                            return;
                        }
                    }

                    // 4. 티와이스틸
                    if (this.CBH01_ESBMCUST.GetValue().ToString() == "TL")
                    {
                        iCnt = 0;
                        this.DbConnector.CommandClear(); //EDSBTLMF (계열사 구분 , 년월)
                        this.DbConnector.Attach("TY_P_AC_3B74T230", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString());
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt == 0)
                        {
                            this.TXT01_ECSSALE.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 주요매출처 자료가 없습니다!";

                            this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                            e.Successed = false;
                            return;
                        }
                    }
                }
                #endregion



                #region Description : EIS 차입금 생성 존재 체크
                if (s차입정보 == "A")
                {
                    //차입금 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EMSLOANMF (계열사 구분 , 년월 )
                    this.DbConnector.Attach("TY_P_AC_3B755231", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSLOAN.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 차입금 자료가 없습니다! ";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
                #endregion

                #region Description : EIS 미확정 자금수지,자금수지계획 존재 체크
                if (s자금수지 == "A")
                {
                    //미확정 자금수지 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EDAFFUNDMF (계열사 구분 , 년월 )
                    this.DbConnector.Attach("TY_P_AC_3B751232", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSCASH.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 미확정 자금수지 자료가 없습니다! ";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }

                    // 자금수지계획 존재 체크
                    iCnt = 0;
                    this.DbConnector.CommandClear(); //EFPLFUNDMF (계열사 구분 , 년월 )
                    this.DbConnector.Attach("TY_P_AC_3BB3V248", this.CBH01_ESBMCUST.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt == 0)
                    {
                        this.TXT01_ECSCASH.Text = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + " 년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + " 월 자금수지 계획 자료가 없습니다! ";

                        this.ShowMessage("TY_M_AC_2422N250"); // 자료가 존재하지 않습니다... (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
                #endregion

            }

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
                this.CKB01_ECSFACI.Visible = false; // 시설정보
            }
            else
            {
                this.CKB01_ECSISSU.Visible = true;
                this.CKB01_ECSFACI.Visible = true;
                this.CKB01_ECSINSA.Visible = true;
                this.CKB01_ECSSALE.Visible = true;
                this.CKB01_ECSPLAN.Visible = true;
                this.CKB01_ECSRESU.Visible = true;
                this.CKB01_ECSLOAN.Visible = true;
                this.CKB01_ECSCASH.Visible = true;
            }

        } 
        #endregion


    }
}
