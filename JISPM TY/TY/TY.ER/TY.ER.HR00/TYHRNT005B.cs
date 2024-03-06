using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 전산신고관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.01.16 14:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  KBGUNMU : 근무처
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT005B : TYBase
    {
        private string fsSAUPNO = string.Empty;
        private string fsSANGHO = string.Empty;
        private string fsDPMK = string.Empty;
        private string fsDPMKNAME = string.Empty;
        private string fsDPTEL = string.Empty;
        private string fsHOMETAXID = string.Empty;
        private string fsCEONAME = string.Empty;

        private int fiRowCnt = 0;

        private bool fbReCordCEx = false;  //C 레코드 존재 유무

        #region  Description : 폼 로드 이벤트
        public TYHRNT005B()
        {
            InitializeComponent();
        }

        private void TYHRNT005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.TXT01_SDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int iRecordCRow = 0;
            int iRecordDRow = 0;

            fiRowCnt = 0;

            //본지점 선택
            if (CBO01_KBGUNMU.GetValue().ToString() != "1")
            {
                fsSAUPNO = "1058516181";
                fsSANGHO = "(주)태영인더스트리서울지점";
                 fsDPMK = "관리팀";
                 fsDPMKNAME = "이성재";
                 fsDPTEL = "02-2090-2619";
                 fsHOMETAXID = "TYC2922";
                 fsCEONAME = "정세진";
                 
            }
            else
            {
                fsSAUPNO = "6108110449";
                fsSANGHO = "(주)태영인더스트리";
                fsDPMK = "관리팀";
                fsDPMKNAME = "조영래";
                fsDPTEL = "052-228-3311";
                fsHOMETAXID = "TYC2921";
                fsCEONAME = "정세진";
            }

            //파일 생성 경로
            string sFilePath = "C:\\Temp\\";
            string sFileName = "";

            if (CBO01_INQOPTION.GetValue().ToString() == "1")  //지급명세서
            {
                sFileName = CBO01_KBGUNMU.GetValue().ToString() != "1" ? "C1058516.181":"C6108110.449";
            }
            else if (CBO01_INQOPTION.GetValue().ToString() == "2")  //의료비지급명세서
            {
                sFileName = CBO01_KBGUNMU.GetValue().ToString() != "1" ? "CA1058516.181" : "CA6108110.449";
            }

            if( File.Exists(sFilePath+sFileName) )
            {
                File.Delete(sFilePath+sFileName);
            }

            //StreamWriter StrWrReCode = File.AppendText(sFilePath+sFileName);

            StreamWriter StrWrReCode = new StreamWriter(sFilePath + sFileName, false, Encoding.Default);

            if (CBO01_INQOPTION.GetValue().ToString() == "1")  //지급명세서
            {
                this.UP_NTS_RecordA(StrWrReCode);

                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_81MH6506", "TY", TXT01_SDATE.GetValue().ToString(), "", CBO01_KBGUNMU.GetValue().ToString());
                this.DbConnector.Attach("TY_P_HR_81MH6506", "TY", TXT01_SDATE.GetValue().ToString(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {                    

                    iRecordCRow = dt.Rows.Count;

                    //종전근무지 레코드 수 
                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_HR_82M9Q623", "TY", TXT01_SDATE.GetValue().ToString(), CBO01_KBGUNMU.GetValue().ToString());
                    this.DbConnector.Attach("TY_P_HR_82M9Q623", "TY", TXT01_SDATE.GetValue().ToString());
                    iRecordDRow = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    this.UP_NTS_RecordB(dt, iRecordCRow, iRecordDRow, StrWrReCode);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UP_NTS_RecordC(dt.Rows[i]["ADCOMPANY"].ToString(), dt.Rows[i]["ADYEAR"].ToString(), dt.Rows[i]["ADSABUN"].ToString(), i + 1, StrWrReCode);

                        if (fbReCordCEx)
                        {
                            //종전근무지
                            UP_NTS_RecordD(dt.Rows[i]["ADCOMPANY"].ToString(), dt.Rows[i]["ADYEAR"].ToString(), dt.Rows[i]["ADSABUN"].ToString(), i + 1, StrWrReCode);                            
                            //소득공제명세(부양가족)
                            UP_NTS_RecordE(dt.Rows[i]["ADCOMPANY"].ToString(), dt.Rows[i]["ADYEAR"].ToString(), dt.Rows[i]["ADSABUN"].ToString(), i + 1, StrWrReCode);
                            //연금.저축등 소득.세액 공제명세
                            UP_NTS_RecordF(dt.Rows[i]["ADCOMPANY"].ToString(), dt.Rows[i]["ADYEAR"].ToString(), dt.Rows[i]["ADSABUN"].ToString(), i + 1, StrWrReCode);
                            //월세액
                            UP_NTS_RecordG(dt.Rows[i]["ADCOMPANY"].ToString(), dt.Rows[i]["ADYEAR"].ToString(), dt.Rows[i]["ADSABUN"].ToString(), i + 1, StrWrReCode);
                            //기부금조정명세
                            UP_NTS_RecordH(dt.Rows[i]["ADCOMPANY"].ToString(), dt.Rows[i]["ADYEAR"].ToString(), dt.Rows[i]["ADSABUN"].ToString(), i + 1, StrWrReCode);
                            //해당년도 기부금 명세서
                            UP_NTS_RecordI(dt.Rows[i]["ADCOMPANY"].ToString(), dt.Rows[i]["ADYEAR"].ToString(), dt.Rows[i]["ADSABUN"].ToString(), i + 1, StrWrReCode);
                        }
                    }
                }                
            }           
            else if (CBO01_INQOPTION.GetValue().ToString() == "2")  //의료비명세서
            {
                this.UP_NTS_MedRecordA("TY", TXT01_SDATE.GetValue().ToString(), StrWrReCode);
            }

            StrWrReCode.Close();

            this.ShowMessage("TY_M_GB_26E30875");

        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 연말정산 A 레코드 생성
        private void UP_NTS_RecordA(StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;

            //A1 레코드구분 X(1)
            sStrRecord = "A";
            //A2 자료구분 9(2)
            sStrRecord = sStrRecord + "20";
            //A3 세무서코드 X(3))
            sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
            //A4 제출연월일 9(8)
            sStrRecord = sStrRecord + DTP01_EDATE.GetString().ToString();
            //A5 제출자구분 9(1) 1-세무대리인 2-법인 3-개인
            sStrRecord = sStrRecord + "2";
            //A6 세무대리인관리번호 X(6)
            sStrRecord = sStrRecord + "      ";
            //A7 홈택스ID X(20)
            sStrRecord = sStrRecord + string.Format("{0,-20:G}", fsHOMETAXID);
            //A8 세무프로그램코드 X(4)
            sStrRecord = sStrRecord + "9000";
            //A9 사업자번호 X(10)
            sStrRecord = sStrRecord + fsSAUPNO;
            //A10 법인명 X(60)
            sStrTemp = "";
            sStrTemp = fsSANGHO;
            sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(fsSANGHO)));
            sStrRecord = sStrRecord + sStrTemp;
            //A11 담당자부서 X(30)
            sStrTemp = "";
            sStrTemp = fsDPMK;
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(fsDPMK)));
            sStrRecord = sStrRecord + sStrTemp;

            //A12 담당자성명 X(30)
            sStrTemp = "";
            sStrTemp = fsDPMKNAME;
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(fsDPMKNAME)));
            sStrRecord = sStrRecord + sStrTemp;

            //A13 담당자 전화번호 X(15)
            sStrRecord = sStrRecord + string.Format("{0,-15:G}", fsDPTEL);

            //A14 귀속년도 X(4)
            sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString();

            //A15 신고의무자수 9(5)
            sStrRecord = sStrRecord + "00001";
            //A16 사용한한글코드 9(3)
            sStrRecord = sStrRecord + "101";
            //A17 레코드구분 X(1691)
            sStrRecord = sStrRecord + string.Format("{0,-1808:G}", "");

            StrWrReCode.WriteLine(sStrRecord);
        }
        #endregion

        #region  Description : 연말정산 B 레코드 생성
        private void UP_NTS_RecordB(DataTable dt, int iRecordCRow, int iRecordDRow, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;
            //B1 레코드구분 X(1)
            sStrRecord = "B";
            //B2 자료구분 9(2)
            sStrRecord = sStrRecord + "20";
            //B3 세무서코드 X(3))
            sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
            //B4 일련번호 9(6)
            sStrRecord = sStrRecord + "000001";

            //B5 사업자등록번호 X(10)
            sStrRecord = sStrRecord + fsSAUPNO;
            //B6 법인명 X(60)
            sStrTemp = "";
            sStrTemp = fsSANGHO;
            sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(fsSANGHO)));
            sStrRecord = sStrRecord + sStrTemp;

            //B7 대표자 X(30)
            sStrTemp = "";
            sStrTemp = fsCEONAME;
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(fsCEONAME)));
            sStrRecord = sStrRecord + sStrTemp;

            //B8 주민번호(법인등록번호) X(13)
            sStrRecord = sStrRecord + "1812110012745";

            //B9 귀속년도X(4)
            sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString();

            //B10 주근무처(C레코드)수 9(7)
            sStrRecord = sStrRecord + String.Format("{0:D7}", Convert.ToInt64(iRecordCRow));

            //B11 종전근무처(D레코드)수 9(7)
            sStrRecord = sStrRecord + String.Format("{0:D7}", Convert.ToInt64(iRecordDRow));

            //B12 총급여총계 9(14)           
            sStrRecord = sStrRecord + String.Format("{0:D14}", Convert.ToInt64(dt.Compute("SUM(WNTaxTarGetPay)", "").ToString()));
            
            //B13 결정세액(소득세)총계   9(13)
            sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Compute("SUM(WNFixTax)", "").ToString()));

            //B14 결정세액(주민세)총계   9(13)
            sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Compute("SUM(WNFixReSidenceTax)", "").ToString()));

            //B15 결정세액(농특세)총계   9(13)
            sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Compute("SUM(WNFixVillTax)", "").ToString()));

            //B16 결정세액 총계   9(13)
            sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Compute("SUM(WNFixTax)", "").ToString()) +
                                                               Convert.ToInt64(dt.Compute("SUM(WNFixReSidenceTax)", "").ToString()) +
                                                               Convert.ToInt64(dt.Compute("SUM(WNFixVillTax)", "").ToString()));

            //B17 제출대상기간코드 9(1)
            sStrRecord = sStrRecord + "1";

            //B18 공란 X(1683)
            sStrRecord = sStrRecord + string.Format("{0,-1800:G}", "");

            StrWrReCode.WriteLine(sStrRecord);

        }
        #endregion

        #region  Description : 연말정산 C 레코드 생성
        private void UP_NTS_RecordC(string sCompy, string sYEAR, string sKBSABUN, Int32 iRowCnt, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;
            Int64 iSpcTaxDedTotal = 0;

            fbReCordCEx = false;

            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_7CT9B380", sCompy, sYEAR, sKBSABUN, "", CBO01_KBGUNMU.GetValue().ToString());
            this.DbConnector.Attach("TY_P_HR_7CT9B380", sCompy, sYEAR, sKBSABUN, "", TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                fbReCordCEx = true;
                //C1 레코드 구분 X(1) 1 영문 대문자 “C” 영문 대문자 ‘C’ 아니면 오류
                sStrRecord = "C";
                //C2 자료구분 9(2) 3 근로소득 - 숫자 “20” ‘20’이 아니면 오류
                sStrRecord = sStrRecord + "20";
                //C3 세무서코드 X(3) 6 원천징수의무자의 납세지관할 세무서코드 ※B레코드의 세무서코드와 항상 일치 [C3] ≠ [B3]이면 오류
                sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                //C4 일련번호 9(6) 12 원천징수의무자별로 1부터 순차 부여
                sStrRecord = sStrRecord + String.Format("{0:D6}", iRowCnt);
                //C5 ③사업자등록번호 X(10) 22 원천징수의무자의 사업자등록번호 ※B레코드의 사업자등록번호와 항상 일치 1.[C5] ≠ [B5]이면 오류 2.잘못된 사업자등록번호 입력 시 오류
                sStrRecord = sStrRecord + fsSAUPNO;
                //C6 종(전)근무처 수 9(2) 24 소득자(근로자)의 종(전)근무처 수
                if ( Convert.ToInt16(dt.Rows[0]["WKCOMCNT"].ToString().Trim()) > 0 )
                {
                    sStrRecord = sStrRecord + String.Format("{0:D2}", Convert.ToInt16(dt.Rows[0]["WKCOMCNT"].ToString().Trim()));
                }
                else
                {
                    sStrRecord = sStrRecord + "00";
                }
                //C7 거주자구분코드 9(1) 25 1:거주자, 2:비거주자 ‘1’, ‘2’가 아니면 오류
                sStrRecord = sStrRecord + "1";
                //C8 거주지국코드 X(2) 
                sStrRecord = sStrRecord + "KR";
                //C9 외국인단일세율 9(1)
                sStrRecord = sStrRecord + "2";
                //C10 외국법인소속파견근로자여부 1:여 2:부
                sStrRecord = sStrRecord + "2";
                //C11 ⑥성명 X(30) 59 소득자의 성명 기재되어 있지 않으면 오류
                sStrTemp = "";
                sStrTemp = dt.Rows[0]["KBHANGL"].ToString().Trim();
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(dt.Rows[0]["KBHANGL"].ToString().Trim())));
                sStrRecord = sStrRecord + sStrTemp;
                //C12 내·외국인 9(1)
                sStrRecord = sStrRecord + "1";
                //C13 ⑦주민등록번호 X(13)
                sStrRecord = sStrRecord + dt.Rows[0]["KBJUMIN"].ToString().Trim().Replace("-","");
                // C14 국적코드 X(2) 75
                sStrRecord = sStrRecord + "KR";
                // C15 세대주여부 X(1) 76
                sStrRecord = sStrRecord + "1";

                // C16 연말정산구분 X(1) 77
                sStrRecord = sStrRecord + dt.Rows[0]["WNGUBN"].ToString().Trim();

                // C17 사업자단위 과세자 여부
                sStrRecord = sStrRecord + "2";

                // C18 종사업장 일련번호
                sStrRecord = sStrRecord + string.Format("{0,-4:G}", "");

                // C19 종료관련종사자 여부( 1:여 2:부 )
                sStrRecord = sStrRecord + "2";

                //C20 ⑩주현근무처- 사업자등록번호X(10) 87
                sStrRecord = sStrRecord + fsSAUPNO;
                //C21 ⑨주현근무처- 근무처명X(60) 127
                sStrTemp = "";
                sStrTemp = fsSANGHO;
                sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(fsSANGHO)));
                sStrRecord = sStrRecord + sStrTemp;

                //C22 ⑪근무기간시작연월일9(8) 135
                sStrRecord = sStrRecord + dt.Rows[0]["WNSDATE"].ToString().Trim();

                //C23 ⑪근무기간 종료연월일9(8) 143
                sStrRecord = sStrRecord + dt.Rows[0]["WNEDATE"].ToString().Trim();

                //C24 ⑫감면기간 시작연월일 9(8) 151
                sStrRecord = sStrRecord + "00000000";

                //C25 ⑫감면기간 종료연월일 9(8) 159
                sStrRecord = sStrRecord + "00000000";
                //C26 ⑬급여 9(11) 170
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNPayMHap"].ToString().Trim()));
                //C27 ⑭상여 9(11) 181
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNPaySHap"].ToString().Trim()));
                //C28 ⑮인정상여 9(11) 192
                sStrRecord = sStrRecord + "00000000000";
                //C29 ⑮-1 주식매수선택권행사이익    9(11) 203
                sStrRecord = sStrRecord + "00000000000";
                //C30 ⑮-2 우리사주 조합인출금 9(11) 214
                sStrRecord = sStrRecord + "00000000000";
                //C31 ⑮-3 임원퇴직소득 금액한도초과액9(11) 225
                sStrRecord = sStrRecord + "00000000000";

                //C32 ⑮-4 직무발명보상금 9(11) 225
                sStrRecord = sStrRecord + "00000000000";

                // C33 공란 9(11) 246 
                sStrRecord = sStrRecord + "00000000000";

                // C34 공란 9(11) 246 
                sStrRecord = sStrRecord + "00000000000";

                // C35 계 9(11) 257
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNPayMHap"].ToString().Trim()) + Convert.ToInt64(dt.Rows[0]["WNPaySHap"].ToString().Trim()) );

                //【주(현)근무처 비과세소득 및 감면 소득】 서식항목 󰊉󰊚 ∼ 󰊊󰊒-1에 해당함

                //C36 󰊉󰊚-5 G01-비과세학자금   9(10) 267
                sStrRecord = sStrRecord + "0000000000";
                //C37 󰊉󰊚-9 H01-경호·승선수당 9(10) 287
                sStrRecord = sStrRecord + "0000000000";
                //C38 󰊉󰊚-4 H06-유아·초중등  9(10) 297
                sStrRecord = sStrRecord + "0000000000";
                //C39 󰊉󰊚-4 H07-고등교육법  9(10) 307
                sStrRecord = sStrRecord + "0000000000";
                //C40 󰊉󰊚-4 H08-특별법      9(10) 317
                sStrRecord = sStrRecord + "0000000000";
                //C41 󰊉󰊚-4 H09-연구기관 등 9(10) 327
                sStrRecord = sStrRecord + "0000000000";
                //C42 󰊉󰊚-4 H10- 기업부설연구소 9(10) 337
                sStrRecord = sStrRecord + "0000000000";

                //C43 󰊉󰊚-22 H14-보육 사 근무환경개선비9(10) 347
                sStrRecord = sStrRecord + "0000000000";
                //C44 󰊉󰊚-23 H15-사립 유치원수석교사·교사의 인건비9(10) 357
                sStrRecord = sStrRecord + "0000000000";
                //C45 󰊉󰊚-6 H11-취재수당  9(10) 367
                sStrRecord = sStrRecord + "0000000000";
                //C46 󰊉󰊚-7 H12-벽지수당  9(10) 377
                sStrRecord = sStrRecord + "0000000000";
                //C47 󰊉󰊚-8 H13-재해관련급여  9(10) 387
                sStrRecord = sStrRecord + "0000000000";

                //C48 󰊉󰊚-24 H16-정부·공공기관지방이전기관종사자 이주수당 9(10) 397
                sStrRecord = sStrRecord + "0000000000";
                //C49 󰊉󰊚-19 H17-종교활동비 9(10) 397
                sStrRecord = sStrRecord + "0000000000";
                //C50 󰊉󰊚-19 I01-외국정부등근무자   9(10) 407
                sStrRecord = sStrRecord + "0000000000";
                //C51 󰊉󰊚-10 K01-외국주둔군인등 9(10) 417
                sStrRecord = sStrRecord + "0000000000";
                //C52 󰊉󰊚M01-국외근로100만원  9(10) 427
                sStrRecord = sStrRecord + "0000000000";
                //C53 󰊉󰊚M02-국외근로300만원  9(10) 437
                sStrRecord = sStrRecord + "0000000000";
                //C54 󰊉󰊚M03-국외근로 9(10) 447
                sStrRecord = sStrRecord + "0000000000";
                //C55 󰊉󰊚-1 O01-야간근로수당  9(10) 457
                sStrRecord = sStrRecord + "0000000000";

                //C56 󰊉󰊚-2 Q01-출산보육수당  9(10) 467
                sStrRecord = sStrRecord + "0000000000";
                //C57 󰊉󰊚-21 R10-근로장학금   9(10) 477
                sStrRecord = sStrRecord + "0000000000";
                //C58 󰊉󰊚-29 R11-직무발명보상금  9(10) 487
                sStrRecord = sStrRecord + "0000000000";
                //C59 󰊉󰊚-11 S01-주식매수선택권  9(10) 497
                sStrRecord = sStrRecord + "0000000000";
                //C60 󰊉󰊚-12 U01-벤처기업주식매수선택권   9(10) 507
                sStrRecord = sStrRecord + "0000000000";
                //C61 a 󰊉󰊚-14 Y02-우리사주조합인출금50%  9(10) 517
                sStrRecord = sStrRecord + "0000000000";
                //C61 b 󰊉󰊚-15 Y03-우리사주조합인출금75%  9(10) 527
                sStrRecord = sStrRecord + "0000000000";
                //C61 c 󰊉󰊚-16 Y04-우리사주조합인출금100%  9(10) 537
                sStrRecord = sStrRecord + "0000000000";
                //C62 󰊉󰊛Y22-전공의수련 보조 수당         9(10) 547
                sStrRecord = sStrRecord + "0000000000";

                //C63 a 󰊉󰊚-12 T01-외국인기술자소득세감면(50%)   9(10) 507
                sStrRecord = sStrRecord + "0000000000";
                //C63 b 󰊉󰊚-12 T01-외국인기술자소득세감면(70%)   9(10) 507
                sStrRecord = sStrRecord + "0000000000";

                //C64  T30-성과공유 중소기업 경영성과급 9(10) 557
                sStrRecord = sStrRecord + "0000000000";

                //C65 a T40-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면   9(10) 587
                sStrRecord = sStrRecord + "0000000000";
                //C65 b T41-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면   9(10) 587
                sStrRecord = sStrRecord + "0000000000";
                //C65 c T42-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면   9(10) 587
                sStrRecord = sStrRecord + "0000000000";
                //C65 d T43-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면   9(10) 587
                sStrRecord = sStrRecord + "0000000000";

                //C66 T40-내국인 우수인력 국내복귀 소득세 감면        9(10) 587
                sStrRecord = sStrRecord + "0000000000";

                //C67 a T11-중소기업 취업자 소득세 감면50%        9(10) 587
                sStrRecord = sStrRecord + "0000000000";
                //C67 b T11-중소기업 취업자 소득세 감면70%        9(10) 587
                sStrRecord = sStrRecord + "0000000000";
                //C67 c T11-중소기업 취업자 소득세 감면90%        9(10) 587
                sStrRecord = sStrRecord + "0000000000";

                //C68 󰊉󰊚-28 T20-조세조약상 교직자감면             9(10) 587
                sStrRecord = sStrRecord + "0000000000";

                //C69 공란 9(10) 597
                sStrRecord = sStrRecord + "0000000000";

                //C70 비과세 계 9(10) 597
                sStrRecord = sStrRecord + "0000000000";
                //C71 감면소득 계 9(10) 597
                sStrRecord = sStrRecord + "0000000000";


                //【정산명세】

                //C72 󰊊󰊓총급여 9(11) 618
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNTaxTarGetPay"].ToString().Trim()));
                //C73 󰊊󰊔근로소득공제 9(10) 628
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNInComeDeduct"].ToString().Trim()));
                //C74 󰊊󰊕근로소득금액 9(11) 639
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNTaxInCome"].ToString().Trim()));

                //【기본공제】
                //C75 󰊊󰊖본인공제금액 9(8) 647
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WNOwnDeduct"].ToString().Trim()));
                //C76 󰊊󰊗배우자공제금액 9(8) 655
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WNWifeDeduct"].ToString().Trim()));
                //C77 a 󰊊󰊘부양가족공제인원 9(2) 657
                sStrRecord = sStrRecord + String.Format("{0:D2}", Convert.ToInt64(dt.Rows[0]["WNMIMAN"].ToString().Trim()) + Convert.ToInt64(dt.Rows[0]["WNISANG"].ToString().Trim()));
                //C77 b 󰊊󰊘부양가족공제금액 9(8) 665
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WNFamilyDeduct"].ToString().Trim()));

                //【추가공제】

                //C78 󰊊󰊙경로우대공제인원 9(2) 667
                sStrRecord = sStrRecord + String.Format("{0:D2}", Convert.ToInt64(dt.Rows[0]["WNKYUNG70"].ToString().Trim()));
                //C78 󰊊󰊙경로우대공제금액 9(8) 675
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WNOldAddDeduct"].ToString().Trim()));
                //C79 󰊊󰊚장애인공제인원 9(2) 677
                sStrRecord = sStrRecord + String.Format("{0:D2}", Convert.ToInt64(dt.Rows[0]["WNJANG"].ToString().Trim()));
                //C79 󰊊󰊚장애인공제금액 9(8) 685
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WNObjAddDeduct"].ToString().Trim()));
                //C80 󰊊󰊛부녀자공제금액 9(8) 693
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WNWomanDeduct"].ToString().Trim()));
                //C81 󰊋󰊒한부모가족공제 금액 9(10) 703
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNSParentDeduct"].ToString().Trim()));

                //【연금보험료공제】

                //C82 󰊋󰊓국민연금보험료공제대상금액  9(10) 713
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNNationPension"].ToString().Trim()));
                //C82 󰊋󰊓국민연금보험료공제  9(10) 713
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNNationPensionTaxAmount"].ToString().Trim()));

                //C83 󰊋󰊔공제_공무원연금공제대상금액     9(10) 723
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNPubOfficial"].ToString().Trim()));
                //C83 󰊋󰊔공제_공무원연금     9(10) 723
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNPubOfficialTaxAmount"].ToString().Trim()));

                //C84 󰊋󰊔-㉯공적연금보험료 공제대상금액_군인연금 9(10) 733
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNArmy"].ToString().Trim()));
                //C84 󰊋󰊔-㉯공적연금보험료 공제_군인연금 9(10) 733
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNArmyTaxAmount"].ToString().Trim()));

                //C85 󰊋󰊔-㉰공적연금보험료 공제_사립학교교직원 연금 공제대상금액 9(10) 743
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNPriSchool"].ToString().Trim()));
                //C85 󰊋󰊔-㉰공적연금보험료 공제_사립학교교직원 연금 9(10) 743
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNPriSchoolTaxAmount"].ToString().Trim()));

                //C86 󰊋󰊔-㉱공적연금보험료공제_별정우 체국연금 공제대상금액     9(10) 753
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNPostOffice"].ToString().Trim()));
                //C86 󰊋󰊔-㉱공적연금보험료공제_별정우 체국연금      9(10) 753
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNPostOfficeTaxAmount"].ToString().Trim()));

                //【특별소득공제】
                //C87  󰊋󰊕-㉮보험료-건강보험료(노인장기요양보험료 포함) 대상금액 9(10) 763
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNHealthInsur"].ToString().Trim()));
                //C87  󰊋󰊕-㉮보험료-건강보험료(노인장기요양보험료 포함)  9(10) 763
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNHealthInsurTaxAmount"].ToString().Trim()));

                //C88 󰊋󰊕-㉯보험료-고용보험료 대상금액                         9(10) 773
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNEmployMent"].ToString().Trim()));
                //C88 󰊋󰊕-㉯보험료-고용보험료                            9(10) 773
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNEmployMentTaxAmount"].ToString().Trim()));

                //[주택자금(주택임차차입금, 장기주택저당차입금, 주택마련저축) 관련 공통 사항]

                //C89 󰊋󰊖-㉮주택자금_주택임차차입금원리금상환액_ 대출기관   9(8) 781
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House_Lender"].ToString().Trim()));
                //C89 󰊋󰊖-㉮주택자금_주택임차차입금원리금상환액_ 거주자     9(8) 789
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House_Resident"].ToString().Trim()));

                //C90  󰊋󰊖-㉯ (2011년이전 차입분)주택자금_장기주택저당차입금이자상환액_15년미만   9(8) 797
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House11_15year"].ToString().Trim()));
                //C90  󰊋󰊖-㉯ (2011년이전 차입분)주택자금_장기주택저당차입금이자상환액_15년~29년  9(8) 805
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House11_29year"].ToString().Trim()));
                //C90  󰊋󰊖-㉯ (2011년이전 차입분)주택자금_장기주택저당차입금이자상환액_30년이상   9(8) 813
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House11_30year"].ToString().Trim()));

                //C91  󰊋󰊖-㉯ (2012년이후 차입분,15년 이상)고정금리·비거치식상환 대출            9(8) 821
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House12_Fixed"].ToString().Trim()));
                //C91  󰊋󰊖-㉯ (2012년이후 차입분,15년 이상)기타대출                               9(8) 829
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House12_Etc"].ToString().Trim()));

                //C92  󰊋󰊖-㉯ (2015년이후 차입분,상환기간 15년이상)고정금리and 비거치상환 대출    9(8) 837
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House15_15yearAndFIX"].ToString().Trim()));
                //C92  󰊋󰊖-㉯ (2015년이후 차입분,상환기간 15년이상)고정금리 or비거치 상환 대출    9(8) 845
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House15_15yearOrFIX"].ToString().Trim()));
                //C92  󰊋󰊖-㉯ (2015년이후 차입분,상환기간 15년이상)그 밖의대출                    9(8) 853
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House15_15yearEtc"].ToString().Trim()));
                //C92  󰊋󰊖-㉯ (2015년이후 차입분,상환기간 10년이상)고정금리 or비거치상환 대출     9(8) 861
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_House15_10To15yearFix"].ToString().Trim()));

                //C93  󰊋󰊗기부금(이월분) 9(11) 872
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WN_OVER_Donation"].ToString().Trim()));

                //C94  󰊋󰊗공란 9(11) 872
                sStrRecord = sStrRecord + "00000000000";

                //C95  󰊋󰊗공란 9(11) 872
                sStrRecord = sStrRecord + "00000000000";

                //C96 󰊋󰊘계 9(11) 883
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WN_SpcDeductTotal"].ToString().Trim()));

                //C97 󰊋󰊙차감소득금액 9(11) 894
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WN_ChaGam_InCome"].ToString().Trim()));

                //【그 밖의 소득공제】

                //C98 󰊋󰊚개인연금저축소득공제      9(8) 902
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_IndPension"].ToString().Trim()));

                //C99 󰊋󰊛소기업·소상공인 공제부금 9(10) 912
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_SmallTradeAmount"].ToString().Trim()));

                //C100 󰊌󰊒-㉮주택마련저축소득공제_청약저축                9(10) 922
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_HouseSav_Sub"].ToString().Trim()));
                //C101  󰊌󰊒-㉯주택마련저축소득공제_주택청약종합저축       9(10) 932
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_HouseSav_ToTalSub"].ToString().Trim()) );
                //C102  󰊌󰊒-㉰주택마련저축소득공제_근로자주택마련저축     9(10) 942
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_HouseSav_WorkerSub"].ToString().Trim()));

                //C103 󰊌󰊓투자조합출자등소득공제                          9(10) 952
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_InvestAmount"].ToString().Trim()));

                //C104 󰊌󰊔신용카드등소득공제                              9(8) 960
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_CreditCardAmount"].ToString().Trim()));

                //C105 󰊌󰊕우리사주조합출연금                              9(10) 970
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_OwnerContAmount"].ToString().Trim()));

                //C106 󰊌󰊖고용유지중소기업 근로자소득 공제                9(10) 980
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_SmallCompanyWorker"].ToString().Trim()));
                //C107 󰊌󰊗장기집합투자증권저축                            9(10) 990
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_InvestStockSaving"].ToString().Trim()));

                //C108 󰊌󰊗청년형 장기집합투자증권저축
                sStrRecord = sStrRecord + "0000000000";

                //C109 󰊌󰊗공란                            9(10) 990
                sStrRecord = sStrRecord + "0000000000";

                //C110 󰊌󰊘그 밖의소득공제계                               9(11) 1001
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WN_OtherTaxTotalAmount"].ToString().Trim()));

                //C111 󰊌󰊙소득공제종합한도초과액  9(11) 1012
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNStd_InComeLimitOver"].ToString().Trim()));

                //C112 󰊌󰊚종합소득과세표준        9(11) 1023
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNStd_TaxBaseAmount"].ToString().Trim()));

                //C113 󰊌󰊛산출세액                9(11) 1033
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNStd_CalTaxAmount"].ToString().Trim()));

                //【세액감면】
                //C114 󰊍󰊒소득세법 9(10) 1043
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxRe_InComeTaxLaw"].ToString().Trim()));
                //C115 󰊍󰊓조특법(󰊍󰊕제외) 9(10) 1053
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxRe_TaxTreaty"].ToString().Trim()));
                //C116 󰊍󰊔조특법 제30조 9(10) 1063
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxRe_SmallBizWorker"].ToString().Trim()));
                //C117 󰊍󰊕조세조약 9(10) 1073
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxRe_FrgnEng"].ToString().Trim()));

                //C118 󰊌󰊗공란                     9(10) 990
                sStrRecord = sStrRecord + "0000000000";

                //C119 󰊌󰊗공란                     9(10) 990
                sStrRecord = sStrRecord + "0000000000";

                //C120 󰊍󰊖세액감면계 9(10) 1083
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxRe_Sum"].ToString().Trim()));

                //【세액공제】

                //C121 󰊍󰊗근로소득세액공제   9(10) 1093
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_EarnedIncome"].ToString().Trim()));

                //C122 󰊍󰊘-㉮자녀세액공제 인원  9(2) 1095
                sStrRecord = sStrRecord + String.Format("{0:D2}", Convert.ToInt64(dt.Rows[0]["WNYANGYUG"].ToString().Trim()));
                //C122 󰊍󰊘-㉮자녀세액공제       9(10) 1105
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ChildrenDeduct"].ToString().Trim()));
                
                ////C121 󰊍󰊘-㉯6세이하자녀 세액공제인원  9(2) 1107
                //sStrRecord = sStrRecord + String.Format("{0:D2}", Convert.ToInt64(dt.Rows[0]["WNYANGYUG"].ToString().Trim()));
                ////C124 󰊍󰊘-㉯6세이하자녀 세액공제      9(10) 1117
                //sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ChildUnderDeduct"].ToString().Trim()));

                //C123 󰊍󰊘-㉰출산·입양세액공제인원  9(2) 1119
                sStrRecord = sStrRecord + String.Format("{0:D2}", Convert.ToInt64(dt.Rows[0]["WNCHULSAN"].ToString().Trim()));
                //C123 󰊍󰊘-㉰출산·입양세액공제      9(10) 1129
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ChildBirth"].ToString().Trim()));

                //C124 󰊍󰊙연금계좌_과학기술인공제_공제대상금액  9(10) 1139
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ScienAmount"].ToString().Trim()));
                //C124 󰊍󰊙연금계좌_과학기술인공제_              9(10) 1149
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ScienTaxAmount"].ToString().Trim()));

                //C125 󰊍󰊚연금계좌_근로자퇴직급여보장법에 따른퇴직급여_공제대상금액  9(10) 1159
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_RetireAmount"].ToString().Trim()));
                //C125 󰊍󰊚연금계좌_근로자퇴직급여보장법에 따른퇴직급여_세액공제액    9(10) 1169
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_RetireTaxAmount"].ToString().Trim()));

                //C126 󰊍󰊛연금계좌_연금저축_공제대상                9(10) 1179
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_PensionSavAmount"].ToString().Trim()));
                //C126 󰊍󰊛연금계좌_연금저축_세액공제액              9(10) 1189
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_PensionSavTaxAmount"].ToString().Trim()));

                //C127 󰊍󰊛ISA계좌 만기시 추가납입액_공제대상금액           9(10) 1179
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ISAAmount"].ToString().Trim()));
                //C127 󰊍󰊛ISA계좌 만기시 추가납입액_세액공제액             9(10) 1189
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ISATaxAmount"].ToString().Trim()));

                //C128 󰊎󰊒특별세액공제_보장성보험료_공제대상금액    9(10) 1199
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_InsurAmount"].ToString().Trim()));
                //C128 󰊎󰊒특별세액공제_보장성보험료_세액공제액      9(10) 1209
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_InsurTaxAmount"].ToString().Trim()));

                //C129 󰊎󰊒특별세액공제_장애인전용보장성보험료_      9(10) 1219
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ObsInsurAmount"].ToString().Trim()));
                //C129 󰊎󰊒특별세액공제_장애인전용보장성보험료_세액공제액   9(10) 1229
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_ObsInsurTaxAmount"].ToString().Trim()));

                //C130 󰊎󰊓특별세액공제_의료비_공제대상금액                 9(10) 1239
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_MedAmount"].ToString().Trim()));
                //C130 󰊎󰊓특별세액공제_의료비_세액                         9(10) 1249
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_MedTaxAmount"].ToString().Trim()));

                //C131 󰊎󰊔특별세액공제_교육비_공제대상금액      9(10) 1259
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_EduAmount"].ToString().Trim()));
                //C131 󰊎󰊔특별세액공제_교육비_세액공제액        9(10) 1269
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_EduTaxAmount"].ToString().Trim()));

                //C132 󰊎󰊕-㉮특별세액공제_기부금_정치자금_10만원이하_공제대상금액  9(10) 1279
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_Con_PoFund10Down"].ToString().Trim()));
                //C132 󰊎󰊕-㉮특별세액공제_기부금_정치자금_10만원이하_세액공제액    9(10) 1289
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_PoFund10Down"].ToString().Trim()));

                //C133 󰊎󰊕-㉮특별세액공제_기부금_정치자금_10만원초과_공제대상금액  9(11) 1300
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_Con_PoFund10Up"].ToString().Trim()));
                //C133 󰊎󰊕-㉮특별세액공제_기부금_정치자금_10만원초과_세액공제액    9(10) 1310
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_PoFund10Up"].ToString().Trim()));

                //C134 󰊎󰊕-㉯특별세액공제_기부금_법정기부금_                       9(11) 1321
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_Con_LawCon"].ToString().Trim()));
                //C134 󰊎󰊕-㉯특별세액공제_기부금_법정기부금_세액공제액             9(10) 1331
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_LawCon"].ToString().Trim()));

                //C135 󰊎󰊕-㉰특별세액공제_기부금_우리사주조합기부금_공제대상금액   9(11) 1342
                sStrRecord = sStrRecord + "00000000000";
                //C135 󰊎󰊕-㉰특별세액공제_기부금_우리사주조합                      9(10) 1352
                sStrRecord = sStrRecord + "0000000000";

                //C136 󰊎󰊕-㉱특별세액공제_기부금_지정기부금_종교단체외_공제대상금액  9(11) 1363
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_Con_FixConGroupOut"].ToString().Trim()));
                //C136 󰊎󰊕-㉱특별세액공제_기부금_지정기부금_종교단체외_세액공제액    9(10) 1373
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_FixConGroupOut"].ToString().Trim()));

                //C137 ⓐ󰊎󰊕-㉲특별세액공제_기부금_지정기부금_종교단체_공제대상금액  9(11) 1384
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_Con_FixConGroup"].ToString().Trim()));
                //C137 ⓑ󰊎󰊕-㉲특별세액공제_기부금_지정기부금_종교단체_세액공제액    9(10) 1394
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_FixConGroup"].ToString().Trim()));

                //C138 공란   9(11) 1394
                sStrRecord = sStrRecord + "00000000000";

                //C139 󰊎󰊖특별세액공제계 9(10) 1404
                iSpcTaxDedTotal = Convert.ToInt64(dt.Rows[0]["WNTaxCr_InsurTaxAmount"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_ObsInsurTaxAmount"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_MedTaxAmount"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_EduTaxAmount"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_PoFund10Down"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_PoFund10Up"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_LawCon"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_FixConGroupOut"].ToString().Trim()) +
                                  Convert.ToInt64(dt.Rows[0]["WNTaxCr_TAX_FixConGroup"].ToString().Trim());
                sStrRecord = sStrRecord + String.Format("{0:D10}", iSpcTaxDedTotal);

                //C140 󰊎󰊗표준세액공제 9(10) 1414
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNStandardTAX"].ToString().Trim()));

                //C141 󰊎󰊘납세조합공제 9(10) 1424
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxAssociation"].ToString().Trim()));

                //C142 󰊎󰊙주택차입금 9(10) 1434
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNHouseLoan"].ToString().Trim()));

                //C143 󰊎󰊚외국납부 9(10) 1444
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNFrgnPayMent"].ToString().Trim()));

                //C144 󰊎󰊛월세세액공제 대상금액  9(10) 1454
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WN_MonthRent"].ToString().Trim()));

                //C144 󰊎󰊛월세세액공제액 9(8) 1462
                sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[0]["WN_TAX_MonthRent"].ToString().Trim()));

                //C145 󰊏󰊒세액공제계 9(10) 1472
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNTaxCr_DeductTotal"].ToString().Trim()));

                //【결정세액】
                //C146 󰊏󰊔소득세 9(11) 1482
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNFixTax"].ToString().Trim()));
                //C146 󰊏󰊔지방소득세 9(10) 1492
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNFixReSidenceTax"].ToString().Trim()));
                //C146 󰊏󰊔농특세 9(10) 1502
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNFixVillTax"].ToString().Trim()));

                //C147 실효세율 9(3) 1502
                //계산방법: (소득세_결정세액 / 총급여) × 100 : [C153] = [C152ⓐ] / [C76] × 100   
                
                sStrRecord = sStrRecord + String.Format("{0:D3}", Convert.ToInt64(dt.Rows[0]["WNActTaxRate"].ToString().Trim()));

                //【기납부세액 - 주(현)근무지】
                //C148 󰊏󰊖소득세 9(11) 1512
                sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNCuInComeTax"].ToString().Trim()));
                //C148 󰊏󰊖지방소득세 9(10) 1522
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNCuReSidenceTax"].ToString().Trim()));
                //C148 󰊏󰊖농특세 9(10) 1532
                sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNCuVillTax"].ToString().Trim()));

                //【납부특례세액】

                //C149 󰊏󰊗소득세 9(11) 1542
                sStrRecord = sStrRecord + "00000000000";
                //C149 󰊏󰊗지방소득세 9(10) 1552
                sStrRecord = sStrRecord + "0000000000";
                //C149 󰊏󰊗농특세 9(10) 1562
                sStrRecord = sStrRecord + "0000000000";

                //【차감징수세액】
                //C150 󰊏󰊘소득세  9(1) 1563  9(10) 1573
                if (Convert.ToDouble(dt.Rows[0]["WNCollectTax"].ToString().Trim()) >= 0)
                {
                    sStrRecord = sStrRecord + "0";  //양수
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNCollectTax"].ToString().Trim()));
                }
                else
                {
                    sStrRecord = sStrRecord + "1";  //음수
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[0]["WNCollectTax"].ToString().Trim()) * -1);
                }
                //C150 󰊏󰊘지방소득세  9(1) 1574  9(10) 1584
                if (Convert.ToDouble(dt.Rows[0]["WNCollectReSidenceTax"].ToString().Trim()) >= 0)
                {
                    sStrRecord = sStrRecord + "0";  //양수
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNCollectReSidenceTax"].ToString().Trim()));
                }
                else
                {
                    sStrRecord = sStrRecord + "1";  //음수
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNCollectReSidenceTax"].ToString().Trim()) * -1);
                }
                //C150 󰊏󰊘농특세      9(1) 1585  9(10) 1595
                if (Convert.ToDouble(dt.Rows[0]["WNCollectVillTax"].ToString().Trim()) >= 0)
                {
                    sStrRecord = sStrRecord + "0";  //양수
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNCollectVillTax"].ToString().Trim()));
                }
                else
                {
                    sStrRecord = sStrRecord + "1";  //음수
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[0]["WNCollectVillTax"].ToString().Trim()) * -1);
                }               

                //C151 공란 X(128) 1620
                sStrRecord = sStrRecord + string.Format("{0,-128:G}", "");

                StrWrReCode.WriteLine(sStrRecord);
            }

        }
        #endregion

        #region  Description : 연말정산 D 레코드 생성
        private void UP_NTS_RecordD(string sCompy, string sYEAR, string sKBSABUN, Int32 iRowCnt, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;
            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BFY117", TYUserInfo.SecureKey, "Y", sCompy, sYEAR, sKBSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fiRowCnt = fiRowCnt + 1;
                    //【자료관리번호】
                    //D1 레코드 구분 X(1) 1
                    sStrRecord = "D";
                    //D2 자료구분 9(2) 3
                    sStrRecord = sStrRecord + "20";
                    //D3 세무서코드 X(3) 6
                    sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                    //D4 일련번호 9(6) 12
                    sStrRecord = sStrRecord + String.Format("{0:D6}", iRowCnt);
                    // 【원천징수의무자】
                    // D5 ③사업자등록번호 X(10) 22
                    sStrRecord = sStrRecord + fsSAUPNO;

                    // D6 공란 X(50) 72(2017년까지)
                    //sStrRecord = sStrRecord + string.Format("{0,-50:G}", "");

                    // 【소득자】
                    //D6 ⑦소득자주민등록번호  X(13) 35
                    sStrRecord = sStrRecord + dt.Rows[i]["KBJUMIN"].ToString().Trim().Replace("-", "");

                    //D7 종교관련종사자 여부  X(1) 36
                    sStrRecord = sStrRecord + "2";

                    //근무처별 소득명세- 종(전)근무처】
                    //D8 󰊉󰊘-1납세조합여부 X(01) 36
                    sStrRecord = sStrRecord + "1";

                    //D9  ⑨법인명(상호) X(60) 96
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["WKSANGHO"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(dt.Rows[i]["WKSANGHO"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;

                    //D10 ⑩사업자등록번호 X(10) 106
                    sStrRecord = sStrRecord + dt.Rows[i]["WKSAUPNO"].ToString().Trim().Replace("-", "");
                    //D11 ⑪근무기간 시작연월일   9(8) 114
                    sStrRecord = sStrRecord + dt.Rows[i]["WKSDATE"].ToString().Trim();
                    //D12 ⑪근무기간 종료연월일   9(8) 122
                    sStrRecord = sStrRecord + dt.Rows[i]["WKEDATE"].ToString().Trim();

                    //D13 ⑫감면기간 시작연월일   9(8) 130
                    sStrRecord = sStrRecord + "00000000";

                    //D14 ⑫감면기간종료연월일  9(8) 138
                    sStrRecord = sStrRecord + "00000000";

                    //D15 ⑬급여 9(11) 149
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[i]["WKMPYAMOUNT"].ToString().Trim()));

                    //D16 ⑭상여 9(11) 160
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[i]["WKSPYAMOUNT"].ToString().Trim()));

                    //D17 ⑮인정상여 9(11) 171
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[i]["WKISPYAMOUNT"].ToString().Trim()));

                    //D18 ⑮-1 주식매수선택권 행사이익 9(11) 182
                    sStrRecord = sStrRecord + "00000000000";
                    //D19 ⑮-2 우리사주조합인출금      9(11) 193
                    sStrRecord = sStrRecord + "00000000000";
                    //D20 ⑮-3 임원퇴직소득 한도 초과액  9(11) 204
                    sStrRecord = sStrRecord + "00000000000";

                    //D21 ⑮-4 직무발명보상금 9(11) 215
                    sStrRecord = sStrRecord + "00000000000";

                    //D22 공란 9(11) 237
                    sStrRecord = sStrRecord + "00000000000";

                    //D23 공란 9(11) 237
                    sStrRecord = sStrRecord + "00000000000";

                    //D24 󰊉󰊘계 9(11) 248
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[i]["WKMPYAMOUNT"].ToString().Trim()) + Convert.ToInt64(dt.Rows[i]["WKSPYAMOUNT"].ToString().Trim()) + Convert.ToInt64(dt.Rows[i]["WKISPYAMOUNT"].ToString().Trim()));


                    //【종(전)근무처 비과세소득 및 감면 소득】 서식항목 󰊉󰊚 ∼ 󰊊󰊒-1에 해당함
                    //D25 󰊉󰊚-5 G01-비과세학자금    9(10) 258
                    sStrRecord = sStrRecord + "0000000000";
                    //D26 󰊉󰊚-18 H05-경호·승선수당 9(10) 278
                    sStrRecord = sStrRecord + "0000000000";
                    //D27 󰊉󰊚-4 H06-유아·초중등    9(10) 288
                    sStrRecord = sStrRecord + "0000000000";
                    //D28 󰊉󰊚-4 H07-고등교육법      9(10) 298
                    sStrRecord = sStrRecord + "0000000000";
                    //D29 󰊉󰊚-4 H08-특별법        9(10) 308
                    sStrRecord = sStrRecord + "0000000000";
                    //D30 󰊉󰊚-4 H09-연구기관 등   9(10) 318
                    sStrRecord = sStrRecord + "0000000000";
                    //D31 󰊉󰊚-4 H10-기업부설연구소              9(10) 328
                    sStrRecord = sStrRecord + "0000000000";
                    //D32 󰊉󰊚-22 H14-보육교사 근무환경 개선비   9(10) 338
                    sStrRecord = sStrRecord + "0000000000";
                    //D33 󰊉󰊚-23 H15-사립유치원수석교사·교사의인건비  9(10) 348
                    sStrRecord = sStrRecord + "0000000000";

                    //D34 󰊉󰊚-6 H11-취재수당           9(10) 358
                    sStrRecord = sStrRecord + "0000000000";
                    //D35 󰊉󰊚-7 H12-벽지수당           9(10) 368
                    sStrRecord = sStrRecord + "0000000000";
                    //D36 󰊉󰊚-8 H13-재해관련급여       9(10) 378
                    sStrRecord = sStrRecord + "0000000000";
                    //D37 󰊉󰊚-24 H16-정부·공공 기관지방이전기관종사자 이주수당  9(10) 388
                    sStrRecord = sStrRecord + "0000000000";
                    //D38 8 󰊉󰊚-30 H17 종교 활동비  9(10) 398
                    sStrRecord = sStrRecord + "0000000000";
                    //D39 󰊉󰊚-19 I01-외국정부등근무자 9(10) 408
                    sStrRecord = sStrRecord + "0000000000";
                    //D40 󰊉󰊚-10 K01-외국주둔군인등  9(10) 418
                    sStrRecord = sStrRecord + "0000000000";
                    //D41 󰊉󰊚M01-국외근로100만원     9(10) 428
                    sStrRecord = sStrRecord + "0000000000";
                    //D42 󰊉󰊚M02-국외근로300만원     9(10) 438
                    sStrRecord = sStrRecord + "0000000000";

                    //D43 󰊉󰊚M03-국외근로 9(10) 448
                    sStrRecord = sStrRecord + "0000000000";
                    //D44 󰊉󰊚-1 O01-야간근로수당  9(10) 458
                    sStrRecord = sStrRecord + "0000000000";
                    //D45 󰊉󰊚-2 Q01-출산보육수당  9(10) 468
                    sStrRecord = sStrRecord + "0000000000";
                    //D46 󰊉󰊚-21 R10-근로장학금   9(10) 478
                    sStrRecord = sStrRecord + "0000000000";
                    //D47 󰊉󰊚-29 R11-직무발명보상금 9(10) 488
                    sStrRecord = sStrRecord + "0000000000";
                    //D48 󰊉󰊚-11 S01-주식매수선택권  9(10) 498
                    sStrRecord = sStrRecord + "0000000000";
                    //D49 U01-벤처기업주식매수선택권   9(10) 508
                    sStrRecord = sStrRecord + "0000000000";
                    //D50 󰊉󰊚-14 Y02-우리사주조합인출금50%  9(10) 518
                    sStrRecord = sStrRecord + "0000000000";

                    //D50 󰊉󰊚-15 Y03-우리사주조합인출금75%   9(10) 528
                    sStrRecord = sStrRecord + "0000000000";
                    //D50 ⓐ󰊉󰊚-16 Y04-우리사주조합인출금100% 9(10) 538
                    sStrRecord = sStrRecord + "0000000000";

                    //D51 󰊉󰊛Y22-전공의수련 보조수당         9(10) 548
                    sStrRecord = sStrRecord + "0000000000";

                    //D52 󰊉󰊉󰊚󰊚-12 T01-외국인기술자소득세감면(50%)  9(10) 558
                    sStrRecord = sStrRecord + "0000000000";
                    //D52 󰊉󰊉󰊚󰊚-12 T01-외국인기술자소득세감면(70%)  9(10) 558
                    sStrRecord = sStrRecord + "0000000000";

                    //D53 T30-성과공유 중소기업 경영성과급  9(10) 568
                    sStrRecord = sStrRecord + "0000000000";

                    //D54 T41-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면 9(10) 568
                    sStrRecord = sStrRecord + "0000000000";
                    //D54 T41-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면 9(10) 568
                    sStrRecord = sStrRecord + "0000000000";
                    //D54 T41-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면 9(10) 568
                    sStrRecord = sStrRecord + "0000000000";
                    //D54 T41-중견기업 청년근로자 및 핵심인력 성과보상기금 소득세 감면 9(10) 568
                    sStrRecord = sStrRecord + "0000000000";


                    //D55 T50-내국인 우수인력 국내복귀 소득세 감면  9(10) 568
                    sStrRecord = sStrRecord + "0000000000";

                    //D56 ⓐ󰊉󰊚-26 T11-중소기업취업 청년소득세 감면50% 9(10) 578
                    sStrRecord = sStrRecord + "0000000000";
                    //D56 ⓑ󰊉󰊚-27 T12-중소기업취업 청년소득세 감면70% 9(10) 588
                    sStrRecord = sStrRecord + "0000000000";
                    //D56 󰊉󰊉󰊚󰊚-32 T13-중소기업취업청년소득세감면90% 9(10) 598
                    sStrRecord = sStrRecord + "0000000000";

                    //D57 󰊉󰊉󰊚󰊚-28 T20-조세 조약상 교직자 감면  9(10) 608
                    sStrRecord = sStrRecord + "0000000000";
                    //D58 공란 9(10) 608
                    sStrRecord = sStrRecord + "0000000000";
                    //D59 비과세계 9(10) 638
                    sStrRecord = sStrRecord + "0000000000";
                    //D60  감면소득 계 9(10) 648
                    sStrRecord = sStrRecord + "0000000000";


                    //【기납부세액 - 종(전)근무지】: 결정세액란의 세액 기재
                    //D61 󰊏󰊕소득세 9(11) 658
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[i]["WKINCOMETAX"].ToString().Trim()));
                    //D62 󰊏󰊕지방소득세 9(10) 668
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WKRESTAX"].ToString().Trim()));
                    //D63 󰊏󰊕농특세 9(10) 678
                    sStrRecord = sStrRecord + "0000000000";

                    //D64 종(전)근무처일련번호 9(2) 680 
                    sStrRecord = sStrRecord + String.Format("{0:D2}", fiRowCnt);
                    //D65 공란 X(1288) 1882
                    sStrRecord = sStrRecord + string.Format("{0,-1288:G}", "");

                    StrWrReCode.WriteLine(sStrRecord);

                }

            }
        }
        #endregion

        #region  Description : 연말정산 E 레코드 생성
        private void UP_NTS_RecordE(string sCompy, string sYEAR, string sKBSABUN, Int32 iRowCnt, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;
            string sYN = string.Empty;
            int iFamyRowCnt = 0;
            int iCnt = 0;
        
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BB4098", sCompy, sYEAR, sKBSABUN, TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {               

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (iFamyRowCnt == 0)
                    {
                        //【자료관리번호】
                        //E1 레코드 구분 X(1) 1
                        sStrRecord = "E";
                        //E2 자료구분 9(2) 3
                        sStrRecord = sStrRecord + "20";
                        //E3 세무서코드 X(3) 6
                        sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                        //E4 일련번호 9(6) 12
                        sStrRecord = sStrRecord + String.Format("{0:D6}", iRowCnt);
                        //【원천징수의무자】
                        //E5 ③사업자등록번호 X(10) 22
                        sStrRecord = sStrRecord + fsSAUPNO;
                        //【소득자】
                        //E6  ⑦소득자 주민등록번호  X(13) 35
                        sStrRecord = sStrRecord + dt.Rows[0]["KBJUMIN"].ToString().Trim().Replace("-", "");
                    }
                    
                    //【소득공제명세의 인적사항1】
                    //E7 관계 X(1) 36
                    sStrRecord = sStrRecord + dt.Rows[i]["WFGUBUN"].ToString().Trim();
                    //E8 내·외국인구분코드  X(1) 37
                    sStrRecord = sStrRecord + dt.Rows[i]["WFNATION"].ToString().Trim();
                    //E9 성명 X(30) 67
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["WFNAME"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(dt.Rows[i]["WFNAME"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;
                    //E10 주민등록번호 X(13) 80
                    sStrRecord = sStrRecord + dt.Rows[i]["WFJUMIN"].ToString().Trim().Replace("-", "");
                    //E11 기본공제 X(1) 81
                    sYN = dt.Rows[i]["WFKIBON"].ToString().Trim() == "Y" ? "1" : " ";
                    sStrRecord = sStrRecord + sYN;
                    //E12 장애인공제 X(1) 82
                    sYN = dt.Rows[i]["WFJANG"].ToString().Trim() == "Y" ? "1" : " ";
                    sStrRecord = sStrRecord + sYN;
                    //E13 부녀자공제 X(1) 83
                    sYN = dt.Rows[i]["WFBUNYO"].ToString().Trim() == "Y" ? "1" : " ";
                    sStrRecord = sStrRecord + sYN;
                    //E14 경로우대공제 X(1) 84
                    if (Convert.ToInt16(dt.Rows[i]["AGE"].ToString().Trim()) >= 70 && dt.Rows[i]["WFKIBON"].ToString().Trim() == "Y")
                    {
                        sStrRecord = sStrRecord + "1";
                    }
                    else
                    {
                        sStrRecord = sStrRecord + " ";
                    }
                    //E15 한부모가족공제 X(1) 85
                    sYN = dt.Rows[i]["WFSPARENT"].ToString().Trim() == "Y" ? "1" : " ";
                    sStrRecord = sStrRecord + sYN;
                    //E16 출산·입양공제 X(1) 86
                    sYN = dt.Rows[i]["WFCHULSAN"].ToString().Trim() == "Y" ? "1" : " ";
                    sStrRecord = sStrRecord + sYN;
                    //E17 자녀공제 X(1) 87
                    sYN = dt.Rows[i]["WFJANYE"].ToString().Trim() == "Y" ? "1" : " ";
                    sStrRecord = sStrRecord + sYN;
                    //E18 교육비공제 X(1) 88
                    if (Convert.ToDouble(dt.Rows[i]["WFTAXGYOUK"].ToString().Trim()) + Convert.ToDouble(dt.Rows[i]["WFGYOUK"].ToString().Trim()) > 0)
                    {
                        sStrRecord = sStrRecord + dt.Rows[i]["WFEDUGNTAXCODE"].ToString().Trim();
                    }
                    else
                    {
                        sStrRecord = sStrRecord + " ";
                    }
                    //【소득공제명세의 국세청 자료1】

                    //E19 보험료_건강      9(10) 98
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXBOHUM"].ToString().Trim()));
                    //E20 보험료_고용보험  9(10) 108
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXEMPLOYAMT"].ToString().Trim()));
                    //E21 보험료_보장성보험 9(10) 118
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXPROBOHUM"].ToString().Trim()));
                    //E22 보험료_장애인전용보장성보험 9(10) 128
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXOBJBOHUM"].ToString().Trim()));
                    //E23 의료비_일반 9(10) 138
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXMEDBH"].ToString().Trim()));
                    //E24 의료비_미숙아.선천성 9(10) 148
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXMEDAPRIO"].ToString().Trim()));
                    //E25 의료비_난임 9(10) 148
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXMEDBHCBP"].ToString().Trim()));
                    //E26 의료비_장애인 9(10) 158
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXMEDBHJANG"].ToString().Trim()));                                      
                    //E27 의료비_실손의료보험금 9(10) 158                    
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXMEDBHSILBO"].ToString().Trim()));                    
                    //E28 교육비_일반 9(10) 168
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXGYOUK"].ToString().Trim()));
                    //E29 교육비_장애인 9(10) 178
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXGYOUKJANG"].ToString().Trim()));                

                    //E30 신용카드 그외분(전통시장·대중교통비 제외) 9(10) 188
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["SYS_WFTAXCARD"].ToString().Trim()));
                    //E31 직불·선불카드 그외분(전통시장·대중교통비 제외) 9(10) 198
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["SYS_WFTAXDEBCARD"].ToString().Trim()));
                    //E32 현금영수증 그외분(전통시장·대중교통비 제외) 9(10) 208
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["SYS_WFTAXCASH"].ToString().Trim()));
                    //E33 도서공연 그외분9(10) 218
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["SYS_WFTAXBOOK"].ToString().Trim()));
                    //E34 전통시장사용액 그외분 9(10) 228
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXMARKET"].ToString().Trim()));

                    //E35 대중교통이용액 그외분 9(10) 238 - 상반기
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXPUBTRANS"].ToString().Trim()));
                    //E36 대중교통이용액 그외분 9(10) 238 - 하반기
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXPUBTRANSSHALF"].ToString().Trim()));

                    //E37 소비증가분_2021년 전체 사용분 9(10) 238 - 신용카드
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXINCOME20"].ToString().Trim()));
                    //E38 소비증가분_2021년 전체 사용분 9(10) 238 - 전통시장
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXINCMARKETPYEAR"].ToString().Trim()));

                    //E39 소비증가분_2022년 전체 사용분 9(10) 238 - 신용카드
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXINCOME21"].ToString().Trim()));
                    //E40 소비증가분_2022년 전체 사용분 9(10) 238 - 전통시장
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFTAXINCMARKETNYEAR"].ToString().Trim()));

                    //E41 기부금 9(13) 251
                    //sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["WFTAXFUND"].ToString().Trim()));
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["RESULTWFTAXFUND"].ToString().Trim()));

                    //【소득공제명세의 기타 자료1】

                    //E42 보험료_건강  9(10) 261
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFBOHUM"].ToString().Trim()));
                    //E43 보험료_고용보험  9(10) 271
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFEMPLOYAMT"].ToString().Trim()));

                    //E44 보험료_보장성보험 9(10) 281
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFPROBOHUM"].ToString().Trim()));
                    //E45 보험료_장애인전용 보장성보험 9(10) 291
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFOBJBOHUM"].ToString().Trim()));

                    //E46 의료비 9(10) 301
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFMEDBH"].ToString().Trim()));
                    //E47 의료비_미숙아.선청성 9(10) 301
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFMEDAPRIO"].ToString().Trim()));

                    //E48 의료비_난임 9(10) 311
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFMEDBHCBP"].ToString().Trim()));
                    //E49 의료비_장애인 9(10) 321
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFMEDBHJANG"].ToString().Trim()));
                    //E50 의료비_실손의료보험금 9(10) 321
                    //부호: 입력값이 0 이상인 경우 ‘0’, 계산값이 0 미만인 경우 ‘1’ 기재
                    if (Convert.ToDouble(dt.Rows[i]["WFMEDBHSILBO"].ToString().Trim()) >= 0)
                    {
                        sStrRecord = sStrRecord + "0";
                    }
                    else
                    {
                        sStrRecord = sStrRecord + "1";
                    }
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFMEDBHSILBO"].ToString().Trim()));

                    //E51 교육비_일반 9(10) 331
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFGYOUK"].ToString().Trim()));
                    //E52 교육비_장애인 9(10) 341
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFGYOUKJANG"].ToString().Trim()));
                  
                    // 그외 사용분
                    //E53 신용카드 외(전통시장·대중교통비 제외) 9(10) 351
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["SYS_WFCARD"].ToString().Trim()));
                    //E54 직불·선불카드 외(전통시장·대중교통비 제외) 9(10) 361
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["SYS_WFDEBCARD"].ToString().Trim()));
                    //E55 도서공연 외(전통시장·대중교통비 제외) 9(10) 371
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["SYS_WFBOOK"].ToString().Trim()));
                    //E56 전통시장사용액 외 9(10) 381
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFMARKET"].ToString().Trim()));

                    //E57 대중교통이용액 외 9(10) 391 - 상반기
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFPUBTRANS"].ToString().Trim()));
                    //E58 대중교통이용액 외 9(10) 391 - 하반기
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFPUBTRANSSHALF"].ToString().Trim()));

                    //E59 소비증가분_2021년 전체 사용분 9(10) 238 - 신용카드
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFINCOME20"].ToString().Trim()));
                    //E60 소비증가분_2021년 전체 사용분 9(10) 238 - 전통시장
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFINCMARKETPYEAR"].ToString().Trim()));

                    //E61 소비증가분_2021년 전체 사용분 9(10) 238 - 신용카드
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFINCOME21"].ToString().Trim()));
                    //E62 소비증가분_2021년 전체 사용분 9(10) 238 - 전통시장
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["WFINCMARKETNYEAR"].ToString().Trim()));


                    //E63 기부금 외 9(13) 404
                    //sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["WFFUND"].ToString().Trim()));
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["RESULTWFFUND"].ToString().Trim()));

                    iFamyRowCnt = iFamyRowCnt + 1;

                    if (iFamyRowCnt == 3)
                    {
                        iFamyRowCnt = 0;

                        iCnt = iCnt + 1;

                        sStrRecord = sStrRecord + String.Format("{0:D2}", iCnt);
                        sStrRecord = sStrRecord + string.Format("{0,-443:G}", "");

                        StrWrReCode.WriteLine(sStrRecord);
                    }
                }

                //공백채우기
                if (3 - iFamyRowCnt != 0 && iFamyRowCnt != 0)
                {
                    for (int i = 1; i <= (3 - iFamyRowCnt); i++)
                    {
                        //【소득공제명세의 인적사항1】
                        //E64 관계 X(1) 36
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E65 내·외국인구분코드  X(1) 37
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E66 성명 X(30) 57
                        sStrTemp = "";
                        sStrTemp = "";
                        sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount("")));
                        sStrRecord = sStrRecord + sStrTemp;
                        //E67 주민등록번호 X(13) 70
                        sStrRecord = sStrRecord + String.Format("{0,-13:G}", "");
                        //E68 기본공제 X(1) 71
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E69 장애인공제 X(1) 72
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E70 부녀자공제 X(1) 73
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E71 경로우대공제 X(1) 74
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E72 한부모가족공제 X(1) 75
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E73 출산·입양공제 X(1) 76
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E74 자녀공제 X(1) 77
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                        //E75 교육비공제 X(1) 78
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");

                        //【소득공제명세의 국세청 자료1】
                        //E19 보험료_건강      9(10) 98
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E20 보험료_고용보험  9(10) 108
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E21 보험료_보장성보험 9(10) 118
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E22 보험료_장애인전용보장성보험 9(10) 128
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E23 의료비_일반 9(10) 138
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E23 의료비_선청성.미숙아 9(10) 138
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E24 의료비_난임 9(10) 148
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E25 의료비_장애인 9(10) 158
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E26 의료비_실손의료보험금 9(10) 158                        
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E27 교육비_일반 9(10) 168
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E28 교육비_장애인 9(10) 178
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));                      

                        //E29 신용카드(전통시장·대중교통비 제외) 9(10) 188
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E30 직불·선불카드(전통시장·대중교통비 제외) 9(10) 198
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E31 현금영수증(전통시장·대중교통비 제외) 9(10) 208
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E32 도서공연 9(10) 218
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E33 전통시장사용액 9(10) 228
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E34 대중교통이용액 9(10) 238 - 상반기
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E34 대중교통이용액 9(10) 238 - 하반기
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E35 소비증가분_2021년 전체 사용분 9(10) 238 - 신용카드
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E35 소비증가분_2021년 전체 사용분 9(10) 238 - 전통시장
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E36 소비증가분_2022년 전체 사용분 9(10) 238 - 신용카드
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E36 소비증가분_2023년 전체 사용분 9(10) 238 - 전통시장
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E37 기부금 9(13) 251
                        //sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["WFTAXFUND"].ToString().Trim()));
                        sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(Get_Numeric("0")));

                        //【소득공제명세의 기타 자료1】

                        //E38 보험료_건강  9(10) 261
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E39 보험료_고용보험  9(10) 271
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E40 보험료_보장성보험 9(10) 281
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E41 보험료_장애인전용 보장성보험 9(10) 291
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E42 의료비 9(10) 301
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E42 의료비_미숙아.선천성 9(10) 301
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E43 의료비_난임 9(10) 311
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E44 의료비_장애인 9(10) 321
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E45 의료비_실손의료보험금 9(10) 321
                        sStrRecord = sStrRecord + "0";
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E46 교육비_일반 9(10) 331
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E47 교육비_장애인 9(10) 341
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));                 
                                           
                        //E48 신용카드 외(전통시장·대중교통비 제외) 9(10) 351
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E49 직불·선불카드 외(전통시장·대중교통비 제외) 9(10) 361
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E50 도서공연 외(전통시장·대중교통비 제외) 9(10) 371
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E51 전통시장사용액 외 9(10) 381
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E52 대중교통이용액 외 9(10) 391 - 상반기
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E52 대중교통이용액 외 9(10) 391 - 하반기
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E53 소비증가분_2021년 전체 사용분 9(10) 238 - 신용카드
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E53 소비증가분_2021년 전체 사용분 9(10) 238 - 전통시장
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E54 소비증가분_2022년 전체 사용분 9(10) 238 - 신용카드
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //E54 소비증가분_2022년 전체 사용분 9(10) 238 - 전통시장
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //E55 기부금 외 9(13) 404
                        //sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["WFFUND"].ToString().Trim()));
                        sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(Get_Numeric("0")));
                    }

                    iCnt = iCnt + 1;

                    sStrRecord = sStrRecord + String.Format("{0:D2}", iCnt);
                    sStrRecord = sStrRecord + string.Format("{0,-443:G}", "");

                    StrWrReCode.WriteLine(sStrRecord);

                }
            }


        }
        #endregion

        #region  Description : 연말정산 F 레코드 생성( 연금. 저축 명세서 )
        private void UP_NTS_RecordF(string sCompy, string sYEAR, string sKBSABUN, Int32 iRowCnt, StreamWriter StrWrReCode)
        {
            int iCnt = 0;
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;
            int iFRecRowCnt = 0;            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_B22A0477", TYUserInfo.SecureKey, "Y", sCompy, sYEAR, sKBSABUN, "");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (iFRecRowCnt == 0)
                    {
                        //【자료관리번호】
                        //F1 레코드 구분 X(1) 1
                        sStrRecord = "F";
                        //F2 자료구분 9(2) 3
                        sStrRecord = sStrRecord + "20";
                        //F3 세무서코드 X(3) 6
                        sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                        //F4 일련번호 9(6) 12
                        sStrRecord = sStrRecord + String.Format("{0:D6}", iRowCnt);
                        //【원천징수의무자】
                        //F5 ②사업자등록번호 X(10) 22
                        sStrRecord = sStrRecord + fsSAUPNO;
                        //【소득자】
                        //F6 ④소득자 주민등록번호 X(13) 35
                        sStrRecord = sStrRecord + dt.Rows[i]["KBJUMIN"].ToString().Trim().Replace("-", "");
                    }

                    //【연금·저축등 소득·세액 공제명세1】
                    //F7 소득공제구분 X(2) 37
                    sStrRecord = sStrRecord + dt.Rows[i]["ANTYPECODE"].ToString();
                    //F8 금융기관코드 X(3) 40
                    sStrRecord = sStrRecord + dt.Rows[i]["ANBANKCODE"].ToString();
                    //F9 금융기관상호 X(60) 100                    
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["ANBANKCODENM"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(dt.Rows[i]["ANBANKCODENM"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;

                    //F10 계좌번호(또는 증권번호) X(20) 120
                    sStrRecord = sStrRecord + string.Format("{0,-20:G}", dt.Rows[i]["ANBANKACNUM"].ToString().Replace("-",""));
                    //F11 납입금액 9(10) 130
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["ANPAYAMOUNT"].ToString().Trim()));
                    //F12 소득·세액공제금액 9(10) 140
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["ANDEDAMOUNT"].ToString().Trim()));

                    //F13 투자년도 9(4) 144
                    sStrRecord = sStrRecord + String.Format("{0:D4}", Convert.ToInt64(dt.Rows[i]["ANINVYEAR"].ToString().Trim()));
                    //F14 투자구분 X(1) 145
                    sStrRecord = sStrRecord + string.Format("{0,-1:G}", dt.Rows[i]["ANINVGUBN"].ToString().Trim());

                    //F15 가입일 9(8) 144
                    sStrRecord = sStrRecord + "00000000";
                    //F16 계약기간 9(2) 
                    sStrRecord = sStrRecord + "00";

                    iFRecRowCnt = iFRecRowCnt + 1;

                    if (iFRecRowCnt == 15)
                    {
                        iFRecRowCnt = 0;

                        iCnt = iCnt + 1;

                        sStrRecord = sStrRecord + String.Format("{0:D2}", iCnt);

                        sStrRecord = sStrRecord + string.Format("{0,-173:G}", "");

                        StrWrReCode.WriteLine(sStrRecord);
                    }
                }

                //공백채우기
                if (15 - iFRecRowCnt != 0 && iFRecRowCnt != 0)
                {
                    for (int i = 1; i <= (15 - iFRecRowCnt); i++)
                    {
                        //F7 소득공제구분 X(2) 37
                        sStrRecord = sStrRecord + String.Format("{0,-2:G}", "");
                        //F8 금융기관코드 X(3) 40
                        sStrRecord = sStrRecord + String.Format("{0,-3:G}", "");
                        //F9 금융기관상호 X(60) 90                    
                        sStrTemp = "";
                        sStrTemp = "";
                        sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount("")));
                        sStrRecord = sStrRecord + sStrTemp;
                        //F10 계좌번호(또는 증권번호) X(20) 110
                        sStrRecord = sStrRecord + String.Format("{0,-20:G}", "");
                        //F11 납입금액 9(10) 120
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));
                        //F12 소득·세액공제금액 9(10) 130
                        sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(Get_Numeric("0")));

                        //F13 투자년도 9(4) 144
                        sStrRecord = sStrRecord + "0000";
                        //F14 투자구분 X(1) 145
                        sStrRecord = sStrRecord + " ";
                        //F15 가입일 9(8) 144
                        sStrRecord = sStrRecord + "00000000";
                        //F16 계약기간 9(2) 
                        sStrRecord = sStrRecord + "00";

                    }

                    iCnt = iCnt + 1;
                    sStrRecord = sStrRecord + String.Format("{0:D2}", iCnt);
                    sStrRecord = sStrRecord + string.Format("{0,-173:G}", "");

                    StrWrReCode.WriteLine(sStrRecord);
                }
            }
        }
        #endregion

        #region  Description : 연말정산 G 레코드 생성( 월세액 )
        private void UP_NTS_RecordG(string sCompy, string sYEAR, string sKBSABUN, Int32 iRowCnt, StreamWriter StrWrReCode)
        {
            int iCnt = 0;
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;
            int iFRecRowCnt = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7BUGB147", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sCompy, sYEAR, sKBSABUN );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (iFRecRowCnt == 0)
                    {
                        //【자료관리번호】
                        //G1 레코드 구분 X(1) 1
                        sStrRecord = "G";
                        //G2 자료구분 9(2) 3
                        sStrRecord = sStrRecord + "20";
                        //G3 세무서코드 X(3) 6
                        sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                        //G4 일련번호 9(6) 12
                        sStrRecord = sStrRecord + String.Format("{0:D6}", iRowCnt);
                        //【원천징수의무자】
                        //G5 ②사업자등록번호 X(10) 22
                        sStrRecord = sStrRecord + fsSAUPNO;
                        //【소득자】
                        //G6 ④소득자 주민등록번호 X(13) 35
                        sStrRecord = sStrRecord + dt.Rows[i]["KBJUMIN"].ToString().Trim().Replace("-", "");
                        //G7 무주택자해당여부 X(2) 37
                        sStrRecord = sStrRecord + "01";
                    }

                    //G8 임대인 성명 X(60) 97
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["MRLESSOR_NAME"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(dt.Rows[i]["MRLESSOR_NAME"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;

                    //G9 임대인 주민등록번호 X(13) 
                    sStrRecord = sStrRecord + dt.Rows[i]["MRLESSOR_JUMIN"].ToString().Trim().Replace("-", "");

                    //G10 유형 X(1) 
                    sStrRecord = sStrRecord + Convert.ToInt16(dt.Rows[i]["MRHUSETYPE"].ToString()).ToString();

                    //G11 계약면적 9(5) 
                    sStrRecord = sStrRecord + String.Format("{0:D5}", Convert.ToInt64(dt.Rows[i]["MRHUSESIZE"].ToString().Trim()));

                    //G12 임대차계약서상주소지 X(150)
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["MRLESSOR_JUSO"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (150 - Encoding.Default.GetByteCount(dt.Rows[i]["MRLESSOR_JUSO"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;

                    //G13 임대차계약기간개시일 9(8)
                    sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[i]["MRCONSDATE"].ToString().Trim()));

                    //G14 임대차계약기간종료일 9(8)
                    sStrRecord = sStrRecord + String.Format("{0:D8}", Convert.ToInt64(dt.Rows[i]["MRCONEDATE"].ToString().Trim()));

                    //G15 연간 월세액 9(10)
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["MRRENTAMOUNT"].ToString().Trim()));

                    //G16 세액공제금액 9(10)
                    sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows[i]["MRDEDAMOUNT"].ToString().Trim()));

                    //G17 대주(貸主) 성명 X(60)
                    sStrTemp = "";
                    sStrTemp = "";
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount("")));
                    sStrRecord = sStrRecord + sStrTemp;

                    //G18 대주주민번호 X(13)
                    sStrRecord = sStrRecord + String.Format("{0,-13:G}", "");
                    //G19 금전 소비대차 계약기간 개시일 9(8)
                    sStrRecord = sStrRecord + "00000000";
                    //G20 금전 소비대차 계약기간 종료일 9(8)
                    sStrRecord = sStrRecord + "00000000";
                    //G21 차입금 이자율 9(4)
                    sStrRecord = sStrRecord + "0000";
                    //G22 원리금 상환액계 9(10)
                    sStrRecord = sStrRecord + "0000000000";
                    //G23 원금 9(10)
                    sStrRecord = sStrRecord + "0000000000";
                    //G24 이자 9(10)
                    sStrRecord = sStrRecord + "0000000000";
                    //G25 공제금액 9(10)
                    sStrRecord = sStrRecord + "0000000000";

                    //G26 임대인 성명 X(60) 97
                    sStrTemp = "";
                    sStrTemp = "";
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount("")));
                    sStrRecord = sStrRecord + sStrTemp;
                    //G27 주민번호(사업자번호) X(13) 97
                    sStrRecord = sStrRecord + String.Format("{0,-13:G}", "");
                    //G28 유형 X(1) 
                    sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");
                    //G29 계약면적 9(5) 
                    sStrRecord = sStrRecord + "00000";
                    //G30 임대차계약서상주소지 X(150)
                    sStrTemp = "";
                    sStrTemp = "";
                    sStrTemp += new String(Convert.ToChar(" "), (150 - Encoding.Default.GetByteCount("")));
                    sStrRecord = sStrRecord + sStrTemp;

                    //G31 임대차계약기간개시일 9(8)
                    sStrRecord = sStrRecord + "00000000";
                    //G32 임대차계약기간종료일 9(8)
                    sStrRecord = sStrRecord + "00000000";
                    //G33 전세보증금 9(10)
                    sStrRecord = sStrRecord + "0000000000";

                    iFRecRowCnt = iFRecRowCnt + 1;

                    if (iFRecRowCnt == 3)
                    {
                        iFRecRowCnt = 0;

                        iCnt = iCnt + 1;

                        sStrRecord = sStrRecord + String.Format("{0:D2}", iCnt);

                        sStrRecord = sStrRecord + string.Format("{0,-12:G}", "");

                        StrWrReCode.WriteLine(sStrRecord);
                    }
                }

                //공백채우기
                if (3 - iFRecRowCnt != 0 && iFRecRowCnt != 0)
                {
                    for (int i = 1; i <= (3 - iFRecRowCnt); i++)
                    {
                        //G8 임대인 성명 X(60) 97
                        sStrTemp = "";
                        sStrTemp = "";
                        sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount("")));
                        sStrRecord = sStrRecord + sStrTemp;

                        //G9 임대인 주민등록번호 X(13) 
                        sStrRecord = sStrRecord + String.Format("{0,-13:G}", "");

                        //G10 유형 X(1) 
                        sStrRecord = sStrRecord + String.Format("{0,-1:G}", "");

                        //G11 계약면적 9(5) 
                        sStrRecord = sStrRecord + "00000";

                        //G12 임대차계약서상주소지 X(150)
                        sStrTemp = "";
                        sStrTemp = "";
                        sStrTemp += new String(Convert.ToChar(" "), (150 - Encoding.Default.GetByteCount("")));
                        sStrRecord = sStrRecord + sStrTemp;

                        //G13 임대차계약기간개시일 9(8)
                        sStrRecord = sStrRecord + "00000000";

                        //G14 임대차계약기간종료일 9(8)
                        sStrRecord = sStrRecord + "00000000";

                        //G15 연간 월세액 9(10)
                        sStrRecord = sStrRecord + "0000000000";

                        //G16 세액공제금액 9(10)
                        sStrRecord = sStrRecord + "0000000000";

                        //G17 대주(貸主) 성명 X(60)
                        sStrRecord = sStrRecord + string.Format("{0,-60:G}", "");
                        //G18 대주주민번호 X(13)
                        sStrRecord = sStrRecord + string.Format("{0,-13:G}", "");
                        //G19 금전 소비대차 계약기간 개시일 9(8)
                        sStrRecord = sStrRecord + "00000000";
                        //G20 금전 소비대차 계약기간 종료일 9(8)
                        sStrRecord = sStrRecord + "00000000";
                        //G21 차입금 이자율 9(4)
                        sStrRecord = sStrRecord + "0000";
                        //G22 원리금 상환액계 9(10)
                        sStrRecord = sStrRecord + "0000000000";
                        //G23 원금 9(10)
                        sStrRecord = sStrRecord + "0000000000";
                        //G24 이자 9(10)
                        sStrRecord = sStrRecord + "0000000000";
                        //G25 공제금액 9(10)
                        sStrRecord = sStrRecord + "0000000000";

                        //G26 임대인 성명 X(60) 97
                        sStrTemp = "";
                        sStrTemp = "";
                        sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount("")));
                        sStrRecord = sStrRecord + sStrTemp;
                        //G27 주민번호(사업자번호) X(13) 97
                        sStrRecord = sStrRecord + string.Format("{0,-13:G}", "");
                        //G28 유형 X(1) 
                        sStrRecord = sStrRecord + string.Format("{0,-1:G}", "");
                        //G29 계약면적 9(5) 
                        sStrRecord = sStrRecord + "00000";
                        //G30 임대차계약서상주소지 X(150)
                        sStrTemp = "";
                        sStrTemp = "";
                        sStrTemp += new String(Convert.ToChar(" "), (150 - Encoding.Default.GetByteCount("")));
                        sStrRecord = sStrRecord + sStrTemp;

                        //G31 임대차계약기간개시일 9(8)
                        sStrRecord = sStrRecord + "00000000";
                        //G32 임대차계약기간종료일 9(8)
                        sStrRecord = sStrRecord + "00000000";
                        //G33 전세보증금 9(10)
                        sStrRecord = sStrRecord + "0000000000";
                    }

                    iCnt = iCnt + 1;
                    sStrRecord = sStrRecord + String.Format("{0:D2}", iCnt);
                    sStrRecord = sStrRecord + string.Format("{0,-12:G}", "");

                    StrWrReCode.WriteLine(sStrRecord);
                }
            }
        }
        #endregion

        #region  Description : 연말정산 H 레코드 생성(기부금조정명세서)
        private void UP_NTS_RecordH(string sCompy, string sYEAR, string sKBSABUN, Int32 iRowCnt, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C7AI201", sCompy, sYEAR, sKBSABUN, "", TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //H1 레코드 구분 X(1) 1 영문 대문자 “F”
                    sStrRecord = "H";
                    //H2 자료구분 9(2) 3
                    sStrRecord = sStrRecord + "20";
                    //H3 세무서코드 X(3) 6
                    sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                    //H4 소득자 일련번호 9(6) 12
                    sStrRecord = sStrRecord + String.Format("{0:D6}", iRowCnt);
                    //【원천징수의무자】
                    //H5 ②사업자등록번호 X(10) 22
                    sStrRecord = sStrRecord + fsSAUPNO;
                    //【소득자】
                    //H6 ④주민등록번호 X(13) 35
                    sStrRecord = sStrRecord + dt.Rows[i]["KBJUMIN"].ToString().Replace("-", "").Trim();
                    //H7 내·외국인 구분코드 9(1) 36
                    sStrRecord = sStrRecord + "1";
                    //H8 ③성명 X(30) 66
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["DASABUNNM"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(dt.Rows[i]["DASABUNNM"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;
                    //H9 ⑦유형 ⑧코드 X(2) 68
                    sStrRecord = sStrRecord + dt.Rows[i]["DADONATION_CD"].ToString().Trim();
                    //H10 기부연도 9(04) 72
                    sStrRecord = sStrRecord + dt.Rows[i]["DADONATION_YEAR"].ToString().Trim();
                    //H11 ⑰기부금액 9(13) 85
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DADEDAMOUNT"].ToString().Trim()));
                    //H12 ⑱전년까지 공제된금액 9(13) 98
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DABFDEDAMOUNT"].ToString().Trim()));
                    //H13 ⑲공제대상금액 (⑰-⑱) 9(13) 111
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DADEDAMOUNT"].ToString().Trim()));
                    //H14 해당 연도 공제금액필요경비 9(13) 124
                    sStrRecord = sStrRecord + "0000000000000";
                    //H15 해당 연도공제금액_세액(소득)공제 9(13) 137
                    //sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DACURRAMOUNT"].ToString().Trim()));

                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DADEDAMOUNT"].ToString().Trim()));

                    //H16 해당 연도에공제받지못한금액_소멸금액  9(13) 150
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DAEXPIREDEDAMOUNT"].ToString().Trim()));
                    //H17 해당 연도에공제받지못한금액_이월금액  9(13) 163
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DATRANSDEDAMOUNT"].ToString().Trim()));
                    //H18 기부금조정명세 일련번호 9(05) 168
                    sStrRecord = sStrRecord + String.Format("{0:D5}", i+1);
                    //H19 공란 X(1725) 1882
                    sStrRecord = sStrRecord + string.Format("{0,-1842:G}", "");

                    StrWrReCode.WriteLine(sStrRecord);
                }
            }
        }
        #endregion

        #region  Description : 연말정산 I 레코드 생성(해당년도기부금명세서)
        private void UP_NTS_RecordI(string sCompy, string sYEAR, string sKBSABUN, Int32 iRowCnt, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C7AG200", sCompy, sYEAR, sKBSABUN, "", TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //I1 레코드 구분 X(1) 1
                    sStrRecord = "I";
                    //I2 자료구분 9(2) 3
                    sStrRecord = sStrRecord + "20";
                    //I3 세무서코드 X(3) 6
                    sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                    //I4 소득자 일련번호 9(6) 12
                    sStrRecord = sStrRecord + String.Format("{0:D6}", iRowCnt);
                    //【원천징수의무자】
                    //I5 ②사업자등록번호 X(10) 22
                    sStrRecord = sStrRecord + fsSAUPNO;
                    //【소득자(연말정산신청자)】
                    //I6  ④소득자주민등록번호 X(13) 35
                    sStrRecord = sStrRecord + dt.Rows[i]["KBJUMIN"].ToString().Replace("-", "").Trim();
                    //I7  ⑦유형 ⑧코드 X(2) 37
                    sStrRecord = sStrRecord + dt.Rows[i]["DOdonation_cd"].ToString().Trim();

                    //I8  ⑨기부내용 X(1) 38
                    sStrRecord = sStrRecord + dt.Rows[i]["DOCONTENT"].ToString().Trim();

                    //【기부처】
                    //I9  ⑪사업자(주민)등록번호  X(13) 51
                    sStrRecord = sStrRecord + string.Format("{0,-13:G}", dt.Rows[i]["DObusnid"].ToString().Trim().Replace("-", "").Trim());

                    //I10 ⑩상호(법인명) X(60) 111
                    sStrTemp = "";
                    sStrTemp = StringTransfer(dt.Rows[i]["DOtrade_nm"].ToString().Trim(), 60);
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(sStrTemp)));
                    sStrRecord = sStrRecord + sStrTemp;

                    //【기부자】 ⑫ - 기부자 정보는 소득공제명세(E) 레코드의
                    //I11 ⑫관계코드 X(1) 112
                    sStrRecord = sStrRecord + dt.Rows[i]["DORelation"].ToString().Trim();
                    //I12 내·외국인 구분코드 X(1) 113
                    sStrRecord = sStrRecord + "1";
                    //I13 ⑫성명 X(30) 143
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["DOName"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(dt.Rows[i]["DOName"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;
                    //I14 ⑫주민등록번호 X(13) 156
                    sStrRecord = sStrRecord + dt.Rows[i]["DOResid"].ToString().Replace("-", "").Trim();
                    //【기부내역】 ⑬,⑭,⑮,⑯
                    //I15 건수 9(5) 161
                    sStrRecord = sStrRecord + String.Format("{0:D5}", Convert.ToInt64(dt.Rows[i]["DOCount"].ToString().Trim()));
                    //I16  ⑬기부금합계금액(⑭+⑮+⑯)  9(13) 174
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DOCONB_SUM"].ToString().Trim()));
                    //I17 ⑭공제대상기부금액 9(13) 187
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DOCONB_SUM"].ToString().Trim()));
                    //I18 ⑮기부장려금신청금액  9(13) 200
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["DOENCAMOUNT"].ToString().Trim()));
                    //I19 ⑯기타 9(13) 213
                    sStrRecord = sStrRecord + "0000000000000";
                    //I20 해당 연도 기부명세일련번호 9(05) 218
                    sStrRecord = sStrRecord + String.Format("{0:D5}", i + 1);
                    //I21 공란 X(1675) 1882
                    //sStrRecord = sStrRecord + string.Format("{0,-1443:G}", "");
                    sStrRecord = sStrRecord + string.Format("{0,-1792:G}", "");

                    StrWrReCode.WriteLine(sStrRecord);
                }
            }
        }
        #endregion

        #region  Description : 연말정산 의료비지급명세 A 레코드 생성
        private void UP_NTS_MedRecordA(string sCompy, string sYEAR, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;

            //의료비공제 받은 대상자 조회
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_81MFE505", sCompy, sYEAR, "", CBO01_KBGUNMU.GetValue().ToString() );
            this.DbConnector.Attach("TY_P_HR_81MFE505", sCompy, sYEAR, "", TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {                   
                    //A1 레코드구분 X(1)
                    sStrRecord = "A";
                    //A2 자료구분 9(2)
                    sStrRecord = sStrRecord + "26";
                    //A3 세무서코드 X(3))
                    sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                    //4 일련번호 9(6) 12
                    sStrRecord = sStrRecord + String.Format("{0:D6}", i + 1);
                    //5 제출년월일 9(8) 20
                    sStrRecord = sStrRecord + DTP01_EDATE.GetString().ToString();
                    //6 사업자등록번호 X(10) 30
                    sStrRecord = sStrRecord + fsSAUPNO;
                    //7 홈택스ID X(20) 50
                    sStrRecord = sStrRecord + string.Format("{0,-20:G}", fsHOMETAXID);
                    //8 세무프로그램코드 X(4) 54
                    sStrRecord = sStrRecord + "9000";

                    //9 귀속년도 X(4) 54
                    sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString();

                    //【원천징수의무자】
                    //10 ④사업자등록번호 X(10) 64 원천징수의무자의 사업자등록번호
                    sStrRecord = sStrRecord + fsSAUPNO;
                    //11 ③상호 X(40) 104
                    sStrTemp = "";
                    sStrTemp = fsSANGHO;
                    sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(fsSANGHO)));
                    sStrRecord = sStrRecord + sStrTemp;
                    //【소득자(연말정산 신청자)】
                    //12  ②소득자 주민등록번호 X(13) 117
                    sStrRecord = sStrRecord + dt.Rows[i]["KBJUMIN"].ToString().Trim();
                    //13 내·외국인 코드 9(1) 118
                    sStrRecord = sStrRecord + "1";
                    //14 ①성명 X(30) 148
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["MDSABUNNM"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(dt.Rows[i]["MDSABUNNM"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;
                    //【지급처】
                    //15 ⑦지급처 사업자등록번호 X(10) 158
                    if (dt.Rows[i]["MDMdCode"].ToString().Trim() == "1")
                    {
                        sStrRecord = sStrRecord + string.Format("{0,-10:G}", "");
                    }
                    else
                    {
                        sStrRecord = sStrRecord + string.Format("{0,-10:G}", dt.Rows[i]["MDbusnid"].ToString().Replace("-", "")); 
                    }
                    //16 ⑧지급처 상호 X(40) 198
                    sStrTemp = "";
                    sStrTemp = dt.Rows[i]["MDtrade_nm"].ToString().Trim();
                    sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(dt.Rows[i]["MDtrade_nm"].ToString().Trim())));
                    sStrRecord = sStrRecord + sStrTemp;
                    //17 ⑨의료증빙코드 X(1) 199
                    sStrRecord = sStrRecord + dt.Rows[i]["MDMdCode"].ToString();
                    //지급명세
                    //18 ⑩건수 9(5) 204
                    sStrRecord = sStrRecord + String.Format("{0:D5}", Convert.ToInt64(dt.Rows[i]["CNT"].ToString().Trim()));
                    //19 ⑪금액 9(11) 215
                    sStrRecord = sStrRecord + String.Format("{0:D11}", Convert.ToInt64(dt.Rows[i]["MDMEDAMOUNT"].ToString().Trim()));

                    //20 ⑫ 미숙아.선천성 9(11) 215                    
                    if (dt.Rows[i]["MDXPRSGN"].ToString() == "2")
                    {
                        sStrRecord = sStrRecord + "1";
                    }
                    else
                    {
                        sStrRecord = sStrRecord + " ";
                    }

                    //21 ⑫난임시술비 해당여부 X(1) 216
                    if (dt.Rows[i]["MDXPRSGN"].ToString() == "1")
                    {
                        sStrRecord = sStrRecord + "1";
                    }
                    else
                    {
                        sStrRecord = sStrRecord + " ";
                    }
                    //【의료비 공제 대상자】
                    //21 ⑤주민등록번호 X(13) 229
                    sStrRecord = sStrRecord + dt.Rows[i]["MDRESID"].ToString().Replace("-", "").Trim();
                    //22 내·외국인 코드 9(1) 230
                    sStrRecord = sStrRecord + "1";
                    //23 ⑥본인 등 해당여부 9(1) 231
                    sStrRecord = sStrRecord + dt.Rows[i]["MDKINGCD"].ToString().Trim();
                    //24 제출대상기간코드 9(1) 232  연간 합산제출 ‘1’, 휴·폐업에 의한 수시제출 ‘2’, 수시 분할제출 ‘3’
                    sStrRecord = sStrRecord + "1";
                    //25 공란 X(19) 251
                    //sStrRecord = sStrRecord + string.Format("{0,-19:G}", "");

                    StrWrReCode.WriteLine(sStrRecord);
                }                
            }          

            
        }
        #endregion

        #region  DesCription : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
