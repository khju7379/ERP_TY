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
    public partial class TYMRRR001I : TYBase
    {
        //private TYData DAT01_PRTHISAB;

        public string fsRRM1000 = string.Empty;
        public string fsRRM1010 = string.Empty;
        public string fsRRM1020 = string.Empty;
        public string fsRRM1030 = string.Empty;

        public string fsPOM1000 = string.Empty;
        public string fsPOM1010 = string.Empty;
        public string fsPOM1020 = string.Empty;
        public string fsPOM1030 = string.Empty;

        private string fsYESAN_COUNT = string.Empty;
        private string fsPOM5110     = string.Empty;

        private string fsPRM5100 = string.Empty;
        private string fsPOM1400 = string.Empty;
        private string fsPOM1410 = string.Empty;
        private string fsPOM1420 = string.Empty;

        private string fsGUBUN = string.Empty;
        private string fsMAGAM = string.Empty;

        #region Description : 페이지 로드
        public TYMRRR001I(string sRRM1000, string sRRM1010, string sRRM1020, string sRRM1030)
        {
            InitializeComponent();

            this.SetPopupStyle();

            if (sRRM1010 == "O")
            {
                // 파라미터값 가져오기
                this.fsPOM1000 = sRRM1000;
                this.fsPOM1010 = sRRM1010;
                this.fsPOM1020 = sRRM1020;
                this.fsPOM1030 = sRRM1030;

                this.fsRRM1010 = "R";
            }
            else
            {
                // 파라미터값 가져오기
                this.fsRRM1000 = sRRM1000;
                this.fsRRM1010 = sRRM1010;
                this.fsRRM1020 = sRRM1020;
                this.fsRRM1030 = sRRM1030;
            }

            this.TXT01_RRM1000.SetValue(fsRRM1000);
            this.TXT01_RRM1010.SetValue(fsRRM1010);
            this.TXT01_RRM1020.SetValue(fsRRM1020);
            this.TXT01_RRM1030.SetValue(fsRRM1030);

            this.TXT01_RRN1000.SetValue(fsRRM1000);
            this.TXT01_RRN1010.SetValue(fsRRM1010);
            this.TXT01_RRN1020.SetValue(fsRRM1020);
            this.TXT01_RRN1030.SetValue(fsRRM1030);

            this.TXT01_RRT1000.SetValue(fsRRM1000);
            this.TXT01_RRT1010.SetValue(fsRRM1010);
            this.TXT01_RRT1020.SetValue(fsRRM1020);
            this.TXT01_RRT1030.SetValue(fsRRM1030);

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_43VE9077, "RRA1200", "PODESC", "RRA1200");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_43VE9077, "RRA1260", "SUDESC", "RRA1260");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_43VE9077, "RRA1270", "MEDESC", "RRA1270");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_43VE9077, "RRA1280", "DTDESC", "RRA1280");
        }

        private void TYMRRR001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_POM10001.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_SAV.ProcessCheck  += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(BTN61_EDIT_ProcessCheck);
            this.BTN61_REM.ProcessCheck  += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);

            //this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2BQ6E639, "RRT1040");

            bool fResult;

            // 등록
            if (this.TXT01_RRM1000.GetValue().ToString() == ""  &&
                this.TXT01_RRM1010.GetValue().ToString() == "R" &&
                this.TXT01_RRM1020.GetValue().ToString() == ""  &&
                this.TXT01_RRM1030.GetValue().ToString() == ""
                )
            {
                fsGUBUN = "MRPRRMF";

                // 컨트롤 초기화
                UP_Control_Initialize("MRPRRMF", true);

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                // 탭 컨트롤
                tabControl1_Enable("");

                // 비용청구
                this.CBO01_RRM6010.SetValue("N");
                // 청구구분
                this.CBO01_RRM6020.SetValue("3");

                // 입고년월
                this.TXT01_RRM1020.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6));

                // 검수부서
                this.CBH01_RRM1410.DummyValue = this.TXT01_RRM1020.GetValue() + "31";

                // 검수사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_RRM1400.SetValue(TYUserInfo.EmpNo);
                // 인수사번
                this.CBH01_RRM1420.SetValue(TYUserInfo.EmpNo);

                // 등록 시 요청부서의 앞자리 가져옴
                this.TXT01_RRM1000.SetValue(this.CBH01_RRM1410.GetValue().ToString().Substring(0, 1));

                SetStartingFocus(this.TXT01_RRM1000);

                // 미입고 그리드에서 선택시
                if (fsPOM1010 == "O")
                {
                    UP_SetPOMData();

                    this.TXT01_POM1000.ReadOnly = true;
                    this.TXT01_POM1010.ReadOnly = true;
                    this.TXT01_POM1020.ReadOnly = true;
                    this.TXT01_POM1030.ReadOnly = true;

                    this.BTN61_POM10001.Enabled = false;
                }
            }
            else // 수정
            {
                fsMAGAM = "";

                this.TXT01_RRM1000.ReadOnly = true;
                this.TXT01_RRM1010.ReadOnly = true;
                this.TXT01_RRM1020.ReadOnly = true;
                this.TXT01_RRM1030.ReadOnly = true;

                fsGUBUN = "MRPRRMF";

                // 컨트롤 초기화
                UP_Control_Initialize("MRPRRMF", true);

                string sDATE = this.TXT01_RRM1020.GetValue() + "31";

                // 검수부서
                this.CBH01_RRM1410.DummyValue = this.TXT01_RRM1020.GetValue() + "31";

                // 마스터 DISPLAY
                UP_MRPRRMF_DISPLAY();

                // 내역사항 DISPLAY
                UP_MRPRRNF_DISPLAY();

                // 특기사항 DISPLAY
                UP_MRPRRTF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    this.SetFocus(this.DTP01_RRM1100);
                }
                else
                {
                    if (TYUserInfo.EmpNo.ToString() == "0377-M")
                    {
                        fsMAGAM = "MAGAM";

                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("5", false);

                        this.SetFocus(this.DTP01_RRM1100);
                    }
                    else
                    {
                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("3", false);
                    }
                }
            }
        }
        #endregion

        //#region Description : 내역사항 신규 버튼
        //private void BTN61_NEW_Click(object sender, EventArgs e)
        //{
        //    UP_ImgbtnDisplay("2", false);

        //    // 컨트롤 초기화
        //    UP_Control_Initialize("MRPRRNF", false);

        //    this.TXT01_RRN1040.SetValue("");
        //    this.TXT01_RRN1070.SetValue("");

        //    this.TXT01_RRN1080.SetValue("");
        //    this.TXT01_RRN1080NM.SetValue("");
        //    this.TXT01_RRN1090.SetValue("");
        //    this.TXT01_RRN1090NM.SetValue("");
        //    this.TXT01_RRN1091.SetValue("");
        //    this.TXT01_RRN1050.SetValue("");
        //    this.TXT01_RRN1050NM.SetValue("");

        //    UP_FieldClear("MRPRRNF");

        //    SetFocus(this.TXT01_RRN1040);
        //}
        //#endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            if (fsGUBUN == "MRPRRMF") // 마스터
            {
                string sRRM1120 = string.Empty;
                string sRRM1130 = string.Empty;
                string sPOM1500 = string.Empty;

                sOUTMSG = "OK";

                // 발주번호
                sRRM1120 = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());
                // 요청번호
                sRRM1130 = this.TXT01_POM1120.GetValue().ToString();
                sRRM1130 = sRRM1130.ToString().Replace("-", "");

                //// 입고번호
                //sPOM1500 = this.TXT01_RRM1000.GetValue().ToString() + this.TXT01_RRM1010.GetValue().ToString() + this.TXT01_RRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_RRM1030.GetValue().ToString());

                // 원본
                // 등록
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BR9Z664",
                //    this.TXT01_RRM1000.GetValue().ToString(),
                //    this.TXT01_RRM1010.GetValue().ToString(),
                //    this.TXT01_RRM1020.GetValue().ToString(),
                //    this.TXT01_RRM1030.GetValue().ToString(),
                //    this.DTP01_RRM1100.GetValue().ToString(),
                //    this.DTP01_RRM1110.GetValue().ToString(),
                //    sRRM1120.ToString(),                      // 발주번호
                //    sRRM1130.ToString(),                      // 요청번호
                //    this.TXT01_RRM1180.GetValue().ToString(), // 공사및구매명
                //    "",                                       // 계약사업장
                //    "",                                       // 계약년도
                //    "",                                       // 계약순서
                //    this.CBH01_RRM1400.GetValue().ToString(), // 검수자
                //    this.CBH01_RRM1410.GetValue().ToString(), // 검수부서
                //    this.CBH01_RRM1420.GetValue().ToString(), // 인수자
                //    this.DTP01_RRM1430.GetValue().ToString(), // 인수일자
                //    this.TXT01_RRM1440.GetValue().ToString(), // 승인자
                //    this.TXT01_RRM1450.GetValue().ToString(), // 승인일자
                //    this.TXT01_RRM1460.GetValue().ToString(), // 그룹웨어승인문서
                //    this.CBO01_RRM1500.GetValue().ToString(), // 월말구분
                //    this.TXT01_RRM1510.GetValue().ToString(), // 전표번호
                //    this.CBO01_RRM6010.GetValue().ToString(), // 비용청구
                //    this.CBO01_RRM6020.GetValue().ToString(), // 청구구분
                //    this.CBH01_RRM6030.GetValue().ToString(), // 청구화주
                //    TYUserInfo.EmpNo.ToString()
                //    );


                string sAUT1030 = string.Empty;

                // 수정(구매입고 SP)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_37U5T281",
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    this.DTP01_RRM1100.GetValue().ToString(),
                    this.DTP01_RRM1110.GetValue().ToString(),
                    sRRM1120.ToString(),                      // 발주번호
                    sRRM1130.ToString(),                      // 요청번호
                    this.TXT01_RRM1180.GetValue().ToString(), // 공사및구매명
                    "",                                       // 계약사업장
                    "",                                       // 계약년도
                    "",                                       // 계약순서
                    this.CBH01_RRM1400.GetValue().ToString(), // 검수자
                    this.CBH01_RRM1410.GetValue().ToString(), // 검수부서
                    this.CBH01_RRM1420.GetValue().ToString(), // 인수자
                    this.DTP01_RRM1430.GetValue().ToString(), // 인수일자
                    this.TXT01_RRM1440.GetValue().ToString(), // 승인자
                    this.TXT01_RRM1450.GetValue().ToString(), // 승인일자
                    this.TXT01_RRM1460.GetValue().ToString(), // 그룹웨어승인문서
                    this.CBO01_RRM1500.GetValue().ToString(), // 월말구분
                    this.TXT01_RRM1510.GetValue().ToString(), // 전표번호
                    this.CBO01_RRM6010.GetValue().ToString(), // 비용청구
                    this.CBO01_RRM6020.GetValue().ToString(), // 청구구분
                    this.CBH01_RRM6030.GetValue().ToString(), // 청구화주
                    TYUserInfo.EmpNo.ToString(),
                    sAUT1030.ToString()
                    );

                sAUT1030 = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sAUT1030.Substring(0, 2).ToString() == "OK")
                {
                    this.TXT01_RRM1030.SetValue(sAUT1030.Substring(3, 4));

                    this.ShowMessage("TY_M_GB_23NAD873");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_246A2488");

                    return;
                }


                
                // 구매입고 예산파일 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BRA7665",
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    this.TXT01_RRM1030.GetValue().ToString(),
                    this.DTP01_RRM1110.GetValue().ToString().Substring(0, 4),
                    this.DTP01_RRM1110.GetValue().ToString().Substring(4, 2),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
                    );

                // 구매입고 내역파일 등록
                this.DbConnector.Attach
                    (
                    "TY_P_MR_9CUGD632",     // 노무비닷컴 적용 후
                    //"TY_P_MR_2BRAU668",   // 노무비닷컷 적용 전
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    this.TXT01_RRM1030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
                    );

                // 구매입고 특기파일 등록
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CD5G146",
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    this.TXT01_RRM1030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
                    );

                this.DbConnector.ExecuteTranQueryList();

                // 마감년월과 발주년월이 다를 경우 입고내역금액 예산테이블에 플러스 시킴
                if (this.DTP01_RRM1110.GetValue().ToString().Substring(0, 6).ToString() != this.TXT01_POM1020.GetValue().ToString())
                {
                    // 예산 가용금액 - 플러스
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BR2G672",
                        this.TXT01_RRM1000.GetValue().ToString(),
                        this.TXT01_RRM1010.GetValue().ToString(),
                        this.TXT01_RRM1020.GetValue().ToString(),
                        this.TXT01_RRM1030.GetValue().ToString(),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }
                else
                {
                    // 예산 가용금액 - 마이너스
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2C74G982",
                        this.TXT01_POM1000.GetValue().ToString(),
                        this.TXT01_POM1010.GetValue().ToString(),
                        this.TXT01_POM1020.GetValue().ToString(),
                        this.TXT01_POM1030.GetValue().ToString(),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // 예산 가용금액 - 플러스
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BR2G672",
                            this.TXT01_RRM1000.GetValue().ToString(),
                            this.TXT01_RRM1010.GetValue().ToString(),
                            this.TXT01_RRM1020.GetValue().ToString(),
                            this.TXT01_RRM1030.GetValue().ToString(),
                            sOUTMSG.ToString()
                            );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    }
                }

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 구매발주 및 요청 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BRBN670",
                        this.TXT01_RRM1000.GetValue().ToString(),
                        this.TXT01_RRM1010.GetValue().ToString(),
                        this.TXT01_RRM1020.GetValue().ToString(),
                        Set_Fill4(this.TXT01_RRM1030.GetValue().ToString()),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        #region Description : 입고승인부분

                        /***************************************************************************************
                         * 1. 입고 등록시 자산 예산이 없을 경우 'Y'
                         *                자산 예산이 있을 경우 'N'
                         * 
                         * 2. 자산 예산이 존재할 경우
                         *    입고자산 내역관리에서 입고수량만큼 자료를 입력한다.
                         ***************************************************************************************/

                        string sRRM7010 = string.Empty;

                        DataTable dt = new DataTable();

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_44191088",
                            this.TXT01_RRM1000.GetValue().ToString(),
                            this.TXT01_RRM1010.GetValue().ToString(),
                            this.TXT01_RRM1020.GetValue().ToString(),
                            Set_Fill4(this.TXT01_RRM1030.GetValue().ToString()),
                            "1",
                            "12200"
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sRRM7010 = "N";
                        }
                        else
                        {
                            sRRM7010 = "Y";
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_4419A089",
                            sRRM7010.ToString(),
                            this.TXT01_RRM1000.GetValue().ToString(),
                            this.TXT01_RRM1010.GetValue().ToString(),
                            this.TXT01_RRM1020.GetValue().ToString(),
                            this.TXT01_RRM1030.GetValue().ToString()
                            );

                        this.DbConnector.ExecuteTranQueryList();

                        #endregion

                        this.ShowMessage("TY_M_GB_23NAD873");

                        // 탭 컨트롤
                        tabControl1_Enable("MRPRRMF");

                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("1", true);

                        this.TXT01_RRM1000.ReadOnly = true;
                        this.TXT01_RRM1010.ReadOnly = true;
                        this.TXT01_RRM1020.ReadOnly = true;
                        this.TXT01_RRM1030.ReadOnly = true;

                        UP_MRPRRMF_DISPLAY();
                    }
                }                
            }
            else if (fsGUBUN == "MRPRRNF") // 내역사항
            {
                //sOUTMSG = "OK";

                //string sPON1530 = string.Empty;
                //string sPON1610 = string.Empty;
                //string sPON1620 = string.Empty;

                //// 비품번호
                //sPON1530 = this.TXT01_RRN5030.GetValue().ToString();
                //// 고정자산번호
                //sPON1610 = this.TXT01_RRN6010.GetValue().ToString();
                //// 고정자산분류코드
                //sPON1620 = this.TXT01_RRN6020.GetValue().ToString();

                //// 내역사항 등록 및 구매요청 관련 업데이트
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BNB4612",
                //    this.TXT01_RRN1000.GetValue().ToString(),
                //    this.TXT01_RRN1010.GetValue().ToString(),
                //    this.TXT01_RRN1020.GetValue().ToString(),
                //    this.TXT01_RRN1030.GetValue().ToString(),
                //    this.TXT01_RRN1040.GetValue().ToString(),
                //    this.TXT01_RRN1050.GetValue().ToString(),
                //    this.TXT01_RRN1070.GetValue().ToString(),
                //    this.TXT01_RRN1080.GetValue().ToString(),
                //    this.TXT01_RRN1090.GetValue().ToString(),
                //    this.TXT01_RRN1091.GetValue().ToString(),
                //    this.TXT01_RRN1092.GetValue().ToString(),
                //    this.CBH01_RRN1100.GetValue().ToString(),
                //    this.CBH01_RRN1110.GetValue().ToString(),
                //    //this.CBO01_PON1120.GetValue().ToString(),
                //    this.CBH01_RRN1120.GetValue().ToString(),
                //    this.CBH01_RRN1120.GetText().ToString(),
                //    this.TXT01_PON1150.GetValue().ToString(),
                //    this.TXT01_PON1160.GetValue().ToString(),
                //    this.TXT01_RRN1230.GetValue().ToString(),
                //    this.TXT01_RRN1140.GetValue().ToString(),
                //    this.TXT01_RRN1200.GetValue().ToString(),
                //    this.TXT01_RRN1210.GetValue().ToString(),
                //    //this.TXT01_PON1220.GetValue().ToString(),
                //    this.TXT01_PON1230.GetValue().ToString(),
                //    //this.TXT01_PON1300.GetValue().ToString(),
                //    //this.TXT01_PON1310.GetValue().ToString(),
                //    this.TXT01_RRN1320.GetValue().ToString(),
                //    this.TXT01_RRN1330.GetValue().ToString(),
                //    //this.CBO01_PON1500.GetValue().ToString(),
                //    //this.TXT01_RRN5010.GetValue().ToString(),
                //    sPON1530.ToString(),
                //    sPON1610.ToString(),
                //    sPON1620.ToString(),
                //    //this.CBO01_PON1630.GetValue().ToString(),
                //    TYUserInfo.EmpNo.ToString(),
                //    this.TXT01_PON1000.GetValue().ToString(),
                //    this.TXT01_PON1010.GetValue().ToString(),
                //    this.TXT01_PON1020.GetValue().ToString(),
                //    this.TXT01_PON1030.GetValue().ToString(),
                //    "A",
                //    sOUTMSG.ToString()
                //    );

                //sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                //if (sOUTMSG.Substring(0, 2) == "OK")
                //{
                //    // 귀속별 예산파일 등록
                //    if (fsYESAN_COUNT.ToString() == "0") // 등록
                //    {
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach
                //            (
                //            "TY_P_MR_2BNAX611",
                //            this.TXT01_RRN1000.GetValue().ToString(),
                //            this.TXT01_RRN1010.GetValue().ToString(),
                //            this.TXT01_RRN1020.GetValue().ToString(),
                //            this.TXT01_RRN1030.GetValue().ToString(),
                //            this.TXT01_RRN1040.GetValue().ToString(),
                //            this.TXT01_RRN1070.GetValue().ToString(),
                //            this.TXT01_RRN1080.GetValue().ToString(),
                //            this.TXT01_RRN1090.GetValue().ToString(),
                //            this.TXT01_RRN1091.GetValue().ToString(),
                //            this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                //            this.TXT01_RRN2040.GetValue().ToString().Substring(4, 2),
                //            TYUserInfo.EmpNo.ToString()
                //            );
                //    }
                //    else // 수정
                //    {
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach
                //            (
                //            "TY_P_MR_2BM91604",
                //            this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                //            this.TXT01_RRN2040.GetValue().ToString().Substring(4, 2),
                //            TYUserInfo.EmpNo.ToString(),
                //            this.TXT01_RRN1000.GetValue().ToString(),
                //            this.TXT01_RRN1010.GetValue().ToString(),
                //            this.TXT01_RRN1020.GetValue().ToString(),
                //            this.TXT01_RRN1030.GetValue().ToString(),
                //            this.TXT01_RRN1040.GetValue().ToString(),
                //            this.TXT01_RRN1070.GetValue().ToString(),
                //            this.TXT01_RRN1080.GetValue().ToString(),
                //            this.TXT01_RRN1090.GetValue().ToString(),
                //            this.TXT01_RRN1091.GetValue().ToString()
                //            );
                //    }

                //    this.DbConnector.ExecuteNonQuery();

                //    // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                //    if (this.fsPRM5100.ToString() != "Y")
                //    {
                //        // 예산 가용금액 - 플러스
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach
                //            (
                //            "TY_P_MR_2BNBE617",
                //            this.TXT01_RRN1000.GetValue().ToString(),
                //            this.TXT01_RRN1010.GetValue().ToString(),
                //            this.TXT01_RRN1020.GetValue().ToString(),
                //            this.TXT01_RRN1030.GetValue().ToString(),
                //            this.TXT01_RRN1040.GetValue().ToString(),
                //            this.TXT01_RRN1050.GetValue().ToString(),
                //            this.TXT01_RRN1070.GetValue().ToString(),
                //            this.TXT01_RRN1080.GetValue().ToString(),
                //            this.TXT01_RRN1090.GetValue().ToString(),
                //            this.TXT01_RRN1091.GetValue().ToString(),
                //            this.TXT01_RRN1092.GetValue().ToString(),
                //            sOUTMSG.ToString()
                //            );

                //        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                //    }

                //    if (sOUTMSG.Substring(0, 2) == "OK")
                //    {
                //        // 마스터 테이블에 발주금액 업데이트
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach
                //            (
                //            "TY_P_MR_2BM94606",
                //            this.TXT01_RRN1000.GetValue().ToString(),
                //            this.TXT01_RRN1010.GetValue().ToString(),
                //            this.TXT01_RRN1020.GetValue().ToString(),
                //            this.TXT01_RRN1030.GetValue().ToString()
                //            );

                //        this.DbConnector.ExecuteNonQuery();

                //        this.ShowMessage("TY_M_GB_23NAD873");

                //        // 버튼 초기화
                //        UP_ImgbtnDisplay("3", false);

                //        // 컨트롤 초기화
                //        UP_Control_Initialize("MRPRRNF", false);

                //        this.TXT01_RRN1040.SetValue("");
                //        this.TXT01_RRN1070.SetValue("");

                //        this.TXT01_RRN1080.SetValue("");
                //        this.TXT01_RRN1080NM.SetValue("");
                //        this.TXT01_RRN1090.SetValue("");
                //        this.TXT01_RRN1090NM.SetValue("");
                //        this.TXT01_RRN1091.SetValue("");
                //        this.TXT01_RRN1050.SetValue("");
                //        this.TXT01_RRN1050NM.SetValue("");

                //        UP_FieldClear("MRPRRNF");

                //        UP_MRPRRNF_DISPLAY();

                //        SetFocus(this.TXT01_RRN1040);
                //    }
                //}
            }
        }
        #endregion

        #region Description : 수정 버튼
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "MRPRRMF") // 마스터
            {
                if (TYUserInfo.EmpNo.ToString() == "0377-M" && fsMAGAM.ToString() == "MAGAM")
                {
                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_34I2U529",
                        this.DTP01_RRM1110.GetValue().ToString(),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_RRM1000.GetValue().ToString(),
                        this.TXT01_RRM1010.GetValue().ToString(),
                        this.TXT01_RRM1020.GetValue().ToString(),
                        this.TXT01_RRM1030.GetValue().ToString()
                        );
                }
                else
                {
                    string sRRM1120 = string.Empty;
                    string sRRM1130 = string.Empty;
                    string sPOM1500 = string.Empty;

                    // 발주번호
                    sRRM1120 = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());
                    // 요청번호
                    sRRM1130 = this.TXT01_POM1120.GetValue().ToString();
                    sRRM1130 = sRRM1130.ToString().Replace("-", "");

                    // 입고번호
                    sPOM1500 = this.TXT01_RRM1000.GetValue().ToString() + this.TXT01_RRM1010.GetValue().ToString() + this.TXT01_RRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_RRM1030.GetValue().ToString());

                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BR83691",
                        this.DTP01_RRM1100.GetValue().ToString(),
                        this.DTP01_RRM1110.GetValue().ToString(),
                        sRRM1120.ToString(),                      // 발주번호
                        sRRM1130.ToString(),                      // 요청번호
                        this.TXT01_RRM1180.GetValue().ToString(), // 공사및구매명
                        "",                                       // 계약사업장
                        "",                                       // 계약년도
                        "",                                       // 계약순서
                        this.CBH01_RRM1400.GetValue().ToString(), // 검수자
                        this.CBH01_RRM1410.GetValue().ToString(), // 검수부서
                        this.CBH01_RRM1420.GetValue().ToString(), // 인수자
                        this.DTP01_RRM1430.GetValue().ToString(), // 인수일자
                        this.TXT01_RRM1440.GetValue().ToString(), // 승인자
                        this.TXT01_RRM1450.GetValue().ToString(), // 승인일자
                        this.TXT01_RRM1460.GetValue().ToString(), // 그룹웨어승인문서
                        this.CBO01_RRM1500.GetValue().ToString(), // 월말구분
                        this.TXT01_RRM1510.GetValue().ToString(), // 전표번호
                        this.CBO01_RRM6010.GetValue().ToString(), // 비용청구
                        this.CBO01_RRM6020.GetValue().ToString(), // 청구구분
                        this.CBH01_RRM6030.GetValue().ToString(), // 청구화주
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_RRM1000.GetValue().ToString(),
                        this.TXT01_RRM1010.GetValue().ToString(),
                        this.TXT01_RRM1020.GetValue().ToString(),
                        this.TXT01_RRM1030.GetValue().ToString()
                        );
                }

                this.DbConnector.ExecuteNonQuery();
                this.ShowMessage("TY_M_MR_2BD3Z286");

                // 탭 컨트롤
                tabControl1_Enable("MRPRRMF");

                UP_MRPRRMF_DISPLAY();
            }
            else if (fsGUBUN == "MRPRRNF") // 내역사항
            {
                // 노무비닷컴 수정 권한체크
                if (TYUserInfo.EmpNo.ToString() == "0377-M" && fsMAGAM.ToString() == "MAGAM")
                {
                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_9CUGV633",
                        this.CBO01_RRN7000.GetValue().ToString(),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_RRN1000.GetValue().ToString(),
                        this.TXT01_RRN1010.GetValue().ToString(),
                        this.TXT01_RRN1020.GetValue().ToString(),
                        this.TXT01_RRN1030.GetValue().ToString(),
                        this.TXT01_RRN1040.GetValue().ToString(),
                        this.TXT01_RRN1050.GetValue().ToString(),
                        this.TXT01_RRN1070.GetValue().ToString(),
                        this.TXT01_RRN1080.GetValue().ToString(),
                        this.TXT01_RRN1090.GetValue().ToString(),
                        this.TXT01_RRN1091.GetValue().ToString(),
                        this.TXT01_RRN1092.GetValue().ToString()
                        );

                    this.DbConnector.ExecuteNonQuery();

                    this.ShowMessage("TY_M_MR_2BD3Z286");

                    // 버튼 초기화
                    UP_ImgbtnDisplay("3", false);

                    // 컨트롤 초기화
                    UP_Control_Initialize("MRPRRNF", false);

                    this.TXT01_RRN1040.SetValue("");
                    this.TXT01_RRN1070.SetValue("");

                    this.TXT01_RRN1080.SetValue("");
                    this.TXT01_RRN1080NM.SetValue("");
                    this.TXT01_RRN1090.SetValue("");
                    this.TXT01_RRN1090NM.SetValue("");
                    this.TXT01_RRN1091.SetValue("");
                    this.TXT01_RRN1092.SetValue("");
                    this.TXT01_RRN1050.SetValue("");
                    this.TXT01_RRN1050NM.SetValue("");

                    UP_FieldClear("MRPRRNF");

                    UP_MRPRRNF_DISPLAY();

                    SetFocus(this.TXT01_RRN1040);
                }
                else
                {
                    string sOUTMSG = string.Empty;

                    string sPON1530 = string.Empty;
                    string sPON1610 = string.Empty;
                    string sPON1620 = string.Empty;
                    string sRRN1130 = string.Empty;

                    DataTable dt = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2CH3M224",
                        this.TXT01_RRN1050.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sRRN1130 = dt.Rows[0]["Z105023"].ToString();
                    }

                    // 비품번호
                    sPON1530 = this.TXT01_RRN5030.GetValue().ToString();
                    // 고정자산번호
                    sPON1610 = this.TXT01_RRN6010.GetValue().ToString();
                    // 고정자산분류코드
                    sPON1620 = this.TXT01_RRN6020.GetValue().ToString();

                    sOUTMSG = "OK";

                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    if (this.TXT01_RRN1080.GetValue().ToString() != "11101001" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                    {
                        // SP 수정완료 미적용
                        // 입고예산 가용금액 - 마이너스
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BR8Y697",
                            this.TXT01_RRN1000.GetValue().ToString(),
                            this.TXT01_RRN1010.GetValue().ToString(),
                            this.TXT01_RRN1020.GetValue().ToString(),
                            this.TXT01_RRN1030.GetValue().ToString(),
                            this.TXT01_RRN1040.GetValue().ToString(),
                            this.TXT01_RRN1050.GetValue().ToString(),
                            this.TXT01_RRN1070.GetValue().ToString(),
                            this.TXT01_RRN1080.GetValue().ToString(),
                            this.TXT01_RRN1090.GetValue().ToString(),
                            this.TXT01_RRN1091.GetValue().ToString(),
                            Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()),
                            sOUTMSG.ToString()
                            );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    }

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {   
                        double dRRN1200 = 0;
                        double dRRN1210 = 0;
                        double dRRN1140 = 0;

                        double dRRN1240 = 0;
                        double dRRN1250 = 0;

                        double dRRN1220 = 0;
                        double dRRN1260 = 0;

                        dRRN1240 = 0;
                        dRRN1250 = 0;
                        dRRN1260 = 0;

                        dRRN1200 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1200.GetValue().ToString())))); //입고량
                        dRRN1210 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1210.GetValue().ToString())))); //입고단가
                        dRRN1140 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1140.GetValue().ToString())))); //환율
                        dRRN1220 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1220.GetValue().ToString())))); //입고감소단가

                        //외화일경우
                        if (this.CBH01_RRN1120.GetValue().ToString() != "1")
                        {
                            // 입고단가(원화)
                            dRRN1240 = Math.Round(dRRN1210 * dRRN1140);
                            // 입고금액(원화)
                            dRRN1250 = Math.Round(dRRN1200 * dRRN1210 * dRRN1140);
                            // 입고감소단가(원화)
                            dRRN1260 = Math.Round(dRRN1220 * dRRN1140);
                        }
                        else
                        {
                            // 입고단가(원화)
                            dRRN1240 = Math.Round(dRRN1210);
                            // 입고금액(원화)
                            dRRN1250 = Math.Round(dRRN1200 * dRRN1210);
                            // 입고감소금액(원화)
                            dRRN1260 = Math.Round(dRRN1220);
                        }


                        // 입고감소금액 추가 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_BBMHB769",
                            this.TXT01_RRN1000.GetValue().ToString(), // 사업부               
                            this.TXT01_RRN1010.GetValue().ToString(), // RR(R)                
                            this.TXT01_RRN1020.GetValue().ToString(), // 년월                 
                            this.TXT01_RRN1030.GetValue().ToString(), // 순서                 
                            this.TXT01_RRN1040.GetValue().ToString(), // 귀속부서             
                            this.TXT01_RRN1050.GetValue().ToString(), // 품목                 
                            this.TXT01_RRN1070.GetValue().ToString(), // 예산구분             
                            this.TXT01_RRN1080.GetValue().ToString(), // 적용계정             
                            this.TXT01_RRN1090.GetValue().ToString(), // 비품코드             
                            this.TXT01_RRN1091.GetValue().ToString(), // 순　　번
                            Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()), // 내역순번
                            this.CBH01_RRN1100.GetValue().ToString(), // 입고거래처           
                            this.CBH01_RRN1110.GetValue().ToString(), // 부가세구분           
                            this.CBH01_RRN1120.GetValue().ToString(), // 화폐
                            sRRN1130.ToString(),                      // 단위
                            //this.CBH01_RRN1120.GetText().ToString(),  // 단위                 
                            this.TXT01_RRN1140.GetValue().ToString(), // 적용환율             
                            this.TXT01_RRN1200.GetValue().ToString(), // 입고수량             
                            this.TXT01_RRN1210.GetValue().ToString(), // 입고단가             
                            dRRN1220,                                 // 입고감소단가             
                            this.TXT01_RRN1230.GetValue().ToString(), // 입고금액             
                            dRRN1240.ToString(),                      // 입고단가(원화)       
                            dRRN1250.ToString(),                      // 입고금액(원화)      
                            dRRN1260.ToString(),                      // 입고감소단가(원화) 
                            this.CBO01_RRN1300.GetValue().ToString(), // 불량여부 Y           
                            this.CBH01_RRN1310.GetValue().ToString(), // 불량형태             
                            this.TXT01_RRN1320.GetValue().ToString(), // 불량수량             
                            this.TXT01_RRN1330.GetValue().ToString(), // 불량금액             
                            this.TXT01_RRN1400.GetValue().ToString(), // 관리대상(Y)          
                            "",                                       // 불출구분             
                            "",                                       // 창고코드             
                            this.TXT01_RRN1520.GetValue().ToString(), // 전표번호             
                            this.TXT01_RRN5010.GetValue().ToString(), // 비품(Y)              
                            this.TXT01_RRN5030.GetValue().ToString(), // 비품번호(년월 - 순번)
                            this.TXT01_RRN6000.GetValue().ToString(), // 고정생성자산번호     
                            this.TXT01_RRN6010.GetValue().ToString(), // 고정자산번호         
                            this.TXT01_RRN6020.GetValue().ToString(), // 고정자산분류코드     
                            this.TXT01_RRN6030.GetValue().ToString(), // 고정자산취합구분     
                            this.CBO01_RRN7000.GetValue().ToString(), // 노무비닷컴 구분
                            TYUserInfo.EmpNo.ToString(),              // 사번
                            "UPT",                                    // 작업구분
                            this.TXT01_RRN1150.GetValue().ToString().Substring(0, 1), // 검수구분
                            sOUTMSG.ToString()                        // OUTPUT메세지
                            );

                        // 노무비닷컴 적용 후
                        // 구매입고 내역사항 수정 및 삭제시 - 구매발주 및 요청 업데이트
                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach
                        //    (
                        //    "TY_P_MR_9CUFM630",
                        //    this.TXT01_RRN1000.GetValue().ToString(), // 사업부               
                        //    this.TXT01_RRN1010.GetValue().ToString(), // RR(R)                
                        //    this.TXT01_RRN1020.GetValue().ToString(), // 년월                 
                        //    this.TXT01_RRN1030.GetValue().ToString(), // 순서                 
                        //    this.TXT01_RRN1040.GetValue().ToString(), // 귀속부서             
                        //    this.TXT01_RRN1050.GetValue().ToString(), // 품목                 
                        //    this.TXT01_RRN1070.GetValue().ToString(), // 예산구분             
                        //    this.TXT01_RRN1080.GetValue().ToString(), // 적용계정             
                        //    this.TXT01_RRN1090.GetValue().ToString(), // 비품코드             
                        //    this.TXT01_RRN1091.GetValue().ToString(), // 순　　번
                        //    Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()), // 내역순번
                        //    this.CBH01_RRN1100.GetValue().ToString(), // 입고거래처           
                        //    this.CBH01_RRN1110.GetValue().ToString(), // 부가세구분           
                        //    this.CBH01_RRN1120.GetValue().ToString(), // 화폐
                        //    sRRN1130.ToString(),                      // 단위
                        //    //this.CBH01_RRN1120.GetText().ToString(),  // 단위                 
                        //    this.TXT01_RRN1140.GetValue().ToString(), // 적용환율             
                        //    this.TXT01_RRN1200.GetValue().ToString(), // 입고수량             
                        //    this.TXT01_RRN1210.GetValue().ToString(), // 입고단가             
                        //    this.TXT01_RRN1230.GetValue().ToString(), // 입고금액             
                        //    dRRN1240.ToString(),                      // 입고단가(원화)       
                        //    dRRN1250.ToString(),                      // 입고금액(원화)       
                        //    this.CBO01_RRN1300.GetValue().ToString(), // 불량여부 Y           
                        //    this.CBH01_RRN1310.GetValue().ToString(), // 불량형태             
                        //    this.TXT01_RRN1320.GetValue().ToString(), // 불량수량             
                        //    this.TXT01_RRN1330.GetValue().ToString(), // 불량금액             
                        //    this.TXT01_RRN1400.GetValue().ToString(), // 관리대상(Y)          
                        //    "",                                       // 불출구분             
                        //    "",                                       // 창고코드             
                        //    this.TXT01_RRN1520.GetValue().ToString(), // 전표번호             
                        //    this.TXT01_RRN5010.GetValue().ToString(), // 비품(Y)              
                        //    this.TXT01_RRN5030.GetValue().ToString(), // 비품번호(년월 - 순번)
                        //    this.TXT01_RRN6000.GetValue().ToString(), // 고정생성자산번호     
                        //    this.TXT01_RRN6010.GetValue().ToString(), // 고정자산번호         
                        //    this.TXT01_RRN6020.GetValue().ToString(), // 고정자산분류코드     
                        //    this.TXT01_RRN6030.GetValue().ToString(), // 고정자산취합구분     
                        //    this.CBO01_RRN7000.GetValue().ToString(), // 노무비닷컴 구분
                        //    TYUserInfo.EmpNo.ToString(),              // 사번
                        //    "UPT",                                    // 작업구분
                        //    this.TXT01_RRN1150.GetValue().ToString().Substring(0, 1), // 검수구분
                        //    sOUTMSG.ToString()                        // OUTPUT메세지
                        //    );

                        // 노무비닷컴 적용 전
                        //// 구매입고 내역사항 수정 및 삭제시 - 구매발주 및 요청 업데이트
                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach
                        //    (
                        //    "TY_P_MR_2BS9K703",
                        //    this.TXT01_RRN1000.GetValue().ToString(), // 사업부               
                        //    this.TXT01_RRN1010.GetValue().ToString(), // RR(R)                
                        //    this.TXT01_RRN1020.GetValue().ToString(), // 년월                 
                        //    this.TXT01_RRN1030.GetValue().ToString(), // 순서                 
                        //    this.TXT01_RRN1040.GetValue().ToString(), // 귀속부서             
                        //    this.TXT01_RRN1050.GetValue().ToString(), // 품목                 
                        //    this.TXT01_RRN1070.GetValue().ToString(), // 예산구분             
                        //    this.TXT01_RRN1080.GetValue().ToString(), // 적용계정             
                        //    this.TXT01_RRN1090.GetValue().ToString(), // 비품코드             
                        //    this.TXT01_RRN1091.GetValue().ToString(), // 순　　번
                        //    Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()), // 내역순번
                        //    this.CBH01_RRN1100.GetValue().ToString(), // 입고거래처           
                        //    this.CBH01_RRN1110.GetValue().ToString(), // 부가세구분           
                        //    this.CBH01_RRN1120.GetValue().ToString(), // 화폐
                        //    sRRN1130.ToString(),                      // 단위
                        //    //this.CBH01_RRN1120.GetText().ToString(),  // 단위                 
                        //    this.TXT01_RRN1140.GetValue().ToString(), // 적용환율             
                        //    this.TXT01_RRN1200.GetValue().ToString(), // 입고수량             
                        //    this.TXT01_RRN1210.GetValue().ToString(), // 입고단가             
                        //    this.TXT01_RRN1230.GetValue().ToString(), // 입고금액             
                        //    dRRN1240.ToString(),                      // 입고단가(원화)       
                        //    dRRN1250.ToString(),                      // 입고금액(원화)       
                        //    this.CBO01_RRN1300.GetValue().ToString(), // 불량여부 Y           
                        //    this.CBH01_RRN1310.GetValue().ToString(), // 불량형태             
                        //    this.TXT01_RRN1320.GetValue().ToString(), // 불량수량             
                        //    this.TXT01_RRN1330.GetValue().ToString(), // 불량금액             
                        //    this.TXT01_RRN1400.GetValue().ToString(), // 관리대상(Y)          
                        //    "",                                       // 불출구분             
                        //    "",                                       // 창고코드             
                        //    this.TXT01_RRN1520.GetValue().ToString(), // 전표번호             
                        //    this.TXT01_RRN5010.GetValue().ToString(), // 비품(Y)              
                        //    this.TXT01_RRN5030.GetValue().ToString(), // 비품번호(년월 - 순번)
                        //    this.TXT01_RRN6000.GetValue().ToString(), // 고정생성자산번호     
                        //    this.TXT01_RRN6010.GetValue().ToString(), // 고정자산번호         
                        //    this.TXT01_RRN6020.GetValue().ToString(), // 고정자산분류코드     
                        //    this.TXT01_RRN6030.GetValue().ToString(), // 고정자산취합구분     
                        //    TYUserInfo.EmpNo.ToString(),              // 사번
                        //    "UPT",                                    // 작업구분
                        //    this.TXT01_RRN1150.GetValue().ToString().Substring(0,1), // 검수구분
                        //    sOUTMSG.ToString()                        // OUTPUT메세지
                        //    );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                        if (sOUTMSG.Substring(0, 2) == "OK")
                        {
                            sOUTMSG = "OK";

                            // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                            if (this.TXT01_RRN1080.GetValue().ToString() != "11101001" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                            {
                                // SP 수정완료 미적용
                                // 예산 가용금액 - 플러스
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_2BS4P736",
                                    this.TXT01_RRN1000.GetValue().ToString(),
                                    this.TXT01_RRN1010.GetValue().ToString(),
                                    this.TXT01_RRN1020.GetValue().ToString(),
                                    this.TXT01_RRN1030.GetValue().ToString(),
                                    this.TXT01_RRN1040.GetValue().ToString(),
                                    this.TXT01_RRN1050.GetValue().ToString(),
                                    this.TXT01_RRN1070.GetValue().ToString(),
                                    this.TXT01_RRN1080.GetValue().ToString(),
                                    this.TXT01_RRN1090.GetValue().ToString(),
                                    this.TXT01_RRN1091.GetValue().ToString(),
                                    Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()),
                                    sOUTMSG.ToString()
                                    );

                                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                            }

                            if (sOUTMSG.Substring(0, 2) == "OK")
                            {
                                // 귀속별 예산파일 수정
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_2BSAY709",
                                    this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                                    this.TXT01_RRN2040.GetValue().ToString().Substring(4, 2),
                                    TYUserInfo.EmpNo.ToString(),
                                    this.TXT01_RRN1000.GetValue().ToString(),
                                    this.TXT01_RRN1010.GetValue().ToString(),
                                    this.TXT01_RRN1020.GetValue().ToString(),
                                    this.TXT01_RRN1030.GetValue().ToString(),
                                    this.TXT01_RRN1040.GetValue().ToString(),
                                    this.TXT01_RRN1070.GetValue().ToString(),
                                    this.TXT01_RRN1080.GetValue().ToString(),
                                    this.TXT01_RRN1090.GetValue().ToString(),
                                    this.TXT01_RRN1091.GetValue().ToString()
                                    );

                                this.DbConnector.ExecuteNonQuery();

                                this.ShowMessage("TY_M_MR_2BD3Z286");

                                // 버튼 초기화
                                UP_ImgbtnDisplay("3", false);

                                // 컨트롤 초기화
                                UP_Control_Initialize("MRPRRNF", false);

                                this.TXT01_RRN1040.SetValue("");
                                this.TXT01_RRN1070.SetValue("");

                                this.TXT01_RRN1080.SetValue("");
                                this.TXT01_RRN1080NM.SetValue("");
                                this.TXT01_RRN1090.SetValue("");
                                this.TXT01_RRN1090NM.SetValue("");
                                this.TXT01_RRN1091.SetValue("");
                                this.TXT01_RRN1092.SetValue("");
                                this.TXT01_RRN1050.SetValue("");
                                this.TXT01_RRN1050NM.SetValue("");

                                UP_FieldClear("MRPRRNF");

                                UP_MRPRRNF_DISPLAY();

                                SetFocus(this.TXT01_RRN1040);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            if (fsGUBUN == "MRPRRMF") // 마스터
            {
                // 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BR85692",
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    this.TXT01_RRM1030.GetValue().ToString()
                    );

                // 구매발주 마스터 입고번호 클리어
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BR88693",
                    "", // 입고번호
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQueryList();
                this.ShowMessage("TY_M_GB_23NAD874");

                // 입고번호
                this.TXT01_RRM1000.SetReadOnly(false);
                this.TXT01_RRM1010.SetReadOnly(true);
                this.TXT01_RRM1020.SetReadOnly(false);
                this.TXT01_RRM1030.SetReadOnly(true);

                // 발주번호
                this.TXT01_POM1000.SetReadOnly(false);
                //this.TXT01_POM1010.ReadOnly = false;
                this.TXT01_POM1020.SetReadOnly(false);
                this.TXT01_POM1030.SetReadOnly(false);

                this.BTN61_POM10001.Enabled = true;
                //this.BTN61_POM10001.SetReadOnly(false);

                this.TXT01_RRM1020.SetValue("");
                this.TXT01_RRM1030.SetValue("");

                // 탭 컨트롤
                tabControl1_Enable("");

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                UP_FieldClear("MRPRRMF");

                //SetFocus(this.TXT01_RRM1020);
                SetFocus(this.TXT01_RRM1000);
            }
            else if (fsGUBUN == "MRPRRNF") // 내역사항
            {
                string sRRN1130 = string.Empty;

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CH3M224",
                    this.TXT01_RRN1050.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sRRN1130 = dt.Rows[0]["Z105023"].ToString();
                }

                sOUTMSG = "OK";

                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_RRN1080.GetValue().ToString() != "11101001" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                {
                    // SP 수정완료 미적용
                    // 입고예산 가용금액 - 마이너스
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BR8Y697",
                        this.TXT01_RRN1000.GetValue().ToString(),
                        this.TXT01_RRN1010.GetValue().ToString(),
                        this.TXT01_RRN1020.GetValue().ToString(),
                        this.TXT01_RRN1030.GetValue().ToString(),
                        this.TXT01_RRN1040.GetValue().ToString(),
                        this.TXT01_RRN1050.GetValue().ToString(),
                        this.TXT01_RRN1070.GetValue().ToString(),
                        this.TXT01_RRN1080.GetValue().ToString(),
                        this.TXT01_RRN1090.GetValue().ToString(),
                        this.TXT01_RRN1091.GetValue().ToString(),
                        Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 입고년월과 발주년월이 같을 경우 발주예산금액 예산테이블에 플러스 시킴
                    if (this.TXT01_RRN1020.GetValue().ToString() == this.TXT01_PON1020.GetValue().ToString())
                    {
                        // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                        if (this.TXT01_RRN1080.GetValue().ToString() != "11101001" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                        {
                            // 발주예산 가용금액 - 플러스
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_MR_2C76Y984",
                                this.TXT01_PON1000.GetValue().ToString(),
                                this.TXT01_PON1010.GetValue().ToString(),
                                this.TXT01_PON1020.GetValue().ToString(),
                                this.TXT01_PON1030.GetValue().ToString(),
                                this.TXT01_RRN1040.GetValue().ToString(),
                                this.TXT01_RRN1050.GetValue().ToString(),
                                this.TXT01_RRN1070.GetValue().ToString(),
                                this.TXT01_RRN1080.GetValue().ToString(),
                                this.TXT01_RRN1090.GetValue().ToString(),
                                this.TXT01_RRN1091.GetValue().ToString(),
                                this.TXT01_RRN1092.GetValue().ToString(),
                                sOUTMSG.ToString()
                                );

                            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                        }
                    }
                }                

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    double dRRN1200 = 0;
                    double dRRN1210 = 0;
                    double dRRN1140 = 0;

                    double dRRN1240 = 0;
                    double dRRN1250 = 0;

                    double dRRN1220 = 0;
                    double dRRN1260 = 0;

                    dRRN1240 = 0;
                    dRRN1250 = 0;
                    dRRN1260 = 0;

                    dRRN1200 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1200.GetValue().ToString())))); //입고량
                    dRRN1210 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1210.GetValue().ToString())))); //입고단가
                    dRRN1140 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1140.GetValue().ToString())))); //환율
                    dRRN1220 = Convert.ToDouble(decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1220.GetValue().ToString())))); //입고감소단가

                    //외화일경우
                    if (this.CBH01_RRN1120.GetValue().ToString() != "1")
                    {
                        // 입고단가(원화)
                        dRRN1240 = Math.Round(dRRN1210 * dRRN1140);
                        // 입고금액(원화)
                        dRRN1250 = Math.Round(dRRN1200 * dRRN1210 * dRRN1140);
                        // 입고감소단가(원화)
                        dRRN1260 = Math.Round(dRRN1220 * dRRN1140);
                    }
                    else
                    {
                        // 입고단가(원화)
                        dRRN1240 = Math.Round(dRRN1210);
                        // 입고금액(원화)
                        dRRN1250 = Math.Round(dRRN1200 * dRRN1210);
                        // 입고감소금액(원화)
                        dRRN1260 = Math.Round(dRRN1220);
                    }

                    // 입고감소금액 추가 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_BBMHB769",
                        this.TXT01_RRN1000.GetValue().ToString(), // 사업부               
                        this.TXT01_RRN1010.GetValue().ToString(), // RR(R)                
                        this.TXT01_RRN1020.GetValue().ToString(), // 년월                 
                        this.TXT01_RRN1030.GetValue().ToString(), // 순서                 
                        this.TXT01_RRN1040.GetValue().ToString(), // 귀속부서             
                        this.TXT01_RRN1050.GetValue().ToString(), // 품목                 
                        this.TXT01_RRN1070.GetValue().ToString(), // 예산구분             
                        this.TXT01_RRN1080.GetValue().ToString(), // 적용계정             
                        this.TXT01_RRN1090.GetValue().ToString(), // 비품코드             
                        this.TXT01_RRN1091.GetValue().ToString(), // 순　　번
                        Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()), // 내역순번
                        this.CBH01_RRN1100.GetValue().ToString(), // 입고거래처           
                        this.CBH01_RRN1110.GetValue().ToString(), // 부가세구분           
                        this.CBH01_RRN1120.GetValue().ToString(), // 화폐
                        sRRN1130.ToString(),                      // 단위
                        //this.CBH01_RRN1120.GetText().ToString(),  // 단위                 
                        this.TXT01_RRN1140.GetValue().ToString(), // 적용환율             
                        this.TXT01_RRN1200.GetValue().ToString(), // 입고수량             
                        this.TXT01_RRN1210.GetValue().ToString(), // 입고단가             
                        dRRN1220,                                 // 입고감소단가             
                        this.TXT01_RRN1230.GetValue().ToString(), // 입고금액             
                        dRRN1240.ToString(),                      // 입고단가(원화)       
                        dRRN1250.ToString(),                      // 입고금액(원화)      
                        dRRN1260.ToString(),                      // 입고감소단가(원화) 
                        this.CBO01_RRN1300.GetValue().ToString(), // 불량여부 Y           
                        this.CBH01_RRN1310.GetValue().ToString(), // 불량형태             
                        this.TXT01_RRN1320.GetValue().ToString(), // 불량수량             
                        this.TXT01_RRN1330.GetValue().ToString(), // 불량금액             
                        this.TXT01_RRN1400.GetValue().ToString(), // 관리대상(Y)          
                        "",                                       // 불출구분             
                        "",                                       // 창고코드             
                        this.TXT01_RRN1520.GetValue().ToString(), // 전표번호             
                        this.TXT01_RRN5010.GetValue().ToString(), // 비품(Y)              
                        this.TXT01_RRN5030.GetValue().ToString(), // 비품번호(년월 - 순번)
                        this.TXT01_RRN6000.GetValue().ToString(), // 고정생성자산번호     
                        this.TXT01_RRN6010.GetValue().ToString(), // 고정자산번호         
                        this.TXT01_RRN6020.GetValue().ToString(), // 고정자산분류코드     
                        this.TXT01_RRN6030.GetValue().ToString(), // 고정자산취합구분     
                        this.CBO01_RRN7000.GetValue().ToString(), // 노무비닷컴 구분
                        TYUserInfo.EmpNo.ToString(),              // 사번
                        "DEL",                                    // 작업구분
                        this.TXT01_RRN1150.GetValue().ToString().Substring(0, 1), // 검수구분
                        sOUTMSG.ToString()                        // OUTPUT메세지
                        );

                    //// 구매입고 내역사항 수정 및 삭제시 - 구매발주 및 요청 업데이트
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach
                    //    (
                    //    "TY_P_MR_2BS9K703",
                    //    this.TXT01_RRN1000.GetValue().ToString(), // 사업부               
                    //    this.TXT01_RRN1010.GetValue().ToString(), // RR(R)                
                    //    this.TXT01_RRN1020.GetValue().ToString(), // 년월                 
                    //    this.TXT01_RRN1030.GetValue().ToString(), // 순서                 
                    //    this.TXT01_RRN1040.GetValue().ToString(), // 귀속부서             
                    //    this.TXT01_RRN1050.GetValue().ToString(), // 품목                 
                    //    this.TXT01_RRN1070.GetValue().ToString(), // 예산구분             
                    //    this.TXT01_RRN1080.GetValue().ToString(), // 적용계정             
                    //    this.TXT01_RRN1090.GetValue().ToString(), // 비품코드             
                    //    this.TXT01_RRN1091.GetValue().ToString(), // 순　　번
                    //    Get_Numeric(this.TXT01_RRN1092.GetValue().ToString()), // 내역순번
                    //    this.CBH01_RRN1100.GetValue().ToString(), // 입고거래처           
                    //    this.CBH01_RRN1110.GetValue().ToString(), // 부가세구분           
                    //    this.CBH01_RRN1120.GetValue().ToString(), // 화폐
                    //    sRRN1130.ToString(),                      // 단위
                    //    //this.CBH01_RRN1120.GetText().ToString(),  // 단위
                    //    this.TXT01_RRN1140.GetValue().ToString(), // 적용환율             
                    //    this.TXT01_RRN1200.GetValue().ToString(), // 입고수량             
                    //    this.TXT01_RRN1210.GetValue().ToString(), // 입고단가             
                    //    this.TXT01_RRN1230.GetValue().ToString(), // 입고금액             
                    //    dRRN1240.ToString(),                      // 입고단가(원화)       
                    //    dRRN1250.ToString(),                      // 입고금액(원화)       
                    //    this.CBO01_RRN1300.GetValue().ToString(), // 불량여부 Y           
                    //    this.CBH01_RRN1310.GetValue().ToString(), // 불량형태             
                    //    this.TXT01_RRN1320.GetValue().ToString(), // 불량수량             
                    //    this.TXT01_RRN1330.GetValue().ToString(), // 불량금액             
                    //    this.TXT01_RRN1400.GetValue().ToString(), // 관리대상(Y)          
                    //    "",                                       // 불출구분             
                    //    "",                                       // 창고코드             
                    //    this.TXT01_RRN1520.GetValue().ToString(), // 전표번호             
                    //    this.TXT01_RRN5010.GetValue().ToString(), // 비품(Y)              
                    //    this.TXT01_RRN5030.GetValue().ToString(), // 비품번호(년월 - 순번)
                    //    this.TXT01_RRN6000.GetValue().ToString(), // 고정생성자산번호     
                    //    this.TXT01_RRN6010.GetValue().ToString(), // 고정자산번호         
                    //    this.TXT01_RRN6020.GetValue().ToString(), // 고정자산분류코드     
                    //    this.TXT01_RRN6030.GetValue().ToString(), // 고정자산취합구분     
                    //    TYUserInfo.EmpNo.ToString(),              // 사번
                    //    "DEL",                                    // 작업구분
                    //    this.TXT01_RRN1150.GetValue().ToString().Substring(0,1), // 검수구분
                    //    sOUTMSG.ToString()                        // OUTPUT메세지
                    //    );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                        if (this.TXT01_RRN1080.GetValue().ToString() != "11101001" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                        {
                            // 귀속별 예산파일 삭제
                            if (fsYESAN_COUNT.ToString() == "1") // 삭제
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_2BSAU708",
                                    this.TXT01_RRN1000.GetValue().ToString(),
                                    this.TXT01_RRN1010.GetValue().ToString(),
                                    this.TXT01_RRN1020.GetValue().ToString(),
                                    this.TXT01_RRN1030.GetValue().ToString(),
                                    this.TXT01_RRN1040.GetValue().ToString(),
                                    this.TXT01_RRN1070.GetValue().ToString(),
                                    this.TXT01_RRN1080.GetValue().ToString(),
                                    this.TXT01_RRN1090.GetValue().ToString(),
                                    this.TXT01_RRN1091.GetValue().ToString()
                                    );
                            }
                        }
                        else
                        {
                            // 귀속별 예산파일 삭제
                            if (fsYESAN_COUNT.ToString() == "1") // 삭제
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_2BSAU708",
                                    this.TXT01_RRN1000.GetValue().ToString(),
                                    this.TXT01_RRN1010.GetValue().ToString(),
                                    this.TXT01_RRN1020.GetValue().ToString(),
                                    this.TXT01_RRN1030.GetValue().ToString(),
                                    this.TXT01_RRN1040.GetValue().ToString(),
                                    this.TXT01_RRN1070.GetValue().ToString(),
                                    this.TXT01_RRN1080.GetValue().ToString(),
                                    this.TXT01_RRN1090.GetValue().ToString(),
                                    this.TXT01_RRN1091.GetValue().ToString()
                                    );
                            }
                        }

                        if (int.Parse(fsYESAN_COUNT.ToString()) > 1) // 수정
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_MR_2BSAY709",
                                this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                                this.TXT01_RRN2040.GetValue().ToString().Substring(4, 2),
                                TYUserInfo.EmpNo.ToString(),
                                this.TXT01_RRN1000.GetValue().ToString(),
                                this.TXT01_RRN1010.GetValue().ToString(),
                                this.TXT01_RRN1020.GetValue().ToString(),
                                this.TXT01_RRN1030.GetValue().ToString(),
                                this.TXT01_RRN1040.GetValue().ToString(),
                                this.TXT01_RRN1070.GetValue().ToString(),
                                this.TXT01_RRN1080.GetValue().ToString(),
                                this.TXT01_RRN1090.GetValue().ToString(),
                                this.TXT01_RRN1091.GetValue().ToString()
                                );
                        }

                        this.DbConnector.ExecuteNonQuery();
                        this.ShowMessage("TY_M_GB_23NAD874");

                        // 버튼 초기화
                        UP_ImgbtnDisplay("3", false);

                        // 컨트롤 초기화
                        UP_Control_Initialize("MRPRRNF", false);

                        this.TXT01_RRN1040.SetValue("");
                        this.TXT01_RRN1070.SetValue("");

                        this.TXT01_RRN1080.SetValue("");
                        this.TXT01_RRN1080NM.SetValue("");
                        this.TXT01_RRN1090.SetValue("");
                        this.TXT01_RRN1090NM.SetValue("");
                        this.TXT01_RRN1091.SetValue("");
                        this.TXT01_RRN1092.SetValue("");
                        this.TXT01_RRN1050.SetValue("");
                        this.TXT01_RRN1050NM.SetValue("");

                        UP_FieldClear("MRPRRNF");

                        UP_MRPRRNF_DISPLAY();

                        SetFocus(this.TXT01_RRN1040);
                    }
                }
            }
        }
        #endregion

        #region Description : 특기사항 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 등록
            this.DataTableColumnAdd(ds.Tables[0], "RRT1000", this.TXT01_RRT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRT1010", this.TXT01_RRT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRT1020", this.TXT01_RRT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRT1030", this.TXT01_RRT1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRTHISAB", TYUserInfo.EmpNo);

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 수정
            this.DataTableColumnAdd(ds.Tables[1], "RRT1000", this.TXT01_RRT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRT1010", this.TXT01_RRT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRT1020", this.TXT01_RRT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRT1030", this.TXT01_RRT1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRTHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2BQ6G640", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_MR_2BQ6G642", ds.Tables[1]); //수정

            this.DbConnector.ExecuteNonQueryList();

            UP_MRPRRTF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장완료 메세지

        }
        #endregion

        #region Description : 특기사항 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 삭제
            this.DataTableColumnAdd(ds.Tables[0], "RRT1000", this.TXT01_RRT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRT1010", this.TXT01_RRT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRT1020", this.TXT01_RRT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRT1030", this.TXT01_RRT1030.GetValue().ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2BQ6G641", ds.Tables[0]); //삭제

            this.DbConnector.ExecuteNonQueryList();

            UP_MRPRRTF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제완료 메세지
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsRRM1000 = this.TXT01_RRM1000.GetValue().ToString();
            fsRRM1010 = this.TXT01_RRM1010.GetValue().ToString();
            fsRRM1020 = this.TXT01_RRM1020.GetValue().ToString();
            fsRRM1030 = this.TXT01_RRM1030.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 마스터 관련

        #region Description : 마스터 DISPLAY
        private void UP_MRPRRMF_DISPLAY()
        {
            UP_FieldClear("MRPRRMF");

            fsPOM5110 = "";

            DataTable dt = new DataTable();

            #region Description : 구매입고 마스터 내용 DISPLAY

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR3M673",
                this.TXT01_RRM1000.GetValue(),
                this.TXT01_RRM1010.GetValue(),
                this.TXT01_RRM1020.GetValue(),
                Set_Fill4(this.TXT01_RRM1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_POM1000.ReadOnly = true;
                this.TXT01_POM1010.ReadOnly = true;
                this.TXT01_POM1020.ReadOnly = true;
                this.TXT01_POM1030.ReadOnly = true;

                this.BTN61_POM10001.Enabled = false;
                //this.BTN61_POM10001.SetReadOnly(true);

                if (fsMAGAM.ToString() == "MAGAM")
                {
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("5", false);
                }
                else
                {
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("1", true);
                }

                // 탭 컨트롤
                tabControl1_Enable("MRPRRMF");

                this.CurrentDataTableRowMapping(dt, "01");

                // 예산 DISPLAY
                UP_MRPRROF_DISPLAY();
            }
            else
            {
                this.DTP01_RRM1100.SetValue(DateTime.Now.ToString("yyyyMMdd"));

                this.TXT01_POM1000.ReadOnly = false;
                //this.TXT01_POM1010.ReadOnly = false;
                this.TXT01_POM1020.ReadOnly = false;
                this.TXT01_POM1030.ReadOnly = false;

                this.BTN61_POM10001.Enabled = true;
                //this.BTN61_POM10001.SetReadOnly(false);
            }

            #endregion
        }
        #endregion

        #region Description : 예산 DISPLAY
        private void UP_MRPRROF_DISPLAY()
        {
            DataTable dt = new DataTable();

            #region Description : 구매발주 마스터(예산조회-귀속부서별)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR3S674",
                this.TXT01_RRM1000.GetValue(),
                this.TXT01_RRM1010.GetValue(),
                this.TXT01_RRM1020.GetValue(),
                this.TXT01_RRM1030.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2BR44676.SetValue(dt);

            #endregion

            #region Description : 구매발주 마스터(예산조회-거래처별)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR3X675",
                this.TXT01_RRM1000.GetValue(),
                this.TXT01_RRM1010.GetValue(),
                this.TXT01_RRM1020.GetValue(),
                this.TXT01_RRM1030.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2BR40677.SetValue(UP_MAST_RROF_SumRowds(dt));

                for (int i = 0; i < this.FPS91_TY_S_MR_2BR40677.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_MR_2BR40677.GetValue(i, "VNSANGHO").ToString() == "거래처별 소계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BR40677.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BR40677.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                    else if (this.FPS91_TY_S_MR_2BR40677.GetValue(i, "VNSANGHO").ToString() == "총   계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BR40677.ActiveSheet.Rows[i].ForeColor = Color.Red;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BR40677.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2BR40677.SetValue(dt);
            }

            #endregion
        }
        #endregion

        #region Description : 구매입고 마스터(예산조회-거래처별 합계)
        private DataTable UP_MAST_RROF_SumRowds(DataTable dt)
        {
            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt;

            string sMEKEY = "";

            double dRRN1230 = 0; // 금액

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;


            for (i = 1; i < nNum; i++)
            {
                /* Row i 번째와 Row i+1 번째의 거래처가 다를경우 빈로우를 생성하고
                 * 거래처별 금액 소계를 낸다.                                     */
                if (table.Rows[i - 1]["RRN1100"].ToString() != table.Rows[i]["RRN1100"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합계 이름 넣기
                    table.Rows[i]["VNSANGHO"] = "거래처별 소계";

                    // 거래처별
                    sMEKEY = " RRN1100 = '" + table.Rows[i - 1]["RRN1100"].ToString() + "' " ;

                    // 금액
                    table.Rows[i]["RRN1230"] = table.Compute("SUM(RRN1230)", sMEKEY).ToString();
                    // 금액합계
                    dRRN1230 += Convert.ToDouble(table.Compute("SUM(RRN1230)", sMEKEY).ToString());

                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 합계 이름 넣기
            table.Rows[i]["VNSANGHO"] = "거래처별 소계";

            // 거래처별
            sMEKEY = " RRN1100 = '" + table.Rows[i - 1]["RRN1100"].ToString() + "' " ;

            // 금액
            table.Rows[i]["RRN1230"] = table.Compute("SUM(RRN1230)", sMEKEY).ToString();
            // 금액합계
            dRRN1230 += Convert.ToDouble(table.Compute("SUM(RRN1230)", sMEKEY).ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);

            // 총계 이름 넣기
            table.Rows[i + 1]["VNSANGHO"] = "총   계";
            table.Rows[i + 1]["RRN1230"] = Convert.ToString(dRRN1230);

            return table;
        }
        #endregion

        #region Description : 그룹웨어 문서 바로가기
        private void BTN61_GW_Click(object sender, EventArgs e)
        {
            if (this.TXT01_RRM1460.GetValue().ToString() != "")
            {
                if ((new TYMRPR005S(this.TXT01_RRM1460.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_2BC51262");
                return;
            }
        }
        #endregion

        //#region Description : 발주 사번
        //private void CBH01_POM1140_TextChanged(object sender, EventArgs e)
        //{
        //    if (this.CBH01_RRM1420.GetValue().ToString().Length >= 6)
        //    {
        //        DataTable dt = new DataTable();

        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_MR_2BEBB293",
        //            DateTime.Now.ToString("yyyyMMdd"),
        //            this.CBH01_RRM1420.GetValue().ToString()
        //            );

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            // 부서코드
        //            this.TXT01_POM1130.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
        //            // 부서명
        //            this.TXT01_DTDESC.SetValue(dt.Rows[0]["KBBUSEONM"].ToString());
        //        }
        //    }
        //}
        //#endregion

        #region Description : 입고 일자 이벤트
        private void DTP01_RRM1100_ValueChanged(object sender, EventArgs e)
        {
            string sDATE = this.DTP01_RRM1100.GetString("yyyyMMdd");

            // 검수 부서
            this.CBH01_RRM1410.DummyValue = this.DTP01_RRM1100.GetString("yyyyMMdd");
        }
        #endregion

        #endregion

        #region Description : 내역사항 관련

        #region Description : 내역사항 확인
        private void UP_MRPRRNF_RUN()
        {
            UP_FieldClear("MRPRRNF");

            DataTable dt = new DataTable();

            #region Description : 구매입고 내역 내용 확인

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR8P696",
                this.TXT01_PON1000.GetValue(),
                this.TXT01_PON1010.GetValue(),
                this.TXT01_PON1020.GetValue(),
                Set_Fill4(this.TXT01_PON1030.GetValue().ToString()),
                this.TXT01_RRN1000.GetValue(),
                this.TXT01_RRN1010.GetValue(),
                this.TXT01_RRN1020.GetValue(),
                Set_Fill4(this.TXT01_RRN1030.GetValue().ToString()),
                this.TXT01_RRN1040.GetValue(),
                this.TXT01_RRN1050.GetValue(),
                this.TXT01_RRN1070.GetValue(),
                this.TXT01_RRN1080.GetValue(),
                this.TXT01_RRN1090.GetValue(),
                this.TXT01_RRN1091.GetValue(),
                this.TXT01_RRN1092.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "MRPRRNF";

                // 버튼 컨트롤
                UP_ImgbtnDisplay("1", true);
            }

            #endregion

            // 검수구분 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BS3V730",
                this.TXT01_PON1000.GetValue().ToString(),
                this.TXT01_PON1010.GetValue().ToString(),
                this.TXT01_PON1020.GetValue().ToString(),
                this.TXT01_PON1030.GetValue().ToString(),
                this.TXT01_RRN1040.GetValue().ToString(),
                this.TXT01_RRN1050.GetValue().ToString(),
                this.TXT01_RRN1070.GetValue().ToString(),
                this.TXT01_RRN1080.GetValue().ToString(),
                this.TXT01_RRN1090.GetValue().ToString(),
                this.TXT01_RRN1091.GetValue().ToString(),
                this.TXT01_RRN1092.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {                
                // 검수구분
                if (dt.Rows[0]["PON1120"].ToString() == "1") // 수량
                {
                    // 입고단가
                    this.TXT01_RRN1210.ReadOnly = true;
                    //this.TXT01_RRN1210.BackColor = Color.Silver;
                    this.TXT01_RRN1220.ReadOnly = true;
                    this.SetFocus(this.TXT01_RRN1200);
                }
                else // 금액
                {
                    // 입고수량
                    this.TXT01_RRN1200.ReadOnly = true;
                    //this.TXT01_RRN1200.BackColor = Color.Silver;
                    this.TXT01_RRN1220.ReadOnly = false;

                    this.SetFocus(this.TXT01_RRN1210);
                }
            }

            //// 요청잔량, 요청 잔액 가져오기
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_2BM7E593",
            //    this.TXT01_PON1000.GetValue(),
            //    this.TXT01_PON1010.GetValue(),
            //    this.TXT01_PON1020.GetValue(),
            //    Set_Fill4(this.TXT01_PON1030.GetValue().ToString()),
            //    this.TXT01_RRN1040.GetValue(),
            //    this.TXT01_RRN1050.GetValue(),
            //    this.TXT01_RRN1070.GetValue(),
            //    this.TXT01_RRN1080.GetValue(),
            //    this.TXT01_RRN1090.GetValue(),
            //    this.TXT01_RRN1091.GetValue(),
            //    this.TXT01_RRN1092.GetValue()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    //this.TXT01_PRN1150.SetValue(dt.Rows[0]["PRN1150"].ToString()); // 요청수량
            //    //this.TXT01_PRN1170.SetValue(dt.Rows[0]["PRN1170"].ToString()); // 요청금액
            //    //this.TXT01_PRN3020.SetValue(dt.Rows[0]["PRN3020"].ToString()); // 발주잔량
            //    //this.TXT01_PRN3070.SetValue(dt.Rows[0]["PRN3070"].ToString()); // 발주잔액
            //}
        }
        #endregion

        #region Description : 내역사항 DISPLAY
        private void UP_MRPRRNF_DISPLAY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR4B678",
                this.TXT01_RRN1000.GetValue(),
                this.TXT01_RRN1010.GetValue(),
                this.TXT01_RRN1020.GetValue(),
                Set_Fill4(this.TXT01_RRN1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DataTable retDt = new DataTable();

                retDt = UP_MRPRRNF_SumRowds(dt);

                this.FPS91_TY_S_MR_2BR4D679.SetValue(retDt);

                for (int i = 0; i < retDt.Rows.Count; i++)
                {
                    if (this.FPS91_TY_S_MR_2BR4D679.GetValue(i, "YSDESC").ToString() == "예산별 소계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_43VDM075.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BR4D679.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BR4D679.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                    else if (this.FPS91_TY_S_MR_2BR4D679.GetValue(i, "YSDESC").ToString() == "예산별 합계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_43VDM075.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BR4D679.ActiveSheet.Rows[i].ForeColor = Color.Red;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BR4D679.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2BR4D679.SetValue(dt);
            }
        }
        #endregion

        #region Description : 구매발주 내역사항(예산별 합계)
        private DataTable UP_MRPRRNF_SumRowds(DataTable dt)
        {
            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt;

            string sMEKEY = "";

            double dRRN1200 = 0; // 수량
            double dRRN1230 = 0; // 금액

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;


            for (i = 1; i < nNum; i++)
            {
                /* Row i 번째와 Row i+1 번째의 귀속부서, 예산, 계정 비품, 순번이 다를경우 빈로우를 생성하고
                 * 예산별 수량, 금액 소계를 낸다.                                                          */
                if (table.Rows[i - 1]["RRN1040"].ToString() != table.Rows[i]["RRN1040"].ToString() ||
                     table.Rows[i - 1]["RRN1070"].ToString() != table.Rows[i]["RRN1070"].ToString() ||
                     table.Rows[i - 1]["RRN1080"].ToString() != table.Rows[i]["RRN1080"].ToString() ||
                     table.Rows[i - 1]["RRN1090"].ToString() != table.Rows[i]["RRN1090"].ToString() ||
                     table.Rows[i - 1]["RRN1091"].ToString() != table.Rows[i]["RRN1091"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);
                    // 합 계 이름 넣기
                    table.Rows[i]["YSDESC"] = "예산별 소계";

                    // 예산별
                    sMEKEY = " RRN1040 = '" + table.Rows[i - 1]["RRN1040"].ToString() + "'      ";
                    sMEKEY += " AND RRN1070 = '" + table.Rows[i - 1]["RRN1070"].ToString() + "' ";
                    sMEKEY += " AND RRN1080 = '" + table.Rows[i - 1]["RRN1080"].ToString() + "' ";
                    sMEKEY += " AND RRN1090 = '" + table.Rows[i - 1]["RRN1090"].ToString() + "' ";
                    sMEKEY += " AND RRN1091 =  " + table.Rows[i - 1]["RRN1091"].ToString() + "  ";

                    // 수량
                    table.Rows[i]["RRN1200"] = table.Compute("SUM(RRN1200)", sMEKEY).ToString();
                    // 금액
                    table.Rows[i]["RRN1230"] = table.Compute("SUM(RRN1230)", sMEKEY).ToString();

                    // 수량합계
                    dRRN1200 += Convert.ToDouble(table.Compute("SUM(RRN1200)", sMEKEY).ToString());
                    // 금액합계
                    dRRN1230 += Convert.ToDouble(table.Compute("SUM(RRN1230)", sMEKEY).ToString());

                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            table.Rows[i]["YSDESC"] = "예산별 소계";

            // 예산별
            sMEKEY = " RRN1040 = '" + table.Rows[i - 1]["RRN1040"].ToString() + "'      ";
            sMEKEY += " AND RRN1070 = '" + table.Rows[i - 1]["RRN1070"].ToString() + "' ";
            sMEKEY += " AND RRN1080 = '" + table.Rows[i - 1]["RRN1080"].ToString() + "' ";
            sMEKEY += " AND RRN1090 = '" + table.Rows[i - 1]["RRN1090"].ToString() + "' ";
            sMEKEY += " AND RRN1091 =  " + table.Rows[i - 1]["RRN1091"].ToString() + "  ";

            // 수량
            table.Rows[i]["RRN1200"] = table.Compute("SUM(RRN1200)", sMEKEY).ToString();
            // 금액
            table.Rows[i]["RRN1230"] = table.Compute("SUM(RRN1230)", sMEKEY).ToString();

            // 수량합계
            dRRN1200 += Convert.ToDouble(table.Compute("SUM(RRN1200)", sMEKEY).ToString());
            // 금액합계
            dRRN1230 += Convert.ToDouble(table.Compute("SUM(RRN1230)", sMEKEY).ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);
            // 합 계 이름 넣기
            table.Rows[i + 1]["YSDESC"] = "예산별 합계";

            table.Rows[i + 1]["RRN1200"] = Convert.ToString(dRRN1200);
            table.Rows[i + 1]["RRN1230"] = Convert.ToString(dRRN1230);

            return table;
        }
        #endregion

        //#region Description : 품목 코드
        //private void TXT01_PON1050_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON10501_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 품목 코드 버튼
        //private void BTN61_PON10501_Click(object sender, EventArgs e)
        //{
        //    // 품목코드 코드헬프
        //    TYMRGB003S popup = new TYMRGB003S();

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.TXT01_RRN1050.SetValue(popup.fsJEPUM);
        //        this.TXT01_RRN1050NM.SetValue(popup.fsZ105013);

        //        SetFocus(this.CBH01_RRN1100.CodeText);
        //    }
        //}
        //#endregion

        //#region Description : 적용계정
        //private void TXT01_PON1070_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON10701_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 적용계정 버튼
        //private void BTN61_PON10701_Click(object sender, EventArgs e)
        //{
        //    if (this.TXT01_RRN1070.GetValue().ToString() == "1") // 기타세목예산(투자&수선) 코드헬프
        //    {
        //        TYMRGB005S popup = new TYMRGB005S(this.TXT01_RRN1020.GetValue().ToString().Substring(0, 4), this.TXT01_RRN1040.GetValue().ToString(), this.TXT01_RRN1040.GetValue().ToString());

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            // 요청년월의 본예산, 집행예산 금액 가져옴
        //            this.TXT01_RRN1080.SetValue(popup.fsCDAC);     // 계정과목
        //            this.TXT01_RRN1080NM.SetValue(popup.fsCDACNM); // 계정과목명
        //            this.TXT01_RRN1091.SetValue(popup.fsSEQ);      // 순번

        //            SetFocus(this.TXT01_RRN1050);
        //        }
        //    }
        //    else if (this.TXT01_RRN1070.GetValue().ToString() == "2") // 소모품비 예산세목 코드헬프
        //    {
        //        TYMRGB006S popup = new TYMRGB006S(this.TXT01_RRN1020.GetValue().ToString().Substring(0, 4), this.TXT01_RRN1040.GetValue().ToString(), this.TXT01_RRN1040.GetValue().ToString());

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            // 요청년월의 본예산, 집행예산 금액 가져옴
        //            this.TXT01_RRN1080.SetValue(popup.fsCDAC);     // 계정과목
        //            this.TXT01_RRN1080NM.SetValue(popup.fsCDACNM); // 계정과목명
        //            this.TXT01_RRN1090.SetValue(popup.fsCDJJ);     // 비품코드
        //            this.TXT01_RRN1090NM.SetValue(popup.fsBPDESC); // 비품명
        //            this.TXT01_RRN1091.SetValue(popup.fsSEQ);      // 순번

        //            SetFocus(this.TXT01_RRN1050);
        //        }
        //    }
        //    else if (this.TXT01_RRN1070.GetValue().ToString() == "3") // 기타예산 코드헬프
        //    {
        //        TYMRGB007S popup = new TYMRGB007S(this.TXT01_RRN1020.GetValue().ToString().Substring(0, 4), this.TXT01_RRN1040.GetValue().ToString(), this.TXT01_RRN1040.GetValue().ToString());

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            // 요청년월의 본예산, 집행예산 금액 가져옴
        //            this.TXT01_RRN1080.SetValue(popup.fsCDAC);     // 계정과목
        //            this.TXT01_RRN1080NM.SetValue(popup.fsCDACNM); // 계정과목명

        //            SetFocus(this.TXT01_RRN1050);
        //        }
        //    }
        //}
        //#endregion

        #region Description : 내역사항 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2BR4D679_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            bool fResult;

            if (this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1040").ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");
            }
            else
            {
                this.TXT01_RRN1040.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1040").ToString());
                this.TXT01_RRN1040NM.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("DTDESC").ToString());
                this.TXT01_RRN1070.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1070").ToString());
                this.TXT01_RRN1070NM.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("YSDESC").ToString());
                this.TXT01_RRN1080.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1080").ToString());
                this.TXT01_RRN1080NM.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("A1NMAC").ToString());
                this.TXT01_RRN1090.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1090").ToString());
                this.TXT01_RRN1090NM.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("BPDESC").ToString());
                this.TXT01_RRN1091.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1091").ToString());
                this.TXT01_RRN1092.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1092").ToString());
                this.TXT01_RRN1050.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("RRN1050").ToString());
                this.TXT01_RRN1050NM.SetValue(this.FPS91_TY_S_MR_2BR4D679.GetValue("Z105013").ToString());

                // 컨트롤 초기화
                UP_Control_Initialize("MRPRRNF", true);

                e.Cancel = true;

                UP_MRPRRNF_RUN();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 마스터 데이터가 존재하므로 
                    // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                    UP_ImgbtnDisplay("1", true);

                    //this.SetFocus(this.TXT01_RRN1200);
                }
                else
                {
                    if (TYUserInfo.EmpNo.ToString() == "0377-M")
                    {
                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("5", false);
                    }
                    else
                    {
                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("3", false);
                    }
                }

                // 노무비닷컴 체크
                Nomubi_Check("RUN");
            }
        }
        #endregion

        #endregion

        #region Description : 특기사항 관련

        #region Description : 특기사항 DISPLAY
        private void UP_MRPRRTF_DISPLAY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BQ6H643",
                this.TXT01_RRT1000.GetValue(),
                this.TXT01_RRT1010.GetValue(),
                this.TXT01_RRT1020.GetValue(),
                Set_Fill4(this.TXT01_RRT1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2BQ6E639.SetValue(dt);
        }
        #endregion

        #endregion


        #region Description : 입고자산 내역관리 관련

        #region Description : 입고자산 내역관리 저장 버튼
        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 등록
            this.DataTableColumnAdd(ds.Tables[0], "RRA1000", this.TXT01_RRA1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1010", this.TXT01_RRA1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1020", this.TXT01_RRA1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1030", this.TXT01_RRA1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1040", this.TXT01_RRA1040.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1050", this.TXT01_RRA1050.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1070", this.TXT01_RRA1070.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1080", this.TXT01_RRA1080.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1090", this.TXT01_RRA1090.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1091", this.TXT01_RRA1091.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1092", this.TXT01_RRA1092.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRAHISAB", TYUserInfo.EmpNo);

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 수정
            this.DataTableColumnAdd(ds.Tables[1], "RRA1000", this.TXT01_RRA1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1010", this.TXT01_RRA1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1020", this.TXT01_RRA1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1030", this.TXT01_RRA1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1040", this.TXT01_RRA1040.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1050", this.TXT01_RRA1050.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1070", this.TXT01_RRA1070.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1080", this.TXT01_RRA1080.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1090", this.TXT01_RRA1090.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1091", this.TXT01_RRA1091.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRA1092", this.TXT01_RRA1092.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "RRAHISAB", TYUserInfo.EmpNo);

            int i = 0;

            this.DbConnector.CommandClear();

            // 저장
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_MR_43VG2083", ds.Tables[0].Rows[i]["RRA1000"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1010"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1020"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1030"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1040"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1050"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1070"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1080"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1090"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1091"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1092"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1100"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1200"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1210"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1220"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1230"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1240"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1250"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1260"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1270"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRA1280"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_MR_43VG2084", ds.Tables[1].Rows[i]["RRA1200"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1210"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1220"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1230"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1240"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1250"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1260"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1270"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1280"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["RRA1000"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1010"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1020"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1030"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1040"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1050"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1070"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1080"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1090"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1091"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1092"].ToString(),
                                                            ds.Tables[1].Rows[i]["RRA1100"].ToString()
                                                            );
            }

            //this.DbConnector.Attach("TY_P_MR_43VG2083", ds.Tables[0]); //저장
            //this.DbConnector.Attach("TY_P_MR_43VG2084", ds.Tables[1]); //수정

            this.DbConnector.ExecuteTranQueryList();

            #region Description : 입고승인부분

            /***************************************************************************************
            * 1. 입고 등록시 자산 예산이 없을 경우 'Y'
            *                자산 예산이 있을 경우 'N'
            * 
            * 2. 자산 예산이 존재할 경우
            *    입고자산 내역관리에서 입고수량만큼 자료를 입력한다.
            ***************************************************************************************/

            string sRRM7010 = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_4419K090",
                this.TXT01_RRM1000.GetValue().ToString(),
                this.TXT01_RRM1010.GetValue().ToString(),
                this.TXT01_RRM1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRM1030.GetValue().ToString()),
                "1",
                "12200"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sRRM7010 = "N";
            }
            else
            {
                sRRM7010 = "Y";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_4419A089",
                sRRM7010.ToString(),
                this.TXT01_RRM1000.GetValue().ToString(),
                this.TXT01_RRM1010.GetValue().ToString(),
                this.TXT01_RRM1020.GetValue().ToString(),
                this.TXT01_RRM1030.GetValue().ToString()
                );

            this.DbConnector.ExecuteNonQueryList();

            #endregion

            UP_MRPRRAF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장완료 메세지
        }
        #endregion

        #region Description : 입고자산 내역관리 삭제 버튼
        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 삭제
            this.DataTableColumnAdd(ds.Tables[0], "RRA1000", this.TXT01_RRA1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1010", this.TXT01_RRA1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1020", this.TXT01_RRA1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1030", this.TXT01_RRA1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1040", this.TXT01_RRA1040.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1050", this.TXT01_RRA1050.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1070", this.TXT01_RRA1070.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1080", this.TXT01_RRA1080.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1090", this.TXT01_RRA1090.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1091", this.TXT01_RRA1091.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "RRA1092", this.TXT01_RRA1092.GetValue().ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_43VHV086", ds.Tables[0]); //삭제

            // 입고마스터 승인구분 업데이트
            this.DbConnector.Attach("TY_P_MR_43VI7087", "N",
                                                        this.TXT01_RRA1000.GetValue().ToString(),
                                                        this.TXT01_RRA1010.GetValue().ToString(),
                                                        this.TXT01_RRA1020.GetValue().ToString(),
                                                        this.TXT01_RRA1030.GetValue().ToString());

            this.DbConnector.ExecuteNonQueryList();


            #region Description : 입고승인부분

            /***************************************************************************************
            * 1. 입고 등록시 자산 예산이 없을 경우 'Y'
            *                자산 예산이 있을 경우 'N'
            * 
            * 2. 자산 예산이 존재할 경우
            *    입고자산 내역관리에서 입고수량만큼 자료를 입력한다.
            ***************************************************************************************/

            string sRRM7010 = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_4419K090",
                this.TXT01_RRM1000.GetValue().ToString(),
                this.TXT01_RRM1010.GetValue().ToString(),
                this.TXT01_RRM1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRM1030.GetValue().ToString()),
                "1",
                "12200"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sRRM7010 = "N";
            }
            else
            {
                sRRM7010 = "Y";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_4419A089",
                sRRM7010.ToString(),
                this.TXT01_RRM1000.GetValue().ToString(),
                this.TXT01_RRM1010.GetValue().ToString(),
                this.TXT01_RRM1020.GetValue().ToString(),
                this.TXT01_RRM1030.GetValue().ToString()
                );

            this.DbConnector.ExecuteNonQueryList();

            #endregion

            UP_MRPRRAF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제완료 메세지
        }
        #endregion

        #region Description : 입고자산 내역사항 확인
        private void UP_MRPRRAF_RUN(string sRRA1100)
        {
            int i = 0;

            int iRRA1000 = int.Parse(UP_DotDelete(sRRA1100.ToString()));

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_43VE8076",
                this.TXT01_RRA1000.GetValue(),
                this.TXT01_RRA1010.GetValue(),
                this.TXT01_RRA1020.GetValue(),
                Set_Fill4(this.TXT01_RRA1030.GetValue().ToString()),
                this.TXT01_RRA1040.GetValue(),
                this.TXT01_RRA1050.GetValue(),
                this.TXT01_RRA1070.GetValue(),
                this.TXT01_RRA1080.GetValue(),
                this.TXT01_RRA1090.GetValue(),
                this.TXT01_RRA1091.GetValue(),
                this.TXT01_RRA1092.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_43VE9077.SetValue(dt);

                int iMAXNUM = int.Parse(Get_Numeric(dt.Rows[i]["MAXNUM"].ToString()));

                for (i = 0; i < iRRA1000; i++)
                {
                    if ((i + 1) <= iMAXNUM)
                    {
                        if ((i + 1) != int.Parse(Get_Numeric(this.FPS91_TY_S_MR_43VE9077.GetValue(i, "RRA1100").ToString())))
                        {
                            this.FPS91_TY_S_MR_43VE9077.ActiveSheet.AddRows(i, 1);
                            this.FPS91_TY_S_MR_43VE9077.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1100", Convert.ToString(i + 1));
                        }
                    }
                    else
                    {
                        this.FPS91_TY_S_MR_43VE9077.ActiveSheet.AddRows(i, 1);
                        this.FPS91_TY_S_MR_43VE9077.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                        this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1100", Convert.ToString(i + 1));
                    }
                }
            }
            else
            {
                // 수량만큼 자산순번이 생성 됨
                this.FPS91_TY_S_MR_43VE9077.SetValue(dt);

                this.FPS91_TY_S_MR_43VE9077.ActiveSheet.AddRows(0, iRRA1000);

                for (i = 0; i < iRRA1000; i++)
                {
                    this.FPS91_TY_S_MR_43VE9077.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";

                    this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1100", Convert.ToString(i+1));
                }
            }

            for (i = 0; i < FPS91_TY_S_MR_43VE9077.CurrentRowCount; i++)
            {
                string year = this.TXT01_RRA1020.GetValue().ToString() + "01";
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_MR_43VE9077, "RRA1280");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = year;
            }
        }
        #endregion

        #region Description : 입고자산 내역 DISPLAY
        private void UP_MRPRRAF_DISPLAY()
        {
            this.FPS91_TY_S_MR_43VE9077.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_43VDM074",
                this.TXT01_RRA1000.GetValue(),
                this.TXT01_RRA1010.GetValue(),
                this.TXT01_RRA1020.GetValue(),
                Set_Fill4(this.TXT01_RRA1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_43VDM075.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                this.BTN63_SAV.Visible = true;
                this.BTN63_REM.Visible = true;
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                this.BTN63_SAV.Visible = false;
                this.BTN63_REM.Visible = false;
            }


            // 원본소스
            //this.FPS91_TY_S_MR_43VE9077.Initialize();

            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_43VDK073",
            //    this.TXT01_RRA1000.GetValue(),
            //    this.TXT01_RRA1010.GetValue(),
            //    this.TXT01_RRA1020.GetValue(),
            //    Set_Fill4(this.TXT01_RRA1030.GetValue().ToString())
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.FPS91_TY_S_MR_43VDM075.SetValue(dt);

            //    this.BTN61_SAV.Visible  = false;
            //    this.BTN61_EDIT.Visible = false;
            //    this.BTN61_REM.Visible  = false;

            //    this.BTN62_SAV.Visible  = false;
            //    this.BTN62_REM.Visible  = false;

            //    this.BTN63_SAV.Visible  = true;
            //    this.BTN63_REM.Visible  = true;
            //}
            //else
            //{
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach
            //        (
            //        "TY_P_MR_43VDM074",
            //        this.TXT01_RRA1000.GetValue(),
            //        this.TXT01_RRA1010.GetValue(),
            //        this.TXT01_RRA1020.GetValue(),
            //        Set_Fill4(this.TXT01_RRA1030.GetValue().ToString())
            //        );

            //    dt = this.DbConnector.ExecuteDataTable();

            //    this.FPS91_TY_S_MR_43VDM075.SetValue(dt);

            //    if (dt.Rows.Count > 0)
            //    {
            //        this.BTN61_SAV.Visible  = false;
            //        this.BTN61_EDIT.Visible = false;
            //        this.BTN61_REM.Visible  = false;

            //        this.BTN62_SAV.Visible  = false;
            //        this.BTN62_REM.Visible  = false;

            //        this.BTN63_SAV.Visible  = true;
            //        this.BTN63_REM.Visible = true;
            //    }
            //    else
            //    {
            //        this.BTN61_SAV.Visible  = false;
            //        this.BTN61_EDIT.Visible = false;
            //        this.BTN61_REM.Visible  = false;

            //        this.BTN62_SAV.Visible  = false;
            //        this.BTN62_REM.Visible  = false;

            //        this.BTN63_SAV.Visible  = false;
            //        this.BTN63_REM.Visible  = false;
            //    }
            //}
        }
        #endregion

        #region Description : 입고자산 내역 조회 스프레드 이벤트
        private void FPS91_TY_S_MR_43VDM075_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_RRA1040.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1040").ToString());
            this.TXT01_RRA1040NM.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("DTDESC").ToString());
            this.TXT01_RRA1070.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1070").ToString());
            this.TXT01_RRA1070NM.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("YSDESC").ToString());
            this.TXT01_RRA1080.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1080").ToString());
            this.TXT01_RRA1080NM.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("A1NMAC").ToString());
            this.TXT01_RRA1090.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1090").ToString());
            this.TXT01_RRA1090NM.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("BPDESC").ToString());
            this.TXT01_RRA1091.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1091").ToString());
            this.TXT01_RRA1050.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1050").ToString());
            this.TXT01_RRA1050NM.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("Z105013").ToString());
            this.TXT01_RRA1092.SetValue(this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1092").ToString());

            string sRRA1100 = this.FPS91_TY_S_MR_43VDM075.GetValue("RRA1100").ToString();

            e.Cancel = true;

            UP_MRPRRAF_RUN(sRRA1100);
        }
        #endregion

        #region Description : 입고자산 내역관리 스프레드 이벤트
        private void FPS91_TY_S_MR_43VE9077_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Row == 0)
            {
                if (this.FPS91_TY_S_MR_43VE9077.ActiveSheet.RowCount > 1)
                {
                    for (int i = 1; i < this.FPS91_TY_S_MR_43VE9077.ActiveSheet.RowCount; i++)
                    {
                        // 사용부서
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "RRA1280").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1280", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "RRA1280").ToString());
                        }

                        // 사용부서명
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "DTDESC").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "DTDESC", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "DTDESC").ToString());
                        }

                        // 위치
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "RRA1200").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1200", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "RRA1200").ToString());
                        }

                        // 위치명
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "PODESC").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "PODESC", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "PODESC").ToString());
                        }

                        // 제조사
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "RRA1210").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1210", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "RRA1210").ToString());
                        }

                        // 사용용도
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "RRA1220").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1220", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "RRA1220").ToString());
                        }

                        // 보증기간S
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "RRA1230").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1230", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "RRA1230").ToString());
                        }

                        // 보증기간E
                        if (this.FPS91_TY_S_MR_43VE9077.GetValue(i, "RRA1240").ToString() == "")
                        {
                            this.FPS91_TY_S_MR_43VE9077.SetValue(i, "RRA1240", this.FPS91_TY_S_MR_43VE9077.GetValue(0, "RRA1240").ToString());
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Description : 구매입고 공통

        #region Description : FieldClear()
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN.ToString() == "MRPRRMF")
            {
                //// 검수사번 <- 등록 및 수정 체크에 넣음
                //this.CBH01_RRM1400.SetValue(TYUserInfo.EmpNo);
                //// 인수사번
                //this.CBH01_RRM1420.SetValue(TYUserInfo.EmpNo);

                //// 등록 시 요청부서의 앞자리 가져옴
                //this.TXT01_RRM1000.SetValue(this.CBH01_RRM1410.GetValue().ToString().Substring(0, 1));

                this.TXT01_POM1000.SetValue("");
                this.TXT01_POM1020.SetValue("");
                this.TXT01_POM1030.SetValue("");
                this.TXT01_POM1180.SetValue("");
                this.TXT01_POM1120.SetValue("");
                this.TXT01_POM1130.SetValue("");
                this.TXT01_DTDESC.SetValue("");
                this.TXT01_PRM2050.SetValue("");
                this.TXT01_PRM2010.SetValue("");
                this.TXT01_DTDESC1.SetValue("");
                this.TXT01_POM1720.SetValue("");
                this.TXT01_POM1160.SetValue("");
                this.TXT01_POM1150.SetValue("");

                this.DTP01_RRM1110.SetValue(DateTime.Now.ToString("yyyyMMdd"));

                this.CBH01_RRM1400.SetValue("");
                this.CBH01_RRM1410.SetValue("");
                this.CBH01_RRM1420.SetValue("");
                this.DTP01_RRM1430.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.TXT01_RRM1440.SetValue("");
                this.TXT01_KBHANGL1.SetValue("");
                this.TXT01_RRM1450.SetValue("");

                this.CBO01_RRM1500.SetValue("1");
                this.TXT01_RRM1510.SetValue("");
                this.TXT01_RRM1460.SetValue("");
            }
            else if (sGUBUN.ToString() == "MRPRRNF")
            {
                //this.TXT01_RRN1040.SetValue("");
                //this.TXT01_RRN1070.SetValue("");
                //this.TXT01_PON1070.SetValue("");
                //this.TXT01_PON1070NM.SetValue("");
                //this.TXT01_PON1080.SetValue("");
                //this.TXT01_PON1080NM.SetValue("");
                //this.TXT01_PON1090.SetValue("");
                //this.TXT01_PON1050.SetValue("");
                //this.TXT01_PON1050NM.SetValue("");
                this.CBH01_RRN1100.SetValue("");
                this.CBH01_RRN1110.SetValue("");
                //this.CBO01_PON1500.SetValue("N");
                this.CBH01_RRN1120.SetValue("");
                this.TXT01_RRN5010.SetValue("N");
                this.TXT01_RRN1150.SetValue("");
                this.TXT01_RRN5030.SetValue("");
                //this.TXT01_PON1531.SetValue("");
                this.TXT01_RRN5030NM.SetValue("");
                this.TXT01_RRN6010.SetValue("");
                this.TXT01_RRN6010NM.SetValue("");
                this.TXT01_RRN6020.SetValue("");
                this.TXT01_RRN6020NM.SetValue("");
                this.TXT01_PON1150.SetValue("");
                this.TXT01_PON1160.SetValue("");
                this.TXT01_RRN1140.SetValue("");
                this.TXT01_RRN1230.SetValue("");
                this.TXT01_RRN1200.SetValue("");
                this.TXT01_RRN1210.SetValue("");
                this.TXT01_RRN1220.SetValue("");
                this.TXT01_RRN12201.SetValue("");
                this.TXT01_PON1230.SetValue("");
                this.TXT01_PON1330.SetValue("");
                this.TXT01_RRN1320.SetValue("");
                this.TXT01_RRN1330.SetValue("");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsYESAN_COUNT = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPRRMF") // 마스터
            {
                fsPRM5100 = "";
                fsPOM1400 = "";
                fsPOM1410 = "";
                fsPOM1420 = "0";
                fsPOM5110 = "";

                // 사업부
                if (this.TXT01_RRM1000.GetValue().ToString() != "A" && this.TXT01_RRM1000.GetValue().ToString() != "S" &&
                    this.TXT01_RRM1000.GetValue().ToString() != "T" && this.TXT01_RRM1000.GetValue().ToString() != "B" &&
                    this.TXT01_RRM1000.GetValue().ToString() != "C" && this.TXT01_RRM1000.GetValue().ToString() != "D" &&
                    this.TXT01_RRM1000.GetValue().ToString() != "E")
                {
                    this.ShowMessage("TY_M_MR_2BK3H504");

                    this.TXT01_RRM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 입고년월
                if (this.TXT01_RRM1020.GetValue().ToString().Length != 6)
                {
                    this.ShowMessage("TY_M_MR_2BK3G501");

                    this.TXT01_RRM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 입고월
                if (int.Parse(Get_Numeric(this.TXT01_RRM1020.GetValue().ToString().Substring(4, 2))) == 0 ||
                    int.Parse(Get_Numeric(this.TXT01_RRM1020.GetValue().ToString().Substring(4, 2))) > 12)
                {
                    this.ShowMessage("TY_M_MR_2CE2E192");

                    this.TXT01_RRM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 사업부
                if (this.TXT01_POM1000.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T645");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 년월
                if (this.TXT01_POM1020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T646");

                    this.TXT01_POM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 순번
                if (this.TXT01_POM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T647");

                    this.TXT01_POM1030.Focus();

                    e.Successed = false;
                    return;
                }

                // 입고 일자
                if (this.DTP01_RRM1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8Y651");

                    this.DTP01_RRM1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 마감 일자
                if (this.DTP01_RRM1110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8Y650");

                    this.DTP01_RRM1110.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수 사번
                if (this.CBH01_RRM1400.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8X649");

                    this.CBH01_RRM1400.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수 부서
                if (this.CBH01_RRM1410.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR93655");

                    this.CBH01_RRM1410.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 인수 사번
                if (this.CBH01_RRM1420.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR90652");

                    this.CBH01_RRM1420.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 인수 일자
                if (this.DTP01_RRM1430.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR90653");

                    this.DTP01_RRM1430.Focus();

                    e.Successed = false;
                    return;
                }
                

                if (this.TXT01_POM1000.GetValue().ToString() == "" ||
                    this.TXT01_POM1020.GetValue().ToString() == "" ||
                    this.TXT01_POM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T648");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 발주금액 -> 요청금액
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BR9A657",
                        this.TXT01_POM1000.GetValue().ToString(),
                        this.TXT01_POM1010.GetValue().ToString(),
                        this.TXT01_POM1020.GetValue().ToString(),
                        this.TXT01_POM1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 발주금액
                        this.TXT01_POM1720.SetValue(dt.Rows[0]["PON1330"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BR93656");

                        this.TXT01_POM1000.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                /****************************************
                 * 비용청구 = 'Y'일 경우에만
                 * 청구구분, 청구화주 필드에 값을 입력 함.
                 ****************************************/
                if (this.CBO01_RRM6010.GetValue().ToString() == "N")
                {
                    this.CBO01_RRM6020.SetValue("3");
                    this.CBH01_RRM6030.SetValue("");
                }
                else
                {
                    if (this.CBO01_RRM6020.GetValue().ToString() == "3")
                    {
                        this.ShowMessage("TY_M_MR_3352R235");

                        this.CBO01_RRM6020.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_RRM6030.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_3352R236");

                        this.CBH01_RRM6030.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 구매발주 내역파일 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BR9E658",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_MR_2BR9G661");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 구매발주 예산파일 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BR9F659",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_MR_2BR9G660");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BR9G663",
                    TXT01_RRM1000.GetValue().ToString(),
                    TXT01_RRM1010.GetValue().ToString(),
                    Get_Numeric(TXT01_RRM1020.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_RRM1030.SetValue(dt.Rows[0]["RRM1030"].ToString());
                }

                // 발주 내역사항 중 한 거래처에 노무비구분이 다른자료 유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_9CUEY628",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("동일 거래처에 노무비닷컴 구분이 다른 자료가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "MRPRRNF") // 내역사항
            {
                //#region Description : KeyCheck

                //// 귀속부서
                //if (this.TXT01_RRN1040.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA1381");

                //    this.TXT01_RRN1040.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 예산구분
                //if (this.TXT01_RRN1070.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA1380");

                //    this.TXT01_RRN1070.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 계정코드
                //if (this.TXT01_RRN1080.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA1379");

                //    this.TXT01_RRN1080.Focus();

                //    e.Successed = false;
                //    return;
                //}
                //else
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAF400",
                //        this.TXT01_RRN1080.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGAE399");

                //        this.TXT01_RRN1080.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        // 계정과목명
                //        this.TXT01_RRN1080NM.SetValue(dt.Rows[0]["A1NMAC"].ToString());
                //    }
                //}

                //// 예산 체크
                //if (this.TXT01_RRN1070.GetValue().ToString() == "1") // 투자예산
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAG401",
                //        this.TXT01_RRN2040.GetValue().ToString().Substring(0,4),
                //        this.TXT01_RRN1040.GetValue().ToString(),
                //        this.TXT01_RRN1080.GetValue().ToString(),
                //        this.TXT01_RRN1091.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA1378");

                //        this.TXT01_RRN1080.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
                //else if (this.TXT01_RRN1070.GetValue().ToString() == "2") // 소모성 예산
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAH402",
                //        this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                //        this.TXT01_RRN1040.GetValue().ToString(),
                //        this.TXT01_RRN1080.GetValue().ToString(),
                //        this.TXT01_RRN1090.GetValue().ToString(),
                //        this.TXT01_RRN1091.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA2390");

                //        this.TXT01_RRN1080.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
                //else if (this.TXT01_RRN1070.GetValue().ToString() == "3") // 기타본예산
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAI403",
                //        this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                //        this.TXT01_RRN1040.GetValue().ToString(),
                //        this.TXT01_RRN1080.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA2389");

                //        this.TXT01_RRN1080.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                //// 품목코드
                //if (this.TXT01_RRN1050.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA2388");

                //    this.TXT01_RRN1050.Focus();

                //    e.Successed = false;
                //    return;
                //}
                //else
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAJ404",
                //        this.TXT01_RRN1050.GetValue().ToString().Substring(0, 1),
                //        this.TXT01_RRN1050.GetValue().ToString().Substring(1, 3),
                //        this.TXT01_RRN1050.GetValue().ToString().Substring(4, 3),
                //        this.TXT01_RRN1050.GetValue().ToString().Substring(7, 5)
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA2387");

                //        this.TXT01_RRN1050.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        // 품목명
                //        this.TXT01_RRN1050NM.SetValue(dt.Rows[0]["Z105023"].ToString());
                //    }
                //}

                //#endregion




                //#region Description : 내용 체크

                //// 요청잔량, 요청 잔액 가져오기
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BM7E593",
                //    this.TXT01_PON1000.GetValue(),
                //    this.TXT01_PON1010.GetValue(),
                //    this.TXT01_PON1020.GetValue(),
                //    Set_Fill4(this.TXT01_PON1030.GetValue().ToString()),
                //    this.TXT01_RRN1040.GetValue(),
                //    this.TXT01_RRN1050.GetValue(),
                //    this.TXT01_RRN1070.GetValue(),
                //    this.TXT01_RRN1080.GetValue(),
                //    this.TXT01_RRN1090.GetValue(),
                //    this.TXT01_RRN1091.GetValue(),
                //    this.TXT01_RRN1092.GetValue()
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    //this.TXT01_PRN1150.SetValue(dt.Rows[0]["PRN1150"].ToString()); // 요청수량
                //    //this.TXT01_PRN1170.SetValue(dt.Rows[0]["PRN1170"].ToString()); // 요청금액
                //    //this.TXT01_PRN3020.SetValue(dt.Rows[0]["PRN3020"].ToString()); // 발주잔량
                //    //this.TXT01_PRN3070.SetValue(dt.Rows[0]["PRN3070"].ToString()); // 발주잔액
                //}
                //else
                //{
                //    this.ShowMessage("TY_M_MR_2BL71537");

                //    this.CBH01_RRN1100.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 거래처
                //if (this.CBH01_RRN1100.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA5395");

                //    this.CBH01_RRN1100.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 부가세 구분
                //if (this.CBH01_RRN1110.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA5393");

                //    this.CBH01_RRN1110.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 화폐
                //if (this.CBH01_RRN1120.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA5392");

                //    this.CBH01_RRN1120.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 발주 수량
                //if (double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())) == 0)
                //{
                //    this.ShowMessage("TY_M_MR_2BM7R596");

                //    this.TXT01_PON1150.Focus();

                //    e.Successed = false;
                //    return;
                //}
                //else
                //{
                //    // 검수구분 수량
                //    //if (this.CBO01_PON1120.GetValue().ToString() == "1")
                //    //{
                //    //    // 요청수량보다 발주 수량이 넘을 순 없다.
                //    //    if (double.Parse(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())))
                //    //    {
                //    //        this.ShowMessage("TY_M_MR_2BM7Y598");

                //    //        this.TXT01_PON1150.Focus();

                //    //        e.Successed = false;
                //    //        return;
                //    //    }
                //    //}
                //}

                //// 발주 단가
                //if (double.Parse(Get_Numeric(this.TXT01_PON1160.GetValue().ToString())) == 0)
                //{
                //    this.ShowMessage("TY_M_MR_2BM7R597");

                //    this.TXT01_PON1160.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 적용환율
                //if (this.CBH01_RRN1120.GetValue().ToString() != "1")
                //{
                //    if (double.Parse(Get_Numeric(this.TXT01_RRN1140.Text.Trim())) == 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGAA396");

                //        this.TXT01_RRN1140.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
                //else
                //{
                //    this.TXT01_RRN1140.SetValue("0");
                //}

                //if (this.TXT01_RRM1210.GetValue().ToString() == "" && Get_Numeric(this.TXT01_RRM1220.GetValue().ToString()) == "0")
                //{
                //    // 계약요청구분 'Y'인 경우
                //    // 계약을 하기 위한 요청일 경우 거래처는 한개만 등록이 되어야 함.

                //    if (this.fsPRM5100.ToString() == "Y")
                //    {
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach
                //            (
                //            "TY_P_MR_2BGAP406",
                //            this.TXT01_PON1000.GetValue().ToString(), // 사업부
                //            this.TXT01_PON1010.GetValue().ToString(), // 요청구분
                //            this.TXT01_PON1020.GetValue().ToString(), // 년월
                //            this.TXT01_PON1030.GetValue().ToString()  // 순번
                //            );

                //        dt = this.DbConnector.ExecuteDataTable();

                //        if (dt.Rows.Count > 0)
                //        {
                //            if (this.CBH01_RRN1100.GetValue().ToString() != dt.Rows[0]["PRN1100"].ToString())
                //            {
                //                this.ShowMessage("TY_M_MR_2BGA5394");

                //                this.CBH01_RRN1100.Focus();

                //                e.Successed = false;
                //                return;
                //            }
                //        }
                //    }
                //}

                //// 자산분류코드 유무체크 로직 넣어야 함.
                //// 비품번호 유무체크 로직 넣어야 함.
                //// 자산번호 유무체크 로직 넣어야 함.

                //// 1. 비품여부 = 'Y'인 경우는 비품을 신규 구매할 경우에만 입력 함.

                //// 2. 비품번호가 존재할 경우
                ////    비품여부 = 'N', 자산계정은 올 수 없음.

                ///**************************************************************************************************
                // *   비품번호   자산번호   자산계정   비품여부(Y)   자산분류코드
                // *1)  없음(X)    없음(X)    Y(자산)      (N)         사용자 필수입력
                // *2)  없음(X)    있음(O)    N(아님)      (N)         조회시 가져오게 함, 등록 및 수정시 강제로 값 넣음
                // *3)  없음(X)    없음(X)    N(아님)      (Y)         사용자 필수입력
                // *4)  있음(O)    없음(X)    N(아님)      (N)         조회시 가져오게 함, 등록 및 수정시 강제로 값 넣음(비품번호 있을 경우 수선임. 이때는 비품여부 N임)
                // *5)  없음(X)    없음(X)    N(아님)      (N)         공백       
                // **************************************************************************************************/

                //if (this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) == "12200")
                //{
                //    // 자산번호
                //    if ((this.TXT01_RRN6010.GetValue().ToString() == "") &&
                //        // 자산분류코드
                //        (this.TXT01_RRN6020.GetValue().ToString() == "")
                //        )
                //    {
                //        this.ShowMessage("TY_M_MR_2BGCZ407");

                //        this.TXT01_RRN6020.Focus();

                //        e.Successed = false;
                //        return;
                //    }

                //    // 신규구매건일 경우
                //    // 자산계정이면서 자산번호가 공백이고 비품구분 = Y 이면 등록 안됨
                //    if ((this.TXT01_RRN6010.GetValue().ToString() == "") &&
                //        // 비품구분
                //        this.TXT01_RRN5010.GetValue().ToString() == "Y")
                //    {
                //        this.ShowMessage("TY_M_MR_2BM5T588");

                //        this.TXT01_RRN5010.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                //// 1) 자산분류코드 체크
                //if ((this.TXT01_RRN5030.GetValue().ToString() == "") && // 비품번호
                //    (this.TXT01_RRN6010.GetValue().ToString() == "") && // 자산번호
                //    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) == "12200" && // 자산계정
                //    this.TXT01_RRN5010.GetValue().ToString() == "N")                       // 비품구분
                //{
                //    // 자산분류코드
                //    if (this.TXT01_RRN6020.GetValue().ToString() == "")
                //    {
                //        this.ShowMessage("TY_M_MR_2BGCZ407");

                //        this.TXT01_RRN6020.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        // 자산분류코드 존재 유무 체크
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach
                //            (
                //            "TY_P_MR_2BM4T579",
                //            this.TXT01_RRN6020.GetValue().ToString()
                //            //this.TXT01_PON1621.GetValue().ToString(),
                //            //this.TXT01_PON1622.GetValue().ToString(),
                //            //this.TXT01_PON1623.GetValue().ToString()
                //            );

                //        dt = this.DbConnector.ExecuteDataTable();

                //        if (dt.Rows.Count > 0)
                //        {
                //            // 자산분류명
                //            this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                //        }
                //        else
                //        {
                //            this.ShowMessage("TY_M_MR_2BM51580");

                //            this.TXT01_RRN6020.Focus();

                //            e.Successed = false;
                //            return;
                //        }
                //    }
                //}

                //// 2) 자산분류코드 체크
                //if ((this.TXT01_RRN5030.GetValue().ToString() == "") && // 비품번호
                //    (this.TXT01_RRN6010.GetValue().ToString() != "") && // 자산번호
                //    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                //    this.TXT01_RRN5010.GetValue().ToString() == "N")                       // 비품구분
                //{
                //    // 고정자산번호에 따른 분류코드를 가져옴.
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BM5E581",
                //        this.TXT01_RRN6010.GetValue().ToString()
                //        //this.TXT01_PON1611.GetValue().ToString(),
                //        //this.TXT01_PON1611.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count > 0)
                //    {
                //        this.TXT01_RRN6020.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(0, 1).ToString());
                //        //this.TXT01_PON1621.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(1, 1).ToString());
                //        //this.TXT01_PON1622.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(2, 3).ToString());
                //        //this.TXT01_PON1623.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(5, 3).ToString());

                //        this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                //    }
                //    else
                //    {
                //        this.ShowMessage("TY_M_MR_2BM5J582");

                //        this.TXT01_RRN6020.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                //// 3) 자산분류코드 체크
                //if ((this.TXT01_RRN5030.GetValue().ToString() == "") && // 비품번호
                //    (this.TXT01_RRN6010.GetValue().ToString() == "") && // 자산번호
                //    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                //    this.TXT01_RRN5010.GetValue().ToString() == "Y")                       // 비품구분
                //{
                //    // 자산분류코드
                //    if (this.TXT01_RRN6020.GetValue().ToString() == "")
                //    {
                //        this.ShowMessage("TY_M_MR_2BGCZ407");

                //        this.TXT01_RRN6020.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        // 자산번호에 따른 분류코드를 가져옴.
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach
                //            (
                //            "TY_P_MR_2BM5N583"
                //            //this.TXT01_RRN5030.GetValue().ToString(),
                //            //this.TXT01_PON1531.GetValue().ToString()
                //            );

                //        dt = this.DbConnector.ExecuteDataTable();

                //        if (dt.Rows.Count > 0)
                //        {
                //            // 자산분류명
                //            this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                //        }
                //        else
                //        {
                //            this.ShowMessage("TY_M_MR_2BM5O584");

                //            this.TXT01_RRN5030.Focus();

                //            e.Successed = false;
                //            return;
                //        }
                //    }
                //}

                //// 4) 자산분류코드 체크
                //if ((this.TXT01_RRN5030.GetValue().ToString() != "") && // 비품번호
                //    (this.TXT01_RRN6010.GetValue().ToString() == "") && // 자산번호
                //    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                //    this.TXT01_RRN5010.GetValue().ToString() == "Y")                       // 비품구분
                //{
                //    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BM5N583",
                //        this.TXT01_RRN6020.GetValue().ToString()
                //        //this.TXT01_PON1621.GetValue().ToString(),
                //        //this.TXT01_PON1622.GetValue().ToString(),
                //        //this.TXT01_PON1623.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count > 0)
                //    {
                //        this.TXT01_RRN6020.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(0, 1).ToString());
                //        //this.TXT01_PON1621.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(1, 1).ToString());
                //        //this.TXT01_PON1622.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(2, 3).ToString());
                //        //this.TXT01_PON1623.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(5, 3).ToString());

                //        this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                //    }
                //    else
                //    {
                //        this.ShowMessage("TY_M_MR_2BM51580");

                //        this.TXT01_RRN6020.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                //// 5) 자산분류코드 체크
                //if ((this.TXT01_RRN5030.GetValue().ToString() != "" ) && // 비품번호
                //    (this.TXT01_RRN6010.GetValue().ToString() == "" ) && // 자산번호
                //    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                //    this.TXT01_RRN5010.GetValue().ToString() == "N")                       // 비품구분
                //{
                //    this.TXT01_RRN6020.SetValue("");
                //}

                //decimal dPON1150 = 0;
                //decimal dPON1160 = 0;
                //decimal dPON1180 = 0;

                //// 발주수량
                //dPON1150 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1150.GetValue().ToString())));
                //// 발주단가
                //dPON1160 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1160.GetValue().ToString())));
                //// 적용환율
                //dPON1180 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1140.GetValue().ToString())));

                //// 발주금액
                //if (double.Parse(Get_Numeric(this.TXT01_RRN1140.GetValue().ToString())) == 0)
                //{
                //    this.TXT01_RRN1230.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPON1150 * dPON1160)));

                //    this.TXT01_RRN1230.SetValue(UP_DotDelete(this.TXT01_RRN1230.GetValue().ToString()));
                //}
                //else
                //{
                //    this.TXT01_RRN1230.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPON1150 * dPON1160 * dPON1180)));
                //}

                //if (this.TXT01_RRM1210.GetValue().ToString() == "" && Get_Numeric(this.TXT01_RRM1220.GetValue().ToString()) == "0")
                //{
                //    // 입고잔량 = 발주수량
                //    this.TXT01_PON1230.SetValue(Get_Numeric(this.TXT01_PON1150.GetValue().ToString()));
                //    // 입고잔액 = 발주금액
                //    this.TXT01_RRN1330.SetValue(Get_Numeric(this.TXT01_RRN1230.GetValue().ToString()));
                //}

                //// 검수구분 금액
                ////if (this.CBO01_PON1120.GetValue().ToString() == "2")
                ////{
                ////    // 남아 있는 발주잔액보다 발주 금액이 넘을 순 없다.
                ////    if (double.Parse(Get_Numeric(this.TXT01_PRN3070.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_RRN1230.GetValue().ToString())))
                ////    {
                ////        this.ShowMessage("TY_M_MR_2BM7Y599");

                ////        this.TXT01_PON1150.Focus();

                ////        e.Successed = false;
                ////        return;
                ////    }
                ////}

                //// 예산 카운트(삭제시 필요)
                //fsYESAN_COUNT = "0";

                //// 예산 존재 체크
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BM87600",
                //    this.TXT01_RRN1000.GetValue().ToString(),
                //    this.TXT01_RRN1010.GetValue().ToString(),
                //    Get_Numeric(this.TXT01_RRN1020.GetValue().ToString()),
                //    Set_Fill4(Get_Numeric(this.TXT01_RRN1030.GetValue().ToString())),
                //    this.TXT01_RRN1040.GetValue().ToString(),
                //    this.TXT01_RRN1070.GetValue().ToString(),
                //    this.TXT01_RRN1080.GetValue().ToString(),
                //    this.TXT01_RRN1090.GetValue().ToString(),
                //    Get_Numeric(this.TXT01_RRN1091.GetValue().ToString())
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                //}

                //#endregion
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR4M682",
                this.TXT01_RRM1000.GetValue().ToString(),
                this.TXT01_RRM1010.GetValue().ToString(),
                this.TXT01_RRM1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRM1030.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BC59266");
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

        #region Description : 수정 체크
        private void BTN61_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsYESAN_COUNT = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPRRMF") // 마스터
            {
                // 사업부
                if (this.TXT01_RRM1000.GetValue().ToString() != "A" && this.TXT01_RRM1000.GetValue().ToString() != "S" &&
                    this.TXT01_RRM1000.GetValue().ToString() != "T" && this.TXT01_RRM1000.GetValue().ToString() != "B" &&
                    this.TXT01_RRM1000.GetValue().ToString() != "C" && this.TXT01_RRM1000.GetValue().ToString() != "D" &&
                    this.TXT01_RRM1000.GetValue().ToString() != "E")
                {
                    this.ShowMessage("TY_M_MR_2BK3H504");

                    this.TXT01_RRM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 입고년월
                if (this.TXT01_RRM1020.GetValue().ToString().Length != 6)
                {
                    this.ShowMessage("TY_M_MR_2BK3G501");

                    this.TXT01_RRM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 사업부
                if (this.TXT01_POM1000.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T645");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 년월
                if (this.TXT01_POM1020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T646");

                    this.TXT01_POM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 순번
                if (this.TXT01_POM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T647");

                    this.TXT01_POM1030.Focus();

                    e.Successed = false;
                    return;
                }

                // 입고 일자
                if (this.DTP01_RRM1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8Y651");

                    this.DTP01_RRM1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 마감 일자
                if (this.DTP01_RRM1110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8Y650");

                    this.DTP01_RRM1110.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수 사번
                if (this.CBH01_RRM1400.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8X649");

                    this.CBH01_RRM1400.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수 부서
                if (this.CBH01_RRM1410.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR93655");

                    this.CBH01_RRM1410.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 인수 사번
                if (this.CBH01_RRM1420.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR90652");

                    this.CBH01_RRM1420.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 인수 일자
                if (this.DTP01_RRM1430.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR90653");

                    this.DTP01_RRM1430.Focus();

                    e.Successed = false;
                    return;
                }


                if (this.TXT01_POM1000.GetValue().ToString() == "" ||
                    this.TXT01_POM1020.GetValue().ToString() == "" ||
                    this.TXT01_POM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BR8T648");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 입고금액 -> 발주금액
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BR9A657",
                        this.TXT01_POM1000.GetValue().ToString(),
                        this.TXT01_POM1010.GetValue().ToString(),
                        this.TXT01_POM1020.GetValue().ToString(),
                        this.TXT01_POM1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 발주금액
                        this.TXT01_POM1720.SetValue(dt.Rows[0]["PON1330"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BR93656");

                        this.TXT01_POM1000.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                /****************************************
                 * 비용청구 = 'Y'일 경우에만
                 * 청구구분, 청구화주 필드에 값을 입력 함.
                 ****************************************/
                if (this.CBO01_RRM6010.GetValue().ToString() == "N")
                {
                    this.CBO01_RRM6020.SetValue("3");
                    this.CBH01_RRM6030.SetValue("");
                }
                else
                {
                    if (this.CBO01_RRM6020.GetValue().ToString() == "3")
                    {
                        this.ShowMessage("TY_M_MR_3352R235");

                        this.CBO01_RRM6020.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_RRM6030.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_3352R236");

                        this.CBH01_RRM6030.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }
            else if (fsGUBUN == "MRPRRNF") // 내역사항
            {
                #region Description : KeyCheck

                // 귀속부서
                if (this.TXT01_RRN1040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1381");

                    this.TXT01_RRN1040.Focus();

                    e.Successed = false;
                    return;
                }

                // 예산구분
                if (this.TXT01_RRN1070.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1380");

                    this.TXT01_RRN1070.Focus();

                    e.Successed = false;
                    return;
                }

                // 계정코드
                if (this.TXT01_RRN1080.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1379");

                    this.TXT01_RRN1080.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAF400",
                        this.TXT01_RRN1080.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAE399");

                        this.TXT01_RRN1080.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 계정과목명
                        this.TXT01_RRN1080NM.SetValue(dt.Rows[0]["A1NMAC"].ToString());
                    }
                }

                // 예산 체크
                if (this.TXT01_RRN1070.GetValue().ToString() == "1") // 투자예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAG401",
                        this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                        this.TXT01_RRN1040.GetValue().ToString(),
                        this.TXT01_RRN1080.GetValue().ToString(),
                        this.TXT01_RRN1091.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA1378");

                        this.TXT01_RRN1080.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.TXT01_RRN1070.GetValue().ToString() == "2") // 소모성 예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAH402",
                        this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                        this.TXT01_RRN1040.GetValue().ToString(),
                        this.TXT01_RRN1080.GetValue().ToString(),
                        this.TXT01_RRN1090.GetValue().ToString(),
                        this.TXT01_RRN1091.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2390");

                        this.TXT01_RRN1080.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.TXT01_RRN1070.GetValue().ToString() == "3") // 기타본예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAI403",
                        this.TXT01_RRN2040.GetValue().ToString().Substring(0, 4),
                        this.TXT01_RRN1040.GetValue().ToString(),
                        this.TXT01_RRN1080.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2389");

                        this.TXT01_RRN1080.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.TXT01_RRN1070.GetValue().ToString() == "4") // 선급자재
                {
                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    if (this.TXT01_RRN1080.GetValue().ToString() != "11101001" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                    {
                        this.ShowMessage("TY_M_MR_31L41841");

                        this.CBH01_RRN1100.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 품목코드
                if (this.TXT01_RRN1050.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA2388");

                    this.TXT01_RRN1050.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAJ404",
                        this.TXT01_RRN1050.GetValue().ToString().Substring(0, 1),
                        this.TXT01_RRN1050.GetValue().ToString().Substring(1, 3),
                        this.TXT01_RRN1050.GetValue().ToString().Substring(4, 3),
                        this.TXT01_RRN1050.GetValue().ToString().Substring(7, 5)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2387");

                        this.TXT01_RRN1050.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 품목명
                        this.TXT01_RRN1050NM.SetValue(dt.Rows[0]["Z105013"].ToString());
                    }
                }

                #endregion




                #region Description : 내용 체크

                //// 요청잔량, 요청 잔액 가져오기
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BM7E593",
                //    this.TXT01_PON1000.GetValue(),
                //    this.TXT01_PON1010.GetValue(),
                //    this.TXT01_PON1020.GetValue(),
                //    Set_Fill4(this.TXT01_PON1030.GetValue().ToString()),
                //    this.TXT01_RRN1040.GetValue(),
                //    this.TXT01_RRN1050.GetValue(),
                //    this.TXT01_RRN1070.GetValue(),
                //    this.TXT01_RRN1080.GetValue(),
                //    this.TXT01_RRN1090.GetValue(),
                //    this.TXT01_RRN1091.GetValue(),
                //    this.TXT01_RRN1092.GetValue()
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    //this.TXT01_PRN1150.SetValue(dt.Rows[0]["PRN1150"].ToString()); // 요청수량
                //    //this.TXT01_PRN1170.SetValue(dt.Rows[0]["PRN1170"].ToString()); // 요청금액
                //    //this.TXT01_PRN3020.SetValue(dt.Rows[0]["PRN3020"].ToString()); // 발주잔량
                //    //this.TXT01_PRN3070.SetValue(dt.Rows[0]["PRN3070"].ToString()); // 발주잔액
                //}
                //else
                //{
                //    this.ShowMessage("TY_M_MR_2BL71537");

                //    this.CBH01_RRN1100.Focus();

                //    e.Successed = false;
                //    return;
                //}

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_43VE8076",
                    this.TXT01_RRN1000.GetValue(),
                    this.TXT01_RRN1010.GetValue(),
                    this.TXT01_RRN1020.GetValue(),
                    Set_Fill4(this.TXT01_RRN1030.GetValue().ToString()),
                    this.TXT01_RRN1040.GetValue(),
                    this.TXT01_RRN1050.GetValue(),
                    this.TXT01_RRN1070.GetValue(),
                    this.TXT01_RRN1080.GetValue(),
                    this.TXT01_RRN1090.GetValue(),
                    this.TXT01_RRN1091.GetValue(),
                    this.TXT01_RRN1092.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (TYUserInfo.EmpNo.ToString() != "0377-M")
                    {
                        this.ShowMessage("TY_M_MR_43VHP085");
                        e.Successed = false;
                        return;
                    }
                }

                // 거래처
                if (this.CBH01_RRN1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5395");

                    this.CBH01_RRN1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 부가세 구분
                if (this.CBH01_RRN1110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5393");

                    this.CBH01_RRN1110.Focus();

                    e.Successed = false;
                    return;
                }

                // 화폐
                if (this.CBH01_RRN1120.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5392");

                    this.CBH01_RRN1120.Focus();

                    e.Successed = false;
                    return;
                }

                // 입고 수량
                if (double.Parse(Get_Numeric(this.TXT01_RRN1200.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BSBN717");

                    this.TXT01_RRN1200.Focus();

                    e.Successed = false;
                    return;
                }
                
                // 입고 단가
                if (double.Parse(Get_Numeric(this.TXT01_RRN1210.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BSBN716");

                    this.TXT01_RRN1210.Focus();

                    e.Successed = false;
                    return;
                }

                // 적용환율
                if (this.CBH01_RRN1120.GetValue().ToString() != "1") // 화폐
                {
                    // 적용환율
                    if (double.Parse(Get_Numeric(this.TXT01_RRN1140.Text.Trim())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAA396");

                        this.TXT01_RRN1140.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.TXT01_RRN1140.SetValue("0");
                }

                // 자산분류코드 유무체크 로직 넣어야 함.
                // 비품번호 유무체크 로직 넣어야 함.
                // 자산번호 유무체크 로직 넣어야 함.

                // 1. 비품여부 = 'Y'인 경우는 비품을 신규 구매할 경우에만 입력 함.

                // 2. 비품번호가 존재할 경우
                //    비품여부 = 'N', 자산계정은 올 수 없음.

                /**************************************************************************************************
                 *   비품번호   자산번호   자산계정   비품여부(Y)   자산분류코드
                 *1)  없음(X)    없음(X)    Y(자산)      (N)         사용자 필수입력
                 *2)  없음(X)    있음(O)    N(아님)      (N)         조회시 가져오게 함, 등록 및 수정시 강제로 값 넣음
                 *3)  없음(X)    없음(X)    N(아님)      (Y)         사용자 필수입력
                 *4)  있음(O)    없음(X)    N(아님)      (N)         조회시 가져오게 함, 등록 및 수정시 강제로 값 넣음(비품번호 있을 경우 수선임. 이때는 비품여부 N임)
                 *5)  없음(X)    없음(X)    N(아님)      (N)         공백       
                 **************************************************************************************************/

                if (this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                {
                    // 자산번호, 자산분류코드
                    if (this.TXT01_RRN6010.GetValue().ToString() == "" && this.TXT01_RRN6020.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.TXT01_RRN6020.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 신규구매건일 경우
                    // 자산계정이면서 자산번호가 공백이고 비품구분 = Y 이면 등록 안됨
                    if (this.TXT01_RRN6010.GetValue().ToString() == "" && this.TXT01_RRN5010.GetValue().ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2BM5T588");

                        this.TXT01_RRN5010.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 비품번호가 존재할 경우
                // 비품여부 = 'N', 자산계정은 올 수 없음.
                if (this.TXT01_RRN5030.GetValue().ToString() != "")
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_RRN5030.GetValue().ToString().Substring(0, 6),
                        this.TXT01_RRN5030.GetValue().ToString().Substring(6, 3)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BM5O584");
                        e.Successed = false;
                        return;
                    }

                    // 비품여부
                    this.TXT01_RRN5010.SetValue("N");

                    // 자산계정 체크
                    if (this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                    {
                        this.TXT01_RRN1080.Focus();

                        this.ShowMessage("TY_M_MR_2CEA5189");
                        e.Successed = false;
                        return;
                    }
                }

                if ((this.TXT01_RRN5030.GetValue().ToString() != "") &&
                    (this.TXT01_RRN6010.GetValue().ToString() != ""))
                {
                    this.ShowMessage("TY_M_MR_2CFCG212");
                    e.Successed = false;
                    return;
                }

                // 1) 자산분류코드 체크
                if ((this.TXT01_RRN5030.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_RRN6010.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_RRN1080.GetValue().ToString() != "12210000" &&// 자산계정
                    this.TXT01_RRN5010.GetValue().ToString() == "N")                       // 비품구분
                {
                    // 자산분류코드
                    if (this.TXT01_RRN6020.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.TXT01_RRN6020.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 자산분류코드 존재 유무 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM4T579",
                            this.TXT01_RRN6020.GetValue().ToString().Substring(1, 1),
                            this.TXT01_RRN6020.GetValue().ToString().Substring(2, 2),
                            this.TXT01_RRN6020.GetValue().ToString().Substring(4, 4),
                            this.TXT01_RRN6020.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM51580");

                            this.TXT01_RRN6020.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 2) 자산분류코드 체크
                if ((this.TXT01_RRN5030.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_RRN6010.GetValue().ToString() != "") && // 자산번호
                    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.TXT01_RRN5010.GetValue().ToString() == "N")                       // 비품구분
                {
                    // 고정자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5E581",
                        this.TXT01_RRN6010.GetValue().ToString().Substring(0,4).ToString(),
                        this.TXT01_RRN6010.GetValue().ToString().Substring(4,4).ToString(),
                        this.TXT01_RRN6010.GetValue().ToString().Substring(8,3).ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_RRN6020.SetValue(dt.Rows[0]["FXGUBN"].ToString());
                        this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM5J582");

                        this.TXT01_RRN6020.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 3) 자산분류코드 체크
                if ((this.TXT01_RRN5030.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_RRN6010.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.TXT01_RRN5010.GetValue().ToString() == "Y")                       // 비품구분
                {
                    // 자산분류코드
                    if (this.TXT01_RRN6020.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.TXT01_RRN6020.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 자산번호에 따른 분류코드를 가져옴.
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM4T579",
                            this.TXT01_RRN6020.GetValue().ToString().Substring(1, 1),
                            this.TXT01_RRN6020.GetValue().ToString().Substring(2, 2),
                            this.TXT01_RRN6020.GetValue().ToString().Substring(4, 4),
                            this.TXT01_RRN6020.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM5O584");

                            this.TXT01_RRN5030.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 4) 자산분류코드 체크
                if ((this.TXT01_RRN5030.GetValue().ToString() != "") && // 비품번호
                    (this.TXT01_RRN6010.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.TXT01_RRN5010.GetValue().ToString() == "Y")                       // 비품구분
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_RRN5030.GetValue().ToString().Substring(0, 6),
                        this.TXT01_RRN5030.GetValue().ToString().Substring(6, 3)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_RRN6020.SetValue(dt.Rows[0]["MABPCODE"].ToString());
                        this.TXT01_RRN6020NM.SetValue(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM51580");

                        this.TXT01_RRN6020.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 5) 자산분류코드 체크
                if ((this.TXT01_RRN5030.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_RRN6010.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.TXT01_RRN5010.GetValue().ToString() == "N")                       // 비품구분
                {
                    this.TXT01_RRN6020.SetValue("");
                    this.TXT01_RRN6020NM.SetValue("");
                }

                // 불량수량 관련 체크
                // 불량여부
                if (this.CBO01_RRN1300.GetValue().ToString() == "Y")
                {
                    // 불량상태
                    if (this.CBH01_RRN1310.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BS3H720");

                        this.CBH01_RRN1310.CodeText.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 불량수량
                    if (double.Parse(Get_Numeric(this.TXT01_RRN1320.GetValue().ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_2BS3H721");

                        this.TXT01_RRN1320.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    // 불량상태
                    if (this.CBH01_RRN1310.GetValue().ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_2BS3H722");

                        this.CBO01_RRN1300.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 불량수량
                    if (double.Parse(Get_Numeric(this.TXT01_RRN1320.GetValue().ToString())) != 0)
                    {
                        this.ShowMessage("TY_M_MR_2BS3H722");

                        this.CBO01_RRN1300.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 입고수량보다 불량수량이 많을수 없음
                if (double.Parse(Get_Numeric(this.TXT01_RRN1200.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_RRN1320.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_MR_2BS3F718");

                    this.TXT01_RRN1320.Focus();

                    e.Successed = false;
                    return;
                }

                decimal dRRN1200 = 0;
                decimal dRRN1210 = 0;
                decimal dRRN1220 = 0;
                decimal dRRN1140 = 0;
                decimal dRRN1320 = 0;

                dRRN1200 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1200.GetValue().ToString()))); //입고수량
                dRRN1210 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1210.GetValue().ToString()))); //입고단가
                dRRN1220 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1220.GetValue().ToString()))); //입고감소단가
                dRRN1140 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1140.GetValue().ToString()))); //환율
                
                dRRN1320 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_RRN1320.GetValue().ToString()))); //불량수량

                //입고금액
                if (double.Parse(Get_Numeric(this.TXT01_RRN1140.GetValue().ToString())) == 0)
                {
                    this.TXT01_RRN1230.SetValue(Convert.ToString(string.Format("{0,9:N3}", dRRN1200 * dRRN1210)));

                    TXT01_RRN1230.Text = UP_DotDelete(this.TXT01_RRN1230.GetValue().ToString());
                }
                else
                {
                    this.TXT01_RRN1230.SetValue(Convert.ToString(string.Format("{0,9:N3}", dRRN1200 * dRRN1210 * dRRN1140)));
                }

                //입고감소금액
                if (double.Parse(Get_Numeric(this.TXT01_RRN1140.GetValue().ToString())) == 0)
                {
                    this.TXT01_RRN12201.SetValue(Convert.ToString(string.Format("{0,9:N3}", dRRN1200 * dRRN1220)));

                    TXT01_RRN12201.Text = UP_DotDelete(this.TXT01_RRN12201.GetValue().ToString());
                }
                else
                {
                    this.TXT01_RRN12201.SetValue(Convert.ToString(string.Format("{0,9:N3}", dRRN1200 * dRRN1220 * dRRN1140)));
                } 

                //불량금액
                if (double.Parse(Get_Numeric(this.TXT01_RRN1140.GetValue().ToString())) == 0)
                {
                    this.TXT01_RRN1330.SetValue(Convert.ToString(string.Format("{0,9:N3}", dRRN1320 * dRRN1210)));

                    this.TXT01_RRN1330.SetValue(UP_DotDelete(this.TXT01_RRN1330.GetValue().ToString()));
                }
                else
                {
                    this.TXT01_RRN1330.SetValue(Convert.ToString(string.Format("{0,9:N3}", dRRN1320 * dRRN1210 * dRRN1140)));
                }

                // 입고금액보다 불량금액이 많을수 없음
                if (double.Parse(Get_Numeric(this.TXT01_RRN1230.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_RRN1330.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_MR_2BS3G719");

                    this.TXT01_RRN1320.Focus();

                    e.Successed = false;
                    return;
                }

                // 입고금액보다 입고감소금액이 많을수 없음
                if (double.Parse(Get_Numeric(this.TXT01_RRN1230.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_RRN12201.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_MR_BBID8754");

                    this.TXT01_RRN12201.Focus();

                    e.Successed = false;
                    return;
                }

                //발주 수량
                string sPON1120  = string.Empty; // 검수구분
                double dPON1170  = 0;            // 발주금액
                double dPOJANQTY = 0;            // 입고잔량
                double dPOJANAMT = 0;            // 입고잔액
                double dIPGOQty  = 0;            // 입고수량(입고수량 - 불량수량)
                double dIPGOAmt  = 0;            // 입고금액(입고금액 - 불량금액)

                // 발주수량 및 발주금액
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BS3V730",
                    this.TXT01_PON1000.GetValue().ToString(),
                    this.TXT01_PON1010.GetValue().ToString(),
                    this.TXT01_PON1020.GetValue().ToString(),
                    this.TXT01_PON1030.GetValue().ToString(),
                    this.TXT01_RRN1040.GetValue().ToString(),
                    this.TXT01_RRN1050.GetValue().ToString(),
                    this.TXT01_RRN1070.GetValue().ToString(),
                    this.TXT01_RRN1080.GetValue().ToString(),
                    this.TXT01_RRN1090.GetValue().ToString(),
                    this.TXT01_RRN1091.GetValue().ToString(),
                    this.TXT01_RRN1092.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 검수구분
                    sPON1120 = dt.Rows[0]["PON1120"].ToString();

                    if (sPON1120 == "1") //수량
                    {
                        // 입고잔량
                        dPOJANQTY = Convert.ToDouble(dt.Rows[0]["PON1230"].ToString());
                    }
                    else //금액
                    {
                        // 입고잔액
                        dPOJANAMT = Convert.ToDouble(dt.Rows[0]["PON1330"].ToString());
                    }

                    // 발주금액
                    dPON1170 = Convert.ToDouble(dt.Rows[0]["PON1170"].ToString());
                }

                /**************************************************************************************************
                 * - 자산계정 분할입고 안되는 경우(발주금액과 입고금액이 일치해야 함)
                 *   1. 자산계정이면서 자산번호가 없을 경우(자산 신규취득)
                 *   2. 자산계정이면서 자산번호가 있을 경우
                 *      - 월상각테이블에서 최근자료의 미상각잔액이 1000원이하일 경우
                 **************************************************************************************************/

                if (this.TXT01_RRN1080.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                {
                    // 1. 자산계정이면서 자산번호가 없을 경우(자산 신규취득)
                    if (this.TXT01_RRN6010.GetValue().ToString() == "")
                    {
                        if (double.Parse(Get_Numeric(this.TXT01_RRN1230.GetValue().ToString())) != dPON1170)
                        {
                            this.ShowMessage("TY_M_MR_31GAN745");

                            this.TXT01_RRN1320.Focus();

                            e.Successed = false;
                            return;
                        }
                    }

                    // 2. 자산계정이면서 자산번호가 있을 경우
                    if (this.TXT01_RRN6010.GetValue().ToString() != "")
                    {
                        double dFXMREPJANAMOUNT = 0;

                        // 월상각테이블에서 최근자료의 미상각잔액이 1000원이하일 경우
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_31GAW748",
                            this.TXT01_RRN6010.GetValue().ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 미상각잔액
                            dFXMREPJANAMOUNT = Convert.ToDouble(dt.Rows[0]["FXMREPJANAMOUNT"].ToString());

                            if (dFXMREPJANAMOUNT <= 1000)
                            {
                                if (double.Parse(Get_Numeric(this.TXT01_RRN1230.GetValue().ToString())) != dPON1170)
                                {
                                    this.ShowMessage("TY_M_MR_31GAN746");

                                    this.TXT01_RRN1320.Focus();

                                    e.Successed = false;
                                    return;
                                }
                            }
                        }
                    }
                }

                // 수정전 구매입고 입고수량(입고수량 - 불량수량) 및 입고금액(입고금액 - 불량금액)을 가져옴.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BS43733",
                    this.TXT01_RRN1000.GetValue().ToString(),
                    this.TXT01_RRN1010.GetValue().ToString(),
                    this.TXT01_RRN1020.GetValue().ToString(),
                    this.TXT01_RRN1030.GetValue().ToString(),
                    this.TXT01_RRN1040.GetValue().ToString(),
                    this.TXT01_RRN1050.GetValue().ToString(),
                    this.TXT01_RRN1070.GetValue().ToString(),
                    this.TXT01_RRN1080.GetValue().ToString(),
                    this.TXT01_RRN1090.GetValue().ToString(),
                    this.TXT01_RRN1091.GetValue().ToString(),
                    this.TXT01_RRN1092.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (sPON1120 == "1") //수량
                    {
                        dPOJANQTY = dPOJANQTY + Convert.ToDouble(dt.Rows[0]["RRN1200"].ToString());
                    }
                    else
                    {
                        dPOJANAMT = dPOJANAMT + Convert.ToDouble(dt.Rows[0]["RRN1230"].ToString());
                    }
                }

                // 검수구분 = 금액일 경우 수량은 1임.
                if (sPON1120 == "2")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_RRN1200.Text.Trim())) != 1)
                    {
                        this.ShowMessage("TY_M_MR_2BRAX669");

                        this.TXT01_RRN1200.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_RRN1200.Text.Trim())) == 1 && double.Parse(Get_Numeric(this.TXT01_RRN1320.Text.Trim())) == 1)
                    {
                        this.ShowMessage("TY_M_MR_2BTA9745");

                        this.TXT01_RRN1320.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                if (sPON1120 == "1") //수량
                {
                    dIPGOQty = Convert.ToDouble(Get_Numeric(this.TXT01_RRN1200.Text.Trim())) - Convert.ToDouble(Get_Numeric(this.TXT01_RRN1320.Text.Trim()));

                    // 입고 수량이 발주 수량을 초과합니다.
                    if (dIPGOQty > dPOJANQTY)
                    {
                        this.ShowMessage("TY_M_MR_2BSBN715");

                        this.TXT01_RRN1200.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else // 금액
                {
                    dIPGOAmt = Convert.ToDouble(Get_Numeric(this.TXT01_RRN1230.Text.Trim())) + Convert.ToDouble(Get_Numeric(this.TXT01_RRN1330.Text.Trim()));

                    // 입고 금액이 발주 금액을 초과합니다.
                    if (dIPGOAmt > dPOJANAMT)
                    {
                        this.ShowMessage("TY_M_MR_2BSBN714");

                        this.TXT01_RRN1200.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 구매입고내역테이블에 고정자산생성번호 존재유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BSB0710",
                    this.TXT01_RRN1000.GetValue().ToString(),
                    this.TXT01_RRN1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_RRN1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_RRN1030.GetValue().ToString())),
                    this.TXT01_RRN1040.GetValue().ToString(),
                    this.TXT01_RRN1050.GetValue().ToString(),
                    this.TXT01_RRN1070.GetValue().ToString(),
                    this.TXT01_RRN1080.GetValue().ToString(),
                    this.TXT01_RRN1090.GetValue().ToString(),
                    Get_Numeric(this.TXT01_RRN1091.GetValue().ToString()),
                    Get_Numeric(this.TXT01_RRN1092.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["RRN6000"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_2BG15408");

                        e.Successed = false;
                        return;
                    }
                }

                // 예산 카운트(삭제시 필요)
                fsYESAN_COUNT = "0";

                // 예산 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BSB3711",
                    this.TXT01_RRN1000.GetValue().ToString(),
                    this.TXT01_RRN1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_RRN1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_RRN1030.GetValue().ToString())),
                    this.TXT01_RRN1040.GetValue().ToString(),
                    this.TXT01_RRN1070.GetValue().ToString(),
                    this.TXT01_RRN1080.GetValue().ToString(),
                    this.TXT01_RRN1090.GetValue().ToString(),
                    Get_Numeric(this.TXT01_RRN1091.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                }

                #endregion
            }

            if (fsMAGAM.ToString() == "")
            {
                // 결재 완료 문서 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BR4M682",
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_RRM1030.GetValue().ToString())
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    fsMAGAM = "MAGAM";

                    if (TYUserInfo.EmpNo.ToString() != "0377-M")
                    {
                        this.ShowMessage("TY_M_MR_2BC59266");
                        e.Successed = false;
                        return;
                    }
                }
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
            fsPOM5110     = "";
            fsYESAN_COUNT = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPRRMF") // 마스터
            {
                // 내역사항 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BR4B678",
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    this.TXT01_RRM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z312");

                    this.CBH01_RRM1420.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 특기사항 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BQ6H643",
                    this.TXT01_RRM1000.GetValue().ToString(),
                    this.TXT01_RRM1010.GetValue().ToString(),
                    this.TXT01_RRM1020.GetValue().ToString(),
                    this.TXT01_RRM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z311");

                    this.CBH01_RRM1420.CodeText.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "MRPRRNF") // 내역사항
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_43VE8076",
                    this.TXT01_RRN1000.GetValue(),
                    this.TXT01_RRN1010.GetValue(),
                    this.TXT01_RRN1020.GetValue(),
                    Set_Fill4(this.TXT01_RRN1030.GetValue().ToString()),
                    this.TXT01_RRN1040.GetValue(),
                    this.TXT01_RRN1050.GetValue(),
                    this.TXT01_RRN1070.GetValue(),
                    this.TXT01_RRN1080.GetValue(),
                    this.TXT01_RRN1090.GetValue(),
                    this.TXT01_RRN1091.GetValue(),
                    this.TXT01_RRN1092.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_43VHP085");
                    e.Successed = false;
                    return;
                }

                // 구매입고내역테이블에 고정자산생성번호 존재유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BSB0710",
                    this.TXT01_RRN1000.GetValue().ToString(),
                    this.TXT01_RRN1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_RRN1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_RRN1030.GetValue().ToString())),
                    this.TXT01_RRN1040.GetValue().ToString(),
                    this.TXT01_RRN1050.GetValue().ToString(),
                    this.TXT01_RRN1070.GetValue().ToString(),
                    this.TXT01_RRN1080.GetValue().ToString(),
                    this.TXT01_RRN1090.GetValue().ToString(),
                    Get_Numeric(this.TXT01_RRN1091.GetValue().ToString()),
                    Get_Numeric(this.TXT01_RRN1092.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["RRN6000"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_2BG15408");

                        e.Successed = false;
                        return;
                    }
                }

                // 예산 카운트(삭제시 필요)
                fsYESAN_COUNT = "0";

                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_RRN1080.GetValue().ToString() != "11101001" && this.TXT01_RRN1080.GetValue().ToString() != "12210000")
                {
                    // 예산 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BSB3711",
                        this.TXT01_RRN1000.GetValue().ToString(),
                        this.TXT01_RRN1010.GetValue().ToString(),
                        Get_Numeric(this.TXT01_RRN1020.GetValue().ToString()),
                        Set_Fill4(Get_Numeric(this.TXT01_RRN1030.GetValue().ToString())),
                        this.TXT01_RRN1040.GetValue().ToString(),
                        this.TXT01_RRN1070.GetValue().ToString(),
                        this.TXT01_RRN1080.GetValue().ToString(),
                        this.TXT01_RRN1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_RRN1091.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                    }
                }
                else
                {
                    // 예산 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_39B1L660",
                        this.TXT01_RRN1000.GetValue().ToString(),
                        this.TXT01_RRN1010.GetValue().ToString(),
                        Get_Numeric(this.TXT01_RRN1020.GetValue().ToString()),
                        Set_Fill4(Get_Numeric(this.TXT01_RRN1030.GetValue().ToString())),
                        this.TXT01_RRN1040.GetValue().ToString(),
                        this.TXT01_RRN1070.GetValue().ToString(),
                        this.TXT01_RRN1080.GetValue().ToString(),
                        this.TXT01_RRN1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_RRN1091.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR4M682",
                this.TXT01_RRM1000.GetValue().ToString(),
                this.TXT01_RRM1010.GetValue().ToString(),
                this.TXT01_RRM1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRM1030.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BC59266");
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

        #region Description : 특기 저장 체크
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2BQ6E639.GetDataSourceInclude(TSpread.TActionType.New, "RRT1040", "RRT1100"));

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2BQ6E639.GetDataSourceInclude(TSpread.TActionType.Update, "RRT1040", "RRT1100"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2BR7H690",
                                       TXT01_RRT1000.GetValue(),
                                       TXT01_RRT1010.GetValue(),
                                       TXT01_RRT1020.GetValue(),
                                       TXT01_RRT1030.GetValue(),
                                       ds.Tables[0].Rows[i]["RRT1040"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z311");
                    e.Successed = false;
                    return;
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR4M682",
                this.TXT01_RRT1000.GetValue().ToString(),
                this.TXT01_RRT1010.GetValue().ToString(),
                this.TXT01_RRT1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRT1030.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BC59266");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 특기 삭제 체크
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2BQ6E639.GetDataSourceInclude(TSpread.TActionType.Remove, "RRT1040"));

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR4M682",
                this.TXT01_RRT1000.GetValue().ToString(),
                this.TXT01_RRT1010.GetValue().ToString(),
                this.TXT01_RRT1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRT1030.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BC59266");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion






        #region Description : 입고자산 내역 저장 체크
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_43VE9077.GetDataSourceInclude(TSpread.TActionType.New,    "RRA1100", "RRA1200", "RRA1210", "RRA1220", "RRA1230", "RRA1240", "RRA1250", "RRA1260", "RRA1270", "RRA1280"));

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_43VE9077.GetDataSourceInclude(TSpread.TActionType.Update, "RRA1100", "RRA1200", "RRA1210", "RRA1220", "RRA1230", "RRA1240", "RRA1250", "RRA1260", "RRA1270", "RRA1280"));

            // 신규
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 중기, 차량운반구
                if (this.TXT01_RRA1080.GetValue().ToString() == "12200500" ||
                    this.TXT01_RRA1080.GetValue().ToString() == "12200600")
                {
                    if (ds.Tables[0].Rows[i]["RRA1250"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_43VFB078");
                        e.Successed = false;
                        return;
                    }
                }

                // 건물, 구축물
                if (this.TXT01_RRA1080.GetValue().ToString() == "12200200" ||
                    this.TXT01_RRA1080.GetValue().ToString() == "12200300")
                {
                    // 구조
                    if (ds.Tables[0].Rows[i]["RRA1260"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_43VFC079");
                        e.Successed = false;
                        return;
                    }

                    // 재질
                    if (ds.Tables[0].Rows[i]["RRA1270"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_43VFD080");
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 중기, 차량운반구
                if (this.TXT01_RRA1080.GetValue().ToString() == "12200500" ||
                    this.TXT01_RRA1080.GetValue().ToString() == "12200600")
                {
                    if (ds.Tables[1].Rows[i]["RRA1250"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_43VFB078");
                        e.Successed = false;
                        return;
                    }
                }

                // 건물, 구축물
                if (this.TXT01_RRA1080.GetValue().ToString() == "12200200" ||
                    this.TXT01_RRA1080.GetValue().ToString() == "12200300")
                {
                    // 구조
                    if (ds.Tables[1].Rows[i]["RRA1260"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_43VFC079");
                        e.Successed = false;
                        return;
                    }

                    // 재질
                    if (ds.Tables[1].Rows[i]["RRA1270"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_43VFD080");
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR4M682",
                this.TXT01_RRA1000.GetValue().ToString(),
                this.TXT01_RRA1010.GetValue().ToString(),
                this.TXT01_RRA1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRA1030.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BC59266");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 입고자산 내역 삭제 체크
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_43VE9077.GetDataSourceInclude(TSpread.TActionType.Remove, "RRA1100"));

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BR4M682",
                this.TXT01_RRA1000.GetValue().ToString(),
                this.TXT01_RRA1010.GetValue().ToString(),
                this.TXT01_RRA1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_RRA1030.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BC59266");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 마감체크
        private bool UP_MAGAM_CHECK()
        {
            //// 결재 완료 문서 체크
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_2BR4M682",
            //    this.TXT01_RRM1000.GetValue().ToString(),
            //    this.TXT01_RRM1010.GetValue().ToString(),
            //    this.TXT01_RRM1020.GetValue().ToString(),
            //    Set_Fill4(this.TXT01_RRM1030.GetValue().ToString())
            //    );

            //if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            //{
            //    this.TXT01_MESSAGE.SetValue("결재 완료 된 자료이므로 작업이 불가합니다.");
            //    return false;
            //}

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

                if (this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Remove(this.tabPage3);

                if (this.tabControl1.TabPages.Contains(this.tabPage5))
                    this.tabControl1.TabPages.Remove(this.tabPage5);
            }
            else if (sGUBUN == "MRPRRMF")
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Add(this.tabPage2);

                if (!this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Add(this.tabPage3);

                if (!this.tabControl1.TabPages.Contains(this.tabPage5))
                    this.tabControl1.TabPages.Add(this.tabPage5);
            }
        }
        #endregion

        #region Description : 탭 페이지 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool fResult;

            fsMAGAM = "";

            if (tabControl1.SelectedIndex == 0) // 마스터
            {
                fsGUBUN = "MRPRRMF";

                // 마스터 DISPLAY
                UP_MRPRRMF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 마스터 데이터가 존재하므로 
                    // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                    UP_ImgbtnDisplay("1", true);

                    SetStartingFocus(this.CBH01_RRM1420.CodeText);
                }
                else  // 마감완료면
                {
                    if (TYUserInfo.EmpNo.ToString() == "0377-M")
                    {
                        fsMAGAM = "MAGAM";

                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("5", false);

                        this.SetFocus(this.DTP01_RRM1100);
                    }
                    else
                    {
                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("3", false);
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 1) // 내역사항
            {
                // 요청번호
                this.TXT01_PON1000.SetValue(this.TXT01_POM1000.GetValue().ToString());
                this.TXT01_PON1010.SetValue(this.TXT01_POM1010.GetValue().ToString());
                this.TXT01_PON1020.SetValue(this.TXT01_POM1020.GetValue().ToString());
                this.TXT01_PON1030.SetValue(this.TXT01_POM1030.GetValue().ToString());

                fsPRM5100 = "";

                // 계약번호
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BLB0526",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsPRM5100 = dt.Rows[0]["PRM5100"].ToString();
                    this.TXT01_RRM1210.SetValue(dt.Rows[0]["PRM5120"].ToString());
                    this.TXT01_RRM1220.SetValue(dt.Rows[0]["PRM5130"].ToString());
                }

                fsGUBUN = "MRPRRNF";

                // 발주번호
                this.TXT01_RRN1000.SetValue(this.TXT01_RRM1000.GetValue());
                this.TXT01_RRN1010.SetValue(this.TXT01_RRM1010.GetValue());
                this.TXT01_RRN1020.SetValue(this.TXT01_RRM1020.GetValue());
                this.TXT01_RRN1030.SetValue(this.TXT01_RRM1030.GetValue());

                this.TXT01_RRN1000.ReadOnly = true;
                this.TXT01_RRN1010.ReadOnly = true;
                this.TXT01_RRN1020.ReadOnly = true;
                this.TXT01_RRN1030.ReadOnly = true;

                //this.TXT01_RRN1000.BackColor = Color.Silver;
                //this.TXT01_RRN1010.BackColor = Color.Silver;
                //this.TXT01_RRN1020.BackColor = Color.Silver;
                //this.TXT01_RRN1030.BackColor = Color.Silver;


                // 발주번호
                this.TXT01_PON1000.SetValue(this.TXT01_POM1000.GetValue());
                this.TXT01_PON1010.SetValue(this.TXT01_POM1010.GetValue());
                this.TXT01_PON1020.SetValue(this.TXT01_POM1020.GetValue());
                this.TXT01_PON1030.SetValue(this.TXT01_POM1030.GetValue());

                this.TXT01_PON1000.ReadOnly = true;
                this.TXT01_PON1010.ReadOnly = true;
                this.TXT01_PON1020.ReadOnly = true;
                this.TXT01_PON1030.ReadOnly = true;

                // 적용년월
                this.TXT01_RRN2040.SetValue(this.DTP01_RRM1110.GetValue().ToString().Substring(0, 4) + this.DTP01_RRM1110.GetValue().ToString().Substring(4, 2));

                this.TXT01_RRM1210.ReadOnly = true;
                this.TXT01_RRM1220.ReadOnly = true;

                //this.TXT01_RRM1210.BackColor = Color.Silver;
                //this.TXT01_RRM1220.BackColor = Color.Silver;

                this.CBH01_RRN1100.SetReadOnly(true);
                this.CBH01_RRN1110.SetReadOnly(true);
                this.CBH01_RRN1120.SetReadOnly(true);

                this.TXT01_RRN1040.ReadOnly = true;
                this.TXT01_RRN1040NM.ReadOnly = true;
                this.TXT01_RRN1070.ReadOnly = true;
                this.TXT01_RRN1070NM.ReadOnly = true;
                this.TXT01_RRN1080.ReadOnly = true;
                this.TXT01_RRN1080NM.ReadOnly = true;
                this.TXT01_RRN1090.ReadOnly = true;
                this.TXT01_RRN1090NM.ReadOnly = true;
                this.TXT01_RRN1091.ReadOnly = true;
                this.TXT01_RRN1092.ReadOnly = true;
                this.TXT01_RRN2040.ReadOnly = true;
                this.TXT01_RRN1050.ReadOnly = true;
                this.TXT01_RRN1050NM.ReadOnly = true;

                //this.TXT01_RRN1040.BackColor   = Color.Silver;
                //this.TXT01_RRN1040NM.BackColor = Color.Silver;
                //this.TXT01_RRN1070.BackColor   = Color.Silver;
                //this.TXT01_RRN1070NM.BackColor = Color.Silver;
                //this.TXT01_RRN1080.BackColor   = Color.Silver;
                //this.TXT01_RRN1080NM.BackColor = Color.Silver;
                //this.TXT01_RRN1090.BackColor   = Color.Silver;
                //this.TXT01_RRN1090NM.BackColor = Color.Silver;
                //this.TXT01_RRN1091.BackColor   = Color.Silver;
                //this.TXT01_RRN2040.BackColor   = Color.Silver;
                //this.TXT01_RRN1050.BackColor   = Color.Silver;
                //this.TXT01_RRN1050NM.BackColor = Color.Silver;

                //this.TXT01_RRN1210.ReadOnly = true;






                this.TXT01_RRN1400.ReadOnly = true;
                this.TXT01_RRN1150.ReadOnly = true;
                this.TXT01_RRN5010.ReadOnly = true;
                this.TXT01_RRN5030.ReadOnly = true;
                this.TXT01_RRN6010.ReadOnly = true;
                this.TXT01_RRN6020.ReadOnly = true;
                this.TXT01_RRN6030.ReadOnly = true;
                this.TXT01_RRN6000.ReadOnly = true;
                this.TXT01_RRN1520.ReadOnly = true;
                this.TXT01_RRN5030NM.ReadOnly = true;
                this.TXT01_RRN6010NM.ReadOnly = true;
                this.TXT01_RRN6020NM.ReadOnly = true;

                //this.TXT01_RRN1210.BackColor = Color.Silver;
                //this.TXT01_RRN1400.BackColor = Color.Silver;
                //this.TXT01_RRN5010.BackColor = Color.Silver;
                //this.TXT01_RRN5030.BackColor = Color.Silver;
                //this.TXT01_RRN6010.BackColor = Color.Silver;
                //this.TXT01_RRN6020.BackColor = Color.Silver;
                //this.TXT01_RRN6030.BackColor = Color.Silver;
                //this.TXT01_RRN6000.BackColor = Color.Silver;
                //this.TXT01_RRN1520.BackColor = Color.Silver;
                //this.TXT01_RRN5030NM.BackColor = Color.Silver;
                //this.TXT01_RRN6010NM.BackColor = Color.Silver;
                //this.TXT01_RRN6020NM.BackColor = Color.Silver;

                this.TXT01_RRN1230.ReadOnly = true;
                this.TXT01_PON1150.ReadOnly = true;
                this.TXT01_PON1160.ReadOnly = true;
                this.TXT01_PON1230.ReadOnly = true;
                this.TXT01_PON1330.ReadOnly = true;
                this.TXT01_RRN1330.ReadOnly = true;

                this.TXT01_RRN1330.ReadOnly = true;

                //this.TXT01_RRN1230.BackColor = Color.Silver;
                //this.TXT01_PON1150.BackColor = Color.Silver;
                //this.TXT01_PON1160.BackColor = Color.Silver;
                //this.TXT01_PON1230.BackColor = Color.Silver;
                //this.TXT01_RRN1330.BackColor = Color.Silver;

                //this.TXT01_RRN1330.BackColor = Color.Silver;

                // 내역사항 DISPLAY
                UP_MRPRRNF_DISPLAY();

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
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("3", false);
                }
                

                //UP_MRPRRNF_DISPLAY();
            }
            else if (tabControl1.SelectedIndex == 2) // 특기사항
            {
                fsGUBUN = "MRPRRTF";

                this.TXT01_RRT1000.SetValue(this.TXT01_RRM1000.GetValue());
                this.TXT01_RRT1010.SetValue(this.TXT01_RRM1010.GetValue());
                this.TXT01_RRT1020.SetValue(this.TXT01_RRM1020.GetValue());
                this.TXT01_RRT1030.SetValue(this.TXT01_RRM1030.GetValue());

                this.TXT01_RRT1000.ReadOnly = true;
                this.TXT01_RRT1010.ReadOnly = true;
                this.TXT01_RRT1020.ReadOnly = true;
                this.TXT01_RRT1030.ReadOnly = true;

                //this.TXT01_RRT1000.BackColor = Color.Silver;
                //this.TXT01_RRT1010.BackColor = Color.Silver;
                //this.TXT01_RRT1020.BackColor = Color.Silver;
                //this.TXT01_RRT1030.BackColor = Color.Silver;

                // 특기사항 DISPLAY
                UP_MRPRRTF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    UP_ImgbtnDisplay("3", false);
                }
                else // 마감완료면
                {
                    UP_ImgbtnDisplay("3", false);
                }
            }
            else if (tabControl1.SelectedIndex == 3) // 구매입고자산 내역관리
            {
                fsGUBUN = "MRPRRAF";

                // 요청번호
                this.TXT01_RRA1000.SetValue(this.TXT01_RRM1000.GetValue().ToString());
                this.TXT01_RRA1010.SetValue(this.TXT01_RRM1010.GetValue().ToString());
                this.TXT01_RRA1020.SetValue(this.TXT01_RRM1020.GetValue().ToString());
                this.TXT01_RRA1030.SetValue(this.TXT01_RRM1030.GetValue().ToString());

                this.TXT01_RRA1040.SetValue("");
                this.TXT01_RRA1040NM.SetValue("");
                this.TXT01_RRA1070.SetValue("");
                this.TXT01_RRA1070NM.SetValue("");
                this.TXT01_RRA1080.SetValue("");
                this.TXT01_RRA1080NM.SetValue("");
                this.TXT01_RRA1090.SetValue("");
                this.TXT01_RRA1090NM.SetValue("");
                this.TXT01_RRA1091.SetValue("");
                this.TXT01_RRA1092.SetValue("");
                this.TXT01_RRA1050.SetValue("");
                this.TXT01_RRA1050NM.SetValue("");

                this.TXT01_RRA1000.ReadOnly = true;
                this.TXT01_RRA1010.ReadOnly = true;
                this.TXT01_RRA1020.ReadOnly = true;
                this.TXT01_RRA1030.ReadOnly = true;
                this.TXT01_RRA1040.ReadOnly = true;
                this.TXT01_RRA1040NM.ReadOnly = true;
                this.TXT01_RRA1070.ReadOnly = true;
                this.TXT01_RRA1070NM.ReadOnly = true;
                this.TXT01_RRA1080.ReadOnly = true;
                this.TXT01_RRA1080NM.ReadOnly = true;
                this.TXT01_RRA1090.ReadOnly = true;
                this.TXT01_RRA1090NM.ReadOnly = true;
                this.TXT01_RRA1091.ReadOnly = true;
                this.TXT01_RRA1092.ReadOnly = true;
                this.TXT01_RRA1050.ReadOnly = true;
                this.TXT01_RRA1050NM.ReadOnly = true;

                // 입고자산 내역사항 DISPLAY
                UP_MRPRRAF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (this.TXT01_MESSAGE.GetValue().ToString() != "")
                {
                    if (fsMAGAM.ToString() == "")
                    {
                        //this.BTN61_NEW.Visible = false;
                        this.BTN61_SAV.Visible = false;
                        this.BTN61_EDIT.Visible = false;
                        this.BTN61_REM.Visible = false;

                        this.BTN62_SAV.Visible = false;
                        this.BTN62_REM.Visible = false;

                        this.BTN63_SAV.Visible = false;
                        this.BTN63_REM.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region Description : 컨트롤 초기화
        private void UP_Control_Initialize(string sGUBUN, bool bTrueFalse)
        {
            this.TXT01_POM1010.SetReadOnly(true);
            if (sGUBUN == "MRPRRMF") // 마스터
            {
                this.TXT01_RRM1010.SetReadOnly(bTrueFalse);
                this.TXT01_RRM1030.SetReadOnly(bTrueFalse);

                //this.TXT01_POM1000.SetReadOnly(bTrueFalse);
                //this.TXT01_POM1010.SetReadOnly(bTrueFalse);
                //this.TXT01_POM1020.SetReadOnly(bTrueFalse);
                //this.TXT01_POM1030.SetReadOnly(bTrueFalse);
                this.TXT01_POM1180.SetReadOnly(bTrueFalse);
                this.TXT01_POM1120.SetReadOnly(bTrueFalse);
                this.TXT01_POM1130.SetReadOnly(bTrueFalse);
                this.TXT01_DTDESC.SetReadOnly(bTrueFalse);
                this.TXT01_PRM2050.SetReadOnly(bTrueFalse);

                this.TXT01_PRM2010.SetReadOnly(bTrueFalse);
                this.TXT01_DTDESC1.SetReadOnly(bTrueFalse);
                this.TXT01_POM1720.SetReadOnly(bTrueFalse);
                this.TXT01_POM1160.SetReadOnly(bTrueFalse);
                this.TXT01_POM1150.SetReadOnly(bTrueFalse);
                this.TXT01_RRM1440.SetReadOnly(bTrueFalse);
                this.TXT01_KBHANGL1.SetReadOnly(bTrueFalse);
                this.TXT01_RRM1450.SetReadOnly(bTrueFalse);
                this.CBO01_RRM1500.SetReadOnly(bTrueFalse);
                this.TXT01_RRM1460.SetReadOnly(bTrueFalse);
                this.TXT01_RRM1510.SetReadOnly(bTrueFalse);

                this.CBO01_RRM6010.SetReadOnly(bTrueFalse);
                this.CBO01_RRM6020.SetReadOnly(bTrueFalse);
                this.CBH01_RRM6030.SetReadOnly(bTrueFalse);

                this.FPS91_TY_S_MR_2BR44676.Initialize();
                this.FPS91_TY_S_MR_2BR40677.Initialize();

                // 예산 DISPLAY
                UP_MRPRROF_DISPLAY();
            }
            else if (sGUBUN == "MRPRRNF") // 내역사항
            {
                //this.TXT01_RRN1000.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1010.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1020.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1030.ReadOnly = bTrueFalse;


                //this.TXT01_PON1000.ReadOnly = bTrueFalse;
                //this.TXT01_PON1010.ReadOnly = bTrueFalse;
                //this.TXT01_PON1020.ReadOnly = bTrueFalse;
                //this.TXT01_PON1030.ReadOnly = bTrueFalse;


                //this.TXT01_RRM1210.ReadOnly = bTrueFalse;
                //this.TXT01_RRM1220.ReadOnly = bTrueFalse;


                //this.CBH01_RRN1100.SetReadOnly(bTrueFalse);
                //this.CBH01_RRN1110.SetReadOnly(bTrueFalse);
                //this.CBH01_RRN1120.SetReadOnly(bTrueFalse);

                //this.TXT01_RRN1040.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1040NM.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1070.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1070NM.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1080.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1080NM.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1090.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1090NM.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1091.ReadOnly = bTrueFalse;
                //this.TXT01_RRN2040.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1050.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1050NM.ReadOnly = bTrueFalse;


                //this.TXT01_RRN1400.ReadOnly = bTrueFalse;
                //this.TXT01_RRN5010.ReadOnly = bTrueFalse;
                //this.TXT01_RRN5030.ReadOnly = bTrueFalse;
                //this.TXT01_RRN6010.ReadOnly = bTrueFalse;
                //this.TXT01_RRN6020.ReadOnly = bTrueFalse;
                //this.TXT01_RRN6030.ReadOnly = bTrueFalse;
                //this.TXT01_RRN6000.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1520.ReadOnly = bTrueFalse;
                //this.TXT01_RRN5030NM.ReadOnly = bTrueFalse;
                //this.TXT01_RRN6010NM.ReadOnly = bTrueFalse;
                //this.TXT01_RRN6020NM.ReadOnly = bTrueFalse;


                //this.TXT01_RRN1230.ReadOnly = bTrueFalse;
                //this.TXT01_PON1150.ReadOnly = bTrueFalse;
                //this.TXT01_PON1160.ReadOnly = bTrueFalse;
                //this.TXT01_PON1230.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1330.ReadOnly = bTrueFalse;

                //this.TXT01_RRN1330.ReadOnly = bTrueFalse;

                // 버튼
                //this.BTN61_PRN10701.ReadOnly = bTrueFalse;
                //this.BTN61_PON10501.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1040.SetReadOnlyButton(bTrueFalse);
                //this.TXT01_RRN1070.SetReadOnlyButton(bTrueFalse);

                //this.TXT01_RRN1040.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1040.SetReadOnlyCode(bTrueFalse);
                //this.TXT01_RRN1040.SetReadOnlyText(bTrueFalse);

                this.TXT01_RRN1050.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1070.ReadOnly = bTrueFalse;
                //this.TXT01_RRN1070.SetReadOnlyCode(bTrueFalse);
                //this.TXT01_RRN1070.SetReadOnlyText(bTrueFalse);
                this.TXT01_RRN1080.ReadOnly = bTrueFalse;
            }
        }
        #endregion

        #region Description : 버튼 컨트롤
        private void UP_ImgbtnDisplay(string sGubn, bool bTrueFalse)
        {
            if (fsGUBUN == "MRPRRMF")
            {
                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                this.BTN63_SAV.Visible = false;
                this.BTN63_REM.Visible = false;

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
                else if (sGubn == "3") // 마감 완료
                {
                    BTN61_SAV.Visible  = bTrueFalse;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible  = bTrueFalse;
                }
                else if (sGubn == "5")
                {
                    BTN61_SAV.Visible = false;
                    BTN61_EDIT.Visible = true;
                    BTN61_REM.Visible = false;
                }
            }
            else if (fsGUBUN == "MRPRRNF")
            {
                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                this.BTN63_SAV.Visible = false;
                this.BTN63_REM.Visible = false;

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
                else if(sGubn == "3") // 마감 완료
                {
                    BTN61_SAV.Visible  = bTrueFalse;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible  = bTrueFalse;
                }
                else if (sGubn == "5")
                {
                    BTN61_SAV.Visible = false;
                    BTN61_EDIT.Visible = true;
                    BTN61_REM.Visible = false;
                }
            }
            else if (fsGUBUN == "MRPRRTF")
            {
                //this.BTN61_NEW.Visible = false;
                this.BTN61_SAV.Visible  = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible  = false;

                this.BTN63_SAV.Visible  = false;
                this.BTN63_REM.Visible  = false;

                this.BTN62_SAV.Visible  = true;
                this.BTN62_REM.Visible  = true;
            }
            else if (fsGUBUN == "MRPRRAF")
            {
                this.BTN61_SAV.Visible  = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible  = false;

                this.BTN62_SAV.Visible  = false;
                this.BTN62_REM.Visible  = false;

                this.BTN63_SAV.Visible  = true;
                this.BTN63_REM.Visible  = true;
            }

            if (this.TXT01_MESSAGE.GetValue().ToString() != "")
            {
                if (fsMAGAM.ToString() == "")
                {
                    //this.BTN61_NEW.Visible = false;
                    this.BTN61_SAV.Visible = false;
                    if (TYUserInfo.EmpNo.ToString() != "0377-M")
                    {
                        this.BTN61_EDIT.Visible = false;
                    }
                    this.BTN61_REM.Visible = false;

                    this.BTN62_SAV.Visible = false;
                    this.BTN62_REM.Visible = false;

                    this.BTN63_SAV.Visible = false;
                    this.BTN63_REM.Visible = false;

                    
                }
            }
        }
        #endregion

        #region Description : 폼 닫기 이벤트
        private void TYMRRR001I_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsRRM1000 = this.TXT01_RRM1000.GetValue().ToString();
            fsRRM1010 = this.TXT01_RRM1010.GetValue().ToString();
            fsRRM1020 = this.TXT01_RRM1020.GetValue().ToString();
            fsRRM1030 = this.TXT01_RRM1030.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        #endregion

        #endregion

        #region Description : 미발주 요청자료 조회
        private void UP_SetPOMData()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_74II5318",
               fsPOM1000,
               fsPOM1010,
               fsPOM1020,
               fsPOM1030
               );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_POM1000.SetValue(dt.Rows[0]["POM1000"].ToString());   /* 사업부 */
                this.TXT01_POM1020.SetValue(dt.Rows[0]["POM1020"].ToString());   /* 년월 */
                this.TXT01_POM1030.SetValue(dt.Rows[0]["POM1030"].ToString());   /* 순서 */
                this.TXT01_POM1180.SetValue(dt.Rows[0]["POM1180"].ToString());   /* 공사및구매명 */
                this.TXT01_POM1120.SetValue(dt.Rows[0]["POM1120"].ToString());   /* 요청번호 */
                this.TXT01_POM1130.SetValue(dt.Rows[0]["POM1130"].ToString());   /* 발의부서   */
                this.TXT01_DTDESC.SetValue(dt.Rows[0]["PODT_DESC"].ToString());  /* 발의부서명 */
                this.TXT01_PRM2050.SetValue(dt.Rows[0]["PRM2020"].ToString());   /* 요청일자 */
                this.TXT01_PRM2010.SetValue(dt.Rows[0]["PRM2010"].ToString());   /* 요청부서 */
                this.TXT01_DTDESC1.SetValue(dt.Rows[0]["PRDT_DESC"].ToString()); /* 요청부서명 */
                this.TXT01_POM1720.SetValue(dt.Rows[0]["POM1720"].ToString());   /* 발주금액 */
                this.TXT01_POM1160.SetValue(dt.Rows[0]["POM1160"].ToString());   /* 인도조건 */
                this.TXT01_POM1150.SetValue(dt.Rows[0]["POM1150"].ToString());   /* 인도지역 */
                this.TXT01_RRM1180.SetValue(dt.Rows[0]["POM1180"].ToString());   /* 공사및구매명 */
                this.CBO01_RRM1500.SetValue(dt.Rows[0]["POM1910"].ToString());   /* 월말구분 */
                this.CBO01_RRM6010.SetValue(dt.Rows[0]["POM6010"].ToString());   /* 비용청구 */
                this.CBO01_RRM6020.SetValue(dt.Rows[0]["POM6020"].ToString());   /* 청구구분 */
                this.CBH01_RRM6030.SetValue(dt.Rows[0]["POM6030"].ToString());   /* 청구화주 */

                // 검수사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_RRM1400.SetValue(TYUserInfo.EmpNo);
                // 인수사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_RRM1420.SetValue(TYUserInfo.EmpNo);

                SetFocus(DTP01_RRM1100);
            }
        }
        #endregion
        

        #region Description : 발주번호 코드헬프

        private void TXT01_POM1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_POM1000.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_POM10001_Click(null, null);
                }
            }
        }

        private void TXT01_POM1020_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_POM1020.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_POM10001_Click(null, null);
                }
            }
        }

        private void TXT01_POM1030_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_POM1030.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_POM10001_Click(null, null);
                }
            }
        }

        private void BTN61_POM10001_Click(object sender, EventArgs e)
        {
            if (this.TXT01_RRM1000.GetValue().ToString() != "A" && this.TXT01_RRM1000.GetValue().ToString() != "S" &&
                this.TXT01_RRM1000.GetValue().ToString() != "T" && this.TXT01_RRM1000.GetValue().ToString() != "B" &&
                this.TXT01_RRM1000.GetValue().ToString() != "C" && this.TXT01_RRM1000.GetValue().ToString() != "D" &&
                this.TXT01_RRM1000.GetValue().ToString() != "E")
            {
                this.ShowMessage("TY_M_MR_2BK3H504");
                this.TXT01_RRM1000.Focus();
                return;
            }

            // 발주년월
            if (this.TXT01_RRM1020.GetValue().ToString().Length != 6)
            {
                this.ShowMessage("TY_M_MR_2BK3G501");
                this.TXT01_RRM1020.Focus();
                return;
            }

            // 구매발주 코드헬프
            TYMZRR01C1 popup = new TYMZRR01C1(this.TXT01_RRM1000.GetValue().ToString(), this.TXT01_RRM1020.GetValue().ToString(), this.TXT01_RRM1020.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_POM1000.SetValue(popup.fsPOM1000);   /* 사업부 */
                this.TXT01_POM1020.SetValue(popup.fsPOM1020);   /* 년월 */
                this.TXT01_POM1030.SetValue(popup.fsPOM1030);   /* 순서 */
                this.TXT01_POM1180.SetValue(popup.fsPOM1180);   /* 공사및구매명 */
                this.TXT01_POM1120.SetValue(popup.fsPOM1120);   /* 요청번호 */
                this.TXT01_POM1130.SetValue(popup.fsPOM1130);   /* 발의부서   */
                this.TXT01_DTDESC.SetValue(popup.fsPOM1130NM);  /* 발의부서명 */
                this.TXT01_PRM2050.SetValue(popup.fsPRM2020);   /* 요청일자 */
                this.TXT01_PRM2010.SetValue(popup.fsPRM2010);   /* 요청부서 */
                this.TXT01_DTDESC1.SetValue(popup.fsPRM2010NM); /* 요청부서명 */
                this.TXT01_POM1720.SetValue(popup.fsPOM1720);   /* 발주금액 */
                this.TXT01_POM1160.SetValue(popup.fsPOM1160);   /* 인도조건 */
                this.TXT01_POM1150.SetValue(popup.fsPOM1150);   /* 인도지역 */
                this.TXT01_RRM1180.SetValue(popup.fsPOM1180);   /* 공사및구매명 */
                this.CBO01_RRM1500.SetValue(popup.fsPOM1910);   /* 월말구분 */
                this.CBO01_RRM6010.SetValue(popup.fsPOM6010);   /* 비용청구 */
                this.CBO01_RRM6020.SetValue(popup.fsPOM6020);   /* 청구구분 */
                this.CBH01_RRM6030.SetValue(popup.fsPOM6030);   /* 청구화주 */

                // 검수사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_RRM1400.SetValue(TYUserInfo.EmpNo);
                // 인수사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_RRM1420.SetValue(TYUserInfo.EmpNo);

                SetFocus(DTP01_RRM1100);
            }
        }

        #endregion

        //#region Description : 비품년월
        //private void TXT01_PON1530_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON15301_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 비품순번
        //private void TXT01_PON1531_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON15301_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 비품버튼
        //private void BTN61_PON15301_Click(object sender, EventArgs e)
        //{
        //    TYMRGB009S popup = new TYMRGB009S();

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        //this.TXT01_RRN5030.SetValue(popup.fsMAYYMM);     // 비품년월
        //        //this.TXT01_PON1531.SetValue(popup.fsMASEQ);      // 비품순번
        //        //this.TXT01_RRN5030NM.SetValue(popup.fsMABPDESC); // 비품명

        //        //this.TXT01_RRN6020.SetValue(popup.fsJASAN);      // 자산구분
        //        //this.TXT01_PON1621.SetValue(popup.fsLARGE);      // 대분류
        //        //this.TXT01_PON1622.SetValue(popup.fsMIDDLE);     // 중분류
        //        //this.TXT01_PON1623.SetValue(popup.fsSMALL);      // 소분류
        //        //this.TXT01_RRN6020NM.SetValue(popup.fsJASANNM);  // 자산분류명
        //    }
        //}
        //#endregion

        //#region Description : 자산년도
        //private void TXT01_PON1610_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON16101_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산순번
        //private void TXT01_PON1611_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON16101_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 가족코드
        //private void TXT01_PON1612_KeyDown(object sender, KeyEventArgs e)
        //{

        //}
        //#endregion

        //#region Description : 고정자산번호 버튼
        //private void BTN61_PON16101_Click(object sender, EventArgs e)
        //{
        //    TYMRGB010S popup = new TYMRGB010S();

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.TXT01_RRN6010.SetValue(popup.fsFXSYEAR);   // 자산년도
        //        //this.TXT01_PON1611.SetValue(popup.fsFXSSEQ);    // 자산순번
        //        //this.TXT01_PON1612.SetValue(popup.fsFXSSUBNUM); // 가족코드
        //        this.TXT01_RRN6010NM.SetValue(popup.fsFXSNAME); // 자산명

        //        this.TXT01_RRN6020.SetValue(popup.fsJASAN);     // 자산구분
        //        //this.TXT01_PON1621.SetValue(popup.fsLARGE);     // 대분류
        //        //this.TXT01_PON1622.SetValue(popup.fsMIDDLE);    // 중분류
        //        //this.TXT01_PON1623.SetValue(popup.fsSMALL);     // 소분류
        //        this.TXT01_RRN6020NM.SetValue(popup.fsJASANNM); // 자산분류명
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 - 자산구분
        //private void TXT01_PON1620_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON16201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 - 대분류
        //private void TXT01_PON1621_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON16201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 - 중분류
        //private void TXT01_PON1622_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON16201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 - 소분류
        //private void TXT01_PON1623_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PON16201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 버튼
        //private void BTN61_PON16201_Click(object sender, EventArgs e)
        //{
        //    TYMRGB011S popup = new TYMRGB011S();

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.TXT01_RRN6020.SetValue(popup.fsFXSLASCODE); // 자산구분
        //        //this.TXT01_PON1621.SetValue(popup.fsFXSLMCODE);  // 대분류
        //        //this.TXT01_PON1622.SetValue(popup.fsFXSMMCODE);  // 중분류
        //        //this.TXT01_PON1623.SetValue(popup.fsFXSMCODE);   // 소분류
        //        this.TXT01_RRN6020NM.SetValue(popup.fsFXSMDESC); // 자산분류명
        //    }
        //}
        //#endregion

        #region Description : 검수 사번
        private void CBH01_RRM1400_TextChanged(object sender, EventArgs e)
        {
            if (this.CBH01_RRM1400.GetValue().ToString().Length >= 6)
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BEBB293",
                    (this.TXT01_RRM1020.GetValue() + "31"),
                    this.CBH01_RRM1400.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 부서코드
                    this.CBH01_RRM1410.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
                }
            }
        }
        #endregion

        #region Description : 노무비닷컴 체크
        private void Nomubi_Check(string sGUBUN)
        {
            // 노무비닷컴 사용여부 확인(회계 거래처관리)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_9CQFU623",
                this.CBH01_RRN1100.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VNCOGUBN"].ToString() == "Y")
                {
                    this.CBO01_RRN7000.SetReadOnly(false);
                }
                else
                {
                    this.CBO01_RRN7000.SetReadOnly(true);
                }
                if (sGUBUN != "RUN")
                {
                    this.CBO01_RRN7000.SetValue(dt.Rows[0]["VNCOGUBN"].ToString());
                }
            }
        }
        #endregion

        private void TXT01_RRN1320_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}