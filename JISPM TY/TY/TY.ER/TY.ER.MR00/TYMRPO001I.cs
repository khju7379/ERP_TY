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
    ///  TY_P_MR_2BGAF400	구매 - 계정과목 체크
    ///  TY_P_MR_2BM5E581	구매 - 고정자산 체크
    ///  TY_P_MR_2BGAI403	구매 - 기타본예산 체크
    ///  TY_P_MR_2BM5N583	구매 - 비품 체크
    ///  TY_P_MR_2BGAH402	구매 - 소모성 예산 체크
    ///  TY_P_MR_2BG1A409	구매 - 입고(고정자산생성번호) 체크
    ///  TY_P_MR_2BM4T579	구매 - 자산분류코드 체크
    ///  TY_P_MR_2BGA8376	구매 - 장기계약 내역 값 가져오기
    ///  TY_P_MR_2BGAG401	구매 - 투자수선 체크
    ///  TY_P_MR_2BGAJ404	구매 - 품목코드 체크
    ///  TY_P_MR_2BM87600	구매발주 - 같은 예산에 등록된 품목 건수 체크
    ///  TY_P_MR_2BJ11461	구매발주 - 결재완료 체크
    ///  TY_P_MR_2BN9T609	구매발주 - 구매요청 발주번호, 발주수량, 잔량 업데이트
    ///  TY_P_MR_2BLB0526	구매발주 - 구매요청 체크
    ///  TY_P_MR_2BM7E593	구매발주 - 요청 잔량, 요청 잔액 가져오기
    ///  TY_P_MR_2BJ19460	구매발주 - 입고번호체크
    ///  TY_P_MR_2BNAX611	구매발주 귀속별 예산파일 등록 - 팝업
    ///  TY_P_MR_2BM99603	구매발주 귀속별 예산파일 삭제 - 팝업
    ///  TY_P_MR_2BM91604	구매발주 귀속별 예산파일 수정 - 팝업
    ///  TY_P_MR_2BM90601	구매발주 내역사항 - 예산 업데이트(MINUS)
    ///  TY_P_MR_2BNBE617	구매발주 내역사항 - 예산 업데이트(PLUS)
    ///  TY_P_MR_2BNB4612	구매발주 내역사항 등록 - 팝업
    ///  TY_P_MR_2BM92605	구매발주 내역사항 삭제 - 팝업
    ///  TY_P_MR_2BNB0613	구매발주 내역사항 수정 - 팝업
    ///  TY_P_MR_2BJ1A466	구매발주 내역사항 조회 - 팝업
    ///  TY_P_MR_2BM73592	구매발주 내역사항 확인 - 팝업
    ///  TY_P_MR_2BLB8527	구매발주 마스터 - 순번 가져오기
    ///  TY_P_MR_2BL6T533	구매발주 마스터 등록 - 팝업
    ///  TY_P_MR_2BL7H539	구매발주 마스터 등록시 - 구매발주 내역 등록
    ///  TY_P_MR_2BL7C538	구매발주 마스터 등록시 - 구매발주 예산 등록
    ///  TY_P_MR_2BL7L542	구매발주 마스터 등록시 - 구매요청 내역 발주관련 업데이트
    ///  TY_P_MR_2BL8E546	구매발주 마스터 등록시 - 구매요청 마스터 발주번호 업데이트
    ///  TY_P_MR_2BR1Z671	구매발주 마스터 등록시 - 예산 업데이트
    ///  TY_P_MR_2BM94606	구매발주 마스터 발주금액 업데이트 - 팝업
    ///  TY_P_MR_2BL6V535	구매발주 마스터 삭제 - 팝업
    ///  TY_P_MR_2BL6V534	구매발주 마스터 수정 - 팝업
    ///  TY_P_MR_2BJ1A465	구매발주 마스터(예산조회-거래처별) - 팝업
    ///  TY_P_MR_2BJ1A464	구매발주 마스터(예산조회-귀속부서별) - 팝업
    ///  TY_P_MR_2BLAW555	구매발주 특기사항 등록 - 팝업
    ///  TY_P_MR_2BLAY557	구매발주 특기사항 삭제 - 팝업
    ///  TY_P_MR_2BLAX556	구매발주 특기사항 수정 - 팝업
    ///  TY_P_MR_2BJ1A467	구매발주 특기사항 조회 - 팝업
    ///  TY_P_MR_2BLB1558	구매발주 특기사항 체크
    ///  TY_P_MR_2BJ15462	구매발주 확인 - 팝업
    ///  TY_P_MR_2BK38495	구매요청 - 내역파일 존재유무 체크
    ///  TY_P_MR_2BG51426	구매요청 - 예산 업데이트(PLUS)
    ///  TY_P_MR_2BK36493	구매요청 - 예산파일 존재유무 체크
    ///  TY_P_MR_2BEBB293	사번 - 부서명 가져오기
    ///     
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2BJ6B478	구매발주 내역사항 조회 - 팝업
    ///  TY_S_MR_2BJ5Z475	구매발주 마스터(예산조회-거래처별) - 팝업
    ///  TY_S_MR_2BJ69476	구매발주 마스터(예산조회-귀속부서별) - 팝업
    ///  TY_S_MR_2BJ65477	구매발주 특기사항 조회 - 팝업
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870	삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871	저장하시겠습니까?
    ///  TY_M_GB_23NAD872	삭제하시겠습니까?
    ///  TY_M_GB_23NAD873	저장하였습니다.
    ///  TY_M_GB_23NAD874	삭제하였습니다.
    ///  TY_M_GB_2452W459	저장할 데이터가 없습니다.
    ///  TY_M_MR_2BC51262	결재 완료 된 문서가 아닙니다.
    ///  TY_M_MR_2BC59266	결재 완료 된 자료이므로 작업이 불가합니다.
    ///  TY_M_MR_2BD3Y285	수정하시겠습니까?
    ///  TY_M_MR_2BD3Z286	수정하였습니다.
    ///  TY_M_MR_2BE2B295	검토 부서를 입력하세요.
    ///  TY_M_MR_2BE2C296	납기 일자를 확인하세요.
    ///  TY_M_MR_2BE2D299	공사 및 구매명을 입력하세요.
    ///  TY_M_MR_2BE2D300	지불 조건을 입력하세요.
    ///  TY_M_MR_2BE2D301	화폐 구분을 입력하세요.
    ///  TY_M_MR_2BE2D302	인도 조건을 입력하세요.
    ///  TY_M_MR_2BE2D303	인도 지역을 입력하세요.
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
    ///  TY_M_MR_2BJ4K470	입고자료에 발주번호가 존재합니다.
    ///  TY_M_MR_2BK3C496	요청 사업부를 입력하세요.
    ///  TY_M_MR_2BK3C497	요청 순번을 입력하세요.
    ///  TY_M_MR_2BK3D498	요청 년월을 입력하세요.
    ///  TY_M_MR_2BK3D499	발의 사번을 입력하세요.
    ///  TY_M_MR_2BK3D500	요청 번호를 입력하세요.
    ///  TY_M_MR_2BK3G501	년월은 6자리입니다.
    ///  TY_M_MR_2BK3H504	사업부를 확인하세요.
    ///  TY_M_MR_2BL71536	구매요청 예산 파일이 존재하지 않습니다.
    ///  TY_M_MR_2BL71537	구매요청 내역 파일이 존재하지 않습니다.
    ///  TY_M_MR_2BLAQ525	발의 일자를 확인하세요.
    ///  TY_M_MR_2BM51580	자산 분류 코드를 확인하세요.
    ///  TY_M_MR_2BM5J582	고정자산 번호를 확인하세요.
    ///  TY_M_MR_2BM5O584	비품 번호를 확인하세요.
    ///  TY_M_MR_2BM5T588	비품 구분을 확인하세요.
    ///  TY_M_MR_2BM7R596	발주 단가를 입력하세요.
    ///  TY_M_MR_2BM7R597	발주 수량을 입력하세요.
    ///  TY_M_MR_2BM7Y598	발주수량이 요청수량보다 많습니다.
    ///  TY_M_MR_2BM7Y599	발주금액이 요청금액보다 많습니다.
    ///  TY_M_MR_2BRAX669	수량은 '1' 입니다.
    ///  TY_M_MR_2BU93766   발주 취소일을 입력하세요.
    ///  TY_M_MR_2BU93767   취소 사유를 입력하세요.
    ///  
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  NEW_MRP_NF : 신규(내역)
    ///  NEW_MRP_TF : 신규(특기)
    /// </summary>
    public partial class TYMRPO001I : TYBase
    {
        //private TYData DAT01_PRTHISAB;

        public string fsPOM1000 = string.Empty;
        public string fsPOM1010 = string.Empty;
        public string fsPOM1020 = string.Empty;
        public string fsPOM1030 = string.Empty;

        public string fsPRM1000 = string.Empty;
        public string fsPRM1010 = string.Empty;
        public string fsPRM1020 = string.Empty;
        public string fsPRM1030 = string.Empty;

        private string fsYESAN_COUNT = string.Empty;
        private string fsPOM5110     = string.Empty;

        private string fsPRM5100 = string.Empty;
        private string fsPOM1400 = string.Empty;
        private string fsPOM1410 = string.Empty;
        private string fsPOM1420 = string.Empty;

        private string fsGUBUN = string.Empty;
        private string fsMAGAM = string.Empty;

        #region Description : 페이지 로드
        public TYMRPO001I(string sPOM1000, string sPOM1010, string sPOM1020, string sPOM1030)
        {
            InitializeComponent();

            this.SetPopupStyle();

            if (sPOM1010 == "P")
            {
                // 파라미터값 가져오기
                this.fsPRM1000 = sPOM1000;
                this.fsPRM1010 = sPOM1010;
                this.fsPRM1020 = sPOM1020;
                this.fsPRM1030 = sPOM1030;

                this.fsPOM1010 = "O";
            }
            else
            {
                // 파라미터값 가져오기
                this.fsPOM1000 = sPOM1000;
                this.fsPOM1010 = sPOM1010;
                this.fsPOM1020 = sPOM1020;
                this.fsPOM1030 = sPOM1030;
            }
            this.TXT01_POM1000.SetValue(fsPOM1000);
            this.TXT01_POM1010.SetValue(fsPOM1010);
            this.TXT01_POM1020.SetValue(fsPOM1020);
            this.TXT01_POM1030.SetValue(fsPOM1030);

            this.TXT01_PON1000.SetValue(fsPOM1000);
            this.TXT01_PON1010.SetValue(fsPOM1010);
            this.TXT01_PON1020.SetValue(fsPOM1020);
            this.TXT01_PON1030.SetValue(fsPOM1030);

            this.TXT01_POT1000.SetValue(fsPOM1000);
            this.TXT01_POT1010.SetValue(fsPOM1010);
            this.TXT01_POT1020.SetValue(fsPOM1020);
            this.TXT01_POT1030.SetValue(fsPOM1030);
        }

        private void TYMRPO001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRM10001.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_PON10701.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN61_PON15301.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN61_PON16101.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_SAV.ProcessCheck  += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(BTN61_EDIT_ProcessCheck);
            this.BTN61_REM.ProcessCheck  += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);
            //this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2BJ65477, "POT1040");

            bool fResult;

            // 등록
            if (this.TXT01_POM1000.GetValue().ToString() == ""  &&
                this.TXT01_POM1010.GetValue().ToString() == "O" &&
                this.TXT01_POM1020.GetValue().ToString() == ""  &&
                this.TXT01_POM1030.GetValue().ToString() == ""
                )
            {
                fsGUBUN = "MRPPOMF";

                // 컨트롤 초기화
                UP_Control_Initialize("MRPPOMF", true);

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                // 탭 컨트롤
                tabControl1_Enable("");

                // 화폐구분
                this.CBH01_POM1700.SetValue("1");
                // 당사지불조건
                this.CBH01_POM1730.SetValue("01");

                // 비용청구
                this.CBO01_POM6010.SetValue("N");
                // 청구구분
                this.CBO01_POM6020.SetValue("3");

                // 발주년월
                this.TXT01_POM1020.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6));

                // 발주사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_POM1140.SetValue(TYUserInfo.EmpNo);

                // 등록 시 요청부서의 앞자리 가져옴
                this.TXT01_POM1000.SetValue(this.TXT01_POM1130.GetValue().ToString().Substring(0, 1));

                SetStartingFocus(this.TXT01_POM1000);

                // 미발주 그리드에서 선택시
                if (fsPRM1010 == "P")
                {
                    UP_SetPRMData();

                    this.TXT01_PRM1000.ReadOnly = true;
                    this.TXT01_PRM1010.ReadOnly = true;
                    this.TXT01_PRM1020.ReadOnly = true;
                    this.TXT01_PRM1030.ReadOnly = true;

                    this.BTN61_PRM10001.Enabled = false;
                }
            }
            else // 수정
            {
                fsMAGAM = "";

                this.TXT01_POM1000.SetReadOnly(true);
                this.TXT01_POM1010.SetReadOnly(true);
                this.TXT01_POM1020.SetReadOnly(true);
                this.TXT01_POM1030.SetReadOnly(true);

                fsGUBUN = "MRPPOMF";

                // 컨트롤 초기화
                UP_Control_Initialize("MRPPOMF", true);

                // 마스터 DISPLAY
                UP_MRPPOMF_DISPLAY();

                // 내역사항 DISPLAY
                UP_MRPPONF_DISPLAY();

                // 특기사항 DISPLAY
                UP_MRPPOTF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    this.SetFocus(this.DTP01_POM1100);
                    //SetStartingFocus(this.DTP01_POM1100);
                }
                else
                {
                    if (TYUserInfo.EmpNo.ToString() == "0377-M")
                    {
                        fsMAGAM = "MAGAM";

                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("5", false);

                        this.SetFocus(this.DTP01_POM1100);
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

        #region Description : 내역사항 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            UP_ImgbtnDisplay("2", false);

            // 컨트롤 초기화
            UP_Control_Initialize("MRPPONF", false);

            this.CBH01_PON1040.SetValue("");
            this.CBH01_PON1060.SetValue("");

            this.TXT01_PON1070.SetValue("");
            this.TXT01_PON1070NM.SetValue("");
            this.TXT01_PON1080.SetValue("");
            this.TXT01_PON1080NM.SetValue("");
            this.TXT01_PON1090.SetValue("");
            this.TXT01_PON1090NM.SetValue("");
            this.TXT01_PON1092.SetValue("");

            this.CBH01_PON1050.SetValue("");
            this.CBH01_PON1050.SetText("");

            this.BTN61_PON10701.Enabled = true;

            this.TXT01_PON1092.ReadOnly = true;

            UP_FieldClear("MRPPONF");

            // 선급자재
            if (this.CBO01_POM6010.GetValue().ToString() == "Y" && this.CBO01_POM6020.GetValue().ToString() == "2")
            {
                this.CBH01_PON1060.SetValue("4");

                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (Convert.ToInt32(TXT01_PRN1020.GetValue().ToString()) >= 201901)
                {
                    this.TXT01_PON1070.SetValue("12210000");
                }
                else
                {
                    this.TXT01_PON1070.SetValue("11101001");
                }

                this.CBH01_PON1060.SetReadOnly(true);

                this.TXT01_PON1070.SetReadOnly(true);
                this.TXT01_PON1070NM.SetReadOnly(true);

                this.BTN61_PON10701.Enabled = false;
            }
            else
            {
                this.CBH01_PON1060.SetReadOnly(false);

                this.TXT01_PON1070.SetReadOnly(false);
                this.TXT01_PON1070NM.SetReadOnly(false);

                this.BTN61_PON10701.Enabled = true;
            }

            SetFocus(this.CBH01_PON1040.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "MRPPOMF") // 마스터
            {
                string sOUTMSG = string.Empty;

                string sPOM1120 = string.Empty;
                string sPRM4010 = string.Empty;

                // 요청번호
                sPOM1120 = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

                //// 발주번호
                //sPRM4010 = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

                // 원본
                // 등록
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BL6T533",
                //    this.TXT01_POM1000.GetValue().ToString(),
                //    this.TXT01_POM1010.GetValue().ToString(),
                //    this.TXT01_POM1020.GetValue().ToString(),
                //    this.TXT01_POM1030.GetValue().ToString(),
                //    this.DTP01_POM1100.GetValue().ToString(),
                //    sPOM1120.ToString(),
                //    this.TXT01_POM1130.GetValue().ToString(),
                //    this.CBH01_POM1140.GetValue().ToString(),
                //    this.TXT01_POM1150.GetValue().ToString(),
                //    this.TXT01_POM1160.GetValue().ToString(),
                //    this.DTP01_POM1170.GetValue().ToString(),
                //    this.TXT01_POM1180.GetValue().ToString(),
                //    this.TXT01_POM1200.GetValue().ToString(),
                //    this.TXT01_POM1210.GetValue().ToString(),
                //    this.TXT01_POM1220.GetValue().ToString(),
                //    this.TXT01_POM1230.GetValue().ToString(),
                //    this.TXT01_POM1300.GetValue().ToString(),
                //    this.CBH01_POM1310.GetValue().ToString(),
                //    fsPOM1400.ToString(),
                //    fsPOM1410.ToString(),
                //    fsPOM1420.ToString(),
                //    "", // 입고번호
                //    this.CBH01_POM1700.GetValue().ToString(),
                //    this.CBO01_POM1710.GetValue().ToString(),
                //    this.TXT01_POM1720.GetValue().ToString(),
                //    this.CBH01_POM1730.GetValue().ToString(),
                //    this.CBO01_POM1910.GetValue().ToString(),
                //    this.CBO01_POM6010.GetValue().ToString(),
                //    this.CBO01_POM6020.GetValue().ToString(),
                //    this.CBH01_POM6030.GetValue().ToString(),
                //    TYUserInfo.EmpNo.ToString()
                //    );

                string sAUT1030 = string.Empty;

                // 수정(구매발주 SP)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_37U3Y280",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.DTP01_POM1100.GetValue().ToString(),
                    sPOM1120.ToString(),
                    this.TXT01_POM1130.GetValue().ToString(),
                    this.CBH01_POM1140.GetValue().ToString(),
                    this.TXT01_POM1150.GetValue().ToString(),
                    this.TXT01_POM1160.GetValue().ToString(),
                    this.DTP01_POM1170.GetValue().ToString(),
                    this.TXT01_POM1180.GetValue().ToString(),
                    this.TXT01_POM1200.GetValue().ToString(),
                    this.TXT01_POM1210.GetValue().ToString(),
                    this.TXT01_POM1220.GetValue().ToString(),
                    this.TXT01_POM1230.GetValue().ToString(),
                    this.TXT01_POM1300.GetValue().ToString(),
                    this.CBH01_POM1310.GetValue().ToString(),
                    fsPOM1400.ToString(),
                    fsPOM1410.ToString(),
                    fsPOM1420.ToString(),
                    "", // 입고번호
                    this.CBH01_POM1700.GetValue().ToString(),
                    this.CBO01_POM1710.GetValue().ToString(),
                    this.TXT01_POM1720.GetValue().ToString(),
                    this.CBH01_POM1730.GetValue().ToString(),
                    this.CBO01_POM1910.GetValue().ToString(),
                    this.CBO01_POM6010.GetValue().ToString(),
                    this.CBO01_POM6020.GetValue().ToString(),
                    this.CBH01_POM6030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    sAUT1030.ToString()
                    );

                sAUT1030 = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sAUT1030.Substring(0, 2).ToString() == "OK")
                {
                    this.TXT01_POM1030.SetValue(sAUT1030.Substring(3, 4));

                    this.ShowMessage("TY_M_GB_23NAD873");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_246A2488");

                    return;
                }


                // 구매발주 예산파일 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BL7C538",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString().Substring(0, 4),
                    this.TXT01_POM1020.GetValue().ToString().Substring(4, 2),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                    );

                // 구매발주 내역파일 등록
                this.DbConnector.Attach
                    (
                    "TY_P_MR_9CRBG625",     // 노무비닷컴 적용후
                    //"TY_P_MR_2BL7H539",   // 노무비닷컴 적용전
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                    );

                // 구매발주 특기파일 등록
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CD4H140",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                    );

                this.DbConnector.ExecuteTranQueryList();

                sOUTMSG = "OK";

                // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                if (this.fsPRM5100.ToString() != "Y")
                {
                    // 발주년월과 요청년월이 다를 경우 발주내역금액 예산테이블에 플러스 시킴
                    if (this.TXT01_POM1020.GetValue().ToString() != this.TXT01_PRM1020.GetValue().ToString())
                    {
                        // 예산 가용금액 - 플러스
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BR1Z671",
                            this.TXT01_POM1000.GetValue().ToString(),
                            this.TXT01_POM1010.GetValue().ToString(),
                            this.TXT01_POM1020.GetValue().ToString(),
                            this.TXT01_POM1030.GetValue().ToString(),
                            sOUTMSG.ToString()
                            );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    }
                }

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 구매요청 내역파일 발주관련 필드 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BL7L542",
                        this.TXT01_PRM1000.GetValue().ToString(),
                        this.TXT01_PRM1010.GetValue().ToString(),
                        this.TXT01_PRM1020.GetValue().ToString(),
                        Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                        );

                    // 발주번호
                    sPRM4010 = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

                    // 구매요청 마스터 발주번호 업데이트
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BL8E546",
                        "Y",  // 발주완료구분
                        sPRM4010.ToString(), // 발주번호
                        this.TXT01_PRM1000.GetValue().ToString(),
                        this.TXT01_PRM1010.GetValue().ToString(),
                        this.TXT01_PRM1020.GetValue().ToString(),
                        Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                        );

                    this.DbConnector.ExecuteNonQueryList();
                    this.ShowMessage("TY_M_GB_23NAD873");

                    // 탭 컨트롤
                    tabControl1_Enable("MRPPOMF");

                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("1", true);

                    this.TXT01_POM1000.ReadOnly = true;
                    this.TXT01_POM1010.ReadOnly = true;
                    this.TXT01_POM1020.ReadOnly = true;
                    this.TXT01_POM1030.ReadOnly = true;

                    UP_MRPPOMF_DISPLAY();
                }
            }
            else if (fsGUBUN == "MRPPONF") // 내역사항
            {
                string sOUTMSG = string.Empty;

                sOUTMSG = "OK";

                string sPON1530 = string.Empty;
                string sPON1610 = string.Empty;
                string sPON1140 = string.Empty;

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CH3M224",
                    this.CBH01_PON1050.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sPON1140 = dt.Rows[0]["Z105023"].ToString();
                }

                sPON1530 = "";
                // 비품번호
                if (this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "")
                {
                    sPON1530 = this.TXT01_PON1530.GetValue().ToString() + Set_Fill3(this.TXT01_PON1531.GetValue().ToString());
                }

                sPON1610 = "";
                // 고정자산번호
                if (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "")
                {
                    sPON1610 = this.TXT01_PON1610.GetValue().ToString() + Set_Fill4(this.TXT01_PON1611.GetValue().ToString()) + Set_Fill3(this.TXT01_PON1612.GetValue().ToString());
                }

                // 노무비닷컴 적용 후
                // 내역사항 등록 및 구매요청 관련 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_9CQHG624",
                    this.TXT01_PON1000.GetValue().ToString(),
                    this.TXT01_PON1010.GetValue().ToString(),
                    this.TXT01_PON1020.GetValue().ToString(),
                    this.TXT01_PON1030.GetValue().ToString(),
                    this.CBH01_PON1040.GetValue().ToString(),
                    this.CBH01_PON1050.GetValue().ToString(),
                    this.CBH01_PON1060.GetValue().ToString(),
                    this.TXT01_PON1070.GetValue().ToString(),
                    this.TXT01_PON1080.GetValue().ToString(),
                    this.TXT01_PON1090.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                    this.CBH01_PON1100.GetValue().ToString(),
                    this.CBH01_PON1110.GetValue().ToString(),
                    this.CBO01_PON1120.GetValue().ToString(),
                    this.CBH01_PON1130.GetValue().ToString(),
                    sPON1140.ToString(),
                    //this.CBH01_PON1130.GetText().ToString(),
                    this.TXT01_PON1150.GetValue().ToString(),
                    this.TXT01_PON1160.GetValue().ToString(),
                    this.TXT01_PON1170.GetValue().ToString(),
                    this.TXT01_PON1180.GetValue().ToString(),
                    this.TXT01_PON1200.GetValue().ToString(),
                    this.TXT01_PON1210.GetValue().ToString(),
                    this.TXT01_PON1220.GetValue().ToString(),
                    this.TXT01_PON1230.GetValue().ToString(),
                    this.TXT01_PON1300.GetValue().ToString(),
                    this.TXT01_PON1310.GetValue().ToString(),
                    this.TXT01_PON1320.GetValue().ToString(),
                    this.TXT01_PON1330.GetValue().ToString(),
                    this.CBO01_PON1500.GetValue().ToString(),
                    this.CBO01_PON1510.GetValue().ToString(),
                    sPON1530.ToString(),
                    sPON1610.ToString(),
                    this.CBH01_PON1620.GetValue().ToString(),
                    this.CBO01_PON1630.GetValue().ToString(),
                    this.CBO01_PON7000.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    this.TXT01_PRN1020.GetValue().ToString(),
                    this.TXT01_PRN1030.GetValue().ToString(),
                    "A",
                    sOUTMSG.ToString()
                    );
                // 노무비닷컴 적용전
                //// 내역사항 등록 및 구매요청 관련 업데이트
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BNB4612",
                //    this.TXT01_PON1000.GetValue().ToString(),
                //    this.TXT01_PON1010.GetValue().ToString(),
                //    this.TXT01_PON1020.GetValue().ToString(),
                //    this.TXT01_PON1030.GetValue().ToString(),
                //    this.CBH01_PON1040.GetValue().ToString(),
                //    this.CBH01_PON1050.GetValue().ToString(),
                //    this.CBH01_PON1060.GetValue().ToString(),
                //    this.TXT01_PON1070.GetValue().ToString(),
                //    this.TXT01_PON1080.GetValue().ToString(),
                //    this.TXT01_PON1090.GetValue().ToString(),
                //    Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                //    this.CBH01_PON1100.GetValue().ToString(),
                //    this.CBH01_PON1110.GetValue().ToString(),
                //    this.CBO01_PON1120.GetValue().ToString(),
                //    this.CBH01_PON1130.GetValue().ToString(),
                //    sPON1140.ToString(),
                //    //this.CBH01_PON1130.GetText().ToString(),
                //    this.TXT01_PON1150.GetValue().ToString(),
                //    this.TXT01_PON1160.GetValue().ToString(),
                //    this.TXT01_PON1170.GetValue().ToString(),
                //    this.TXT01_PON1180.GetValue().ToString(),
                //    this.TXT01_PON1200.GetValue().ToString(),
                //    this.TXT01_PON1210.GetValue().ToString(),
                //    this.TXT01_PON1220.GetValue().ToString(),
                //    this.TXT01_PON1230.GetValue().ToString(),
                //    this.TXT01_PON1300.GetValue().ToString(),
                //    this.TXT01_PON1310.GetValue().ToString(),
                //    this.TXT01_PON1320.GetValue().ToString(),
                //    this.TXT01_PON1330.GetValue().ToString(),
                //    this.CBO01_PON1500.GetValue().ToString(),
                //    this.CBO01_PON1510.GetValue().ToString(),
                //    sPON1530.ToString(),
                //    sPON1610.ToString(),
                //    this.CBH01_PON1620.GetValue().ToString(),
                //    this.CBO01_PON1630.GetValue().ToString(),
                //    TYUserInfo.EmpNo.ToString(),
                //    this.TXT01_PRN1000.GetValue().ToString(),
                //    this.TXT01_PRN1010.GetValue().ToString(),
                //    this.TXT01_PRN1020.GetValue().ToString(),
                //    this.TXT01_PRN1030.GetValue().ToString(),
                //    "A",
                //    sOUTMSG.ToString()
                //    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 귀속별 예산파일 등록
                    if (fsYESAN_COUNT.ToString() == "0") // 등록
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BNAX611",
                            this.TXT01_PON1000.GetValue().ToString(),
                            this.TXT01_PON1010.GetValue().ToString(),
                            this.TXT01_PON1020.GetValue().ToString(),
                            this.TXT01_PON1030.GetValue().ToString(),
                            this.CBH01_PON1040.GetValue().ToString(),
                            this.CBH01_PON1060.GetValue().ToString(),
                            this.TXT01_PON1070.GetValue().ToString(),
                            this.TXT01_PON1080.GetValue().ToString(),
                            this.TXT01_PON1090.GetValue().ToString(),
                            this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                            this.TXT01_POO1140.GetValue().ToString().Substring(4, 2),
                            TYUserInfo.EmpNo.ToString()
                            );
                    }
                    else // 수정
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM91604",
                            this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                            this.TXT01_POO1140.GetValue().ToString().Substring(4, 2),
                            TYUserInfo.EmpNo.ToString(),
                            this.TXT01_PON1000.GetValue().ToString(),
                            this.TXT01_PON1010.GetValue().ToString(),
                            this.TXT01_PON1020.GetValue().ToString(),
                            this.TXT01_PON1030.GetValue().ToString(),
                            this.CBH01_PON1040.GetValue().ToString(),
                            this.CBH01_PON1060.GetValue().ToString(),
                            this.TXT01_PON1070.GetValue().ToString(),
                            this.TXT01_PON1080.GetValue().ToString(),
                            this.TXT01_PON1090.GetValue().ToString()
                            );
                    }

                    this.DbConnector.ExecuteNonQuery();

                    // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                    // 선급자재일 경우 예산 업데이트 안함.
                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    if (this.fsPRM5100.ToString() != "Y" && (this.TXT01_PON1070.GetValue().ToString() != "11101001" && this.TXT01_PON1070.GetValue().ToString() != "12210000"))
                    {
                        // 예산 가용금액 - 플러스
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BNBE617",
                            this.TXT01_PON1000.GetValue().ToString(),
                            this.TXT01_PON1010.GetValue().ToString(),
                            this.TXT01_PON1020.GetValue().ToString(),
                            this.TXT01_PON1030.GetValue().ToString(),
                            this.CBH01_PON1040.GetValue().ToString(),
                            this.CBH01_PON1050.GetValue().ToString(),
                            this.CBH01_PON1060.GetValue().ToString(),
                            this.TXT01_PON1070.GetValue().ToString(),
                            this.TXT01_PON1080.GetValue().ToString(),
                            this.TXT01_PON1090.GetValue().ToString(),
                            Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                            sOUTMSG.ToString()
                            );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    }

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // 마스터 테이블에 발주금액 업데이트
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM94606",
                            this.TXT01_PON1000.GetValue().ToString(),
                            this.TXT01_PON1010.GetValue().ToString(),
                            this.TXT01_PON1020.GetValue().ToString(),
                            this.TXT01_PON1030.GetValue().ToString()
                            );

                        this.DbConnector.ExecuteNonQuery();

                        this.ShowMessage("TY_M_GB_23NAD873");

                        // 버튼 초기화
                        UP_ImgbtnDisplay("3", false);

                        // 컨트롤 초기화
                        UP_Control_Initialize("MRPPONF", true);

                        this.CBH01_PON1040.SetValue("");
                        this.CBH01_PON1060.SetValue("");

                        this.TXT01_PON1070.SetValue("");
                        this.TXT01_PON1070NM.SetValue("");
                        this.TXT01_PON1080.SetValue("");
                        this.TXT01_PON1080NM.SetValue("");
                        this.TXT01_PON1090.SetValue("");
                        this.TXT01_PON1090NM.SetValue("");
                        this.TXT01_PON1092.SetValue("");
                        this.CBH01_PON1050.SetValue("");
                        this.CBH01_PON1050.SetText("");

                        UP_FieldClear("MRPPONF");

                        UP_MRPPONF_DISPLAY();

                        SetFocus(this.CBH01_PON1040.CodeText);
                    }
                }
            }
        }
        #endregion

        #region Description : 수정 버튼
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "MRPPOMF") // 마스터
            {
                if (TYUserInfo.EmpNo.ToString() == "0377-M" && fsMAGAM.ToString() == "MAGAM")
                {   
                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_BBU90820",
                        this.TXT01_POM1300.GetValue().ToString().Replace("-",""),
                        this.CBH01_POM1310.GetValue().ToString(),
                        this.CBH01_POM1730.GetValue().ToString(),
                        this.CBO01_POM1910.GetValue().ToString(),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_POM1000.GetValue().ToString(),
                        this.TXT01_POM1010.GetValue().ToString(),
                        this.TXT01_POM1020.GetValue().ToString(),
                        this.TXT01_POM1030.GetValue().ToString()
                        );

                    //// 수정
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach
                    //    (
                    //    "TY_P_MR_34I2G527",
                    //    this.CBH01_POM1730.GetValue().ToString(),
                    //    this.CBO01_POM1910.GetValue().ToString(),
                    //    TYUserInfo.EmpNo.ToString(),
                    //    this.TXT01_POM1000.GetValue().ToString(),
                    //    this.TXT01_POM1010.GetValue().ToString(),
                    //    this.TXT01_POM1020.GetValue().ToString(),
                    //    this.TXT01_POM1030.GetValue().ToString()
                    //    );


                    
                }
                else
                {
                    string sPOM1120 = string.Empty;

                    // 요청번호
                    sPOM1120 = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BL6V534",
                        this.DTP01_POM1100.GetValue().ToString(),
                        sPOM1120.ToString(),
                        this.TXT01_POM1130.GetValue().ToString(),
                        this.CBH01_POM1140.GetValue().ToString(),
                        this.TXT01_POM1150.GetValue().ToString(),
                        this.TXT01_POM1160.GetValue().ToString(),
                        this.DTP01_POM1170.GetValue().ToString(),
                        this.TXT01_POM1180.GetValue().ToString(),
                        this.TXT01_POM1200.GetValue().ToString(),
                        this.TXT01_POM1210.GetValue().ToString(),
                        this.TXT01_POM1220.GetValue().ToString(),
                        this.TXT01_POM1230.GetValue().ToString(),
                        this.TXT01_POM1300.GetValue().ToString(),
                        this.CBH01_POM1310.GetValue().ToString(),
                        fsPOM1400.ToString(),
                        fsPOM1410.ToString(),
                        fsPOM1420.ToString(),
                        "", // 입고번호
                        this.CBH01_POM1700.GetValue().ToString(),
                        this.CBO01_POM1710.GetValue().ToString(),
                        this.TXT01_POM1720.GetValue().ToString(),
                        this.CBH01_POM1730.GetValue().ToString(),
                        this.CBO01_POM1910.GetValue().ToString(),
                        this.CBO01_POM6010.GetValue().ToString(),
                        this.CBO01_POM6020.GetValue().ToString(),
                        this.CBH01_POM6030.GetValue().ToString(),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_POM1000.GetValue().ToString(),
                        this.TXT01_POM1010.GetValue().ToString(),
                        this.TXT01_POM1020.GetValue().ToString(),
                        this.TXT01_POM1030.GetValue().ToString()
                        );
                }

                this.DbConnector.ExecuteNonQuery();
                this.ShowMessage("TY_M_MR_2BD3Z286");

                // 탭 컨트롤
                tabControl1_Enable("MRPPOMF");

                UP_MRPPOMF_DISPLAY();
            }
            else if (fsGUBUN == "MRPPONF") // 내역사항
            {
                // 노무비닷컴 수정 권한체크
                if (TYUserInfo.EmpNo.ToString() == "0377-M" && fsMAGAM.ToString() == "MAGAM")
                {
                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_9CVD1634",
                        this.CBO01_PON7000.GetValue().ToString(),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_PON1000.GetValue().ToString(),
                        this.TXT01_PON1010.GetValue().ToString(),
                        this.TXT01_PON1020.GetValue().ToString(),
                        this.TXT01_PON1030.GetValue().ToString(),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.CBH01_PON1050.GetValue().ToString(),
                        this.CBH01_PON1060.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1080.GetValue().ToString(),
                        this.TXT01_PON1090.GetValue().ToString(),
                        this.TXT01_PON1092.GetValue().ToString()
                        );

                    this.DbConnector.ExecuteNonQuery();

                    this.ShowMessage("TY_M_MR_2BD3Z286");

                    // 버튼 초기화
                    UP_ImgbtnDisplay("3", false);

                    // 컨트롤 초기화
                    UP_Control_Initialize("MRPPONF", true);

                    this.CBH01_PON1040.SetValue("");
                    this.CBH01_PON1060.SetValue("");

                    this.TXT01_PON1070.SetValue("");
                    this.TXT01_PON1070NM.SetValue("");
                    this.TXT01_PON1080.SetValue("");
                    this.TXT01_PON1080NM.SetValue("");
                    this.TXT01_PON1090.SetValue("");
                    this.TXT01_PON1090NM.SetValue("");
                    this.TXT01_PON1092.SetValue("");
                    this.CBH01_PON1050.SetValue("");
                    this.CBH01_PON1050.SetText("");

                    UP_FieldClear("MRPPONF");

                    UP_MRPPONF_DISPLAY();

                    SetFocus(this.CBH01_PON1040.CodeText);
                }
                else
                {
                    string sOUTMSG = string.Empty;

                    string sPON1530 = string.Empty;
                    string sPON1610 = string.Empty;
                    string sPON1140 = string.Empty;

                    DataTable dt = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2CH3M224",
                        this.CBH01_PON1050.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sPON1140 = dt.Rows[0]["Z105023"].ToString();
                    }

                    sPON1530 = "";
                    // 비품번호
                    if (this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "")
                    {
                        sPON1530 = this.TXT01_PON1530.GetValue().ToString() + Set_Fill3(this.TXT01_PON1531.GetValue().ToString());
                    }

                    sPON1610 = "";
                    // 고정자산번호
                    if (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "")
                    {
                        sPON1610 = this.TXT01_PON1610.GetValue().ToString() + Set_Fill4(this.TXT01_PON1611.GetValue().ToString()) + Set_Fill3(this.TXT01_PON1612.GetValue().ToString());
                    }

                    sOUTMSG = "OK";

                    // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                    // 선급자재일 경우 예산 업데이트 안함.
                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    if (this.fsPRM5100.ToString() != "Y" && (this.TXT01_PON1070.GetValue().ToString() != "11101001" && this.TXT01_PON1070.GetValue().ToString() != "12210000"))
                    {
                        // 예산 가용금액 - 마이너스
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM90601",
                            this.TXT01_PON1000.GetValue().ToString(),
                            this.TXT01_PON1010.GetValue().ToString(),
                            this.TXT01_PON1020.GetValue().ToString(),
                            this.TXT01_PON1030.GetValue().ToString(),
                            this.CBH01_PON1040.GetValue().ToString(),
                            this.CBH01_PON1050.GetValue().ToString(),
                            this.CBH01_PON1060.GetValue().ToString(),
                            this.TXT01_PON1070.GetValue().ToString(),
                            this.TXT01_PON1080.GetValue().ToString(),
                            this.TXT01_PON1090.GetValue().ToString(),
                            Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                            sOUTMSG.ToString()
                            );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    }

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // 노무비닷컴 적용후
                        // 내역사항 수정 및 구매요청 관련 업데이트
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_9CQHG624",
                            this.TXT01_PON1000.GetValue().ToString(),
                            this.TXT01_PON1010.GetValue().ToString(),
                            this.TXT01_PON1020.GetValue().ToString(),
                            this.TXT01_PON1030.GetValue().ToString(),
                            this.CBH01_PON1040.GetValue().ToString(),
                            this.CBH01_PON1050.GetValue().ToString(),
                            this.CBH01_PON1060.GetValue().ToString(),
                            this.TXT01_PON1070.GetValue().ToString(),
                            this.TXT01_PON1080.GetValue().ToString(),
                            this.TXT01_PON1090.GetValue().ToString(),
                            Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                            this.CBH01_PON1100.GetValue().ToString(),
                            this.CBH01_PON1110.GetValue().ToString(),
                            this.CBO01_PON1120.GetValue().ToString(),
                            this.CBH01_PON1130.GetValue().ToString(),
                            sPON1140.ToString(),
                            //this.CBH01_PON1130.GetText().ToString(),
                            this.TXT01_PON1150.GetValue().ToString(),
                            this.TXT01_PON1160.GetValue().ToString(),
                            this.TXT01_PON1170.GetValue().ToString(),
                            this.TXT01_PON1180.GetValue().ToString(),
                            this.TXT01_PON1200.GetValue().ToString(),
                            this.TXT01_PON1210.GetValue().ToString(),
                            this.TXT01_PON1220.GetValue().ToString(),
                            this.TXT01_PON1230.GetValue().ToString(),
                            this.TXT01_PON1300.GetValue().ToString(),
                            this.TXT01_PON1310.GetValue().ToString(),
                            this.TXT01_PON1320.GetValue().ToString(),
                            this.TXT01_PON1330.GetValue().ToString(),
                            this.CBO01_PON1500.GetValue().ToString(),
                            this.CBO01_PON1510.GetValue().ToString(),
                            sPON1530.ToString(),
                            sPON1610.ToString(),
                            this.CBH01_PON1620.GetValue().ToString(),
                            this.CBO01_PON1630.GetValue().ToString(),
                            this.CBO01_PON7000.GetValue().ToString(),
                            TYUserInfo.EmpNo.ToString(),
                            this.TXT01_PRN1000.GetValue().ToString(),
                            this.TXT01_PRN1010.GetValue().ToString(),
                            this.TXT01_PRN1020.GetValue().ToString(),
                            this.TXT01_PRN1030.GetValue().ToString(),
                            "C",
                            sOUTMSG.ToString()
                            );

                        // 노무비닷컴 적용전
                        //// 내역사항 수정 및 구매요청 관련 업데이트
                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach
                        //    (
                        //    "TY_P_MR_2BNB4612",
                        //    this.TXT01_PON1000.GetValue().ToString(),
                        //    this.TXT01_PON1010.GetValue().ToString(),
                        //    this.TXT01_PON1020.GetValue().ToString(),
                        //    this.TXT01_PON1030.GetValue().ToString(),
                        //    this.CBH01_PON1040.GetValue().ToString(),
                        //    this.CBH01_PON1050.GetValue().ToString(),
                        //    this.CBH01_PON1060.GetValue().ToString(),
                        //    this.TXT01_PON1070.GetValue().ToString(),
                        //    this.TXT01_PON1080.GetValue().ToString(),
                        //    this.TXT01_PON1090.GetValue().ToString(),
                        //    Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                        //    this.CBH01_PON1100.GetValue().ToString(),
                        //    this.CBH01_PON1110.GetValue().ToString(),
                        //    this.CBO01_PON1120.GetValue().ToString(),
                        //    this.CBH01_PON1130.GetValue().ToString(),
                        //    sPON1140.ToString(),
                        //    //this.CBH01_PON1130.GetText().ToString(),
                        //    this.TXT01_PON1150.GetValue().ToString(),
                        //    this.TXT01_PON1160.GetValue().ToString(),
                        //    this.TXT01_PON1170.GetValue().ToString(),
                        //    this.TXT01_PON1180.GetValue().ToString(),
                        //    this.TXT01_PON1200.GetValue().ToString(),
                        //    this.TXT01_PON1210.GetValue().ToString(),
                        //    this.TXT01_PON1220.GetValue().ToString(),
                        //    this.TXT01_PON1230.GetValue().ToString(),
                        //    this.TXT01_PON1300.GetValue().ToString(),
                        //    this.TXT01_PON1310.GetValue().ToString(),
                        //    this.TXT01_PON1320.GetValue().ToString(),
                        //    this.TXT01_PON1330.GetValue().ToString(),
                        //    this.CBO01_PON1500.GetValue().ToString(),
                        //    this.CBO01_PON1510.GetValue().ToString(),
                        //    sPON1530.ToString(),
                        //    sPON1610.ToString(),
                        //    this.CBH01_PON1620.GetValue().ToString(),
                        //    this.CBO01_PON1630.GetValue().ToString(),
                        //    TYUserInfo.EmpNo.ToString(),
                        //    this.TXT01_PRN1000.GetValue().ToString(),
                        //    this.TXT01_PRN1010.GetValue().ToString(),
                        //    this.TXT01_PRN1020.GetValue().ToString(),
                        //    this.TXT01_PRN1030.GetValue().ToString(),
                        //    "C",
                        //    sOUTMSG.ToString()
                        //    );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                        if (sOUTMSG.Substring(0, 2) == "OK")
                        {
                            sOUTMSG = "OK";

                            // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                            // 선급자재일 경우 예산 업데이트 안함.
                            // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                            if (this.fsPRM5100.ToString() != "Y" && (this.TXT01_PON1070.GetValue().ToString() != "11101001" && this.TXT01_PON1070.GetValue().ToString() != "12210000"))
                            {
                                // 예산 가용금액 - 플러스
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_2BNBE617",
                                    this.TXT01_PON1000.GetValue().ToString(),
                                    this.TXT01_PON1010.GetValue().ToString(),
                                    this.TXT01_PON1020.GetValue().ToString(),
                                    this.TXT01_PON1030.GetValue().ToString(),
                                    this.CBH01_PON1040.GetValue().ToString(),
                                    this.CBH01_PON1050.GetValue().ToString(),
                                    this.CBH01_PON1060.GetValue().ToString(),
                                    this.TXT01_PON1070.GetValue().ToString(),
                                    this.TXT01_PON1080.GetValue().ToString(),
                                    this.TXT01_PON1090.GetValue().ToString(),
                                    Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
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
                                    "TY_P_MR_2BM91604",
                                    this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                                    this.TXT01_POO1140.GetValue().ToString().Substring(4, 2),
                                    TYUserInfo.EmpNo.ToString(),
                                    this.TXT01_PON1000.GetValue().ToString(),
                                    this.TXT01_PON1010.GetValue().ToString(),
                                    this.TXT01_PON1020.GetValue().ToString(),
                                    this.TXT01_PON1030.GetValue().ToString(),
                                    this.CBH01_PON1040.GetValue().ToString(),
                                    this.CBH01_PON1060.GetValue().ToString(),
                                    this.TXT01_PON1070.GetValue().ToString(),
                                    this.TXT01_PON1080.GetValue().ToString(),
                                    this.TXT01_PON1090.GetValue().ToString()
                                    );

                                // 마스터 테이블에 발주금액 업데이트
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_2BM94606",
                                    this.TXT01_PON1000.GetValue().ToString(),
                                    this.TXT01_PON1010.GetValue().ToString(),
                                    this.TXT01_PON1020.GetValue().ToString(),
                                    this.TXT01_PON1030.GetValue().ToString()
                                    );

                                this.DbConnector.ExecuteNonQueryList();

                                this.ShowMessage("TY_M_MR_2BD3Z286");

                                // 버튼 초기화
                                UP_ImgbtnDisplay("3", false);

                                // 컨트롤 초기화
                                UP_Control_Initialize("MRPPONF", true);

                                this.CBH01_PON1040.SetValue("");
                                this.CBH01_PON1060.SetValue("");

                                this.TXT01_PON1070.SetValue("");
                                this.TXT01_PON1070NM.SetValue("");
                                this.TXT01_PON1080.SetValue("");
                                this.TXT01_PON1080NM.SetValue("");
                                this.TXT01_PON1090.SetValue("");
                                this.TXT01_PON1090NM.SetValue("");
                                this.TXT01_PON1092.SetValue("");
                                this.CBH01_PON1050.SetValue("");
                                this.CBH01_PON1050.SetText("");

                                UP_FieldClear("MRPPONF");

                                UP_MRPPONF_DISPLAY();

                                SetFocus(this.CBH01_PON1040.CodeText);

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

            if (fsGUBUN == "MRPPOMF") // 마스터
            {
                // 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BL6V535",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                // 구매요청 마스터 발주번호, 발주완료구분 업데이트
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BL8E546",
                    "", // 발주완료구분
                    "", // 발주번호
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQueryList();
                this.ShowMessage("TY_M_GB_23NAD874");

                // 발주번호
                this.TXT01_POM1000.SetReadOnly(false);
                this.TXT01_POM1010.SetReadOnly(true);
                this.TXT01_POM1020.SetReadOnly(false);
                this.TXT01_POM1030.SetReadOnly(true);

                // 요청번호
                this.TXT01_PRM1000.SetReadOnly(false);
                //this.TXT01_PRM1010.ReadOnly = false;
                this.TXT01_PRM1020.SetReadOnly(false);
                this.TXT01_PRM1030.SetReadOnly(false);

                this.BTN61_PRM10001.Enabled = true;
                //this.BTN61_PRM10001.SetReadOnly(false);

                this.TXT01_POM1020.SetValue("");
                this.TXT01_POM1030.SetValue("");

                // 탭 컨트롤
                tabControl1_Enable("");

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                UP_FieldClear("MRPPOMF");

                //SetFocus(this.TXT01_POM1020);
                SetFocus(this.TXT01_POM1000);
            }
            else if (fsGUBUN == "MRPPONF") // 내역사항
            {
                sOUTMSG = "OK";

                // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.fsPRM5100.ToString() != "Y" && (this.TXT01_PON1070.GetValue().ToString() != "11101001" && this.TXT01_PON1070.GetValue().ToString() != "12210000"))
                {
                    // 발주예산 가용금액 - 마이너스
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM90601",
                        this.TXT01_PON1000.GetValue().ToString(),
                        this.TXT01_PON1010.GetValue().ToString(),
                        this.TXT01_PON1020.GetValue().ToString(),
                        this.TXT01_PON1030.GetValue().ToString(),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.CBH01_PON1050.GetValue().ToString(),
                        this.CBH01_PON1060.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1080.GetValue().ToString(),
                        this.TXT01_PON1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // 발주년월과 요청년월이 같을 경우 요청예산금액 예산테이블에 플러스 시킴
                        if (this.TXT01_PON1020.GetValue().ToString() == this.TXT01_PRN1020.GetValue().ToString())
                        {
                            // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                            if (this.TXT01_PON1070.GetValue().ToString() != "11101001" && this.TXT01_PON1070.GetValue().ToString() != "12210000")
                            {
                                // 요청예산 가용금액 - 플러스
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_2BG51426",
                                    this.TXT01_PRN1000.GetValue().ToString(),
                                    this.TXT01_PRN1010.GetValue().ToString(),
                                    this.TXT01_PRN1020.GetValue().ToString(),
                                    this.TXT01_PRN1030.GetValue().ToString(),
                                    this.CBH01_PON1040.GetValue().ToString(),
                                    this.CBH01_PON1050.GetValue().ToString(),
                                    this.CBH01_PON1060.GetValue().ToString(),
                                    this.TXT01_PON1070.GetValue().ToString(),
                                    this.TXT01_PON1080.GetValue().ToString(),
                                    this.TXT01_PON1090.GetValue().ToString(),
                                    Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                                    sOUTMSG.ToString()
                                    );

                                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                            }
                            
                        }
                    }
                }

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 내역사항 삭제 및 구매요청 관련 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BN9T609",
                        this.TXT01_PON1000.GetValue().ToString(),
                        this.TXT01_PON1010.GetValue().ToString(),
                        this.TXT01_PON1020.GetValue().ToString(),
                        this.TXT01_PON1030.GetValue().ToString(),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.CBH01_PON1050.GetValue().ToString(),
                        this.CBH01_PON1060.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1080.GetValue().ToString(),
                        this.TXT01_PON1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PON1092.GetValue().ToString()),
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // 귀속별 예산파일 삭제
                        if (fsYESAN_COUNT.ToString() == "1") // 삭제
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_MR_2BM99603",
                                this.TXT01_PON1000.GetValue().ToString(),
                                this.TXT01_PON1010.GetValue().ToString(),
                                this.TXT01_PON1020.GetValue().ToString(),
                                this.TXT01_PON1030.GetValue().ToString(),
                                this.CBH01_PON1040.GetValue().ToString(),
                                this.CBH01_PON1060.GetValue().ToString(),
                                this.TXT01_PON1070.GetValue().ToString(),
                                this.TXT01_PON1080.GetValue().ToString(),
                                this.TXT01_PON1090.GetValue().ToString()
                                );
                        }
                        if (int.Parse(fsYESAN_COUNT.ToString()) > 1) // 수정
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_MR_2BM91604",
                                this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                                this.TXT01_POO1140.GetValue().ToString().Substring(4, 2),
                                TYUserInfo.EmpNo.ToString(),
                                this.TXT01_PON1000.GetValue().ToString(),
                                this.TXT01_PON1010.GetValue().ToString(),
                                this.TXT01_PON1020.GetValue().ToString(),
                                this.TXT01_PON1030.GetValue().ToString(),
                                this.CBH01_PON1040.GetValue().ToString(),
                                this.CBH01_PON1060.GetValue().ToString(),
                                this.TXT01_PON1070.GetValue().ToString(),
                                this.TXT01_PON1080.GetValue().ToString(),
                                this.TXT01_PON1090.GetValue().ToString()
                                );
                        }

                        // 마스터 테이블에 발주금액 업데이트
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM94606",
                            this.TXT01_PON1000.GetValue().ToString(),
                            this.TXT01_PON1010.GetValue().ToString(),
                            this.TXT01_PON1020.GetValue().ToString(),
                            this.TXT01_PON1030.GetValue().ToString()
                            );

                        this.DbConnector.ExecuteNonQueryList();
                        this.ShowMessage("TY_M_GB_23NAD874");

                        // 버튼 초기화
                        UP_ImgbtnDisplay("3", false);

                        // 컨트롤 초기화
                        UP_Control_Initialize("MRPPONF", false);

                        this.CBH01_PON1040.SetValue("");
                        this.CBH01_PON1060.SetValue("");

                        this.TXT01_PON1070.SetValue("");
                        this.TXT01_PON1070NM.SetValue("");
                        this.TXT01_PON1080.SetValue("");
                        this.TXT01_PON1080NM.SetValue("");
                        this.TXT01_PON1090.SetValue("");
                        this.TXT01_PON1090NM.SetValue("");
                        this.TXT01_PON1092.SetValue("");
                        this.CBH01_PON1050.SetValue("");
                        this.CBH01_PON1050.SetText("");

                        UP_FieldClear("MRPPONF");

                        UP_MRPPONF_DISPLAY();

                        SetFocus(this.CBH01_PON1040.CodeText);
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
            this.DataTableColumnAdd(ds.Tables[0], "POT1000", this.TXT01_POT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "POT1010", this.TXT01_POT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "POT1020", this.TXT01_POT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "POT1030", this.TXT01_POT1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "POTHISAB", TYUserInfo.EmpNo);

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 수정
            this.DataTableColumnAdd(ds.Tables[1], "POT1000", this.TXT01_POT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "POT1010", this.TXT01_POT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "POT1020", this.TXT01_POT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "POT1030", this.TXT01_POT1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "POTHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2BLAW555", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_MR_2BLAX556", ds.Tables[1]); //수정

            this.DbConnector.ExecuteNonQueryList();

            UP_MRPPOTF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장완료 메세지
        }
        #endregion

        #region Description : 특기사항 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 수정
            this.DataTableColumnAdd(ds.Tables[0], "POT1000", this.TXT01_POT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "POT1010", this.TXT01_POT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "POT1020", this.TXT01_POT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "POT1030", this.TXT01_POT1030.GetValue().ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2BLAY557", ds.Tables[0]); //삭제

            this.DbConnector.ExecuteNonQueryList();

            UP_MRPPOTF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제완료 메세지
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsPOM1000 = this.TXT01_POM1000.GetValue().ToString();
            fsPOM1010 = this.TXT01_POM1010.GetValue().ToString();
            fsPOM1020 = this.TXT01_POM1020.GetValue().ToString();
            fsPOM1030 = this.TXT01_POM1030.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 마스터 관련

        #region Description : 마스터 DISPLAY
        private void UP_MRPPOMF_DISPLAY()
        {
            UP_FieldClear("MRPPOMF");

            fsPOM5110 = "";

            DataTable dt = new DataTable();

            #region Description : 구매요청 마스터 내용 DISPLAY

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ15462",
                this.TXT01_POM1000.GetValue(),
                this.TXT01_POM1010.GetValue(),
                this.TXT01_POM1020.GetValue(),
                Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_PRM1000.ReadOnly = true;
                this.TXT01_PRM1010.ReadOnly = true;
                this.TXT01_PRM1020.ReadOnly = true;
                this.TXT01_PRM1030.ReadOnly = true;

                this.BTN61_PRM10001.Enabled = false;
                //this.BTN61_PRM10001.SetReadOnly(true);

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
                tabControl1_Enable("MRPPOMF");

                this.CurrentDataTableRowMapping(dt, "01");

                // 예산 DISPLAY
                UP_MRPPOOF_DISPLAY();
            }
            else
            {
                this.TXT01_PRM1000.ReadOnly = false;
                //this.TXT01_PRM1010.ReadOnly = false;
                this.TXT01_PRM1020.ReadOnly = false;
                this.TXT01_PRM1030.ReadOnly = false;

                this.BTN61_PRM10001.Enabled = true;
                //this.BTN61_PRM10001.SetReadOnly(false);
            }

            #endregion
        }
        #endregion

        #region Description : 예산 DISPLAY
        private void UP_MRPPOOF_DISPLAY()
        {
            DataTable dt = new DataTable();

            #region Description : 구매발주 마스터(예산조회-귀속부서별)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ1A464",
                this.TXT01_POM1000.GetValue(),
                this.TXT01_POM1010.GetValue(),
                this.TXT01_POM1020.GetValue(),
                this.TXT01_POM1030.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2BJ69476.SetValue(dt);

            #endregion

            #region Description : 구매발주 마스터(예산조회-거래처별)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ1A465",
                this.TXT01_POM1000.GetValue(),
                this.TXT01_POM1010.GetValue(),
                this.TXT01_POM1020.GetValue(),
                this.TXT01_POM1030.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2BJ5Z475.SetValue(UP_MAST_POOF_SumRowds(dt));

                for (int i = 0; i < this.FPS91_TY_S_MR_2BJ5Z475.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_MR_2BJ5Z475.GetValue(i, "VNSANGHO").ToString() == "거래처별 소계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BJ5Z475.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BJ5Z475.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                    else if (this.FPS91_TY_S_MR_2BJ5Z475.GetValue(i, "VNSANGHO").ToString() == "총   계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BJ5Z475.ActiveSheet.Rows[i].ForeColor = Color.Red;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BJ5Z475.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2BJ5Z475.SetValue(dt);
            }

            #endregion
        }
        #endregion

        #region Description : 구매발주 마스터(예산조회-거래처별 합계)
        private DataTable UP_MAST_POOF_SumRowds(DataTable dt)
        {
            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt;

            string sMEKEY = "";

            double dPON1170 = 0; // 금액

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;


            for (i = 1; i < nNum; i++)
            {
                /* Row i 번째와 Row i+1 번째의 거래처가 다를경우 빈로우를 생성하고
                 * 거래처별 금액 소계를 낸다.                                     */
                if (table.Rows[i - 1]["PON1100"].ToString() != table.Rows[i]["PON1100"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합계 이름 넣기
                    table.Rows[i]["VNSANGHO"] = "거래처별 소계";

                    // 거래처별
                    sMEKEY = " PON1100 = '" + table.Rows[i - 1]["PON1100"].ToString() + "' " ;

                    // 금액
                    table.Rows[i]["PON1170"] = table.Compute("SUM(PON1170)", sMEKEY).ToString();
                    // 금액합계
                    dPON1170 += Convert.ToDouble(table.Compute("SUM(PON1170)", sMEKEY).ToString());

                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 합계 이름 넣기
            table.Rows[i]["VNSANGHO"] = "거래처별 소계";

            // 거래처별
            sMEKEY = " PON1100 = '" + table.Rows[i - 1]["PON1100"].ToString() + "' " ;

            // 금액
            table.Rows[i]["PON1170"] = table.Compute("SUM(PON1170)", sMEKEY).ToString();
            // 금액합계
            dPON1170 += Convert.ToDouble(table.Compute("SUM(PON1170)", sMEKEY).ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);

            // 총계 이름 넣기
            table.Rows[i + 1]["VNSANGHO"] = "총   계";            
            table.Rows[i + 1]["PON1170"]  = Convert.ToString(dPON1170);

            return table;
        }
        #endregion

        #region Description : 그룹웨어 문서 바로가기
        private void BTN61_GW_Click(object sender, EventArgs e)
        {
            if (this.TXT01_POM1230.GetValue().ToString() != "")
            {
                if ((new TYMRPR005S(this.TXT01_POM1230.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

        #region Description : 발주 사번
        private void CBH01_POM1140_TextChanged(object sender, EventArgs e)
        {
            if (this.CBH01_POM1140.GetValue().ToString().Length >= 6)
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BEBB293",
                    DateTime.Now.ToString("yyyyMMdd"),
                    this.CBH01_POM1140.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 부서코드
                    this.TXT01_POM1130.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
                    // 부서명
                    this.TXT01_DTDESC.SetValue(dt.Rows[0]["KBBUSEONM"].ToString());
                }
            }
        }
        #endregion

        #region Description : 발주일자 이벤트
        private void DTP01_POM1100_ValueChanged(object sender, EventArgs e)
        {
            // 내역 귀속부서
            this.CBH01_PON1040.DummyValue = this.DTP01_POM1100.GetString();
        }
        #endregion

        #endregion

        #region Description : 내역사항 관련

        #region Description : 내역사항 확인
        private void UP_MRPPONF_RUN()
        {
            UP_FieldClear("MRPPONF");

            DataTable dt = new DataTable();

            #region Description : 구매발주 내역 내용 확인

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BM73592",
                this.TXT01_PON1000.GetValue(),
                this.TXT01_PON1010.GetValue(),
                this.TXT01_PON1020.GetValue(),
                Set_Fill4(this.TXT01_PON1030.GetValue().ToString()),
                this.CBH01_PON1040.GetValue(),
                this.CBH01_PON1050.GetValue(),
                this.CBH01_PON1060.GetValue(),
                this.TXT01_PON1070.GetValue(),
                this.TXT01_PON1080.GetValue(),
                Get_Numeric(this.TXT01_PON1090.GetValue().ToString()),
                Get_Numeric(this.TXT01_PON1092.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "MRPPONF";

                // 버튼 컨트롤
                UP_ImgbtnDisplay("1", true);
            }

            #endregion

            // 요청잔량, 요청 잔액 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BM7E593",
                this.TXT01_PRN1000.GetValue(),
                this.TXT01_PRN1010.GetValue(),
                this.TXT01_PRN1020.GetValue(),
                Set_Fill4(this.TXT01_PRN1030.GetValue().ToString()),
                this.CBH01_PON1040.GetValue(),
                this.CBH01_PON1050.GetValue(),
                this.CBH01_PON1060.GetValue(),
                this.TXT01_PON1070.GetValue(),
                this.TXT01_PON1080.GetValue(),
                Get_Numeric(this.TXT01_PON1090.GetValue().ToString()),
                Get_Numeric(this.TXT01_PON1092.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_PRN1150.SetValue(dt.Rows[0]["PRN1150"].ToString()); // 요청수량
                this.TXT01_PRN1170.SetValue(dt.Rows[0]["PRN1170"].ToString()); // 요청금액
                this.TXT01_PRN3020.SetValue(dt.Rows[0]["PRN3020"].ToString()); // 발주잔량
                this.TXT01_PRN3070.SetValue(dt.Rows[0]["PRN3070"].ToString()); // 발주잔액
            }
        }
        #endregion

        #region Description : 내역사항 DISPLAY
        private void UP_MRPPONF_DISPLAY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ1A466",
                this.TXT01_PON1000.GetValue(),
                this.TXT01_PON1010.GetValue(),
                this.TXT01_PON1020.GetValue(),
                Set_Fill4(this.TXT01_PON1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DataTable retDt = new DataTable();

                retDt = UP_MRPPONF_SumRowds(dt);

                this.FPS91_TY_S_MR_2BJ6B478.SetValue(retDt);

                for (int i = 0; i < retDt.Rows.Count; i++)
                {
                    if (this.FPS91_TY_S_MR_2BJ6B478.GetValue(i, "YSDESC").ToString() == "예산별 소계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BJ6B478.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BJ6B478.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                    else if (this.FPS91_TY_S_MR_2BJ6B478.GetValue(i, "YSDESC").ToString() == "예산별 합계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BJ6B478.ActiveSheet.Rows[i].ForeColor = Color.Red;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BJ6B478.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2BJ6B478.SetValue(dt);
            }
        }
        #endregion

        #region Description : 구매요청 내역사항(예산별 합계)
        private DataTable UP_MRPPONF_SumRowds(DataTable dt)
        {
            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt;

            string sMEKEY = "";

            double dPON1150 = 0; // 수량
            double dPON1170 = 0; // 금액

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;


            for (i = 1; i < nNum; i++)
            {
                /* Row i 번째와 Row i+1 번째의 귀속부서, 예산, 계정 비품, 순번이 다를경우 빈로우를 생성하고
                 * 예산별 수량, 금액 소계를 낸다.                                                          */
                if (table.Rows[i - 1]["PON1040"].ToString() != table.Rows[i]["PON1040"].ToString() ||
                     table.Rows[i - 1]["PON1060"].ToString() != table.Rows[i]["PON1060"].ToString() ||
                     table.Rows[i - 1]["PON1070"].ToString() != table.Rows[i]["PON1070"].ToString() ||
                     table.Rows[i - 1]["PON1080"].ToString() != table.Rows[i]["PON1080"].ToString() ||
                     table.Rows[i - 1]["PON1090"].ToString() != table.Rows[i]["PON1090"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);
                    // 합 계 이름 넣기
                    table.Rows[i]["YSDESC"] = "예산별 소계";

                    // 예산별
                    sMEKEY = " PON1040 = '" + table.Rows[i - 1]["PON1040"].ToString() + "'      ";
                    sMEKEY += " AND PON1060 = '" + table.Rows[i - 1]["PON1060"].ToString() + "' ";
                    sMEKEY += " AND PON1070 = '" + table.Rows[i - 1]["PON1070"].ToString() + "' ";
                    sMEKEY += " AND PON1080 = '" + table.Rows[i - 1]["PON1080"].ToString() + "' ";
                    sMEKEY += " AND PON1090 =  " + table.Rows[i - 1]["PON1090"].ToString() + "  ";

                    // 수량
                    table.Rows[i]["PON1150"] = table.Compute("SUM(PON1150)", sMEKEY).ToString();
                    // 금액
                    table.Rows[i]["PON1170"] = table.Compute("SUM(PON1170)", sMEKEY).ToString();

                    // 수량합계
                    dPON1150 += Convert.ToDouble(table.Compute("SUM(PON1150)", sMEKEY).ToString());
                    // 금액합계
                    dPON1170 += Convert.ToDouble(table.Compute("SUM(PON1170)", sMEKEY).ToString());

                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            table.Rows[i]["YSDESC"] = "예산별 소계";

            // 예산별
            sMEKEY = " PON1040 = '" + table.Rows[i - 1]["PON1040"].ToString() + "'      ";
            sMEKEY += " AND PON1060 = '" + table.Rows[i - 1]["PON1060"].ToString() + "' ";
            sMEKEY += " AND PON1070 = '" + table.Rows[i - 1]["PON1070"].ToString() + "' ";
            sMEKEY += " AND PON1080 = '" + table.Rows[i - 1]["PON1080"].ToString() + "' ";
            sMEKEY += " AND PON1090 =  " + table.Rows[i - 1]["PON1090"].ToString() + "  ";

            // 수량
            table.Rows[i]["PON1150"] = table.Compute("SUM(PON1150)", sMEKEY).ToString();
            // 금액
            table.Rows[i]["PON1170"] = table.Compute("SUM(PON1170)", sMEKEY).ToString();

            // 수량합계
            dPON1150 += Convert.ToDouble(table.Compute("SUM(PON1150)", sMEKEY).ToString());
            // 금액합계
            dPON1170 += Convert.ToDouble(table.Compute("SUM(PON1170)", sMEKEY).ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);
            // 합 계 이름 넣기
            table.Rows[i + 1]["YSDESC"] = "예산별 합계";

            table.Rows[i + 1]["PON1150"] = Convert.ToString(dPON1150);
            table.Rows[i + 1]["PON1170"] = Convert.ToString(dPON1170);

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
        //        this.TXT01_PON1050.SetValue(popup.fsJEPUM);
        //        this.TXT01_PON1050NM.SetValue(popup.fsZ105013);

        //        SetFocus(this.CBH01_PON1100.CodeText);
        //    }
        //}
        //#endregion

        #region Description : 적용계정
        private void TXT01_PON1070_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PON1070.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PON10701_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 적용계정 버튼
        private void BTN61_PON10701_Click(object sender, EventArgs e)
        {
            if (this.CBH01_PON1060.GetValue().ToString() == "1") // 기타세목예산(투자&수선) 코드헬프
            {
                TYMRGB005S popup = new TYMRGB005S(this.TXT01_PON1020.GetValue().ToString().Substring(0, 4), this.CBH01_PON1040.GetValue().ToString(), this.CBH01_PON1040.GetText().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 요청년월의 본예산, 집행예산 금액 가져옴
                    this.TXT01_PON1070.SetValue(popup.fsCDAC);      // 계정과목
                    this.TXT01_PON1070NM.SetValue(popup.fsCDACNM);  // 계정과목명
                    this.TXT01_PON1090.SetValue(popup.fsSEQ);       // 순번
                    this.TXT01_PON1090NM.SetValue(popup.fsYESANNM); // 예산명

                    SetFocus(this.CBH01_PON1050);
                }
            }
            else if (this.CBH01_PON1060.GetValue().ToString() == "2") // 소모품비 예산세목 코드헬프
            {
                TYMRGB006S popup = new TYMRGB006S(this.TXT01_PON1020.GetValue().ToString().Substring(0, 4), this.CBH01_PON1040.GetValue().ToString(), this.CBH01_PON1040.GetText().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 요청년월의 본예산, 집행예산 금액 가져옴
                    this.TXT01_PON1070.SetValue(popup.fsCDAC);     // 계정과목
                    this.TXT01_PON1070NM.SetValue(popup.fsCDACNM); // 계정과목명
                    this.TXT01_PON1080.SetValue(popup.fsCDJJ);     // 비품코드
                    this.TXT01_PON1080NM.SetValue(popup.fsBPDESC); // 비품명
                    this.TXT01_PON1090.SetValue(popup.fsSEQ);      // 순번

                    SetFocus(this.CBH01_PON1050);
                }
            }
            else if (this.CBH01_PON1060.GetValue().ToString() == "3") // 기타예산 코드헬프
            {
                TYMRGB007S popup = new TYMRGB007S(this.TXT01_PON1020.GetValue().ToString().Substring(0, 4), this.CBH01_PON1040.GetValue().ToString(), this.CBH01_PON1040.GetText().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 요청년월의 본예산, 집행예산 금액 가져옴
                    this.TXT01_PON1070.SetValue(popup.fsCDAC);     // 계정과목
                    this.TXT01_PON1070NM.SetValue(popup.fsCDACNM); // 계정과목명

                    SetFocus(this.CBH01_PON1050);
                }
            }
        }
        #endregion

        #region Description : 내역사항 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2BFBJ342_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            bool fResult;

            if (this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1040").ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");
            }
            else
            {
                this.CBH01_PON1040.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1040").ToString());
                this.CBH01_PON1060.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1060").ToString());
                this.TXT01_PON1070.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1070").ToString());
                this.TXT01_PON1070NM.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("A1NMAC").ToString());
                this.TXT01_PON1080.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1080").ToString());
                this.TXT01_PON1080NM.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("BPDESC").ToString());
                this.TXT01_PON1090.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1090").ToString());
                this.TXT01_PON1090NM.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1090NM").ToString());
                this.TXT01_PON1092.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1092").ToString());
                this.CBH01_PON1050.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("PON1050").ToString());
                this.CBH01_PON1050.SetText(this.FPS91_TY_S_MR_2BJ6B478.GetValue("Z105013").ToString());

                // 제품 사진개수
                this.TXT01_Z106000.SetValue(this.FPS91_TY_S_MR_2BJ6B478.GetValue("Z106000").ToString());

                e.Cancel = true;

                // 컨트롤 초기화
                UP_Control_Initialize("MRPPONF", true);
                
                UP_MRPPONF_RUN();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 마스터 데이터가 존재하므로 
                    // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                    UP_ImgbtnDisplay("1", true);

                    this.SetFocus(this.CBH01_PON1100.CodeText);
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
        private void UP_MRPPOTF_DISPLAY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ1A467",
                this.TXT01_POT1000.GetValue(),
                this.TXT01_POT1010.GetValue(),
                this.TXT01_POT1020.GetValue(),
                Set_Fill4(this.TXT01_POT1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2BJ65477.SetValue(dt);
        }
        #endregion

        #endregion

        #region Description : 구매발주 공통

        #region Description : FieldClear()
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN.ToString() == "MRPPOMF")
            {
                //// 신청사번 <- 등록 및 수정 체크에 넣음
                //this.CBH01_POM1140.SetValue(TYUserInfo.EmpNo);

                //// 등록 시 요청부서의 앞자리 가져옴
                //this.TXT01_POM1000.SetValue(this.TXT01_POM1130.GetValue().ToString().Substring(0, 1));

                this.TXT01_PRM1000.SetValue("");
                this.TXT01_PRM1020.SetValue("");
                this.TXT01_PRM1030.SetValue("");
                this.TXT01_PRM2020.SetValue("");
                this.TXT01_PRM2030.SetValue("");
                this.TXT01_PRM2040.SetValue("");
                this.TXT01_PRM2080.SetValue("");
                this.TXT01_PRM2090.SetValue("");
                this.TXT01_PRM2070.SetValue("");
                this.TXT01_PRM5130.SetValue("");
                this.TXT01_OPM1040.SetValue("");
                this.DTP01_POM1100.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.CBH01_POM1140.SetValue("");
                this.TXT01_POM1130.SetValue("");
                this.TXT01_DTDESC.SetValue("");
                this.TXT01_POM1150.SetValue("");
                this.TXT01_POM1160.SetValue("");
                this.DTP01_POM1170.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.CBH01_POM1700.SetValue("");
                this.CBH01_POM1730.SetValue("");
                this.CBO01_POM1710.SetValue("1");
                this.CBO01_POM1910.SetValue("1");
                this.TXT01_POM1720.SetValue("");
                this.TXT01_POM1180.SetValue("");
                this.TXT01_POM1500.SetValue("");
                this.TXT01_POM1230.SetValue("");
                this.TXT01_POM1300.SetValue("");
                this.CBH01_POM1310.SetValue("");
                this.TXT01_POM1200.SetValue("");
                this.TXT01_POM1210.SetValue("");
                this.TXT01_KBHANGL1.SetValue("");
                this.TXT01_POM1220.SetValue("");

                this.CBO01_POM6010.SetValue("N");
                this.CBO01_POM6020.SetValue("3");
                this.CBH01_POM6030.SetValue("");
            }
            else if (sGUBUN.ToString() == "MRPPONF")
            {
                //this.CBH01_PON1040.SetValue("");
                //this.CBH01_PON1060.SetValue("");
                //this.TXT01_PON1070.SetValue("");
                //this.TXT01_PON1070NM.SetValue("");
                //this.TXT01_PON1080.SetValue("");
                //this.TXT01_PON1080NM.SetValue("");
                //this.TXT01_PON1090.SetValue("");
                //this.TXT01_PON1050.SetValue("");
                //this.TXT01_PON1050NM.SetValue("");
                this.CBH01_PON1100.SetValue("");
                this.CBH01_PON1110.SetValue("71");
                this.CBO01_PON1500.SetValue("N");
                this.CBH01_PON1130.SetValue("1");
                this.CBO01_PON1510.SetValue("N");
                this.TXT01_PON1530.SetValue("");
                this.TXT01_PON1531.SetValue("");
                this.TXT01_PON1530NM.SetValue("");
                this.TXT01_PON1610.SetValue("");
                this.TXT01_PON1611.SetValue("");
                this.TXT01_PON1612.SetValue("");
                this.TXT01_PON1610NM.SetValue("");
                this.CBH01_PON1620.SetValue("");
                this.CBH01_PON1620.SetText("");
                this.TXT01_PON16201NM.SetValue("");
                this.TXT01_PRN1150.SetValue("");
                this.TXT01_PRN1170.SetValue("");
                this.TXT01_PRN3020.SetValue("");
                this.TXT01_PRN3070.SetValue("");
                this.TXT01_PON1150.SetValue("");
                this.TXT01_PON1160.SetValue("");
                this.TXT01_PON1180.SetValue("");
                this.TXT01_PON1170.SetValue("");
                this.TXT01_PON1200.SetValue("");
                this.TXT01_PON1210.SetValue("");
                this.TXT01_PON1220.SetValue("");
                this.TXT01_PON1230.SetValue("");
                this.TXT01_PON1300.SetValue("");
                this.TXT01_PON1310.SetValue("");
                this.TXT01_PON1320.SetValue("");
                this.TXT01_PON1330.SetValue("");
                this.TXT01_PON1190.SetValue("");
                this.TXT01_PON11901.SetValue("");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsYESAN_COUNT = "0";

            fsPRM5100 = "";
            fsPOM1400 = "";
            fsPOM1410 = "";
            fsPOM1420 = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPPOMF") // 마스터
            {
                fsPOM5110 = "";

                // 사업부
                if (this.TXT01_POM1000.GetValue().ToString() != "A" && this.TXT01_POM1000.GetValue().ToString() != "S" &&
                    this.TXT01_POM1000.GetValue().ToString() != "T" && this.TXT01_POM1000.GetValue().ToString() != "B" &&
                    this.TXT01_POM1000.GetValue().ToString() != "C" && this.TXT01_POM1000.GetValue().ToString() != "D" &&
                    this.TXT01_POM1000.GetValue().ToString() != "E")
                {
                    this.ShowMessage("TY_M_MR_2BK3H504");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주년월
                if (this.TXT01_POM1020.GetValue().ToString().Length != 6)
                {
                    this.ShowMessage("TY_M_MR_2BK3G501");

                    this.TXT01_POM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주월
                if (int.Parse(Get_Numeric(this.TXT01_POM1020.GetValue().ToString().Substring(4, 2))) == 0 ||
                    int.Parse(Get_Numeric(this.TXT01_POM1020.GetValue().ToString().Substring(4, 2))) > 12)
                {
                    this.ShowMessage("TY_M_MR_2CE2E192");

                    this.TXT01_POM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 사업부
                if (this.TXT01_PRM1000.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3C496");

                    this.TXT01_PRM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 년월
                if (this.TXT01_PRM1020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3D498");

                    this.TXT01_PRM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 순번
                if (this.TXT01_PRM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3C497");

                    this.TXT01_PRM1030.Focus();

                    e.Successed = false;
                    return;
                }


                // 발의 사번
                if (this.CBH01_POM1140.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3D499");

                    this.CBH01_POM1140.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주일자
                if (this.DTP01_POM1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BLAQ525");

                    this.DTP01_POM1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 인도지역
                if (this.TXT01_POM1150.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D303");

                    this.TXT01_POM1150.Focus();

                    e.Successed = false;
                    return;
                }

                // 인도조건
                if (this.TXT01_POM1160.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D302");

                    this.TXT01_POM1160.Focus();

                    e.Successed = false;
                    return;
                }

                // 납기일자
                if (this.DTP01_POM1170.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C296");

                    this.DTP01_POM1170.Focus();

                    e.Successed = false;
                    return;
                }

                // 화폐구분
                if (this.CBH01_POM1700.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D301");

                    this.CBH01_POM1700.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 지불조건
                if (this.CBH01_POM1730.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D300");

                    this.CBH01_POM1730.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 공사 및 구매명
                if (this.TXT01_POM1180.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D299");

                    this.TXT01_POM1180.Focus();

                    e.Successed = false;
                    return;
                }

                /* 월말구분 = 1 - 월말마감
                 * 지불조건 = 01 - 당사지불조건
                 * 지불조건 = 02 - 익월현금
                 * 
                 * 월말구분 = 2 - 즉시마감
                 * 지불조건 = 06 - 즉시현금
                 */

                //if (this.CBH01_POM1730.GetValue().ToString() == "01" || this.CBH01_POM1730.GetValue().ToString() == "02")
                //{
                //    if (this.CBO01_POM1910.GetValue().ToString() != "1")
                //    {
                //        this.ShowMessage("TY_M_MR_318AQ532");

                //        this.CBO01_POM1910.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                //if (this.CBH01_POM1730.GetValue().ToString() == "06")
                //{
                //    if (this.CBO01_POM1910.GetValue().ToString() != "2")
                //    {
                //        this.ShowMessage("TY_M_MR_318AR533");

                //        this.CBO01_POM1910.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                if (this.TXT01_PRM1000.GetValue().ToString() == "" ||
                    this.TXT01_PRM1020.GetValue().ToString() == "" ||
                    this.TXT01_PRM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3D500");

                    this.TXT01_PRM1000.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 계약번호 및 요청금액 -> 발주금액
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BLB0526",
                        this.TXT01_PRM1000.GetValue().ToString(),
                        this.TXT01_PRM1010.GetValue().ToString(),
                        this.TXT01_PRM1020.GetValue().ToString(),
                        this.TXT01_PRM1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        fsPRM5100 = dt.Rows[0]["PRM5100"].ToString();
                        fsPOM1400 = dt.Rows[0]["PRM5110"].ToString();
                        fsPOM1410 = dt.Rows[0]["PRM5120"].ToString();
                        fsPOM1420 = dt.Rows[0]["PRM5130"].ToString();

                        this.TXT01_POM1410.SetValue(fsPOM1410.ToString());
                        this.TXT01_POM1420.SetValue(fsPOM1420.ToString());
                        
                        // 발주금액
                        this.TXT01_POM1720.SetValue(dt.Rows[0]["PRN1170"].ToString());
                    }
                }

                /****************************************
                 * 비용청구 = 'Y'일 경우에만
                 * 청구구분, 청구화주 필드에 값을 입력 함.
                 ****************************************/
                if (this.CBO01_POM6010.GetValue().ToString() == "N")
                {
                    this.CBO01_POM6020.SetValue("3");
                    this.CBH01_POM6030.SetValue("");
                }
                else
                {
                    if (this.CBO01_POM6020.GetValue().ToString() == "3")
                    {
                        this.ShowMessage("TY_M_MR_3352R235");

                        this.CBO01_POM6020.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_POM6030.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_3352R236");

                        this.CBH01_POM6030.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 구매요청 내역파일 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BK38495",
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_MR_2BL71537");

                    this.TXT01_PRM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 구매요청 예산파일 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BK36493",
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_MR_2BL71536");

                    this.TXT01_PRM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주취소일, 취소사유
                if (this.TXT01_POM1300.GetValue().ToString() == "" && this.CBH01_POM1310.GetValue().ToString() != "")
                {
                    this.ShowMessage("TY_M_MR_2BU93766");

                    this.TXT01_POM1300.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주취소일, 취소사유
                if (this.TXT01_POM1300.GetValue().ToString() != "" && this.CBH01_POM1310.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BU93767");

                    this.CBH01_POM1310.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                //// 순번 가져오기
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BLB8527",
                //    TXT01_POM1000.GetValue().ToString(),
                //    TXT01_POM1010.GetValue().ToString(),
                //    Get_Numeric(TXT01_POM1020.GetValue().ToString())
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.TXT01_POM1030.SetValue(dt.Rows[0]["POM1030"].ToString());
                //}
            }
            else if (fsGUBUN == "MRPPONF") // 내역사항
            {
                #region Description : KeyCheck

                // 귀속부서
                if (this.CBH01_PON1040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1381");

                    this.CBH01_PON1040.Focus();

                    e.Successed = false;
                    return;
                }

                // 예산구분
                if (this.CBH01_PON1060.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1380");

                    this.CBH01_PON1060.Focus();

                    e.Successed = false;
                    return;
                }

                // 계정코드
                if (this.TXT01_PON1070.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1379");

                    this.TXT01_PON1070.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAF400",
                        this.TXT01_PON1070.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAE399");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 계정과목명
                        this.TXT01_PON1070NM.SetValue(dt.Rows[0]["A1NMAC"].ToString());
                    }
                }

                // 예산 체크
                if (this.CBH01_PON1060.GetValue().ToString() == "1") // 투자예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAG401",
                        this.TXT01_POO1140.GetValue().ToString().Substring(0,4),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1090.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA1378");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PON1060.GetValue().ToString() == "2") // 소모성 예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAH402",
                        this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1080.GetValue().ToString(),
                        this.TXT01_PON1090.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2390");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PON1060.GetValue().ToString() == "3") // 기타본예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAI403",
                        this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2389");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PON1060.GetValue().ToString() == "4") // 선급자재
                {
                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    if (this.TXT01_PON1070.GetValue().ToString() != "11101001" && this.TXT01_PON1070.GetValue().ToString() != "12210000")
                    {
                        this.ShowMessage("TY_M_MR_31L41841");

                        this.CBH01_PON1060.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                /* 선급자재 계정일 경우 체크
                 * 1. 선급자재 계정만 오고 다른 계정은 올 수 없다.
                 * 2. 단일 귀속부서만 올 수 있다.
                 */
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_PON1070.GetValue().ToString() == "11101001" || this.TXT01_PON1070.GetValue().ToString() == "12210000")
                {
                    // 선급자재 계정만 오고 다른 계정은 올 수 없다.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32J9K118",
                        this.TXT01_PON1000.GetValue().ToString(),
                        this.TXT01_PON1010.GetValue().ToString(),
                        this.TXT01_PON1020.GetValue().ToString(),
                        this.TXT01_PON1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32J91113");

                        this.CBH01_PON1040.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 단일 귀속부서만 올 수 있다.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32J9K117",
                        this.TXT01_PON1000.GetValue().ToString(),
                        this.TXT01_PON1010.GetValue().ToString(),
                        this.TXT01_PON1020.GetValue().ToString(),
                        this.TXT01_PON1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["PON1040"].ToString() != this.CBH01_PON1040.GetValue().ToString())
                            {
                                this.ShowMessage("TY_M_MR_32J9A115");

                                this.CBH01_PON1040.Focus();

                                e.Successed = false;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    // 선급자재 계정만 오고 다른 계정은 올 수 없다.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32J9K117",
                        this.TXT01_PON1000.GetValue().ToString(),
                        this.TXT01_PON1010.GetValue().ToString(),
                        this.TXT01_PON1020.GetValue().ToString(),
                        this.TXT01_PON1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32J91113");

                        this.CBH01_PON1040.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 품목코드
                if (this.CBH01_PON1050.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA2388");

                    this.CBH01_PON1050.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAJ404",
                        this.CBH01_PON1050.GetValue().ToString().Substring(0, 1),
                        this.CBH01_PON1050.GetValue().ToString().Substring(1, 3),
                        this.CBH01_PON1050.GetValue().ToString().Substring(4, 3),
                        this.CBH01_PON1050.GetValue().ToString().Substring(7, 5)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2387");

                        this.CBH01_PON1050.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 품목명
                        this.CBH01_PON1050.SetText(dt.Rows[0]["Z105013"].ToString());
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BM73592",
                    this.TXT01_PON1000.GetValue(),
                    this.TXT01_PON1010.GetValue(),
                    this.TXT01_PON1020.GetValue(),
                    Set_Fill4(this.TXT01_PON1030.GetValue().ToString()),
                    this.CBH01_PON1040.GetValue(),
                    this.CBH01_PON1050.GetValue(),
                    this.CBH01_PON1060.GetValue(),
                    this.TXT01_PON1070.GetValue(),
                    this.TXT01_PON1080.GetValue(),
                    Get_Numeric(this.TXT01_PON1090.GetValue().ToString()),
                    Get_Numeric(this.TXT01_PON1092.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2C48B906");

                    this.CBH01_PON1050.Focus();

                    e.Successed = false;
                    return;
                }

                // 내역 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_35N50721",
                    this.TXT01_PON1000.GetValue(),
                    this.TXT01_PON1010.GetValue(),
                    this.TXT01_PON1020.GetValue(),
                    Set_Fill4(this.TXT01_PON1030.GetValue().ToString()),
                    this.CBH01_PON1040.GetValue(),
                    this.CBH01_PON1050.GetValue(),
                    this.CBH01_PON1060.GetValue(),
                    this.TXT01_PON1070.GetValue(),
                    this.TXT01_PON1080.GetValue(),
                    Get_Numeric(this.TXT01_PON1090.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_PON1092.SetValue(dt.Rows[0]["PON1092"].ToString());
                }

                #endregion

                #region Description : 내용 체크

                // 자산취득일경우 같은예산에 동일품목을 입력할 수 없음.
                // 자산취득일 경우 예산의 6번째 숫자와 회계의 자산분류코드 1번째 숫자가 일치해야 한다.
                if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.CBH01_PON1620.GetValue().ToString() != "" &&
                    this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_35N8I725",
                        this.TXT01_PON1000.GetValue().ToString(),
                        this.TXT01_PON1010.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PON1020.GetValue().ToString()),
                        Set_Fill4(Get_Numeric(this.TXT01_PON1030.GetValue().ToString())),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.CBH01_PON1050.GetValue().ToString(),
                        this.CBH01_PON1060.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1080.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PON1090.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_35N8E724");

                        this.CBH01_PON1050.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 20190610 임차장님과 얘기 후 품(유형자산 비교하는 화면에서 매칭이 안되어서 품)
                    // 20171102 임차장님과 얘기 후 풀기로 함
                    if (this.TXT01_PON1070.GetValue().ToString().Substring(5, 1) != this.CBH01_PON1620.GetValue().ToString().Substring(0, 1))
                    {
                        this.ShowMessage("TY_M_MR_66NGQ383");

                        this.CBH01_PON1620.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 자산 취득 및 자본적 지출일 경우
                // 예산의 6번째 숫자와 회계의 자산분류코드 1번째 숫자가 일치해야 한다.
                if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000" && this.CBH01_PON1620.GetValue().ToString() != "")
                {
                    //if (this.TXT01_PON1070.GetValue().ToString().Substring(5, 1) != this.CBH01_PON1620.GetValue().ToString().Substring(0, 1))
                    //{
                    //    this.ShowMessage("TY_M_MR_66NGQ383");

                    //    this.CBH01_PON1620.Focus();

                    //    e.Successed = false;
                    //    return;
                    //}
                }

                // 선급자재일 경우 고정자산번호 및 분류코드는 공백이다.
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_PON1070.GetValue().ToString() == "11101001" || this.TXT01_PON1070.GetValue().ToString() == "12210000")
                {
                    this.TXT01_PON1610.SetValue("");
                    this.TXT01_PON1611.SetValue("");
                    this.TXT01_PON1612.SetValue("");
                    this.TXT01_PON1610NM.SetValue("");
                    this.CBH01_PON1620.SetValue("");
                    this.CBH01_PON1620.SetText("");
                    this.TXT01_PON16201NM.SetValue("");
                }

                // 계약번호 및 요청금액 -> 발주금액
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C7BD980",
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    this.TXT01_PRN1020.GetValue().ToString(),
                    this.TXT01_PRN1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsPRM5100 = dt.Rows[0]["PRM5100"].ToString();
                    fsPOM1400 = dt.Rows[0]["PRM5110"].ToString();
                    fsPOM1410 = dt.Rows[0]["PRM5120"].ToString();
                    fsPOM1420 = dt.Rows[0]["PRM5130"].ToString();

                    this.TXT01_POM1410.SetValue(fsPOM1410.ToString());
                    this.TXT01_POM1420.SetValue(fsPOM1420.ToString());

                    // 발주금액
                    //this.TXT01_POM1720.SetValue(dt.Rows[0]["PRN1170"].ToString());
                }

                // 요청잔량, 요청 잔액 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BM7E593",
                    this.TXT01_PRN1000.GetValue(),
                    this.TXT01_PRN1010.GetValue(),
                    this.TXT01_PRN1020.GetValue(),
                    Set_Fill4(this.TXT01_PRN1030.GetValue().ToString()),
                    this.CBH01_PON1040.GetValue(),
                    this.CBH01_PON1050.GetValue(),
                    this.CBH01_PON1060.GetValue(),
                    this.TXT01_PON1070.GetValue(),
                    this.TXT01_PON1080.GetValue(),
                    Get_Numeric(this.TXT01_PON1090.GetValue().ToString()),
                    Get_Numeric(this.TXT01_PON1092.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_PRN1150.SetValue(dt.Rows[0]["PRN1150"].ToString()); // 요청수량
                    this.TXT01_PRN1170.SetValue(dt.Rows[0]["PRN1170"].ToString()); // 요청금액
                    this.TXT01_PRN3020.SetValue(dt.Rows[0]["PRN3020"].ToString()); // 발주잔량
                    this.TXT01_PRN3070.SetValue(dt.Rows[0]["PRN3070"].ToString()); // 발주잔액
                }
                else
                {
                    this.ShowMessage("TY_M_MR_2BL71537");

                    this.CBH01_PON1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 거래처
                if (this.CBH01_PON1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5395");

                    this.CBH01_PON1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 부가세 구분
                if (this.CBH01_PON1110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5393");

                    this.CBH01_PON1110.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수구분 = 금액일 경우 수량은 1임.
                if (this.CBO01_PON1120.GetValue().ToString() == "2")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())) != 1)
                    {
                        this.TXT01_PON1150.SetValue("1");

                        //this.ShowMessage("TY_M_MR_2BRAX669");

                        //this.TXT01_PON1150.Focus();

                        //e.Successed = false;
                        //return;
                    }
                }

                // 화폐
                if (this.CBH01_PON1130.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5392");

                    this.CBH01_PON1130.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 수량
                if (double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BM7R596");

                    this.TXT01_PON1150.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 검수구분 수량
                    if (this.CBO01_PON1120.GetValue().ToString() == "1")
                    {
                        // 요청수량보다 발주 수량이 넘을 순 없다.
                        if (double.Parse(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())))
                        {
                            this.ShowMessage("TY_M_MR_2BM7Y598");

                            this.TXT01_PON1150.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 발주 단가
                if (double.Parse(Get_Numeric(this.TXT01_PON1160.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BM7R597");

                    this.TXT01_PON1160.Focus();

                    e.Successed = false;
                    return;
                }

                // 적용환율
                if (this.CBH01_PON1130.GetValue().ToString() != "1")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PON1180.Text.Trim())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAA396");

                        this.TXT01_PON1180.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.TXT01_PON1180.SetValue("0");
                }

                if (this.TXT01_POM1410.GetValue().ToString() == "" && Get_Numeric(this.TXT01_POM1420.GetValue().ToString()) == "0")
                {
                    // 계약요청구분 'Y'인 경우
                    // 계약을 하기 위한 요청일 경우 거래처는 한개만 등록이 되어야 함.

                    if (this.fsPRM5100.ToString() == "Y")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BGAP406",
                            this.TXT01_PRN1000.GetValue().ToString(), // 사업부
                            this.TXT01_PRN1010.GetValue().ToString(), // 요청구분
                            this.TXT01_PRN1020.GetValue().ToString(), // 년월
                            this.TXT01_PRN1030.GetValue().ToString()  // 순번
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            if (this.CBH01_PON1100.GetValue().ToString() != dt.Rows[0]["PRN1100"].ToString())
                            {
                                this.ShowMessage("TY_M_MR_2BGA5394");

                                this.CBH01_PON1100.Focus();

                                e.Successed = false;
                                return;
                            }
                        }
                    }
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

                if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000")
                {
                    // 자산번호
                    if ((this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") &&
                        // 자산분류코드
                        (this.CBH01_PON1620.GetValue().ToString() == "")
                        )
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

                        e.Successed = false;
                        return;
                    }

                    // 신규구매건일 경우
                    // 자산계정이면서 자산번호가 공백이고 비품구분 = Y 이면 등록 안됨
                    if ((this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") &&
                        // 비품구분
                        this.CBO01_PON1510.GetValue().ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2BM5T588");

                        this.CBO01_PON1510.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 비품번호가 존재할 경우
                // 비품여부 = 'N', 자산계정은 올 수 없음.
                if (this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "")
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PON1530.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PON1531.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.SetFocus(TXT01_PON1530);

                        this.ShowMessage("TY_M_MR_2BM5O584");
                        e.Successed = false;
                        return;
                    }

                    // 비품여부
                    this.CBO01_PON1510.SetValue("N");

                    // 자산계정 체크
                    if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000")
                    {
                        this.TXT01_PON1070.Focus();

                        this.ShowMessage("TY_M_MR_2CEA5189");
                        e.Successed = false;
                        return;
                    }
                }

                if ((this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "") &&
                    (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "")
                    )
                {
                    this.ShowMessage("TY_M_MR_2CFCG212");
                    e.Successed = false;
                    return;
                }

                // 1) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000" && // 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "N")                       // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PON1620.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

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
                            this.CBH01_PON1620.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PON1620.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PON1620.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PON1620.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM51580");

                            this.SetFocus(this.CBH01_PON1620.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 2) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" &&// 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "N")                       // 비품구분
                {
                    // 고정자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5E581",
                        this.TXT01_PON1610.GetValue().ToString(),
                        Set_Fill4(this.TXT01_PON1611.GetValue().ToString()),
                        Set_Fill3(this.TXT01_PON1612.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PON1620.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(0, 1).ToString() + 
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(1, 1).ToString() + 
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(2, 2).ToString() + 
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM5J582");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

                        e.Successed = false;
                        return;
                    }
                }

                // 3) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" &&// 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "Y")                       // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PON1620.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

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
                            this.CBH01_PON1620.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PON1620.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PON1620.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PON1620.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM5O584");

                            this.TXT01_PON1530.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 4) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "Y")                       // 비품구분
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PON1530.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PON1531.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PON1620.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(0, 1).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(1, 1).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(2, 2).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM51580");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

                        e.Successed = false;
                        return;
                    }
                }

                // 5) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "N")                       // 비품구분
                {
                    this.CBH01_PON1620.SetValue("");
                    this.CBH01_PON1620.SetText("");
                    this.TXT01_PON16201NM.SetValue("");
                }

                // 자산의 수리시 수량도 나와야 하는 경우가 있음
                // 예) 타이어 교체

                // 자산번호가 존재한다는건 자산의 수리를 말함.
                if (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "")
                {
                    //if (this.CBO01_PON1120.GetValue().ToString() != "2")
                    //{
                    //    this.CBO01_PON1120.Focus();

                    //    this.ShowMessage("TY_M_MR_31M4H862");
                    //    e.Successed = false;
                    //    return;
                    //}
                }

                decimal dPON1150 = 0;
                decimal dPON1160 = 0;
                decimal dPON1180 = 0;

                // 발주수량
                dPON1150 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1150.GetValue().ToString())));
                // 발주단가
                dPON1160 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1160.GetValue().ToString())));
                // 적용환율
                dPON1180 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1180.GetValue().ToString())));

                // 발주금액
                if (double.Parse(Get_Numeric(this.TXT01_PON1180.GetValue().ToString())) == 0)
                {
                    this.TXT01_PON1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPON1150 * dPON1160)));

                    this.TXT01_PON1170.SetValue(UP_DotDelete(this.TXT01_PON1170.GetValue().ToString()));
                }
                else
                {
                    this.TXT01_PON1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPON1150 * dPON1160 * dPON1180)));
                }

                if (this.TXT01_POM1410.GetValue().ToString() == "" && Get_Numeric(this.TXT01_POM1420.GetValue().ToString()) == "0")
                {
                    // 입고잔량 = 발주수량
                    this.TXT01_PON1230.SetValue(Get_Numeric(this.TXT01_PON1150.GetValue().ToString()));
                    // 입고잔액 = 발주금액
                    this.TXT01_PON1330.SetValue(Get_Numeric(this.TXT01_PON1170.GetValue().ToString()));
                }

                // 검수구분 금액
                if (this.CBO01_PON1120.GetValue().ToString() == "2")
                {
                    // 남아 있는 요청금액보다 발주 금액이 넘을 순 없다.
                    if (double.Parse(Get_Numeric(this.TXT01_PRN1170.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_PON1170.GetValue().ToString())))
                    {
                        this.ShowMessage("TY_M_MR_2BM7Y599");

                        this.TXT01_PON1150.Focus();

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
                    "TY_P_MR_2BM87600",
                    this.TXT01_PON1000.GetValue().ToString(),
                    this.TXT01_PON1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PON1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_PON1030.GetValue().ToString())),
                    this.CBH01_PON1040.GetValue().ToString(),
                    this.CBH01_PON1060.GetValue().ToString(),
                    this.TXT01_PON1070.GetValue().ToString(),
                    this.TXT01_PON1080.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PON1090.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                }

                #endregion
            }

            string sPONUM = string.Empty;

            sPONUM = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

            // 입고테이블에 발주번호 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ19460",
                sPONUM.ToString()
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BJ4K470");
                e.Successed = false;
                return;
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ11461",
                this.TXT01_POM1000.GetValue().ToString(),
                this.TXT01_POM1010.GetValue().ToString(),
                this.TXT01_POM1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
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

            fsPRM5100 = "";
            fsPOM1400 = "";
            fsPOM1410 = "";
            fsPOM1420 = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPPOMF") // 마스터
            {
                fsPRM5100 = "";
                fsPOM1400 = "";
                fsPOM1410 = "";
                fsPOM1420 = "0";
                fsPOM5110 = "";

                // 사업부
                if (this.TXT01_POM1000.GetValue().ToString() != "A" && this.TXT01_POM1000.GetValue().ToString() != "S" &&
                    this.TXT01_POM1000.GetValue().ToString() != "T" && this.TXT01_POM1000.GetValue().ToString() != "B" &&
                    this.TXT01_POM1000.GetValue().ToString() != "C" && this.TXT01_POM1000.GetValue().ToString() != "D" &&
                    this.TXT01_POM1000.GetValue().ToString() != "E")
                {
                    this.ShowMessage("TY_M_MR_2BK3H504");

                    this.TXT01_POM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주년월
                if (this.TXT01_POM1020.GetValue().ToString().Length != 6)
                {
                    this.ShowMessage("TY_M_MR_2BK3G501");

                    this.TXT01_POM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 사업부
                if (this.TXT01_PRM1000.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3C496");

                    this.TXT01_PRM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 년월
                if (this.TXT01_PRM1020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3D498");

                    this.TXT01_PRM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 순번
                if (this.TXT01_PRM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3C497");

                    this.TXT01_PRM1030.Focus();

                    e.Successed = false;
                    return;
                }


                // 발의 사번
                if (this.CBH01_POM1140.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3D499");

                    this.CBH01_POM1140.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주일자
                if (this.DTP01_POM1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BLAQ525");

                    this.DTP01_POM1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 인도지역
                if (this.TXT01_POM1150.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D303");

                    this.TXT01_POM1150.Focus();

                    e.Successed = false;
                    return;
                }

                // 인도조건
                if (this.TXT01_POM1160.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D302");

                    this.TXT01_POM1160.Focus();

                    e.Successed = false;
                    return;
                }

                // 납기일자
                if (this.DTP01_POM1170.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C296");

                    this.DTP01_POM1170.Focus();

                    e.Successed = false;
                    return;
                }

                // 화폐구분
                if (this.CBH01_POM1700.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D301");

                    this.CBH01_POM1700.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 지불조건
                if (this.CBH01_POM1730.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D300");

                    this.CBH01_POM1730.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 공사 및 구매명
                if (this.TXT01_POM1180.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D299");

                    this.TXT01_POM1180.Focus();

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_PRM1000.GetValue().ToString() == "" ||
                    this.TXT01_PRM1020.GetValue().ToString() == "" ||
                    this.TXT01_PRM1030.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BK3D500");

                    this.TXT01_PRM1000.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 계약번호 및 요청금액 -> 발주금액
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2C7BD980",
                        this.TXT01_PRM1000.GetValue().ToString(),
                        this.TXT01_PRM1010.GetValue().ToString(),
                        this.TXT01_PRM1020.GetValue().ToString(),
                        this.TXT01_PRM1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        fsPRM5100 = dt.Rows[0]["PRM5100"].ToString();
                        fsPOM1400 = dt.Rows[0]["PRM5110"].ToString();
                        fsPOM1410 = dt.Rows[0]["PRM5120"].ToString();
                        fsPOM1420 = dt.Rows[0]["PRM5130"].ToString();

                        this.TXT01_POM1410.SetValue(fsPOM1410.ToString());
                        this.TXT01_POM1420.SetValue(fsPOM1420.ToString());

                        // 발주금액
                        //this.TXT01_POM1720.SetValue(dt.Rows[0]["PRN1170"].ToString());
                    }
                }

                /****************************************
                 * 비용청구 = 'Y'일 경우에만
                 * 청구구분, 청구화주 필드에 값을 입력 함.
                 ****************************************/
                if (this.CBO01_POM6010.GetValue().ToString() == "N")
                {
                    this.CBO01_POM6020.SetValue("3");
                    this.CBH01_POM6030.SetValue("");
                }
                else
                {
                    if (this.CBO01_POM6020.GetValue().ToString() == "3")
                    {
                        this.ShowMessage("TY_M_MR_3352R235");

                        this.CBO01_POM6020.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_POM6030.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_3352R236");

                        this.CBH01_POM6030.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 발주취소일, 취소사유
                if (this.TXT01_POM1300.GetValue().ToString() == "" && this.CBH01_POM1310.GetValue().ToString() != "")
                {
                    this.ShowMessage("TY_M_MR_2BU93766");

                    this.TXT01_POM1300.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주취소일, 취소사유
                if (this.TXT01_POM1300.GetValue().ToString() != "" && this.CBH01_POM1310.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BU93767");

                    this.CBH01_POM1310.CodeText.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "MRPPONF") // 내역사항
            {
                #region Description : KeyCheck

                // 귀속부서
                if (this.CBH01_PON1040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1381");

                    this.CBH01_PON1040.Focus();

                    e.Successed = false;
                    return;
                }

                // 예산구분
                if (this.CBH01_PON1060.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1380");

                    this.CBH01_PON1060.Focus();

                    e.Successed = false;
                    return;
                }

                // 계정코드
                if (this.TXT01_PON1070.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1379");

                    this.TXT01_PON1070.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAF400",
                        this.TXT01_PON1070.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAE399");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 계정과목명
                        this.TXT01_PON1070NM.SetValue(dt.Rows[0]["A1NMAC"].ToString());
                    }
                }

                // 예산 체크
                if (this.CBH01_PON1060.GetValue().ToString() == "1") // 투자예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAG401",
                        this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1090.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA1378");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PON1060.GetValue().ToString() == "2") // 소모성 예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAH402",
                        this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString(),
                        this.TXT01_PON1080.GetValue().ToString(),
                        this.TXT01_PON1090.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2390");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PON1060.GetValue().ToString() == "3") // 기타본예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAI403",
                        this.TXT01_POO1140.GetValue().ToString().Substring(0, 4),
                        this.CBH01_PON1040.GetValue().ToString(),
                        this.TXT01_PON1070.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2389");

                        this.TXT01_PON1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PON1060.GetValue().ToString() == "4") // 선급자재
                {
                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    if (this.TXT01_PON1070.GetValue().ToString() != "11101001" && this.TXT01_PON1070.GetValue().ToString() != "12210000")
                    {
                        this.ShowMessage("TY_M_MR_31L41841");

                        this.CBH01_PON1060.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 품목코드
                if (this.CBH01_PON1050.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA2388");

                    this.CBH01_PON1050.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAJ404",
                        this.CBH01_PON1050.GetValue().ToString().Substring(0, 1),
                        this.CBH01_PON1050.GetValue().ToString().Substring(1, 3),
                        this.CBH01_PON1050.GetValue().ToString().Substring(4, 3),
                        this.CBH01_PON1050.GetValue().ToString().Substring(7, 5)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2387");

                        this.CBH01_PON1050.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 품목명
                        this.CBH01_PON1050.SetText(dt.Rows[0]["Z105013"].ToString());
                    }
                }

                #endregion

                #region Description : 내용 체크

                //// 자산취득일경우 같은예산에 동일품목을 입력할 수 없음.
                //if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.CBH01_PON1620.GetValue().ToString() != "" &&
                //    this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_35N8I725",
                //        this.TXT01_PON1000.GetValue().ToString(),
                //        this.TXT01_PON1010.GetValue().ToString(),
                //        Get_Numeric(this.TXT01_PON1020.GetValue().ToString()),
                //        Set_Fill4(Get_Numeric(this.TXT01_PON1030.GetValue().ToString())),
                //        this.CBH01_PON1040.GetValue().ToString(),
                //        this.CBH01_PON1050.GetValue().ToString(),
                //        this.CBH01_PON1060.GetValue().ToString(),
                //        this.TXT01_PON1070.GetValue().ToString(),
                //        this.TXT01_PON1080.GetValue().ToString(),
                //        Get_Numeric(this.TXT01_PON1090.GetValue().ToString())
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count > 0)
                //    {
                //        this.ShowMessage("TY_M_MR_35N8E724");

                //        this.CBH01_PON1050.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                // 자산취득일 경우 예산의 6번째 숫자와 회계의 자산분류코드 1번째 숫자가 일치해야 한다.
                if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000" && this.CBH01_PON1620.GetValue().ToString() != "" &&
                    this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "")
                {
                    //if (this.TXT01_PON1070.GetValue().ToString().Substring(5, 1) != this.CBH01_PON1620.GetValue().ToString().Substring(0, 1))
                    //{
                    //    this.ShowMessage("TY_M_MR_66NGQ383");

                    //    this.CBH01_PON1620.Focus();

                    //    e.Successed = false;
                    //    return;
                    //}
                }

                // 선급자재일 경우 고정자산번호 및 분류코드는 공백이다.
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_PON1070.GetValue().ToString() == "11101001" || this.TXT01_PON1070.GetValue().ToString() == "12210000")
                {
                    this.TXT01_PON1610.SetValue("");
                    this.TXT01_PON1611.SetValue("");
                    this.TXT01_PON1612.SetValue("");
                    this.TXT01_PON1610NM.SetValue("");
                    this.CBH01_PON1620.SetValue("");
                    this.CBH01_PON1620.SetText("");
                    this.TXT01_PON16201NM.SetValue("");
                }

                // 계약번호 및 요청금액 -> 발주금액
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C7BD980",
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    this.TXT01_PRN1020.GetValue().ToString(),
                    this.TXT01_PRN1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsPRM5100 = dt.Rows[0]["PRM5100"].ToString();
                    fsPOM1400 = dt.Rows[0]["PRM5110"].ToString();
                    fsPOM1410 = dt.Rows[0]["PRM5120"].ToString();
                    fsPOM1420 = dt.Rows[0]["PRM5130"].ToString();

                    this.TXT01_POM1410.SetValue(fsPOM1410.ToString());
                    this.TXT01_POM1420.SetValue(fsPOM1420.ToString());

                    // 발주금액
                    //this.TXT01_POM1720.SetValue(dt.Rows[0]["PRN1170"].ToString());
                }

                // 요청잔량, 요청 잔액 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BM7E593",
                    this.TXT01_PRN1000.GetValue(),
                    this.TXT01_PRN1010.GetValue(),
                    this.TXT01_PRN1020.GetValue(),
                    Set_Fill4(this.TXT01_PRN1030.GetValue().ToString()),
                    this.CBH01_PON1040.GetValue(),
                    this.CBH01_PON1050.GetValue(),
                    this.CBH01_PON1060.GetValue(),
                    this.TXT01_PON1070.GetValue(),
                    this.TXT01_PON1080.GetValue(),
                    Get_Numeric(this.TXT01_PON1090.GetValue().ToString()),
                    Get_Numeric(this.TXT01_PON1092.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_PRN1150.SetValue(dt.Rows[0]["PRN1150"].ToString()); // 요청수량
                    this.TXT01_PRN1170.SetValue(dt.Rows[0]["PRN1170"].ToString()); // 요청금액
                    this.TXT01_PRN3020.SetValue(dt.Rows[0]["PRN3020"].ToString()); // 발주잔량
                    this.TXT01_PRN3070.SetValue(dt.Rows[0]["PRN3070"].ToString()); // 발주잔액
                }
                else
                {
                    this.ShowMessage("TY_M_MR_2BL71537");

                    this.CBH01_PON1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 거래처
                if (this.CBH01_PON1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5395");

                    this.CBH01_PON1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 부가세 구분
                if (this.CBH01_PON1110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5393");

                    this.CBH01_PON1110.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수구분 = 금액일 경우 수량은 1임.
                if (this.CBO01_PON1120.GetValue().ToString() == "2")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())) != 1)
                    {
                        this.TXT01_PON1150.SetValue("1");

                        //this.ShowMessage("TY_M_MR_2BRAX669");

                        //this.TXT01_PON1150.Focus();

                        //e.Successed = false;
                        //return;
                    }
                }

                // 화폐
                if (this.CBH01_PON1130.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5392");

                    this.CBH01_PON1130.Focus();

                    e.Successed = false;
                    return;
                }

                // 발주 수량
                if (double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BM7R596");

                    this.TXT01_PON1150.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    // 검수구분 수량
                    if (this.CBO01_PON1120.GetValue().ToString() == "1")
                    {
                        // 요청수량보다 발주 수량이 넘을 순 없다.
                        if (double.Parse(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_PON1150.GetValue().ToString())))
                        {
                            this.ShowMessage("TY_M_MR_2BM7Y598");

                            this.TXT01_PON1150.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 발주 단가
                if (double.Parse(Get_Numeric(this.TXT01_PON1160.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BM7R597");

                    this.TXT01_PON1160.Focus();

                    e.Successed = false;
                    return;
                }

                // 적용환율
                if (this.CBH01_PON1130.GetValue().ToString() != "1")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PON1180.Text.Trim())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAA396");

                        this.TXT01_PON1180.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.TXT01_PON1180.SetValue("0");
                }

                if (this.TXT01_POM1410.GetValue().ToString() == "" && Get_Numeric(this.TXT01_POM1420.GetValue().ToString()) == "0")
                {
                    // 계약요청구분 'Y'인 경우
                    // 계약을 하기 위한 요청일 경우 거래처는 한개만 등록이 되어야 함.

                    if (this.fsPRM5100.ToString() == "Y")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BGAP406",
                            this.TXT01_PRN1000.GetValue().ToString(), // 사업부
                            this.TXT01_PRN1010.GetValue().ToString(), // 요청구분
                            this.TXT01_PRN1020.GetValue().ToString(), // 년월
                            this.TXT01_PRN1030.GetValue().ToString()  // 순번
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            if (this.CBH01_PON1100.GetValue().ToString() != dt.Rows[0]["PRN1100"].ToString())
                            {
                                this.ShowMessage("TY_M_MR_2BGA5394");

                                this.CBH01_PON1100.Focus();

                                e.Successed = false;
                                return;
                            }
                        }
                    }
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

                if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000")
                {
                    // 자산번호
                    if ((this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") &&
                        // 자산분류코드
                        (this.CBH01_PON1620.GetValue().ToString() == "")
                        )
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

                        e.Successed = false;
                        return;
                    }

                    // 신규구매건일 경우
                    // 자산계정이면서 자산번호가 공백이고 비품구분 = Y 이면 등록 안됨
                    if ((this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") &&
                        // 비품구분
                        this.CBO01_PON1510.GetValue().ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2BM5T588");

                        this.CBO01_PON1510.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 비품번호가 존재할 경우
                // 비품여부 = 'N', 자산계정은 올 수 없음.
                if (this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "")
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PON1530.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PON1531.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.SetFocus(TXT01_PON1530);

                        this.ShowMessage("TY_M_MR_2BM5O584");
                        e.Successed = false;
                        return;
                    }

                    // 비품여부
                    this.CBO01_PON1510.SetValue("N");

                    // 자산계정 체크
                    if (this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000")
                    {
                        this.TXT01_PON1070.Focus();

                        this.ShowMessage("TY_M_MR_2CEA5189");
                        e.Successed = false;
                        return;
                    }
                }

                if ((this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "") &&
                    (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "")
                    )
                {
                    this.ShowMessage("TY_M_MR_2CFCG212");
                    e.Successed = false;
                    return;
                }

                // 1) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PON1070.GetValue().ToString() != "12210000" &&// 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "N")                       // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PON1620.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

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
                            this.CBH01_PON1620.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PON1620.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PON1620.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PON1620.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM51580");

                            this.SetFocus(this.CBH01_PON1620.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 2) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "N")                       // 비품구분
                {
                    // 고정자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5E581",
                        this.TXT01_PON1610.GetValue().ToString(),
                        Set_Fill4(this.TXT01_PON1611.GetValue().ToString()),
                        Set_Fill3(this.TXT01_PON1612.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PON1620.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(0, 1).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(1, 1).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(2, 2).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM5J582");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

                        e.Successed = false;
                        return;
                    }
                }

                // 3) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "Y")                       // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PON1620.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

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
                            this.CBH01_PON1620.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PON1620.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PON1620.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PON1620.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM5O584");

                            this.TXT01_PON1530.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 4) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() != "" && this.TXT01_PON1531.GetValue().ToString() != "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "Y")                       // 비품구분
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PON1530.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PON1531.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PON1620.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(0, 1).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(1, 1).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(2, 2).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PON1620.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM51580");

                        this.SetFocus(this.CBH01_PON1620.CodeText);

                        e.Successed = false;
                        return;
                    }
                }

                // 5) 자산분류코드 체크
                if ((this.TXT01_PON1530.GetValue().ToString() == "" && this.TXT01_PON1531.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PON1610.GetValue().ToString() == "" && this.TXT01_PON1611.GetValue().ToString() == "" && this.TXT01_PON1612.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PON1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PON1510.GetValue().ToString() == "N")                       // 비품구분
                {
                    this.CBH01_PON1620.SetValue("");
                    this.CBH01_PON1620.SetText("");
                    this.TXT01_PON16201NM.SetValue("");
                }

                // 자산의 수리시 수량도 나와야 하는 경우가 있음
                // 예) 타이어 교체

                // 자산번호가 존재한다는건 자산의 수리를 말함.
                if (this.TXT01_PON1610.GetValue().ToString() != "" && this.TXT01_PON1611.GetValue().ToString() != "" && this.TXT01_PON1612.GetValue().ToString() != "")
                {
                    //if (this.CBO01_PON1120.GetValue().ToString() != "2")
                    //{
                    //    this.CBO01_PON1120.Focus();

                    //    this.ShowMessage("TY_M_MR_31M4H862");
                    //    e.Successed = false;
                    //    return;
                    //}
                }

                decimal dPON1150 = 0;
                decimal dPON1160 = 0;
                decimal dPON1180 = 0;

                // 발주수량
                dPON1150 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1150.GetValue().ToString())));
                // 발주단가
                dPON1160 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1160.GetValue().ToString())));
                // 적용환율
                dPON1180 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PON1180.GetValue().ToString())));

                // 발주금액
                if (double.Parse(Get_Numeric(this.TXT01_PON1180.GetValue().ToString())) == 0)
                {
                    this.TXT01_PON1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPON1150 * dPON1160)));

                    this.TXT01_PON1170.SetValue(UP_DotDelete(this.TXT01_PON1170.GetValue().ToString()));
                }
                else
                {
                    this.TXT01_PON1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPON1150 * dPON1160 * dPON1180)));
                }

                if (this.TXT01_POM1410.GetValue().ToString() == "" && Get_Numeric(this.TXT01_POM1420.GetValue().ToString()) == "0")
                {
                    // 입고잔량 = 발주수량
                    this.TXT01_PON1230.SetValue(Get_Numeric(this.TXT01_PON1150.GetValue().ToString()));
                    // 입고잔액 = 발주금액
                    this.TXT01_PON1330.SetValue(Get_Numeric(this.TXT01_PON1170.GetValue().ToString()));
                }

                // 20140217일 수정 요청(윤홍준)
                // 발주시 요청금액보다 발주금액이 많을 경우가 발생함.

                // 검수구분 금액
                //if (this.CBO01_PON1120.GetValue().ToString() == "2")
                //{
                //    // 남아 있는 요청금액보다 발주 금액이 넘을 순 없다.
                //    if (double.Parse(Get_Numeric(this.TXT01_PRN1170.GetValue().ToString())) < double.Parse(Get_Numeric(this.TXT01_PON1170.GetValue().ToString())))
                //    {
                //        this.ShowMessage("TY_M_MR_2BM7Y599");

                //        this.TXT01_PON1170.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                // 예산 카운트(삭제시 필요)
                fsYESAN_COUNT = "0";

                // 예산 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BM87600",
                    this.TXT01_PON1000.GetValue().ToString(),
                    this.TXT01_PON1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PON1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_PON1030.GetValue().ToString())),
                    this.CBH01_PON1040.GetValue().ToString(),
                    this.CBH01_PON1060.GetValue().ToString(),
                    this.TXT01_PON1070.GetValue().ToString(),
                    this.TXT01_PON1080.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PON1090.GetValue().ToString())
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
                string sPONUM = string.Empty;

                sPONUM = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

                // 입고테이블에 발주번호 존재체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BJ19460",
                    sPONUM.ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BJ4K470");
                    e.Successed = false;
                    return;
                }

                // 결재 완료 문서 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BJ11461",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
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

            fsPRM5100 = "";
            fsPOM1400 = "";
            fsPOM1410 = "";
            fsPOM1420 = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPPOMF") // 마스터
            {
                // 내역사항 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BJ1A466",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z312");

                    this.CBH01_POM1140.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 특기사항 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BJ1A467",
                    this.TXT01_POM1000.GetValue().ToString(),
                    this.TXT01_POM1010.GetValue().ToString(),
                    this.TXT01_POM1020.GetValue().ToString(),
                    this.TXT01_POM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z311");

                    this.CBH01_POM1140.CodeText.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "MRPPONF") // 내역사항
            {
                // 계약번호 및 요청금액 -> 발주금액
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2C7BD980",
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    this.TXT01_PRN1020.GetValue().ToString(),
                    this.TXT01_PRN1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsPRM5100 = dt.Rows[0]["PRM5100"].ToString();
                    fsPOM1400 = dt.Rows[0]["PRM5110"].ToString();
                    fsPOM1410 = dt.Rows[0]["PRM5120"].ToString();
                    fsPOM1420 = dt.Rows[0]["PRM5130"].ToString();

                    this.TXT01_POM1410.SetValue(fsPOM1410.ToString());
                    this.TXT01_POM1420.SetValue(fsPOM1420.ToString());

                    // 발주금액
                    //this.TXT01_POM1720.SetValue(dt.Rows[0]["PRN1170"].ToString());
                }

                string sRRM1130 = string.Empty;

                // 요청번호
                sRRM1130 = this.TXT01_PON1000.GetValue().ToString() + this.TXT01_PON1010.GetValue().ToString() + this.TXT01_PON1020.GetValue().ToString() + this.TXT01_PON1030.GetValue().ToString();

                // 구매입고내역테이블에 고정자산생성번호 존재유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BG1A409",
                    sRRM1130.ToString()
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
                    "TY_P_MR_2BM87600",
                    this.TXT01_PON1000.GetValue().ToString(),
                    this.TXT01_PON1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PON1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_PON1030.GetValue().ToString())),
                    this.CBH01_PON1040.GetValue().ToString(),
                    this.CBH01_PON1060.GetValue().ToString(),
                    this.TXT01_PON1070.GetValue().ToString(),
                    this.TXT01_PON1080.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PON1090.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                }
            }

            string sPONUM = string.Empty;

            sPONUM = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

            // 입고테이블에 발주번호 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ19460",
                sPONUM.ToString()
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BJ4K470");
                e.Successed = false;
                return;
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ11461",
                this.TXT01_POM1000.GetValue().ToString(),
                this.TXT01_POM1010.GetValue().ToString(),
                this.TXT01_POM1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
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
            ds.Tables.Add(this.FPS91_TY_S_MR_2BJ65477.GetDataSourceInclude(TSpread.TActionType.New, "POT1040", "POT1100"));

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2BJ65477.GetDataSourceInclude(TSpread.TActionType.Update, "POT1040", "POT1100"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2BLB1558",
                                       TXT01_POT1000.GetValue(),
                                       TXT01_POT1010.GetValue(),
                                       TXT01_POT1020.GetValue(),
                                       TXT01_POT1030.GetValue(),
                                       ds.Tables[0].Rows[i]["POT1040"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z311");
                    e.Successed = false;
                    return;
                }
            }

            string sPONUM = string.Empty;

            sPONUM = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

            // 입고테이블에 발주번호 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ19460",
                sPONUM.ToString()
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BJ4K470");
                e.Successed = false;
                return;
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ11461",
                this.TXT01_POT1000.GetValue().ToString(),
                this.TXT01_POT1010.GetValue().ToString(),
                this.TXT01_POT1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_POT1030.GetValue().ToString())
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
            ds.Tables.Add(this.FPS91_TY_S_MR_2BJ65477.GetDataSourceInclude(TSpread.TActionType.Remove, "POT1040"));

            string sPONUM = string.Empty;

            sPONUM = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

            // 입고테이블에 발주번호 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ19460",
                sPONUM.ToString()
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BJ4K470");
                e.Successed = false;
                return;
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ11461",
                this.TXT01_POT1000.GetValue().ToString(),
                this.TXT01_POT1010.GetValue().ToString(),
                this.TXT01_POT1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_POT1030.GetValue().ToString())
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

        #region Description : 마감체크
        private bool UP_MAGAM_CHECK()
        {
            string sPONUM = string.Empty;

            sPONUM = this.TXT01_POM1000.GetValue().ToString() + this.TXT01_POM1010.GetValue().ToString() + this.TXT01_POM1020.GetValue().ToString() + Set_Fill4(this.TXT01_POM1030.GetValue().ToString());

            // 입고테이블에 발주번호 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ19460",
                sPONUM.ToString()
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.TXT01_MESSAGE.SetValue("입고자료에 발주번호가 존재합니다.");
                return false;
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BJ11461",
                this.TXT01_POM1000.GetValue().ToString(),
                this.TXT01_POM1010.GetValue().ToString(),
                this.TXT01_POM1020.GetValue().ToString(),
                Set_Fill4(this.TXT01_POM1030.GetValue().ToString())
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.TXT01_MESSAGE.SetValue("결재 완료 된 자료이므로 작업이 불가합니다.");
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

                if (this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Remove(this.tabPage3);
            }
            else if (sGUBUN == "MRPPOMF")
            {
                if (!this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Add(this.tabPage2);

                if (!this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Add(this.tabPage3);
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
                fsGUBUN = "MRPPOMF";

                // 마스터 DISPLAY
                UP_MRPPOMF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 마스터 데이터가 존재하므로 
                    // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                    UP_ImgbtnDisplay("1", true);

                    SetStartingFocus(this.CBH01_POM1140.CodeText);
                }
                else  // 마감완료면
                {
                    if (TYUserInfo.EmpNo.ToString() == "0377-M")
                    {
                        fsMAGAM = "MAGAM";

                        // 버튼 컨트롤
                        UP_ImgbtnDisplay("5", false);

                        this.SetFocus(this.DTP01_POM1100);
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
                this.TXT01_PRN1000.SetValue(this.TXT01_PRM1000.GetValue().ToString());
                this.TXT01_PRN1010.SetValue(this.TXT01_PRM1010.GetValue().ToString());
                this.TXT01_PRN1020.SetValue(this.TXT01_PRM1020.GetValue().ToString());
                this.TXT01_PRN1030.SetValue(this.TXT01_PRM1030.GetValue().ToString());

                fsPRM5100 = "";

                // 계약번호
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BLB0526",
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsPRM5100 = dt.Rows[0]["PRM5100"].ToString();
                    this.TXT01_POM1410.SetValue(dt.Rows[0]["PRM5120"].ToString());
                    this.TXT01_POM1420.SetValue(dt.Rows[0]["PRM5130"].ToString());
                }

                fsGUBUN = "MRPPONF";

                // 발주번호
                this.TXT01_PON1000.SetValue(this.TXT01_POM1000.GetValue());
                this.TXT01_PON1010.SetValue(this.TXT01_POM1010.GetValue());
                this.TXT01_PON1020.SetValue(this.TXT01_POM1020.GetValue());
                this.TXT01_PON1030.SetValue(this.TXT01_POM1030.GetValue());

                this.TXT01_PON1000.ReadOnly = true;
                this.TXT01_PON1010.ReadOnly = true;
                this.TXT01_PON1020.ReadOnly = true;
                this.TXT01_POT1030.ReadOnly = true;

                //this.TXT01_PON1000.BackColor = Color.Silver;
                //this.TXT01_PON1010.BackColor = Color.Silver;
                //this.TXT01_PON1020.BackColor = Color.Silver;
                //this.TXT01_PON1030.BackColor = Color.Silver;


                // 요청번호
                this.TXT01_PRN1000.SetValue(this.TXT01_PRM1000.GetValue());
                this.TXT01_PRN1010.SetValue(this.TXT01_PRM1010.GetValue());
                this.TXT01_PRN1020.SetValue(this.TXT01_PRM1020.GetValue());
                this.TXT01_PRN1030.SetValue(this.TXT01_PRM1030.GetValue());

                this.TXT01_PRN1000.ReadOnly = true;
                this.TXT01_PRN1010.ReadOnly = true;
                this.TXT01_PRN1020.ReadOnly = true;
                this.TXT01_PRN1030.ReadOnly = true;

                //this.TXT01_PRN1000.BackColor = Color.Silver;
                //this.TXT01_PRN1010.BackColor = Color.Silver;
                //this.TXT01_PRN1020.BackColor = Color.Silver;
                //this.TXT01_PRN1030.BackColor = Color.Silver;

                // 적용년월
                this.TXT01_POO1140.SetValue(this.TXT01_POM1020.GetValue().ToString().Substring(0, 4) + this.TXT01_POM1020.GetValue().ToString().Substring(4, 2));

                this.TXT01_POM1410.ReadOnly = true;
                this.TXT01_POM1420.ReadOnly = true;

                //this.TXT01_POM1410.BackColor = Color.Silver;
                //this.TXT01_POM1420.BackColor = Color.Silver;


                this.TXT01_PON1070NM.ReadOnly = true;
                this.TXT01_PON1080.ReadOnly   = true;
                this.TXT01_PON1080NM.ReadOnly = true;
                this.TXT01_PON1090.ReadOnly   = true;
                this.TXT01_PON1090NM.ReadOnly = true;
                this.TXT01_PON1092.ReadOnly   = true;
                this.TXT01_POO1140.ReadOnly   = true;
                this.CBH01_PON1050.SetReadOnly(true);

                //this.TXT01_PON1070NM.BackColor = Color.Silver;
                //this.TXT01_PON1080.BackColor   = Color.Silver;
                //this.TXT01_PON1080NM.BackColor = Color.Silver;
                //this.TXT01_PON1090.BackColor   = Color.Silver;
                //this.TXT01_POO1140.BackColor   = Color.Silver;

                this.TXT01_PON1530NM.ReadOnly = true;
                this.TXT01_PON1610NM.ReadOnly = true;
                this.TXT01_PON16201NM.ReadOnly = true;
                
                //this.TXT01_PON1530NM.BackColor = Color.Silver;
                //this.TXT01_PON1610NM.BackColor = Color.Silver;

                this.TXT01_PRN1150.ReadOnly = true;
                this.TXT01_PRN1170.ReadOnly = true;
                this.TXT01_PRN3020.ReadOnly = true;
                this.TXT01_PRN3070.ReadOnly = true;
                this.TXT01_PON1200.ReadOnly = true;
                this.TXT01_PON1210.ReadOnly = true;
                this.TXT01_PON1220.ReadOnly = true;
                this.TXT01_PON1230.ReadOnly = true;
                this.TXT01_PON1300.ReadOnly = true;
                this.TXT01_PON1310.ReadOnly = true;
                this.TXT01_PON1320.ReadOnly = true;
                this.TXT01_PON1330.ReadOnly = true;
                this.TXT01_PON1190.ReadOnly = true;
                this.TXT01_PON11901.ReadOnly = true;

                //this.TXT01_PRN1150.BackColor = Color.Silver;
                //this.TXT01_PRN1170.BackColor = Color.Silver;
                //this.TXT01_PRN3020.BackColor = Color.Silver;
                //this.TXT01_PRN3070.BackColor = Color.Silver;
                //this.TXT01_PON1200.BackColor = Color.Silver;
                //this.TXT01_PON1210.BackColor = Color.Silver;
                //this.TXT01_PON1220.BackColor = Color.Silver;
                //this.TXT01_PON1230.BackColor = Color.Silver;
                //this.TXT01_PON1300.BackColor = Color.Silver;
                //this.TXT01_PON1310.BackColor = Color.Silver;
                //this.TXT01_PON1320.BackColor = Color.Silver;
                //this.TXT01_PON1330.BackColor = Color.Silver;

                // 내역사항 DISPLAY
                UP_MRPPONF_DISPLAY();

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
                

                //UP_MRPPONF_DISPLAY();
            }
            else if (tabControl1.SelectedIndex == 2) // 특기사항
            {
                fsGUBUN = "MRPPOTF";

                this.TXT01_POT1000.SetValue(this.TXT01_POM1000.GetValue());
                this.TXT01_POT1010.SetValue(this.TXT01_POM1010.GetValue());
                this.TXT01_POT1020.SetValue(this.TXT01_POM1020.GetValue());
                this.TXT01_POT1030.SetValue(this.TXT01_POM1030.GetValue());

                this.TXT01_POT1000.ReadOnly = true;
                this.TXT01_POT1010.ReadOnly = true;
                this.TXT01_POT1020.ReadOnly = true;
                this.TXT01_POT1030.ReadOnly = true;

                //this.TXT01_POT1000.BackColor = Color.Silver;
                //this.TXT01_POT1010.BackColor = Color.Silver;
                //this.TXT01_POT1020.BackColor = Color.Silver;
                //this.TXT01_POT1030.BackColor = Color.Silver;

                // 특기사항 DISPLAY
                UP_MRPPOTF_DISPLAY();

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
        }
        #endregion

        #region Description : 컨트롤 초기화
        private void UP_Control_Initialize(string sGUBUN, bool bTrueFalse)
        {
            this.TXT01_PRM1010.SetReadOnly(true);
            this.TXT01_PON1000.SetReadOnly(true);
            this.TXT01_PON1010.SetReadOnly(true);
            this.TXT01_PON1020.SetReadOnly(true);
            this.TXT01_PON1030.SetReadOnly(true);

            this.TXT01_PRN1000.SetReadOnly(true);
            this.TXT01_PRN1010.SetReadOnly(true);
            this.TXT01_PRN1020.SetReadOnly(true);
            this.TXT01_PRN1030.SetReadOnly(true);

            this.TXT01_POT1000.SetReadOnly(true);
            this.TXT01_POT1010.SetReadOnly(true);
            this.TXT01_POT1020.SetReadOnly(true);
            this.TXT01_POT1030.SetReadOnly(true);

            this.TXT01_POM1410.SetReadOnly(true);
            this.TXT01_POM1420.SetReadOnly(true);

            this.TXT01_GAMOUNT.SetReadOnly(true);

            if (sGUBUN == "MRPPOMF") // 마스터
            {
                this.TXT01_POM1010.SetReadOnly(bTrueFalse);
                this.TXT01_POM1030.SetReadOnly(bTrueFalse);

                //this.TXT01_PRM1010.SetReadOnly(bTrueFalse);

                this.TXT01_PRM2020.SetReadOnly(bTrueFalse);
                this.TXT01_PRM2030.SetReadOnly(bTrueFalse);
                this.TXT01_PRM2040.SetReadOnly(bTrueFalse);

                this.TXT01_PRM2080.SetReadOnly(bTrueFalse);
                this.TXT01_PRM2090.SetReadOnly(bTrueFalse);
                this.TXT01_PRM2070.SetReadOnly(bTrueFalse);
                this.TXT01_PRM5130.SetReadOnly(bTrueFalse);
                this.TXT01_OPM1040.SetReadOnly(bTrueFalse);
                this.TXT01_DTDESC.SetReadOnly(bTrueFalse);
                this.TXT01_POM1720.SetReadOnly(bTrueFalse);
                this.TXT01_POM1500.SetReadOnly(bTrueFalse);
                this.TXT01_POM1230.SetReadOnly(bTrueFalse);
                this.TXT01_POM1200.SetReadOnly(bTrueFalse);
                this.TXT01_POM1210.SetReadOnly(bTrueFalse);
                this.TXT01_KBHANGL1.SetReadOnly(bTrueFalse);
                this.TXT01_POM1220.SetReadOnly(bTrueFalse);

                this.FPS91_TY_S_MR_2BJ69476.Initialize();
                this.FPS91_TY_S_MR_2BJ5Z475.Initialize();

                // 예산 DISPLAY
                UP_MRPPOOF_DISPLAY();
            }
            else if (sGUBUN == "MRPPONF") // 내역사항
            {
                // 버튼
                if (bTrueFalse == false)
                {
                    this.BTN61_PON10701.Enabled = true;
                }
                else
                {
                    this.BTN61_PON10701.Enabled = false;
                }

                //this.BTN61_PON10701.SetReadOnly(bTrueFalse);
                this.CBH01_PON1040.SetReadOnlyButton(bTrueFalse);
                this.CBH01_PON1060.SetReadOnlyButton(bTrueFalse);

                this.CBH01_PON1050.SetReadOnly(bTrueFalse);
                this.CBH01_PON1050.SetReadOnlyCode(bTrueFalse);
                this.CBH01_PON1050.SetReadOnlyText(bTrueFalse);

                this.CBH01_PON1040.SetReadOnly(bTrueFalse);
                this.CBH01_PON1040.SetReadOnlyCode(bTrueFalse);
                this.CBH01_PON1040.SetReadOnlyText(bTrueFalse);

                
                this.CBH01_PON1060.SetReadOnly(bTrueFalse);
                this.CBH01_PON1060.SetReadOnlyCode(bTrueFalse);
                this.CBH01_PON1060.SetReadOnlyText(bTrueFalse);
                this.TXT01_PON1070.ReadOnly = bTrueFalse;
            }
        }
        #endregion

        #region Description : 버튼 컨트롤
        private void UP_ImgbtnDisplay(string sGubn, bool bTrueFalse)
        {
            if (fsGUBUN == "MRPPOMF")
            {
                this.BTN61_NEW.Visible = false;
                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

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
            else if (fsGUBUN == "MRPPONF")
            {
                this.BTN61_NEW.Visible = true;
                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

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
            else if (fsGUBUN == "MRPPOTF")
            {
                this.BTN61_NEW.Visible  = false;
                this.BTN61_SAV.Visible  = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible  = false;

                this.BTN62_SAV.Visible  = true;
                this.BTN62_REM.Visible  = true;
            }

            if (this.TXT01_MESSAGE.GetValue().ToString() != "")
            {
                if (fsMAGAM.ToString() == "")
                {
                    this.BTN61_NEW.Visible = false;
                    this.BTN61_SAV.Visible = false;
                    if (TYUserInfo.EmpNo.ToString() != "0377-M")
                    {
                        this.BTN61_EDIT.Visible = false;
                    }
                    this.BTN61_REM.Visible = false;

                    this.BTN62_SAV.Visible = false;
                    this.BTN62_REM.Visible = false;
                }
            }
        }
        #endregion

        #region Description : 폼 닫기 이벤트
        private void TYMRPO001I_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsPOM1000 = this.TXT01_POM1000.GetValue().ToString();
            fsPOM1010 = this.TXT01_POM1010.GetValue().ToString();
            fsPOM1020 = this.TXT01_POM1020.GetValue().ToString();
            fsPOM1030 = this.TXT01_POM1030.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        #endregion

        #endregion
        


        #region Description : 요청번호 코드헬프

        private void TXT01_PRM1000_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRM1000.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_PRM10001_Click(null, null);
                }
            }
        }

        private void TXT01_PRM1020_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRM1020.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_PRM10001_Click(null, null);
                }
            }
        }

        private void TXT01_PRM1030_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRM1030.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    this.BTN61_PRM10001_Click(null, null);
                }
            }
        }

        private void BTN61_PRM10001_Click(object sender, EventArgs e)
        {
            if (this.TXT01_POM1000.GetValue().ToString() != "A" && this.TXT01_POM1000.GetValue().ToString() != "S" &&
                this.TXT01_POM1000.GetValue().ToString() != "T" && this.TXT01_POM1000.GetValue().ToString() != "B" &&
                this.TXT01_POM1000.GetValue().ToString() != "C" && this.TXT01_POM1000.GetValue().ToString() != "D" &&
                this.TXT01_POM1000.GetValue().ToString() != "E")
            {
                this.ShowMessage("TY_M_MR_2BK3H504");
                this.TXT01_POM1000.Focus();
                return;
            }

            // 발주년월
            if (this.TXT01_POM1020.GetValue().ToString().Length != 6)
            {
                this.ShowMessage("TY_M_MR_2BK3G501");
                this.TXT01_POM1020.Focus();
                return;
            }

            // 구매요청 코드헬프
            TYMZPO01C1 popup = new TYMZPO01C1(this.TXT01_POM1000.GetValue().ToString(), this.TXT01_POM1020.GetValue().ToString(), this.TXT01_POM1020.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PRM1000.SetValue(popup.fsPRM1000); // 사업부
                this.TXT01_PRM1020.SetValue(popup.fsPRM1020); // 년월
                this.TXT01_PRM1030.SetValue(popup.fsPRM1030); // 순서
                this.TXT01_PRM2020.SetValue(popup.fsPRM2020); // 발생일자
                this.TXT01_PRM2030.SetValue(popup.fsKBHANGL); // 신청자
                this.TXT01_POM1180.SetValue(popup.fsPRM2120); // 구매요청명
                this.TXT01_PRM2040.SetValue(popup.fsDTDESC1); // 부서명
                this.TXT01_PRM2080.SetValue(popup.fsPRM2080); // 기술검토
                this.TXT01_PRM2090.SetValue(popup.fsDTDESC2); // 기술검토부서
                this.TXT01_PRM2070.SetValue(popup.fsPRM2070); // 구매방법
                this.TXT01_PRM5130.SetValue(popup.fsPRM5130); // 계약번호
                this.TXT01_OPM1040.SetValue(popup.fsOPM1040); // 계약내용
                this.TXT01_POM1150.SetValue(popup.fsPRM2100); // 인도지역
                this.TXT01_POM1160.SetValue(popup.fsPRM2110); // 인도조건
                this.DTP01_POM1170.SetValue(popup.fsPRM2050); // 납기일자
                this.CBH01_POM1700.SetValue(popup.fsPRM3000); // 요청화폐
                this.CBH01_POM1730.SetValue(popup.fsPRM3020); // 지불조건
                this.CBO01_POM6010.SetValue(popup.fsPRM6010); // 비용청구
                this.CBO01_POM6020.SetValue(popup.fsPRM6020); // 청구구분
                this.CBH01_POM6030.SetValue(popup.fsPRM6030); // 지불조건

                // 발주사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_POM1140.SetValue(TYUserInfo.EmpNo);

                SetFocus(DTP01_POM1100);
            }
        }

        #endregion

        #region Description : 미발주 요청자료 조회
        private void UP_SetPRMData()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_74HIA307",
               fsPRM1000,
               fsPRM1010,
               fsPRM1020,
               fsPRM1030
               );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_PRM1000.SetValue(dt.Rows[0]["PRM1000"].ToString()); // 사업부
                this.TXT01_PRM1020.SetValue(dt.Rows[0]["PRM1020"].ToString()); // 년월
                this.TXT01_PRM1030.SetValue(dt.Rows[0]["PRM1030"].ToString()); // 순서
                this.TXT01_PRM2020.SetValue(dt.Rows[0]["PRM2020"].ToString()); // 발생일자
                this.TXT01_PRM2030.SetValue(dt.Rows[0]["KBHANGL"].ToString()); // 신청자
                this.TXT01_POM1180.SetValue(dt.Rows[0]["PRM2120"].ToString()); // 구매요청명
                this.TXT01_PRM2040.SetValue(dt.Rows[0]["DTDESC1"].ToString()); // 부서명
                this.TXT01_PRM2080.SetValue(dt.Rows[0]["PRM2080"].ToString()); // 기술검토
                this.TXT01_PRM2090.SetValue(dt.Rows[0]["DTDESC2"].ToString()); // 기술검토부서
                this.TXT01_PRM2070.SetValue(dt.Rows[0]["PRM2070"].ToString()); // 구매방법
                this.TXT01_PRM5130.SetValue(dt.Rows[0]["PRM5130"].ToString()); // 계약번호
                this.TXT01_OPM1040.SetValue(dt.Rows[0]["OPM1040"].ToString()); // 계약내용
                this.TXT01_POM1150.SetValue(dt.Rows[0]["PRM2100"].ToString()); // 인도지역
                this.TXT01_POM1160.SetValue(dt.Rows[0]["PRM2110"].ToString()); // 인도조건
                this.DTP01_POM1170.SetValue(dt.Rows[0]["PRM2050"].ToString()); // 납기일자
                this.CBH01_POM1700.SetValue(dt.Rows[0]["PRM3000"].ToString()); // 요청화폐
                this.CBH01_POM1730.SetValue(dt.Rows[0]["PRM3020"].ToString()); // 지불조건
                this.CBO01_POM6010.SetValue(dt.Rows[0]["PRM6010"].ToString()); // 비용청구
                this.CBO01_POM6020.SetValue(dt.Rows[0]["PRM6020"].ToString()); // 청구구분
                this.CBH01_POM6030.SetValue(dt.Rows[0]["PRM6030"].ToString()); // 지불조건
            }

            // 발주사번 <- 등록 및 수정 체크에 넣음
            this.CBH01_POM1140.SetValue(TYUserInfo.EmpNo);

            SetFocus(DTP01_POM1100);
        }
        #endregion

        #region Description : 비품년월
        private void TXT01_PON1530_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PON1530.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PON15301_Click(null, null);
                }
                else
                {
                    this.CBH01_PON1620.SetReadOnly(false);
                }
            }
        }
        #endregion

        #region Description : 비품순번
        private void TXT01_PON1531_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PON1531.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PON15301_Click(null, null);
                }
                else
                {
                    this.CBH01_PON1620.SetReadOnly(false);
                }
            }
        }
        #endregion

        #region Description : 비품버튼
        private void BTN61_PON15301_Click(object sender, EventArgs e)
        {
            TYMRGB009S popup = new TYMRGB009S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PON1530.SetValue(popup.fsMAYYMM);     // 비품년월
                this.TXT01_PON1531.SetValue(popup.fsMASEQ);      // 비품순번
                this.TXT01_PON1530NM.SetValue(popup.fsMABPDESC); // 비품명

                this.CBH01_PON1620.SetValue(popup.fsJASAN + popup.fsSAUPBU + popup.fsLARGE + popup.fsMIDDLE + popup.fsSMALL); // 자산분류코드
                this.CBH01_PON1620.SetText(popup.fsJASANNM);     // 자산분류명

                this.CBH01_PON1620.SetReadOnly(true);

                this.SetFocus(this.TXT01_PON1610);
            }
            else
            {
                this.CBH01_PON1620.SetReadOnly(false);

                this.SetFocus(this.TXT01_PON1610);
            }
        }
        #endregion

        #region Description : 자산년도
        private void TXT01_PON1610_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PON1610.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PON16101_Click(null, null);
                }
                else
                {
                    this.CBH01_PON1620.SetReadOnly(false);
                }
            }
        }
        #endregion

        #region Description : 자산순번
        private void TXT01_PON1611_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PON1611.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PON16101_Click(null, null);
                }
                else
                {
                    this.CBH01_PON1620.SetReadOnly(false);
                }
            }
        }
        #endregion

        #region Description : 가족코드
        private void TXT01_PON1612_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PON1612.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PON16101_Click(null, null);
                }
                else
                {
                    this.CBH01_PON1620.SetReadOnly(false);
                }
            }
        }
        #endregion

        #region Description : 고정자산번호 버튼
        private void BTN61_PON16101_Click(object sender, EventArgs e)
        {
            TYMRGB010S popup = new TYMRGB010S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PON1610.SetValue(popup.fsFXSYEAR);   // 자산년도
                this.TXT01_PON1611.SetValue(popup.fsFXSSEQ);    // 자산순번
                this.TXT01_PON1612.SetValue(popup.fsFXSSUBNUM); // 가족코드
                this.TXT01_PON1610NM.SetValue(popup.fsFXSNAME); // 자산명

                this.CBH01_PON1620.SetValue(popup.fsJASAN + popup.fsSAUPBU + popup.fsLARGE + popup.fsMIDDLE + popup.fsSMALL); // 자산분류코드
                this.CBH01_PON1620.SetText(popup.fsJASANNM);    // 자산분류명

                this.CBH01_PON1620.SetReadOnly(true);

                this.SetFocus(this.CBO01_PON1630);
            }
            else
            {
                this.CBH01_PON1620.SetReadOnly(false);

                this.SetFocus(this.CBH01_PON1620.CodeText);
            }
        }
        #endregion

        private void TXT01_PON1170_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : 예산 구분
        private void CBH01_PON1060_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_PON1060.GetValue().ToString() == "4")
            {
                this.TXT01_PON1070.SetReadOnly(true);
                this.TXT01_PON1070NM.SetReadOnly(true);
                this.BTN61_PON10701.Enabled = false;

                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (Convert.ToInt32(TXT01_PRN1020.GetValue().ToString()) >= 201901)
                {
                    this.TXT01_PON1070.SetValue("12210000");
                }
                else
                {
                    this.TXT01_PON1070.SetValue("11101001");
                }

                this.TXT01_PON1080.SetValue("");
                this.TXT01_PON1080NM.SetValue("");
                this.TXT01_PON1090.SetValue("");
                this.TXT01_PON1090NM.SetValue("");

            }
            else
            {
                this.TXT01_PON1070.SetReadOnly(false);
                this.TXT01_PON1070NM.SetReadOnly(false);
                this.BTN61_PON10701.Enabled = true;

                this.TXT01_PON1070.SetValue("");
            }
        }
        #endregion

        private void CBH01_PON1050_CodeBoxDataBinded(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // 품목별 최근 구매단가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_32DAH053",
                this.CBH01_PON1050.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_GAMOUNT.SetValue(dt.Rows[0]["RRN1210"].ToString());
            }
        }

        #region Description : 품목사진 버튼
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            string sJPCODE = string.Empty;

            if (this.CBH01_PON1050.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BGA2388");
                this.CBH01_PON1050.Focus();

                return;
            }
            else
            {
                sJPCODE = this.CBH01_PON1050.GetValue().ToString();
            }

            if ((new TYMRCO007I(sJPCODE.ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataTable dt = new DataTable();

                // 품목에 등록된 사진 개수 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_3499B468",
                    sJPCODE.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_Z106000.SetValue(dt.Rows[0]["Z106000"].ToString());
                }

                this.SetFocus(this.CBH01_PON1100);
            }
        }
        #endregion

        #region Description : 자산분류코드
        private void CBH01_PON1620_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string sPON1620NM_JASAN  = string.Empty;
            string sPON1620NM_SAUPBU = string.Empty;
            string sPON1620NM_LARGE  = string.Empty;
            string sPON1620NM_MIDDLE = string.Empty;
            string sPON1620NM_SMALL  = string.Empty;
            string sPON1620NM        = string.Empty;

            if (this.CBH01_PON1620.GetValue().ToString() != "")
            {
                DataTable dt = new DataTable();

                // 적용계정명
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BM1D571",
                    this.CBH01_PON1620.GetValue().ToString().Substring(0, 1),
                    this.CBH01_PON1620.GetValue().ToString().Substring(0, 1),
                    this.CBH01_PON1620.GetValue().ToString().Substring(1, 10),
                    ""
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sPON1620NM_JASAN  = dt.Rows[0]["ASDESC1"].ToString();
                    sPON1620NM_SAUPBU = dt.Rows[0]["CDCODE"].ToString();
                    sPON1620NM_LARGE  = dt.Rows[0]["FXLMDESC"].ToString();
                    sPON1620NM_MIDDLE = dt.Rows[0]["FXMMDESC"].ToString();
                    sPON1620NM_SMALL  = dt.Rows[0]["FXSMDESC"].ToString();

                    sPON1620NM = sPON1620NM_JASAN.ToString() + "-" + sPON1620NM_SAUPBU + "-" + sPON1620NM_LARGE.ToString() + "-" + sPON1620NM_MIDDLE.ToString();
                }

                this.TXT01_PON16201NM.SetValue(sPON1620NM.ToString());
            }
        }
        #endregion

        #region Descriptoin : 청구거래처 데이터 입력 이벤트
        private void CBH01_PON1100_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_PON1100.GetValue().ToString().Length == 6)
            {
                Nomubi_Check("");
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
                this.CBH01_PON1100.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VNCOGUBN"].ToString() == "Y")
                {
                    this.CBO01_PON7000.SetReadOnly(false);
                }
                else
                {
                    this.CBO01_PON7000.SetReadOnly(true);
                }
                if (sGUBUN != "RUN")
                {
                    this.CBO01_PON7000.SetValue(dt.Rows[0]["VNCOGUBN"].ToString());
                }
            }
        }
        #endregion


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
        //        this.TXT01_PON1620.SetValue(popup.fsFXSLASCODE); // 자산구분
        //        this.TXT01_PON1621.SetValue(popup.fsFXSLMCODE);  // 대분류
        //        this.TXT01_PON1622.SetValue(popup.fsFXSMMCODE);  // 중분류
        //        this.TXT01_PON1623.SetValue(popup.fsFXSMCODE);   // 소분류
        //        this.TXT01_PON1620NM.SetValue(popup.fsFXSMDESC); // 자산분류명
        //    }
        //}
        //#endregion
    }
}