using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Document;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미승인전표 등록 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.09.27 14:31
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23N3M888 : 계정 과목 코드 조회 (O)
    ///  TY_P_AC_23N3L884 : 계정과목 조회(상세)
    ///  TY_P_AC_29C7M958 : 자동순번 가져오기 (O)
    ///  TY_P_AC_2454Y465 : 거래처관리 사업자번호 중복체크 
    ///  TY_P_AC_2B18W972 : 거래처관리 사업자번호 체크
    ///  TY_P_HR_28SAH583 : 조직도 부서코드 체크 (존재유무 체크)
    ///  TY_P_AC_25G2Z500 : ASGRRSF 발생 상태 존재 체크
    ///  TY_P_AC_2AI5O748 : 반제설정 조회 라인번호 (미승인등록)
    ///  TY_P_AC_29C7O959 : 미승인전표 SP호출 이력 저장
    ///  TY_P_AC_29C80960 : 전표생성,전표취소 처리 SP
    ///  TY_P_AC_29D5B004 : 전표호출 파라메타 파일 조회
    ///  TY_P_AC_2AJ9G760 : 외화정리 조회(미승인 등록)
    ///  TY_P_AC_2AM4G781 :년예산 등록 체크(미승인 등록)
    ///  TY_P_AC_2AM4H782 : 월예산 등록 체크(미승인 등록)
    ///  TY_P_AC_2AM4H783 : 본예산 합계(미승인 등록)
    ///  TY_P_AC_2AM4I784 :수정예산 합계(미승인 등록)
    ///  TY_P_AC_2AM4J785 : 기간동안 발행된 전표금액 합산(미승인 등록)
    ///  TY_P_AC_2AM4J786 : 현재 불러온 전표에 대한 합계(미승인 등록)
    ///  TY_P_AC_2AM4K787 : 현재 편집 중인 미승인전표 대한 합계(미승인 등록)
    ///  TY_P_AC_2AM4K788 : 임시화일중 line 번호에 대한 값(미승인 등록)
    ///  TY_P_AC_2ANBH793 : 년 예산 파일 존재 체크
    ///  TY_P_AC_2ANBX794 : 기타예산세목 조회 (미승인 등록)
    ///  TY_P_AC_2ANBX795 : 여비교통비　예산세목 조회 (미승인 등록)
    ///  TY_P_AC_2ANBY797 : 소모품비　예산세목 조회 (미승인 등록)
    ///  
    ///  TY_P_AC_29S1X350 : 미승인전표 임시화일 조회(그리드_요약)  (O)
    ///  TY_P_AC_29S1G346 : 미승인전표 임시화일 조회 (TMAC1102F 상세)
    ///  TY_P_AC_2AB3H673 : 미승인전표 조회(미승인등록 ADLSLGLF) (부서,일자,번호)
    ///  TY_P_AC_2AI5H746 : 미승인전표 조회 라인 (미승인등록)ADLSLGLF(부서,일자,번호,순번)
    ///  
    ///  TY_P_AC_29C7K957 : 미승인전표 임시파일 입력 (미승인 -> 임시파일 등록 ADSLGLF -> TMAC1102F)
    ///  
    ///  TY_P_AC_2AB2O670 : 미승인전표 임시화일 삭제(TMAC1102F)
    ///  TY_P_AC_2AB4S685 : 미승인전표 임시화일 전체삭제(TMAC1102F)
    ///  TY_P_AC_2AB2P672 : 미승인전표 임시화일 수정(TMAC1102)
    ///  TY_P_AC_29DA5966 : 미승인전표 임시파일 등록(TMAC1102)  (O)
    ///  TY_P_AC_2AJ96759 : 미승인전표 외화처리 처리(대변-임시자료기준)
    /// 
    ///  TY_P_AC_2AB4L678 : 접대비 임시화일 등록(TMAC1102SF)
    ///  TY_P_AC_2AB4M680 : 접대비 임시화일 삭제(TMAC1102SF)
    ///  TY_P_AC_2AB4T687 : 접대비 임시화일 전체삭제(TMAC1102SF)
    ///  TY_P_AC_2AB4M679 : 접대비 임시화일 수정(TMAC1102SF)
    ///  TY_P_AC_2AB4K677 : 접대비 임시화일 조회(TMAC1102SF)
    ///  TY_P_AC_2AH8K737 : 접대비 임시화일 조회 접대비번호 (TMAC1102SF)
    
    ///  
    ///  TY_P_AC_2AB4N682 : 외화관리 임시화일 등록(TMAC1102WF)
    ///  TY_P_AC_2AB4O684 : 외화관리 임시화일 삭제(TMAC1102WF)
    ///  TY_P_AC_2AB4U688 : 외화관리 임시화일 전체삭제(TMAC1102WF)
    ///  TY_P_AC_2AB4N683 : 외화관리 임시화일 수정(TMAC1102WF)
    ///  TY_P_AC_2AB4N681 : 외화관리 임시화일 조회(TMAC1102WF)
    ///  
    ///  TY_P_AC_2AB5P690 : 접대비 조회(미승인)
    ///  TY_P_AC_2AB6A696 : 외화설정 조회(미승인등록)
    ///  TY_P_AC_2AC1N706 : 입금표 임시화일 등록(TMAC1151REF)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29S1V349 : 미승인전표 임시화일 조회(그리드)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  B2CDAC : 계정코드
    ///  B2DPMK : 작성부서
    ///  B2HISAB : 작성사번
    ///  B2IDJP :  전표구분
    ///  B2DTMK : 작성일자
    ///  B2AMCR :  대변금액
    ///  B2AMDR :  차변금액
    ///  B2NOLN : LINE SEQ.
    ///  B2NOSQ :  일련번호
    ///  B2RKAC :  적요
    ///  B2RKCU :  상대처
    ///  B2WCJP :  원천전표번호
    /// </summary>
    public partial class TYACBJ001I : TYBase
    {
        #region 팝업에서 부모창 메소드 호출 
        private delegate void PopupClosingDelegate(TYCodeBox codeBox);
        private PopupClosingDelegate _popupClosingDelegate;

        private string fsATCRGB = string.Empty; // 자동전표발행(A), 미승이전표발행(J)

        public void PopupClosing(TYCodeBox codeBox)
        {
            if (codeBox == null)
                return;

            this._popupClosingDelegate = new PopupClosingDelegate(this.PopupClosingByDelegate);
            this.Invoke(this._popupClosingDelegate, codeBox);
        }
        // 코드 박스 처리 이후  ---> PopupClosingByDelegate()
        private void PopupClosingByDelegate(TYCodeBox codeBox)
        {
            //this.ShowCustomMessage(codeBox.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UP_SetGridMaster();
        }
        #endregion

        #region 화면 펑션키 정의 ---> ProcessCmdKey()
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if (keyData == Keys.F10) // 전표 생성
            //{
            //    this.BTN61_SAV_ProcessCheck(null, null);
            //    this.BTN61_SAV_Click(null, null);

            //    return true;
            //}

            //if (keyData == Keys.F23) // 전표 취소
            //{
            //    this.BTN61_CANCEL_ProcessCheck(null, null);
            //    this.BTN61_CANCEL_Click(null, null);

            //    return true;
            //}

            //if (keyData == Keys.F6) // 전표 출력
            //{
            //    this.BTN61_PRT_ProcessCheck(null, null);
            //    this.BTN61_PRT_Click(null, null);

            //    return true;
            //}

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        bool _Isloaded = false;
        bool fbRtnFlag = false;

        private string fsSessionId = string.Empty;
        private string HiddenOK;

        #region Descriontion : 미승인 전표 Data 변수 정의
        private TYData DAT02_W2SSID;
        private TYData DAT02_W2DPMK;
        private TYData DAT02_W2DTMK;
        private TYData DAT02_W2NOSQ;
        private TYData DAT02_W2NOLN;
        private TYData DAT02_W2IDJP;
        private TYData DAT02_W2NOJP;
        private TYData DAT02_W2CDAC;
        private TYData DAT02_W2DTAC;
        private TYData DAT02_W2DTLI;
        private TYData DAT02_W2DPAC;
        private TYData DAT02_W2CDMI1;
        private TYData DAT02_W2VLMI1;
        private TYData DAT02_W2CDMI2;
        private TYData DAT02_W2VLMI2;
        private TYData DAT02_W2CDMI3;
        private TYData DAT02_W2VLMI3;
        private TYData DAT02_W2CDMI4;
        private TYData DAT02_W2VLMI4;
        private TYData DAT02_W2CDMI5;
        private TYData DAT02_W2VLMI5;
        private TYData DAT02_W2CDMI6;
        private TYData DAT02_W2VLMI6;
        private TYData DAT02_W2AMDR;
        private TYData DAT02_W2AMCR;
        private TYData DAT02_W2CDFD;
        private TYData DAT02_W2AMFD;
        private TYData DAT02_W2RKAC;
        private TYData DAT02_W2RKCU;
        private TYData DAT02_W2WCJP;
        private TYData DAT02_W2PRGB;
        private TYData DAT02_W2HIGB;
        //private TYData DAT02_W2HIDAT;
        //private TYData DAT02_W2HITIM;
        private TYData DAT02_W2HISAB;
        private TYData DAT02_W2GUBUN;
        private TYData DAT02_W2TXAMT;
        private TYData DAT02_W2TXVAT;
        private TYData DAT02_W2HWAJU;

        //private TYData DAT02_W2HANG;
        //private TYData DAT02_W2SAPP; 
        #endregion

        #region Descriontion : 접대비 Data 변수 정의
        private TYData DAT03_TSJPSSID;
        private TYData DAT03_TSJPDPMK;
        private TYData DAT03_TSJPDTMK;
        private TYData DAT03_TSJPNO_SQ;
        private TYData DAT03_TSJPNO_LN;
        private TYData DAT03_TSDTYY;
        private TYData DAT03_TSDTMM;
        private TYData DAT03_TSNOSQ;
        private TYData DAT03_TSDTOC;
        private TYData DAT03_TSDEID;
        private TYData DAT03_TSNOCL;
        private TYData DAT03_TSNMCP;
        private TYData DAT03_TSADCL;
        private TYData DAT03_TSNMRP;
        private TYData DAT03_TSNOCC;
        private TYData DAT03_TSREMK;
        private TYData DAT03_TSNOMK;
        private TYData DAT03_TSAMSE;
        private TYData DAT03_TSCGSE;
        private TYData DAT03_TSJPNO_1;
        private TYData DAT03_TSGUBN; 
        #endregion
        
        #region Descriontion : 외화관리 Data 변수 정의
        private TYData DAT04_TWJPSSID;
        private TYData DAT04_TWJPDPMK;
        private TYData DAT04_TWJPDTMK;
        private TYData DAT04_TWJPNOSQ;
        private TYData DAT04_TWJPNOLN;
        private TYData DAT04_TWYEAR;
        private TYData DAT04_TWNOSQ;
        private TYData DAT04_TWBANK;
        private TYData DAT04_TWGUJA;
        private TYData DAT04_TWGUIP;
        private TYData DAT04_TWGUCD;
        private TYData DAT04_TWGONG;
        private TYData DAT04_TWGUBN;
        private TYData DAT04_TWYUL;
        private TYData DAT04_TWIAMT; 
        #endregion

        #region Descriontion : 입금표관리 Data 변수 정의
        private TYData DAT05_TRSSID;
        private TYData DAT05_TRJPDPMK;
        private TYData DAT05_TRJPDTMK;
        private TYData DAT05_TRJPNOSQ;
        private TYData DAT05_TRJPNOLN;
        private TYData DAT05_TRDPMK;
        private TYData DAT05_TRYEAR;
        private TYData DAT05_TRCDSB;
        private TYData DAT05_TRSEQ;
        private TYData DAT05_TRVEND;
        private TYData DAT05_TRAMT;
        private TYData DAT05_TRRKAC;
        #endregion

        #region Descriontion : 불공제 Data 변수 정의
        private TYData DAT06_TSBPSSID;
        private TYData DAT06_TSBPDPMK;
        private TYData DAT06_TSBPDTMK;
        private TYData DAT06_TSBPNOSQ;
        private TYData DAT06_TSBPNOLN;
        private TYData DAT06_TSBCDTX;
        private TYData DAT06_TSBGUBN;
        private TYData DAT06_TSBAMT;
        private TYData DAT06_TSBVAT;
        #endregion

        #region Descriontion : 무역원가 Data 변수 정의
        private TYData DAT07_LCSSID;
        private TYData DAT07_LCGUBN;
        private TYData DAT07_LCCHTEAM;
        private TYData DAT07_LCCHSABN;
        private TYData DAT07_LCCHYYNO;
        private TYData DAT07_LCCHSQNO;
        private TYData DAT07_LCCHDEPT;
        private TYData DAT07_LCCHILJA;
        private TYData DAT07_LCCHJPNO;
        private TYData DAT07_LCCHJPSQ;
        private TYData DAT07_LCPUMMOK;
        private TYData DAT07_LCRECODE;
        private TYData DAT07_LCCHMICD;
        private TYData DAT07_LCCHACCD;
        private TYData DAT07_LCCHAMT;
        private TYData DAT07_LCCHGUBN;
        private TYData DAT07_LCBLGUBN;
        private TYData DAT07_LCBLNUMB;
        private TYData DAT07_LCACCEPT;
        private TYData DAT07_LCRKAC;
        private TYData DAT07_LCHIGUBN;
        //private TYData DAT07_LCHIDATE;
        //private TYData DAT07_LCHITIME;
        #endregion

        /*  관리항목코드  */
        private string txtfsA1CDMI1 = string.Empty, txtfsA1CDMI2 = string.Empty, txtfsA1CDMI3 = string.Empty, txtfsA1CDMI4 = string.Empty, txtfsA1CDMI5 = string.Empty, txtfsA1CDMI6 = string.Empty;

        /*  계정과목 관련 각종 코드 변수 */
        private string txtfsA1TAG01 = string.Empty;    /* 19-차/대        */
        private string txtfsA1TAG02 = string.Empty;    /* 20-전표계정     */
        private string txtfsA1TAG03 = string.Empty;    /* 21-관리대장KEY  */
        private string txtfsA1TAG04 = string.Empty;    /* 22-기간비용정리 */
        private string txtfsA1TAG05 = string.Empty;    /* 23-자금관리     */
        private string txtfsA1TAG06 = string.Empty;    /* 24-예산통제여부 */
        private string txtfsA1TAG07 = string.Empty;    /* 25-반제관리     */
        private string txtfsA1TAG08 = string.Empty;    /* 26-잔액명세서출력*/
        private string txtfsA1TAG09 = string.Empty;    /* 27-접대비        */
        private string txtfsA1TAG10 = string.Empty;    /* 28-충당금        */
        private string txtfsA1TAG11 = string.Empty;    /* 29-반제연결      */

        private string txtfsA1OTMI1 = string.Empty, txtfsA1OTMI2 = string.Empty, txtfsA1OTMI3 = string.Empty, txtfsA1OTMI4 = string.Empty, txtfsA1OTMI5 = string.Empty, txtfsA1OTMI6 = string.Empty;

        private string txtJunPyoGubn = string.Empty;

        private string fsCDMI01 = string.Empty;
        private string fsCDMI02 = string.Empty;
        private string fsCDMI03 = string.Empty;
        private string fsCDMI04 = string.Empty;
        private string fsCDMI05 = string.Empty;
        private string fsCDMI06 = string.Empty;

        private string fsVLMI01 = string.Empty;
        private string fsVLMI02 = string.Empty;
        private string fsVLMI03 = string.Empty;
        private string fsVLMI04 = string.Empty;
        private string fsVLMI05 = string.Empty;
        private string fsVLMI06 = string.Empty;

        private string fsTabCtl = string.Empty; // Tab Control
        
        #region Descriotion : 관리항목 관련 변수 정의(관리 항목 1 ~ 6)
        TYCodeBox CBH10_VALUE01; // 거래처 코드
        TYCodeBox CBH11_VALUE01;
        TYCodeBox CBH12_VALUE01;
        TYCodeBox CBH13_VALUE01;
        TYCodeBox CBH14_VALUE01;
        TYCodeBox CBH15_VALUE01;

        TYCodeBox CBH10_VALUE03; // 부서코드
        TYCodeBox CBH11_VALUE03;
        TYCodeBox CBH12_VALUE03;
        TYCodeBox CBH13_VALUE03;
        TYCodeBox CBH14_VALUE03;
        TYCodeBox CBH15_VALUE03;

        TYTextBox TXT10_VALUE04; // EL NO
        TYTextBox TXT11_VALUE04;
        TYTextBox TXT12_VALUE04;
        TYTextBox TXT13_VALUE04;
        TYTextBox TXT14_VALUE04;
        TYTextBox TXT15_VALUE04;

        TYCodeBox CBH10_VALUE05; // 사원번호
        TYCodeBox CBH11_VALUE05;
        TYCodeBox CBH12_VALUE05;
        TYCodeBox CBH13_VALUE05;
        TYCodeBox CBH14_VALUE05;
        TYCodeBox CBH15_VALUE05;

        TYTextBox TXT10_VALUE06; // 과세표준
        TYTextBox TXT11_VALUE06;
        TYTextBox TXT12_VALUE06;
        TYTextBox TXT13_VALUE06;
        TYTextBox TXT14_VALUE06;
        TYTextBox TXT15_VALUE06;

        TYCodeBox CBH10_VALUE07; // 계좌번호
        TYCodeBox CBH11_VALUE07;
        TYCodeBox CBH12_VALUE07;
        TYCodeBox CBH13_VALUE07;
        TYCodeBox CBH14_VALUE07;
        TYCodeBox CBH15_VALUE07;

        TYTextBox TXT10_VALUE08; // 관리번호(채권번호) 지역개발공채
        TYTextBox TXT11_VALUE08;
        TYTextBox TXT12_VALUE08;
        TYTextBox TXT13_VALUE08;
        TYTextBox TXT14_VALUE08;
        TYTextBox TXT15_VALUE08;

        TYCodeBox CBH10_VALUE09; // 어음번호 (지어)
        TYCodeBox CBH11_VALUE09;
        TYCodeBox CBH12_VALUE09;
        TYCodeBox CBH13_VALUE09;
        TYCodeBox CBH14_VALUE09;
        TYCodeBox CBH15_VALUE09;

        TYCodeBox CBH10_VALUE10; // LC NO  
        TYCodeBox CBH11_VALUE10;
        TYCodeBox CBH12_VALUE10;
        TYCodeBox CBH13_VALUE10;
        TYCodeBox CBH14_VALUE10;
        TYCodeBox CBH15_VALUE10;

        TYCodeBox CBH10_VALUE11; // 세무구분 
        TYCodeBox CBH11_VALUE11;
        TYCodeBox CBH12_VALUE11;
        TYCodeBox CBH13_VALUE11;
        TYCodeBox CBH14_VALUE11;
        TYCodeBox CBH15_VALUE11;

        //TYTextBox TXT10_VALUE10; // LC NO   (텍스트 박스로 변경)
        //TYTextBox TXT11_VALUE10;
        //TYTextBox TXT12_VALUE10;
        //TYTextBox TXT13_VALUE10;
        //TYTextBox TXT14_VALUE10;
        //TYTextBox TXT15_VALUE10;

        TYTextBox TXT10_VALUE12; // 차입금번호(회차순번) 시설자금
        TYTextBox TXT11_VALUE12;
        TYTextBox TXT12_VALUE12;
        TYTextBox TXT13_VALUE12;
        TYTextBox TXT14_VALUE12;
        TYTextBox TXT15_VALUE12;

        TYTextBox TXT10_VALUE13; // 전화번호
        TYTextBox TXT11_VALUE13;
        TYTextBox TXT12_VALUE13;
        TYTextBox TXT13_VALUE13;
        TYTextBox TXT14_VALUE13;
        TYTextBox TXT15_VALUE13;

        TYTextBox TXT10_VALUE14; // 공급가액
        TYTextBox TXT11_VALUE14;
        TYTextBox TXT12_VALUE14;
        TYTextBox TXT13_VALUE14;
        TYTextBox TXT14_VALUE14;
        TYTextBox TXT15_VALUE14;

        TYTextBox TXT10_VALUE15; // 거래일자
        TYTextBox TXT11_VALUE15;
        TYTextBox TXT12_VALUE15;
        TYTextBox TXT13_VALUE15;
        TYTextBox TXT14_VALUE15;
        TYTextBox TXT15_VALUE15;

        TYTextBox TXT10_VALUE16; // 기간
        TYTextBox TXT11_VALUE16;
        TYTextBox TXT12_VALUE16;
        TYTextBox TXT13_VALUE16;
        TYTextBox TXT14_VALUE16;
        TYTextBox TXT15_VALUE16;

        TYTextBox TXT10_VALUE17; // 자동차번호
        TYTextBox TXT11_VALUE17;
        TYTextBox TXT12_VALUE17;
        TYTextBox TXT13_VALUE17;
        TYTextBox TXT14_VALUE17;
        TYTextBox TXT15_VALUE17;

        TYTextBox TXT10_VALUE18; // 회차
        TYTextBox TXT11_VALUE18;
        TYTextBox TXT12_VALUE18;
        TYTextBox TXT13_VALUE18;
        TYTextBox TXT14_VALUE18;
        TYTextBox TXT15_VALUE18;

        TYTextBox TXT10_VALUE19; // 고지번호
        TYTextBox TXT11_VALUE19;
        TYTextBox TXT12_VALUE19;
        TYTextBox TXT13_VALUE19;
        TYTextBox TXT14_VALUE19;
        TYTextBox TXT15_VALUE19;

        TYTextBox TXT10_VALUE20; // 이율(%)
        TYTextBox TXT11_VALUE20;
        TYTextBox TXT12_VALUE20;
        TYTextBox TXT13_VALUE20;
        TYTextBox TXT14_VALUE20;
        TYTextBox TXT15_VALUE20;

        TYTextBox TXT10_VALUE21; // 외화금액
        TYTextBox TXT11_VALUE21;
        TYTextBox TXT12_VALUE21;
        TYTextBox TXT13_VALUE21;
        TYTextBox TXT14_VALUE21;
        TYTextBox TXT15_VALUE21;

        TYTextBox TXT10_VALUE23; // 예,적금 계정코드
        TYTextBox TXT11_VALUE23;
        TYTextBox TXT12_VALUE23;
        TYTextBox TXT13_VALUE23;
        TYTextBox TXT14_VALUE23;
        TYTextBox TXT15_VALUE23;

        TYCodeBox CBH10_VALUE26; // 계정코드
        TYCodeBox CBH11_VALUE26;
        TYCodeBox CBH12_VALUE26;
        TYCodeBox CBH13_VALUE26;
        TYCodeBox CBH14_VALUE26;
        TYCodeBox CBH15_VALUE26;

        //TYCodeBox CBH10_VALUE27; // 접대비번호
        //TYCodeBox CBH11_VALUE27;
        //TYCodeBox CBH12_VALUE27;
        //TYCodeBox CBH13_VALUE27;
        //TYCodeBox CBH14_VALUE27;
        //TYCodeBox CBH15_VALUE27;

        TYTextBox TXT10_VALUE27; // 접대비번호(텍스트 박스로 변경)
        TYTextBox TXT11_VALUE27;
        TYTextBox TXT12_VALUE27;
        TYTextBox TXT13_VALUE27;
        TYTextBox TXT14_VALUE27;
        TYTextBox TXT15_VALUE27;

        TYTextBox TXT10_VALUE28; // 년월일확인
        TYTextBox TXT11_VALUE28;
        TYTextBox TXT12_VALUE28;
        TYTextBox TXT13_VALUE28;
        TYTextBox TXT14_VALUE28;
        TYTextBox TXT15_VALUE28;

        TYCodeBox CBH10_VALUE29; // 어음번호 (받어)
        TYCodeBox CBH11_VALUE29;
        TYCodeBox CBH12_VALUE29;
        TYCodeBox CBH13_VALUE29;
        TYCodeBox CBH14_VALUE29;
        TYCodeBox CBH15_VALUE29;


        TYTextBox TXT10_VALUE31; // 수출외환
        TYTextBox TXT11_VALUE31;
        TYTextBox TXT12_VALUE31;
        TYTextBox TXT13_VALUE31;
        TYTextBox TXT14_VALUE31;
        TYTextBox TXT15_VALUE31;

        TYCodeBox CBH10_VALUE32; // 신용카드NO
        TYCodeBox CBH11_VALUE32;
        TYCodeBox CBH12_VALUE32;
        TYCodeBox CBH13_VALUE32;
        TYCodeBox CBH14_VALUE32;
        TYCodeBox CBH15_VALUE32;

        TYTextBox TXT10_VALUE33; // 일자확인
        TYTextBox TXT11_VALUE33;
        TYTextBox TXT12_VALUE33;
        TYTextBox TXT13_VALUE33;
        TYTextBox TXT14_VALUE33;
        TYTextBox TXT15_VALUE33;

        TYCodeBox CBH10_VALUE34; // 품목코드 
        TYCodeBox CBH11_VALUE34;
        TYCodeBox CBH12_VALUE34;
        TYCodeBox CBH13_VALUE34;
        TYCodeBox CBH14_VALUE34;
        TYCodeBox CBH15_VALUE34;

        //TYTextBox TXT10_VALUE34; // 품목코드(텍스트 박스로 변경)
        //TYTextBox TXT11_VALUE34;
        //TYTextBox TXT12_VALUE34;
        //TYTextBox TXT13_VALUE34;
        //TYTextBox TXT14_VALUE34;
        //TYTextBox TXT15_VALUE34;

        TYCodeBox CBH10_VALUE35; // 예산세목
        TYCodeBox CBH11_VALUE35;
        TYCodeBox CBH12_VALUE35;
        TYCodeBox CBH13_VALUE35;
        TYCodeBox CBH14_VALUE35;
        TYCodeBox CBH15_VALUE35;

        TYTextBox TXT10_VALUE36; // 환율
        TYTextBox TXT11_VALUE36;
        TYTextBox TXT12_VALUE36;
        TYTextBox TXT13_VALUE36;
        TYTextBox TXT14_VALUE36;
        TYTextBox TXT15_VALUE36;

        TYCodeBox CBH10_VALUE41; // 외화관리번호
        TYCodeBox CBH11_VALUE41;
        TYCodeBox CBH12_VALUE41;
        TYCodeBox CBH13_VALUE41;
        TYCodeBox CBH14_VALUE41;
        TYCodeBox CBH15_VALUE41;


        TYCodeBox CBH10_VALUE42; // 화일번호
        TYCodeBox CBH11_VALUE42;
        TYCodeBox CBH12_VALUE42;
        TYCodeBox CBH13_VALUE42;
        TYCodeBox CBH14_VALUE42;
        TYCodeBox CBH15_VALUE42;

        //TYTextBox TXT10_VALUE42; // 화일번호(텍스트 박스로 변경)
        //TYTextBox TXT11_VALUE42;
        //TYTextBox TXT12_VALUE42;
        //TYTextBox TXT13_VALUE42;
        //TYTextBox TXT14_VALUE42;
        //TYTextBox TXT15_VALUE42;

        TYCodeBox CBH10_VALUE44; // B/L 번호
        TYCodeBox CBH11_VALUE44;
        TYCodeBox CBH12_VALUE44;
        TYCodeBox CBH13_VALUE44;
        TYCodeBox CBH14_VALUE44;
        TYCodeBox CBH15_VALUE44;

        //TYTextBox TXT10_VALUE44; // B/L 번호(텍스트 박스로 변경)
        //TYTextBox TXT11_VALUE44;
        //TYTextBox TXT12_VALUE44;
        //TYTextBox TXT13_VALUE44;
        //TYTextBox TXT14_VALUE44;
        //TYTextBox TXT15_VALUE44;

        TYCodeBox CBH10_VALUE37; // 출장번호
        TYCodeBox CBH11_VALUE37;
        TYCodeBox CBH12_VALUE37;
        TYCodeBox CBH13_VALUE37;
        TYCodeBox CBH14_VALUE37;
        TYCodeBox CBH15_VALUE37;

        TYCodeBox CBH10_VALUE38; // 자산관리번호
        TYCodeBox CBH11_VALUE38;
        TYCodeBox CBH12_VALUE38;
        TYCodeBox CBH13_VALUE38;
        TYCodeBox CBH14_VALUE38;
        TYCodeBox CBH15_VALUE38;

        TYTextBox TXT10_VALUE47; // 수출신고번호
        TYTextBox TXT11_VALUE47;
        TYTextBox TXT12_VALUE47;
        TYTextBox TXT13_VALUE47;
        TYTextBox TXT14_VALUE47;
        TYTextBox TXT15_VALUE47;

        TYTextBox TXT10_VALUE49;  //입항일자
        TYTextBox TXT11_VALUE49;
        TYTextBox TXT12_VALUE49;
        TYTextBox TXT13_VALUE49;
        TYTextBox TXT14_VALUE49;
        TYTextBox TXT15_VALUE49;

        TYCodeBox CBH10_VALUE50; // 본선
        TYCodeBox CBH11_VALUE50;
        TYCodeBox CBH12_VALUE50;
        TYCodeBox CBH13_VALUE50;
        TYCodeBox CBH14_VALUE50;
        TYCodeBox CBH15_VALUE50;

        TYCodeBox CBH10_VALUE51; // 화주
        TYCodeBox CBH11_VALUE51;
        TYCodeBox CBH12_VALUE51;
        TYCodeBox CBH13_VALUE51;
        TYCodeBox CBH14_VALUE51;
        TYCodeBox CBH15_VALUE51;

        TYTextBox TXT10_VALUE52;  //납부기한
        TYTextBox TXT11_VALUE52;
        TYTextBox TXT12_VALUE52;
        TYTextBox TXT13_VALUE52;
        TYTextBox TXT14_VALUE52;
        TYTextBox TXT15_VALUE52;

        TYTextBox TXT10_VALUE53;  //입고번호
        TYTextBox TXT11_VALUE53;
        TYTextBox TXT12_VALUE53;
        TYTextBox TXT13_VALUE53;
        TYTextBox TXT14_VALUE53;
        TYTextBox TXT15_VALUE53;


        //공통
        TYCodeBox CBH10_B2INDX;
        TYCodeBox CBH11_B2INDX;
        TYCodeBox CBH12_B2INDX;
        TYCodeBox CBH13_B2INDX;
        TYCodeBox CBH14_B2INDX;
        TYCodeBox CBH15_B2INDX;
        #endregion

        private string _DPMK;
        private string _DTMK;
        private string _NOSQ;

        public TYACBJ001I(string LDPMK, string LDTMK, string LNOSQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._DPMK = LDPMK;
            this._DTMK = LDTMK;
            this._NOSQ = LNOSQ;

            //this.SetFocus(PAN10_VLMI1.CurControl);  

            this.ButtonTabIndexLast = false;

            #region Description : 미승인 저장 변수 정의
            this.DAT02_W2SSID = new TYData("DAT02_W2SSID", null);
            this.DAT02_W2DPMK = new TYData("DAT02_W2DPMK", null);
            this.DAT02_W2DTMK = new TYData("DAT02_W2DTMK", null);
            this.DAT02_W2NOSQ = new TYData("DAT02_W2NOSQ", null);
            this.DAT02_W2NOLN = new TYData("DAT02_W2NOLN", null);
            this.DAT02_W2IDJP = new TYData("DAT02_W2IDJP", null);
            this.DAT02_W2NOJP = new TYData("DAT02_W2NOJP", null);
            this.DAT02_W2CDAC = new TYData("DAT02_W2CDAC", null);
            this.DAT02_W2DTAC = new TYData("DAT02_W2DTAC", null);
            this.DAT02_W2DTLI = new TYData("DAT02_W2DTLI", null);
            this.DAT02_W2DPAC = new TYData("DAT02_W2DPAC", null);
            this.DAT02_W2CDMI1 = new TYData("DAT02_W2CDMI1", null);
            this.DAT02_W2VLMI1 = new TYData("DAT02_W2VLMI1", null);
            this.DAT02_W2CDMI2 = new TYData("DAT02_W2CDMI2", null);
            this.DAT02_W2VLMI2 = new TYData("DAT02_W2VLMI2", null);
            this.DAT02_W2CDMI3 = new TYData("DAT02_W2CDMI3", null);
            this.DAT02_W2VLMI3 = new TYData("DAT02_W2VLMI3", null);
            this.DAT02_W2CDMI4 = new TYData("DAT02_W2CDMI4", null);
            this.DAT02_W2VLMI4 = new TYData("DAT02_W2VLMI4", null);
            this.DAT02_W2CDMI5 = new TYData("DAT02_W2CDMI5", null);
            this.DAT02_W2VLMI5 = new TYData("DAT02_W2VLMI5", null);
            this.DAT02_W2CDMI6 = new TYData("DAT02_W2CDMI6", null);
            this.DAT02_W2VLMI6 = new TYData("DAT02_W2VLMI6", null);
            this.DAT02_W2AMDR = new TYData("DAT02_W2AMDR", null);
            this.DAT02_W2AMCR = new TYData("DAT02_W2AMCR", null);
            this.DAT02_W2CDFD = new TYData("DAT02_W2CDFD", null);
            this.DAT02_W2AMFD = new TYData("DAT02_W2AMFD", null);
            this.DAT02_W2RKAC = new TYData("DAT02_W2RKAC", null);
            this.DAT02_W2RKCU = new TYData("DAT02_W2RKCU", null);
            this.DAT02_W2WCJP = new TYData("DAT02_W2WCJP", null);
            this.DAT02_W2PRGB = new TYData("DAT02_W2PRGB", null);
            this.DAT02_W2HIGB = new TYData("DAT02_W2HIGB", null);
            //this.DAT02_W2HIDAT = new TYData("DAT02_W2HIDAT", null);
            //this.DAT02_W2HITIM = new TYData("DAT02_W2HITIM", null);
            this.DAT02_W2HISAB = new TYData("DAT02_W2HISAB", null);
            this.DAT02_W2GUBUN = new TYData("DAT02_W2GUBUN", null);
            this.DAT02_W2TXAMT = new TYData("DAT02_W2TXAMT", null);
            this.DAT02_W2TXVAT = new TYData("DAT02_W2TXVAT", null);
            this.DAT02_W2HWAJU = new TYData("DAT02_W2HWAJU", null);
            #endregion

            #region  Description : 접대비 저장 변수 정의
            this.DAT03_TSJPSSID = new TYData("DAT03_TSJPSSID", null);
            this.DAT03_TSJPDPMK = new TYData("DAT03_TSJPDPMK", null);
            this.DAT03_TSJPDTMK = new TYData("DAT03_TSJPDTMK", null);
            this.DAT03_TSJPNO_SQ = new TYData("DAT03_TSJPNO_SQ", null);
            this.DAT03_TSJPNO_LN = new TYData("DAT03_TSJPNO_LN", null);
            this.DAT03_TSDTYY = new TYData("DAT03_TSDTYY", null);
            this.DAT03_TSDTMM = new TYData("DAT03_TSDTMM", null);
            this.DAT03_TSNOSQ = new TYData("DAT03_TSNOSQ", null);
            this.DAT03_TSDTOC = new TYData("DAT03_TSDTOC", null);
            this.DAT03_TSDEID = new TYData("DAT03_TSDEID", null);
            this.DAT03_TSNOCL = new TYData("DAT03_TSNOCL", null);
            this.DAT03_TSNMCP = new TYData("DAT03_TSNMCP", null);
            this.DAT03_TSADCL = new TYData("DAT03_TSADCL", null);
            this.DAT03_TSNMRP = new TYData("DAT03_TSNMRP", null);
            this.DAT03_TSNOCC = new TYData("DAT03_TSNOCC", null);
            this.DAT03_TSREMK = new TYData("DAT03_TSREMK", null);
            this.DAT03_TSNOMK = new TYData("DAT03_TSNOMK", null);
            this.DAT03_TSAMSE = new TYData("DAT03_TSAMSE", null);
            this.DAT03_TSCGSE = new TYData("DAT03_TSCGSE", null);
            this.DAT03_TSJPNO_1 = new TYData("DAT03_TSJPNO_1", null);
            this.DAT03_TSGUBN = new TYData("DAT03_TSGUBN", null); 
            #endregion

            #region Description : 외화관리 저장 변수 정의
            this.DAT04_TWJPSSID = new TYData("DAT04_TWJPSSID", null);
            this.DAT04_TWJPDPMK = new TYData("DAT04_TWJPDPMK", null);
            this.DAT04_TWJPDTMK = new TYData("DAT04_TWJPDTMK", null);
            this.DAT04_TWJPNOSQ = new TYData("DAT04_TWJPNOSQ", null);
            this.DAT04_TWJPNOLN = new TYData("DAT04_TWJPNOLN", null);
            this.DAT04_TWYEAR = new TYData("DAT04_TWYEAR", null);
            this.DAT04_TWNOSQ = new TYData("DAT04_TWNOSQ", null);
            this.DAT04_TWBANK = new TYData("DAT04_TWBANK", null);
            this.DAT04_TWGUJA = new TYData("DAT04_TWGUJA", null);
            this.DAT04_TWGUIP = new TYData("DAT04_TWGUIP", null);
            this.DAT04_TWGUCD = new TYData("DAT04_TWGUCD", null);
            this.DAT04_TWGONG = new TYData("DAT04_TWGONG", null);
            this.DAT04_TWGUBN = new TYData("DAT04_TWGUBN", null);
            this.DAT04_TWYUL = new TYData("DAT04_TWYUL", null);
            this.DAT04_TWIAMT = new TYData("DAT04_TWIAMT", null); 
            #endregion

            #region Description : 입금표관리 저장 변수 정의
            this.DAT05_TRSSID   = new TYData("DAT05_TRSSID", null);
            this.DAT05_TRJPDPMK = new TYData("DAT05_TRJPDPMK", null);
            this.DAT05_TRJPDTMK = new TYData("DAT05_TRJPDTMK", null);
            this.DAT05_TRJPNOSQ = new TYData("DAT05_TRJPNOSQ", null);
            this.DAT05_TRJPNOLN = new TYData("DAT05_TRJPNOLN", null);
            this.DAT05_TRDPMK = new TYData("DAT05_TRDPMK", null);
            this.DAT05_TRYEAR   = new TYData("DAT05_TRYEAR", null);
            this.DAT05_TRCDSB   = new TYData("DAT05_TRCDSB", null);
            this.DAT05_TRSEQ    = new TYData("DAT05_TRSEQ", null);
            this.DAT05_TRVEND   = new TYData("DAT05_TRVEND", null);
            this.DAT05_TRAMT    = new TYData("DAT05_TRAMT", null);
            this.DAT05_TRRKAC   = new TYData("DAT05_TRRKAC", null);
            #endregion

            #region Description : 불공제관리 저장 변수 정의
            this.DAT06_TSBPSSID = new TYData("DAT06_TSBPSSID", null);
            this.DAT06_TSBPDPMK = new TYData("DAT06_TSBPDPMK", null);
            this.DAT06_TSBPDTMK = new TYData("DAT06_TSBPDTMK", null);
            this.DAT06_TSBPNOSQ = new TYData("DAT06_TSBPNOSQ", null);
            this.DAT06_TSBPNOLN = new TYData("DAT06_TSBPNOLN", null);
            this.DAT06_TSBCDTX = new TYData("DAT06_TSBCDTX", null);
            this.DAT06_TSBGUBN = new TYData("DAT06_TSBGUBN", null);
            this.DAT06_TSBAMT = new TYData("DAT06_TSBAMT", null);
            this.DAT06_TSBVAT = new TYData("DAT06_TSBVAT", null);
            #endregion

            #region Description : 무역원가 변수 정의
            this.DAT07_LCSSID = new TYData("DAT07_LCSSID", null);
            this.DAT07_LCGUBN = new TYData("DAT07_LCGUBN", null);
            this.DAT07_LCCHTEAM = new TYData("DAT07_LCCHTEAM", null);
            this.DAT07_LCCHSABN = new TYData("DAT07_LCCHSABN", null);
            this.DAT07_LCCHYYNO = new TYData("DAT07_LCCHYYNO", null);
            this.DAT07_LCCHSQNO = new TYData("DAT07_LCCHSQNO", null);
            this.DAT07_LCCHDEPT = new TYData("DAT07_LCCHDEPT", null);
            this.DAT07_LCCHILJA = new TYData("DAT07_LCCHILJA", null);
            this.DAT07_LCCHJPNO = new TYData("DAT07_LCCHJPNO", null);
            this.DAT07_LCCHJPSQ = new TYData("DAT07_LCCHJPSQ", null);
            this.DAT07_LCPUMMOK = new TYData("DAT07_LCPUMMOK", null);
            this.DAT07_LCRECODE = new TYData("DAT07_LCRECODE", null);
            this.DAT07_LCCHMICD = new TYData("DAT07_LCCHMICD", null);
            this.DAT07_LCCHACCD = new TYData("DAT07_LCCHACCD", null);
            this.DAT07_LCCHAMT = new TYData("DAT07_LCCHAMT", null);
            this.DAT07_LCCHGUBN = new TYData("DAT07_LCCHGUBN", null);
            this.DAT07_LCBLGUBN = new TYData("DAT07_LCBLGUBN", null);
            this.DAT07_LCBLNUMB = new TYData("DAT07_LCBLNUMB", null);
            this.DAT07_LCACCEPT = new TYData("DAT07_LCACCEPT", null);
            this.DAT07_LCRKAC = new TYData("DAT07_LCRKAC", null);
            this.DAT07_LCHIGUBN = new TYData("DAT07_LCHIGUBN", null);
            //this.DAT07_LCHIDATE = new TYData("DAT07_LCHIDATE", null);
            //this.DAT07_LCHITIME = new TYData("DAT07_LCHITIME", null);
            #endregion

            //UP_Set_Control();

            UP_Set_Control_New();

        }

        #region Description : Page Load
        private void TYACBJ001I_Load(object sender, System.EventArgs e)
        {
            //this.CBH10_VALUE35.SetIPopupHelper(new TYAZBJ01C1()); // 예산선택
            //this.CBH11_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
            //this.CBH12_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
            //this.CBH13_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
            //this.CBH14_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
            //this.CBH15_VALUE35.SetIPopupHelper(new TYAZBJ01C1());

            //this.CBH10_VALUE29.SetIPopupHelper(new TYAZBJ01C2()); // 어음번호 (받어)
            //this.CBH11_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
            //this.CBH12_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
            //this.CBH13_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
            //this.CBH14_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
            //this.CBH15_VALUE29.SetIPopupHelper(new TYAZBJ01C2());

            //this.CBH10_VALUE09.SetIPopupHelper(new TYAZBJ01C3()); // 어음번호 (지어)
            //this.CBH11_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
            //this.CBH12_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
            //this.CBH13_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
            //this.CBH14_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
            //this.CBH15_VALUE09.SetIPopupHelper(new TYAZBJ01C3());

            //this.CBH10_VALUE41.SetIPopupHelper(new TYAZBJ01C5()); // 외화관리번호
            //this.CBH11_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
            //this.CBH12_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
            //this.CBH13_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
            //this.CBH14_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
            //this.CBH15_VALUE41.SetIPopupHelper(new TYAZBJ01C5());

            //this.CBH10_VALUE37.SetIPopupHelper(new TYAZBJ01C7()); // 출장번호
            //this.CBH11_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
            //this.CBH12_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
            //this.CBH13_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
            //this.CBH14_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
            //this.CBH15_VALUE37.SetIPopupHelper(new TYAZBJ01C7());

            //this.CBH10_VALUE38.SetIPopupHelper(new TYAZBJ01C8()); // 자산관리번호
            //this.CBH11_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
            //this.CBH12_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
            //this.CBH13_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
            //this.CBH14_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
            //this.CBH15_VALUE38.SetIPopupHelper(new TYAZBJ01C8());

            this.CBH01_B2DPMK.CodeText.Enter += new EventHandler(this.CBH01_B2DPMK_CodeText_Enter); // 코드박스 커스 포커스 정의(Enter)
            this.CBH01_B2HISAB.CodeText.Enter += new EventHandler(this.CBH01_2HISAB_CodeText_Enter); // 코드박스 커스 포커스 정의(Enter)
            this.CBH01_TSNOCL.CodeText.KeyPress += new KeyPressEventHandler(this.CBH01_TSNOCL_CodeText_KeyPress);

            //// 자산관리번호 Value Changed 이벤트
            //this.CBH10_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH10_VALUE38_CodeText_TextChanged);
            //this.CBH11_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH11_VALUE38_CodeText_TextChanged);
            //this.CBH12_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH12_VALUE38_CodeText_TextChanged);
            //this.CBH13_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH13_VALUE38_CodeText_TextChanged);
            //this.CBH14_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH14_VALUE38_CodeText_TextChanged);
            //this.CBH15_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH15_VALUE38_CodeText_TextChanged);
            


            // CBH01_B2CDAC

            // 미승인전표 등록시 처리버튼 체크  이벤트 정의 
            this.BTN61_INP.ProcessCheck += new TButton.CheckHandler(this.BTN61_INP_ProcessCheck);
            this.BTN61_CONFIRM.ProcessCheck += new TButton.CheckHandler(this.BTN61_CONFIRM_ProcessCheck);
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(this.BTN61_INQ_ProcessCheck);
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(this.BTN61_EDIT_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(this.BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_ProcessCheck);
            this.BTN61_CANCEL.ProcessCheck += new TButton.CheckHandler(this.BTN61_CANCEL_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(this.BTN61_PRT_ProcessCheck);
 
            // 접대비 등록시 처리버튼 체크  이벤트 정의
            this.BTN61_INP_RECP.ProcessCheck += new TButton.CheckHandler(this.BTN61_INP_RECP_ProcessCheck);
            this.BTN61_SAV_RECP.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_RECP_ProcessCheck);

            // 외화관리 등록시 처리버튼 체크  이벤트 정의
            this.BTN61_INP_FORE.ProcessCheck += new TButton.CheckHandler(this.BTN61_INP_FORE_ProcessCheck);
            this.BTN61_SAV_FORE.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_FORE_ProcessCheck);

            // 입금표 등록시 처리버튼 체크  이벤트 정의
            this.BTN61_INP_MONE.ProcessCheck += new TButton.CheckHandler(this.BTN61_INP_MONE_ProcessCheck);
            this.BTN61_SAV_MONE.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_MONE_ProcessCheck);
            this.BTN61_REM_MONE.ProcessCheck += new TButton.CheckHandler(this.BTN61_REM_MONE_ProcessCheck);

            // 불공재 등록시 처리버튼 체크  이벤트 정의
            this.BTN61_INP_TXEX.ProcessCheck += new TButton.CheckHandler(this.BTN61_INP_TXEX_ProcessCheck);
            this.BTN61_SAV_TXEX.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_TXEX_ProcessCheck);


            //BATID번호 부여(SSID)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            this.fsSessionId = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            #region Description : 미승인전표 ControlFactory ADD  정의
            this.ControlFactory.Add(this.DAT02_W2SSID);
            this.ControlFactory.Add(this.DAT02_W2DPMK);
            this.ControlFactory.Add(this.DAT02_W2DTMK);
            this.ControlFactory.Add(this.DAT02_W2NOSQ);
            this.ControlFactory.Add(this.DAT02_W2NOLN);
            this.ControlFactory.Add(this.DAT02_W2IDJP);
            this.ControlFactory.Add(this.DAT02_W2NOJP);
            this.ControlFactory.Add(this.DAT02_W2CDAC);
            this.ControlFactory.Add(this.DAT02_W2DTAC);
            this.ControlFactory.Add(this.DAT02_W2DTLI);
            this.ControlFactory.Add(this.DAT02_W2DPAC);
            this.ControlFactory.Add(this.DAT02_W2CDMI1);
            this.ControlFactory.Add(this.DAT02_W2VLMI1);
            this.ControlFactory.Add(this.DAT02_W2CDMI2);
            this.ControlFactory.Add(this.DAT02_W2VLMI2);
            this.ControlFactory.Add(this.DAT02_W2CDMI3);
            this.ControlFactory.Add(this.DAT02_W2VLMI3);
            this.ControlFactory.Add(this.DAT02_W2CDMI4);
            this.ControlFactory.Add(this.DAT02_W2VLMI4);
            this.ControlFactory.Add(this.DAT02_W2CDMI5);
            this.ControlFactory.Add(this.DAT02_W2VLMI5);
            this.ControlFactory.Add(this.DAT02_W2CDMI6);
            this.ControlFactory.Add(this.DAT02_W2VLMI6);
            this.ControlFactory.Add(this.DAT02_W2AMDR);
            this.ControlFactory.Add(this.DAT02_W2AMCR);
            this.ControlFactory.Add(this.DAT02_W2CDFD);
            this.ControlFactory.Add(this.DAT02_W2AMFD);
            this.ControlFactory.Add(this.DAT02_W2RKAC);
            this.ControlFactory.Add(this.DAT02_W2RKCU);
            this.ControlFactory.Add(this.DAT02_W2WCJP);
            this.ControlFactory.Add(this.DAT02_W2PRGB);
            this.ControlFactory.Add(this.DAT02_W2HIGB);
            //this.ControlFactory.Add(this.DAT02_W2HIDAT);
            //this.ControlFactory.Add(this.DAT02_W2HITIM);
            this.ControlFactory.Add(this.DAT02_W2HISAB);
            this.ControlFactory.Add(this.DAT02_W2GUBUN);
            this.ControlFactory.Add(this.DAT02_W2TXAMT);
            this.ControlFactory.Add(this.DAT02_W2TXVAT);
            this.ControlFactory.Add(this.DAT02_W2HWAJU); 
            #endregion

            #region Description : 접대비 ControlFactory ADD  정의
            this.ControlFactory.Add(this.DAT03_TSJPSSID);
            this.ControlFactory.Add(this.DAT03_TSJPDPMK);
            this.ControlFactory.Add(this.DAT03_TSJPDTMK);
            this.ControlFactory.Add(this.DAT03_TSJPNO_SQ);
            this.ControlFactory.Add(this.DAT03_TSJPNO_LN);
            this.ControlFactory.Add(this.DAT03_TSDTYY);
            this.ControlFactory.Add(this.DAT03_TSDTMM);
            this.ControlFactory.Add(this.DAT03_TSNOSQ);
            this.ControlFactory.Add(this.DAT03_TSDTOC);
            this.ControlFactory.Add(this.DAT03_TSDEID);
            this.ControlFactory.Add(this.DAT03_TSNOCL);
            this.ControlFactory.Add(this.DAT03_TSNMCP);
            this.ControlFactory.Add(this.DAT03_TSADCL);
            this.ControlFactory.Add(this.DAT03_TSNMRP);
            this.ControlFactory.Add(this.DAT03_TSNOCC);
            this.ControlFactory.Add(this.DAT03_TSREMK);
            this.ControlFactory.Add(this.DAT03_TSNOMK);
            this.ControlFactory.Add(this.DAT03_TSAMSE);
            this.ControlFactory.Add(this.DAT03_TSCGSE);
            this.ControlFactory.Add(this.DAT03_TSJPNO_1);
            this.ControlFactory.Add(this.DAT03_TSGUBN); 
            #endregion

            #region Description : 외화관리 ControlFactory ADD  정의
            this.ControlFactory.Add(this.DAT04_TWJPSSID);
            this.ControlFactory.Add(this.DAT04_TWJPDPMK);
            this.ControlFactory.Add(this.DAT04_TWJPDTMK);
            this.ControlFactory.Add(this.DAT04_TWJPNOSQ);
            this.ControlFactory.Add(this.DAT04_TWJPNOLN);
            this.ControlFactory.Add(this.DAT04_TWYEAR);
            this.ControlFactory.Add(this.DAT04_TWNOSQ);
            this.ControlFactory.Add(this.DAT04_TWBANK);
            this.ControlFactory.Add(this.DAT04_TWGUJA);
            this.ControlFactory.Add(this.DAT04_TWGUIP);
            this.ControlFactory.Add(this.DAT04_TWGUCD);
            this.ControlFactory.Add(this.DAT04_TWGONG);
            this.ControlFactory.Add(this.DAT04_TWGUBN);
            this.ControlFactory.Add(this.DAT04_TWYUL);
            this.ControlFactory.Add(this.DAT04_TWIAMT); 
            #endregion

            #region Description : 입금표관리  ControlFactory ADD  정의
            this.ControlFactory.Add(this.DAT05_TRSSID);
            this.ControlFactory.Add(this.DAT05_TRJPDPMK);
            this.ControlFactory.Add(this.DAT05_TRJPDTMK);
            this.ControlFactory.Add(this.DAT05_TRJPNOSQ);
            this.ControlFactory.Add(this.DAT05_TRJPNOLN);
            this.ControlFactory.Add(this.DAT05_TRDPMK);
            this.ControlFactory.Add(this.DAT05_TRYEAR);
            this.ControlFactory.Add(this.DAT05_TRCDSB);
            this.ControlFactory.Add(this.DAT05_TRSEQ);
            this.ControlFactory.Add(this.DAT05_TRVEND);
            this.ControlFactory.Add(this.DAT05_TRAMT);
            this.ControlFactory.Add(this.DAT05_TRRKAC);
            #endregion

            #region Description : 불공제관리  ControlFactory ADD  정의
            this.ControlFactory.Add(this.DAT06_TSBPSSID);
            this.ControlFactory.Add(this.DAT06_TSBPDPMK);
            this.ControlFactory.Add(this.DAT06_TSBPDTMK);
            this.ControlFactory.Add(this.DAT06_TSBPNOSQ);
            this.ControlFactory.Add(this.DAT06_TSBPNOLN);
            this.ControlFactory.Add(this.DAT06_TSBCDTX);
            this.ControlFactory.Add(this.DAT06_TSBGUBN);
            this.ControlFactory.Add(this.DAT06_TSBAMT);
            this.ControlFactory.Add(this.DAT06_TSBVAT);
            #endregion

            #region Description : 무역원가 ControlFactory ADD  정의
            this.ControlFactory.Add(this.DAT07_LCSSID);
            this.ControlFactory.Add(this.DAT07_LCGUBN);
            this.ControlFactory.Add(this.DAT07_LCCHTEAM);
            this.ControlFactory.Add(this.DAT07_LCCHSABN);
            this.ControlFactory.Add(this.DAT07_LCCHYYNO);
            this.ControlFactory.Add(this.DAT07_LCCHSQNO);
            this.ControlFactory.Add(this.DAT07_LCCHDEPT);
            this.ControlFactory.Add(this.DAT07_LCCHILJA);
            this.ControlFactory.Add(this.DAT07_LCCHJPNO);
            this.ControlFactory.Add(this.DAT07_LCCHJPSQ);
            this.ControlFactory.Add(this.DAT07_LCPUMMOK);
            this.ControlFactory.Add(this.DAT07_LCRECODE);
            this.ControlFactory.Add(this.DAT07_LCCHMICD);
            this.ControlFactory.Add(this.DAT07_LCCHACCD);
            this.ControlFactory.Add(this.DAT07_LCCHAMT);
            this.ControlFactory.Add(this.DAT07_LCCHGUBN);
            this.ControlFactory.Add(this.DAT07_LCBLGUBN);
            this.ControlFactory.Add(this.DAT07_LCBLNUMB);
            this.ControlFactory.Add(this.DAT07_LCACCEPT);
            this.ControlFactory.Add(this.DAT07_LCRKAC);
            this.ControlFactory.Add(this.DAT07_LCHIGUBN);
            //this.ControlFactory.Add(this.DAT07_LCHIDATE);
            //this.ControlFactory.Add(this.DAT07_LCHITIME);
            #endregion

            this.HiddenOK = "false";

            this.PAN10_VLMI1.Initialize();
            this.PAN10_VLMI2.Initialize();
            this.PAN10_VLMI3.Initialize();
            this.PAN10_VLMI4.Initialize();
            this.PAN10_VLMI5.Initialize();
            this.PAN10_VLMI6.Initialize();

            //this.CBH10_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH10_B2INDX_CodeBoxDataBinded);
            //this.CBH11_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH11_B2INDX_CodeBoxDataBinded);
            //this.CBH12_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH12_B2INDX_CodeBoxDataBinded);
            //this.CBH13_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH13_B2INDX_CodeBoxDataBinded);
            //this.CBH14_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH14_B2INDX_CodeBoxDataBinded);
            //this.CBH15_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH15_B2INDX_CodeBoxDataBinded);

            this.CBH01_TWBANK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(this.CBH01_TWBANK_CodeBoxDataBinded); // 외화관리 
            
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString();
            this.CBH01_TRDPMK.DummyValue = this.DTP01_B2DTMK.GetString();
            this.CBH01_B2DPAC.DummyValue = this.DTP01_B2DTMK.GetString();

            ImgBtnFocusEvent();

            UP_ComBoLineClear(); //라인번호 클리어

            UP_FieldClear();

            tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표,불공제,무역원가)

            UP_FieldLock(true);

            if (string.IsNullOrEmpty(this._DPMK) && string.IsNullOrEmpty(this._DTMK) && string.IsNullOrEmpty(this._NOSQ))
            {
                UP_Page_SabunInit();
                this.SetFocus(this.CBH01_B2DPMK.CodeText);
            }
            else
            {
                this.CBH01_B2DPMK.DummyValue = this._DTMK;
                this.CBH01_B2DPMK.SetValue(this._DPMK);
                this.DTP01_B2DTMK.SetValue(this._DTMK);
                this.TXT01_B2NOSQ.SetValue(this._NOSQ);

                this.BTN61_CONFIRM_ProcessCheck(null, null);
                //this.BTN61_CONFIRM_Click(null, null);
            }

            //로그인사번의 최근 2개월동안의 적요 셋팅
            UP_Get_MemoLogList();

            //TabIndex 이벤트 
            TXT01_B2RKAC.TabIndexCustom = true;

        } 
        #endregion

        #region Description : 관리 항목 1 ~ 6 코드 BOX 셋팅 ---> UP_Set_Control()

        private void UP_Set_Control()
        {
            string sCode = "";
            string sCodeName = "";

            sCode = "01";
            sCodeName = "거래처코드";
            if (sCode == "01")
            {
                this.CBH10_VALUE01 = new TYCodeBox();
                this.CBH10_VALUE01.Name = "CBH10_VALUE01";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE01);
                this.CBH11_VALUE01 = new TYCodeBox();
                this.CBH11_VALUE01.Name = "CBH11_VALUE01";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE01);
                this.CBH12_VALUE01 = new TYCodeBox();
                this.CBH12_VALUE01.Name = "CBH12_VALUE01";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE01);
                this.CBH13_VALUE01 = new TYCodeBox();
                this.CBH13_VALUE01.Name = "CBH13_VALUE01";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE01);
                this.CBH14_VALUE01 = new TYCodeBox();
                this.CBH14_VALUE01.Name = "CBH14_VALUE01";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE01);
                this.CBH15_VALUE01 = new TYCodeBox();
                this.CBH15_VALUE01.Name = "CBH15_VALUE01";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE01);

            }
            sCode = "02";
            sCodeName = "금융기관";
            if (sCode == "02")
            {
                this.CBH10_B2INDX = new TYCodeBox();
                this.CBH10_B2INDX.Name = "CBH10_B2INDX";
                this.CBH10_B2INDX.DummyValue = "BK";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                this.CBH11_B2INDX = new TYCodeBox();
                this.CBH11_B2INDX.Name = "CBH11_B2INDX";
                this.CBH11_B2INDX.DummyValue = "BK";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                this.CBH12_B2INDX = new TYCodeBox();
                this.CBH12_B2INDX.Name = "CBH12_B2INDX";
                this.CBH12_B2INDX.DummyValue = "BK";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                this.CBH13_B2INDX = new TYCodeBox();
                this.CBH13_B2INDX.Name = "CBH13_B2INDX";
                this.CBH13_B2INDX.DummyValue = "BK";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                this.CBH14_B2INDX = new TYCodeBox();
                this.CBH14_B2INDX.Name = "CBH14_B2INDX";
                this.CBH14_B2INDX.DummyValue = "BK";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                this.CBH15_B2INDX = new TYCodeBox();
                this.CBH15_B2INDX.Name = "CBH15_B2INDX";
                this.CBH15_B2INDX.DummyValue = "BK";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
            }
            sCode = "03";
            sCodeName = "부서코드";
            if (sCode == "03")
            {
                this.CBH10_VALUE03 = new TYCodeBox();
                this.CBH10_VALUE03.Name = "CBH10_VALUE03";
                this.CBH10_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE03, this.CBH10_VALUE03.DummyValue);
                this.CBH11_VALUE03 = new TYCodeBox();
                this.CBH11_VALUE03.Name = "CBH11_VALUE03";
                this.CBH11_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE03, this.CBH11_VALUE03.DummyValue);
                this.CBH12_VALUE03 = new TYCodeBox();
                this.CBH12_VALUE03.Name = "CBH12_VALUE03";
                this.CBH12_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE03, this.CBH12_VALUE03.DummyValue);
                this.CBH13_VALUE03 = new TYCodeBox();
                this.CBH13_VALUE03.Name = "CBH13_VALUE03";
                this.CBH13_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE03, this.CBH13_VALUE03.DummyValue);
                this.CBH14_VALUE03 = new TYCodeBox();
                this.CBH14_VALUE03.Name = "CBH14_VALUE03";
                this.CBH14_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE03, this.CBH14_VALUE03.DummyValue);
                this.CBH15_VALUE03 = new TYCodeBox();
                this.CBH15_VALUE03.Name = "CBH15_VALUE03";
                this.CBH15_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE03, this.CBH15_VALUE03.DummyValue);
            }
            sCode = "04";
            sCodeName = "E/L NO";
            if (sCode == "04")
            {
                this.TXT10_VALUE04 = new TYTextBox();
                this.TXT10_VALUE04.Name = "TXT10_VALUE04";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE04);
                this.TXT11_VALUE04 = new TYTextBox();
                this.TXT11_VALUE04.Name = "TXT11_VALUE04";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE04);
                this.TXT12_VALUE04 = new TYTextBox();
                this.TXT12_VALUE04.Name = "TXT12_VALUE04";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE04);
                this.TXT13_VALUE04 = new TYTextBox();
                this.TXT13_VALUE04.Name = "TXT13_VALUE04";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE04);
                this.TXT14_VALUE04 = new TYTextBox();
                this.TXT14_VALUE04.Name = "TXT14_VALUE04";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE04);
                this.TXT15_VALUE04 = new TYTextBox();
                this.TXT15_VALUE04.Name = "TXT15_VALUE04";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE04);
            }
            sCode = "05";
            sCodeName = "사원번호";
            if (sCode == "05")
            {
                this.CBH10_VALUE05 = new TYCodeBox();
                this.CBH10_VALUE05.Name = "CBH10_VALUE05";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE05);
                this.CBH11_VALUE05 = new TYCodeBox();
                this.CBH11_VALUE05.Name = "CBH11_VALUE05";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE05);
                this.CBH12_VALUE05 = new TYCodeBox();
                this.CBH12_VALUE05.Name = "CBH12_VALUE05";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE05);
                this.CBH13_VALUE05 = new TYCodeBox();
                this.CBH13_VALUE05.Name = "CBH13_VALUE05";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE05);
                this.CBH14_VALUE05 = new TYCodeBox();
                this.CBH14_VALUE05.Name = "CBH14_VALUE05";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE05);
                this.CBH15_VALUE05 = new TYCodeBox();
                this.CBH15_VALUE05.Name = "CBH15_VALUE05";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE05);
            }
            sCode = "06";
            sCodeName = "과세표준";
            if (sCode == "06")
            {
                this.TXT10_VALUE06 = new TYTextBox();
                this.TXT10_VALUE06.Name = "TXT10_VALUE06";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE06);
                this.TXT11_VALUE06 = new TYTextBox();
                this.TXT11_VALUE06.Name = "TXT11_VALUE06";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE06);
                this.TXT12_VALUE06 = new TYTextBox();
                this.TXT12_VALUE06.Name = "TXT12_VALUE06";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE06);
                this.TXT13_VALUE06 = new TYTextBox();
                this.TXT13_VALUE06.Name = "TXT13_VALUE06";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE06);
                this.TXT14_VALUE06 = new TYTextBox();
                this.TXT14_VALUE06.Name = "TXT14_VALUE06";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE06);
                this.TXT15_VALUE06 = new TYTextBox();
                this.TXT15_VALUE06.Name = "TXT15_VALUE06";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE06);
            }
            sCode = "07";
            sCodeName = "계좌번호";
            if (sCode == "07")
            {
                this.CBH10_VALUE07 = new TYCodeBox();
                this.CBH10_VALUE07.Name = "CBH10_VALUE07";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE07);
                this.CBH11_VALUE07 = new TYCodeBox();
                this.CBH11_VALUE07.Name = "CBH11_VALUE07";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE07);
                this.CBH12_VALUE07 = new TYCodeBox();
                this.CBH12_VALUE07.Name = "CBH12_VALUE07";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE07);
                this.CBH13_VALUE07 = new TYCodeBox();
                this.CBH13_VALUE07.Name = "CBH13_VALUE07";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE07);
                this.CBH14_VALUE07 = new TYCodeBox();
                this.CBH14_VALUE07.Name = "CBH14_VALUE07";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE07);
                this.CBH15_VALUE07 = new TYCodeBox();
                this.CBH15_VALUE07.Name = "CBH15_VALUE07";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE07);
            }
            sCode = "08";
            sCodeName = "관리번호";
            if (sCode == "08")
            {
                this.TXT10_VALUE08 = new TYTextBox();
                this.TXT10_VALUE08.Name = "TXT10_VALUE08";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE08);
                this.TXT11_VALUE08 = new TYTextBox();
                this.TXT11_VALUE08.Name = "TXT11_VALUE08";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE08);
                this.TXT12_VALUE08 = new TYTextBox();
                this.TXT12_VALUE08.Name = "TXT12_VALUE08";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE08);
                this.TXT13_VALUE08 = new TYTextBox();
                this.TXT13_VALUE08.Name = "TXT13_VALUE08";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE08);
                this.TXT14_VALUE08 = new TYTextBox();
                this.TXT14_VALUE08.Name = "TXT14_VALUE08";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE08);
                this.TXT15_VALUE08 = new TYTextBox();
                this.TXT15_VALUE08.Name = "TXT15_VALUE08";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE08);
            }
            sCode = "09";
            sCodeName = "어음번호"; // 지급어음
            if (sCode == "09")
            {
                this.CBH10_VALUE09 = new TYCodeBox();
                this.CBH10_VALUE09.Name = "CBH10_VALUE09";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE09);
                this.CBH11_VALUE09 = new TYCodeBox();
                this.CBH11_VALUE09.Name = "CBH11_VALUE09";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE09);
                this.CBH12_VALUE09 = new TYCodeBox();
                this.CBH12_VALUE09.Name = "CBH12_VALUE09";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE09);
                this.CBH13_VALUE09 = new TYCodeBox();
                this.CBH13_VALUE09.Name = "CBH13_VALUE09";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE09);
                this.CBH14_VALUE09 = new TYCodeBox();
                this.CBH14_VALUE09.Name = "CBH14_VALUE09";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE09);
                this.CBH15_VALUE09 = new TYCodeBox();
                this.CBH15_VALUE09.Name = "CBH15_VALUE09";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE09);
            }
            sCode = "10";
            sCodeName = "L/C NO"; // 무역
            if (sCode == "10")
            {
                this.CBH10_VALUE10 = new TYCodeBox();
                this.CBH10_VALUE10.Name = "CBH10_VALUE10";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE10);
                this.CBH11_VALUE10 = new TYCodeBox();
                this.CBH11_VALUE10.Name = "CBH11_VALUE10";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE10);
                this.CBH12_VALUE10 = new TYCodeBox();
                this.CBH12_VALUE10.Name = "CBH12_VALUE10";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE10);
                this.CBH13_VALUE10 = new TYCodeBox();
                this.CBH13_VALUE10.Name = "CBH13_VALUE10";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE10);
                this.CBH14_VALUE10 = new TYCodeBox();
                this.CBH14_VALUE10.Name = "CBH14_VALUE10";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE10);
                this.CBH15_VALUE10 = new TYCodeBox();
                this.CBH15_VALUE10.Name = "CBH15_VALUE10";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE10);

                //this.TXT10_VALUE10 = new TYTextBox();
                //this.TXT10_VALUE10.Name = "TXT10_VALUE10";
                //this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_VALUE10);
                //this.TXT11_VALUE10 = new TYTextBox();
                //this.TXT11_VALUE10.Name = "TXT11_VALUE10";
                //this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT11_VALUE10);
                //this.TXT12_VALUE10 = new TYTextBox();
                //this.TXT12_VALUE10.Name = "TXT12_VALUE10";
                //this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT12_VALUE10);
                //this.TXT13_VALUE10 = new TYTextBox();
                //this.TXT13_VALUE10.Name = "TXT13_VALUE10";
                //this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT13_VALUE10);
                //this.TXT14_VALUE10 = new TYTextBox();
                //this.TXT14_VALUE10.Name = "TXT14_VALUE10";
                //this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT14_VALUE10);
                //this.TXT15_VALUE10 = new TYTextBox();
                //this.TXT15_VALUE10.Name = "TXT15_VALUE10";
                //this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT15_VALUE10);

            }
            sCode = "11";
            sCodeName = "세무구분"; // TX
            if (sCode == "11")
            {
                this.CBH10_B2INDX.DummyValue = "TX";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                this.CBH11_B2INDX.DummyValue = "TX";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                this.CBH12_B2INDX.DummyValue = "TX";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                this.CBH13_B2INDX.DummyValue = "TX";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                this.CBH14_B2INDX.DummyValue = "TX";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                this.CBH15_B2INDX.DummyValue = "TX";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);

                //this.CBH10_VALUE11 = new TYCodeBox();
                //this.CBH10_VALUE11.Name = "CBH10_VALUE11";
                //this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_VALUE11);
                //this.CBH11_VALUE11 = new TYCodeBox();
                //this.CBH11_VALUE11.Name = "CBH11_VALUE11";
                //this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_VALUE11);
                //this.CBH12_VALUE11 = new TYCodeBox();
                //this.CBH12_VALUE11.Name = "CBH12_VALUE11";
                //this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_VALUE11);
                //this.CBH13_VALUE11 = new TYCodeBox();
                //this.CBH13_VALUE11.Name = "CBH13_VALUE11";
                //this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_VALUE11);
                //this.CBH14_VALUE11 = new TYCodeBox();
                //this.CBH14_VALUE11.Name = "CBH14_VALUE11";
                //this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_VALUE11);
                //this.CBH15_VALUE11 = new TYCodeBox();
                //this.CBH15_VALUE11.Name = "CBH15_VALUE11";
                //this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_VALUE11);

            }
            sCode = "12";
            sCodeName = "차입금번호";            
            if (sCode == "12")
            {
                this.TXT10_VALUE12 = new TYTextBox();
                this.TXT10_VALUE12.Name = "TXT10_VALUE12";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE12);
                this.TXT11_VALUE12 = new TYTextBox();
                this.TXT11_VALUE12.Name = "TXT11_VALUE12";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE12);
                this.TXT12_VALUE12 = new TYTextBox();
                this.TXT12_VALUE12.Name = "TXT12_VALUE12";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE12);
                this.TXT13_VALUE12 = new TYTextBox();
                this.TXT13_VALUE12.Name = "TXT13_VALUE12";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE12);
                this.TXT14_VALUE12 = new TYTextBox();
                this.TXT14_VALUE12.Name = "TXT14_VALUE12";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE12);
                this.TXT15_VALUE12 = new TYTextBox();
                this.TXT15_VALUE12.Name = "TXT15_VALUE12";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE12);
            }
            sCode = "13";
            sCodeName = "전화번호";
            if (sCode == "13")
            {
                this.TXT10_VALUE13 = new TYTextBox();
                this.TXT10_VALUE13.Name = "TXT10_VALUE13";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE13);
                this.TXT11_VALUE13 = new TYTextBox();
                this.TXT11_VALUE13.Name = "TXT11_VALUE13";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE13);
                this.TXT12_VALUE13 = new TYTextBox();
                this.TXT12_VALUE13.Name = "TXT12_VALUE13";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE13);
                this.TXT13_VALUE13 = new TYTextBox();
                this.TXT13_VALUE13.Name = "TXT13_VALUE13";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE13);
                this.TXT14_VALUE13 = new TYTextBox();
                this.TXT14_VALUE13.Name = "TXT14_VALUE13";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE13);
                this.TXT15_VALUE13 = new TYTextBox();
                this.TXT15_VALUE13.Name = "TXT15_VALUE13";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE13);
            }
            sCode = "14";
            sCodeName = "공급가액";
            if (sCode == "14")
            {
                this.TXT10_VALUE14 = new TYTextBox();
                this.TXT10_VALUE14.Name = "TXT10_VALUE14";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE14);
                this.TXT11_VALUE14 = new TYTextBox();
                this.TXT11_VALUE14.Name = "TXT11_VALUE14";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE14);
                this.TXT12_VALUE14 = new TYTextBox();
                this.TXT12_VALUE14.Name = "TXT12_VALUE14";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE14);
                this.TXT13_VALUE14 = new TYTextBox();
                this.TXT13_VALUE14.Name = "TXT13_VALUE14";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE14);
                this.TXT14_VALUE14 = new TYTextBox();
                this.TXT14_VALUE14.Name = "TXT14_VALUE14";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE14);
                this.TXT15_VALUE14 = new TYTextBox();
                this.TXT15_VALUE14.Name = "TXT15_VALUE14";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE14);
            }
            sCode = "15";
            sCodeName = "거래일자";
            if (sCode == "15")
            {
                this.TXT10_VALUE15 = new TYTextBox();
                this.TXT10_VALUE15.Name = "TXT10_VALUE15";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE15);
                this.TXT11_VALUE15 = new TYTextBox();
                this.TXT11_VALUE15.Name = "TXT11_VALUE15";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE15);
                this.TXT12_VALUE15 = new TYTextBox();
                this.TXT12_VALUE15.Name = "TXT12_VALUE15";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE15);
                this.TXT13_VALUE15 = new TYTextBox();
                this.TXT13_VALUE15.Name = "TXT13_VALUE15";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE15);
                this.TXT14_VALUE15 = new TYTextBox();
                this.TXT14_VALUE15.Name = "TXT14_VALUE15";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE15);
                this.TXT15_VALUE15 = new TYTextBox();
                this.TXT15_VALUE15.Name = "TXT15_VALUE15";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE15);
            }
            sCode = "16";
            sCodeName = "기간";
            if (sCode == "16")
            {
                this.TXT10_VALUE16 = new TYTextBox();
                this.TXT10_VALUE16.Name = "TXT10_VALUE16";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE16);
                this.TXT11_VALUE16 = new TYTextBox();
                this.TXT11_VALUE16.Name = "TXT11_VALUE16";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE16);
                this.TXT12_VALUE16 = new TYTextBox();
                this.TXT12_VALUE16.Name = "TXT12_VALUE16";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE16);
                this.TXT13_VALUE16 = new TYTextBox();
                this.TXT13_VALUE16.Name = "TXT13_VALUE16";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE16);
                this.TXT14_VALUE16 = new TYTextBox();
                this.TXT14_VALUE16.Name = "TXT14_VALUE16";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE16);
                this.TXT15_VALUE16 = new TYTextBox();
                this.TXT15_VALUE16.Name = "TXT15_VALUE16";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE16);
            }
            sCode = "17";
            sCodeName = "자동차번호";
            if (sCode == "17")
            {
                this.TXT10_VALUE17 = new TYTextBox();
                this.TXT10_VALUE17.Name = "TXT10_VALUE17";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE17);
                this.TXT11_VALUE17 = new TYTextBox();
                this.TXT11_VALUE17.Name = "TXT11_VALUE17";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE17);
                this.TXT12_VALUE17 = new TYTextBox();
                this.TXT12_VALUE17.Name = "TXT12_VALUE17";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE17);
                this.TXT13_VALUE17 = new TYTextBox();
                this.TXT13_VALUE17.Name = "TXT13_VALUE17";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE17);
                this.TXT14_VALUE17 = new TYTextBox();
                this.TXT14_VALUE17.Name = "TXT14_VALUE17";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE17);
                this.TXT15_VALUE17 = new TYTextBox();
                this.TXT15_VALUE17.Name = "TXT15_VALUE17";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE17);
            }
            sCode = "18";
            sCodeName = "회차";
            if (sCode == "18")
            {
                this.TXT10_VALUE18 = new TYTextBox();
                this.TXT10_VALUE18.Name = "TXT10_VALUE18";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE18);
                this.TXT11_VALUE18 = new TYTextBox();
                this.TXT11_VALUE18.Name = "TXT11_VALUE18";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE18);
                this.TXT12_VALUE18 = new TYTextBox();
                this.TXT12_VALUE18.Name = "TXT12_VALUE18";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE18);
                this.TXT13_VALUE18 = new TYTextBox();
                this.TXT13_VALUE18.Name = "TXT13_VALUE18";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE18);
                this.TXT14_VALUE18 = new TYTextBox();
                this.TXT14_VALUE18.Name = "TXT14_VALUE18";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE18);
                this.TXT15_VALUE18 = new TYTextBox();
                this.TXT15_VALUE18.Name = "TXT15_VALUE18";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE18);
            }
            sCode = "19";
            sCodeName = "고지번호";
            if (sCode == "19")
            {
                this.TXT10_VALUE19 = new TYTextBox();
                this.TXT10_VALUE19.Name = "TXT10_VALUE19";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE19);
                this.TXT11_VALUE19 = new TYTextBox();
                this.TXT11_VALUE19.Name = "TXT11_VALUE19";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE19);
                this.TXT12_VALUE19 = new TYTextBox();
                this.TXT12_VALUE19.Name = "TXT12_VALUE19";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE19);
                this.TXT13_VALUE19 = new TYTextBox();
                this.TXT13_VALUE19.Name = "TXT13_VALUE19";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE19);
                this.TXT14_VALUE19 = new TYTextBox();
                this.TXT14_VALUE19.Name = "TXT14_VALUE19";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE19);
                this.TXT15_VALUE19 = new TYTextBox();
                this.TXT15_VALUE19.Name = "TXT15_VALUE19";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE19);
            }
            sCode = "20";
            sCodeName = "이자율(%)";
            if (sCode == "20")
            {
                this.TXT10_VALUE20 = new TYTextBox();
                this.TXT10_VALUE20.Name = "TXT10_VALUE20";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE20);
                this.TXT11_VALUE20 = new TYTextBox();
                this.TXT11_VALUE20.Name = "TXT11_VALUE20";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE20);
                this.TXT12_VALUE20 = new TYTextBox();
                this.TXT12_VALUE20.Name = "TXT12_VALUE20";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE20);
                this.TXT13_VALUE20 = new TYTextBox();
                this.TXT13_VALUE20.Name = "TXT13_VALUE20";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE20);
                this.TXT14_VALUE20 = new TYTextBox();
                this.TXT14_VALUE20.Name = "TXT14_VALUE20";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE20);
                this.TXT15_VALUE20 = new TYTextBox();
                this.TXT15_VALUE20.Name = "TXT15_VALUE20";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE20);
            }
            sCode = "21";
            sCodeName = "외화금액";
            if (sCode == "21")
            {
                this.TXT10_VALUE21 = new TYTextBox();
                this.TXT10_VALUE21.Name = "TXT10_VALUE21";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE21);
                this.TXT11_VALUE21 = new TYTextBox();
                this.TXT11_VALUE21.Name = "TXT11_VALUE21";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE21);
                this.TXT12_VALUE21 = new TYTextBox();
                this.TXT12_VALUE21.Name = "TXT12_VALUE21";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE21);
                this.TXT13_VALUE21 = new TYTextBox();
                this.TXT13_VALUE21.Name = "TXT13_VALUE21";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE21);
                this.TXT14_VALUE21 = new TYTextBox();
                this.TXT14_VALUE21.Name = "TXT14_VALUE21";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE21);
                this.TXT15_VALUE21 = new TYTextBox();
                this.TXT15_VALUE21.Name = "TXT15_VALUE21";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE21);
            }
            sCode = "23";
            sCodeName = "예/적금 계정코드";
            if (sCode == "23")
            {
                this.TXT10_VALUE23 = new TYTextBox();
                this.TXT10_VALUE23.Name = "TXT10_VALUE23";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE23);
                this.TXT11_VALUE23 = new TYTextBox();
                this.TXT11_VALUE23.Name = "TXT11_VALUE23";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE23);
                this.TXT12_VALUE23 = new TYTextBox();
                this.TXT12_VALUE23.Name = "TXT12_VALUE23";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE23);
                this.TXT13_VALUE23 = new TYTextBox();
                this.TXT13_VALUE23.Name = "TXT13_VALUE23";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE23);
                this.TXT14_VALUE23 = new TYTextBox();
                this.TXT14_VALUE23.Name = "TXT14_VALUE23";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE23);
                this.TXT15_VALUE23 = new TYTextBox();
                this.TXT15_VALUE23.Name = "TXT15_VALUE23";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE23);
            }
            sCode = "24";
            sCodeName = "어음구분"; // BG
            if (sCode == "24")
            {
                this.CBH10_B2INDX.DummyValue = "BG";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                this.CBH11_B2INDX.DummyValue = "BG";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                this.CBH12_B2INDX.DummyValue = "BG";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                this.CBH13_B2INDX.DummyValue = "BG";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                this.CBH14_B2INDX.DummyValue = "BG";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                this.CBH15_B2INDX.DummyValue = "BG";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
            }
            sCode = "25";
            sCodeName = "어음상태"; // BB
            if (sCode == "25")
            {
                this.CBH10_B2INDX.DummyValue = "BB";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                this.CBH11_B2INDX.DummyValue = "BB";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                this.CBH12_B2INDX.DummyValue = "BB";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                this.CBH13_B2INDX.DummyValue = "BB";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                this.CBH14_B2INDX.DummyValue = "BB";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                this.CBH15_B2INDX.DummyValue = "BB";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
            }
            sCode = "26";
            sCodeName = "계정코드"; 
            if (sCode == "26")
            {
                this.CBH10_VALUE26 = new TYCodeBox();
                this.CBH10_VALUE26.Name = "CBH10_VALUE26";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE26);
                this.CBH11_VALUE26 = new TYCodeBox();
                this.CBH11_VALUE26.Name = "CBH11_VALUE26";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE26);
                this.CBH12_VALUE26 = new TYCodeBox();
                this.CBH12_VALUE26.Name = "CBH12_VALUE26";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE26);
                this.CBH13_VALUE26 = new TYCodeBox();
                this.CBH13_VALUE26.Name = "CBH13_VALUE26";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE26);
                this.CBH14_VALUE26 = new TYCodeBox();
                this.CBH14_VALUE26.Name = "CBH14_VALUE26";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE26);
                this.CBH15_VALUE26 = new TYCodeBox();
                this.CBH15_VALUE26.Name = "CBH15_VALUE26";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE26);
            }
            sCode = "27";
            sCodeName = "접대비번호";
            if (sCode == "27")
            {
                //this.CBH10_VALUE27 = new TYCodeBox();
                //this.CBH10_VALUE27.Name = "CBH10_VALUE27";
                //this.PAN10_VLMI1.AddControl(sCode, sCodeName, CBH10_VALUE27);
                //this.CBH11_VALUE27 = new TYCodeBox();
                //this.CBH11_VALUE27.Name = "CBH11_VALUE27";
                //this.PAN10_VLMI2.AddControl(sCode, sCodeName, CBH11_VALUE27);
                //this.CBH12_VALUE27 = new TYCodeBox();
                //this.CBH12_VALUE27.Name = "CBH12_VALUE27";
                //this.PAN10_VLMI3.AddControl(sCode, sCodeName, CBH12_VALUE27);
                //this.CBH13_VALUE27 = new TYCodeBox();
                //this.CBH13_VALUE27.Name = "CBH13_VALUE27";
                //this.PAN10_VLMI4.AddControl(sCode, sCodeName, CBH13_VALUE27);
                //this.CBH14_VALUE27 = new TYCodeBox();
                //this.CBH14_VALUE27.Name = "CBH14_VALUE27";
                //this.PAN10_VLMI5.AddControl(sCode, sCodeName, CBH14_VALUE27);
                //this.CBH15_VALUE27 = new TYCodeBox();
                //this.CBH15_VALUE27.Name = "CBH15_VALUE27";
                //this.PAN10_VLMI6.AddControl(sCode, sCodeName, CBH15_VALUE27);

                this.TXT10_VALUE27 = new TYTextBox();
                this.TXT10_VALUE27.Name = "TXT10_VALUE27";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE27);
                this.TXT11_VALUE27 = new TYTextBox();
                this.TXT11_VALUE27.Name = "TXT11_VALUE27";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE27);
                this.TXT12_VALUE27 = new TYTextBox();
                this.TXT12_VALUE27.Name = "TXT12_VALUE27";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE27);
                this.TXT13_VALUE27 = new TYTextBox();
                this.TXT13_VALUE27.Name = "TXT13_VALUE27";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE27);
                this.TXT14_VALUE27 = new TYTextBox();
                this.TXT14_VALUE27.Name = "TXT14_VALUE27";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE27);
                this.TXT15_VALUE27 = new TYTextBox();
                this.TXT15_VALUE27.Name = "TXT15_VALUE27";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE27);

            }
            sCode = "28";
            sCodeName = "년월일확인";
            if (sCode == "28")
            {
                this.TXT10_VALUE28 = new TYTextBox();
                this.TXT10_VALUE28.Name = "TXT10_VALUE28";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE28);
                this.TXT11_VALUE28 = new TYTextBox();
                this.TXT11_VALUE28.Name = "TXT11_VALUE28";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE28);
                this.TXT12_VALUE28 = new TYTextBox();
                this.TXT12_VALUE28.Name = "TXT12_VALUE28";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE28);
                this.TXT13_VALUE28 = new TYTextBox();
                this.TXT13_VALUE28.Name = "TXT13_VALUE28";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE28);
                this.TXT14_VALUE28 = new TYTextBox();
                this.TXT14_VALUE28.Name = "TXT14_VALUE28";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE28);
                this.TXT15_VALUE28 = new TYTextBox();
                this.TXT15_VALUE28.Name = "TXT15_VALUE28";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE28);
            }
            sCode = "29";
            sCodeName = "어음번호"; // 받을어음
            if (sCode == "29")
            {
                this.CBH10_VALUE29 = new TYCodeBox();
                this.CBH10_VALUE29.Name = "CBH10_VALUE29";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE29);
                this.CBH11_VALUE29 = new TYCodeBox();
                this.CBH11_VALUE29.Name = "CBH11_VALUE29";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE29);
                this.CBH12_VALUE29 = new TYCodeBox();
                this.CBH12_VALUE29.Name = "CBH12_VALUE29";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE29);
                this.CBH13_VALUE29 = new TYCodeBox();
                this.CBH13_VALUE29.Name = "CBH13_VALUE29";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE29);
                this.CBH14_VALUE29 = new TYCodeBox();
                this.CBH14_VALUE29.Name = "CBH14_VALUE29";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE29);
                this.CBH15_VALUE29 = new TYCodeBox();
                this.CBH15_VALUE29.Name = "CBH15_VALUE29";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE29);
            }
            sCode = "30";
            sCodeName = "외화구분"; // FR
            if (sCode == "30")
            {
                this.CBH10_B2INDX.DummyValue = "FR";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                this.CBH11_B2INDX.DummyValue = "FR";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                this.CBH12_B2INDX.DummyValue = "FR";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                this.CBH13_B2INDX.DummyValue = "FR";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                this.CBH14_B2INDX.DummyValue = "FR";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                this.CBH15_B2INDX.DummyValue = "FR";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);

            }
            sCode = "31";
            sCodeName = "수출외환";
            if (sCode == "31")
            {
                this.TXT10_VALUE31 = new TYTextBox();
                this.TXT10_VALUE31.Name = "TXT10_VALUE31";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE31);
                this.TXT11_VALUE31 = new TYTextBox();
                this.TXT11_VALUE31.Name = "TXT11_VALUE31";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE31);
                this.TXT12_VALUE31 = new TYTextBox();
                this.TXT12_VALUE31.Name = "TXT12_VALUE31";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE31);
                this.TXT13_VALUE31 = new TYTextBox();
                this.TXT13_VALUE31.Name = "TXT13_VALUE31";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE31);
                this.TXT14_VALUE31 = new TYTextBox();
                this.TXT14_VALUE31.Name = "TXT14_VALUE31";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE31);
                this.TXT15_VALUE31 = new TYTextBox();
                this.TXT15_VALUE31.Name = "TXT15_VALUE31";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE31);
            }
            sCode = "32";
            sCodeName = "카드NO"; 
            if (sCode == "32")
            {
                this.CBH10_VALUE32 = new TYCodeBox();
                this.CBH10_VALUE32.Name = "CBH10_VALUE32";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE32);
                this.CBH11_VALUE32 = new TYCodeBox();
                this.CBH11_VALUE32.Name = "CBH11_VALUE32";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE32);
                this.CBH12_VALUE32 = new TYCodeBox();
                this.CBH12_VALUE32.Name = "CBH12_VALUE32";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE32);
                this.CBH13_VALUE32 = new TYCodeBox();
                this.CBH13_VALUE32.Name = "CBH13_VALUE32";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE32);
                this.CBH14_VALUE32 = new TYCodeBox();
                this.CBH14_VALUE32.Name = "CBH14_VALUE32";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE32);
                this.CBH15_VALUE32 = new TYCodeBox();
                this.CBH15_VALUE32.Name = "CBH15_VALUE32";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE32);
            }
            sCode = "33";
            sCodeName = "일자확인9(08)";
            if (sCode == "33")
            {
                this.TXT10_VALUE33 = new TYTextBox();
                this.TXT10_VALUE33.Name = "TXT10_VALUE33";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE33);
                this.TXT11_VALUE33 = new TYTextBox();
                this.TXT11_VALUE33.Name = "TXT11_VALUE33";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE33);
                this.TXT12_VALUE33 = new TYTextBox();
                this.TXT12_VALUE33.Name = "TXT12_VALUE33";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE33);
                this.TXT13_VALUE33 = new TYTextBox();
                this.TXT13_VALUE33.Name = "TXT13_VALUE33";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE33);
                this.TXT14_VALUE33 = new TYTextBox();
                this.TXT14_VALUE33.Name = "TXT14_VALUE33";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE33);
                this.TXT15_VALUE33 = new TYTextBox();
                this.TXT15_VALUE33.Name = "TXT15_VALUE33";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE33);
            }
            sCode = "34";
            sCodeName = "품목코드";
            if (sCode == "34")
            {
                this.CBH10_VALUE34 = new TYCodeBox();
                this.CBH10_VALUE34.Name = "CBH10_VALUE34";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE34);
                this.CBH11_VALUE34 = new TYCodeBox();
                this.CBH11_VALUE34.Name = "CBH11_VALUE34";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE34);
                this.CBH12_VALUE34 = new TYCodeBox();
                this.CBH12_VALUE34.Name = "CBH12_VALUE34";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE34);
                this.CBH13_VALUE34 = new TYCodeBox();
                this.CBH13_VALUE34.Name = "CBH13_VALUE34";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE34);
                this.CBH14_VALUE34 = new TYCodeBox();
                this.CBH14_VALUE34.Name = "CBH14_VALUE34";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE34);
                this.CBH15_VALUE34 = new TYCodeBox();
                this.CBH15_VALUE34.Name = "CBH15_VALUE34";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE34);

                //this.TXT10_VALUE34 = new TYTextBox();
                //this.TXT10_VALUE34.Name = "TXT10_VALUE34";
                //this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_VALUE34);
                //this.TXT11_VALUE34 = new TYTextBox();
                //this.TXT11_VALUE34.Name = "TXT11_VALUE34";
                //this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT11_VALUE34);
                //this.TXT12_VALUE34 = new TYTextBox();
                //this.TXT12_VALUE34.Name = "TXT12_VALUE34";
                //this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT12_VALUE34);
                //this.TXT13_VALUE34 = new TYTextBox();
                //this.TXT13_VALUE34.Name = "TXT13_VALUE34";
                //this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT13_VALUE34);
                //this.TXT14_VALUE34 = new TYTextBox();
                //this.TXT14_VALUE34.Name = "TXT14_VALUE34";
                //this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT14_VALUE34);
                //this.TXT15_VALUE34 = new TYTextBox();
                //this.TXT15_VALUE34.Name = "TXT15_VALUE34";
                //this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT15_VALUE34);

            }
            sCode = "35";
            sCodeName = "예산세목";
            if (sCode == "35")
            {
                this.CBH10_VALUE35 = new TYCodeBox();
                this.CBH10_VALUE35.Name = "CBH10_VALUE35";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE35);
                this.CBH11_VALUE35 = new TYCodeBox();
                this.CBH11_VALUE35.Name = "CBH11_VALUE35";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE35);
                this.CBH12_VALUE35 = new TYCodeBox();
                this.CBH12_VALUE35.Name = "CBH12_VALUE35";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE35);
                this.CBH13_VALUE35 = new TYCodeBox();
                this.CBH13_VALUE35.Name = "CBH13_VALUE35";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE35);
                this.CBH14_VALUE35 = new TYCodeBox();
                this.CBH14_VALUE35.Name = "CBH14_VALUE35";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE35);
                this.CBH15_VALUE35 = new TYCodeBox();
                this.CBH15_VALUE35.Name = "CBH15_VALUE35";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE35);
            }
            sCode = "36";
            sCodeName = "환율";
            if (sCode == "36")
            {
                this.TXT10_VALUE36 = new TYTextBox();
                this.TXT10_VALUE36.Name = "TXT10_VALUE36";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE36);
                this.TXT11_VALUE36 = new TYTextBox();
                this.TXT11_VALUE36.Name = "TXT11_VALUE36";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE36);
                this.TXT12_VALUE36 = new TYTextBox();
                this.TXT12_VALUE36.Name = "TXT12_VALUE36";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE36);
                this.TXT13_VALUE36 = new TYTextBox();
                this.TXT13_VALUE36.Name = "TXT13_VALUE36";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE36);
                this.TXT14_VALUE36 = new TYTextBox();
                this.TXT14_VALUE36.Name = "TXT14_VALUE36";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE36);
                this.TXT15_VALUE36 = new TYTextBox();
                this.TXT15_VALUE36.Name = "TXT15_VALUE36";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE36);
            }
            sCode = "40";
            sCodeName = "자금배부"; // AL
            if (sCode == "40")
            {
                this.CBH10_B2INDX.DummyValue = "AL";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                this.CBH11_B2INDX.DummyValue = "AL";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                this.CBH12_B2INDX.DummyValue = "AL";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                this.CBH13_B2INDX.DummyValue = "AL";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                this.CBH14_B2INDX.DummyValue = "AL";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                this.CBH15_B2INDX.DummyValue = "AL";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
            }
            sCode = "41";
            sCodeName = "외화관리번호";
            if (sCode == "41")
            {
                this.CBH10_VALUE41 = new TYCodeBox();
                this.CBH10_VALUE41.Name = "CBH10_VALUE41";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE41);
                this.CBH11_VALUE41 = new TYCodeBox();
                this.CBH11_VALUE41.Name = "CBH11_VALUE41";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE41);
                this.CBH12_VALUE41 = new TYCodeBox();
                this.CBH12_VALUE41.Name = "CBH12_VALUE41";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE41);
                this.CBH13_VALUE41 = new TYCodeBox();
                this.CBH13_VALUE41.Name = "CBH13_VALUE41";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE41);
                this.CBH14_VALUE41 = new TYCodeBox();
                this.CBH14_VALUE41.Name = "CBH14_VALUE41";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE41);
                this.CBH15_VALUE41 = new TYCodeBox();
                this.CBH15_VALUE41.Name = "CBH15_VALUE41";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE41);
            }
            sCode = "42";
            sCodeName = "FILE-NO";
            if (sCode == "42")
            {
                this.CBH10_VALUE42 = new TYCodeBox();
                this.CBH10_VALUE42.Name = "CBH10_VALUE42";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE42);
                this.CBH11_VALUE42 = new TYCodeBox();
                this.CBH11_VALUE42.Name = "CBH11_VALUE42";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE42);
                this.CBH12_VALUE42 = new TYCodeBox();
                this.CBH12_VALUE42.Name = "CBH12_VALUE42";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE42);
                this.CBH13_VALUE42 = new TYCodeBox();
                this.CBH13_VALUE42.Name = "CBH13_VALUE42";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE42);
                this.CBH14_VALUE42 = new TYCodeBox();
                this.CBH14_VALUE42.Name = "CBH14_VALUE42";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE42);
                this.CBH15_VALUE42 = new TYCodeBox();
                this.CBH15_VALUE42.Name = "CBH15_VALUE42";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE42);

                //this.TXT10_VALUE42 = new TYTextBox();
                //this.TXT10_VALUE42.Name = "TXT10_VALUE42";
                //this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_VALUE42);
                //this.TXT11_VALUE42 = new TYTextBox();
                //this.TXT11_VALUE42.Name = "TXT11_VALUE42";
                //this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT11_VALUE42);
                //this.TXT12_VALUE42 = new TYTextBox();
                //this.TXT12_VALUE42.Name = "TXT12_VALUE42";
                //this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT12_VALUE42);
                //this.TXT13_VALUE42 = new TYTextBox();
                //this.TXT13_VALUE42.Name = "TXT13_VALUE42";
                //this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT13_VALUE42);
                //this.TXT14_VALUE42 = new TYTextBox();
                //this.TXT14_VALUE42.Name = "TXT14_VALUE42";
                //this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT14_VALUE42);
                //this.TXT15_VALUE42 = new TYTextBox();
                //this.TXT15_VALUE42.Name = "TXT15_VALUE42";
                //this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT15_VALUE42);
            }
            sCode = "44";
            sCodeName = "BL-NO";
            if (sCode == "44")
            {
                this.CBH10_VALUE44 = new TYCodeBox();
                this.CBH10_VALUE44.Name = "CBH10_VALUE44";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE44);
                this.CBH11_VALUE44 = new TYCodeBox();
                this.CBH11_VALUE44.Name = "CBH11_VALUE44";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE44);
                this.CBH12_VALUE44 = new TYCodeBox();
                this.CBH12_VALUE44.Name = "CBH12_VALUE44";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE44);
                this.CBH13_VALUE44 = new TYCodeBox();
                this.CBH13_VALUE44.Name = "CBH13_VALUE44";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE44);
                this.CBH14_VALUE44 = new TYCodeBox();
                this.CBH14_VALUE44.Name = "CBH14_VALUE44";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE44);
                this.CBH15_VALUE44 = new TYCodeBox();
                this.CBH15_VALUE44.Name = "CBH15_VALUE44";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE44);

                //this.TXT10_VALUE44 = new TYTextBox();
                //this.TXT10_VALUE44.Name = "TXT10_VALUE44";
                //this.PAN10_VLMI1.AddControl(sCode, sCodeName, TXT10_VALUE44);
                //this.TXT11_VALUE44 = new TYTextBox();
                //this.TXT11_VALUE44.Name = "TXT11_VALUE44";
                //this.PAN10_VLMI2.AddControl(sCode, sCodeName, TXT11_VALUE44);
                //this.TXT12_VALUE44 = new TYTextBox();
                //this.TXT12_VALUE44.Name = "TXT12_VALUE44";
                //this.PAN10_VLMI3.AddControl(sCode, sCodeName, TXT12_VALUE44);
                //this.TXT13_VALUE44 = new TYTextBox();
                //this.TXT13_VALUE44.Name = "TXT13_VALUE44";
                //this.PAN10_VLMI4.AddControl(sCode, sCodeName, TXT13_VALUE44);
                //this.TXT14_VALUE44 = new TYTextBox();
                //this.TXT14_VALUE44.Name = "TXT14_VALUE44";
                //this.PAN10_VLMI5.AddControl(sCode, sCodeName, TXT14_VALUE44);
                //this.TXT15_VALUE44 = new TYTextBox();
                //this.TXT15_VALUE44.Name = "TXT15_VALUE44";
                //this.PAN10_VLMI6.AddControl(sCode, sCodeName, TXT15_VALUE44);

            }
            sCode = "46";
            sCodeName = "차입금결재구분"; // UA
            if (sCode == "46")
            {
                this.CBH10_B2INDX.DummyValue = "UA";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                this.CBH11_B2INDX.DummyValue = "UA";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                this.CBH12_B2INDX.DummyValue = "UA";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                this.CBH13_B2INDX.DummyValue = "UA";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                this.CBH14_B2INDX.DummyValue = "UA";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                this.CBH15_B2INDX.DummyValue = "UA";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
            }

            sCode = "37";
            sCodeName = "출장번호";
            if (sCode == "37")
            {
                this.CBH10_VALUE37 = new TYCodeBox();
                this.CBH10_VALUE37.Name = "CBH10_VALUE37";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE37);
                this.CBH11_VALUE37 = new TYCodeBox();
                this.CBH11_VALUE37.Name = "CBH11_VALUE37";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE37);
                this.CBH12_VALUE37 = new TYCodeBox();
                this.CBH12_VALUE37.Name = "CBH12_VALUE37";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE37);
                this.CBH13_VALUE37 = new TYCodeBox();
                this.CBH13_VALUE37.Name = "CBH13_VALUE37";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE37);
                this.CBH14_VALUE37 = new TYCodeBox();
                this.CBH14_VALUE37.Name = "CBH14_VALUE37";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE37);
                this.CBH15_VALUE37 = new TYCodeBox();
                this.CBH15_VALUE37.Name = "CBH15_VALUE37";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE37);
            }

            sCode = "38";
            sCodeName = "자산관리번호";
            if (sCode == "38")
            {
                this.CBH10_VALUE38 = new TYCodeBox();
                this.CBH10_VALUE38.Name = "CBH10_VALUE38";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE38);
                this.CBH11_VALUE38 = new TYCodeBox();
                this.CBH11_VALUE38.Name = "CBH11_VALUE38";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE38);
                this.CBH12_VALUE38 = new TYCodeBox();
                this.CBH12_VALUE38.Name = "CBH12_VALUE38";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE38);
                this.CBH13_VALUE38 = new TYCodeBox();
                this.CBH13_VALUE38.Name = "CBH13_VALUE38";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE38);
                this.CBH14_VALUE38 = new TYCodeBox();
                this.CBH14_VALUE38.Name = "CBH14_VALUE38";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE38);
                this.CBH15_VALUE38 = new TYCodeBox();
                this.CBH15_VALUE38.Name = "CBH15_VALUE38";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE38);
            }

            sCode = "47";
            sCodeName = "수출신고번호";
            if (sCode == "47")
            {
                this.TXT10_VALUE47 = new TYTextBox();
                this.TXT10_VALUE47.Name = "TXT10_VALUE47";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE47);
                this.TXT11_VALUE47 = new TYTextBox();
                this.TXT11_VALUE47.Name = "TXT11_VALUE47";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE47);
                this.TXT12_VALUE47 = new TYTextBox();
                this.TXT12_VALUE47.Name = "TXT12_VALUE47";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE47);
                this.TXT13_VALUE47 = new TYTextBox();
                this.TXT13_VALUE47.Name = "TXT13_VALUE47";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE47);
                this.TXT14_VALUE47 = new TYTextBox();
                this.TXT14_VALUE47.Name = "TXT14_VALUE47";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE47);
                this.TXT15_VALUE47 = new TYTextBox();
                this.TXT15_VALUE47.Name = "TXT15_VALUE47";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE47);
            }

            sCode = "49";
            sCodeName = "입항일자";
            if (sCode == "49")
            {
                this.TXT10_VALUE49 = new TYTextBox();
                this.TXT10_VALUE49.Name = "TXT10_VALUE49";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE49);
                this.TXT11_VALUE49 = new TYTextBox();
                this.TXT11_VALUE49.Name = "TXT11_VALUE49";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE49);
                this.TXT12_VALUE49 = new TYTextBox();
                this.TXT12_VALUE49.Name = "TXT12_VALUE49";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE49);
                this.TXT13_VALUE49 = new TYTextBox();
                this.TXT13_VALUE49.Name = "TXT13_VALUE49";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE49);
                this.TXT14_VALUE49 = new TYTextBox();
                this.TXT14_VALUE49.Name = "TXT14_VALUE49";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE49);
                this.TXT15_VALUE49 = new TYTextBox();
                this.TXT15_VALUE49.Name = "TXT15_VALUE49";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE49);
            }

            sCode = "50";
            sCodeName = "본 선";
            if (sCode == "50")
            {
                this.CBH10_VALUE50 = new TYCodeBox();
                this.CBH10_VALUE50.Name = "CBH10_VALUE50";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE50);
                this.CBH11_VALUE50 = new TYCodeBox();
                this.CBH11_VALUE50.Name = "CBH11_VALUE50";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE50);
                this.CBH12_VALUE50 = new TYCodeBox();
                this.CBH12_VALUE50.Name = "CBH12_VALUE50";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE50);
                this.CBH13_VALUE50 = new TYCodeBox();
                this.CBH13_VALUE50.Name = "CBH13_VALUE50";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE50);
                this.CBH14_VALUE50 = new TYCodeBox();
                this.CBH14_VALUE50.Name = "CBH14_VALUE50";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE50);
                this.CBH15_VALUE50 = new TYCodeBox();
                this.CBH15_VALUE50.Name = "CBH15_VALUE50";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE50);
            }

            sCode = "51";
            sCodeName = "화 주";
            if (sCode == "51")
            {
                this.CBH10_VALUE51 = new TYCodeBox();
                this.CBH10_VALUE51.Name = "CBH10_VALUE51";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE51);
                this.CBH11_VALUE51 = new TYCodeBox();
                this.CBH11_VALUE51.Name = "CBH11_VALUE51";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE51);
                this.CBH12_VALUE51 = new TYCodeBox();
                this.CBH12_VALUE51.Name = "CBH12_VALUE51";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE51);
                this.CBH13_VALUE51 = new TYCodeBox();
                this.CBH13_VALUE51.Name = "CBH13_VALUE51";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE51);
                this.CBH14_VALUE51 = new TYCodeBox();
                this.CBH14_VALUE51.Name = "CBH14_VALUE51";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE51);
                this.CBH15_VALUE51 = new TYCodeBox();
                this.CBH15_VALUE51.Name = "CBH15_VALUE51";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE51);
            }
            
            sCode = "52";
            sCodeName = "납부기한";
            if (sCode == "52")
            {
                this.TXT10_VALUE52 = new TYTextBox();
                this.TXT10_VALUE52.Name = "TXT10_VALUE52";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE52);
                this.TXT11_VALUE52 = new TYTextBox();
                this.TXT11_VALUE52.Name = "TXT11_VALUE52";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE52);
                this.TXT12_VALUE52 = new TYTextBox();
                this.TXT12_VALUE52.Name = "TXT12_VALUE52";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE52);
                this.TXT13_VALUE52 = new TYTextBox();
                this.TXT13_VALUE52.Name = "TXT13_VALUE52";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE52);
                this.TXT14_VALUE52 = new TYTextBox();
                this.TXT14_VALUE52.Name = "TXT14_VALUE52";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE52);
                this.TXT15_VALUE52 = new TYTextBox();
                this.TXT15_VALUE52.Name = "TXT15_VALUE52";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE52);
            }

            sCode = "53";
            sCodeName = "입고번호";
            if (sCode == "53")
            {
                this.TXT10_VALUE53 = new TYTextBox();
                this.TXT10_VALUE53.Name = "TXT10_VALUE53";
                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE53);
                this.TXT11_VALUE53 = new TYTextBox();
                this.TXT11_VALUE53.Name = "TXT11_VALUE53";
                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE53);
                this.TXT12_VALUE53 = new TYTextBox();
                this.TXT12_VALUE53.Name = "TXT12_VALUE53";
                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE53);
                this.TXT13_VALUE53 = new TYTextBox();
                this.TXT13_VALUE53.Name = "TXT13_VALUE53";
                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE53);
                this.TXT14_VALUE53 = new TYTextBox();
                this.TXT14_VALUE53.Name = "TXT14_VALUE53";
                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE53);
                this.TXT15_VALUE53 = new TYTextBox();
                this.TXT15_VALUE53.Name = "TXT15_VALUE53";
                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE53);
            }


        }
        #endregion

        #region  Description : 계좌번호 (코드 BOX) 셋팅  ---> CBH10_B2INDX_CodeBoxDataBinded()
        private void CBH10_B2INDX_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH10_B2INDX.GetValue().ToString();
            if (this.CBH11_VALUE07 != null)
            {
                this.CBH11_VALUE07.DummyValue = groupCode;
                this.CBH11_VALUE07.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) this.CBH11_VALUE07.Initialize();
            }
            //if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "111031" || this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "211031" )
            //{
            //    if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "111031")
            //    {
            //        this.CBH11_VALUE11.DummyValue = "1";
            //    }
            //    else if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "211031")
            //    {
            //        this.CBH11_VALUE11.DummyValue = "2";
            //    }
            //    this.CBH11_VALUE11.SetReadOnly(string.IsNullOrEmpty(groupCode));
            //    if (this._Isloaded) this.CBH11_VALUE11.Initialize();
            //}
        }
        private void CBH11_B2INDX_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH11_B2INDX.GetValue().ToString();
            if (this.CBH12_VALUE07 != null)
            {
                this.CBH12_VALUE07.DummyValue = groupCode;
                this.CBH12_VALUE07.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) this.CBH12_VALUE07.Initialize();
            }

            //if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "111031" || this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "211031")
            //{
            //    if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "111031")
            //    {
            //        this.CBH11_VALUE11.DummyValue = "1";
            //    }
            //    else if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "211031")
            //    {
            //        this.CBH11_VALUE11.DummyValue = "2";
            //    }
            //    this.CBH11_VALUE11.SetReadOnly(string.IsNullOrEmpty(groupCode));
            //    if (this._Isloaded) this.CBH11_VALUE11.Initialize();
            //}
        }
        private void CBH12_B2INDX_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH12_B2INDX.GetValue().ToString();
            if (this.CBH13_VALUE07 != null)
            {
                this.CBH13_VALUE07.DummyValue = groupCode;
                this.CBH13_VALUE07.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) this.CBH13_VALUE07.Initialize();
            }
        }
        private void CBH13_B2INDX_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH13_B2INDX.GetValue().ToString();
            if (this.CBH14_VALUE07 != null)
            {
                this.CBH14_VALUE07.DummyValue = groupCode;
                this.CBH14_VALUE07.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) this.CBH14_VALUE07.Initialize();
            }
        }
        private void CBH14_B2INDX_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH14_B2INDX.GetValue().ToString();
            if (this.CBH15_VALUE07 != null)
            {
                this.CBH15_VALUE07.DummyValue = groupCode;
                this.CBH15_VALUE07.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) this.CBH15_VALUE07.Initialize();
            }
        }
        private void CBH15_B2INDX_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH15_B2INDX.GetValue().ToString();
            if (this.CBH15_VALUE07 != null)
            {
                this.CBH15_VALUE07.DummyValue = groupCode;
                this.CBH15_VALUE07.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) this.CBH15_VALUE07.Initialize();
            }
        }
        #endregion


        #region Description : 계정과목 필드 변경시 이벤트  ----> CBH01_B2CDAC_CodeBoxDataBinded() 
        private void CBH01_B2CDAC_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (CBH01_B2CDAC.GetValue().ToString().Trim().Length == 8)
            {

                this.PAN10_VLMI1.Initialize();
                this.PAN10_VLMI2.Initialize();
                this.PAN10_VLMI3.Initialize();
                this.PAN10_VLMI4.Initialize();
                this.PAN10_VLMI5.Initialize();
                this.PAN10_VLMI6.Initialize();

                //계정과목 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH01_B2CDAC.GetValue(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    tabControl_Remove();

                    if (dt.Rows[0]["A1CDMI1"].ToString().Trim() != "")
                    {
                        this.PAN10_VLMI1.SetCurCode(dt.Rows[0]["A1CDMI1"].ToString().Trim().Substring(0, 2));
                        this.fsCDMI01 = dt.Rows[0]["A1CDMI1"].ToString().Trim().Substring(0, 2);

                        //은행코드가 없으면 계좌번호 잠금
                        if (dt.Rows[0]["A1CDMI1"].ToString().Trim().Substring(0,2)  == "02")
                        {
                            this.CBH10_B2INDX.OnCodeBoxDataBinded(null, null);
                        }
                    }
                    else
                    {
                        this.fsCDMI01 = "";
                    };

                    if (dt.Rows[0]["A1CDMI2"].ToString().Trim() != "")
                    {
                        this.PAN10_VLMI2.SetCurCode(dt.Rows[0]["A1CDMI2"].ToString().Trim().Substring(0, 2));
                        this.fsCDMI02 = dt.Rows[0]["A1CDMI2"].ToString().Trim().Substring(0, 2);
                        //은행코드가 없으면 계좌번호 잠금
                        if (dt.Rows[0]["A1CDMI2"].ToString().Trim().Substring(0, 2) == "02")
                        {
                            this.CBH11_B2INDX.OnCodeBoxDataBinded(null, null);
                        }

                    }
                    else
                    {
                        this.fsCDMI02 = "";
                    };

                    if (dt.Rows[0]["A1CDMI3"].ToString().Trim() != "")
                    {
                        this.PAN10_VLMI3.SetCurCode(dt.Rows[0]["A1CDMI3"].ToString().Trim().Substring(0, 2));
                        this.fsCDMI03 = dt.Rows[0]["A1CDMI3"].ToString().Trim().Substring(0, 2);
                        //은행코드가 없으면 계좌번호 잠금
                        if (dt.Rows[0]["A1CDMI3"].ToString().Trim().Substring(0, 2) == "02")
                        {
                            this.CBH12_B2INDX.OnCodeBoxDataBinded(null, null);
                        }

                    }
                    else
                    {
                        this.fsCDMI03 = "";
                    };
                    if (dt.Rows[0]["A1CDMI4"].ToString().Trim() != "")
                    {
                        this.PAN10_VLMI4.SetCurCode(dt.Rows[0]["A1CDMI4"].ToString().Trim().Substring(0, 2));
                        this.fsCDMI04 = dt.Rows[0]["A1CDMI4"].ToString().Trim().Substring(0, 2);
                        //은행코드가 없으면 계좌번호 잠금
                        if (dt.Rows[0]["A1CDMI4"].ToString().Trim().Substring(0, 2) == "02")
                        {
                            this.CBH13_B2INDX.OnCodeBoxDataBinded(null, null);
                        }
                    }
                    else
                    {
                        this.fsCDMI04 = "";
                    };
                    if (dt.Rows[0]["A1CDMI5"].ToString().Trim() != "")
                    {
                        this.PAN10_VLMI5.SetCurCode(dt.Rows[0]["A1CDMI5"].ToString().Trim().Substring(0, 2));
                        this.fsCDMI05 = dt.Rows[0]["A1CDMI5"].ToString().Trim().Substring(0, 2);
                        //은행코드가 없으면 계좌번호 잠금
                        if (dt.Rows[0]["A1CDMI5"].ToString().Trim().Substring(0, 2) == "02")
                        {
                            this.CBH14_B2INDX.OnCodeBoxDataBinded(null, null);
                        }
                    }
                    else
                    {
                        this.fsCDMI05 = "";
                    }
                    ;
                    if (dt.Rows[0]["A1CDMI6"].ToString().Trim() != "")
                    {
                        this.PAN10_VLMI6.SetCurCode(dt.Rows[0]["A1CDMI6"].ToString().Trim().Substring(0, 2));
                        this.fsCDMI06 = dt.Rows[0]["A1CDMI6"].ToString().Trim().Substring(0, 2);
                        //은행코드가 없으면 계좌번호 잠금
                        if (dt.Rows[0]["A1CDMI6"].ToString().Trim().Substring(0, 2) == "02")
                        {
                            this.CBH15_B2INDX.OnCodeBoxDataBinded(null, null);
                        }
                    }
                    else
                    {
                        this.fsCDMI06 = "";
                    };

                    // -------------------------------------------------------------------------------------------------------- //
                    // ----------------------------------------    Control Box  처리 시작   ------------------------------------ //
                    // -------------------------------------------------------------------------------------------------------- //
                    /* 접대비 경우  */
                    if (this.fsCDMI01 == "27" || this.fsCDMI02 == "27" || this.fsCDMI03 == "27" ||  
                        this.fsCDMI04 == "27" || this.fsCDMI05 == "27" || this.fsCDMI06 == "27")
                    {
                        fsTabCtl = "RECP"; // 접대비
                        tabControl1_Enable();

                        UP_ASERVECheck("");
                    }

                    // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품 or 관리항목 코드 중 "42" 
                    if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                         this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
                       (dt.Rows[0]["A1CDAC"].ToString().Trim() == "11200300" || dt.Rows[0]["A1CDAC"].ToString().Trim() == "11200101" ||
                        dt.Rows[0]["A1CDAC"].ToString().Trim() == "11200102" || dt.Rows[0]["A1CDAC"].ToString().Trim() == "11200103" ||
                        dt.Rows[0]["A1CDAC"].ToString().Trim() == "11200104" || dt.Rows[0]["A1CDAC"].ToString().Trim() == "11200105" ||
                        dt.Rows[0]["A1CDAC"].ToString().Trim() == "11200188"))
                    {
                        fsTabCtl = "LCCH"; // 무역원가
                        tabControl1_Enable();

                        UP_TDLCCHNF_Check();

                    }

                    // 대변금액에 영향을 받는경우 (Tabcontrol 처리)
                    ///* 외화설정관리 경우  */
                    //if ((this.fsCDMI01 == "41" || this.fsCDMI02 == "41" || this.fsCDMI03 == "41" ||
                    //     this.fsCDMI04 == "41" || this.fsCDMI05 == "41" || this.fsCDMI06 == "41") &&
                    //    (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) > 0 || Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) < 0))
                    //{

                    //    fsTabCtl = "FORE"; // 외화
                    //    tabControl1_Enable();

                    //    UP_TMAC1102WF_Check("");
                    //}
                    // -------------------------------------------------------------------------------------------------------- //
                    // ----------------------------------------    Control Box  처리 종료   ------------------------------------ //
                    // -------------------------------------------------------------------------------------------------------- //


                }

            }
        }
      
        #endregion

        #region Description : 대변금액 Enter 처리시 이벤트  ----> TXT01_B2AMCR_KeyPress()  

        // 대변금액 Enter 처리시  -- UP_CBH01_TabCtl_Changed 이벤트 처리됨
        private void TXT01_B2AMCR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_CBH01_TabCtl_Changed();

                //this.SetFocus(this.CBH01_B2DPAC);
            }
        }

        private void UP_CBH01_TabCtl_Changed()
        {
            if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Length == 8)
            {
                #region MyRegion
                //this.PAN10_VLMI1.Initialize();
                //this.PAN10_VLMI2.Initialize();
                //this.PAN10_VLMI3.Initialize();
                //this.PAN10_VLMI4.Initialize();
                //this.PAN10_VLMI5.Initialize();
                //this.PAN10_VLMI6.Initialize();

                ////계정과목 조회
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH01_B2CDAC.GetValue(), "");
                //DataTable dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)

                //    if (dt.Rows[0]["A1CDMI1"].ToString().Trim() != "")
                //    {
                //        this.PAN10_VLMI1.SetCurCode(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                //        this.fsCDMI01 = dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2);
                //    }
                //    else
                //    {
                //        this.fsCDMI01 = "";
                //    };

                //    if (dt.Rows[0]["A1CDMI2"].ToString().Trim() != "")
                //    {
                //        this.PAN10_VLMI2.SetCurCode(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                //        this.fsCDMI02 = dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2);
                //    }
                //    else
                //    {
                //        this.fsCDMI02 = "";
                //    };

                //    if (dt.Rows[0]["A1CDMI3"].ToString().Trim() != "")
                //    {
                //        this.PAN10_VLMI3.SetCurCode(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                //        this.fsCDMI03 = dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2);
                //    }
                //    else
                //    {
                //        this.fsCDMI03 = "";
                //    };
                //    if (dt.Rows[0]["A1CDMI4"].ToString().Trim() != "")
                //    {
                //        this.PAN10_VLMI4.SetCurCode(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                //        this.fsCDMI04 = dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2);
                //    }
                //    else
                //    {
                //        this.fsCDMI04 = "";
                //    };
                //    if (dt.Rows[0]["A1CDMI5"].ToString().Trim() != "")
                //    {
                //        this.PAN10_VLMI5.SetCurCode(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                //        this.fsCDMI05 = dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2);
                //    }
                //    else
                //    {
                //        this.fsCDMI05 = "";
                //    }
                //    ;
                //    if (dt.Rows[0]["A1CDMI6"].ToString().Trim() != "")
                //    {
                //        this.PAN10_VLMI6.SetCurCode(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                //        this.fsCDMI06 = dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2);
                //    }
                //    else
                //    {
                //        this.fsCDMI06 = "";
                //    };

                //    // -------------------------------------------------------------------------------------------------------- //
                //    // ----------------------------------------    Control Box  처리 시작   ------------------------------------ //
                //    // -------------------------------------------------------------------------------------------------------- //
                //    /* 접대비 경우  */
                //    if (this.fsCDMI01 == "27" || this.fsCDMI02 == "27" || this.fsCDMI03 == "27" ||  // txtfsA1CDMI1
                //        this.fsCDMI04 == "27" || this.fsCDMI05 == "27" || this.fsCDMI06 == "27")
                //    {
                //        fsTabCtl = "RECP"; // 접대비
                //        tabControl1_Enable();

                //        UP_ASERVECheck("");
                //        this.DTP01_TSDTOC.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim());
                //    }
                //    // ---------------------------------------------------------------------------------------------------------------- */
                //    // 대변금액에 영향을 받는경우 (Tabcontrol 처리)

                //    /* 외화설정관리 경우  */
                //    if ((this.fsCDMI01 == "41" || this.fsCDMI02 == "41" || this.fsCDMI03 == "41" ||
                //         this.fsCDMI04 == "41" || this.fsCDMI05 == "41" || this.fsCDMI06 == "41") &&
                //        (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) > 0 || Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) < 0))
                //    {

                //        fsTabCtl = "FORE"; // 외화
                //        tabControl1_Enable();

                //        UP_TMAC1102WF_Check("");
                //    }

                //    /* 입금등록일 경우 */
                //    if ( ((this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "111004") ||  // 111005 외상매출금
                //          (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "21100801") || (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11101002") ) &&   // 매출관련 선수금,선급보험료
                //         (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) > 0) )
                //    {
                //        fsTabCtl = "MONE"; // 입금
                //        tabControl1_Enable();

                //        UP_TMAC1151REF_Check("");
                //    }
                //    // -------------------------------------------------------------------------------------------------------- //
                //    // ----------------------------------------    Control Box  처리 종료   ------------------------------------ //
                //    // -------------------------------------------------------------------------------------------------------- //

                //}

                #endregion
                // -------------------------------------------------------------------------------------------------------- //
                // ----------------------------------------    Control Box  처리 시작   ------------------------------------ //
                // -------------------------------------------------------------------------------------------------------- //
                /* 외화설정관리 경우  */
                if ((this.fsCDMI01 == "41" || this.fsCDMI02 == "41" || this.fsCDMI03 == "41" ||
                     this.fsCDMI04 == "41" || this.fsCDMI05 == "41" || this.fsCDMI06 == "41") &&
                    (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) > 0 || Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) < 0))
                {

                    fsTabCtl = "FORE"; // 외화
                    tabControl1_Enable();

                    UP_TMAC1102WF_Check("");
                }

                /* 입금등록일 경우 */
                if (((this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "111004") ||  // 111005 외상매출금
                      (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "21100801") || (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11101002")) &&   // 매출관련 선수금,선급보험료
                     (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) > 0))
                {
                    fsTabCtl = "MONE"; // 입금
                    tabControl1_Enable();

                    UP_TMAC1151REF_Check("");
                }
                // -------------------------------------------------------------------------------------------------------- //
                // ----------------------------------------    Control Box  처리 종료   ------------------------------------ //
                // -------------------------------------------------------------------------------------------------------- //

            }

        }

        #endregion


        #region Description : 작성부서 자동 처리  ---> CBH01_B2HISAB_Leave()
        private void CBH01_B2HISAB_Leave(object sender, EventArgs e)
        {
            ////사번 조회
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
            //DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            //if (dt_sabun.Rows.Count == 0)
            //{
            //    //this.ShowCustomMessage("사원번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    //this.SetFocus(this.CBH01_B2HISAB);
            //}
            //else
            //{
            //    this.CBH01_B2DPMK.SetValue(dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim());
            //}
        }

        private void CBH01_B2HISAB_MouseLeave(object sender, EventArgs e)
        {
            ////사번 조회
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
            //DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            //if (dt_sabun.Rows.Count == 0)
            //{
            //    //this.ShowCustomMessage("사원번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    //this.SetFocus(this.CBH01_B2HISAB);

            //}
            //else
            //{
            //    this.CBH01_B2DPMK.SetValue(dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim());
            //}
        }

        #endregion

        #region Description : 관리항목 코드 -> Value 리턴 ---> UP_CDMIToVLMI()
        private string UP_CDMIToVLMI(string sCDMIValue, string sCDMI1, string sCDMI2, string sCDMI3, string sCDMI4, string sCDMI5, string sCDMI6,
                             string sVLMI1, string sVLMI2, string sVLMI3, string sVLMI4, string sVLMI5, string sVLMI6)
        {
            string sVlMI = "";

            if (sCDMIValue == "35") sVlMI = "false";

            if (sCDMIValue == sCDMI1) sVlMI = sVLMI1;
            if (sCDMIValue == sCDMI2) sVlMI = sVLMI2;
            if (sCDMIValue == sCDMI3) sVlMI = sVLMI3;
            if (sCDMIValue == sCDMI4) sVlMI = sVLMI4;
            if (sCDMIValue == sCDMI5) sVlMI = sVLMI5;
            if (sCDMIValue == sCDMI6) sVlMI = sVLMI6;

            return sVlMI;
        } 
        #endregion

        #region Description : 선택 라인번호 조회 ---> UP_DisPlayLineNoSq()
        private string UP_DisPlayLineNoSq()
        {
            string sRetrunParam = "";
            //string sNOLN = this.CBO01_B2NOLN.SelectedIndex.ToString(); // 2013.01.31
            string sNOLN = this.CBO01_B2NOLN.SelectedItem.ToString();

            //if (this.CBO01_B2NOLN.SelectedItem != null) { SItem = this.CBO01_B2NOLN.SelectedItem.ToString(); this.TXT01_B2RKAC.SetValue(SItem); };

            //필드 CLEAR
            UP_FieldClear();

            this.fsCDMI01 = "";
            this.fsCDMI02 = "";
            this.fsCDMI03 = "";
            this.fsCDMI04 = "";
            this.fsCDMI05 = "";
            this.fsCDMI06 = "";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29S1G346", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), sNOLN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_B2CDAC.SetValue(dt.Rows[0]["W2CDAC"].ToString().Trim());
                this.CBH01_B2DPAC.DummyValue = dt.Rows[0]["W2DTMK"].ToString().Trim();
                this.CBH01_B2DPAC.SetValue(dt.Rows[0]["W2DPAC"].ToString().Trim());

                if (dt.Rows[0]["W2CDMI1"].ToString().Trim() != "")
                {
                    this.PAN10_VLMI1.SetCurCode(dt.Rows[0]["W2CDMI1"].ToString().Trim().Substring(0, 2));
                    this.fsCDMI01 = dt.Rows[0]["W2CDMI1"].ToString().Trim().Substring(0, 2);
                }
                if (dt.Rows[0]["W2CDMI2"].ToString().Trim() != "")
                {
                    this.PAN10_VLMI2.SetCurCode(dt.Rows[0]["W2CDMI2"].ToString().Trim().Substring(0, 2));
                    this.fsCDMI02 = dt.Rows[0]["W2CDMI2"].ToString().Trim().Substring(0, 2);
                }
                if (dt.Rows[0]["W2CDMI3"].ToString().Trim() != "")
                {
                    this.PAN10_VLMI3.SetCurCode(dt.Rows[0]["W2CDMI3"].ToString().Trim().Substring(0, 2));
                    this.fsCDMI03 = dt.Rows[0]["W2CDMI3"].ToString().Trim().Substring(0, 2);
                }
                if (dt.Rows[0]["W2CDMI4"].ToString().Trim() != "")
                {
                    this.PAN10_VLMI4.SetCurCode(dt.Rows[0]["W2CDMI4"].ToString().Trim().Substring(0, 2));
                    this.fsCDMI04 = dt.Rows[0]["W2CDMI4"].ToString().Trim().Substring(0, 2);
                }
                if (dt.Rows[0]["W2CDMI5"].ToString().Trim() != "")
                {
                    this.PAN10_VLMI5.SetCurCode(dt.Rows[0]["W2CDMI5"].ToString().Trim().Substring(0, 2));
                    this.fsCDMI05 = dt.Rows[0]["W2CDMI5"].ToString().Trim().Substring(0, 2);
                }
                if (dt.Rows[0]["W2CDMI6"].ToString().Trim() != "")
                {
                    this.PAN10_VLMI6.SetCurCode(dt.Rows[0]["W2CDMI6"].ToString().Trim().Substring(0, 2));
                    this.fsCDMI06 = dt.Rows[0]["W2CDMI6"].ToString().Trim().Substring(0, 2);
                }

                /*------------------------------------------------------------------*/
                /*------------- 위 로직 처리후 처리 되어야 함(합치기 안됨) ------------*/
                if (dt.Rows[0]["W2CDMI1"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "35")
                    {
                        this.CBH10_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBH01_B2CDAC.GetValue().ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "29")
                    {

                        string sValue1 = UP_CDMIToVLMI("01", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());

                        this.CBH10_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBO01_B2IDJP.GetValue().ToString().Trim(), this.CBH01_B2HISAB.GetValue().ToString().Trim(), sValue1, dt.Rows[0]["W2CDAC"].ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "09")
                    {
                        this.CBH10_VALUE09.DummyValue = new string[] { fsSessionId ,"" };
                    }
                    else if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "03")
                    {
                        this.CBH10_VALUE03.DummyValue = this.DTP01_B2DTMK.GetString().ToString().Trim();
                    }
                    else if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "41")
                    {
                        string sBankCode = SetDefaultValue(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                        string sGuJaCode = SetDefaultValue(dt.Rows[0]["W2VLMI2"].ToString().Trim());
                        this.CBH10_VALUE41.DummyValue = new string[] { fsSessionId, sBankCode, sGuJaCode, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim(), "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "37")
                    {
                        string sSabun = UP_CDMIToVLMI("05", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH10_VALUE37.DummyValue = new string[] { fsSessionId, sSabun, "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "38")
                    {
                        string sFXNUM = UP_CDMIToVLMI("38", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH10_VALUE38.DummyValue = new string[] { fsSessionId, "1", sFXNUM }; // GetDataSource()
                    }
                    else if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "02")
                    {
                        if (dt.Rows[0]["W2VLMI1"].ToString().Trim().Length != 6)
                        {
                            this.CBH10_B2INDX.DummyValue = "BJ";
                        }
                    }

                    this.PAN10_VLMI1.SetValue(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                }

                if (dt.Rows[0]["W2CDMI2"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "35")
                    {
                        this.CBH11_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBH01_B2CDAC.GetValue().ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "29")
                    {
                        string sValue1 = UP_CDMIToVLMI("01", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());

                        this.CBH11_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBO01_B2IDJP.GetValue().ToString().Trim(), this.CBH01_B2HISAB.GetValue().ToString().Trim(), sValue1, dt.Rows[0]["W2CDAC"].ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "09")
                    {
                        this.CBH11_VALUE09.DummyValue = new string[] { fsSessionId, "" };
                    }
                    else if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "03")
                    {
                        this.CBH11_VALUE03.DummyValue = this.DTP01_B2DTMK.GetString().ToString().Trim();
                    }
                    else if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "41")
                    {
                        string sBankCode = SetDefaultValue(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                        string sGuJaCode = SetDefaultValue(dt.Rows[0]["W2VLMI2"].ToString().Trim());
                        this.CBH11_VALUE41.DummyValue = new string[] { fsSessionId, sBankCode, sGuJaCode, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim(), "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "37")
                    {
                        string sSabun = UP_CDMIToVLMI("05", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH11_VALUE37.DummyValue = new string[] { fsSessionId, sSabun, "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "38")
                    {
                        string sFXNUM = UP_CDMIToVLMI("38", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH11_VALUE38.DummyValue = new string[] { fsSessionId, "1", sFXNUM }; // GetDataSource()
                    }
                    else if (dt.Rows[0]["W2CDMI2"].ToString().Trim() == "02")
                    {
                        if (dt.Rows[0]["W2VLMI2"].ToString().Trim().Length != 6)
                        {
                            this.CBH11_B2INDX.DummyValue = "BJ";
                        }
                    }

                    this.PAN10_VLMI2.SetValue(dt.Rows[0]["W2VLMI2"].ToString().Trim());
                }

                if (dt.Rows[0]["W2CDMI3"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "35")
                    {
                        this.CBH12_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBH01_B2CDAC.GetValue().ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "29")
                    {
                        string sValue1 = UP_CDMIToVLMI("01", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());

                        this.CBH12_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBO01_B2IDJP.GetValue().ToString().Trim(), this.CBH01_B2HISAB.GetValue().ToString().Trim(), sValue1, dt.Rows[0]["W2CDAC"].ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "09")
                    {
                        this.CBH12_VALUE09.DummyValue = new string[] { fsSessionId, "" };
                    }
                    else if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "03")
                    {
                        this.CBH12_VALUE03.DummyValue = this.DTP01_B2DTMK.GetString().ToString().Trim();
                    }
                    else if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "41")
                    {
                        string sBankCode = SetDefaultValue(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                        string sGuJaCode = SetDefaultValue(dt.Rows[0]["W2VLMI2"].ToString().Trim());
                        this.CBH12_VALUE41.DummyValue = new string[] { fsSessionId, sBankCode, sGuJaCode, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim(), "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "37")
                    {
                        string sSabun = UP_CDMIToVLMI("05", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH12_VALUE37.DummyValue = new string[] { fsSessionId, sSabun, "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "38")
                    {
                        string sFXNUM = UP_CDMIToVLMI("38", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH12_VALUE38.DummyValue = new string[] { fsSessionId, "1", sFXNUM }; // GetDataSource()
                    }
                    else if (dt.Rows[0]["W2CDMI3"].ToString().Trim() == "02")
                    {
                        if (dt.Rows[0]["W2VLMI3"].ToString().Trim().Length != 6)
                        {
                            this.CBH12_B2INDX.DummyValue = "BJ";
                        }
                    }

                    this.PAN10_VLMI3.SetValue(dt.Rows[0]["W2VLMI3"].ToString().Trim());
                }

                if (dt.Rows[0]["W2CDMI4"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "35")
                    {
                        this.CBH13_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBH01_B2CDAC.GetValue().ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "29")
                    {
                        string sValue1 = UP_CDMIToVLMI("01", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());

                        this.CBH13_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBO01_B2IDJP.GetValue().ToString().Trim(), this.CBH01_B2HISAB.GetValue().ToString().Trim(), sValue1, dt.Rows[0]["W2CDAC"].ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "09")
                    {
                        this.CBH13_VALUE09.DummyValue = new string[] { fsSessionId, "" };
                    }
                    else if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "03")
                    {
                        this.CBH13_VALUE03.DummyValue = this.DTP01_B2DTMK.GetString().ToString().Trim();
                    }
                    else if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "41")
                    {
                        string sBankCode = SetDefaultValue(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                        string sGuJaCode = SetDefaultValue(dt.Rows[0]["W2VLMI2"].ToString().Trim());
                        this.CBH13_VALUE41.DummyValue = new string[] { fsSessionId, sBankCode, sGuJaCode, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim(), "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "37")
                    {
                        string sSabun = UP_CDMIToVLMI("05", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH13_VALUE37.DummyValue = new string[] { fsSessionId, sSabun, "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "38")
                    {
                        string sFXNUM = UP_CDMIToVLMI("38", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH13_VALUE38.DummyValue = new string[] { fsSessionId, "1", sFXNUM }; // GetDataSource()
                    }
                    else if (dt.Rows[0]["W2CDMI4"].ToString().Trim() == "02")
                    {
                        if (dt.Rows[0]["W2VLMI4"].ToString().Trim().Length != 6)
                        {
                            this.CBH13_B2INDX.DummyValue = "BJ";
                        }
                    }

                    this.PAN10_VLMI4.SetValue(dt.Rows[0]["W2VLMI4"].ToString().Trim());                    
                }

                if (dt.Rows[0]["W2CDMI5"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "35")
                    {
                        this.CBH14_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBH01_B2CDAC.GetValue().ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "29")
                    {
                        string sValue1 = UP_CDMIToVLMI("01", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());

                        this.CBH14_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().Trim().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBO01_B2IDJP.GetValue().ToString().Trim(), this.CBH01_B2HISAB.GetValue().ToString().Trim(), sValue1, dt.Rows[0]["W2CDAC"].ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "09")
                    {
                        this.CBH14_VALUE09.DummyValue = new string[] { fsSessionId, "" };
                    }
                    else if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "03")
                    {
                        this.CBH14_VALUE03.DummyValue = this.DTP01_B2DTMK.GetString().ToString().Trim();
                    }
                    else if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "41")
                    {
                        string sBankCode = SetDefaultValue(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                        string sGuJaCode = SetDefaultValue(dt.Rows[0]["W2VLMI2"].ToString().Trim());
                        this.CBH14_VALUE41.DummyValue = new string[] { fsSessionId, sBankCode, sGuJaCode, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim(), "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "37")
                    {
                        string sSabun = UP_CDMIToVLMI("05", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH14_VALUE37.DummyValue = new string[] { fsSessionId, sSabun, "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "38")
                    {
                        string sFXNUM = UP_CDMIToVLMI("38", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH14_VALUE38.DummyValue = new string[] { fsSessionId, "1", sFXNUM }; // GetDataSource()
                    }
                    else if (dt.Rows[0]["W2CDMI5"].ToString().Trim() == "02")
                    {
                        if (dt.Rows[0]["W2VLMI5"].ToString().Trim().Length != 6)
                        {
                            this.CBH14_B2INDX.DummyValue = "BJ";
                        }
                    }

                    this.PAN10_VLMI5.SetValue(dt.Rows[0]["W2VLMI5"].ToString().Trim());
                }

                if (dt.Rows[0]["W2CDMI6"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "35")
                    {
                        this.CBH15_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBH01_B2CDAC.GetValue().ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "29")
                    {
                        string sValue1 = UP_CDMIToVLMI("01", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                             dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());

                        this.CBH15_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBH01_B2DPAC.GetValue().ToString().Trim(), this.CBO01_B2IDJP.GetValue().ToString().Trim(), this.CBH01_B2HISAB.GetValue().ToString().Trim(), sValue1, dt.Rows[0]["W2CDAC"].ToString().Trim() };
                    }
                    else if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "09")
                    {
                        this.CBH15_VALUE09.DummyValue = new string[] { fsSessionId, "" };
                    }
                    else if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "03")
                    {
                        this.CBH15_VALUE03.DummyValue = this.DTP01_B2DTMK.GetString().ToString().Trim();
                    }
                    else if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "41")
                    {
                        string sBankCode = SetDefaultValue(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                        string sGuJaCode = SetDefaultValue(dt.Rows[0]["W2VLMI2"].ToString().Trim());
                        this.CBH15_VALUE41.DummyValue = new string[] { fsSessionId, sBankCode, sGuJaCode, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim(), "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "37")
                    {
                        string sSabun = UP_CDMIToVLMI("05", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH15_VALUE37.DummyValue = new string[] { fsSessionId, sSabun, "1" };
                    }
                    else if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "38")
                    {
                        string sFXNUM = UP_CDMIToVLMI("38", dt.Rows[0]["W2CDMI1"].ToString().Trim(), dt.Rows[0]["W2CDMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI3"].ToString().Trim(), dt.Rows[0]["W2CDMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2CDMI5"].ToString().Trim(), dt.Rows[0]["W2CDMI6"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI1"].ToString().Trim(), dt.Rows[0]["W2VLMI2"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI3"].ToString().Trim(), dt.Rows[0]["W2VLMI4"].ToString().Trim(),
                                                            dt.Rows[0]["W2VLMI5"].ToString().Trim(), dt.Rows[0]["W2VLMI6"].ToString().Trim());
                        this.CBH15_VALUE38.DummyValue = new string[] { fsSessionId, "1", sFXNUM }; // GetDataSource()
                    }
                    else if (dt.Rows[0]["W2CDMI6"].ToString().Trim() == "02")
                    {
                        if (dt.Rows[0]["W2VLMI6"].ToString().Trim().Length != 6)
                        {
                            this.CBH15_B2INDX.DummyValue = "BJ";
                        }
                    }

                    this.PAN10_VLMI6.SetValue(dt.Rows[0]["W2VLMI6"].ToString().Trim());
                }

                this.TXT01_B2AMDR.SetValue(dt.Rows[0]["W2AMDR"].ToString().Trim());
                this.TXT01_B2AMCR.SetValue(dt.Rows[0]["W2AMCR"].ToString().Trim());
                this.TXT01_B2RKCU.SetValue(dt.Rows[0]["W2RKCU"].ToString().Trim());
                this.TXT01_B2WCJP.SetValue(dt.Rows[0]["W2WCJP"].ToString().Trim());
                this.TXT01_B2RKAC.SetValue(dt.Rows[0]["W2RKAC"].ToString().Trim());

                // -------------------------------------------------------------------------------------------------------- //
                // ----------------------------------------    Control Box  처리 시작   ------------------------------------ //
                // -------------------------------------------------------------------------------------------------------- //
                // 세부 내역 등록 (접대비)

                if (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "27" || dt.Rows[0]["W2CDMI2"].ToString().Trim() == "27" || dt.Rows[0]["W2CDMI3"].ToString().Trim() == "27" ||
                    dt.Rows[0]["W2CDMI4"].ToString().Trim() == "27" || dt.Rows[0]["W2CDMI5"].ToString().Trim() == "27" || dt.Rows[0]["W2CDMI6"].ToString().Trim() == "27")
                {
                    fsTabCtl = "RECP"; // 접대비
                    tabControl1_Enable();

                    this.TXT01_TSDTYY.SetValue(dt.Rows[0]["W2VLMI4"].ToString().Trim().Substring(0, 6));
                    this.TXT01_TSNOSQ.SetValue(dt.Rows[0]["W2VLMI4"].ToString().Trim().Substring(6, 4));

                    UP_ASERVECheck(dt.Rows[0]["W2VLMI4"].ToString().Trim());
                }

                // ---------------------------------------------------------------------------------------------------------------- */
                // 대변금액에 영향을 받는경우 (Tabcontrol 처리)

                /* 외화설정관리 경우  */
                if ((dt.Rows[0]["W2CDMI1"].ToString().Trim() == "41" || dt.Rows[0]["W2CDMI2"].ToString().Trim() == "41" || dt.Rows[0]["W2CDMI3"].ToString().Trim() == "41" ||
                     dt.Rows[0]["W2CDMI4"].ToString().Trim() == "41" || dt.Rows[0]["W2CDMI5"].ToString().Trim() == "41" || dt.Rows[0]["W2CDMI6"].ToString().Trim() == "41") &&
                    (Convert.ToDouble(Get_Numeric(dt.Rows[0]["W2AMDR"].ToString().Trim())) > 0 || Convert.ToDouble(Get_Numeric(dt.Rows[0]["W2AMCR"].ToString().Trim())) < 0))
                {
                    fsTabCtl = "FORE"; // 외화
                    tabControl1_Enable();

                    UP_TMAC1102WF_Check(dt.Rows[0]["W2VLMI6"].ToString().Trim());
                }

                /* 입금등록일 경우 */
                if ( ((dt.Rows[0]["W2CDAC"].ToString().Trim().Substring(0, 6) == "111004") ||  // 111005 외상매출금
                      (dt.Rows[0]["W2CDAC"].ToString().Trim() == "21100801") || (dt.Rows[0]["W2CDAC"].ToString().Trim() == "11101002")) &&   // 매출관련 선수금,선급보험료
                    (Convert.ToDouble(Get_Numeric(dt.Rows[0]["W2AMCR"].ToString().Trim())) > 0))
                {
                    fsTabCtl = "MONE"; // 입금
                    tabControl1_Enable();

                    UP_TMAC1151REF_Check("");
                }

                /* 매입부가세 불공제분 경우  */
                if ((dt.Rows[0]["W2CDAC"].ToString().Trim() == "11103101" || dt.Rows[0]["W2CDAC"].ToString().Trim() == "11103102") &&
                    (dt.Rows[0]["W2CDMI1"].ToString().Trim() == "11"))
                {
                    if (dt.Rows[0]["W2VLMI1"].ToString().Trim() == "54" || dt.Rows[0]["W2VLMI1"].ToString().Trim() == "55" || dt.Rows[0]["W2VLMI1"].ToString().Trim() == "74" || dt.Rows[0]["W2VLMI1"].ToString().Trim() == "75")
                    {
                        fsTabCtl = "TXEX"; //  매입부가세 불공제분
                        tabControl1_Enable();

                        UP_TMAC1102BF_Check(dt.Rows[0]["W2VLMI1"].ToString().Trim());
                    }
                }

                // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품 or 관리항목 "42"
                if ((dt.Rows[0]["W2CDMI1"].ToString().Trim() == "42" || dt.Rows[0]["W2CDMI2"].ToString().Trim() == "42" || dt.Rows[0]["W2CDMI3"].ToString().Trim() == "42" ||
                     dt.Rows[0]["W2CDMI4"].ToString().Trim() == "42" || dt.Rows[0]["W2CDMI5"].ToString().Trim() == "42" || dt.Rows[0]["W2CDMI6"].ToString().Trim() == "42") ||
                   (dt.Rows[0]["W2CDAC"].ToString().Trim() == "11200300" || dt.Rows[0]["W2CDAC"].ToString().Trim() == "11200101" ||
                    dt.Rows[0]["W2CDAC"].ToString().Trim() == "11200102" || dt.Rows[0]["W2CDAC"].ToString().Trim() == "11200103" ||
                    dt.Rows[0]["W2CDAC"].ToString().Trim() == "11200104" || dt.Rows[0]["W2CDAC"].ToString().Trim() == "11200105" ||
                    dt.Rows[0]["W2CDAC"].ToString().Trim() == "11200188" ))
                {
                    if (dt.Rows[0]["W2CDAC"].ToString().Trim() == "11103102") // 부가세-지점
                    {
                        if (dt.Rows[0]["W2VLMI1"].ToString().Trim() != "54" && dt.Rows[0]["W2VLMI1"].ToString().Trim() != "55" && // 불공제분 코드(54,55,74,75) 아닌것면 무역원가 조회
                            dt.Rows[0]["W2VLMI1"].ToString().Trim() != "74" && dt.Rows[0]["W2VLMI1"].ToString().Trim() != "75")
                        {
                            fsTabCtl = "LCCH"; // 무역원가
                            tabControl1_Enable();

                            UP_TDLCCHNF_Check();
                        }
                    }
                    else
                    {
                        fsTabCtl = "LCCH"; // 무역원가
                        tabControl1_Enable();

                        UP_TDLCCHNF_Check();

                    }
                 }

                // -------------------------------------------------------------------------------------------------------- //
                // ----------------------------------------    Control Box  처리 종료   ------------------------------------ //
                // -------------------------------------------------------------------------------------------------------- //

                //자료 존재 & 라인이 삭제 된경우
                if (dt.Rows[0]["W2HIGB"].ToString().Trim() == "D") { sRetrunParam = "D"; }
                else { sRetrunParam = "C"; }
            }
            else
            {
                sRetrunParam = "A";
            }

            return sRetrunParam;
        }
        #endregion


        #region Description : 임시화일에 저장된 내용을 그리드에 조회 함 ---> UP_SetGridMaster()
        // 임시화일에 저장된 내용을 그리드에 조회 함.
        private void UP_SetGridMaster()
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;
            double dDRAmt = 0;
            double dCRAmt = 0;

            sB2DPMK = CBH01_B2DPMK.GetValue().ToString();
            sB2DTMK = DTP01_B2DTMK.GetString().ToString();
            sB2NOSQ = TXT01_B2NOSQ.GetValue().ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29S1X350", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ); //TMAC1102F
            this.FPS91_TY_S_AC_29S1V349.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_AC_29S1V349.CurrentRowCount > 0)
            {
                this.FPS91_TY_S_AC_29S1V349.Select();
                this.FPS91_TY_S_AC_29S1V349_Sheet1.ActiveRowIndex = 0;

                // 차대변 합계구하기
                for (int i = 0; i < this.FPS91_TY_S_AC_29S1V349.CurrentRowCount; i++)
                {
                    dDRAmt = dDRAmt + Convert.ToDouble(this.FPS91_TY_S_AC_29S1V349.GetValue(i, "W2AMDR").ToString());
                    dCRAmt = dCRAmt + Convert.ToDouble(this.FPS91_TY_S_AC_29S1V349.GetValue(i, "W2AMCR").ToString());
                }
                this.TXT01_B2AMDRTOTAL.SetValue(dDRAmt);
                this.TXT01_B2AMCRTOTAL.SetValue(dCRAmt);

                // 차/대변 차이금액
                if (dDRAmt - dCRAmt > 0)
                {
                    this.TXT01_B2GAP.SetValue(dDRAmt - dCRAmt);
                }
                else
                {
                    this.TXT01_B2GAP.SetValue(dCRAmt - dDRAmt);
                }

                //this.CBO01_B2NOLN.SetValue(this.FPS91_TY_S_AC_29S1V349.GetValue(0, "W2NOLN").ToString());
                //
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29S1X350", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ); //TMAC1102F
            DataSet ds = this.DbConnector.ExecuteDataSet();

            UP_SetCombo(this.CBO01_B2NOLN, ds);

            UP_FieldClear(); // 필더 Clear 
            this.CBH01_B2CDAC.SetReadOnly(true); // 계정과목 잠금

            this.CBO01_B2NOLN.SelectedIndex = 0; // 순번을 "추가" 상태로 만들기 위함
        }
        #endregion

        #region Description : 그리드 선택시 처리 메소드 ----> FPS91_TY_S_AC_29S1V349_CellDoubleClick
        private void FPS91_TY_S_AC_29S1V349_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sBtValue = "";

            //this.CBO01_B2NOLN.SetValue(this.FPS91_TY_S_AC_29S1V349.GetValue("W2NOLN").ToString()); 2013.01.31
            //this.CBO01_B2NOLN.SelectedIndex = Convert.ToInt16(this.FPS91_TY_S_AC_29S1V349.GetValue("W2NOLN").ToString());

            this.CBO01_B2NOLN.SelectedIndex = this.FPS91_TY_S_AC_29S1V349.ActiveRowIndex + 1; //
            if (this.CBO01_B2NOLN.SelectedItem != null) { this.CBO01_B2NOLN.SelectedItem = this.CBO01_B2NOLN.SelectedItem.ToString(); };

            tabControl_Remove();  // 세부 내역 잠그기(접대비,외화관리,입금표,불공제,무역원가)

            sBtValue = UP_DisPlayLineNoSq();

            //필드lock = false
            UP_FieldLock(false);

            this.CBH01_B2CDAC.SetReadOnly(true); // 계정과목 잠그기

            if (sBtValue == "C")
            {
                //승인전표일경우 버튼 false
                if (this.txtJunPyoGubn == "2")
                {
                    UP_ImgBtnDisPlay("false", false, false, false, false);
                }
                else
                {
                    UP_ImgBtnDisPlay("false", false, true, true, true);
                }
            }
            else
            {
                //승인전표일경우 버튼 false
                if (this.txtJunPyoGubn == "2")
                {
                    UP_ImgBtnDisPlay("false", false, false, false, false);
                }
                else
                {
                    UP_ImgBtnDisPlay("false", true, false, false, true);
                }
            }

            this.SetFocus(this.CBH01_B2DPAC.CodeText);  
        }

        #endregion


        #region Description :적요 필드 TXT 박스에서 Enter KEY 발생 이후 포커스 이동)
        private void TXT01_B2RKAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품
                if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                     this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
                   (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
                    this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
                    this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
                    this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188"))
                {
                    this.SetFocus(this.TXT01_FILENUM);
                }
                else
                {
                    if (this.BTN61_INP.Visible == false)
                    {
                        Timer tmr = new Timer();

                        tmr.Tick += delegate
                        {
                            tmr.Stop();
                            this.BTN61_EDIT.Focus();
                        };

                        tmr.Interval = 100;
                        tmr.Start();

                        //this.SetFocus(this.BTN61_EDIT);
                    }
                    else
                    {
                        Timer tmr = new Timer();

                        tmr.Tick += delegate
                        {
                            tmr.Stop();
                            this.BTN61_INP.Focus();
                        };

                        tmr.Interval = 100;
                        tmr.Start();

                        //this.SetFocus(this.BTN61_INP);
                    }
                }
            }
        }

        private void TXT01_B2RKCU_Leave(object sender, EventArgs e)
        {
            /* 매입부가세 불공제분 경우  */
            if ((this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11103101" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11103102") &&
                (this.fsCDMI01 == "11"))
            {
                string sTXVLMI = this.PAN10_VLMI1.GetValue().ToString();

                if (sTXVLMI == "54" || sTXVLMI == "55" || sTXVLMI == "74" || sTXVLMI == "75")
                {
                    fsTabCtl = "TXEX"; //  매입부가세 불공제분
                    tabControl1_Enable();

                    UP_TMAC1102BF_Check(sTXVLMI);
                }
            }
        }

        #endregion

        #region Description : TXT01_B2RKAC_KeyDown 이벤트
        private void TXT01_B2RKAC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품
                if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                     this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
                   (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
                    this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
                    this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
                    this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188"))
                {
                    this.SetFocus(this.TXT01_FILENUM);
                }
                else
                {
                    if (this.BTN61_INP.Visible == false)
                    {
                        Timer tmr = new Timer();

                        tmr.Tick += delegate
                        {
                            tmr.Stop();
                            this.BTN61_EDIT.Focus();
                        };

                        tmr.Interval = 100;
                        tmr.Start();

                        //this.SetFocus(this.BTN61_EDIT);
                    }
                    else
                    {
                        Timer tmr = new Timer();

                        tmr.Tick += delegate
                        {
                            tmr.Stop();
                            this.BTN61_INP.Focus();
                        };

                        tmr.Interval = 100;
                        tmr.Start();

                        //this.SetFocus(this.BTN61_INP);
                    }
                }
            }
        }
        #endregion


        // --------------------------------   전표 관련 사항  ------------------------------------ //

        #region Description : 전표발행 처리(전표발생)
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //전표 생성 함수 호출 (필수 J ----> 미승인 전표 생성시 사용)
            this.DbConnector.CommandClear();

            // 23.02.20 미승인전표 테스트
            //this.DbConnector.Attach("TY_P_AC_BACE6622", fsATCRGB, fsSessionId, "");  // TYSCMLIB.SP_GB_AC_NORECOJPPRO_TEST

            // 23.02.20 미승인전표 SP 수정 후 소스
            this.DbConnector.Attach("TY_P_AC_29C80960", fsATCRGB, fsSessionId, "");  // TYSCMLIB.SP_GB_AC_NORECOJPPRO



            // 21.10.12 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_29C80960", "J", fsSessionId, "");  // TYSCMLIB.SP_GB_AC_NORECOJPPRO
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2BF5D359", fsSessionId);  // ACAGSENDMF 삭제 (전표 관련된 내용을 수정후 재생성시 처리하기 위함)
                this.DbConnector.ExecuteNonQuery();

                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                // 무역 실제원가 등록
                this.DbConnector.CommandClear(); // CBO_EXPENSE
                this.DbConnector.Attach("TY_P_AC_2C37I813", this.CBH01_B2HISAB.GetValue().ToString().Trim(), fsSessionId, 
                                                            this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));
                this.DbConnector.ExecuteNonQuery();

                UP_Business_Press_Save_Del("INS"); // 출장자료 정리 처리(전표번호 세팅)

                //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                this.ShowMessage("TY_M_AC_25O8K620");
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29D5B004", fsSessionId);
                DataTable dtresult = this.DbConnector.ExecuteDataTable();
                if (dtresult.Rows.Count > 0)
                {
                    if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                    {
                        //전표번호 받아오기
                        string sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                        if (sJpno.Trim() != "")
                        {
                            //fsB2DPMK = sJpno.Substring(0, 6);
                            //fsB2DTMK = sJpno.Substring(7, 8);
                            //fsB2NOSQ = sJpno.Substring(16, 3);

                            //UP_Set_ExistJunPyo();
                        }
                    }
                }
            }

            this.SetFocus(this.DTP01_B2DTMK);  
        } 
        #endregion

        #region Description : 전표발행 전 체크 로직(전표발생)
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsATCRGB = "J";

            // 1.기존 미승인전표 존재시 기존 미승인 전표 삭제 후 전표 생성처리 함
            // 2.기존 미승인전표 미존재시 전표 생성 처리 함

            //미승인 전표 미 존재 확인 (존재시에 기존전표에 대한 체크(수정 발행) , 미존재시 임시화일에 대한 전표 처리)
            string sChk_NorRec = string.Empty;
            string sOUTMSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B7BT153", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim()); // ADSLGLF
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count > 0)
            {
                if (dt_adsl.Rows[0]["B2NOJP"].ToString().Trim() != "")
                {
                    this.ShowCustomMessage("승인된 전표이므로 삭제 할수 없음!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.DTP01_B2DTMK);
                    e.Successed = false;
                    return;
                };

                sChk_NorRec = "N";
            }
            else
            {
                sChk_NorRec = "Y";
            }

            //string sIN_PARAM = this.CBH01_B2DPMK.GetValue().ToString().Trim() + "-" + this.DTP01_B2DTMK.GetString().Trim() + "-" + Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim());

            if (sChk_NorRec == "N") 
            {
                DataTable dt = new DataTable();

                // 미승인전표 삭제시 헤더파일 체크
                this.DbConnector.CommandClear();                
                this.DbConnector.Attach("TY_P_AC_BACDV621", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["B1AUGN"].ToString() == "A") // 자동전표 발행
                    {
                        fsATCRGB = "A";
                    }
                    else // 미승인전표 발행
                    {
                        fsATCRGB = "J";
                    }
                }

                // 임시 미승인전표 차/대변 합계 금액 비교
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_CCNAU428", fsSessionId.ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("차/대변 합계 금액을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_B2NOLN);
                    e.Successed = false;
                    return;
                }

                //미승인전표 체크 함수 호출
                sOUTMSG = "";
                this.DbConnector.CommandClear(); // TYSCMLIB.SP_GB_AC_NORECOJPDELCHK  (ADSLGLF 삭제전 체크)
                this.DbConnector.Attach("TY_P_AC_2B71A154", "J", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()), fsSessionId, "");
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_B2NOLN);
                    e.Successed = false;
                    return;
                }
                else
                {
                    UP_Business_Press_Save_Del("DEL"); // 출장자료 정리 처리

                    //미승인전표 삭제 함수 호출
                    sOUTMSG = "";
                    this.DbConnector.CommandClear();  // TYSCMLIB.SP_GB_AC_NORECOJPDEL (ADSLGLF 삭제)
                    this.DbConnector.Attach("TY_P_AC_2B71D155", "J", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()), fsSessionId, "");
                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    if (sOUTMSG.Substring(0, 2) == "ER")
                    {
                        this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBO01_B2NOLN);
                        e.Successed = false;
                        return;
                    }

                    // 무역 실제원가 내역 삭제
                    this.DbConnector.CommandClear(); // CBO_EXPENSE
                    this.DbConnector.Attach("TY_P_AC_2C37E811", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));
                    this.DbConnector.ExecuteNonQuery();
                }
            }

            /* ------------------------------------------------------------------------------------------------------------------ */
            // 매입부가세 전표중 신용카드사용분 계정이 존재 할때 신용카드번호가 같은 신용카드이어야 함(부가세 개발시 적용함 2013.12.12)
            int iCnt = 0;

             
            this.DbConnector.CommandClear(); // 매입부가세 중 신용카드 사용분 찾기 ( TMAC1102F )
            this.DbConnector.Attach("TY_P_AC_3CC1Y735", fsSessionId,
                                                        this.CBH01_B2DPMK.GetValue().ToString().Trim(), 
                                                        this.DTP01_B2DTMK.GetString().ToString().Trim(), 
                                                        Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt != 0)
            {
                this.DbConnector.CommandClear(); // 전표에서 신용카드 번호 찾기( TMAC1102F )
                this.DbConnector.Attach("TY_P_AC_3CC21736", fsSessionId,
                                             this.CBH01_B2DPMK.GetValue().ToString().Trim(),
                                             this.DTP01_B2DTMK.GetString().ToString().Trim(),
                                             Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));

                DataTable dt_tmac1102f = this.DbConnector.ExecuteDataTable();
                if (dt_tmac1102f.Rows.Count > 1)
                {
                    this.ShowCustomMessage("부가세 전표에 다른 신용카드번호 등록 할수 없음(회계팀 문의)!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_B2NOLN);
                    e.Successed = false;
                    return;
                }
            }

            /* ------------------------------------------------------------------------------------------------------------------ */

            string sIN_PARAM = this.CBH01_B2DPMK.GetValue().ToString().Trim() + "-" + this.DTP01_B2DTMK.GetString().Trim() + "-" + Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim());
            //미승인 전표 생성을 위한 SP호출 파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7O959", fsSessionId, this.ProgramNo, this.CBH01_B2HISAB.GetValue(), "A",
                                                        this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetString(), sIN_PARAM, "",
                                                        "", "", Employer.EmpNo);
            this.DbConnector.ExecuteTranQueryList();

            // 미승인 전표 생성시 각종 체크를 진행 //
            sOUTMSG = "";
            this.DbConnector.CommandClear();  // TYSCMLIB.SP_GB_AC_NORECOJPCHK (TMAC1102F 체크)
            this.DbConnector.Attach("TY_P_AC_2BF55355", "J", fsSessionId, "");  
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2BF5D359", fsSessionId);  // ACAGSENDMF 삭제 (전표 관련된 내용을 수정후 재생성시 처리하기 위함)
                this.DbConnector.ExecuteNonQuery();

                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBO01_B2NOLN);
                e.Successed = false;
                return;
            }
        } 
        #endregion


        #region Description : 전표출력 버튼 처리(전표출력)
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2AU2M916",
                this.CBH01_B2DPMK.GetValue().ToString(),
                this.DTP01_B2DTMK.GetString().ToString(),
                this.TXT01_B2NOSQ.GetValue().ToString(), // 시작 번호
                this.TXT01_B2NOSQ.GetValue().ToString()  // 종료 번호
                );

            if (Convert.ToDouble(this.DTP01_B2DTMK.GetString().ToString().Substring(0, 4)) > 2014)
            {
                SectionReport rpt = new TYACBJ0012R();
                // 세로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                }
            }
            else
            {
                SectionReport rpt = new TYACBJ001R();
                // 세로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                }
            }

            this.SetFocus(this.DTP01_B2DTMK);

        }
        #endregion

        #region Description : 전표출력 전 체크 로직(전표출력)
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //미승인 전표 미 존재시 출력 않됨
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B7BT153", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim()); // ADSLGLF
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count == 0)
            {
                this.SetFocus(this.DTP01_B2DTMK);
                this.ShowCustomMessage("미승인전표가 존재 하지 않습니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        } 
        #endregion


        #region Description : 전표취소 버튼 처리(전표취소)
        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            UP_Business_Press_Save_Del("DEL"); // 출장자료 정리 처리

            //미승인전표 삭제 함수 호출 (실제 삭제)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B71D155", "J", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()), fsSessionId, "");
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.SetFocus(this.DTP01_B2DTMK);
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                // 무역 실제원가 내역 삭제
                this.DbConnector.CommandClear(); // CBO_EXPENSE
                this.DbConnector.Attach("TY_P_AC_2C37E811", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));
                this.DbConnector.ExecuteNonQuery();

                this.ShowCustomMessage("삭제 처리 완료 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            this.SetFocus(this.DTP01_B2DTMK);
        } 
        #endregion

        #region Description : 전표취소 전 체크 로직(전표취소)
        private void BTN61_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sATCRGB = string.Empty;

            DataTable dt = new DataTable();

            // 미승인전표 삭제시 헤더파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_BACDV621", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["B1AUGN"].ToString() == "A") // 자동전표 발행
                {
                    sATCRGB = "A";
                }
                else // 미승인전표 발행
                {
                    sATCRGB = "J";
                }
            }

            if (sATCRGB.ToString() == "A")
            {
                this.ShowCustomMessage("현업에서 발행한 자동 전표는 전표 취소가 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_B2DTMK);
                e.Successed = false;
                return;
            }

            // 1. 미승인 전표 존재 체크 및 승인확인
            // 2. 미승인 전표 삭제 관련 체크
            // 3. 미승인 전표 삭제

            if (Convert.ToDouble(this.DTP01_B2DTMK.GetString().Trim().ToString()) <= 20130100)
            {
                this.ShowCustomMessage("2013년 이전 전표는 처리 할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_B2DTMK);
                e.Successed = false;
                return ;
            }

            //미승인 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B7BT153", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim()); // ADSLGLF
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count == 0)
            {
                this.SetFocus(this.DTP01_B2DTMK);
                this.ShowCustomMessage("미승인전표가 존재 하지 않습니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            else
            {
                if (dt_adsl.Rows[0]["B2NOJP"].ToString().Trim() != "")
                {
                    this.SetFocus(this.DTP01_B2DTMK);
                    this.ShowCustomMessage("승인된 전표이므로 삭제 할수 없음!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                };
            }

            bool bMsg = this.ShowCustomMessage("삭제하시겠습니까?", "확인", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

            if (bMsg == false)
            {
                e.Successed = false;
                return;
            }

            string sOUTMSG = string.Empty;
            //미승인전표 체크 함수 호출
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B71A154", "J", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()), fsSessionId, "");
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.SetFocus(this.DTP01_B2DTMK);
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            else
            {
                return;
            }

        } 
        #endregion


        #region Description : 확인버튼(전표확인)
        private void BTN61_CONFIRM_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Description : 확인 전 체크 로직 (전표확인)
        private void BTN61_CONFIRM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;
            string sJubNo = string.Empty;
            string sAIHWANMNo = string.Empty;

            int iCnt = 0;

            this.HiddenOK = "true";
            this.txtJunPyoGubn = "1";

            this.CBH01_B2DPAC.DummyValue = this.DTP01_B2DTMK.GetString();

            sB2DPMK = CBH01_B2DPMK.GetValue().ToString();
            sB2DTMK = DTP01_B2DTMK.GetString().ToString();
            sB2NOSQ = TXT01_B2NOSQ.GetValue().ToString();

            // 미승인전표 임시화일 전체삭제(TMAC1102F)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB4S685", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
            this.DbConnector.ExecuteNonQuery();
            // 접대비 임시화일 전체삭제(TMAC1102SF)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB4T687", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
            this.DbConnector.ExecuteNonQuery();
            //외화관리 임시화일 전체삭제(TMAC1102WF)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB4U688", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
            this.DbConnector.ExecuteNonQuery();
            //입금표 임시화일 전체삭제(TMAC1151REF - DELETE)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B2AM002", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
            this.DbConnector.ExecuteNonQuery();
            //불공제 임시화일 전체삭제(TMAC1102BF)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2BJ5F471", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
            this.DbConnector.ExecuteNonQuery();
            //LC비용내역 임시 전체삭제(NTDLCCHNF - DELETE)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C38P816", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
            this.DbConnector.ExecuteNonQuery();


            string sProcedure = string.Empty;

            // 23.02.20 수정 후 소스
            sProcedure = "TY_P_AC_D2KEX612";

            // 23.02.20 수정 전 소스
            //sProcedure = "TY_P_AC_2AB3H673";

            // 미승인 전표조회 후 각종 임시화일에 저장
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure.ToString(), sB2DPMK, sB2DTMK, sB2NOSQ);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //미승인전표 -> 임시파일 입력 (미승인 -> 임시파일 등록 ADSLGLF -> TMAC1102F)
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_AC_29C7K957", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
                this.DbConnector.ExecuteNonQuery();

                // 23.02.20 수정 후 소스
                this.CBH01_B2HISAB.SetValue(dt.Rows[0]["B1NOMK"].ToString());

                // 23.02.20 수정 전 소스
                //this.CBH01_B2HISAB.SetValue(dt.Rows[0]["B2HISAB"].ToString());

                this.CBO01_B2IDJP.SetValue(dt.Rows[0]["B2IDJP"].ToString());

                if (dt.Rows[0]["B2NOJP"].ToString() != "")
                {
                    txtJunPyoGubn = "2";
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    /*  접대비 임시파일에 넣기  */
                    if (dt.Rows[i]["B2CDMI1"].ToString() == "27" || dt.Rows[i]["B2CDMI2"].ToString() == "27" ||
                        dt.Rows[i]["B2CDMI3"].ToString() == "27" || dt.Rows[i]["B2CDMI4"].ToString() == "27" ||
                        dt.Rows[i]["B2CDMI5"].ToString() == "27" || dt.Rows[i]["B2CDMI6"].ToString() == "27")
                    {

                        sJubNo = UP_CDMIToVLMI("27", dt.Rows[i]["B2CDMI1"].ToString(), dt.Rows[i]["B2CDMI2"].ToString(),
                                                     dt.Rows[i]["B2CDMI3"].ToString(), dt.Rows[i]["B2CDMI4"].ToString(),
                                                     dt.Rows[i]["B2CDMI5"].ToString(), dt.Rows[i]["B2CDMI6"].ToString(),
                                                     dt.Rows[i]["B2VLMI1"].ToString(), dt.Rows[i]["B2VLMI2"].ToString(),
                                                     dt.Rows[i]["B2VLMI3"].ToString(), dt.Rows[i]["B2VLMI4"].ToString(),
                                                     dt.Rows[i]["B2VLMI5"].ToString(), dt.Rows[i]["B2VLMI6"].ToString());

                        if (sJubNo.Length != 10)
                        {
                            this.ShowCustomMessage("접대비번호가 올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }

                        //접대비임시Table 입력 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AB5P690", sJubNo.Substring(0, 4), sJubNo.Substring(4, 2), sJubNo.Substring(6, 4));
                        DataTable dt1 = this.DbConnector.ExecuteDataTable();
                        if (dt1.Rows.Count > 0)
                        {
                            try
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_AC_2AB4L678",
                                                         fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, dt.Rows[i]["B2NOLN"].ToString(),
                                                         dt1.Rows[0]["B7DTYY"].ToString(), dt1.Rows[0]["B7DTMM"].ToString(),
                                                         dt1.Rows[0]["B7NOSQ"].ToString(), dt1.Rows[0]["B7DTOC"].ToString(),
                                                         dt1.Rows[0]["B7DEID"].ToString(), dt1.Rows[0]["B7NOCL"].ToString(),
                                                         dt1.Rows[0]["B7NMCP"].ToString(), dt1.Rows[0]["B7ADCL"].ToString(),
                                                         dt1.Rows[0]["B7NMRP"].ToString(), dt1.Rows[0]["B7NOCC"].ToString(),
                                                         dt1.Rows[0]["B7REMK"].ToString(), dt1.Rows[0]["B7NOMK"].ToString(),
                                                         dt1.Rows[0]["B7AMSE"].ToString(), dt1.Rows[0]["B7CGSE"].ToString(),
                                                         dt1.Rows[0]["B7JPNO"].ToString(), dt1.Rows[0]["B7GUBN"].ToString());

                                this.DbConnector.ExecuteNonQueryList();

                                //iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar(0));
                                //if (iCnt > 0)
                                //{
                                //    this.ShowCustomMessage("접대비 임시 화일에 입력중 오류 발생!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                //    e.Successed = false;
                                //    return;
                                //}

                            }
                            catch (Exception ex)
                            {
                                string sMessage = string.Empty;
                                sMessage = ex.ToString();
                                this.ShowCustomMessage("접대비 임시 화일에 입력중 오류 발생!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                            //finally
                            //{
                            //}
                        }
                    } // End .. 접대비

                    /*  외화설정 임시파일에 넣기  */
                    iCnt = 0;
                    if (dt.Rows[i]["B2CDMI1"].ToString() == "41" || dt.Rows[i]["B2CDMI2"].ToString() == "41" ||
                        dt.Rows[i]["B2CDMI3"].ToString() == "41" || dt.Rows[i]["B2CDMI4"].ToString() == "41" ||
                        dt.Rows[i]["B2CDMI5"].ToString() == "41" || dt.Rows[i]["B2CDMI6"].ToString() == "41" &&
                        (Convert.ToDouble(dt.Rows[i]["B2AMDR"].ToString()) > 0 || Convert.ToDouble(dt.Rows[i]["B2AMCR"].ToString()) < 0))
                    {

                        sAIHWANMNo = UP_CDMIToVLMI("41", dt.Rows[i]["B2CDMI1"].ToString(), dt.Rows[i]["B2CDMI2"].ToString(),
                                                         dt.Rows[i]["B2CDMI3"].ToString(), dt.Rows[i]["B2CDMI4"].ToString(),
                                                         dt.Rows[i]["B2CDMI5"].ToString(), dt.Rows[i]["B2CDMI6"].ToString(),
                                                         dt.Rows[i]["B2VLMI1"].ToString(), dt.Rows[i]["B2VLMI2"].ToString(),
                                                         dt.Rows[i]["B2VLMI3"].ToString(), dt.Rows[i]["B2VLMI4"].ToString(),
                                                         dt.Rows[i]["B2VLMI5"].ToString(), dt.Rows[i]["B2VLMI6"].ToString());

                        if (sAIHWANMNo.Length != 8)
                        {
                            this.ShowCustomMessage("외화설정관리번호가 올바르지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }

                        //외화관리 임시Table 입력 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AB6A696", sAIHWANMNo.Substring(0, 4), sAIHWANMNo.Substring(4, 4));
                        DataTable dt2 = this.DbConnector.ExecuteDataTable();
                        if (dt2.Rows.Count > 0)
                        {
                            try
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_AC_2AB4N682",
                                                         fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, dt.Rows[i]["B2NOLN"].ToString(),
                                                         dt2.Rows[0]["IHYEAR"].ToString(),
                                                         dt2.Rows[0]["IHNOSQ"].ToString(),
                                                         dt2.Rows[0]["IHBANK"].ToString(),
                                                         dt2.Rows[0]["IHGUJA"].ToString(),
                                                         dt2.Rows[0]["IHGUIP"].ToString(),
                                                         dt2.Rows[0]["IHGUCD"].ToString(),
                                                         dt2.Rows[0]["IHGONG"].ToString(),
                                                         dt2.Rows[0]["IHGUBN"].ToString(),
                                                         dt2.Rows[0]["IHYUL"].ToString(),
                                                         dt2.Rows[0]["IHIAMT"].ToString());

                                this.DbConnector.ExecuteNonQueryList();

                                //iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                                //if (iCnt > 0)
                                //{
                                //    this.ShowCustomMessage("외화관리 임시 화일에 입력중 오류 발생", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                //    e.Successed = false;
                                //    return;
                                //}
                            }
                            catch
                            {
                                this.ShowCustomMessage("외화관리 임시 화일에 입력중 오류 발생", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                    } // End .. 외화관리

                } // End .. For

                //입금표 -> 입금표 임시파일 입력 (입금표 -> 임시파일 등록 ACRECEMF -> TMAC1151REF)
                string sJPNO = sB2DPMK + sB2DTMK + Set_Fill3(sB2NOSQ);
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AC1N706", fsSessionId, sJPNO);
                this.DbConnector.ExecuteNonQuery();

                //부가세 불공제 -> 부가세 불공제 임시파일 입력 (부가세 불공제 -> 부가세 불공제 임시화일 등록 ACBDDMF -> TMAC1102BF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2BJ5N472", fsSessionId, sB2DPMK , sB2DTMK, sB2NOSQ);
                this.DbConnector.ExecuteNonQuery();

                // LC비용내역파일조회 -> 임시 Lc비용내역파일 (원가화일 --> NTDLCCHNF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C31W797", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
                this.DbConnector.ExecuteNonQuery();


                UP_SetGridMaster();

                this.SetFocus(this.CBO01_B2NOLN);

            }
            else  // 미승인 화일 미존재
            {
                /* 전표번호 전체를 입력한경우 */
                if (Get_Numeric(sB2NOSQ) != "0")
                {
                    this.ShowCustomMessage("전표번호가 존재하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2NOSQ);
                    e.Successed = false;
                    return;
                }

                if (UP_KeyCheck() == true)
                {
                    //미승인 전표 번호 생성(미승인 등록)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AO52819", "GL", sB2DPMK, sB2DTMK, ""); // SP 호출 TYSCMLIB.SP_GB_AC_AUTOPFSEQ
                    string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    if (sOUTMSG.Substring(0, 1) == "E")
                    {
                        this.ShowCustomMessage("전표번호 생성중 오류 발생(AUTOPF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2NOSQ);
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        this.TXT01_B2NOSQ.SetValue(Set_Fill3(sOUTMSG));

                        this.TXT01_B2AMDRTOTAL.SetValue("");
                        this.TXT01_B2AMCRTOTAL.SetValue("");
                        UP_ComBoLineClear(); //라인번호 클리어
                        UP_FieldClear();
                        this.FPS91_TY_S_AC_29S1V349.Initialize();

                        this.SetFocus(this.CBO01_B2NOLN);
                    }
                }
                else
                {
                    e.Successed = false;
                    return;
                }

            };

            if (this.txtJunPyoGubn == "1")
            {
                UP_ImgBtnDisPlay("false", false, false, false, true);
            }
            else
            {
                UP_ImgBtnDisPlay("false", false, false, false, false);
            }

            this.SetFocus(this.CBO01_B2NOLN);
            
        }
        #endregion
        
        // ----------------------------   라인 번호 관련 사항  ------------------------------------ //

        #region Description : 조회 버튼 처리 (라인번호)
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //this.PAN10_VLMI1.Initialize();
            //this.PAN10_VLMI2.Initialize();
            //this.PAN10_VLMI3.Initialize();
            //this.PAN10_VLMI4.Initialize();
            //this.PAN10_VLMI5.Initialize();
            //this.PAN10_VLMI6.Initialize();

        }
        #endregion

        #region Description : 조회전 체크 로직 (라인번호)
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (this.HiddenOK != "true")
            {
                this.ShowCustomMessage("확인버튼 클릭후 작업하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2DPMK.CodeText);
                e.Successed = false;
                return;
            }

            if (UP_KeyCheck() == false)
            {
                e.Successed = false;
                return;
            }

            //전표번호 확인 작업
            if (Get_Numeric(this.TXT01_B2NOSQ.GetValue().ToString().Trim()) == "0")
            {
                this.ShowCustomMessage("라인번호를 확인해주세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_B2NOSQ);
                e.Successed = false;
                return;
            }

            ///* 추가선택시  */
            ///this.CBO01_B2NOLN.SelectedItem
            if (this.CBO01_B2NOLN.SelectedIndex.ToString().Trim() == "0" || this.CBO01_B2NOLN.SelectedIndex.ToString().Trim() == "-1" )
            {
                string sB2NOLN = string.Empty;

                UP_SetGridMaster();

                //필드lock = false
                UP_FieldLock(false);
                
                tabControl_Remove();  // 세부 내역 잠그기(접대비,외화관리,입금표,불공제,무역원가)
                UP_FieldClear();      // 필드 CLEAR

                if (this.txtJunPyoGubn == "1")
                {
                    UP_ImgBtnDisPlay("false", true, false, false, true);
                }
                else
                {
                    UP_ImgBtnDisPlay("false", false, false, false, false);
                }

                // 임시화일에 마직막 전표 번호를 가지고 옮
                this.DbConnector.CommandClear();  // TMAC1102F
                this.DbConnector.Attach("TY_P_AC_2AO4D818", fsSessionId, this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetString(), this.TXT01_B2NOSQ.GetValue());
                sB2NOLN = this.DbConnector.ExecuteScalar().ToString();

                sB2NOLN = Set_Fill2(Convert.ToString(Int32.Parse(sB2NOLN) + 1));
                if (Int32.Parse(sB2NOLN) > 99)
                {
                    this.ShowCustomMessage("라인번호가 99 개를 넘을수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_B2NOLN);
                    e.Successed = false;
                    return;
                }

                this.CBH01_B2DPAC.SetValue(this.CBH01_B2DPMK.GetValue().ToString().Trim());

                if (Convert.ToInt32(sB2NOLN) > 1)
                {
                    string sLineSeq;
                    sLineSeq = Set_Fill2(Convert.ToString(Convert.ToInt32(sB2NOLN)));

                    this.CBO01_B2NOLN.Items.Add(sLineSeq);
                    this.CBO01_B2NOLN.Items[this.CBO01_B2NOLN.Items.Count - 1] = Convert.ToInt32(sLineSeq).ToString();
                    this.CBO01_B2NOLN.SelectedIndex = this.CBO01_B2NOLN.Items.Count - 1;
                }
                else
                {
                    this.CBO01_B2NOLN.Items.Clear();
                    this.CBO01_B2NOLN.Items.Add("임시");
                    this.CBO01_B2NOLN.Items[this.CBO01_B2NOLN.Items.Count - 1] = "추가";
                    this.CBO01_B2NOLN.Items.Add(sB2NOLN);
                    //this.CBO01_B2NOLN.Items[this.CBO01_B2NOLN.Items.Count - 1] = "1"; // 2013.01.31

                    this.CBO01_B2NOLN.SelectedIndex = this.CBO01_B2NOLN.Items.Count - 1;  //  
                }

                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                
            }
            else  /* 라인번호 선택시 */
            {
                string sBtValue = "";
                //필드 CLEAR
                //UP_FieldClear();
                tabControl_Remove();  // 세부 내역 잠그기(접대비,외화관리,입금표,불공제,무역원가)
                sBtValue = UP_DisPlayLineNoSq();

                //라인이 삭제된 경우
                if (sBtValue == "D")
                {
                    this.ShowCustomMessage("해당 전표 라인은 삭제 되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_B2NOLN);
                    e.Successed = false;
                    return;
                }

                //필드lock = false
                UP_FieldLock(false);
                this.CBH01_B2CDAC.SetReadOnly(true); // 계정과목 잠그기

                //조회만 가능한 전표인지 등록,수정,삭제가 가능한 전표인지 구분하여 버튼 조정
                if (this.txtJunPyoGubn == "1")
                {
                    if (sBtValue == "C")
                    {
                        UP_ImgBtnDisPlay("false", false, true, true, true);
                    }
                    else
                    {
                        UP_ImgBtnDisPlay("false", true, false, false, true);
                    }
                }
                else
                {
                    UP_ImgBtnDisPlay("false", false, false, false, false);
                }

                this.SetFocus(this.CBH01_B2DPAC.CodeText);
            }
        }
        #endregion


        #region Description : 입력 버튼 처리 (라인번호)
        private void BTN61_INP_Click(object sender, EventArgs e)
        {
            UP_ScreenFile_Save();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29DA5966", this.ControlFactory, "02"); // 저장
            this.DbConnector.ExecuteNonQuery();

             // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품
            if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                 this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
               (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188") && (this.CBH01_B2DPAC.GetValue().ToString().Substring(0, 1) == "B"))
            {
                UP_Trad_Press_Save_Del("INS"); // 무역원가 처리 (등록)
            }

            UP_SetGridMaster();

            //버튼 잠금
            this.BTN61_INP.Visible = false; // 라인 입력
            this.BTN61_EDIT.Visible = false;// 라인수정
            this.BTN61_REM.Visible = false; // 라인삭제

            this.SetFocus(this.CBO01_B2NOLN);
        }
        #endregion

        #region Description : 입력 전 체크 로직 (라인번호)
        private void BTN61_INP_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRetrun = true;

            // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품
            if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                 this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
               (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188") && (this.CBH01_B2DPAC.GetValue().ToString().Substring(0, 1) == "B"))
            {
                bRetrun = UP_TradField_Check();
                if (bRetrun == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            bRetrun = UP_FieldValueCheck("A");
            if (bRetrun == false)
            {
                e.Successed = false;
                return;
            }

        } 
        #endregion


        #region Description : 수정 버튼 처리 (라인번호)
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            UP_ScreenFile_Save();

            this.DAT02_W2HIGB.SetValue("C");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB2P672", this.ControlFactory, "02"); // 수정
            this.DbConnector.ExecuteNonQuery();

            // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품
            if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                 this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
               (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188") && (this.CBH01_B2DPAC.GetValue().ToString().Substring(0,1) =="B"))
            {
                UP_Trad_Press_Save_Del("UPD"); // 무역원가 처리 
            }

            UP_SetGridMaster();

            //버튼 잠금
            this.BTN61_INP.Visible = false; // 라인 입력
            this.BTN61_EDIT.Visible = false;// 라인수정
            this.BTN61_REM.Visible = false; // 라인삭제

            this.SetFocus(this.CBO01_B2NOLN);
        }
        #endregion

        #region Description : 수정 전 체크 로직  (라인번호)
        private void BTN61_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRetrun = true;

            // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품
            if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                 this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
               (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188") && (this.CBH01_B2DPAC.GetValue().ToString().Substring(0, 1) == "B"))
            {
                bRetrun = UP_TradField_Check();
                if (bRetrun == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            bRetrun = UP_FieldValueCheck("C");
            if (bRetrun == false)
            {
                e.Successed = false;
                return;
            }

        }
        #endregion


        #region Description : 삭제 버튼 처리 (라인번호)
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            UP_ScreenFile_Save();

            this.DAT02_W2HIGB.SetValue("D");

            this.DbConnector.CommandClear(); //TMAC1102F
            this.DbConnector.Attach("TY_P_AC_2AQ8X835", this.DAT02_W2SSID.GetValue().ToString().Trim(), this.DAT02_W2DPMK.GetValue().ToString().Trim(), this.DAT02_W2DTMK.GetValue().ToString().Trim(),
                                                        this.DAT02_W2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString()); // 
            this.DbConnector.ExecuteNonQuery();

            // 세부 내역 등록 (무역원가 ) --> 미착상품 , 내수상품 ,수입상품,수출상품,중계상품 ,B/L도상품,기타상품
            if ((this.fsCDMI01 == "42" || this.fsCDMI02 == "42" || this.fsCDMI03 == "42" ||
                 this.fsCDMI04 == "42" || this.fsCDMI05 == "42" || this.fsCDMI06 == "42") ||
               (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
                this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188") && (this.CBH01_B2DPAC.GetValue().ToString().Substring(0, 1) == "B"))
            {
                UP_Trad_Press_Save_Del("DEL"); // 무역원가 처리 (삭제)
            }

            UP_SetGridMaster();

            //버튼 잠금
            this.BTN61_INP.Visible = false; // 라인 입력
            this.BTN61_EDIT.Visible = false;// 라인수정
            this.BTN61_REM.Visible = false; // 라인삭제

            this.SetFocus(this.CBO01_B2NOLN);
        }
        #endregion

        #region Description : 삭제 전 체크 로직  (라인번호)
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRetrun = true;

            bRetrun = UP_FieldValueCheck("D");

            if (bRetrun == false)
            {
                e.Successed = false;
                return;
            }

        }
        #endregion


        #region Description : 반제 원천번호 처리 버튼 (---- 팝 업 ----)
        private void BTN61_BTWCJP_Click(object sender, EventArgs e)
        {

            UP_GetACDMIMF(this.CBH01_B2CDAC.GetValue().ToString().Trim());

            /* 반제계정일경우 반제조회화면을 보여준다 */
            if (SetDefaultValue(txtfsA1TAG07) == "Y")
            {
                UP_SetVLMIVALUE() ;  // 화면에 등록된 내용을 가지고 옮

                string sRtnValue1 = string.Empty;  // 거래처
                string sRtnValue2 = string.Empty;  // 은행
                string sRtnValue3 = string.Empty;  // 계정과목
                string sRtnValue4 = string.Empty;  // 사번

                sRtnValue3 = this.CBH01_B2CDAC.GetValue().ToString().Trim();  // 계정과목

                switch (txtfsA1CDMI1.Trim())
                {
                    //거래처
                    case "01":
                        sRtnValue1 = fsVLMI01.Trim();
                        break;
                    //은행
                    case "02":
                        sRtnValue2 = fsVLMI01.Trim();
                        break;
                    //사번	 
                    case "05":
                        sRtnValue4 = fsVLMI01.Trim();
                        break;
                }
                switch (txtfsA1CDMI2.Trim())
                {
                    //거래처
                    case "01":
                        sRtnValue1 = fsVLMI02.Trim();
                        break;
                    //은행
                    case "02":
                        sRtnValue2 = fsVLMI02.Trim();
                        break;
                    //사번	 
                    case "05":
                        sRtnValue4 = fsVLMI02.Trim();
                        break;
                }
                switch (txtfsA1CDMI3.Trim())
                {
                    //거래처
                    case "01":
                        sRtnValue1 = fsVLMI03.Trim();
                        break;
                    //은행
                    case "02":
                        sRtnValue2 = fsVLMI03.Trim();
                        break;
                    //사번	 
                    case "05":
                        sRtnValue4 = fsVLMI03.Trim();
                        break;
                }
                switch (txtfsA1CDMI4.Trim())
                {
                    //거래처
                    case "01":
                        sRtnValue1 = fsVLMI04.Trim();
                        break;
                    //은행
                    case "02":
                        sRtnValue2 = fsVLMI04.Trim();
                        break;
                    //사번	 
                    case "05":
                        sRtnValue4 = fsVLMI04.Trim();
                        break;
                }
                switch (txtfsA1CDMI5.Trim())
                {
                    //거래처
                    case "01":
                        sRtnValue1 = fsVLMI05.Trim();
                        break;
                    //은행
                    case "02":
                        sRtnValue2 = fsVLMI05.Trim();
                        break;
                    //사번	 
                    case "05":
                        sRtnValue4 = fsVLMI05.Trim();
                        break;
                }
                switch (txtfsA1CDMI6.Trim())
                {
                    //거래처
                    case "01":
                        sRtnValue1 = fsVLMI06.Trim();
                        break;
                    //은행
                    case "02":
                        sRtnValue2 = fsVLMI06.Trim();
                        break;
                    //사번	 
                    case "05":
                        sRtnValue4 = fsVLMI06.Trim();
                        break;
                }

                //반제연결 구분
                if (SetDefaultValue(txtfsA1TAG11.Trim()) == "1")   //거래처
                {
                    sRtnValue2 = "";
                    sRtnValue4 = "";
                }
                else if (SetDefaultValue(txtfsA1TAG11.Trim()) == "2") //은행
                {
                    sRtnValue1 = "";
                    sRtnValue4 = "";
                }
                else if (SetDefaultValue(txtfsA1TAG11.Trim()) == "3") //사번
                {
                    sRtnValue1 = "";
                    sRtnValue2 = "";
                }

                // ssid , 작성부서,작성일자, 순번, 전표구분, 작성사번 (생성정보)
                // 거래처 ,은행 ,계정과목 ,사번 (관리항목 정보)

                TYAZBJ01C4 popup = new TYAZBJ01C4(fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), Set_Fill2(this.CBO01_B2NOLN.SelectedItem.ToString()),
                                                  this.CBO01_B2IDJP.GetValue().ToString().Trim(), this.CBH01_B2HISAB.GetValue().ToString().ToUpper().Trim(),
                                                  sRtnValue1, sRtnValue2, sRtnValue3, sRtnValue4);

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    UP_SetGridMaster();

                    this.SetFocus(this.CBO01_B2NOLN);
                }
            }
        } 
        #endregion

        // ------------------------------------------------------------------------------------- //

        #region Description : 입력,수정시 필드 입력값 체크 루틴  ------------------------------> UP_FieldValueCheck()
        private bool UP_FieldValueCheck(string sOption)
        {

            /* 작업 변수 정의 */
            bool bFlag = false;
            bool bTrueAndFalse = false;

            //string sMessage;
            string sMyCode = string.Empty, sSubCode = string.Empty;
            string sWK_NONR = string.Empty;
            string sWK_BanAMT = "0";
            string sWK_BanAMBJ = "0";
            string sWK_BanAMJN = "0";
            string sWK_DRAMT = "0";
            string sWK_CRAMT = "0";
            string sWK_IHWAMT = "0";
            string sWK_W2CDAC = string.Empty;
            string sWK_HISAB = string.Empty;
            string sReturnValue = string.Empty;
            int iCnt = 0;

            //접대비 초과금액 체크 임시변수
            string sWK_YSAMT = "0";
            string sWK_GLAMT = "0";

            /* 화면자료 변수 */
            string sB2DTMK = string.Empty, sB2DPMK = string.Empty;
            string sB2NOSQ = string.Empty, sB2NOLN = string.Empty;
            string sB2CDAC = string.Empty;
            string sB2DPAC = string.Empty;
            string sB2IDJP = string.Empty;
            string sB2HISAB = string.Empty;

            string sB2AMDR = string.Empty, sB2AMCR = string.Empty;
            string sB2VLMI1 = string.Empty, sB2VLMI2 = string.Empty, sB2VLMI3 = string.Empty, sB2VLMI4 = string.Empty, sB2VLMI5 = string.Empty, sB2VLMI6 = string.Empty;

            sB2DPMK = this.CBH01_B2DPMK.GetValue().ToString();  //작성부서
            sB2DTMK = Get_Numeric(this.DTP01_B2DTMK.GetString().ToString());  //작성일자
            sB2NOSQ = Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString());
            sB2NOLN = Set_Fill2(this.CBO01_B2NOLN.SelectedItem.ToString());

            sB2CDAC = this.CBH01_B2CDAC.GetValue().ToString();  // 계정과목코드
            sB2DPAC = this.CBH01_B2DPAC.GetValue().ToString();  // 귀속부서
            sB2IDJP = this.CBO01_B2IDJP.GetValue().ToString();  // 전표구분

            sB2HISAB = CBH01_B2HISAB.GetValue().ToString();

            sB2AMDR = Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString());
            sB2AMCR = Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString());

            // 수정 ,삭제,등록시 처리 않됨

            if (Convert.ToDouble(this.DTP01_B2DTMK.GetString().Trim().ToString()) <= 20130100)
            {
                this.ShowCustomMessage("2013년 이전 전표는 처리 할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }

            //계정과목
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString()) == "")
            {
                this.ShowCustomMessage("계정과목을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }
            else
            {
                if (Convert.ToDouble(this.DTP01_B2DTMK.GetString().Trim().ToString()) >= 20210101)
                {
                    if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString()) == "11103102" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString()) == "21103102")
                    {
                        this.ShowCustomMessage("사용할 수 없는 계정과목입니다. 계정 과목을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }
                }
            }


            if (int.Parse(Get_Date(this.DTP01_B2DTMK.GetValue().ToString())) >= 20190101)
            {
                if (double.Parse(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString())) != 0)
                {
                    if (this.CBH01_B2CDAC.GetValue().ToString() == "11101001")
                    {
                        this.ShowCustomMessage("차변에 올수 없는 계정과목입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }
                }
            }

            UP_SetVLMIVALUE(); // 계정과목 관리항목에 값을 가지고 화면에 있는 관리항목 값을 처리

            sB2VLMI1 = this.fsVLMI01;
            sB2VLMI2 = this.fsVLMI02;
            sB2VLMI3 = this.fsVLMI03;
            sB2VLMI4 = this.fsVLMI04;
            sB2VLMI5 = this.fsVLMI05;
            sB2VLMI6 = this.fsVLMI06;

            //귀속부서
            if (SetDefaultValue(this.CBH01_B2DPAC.GetValue().ToString()) == "")
            {
                this.ShowCustomMessage("귀속부서를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2DPAC.CodeText);
                return false;
            }

            /* 1- 계정과목 확인 */
            UP_GetACDMIMF(sB2CDAC);

            // 22.12.26 황성환 요청
            // 매입부가세-본점(11103101)이면서 관리항목값이 59,79일 경우 차/대변 금액은 모두 0이어야 함
            if (sB2CDAC.ToString() == "11103101")
            {
                if((SetDefaultValue(this.txtfsA1CDMI1.Trim()) == "11") & (sB2VLMI1.ToString() == "59" || sB2VLMI1.ToString() =="79"))
                {
                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("차변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMDR);
                        return false;
                    }

                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("대변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMCR);
                        return false;
                    }
                }

                if ((SetDefaultValue(this.txtfsA1CDMI2.Trim()) == "11") & (sB2VLMI2.ToString() == "59" || sB2VLMI2.ToString() == "79"))
                {
                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("차변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMDR);
                        return false;
                    }

                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("대변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMCR);
                        return false;
                    }
                }

                if ((SetDefaultValue(this.txtfsA1CDMI3.Trim()) == "11") & (sB2VLMI3.ToString() == "59" || sB2VLMI3.ToString() == "79"))
                {
                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("차변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMDR);
                        return false;
                    }

                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("대변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMCR);
                        return false;
                    }
                }

                if ((SetDefaultValue(this.txtfsA1CDMI4.Trim()) == "11") & (sB2VLMI4.ToString() == "59" || sB2VLMI4.ToString() == "79"))
                {
                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("차변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMDR);
                        return false;
                    }

                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("대변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMCR);
                        return false;
                    }
                }

                if ((SetDefaultValue(this.txtfsA1CDMI5.Trim()) == "11") & (sB2VLMI5.ToString() == "59" || sB2VLMI5.ToString() == "79"))
                {
                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("차변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMDR);
                        return false;
                    }

                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("대변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMCR);
                        return false;
                    }
                }

                if ((SetDefaultValue(this.txtfsA1CDMI6.Trim()) == "11") & (sB2VLMI6.ToString() == "59" || sB2VLMI6.ToString() == "79"))
                {
                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("차변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMDR);
                        return false;
                    }

                    if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0)
                    {
                        this.ShowCustomMessage("대변 금액을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMCR);
                        return false;
                    }
                }
            }

            if (SetDefaultValue(this.txtfsA1TAG02.Trim()) != "Y")
            {
                this.ShowCustomMessage("실계정 전표가 아닙니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }
            /* 3- 현금계정은 취소 */
            if (sB2CDAC.Substring(0, 6) == "111001")
            {
                this.ShowCustomMessage("현금계정은 사용할 수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }

            /* 5- 매출부가세 및 매입부가세 95.08.31일까지 전표계정이었다가 
             *    9월에 공장별로 계정 나누어짐 시산표출력을 위하여 전표항목에 "Y"
             *    로 표기함   	    */

            if (sB2CDAC == "21103100" || sB2CDAC == "11103100")   // 21101000  , 1300500   11103100
            {
                this.ShowCustomMessage("실계정전표가 아닙니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }

            /* 6- 외상매출계정은 취소 */
            /*-- 매출 입금전표도 미승인에서 작업 하기로 함...  */
            if ((sB2CDAC.Substring(0, 6) == "111004") && // 111005
                (sB2CDAC != "11100499") &&               // 11100599
                (sB2DPMK.Trim() != "A10300") &&
                (sB2DPMK.Trim() != "A10400") &&
                (sB2DPMK.Trim() != "A20100") &&
                (sB2DPMK.Trim() != "A10200") &&
                (sB2DPMK.Trim() != "T40100") &&
                (Convert.ToDouble(sB2AMDR) > 0))
            {
                this.ShowCustomMessage("외상매출계정은 사용할수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }

            if ((sB2CDAC.Substring(0, 6) == "111004") &&  // 111005
                (sB2CDAC != "11100499") &&                // 11100599
                (sB2DPMK.Trim() != "A10300") &&
                (sB2DPMK.Trim() != "A10400") &&
                (sB2DPMK.Trim() != "A20100") &&
                (sB2DPMK.Trim() != "A10200") &&
                (sB2DPMK.Trim() != "T40100") &&
                (Convert.ToDouble(sB2AMCR) < 0))
            {
                this.ShowCustomMessage("외상매출계정은 사용할수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }

            string sB2CDACTemp = string.Empty;
            string sB2DPACTemp1 = string.Empty;
            string sB2DPACTemp2 = string.Empty;

            sB2CDACTemp = sB2CDAC.Substring(0, 4);
            sB2DPACTemp1 = sB2DPAC.Substring(0, 1);
            sB2DPACTemp2 = sB2DPAC.Substring(0, 2);

            /* 해당귀속 부서에 대한 계정과목 체크 */
            // 4241 = 운영 , 4411 = 판매 , 4412 = 무역 , 4413 = 전산 ,  4421 = 일반
            if (sB2CDACTemp == "4241" || sB2CDACTemp == "4411" ||
                sB2CDACTemp == "4412" || sB2CDACTemp == "4421" || sB2CDACTemp == "4413")
            {
                if (((sB2CDACTemp == "4241") && (sB2DPACTemp2 == "S1" || sB2DPACTemp2 == "S2" || sB2DPACTemp2 == "T1" || sB2DPACTemp2 == "T2" || sB2DPACTemp2 == "T3" || sB2DPAC.Substring(0, 4) == "S301")) ||
                    ((sB2CDACTemp == "4411") && (sB2DPACTemp2 == "E3" || sB2DPACTemp2 == "T4" || sB2DPACTemp2 == "S4" || sB2DPAC.Substring(0, 4) == "S302")) ||
                    ((sB2CDACTemp == "4412") && (sB2DPACTemp1 == "O" || sB2DPACTemp1 == "B")) ||
                    ((sB2CDACTemp == "4421") && (sB2DPACTemp1 == "A" || sB2DPACTemp1 == "C")))
                {
                }
                else
                {
                    this.ShowCustomMessage("부서와 계정이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2CDAC.CodeText);
                    return false;
                }
            }

            /* 11- 접대비 계정 확인 
			*     접대비번호는 수정할수없다
			*     미승인전표가 생성되지 안았을경우 (전표처리이전) 
			*     접대비에 미승인전표가 있으면 처리안됨
			*     접대비에 해당하는 거래처인지 체크
			*     접대비번호는 관리항목 4번만 체크함    */

            string sJunbDaeBiNumber = string.Empty;

            if ((SetDefaultValue(this.txtfsA1TAG09.Trim()) == "Y") &&
               ((SetDefaultValue(this.txtfsA1CDMI1.Trim()) == "27") || (SetDefaultValue(this.txtfsA1CDMI2.Trim()) == "27") ||
                (SetDefaultValue(this.txtfsA1CDMI3.Trim()) == "27") || (SetDefaultValue(this.txtfsA1CDMI4.Trim()) == "27") ||
                (SetDefaultValue(this.txtfsA1CDMI5.Trim()) == "27") || (SetDefaultValue(this.txtfsA1CDMI6.Trim()) == "27")))
            {
                sJunbDaeBiNumber = "";

                /*  발행된 전표를 수정하는 경우 접대비번호는 수정할 수 없다(삭제후 등록)  */
                this.DbConnector.CommandClear(); //ADSLGLF
                this.DbConnector.Attach("TY_P_AC_2AI5H746", sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN); // 미승인전표
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    UP_SetVLMIVALUE();

                    if ((SetDefaultValue(this.txtfsA1CDMI1.Trim()) == "27" && (dt.Rows[0]["B2VLMI1"].ToString() != SetDefaultValue(this.fsVLMI01))) ||
                        (SetDefaultValue(this.txtfsA1CDMI2.Trim()) == "27" && (dt.Rows[0]["B2VLMI2"].ToString() != SetDefaultValue(this.fsVLMI02))) ||
                        (SetDefaultValue(this.txtfsA1CDMI3.Trim()) == "27" && (dt.Rows[0]["B2VLMI3"].ToString() != SetDefaultValue(this.fsVLMI03))) ||
                        (SetDefaultValue(this.txtfsA1CDMI4.Trim()) == "27" && (dt.Rows[0]["B2VLMI4"].ToString() != SetDefaultValue(this.fsVLMI04))) ||
                        (SetDefaultValue(this.txtfsA1CDMI5.Trim()) == "27" && (dt.Rows[0]["B2VLMI5"].ToString() != SetDefaultValue(this.fsVLMI05))) ||
                        (SetDefaultValue(this.txtfsA1CDMI6.Trim()) == "27" && (dt.Rows[0]["B2VLMI6"].ToString() != SetDefaultValue(this.fsVLMI06))))
                    {
                        #region MyRegion
                        this.ShowCustomMessage("접대비번호를 수정할수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (SetDefaultValue(this.txtfsA1CDMI1.Trim()) == "27")
                        {
                            if (PAN10_VLMI1.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI1.CurControl); }
                            return false;
                        }
                        if (SetDefaultValue(this.txtfsA1CDMI2.Trim()) == "27")
                        {
                            if (PAN10_VLMI1.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI1.CurControl); }
                            return false;
                        }
                        if (SetDefaultValue(this.txtfsA1CDMI3.Trim()) == "27")
                        {
                            if (PAN10_VLMI3.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI3.CurControl); }
                            return false;
                        }
                        if (SetDefaultValue(this.txtfsA1CDMI4.Trim()) == "27")
                        {
                            if (PAN10_VLMI4.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI4.CurControl); }
                            return false;
                        }
                        if (SetDefaultValue(this.txtfsA1CDMI5.Trim()) == "27")
                        {
                            if (PAN10_VLMI5.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI5.CurControl); }
                            return false;
                        }
                        if (SetDefaultValue(this.txtfsA1CDMI6.Trim()) == "27")
                        {
                            if (PAN10_VLMI6.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI6.CurControl); }
                            return false;
                        }
                        #endregion
                    }
                }
                #region MyRegion

                /* 접대비번호 자리수 확인 */
                if (SetDefaultValue(this.txtfsA1CDMI1) == "27")
                {
                    sJunbDaeBiNumber = SetDefaultValue(this.fsVLMI01);

                    if (this.fsVLMI01.Length != 10)
                    {
                        this.ShowCustomMessage("접대비번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI1.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI1.CurControl); }
                        return false;
                    }
                }
                if (SetDefaultValue(this.txtfsA1CDMI2) == "27")
                {
                    sJunbDaeBiNumber = SetDefaultValue(this.fsVLMI02);
                    if (this.fsVLMI02.Length != 10)
                    {
                        this.ShowCustomMessage("접대비번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI2.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI2.CurControl); }
                        return false;
                    }
                }
                if (SetDefaultValue(this.txtfsA1CDMI3) == "27")
                {
                    sJunbDaeBiNumber = SetDefaultValue(this.fsVLMI03);
                    if (this.fsVLMI03.Length != 10)
                    {
                        this.ShowCustomMessage("접대비번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }
                }
                if (SetDefaultValue(this.txtfsA1CDMI4) == "27")
                {
                    sJunbDaeBiNumber = SetDefaultValue(this.fsVLMI04);
                    if (this.fsVLMI04.Length != 10)
                    {
                        this.ShowCustomMessage("접대비번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI4.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI4.CurControl); }
                        return false;
                    }
                }
                if (SetDefaultValue(this.txtfsA1CDMI5) == "27")
                {
                    sJunbDaeBiNumber = SetDefaultValue(this.fsVLMI05);
                    if (this.fsVLMI05.Length != 10)
                    {
                        this.ShowCustomMessage("접대비번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI5.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI5.CurControl); }
                        return false;
                    }
                }
                if (SetDefaultValue(this.txtfsA1CDMI6) == "27")
                {
                    sJunbDaeBiNumber = SetDefaultValue(this.fsVLMI06);
                    if (this.fsVLMI06.Length != 10)
                    {
                        this.ShowCustomMessage("접대비번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI6.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI6.CurControl); }
                        return false;
                    }
                }

                #endregion

                /* 거래처 임시파일에 등록된 사업자번호, 접대금액이 일치 해야 한다. */
                this.DbConnector.CommandClear(); // TMAC1102SF
                this.DbConnector.Attach("TY_P_AC_2AH8K737", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN, sJunbDaeBiNumber.Substring(0, 4), sJunbDaeBiNumber.Substring(4, 2), sJunbDaeBiNumber.Substring(6, 4));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();
                if (dt1.Rows.Count > 0)
                {
                    if ((SetDefaultValue(this.txtfsA1CDMI1) == "01" && SetDefaultValue(this.fsVLMI01.Trim()) != "") ||
                        (SetDefaultValue(this.txtfsA1CDMI2) == "01" && SetDefaultValue(this.fsVLMI02.Trim()) != "") ||
                        (SetDefaultValue(this.txtfsA1CDMI3) == "01" && SetDefaultValue(this.fsVLMI03.Trim()) != "") ||
                        (SetDefaultValue(this.txtfsA1CDMI4) == "01" && SetDefaultValue(this.fsVLMI04.Trim()) != "") ||
                        (SetDefaultValue(this.txtfsA1CDMI5) == "01" && SetDefaultValue(this.fsVLMI05.Trim()) != "") ||
                        (SetDefaultValue(this.txtfsA1CDMI6) == "01" && SetDefaultValue(this.fsVLMI06.Trim()) != ""))
                    {
                        #region MyRegion
                        sMyCode = UP_CDMIToVLMI("01", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                                      sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);

                        // 
                        /* 거래처 임시파일에 등록된 사업자번호, 거래처화일등 등록된 사업자번호가  일치 해야 한다. */
                        this.DbConnector.CommandClear(); //AVENDMF
                        this.DbConnector.Attach("TY_P_AC_2B18W972", sMyCode);
                        DataTable dt2 = this.DbConnector.ExecuteDataTable();
                        if (dt2.Rows.Count > 0)
                        {
                            #region MyRegion
                            if (dt1.Rows[0]["TSNOCL"].ToString() != dt2.Rows[0]["VNSAUPNO"].ToString())
                            {
                                //Message_Alert("거래처에 등록된 사업자번호와 접대비에 등록된 사업자번호가 일치하지 않습니다.");
                                this.ShowCustomMessage("거래처에 등록된 사업자번호와 접대비에 등록된 사업자번호가 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                if (SetDefaultValue(this.txtfsA1CDMI1) == "01")
                                {
                                    if (PAN10_VLMI1.CurControl is TYCodeBox)
                                    { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                                    else { this.SetFocus(PAN10_VLMI1.CurControl); }
                                    return false;
                                }
                                if (SetDefaultValue(this.txtfsA1CDMI2) == "01")
                                {
                                    if (PAN10_VLMI2.CurControl is TYCodeBox)
                                    { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                                    else { this.SetFocus(PAN10_VLMI2.CurControl); }
                                    return false;
                                }
                                if (SetDefaultValue(this.txtfsA1CDMI3) == "01")
                                {
                                    if (PAN10_VLMI3.CurControl is TYCodeBox)
                                    { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                                    else { this.SetFocus(PAN10_VLMI3.CurControl); }
                                    return false;
                                }
                                if (SetDefaultValue(this.txtfsA1CDMI4) == "01")
                                {
                                    if (PAN10_VLMI4.CurControl is TYCodeBox)
                                    { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                                    else { this.SetFocus(PAN10_VLMI4.CurControl); }
                                    return false;
                                }
                                if (SetDefaultValue(this.txtfsA1CDMI5) == "01")
                                {
                                    if (PAN10_VLMI5.CurControl is TYCodeBox)
                                    { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                                    else { this.SetFocus(PAN10_VLMI5.CurControl); }
                                    return false;
                                }
                                if (SetDefaultValue(this.txtfsA1CDMI6) == "01")
                                {
                                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                                    return false;
                                }

                            }
                            #endregion

                        }
                        else
                        {
                            #region MyRegion
                            this.ShowCustomMessage("미등록 거래처 입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (SetDefaultValue(this.txtfsA1CDMI1) == "01")
                            {
                                if (PAN10_VLMI1.CurControl is TYCodeBox)
                                { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                                else { this.SetFocus(PAN10_VLMI1.CurControl); }
                                return false;
                            }
                            if (SetDefaultValue(this.txtfsA1CDMI2) == "01")
                            {
                                if (PAN10_VLMI2.CurControl is TYCodeBox)
                                { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                                else { this.SetFocus(PAN10_VLMI2.CurControl); }
                                return false;
                            }
                            if (SetDefaultValue(this.txtfsA1CDMI3) == "01")
                            {
                                if (PAN10_VLMI3.CurControl is TYCodeBox)
                                { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                                else { this.SetFocus(PAN10_VLMI3.CurControl); }
                                return false;
                            }
                            if (SetDefaultValue(this.txtfsA1CDMI4) == "01")
                            {
                                if (PAN10_VLMI4.CurControl is TYCodeBox)
                                { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                                else { this.SetFocus(PAN10_VLMI4.CurControl); }
                                return false;
                            }
                            if (SetDefaultValue(this.txtfsA1CDMI5) == "01")
                            {
                                if (PAN10_VLMI5.CurControl is TYCodeBox)
                                { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                                else { this.SetFocus(PAN10_VLMI5.CurControl); }
                                return false;
                            }
                            if (SetDefaultValue(this.txtfsA1CDMI6) == "01")
                            {
                                if (PAN10_VLMI6.CurControl is TYCodeBox)
                                { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                                else { this.SetFocus(PAN10_VLMI6.CurControl); }
                                return false;
                            }
                            #endregion
                        }
                        #endregion

                    }
                    double dJubAmt = Convert.ToDouble(dt1.Rows[0]["TSAMSE"].ToString()) + Convert.ToDouble(dt1.Rows[0]["TSCGSE"].ToString());

                }
                else
                {
                    #region MyRegion
                    //Message_Alert("접대비번호를 입력하십시오.");
                    this.ShowCustomMessage("접대비내역 입력하십시오", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (SetDefaultValue(this.txtfsA1CDMI1) == "27")
                    {
                        if (PAN10_VLMI1.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI1.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI2) == "27")
                    {
                        if (PAN10_VLMI2.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI2.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI3) == "27")
                    {
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI4) == "27")
                    {
                        if (PAN10_VLMI4.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI4.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI5) == "27")
                    {
                        if (PAN10_VLMI5.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI5.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI6) == "27")
                    {
                        if (PAN10_VLMI6.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI6.CurControl); }
                        return false;
                    }
                    #endregion
                }

                #region MyRegion

                /*  임시파일에 등록된 접대금액과 화면상에 입력된 접대비 금액이 일치해야 한다. */
                if (((this.CBO01_B2IDJP.GetValue().ToString() == "2") || (this.CBO01_B2IDJP.GetValue().ToString() == "3")) &&
                    (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString())) != (Convert.ToDouble(dt1.Rows[0]["TSAMSE"].ToString()) + Convert.ToDouble(dt1.Rows[0]["TSCGSE"].ToString()))) ||
                    ((this.CBO01_B2IDJP.GetValue().ToString() == "1") &&
                    (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString())) != (Convert.ToDouble(dt1.Rows[0]["TSAMSE"].ToString()) + Convert.ToDouble(dt1.Rows[0]["TSCGSE"].ToString())))))
                {
                    //Message_Alert("접대비번호를 입력하십시오.");
                    this.ShowCustomMessage("접대비번호를 입력하십시오", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (SetDefaultValue(this.txtfsA1CDMI1) == "27")
                    {
                        if (PAN10_VLMI1.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI1.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI2) == "27")
                    {
                        if (PAN10_VLMI2.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI2.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI3) == "27")
                    {
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI4) == "27")
                    {
                        if (PAN10_VLMI4.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI4.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI5) == "27")
                    {
                        if (PAN10_VLMI5.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI5.CurControl); }
                        return false;
                    }
                    if (SetDefaultValue(this.txtfsA1CDMI6) == "27")
                    {
                        if (PAN10_VLMI6.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI6.CurControl); }
                        return false;
                    }
                }
                #endregion

            } // If .... End (접대비)


            /* 14- 귀속부서 확인   */
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S9Q562", "TY", sB2DTMK , this.CBH01_B2DPAC.GetValue().ToString());
            DataTable dt_dpac = this.DbConnector.ExecuteDataTable();
            if (dt_dpac.Rows.Count == 0)
            {
                this.ShowCustomMessage("귀속부서를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2DPAC.CodeText);
                return false;
            }

            // 2015년 조직개편 후 귀속부서 사용 강제세팅( 공무계전팀-E10000 ) -적용안함
            //if (Convert.ToDouble(this.DTP01_B2DTMK.GetValue().ToString().Substring(0, 4)) > 2014 && this.CBH01_B2DPMK.GetValue().ToString().Substring(0, 1) == "E")
            //{
            //    if (this.CBH01_B2DPMK.GetValue().ToString() == "E10200")
            //    {
            //        this.CBH01_B2DPAC.SetValue("S10000");
            //    }
            //    if (this.CBH01_B2DPMK.GetValue().ToString() == "E10100")
            //    {
            //        this.CBH01_B2DPAC.SetValue("T10000");
            //    }
            //}

            // 2015년 조직개편 후 귀속부서 사용제한(안전환경팀-D10000, 공무계전팀-E10000 )
            if ( Convert.ToDouble( this.DTP01_B2DTMK.GetValue().ToString().Substring(0, 4)) > 2014)
            {
                if ((this.CBH01_B2DPMK.GetValue().ToString().Substring(0, 1) == "D" || this.CBH01_B2DPMK.GetValue().ToString().Substring(0, 1) == "E")
                    && (this.CBH01_B2DPAC.GetValue().ToString().Substring(0, 1) == "D" || this.CBH01_B2DPAC.GetValue().ToString().Substring(0, 1) == "E"))
                {
                    this.ShowCustomMessage("귀속부서 확인 (안전환경팀,공무계전팀:귀속부서 사용불가) [S10000,T10000]-->사용", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2DPAC.CodeText);
                    return false;
                }
            }

            if (Convert.ToDouble(this.DTP01_B2DTMK.GetValue().ToString().Substring(0, 4)) > 2014)
            {
                if (this.CBH01_B2DPAC.GetValue().ToString() == "A20100" || this.CBH01_B2DPAC.GetValue().ToString() == "A50300" || this.CBH01_B2DPAC.GetValue().ToString() == "S30200")
                {
                    this.ShowCustomMessage("귀속부서를 확인하세요(2015년:귀속부서 사용불가)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2DPAC.CodeText);
                    return false;
                }
            }

            /* 15- 관리항목에 의해 입력된 코드값 확인  */
            #region Description : 관리항목 체크 (수정필요)
            // A1OTMI 값이  : '' ==> 필수 , D ==> 차변필수 , C ==> 대변필수 ,  'O' ==> 선택
            if (SetDefaultValue(this.txtfsA1CDMI1) != "")
            {
                if (SetDefaultValue(this.txtfsA1OTMI1) == "" && sB2VLMI1.Trim() == "") 
                {
                    this.ShowCustomMessage("관리항목를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI1.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI1.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI1) == "D" && (this.TXT01_B2AMDR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMDR.GetValue().ToString().Trim() == "") && sB2VLMI1.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI1) == "D" && ( Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0  ) && sB2VLMI1.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(차변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI1.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI1.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI1) == "C" && (this.TXT01_B2AMCR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMCR.GetValue().ToString().Trim() == "") && sB2VLMI1.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI1) == "C" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0 ) && sB2VLMI1.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(대변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI1.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI1.CurControl); }
                    return false;
                }
            }

            if (SetDefaultValue(this.txtfsA1CDMI2) != "")
            {
                if (SetDefaultValue(this.txtfsA1OTMI2) == "" && sB2VLMI2.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI2.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI2.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI2) == "D" && (this.TXT01_B2AMDR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMDR.GetValue().ToString().Trim() == "") && sB2VLMI2.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI2) == "D" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0) && sB2VLMI2.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(차변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI2.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI2.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI2) == "C" && (this.TXT01_B2AMCR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMCR.GetValue().ToString().Trim() == "") && sB2VLMI2.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI2) == "C" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0) && sB2VLMI2.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(대변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI2.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI2.CurControl); }
                    return false;
                }
            }

            if (SetDefaultValue(this.txtfsA1CDMI3) != "")
            {
                if (SetDefaultValue(this.txtfsA1OTMI3) == "" && sB2VLMI3.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI3.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI3.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI3) == "D" && (this.TXT01_B2AMDR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMDR.GetValue().ToString().Trim() == "") && sB2VLMI3.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI3) == "D" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0) && sB2VLMI3.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(차변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI3.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI3.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI3) == "C" && (this.TXT01_B2AMCR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMCR.GetValue().ToString().Trim() == "") && sB2VLMI3.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI3) == "C" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0) && sB2VLMI3.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(대변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI3.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI3.CurControl); }
                    return false;
                }
            }

            if (SetDefaultValue(this.txtfsA1CDMI4) != "")
            {
                if (SetDefaultValue(this.txtfsA1OTMI4) == "" && sB2VLMI4.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI4.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI4.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI4) == "D" && (this.TXT01_B2AMDR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMDR.GetValue().ToString().Trim() == "") && sB2VLMI4.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI4) == "D" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0) && sB2VLMI4.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(차변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI4.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI4.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI4) == "C" && (this.TXT01_B2AMCR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMCR.GetValue().ToString().Trim() == "") && sB2VLMI4.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI4) == "C" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0) && sB2VLMI4.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(대변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI4.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI4.CurControl); }
                    return false;
                }
            }

            if (SetDefaultValue(this.txtfsA1CDMI5) != "")
            {
                if (SetDefaultValue(this.txtfsA1OTMI5) == "" && sB2VLMI5.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI5.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI5.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI5) == "D" && (this.TXT01_B2AMDR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMDR.GetValue().ToString().Trim() == "") && sB2VLMI5.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI5) == "D" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0) && sB2VLMI5.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(차변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI5.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI5.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI5) == "C" && (this.TXT01_B2AMCR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMCR.GetValue().ToString().Trim() == "") && sB2VLMI5.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI5) == "C" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0) && sB2VLMI5.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(대변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI5.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI5.CurControl); }
                    return false;
                }
            }

            if (SetDefaultValue(this.txtfsA1CDMI6) != "")
            {
                if (SetDefaultValue(this.txtfsA1OTMI6) == "" && sB2VLMI6.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI6) == "D" && (this.TXT01_B2AMDR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMDR.GetValue().ToString().Trim() == "") && sB2VLMI6.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI6) == "D" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0) && sB2VLMI6.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(차변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }

                //if (SetDefaultValue(this.txtfsA1OTMI6) == "C" && (this.TXT01_B2AMCR.GetValue().ToString().Trim() != "" || this.TXT01_B2AMCR.GetValue().ToString().Trim() == "") && sB2VLMI6.Trim() == "")
                if (SetDefaultValue(this.txtfsA1OTMI6) == "C" && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0) && sB2VLMI6.Trim() == "")
                {
                    this.ShowCustomMessage("관리항목를 확인하세요(대변필수)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }
            }

            #region 사용보류
            //if (SetDefaultValue(this.txtfsA1CDMI1) != "")
            //{
            //    txtB2VLMI1NM.Text = fFunction_FieldCheck_CDMI(sB2CDAC, SetDefaultValue(this.txtfsA1OTMI1), SetDefaultValue(this.txtfsA1CDMI1), fsVLMI01.Trim(), sB2DPAC);
            //    if (fbRtnFlag == false)
            //    {
            //        //Message_Alert("관리항목를 확인하세요.");
            //        this.SetFocus(this.PAN10_VLMI1);
            //        return false;
            //    }
            //}
            //if (SetDefaultValue(txtfsA1CDMI2) != "")
            //{
            //    txtB2VLMI2NM.Text = fFunction_FieldCheck_CDMI(sB2CDAC, SetDefaultValue(this.txtfsA1OTMI2), SetDefaultValue(this.txtfsA1CDMI2), fsVLMI02.Trim(), sB2DPAC);
            //    if (fbRtnFlag == false)
            //    {
            //        //Message_Alert("관리항목를 확인하세요.");
            //        this.SetFocus(this.PAN10_VLMI2);
            //        return false;
            //    }
            //}
            //if (SetDefaultValue(txtfsA1CDMI3) != "")
            //{
            //    txtB2VLMI3NM.Text = fFunction_FieldCheck_CDMI(sB2CDAC, SetDefaultValue(this.txtfsA1OTMI3), SetDefaultValue(this.txtfsA1CDMI3), fsVLMI03.Trim(), sB2DPAC);
            //    if (fbRtnFlag == false)
            //    {
            //        //Message_Alert("관리항목를 확인하세요.");
            //        this.SetFocus(this.PAN10_VLMI3);
            //        return false;
            //    }
            //}
            //if (SetDefaultValue(txtfsA1CDMI4) != "")
            //{
            //    txtB2VLMI4NM.Text = fFunction_FieldCheck_CDMI(sB2CDAC, SetDefaultValue(this.txtfsA1OTMI4), SetDefaultValue(this.txtfsA1CDMI4), fsVLMI04.Trim(), sB2DPAC);
            //    if (fbRtnFlag == false)
            //    {
            //        //Message_Alert("관리항목를 확인하세요.");
            //        this.SetFocus(this.PAN10_VLMI4);
            //        return false;
            //    }
            //}
            //if (SetDefaultValue(txtfsA1CDMI5) != "")
            //{
            //    txtB2VLMI5NM.Text = fFunction_FieldCheck_CDMI(sB2CDAC, SetDefaultValue(this.txtfsA1OTMI5), SetDefaultValue(this.txtfsA1CDMI5), fsVLMI05.Trim(), sB2DPAC);
            //    if (fbRtnFlag == false)
            //    {
            //        //Message_Alert("관리항목를 확인하세요.");
            //        this.SetFocus(this.PAN10_VLMI5);
            //        return false;
            //    }
            //}
            //if (SetDefaultValue(txtfsA1CDMI6) != "")
            //{
            //    txtB2VLMI6NM.Text = fFunction_FieldCheck_CDMI(sB2CDAC, SetDefaultValue(this.txtfsA1OTMI6), SetDefaultValue(this.txtfsA1CDMI6), fsVLMI05.Trim(), sB2DPAC);
            //    if (fbRtnFlag == false)
            //    {
            //        //Message_Alert("관리항목를 확인하세요.");
            //        this.SetFocus(this.PAN10_VLMI6);
            //        return false;
            //    }
            //}  
            #endregion

            #endregion

            /* 20- 부도어음 확인     12200800
             *     부도어음 차변 (부도어음발생) : 현상태가 부도 14
             *     부도어음 대변 (부도회수) : 현상태가 부도회수 17 */
            if (sB2CDAC == "12400200" && Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0)
            {
                sWK_NONR = UP_CDMIToVLMI("29", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                               sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);

                //어음내역파일이 존재 체크
                this.DbConnector.CommandClear(); //ASGRRSF
                this.DbConnector.Attach("TY_P_AC_25G2Z500", sWK_NONR, "14", sB2DTMK);
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt == 0)
                {
                    this.ShowCustomMessage("어음상태가 부도가 아닙니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2DPAC.CodeText);
                    return false;
                }
            }

            /* 22- 매입 및 매출부가세 관리항목중 '15' (거래일자) 체크
             *     거래일자가 전표일자보도 크거나 년월이 일치하지않는경우   */
            // 11300501 , 11300502 ,11300503 ,21101003 ,21101001 ,21101002
            bool bOptonCheck = false;
            sSubCode = "";
            sSubCode = UP_CDMIToVLMI("15",this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                         sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);
            if (sB2CDAC == "11103101" || sB2CDAC == "11103102" ||  sB2CDAC == "21103101" || sB2CDAC == "21103102")
            {
                if (SetDefaultValue(this.txtfsA1OTMI1) == "" )
                {
                    //무조건 필수
                    bOptonCheck = true;
                }
                if (SetDefaultValue(this.txtfsA1OTMI1) == "D" && Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString().Trim())) != 0 )
                {
                    //차변계정이 차변금액이 있으면 필수
                    bOptonCheck = true;
                }
                if (SetDefaultValue(this.txtfsA1OTMI1) == "C" && Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString().Trim())) != 0)
                {
                    //대변계정이 대변금액이 있으면 필수
                    bOptonCheck = true;
                }

                if (bOptonCheck)
                {
                    bool bRtn = dateValidateCheck(sSubCode.Trim());
                    if (!bRtn)
                    {
                        this.ShowCustomMessage("거래일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }

                    if (sSubCode.Trim() == "")
                    {
                        this.ShowCustomMessage("거래일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }
                    else
                    {
                        sMyCode = sB2DTMK;
                        if ((Convert.ToInt32(sSubCode.Trim().Replace("-", "")) > Convert.ToInt32(sMyCode)) ||
                            (sSubCode.Substring(0, 6) != sMyCode.Substring(0, 6)))
                        {
                            this.ShowCustomMessage("거래일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI3.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI3.CurControl); }
                            return false;
                        }
                    }
                }

                //2016.10.04 고철매각 전표관련 체크 로직 추가( 임경화 )
                //2016.10.26 김계영 요청으로 체크 제외
                /*
                if (sB2CDAC == "21103101")
                {
                    if (sB2VLMI1 == "68") //(전자)매입자납부
                    {
                        //이경우에 이전라인에 반드시 고철매각계정라인 있어야 입력할수 있다.
                        this.DbConnector.CommandClear();  // ACRDCDF
                        this.DbConnector.Attach("TY_P_AC_6A4DG291", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), this.CBO01_B2NOLN.SelectedItem.ToString()); // 카드화일
                        Int16 iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iRowCnt <= 0)
                        {
                            this.ShowCustomMessage("(전자)매입자납부일 경우 이전라인에 고철매각계정이 받드시 있어야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI1.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI1.CurControl); }
                            return false;                            
                        }

                    }
                }
               */
            }

            if ((sSubCode != "") && ((Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString())) != 0) ||
                (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString())) != 0)))
            {
                if (sB2CDAC == "11103101" || sB2CDAC == "11103102" || sB2CDAC == "21103101" || sB2CDAC == "21103102" )
                {
                    sMyCode = sB2DTMK;
                    if ((Convert.ToInt32(sSubCode.Trim().Replace("-", "")) > Convert.ToInt32(sMyCode)) || (sSubCode.Trim().Replace("-", "").Substring(0, 6) != sMyCode.Substring(0, 6)))
                    {
                        this.ShowCustomMessage("거래일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }
                }
            }

            if ( (sSubCode != "") && (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString())) != 0) )
            {
                if (sB2CDAC == "21101006" )  // 신용카드사용분
                {
                    bool bRtn = dateValidateCheck(sSubCode.Trim());
                    if (!bRtn)
                    {
                        this.ShowCustomMessage("거래일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI2.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI2.CurControl); }
                        return false;
                    }
                }
            }

            if (sB2CDAC == "21101006")  // 신용카드사용분
            {
                sSubCode = "";
                sSubCode = UP_CDMIToVLMI("32", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                               sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);
                if (sSubCode != "")
                {
                    this.DbConnector.CommandClear();  // ACRDCDF
                    this.DbConnector.Attach("TY_P_AC_2CI6K247", sSubCode); // 카드화일
                    DataTable dt_cd = this.DbConnector.ExecuteDataTable();
                    if (dt_cd.Rows.Count != 0)
                    {
                        /*
                        if (dt_cd.Rows[0]["A6NMPD"].ToString().Trim() != "태영")
                        {
                            this.ShowCustomMessage("사용할수 없는 카드(법인카드아님) 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI1.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI1.CurControl); }
                            return false;
                        }
                        */
                    }
                    else
                    {
                        this.ShowCustomMessage("신용카드번호를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI1.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI1.CurControl); }
                        return false;
                    }
                }
            }
            /* 23- 차변금액확인 */
            if (((sB2IDJP == "1") && (Convert.ToDouble(sB2AMCR) == 0)) ||
                ((sB2IDJP == "2") && (Convert.ToDouble(sB2AMDR) == 0)) ||
                ((sB2IDJP == "3") && (Convert.ToDouble(sB2AMDR) == 0) &&
                (Convert.ToDouble(sB2AMCR) == 0)))
            {
                if (((this.txtfsA1CDMI1 == "11") && ((sB2VLMI1 == "52") || (sB2VLMI1 == "54") || (sB2VLMI1 == "55") || (sB2VLMI1 == "56") ||
                    (sB2VLMI1 == "59") || (sB2VLMI1 == "72") || (sB2VLMI1 == "74") || (sB2VLMI1 == "75") || (sB2VLMI1 == "79") || (sB2VLMI1 == "62") || (sB2VLMI1 == "66") ||
                    (sB2VLMI1 == "12") || (sB2VLMI1 == "13") || (sB2VLMI1 == "22"))) ||
                    ((this.txtfsA1CDMI2 == "11") && ((sB2VLMI2 == "52") || (sB2VLMI2 == "54") || (sB2VLMI2 == "55") || (sB2VLMI2 == "56") ||
                    (sB2VLMI2 == "59") || (sB2VLMI2 == "72") || (sB2VLMI2 == "74") || (sB2VLMI2 == "75") || (sB2VLMI2 == "79") || (sB2VLMI2 == "62") || (sB2VLMI2 == "66") ||
                    (sB2VLMI2 == "12") || (sB2VLMI2 == "13") || (sB2VLMI2 == "22"))) ||
                    ((this.txtfsA1CDMI3 == "11") && ((sB2VLMI3 == "52") || (sB2VLMI3 == "54") || (sB2VLMI3 == "55") || (sB2VLMI3 == "56") ||
                    (sB2VLMI3 == "59") || (sB2VLMI3 == "72") || (sB2VLMI3 == "74") || (sB2VLMI3 == "75") || (sB2VLMI3 == "79") || (sB2VLMI3 == "62") || (sB2VLMI3 == "66") ||
                    (sB2VLMI3 == "12") || (sB2VLMI3 == "13") || (sB2VLMI3 == "22"))) ||
                    ((this.txtfsA1CDMI4 == "11") && ((sB2VLMI4 == "52") || (sB2VLMI4 == "54") || (sB2VLMI4 == "55") || (sB2VLMI4 == "56") ||
                    (sB2VLMI4 == "59") || (sB2VLMI4 == "72") || (sB2VLMI4 == "74") || (sB2VLMI4 == "75") || (sB2VLMI4 == "79") || (sB2VLMI4 == "62") || (sB2VLMI4 == "66") ||
                    (sB2VLMI4 == "12") || (sB2VLMI4 == "13") || (sB2VLMI4 == "22"))) ||
                    ((this.txtfsA1CDMI5 == "11") && ((sB2VLMI5 == "52") || (sB2VLMI5 == "54") || (sB2VLMI5 == "55") || (sB2VLMI5 == "56") ||
                    (sB2VLMI5 == "59") || (sB2VLMI5 == "72") || (sB2VLMI5 == "74") || (sB2VLMI5 == "75") || (sB2VLMI5 == "79") || (sB2VLMI5 == "62") || (sB2VLMI5 == "66") ||
                    (sB2VLMI5 == "12") || (sB2VLMI5 == "13") || (sB2VLMI5 == "22"))) ||
                    ((this.txtfsA1CDMI6 == "11") && ((sB2VLMI6 == "52") || (sB2VLMI6 == "54") || (sB2VLMI6 == "55") || (sB2VLMI6 == "56") ||
                    (sB2VLMI6 == "59") || (sB2VLMI6 == "72") || (sB2VLMI6 == "74") || (sB2VLMI6 == "75") || (sB2VLMI6 == "79") || (sB2VLMI6 == "62") || (sB2VLMI6 == "66") ||
                    (sB2VLMI6 == "12") || (sB2VLMI6 == "13") || (sB2VLMI6 == "22"))))
                {
                    bFlag = false;
                }
                else bFlag = true;
                if (bFlag == true)
                {
                    switch (sB2IDJP)
                    {
                        case "1":
                            this.ShowCustomMessage("대변 금액을 입력하십시요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_B2AMCR);
                            return false;
                        case "2":
                            this.ShowCustomMessage("차변 금액을 입력하십시요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_B2AMDR);
                            return false;
                        case "3":
                            this.ShowCustomMessage("차/대변 금액을 입력하십시요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_B2AMDR);
                            return false;
                    }
                }
            }

            /* 24- 매입부가세 세무구분이 52, 54, 55, 59이면 통과  */
            /* 25- 매출부가세 세무구분이 12.13.22 이면 통과  */
            /* 26- 대변금액확인 */
            /* 27- 매입부가세 세무구분이 52, 54, 55, 59이면 통과  */
            /* 28- 매출부가세 세무구분이 12.13.22 이면 통과  */
            /* 29- 대체전표의 경우 차대변금액 확인 */
            /* 30- 매입부가세 세무구분이 52, 54, 55, 59이면 통과  */
            /* 31- 매출부가세 세무구분이 12.13.22 이면 통과  */
            if (Convert.ToDouble(sB2AMDR) != 0  && Convert.ToDouble(sB2AMCR) != 0)
            {
                this.ShowCustomMessage("차대변 금액이 동시에 올 수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_B2AMDR);
                return false;
            }

            /*상대처 확인*/
            if (this.TXT01_B2RKCU.GetValue().ToString().Trim() == "")
            {
                if (this.txtfsA1CDMI1 == "01" || this.txtfsA1CDMI1 == "02") this.TXT01_B2RKCU.SetValue(UP_SetRKCU(this.txtfsA1CDMI1, sB2VLMI1));
                if (this.txtfsA1CDMI2 == "01" || this.txtfsA1CDMI2 == "02") this.TXT01_B2RKCU.SetValue(UP_SetRKCU(this.txtfsA1CDMI2, sB2VLMI2));
                if (this.txtfsA1CDMI3 == "01" || this.txtfsA1CDMI3 == "02") this.TXT01_B2RKCU.SetValue(UP_SetRKCU(this.txtfsA1CDMI3, sB2VLMI3));
                if (this.txtfsA1CDMI4 == "01" || this.txtfsA1CDMI4 == "02") this.TXT01_B2RKCU.SetValue(UP_SetRKCU(this.txtfsA1CDMI4, sB2VLMI4));
                if (this.txtfsA1CDMI5 == "01" || this.txtfsA1CDMI5 == "02") this.TXT01_B2RKCU.SetValue(UP_SetRKCU(this.txtfsA1CDMI5, sB2VLMI5));
                if (this.txtfsA1CDMI6 == "01" || this.txtfsA1CDMI6 == "02") this.TXT01_B2RKCU.SetValue(UP_SetRKCU(this.txtfsA1CDMI6, sB2VLMI6));
            }

            /* 32- 적요확인      */
            if (this.TXT01_B2RKAC.GetValue().ToString().Trim() == "")
            {
                this.ShowCustomMessage("적요를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_B2RKAC);
                return false;
            }

            /* 33- 반제전표 확인 
             '*--------------------------------------------------------------------------*
             '* << 반제전표 확인 >>
             '*    반제계정(A1TAG07 = "Y")인 경우 차면 계정이 차변에 오면 설정
             '*    대변에오면 정리(원천번호입력) -금액이 -인 경우 반대
             '*--------------------------------------------------------------------------*/
            if (this.txtfsA1TAG01 == "D" && this.txtfsA1TAG07 == "Y")
            {
                if (Convert.ToDouble(sB2AMDR) < 0 || Convert.ToDouble(sB2AMCR) > 0)
                {
                    if (this.TXT01_B2WCJP.GetValue().ToString().Trim()  == "")
                    {
                        this.ShowCustomMessage("원천전표번호 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2WCJP);
                        return false;
                    }
                }
                if (Convert.ToDouble(sB2AMDR) > 0 && this.TXT01_B2WCJP.GetValue().ToString().Trim() != "")
                {
                    this.ShowCustomMessage("원천전표번호를  입력할 수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2WCJP);
                    return false;
                }
            }
            if (this.txtfsA1TAG01 == "C" && this.txtfsA1TAG07 == "Y")
            {
                if (Convert.ToDouble(sB2AMCR) < 0 || Convert.ToDouble(sB2AMDR) > 0)
                {
                    if (this.TXT01_B2WCJP.GetValue().ToString().Trim() == "")
                    {
                        this.ShowCustomMessage("원천전표번호 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2WCJP);
                        return false;
                    }
                }
                if (Convert.ToDouble(sB2AMCR) > 0 && this.TXT01_B2WCJP.GetValue().ToString().Trim() != "")
                {
                    this.ShowCustomMessage("원천전표번호를  입력할 수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2WCJP);
                    return false;
                }
            }

            /* 34- 신용카드 사용분(21101210) 차변금액 발생시
                  원천전표의 카드번호와 화면상의 카드번호 확인 */
            if (sB2CDAC == "21101006" && this.TXT01_B2WCJP.GetValue().ToString().Trim() != "")
            {
                if (Convert.ToDouble(sB2AMDR) != 0 ||
                    Convert.ToDouble(sB2AMCR) < 0)
                {
                    this.DbConnector.CommandClear();  // ADSLGLF
                    this.DbConnector.Attach("TY_P_AC_2AI5H746", this.TXT01_B2WCJP.GetValue().ToString().Substring(0, 6), this.TXT01_B2WCJP.GetValue().ToString().Substring(6, 8), this.TXT01_B2WCJP.GetValue().ToString().Substring(14, 3), this.TXT01_B2WCJP.GetValue().ToString().Substring(17, 2)); // 미승인전표
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count == 0)
                    {
                        this.ShowCustomMessage("원천번호에 대한 미승인전표가 존재하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2WCJP);
                        return false;
                    }
                }
            }
            /* 35- 반제설정전표인경우 반제금액이 있으면 삭제불가 
             *     수정시 반제금액보다 설정금액이 작을 경우 작업불가 */
            if (this.txtfsA1TAG07 == "Y" && sOption == "C" || sOption == "D")
            {
                if ((this.txtfsA1TAG01 == "D" &&
                    (Convert.ToDouble(sB2AMDR) > 0 || Convert.ToDouble(sB2AMCR) < 0)) ||
                    (this.txtfsA1TAG01 == "C" &&
                    (Convert.ToDouble(sB2AMCR) > 0 || Convert.ToDouble(sB2AMDR) < 0)))
                {
                    this.DbConnector.CommandClear();  //ABANJMF 
                    this.DbConnector.Attach("TY_P_AC_2AI5O748", sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN); // 반제설정 조회
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sWK_BanAMT = dt.Rows[0]["B7AMBJ"].ToString();

                        if (sOption == "D" && Convert.ToDouble(sWK_BanAMT) != 0)
                        {
                            this.ShowCustomMessage("반제 정리금액이 존재하므로 삭제할수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_B2AMDR);
                            return false;
                        }
                        if (sOption == "C")
                        {
                            if ((sOption == "D" &&
                                (Convert.ToDouble(sWK_BanAMT) > Convert.ToDouble(sB2AMDR))) ||
                                (sOption == "C" &&
                                (Convert.ToDouble(sWK_BanAMT) > Convert.ToDouble(sB2AMCR))))
                            {
                                this.ShowCustomMessage("반제 정리금액보다 설정금액이 작습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                this.SetFocus(this.TXT01_B2AMDR);
                                return false;
                            }
                        }
                    }
                }
            }

            /* 36- 반제 정리전표인경우 반제금액 over 확인 */
            if ((this.txtfsA1TAG07 == "Y") &&
               ((this.txtfsA1TAG01 == "D" && Convert.ToDouble(sB2AMCR) > 0) ||
                (this.txtfsA1TAG01 == "D" && Convert.ToDouble(sB2AMDR) < 0) ||
                (this.txtfsA1TAG01 == "C" && Convert.ToDouble(sB2AMDR) > 0) ||
                (this.txtfsA1TAG01 == "C" && Convert.ToDouble(sB2AMCR) < 0)))
            {
                if (this.TXT01_B2WCJP.GetValue().ToString().Trim() == "")
                {
                    this.ShowCustomMessage("원천전표번호를 입력해주세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2WCJP);
                    return false;
                }

                /*  작성일자 보다 원천번호의 작성일자가 늦으면 전표처리가 안됨.  */
                string sWK_WCDate = "";
                //원천번호의 작성일자 
                sWK_WCDate = this.TXT01_B2WCJP.GetValue().ToString().Trim().Substring(6, 8);
                if (Convert.ToInt32(sWK_WCDate) > Convert.ToInt32(sB2DTMK))
                {
                    this.ShowCustomMessage("원천번호의 일자가 작성일자보다 큽니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2WCJP);
                    return false;
                }

                this.DbConnector.CommandClear();  // ABANJMF
                this.DbConnector.Attach("TY_P_AC_2AI5O748", this.TXT01_B2WCJP.GetValue().ToString().Trim().Substring(0, 6), this.TXT01_B2WCJP.GetValue().ToString().Trim().Substring(6, 8), this.TXT01_B2WCJP.GetValue().ToString().Trim().Substring(14, 3), this.TXT01_B2WCJP.GetValue().ToString().Trim().Substring(17, 2)); // 반제설정 조회
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    sWK_BanAMT = dt.Rows[0]["B7AMAT"].ToString();  // 발생금액
                    sWK_BanAMBJ = dt.Rows[0]["B7AMBJ"].ToString(); // 정리금액
                    sWK_BanAMJN = dt.Rows[0]["B7AMJN"].ToString(); // 잔액		
                }
                else
                {
                    this.ShowCustomMessage("미등록 원천번호 입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2WCJP);
                    return false;
                }

                //<< 수정전 미승인전표의 정리 금액을 차감(전표발행전 상태로 금액수정) >>
                string sWK_B2AMBJ = "0";
                if (this.txtfsA1TAG01 == "D")
                {
                    this.DbConnector.CommandClear();  //ADSLGLF
                    this.DbConnector.Attach("TY_P_AC_2AI7H749", sB2DPMK, sB2DTMK, sB2NOSQ, this.TXT01_B2WCJP.GetValue().ToString().Trim(), sB2DPMK, sB2DTMK, sB2NOSQ, this.TXT01_B2WCJP.GetValue().ToString().Trim());
                    DataTable dt_tm_1 = this.DbConnector.ExecuteDataTable();
                    if (dt_tm_1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_tm_1.Rows.Count; i++)
                        {
                            sWK_B2AMBJ = dt_tm_1.Rows[0]["B2AMBJ"].ToString();
                        }
                    }
                }
                else
                {
                    this.DbConnector.CommandClear();  // ADSLGLF
                    this.DbConnector.Attach("TY_P_AC_2AI7J750", sB2DPMK, sB2DTMK, sB2NOSQ, this.TXT01_B2WCJP.GetValue().ToString().Trim(), sB2DPMK, sB2DTMK, sB2NOSQ, this.TXT01_B2WCJP.GetValue().ToString().Trim());
                    DataTable dt_tm_2 = this.DbConnector.ExecuteDataTable();
                    if (dt_tm_2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_tm_2.Rows.Count; i++)
                        {
                            sWK_B2AMBJ = dt_tm_2.Rows[0]["B2AMBJ"].ToString();
                        }
                    }
                }

                sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(Get_Numeric(sWK_BanAMBJ)) - Convert.ToDouble(Get_Numeric(sWK_B2AMBJ)));

                /* 37- 수정후 임시화일의 정리금액을 더함 */
                string sWK_W2AM = "0";
                if (this.txtfsA1TAG01 == "D")
                {
                    this.DbConnector.CommandClear();  // TMAC1102F
                    this.DbConnector.Attach("TY_P_AC_2AI7K755", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN, this.TXT01_B2WCJP.GetValue().ToString().Trim(),
                                                                fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN, this.TXT01_B2WCJP.GetValue().ToString().Trim());
                    DataTable dt_tm_1 = this.DbConnector.ExecuteDataTable();
                    if (dt_tm_1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_tm_1.Rows.Count; i++)
                        {
                            sWK_W2AM = dt_tm_1.Rows[0]["W2AM"].ToString();
                        }
                    }
                }
                else
                {
                    this.DbConnector.CommandClear(); // TMAC1102F
                    this.DbConnector.Attach("TY_P_AC_2AI7L756", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN, this.TXT01_B2WCJP.GetValue().ToString().Trim(),
                                                                fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN, this.TXT01_B2WCJP.GetValue().ToString().Trim());
                    DataTable dt_tm_1 = this.DbConnector.ExecuteDataTable();
                    if (dt_tm_1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_tm_1.Rows.Count; i++)
                        {
                            sWK_W2AM = dt_tm_1.Rows[0]["W2AM"].ToString();
                        }
                    }
                }

                sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(Get_Numeric(sWK_BanAMBJ)) + Convert.ToDouble(Get_Numeric(sWK_W2AM)));

                //<< 현재 작업중인 레코드에대한 처리 >>
                if (sOption != "D")
                {
                    if (this.txtfsA1TAG01 == "D")
                    {
                        if (Convert.ToDouble(sB2AMDR) < 0)
                        {
                            sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                                                          (Convert.ToDouble(sB2AMDR) * -1));
                        }
                        else
                        {
                            sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                                                          Convert.ToDouble(sB2AMCR));
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(sB2AMCR) < 0)
                        {
                            sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                                                          (Convert.ToDouble(sB2AMCR) * -1));
                        }
                        else
                        {
                            sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                                                          Convert.ToDouble(sB2AMDR));
                        }
                    }
                }

                //<< 반재잔액 계산 >>
                sWK_BanAMJN = Convert.ToString(Convert.ToDouble(sWK_BanAMT) - Convert.ToDouble(sWK_BanAMBJ));

                if (Convert.ToDouble(sWK_BanAMJN) < 0 && sOption != "D")
                {
                    this.ShowCustomMessage("정리금액이 설정금액을 초과하였습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2AMDR);
                    return false;
                }
            } // End .............. 36

            /* 41- 외화잔액 체크 
             '*------------------------------------*
             '* << 외화잔액  CHECK >>
             '*    외화계정 관리항목 고정()
             '*     관항1. "02" 금융기간(은행)
             '*     관항2. "07" 계좌번호
             '*     관항3. "30" 외화구분
             '*     관항4. "21" 외화금액
             '*     관항5. "36" 환    율
             '*     관항6. "41" 외화관리번호
             '*-----------------------------------*
             '* << 외화 설정 관련 Check >>
             '*-----------------------------------*/
            if ( (this.txtfsA1CDMI1 == "41" || this.txtfsA1CDMI2 == "41" || this.txtfsA1CDMI2 == "41" ||
                  this.txtfsA1CDMI4 == "41" || this.txtfsA1CDMI5 == "41" || this.txtfsA1CDMI6 == "41") &&
                 (Convert.ToDouble(sB2AMDR) > 0 || Convert.ToDouble(sB2AMCR) < 0) )
            {
                //<< 외화관리번호 등록여부 확인  TMAC1102WF >>
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB4N681", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN);
                DataTable dt_tmw = this.DbConnector.ExecuteDataTable();
                if (dt_tmw.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tmw.Rows.Count; i++)
                    {
                        //화면의 관리항목에 등록된 내용과  외화설정파일의 내용과 일치여부 확인
                        if (sB2VLMI1 != dt_tmw.Rows[0]["TWBANK"].ToString())
                        {
                            this.ShowCustomMessage("관리항목 은행과 외화설정은행이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI1.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI1.CurControl); }
                            return false;
                        }
                        if (sB2VLMI2 != dt_tmw.Rows[0]["TWGUJA"].ToString())
                        {
                            this.ShowCustomMessage("관리항목 계좌와 외화설정계좌가 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI2.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI2.CurControl); }
                            return false;
                        }
                        if (sB2VLMI3 != dt_tmw.Rows[0]["TWGUBN"].ToString())
                        {
                            this.ShowCustomMessage("관리항목 외화구분과 외화설정외화구분이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI3.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI3.CurControl); }
                            return false;
                        }
                        if (Convert.ToDouble(String.Format(sB2VLMI4, "{0:0.000}")) !=  Convert.ToDouble(String.Format(dt_tmw.Rows[0]["TWIAMT"].ToString(), "{0:0.000}")))
                        {
                            this.ShowCustomMessage("관리항목 외화금액과 외화설정 외화금액이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI4.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI4.CurControl); }
                            return false;
                        }

                        //관리항목의 환율
                        double dVLMIYUL = Convert.ToDouble(String.Format(sB2VLMI5, "{0:0.000}"));
                        //외화임시파일의 환율 
                        double dTWYUL = Convert.ToDouble(String.Format(dt_tmw.Rows[0]["TWYUL"].ToString(), "{0:0.000}"));

                        if (dVLMIYUL != dTWYUL)
                        {
                            this.ShowCustomMessage("관리항목 환율과 외화설정 환율이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }

                        //관리항목의 외화 환산 금액이 계정금액과 +-10범위 내에서 가능
                        sWK_DRAMT = "0";
                        sWK_CRAMT = "0";
                        if (Convert.ToDouble(sB2AMDR) > 0) sWK_DRAMT = sB2AMDR;
                        else
                        {
                            if (Convert.ToDouble(sB2AMCR) > 0)
                            {
                                sWK_DRAMT = Convert.ToString(Convert.ToDouble(sB2AMDR) * -1);
                            }
                        }
                        if (sB2VLMI3.Substring(0, 3) == "JPY")
                        {
                            sWK_CRAMT = Convert.ToString((Convert.ToDouble(dt_tmw.Rows[0]["TWIAMT"].ToString()) / 100) *
                                Convert.ToDouble(dt_tmw.Rows[0]["TWYUL"].ToString()));
                            sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) - Convert.ToDouble(sWK_CRAMT));
                        }
                        else
                        {
                            // 수정 전(2005.08.08)
                            sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) -
                                                         Convert.ToInt64(Convert.ToDouble(dt_tmw.Rows[0]["TWIAMT"].ToString()) *
                                                                         Convert.ToDouble(dt_tmw.Rows[0]["TWYUL"].ToString())));

                            // 수정 후(2005.08.08)
                            //						sWK_DRAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(sWK_DRAMT)) - 
                            //							Convert.ToDouble(Get_Numeric(MyReader["TWIAMT"].ToString())) * 
                            //							Convert.ToDouble(Get_Numeric(MyReader["TWYUL"].ToString()))
                            //							);
                        }
                        if (Convert.ToDouble(sWK_DRAMT) > 10 || Convert.ToDouble(sWK_DRAMT) < -10)
                        {
                            this.ShowCustomMessage("외화환산 금액과 차.대 금액이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }
                        /*외화설정전표인경우　정리금액이　있으면　삭제불가 
                         수정시　설정금액이　정리금액보다　작을경우　작업불가 */
                        if (sOption == "C" || sOption == "D")
                        {
                            if (Convert.ToDouble(sB2AMDR) > 0 || Convert.ToDouble(sB2AMCR) < 0)
                            {
                                //<< 외화관리번호 등록여부 확인 AIHWANMF  >>
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_AC_2AB6A696", sB2VLMI6.Substring(0, 4), sB2VLMI6.Substring(4, 4));
                                DataTable dt_hwm = this.DbConnector.ExecuteDataTable();
                                if (dt_hwm.Rows.Count > 0)
                                {
                                    for (int k = 0; k < dt_hwm.Rows.Count; k++)
                                    {
                                        if (sOption == "D" && Convert.ToDouble(dt_hwm.Rows[0]["IHWABJ"].ToString().Trim()) != 0)
                                        {
                                            this.ShowCustomMessage("외화 정리금액이 존재하므로 삭제할수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                                            return false;
                                        }
                                        if (sOption == "C" && Convert.ToDouble(dt_hwm.Rows[0]["IHWABJ"].ToString().Trim()) > Convert.ToDouble(String.Format(sB2VLMI4, "{0:0.000}")))
                                        {
                                            this.ShowCustomMessage("외화 설정금액이 정리금액보다 작습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                                            return false;
                                        }
                                    } // End ..for
                                }
                            }
                        } // Een .. sOption

                    }
                }
                else
                {
                    //Message_Alert("외화관리번호를 입력 하십시요.");
                    this.ShowCustomMessage("외화관리번호를 입력 하십시요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2DPAC.CodeText);
                    return false;
                }
            } // End .............. 41

            /*<< 외화 정리 관련 Check >> 
             * WK-DRAMT =  외화설정잔액  +  실전표의정리외화금액 ( 발행전 )         
             * WK-CRAMT =  임시화일에등록된　정리외화금액의합    ( 발행후 ) */
            if ( (this.txtfsA1CDMI1 == "41" || this.txtfsA1CDMI2 == "41" || this.txtfsA1CDMI2 == "41" ||
                  this.txtfsA1CDMI4 == "41" || this.txtfsA1CDMI5 == "41" || this.txtfsA1CDMI6 == "41") &&
                 (Convert.ToDouble(sB2AMDR) < 0 || Convert.ToDouble(sB2AMCR) > 0) )
            {
                double dWD_DRAMT = 0;
                //double dWD_CRAMT = 0;

                sWK_DRAMT = "0";
                sWK_CRAMT = "0";
                sB2VLMI4 = Get_Numeric(sB2VLMI4);
                sB2VLMI5 = Get_Numeric(sB2VLMI5);

                //'설정전표번호가 없는경우
                if (sB2VLMI6 == "")
                {
                    //Message_Alert("외화설정 전표가 일치하지않습니다");
                    this.ShowCustomMessage("외화설정 전표가 일치하지않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }
                //<< 외화관리번호 등록여부 확인 AIHWANMF  >>
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB6A696",  sB2VLMI6.Substring(0, 4), sB2VLMI6.Substring(4, 4));
                DataTable dt_hwm = this.DbConnector.ExecuteDataTable();
                if (dt_hwm.Rows.Count > 0)
                {
                    //화면의 관리항목에 등록된 내용과  외화설정파일의 내용과 일치여부 확인
                    if (sB2VLMI1 != dt_hwm.Rows[0]["IHBANK"].ToString().Trim())
                    {
                        this.ShowCustomMessage("관리항목 은행과 외화설정은행이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI1.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI1.CurControl); }
                        return false;
                    }
                    if (sB2VLMI2 != dt_hwm.Rows[0]["IHGUJA"].ToString().Trim())
                    {
                        this.ShowCustomMessage("관리항목 계좌와 외화설정계좌가 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI2.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI2.CurControl); }
                        return false;
                    }
                    if (sB2VLMI3 != dt_hwm.Rows[0]["IHGUBN"].ToString().Trim())
                    {
                        this.ShowCustomMessage("관리항목 외화구분과 외화설정외화구분이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }

                    //외화잔액
                    dWD_DRAMT = Convert.ToDouble(dt_hwm.Rows[0]["IHWAJA"].ToString().Trim());

                    //관리항목의 외화 환산 금액이 계정금액과 +-10범위 내에서 가능
                    if (Convert.ToDouble(sB2AMDR) < 0)
                    {
                        sWK_DRAMT = Convert.ToString(Convert.ToDouble(sB2AMDR) * -1);
                    }
                    else
                    {
                        if (Convert.ToDouble(sB2AMCR) > 0)
                        {
                            sWK_DRAMT = sB2AMCR;
                        }
                    }
                    if (sB2VLMI3.Substring(0, 3) == "JPY")
                    {
                        sWK_CRAMT = Convert.ToString((Convert.ToDouble(sB2VLMI4) / 100) *
                                                      Convert.ToDouble(sB2VLMI5));
                        sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) - Convert.ToDouble(sWK_CRAMT));
                    }
                    else
                    {
                        sWK_CRAMT = Convert.ToString(Convert.ToDouble(sB2VLMI4) *
                                                      Convert.ToDouble(sB2VLMI5));
                        sWK_DRAMT = Convert.ToString(Convert.ToDecimal(sWK_DRAMT) -
                                                    (Convert.ToDecimal(sB2VLMI4) *
                                                     Convert.ToDecimal(sB2VLMI5)));
                    }
                    if (Convert.ToDouble(sWK_DRAMT) > 10 || Convert.ToDouble(sWK_DRAMT) < -10)
                    {
                        this.ShowCustomMessage("외화환산 금액과 차.대 금액이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }
                }
                else
                {
                    this.ShowCustomMessage("미등록 외화 설정번호 입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }

                //외화 정리금액이 설정금액을 초과할 수 없다
                //전표발행전　외화잔액
                sWK_DRAMT = "0";
                sWK_CRAMT = "0";

                // ADSLGLF
                this.DbConnector.CommandClear();  // ADSLGLF 
                this.DbConnector.Attach("TY_P_AC_2AB3H673", sB2DPMK, sB2DTMK, sB2NOSQ);
                DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
                if (dt_adsl.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_adsl.Rows.Count; i++)
                    {
                        if ((dt_adsl.Rows[0]["B2CDAC"].ToString().Trim() == "11100307" || dt_adsl.Rows[0]["B2CDAC"].ToString().Trim() == "11100308") &&  // 11100308  ,11100309
                            (Convert.ToDouble(dt_adsl.Rows[0]["B2AMDR"].ToString().Trim()) < 0 || Convert.ToDouble(dt_adsl.Rows[0]["B2AMCR"].ToString().Trim()) > 0) &&
                            (dt_adsl.Rows[0]["B2VLMI6"].ToString().Trim() == sB2VLMI6))
                        {
                            sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) + Convert.ToDouble(dt_adsl.Rows[0]["B2VLMI4"].ToString().Trim()));
                        }
                    }
                }

                //전표발행후 외화잔액 TMAC1102F
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AJ96759", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
                DataTable dt_tmac = this.DbConnector.ExecuteDataTable();
                if (dt_tmac.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tmac.Rows.Count; i++)
                    {
                        if ((dt_tmac.Rows[0]["W2CDAC"].ToString().Trim() == "11100307" || dt_tmac.Rows[0]["W2CDAC"].ToString().Trim() == "11100308") &&  // 11100308  ,11100309
                            (Convert.ToDouble(dt_tmac.Rows[0]["W2AMDR"].ToString().Trim()) < 0 || Convert.ToDouble(dt_tmac.Rows[0]["W2AMCR"].ToString().Trim()) > 0) &&
                            (dt_tmac.Rows[0]["W2VLMI6"].ToString().Trim() == sB2VLMI6))
                        {
                            sWK_CRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) + Convert.ToDouble(dt_tmac.Rows[0]["W2VLMI4"].ToString().Trim()));
                        }
                    }
                }

                sWK_CRAMT = Convert.ToString(Convert.ToDouble(sWK_CRAMT) - Convert.ToDouble(sB2VLMI4));

                if (Convert.ToDouble(sWK_CRAMT) > 0)
                {
                    if ((dWD_DRAMT + Convert.ToDouble(sWK_CRAMT)) < 0)
                    {
                        this.ShowCustomMessage("외화 정리금액이 설정 금액을 초과하였습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI6.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI6.CurControl); }
                        return false;
                    }
                }
                else
                {
                    sWK_CRAMT = Convert.ToString(Convert.ToDouble(sWK_CRAMT) * -1);

                    if ((dWD_DRAMT - Convert.ToDouble(sWK_CRAMT)) < 0)
                    {
                        this.ShowCustomMessage("외화 정리금액이 설정 금액을 초과하였습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI6.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI6.CurControl); }
                        return false;
                    }
                }
            }
            // ---------------------------------------------------------------------------------------------- //
            /* 외화잔액이　０일경우　원화　정리금액은　                             
             * 원화설정　금액과　동일해야함                                         
             * 위의　외화정리　잔액의 WK-DRAMT, WK-CRAMT 변수금액　사용 */
            if ((this.txtfsA1CDMI1 == "41" || this.txtfsA1CDMI2 == "41" || this.txtfsA1CDMI2 == "41" ||
                 this.txtfsA1CDMI4 == "41" || this.txtfsA1CDMI5 == "41" || this.txtfsA1CDMI6 == "41") &&
                (Convert.ToDouble(sB2AMCR) > 0 || Convert.ToDouble(sB2AMDR) < 0) && (Convert.ToDouble(sWK_DRAMT) == Convert.ToDouble(sWK_CRAMT)))
            {
                sWK_DRAMT = "0";
                sWK_CRAMT = "0";
                //설정전표번호가 없는경우
                if (sB2VLMI6 == "")
                {
                    this.ShowCustomMessage("외화설정 전표가 일치하지않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }

                sWK_IHWAMT = "0";
                //<< 외화관리번호 등록여부 확인 AIHWANMF  >>
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB6A696", sB2VLMI6.Substring(0, 4), sB2VLMI6.Substring(4, 4));
                DataTable dt_hwm = this.DbConnector.ExecuteDataTable();
                if (dt_hwm.Rows.Count > 0)
                {
                    sWK_IHWAMT = dt_hwm.Rows[0]["IHWAJA"].ToString().Trim();  //설정금액
                }
                else
                {
                    this.ShowCustomMessage("미등록 외화 설정번호 입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }

                //<< 전표발행전 원화정리금액 AIHWANSF  >>
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AJ9G760", sB2VLMI6.Substring(0, 4), sB2VLMI6.Substring(4, 4));
                DataTable dt_hwsf = this.DbConnector.ExecuteDataTable();
                if (dt_hwsf.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_hwsf.Rows.Count; i++)
                    {
                        if ((dt_hwsf.Rows[0]["IHSJPNO"].ToString().Trim().Substring(0, 6) != sB2DPMK) ||
                            (dt_hwsf.Rows[0]["IHSJPNO"].ToString().Trim().Substring(6, 8) != sB2DTMK) ||
                            (dt_hwsf.Rows[0]["IHSJPNO"].ToString().Trim().Substring(14, 3) != sB2NOSQ))
                        {
                            sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) + Convert.ToDouble(dt_hwsf.Rows[0]["IHSWAMT"].ToString()));
                        }
                    }
                }

                this.DbConnector.CommandClear(); //TMAC1102F
                this.DbConnector.Attach("TY_P_AC_2AJ96759", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);  
                DataTable dt_tmac = this.DbConnector.ExecuteDataTable();
                if (dt_tmac.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tmac.Rows.Count; i++)
                    {
                        if ((dt_tmac.Rows[0]["W2CDAC"].ToString().Trim() == "11100307" || dt_tmac.Rows[0]["W2CDAC"].ToString().Trim() == "11100308") &&  //11100308 ,11100309
                            (Convert.ToDouble(dt_tmac.Rows[0]["W2AMDR"].ToString().Trim()) < 0 || Convert.ToDouble(dt_tmac.Rows[0]["W2AMCR"].ToString().Trim()) > 0) &&
                            (dt_tmac.Rows[0]["W2VLMI6"].ToString().Trim() == sB2VLMI6) && (sB2NOLN != dt_tmac.Rows[0]["W2NOLN"].ToString().Trim()))
                        {
                            if (Convert.ToDouble(dt_tmac.Rows[0]["W2AMDR"].ToString()) < 0)
                            {
                                sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) + (Convert.ToDouble(dt_tmac.Rows[0]["W2VLMI4"].ToString().Trim()) * -1));
                            }
                            else
                            {
                                sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) + Convert.ToDouble(sWK_CRAMT));
                            };
                        }
                    }
                }

                if (sOption != "D")
                {
                    if (Convert.ToDouble(sB2AMDR) < 0)
                    {
                        sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) + (Convert.ToDouble(sB2AMDR) * -1));
                    }
                    else
                    {
                        sWK_DRAMT = Convert.ToString(Convert.ToDouble(sWK_DRAMT) + Convert.ToDouble(sB2AMCR));
                    }
                }
                if (Convert.ToDouble(sWK_IHWAMT) != Convert.ToDouble(sWK_DRAMT))
                {
                    this.ShowCustomMessage("원화 설정금액과 정리 금액이 일치하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    if (PAN10_VLMI6.CurControl is TYCodeBox)
                    { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                    else { this.SetFocus(PAN10_VLMI6.CurControl); }
                    return false;
                }
            }

            /* 42- 예산 확인 
             *     --------------------------------------
             *     예산관련계정
             *     1. 접대비, 판매촉진비  계정 전체
             *     2. 복리후생비 계정중 회식대
             *     3. 교육훈련비중 분임토의 회식비 
             *     -------------------------------------- 	 */
            sReturnValue = UP_FieldCheck_CDAC(sB2CDAC);
            string sY1YEAR = string.Empty;
            string sY1CDDP = string.Empty;
            string sY1CDSB = string.Empty;
            string sY1CDAC = string.Empty;
            /* --------------------- 예산계정이면 통과 ---------------------------- */
            if (sReturnValue != "00")
            {
                // 2012.12.23 접대비및 운영예산 등록시 정세진,최성희,최가연만 등록 가능 
                // 2014.06.27 기획공통(A90000) 접대비 및 운영예산 등록시 체크 안함 (회계팀 강경석)
                //if ((sReturnValue == "01" || sReturnValue == "02" || sReturnValue == "03")
                //    && (sOption == "A" || sOption == "C"))
                //{
                //    if ((sB2DPAC == "A90000") && (sB2HISAB != "0123-M" && sB2HISAB != "0312-F" && sB2HISAB != "0346-F" && sB2HISAB != "0391-F")) // 정세진,최성희,최가연,윤현진
                //    {
                //        this.ShowCustomMessage("기획공통(A90000) 예산 전표등록사번이 아닙니다. (접대비및 운영예산)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                //        return false;
                //    }
                //}

                if ((sReturnValue == "01" || sReturnValue == "02" || sReturnValue == "03" || sReturnValue == "04")
                    && (sOption == "A" || sOption == "C"))
                {

                    if (sReturnValue == "01" || sReturnValue == "02")
                    {
                        sWK_W2CDAC = sB2CDAC.Substring(0, 6) + "00";
                    }
                    else
                    {
                        sWK_W2CDAC = sB2CDAC;
                    }
                    sY1YEAR = sB2DTMK.Substring(0, 4);
                    sY1CDDP = sB2DPAC;
                    sY1CDSB = "";

                    /* 년예산에　등록된　계정인지 CHECK */
                    /*변대수(0002-M) && 귀속 C10000 -> 회장님 접대비 처리 2005.06.21*/
                    /* -------------------------------------------------------------------------------- */
                    /* 2013 년 부터 기획공통 윤세영,정우모,윤석민 별도 구분안함 기획팀 임현주 대리 요청 사항 */
                    /* -------------------------------------------------------------------------------- */
                    #region Description : 2013 년 부터 사용안함
                    //if (sB2DPAC == "A90000") { sB2HISAB = ""; }
                    //if ((sReturnValue == "01" || sReturnValue == "02" || sReturnValue == "03") &&
                    //    ((sB2HISAB == "0001-M" || sB2HISAB == "0016-M" || sB2HISAB == "0073-M") ||
                    //    (sB2HISAB == "0312-F" && sB2DPAC == "C10000") ||    // 최성희
                    //    (sB2HISAB == "0346-F" && sB2DPAC == "C10000") ||    // 최가연
                    //    (sB2HISAB == "0123-M" && sB2DPAC == "C10000") ||    // 정세진         
                    //    (sB2HISAB == "0002-M" && sB2DPAC == "C10000")))     // 변대수
                    //{
                    //    if (sReturnValue == "03")   // 2010.01.11 [회식대 46130115 윤세영,정우모,윤석민 부서 합계처리 
                    //    {
                    //        if ((sB2HISAB == "0001-M" && sB2DPAC == "C10000") || // 윤세영            
                    //            (sB2HISAB == "0016-M" && sB2DPAC == "C10000") || // 정우모
                    //            (sB2HISAB == "0073-M" && sB2DPAC == "C10000") || // 윤석민
                    //            (sB2HISAB == "0123-M" && sB2DPAC == "C10000"))   // 정세진
                    //        {
                    //            sY1CDSB = "";
                    //            sWK_HISAB = "";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (sB2HISAB == "0002-M" && sB2DPAC == "C10000")      // 변대수
                    //        {
                    //            sY1CDSB = "0001-M";
                    //            sWK_HISAB = "0001-M";
                    //        }
                    //        else if (sB2HISAB == "0312-F" && sB2DPAC == "C10000") // 최성희
                    //        {
                    //            sY1CDSB = "0001-M";
                    //            sWK_HISAB = "0001-M";
                    //        }
                    //        else if (sB2HISAB == "0346-F" && sB2DPAC == "C10000") // 최가연
                    //        {
                    //            sY1CDSB = "0001-M";
                    //            sWK_HISAB = "0001-M";
                    //        }
                    //        else if (sB2HISAB == "0123-M" && sB2DPAC == "C10000") // 정세진  (정우모 부회장 예산 사용)
                    //        {
                    //            sY1CDSB = "0016-M";
                    //            sWK_HISAB = "0016-M";
                    //        }
                    //        else
                    //        {
                    //            sY1CDSB = sB2HISAB;
                    //            sWK_HISAB = sB2HISAB;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    sY1CDSB = "";
                    //    sWK_HISAB = "";
                    //} 
                    #endregion

                    sY1CDAC = sWK_W2CDAC;

                    //년예산에　등록된　계정인지 CHECK
                    this.DbConnector.CommandClear();  // ACYSMF
                    this.DbConnector.Attach("TY_P_AC_2AM4G781", sY1YEAR, sY1CDDP, sY1CDSB, sY1CDAC); 
                    DataTable dt_ysmf = this.DbConnector.ExecuteDataTable();
                    if (dt_ysmf.Rows.Count == 0)
                    {
                        this.ShowCustomMessage("년예산에 등록된 귀속부서가 아닙니다.(접대비및 운영예산)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2DPAC.CodeText);
                        return false;
                    }
                    /* 접대비, 운영비계정의 경우 C10000(공통), A80000(울산공통),A90000(기획공통) 을 제외한 나머지 부서는 분기 예산초과를 체크한다.  */
                    string sY2YEAR = string.Empty;
                    string sY2MONTH = string.Empty;
                    string sY2CDDP = string.Empty;
                    string sY2CDSB = string.Empty;
                    string sY2CDAC = string.Empty;

                    if ((sReturnValue == "01" || sReturnValue == "02" || sReturnValue == "03" || sReturnValue == "04") &&
                        (sB2DPAC != "C10000" && sB2DPAC != "A80000" && sB2DPAC != "A90000"))
                    {
                        #region
                        /* 월　예산　등록된　계정인지 CHECK  */
                        sY2YEAR = sB2DTMK.Substring(0, 4);
                        sY2MONTH = sB2DTMK.Substring(4, 2);
                        sY2CDDP = sB2DPAC;

                        /* -------------------------------------------------------------------------------- */
                        /* 2013 년 부터 기획공통 윤세영,정우모,윤석민 별도 구분안함 기획팀 임현주 대리 요청 사항 */
                        /* -------------------------------------------------------------------------------- */
                        sY2CDSB = "";

                        #region Description : 2013년 부터 사용안함
                        //if (sB2DPAC == "A90000") { sB2HISAB = ""; }
                        //if ((sReturnValue == "01" || sReturnValue == "02" || sReturnValue == "03") &&
                        //    ((sB2HISAB == "0001-M" || sB2HISAB == "0016-M" || sB2HISAB == "0073-M") || // 윤세영,정우모,윤석민
                        //    (sB2HISAB == "0123-M" && sB2DPAC == "C10000") ||  // 정세진         
                        //    (sB2HISAB == "0312-F" && sB2DPAC == "C10000") ||  // 최성희
                        //    (sB2HISAB == "0346-F" && sB2DPAC == "C10000") ||  // 최가연
                        //    (sB2HISAB == "0002-M" && sB2DPAC == "C10000")))   // 변대수
                        //{
                        //    if (sReturnValue == "03")   // 2010.01.11 [회식대 46130115 윤세영,정우모,윤석민 부서 합계처리 
                        //    {
                        //        if ((sB2HISAB == "0001-M" && sB2DPAC == "C10000") || // 윤세영
                        //            (sB2HISAB == "0016-M" && sB2DPAC == "C10000") || // 정우모
                        //            (sB2HISAB == "0073-M" && sB2DPAC == "C10000") || // 윤석민
                        //            (sB2HISAB == "0123-M" && sB2DPAC == "C10000"))   // 정세진
                        //        {
                        //            sY2CDSB = "";
                        //            sWK_HISAB = "";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (sB2HISAB == "0002-M" && sB2DPAC == "C10000")     // 변대수
                        //        {
                        //            sY2CDSB = "0001-M";
                        //            sWK_HISAB = "0001-M";
                        //        }
                        //        else if (sB2HISAB == "0312-F" && sB2DPAC == "C10000") // 최성희
                        //        {
                        //            sY2CDSB = "0001-M";
                        //            sWK_HISAB = "0001-M";
                        //        }
                        //        else if (sB2HISAB == "0346-F" && sB2DPAC == "C10000") // 최가연
                        //        {
                        //            sY2CDSB = "0001-M";
                        //            sWK_HISAB = "0001-M";
                        //        }
                        //        else if (sB2HISAB == "0123-M" && sB2DPAC == "C10000") // 정세진 (정우모 부회장 예산 사용)
                        //        {
                        //            sY2CDSB = "0016-M";
                        //            sWK_HISAB = "0016-M";
                        //        }
                        //        else
                        //        {
                        //            sWK_HISAB = sB2HISAB;
                        //            sY2CDSB = sWK_HISAB;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    sY2CDSB = "";
                        //    sWK_HISAB = "";
                        //} 
                        #endregion

                        sY2CDAC = sWK_W2CDAC;

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4H782", sY2YEAR, sY2MONTH, sY2CDDP, sY2CDSB, sY2CDAC);  //ACYSMMF
                        DataTable dt_ysmmf = this.DbConnector.ExecuteDataTable();
                        if (dt_ysmmf.Rows.Count == 0)
                        {
                            this.ShowCustomMessage("월예산에 등록된 귀속부서가 아닙니다 (접대비및 운영예산)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }

                        // 2009.03.04 년단위 예산초과 -- 분기단위 초과금액으로 수정 (회계팀 요청)
                        // 2009.04.08 년단위 예산초과 -- 분기단위누계  초과금액으로 수정 (회계팀 요청)
                        //예)  1분기 접대비 ---> 1분기 접대비 예산
                        //     2분기 접대비 ---> 1분기, 2분기 접대비예산
                        //     3분기 접대비 ---> 1분기, 2분기, 3분기 접대비예산
                        //     4분기 접대비 ---> 1분기, 2분기, 3분기, 4분기 접대비예산
                        string sSTRMM = string.Empty;
                        string sENDMM = string.Empty;
                        string sSTRYYMM = string.Empty;
                        string sENDYYMM = string.Empty;
                        string sY2MONTH1 = string.Empty;
                        string sY2MONTH2 = string.Empty;

                        sY2YEAR = "";

                        sY2CDDP = "";
                        sY2CDSB = "";
                        sY2CDAC = "";

                        if (sB2DTMK.Substring(4, 2) == "01" || sB2DTMK.Substring(4, 2) == "02" || sB2DTMK.Substring(4, 2) == "03")
                        {
                            sSTRMM = "01";
                            sENDMM = "03";
                            sSTRYYMM = sB2DTMK.Substring(0, 4) + sSTRMM;
                            sENDYYMM = sB2DTMK.Substring(0, 4) + sENDMM;
                        }
                        if (sB2DTMK.Substring(4, 2) == "04" || sB2DTMK.Substring(4, 2) == "05" || sB2DTMK.Substring(4, 2) == "06")
                        {
                            sSTRMM = "01";
                            sENDMM = "06";
                            sSTRYYMM = sB2DTMK.Substring(0, 4) + sSTRMM;
                            sENDYYMM = sB2DTMK.Substring(0, 4) + sENDMM;
                        }
                        if (sB2DTMK.Substring(4, 2) == "07" || sB2DTMK.Substring(4, 2) == "08" || sB2DTMK.Substring(4, 2) == "09")
                        {
                            sSTRMM = "01";
                            sENDMM = "09";
                            sSTRYYMM = sB2DTMK.Substring(0, 4) + sSTRMM;
                            sENDYYMM = sB2DTMK.Substring(0, 4) + sENDMM;
                        }
                        if (sB2DTMK.Substring(4, 2) == "10" || sB2DTMK.Substring(4, 2) == "11" || sB2DTMK.Substring(4, 2) == "12")
                        {
                            sSTRMM = "01";
                            sENDMM = "12";
                            sSTRYYMM = sB2DTMK.Substring(0, 4) + sSTRMM;
                            sENDYYMM = sB2DTMK.Substring(0, 4) + sENDMM;
                        }

                        // 본예산 합계
                        sY2YEAR = sB2DTMK.Substring(0, 4);
                        sY2MONTH1 = sSTRMM;
                        sY2MONTH2 = sENDMM;
                        sY2CDDP = sB2DPAC;
                        sY2CDSB = "";
                        sY2CDAC = sWK_W2CDAC;

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4H783", sY2YEAR, sY2MONTH1, sY2MONTH2, sY2CDDP, sY2CDSB, sY2CDAC);  //ACYSMMF
                        DataTable dt_ysmm = this.DbConnector.ExecuteDataTable();
                        if (dt_ysmm.Rows.Count > 0)
                        {
                            sWK_YSAMT = dt_ysmm.Rows[0]["Y2AMT"].ToString();
                        }

                        // 수정예산 합계
                        //sY2YEAR = sB2DTMK.Substring(0, 4);
                        //sY2MONTH1 = sSTRMM;
                        //sY2MONTH2 = sENDMM;
                        //sY2CDDP = sB2DPAC;
                        //sY2CDSB = "";
                        //sY2CDAC = sWK_W2CDAC;

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4I784", sY2YEAR, sY2MONTH1, sY2MONTH2, sY2CDDP, sY2CDSB, sY2CDAC);  //ACYSMMF
                        DataTable dt_ysmm_pl = this.DbConnector.ExecuteDataTable();
                        if (dt_ysmm_pl.Rows.Count > 0)
                        {
                            sWK_YSAMT = Convert.ToString(double.Parse(sWK_YSAMT) + double.Parse(dt_ysmm_pl.Rows[0]["Y2PLAMT"].ToString()));
                        }

                        string sDPAC = string.Empty;
                        string sCDAC1 = string.Empty;
                        string sCDAC2 = string.Empty;
                        string sDTMK1 = string.Empty;
                        string sDTMK2 = string.Empty;

                        //기간동안 발행된 전표금액 합산 (년 --> 분기별로 변경)

                        sDPAC = sB2DPAC;
                        if (sReturnValue == "01" || sReturnValue == "02")
                        {
                            sCDAC1 = sWK_W2CDAC.Substring(0, 6) + "00";
                            sCDAC2 = sWK_W2CDAC.Substring(0, 6) + "99";
                        }
                        else if (sReturnValue == "03" || sReturnValue == "04")
                        {
                            sCDAC1 = sWK_W2CDAC;
                            sCDAC2 = sWK_W2CDAC;
                        }
                        sDTMK1 = sSTRYYMM;
                        sDTMK2 = sENDYYMM;

                        // 기간동안 발행된 전표금액 합산 (귀속부서,시작계정,종료계정,시작년월,종료년월)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4J785", sDPAC, sCDAC1, sCDAC2, sDTMK1, sDTMK2);  //ADSLGLF
                        DataTable dt_adls_dr = this.DbConnector.ExecuteDataTable();
                        if (dt_adls_dr.Rows.Count > 0)
                        {
                            sWK_GLAMT = Convert.ToString(double.Parse(dt_adls_dr.Rows[0]["B2AMDR"].ToString()));
                        }

                        string sWK_EDGLAM = "0"; // 현재 편집 중인 전표(부서+일자+순번) ADLSLGLF 에 대한 합계
                        string sWK_IMGLAM = "0"; // 현재 불러온 전표에 대한 합계
                        string sWK_IMAMAM = "0"; // 임시화일중 line 번호에 대하 값
                        double swk_TOTAMT1 = 0; // 계산
                        double swk_TOTAMT2 = 0; // 계산
                        double swk_TOTAMT3 = 0; // 계산

                        // 현재 불러온 전표에 대한 합계 (SSID,시작계정,종료계정,시작년월,종료년월,전표부서,전표일자,전표번호,귀속부서)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4J786", fsSessionId, sCDAC1, sCDAC2, sB2DPMK, sB2DTMK, sB2NOSQ, sB2DPAC);  //TMAC1102F
                        DataTable dt_tmacs_dr = this.DbConnector.ExecuteDataTable();
                        if (dt_tmacs_dr.Rows.Count > 0)
                        {
                            sWK_IMGLAM = Convert.ToString(double.Parse(dt_tmacs_dr.Rows[0]["W2AMDR"].ToString()));
                        }

                        //  현재 편집 중인 전표(부서+일자+순번) ADLSLGLF 에 대한 합계 (귀속부서,시작계정,종료계정,전표년,전표부서,전표일자,전표번호)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4K787", sB2DPAC, sCDAC1, sCDAC2, sB2DTMK.Substring(0, 4), sB2DTMK, sB2DPMK, sB2NOSQ);  //ADSLGLF
                        DataTable dt_adsl_gl = this.DbConnector.ExecuteDataTable();
                        if (dt_adsl_gl.Rows.Count > 0)
                        {
                            sWK_EDGLAM = Convert.ToString(double.Parse(dt_adsl_gl.Rows[0]["B2AMDR"].ToString()));
                        }

                        // 임시화일중 line 번호에 대한 값 (SSID,전표부서,전표일자,전표번호,전표순번)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4K788", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN);  //TMAC1102F
                        DataTable dt_tmac_im = this.DbConnector.ExecuteDataTable();
                        if (dt_tmac_im.Rows.Count > 0)
                        {
                            sWK_IMAMAM = Convert.ToString(double.Parse(dt_tmac_im.Rows[0]["W2AMDR"].ToString()));
                        }

                        swk_TOTAMT1 = double.Parse(sWK_GLAMT) - double.Parse(sWK_EDGLAM); // 기간동안 발행된 전표금액 합산 - 현재 편집 중인 전표(부서+일자+순번) ADLSLGLF 에 대한 합계
                        swk_TOTAMT2 = double.Parse(sWK_IMGLAM) - double.Parse(sWK_IMAMAM); // 현재 불러온 전표에 대한 합계(임시테이블) -  임시화일중 line 번호에 대한 값
                        swk_TOTAMT3 = swk_TOTAMT1 + swk_TOTAMT2 + double.Parse(this.TXT01_B2AMDR.GetValue().ToString());

                        if (double.Parse(sWK_YSAMT) < swk_TOTAMT3)
                        {
                            /* 입력 처리시 TMAC1102F 임시화일에 등록전이므로 접대비 화면을 수정할수 있게끔 수정버튼으로 활성화 시켜준다 */
                            /* 접대비 임시화일엔 TMA1102SF 에 이미등록 되어 있으므로 강제로 예산초과시 금액을 수정할수 있게 하여야 함 */
                            if ((this.txtfsA1CDMI1 == "27" || this.txtfsA1CDMI2 == "27" || this.txtfsA1CDMI2 == "27" ||
                                 this.txtfsA1CDMI4 == "27" || this.txtfsA1CDMI5 == "27" || this.txtfsA1CDMI6 == "27"))
                            {
                                if (this.BTN61_INP_RECP.Visible == false)
                                {
                                    this.DbConnector.CommandClear(); //TMAC1102SF
                                    this.DbConnector.Attach("TY_P_AC_2AB4K677", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN);
                                    DataTable dt = this.DbConnector.ExecuteDataTable();
                                    if (dt.Rows.Count > 0)
                                    {
                                        this.BTN61_SAV_RECP.Visible = true; // 수정
                                    }
                                }
                            }

                            //Message_Alert("분기 예산이 초과되었습니다!");

                            // 회식대
                            // 20180911 원본소스 - 주석 풀어야 함
                            this.ShowCustomMessage("분기 예산이 초과되었습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }

                        #endregion
                    }
                    else
                    {
                        /* 월　예산　등록된　계정인지 CHECK  */
                        sY2YEAR = sB2DTMK.Substring(0, 4);
                        sY2MONTH = sB2DTMK.Substring(4, 2);
                        sY2CDDP = sB2DPAC;
                        sY2CDAC = sWK_W2CDAC;
                        sY2CDSB = "";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4H782", sY2YEAR, sY2MONTH, sY2CDDP, sY2CDSB, sY2CDAC);  //ACYSMMF
                        DataTable dt_ysmmf = this.DbConnector.ExecuteDataTable();
                        if (dt_ysmmf.Rows.Count == 0)
                        {
                            this.ShowCustomMessage("월예산에 등록된 귀속부서가 아닙니다 (접대비및 운영예산)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }
                    }
                }
            } // 42 예산계정 체크 ...End


            // 43 예산계정이면 
            if ((this.txtfsA1TAG06 == "Y") &&
                (((this.txtfsA1TAG01 == "D") && (Convert.ToInt64(sB2AMDR) != 0)) ||
                ((this.txtfsA1TAG01 == "C") && (Convert.ToInt64(sB2AMCR) != 0))))
            {
                /* 본 예산 파일 체크 */
                //월　예산 마스타 파일체크
                bTrueAndFalse = UP_APPRMMF_Check(sB2DTMK.Substring(0, 4),
                                                 sB2DPAC, sB2CDAC,
                                                 sB2AMDR, sB2AMCR, sReturnValue);
                if (bTrueAndFalse == false)
                {
                    this.ShowCustomMessage("본 예산에 등록된 귀속부서가 아닙니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2DPAC.CodeText);
                    return false;
                }

                //월　예산 마스타 파일체크
                bTrueAndFalse = UP_APPRMMF_Check_YYMM(sB2DTMK.Substring(0, 4), sB2DTMK.Substring(4, 2),
                                                      sB2DPAC, sB2CDAC,
                                                      sB2AMDR, sB2AMCR, sReturnValue);
                if (bTrueAndFalse == false)
                {
                    this.ShowCustomMessage("등록하신 예산은 본예산에 등록된 해당월이 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2DPAC.CodeText);
                    return false;
                }

                /*   세목별 예산 등록 체크  */
                //2018년부터 여비교통비 관리항목에 예산번호를 제외한다.
                if ( sReturnValue == "21" || sReturnValue == "22" )
                {
                    if (Convert.ToInt16(sB2DTMK.Substring(0, 4)) <= 2017)
                    {
                        bTrueAndFalse = UP_YESAN_Check(sB2DTMK, sB2DPAC, sB2CDAC,
                                                       sB2VLMI1, sB2VLMI2, sB2VLMI3,
                                                       sB2VLMI4, sB2VLMI5, sB2VLMI6,
                                                       sB2AMDR, sB2AMCR, sReturnValue);
                        if (bTrueAndFalse == false)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //소모품비세목관리 예산중에 연료대만 2019년부터 예산 체크
                    if (sReturnValue == "33" )
                    {
                        if (Convert.ToInt16(sB2DTMK.Substring(0, 4)) > 2018)
                        {
                            bTrueAndFalse = UP_YESAN_Check(sB2DTMK, sB2DPAC, sB2CDAC,
                                                           sB2VLMI1, sB2VLMI2, sB2VLMI3,
                                                           sB2VLMI4, sB2VLMI5, sB2VLMI6,
                                                           sB2AMDR, sB2AMCR, sReturnValue);
                            if (bTrueAndFalse == false)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        bTrueAndFalse = UP_YESAN_Check(sB2DTMK, sB2DPAC, sB2CDAC,
                                                       sB2VLMI1, sB2VLMI2, sB2VLMI3,
                                                       sB2VLMI4, sB2VLMI5, sB2VLMI6,
                                                       sB2AMDR, sB2AMCR, sReturnValue);
                        if (bTrueAndFalse == false)
                        {
                            return false;
                        }
                    }
                }
            } // 43 예산계정이면 ... END

            /* 해외사업부　선수금　금액 UPDATE  CHECK. */
            if ((sB2DPAC.Substring(0, 1) == "B") && (sB2CDAC.Substring(0, 6) == "211008"))  // 211009
            {
                int iCount = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AN4M805", sB2DTMK.Substring(0, 6)); // TRMEMISF
                iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCount == 0)
                {
                    this.ShowCustomMessage("매출 이월후 등록하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2CDAC.CodeText);
                    return false;
                }
            }

            /* UTT, SILO,무역  미수금 파일 확인 */
            if ((sB2CDAC == "11100401" || sB2CDAC == "11100402" || sB2CDAC == "11100403" || sB2CDAC == "11100404" ||
                 sB2CDAC == "11100405" || sB2CDAC == "11100411" || sB2CDAC == "11100412" || sB2CDAC == "11100413" ||
                 sB2CDAC == "11100414" || sB2CDAC == "11100415" || sB2CDAC == "11100416" || sB2CDAC == "11100417" ||
                 sB2CDAC == "11100418" || sB2CDAC == "11100419" || sB2CDAC == "11100420" || sB2CDAC == "11100421" ||
                 sB2CDAC == "11100422" || sB2CDAC == "11101002") && Convert.ToDouble(sB2AMCR) > 0)
            {
                string sWK_FileCode = "";

                //거래처 코드 
                sWK_FileCode = UP_CDMIToVLMI("01", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3,
                                                   this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                                   sB2VLMI1, sB2VLMI2, sB2VLMI3,
                                                   sB2VLMI4, sB2VLMI5, sB2VLMI6);
                //T00000 - UTT
                if (sB2DPAC.Substring(0, 1) == "T")
                {
                    string sMIYYMM = "";
                    string sMIHWAJU = "";
                    string sMIMAECH = "";

                    //일자
                    sMIYYMM = sB2DTMK.Substring(0, 6);
                    //거래처
                    // (현업 거래처 코드를 가지고 옮) 반제TABLE 에 있는것을 우선처리 
                    if (this.TXT01_B2WCJP.GetValue().ToString() != "")
                    {
                        sMIHWAJU = UP_Get_VendCode_HWAJU_ABANJMF(this.TXT01_B2WCJP.GetValue().ToString().Trim().Substring(0, 19));
                    }

                    if (sMIHWAJU == "")
                    {
                        sMIHWAJU = UP_Get_VendCode(sWK_FileCode, "T");
                    }

                    if (sMIHWAJU == "")
                    {
                        this.ShowCustomMessage("UTT 미수금 거래처가 올바르지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }
                    switch (sB2CDAC)
                    {
                        case "11100412":
                            sMIMAECH = "01"; //접  안  료  11100506
                            break;
                        case "11101002":
                            sMIMAECH = "02"; //보  험  료  11300101
                            break;
                        case "11100413":
                            sMIMAECH = "03"; //하  역  료  11100507
                            break;
                        case "11100411":
                            sMIMAECH = "04"; //보  관  료  11100505
                            break;
                        case "11100414":
                            sMIMAECH = "05"; //취  급  료   11100508
                            break;
                    }

                    int iCount = 0;
                    this.DbConnector.CommandClear();  // UTIMISUF
                    this.DbConnector.Attach("TY_P_AC_2AN4N806", sMIYYMM, sMIHWAJU, sMIMAECH);
                    iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCount == 0)
                    {
                        this.ShowCustomMessage("UTT 미수금 파일(UTIMISUF)에 자료가 존재하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }
                }

                //S00000 - SILO
                if (sB2DPAC.Substring(0, 1) == "S")
                {
                    string sMIYYMM = "";
                    string sMIHWAJU = "";
                    string sMIMAECH = "";

                    //일자
                    sMIYYMM = sB2DTMK.Substring(0, 6);
                    //거래처 코드 
                    // (현업 거래처 코드를 가지고 옮) 반제TABLE 에 있는것을 우선처리 

                    if (this.TXT01_B2WCJP.GetValue().ToString() != "")
                    {
                        sMIHWAJU = UP_Get_VendCode_HWAJU_ABANJMF(this.TXT01_B2WCJP.GetValue().ToString().Trim().Substring(0, 19));
                    }

                    if (sMIHWAJU == "")
                    {
                        sMIHWAJU = UP_Get_VendCode(sWK_FileCode, "S");
                    }

                    if (sMIHWAJU == "")
                    {
                        this.ShowCustomMessage("SILO 미수금 거래처가 올바르지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }

                    switch (sB2CDAC)
                    {
                        case "11100422":
                            sMIMAECH = "10"; //훈증 소득료   11100512
                            break;
                        case "11100412":
                            sMIMAECH = "11"; //접  안  료  11100506
                            break;
                        case "11100415":
                            sMIMAECH = "12"; //시설 사용료   11100509
                            break;
                        case "11100413":
                            sMIMAECH = "13"; //하 역 료     11100507
                            break;
                        case "11100411":
                            sMIMAECH = "14"; //보 관 료     11100505
                            break;
                        case "11100416":
                            sMIMAECH = "15"; // 조 출 료    11100511
                            break;
                        case "11100419":
                            sMIMAECH = "17"; //포대 개포작업비   11100513
                            break;
                        case "11100421":
                            sMIMAECH = "18"; //훈증 시설사용료   11100510
                            break;
                        case "11100417":
                            sMIMAECH = "19"; //화  물  료   11100514
                            break;
                        case "11100418":
                            sMIMAECH = "20"; //현대화 기금   11100516
                            break;
                        case "11100414":
                            sMIMAECH = "21"; //취  급  료   11100508
                            break;
                        case "11100420":
                            sMIMAECH = "22"; //중개수수료   11100524
                            break;
                    }
                    int iCount = 0;
                    this.DbConnector.CommandClear(); // USIMISUF
                    this.DbConnector.Attach("TY_P_AC_2AN4N807", sMIYYMM, sMIHWAJU, sMIMAECH); 
                    iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCount == 0)
                    {
                        this.ShowCustomMessage("SILO 미수금 파일(USIMISUF)에 자료가 존재하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }
                }

                //무역부- O,B
                if (sB2DPAC.Substring(0, 1) == "B")
                {
                    string sMESILJA = "";
                    string sMESGECD = "";

                    //일자
                    sMESILJA = sB2DTMK.Substring(0, 6);
                    //거래처 코드 
                    sMESGECD = sWK_FileCode;

                    int iCount = 0;
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AN4U808", sMESILJA, sMESGECD); // TRMEMISF
                    iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCount == 0 && sOption == "A")
                    {
                        this.ShowCustomMessage("무역 미수금 파일(TRMEMISF)에 자료가 존재하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2CDAC.CodeText);
                        return false;
                    }
                }
            }

            /*  46 . 불공제 체크  */
            if (sB2CDAC == "11103101" || sB2CDAC == "11103102")
            {
                 /* (54)매입세액불공제,(55)소형승용자불공제,(74-전자)매입세액불공제,(75-전자)승용차 불공제 경우  */
                if (sB2VLMI1 == "54" || sB2VLMI1 == "55" || sB2VLMI1 == "74" || sB2VLMI1 == "75")
                {
                    //<< 불공재 등록여부 확인  TMAC1102BF >>
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2BK12486", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN);
                    DataTable dt_txex = this.DbConnector.ExecuteDataTable();
                    if (dt_txex.Rows.Count > 0)
                    {
                        //화면의 관리항목에 등록된 내용과  외화설정파일의 내용과 일치여부 확인
                        if (dt_txex.Rows[0]["CNT"].ToString().Trim() == "0")
                        {
                            this.ShowCustomMessage("불공제 파일(TMAC1102BF)미존재, (불공등록) 선택하여 등록하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI1.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI1.CurControl); }
                            return false;
                        }

                        if (Convert.ToDouble(Get_Numeric(sB2VLMI4)) != Convert.ToDouble(Get_Numeric(dt_txex.Rows[0]["SUM"].ToString().Trim())))
                        {
                            this.ShowCustomMessage("관리항목 금액과 불공제등록 금액이 일치 하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (PAN10_VLMI4.CurControl is TYCodeBox)
                            { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN10_VLMI4.CurControl); }
                            return false;
                        }

                        //*-----------------------------------------------------------------------------*
                        //  차대변 합계 금액과 불공제금액(공급가,부가세)이 일치해야 전표발행이 가능하다.
                        //-------------------------------------------------------------------------------*
                        //if (Convert.ToDouble(Get_Numeric(this.TXT01_B2AMDRTOTAL.GetValue().ToString().Trim())) != Convert.ToDouble(Get_Numeric(dt_txex.Rows[0]["SUM"].ToString().Trim())))
                        //{
                        //    this.ShowCustomMessage("차대변 금액과 불공제등록 금액이 일치 하지 않습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        //    this.SetFocus(this.TXT01_B2AMDR);
                        //    return false;
                        //}

                        if (double.Parse(sB2AMDR) != 0 || double.Parse(sB2AMCR) != 0)
                        {
                            this.ShowCustomMessage("불공제분에 대해 차.대변 금액이 올수 없습니다.(차.대변 확인) ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_B2AMDR);
                            return false;
                        }
                    }

                }
            } // .... 46 불공제분 체크 ... End

            /* 47- 출장금액 확인 및 문서 확인 체크  2013.06.10 추가됨 (이전 전표에 대해서는 관리항목 자료가 없음) */
            if ((sB2CDAC == "42411201" || sB2CDAC == "42411202" || sB2CDAC == "44111201" || sB2CDAC == "44111202" ||
                 sB2CDAC == "44121201" || sB2CDAC == "44121202" || sB2CDAC == "44211201" || sB2CDAC == "44211202") && double.Parse(sB2AMDR) > 0)
            {
                string sSABUN = string.Empty;
                string sBess = string.Empty;
                sSABUN = UP_CDMIToVLMI("05", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                             sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);


                sBess = UP_CDMIToVLMI("37", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                             sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);

                this.DbConnector.CommandClear();  // GTHUEXPENSEF
                //ORA
                //this.DbConnector.Attach("TY_P_AC_35F9J675", sSABUN, sBess);
                //DB2 신인사용(2016년1월사용)

                // 20181204일 수정 - 원본
                //this.DbConnector.Attach("TY_P_AC_5BCEY156", sSABUN, sBess, sB2DTMK.Substring(0,6));

                string sDATE   = sB2DTMK.ToString().Substring(0, 4) + "-" + sB2DTMK.ToString().Substring(4, 2) + "-01";
                DateTime dDate = Convert.ToDateTime(sDATE).AddMonths(-6);

                string sSTYYMM = dDate.ToString().Substring(0, 4) + dDate.ToString().Substring(5, 2);
                string sEDYYMM = sB2DTMK.Substring(0, 6);

                this.DbConnector.Attach("TY_P_AC_5BCEY156", sSABUN, sBess, sSTYYMM.ToString(), sEDYYMM.ToString());
                DataTable dt_bas = this.DbConnector.ExecuteDataTable();

                string sCOSTAMT = string.Empty;
                string sUSAMT = string.Empty;

                if (dt_bas.Rows.Count > 0)
                {
                    sCOSTAMT = dt_bas.Rows[0]["TOTCOST"].ToString().Trim();  //출장금액 전체
                    sUSAMT = dt_bas.Rows[0]["GXJUNTOTAL"].ToString().Trim();  //출장금액 전표발행금액

                    if (sOption == "A")
                    {
                        if ((Convert.ToDouble(sCOSTAMT.ToString()) - Convert.ToDouble(sUSAMT.ToString())) < Convert.ToDouble(this.TXT01_B2AMDR.GetValue().ToString().Trim()))
                        {
                            this.ShowCustomMessage("출장금액 을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_B2AMDR);
                            return false;
                        }
                    }
                    // 수정 및 삭제
                    if (sOption == "C" || sOption == "D") 
                    {
                        string sWK_EDGLAM = string.Empty;  // 현재 편집 중인 전표(부서+일자+순번) TMAC1102F 에 대한 합계
                        double swk_TOTAMT1 = 0;

                        // 임시화일중 line 번호에 대한 값 (SSID,전표부서,전표일자,전표번호,전표순번)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AM4K788", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ, sB2NOLN);  //TMAC1102F
                        DataTable dt_tmac_im = this.DbConnector.ExecuteDataTable();
                        if (dt_tmac_im.Rows.Count > 0)
                        {
                            sWK_EDGLAM = Convert.ToString(double.Parse(dt_tmac_im.Rows[0]["W2AMDR"].ToString()));
                        }

                        // 기간동안 발행된 전표금액 합산 - 현재 편집 중인 전표(부서+일자+순번) TMAC1102F 에 대한 합계(변경전금액) + 현재 라인 수정금액(변경금액)
                        swk_TOTAMT1 = (double.Parse(sUSAMT) - double.Parse(sWK_EDGLAM)) + double.Parse(this.TXT01_B2AMDR.GetValue().ToString()); 

                        // 출장전체금액 
                        if (double.Parse(sCOSTAMT) < swk_TOTAMT1)
                        {
                            this.ShowCustomMessage("출장금액을 확인하세요- 출장 전체금액 초과.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_B2AMDR);
                            return false;
                        }
                    }
                }
                else
                {
                    //2015.10.02 윤석민 부회장은 제외한다.
                    if (sSABUN != "0073-M")
                    {
                        this.ShowCustomMessage("출장내역 자료가 존재 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_B2AMDR);
                        return false;
                    }
                }
            } // 47.. 출장금액 확인 ... End 



            //49 입항일자 체크
            bOptonCheck = false;
            sSubCode = "";
            sSubCode = UP_CDMIToVLMI("49", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                          sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);

            if (sSubCode != "")
            {
                bool bRtn = dateValidateCheck(sSubCode.Trim());
                if (!bRtn)
                {
                    if (sB2VLMI1 != "")
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI1.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI1.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI1.CurControl); }
                        return false;

                    }
                    if (sB2VLMI2 != "")
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI2.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI2.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI2.CurControl); }
                        return false;
                    }
                    if (sB2VLMI3 != "")
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI3.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI3.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI3.CurControl); }
                        return false;
                    }
                    if (sB2VLMI4 != "")
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI4.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI4.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI4.CurControl); }
                        return false;
                    }
                    if (sB2VLMI5 != "")
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI5.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI5.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI5.CurControl); }
                        return false;
                    }
                    if (sB2VLMI6 != "")
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (PAN10_VLMI6.CurControl is TYCodeBox)
                        { this.SetFocus((PAN10_VLMI6.CurControl as TYCodeBox).CodeText); }
                        else { this.SetFocus(PAN10_VLMI6.CurControl); }
                        return false;
                    }
                }
            }

            //38 자산관리번호 체크
            if ((this.txtfsA1CDMI1 == "38" || this.txtfsA1CDMI2 == "38" || this.txtfsA1CDMI2 == "38" ||
                 this.txtfsA1CDMI4 == "38" || this.txtfsA1CDMI5 == "38" || this.txtfsA1CDMI6 == "38") &&
                (Convert.ToDouble(sB2AMDR) > 0 || Convert.ToDouble(sB2AMCR) > 0))
            {
                string sFXSNUM = UP_CDMIToVLMI("38", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3,
                                                     this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                                     sB2VLMI1, sB2VLMI2, sB2VLMI3,
                                                     sB2VLMI4, sB2VLMI5, sB2VLMI6);

                if ((sB2CDAC == "12200100" || sB2CDAC == "12200200" || sB2CDAC == "12200300" || sB2CDAC == "12200400" || sB2CDAC == "12200500" ||
                     sB2CDAC == "12200600" || sB2CDAC == "12200700" || sB2CDAC == "12200800" || sB2CDAC == "12200900") && (sFXSNUM != ""))
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_8CAE6291", sFXSNUM.Substring(0, 4), sFXSNUM.Substring(4, 4), sFXSNUM.Substring(8, 3));
                    DataTable dt_asset = this.DbConnector.ExecuteDataTable();
                    if (dt_asset.Rows.Count > 0)
                    {
                        string sRtnValue = string.Empty;
                        switch (dt_asset.Rows[0]["FXSCLASS"].ToString().Substring(0, 1))
                        {
                            case "1":  //토지
                                sRtnValue = "12200100";
                                break;
                            case "2":  //건물
                                sRtnValue = "12200200";
                                break;
                            case "3":  //구축물
                                sRtnValue = "12200300";
                                break;
                            case "4":  //기계장치
                                sRtnValue = "12200400";
                                break;
                            case "5":  //중기
                                sRtnValue = "12200500";
                                break;
                            case "6":  //차량운반구
                                sRtnValue = "12200600";
                                break;
                            case "7":  //공구와기구
                                sRtnValue = "12200700";
                                break;
                            case "8":  //비품
                                sRtnValue = "12200800";
                                break;
                            case "9":  //시설장치
                                sRtnValue = "12200900";
                                break;
                        }

                        if (sB2CDAC != sRtnValue)
                        {
                            TYItemPanel PAN_VLMI = new TYItemPanel();

                            this.ShowCustomMessage("자산분류와 계정과목코드가 다릅니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (sB2VLMI1 != "")
                            {
                                PAN_VLMI = PAN10_VLMI1;
                            }
                            if (sB2VLMI2 != "")
                            {
                                PAN_VLMI = PAN10_VLMI2;
                            }
                            if (sB2VLMI3 != "")
                            {
                                PAN_VLMI = PAN10_VLMI3;
                            }
                            if (sB2VLMI4 != "")
                            {
                                PAN_VLMI = PAN10_VLMI4;
                            }
                            if (sB2VLMI5 != "")
                            {
                                PAN_VLMI = PAN10_VLMI5;
                            }
                            if (sB2VLMI6 != "")
                            {
                                PAN_VLMI = PAN10_VLMI6;
                            }

                            if (PAN_VLMI.CurControl is TYCodeBox)
                            { this.SetFocus((PAN_VLMI.CurControl as TYCodeBox).CodeText); }
                            else { this.SetFocus(PAN_VLMI.CurControl); }

                            return false;
                        }

                    }
                    else
                    {
                        this.ShowCustomMessage("자산번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return false;
                    }
                }
            }


            return true;
        }  
        #endregion


        #region Description : 매출입금 관련 사업장별 거래처 코드 조회 함수  ---> UP_Get_VendCode()
        private string UP_Get_VendCode(string sVNCODE, string sGUBN)
        {
            string sReSultVNCODE = "";

            if (sGUBN == "S")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AN4G802", sVNCODE);  //USIVENDF
                DataTable dt_S = this.DbConnector.ExecuteDataTable();
                if (dt_S.Rows.Count > 0)
                {
                    sReSultVNCODE = dt_S.Rows[0]["VNCODE"].ToString().Trim();
                }
            }
            else if (sGUBN == "T")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AN4H803", sVNCODE);  //UTIVENDF
                DataTable dt_T = this.DbConnector.ExecuteDataTable();
                if (dt_T.Rows.Count > 0)
                {
                    sReSultVNCODE = dt_T.Rows[0]["VNCODE"].ToString().Trim();
                }
            }
            else if (sGUBN == "B")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AN4H804", sVNCODE);  //TRVENDF
                DataTable dt_B = this.DbConnector.ExecuteDataTable();
                if (dt_B.Rows.Count > 0)
                {
                    sReSultVNCODE = dt_B.Rows[0]["VNCODE"].ToString().Trim();
                }
            }

            return sReSultVNCODE;
        }

        #endregion


        #region Description : 계정과목코드 => 관리항목,OPTION 값가져오기  ---> UP_GetACDMIMF()
        private void UP_GetACDMIMF(string sB2CDAC)
        {
            //계정과목 조회(상세)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3L884", sB2CDAC);
            DataSet ds = this.DbConnector.ExecuteDataSet();
            if (ds.Tables[0].Rows.Count != 0)
            {
                /*  관리항목코드  */
                txtfsA1CDMI1 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI1"].ToString());
                txtfsA1CDMI2 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI2"].ToString());
                txtfsA1CDMI3 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI3"].ToString());
                txtfsA1CDMI4 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI4"].ToString());
                txtfsA1CDMI5 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI5"].ToString());
                txtfsA1CDMI6 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI6"].ToString());

                /*  계정과목 관련 각종 코드 변수 */
                txtfsA1TAG01 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG01"].ToString());    /* 19-차/대        */
                txtfsA1TAG02 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG02"].ToString());    /* 20-전표계정     */
                txtfsA1TAG03 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG03"].ToString());    /* 21-관리대장KEY  */
                txtfsA1TAG04 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG04"].ToString());    /* 22-기간비용정리 */
                txtfsA1TAG05 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG05"].ToString());    /* 23-자금관리     */
                txtfsA1TAG06 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG06"].ToString());    /* 24-예산통제여부 */
                txtfsA1TAG07 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG07"].ToString());    /* 25-반제관리     */
                txtfsA1TAG08 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG08"].ToString());    /* 26-잔액명세서출력*/
                txtfsA1TAG09 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG09"].ToString());    /* 27-접대비        */
                txtfsA1TAG10 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG10"].ToString());    /* 28-충당금        */
                txtfsA1TAG11 = SetDefaultValue(ds.Tables[0].Rows[0]["A1TAG11"].ToString());    /* 29-반제연결      */

                txtfsA1OTMI1 = SetDefaultValue(ds.Tables[0].Rows[0]["A1OTMI1"].ToString());
                txtfsA1OTMI2 = SetDefaultValue(ds.Tables[0].Rows[0]["A1OTMI2"].ToString());
                txtfsA1OTMI3 = SetDefaultValue(ds.Tables[0].Rows[0]["A1OTMI3"].ToString());
                txtfsA1OTMI4 = SetDefaultValue(ds.Tables[0].Rows[0]["A1OTMI4"].ToString());
                txtfsA1OTMI5 = SetDefaultValue(ds.Tables[0].Rows[0]["A1OTMI5"].ToString());
                txtfsA1OTMI6 = SetDefaultValue(ds.Tables[0].Rows[0]["A1OTMI6"].ToString());

            }
        }
        #endregion


        #region Description : 미승인등록 필드 LOCK   ---> UP_FieldLock()
        private void UP_FieldLock(bool bFieldLock)
        {
            this.CBO01_B2NOLN.SetReadOnly(bFieldLock);

            this.CBH01_B2CDAC.SetReadOnly(bFieldLock);
            this.CBH01_B2DPAC.SetReadOnly(bFieldLock);
            this.PAN10_VLMI1.SetReadOnly(bFieldLock);
            this.PAN10_VLMI2.SetReadOnly(bFieldLock);
            this.PAN10_VLMI3.SetReadOnly(bFieldLock);
            this.PAN10_VLMI4.SetReadOnly(bFieldLock);
            this.PAN10_VLMI5.SetReadOnly(bFieldLock);
            this.PAN10_VLMI6.SetReadOnly(bFieldLock);
            // false
            if (bFieldLock == false)
            {
                this.TXT01_B2AMDR.Visible = true;
                this.TXT01_B2AMCR.Visible = true;
                this.TXT01_B2WCJP.Visible = true;
                this.TXT01_B2RKAC.Visible = true;
                this.TXT01_B2RKCU.Visible = true;
            }
            else
            {
                this.TXT01_B2AMDR.Visible = false;
                this.TXT01_B2AMCR.Visible = false;
                this.TXT01_B2WCJP.Visible = false;
                this.TXT01_B2RKAC.Visible = false;
                this.TXT01_B2RKCU.Visible = false;
            }


            //this.TXT01_B2AMDR.SetReadOnly(bFieldLock);
            //this.TXT01_B2AMCR.SetReadOnly(bFieldLock);
            //this.TXT01_B2WCJP.SetReadOnly(bFieldLock);
            //this.TXT01_B2RKAC.SetReadOnly(bFieldLock);
            //this.TXT01_B2RKCU.SetReadOnly(bFieldLock);

            //txtB2CDFD.ReadOnly = bFieldLock;
            //txtB2AMFD.ReadOnly = bFieldLock;

            //this.CBO01_B2NOLN.Enabled = bFieldLock;

            //this.CBH01_B2CDAC.Enabled = bFieldLock;
            //this.CBH01_B2DPAC.Enabled = bFieldLock;
            //this.PAN10_VLMI1.Enabled = bFieldLock;
            //this.PAN10_VLMI2.Enabled = bFieldLock;
            //this.PAN10_VLMI3.Enabled = bFieldLock;
            //this.PAN10_VLMI4.Enabled = bFieldLock;
            //this.PAN10_VLMI5.Enabled = bFieldLock;
            //this.PAN10_VLMI6.Enabled = bFieldLock;
            //this.TXT01_B2AMDR.Enabled = bFieldLock;
            //this.TXT01_B2AMCR.Enabled = bFieldLock;
            //this.TXT01_B2WCJP.Enabled = bFieldLock;
            //this.TXT01_B2RKAC.Enabled = bFieldLock;
            //this.TXT01_B2RKCU.Enabled = bFieldLock;


        } 
        #endregion

        #region Description : 미승인등록 필드 CLEAR  --> UP_FieldClear()
        private void UP_FieldClear()
        {
            this.CBH01_B2CDAC.SetValue("");
            this.CBH01_B2DPAC.SetValue("");

            this.PAN10_VLMI1.Initialize();
            this.PAN10_VLMI2.Initialize();
            this.PAN10_VLMI3.Initialize();
            this.PAN10_VLMI4.Initialize();
            this.PAN10_VLMI5.Initialize();
            this.PAN10_VLMI6.Initialize();

            this.TXT01_B2AMDR.SetValue("");
            this.TXT01_B2AMCR.SetValue("");
            this.TXT01_B2RKCU.SetValue("");
            this.TXT01_B2RKAC.SetValue("");
            this.TXT01_B2WCJP.SetValue("");
            //txtB2CDFD.Text = "";
            //txtB2AMFD.Text = "";

         

        } 
        #endregion

        #region Description : 미승인등록 화면값 --> 저장변수 담기  ---> UP_ScreenFile_Save()
        private void UP_ScreenFile_Save()
        {
            string sValue = string.Empty;
            string sText = string.Empty;
            string sItem = string.Empty;
            string sSS4 = string.Empty;

            this.DAT02_W2SSID.SetValue(fsSessionId);
            this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue().ToString());
            this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString());
            this.DAT02_W2NOSQ.SetValue(this.TXT01_B2NOSQ.GetValue().ToString());
            this.DAT02_W2NOLN.SetValue(this.CBO01_B2NOLN.SelectedItem.ToString());

            this.DAT02_W2IDJP.SetValue(this.CBO01_B2IDJP.GetValue().ToString());
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(this.CBH01_B2CDAC.GetValue().ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPAC.GetValue().ToString());

            UP_SetVLMIVALUE(); // 관리항목 셋팅

            this.DAT02_W2CDMI1.SetValue(this.fsCDMI01);
            this.DAT02_W2VLMI1.SetValue(this.fsVLMI01);
            this.DAT02_W2CDMI2.SetValue(this.fsCDMI02);
            this.DAT02_W2VLMI2.SetValue(this.fsVLMI02);
            this.DAT02_W2CDMI3.SetValue(this.fsCDMI03);
            this.DAT02_W2VLMI3.SetValue(this.fsVLMI03);
            this.DAT02_W2CDMI4.SetValue(this.fsCDMI04);
            this.DAT02_W2VLMI4.SetValue(this.fsVLMI04);
            this.DAT02_W2CDMI5.SetValue(this.fsCDMI05);
            this.DAT02_W2VLMI5.SetValue(this.fsVLMI05);
            this.DAT02_W2CDMI6.SetValue(this.fsCDMI06);
            this.DAT02_W2VLMI6.SetValue(this.fsVLMI06);

            this.DAT02_W2AMDR.SetValue(this.TXT01_B2AMDR.GetValue().ToString());
            this.DAT02_W2AMCR.SetValue(this.TXT01_B2AMCR.GetValue().ToString());
            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(this.TXT01_B2RKAC.GetValue().ToString());
            this.DAT02_W2RKCU.SetValue(this.TXT01_B2RKCU.GetValue().ToString());
            this.DAT02_W2WCJP.SetValue(this.TXT01_B2WCJP.GetValue().ToString());
            this.DAT02_W2PRGB.SetValue("");
            this.DAT02_W2HIGB.SetValue("A");
            // 23.02.20 수정 후 소스
            this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
            // 23.02.20 수정 전 소스
            //this.DAT02_W2HISAB.SetValue(CBH01_B2HISAB.GetValue().ToString());
            this.DAT02_W2GUBUN.SetValue("");
            this.DAT02_W2TXAMT.SetValue("0");
            this.DAT02_W2TXVAT.SetValue("0");
            this.DAT02_W2HWAJU.SetValue("");
        } 
        #endregion

        #region Description : 미승인등록 관리항목 세팅작업  --->  UP_SetVLMIVALUE()
        private void UP_SetVLMIVALUE()
        {
            try
            {
                if (this.fsCDMI01 != "")
                {
                    fsCDMI01 = this.PAN10_VLMI1.GetCurCode().ToString();
                    fsVLMI01 = this.PAN10_VLMI1.GetValue().ToString();
                }
                else
                {
                    fsCDMI01 = "";
                    fsVLMI01 = "";
                };

                if (this.fsCDMI02 != "")
                {
                    fsCDMI02 = this.PAN10_VLMI2.GetCurCode().ToString();
                    fsVLMI02 = this.PAN10_VLMI2.GetValue().ToString();
                }
                else
                {
                    fsCDMI02 = "";
                    fsVLMI02 = "";
                };

                if (this.fsCDMI03 != "")
                {
                    fsCDMI03 = this.PAN10_VLMI3.GetCurCode().ToString();
                    fsVLMI03 = this.PAN10_VLMI3.GetValue().ToString();
                }
                else
                {
                    fsCDMI03 = "";
                    fsVLMI03 = "";
                };

                if (this.fsCDMI04 != "")
                {
                    fsCDMI04 = this.PAN10_VLMI4.GetCurCode().ToString();
                    fsVLMI04 = this.PAN10_VLMI4.GetValue().ToString();
                }
                else
                {
                    fsCDMI04 = "";
                    fsVLMI04 = "";
                };

                if (this.fsCDMI05 != "")
                {
                    fsCDMI05 = this.PAN10_VLMI5.GetCurCode().ToString();
                    fsVLMI05 = this.PAN10_VLMI5.GetValue().ToString();
                }
                else
                {
                    fsCDMI05 = "";
                    fsVLMI05 = "";
                };

                if (this.fsCDMI06 != "")
                {
                    fsCDMI06 = this.PAN10_VLMI6.GetCurCode().ToString();
                    fsVLMI06 = this.PAN10_VLMI6.GetValue().ToString();
                }
                else
                {
                    fsCDMI06 = "";
                    fsVLMI06 = "";
                };
            }
            catch { }
            finally { }
        } 
        #endregion

        #region Description : 관리항목 체크 -------------------------------------------------- (수정 요망)
        //private string fFunction_FieldCheck_CDMI(string sB2CDAC, string sA1OTMI, string sA1CDMI, string sVLMI, string sB2DPAC)
        //{
            //string sCall_Code = "";
            //string sCall_Index = "";
            //string sReturnValue = "";
 
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2BF70363", sA1CDMI, sVLMI, ""); // 함수 호출 TYSCMLIB.SF_GB_FINVLMICHK
            //sReturnValue = Convert.ToString(this.DbConnector.ExecuteScalar());


            //sCall_Index = "00" + sA1CDMI;
            //sCall_Code = sVLMI;

            ///* << 예산세목 Check(관항35) >>
            //'*--------------------------------------------------------------------------*
            //'* sCall_Code 구조 :
            //'*   XX (21:98기타예산, 22:98여비교통비예산, 23:98소모품비예산, 기타:97세목예산)
            //'*   XXXXXXXX (계정코드)
            //'*   X(20) (예산코드 :[21 : XXXX=년도 XXXXXX=부서 XXX=순번]
            //'*                    [22 : XXXX=년도 XXXXXX=부서 XXXXXX=사번  XXX=순번]
            //'*                    [23 : XXXX=년도 XXXXXX=부서 XXX=자재구분 XXX=순번]
            //'*                    [98 이전예산  : X=예산구분  X=부서구분   XXXX=년도 XXX=순번]
            //'*--------------------------------------------------------------------------*/
            //CommExecute.clsQuery oInquery = new CommExecute.clsQuery();

            //if (sA1CDMI == "35")
            //{
            //    sReturnValue = UP_FieldCheck_CDAC(sB2CDAC);
            //    sCall_Code = sReturnValue.Substring(0, 2) + sB2CDAC + sVLMI;
            //}

            ///*<< 지급어음 확인시 지급어음파일에 등록된 은행과 관리항목에 
            // *  등록된 은행의 일치여부를 같이Check >>
            //  sCall_Code : 1~20 어음번호, 21~26 은행코드 */
            //if (sA1CDMI == "09")
            //{
            //    sCall_Code = string.Format("{0,20:G}", sCall_Code);
            //    if (SetDefaultValue(this.txtfsA1CDMI1) == "02") sCall_Code = sCall_Code + txtB2VLMI1.Text.Substring(0, 6);
            //    if (SetDefaultValue(this.txtfsA1CDMI2) == "02") sCall_Code = sCall_Code + txtB2VLMI2.Text.Substring(0, 6);
            //    if (SetDefaultValue(this.txtfsA1CDMI3) == "02") sCall_Code = sCall_Code + txtB2VLMI3.Text.Substring(0, 6);
            //    if (SetDefaultValue(this.txtfsA1CDMI4) == "02") sCall_Code = sCall_Code + txtB2VLMI4.Text.Substring(0, 6);
            //    if (SetDefaultValue(this.txtfsA1CDMI5) == "02") sCall_Code = sCall_Code + txtB2VLMI5.Text.Substring(0, 6);
            //    if (SetDefaultValue(this.txtfsA1CDMI6) == "02") sCall_Code = sCall_Code + txtB2VLMI6.Text.Substring(0, 6);
            //}
            ////<<  Check Function Call >> 
            //sReturnValue = UP_CommonCheck(sCall_Index, sCall_Code, myConnection);

            ///* SILO 판매원가 계정 관항5 체크 BL번호 체크 함수-2006.02.25 임경화 */
            //if (sB2CDAC.Trim().Substring(0, 4) == "4513" &&
            //    sA1CDMI.Trim() == "44" &&
            //    sB2DPAC.Trim().Substring(0, 1) == "S")
            //{
            //    if (sVLMI.Trim() != "")
            //    {
            //        if (txtB2VLMI3.Text.Trim().Length == 19)
            //        {
            //            //내수판매
            //            if (txtB2VLMI3.Text.Trim().Substring(0, 3) == "510")
            //            {
            //                gsMySql = "";
            //                gsMySql = "SELECT YMFBLNO AS IPBLNO ";
            //                gsMySql = gsMySql + "FROM TYFILELIB.SDFOMMTF ";
            //                gsMySql = gsMySql + " WHERE YMFGUBN = '" + txtB2VLMI3.Text.Trim().Substring(0, 3) + "'";
            //                gsMySql = gsMySql + " AND YMFSABN = '" + txtB2VLMI3.Text.Trim().Substring(4, 6) + "'";
            //                gsMySql = gsMySql + " AND YMFYYNO = '" + txtB2VLMI3.Text.Trim().Substring(11, 4) + "'";
            //                gsMySql = gsMySql + " AND YMFSQNO = " + txtB2VLMI3.Text.Trim().Substring(16, 3) + " ";
            //                gsMySql = gsMySql + " AND YMFBLNO = '" + sCall_Code.Trim() + "' ";
            //            }
            //            else
            //            {
            //                gsMySql = "";
            //                gsMySql = "SELECT IPBLNO ";
            //                gsMySql = gsMySql + "FROM TYFILELIB.SDPUMNF ";
            //                gsMySql = gsMySql + " WHERE IPBLGUBN = '" + txtB2VLMI3.Text.Trim().Substring(0, 3) + "'";
            //                gsMySql = gsMySql + " AND IPBLSABN = '" + txtB2VLMI3.Text.Trim().Substring(4, 6) + "'";
            //                gsMySql = gsMySql + " AND IPBLYYNO = '" + txtB2VLMI3.Text.Trim().Substring(11, 4) + "'";
            //                gsMySql = gsMySql + " AND IPBLSQNO = " + txtB2VLMI3.Text.Trim().Substring(16, 3) + " ";
            //                gsMySql = gsMySql + " AND IPBLNO = '" + sCall_Code.Trim() + "' ";

            //            }
            //            MyReader = oInquery.rdQuery(gsMySql, myConnection);
            //            if (MyReader.Read())
            //            {
            //                sReturnValue = MyReader["IPBLNO"].ToString();
            //            }
            //            else
            //            {
            //                MyReader.Close();
            //                sReturnValue = "등록된 BL이 아닙니다";
            //                fbRtnFlag = false;
            //                return sReturnValue;
            //            }
            //            MyReader.Close();
            //        }
            //        else
            //        {
            //            sReturnValue = "파일번호를 입력하세요!";
            //            fbRtnFlag = false;
            //            return sReturnValue;
            //        }
            //    }
            //    else
            //    {
            //        if (txtB2VLMI3.Text.Trim().Length == 19)
            //        {
            //            if (txtB2VLMI3.Text.Trim().Substring(0, 1) != "8")
            //            {
            //                sReturnValue = "BL 번호를 입력하세요!";
            //                fbRtnFlag = false;
            //                return sReturnValue;
            //            }
            //        }
            //    }
            //}

            ///* 무역 판매원가 계정 관항5 체크 BL번호 체크 함수-2006.12.22 임경화 */
            //if (sB2CDAC.Trim().Substring(0, 4) == "4343" &&
            //    sA1CDMI.Trim() == "44" &&
            //    sB2DPAC.Trim().Substring(0, 1) == "B")
            //{
            //    if (sVLMI.Trim() != "")
            //    {
            //        if (txtB2VLMI3.Text.Trim().Length == 19)
            //        {
            //            if (txtB2VLMI3.Text.Trim().Substring(0, 3) == "110" || txtB2VLMI3.Text.Trim().Substring(0, 3) == "120" ||
            //                txtB2VLMI3.Text.Trim().Substring(0, 3) == "130" || txtB2VLMI3.Text.Trim().Substring(0, 3) == "140")
            //            {
            //                gsMySql = "";
            //                gsMySql = "SELECT TGBLNO AS IPBLNO ";
            //                gsMySql = gsMySql + "FROM TYFILELIB.TDMEJGMF ";
            //                gsMySql = gsMySql + "WHERE TGGUBN = '" + txtB2VLMI3.Text.Substring(0, 3) + "' ";
            //                gsMySql = gsMySql + "  AND TGSABN = '" + txtB2VLMI3.Text.Substring(4, 6) + "' ";
            //                gsMySql = gsMySql + "  AND TGYYNO = '" + txtB2VLMI3.Text.Substring(11, 4) + "' ";
            //                gsMySql = gsMySql + "  AND TGSQNO = " + txtB2VLMI3.Text.Substring(16, 3) + " ";
            //                gsMySql = gsMySql + "  AND TGBLNO = '" + sCall_Code.Trim() + "' ";
            //            }
            //            else if (txtB2VLMI3.Text.Trim().Substring(0, 3) == "610")
            //            {
            //                gsMySql = "";
            //                gsMySql = "SELECT YDMBLNO AS IPBLNO ";
            //                gsMySql = gsMySql + "FROM TYFILELIB.TDFOMMTF ";
            //                gsMySql = gsMySql + "WHERE YDMGUBN = '" + txtB2VLMI3.Text.Substring(0, 3) + "' ";
            //                gsMySql = gsMySql + "  AND YDMSABN = '" + txtB2VLMI3.Text.Substring(4, 6) + "' ";
            //                gsMySql = gsMySql + "  AND YDMYYNO = '" + txtB2VLMI3.Text.Substring(11, 4) + "' ";
            //                gsMySql = gsMySql + "  AND YDMSQNO = " + txtB2VLMI3.Text.Substring(16, 3) + " ";
            //                gsMySql = gsMySql + "  AND YDMBLNO = '" + sCall_Code.Trim() + "' ";

            //            }
            //            MyReader = oInquery.rdQuery(gsMySql, myConnection);
            //            if (MyReader.Read())
            //            {
            //                sReturnValue = MyReader["IPBLNO"].ToString();
            //            }
            //            else
            //            {
            //                MyReader.Close();
            //                sReturnValue = "등록된 BL이 아닙니다";
            //                fbRtnFlag = false;
            //                return sReturnValue;
            //            }
            //            MyReader.Close();
            //        }
            //        else
            //        {
            //            sReturnValue = "파일번호를 입력하세요!";
            //            fbRtnFlag = false;
            //            return sReturnValue;
            //        }
            //    }
            //    else
            //    {
            //        if (txtB2VLMI3.Text.Trim().Length == 19)
            //        {
            //            if (txtB2VLMI3.Text.Trim().Substring(0, 1) != "3")
            //            {
            //                sReturnValue = "BL 번호를 입력하세요!";
            //                fbRtnFlag = false;
            //                return sReturnValue;
            //            }
            //        }
            //    }
            //}


            ///* sA1OTMI : 관리항목 OPTION(" ", "C", "D") */
            //if (((sA1CDMI == "27") || (sA1CDMI == "41"))
            //      ||
            //    (((sA1OTMI == "O") && (sVLMI == ""))
            //      ||
            //    ((sA1OTMI == "D") && (Convert.ToDouble(Get_Numeric(txtB2AMDR.Text.Trim())) == 0))
            //      ||
            //    ((sA1OTMI == "C") && (Convert.ToDouble(Get_Numeric(txtB2AMCR.Text.Trim())) == 0)))
            //    )
            //{
            //    fbRtnFlag = true;
            //    return sReturnValue;
            //}



            //if (fbRtnFlag == false)
            //{
            //    return sReturnValue;
            //}

            ///*  당좌예금 대변 지급어음 온 경우 지급어음 번호 확인 */
            //if (sB2CDAC == "11100301" &&
            //    Convert.ToDouble(Get_Numeric(txtB2AMCR.Text.Trim())) != 0 &&
            //    sA1CDMI == "09" && sVLMI != "")
            //{

            //    gsMySql = "";
            //    gsMySql = "SELECT F5KDNC, F5IDUS FROM TYFILELIB.ANTCKMF ";
            //    gsMySql = gsMySql + "WHERE F5NONC = '" + sVLMI + "' ";
            //    MyReader = oInquery.rdQuery(gsMySql, myConnection);
            //    while (MyReader.Read())
            //    {
            //        if (MyReader["F5KDNC"].ToString() != "2" && MyReader["F5IDUS"].ToString() != "")
            //        {
            //            fbRtnFlag = false;
            //            sReturnValue = "어음수표용지의 종류와 사용구분을 확인하세요.";
            //            MyReader.Close();
            //            return sReturnValue;
            //        }
            //        iCount = iCount + 1;
            //    }
            //    MyReader.Close();
            //    if (iCount == 0)
            //    {
            //        fbRtnFlag = false;
            //        sReturnValue = "미등록된 어음 수표 용지입니다";
            //        return sReturnValue;
            //    }
            //}

            //if (sB2CDAC == "21100201" &&
            //    Convert.ToDouble(Get_Numeric(txtB2AMDR.Text.Trim())) != 0 &&
            //    sA1CDMI == "09")
            //{
            //    gsMySql = "";
            //    gsMySql = "SELECT F5KDNC, F5IDUS FROM ANTCKMF ";
            //    gsMySql = gsMySql + "WHERE F5NONC = '" + sVLMI + "' ";
            //    MyReader = oInquery.rdQuery(gsMySql, myConnection);
            //    while (MyReader.Read())
            //    {
            //        if (MyReader["F5KDNC"].ToString() != "1")
            //        {
            //            fbRtnFlag = false;
            //            sReturnValue = "어음수표용지의 종류와 사용구분을 확인하세요.";
            //            MyReader.Close();
            //            return sReturnValue;
            //        }
            //        iCount = iCount + 1;
            //    }
            //    MyReader.Close();
            //    if (iCount == 0)
            //    {
            //        fbRtnFlag = false;
            //        sReturnValue = "미등록된 어음 수표 용지입니다";
            //        return sReturnValue;
            //    }
            //}

            ///* 받을어음의 경우 받을어음 마스타에 등록된 계정과목 체크 
            //   E6GUBUN: 1 - 11100600 -> 받을어음
            //   E6GUBUN: 2 - 11102000 -> 전자받을어음  */
            //if (sB2CDAC == "11100600" || sB2CDAC == "11102000")
            //{
            //    gsMySql = "";
            //    gsMySql = "SELECT E6GUBUN FROM TYFILELIB.ASGRRMF ";
            //    gsMySql = gsMySql + "WHERE E6NONR = '" + sVLMI + "' ";
            //    MyReader = oInquery.rdQuery(gsMySql, myConnection);
            //    while (MyReader.Read())
            //    {
            //        string sE6GUBUN = "";
            //        sE6GUBUN = MyReader["E6GUBUN"].ToString();

            //        if (sE6GUBUN == "1") //받을어음
            //        {
            //            if (sB2CDAC != "11100600")
            //            {
            //                fbRtnFlag = false;
            //                sReturnValue = "받을어음 등록관리에  받을/전자어음 구분을 확인하세요!";
            //                MyReader.Close();
            //                return sReturnValue;
            //            }
            //        }
            //        else if (sE6GUBUN == "2")  //전자받을어음
            //        {
            //            if (sB2CDAC != "11102000")
            //            {
            //                fbRtnFlag = false;
            //                sReturnValue = "받을어음 등록관리에  받을/전자어음 구분을 확인하세요!";
            //                MyReader.Close();
            //                return sReturnValue;
            //            }
            //        }
            //    }
            //    MyReader.Close();

            //}


            ///* 2012.03.20 은행코드에 따른 계좌번호 체크 추가 */

            //string sE3NOAC = string.Empty;
            //string sE3CDBK = string.Empty;

            //if (sA1CDMI == "07" && sVLMI != "")
            //{
            //    if (this.txtfsA1CDMI1 == "02")
            //    {
            //        sE3CDBK = SetDefaultValue(txtB2VLMI1.Text.Trim());
            //    }
            //    if (this.txtfsA1CDMI2 == "02")
            //    {
            //        sE3CDBK = SetDefaultValue(txtB2VLMI2.Text.Trim());
            //    }
            //    if (this.txtfsA1CDMI3 == "02")
            //    {
            //        sE3CDBK = SetDefaultValue(txtB2VLMI3.Text.Trim());
            //    }
            //    if (this.txtfsA1CDMI4 == "02")
            //    {
            //        sE3CDBK = SetDefaultValue(txtB2VLMI4.Text.Trim());
            //    }
            //    if (this.txtfsA1CDMI5 == "02")
            //    {
            //        sE3CDBK = SetDefaultValue(txtB2VLMI5.Text.Trim());
            //    }
            //    if (this.txtfsA1CDMI6 == "02")
            //    {
            //        sE3CDBK = SetDefaultValue(txtB2VLMI6.Text.Trim());
            //    }

            //    iCount = 0;

            //    gsMySql = "";
            //    gsMySql = "SELECT E3CDBK FROM TYFILELIB.ASAVEMF ";
            //    gsMySql = gsMySql + "WHERE E3NOAC = '" + sVLMI.Trim() + "'";
            //    MyReader = oInquery.rdQuery(gsMySql, myConnection);
            //    while (MyReader.Read())
            //    {
            //        if (MyReader["E3CDBK"].ToString() != sE3CDBK)
            //        {
            //            fbRtnFlag = false;
            //            sReturnValue = "은행에등록된계좌번호가 아닙니다";
            //            MyReader.Close();
            //            return sReturnValue;
            //        }

            //        iCount = iCount + 1;
            //    }
            //    MyReader.Close();
            //    if (iCount == 0)
            //    {
            //        fbRtnFlag = false;
            //        sReturnValue = "미등록된 계좌번호 번호입니다";
            //        return sReturnValue;
            //    }

            //}


            //return sReturnValue;
        //}

        #endregion 
        
        #region Description : 예산계정 구분별 CHECK  ---> UP_FieldCheck_CDAC()
        private string UP_FieldCheck_CDAC(string sCDAC)
        {
            string sValue = "";

            //접대비 체크
            switch (sCDAC.Substring(0, 6))
            {
                case "442120":
                case "424120":
                case "441120":
                case "441220":
                    return "01";
                case "442121":
                case "424121":
                case "441121":
                case "441221":
                    return "02";
                default:
                    sValue = "00";
                    break;
            }
            // 운영비 및 분임 토의비
            switch (sCDAC)
            {
                case "44211110":
                case "42411110":
                case "44111110":
                case "44121110":
                    return "03";
                case "44212903":
                case "42412903":
                case "44112903":
                case "44122903":
                    return "04";
                default:
                    sValue = "00";
                    break;
            }

            //기타 세목
            switch (sCDAC)
            {
                case "12200100":
                case "12200200":
                case "12200300":
                case "12200400":
                case "12200500":
                case "12200600":
                case "12200700":
                case "12200800":
                case "12200900":
                case "12210000":
                    return "11";

                case "42411503":
                case "44121503":
                case "44111503":
                case "44211503":
                    return "13";

                case "42412901":
                case "44122901":
                case "44112901":
                case "44212901":
                    return "15";

                case "42412803":
                case "44122803":
                case "44112803":
                case "44212803":
                    return "16";
                default:
                    if (Convert.ToDouble(sCDAC) > 52001500 &&
                        Convert.ToDouble(sCDAC) < 52001599)
                    {
                        sValue = "12";
                    }
                    else if (Convert.ToDouble(sCDAC) > 42411800 &&
                        Convert.ToDouble(sCDAC) < 42411899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44121800 &&
                        Convert.ToDouble(sCDAC) < 44121899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44111800 &&
                        Convert.ToDouble(sCDAC) < 44111899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44211800 &&
                        Convert.ToDouble(sCDAC) < 44211899)
                    {
                        sValue = "14";
                    }
                    if (sValue != "00") return sValue;
                    break;
            }
            //여비교통비
            switch (sCDAC)
            {
                case "42411201":
                case "44121201":
                case "44111201":
                case "44211201":
                    return "21";

                case "42411202":
                case "44121202":
                case "44111202":
                case "44211202":
                    return "22";

            }

            switch (sCDAC)
            {
                case "42413301":
                case "44123301":
                case "44113301":
                case "44213301":
                    return "31";

                // 기타 소모품은 예산 세목이 없음
                //case "42413388":
                //case "44123388":
                //case "44113388":
                //case "44213388":
                //    return "32";

                //case "43332308":
                //case "43432308":
                //case "43532308":
                //case "45132308":
                //case "46132308":
                //case "47132308":
                //case "70032308":
                //case "43632308":
                //    return "33";

                //case "43332310":
                //case "43432310":
                //case "43532310":
                //case "45132310":
                //case "46132310":
                //case "47132310":
                //case "70032310":
                //case "43632310":
                //    return "34";

               
                case "42413306":
                case "44123306":
                case "44113306":
                case "44213306":
                    return "33";
               

            }

            /*  전산관련  --->   35 :소프트웨어개발 ,전산기기판매,소프트웨어개발외상매출 ,전산기기판매외상매출금 ,소프트웨어 매출원가
            *                     ( 41300100 : 41300200 : 11100485 ,11100486 : 42300000 ) */

            switch (sCDAC)
            {
                case "41300100":
                case "41300200":
                case "11100485":
                case "11100486":
                case "42300000":
                    return "35";
            }
            return sValue;
        } 
        #endregion

        #region Description : 년 예산 파일 존재 체크  ---> UP_APPRMMF_Check()
        private bool UP_APPRMMF_Check(string sMMYEAR, string sMMCDDP, string sMMCDAC, string sAMDR, string sAMCR, string sCHK)
        {
            int iCount = 0;
            bool bRtnFalg = true;

            //년 예산 파일 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2ANBH793", sMMYEAR, sMMCDDP, sMMCDAC); // APPRMMF
            iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCount == 0)
            {
                bRtnFalg = false;
            }
            return bRtnFalg;

        }

        private bool UP_APPRMMF_Check_YYMM(string sMMYEAR, string sMMMONTH, string sMMCDDP, string sMMCDAC, string sAMDR, string sAMCR, string sCHK)
        {
            int iCount = 0;
            bool bRtnFalg = true;

            //년 예산 파일 존재 체크 (해당월 존재 체크)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31L38837", sMMYEAR, sMMMONTH, sMMCDDP, sMMCDAC); // APPRMMF
            iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCount == 0)
            {
                bRtnFalg = false;
            }
            return bRtnFalg;

        }
        #endregion

        #region Description : 예산 등록 체크 함수 ( 기타예산세목 ,여비교통비 ,소모품비 )  ---> UP_YESAN_Check()
        private bool UP_YESAN_Check(string sB2DTMK, string sB2DPAC, string sB2CDAC,
                            string sB2VLMI1, string sB2VLMI2, string sB2VLMI3,
                            string sB2VLMI4, string sB2VLMI5, string sB2VLMI6,
                            string sB2AMDR, string sB2AMCR, string sReturnValue)
        {
            bool bTrueAndFalse = true;
            string sWK_APPRCD = "";

            sWK_APPRCD = UP_CDMIToVLMI("35", this.txtfsA1CDMI1, this.txtfsA1CDMI2, this.txtfsA1CDMI3, this.txtfsA1CDMI4, this.txtfsA1CDMI5, this.txtfsA1CDMI6,
                                       sB2VLMI1, sB2VLMI2, sB2VLMI3, sB2VLMI4, sB2VLMI5, sB2VLMI6);
            if (sWK_APPRCD != "" && sWK_APPRCD != "false")
            {
                switch (sReturnValue)
                {

                    //1. 기타예산세목
                    case "11":
                    case "12":
                    case "13":
                    case "14":
                    case "15":
                    case "16":
                        bTrueAndFalse = UP_APPSLSF_Check(sWK_APPRCD.Substring(0, 4),
                                                         sWK_APPRCD.Substring(4, 6),
                                                         sB2CDAC, sWK_APPRCD.Substring(10, 3),
                                                         sB2DTMK.Substring(4, 2), sB2AMDR, sB2AMCR, sReturnValue);
                        if (bTrueAndFalse == false)
                        {
                            this.ShowCustomMessage("예산에 등록된 세목이 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }
                        break;
                    //2. 여비교통비　예산세목
                    case "21":
                    case "22":
                        bTrueAndFalse = UP_APPTLSF_Check(sWK_APPRCD.Substring(0, 4),
                                                         sWK_APPRCD.Substring(4, 6),
                                                         sB2CDAC, sWK_APPRCD.Substring(10, 6),
                                                         sWK_APPRCD.Substring(16, 3), sB2DTMK.Substring(4, 2),
                                                         sB2AMDR, sB2AMCR, sReturnValue);
                        if (bTrueAndFalse == false)
                        {
                            this.ShowCustomMessage("예산에 등록된 세목이 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }
                        break;
                    //3. 소모품비　예산세목
                    case "31":
                    case "32":
                    case "33":
                    case "34":
                        bTrueAndFalse = UP_APPJLSF_Check(sWK_APPRCD.Substring(0, 4),
                                                         sWK_APPRCD.Substring(4, 6),
                                                         sB2CDAC, sWK_APPRCD.Substring(10, 3),
                                                         sWK_APPRCD.Substring(13, 3),
                                                         sB2DTMK.Substring(4, 2),
                                                         sB2AMDR, sB2AMCR, sReturnValue);
                        if (bTrueAndFalse == false)
                        {
                            this.ShowCustomMessage("예산에 등록된 세목이 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2DPAC.CodeText);
                            return false;
                        }
                        break;
                }
            }
            else if (sWK_APPRCD != "false" && fbRtnFlag == false)
            {
                this.ShowCustomMessage("예산에 등록된 세목이 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2DPAC.CodeText);
                return false;
            }
            return true;
        } 
        #endregion

        #region Description : 예산세목 존재 체크 (기타예산세목 ,여비교통비 ,소모품비)  ---> UP_APPSLSF_Check()
        // 기타예산세목 조회
        private bool UP_APPSLSF_Check(string sP2YEAR, string sP2CDDP, string sP2CDAC, string sP2SEQ, string sP2MONTH, string sAMDR, string sAMCR, string sCHK)
        {
            int iCount = 0;
            bool bRtnFalg = true;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2ANBX794", sP2YEAR, sP2CDDP, sP2CDAC, sP2SEQ, sP2MONTH);  //APPSLSF
            DataTable dt_appsl = this.DbConnector.ExecuteDataTable();
            if (dt_appsl.Rows.Count > 0)
            {
                iCount = iCount + 1;
            }
            if (iCount == 0)
            {
                bRtnFalg = false;
            }
            return bRtnFalg;
        }
        //여비교통비　예산세목 조회
        private bool UP_APPTLSF_Check(string sT2YEAR, string sT2CDDP, string sT2CDAC, string sT2CDSB, string sT2SEQ, string sT2MONTH, string sAMDR, string sAMCR, string sCHK)
        {
            int iCount = 0;
            bool bRtnFalg = true;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2ANBX795", sT2YEAR, sT2CDDP, sT2CDAC, sT2CDSB, sT2SEQ, sT2MONTH);  //APPTLSF
            DataTable dt_apptl = this.DbConnector.ExecuteDataTable();
            if (dt_apptl.Rows.Count > 0)
            {
                iCount = iCount + 1;
            }
            if (iCount == 0)
            {
                bRtnFalg = false;
            }
            return bRtnFalg;
        }
        // 소모품비　예산세목 조회
        private bool UP_APPJLSF_Check(string sJ2YEAR, string sJ2CDDP, string sJ2CDAC, string sJ2CDJJ, string sJ2SEQ, string sJ2MONTH, string sAMDR, string sAMCR, string sCHK)
        {
            int iCount = 0;
            bool bRtnFalg = true;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2ANBY797", sJ2YEAR, sJ2CDDP, sJ2CDAC, sJ2CDJJ, sJ2SEQ, sJ2MONTH);  //APPJLSF
            DataTable dt_appjl = this.DbConnector.ExecuteDataTable();
            if (dt_appjl.Rows.Count > 0)
            {
                iCount = iCount + 1;
            }
            if (iCount == 0)
            {
                bRtnFalg = false;
            }
            return bRtnFalg;
        }
        
        #endregion


        #region Description : 상대처 처리 거래처 코드 조회 함수  ---> UP_SetRKCU()
        private string UP_SetRKCU(string sGUBN, string sVNCODE)
        {
            string sReSultVNCODE = "";

            if (sGUBN == "01")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2445D438", sVNCODE);  //AVENDMF
                DataTable dt_S = this.DbConnector.ExecuteDataTable();
                if (dt_S.Rows.Count > 0)
                {
                    sReSultVNCODE = StringTransfer(dt_S.Rows[0]["VNSANGHO"].ToString().Trim(),20);
                }
            }
            else if (sGUBN == "02")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_243BQ336", sVNCODE);  //ACODEMF
                    DataTable dt_T = this.DbConnector.ExecuteDataTable();
                    if (dt_T.Rows.Count > 0)
                    {
                        sReSultVNCODE = StringTransfer(dt_T.Rows[0]["CDDESC1"].ToString().Trim(),20);
                    }
                }
            return sReSultVNCODE;
        }
        #endregion

        #region Description : 매출입금 관련 현업 거래처 코드 조회 함수(외상매출금)   --->  UP_Get_VendCode_HWAJU_ABANJMF()
        private string UP_Get_VendCode_HWAJU_ABANJMF(string sWNJPNO)
        {
            string sReSultVNCODE = "";
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AN4D801", sWNJPNO);  //ABANJMF
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sReSultVNCODE = dt.Rows[0]["VNCODE"].ToString().Trim();
            }

            return sReSultVNCODE;
        }
        #endregion


        #region Description : 사용자 지정 이벤트 핸들러함수 (이미지 버튼 처리)   ---> ImgBtnFocusEvent()
        private void ImgBtnFocusEvent()
        {
            //HiddField.Value = "true";
            this.BTN61_INP.Visible = false; // 라인 입력
            this.BTN61_EDIT.Visible = false;// 라인수정
            this.BTN61_REM.Visible = false; // 라인삭제
            this.BTN61_SAV.Visible = false; // 전표발행
            this.BTN61_CANCEL.Visible = false; //전표취소
        }

        private void UP_ImgBtnDisPlay(string sHiddValue, bool bClear, bool bSave, bool bDelete, bool bVisble)
        {
            this.CBO01_B2NOLN.SetReadOnly(false);

            this.BTN61_INP.Visible = bClear; // 라인 입력
            this.BTN61_EDIT.Visible = bSave;// 라인수정
            this.BTN61_REM.Visible = bDelete; // 라인삭제
            this.BTN61_SAV.Visible = bVisble; // 전표발행
            this.BTN61_CANCEL.Visible = bVisble; // 전표삭제

            //this.tabControl1.Visible = bVisble; // 승인완료후 세부내역을 보여주기 위해 사용안함[2013.08.16]
        }

        #endregion

        #region Description : 순번 Combo Setting   ---> UP_ComBoLineClear()
        //전표라인번호 Clear
        private void UP_ComBoLineClear()
        {
            this.CBO01_B2NOLN.Items.Clear();
            this.CBO01_B2NOLN.Items.Add("임시");

            this.CBO01_B2NOLN.Items[this.CBO01_B2NOLN.Items.Count - 1] = "추가";
            this.CBO01_B2NOLN.SelectedIndex = this.CBO01_B2NOLN.Items.Count - 1;

            //this.CBO01_B2NOLN.Items[0] = "추가";
            //this.CBO01_B2NOLN.SelectedIndex = 0;
        }
        
        private void UP_SetCombo(TYComboBox dlist, DataSet ds)
        {
            dlist.Items.Clear();
            dlist.Items.Add("임시");
            dlist.Items[0] = "추가";

            DataSet dsCombo = new DataSet();
            dsCombo = ds;

            if (dsCombo == null ) return;

            for (int i = 0; i < dsCombo.Tables[0].Rows.Count; i++)
            {
                dlist.Items.Add(Set_Fill2(dsCombo.Tables[0].Rows[i].ItemArray[0].ToString()));         // comboBox.Items[i+1].Text
                dlist.Items[i + 1] = dsCombo.Tables[0].Rows[i].ItemArray[0].ToString(); // comboBox.Items[i+1].Value
            }

            
        }
        #endregion


        #region Description : 미승인 등록 키부분 체크   ---> UP_KeyCheck()
        private bool UP_KeyCheck()
        {
            string sRetrunValue = string.Empty;

            //전표구분
            if (Get_Numeric(this.TXT01_B2NOSQ.GetValue().ToString()) == "0")
            {
                if (this.CBO01_B2IDJP.GetValue().ToString().Trim() == "1" && this.CBO01_B2IDJP.GetValue().ToString().Trim() == "2" && this.CBO01_B2IDJP.GetValue().ToString().Trim() == "3")
                {
                    this.ShowCustomMessage("전표구분 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_B2IDJP);
                    return false;
                }
            }

            //사원번호확인
            if (Get_Numeric(this.TXT01_B2NOSQ.GetValue().ToString()) == "0")
            {
                if (this.CBH01_B2HISAB.GetValue().ToString().Trim() == "")
                {
                    this.ShowCustomMessage("사원번호를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_B2HISAB);
                    return false;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
                    this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_B2DTMK.GetValue(),  this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF

                    DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
                    if (dt_sabun.Rows.Count == 0)
                    {
                        this.ShowCustomMessage("사원번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_B2HISAB);
                        return false;
                    }
                    else
                    {
                        sRetrunValue = dt_sabun.Rows[0]["KBBUSEO"].ToString();
                    }

                    if (this.txtJunPyoGubn != "2")
                    {
                        if (this.CBH01_B2DPMK.GetValue().ToString().Trim() != sRetrunValue)
                        {
                            this.ShowCustomMessage("사원번호의 부서코드를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.CBH01_B2HISAB);
                            return false;
                        }
                    }
                }
            }

            return true;

        }
        #endregion


        #region Description : 키부분 포인터 이동시 초기화 처리  -- 코드박스 Enter
        //  Description : 코드박스 Enter (부서)
        private void CBH01_B2DPMK_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                return;
            }
        }

        private void CBH01_B2DPMK_CodeText_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                return;
            }
        }
        //  Description : 날자박스 Enter (일자)
        private void DTP01_B2DTMK_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표,불공제,무역원가)
                return;
            }
        }

        //  Description : 텍스트박스 Enter (순번)
        private void TXT01_B2NOSQ_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)
                return;
            }
        }

        //  Description : 콤보박스 Enter (전표구분)
        private void CBO01_B2IDJP_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)
                return;
            }
        }

        // Description : 사번 처리
        private void CBH01_B2HISAB_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)
                return;
            }
        }
        private void CBH01_2HISAB_CodeText_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)
                return;
            }
        } 
        #endregion


        #region Description : 화면 초기화 & 임시화일 삭제(TMAC1102F,TMAC1102SF,TMAC1102WF, TMAC1151REF, TMAC1102BF) ---> UP_Screen_Initialize()
        private void UP_Screen_Initialize()
        {
            if (this.CBH01_B2DPMK.GetValue().ToString().Trim() != "" && this.DTP01_B2DTMK.GetString().ToString() != "" && this.TXT01_B2NOSQ.GetValue().ToString() != "")
            {
                // 임시화일전체삭제 (TMAC1102F - DELETE )
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB4S685", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString()); // TMAC1102F
                this.DbConnector.ExecuteNonQuery();

                // 접대비 임시화일 전체삭제(TMAC1102SF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB4T687", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1102SF
                this.DbConnector.ExecuteNonQuery();

                //외화관리 임시화일 전체삭제(TMAC1102WF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB4U688", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1102WF
                this.DbConnector.ExecuteNonQuery();

                //입금표 임시화일 전체삭제(TMAC1151REF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B2AM002", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1151REF
                this.DbConnector.ExecuteNonQuery();

                //불공제 임시화일 전체삭제(TMAC1102BF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2BJ5F471", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1102BF
                this.DbConnector.ExecuteNonQuery();

                //LC비용내역 임시 전체삭제(NTDLCCHNF - DELETE) -오라클
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C38P816", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// NTDLCCHNF
                this.DbConnector.ExecuteNonQuery();

            }

            this.PAN10_VLMI1.Initialize();
            this.PAN10_VLMI2.Initialize();
            this.PAN10_VLMI3.Initialize();
            this.PAN10_VLMI4.Initialize();
            this.PAN10_VLMI5.Initialize();
            this.PAN10_VLMI6.Initialize();

            this.TXT01_B2AMDRTOTAL.SetValue("");
            this.TXT01_B2AMCRTOTAL.SetValue("");

            ImgBtnFocusEvent();
            UP_ComBoLineClear(); //라인번호 클리어
            UP_FieldClear();
            UP_FieldLock(true);

            this.FPS91_TY_S_AC_29S1V349.Initialize();

            //BATID번호 부여(SSID) 키부분에 도달 할때 새로운 SSID를 부여 받음
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            this.fsSessionId = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();


            this.SetFocus(this.DTP01_B2DTMK);

            this.HiddenOK = "false";
        }
        #endregion


        #region Description :  항목 코드Help 처리(기타예산세목 ,여비교통비 ,소모품비 예산 , 받을어음조회 및 등록  조회,출장문서번호 , 자산관리번호)
        private void PAN10_VLMI1_Enter(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();
            // 예산관리조회
            if (fsCDMI01 == "35")
                this.CBH10_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBH01_B2CDAC.GetValue().ToString() };
            // 받을어음 ,부도 어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100501" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100502" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12400200")
            {
                string sValue1 = UP_CDMIToVLMI("01", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI01 == "29")
                    this.CBH10_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBO01_B2IDJP.GetValue().ToString(), this.CBH01_B2HISAB.GetValue().ToString(), sValue1, SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) };
            }
            // 지급어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100202" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100203")
            {
                if (fsCDMI01 == "09")
                    this.CBH10_VALUE09.DummyValue = new string[] { fsSessionId, "" };
            }
            // 외화관리
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100307" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100308")
            {
                //string sBankCode = UP_CDMIToVLMI("02", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                //string sGuJaCode = UP_CDMIToVLMI("07", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                string sBankCode = SetDefaultValue(fsVLMI01);
                string sGuJaCode = SetDefaultValue(fsVLMI02);
                if (fsCDMI01 == "41")
                    this.CBH10_VALUE41.DummyValue = new string[] { sBankCode, sGuJaCode };
            }

            // 출장번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211202")
            {
                string sSabun = UP_CDMIToVLMI("05", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI01 == "37")
                    this.CBH10_VALUE37.DummyValue = new string[] { fsSessionId, sSabun };
            }

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                if (fsCDMI01 == "38")
                    this.CBH10_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI01 };
            }

        }      


        private void PAN10_VLMI2_Enter(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();
            // 예산관리조회
            if (fsCDMI02 == "35")
                this.CBH11_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBH01_B2CDAC.GetValue().ToString() };
            // 받을어음,부도어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100501" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100502" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12400200")
            {
                string sValue1 = UP_CDMIToVLMI("01", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI02 == "29")
                    this.CBH11_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBO01_B2IDJP.GetValue().ToString(), this.CBH01_B2HISAB.GetValue().ToString(), sValue1, SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) };
            }
            // 지급어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100202" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100203")
            {
                if (fsCDMI02 == "09")
                    this.CBH11_VALUE09.DummyValue = new string[] { fsSessionId, "" };
            }
            // 외화관리
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100307" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100308")
            {
                //string sBankCode = UP_CDMIToVLMI("02", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                //string sGuJaCode = UP_CDMIToVLMI("07", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                string sBankCode = SetDefaultValue(fsVLMI01);
                string sGuJaCode = SetDefaultValue(fsVLMI02);
                if (fsCDMI02 == "41")
                    this.CBH11_VALUE41.DummyValue = new string[] { sBankCode, sGuJaCode };
            }

            // 출장번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211202")
            {
                string sSabun = UP_CDMIToVLMI("05", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI02 == "37")
                    this.CBH11_VALUE37.DummyValue = new string[] { fsSessionId, sSabun };
            }

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                if (fsCDMI02 == "38")
                    this.CBH11_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI02 };
            }
        }
        private void PAN10_VLMI3_Enter(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();
            // 예산관리조회
            if (fsCDMI03 == "35")
                this.CBH12_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBH01_B2CDAC.GetValue().ToString() };
            // 받을어음,부도어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100501" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100502" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12400200")
            {
                string sValue1 = UP_CDMIToVLMI("01", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI03 == "29")
                    this.CBH12_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBO01_B2IDJP.GetValue().ToString(), this.CBH01_B2HISAB.GetValue().ToString(), sValue1, SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) };
            }
            // 지급어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100202" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100203")
            {
                string sValue1 = UP_CDMIToVLMI("09", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI03 == "09")
                    this.CBH12_VALUE09.DummyValue = new string[] { fsSessionId, sValue1 };
            }
            // 외화관리
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100307" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100308")
            {
                //string sBankCode = UP_CDMIToVLMI("02", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                //string sGuJaCode = UP_CDMIToVLMI("07", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                string sBankCode = SetDefaultValue(fsVLMI01);
                string sGuJaCode = SetDefaultValue(fsVLMI02);
                if (fsCDMI03 == "41")
                    this.CBH12_VALUE41.DummyValue = new string[] { sBankCode, sGuJaCode };
            }

            // 출장번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211202")
            {
                string sSabun = UP_CDMIToVLMI("05", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI03 == "37")
                    this.CBH12_VALUE37.DummyValue = new string[] { fsSessionId, sSabun };
            }

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                if (fsCDMI03 == "38")
                    this.CBH12_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI03 };
            }

        }
        private void PAN10_VLMI4_Enter(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();
            // 예산관리조회
            if (fsCDMI04 == "35")
                this.CBH13_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBH01_B2CDAC.GetValue().ToString() };
            // 받을어음,부도어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100501" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100502" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12400200")
            {
                string sValue1 = UP_CDMIToVLMI("01", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI04 == "29")
                    this.CBH13_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBO01_B2IDJP.GetValue().ToString(), this.CBH01_B2HISAB.GetValue().ToString(), sValue1, SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) };
            }
            // 지급어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100202" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100203")
            {
                if (fsCDMI04 == "09")
                    this.CBH13_VALUE09.DummyValue = new string[] { fsSessionId, "" };
            }
            // 외화관리
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100307" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100308")
            {
                //string sBankCode = UP_CDMIToVLMI("02", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                //string sGuJaCode = UP_CDMIToVLMI("07", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                string sBankCode = SetDefaultValue(fsVLMI01);
                string sGuJaCode = SetDefaultValue(fsVLMI02);
                if (fsCDMI04 == "41")
                    this.CBH13_VALUE41.DummyValue = new string[] { sBankCode, sGuJaCode };
            }

            // 출장번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211202")
            {
                string sSabun = UP_CDMIToVLMI("05", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI04 == "37")
                    this.CBH13_VALUE37.DummyValue = new string[] { fsSessionId, sSabun };
            }

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                if (fsCDMI04 == "38")
                    this.CBH13_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI04 };
            }
        }
        private void PAN10_VLMI5_Enter(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();
            // 예산관리조회
            if (fsCDMI05 == "35")
                this.CBH14_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBH01_B2CDAC.GetValue().ToString() };
            // 받을어음,부도어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100501" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100502" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12400200")
            {
                string sValue1 = UP_CDMIToVLMI("01", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI05 == "29")
                    this.CBH14_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBO01_B2IDJP.GetValue().ToString(), this.CBH01_B2HISAB.GetValue().ToString(), sValue1, SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim())};
            }
            // 지급어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100202" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100203")
            {
                if (fsCDMI05 == "09")
                    this.CBH14_VALUE09.DummyValue = new string[] { fsSessionId, "" };
            }
            // 외화관리
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100307" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100308")
            {
                //string sBankCode = UP_CDMIToVLMI("02", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                //string sGuJaCode = UP_CDMIToVLMI("07", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                string sBankCode = SetDefaultValue(fsVLMI01);
                string sGuJaCode = SetDefaultValue(fsVLMI02);
                if (fsCDMI05 == "41")
                    this.CBH14_VALUE41.DummyValue = new string[] { sBankCode, sGuJaCode };
            }

            // 출장번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211202")
            {
                string sSabun = UP_CDMIToVLMI("05", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI05 == "37")
                    this.CBH14_VALUE37.DummyValue = new string[] { fsSessionId, sSabun };
            }

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                if (fsCDMI05 == "38")
                    this.CBH14_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI05 };
            }
        }
        private void PAN10_VLMI6_Enter(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();
            // 예산관리조회
            if (fsCDMI06 == "35")
                this.CBH15_VALUE35.DummyValue = new string[] { this.DTP01_B2DTMK.GetString().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBH01_B2CDAC.GetValue().ToString() };
            // 받을어음,부도어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100501" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100502" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12400200")
            {
                string sValue1 = UP_CDMIToVLMI("01", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI06 == "29")
                    this.CBH15_VALUE29.DummyValue = new string[] { fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), this.CBH01_B2DPAC.GetValue().ToString(), this.CBO01_B2IDJP.GetValue().ToString(), this.CBH01_B2HISAB.GetValue().ToString(), sValue1, SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) };
            }
            // 지급어음
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100202" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "21100203")
            {
                if (fsCDMI06 == "09")
                    this.CBH15_VALUE09.DummyValue = new string[] { fsSessionId, "" };
            }
            // 외화관리
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100307" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "11100308")
            {
                //string sBankCode = UP_CDMIToVLMI("02", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                //string sGuJaCode = UP_CDMIToVLMI("07", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                string sBankCode = SetDefaultValue(fsVLMI01);
                string sGuJaCode = SetDefaultValue(fsVLMI02);
                if (fsCDMI06 == "41")
                    this.CBH15_VALUE41.DummyValue = new string[] { sBankCode, sGuJaCode };
            }

            // 출장번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "42411202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44111202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44121202" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211201" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "44211202")
            {
                string sSabun = UP_CDMIToVLMI("05", fsCDMI01, fsCDMI02, fsCDMI03, fsCDMI04, fsCDMI05, fsCDMI06, fsVLMI01, fsVLMI02, fsVLMI03, fsVLMI04, fsVLMI05, fsVLMI06);
                if (fsCDMI06 == "37")
                    this.CBH15_VALUE37.DummyValue = new string[] { fsSessionId, sSabun };
            }

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                if (fsCDMI06 == "38")
                    this.CBH15_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI06 };
            }
        } 
        #endregion


        #region Descriotion : TabControl 제어   ---> tabControl1_Enable()
        private void tabControl1_Enable()
        {
            if (fsTabCtl == "RECP") // 접대비
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage1))
                {
                    this.tabControl1.TabPages.Add(this.tabPage1);
                    this.tabControl1.TabPages.Remove(this.tabPage2);
                    this.tabControl1.TabPages.Remove(this.tabPage3);
                    this.tabControl1.TabPages.Remove(this.tabPage4);
                    this.tabControl1.TabPages.Remove(this.tabPage5);
                }
            }
            else if (fsTabCtl == "FORE") //외화
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage2))
                {
                    this.tabControl1.TabPages.Add(this.tabPage2);
                    this.tabControl1.TabPages.Remove(this.tabPage1);
                    this.tabControl1.TabPages.Remove(this.tabPage3);
                    this.tabControl1.TabPages.Remove(this.tabPage4);
                    this.tabControl1.TabPages.Remove(this.tabPage5);
                }
            }
            else if (fsTabCtl == "MONE") //입금표
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage3))
                {
                    this.tabControl1.TabPages.Add(this.tabPage3);
                    this.tabControl1.TabPages.Remove(this.tabPage1);
                    this.tabControl1.TabPages.Remove(this.tabPage2);
                    this.tabControl1.TabPages.Remove(this.tabPage4);
                    this.tabControl1.TabPages.Remove(this.tabPage5);
                }
            }
            else if (fsTabCtl == "TXEX") // 부가세 불공제분 사유등록
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage4))
                {
                    this.tabControl1.TabPages.Add(this.tabPage4);
                    this.tabControl1.TabPages.Remove(this.tabPage1);
                    this.tabControl1.TabPages.Remove(this.tabPage2);
                    this.tabControl1.TabPages.Remove(this.tabPage3);
                    this.tabControl1.TabPages.Remove(this.tabPage5);
                }
            }
            else if (fsTabCtl == "LCCH") // 무역원가 등록
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage5))
                {
                    this.tabControl1.TabPages.Add(this.tabPage5);
                    this.tabControl1.TabPages.Remove(this.tabPage1);
                    this.tabControl1.TabPages.Remove(this.tabPage2);
                    this.tabControl1.TabPages.Remove(this.tabPage3);
                    this.tabControl1.TabPages.Remove(this.tabPage4);
                }
            }
        }

        private void tabControl_Remove()
        {
            this.tabControl1.TabPages.Remove(this.tabPage1);
            this.tabControl1.TabPages.Remove(this.tabPage2);
            this.tabControl1.TabPages.Remove(this.tabPage3);
            this.tabControl1.TabPages.Remove(this.tabPage4);
            this.tabControl1.TabPages.Remove(this.tabPage5);
        } 
        #endregion


        /* --------------------------------------------------------------------------------------------------- */
        /* --------------------------------------   접대비  관리 처리  ---------------------------------------- */
        /* --------------------------------------------------------------------------------------------------- */

        #region  Description : 접대비 번호 체크 (   호  출  부  분  ) - UP_ASERVECheck
        private void UP_ASERVECheck(string sJubNo )
		{
            string sNOLN = this.CBO01_B2NOLN.SelectedItem.ToString();

            if (sJubNo == "")
            {
                // 접대비 번호 생성(미승인 등록)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AO52819", "JD", "", this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 6), ""); // SP 호출 TYSCMLIB.SP_GB_AC_AUTOPFSEQ
                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                if (sOUTMSG.Substring(0, 1) == "E")
                {
                    this.ShowCustomMessage("접대비 번호 생성중 오류 발생(AUTOPF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_B2NOSQ);
                    //e.Successed = false;
                    //return;
                }
                else
                {
                    this.TXT01_TSDTYY.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 6));
                    this.TXT01_TSNOSQ.SetValue(Set_Fill4(sOUTMSG));

                    UP_FieldClear_RECP();

                    this.DTP01_TSDTOC.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim());
                    this.BTN61_INP_RECP.Visible = true;  // 저장
                    this.BTN61_SAV_RECP.Visible = false; // 수정
                }
            }
            else
            {
                this.DbConnector.CommandClear(); //TMAC1102SF
                this.DbConnector.Attach("TY_P_AC_2AB4K677", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), sNOLN);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AO52819", "JD", "", this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 6), ""); // SP 호출 TYSCMLIB.SP_GB_AC_AUTOPFSEQ
                    string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    if (sOUTMSG.Substring(0, 1) != "E")
                    {
                        this.TXT01_TSDTYY.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 6));
                        this.TXT01_TSNOSQ.SetValue(Set_Fill4(sOUTMSG));

                        UP_FieldClear_RECP();

                        this.DTP01_TSDTOC.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim());

                        this.BTN61_INP_RECP.Visible = true;  // 저장
                        this.BTN61_SAV_RECP.Visible = false; // 수정
                    }
                }
                else  //수정 
                {
                    UP_FieldClear_RECP();

                    this.CBH01_TSDEID.SetValue(dt.Rows[0]["TSDEID"].ToString().Trim()); //거래구분
                    this.DTP01_TSDTOC.SetValue(dt.Rows[0]["TSDTOC"].ToString().Trim()); //발생일자
                    this.CBH01_TSNOCL.SetValue(dt.Rows[0]["TSNOCL"].ToString().Trim()); //사업자등록번호
                    this.TXT01_TSIRUM.SetValue(dt.Rows[0]["VNIRUM"].ToString().Trim()); //대표자명 (조회전용)
                    this.TXT01_TSVNCODE.SetValue(dt.Rows[0]["VNCODE"].ToString().Trim()); // 거래처 코드
                    this.TXT01_TSADCL.SetValue(dt.Rows[0]["VNJUSO"].ToString().Trim()); //주소

                    this.CBH01_TSNOCC.SetValue(dt.Rows[0]["TSNOCC"].ToString().Trim()); //카드번호
                    this.TXT01_TSNMRP.SetValue(dt.Rows[0]["A6NMPD"].ToString().Trim()); //대표자명(카드)
                    this.TXT01_TSREMK.SetValue(dt.Rows[0]["TSREMK"].ToString().Trim()); //사용내역
                    this.CBH01_TSNOMK.SetValue(dt.Rows[0]["TSNOMK"].ToString().Trim()); //사원번호
                    this.TXT01_TSAMSE.SetValue(dt.Rows[0]["TSAMSE"].ToString().Trim()); //접대비
                    this.TXT01_TSCGSE.SetValue(dt.Rows[0]["TSCGSE"].ToString().Trim()); //봉사료
                    this.TXT01_TSHAPAMT.SetValue(dt.Rows[0]["SUM"].ToString().Trim());  //합계
                    this.CBO01_TSGUBN.SetValue(dt.Rows[0]["TSGUBN"].ToString().Trim());

                    this.BTN61_INP_RECP.Visible = false; // 저장
                    this.BTN61_SAV_RECP.Visible = true;  // 수정
                }           
            }

            // 승인일때 조회는 가능하나 수정은 불가능 하게 처리 //
            if (this.txtJunPyoGubn == "2")
            {
                this.BTN61_INP_RECP.Visible = false;  // 저장
                this.BTN61_SAV_RECP.Visible = false;  // 수정
                this.BTN61_CLO_RECP.Visible = false; // 닫기
            }

            this.SetFocus(this.CBH01_TSDEID.CodeText);
		}
		#endregion

        #region Description : 접대비 저장 버튼 처리
        private void BTN61_INP_RECP_Click(object sender, EventArgs e)
        {
            string sNOLN = this.CBO01_B2NOLN.SelectedItem.ToString();

            /* 접대비 저장시에 이미저장된 전표번호가 존재 하면 삭제후 등록함 */
            this.DbConnector.CommandClear(); //TMAC1102SF
            this.DbConnector.Attach("TY_P_AC_2AB4K677", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), sNOLN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                // 삭제 처리
                this.DbConnector.CommandClear(); //TMAC1102SF
                this.DbConnector.Attach("TY_P_AC_2AB4M680", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), sNOLN);
                this.DbConnector.ExecuteNonQuery();
            }

            /* 저장 처리 */
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB4L678", this.ControlFactory, "03"); // 저장
            this.DbConnector.ExecuteNonQuery();

            UP_FieldSeting_RECP();

            //버튼 잠금
            this.BTN61_INP_RECP.Visible = false; // 저장
            this.BTN61_SAV_RECP.Visible = false;  // 수정

            this.SetFocus(this.CBH01_B2DPAC.CodeText); //  TXT01_B2RKAC
        } 
        #endregion

        #region Description : 접대비 저장 전 체크 
        private void BTN61_INP_RECP_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //2015.10.05 황성환 요청
            //전표 작성일자와 발생일자는 동일한 월만 가능하다.
            if (this.DTP01_B2DTMK.GetString().ToString().Substring(0, 6) != this.DTP01_TSDTOC.GetString().ToString().Substring(0, 6))
            {
                this.ShowCustomMessage("발생일자는 작성일자와 동일한 월에만 가능합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_TSDTOC);
                e.Successed = false;
                return;
            }

            UP_VendSting_RECP();
            UP_ScreenFile_RECP_Save();

            if (UP_Field_Check_RECP() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 접대비 수정 버튼 처리
        private void BTN61_SAV_RECP_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB4M679", this.ControlFactory, "03"); // 수정
            this.DbConnector.ExecuteNonQuery();

            UP_FieldSeting_RECP();

            //버튼 잠금
            this.BTN61_INP_RECP.Visible = false; // 저장
            this.BTN61_SAV_RECP.Visible = false;  // 수정

            this.SetFocus(this.TXT01_B2RKAC);
        } 
        #endregion

        #region Description : 접대비 수정 전 체크 
        private void BTN61_SAV_RECP_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_VendSting_RECP();
            UP_ScreenFile_RECP_Save();

            if (UP_Field_Check_RECP() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 접대비 닫기 버튼 처리
        private void BTN61_CLO_RECP_Click(object sender, EventArgs e)
        {
            tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)

            //this.tabControl1.Visible = true;
            this.SetFocus(this.TXT01_B2RKAC);
        } 
        #endregion

        #region Description : 접대비 저장_삭제시 미승인전표 관리 항목 세팅
        private void UP_FieldSeting_RECP()
        {
            this.PAN10_VLMI4.SetValue(this.TXT01_TSDTYY.GetValue().ToString().Trim() + Set_Fill4(this.TXT01_TSNOSQ.GetValue().ToString().Trim()));
            string sHAP = Convert.ToString(Convert.ToDecimal(Get_Numeric(this.TXT01_TSAMSE.GetValue().ToString().Trim())) + Convert.ToDecimal(Get_Numeric(this.TXT01_TSCGSE.GetValue().ToString().Trim())));
            this.TXT01_B2AMDR.SetValue(sHAP.Trim());
            this.TXT01_B2RKCU.SetValue(StringTransfer(this.CBH01_TSNOCL.GetText().ToString().Trim(), 20));
            this.TXT01_B2RKAC.SetValue(this.TXT01_TSREMK.GetValue().ToString().Trim());

            if (this.TXT01_TSVNCODE.GetValue().ToString().Trim() != "" && fsCDMI01 == "01" )
            {
                this.PAN10_VLMI1.SetValue(this.TXT01_TSVNCODE.GetValue().ToString().Trim());
            }

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2B19E974", this.CBH01_TSNOCL.GetValue().ToString().Trim());
            //DataTable dt = this.DbConnector.ExecuteDataTable();
            //if (dt.Rows.Count > 0)
            //{
            //    this.PAN10_VLMI1.SetValue(dt.Rows[0]["VNCODE"].ToString().Trim());
            //}

        }
        #endregion

        #region Descriotion : 접대비 (사업자등록 선택시 : 대표자명,주소,코드 처리 ) 키 이벤트 처리
        private void CBH01_TSNOCL_KeyPress(object sender, KeyPressEventArgs e)
        {
            UP_VendSting_RECP();
        }

        private void CBH01_TSNOCL_CodeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            UP_VendSting_RECP();
        } 
        #endregion

        #region Description : 사업자 관련 주소 회사명 처리
        private void UP_VendSting_RECP()
        {
            if ((this.CBH01_TSNOCL.GetValue().ToString().Trim() != "") && (this.CBH01_TSNOCL.GetValue().ToString().Trim().Length == 10))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B19E974", this.CBH01_TSNOCL.GetValue().ToString().Trim());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.TXT01_TSVNCODE.SetValue(dt.Rows[0]["VNCODE"].ToString().Trim()); // 거래처 코드
                    this.TXT01_TSIRUM.SetValue(dt.Rows[0]["VNIRUM"].ToString().Trim()); //대표자명 (조회전용)
                    this.TXT01_TSADCL.SetValue(dt.Rows[0]["VNJUSO"].ToString().Trim()); //주소
                }
            }
        }
        #endregion
        
        #region Description : 접대비 Field Check 메소드
        private bool UP_Field_Check_RECP()
        {
            if (this.TXT01_TSDTYY.GetValue().ToString().Trim().Length != 6)
            {
                this.ShowCustomMessage("접대비 번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_TSDEID.CodeText);
                return false;
            }

            if (this.TXT01_TSNOSQ.GetValue().ToString().Trim().Length != 4)
            {
                this.ShowCustomMessage("접대비 번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_TSDEID.CodeText);
                return false;
            }

            if (this.CBH01_TSDEID.GetValue().ToString().Trim() == "13")
            {
                this.ShowCustomMessage("사용할수 없는 코드입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_TSDEID.CodeText);
                return false;

            }

            if (this.CBH01_TSDEID.GetValue().ToString().Trim() != "31" && this.CBH01_TSDEID.GetValue().ToString().Trim() != "32")
            {
                if (this.CBH01_TSNOCL.GetValue().ToString().Trim() == "")
                {
                    this.ShowCustomMessage("사용할수 없는 코드입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_TSNOCL.CodeText);
                    return false;
                }
            }

            // 카드 사용시 발생일 등록 체크
            if (this.CBH01_TSDEID.GetValue().ToString().Trim() == "11" || this.CBH01_TSDEID.GetValue().ToString().Trim() == "21")
            {
                if (this.DTP01_TSDTOC.GetString().ToString().Trim() == "")  // DTP01_TSDTOC
                {
                    this.ShowCustomMessage("발생일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.DTP01_TSDTOC);
                    return false;
                }

            }

            //사업자번호
            if (this.CBH01_TSDEID.GetValue().ToString().Trim() != "31" && this.CBH01_TSDEID.GetValue().ToString().Trim() != "32")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B19E974", this.CBH01_TSNOCL.GetValue().ToString().Trim());  // AVENDMF
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    //this.CBH01_TSNOCL.SetValue(dt.Rows[0]["VNSAUPNO"].ToString().Trim()); // 사업자등록 번호
                    this.TXT01_TSVNCODE.SetValue(dt.Rows[0]["VNCODE"].ToString().Trim()); // 거래처 코드
                    this.TXT01_TSIRUM.SetValue(dt.Rows[0]["VNIRUM"].ToString().Trim()); //대표자명 (조회전용)
                    this.TXT01_TSADCL.SetValue(dt.Rows[0]["VNJUSO"].ToString().Trim()); //주소
                }
                else
                {
                    this.ShowCustomMessage("사업자번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_TSNOCL.CodeText);
                    return false;
                }
            }

            //법인카드만 사용 가능
            if (this.CBH01_TSDEID.GetValue().ToString().Trim() == "11" || this.CBH01_TSDEID.GetValue().ToString().Trim() == "21")
            {
                if (this.CBH01_TSNOCC.GetValue().ToString().Trim() == "")
                {
                    this.ShowCustomMessage("신용카드번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_TSNOCC.CodeText);
                    return false;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2443B420", this.CBH01_TSNOCC.GetValue().ToString().Trim());  // ACRDCDF
                    DataTable dt_CRDC = this.DbConnector.ExecuteDataTable();
                    if (dt_CRDC.Rows.Count > 0)
                    {
                        //this.CBH01_TSNOCC.SetValue(dt_CRDC.Rows[0]["A6CRDT"].ToString().Trim()); //카드번호
                        this.TXT01_TSNMRP.SetValue(dt_CRDC.Rows[0]["A6NMPD"].ToString().Trim()); //대표자명

                        //if (dt_CRDC.Rows[0]["A6NMPD"].ToString().Trim() != "태영화학" && dt_CRDC.Rows[0]["A6NMPD"].ToString().Trim() != "태영인더")
                        //{
                        //    this.ShowCustomMessage("신용카드번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        //    this.SetFocus(this.CBH01_TSNOCC.CodeText);
                        //    return false;
                        //}
                    }
                    else
                    {
                        this.ShowCustomMessage("신용카드번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_TSNOCC.CodeText);
                        return false;
                    }
                }
            }
            else
            {
                this.CBH01_TSNOCC.SetValue("");
                this.TXT01_TSNMRP.SetValue("");
            }

            if (this.CBH01_TSDEID.GetValue().ToString().Trim() == "")
            {
                this.ShowCustomMessage("거래구분 코드를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_TSDEID.CodeText);
                return false;
            }

            //사원번호
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_TSNOMK.GetValue().ToString().Trim());  //INKIBNMF
            this.DbConnector.Attach("TY_P_GB_4CVJ7024",this.DTP01_TSDTOC.GetString(), this.CBH01_TSNOMK.GetValue().ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count == 0)
            {
                this.ShowCustomMessage("사원번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_TSNOMK.CodeText);
                return false;
            }

            if ((this.CBO01_TSGUBN.GetValue().ToString().Trim() != "1") && (this.CBO01_TSGUBN.GetValue().ToString().Trim() != "2"))
            {
                this.ShowCustomMessage("접대비 구분을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBO01_TSGUBN);
                return false;
            }
            //접대비 합계금액
            this.TXT01_TSHAPAMT.SetValue(Get_Numeric(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_TSAMSE.GetValue().ToString().Trim())) +
                                                                       Convert.ToDouble(Get_Numeric(this.TXT01_TSCGSE.GetValue().ToString().Trim())))));

            if ( Get_Numeric(this.TXT01_TSHAPAMT.GetValue().ToString().Trim()) == "0")
            {
                this.ShowCustomMessage("접대비 금액을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TSAMSE);
                return false;
            }

            return true;
        }
        #endregion

        #region Description : 접대비 필드 CLEAR
        private void UP_FieldClear_RECP()
        {
            this.CBH01_TSDEID.SetValue(""); //거래구분
            this.DTP01_TSDTOC.SetValue(""); //발생일자
            this.CBH01_TSNOCL.SetValue(""); //사업자등록번호
            this.TXT01_TSVNCODE.SetValue(""); //거래처 코드
            this.TXT01_TSIRUM.SetValue(""); //대표자명 (조회전용)
            this.TXT01_TSADCL.SetValue(""); //주소
            this.CBH01_TSNOCC.SetValue(""); //카드번호
            this.TXT01_TSNMRP.SetValue(""); // 대표자명(카드)
            this.TXT01_TSREMK.SetValue(""); //사용내역
            this.CBH01_TSNOMK.SetValue(""); //사원번호
            this.TXT01_TSAMSE.SetValue(""); //접대비
            this.TXT01_TSCGSE.SetValue(""); //봉사료
            this.TXT01_TSHAPAMT.SetValue(""); //합계
            this.CBO01_TSGUBN.SetValue("");
        }
        #endregion

        #region Description : 접대비 화면값 --> 저장변수 담기
        private void UP_ScreenFile_RECP_Save()
        {
            string sJPNO = this.CBH01_B2DPMK.GetValue().ToString() + this.DTP01_B2DTMK.GetString().ToString() + Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString());

            this.DAT03_TSJPSSID.SetValue(fsSessionId);
            this.DAT03_TSJPDPMK.SetValue(this.CBH01_B2DPMK.GetValue().ToString());
            this.DAT03_TSJPDTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString());
            this.DAT03_TSJPNO_SQ.SetValue(this.TXT01_B2NOSQ.GetValue().ToString());
            this.DAT03_TSJPNO_LN.SetValue(this.CBO01_B2NOLN.SelectedItem.ToString());

            this.DAT03_TSDTYY.SetValue(this.TXT01_TSDTYY.GetValue().ToString().Trim().Substring(0, 4));
            this.DAT03_TSDTMM.SetValue(this.TXT01_TSDTYY.GetValue().ToString().Trim().Substring(4, 2));
            this.DAT03_TSNOSQ.SetValue(this.TXT01_TSNOSQ.GetValue().ToString().Trim());
            this.DAT03_TSDTOC.SetValue(this.DTP01_TSDTOC.GetValue().ToString().Trim());
            this.DAT03_TSDEID.SetValue(this.CBH01_TSDEID.GetValue().ToString().Trim());
            this.DAT03_TSNOCL.SetValue(this.CBH01_TSNOCL.GetValue().ToString().Trim());
            this.DAT03_TSNMCP.SetValue(this.CBH01_TSNOCL.GetText().ToString().Trim());

            this.DAT03_TSADCL.SetValue(this.TXT01_TSADCL.GetValue().ToString().Trim());
            this.DAT03_TSNMRP.SetValue(this.TXT01_TSNMRP.GetValue().ToString().Trim());
            this.DAT03_TSNOCC.SetValue(this.CBH01_TSNOCC.GetValue().ToString().Trim());
            this.DAT03_TSREMK.SetValue(this.TXT01_TSREMK.GetValue().ToString().Trim());
            this.DAT03_TSNOMK.SetValue(this.CBH01_TSNOMK.GetValue().ToString().Trim());
            this.DAT03_TSAMSE.SetValue(this.TXT01_TSAMSE.GetValue().ToString().Trim());
            this.DAT03_TSCGSE.SetValue(this.TXT01_TSCGSE.GetValue().ToString().Trim());
            this.DAT03_TSJPNO_1.SetValue(sJPNO);
            this.DAT03_TSGUBN.SetValue(this.CBO01_TSGUBN.GetValue().ToString().Trim());
        }
        #endregion


        /* --------------------------------------------------------------------------------------------------- */
        /* --------------------------------------    외화 관리 처리    ---------------------------------------- */
        /* --------------------------------------------------------------------------------------------------- */

        #region Description : 외화관리 번호 체크 (   호  출  부  분  ) - UP_TMAC1102WF_Check
        private void UP_TMAC1102WF_Check(string sYYMM)
        {
            string sNOLN = this.CBO01_B2NOLN.SelectedItem.ToString();

            this.CBH01_TWBANK.OnCodeBoxDataBinded(null, null); 

            this.DbConnector.CommandClear();  // TMAC1102WF
            this.DbConnector.Attach("TY_P_AC_2AB4N681", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), sNOLN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B13T992", this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 4), ""); // SP 호출 TYSCMLIB.SP_GB_AC_HWANFSEQ
                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                if (sOUTMSG.Substring(0, 1) != "E")
                {
                    UP_FieldClear_FORE();

                    this.TXT01_TWYEAR.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 4));
                    this.TXT01_TWNOSQ.SetValue(Set_Fill4(sOUTMSG));


                    this.BTN61_INP_FORE.Visible = true;  // 저장
                    this.BTN61_SAV_FORE.Visible = false; // 수정
                }
            }
            else
            {
                UP_FieldClear_FORE();

                this.TXT01_TWYEAR.SetValue(dt.Rows[0]["TWYEAR"].ToString().Trim());             // 외화관리번호(년)
                this.TXT01_TWNOSQ.SetValue(Set_Fill4(dt.Rows[0]["TWNOSQ"].ToString().Trim()));  // 외화관리번호(순번)
                this.CBH01_TWBANK.SetValue(dt.Rows[0]["TWBANK"].ToString().Trim());             // 은행
                this.CBH01_TWGUJA.DummyValue = dt.Rows[0]["TWBANK"].ToString().Trim();
                this.CBH01_TWGUJA.SetValue(dt.Rows[0]["TWGUJA"].ToString().Trim());             // 계좌번호
                this.CBO01_TWGUIP.SetValue(dt.Rows[0]["TWGUIP"].ToString().Trim());             // 구입방법
                this.CBH01_TWGUCD.SetValue(dt.Rows[0]["TWGUCD"].ToString().Trim());             // 구입업체
                this.CBO01_TWGONG.SetValue(dt.Rows[0]["TWGONG"].ToString().Trim());             // 사업장
                this.CBH01_TWGUBN.SetValue(dt.Rows[0]["TWGUBN"].ToString().Trim());             // 외화구분
                this.TXT01_TWYUL.SetValue(dt.Rows[0]["TWYUL"].ToString().Trim());               // 입금환율
                this.TXT01_TWIAMT.SetValue(dt.Rows[0]["TWIAMT"].ToString().Trim());             // 설정금액

                this.BTN61_INP_FORE.Visible = false;  // 저장
                this.BTN61_SAV_FORE.Visible = true;   // 수정
            };

            // 승인일때 조회는 가능하나 수정은 불가능 하게 처리 //
            if (this.txtJunPyoGubn == "2")
            {
                this.BTN61_INP_FORE.Visible = false;  // 저장
                this.BTN61_SAV_FORE.Visible = false;  // 수정
                this.BTN61_CLO_FORE.Visible = false;  // 닫기
            }

            this.SetFocus(this.CBH01_TWBANK.CodeText);
        }
        #endregion

        #region Description : 외화관리 저장 버튼 처리
        private void BTN61_INP_FORE_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB4N682", this.ControlFactory, "04"); // 저장
            this.DbConnector.ExecuteNonQuery();

            UP_FieldSeting_FORE();

            ////버튼 잠금
            this.BTN61_INP_FORE.Visible = false; // 저장
            this.BTN61_SAV_FORE.Visible = false;  // 수정

            this.SetFocus(this.TXT01_B2RKAC);
        } 
        #endregion

        #region Description : 외화관리 저장 전 체크 
        private void BTN61_INP_FORE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_ScreenFile_FORE_Save();

            if (UP_Field_Check_FORE() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 외화관리 수정 버튼 처리  
        private void BTN61_SAV_FORE_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB4N683", this.ControlFactory, "04"); // 수정
            this.DbConnector.ExecuteNonQuery();

            UP_FieldSeting_FORE();

            ////버튼 잠금
            this.BTN61_INP_FORE.Visible = false; // 저장
            this.BTN61_SAV_FORE.Visible = false;  // 수정

            this.SetFocus(this.TXT01_B2RKAC);
        } 
        #endregion

        #region Description : 외화관리 저장_삭제시 미승인전표 관리 항목 세팅
        private void UP_FieldSeting_FORE()
        {
            this.PAN10_VLMI1.SetValue(this.CBH01_TWBANK.GetValue().ToString().Trim());
            this.PAN10_VLMI2.SetValue(this.CBH01_TWGUJA.GetValue().ToString().Trim());
            this.PAN10_VLMI3.SetValue(this.CBH01_TWGUBN.GetValue().ToString().Trim());
            this.PAN10_VLMI4.SetValue(UP_DotDelete2(string.Format("{0:#,##0.00}", this.TXT01_TWIAMT.GetValue().ToString().Trim())));
            this.PAN10_VLMI5.SetValue(UP_DotDelete2(string.Format("{0:#,##0.00}", this.TXT01_TWYUL.GetValue().ToString().Trim())));
            // 외화 코드박스 처리를 위한것
            this.CBH15_VALUE41.DummyValue = new string[] { fsSessionId, this.CBH01_TWBANK.GetValue().ToString().Trim(), this.CBH01_TWGUJA.GetValue().ToString().Trim(), this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim(), "1" };
            this.PAN10_VLMI6.SetValue(this.TXT01_TWYEAR.GetValue().ToString().Trim() + Set_Fill4(this.TXT01_TWNOSQ.GetValue().ToString().Trim()));

            if (this.CBH01_TWGUBN.GetValue().ToString().Trim() == "JPY")
            {
                this.TXT01_B2AMDR.SetValue( UP_DotDelete( Convert.ToString(Convert.ToDouble(Convert.ToDouble(Get_Numeric(this.TXT01_TWIAMT.GetValue().ToString().Trim())) / 100) *
                                              Convert.ToDouble(Get_Numeric(this.TXT01_TWYUL.GetValue().ToString().Trim()))) ) );
            }
            else
            {
               this.TXT01_B2AMDR.SetValue( UP_DotDelete( Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_TWIAMT.GetValue().ToString().Trim())) *
                                              Convert.ToDouble(Get_Numeric(this.TXT01_TWYUL.GetValue().ToString().Trim()))) ) );
            }
        }
        #endregion

        #region Description : 외화관리 수정 전 체크
        private void BTN61_SAV_FORE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_ScreenFile_FORE_Save();

            if (UP_Field_Check_FORE() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 외화관리 닫기 버튼 처리
        private void BTN61_CLO_FORE_Click(object sender, EventArgs e)
        {
            tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)

            //this.tabControl1.Visible = true;
            this.SetFocus(this.TXT01_B2RKAC);
        } 
        #endregion

        #region Description : 외화관리 Field Check 메소드
        private bool UP_Field_Check_FORE()
        {

            //구입방법
            //if (SetDefaultValue(this.CBO01_TWGUIP.GetValue().ToString().Trim()) != "1" && SetDefaultValue(this.CBO01_TWGUIP.GetValue().ToString().Trim()) != "2")
            //{
            //    this.ShowCustomMessage("구입방법를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    this.SetFocus(this.CBO01_TWGUIP);
            //    return false;
            //}

            //거래처확인
            if (this.CBO01_TWGUIP.GetValue().ToString().Trim() == "1")
            {
                if (this.CBH01_TWGUCD.GetValue().ToString().Trim() == "")
                {
                    this.ShowCustomMessage("거래처를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_TWGUCD.CodeText);
                    return false;
                }
            }
            else
            {
                this.CBH01_TWGUCD.SetValue("");
            }

            //사업장확인

            if (this.CBO01_TWGONG.GetValue().ToString().Trim() != "")
            {
                if (SetDefaultValue(this.CBO01_TWGONG.GetValue().ToString().Trim()) != "1" && SetDefaultValue(this.CBO01_TWGONG.GetValue().ToString().Trim()) != "2" &&
                    SetDefaultValue(this.CBO01_TWGONG.GetValue().ToString().Trim()) != "3" && SetDefaultValue(this.CBO01_TWGONG.GetValue().ToString().Trim()) != "4")
                {
                    this.ShowCustomMessage("사업장를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_TWGONG);
                    return false;
                }
            }
            //입금환율
            if (Get_Numeric(this.TXT01_TWYUL.GetValue().ToString().Trim()) == "0")
            {
                this.ShowCustomMessage("입금환율 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TWYUL);
                return false;
            }
            //설정금액
            if (Get_Numeric(this.TXT01_TWIAMT.GetValue().ToString().Trim()) == "0")
            {
                this.ShowCustomMessage("설정금액 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TWIAMT);
                return false;
            }

            //정리금액
            string sTWBJAMT = string.Empty;
            string sTWJNAMT = string.Empty;

            // TY_P_AC_2AB6A696
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AB6A696", SetDefaultValue(this.TXT01_TWYEAR.GetValue().ToString().Trim()),SetDefaultValue(this.TXT01_TWNOSQ.GetValue().ToString().Trim()));  // AIHWANMF
            DataTable dt_WANM = this.DbConnector.ExecuteDataTable();
            if (dt_WANM.Rows.Count > 0)
            {
                sTWBJAMT = Get_Numeric(dt_WANM.Rows[0]["IHWABJ"].ToString().Trim()); //  정리외화금액
            }
            else
            {
                sTWBJAMT = "0";
            }

            this.TXT01_TWBJAMT.SetValue(Get_Numeric(sTWBJAMT));

            //잔액
            sTWJNAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_TWIAMT.GetValue().ToString().Trim())) - Convert.ToDouble(sTWBJAMT));
            //설정금액 < 정리금액
            if (Convert.ToDouble(sTWJNAMT) < 0)
            {
                this.ShowCustomMessage("외화정리금액이 설정금액을 초과합니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TWIAMT);
                return false;
            }

            if (this.CBH01_TWGUBN.GetValue().ToString().Trim() == "JPY")
            {
                sTWJNAMT = Convert.ToString(Convert.ToDouble(Convert.ToDouble(Get_Numeric(this.TXT01_TWIAMT.GetValue().ToString().Trim())) / 100) - Convert.ToDouble(Get_Numeric(sTWBJAMT)));
            }

            this.TXT01_TWJNAMT.SetValue(Get_Numeric(sTWJNAMT));

            return true;
        }
        #endregion

        #region Description : 외화관리 필드 CLEAR
        private void UP_FieldClear_FORE()
        {
            this.TXT01_TWYEAR.SetValue("");             // 외화관리번호(년)
            this.TXT01_TWNOSQ.SetValue("");             // 외화관리번호(순번)
            this.CBH01_TWBANK.SetValue("");             // 은행
            this.CBH01_TWGUJA.SetValue("");             // 계좌번호
            this.CBO01_TWGUIP.SetValue("");             // 구입방법
            this.CBH01_TWGUCD.SetValue("");             // 구입업체
            this.CBO01_TWGONG.SetValue("");             // 사업장
            this.CBH01_TWGUBN.SetValue("");             // 외화구분
            this.TXT01_TWYUL.SetValue("");              // 입금환율
            this.TXT01_TWIAMT.SetValue("");             // 설정금액
            this.TXT01_TWBJAMT.SetValue("");
            this.TXT01_TWJNAMT.SetValue("");

        }
        #endregion

        #region Description : 외화관리 화면값 --> 저장변수 담기
        private void UP_ScreenFile_FORE_Save()
        {
            this.DAT04_TWJPSSID.SetValue(fsSessionId);
            this.DAT04_TWJPDPMK.SetValue(this.CBH01_B2DPMK.GetValue().ToString().Trim());
            this.DAT04_TWJPDTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim());
            this.DAT04_TWJPNOSQ.SetValue(this.TXT01_B2NOSQ.GetValue().ToString().Trim());
            this.DAT04_TWJPNOLN.SetValue(this.CBO01_B2NOLN.SelectedItem.ToString().Trim());

            this.DAT04_TWYEAR.SetValue(this.TXT01_TWYEAR.GetValue().ToString().Trim());
            this.DAT04_TWNOSQ.SetValue(this.TXT01_TWNOSQ.GetValue().ToString().Trim());
            this.DAT04_TWBANK.SetValue(this.CBH01_TWBANK.GetValue().ToString().Trim());
            this.DAT04_TWGUJA.SetValue(this.CBH01_TWGUJA.GetValue().ToString().Trim());
            this.DAT04_TWGUIP.SetValue(this.CBO01_TWGUIP.GetValue().ToString().Trim());
            this.DAT04_TWGUCD.SetValue(this.CBH01_TWGUCD.GetValue().ToString().Trim());
            this.DAT04_TWGONG.SetValue(this.CBO01_TWGONG.GetValue().ToString().Trim());
            this.DAT04_TWGUBN.SetValue(this.CBH01_TWGUBN.GetValue().ToString().Trim());
            this.DAT04_TWYUL.SetValue(this.TXT01_TWYUL.GetValue().ToString().Trim());
            this.DAT04_TWIAMT.SetValue(this.TXT01_TWIAMT.GetValue().ToString().Trim());
        }
        #endregion

        #region Description : 계좌번호 코드 헬프 처리
        private void CBH01_TWBANK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_TWBANK.GetValue().ToString();
            this.CBH01_TWGUJA.DummyValue = groupCode;
            this.CBH01_TWGUJA.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_TWGUJA.Initialize();
        }
        #endregion

        /* --------------------------------------------------------------------------------------------------- */
        /* --------------------------------------    입금표 관리 처리    -------------------------------------- */
        /* --------------------------------------------------------------------------------------------------- */

        #region Description : 입금TMAC1151REF관리 번호 체크 (   호  출  부  분  ) - UP_TMAC1151REF_Check
        private void UP_TMAC1151REF_Check(string sYYMM)
        {

            this.DbConnector.CommandClear();  // TMAC1151REF (전체) 
            this.DbConnector.Attach("TY_P_AC_2B59V050", fsSessionId, 
                                                        this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(),
                                                        this.TXT01_B2NOSQ.GetValue().ToString(), this.CBO01_B2NOLN.SelectedItem.ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CBH01_TRDPMK.SetValue(dt.Rows[0]["TRDPMK"].ToString().Trim());
                this.TXT01_TRYEAR.SetValue(dt.Rows[0]["TRYEAR"].ToString().Trim());
                this.CBH01_TRCDSB.SetValue(dt.Rows[0]["TRCDSB"].ToString().Trim());
                this.TXT01_TRSEQ.SetValue(dt.Rows[0]["TRSEQ"].ToString().Trim());

                UP_Run_MONE();
            }
            else
            {
                UP_FieldClear_MONE();

                //등록
                UP_ImgDisPlay_MONE(true, false, false);
            };

            // 승인일때 조회는 가능하나 수정은 불가능 하게 처리 //
            if (this.txtJunPyoGubn == "2")
            {
                this.BTN61_INP_MONE.Visible = false;  // 저장
                this.BTN61_SAV_MONE.Visible = false;  // 수정
                this.BTN61_REM_MONE.Visible = false;  // 삭제
                this.BTN61_CLO_MONE.Visible = false;  // 닫기
            }

        }
        #endregion

        #region Description : 입력 버튼 처리
        private void BTN61_INP_MONE_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B29V996", this.ControlFactory, "05"); // 저장
            this.DbConnector.ExecuteNonQuery();

            UP_ImgDisPlay_MONE(false, false, false);
            this.ShowCustomMessage("저장 처리 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            this.SetFocus(this.TXT01_TRSEQ);
        }

        private void BTN61_INP_MONE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_ScreenFile_MONE_Save();

            if (UP_Field_Check_MONE() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 수정 버튼 처리
        private void BTN61_SAV_MONE_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B29W997", this.ControlFactory, "05"); // 수정
            this.DbConnector.ExecuteNonQuery();

            UP_ImgDisPlay_MONE(false, false, false);
            this.ShowCustomMessage("수정 처리 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            this.SetFocus(this.TXT01_TRSEQ);
        }

        private void BTN61_SAV_MONE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_ScreenFile_MONE_Save();

            if (UP_Field_Check_MONE() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 삭제 버튼 처리
        private void BTN61_REM_MONE_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B29W998", fsSessionId,
                                                        this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(),
                                                        this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString(),
                                                        this.CBH01_TRDPMK.GetValue().ToString().Trim(), this.TXT01_TRYEAR.GetValue().ToString().Trim(),
                                                        this.CBH01_TRCDSB.GetValue().ToString().Trim(), this.TXT01_TRSEQ.GetValue().ToString().Trim()); // 삭제
            this.DbConnector.ExecuteNonQuery();

            UP_ImgDisPlay_MONE(false, false, false);
            this.ShowCustomMessage("삭제 처리 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            this.SetFocus(this.TXT01_TRSEQ);
        }

        private void BTN61_REM_MONE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_ScreenFile_MONE_Save();

            if (UP_Field_Check_MONE() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 입금표 닫기 버튼
        private void BTN61_CLO_MONE_Click(object sender, EventArgs e)
        {
            tabControl_Remove(); // 세부 내역 잠그기(접대비,외화관리,입금표..)

            this.SetFocus(this.TXT01_B2RKAC);
        } 
        #endregion

        #region Description : 입금표  DB 함수
        //확인 버튼
        private void UP_Run_MONE()
        {
            this.DbConnector.CommandClear();  // TMAC1151REF (상세)
            this.DbConnector.Attach("TY_P_AC_2B29X999", fsSessionId, 
                                                        this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(),
                                                        this.TXT01_B2NOSQ.GetValue().ToString(), this.CBO01_B2NOLN.SelectedItem.ToString(),
                                                        this.CBH01_TRDPMK.GetValue().ToString(), this.TXT01_TRYEAR.GetValue().ToString(),
                                                        this.CBH01_TRCDSB.GetValue().ToString(), this.TXT01_TRSEQ.GetValue().ToString());
            DataTable dt_run = this.DbConnector.ExecuteDataTable();
            if (dt_run.Rows.Count > 0)
            {
                //수정,삭제
                UP_ImgDisPlay_MONE(false, true, true);

                this.CBH01_TRVEND.SetValue(dt_run.Rows[0]["TRVEND"].ToString().Trim());
                this.TXT01_TRAMT.SetValue(dt_run.Rows[0]["TRAMT"].ToString().Trim());
                this.TXT01_TRRKAC.SetValue(dt_run.Rows[0]["TRRKAC"].ToString().Trim());
            }
            else
            {
                UP_FieldClear_MONE();

                //등록
                UP_ImgDisPlay_MONE(true, false, false);
            };

            this.SetFocus(this.CBH01_TRVEND);
        }
        #endregion

        #region Description : 입금표관리 Field Check 메소드
        private bool UP_Field_Check_MONE()
        {

            //년도
            if (SetDefaultValue(this.TXT01_TRYEAR.GetValue().ToString().Trim()) == "")
            {
                this.ShowCustomMessage("년도를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TRYEAR);
                return false;
            }
            //년도
            if (Int32.Parse(Get_Numeric(this.TXT01_TRYEAR.GetValue().ToString().Trim())) < 1970 && Int32.Parse(Get_Numeric(this.TXT01_TRYEAR.GetValue().ToString().Trim())) > 2030)
            {
                this.ShowCustomMessage("년도를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TRYEAR);
                return false;
            }

            //입금액
            if (SetDefaultValue(this.TXT01_TRAMT.GetValue().ToString().Trim()) == "" )
            {
                this.ShowCustomMessage("입금액을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TRAMT);
                return false;
            }
            
            //입금표 번호 존재유무 확인
            this.DbConnector.CommandClear();  // ACRECEMF
            this.DbConnector.Attach("TY_P_AC_26D9D845", this.TXT01_TRYEAR.GetValue().ToString(), this.CBH01_TRDPMK.GetValue().ToString(), this.TXT01_TRSEQ.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("입금표 번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TRSEQ);
                return false;
            }

            return true;
        }
        #endregion

        #region Description : 처리 버튼 이미지 DisPlay
        private void UP_ImgDisPlay_MONE(bool bInsert, bool bUpdate, bool bDelete)
        {
            this.BTN61_INP_MONE.Visible = bInsert;
            this.BTN61_SAV_MONE.Visible = bUpdate;
            this.BTN61_REM_MONE.Visible = bDelete;
        }
        #endregion

        #region Description : 입금표관리 필드 CLEAR
        private void UP_FieldClear_MONE()
        {
            this.CBH01_TRDPMK.SetValue("");
            this.TXT01_TRYEAR.SetValue("");
            this.CBH01_TRCDSB.SetValue("");
            this.TXT01_TRSEQ.SetValue("");
            this.CBH01_TRVEND.SetValue("");
            this.TXT01_TRAMT.SetValue("");
            this.TXT01_TRRKAC.SetValue("");
        }
        #endregion

        #region Description : 입금표관리 화면값 --> 저장변수 담기
        private void UP_ScreenFile_MONE_Save()
        {
            this.DAT05_TRSSID.SetValue(fsSessionId);
            this.DAT05_TRJPDPMK.SetValue(this.CBH01_B2DPMK.GetValue().ToString().Trim());
            this.DAT05_TRJPDTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim());
            this.DAT05_TRJPNOSQ.SetValue(this.TXT01_B2NOSQ.GetValue().ToString().Trim());
            this.DAT05_TRJPNOLN.SetValue(this.CBO01_B2NOLN.SelectedItem.ToString().Trim());
            this.DAT05_TRDPMK.SetValue(this.CBH01_TRDPMK.GetValue().ToString().Trim());
            this.DAT05_TRYEAR.SetValue(this.TXT01_TRYEAR.GetValue().ToString().Trim());
            this.DAT05_TRCDSB.SetValue(this.CBH01_TRCDSB.GetValue().ToString().Trim());
            this.DAT05_TRSEQ.SetValue(this.TXT01_TRSEQ.GetValue().ToString().Trim());
            this.DAT05_TRVEND.SetValue(this.CBH01_TRVEND.GetValue().ToString().Trim());
            this.DAT05_TRAMT.SetValue(this.TXT01_TRAMT.GetValue().ToString().Trim());
            this.DAT05_TRRKAC.SetValue(this.TXT01_TRRKAC.GetValue().ToString().Trim());
        }
        #endregion

        #region Description : 입금표 관리 팝업(입금표 조회)
        private void BTN61_MON_INQ_Click(object sender, EventArgs e)
        {
            TYACBJ001M popup = new TYACBJ001M(this.CBH01_TRDPMK.GetValue().ToString().Trim(), this.TXT01_TRYEAR.GetValue().ToString().Trim(), this.CBH01_TRCDSB.GetValue().ToString().Trim(), "");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CBH01_TRDPMK.SetValue(popup.sLOCAL);
                TXT01_TRYEAR.SetValue(popup.sYEAR);
                TXT01_TRSEQ.SetValue(popup.sTRSEQ);

                //this.BTN61_CONFIRM_Click(null, null);
            }
        }
        #endregion

        /* --------------------------------------------------------------------------------------------------- */
        /* --------------------------------------    불공제 관리 처리    -------------------------------------- */
        /* --------------------------------------------------------------------------------------------------- */

        #region Description : 불공제 내용 체크 (   호  출  부  분  ) - UP_TMAC1102BF_Check
        private void UP_TMAC1102BF_Check(string sTXGB)
        {
            string sNOLN = this.CBO01_B2NOLN.SelectedItem.ToString();

            this.DbConnector.CommandClear();  // TMAC1102BF
            this.DbConnector.Attach("TY_P_AC_2BK9P480", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), sNOLN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                    UP_FieldClear_TXEX();

                    this.CBH01_TSBCDTX.SetValue(sTXGB);

                    this.BTN61_INP_TXEX.Visible = true;  // 저장
                    this.BTN61_SAV_TXEX.Visible = false; // 수정
            }
            else
            {
                UP_FieldClear_TXEX();

                this.CBH01_TSBCDTX.SetValue(dt.Rows[0]["TSBCDTX"].ToString().Trim());          // 부가 구분
                this.CBH01_TSBGUBN.SetValue(dt.Rows[0]["TSBGUBN"].ToString().Trim());          // 사유구분
                this.TXT01_TSBAMT.SetValue(dt.Rows[0]["TSBAMT"].ToString().Trim());            // 공급가액
                this.TXT01_TSBVAT.SetValue(dt.Rows[0]["TSBVAT"].ToString().Trim());            // 부가세
                this.TXT01_VATTOT.SetValue( Convert.ToString( Convert.ToDouble(dt.Rows[0]["TSBAMT"].ToString().Trim()) + Convert.ToDouble(dt.Rows[0]["TSBVAT"].ToString().Trim()) ));  // 합계

                this.BTN61_INP_TXEX.Visible = false;  // 저장
                this.BTN61_SAV_TXEX.Visible = true;   // 수정
            };

            // 승인일때 조회는 가능하나 수정은 불가능 하게 처리 //
            if (this.txtJunPyoGubn == "2")
            {
                this.BTN61_INP_TXEX.Visible = false;  // 저장
                this.BTN61_SAV_TXEX.Visible = false;  // 수정
                this.BTN61_CLO_TXEX.Visible = false;  // 닫기
            }

            this.SetFocus(this.CBH01_TSBGUBN.CodeText);
        }
        #endregion

        #region Description : 불공제 입력 버튼 처리
        private void BTN61_INP_TXEX_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear(); // TMAC1102BF
            this.DbConnector.Attach("TY_P_AC_2BK9Y481", this.ControlFactory, "06"); // 저장
            this.DbConnector.ExecuteNonQuery();

            UP_ImgDisPlay_TXEX(false, false);
            this.ShowCustomMessage("저장 처리 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //this.SetFocus(this.CBH01_TSBGUBN.CodeText);
            this.TXT01_B2RKAC.SetValue("매입부가세");
            this.SetFocus(this.TXT01_B2RKAC);
        }

        private void BTN61_INP_TXEX_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_ScreenFile_TXEX_Save();

            if (UP_Field_Check_TXEX() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }
        } 
        #endregion

        #region Description : 불공제 수정 버튼 처리
        private void BTN61_SAV_TXEX_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear(); //TMAC1102BF
            this.DbConnector.Attach("TY_P_AC_2BK9Z482", this.ControlFactory, "06"); // 수정
            this.DbConnector.ExecuteNonQuery();

            UP_ImgDisPlay_TXEX(false, false);
            this.ShowCustomMessage("수정 처리 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //this.SetFocus(this.CBH01_TSBGUBN.CodeText);
            this.TXT01_B2RKAC.SetValue("매입부가세");
            this.SetFocus(this.TXT01_B2RKAC);
        }

        private void BTN61_SAV_TXEX_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_ScreenFile_TXEX_Save();

            if (UP_Field_Check_TXEX() == false)
            {
                e.Successed = false;
                return;
            }
            else
            {
                e.Successed = true;
                return;
            }

        } 
        #endregion

        #region Description : 불공제관리 닫기 버튼 처리
        private void BTN61_CLO_TXEX_Click(object sender, EventArgs e)
        {
            this.SetFocus(this.TXT01_B2RKAC);
        }
        #endregion

        #region Description : 불공재관리 Field Check 메소드
        private bool UP_Field_Check_TXEX()
        {
            //사유
            if (SetDefaultValue(this.CBH01_TSBGUBN.GetValue().ToString().Trim()) == "")
            {
                this.ShowCustomMessage("사유를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_TSBGUBN.CodeText);
                return false;
            }

            if (this.CBH01_TSBCDTX.GetValue().ToString().Trim() == "55" || this.CBH01_TSBCDTX.GetValue().ToString().Trim() == "75")
            {
                if (this.CBH01_TSBGUBN.GetValue().ToString().Trim() != "03")
                {
                    this.ShowCustomMessage("승용차 불공제는  (비용업용 소형승용차 구입 및 임차)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_TSBGUBN.CodeText);
                    return false;
                }
            }

            if (this.CBH01_TSBCDTX.GetValue().ToString().Trim() == "54" || this.CBH01_TSBCDTX.GetValue().ToString().Trim() == "74")
            {
                if (this.CBH01_TSBGUBN.GetValue().ToString().Trim() == "03")
                {
                    this.ShowCustomMessage("매입세액 불공제는  (소형승용차 구입 사유등록 불가)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_TSBGUBN.CodeText);
                    return false;
                }
            }

            //공급가액
            if (SetDefaultValue(this.TXT01_TSBAMT.GetValue().ToString().Trim()) == "" || Get_Numeric(SetDefaultValue(this.TXT01_TSBAMT.GetValue().ToString().Trim())) == "0")
            {
                this.ShowCustomMessage("공급가액를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TSBAMT);
                return false;
            }

            //부가세
            if (SetDefaultValue(this.TXT01_TSBVAT.GetValue().ToString().Trim()) == "" || Get_Numeric(SetDefaultValue(this.TXT01_TSBVAT.GetValue().ToString().Trim())) == "0")
            {
                this.ShowCustomMessage("부가세를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_TSBVAT);
                return false;
            }

            this.TXT01_VATTOT.SetValue(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_TSBAMT.GetValue().ToString().Trim())) + Convert.ToDouble(Get_Numeric(this.TXT01_TSBVAT.GetValue().ToString().Trim()))));  // 합계

            this.PAN10_VLMI4.SetValue(this.TXT01_VATTOT.GetValue().ToString().Trim());

            return true;
        }
        #endregion

        #region Description : 불공제 필드 CLEAR
        private void UP_FieldClear_TXEX()
        {
            this.CBH01_TSBCDTX.SetValue("");           // 부가 구분
            this.CBH01_TSBGUBN.SetValue("");           // 사유구분
            this.TXT01_TSBAMT.SetValue("");            // 공급가액
            this.TXT01_TSBVAT.SetValue("");            // 부가세
            this.TXT01_VATTOT.SetValue("");            // 합계
        }
        #endregion

        #region Description : 불공제 화면값 --> 저장변수 담기
        private void UP_ScreenFile_TXEX_Save()
        {
            this.DAT06_TSBPSSID.SetValue(fsSessionId);
            this.DAT06_TSBPDPMK.SetValue(this.CBH01_B2DPMK.GetValue().ToString().Trim());
            this.DAT06_TSBPDTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim());
            this.DAT06_TSBPNOSQ.SetValue(this.TXT01_B2NOSQ.GetValue().ToString().Trim());
            this.DAT06_TSBPNOLN.SetValue(this.CBO01_B2NOLN.SelectedItem.ToString().Trim());
            this.DAT06_TSBCDTX.SetValue(this.CBH01_TSBCDTX.GetValue().ToString().Trim());
            this.DAT06_TSBGUBN.SetValue(this.CBH01_TSBGUBN.GetValue().ToString().Trim());
            this.DAT06_TSBAMT.SetValue(this.TXT01_TSBAMT.GetValue().ToString().Trim());
            this.DAT06_TSBVAT.SetValue(this.TXT01_TSBVAT.GetValue().ToString().Trim());

            //this.DAT06_TSBVAT.SetValue(this.TXT01_VATTOT.GetValue().ToString().Trim());
        }
        #endregion

        #region Description : 처리 버튼 이미지 DisPlay
        private void UP_ImgDisPlay_TXEX(bool bInsert, bool bUpdate)
        {
            this.BTN61_INP_TXEX.Visible = bInsert;
            this.BTN61_SAV_TXEX.Visible = bUpdate;
        }
        #endregion

        /* --------------------------------------------------------------------------------------------------- */
        /* --------------------------------------    무역원가 관리 처리    ------------------------------------ */
        /* --------------------------------------------------------------------------------------------------- */

        #region Description : 무역원가 내용 체크 (   호  출  부  분  ) - UP_TDLCCHNF_Check
        private void UP_TDLCCHNF_Check()
        {
            string sNOLN = this.CBO01_B2NOLN.SelectedItem.ToString();

            this.DbConnector.CommandClear();  // NTDLCCHNF
            this.DbConnector.Attach("TY_P_AC_2C352804", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString(), sNOLN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                UP_FieldClear_LCCH();
            }
            else
            {
                UP_FieldClear_LCCH();

                this.CBH01_LCBLGUBN.SetValue(dt.Rows[0]["LCBLGUBN"].ToString().Trim());         // 결재방법
                this.CBH01_LCCHMICD.SetValue(dt.Rows[0]["LCCHMICD"].ToString().Trim());         // 미착코드 
                this.CBO01_LCCHGUBN.SetValue(dt.Rows[0]["LCCHGUBN"].ToString().Trim());         // 발생구분
                this.CBO01_LCACCEPT.SetValue(dt.Rows[0]["LCACCEPT"].ToString().Trim());         // 원가구분

                this.TXT01_FILENUM.SetValue(dt.Rows[0]["FILENO"].ToString().Trim());            // 계획번호
                this.TXT01_LCBLNUMB.SetValue(dt.Rows[0]["LCBLNUMB"].ToString().Trim());         // B/L번호
                this.TXT01_LCPUMMOK.SetValue(dt.Rows[0]["LCPUMMOK"].ToString().Trim());         // 품목코드
                this.TXT01_LCPUMMOKNM.SetValue(dt.Rows[0]["ITEM_NAME1"].ToString().Trim());     // 품목명
            };

            // 승인일때 조회는 가능하나 수정은 불가능 하게 처리 //
            if (this.txtJunPyoGubn == "2")
            {
                this.CBH01_LCBLGUBN.SetReadOnly(true);  // 결재방법
                this.CBH01_LCCHMICD.SetReadOnly(true);  // 미착코드

                this.CBO01_LCCHGUBN.SetReadOnly(true);  // 발생구분
                this.CBO01_LCACCEPT.SetReadOnly(true);  // 원가구분

                this.TXT01_FILENUM.SetReadOnly(true);   // 계획번호
                this.TXT01_LCBLNUMB.SetReadOnly(true);  // B/L번호
                this.TXT01_LCPUMMOK.SetReadOnly(true);  // 품목코드
                this.TXT01_LCPUMMOKNM.SetReadOnly(true);// 품목명
            }
        }
        #endregion

        #region Description : 무역원가 필드 CLEAR
        private void UP_FieldClear_LCCH()
        {
            this.CBH01_LCBLGUBN.SetValue("");           // 결재방법
            this.CBH01_LCCHMICD.SetValue("");           // 미착코드

            this.CBO01_LCCHGUBN.SetValue("");           // 발생구분
            this.CBO01_LCACCEPT.SetValue("");           // 원가구분

            this.TXT01_FILENUM.SetValue("");            // 계획번호
            this.TXT01_LCBLNUMB.SetValue("");           // B/L번호
            this.TXT01_LCPUMMOK.SetValue("");           // 품목코드
            this.TXT01_LCPUMMOKNM.SetValue("");         // 품목명
        }
        #endregion

        #region Description : 무역 입력 필드 체크 ----> UP_TradField_Check()
        // 무역 입력 필드 체크
        // ------------------------------------------------------------------------------ //
        // 1. 무역원가 등록시 화일번호가 없어면 무조건 체크 하지 않는다.
        // ------------------------------------------------------------------------------ //

        private bool UP_TradField_Check()
        {
            string sFILE_NO = string.Empty;

            string spo_no = string.Empty;            //: po no
            string spo_line_no = string.Empty;        //: po 라인 번호,
            string slc_no = string.Empty;            //: lc 번호
            string shouse_bl_no = string.Empty;      //: BL 번호
            string sitem_code = string.Empty;        //: 품목 번호
            string sitem_description = string.Empty;  //: 품목 명
            string sfile_no = string.Empty;        //: 파일번호(3자리)

            double dq_import = 0;

            if ((this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101" ||
               this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200102" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200103" ||
               this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200104" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200105" ||
               this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200188") && (this.TXT01_FILENUM.GetValue().ToString().Trim() == ""))
            {
                this.ShowCustomMessage("무역관력 계정이므로 비용등록 하여야 합니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_FILENUM);
                return false;
            }

            if (this.TXT01_FILENUM.GetValue().ToString().Trim() != "")
            {
                //// 무역관련 계정과목에 대해서 받드시 화일번호가 있어야 한다.
                //if (this.TXT01_FILENUM.GetValue().ToString().Trim() == "")
                //{
                //    this.ShowCustomMessage("무역관력 계정이므로 비용등록 하여야 합니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    this.SetFocus(this.TXT01_FILENUM);
                //    return false;
                //}

                //발생구분　확인 
                if (SetDefaultValue(this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 3)) == "100")
                {
                    if (this.CBO01_LCCHGUBN.GetValue().ToString().Trim() != "1" &&
                        this.CBO01_LCCHGUBN.GetValue().ToString().Trim() != "2")
                    {
                        this.ShowCustomMessage("선택을 잘못하셨습니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBO01_LCCHGUBN);
                        return false;
                    }
                }

                if (this.CBH01_LCCHMICD.GetValue().ToString().Trim() == "")
                {
                    this.ShowCustomMessage("미착코드를 확인하세요.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_LCCHMICD);
                    return false;
                }

                //if (txtLCGUBN.Text.Trim() == "" || txtLCCDPMK.Text.Trim() == "" || txtLCCHSABN.Text.Trim() == "" ||
                //    txtLCHYYNO.Text.Trim() == "" || txtLCCHSQNO.Text.Trim() == "")
                //{
                //    UP_ControlFocus("txtLCGUBN");
                //    return "화일번호를 확인하세요!!!!";

                //}

                //내수상품,중계상품 에는　미착상품계정을　사용할수　없음
                if ((this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 1) == "3" || this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 1) == "2")
                    && (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300"))
                {
                    this.ShowCustomMessage("내수상품 , 중계상품은 미착상품(11200300)계정을 사용할수 없음.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_LCCHMICD);
                    return false;
                }

                // po_no              : po no
                // po_line_no         : po 라인 번호,
                // lc_no              : lc 번호
                // house_bl_no        : BL 번호
                // item_code          : 품목 번호
                // item_description   : 품목 명
                // file_no            : 파일번호(3자리)
                // import_type_code   : 구매형태 코드
                // import_type_name   : 구매형태 명
                // export_type_code   : 판매형태 코드
                // export_type_name   : 판매형태 명
                // import_bl_flag     : 수입BL   진행 여부('N'이면 미진행, 'Y'면 진행) - 내수의 경우는 거래명세 진행여부
                // import_cl_flag     : 수입통관 진행 여부('N'이면 미진행, 'Y'면 진행) - 내수의 경우는 거래명세 진행여부

                // this.TXT01_FILENUM.GetValue().ToString().Trim()
                sFILE_NO = this.TXT01_FILENUM.GetValue().ToString().Trim();

                this.DbConnector.CommandClear(); //
                this.DbConnector.Attach("TY_P_AC_2C45J887", sFILE_NO, this.TXT01_LCBLNUMB.GetValue().ToString().Trim(), "");
                DataTable dt_file = this.DbConnector.ExecuteDataTable();
                if (dt_file.Rows.Count > 0)
                {
                    slc_no = dt_file.Rows[0]["LCNUMBER"].ToString().Trim();
                    shouse_bl_no = dt_file.Rows[0]["BLNUMBER"].ToString().Trim();
                    sitem_code = dt_file.Rows[0]["PUMMOK"].ToString().Trim();
                }
                else
                {
                    this.ShowCustomMessage("미등록된 화일번호입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.TXT01_FILENUM);
                    return false;
                }

                //110파일번호 해당월에 실제 통관량이 존재하는지 체크
                //해당월에 통관량이 없으면 110 3번비용(통관비용)을 등록할수 없다
                if (sFILE_NO.Substring(0, 3) == "110")
                {
                    this.DbConnector.CommandClear(); //
                    this.DbConnector.Attach("TY_P_AC_2C46G888", this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 6), sFILE_NO, this.TXT01_LCBLNUMB.GetValue().ToString().Trim(), this.TXT01_LCPUMMOK.GetValue().ToString().Trim());
                    DataTable dt_cuqty = this.DbConnector.ExecuteDataTable();
                    if (dt_cuqty.Rows.Count > 0)
                    {
                        dq_import = Convert.ToDouble(dt_cuqty.Rows[0]["CL_QUANTITY"].ToString().Trim());
                    }
                    else
                    {
                        dq_import = 0;
                    }

                    if (dq_import == 0)
                    {
                        this.ShowCustomMessage("해당월이전에 통관자료가 존재하지 않습니다! 통관후 110파일을 사용할수 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_FILENUM);
                        return false;
                    }
                }

                //Ｂ／Ｌ번호　확인（발생구분이　２（Ｂ／Ｌ）인경우만CHECK)
                //３（통관）인경우도CHECK
                if (this.CBO01_LCCHGUBN.GetValue().ToString().Trim() != "1")
                {
                    if ((SetDefaultValue(this.TXT01_LCBLNUMB.GetValue().ToString().Trim()) == "" && this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 1) == "1")
                        || SetDefaultValue(this.TXT01_LCPUMMOK.GetValue().ToString().Trim()) == "")
                    {
                        if (SetDefaultValue(this.TXT01_LCBLNUMB.GetValue().ToString().Trim()) == "")
                        {
                            this.ShowCustomMessage("BL번호을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_FILENUM);
                            return false;
                        }

                        if (SetDefaultValue(this.TXT01_LCPUMMOK.GetValue().ToString().Trim()) == "")
                        {
                            this.ShowCustomMessage("품목을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_FILENUM);
                            return false;
                        }
                    }
                    else
                    {
                        //B/L 번호
                        if (this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 1) == "1")
                        {
                            if (shouse_bl_no != SetDefaultValue(this.TXT01_LCBLNUMB.GetValue().ToString().Trim()))
                            {
                                this.ShowCustomMessage("상품내역화일의 계획번호와BL번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                this.SetFocus(this.TXT01_FILENUM);
                                return false;
                            }

                            // 품목코드
                            if (sitem_code != SetDefaultValue(this.TXT01_LCPUMMOK.GetValue().ToString().Trim()))
                            {
                                this.ShowCustomMessage("품목을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                this.SetFocus(this.TXT01_FILENUM);
                                return false;
                            }
                        }
                    }
                    //// 수출상품,내수상품 체크
                    if (this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 1) == "2" || this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 1) == "3")
                    {
                        this.DbConnector.CommandClear(); //
                        this.DbConnector.Attach("TY_P_AC_2C45J887", sFILE_NO, this.TXT01_LCBLNUMB.GetValue().ToString().Trim(), this.TXT01_LCPUMMOK.GetValue().ToString().Trim());
                        DataTable dt_file_one = this.DbConnector.ExecuteDataTable();
                        if (dt_file_one.Rows.Count > 0)
                        {
                            sitem_code = dt_file_one.Rows[0]["PUMMOK"].ToString().Trim();
                        }
                        else
                        {
                            this.ShowCustomMessage("미등록된 품목코드입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_FILENUM);
                            return false;
                        }

                        //    sSql_statm = oCom.SelectOrder_FILENO(sFILE_NO, txtLCBLNUMB.Text.Trim(), SetDefaultValue(txtLCPUMMOK.Text));
                        //    OrReader = oInquery.rdQuery_Ora(sSql_statm);
                        //    if (OrReader.Read())
                        //    {
                        //        spo_no = OrReader["po_no"].ToString();
                        //        sitem_code = OrReader["item_code"].ToString();
                        //    }
                        //    OrReader.Close();

                        //    if (sitem_code != SetDefaultValue(txtLCPUMMOK.Text))
                        //    {
                        //        UP_ControlFocus("txtLCPUMMOK");
                        //        return "미등록된 품목코드입니다!";
                        //    }
                    }
                }

                ////폼목품목명 확인
                //CommUnit.clsCommon oComUnit = new CommUnit.clsCommon();
                //string sRetrunValue = oComUnit.Get_CodeName_Ora("ITEM_CODE", "ITEM_NAME1",
                //                                                  "ITEM",
                //                                                  SetDefaultValue(txtLCPUMMOK.Text));
                //if (sRetrunValue == "없는코드")
                //{
                //    UP_ControlFocus("txtLCPUMMOK");
                //    return "미등록된 품목코드입니다!";
                //}
                //txtLCPUMMOKNM.Text = sRetrunValue;


                // 미착상품 11200300 , 내수상품 11200101,
                if (this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200300" || this.CBH01_B2CDAC.GetValue().ToString().Trim() == "11200101")
                {
                    this.CBO01_LCACCEPT.SetValue("Y");//  원가구분(Y)
                }
                // 비용,부가세
                if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 4) == "4412" || this.CBH01_B2CDAC.GetValue().ToString().Trim().Substring(0, 6) == "111031")
                {
                    this.CBO01_LCACCEPT.SetValue("N");//  원가구분(N)
                }

                //LC 재고 체크 선적이 완료되면 LC원가를 칠수 없다
                if (sFILE_NO.Substring(0, 3) == "100" && this.CBO01_LCACCEPT.GetValue().ToString().Trim() == "Y")
                {
                    if (this.CBO01_LCCHGUBN.GetValue().ToString().Trim() == "1")  //LC 재고체크
                    {
                        this.DbConnector.CommandClear(); //
                        this.DbConnector.Attach("TY_P_AC_2C46X892", this.DTP01_B2DTMK.GetString().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(),
                                                                    sFILE_NO);
                        DataTable dt_LCJEGO = this.DbConnector.ExecuteDataTable();
                        if (dt_LCJEGO.Rows.Count > 0)
                        {
                            if (Convert.ToDouble(dt_LCJEGO.Rows[0]["LCQTY"].ToString().Trim()) - Convert.ToDouble(dt_LCJEGO.Rows[0]["TONGQTY"].ToString().Trim()) - Convert.ToDouble(dt_LCJEGO.Rows[0]["relayqty"].ToString().Trim()) <= 0)
                            {
                                this.ShowCustomMessage("LC재고없는 파일은 원가전표를 발행할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                this.SetFocus(this.TXT01_FILENUM);
                                return false;
                            }
                        }
                        else
                        {
                            this.ShowCustomMessage("미등록된 화일번호입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_FILENUM);
                            return false;
                        }
                    }

                    if (this.CBO01_LCCHGUBN.GetValue().ToString().Trim() == "2")  //BL LC 비용등록 LC_110 수량 체크(미승인등록)
                    {
                        this.DbConnector.CommandClear(); //
                        this.DbConnector.Attach("TY_P_AC_2C46Y893", this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_LCBLNUMB.GetValue().ToString().Trim(),
                                                                    this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_LCBLNUMB.GetValue().ToString().Trim(),
                                                                    this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_LCBLNUMB.GetValue().ToString().Trim(),
                                                                    sFILE_NO);
                        DataTable dt_LCJEGO_ONE = this.DbConnector.ExecuteDataTable();
                        if (dt_LCJEGO_ONE.Rows.Count > 0)
                        {
                            if (Convert.ToDouble(dt_LCJEGO_ONE.Rows[0]["BLQTY"].ToString().Trim()) > 0)
                            {
                                if (Convert.ToDouble(dt_LCJEGO_ONE.Rows[0]["BLQTY"].ToString().Trim()) - Convert.ToDouble(dt_LCJEGO_ONE.Rows[0]["TONGQTY"].ToString().Trim()) <= 0)
                                {
                                    this.ShowCustomMessage("통관이 완료된 파일은 원가전표를 발행할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    this.SetFocus(this.TXT01_FILENUM);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            this.ShowCustomMessage("미등록된 화일번호입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_FILENUM);
                            return false;
                        }
                    }
                }

                //110 파일일경우 출고완료가 되면 원가전표를 칠수 없다  
                if (sFILE_NO.Substring(0, 3) == "110" && this.CBO01_LCACCEPT.GetValue().ToString().Trim() == "Y")
                {
                    this.DbConnector.CommandClear(); //
                    this.DbConnector.Attach("TY_P_AC_2C475898", this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 6), sFILE_NO,
                                                                this.TXT01_LCBLNUMB.GetValue().ToString().Trim(),
                                                                this.TXT01_LCPUMMOK.GetValue().ToString().Trim());
                    DataTable dt_BLJEGO = this.DbConnector.ExecuteDataTable();
                    if (dt_BLJEGO.Rows.Count > 0)
                    {

                        if (Convert.ToDouble(dt_BLJEGO.Rows[0]["jegoqty"].ToString().Trim()) <= 0)
                        {
                            this.ShowCustomMessage("BL재고없는 파일은 원가전표를 발행할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.SetFocus(this.TXT01_FILENUM);
                            return false;
                        }
                    }
                    else
                    {
                        this.ShowCustomMessage("미등록된 화일번호입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_FILENUM);
                        return false;
                    }
                }

                //월마감 작업이후 원가전표를 발행할수 없다 
                if (this.CBO01_LCACCEPT.GetValue().ToString().Trim() == "Y")
                {
                    this.DbConnector.CommandClear(); //
                    this.DbConnector.Attach("TY_P_AC_2C46Q891", this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 6));
                    DataTable dt_JEGOACF = this.DbConnector.ExecuteDataTable();
                    if (dt_JEGOACF.Rows.Count > 0)
                    {
                        string sJEMESG = this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(0, 4) + "년" + this.DTP01_B2DTMK.GetString().ToString().Trim().Substring(4, 2) + "월 마감작업이 완료되어 원가전표 작업이 불가합니다!";
                        this.ShowCustomMessage(sJEMESG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_FILENUM);
                        return false;
                    }
                }

            } // End ...... >  this.TXT01_FILENUM.GetValue().ToString().Trim() != ""  무역원가 등록시 화일번호가 없어면 무족건 체크 하지 않은다.
            return true;
        }

        #endregion

        #region Description : 화일번호 선택후 관리항목 셋팅
        private void BTN61_BTFILE_Click(object sender, EventArgs e)
        {
            TYAZBJ01C6 popup = new TYAZBJ01C6(this.TXT01_FILENUM.GetValue().ToString().Trim());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_FILENUM.SetValue(popup.fsFILENO);
                this.TXT01_LCBLNUMB.SetValue(popup.fsBLNO);
                this.TXT01_LCPUMMOK.SetValue(popup.fsITEM_CODE);
                this.TXT01_LCPUMMOKNM.SetValue(popup.fsITEM_NAME);

                switch (fsCDMI01.Trim())
                {
                    // L/C
                    case "10":
                        this.PAN10_VLMI1.SetValue(popup.fsLCNO);
                        //this.PAN10_VLMI1.SetReadOnly(true);
                        break;
                    // 품목
                    case "34":
                        this.PAN10_VLMI1.SetValue(popup.fsITEM_CODE);
                        //this.PAN10_VLMI1.SetReadOnly(true);
                        break;
                    // FILE	 
                    case "42":
                        this.PAN10_VLMI1.SetValue(popup.fsFILENO);
                        //this.PAN10_VLMI1.SetReadOnly(true);
                        break;
                    // B/L	 
                    case "44":
                        this.PAN10_VLMI1.SetValue(popup.fsBLNO);
                        //this.PAN10_VLMI1.SetReadOnly(true);
                        break;
                }
                switch (fsCDMI02.Trim())
                {
                    // L/C
                    case "10":
                        this.PAN10_VLMI2.SetValue(popup.fsLCNO);
                        //this.PAN10_VLMI2.SetReadOnly(true);
                        break;
                    // 품목
                    case "34":
                        this.PAN10_VLMI2.SetValue(popup.fsITEM_CODE);
                        //this.PAN10_VLMI2.SetReadOnly(true);
                        break;
                    // FILE	 
                    case "42":
                        this.PAN10_VLMI2.SetValue(popup.fsFILENO);
                        //this.PAN10_VLMI2.SetReadOnly(true);
                        break;
                    // B/L	 
                    case "44":
                        this.PAN10_VLMI2.SetValue(popup.fsBLNO);
                        //this.PAN10_VLMI2.SetReadOnly(true);
                        break;
                }
                switch (fsCDMI03.Trim())
                {
                    // L/C
                    case "10":
                        this.PAN10_VLMI3.SetValue(popup.fsLCNO);
                        //this.PAN10_VLMI3.SetReadOnly(true);
                        break;
                    // 품목
                    case "34":
                        this.PAN10_VLMI3.SetValue(popup.fsITEM_CODE);
                        //this.PAN10_VLMI3.SetReadOnly(true);
                        break;
                    // FILE	 
                    case "42":
                        this.PAN10_VLMI3.SetValue(popup.fsFILENO);
                        //this.PAN10_VLMI3.SetReadOnly(true);
                        break;
                    // B/L	 
                    case "44":
                        this.PAN10_VLMI3.SetValue(popup.fsBLNO);
                        //this.PAN10_VLMI3.SetReadOnly(true);
                        break;
                }
                switch (fsCDMI04.Trim())
                {
                    // L/C
                    case "10":
                        this.PAN10_VLMI4.SetValue(popup.fsLCNO);
                        //this.PAN10_VLMI4.SetReadOnly(true);
                        break;
                    // 품목
                    case "34":
                        this.PAN10_VLMI4.SetValue(popup.fsITEM_CODE);
                        //this.PAN10_VLMI4.SetReadOnly(true);
                        break;
                    // FILE	 
                    case "42":
                        this.PAN10_VLMI4.SetValue(popup.fsFILENO);
                        //this.PAN10_VLMI4.SetReadOnly(true);
                        break;
                    // B/L	 
                    case "44":
                        this.PAN10_VLMI4.SetValue(popup.fsBLNO);
                        //this.PAN10_VLMI4.SetReadOnly(true);
                        break;
                }
                switch (fsCDMI05.Trim())
                {
                    // L/C
                    case "10":
                        this.PAN10_VLMI5.SetValue(popup.fsLCNO);
                        //this.PAN10_VLMI5.SetReadOnly(true);
                        break;
                    // 품목
                    case "34":
                        this.PAN10_VLMI5.SetValue(popup.fsITEM_CODE);
                        //this.PAN10_VLMI5.SetReadOnly(true);
                        break;
                    // FILE	 
                    case "42":
                        this.PAN10_VLMI5.SetValue(popup.fsFILENO);
                        //this.PAN10_VLMI5.SetReadOnly(true);
                        break;
                    // B/L	 
                    case "44":
                        this.PAN10_VLMI5.SetValue(popup.fsBLNO);
                        //this.PAN10_VLMI5.SetReadOnly(true);
                        break;
                }
                switch (fsCDMI06.Trim())
                {
                    // L/C
                    case "10":
                        this.PAN10_VLMI6.SetValue(popup.fsLCNO);
                        //this.PAN10_VLMI6.SetReadOnly(true);
                        break;
                    // 품목
                    case "34":
                        this.PAN10_VLMI6.SetValue(popup.fsITEM_CODE);
                        //this.PAN10_VLMI6.SetReadOnly(true);
                        break;
                    // FILE	 
                    case "42":
                        this.PAN10_VLMI6.SetValue(popup.fsFILENO);
                        //this.PAN10_VLMI6.SetReadOnly(true);
                        break;
                    // B/L	 
                    case "44":
                        this.PAN10_VLMI6.SetValue(popup.fsBLNO);
                        //this.PAN10_VLMI6.SetReadOnly(true);
                        break;
                }

            }

            this.SetFocus(this.CBO01_LCCHGUBN);
        } 
        #endregion

        #region Description : 원가구분 Enter Key 처리 ---> CBO01_LCACCEPT_KeyPress()
        private void CBO01_LCACCEPT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_INP.Visible == false)
                {
                    this.SetFocus(this.BTN61_EDIT);
                }
                else
                {
                    this.SetFocus(this.BTN61_INP);
                }

            }
        } 
        #endregion

        #region Description : 무역원가 등록,수정,삭제 처리 ----> UP_Trad_Press_Save_Del()
        private void UP_Trad_Press_Save_Del( string sPRSUBN)
        {
            if (this.TXT01_FILENUM.GetValue().ToString().Trim() != "")
            {
                if (sPRSUBN == "INS")
                {
                    UP_LCCH_ScreenFile_Save();
                    UP_LCCH_Save();
                }
                else if (sPRSUBN == "UPD")
                {
                    UP_LCCH_Delete();
                    UP_LCCH_ScreenFile_Save();
                    UP_LCCH_Save();
                }
                else if (sPRSUBN == "DEL")
                {
                    UP_LCCH_Delete();
                }
            }
        }
        #endregion

        #region Description : 무역원가등록 화면값 --> 저장변수 담기  ---> UP_LCCH_ScreenFile_Save()
        private void UP_LCCH_ScreenFile_Save()
        {
            this.DAT07_LCSSID.SetValue(fsSessionId);

            //   TXT01_FILENUM (100-B80200-0281-M-12-0009)
            this.DAT07_LCGUBN.SetValue(this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(0, 3));
            this.DAT07_LCCHTEAM.SetValue(this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(4, 6));
            this.DAT07_LCCHSABN.SetValue(this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(11, 6));
            this.DAT07_LCCHYYNO.SetValue(this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(18, 2));
            this.DAT07_LCCHSQNO.SetValue(this.TXT01_FILENUM.GetValue().ToString().Trim().Substring(21, 4));

            this.DAT07_LCCHDEPT.SetValue(this.CBH01_B2DPMK.GetValue().ToString().Trim());
            this.DAT07_LCCHILJA.SetValue(this.DTP01_B2DTMK.GetString().ToString().Trim());
            this.DAT07_LCCHJPNO.SetValue(this.TXT01_B2NOSQ.GetValue().ToString().Trim());
            this.DAT07_LCCHJPSQ.SetValue(this.CBO01_B2NOLN.SelectedItem.ToString().Trim());

            this.DAT07_LCPUMMOK.SetValue(this.TXT01_LCPUMMOK.GetValue().ToString().Trim());
            this.DAT07_LCRECODE.SetValue("");
            this.DAT07_LCCHMICD.SetValue(this.CBH01_LCCHMICD.GetValue().ToString().Trim());
            this.DAT07_LCCHACCD.SetValue(this.CBH01_B2CDAC.GetValue().ToString().Trim());
            this.DAT07_LCCHAMT.SetValue(this.TXT01_B2AMDR.GetValue().ToString());
            this.DAT07_LCCHGUBN.SetValue(this.CBO01_LCCHGUBN.GetValue().ToString().Trim());
            this.DAT07_LCBLGUBN.SetValue(this.CBH01_LCBLGUBN.GetValue().ToString().Trim());
            this.DAT07_LCBLNUMB.SetValue(this.TXT01_LCBLNUMB.GetValue().ToString().Trim());
            this.DAT07_LCACCEPT.SetValue(this.CBO01_LCACCEPT.GetValue().ToString().Trim());
            this.DAT07_LCRKAC.SetValue(this.TXT01_B2RKAC.GetValue().ToString().Trim());
            this.DAT07_LCHIGUBN.SetValue("A");
            //this.DAT07_LCHIDATE;
            //this.DAT07_LCHITIME;

        }
        #endregion

        #region Description : 무역원가 입력 처리
        private void UP_LCCH_Save()
        {
            this.DbConnector.CommandClear();  // NTDLCCHNF
            this.DbConnector.Attach("TY_P_AC_2C350802", this.ControlFactory, "07"); // 저장
            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 무역원가 삭제 처리
        private void UP_LCCH_Delete()
        {
            this.DbConnector.CommandClear(); // NTDLCCHNF
            this.DbConnector.Attach("TY_P_AC_2C34V800", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(),
                                                                     this.TXT01_B2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString().Trim()); // 삭제
            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        /* --------------------------------------------------------------------------------------------------- */
        /* --------------------------------------    출장신청서 문서 관리 처리   ------------------------------ */
        /* --------------------------------------------------------------------------------------------------- */

        #region Description : 출장문서  등록,수정,삭제 처리 ----> UP_Business_Press_Save_Del()
        private void UP_Business_Press_Save_Del(string sPRGUBN)
        {
            if (sPRGUBN == "DEL")
            {
                this.DbConnector.CommandClear(); // ADSLGLF '42411201','42411202','44111201','44111202','44121201','44121202','44211201','44211202'
                this.DbConnector.Attach("TY_P_AC_35E6J669", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim()); // ADSLGLF
                DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
                if (dt_adsl.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_adsl.Rows.Count; i++)
                    {
                        if (dt_adsl.Rows[i]["B2CDMI1"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI2"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim() == "37" ||
                            dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI5"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI6"].ToString().Trim() == "37")
                        {
                            string sSabun = UP_CDMIToVLMI("05", dt_adsl.Rows[i]["B2CDMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI2"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI4"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2CDMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI6"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2VLMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI2"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2VLMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI4"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2VLMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI6"].ToString().Trim());

                            string sBesnum = UP_CDMIToVLMI("37", dt_adsl.Rows[i]["B2CDMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI2"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI4"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2CDMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI6"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2VLMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI2"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2VLMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI4"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2VLMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI6"].ToString().Trim());

                            string sJpnosq = dt_adsl.Rows[i]["B2DPMK"].ToString().Trim() + dt_adsl.Rows[i]["B2DTMK"].ToString().Trim() +
                                             Set_Fill3(dt_adsl.Rows[i]["B2NOSQ"].ToString().Trim()) + Set_Fill2(dt_adsl.Rows[i]["B2NOLN"].ToString().Trim());

                            string sB2AMDR = dt_adsl.Rows[i]["B2AMDR"].ToString().Trim();
                            // 전표번호삭제 UPDATE 
                            if (sBesnum != "" && sSabun != "")
                            {
                                this.DbConnector.CommandClear();
                                //ORA
                                //this.DbConnector.Attach("TY_P_AC_36L9K864", sSabun, sBesnum); // GTHUEXPENSEF (전표번호 구하기)
                                //DB2 신인사용(2016년 1월사용)
                                this.DbConnector.Attach("TY_P_AC_5BCF1160", sSabun, sBesnum); // GTHUEXPENSEF (전표번호 구하기)
                                DataTable dt_EXPENSE = this.DbConnector.ExecuteDataTable();
                                if (dt_EXPENSE.Rows.Count > 0)
                                {
                                    if (dt_EXPENSE.Rows[0]["GXJUNNO1"].ToString().Trim() == sJpnosq)
                                    {
                                        this.DbConnector.CommandClear();  // GTHUEXPENSEF (전표번호 1)
                                        //ORA
                                        //this.DbConnector.Attach("TY_P_AC_36L2C869", "", sB2AMDR , sSabun, sBesnum);
                                        //DB2 신인사용(2016년 1월사용)
                                        this.DbConnector.Attach("TY_P_AC_5BCEY157", "", sB2AMDR, sSabun, sBesnum);
                                        this.DbConnector.ExecuteNonQuery();
                                    }
                                    else
                                        if (dt_EXPENSE.Rows[0]["GXJUNNO2"].ToString().Trim() == sJpnosq)
                                        {
                                            this.DbConnector.CommandClear();  // GTHUEXPENSEF (전표번호 2)
                                            //ORA
                                            //this.DbConnector.Attach("TY_P_AC_36L2D870", "",sB2AMDR, sSabun, sBesnum);
                                            //DB2 신인사용(2016년 1월사용)
                                            this.DbConnector.Attach("TY_P_AC_5BCEZ158", "", sB2AMDR, sSabun, sBesnum);
                                            this.DbConnector.ExecuteNonQuery();
                                        }
                                }
                            }
                        }
                    }
                } // .... end .... dt_adsl.Rows.Count > 0
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_35E6J669", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim()); // ADSLGLF
                DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
                if (dt_adsl.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_adsl.Rows.Count; i++)
                    {
                        if (dt_adsl.Rows[i]["B2CDMI1"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI2"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim() == "37" ||
                            dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI5"].ToString().Trim() == "37" || dt_adsl.Rows[i]["B2CDMI6"].ToString().Trim() == "37")
                        {
                            string sSabun = UP_CDMIToVLMI("05", dt_adsl.Rows[i]["B2CDMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI2"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI4"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2CDMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI6"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2VLMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI2"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2VLMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI4"].ToString().Trim(),
                                                                dt_adsl.Rows[i]["B2VLMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI6"].ToString().Trim());

                            string sBesnum = UP_CDMIToVLMI("37", dt_adsl.Rows[i]["B2CDMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI2"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2CDMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI4"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2CDMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2CDMI6"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2VLMI1"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI2"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2VLMI3"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI4"].ToString().Trim(),
                                                                 dt_adsl.Rows[i]["B2VLMI5"].ToString().Trim(), dt_adsl.Rows[i]["B2VLMI6"].ToString().Trim());

                            string sJpnosq = dt_adsl.Rows[i]["B2DPMK"].ToString().Trim() + dt_adsl.Rows[i]["B2DTMK"].ToString().Trim() +
                                             Set_Fill3(dt_adsl.Rows[i]["B2NOSQ"].ToString().Trim()) + Set_Fill2(dt_adsl.Rows[i]["B2NOLN"].ToString().Trim());

                            string sB2AMDR = dt_adsl.Rows[i]["B2AMDR"].ToString().Trim();

                            // 전표번호등록 UPDATE 
                            if (sBesnum != "" && sSabun != "" )
                            {
                                this.DbConnector.CommandClear();
                                //ORA
                                //this.DbConnector.Attach("TY_P_AC_36L9K864", sSabun, sBesnum); // GTHUEXPENSEF (전표번호 구하기)
                                //DB2 신인사용(2016년 1월 사용)
                                this.DbConnector.Attach("TY_P_AC_5BCF1160", sSabun, sBesnum);
                                DataTable dt_EXPENSE = this.DbConnector.ExecuteDataTable();
                                if (dt_EXPENSE.Rows.Count > 0)
                                {
                                    if (dt_EXPENSE.Rows[0]["GXJUNNO1"].ToString().Trim() == "")
                                    {
                                        this.DbConnector.CommandClear();  // GTHUEXPENSEF  (전표번호 1)
                                        //ORA
                                        //this.DbConnector.Attach("TY_P_AC_35E2V665", sJpnosq, sB2AMDR, sSabun, sBesnum);
                                        //DB2 신인사용(2016년 1월 사용)
                                        this.DbConnector.Attach("TY_P_AC_5BCEW155", sJpnosq, sB2AMDR, sSabun, sBesnum);
                                        this.DbConnector.ExecuteNonQuery();
                                    }
                                    else
                                        if (dt_EXPENSE.Rows[0]["GXJUNNO2"].ToString().Trim() == "")
                                        {
                                            this.DbConnector.CommandClear();  // GTHUEXPENSEF  (전표번호 2)
                                            //ORA
                                            //this.DbConnector.Attach("TY_P_AC_36L9J862", sJpnosq, sB2AMDR, sSabun, sBesnum);
                                            //DB2 신인사용(2016년 1월 사용)
                                            this.DbConnector.Attach("TY_P_AC_5BCF0159", sJpnosq, sB2AMDR, sSabun, sBesnum);
                                            this.DbConnector.ExecuteNonQuery();
                                        }
                                }



                            }
                        }
                    }
                } // .... end .... dt_adsl.Rows.Count > 0
            }

        }
        #endregion

        /* --------------------------------------------------------------------------------------------------- */

        #region Description : Page_Load 시 사번,귀속부서 세팅
        private void UP_Page_SabunInit()
        {
            //사번 조회
            this.CBH01_B2HISAB.SetValue(Employer.EmpNo.ToString().Trim());
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_B2DTMK.GetString(),this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count == 0)
            {
                //this.ShowCustomMessage("사원번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //this.SetFocus(this.CBH01_B2HISAB);
            }
            else
            {
                this.CBH01_B2DPMK.SetValue(dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim());
            }
        } 
        #endregion

        #region  Description : DTP01_B2DTMK_ValueChanged 이벤트
        private void DTP01_B2DTMK_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString();
        }
        #endregion

        #region Description : FormClosing  --->  TYACBJ001I_FormClosing() (미승인전표 닫기 (X))
        private void TYACBJ001I_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CBH01_B2DPMK.GetValue().ToString().Trim() != "" && this.DTP01_B2DTMK.GetString().ToString() != "" && this.TXT01_B2NOSQ.GetValue().ToString() != "")
            {
                // 임시화일전체삭제 (TMAC1102F - DELETE )
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB4S685", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString()); // TMAC1102F
                this.DbConnector.ExecuteNonQuery();

                // 접대비 임시화일 전체삭제(TMAC1102SF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB4T687", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1102SF
                this.DbConnector.ExecuteNonQuery();

                //외화관리 임시화일 전체삭제(TMAC1102WF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AB4U688", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1102WF
                this.DbConnector.ExecuteNonQuery();

                //입금표 임시화일 전체삭제(TMAC1151REF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B2AM002", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1151REF
                this.DbConnector.ExecuteNonQuery();

                //불공제 임시화일 전체삭제(TMAC1102BF - DELETE)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2BJ5F471", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// TMAC1102BF
                this.DbConnector.ExecuteNonQuery();

                //LC비용내역 임시 전체삭제(NTDLCCHNF - DELETE) -오라클
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C38P816", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString());// NTDLCCHNF
                this.DbConnector.ExecuteNonQuery();
            }
        }
        #endregion

        #region Description : 적용 기본 셋팅
        private void UP_Get_MemoLogList()
        {

            DateTime dTime = Convert.ToDateTime(Set_Date(this.DTP01_B2DTMK.GetString().ToString())).AddMonths(-2);

            string sSdate = dTime.Year.ToString() + Set_Fill2(dTime.Month.ToString()) + Set_Fill2(dTime.Day.ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_54MB6253", sSdate, this.DTP01_B2DTMK.GetString().ToString(), TYUserInfo.EmpNo);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                AutoCompleteStringCollection aclist = null;
                aclist = new AutoCompleteStringCollection();

                foreach (DataRow dr in dt.Rows)
                {
                    aclist.Add(dr[0].ToString());
                }
                this.TXT01_B2RKAC.AutoCompleteCustomSource = aclist;                
            }
        }
        #endregion      
        
        #region Description : 자산번호 조회 VALUE38_CodeText_TextChanged 이벤트
        private void CBH10_VALUE38_CodeText_TextChanged(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                this.CBH10_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI01 };
            }
        }
        private void CBH11_VALUE38_CodeText_TextChanged(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                this.CBH11_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI02 };
            }
        }
        private void CBH12_VALUE38_CodeText_TextChanged(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                this.CBH12_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI03 };
            }
        }
        private void CBH13_VALUE38_CodeText_TextChanged(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                this.CBH13_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI04 };
            }
        }
        private void CBH14_VALUE38_CodeText_TextChanged(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                this.CBH14_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI05 };
            }
        }
        private void CBH15_VALUE38_CodeText_TextChanged(object sender, EventArgs e)
        {
            UP_SetVLMIVALUE();

            // 자산관리번호
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200100" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200200" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200300" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200400" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200500" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200600" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200700" || SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200800" ||
                SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString().Trim()) == "12200900")
            {
                this.CBH15_VALUE38.DummyValue = new string[] { fsSessionId, "", fsVLMI06 };
            }
        }
        #endregion

        #region Description : 관리 항목 1 ~ 6 코드 BOX 셋팅 ---> UP_Set_Control_New()
        public void UP_Set_Control_New()
        {
            string sCode = "";
            string sCodeName = "";

            DbConnector dbConnector = new DbConnector(CurrentSystem.VirtualProgram);
            dbConnector.Attach("TY_P_AC_D3REY776");  //계정과목에 따른 관리항목 
            DataTable dt = dbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt16(dt.Rows[i]["NUM"].ToString()) == 1)
                    {
                        if (Convert.ToInt16(dt.Rows[i]["INDXCNT"].ToString()) > 0 && Convert.ToInt16(dt.Rows[i]["INDXROW"].ToString()) == 1)
                        {
                            this.CBH10_B2INDX = new TYCodeBox();
                            this.CBH10_B2INDX.Name = "CBH10_B2INDX";
                            this.CBH10_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH10_B2INDX_CodeBoxDataBinded);
                        }

                        sCode = dt.Rows[i]["A1CDMI1"].ToString();
                        sCodeName = dt.Rows[i]["A2NMCD"].ToString();
                        switch (sCode)
                        {
                            case "01":
                                this.CBH10_VALUE01 = new TYCodeBox();
                                this.CBH10_VALUE01.Name = "CBH10_VALUE01";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE01);
                                break;
                            case "02":
                                this.CBH10_B2INDX.DummyValue = "BK";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                                break;
                            case "03":
                                this.CBH10_VALUE03 = new TYCodeBox();
                                this.CBH10_VALUE03.Name = "CBH10_VALUE03";
                                this.CBH10_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE03, this.CBH10_VALUE03.DummyValue);
                                break;
                            case "04":
                                this.TXT10_VALUE04 = new TYTextBox();
                                this.TXT10_VALUE04.Name = "TXT10_VALUE04";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE04);
                                break;
                            case "05":
                                this.CBH10_VALUE05 = new TYCodeBox();
                                this.CBH10_VALUE05.Name = "CBH10_VALUE05";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE05);
                                break;
                            case "06":
                                this.TXT10_VALUE06 = new TYTextBox();
                                this.TXT10_VALUE06.Name = "TXT10_VALUE06";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE06);
                                break;
                            case "07":
                                this.CBH10_VALUE07 = new TYCodeBox();
                                this.CBH10_VALUE07.Name = "CBH10_VALUE07";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE07);
                                break;
                            case "08":
                                this.TXT10_VALUE08 = new TYTextBox();
                                this.TXT10_VALUE08.Name = "TXT10_VALUE08";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE08);
                                break;
                            case "09":
                                this.CBH10_VALUE09 = new TYCodeBox();
                                this.CBH10_VALUE09.Name = "CBH10_VALUE09";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE09);
                                this.CBH10_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
                                break;
                            case "10":
                                this.CBH10_VALUE10 = new TYCodeBox();
                                this.CBH10_VALUE10.Name = "CBH10_VALUE10";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE10);
                                break;
                            case "11":
                                this.CBH10_B2INDX.DummyValue = "TX";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                                break;
                            case "12":
                                this.TXT10_VALUE12 = new TYTextBox();
                                this.TXT10_VALUE12.Name = "TXT10_VALUE12";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE12);
                                break;
                            case "13":
                                this.TXT10_VALUE13 = new TYTextBox();
                                this.TXT10_VALUE13.Name = "TXT10_VALUE13";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE13);
                                break;
                            case "14":
                                this.TXT10_VALUE14 = new TYTextBox();
                                this.TXT10_VALUE14.Name = "TXT10_VALUE14";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE14);
                                break;
                            case "15":
                                this.TXT10_VALUE15 = new TYTextBox();
                                this.TXT10_VALUE15.Name = "TXT10_VALUE15";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE15);
                                break;
                            case "16":
                                this.TXT10_VALUE16 = new TYTextBox();
                                this.TXT10_VALUE16.Name = "TXT10_VALUE16";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE16);
                                break;
                            case "17":
                                this.TXT10_VALUE17 = new TYTextBox();
                                this.TXT10_VALUE17.Name = "TXT10_VALUE17";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE17);
                                break;
                            case "18":
                                this.TXT10_VALUE18 = new TYTextBox();
                                this.TXT10_VALUE18.Name = "TXT10_VALUE18";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE18);
                                break;
                            case "19":
                                this.TXT10_VALUE19 = new TYTextBox();
                                this.TXT10_VALUE19.Name = "TXT10_VALUE19";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE19);
                                break;
                            case "20":
                                this.TXT10_VALUE20 = new TYTextBox();
                                this.TXT10_VALUE20.Name = "TXT10_VALUE20";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE20);
                                break;
                            case "21":
                                this.TXT10_VALUE21 = new TYTextBox();
                                this.TXT10_VALUE21.Name = "TXT10_VALUE21";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE21);
                                break;
                            case "23":
                                this.TXT10_VALUE23 = new TYTextBox();
                                this.TXT10_VALUE23.Name = "TXT10_VALUE23";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE23);
                                break;
                            case "24":
                                this.CBH10_B2INDX.DummyValue = "BG";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                                break;
                            case "25":
                                this.CBH10_B2INDX.DummyValue = "BB";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                                break;
                            case "26":
                                this.CBH10_VALUE26 = new TYCodeBox();
                                this.CBH10_VALUE26.Name = "CBH10_VALUE26";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE26);
                                break;
                            case "27":
                                this.TXT10_VALUE27 = new TYTextBox();
                                this.TXT10_VALUE27.Name = "TXT10_VALUE27";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE27);
                                break;
                            case "28":
                                this.TXT10_VALUE28 = new TYTextBox();
                                this.TXT10_VALUE28.Name = "TXT10_VALUE28";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE28);
                                break;
                            case "29":
                                this.CBH10_VALUE29 = new TYCodeBox();
                                this.CBH10_VALUE29.Name = "CBH10_VALUE29";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE29);
                                this.CBH10_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
                                break;
                            case "30":
                                this.CBH10_B2INDX.DummyValue = "FR";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                                break;
                            case "31":
                                this.TXT10_VALUE31 = new TYTextBox();
                                this.TXT10_VALUE31.Name = "TXT10_VALUE31";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE31);
                                break;
                            case "32":
                                this.CBH10_VALUE32 = new TYCodeBox();
                                this.CBH10_VALUE32.Name = "CBH10_VALUE32";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE32);
                                break;
                            case "33":
                                this.TXT10_VALUE33 = new TYTextBox();
                                this.TXT10_VALUE33.Name = "TXT10_VALUE33";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE33);
                                break;
                            case "34":
                                this.CBH10_VALUE34 = new TYCodeBox();
                                this.CBH10_VALUE34.Name = "CBH10_VALUE34";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE34);
                                break;
                            case "35":
                                this.CBH10_VALUE35 = new TYCodeBox();
                                this.CBH10_VALUE35.Name = "CBH10_VALUE35";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE35);
                                this.CBH10_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
                                break;
                            case "36":
                                this.TXT10_VALUE36 = new TYTextBox();
                                this.TXT10_VALUE36.Name = "TXT10_VALUE36";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE36);
                                break;
                            case "37":
                                this.CBH10_VALUE37 = new TYCodeBox();
                                this.CBH10_VALUE37.Name = "CBH10_VALUE37";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE37);
                                this.CBH10_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
                                break;
                            case "38":
                                this.CBH10_VALUE38 = new TYCodeBox();
                                this.CBH10_VALUE38.Name = "CBH10_VALUE38";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE38);
                                this.CBH10_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH10_VALUE38_CodeText_TextChanged);
                                this.CBH10_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
                                break;
                            case "40":
                                this.CBH10_B2INDX.DummyValue = "AL";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                                break;
                            case "41":
                                this.CBH10_VALUE41 = new TYCodeBox();
                                this.CBH10_VALUE41.Name = "CBH10_VALUE41";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE41);
                                this.CBH10_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
                                break;
                            case "42":
                                this.CBH10_VALUE42 = new TYCodeBox();
                                this.CBH10_VALUE42.Name = "CBH10_VALUE42";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE42);
                                break;
                            case "44":
                                this.CBH10_VALUE44 = new TYCodeBox();
                                this.CBH10_VALUE44.Name = "CBH10_VALUE44";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE44);
                                break;
                            case "46":
                                this.CBH10_B2INDX.DummyValue = "UA";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_B2INDX, this.CBH10_B2INDX.DummyValue);
                                break;
                            case "47":
                                this.TXT10_VALUE47 = new TYTextBox();
                                this.TXT10_VALUE47.Name = "TXT10_VALUE47";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE47);
                                break;
                            case "49":
                                this.TXT10_VALUE49 = new TYTextBox();
                                this.TXT10_VALUE49.Name = "TXT10_VALUE49";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE49);
                                break;
                            case "50":
                                this.CBH10_VALUE50 = new TYCodeBox();
                                this.CBH10_VALUE50.Name = "CBH10_VALUE50";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE50);
                                break;
                            case "51":
                                this.CBH10_VALUE51 = new TYCodeBox();
                                this.CBH10_VALUE51.Name = "CBH10_VALUE51";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.CBH10_VALUE51);
                                break;
                            case "52":
                                this.TXT10_VALUE52 = new TYTextBox();
                                this.TXT10_VALUE52.Name = "TXT10_VALUE52";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE52);
                                break;
                            case "53":
                                this.TXT10_VALUE53 = new TYTextBox();
                                this.TXT10_VALUE53.Name = "TXT10_VALUE53";
                                this.PAN10_VLMI1.AddControl(sCode, sCodeName, this.TXT10_VALUE53);
                                break;
                            default:
                                break;
                        }

                    }

                    if (Convert.ToInt16(dt.Rows[i]["NUM"].ToString()) == 2)
                    {
                        if (Convert.ToInt16(dt.Rows[i]["INDXCNT"].ToString()) > 0 && Convert.ToInt16(dt.Rows[i]["INDXROW"].ToString()) == 1)
                        {
                            this.CBH11_B2INDX = new TYCodeBox();
                            this.CBH11_B2INDX.Name = "CBH11_B2INDX";
                            this.CBH11_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH11_B2INDX_CodeBoxDataBinded);
                        }

                        sCode = dt.Rows[i]["A1CDMI1"].ToString();
                        sCodeName = dt.Rows[i]["A2NMCD"].ToString();

                        switch (sCode)
                        {
                            case "01":
                                this.CBH11_VALUE01 = new TYCodeBox();
                                this.CBH11_VALUE01.Name = "CBH11_VALUE01";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE01);
                                break;
                            case "02":

                                this.CBH11_B2INDX.DummyValue = "BK";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                                break;
                            case "03":
                                this.CBH11_VALUE03 = new TYCodeBox();
                                this.CBH11_VALUE03.Name = "CBH11_VALUE03";
                                this.CBH11_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE03, this.CBH11_VALUE03.DummyValue);
                                break;
                            case "04":
                                this.TXT11_VALUE04 = new TYTextBox();
                                this.TXT11_VALUE04.Name = "TXT11_VALUE04";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE04);
                                break;
                            case "05":
                                this.CBH11_VALUE05 = new TYCodeBox();
                                this.CBH11_VALUE05.Name = "CBH11_VALUE05";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE05);
                                break;
                            case "06":
                                this.TXT11_VALUE06 = new TYTextBox();
                                this.TXT11_VALUE06.Name = "TXT11_VALUE06";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE06);
                                break;
                            case "07":
                                this.CBH11_VALUE07 = new TYCodeBox();
                                this.CBH11_VALUE07.Name = "CBH11_VALUE07";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE07);
                                break;
                            case "08":
                                this.TXT11_VALUE08 = new TYTextBox();
                                this.TXT11_VALUE08.Name = "TXT11_VALUE08";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE08);
                                break;
                            case "09":
                                this.CBH11_VALUE09 = new TYCodeBox();
                                this.CBH11_VALUE09.Name = "CBH11_VALUE09";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE09);
                                this.CBH11_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
                                break;
                            case "10":
                                this.CBH11_VALUE10 = new TYCodeBox();
                                this.CBH11_VALUE10.Name = "CBH11_VALUE10";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE10);
                                break;
                            case "11":
                                this.CBH11_B2INDX.DummyValue = "TX";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                                break;
                            case "12":
                                this.TXT11_VALUE12 = new TYTextBox();
                                this.TXT11_VALUE12.Name = "TXT11_VALUE12";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE12);
                                break;
                            case "13":
                                this.TXT11_VALUE13 = new TYTextBox();
                                this.TXT11_VALUE13.Name = "TXT11_VALUE13";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE13);
                                break;
                            case "14":
                                this.TXT11_VALUE14 = new TYTextBox();
                                this.TXT11_VALUE14.Name = "TXT11_VALUE14";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE14);
                                break;
                            case "15":
                                this.TXT11_VALUE15 = new TYTextBox();
                                this.TXT11_VALUE15.Name = "TXT11_VALUE15";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE15);
                                break;
                            case "16":
                                this.TXT11_VALUE16 = new TYTextBox();
                                this.TXT11_VALUE16.Name = "TXT11_VALUE16";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE16);
                                break;
                            case "17":
                                this.TXT11_VALUE17 = new TYTextBox();
                                this.TXT11_VALUE17.Name = "TXT11_VALUE17";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE17);
                                break;
                            case "18":
                                this.TXT11_VALUE18 = new TYTextBox();
                                this.TXT11_VALUE18.Name = "TXT11_VALUE18";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE18);
                                break;
                            case "19":
                                this.TXT11_VALUE19 = new TYTextBox();
                                this.TXT11_VALUE19.Name = "TXT11_VALUE19";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE19);
                                break;
                            case "20":
                                this.TXT11_VALUE20 = new TYTextBox();
                                this.TXT11_VALUE20.Name = "TXT11_VALUE20";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE20);
                                break;
                            case "21":
                                this.TXT11_VALUE21 = new TYTextBox();
                                this.TXT11_VALUE21.Name = "TXT11_VALUE21";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE21);
                                break;
                            case "23":
                                this.TXT11_VALUE23 = new TYTextBox();
                                this.TXT11_VALUE23.Name = "TXT11_VALUE23";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE23);
                                break;
                            case "24":
                                this.CBH11_B2INDX.DummyValue = "BG";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                                break;
                            case "25":
                                this.CBH11_B2INDX.DummyValue = "BB";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                                break;
                            case "26":
                                this.CBH11_VALUE26 = new TYCodeBox();
                                this.CBH11_VALUE26.Name = "CBH11_VALUE26";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE26);
                                break;
                            case "27":
                                this.TXT11_VALUE27 = new TYTextBox();
                                this.TXT11_VALUE27.Name = "TXT11_VALUE27";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE27);
                                break;
                            case "28":
                                this.TXT11_VALUE28 = new TYTextBox();
                                this.TXT11_VALUE28.Name = "TXT11_VALUE28";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE28);
                                break;
                            case "29":
                                this.CBH11_VALUE29 = new TYCodeBox();
                                this.CBH11_VALUE29.Name = "CBH11_VALUE29";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE29);
                                this.CBH11_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
                                break;
                            case "30":
                                this.CBH11_B2INDX.DummyValue = "FR";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                                break;
                            case "31":
                                this.TXT11_VALUE31 = new TYTextBox();
                                this.TXT11_VALUE31.Name = "TXT11_VALUE31";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE31);
                                break;
                            case "32":
                                this.CBH11_VALUE32 = new TYCodeBox();
                                this.CBH11_VALUE32.Name = "CBH11_VALUE32";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE32);
                                break;
                            case "33":
                                this.TXT11_VALUE33 = new TYTextBox();
                                this.TXT11_VALUE33.Name = "TXT11_VALUE33";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE33);
                                break;
                            case "34":
                                this.CBH11_VALUE34 = new TYCodeBox();
                                this.CBH11_VALUE34.Name = "CBH11_VALUE34";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE34);
                                break;
                            case "35":
                                this.CBH11_VALUE35 = new TYCodeBox();
                                this.CBH11_VALUE35.Name = "CBH11_VALUE35";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE35);
                                this.CBH11_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
                                break;
                            case "36":
                                this.TXT11_VALUE36 = new TYTextBox();
                                this.TXT11_VALUE36.Name = "TXT11_VALUE36";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE36);
                                break;
                            case "37":
                                this.CBH11_VALUE37 = new TYCodeBox();
                                this.CBH11_VALUE37.Name = "CBH11_VALUE37";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE37);
                                this.CBH11_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
                                break;
                            case "38":
                                this.CBH11_VALUE38 = new TYCodeBox();
                                this.CBH11_VALUE38.Name = "CBH11_VALUE38";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE38);
                                this.CBH11_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH11_VALUE38_CodeText_TextChanged);
                                this.CBH11_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
                                break;
                            case "40":
                                this.CBH11_B2INDX.DummyValue = "AL";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                                break;
                            case "41":
                                this.CBH11_VALUE41 = new TYCodeBox();
                                this.CBH11_VALUE41.Name = "CBH11_VALUE41";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE41);
                                this.CBH11_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
                                break;
                            case "42":
                                this.CBH11_VALUE42 = new TYCodeBox();
                                this.CBH11_VALUE42.Name = "CBH11_VALUE42";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE42);
                                break;
                            case "44":
                                this.CBH11_VALUE44 = new TYCodeBox();
                                this.CBH11_VALUE44.Name = "CBH11_VALUE44";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE44);
                                break;
                            case "46":
                                this.CBH11_B2INDX.DummyValue = "UA";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_B2INDX, this.CBH11_B2INDX.DummyValue);
                                break;
                            case "47":
                                this.TXT11_VALUE47 = new TYTextBox();
                                this.TXT11_VALUE47.Name = "TXT11_VALUE47";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE47);
                                break;
                            case "49":
                                this.TXT11_VALUE49 = new TYTextBox();
                                this.TXT11_VALUE49.Name = "TXT11_VALUE49";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE49);
                                break;
                            case "50":
                                this.CBH11_VALUE50 = new TYCodeBox();
                                this.CBH11_VALUE50.Name = "CBH11_VALUE50";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE50);
                                break;
                            case "51":
                                this.CBH11_VALUE51 = new TYCodeBox();
                                this.CBH11_VALUE51.Name = "CBH11_VALUE51";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.CBH11_VALUE51);
                                break;
                            case "52":
                                this.TXT11_VALUE52 = new TYTextBox();
                                this.TXT11_VALUE52.Name = "TXT11_VALUE52";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE52);
                                break;
                            case "53":
                                this.TXT11_VALUE53 = new TYTextBox();
                                this.TXT11_VALUE53.Name = "TXT11_VALUE53";
                                this.PAN10_VLMI2.AddControl(sCode, sCodeName, this.TXT11_VALUE53);
                                break;
                            default:
                                break;
                        }


                    }

                    if (Convert.ToInt16(dt.Rows[i]["NUM"].ToString()) == 3)
                    {
                        if (Convert.ToInt16(dt.Rows[i]["INDXCNT"].ToString()) > 0 && Convert.ToInt16(dt.Rows[i]["INDXROW"].ToString()) == 1)
                        {
                            this.CBH12_B2INDX = new TYCodeBox();
                            this.CBH12_B2INDX.Name = "CBH12_B2INDX";
                            this.CBH12_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH12_B2INDX_CodeBoxDataBinded);
                        }

                        sCode = dt.Rows[i]["A1CDMI1"].ToString();
                        sCodeName = dt.Rows[i]["A2NMCD"].ToString();

                        switch (sCode)
                        {
                            case "01":
                                this.CBH12_VALUE01 = new TYCodeBox();
                                this.CBH12_VALUE01.Name = "CBH12_VALUE01";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE01);
                                break;
                            case "02":
                                this.CBH12_B2INDX.DummyValue = "BK";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                                break;
                            case "03":
                                this.CBH12_VALUE03 = new TYCodeBox();
                                this.CBH12_VALUE03.Name = "CBH12_VALUE03";
                                this.CBH12_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE03, this.CBH12_VALUE03.DummyValue);
                                break;
                            case "04":
                                this.TXT12_VALUE04 = new TYTextBox();
                                this.TXT12_VALUE04.Name = "TXT12_VALUE04";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE04);
                                break;
                            case "05":
                                this.CBH12_VALUE05 = new TYCodeBox();
                                this.CBH12_VALUE05.Name = "CBH12_VALUE05";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE05);
                                break;
                            case "06":
                                this.TXT12_VALUE06 = new TYTextBox();
                                this.TXT12_VALUE06.Name = "TXT12_VALUE06";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE06);
                                break;
                            case "07":
                                this.CBH12_VALUE07 = new TYCodeBox();
                                this.CBH12_VALUE07.Name = "CBH12_VALUE07";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE07);
                                break;
                            case "08":
                                this.TXT12_VALUE08 = new TYTextBox();
                                this.TXT12_VALUE08.Name = "TXT12_VALUE08";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE08);
                                break;
                            case "09":
                                this.CBH12_VALUE09 = new TYCodeBox();
                                this.CBH12_VALUE09.Name = "CBH12_VALUE09";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE09);
                                this.CBH12_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
                                break;
                            case "10":
                                this.CBH12_VALUE10 = new TYCodeBox();
                                this.CBH12_VALUE10.Name = "CBH12_VALUE10";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE10);
                                break;
                            case "11":
                                this.CBH12_B2INDX.DummyValue = "TX";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                                break;
                            case "12":
                                this.TXT12_VALUE12 = new TYTextBox();
                                this.TXT12_VALUE12.Name = "TXT12_VALUE12";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE12);
                                break;
                            case "13":
                                this.TXT12_VALUE13 = new TYTextBox();
                                this.TXT12_VALUE13.Name = "TXT12_VALUE13";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE13);
                                break;
                            case "14":
                                this.TXT12_VALUE14 = new TYTextBox();
                                this.TXT12_VALUE14.Name = "TXT12_VALUE14";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE14);
                                break;
                            case "15":
                                this.TXT12_VALUE15 = new TYTextBox();
                                this.TXT12_VALUE15.Name = "TXT12_VALUE15";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE15);
                                break;
                            case "16":
                                this.TXT12_VALUE16 = new TYTextBox();
                                this.TXT12_VALUE16.Name = "TXT12_VALUE16";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE16);
                                break;
                            case "17":
                                this.TXT12_VALUE17 = new TYTextBox();
                                this.TXT12_VALUE17.Name = "TXT12_VALUE17";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE17);
                                break;
                            case "18":
                                this.TXT12_VALUE18 = new TYTextBox();
                                this.TXT12_VALUE18.Name = "TXT12_VALUE18";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE18);
                                break;
                            case "19":
                                this.TXT12_VALUE19 = new TYTextBox();
                                this.TXT12_VALUE19.Name = "TXT12_VALUE19";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE19);
                                break;
                            case "20":
                                this.TXT12_VALUE20 = new TYTextBox();
                                this.TXT12_VALUE20.Name = "TXT12_VALUE20";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE20);
                                break;
                            case "21":
                                this.TXT12_VALUE21 = new TYTextBox();
                                this.TXT12_VALUE21.Name = "TXT12_VALUE21";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE21);
                                break;
                            case "23":
                                this.TXT12_VALUE23 = new TYTextBox();
                                this.TXT12_VALUE23.Name = "TXT12_VALUE23";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE23);
                                break;
                            case "24":
                                this.CBH12_B2INDX.DummyValue = "BG";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                                break;
                            case "25":
                                this.CBH12_B2INDX.DummyValue = "BB";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                                break;
                            case "26":
                                this.CBH12_VALUE26 = new TYCodeBox();
                                this.CBH12_VALUE26.Name = "CBH12_VALUE26";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE26);
                                break;
                            case "27":
                                this.TXT12_VALUE27 = new TYTextBox();
                                this.TXT12_VALUE27.Name = "TXT12_VALUE27";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE27);
                                break;
                            case "28":
                                this.TXT12_VALUE28 = new TYTextBox();
                                this.TXT12_VALUE28.Name = "TXT12_VALUE28";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE28);
                                break;
                            case "29":
                                this.CBH12_VALUE29 = new TYCodeBox();
                                this.CBH12_VALUE29.Name = "CBH12_VALUE29";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE29);
                                this.CBH12_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
                                break;
                            case "30":
                                this.CBH12_B2INDX.DummyValue = "FR";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                                break;
                            case "31":
                                this.TXT12_VALUE31 = new TYTextBox();
                                this.TXT12_VALUE31.Name = "TXT12_VALUE31";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE31);
                                break;
                            case "32":
                                this.CBH12_VALUE32 = new TYCodeBox();
                                this.CBH12_VALUE32.Name = "CBH12_VALUE32";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE32);
                                break;
                            case "33":
                                this.TXT12_VALUE33 = new TYTextBox();
                                this.TXT12_VALUE33.Name = "TXT12_VALUE33";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE33);
                                break;
                            case "34":
                                this.CBH12_VALUE34 = new TYCodeBox();
                                this.CBH12_VALUE34.Name = "CBH12_VALUE34";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE34);
                                break;
                            case "35":
                                this.CBH12_VALUE35 = new TYCodeBox();
                                this.CBH12_VALUE35.Name = "CBH12_VALUE35";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE35);
                                this.CBH12_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
                                break;
                            case "36":
                                this.TXT12_VALUE36 = new TYTextBox();
                                this.TXT12_VALUE36.Name = "TXT12_VALUE36";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE36);
                                break;
                            case "37":
                                this.CBH12_VALUE37 = new TYCodeBox();
                                this.CBH12_VALUE37.Name = "CBH12_VALUE37";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE37);
                                this.CBH12_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
                                break;
                            case "38":
                                this.CBH12_VALUE38 = new TYCodeBox();
                                this.CBH12_VALUE38.Name = "CBH12_VALUE38";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE38);
                                this.CBH12_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH12_VALUE38_CodeText_TextChanged);
                                this.CBH12_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
                                break;
                            case "40":
                                this.CBH12_B2INDX.DummyValue = "AL";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                                break;
                            case "41":
                                this.CBH12_VALUE41 = new TYCodeBox();
                                this.CBH12_VALUE41.Name = "CBH12_VALUE41";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE41);
                                this.CBH12_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
                                break;
                            case "42":
                                this.CBH12_VALUE42 = new TYCodeBox();
                                this.CBH12_VALUE42.Name = "CBH12_VALUE42";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE42);
                                break;
                            case "44":
                                this.CBH12_VALUE44 = new TYCodeBox();
                                this.CBH12_VALUE44.Name = "CBH12_VALUE44";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE44);
                                break;
                            case "46":
                                this.CBH12_B2INDX.DummyValue = "UA";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_B2INDX, this.CBH12_B2INDX.DummyValue);
                                break;
                            case "47":
                                this.TXT12_VALUE47 = new TYTextBox();
                                this.TXT12_VALUE47.Name = "TXT12_VALUE47";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE47);
                                break;
                            case "49":
                                this.TXT12_VALUE49 = new TYTextBox();
                                this.TXT12_VALUE49.Name = "TXT12_VALUE49";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE49);
                                break;
                            case "50":
                                this.CBH12_VALUE50 = new TYCodeBox();
                                this.CBH12_VALUE50.Name = "CBH12_VALUE50";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE50);
                                break;
                            case "51":
                                this.CBH12_VALUE51 = new TYCodeBox();
                                this.CBH12_VALUE51.Name = "CBH12_VALUE51";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.CBH12_VALUE51);
                                break;
                            case "52":
                                this.TXT12_VALUE52 = new TYTextBox();
                                this.TXT12_VALUE52.Name = "TXT12_VALUE52";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE52);
                                break;
                            case "53":
                                this.TXT12_VALUE53 = new TYTextBox();
                                this.TXT12_VALUE53.Name = "TXT12_VALUE53";
                                this.PAN10_VLMI3.AddControl(sCode, sCodeName, this.TXT12_VALUE53);
                                break;
                            default:
                                break;
                        }


                    }

                    if (Convert.ToInt16(dt.Rows[i]["NUM"].ToString()) == 4)
                    {
                        if (Convert.ToInt16(dt.Rows[i]["INDXCNT"].ToString()) > 0 && Convert.ToInt16(dt.Rows[i]["INDXROW"].ToString()) == 1)
                        {
                            this.CBH13_B2INDX = new TYCodeBox();
                            this.CBH13_B2INDX.Name = "CBH13_B2INDX";
                            this.CBH13_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH13_B2INDX_CodeBoxDataBinded);
                        }

                        sCode = dt.Rows[i]["A1CDMI1"].ToString();
                        sCodeName = dt.Rows[i]["A2NMCD"].ToString();

                        switch (sCode)
                        {
                            case "01":
                                this.CBH13_VALUE01 = new TYCodeBox();
                                this.CBH13_VALUE01.Name = "CBH13_VALUE01";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE01);
                                break;
                            case "02":
                                this.CBH13_B2INDX.DummyValue = "BK";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                                break;
                            case "03":
                                this.CBH13_VALUE03 = new TYCodeBox();
                                this.CBH13_VALUE03.Name = "CBH13_VALUE03";
                                this.CBH13_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE03, this.CBH13_VALUE03.DummyValue);
                                break;
                            case "04":
                                this.TXT13_VALUE04 = new TYTextBox();
                                this.TXT13_VALUE04.Name = "TXT13_VALUE04";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE04);
                                break;
                            case "05":
                                this.CBH13_VALUE05 = new TYCodeBox();
                                this.CBH13_VALUE05.Name = "CBH13_VALUE05";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE05);
                                break;
                            case "06":
                                this.TXT13_VALUE06 = new TYTextBox();
                                this.TXT13_VALUE06.Name = "TXT13_VALUE06";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE06);
                                break;
                            case "07":
                                this.CBH13_VALUE07 = new TYCodeBox();
                                this.CBH13_VALUE07.Name = "CBH13_VALUE07";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE07);
                                break;
                            case "08":
                                this.TXT13_VALUE08 = new TYTextBox();
                                this.TXT13_VALUE08.Name = "TXT13_VALUE08";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE08);
                                break;
                            case "09":
                                this.CBH13_VALUE09 = new TYCodeBox();
                                this.CBH13_VALUE09.Name = "CBH13_VALUE09";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE09);
                                this.CBH13_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
                                break;
                            case "10":
                                this.CBH13_VALUE10 = new TYCodeBox();
                                this.CBH13_VALUE10.Name = "CBH13_VALUE10";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE10);
                                break;
                            case "11":
                                this.CBH13_B2INDX.DummyValue = "TX";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                                break;
                            case "12":
                                this.TXT13_VALUE12 = new TYTextBox();
                                this.TXT13_VALUE12.Name = "TXT13_VALUE12";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE12);
                                break;
                            case "13":
                                this.TXT13_VALUE13 = new TYTextBox();
                                this.TXT13_VALUE13.Name = "TXT13_VALUE13";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE13);
                                break;
                            case "14":
                                this.TXT13_VALUE14 = new TYTextBox();
                                this.TXT13_VALUE14.Name = "TXT13_VALUE14";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE14);
                                break;
                            case "15":
                                this.TXT13_VALUE15 = new TYTextBox();
                                this.TXT13_VALUE15.Name = "TXT13_VALUE15";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE15);
                                break;
                            case "16":
                                this.TXT13_VALUE16 = new TYTextBox();
                                this.TXT13_VALUE16.Name = "TXT13_VALUE16";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE16);
                                break;
                            case "17":
                                this.TXT13_VALUE17 = new TYTextBox();
                                this.TXT13_VALUE17.Name = "TXT13_VALUE17";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE17);
                                break;
                            case "18":
                                this.TXT13_VALUE18 = new TYTextBox();
                                this.TXT13_VALUE18.Name = "TXT13_VALUE18";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE18);
                                break;
                            case "19":
                                this.TXT13_VALUE19 = new TYTextBox();
                                this.TXT13_VALUE19.Name = "TXT13_VALUE19";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE19);
                                break;
                            case "20":
                                this.TXT13_VALUE20 = new TYTextBox();
                                this.TXT13_VALUE20.Name = "TXT13_VALUE20";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE20);
                                break;
                            case "21":
                                this.TXT13_VALUE21 = new TYTextBox();
                                this.TXT13_VALUE21.Name = "TXT13_VALUE21";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE21);
                                break;
                            case "23":
                                this.TXT13_VALUE23 = new TYTextBox();
                                this.TXT13_VALUE23.Name = "TXT13_VALUE23";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE23);
                                break;
                            case "24":
                                this.CBH13_B2INDX.DummyValue = "BG";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                                break;
                            case "25":
                                this.CBH13_B2INDX.DummyValue = "BB";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                                break;
                            case "26":
                                this.CBH13_VALUE26 = new TYCodeBox();
                                this.CBH13_VALUE26.Name = "CBH10_VALUE26";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE26);
                                break;
                            case "27":
                                this.TXT13_VALUE27 = new TYTextBox();
                                this.TXT13_VALUE27.Name = "TXT13_VALUE27";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE27);
                                break;
                            case "28":
                                this.TXT13_VALUE28 = new TYTextBox();
                                this.TXT13_VALUE28.Name = "TXT13_VALUE28";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE28);
                                break;
                            case "29":
                                this.CBH13_VALUE29 = new TYCodeBox();
                                this.CBH13_VALUE29.Name = "CBH13_VALUE29";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE29);
                                this.CBH13_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
                                break;
                            case "30":
                                this.CBH13_B2INDX.DummyValue = "FR";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                                break;
                            case "31":
                                this.TXT13_VALUE31 = new TYTextBox();
                                this.TXT13_VALUE31.Name = "TXT13_VALUE31";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE31);
                                break;
                            case "32":
                                this.CBH13_VALUE32 = new TYCodeBox();
                                this.CBH13_VALUE32.Name = "CBH13_VALUE32";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE32);
                                break;
                            case "33":
                                this.TXT13_VALUE33 = new TYTextBox();
                                this.TXT13_VALUE33.Name = "TXT13_VALUE33";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE33);
                                break;
                            case "34":
                                this.CBH13_VALUE34 = new TYCodeBox();
                                this.CBH13_VALUE34.Name = "CBH13_VALUE34";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE34);
                                break;
                            case "35":
                                this.CBH13_VALUE35 = new TYCodeBox();
                                this.CBH13_VALUE35.Name = "CBH13_VALUE35";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE35);
                                this.CBH13_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
                                break;
                            case "36":
                                this.TXT13_VALUE36 = new TYTextBox();
                                this.TXT13_VALUE36.Name = "TXT13_VALUE36";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE36);
                                break;
                            case "37":
                                this.CBH13_VALUE37 = new TYCodeBox();
                                this.CBH13_VALUE37.Name = "CBH13_VALUE37";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE37);
                                this.CBH13_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
                                break;
                            case "38":
                                this.CBH13_VALUE38 = new TYCodeBox();
                                this.CBH13_VALUE38.Name = "CBH13_VALUE38";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE38);
                                this.CBH13_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH13_VALUE38_CodeText_TextChanged);
                                this.CBH13_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
                                break;
                            case "40":
                                this.CBH13_B2INDX.DummyValue = "AL";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                                break;
                            case "41":
                                this.CBH13_VALUE41 = new TYCodeBox();
                                this.CBH13_VALUE41.Name = "CBH13_VALUE41";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE41);
                                this.CBH13_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
                                break;
                            case "42":
                                this.CBH13_VALUE42 = new TYCodeBox();
                                this.CBH13_VALUE42.Name = "CBH13_VALUE42";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE42);
                                break;
                            case "44":
                                this.CBH13_VALUE44 = new TYCodeBox();
                                this.CBH13_VALUE44.Name = "CBH13_VALUE44";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE44);
                                break;
                            case "46":
                                this.CBH13_B2INDX.DummyValue = "UA";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_B2INDX, this.CBH13_B2INDX.DummyValue);
                                break;
                            case "47":
                                this.TXT13_VALUE47 = new TYTextBox();
                                this.TXT13_VALUE47.Name = "TXT13_VALUE47";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE47);
                                break;
                            case "49":
                                this.TXT13_VALUE49 = new TYTextBox();
                                this.TXT13_VALUE49.Name = "TXT13_VALUE49";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE49);
                                break;
                            case "50":
                                this.CBH13_VALUE50 = new TYCodeBox();
                                this.CBH13_VALUE50.Name = "CBH13_VALUE50";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE50);
                                break;
                            case "51":
                                this.CBH13_VALUE51 = new TYCodeBox();
                                this.CBH13_VALUE51.Name = "CBH13_VALUE51";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.CBH13_VALUE51);
                                break;
                            case "52":
                                this.TXT13_VALUE52 = new TYTextBox();
                                this.TXT13_VALUE52.Name = "TXT13_VALUE52";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE52);
                                break;
                            case "53":
                                this.TXT13_VALUE53 = new TYTextBox();
                                this.TXT13_VALUE53.Name = "TXT13_VALUE53";
                                this.PAN10_VLMI4.AddControl(sCode, sCodeName, this.TXT13_VALUE53);
                                break;
                            default:
                                break;
                        }


                    }

                    if (Convert.ToInt16(dt.Rows[i]["NUM"].ToString()) == 5)
                    {
                        if (Convert.ToInt16(dt.Rows[i]["INDXCNT"].ToString()) > 0 && Convert.ToInt16(dt.Rows[i]["INDXROW"].ToString()) == 1)
                        {
                            this.CBH14_B2INDX = new TYCodeBox();
                            this.CBH14_B2INDX.Name = "CBH14_B2INDX";
                            this.CBH14_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH14_B2INDX_CodeBoxDataBinded);
                        }

                        sCode = dt.Rows[i]["A1CDMI1"].ToString();
                        sCodeName = dt.Rows[i]["A2NMCD"].ToString();

                        switch (sCode)
                        {
                            case "01":
                                this.CBH14_VALUE01 = new TYCodeBox();
                                this.CBH14_VALUE01.Name = "CBH14_VALUE01";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE01);
                                break;
                            case "02":
                                this.CBH14_B2INDX.DummyValue = "BK";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                                break;
                            case "03":
                                this.CBH14_VALUE03 = new TYCodeBox();
                                this.CBH14_VALUE03.Name = "CBH14_VALUE03";
                                this.CBH14_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE03, this.CBH14_VALUE03.DummyValue);
                                break;
                            case "04":
                                this.TXT14_VALUE04 = new TYTextBox();
                                this.TXT14_VALUE04.Name = "TXT14_VALUE04";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE04);
                                break;
                            case "05":
                                this.CBH14_VALUE05 = new TYCodeBox();
                                this.CBH14_VALUE05.Name = "CBH14_VALUE05";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE05);
                                break;
                            case "06":
                                this.TXT14_VALUE06 = new TYTextBox();
                                this.TXT14_VALUE06.Name = "TXT14_VALUE06";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE06);
                                break;
                            case "07":
                                this.CBH14_VALUE07 = new TYCodeBox();
                                this.CBH14_VALUE07.Name = "CBH14_VALUE07";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE07);
                                break;
                            case "08":
                                this.TXT14_VALUE08 = new TYTextBox();
                                this.TXT14_VALUE08.Name = "TXT14_VALUE08";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE08);
                                break;
                            case "09":
                                this.CBH14_VALUE09 = new TYCodeBox();
                                this.CBH14_VALUE09.Name = "CBH14_VALUE09";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE09);
                                this.CBH14_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
                                break;
                            case "10":
                                this.CBH14_VALUE10 = new TYCodeBox();
                                this.CBH14_VALUE10.Name = "CBH14_VALUE10";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE10);
                                break;
                            case "11":
                                this.CBH14_B2INDX.DummyValue = "TX";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                                break;
                            case "12":
                                this.TXT14_VALUE12 = new TYTextBox();
                                this.TXT14_VALUE12.Name = "TXT14_VALUE12";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE12);
                                break;
                            case "13":
                                this.TXT14_VALUE13 = new TYTextBox();
                                this.TXT14_VALUE13.Name = "TXT14_VALUE13";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE13);
                                break;
                            case "14":
                                this.TXT14_VALUE14 = new TYTextBox();
                                this.TXT14_VALUE14.Name = "TXT14_VALUE14";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE14);
                                break;
                            case "15":
                                this.TXT14_VALUE15 = new TYTextBox();
                                this.TXT14_VALUE15.Name = "TXT14_VALUE15";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE15);
                                break;
                            case "16":
                                this.TXT14_VALUE16 = new TYTextBox();
                                this.TXT14_VALUE16.Name = "TXT14_VALUE16";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE16);
                                break;
                            case "17":
                                this.TXT14_VALUE17 = new TYTextBox();
                                this.TXT14_VALUE17.Name = "TXT14_VALUE17";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE17);
                                break;
                            case "18":
                                this.TXT14_VALUE18 = new TYTextBox();
                                this.TXT14_VALUE18.Name = "TXT14_VALUE18";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE18);
                                break;
                            case "19":
                                this.TXT14_VALUE19 = new TYTextBox();
                                this.TXT14_VALUE19.Name = "TXT14_VALUE19";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE19);
                                break;
                            case "20":
                                this.TXT14_VALUE20 = new TYTextBox();
                                this.TXT14_VALUE20.Name = "TXT14_VALUE20";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE20);
                                break;
                            case "21":
                                this.TXT14_VALUE21 = new TYTextBox();
                                this.TXT14_VALUE21.Name = "TXT14_VALUE21";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE21);
                                break;
                            case "23":
                                this.TXT14_VALUE23 = new TYTextBox();
                                this.TXT14_VALUE23.Name = "TXT14_VALUE23";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE23);
                                break;
                            case "24":
                                this.CBH14_B2INDX.DummyValue = "BG";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                                break;
                            case "25":
                                this.CBH14_B2INDX.DummyValue = "BB";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                                break;
                            case "26":
                                this.CBH14_VALUE26 = new TYCodeBox();
                                this.CBH14_VALUE26.Name = "CBH14_VALUE26";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE26);
                                break;
                            case "27":
                                this.TXT14_VALUE27 = new TYTextBox();
                                this.TXT14_VALUE27.Name = "TXT14_VALUE27";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE27);
                                break;
                            case "28":
                                this.TXT14_VALUE28 = new TYTextBox();
                                this.TXT14_VALUE28.Name = "TXT14_VALUE28";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE28);
                                break;
                            case "29":
                                this.CBH14_VALUE29 = new TYCodeBox();
                                this.CBH14_VALUE29.Name = "CBH14_VALUE29";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE29);
                                this.CBH14_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
                                break;
                            case "30":
                                this.CBH14_B2INDX.DummyValue = "FR";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                                break;
                            case "31":
                                this.TXT14_VALUE31 = new TYTextBox();
                                this.TXT14_VALUE31.Name = "TXT14_VALUE31";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE31);
                                break;
                            case "32":
                                this.CBH14_VALUE32 = new TYCodeBox();
                                this.CBH14_VALUE32.Name = "CBH14_VALUE32";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE32);
                                break;
                            case "33":
                                this.TXT14_VALUE33 = new TYTextBox();
                                this.TXT14_VALUE33.Name = "TXT14_VALUE33";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE33);
                                break;
                            case "34":
                                this.CBH14_VALUE34 = new TYCodeBox();
                                this.CBH14_VALUE34.Name = "CBH14_VALUE34";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE34);
                                break;
                            case "35":
                                this.CBH14_VALUE35 = new TYCodeBox();
                                this.CBH14_VALUE35.Name = "CBH14_VALUE35";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE35);
                                this.CBH14_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
                                break;
                            case "36":
                                this.TXT14_VALUE36 = new TYTextBox();
                                this.TXT14_VALUE36.Name = "TXT14_VALUE36";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE36);
                                break;
                            case "37":
                                this.CBH14_VALUE37 = new TYCodeBox();
                                this.CBH14_VALUE37.Name = "CBH14_VALUE37";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE37);
                                this.CBH14_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
                                break;
                            case "38":
                                this.CBH14_VALUE38 = new TYCodeBox();
                                this.CBH14_VALUE38.Name = "CBH14_VALUE38";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE38);
                                this.CBH14_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH14_VALUE38_CodeText_TextChanged);
                                this.CBH14_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
                                break;
                            case "40":
                                this.CBH14_B2INDX.DummyValue = "AL";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                                break;
                            case "41":
                                this.CBH14_VALUE41 = new TYCodeBox();
                                this.CBH14_VALUE41.Name = "CBH14_VALUE41";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE41);
                                this.CBH14_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
                                break;
                            case "42":
                                this.CBH14_VALUE42 = new TYCodeBox();
                                this.CBH14_VALUE42.Name = "CBH14_VALUE42";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE42);
                                break;
                            case "44":
                                this.CBH14_VALUE44 = new TYCodeBox();
                                this.CBH14_VALUE44.Name = "CBH14_VALUE44";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE44);
                                break;
                            case "46":
                                this.CBH14_B2INDX.DummyValue = "UA";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_B2INDX, this.CBH14_B2INDX.DummyValue);
                                break;
                            case "47":
                                this.TXT14_VALUE47 = new TYTextBox();
                                this.TXT14_VALUE47.Name = "TXT14_VALUE47";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE47);
                                break;
                            case "49":
                                this.TXT14_VALUE49 = new TYTextBox();
                                this.TXT14_VALUE49.Name = "TXT11_VALUE49";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE49);
                                break;
                            case "50":
                                this.CBH14_VALUE50 = new TYCodeBox();
                                this.CBH14_VALUE50.Name = "CBH14_VALUE50";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE50);
                                break;
                            case "51":
                                this.CBH14_VALUE51 = new TYCodeBox();
                                this.CBH14_VALUE51.Name = "CBH14_VALUE51";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH14_VALUE51);
                                break;
                            case "52":
                                this.TXT14_VALUE52 = new TYTextBox();
                                this.TXT14_VALUE52.Name = "TXT14_VALUE52";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE52);
                                break;
                            case "53":
                                this.TXT14_VALUE53 = new TYTextBox();
                                this.TXT14_VALUE53.Name = "TXT14_VALUE53";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.TXT14_VALUE53);
                                break;
                            default:
                                break;
                        }
                    }

                    if (Convert.ToInt16(dt.Rows[i]["NUM"].ToString()) == 6)
                    {
                        if (Convert.ToInt16(dt.Rows[i]["INDXCNT"].ToString()) > 0 && Convert.ToInt16(dt.Rows[i]["INDXROW"].ToString()) == 1)
                        {
                            this.CBH15_B2INDX = new TYCodeBox();
                            this.CBH15_B2INDX.Name = "CBH15_B2INDX";
                            this.CBH15_B2INDX.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(this.CBH15_B2INDX_CodeBoxDataBinded);
                        }

                        sCode = dt.Rows[i]["A1CDMI1"].ToString();
                        sCodeName = dt.Rows[i]["A2NMCD"].ToString();

                        switch (sCode)
                        {
                            case "01":
                                this.CBH15_VALUE01 = new TYCodeBox();
                                this.CBH15_VALUE01.Name = "CBH15_VALUE01";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE01);
                                break;
                            case "02":
                                this.CBH15_B2INDX.DummyValue = "BK";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
                                break;
                            case "03":
                                this.CBH15_VALUE03 = new TYCodeBox();
                                this.CBH15_VALUE03.Name = "CBH15_VALUE03";
                                this.CBH15_VALUE03.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE03, this.CBH15_VALUE03.DummyValue);
                                break;
                            case "04":
                                this.TXT15_VALUE04 = new TYTextBox();
                                this.TXT15_VALUE04.Name = "TXT15_VALUE04";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE04);
                                break;
                            case "05":
                                this.CBH15_VALUE05 = new TYCodeBox();
                                this.CBH15_VALUE05.Name = "CBH15_VALUE05";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE05);
                                break;
                            case "06":
                                this.TXT15_VALUE06 = new TYTextBox();
                                this.TXT15_VALUE06.Name = "TXT15_VALUE06";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE06);
                                break;
                            case "07":
                                this.CBH15_VALUE07 = new TYCodeBox();
                                this.CBH15_VALUE07.Name = "CBH15_VALUE07";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE07);
                                break;
                            case "08":
                                this.TXT15_VALUE08 = new TYTextBox();
                                this.TXT15_VALUE08.Name = "TXT15_VALUE08";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE08);
                                break;
                            case "09":
                                this.CBH15_VALUE09 = new TYCodeBox();
                                this.CBH15_VALUE09.Name = "CBH15_VALUE09";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE09);
                                this.CBH15_VALUE09.SetIPopupHelper(new TYAZBJ01C3());
                                break;
                            case "10":
                                this.CBH15_VALUE10 = new TYCodeBox();
                                this.CBH15_VALUE10.Name = "CBH15_VALUE10";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE10);
                                break;
                            case "11":
                                this.CBH15_B2INDX.DummyValue = "TX";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
                                break;
                            case "12":
                                this.TXT15_VALUE12 = new TYTextBox();
                                this.TXT15_VALUE12.Name = "TXT15_VALUE12";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE12);
                                break;
                            case "13":
                                this.TXT15_VALUE13 = new TYTextBox();
                                this.TXT15_VALUE13.Name = "TXT15_VALUE13";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE13);
                                break;
                            case "14":
                                this.TXT15_VALUE14 = new TYTextBox();
                                this.TXT15_VALUE14.Name = "TXT15_VALUE14";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE14);
                                break;
                            case "15":
                                this.TXT15_VALUE15 = new TYTextBox();
                                this.TXT15_VALUE15.Name = "TXT15_VALUE15";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE15);
                                break;
                            case "16":
                                this.TXT15_VALUE16 = new TYTextBox();
                                this.TXT15_VALUE16.Name = "TXT15_VALUE16";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE16);
                                break;
                            case "17":
                                this.TXT15_VALUE17 = new TYTextBox();
                                this.TXT15_VALUE17.Name = "TXT15_VALUE17";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE17);
                                break;
                            case "18":
                                this.TXT15_VALUE18 = new TYTextBox();
                                this.TXT15_VALUE18.Name = "TXT15_VALUE18";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE18);
                                break;
                            case "19":
                                this.TXT15_VALUE19 = new TYTextBox();
                                this.TXT15_VALUE19.Name = "TXT15_VALUE19";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE19);
                                break;
                            case "20":
                                this.TXT15_VALUE20 = new TYTextBox();
                                this.TXT15_VALUE20.Name = "TXT15_VALUE20";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE20);
                                break;
                            case "21":
                                this.TXT15_VALUE21 = new TYTextBox();
                                this.TXT15_VALUE21.Name = "TXT15_VALUE21";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE21);
                                break;
                            case "23":
                                this.TXT15_VALUE23 = new TYTextBox();
                                this.TXT15_VALUE23.Name = "TXT15_VALUE23";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE23);
                                break;
                            case "24":
                                this.CBH15_B2INDX.DummyValue = "BG";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
                                break;
                            case "25":
                                this.CBH15_B2INDX.DummyValue = "BB";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
                                break;
                            case "26":
                                this.CBH15_VALUE26 = new TYCodeBox();
                                this.CBH15_VALUE26.Name = "CBH15_VALUE26";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE26);
                                break;
                            case "27":
                                this.TXT15_VALUE27 = new TYTextBox();
                                this.TXT15_VALUE27.Name = "TXT15_VALUE27";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE27);
                                break;
                            case "28":
                                this.TXT15_VALUE28 = new TYTextBox();
                                this.TXT15_VALUE28.Name = "TXT15_VALUE28";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE28);
                                break;
                            case "29":
                                this.CBH15_VALUE29 = new TYCodeBox();
                                this.CBH15_VALUE29.Name = "CBH15_VALUE29";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE29);
                                this.CBH15_VALUE29.SetIPopupHelper(new TYAZBJ01C2());
                                break;
                            case "30":
                                this.CBH15_B2INDX.DummyValue = "FR";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
                                break;
                            case "31":
                                this.TXT15_VALUE31 = new TYTextBox();
                                this.TXT15_VALUE31.Name = "TXT15_VALUE31";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE31);
                                break;
                            case "32":
                                this.CBH15_VALUE32 = new TYCodeBox();
                                this.CBH15_VALUE32.Name = "CBH15_VALUE32";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE32);
                                break;
                            case "33":
                                this.TXT15_VALUE33 = new TYTextBox();
                                this.TXT15_VALUE33.Name = "TXT15_VALUE33";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE33);
                                break;
                            case "34":
                                this.CBH15_VALUE34 = new TYCodeBox();
                                this.CBH15_VALUE34.Name = "CBH15_VALUE34";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE34);
                                break;
                            case "35":
                                this.CBH15_VALUE35 = new TYCodeBox();
                                this.CBH15_VALUE35.Name = "CBH15_VALUE35";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE35);
                                this.CBH15_VALUE35.SetIPopupHelper(new TYAZBJ01C1());
                                break;
                            case "36":
                                this.TXT15_VALUE36 = new TYTextBox();
                                this.TXT15_VALUE36.Name = "TXT15_VALUE36";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE36);
                                break;
                            case "37":
                                this.CBH15_VALUE37 = new TYCodeBox();
                                this.CBH15_VALUE37.Name = "CBH15_VALUE37";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE37);
                                this.CBH15_VALUE37.SetIPopupHelper(new TYAZBJ01C7());
                                break;
                            case "38":
                                this.CBH15_VALUE38 = new TYCodeBox();
                                this.CBH15_VALUE38.Name = "CBH15_VALUE38";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH15_VALUE38);
                                this.CBH15_VALUE38.CodeText.TextChanged += new EventHandler(this.CBH15_VALUE38_CodeText_TextChanged);
                                this.CBH15_VALUE38.SetIPopupHelper(new TYAZBJ01C8());
                                break;
                            case "40":
                                this.CBH15_B2INDX.DummyValue = "AL";
                                this.PAN10_VLMI5.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
                                break;
                            case "41":
                                this.CBH15_VALUE41 = new TYCodeBox();
                                this.CBH15_VALUE41.Name = "CBH15_VALUE41";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE41);
                                this.CBH15_VALUE41.SetIPopupHelper(new TYAZBJ01C5());
                                break;
                            case "42":
                                this.CBH15_VALUE42 = new TYCodeBox();
                                this.CBH15_VALUE42.Name = "CBH15_VALUE42";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE42);
                                break;
                            case "44":
                                this.CBH15_VALUE44 = new TYCodeBox();
                                this.CBH15_VALUE44.Name = "CBH15_VALUE44";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE44);
                                break;
                            case "46":
                                this.CBH15_B2INDX.DummyValue = "UA";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_B2INDX, this.CBH15_B2INDX.DummyValue);
                                break;
                            case "47":
                                this.TXT15_VALUE47 = new TYTextBox();
                                this.TXT15_VALUE47.Name = "TXT15_VALUE47";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE47);
                                break;
                            case "49":
                                this.TXT15_VALUE49 = new TYTextBox();
                                this.TXT15_VALUE49.Name = "TXT15_VALUE49";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE49);
                                break;
                            case "50":
                                this.CBH15_VALUE50 = new TYCodeBox();
                                this.CBH15_VALUE50.Name = "CBH15_VALUE50";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE50);
                                break;
                            case "51":
                                this.CBH15_VALUE51 = new TYCodeBox();
                                this.CBH15_VALUE51.Name = "CBH15_VALUE51";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.CBH15_VALUE51);
                                break;
                            case "52":
                                this.TXT15_VALUE52 = new TYTextBox();
                                this.TXT15_VALUE52.Name = "TXT15_VALUE52";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE52);
                                break;
                            case "53":
                                this.TXT15_VALUE53 = new TYTextBox();
                                this.TXT15_VALUE53.Name = "TXT10_VALUE53";
                                this.PAN10_VLMI6.AddControl(sCode, sCodeName, this.TXT15_VALUE53);
                                break;
                            default:
                                break;
                        }
                    }


                }
            }


        }
        #endregion


    }
}                     
