using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 자료생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.06.18 15:39
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK 확인
    ///  TY_P_AC_26J10935 : EIS 인원 저장
    ///  TY_P_AC_26I45922 : EIS 생성 손익
    ///  TY_P_AC_26I4G925 : EIS 생성 인건비
    ///  TY_P_AC_26I4H926 : EIS 생성 인원(오라클)
    ///  TY_P_AC_27NBL193 : EIS 생성 자금수지(전월)
    ///  TY_P_AC_27NBS201 : EIS 생성 차입금 세부내역(전월)
    ///  TY_P_AC_26R2N943 : EIS 생성 통제성경비
    ///  TY_P_AC_27OAS249 : EIS 생성 투하자산 받을어음
    ///  TY_P_AC_27OBJ260 : EIS 생성 투하자산 부실채권
    ///  TY_P_AC_27OAQ248 : EIS 생성 투하자산 외상매출금
    ///  TY_P_AC_27N4T210 : EIS 생성 재고자산 주요품목(전월)
    ///  TY_P_AC_27A5U001 : EIS 생성 차입금
    ///  TY_P_AC_26KBZ939 : EIS 생성 재무상태표
    ///  TY_P_AC_28A9D358 : EIS 주요품목생성전 CHECK  확인
    ///  TY_P_AC_28A9P360 : EIS 생성 무역 재고자산(오라클)
    ///  TY_P_AC_28AAJ361 : EIS 주요품목생성전 품목CHECK 확인
    ///  TY_P_AC_28A15369 : EIS 생성 재고자산
    ///  
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    ///  TY_M_AC_28A9J359 : 해당월에 재고자산품목 자료가 없습니다. 생성후 처리 하세요.
    ///  TY_M_AC_28AAV362 : 무역 재고마감 자료가 없습니다
    ///  
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GSTYYMM : 시작년월
    ///  EPBOGB : 인건비생성
    ///  EPBOGY : 년 계획생성
    ///  EPLCGB : 손익생성
    ///  EPBSGB : 재무상태표 생성
    ///  EPCTGB : 통제성 경비 생성
    ///  EPCTGY : 통재성 경비 년 생성
    ///  EPLOGB : 차입금 경비 생성
    ///  EPASGB : 재고자산 주요품목 생성
    ///  EPFUGB : 자금수지 생성 (전월)
    ///  EPLNGB : 차입금 세부 생성 (전월)
    ///  EPCAGB : 외상매출금 생성
    ///  EPCBGB : 받을어음 생성
    ///  EPCNGB : 부실채권 생성
    ///  EPACGB : 재고자산생성
    ///  GOKCR : 생성구분 

    /// 
    /// </summary>
    public partial class TYACPC001B : TYBase
    {
        #region Description : 페이지 로드
        public TYACPC001B()
        {
            InitializeComponent();
        }

        private void TYACPC001B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTYYMM.Focus();

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
        } 
        #endregion


        #region  Description : 배치 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {

            int iCCNT = 0;
            string sOUTMSG = string.Empty;
            string sELMYYMM = string.Empty;
            string sELMCNITT = string.Empty;
            string sELMCNETT = string.Empty;
            string sELMCNISS = string.Empty;
            string sELMCNESS = string.Empty;
            string sELMCNIO1 = string.Empty;
            string sELMCNEO1 = string.Empty;
            string sELMCNIO2 = string.Empty;
            string sELMCNEO2 = string.Empty;
            string sELMCNIAA = string.Empty;
            string sELMCNEAA = string.Empty;
            string sELMCNICC = string.Empty;
            string sELMCNECC = string.Empty;
            string sELMCNIGG = string.Empty;
            string sELMCNEGG = string.Empty;
            string sELMCNITO = string.Empty;
            string sELMCNETO = string.Empty;

            string s재고자산품목 = this.CKB01_EPASGB.GetValue().ToString();
            string s자금수지 = this.CKB01_EPFUGB.GetValue().ToString();
            string s차입금세부 = this.CKB01_EPLNGB.GetValue().ToString();

            string s손익처리구분 = this.CKB01_EPLCGB.GetValue().ToString();
            string s인건비처리 = this.CKB01_EPBOGB.GetValue().ToString();
            string s인건비년처리 = this.CKB01_EPBOGY.GetValue().ToString();
            string s재무재표처리 = this.CKB01_EPBSGB.GetValue().ToString();

            string s외상매출금처리 = this.CKB01_EPCAGB.GetValue().ToString(); // 투하자산
            string s받을어음처리 = this.CKB01_EPCBGB.GetValue().ToString();   // 투하자산
            string s부실채권처리 = this.CKB01_EPCNGB.GetValue().ToString();   // 투하자산

            string s통제경비년처리 = this.CKB01_EPCTGY.GetValue().ToString();
            string s통제성경비처리 = this.CKB01_EPCTGB.GetValue().ToString();
            string s차입금처리 = this.CKB01_EPLOGB.GetValue().ToString();

            string s재고자산생성 = this.CKB01_EPACGB.GetValue().ToString();

            string s투자현황  = this.CKB01_EPFDGB.GetValue().ToString();
            string s채권현황  = this.CKB01_EPCGGB.GetValue().ToString();
            
            string s유형자산현황 = this.CKB01_EPJSGB.GetValue().ToString();

            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();

            this.TXT01_TEPASGB.Text = "";
            this.TXT01_TEPFUGB.Text = "";
            this.TXT01_TEPLNGB.Text = "";
            this.TXT01_TEPLCGB.Text = "";
            this.TXT01_TEPBOGB.Text = "";
            this.TXT01_TEPBSGB.Text = "";
            this.TXT01_TEPCAGB.Text = "";
            this.TXT01_TEPCBGB.Text = "";
            this.TXT01_TEPCNGB.Text = "";
            this.TXT01_TEPCTGB.Text = "";
            this.TXT01_TEPLOGB.Text = "";
            this.TXT01_TEPACGB.Text = "";
            this.TXT01_EPFDGB.Text = "";
            this.TXT01_EPCGGB.Text = "";
            this.TXT01_EPJSGB.Text = "";

            s자금수지 = ""; // 수동등록
            s차입금세부 = ""; // 사용안함
            s재무재표처리 = ""; // 수동등록
            s인건비년처리 = "";  // 년초 생성(1월)
            //이거 왜 막아놨을까?????
            //s통제성경비처리 = ""; // 년초 생성(1월)


            if (s재고자산품목 == "A") { iCCNT = iCCNT + 1; };
            if (s자금수지 == "A") { iCCNT = iCCNT + 1; };
            if (s차입금세부 == "A") { iCCNT = iCCNT + 1; };
            if (s손익처리구분 == "A") { iCCNT = iCCNT + 1; };
            if (s인건비처리 == "A") { iCCNT = iCCNT + 1; };
            if (s재무재표처리 == "A") { iCCNT = iCCNT + 1; };
            if (s받을어음처리 == "A") { iCCNT = iCCNT + 1; };
            if (s부실채권처리 == "A") { iCCNT = iCCNT + 1; };
            if (s외상매출금처리 == "A") { iCCNT = iCCNT + 1; };
            if (s통제경비년처리 == "A") { iCCNT = iCCNT + 1; };
            if (s통제성경비처리 == "A") { iCCNT = iCCNT + 1; };
            if (s차입금처리 == "A") { iCCNT = iCCNT + 1; };
            if (s재고자산생성 == "A") { iCCNT = iCCNT + 1; };

            if (s투자현황 == "A") { iCCNT = iCCNT + 1; };
            if (s채권현황 == "A") { iCCNT = iCCNT + 1; };
            if (s유형자산현황 == "A") { iCCNT = iCCNT + 1; };
            

            #region Description : EIS 재고자산 주요품목 생성(전월)
            if (s재고자산품목 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27N4T210",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPASGB.Text = sOUTMSG;

            }
            #endregion

            #region Description : EIS 생성 자금수지 생성(전월)
            if (s자금수지 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27NBL193",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPFUGB.Text = sOUTMSG; 

            }
            #endregion

            #region Description : EIS 생성 차입금 세부내역 생성(전월)
            if (s차입금세부 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27NBS201",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPLNGB.Text = sOUTMSG; 

            }
            #endregion


            #region Description : EIS 손익자료 생성 처리
            if (s손익처리구분 == "A")
            {
                string sProcedureid = string.Empty;

                // 2014년 이전 TY_P_AC_26I45922
                // 2014 TY_P_AC_427BX255

                if ( Convert.ToDouble(this.DTP01_GSTYYMM.GetValue()) <= 201312 )
                {
                    sProcedureid = "TY_P_AC_26I45922";
                }
                else
                {
                    sProcedureid = "TY_P_AC_427BX255";
                }
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    sProcedureid,
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPLCGB.Text = sOUTMSG;

            } 
            #endregion

            #region Description : EIS 인건비 생성 처리
            if (s인건비처리 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_26I4G925",
                    s전체처리구분,
                    s인건비년처리,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.TXT01_TEPBOGB.Text = sOUTMSG;

                }
                else
                {
                    if (s전체처리구분 == "A")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_26I4H926");
                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count != 0)
                        {
                            sELMCNITT = dt.Rows[0]["WK_IAMTT"].ToString();
                            sELMCNETT = dt.Rows[0]["WK_EAMTT"].ToString();
                            sELMCNISS = dt.Rows[0]["WK_IAMSS"].ToString();
                            sELMCNESS = dt.Rows[0]["WK_EAMSS"].ToString();
                            sELMCNIO1 = dt.Rows[0]["WK_IAMO1"].ToString();
                            sELMCNEO1 = dt.Rows[0]["WK_EAMO1"].ToString();
                            sELMCNIO2 = dt.Rows[0]["WK_IAMO2"].ToString();
                            sELMCNEO2 = dt.Rows[0]["WK_EAMO2"].ToString();
                            sELMCNIAA = dt.Rows[0]["WK_IAMAA"].ToString();
                            sELMCNEAA = dt.Rows[0]["WK_EAMAA"].ToString();
                            sELMCNICC = dt.Rows[0]["WK_IAMCC"].ToString();
                            sELMCNECC = dt.Rows[0]["WK_EAMCC"].ToString();
                            sELMCNIGG = dt.Rows[0]["WK_IAMGG"].ToString();
                            sELMCNEGG = dt.Rows[0]["WK_EAMGG"].ToString();
                            sELMCNITO = dt.Rows[0]["WK_IAMTO"].ToString();
                            sELMCNETO = dt.Rows[0]["WK_EAMTO"].ToString();

                            sELMCNIAA = Convert.ToString(Convert.ToDouble(sELMCNIAA) + Convert.ToDouble(sELMCNICC) + Convert.ToDouble(sELMCNIGG));
                            sELMCNEAA = Convert.ToString(Convert.ToDouble(sELMCNEAA) + Convert.ToDouble(sELMCNECC) + Convert.ToDouble(sELMCNEGG));
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_26J10935", sELMCNITT, sELMCNETT, sELMCNISS, sELMCNESS,
                                                sELMCNIO1, sELMCNEO1, sELMCNIO2, sELMCNEO2, sELMCNIAA, sELMCNEAA, sELMCNITO, sELMCNETO,
                                                this.DTP01_GSTYYMM.GetValue()); // 저장
                        this.DbConnector.ExecuteNonQuery();
                    }

                    this.TXT01_TEPBOGB.Text = sOUTMSG;

                }
            } 
            #endregion

            #region Description : EIS 재무재표 생성 처리
            if (s재무재표처리 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_26KBZ939",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPBSGB.Text = sOUTMSG;

            } 
            #endregion

            #region Description : EIS 생성 외상매출금
            if (s외상매출금처리 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27OAQ248",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPCAGB.Text = sOUTMSG;

            }
            #endregion

            #region Description : EIS 생성 받을어음
            if (s받을어음처리 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27OAS249",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPCBGB.Text = sOUTMSG;

            }
            #endregion

            #region Description : EIS 생성 부실채권
            if (s부실채권처리 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27OBJ260",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPCNGB.Text = sOUTMSG;

            }
            #endregion

            #region Description : EIS 통제성 경비 생성 처리
            if (s통제성경비처리 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_26R2N943",
                    s전체처리구분,
                    s통제경비년처리,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPCTGB.Text = sOUTMSG;

            } 
            #endregion

            #region Description : EIS 차입금 생성 처리
            if (s차입금처리 == "A")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27A5U001",
                    s전체처리구분,
                    this.DTP01_GSTYYMM.GetValue(),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TEPLOGB.Text = sOUTMSG;

            } 
            #endregion

            #region Description : EIS 재고자산 생성 처리

            string sSJGPUMMOK = string.Empty;
            string sFILENUM = string.Empty;
            string sPJMCJQTY3 = string.Empty;
            string sPJMCJAMT12 = string.Empty;
            string sPJSUJQTY5 = string.Empty;
            string sPJSUJAMT14 = string.Empty;
            string sPDBMJQTY4 = string.Empty;
            string sPDMBAAMT13 = string.Empty;
            string sPDMAEQTY7 = string.Empty;
            string sPDMAWAMT8 = string.Empty;
            string sPDMCJQTY9 = string.Empty;
            string sPDMCJAMT18 = string.Empty;
            string sPDSUIQTY10 = string.Empty;
            string sPDSUIPAMT19 = string.Empty;

            if (s재고자산생성 == "A")
            {
                // 기존데이타 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_31841534", this.DTP01_GSTYYMM.GetValue().ToString());
                this.DbConnector.ExecuteScalar();

                // 무역 재고자산을 읽어 옮(오라클)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_28A9P360", this.DTP01_GSTYYMM.GetValue().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sSJGPUMMOK = dt.Rows[i]["JGPUMMOK"].ToString();
                        sFILENUM = dt.Rows[i]["FILENUM"].ToString().Replace("A50300","B80100");
                        sPJMCJQTY3 = dt.Rows[i]["PJMCJQTY3"].ToString();
                        sPJMCJAMT12 = dt.Rows[i]["PJMCJAMT12"].ToString();
                        sPJSUJQTY5 = dt.Rows[i]["PJSUJQTY5"].ToString();
                        sPJSUJAMT14 = dt.Rows[i]["PJSUJAMT14"].ToString();
                        sPDBMJQTY4 = dt.Rows[i]["PDBMJQTY4"].ToString();
                        sPDMBAAMT13 = dt.Rows[i]["PDMBAAMT13"].ToString();
                        sPDMAEQTY7 = dt.Rows[i]["PDMAEQTY7"].ToString();
                        sPDMAWAMT8 = dt.Rows[i]["PDMAWAMT8"].ToString();
                        sPDMCJQTY9 = dt.Rows[i]["PDMCJQTY9"].ToString();
                        sPDMCJAMT18 = dt.Rows[i]["PDMCJAMT18"].ToString();
                        sPDSUIQTY10 = dt.Rows[i]["PDSUIQTY10"].ToString();
                        sPDSUIPAMT19 = dt.Rows[i]["PDSUIPAMT19"].ToString();

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_28A15369", s전체처리구분, this.DTP01_GSTYYMM.GetValue(),
                                                    sSJGPUMMOK,
                                                    sFILENUM,
                                                    sPJMCJQTY3,
                                                    sPJMCJAMT12,
                                                    sPJSUJQTY5,
                                                    sPJSUJAMT14,
                                                    sPDBMJQTY4,
                                                    sPDMBAAMT13,
                                                    sPDMAEQTY7,
                                                    sPDMAWAMT8,
                                                    sPDMCJQTY9,
                                                    sPDMCJAMT18,
                                                    sPDSUIQTY10,
                                                    sPDSUIPAMT19,
                                                    sOUTMSG.ToString());

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                        if (sOUTMSG.Substring(0, 1) == "E")
                        {
                            break;
                        }
                    }

                    this.TXT01_TEPACGB.Text = sOUTMSG;

                }

            }
            #endregion

            #region Description : EIS 투자현황 생성 처리
            if (s투자현황 == "A")
            {
                //전월구하기
                DateTime dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-" + "01");
                dt = dt.AddMonths(-1);
                string sJunWol = Convert.ToString(dt.Year) + Set_Fill2(Convert.ToString(dt.Month)); 

                this.DbConnector.CommandClear();
                //삭제
                this.DbConnector.Attach("TY_P_AC_39C95688", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

                //복사
                this.DbConnector.Attach("TY_P_AC_39C9C689", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sJunWol);

                this.DbConnector.ExecuteTranQueryList();

                this.TXT01_EPFDGB.Text = "OK-정상처리 되었습니다";
            }
            #endregion

            #region Description : EIS 채권현황 생성 처리
            if (s채권현황 == "A")
            {
                this.DbConnector.CommandClear();
                //삭제
                this.DbConnector.Attach("TY_P_AC_39R1P889", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                this.DbConnector.Attach("TY_P_AC_39R1Q890", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

                //생성
                //일반채권
                this.DbConnector.Attach("TY_P_AC_39R1G886", TYUserInfo.EmpNo, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                //장기채권
                this.DbConnector.Attach("TY_P_AC_3C93K661", TYUserInfo.EmpNo, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

                this.DbConnector.Attach("TY_P_AC_39R1N888", TYUserInfo.EmpNo, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

                this.DbConnector.ExecuteTranQueryList();

                this.TXT01_EPCGGB.Text = "OK-정상처리 되었습니다";
            }
            #endregion

            #region Description : EIS 유형자산 생성 처리
            if (s유형자산현황 == "A")
            {
                //전년도말
                DateTime dtm =  Convert.ToDateTime(Set_Date(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6) + "01"));
                string sPreYYMM = dtm.AddYears(-1).Year.ToString() + "12";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_9BBAQ498", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_9B8EV489", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sPreYYMM);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_AC_9B8ET488", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                                                    dt.Rows[i]["YD_SAUP"].ToString()+"00000",
                                                                    "1",
                                                                    dt.Rows[i]["YD_CODE"].ToString(),
                                                                    dt.Rows[i]["YP_AMMALAMOUNT"].ToString(),
                                                                    dt.Rows[i]["YP_REPJANAMOUNT"].ToString(),
                                                                    dt.Rows[i]["YD_AMMALAMOUNT"].ToString(),
                                                                    dt.Rows[i]["YD_REPJANAMOUNT"].ToString(),
                                                                    dt.Rows[i]["AMMALAMOUNT_GAP"].ToString(),
                                                                    dt.Rows[i]["REPJANAMOUNT_GAP"].ToString() 
                                                                        );
                    }
                    if (this.DbConnector.CommandCount > 0)
                    {
                        this.DbConnector.ExecuteTranQueryList();
                    }
                }                

                this.TXT01_EPJSGB.Text = "OK-정상처리 되었습니다";
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

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region Description : 처리 CHECK 
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 마감 완료 CHECK 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27H64059", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            ////  EIS 주요품목생성 전 CHECK  확인 
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_28A9D358", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            //DataTable dt2 = this.DbConnector.ExecuteDataTable();
            //if (dt2.Rows[0]["CNT"].ToString() == "0")
            //{
            //    this.ShowMessage("TY_M_AC_28A9J359"); // 해당월에 재고자산품목 자료가 없습니다. 생성후 처리 하세요.
            //    e.Successed = false;
            //    return;
            //}

            ////  EIS 주요품목생성전 품목CHECK  확인 (무역재고 생성화일 체크 병행) 
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_28A9P360", this.DTP01_GSTYYMM.GetValue().ToString());
            //DataTable dt3 = this.DbConnector.ExecuteDataTable();

            //string sJGPUMMOK = string.Empty;
            //string sOUTMSG = string.Empty;

            //if (dt3.Rows.Count != 0)
            //{
            //    for (int i = 0; i < dt3.Rows.Count; i++)
            //    {
            //        sJGPUMMOK = dt3.Rows[0]["JGPUMMOK"].ToString();

            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_AC_28AAJ361", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2), sJGPUMMOK); // 체크
            //        DataTable dt4 = this.DbConnector.ExecuteDataTable();

            //        if (dt4.Rows.Count != 0)
            //        {
            //            sOUTMSG = sJGPUMMOK + " 해당월에 재고자산폼목 미존재 합니다 " ;
            //            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //            e.Successed = false;
            //            return;
            //        }
            //    }
            //}
            //else
            //{
            //    this.ShowMessage("TY_M_AC_28AAV362"); // 무역 재고마감 자료가 없습니다.
            //    e.Successed = false;
            //    return;
            //}
        }
        #endregion

    }
}
