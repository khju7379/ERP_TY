using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.MR00
{
    /// <summary>
    /// 거래처관리 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// TY_P_MR_2BGAF400	구매 - 계정과목 체크
    /// TY_P_MR_2BM5E581	구매 - 고정자산 체크
    /// TY_P_MR_2BGAI403	구매 - 기타본예산 체크
    /// TY_P_MR_2BM5N583	구매 - 비품 체크
    /// TY_P_MR_2BGAH402	구매 - 소모성 예산 체크
    /// TY_P_MR_2BG1A409	구매 - 입고(고정자산생성번호) 체크
    /// TY_P_MR_2BM4T579	구매 - 자산분류코드 체크
    /// TY_P_MR_2BGAG401	구매 - 투자수선 체크
    /// TY_P_MR_2BGAJ404	구매 - 품목코드 체크
    /// TY_P_MR_2BLB0526	구매발주 - 구매요청 체크
    /// TY_P_MR_2BR9E658	구매발주 - 내역파일 존재유무 체크
    /// TY_P_MR_2BR9F659	구매발주 - 예산파일 존재유무 체크
    /// TY_P_MR_2BNBE617	구매발주 내역사항 - 예산 업데이트(PLUS)
    /// TY_P_MR_2BSB3711	구매입고 - 같은 예산에 등록된 품목 건수 체크
    /// TY_P_MR_2BR9A657	구매입고 - 구매발주 체크
    /// TY_P_MR_2BS3V730	구매입고 - 발주수량 및 발주금액
    /// TY_P_MR_2BS43733	구매입고 - 수량(입고수량 - 불량수량) 및 금액(입고금액 - 불량금액)
    /// TY_P_MR_2BSAU708	구매입고 귀속별 예산파일 삭제 - 팝업
    /// TY_P_MR_2BSAY709	구매입고 귀속별 예산파일 수정 - 팝업
    /// TY_P_MR_2BS9K703	구매입고 내역 수정 및 삭제시 - 구매발주 및 요청 업데이트
    /// TY_P_MR_2BS4P736	구매입고 내역사항 - 예산 업데이트 플러스
    /// TY_P_MR_2BR8Y697	구매입고 내역사항 - 예산 업데이트(MINUS)
    /// TY_P_MR_2BR4B678	구매입고 내역사항 조회 - 팝업
    /// TY_P_MR_2BR8P696	구매입고 내역사항 확인 - 팝업
    /// TY_P_MR_2BR4M682	구매입고 마감체크
    /// TY_P_MR_2BR9G663	구매입고 마스터 - 순번 가져오기
    /// TY_P_MR_2BR9Z664	구매입고 마스터 등록 - 팝업
    /// TY_P_MR_2BRBN670	구매입고 마스터 등록시 - 구매발주 및 요청 업데이트
    /// TY_P_MR_2BRAU668	구매입고 마스터 등록시 - 구매입고 내역 등록
    /// TY_P_MR_2BRA7665	구매입고 마스터 등록시 - 구매입고 예산 등록
    /// TY_P_MR_2BR2G672	구매입고 마스터 등록시 - 예산 업데이트
    /// TY_P_MR_2BR88693	구매입고 마스터 삭제 - 구매발주 마스터 입고번호 클리어
    /// TY_P_MR_2BR85692	구매입고 마스터 삭제 - 팝업
    /// TY_P_MR_2BR83691	구매입고 마스터 수정 - 팝업
    /// TY_P_MR_2BR3M673	구매입고 마스터 확인
    /// TY_P_MR_2BR3X675	구매입고 마스터(예산조회-거래처별) - 팝업
    /// TY_P_MR_2BR3S674	구매입고 마스터(예산조회-귀속부서별) - 팝업
    /// TY_P_MR_2BSB0710	구매입고 수정 및 삭제 - 고정자산생성번호 체크
    /// TY_P_MR_2BQ6G640	구매입고 특기사항 등록 - 팝업
    /// TY_P_MR_2BQ6G641	구매입고 특기사항 삭제 - 팝업
    /// TY_P_MR_2BQ6G642	구매입고 특기사항 수정 - 팝업
    /// TY_P_MR_2BQ6H643	구매입고 특기사항 조회 - 팝업
    /// TY_P_MR_2BR7H690	구매입고 특기사항 체크
    /// TY_P_MR_2BEBB293	사번 - 부서명 가져오기
    /// TY_P_MR_2BT81761    구매입고 - 구매발주 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  구매요청 마스터
    ///  TY_S_MR_2BR4D679	구매입고 내역사항 조회 - 팝업
    ///  TY_S_MR_2BR40677	구매입고 마스터(예산조회-거래처별) - 팝업
    ///  TY_S_MR_2BR44676	구매입고 마스터(예산조회-귀속부서별) - 팝업
    ///  TY_S_MR_2BQ6E639	구매입고 특기사항 조회 - 팝업    
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871	저장하시겠습니까?
    ///  TY_M_GB_23NAD872	삭제하시겠습니까?
    ///  TY_M_GB_23NAD873	저장하였습니다.
    ///  TY_M_GB_23NAD874	삭제하였습니다.
    ///  TY_M_MR_2BC51262	결재 완료 된 문서가 아닙니다.
    ///  TY_M_MR_2BC59266	결재 완료 된 자료이므로 작업이 불가합니다.
    ///  TY_M_MR_2BD3Y285	수정하시겠습니까?
    ///  TY_M_MR_2BD3Z286	수정하였습니다.
    ///  TY_M_MR_2BE4Z311	특기 사항이 존재합니다.
    ///  TY_M_MR_2BE4Z312	내역 사항이 존재 합니다.
    ///  TY_M_MR_2BF4Z352	처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353	처리하시겠습니까?
    ///  TY_M_MR_2BF50354	처리하였습니다.
    ///  TY_M_MR_2BF8A365	합계 부분입니다. 다른 데이터를 선택하세요.
    ///  TY_M_MR_2BG15408	구매입고에 고정자산생성번호가 존재합니다.
    ///  TY_M_MR_2BGA1378	투자예산을 확인하세요.
    ///  TY_M_MR_2BGA1379	계정 코드를 입력하세요.
    ///  TY_M_MR_2BGA1380	예산 구분을 입력하세요.
    ///  TY_M_MR_2BGA1381	귀속 부서를 입력하세요.
    ///  TY_M_MR_2BGA2387	품목 코드를 확인하세요.
    ///  TY_M_MR_2BGA2388	품목 코드를 입력하세요.
    ///  TY_M_MR_2BGA2389	기타예산을 확인하세요.
    ///  TY_M_MR_2BGA2390	소모성 예산을 확인하세요.
    ///  TY_M_MR_2BGA5392	화폐를 입력하세요.
    ///  TY_M_MR_2BGA5393	부가세 구분을 입력하세요.
    ///  TY_M_MR_2BGA5395	거래처를 입력하세요.
    ///  TY_M_MR_2BGAA396	적용 환율을 입력하세요.
    ///  TY_M_MR_2BGAE399	계정 코드를 확인하세요.
    ///  TY_M_MR_2BGCZ407	자산 분류 코드를 입력하세요.
    ///  TY_M_MR_2BK3G501	년월은 6자리입니다.
    ///  TY_M_MR_2BK3H504	사업부를 확인하세요.
    ///  TY_M_MR_2BM51580	자산 분류 코드를 확인하세요.
    ///  TY_M_MR_2BM5J582	고정자산 번호를 확인하세요.
    ///  TY_M_MR_2BM5O584	비품 번호를 확인하세요.
    ///  TY_M_MR_2BM5T588	비품 구분을 확인하세요.
    ///  TY_M_MR_2BR8T645	발주 사업부를 입력하세요.
    ///  TY_M_MR_2BR8T646	발주 년월을 입력하세요.
    ///  TY_M_MR_2BR8T647	발주 순번을 입력하세요.
    ///  TY_M_MR_2BR8T648	발주 번호를 입력하세요.
    ///  TY_M_MR_2BR8X649	검수 사번을 입력하세요.
    ///  TY_M_MR_2BR8Y650	마감 일자를 입력하세요.
    ///  TY_M_MR_2BR8Y651	입고 일자를 입력하세요.
    ///  TY_M_MR_2BR90652	인수 사번을 입력하세요.
    ///  TY_M_MR_2BR90653	인수 일자를 입력하세요.
    ///  TY_M_MR_2BR92654	검수 일자를 입력하세요.
    ///  TY_M_MR_2BR93655	검수 부서를 입력하세요.
    ///  TY_M_MR_2BR93656	발주 번호를 확인하세요.
    ///  TY_M_MR_2BR9G660	구매발주 예산파일이 존재 하지 않습니다.
    ///  TY_M_MR_2BRAX669	수량은 '1' 입니다.
    ///  TY_M_MR_2BS3F718	불량 수량이 입고 수량보다 많습니다.
    ///  TY_M_MR_2BS3G719	불량 금액이 입고 금액보다 많습니다.
    ///  TY_M_MR_2BS3H720	불량 상태를 입력하세요.
    ///  TY_M_MR_2BS3H721	불량 수량을 입력하세요.
    ///  TY_M_MR_2BS3H722	불량 여부를 확인하세요.
    ///  TY_M_MR_2BSBN714	입고 금액이 발주 금액을 초과합니다.
    ///  TY_M_MR_2BSBN715	입고 수량이 발주 수량을 초과합니다.
    ///  TY_M_MR_2BSBN716	입고 단가를 입력하세요.
    ///  TY_M_MR_2BSBN717	입고 수량을 입력하세요.
    ///  TY_M_MR_2BTA9745	검수구분이 금액일 경우 불량수량을 입력할 수 없습니다.

    ///  
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  NEW_MRP_NF : 신규(내역)
    ///  NEW_MRP_TF : 신규(특기)
    /// </summary>
    public partial class TYMROP001I : TYBase
    {
        //private TYData DAT01_PRTHISAB;

        public string fsOPM1000;
        public string fsOPM1010;

        private string fsYESAN_COUNT = string.Empty;
        private string fsPOM5110     = string.Empty;

        private string fsPRM5100 = string.Empty;
        private string fsPOM1400 = string.Empty;
        private string fsPOM1410 = string.Empty;
        private string fsPOM1420 = string.Empty;

        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYMROP001I(string sOPM1000, string sOPM1010)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsOPM1000 = sOPM1000;
            this.fsOPM1010 = sOPM1010;

            this.TXT01_OPM1000.SetValue(fsOPM1000);
            this.TXT01_OPM1010.SetValue(fsOPM1010);

            this.TXT01_OPN1000.SetValue(fsOPM1000);
            this.TXT01_OPN1010.SetValue(fsOPM1010);
        }

        private void TYMROP001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_OPM11601.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_SAV.ProcessCheck  += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(BTN61_EDIT_ProcessCheck);
            this.BTN61_REM.ProcessCheck  += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            bool fResult;

            this.TXT01_OPM1161.SetReadOnly(true);
            this.TXT01_OPM1140.SetReadOnly(true);

            // 등록
            if (this.TXT01_OPM1000.GetValue().ToString() == ""  &&
                this.TXT01_OPM1010.GetValue().ToString() == ""
                )
            {
                this.TXT01_OPM1000.SetReadOnly(false);
                this.TXT01_OPM1010.SetReadOnly(true);

                fsGUBUN = "MRPOPMF";

                // 컨트롤 초기화
                UP_Control_Initialize("MRPOPMF", true);

                // 컨트롤 초기화
                UP_Control_Initialize("MRPOPNF", true);

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                // 탭 컨트롤
                tabControl1_Enable("");

                SetStartingFocus(this.TXT01_OPM1000);
            }
            else // 수정
            {
                this.TXT01_OPM1000.SetReadOnly(true);
                this.TXT01_OPM1010.SetReadOnly(true);

                this.CBH01_OPM1120.SetReadOnly(true);
                this.CBH01_OPM1150.SetReadOnly(true);
                this.TXT01_OPM1140.SetReadOnly(true);

                fsGUBUN = "MRPOPMF";

                // 컨트롤 초기화
                UP_Control_Initialize("MRPOPMF", true);

                // 컨트롤 초기화
                UP_Control_Initialize("MRPOPNF", true);

                // 마스터 DISPLAY
                UP_MRPOPMF_DISPLAY();

                // 내역사항 DISPLAY
                UP_MRPOPNF_DISPLAY("MASTER");

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    SetFocus(this.CBH01_OPM1020.CodeText);
                }
                else
                {
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("3", false);
                }
            }            
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            if (fsGUBUN == "MRPOPMF") // 마스터
            {
                string sOPM1160 = string.Empty;

                // 발주번호
                sOPM1160 = this.TXT01_OPM1160.GetValue().ToString() + this.TXT01_OPM1161.GetValue().ToString() + this.TXT01_OPM1162.GetValue().ToString() + Set_Fill4(this.TXT01_OPM1163.GetValue().ToString());

                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C55D955",
                    this.TXT01_OPM1000.GetValue().ToString(),
                    this.TXT01_OPM1010.GetValue().ToString(),
                    this.CBH01_OPM1020.GetValue().ToString(),
                    this.CBH01_OPM1030.GetValue().ToString(),
                    this.TXT01_OPM1040.GetValue().ToString(),
                    this.DTP01_OPM1050.GetValue().ToString(),
                    this.DTP01_OPM1060.GetValue().ToString(),
                    "",                                       // 개정일자
                    this.CBH01_OPM1120.GetValue().ToString(),
                    this.TXT01_OPM1130.GetValue().ToString(),
                    this.TXT01_OPM1140.GetValue().ToString(),
                    this.CBH01_OPM1150.GetValue().ToString(),
                    sOPM1160.ToString(),
                    this.CBO01_OPM1170.GetValue().ToString(),
                    this.TXT01_OPM1180.GetValue().ToString(),
                    "",                                       // 귀속부서
                    "",                                       // 예산구분
                    "",                                       // 적용계정
                    "",                                       // 비품코드
                    "",                                       // 순번
                    TYUserInfo.EmpNo.ToString()
                    );

                // 구매발주 내역 -> 장기계약 내역 등록
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C55M957",
                    this.TXT01_OPM1000.GetValue().ToString(), // 계약년도
                    this.TXT01_OPM1010.GetValue().ToString(), // 계약순번
                    this.DTP01_OPM1050.GetValue().ToString(), // 계약일자
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_OPM1160.GetValue().ToString(),
                    this.TXT01_OPM1161.GetValue().ToString(),
                    this.TXT01_OPM1162.GetValue().ToString(),
                    this.TXT01_OPM1163.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQueryList();
                this.ShowMessage("TY_M_GB_23NAD873");

                // 탭 컨트롤
                tabControl1_Enable("MRPOPMF");

                // 버튼 컨트롤
                UP_ImgbtnDisplay("1", true);
                
                this.TXT01_OPM1000.SetReadOnly(true);
                this.TXT01_OPM1010.SetReadOnly(true);

                UP_MRPOPMF_DISPLAY();

                SetFocus(this.CBH01_OPM1020.CodeText);
            }
            else if (fsGUBUN == "MRPOPNF") // 내역사항
            {
                // 내역사항 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BNB4612",
                    this.TXT01_OPN1000.GetValue().ToString(),
                    this.TXT01_OPN1010.GetValue().ToString(),
                    this.TXT01_OPN1020.GetValue().ToString(),
                    this.TXT01_OPN1030.GetValue().ToString(),
                    this.TXT01_OPN1040.GetValue().ToString(),
                    this.TXT01_OPN1050.GetValue().ToString(),
                    this.TXT01_OPN1060.GetValue().ToString(),
                    this.TXT01_OPN1070.GetValue().ToString(),
                    this.TXT01_OPN1080.GetValue().ToString(),
                    this.TXT01_OPN1090.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString()
                    );

                this.DbConnector.ExecuteNonQuery();
                this.ShowMessage("TY_M_GB_23NAD873");

                // 버튼 초기화
                UP_ImgbtnDisplay("3", false);

                // 컨트롤 초기화
                UP_Control_Initialize("MRPOPNF", false);

                this.TXT01_OPN1030.SetValue("");
                this.TXT01_OPN1030NM.SetValue("");
                this.TXT01_OPN1040.SetValue("");
                this.TXT01_OPN1040NM.SetValue("");
                this.TXT01_OPN1050.SetValue("");
                this.TXT01_OPN1050NM.SetValue("");
                this.TXT01_OPN1060.SetValue("");
                this.TXT01_OPN1060NM.SetValue("");
                this.TXT01_OPN1070.SetValue("");
                this.TXT01_OPN1080.SetValue("");
                this.TXT01_OPN1090.SetValue("");

                UP_FieldClear("TXT01_OPN1080");

                UP_MRPOPNF_DISPLAY("DETAIL");

                SetFocus(this.TXT01_OPN1090);
            }
        }
        #endregion

        #region Description : 수정 버튼
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "MRPOPMF") // 마스터
            {
                string sOPM1160 = string.Empty;

                // 발주번호
                sOPM1160 = this.TXT01_OPM1160.GetValue().ToString() + this.TXT01_OPM1161.GetValue().ToString() + this.TXT01_OPM1162.GetValue().ToString() + Set_Fill4(this.TXT01_OPM1163.GetValue().ToString());

                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C55E956",
                    this.CBH01_OPM1020.GetValue().ToString(),
                    this.CBH01_OPM1030.GetValue().ToString(),
                    this.TXT01_OPM1040.GetValue().ToString(),
                    this.DTP01_OPM1050.GetValue().ToString(),
                    this.DTP01_OPM1060.GetValue().ToString(),
                    "",                                       // 개정일자
                    this.CBH01_OPM1120.GetValue().ToString(),
                    this.TXT01_OPM1130.GetValue().ToString(),
                    this.TXT01_OPM1140.GetValue().ToString(),
                    this.CBH01_OPM1150.GetValue().ToString(),
                    sOPM1160.ToString(),
                    this.CBO01_OPM1170.GetValue().ToString(),
                    this.TXT01_OPM1180.GetValue().ToString(),
                    "",                                       // 귀속부서
                    "",                                       // 예산구분
                    "",                                       // 적용계정
                    "",                                       // 비품코드
                    "",                                       // 순번
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_OPM1000.GetValue().ToString(),
                    this.TXT01_OPM1010.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQuery();
                this.ShowMessage("TY_M_MR_2BD3Z286");

                // 탭 컨트롤
                tabControl1_Enable("MRPOPMF");

                UP_MRPOPMF_DISPLAY();

                SetFocus(this.CBH01_OPM1020.CodeText);
            }
            else if (fsGUBUN == "MRPOPNF") // 내역사항
            {
                // 내역사항 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C556952",
                    this.TXT01_OPN1090.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_OPN1000.GetValue().ToString(),
                    this.TXT01_OPN1010.GetValue().ToString(),
                    this.TXT01_OPN1020.GetValue().ToString(),
                    this.TXT01_OPN1030.GetValue().ToString(),
                    this.TXT01_OPN1040.GetValue().ToString(),
                    this.TXT01_OPN1050.GetValue().ToString(),
                    this.TXT01_OPN1060.GetValue().ToString(),
                    this.TXT01_OPN1070.GetValue().ToString(),
                    this.TXT01_OPN1080.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQuery();
                this.ShowMessage("TY_M_MR_2BD3Z286");

                // 버튼 초기화
                UP_ImgbtnDisplay("3", false);

                // 컨트롤 초기화
                UP_Control_Initialize("MRPOPNF", false);

                this.TXT01_OPN1030.SetValue("");
                this.TXT01_OPN1030NM.SetValue("");
                this.TXT01_OPN1040.SetValue("");
                this.TXT01_OPN1040NM.SetValue("");
                this.TXT01_OPN1050.SetValue("");
                this.TXT01_OPN1050NM.SetValue("");
                this.TXT01_OPN1060.SetValue("");
                this.TXT01_OPN1060NM.SetValue("");
                this.TXT01_OPN1070.SetValue("");
                this.TXT01_OPN1080.SetValue("");
                this.TXT01_OPN1090.SetValue("");

                UP_FieldClear("TXT01_OPN1080");

                UP_MRPOPNF_DISPLAY("DETAIL");

                SetFocus(this.TXT01_OPN1090);
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            if (fsGUBUN == "MRPOPMF") // 마스터
            {
                // 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C55B954",
                    this.TXT01_OPM1000.GetValue().ToString(),
                    this.TXT01_OPM1010.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQueryList();
                this.ShowMessage("TY_M_GB_23NAD874");

                // 계약번호
                this.TXT01_OPM1000.SetReadOnly(false);
                this.TXT01_OPM1010.SetReadOnly(true);

                // 발주번호
                this.TXT01_OPM1160.SetReadOnly(false);
                this.TXT01_OPM1162.SetReadOnly(false);
                this.TXT01_OPM1163.SetReadOnly(false);

                this.BTN61_OPM11601.Enabled = true;
                //this.BTN61_OPM11601.SetReadOnly(false);

                this.TXT01_OPM1000.SetValue("");
                this.TXT01_OPM1010.SetValue("");

                this.TXT01_OPM1160.SetValue("");
                this.TXT01_OPM1162.SetValue("");
                this.TXT01_OPM1163.SetValue("");

                // 탭 컨트롤
                tabControl1_Enable("");

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                UP_FieldClear("MRPOPMF");

                SetFocus(this.TXT01_OPM1000);
            }
            else if (fsGUBUN == "MRPOPNF") // 내역사항
            {
                // 내역사항 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C557953",
                    this.TXT01_OPN1000.GetValue().ToString(),
                    this.TXT01_OPN1010.GetValue().ToString(),
                    this.TXT01_OPN1020.GetValue().ToString(),
                    this.TXT01_OPN1030.GetValue().ToString(),
                    this.TXT01_OPN1040.GetValue().ToString(),
                    this.TXT01_OPN1050.GetValue().ToString(),
                    this.TXT01_OPN1060.GetValue().ToString(),
                    this.TXT01_OPN1070.GetValue().ToString(),
                    this.TXT01_OPN1080.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQuery();
                this.ShowMessage("TY_M_GB_23NAD874");

                // 버튼 초기화
                UP_ImgbtnDisplay("3", false);

                // 컨트롤 초기화
                UP_Control_Initialize("MRPOPNF", false);

                this.TXT01_OPN1030.SetValue("");
                this.TXT01_OPN1030NM.SetValue("");
                this.TXT01_OPN1040.SetValue("");
                this.TXT01_OPN1040NM.SetValue("");
                this.TXT01_OPN1050.SetValue("");
                this.TXT01_OPN1050NM.SetValue("");
                this.TXT01_OPN1060.SetValue("");
                this.TXT01_OPN1060NM.SetValue("");
                this.TXT01_OPN1070.SetValue("");
                this.TXT01_OPN1080.SetValue("");
                this.TXT01_OPN1080NM.SetValue("");
                this.TXT01_OPN1090.SetValue("");

                UP_FieldClear("TXT01_OPN1080");

                UP_MRPOPNF_DISPLAY("DETAIL");

                SetFocus(this.TXT01_OPN1090);
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsOPM1000 = this.TXT01_OPM1000.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 마스터 관련

        #region Description : 마스터 DISPLAY
        private void UP_MRPOPMF_DISPLAY()
        {
            UP_FieldClear("MRPOPMF");

            fsPOM5110 = "";

            DataTable dt = new DataTable();

            #region Description : 구매입고 마스터 내용 DISPLAY

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2C59W918",
                this.TXT01_OPM1000.GetValue(),
                Set_Fill4(this.TXT01_OPM1010.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_OPM1160.SetReadOnly(true);
                this.TXT01_OPM1161.SetReadOnly(true);
                this.TXT01_OPM1162.SetReadOnly(true);
                this.TXT01_OPM1163.SetReadOnly(true);

                this.BTN61_OPM11601.Enabled = false;
                //this.BTN61_OPM11601.SetReadOnly(true);

                // 버튼 컨트롤
                UP_ImgbtnDisplay("1", true);

                // 탭 컨트롤
                tabControl1_Enable("MRPOPMF");

                this.CurrentDataTableRowMapping(dt, "01");

                // 마스터 내역사항 보여주기
                UP_MRPOPNF_DISPLAY("MASTER");
            }
            else
            {
                this.TXT01_OPM1160.SetReadOnly(false);
                this.TXT01_OPM1161.SetReadOnly(false);
                this.TXT01_OPM1162.SetReadOnly(false);
                this.TXT01_OPM1163.SetReadOnly(false);

                this.BTN61_OPM11601.Enabled = true;
                //this.BTN61_OPM11601.SetReadOnly(false);
            }

            #endregion
        }
        #endregion

        #region Description : 발주번호 코드헬프
        private void TXT01_OPM1160_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_OPM1160.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_OPM11601_Click(null, null);
                }
            }
        }

        private void TXT01_OPM1162_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_OPM1162.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_OPM11601_Click(null, null);
                }
            }
        }

        private void TXT01_OPM1163_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_OPM1163.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_OPM11601_Click(null, null);
                }
            }
        }

        private void BTN61_OPM11601_Click(object sender, EventArgs e)
        {
            if (this.TXT01_OPM1000.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2C54V950");
                this.TXT01_OPM1000.Focus();
                return;
            }

            // 계약년도
            if (this.TXT01_OPM1000.GetValue().ToString().Length != 4)
            {
                this.ShowMessage("TY_M_MR_2C563958");
                this.TXT01_OPM1000.Focus();
                return;
            }

            // 장기계약 - 구매발주 코드헬프
            TYMZOP01C1 popup = new TYMZOP01C1(this.TXT01_OPM1000.GetValue().ToString() + "01", this.TXT01_OPM1000.GetValue().ToString() + "12");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_OPM1160.SetValue(popup.fsPOM1000); // 발주사업부
                this.TXT01_OPM1161.SetValue(popup.fsPOM1010); // 발주구분
                this.TXT01_OPM1162.SetValue(popup.fsPOM1020); // 년월
                this.TXT01_OPM1163.SetValue(popup.fsPOM1030); // 순서
                this.CBH01_OPM1020.SetValue(popup.fsPON1100); // 계약업체
                this.CBH01_OPM1030.SetValue(popup.fsOPM1030); // 계약종류
                this.TXT01_OPM1040.SetValue(popup.fsPOM1180); // 계약내용
                this.DTP01_OPM1050.SetValue(popup.fsPOM1100); // 계약일자
                this.CBH01_OPM1120.SetValue(popup.fsPON1730); // 대금지불
                this.CBH01_OPM1150.SetValue(popup.fsPON1110); // 부가세구분
                this.TXT01_OPM1140.SetValue(popup.fsPRM1000); // 계약사업부

                this.CBH01_OPM1120.SetReadOnly(true);
                this.CBH01_OPM1150.SetReadOnly(true);
                this.TXT01_OPM1140.SetReadOnly(true);

                SetFocus(CBH01_OPM1020.CodeText);
            }
        }
        #endregion

        #endregion

        #region Description : 내역사항 관련

        #region Description : 내역사항 DISPLAY
        private void UP_MRPOPNF_DISPLAY(string sGUBUN)
        {
            string sOPM1000 = string.Empty;
            string sOPM1010 = string.Empty;

            DataTable dt = new DataTable();

            if (sGUBUN.ToString() == "MASTER")
            {
                sOPM1000 = this.TXT01_OPM1000.GetValue().ToString();
                sOPM1010 = this.TXT01_OPM1010.GetValue().ToString();
            }
            else
            {
                sOPM1000 = this.TXT01_OPN1000.GetValue().ToString();
                sOPM1010 = this.TXT01_OPN1010.GetValue().ToString();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2C59D915",
                sOPM1000.ToString(),
                Set_Fill4(sOPM1010.ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (sGUBUN.ToString() == "MASTER")
                {
                    this.FPS91_TY_S_MR_2C59E916.SetValue(dt);
                }
                else
                {
                    this.FPS91_TY_S_MR_2C59N917.SetValue(dt);
                }
            }
            else
            {
                if (sGUBUN.ToString() == "MASTER")
                {
                    this.FPS91_TY_S_MR_2C59E916.SetValue(dt);
                }
                else
                {
                    this.FPS91_TY_S_MR_2C59N917.SetValue(dt);
                }
            }
        }
        #endregion

        #region Description : 내역사항 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2C59N917_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            bool fResult;

            this.TXT01_OPN1030.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("OPN1030").ToString());
            this.TXT01_OPN1030NM.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("DTDESC").ToString());
            this.TXT01_OPN1040.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("OPN1040").ToString());
            this.TXT01_OPN1040NM.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("YSDESC").ToString());
            this.TXT01_OPN1050.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("OPN1050").ToString());
            this.TXT01_OPN1050NM.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("A1NMAC").ToString());
            this.TXT01_OPN1060.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("OPN1060").ToString());
            this.TXT01_OPN1060NM.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("BPDESC").ToString());
            this.TXT01_OPN1070.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("OPN1070").ToString());

            this.TXT01_OPN1080.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("OPN1080").ToString());
            this.TXT01_OPN1080NM.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("Z105013").ToString());
            this.TXT01_OPN1090.SetValue(this.FPS91_TY_S_MR_2C59N917.GetValue("OPN1090").ToString());

            e.Cancel = true;

            // 마감체크
            fResult = UP_MAGAM_CHECK();

            if (fResult == true)
            {
                // 버튼 컨트롤
                // 마스터 데이터가 존재하므로 
                // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                UP_ImgbtnDisplay("1", true);

                this.SetFocus(TXT01_OPN1090);
            }
            else
            {
                // 버튼 컨트롤
                UP_ImgbtnDisplay("3", false);
            }
        }
        #endregion

        #region Description : 발주번호 품목 코드헬프
        private void TXT01_OPN1080_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == System.Windows.Forms.Keys.F1)
            //{
            //    this.BTN61_OPN10801_Click(null, null);
            //}
        }

        private void BTN61_OPN10801_Click(object sender, EventArgs e)
        {
            // 장기계약 - 구매발주 품목 코드헬프
            TYMZOP03C1 popup = new TYMZOP03C1(this.TXT01_PON1000.GetValue().ToString(), this.TXT01_PON1010.GetValue().ToString(),
                                              this.TXT01_PON1020.GetValue().ToString(), this.TXT01_PON1030.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_OPN1030.SetValue(popup.fsArgOPN1030);     /* 귀속부서   */
                this.TXT01_OPN1030NM.SetValue(popup.fsArgOPN1030NM); /* 귀속부서명 */
                this.TXT01_OPN1040.SetValue(popup.fsArgOPN1040);     /* 예산구분   */
                this.TXT01_OPN1040NM.SetValue(popup.fsArgOPN1040NM); /* 예산구분명 */
                this.TXT01_OPN1050.SetValue(popup.fsArgOPN1050);     /* 적용계정   */
                this.TXT01_OPN1050NM.SetValue(popup.fsArgOPN1050NM); /* 적용계정명 */
                this.TXT01_OPN1060.SetValue(popup.fsArgOPN1060);     /* 비품코드   */
                this.TXT01_OPN1060NM.SetValue(popup.fsArgOPN1060NM); /* 비품코드명 */
                this.TXT01_OPN1070.SetValue(popup.fsArgOPN1070);     /* 순번       */
                this.TXT01_OPN1080.SetValue(popup.fsArgOPN1080);     /* 품목코드   */
                this.TXT01_OPN1080NM.SetValue(popup.fsArgOPN1080NM); /* 품목명     */
                this.TXT01_OPN1090.SetValue(popup.fsArgOPN1090);     /* 발주단가   */

                SetFocus(CBH01_OPM1020.CodeText);
            }
        }
        #endregion

        #endregion

        #region Description : 장기계약 공통

        #region Description : FieldClear()
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN.ToString() == "MRPOPMF")
            {
                this.TXT01_OPM1160.SetValue("");
                this.TXT01_OPM1162.SetValue("");
                this.TXT01_OPM1163.SetValue("");
                this.CBH01_OPM1020.SetValue("");
                this.CBH01_OPM1030.SetValue("");
                this.TXT01_OPM1040.SetValue("");
                //this.CBO01_OPM1100.SetValue("N");
                //this.TXT01_OPM1080.SetValue("");
                //this.TXT01_OPM1110.SetValue("");
                this.CBH01_OPM1120.SetValue("");
                this.CBH01_OPM1150.SetValue("");
                this.TXT01_OPM1130.SetValue("");
                this.TXT01_OPM1140.SetValue("");
                this.CBO01_OPM1170.SetValue("N");
                this.TXT01_OPM1180.SetValue("");

                this.DTP01_OPM1050.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.DTP01_OPM1060.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            }
            else if (sGUBUN.ToString() == "MRPOPNF")
            {
                this.TXT01_OPN1030.SetValue("");
                this.TXT01_OPN1030NM.SetValue("");
                this.TXT01_OPN1040.SetValue("");
                this.TXT01_OPN1040NM.SetValue("");
                this.TXT01_OPN1050.SetValue("");
                this.TXT01_OPN1050NM.SetValue("");
                this.TXT01_OPN1060.SetValue("");
                this.TXT01_OPN1060NM.SetValue("");
                this.TXT01_OPN1070.SetValue("");
                this.TXT01_OPN1080.SetValue("");
                this.TXT01_OPN1080NM.SetValue("");
                this.TXT01_OPN1090.SetValue("");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPOPMF") // 마스터
            {
                if (this.TXT01_OPM1000.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C54V950");
                    this.TXT01_OPM1000.Focus();
                    return;
                }

                // 계약년도
                if (this.TXT01_OPM1000.GetValue().ToString().Length != 4)
                {
                    this.ShowMessage("TY_M_MR_2C563958");
                    this.TXT01_OPM1000.Focus();
                    return;
                }

                if (TXT01_OPM1160.GetValue().ToString() == "" || TXT01_OPM1161.GetValue().ToString() == "" ||
                    TXT01_OPM1162.GetValue().ToString() == "" || TXT01_OPM1163.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C54B943");

                    SetFocus(this.CBH01_OPM1020.CodeText);

                    e.Successed = false;
                    return;
                }

                // 계약 업체
                if (this.CBH01_OPM1020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5395");

                    SetFocus(this.CBH01_OPM1020.CodeText);

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 발주 거래처 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2C54F945",
                        this.TXT01_OPM1160.GetValue().ToString(),
                        this.TXT01_OPM1161.GetValue().ToString(),
                        this.TXT01_OPM1162.GetValue().ToString(),
                        this.TXT01_OPM1163.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (this.CBH01_OPM1020.GetValue().ToString() != dt.Rows[0]["PON1100"].ToString())
                        {
                            this.ShowMessage("TY_M_MR_2C54B944");

                            SetFocus(this.CBH01_OPM1020.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2C54B943");

                        this.TXT01_OPM1160.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 계약 종류
                if (this.CBH01_OPM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C549936");

                    this.CBH01_OPM1030.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약 내용
                if (this.TXT01_OPM1040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C549936");

                    this.TXT01_OPM1040.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약년도와 계약일자 년도는 동일해야 함.
                // 20121206일 차장님.
                // 2013년도에 입력한 예산과 2014년도에 입력한 예산이 다를 경우 때문.
                // 그래서 계약년도와 계약일자의 년도, 유효일자의 년도가 동일해야 함.
                if (this.TXT01_OPM1000.GetValue().ToString() != this.DTP01_OPM1050.GetValue().ToString().Substring(0, 4))
                {
                    this.ShowMessage("TY_M_MR_2C653968");

                    this.DTP01_OPM1050.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약년도와 유효일자 년도는 동일해야 함.
                if (this.TXT01_OPM1000.GetValue().ToString() != this.DTP01_OPM1060.GetValue().ToString().Substring(0, 4))
                {
                    this.ShowMessage("TY_M_MR_2C653967");

                    this.DTP01_OPM1060.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약일자와 유효일자 비교
                if (int.Parse(this.DTP01_OPM1050.GetValue().ToString()) > int.Parse(this.DTP01_OPM1060.GetValue().ToString()))
                {
                    this.ShowMessage("TY_M_MR_2C541938");

                    this.DTP01_OPM1050.Focus();

                    e.Successed = false;
                    return;
                }

                //// 연장 월수
                //if (this.CBO01_OPM1100.GetValue().ToString() == "Y")
                //{
                //    if (Get_Numeric(TXT01_OPM1110.GetValue().ToString()) == "0")
                //    {
                //        this.ShowMessage("TY_M_MR_2C544939");

                //        this.TXT01_OPM1110.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                // 대금 지불
                if (this.CBH01_OPM1120.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C545940");

                    this.CBH01_OPM1120.Focus();

                    e.Successed = false;
                    return;
                }

                // 부가세 구분
                if (this.CBH01_OPM1150.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C545940");

                    this.CBH01_OPM1120.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약완료 체크
                if (this.CBO01_OPM1170.GetValue().ToString() == "Y")
                {
                    if (this.TXT01_OPM1180.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2C54Q948");

                        this.TXT01_OPM1180.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (this.TXT01_OPM1180.GetValue().ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_2C54Q949");

                        this.CBO01_OPM1170.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C54J946",
                    TXT01_OPM1000.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_OPM1010.SetValue(dt.Rows[0]["OPM1010"].ToString());
                }

            }
            else if (fsGUBUN == "MRPOPNF") // 내역사항
            {
                // 계약 단가
                if (double.Parse(Get_Numeric(this.TXT01_OPN1090.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2C54M947");

                    this.TXT01_OPN1090.Focus();

                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 수정 체크
        private void BTN61_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsYESAN_COUNT = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPOPMF") // 마스터
            {
                if (TXT01_OPM1160.GetValue().ToString() == "" || TXT01_OPM1161.GetValue().ToString() == "" ||
                    TXT01_OPM1162.GetValue().ToString() == "" || TXT01_OPM1163.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C54B943");

                    SetFocus(this.CBH01_OPM1020.CodeText);

                    e.Successed = false;
                    return;
                }

                // 계약 업체
                if (this.CBH01_OPM1020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5395");

                    SetFocus(this.CBH01_OPM1020.CodeText);

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 발주 거래처 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2C54F945",
                        this.TXT01_OPM1160.GetValue().ToString(),
                        this.TXT01_OPM1161.GetValue().ToString(),
                        this.TXT01_OPM1162.GetValue().ToString(),
                        this.TXT01_OPM1163.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (this.CBH01_OPM1020.GetValue().ToString() != dt.Rows[0]["PON1100"].ToString())
                        {
                            this.ShowMessage("TY_M_MR_2C54B944");

                            SetFocus(this.CBH01_OPM1020.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2C54B943");

                        this.TXT01_OPM1160.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 계약 종류
                if (this.CBH01_OPM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C549936");

                    this.CBH01_OPM1030.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약 내용
                if (this.TXT01_OPM1040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C549936");

                    this.TXT01_OPM1040.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약년도와 계약일자 년도는 동일해야 함.
                if (this.TXT01_OPM1000.GetValue().ToString() != this.DTP01_OPM1050.GetValue().ToString().Substring(0,4))
                {
                    this.ShowMessage("TY_M_MR_2C653968");

                    this.DTP01_OPM1050.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약년도와 유효일자 년도는 동일해야 함.
                if (this.TXT01_OPM1000.GetValue().ToString() != this.DTP01_OPM1060.GetValue().ToString().Substring(0, 4))
                {
                    this.ShowMessage("TY_M_MR_2C653967");

                    this.DTP01_OPM1060.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약일자와 유효일자 비교
                if (int.Parse(this.DTP01_OPM1050.GetValue().ToString()) > int.Parse(this.DTP01_OPM1060.GetValue().ToString()))
                {
                    this.ShowMessage("TY_M_MR_2C541938");

                    this.DTP01_OPM1050.Focus();

                    e.Successed = false;
                    return;
                }

                //// 연장 월수
                //if (this.CBO01_OPM1100.GetValue().ToString() == "Y")
                //{
                //    if (Get_Numeric(TXT01_OPM1110.GetValue().ToString()) == "0")
                //    {
                //        this.ShowMessage("TY_M_MR_2C544939");

                //        this.TXT01_OPM1110.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                // 대금 지불
                if (this.CBH01_OPM1120.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C545940");

                    this.CBH01_OPM1120.Focus();

                    e.Successed = false;
                    return;
                }

                // 부가세 구분
                if (this.CBH01_OPM1150.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2C545940");

                    this.CBH01_OPM1120.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "MRPOPNF") // 내역사항
            {
                // 계약 단가
                if (double.Parse(Get_Numeric(this.TXT01_OPN1090.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2C54M947");

                    this.TXT01_OPN1090.Focus();

                    e.Successed = false;
                    return;
                }
            }

            /********************************************************************
             * 구매요청에서 계약번호가 등록된 데이터가 존재하면 수정 및 삭제 불가   *
             ********************************************************************/

            // 구매요청 자료 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2C542933",
                this.TXT01_OPM1000.GetValue().ToString(),
                Set_Fill4(this.TXT01_OPM1010.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2C546935");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            /********************************************************************
             * 구매요청에서 계약번호가 등록된 데이터가 존재하면 수정 및 삭제 불가   *
             ********************************************************************/

            if (fsGUBUN == "MRPOPMF") // 마스터
            {
                DataTable dt = new DataTable();

                // 내역사항 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C59D915",
                    this.TXT01_OPM1000.GetValue().ToString(),
                    Set_Fill4(this.TXT01_OPM1010.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z312");

                    SetFocus(this.CBH01_OPM1020.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            // 구매요청 자료 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2C542933",
                this.TXT01_OPM1000.GetValue().ToString(),
                Set_Fill4(this.TXT01_OPM1010.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2C545934");
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

        #region Description : 마감체크
        private bool UP_MAGAM_CHECK()
        {
            // 구매요청 자료 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2C542933",
                this.TXT01_OPM1000.GetValue().ToString(),
                Set_Fill4(this.TXT01_OPM1010.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.TXT01_MESSAGE.SetValue("요청 데이터에 계약번호가 존재하므로 작업이 불가합니다.");
                return false;
            }

            return true;
        }
        #endregion

        #region Description : 탭 컨트롤 이벤트
        private void tabControl1_Enable(string sGUBUN)
        {
            if (sGUBUN == "")
            {
                if (this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Remove(this.tabPage2);
            }
            else if (sGUBUN == "MRPOPMF")
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Add(this.tabPage2);
            }
        }
        #endregion

        #region Description : 탭 페이지 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool fResult;

            if (tabControl1.SelectedIndex == 0) // 마스터
            {
                fsGUBUN = "MRPOPMF";

                // 마스터 DISPLAY
                UP_MRPOPMF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 마스터 데이터가 존재하므로 
                    // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                    UP_ImgbtnDisplay("1", true);

                    SetStartingFocus(this.CBH01_OPM1120.CodeText);
                }
                else  // 마감완료면
                {
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("3", false);
                }
            }
            else if (tabControl1.SelectedIndex == 1) // 내역사항
            {
                // 계약번호
                this.TXT01_OPN1000.SetValue(this.TXT01_OPM1000.GetValue().ToString());
                this.TXT01_OPN1010.SetValue(this.TXT01_OPM1010.GetValue().ToString());

                // 계약일자
                this.TXT01_OPN1020.SetValue(this.DTP01_OPM1050.GetValue().ToString());

                // 발주번호
                this.TXT01_PON1000.SetValue(this.TXT01_OPM1160.GetValue().ToString());
                this.TXT01_PON1010.SetValue(this.TXT01_OPM1161.GetValue().ToString());
                this.TXT01_PON1020.SetValue(this.TXT01_OPM1162.GetValue().ToString());
                this.TXT01_PON1030.SetValue(this.TXT01_OPM1163.GetValue().ToString());

                fsGUBUN = "MRPOPNF";

                // 내역사항 DISPLAY
                UP_MRPOPNF_DISPLAY("DETAIL");

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 특기사항 데이터가 존재하는지 알 수 없으므로
                    // 특기사항 탭 로드시(저장, 수정, 삭제) 버튼 없앰.
                    UP_ImgbtnDisplay("3", false);
                }
                else
                {
                    UP_ImgbtnDisplay("3", false);
                }
                

                //UP_MRPOPNF_DISPLAY();
            }
        }
        #endregion

        #region Description : 컨트롤 초기화
        private void UP_Control_Initialize(string sGUBUN, bool bTrueFalse)
        {
            if (sGUBUN == "MRPOPMF") // 마스터
            {
                //this.TXT01_RRM1010.SetReadOnly(bTrueFalse);
                //this.TXT01_OPM1010.SetReadOnly(bTrueFalse);

                ////this.TXT01_POM1000.SetReadOnly(bTrueFalse);
                //this.TXT01_OPM1161.SetReadOnly(bTrueFalse);
                ////this.TXT01_POM1020.SetReadOnly(bTrueFalse);
                ////this.TXT01_POM1030.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM2120.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM2030.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM2040.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM2080.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM2090.SetReadOnly(bTrueFalse);

                //this.TXT01_PRM2070.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM5130.SetReadOnly(bTrueFalse);
                //this.TXT01_OPM1040.SetReadOnly(bTrueFalse);

                //this.TXT01_PRM3010.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM2110.SetReadOnly(bTrueFalse);
                //this.TXT01_PRM2100.SetReadOnly(bTrueFalse);
                //this.TXT01_RRM1440.SetReadOnly(bTrueFalse);
                //this.TXT01_KBHANGL1.SetReadOnly(bTrueFalse);
                //this.TXT01_OPM1130.SetReadOnly(bTrueFalse);
                //this.TXT01_OPM1080.SetReadOnly(bTrueFalse);
                //this.TXT01_OPM1040.SetReadOnly(bTrueFalse);
                //this.TXT01_OPM1180.SetReadOnly(bTrueFalse);

                //this.FPS91_TY_S_MR_2BR44676.Initialize();
                //this.FPS91_TY_S_MR_2C59E916.Initialize();
            }
            else if (sGUBUN == "MRPOPNF") // 내역사항
            {
                this.TXT01_OPN1000.SetReadOnly(true);
                this.TXT01_OPN1010.SetReadOnly(true);
                this.TXT01_OPN1020.SetReadOnly(true);

                this.TXT01_PON1000.SetReadOnly(true);
                this.TXT01_PON1010.SetReadOnly(true);
                this.TXT01_PON1020.SetReadOnly(true);
                this.TXT01_PON1030.SetReadOnly(true);

                this.TXT01_OPN1030.SetReadOnly(true);
                this.TXT01_OPN1030NM.SetReadOnly(true);
                this.TXT01_OPN1040.SetReadOnly(true);
                this.TXT01_OPN1040NM.SetReadOnly(true);
                this.TXT01_OPN1050.SetReadOnly(true);
                this.TXT01_OPN1050NM.SetReadOnly(true);
                this.TXT01_OPN1060.SetReadOnly(true);
                this.TXT01_OPN1060NM.SetReadOnly(true);
                this.TXT01_OPN1070.SetReadOnly(true);

                this.TXT01_OPN1080.SetReadOnly(true);
                this.TXT01_OPN1080NM.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 버튼 컨트롤
        private void UP_ImgbtnDisplay(string sGubn, bool bTrueFalse)
        {
            if (fsGUBUN == "MRPOPMF")
            {
                if (sGubn == "1") // 수정 및 삭제
                {
                    BTN61_SAV.Visible = false;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                }
                else if (sGubn == "2") // 등록
                {
                    BTN61_SAV.Visible = true;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                }
                else
                {
                    BTN61_SAV.Visible  = bTrueFalse;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible  = bTrueFalse;
                }
            }
            else if (fsGUBUN == "MRPOPNF")
            {
                if (sGubn == "1") // 수정 및 삭제
                {
                    //BTN61_SAV.Visible = false;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                }
                else if (sGubn == "2") // 등록
                {
                    //BTN61_SAV.Visible = true;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                }
                else
                {
                    //BTN61_SAV.Visible  = bTrueFalse;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible  = bTrueFalse;
                }
            }

            if (this.TXT01_MESSAGE.GetValue().ToString() != "")
            {
                this.BTN61_SAV.Visible  = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible  = false;
            }
        }
        #endregion

        #region Description : 폼 닫기 이벤트
        private void TYMROP001I_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsOPM1000 = this.TXT01_OPM1000.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        #endregion

        private void TXT01_OPN1090_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    this.SetFocus(this.BTN61_SAV);
                }
                else if (this.BTN61_EDIT.Visible == true)
                {
                    this.SetFocus(this.BTN61_EDIT);
                }
            }
        }
        #endregion
    }
}