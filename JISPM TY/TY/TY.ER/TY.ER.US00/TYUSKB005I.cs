using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.US00
{
    /// <summary>
    /// 입항 및 통관관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.03.07 16:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_938DK025 : 입항관리 조회
    ///  TY_P_US_938DN027 : 입항관리 등록
    ///  TY_P_US_938DP028 : 입항관리 수정
    ///  TY_P_US_938DP029 : 입항관리 삭제
    ///  TY_P_US_938DQ030 : 입항관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_93EDW120 : 입항관리 전체조회
    ///  TY_S_US_938DM026 : 입항관리 조회
    ///  TY_S_US_93EDW121 : 입고관리 전체조회
    ///  TY_S_US_93EDW122 : 입고관리 조회
    ///  TY_S_US_93EDW123 : B/L별 입고관리 전체조회
    ///  TY_S_US_93EDW124 : B/L별 입고관리 조회
    ///  TY_S_US_93EDW125 : B/L별 통관관리 전체조회
    ///  TY_S_US_93EDY126 : B/L별 통관관리 조회
    ///  TY_S_US_93EDY127 : 양수도관리 전체조회
    ///  TY_S_US_93EDY128 : 양수도관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  CSBANGB : 반출구분
    ///  CSGOKJONG : 곡종
    ///  CSHANGCHA : 항차
    ///  CSHWAJU : 화주
    ///  CSSINNM : 관세사
    ///  IBBPSAGO : 반입사고
    ///  IBGOKJONG : 곡종
    ///  IBGUBUN : 입고구분
    ///  IBHANGCHA : 항차
    ///  IBHWAJU : 화주
    ///  IBJNHWAJU : 이전화주
    ///  IBSOSOK : 협회
    ///  IBUNSONG : 운송회사
    ///  IBWONSAN : 원산지
    ///  IHBRANCH : 대리점
    ///  IHGOKJONG1 : 곡종１
    ///  IHGOKJONG2 : 곡종２
    ///  IHGOKJONG3 : 곡종３
    ///  IHHANGCHA : 항차
    ///  IHHWAJU : 화주
    ///  IHLSCODE : 검정사
    ///  IHSODOK : 소독구분
    ///  IHSOSOK : 협회
    ///  IHTAXCODE : 부가세코드
    ///  IHUSEDGUB : 시설사용료구분
    ///  IHWONSAN1 : 원산지１
    ///  IHWONSAN2 : 원산지２
    ///  IHWONSAN3 : 원산지３
    ///  IPGOKJONG : 곡종
    ///  IPHANGCHA : 항차
    ///  IHHALIN : 할인구분
    ///  IHHMHALIN : 화물료할인
    ///  IHHUVSGB : 항운노조모선구분
    ///  IHLMOGB : 유전자변형구분
    ///  IHSUNHU : 선．후항구분
    ///  IHVESLGUB : 내．외항구분
    ///  CSDATE : 통관일자
    ///  CSEDDATE : 종료일자
    ///  CSYJDATE : 반출연장일
    ///  IBBLDATE : B/L분할일자
    ///  IBDATE : 입고일자
    ///  IBHWAKIL : 확정일
    ///  IBJESTDAT : 보관시작일
    ///  IHIPHANG : 입항일자
    ///  IHJAKENDAT : 작업종료일자
    ///  IHJAKSTDAT : 작업시작일자
    ///  IHJUBDAT : 접안일자
    ///  IPIPSTDAT : 입고시작일
    ///  CSBLHSN : HSN
    ///  CSBLMSN : MSN
    ///  CSBLNO : B/L 번호
    ///  CSCHQTY : 출고량
    ///  CSCOSTUS : 감정가($)
    ///  CSCOSTWN : 감정가 원화
    ///  CSHMNO1 : 화물번호1
    ///  CSHMNO2 : 화물번호2
    ///  CSJGQTY : 재 고 량
    ///  CSQTY : 통관량
    ///  CSSEQ : 통관차수
    ///  CSSINNO : 신고번호
    ///  CSSINQTY : 신고수량
    ///  CSYJNUMB : 반출근거
    ///  IBBECHQTY : 분할출고배정량
    ///  IBBEIPQTY : 분할입고배정량
    ///  IBBEJNQTY : 적하중량
    ///  IBBHGUBN : 분할구분
    ///  IBBLHSN : HSN
    ///  IBBLMSN : MSN
    ///  IBBLNO : B/L 번호
    ///  IBBLSEQ : 입고차수
    ///  IBCONTNO : 계약번호
    ///  IBHBLNO : HOUSE B/L
    ///  IBHMNO1 : 화물번호1
    ///  IBHMNO2 : 화물번호2
    ///  IBHWAKQTY : 확정량
    ///  IBHWCHQTY : 분할출고확정량
    ///  IBHWIPQTY : 분할입고확정량
    ///  IBJNBLHSN : 이전HSN
    ///  IBJNBLSEQ : 이전입고차수
    ///  IHBANIPNO : 반입근거번호
    ///  IHBLQTY : B/L수량
    ///  IHBLQTY1 : B/L량1
    ///  IHBLQTY2 : B/L량2
    ///  IHBLQTY3 : B/L량3
    ///  IHGROSS : GROSS TON
    ///  IHHUNEDDAT : 훈증기간ED
    ///  IHHUNSTDAT : 훈증기간ST
    ///  IHHUWKQTY : 항운노조작업량
    ///  IHIANDAT : 이안일자
    ///  IHIANTIM : 이안시간
    ///  IHIPHTIM : 입항시간
    ///  IHJAKENTIM : 작업종료시간
    ///  IHJAKSTTIM : 작업시작시간
    ///  IHJUBTIM : 접안시간
    ///  IHJUKHANO : 적하목록번호
    ///  IHJUKHANO2 : 선사부호
    ///  IHSHDATE : 모선스케줄일자
    ///  IHSHSEQ : 모선스케줄순번
    ///  IPIPHAP : 자동화입고누계
    ///  IPTOTQTY : 확정량
    /// </summary>
    public partial class TYUSKB005I : TYBase
    {
        private string fsTAB_GUBUN = string.Empty;

        private string fsWK_GUBUN1 = "NEW";
        private string fsWK_GUBUN2 = "NEW";
        private string fsWK_GUBUN3 = "NEW";
        private string fsWK_GUBUN4 = "NEW";
        private string fsWK_GUBUN5 = "NEW";

        private string fsBLSAVEGB1 = string.Empty;
        private string fsBLSAVEGB2 = string.Empty;
        private string fsBLSAVEGB3 = string.Empty;
        private string fsBLSAVEGB4 = string.Empty;
        private string fsBLSAVEGB5 = string.Empty;
        private string fsBLSAVEGB6 = string.Empty;

        private string fsCSSAVEGB1 = string.Empty;

        private string fsYNSAVEGB1 = string.Empty;
        private string fsYNSAVEGB2 = string.Empty;
        private string fsYNSAVEGB3 = string.Empty;
        private string fsYNSAVEGB4 = string.Empty;
        private string fsYNSAVEGB5 = string.Empty;
        private string fsYNSAVEGB6 = string.Empty;

        #region Description : 쿠키 값

        private string fsHANGCHA = string.Empty;
        private string fsGOKJONG = string.Empty;
        private string fsHWAJU = string.Empty;
        private string fsBLNO = string.Empty;
        private string fsBLMSN = string.Empty;
        private string fsBLHSN = string.Empty;
        private string fsIPSEQ = string.Empty;
        private string fsIPDATE = string.Empty;
        private string fsHMNO1 = string.Empty;
        private string fsHMNO2 = string.Empty;
        private string fsCSDTAE = string.Empty;
        private string fsCSSEQ = string.Empty;
        private string fsYSHWAJU = string.Empty;
        private string fsYSDATE = string.Empty;
        private string fsYSSEQ = string.Empty;
        private string fsYDSEQ = string.Empty;

        #endregion

        #region Description : B/L별 입고관리 변수
        // B/L별 입고관리
        private string fsIBBPSAGO = string.Empty;
        private string fsIBUNSONG = "900";

        private double fdPRE_IBBEIPQTY = 0;
        private double fdPRE_IBHWIPQTY = 0;

        private string fsPRE_IBJNHWAJU = string.Empty;   // 수정 전 이전화주
        private string fsPRE_IBJNBLHSN = string.Empty;   // 수정 전 이전HSN
        private string fsPRE_IBJNBLSEQ = string.Empty;   // 수정 전 이전입고차수

        private double fdIBBECHQTY = 0;
        private double fdIBHWCHQTY = 0;
        private double fdIBBECHQTY2 = 0;
        private double fdIBHWCHQTY2 = 0;

        private double fdJBBECHQTY = 0;
        private double fdJBHWCHQTY = 0;
        private double fdJBBECHQTY2 = 0;
        private double fdJBHWCHQTY2 = 0;

        private double fdIBBEJNQTY = 0;
        private double fdIBHWAKQTY = 0;

        private double fdJGBECHQTY = 0;
        private double fdJGHWCHQTY = 0;
        private double fdJGBECHQTY2 = 0;
        private double fdJGHWCHQTY2 = 0;

        // B/L별 재고
        private double fdBL_JBBEJNQTY = 0;  // 배정량
        private double fdBL_JBHWAKQTY = 0;  // 확정량
        private double fdBL_JBYDQTY = 0;  // 양도량
        private double fdBL_JBYSQTY = 0;  // 양수량
        private double fdBL_JBYSYDQTY = 0;  // 양수분양도량
        private double fdBL_JBCSQTY = 0;  // 통관수량
        private double fdBL_JBCHQTY = 0;  // 출고수량
        private double fdBL_JBYSCHQTY = 0;  // 양수출고량
        private double fdBL_JBJANQTY = 0;  // 잔량
        private double fdBL_JBCSJANQTY = 0;  // 통관잔량
        private double fdBL_JBYSJANQTY = 0;  // 양수출고잔량
        private double fdBL_JBJEGOQTY = 0;  // 재고량
        private double fdBL_JBBEIPQTY = 0;  // 분할배정입고량
        private double fdBL_JBHWIPQTY = 0;  // 분할확정입고량
        private double fdBL_JBHWCHQTY = 0;  // 분할확정출고량

        // 이전화주 용 변수(등록)
        private double fdBL_JBBEJNQTY2 = 0;  // 배정량
        private double fdBL_JBHWAKQTY2 = 0;  // 확정량
        private double fdBL_JBYDQTY2 = 0;  // 양도량
        private double fdBL_JBYSQTY2 = 0;  // 양수량
        private double fdBL_JBYSYDQTY2 = 0;  // 양수분양도량
        private double fdBL_JBCSQTY2 = 0;  // 통관수량
        private double fdBL_JBCHQTY2 = 0;  // 출고수량
        private double fdBL_JBYSCHQTY2 = 0;  // 양수출고량
        private double fdBL_JBJANQTY2 = 0;  // 잔량
        private double fdBL_JBCSJANQTY2 = 0;  // 통관잔량
        private double fdBL_JBJEGOQTY2 = 0;  // 재고량
        private double fdBL_JBBEIPQTY2 = 0;  // 분할배정입고량
        private double fdBL_JBHWIPQTY2 = 0;  // 분할확정입고량
        private double fdBL_JBHWCHQTY2 = 0;  // 분할확정출고량     

        // 이전화주 용 변수(삭제)
        private double fdBL_JBBEJNQTY3 = 0;  // 배정량
        private double fdBL_JBHWAKQTY3 = 0;  // 확정량
        private double fdBL_JBYDQTY3 = 0;  // 양도량
        private double fdBL_JBYSQTY3 = 0;  // 양수량
        private double fdBL_JBYSYDQTY3 = 0;  // 양수분양도량
        private double fdBL_JBCSQTY3 = 0;  // 통관수량
        private double fdBL_JBCHQTY3 = 0;  // 출고수량
        private double fdBL_JBYSCHQTY3 = 0;  // 양수출고량
        private double fdBL_JBJANQTY3 = 0;  // 잔량
        private double fdBL_JBCSJANQTY3 = 0;  // 통관잔량
        private double fdBL_JBJEGOQTY3 = 0;  // 재고량
        private double fdBL_JBBEIPQTY3 = 0;  // 분할배정입고량
        private double fdBL_JBHWIPQTY3 = 0;  // 분할확정입고량
        private double fdBL_JBHWCHQTY3 = 0;  // 분할확정출고량 

        // 화주별 재고
        private double fdBL_JGBEJNQTY = 0;  // 배정량
        private double fdBL_JGHWAKQTY = 0;  // 확정량
        private double fdBL_JGYDQTY = 0;  // 양도량
        private double fdBL_JGYSQTY = 0;  // 양수량
        private double fdBL_JGYSYDQTY = 0;  // 양수분양도량
        private double fdBL_JGCSQTY = 0;  // 통관수량
        private double fdBL_JGCHQTY = 0;  // 출고수량
        private double fdBL_JGYSCHQTY = 0;  // 양수출고량
        private double fdBL_JGJANQTY = 0;  // 잔량
        private double fdBL_JGCSJANQTY = 0;  // 통관잔량
        private double fdBL_JGYSJANQTY = 0;  // 양수출고잔량
        private double fdBL_JGJEGOQTY = 0;  // 재고량
        private double fdBL_JGBEIPQTY = 0;   // 분할배정입고량
        private double fdBL_JGHWIPQTY = 0;   // 분할확정입고량
        private double fdBL_JGHWCHQTY = 0;   // 분할확정출고량

        private double fdBL_JGHWAKQTY2 = 0;  // 확정량
        private double fdBL_JGYDQTY2 = 0;  // 양도량
        private double fdBL_JGYSQTY2 = 0;  // 양수량
        private double fdBL_JGYSYDQTY2 = 0;  // 양수분양도량
        private double fdBL_JGCSQTY2 = 0;  // 통관수량
        private double fdBL_JGCHQTY2 = 0;  // 출고수량
        private double fdBL_JGYSCHQTY2 = 0;  // 양수출고량
        private double fdBL_JGJANQTY2 = 0;  // 잔량
        private double fdBL_JGCSJANQTY2 = 0;  // 통관잔량
        private double fdBL_JGJEGOQTY2 = 0;  // 재고량
        private double fdBL_JGHWIPQTY2 = 0;   // 분할확정입고량

        private double fdBL_JGHWAKQTY3 = 0;  // 확정량
        private double fdBL_JGYDQTY3 = 0;  // 양도량
        private double fdBL_JGYSQTY3 = 0;  // 양수량
        private double fdBL_JGYSYDQTY3 = 0;  // 양수분양도량
        private double fdBL_JGCSQTY3 = 0;  // 통관수량
        private double fdBL_JGCHQTY3 = 0;  // 출고수량
        private double fdBL_JGYSCHQTY3 = 0;  // 양수출고량
        private double fdBL_JGJANQTY3 = 0;  // 잔량
        private double fdBL_JGCSJANQTY3 = 0;  // 통관잔량
        private double fdBL_JGJEGOQTY3 = 0;  // 재고량
        private double fdBL_JGHWIPQTY3 = 0;   // 분할확정입고량

        private string fsIBDATE = string.Empty;
        private string fsIBHMNO1 = string.Empty;
        private string fsIBHMNO2 = string.Empty;

        private string fsPRE_IBDATE = string.Empty;
        private string fsPRE_IBHMNO1 = string.Empty;
        private string fsPRE_IBHMNO2 = string.Empty;

        // 적화목록
        private double fdBL_JKHWAKQTY = 0;  // 확정량
        #endregion

        #region Description : 통관관리 변수
        // 통관관리
        private double fdCSQTY = 0;
        private double fdCJEGOQTY = 0;

        private double fdJBCSQTY = 0;
        private double fdJBCSJANQTY = 0;
        private double fdJGCSQTY = 0;
        private double fdJGCSJANQTY = 0;

        private string fsJBSOSOK = string.Empty; 
        private string fsJBJESTDAT = string.Empty;
        private string fsJBCONTNO = string.Empty;
        private string fsIBWONSAN = string.Empty;
        
        // 통관관리 저장, 삭제 체크
        private double fdCS_JBBEJNQTY = 0; //  1 = 배정량
        private double fdCS_JBHWAKQTY = 0; //  2 = 확정량 
        private double fdCS_JBYDQTY = 0; //  3 = 양도량
        private double fdCS_JBYSQTY = 0; //  4 = 양수량
        private double fdCS_JBCSQTY = 0; //  5 = 통관수량
        private double fdCS_JBCSJANQTY = 0; //  6 = 통관잔량
        private double fdCS_JBCHQTY = 0; //  7 = 출고수량
        private double fdCS_JBJEGOQTY = 0; //  8 = 재고량
        private double fdCS_JGBEJNQTY = 0; //  1 = 배정량
        private double fdCS_JGHWAKQTY = 0; //  2 = 확정량
        private double fdCS_JGYDQTY = 0; //  3 = 양도량
        private double fdCS_JGYSQTY = 0; //  4 = 양수량
        private double fdCS_JGYSYDQTY = 0; //  5 = 양수분양도량
        private double fdCS_JGCSQTY = 0; //  6 = 통관수량
        private double fdCS_JGCHQTY = 0; //  7 = 출고수량
        private double fdCS_JGYSCHQTY = 0; //  8 = 양수출고량 
        private double fdCS_JGJANQTY = 0; //  9 = 잔량 
        private double fdCS_JGCSJANQTY = 0; // 10 = 통관잔량 
        private double fdCS_JGYSJANQTY = 0; // 11 = 양수출고잔량
        private double fdCS_JGJEGOQTY = 0; // 12 = 재고량 

        private double fdCS_JCJEGOQTY = 0;
        private double fdCS_JCYSQTY = 0;
        private double fdCS_JCYDQTY = 0;
        private double fdCS_JCYSYDQTY = 0;
        private double fdCS_JCCHQTY = 0;
        private double fdCS_JCYSCHQTY = 0;

        #endregion

        #region Description : 양수도관리 변수
        //양수도관리 
        private string fsSIKBAEL = "";
        private double fdYNQTY = 0;     
        private double fdYNYSYDQTY = 0; 
        private string fsYNYSYDHWAJU = "";
        private string fsYNYSYDDATE = "";
        private string fsYNYSYDSEQ = "0";

        private string fsJCYNHWAJU = "";
        private string fsJCYDHWAJU = "";
        private string fsJCYSDATE = ""; 
        private string fsJCYSSEQ = "";  
        private string fsJCYDSEQ = "";  
        private string fsJCHWAJU = "";  
        
        private double fdOWN_YNYSYDQTY = 0;

        // 양도화주
        private double fdYNYD_JBYDQTY = 0; // 양도량
        private double fdYNYD_JBJANQTY = 0; // 잔량 
        private double fdYNYD_JBJEGOQTY = 0; // 재고량 
        private double fdYNYD_JBYSQTY = 0; // 양수량
        private double fdYNYD_JBYSJANQTY = 0; // 양수출고잔량
        private double fdYNYD_JBYSYDQTY = 0; // 양수분양도량 

        private double fdYNYD_JCYDQTY = 0; // 양도량 
        private double fdYNYD_JCJEGOQTY = 0; // 재고량 
        private double fdYNYD_JCYSQTY = 0; // 양수량
        private string fsYNYD_JCNUMBER = ""; // 출고순번
        private double fdYNYD_JCYSYDQTY = 0; // 양수분양도량 

        private double fdYNYD_JGYDQTY = 0; // 양도량
        private double fdYNYD_JGJANQTY = 0; // 잔량 
        private double fdYNYD_JGJEGOQTY = 0; // 재고량
        private double fdYNYD_JGYSQTY = 0; // 양수량
        private double fdYNYD_JGYSJANQTY = 0; // 양수출고잔량 
        private double fdYNYD_JGYSYDQTY = 0; // 양수분양도량

        private string fsYNYS_JCWONSAN = ""; // 원산지

        // 양수화주
        private string fsYNYS_JBSOSOK = ""; // 14 = 협회
        private string fsYNYS_JBJESTDAT = "0"; // 15 = 보관시작일
        private string fsYNYS_JBCONTNO = "0"; // 16 = 계약번호

        private double fdYNYS_JBYDQTY = 0; // 양도량
        private double fdYNYS_JBJANQTY = 0; // 잔량
        private double fdYNYS_JBJEGOQTY = 0; // 재고량
        private double fdYNYS_JBYSQTY = 0; // 양수량 
        private double fdYNYS_JBYSJANQTY = 0; // 양수출고잔량 
        private double fdYNYS_JBYSYDQTY = 0; // 양수분양도량

        private double fdYNYS_JCYDQTY = 0; // 양도량
        private double fdYNYS_JCJEGOQTY = 0; // 재고량 
        private double fdYNYS_JCYSQTY = 0; // 양수량 
        private string fsYNYS_JCNUMBER = ""; // 출고순번 
        private double fdYNYS_JCYSYDQTY = 0; // 양수분양도량

        private double fdYNYS_JGYDQTY = 0; // 양도량
        private double fdYNYS_JGJANQTY = 0; // 잔량
        private double fdYNYS_JGJEGOQTY = 0; // 재고량
        private double fdYNYS_JGYSQTY = 0; // 양수량
        private double fdYNYS_JGYSJANQTY = 0; // 양수출고잔량 
        private double fdYNYS_JGYSYDQTY = 0; // 양수분양도량

        private string fsYNYS_WONSAN = string.Empty; //원산지
        #endregion

        #region Description : 폼 로드
        public TYUSKB005I()
        {
            InitializeComponent();
        }

        private void TYUSKB005I_Load(object sender, System.EventArgs e)
        {
            // 입항관리 저장,삭제 체크
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            // 입고관리 저장,삭제 체크
            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);

            // B/L별 입고관리 저장,삭제 체크
            this.BTN64_SAV.ProcessCheck += new TButton.CheckHandler(BTN64_SAV_ProcessCheck);
            this.BTN64_REM.ProcessCheck += new TButton.CheckHandler(BTN64_REM_ProcessCheck);

            // 통관관리 저장,삭제 체크
            this.BTN65_SAV.ProcessCheck += new TButton.CheckHandler(BTN65_SAV_ProcessCheck);
            this.BTN65_REM.ProcessCheck += new TButton.CheckHandler(BTN65_REM_ProcessCheck);

            // 양수도관리 저장,삭제 체크
            this.BTN66_SAV.ProcessCheck += new TButton.CheckHandler(BTN66_SAV_ProcessCheck);
            this.BTN66_REM.ProcessCheck += new TButton.CheckHandler(BTN66_REM_ProcessCheck);

            fsTAB_GUBUN = "INVENTORY";
            DTP01_IHIPHANG2.SetReadOnly(true);
            CBH01_CSHWAJU.SetReadOnly(true);
            CBH01_IBJNHWAJU.SetReadOnly(true);
            SetStartingFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼(공통)
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (fsTAB_GUBUN == "INVENTORY")          //INVENTORY
            {
                UP_INVENTORY_Search();
            }
            else if (fsTAB_GUBUN == "USIIPHAF")     //입항관리
            {
                
                UP_USIIPHAF_Search();
                dt = FPS91_TY_S_US_93EDW120.GetDataSourceInclude(TSpread.TActionType.All);
                if (dt.Rows.Count > 0)
                {
                    SetFocus(FPS91_TY_S_US_93EDW120);
                }
            }
            else if (fsTAB_GUBUN == "USIIPGOF")     //입고관리
            {
                UP_USIIPGOF_Search();

                dt = FPS91_TY_S_US_93EDW121.GetDataSourceInclude(TSpread.TActionType.All);
                if (dt.Rows.Count > 0)
                {
                    SetFocus(FPS91_TY_S_US_93EDW121);
                }
            }
            else if (fsTAB_GUBUN == "USIIPBLF")     //B/L별 입고관리
            {
                UP_USIIPBLF_Search();

                dt = FPS91_TY_S_US_93EDW123.GetDataSourceInclude(TSpread.TActionType.All);
                if (dt.Rows.Count > 0)
                {
                    SetFocus(FPS91_TY_S_US_93EDW123);
                }
            }
            else if (fsTAB_GUBUN == "USICUSTF")     //통관관리
            {
                UP_USICUSTF_Search();

                dt = FPS91_TY_S_US_95FF7562.GetDataSourceInclude(TSpread.TActionType.All);
                if (dt.Rows.Count > 0)
                {
                    SetFocus(FPS91_TY_S_US_95FF7562);
                }
            }
            else if (fsTAB_GUBUN == "USIYANGNF")    // 양수도관리
            {   
                UP_USIYANGNF_Search();
                
                dt = FPS91_TY_S_US_93EDY127.GetDataSourceInclude(TSpread.TActionType.All);
                if (dt.Rows.Count > 0)
                {
                    SetFocus(FPS91_TY_S_US_93EDY127);
                }
            }
        }
        #endregion

        #region Description : INVENTORY
        #region Description : INVENTORY 조회
        private void UP_INVENTORY_Search()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_97JGY056",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString(),
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_97JGZ058.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_US_97JGZ058.ActiveSheet.RowCount; i++)
                {
                    // 입항관리
                    if (this.FPS91_TY_S_US_97JGZ058.GetValue(i, "IPHA_DATA").ToString() == "IPHA")
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 6].Text = "";
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 6].BackColor = Color.Thistle;
                    }

                    // 입고관리
                    if (this.FPS91_TY_S_US_97JGZ058.GetValue(i, "IPGO_DATA").ToString() == "IPGO")
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 7].Text = "";
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 7].BackColor = Color.Thistle;
                    }

                    // B/L별 입고관리
                    if (Convert.ToDouble(this.FPS91_TY_S_US_97JGZ058.GetValue(i, "IPBL_DATA").ToString()) == 0 &&
                        Convert.ToDouble(this.FPS91_TY_S_US_97JGZ058.GetValue(i, "IBBEJNQTY").ToString()) != 0)
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 13].Text = "";
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 13].BackColor = Color.Thistle;
                    }
                    else if (Convert.ToDouble(this.FPS91_TY_S_US_97JGZ058.GetValue(i, "IPBL_DATA").ToString()) == 0 &&
                        Convert.ToDouble(this.FPS91_TY_S_US_97JGZ058.GetValue(i, "IBBEJNQTY").ToString()) == 0)
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 13].Text = "";
                    }
                    else
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 13].BackColor = Color.SkyBlue;
                    }

                    // 통관관리
                    if (Convert.ToDouble(this.FPS91_TY_S_US_97JGZ058.GetValue(i, "CUST_DATA").ToString()) != 0)
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 14].BackColor = Color.SkyBlue;
                    }
                    else
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 14].Text = "";

                        if (Convert.ToDouble(this.FPS91_TY_S_US_97JGZ058.GetValue(i, "IBBEJNQTY").ToString()) != 0)
                        {   
                            // 특정 ROW 색깔 입히기
                            this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 14].BackColor = Color.Thistle;
                        }
                    }

                    // 양수도관리
                    if (Convert.ToDouble(this.FPS91_TY_S_US_97JGZ058.GetValue(i, "YANG_DATA").ToString()) == 0)
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 16].Text = "";
                    }
                    else
                    {
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 16].Text = "";
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_US_97JGZ058.ActiveSheet.Cells[i, 16].BackColor = Color.Thistle;
                    }
                }
            }
        }
        #endregion

        #region Description : INVENTORY 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_US_97JGZ058_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsHANGCHA = this.FPS91_TY_S_US_97JGZ058.GetValue("HANGCHA").ToString();
            fsGOKJONG = this.FPS91_TY_S_US_97JGZ058.GetValue("GOKJONG").ToString();
            fsHWAJU = this.FPS91_TY_S_US_97JGZ058.GetValue("HWAJU").ToString();

            if (e.Column.ToString() == "6")
            {
                this.tabControl1.SelectedIndex = 1;

                this.CBH01_IHHANGCHA.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HANGCHA").ToString());

                // 입항관리 확인
                UP_USIIPHAF_Run();

                // 입항관리 조회
                UP_USIIPHAF_TAB_Search();
            }
            else if (e.Column.ToString() == "7")
            {
                this.tabControl1.SelectedIndex = 2;

                this.CBH01_IPHANGCHA.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HANGCHA").ToString());
                this.CBH01_IPGOKJONG.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("GOKJONG").ToString());

                // 입고관리 확인
                UP_USIIPGOF_Run();

                // 입고관리 조회
                UP_USIIPGOF_TAB_Search();
            }
            else if (e.Column.ToString() == "13")
            {
                this.tabControl1.SelectedIndex = 3;

                this.CBH01_IBHANGCHA.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HANGCHA").ToString());
                this.CBH01_IBGOKJONG.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("GOKJONG").ToString());
                this.CBH01_IBHWAJU.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HWAJU").ToString());

                // B/L별 입고관리 조회
                UP_USIIPBLF_TAB_Search();

                UP_IBBPSAGO_Select();
            }
            else if (e.Column.ToString() == "14")
            {
                this.tabControl1.SelectedIndex = 4;

                this.CBH01_CSHANGCHA.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HANGCHA").ToString());
                this.CBH01_CSGOKJONG.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("GOKJONG").ToString());
                this.CBH01_CSHWAJU.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HWAJU").ToString());

                // 통관관리 조회
                UP_USICUSTF_TAB_Search();
            }
            else if (e.Column.ToString() == "16")
            {
                this.tabControl1.SelectedIndex = 5;

                this.CBH01_YNHANGCHA.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HANGCHA").ToString());
                this.CBH01_YNGOKJONG.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("GOKJONG").ToString());
                this.CBH01_YNHWAJU.SetValue(this.FPS91_TY_S_US_97JGZ058.GetValue("HWAJU").ToString());

                // 양수도관리 조회
                UP_USIYANGNF_TAB_Search();
            }
        }
        #endregion
        #endregion

        #region Description : 입항관리

        #region Description : 입항관리 전체조회
        private void UP_USIIPHAF_Search()
        {
            this.FPS91_TY_S_US_93EDW120.Initialize();
            this.FPS91_TY_S_US_938DM026.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_938DK025",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_93EDW120.SetValue(dt);

            //if (dt.Rows.Count > 0)
            //{

            //    Timer tmr = new Timer();

            //    tmr.Tick += delegate
            //    {
            //        tmr.Stop();
            //        this.SetFocus(this.FPS91_TY_S_US_93EDW120);
            //    };

            //    tmr.Interval = 100;
            //    tmr.Start();
            //}
        }
        #endregion

        #region Description : 입항관리 조회
        private void UP_USIIPHAF_TAB_Search()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_941ET214",
                this.CBH01_IHHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_938DM026.SetValue(dt);
        }
        #endregion

        #region Description : 입항관리 확인
        private void UP_USIIPHAF_Run()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_941ET214",
                this.CBH01_IHHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {   
                CBH01_IHHANGCHA.SetValue(dt.Rows[0]["IHHANGCHA"].ToString());

                CBO01_IHHALIN.SetValue(dt.Rows[0]["IHHALIN"].ToString());
                TXT01_IHGROSS.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHGROSS"].ToString().Trim())));
                MTB01_IHIPHANG.SetValue(dt.Rows[0]["IHIPHANG"].ToString());
                MTB01_IHIPHTIM.SetValue(dt.Rows[0]["IHIPHTIM"].ToString());
                MTB01_IHJUBDAT.SetValue(dt.Rows[0]["IHJUBDAT"].ToString());
                MTB01_IHJUBTIM.SetValue(dt.Rows[0]["IHJUBTIM"].ToString());
                MTB01_IHJAKSTDAT.SetValue(dt.Rows[0]["IHJAKSTDAT"].ToString());
                MTB01_IHJAKSTTIM.SetValue(dt.Rows[0]["IHJAKSTTIM"].ToString());
                MTB01_IHJAKENDAT.SetValue(dt.Rows[0]["IHJAKENDAT"].ToString());
                MTB01_IHJAKENTIM.SetValue(dt.Rows[0]["IHJAKENTIM"].ToString());
                MTB01_IHIANDAT.SetValue(dt.Rows[0]["IHIANDAT"].ToString());
                MTB01_IHIANTIM.SetValue(dt.Rows[0]["IHIANTIM"].ToString());
                MTB01_IHHUNSTDAT.SetValue(dt.Rows[0]["IHHUNSTDAT"].ToString());
                MTB01_IHHUNEDDAT.SetValue(dt.Rows[0]["IHHUNEDDAT"].ToString());

                CBH01_IHSOSOK.SetValueText(dt.Rows[0]["IHSOSOK"].ToString(), dt.Rows[0]["IHSOSOKNM"].ToString());
                CBH01_IHHWAJU.SetValue(dt.Rows[0]["IHHWAJU"].ToString());
                TXT01_IHJUKHANO.SetValue(dt.Rows[0]["IHJUKHANO"].ToString());
                TXT01_IHBANIPNO.SetValue(dt.Rows[0]["IHBANIPNO"].ToString());
                CBH01_IHGOKJONG1.SetValue(dt.Rows[0]["IHGOKJONG1"].ToString());
                CBH01_IHWONSAN1.SetValue(dt.Rows[0]["IHWONSAN1"].ToString());
                TXT01_IHBLQTY1.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHBLQTY1"].ToString().Trim())));
                CBH01_IHGOKJONG2.SetValue(dt.Rows[0]["IHGOKJONG2"].ToString());
                CBH01_IHWONSAN2.SetValue(dt.Rows[0]["IHWONSAN2"].ToString());
                TXT01_IHBLQTY2.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHBLQTY2"].ToString().Trim())));
                CBH01_IHGOKJONG3.SetValue(dt.Rows[0]["IHGOKJONG3"].ToString());
                CBH01_IHWONSAN3.SetValue(dt.Rows[0]["IHWONSAN3"].ToString());
                TXT01_IHBLQTY3.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHBLQTY3"].ToString().Trim())));
                TXT01_IHBLQTY_HAP.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHBLQTY_HAP"].ToString().Trim())));

                CBH01_IHBRANCH.SetValue(dt.Rows[0]["IHBRANCH"].ToString());
                CBO01_IHSUNHU.SetValue(dt.Rows[0]["IHSUNHU"].ToString());
                CBO01_IHSODOK.SetValue(dt.Rows[0]["IHSODOK"].ToString());
                CBO01_IHVESLGUB.SetValue(dt.Rows[0]["IHVESLGUB"].ToString());
                CBH01_IHLSCODE.SetValue(dt.Rows[0]["IHLSCODE"].ToString());
                CBO01_IHLMOGB.SetValue(dt.Rows[0]["IHLMOGB"].ToString());
                CBO01_IHHUVSGB.SetValue(dt.Rows[0]["IHHUVSGB"].ToString());
                TXT01_IHHUWKQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHHUWKQTY"].ToString().Trim())));
                CBO01_IHUSEDGUB.SetValue(dt.Rows[0]["IHUSEDGUB"].ToString());
                CBO01_IHHMHALIN.SetValue(dt.Rows[0]["IHHMHALIN"].ToString());
                CBH01_IHTAXCODE.SetValue(dt.Rows[0]["IHTAXCODE"].ToString());
                DTP01_IHSHDATE.SetValue(dt.Rows[0]["IHSHDATE"].ToString());
                TXT01_IHSHSEQ.SetValue(dt.Rows[0]["IHSHSEQ"].ToString());

                fsWK_GUBUN1 = "UPT";
                UP_FieldVisible("UPT");

            }
            else
            {   
                UP_FieldClear("입항");
            }

            // FOCUS
            Timer tmr = new Timer();

            tmr.Tick += delegate
            {
                tmr.Stop();
                this.TXT01_IHGROSS.Focus();
            };

            tmr.Interval = 100;
            tmr.Start();

            Set_Cookie();
        }
        #endregion

        #region Description : 입항관리 조회 그리드 더블클릭
        private void FPS91_TY_S_US_938DM026_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_IHHANGCHA.SetValue(this.FPS91_TY_S_US_938DM026.GetValue("IHHANGCHA").ToString());

            UP_USIIPHAF_Run();
        }
        #endregion

        #region Description : 입항관리 전체조회 그리드 더블클릭
        private void FPS91_TY_S_US_93EDW120_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {   
            this.CBH01_IHHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW120.GetValue("IHHANGCHA").ToString());

            UP_USIIPHAF_TAB_Search();

            UP_USIIPHAF_Run();
        }
        private void FPS91_TY_S_US_93EDW120_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_IHHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW120.GetValue("IHHANGCHA").ToString());

                UP_USIIPHAF_TAB_Search();

                UP_USIIPHAF_Run();
            }
        }
        #endregion

        #region Description : 입항관리 신규버튼
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            fsWK_GUBUN1 = "NEW";
            UP_FieldClear("입항");
            UP_FieldVisible("NEW");
            UP_Set_KeyFocus();
            FPS91_TY_S_US_938DM026.Initialize();
        }
        #endregion

        #region Description : 입항관리 저장버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (fsWK_GUBUN1 == "NEW")   //등록
            {   
                this.DbConnector.Attach("TY_P_US_938DN027",
                                        this.CBH01_IHHANGCHA.GetValue().ToString(),
                                        this.CBO01_IHHALIN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHGROSS.GetValue().ToString()),
                                        Get_Date(this.MTB01_IHIPHANG.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHIPHTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHJUBDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHJUBTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHJAKENDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHIANDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHIANTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        this.CBH01_IHSOSOK.GetValue().ToString(),
                                        this.CBH01_IHHWAJU.GetValue().ToString(),
                                        this.TXT01_IHJUKHANO.GetValue().ToString(),
                                        this.TXT01_IHBANIPNO.GetValue().ToString(),
                                        this.CBH01_IHGOKJONG1.GetValue().ToString(),
                                        this.CBH01_IHWONSAN1.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY1.GetValue().ToString()),
                                        this.CBH01_IHGOKJONG2.GetValue().ToString(),
                                        this.CBH01_IHWONSAN2.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY2.GetValue().ToString()),
                                        this.CBH01_IHGOKJONG3.GetValue().ToString(),
                                        this.CBH01_IHWONSAN3.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY3.GetValue().ToString()),
                                        this.CBH01_IHBRANCH.GetValue().ToString(),
                                        this.CBO01_IHSUNHU.GetValue().ToString(),
                                        this.CBO01_IHSODOK.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY_HAP.GetValue().ToString()),
                                        this.CBO01_IHVESLGUB.GetValue().ToString(),
                                        this.CBO01_IHHUVSGB.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHHUWKQTY.GetValue().ToString()),
                                        this.CBO01_IHUSEDGUB.GetValue().ToString(),
                                        this.CBO01_IHHMHALIN.GetValue().ToString(),
                                        this.CBH01_IHTAXCODE.GetValue().ToString(),
                                        Get_Date(this.MTB01_IHHUNSTDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        Get_Date(this.MTB01_IHHUNEDDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.CBH01_IHLSCODE.GetValue().ToString(),
                                        this.CBO01_IHLMOGB.GetValue().ToString(),
                                        Get_Date(this.DTP01_IHSHDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.TXT01_IHSHSEQ.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );
            }
            else if (fsWK_GUBUN1 == "UPT")  //수정
            {   
                this.DbConnector.Attach("TY_P_US_938DP028",
                                        this.CBO01_IHHALIN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHGROSS.GetValue().ToString()),
                                        Get_Date(this.MTB01_IHIPHANG.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHIPHTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHJUBDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHJUBTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHJAKENDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        Get_Date(this.MTB01_IHIANDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.MTB01_IHIANTIM.GetValue().ToString().Replace(":", "").Trim(),
                                        this.CBH01_IHSOSOK.GetValue().ToString(),
                                        this.CBH01_IHHWAJU.GetValue().ToString(),
                                        this.TXT01_IHJUKHANO.GetValue().ToString(),
                                        this.TXT01_IHBANIPNO.GetValue().ToString(),
                                        this.CBH01_IHGOKJONG1.GetValue().ToString(),
                                        this.CBH01_IHWONSAN1.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY1.GetValue().ToString()),
                                        this.CBH01_IHGOKJONG2.GetValue().ToString(),    //20
                                        this.CBH01_IHWONSAN2.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY2.GetValue().ToString()),
                                        this.CBH01_IHGOKJONG3.GetValue().ToString(),
                                        this.CBH01_IHWONSAN3.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY3.GetValue().ToString()),
                                        this.CBH01_IHBRANCH.GetValue().ToString(),
                                        this.CBO01_IHSUNHU.GetValue().ToString(),
                                        this.CBO01_IHSODOK.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHBLQTY_HAP.GetValue().ToString()),
                                        this.CBO01_IHVESLGUB.GetValue().ToString(),     //30
                                        this.CBO01_IHHUVSGB.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IHHUWKQTY.GetValue().ToString()),
                                        this.CBO01_IHUSEDGUB.GetValue().ToString(),
                                        this.CBO01_IHHMHALIN.GetValue().ToString(),
                                        this.CBH01_IHTAXCODE.GetValue().ToString(),
                                        Get_Date(this.MTB01_IHHUNSTDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        Get_Date(this.MTB01_IHHUNEDDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.CBH01_IHLSCODE.GetValue().ToString(),
                                        this.CBO01_IHLMOGB.GetValue().ToString(),
                                        Get_Date(this.DTP01_IHSHDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),  //40
                                        this.TXT01_IHSHSEQ.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        this.CBH01_IHHANGCHA.GetValue().ToString()
                                        );

                
            }

            // 선박스케줄 업데이트
            this.DbConnector.Attach("TY_P_US_A3UEJ166",
                                    Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString()),
                                    Get_Date(this.MTB01_IHJAKENDAT.GetValue().ToString()),
                                    Get_Date(this.MTB01_IHJUBDAT.GetValue().ToString()),
                                    this.MTB01_IHJUBTIM.GetValue().ToString().Replace(":", "").Trim(),
                                    Get_Date(this.MTB01_IHIPHANG.GetValue().ToString()),
                                    this.MTB01_IHIPHTIM.GetValue().ToString().Replace(":", "").Trim(),
                                    CBH01_IHHANGCHA.GetValue().ToString(),
                                    CBH01_IHBRANCH.GetValue().ToString(),
                                    CBH01_IHLSCODE.GetValue().ToString(),
                                    Get_Date(DTP01_IHSHDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                                    TXT01_IHSHSEQ.GetValue().ToString());

            this.DbConnector.ExecuteTranQueryList();

            fsWK_GUBUN1 = "UPT";
            UP_USIIPHAF_Search();
            UP_USIIPHAF_TAB_Search();
            UP_USIIPHAF_Run();
            this.ShowMessage("TY_M_GB_23NAD873");
            this.TXT01_IHGROSS.Focus();
            Set_Cookie();
        }
        #endregion

        #region Description : 입항관리 저장 체크
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRtn;

            // 날짜 체크
            if (Get_Date(MTB01_IHIPHANG.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IHIPHANG.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("입항일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IHIPHANG);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IHIPHANG.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IHIPHANG);
                        return;
                    }
                }
            }
            if (Get_Date(MTB01_IHJUBDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IHJUBDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("접안일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IHJUBDAT);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IHJUBDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("접안일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IHJUBDAT);
                        return;
                    }
                }
            }
            if (Get_Date(MTB01_IHJAKSTDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IHJAKSTDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("작업시작일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IHJAKSTDAT);
                    return;
                }
                else {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IHJAKSTDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("작업시작일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IHJAKSTDAT);
                        return;
                    }
                }
            }
            if (Get_Date(MTB01_IHJAKENDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IHJAKENDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("작업종료일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IHJAKENDAT);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IHJAKENDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("작업종료일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IHJAKENDAT);
                        return;
                    }
                }
            }
            if (Get_Date(MTB01_IHIANDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IHIANDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("이안일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IHIANDAT);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IHIANDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("이안일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IHIANDAT);
                        return;
                    }
                }
            }
            
            
            if (Get_Date(MTB01_IHHUNSTDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IHHUNSTDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("훈증시작일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IHHUNSTDAT);
                    return;
                }
                else{
                    bRtn = dateValidateCheck(Get_Date(MTB01_IHHUNSTDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("훈증시작일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IHHUNSTDAT);
                        return;
                    }
                }
            }
            if (Get_Date(MTB01_IHHUNEDDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IHHUNEDDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("훈증종료일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IHHUNEDDAT);
                    return;
                }
                else {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IHHUNEDDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("훈증종료일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IHHUNEDDAT);
                        return;
                    }
                }
            }

            // 입항일자, 시간 체크
            if (this.MTB01_IHIPHTIM.GetValue().ToString().Replace(":","").Trim() != "")
            {
                if (Get_Date(this.MTB01_IHIPHANG.GetValue().ToString().Trim().Replace(" ", "")) == "")
                {
                    this.ShowCustomMessage("입항일자를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHIPHANG);
                    e.Successed = false;
                    return;
                }
                
                if (Convert.ToInt32(Get_Numeric(this.MTB01_IHIPHTIM.GetValue().ToString().Replace(":", "").Trim().Substring(0, 2))) > 24 || Convert.ToInt32(Get_Numeric(this.MTB01_IHIPHTIM.GetValue().ToString().Replace(":", "").Trim().Substring(2, 2))) > 59)
                {
                    this.ShowCustomMessage("입항시간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHIPHTIM);
                    e.Successed = false;
                    return;
                }
                
            }

            //접안일자, 시간 체크
            if (this.MTB01_IHJUBTIM.GetValue().ToString().Replace(":","").Trim() != "")
            {
                if (Get_Date(this.MTB01_IHJUBDAT.GetValue().ToString().Trim().Replace(" ", "")) == "")
                {
                    this.ShowCustomMessage("접안일자를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHJUBDAT);
                    e.Successed = false;
                    return;
                }
                
                if (Convert.ToInt32(Get_Numeric(this.MTB01_IHJUBTIM.GetValue().ToString().Replace(":", "").Trim().Substring(0, 2))) > 24 || Convert.ToInt32(Get_Numeric(this.MTB01_IHJUBTIM.GetValue().ToString().Replace(":", "").Trim().Substring(2, 2))) > 59)
                {
                    this.ShowCustomMessage("접안시간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHJUBTIM);
                    e.Successed = false;
                    return;
                }
            }

            //작업개시일자, 시간 체크
            if (this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":","").Trim() != "")
            {
                if (Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Trim().Replace(" ", "")) == "")
                {
                    this.ShowCustomMessage("작업시작일자를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHJAKSTDAT);
                    e.Successed = false;
                    return;
                }
                if (this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":", "").Trim() != "")
                {
                    if (Convert.ToInt32(Get_Numeric(this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":", "").Trim().Substring(0, 2))) > 24 || Convert.ToInt32(Get_Numeric(this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":", "").Trim().Substring(2, 2))) > 59)
                    {
                        this.ShowCustomMessage("작업시작시간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.MTB01_IHJAKSTTIM);
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 입고파일 체크 USIIPGOF
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_93FBN133",
                                    this.CBH01_IHHANGCHA.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (this.MTB01_IHJUBTIM.GetValue().ToString().Replace(":", "").Trim() == "")
                {
                    this.ShowCustomMessage("접안시작시간을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHJUBTIM);
                    e.Successed = false;
                    return;
                }
                if (this.MTB01_IHIANTIM.GetValue().ToString().Replace(":", "").Trim() == "")
                {
                    this.ShowCustomMessage("이안시작시간을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHIANTIM);
                    e.Successed = false;
                    return;
                }
            }

            //작업종료일자, 시간 체크
            if (this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":","").Trim() != "")
            {
                if (Get_Date(this.MTB01_IHJAKENDAT.GetValue().ToString().Trim().Replace(" ", "")) == "")
                {
                    this.ShowCustomMessage("작업종료일자를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHJAKENDAT);
                    e.Successed = false;
                    return;
                }
                if (this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":", "").Trim() != "")
                {
                    if (Convert.ToInt32(Get_Numeric(this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":", "").Trim().Substring(0, 2))) > 24 || Convert.ToInt32(Get_Numeric(this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":", "").Trim().Substring(2, 2))) > 59)
                    {
                        this.ShowCustomMessage("작업종료시간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.MTB01_IHJAKENTIM);
                        e.Successed = false;
                        return;
                    }
                }
            }

            //접안일자 > 작업개시일자 체크
            if (Get_Date(this.MTB01_IHJUBDAT.GetValue().ToString().Trim().Replace(" ", "")) != "" && Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Trim().Replace(" ", "")) != "")
            {
                if (Convert.ToInt32(Get_Numeric(Get_Date(this.MTB01_IHJUBDAT.GetValue().ToString().Trim().Replace(" ", "")))) > Convert.ToInt32(Get_Numeric(Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Trim().Replace(" ", "")))))
                {
                    this.ShowCustomMessage("접안일자가 작업개시일자 보다 클 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHJUBDAT);
                    e.Successed = false;
                    return;
                }
            }

            //작업개시일자 > 작업종료일자 체크
            if (Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Trim().Replace(" ", "")) != "" && Get_Date(this.MTB01_IHJAKENDAT.GetValue().ToString().Trim().Replace(" ", "")) != "")
            {
                if (Convert.ToInt32(Get_Numeric(Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Trim().Replace(" ", "")))) > Convert.ToInt32(Get_Numeric(Get_Date(this.MTB01_IHJAKENDAT.GetValue().ToString().Trim().Replace(" ", "")))))
                {
                    this.ShowCustomMessage("작업개시일자가 작업종료일자보다 클 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHJAKSTDAT);
                    e.Successed = false;
                    return;
                }
                if (Get_Date(this.MTB01_IHJAKSTDAT.GetValue().ToString().Replace(" ", "").Trim()) == Get_Date(this.MTB01_IHJAKENDAT.GetValue().ToString().Replace(" ", "").Trim()))
                {
                    if (this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":", "").Trim() != "" && this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":", "").Trim() != "")
                    {
                        if (Convert.ToInt32(Get_Numeric(this.MTB01_IHJAKSTTIM.GetValue().ToString().Replace(":", "").Trim())) > Convert.ToInt32(Get_Numeric(this.MTB01_IHJAKENTIM.GetValue().ToString().Replace(":", "").Trim())))
                        {
                            this.ShowCustomMessage("작업개시시간이 작업종료시간보다 클 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.MTB01_IHJAKSTTIM);
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            //이안일자, 시간 체크
            if (this.MTB01_IHIANTIM.GetValue().ToString().Replace(":","").Trim() != "")
            {
                if (Get_Date(this.MTB01_IHIANDAT.GetValue().ToString().Trim().Replace(" ", "")) == "")
                {
                    this.ShowCustomMessage("이안일자를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHIANDAT);
                    e.Successed = false;
                    return;
                }
                if (this.MTB01_IHIANTIM.GetValue().ToString().Replace(":", "").Trim() != "")
                {
                    if (Convert.ToInt32(Get_Numeric(this.MTB01_IHIANTIM.GetValue().ToString().Replace(":", "").Trim().Substring(0, 2))) > 24 || Convert.ToInt32(Get_Numeric(this.MTB01_IHIANTIM.GetValue().ToString().Replace(":", "").Trim().Substring(2, 2))) > 59)
                    {
                        this.ShowCustomMessage("이안시간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.MTB01_IHIANTIM);
                        e.Successed = false;
                        return;
                    }
                }
            }
            
            //훈증시작 > 종료일자 체크
            if (Get_Date(this.MTB01_IHHUNSTDAT.GetValue().ToString().Trim().Replace(" ", "")) != "" && Get_Date(this.MTB01_IHHUNEDDAT.GetValue().ToString().Trim().Replace(" ", "")) != "")
            {
                if (Convert.ToInt32(Get_Numeric(Get_Date(this.MTB01_IHHUNSTDAT.GetValue().ToString().Trim().Replace(" ", "")))) > Convert.ToInt32(Get_Numeric(Get_Date(this.MTB01_IHHUNEDDAT.GetValue().ToString().Trim().Replace(" ", "")))))
                {
                    this.ShowCustomMessage("훈증시작일자가 종료일자 보다 클 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.MTB01_IHHUNSTDAT);
                    e.Successed = false;
                    return;
                }
            }

            //매출 구분 체크
            // 201011월 부터 전자세금계산서 11 -> 61, 12 -> 62, 13은 그대로
            if (this.CBH01_IHHANGCHA.GetValue().ToString() == "201053" || this.CBH01_IHHANGCHA.GetValue().ToString() == "201056" ||
                this.CBH01_IHHANGCHA.GetValue().ToString() == "201057" || Convert.ToInt32(Get_Numeric(this.CBH01_IHHANGCHA.GetValue().ToString())) > 201059)
            {
                if (this.CBH01_IHTAXCODE.GetValue().ToString() != "61" && this.CBH01_IHTAXCODE.GetValue().ToString() != "13" && this.CBH01_IHTAXCODE.GetValue().ToString() != "62")
                {
                    this.ShowCustomMessage("매출 구분은 61,13,62 만 등록가능 합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.CBH01_IHTAXCODE.CodeText);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (this.CBH01_IHTAXCODE.GetValue().ToString() != "11" && this.CBH01_IHTAXCODE.GetValue().ToString() != "13" && this.CBH01_IHTAXCODE.GetValue().ToString() != "22")
                {
                    this.ShowCustomMessage("매출 구분은 11,13,22 만 등록가능 합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.CBH01_IHTAXCODE.CodeText);
                    e.Successed = false;
                    return;
                }
            }
            // B/L 량 체크
            double dIHBLQTY = 0;

            if (Get_Numeric(this.TXT01_IHBLQTY1.GetValue().ToString()) != "")
            {
                dIHBLQTY = Convert.ToDouble(String.Format("{0,9:N3}", dIHBLQTY + Convert.ToDouble(Get_Numeric(TXT01_IHBLQTY1.GetValue().ToString()))));
            }
            if (Get_Numeric(this.TXT01_IHBLQTY2.GetValue().ToString()) != "")
            {
                dIHBLQTY = Convert.ToDouble(String.Format("{0,9:N3}", dIHBLQTY + Convert.ToDouble(Get_Numeric(TXT01_IHBLQTY2.GetValue().ToString()))));
            }
            if (Get_Numeric(this.TXT01_IHBLQTY3.GetValue().ToString()) != "")
            {
                dIHBLQTY = Convert.ToDouble(String.Format("{0,9:N3}", dIHBLQTY + Convert.ToDouble(Get_Numeric(TXT01_IHBLQTY3.GetValue().ToString()))));
            }
            if (Get_Numeric(this.TXT01_IHBLQTY_HAP.GetValue().ToString()) != "")
            {
                if (dIHBLQTY != Convert.ToDouble(Get_Numeric(this.TXT01_IHBLQTY_HAP.GetValue().ToString())))
                {
                    this.ShowCustomMessage("총 B/L량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.TXT01_IHBLQTY_HAP);
                    e.Successed = false;
                    return;
                }
            }

            // 모선스케줄 선택 체크
            if (Get_Date(this.DTP01_IHSHDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "") == "")
            {
                this.ShowCustomMessage("모선스케줄을 조회하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.DTP01_IHSHDATE);
                e.Successed = false;
                return;
            }

            // 적하목록 배정량 체크
            double dJKBEJNQTY = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_99KF4239",
                                    this.CBH01_IHHANGCHA.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                dJKBEJNQTY = Convert.ToDouble(dt.Rows[0]["JKBEJNQTY"].ToString().Trim());

                if (dJKBEJNQTY > Convert.ToDouble(Get_Numeric(TXT01_IHBLQTY_HAP.GetValue().ToString().Trim())))
                {
                    this.ShowCustomMessage("적하목록의 배정량 보다 B/L량이 작습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.TXT01_IHBLQTY_HAP);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 입항관리 삭제버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            // 입항관리 삭제
            this.DbConnector.Attach("TY_P_US_938DP029", this.CBH01_IHHANGCHA.GetValue().ToString());

            // 적하목록 삭제
            this.DbConnector.Attach("TY_P_US_99KFI240", this.CBH01_IHHANGCHA.GetValue().ToString());

            
            this.DbConnector.Attach("TY_P_US_99KG3242",
                                    "",
                                    Get_Date(DTP01_IHSHDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                                    TXT01_IHSHSEQ.GetValue().ToString());

            this.DbConnector.ExecuteTranQueryList();

            UP_FieldVisible("INIT");
            UP_FieldClear("입항");
            fsWK_GUBUN1 = "NEW";

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
            this.CBH01_IHHANGCHA.CodeText.Focus();
        }
        #endregion

        #region Description : 입항관리 삭제 체크
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // B/L별 입고관리 테이블 체크 USIIPBLF
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_93FBR134",
                                    this.CBH01_IHHANGCHA.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {   
                this.ShowCustomMessage("입고관리에 자료가 존재 하므로 삭제가 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 입항관리 - 모선스케줄 코드헬프
        private void BTN62_SILOCODEHELP02_Click(object sender, EventArgs e)
        {
            TYUSGB003S popup = new TYUSGB003S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_IHSHDATE.SetValue(popup.fsSHDATE);
                this.TXT01_IHSHSEQ.SetValue(popup.fsSHSEQ);
                this.MTB01_IHJAKSTDAT.SetValue(popup.fsSHETCD_S);
                this.MTB01_IHJAKENDAT.SetValue(popup.fsSHETCD_E);
                this.MTB01_IHIPHANG.SetValue(popup.fsSHETAULSAN);
                this.MTB01_IHIPHTIM.SetValue(popup.fsSHETAULTIME);
                this.TXT01_IHBLQTY1.SetValue(popup.fsSHULSANQTY);
                this.TXT01_IHBLQTY_HAP.SetValue(popup.fsSHULSANQTY);
                this.CBH01_IHSOSOK.SetValueText(popup.fsSHSOSOK, popup.fsSKDESC1);
                this.CBH01_IHGOKJONG1.SetValue(popup.fsSHGOKJONG);
                this.CBH01_IHWONSAN1.SetValue(popup.fsSHWONSAN);
                this.CBH01_IHBRANCH.SetValue(popup.fsSHAGENT);
                this.CBH01_IHLSCODE.SetValue(popup.fsSHSURVEY);

                //this.CBH01_IHHANGCHA.CodeText.Focus();
                this.SetFocus(this.TXT01_IHGROSS);

                //// 값 저장
                //UP_SET_Cookie2(this.DTP01_CMIPHANG.GetValue().ToString(), this.CBH01_CMBONSUN.GetValue().ToString(),
                //               this.CBH01_CMHWAJU.GetValue().ToString(), this.CBH01_CMHWAMUL.GetValue().ToString());

                //SetFocus(this.CBH01_CMHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 입항관리 - 적하목록관리 팝업
        private void BTN62_SILOCODEHELP03_Click(object sender, EventArgs e)
        {
            TYUSKB004I popup = new TYUSKB004I(this.CBH01_IHHANGCHA.GetValue().ToString(), this.TXT01_IHJUKHANO.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_IHGROSS.Focus();
            }
        }
        #endregion

        #region Description : 입항관리 총 B/L량 자동계산
        private void TXT01_IHBLQTY1_Leave(object sender, EventArgs e)
        {
            UP_IHBLQTY_Compute();
        }

        private void TXT01_IHBLQTY2_Leave(object sender, EventArgs e)
        {
            UP_IHBLQTY_Compute();
        }

        private void TXT01_IHBLQTY3_Leave(object sender, EventArgs e)
        {
            UP_IHBLQTY_Compute();
        }

        private void TXT01_IHBLQTY_Leave(object sender, EventArgs e)
        {
            UP_IHBLQTY_Compute();
        }

        private void UP_IHBLQTY_Compute()
        {
            double dIHBLQTY = 0;

            if (Get_Numeric(this.TXT01_IHBLQTY1.GetValue().ToString()) != "")
            {
                dIHBLQTY = Convert.ToDouble(String.Format("{0,9:N3}", dIHBLQTY + Convert.ToDouble(Get_Numeric(TXT01_IHBLQTY1.GetValue().ToString().Trim()))));
            }
            if (Get_Numeric(this.TXT01_IHBLQTY2.GetValue().ToString()) != "")
            {
                dIHBLQTY = Convert.ToDouble(String.Format("{0,9:N3}", dIHBLQTY + Convert.ToDouble(Get_Numeric(TXT01_IHBLQTY2.GetValue().ToString().Trim()))));
            }
            if (Get_Numeric(this.TXT01_IHBLQTY3.GetValue().ToString()) != "")
            {
                dIHBLQTY = Convert.ToDouble(String.Format("{0,9:N3}", dIHBLQTY + Convert.ToDouble(Get_Numeric(TXT01_IHBLQTY3.GetValue().ToString().Trim()))));
            }
            this.TXT01_IHBLQTY_HAP.SetValue(String.Format("{0,9:N3}",dIHBLQTY));
        }
        #endregion

        #endregion

        #region Description : 입고관리

        #region Description : 입고관리 전체조회
        private void UP_USIIPGOF_Search()
        {
            this.FPS91_TY_S_US_93EDW122.Initialize();
            this.FPS91_TY_S_US_93EDW121.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_938EH035",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_93EDW121.SetValue(dt);
        }
        #endregion

        #region Description : 입고관리 조회
        private void UP_USIIPGOF_TAB_Search()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_949FE298",
                this.CBH01_IPHANGCHA.GetValue().ToString(),
                this.CBH01_IPGOKJONG.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_93EDW122.SetValue(dt);
        }
        #endregion

        #region Description : 입고관리 확인
        private void UP_USIIPGOF_Run()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_949FE298",
                this.CBH01_IPHANGCHA.GetValue().ToString(),
                this.CBH01_IPGOKJONG.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_IPHANGCHA.SetValue(dt.Rows[0]["IPHANGCHA"].ToString());
                this.CBH01_IPGOKJONG.SetValue(dt.Rows[0]["IPGOKJONG"].ToString());

                this.MTB01_IPIPHANG.SetValue(dt.Rows[0]["IHIPHANG"].ToString());
                this.TXT01_IPBLQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHBLQTY"].ToString().Trim())));
                this.TXT01_IPJANQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IPJANQTY"].ToString().Trim())));

                this.MTB01_IPIPSTDAT.SetValue(dt.Rows[0]["IPIPSTDAT"].ToString());

                this.TXT01_IPHAP.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IPIPPQTY_ABC"].ToString().Trim())));        //ABC 수둥
                this.TXT01_IPIPHAP.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IPIPQTY_ABC"].ToString().Trim())));      //ABC 자동

                this.TXT01_IPDHAP.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IPIPPQTY_D"].ToString().Trim())));      //D 수동
                this.TXT01_IPIPDHAP.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IPIPQTY_D"].ToString().Trim())));    //D 자동

                this.TXT01_IPAUTOTOTQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IPIPHAP"].ToString().Trim()))); //자동누계
                this.TXT01_IPTOTQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IPTOTQTY"].ToString().Trim())));    //확정량

                // ABC 그룹 입고량 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_938FG039",
                    this.CBH01_IPHANGCHA.GetValue().ToString(),
                    this.CBH01_IPGOKJONG.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_US_945HS264.SetValue(dt);

                // D 그룹 입고량 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_94II3399",
                    this.CBH01_IPHANGCHA.GetValue().ToString(),
                    this.CBH01_IPGOKJONG.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_US_94II4400.SetValue(dt);

                fsWK_GUBUN2 = "UPT";
                UP_FieldVisible("UPT");

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.MTB01_IPIPSTDAT.Focus();
                };

                tmr.Interval = 100;
                tmr.Start();
            }
            else
            {
                UP_FieldClear("입고");
            }
            Set_Cookie();
        }
        #endregion

        #region Description : 입고관리 조회 그리드 더블클릭
        private void FPS91_TY_S_US_93EDW122_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_IPHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW122.GetValue("IPHANGCHA").ToString());
            this.CBH01_IPGOKJONG.SetValue(this.FPS91_TY_S_US_93EDW122.GetValue("IPGOKJONG").ToString());

            UP_USIIPGOF_Run();
        }
        #endregion

        #region Description : 입고관리 전체조회 그리드 더블클릭
        private void FPS91_TY_S_US_93EDW121_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_IPHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW121.GetValue("IPHANGCHA").ToString());
            this.CBH01_IPGOKJONG.SetValue(this.FPS91_TY_S_US_93EDW121.GetValue("IPGOKJONG").ToString());

            UP_USIIPGOF_TAB_Search();

            UP_USIIPGOF_Run();
        }
        private void FPS91_TY_S_US_93EDW121_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_IPHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW121.GetValue("IPHANGCHA").ToString());
                this.CBH01_IPGOKJONG.SetValue(this.FPS91_TY_S_US_93EDW121.GetValue("IPGOKJONG").ToString());

                UP_USIIPGOF_TAB_Search();

                UP_USIIPGOF_Run();
            }
        }
        #endregion

        #region Description : 입고관리 신규버튼
        private void BTN63_NEW_Click(object sender, EventArgs e)
        {   
            UP_FieldClear("입고");
            UP_FieldVisible("NEW");
            Get_Cookie();
            fsWK_GUBUN2 = "NEW";
            UP_Set_KeyFocus();
            FPS91_TY_S_US_93EDW122.Initialize();
        }
        #endregion

        #region Description : 입고관리 저장버튼
        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN2 == "NEW")   //등록
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_938EP036",
                                        this.CBH01_IPHANGCHA.GetValue().ToString(),
                                        this.CBH01_IPGOKJONG.GetValue().ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPQTY2").ToString(),
                                        Get_Numeric(this.TXT01_IPTOTQTY.GetValue().ToString()),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDQTY2").ToString(),
                                        Get_Numeric(this.TXT01_IPIPHAP.GetValue().ToString()),
                                        Get_Date(this.MTB01_IPIPSTDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY2").ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else if (fsWK_GUBUN2 == "UPT")  //수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_938EY037",
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPQTY2").ToString(),
                                        Get_Numeric(this.TXT01_IPTOTQTY.GetValue().ToString()),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDQTY2").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT1").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDQTY1").ToString(),
                                        Get_Date(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT2").ToString()).Replace("19000101", "").Replace("44441231", ""),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDQTY2").ToString(),
                                        Get_Numeric(this.TXT01_IPIPHAP.GetValue().ToString()),
                                        Get_Date(this.MTB01_IPIPSTDAT.GetValue().ToString().Replace(" ","").Trim()),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY1").ToString(),
                                        this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY2").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY1").ToString(),
                                        this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY2").ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        this.CBH01_IPHANGCHA.GetValue().ToString(),
                                        this.CBH01_IPGOKJONG.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            fsWK_GUBUN2 = "UPT";
            UP_USIIPGOF_Search();
            UP_USIIPGOF_TAB_Search();
            UP_USIIPGOF_Run();
            this.ShowMessage("TY_M_GB_23NAD873");
            this.MTB01_IPIPSTDAT.Focus();
            Set_Cookie();
        }
        #endregion

        #region Description : 입고관리 저장 체크
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRtn;

            // 날짜 체크
            if (Get_Date(MTB01_IPIPHANG.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IPIPHANG.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("입항일자를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IPIPHANG);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IPIPHANG.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("입항일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IPIPHANG);
                        return;
                    }
                }
            }

            if (Get_Date(MTB01_IPIPSTDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IPIPSTDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("보관료발생일자를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IPIPSTDAT);
                    return;
                }
                else {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IPIPSTDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("보관료발생일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IPIPSTDAT);
                        return;
                    }
                }
            }

            // ABC 그룹
            // 입고일자1, 입고량1
            if (this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT1").ToString() == "" || this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT1").ToString() == "19000101")
            {
                this.ShowCustomMessage("[ABC 그룹]입고일자1을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(0, 0, 1, 1);
                e.Successed = false;
                return;
            }
            if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY1").ToString()) == 0)
            {
                this.ShowCustomMessage("[ABC 그룹]입고량1을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(0, 2, 1, 1);
                e.Successed = false;
                return;
            }

            // 입고량2
            if (this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT2").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량2를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(0, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자2를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(0, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량3
            if (this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT1").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량3을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(1, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자3을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(1, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량4
            if (this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT2").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량4를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(1, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자4를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(1, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량5
            if (this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT1").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량5를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(2, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자5를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(2, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량6
            if (this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT2").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량6을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(2, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자6을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(2, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량7
            if (this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT1").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량7을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(3, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자7을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(3, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량8
            if (this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT2").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량8을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(3, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자8을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(3, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량9
            if (this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT1").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량9를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(4, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자9를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(4, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량10
            if (this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT2").ToString() != "" && this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고량10을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(4, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[ABC 그룹]입고일자10을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_945HS264.ActiveSheet.Models.Selection.SetSelection(4, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // D 그룹
            // 입고량1
            if (this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT1").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량1을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(0, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자1을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(0, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량2
            if (this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT2").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량2를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(0, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자2를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(0, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량3
            if (this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT1").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량3을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(1, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자3을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(1, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량4
            if (this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT2").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량4를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(1, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자4를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(1, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량5
            if (this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT1").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량5를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(2, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자5를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(2, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량6
            if (this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT2").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량6을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(2, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자6을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(2, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량7
            if (this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT1").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량7을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(3, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자7을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(3, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량8
            if (this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT2").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량8을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(3, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자8을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(3, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량9
            if (this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT1").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT1").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY1").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량9를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(4, 2, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY1").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자9를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(4, 0, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            // 입고량10
            if (this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT2").ToString() != "" && this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDDAT2").ToString() != "19000101")
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY2").ToString()) == 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고량10을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(4, 5, 1, 1);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY2").ToString()) != 0)
                {
                    this.ShowCustomMessage("[D 그룹]입고일자10을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_US_94II4400.ActiveSheet.Models.Selection.SetSelection(4, 3, 1, 1);
                    e.Successed = false;
                    return;
                }
            }

            string sIHGOKJONG1 = "";
            string sIHGOKJONG2 = "";
            string sIHGOKJONG3 = "";
            //string sIHIPHANG = "";
            //string sIHBLQTY = "0";
            string sIHJUBTIM = "";
            string sIHIANTIM = "";

            // 입항관리 곡종체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_949IV311",
                                    this.CBH01_IPHANGCHA.GetValue().ToString());
        
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sIHGOKJONG1 = dt.Rows[0]["IHGOKJONG1"].ToString();
                sIHGOKJONG2 = dt.Rows[0]["IHGOKJONG2"].ToString();
                sIHGOKJONG3 = dt.Rows[0]["IHGOKJONG3"].ToString();
                //sIHIPHANG = dt.Rows[0]["IHIPHANG"].ToString();
                //sIHBLQTY = dt.Rows[0]["IHBLQTY"].ToString();
                sIHJUBTIM = dt.Rows[0]["IHJUBTIM"].ToString();
                sIHIANTIM = dt.Rows[0]["IHIANTIM"].ToString();
            }
            else{
                this.ShowCustomMessage("입항관리에 등록된 항차가 아닙니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.CBH01_IPHANGCHA.CodeText);
                e.Successed = false;
                return;
            }

            if(sIHGOKJONG1 != this.CBH01_IPGOKJONG.GetValue().ToString() &&
               sIHGOKJONG2 != this.CBH01_IPGOKJONG.GetValue().ToString() &&
               sIHGOKJONG3 != this.CBH01_IPGOKJONG.GetValue().ToString())
            {
                this.ShowCustomMessage("입항관리에 등록된 곡종이 아닙니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.CBH01_IPGOKJONG.CodeText);
                e.Successed = false;
                return;
            }

            // 입항관리 접안, 이안시간 체크
            if (sIHJUBTIM == "0" || sIHIANTIM == "0")
            {
                this.ShowCustomMessage("입항관리의 접안시간과 이안시간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            // ABC 그룹 입고량 계산
            string sIPHAP = String.Format("{0,9:N3}", (Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY1").ToString().Trim())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPPQTY2").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY1").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPPQTY2").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY1").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPPQTY2").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY1").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPPQTY2").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY1").ToString())) +
                             Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPPQTY2").ToString()))));

            // ABC 그룹 수동입고량
            this.TXT01_IPHAP.SetValue(string.Format("{0,9:N3}", sIPHAP));

            string sIPIPHAP = String.Format("{0,9:N3}", (Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPQTY1").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(0, "IPIPQTY2").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPQTY1").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(1, "IPIPQTY2").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPQTY1").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(2, "IPIPQTY2").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPQTY1").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(3, "IPIPQTY2").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPQTY1").ToString().Trim())) +
                               Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_945HS264.GetValue(4, "IPIPQTY2").ToString().Trim()))));

            // ABC 그룹 자동입고량
            this.TXT01_IPIPHAP.SetValue(string.Format("{0,9:N3}", sIPIPHAP));

            // D 그룹 입고량 계산
            string sIPDHAP = String.Format("{0,9:N3}", (Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY1").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIPDQTY2").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY1").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIPDQTY2").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY1").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIPDQTY2").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY1").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIPDQTY2").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY1").ToString().Trim())) +
                              Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIPDQTY2").ToString().Trim()))));

            // D 그룹 수동입고량
            this.TXT01_IPDHAP.SetValue(string.Format("{0,9:N3}", sIPDHAP));

            string sIPIPDHAP = String.Format("{0,9:N3}", (Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDQTY1").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(0, "IPIDQTY2").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDQTY1").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(1, "IPIDQTY2").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDQTY1").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(2, "IPIDQTY2").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDQTY1").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(3, "IPIDQTY2").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDQTY1").ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_94II4400.GetValue(4, "IPIDQTY2").ToString().Trim()))));

            // D 그룹 자동입고량
            this.TXT01_IPIPDHAP.SetValue(sIPIPDHAP);
            
            // 자동입고계
            this.TXT01_IPAUTOTOTQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(sIPIPHAP.Trim())) + Convert.ToDouble(Get_Numeric(sIPIPDHAP.Trim()))));

            // 확정량
            this.TXT01_IPTOTQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(sIPHAP.Trim())) + Convert.ToDouble(Get_Numeric(sIPDHAP.Trim()))));

            // B/L별 입고관리 확정량 체크
            double dIBHWAKQTY = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94NIW457",
                                    this.CBH01_IPHANGCHA.GetValue().ToString(),
                                    this.CBH01_IPGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dIBHWAKQTY = Convert.ToDouble(dt.Rows[0]["IBHWAKQTY"].ToString().Trim());

                if (dIBHWAKQTY > Convert.ToDouble(Get_Numeric(TXT01_IPTOTQTY.GetValue().ToString().Trim())))
                {
                    this.ShowCustomMessage("B/L별 입고관리의 확정량 보다 수량이 작습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.TXT01_IPTOTQTY);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 입고관리 삭제버튼
        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_US_938EY038", this.CBH01_IPHANGCHA.GetValue().ToString(),
                                                        this.CBH01_IPGOKJONG.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            UP_FieldVisible("INIT");
            UP_FieldClear("입고");
            fsWK_GUBUN2 = "NEW";

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
            this.CBH01_IPHANGCHA.CodeText.Focus();
        }
        #endregion

        #region Description : 입고관리 삭제 체크
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // USIJEGOF 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_949JX312",
                                    this.CBH01_IPHANGCHA.GetValue().ToString(),
                                    this.CBH01_IPGOKJONG.GetValue().ToString());
        
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("재고파일에 자료가 존재하여 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 입고관리 - 입항조회 코드헬프
        private void BTN63_SILOCODEHELP04_Click(object sender, EventArgs e)
        {
            TYUSGB004S popup = new TYUSGB004S(CBH01_IPHANGCHA.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_IPHANGCHA.SetValue(popup.fsIHHANGCHA);
                this.CBH01_IPGOKJONG.SetValue(popup.fsIHGOKJONG1);

                UP_USIIPGOF_Run();

                if (fsWK_GUBUN2 == "NEW")
                {
                    this.CBH01_IPHANGCHA.SetValue(popup.fsIHHANGCHA);
                    this.CBH01_IPGOKJONG.SetValue(popup.fsIHGOKJONG1);

                    this.MTB01_IPIPHANG.SetValue(popup.fsIHIPHANG);
                    this.TXT01_IPBLQTY.SetValue(popup.fsIHBLQTY);
                    this.MTB01_IPIPSTDAT.SetValue(popup.fsIHJAKENDAT);

                    this.SetFocus(this.MTB01_IPIPSTDAT);
                }
            }
        }
        #endregion

        #region Description : 입고량 복사
        private void BTN63_COPY_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9A8E4295",
                                    this.CBH01_IPHANGCHA.GetValue().ToString(),
                                    this.CBH01_IPGOKJONG.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            UP_USIIPGOF_Run();
        }
        #endregion

        #region Description : 입고관리 - 입고일자 미입력시 공백처리
        private void FPS91_TY_S_US_945HS264_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (this.FPS91_TY_S_US_945HS264.GetValue("IPIPDAT1").ToString() == "19000101")
            {
                this.FPS91_TY_S_US_945HS264.SetValue("IPIPDAT1", "");
            }
            if (this.FPS91_TY_S_US_945HS264.GetValue("IPIPDAT2").ToString() == "19000101")
            {
                this.FPS91_TY_S_US_945HS264.SetValue("IPIPDAT2", "");
            }
            if (this.FPS91_TY_S_US_945HS264.GetValue("IPIPPQTY1").ToString() == "")
            {
                this.FPS91_TY_S_US_945HS264.SetValue("IPIPPQTY1", "0.000");
            }
            if (this.FPS91_TY_S_US_945HS264.GetValue("IPIPPQTY2").ToString() == "")
            {
                this.FPS91_TY_S_US_945HS264.SetValue("IPIPPQTY2", "0.000");
            }
        }
        private void FPS91_TY_S_US_94II4400_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (this.FPS91_TY_S_US_94II4400.GetValue("IPIDDAT1").ToString() == "19000101")
            {
                this.FPS91_TY_S_US_94II4400.SetValue("IPIDDAT1", "");
            }
            if (this.FPS91_TY_S_US_94II4400.GetValue("IPIDDAT2").ToString() == "19000101")
            {
                this.FPS91_TY_S_US_94II4400.SetValue("IPIDDAT2", "");
            }
            if (this.FPS91_TY_S_US_94II4400.GetValue("IPIPDQTY1").ToString() == "")
            {
                this.FPS91_TY_S_US_94II4400.SetValue("IPIPDQTY1", "0.000");
            }
            if (this.FPS91_TY_S_US_94II4400.GetValue("IPIPDQTY2").ToString() == "")
            {
                this.FPS91_TY_S_US_94II4400.SetValue("IPIPDQTY2", "0.000");
            }
        }
        #endregion

        #endregion

        #region Descriptoin : B/L별 입고관리

        #region Description : B/L별 입고관리 전체조회
        private void UP_USIIPBLF_Search()
        {
            this.FPS91_TY_S_US_93EDW124.Initialize();
            this.FPS91_TY_S_US_93EDW123.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_94AKG336",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_93EDW123.SetValue(dt);
        }
        #endregion

        #region Description : B/L별 입고관리 조회
        private void UP_USIIPBLF_TAB_Search()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_94CGN363",
                this.CBH01_IBHANGCHA.GetValue().ToString(),
                this.CBH01_IBGOKJONG.GetValue().ToString(),
                this.CBH01_IBHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_93EDW124.SetValue(dt);
        }
        #endregion

        #region Description : B/L별 입고관리 확인
        private void UP_USIIPBLF_Run(string sGUBUN)
        {
            DataTable dt = new DataTable();

            fdIBHWAKQTY = 0;
            fdPRE_IBBEIPQTY = 0;
            fdPRE_IBHWIPQTY = 0;

            UP_IPBLvarInit();

            if (sGUBUN == "NEW")
            {
                UP_FieldClear("B/L별입고");

                // 적하목록 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NKS460",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_JKBEJNQTY.SetValue(String.Format("{0,9:N3}",Convert.ToDouble(dt.Rows[0]["JKBEJNQTY"].ToString().Trim())));  //배정합계
                    this.TXT01_JKHWAKQTY.SetValue(String.Format("{0,9:N3}",Convert.ToDouble(dt.Rows[0]["JKHWAKQTY"].ToString().Trim())));  //확정합계
                }

                // 입항내역 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NKV461",
                                        this.CBH01_IBHANGCHA.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                string sIHJAKENDAT = string.Empty;

                if (dt.Rows.Count > 0)
                {
                    this.DTP01_IHIPHANG2.SetValue(dt.Rows[0]["IHIPHANG"].ToString());   //입항일자
                    this.TXT01_IHBLQTY5.SetValue(String.Format("{0,9:N3}",Convert.ToDouble(dt.Rows[0]["IHBLQTY"].ToString().Trim())));     //B/L량
                    sIHJAKENDAT = dt.Rows[0]["IHJAKENDAT"].ToString();     //작업종료일자
                }

                //// 화물번호 조회

                //this.TXT01_IBHMNO1.SetValue(this.CBH01_IBHANGCHA.GetValue().ToString().Substring(0, 4));
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_US_94NL2462",
                //                        this.CBH01_IBHANGCHA.GetValue().ToString());

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.TXT01_IBHMNO2.SetValue(this.CBH01_IBHANGCHA.GetValue().ToString().Substring(4, 2) + Set_Fill4(dt.Rows[0]["COUNT"].ToString()));
                //}

                this.MTB01_IBHWAKIL.SetValue(Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ","").Trim()));    //확정일자

                this.MTB01_IBJESTDAT.SetValue(DateTime.Parse(Set_Date(sIHJAKENDAT)).AddDays(+18).ToString("yyyy-MM-dd"));
            }
            else if (sGUBUN == "RUN")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_94CH4367",
                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                    this.CBH01_IBHWAJU.GetValue().ToString(),
                    this.TXT01_IBBLNO.GetValue().ToString(),
                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                    this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                    this.TXT01_IBBLSEQ.GetValue().ToString().Trim(),
                    Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ","").Trim())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_IBHANGCHA.SetValue(dt.Rows[0]["IBHANGCHA"].ToString());
                    this.CBH01_IBGOKJONG.SetValue(dt.Rows[0]["IBGOKJONG"].ToString());
                    this.CBH01_IBHWAJU.SetValue(dt.Rows[0]["IBHWAJU"].ToString());
                    this.TXT01_IBBLNO.SetValue(dt.Rows[0]["IBBLNO"].ToString());
                    this.TXT01_IBBLMSN.SetValue(dt.Rows[0]["IBBLMSN"].ToString());
                    this.TXT01_IBBLHSN.SetValue(dt.Rows[0]["IBBLHSN"].ToString());
                    this.TXT01_IBBLSEQ.SetValue(dt.Rows[0]["IBBLSEQ"].ToString());
                    this.MTB01_IBDATE.SetValue(dt.Rows[0]["IBDATE"].ToString());
                    this.TXT01_IBHMNO1.SetValue(dt.Rows[0]["IBHMNO1"].ToString());
                    this.TXT01_IBHMNO2.SetValue(dt.Rows[0]["IBHMNO2"].ToString());

                    this.DTP01_IHIPHANG2.SetValue(dt.Rows[0]["IHIPHANG"].ToString());
                    this.TXT01_IHBLQTY5.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IHBLQTY5"].ToString().Trim())));
                    this.TXT01_JKBEJNQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGBEJNQTY"].ToString().Trim())));
                    this.TXT01_JKHWAKQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString().Trim())));

                    this.TXT01_IBHBLNO.SetValue(dt.Rows[0]["IBHBLNO"].ToString());
                    this.CBO01_IBBHGUBN.SetValue(dt.Rows[0]["IBBHGUBN"].ToString());
                    this.CBH01_IBGUBUN.SetValue(dt.Rows[0]["IBGUBUN"].ToString());
                    this.CBH01_IBWONSAN.SetValue(dt.Rows[0]["IBWONSAN"].ToString());
                    this.TXT01_IBBEJNQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBEJNQTY"].ToString().Trim())));
                    fdIBBEJNQTY = Convert.ToDouble(dt.Rows[0]["IBBEJNQTY"].ToString());
                    this.TXT01_IBHWAKQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWAKQTY"].ToString().Trim())));
                    fdIBHWAKQTY = Convert.ToDouble(dt.Rows[0]["IBHWAKQTY"].ToString());
                    this.MTB01_IBHWAKIL.SetValue(dt.Rows[0]["IBHWAKIL"].ToString());
                    this.CBH01_IBSOSOK.SetValue(dt.Rows[0]["IBSOSOK"].ToString(), dt.Rows[0]["SOSOKNM"].ToString());
                    this.MTB01_IBJESTDAT.SetValue(dt.Rows[0]["IBJESTDAT"].ToString());
                    this.TXT01_IBCONTNO.SetValue(dt.Rows[0]["IBCONTNO"].ToString());
                    this.CBH01_IBBPSAGO.SetValue(dt.Rows[0]["IBBPSAGO"].ToString());
                    this.CBH01_IBUNSONG.SetValue(dt.Rows[0]["IBUNSONG"].ToString());

                    this.MTB01_IBBLDATE.SetValue(dt.Rows[0]["IBBLDATE"].ToString());
                    this.CBH01_IBJNHWAJU.SetValue(dt.Rows[0]["IBJNHWAJU"].ToString());
                    fsPRE_IBJNHWAJU = dt.Rows[0]["IBJNHWAJU"].ToString();
                    this.TXT01_IBJNBLHSN.SetValue(dt.Rows[0]["IBJNBLHSN"].ToString());
                    fsPRE_IBJNBLHSN = dt.Rows[0]["IBJNBLHSN"].ToString();
                    this.TXT01_IBJNBLSEQ.SetValue(dt.Rows[0]["IBJNBLSEQ"].ToString());
                    fsPRE_IBJNBLSEQ = dt.Rows[0]["IBJNBLSEQ"].ToString();
                    this.TXT01_IBBEIPQTY.SetValue(String.Format("{0,9:N3}",Convert.ToDouble(dt.Rows[0]["IBBEIPQTY"].ToString().Trim())));
                    fdPRE_IBBEIPQTY = Convert.ToDouble(dt.Rows[0]["IBBEIPQTY"].ToString());
                    this.TXT01_IBHWIPQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWIPQTY"].ToString().Trim())));
                    fdPRE_IBHWIPQTY = Convert.ToDouble(dt.Rows[0]["IBHWIPQTY"].ToString());
                    this.TXT01_IBBECHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBECHQTY"].ToString().Trim())));
                    this.TXT01_IBHWCHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWCHQTY"].ToString().Trim())));

                    fsWK_GUBUN3 = "UPT";
                    UP_FieldVisible("UPT");

                    // FOCUS
                    Timer tmr = new Timer();

                    tmr.Tick += delegate
                    {
                        tmr.Stop();
                        this.TXT01_IBHBLNO.Focus();
                    };

                    tmr.Interval = 100;
                    tmr.Start();
                }
            }
            
            Set_Cookie();
        }
        #endregion

        #region Description : B/L별 입고관리 조회 그리드 더블클릭
        private void FPS91_TY_S_US_93EDW124_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_IBHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBHANGCHA").ToString());
            this.CBH01_IBGOKJONG.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBGOKJONG").ToString());
            this.CBH01_IBHWAJU.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBHWAJU").ToString());
            this.TXT01_IBBLNO.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBBLNO").ToString());
            this.TXT01_IBBLMSN.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBBLMSN").ToString());
            this.TXT01_IBBLHSN.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBBLHSN").ToString());
            this.TXT01_IBBLSEQ.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBBLSEQ").ToString());
            this.MTB01_IBDATE.SetValue(this.FPS91_TY_S_US_93EDW124.GetValue("IBDATE").ToString());

            UP_USIIPBLF_Run("RUN");
        }
        #endregion

        #region Description : B/L별 입고관리 전체조회 그리드 더블클릭
        private void FPS91_TY_S_US_93EDW123_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_IBHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBHANGCHA").ToString());
            this.CBH01_IBGOKJONG.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBGOKJONG").ToString());
            this.CBH01_IBHWAJU.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBHWAJU").ToString());
            this.TXT01_IBBLNO.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLNO").ToString());
            this.TXT01_IBBLMSN.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLMSN").ToString());
            this.TXT01_IBBLHSN.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLHSN").ToString());
            this.TXT01_IBBLSEQ.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLSEQ").ToString());
            this.MTB01_IBDATE.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBDATE").ToString());

            UP_USIIPBLF_TAB_Search();

            UP_USIIPBLF_Run("RUN");
        }
        private void FPS91_TY_S_US_93EDW123_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_IBHANGCHA.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBHANGCHA").ToString());
                this.CBH01_IBGOKJONG.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBGOKJONG").ToString());
                this.CBH01_IBHWAJU.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBHWAJU").ToString());
                this.TXT01_IBBLNO.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLNO").ToString());
                this.TXT01_IBBLMSN.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLMSN").ToString());
                this.TXT01_IBBLHSN.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLHSN").ToString());
                this.TXT01_IBBLSEQ.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBBLSEQ").ToString());
                this.MTB01_IBDATE.SetValue(this.FPS91_TY_S_US_93EDW123.GetValue("IBDATE").ToString());

                UP_USIIPBLF_TAB_Search();

                UP_USIIPBLF_Run("RUN");
            }
        }
        #endregion

        #region Description : B/L별 입고관리 신규버튼
        private void BTN64_NEW_Click(object sender, EventArgs e)
        {
            fsWK_GUBUN3 = "NEW";
            UP_FieldClear("B/L별입고");
            UP_FieldVisible("NEW");
            Get_Cookie();

            this.TXT01_IBBLSEQ.SetValue("");
            this.TXT01_IBHMNO1.SetValue("");
            this.TXT01_IBHMNO2.SetValue("");

            UP_IBBPSAGO_Select();

            UP_Set_KeyFocus();

            FPS91_TY_S_US_93EDW124.Initialize();
        }
        #endregion

        #region Description : B/L별 입고관리 이전 저장데이터 조회(반입사고, 운송회사)
        private void UP_IBBPSAGO_Select()
        {   
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_99GDV219",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsIBBPSAGO = dt.Rows[0]["IBBPSAGO"].ToString();
                fsIBUNSONG = dt.Rows[0]["IBUNSONG"].ToString();
            }
            else
            {
                fsIBBPSAGO = "";
                fsIBUNSONG = "900";
            }

            this.CBH01_IBBPSAGO.SetValue(fsIBBPSAGO);
            this.CBH01_IBUNSONG.SetValue(fsIBUNSONG);
        }
        #endregion

        #region Description : B/L별 입고관리 저장버튼
        private void BTN64_SAV_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // 신규 등록인 경우 화물번호, 입고차수 가져오기
            if (fsWK_GUBUN3 == "NEW")
            {   
                // 적하목록 등록 체크 (등록 전 한번 더 체크)
                // 원화주인 경우만 체크
                if (TXT01_IBBLHSN.GetValue().ToString().Trim() == "0")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_9C3DG560",
                                            this.CBH01_IBHANGCHA.GetValue().ToString(),
                                            this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                            this.CBH01_IBHWAJU.GetValue().ToString());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowCustomMessage("적하목록이 등록되지 않았습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }

                // 화물번호
                this.TXT01_IBHMNO1.SetValue(this.CBH01_IBHANGCHA.GetValue().ToString().Substring(0, 4));
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NL2462",
                                        this.CBH01_IBHANGCHA.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_IBHMNO2.SetValue(this.CBH01_IBHANGCHA.GetValue().ToString().Substring(4, 2) + Set_Fill4(dt.Rows[0]["COUNT"].ToString()));
                }

                // 입고차수
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NKM459",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_IBBLSEQ.SetValue(dt.Rows[0]["IBBLSEQ"].ToString());
                }
            }

            UP_IPBLvarInit();

            UP_IPBL_SUIJEBLF_SetData();
            UP_IPBL_USIJEGOF_SetData();
            UP_IPBL_USIJKHAF_SetData();
            UP_IPBL_IBJNHWAJU_SetData();

            if(fsWK_GUBUN3 == "NEW")  //신규
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_US_959C1522",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLSEQ.GetValue().ToString().Trim(),
                                        Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ","").Trim()),
                                        this.TXT01_IBHMNO1.GetValue().ToString(),
                                        this.TXT01_IBHMNO2.GetValue().ToString(),
                                        this.TXT01_IBHBLNO.GetValue().ToString(),
                                        this.CBH01_IBGUBUN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IBBEJNQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBHWAKQTY.GetValue().ToString().Trim()),
                                        Get_Date(this.MTB01_IBHWAKIL.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.CBH01_IBSOSOK.GetValue().ToString(),
                                        Get_Date(this.MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.TXT01_IBCONTNO.GetValue().ToString(),
                                        this.CBH01_IBBPSAGO.GetValue().ToString(),
                                        this.CBH01_IBUNSONG.GetValue().ToString(),
                                        this.CBO01_IBBHGUBN.GetValue().ToString(),
                                        Get_Date(this.MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                        this.TXT01_IBJNBLSEQ.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBBECHQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBHWCHQTY.GetValue().ToString().Trim()),
                                        this.CBH01_IBWONSAN.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );

                
            }
            else if (fsWK_GUBUN3 == "UPT")  //수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_959D2523",
                                        this.TXT01_IBHMNO1.GetValue().ToString(),
                                        this.TXT01_IBHMNO2.GetValue().ToString(),
                                        this.TXT01_IBHBLNO.GetValue().ToString(),
                                        this.CBH01_IBGUBUN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IBBEJNQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBHWAKQTY.GetValue().ToString().Trim()),
                                        Get_Date(this.MTB01_IBHWAKIL.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.CBH01_IBSOSOK.GetValue().ToString(),
                                        Get_Date(this.MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.TXT01_IBCONTNO.GetValue().ToString(),
                                        this.CBH01_IBBPSAGO.GetValue().ToString(),
                                        this.CBH01_IBUNSONG.GetValue().ToString(),
                                        this.CBO01_IBBHGUBN.GetValue().ToString(),
                                        Get_Date(this.MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                        this.TXT01_IBJNBLSEQ.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBBECHQTY.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_IBHWCHQTY.GetValue().ToString().Trim()),
                                        this.CBH01_IBWONSAN.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLSEQ.GetValue().ToString().Trim(),
                                        Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ","").Trim())
                                        );
            }

            // B/L별 재고파일 정리 USIJEBLF
            UP_IPBL_USIJEBLF_UPT();
            // 재고파일 정리 USIJEGOF
            UP_IPBL_USIJEGOF_UPT();
            // 적하목록 파일 정리 USIJKHAF
            UP_IPBL_USIJKHAF_UPT();
            // 이전화주 정리
            UP_IPBL_IBJNHWAJU_UPT();

            this.DbConnector.ExecuteTranQueryList();
            
            fsWK_GUBUN3 = "UPT";
            UP_USIIPBLF_Search();
            UP_USIIPBLF_TAB_Search();

            if (MessageBox.Show("저장 작업이 완료 되었습니다. 추가 할 DATA가 존재합니까?", "확인", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 신규버튼
                this.BTN64_NEW_Click(null, null);
            }
            else
            {
                UP_USIIPBLF_Run("RUN");
            }

            Set_Cookie();
        }
        #endregion

        #region Description : B/L별 입고관리 저장 체크
        private void BTN64_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRtn;
            DataTable dt = new DataTable();

            if (Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
            {
                this.ShowCustomMessage("'입고일자' 항목은 필수입력 항목입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.MTB01_IBDATE);
                return;
            }
            else if (Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()).Length < 8) // 날짜 필드 8자리 체크
            {
                this.ShowCustomMessage("입고일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.MTB01_IBDATE);
                return;
            }
            else
            {
                bRtn = dateValidateCheck(Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()));

                if (!bRtn)
                {
                    this.ShowCustomMessage("입고일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IBDATE);
                    return;
                }
            }

            if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEJNQTY.GetValue().ToString().Trim())) < 0)
            {
                this.ShowCustomMessage("적하중량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBBEJNQTY);
                return;
            }

            if (Convert.ToDouble(Get_Numeric(this.TXT01_IBHWAKQTY.GetValue().ToString().Trim())) < 0)
            {
                this.ShowCustomMessage("확정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBHWAKQTY);
                return;
            }

            // 날짜 체크
            if (Get_Date(MTB01_IBHWAKIL.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IBHWAKIL.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("확정일을 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IBHWAKIL);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IBHWAKIL.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("확정일을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IBHWAKIL);
                        return;
                    }
                }
            }
            if (Get_Date(MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("보관시작일을 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IBJESTDAT);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("보관시작일을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IBJESTDAT);
                        return;
                    }
                }
            }

            if (Get_Date(MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()) != "")
            {
                if (Get_Date(MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
                {
                    this.ShowCustomMessage("B/L분할일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IBBLDATE);
                    return;
                }
                else
                {
                    bRtn = dateValidateCheck(Get_Date(MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()));

                    if (!bRtn)
                    {
                        this.ShowCustomMessage("B/L분할일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.MTB01_IBBLDATE);
                        return;
                    }
                }
            }

            if (CBH01_IBBPSAGO.GetValue().ToString() == "OK")
            {
                if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEJNQTY.GetValue().ToString().Trim())) != Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())))
                {
                    this.ShowCustomMessage("확정량과 적하중량이 다릅니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.CBH01_IBBPSAGO.CodeText);
                    return;
                }
            }

            // 적하목록 등록 체크
            // 원화주인 경우만 체크
            if (TXT01_IBBLHSN.GetValue().ToString().Trim() == "0")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9C3DG560",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.CBH01_IBHWAJU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("적하목록이 등록되지 않았습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            // 계약번호 체크 (존재 유무 체크, 계약기간 만료 확인) 신규인 경우만 확인
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_992FZ178",
                                    Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()),
                                    CBH01_IBHWAJU.GetValue().ToString(),
                                    TXT01_IBCONTNO.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("계약번호를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBCONTNO);
                return;
            }

            // 계약번호 체크 (항차, 곡종, 화주가 같은 경우 조출,체선료 비율이 다른 경우 등록 불가)
            // 현재 계약번호 조출,체선료 비율 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_98KIG132",
                                    this.TXT01_IBCONTNO.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            double dCNDISBIYUL = 0;
            double dCNDEMBIYUL = 0;

            if (dt.Rows.Count > 0)
            {
                dCNDISBIYUL = Convert.ToDouble(dt.Rows[0]["CNDISBIYUL"].ToString());
                dCNDEMBIYUL = Convert.ToDouble(dt.Rows[0]["CNDEMBIYUL"].ToString());

                // 항차, 곡종, 화주가 같은 자료의 조출, 체선료 비율 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_98KIE131",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLSEQ.GetValue().ToString().Trim(),
                                        Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ","").Trim()),
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBHWAJU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((dCNDISBIYUL != Convert.ToDouble(dt.Rows[i]["CNDISBIYUL"].ToString())) || (dCNDEMBIYUL != Convert.ToDouble(dt.Rows[i]["CNDEMBIYUL"].ToString())))
                    {
                        this.ShowCustomMessage("계약번호를 확인하세요.[조체선료 비율이 다른 자료가 존재합니다]", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_IBCONTNO);
                        return;
                    }
                }
            }
            else
            {   
                this.ShowCustomMessage("계약번호를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBCONTNO);
                return;
            }

            // B/L별 입고관리 원산지 체크
            // 항처, 곡종, 화주, BL, MSN, HSN이 같으면 원산지도 같음.

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94NFW442",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBBLHSN.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (fsWK_GUBUN3 == "NEW")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["IBWONSAN"].ToString() != this.CBH01_IBWONSAN.GetValue().ToString())
                    {
                        this.ShowCustomMessage("원산지를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.CBH01_IBWONSAN.CodeText);
                        return;
                    }
                }
            }
            else
            {
                if (dt.Rows.Count > 1)
                {
                    if (dt.Rows[0]["IBWONSAN"].ToString() != this.CBH01_IBWONSAN.GetValue().ToString())
                    {
                        this.ShowCustomMessage("원산지를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.CBH01_IBWONSAN.CodeText);
                        return;
                    }
                }
            }

            if (fsWK_GUBUN3 == "NEW")
            {
                //// 화물번호 체크
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_US_94NG7443",
                //                        this.TXT01_IBHMNO1.GetValue().ToString(),
                //                        this.TXT01_IBHMNO2.GetValue().ToString());

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.ShowCustomMessage("이미 등록된 입출고번호입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    e.Successed = false;
                //    SetFocus(this.TXT01_IBHMNO1);
                //    return;
                //}
            }

            if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) != 0)
            {
                if (Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) == 0)
                {
                    this.ShowCustomMessage("분할확정입고량을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBHWIPQTY);
                    return;
                }
            }
            else
            {
                if (Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) != 0)
                {
                    this.ShowCustomMessage("분할배정입고량을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBBEIPQTY);
                    return;
                }
            }

            fdIBBECHQTY = 0;
            fdIBHWCHQTY = 0;
            fdJBBECHQTY = 0;
            fdJBHWCHQTY = 0;

            fdJGBECHQTY = 0;
            fdJGHWCHQTY = 0;

            // 처음에 분할배정 및 확정량을 입력 한 후
            // 수정시 분할배정 및 확정량을 0으로 하면 삭제만 가능하도록 함.
            if (fdPRE_IBBEIPQTY > 0)
            {
                if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) <= 0)
                {
                    this.ShowCustomMessage("분할배정입고량을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBBEIPQTY);
                    return;
                }
            }

            if (fdPRE_IBHWIPQTY > 0)
            {
                if (Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) <= 0)
                {
                    this.ShowCustomMessage("분할확정입고량을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBHWIPQTY);
                    return;
                }
            }

            if (Get_Date(this.MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()) != "" || this.CBH01_IBJNHWAJU.GetValue().ToString() != "" ||
                this.TXT01_IBJNBLHSN.GetValue().ToString() != "" || this.TXT01_IBJNBLSEQ.GetValue().ToString() != "")
            {
                if (Convert.ToDouble(Get_Numeric(this.TXT01_IBJNBLSEQ.GetValue().ToString())) > 0)
                {
                    if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) == 0)
                    {
                        this.ShowCustomMessage("분할배정입고량을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_IBBEIPQTY);
                        return;
                    }

                    if (Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) == 0)
                    {
                        this.ShowCustomMessage("분할확정입고량을 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_IBHWIPQTY);
                        return;
                    }
                }
            }

            // 적하중량 , 분할입고 배정량 동시입력 체크
            if (Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim())) > 0 && Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim())) > 0)
            {
                this.ShowCustomMessage("적하중량과 분할입고 배정량은 같이 입력할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBBEJNQTY);
                return;
            }
            else if (Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim())) == 0 && Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim())) == 0)
            {
                this.ShowCustomMessage("적하중량과 분할입고 배정량중 하나는 입력하여야 합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBBEJNQTY);
                return;
            }
            // 확정량 , 분할입고 확정량 동시입력 체크
            if (Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) > 0 && Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) > 0)
            {
                this.ShowCustomMessage("확정량과 분할입고 확정량은 같이 입력할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBHWAKQTY);
                return;
            }
            else if (Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) == 0 && Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) == 0)
            {
                this.ShowCustomMessage("확정량과 분할입고 확정량중 하나는 입력되어야 합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBHWAKQTY);
                return;
            }

            string sIBHMNO1 = string.Empty;
            string sIBHMNO2 = string.Empty;

            double dIBBEJNQTY = 0;
            double dIBHWAKQTY = 0;

            if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) != 0 && Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) != 0)
            {
                if (Get_Date(this.MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
                {
                    this.ShowCustomMessage("B/L 분할일자를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_IBBLDATE);
                    return;
                }

                if (this.CBH01_IBJNHWAJU.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("이전화주를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.CBH01_IBJNHWAJU.CodeText);
                    return;
                }

                if (this.TXT01_IBJNBLHSN.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("이전HSN 번호를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLHSN);
                    return;
                }

                if (this.TXT01_IBJNBLSEQ.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("이전입고차수를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLSEQ);
                    return;
                }
                // 배정량
                this.TXT01_IBBEJNQTY.SetValue("0");
                // 확정량
                this.TXT01_IBHWAKQTY.SetValue("0");

                // 이전화주 데이터 조회 (USIIPBLF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NHN453",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                        this.TXT01_IBJNBLSEQ.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if ((fsWK_GUBUN3 == "NEW") || (fsPRE_IBJNHWAJU == this.CBH01_IBJNHWAJU.GetValue().ToString() && fsPRE_IBJNBLHSN == this.TXT01_IBJNBLHSN.GetValue().ToString() && fsPRE_IBJNBLSEQ == this.TXT01_IBJNBLSEQ.GetValue().ToString()))
                    {
                        // 분할배정출고량
                        fdIBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));
                        // 분할확정출고량
                        fdIBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));
                    }
                    else
                    {
                        // 분할배정출고량
                        fdIBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));
                        // 분할확정출고량
                        fdIBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                    }
                    
                    // 배정량
                    dIBBEJNQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBEJNQTY"].ToString().Trim())));
                    // 확정량
                    dIBHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWAKQTY"].ToString().Trim())));

                    sIBHMNO1 = dt.Rows[0]["IBHMNO1"].ToString();
                    sIBHMNO2 = dt.Rows[0]["IBHMNO2"].ToString();

                    if (dIBBEJNQTY < fdIBBECHQTY)
                    {
                        this.ShowCustomMessage("분할입고배정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_IBBEIPQTY);
                        return;
                    }

                    if (dIBHWAKQTY < fdIBHWCHQTY)
                    {
                        this.ShowCustomMessage("분할입고확정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_IBHWIPQTY);
                        return;
                    }
                }
                else
                {
                    this.ShowCustomMessage("원B/L에 대한 데이터가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLHSN);
                    return;
                }

                // 이전화주 데이터 조회 (USIJEBLF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NHY454",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                        sIBHMNO1,
                                        sIBHMNO2);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if ((fsWK_GUBUN3 == "NEW") || (fsPRE_IBJNHWAJU == this.CBH01_IBJNHWAJU.GetValue().ToString() && fsPRE_IBJNBLHSN == this.TXT01_IBJNBLHSN.GetValue().ToString() && fsPRE_IBJNBLSEQ == this.TXT01_IBJNBLSEQ.GetValue().ToString()))
                    {
                        // 분할배정출고량
                        fdJBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));
                        // 분할확정출고량
                        fdJBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));
                    }
                    else
                    {
                        fdJBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));
                        // 분할확정출고량
                        fdJBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                    }
                }
                else
                {
                    this.ShowCustomMessage("원B/L에 대한 데이터가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLHSN);
                    return;
                }

                // 이전화주 데이터 조회 (USIJEGOF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NI4455",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if ((fsWK_GUBUN3 == "NEW") || (fsPRE_IBJNHWAJU == this.CBH01_IBJNHWAJU.GetValue().ToString() && fsPRE_IBJNBLHSN == this.TXT01_IBJNBLHSN.GetValue().ToString() && fsPRE_IBJNBLSEQ == this.TXT01_IBJNBLSEQ.GetValue().ToString()))
                    {
                        // 분할배정출고량
                        fdJGBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));
                        // 분할확정출고량
                        fdJGHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));
                    }
                    else
                    {
                        // 분할배정출고량
                        fdJGBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));
                        // 분할확정출고량
                        fdJGHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                    }
                }
                else
                {
                    this.ShowCustomMessage("원B/L에 대한 데이터가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLHSN);
                    return;
                }
            }

            double dIPTOTQTY = 0;
            double dTOTHWAKQTY = 0;

            dIBHWAKQTY = 0;

            // 입고파일 확정량 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94NIW456",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dIPTOTQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["IPTOTQTY"].ToString()));
            }

            // B/L별 입고파일 확정량 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94NIW457",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dIBHWAKQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["IBHWAKQTY"].ToString()));
            }

            dTOTHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",dIBHWAKQTY - fdIBHWAKQTY + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));

            if (dIPTOTQTY < dTOTHWAKQTY)
            {
                this.ShowCustomMessage("입고 관리의 확정량보다 많습니다. 확정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_IBHWAKQTY);
                return;
            }

            // 분할출고 확정량 > 확정량 체크
            if (Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) > 0)
            {
                if (Convert.ToDouble(Get_Numeric(TXT01_IBHWCHQTY.GetValue().ToString().Trim())) > Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())))
                {
                    this.ShowCustomMessage("확정량보다 분할출고 확정량이 많습니다. 확정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBHWAKQTY);
                    return;
                }
            }
            // 분할출고 배정량 > 적하중량 체크
            if (Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim())) > 0)
            {
                if (Convert.ToDouble(Get_Numeric(TXT01_IBBECHQTY.GetValue().ToString().Trim())) > Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim())))
                {
                    this.ShowCustomMessage("적하중량보다 분할출고 배정량이 많습니다. 적하중량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBBEJNQTY);
                    return;
                }
            }

            // 통관량 > (분할입고)확정량 체크
            double dIBHWAKQTYHAP = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) + Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_99GEZ220",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBHMNO1.GetValue().ToString(),
                                    this.TXT01_IBHMNO2.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            double dCSQTY = 0;

            if (dt.Rows.Count > 0)
            {
                dCSQTY = Convert.ToDouble(dt.Rows[0]["CSQTY"].ToString());
            }

            if (dCSQTY > dIBHWAKQTYHAP)
            {

                if (Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) > 0)
                {
                    this.ShowCustomMessage("확정량보다 통관량이 많습니다. 확정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBHWAKQTY);
                    return;
                }
                if (Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) > 0)
                {
                    this.ShowCustomMessage("분할입고 확정량보다 통관량이 많습니다. 분할입고 확정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBHWIPQTY);
                    return;
                }
            }
            // B/L 분할인 경우 원 B/L에 대한 통관량 체크
            
            if (Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_95TEP644",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBJNBLHSN.GetValue().ToString().Trim(),
                                        fsPRE_IBHMNO1,
                                        fsPRE_IBHMNO2);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) > Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString()))
                    {
                        this.ShowCustomMessage("분할입고 확정량보다 통관량이 많습니다. 분할입고 확정량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_IBHWIPQTY);
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : B/L별 입고관리 삭제버튼
        private void BTN64_REM_Click(object sender, EventArgs e)
        {
            UP_IPBLvarInit();

            UP_IPBL_SUIJEBLF_SetData();
            UP_IPBL_USIJEGOF_SetData();
            UP_IPBL_USIJKHAF_SetData();
            UP_IPBL_IBJNHWAJU_SetData();

            // B/L별 입고관리 삭제
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_94MF5423", 
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBBLSEQ.GetValue().ToString().Trim(),
                                    Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ","").Trim()));

            

            // B/L별 재고파일 정리 USIJEBLF 
            UP_IPBL_USIJEBLF_UPT();
            // 재고파일 정리 USIJEGOF 
            UP_IPBL_USIJEGOF_UPT();
            // 적하목록 파일 정리 USIJKHAF
            UP_IPBL_USIJKHAF_UPT();
            // 이전화주 정리
            UP_IPBL_IBJNHWAJU_UPT();

            this.DbConnector.ExecuteTranQueryList();

            this.CBH01_IBHANGCHA.SetValue("");
            this.CBH01_IBGOKJONG.SetValue("");
            this.CBH01_IBHWAJU.SetValue("");
            this.TXT01_IBBLNO.SetValue("");
            this.TXT01_IBBLMSN.SetValue("");
            this.TXT01_IBBLHSN.SetValue("");
            this.TXT01_IBBLSEQ.SetValue("");
            this.MTB01_IBDATE.SetValue("");
            this.TXT01_IBHMNO1.SetValue("");
            this.TXT01_IBHMNO2.SetValue("");

            UP_FieldVisible("INIT");
            UP_FieldClear("B/L별입고");
            fsWK_GUBUN3 = "NEW";

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
            fsIBBPSAGO = "";
            fsIBUNSONG = "900";
            this.CBH01_IBHANGCHA.CodeText.Focus();
        }
        #endregion

        #region Description : B/L별 입고관리 삭제 체크
        private void BTN64_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 데이터 다시 조회 후 삭제 작업 진행
            UP_USIIPGOF_Run();

            DataTable dt = new DataTable();

            fdIBBECHQTY = 0;
            fdIBHWCHQTY = 0;
            fdJBBECHQTY = 0;
            fdJBHWCHQTY = 0;
            fdJGBECHQTY = 0;
            fdJGHWCHQTY = 0;

            if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) != 0 && Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) != 0)
            {
                string sIBHMNO1 = string.Empty;
                string sIBHMNO2 = string.Empty;

                // B/L별 입고관리 B/L분할 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NJC458",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBBLSEQ.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("B/L분할 데이터가 존재하여 삭제할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBBLHSN);
                    return;
                }

                // 이전화주 데이터 조회 (USIIPBLF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NHN453",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                        this.TXT01_IBJNBLSEQ.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 분할배정출고량
                    fdIBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBECHQTY"].ToString().Trim()) - fdPRE_IBBEIPQTY));
                    // 분할확정출고량
                    fdIBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWCHQTY"].ToString().Trim()) - fdPRE_IBHWIPQTY));

                    sIBHMNO1 = dt.Rows[0]["IBHMNO1"].ToString();
                    sIBHMNO2 = dt.Rows[0]["IBHMNO2"].ToString();
                }
                else
                {
                    this.ShowCustomMessage("원B/L에 대한 데이터가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLHSN);
                    return;
                }

                // 이전화주 데이터 조회 (USIJEBLF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NHY454",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                        this.TXT01_IBBLNO.GetValue().ToString(),
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                        sIBHMNO1,
                                        sIBHMNO2);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 분할배정출고량
                    fdJBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBECHQTY"].ToString().Trim()) - fdPRE_IBBEIPQTY));
                    // 분할확정출고량
                    fdJBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString().Trim()) - fdPRE_IBHWIPQTY));
                }
                else
                {
                    this.ShowCustomMessage("원B/L에 대한 데이터가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLHSN);
                    return;
                }

                // 이전화주 데이터 조회 (USIJEGOF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94NI4455",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 분할배정출고량
                    fdJGBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGBECHQTY"].ToString().Trim()) - fdPRE_IBBEIPQTY));
                    // 분할확정출고량
                    fdJGHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGHWCHQTY"].ToString().Trim()) - fdPRE_IBHWIPQTY));
                }
                else
                {
                    this.ShowCustomMessage("원B/L에 대한 데이터가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_IBJNBLHSN);
                    return;
                }
            }

            // B/L별 재고파일 재고량 - 확정량 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94JHX408",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBHMNO1.GetValue().ToString(),
                                    this.TXT01_IBHMNO2.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                double dJEGOQTY = Convert.ToDouble(dt.Rows[0]["JBJEGOQTY"]);

                if (Convert.ToDouble(String.Format("{0,9:N3}", dJEGOQTY - Convert.ToDouble(Get_Numeric(this.TXT01_IBHWAKQTY.GetValue().ToString().Trim())))) < 0)
                {
                    this.ShowCustomMessage("출고내역이 존재하여 삭제할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            // 통관 데이터 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_97BII042",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBBLHSN.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {   
                this.ShowCustomMessage("통관내역이 존재하여 삭제할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            fsWK_GUBUN3 = "DEL";
        }
        #endregion

        #region Description : B/L별 입고관리 - 입항조회 코드헬프
        private void BTN64_SILOCODEHELP04_Click(object sender, EventArgs e)
        {
            TYUSGB004S popup = new TYUSGB004S(CBH01_IBHANGCHA.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_IBHANGCHA.SetValue(popup.fsIHHANGCHA);
                this.CBH01_IBGOKJONG.SetValue(popup.fsIHGOKJONG1);

                this.SetFocus(this.CBH01_IBHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : B/L별 입고관리 - B/L분할 내역 조회 코드헬프
        private void BTN64_SILOCODEHELP12_Click(object sender, EventArgs e)
        {
            TYUSGB008S popup = new TYUSGB008S(CBH01_IBHANGCHA.GetValue().ToString(),
                                              CBH01_IBGOKJONG.GetValue().ToString(),
                                              TXT01_IBBLNO.GetValue().ToString(),
                                              TXT01_IBBLMSN.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Get_Date(this.MTB01_IBBLDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
                {
                    MTB01_IBBLDATE.SetValue(Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()));
                }

                this.CBH01_IBJNHWAJU.SetValue(popup.fsIBHWAJU);
                this.TXT01_IBJNBLHSN.SetValue(popup.fsIBBLHSN);
                this.TXT01_IBJNBLSEQ.SetValue(popup.fsIBBLSEQ);
                this.TXT01_IBBEIPQTY.SetValue(popup.fsIBBEJNQTY);
                this.TXT01_IBHWIPQTY.SetValue(popup.fsIBHWAKQTY);

                fsPRE_IBHMNO1 = popup.fsIBHMNO1;
                fsPRE_IBHMNO2 = popup.fsIBHMNO2;

                this.SetFocus(this.TXT01_IBBEIPQTY);
            }
        }

        private void MTB01_IBBLDATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                BTN64_SILOCODEHELP12_Click(null, null);
            }
        }
        #endregion

        #region Descriptoin : B/L별 입고관리 계약번호 조회 버튼
        private void BTN64_CONTNO_Click(object sender, EventArgs e)
        {
            TYUSGB005S popup = new TYUSGB005S(Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()),
                                              this.CBH01_IBHWAJU.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_IBCONTNO.SetValue(popup.fsCONTNO);

                this.SetFocus(this.CBH01_IBBPSAGO.CodeText);
            }
        }
        #endregion

        #region Description : USIJEBLF 데이터 조회
        private void UP_IPBL_SUIJEBLF_SetData()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94MHY435",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBBLHSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBHMNO1.GetValue().ToString(),
                                    this.TXT01_IBHMNO2.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fdBL_JBBEJNQTY = Convert.ToDouble(dt.Rows[0]["JBBEJNQTY"].ToString());  //  배정량
                fdBL_JBHWAKQTY = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString());  //  확정량
                fdBL_JBYDQTY = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString());      //  양도량
                fdBL_JBYSQTY = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString());      //  양수량
                fdBL_JBYSYDQTY = Convert.ToDouble(dt.Rows[0]["JBYSYDQTY"].ToString());  //  양수분양도량
                fdBL_JBCSQTY = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString());      //  통관수량
                fdBL_JBCHQTY = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString());      //  출고수량
                fdBL_JBYSCHQTY = Convert.ToDouble(dt.Rows[0]["JBYSCHQTY"].ToString());  //  양수출고량
                fdBL_JBJANQTY = Convert.ToDouble(dt.Rows[0]["JBJANQTY"].ToString());    //  잔량
                fdBL_JBCSJANQTY = Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString());//  통관잔량
                fdBL_JBYSJANQTY = Convert.ToDouble(dt.Rows[0]["JBYSJANQTY"].ToString());//  양수출고잔량
                fdBL_JBJEGOQTY = Convert.ToDouble(dt.Rows[0]["JBJEGOQTY"].ToString());  //  재고량
                fdBL_JBBEIPQTY = Convert.ToDouble(dt.Rows[0]["JBBEIPQTY"].ToString());  // 분할배정입고량
                fdBL_JBHWIPQTY = Convert.ToDouble(dt.Rows[0]["JBHWIPQTY"].ToString());  // 분할확정입고량
                fdBL_JBHWCHQTY = Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString());  // 분할확정출고량

                if (fsWK_GUBUN3 == "NEW")
                {
                    //배정량 = 배정량
                    fdBL_JBBEJNQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JBBEJNQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim()))));
                    // 분할배정입고량
                    fdBL_JBBEIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));

                    //확정량 = 확정량
                    fdBL_JBHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JBHWAKQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));
                    // 분할확정입고량
                    fdBL_JBHWIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));

                    fsBLSAVEGB1 = "수정";
                }
                else if (fsWK_GUBUN3 == "UPT")
                {
                    //배정량 = 배정량
                    fdBL_JBBEJNQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JBBEJNQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim())) - fdIBBEJNQTY));
                    // 분할배정입고량
                    fdBL_JBBEIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));

                    //확정량 = 확정량
                    fdBL_JBHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JBHWAKQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) - fdIBHWAKQTY));
                    // 분할확정입고량
                    fdBL_JBHWIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));

                    fsBLSAVEGB1 = "수정";
                }
                else if (fsWK_GUBUN3 == "DEL")
                {
                    //배정량 = 배정량
                    fdBL_JBBEJNQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JBBEJNQTY - Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim()))));
                    // 분할배정입고량
                    fdBL_JBBEIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEIPQTY - Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));

                    //확정량 = 확정량
                    fdBL_JBHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JBHWAKQTY - Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));
                    // 분할확정입고량
                    fdBL_JBHWIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWIPQTY - Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                }

                // 잔량     = (확정량 + 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                fdBL_JBJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY + fdBL_JBHWIPQTY - fdBL_JBHWCHQTY) - (fdBL_JBYDQTY + fdBL_JBCHQTY)));

                // 통관잔량 = (확정량 + 분할확정입고량 - 분할확정출고량) - 통관수량
                fdBL_JBCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY + fdBL_JBHWIPQTY - fdBL_JBHWCHQTY) - fdBL_JBCSQTY));

                // 재고량   = (확정량 + 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                fdBL_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY + fdBL_JBHWIPQTY - fdBL_JBHWCHQTY + fdBL_JBYSQTY) - (fdBL_JBYDQTY + fdBL_JBYSYDQTY + fdBL_JBCHQTY + fdBL_JBYSCHQTY)));

                if (fsWK_GUBUN3 == "DEL")
                {
                    if ((fdBL_JBBEJNQTY != 0) || (fdBL_JBHWAKQTY != 0) || (fdBL_JBYDQTY != 0) || (fdBL_JBYSQTY != 0) ||
                            (fdBL_JBYSYDQTY != 0) || (fdBL_JBCSQTY != 0) || (fdBL_JBCHQTY != 0) || (fdBL_JBYSCHQTY != 0) ||
                            (fdBL_JBJANQTY != 0) || (fdBL_JBJEGOQTY != 0))
                    {
                        fsBLSAVEGB1 = "수정";
                    }
                    else
                    {
                        fsBLSAVEGB1 = "삭제";
                    }
                }
            }
            else
            {
                // 배정량 = 배정량
                fdBL_JBBEJNQTY = Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim()));
                // 분할배정입고량
                fdBL_JBBEIPQTY = Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim()));

                // 확정량 = 확정량
                fdBL_JBHWAKQTY = Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()));
                // 분할확정입고량
                fdBL_JBHWIPQTY = Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()));

                // 잔량     = (확정량 + 분할확정입고량) - (양도량 + 출고수량)
                fdBL_JBJANQTY = Convert.ToDouble(String.Format("{0,9:N3}",(fdBL_JBHWAKQTY + fdBL_JBHWIPQTY) - (fdBL_JBYDQTY + fdBL_JBCHQTY)));

                // 통관잔량 = (확정량 + 분할확정입고량) - 통관수량
                fdBL_JBCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}",(fdBL_JBHWAKQTY + fdBL_JBHWIPQTY) - fdBL_JBCSQTY));

                // 재고량   = (확정량 + 분할확정입고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                fdBL_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}",(fdBL_JBHWAKQTY + fdBL_JBHWIPQTY + fdBL_JBYSQTY) - (fdBL_JBYDQTY + fdBL_JBYSYDQTY + fdBL_JBCHQTY + fdBL_JBYSCHQTY)));

                fsBLSAVEGB1 = "등록";
            }
        }
        #endregion

        #region Description : USIJEGOF 데이터 조회
        private void UP_IPBL_USIJEGOF_SetData()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94QEP466",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBHWAJU.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fdBL_JGBEJNQTY = Convert.ToDouble(dt.Rows[0]["JGBEJNQTY"].ToString());  // 배정량
                fdBL_JGHWAKQTY = Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString());  // 확정량
                fdBL_JGYDQTY = Convert.ToDouble(dt.Rows[0]["JGYDQTY"].ToString());      // 양도량
                fdBL_JGYSQTY = Convert.ToDouble(dt.Rows[0]["JGYSQTY"].ToString());      // 양수량
                fdBL_JGYSYDQTY = Convert.ToDouble(dt.Rows[0]["JGYSYDQTY"].ToString());  // 양수분양도량
                fdBL_JGCSQTY = Convert.ToDouble(dt.Rows[0]["JGCSQTY"].ToString());      // 통관수량
                fdBL_JGCHQTY = Convert.ToDouble(dt.Rows[0]["JGCHQTY"].ToString());      // 출고수량
                fdBL_JGYSCHQTY = Convert.ToDouble(dt.Rows[0]["JGYSCHQTY"].ToString());  // 양수출고량
                fdBL_JGJANQTY = Convert.ToDouble(dt.Rows[0]["JGJANQTY"].ToString());    // 잔량
                fdBL_JGCSJANQTY = Convert.ToDouble(dt.Rows[0]["JGCSJANQTY"].ToString());// 통관잔량
                fdBL_JGYSJANQTY = Convert.ToDouble(dt.Rows[0]["JGYSJANQTY"].ToString());// 양수출고잔량
                fdBL_JGJEGOQTY = Convert.ToDouble(dt.Rows[0]["JGJEGOQTY"].ToString());  // 재고량
                fdBL_JGBEIPQTY = Convert.ToDouble(dt.Rows[0]["JGBEIPQTY"].ToString());  // 분할배정입고량
                fdBL_JGHWIPQTY = Convert.ToDouble(dt.Rows[0]["JGHWIPQTY"].ToString());  // 분할확정입고량
                fdBL_JGHWCHQTY = Convert.ToDouble(dt.Rows[0]["JGHWCHQTY"].ToString());  // 분할확정출고량

                if (fsWK_GUBUN3 == "NEW")
                {
                    // 배정량 = 배정량
                    fdBL_JGBEJNQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JGBEJNQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim()))));
                    // 분할입고배정량
                    fdBL_JGBEIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JGBEIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));
                    // 확정량 = 확정량
                    fdBL_JGHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JGHWAKQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));
                    // 분할확정입고량
                    fdBL_JGHWIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JGHWIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));

                    fsBLSAVEGB2 = "수정";
                }
                else if (fsWK_GUBUN3 == "UPT")
                {
                    // 배정량 = 배정량
                    fdBL_JGBEJNQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JGBEJNQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim())) - fdIBBEJNQTY));
                    // 분할입고배정량
                    fdBL_JGBEIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JGBEIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));
                    // 확정량 = 확정량
                    fdBL_JGHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JGHWAKQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) - fdIBHWAKQTY));
                    // 분할확정입고량
                    fdBL_JGHWIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JGHWIPQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));

                    fsBLSAVEGB2 = "수정";
                }
                else if (fsWK_GUBUN3 == "DEL")
                {
                    // 배정량 = 배정량
                    fdBL_JGBEJNQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JGBEJNQTY - Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim()))));
                    // 분할배정입고량
                    fdBL_JGBEIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JGBEIPQTY - Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));
                    // 확정량 = 확정량
                    fdBL_JGHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JGHWAKQTY - Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));
                    // 분할확정입고량
                    fdBL_JGHWIPQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JGHWIPQTY - Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                }
                // 잔량     = (확정량 + 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                fdBL_JGJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY + fdBL_JGHWIPQTY - fdBL_JGHWCHQTY) - (fdBL_JGYDQTY + fdBL_JGCHQTY)));

                // 통관잔량 = (확정량 + 분할확정입고량 - 분할확정출고량) - 통관수량
                fdBL_JGCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY + fdBL_JGHWIPQTY - fdBL_JGHWCHQTY) - fdBL_JGCSQTY));

                // 재고량   = (확정량 + 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                fdBL_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY + fdBL_JGHWIPQTY - fdBL_JGHWCHQTY + fdBL_JGYSQTY) - (fdBL_JGYDQTY + fdBL_JGYSYDQTY + fdBL_JGCHQTY + fdBL_JGYSCHQTY)));

                if (fsWK_GUBUN3 == "DEL")
                {
                    if ((fdBL_JGBEJNQTY != 0) || (fdBL_JGHWAKQTY != 0) || (fdBL_JGYDQTY != 0) || (fdBL_JGYSQTY != 0) ||
                          (fdBL_JGYSYDQTY != 0) || (fdBL_JGCSQTY != 0) || (fdBL_JGCHQTY != 0) || (fdBL_JGYSCHQTY != 0) ||
                          (fdBL_JGJANQTY != 0) || (fdBL_JGJEGOQTY != 0))
                    {
                        fsBLSAVEGB2 = "수정";
                    }
                    else
                    {
                        fsBLSAVEGB2 = "삭제";
                    }
                }
            }
            else
            {
                // 배정량 = 배정량
                fdBL_JGBEJNQTY = Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim()));

                // 확정량 = 확정량
                fdBL_JGHWAKQTY = Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()));

                // 분할확정입고량
                fdBL_JGHWIPQTY = Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()));

                // 분할배정입고량
                fdBL_JGBEIPQTY = Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim()));

                // 잔량     = (확정량 + 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                fdBL_JGJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY + fdBL_JGHWIPQTY - fdBL_JGHWCHQTY) - (fdBL_JGYDQTY + fdBL_JGCHQTY)));

                // 통관잔량 = (확정량 + 분할확정입고량 - 분할확정출고량) - 통관수량
                fdBL_JGCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY + fdBL_JGHWIPQTY - fdBL_JGHWCHQTY) - fdBL_JGCSQTY));

                // 재고량   = (확정량 + 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                fdBL_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY + fdBL_JGHWIPQTY - fdBL_JGHWCHQTY + fdBL_JGYSQTY) - (fdBL_JGYDQTY + fdBL_JGYSYDQTY + fdBL_JGCHQTY + fdBL_JGYSCHQTY)));

                fsBLSAVEGB2 = "등록";
            }
        }
        #endregion

        #region Description : USIJKHAF 데이터 조회
        private void UP_IPBL_USIJKHAF_SetData()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94QFQ472",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (fsWK_GUBUN3 == "NEW")
                {
                    fdBL_JKHWAKQTY = Convert.ToDouble(dt.Rows[0]["JKHWAKQTY"].ToString());    // 확정량
                    // 확정량 = 확정량
                    fdBL_JKHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JKHWAKQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));
                }
                else if (fsWK_GUBUN3 == "UPT")
                {
                    fdBL_JKHWAKQTY = Convert.ToDouble(dt.Rows[0]["JKHWAKQTY"].ToString());    // 확정량
                    // 확정량 = 확정량
                    fdBL_JKHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JKHWAKQTY + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) - fdIBHWAKQTY));
                }
                else if (fsWK_GUBUN3 == "DEL")
                {
                    fdBL_JKHWAKQTY = Convert.ToDouble(dt.Rows[0]["JKHWAKQTY"].ToString());    // 확정량
                    // 확정량 = 확정량
                    fdBL_JKHWAKQTY = Convert.ToDouble(String.Format("{0,9:N3}",fdBL_JKHWAKQTY - Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));
                }
            }
        }
        #endregion

        #region Description : 이전화주 데이터 조회
        private void UP_IPBL_IBJNHWAJU_SetData()
        {
            DataTable dt = new DataTable();

            string sIBHMNO1 = string.Empty;
            string sIBHMNO2 = string.Empty;

            if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) != 0 && Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) != 0)
            {
                // 이전화주, 이전HSN, 이전입고차수가 다른경우에 실행
                if (fsPRE_IBJNHWAJU != this.CBH01_IBJNHWAJU.GetValue().ToString() || fsPRE_IBJNBLHSN != this.TXT01_IBJNBLHSN.GetValue().ToString() || fsPRE_IBJNBLSEQ != this.TXT01_IBJNBLSEQ.GetValue().ToString())
                {
                    if (fsWK_GUBUN3 == "UPT")
                    {
                        // 수정 전 이전화주 데이터 조회 (USIIPBLF)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94NHN453",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    fsPRE_IBJNHWAJU,
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    fsPRE_IBJNBLHSN,
                                    fsPRE_IBJNBLSEQ);

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 분할배정출고량
                            fdIBBECHQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBECHQTY"].ToString().Trim()) - fdPRE_IBBEIPQTY));
                            // 분할확정출고량
                            fdIBHWCHQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWCHQTY"].ToString().Trim()) - fdPRE_IBHWIPQTY));

                            sIBHMNO1 = dt.Rows[0]["IBHMNO1"].ToString();
                            sIBHMNO2 = dt.Rows[0]["IBHMNO2"].ToString();
                        }

                        // 수정 전 이전화주 데이터 조회 (USIJEBLF)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94NHY454",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    fsPRE_IBJNHWAJU,
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    fsPRE_IBJNBLHSN,
                                    sIBHMNO1,
                                    sIBHMNO2);

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 분할배정출고량
                            fdJBBECHQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBECHQTY"].ToString().Trim()) - fdPRE_IBBEIPQTY));
                            // 분할확정출고량
                            fdJBHWCHQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString().Trim()) - fdPRE_IBHWIPQTY));
                        }

                        // 수정 전 이전화주 데이터 조회(USIJEGOF)


                        // ---------------------------------------------

                        // 이전화주 데이터 조회 (USIIPBLF)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94NHN453",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                    this.TXT01_IBJNBLSEQ.GetValue().ToString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            if (fsPRE_IBJNHWAJU == this.CBH01_IBJNHWAJU.GetValue().ToString() && fsPRE_IBJNBLHSN == this.TXT01_IBJNBLHSN.GetValue().ToString() && fsPRE_IBJNBLSEQ == this.TXT01_IBJNBLSEQ.GetValue().ToString())
                            {
                                // 분할배정출고량
                                fdIBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));
                                // 분할확정출고량
                                fdIBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));
                            }
                            else
                            {
                                // 분할배정출고량
                                fdIBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));
                                // 분할확정출고량
                                fdIBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["IBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                            }

                            sIBHMNO1 = dt.Rows[0]["IBHMNO1"].ToString();
                            sIBHMNO2 = dt.Rows[0]["IBHMNO2"].ToString();
                        }

                        // 이전화주 데이터 조회 (USIJEBLF)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94NHY454",
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),
                                    this.CBH01_IBGOKJONG.GetValue().ToString(),
                                    this.CBH01_IBJNHWAJU.GetValue().ToString(),
                                    this.TXT01_IBBLNO.GetValue().ToString(),
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_IBJNBLHSN.GetValue().ToString(),
                                    sIBHMNO1,
                                    sIBHMNO2);

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            if (fsPRE_IBJNHWAJU == this.CBH01_IBJNHWAJU.GetValue().ToString() && fsPRE_IBJNBLHSN == this.TXT01_IBJNBLHSN.GetValue().ToString() && fsPRE_IBJNBLSEQ == this.TXT01_IBJNBLSEQ.GetValue().ToString())
                            {
                                // 분할배정출고량
                                fdJBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));
                                // 분할확정출고량
                                fdJBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));
                            }
                            else
                            {
                                // 분할배정출고량
                                fdJBBECHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBECHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));
                                // 분할확정출고량
                                fdJBHWCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString().Trim()) + Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                            }
                        }

                        // -------------------------------------

                        // 원화주 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94QG7475",
                                                this.CBH01_IBHANGCHA.GetValue().ToString(),         //항차
                                                this.CBH01_IBGOKJONG.GetValue().ToString(),         //곡종
                                                fsPRE_IBJNHWAJU,                                    //수정 전이전화주
                                                this.TXT01_IBBLNO.GetValue().ToString(),            //B/L NO
                                                this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    //MSN
                                                fsPRE_IBJNBLHSN,                                    //수정 전 이전HSN
                                                fsPRE_IBJNBLSEQ);                                   //수정 전 이전입고차수

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            fsPRE_IBDATE = dt.Rows[0]["IBDATE"].ToString();
                            fsPRE_IBHMNO1 = dt.Rows[0]["IBHMNO1"].ToString();
                            fsPRE_IBHMNO2 = dt.Rows[0]["IBHMNO2"].ToString();
                        }

                        // USIJEBLF 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94MHY435",
                                                this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                                this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종
                                                fsPRE_IBJNHWAJU,                                    // 수정 전 이전화주
                                                this.TXT01_IBBLNO.GetValue().ToString(),            // B/L 번호
                                                this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    // MSN
                                                fsPRE_IBJNBLHSN,                                    // 수정 전 HSN
                                                fsPRE_IBHMNO1,                                      // 화물번호1
                                                fsPRE_IBHMNO2);                                     // 화물번호2

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            fdBL_JBHWAKQTY3 = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString());   //  확정량
                            fdBL_JBYDQTY3 = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString());       //  양도량
                            fdBL_JBYSQTY3 = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString());       //  양수량
                            fdBL_JBYSYDQTY3 = Convert.ToDouble(dt.Rows[0]["JBYSYDQTY"].ToString());   //  양수분양도량
                            fdBL_JBCSQTY3 = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString());       //  통관수량
                            fdBL_JBCHQTY3 = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString());       //  출고수량
                            fdBL_JBYSCHQTY3 = Convert.ToDouble(dt.Rows[0]["JBYSCHQTY"].ToString());   //  양수출고량
                            // 분할배정입고량
                            fdBL_JBBEIPQTY3 = Convert.ToDouble(dt.Rows[0]["JBBEIPQTY"].ToString());
                            // 분할확정입고량
                            fdBL_JBHWIPQTY3 = Convert.ToDouble(dt.Rows[0]["JBHWIPQTY"].ToString());
                            // 분할확정출고량
                            fdBL_JBHWCHQTY3 = Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString());

                            // 배정량 = 배정량
                            fdBL_JBBEJNQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEJNQTY3 - fdIBBEJNQTY));
                            // 분할배정입고량
                            fdBL_JBBEIPQTY3 = fdPRE_IBBEIPQTY;

                            // 확정량 = 확정량
                            fdBL_JBHWAKQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWAKQTY3 - fdIBHWAKQTY));
                            // 분할확정입고량
                            fdBL_JBHWIPQTY3 = fdPRE_IBHWIPQTY;

                            // 잔량     = (확정량 + 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                            fdBL_JBJANQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY3 + fdBL_JBHWIPQTY3 - fdBL_JBHWCHQTY3) - (fdBL_JBYDQTY3 + fdBL_JBCHQTY3)));

                            // 통관잔량 = (확정량 + 분할확정입고량 - 분할확정출고량) - 통관수량
                            fdBL_JBCSJANQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY3 + fdBL_JBHWIPQTY3 - fdBL_JBHWCHQTY3) - fdBL_JBCSQTY3));

                            // 재고량   = (확정량 + 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                            fdBL_JBJEGOQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY3 + fdBL_JBHWIPQTY3 - fdBL_JBHWCHQTY3 + fdBL_JBYSQTY3) - (fdBL_JBYDQTY3 + fdBL_JBYSYDQTY3 + fdBL_JBCHQTY3 + fdBL_JBYSCHQTY3)));
                        }

                        // 수정 전 이전화주 데이터 조회 (USIJEGOF)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94NI4455",
                                                this.CBH01_IBHANGCHA.GetValue().ToString(),
                                                this.CBH01_IBGOKJONG.GetValue().ToString(),
                                                fsPRE_IBJNHWAJU);

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {   
                            // 분할배정출고량
                            fdJGBECHQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGBECHQTY"].ToString().Trim()) - fdPRE_IBBEIPQTY));
                            // 분할확정출고량
                            fdJGHWCHQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JGHWCHQTY"].ToString().Trim()) - fdPRE_IBHWIPQTY));
                        }

                        // 수정 전 이전화주 USIJEGOF 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94QEP466",
                                                this.CBH01_IBHANGCHA.GetValue().ToString(),
                                                this.CBH01_IBGOKJONG.GetValue().ToString(),
                                                fsPRE_IBJNHWAJU);

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            fdBL_JGHWAKQTY3 = Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString()); // 확정량
                            fdBL_JGYDQTY3 = Convert.ToDouble(dt.Rows[0]["JGYDQTY"].ToString()); // 양도량
                            fdBL_JGYSQTY3 = Convert.ToDouble(dt.Rows[0]["JGYSQTY"].ToString()); // 양수량
                            fdBL_JGYSYDQTY3 = Convert.ToDouble(dt.Rows[0]["JGYSYDQTY"].ToString()); // 양수분양도량
                            fdBL_JGCSQTY3 = Convert.ToDouble(dt.Rows[0]["JGCSQTY"].ToString()); // 통관수량
                            fdBL_JGCHQTY3 = Convert.ToDouble(dt.Rows[0]["JGCHQTY"].ToString()); // 출고수량
                            fdBL_JGYSCHQTY3 = Convert.ToDouble(dt.Rows[0]["JGYSCHQTY"].ToString()); // 양수출고량
                            fdBL_JGJEGOQTY3 = Convert.ToDouble(dt.Rows[0]["JGJEGOQTY"].ToString()); // 재고량
                            // 분할확정입고량
                            fdBL_JGHWIPQTY3 = Convert.ToDouble(dt.Rows[0]["JGHWIPQTY"].ToString());

                            // 잔량     = (확정량 + 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                            fdBL_JGJANQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY3 + fdBL_JGHWIPQTY3 - fdJGHWCHQTY2) - (fdBL_JGYDQTY3 + fdBL_JGCHQTY3)));

                            // 통관잔량 = (확정량 + 분할확정입고량 - 분할확정출고량) - 통관수량
                            fdBL_JGCSJANQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY3 + fdBL_JGHWIPQTY3 - fdJGHWCHQTY2) - fdBL_JGCSQTY3));

                            // 재고량   = (확정량 + 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                            fdBL_JGJEGOQTY3 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY3 + fdBL_JGHWIPQTY3 - fdJGHWCHQTY2 + fdBL_JGYSQTY3) - (fdBL_JGYDQTY3 + fdBL_JGYSYDQTY3 + fdBL_JGCHQTY3 + fdBL_JGYSCHQTY3)));
                        }
                    }
                }

                // -------------------------------------------------------------
                
                // 원화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94QG7475",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),     //항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),     //곡종
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),     //이전화주
                                        this.TXT01_IBBLNO.GetValue().ToString(),        //B/L NO
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),       //MSN
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),     //이전HSN
                                        this.TXT01_IBJNBLSEQ.GetValue().ToString());    //이전입고차수

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsIBDATE = dt.Rows[0]["IBDATE"].ToString();
                    fsIBHMNO1 = dt.Rows[0]["IBHMNO1"].ToString();
                    fsIBHMNO2 = dt.Rows[0]["IBHMNO2"].ToString();
                }

                // USIJEBLF 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94MHY435",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종    
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),         // 이전화주
                                        this.TXT01_IBBLNO.GetValue().ToString(),            // B/L 번호
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    // MSN
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),         // 이전HSN
                                        fsIBHMNO1,                                          // 화물번호1
                                        fsIBHMNO2);                                         // 화물번호2

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (fsWK_GUBUN3 == "NEW" || fsWK_GUBUN3 == "UPT")
                    {
                        fdBL_JBHWAKQTY2 = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString());   //  확정량
                        fdBL_JBYDQTY2 = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString());   //  양도량
                        fdBL_JBYSQTY2 = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString());   //  양수량
                        fdBL_JBYSYDQTY2 = Convert.ToDouble(dt.Rows[0]["JBYSYDQTY"].ToString());   //  양수분양도량
                        fdBL_JBCSQTY2 = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString());   //  통관수량
                        fdBL_JBCHQTY2 = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString());   //  출고수량
                        fdBL_JBYSCHQTY2 = Convert.ToDouble(dt.Rows[0]["JBYSCHQTY"].ToString());   //  양수출고량
                        // 분할배정입고량
                        fdBL_JBBEIPQTY2 = Convert.ToDouble(dt.Rows[0]["JBBEIPQTY"].ToString());
                        // 분할확정입고량
                        fdBL_JBHWIPQTY2 = Convert.ToDouble(dt.Rows[0]["JBHWIPQTY"].ToString());
                        // 분할확정출고량
                        fdBL_JBHWCHQTY2 = Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString());

                        // 이전화주, 이전HSN, 이전입고차수가 같은경우에 실행
                        if (fsPRE_IBJNHWAJU == this.CBH01_IBJNHWAJU.GetValue().ToString() && fsPRE_IBJNBLHSN == this.TXT01_IBJNBLHSN.GetValue().ToString() && fsPRE_IBJNBLSEQ == this.TXT01_IBJNBLSEQ.GetValue().ToString())
                        {
                            //배정량 = 배정량
                            fdBL_JBBEJNQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEJNQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim())) - fdIBBEJNQTY));
                            // 분할배정입고량
                            fdBL_JBBEIPQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEIPQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim())) - fdPRE_IBBEIPQTY));

                            //확정량 = 확정량
                            fdBL_JBHWAKQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWAKQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim())) - fdIBHWAKQTY));
                            // 분할확정입고량
                            fdBL_JBHWIPQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWIPQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim())) - fdPRE_IBHWIPQTY));
                            
                        }
                        else
                        {
                            //배정량 = 배정량
                            fdBL_JBBEJNQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEJNQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBBEJNQTY.GetValue().ToString().Trim()))));

                            // 분할배정입고량
                            fdBL_JBBEIPQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEIPQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBBEIPQTY.GetValue().ToString().Trim()))));

                            //확정량 = 확정량
                            fdBL_JBHWAKQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWAKQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBHWAKQTY.GetValue().ToString().Trim()))));

                            // 분할확정입고량
                            fdBL_JBHWIPQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWIPQTY2 + Convert.ToDouble(Get_Numeric(TXT01_IBHWIPQTY.GetValue().ToString().Trim()))));
                        }

                        // 잔량     = (확정량 - 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                        fdBL_JBJANQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY2 - fdBL_JBHWIPQTY2 - fdBL_JBHWCHQTY2) - (fdBL_JBYDQTY2 + fdBL_JBCHQTY2)));

                        // 통관잔량 = (확정량 - 분할확정입고량 - 분할확정출고량) - 통관수량
                        fdBL_JBCSJANQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY2 - fdBL_JBHWIPQTY2 - fdBL_JBHWCHQTY2) - fdBL_JBCSQTY2));

                        // 재고량   = (확정량 - 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                        fdBL_JBJEGOQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY2 - fdBL_JBHWIPQTY2 - fdBL_JBHWCHQTY2 + fdBL_JBYSQTY2) - (fdBL_JBYDQTY2 + fdBL_JBYSYDQTY2 + fdBL_JBCHQTY2 + fdBL_JBYSCHQTY2)));
                    }
                    else if (fsWK_GUBUN3 == "DEL")
                    {
                        fdBL_JBHWAKQTY2 = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString());   //  확정량
                        fdBL_JBYDQTY2 = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString());   //  양도량
                        fdBL_JBYSQTY2 = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString());   //  양수량
                        fdBL_JBYSYDQTY2 = Convert.ToDouble(dt.Rows[0]["JBYSYDQTY"].ToString());   //  양수분양도량
                        fdBL_JBCSQTY2 = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString());   //  통관수량
                        fdBL_JBCHQTY2 = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString());   //  출고수량
                        fdBL_JBYSCHQTY2 = Convert.ToDouble(dt.Rows[0]["JBYSCHQTY"].ToString());   //  양수출고량
                        // 분할배정입고량
                        fdBL_JBBEIPQTY2 = Convert.ToDouble(dt.Rows[0]["JBBEIPQTY"].ToString());
                        // 분할확정입고량
                        fdBL_JBHWIPQTY2 = Convert.ToDouble(dt.Rows[0]["JBHWIPQTY"].ToString());
                        // 분할확정출고량
                        fdBL_JBHWCHQTY2 = Convert.ToDouble(dt.Rows[0]["JBHWCHQTY"].ToString());

                        // 배정량 = 배정량
                        fdBL_JBBEJNQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBBEJNQTY2 - fdIBBEJNQTY));
                        // 분할배정입고량
                        fdBL_JBBEIPQTY2 = fdPRE_IBBEIPQTY;

                        // 확정량 = 확정량
                        fdBL_JBHWAKQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", fdBL_JBHWAKQTY2 - fdIBHWAKQTY));
                        // 분할확정입고량
                        fdBL_JBHWIPQTY2 = fdPRE_IBHWIPQTY;

                        // 잔량     = (확정량 + 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                        fdBL_JBJANQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY2 + fdBL_JBHWIPQTY2 - fdBL_JBHWCHQTY2) - (fdBL_JBYDQTY2 + fdBL_JBCHQTY2)));

                        // 통관잔량 = (확정량 + 분할확정입고량 - 분할확정출고량) - 통관수량
                        fdBL_JBCSJANQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY2 + fdBL_JBHWIPQTY2 - fdBL_JBHWCHQTY2) - fdBL_JBCSQTY2));

                        // 재고량   = (확정량 + 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                        fdBL_JBJEGOQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JBHWAKQTY2 + fdBL_JBHWIPQTY2 - fdBL_JBHWCHQTY2 + fdBL_JBYSQTY2) - (fdBL_JBYDQTY2 + fdBL_JBYSYDQTY2 + fdBL_JBCHQTY2 + fdBL_JBYSCHQTY2)));
                    }
                }
                

                // USIJEGOF 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94QEP466",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),
                                        this.CBH01_IBJNHWAJU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fdBL_JGHWAKQTY2 = Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString()); // 확정량
                    fdBL_JGYDQTY2 = Convert.ToDouble(dt.Rows[0]["JGYDQTY"].ToString()); // 양도량
                    fdBL_JGYSQTY2 = Convert.ToDouble(dt.Rows[0]["JGYSQTY"].ToString()); // 양수량
                    fdBL_JGYSYDQTY2 = Convert.ToDouble(dt.Rows[0]["JGYSYDQTY"].ToString()); // 양수분양도량
                    fdBL_JGCSQTY2 = Convert.ToDouble(dt.Rows[0]["JGCSQTY"].ToString()); // 통관수량
                    fdBL_JGCHQTY2 = Convert.ToDouble(dt.Rows[0]["JGCHQTY"].ToString()); // 출고수량
                    fdBL_JGYSCHQTY2 = Convert.ToDouble(dt.Rows[0]["JGYSCHQTY"].ToString()); // 양수출고량
                    fdBL_JGJEGOQTY2 = Convert.ToDouble(dt.Rows[0]["JGJEGOQTY"].ToString()); // 재고량
                    // 분할확정입고량
                    fdBL_JGHWIPQTY2 = Convert.ToDouble(dt.Rows[0]["JGHWIPQTY"].ToString());

                    // 잔량     = (확정량 + 분할확정입고량 - 분할확정출고량) - (양도량 + 출고수량)
                    fdBL_JGJANQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY2 + fdBL_JGHWIPQTY2 - fdJGHWCHQTY) - (fdBL_JGYDQTY2 + fdBL_JGCHQTY2)));

                    // 통관잔량 = (확정량 + 분할확정입고량 - 분할확정출고량) - 통관수량
                    fdBL_JGCSJANQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY2 + fdBL_JGHWIPQTY2 - fdJGHWCHQTY) - fdBL_JGCSQTY2));

                    // 재고량   = (확정량 + 분할확정입고량 - 분할확정출고량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                    fdBL_JGJEGOQTY2 = Convert.ToDouble(String.Format("{0,9:N3}", (fdBL_JGHWAKQTY2 + fdBL_JGHWIPQTY2 - fdJGHWCHQTY + fdBL_JGYSQTY2) - (fdBL_JGYDQTY2 + fdBL_JGYSYDQTY2 + fdBL_JGCHQTY2 + fdBL_JGYSCHQTY2)));
                }
            }
        }
        #endregion

        #region Description : USIJEBLF 정리
        private void UP_IPBL_USIJEBLF_UPT()
        {   
            if (fsBLSAVEGB1 == "등록")
            {
                this.DbConnector.Attach("TY_P_US_94QEA465",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         //  1 항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         //  2 곡종
                                        this.CBH01_IBHWAJU.GetValue().ToString(),           //  3 화주    
                                        this.TXT01_IBBLNO.GetValue().ToString().Trim(),     //  4 B/L번호
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    //  5 MSN
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim(),    //  6 HSN
                                        this.TXT01_IBHMNO1.GetValue().ToString(),           //  7 화물번호1
                                        this.TXT01_IBHMNO2.GetValue().ToString(),           //  8 화물번호2
                                        fdBL_JBBEJNQTY,                                     //  9 배정량
                                        fdBL_JBHWAKQTY,                                     // 10 확정량
                                        0,                                                  // 11 양도량
                                        0,                                                  // 12 양수량
                                        0,                                                  // 13 양수분양도량
                                        0,                                                  // 14 통관수량
                                        0,                                                  // 15 출고수량
                                        0,                                                  // 16 양수출고량
                                        fdBL_JBJANQTY,                                      // 17 잔량
                                        fdBL_JBCSJANQTY,                                    // 18 통관잔량
                                        0,                                                  // 18 양수출고잔량
                                        fdBL_JBJEGOQTY,                                     // 20 재고량
                                        this.CBH01_IBSOSOK.GetValue().ToString(),           // 21 협회
                                        Get_Date(this.MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()),    // 22 보관시작일
                                        this.TXT01_IBCONTNO.GetValue().ToString().Replace("-", ""),     // 23 계약번호
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),         // 24 화주
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),         // 25 HSN
                                        fdBL_JBBEIPQTY,                                     // 26 분할입고배정량
                                        0,                                                  // 27 분할출고배정량
                                        fdBL_JBHWIPQTY,                                     // 28 분할입고확정량
                                        0,                                                  // 29 분할출고확정량
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );
            }
            else if (fsBLSAVEGB1 == "수정")
            {
                this.DbConnector.Attach("TY_P_US_94QE4464",
                                        fdBL_JBBEJNQTY,                                     //배정량
                                        fdBL_JBHWAKQTY,                                     //확정량
                                        fdBL_JBJANQTY,                                      //잔량
                                        fdBL_JBCSJANQTY,                                    //통관잔량
                                        fdBL_JBBEIPQTY,                                     //분할입고배정량
                                        fdBL_JBHWIPQTY,                                     //분할입고확정량
                                        fdBL_JBJEGOQTY,                                     //재고량
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),         //화주
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),         //HSN
                                        this.CBH01_IBSOSOK.GetValue().ToString(),           //협회
                                        Get_Date(this.MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()),     //보관시작일
                                        this.TXT01_IBCONTNO.GetValue().ToString().Replace("-", ""),     //계약번호
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         //항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         //곡종
                                        this.CBH01_IBHWAJU.GetValue().ToString(),           //화주    
                                        this.TXT01_IBBLNO.GetValue().ToString().Trim(),     //B/L번호
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    //MSN
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim(),    //HSN
                                        this.TXT01_IBHMNO1.GetValue().ToString(),           //화물번호1
                                        this.TXT01_IBHMNO2.GetValue().ToString()            //화물번호2
                                        );
            }
            else if (fsBLSAVEGB1 == "삭제")
            {
                this.DbConnector.Attach("TY_P_US_94QE2463",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         //항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         //곡종
                                        this.CBH01_IBHWAJU.GetValue().ToString(),           //화주    
                                        this.TXT01_IBBLNO.GetValue().ToString().Trim(),     //B/L번호
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    //MSN
                                        this.TXT01_IBBLHSN.GetValue().ToString().Trim(),    //HSN
                                        this.TXT01_IBHMNO1.GetValue().ToString(),           //화물번호1
                                        this.TXT01_IBHMNO2.GetValue().ToString()            //화물번호2
                                        );
            }
        }
        #endregion

        #region Description : USIJEGOF 정리
        private void UP_IPBL_USIJEGOF_UPT()
        {
            if (fsBLSAVEGB2 == "등록")
            {
                this.DbConnector.Attach("TY_P_US_94QF5469",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         // 1 항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         // 2 곡종
                                        this.CBH01_IBHWAJU.GetValue().ToString(),           // 3 화주    
                                        fdBL_JGBEJNQTY,                                     // 4 배정량
                                        fdBL_JGHWAKQTY,                                     // 5 확정량
                                        "0",                                                // 6 양도량
                                        "0",                                                // 7 양수량
                                        "0",                                                // 8 양수분양도량
                                        "0",                                                // 9 통관수량
                                        "0",                                                // 10 출고수량
                                        "0",                                                // 11 양수출고량
                                        fdBL_JGJANQTY,                                      // 12 잔량
                                        fdBL_JGCSJANQTY,                                    // 13 통관잔량
                                        "0",                                                // 14 양수출고잔량
                                        fdBL_JGJEGOQTY,                                     // 15 재고량
                                        this.CBH01_IBSOSOK.GetValue().ToString(),           // 16협회
                                        Get_Date(this.MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()),    // 17 보관시작일
                                        this.TXT01_IBCONTNO.GetValue().ToString().Replace("-", ""),     // 18 계약번호
                                        fdBL_JGBEIPQTY,                                     // 19 분할입고배정량
                                        "0",                                                // 20 분할출고배정량
                                        fdBL_JGHWIPQTY,                                     // 21 분할입고확정량
                                        "0",                                                // 22 분할출고확정량
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );
            }
            else if (fsBLSAVEGB2 == "수정")
            {
                this.DbConnector.Attach("TY_P_US_94QFA470",
                                        fdBL_JGBEJNQTY,                                     //배정량
                                        fdBL_JGHWAKQTY,                                     //확정량
                                        fdBL_JGJANQTY,                                      //잔량
                                        fdBL_JGCSJANQTY,                                    //통관잔량
                                        fdBL_JGBEIPQTY,                                     //분할입고배정량
                                        fdBL_JGHWIPQTY,                                     //분할입고확정량
                                        fdBL_JGJEGOQTY,                                     //재고량
                                        this.CBH01_IBSOSOK.GetValue().ToString(),           //협회
                                        Get_Date(this.MTB01_IBJESTDAT.GetValue().ToString().Replace(" ", "").Trim()),    //보관시작일
                                        this.TXT01_IBCONTNO.GetValue().ToString().Replace("-", ""),     //계약번호
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         //항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         //곡종
                                        this.CBH01_IBHWAJU.GetValue().ToString()            //화주    
                                        );
            }
            else if (fsBLSAVEGB2 == "삭제")
            {
                this.DbConnector.Attach("TY_P_US_94QFB471",
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         //항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         //곡종
                                        this.CBH01_IBHWAJU.GetValue().ToString()            //화주    
                                        );
            }
        }
        #endregion

        #region Description : USIJKHAF 정리
        private void UP_IPBL_USIJKHAF_UPT()
        {
            this.DbConnector.Attach("TY_P_US_94QFW473",
                                    fdBL_JKHWAKQTY,                                     //확정량
                                    TYUserInfo.EmpNo.ToString().Trim(),
                                    this.CBH01_IBHANGCHA.GetValue().ToString(),         //항차
                                    this.TXT01_IBBLMSN.GetValue().ToString().Trim()            //MSN  
                                    );
        }
        #endregion

        #region Description : 이전화주 정리
        private void UP_IPBL_IBJNHWAJU_UPT()
        {   
            if (Convert.ToDouble(Get_Numeric(this.TXT01_IBBEIPQTY.GetValue().ToString().Trim())) != 0 && Convert.ToDouble(Get_Numeric(this.TXT01_IBHWIPQTY.GetValue().ToString().Trim())) != 0)
            {
                // 이전화주, 이전HSN, 이전입고차수가 다른경우에 실행
                if (fsPRE_IBJNHWAJU != this.CBH01_IBJNHWAJU.GetValue().ToString() || fsPRE_IBJNBLHSN != this.TXT01_IBJNBLHSN.GetValue().ToString() || fsPRE_IBJNBLSEQ != this.TXT01_IBJNBLSEQ.GetValue().ToString())
                {
                    if (fsWK_GUBUN3 == "UPT")
                    {
                        // USIIPBLF
                        this.DbConnector.Attach("TY_P_US_94QGC476",
                                                fdIBBECHQTY2,                                       // 분할출고배정량
                                                fdIBHWCHQTY2,                                       // 분할출고확정량
                                                TYUserInfo.EmpNo.ToString().Trim(),                 //
                                                this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                                this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종
                                                fsPRE_IBJNHWAJU,                                    // 수정 전 이전화주
                                                this.TXT01_IBBLNO.GetValue().ToString(),            // B/L NO
                                                this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    // MSN
                                                fsPRE_IBJNBLHSN,                                    // 수정 전 이전HSN
                                                fsPRE_IBJNBLSEQ,                                    // 수정 전 이전입고차수
                                                fsPRE_IBDATE);                                          // 입고일자

                        // USIJEBLF
                        this.DbConnector.Attach("TY_P_US_94QGT478",
                                                fdBL_JBJANQTY3,                                     // 잔량
                                                fdBL_JBCSJANQTY3,                                   // 통관잔량
                                                fdBL_JBJEGOQTY3,                                    // 재고량
                                                fdJBBECHQTY2,                                       // 분할출고배정량
                                                fdJBHWCHQTY2,                                       // 분할출고확정량
                                                TYUserInfo.EmpNo.ToString().Trim(),                 //
                                                this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                                this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종
                                                fsPRE_IBJNHWAJU,                                    // 수정 전 이전화주
                                                this.TXT01_IBBLNO.GetValue().ToString(),            // B/L번호
                                                this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    // MSN
                                                fsPRE_IBJNBLHSN,                                    // 수정 전 HSN
                                                fsPRE_IBHMNO1,                                      // 화물번호1
                                                fsPRE_IBHMNO2);                                     // 화물번호2

                        // USIJEGOF
                        this.DbConnector.Attach("TY_P_US_94QHD479",
                                                fdBL_JGJANQTY3,                                     // 잔량
                                                fdBL_JGCSJANQTY3,                                   // 통관잔량
                                                fdBL_JGJEGOQTY3,                                    // 재고량
                                                fdJGBECHQTY2,                                       // 분할출고배정량
                                                fdJGHWCHQTY2,                                       // 분할출고확정량
                                                TYUserInfo.EmpNo.ToString().Trim(),                 //
                                                this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                                this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종
                                                fsPRE_IBJNHWAJU);                                   // 수정 전 이전화주
                    }
                }
                
                // USIIPBLF
                this.DbConnector.Attach("TY_P_US_94QGC476",
                                        fdIBBECHQTY,                                        // 분할출고배정량
                                        fdIBHWCHQTY,                                        // 분할출고확정량
                                        TYUserInfo.EmpNo.ToString().Trim(),                 //
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),         // 이전화주
                                        this.TXT01_IBBLNO.GetValue().ToString(),            // B/L NO
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    // MSN
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),         // 이전HSN
                                        this.TXT01_IBJNBLSEQ.GetValue().ToString(),         // 이전입고차수
                                        fsIBDATE);                                          // 입고일자

                // USIJEBLF
                this.DbConnector.Attach("TY_P_US_94QGT478",
                                        fdBL_JBJANQTY2,                                     // 잔량
                                        fdBL_JBCSJANQTY2,                                   // 통관잔량
                                        fdBL_JBJEGOQTY2,                                    // 재고량
                                        fdJBBECHQTY,                                        // 분할출고배정량
                                        fdJBHWCHQTY,                                        // 분할출고확정량
                                        TYUserInfo.EmpNo.ToString().Trim(),                 //
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종
                                        this.CBH01_IBJNHWAJU.GetValue().ToString(),         // 이전화주
                                        this.TXT01_IBBLNO.GetValue().ToString(),            // B/L번호
                                        this.TXT01_IBBLMSN.GetValue().ToString().Trim(),    // MSN
                                        this.TXT01_IBJNBLHSN.GetValue().ToString(),         // HSN
                                        fsIBHMNO1,                                          // 화물번호1
                                        fsIBHMNO2);                                         // 화물번호2

                // USIJEGOF
                this.DbConnector.Attach("TY_P_US_94QHD479",
                                        fdBL_JGJANQTY2,                                     // 잔량
                                        fdBL_JGCSJANQTY2,                                   // 통관잔량
                                        fdBL_JGJEGOQTY2,                                    // 재고량
                                        fdJGBECHQTY,                                        // 분할출고배정량
                                        fdJGHWCHQTY,                                        // 분할출고확정량
                                        TYUserInfo.EmpNo.ToString().Trim(),                 //
                                        this.CBH01_IBHANGCHA.GetValue().ToString(),         // 항차
                                        this.CBH01_IBGOKJONG.GetValue().ToString(),         // 곡종
                                        this.CBH01_IBJNHWAJU.GetValue().ToString());        // 화주
            }
        }
        #endregion

        #region Description : B/L별 입고관리 변수 초기화
        private void UP_IPBLvarInit()
        {
            fdBL_JBBEJNQTY = 0;  // 배정량
            fdBL_JBHWAKQTY = 0;  // 확정량
            fdBL_JBYDQTY = 0;  // 양도량
            fdBL_JBYSQTY = 0;  // 양수량
            fdBL_JBYSYDQTY = 0;  // 양수분양도량
            fdBL_JBCSQTY = 0;  // 통관수량
            fdBL_JBCHQTY = 0;  // 출고수량
            fdBL_JBYSCHQTY = 0;  // 양수출고량
            fdBL_JBJANQTY = 0;  // 잔량
            fdBL_JBCSJANQTY = 0;  // 통관잔량
            fdBL_JBYSJANQTY = 0;  // 양수출고잔량
            fdBL_JBJEGOQTY = 0;  // 재고량
            fdBL_JBBEIPQTY = 0;  // 분할배정입고량
            fdBL_JBHWIPQTY = 0;  // 분할확정입고량
            fdBL_JBHWCHQTY = 0;  // 분할확정출고량

            fdBL_JBBEJNQTY2 = 0;  // 배정량
            fdBL_JBHWAKQTY2 = 0;  // 확정량
            fdBL_JBYDQTY2 = 0;  // 양도량
            fdBL_JBYSQTY2 = 0;  // 양수량
            fdBL_JBYSYDQTY2 = 0;  // 양수분양도량
            fdBL_JBCSQTY2 = 0;  // 통관수량
            fdBL_JBCHQTY2 = 0;  // 출고수량
            fdBL_JBYSCHQTY2 = 0;  // 양수출고량
            fdBL_JBJANQTY2 = 0;  // 잔량
            fdBL_JBCSJANQTY2 = 0;  // 통관잔량
            fdBL_JBJEGOQTY2 = 0;  // 재고량
            fdBL_JBBEIPQTY2 = 0;  // 분할배정입고량
            fdBL_JBHWIPQTY2 = 0;  // 분할확정입고량
            fdBL_JBHWCHQTY2 = 0;  // 분할확정출고량 

            fdBL_JBBEJNQTY3 = 0;  // 배정량
            fdBL_JBHWAKQTY3 = 0;  // 확정량
            fdBL_JBYDQTY3 = 0;  // 양도량
            fdBL_JBYSQTY3 = 0;  // 양수량
            fdBL_JBYSYDQTY3 = 0;  // 양수분양도량
            fdBL_JBCSQTY3 = 0;  // 통관수량
            fdBL_JBCHQTY3 = 0;  // 출고수량
            fdBL_JBYSCHQTY3 = 0;  // 양수출고량
            fdBL_JBJANQTY3 = 0;  // 잔량
            fdBL_JBCSJANQTY3 = 0;  // 통관잔량
            fdBL_JBJEGOQTY3 = 0;  // 재고량
            fdBL_JBBEIPQTY3 = 0;  // 분할배정입고량
            fdBL_JBHWIPQTY3 = 0;  // 분할확정입고량
            fdBL_JBHWCHQTY3 = 0;  // 분할확정출고량 

            fdBL_JGBEJNQTY = 0;  // 배정량
            fdBL_JGHWAKQTY = 0;  // 확정량
            fdBL_JGYDQTY = 0;  // 양도량
            fdBL_JGYSQTY = 0;  // 양수량
            fdBL_JGYSYDQTY = 0;  // 양수분양도량
            fdBL_JGCSQTY = 0;  // 통관수량
            fdBL_JGCHQTY = 0;  // 출고수량
            fdBL_JGYSCHQTY = 0;  // 양수출고량
            fdBL_JGJANQTY = 0;  // 잔량
            fdBL_JGCSJANQTY = 0;  // 통관잔량
            fdBL_JGYSJANQTY = 0;  // 양수출고잔량
            fdBL_JGJEGOQTY = 0;  // 재고량
            fdBL_JGBEIPQTY = 0;   // 분할배정입고량
            fdBL_JGHWIPQTY = 0;   // 분할확정입고량
            fdBL_JGHWCHQTY = 0;   // 분할확정출고량

            fdBL_JGHWAKQTY2 = 0;  // 확정량
            fdBL_JGYDQTY2 = 0;  // 양도량
            fdBL_JGYSQTY2 = 0;  // 양수량
            fdBL_JGYSYDQTY2 = 0;  // 양수분양도량
            fdBL_JGCSQTY2 = 0;  // 통관수량
            fdBL_JGCHQTY2 = 0;  // 출고수량
            fdBL_JGYSCHQTY2 = 0;  // 양수출고량
            fdBL_JGJANQTY2 = 0;  // 잔량
            fdBL_JGCSJANQTY2 = 0;  // 통관잔량
            fdBL_JGJEGOQTY2 = 0;  // 재고량
            fdBL_JGHWIPQTY2 = 0;   // 분할확정입고량

            fsIBDATE = string.Empty;
            fsIBHMNO1 = string.Empty;
            fsIBHMNO2 = string.Empty;
        }
        #endregion

        #region Description : 신규 등록시 원산지, 협회 자동 입력
        private void UP_Set_IPBLWONSAN()
        {
            // 입항관리 원산지1 조회
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_941ET214", this.CBH01_IBHANGCHA.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_IBWONSAN.SetValue(dt.Rows[0]["IHWONSAN1"].ToString());
            }
            // 거래처관리 거래처코드(화주코드)로 협회 코드 조회
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_935AG979", this.CBH01_IBHWAJU.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_IBSOSOK.SetValue(dt.Rows[0]["VNSOSOK"].ToString());
            }
        }
        #endregion

        #region Description : 입고구분 이벤트
        private void CBH01_IBGUBUN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.CBH01_IBGUBUN.GetValue().ToString() == "23")
                {
                    this.MTB01_IBBLDATE.SetValue(Get_Date(this.MTB01_IBDATE.GetValue().ToString()));
                }
            }
        }
        #endregion

        #endregion

        #region Descriptoin : 통관관리

        #region Description : 통관관리 전체조회
        private void UP_USICUSTF_Search()
        {
            this.FPS91_TY_S_US_95FF7563.Initialize();
            this.FPS91_TY_S_US_95FF7562.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95FF3560",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_95FF7562.SetValue(dt);
        }
        #endregion

        #region Description : 통관관리 조회
        private void UP_USICUSTF_TAB_Search()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95FF6561",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_95FF7563.SetValue(dt);
        }
        #endregion

        #region Description : 통관관리 확인
        private void UP_USICUSTF_Run()
        {
            DataTable dt = new DataTable();

            // B/L별 재고 체크 USIJEBLF
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95TEP644",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSBLMSN.GetValue().ToString().Trim(),
                this.TXT01_CSBLHSN.GetValue().ToString().Trim(),
                this.TXT01_CSHMNO1.GetValue().ToString(),
                this.TXT01_CSHMNO2.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //  1 = 배정량
                TXT01_JBBEJNQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBEJNQTY"].ToString().Trim())));
                //  2 = 확정량            
                TXT01_JBHWAKQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString().Trim())));
                //  3 = 양도량
                TXT01_JBYDQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString().Trim())));
                //  4 = 양수량
                TXT01_JBYSQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString().Trim())));
                //  5 = 통관수량
                TXT01_JBCSQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString().Trim())));
                //  6 = 통관잔량, 통관사항 통관수량
                TXT01_JBCSJANQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString().Trim())));
                TXT01_CSQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString().Trim())));
                //  7 = 출고수량
                TXT01_JBCHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString().Trim())));
                //  8 = 재고량
                TXT01_JBJEGOQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBJEGOQTY"].ToString().Trim())));
                //  9 = 화물번호１
                TXT01_CSHMNO1.SetValue(dt.Rows[0]["JBHMNO1"].ToString());
                // 10 = 화물번호２
                TXT01_CSHMNO2.SetValue(dt.Rows[0]["JBHMNO2"].ToString());
                // 11 = 협회
                fsJBSOSOK = dt.Rows[0]["JBSOSOK"].ToString();
                // 12 = 보관시작일
                fsJBJESTDAT = dt.Rows[0]["JBJESTDAT"].ToString();
                // 13 = 계약번호
                fsJBCONTNO = dt.Rows[0]["JBCONTNO"].ToString();
                // 14 = 신고수량
                TXT01_CSSINQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JBBEJNQTY"].ToString().Trim()) - Convert.ToDouble(dt.Rows[0]["CSSINQTY"].ToString().Trim())));
            }

            // 통관일별 재고파일 출고량, 재고량 조회

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95THO649",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSBLMSN.GetValue().ToString().Trim(),
                this.TXT01_CSBLHSN.GetValue().ToString().Trim(),
                Get_Date(this.MTB01_CSDATE.GetValue().ToString().Replace(" ","").Trim()),
                this.TXT01_CSSEQ.GetValue().ToString().Trim(),
                "",
                "0",
                "0",
                "0"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_SCCHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["SCCHQTY"].ToString().Trim())));
                this.TXT01_JCJEGOQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString().Trim())));
            }
            else
            {
                this.TXT01_SCCHQTY.SetValue("0.000");
                this.TXT01_JCJEGOQTY.SetValue("0.000");
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95FGD565",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSBLMSN.GetValue().ToString().Trim(),
                this.TXT01_CSBLHSN.GetValue().ToString().Trim(),
                Get_Date(this.MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()),
                this.TXT01_CSSEQ.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_CSHANGCHA.SetValue(dt.Rows[0]["CSHANGCHA"].ToString());
                this.CBH01_CSGOKJONG.SetValue(dt.Rows[0]["CSGOKJONG"].ToString());
                this.CBH01_CSHWAJU.SetValue(dt.Rows[0]["CSHWAJU"].ToString());
                this.TXT01_CSBLNO.SetValue(dt.Rows[0]["CSBLNO"].ToString());
                this.TXT01_CSBLMSN.SetValue(dt.Rows[0]["CSBLMSN"].ToString());
                this.TXT01_CSBLHSN.SetValue(dt.Rows[0]["CSBLHSN"].ToString());
                this.MTB01_CSDATE.SetValue(dt.Rows[0]["CSDATE"].ToString());
                this.TXT01_CSSEQ.SetValue(dt.Rows[0]["CSSEQ"].ToString());
                this.TXT01_CSHMNO1.SetValue(dt.Rows[0]["CSHMNO1"].ToString());
                this.TXT01_CSHMNO2.SetValue(dt.Rows[0]["CSHMNO2"].ToString());

                this.TXT01_CSQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["CSQTY"].ToString().Trim())));
                fdCSQTY = Convert.ToDouble(dt.Rows[0]["CSQTY"].ToString());
                this.TXT01_CSCHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["CSCHQTY"].ToString().Trim())));
                this.TXT01_CSJGQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["CSJGQTY"].ToString().Trim())));
                this.TXT01_CSCOSTUS.SetValue(dt.Rows[0]["CSCOSTUS"].ToString());
                this.TXT01_CSCOSTWN.SetValue(dt.Rows[0]["CSCOSTWN"].ToString());
                this.TXT01_CSSINNO.SetValue(dt.Rows[0]["CSSINNO"].ToString());
                this.TXT01_CSSINQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["CSSINQTY"].ToString().Trim())));
                this.CBH01_CSSINNM.SetValue(dt.Rows[0]["CSSINNM"].ToString());
                this.CBH01_CSBANGB.SetValue(dt.Rows[0]["CSBANGB"].ToString());
                this.DTP01_CSEDDATE.SetValue(dt.Rows[0]["CSEDDATE"].ToString());
                this.DTP01_CSYJDATE.SetValue(dt.Rows[0]["CSYJDATE"].ToString());
                this.TXT01_CSYJNUMB.SetValue(dt.Rows[0]["CSYJNUMB"].ToString());

                UP_FieldVisible("UPT");

                fsWK_GUBUN4 = "UPT";

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    SetFocus(this.TXT01_CSQTY);
                };

                tmr.Interval = 100;
                tmr.Start();
            }

            Set_Cookie();
        }
        #endregion

        #region Description : 통관관리 조회 그리드 더블클릭
        private void FPS91_TY_S_US_95FF7563_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_CSHANGCHA.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSHANGCHA").ToString());
            this.CBH01_CSGOKJONG.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSGOKJONG").ToString());
            this.CBH01_CSHWAJU.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSHWAJU").ToString());
            this.TXT01_CSBLNO.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSBLNO").ToString());
            this.TXT01_CSBLMSN.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSBLMSN").ToString());
            this.TXT01_CSBLHSN.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSBLHSN").ToString());
            this.MTB01_CSDATE.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSDATE").ToString());
            this.TXT01_CSSEQ.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSSEQ").ToString());
            this.TXT01_CSHMNO1.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSHMNO1").ToString());
            this.TXT01_CSHMNO2.SetValue(this.FPS91_TY_S_US_95FF7563.GetValue("CSHMNO2").ToString());

            UP_USICUSTF_Run();
        }
        #endregion

        #region Description : 통관관리 전체조회 그리드 더블클릭
        private void FPS91_TY_S_US_95FF7562_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_CSHANGCHA.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHANGCHA").ToString());
            this.CBH01_CSGOKJONG.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSGOKJONG").ToString());
            this.CBH01_CSHWAJU.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHWAJU").ToString());
            this.TXT01_CSBLNO.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSBLNO").ToString());
            this.TXT01_CSBLMSN.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSBLMSN").ToString());
            this.TXT01_CSBLHSN.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSBLHSN").ToString());
            this.MTB01_CSDATE.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSDATE").ToString());
            this.TXT01_CSSEQ.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSSEQ").ToString());
            this.TXT01_CSHMNO1.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHMNO1").ToString());
            this.TXT01_CSHMNO2.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHMNO2").ToString());

            UP_USICUSTF_TAB_Search();

            UP_USICUSTF_Run();
        }
        private void FPS91_TY_S_US_95FF7562_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_CSHANGCHA.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHANGCHA").ToString());
                this.CBH01_CSGOKJONG.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSGOKJONG").ToString());
                this.CBH01_CSHWAJU.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHWAJU").ToString());
                this.TXT01_CSBLNO.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSBLNO").ToString());
                this.TXT01_CSBLMSN.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSBLMSN").ToString());
                this.TXT01_CSBLHSN.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSBLHSN").ToString());
                this.MTB01_CSDATE.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSDATE").ToString());
                this.TXT01_CSSEQ.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSSEQ").ToString());
                this.TXT01_CSHMNO1.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHMNO1").ToString());
                this.TXT01_CSHMNO2.SetValue(this.FPS91_TY_S_US_95FF7562.GetValue("CSHMNO2").ToString());

                UP_USICUSTF_TAB_Search();

                UP_USICUSTF_Run();
            }
        }
        #endregion

        #region Description : 통관관리 신규버튼
        private void BTN65_NEW_Click(object sender, EventArgs e)
        {   
            fsWK_GUBUN4 = "NEW";
            UP_FieldClear("통관");
            UP_FieldVisible("NEW");
            Get_Cookie();

            this.CBH01_CSHWAJU.SetValue("");
            this.TXT01_CSBLNO.SetValue("");
            this.TXT01_CSBLMSN.SetValue("");
            this.TXT01_CSBLHSN.SetValue("");
            this.MTB01_CSDATE.SetValue("");
            this.TXT01_CSSEQ.SetValue("");
            this.TXT01_CSHMNO1.SetValue("");
            this.TXT01_CSHMNO2.SetValue("");

            UP_Set_KeyFocus();

            FPS91_TY_S_US_95FF7563.Initialize();
        }
        #endregion

        #region Description : 통관관리 저장버튼
        private void BTN65_SAV_Click(object sender, EventArgs e)
        {
            if (fsWK_GUBUN4 == "NEW")
            {
                DataTable dt = new DataTable();

                if (TXT01_CSSEQ.GetValue().ToString().Trim() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_95TFQ645",
                        this.CBH01_CSHANGCHA.GetValue().ToString(),
                        this.CBH01_CSGOKJONG.GetValue().ToString(),
                        this.CBH01_CSHWAJU.GetValue().ToString(),
                        this.TXT01_CSBLNO.GetValue().ToString(),
                        this.TXT01_CSBLMSN.GetValue().ToString().Trim(),
                        this.TXT01_CSBLHSN.GetValue().ToString().Trim(),
                        Get_Date(this.MTB01_CSDATE.GetValue().ToString().Replace(" ","").Trim())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_CSSEQ.SetValue(dt.Rows[0]["CSSEQ"].ToString());
                    }
                }

                this.DbConnector.CommandClear();

                // 통관관리 등록
                this.DbConnector.Attach("TY_P_US_95VHG661",
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  1  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  2  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString(), 	     //  3  = 화주
                                        TXT01_CSBLNO.GetValue().ToString(), 	     //  4  = Ｂ／Ｌ번호
                                        Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  5  = ＭＳＮ
                                        Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  6  = ＨＳＮ
                                        Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()), 	                 //  7  = 통관일자
                                        Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim()),  	     //  8  = 통관차수
                                        TXT01_CSHMNO1.GetValue().ToString(),	     //  9  = 화물번호１
                                        TXT01_CSHMNO2.GetValue().ToString(),	     //  10 = 화물번호２
                                        Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()), 	         //  11 = 통관량
                                        Get_Numeric(TXT01_CSCHQTY.GetValue().ToString().Trim()),	     //  12 = 출고량
                                        Get_Numeric(TXT01_CSJGQTY.GetValue().ToString().Trim()),	     //  13 = 재고량
                                        Get_Numeric(TXT01_CSCOSTUS.GetValue().ToString().Trim()),	     //  14 = 감정가($)
                                        Get_Numeric(TXT01_CSCOSTWN.GetValue().ToString().Trim()),	     //  15 = 감정가(\)
                                        TXT01_CSSINNO.GetValue().ToString(),	     //  16 = 신고번호
                                        Get_Numeric(TXT01_CSSINQTY.GetValue().ToString().Trim()),	     //  17 = 신고수량
                                        CBH01_CSSINNM.GetValue().ToString(),     	 //  18 = 관세사
                                        CBH01_CSBANGB.GetValue().ToString(),         //  19 = 반출구분
                                        Get_Date(DTP01_CSEDDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),     //  20 = 종료일자
                                        Get_Date(DTP01_CSYJDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),     //  21 = 반출연장일
                                        TXT01_CSYJNUMB.GetValue().ToString(),	     //  22 = 반출근거
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );

                // 통관일별 재고 등록 
                this.DbConnector.Attach("TY_P_US_95VHM664",
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  1  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  2  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString(), 	     //  3  = 화주
                                        TXT01_CSBLNO.GetValue().ToString(), 	     //  4  = Ｂ／Ｌ번호
                                        Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  5  = ＭＳＮ
                                        Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  6  = ＨＳＮ
                                        Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ","").Trim()), 	                 //  7  = 통관일자
                                        Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim()),  	     //  8  = 통관차수
                                        "",                                          //  9 = 양도화주
                                        "0",                                         // 10 = 양수일자
                                        "0",                                         // 11 = 양수순번
                                        "0",										 // 12 = 양도순번
                                        TXT01_CSHMNO1.GetValue().ToString(),	     // 13 = 화물번호１
                                        TXT01_CSHMNO2.GetValue().ToString(),	     // 14 = 화물번호２
                                        "0", 	                                     // 15 = 양도량
                                        "0",	                                     // 16 = 양수량
                                        "0",	                                     // 17 = 양수분양도량
                                        Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()),	         // 18 = 통관수량
                                        "0",	                                     // 19 = 출고수량
                                        "0",	                                     // 20 = 양수출고량
                                        Get_Numeric(TXT01_CSJGQTY.GetValue().ToString().Trim()),	     // 21 = 재고량
                                        fsJBSOSOK,                          	     // 22 = 협회
                                        fsJBJESTDAT,                                 // 23 = 보관시작일
                                        fsJBCONTNO,                     	         // 24 = 계약번호
                                        CBH01_CSHWAJU.GetValue().ToString(),         // 25 = 원화주
                                        "50",	                                     // 26 = 출고순번
                                        fsIBWONSAN,                           	     // 27 = 원산지
                                        "0",	                                     // 28 = 가상출고			
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );

                // B/L별 재고 수정 
                this.DbConnector.Attach("TY_P_US_95VHS668",
                                        fdJBCSQTY,                                   //  1 = 통관량
                                        fdJBCSJANQTY,                                //  2 = 통관잔량
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  3  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  4  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString(), 	     //  5  = 화주
                                        TXT01_CSBLNO.GetValue().ToString(), 	     //  6  = Ｂ／Ｌ번호
                                        Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  7  = ＭＳＮ
                                        Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  8  = ＨＳＮ
                                        TXT01_CSHMNO1.GetValue().ToString(),	     //  9 = 화물번호１
                                        TXT01_CSHMNO2.GetValue().ToString() 	     // 10 = 화물번호２
                                        
                                        );

                // 화주별 재고 수정
                this.DbConnector.Attach("TY_P_US_95VHT669",
                                        fdJGCSQTY,                                   //  1 = 통관량
                                        fdJGCSJANQTY,                                //  2 = 통관잔량
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  3  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  4  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString()  	     //  5  = 화주
                                        );
                
                // 통관차수 등록,수정
                if(fsCSSAVEGB1 == "등록")
                {
                    this.DbConnector.Attach("TY_P_US_95VIM671",
                                            TXT01_CSHMNO1.GetValue().ToString(),	     
                                            TXT01_CSHMNO2.GetValue().ToString(),	     
                                            Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim())
                                            );
                }
                else if(fsCSSAVEGB1 == "수정")
                {
                    this.DbConnector.Attach("TY_P_US_95VIN672",
                                            Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim()),
                                            TXT01_CSHMNO1.GetValue().ToString(),
                                            TXT01_CSHMNO2.GetValue().ToString()
                                            );
                }

                this.DbConnector.ExecuteTranQueryList();
            }
            else if (fsWK_GUBUN4 == "UPT")
            {
                // 통관관리 수정
                this.DbConnector.Attach("TY_P_US_95VHJ662",
                                        TXT01_CSHMNO1.GetValue().ToString(),	     //  9  = 화물번호１
                                        TXT01_CSHMNO2.GetValue().ToString(),	     //  10 = 화물번호２
                                        Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()), 	         //  11 = 통관량
                                        Get_Numeric(TXT01_CSCHQTY.GetValue().ToString().Trim()),	     //  12 = 출고량
                                        Get_Numeric(TXT01_CSJGQTY.GetValue().ToString().Trim()),	     //  13 = 재고량
                                        Get_Numeric(TXT01_CSCOSTUS.GetValue().ToString().Trim()),	     //  14 = 감정가($)
                                        Get_Numeric(TXT01_CSCOSTWN.GetValue().ToString().Trim()),	     //  15 = 감정가(\)
                                        TXT01_CSSINNO.GetValue().ToString(),	     //  16 = 신고번호
                                        Get_Numeric(TXT01_CSSINQTY.GetValue().ToString().Trim()),	     //  17 = 신고수량
                                        CBH01_CSSINNM.GetValue().ToString(),     	 //  18 = 관세사
                                        CBH01_CSBANGB.GetValue().ToString(),         //  19 = 반출구분
                                        Get_Date(DTP01_CSEDDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),     //  20 = 종료일자
                                        Get_Date(DTP01_CSYJDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),//  21 = 반출연장일
                                        TXT01_CSYJNUMB.GetValue().ToString(),	     //  22 = 반출근거
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  1  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  2  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString(), 	     //  3  = 화주
                                        TXT01_CSBLNO.GetValue().ToString(), 	     //  4  = Ｂ／Ｌ번호
                                        Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  5  = ＭＳＮ
                                        Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  6  = ＨＳＮ
                                        Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()),	                 //  7  = 통관일자
                                        Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim())    	     //  8  = 통관차수
                                        );

                // 통관일별 재고 수정
                this.DbConnector.Attach("TY_P_US_95VHN665",
                                        TXT01_CSHMNO1.GetValue().ToString(),	     // 13 = 화물번호１
                                        TXT01_CSHMNO2.GetValue().ToString(),	     // 14 = 화물번호２
                                        Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()),	         // 18 = 통관수량
                                        Get_Numeric(TXT01_CSJGQTY.GetValue().ToString().Trim()),	     // 21 = 재고량
                                        fsJBSOSOK,                          	     // 22 = 협회
                                        fsJBJESTDAT,                                 // 23 = 보관시작일
                                        fsJBCONTNO,                     	         // 24 = 계약번호
                                        fsIBWONSAN,                           	     // 27 = 원산지
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  1  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  2  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString(), 	     //  3  = 화주
                                        TXT01_CSBLNO.GetValue().ToString(), 	     //  4  = Ｂ／Ｌ번호
                                        Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  5  = ＭＳＮ
                                        Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  6  = ＨＳＮ
                                        Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()),	                 //  7  = 통관일자
                                        Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim()),  	     //  8  = 통관차수
                                        "",                                          //  9 = 양도화주
                                        "0",                                         // 10 = 양수일자
                                        "0",                                         // 11 = 양수순번
                                        "0" 										 // 12 = 양도순번
                                        );

                // B/L별 재고 수정 
                this.DbConnector.Attach("TY_P_US_95VHS668",
                                        fdJBCSQTY,                                   //  1 = 통관량
                                        fdJBCSJANQTY,                                //  2 = 통관잔량
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  3  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  4  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString(), 	     //  5  = 화주
                                        TXT01_CSBLNO.GetValue().ToString(), 	     //  6  = Ｂ／Ｌ번호
                                        Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  7  = ＭＳＮ
                                        Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  8  = ＨＳＮ
                                        TXT01_CSHMNO1.GetValue().ToString(),	     //  9 = 화물번호１
                                        TXT01_CSHMNO2.GetValue().ToString()          // 10 = 화물번호２
                                        );

                // 화주별 재고 수정 
                this.DbConnector.Attach("TY_P_US_95VHT669",
                                        fdJGCSQTY,                                   //  1 = 통관량
                                        fdJGCSJANQTY,                                //  2 = 통관잔량
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        CBH01_CSHANGCHA.GetValue().ToString(),       //  3  = 항차
                                        CBH01_CSGOKJONG.GetValue().ToString(),       //  4  = 곡종
                                        CBH01_CSHWAJU.GetValue().ToString()  	     //  5  = 화주
                                        );

                this.DbConnector.ExecuteTranQueryList();
            }
            
            fsWK_GUBUN4 = "UPT";
            UP_USICUSTF_Search();
            UP_USICUSTF_TAB_Search();
            this.ShowMessage("TY_M_GB_23NAD873");
            UP_USICUSTF_Run();
            //this.SetFocus(TXT01_CSQTY);
            Set_Cookie();
        }
        #endregion

        #region Description : 통관관리 저장 체크
        private void BTN65_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRtn;
            double dCSJGQTY = 0;

            if (Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
            {
                this.ShowCustomMessage("'통관일자' 항목은 필수입력 항목입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.MTB01_CSDATE);
                return;
            }
            else if (Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
            {
                this.ShowCustomMessage("통관일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.MTB01_CSDATE);
                return;
            }
            else
            {
                bRtn = dateValidateCheck(Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()));

                if (!bRtn)
                {
                    this.ShowCustomMessage("통관일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_CSDATE);
                    return;
                }
            }

            if (UP_USICUSTF_Check())
            {
                if (fsWK_GUBUN4 == "NEW")
                {   
                    fdCS_JBCSQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JBCSQTY + Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()))));
                    fdCS_JBCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JBHWAKQTY - fdCS_JBCSQTY));
                    fdCS_JGCSQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JGCSQTY + Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()))));
                    fdCS_JGCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JGHWAKQTY - fdCS_JGCSQTY));
                    if ((fdCS_JBCSJANQTY < 0) || (fdCS_JGCSJANQTY < 0))
                    {
                        this.ShowCustomMessage("총 통관량이 확정량을 초과했습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_CSQTY);
                        return;
                    }
                    else
                    {
                        fdJBCSQTY = fdCS_JBCSQTY;
                        fdJBCSJANQTY = fdCS_JBCSJANQTY;
                        fdJGCSQTY = fdCS_JGCSQTY;
                        fdJGCSJANQTY = fdCS_JGCSJANQTY;
                    }

                    // 통관차수 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_95VIH670",
                        this.TXT01_CSHMNO1.GetValue().ToString(),
                        this.TXT01_CSHMNO2.GetValue().ToString()
                        );

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        fsCSSAVEGB1 = "수정";
                    }
                    else
                    {
                        fsCSSAVEGB1 = "등록";
                    }
                }
                else if (fsWK_GUBUN4 == "UPT")
                {
                    fdCS_JBCSQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JBCSQTY - fdCSQTY + Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()))));
                    fdCS_JBCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JBHWAKQTY - fdCS_JBCSQTY));
                    fdCS_JGCSQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JGCSQTY - fdCSQTY + Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()))));
                    fdCS_JGCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JGHWAKQTY - fdCS_JGCSQTY));
                    if ((fdCS_JBCSJANQTY < 0) || (fdCS_JGCSJANQTY < 0))
                    {
                        this.ShowCustomMessage("총 통관량이 확정량을 초과했습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_CSQTY);
                        return;
                    }
                    else
                        if ((fdCS_JBCSQTY < (fdCS_JBYDQTY + fdCS_JBCHQTY)) || (fdCS_JGCSQTY < (fdCS_JGYDQTY + fdCS_JGCHQTY)))
                        {
                            this.ShowCustomMessage("출고량이 통관량보다 많습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            SetFocus(this.TXT01_CSQTY);
                            return;
                        }
                        else
                        {
                            fdJBCSQTY = fdCS_JBCSQTY;
                            fdJBCSJANQTY = fdCS_JBCSJANQTY;
                            fdJGCSQTY = fdCS_JGCSQTY;
                            fdJGCSJANQTY = fdCS_JGCSJANQTY;
                        }
                }

                dCSJGQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim())) - Convert.ToDouble(Get_Numeric(this.TXT01_CSCHQTY.GetValue().ToString().Trim()))));
                this.TXT01_CSJGQTY.SetValue(String.Format("{0,9:N3}", dCSJGQTY));

                if (!this.ShowMessage("TY_M_GB_23NAD871"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 통관관리 삭제버튼
        private void BTN65_REM_Click(object sender, EventArgs e)
        {
            // 통관관리 삭제
            this.DbConnector.Attach("TY_P_US_95VHJ663",
                                    CBH01_CSHANGCHA.GetValue().ToString(),       //  1  = 항차
                                    CBH01_CSGOKJONG.GetValue().ToString(),       //  2  = 곡종
                                    CBH01_CSHWAJU.GetValue().ToString(), 	     //  3  = 화주
                                    TXT01_CSBLNO.GetValue().ToString(), 	     //  4  = Ｂ／Ｌ번호
                                    Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  5  = ＭＳＮ
                                    Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  6  = ＨＳＮ
                                    Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()), 	                 //  7  = 통관일자
                                    Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim())    	     //  8  = 통관차수
                                    );

            // 통관일별 재고 삭제
            this.DbConnector.Attach("TY_P_US_95VHO666",
                                    CBH01_CSHANGCHA.GetValue().ToString(),       //  1  = 항차
                                    CBH01_CSGOKJONG.GetValue().ToString(),       //  2  = 곡종
                                    CBH01_CSHWAJU.GetValue().ToString(), 	     //  3  = 화주
                                    TXT01_CSBLNO.GetValue().ToString(), 	     //  4  = Ｂ／Ｌ번호
                                    Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  5  = ＭＳＮ
                                    Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  6  = ＨＳＮ
                                    Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()),	                 //  7  = 통관일자
                                    Get_Numeric(TXT01_CSSEQ.GetValue().ToString().Trim()),  	     //  8  = 통관차수
                                    "",                                          //  9 = 양도화주
                                    "0",                                         // 10 = 양수일자
                                    "0",                                         // 11 = 양수순번
                                    "0" 										 // 12 = 양도순번
                                    );

            // B/L별 재고 수정 
            this.DbConnector.Attach("TY_P_US_95VHS668",
                                    fdJBCSQTY,                                   //  1 = 통관량
                                    fdJBCSJANQTY,                                //  2 = 통관잔량
                                    TYUserInfo.EmpNo.ToString().Trim(),
                                    CBH01_CSHANGCHA.GetValue().ToString(),       //  3  = 항차
                                    CBH01_CSGOKJONG.GetValue().ToString(),       //  4  = 곡종
                                    CBH01_CSHWAJU.GetValue().ToString(), 	     //  5  = 화주
                                    TXT01_CSBLNO.GetValue().ToString(), 	     //  6  = Ｂ／Ｌ번호
                                    Get_Numeric(TXT01_CSBLMSN.GetValue().ToString().Trim()),	     //  7  = ＭＳＮ
                                    Get_Numeric(TXT01_CSBLHSN.GetValue().ToString().Trim()), 	  	 //  8  = ＨＳＮ
                                    TXT01_CSHMNO1.GetValue().ToString(),	     //  9 = 화물번호１
                                    TXT01_CSHMNO2.GetValue().ToString()	         // 10 = 화물번호２
                                    );

            // 화주별 재고 수정 
            this.DbConnector.Attach("TY_P_US_95VHT669",
                                    fdJGCSQTY,                                   //  1 = 통관량
                                    fdJGCSJANQTY,                                //  2 = 통관잔량
                                    TYUserInfo.EmpNo.ToString().Trim(),
                                    CBH01_CSHANGCHA.GetValue().ToString(),       //  3  = 항차
                                    CBH01_CSGOKJONG.GetValue().ToString(),       //  4  = 곡종
                                    CBH01_CSHWAJU.GetValue().ToString()  	     //  5  = 화주
                                    );

            this.DbConnector.ExecuteTranQueryList();

            UP_FieldVisible("INIT");
            UP_FieldClear("통관");
            fsWK_GUBUN4 = "NEW";

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
            this.CBH01_CSHANGCHA.CodeText.Focus();
        }
        #endregion

        #region Description : 통관관리 삭제 체크
        private void BTN65_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dCSJGQTY = 0;

            if (UP_USICUSTF_Check())
            {

                if ((fdCS_JCCHQTY != 0) || (fdCS_JCYSCHQTY != 0))
                {
                    this.ShowCustomMessage("출고내역이 존재 합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.CBH01_CSHANGCHA.CodeText);
                    return;
                }
                if ((fdCS_JCYDQTY != 0) || (fdCS_JCYSYDQTY != 0) || (fdCS_JCYSQTY != 0))
                {
                    this.ShowCustomMessage("양수도내역이 존재 합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.CBH01_CSHANGCHA.CodeText);
                    return;
                }

                if (Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim())) != Convert.ToDouble(fdCSQTY))
                {
                    this.ShowCustomMessage("통관량은 수정할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CSQTY);
                    return;
                }
                else
                {
                    fdCS_JBCSQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JBCSQTY - Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()))));
                    fdCS_JBCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JBHWAKQTY - fdCS_JBCSQTY));
                    fdCS_JGCSQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JGCSQTY - Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim()))));
                    fdCS_JGCSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCS_JGHWAKQTY - fdCS_JGCSQTY));

                    if ((fdCS_JBCSQTY < (fdCS_JBYDQTY + fdCS_JBCHQTY)) || (fdCS_JGCSQTY < (fdCS_JGYDQTY + fdCS_JGCHQTY)))
                    {
                        this.ShowCustomMessage("출고량이 통관량보다 많습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        SetFocus(this.TXT01_CSQTY);
                        return;
                    }
                    else
                    {
                        fdJBCSQTY = fdCS_JBCSQTY;
                        fdJBCSJANQTY = fdCS_JBCSJANQTY;
                        fdJGCSQTY = fdCS_JGCSQTY;
                        fdJGCSJANQTY = fdCS_JGCSJANQTY;
                    }
                }

                dCSJGQTY = Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim())) - Convert.ToDouble(Get_Numeric(this.TXT01_CSCHQTY.GetValue().ToString().Trim()))));
                this.TXT01_CSJGQTY.SetValue(String.Format("{0,9:N3}",dCSJGQTY));

                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 통관관리 체크
        private bool UP_USICUSTF_Check()
        {
            fdCS_JBBEJNQTY = 0; //  1 = 배정량
            fdCS_JBHWAKQTY = 0; //  2 = 확정량 
            fdCS_JBYDQTY = 0; //  3 = 양도량
            fdCS_JBYSQTY = 0; //  4 = 양수량
            fdCS_JBCSQTY = 0; //  5 = 통관수량
            fdCS_JBCSJANQTY = 0; //  6 = 통관잔량
            fdCS_JBCHQTY = 0; //  7 = 출고수량
            fdCS_JBJEGOQTY = 0; //  8 = 재고량
            fdCS_JGBEJNQTY = 0; //  1 = 배정량
            fdCS_JGHWAKQTY = 0; //  2 = 확정량
            fdCS_JGYDQTY = 0; //  3 = 양도량
            fdCS_JGYSQTY = 0; //  4 = 양수량
            fdCS_JGYSYDQTY = 0; //  5 = 양수분양도량
            fdCS_JGCSQTY = 0; //  6 = 통관수량
            fdCS_JGCHQTY = 0; //  7 = 출고수량
            fdCS_JGYSCHQTY = 0; //  8 = 양수출고량 
            fdCS_JGJANQTY = 0; //  9 = 잔량 
            fdCS_JGCSJANQTY = 0; // 10 = 통관잔량 
            fdCS_JGYSJANQTY = 0; // 11 = 양수출고잔량
            fdCS_JGJEGOQTY = 0; // 12 = 재고량 

            fdCS_JCJEGOQTY = 0;
            fdCS_JCYSQTY = 0;
            fdCS_JCYDQTY = 0;
            fdCS_JCYSYDQTY = 0;
            fdCS_JCCHQTY = 0;
            fdCS_JCYSCHQTY = 0;

            DataTable dt = new DataTable();

            if (this.TXT01_CSHMNO1.GetValue().ToString() == "" || this.TXT01_CSHMNO2.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("화물번호가 입력되지 않았습니다. B/L별 입고 조회를 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                SetFocus(this.BTN65_SILOCODEHELP05);
                return false;
            }

            if (Convert.ToDouble(Get_Numeric(TXT01_CSQTY.GetValue().ToString().Trim())) <= 0)
            {
                this.ShowCustomMessage("통관량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.TXT01_CSQTY);
                return false;
            }

            if (Convert.ToDouble(Get_Numeric(TXT01_CSSINQTY.GetValue().ToString().Trim())) <= 0)
            {
                this.ShowCustomMessage("신고수량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.TXT01_CSSINQTY);
                return false;
            }

            // B/L별 재고 체크 USIJEBLF
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95TEP644",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSBLMSN.GetValue().ToString().Trim(),
                this.TXT01_CSBLHSN.GetValue().ToString().Trim(),
                this.TXT01_CSHMNO1.GetValue().ToString(),
                this.TXT01_CSHMNO2.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //  1 = 배정량
                fdCS_JBBEJNQTY = Convert.ToDouble(dt.Rows[0]["JBBEJNQTY"].ToString());
                //  2 = 확정량            
                fdCS_JBHWAKQTY = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString());
                //  3 = 양도량
                fdCS_JBYDQTY = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString());
                //  4 = 양수량
                fdCS_JBYSQTY = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString());
                //  5 = 통관수량
                fdCS_JBCSQTY = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString());
                //  6 = 통관잔량
                fdCS_JBCSJANQTY = Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString());
                //  7 = 출고수량
                fdCS_JBCHQTY = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString());
                //  8 = 재고량
                fdCS_JBJEGOQTY = Convert.ToDouble(dt.Rows[0]["JBJEGOQTY"].ToString());
                // 11 = 협회
                fsJBSOSOK = dt.Rows[0]["JBSOSOK"].ToString();
                // 12 = 보관시작일
                fsJBJESTDAT = dt.Rows[0]["JBJESTDAT"].ToString();
                // 13 = 계약번호
                fsJBCONTNO = dt.Rows[0]["JBCONTNO"].ToString();
            }
            else
            {
                this.ShowCustomMessage("입고 파일에 자료가 없습니다.(USIJEBLF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.CBH01_CSHANGCHA.CodeText);
                return false;
            }

            // 화주별 재고 체크 USIJEGOF
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95TKZ651",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //  1 = 배정량
                fdCS_JGBEJNQTY = Convert.ToDouble(dt.Rows[0]["JGBEJNQTY"].ToString());
                //  2 = 확정량            
                fdCS_JGHWAKQTY = Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString());
                //  3 = 양도량
                fdCS_JGYDQTY = Convert.ToDouble(dt.Rows[0]["JGYDQTY"].ToString());
                //  4 = 양수량
                fdCS_JGYSQTY = Convert.ToDouble(dt.Rows[0]["JGYSQTY"].ToString());
                //  5 = 양수분양도량
                fdCS_JGYSYDQTY = Convert.ToDouble(dt.Rows[0]["JGYSYDQTY"].ToString());
                //  6 = 통관수량
                fdCS_JGCSQTY = Convert.ToDouble(dt.Rows[0]["JGCSQTY"].ToString());
                //  7 = 출고수량
                fdCS_JGCHQTY = Convert.ToDouble(dt.Rows[0]["JGCHQTY"].ToString());
                //  8 = 양수출고량
                fdCS_JGYSCHQTY = Convert.ToDouble(dt.Rows[0]["JGYSCHQTY"].ToString());
                //  9 = 잔량
                fdCS_JGJANQTY = Convert.ToDouble(dt.Rows[0]["JGJANQTY"].ToString());
                //  10 = 통관잔량
                fdCS_JGCSJANQTY = Convert.ToDouble(dt.Rows[0]["JGCSJANQTY"].ToString());
                //  11 = 양수출고잔량
                fdCS_JGYSJANQTY = Convert.ToDouble(dt.Rows[0]["JGYSJANQTY"].ToString());
                //  12 = 재고량
                fdCS_JGJEGOQTY = Convert.ToDouble(dt.Rows[0]["JGJEGOQTY"].ToString());
            }
            else
            {
                this.ShowCustomMessage("재고 파일에 자료가 없습니다.(USIJEGOF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.CBH01_CSHANGCHA.CodeText);
                return false;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95THO649",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSBLMSN.GetValue().ToString().Trim(),
                this.TXT01_CSBLHSN.GetValue().ToString().Trim(),
                Get_Date(this.MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()),
                this.TXT01_CSSEQ.GetValue().ToString().Trim(),
                "",
                "0",
                "0",
                "0"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //  4 =  양수량
                fdCS_JCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString());
                //  5 =  양도량
                fdCS_JCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString());
                //  6 =  양수분양도량
                fdCS_JCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString());
                //  7 =  출고수량
                fdCS_JCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString());
                //  8 =  양수출고량
                fdCS_JCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString());

                fdCS_JCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}",(Convert.ToDouble(TXT01_CSQTY.GetValue().ToString().Trim()) + fdCS_JCYSQTY) - (fdCS_JCYDQTY + fdCS_JCYSYDQTY + fdCS_JCCHQTY + fdCS_JCYSCHQTY)));
                fdCJEGOQTY = fdCS_JCJEGOQTY;
            }

            // B/L별 입고파일 재고내역 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_94NG7443",
                this.TXT01_CSHMNO1.GetValue().ToString(),
                this.TXT01_CSHMNO2.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["IBHWAKIL"].ToString() == "")
                {
                    this.ShowCustomMessage("확정되지 않은 재고 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    SetFocus(this.CBH01_CSHANGCHA.CodeText);
                    return false;
                }

                if (Convert.ToInt32(Get_Numeric(Get_Date(this.MTB01_CSDATE.GetValue().ToString().Replace(" ","").Trim()))) < Convert.ToInt32(Get_Numeric(dt.Rows[0]["IBDATE"].ToString())))
                {
                    if (dt.Rows[0]["IBGUBUN"].ToString() != "12")
                    {
                        this.ShowCustomMessage("통관일이 입고일보다 작습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                        SetFocus(this.CBH01_CSHANGCHA.CodeText);
                        return false;
                    }
                }
            }
            else
            {
                this.ShowCustomMessage("입고 파일에 자료가 없습니다.!(USIIPBLF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                SetFocus(this.CBH01_CSHANGCHA.CodeText);
                return false;
            }

            // B/L별 입고관리 원산지 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_94NFW442",
                this.CBH01_CSHANGCHA.GetValue().ToString(),
                this.CBH01_CSGOKJONG.GetValue().ToString(),
                this.CBH01_CSHWAJU.GetValue().ToString(),
                this.TXT01_CSBLNO.GetValue().ToString(),
                this.TXT01_CSBLMSN.GetValue().ToString().Trim(),
                this.TXT01_CSBLHSN.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsIBWONSAN = dt.Rows[0]["IBWONSAN"].ToString();
            }

            return true;
        }
        #endregion

        #region Description : 통관관리 - 입고조회 코드헬프
        private void BTN65_SILOCODEHELP05_Click(object sender, EventArgs e)
        {
            TYUSGB006S popup = new TYUSGB006S(this.CBH01_CSHANGCHA.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_CSHANGCHA.SetValue(popup.fsIBHANGCHA);
                this.CBH01_CSGOKJONG.SetValue(popup.fsIBGOKJONG);

                this.CBH01_CSHWAJU.SetValue(popup.fsIBHWAJU);
                this.TXT01_CSBLNO.SetValue(popup.fsIBBLNO);
                this.TXT01_CSBLMSN.SetValue(popup.fsIBBLMSN);
                this.TXT01_CSBLHSN.SetValue(popup.fsIBBLHSN);
                this.TXT01_CSHMNO1.SetValue(popup.fsIBHMNO1);
                this.TXT01_CSHMNO2.SetValue(popup.fsIBHMNO2);

                UP_USICUSTF_Run();

                this.SetFocus(this.MTB01_CSDATE);
            }
        }
        #endregion

        #region Description : 통관관리 확인버튼
        private void BTN65_OK_Click(object sender, EventArgs e)
        {
            UP_USICUSTF_Run();
        }
        #endregion

        #endregion

        #region Description : 양수도관리

        #region Description : 양수도관리 전체조회
        private void UP_USIYANGNF_Search()
        {
            this.FPS91_TY_S_US_93EDY128.Initialize();
            this.FPS91_TY_S_US_93EDY127.Initialize();

            DataTable dt = UP_Change_DataTable();

            this.FPS91_TY_S_US_93EDY127.SetValue(dt);

            int iMergeCount = 1;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count - 18; i++)
                {   
                    FPS91_TY_S_US_93EDY127_Sheet1.ColumnHeader.Columns[6 + i].Visible = true;
                }
                for (int i = 0; i < this.FPS91_TY_S_US_93EDY127_Sheet1.RowCount; i++)
                {
                    if (i > 0)
                    {
                        if (dt.Rows[i]["GUBUN"].ToString() == dt.Rows[i - 1]["GUBUN"].ToString())
                        {
                            iMergeCount++;
                        }
                        else
                        {
                            iMergeCount = 1;
                        }
                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 0, iMergeCount, 1);
                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 1, iMergeCount, 1);
                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 2, iMergeCount, 1);
                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 3, iMergeCount, 1);
                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 4, iMergeCount, 1);
                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 5, iMergeCount, 1);

                        for (int j = 1; j < Convert.ToInt32(dt.Rows[i]["CHASU"].ToString()); j++)
                        {
                            this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 5+j, iMergeCount, 1);
                        }

                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 16, iMergeCount, 1);
                        this.FPS91_TY_S_US_93EDY127_Sheet1.AddSpanCell(i - (iMergeCount - 1), 17, iMergeCount, 1);
                    }
                }
            }
        }
        #endregion

        #region Description : 양수도 전체조회 DataTable 변환
        private DataTable UP_Change_DataTable()
        {
            DataTable dtRtn = new DataTable();

            DataTable dtTemp = new DataTable();
            DataRow row;

            int iChasu = 0;
            int iRowNum = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_973DL992",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dtTemp = this.DbConnector.ExecuteDataTable();

            iChasu = Convert.ToInt32(dtTemp.Rows[0]["YDSEQ"].ToString());

            if (iChasu > 0)
            {
                dtRtn.Columns.Add("YNHANGCHA", typeof(System.String));
                dtRtn.Columns.Add("YNHANGCHANM", typeof(System.String));
                dtRtn.Columns.Add("YNGOKJONG", typeof(System.String));
                dtRtn.Columns.Add("YNGOKJONGNM", typeof(System.String));
                dtRtn.Columns.Add("YNYNHWAJU", typeof(System.String));
                dtRtn.Columns.Add("YNYNHWAJUNM", typeof(System.String));
                // 최대 차수만큼 양수화주필드 추가
                for (int i = 0; i < iChasu; i++)
                {
                    dtRtn.Columns.Add("YNYSHWAJU" + (i + 1), typeof(System.String));
                }
                dtRtn.Columns.Add("YNQTY", typeof(System.String));
                dtRtn.Columns.Add("YNYSDATE", typeof(System.String));
                dtRtn.Columns.Add("YNHWAJU", typeof(System.String));
                dtRtn.Columns.Add("YNBLNO", typeof(System.String));
                dtRtn.Columns.Add("YNBLMSN", typeof(System.String));
                dtRtn.Columns.Add("YNBLHSN", typeof(System.String));
                dtRtn.Columns.Add("YNCUSTIL", typeof(System.String));
                dtRtn.Columns.Add("YNCUSTCH", typeof(System.String));
                dtRtn.Columns.Add("YNYSSEQ", typeof(System.String));
                dtRtn.Columns.Add("YNYDSEQ", typeof(System.String));
                dtRtn.Columns.Add("GUBUN", typeof(System.String));
                dtRtn.Columns.Add("CHASU", typeof(System.String));

                // 추가필요

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_965F1715",
                    this.CBH01_STHANGCHA.GetValue().ToString(),
                    this.CBH01_EDHANGCHA.GetValue().ToString(),
                    this.CBH01_GGOKJONG.GetValue().ToString(),
                    this.CBH01_GHWAJU.GetValue().ToString(),
                    this.CBH01_STHANGCHA.GetValue().ToString(),
                    this.CBH01_EDHANGCHA.GetValue().ToString(),
                    this.CBH01_GGOKJONG.GetValue().ToString(),
                    this.CBH01_GHWAJU.GetValue().ToString()
                    );

                dtTemp = this.DbConnector.ExecuteDataTable();

                

                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    // 1차수인 경우 
                    if (dtTemp.Rows[i]["YNYDSEQ"].ToString() == "1")
                    {
                        row = dtRtn.NewRow();

                        row["YNHANGCHA"] = dtTemp.Rows[i]["YNHANGCHA"].ToString();
                        row["YNHANGCHANM"] = dtTemp.Rows[i]["YNHANGCHANM"].ToString();
                        row["YNGOKJONG"] = dtTemp.Rows[i]["YNGOKJONG"].ToString();
                        row["YNGOKJONGNM"] = dtTemp.Rows[i]["YNGOKJONGNM"].ToString();
                        row["YNYNHWAJU"] = dtTemp.Rows[i]["YNYNHWAJU"].ToString();
                        row["YNYNHWAJUNM"] = dtTemp.Rows[i]["YNYNHWAJUNM"].ToString();

                        row["YNYSHWAJU" + "1"] = dtTemp.Rows[i]["YNYSHWAJU"].ToString();

                        row["YNQTY"] = dtTemp.Rows[i]["YNQTY"].ToString();
                        row["YNYSDATE"] = dtTemp.Rows[i]["YNYSDATE"].ToString();
                        row["YNHWAJU"] = dtTemp.Rows[i]["YNHWAJU"].ToString();
                        row["YNBLNO"] = dtTemp.Rows[i]["YNBLNO"].ToString();
                        row["YNBLMSN"] = dtTemp.Rows[i]["YNBLMSN"].ToString();
                        row["YNBLHSN"] = dtTemp.Rows[i]["YNBLHSN"].ToString();
                        row["YNCUSTIL"] = dtTemp.Rows[i]["YNCUSTIL"].ToString();
                        row["YNCUSTCH"] = dtTemp.Rows[i]["YNCUSTCH"].ToString();
                        row["YNYSSEQ"] = dtTemp.Rows[i]["YNMYSSEQ"].ToString();
                        row["YNYDSEQ"] = dtTemp.Rows[i]["YNMYDSEQ"].ToString();
                        row["GUBUN"] = dtTemp.Rows[i]["GUBUN"].ToString();
                        row["CHASU"] = dtTemp.Rows[i]["YNYDSEQ"].ToString();

                        dtRtn.Rows.Add(row);

                        iRowNum++;
                    }
                    // 2차수 이상
                    else
                    {
                        if (dtRtn.Rows[iRowNum - 1]["YNYSHWAJU" + dtTemp.Rows[i]["YNYDSEQ"].ToString()].ToString() != "")
                        {
                            row = dtRtn.NewRow();

                            row["YNHANGCHA"] = dtTemp.Rows[i]["YNHANGCHA"].ToString();
                            row["YNHANGCHANM"] = dtTemp.Rows[i]["YNHANGCHANM"].ToString();
                            row["YNGOKJONG"] = dtTemp.Rows[i]["YNGOKJONG"].ToString();
                            row["YNGOKJONGNM"] = dtTemp.Rows[i]["YNGOKJONGNM"].ToString();
                            row["YNYNHWAJU"] = dtTemp.Rows[i]["YNYNHWAJU"].ToString();
                            row["YNYNHWAJUNM"] = dtTemp.Rows[i]["YNYNHWAJUNM"].ToString();

                            // 이전 차수 데이터
                            for (int j = Convert.ToInt32(dtTemp.Rows[i]["YNYDSEQ"].ToString()); j > 1; j--)
                            {
                                row["YNYSHWAJU" + (j - 1).ToString()] = dtRtn.Rows[iRowNum - 1]["YNYSHWAJU" + (j - 1).ToString()].ToString();
                            }

                            row["YNYSHWAJU" + dtTemp.Rows[i]["YNYDSEQ"].ToString()] = dtTemp.Rows[i]["YNYSHWAJU"].ToString();

                            row["YNQTY"] = dtTemp.Rows[i]["YNQTY"].ToString();
                            row["YNYSDATE"] = dtTemp.Rows[i]["YNYSDATE"].ToString();
                            row["YNHWAJU"] = dtTemp.Rows[i]["YNHWAJU"].ToString();
                            row["YNBLNO"] = dtTemp.Rows[i]["YNBLNO"].ToString();
                            row["YNBLMSN"] = dtTemp.Rows[i]["YNBLMSN"].ToString();
                            row["YNBLHSN"] = dtTemp.Rows[i]["YNBLHSN"].ToString();
                            row["YNCUSTIL"] = dtTemp.Rows[i]["YNCUSTIL"].ToString();
                            row["YNCUSTCH"] = dtTemp.Rows[i]["YNCUSTCH"].ToString();
                            row["YNYSSEQ"] = dtTemp.Rows[i]["YNMYSSEQ"].ToString();
                            row["YNYDSEQ"] = dtTemp.Rows[i]["YNMYDSEQ"].ToString();
                            row["GUBUN"] = dtTemp.Rows[i]["GUBUN"].ToString();
                            row["CHASU"] = dtTemp.Rows[i]["YNYDSEQ"].ToString();

                            dtRtn.Rows.Add(row);

                            iRowNum++;
                        }
                        else
                        {
                            dtRtn.Rows[iRowNum - 1]["YNYSHWAJU" + dtTemp.Rows[i]["YNYDSEQ"].ToString()] = dtTemp.Rows[i]["YNYSHWAJU"].ToString();
                        }
                    }
                }
            }

            return dtRtn;
        }
        #endregion

        #region Description : 양수도관리 조회
        private void UP_USIYANGNF_TAB_Search()
        {
            double dJEGOQTY = 0;
            double dYNQTY = 0;
            double dYNYSYDQTY = 0;
            double dYNYSCHQTY = 0;

            string sYNHWAJU = string.Empty;
            string sYNYSYDHWAJ = string.Empty;
            string sYNYSYDDATE = string.Empty;
            string sYNYSYDSEQ = string.Empty;

            DataTable dtRtn = new DataTable();
            DataTable dt = new DataTable();
            DataTable dtAft = new DataTable();

            DataRow row;

            dtRtn.Columns.Add("CHASU", typeof(System.String));
            dtRtn.Columns.Add("YNYSSEQ", typeof(System.String));
            dtRtn.Columns.Add("YNYNHWAJU", typeof(System.String));
            dtRtn.Columns.Add("YNDESC1", typeof(System.String));
            dtRtn.Columns.Add("YNHANGCHA", typeof(System.String));
            dtRtn.Columns.Add("VSDESC1", typeof(System.String));
            dtRtn.Columns.Add("YNGOKJONG", typeof(System.String));
            dtRtn.Columns.Add("GKDESC1", typeof(System.String));
            dtRtn.Columns.Add("YNHWAJU", typeof(System.String));
            dtRtn.Columns.Add("HJDESC1", typeof(System.String));
            dtRtn.Columns.Add("YDHWAJU", typeof(System.String));
            dtRtn.Columns.Add("YNBLNO", typeof(System.String));
            dtRtn.Columns.Add("YNBLMSN", typeof(System.String));
            dtRtn.Columns.Add("YNBLHSN", typeof(System.String));
            dtRtn.Columns.Add("YNCUSTIL", typeof(System.String));
            dtRtn.Columns.Add("YNCUSTCH", typeof(System.String));
            dtRtn.Columns.Add("YNYSHWAJU", typeof(System.String));
            dtRtn.Columns.Add("YNHJDESC1", typeof(System.String));
            dtRtn.Columns.Add("YSHWAJU", typeof(System.String));
            dtRtn.Columns.Add("YNYSDATE", typeof(System.String));
            dtRtn.Columns.Add("YNYDSEQ", typeof(System.String));
            dtRtn.Columns.Add("YNBKILJA", typeof(System.String));
            dtRtn.Columns.Add("YNBKJNHJ", typeof(System.String));
            dtRtn.Columns.Add("YNJNCDESC1", typeof(System.String));
            dtRtn.Columns.Add("YNBKHUHJ", typeof(System.String));
            dtRtn.Columns.Add("YNHUDESC1", typeof(System.String));
            dtRtn.Columns.Add("YNQTY", typeof(System.String));
            dtRtn.Columns.Add("YNYSCHQTY", typeof(System.String));
            dtRtn.Columns.Add("YNYSYDQTY", typeof(System.String));
            dtRtn.Columns.Add("JEGO", typeof(System.String));
            dtRtn.Columns.Add("YNYSYDHWAJ", typeof(System.String));
            dtRtn.Columns.Add("YNYSYDDATE", typeof(System.String));
            dtRtn.Columns.Add("YNYSYDSEQ", typeof(System.String));

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96LH0922", 
                CBH01_YNHANGCHA.GetValue().ToString(),  //  1 = 항차
                CBH01_YNGOKJONG.GetValue().ToString(),  //  2 = 곡종
                TXT01_YNBLNO.GetValue().ToString(),     //  3 = Ｂ／Ｌ번호
                TXT01_YNBLMSN.GetValue().ToString(),    //  4 = ＭＳＮ
                TXT01_YNBLHSN.GetValue().ToString(),    //  5 = ＨＳＮ
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),             //  6 = 통관일자
                TXT01_YNCUSTCH.GetValue().ToString(),   //  7 = 통관차수
                CBH01_YNYNHWAJU.GetValue().ToString(),  //  8 = 원화주
                TXT01_YNYSSEQ.GetValue().ToString(),    //  9 = 양수순번
                TXT01_YNYDSEQ.GetValue().ToString(),    // 10 = 양도차수
                TXT01_YNYSSEQ.GetValue().ToString()     // 11 = 양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    sYNHWAJU = dt.Rows[i]["YNYSHWAJU"].ToString();
                    sYNYSYDHWAJ = dt.Rows[i]["YNHWAJU"].ToString();
                    sYNYSYDDATE = dt.Rows[i]["YNYSDATE"].ToString();
                    sYNYSYDSEQ = dt.Rows[i]["YNYSSEQ"].ToString();

                    row = dtRtn.NewRow();

                    row["CHASU"] = dt.Rows[i][0].ToString();
                    row["YNYSSEQ"] = dt.Rows[i][1].ToString();
                    row["YNYNHWAJU"] = dt.Rows[i][2].ToString();
                    row["YNDESC1"] = dt.Rows[i][3].ToString();
                    row["YNHANGCHA"] = dt.Rows[i][4].ToString();
                    row["VSDESC1"] = dt.Rows[i][5].ToString();
                    row["YNGOKJONG"] = dt.Rows[i][6].ToString();
                    row["GKDESC1"] = dt.Rows[i][7].ToString();
                    row["YNHWAJU"] = dt.Rows[i][8].ToString();
                    row["HJDESC1"] = dt.Rows[i][9].ToString();
                    row["YDHWAJU"] = dt.Rows[i][10].ToString();
                    row["YNBLNO"] = dt.Rows[i][11].ToString();
                    row["YNBLMSN"] = dt.Rows[i][12].ToString();
                    row["YNBLHSN"] = dt.Rows[i][13].ToString();
                    row["YNCUSTIL"] = dt.Rows[i][14].ToString();
                    row["YNCUSTCH"] = dt.Rows[i][15].ToString();
                    row["YNYSHWAJU"] = dt.Rows[i][16].ToString();
                    row["YNHJDESC1"] = dt.Rows[i][17].ToString();
                    row["YSHWAJU"] = dt.Rows[i][18].ToString();
                    row["YNYSDATE"] = dt.Rows[i][19].ToString();
                    row["YNYDSEQ"] = dt.Rows[i][20].ToString();
                    row["YNBKILJA"] = dt.Rows[i][21].ToString();
                    row["YNBKJNHJ"] = dt.Rows[i][22].ToString();
                    row["YNJNCDESC1"] = dt.Rows[i][23].ToString();
                    row["YNBKHUHJ"] = dt.Rows[i][24].ToString();
                    row["YNHUDESC1"] = dt.Rows[i][25].ToString();

                    // 양수량
                    row["YNQTY"] = dt.Rows[i][26].ToString();
                    dYNQTY = Convert.ToDouble(dt.Rows[i][26].ToString().Trim());

                    // 양수양도량
                    row["YNYSYDQTY"] = dt.Rows[i][32].ToString();
                    dYNYSYDQTY = Convert.ToDouble(dt.Rows[i][32].ToString().Trim());

                    // 출고량
                    row["YNYSCHQTY"] = dt.Rows[i][27].ToString();
                    dYNYSCHQTY = Convert.ToDouble(dt.Rows[i][27].ToString().Trim());

                    // 재고량
                    dJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}",dYNQTY - dYNYSYDQTY - dYNYSCHQTY));

                    row["JEGO"] = dJEGOQTY;

                    row["YNYSYDHWAJ"] = dt.Rows[i][29].ToString();
                    row["YNYSYDDATE"] = dt.Rows[i][30].ToString();
                    row["YNYSYDSEQ"] = dt.Rows[i][31].ToString();

                    dtRtn.Rows.Add(row);
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_96LH3923",
                    CBH01_YNHANGCHA.GetValue().ToString(),  //  1 = 항차
                    CBH01_YNGOKJONG.GetValue().ToString(),  //  2 = 곡종
                    TXT01_YNBLNO.GetValue().ToString(),     //  3 = Ｂ／Ｌ번호
                    TXT01_YNBLMSN.GetValue().ToString(),  	//  4 = ＭＳＮ
                    TXT01_YNBLHSN.GetValue().ToString(),    //  5 = ＨＳＮ
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),  	        //  6 = 통관일자
                    TXT01_YNCUSTCH.GetValue().ToString(),   //  7 = 통관차수
                    TXT01_YNYSSEQ.GetValue().ToString()     //  8 = 양수순번(식별자)
                    );

                dtAft = this.DbConnector.ExecuteDataTable();

                if (dtAft.Rows.Count > 0)
                {
                    for (int j = 0; j < dtAft.Rows.Count; j++)
                    {
                        sYNHWAJU = dtAft.Rows[j][16].ToString();
                        sYNYSYDHWAJ = dtAft.Rows[j][8].ToString();
                        sYNYSYDDATE = dtAft.Rows[j][19].ToString();
                        sYNYSYDSEQ = dtAft.Rows[j][1].ToString();

                        row = dtRtn.NewRow();

                        row["CHASU"] = dtAft.Rows[j][0].ToString();
                        row["YNYSSEQ"] = dtAft.Rows[j][1].ToString();
                        row["YNYNHWAJU"] = dtAft.Rows[j][2].ToString();
                        row["YNDESC1"] = dtAft.Rows[j][3].ToString();
                        row["YNHANGCHA"] = dtAft.Rows[j][4].ToString();
                        row["VSDESC1"] = dtAft.Rows[j][5].ToString();
                        row["YNGOKJONG"] = dtAft.Rows[j][6].ToString();
                        row["GKDESC1"] = dtAft.Rows[j][7].ToString();
                        row["YNHWAJU"] = dtAft.Rows[j][8].ToString();
                        row["HJDESC1"] = dtAft.Rows[j][9].ToString();
                        row["YDHWAJU"] = dtAft.Rows[j][10].ToString();
                        row["YNBLNO"] = dtAft.Rows[j][11].ToString();
                        row["YNBLMSN"] = dtAft.Rows[j][12].ToString();
                        row["YNBLHSN"] = dtAft.Rows[j][13].ToString();
                        row["YNCUSTIL"] = dtAft.Rows[j][14].ToString();
                        row["YNCUSTCH"] = dtAft.Rows[j][15].ToString();
                        row["YNYSHWAJU"] = dtAft.Rows[j][16].ToString();
                        row["YNHJDESC1"] = dtAft.Rows[j][17].ToString();
                        row["YSHWAJU"] = dtAft.Rows[j][18].ToString();
                        row["YNYSDATE"] = dtAft.Rows[j][19].ToString();
                        row["YNYDSEQ"] = dtAft.Rows[j][20].ToString();
                        row["YNBKILJA"] = dtAft.Rows[j][21].ToString();
                        row["YNBKJNHJ"] = dtAft.Rows[j][22].ToString();
                        row["YNJNCDESC1"] = dtAft.Rows[j][23].ToString();
                        row["YNBKHUHJ"] = dtAft.Rows[j][24].ToString();
                        row["YNHUDESC1"] = dtAft.Rows[j][25].ToString();
                        // 양수량
                        row["YNQTY"] = dtAft.Rows[j][26].ToString();
                        dYNQTY = Convert.ToDouble(dtAft.Rows[j][26].ToString().Trim());

                        // 양수양도량
                        row["YNYSYDQTY"] = dtAft.Rows[j][32].ToString();
                        dYNYSYDQTY = Convert.ToDouble(dtAft.Rows[j][32].ToString().Trim());

                        // 출고량
                        row["YNYSCHQTY"] = dtAft.Rows[j][27].ToString();
                        dYNYSCHQTY = Convert.ToDouble(dtAft.Rows[j][27].ToString().Trim());

                        // 재고량
                        dJEGOQTY = dYNQTY - dYNYSYDQTY - dYNYSCHQTY;
                        row["JEGO"] = String.Format("{0,9:N3}", dJEGOQTY);

                        row["YNYSYDHWAJ"] = dtAft.Rows[j][29].ToString();
                        row["YNYSYDDATE"] = dtAft.Rows[j][30].ToString();
                        row["YNYSYDSEQ"] = dtAft.Rows[j][31].ToString();

                        dtRtn.Rows.Add(row);
                    }
                }
            }

            this.FPS91_TY_S_US_93EDY128.SetValue(dtRtn);
        }
        #endregion

        #region Description : 양수도관리 확인
        private void UP_USIYANGNF_Run()
        {
            UP_YANGvarInit();
            #region Description : 재고사항 조회

            string sYNHWAJU = string.Empty;
            string sYNYSHWAJU = string.Empty;
            string sYNYSDATE = string.Empty;
            string sYNYSSEQ = string.Empty;
            string sYNYDSEQ = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96AET753",
                this.CBH01_YNHANGCHA.GetValue().ToString(),
                this.CBH01_YNGOKJONG.GetValue().ToString(),
                this.CBH01_YNHWAJU.GetValue().ToString(),
                this.TXT01_YNBLNO.GetValue().ToString(),
                this.TXT01_YNBLMSN.GetValue().ToString(),
                this.TXT01_YNBLHSN.GetValue().ToString(),
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                this.TXT01_YNCUSTCH.GetValue().ToString(),
                this.CBH01_YNYSHWAJU.GetValue().ToString(),
                Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),
                this.TXT01_YNYSSEQ.GetValue().ToString(),
                this.TXT01_YNYDSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sYNHWAJU = dt.Rows[0]["YNHWAJU"].ToString();        // 양도화주
                sYNYSHWAJU = dt.Rows[0]["YNYSHWAJU"].ToString();    // 양수화주
                sYNYSDATE = dt.Rows[0]["YNYSDATE"].ToString();      // 양수일자
                sYNYSSEQ = dt.Rows[0]["YNYSSEQ"].ToString();        // 양수순번
                sYNYDSEQ = dt.Rows[0]["YNYDSEQ"].ToString();        // 양수차수
            }
            else
            {
                // 필드추가 필요
                sYNHWAJU = fsJCYDHWAJU;   // 양도화주
                sYNYSHWAJU = fsJCHWAJU;   // 양수화주
                sYNYSDATE = fsJCYSDATE;   // 양수일자
                sYNYSSEQ = fsJCYSSEQ;     // 양수순번
                sYNYDSEQ = fsJCYDSEQ;     // 양수차수
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96AFT757",
                this.CBH01_YNHANGCHA.GetValue().ToString(),     // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(),     // 곡종
                sYNYSHWAJU,                                     // 양수화주
                this.TXT01_YNBLNO.GetValue().ToString(),        // B/L 번호
                this.TXT01_YNBLMSN.GetValue().ToString(),       // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),       // HSN
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                // 통관일자
                this.TXT01_YNCUSTCH.GetValue().ToString(),      // 통관차수
                sYNHWAJU,                                       // 양도화주
                sYNYSDATE,                                      // 양수일자
                sYNYSSEQ,                                       // 양수순번
                sYNYDSEQ                                        // 양도차수
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                TXT01_YNHMNO1.SetValue(dt.Rows[0]["JCHMNO1"].ToString());
                TXT01_YNHMNO2.SetValue(dt.Rows[0]["JCHMNO2"].ToString());
                TXT01_YDQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString().Trim())));
                TXT01_YSYDQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString().Trim())));
                TXT01_YSQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString().Trim())));
                TXT01_YSCHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString().Trim())));
                TXT01_JEGOQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString().Trim())));
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_96AFT757",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),     // 항차
                    this.CBH01_YNGOKJONG.GetValue().ToString(),     // 곡종
                    sYNYSHWAJU,                                     // 양수화주
                    this.TXT01_YNBLNO.GetValue().ToString(),        // B/L번호
                    this.TXT01_YNBLMSN.GetValue().ToString(),       // MSN
                    this.TXT01_YNBLHSN.GetValue().ToString(),       // HSN
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                // 통관일자
                    this.TXT01_YNCUSTCH.GetValue().ToString(),      // 통관차수
                    "",                                             // 양수화주
                    "0",                                            // 양수일자
                    "0",                                            // 양수순번
                    "0"                                             // 양도차수
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    TXT01_YNHMNO1.SetValue(dt.Rows[0]["JCHMNO1"].ToString());
                    TXT01_YNHMNO2.SetValue(dt.Rows[0]["JCHMNO2"].ToString());
                    TXT01_YDQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString().Trim())));
                    TXT01_YSYDQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString().Trim())));
                    TXT01_YSQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString().Trim())));
                    TXT01_YSCHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString().Trim())));
                    TXT01_JEGOQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString().Trim())));
                }
            }
            #endregion 

            fdOWN_YNYSYDQTY = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96AHF763",
                this.CBH01_YNHANGCHA.GetValue().ToString(),     // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(),     // 곡종
                this.CBH01_YNHWAJU.GetValue().ToString(),       // 양도화주
                this.TXT01_YNBLNO.GetValue().ToString(),        // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),       // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),       // HSN
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                // 통관일자
                this.TXT01_YNCUSTCH.GetValue().ToString(),      // 통관차수
                this.CBH01_YNYSHWAJU.GetValue().ToString(),     // 양수화주
                Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),                // 양수일자
                this.TXT01_YNYSSEQ.GetValue().ToString(),       // 양수순번
                this.TXT01_YNYDSEQ.GetValue().ToString()        // 양도차수
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DTP01_YNBKILJA.SetValue(dt.Rows[0]["YNBKILJA"].ToString());         // 보관료 시점일자
                CBH01_YNBKJNHJ.SetValue(dt.Rows[0]["YNBKJNHJ"].ToString());         // 보관료 이전화주
                CBH01_YNBKHUHJ.SetValue(dt.Rows[0]["YNBKHUHJ"].ToString());         // 보관료 이후화주
                fdOWN_YNYSYDQTY = Convert.ToDouble(dt.Rows[0]["YNQTY"].ToString()); // 양수량
                TXT01_YNYSYDQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["YNQTY"].ToString().Trim())));           // 양수량
                TXT01_YNYSCHQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(dt.Rows[0]["YNYSCHQTY"].ToString().Trim())));       // 출고량
                TXT01_YNSIKBAEL.SetValue(dt.Rows[0]["YNSIKBAEL"].ToString());       // 식별자

                CBH01_YNYSYDHWAJ.SetValue(dt.Rows[0]["YNYSYDHWAJ"].ToString());     // 이전양도화주
                DTP01_YNYSYDDATE.SetValue(dt.Rows[0]["YNYSYDDATE"].ToString());     // 이전양수일자
                TXT01_YNYSYDSEQ.SetValue(dt.Rows[0]["YNYSYDSEQ"].ToString());       // 이전양수순번
                CBH01_YNYNHWAJU.SetValue(dt.Rows[0]["YNYNHWAJU"].ToString());       // 원화주
                
                // 이전화주용 변수
                fsJCYNHWAJU = dt.Rows[0]["YNYNHWAJU"].ToString();                   // 원화주
                fsJCYDHWAJU = dt.Rows[0]["YNYSYDHWAJ"].ToString();                  // 이전양도화주
                fsJCYSDATE = dt.Rows[0]["YNYSYDDATE"].ToString();                   // 이전양수일자
                fsJCYSSEQ = dt.Rows[0]["YNYSYDSEQ"].ToString();                     // 이전양수순번
                fsJCHWAJU = CBH01_YNHWAJU.GetValue().ToString();                    // 이전양수화주

                // 이전양도
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_96B8Z764",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),     // 항차
                    this.CBH01_YNGOKJONG.GetValue().ToString(),     // 곡종
                    fsJCHWAJU,                                      // 이전양수화주
                    this.TXT01_YNBLNO.GetValue().ToString(),        // B/L번호
                    this.TXT01_YNBLMSN.GetValue().ToString(),       // MSN
                    this.TXT01_YNBLHSN.GetValue().ToString(),       // HSN
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                // 통관일자
                    this.TXT01_YNCUSTCH.GetValue().ToString(),      // 통관차수
                    fsJCYDHWAJU,                                    // 이전양도화주
                    fsJCYSDATE,                                     // 이전양수일자
                    fsJCYSSEQ,                                      // 이전양수순번
                    this.CBH01_YNYNHWAJU.GetValue().ToString()      // 원화주
                    );

                dt = this.DbConnector.ExecuteDataTable();

                fsJCYDSEQ = "0";

                if (dt.Rows.Count > 0)
                {   
                    fsJCYDSEQ = dt.Rows[0]["JCYDSEQ"].ToString();   // 이전양도순번
                }

                fsWK_GUBUN5 = "UPT";
                UP_FieldVisible("UPT");

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    SetFocus(this.DTP01_YNBKILJA);
                };

                tmr.Interval = 100;
                tmr.Start();
            }
            else
            {
                CBH01_YNYSYDHWAJ.SetValue(fsJCYDHWAJU);     // 이전양도화주

                DTP01_YNYSYDDATE.SetValue(fsJCYSDATE);      // 이전양수일자
                        
                TXT01_YNYSYDSEQ.SetValue(fsJCYSSEQ);        // 이전양수순번
                    
                CBH01_YNYNHWAJU.SetValue(fsJCYNHWAJU);      // 원화주

                /*****************************************************************
				*항차,곡종,양도화주,B/L번호,MSN,HSN,통관일자,통관차수,양수화주, *
				*양수일자가 같은 데이터는 1번이상 등록안됨,                     *
				*같은 양수일자에 추가적으로 같은 양수화주에게 줄 경우는         *
				*데이터를 수정해야 함.                                          *
				*****************************************************************/

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                (
                "TY_P_US_96AET753",
                this.CBH01_YNHANGCHA.GetValue().ToString(),     // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(),     // 곡종
                this.CBH01_YNHWAJU.GetValue().ToString(),       // 양도화주
                this.TXT01_YNBLNO.GetValue().ToString(),        // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),       // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),       // HSN
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                // 통관일자
                this.TXT01_YNCUSTCH.GetValue().ToString(),      // 통관차수
                this.CBH01_YNYSHWAJU.GetValue().ToString(),     // 양수화주
                Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),                // 양수일자
                "0",                                            // 양수순번
                "0"                                             // 양도차수
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("양수순번 " + dt.Rows[0]["YNYSSEQ"].ToString() + "양도순번 " + dt.Rows[0]["YNYDSEQ"].ToString() + "에 데이터가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.CBH01_YNHANGCHA.CodeText);
                    //txtCHECK.Text = "";
                    return;
                }

                // 양수순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                (
                "TY_P_US_96BDB770",
                this.CBH01_YNHANGCHA.GetValue().ToString(),
                this.CBH01_YNGOKJONG.GetValue().ToString(),
                this.CBH01_YNHWAJU.GetValue().ToString(),
                this.TXT01_YNBLNO.GetValue().ToString(),
                this.TXT01_YNBLMSN.GetValue().ToString(),
                this.TXT01_YNBLHSN.GetValue().ToString(),
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                this.TXT01_YNCUSTCH.GetValue().ToString(),
                this.TXT01_YNYDSEQ.GetValue().ToString()
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    TXT01_YNYSSEQ.SetValue(dt.Rows[0]["SEQ"].ToString());
                }
                fsWK_GUBUN5 = "NEW";
            }
            
            Set_Cookie();
        }
        #endregion

        #region Description : 양수도관리 조회 그리드 더블클릭
        private void FPS91_TY_S_US_93EDY128_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_YNHANGCHA.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNHANGCHA").ToString());
            this.CBH01_YNGOKJONG.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNGOKJONG").ToString());
            this.CBH01_YNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNHWAJU").ToString());
            this.TXT01_YNBLNO.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNBLNO").ToString());
            this.TXT01_YNBLMSN.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNBLMSN").ToString());
            this.TXT01_YNBLHSN.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNBLHSN").ToString());
            this.DTP01_YNCUSTIL.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNCUSTIL").ToString());
            this.TXT01_YNCUSTCH.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNCUSTCH").ToString());
            this.CBH01_YNYSHWAJU.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYSHWAJU").ToString());
            this.MTB01_YNYSDATE.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYSDATE").ToString());
            this.TXT01_YNYSSEQ.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYSSEQ").ToString());
            this.TXT01_YNYDSEQ.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYDSEQ").ToString());
            this.CBH01_YNYNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYNHWAJU").ToString());

            UP_USIYANGNF_Run();
        }
        private void FPS91_TY_S_US_93EDY128_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_YNHANGCHA.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNHANGCHA").ToString());
                this.CBH01_YNGOKJONG.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNGOKJONG").ToString());
                this.CBH01_YNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNHWAJU").ToString());
                this.TXT01_YNBLNO.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNBLNO").ToString());
                this.TXT01_YNBLMSN.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNBLMSN").ToString());
                this.TXT01_YNBLHSN.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNBLHSN").ToString());
                this.DTP01_YNCUSTIL.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNCUSTIL").ToString());
                this.TXT01_YNCUSTCH.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNCUSTCH").ToString());
                this.CBH01_YNYSHWAJU.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYSHWAJU").ToString());
                this.MTB01_YNYSDATE.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYSDATE").ToString());
                this.TXT01_YNYSSEQ.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYSSEQ").ToString());
                this.TXT01_YNYDSEQ.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYDSEQ").ToString());
                this.CBH01_YNYNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY128.GetValue("YNYNHWAJU").ToString());

                UP_USIYANGNF_Run();
            }
        }
        #endregion

        #region Description : 양수도관리 전체조회 그리드 더블클릭
        private void FPS91_TY_S_US_93EDY127_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_YNHANGCHA.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNHANGCHA").ToString());
            this.CBH01_YNGOKJONG.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNGOKJONG").ToString());
            this.CBH01_YNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNHWAJU").ToString());
            this.TXT01_YNBLNO.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNBLNO").ToString());
            this.TXT01_YNBLMSN.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNBLMSN").ToString());
            this.TXT01_YNBLHSN.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNBLHSN").ToString());
            this.DTP01_YNCUSTIL.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNCUSTIL").ToString());
            this.TXT01_YNCUSTCH.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNCUSTCH").ToString());
            this.CBH01_YNYSHWAJU.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYSHWAJU1").ToString());
            this.MTB01_YNYSDATE.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYSDATE").ToString());
            this.TXT01_YNYSSEQ.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYSSEQ").ToString());
            this.TXT01_YNYDSEQ.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYDSEQ").ToString());
            this.CBH01_YNYNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYNHWAJU").ToString());

            UP_USIYANGNF_TAB_Search();

            UP_USIYANGNF_Run();

            // FOCUS
            Timer tmr = new Timer();

            tmr.Tick += delegate
            {
                tmr.Stop();
                SetFocus(FPS91_TY_S_US_93EDY128);
            };

            tmr.Interval = 300;
            tmr.Start();
            
        }
        private void FPS91_TY_S_US_93EDY127_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_YNHANGCHA.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNHANGCHA").ToString());
                this.CBH01_YNGOKJONG.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNGOKJONG").ToString());
                this.CBH01_YNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNHWAJU").ToString());
                this.TXT01_YNBLNO.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNBLNO").ToString());
                this.TXT01_YNBLMSN.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNBLMSN").ToString());
                this.TXT01_YNBLHSN.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNBLHSN").ToString());
                this.DTP01_YNCUSTIL.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNCUSTIL").ToString());
                this.TXT01_YNCUSTCH.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNCUSTCH").ToString());
                this.CBH01_YNYSHWAJU.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYSHWAJU1").ToString());
                this.MTB01_YNYSDATE.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYSDATE").ToString());
                this.TXT01_YNYSSEQ.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYSSEQ").ToString());
                this.TXT01_YNYDSEQ.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYDSEQ").ToString());
                this.CBH01_YNYNHWAJU.SetValue(this.FPS91_TY_S_US_93EDY127.GetValue("YNYNHWAJU").ToString());

                UP_USIYANGNF_TAB_Search();

                UP_USIYANGNF_Run();

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    SetFocus(FPS91_TY_S_US_93EDY128);
                };

                tmr.Interval = 300;
                tmr.Start();
            }
        }
        #endregion

        #region Description : 양수도관리 신규버튼
        private void BTN66_NEW_Click(object sender, EventArgs e)
        {
            fsWK_GUBUN5 = "NEW";
            UP_FieldClear("양수도");
            UP_FieldVisible("NEW");
            Get_Cookie();

            CBH01_YNHWAJU.SetValue("");
            TXT01_YNBLNO.SetValue("");
            TXT01_YNBLMSN.SetValue("");
            TXT01_YNBLHSN.SetValue("");
            DTP01_YNCUSTIL.SetValue("");
            TXT01_YNCUSTCH.SetValue("");
            CBH01_YNYSHWAJU.SetValue("");
            MTB01_YNYSDATE.SetValue("");
            TXT01_YNYSSEQ.SetValue("");
            TXT01_YNYDSEQ.SetValue("");

            UP_Set_KeyFocus();

            FPS91_TY_S_US_93EDY128.Initialize();
        }
        #endregion

        #region Description : 양수도관리 저장버튼
        private void BTN66_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                // 수정인 경우 자료 정리 후 다시 업데이트
                if (fsWK_GUBUN5 == "UPT")
                {
                    UP_YANGvarInit();

                    UP_YANG_SUIJEBLF_SetData("DEL");
                    UP_YANG_USIJEGOF_SetData("DEL");
                    UP_YANG_USIJECSNF_SetData("DEL");

                    this.DbConnector.CommandClear();

                    UP_YANG_USIJEBLF_UPT();
                    UP_YANG_USIJEGOF_UPT();
                    UP_YANG_USIJECSNF_UPT();

                    this.DbConnector.ExecuteTranQueryList();
                }

                UP_YANGvarInit();

                UP_YANG_SUIJEBLF_SetData("SAVE");
                UP_YANG_USIJEGOF_SetData("SAVE");
                UP_YANG_USIJECSNF_SetData("SAVE");

                this.DbConnector.CommandClear();

                UP_YANG_USIJEBLF_UPT();
                UP_YANG_USIJEGOF_UPT();
                UP_YANG_USIJECSNF_UPT();

                if (fsWK_GUBUN5 == "NEW")
                {
                    this.DbConnector.Attach("TY_P_US_96EHJ849",
                                            CBH01_YNHANGCHA.GetValue().ToString(),                  // 항차
                                            CBH01_YNGOKJONG.GetValue().ToString(),                  // 곡종
                                            CBH01_YNHWAJU.GetValue().ToString(),                    // 양도화주
                                            TXT01_YNBLNO.GetValue().ToString(),                     // B/L 번호
                                            TXT01_YNBLMSN.GetValue().ToString(),                    // MSN
                                            TXT01_YNBLHSN.GetValue().ToString(),                    // HSN
                                            Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                             // 통관일자
                                            TXT01_YNCUSTCH.GetValue().ToString(),                   // 통관차수
                                            CBH01_YNYSHWAJU.GetValue().ToString(),                  // 양수화주
                                            Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),     // 양수일자
                                            TXT01_YNYSSEQ.GetValue().ToString(),                    // 양수순번
                                            TXT01_YNYDSEQ.GetValue().ToString(),                    // 양도차수
                                            TXT01_YNHMNO1.GetValue().ToString(),                    // 화물번호1
                                            TXT01_YNHMNO2.GetValue().ToString(),                    // 화물번호2
                                            CBH01_YNYNHWAJU.GetValue().ToString(),                  // 원화주
                                            fdYNQTY,                                                // 확정분 양도량
                                            fdYNYSYDQTY,                                            // 양수분 양수량
                                            fsJCYDHWAJU,                                            // 양수분 양도화주
                                            fsJCYSDATE,                                             // 양수분 양도일자
                                            fsJCYSSEQ,                                              // 양수분 양도차수
                                            Get_Date(DTP01_YNBKILJA.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),     // 보관료 시점일자
                                            CBH01_YNBKJNHJ.GetValue().ToString(),                   // 보관료 이전화주
                                            CBH01_YNBKHUHJ.GetValue().ToString(),                   // 보관료 이후화주
                                            "0",                                                    // 출고량
                                            fsSIKBAEL,                                              // 식별자
                                            TYUserInfo.EmpNo.ToString().Trim()
                                            );
                }
                else if (fsWK_GUBUN5 == "UPT")
                {
                    this.DbConnector.Attach("TY_P_US_96EHL850",
                                            TXT01_YNHMNO1.GetValue().ToString(),                    // 화물번호1
                                            TXT01_YNHMNO2.GetValue().ToString(),                    // 화물번호2
                                            CBH01_YNYNHWAJU.GetValue().ToString(),                  // 원화주
                                            fdYNQTY,                                                // 확정분 양도량
                                            fdYNYSYDQTY,                                            // 양수분 양수량
                                            fsJCYDHWAJU,                                            // 양수분 양도화주
                                            fsJCYSDATE,                                             // 양수분 양도일자
                                            fsJCYSSEQ,                                              // 양수분 양도차수
                                            Get_Date(DTP01_YNBKILJA.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),     // 보관료 시점일자
                                            CBH01_YNBKJNHJ.GetValue().ToString(),                   // 보관료 이전화주
                                            CBH01_YNBKHUHJ.GetValue().ToString(),                   // 보관료 이후화주
                                            TXT01_YNSIKBAEL.GetValue().ToString(),                  // 식별자
                                            TYUserInfo.EmpNo.ToString().Trim(),
                                            CBH01_YNHANGCHA.GetValue().ToString(),                  // 항차
                                            CBH01_YNGOKJONG.GetValue().ToString(),                  // 곡종
                                            CBH01_YNHWAJU.GetValue().ToString(),                    // 양도화주
                                            TXT01_YNBLNO.GetValue().ToString(),                     // B/L 번호
                                            TXT01_YNBLMSN.GetValue().ToString(),                    // MSN
                                            TXT01_YNBLHSN.GetValue().ToString(),                    // HSN
                                            Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                             // 통관일자
                                            TXT01_YNCUSTCH.GetValue().ToString(),                   // 통관차수
                                            CBH01_YNYSHWAJU.GetValue().ToString(),                  // 양수화주
                                            Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),     // 양수일자
                                            TXT01_YNYSSEQ.GetValue().ToString(),                    // 양수순번
                                            TXT01_YNYDSEQ.GetValue().ToString()                     // 양도차수
                                            );
                }

                this.DbConnector.ExecuteTranQueryList();

                // 저장되었습니다.

                fsWK_GUBUN5 = "UPT";
                UP_USIYANGNF_Search();
                UP_USIYANGNF_TAB_Search();
                this.ShowMessage("TY_M_GB_23NAD873");
                UP_USIYANGNF_Run();
                Set_Cookie();
            }
            catch
            {
            }
        }
        #endregion

        #region Description : USIJEBLF 데이터 조회
        private void UP_YANG_SUIJEBLF_SetData(string sGUBUN)
        {
            double dJBBEJNQTY = 0; //  0 = 배정량 (지역)     
            double dJBHWAKQTY = 0; //  1 = 확정량 (지역)     
            double dJBYDQTY = 0; //  2 = 양도량 (지역)       
            double dJBYSQTY = 0; //  3 = 양수량 (지역)       
            double dJBYSYDQTY = 0; //  4 = 양수분양도량 (지역)
            double dJBCSQTY = 0; //  5 = 통관수량 (지역)      
            double dJBCHQTY = 0; //  6 = 출고수량 (지역)      
            double dJBYSCHQTY = 0; //  7 = 양수출고량 (지역)  
            double dJBJANQTY = 0; //  8 = 잔량 (지역)        
            double dJBCSJANQTY = 0; //  9 = 통관잔량 (지역)   
            double dJBYSJANQTY = 0; // 10 = 양수출고잔량(지역)
            double dJBJEGOQTY = 0; // 11 = 재고량 (지역)     
            string sJBHMNO1 = ""; // 12 = 화물번호１(지역)    
            string sJBHMNO2 = ""; // 13 = 화물번호 2 (지역)   
            string sJBSOSOK = ""; // 14 = 협회 (지역)        
            string sJBJESTDAT = "0"; // 15 = 보관시작일 (지역)
            string sJBCONTNO = "0"; // 16 = 계약번호 (지역)   

            DataTable dt = new DataTable();

            // 양도화주 (등록, 삭제 공통)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96CFW789", 
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                this.CBH01_YNHWAJU.GetValue().ToString(),   // 양도화주
                this.TXT01_YNBLNO.GetValue().ToString(),    // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                this.TXT01_YNHMNO1.GetValue().ToString(),   // 화물번호1
                this.TXT01_YNHMNO2.GetValue().ToString()    // 화물번호2
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsYNSAVEGB1 = "수정";

                dJBBEJNQTY = Convert.ToDouble(dt.Rows[0]["JBBEJNQTY"].ToString().Trim());  //  0 =배정량
                dJBHWAKQTY = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString().Trim());  //  1 =확정량 
                dJBYDQTY = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString().Trim());      //  2 =양도량
                dJBYSQTY = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString().Trim());      //  3 =양수량
                dJBYSYDQTY = Convert.ToDouble(dt.Rows[0]["JBYSYDQTY"].ToString().Trim());  //  4 =양수분양도량 
                dJBCSQTY = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString().Trim());      //  5 =통관수량
                dJBCHQTY = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString().Trim());      //  6 =출고수량
                dJBYSCHQTY = Convert.ToDouble(dt.Rows[0]["JBYSCHQTY"].ToString().Trim());  //  7 =양수출고량 
                dJBJANQTY = Convert.ToDouble(dt.Rows[0]["JBJANQTY"].ToString().Trim());    //  8 =잔량 
                dJBCSJANQTY = Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString().Trim());//  9 =통관잔량 
                dJBYSJANQTY = Convert.ToDouble(dt.Rows[0]["JBYSJANQTY"].ToString().Trim());// 10 =양수출고잔량 
                dJBJEGOQTY = Convert.ToDouble(dt.Rows[0]["JBJEGOQTY"].ToString().Trim());  // 11 =재고량 
                sJBHMNO1 = dt.Rows[0]["JBHMNO1"].ToString().Trim();                        // 12 = 화물번호１
                sJBHMNO2 = dt.Rows[0]["JBHMNO2"].ToString().Trim();                        // 13 = 화물번호 2
                if (sJBSOSOK == "")
                {
                    sJBSOSOK = dt.Rows[0]["JBSOSOK"].ToString().Trim();                    // 14 = 협회
                }
                if (Get_Numeric(sJBJESTDAT) == "0")
                {
                    sJBJESTDAT = dt.Rows[0]["JBJESTDAT"].ToString().Trim();                // 15 = 보관시작일
                    sJBCONTNO  = dt.Rows[0]["JBCONTNO"].ToString().Trim();                 // 16 = 계약번호

                    fsYNYS_JBJESTDAT = dt.Rows[0]["JBJESTDAT"].ToString().Trim();          // 15 = 보관시작일
                    fsYNYS_JBCONTNO  = dt.Rows[0]["JBCONTNO"].ToString().Trim();           // 16 = 계약번호
                }

                if (fsJCYDHWAJU == "" && fsJCYSDATE == "0" && fsJCYSSEQ == "0" && fsJCYDSEQ == "0")
                {
                    if (sGUBUN == "SAVE")
                    {
                        // 등록용
                        fdYNYD_JBYDQTY = 0; // 양도량
                        fdYNYD_JBJANQTY = 0; // 잔량
                        fdYNYD_JBJEGOQTY = 0; // 재고량 

                        fdYNYD_JBYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYDQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양도량
                        fdYNYD_JBJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBHWAKQTY - (fdYNYD_JBYDQTY + dJBCHQTY)));   // 잔량
                        fdYNYD_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}",(dJBHWAKQTY + dJBYSQTY) - (fdYNYD_JBYDQTY + dJBYSYDQTY + dJBCHQTY + dJBYSCHQTY)));  // 재고량
                    }
                    else if (sGUBUN == "DEL")
                    {
                        //삭제용 변수 분리 필요
                        fdYNYD_JBYDQTY = 0;// 양도량
                        fdYNYD_JBJANQTY = 0;// 잔량
                        fdYNYD_JBJEGOQTY = 0;// 재고량

                        fdYNYD_JBYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYDQTY - fdOWN_YNYSYDQTY));   // 양도량
                        fdYNYD_JBJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBHWAKQTY - (fdYNYD_JBYDQTY + dJBCHQTY)));   // 잔량
                        fdYNYD_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJBHWAKQTY + dJBYSQTY) - (fdYNYD_JBYDQTY + dJBYSYDQTY + dJBCHQTY + dJBYSCHQTY)));  // 재고량
                    }
                }
                else
                {
                    if (sGUBUN == "SAVE")
                    {
                        //등록용
                        fdYNYD_JBYSYDQTY = 0; // 양수분양도량
                        fdYNYD_JBYSJANQTY = 0; // 양수출고잔량
                        fdYNYD_JBJEGOQTY = 0; // 재고량 

                        fdYNYD_JBYSYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSYDQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양수분양도량
                        fdYNYD_JBYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSQTY - (dJBYSCHQTY + fdYNYD_JBYSYDQTY)));   // 양수출고잔량
                        fdYNYD_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJBHWAKQTY + dJBYSQTY) - (dJBYDQTY + fdYNYD_JBYSYDQTY + dJBCHQTY + dJBYSCHQTY)));  // 재고량

                    }
                    else if (sGUBUN == "DEL")
                    {
                        //삭제용
                        fdYNYD_JBYSYDQTY = 0; // 양수분양도량
                        fdYNYD_JBYSJANQTY = 0; // 양수출고잔량
                        fdYNYD_JBJEGOQTY = 0; // 재고량 

                        fdYNYD_JBYSYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSYDQTY - fdOWN_YNYSYDQTY));   // 양수분양도량
                        fdYNYD_JBYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSQTY - (dJBYSCHQTY + fdYNYD_JBYSYDQTY)));   // 양수출고잔량
                        fdYNYD_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJBHWAKQTY + dJBYSQTY) - (dJBYDQTY + fdYNYD_JBYSYDQTY + dJBCHQTY + dJBYSCHQTY)));  // 재고량
                    }
                }
            }

            dJBBEJNQTY = 0; //  0 = 배정량 (지역)     
            dJBHWAKQTY = 0; //  1 = 확정량 (지역)     
            dJBYDQTY = 0; //  2 = 양도량 (지역)       
            dJBYSQTY = 0; //  3 = 양수량 (지역)       
            dJBYSYDQTY = 0; //  4 = 양수분양도량 (지역)
            dJBCSQTY = 0; //  5 = 통관수량 (지역)      
            dJBCHQTY = 0; //  6 = 출고수량 (지역)      
            dJBYSCHQTY = 0; //  7 = 양수출고량 (지역)  
            dJBJANQTY = 0; //  8 = 잔량 (지역)        
            dJBCSJANQTY = 0; //  9 = 통관잔량 (지역)   
            dJBYSJANQTY = 0; // 10 = 양수출고잔량(지역)
            dJBJEGOQTY = 0; // 11 = 재고량 (지역)     
            sJBHMNO1 = ""; // 12 = 화물번호１(지역)    
            sJBHMNO2 = ""; // 13 = 화물번호 2 (지역)   
            //sJBSOSOK = ""; // 14 = 협회 (지역)        
            //sJBJESTDAT = "0"; // 15 = 보관시작일 (지역)
            //sJBCONTNO = "0"; // 16 = 계약번호 (지역)   

            // 양수화주 양수량 조회 (등록)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96CFW789",
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                this.CBH01_YNYSHWAJU.GetValue().ToString(), // 양수화주
                this.TXT01_YNBLNO.GetValue().ToString(),   // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                this.TXT01_YNHMNO1.GetValue().ToString(),   // 화물번호1
                this.TXT01_YNHMNO2.GetValue().ToString()    // 화물번호2
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsYNSAVEGB2 = "수정";

                dJBBEJNQTY = Convert.ToDouble(dt.Rows[0]["JBBEJNQTY"].ToString().Trim());  //  0 =배정량
                dJBHWAKQTY = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString().Trim());  //  1 =확정량
                dJBYDQTY = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString().Trim());      //  2 =양도량
                dJBYSQTY = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString().Trim());      //  3 =양수량
                dJBYSYDQTY = Convert.ToDouble(dt.Rows[0]["JBYSYDQTY"].ToString().Trim());  //  4 =양수분양도량 
                dJBCSQTY = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString().Trim());      //  5 =통관수량
                dJBCHQTY = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString().Trim());      //  6 =출고수량
                dJBYSCHQTY = Convert.ToDouble(dt.Rows[0]["JBYSCHQTY"].ToString().Trim());  //  7 =양수출고량 
                dJBJANQTY = Convert.ToDouble(dt.Rows[0]["JBJANQTY"].ToString().Trim());    //  8 =잔량 
                dJBCSJANQTY = Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString().Trim());//  9 =통관잔량 
                dJBYSJANQTY = Convert.ToDouble(dt.Rows[0]["JBYSJANQTY"].ToString().Trim());// 10 =양수출고잔량 
                dJBJEGOQTY = Convert.ToDouble(dt.Rows[0]["JBJEGOQTY"].ToString().Trim());  // 11 =재고량 

                if (fsYNYS_JBSOSOK == "")
                {
                    fsYNYS_JBSOSOK = dt.Rows[0]["JBSOSOK"].ToString().Trim();              // 14 = 협회
                }
                if (Get_Numeric(sJBJESTDAT) == "0")
                {
                    fsYNYS_JBJESTDAT = dt.Rows[0]["JBJESTDAT"].ToString().Trim();          // 15 = 보관시작일
                    fsYNYS_JBCONTNO  = dt.Rows[0]["JBCONTNO"].ToString().Trim();           // 16 = 계약번호
                }
            }
            else
            {   
                fsYNYS_JBSOSOK = sJBSOSOK;      // 14 = 협회
                fsYNYS_JBJESTDAT = sJBJESTDAT;  // 15 = 보관시작일
                fsYNYS_JBCONTNO = sJBCONTNO;    // 16 = 계약번호
            }
            
            if (sGUBUN == "SAVE")
            {
                //등록용
                fdYNYS_JBYSQTY = 0;      // 양수량
                fdYNYS_JBYSJANQTY = 0;   // 양수출고잔량
                fdYNYS_JBJEGOQTY = 0;    // 재고량

                fdYNYS_JBYSQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));        // 양수량
                fdYNYS_JBYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSJANQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양수출고잔량
                fdYNYS_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJBHWAKQTY + fdYNYS_JBYSQTY) - (dJBYDQTY + dJBYSYDQTY + dJBCHQTY + dJBYSCHQTY)));   // 재고량
            }
            else if (sGUBUN == "DEL")
            {
                //삭제용
                fdYNYS_JBYSQTY = 0;  // 양수량
                fdYNYS_JBYSJANQTY = 0;   // 양수출고잔량
                fdYNYS_JBJEGOQTY = 0;    // 재고량

                fdYNYS_JBYSQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSQTY - fdOWN_YNYSYDQTY));    // 양수량
                fdYNYS_JBYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBYSJANQTY - fdOWN_YNYSYDQTY));  // 양수출고잔량
                fdYNYS_JBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJBHWAKQTY + fdYNYS_JBYSQTY) - (dJBYDQTY + dJBYSYDQTY + dJBCHQTY + dJBYSCHQTY)));   // 재고량
            }
        }
        #endregion

        #region Description : USIJEGOF 데이터 조회
        private void UP_YANG_USIJEGOF_SetData(string sGUBUN)
        {
            double dJGBEJNQTY = 0; //  1 = 배정량 (지역)      
            double dJGHWAKQTY = 0; //  2 = 확정량 (지역)      
            double dJGYDQTY = 0; //  3 = 양도량 (지역)        
            double dJGYSQTY = 0; //  4 = 양수량 (지역)        
            double dJGYSYDQTY = 0; //  5 = 양수분양도량 (지역)
            double dJGCSQTY = 0; //  6 = 통관수량 (지역)      
            double dJGCHQTY = 0; //  7 = 출고수량 (지역)      
            double dJGYSCHQTY = 0; //  8 = 양수출고량 (지역)  
            double dJGJANQTY = 0; //  9 = 잔량 (지역)        
            double dJGCSJANQTY = 0; // 10 = 통관잔량 (지역)   
            double dJGYSJANQTY = 0; // 11 = 양수출고잔량 (지역)
            double dJGJEGOQTY = 0; // 12 = 재고량 (지역)      

            DataTable dt = new DataTable();

            //양도화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96DDH801", 
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                this.CBH01_YNHWAJU.GetValue().ToString()    // 양도화주
                );
                
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsYNSAVEGB3 = "수정";

                dJGBEJNQTY = Convert.ToDouble(dt.Rows[0]["JGBEJNQTY"].ToString().Trim());    //   1 = 배정량
                dJGHWAKQTY = Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString().Trim());    //   2 = 확정량 	 
                dJGYDQTY = Convert.ToDouble(dt.Rows[0]["JGYDQTY"].ToString().Trim());        //   3 = 양도량
                dJGYSQTY = Convert.ToDouble(dt.Rows[0]["JGYSQTY"].ToString().Trim());        //   4 = 양수량
                dJGYSYDQTY = Convert.ToDouble(dt.Rows[0]["JGYSYDQTY"].ToString().Trim());    //   5 = 양수분양도량
                dJGCSQTY = Convert.ToDouble(dt.Rows[0]["JGCSQTY"].ToString().Trim());        //   6 = 통관수량
                dJGCHQTY = Convert.ToDouble(dt.Rows[0]["JGCHQTY"].ToString().Trim());        //   7 = 출고수량
                dJGYSCHQTY = Convert.ToDouble(dt.Rows[0]["JGYSCHQTY"].ToString().Trim());    //   8 = 양수출고량
                dJGJANQTY = Convert.ToDouble(dt.Rows[0]["JGJANQTY"].ToString().Trim());      //   9 = 잔량
                dJGCSJANQTY = Convert.ToDouble(dt.Rows[0]["JGCSJANQTY"].ToString().Trim());  //  10 = 통관잔량       
                dJGYSJANQTY = Convert.ToDouble(dt.Rows[0]["JGYSJANQTY"].ToString().Trim());  //  11 = 양수출고잔량
                dJGJEGOQTY = Convert.ToDouble(dt.Rows[0]["JGJEGOQTY"].ToString().Trim());    //  12 = 재고량

                if (fsJCYDHWAJU == "" && fsJCYSDATE == "0" && fsJCYSSEQ == "0" && fsJCYDSEQ == "0")
                {
                    if (sGUBUN == "SAVE")
                    {
                        // 등록용
                        fdYNYD_JGYDQTY = 0; // 양도량
                        fdYNYD_JGJANQTY = 0; // 잔량
                        fdYNYD_JGJEGOQTY = 0; // 재고량 

                        fdYNYD_JGYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYDQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양도량
                        fdYNYD_JGJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGHWAKQTY - (fdYNYD_JGYDQTY + dJGCHQTY)));   // 잔량
                        fdYNYD_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJGHWAKQTY + dJGYSQTY) - (fdYNYD_JGYDQTY + dJGYSYDQTY + dJGCHQTY + dJGYSCHQTY)));  // 재고량
                    }
                    else if (sGUBUN == "DEL")
                    {
                        //삭제용
                        fdYNYD_JGYDQTY = 0;   // 양도량
                        fdYNYD_JGJANQTY = 0;   // 잔량
                        fdYNYD_JGJEGOQTY = 0;   // 재고량 

                        fdYNYD_JGYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYDQTY - fdOWN_YNYSYDQTY));   // 양도량
                        fdYNYD_JGJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGHWAKQTY - (fdYNYD_JGYDQTY + dJGCHQTY)));   // 잔량
                        fdYNYD_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJGHWAKQTY + dJGYSQTY) - (fdYNYD_JGYDQTY + dJGYSYDQTY + dJGCHQTY + dJGYSCHQTY)));  // 재고량
                    }
                }
                else
                {
                    if (sGUBUN == "SAVE")
                    {
                        // 등록용
                        fdYNYD_JGYSYDQTY = 0; // 양수분양도량
                        fdYNYD_JGYSJANQTY = 0; // 양수출고잔량
                        fdYNYD_JGJEGOQTY = 0; // 재고량

                        fdYNYD_JGYSYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSYDQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양수분양도량
                        fdYNYD_JGYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSQTY - (dJGYSCHQTY + fdYNYD_JGYSYDQTY)));   // 양수출고잔량
                        fdYNYD_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJGHWAKQTY + dJGYSQTY) - (dJGYDQTY + fdYNYD_JGYSYDQTY + dJGCHQTY + dJGYSCHQTY)));  // 재고량
                    }
                    else if (sGUBUN == "DEL")
                    {
                        //삭제용
                        fdYNYD_JGYSYDQTY = 0; // 양수분양도량
                        fdYNYD_JGYSJANQTY = 0; // 양수출고잔량
                        fdYNYD_JGJEGOQTY = 0; // 재고량

                        fdYNYD_JGYSYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSYDQTY - fdOWN_YNYSYDQTY));   // 양수분양도량
                        fdYNYD_JGYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSQTY - (dJGYSCHQTY + fdYNYD_JGYSYDQTY)));   // 양수출고잔량
                        fdYNYD_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJGHWAKQTY + dJGYSQTY) - (dJGYDQTY + fdYNYD_JGYSYDQTY + dJGCHQTY + dJGYSCHQTY)));  // 재고량
                    }
                }
            }

            dJGBEJNQTY = 0; //  1 = 배정량 (지역)      
            dJGHWAKQTY = 0; //  2 = 확정량 (지역)      
            dJGYDQTY = 0; //  3 = 양도량 (지역)        
            dJGYSQTY = 0; //  4 = 양수량 (지역)        
            dJGYSYDQTY = 0; //  5 = 양수분양도량 (지역)
            dJGCSQTY = 0; //  6 = 통관수량 (지역)      
            dJGCHQTY = 0; //  7 = 출고수량 (지역)      
            dJGYSCHQTY = 0; //  8 = 양수출고량 (지역)  
            dJGJANQTY = 0; //  9 = 잔량 (지역)        
            dJGCSJANQTY = 0; // 10 = 통관잔량 (지역)   
            dJGYSJANQTY = 0; // 11 = 양수출고잔량 (지역)
            dJGJEGOQTY = 0; // 12 = 재고량 (지역)      

            // 양수화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96DDH801", 
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                this.CBH01_YNYSHWAJU.GetValue().ToString()  // 양수화주
                );
                
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsYNSAVEGB4 = "수정";

                dJGBEJNQTY = Convert.ToDouble(dt.Rows[0]["JGBEJNQTY"].ToString().Trim());  //   1 = 배정량
                dJGHWAKQTY = Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString().Trim());  //   2 = 확정량 	 
                dJGYDQTY = Convert.ToDouble(dt.Rows[0]["JGYDQTY"].ToString().Trim());    //   3 = 양도량
                dJGYSQTY = Convert.ToDouble(dt.Rows[0]["JGYSQTY"].ToString().Trim());    //   4 = 양수량
                dJGYSYDQTY = Convert.ToDouble(dt.Rows[0]["JGYSYDQTY"].ToString().Trim());  //   5 = 양수분양도량
                dJGCSQTY = Convert.ToDouble(dt.Rows[0]["JGCSQTY"].ToString().Trim());    //   6 = 통관수량
                dJGCHQTY = Convert.ToDouble(dt.Rows[0]["JGCHQTY"].ToString().Trim());    //   7 = 출고수량
                dJGYSCHQTY = Convert.ToDouble(dt.Rows[0]["JGYSCHQTY"].ToString().Trim());  //   8 = 양수출고량
                dJGJANQTY = Convert.ToDouble(dt.Rows[0]["JGJANQTY"].ToString().Trim());   //   9 = 잔량
                dJGCSJANQTY = Convert.ToDouble(dt.Rows[0]["JGCSJANQTY"].ToString().Trim()); //  10 = 통관잔량       
                dJGYSJANQTY = Convert.ToDouble(dt.Rows[0]["JGYSJANQTY"].ToString().Trim()); //  11 = 양수출고잔량
                dJGJEGOQTY = Convert.ToDouble(dt.Rows[0]["JGJEGOQTY"].ToString().Trim());  //  12 = 재고량
            }
            if (sGUBUN == "SAVE")
            {
                //등록용
                fdYNYS_JGYSQTY = 0;  // 양수량
                fdYNYS_JGYSJANQTY = 0;   // 양수출고잔량
                fdYNYS_JGJEGOQTY = 0;    // 재고량

                fdYNYS_JGYSQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim())))); // 양수량
                fdYNYS_JGYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSJANQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양수출고잔량
                fdYNYS_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJGHWAKQTY + fdYNYS_JGYSQTY) - (dJGYDQTY + dJGYSYDQTY + dJGCHQTY + dJGYSCHQTY)));   // 재고량
            }
            else if (sGUBUN == "DEL")
            {
                //삭제용
                fdYNYS_JGYSQTY = 0;   // 양수량
                fdYNYS_JGYSJANQTY = 0;    // 양수출고잔량
                fdYNYS_JGJEGOQTY = 0; // 재고량

                fdYNYS_JGYSQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSQTY - fdOWN_YNYSYDQTY));   // 양수량
                fdYNYS_JGYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSJANQTY - fdOWN_YNYSYDQTY)); // 양수출고잔량
                fdYNYS_JGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJGHWAKQTY + fdYNYS_JGYSQTY) - (dJGYDQTY + dJGYSYDQTY + dJGCHQTY + dJGYSCHQTY)));  // 재고량
            }
        }
        #endregion

        #region Description : USIJECSNF 데이터 조회
        private void UP_YANG_USIJECSNF_SetData(string sGUBUN)
        {
            string sIHGOKJONG1 = ""; // 1 = 곡종1 (지역)
            string sIHGOKJONG2 = ""; // 2 = 곡종2 (지역)
            string sIHGOKJONG3 = ""; // 3 = 곡종3 (지역)
            string sIHWONSAN1 = ""; // 1 = 원산1 (지역)
            string sIHWONSAN2 = ""; // 2 = 원산2 (지역)
            string sIHWONSAN3 = ""; // 3 = 원산3 (지역)

            double dJCYDQTY = 0; // 1 = 양도량 (지역)      
            double dJCYSQTY = 0; // 2 = 양수량 (지역)      
            double dJCCSQTY = 0; // 3 = 통관수량 (지역)     
            double dJCCHQTY = 0; // 4 = 출고수량 (지역)     
            double dJCJEGOQTY = 0; // 5 = 재고량 (지역)    
            double dJCYSYDQTY = 0; // 6 = 양수분양도량 (지역)
            double dJCYSCHQTY = 0; // 7 = 양수출고량 (지역) 
            string sJCNUMBER = ""; // 8 = 출고순번 (지역)   

            fsYNYS_JCWONSAN  = "";

            DataTable dt = new DataTable();

            // 원산지 조회 (USIIPHAF)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                ("TY_P_US_975FB016",
                this.CBH01_YNHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sIHGOKJONG1 = dt.Rows[0]["IHGOKJONG1"].ToString();
                sIHGOKJONG2 = dt.Rows[0]["IHGOKJONG2"].ToString();
                sIHGOKJONG3 = dt.Rows[0]["IHGOKJONG3"].ToString();
                sIHWONSAN1 = dt.Rows[0]["IHWONSAN1"].ToString();
                sIHWONSAN2 = dt.Rows[0]["IHWONSAN2"].ToString();
                sIHWONSAN3 = dt.Rows[0]["IHWONSAN3"].ToString();
            }

            // 곡종이 같고 원산지가 같은경우 무조건 첫번째 항목이 선택됨 (개선필요)
            fsYNYS_WONSAN = "";
            if (CBH01_YNGOKJONG.GetValue().ToString() == sIHGOKJONG1)
            {
                fsYNYS_WONSAN = sIHWONSAN1;
            }
            else if (CBH01_YNGOKJONG.GetValue().ToString() == sIHGOKJONG2)
            {
                fsYNYS_WONSAN = sIHWONSAN2;
            }
            else if (CBH01_YNGOKJONG.GetValue().ToString() == sIHGOKJONG3)
            {
                fsYNYS_WONSAN = sIHWONSAN3;
            }

            // 양도화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96DHQ812", 
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                fsJCHWAJU,                                  // 양수화주
                this.TXT01_YNBLNO.GetValue().ToString(),    // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""), // 통관일자
                this.TXT01_YNCUSTCH.GetValue().ToString(),  // 통관차수
                fsJCYDHWAJU,                                // 양도화주
                fsJCYSDATE,                                 // 양수일자
                fsJCYSSEQ,                                  // 양수순번
                fsJCYDSEQ                                   // 양도차수
                );
                
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsYNSAVEGB5 = "수정";

                dJCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString().Trim());     // 3  = 양도량
                dJCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString().Trim());     // 4  = 양수량
                dJCCSQTY = Convert.ToDouble(dt.Rows[0]["JCCSQTY"].ToString().Trim());     // 5  = 통관수량
                dJCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString().Trim());     // 6  = 출고수량
                dJCJEGOQTY = Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString().Trim()); // 7  = 재고량
                dJCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString().Trim()); // 8  = 양수분양도량
                dJCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString().Trim()); // 9  = 양수출고량
                sJCNUMBER = dt.Rows[0]["JCNUMBER"].ToString().Trim();                     // 10 = 출고순번

                // 20191227 추가소스
                fsYNYS_JCWONSAN = dt.Rows[0]["JCWONSAN"].ToString().Trim();               // 11 = 원산지

                if (fsJCYDHWAJU == "" && fsJCYSDATE == "0" && fsJCYSSEQ == "0" && fsJCYDSEQ == "0")
                {
                    if (sGUBUN == "SAVE")
                    {
                        // 등록용
                        fdYNYD_JCYDQTY = 0;   // 양도량
                        fdYNYD_JCJEGOQTY = 0; // 재고량

                        fdYNYD_JCYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJCYDQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양도량
                        fdYNYD_JCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + dJCYSQTY) - (fdYNYD_JCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY)));    // 재고량
                    }
                    else if (sGUBUN == "DEL")
                    {
                        // 삭제용
                        fdYNYD_JCYDQTY = 0; // 양도량
                        fdYNYD_JCJEGOQTY = 0; // 재고량

                        fdYNYD_JCYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJCYDQTY - fdOWN_YNYSYDQTY));    // 양도량
                        fdYNYD_JCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + dJCYSQTY) - (fdYNYD_JCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY))); // 재고량
                    }
                }
                else
                {
                    if (sGUBUN == "SAVE")
                    {
                        // 등록용
                        fdYNYD_JCYSYDQTY = 0; // 양수분양도량
                        fdYNYD_JCJEGOQTY = 0; // 재고량

                        fdYNYD_JCYSYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJCYSYDQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양수분양도량
                        fdYNYD_JCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + dJCYSQTY) - (dJCYDQTY + fdYNYD_JCYSYDQTY + dJCCHQTY + dJCYSCHQTY)));    // 재고량
                    }
                    else if (sGUBUN == "DEL")
                    {
                        // 삭제용
                        fdYNYD_JCYSYDQTY = 0;    // 양수분양도량
                        fdYNYD_JCJEGOQTY = 0;    // 재고량

                        fdYNYD_JCYSYDQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJCYSYDQTY - fdOWN_YNYSYDQTY));    // 양수분양도량
                        fdYNYD_JCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + dJCYSQTY) - (dJCYDQTY + fdYNYD_JCYSYDQTY + dJCCHQTY + dJCYSCHQTY))); // 재고량
                    }
                }
            }

            dJCYDQTY = 0; // 1 = 양도량 (지역)      
            dJCYSQTY = 0; // 2 = 양수량 (지역)      
            dJCCSQTY = 0; // 3 = 통관수량 (지역)     
            dJCCHQTY = 0; // 4 = 출고수량 (지역)     
            dJCJEGOQTY = 0; // 5 = 재고량 (지역)    
            dJCYSYDQTY = 0; // 6 = 양수분양도량 (지역)
            dJCYSCHQTY = 0; // 7 = 양수출고량 (지역) 
            sJCNUMBER = ""; // 8 = 출고순번 (지역)   

            // 양수화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96DHQ812",
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                this.CBH01_YNYSHWAJU.GetValue().ToString(), // 양수화주
                this.TXT01_YNBLNO.GetValue().ToString(),    // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""), // 통관일자
                this.TXT01_YNCUSTCH.GetValue().ToString(),  // 통관차수
                this.CBH01_YNHWAJU.GetValue().ToString(),   // 양도화주
                Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ","").Trim()), // 양수일자 
                this.TXT01_YNYSSEQ.GetValue().ToString(),   // 양수순번
                this.TXT01_YNYDSEQ.GetValue().ToString()    // 양도차수
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsYNSAVEGB6 = "수정";

                dJCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString().Trim());    //  3 = 양도량
                dJCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString().Trim());    //  4 = 양수량
                dJCCSQTY = Convert.ToDouble(dt.Rows[0]["JCCSQTY"].ToString().Trim());    //  5 = 통관수량
                dJCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString().Trim());    //  6 = 출고수량
                dJCJEGOQTY = Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString().Trim());  //  7 = 재고량
                dJCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString().Trim());  //  8 = 양수분양도량
                dJCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString().Trim());  //  9 = 양수출고량
                sJCNUMBER = dt.Rows[0]["JCNUMBER"].ToString().Trim();                  // 10 = 출고순번

            }
            if (sGUBUN == "SAVE")
            {
                // 등록용
                fdYNYS_JCYSQTY = 0; // 양수량
                fdYNYS_JCJEGOQTY = 0; // 재고량
                fsYNYS_JCNUMBER = ""; // 출고순번
                fsYNYS_JCNUMBER = "80"; // 출고순번

                fdYNYS_JCYSQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJCYSQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()))));   // 양수량
                fdYNYS_JCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + fdYNYS_JCYSQTY) - (dJCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY)));    // 재고량
            }
            else if (sGUBUN == "DEL")
            {
                // 삭제용
                fdYNYS_JCYSQTY = 0; // 양수량
                fdYNYS_JCJEGOQTY = 0; // 재고량			

                fdYNYS_JCYSQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJCYSQTY - fdOWN_YNYSYDQTY));    // 양수량
                fdYNYS_JCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + fdYNYS_JCYSQTY) - (dJCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY))); // 재고량
            }
        }
        #endregion

        #region Description : USIJEBLF 정리
        private void UP_YANG_USIJEBLF_UPT()
        {
            if (fsYNSAVEGB1 == "수정")
            {
                if (fsJCYDHWAJU == "" && fsJCYSDATE == "0" && fsJCYSSEQ == "0" && fsJCYDSEQ == "0")
                {
                    // 양도량 업데이트
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96CGZ790",
                        fdYNYD_JBYDQTY,                           // 양도량 OK
                        fdYNYD_JBJANQTY,                          // 잔량 OK
                        fdYNYD_JBJEGOQTY,                         // 재고량 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                        this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                        this.CBH01_YNHWAJU.GetValue().ToString(),   // 양도화주
                        this.TXT01_YNBLNO.GetValue().ToString(),   // B/L번호
                        this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                        this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                        this.TXT01_YNHMNO1.GetValue().ToString(),   // 화물번호1
                        this.TXT01_YNHMNO2.GetValue().ToString()    // 화물번호2
                        );
                    
                }
                else
                {
                    // 양도양수량 업데이트
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96CGZ792",
                        fdYNYD_JBYSYDQTY,                         // 양수도분양도량 OK
                        fdYNYD_JBYSJANQTY,                        // 양수출고잔량 OK
                        fdYNYD_JBJEGOQTY,                         // 재고량 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                        this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                        this.CBH01_YNHWAJU.GetValue().ToString(),   // 양도화주
                        this.TXT01_YNBLNO.GetValue().ToString(),   // B/L번호
                        this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                        this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                        this.TXT01_YNHMNO1.GetValue().ToString(),   // 화물번호1
                        this.TXT01_YNHMNO2.GetValue().ToString()    // 화물번호2
                        );
                }
            }

            if (fsYNSAVEGB2 == "수정")
            {   
                // 양수량 업데이트
                this.DbConnector.Attach
                    (
                    "TY_P_US_96CGZ793",
                    fdYNYS_JBYSQTY,                           // 양수량 OK
                    fdYNYS_JBYSJANQTY,                        // 양수출고잔량 OK
                    fdYNYS_JBJEGOQTY,                         // 재고량 OK
                    fsYNYS_JBSOSOK,                            // 협회 OK
                    fsYNYS_JBJESTDAT,                          // 보관시작일 OK
                    fsYNYS_JBCONTNO,                           // 계약번호 OK
                    TYUserInfo.EmpNo.ToString().Trim(),
                    this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                    this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                    this.CBH01_YNYSHWAJU.GetValue().ToString(), // 양수화주
                    this.TXT01_YNBLNO.GetValue().ToString(),    // B/L번호
                    this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                    this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                    this.TXT01_YNHMNO1.GetValue().ToString(),   // 화물번호1
                    this.TXT01_YNHMNO2.GetValue().ToString()    // 화물번호2
                    );
            }
            else
            {
                if (fsWK_GUBUN5 == "NEW" || fsWK_GUBUN5 == "UPT")
                {
                // B/L별 재고 등록
                this.DbConnector.Attach
                    (
                    "TY_P_US_94QEA465",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),     // 1 항차
                    this.CBH01_YNGOKJONG.GetValue().ToString(),     // 2 곡종
                    this.CBH01_YNYSHWAJU.GetValue().ToString(),     // 3 양수화주
                    this.TXT01_YNBLNO.GetValue().ToString(),        // 4 B/L번호
                    this.TXT01_YNBLMSN.GetValue().ToString(),       // 5 MSN
                    this.TXT01_YNBLHSN.GetValue().ToString(),       // 6 HSN
                    this.TXT01_YNHMNO1.GetValue().ToString(),       // 7 화물번호1
                    this.TXT01_YNHMNO2.GetValue().ToString(),       // 8 화물번호2
                    "0",                                            // 9 배정량
                    "0",                                            // 10 확정량
                    "0",                                            // 11 양도량
                    fdYNYS_JBYSQTY,                               // 12 양수량 OK
                    "0",                                            // 13 양수분양도량
                    "0",                                            // 14 통관수량
                    "0",                                            // 15 출고수량
                    "0",                                            // 16 양수출고량
                    "0",                                            // 17 잔량
                    "0",                                            // 18 통관잔량
                    fdYNYS_JBYSJANQTY,                            // 19 양수출고잔량 OK
                    fdYNYS_JBJEGOQTY,                             // 20 재고량 OK
                    fsYNYS_JBSOSOK,                                // 21 협회 OK
                    fsYNYS_JBJESTDAT,                              // 22 보관시작일 OK
                    fsYNYS_JBCONTNO,                               // 23 계약번호 OK
                    "",                                             // 24 이전화주
                    "0",                                            // 25 이전HSN
                    "0",                                            // 26 분할입고배정량
                    "0",                                            // 27 분할출고배정량
                    "0",                                            // 28 분할입고확정량
                    "0",                                            // 29 분할출고확정량
                    TYUserInfo.EmpNo.ToString().Trim()
                    );
                }
            }
        }
        #endregion

        #region Description : USIJEGOF 정리
        private void UP_YANG_USIJEGOF_UPT()
        {
            if (fsYNSAVEGB3 == "수정")
            {
                if (fsJCYDHWAJU == "" && fsJCYSDATE == "0" && fsJCYSSEQ == "0" && fsJCYDSEQ == "0")
                {
                    // 양도량 업데이트         
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96DGG805",
                        fdYNYD_JGYDQTY,                           // 양도량 OK
                        fdYNYD_JGJANQTY,                          // 잔량 OK
                        fdYNYD_JGJEGOQTY,                         // 재고량 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                        this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                        this.CBH01_YNHWAJU.GetValue().ToString()    // 양도화주
                        );
                }
                else
                {
                    // 양수양도량 업데이트
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96DGU808",
                        fdYNYD_JGYSYDQTY,                         // 양수도분양도량 OK
                        fdYNYD_JGYSJANQTY,                        // 양수출고잔량 OK
                        fdYNYD_JGJEGOQTY,                         // 재고량 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                        this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                        this.CBH01_YNHWAJU.GetValue().ToString()    // 양도화주
                        );
                }
            }

            if (fsYNSAVEGB4 == "수정")
            {
                // 양수량 업데이트
                if (fsWK_GUBUN5 == "NEW" || fsWK_GUBUN5 == "UPT")
                {   
                    this.DbConnector.Attach
                            (
                            "TY_P_US_96DGV809",
                            fdYNYS_JGYSQTY,                           // 양수량 OK
                            fdYNYS_JGYSJANQTY,                        // 양수출고잔량 OK
                            fdYNYS_JGJEGOQTY,                         // 재고량 OK
                            fsYNYS_JBSOSOK,                            // 협회 OK
                            fsYNYS_JBCONTNO,                           // 계약번호 OK
                            TYUserInfo.EmpNo.ToString().Trim(),
                            this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                            this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                            this.CBH01_YNYSHWAJU.GetValue().ToString()  // 양수화주
                            );
                }
                else
                {   
                    // 삭제용
                    this.DbConnector.Attach
                            (
                            "TY_P_US_96EGB847",
                            fdYNYS_JGYSQTY,                           // 양수량 OK
                            fdYNYS_JGYSJANQTY,                        // 양수출고잔량 OK
                            fdYNYS_JGJEGOQTY,                         // 재고량 OK
                            TYUserInfo.EmpNo.ToString().Trim(),
                            this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                            this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                            this.CBH01_YNYSHWAJU.GetValue().ToString()  // 양수화주
                            );
                }
            }
            else
            {
                if (fsWK_GUBUN5 == "NEW" || fsWK_GUBUN5 == "UPT")
                {
                    // 재고파일 등록 
                    this.DbConnector.Attach
                        (
                        "TY_P_US_94QF5469",
                        this.CBH01_YNHANGCHA.GetValue().ToString(),     // 1 항차
                        this.CBH01_YNGOKJONG.GetValue().ToString(),     // 2 곡종
                        this.CBH01_YNYSHWAJU.GetValue().ToString(),     // 3 양수화주
                        "0",                                            // 4 배정량
                        "0",                                            // 5 확정량
                        "0",                                            // 6 양도량
                        fdYNYS_JGYSQTY,                               // 7 양수량 OK
                        "0",                                            // 8 양수분양도량
                        "0",                                            // 9 통관수량
                        "0",                                            // 10 출고수량
                        "0",                                            // 11 양수출고량
                        "0",                                            // 12 잔량
                        "0",                                            // 13 통관잔량
                        fdYNYS_JGYSJANQTY,                            // 14 양수출고잔량 OK
                        fdYNYS_JGJEGOQTY,                             // 15 재고량 OK
                        fsYNYS_JBSOSOK,                                // 16 협회 OK
                        fsYNYS_JBJESTDAT,                              // 17 보관시작일 OK
                        fsYNYS_JBCONTNO,                               // 18 계약번호 OK
                        "0",                                            // 19 분할입고배정량
                        "0",                                            // 20 분할출고배정량
                        "0",                                            // 21 분할입고확정량
                        "0",                                            // 22 분할출고확정량
                        TYUserInfo.EmpNo.ToString().Trim()
                        );
                }
            }
        }
        #endregion

        #region Description : USIJECSNF 정리
        private void UP_YANG_USIJECSNF_UPT()
        {
            if (fsYNSAVEGB5 == "수정")
            {
                if (fsJCYDHWAJU == "" && fsJCYSDATE == "0" && fsJCYSSEQ == "0" && fsJCYDSEQ == "0")
                {
                    // 양도량 업데이트
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96DJM813",
                        fdYNYD_JCYDQTY,                                   // 양도량 OK
                        fdYNYD_JCJEGOQTY,                                 // 재고량 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        this.CBH01_YNHANGCHA.GetValue().ToString(),         // 항차
                        this.CBH01_YNGOKJONG.GetValue().ToString(),         // 곡종
                        fsJCHWAJU,                                          // 양수화주 OK
                        TXT01_YNBLNO.GetValue().ToString().ToUpper(),       // B/L번호
                        TXT01_YNBLMSN.GetValue().ToString(),                // MSN
                        TXT01_YNBLHSN.GetValue().ToString(),                // HSN
                        Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""), // 통관일자
                        TXT01_YNCUSTCH.GetValue().ToString(),               // 통관차수
                        fsJCYDHWAJU,                                        // 양도화주 OK
                        fsJCYSDATE,                                         // 양수일자 OK
                        fsJCYSSEQ,                                          // 양수순번 OK
                        fsJCYDSEQ                                           // 양도차수 OK
                        );
                }
                else
                {
                    // 양수양도량 업데이트
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96DJN815",
                        fdYNYD_JCYSYDQTY,                                 // 양수분양도량 OK
                        fdYNYD_JCJEGOQTY,                                 // 재고량 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        this.CBH01_YNHANGCHA.GetValue().ToString(),         // 항차
                        this.CBH01_YNGOKJONG.GetValue().ToString(),         // 곡종
                        fsJCHWAJU,                                          // 양수화주 OK
                        TXT01_YNBLNO.GetValue().ToString().ToUpper(),       // B/L번호
                        TXT01_YNBLMSN.GetValue().ToString(),                // MSN
                        TXT01_YNBLHSN.GetValue().ToString(),                // HSN
                        Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""), // 통관일자
                        TXT01_YNCUSTCH.GetValue().ToString(),               // 통관차수
                        fsJCYDHWAJU,                                        // 양도화주 OK
                        fsJCYSDATE,                                         // 양수일자 OK 
                        fsJCYSSEQ,                                          // 양수순번 OK 
                        fsJCYDSEQ                                           // 양도차수 OK
                        );
                }
            };

            if (fsYNSAVEGB6 == "수정")
            {
                // 양수량 업데이트
                if (fsWK_GUBUN5 == "NEW" || fsWK_GUBUN5 == "UPT")
                {   
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96DJP816",
                        TXT01_YNHMNO1.GetValue().ToString(),                       // 화물번호1
                        TXT01_YNHMNO2.GetValue().ToString(),                       // 화물번호2
                        fdYNYS_JCYSQTY,                                            // 양수량 OK
                        fdYNYS_JCJEGOQTY,                                          // 재고량 OK
                        fsYNYS_JBSOSOK,                                            // 협회 OK
                        fsYNYS_JBJESTDAT.Replace("19000101", "").Replace("44441231", ""), // 보관시작일 OK
                        fsYNYS_JBCONTNO,                                           // 계약번호 OK
                        fsYNYS_JCNUMBER,                                           // 출고순번 OK
                        // 20191227 수정후 소스
                        fsYNYS_JCWONSAN,                                           // 원산지 OK
                        // 20191227 수정전 소스
                        //fsYNYS_WONSAN,                                             // 원산지 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        CBH01_YNHANGCHA.GetValue().ToString(),                     // 항차
                        CBH01_YNGOKJONG.GetValue().ToString(),                     // 곡종
                        CBH01_YNYSHWAJU.GetValue().ToString(),                     // 양수화주
                        TXT01_YNBLNO.GetValue().ToString(),                        // B/L번호
                        TXT01_YNBLMSN.GetValue().ToString(),                       // MSN
                        TXT01_YNBLHSN.GetValue().ToString(),                       // HSN
                        Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),         // 통관일자
                        TXT01_YNCUSTCH.GetValue().ToString(),                      // 통관차수
                        CBH01_YNHWAJU.GetValue().ToString(),                       // 양도화주
                        Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),         // 양수일자
                        TXT01_YNYSSEQ.GetValue().ToString(),                       // 양수순번
                        TXT01_YNYDSEQ.GetValue().ToString()                        // 양도차수
                        );
                }
                else
                {
                    // 삭제용
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96EGQ848",
                        fdYNYS_JCYSQTY,                                           // 양수량 OK
                        fdYNYS_JCJEGOQTY,                                         // 재고량 OK
                        TYUserInfo.EmpNo.ToString().Trim(),
                        CBH01_YNHANGCHA.GetValue().ToString(),                      // 항차
                        CBH01_YNGOKJONG.GetValue().ToString(),                      // 곡종
                        CBH01_YNYSHWAJU.GetValue().ToString(),                     // 양수화주
                        TXT01_YNBLNO.GetValue().ToString(),                         // B/L번호
                        TXT01_YNBLMSN.GetValue().ToString(),                        // MSN
                        TXT01_YNBLHSN.GetValue().ToString(),                        // HSN
                        Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),         // 통관일자
                        TXT01_YNCUSTCH.GetValue().ToString(),                       // 통관차수
                        CBH01_YNHWAJU.GetValue().ToString(),                        // 양도화주
                        Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),         // 양수일자
                        TXT01_YNYSSEQ.GetValue().ToString(),                        // 양수순번
                        TXT01_YNYDSEQ.GetValue().ToString()                         // 양도차수
                        );
                }
            }
            else
            {
                // 통관일별 재고 등록
                if (fsWK_GUBUN5 == "NEW" || fsWK_GUBUN5 == "UPT")
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_US_95VHM664",
                        CBH01_YNHANGCHA.GetValue().ToString(),                      // 항차
                        CBH01_YNGOKJONG.GetValue().ToString(),                      // 곡종
                        CBH01_YNYSHWAJU.GetValue().ToString(),                      // 양수화주
                        TXT01_YNBLNO.GetValue().ToString(),                         // B/L번호
                        TXT01_YNBLMSN.GetValue().ToString(),                        // MSN
                        TXT01_YNBLHSN.GetValue().ToString(),                        // HSN
                        Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),         // 통관일자
                        TXT01_YNCUSTCH.GetValue().ToString(),                       // 통관차수
                        CBH01_YNHWAJU.GetValue().ToString(),                        // 양도하주
                        Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),         // 양수일자
                        TXT01_YNYSSEQ.GetValue().ToString(),                        // 양수순번
                        TXT01_YNYDSEQ.GetValue().ToString(),                        // 양도차수
                        TXT01_YNHMNO1.GetValue().ToString(),                        // 화물번호1
                        TXT01_YNHMNO2.GetValue().ToString(),                        // 화물번호2
                        "0", 	                                                    // 15 양도량  
                        fdYNYS_JCYSQTY,                                             // 16 양수량 OK
                        "0",                                                        // 17 양수분양도량
                        "0",                                                        // 18 통관수량
                        "0",                                                        // 19 출고수량
                        "0",                                                        // 20 양수출고량
                        fdYNYS_JCJEGOQTY,                                           // 21 재고량 OK
                        fsYNYS_JBSOSOK,                                             // 22 협회 OK
                        fsYNYS_JBJESTDAT.Replace("19000101", "").Replace("44441231", ""), // 23 보관시작일 OK
                        fsYNYS_JBCONTNO,                                            // 24 계약번호 OK
                        CBH01_YNYNHWAJU.GetValue().ToString(),                      // 25 원화주
                        "80",	                                                    // 26 출고순번
                        // 20191227 수정후 소스
                        fsYNYS_JCWONSAN,                                            // 27 원산지 OK
                        // 20191227 수정전 소스
                        //fsYNYS_WONSAN,                                              // 27 원산지 OK
                        "0",                                                        // 28 가상출고
                        TYUserInfo.EmpNo.ToString().Trim()
                        );
                }
            };
        }
        #endregion

        #region Description : 양수도관리 저장 체크
        private void BTN66_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bRtn;
            string sJCHMNO1 = "";
            string sJCHMNO2 = "";
            double dJCYDQTY = 0;
            double dJCYSQTY = 0;
            double dJCCSQTY = 0;
            double dJCCHQTY = 0;
            double dJCJEGOQTY = 0;
            double dJCYSYDQTY = 0;
            double dJCYSCHQTY = 0;
            double d_KESAN = 0;

            fsSIKBAEL = "";
            fdYNQTY = 0;
            fdYNYSYDQTY = 0;
            fsYNYSYDHWAJU = "";
            fsYNYSYDDATE = "";
            fsYNYSYDSEQ = "0";

            DataTable dt = new DataTable();

            if (Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
            {
                this.ShowCustomMessage("'양수일자' 항목은 필수입력 항목입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.MTB01_YNYSDATE);
                return;
            }
            else if (Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()).Length < 8)
            {
                this.ShowCustomMessage("양수일자를 확인 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.MTB01_YNYSDATE);
                return;
            }
            else
            {
                bRtn = dateValidateCheck(Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()));

                if (!bRtn)
                {
                    this.ShowCustomMessage("양수일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.MTB01_YNYSDATE);
                    return;
                }
            }
            if (Convert.ToDouble(Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")) > Convert.ToDouble(Get_Numeric(Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()))))
            {
                this.ShowCustomMessage("양수일자를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.MTB01_YNYSDATE);
                e.Successed = false;
                return;
            }

            if (Convert.ToDouble(Get_Numeric(Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()))) > Convert.ToDouble(Get_Date(DTP01_YNBKILJA.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")))
            {
                this.ShowCustomMessage("보관료 시점 일자를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.DTP01_YNBKILJA);
                e.Successed = false;
                return;
            }

            // 보관료 시점일자 체크 (이전, 이후차수 데이터 보관료 시점일자와 비교)
            if (this.CBH01_YNYSYDHWAJ.GetValue().ToString() != "")
            {
                //2차수 이상인 경우만 이전자료 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_A27BS802",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),
                    this.CBH01_YNGOKJONG.GetValue().ToString(),
                    fsJCYDHWAJU,
                    this.TXT01_YNBLNO.GetValue().ToString(),
                    this.TXT01_YNBLMSN.GetValue().ToString(),
                    this.TXT01_YNBLHSN.GetValue().ToString(),
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                    this.TXT01_YNCUSTCH.GetValue().ToString(),
                    fsJCHWAJU,
                    fsJCYSDATE,
                    fsJCYSSEQ,
                    fsJCYDSEQ,
                    this.CBH01_YNYNHWAJU.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDouble(Get_Date(DTP01_YNBKILJA.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")) < Convert.ToDouble(Get_Numeric(Get_Date(dt.Rows[0]["YNBKILJA"].ToString()))))
                    {
                        this.ShowCustomMessage("이전자료의 보관료 시점 일자보다 현재 보관료 시점일자가 작습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.DTP01_YNBKILJA);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (fsWK_GUBUN5 == "UPT")
            {
                // 이후자료 체크 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_A27BR801",
                    this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                    this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                    this.CBH01_YNYSHWAJU.GetValue().ToString(), // 양도화주
                    this.TXT01_YNBLNO.GetValue().ToString(),    // B/L번호
                    this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                    this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),            // 통관일자
                    this.TXT01_YNCUSTCH.GetValue().ToString(),  // 통관차수
                    this.CBH01_YNHWAJU.GetValue().ToString(),   // 이전양도화주
                    Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),            // 이전양수일자
                    this.TXT01_YNYSSEQ.GetValue().ToString(),   // 이전양수순번
                    this.CBH01_YNYNHWAJU.GetValue().ToString()  // 원화주
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["YNBKILJA"].ToString().Trim() != "")
                    {
                        if (Convert.ToDouble(Get_Date(DTP01_YNBKILJA.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")) > Convert.ToDouble(Get_Numeric(Get_Date(dt.Rows[0]["YNBKILJA"].ToString()))))
                        {
                            this.ShowCustomMessage("이후자료의 보관료 시점 일자보다 현재 보관료 시점일자가 큽니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.DTP01_YNBKILJA);
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            if (Convert.ToDouble(Get_Numeric(TXT01_JEGOQTY.GetValue().ToString().Trim())) < 0)
            {
                this.ShowCustomMessage("통관일별 재고파일의 재고량을 확인하세요.(USIJECSNF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.CBH01_YNHANGCHA.CodeText);
                e.Successed = false;
                return;
            }

            if(Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim())) <= 0)
            {
                this.ShowCustomMessage("양수량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.TXT01_YNYSYDQTY);
                e.Successed = false;
                return;
            }

            if (fsWK_GUBUN5 == "NEW")
            {
                if (this.TXT01_YNHMNO1.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("화물번호가 입력되지 않았습니다. 통관조회를 하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_96AHF763",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),
                    this.CBH01_YNGOKJONG.GetValue().ToString(),
                    this.CBH01_YNHWAJU.GetValue().ToString(),
                    this.TXT01_YNBLNO.GetValue().ToString(),
                    this.TXT01_YNBLMSN.GetValue().ToString(),
                    this.TXT01_YNBLHSN.GetValue().ToString(),
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                    Get_Numeric(this.TXT01_YNCUSTCH.GetValue().ToString()),
                    this.CBH01_YNYSHWAJU.GetValue().ToString(),
                    Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),
                    this.TXT01_YNYSSEQ.GetValue().ToString(),
                    this.TXT01_YNYDSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 자료입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.DTP01_YNYSYDDATE);    
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_96AFT757",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),
                    this.CBH01_YNGOKJONG.GetValue().ToString(),
                    fsJCHWAJU,
                    this.TXT01_YNBLNO.GetValue().ToString(),
                    this.TXT01_YNBLMSN.GetValue().ToString(),
                    this.TXT01_YNBLHSN.GetValue().ToString(),
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                    this.TXT01_YNCUSTCH.GetValue().ToString(),
                    fsJCYDHWAJU,
                    fsJCYSDATE,
                    fsJCYSSEQ,
                    fsJCYDSEQ
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sJCHMNO1 = dt.Rows[0]["JCHMNO1"].ToString();
                    sJCHMNO2 = dt.Rows[0]["JCHMNO2"].ToString();
                    dJCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString());
                    dJCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString());
                    dJCCSQTY = Convert.ToDouble(dt.Rows[0]["JCCSQTY"].ToString());
                    dJCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString());
                    dJCJEGOQTY = Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString());
                    dJCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString());
                    dJCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString());
                    d_KESAN = Convert.ToDouble(dt.Rows[0]["JCNUMBER"].ToString());
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96AFT757",
                        this.CBH01_YNHANGCHA.GetValue().ToString(),
                        this.CBH01_YNGOKJONG.GetValue().ToString(),
                        fsJCHWAJU,
                        this.TXT01_YNBLNO.GetValue().ToString(),
                        this.TXT01_YNBLMSN.GetValue().ToString(),
                        this.TXT01_YNBLHSN.GetValue().ToString(),
                        Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                        this.TXT01_YNCUSTCH.GetValue().ToString(),
                        "",
                        "0",
                        "0",
                        "0"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sJCHMNO1 = dt.Rows[0]["JCHMNO1"].ToString();
                        sJCHMNO2 = dt.Rows[0]["JCHMNO2"].ToString();
                        dJCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString());
                        dJCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString());
                        dJCCSQTY = Convert.ToDouble(dt.Rows[0]["JCCSQTY"].ToString());
                        dJCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString());
                        dJCJEGOQTY = Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString());
                        dJCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString());
                        dJCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString());
                        d_KESAN = Convert.ToDouble(dt.Rows[0]["JCNUMBER"].ToString());
                    }
                    else
                    {
                        this.ShowCustomMessage("재고파일에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.CBH01_YNHANGCHA.CodeText);
                        e.Successed = false;
                        return;
                    }
                }
                d_KESAN = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + dJCYSQTY) - (dJCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY)));

                if (d_KESAN < Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim())))
                {
                    this.ShowCustomMessage("양도 수량을 초과했습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.TXT01_YNYSYDQTY);
                    e.Successed = false;
                    return;
                }

                if (fsJCYSSEQ == "0")
                {
                    fsSIKBAEL = TXT01_YNYSSEQ.GetValue().ToString();
                }
                else
                {
                    // 이전 식별자 조회 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_96BHG783",
                        this.CBH01_YNHANGCHA.GetValue().ToString(),
                        this.CBH01_YNGOKJONG.GetValue().ToString(),
                        fsJCYDHWAJU,
                        this.TXT01_YNBLNO.GetValue().ToString(),
                        this.TXT01_YNBLMSN.GetValue().ToString(),
                        this.TXT01_YNBLHSN.GetValue().ToString(),
                        Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                        this.TXT01_YNCUSTCH.GetValue().ToString(),
                        fsJCHWAJU,
                        fsJCYSDATE,
                        fsJCYSSEQ,
                        fsJCYDSEQ,
                        this.CBH01_YNYNHWAJU.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        fsSIKBAEL = dt.Rows[0]["YNSIKBAEL"].ToString() + TXT01_YNYSSEQ.GetValue().ToString();
                    }
                }
            }
            else if (fsWK_GUBUN5 == "UPT")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_96AFT757",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),
                    this.CBH01_YNGOKJONG.GetValue().ToString(),
                    this.CBH01_YNYSHWAJU.GetValue().ToString(),
                    this.TXT01_YNBLNO.GetValue().ToString(),
                    this.TXT01_YNBLMSN.GetValue().ToString(),
                    this.TXT01_YNBLHSN.GetValue().ToString(),
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                    this.TXT01_YNCUSTCH.GetValue().ToString(),
                    this.CBH01_YNHWAJU.GetValue().ToString(),
                    Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),
                    this.TXT01_YNYSSEQ.GetValue().ToString(),
                    this.TXT01_YNYDSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sJCHMNO1 = dt.Rows[0]["JCHMNO1"].ToString();
                    sJCHMNO2 = dt.Rows[0]["JCHMNO2"].ToString();
                    dJCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString());
                    dJCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString());
                    dJCCSQTY = Convert.ToDouble(dt.Rows[0]["JCCSQTY"].ToString());
                    dJCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString());
                    dJCJEGOQTY = Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString());
                    dJCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString());
                    dJCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString());
                    d_KESAN = Convert.ToDouble(dt.Rows[0]["JCNUMBER"].ToString());
                }
                else
                {
                    this.ShowCustomMessage("재고파일에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.CBH01_YNHANGCHA.CodeText);
                    e.Successed = false;
                    return;
                }
                d_KESAN = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + dJCYSQTY + Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim())))
                         - (fdOWN_YNYSYDQTY + dJCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY)));

                if (d_KESAN < 0)
                {
                    this.ShowCustomMessage("양도 수량을 초과했습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.TXT01_YNYSYDQTY);
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_96AFT757",
                    this.CBH01_YNHANGCHA.GetValue().ToString(),
                    this.CBH01_YNGOKJONG.GetValue().ToString(),
                    fsJCHWAJU,
                    this.TXT01_YNBLNO.GetValue().ToString(),
                    this.TXT01_YNBLMSN.GetValue().ToString(),
                    this.TXT01_YNBLHSN.GetValue().ToString(),
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                    this.TXT01_YNCUSTCH.GetValue().ToString(),
                    fsJCYDHWAJU,
                    fsJCYSDATE,
                    fsJCYSSEQ,
                    fsJCYDSEQ
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sJCHMNO1 = dt.Rows[0]["JCHMNO1"].ToString();
                    sJCHMNO2 = dt.Rows[0]["JCHMNO2"].ToString();
                    dJCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString());
                    dJCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString());
                    dJCCSQTY = Convert.ToDouble(dt.Rows[0]["JCCSQTY"].ToString());
                    dJCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString());
                    dJCJEGOQTY = Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString());
                    dJCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString());
                    dJCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString());
                    d_KESAN = Convert.ToDouble(dt.Rows[0]["JCNUMBER"].ToString());
                }
                else
                {
                    this.ShowCustomMessage("재고파일에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.CBH01_YNHANGCHA.CodeText);
                    e.Successed = false;
                    return;
                }
                // (통관량 + 양수량) - (현재양도량 - 이전양도량 + 양도량 + 양수분양도량 + 출고량 + 양수분출고량)
                d_KESAN = Convert.ToDouble(String.Format("{0,9:N3}", (dJCCSQTY + dJCYSQTY)
                         - (Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim())) - fdOWN_YNYSYDQTY + dJCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY)));

                if (d_KESAN < 0)
                {
                    this.ShowCustomMessage("이전화주의 양도수량이 재고수량을 초과했습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.TXT01_YNYSYDQTY);
                    e.Successed = false;
                    return;
                }
            }

            if (CBH01_YNHWAJU.GetValue().ToString() == CBH01_YNYNHWAJU.GetValue().ToString())
            {
                if (TXT01_YNYDSEQ.GetValue().ToString() == "1")
                {
                    fdYNQTY = Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()));
                }
                else
                {
                    fdYNYSYDQTY = Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()));
                    fsYNYSYDHWAJU = "";
                    fsYNYSYDDATE = "";
                    fsYNYSYDSEQ = "0";
                }
            }
            else
            {
                fdYNYSYDQTY = Convert.ToDouble(Get_Numeric(TXT01_YNYSYDQTY.GetValue().ToString().Trim()));
            }

            // 이전 화주 데이터에서 양수일자를 체크
            if (Convert.ToDouble(Get_Numeric(Get_Date(DTP01_YNYSYDDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""))) > Convert.ToDouble(Get_Numeric(Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()))))
            {
                this.ShowCustomMessage("이전화주의 양수일자가 더 큽니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.MTB01_YNYSDATE);
                e.Successed = false;
                return;
            }
            
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 양수도관리 삭제버튼
        private void BTN66_REM_Click(object sender, EventArgs e)
        {
            UP_YANGvarInit();

            UP_YANG_SUIJEBLF_SetData("DEL");
            UP_YANG_USIJEGOF_SetData("DEL");
            UP_YANG_USIJECSNF_SetData("DEL");

            this.DbConnector.CommandClear();

            UP_YANG_USIJEBLF_UPT();
            UP_YANG_USIJEGOF_UPT();
            UP_YANG_USIJECSNF_UPT();

            this.DbConnector.Attach("TY_P_US_96EHM853",
                                    CBH01_YNHANGCHA.GetValue().ToString(),                  // 항차
                                    CBH01_YNGOKJONG.GetValue().ToString(),                  // 곡종
                                    CBH01_YNHWAJU.GetValue().ToString(),                    // 양도화주
                                    TXT01_YNBLNO.GetValue().ToString(),                     // B/L 번호
                                    TXT01_YNBLMSN.GetValue().ToString(),                    // MSN
                                    TXT01_YNBLHSN.GetValue().ToString(),                    // HSN
                                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                             // 통관일자
                                    TXT01_YNCUSTCH.GetValue().ToString(),                   // 통관차수
                                    CBH01_YNYSHWAJU.GetValue().ToString(),                  // 양수화주
                                    Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),     // 양수일자
                                    TXT01_YNYSSEQ.GetValue().ToString(),                    // 양수순번
                                    TXT01_YNYDSEQ.GetValue().ToString()                     // 양도차수
                                    );

            this.DbConnector.ExecuteTranQueryList();

            UP_FieldVisible("INIT");
            UP_FieldClear("양수도");
            fsWK_GUBUN5 = "NEW";

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
            this.CBH01_YNHANGCHA.CodeText.Focus();
        }
        #endregion

        #region Description : 양수도관리 삭제 체크
        private void BTN66_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();
            //  출고량 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96BHM785",
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                this.CBH01_YNHWAJU.GetValue().ToString(),   // 양도화주
                this.TXT01_YNBLNO.GetValue().ToString(),    // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),            // 통관일자
                this.TXT01_YNCUSTCH.GetValue().ToString(),  // 통관차수
                this.CBH01_YNYSHWAJU.GetValue().ToString(), // 양수화주
                Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),            // 양수일자
                this.TXT01_YNYSSEQ.GetValue().ToString(),   // 양수순번
                this.TXT01_YNYDSEQ.GetValue().ToString()    // 양도순번
                );

            dt = this.DbConnector.ExecuteDataTable();


            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDouble(dt.Rows[0]["YNYSCHQTY"].ToString()) > 0)
                {
                    this.ShowCustomMessage("출고 데이터가 존재하여 삭제가 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetFocus(this.CBH01_YNHANGCHA.CodeText);
                    e.Successed = false;
                    return;
                }
            }
            // 이후자료 체크 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96CB3786",
                this.CBH01_YNHANGCHA.GetValue().ToString(), // 항차
                this.CBH01_YNGOKJONG.GetValue().ToString(), // 곡종
                this.CBH01_YNYSHWAJU.GetValue().ToString(), // 양도화주
                this.TXT01_YNBLNO.GetValue().ToString(),    // B/L번호
                this.TXT01_YNBLMSN.GetValue().ToString(),   // MSN
                this.TXT01_YNBLHSN.GetValue().ToString(),   // HSN
                Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),            // 통관일자
                this.TXT01_YNCUSTCH.GetValue().ToString(),  // 통관차수
                this.CBH01_YNHWAJU.GetValue().ToString(),   // 이전양도화주
                Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()),            // 이전양수일자
                this.TXT01_YNYSSEQ.GetValue().ToString(),   // 이전양수순번
                this.CBH01_YNYNHWAJU.GetValue().ToString()  // 원화주
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("이후 양수도 자료가 존재하여 삭제가 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.CBH01_YNHANGCHA.CodeText);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 양수도관리 - 통관조회 코드헬프
        private void BTN66_SILOCODEHELP06_Click(object sender, EventArgs e)
        {
            TYUSGB007S popup = new TYUSGB007S(this.CBH01_YNHANGCHA.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_YNHANGCHA.SetValue(popup.fsJCHANGCHA);   // 항차
                this.CBH01_YNGOKJONG.SetValue(popup.fsJCGOKJONG);   // 곡종

                this.CBH01_YNHWAJU.SetValue(popup.fsJCHWAJU);       // 양수화주
                this.TXT01_YNBLNO.SetValue(popup.fsJCBLNO);         // B/L 번호
                this.TXT01_YNBLMSN.SetValue(popup.fsJCBLMSN);       // MSN
                this.TXT01_YNBLHSN.SetValue(popup.fsJCBLHSN);       // HSN
                this.DTP01_YNCUSTIL.SetValue(popup.fsJCCUSTIL);     // 통관일자
                this.TXT01_YNCUSTCH.SetValue(popup.fsJCCUSTCH);     // 통관차수
                this.TXT01_YNYDSEQ.SetValue(popup.fsYDSEQ);         // 양도순번

                fsJCYDHWAJU = popup.fsJCYDHWAJU;                    // 양도화주
                fsJCYSDATE = popup.fsJCYSDATE;                      // 양수일자
                fsJCYSSEQ = popup.fsJCYSSEQ;                        // 양수순번
                fsJCYDSEQ = popup.fsJCYDSEQ;                        // 양도순번
                fsJCYNHWAJU = popup.fsJCWNHWAJU;                    // 원화주
                fsJCHWAJU = popup.fsJCHWAJU;                        // 양수화주

                UP_USIYANGNF_Run();
                 
                this.SetFocus(this.CBH01_YNYSHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 양수도 관리 확인버튼
        private void BTN66_OK_Click(object sender, EventArgs e)
        {
            UP_USIYANGNF_Run();
        }
        #endregion

        #region Description : 양수도관리 변수 초기화
        private void UP_YANGvarInit()
        {
            fsYNSAVEGB1 = "";
            fsYNSAVEGB2 = "";
            fsYNSAVEGB3 = "";
            fsYNSAVEGB4 = "";
            fsYNSAVEGB5 = "";
            fsYNSAVEGB6 = "";

            fdYNYD_JBYDQTY = 0; 
            fdYNYD_JBJANQTY = 0;
            fdYNYD_JBJEGOQTY = 0;
            fdYNYD_JBYSQTY = 0; 
            fdYNYD_JBYSJANQTY = 0;
            fdYNYD_JBYSYDQTY = 0; 

            fdYNYD_JCYDQTY = 0; 
            fdYNYD_JCJEGOQTY = 0; 
            fdYNYD_JCYSQTY = 0; 
            fsYNYD_JCNUMBER = "";
            fdYNYD_JCYSYDQTY = 0;

            fdYNYD_JGYDQTY = 0; 
            fdYNYD_JGJANQTY = 0; 
            fdYNYD_JGJEGOQTY = 0; 
            fdYNYD_JGYSQTY = 0; 
            fdYNYD_JGYSJANQTY = 0;
            fdYNYD_JGYSYDQTY = 0;

            fsYNYS_JBSOSOK = "";
            fsYNYS_JBJESTDAT = "";
            fsYNYS_JBCONTNO = "";

            fdYNYS_JBYDQTY = 0;
            fdYNYS_JBJANQTY = 0;
            fdYNYS_JBJEGOQTY = 0; 
            fdYNYS_JBYSQTY = 0; 
            fdYNYS_JBYSJANQTY = 0;
            fdYNYS_JBYSYDQTY = 0; 

            fdYNYS_JCYDQTY = 0; 
            fdYNYS_JCJEGOQTY = 0; 
            fdYNYS_JCYSQTY = 0; 
            fsYNYS_JCNUMBER = "";
            fdYNYS_JCYSYDQTY = 0;

            fdYNYS_JGYDQTY = 0;
            fdYNYS_JGJANQTY = 0;
            fdYNYS_JGJEGOQTY = 0;
            fdYNYS_JGYSQTY = 0; 
            fdYNYS_JGYSJANQTY = 0;
            fdYNYS_JGYSYDQTY = 0;

            fsYNYS_JCWONSAN = "";

            fsYNYS_WONSAN = string.Empty; 
        }
        #endregion

        #endregion

        #region Description : 버튼,필드 잠금/해제(공통)
        private void UP_FieldVisible(string sGUBUN)
        {
            if (fsTAB_GUBUN == "USIIPHAF")          // 입항관리
            {
                if (sGUBUN == "NEW")
                {
                    this.CBH01_IHHANGCHA.SetReadOnly(false);

                    this.BTN62_SILOCODEHELP02.Visible = true;
                    this.BTN62_SILOCODEHELP03.Visible = false;
                    this.BTN62_REM.Visible = false;
                    this.BTN62_SAV.Visible = true;
                }
                else if (sGUBUN == "UPT")
                {
                    this.CBH01_IHHANGCHA.SetReadOnly(true);

                    this.BTN62_SILOCODEHELP02.Visible = false;
                    this.BTN62_SILOCODEHELP03.Visible = true;
                    this.BTN62_REM.Visible = true;
                    this.BTN62_SAV.Visible = true;
                }
                else if (sGUBUN == "INIT")
                {
                    this.CBH01_IHHANGCHA.SetReadOnly(false);
                    this.DTP01_IHSHDATE.SetReadOnly(true);
                    this.TXT01_IHSHSEQ.SetReadOnly(true);
                    this.CBO01_IHHALIN.SetReadOnly(true);

                    this.BTN62_SILOCODEHELP02.Visible = false;
                    this.BTN62_SILOCODEHELP03.Visible = false;
                    this.BTN62_SAV.Visible = false;
                    this.BTN62_REM.Visible = false;
                }
            }
            else if (fsTAB_GUBUN == "USIIPGOF")     // 입고관리
            {
                if (sGUBUN == "NEW")
                {
                    this.CBH01_IPHANGCHA.SetReadOnly(false);
                    this.CBH01_IPGOKJONG.SetReadOnly(false);

                    this.BTN63_SILOCODEHELP04.Visible = true;
                    this.BTN63_REM.Visible = false;
                    this.BTN63_SAV.Visible = true;
                }
                else if (sGUBUN == "UPT")
                {
                    this.CBH01_IPHANGCHA.SetReadOnly(true);
                    this.CBH01_IPGOKJONG.SetReadOnly(true);

                    this.BTN63_SILOCODEHELP04.Visible = false;
                    this.BTN63_REM.Visible = true;
                    this.BTN63_SAV.Visible = true;
                }
                else if (sGUBUN == "INIT")
                {
                    this.CBH01_IPHANGCHA.SetReadOnly(false);
                    this.CBH01_IPGOKJONG.SetReadOnly(false);

                    this.BTN63_SILOCODEHELP04.Visible = false;
                    this.BTN63_SAV.Visible = false;
                    this.BTN63_REM.Visible = false;
                }
            }
            else if (fsTAB_GUBUN == "USIIPBLF")     // B/L별 입고관리
            {
                if (sGUBUN == "NEW")
                {
                    this.CBH01_IBHANGCHA.SetReadOnly(false);
                    this.CBH01_IBGOKJONG.SetReadOnly(false);
                    this.CBH01_IBHWAJU.SetReadOnly(false);
                    this.TXT01_IBBLNO.SetReadOnly(false);
                    this.TXT01_IBBLMSN.SetReadOnly(false);
                    this.TXT01_IBBLHSN.SetReadOnly(false);
                    this.MTB01_IBDATE.SetReadOnly(false);
                    this.MTB01_IBDATE.Enabled = true;
                    
                    this.BTN64_SILOCODEHELP04.Visible = true;
                    this.BTN64_CONTNO.Visible = true;
                    this.BTN64_SILOCODEHELP12.Visible = true;
                    this.BTN64_SAV.Visible = true;
                    this.BTN64_REM.Visible = false;
                }
                else if (sGUBUN == "UPT")
                {
                    this.CBH01_IBHANGCHA.SetReadOnly(true);
                    this.CBH01_IBGOKJONG.SetReadOnly(true);
                    this.CBH01_IBHWAJU.SetReadOnly(true);
                    this.TXT01_IBBLNO.SetReadOnly(true);
                    this.TXT01_IBBLMSN.SetReadOnly(true);
                    this.TXT01_IBBLHSN.SetReadOnly(true);
                    this.MTB01_IBDATE.SetReadOnly(true);
                    this.MTB01_IBDATE.Enabled = false;

                    this.BTN64_SILOCODEHELP04.Visible = false;
                    this.BTN64_CONTNO.Visible = true;
                    this.BTN64_SILOCODEHELP12.Visible = true;
                    this.BTN64_SAV.Visible = true;
                    this.BTN64_REM.Visible = true;
                }
                else if (sGUBUN == "INIT")
                {
                    this.CBH01_IBHANGCHA.SetReadOnly(false);
                    this.CBH01_IBGOKJONG.SetReadOnly(false);
                    this.CBH01_IBHWAJU.SetReadOnly(false);
                    this.TXT01_IBBLNO.SetReadOnly(false);
                    this.TXT01_IBBLMSN.SetReadOnly(false);
                    this.TXT01_IBBLHSN.SetReadOnly(false);
                    this.MTB01_IBDATE.SetReadOnly(false);
                    this.MTB01_IBDATE.Enabled = true;

                    this.BTN64_SILOCODEHELP04.Visible = false;
                    this.BTN64_CONTNO.Visible = false;
                    this.BTN64_SILOCODEHELP12.Visible = false;
                    this.BTN64_SAV.Visible = false;
                    this.BTN64_REM.Visible = false;
                }
            }
            else if (fsTAB_GUBUN == "USICUSTF")     // 통관관리
            {
                if (sGUBUN == "NEW")
                {
                    this.CBH01_CSHANGCHA.SetReadOnly(false);
                    this.CBH01_CSGOKJONG.SetReadOnly(false);
                    this.MTB01_CSDATE.SetReadOnly(false);
                    this.MTB01_CSDATE.Enabled = true;
                    
                    this.BTN65_SILOCODEHELP05.Visible = true;
                    this.BTN65_SAV.Visible = true;
                    this.BTN65_REM.Visible = false;
                }
                else if (sGUBUN == "UPT")
                {
                    this.CBH01_CSHANGCHA.SetReadOnly(true);
                    this.CBH01_CSGOKJONG.SetReadOnly(true);
                    this.MTB01_CSDATE.SetReadOnly(true);
                    this.MTB01_CSDATE.Enabled = false;
                    
                    this.BTN65_SILOCODEHELP05.Visible = false;
                    this.BTN65_SAV.Visible = true;
                    this.BTN65_REM.Visible = true;
                }
                else if (sGUBUN == "INIT")
                {
                    this.CBH01_CSHANGCHA.SetReadOnly(false);
                    this.CBH01_CSGOKJONG.SetReadOnly(false);
                    this.MTB01_CSDATE.SetReadOnly(false);
                    this.MTB01_CSDATE.Enabled = true;

                    this.BTN65_SILOCODEHELP05.Visible = false;
                    this.BTN65_SAV.Visible = false;
                    this.BTN65_REM.Visible = false;
                }
            }
            else if (fsTAB_GUBUN == "USIYANGNF")    // 양수도관리
            {
                if (sGUBUN == "NEW")
                {
                    this.CBH01_YNHANGCHA.SetReadOnly(false);
                    this.CBH01_YNGOKJONG.SetReadOnly(false);
                    this.MTB01_YNYSDATE.SetReadOnly(false);
                    this.MTB01_YNYSDATE.Enabled = true;
                    //this.TXT01_YNYSSEQ.SetReadOnly(false);
                    //this.TXT01_YNYDSEQ.SetReadOnly(false);
                    this.CBH01_YNYSHWAJU.SetReadOnly(false);

                    this.BTN66_SILOCODEHELP06.Visible = true;
                    this.BTN66_REM.Visible = false;
                    this.BTN66_SAV.Visible = true;
                }
                else if (sGUBUN == "UPT")
                {
                    this.CBH01_YNHANGCHA.SetReadOnly(true);
                    this.CBH01_YNGOKJONG.SetReadOnly(true);
                    this.MTB01_YNYSDATE.SetReadOnly(true);
                    this.MTB01_YNYSDATE.Enabled = false;
                    //this.TXT01_YNYSSEQ.SetReadOnly(true);
                    //this.TXT01_YNYDSEQ.SetReadOnly(true);
                    this.CBH01_YNYSHWAJU.SetReadOnly(true);

                    this.BTN66_SILOCODEHELP06.Visible = false;
                    this.BTN66_SAV.Visible = true;
                    this.BTN66_REM.Visible = true;
                }
                else if (sGUBUN == "INIT")
                {
                    this.CBH01_YNHANGCHA.SetReadOnly(false);
                    this.CBH01_YNGOKJONG.SetReadOnly(false);
                    this.MTB01_YNYSDATE.SetReadOnly(false);
                    this.MTB01_YNYSDATE.Enabled = true;
                    //this.TXT01_YNYSSEQ.SetReadOnly(false);
                    //this.TXT01_YNYDSEQ.SetReadOnly(false);
                    this.CBH01_YNYSHWAJU.SetReadOnly(false);

                    this.CBH01_YNHWAJU.SetReadOnly(true);
                    this.DTP01_YNCUSTIL.SetReadOnly(true);
                    this.CBH01_YNYSYDHWAJ.SetReadOnly(true);
                    this.DTP01_YNYSYDDATE.SetReadOnly(true);
                    this.CBH01_YNYNHWAJU.SetReadOnly(true);

                    this.BTN66_SILOCODEHELP06.Visible = false;
                    this.BTN66_SAV.Visible = false;
                    this.BTN66_REM.Visible = false;
                }
            }
        }
        #endregion

        #region Description : 필드 초기화(공통)
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN == "입항")
            {
                //this.CBH01_IHHANGCHA.SetValue("");

                this.CBO01_IHHALIN.SetValue("");    // 할인 구분 필드 현재 미사용(접안료 매출 생성시 사용되었음)
                this.TXT01_IHGROSS.SetValue("");
                this.MTB01_IHIPHANG.SetValue("");
                this.MTB01_IHIPHTIM.SetValue("");
                this.MTB01_IHJUBDAT.SetValue("");
                this.MTB01_IHJUBTIM.SetValue("");
                this.MTB01_IHJAKSTDAT.SetValue("");
                this.MTB01_IHJAKSTTIM.SetValue("");
                this.MTB01_IHJAKENDAT.SetValue("");
                this.MTB01_IHJAKENTIM.SetValue("");
                this.MTB01_IHIANDAT.SetValue("");
                this.MTB01_IHIANTIM.SetValue("");
                this.MTB01_IHHUNSTDAT.SetValue("");
                this.MTB01_IHHUNEDDAT.SetValue("");

                this.CBH01_IHSOSOK.SetValue("");
                this.CBH01_IHHWAJU.SetValue("");
                this.TXT01_IHJUKHANO.SetValue("");
                this.TXT01_IHBANIPNO.SetValue("");
                this.CBH01_IHGOKJONG1.SetValue("");
                this.CBH01_IHWONSAN1.SetValue("");
                this.TXT01_IHBLQTY1.SetValue("");
                this.CBH01_IHGOKJONG2.SetValue("");
                this.CBH01_IHWONSAN2.SetValue("");
                this.TXT01_IHBLQTY2.SetValue("");
                this.CBH01_IHGOKJONG3.SetValue("");
                this.CBH01_IHWONSAN3.SetValue("");
                this.TXT01_IHBLQTY3.SetValue("");
                this.TXT01_IHBLQTY_HAP.SetValue("");

                this.CBH01_IHBRANCH.SetValue("");
                this.CBO01_IHSUNHU.SetValue("1");
                this.CBO01_IHSODOK.SetValue("1");
                this.CBO01_IHVESLGUB.SetValue("N");
                this.CBH01_IHLSCODE.SetValue("");
                this.CBO01_IHLMOGB.SetValue("N");
                this.CBO01_IHHUVSGB.SetValue("1");
                this.TXT01_IHHUWKQTY.SetValue("");
                this.CBO01_IHUSEDGUB.SetValue("2");
                this.CBO01_IHHMHALIN.SetValue("N");
                this.CBH01_IHTAXCODE.SetValue("");
                this.DTP01_IHSHDATE.SetValue("");
                this.TXT01_IHSHSEQ.SetValue("");

                fsWK_GUBUN1 = "NEW";
            }
            else if (sGUBUN == "입고")
            {
                //this.CBH01_IPHANGCHA.SetValue("");
                //this.CBH01_IPGOKJONG.SetValue("");

                this.MTB01_IPIPHANG.SetValue("");
                this.TXT01_IPBLQTY.SetValue("");
                this.TXT01_IPJANQTY.SetValue("");

                this.MTB01_IPIPSTDAT.SetValue("");
                this.TXT01_IPHAP.SetValue("");
                this.TXT01_IPIPHAP.SetValue("");
                this.TXT01_IPTOTQTY.SetValue("");

                this.TXT01_IPAUTOTOTQTY.SetValue("");
                this.TXT01_IPIPDHAP.SetValue("");
                this.TXT01_IPDHAP.SetValue("");

                DataTable dt = new DataTable();
                DataRow row;

                dt.Columns.Add("IPIPDAT1", typeof(System.String));
                dt.Columns.Add("IPIPQTY1", typeof(System.Double));
                dt.Columns.Add("IPIPPQTY1", typeof(System.Double));
                dt.Columns.Add("IPIPDAT2", typeof(System.String));
                dt.Columns.Add("IPIPQTY2", typeof(System.Double));
                dt.Columns.Add("IPIPPQTY2", typeof(System.Double));

                for (int i = 0; i < 5; i++)
                {
                    row = dt.NewRow();

                    row["IPIPDAT1"] = "";
                    row["IPIPQTY1"] = 0.000;
                    row["IPIPPQTY1"] = 0.000;
                    row["IPIPDAT2"] = "";
                    row["IPIPQTY2"] = 0.000;
                    row["IPIPPQTY2"] = 0.000;

                    dt.Rows.Add(row);
                }

                this.FPS91_TY_S_US_945HS264.SetValue(dt);

                DataTable dt2 = new DataTable();
                DataRow row2;

                dt2.Columns.Add("IPIDDAT1", typeof(System.String));
                dt2.Columns.Add("IPIDQTY1", typeof(System.Double));
                dt2.Columns.Add("IPIPDQTY1", typeof(System.Double));
                dt2.Columns.Add("IPIDDAT2", typeof(System.String));
                dt2.Columns.Add("IPIDQTY2", typeof(System.Double));
                dt2.Columns.Add("IPIPDQTY2", typeof(System.Double));

                for (int i = 0; i < 5; i++)
                {
                    row2 = dt2.NewRow();

                    row2["IPIDDAT1"] = "";
                    row2["IPIDQTY1"] = 0.000;
                    row2["IPIPDQTY1"] = 0.000;
                    row2["IPIDDAT2"] = "";
                    row2["IPIDQTY2"] = 0.000;
                    row2["IPIPDQTY2"] = 0.000;

                    dt2.Rows.Add(row2);
                }

                this.FPS91_TY_S_US_94II4400.SetValue(dt2);
                fsWK_GUBUN2 = "NEW";
            }
            else if (sGUBUN == "B/L별입고")
            {
                this.DTP01_IHIPHANG2.SetValue("");
                this.TXT01_IHBLQTY5.SetValue("");
                this.TXT01_JKBEJNQTY.SetValue("");
                this.TXT01_JKHWAKQTY.SetValue("");

                this.TXT01_IBHBLNO.SetValue("");
                this.CBO01_IBBHGUBN.SetValue("");
                this.CBH01_IBGUBUN.SetValue("");
                this.CBH01_IBWONSAN.SetValue("");
                this.TXT01_IBBEJNQTY.SetValue("");
                this.TXT01_IBHWAKQTY.SetValue("");
                this.MTB01_IBHWAKIL.SetValue("");
                this.CBH01_IBSOSOK.SetValue("");
                this.MTB01_IBJESTDAT.SetValue("");
                this.TXT01_IBCONTNO.SetValue("");
                //this.CBH01_IBBPSAGO.SetValue("");
                //this.CBH01_IBUNSONG.SetValue("");

                this.MTB01_IBBLDATE.SetValue("");
                this.CBH01_IBJNHWAJU.SetValue("");
                this.TXT01_IBJNBLHSN.SetValue("");
                this.TXT01_IBJNBLSEQ.SetValue("");
                this.TXT01_IBBEIPQTY.SetValue("");
                this.TXT01_IBHWIPQTY.SetValue("");
                this.TXT01_IBBECHQTY.SetValue("");
                this.TXT01_IBHWCHQTY.SetValue("");

                fsWK_GUBUN3 = "NEW";
            }
            else if (sGUBUN == "통관")
            {
                this.TXT01_CSHMNO1.SetValue("");
                this.TXT01_CSHMNO2.SetValue("");

                this.TXT01_JBBEJNQTY.SetValue("");
                this.TXT01_JBHWAKQTY.SetValue("");
                this.TXT01_JBYDQTY.SetValue("");
                this.TXT01_JBYSQTY.SetValue("");
                this.TXT01_JBCSQTY.SetValue("");
                this.TXT01_JBCSJANQTY.SetValue("");
                this.TXT01_JBCHQTY.SetValue("");
                this.TXT01_JBJEGOQTY.SetValue("");

                this.TXT01_CSQTY.SetValue("");
                this.TXT01_CSCHQTY.SetValue("");
                this.TXT01_SCCHQTY.SetValue("");
                this.TXT01_CSJGQTY.SetValue("");
                this.TXT01_JCJEGOQTY.SetValue("");
                this.TXT01_CSCOSTUS.SetValue("");
                this.TXT01_CSCOSTWN.SetValue("");
                this.TXT01_CSSINNO.SetValue("");
                this.TXT01_CSSINQTY.SetValue("");
                this.CBH01_CSSINNM.SetValue("");
                this.CBH01_CSBANGB.SetValue("");
                this.DTP01_CSYJDATE.SetValue("");
                this.TXT01_CSYJNUMB.SetValue("");
                this.DTP01_CSEDDATE.SetValue("");

                fsWK_GUBUN4 = "NEW";
            }
            else if (sGUBUN == "양수도")
            {
                this.TXT01_YNHMNO1.SetValue("");
                this.TXT01_YNHMNO2.SetValue("");
                this.TXT01_YSQTY.SetValue("");
                this.TXT01_YDQTY.SetValue("");
                this.TXT01_YSYDQTY.SetValue("");
                this.TXT01_YSCHQTY.SetValue("");
                this.TXT01_JEGOQTY.SetValue("");

                this.CBH01_YNYSYDHWAJ.SetValue("");
                this.DTP01_YNYSYDDATE.SetValue("");
                this.TXT01_YNYSYDSEQ.SetValue("");
                this.TXT01_YNSIKBAEL.SetValue("");

                this.DTP01_YNBKILJA.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                this.CBH01_YNBKJNHJ.SetValue("");
                this.CBH01_YNBKHUHJ.SetValue("");
                this.TXT01_YNYSYDQTY.SetValue("");
                this.TXT01_YNYSCHQTY.SetValue("");
                this.CBH01_YNYNHWAJU.SetValue("");

                fsWK_GUBUN5 = "NEW";
            }

            //UP_FieldVisible("NEW");
        }
        #endregion

        #region Description : 탭 선택 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // 입항 및 통관관리 조회
            {
                fsTAB_GUBUN = "INVENTORY";

                UP_INVENTORY_Search();
                //UP_INVENTORY_TAB_Search();
            }
            else if (tabControl1.SelectedIndex == 1) // 입항관리
            {
                fsTAB_GUBUN = "USIIPHAF";
                UP_FieldVisible("INIT");
                UP_FieldClear("입항");
                Get_Cookie();
                UP_USIIPHAF_Search();
                UP_USIIPHAF_TAB_Search();

                if (CBH01_IHHANGCHA.GetValue().ToString() != "")
                {
                    UP_USIIPHAF_Run();
                }
                else
                {
                    UP_Set_KeyFocus();
                }
            }
            else if (tabControl1.SelectedIndex == 2) // 입고관리
            {
                fsTAB_GUBUN = "USIIPGOF";
                UP_FieldVisible("INIT");
                UP_FieldClear("입고");
                Get_Cookie();
                UP_USIIPGOF_Search();
                UP_USIIPGOF_TAB_Search();

                if (CBH01_IPHANGCHA.GetValue().ToString() != "" && CBH01_IPGOKJONG.GetValue().ToString() != "")
                {
                    UP_USIIPGOF_Run();
                    if (fsWK_GUBUN2 == "NEW")
                    {
                        SetFocus(MTB01_IPIPHANG);
                    }
                }
                else
                {
                    UP_Set_KeyFocus();
                }
            }
            else if (tabControl1.SelectedIndex == 3) // B/L별 입고관리
            {
                fsTAB_GUBUN = "USIIPBLF";
                UP_FieldVisible("INIT");
                UP_FieldClear("B/L별입고");
                Get_Cookie();
                UP_USIIPBLF_Search();
                UP_USIIPBLF_TAB_Search();
                fsIBBPSAGO = "";
                fsIBUNSONG = "900";

                if(CBH01_IBHANGCHA.GetValue().ToString() != "" && CBH01_IBGOKJONG.GetValue().ToString() != "" &&
                    CBH01_IBHWAJU.GetValue().ToString() != "" && TXT01_IBBLNO.GetValue().ToString() != "" &&
                    TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && TXT01_IBBLHSN.GetValue().ToString().Trim() != "" &&
                    TXT01_IBBLSEQ.GetValue().ToString().Trim() != "" && Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()) != "" &&
                    TXT01_IBHMNO1.GetValue().ToString() != "" && TXT01_IBHMNO2.GetValue().ToString() != "")
                {
                    UP_USIIPBLF_Run("RUN");
                    if (fsWK_GUBUN3 == "NEW")
                    {
                        SetFocus(TXT01_IBHBLNO);
                    }
                }
                else
                {
                    UP_Set_KeyFocus();
                }
            }
            else if (tabControl1.SelectedIndex == 4) // 통관관리
            {
                fsTAB_GUBUN = "USICUSTF";
                UP_FieldVisible("INIT");
                UP_FieldClear("통관");
                Get_Cookie();
                UP_USICUSTF_Search();
                UP_USICUSTF_TAB_Search();

                if (CBH01_CSHANGCHA.GetValue().ToString() != "" && CBH01_CSGOKJONG.GetValue().ToString() != "" &&
                    CBH01_CSHWAJU.GetValue().ToString() != "" && TXT01_CSBLNO.GetValue().ToString() != "" &&
                    TXT01_CSBLMSN.GetValue().ToString().Trim() != "" && TXT01_CSBLHSN.GetValue().ToString().Trim() != "" &&
                    Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()) != "" && TXT01_CSSEQ.GetValue().ToString().Trim() != "" &&
                    TXT01_CSHMNO1.GetValue().ToString() != "" && TXT01_CSHMNO2.GetValue().ToString() != "")
                {
                    UP_USICUSTF_Run();
                    if (fsWK_GUBUN4 == "NEW")
                    {
                        SetFocus(TXT01_CSQTY);
                    }
                }
                else
                {
                    UP_Set_KeyFocus();
                }
            }
            else if (tabControl1.SelectedIndex == 5) // 양수도관리
            {
                fsTAB_GUBUN = "USIYANGNF";
                UP_FieldVisible("INIT");
                UP_FieldClear("양수도");
                Get_Cookie();
                UP_USIYANGNF_Search();
                UP_USIYANGNF_TAB_Search();

                if (CBH01_YNHANGCHA.GetValue().ToString() != "" && CBH01_YNGOKJONG.GetValue().ToString() != "" &&
                    CBH01_YNHWAJU.GetValue().ToString() != "" && TXT01_YNBLNO.GetValue().ToString() != "" &&
                    TXT01_YNBLMSN.GetValue().ToString() != "" && TXT01_YNBLHSN.GetValue().ToString() != "" &&
                    Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "") != "" && TXT01_YNCUSTCH.GetValue().ToString() != "" &&
                    CBH01_YNYSHWAJU.GetValue().ToString() != "" && Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()) != "" &&
                    TXT01_YNYSSEQ.GetValue().ToString() != "" && TXT01_YNYDSEQ.GetValue().ToString() != "")
                {
                    UP_USIYANGNF_Run();
                    if (fsWK_GUBUN5 == "NEW")
                    {
                        SetFocus(DTP01_YNBKILJA);
                    }
                }
                else
                {
                    UP_Set_KeyFocus();
                }
            }
        }
        #endregion

        #region Description : 키 이벤트
        // 시작항차 입력 시 종료항차 동일자료 입력
        private void CBH01_STHANGCHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_EDHANGCHA.SetValue(this.CBH01_STHANGCHA.GetValue().ToString());
            }
        }
        private void CBH01_IHHANGCHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsWK_GUBUN1 == "NEW")
                {
                    if (this.CBH01_IHHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPHAF_Run();
                    }
                }
            }
        }
        private void CBH01_IPHANGCHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsWK_GUBUN2 == "NEW")
                {
                    if (CBH01_IPHANGCHA.GetValue().ToString() != "" && CBH01_IPGOKJONG.GetValue().ToString() != "")
                    {
                        UP_USIIPGOF_Run();
                    }
                }
            }
        }

        private void CBH01_IPGOKJONG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsWK_GUBUN2 == "NEW")
                {
                    if (CBH01_IPHANGCHA.GetValue().ToString() != "" && CBH01_IPGOKJONG.GetValue().ToString() != "")
                    {
                        UP_USIIPGOF_Run();
                    }
                }
            }
        }
        // B/L별 입고관리 적하목록 조회
        private void CBH01_IBHANGCHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (TXT01_IBBLMSN.ReadOnly == false && TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && this.TXT01_IBBLHSN.GetValue().ToString().Trim() != "")
                {
                    if (this.CBH01_IBHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPBLF_Run("NEW");
                    }
                }
                if (fsWK_GUBUN3 == "NEW" && CBH01_IBHWAJU.GetValue().ToString() != "" && CBH01_IBSOSOK.GetValue().ToString() == "" && CBH01_IBWONSAN.GetValue().ToString() == "")
                {
                    UP_Set_IPBLWONSAN();
                }
            }
        }

        private void CBH01_IBGOKJONG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (TXT01_IBBLMSN.ReadOnly == false && TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && this.TXT01_IBBLHSN.GetValue().ToString().Trim() != "")
                {
                    if (this.CBH01_IBHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPBLF_Run("NEW");
                    }
                }
                if (fsWK_GUBUN3 == "NEW" && CBH01_IBHWAJU.GetValue().ToString() != "" && CBH01_IBSOSOK.GetValue().ToString() == "" && CBH01_IBWONSAN.GetValue().ToString() == "")
                {
                    UP_Set_IPBLWONSAN();
                }
            }
        }

        private void CBH01_IBHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (TXT01_IBBLMSN.ReadOnly == false && TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && this.TXT01_IBBLHSN.GetValue().ToString().Trim() != "")
                {
                    if (this.CBH01_IBHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPBLF_Run("NEW");
                    }
                }
                if (fsWK_GUBUN3 == "NEW" && CBH01_IBHWAJU.GetValue().ToString() != "" && CBH01_IBSOSOK.GetValue().ToString() == "" && CBH01_IBWONSAN.GetValue().ToString() == "")
                {
                    UP_Set_IPBLWONSAN();
                }
            }
        }

        private void TXT01_IBBLNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (TXT01_IBBLMSN.ReadOnly == false && TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && this.TXT01_IBBLHSN.GetValue().ToString().Trim() != "")
                {
                    if (this.CBH01_IBHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPBLF_Run("NEW");
                    }
                }
                if (fsWK_GUBUN3 == "NEW" && CBH01_IBHWAJU.GetValue().ToString() != "" && CBH01_IBSOSOK.GetValue().ToString() == "" && CBH01_IBWONSAN.GetValue().ToString() == "")
                {
                    UP_Set_IPBLWONSAN();
                }
            }
        }

        private void TXT01_IBBLMSN_KeyPress(object sender, KeyPressEventArgs e)
        {   
            if (e.KeyChar == '\r')
            {
                if (TXT01_IBBLMSN.ReadOnly == false && TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && this.TXT01_IBBLHSN.GetValue().ToString().Trim() != "")
                {
                    if (this.CBH01_IBHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPBLF_Run("NEW");
                    }
                }
                if (fsWK_GUBUN3 == "NEW" && CBH01_IBHWAJU.GetValue().ToString() != "" && CBH01_IBSOSOK.GetValue().ToString() == "" && CBH01_IBWONSAN.GetValue().ToString() == "")
                {
                    UP_Set_IPBLWONSAN();
                }
            }
        }

        private void TXT01_IBBLHSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (TXT01_IBBLMSN.ReadOnly == false && TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && this.TXT01_IBBLHSN.GetValue().ToString().Trim() != "")
                {
                    if (this.CBH01_IBHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPBLF_Run("NEW");
                    }
                }
                if (fsWK_GUBUN3 == "NEW" && CBH01_IBHWAJU.GetValue().ToString() != "" && CBH01_IBSOSOK.GetValue().ToString() == "" && CBH01_IBWONSAN.GetValue().ToString() == "")
                {
                    UP_Set_IPBLWONSAN();
                }
            }
        }

        private void MTB01_IBDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (TXT01_IBBLMSN.ReadOnly == false && TXT01_IBBLMSN.GetValue().ToString().Trim() != "" && this.TXT01_IBBLHSN.GetValue().ToString().Trim() != "")
                {
                    if (this.CBH01_IBHANGCHA.GetValue().ToString() != "")
                    {
                        UP_USIIPBLF_Run("NEW");
                    }
                }
                if (fsWK_GUBUN3 == "NEW" && CBH01_IBSOSOK.GetValue().ToString() == "" && CBH01_IBWONSAN.GetValue().ToString() == "")
                {
                    UP_Set_IPBLWONSAN();
                }
            }
        }

        private void TXT01_IBCONTNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUSGB005S popup = new TYUSGB005S(Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()),
                                                  this.CBH01_IBHWAJU.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_IBCONTNO.SetValue(popup.fsCONTNO);

                    this.SetFocus(this.CBH01_IBBPSAGO.CodeText);
                }
            }
        }

        private void CBH01_YNYSHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsWK_GUBUN5 == "NEW")
                {
                    //if (CBH01_YNBKJNHJ.GetValue().ToString() == "" && CBH01_YNBKHUHJ.GetValue().ToString() == "")
                    //{
                        CBH01_YNBKJNHJ.SetValue(CBH01_YNHWAJU.GetValue().ToString());
                        CBH01_YNBKHUHJ.SetValue(CBH01_YNYSHWAJU.GetValue().ToString());
                    //}
                }
            }
        }

        #endregion

        #region Description : 포커스 이동
        private void UP_Set_KeyFocus()
        {
            if (fsTAB_GUBUN == "USIIPHAF")      // 입항관리
            {
                SetFocus(CBH01_IHHANGCHA.CodeText);
            }
            else if (fsTAB_GUBUN == "USIIPGOF") // 입고관리
            {
                if (CBH01_IPHANGCHA.GetValue().ToString() == "")
                {
                    SetFocus(CBH01_IPHANGCHA.CodeText);
                }
                else
                {
                    SetFocus(CBH01_IPGOKJONG.CodeText);
                }
            }
            else if (fsTAB_GUBUN == "USIIPBLF") // B/L별 입고관리
            {
                if (CBH01_IBHANGCHA.GetValue().ToString() == "")
                {
                    SetFocus(CBH01_IBHANGCHA.CodeText);
                }
                else if (CBH01_IBGOKJONG.GetValue().ToString() == "")
                {
                    SetFocus(CBH01_IBGOKJONG.CodeText);
                }
                else //if (CBH01_IBHWAJU.GetValue().ToString() == "")
                {
                    SetFocus(CBH01_IBHWAJU.CodeText);
                }
                //else if (TXT01_IBBLNO.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_IBBLNO);
                //}
                //else if (TXT01_IBBLMSN.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_IBBLMSN);
                //}
                //else if (TXT01_IBBLHSN.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_IBBLHSN);
                //}
                //else //if (Get_Date(MTB01_IBDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
                //{
                //    SetFocus(MTB01_IBDATE);
                //}
            }
            else if (fsTAB_GUBUN == "USICUSTF") // 통관관리
            {
                if (CBH01_CSHANGCHA.GetValue().ToString() == "")
                {
                    SetFocus(CBH01_CSHANGCHA.CodeText);
                }
                else //if (CBH01_CSGOKJONG.GetValue().ToString() == "")
                {
                    SetFocus(CBH01_CSGOKJONG.CodeText);
                }
                //else if (CBH01_CSHWAJU.GetValue().ToString() == "")
                //{
                //    SetFocus(CBH01_CSHWAJU.CodeText);
                //}
                //else if (TXT01_CSBLNO.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_CSBLNO);
                //}
                //else if (TXT01_CSBLMSN.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_CSBLMSN);
                //}
                //else if (TXT01_CSBLHSN.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_CSBLHSN);
                //}
                //else //if (Get_Date(MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
                //{
                //    SetFocus(MTB01_CSDATE);
                //}
            }
            else if (fsTAB_GUBUN == "USIYANGNF")// 양수도관리
            {
                SetFocus(CBH01_YNHANGCHA.CodeText);

                //if (CBH01_YNHANGCHA.GetValue().ToString() == "")
                //{
                //    SetFocus(CBH01_YNHANGCHA.CodeText);
                //}
                //else if (CBH01_YNGOKJONG.GetValue().ToString() == "")
                //{
                //    SetFocus(CBH01_YNGOKJONG.CodeText);
                //}
                //else if (CBH01_YNYSHWAJU.GetValue().ToString() == "")
                //{
                //    SetFocus(CBH01_YNYSHWAJU.CodeText);
                //}
                //else if (Get_Date(MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim()) == "")
                //{
                //    SetFocus(MTB01_YNYSDATE);
                //}
                //else if (TXT01_YNYSSEQ.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_YNYSSEQ);
                //}
                //else //if (TXT01_YNYDSEQ.GetValue().ToString() == "")
                //{
                //    SetFocus(TXT01_YNYDSEQ);
                //}
            }
        }

        private void CBH01_GHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {   
                SetFocus(this.BTN61_INQ);
            }
        }

        private void CBH01_IHTAXCODE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN62_SAV.Visible == true)
                {
                    SetFocus(this.BTN62_SAV);
                }
            }
        }

        private void TXT01_IPTOTQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN63_SAV.Visible == true)
                {
                    SetFocus(this.BTN63_SAV);
                }
            }
        }

        private void MTB01_IPIPSTDAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.FPS91_TY_S_US_945HS264);
            }
        }

        private void TXT01_IBHWIPQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN64_SAV.Visible == true)
                {
                    SetFocus(this.BTN64_SAV);
                }
            }
        }

        private void TXT01_IBHWCHQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN64_SAV.Visible == true)
                {
                    SetFocus(this.BTN64_SAV);
                }
            }
        }

        private void TXT01_CSYJNUMB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN65_SAV.Visible == true)
                {
                    SetFocus(this.BTN65_SAV);
                }
            }
        }

        private void CBH01_CSGOKJONG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN65_SILOCODEHELP05.Visible == true)
                {
                    SetFocus(this.BTN65_SILOCODEHELP05);
                }
            }
        }

        private void CBH01_YNHANGCHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN66_SILOCODEHELP06.Visible == true)
                {
                    SetFocus(this.BTN66_SILOCODEHELP06);
                }
            }
        }

        private void TXT01_YNYSYDQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN66_SAV);
            }
        }

        #endregion

        #region Description : Set_Cookie(공통)
        private void Set_Cookie()
        {
            if (fsTAB_GUBUN == "USIIPHAF")      // 입항관리
            {
                fsHANGCHA = this.CBH01_IHHANGCHA.GetValue().ToString();
            }
            else if (fsTAB_GUBUN == "USIIPGOF") // 입고관리
            {
                fsHANGCHA = this.CBH01_IPHANGCHA.GetValue().ToString();
                fsGOKJONG = this.CBH01_IPGOKJONG.GetValue().ToString();
            }
            else if (fsTAB_GUBUN == "USIIPBLF") // B/L별 입고관리
            {
                fsHANGCHA = this.CBH01_IBHANGCHA.GetValue().ToString();
                fsGOKJONG = this.CBH01_IBGOKJONG.GetValue().ToString();
                fsHWAJU = this.CBH01_IBHWAJU.GetValue().ToString();
                fsBLNO = this.TXT01_IBBLNO.GetValue().ToString();
                fsBLMSN = this.TXT01_IBBLMSN.GetValue().ToString().Trim();
                fsBLHSN = this.TXT01_IBBLHSN.GetValue().ToString().Trim();
                fsIPSEQ = this.TXT01_IBBLSEQ.GetValue().ToString().Trim();
                fsIPDATE = Get_Date(this.MTB01_IBDATE.GetValue().ToString().Replace(" ","").Trim());
                fsHMNO1 = this.TXT01_IBHMNO1.GetValue().ToString();
                fsHMNO2 = this.TXT01_IBHMNO2.GetValue().ToString();
            }
            else if (fsTAB_GUBUN == "USICUSTF") // 통관관리
            {
                fsHANGCHA = this.CBH01_CSHANGCHA.GetValue().ToString();
                fsGOKJONG = this.CBH01_CSGOKJONG.GetValue().ToString();
                fsHWAJU = this.CBH01_CSHWAJU.GetValue().ToString();
                fsBLNO = this.TXT01_CSBLNO.GetValue().ToString();
                fsBLMSN = this.TXT01_CSBLMSN.GetValue().ToString().Trim();
                fsBLHSN = this.TXT01_CSBLHSN.GetValue().ToString().Trim();
                fsCSDTAE = Get_Date(this.MTB01_CSDATE.GetValue().ToString().Replace(" ", "").Trim());
                fsCSSEQ = this.TXT01_CSSEQ.GetValue().ToString().Trim();
                fsHMNO1 = this.TXT01_CSHMNO1.GetValue().ToString();
                fsHMNO2 = this.TXT01_CSHMNO2.GetValue().ToString();
            }
            else if (fsTAB_GUBUN == "USIYANGNF")// 양수도관리
            {
                fsHANGCHA = this.CBH01_YNHANGCHA.GetValue().ToString();
                fsGOKJONG = this.CBH01_YNGOKJONG.GetValue().ToString();
                fsHWAJU = this.CBH01_YNHWAJU.GetValue().ToString();
                fsBLNO = this.TXT01_YNBLNO.GetValue().ToString();
                fsBLMSN = this.TXT01_YNBLMSN.GetValue().ToString();
                fsBLHSN = this.TXT01_YNBLHSN.GetValue().ToString();
                fsCSDTAE = Get_Date(DTP01_YNCUSTIL.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "");
                fsCSSEQ = this.TXT01_YNCUSTCH.GetValue().ToString();
                fsYSHWAJU = this.CBH01_YNYSHWAJU.GetValue().ToString();
                fsYSDATE = Get_Date(this.MTB01_YNYSDATE.GetValue().ToString().Replace(" ", "").Trim());
                fsYSSEQ = this.TXT01_YNYSSEQ.GetValue().ToString();
                fsYDSEQ = this.TXT01_YNYDSEQ.GetValue().ToString();
            }
        }
        #endregion

        #region Description : Get_Cookie(공통)
        private void Get_Cookie()
        {
            if (fsTAB_GUBUN == "USIIPHAF")      // 입항관리
            {
                this.CBH01_IHHANGCHA.SetValue(fsHANGCHA);
            }
            else if (fsTAB_GUBUN == "USIIPGOF") // 입고관리
            {
                this.CBH01_IPHANGCHA.SetValue(fsHANGCHA);
                this.CBH01_IPGOKJONG.SetValue(fsGOKJONG);
            }
            else if (fsTAB_GUBUN == "USIIPBLF") // B/L별 입고관리
            {
                this.CBH01_IBHANGCHA.SetValue(fsHANGCHA);
                this.CBH01_IBGOKJONG.SetValue(fsGOKJONG);
                this.CBH01_IBHWAJU.SetValue(fsHWAJU);
                this.TXT01_IBBLNO.SetValue(fsBLNO);
                this.TXT01_IBBLMSN.SetValue(fsBLMSN);
                this.TXT01_IBBLHSN.SetValue(fsBLHSN);
                this.TXT01_IBBLSEQ.SetValue(fsIPSEQ);       //입고차수
                this.MTB01_IBDATE.SetValue(fsIPDATE);       //입고일자
                this.TXT01_IBHMNO1.SetValue(fsHMNO1);       //화물번호1
                this.TXT01_IBHMNO2.SetValue(fsHMNO2);       //화물번호2
            }
            else if (fsTAB_GUBUN == "USICUSTF") // 통관관리
            {
                this.CBH01_CSHANGCHA.SetValue(fsHANGCHA);
                this.CBH01_CSGOKJONG.SetValue(fsGOKJONG);
                this.CBH01_CSHWAJU.SetValue(fsHWAJU);
                this.TXT01_CSBLNO.SetValue(fsBLNO);
                this.TXT01_CSBLMSN.SetValue(fsBLMSN);
                this.TXT01_CSBLHSN.SetValue(fsBLHSN);
                this.MTB01_CSDATE.SetValue(fsCSDTAE);       //통관일자
                this.TXT01_CSSEQ.SetValue(fsCSSEQ);         //통관차수
                this.TXT01_CSHMNO1.SetValue(fsHMNO1);
                this.TXT01_CSHMNO2.SetValue(fsHMNO2);
            }
            else if (fsTAB_GUBUN == "USIYANGNF")// 양수도관리
            {
                this.CBH01_YNHANGCHA.SetValue(fsHANGCHA);
                this.CBH01_YNGOKJONG.SetValue(fsGOKJONG);
                this.CBH01_YNHWAJU.SetValue(fsHWAJU);
                this.TXT01_YNBLNO.SetValue(fsBLNO);
                this.TXT01_YNBLMSN.SetValue(fsBLMSN);
                this.TXT01_YNBLHSN.SetValue(fsBLHSN);
                this.DTP01_YNCUSTIL.SetValue(fsCSDTAE);
                this.TXT01_YNCUSTCH.SetValue(fsCSSEQ);
                this.CBH01_YNYSHWAJU.SetValue(fsYSHWAJU);
                this.MTB01_YNYSDATE.SetValue(fsYSDATE);
                this.TXT01_YNYSSEQ.SetValue(fsYSSEQ);
                this.TXT01_YNYDSEQ.SetValue(fsYDSEQ);
            }
        }
        #endregion

        
    }
}
