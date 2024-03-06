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
//using System.Net.Mail;
using System.Web.Mail;

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
    ///  구매요청 공통
    ///  TY_P_MR_2BC54263 : 구매요청 삭제 - 입고번호체크(계약번호 존재시)
    ///  TY_P_MR_2BC50267 : 구매요청 삭제 - 발주번호체크(계약번호 없을경우)
    ///  TY_P_MR_2BC53268 : 구매요청 삭제 - 결재 완료 체크
    ///  TY_P_MR_2BE8V292 : 구매요청      - 계약 번호 가져오기
    ///  
    ///  구매요청 마스터
    ///  TY_P_MR_2BD2L282 : 구매요청 마스터(확인) - 팝업
    ///  TY_P_MR_2BCAF252 : 구매요청 마스터(예산조회-귀속부서별) - 팝업
    ///  TY_P_MR_2BD4K287 : 구매요청 마스터(예산조회-거래처별) - 팝업
    ///  TY_P_MR_2BD2Q283 : 구매요청 마스터 등록 - 팝업
    ///  TY_P_MR_2BD2T284 : 구매요청 마스터 수정 - 팝업
    ///  TY_P_MR_2BE3D308 : 구매요청 마스터 삭제 - 팝업
    ///  TY_P_MR_2BEBB293 : 사번 - 부서명 가져오기
    ///  TY_P_MR_2BE2R306 : 장기계약파일 조회
    ///  TY_P_MR_2BE2Y307 : 구매요청 마스터 - 순번 가져오기
    ///  
    ///  구매요청 내역
    ///  TY_P_MR_2BF8N367 : 구매요청 내역사항 확인 - 팝업
    ///  TY_P_MR_2BD2D280 : 구매요청 내역사항 조회 - 팝업
    ///  
    /// 
    /// 
    /// 
    ///  구매요청 특기
    ///  TY_P_MR_2BE4W310 : 구매요청 특기사항 조회 - 팝업
    ///  TY_P_MR_2BE6H327 : 구매요청 특기사항 - 순번 가져오기
    ///  TY_P_MR_2BE6J328 : 구매요청 특기사항 등록 - 팝업
    ///  TY_P_MR_2BE6K329 : 구매요청 특기사항 수정 - 팝업
    ///  TY_P_MR_2BE6M330 : 구매요청 특기사항 삭제 - 팝업
    /// 
    /// 
    /// 
    /// 
    ///  # 스프레드 정보 ####
    ///  구매요청 마스터
    ///  TY_S_MR_2BCBY255 : 구매요청 마스터(예산조회-귀속부서별) - 팝업
    ///  TY_S_MR_2BD4M288 : 구매요청 마스터(예산조회-거래처별) - 팝업
    ///  
    /// 
    /// 
    /// 
    /// 
    /// 
    ///  구매요청 내역사항
    ///  TY_P_MR_2BGAF400 : 구매 - 계정과목 체크
    ///  TY_P_MR_2BGAI403 : 구매 - 기타본예산 체크
    ///  TY_P_MR_2BGAH402 : 구매 - 소모성 예산 체크
    ///  TY_P_MR_2BGA8376 : 구매 - 장기계약 내역 값 가져오기
    ///  TY_P_MR_2BGA6375 : 구매 - 장기계약 체크
    ///  TY_P_MR_2BGAP406 : 구매 - 장기계약을 위한 요청일 경우 체크
    ///  TY_P_MR_2BGAG401 : 구매 - 투자수선 체크
    ///  TY_P_MR_2BGAJ404 : 구매 - 품목코드 체크
    ///  TY_P_MR_2BG31424 : 구매요청 - 같은 예산에 등록된 품목 건수 체크
    ///
    ///
    ///
    ///
    ///  구매요청 특기사항
    ///  TY_S_MR_2BC30258 : 구매요청 특기사항 조회 - 팝업
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BC57264 : 입고 자료에 요청번호가 존재합니다.
    ///  TY_M_MR_2BC58265 : 발주 자료에 요청번호가 존재합니다!
    ///  TY_M_MR_2BC59266 : 결재 완료 된 자료이므로 작업이 불가합니다!
    ///  TY_M_MR_2BC51262 : 결재 완료 된 문서가 아닙니다.
    ///  TY_M_MR_2BE2B295 : 검토 부서를 입력하세요.
    ///  TY_M_MR_2BE2C296 : 납기 일자를 확인하세요.
    ///  TY_M_MR_2BE2C297 : 요청 일자를 확인하세요.
    ///  TY_M_MR_2BE2C298 : 신청 사번을 입력하세요.
    ///  TY_M_MR_2BE2D299 : 공사 및 구매명을 입력하세요.
    ///  TY_M_MR_2BE2D300 : 지불 조건을 입력하세요.
    ///  TY_M_MR_2BE2D301 : 화폐 구분을 입력하세요. 
    ///  TY_M_MR_2BE2D302 : 인도 조건을 입력하세요.
    ///  TY_M_MR_2BE2D303 : 인도 지역을 입력하세요. 
    ///  TY_M_MR_2BE2E304 : 계약 번호를 확인하세요.
    ///  TY_M_MR_2BE2E305 : 계약요청구분은 장기계약에 대한 요청을 올릴 때만 'Y'입니다.
    ///  TY_M_MR_2BE4Z312 : 내역 사항이 존재 합니다.
    ///  TY_M_MR_2BE4Z311 : 특기 사항이 존재합니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_MR_2BF4Z352 : 처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_MR_2BF8A365 : 합계 부분입니다. 다른 데이터를 선택하세요.
    ///  TY_M_MR_2BGA1378 : 투자예산을 확인하세요.
    ///  TY_M_MR_2BGA1379 : 계정 코드를 입력하세요.
    ///  TY_M_MR_2BGAE399 : 계정 코드를 확인하세요.
    ///  TY_M_MR_2BGA1380 : 예산 구분을 입력하세요.
    ///  TY_M_MR_2BGA1381 : 귀속 부서를 입력하세요.
    ///  TY_M_MR_2BGA2382 : 장기계약에 등록 된 부가세 구분이 다릅니다.
    ///  TY_M_MR_2BGA2383 : 장기계약에 등록 된 단가와 다릅니다.
    ///  TY_M_MR_2BGA2384 : 장기계약에 등록 된 거래처와 다릅니다.
    ///  TY_M_MR_2BGA2385 : 계약번호에 해당하는 거래처를 확인하세요.
    ///  TY_M_MR_2BGA2386 : 계약번호에 해당하는 품옥을 확인하세요.
    ///  TY_M_MR_2BGA2387 : 품목 코드를 확인하세요.
    ///  TY_M_MR_2BGA2388 : 품목 코드를 입력하세요.
    ///  TY_M_MR_2BGA2389 : 기타예산을 확인하세요.
    ///  TY_M_MR_2BGA2390 : 소모성 예산을 확인하세요.
    ///  TY_M_MR_2BGA5392 : 화폐를 입력하세요.
    ///  TY_M_MR_2BGA5393 : TY_M_MR_2BGA5393
    ///  TY_M_MR_2BGA5394 : 계약을 하기 위한 요청일 경우 한가지 거래처만 등록 가능합니다.
    ///  TY_M_MR_2BGA5395 : 거래처를 입력하세요.
    ///  TY_M_MR_2BGAA396 : 적용 환율을 입력하세요.
    ///  TY_M_MR_2BGAA397 : 요청 단가를 입력하세요.
    ///  TY_M_MR_2BGAA398 : 요청 수량을 입력하세요.
    ///  TY_M_MR_2BGCZ407 : 자산 분류 코드를 입력하세요.
    ///  TY_M_MR_2BG15408 : 구매입고에 고정자산생성번호가 존재합니다.
    /// 
    /// 
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_MR_2BD3Y285 : 수정하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_MR_2BD3Z286 : 수정하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  NEW_MRP_NF : 신규(내역)
    ///  NEW_MRP_TF : 신규(특기)
    /// </summary>
    public partial class TYMRPR001I : TYBase
    {
        //private TYData DAT01_PRTHISAB;

        public string fsPRM1000 = string.Empty;
        public string fsPRM1010 = string.Empty;
        public string fsPRM1020 = string.Empty;
        public string fsPRM1030 = string.Empty;

        // 신규 버튼 클릭시 이전값 가지고 있을 변수
        private string fsPRN1040   = string.Empty;
        private string fsPRN1060   = string.Empty;
        private string fsPRN1070   = string.Empty;
        private string fsPRN1070NM = string.Empty;
        private string fsPRN1080   = string.Empty;
        private string fsPRN1080NM = string.Empty;
        private string fsPRN1090   = string.Empty;
        private string fsPRN1090NM = string.Empty;
        private string fsPRO2040   = string.Empty;
        private string fsPRN1100   = string.Empty;
        private string fsPRN1110   = string.Empty;

        private string fsYESAN_COUNT = string.Empty;
        private string fsPRM5110     = string.Empty;

        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYMRPR001I(string sPRM1000, string sPRM1010, string sPRM1020, string sPRM1030)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsPRM1000 = sPRM1000;
            this.fsPRM1010 = sPRM1010;
            this.fsPRM1020 = sPRM1020;
            this.fsPRM1030 = sPRM1030;

            this.TXT01_PRM1000.SetValue(fsPRM1000);
            this.TXT01_PRM1010.SetValue(fsPRM1010);
            this.TXT01_PRM1020.SetValue(fsPRM1020);
            this.TXT01_PRM1030.SetValue(fsPRM1030);

            this.TXT01_PRN1000.SetValue(fsPRM1000);
            this.TXT01_PRN1010.SetValue(fsPRM1010);
            this.TXT01_PRN1020.SetValue(fsPRM1020);
            this.TXT01_PRN1030.SetValue(fsPRM1030);

            this.TXT01_PRT1000.SetValue(fsPRM1000);
            this.TXT01_PRT1010.SetValue(fsPRM1010);
            this.TXT01_PRT1020.SetValue(fsPRM1020);
            this.TXT01_PRT1030.SetValue(fsPRM1030);
        }

        private void TYMRPR001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_OPM10401.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_PRN10701.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN61_PRN50301.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN61_PRN60101.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_SAV.ProcessCheck  += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(BTN61_EDIT_ProcessCheck);
            this.BTN61_REM.ProcessCheck  += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);
            //this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2BC30258, "PRT1040");

            bool fResult;

            // 마스터 검토부서
            this.CBH01_PRM2090.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            // 등록
            if (this.TXT01_PRM1000.GetValue().ToString() == ""  &&
                this.TXT01_PRM1010.GetValue().ToString() == "P" &&
                this.TXT01_PRM1020.GetValue().ToString() == ""  &&
                this.TXT01_PRM1030.GetValue().ToString() == ""
                )
            {
                fsGUBUN = "MRPPRMF";

                // 신청사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_PRM2040.SetValue(TYUserInfo.EmpNo);

                // 등록 시 요청부서의 앞자리 가져옴
                this.TXT01_PRM1000.SetValue(this.TXT01_PRM2010.GetValue().ToString().Substring(0, 1));

                // 컨트롤 초기화
                UP_Control_Initialize("MRPPRMF", true);

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                // 탭 컨트롤
                tabControl1_Enable("");

                // 화폐구분
                this.CBH01_PRM3000.SetValue("1");
                // 당사지불조건
                this.CBH01_PRM3020.SetValue("01");

                // 비용청구
                this.CBO01_PRM6010.SetValue("N");
                // 청구구분
                this.CBO01_PRM6020.SetValue("3");

                // 요청년월
                this.TXT01_PRM1020.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6));

                // 신청사번 <- 등록 및 수정 체크에 넣음
                this.CBH01_PRM2040.SetValue(TYUserInfo.EmpNo);

                // 등록 시 요청부서의 앞자리 가져옴
                this.TXT01_PRM1000.SetValue(this.TXT01_PRM2010.GetValue().ToString().Substring(0, 1));

                SetStartingFocus(this.TXT01_PRM1000);
            }
            else // 수정
            {
                this.TXT01_PRM1000.SetReadOnly(true);
                this.TXT01_PRM1010.SetReadOnly(true);
                this.TXT01_PRM1020.SetReadOnly(true);
                this.TXT01_PRM1030.SetReadOnly(true);

                this.TXT01_PRM5120.SetReadOnly(true);
                this.TXT01_PRM5130.SetReadOnly(true);
                this.BTN61_OPM10401.Enabled = false;
                //this.BTN61_OPM10401.SetReadOnly(true);
                this.TXT01_OPM1040.SetReadOnly(true);

                fsGUBUN = "MRPPRMF";

                // 컨트롤 초기화
                UP_Control_Initialize("MRPPRMF", true);

                // 마스터 DISPLAY
                UP_MRPPRMF_DISPLAY();

                // 내역사항 DISPLAY
                UP_MRPPRNF_DISPLAY();

                // 특기사항 DISPLAY
                UP_MRPPRTF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    this.SetFocus(this.CBH01_PRM2040.CodeText);
                    //this.SetStartingFocus(this.CBH01_PRM2040.CodeText);
                }
                else
                {
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("3", false);
                }
            }            
        }
        #endregion

        #region Description : 내역사항 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            UP_ImgbtnDisplay("2", false);

            // 컨트롤 초기화
            UP_Control_Initialize("MRPPRNF", false);

            this.CBH01_PRN1040.SetValue("");
            this.CBH01_PRN1060.SetValue("");

            this.TXT01_PRN1070.SetValue("");
            this.TXT01_PRN1070NM.SetValue("");
            this.TXT01_PRN1080.SetValue("");
            this.TXT01_PRN1080NM.SetValue("");
            this.TXT01_PRN1090.SetValue("");
            this.TXT01_PRN1090NM.SetValue("");
            this.TXT01_PRN1092.SetValue("");
            this.BTN61_PRN10701.Enabled = true;

            this.TXT01_PRN1092.SetReadOnly(true);

            // 장기계약일 경우 체크
            if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
            {
                this.CBH01_PRN1050.Initialize();
                this.CBH01_PRN1050.SetValue("");
                this.CBH01_PRN1050.SetText("");
            }
            else
            {
                this.CBH01_PRN1051.Initialize();
                this.CBH01_PRN1051.SetValue("");
                this.CBH01_PRN1051.SetText("");
            }

            UP_FieldClear("MRPPRNF");

            // 신규 버튼 클릭시 이전값을 필드에 뿌려줌
            UP_SET_MRPPRNF_REMEMBER();

            // 선급자재
            if (this.CBO01_PRM6010.GetValue().ToString() == "Y" && this.CBO01_PRM6020.GetValue().ToString() == "2")
            {
                this.CBH01_PRN1060.SetValue("4");

                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                //if (Convert.ToInt32(TXT01_PRN1020.GetValue().ToString()) >= 201901)
                //{
                this.TXT01_PRN1070.SetValue("12210000");
                //}
                //else
                //{
                //    this.TXT01_PRN1070.SetValue("11101001");
                //}

                this.CBH01_PRN1060.SetReadOnly(true);

                this.TXT01_PRN1070.SetReadOnly(true);
                this.TXT01_PRN1070NM.SetReadOnly(true);

                this.BTN61_PRN10701.Enabled = false;
            }
            else
            {
                this.CBH01_PRN1060.SetReadOnly(false);

                this.TXT01_PRN1070.SetReadOnly(false);
                this.TXT01_PRN1070NM.SetReadOnly(false);

                this.BTN61_PRN10701.Enabled = true;
            }

            if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
            {
                this.CBH01_PRN1040.SetReadOnly(true);
                this.CBH01_PRN1060.SetReadOnly(true);
                this.CBH01_PRN1100.SetReadOnly(true);
                this.CBH01_PRN1110.SetReadOnly(true);

                this.TXT01_PRN1070.SetReadOnly(true);
                this.TXT01_PRN1070NM.SetReadOnly(true);
                this.TXT01_PRN1080.SetReadOnly(true);
                this.TXT01_PRN1080NM.SetReadOnly(true);
                this.TXT01_PRN1090.SetReadOnly(true);
                this.TXT01_PRN1090NM.SetReadOnly(true);
                this.TXT01_PRN1092.SetReadOnly(true);
                this.TXT01_PRN1160.SetReadOnly(true);

                //this.TXT01_PRN1070.BackColor   = Color.Silver;
                //this.TXT01_PRN1070NM.BackColor = Color.Silver;
                //this.TXT01_PRN1080.BackColor   = Color.Silver;
                //this.TXT01_PRN1080NM.BackColor = Color.Silver;
                //this.TXT01_PRN1090.BackColor   = Color.Silver;
                //this.TXT01_PRN1160.BackColor   = Color.Silver;

                SetFocus(this.CBH01_PRN1051.CodeText);
            }
            else
            {
                SetFocus(this.CBH01_PRN1040.CodeText);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sOUT_MSG = string.Empty;

            if (fsGUBUN == "MRPPRMF") // 마스터
            {
                // 원본
                // 등록
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BD2Q283",
                //    //"TY_P_MR_37UA1263",
                //    this.TXT01_PRM1000.GetValue().ToString(),
                //    this.TXT01_PRM1010.GetValue().ToString(),
                //    this.TXT01_PRM1020.GetValue().ToString(),
                //    this.TXT01_PRM1030.GetValue().ToString(),
                //    //this.TXT01_PRM1000.GetValue().ToString(),
                //    //this.TXT01_PRM1010.GetValue().ToString(),
                //    //this.TXT01_PRM1020.GetValue().ToString(),
                //    this.TXT01_PRM2010.GetValue().ToString(),
                //    this.DTP01_PRM2020.GetValue().ToString(),
                //    this.TXT01_PRM2030.GetValue().ToString(),
                //    this.CBH01_PRM2040.GetValue().ToString(),
                //    this.DTP01_PRM2050.GetValue().ToString(),
                //    this.CBO01_PRM2060.GetValue().ToString(),
                //    this.CBO01_PRM2070.GetValue().ToString(),
                //    this.CBO01_PRM2080.GetValue().ToString(),
                //    this.CBH01_PRM2090.GetValue().ToString(),
                //    this.TXT01_PRM2100.GetValue().ToString(),
                //    this.TXT01_PRM2110.GetValue().ToString(),
                //    this.TXT01_PRM2120.GetValue().ToString(),
                //    this.CBH01_PRM3000.GetValue().ToString(),
                //    this.TXT01_PRM3010.GetValue().ToString(),
                //    this.CBH01_PRM3020.GetValue().ToString(),
                //    this.TXT01_PRM4000.GetValue().ToString(),
                //    this.TXT01_PRM4010.GetValue().ToString(),
                //    this.TXT01_PRM4020.GetValue().ToString(),
                //    this.TXT01_PRM4030.GetValue().ToString(),
                //    this.TXT01_PRM4040.GetValue().ToString(),
                //    this.TXT01_PRM4050.GetValue().ToString(),
                //    this.CBO01_PRM5100.GetValue().ToString(),
                //    fsPRM5110.ToString(),
                //    this.TXT01_PRM5120.GetValue().ToString(),
                //    Get_Numeric(this.TXT01_PRM5130.GetValue().ToString()),
                //    this.CBO01_PRM6010.GetValue().ToString(),
                //    this.CBO01_PRM6020.GetValue().ToString(),
                //    this.CBH01_PRM6030.GetValue().ToString(),
                //    TYUserInfo.EmpNo.ToString()
                //    );

                //this.DbConnector.ExecuteNonQueryList();
                //this.ShowMessage("TY_M_GB_23NAD873");


                string sAUT1030 = string.Empty;

                // 수정(구매요청 SP)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_37U2W278",
                    //"TY_P_MR_37UA1263",
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM2010.GetValue().ToString(),
                    this.DTP01_PRM2020.GetValue().ToString(),
                    this.TXT01_PRM2030.GetValue().ToString(),
                    this.CBH01_PRM2040.GetValue().ToString(),
                    this.DTP01_PRM2050.GetValue().ToString(),
                    this.CBO01_PRM2060.GetValue().ToString(),
                    this.CBO01_PRM2070.GetValue().ToString(),
                    this.CBO01_PRM2080.GetValue().ToString(),
                    this.CBH01_PRM2090.GetValue().ToString(),
                    this.TXT01_PRM2100.GetValue().ToString(),
                    this.TXT01_PRM2110.GetValue().ToString(),
                    this.TXT01_PRM2120.GetValue().ToString(),
                    this.CBH01_PRM3000.GetValue().ToString(),
                    this.TXT01_PRM3010.GetValue().ToString(),
                    this.CBH01_PRM3020.GetValue().ToString(),
                    this.TXT01_PRM4000.GetValue().ToString(),
                    this.TXT01_PRM4010.GetValue().ToString(),
                    this.TXT01_PRM4020.GetValue().ToString(),
                    this.TXT01_PRM4030.GetValue().ToString(),
                    this.TXT01_PRM4040.GetValue().ToString(),
                    this.TXT01_PRM4050.GetValue().ToString(),
                    this.CBO01_PRM5100.GetValue().ToString(),
                    fsPRM5110.ToString(),
                    this.TXT01_PRM5120.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRM5130.GetValue().ToString()),
                    this.CBO01_PRM6010.GetValue().ToString(),
                    this.CBO01_PRM6020.GetValue().ToString(),
                    this.CBH01_PRM6030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    sOUT_MSG.ToString()
                    );

                sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
                {
                    this.TXT01_PRM1030.SetValue(sOUT_MSG.Substring(3, 4));

                    this.ShowMessage("TY_M_GB_23NAD873");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_246A2488");

                    return;
                }
                
                


                // 순번 가져오기
                

                //DataTable dt = new DataTable();

                //// 순번 가져오기
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_37U9X262",
                //    TXT01_PRM1000.GetValue().ToString(),
                //    TXT01_PRM1010.GetValue().ToString(),
                //    Get_Numeric(TXT01_PRM1020.GetValue().ToString()),
                //    TYUserInfo.EmpNo.ToString()
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.TXT01_PRM1030.SetValue(dt.Rows[0]["PRM1030"].ToString());
                //}

                // 탭 컨트롤
                tabControl1_Enable("MRPPRMF");

                // 버튼 컨트롤
                UP_ImgbtnDisplay("1", true);

                this.TXT01_PRM1000.SetReadOnly(true);
                this.TXT01_PRM1010.SetReadOnly(true);
                this.TXT01_PRM1020.SetReadOnly(true);
                this.TXT01_PRM1030.SetReadOnly(true);

                UP_MRPPRMF_DISPLAY();
            }
            else if (fsGUBUN == "MRPPRNF") // 내역사항
            {
                string sPRN5030 = string.Empty;
                string sPRN6010 = string.Empty;
                string sPRN1050 = string.Empty;
                string sPRN1140 = string.Empty;

                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    sPRN1050 = this.CBH01_PRN1051.GetValue().ToString();
                }
                else
                {
                    sPRN1050 = this.CBH01_PRN1050.GetValue().ToString();
                }

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CH3M224",
                    sPRN1050.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sPRN1140 = dt.Rows[0]["Z105023"].ToString();
                }

                sPRN5030 = "";
                // 비품번호
                if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                {
                    sPRN5030 = this.TXT01_PRN5030.GetValue().ToString() + Set_Fill3(this.TXT01_PRN5031.GetValue().ToString());
                }

                sPRN6010 = "";
                // 고정자산번호
                if (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "")
                {
                    sPRN6010 = this.TXT01_PRN6010.GetValue().ToString() + Set_Fill4(this.TXT01_PRN6011.GetValue().ToString()) + Set_Fill3(this.TXT01_PRN6012.GetValue().ToString());
                }


                // 내역사항 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BG28410",
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    this.TXT01_PRN1020.GetValue().ToString(),
                    this.TXT01_PRN1030.GetValue().ToString(),
                    this.CBH01_PRN1040.GetValue().ToString(),
                    sPRN1050.ToString(),
                    this.CBH01_PRN1060.GetValue().ToString(),
                    this.TXT01_PRN1070.GetValue().ToString(),
                    this.TXT01_PRN1080.GetValue().ToString(),
                    this.TXT01_PRN1090.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRN1092.GetValue().ToString()),
                    this.CBH01_PRN1100.GetValue().ToString(),
                    this.CBH01_PRN1110.GetValue().ToString(),
                    this.CBO01_PRN1120.GetValue().ToString(),
                    this.CBH01_PRN1130.GetValue().ToString(),
                    sPRN1140.ToString(),
                    //this.CBH01_PRN1130.GetText().ToString(),
                    this.TXT01_PRN1150.GetValue().ToString(),
                    this.TXT01_PRN1160.GetValue().ToString(),
                    this.TXT01_PRN1170.GetValue().ToString(),
                    this.TXT01_PRN1180.GetValue().ToString(),
                    this.TXT01_PRN2010.GetValue().ToString(),
                    this.TXT01_PRN2020.GetValue().ToString(),
                    this.TXT01_PRN2030.GetValue().ToString(),
                    this.TXT01_PRN2040.GetValue().ToString(),
                    this.TXT01_PRN2060.GetValue().ToString(),
                    this.TXT01_PRN2070.GetValue().ToString(),
                    this.TXT01_PRN2080.GetValue().ToString(),
                    this.TXT01_PRN2090.GetValue().ToString(),
                    this.TXT01_PRN3010.GetValue().ToString(),
                    this.TXT01_PRN3020.GetValue().ToString(),
                    this.TXT01_PRN3060.GetValue().ToString(),
                    this.TXT01_PRN3070.GetValue().ToString(),
                    this.CBO01_PRN5000.GetValue().ToString(),
                    this.CBO01_PRN5010.GetValue().ToString(),
                    sPRN5030.ToString(),
                    sPRN6010.ToString(),
                    this.CBH01_PRN6020.GetValue().ToString(),
                    this.CBO01_PRN6030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString()
                    );

                // 귀속별 예산파일 등록
                if (fsYESAN_COUNT.ToString() == "0") // 등록
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG2A414",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString(),
                        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                        this.TXT01_PRO2040.GetValue().ToString().Substring(4, 2),
                        TYUserInfo.EmpNo.ToString()
                        );
                }
                else // 수정
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG2A415",
                        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                        this.TXT01_PRO2040.GetValue().ToString().Substring(4, 2),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString()
                        );
                }

                this.DbConnector.ExecuteNonQueryList();

                string sOUTMSG = string.Empty;

                sOUTMSG = "OK";

                // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                // 선급자재일 경우 예산 업데이트 안함.
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_PRM5100_1.GetValue().ToString() != "Y" && this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                {
                    // 예산 가용금액 - 플러스
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG51426",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        sPRN1050.ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PRN1092.GetValue().ToString()),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 신규 버튼 클릭시 이전값 가지고 있을 변수
                    UP_GET_MRPPRNF_REMEMBER("ADD");

                    // 마스터 테이블에 요청금액 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BI1Q431",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString()
                        );

                    this.DbConnector.ExecuteNonQuery();

                    this.ShowMessage("TY_M_GB_23NAD873");

                    // 버튼 초기화
                    UP_ImgbtnDisplay("3", false);

                    // 컨트롤 초기화
                    UP_Control_Initialize("MRPPRNF", true);

                    this.CBH01_PRN1040.SetValue("");
                    this.CBH01_PRN1060.SetValue("");

                    this.TXT01_PRN1070.SetValue("");
                    this.TXT01_PRN1070NM.SetValue("");
                    this.TXT01_PRN1080.SetValue("");
                    this.TXT01_PRN1080NM.SetValue("");
                    this.TXT01_PRN1090.SetValue("");
                    this.TXT01_PRN1090NM.SetValue("");
                    this.TXT01_PRN1092.SetValue("");
                    // 장기계약일 경우 체크
                    if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                    {
                        this.CBH01_PRN1051.SetValue("");
                        this.CBH01_PRN1051.SetText("");
                    }
                    else
                    {
                        this.CBH01_PRN1050.SetValue("");
                        this.CBH01_PRN1050.SetText("");
                    }

                    UP_FieldClear("MRPPRNF");

                    UP_MRPPRNF_DISPLAY();

                    SetFocus(this.CBH01_PRN1040.CodeText);
                }
            }
        }
        #endregion

        #region Description : 수정 버튼
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "MRPPRMF") // 마스터
            {
                // 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BD2T284",
                    this.TXT01_PRM2010.GetValue().ToString(),
                    this.DTP01_PRM2020.GetValue().ToString(),
                    this.TXT01_PRM2030.GetValue().ToString(),
                    this.CBH01_PRM2040.GetValue().ToString(),
                    this.DTP01_PRM2050.GetValue().ToString(),
                    this.CBO01_PRM2060.GetValue().ToString(),
                    this.CBO01_PRM2070.GetValue().ToString(),
                    this.CBO01_PRM2080.GetValue().ToString(),
                    this.CBH01_PRM2090.GetValue().ToString(),
                    this.TXT01_PRM2100.GetValue().ToString(),
                    this.TXT01_PRM2110.GetValue().ToString(),
                    this.TXT01_PRM2120.GetValue().ToString(),
                    this.CBH01_PRM3000.GetValue().ToString(),
                    this.TXT01_PRM3010.GetValue().ToString(),
                    this.CBH01_PRM3020.GetValue().ToString(),
                    this.TXT01_PRM4000.GetValue().ToString(),
                    this.TXT01_PRM4010.GetValue().ToString(),
                    this.TXT01_PRM4020.GetValue().ToString(),
                    this.TXT01_PRM4030.GetValue().ToString(),
                    this.TXT01_PRM4040.GetValue().ToString(),
                    this.TXT01_PRM4050.GetValue().ToString(),
                    this.CBO01_PRM5100.GetValue().ToString(),
                    fsPRM5110.ToString(),
                    this.TXT01_PRM5120.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRM5130.GetValue().ToString()),
                    this.CBO01_PRM6010.GetValue().ToString(),
                    this.CBO01_PRM6020.GetValue().ToString(),
                    this.CBH01_PRM6030.GetValue().ToString(),
                    TYUserInfo.EmpNo.ToString(),
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQueryList();
                this.ShowMessage("TY_M_MR_2BD3Z286");

                // 탭 컨트롤
                tabControl1_Enable("MRPPRMF");

                UP_MRPPRMF_DISPLAY();
            }
            else if (fsGUBUN == "MRPPRNF") // 내역사항
            {
                string sOUTMSG  = string.Empty;

                string sPRN5030 = string.Empty;
                string sPRN6010 = string.Empty;
                string sPRN1050 = string.Empty;
                string sPRN1140 = string.Empty;

                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    sPRN1050 = this.CBH01_PRN1051.GetValue().ToString();
                }
                else
                {
                    sPRN1050 = this.CBH01_PRN1050.GetValue().ToString();
                }

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CH3M224",
                    sPRN1050.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sPRN1140 = dt.Rows[0]["Z105023"].ToString();
                }

                sPRN5030 = "";
                // 비품번호
                if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                {
                    sPRN5030 = this.TXT01_PRN5030.GetValue().ToString() + Set_Fill3(this.TXT01_PRN5031.GetValue().ToString());
                }

                sPRN6010 = "";
                // 고정자산번호
                if (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "")
                {
                    sPRN6010 = this.TXT01_PRN6010.GetValue().ToString() + Set_Fill4(this.TXT01_PRN6011.GetValue().ToString()) + Set_Fill3(this.TXT01_PRN6012.GetValue().ToString());
                }

                sOUTMSG = "OK";

                // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_PRM5100_1.GetValue().ToString() != "Y" && this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                {
                    // 예산 가용금액 - 마이너스
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG5E427",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        sPRN1050.ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PRN1092.GetValue().ToString()),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 내역사항 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG21411",
                        this.CBH01_PRN1100.GetValue().ToString(),
                        this.CBH01_PRN1110.GetValue().ToString(),
                        this.CBO01_PRN1120.GetValue().ToString(),
                        this.CBH01_PRN1130.GetValue().ToString(),
                        sPRN1140.ToString(),
                        //this.CBH01_PRN1130.GetText().ToString(),
                        this.TXT01_PRN1150.GetValue().ToString(),
                        this.TXT01_PRN1160.GetValue().ToString(),
                        this.TXT01_PRN1170.GetValue().ToString(),
                        this.TXT01_PRN1180.GetValue().ToString(),
                        this.TXT01_PRN2010.GetValue().ToString(),
                        this.TXT01_PRN2020.GetValue().ToString(),
                        this.TXT01_PRN2030.GetValue().ToString(),
                        this.TXT01_PRN2040.GetValue().ToString(),
                        this.TXT01_PRN2060.GetValue().ToString(),
                        this.TXT01_PRN2070.GetValue().ToString(),
                        this.TXT01_PRN2080.GetValue().ToString(),
                        this.TXT01_PRN2090.GetValue().ToString(),
                        this.TXT01_PRN3010.GetValue().ToString(),
                        this.TXT01_PRN3020.GetValue().ToString(),
                        this.TXT01_PRN3060.GetValue().ToString(),
                        this.TXT01_PRN3070.GetValue().ToString(),
                        this.CBO01_PRN5000.GetValue().ToString(),
                        this.CBO01_PRN5010.GetValue().ToString(),
                        sPRN5030.ToString(),
                        sPRN6010.ToString(),
                        this.CBH01_PRN6020.GetValue().ToString(),
                        this.CBO01_PRN6030.GetValue().ToString(),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        sPRN1050.ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PRN1092.GetValue().ToString())
                        );

                    // 귀속별 예산파일 수정
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG2A415",
                        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                        this.TXT01_PRO2040.GetValue().ToString().Substring(4, 2),
                        TYUserInfo.EmpNo.ToString(),
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString()
                        );
                    this.DbConnector.ExecuteNonQueryList();

                    sOUTMSG = "OK";

                    // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                    // 선급자재일 경우 예산 업데이트 안함.
                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    if (this.TXT01_PRM5100_1.GetValue().ToString() != "Y" && this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                    {
                        // 예산 가용금액 - 플러스
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BG51426",
                            this.TXT01_PRN1000.GetValue().ToString(),
                            this.TXT01_PRN1010.GetValue().ToString(),
                            this.TXT01_PRN1020.GetValue().ToString(),
                            this.TXT01_PRN1030.GetValue().ToString(),
                            this.CBH01_PRN1040.GetValue().ToString(),
                            sPRN1050.ToString(),
                            this.CBH01_PRN1060.GetValue().ToString(),
                            this.TXT01_PRN1070.GetValue().ToString(),
                            this.TXT01_PRN1080.GetValue().ToString(),
                            this.TXT01_PRN1090.GetValue().ToString(),
                            Get_Numeric(this.TXT01_PRN1092.GetValue().ToString()),
                            sOUTMSG.ToString()
                            );

                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                    }

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // 신규 버튼 클릭시 이전값 가지고 있을 변수
                        UP_GET_MRPPRNF_REMEMBER("UPT");

                        // 마스터 테이블에 요청금액 업데이트
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BI1Q431",
                            this.TXT01_PRN1000.GetValue().ToString(),
                            this.TXT01_PRN1010.GetValue().ToString(),
                            this.TXT01_PRN1020.GetValue().ToString(),
                            this.TXT01_PRN1030.GetValue().ToString()
                            );

                        this.DbConnector.ExecuteNonQuery();

                        this.ShowMessage("TY_M_MR_2BD3Z286");

                        // 버튼 초기화
                        UP_ImgbtnDisplay("3", false);

                        // 컨트롤 초기화
                        UP_Control_Initialize("MRPPRNF", true);

                        this.CBH01_PRN1040.SetValue("");
                        this.CBH01_PRN1060.SetValue("");

                        this.TXT01_PRN1070.SetValue("");
                        this.TXT01_PRN1070NM.SetValue("");
                        this.TXT01_PRN1080.SetValue("");
                        this.TXT01_PRN1080NM.SetValue("");
                        this.TXT01_PRN1090.SetValue("");
                        this.TXT01_PRN1090NM.SetValue("");
                        this.TXT01_PRN1092.SetValue("");
                        // 장기계약일 경우 체크
                        if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                        {
                            this.CBH01_PRN1051.SetValue("");
                            this.CBH01_PRN1051.SetText("");
                        }
                        else
                        {
                            this.CBH01_PRN1050.SetValue("");
                            this.CBH01_PRN1050.SetText("");
                        }

                        UP_FieldClear("MRPPRNF");

                        UP_MRPPRNF_DISPLAY();

                        SetFocus(this.CBH01_PRN1040.CodeText);
                    }
                }
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            if (fsGUBUN == "MRPPRMF") // 마스터
            {
                // 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BE3D308",
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                this.DbConnector.ExecuteNonQueryList();
                this.ShowMessage("TY_M_GB_23NAD874");

                this.TXT01_PRM1000.SetReadOnly(false);
                this.TXT01_PRM1010.SetReadOnly(true);
                this.TXT01_PRM1020.SetReadOnly(false);
                this.TXT01_PRM1030.SetReadOnly(true);

                this.TXT01_PRM1020.SetValue("");
                this.TXT01_PRM1030.SetValue("");

                // 탭 컨트롤
                tabControl1_Enable("");

                // 버튼 컨트롤
                UP_ImgbtnDisplay("2", false);

                UP_FieldClear("MRPPRMF");

                this.TXT01_PRM5120.SetReadOnly(false);
                this.TXT01_PRM5130.SetReadOnly(false);
                this.BTN61_OPM10401.Enabled = true;

                SetFocus(this.TXT01_PRM1000);
            }
            else if (fsGUBUN == "MRPPRNF") // 내역사항
            {
                sOUTMSG = "OK";
                string sPRN1050 = string.Empty;

                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    sPRN1050 = this.CBH01_PRN1051.GetValue().ToString();
                }
                else
                {
                    sPRN1050 = this.CBH01_PRN1050.GetValue().ToString();
                }

                // 장기계약을 하기 위한 요청일 경우 예산 업데이트 안함.
                if (this.TXT01_PRM5100_1.GetValue().ToString() != "Y")
                {
                    // 예산 가용금액 - 마이너스
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG5E427",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        sPRN1050.ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PRN1092.GetValue().ToString()),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 내역사항 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BG22412",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString(),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        sPRN1050.ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PRN1092.GetValue().ToString())
                        );

                    // 귀속별 예산파일 삭제
                    if (fsYESAN_COUNT.ToString() == "1") // 삭제
                    {
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BG25413",
                            this.TXT01_PRN1000.GetValue().ToString(),
                            this.TXT01_PRN1010.GetValue().ToString(),
                            this.TXT01_PRN1020.GetValue().ToString(),
                            this.TXT01_PRN1030.GetValue().ToString(),
                            this.CBH01_PRN1040.GetValue().ToString(),
                            this.CBH01_PRN1060.GetValue().ToString(),
                            this.TXT01_PRN1070.GetValue().ToString(),
                            this.TXT01_PRN1080.GetValue().ToString(),
                            this.TXT01_PRN1090.GetValue().ToString()
                            );
                    }
                    if (int.Parse(fsYESAN_COUNT.ToString()) > 1) // 수정
                    {
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BG2A415",
                            this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                            this.TXT01_PRO2040.GetValue().ToString().Substring(4, 2),
                            TYUserInfo.EmpNo.ToString(),
                            this.TXT01_PRN1000.GetValue().ToString(),
                            this.TXT01_PRN1010.GetValue().ToString(),
                            this.TXT01_PRN1020.GetValue().ToString(),
                            this.TXT01_PRN1030.GetValue().ToString(),
                            this.CBH01_PRN1040.GetValue().ToString(),
                            this.CBH01_PRN1060.GetValue().ToString(),
                            this.TXT01_PRN1070.GetValue().ToString(),
                            this.TXT01_PRN1080.GetValue().ToString(),
                            this.TXT01_PRN1090.GetValue().ToString()
                            );
                    }

                    this.DbConnector.ExecuteNonQueryList();

                    // 신규 버튼 클릭시 이전값 가지고 있을 변수
                    UP_GET_MRPPRNF_REMEMBER("DEL");

                    // 마스터 테이블에 요청금액 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BI1Q431",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString()
                        );

                    this.DbConnector.ExecuteNonQuery();

                    this.ShowMessage("TY_M_GB_23NAD874");

                    // 버튼 초기화
                    UP_ImgbtnDisplay("3", false);

                    // 컨트롤 초기화
                    UP_Control_Initialize("MRPPRNF", false);

                    this.CBH01_PRN1040.SetValue("");
                    this.CBH01_PRN1060.SetValue("");

                    this.TXT01_PRN1070.SetValue("");
                    this.TXT01_PRN1070NM.SetValue("");
                    this.TXT01_PRN1080.SetValue("");
                    this.TXT01_PRN1080NM.SetValue("");
                    this.TXT01_PRN1090.SetValue("");
                    this.TXT01_PRN1090NM.SetValue("");
                    this.TXT01_PRN1092.SetValue("");
                    // 장기계약일 경우 체크
                    if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                    {
                        this.CBH01_PRN1051.SetValue("");
                        this.CBH01_PRN1051.SetText("");
                    }
                    else
                    {
                        this.CBH01_PRN1050.SetValue("");
                        this.CBH01_PRN1050.SetText("");
                    }

                    UP_FieldClear("MRPPRNF");

                    UP_MRPPRNF_DISPLAY();

                    SetFocus(this.CBH01_PRN1040.CodeText);
                }
            }
        }
        #endregion

        #region Description : 특기사항 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 등록
            this.DataTableColumnAdd(ds.Tables[0], "PRT1000", this.TXT01_PRT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "PRT1010", this.TXT01_PRT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "PRT1020", this.TXT01_PRT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "PRT1030", this.TXT01_PRT1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "PRTHISAB", TYUserInfo.EmpNo);

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 수정
            this.DataTableColumnAdd(ds.Tables[1], "PRT1000", this.TXT01_PRT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "PRT1010", this.TXT01_PRT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "PRT1020", this.TXT01_PRT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "PRT1030", this.TXT01_PRT1030.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[1], "PRTHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2BE6J328", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_MR_2BE6K329", ds.Tables[1]); //수정

            this.DbConnector.ExecuteNonQueryList();

            UP_MRPPRTF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장완료 메세지
        }
        #endregion

        #region Description : 특기사항 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가 - 삭제
            this.DataTableColumnAdd(ds.Tables[0], "PRT1000", this.TXT01_PRT1000.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "PRT1010", this.TXT01_PRT1010.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "PRT1020", this.TXT01_PRT1020.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "PRT1030", this.TXT01_PRT1030.GetValue().ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2BE6M330", ds.Tables[0]); //삭제

            this.DbConnector.ExecuteNonQueryList();

            UP_MRPPRTF_DISPLAY();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제완료 메세지
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsPRM1000 = this.TXT01_PRM1000.GetValue().ToString();
            fsPRM1010 = this.TXT01_PRM1010.GetValue().ToString();
            fsPRM1020 = this.TXT01_PRM1020.GetValue().ToString();
            fsPRM1030 = this.TXT01_PRM1030.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 마스터 관련

        #region Description : 마스터 DISPLAY
        private void UP_MRPPRMF_DISPLAY()
        {
            UP_FieldClear("MRPPRMF");

            fsPRM5110 = "";

            DataTable dt = new DataTable();

            #region Description : 구매요청 마스터 내용 DISPLAY

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BD2L282",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 버튼 컨트롤
                UP_ImgbtnDisplay("1", true);

                // 탭 컨트롤
                tabControl1_Enable("MRPPRMF");

                this.CurrentDataTableRowMapping(dt, "01");

                // 예산 DISPLAY
                UP_MRPPROF_DISPLAY();
            }

            #endregion

            
        }
        #endregion

        #region Description : 예산 DISPLAY
        private void UP_MRPPROF_DISPLAY()
        {
            DataTable dt = new DataTable();

            #region Description : 구매요청 마스터(예산조회-귀속부서별)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BCAF252",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM1030.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2BCBY255.SetValue(dt);

            #endregion

            #region Description : 구매요청 마스터(예산조회-거래처별)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BD4K287",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM1030.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2BD4M288.SetValue(UP_MAST_PROF_SumRowds(dt));

                for (int i = 0; i < this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_MR_2BD4M288.GetValue(i, "VNSANGHO").ToString() == "거래처별 소계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                    else if (this.FPS91_TY_S_MR_2BD4M288.GetValue(i, "VNSANGHO").ToString() == "총   계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].ForeColor = Color.Red;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BD4M288.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2BD4M288.SetValue(dt);
            }

            #endregion
        }
        #endregion

        #region Description : 구매요청 마스터(예산조회-거래처별 합계)
        private DataTable UP_MAST_PROF_SumRowds(DataTable dt)
        {
            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt;

            string sMEKEY = "";

            double dPRN1170 = 0; // 금액

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;


            for (i = 1; i < nNum; i++)
            {
                /* Row i 번째와 Row i+1 번째의 거래처가 다를경우 빈로우를 생성하고
                 * 거래처별 금액 소계를 낸다.                                     */
                if (table.Rows[i - 1]["PRN1100"].ToString() != table.Rows[i]["PRN1100"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합계 이름 넣기
                    table.Rows[i]["VNSANGHO"] = "거래처별 소계";

                    // 거래처별
                    sMEKEY = " PRN1100 = '" + table.Rows[i - 1]["PRN1100"].ToString() + "' " ;

                    // 금액
                    table.Rows[i]["PRN1170"] = table.Compute("SUM(PRN1170)", sMEKEY).ToString();
                    // 금액합계
                    dPRN1170 += Convert.ToDouble(table.Compute("SUM(PRN1170)", sMEKEY).ToString());

                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 합계 이름 넣기
            table.Rows[i]["VNSANGHO"] = "거래처별 소계";

            // 거래처별
            sMEKEY = " PRN1100 = '" + table.Rows[i - 1]["PRN1100"].ToString() + "' " ;

            // 금액
            table.Rows[i]["PRN1170"] = table.Compute("SUM(PRN1170)", sMEKEY).ToString();
            // 금액합계
            dPRN1170 += Convert.ToDouble(table.Compute("SUM(PRN1170)", sMEKEY).ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);

            // 총계 이름 넣기
            table.Rows[i + 1]["VNSANGHO"] = "총   계";            
            table.Rows[i + 1]["PRN1170"]  = Convert.ToString(dPRN1170);

            return table;
        }
        #endregion

        #region Description : 그룹웨어 문서 바로가기
        private void BTN61_GW_Click(object sender, EventArgs e)
        {
            if (this.TXT01_PRM4050.GetValue().ToString() != "")
            {
                if ((new TYMRPR005S(this.TXT01_PRM4050.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

        #region Description : 신청 사번
        private void CBH01_PRM2040_TextChanged(object sender, EventArgs e)
        {
            if (CBH01_PRM2040.GetValue().ToString().Length >= 6)
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BEBB293",
                    DateTime.Now.ToString("yyyyMMdd"),
                    this.CBH01_PRM2040.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 부서코드
                    this.TXT01_PRM2010.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
                    // 부서명
                    this.TXT01_DTDESC.SetValue(dt.Rows[0]["KBBUSEONM"].ToString());
                }
            }
        }
        #endregion

        #region Description : 계약년도 이벤트
        private void TXT01_PRM5120_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRM5120.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_OPM10401_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 계약순번 이벤트
        private void TXT01_PRM5130_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRM5130.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_OPM10401_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 계약 버튼
        private void BTN61_OPM10401_Click(object sender, EventArgs e)
        {
            // 장기계약 코드헬프
            TYMRGB001S popup = new TYMRGB001S(this.TXT01_PRM1020.GetValue().ToString().Substring(0,4));

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PRM5120.SetValue(popup.fsOPM1000); // 계약년도
                this.TXT01_PRM5130.SetValue(popup.fsOPM1010); // 계약순번
                this.TXT01_OPM1040.SetValue(popup.fsOPM1040); // 계약내용
            }
        }
        #endregion

        #region Description : 요청일자 이벤트
        private void DTP01_PRM2020_ValueChanged(object sender, EventArgs e)
        {
            // 마스터 검토부서
            this.CBH01_PRM2090.DummyValue = this.DTP01_PRM2020.GetString();

            // 내역 귀속부서
            this.CBH01_PRN1040.DummyValue = this.DTP01_PRM2020.GetString();
        }
        #endregion

        #endregion

        #region Description : 내역사항 관련

        #region Description : 내역사항 확인
        private void UP_MRPPRNF_RUN()
        {
            UP_FieldClear("MRPPRNF");

            string sPRN1050 = string.Empty;

            // 장기계약일 경우 체크
            if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
            {
                sPRN1050 = this.CBH01_PRN1051.GetValue().ToString();
            }
            else
            {
                sPRN1050 = this.CBH01_PRN1050.GetValue().ToString();
            }

            DataTable dt = new DataTable();

            #region Description : 구매요청 내역 내용 DISPLAY

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BF8N367",
                this.TXT01_PRN1000.GetValue(),
                this.TXT01_PRN1010.GetValue(),
                this.TXT01_PRN1020.GetValue(),
                Set_Fill4(this.TXT01_PRN1030.GetValue().ToString()),
                this.CBH01_PRN1040.GetValue(),
                sPRN1050.ToString(),
                this.CBH01_PRN1060.GetValue(),
                this.TXT01_PRN1070.GetValue(),
                this.TXT01_PRN1080.GetValue(),
                Get_Numeric(this.TXT01_PRN1090.GetValue().ToString()),
                Get_Numeric(this.TXT01_PRN1092.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "MRPPRNF";

                // 버튼 컨트롤
                UP_ImgbtnDisplay("1", true);
            }

            #endregion
        }
        #endregion

        #region Description : 내역사항 DISPLAY
        private void UP_MRPPRNF_DISPLAY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BD2D280",
                this.TXT01_PRN1000.GetValue(),
                this.TXT01_PRN1010.GetValue(),
                this.TXT01_PRN1020.GetValue(),
                Set_Fill4(this.TXT01_PRN1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DataTable retDt = new DataTable();

                retDt = UP_MRPPRNF_SumRowds(dt);

                this.FPS91_TY_S_MR_2BFBJ342.SetValue(retDt);

                for (int i = 0; i < retDt.Rows.Count; i++)
                {
                    if (this.FPS91_TY_S_MR_2BFBJ342.GetValue(i, "YSDESC").ToString() == "예산별 소계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                    else if (this.FPS91_TY_S_MR_2BFBJ342.GetValue(i, "YSDESC").ToString() == "예산별 합계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 ROW 글자색깔 입히기
                        this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].ForeColor = Color.Red;

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_MR_2BFBJ342.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2BFBJ342.SetValue(dt);
            }
        }
        #endregion

        #region Description : 구매요청 내역사항(예산별 합계)
        private DataTable UP_MRPPRNF_SumRowds(DataTable dt)
        {
            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt;

            string sMEKEY = "";

            double dPRN1150 = 0; // 수량
            double dPRN1170 = 0; // 금액

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;


            for (i = 1; i < nNum; i++)
            {
                /* Row i 번째와 Row i+1 번째의 귀속부서, 예산, 계정 비품, 순번이 다를경우 빈로우를 생성하고
                 * 예산별 수량, 금액 소계를 낸다.                                                          */
                if (table.Rows[i - 1]["PRN1040"].ToString() != table.Rows[i]["PRN1040"].ToString() ||
                     table.Rows[i - 1]["PRN1060"].ToString() != table.Rows[i]["PRN1060"].ToString() ||
                     table.Rows[i - 1]["PRN1070"].ToString() != table.Rows[i]["PRN1070"].ToString() ||
                     table.Rows[i - 1]["PRN1080"].ToString() != table.Rows[i]["PRN1080"].ToString() ||
                     table.Rows[i - 1]["PRN1090"].ToString() != table.Rows[i]["PRN1090"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);
                    // 합 계 이름 넣기
                    table.Rows[i]["YSDESC"] = "예산별 소계";

                    // 예산별
                    sMEKEY = " PRN1040 = '" + table.Rows[i - 1]["PRN1040"].ToString() + "'      ";
                    sMEKEY += " AND PRN1060 = '" + table.Rows[i - 1]["PRN1060"].ToString() + "' ";
                    sMEKEY += " AND PRN1070 = '" + table.Rows[i - 1]["PRN1070"].ToString() + "' ";
                    sMEKEY += " AND PRN1080 = '" + table.Rows[i - 1]["PRN1080"].ToString() + "' ";
                    sMEKEY += " AND PRN1090 =  " + table.Rows[i - 1]["PRN1090"].ToString() + "  ";

                    // 수량
                    table.Rows[i]["PRN1150"] = table.Compute("SUM(PRN1150)", sMEKEY).ToString();
                    // 금액
                    table.Rows[i]["PRN1170"] = table.Compute("SUM(PRN1170)", sMEKEY).ToString();

                    // 수량합계
                    dPRN1150 += Convert.ToDouble(table.Compute("SUM(PRN1150)", sMEKEY).ToString());
                    // 금액합계
                    dPRN1170 += Convert.ToDouble(table.Compute("SUM(PRN1170)", sMEKEY).ToString());

                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            table.Rows[i]["YSDESC"] = "예산별 소계";

            // 예산별
            sMEKEY = " PRN1040 = '" + table.Rows[i - 1]["PRN1040"].ToString() + "'      ";
            sMEKEY += " AND PRN1060 = '" + table.Rows[i - 1]["PRN1060"].ToString() + "' ";
            sMEKEY += " AND PRN1070 = '" + table.Rows[i - 1]["PRN1070"].ToString() + "' ";
            sMEKEY += " AND PRN1080 = '" + table.Rows[i - 1]["PRN1080"].ToString() + "' ";
            sMEKEY += " AND PRN1090 =  " + table.Rows[i - 1]["PRN1090"].ToString() + "  ";

            // 수량
            table.Rows[i]["PRN1150"] = table.Compute("SUM(PRN1150)", sMEKEY).ToString();
            // 금액
            table.Rows[i]["PRN1170"] = table.Compute("SUM(PRN1170)", sMEKEY).ToString();

            // 수량합계
            dPRN1150 += Convert.ToDouble(table.Compute("SUM(PRN1150)", sMEKEY).ToString());
            // 금액합계
            dPRN1170 += Convert.ToDouble(table.Compute("SUM(PRN1170)", sMEKEY).ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);
            // 합 계 이름 넣기
            table.Rows[i + 1]["YSDESC"] = "예산별 합계";

            table.Rows[i + 1]["PRN1150"] = Convert.ToString(dPRN1150);
            table.Rows[i + 1]["PRN1170"] = Convert.ToString(dPRN1170);

            return table;
        }
        #endregion

        //#region Description : 품목 코드
        //private void TXT01_PRN1050_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PRN10501_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 품목 코드 버튼
        //private void BTN61_PRN10501_Click(object sender, EventArgs e)
        //{
        //    if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && this.TXT01_PRM5130_1.GetValue().ToString() == "")
        //    {
        //        // 품목코드 코드헬프
        //        TYMRGB003S popup = new TYMRGB003S();

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            this.TXT01_PRN1050.SetValue(popup.fsJEPUM);
        //            this.TXT01_PRN1050NM.SetValue(popup.fsZ105013);

        //            SetFocus(this.CBH01_PRN1100.CodeText);
        //        }
        //    }
        //    else
        //    {
        //        // 장기계약 품목코드 코드헬프
        //        TYMRGB012S popup = new TYMRGB012S(this.TXT01_PRM5120_1.GetValue().ToString(), this.TXT01_PRM5130_1.GetValue().ToString());

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            this.CBH01_PRN1040.SetValue(popup.fsOPN1030);
        //            this.CBH01_PRN1040.SetText(popup.fsOPN1030NM);

        //            this.CBH01_PRN1060.SetValue(popup.fsOPN1040);
        //            this.CBH01_PRN1060.SetText(popup.fsOPN1040NM);

        //            this.TXT01_PRN1070.SetValue(popup.fsOPN1050);
        //            this.TXT01_PRN1070NM.SetValue(popup.fsOPN1050NM);

        //            this.TXT01_PRN1080.SetValue(popup.fsOPN1060);
        //            this.TXT01_PRN1080NM.SetValue(popup.fsOPN1060NM);

        //            this.TXT01_PRN1090.SetValue(popup.fsOPN1070);

        //            this.TXT01_PRN1050.SetValue(popup.fsOPN1080);
        //            this.TXT01_PRN1050NM.SetValue(popup.fsOPN1080NM);

        //            this.TXT01_PRN1160.SetValue(popup.fsPRN1160);

        //            this.CBH01_PRN1100.SetValue(popup.fsPRN1100);
        //            this.CBH01_PRN1100.SetText(popup.fsPRN1100NM);

        //            this.CBH01_PRN1110.SetValue(popup.fsPRN1110);
        //            this.CBH01_PRN1110.SetText(popup.fsPRN1110NM);

        //            SetFocus(this.CBO01_PRN1120);
        //        }
        //    }
        //}
        //#endregion

        #region Description : 적용계정
        private void TXT01_PRN1070_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRN1070.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PRN10701_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 적용계정 버튼
        private void BTN61_PRN10701_Click(object sender, EventArgs e)
        {
            if (this.CBH01_PRN1060.GetValue().ToString() == "1") // 기타세목예산(투자&수선) 코드헬프
            {
                TYMRGB005S popup = new TYMRGB005S(this.TXT01_PRN1020.GetValue().ToString().Substring(0, 4), this.CBH01_PRN1040.GetValue().ToString(), this.CBH01_PRN1040.GetText().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 요청년월의 본예산, 집행예산 금액 가져옴
                    this.TXT01_PRN1070.SetValue(popup.fsCDAC);      // 계정과목
                    this.TXT01_PRN1070NM.SetValue(popup.fsCDACNM);  // 계정과목명
                    this.TXT01_PRN1090.SetValue(popup.fsSEQ);       // 순번
                    this.TXT01_PRN1090NM.SetValue(popup.fsYESANNM); // 예산명

                    // 장기계약일 경우 체크
                    if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                    {
                        SetFocus(this.CBH01_PRN1051.CodeText);
                    }
                    else
                    {
                        SetFocus(this.CBH01_PRN1050.CodeText);
                    }
                }
            }
            else if (this.CBH01_PRN1060.GetValue().ToString() == "2") // 소모품비 예산세목 코드헬프
            {
                TYMRGB006S popup = new TYMRGB006S(this.TXT01_PRN1020.GetValue().ToString().Substring(0, 4), this.CBH01_PRN1040.GetValue().ToString(), this.CBH01_PRN1040.GetText().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 요청년월의 본예산, 집행예산 금액 가져옴
                    this.TXT01_PRN1070.SetValue(popup.fsCDAC);     // 계정과목
                    this.TXT01_PRN1070NM.SetValue(popup.fsCDACNM); // 계정과목명
                    this.TXT01_PRN1080.SetValue(popup.fsCDJJ);     // 비품코드
                    this.TXT01_PRN1080NM.SetValue(popup.fsBPDESC); // 비품명
                    this.TXT01_PRN1090.SetValue(popup.fsSEQ);      // 순번

                    // 장기계약일 경우 체크
                    if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                    {
                        SetFocus(this.CBH01_PRN1051.CodeText);
                    }
                    else
                    {
                        SetFocus(this.CBH01_PRN1050.CodeText);
                    }
                }
            }
            else if (this.CBH01_PRN1060.GetValue().ToString() == "3") // 기타예산 코드헬프
            {
                TYMRGB007S popup = new TYMRGB007S(this.TXT01_PRN1020.GetValue().ToString().Substring(0, 4), this.CBH01_PRN1040.GetValue().ToString(), this.CBH01_PRN1040.GetText().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 요청년월의 본예산, 집행예산 금액 가져옴
                    this.TXT01_PRN1070.SetValue(popup.fsCDAC);     // 계정과목
                    this.TXT01_PRN1070NM.SetValue(popup.fsCDACNM); // 계정과목명

                    // 장기계약일 경우 체크
                    if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                    {
                        SetFocus(this.CBH01_PRN1051.CodeText);
                    }
                    else
                    {
                        SetFocus(this.CBH01_PRN1050.CodeText);
                    }
                }
            }
        }
        #endregion

        #region Description : 비품년월
        private void TXT01_PRN5030_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRN5030.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PRN50301_Click(null, null);
                }
                else
                {
                    this.CBH01_PRN6020.SetReadOnly(false);
                }
            }
        }
        #endregion

        #region Description : 비품순번
        private void TXT01_PRN5031_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRN5031.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PRN50301_Click(null, null);
                }
                else
                {
                    this.CBH01_PRN6020.SetReadOnly(false);
                }
            }
        }
        #endregion

        #region Description : 비품버튼
        private void BTN61_PRN50301_Click(object sender, EventArgs e)
        {
            TYMRGB009S popup = new TYMRGB009S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PRN5030.SetValue(popup.fsMAYYMM);     // 비품년월
                this.TXT01_PRN5031.SetValue(popup.fsMASEQ);      // 비품순번
                this.TXT01_PRN5030NM.SetValue(popup.fsMABPDESC); // 비품명

                this.CBH01_PRN6020.SetValue(popup.fsJASAN + popup.fsSAUPBU + popup.fsLARGE + popup.fsMIDDLE + popup.fsSMALL); // 고정자산분류코드
                this.CBH01_PRN6020.SetText(popup.fsJASANNM);     // 자산분류명

                //this.TXT01_PRN6010.SetValue("");
                //this.TXT01_PRN6011.SetValue("");
                //this.TXT01_PRN6012.SetValue("");

                //this.TXT01_PRN6010.SetReadOnly(true);
                //this.TXT01_PRN6011.SetReadOnly(true);
                //this.TXT01_PRN6012.SetReadOnly(true);
                //this.BTN61_PRN60101.SetReadOnly(true);

                this.CBH01_PRN6020.SetReadOnly(true);

                this.SetFocus(this.TXT01_PRN6010);
            }
            else
            {
                //this.TXT01_PRN6010.SetValue("");
                //this.TXT01_PRN6011.SetValue("");
                //this.TXT01_PRN6012.SetValue("");

                //this.TXT01_PRN6010.SetReadOnly(false);
                //this.TXT01_PRN6011.SetReadOnly(false);
                //this.TXT01_PRN6012.SetReadOnly(false);
                //this.BTN61_PRN60101.SetReadOnly(false);
                

                this.CBH01_PRN6020.SetReadOnly(false);

                this.SetFocus(TXT01_PRN6010);
            }
        }
        #endregion

        //#region Description : 자산분류코드 - 자산구분
        //private void TXT01_PRN6020_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PRN60201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 - 대분류
        //private void TXT01_PRN6021_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PRN60201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 - 중분류
        //private void TXT01_PRN6022_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PRN60201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 - 소분류
        //private void TXT01_PRN6023_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        BTN61_PRN60201_Click(null, null);
        //    }
        //}
        //#endregion

        //#region Description : 자산분류코드 버튼
        //private void BTN61_PRN60201_Click(object sender, EventArgs e)
        //{
        //    TYMRGB011S popup = new TYMRGB011S();

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.TXT01_PRN6020.SetValue(popup.fsFXSLASCODE); // 자산구분
        //        this.TXT01_PRN6021.SetValue(popup.fsFXSLMCODE);  // 대분류
        //        this.TXT01_PRN6022.SetValue(popup.fsFXSMMCODE);  // 중분류
        //        this.TXT01_PRN6023.SetValue(popup.fsFXSMCODE);   // 소분류
        //        this.TXT01_PRN60201NM.SetValue(popup.fsFXSMDESC); // 자산분류명
        //    }
        //}
        //#endregion

        #region Description : 자산년도
        private void TXT01_PRN6010_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRN6010.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PRN60101_Click(null, null);
                }
                else
                {
                    if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                    {
                        this.CBH01_PRN6020.SetReadOnly(true);
                    }
                    else
                    {
                        this.CBH01_PRN6020.SetReadOnly(false);
                    }
                }
            }
        }
        #endregion

        #region Description : 자산순번
        private void TXT01_PRN6011_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRN6011.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PRN60101_Click(null, null);
                }
                else
                {
                    if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                    {
                        this.CBH01_PRN6020.SetReadOnly(true);
                    }
                    else
                    {
                        this.CBH01_PRN6020.SetReadOnly(false);
                    }
                }
            }
        }
        #endregion

        #region Description : 가족코드
        private void TXT01_PRN6012_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.TXT01_PRN6012.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_PRN60101_Click(null, null);
                }
                else
                {
                    if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                    {
                        this.CBH01_PRN6020.SetReadOnly(true);
                    }
                    else
                    {
                        this.CBH01_PRN6020.SetReadOnly(false);
                    }
                }
            }
        }
        #endregion

        #region Description : 고정자산번호 버튼
        private void BTN61_PRN60101_Click(object sender, EventArgs e)
        {
            TYMRGB010S popup = new TYMRGB010S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PRN6010.SetValue(popup.fsFXSYEAR);   // 자산년도
                this.TXT01_PRN6011.SetValue(popup.fsFXSSEQ);    // 자산순번
                this.TXT01_PRN6012.SetValue(popup.fsFXSSUBNUM); // 가족코드
                this.TXT01_PRN6010NM.SetValue(popup.fsFXSNAME); // 자산명

                this.CBH01_PRN6020.SetValue(popup.fsJASAN + popup.fsSAUPBU + popup.fsLARGE + popup.fsMIDDLE + popup.fsSMALL); // 고정자산분류코드
                this.CBH01_PRN6020.SetText(popup.fsJASANNM);    // 자산분류명

                this.CBH01_PRN6020.SetReadOnly(true);

                this.SetFocus(CBO01_PRN6030);
            }
            else
            {
                if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                {
                    this.CBH01_PRN6020.SetReadOnly(true);

                    this.SetFocus(CBO01_PRN6030);
                }
                else
                {
                    this.CBH01_PRN6020.SetReadOnly(false);

                    this.SetFocus(CBH01_PRN6020.CodeText);
                }
            }
        }
        #endregion

        #region Description : 내역사항 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2BFBJ342_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            bool fResult;

            if (this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1040").ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");
            }
            else
            {
                this.CBH01_PRN1040.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1040").ToString());
                this.CBH01_PRN1060.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1060").ToString());
                this.TXT01_PRN1070.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1070").ToString());
                this.TXT01_PRN1070NM.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("A1NMAC").ToString());
                this.TXT01_PRN1080.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1080").ToString());
                this.TXT01_PRN1080NM.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("BPDESC").ToString());
                this.TXT01_PRN1090.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1090").ToString());
                this.TXT01_PRN1090NM.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1090NM").ToString());
                this.TXT01_PRN1092.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1092").ToString());
                if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
                {
                    // 일반 구매요청 품목코드
                    this.CBH01_PRN1050.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1050").ToString());
                    this.CBH01_PRN1050.SetText(this.FPS91_TY_S_MR_2BFBJ342.GetValue("Z105013").ToString());
                }
                else
                {
                    // 장기계약 구매요청 품목코드
                    this.CBH01_PRN1051.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("PRN1050").ToString());
                    this.CBH01_PRN1051.SetText(this.FPS91_TY_S_MR_2BFBJ342.GetValue("Z105013").ToString());
                }

                // 사진리스트
                this.TXT01_Z106000.SetValue(this.FPS91_TY_S_MR_2BFBJ342.GetValue("Z106000").ToString());

                e.Cancel = true;

                // 컨트롤 초기화
                UP_Control_Initialize("MRPPRNF", true);
                
                UP_MRPPRNF_RUN();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 마스터 데이터가 존재하므로 
                    // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                    UP_ImgbtnDisplay("1", true);

                    this.SetFocus(this.CBH01_PRN1100.CodeText);
                }
                else
                {
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("3", false);
                }
            }
        }
        #endregion

        #endregion

        #region Description : 특기사항 관련

        #region Description : 특기사항 DISPLAY
        private void UP_MRPPRTF_DISPLAY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BE4W310",
                this.TXT01_PRT1000.GetValue(),
                this.TXT01_PRT1010.GetValue(),
                this.TXT01_PRT1020.GetValue(),
                Set_Fill4(this.TXT01_PRT1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2BC30258.SetValue(dt);
        }
        #endregion

        #endregion

        #region Description : 구매요청 공통

        #region Description : FieldClear()
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN.ToString() == "MRPPRMF")
            {
                //// 신청사번 <- 등록 및 수정 체크에 넣음
                //this.CBH01_PRM2040.SetValue(TYUserInfo.EmpNo);

                //// 등록 시 요청부서의 앞자리 가져옴
                //this.TXT01_PRM1000.SetValue(this.TXT01_PRM2010.GetValue().ToString().Substring(0, 1));

                this.CBO01_PRM2070.SetValue("1");
                this.CBO01_PRM2080.SetValue("N");
                this.CBH01_PRM2090.SetValue("");
                this.CBO01_PRM2060.SetValue("N");
                this.TXT01_PRM2100.SetValue("");
                this.TXT01_PRM2110.SetValue("");
                this.DTP01_PRM2020.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.DTP01_PRM2050.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.CBH01_PRM3000.SetValue("1");
                this.CBH01_PRM3020.SetValue("01");
                this.TXT01_PRM3010.SetValue("");
                this.TXT01_PRM5120.SetValue("");
                this.TXT01_PRM5130.SetValue("");
                this.CBO01_PRM5100.SetValue("N");
                this.TXT01_PRM2120.SetValue("");
                this.TXT01_PRM4010.SetValue("");
                this.TXT01_PRM4050.SetValue("");
                this.TXT01_PRM4000.SetValue("");
                this.TXT01_PRM4020.SetValue("");
                this.TXT01_PRM4030.SetValue("");
                this.TXT01_KBHANGL1.SetValue("");
                this.TXT01_PRM4040.SetValue("");

                this.CBO01_PRM6010.SetValue("N");
                this.CBO01_PRM6020.SetValue("3");
                this.CBH01_PRM6030.SetValue("");

                this.TXT01_PRM7010.SetValue("");
                this.TXT01_PRM7020.SetValue("");
                this.TXT01_PRM7030.SetValue("");
                this.TXT01_PRM7060.SetValue("");
                this.TXT01_PRM7070.SetValue("");
                this.TXT01_PRM7080.SetValue("");
            }
            else if (sGUBUN.ToString() == "MRPPRNF")
            {
                this.CBH01_PRN1100.SetValue("");

                this.CBH01_PRN1110.SetValue("");
                this.CBO01_PRN1120.SetValue("1");
                this.CBO01_PRN5000.SetValue("N");
                this.CBH01_PRN1130.SetValue("1");
                this.CBO01_PRN5010.SetValue("N");
                this.TXT01_PRN5030.SetValue("");
                this.TXT01_PRN5031.SetValue("");
                this.TXT01_PRN5030NM.SetValue("");
                this.TXT01_PRN6010.SetValue("");
                this.TXT01_PRN6011.SetValue("");
                this.TXT01_PRN6012.SetValue("");
                this.TXT01_PRN6010NM.SetValue("");
                this.CBH01_PRN6020.SetValue("");
                this.CBH01_PRN6020.SetText("");
                this.TXT01_PRN60201NM.SetValue("");
                this.CBO01_PRN6030.SetValue("N");
                this.TXT01_PRN1150.SetValue("");
                this.TXT01_PRN1160.SetValue("");
                this.TXT01_PRN1180.SetValue("");
                this.TXT01_PRN1170.SetValue("");
                this.TXT01_PRN2010.SetValue("");
                this.TXT01_PRN2020.SetValue("");
                this.TXT01_PRN2030.SetValue("");
                this.TXT01_PRN2040.SetValue("");
                this.TXT01_PRN2060.SetValue("");
                this.TXT01_PRN2070.SetValue("");
                this.TXT01_PRN2080.SetValue("");
                this.TXT01_PRN2090.SetValue("");
                this.TXT01_PRN3010.SetValue("");
                this.TXT01_PRN3020.SetValue("");
                this.TXT01_PRN3060.SetValue("");
                this.TXT01_PRN3070.SetValue("");

                this.TXT01_GAMOUNT.SetValue("");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsPRM5110     = "";
            fsYESAN_COUNT = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPPRMF") // 마스터
            {
                // 사업부
                if (this.TXT01_PRM1000.GetValue().ToString() != "A" && this.TXT01_PRM1000.GetValue().ToString() != "S" &&
                    this.TXT01_PRM1000.GetValue().ToString() != "T" && this.TXT01_PRM1000.GetValue().ToString() != "B" &&
                    this.TXT01_PRM1000.GetValue().ToString() != "C" && this.TXT01_PRM1000.GetValue().ToString() != "D" &&
                    this.TXT01_PRM1000.GetValue().ToString() != "E" )
                {
                    this.ShowMessage("TY_M_MR_2BK3H504");

                    this.TXT01_PRM1000.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청년월
                if (this.TXT01_PRM1020.GetValue().ToString().Length != 6)
                {
                    this.ShowMessage("TY_M_MR_2BK3G501");

                    this.TXT01_PRM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청월
                if (int.Parse(Get_Numeric(this.TXT01_PRM1020.GetValue().ToString().Substring(4,2))) == 0 ||
                    int.Parse(Get_Numeric(this.TXT01_PRM1020.GetValue().ToString().Substring(4, 2))) > 12)
                {
                    this.ShowMessage("TY_M_MR_2CE2E192");

                    this.TXT01_PRM1020.Focus();

                    e.Successed = false;
                    return;
                }

                // 신청사번
                if (this.CBH01_PRM2040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C298");

                    this.CBH01_PRM2040.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청일자
                if (this.DTP01_PRM2020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C297");

                    this.DTP01_PRM2020.Focus();

                    e.Successed = false;
                    return;
                }

                // 납기일자
                if (this.DTP01_PRM2050.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C296");

                    this.DTP01_PRM2050.Focus();

                    e.Successed = false;
                    return;
                }

                // 기술검토여부
                if (this.CBO01_PRM2080.GetValue().ToString() == "Y")
                {
                    if (this.CBH01_PRM2090.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BE2B295");

                        this.CBH01_PRM2090.CodeText.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 인도지역
                if (this.TXT01_PRM2100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D303");

                    this.TXT01_PRM2100.Focus();

                    e.Successed = false;
                    return;
                }

                // 인도조건
                if (this.TXT01_PRM2110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D302");

                    this.TXT01_PRM2110.Focus();

                    e.Successed = false;
                    return;
                }

                // 화폐구분
                if (this.CBH01_PRM3000.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D301");

                    this.CBH01_PRM3000.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 지불조건
                if (this.CBH01_PRM3020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D300");

                    this.CBH01_PRM3020.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 공사 및 구매명
                if (this.TXT01_PRM2120.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D299");

                    this.TXT01_PRM2120.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약번호
                if (TXT01_PRM5120.GetValue().ToString() != "" && Get_Numeric(TXT01_PRM5130.GetValue().ToString()) != "0")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BE2R306",
                        TXT01_PRM5120.GetValue().ToString(),
                        TXT01_PRM5130.GetValue().ToString(),
                        Get_Numeric(TXT01_PRM1020.GetValue().ToString().Substring(0, 4)),
                        Get_Numeric(TXT01_PRM1020.GetValue().ToString().Substring(0, 4))
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BE2E304");

                        this.TXT01_PRM2120.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        fsPRM5110 = this.TXT01_PRM1000.GetValue().ToString();
                    }

                    if (CBO01_PRM5100.GetValue().ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2BE2E305");

                        this.TXT01_PRM2120.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                /****************************************
                 * 비용청구 = 'Y'일 경우에만
                 * 청구구분, 청구화주 필드에 값을 입력 함.
                 ****************************************/
                if (this.CBO01_PRM6010.GetValue().ToString() == "N")
                {
                    this.CBO01_PRM6020.SetValue("3");
                    this.CBH01_PRM6030.SetValue("");
                }
                else
                {
                    if (this.CBO01_PRM6020.GetValue().ToString() == "3")
                    {
                        this.ShowMessage("TY_M_MR_3352R235");

                        this.CBO01_PRM6020.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_PRM6030.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_3352R236");

                        this.CBH01_PRM6030.Focus();

                        e.Successed = false;
                        return;
                    }
                }


                //// 순번 가져오기
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_MR_2BE2Y307",
                //    TXT01_PRM1000.GetValue().ToString(),
                //    TXT01_PRM1010.GetValue().ToString(),
                //    Get_Numeric(TXT01_PRM1020.GetValue().ToString())
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.TXT01_PRM1030.SetValue(dt.Rows[0]["PRM1030"].ToString());
                //}
            }
            else if (fsGUBUN == "MRPPRNF") // 내역사항
            {
                #region Description : KeyCheck

                // 귀속부서
                if (this.CBH01_PRN1040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1381");

                    this.CBH01_PRN1040.Focus();

                    e.Successed = false;
                    return;
                }

                // 예산구분
                if (this.CBH01_PRN1060.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1380");

                    this.CBH01_PRN1060.Focus();

                    e.Successed = false;
                    return;
                }

                // 계정코드
                if (this.TXT01_PRN1070.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA1379");

                    this.TXT01_PRN1070.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAF400",
                        this.TXT01_PRN1070.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAE399");

                        this.TXT01_PRN1070.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 계정과목명
                        this.TXT01_PRN1070NM.SetValue(dt.Rows[0]["A1NMAC"].ToString());
                    }
                }

                // 예산 체크
                if (this.CBH01_PRN1060.GetValue().ToString() == "1") // 투자예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAG401",
                        this.TXT01_PRO2040.GetValue().ToString().Substring(0,4),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA1378");

                        this.TXT01_PRN1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PRN1060.GetValue().ToString() == "2") // 소모성 예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAH402",
                        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        this.TXT01_PRN1090.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2390");

                        this.TXT01_PRN1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PRN1060.GetValue().ToString() == "3") // 기타본예산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGAI403",
                        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2389");

                        this.TXT01_PRN1070.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (this.CBH01_PRN1060.GetValue().ToString() == "4") // 선급자재
                {
                    // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                    //if (Convert.ToInt32(TXT01_PRN1020.GetValue().ToString()) >= 201901)
                    //{
                    if (this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                        {
                            this.ShowMessage("TY_M_MR_31L41841");

                            this.CBH01_PRN1060.Focus();

                            e.Successed = false;
                            return;
                        }
                    //}
                    //else
                    //{
                    //    if (this.TXT01_PRN1070.GetValue().ToString() != "11101001")
                    //    {
                    //        this.ShowMessage("TY_M_MR_31L41841");

                    //        this.CBH01_PRN1060.Focus();

                    //        e.Successed = false;
                    //        return;
                    //    }
                    //}
                }

                if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
                {
                    // 품목코드
                    if (this.CBH01_PRN1050.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGA2388");

                        this.CBH01_PRN1050.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BGAJ404",
                            this.CBH01_PRN1050.GetValue().ToString().Substring(0, 1),
                            this.CBH01_PRN1050.GetValue().ToString().Substring(1, 3),
                            this.CBH01_PRN1050.GetValue().ToString().Substring(4, 3),
                            this.CBH01_PRN1050.GetValue().ToString().Substring(7, 5)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            this.ShowMessage("TY_M_MR_2BGA2387");

                            this.CBH01_PRN1050.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }
                else
                {
                    // 품목코드
                    if (this.CBH01_PRN1051.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGA2388");

                        this.CBH01_PRN1051.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BGAJ404",
                            this.CBH01_PRN1051.GetValue().ToString().Substring(0, 1),
                            this.CBH01_PRN1051.GetValue().ToString().Substring(1, 3),
                            this.CBH01_PRN1051.GetValue().ToString().Substring(4, 3),
                            this.CBH01_PRN1051.GetValue().ToString().Substring(7, 5)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            this.ShowMessage("TY_M_MR_2BGA2387");

                            this.CBH01_PRN1051.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                /* 선급자재 계정일 경우 체크
                 * 1. 선급자재 계정만 오고 다른 계정은 올 수 없다.
                 * 2. 단일 귀속부서만 올 수 있다.
                 */
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용황성환 대리 요청 
                if (this.TXT01_PRN1070.GetValue().ToString() == "12210000")
                {
                    // 선급자재 계정만 오고 다른 계정은 올 수 없다.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32J94112",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32J91113");

                        this.CBH01_PRN1040.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 단일 귀속부서만 올 수 있다.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32J93111",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["PRN1040"].ToString() != this.CBH01_PRN1040.GetValue().ToString())
                            {
                                this.ShowMessage("TY_M_MR_32J9A115");

                                this.CBH01_PRN1040.Focus();

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
                        "TY_P_MR_32J93111",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        this.TXT01_PRN1020.GetValue().ToString(),
                        this.TXT01_PRN1030.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32J91113");

                        this.CBH01_PRN1040.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGA8376",
                        this.TXT01_PRM5120_1.GetValue().ToString(),              // 계약년도
                        Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()), // 계약순번
                        this.CBH01_PRN1040.GetValue().ToString(),                // 귀속부서
                        this.CBH01_PRN1060.GetValue().ToString(),                // 예산구분
                        this.TXT01_PRN1070.GetValue().ToString(),                // 적용계정
                        this.TXT01_PRN1080.GetValue().ToString(),                // 비품코드
                        this.TXT01_PRN1090.GetValue().ToString(),                // 순번
                        this.CBH01_PRN1051.GetValue().ToString()                 // 품목코드
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2386");

                        this.CBH01_PRN1051.Focus();

                        e.Successed = false;
                        return;
                    }                    
                }

                if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BF8N367",
                        this.TXT01_PRN1000.GetValue(),
                        this.TXT01_PRN1010.GetValue(),
                        this.TXT01_PRN1020.GetValue(),
                        Set_Fill4(this.TXT01_PRN1030.GetValue().ToString()),
                        this.CBH01_PRN1040.GetValue(),
                        this.CBH01_PRN1050.GetValue(),
                        this.CBH01_PRN1060.GetValue(),
                        this.TXT01_PRN1070.GetValue(),
                        this.TXT01_PRN1080.GetValue(),
                        Get_Numeric(this.TXT01_PRN1090.GetValue().ToString()),
                        Get_Numeric(this.TXT01_PRN1092.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2C48A905");

                        this.CBH01_PRN1050.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BF8N367",
                        this.TXT01_PRN1000.GetValue(),
                        this.TXT01_PRN1010.GetValue(),
                        this.TXT01_PRN1020.GetValue(),
                        Set_Fill4(this.TXT01_PRN1030.GetValue().ToString()),
                        this.CBH01_PRN1040.GetValue(),
                        this.CBH01_PRN1051.GetValue(),
                        this.CBH01_PRN1060.GetValue(),
                        this.TXT01_PRN1070.GetValue(),
                        this.TXT01_PRN1080.GetValue(),
                        Get_Numeric(this.TXT01_PRN1090.GetValue().ToString()),
                        Get_Numeric(this.TXT01_PRN1092.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2C48A905");

                        this.CBH01_PRN1051.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                string sPRN1050 = string.Empty;

                if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
                {
                    sPRN1050 = this.CBH01_PRN1050.GetValue().ToString();
                }
                else
                {
                    sPRN1050 = this.CBH01_PRN1051.GetValue().ToString();
                }

                // 내역 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_35N4Y720",
                    this.TXT01_PRN1000.GetValue(),
                    this.TXT01_PRN1010.GetValue(),
                    this.TXT01_PRN1020.GetValue(),
                    Set_Fill4(this.TXT01_PRN1030.GetValue().ToString()),
                    this.CBH01_PRN1040.GetValue(),
                    sPRN1050.ToString(),
                    this.CBH01_PRN1060.GetValue(),
                    this.TXT01_PRN1070.GetValue(),
                    this.TXT01_PRN1080.GetValue(),
                    Get_Numeric(this.TXT01_PRN1090.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_PRN1092.SetValue(dt.Rows[0]["PRN1092"].ToString());
                }

                #endregion

                #region Description : 내용 체크

                // 자산취득일경우 같은예산에 동일품목을 입력할 수 없음.
                // 자산취득일 경우 예산의 6번째 숫자와 회계의 자산분류코드 1번째 숫자가 일치해야 한다.
                if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000" && this.CBH01_PRN6020.GetValue().ToString() != "" &&
                    this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_35N8M726",
                        this.TXT01_PRN1000.GetValue().ToString(),
                        this.TXT01_PRN1010.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PRN1020.GetValue().ToString()),
                        Set_Fill4(Get_Numeric(this.TXT01_PRN1030.GetValue().ToString())),
                        this.CBH01_PRN1040.GetValue().ToString(),
                        sPRN1050.ToString(),
                        this.CBH01_PRN1060.GetValue().ToString(),
                        this.TXT01_PRN1070.GetValue().ToString(),
                        this.TXT01_PRN1080.GetValue().ToString(),
                        Get_Numeric(this.TXT01_PRN1090.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_35N8E724");

                        this.CBH01_PRN1050.Focus();

                        e.Successed = false;
                        return;
                    }

                    // 20190610 임차장님과 얘기 후 품(유형자산 비교하는 화면에서 매칭이 안되어서 품)
                    // 20171102 임차장님과 얘기 후 풀기로 함
                    if (this.TXT01_PRN1070.GetValue().ToString().Substring(5, 1) != this.CBH01_PRN6020.GetValue().ToString().Substring(0, 1))
                    {
                        this.ShowMessage("TY_M_MR_66NGQ383");

                        this.CBH01_PRN6020.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 자산 취득 및 자본적 지출일 경우
                // 예산의 6번째 숫자와 회계의 자산분류코드 1번째 숫자가 일치해야 한다.
                if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000" && this.CBH01_PRN6020.GetValue().ToString() != "")
                {
                    //if (this.TXT01_PRN1070.GetValue().ToString().Substring(5, 1) != this.CBH01_PRN6020.GetValue().ToString().Substring(0, 1))
                    //{
                    //    this.ShowMessage("TY_M_MR_66NGQ383");

                    //    this.CBH01_PRN6020.Focus();

                    //    e.Successed = false;
                    //    return;
                    //}
                }

                // 선급자재일 경우 고정자산번호 및 분류코드는 공백이다.
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_PRN1070.GetValue().ToString() == "12210000")
                {
                    this.TXT01_PRN6010.SetValue("");
                    this.TXT01_PRN6011.SetValue("");
                    this.TXT01_PRN6012.SetValue("");
                    this.TXT01_PRN6010NM.SetValue("");
                    this.CBH01_PRN6020.SetValue("");
                    this.CBH01_PRN6020.SetText("");
                    this.TXT01_PRN60201NM.SetValue("");
                    this.CBO01_PRN1120.SetValue("2");
                }

                // 거래처
                if (this.CBH01_PRN1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5395");

                    this.CBH01_PRN1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 부가세 구분
                if (this.CBH01_PRN1110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5393");

                    this.CBH01_PRN1110.Focus();

                    e.Successed = false;
                    return;
                }

                // 화폐
                if (this.CBH01_PRN1130.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5392");

                    this.SetFocus(CBH01_PRN1130.CodeText);

                    e.Successed = false;
                    return;
                }

                // 요청 수량
                if (double.Parse(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BGAA398");

                    this.TXT01_PRN1150.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 단가
                if (double.Parse(Get_Numeric(this.TXT01_PRN1160.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BGAA397");

                    this.TXT01_PRN1160.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수구분 = 금액일 경우 수량은 1임.
                if (this.CBO01_PRN1120.GetValue().ToString() == "2")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())) != 1)
                    {
                        this.TXT01_PRN1150.SetValue("1");
                        //this.ShowMessage("TY_M_MR_2BRAX669");

                        //this.TXT01_PRN1150.Focus();

                        //e.Successed = false;
                        //return;
                    }
                }

                // 화폐구분
                if (this.CBH01_PRN1130.GetValue().ToString() != "1")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PRN1180.Text.Trim())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAA396");

                        this.TXT01_PRN1180.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.TXT01_PRN1180.SetValue("0");
                }

                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    // 청구 거래처 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGA6375",
                        this.TXT01_PRM5120_1.GetValue().ToString(),              // 계약년도
                        Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()), // 계약순번
                        this.CBH01_PRN1100.GetValue().ToString()                 // 거래처
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2385");

                        this.CBH01_PRN1100.Focus();

                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGA8376",
                        this.TXT01_PRM5120_1.GetValue().ToString(),              // 계약년도
                        Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()), // 계약순번
                        this.CBH01_PRN1040.GetValue().ToString(),                // 귀속부서
                        this.CBH01_PRN1060.GetValue().ToString(),                // 예산구분
                        this.TXT01_PRN1070.GetValue().ToString(),                // 적용계정
                        this.TXT01_PRN1080.GetValue().ToString(),                // 비품코드
                        this.TXT01_PRN1090.GetValue().ToString(),                // 순번
                        this.CBH01_PRN1051.GetValue().ToString()                 // 품목코드
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 거래처
                        if (this.CBH01_PRN1100.GetValue().ToString() != dt.Rows[0]["OPM1020"].ToString())
                        {
                            this.ShowMessage("TY_M_MR_2BGA2384");

                            this.CBH01_PRN1100.Focus();

                            e.Successed = false;
                            return;
                        }

                        // 단가
                        if (double.Parse(Get_Numeric(this.TXT01_PRN1160.GetValue().ToString())) != double.Parse(dt.Rows[0]["OPN1090"].ToString()))
                        {
                            this.ShowMessage("TY_M_MR_2BGA2383");

                            this.TXT01_PRN1160.Focus();

                            e.Successed = false;
                            return;
                        }

                        // 부가세 구분
                        if (this.CBH01_PRN1110.GetValue().ToString() != dt.Rows[0]["OPM1150"].ToString())
                        {
                            this.ShowMessage("TY_M_MR_2BGA2382");

                            this.CBH01_PRN1110.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
                {
                    // 계약요청구분 'Y'인 경우
                    // 계약을 하기 위한 요청일 경우 거래처는 한개만 등록이 되어야 함.

                    if (this.TXT01_PRM5100_1.GetValue().ToString() == "Y")
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
                            if (this.CBH01_PRN1100.GetValue().ToString() != dt.Rows[0]["PRN1100"].ToString())
                            {
                                this.ShowMessage("TY_M_MR_2BGA5394");

                                this.CBH01_PRN1100.Focus();

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

                if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                {
                    // 자산번호
                    if ((this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") &&
                        // 자산분류코드
                        (this.CBH01_PRN6020.GetValue().ToString() == "")
                        )
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(CBH01_PRN6020.CodeText);

                        e.Successed = false;
                        return;
                    }

                    // 신규구매건일 경우
                    // 자산계정이면서 자산번호가 공백이고 비품구분 = Y 이면 등록 안됨
                    if ((this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") &&
                        // 비품구분
                        this.CBO01_PRN5010.GetValue().ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2BM5T588");

                        this.CBO01_PRN5010.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 비품번호가 존재할 경우
                // 비품여부 = 'N', 자산계정은 올 수 없음.
                if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                {
                    // 비품번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PRN5030.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PRN5031.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.SetFocus(TXT01_PRN5030);

                        this.ShowMessage("TY_M_MR_2BM5O584");
                        e.Successed = false;
                        return;
                    }

                    // 비품여부
                    this.CBO01_PRN5010.SetValue("N");

                    // 자산계정 체크
                    if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                    {
                        this.TXT01_PRN1070.Focus();

                        this.ShowMessage("TY_M_MR_2CEA5189");
                        e.Successed = false;
                        return;
                    }
                }

                if ((this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "") &&
                    (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "")
                    )
                {
                    this.ShowMessage("TY_M_MR_2CFCG212");
                    e.Successed = false;
                    return;
                }

                // 1) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000" &&// 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "N")                      // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PRN6020.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(CBH01_PRN6020.CodeText);

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM4T579",
                            this.CBH01_PRN6020.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM51580");

                            this.SetFocus(CBH01_PRN6020.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 2) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" &&// 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "N")                      // 비품구분
                {
                    // 고정자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5E581",
                        this.TXT01_PRN6010.GetValue().ToString(),
                        Set_Fill4(this.TXT01_PRN6011.GetValue().ToString()),
                        Set_Fill3(this.TXT01_PRN6012.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PRN6020.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(0, 1).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(1, 1).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(2, 2).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM5J582");

                        this.TXT01_PRN6010.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 3) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" &&// 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "Y")                      // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PRN6020.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(CBH01_PRN6020.CodeText);

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
                            this.CBH01_PRN6020.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM5O584");

                            this.SetFocus(CBH01_PRN6020.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 4) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" &&// 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "Y")                      // 비품구분
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PRN5030.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PRN5031.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PRN6020.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(0, 1).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(1, 1).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(2, 2).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM51580");

                        this.SetFocus(CBH01_PRN6020.CodeText);

                        e.Successed = false;
                        return;
                    }
                }

                // 5) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" &&// 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "N")                       // 비품구분
                {
                    this.CBH01_PRN6020.SetValue("");
                    this.CBH01_PRN6020.SetText("");
                    this.TXT01_PRN60201NM.SetValue("");
                    
                }

                // 자산의 수리시 수량도 나와야 하는 경우가 있음
                // 예) 타이어 교체

                // 자산번호가 존재한다는건 자산의 수리를 말함.
                if (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "")
                {
                    //if (this.CBO01_PRN1120.GetValue().ToString() != "2")
                    //{
                    //    this.CBO01_PRN1120.Focus();

                    //    this.ShowMessage("TY_M_MR_31M4H862");
                    //    e.Successed = false;
                    //    return;
                    //}
                }

                decimal dPRN1150 = 0;
                decimal dPRN1160 = 0;
                decimal dPRN1180 = 0;

                // 요청수량
                dPRN1150 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())));
                // 요청단가
                dPRN1160 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PRN1160.GetValue().ToString())));
                // 적용환율
                dPRN1180 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PRN1180.GetValue().ToString())));

                // 요청금액
                if (double.Parse(Get_Numeric(this.TXT01_PRN1180.GetValue().ToString())) == 0)
                {
                    this.TXT01_PRN1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPRN1150 * dPRN1160)));

                    this.TXT01_PRN1170.SetValue(UP_DotDelete(this.TXT01_PRN1170.GetValue().ToString()));
                }
                else
                {
                    this.TXT01_PRN1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPRN1150 * dPRN1160 * dPRN1180)));
                }

                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    // 계약입고잔량 = 요청수량
                    this.TXT01_PRN2040.SetValue(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString()));
                    // 계약입고잔액 = 요청금액
                    this.TXT01_PRN2090.SetValue(Get_Numeric(this.TXT01_PRN1170.GetValue().ToString()));
                }
                else
                {
                    // 발주잔량 = 요청수량
                    this.TXT01_PRN3020.SetValue(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString()));
                    // 발주잔액 = 요청금액
                    this.TXT01_PRN3070.SetValue(Get_Numeric(this.TXT01_PRN1170.GetValue().ToString()));
                }


                // 예산 카운트(삭제시 필요)
                fsYESAN_COUNT = "0";

                // 예산 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BG31424",
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRN1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_PRN1030.GetValue().ToString())),
                    this.CBH01_PRN1040.GetValue().ToString(),
                    this.CBH01_PRN1060.GetValue().ToString(),
                    this.TXT01_PRN1070.GetValue().ToString(),
                    this.TXT01_PRN1080.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRN1090.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                }

                #endregion
            }

            /*******************************************************************************************************
            * 등록 및 수정 삭제 불가인 경우
            *  1) 계약번호 존재시 입고테이블에 요청번호가 존재하는 경우
            *  2) 계약번호가 없는경우 발주테이블에 요청번호가 존재하는 경우
            *  3) 요청파일에 현업결재(PRM4020)이 'Y'이면서 결재자 <> ''고 결재일자 <>''경우
            * *****************************************************************************************************/

            string sPRM5120 = string.Empty;
            string sPRM5130 = string.Empty;

            string sPRNUM = string.Empty;

            sPRNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2BE8V292",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 계약번호 존재 할 경우
                if (dt.Rows[0]["PRM5120"].ToString() != "" && Get_Numeric(dt.Rows[0]["PRM5130"].ToString()) != "0")
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC54263",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC57264");

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC50267",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC58265");

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BC53268",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM1030.GetValue()
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
            fsPRM5110     = "";
            fsYESAN_COUNT = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPPRMF") // 마스터
            {
                // 신청사번
                if (this.CBH01_PRM2040.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C298");

                    this.CBH01_PRM2040.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청일자
                if (this.DTP01_PRM2020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C297");

                    this.DTP01_PRM2020.Focus();

                    e.Successed = false;
                    return;
                }

                // 납기일자
                if (this.DTP01_PRM2050.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2C296");

                    this.DTP01_PRM2050.Focus();

                    e.Successed = false;
                    return;
                }

                // 기술검토여부
                if (this.CBO01_PRM2080.GetValue().ToString() == "Y")
                {
                    if (this.CBH01_PRM2090.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BE2B295");

                        this.CBH01_PRM2090.CodeText.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 인도지역
                if (this.TXT01_PRM2100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D303");

                    this.TXT01_PRM2100.Focus();

                    e.Successed = false;
                    return;
                }

                // 인도조건
                if (this.TXT01_PRM2110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D302");

                    this.TXT01_PRM2110.Focus();

                    e.Successed = false;
                    return;
                }

                // 화폐구분
                if (this.CBH01_PRM3000.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D301");

                    this.CBH01_PRM3000.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 지불조건
                if (this.CBH01_PRM3020.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D300");

                    this.CBH01_PRM3020.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 공사 및 구매명
                if (this.TXT01_PRM2120.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BE2D299");

                    this.TXT01_PRM2120.Focus();

                    e.Successed = false;
                    return;
                }

                // 계약번호
                if (TXT01_PRM5120.GetValue().ToString() != "" && Get_Numeric(TXT01_PRM5130.GetValue().ToString()) != "0")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BE2R306",
                        TXT01_PRM5120.GetValue().ToString(),
                        TXT01_PRM5130.GetValue().ToString(),
                        Get_Numeric(TXT01_PRM1020.GetValue().ToString().Substring(0, 4)),
                        Get_Numeric(TXT01_PRM1020.GetValue().ToString().Substring(0, 4))
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BE2E304");

                        this.TXT01_PRM2120.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        fsPRM5110 = this.TXT01_PRM1000.GetValue().ToString();
                    }

                    if (CBO01_PRM5100.GetValue().ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2BE2E305");

                        this.TXT01_PRM2120.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                /****************************************
                 * 비용청구 = 'Y'일 경우에만
                 * 청구구분, 청구화주 필드에 값을 입력 함.
                 ****************************************/
                if (this.CBO01_PRM6010.GetValue().ToString() == "N")
                {
                    this.CBO01_PRM6020.SetValue("3");
                    this.CBH01_PRM6030.SetValue("");
                }
                else
                {
                    if (this.CBO01_PRM6020.GetValue().ToString() == "3")
                    {
                        this.ShowMessage("TY_M_MR_3352R235");

                        this.CBO01_PRM6020.Focus();

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_PRM6030.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_3352R236");

                        this.CBH01_PRM6030.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }
            else if (fsGUBUN == "MRPPRNF") // 내역사항
            {
                //#region Description : KeyCheck

                //// 귀속부서
                //if (this.CBH01_PRN1040.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA1381");

                //    this.CBH01_PRN1040.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 예산구분
                //if (this.CBH01_PRN1060.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA1380");

                //    this.CBH01_PRN1060.Focus();

                //    e.Successed = false;
                //    return;
                //}

                //// 계정코드
                //if (this.TXT01_PRN1070.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA1379");

                //    this.TXT01_PRN1070.Focus();

                //    e.Successed = false;
                //    return;
                //}
                //else
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAF400",
                //        this.TXT01_PRN1070.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGAE399");

                //        this.TXT01_PRN1070.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        // 계정과목명
                //        this.TXT01_PRN1070NM.SetValue(dt.Rows[0]["A1NMAC"].ToString());
                //    }
                //}

                //// 예산 체크
                //if (this.CBH01_PRN1060.GetValue().ToString() == "1") // 투자예산
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAG401",
                //        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                //        this.CBH01_PRN1040.GetValue().ToString(),
                //        this.TXT01_PRN1070.GetValue().ToString(),
                //        this.TXT01_PRN1090.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA1378");

                //        this.TXT01_PRN1070.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
                //else if (this.CBH01_PRN1060.GetValue().ToString() == "2") // 소모성 예산
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAH402",
                //        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                //        this.CBH01_PRN1040.GetValue().ToString(),
                //        this.TXT01_PRN1070.GetValue().ToString(),
                //        this.TXT01_PRN1080.GetValue().ToString(),
                //        this.TXT01_PRN1090.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA2390");

                //        this.TXT01_PRN1070.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}
                //else if (this.CBH01_PRN1060.GetValue().ToString() == "3") // 기타본예산
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAI403",
                //        this.TXT01_PRO2040.GetValue().ToString().Substring(0, 4),
                //        this.CBH01_PRN1040.GetValue().ToString(),
                //        this.TXT01_PRN1070.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA2389");

                //        this.TXT01_PRN1070.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                //// 품목코드
                //if (this.CBH01_PRN1050.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_MR_2BGA2388");

                //    this.CBH01_PRN1050.Focus();

                //    e.Successed = false;
                //    return;
                //}
                //else
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGAJ404",
                //        this.CBH01_PRN1050.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA2387");

                //        this.CBH01_PRN1050.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        // 품목명
                //        this.CBH01_PRN1050.SetText(dt.Rows[0]["Z105023"].ToString());
                //    }
                //}

                //// 장기계약일 경우 체크
                //if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_2BGA8376",
                //        this.TXT01_PRM5120_1.GetValue().ToString(),              // 계약년도
                //        Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()), // 계약순번
                //        this.CBH01_PRN1040.GetValue().ToString(),                // 귀속부서
                //        this.CBH01_PRN1060.GetValue().ToString(),                // 예산구분
                //        this.TXT01_PRN1070.GetValue().ToString(),                // 적용계정
                //        this.TXT01_PRN1080.GetValue().ToString(),                // 비품코드
                //        this.TXT01_PRN1090.GetValue().ToString(),                // 순번
                //        this.CBH01_PRN1050.GetValue().ToString()                 // 품목코드
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_MR_2BGA2386");

                //        this.CBH01_PRN1050.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                //#endregion

                #region Description : 내용 체크

                //string sPRN1050 = string.Empty;

                //if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
                //{
                //    sPRN1050 = this.CBH01_PRN1050.GetValue().ToString();
                //}
                //else
                //{
                //    sPRN1050 = this.CBH01_PRN1051.GetValue().ToString();
                //}

                //// 자산취득일경우 같은예산에 동일품목을 입력할 수 없음.
                //if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.CBH01_PRN6020.GetValue().ToString() != "" &&
                //    this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_MR_35N8M726",
                //        this.TXT01_PRN1000.GetValue().ToString(),
                //        this.TXT01_PRN1010.GetValue().ToString(),
                //        Get_Numeric(this.TXT01_PRN1020.GetValue().ToString()),
                //        Set_Fill4(Get_Numeric(this.TXT01_PRN1030.GetValue().ToString())),
                //        this.CBH01_PRN1040.GetValue().ToString(),
                //        sPRN1050.ToString(),
                //        this.CBH01_PRN1060.GetValue().ToString(),
                //        this.TXT01_PRN1070.GetValue().ToString(),
                //        this.TXT01_PRN1080.GetValue().ToString(),
                //        Get_Numeric(this.TXT01_PRN1090.GetValue().ToString())
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count > 0)
                //    {
                //        this.ShowMessage("TY_M_MR_35N8E724");

                //        this.CBH01_PRN1050.Focus();

                //        e.Successed = false;
                //        return;
                //    }
                //}

                // 자산취득일 경우 예산의 6번째 숫자와 회계의 자산분류코드 1번째 숫자가 일치해야 한다.
                if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000" && this.CBH01_PRN6020.GetValue().ToString() != "" &&
                    this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "")
                {
                    //if (this.TXT01_PRN1070.GetValue().ToString().Substring(5, 1) != this.CBH01_PRN6020.GetValue().ToString().Substring(0, 1))
                    //{
                    //    this.ShowMessage("TY_M_MR_66NGQ383");

                    //    this.CBH01_PRN6020.Focus();

                    //    e.Successed = false;
                    //    return;
                    //}
                }

                // 선급자재일 경우 고정자산번호 및 분류코드는 공백이다.
                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (this.TXT01_PRN1070.GetValue().ToString() == "12210000")
                {
                    this.TXT01_PRN6010.SetValue("");
                    this.TXT01_PRN6011.SetValue("");
                    this.TXT01_PRN6012.SetValue("");
                    this.TXT01_PRN6010NM.SetValue("");
                    this.CBH01_PRN6020.SetValue("");
                    this.CBH01_PRN6020.SetText("");
                    this.TXT01_PRN60201NM.SetValue("");
                    this.CBO01_PRN1120.SetValue("2");
                }

                // 거래처
                if (this.CBH01_PRN1100.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5395");

                    this.CBH01_PRN1100.Focus();

                    e.Successed = false;
                    return;
                }

                // 부가세 구분
                if (this.CBH01_PRN1110.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5393");

                    this.CBH01_PRN1110.Focus();

                    e.Successed = false;
                    return;
                }

                // 검수구분 = 금액일 경우 수량은 1임.
                if (this.CBO01_PRN1120.GetValue().ToString() == "2")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())) != 1)
                    {
                        this.TXT01_PRN1150.SetValue("1");
                        //this.ShowMessage("TY_M_MR_2BRAX669");

                        //this.TXT01_PRN1150.Focus();

                        //e.Successed = false;
                        //return;
                    }
                }

                // 화폐
                if (this.CBH01_PRN1130.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA5392");

                    this.SetFocus(CBH01_PRN1130.CodeText);

                    e.Successed = false;
                    return;
                }

                // 요청 수량
                if (double.Parse(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BGAA398");

                    this.TXT01_PRN1150.Focus();

                    e.Successed = false;
                    return;
                }

                // 요청 단가
                if (double.Parse(Get_Numeric(this.TXT01_PRN1160.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_MR_2BGAA397");

                    this.TXT01_PRN1160.Focus();

                    e.Successed = false;
                    return;
                }

                // 화폐구분
                if (this.CBH01_PRN1130.GetValue().ToString() != "1")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_PRN1180.Text.Trim())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGAA396");

                        this.TXT01_PRN1180.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.TXT01_PRN1180.SetValue("0");
                }

                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    // 청구 거래처 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGA6375",
                        this.TXT01_PRM5120_1.GetValue().ToString(),              // 계약년도
                        Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()), // 계약순번
                        this.CBH01_PRN1100.GetValue().ToString()                 // 거래처
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_MR_2BGA2385");

                        this.CBH01_PRN1100.Focus();

                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BGA8376",
                        this.TXT01_PRM5120_1.GetValue().ToString(),              // 계약년도
                        Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()), // 계약순번
                        this.CBH01_PRN1040.GetValue().ToString(),                // 귀속부서
                        this.CBH01_PRN1060.GetValue().ToString(),                // 예산구분
                        this.TXT01_PRN1070.GetValue().ToString(),                // 적용계정
                        this.TXT01_PRN1080.GetValue().ToString(),                // 비품코드
                        this.TXT01_PRN1090.GetValue().ToString(),                // 순번
                        this.CBH01_PRN1051.GetValue().ToString()                 // 품목코드
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 거래처
                        if (this.CBH01_PRN1100.GetValue().ToString() != dt.Rows[0]["OPM1020"].ToString())
                        {
                            this.ShowMessage("TY_M_MR_2BGA2384");

                            this.CBH01_PRN1100.Focus();

                            e.Successed = false;
                            return;
                        }

                        // 단가
                        if (double.Parse(Get_Numeric(this.TXT01_PRN1160.GetValue().ToString())) != double.Parse(dt.Rows[0]["OPN1090"].ToString()))
                        {
                            this.ShowMessage("TY_M_MR_2BGA2383");

                            this.TXT01_PRN1160.Focus();

                            e.Successed = false;
                            return;
                        }

                        // 부가세 구분
                        if (this.CBH01_PRN1110.GetValue().ToString() != dt.Rows[0]["OPM1150"].ToString())
                        {
                            this.ShowMessage("TY_M_MR_2BGA2382");

                            this.CBH01_PRN1110.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
                {
                    // 계약요청구분 'Y'인 경우
                    // 계약을 하기 위한 요청일 경우 거래처는 한개만 등록이 되어야 함.

                    if (this.TXT01_PRM5100_1.GetValue().ToString() == "Y")
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
                            if (this.CBH01_PRN1100.GetValue().ToString() != dt.Rows[0]["PRN1100"].ToString())
                            {
                                this.ShowMessage("TY_M_MR_2BGA5394");

                                this.CBH01_PRN1100.Focus();

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

                if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                {
                    // 자산번호
                    if ((this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") &&
                        // 자산분류코드
                        (this.CBH01_PRN6020.GetValue().ToString() == "")
                        )
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(CBH01_PRN6020.CodeText);

                        e.Successed = false;
                        return;
                    }

                    // 신규구매건일 경우
                    // 자산계정이면서 자산번호가 공백이고 비품구분 = Y 이면 등록 안됨
                    if ((this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") &&
                        // 비품구분
                        this.CBO01_PRN5010.GetValue().ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2BM5T588");

                        this.CBO01_PRN5010.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 비품번호가 존재할 경우
                // 비품여부 = 'N', 자산계정은 올 수 없음.
                if (this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "")
                {
                    // 비품번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PRN5030.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PRN5031.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.SetFocus(TXT01_PRN5030);

                        this.ShowMessage("TY_M_MR_2BM5O584");
                        e.Successed = false;
                        return;
                    }

                    // 비품여부
                    this.CBO01_PRN5010.SetValue("N");

                    // 자산계정 체크
                    if (this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000")
                    {
                        this.TXT01_PRN1070.Focus();

                        this.ShowMessage("TY_M_MR_2CEA5189");
                        e.Successed = false;
                        return;
                    }
                }

                if ((this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "") &&
                    (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "")
                    )
                {
                    this.ShowMessage("TY_M_MR_2CFCG212");
                    e.Successed = false;
                    return;
                }

                // 1) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) == "12200" && this.TXT01_PRN1070.GetValue().ToString() != "12210000" &&// 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "N")                      // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PRN6020.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(CBH01_PRN6020.CodeText);

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_2BM4T579",
                            this.CBH01_PRN6020.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM51580");

                            this.SetFocus(CBH01_PRN6020.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 2) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "N")                      // 비품구분
                {
                    // 고정자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5E581",
                        this.TXT01_PRN6010.GetValue().ToString(),
                        Set_Fill4(this.TXT01_PRN6011.GetValue().ToString()),
                        Set_Fill3(this.TXT01_PRN6012.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PRN6020.SetValue(dt.Rows[0]["FXGUBN"].ToString().Substring(0, 1).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(1, 1).ToString() + 
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(2, 2).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["FXGUBN"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM5J582");

                        this.TXT01_PRN6010.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 3) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "Y")                      // 비품구분
                {
                    // 자산분류코드
                    if (this.CBH01_PRN6020.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2BGCZ407");

                        this.SetFocus(CBH01_PRN6020.CodeText);

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
                            this.CBH01_PRN6020.GetValue().ToString().Substring(1, 1),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(2, 2),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(4, 4),
                            this.CBH01_PRN6020.GetValue().ToString().Substring(8, 3)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 자산분류명
                            this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                        }
                        else
                        {
                            this.ShowMessage("TY_M_MR_2BM5O584");

                            this.SetFocus(CBH01_PRN6020.CodeText);

                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 4) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() != "" && this.TXT01_PRN5031.GetValue().ToString() != "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "Y")                      // 비품구분
                {
                    // 비품DB의 자산번호에 따른 분류코드를 가져옴.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BM5N583",
                        this.TXT01_PRN5030.GetValue().ToString(),
                        Set_Fill3(this.TXT01_PRN5031.GetValue().ToString())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CBH01_PRN6020.SetValue(dt.Rows[0]["MABPCODE"].ToString().Substring(0, 1).ToString() + 
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(1, 1).ToString() + 
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(2, 2).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(4, 4).ToString() +
                                                    dt.Rows[0]["MABPCODE"].ToString().Substring(8, 3).ToString());

                        this.CBH01_PRN6020.SetText(dt.Rows[0]["FXSMDESC"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_MR_2BM51580");

                        this.SetFocus(CBH01_PRN6020.CodeText);

                        e.Successed = false;
                        return;
                    }
                }

                // 5) 자산분류코드 체크
                if ((this.TXT01_PRN5030.GetValue().ToString() == "" && this.TXT01_PRN5031.GetValue().ToString() == "") && // 비품번호
                    (this.TXT01_PRN6010.GetValue().ToString() == "" && this.TXT01_PRN6011.GetValue().ToString() == "" && this.TXT01_PRN6012.GetValue().ToString() == "") && // 자산번호
                    this.TXT01_PRN1070.GetValue().ToString().Substring(0, 5) != "12200" && // 자산계정
                    this.CBO01_PRN5010.GetValue().ToString() == "N")                       // 비품구분
                {
                    this.CBH01_PRN6020.SetValue("");
                    this.CBH01_PRN6020.SetText("");
                    this.TXT01_PRN60201NM.SetValue("");
                }

                // 자산의 수리시 수량도 나와야 하는 경우가 있음
                // 예) 타이어 교체

                // 자산번호가 존재한다는건 자산의 수리를 말함.
                if (this.TXT01_PRN6010.GetValue().ToString() != "" && this.TXT01_PRN6011.GetValue().ToString() != "" && this.TXT01_PRN6012.GetValue().ToString() != "")
                {
                    //if (this.CBO01_PRN1120.GetValue().ToString() != "2")
                    //{
                    //    this.CBO01_PRN1120.Focus();

                    //    this.ShowMessage("TY_M_MR_31M4H862");
                    //    e.Successed = false;
                    //    return;
                    //}
                }

                decimal dPRN1150 = 0;
                decimal dPRN1160 = 0;
                decimal dPRN1180 = 0;

                // 요청수량
                dPRN1150 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PRN1150.GetValue().ToString())));
                // 요청단가
                dPRN1160 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PRN1160.GetValue().ToString())));
                // 적용환율
                dPRN1180 = decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_PRN1180.GetValue().ToString())));

                // 요청금액
                if (double.Parse(Get_Numeric(this.TXT01_PRN1180.GetValue().ToString())) == 0)
                {
                    this.TXT01_PRN1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPRN1150 * dPRN1160)));

                    this.TXT01_PRN1170.SetValue(UP_DotDelete(this.TXT01_PRN1170.GetValue().ToString()));
                }
                else
                {
                    this.TXT01_PRN1170.SetValue(Convert.ToString(string.Format("{0,9:N3}", dPRN1150 * dPRN1160 * dPRN1180)));
                }

                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    // 계약입고잔량 = 요청수량
                    this.TXT01_PRN2040.SetValue(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString()));
                    // 계약입고잔액 = 요청금액
                    this.TXT01_PRN2090.SetValue(Get_Numeric(this.TXT01_PRN1170.GetValue().ToString()));
                }
                else
                {
                    // 발주잔량 = 요청수량
                    this.TXT01_PRN3020.SetValue(Get_Numeric(this.TXT01_PRN1150.GetValue().ToString()));
                    // 발주잔액 = 요청금액
                    this.TXT01_PRN3070.SetValue(Get_Numeric(this.TXT01_PRN1170.GetValue().ToString()));
                }

                // 예산 카운트(삭제시 필요)
                fsYESAN_COUNT = "0";

                // 예산 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BG31424",
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRN1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_PRN1030.GetValue().ToString())),
                    this.CBH01_PRN1040.GetValue().ToString(),
                    this.CBH01_PRN1060.GetValue().ToString(),
                    this.TXT01_PRN1070.GetValue().ToString(),
                    this.TXT01_PRN1080.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRN1090.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                }

                #endregion
            }

            /*******************************************************************************************************
             * 등록 및 수정 삭제 불가인 경우
             *  1) 계약번호 존재시 입고테이블에 요청번호가 존재하는 경우
             *  2) 계약번호가 없는경우 발주테이블에 요청번호가 존재하는 경우
             *  3) 요청파일에 현업결재(PRM4020)이 'Y'이면서 결재자 <> ''고 결재일자 <>''경우
             * *****************************************************************************************************/

            string sPRM5120 = string.Empty;
            string sPRM5130 = string.Empty;

            string sPRNUM = string.Empty;

            sPRNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2BE8V292",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 계약번호 존재 할 경우
                if (dt.Rows[0]["PRM5120"].ToString() != "" && Get_Numeric(dt.Rows[0]["PRM5130"].ToString()) != "0")
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC54263",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC57264");

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC50267",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC58265");

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BC53268",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM1030.GetValue()
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2BC59266");

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
            fsPRM5110     = "";
            fsYESAN_COUNT = "0";

            DataTable dt = new DataTable();

            if (fsGUBUN == "MRPPRMF") // 마스터
            {
                // 내역사항 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BD2D280",
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z312");

                    this.CBH01_PRM2040.CodeText.Focus();

                    e.Successed = false;
                    return;
                }

                // 특기사항 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BE4W310",
                    this.TXT01_PRM1000.GetValue().ToString(),
                    this.TXT01_PRM1010.GetValue().ToString(),
                    this.TXT01_PRM1020.GetValue().ToString(),
                    this.TXT01_PRM1030.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z311");

                    this.CBH01_PRM2040.CodeText.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "MRPPRNF") // 내역사항
            {
                string sRRM1130 = string.Empty;

                // 요청번호
                sRRM1130 = this.TXT01_PRN1000.GetValue().ToString() + this.TXT01_PRN1010.GetValue().ToString() + this.TXT01_PRN1020.GetValue().ToString() + this.TXT01_PRN1030.GetValue().ToString();

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
                    "TY_P_MR_2BG31424",
                    this.TXT01_PRN1000.GetValue().ToString(),
                    this.TXT01_PRN1010.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRN1020.GetValue().ToString()),
                    Set_Fill4(Get_Numeric(this.TXT01_PRN1030.GetValue().ToString())),
                    this.CBH01_PRN1040.GetValue().ToString(),
                    this.CBH01_PRN1060.GetValue().ToString(),
                    this.TXT01_PRN1070.GetValue().ToString(),
                    this.TXT01_PRN1080.GetValue().ToString(),
                    Get_Numeric(this.TXT01_PRN1090.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsYESAN_COUNT = dt.Rows[0]["COUNT"].ToString();
                }
            }

            /*******************************************************************************************************
             * 등록 및 수정 삭제 불가인 경우
             *  1) 계약번호 존재시 입고테이블에 요청번호가 존재하는 경우
             *  2) 계약번호가 없는경우 발주테이블에 요청번호가 존재하는 경우
             *  3) 요청파일에 현업결재(PRM4020)이 'Y'이면서 결재자 <> ''고 결재일자 <>''경우
             * *****************************************************************************************************/

            string sPRM5120 = string.Empty;
            string sPRM5130 = string.Empty;

            string sPRNUM = string.Empty;

            sPRNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2BE8V292",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 계약번호 존재 할 경우
                if (dt.Rows[0]["PRM5120"].ToString() != "" && Get_Numeric(dt.Rows[0]["PRM5130"].ToString()) != "0")
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC54263",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC57264");

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC50267",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC58265");

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BC53268",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM1030.GetValue()
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
            ds.Tables.Add(this.FPS91_TY_S_MR_2BC30258.GetDataSourceInclude(TSpread.TActionType.New, "PRT1040", "PRT1050"));

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2BC30258.GetDataSourceInclude(TSpread.TActionType.Update, "PRT1040", "PRT1050"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2BC3I259",
                                       TXT01_PRT1000.GetValue(),
                                       TXT01_PRT1010.GetValue(),
                                       TXT01_PRT1020.GetValue(),
                                       TXT01_PRT1030.GetValue(),
                                       ds.Tables[0].Rows[i]["PRT1040"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2BE4Z311");
                    e.Successed = false;
                    return;
                }
            }

            DataTable dt = new DataTable();

            /*******************************************************************************************************
             * 등록 및 수정 삭제 불가인 경우
             *  1) 계약번호 존재시 입고테이블에 요청번호가 존재하는 경우
             *  2) 계약번호가 없는경우 발주테이블에 요청번호가 존재하는 경우
             *  3) 요청파일에 현업결재(PRM4020)이 'Y'이면서 결재자 <> ''고 결재일자 <>''경우
             * *****************************************************************************************************/

            string sPRM5120 = string.Empty;
            string sPRM5130 = string.Empty;

            string sPRNUM = string.Empty;

            sPRNUM = this.TXT01_PRT1000.GetValue().ToString() + this.TXT01_PRT1010.GetValue().ToString() + this.TXT01_PRT1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRT1030.GetValue().ToString());

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2BE8V292",
                this.TXT01_PRT1000.GetValue(),
                this.TXT01_PRT1010.GetValue(),
                this.TXT01_PRT1020.GetValue(),
                Set_Fill4(this.TXT01_PRT1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 계약번호 존재 할 경우
                if (dt.Rows[0]["PRM5120"].ToString() != "" && Get_Numeric(dt.Rows[0]["PRM5130"].ToString()) != "0")
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC54263",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC57264");

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC50267",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC58265");

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BC53268",
                this.TXT01_PRT1000.GetValue(),
                this.TXT01_PRT1010.GetValue(),
                this.TXT01_PRT1020.GetValue(),
                this.TXT01_PRT1030.GetValue()
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
            ds.Tables.Add(this.FPS91_TY_S_MR_2BC30258.GetDataSourceInclude(TSpread.TActionType.Remove, "PRT1040"));

            DataTable dt = new DataTable();

            /*******************************************************************************************************
             * 등록 및 수정 삭제 불가인 경우
             *  1) 계약번호 존재시 입고테이블에 요청번호가 존재하는 경우
             *  2) 계약번호가 없는경우 발주테이블에 요청번호가 존재하는 경우
             *  3) 요청파일에 현업결재(PRM4020)이 'Y'이면서 결재자 <> ''고 결재일자 <>''경우
             * *****************************************************************************************************/

            string sPRM5120 = string.Empty;
            string sPRM5130 = string.Empty;

            string sPRNUM = string.Empty;

            sPRNUM = this.TXT01_PRT1000.GetValue().ToString() + this.TXT01_PRT1010.GetValue().ToString() + this.TXT01_PRT1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRT1030.GetValue().ToString());

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2BE8V292",
                this.TXT01_PRT1000.GetValue(),
                this.TXT01_PRT1010.GetValue(),
                this.TXT01_PRT1020.GetValue(),
                Set_Fill4(this.TXT01_PRT1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 계약번호 존재 할 경우
                if (dt.Rows[0]["PRM5120"].ToString() != "" && Get_Numeric(dt.Rows[0]["PRM5130"].ToString()) != "0")
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC54263",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC57264");

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC50267",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2BC58265");

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BC53268",
                this.TXT01_PRT1000.GetValue(),
                this.TXT01_PRT1010.GetValue(),
                this.TXT01_PRT1020.GetValue(),
                this.TXT01_PRT1030.GetValue()
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
            /*******************************************************************************************************
             * 등록 및 수정 삭제 불가인 경우
             *  1) 계약번호 존재시 입고테이블에 요청번호가 존재하는 경우
             *  2) 계약번호가 없는경우 발주테이블에 요청번호가 존재하는 경우
             *  3) 요청파일에 현업결재(PRM4020)이 'Y'이면서 결재자 <> ''고 결재일자 <>''경우
             * *****************************************************************************************************/

            string sPRM5120 = string.Empty;
            string sPRM5130 = string.Empty;

            string sPRNUM = string.Empty;

            sPRNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2BE8V292",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                Set_Fill4(this.TXT01_PRM1030.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 계약번호 존재 할 경우
                if (dt.Rows[0]["PRM5120"].ToString() != "" && dt.Rows[0]["PRM5130"].ToString() != "0")
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC54263",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.TXT01_MESSAGE.SetValue("입고 자료에 요청번호가 존재합니다.");
                        return false;
                    }
                }
                else
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2BC50267",
                        sPRNUM.ToString()
                        );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.TXT01_MESSAGE.SetValue("발주 자료에 요청번호가 존재합니다.");
                        return false;
                    }
                }
            }

            // 결재 완료 문서 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BC53268",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM1030.GetValue()
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
            else if (sGUBUN == "MRPPRMF")
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

            if (tabControl1.SelectedIndex == 0) // 마스터
            {
                fsGUBUN = "MRPPRMF";

                // 마스터 DISPLAY
                UP_MRPPRMF_DISPLAY();

                // 마감체크
                fResult = UP_MAGAM_CHECK();

                if (fResult == true)
                {
                    // 버튼 컨트롤
                    // 마스터 데이터가 존재하므로 
                    // 구매요청 마스터 탭 로드시 수정, 삭제 버튼 보이게 함
                    UP_ImgbtnDisplay("1", true);

                    SetStartingFocus(this.CBH01_PRM2040.CodeText);
                }
                else  // 마감완료면
                {
                    // 버튼 컨트롤
                    UP_ImgbtnDisplay("3", false);
                }
            }
            else if (tabControl1.SelectedIndex == 1) // 내역사항
            {
                fsGUBUN = "MRPPRNF";

                this.TXT01_PRN1000.SetValue(this.TXT01_PRM1000.GetValue());
                this.TXT01_PRN1010.SetValue(this.TXT01_PRM1010.GetValue());
                this.TXT01_PRN1020.SetValue(this.TXT01_PRM1020.GetValue());
                this.TXT01_PRN1030.SetValue(this.TXT01_PRM1030.GetValue());

                // 계약번호
                this.TXT01_PRM5100_1.SetValue(this.CBO01_PRM5100.GetValue());
                this.TXT01_PRM5120_1.SetValue(this.TXT01_PRM5120.GetValue());
                this.TXT01_PRM5130_1.SetValue(this.TXT01_PRM5130.GetValue());

                // 적용년월
                this.TXT01_PRO2040.SetValue(this.TXT01_PRM1020.GetValue().ToString().Substring(0, 4) + this.TXT01_PRM1020.GetValue().ToString().Substring(4, 2));

                // 장기계약 품목코드 조회시 사용
                this.CBH01_PRN1051.DummyValue = new string[] { this.TXT01_PRM5120_1.GetValue().ToString(), this.TXT01_PRM5130_1.GetValue().ToString() };

                this.TXT01_PRN1070.SetReadOnly(true);
                this.TXT01_PRN1070NM.SetReadOnly(true);
                this.TXT01_PRN1080.SetReadOnly(true);
                this.TXT01_PRN1080NM.SetReadOnly(true);
                this.TXT01_PRN1090.SetReadOnly(true);
                this.TXT01_PRN1090NM.SetReadOnly(true);
                this.TXT01_PRN1092.SetReadOnly(true);

                this.TXT01_PRO2040.SetReadOnly(true);

                this.CBH01_PRN1051.SetReadOnly(true);
                this.CBH01_PRN1050.SetReadOnly(true);
                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    this.CBH01_PRN1051.Visible = true;
                    this.CBH01_PRN1050.Visible = false;
                }
                else
                {
                    this.CBH01_PRN1051.Visible = false;
                    this.CBH01_PRN1050.Visible = true;
                }

                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    this.CBH01_PRN1040.SetReadOnly(true);
                    this.CBH01_PRN1060.SetReadOnly(true);
                    this.CBH01_PRN1100.SetReadOnly(true);
                    this.CBH01_PRN1110.SetReadOnly(true);

                    // 자산분류코드
                    this.CBH01_PRN6020.SetReadOnly(true);

                    this.TXT01_PRN1070.SetReadOnly(true);
                    this.BTN61_PRN10701.Enabled = false;
                    //this.BTN61_PRN10701.SetReadOnly(true);
                    this.TXT01_PRN1070NM.SetReadOnly(true);
                    this.TXT01_PRN1080.SetReadOnly(true);
                    this.TXT01_PRN1080NM.SetReadOnly(true);
                    this.TXT01_PRN1090.SetReadOnly(true);
                    this.TXT01_PRN1090NM.SetReadOnly(true);
                    this.TXT01_PRN1092.SetReadOnly(true);

                    this.TXT01_PRN1160.SetReadOnly(true);
                }

                // 내역사항 DISPLAY
                UP_MRPPRNF_DISPLAY();

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
            }
            else if (tabControl1.SelectedIndex == 2) // 특기사항
            {
                fsGUBUN = "MRPPRTF";

                this.TXT01_PRT1000.SetValue(this.TXT01_PRM1000.GetValue());
                this.TXT01_PRT1010.SetValue(this.TXT01_PRM1010.GetValue());
                this.TXT01_PRT1020.SetValue(this.TXT01_PRM1020.GetValue());
                this.TXT01_PRT1030.SetValue(this.TXT01_PRM1030.GetValue());

                this.TXT01_PRT1000.SetReadOnly(true);
                this.TXT01_PRT1010.SetReadOnly(true);
                this.TXT01_PRT1020.SetReadOnly(true);
                this.TXT01_PRT1030.SetReadOnly(true);

                // 특기사항 DISPLAY
                UP_MRPPRTF_DISPLAY();

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

        #region Description : 신규 버튼 클릭시 이전값 가지고 있음
        private void UP_GET_MRPPRNF_REMEMBER(string sGUBUN)
        {
            if (sGUBUN.ToString() != "DEL")
            {
                fsPRN1040   = this.CBH01_PRN1040.GetValue().ToString();
                fsPRN1060   = this.CBH01_PRN1060.GetValue().ToString();
                fsPRN1070   = this.TXT01_PRN1070.GetValue().ToString();
                fsPRN1070NM = this.TXT01_PRN1070NM.GetValue().ToString();
                fsPRN1080   = this.TXT01_PRN1080.GetValue().ToString();
                fsPRN1080NM = this.TXT01_PRN1080NM.GetValue().ToString();
                fsPRN1090   = this.TXT01_PRN1090.GetValue().ToString();
                fsPRN1090NM = this.TXT01_PRN1090NM.GetValue().ToString();
                //fsPRO2040   = this.TXT01_PRO2040.GetValue().ToString();
                fsPRN1100   = this.CBH01_PRN1100.GetValue().ToString();
                fsPRN1110   = this.CBH01_PRN1110.GetValue().ToString();
            }
            else
            {
                fsPRN1040   = "";
                fsPRN1060   = "";
                fsPRN1070   = "";
                fsPRN1070NM = "";
                fsPRN1080   = "";
                fsPRN1080NM = "";
                fsPRN1090   = "";
                fsPRN1090NM = "";
                //fsPRO2040   = "";
                fsPRN1100   = "";
                fsPRN1110   = "";
            }
        }
        #endregion

        #region Description : 신규 버튼 클릭시 이전값을 필드에 뿌려줌
        private void UP_SET_MRPPRNF_REMEMBER()
        {
            this.CBH01_PRN1040.SetValue(fsPRN1040.ToString());
            this.CBH01_PRN1060.SetValue(fsPRN1060.ToString());
            this.TXT01_PRN1070.SetValue(fsPRN1070.ToString());
            this.TXT01_PRN1070NM.SetValue(fsPRN1070NM.ToString());
            this.TXT01_PRN1080.SetValue(fsPRN1080.ToString());
            this.TXT01_PRN1080NM.SetValue(fsPRN1080NM.ToString());
            this.TXT01_PRN1090.SetValue(fsPRN1090.ToString());
            this.TXT01_PRN1090NM.SetValue(fsPRN1090NM.ToString());
            //this.TXT01_PRO2040.SetValue(fsPRO2040.ToString());
            this.CBH01_PRN1100.SetValue(fsPRN1100.ToString());
            this.CBH01_PRN1110.SetValue(fsPRN1110.ToString());
        }
        #endregion

        #region Description : 컨트롤 초기화
        private void UP_Control_Initialize(string sGUBUN, bool bTrueFalse)
        {
            this.TXT01_PRN1000.SetReadOnly(true);
            this.TXT01_PRN1010.SetReadOnly(true);
            this.TXT01_PRN1020.SetReadOnly(true);
            this.TXT01_PRN1030.SetReadOnly(true);

            this.TXT01_PRT1000.SetReadOnly(true);
            this.TXT01_PRT1010.SetReadOnly(true);
            this.TXT01_PRT1020.SetReadOnly(true);
            this.TXT01_PRT1030.SetReadOnly(true);

            this.TXT01_PRM5100_1.SetReadOnly(true);
            this.TXT01_PRM5120_1.SetReadOnly(true);
            this.TXT01_PRM5130_1.SetReadOnly(true);

            this.TXT01_OPM1040.SetReadOnly(true);

            this.TXT01_GAMOUNT.SetReadOnly(true);

            if (sGUBUN == "MRPPRMF") // 마스터
            {
                this.TXT01_PRM1010.SetReadOnly(bTrueFalse);
                this.TXT01_PRM1030.SetReadOnly(bTrueFalse);

                this.TXT01_PRM2010.SetReadOnly(bTrueFalse);
                this.TXT01_DTDESC.SetReadOnly(bTrueFalse);
                this.TXT01_PRM2030.SetReadOnly(bTrueFalse);
                this.TXT01_DTDESC1.SetReadOnly(bTrueFalse);

                this.TXT01_PRM3010.SetReadOnly(bTrueFalse);
                this.TXT01_PRM4010.SetReadOnly(bTrueFalse);
                this.TXT01_PRM4050.SetReadOnly(bTrueFalse);
                this.TXT01_PRM4000.SetReadOnly(bTrueFalse);
                this.TXT01_PRM4020.SetReadOnly(bTrueFalse);
                this.TXT01_PRM4030.SetReadOnly(bTrueFalse);
                this.TXT01_PRM4040.SetReadOnly(bTrueFalse);
                this.TXT01_KBHANGL1.SetReadOnly(bTrueFalse);

                this.TXT01_PRM7010.SetReadOnly(bTrueFalse);
                this.TXT01_PRM7020.SetReadOnly(bTrueFalse);
                this.TXT01_PRM7030.SetReadOnly(bTrueFalse);
                this.TXT01_PRM7060.SetReadOnly(bTrueFalse);
                this.TXT01_PRM7070.SetReadOnly(bTrueFalse);
                this.TXT01_PRM7080.SetReadOnly(bTrueFalse);

                this.FPS91_TY_S_MR_2BCBY255.Initialize();
                this.FPS91_TY_S_MR_2BD4M288.Initialize();

                // 예산 DISPLAY
                UP_MRPPROF_DISPLAY();
            }
            else if (sGUBUN == "MRPPRNF") // 내역사항
            {
                this.CBH01_PRN1040.SetReadOnlyButton(bTrueFalse);
                this.CBH01_PRN1060.SetReadOnlyButton(bTrueFalse);

                // 장기계약일 경우 체크
                if (this.TXT01_PRM5120_1.GetValue().ToString() != "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) != "0")
                {
                    // 버튼
                    this.BTN61_PRN10701.Enabled = false;
                    //this.BTN61_PRN10701.SetReadOnly(true);

                    this.CBH01_PRN1050.Visible = false;
                    this.CBH01_PRN1051.Visible = true;

                    this.CBH01_PRN1051.SetReadOnly(bTrueFalse);
                    this.CBH01_PRN1051.SetReadOnlyButton(bTrueFalse);
                    this.CBH01_PRN1051.SetReadOnlyText(bTrueFalse);
                }
                else
                {
                    this.CBH01_PRN1050.Visible = true;
                    this.CBH01_PRN1051.Visible = false;

                    this.CBH01_PRN1050.SetReadOnly(bTrueFalse);
                    this.CBH01_PRN1050.SetReadOnlyButton(bTrueFalse);
                    this.CBH01_PRN1050.SetReadOnlyText(bTrueFalse);
                }

                this.CBH01_PRN1040.SetReadOnly(bTrueFalse);
                this.CBH01_PRN1040.SetReadOnlyCode(bTrueFalse);
                this.CBH01_PRN1040.SetReadOnlyText(bTrueFalse);

                
                this.CBH01_PRN1060.SetReadOnly(bTrueFalse);
                this.CBH01_PRN1060.SetReadOnlyCode(bTrueFalse);
                this.CBH01_PRN1060.SetReadOnlyText(bTrueFalse);
                
                this.TXT01_PRN1070.SetReadOnly(bTrueFalse);
                this.TXT01_PRN1070NM.SetReadOnly(bTrueFalse);

                //if (bTrueFalse == true)
                //{
                //    this.BTN61_PRN10701.Enabled = false;
                //}
                //else
                //{
                //    this.BTN61_PRN10701.Enabled = true;
                //}
                //this.BTN61_PRN10701.SetReadOnly(bTrueFalse);
            }
        }
        #endregion

        #region Description : 버튼 컨트롤
        private void UP_ImgbtnDisplay(string sGubn, bool bTrueFalse)
        {
            if (fsGUBUN == "MRPPRMF")
            {
                this.BTN61_NEW.Visible = false;

                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                if (sGubn == "1") // 수정 및 삭제
                {
                    BTN61_SAV.Visible = false;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                    BTN61_SEND.Visible = true;
                }
                else if (sGubn == "2") // 등록
                {
                    BTN61_SAV.Visible = true;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                    BTN61_SEND.Visible = true;
                }
                else
                {
                    BTN61_SAV.Visible  = bTrueFalse;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible  = bTrueFalse;
                    BTN61_SEND.Visible = false;
                }
            }
            else if (fsGUBUN == "MRPPRNF")
            {
                this.BTN61_NEW.Visible = true;
                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                if (sGubn == "1") // 수정 및 삭제
                {
                    BTN61_SAV.Visible = false;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                    BTN61_SEND.Visible = true;

                    //BTN61_SEARCH.Visible = true;
                }
                else if (sGubn == "2") // 등록
                {
                    BTN61_SAV.Visible = true;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible = bTrueFalse;
                    BTN61_SEND.Visible = true;

                    //BTN61_SEARCH.Visible = true;
                }
                else
                {
                    BTN61_SAV.Visible  = bTrueFalse;
                    BTN61_EDIT.Visible = bTrueFalse;
                    BTN61_REM.Visible  = bTrueFalse;
                    BTN61_SEND.Visible = false;

                    //BTN61_SEARCH.Visible = bTrueFalse;
                }
            }
            else if (fsGUBUN == "MRPPRTF")
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
                this.BTN61_NEW.Visible  = false;
                this.BTN61_SAV.Visible  = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible  = false;

                this.BTN62_SAV.Visible = false;
                this.BTN62_REM.Visible = false;

                BTN61_SEND.Visible = false;
            }
        }
        #endregion

        #region Description : 폼 닫기 이벤트
        private void TYMRPR001I_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 팝업창 파라미터값을 부모창에 전달 함.
            fsPRM1000 = this.TXT01_PRM1000.GetValue().ToString();
            fsPRM1010 = this.TXT01_PRM1010.GetValue().ToString();
            fsPRM1020 = this.TXT01_PRM1020.GetValue().ToString();
            fsPRM1030 = this.TXT01_PRM1030.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        #endregion

        #endregion






















        private void TXT01_PRM1030_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                // 품목코드 코드헬프
                //TYMRGB003S popup = new TYMRGB003S();

                //if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    this.TXT01_PRM1020.SetValue(popup.fsJEPUM);
                //    this.TXT01_PRM1030.SetValue(popup.fsZ105013);
                //}

                // 장기계약 코드헬프
                //TYMRGB001S popup = new TYMRGB001S(this.TXT01_PRM1020.GetValue().ToString());

                //if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    this.TXT01_PRM1000.SetValue(popup.fsOPM1000); // 계약년도
                //    this.TXT01_PRM1010.SetValue(popup.fsOPM1010); // 계약순번
                //    this.TXT01_PRM1030.SetValue(popup.fsOPM1040); // 계약내용
                //}

                // 기타세목예산(투자&수선) 코드헬프
                //TYMRGB005S popup = new TYMRGB005S(this.TXT01_PRM1000.GetValue().ToString().Substring(0,4), this.TXT01_PRM1010.GetValue().ToString(), this.TXT01_PRM1020.GetValue().ToString());

                //if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    // 적용년월은 요청년월임
                //    // 요청년월의 본예산, 집행예산 금액 가져옴
                //    this.TXT01_Text1.SetValue(popup.fsCDAC);   // 계정과목
                //    this.TXT01_Text3.SetValue(popup.fsCDACNM); // 계정과목명
                //    this.TXT01_Text5.SetValue(popup.fsSEQ);    // 순번
                //}

                //// 소모품비 예산세목 코드헬프
                //TYMRGB006S popup = new TYMRGB006S(this.TXT01_PRM1000.GetValue().ToString().Substring(0, 4), this.TXT01_PRM1010.GetValue().ToString(), this.TXT01_PRM1020.GetValue().ToString());

                //if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    // 적용년월은 요청년월임
                //    // 요청년월의 본예산, 집행예산 금액 가져옴
                //    this.TXT01_Text1.SetValue(popup.fsCDAC);   // 계정과목
                //    this.TXT01_Text3.SetValue(popup.fsCDACNM); // 계정과목명
                //    this.TXT01_Text5.SetValue(popup.fsCDJJ);   // 비품코드
                //    this.TXT01_Text6.SetValue(popup.fsBPDESC); // 비품명
                //    this.TXT01_Text7.SetValue(popup.fsSEQ);    // 순번
                //}

                //// 기타예산 코드헬프
                //TYMRGB007S popup = new TYMRGB007S(this.TXT01_PRM1000.GetValue().ToString().Substring(0, 4), this.TXT01_PRM1010.GetValue().ToString(), this.TXT01_PRM1020.GetValue().ToString());

                //if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    // 적용년월은 요청년월임
                //    // 요청년월의 본예산, 집행예산 금액 가져옴
                //    this.TXT01_Text1.SetValue(popup.fsCDAC);   // 계정과목
                //    this.TXT01_Text3.SetValue(popup.fsCDACNM); // 계정과목명
                //}

                //// 구매요청 코드헬프
                //TYMZPO01C1 popup = new TYMZPO01C1(this.TXT01_PRM1000.GetValue().ToString(), this.TXT01_PRM1010.GetValue().ToString(), this.TXT01_PRM1010.GetValue().ToString());

                //if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    this.TXT01_Text1.SetValue(popup.fsPRM1000);
                //    this.TXT01_Text3.SetValue(popup.fsPRM1020);
                //    this.TXT01_Text5.SetValue(popup.fsPRM1030);
                //    this.TXT01_Text6.SetValue(popup.fsPRM2020);
                //    this.TXT01_Text7.SetValue(popup.fsKBHANGL);
                //    this.TXT01_Text9.SetValue(popup.fsPRM2120);
                //    this.TXT01_Text10.SetValue(popup.fsDTDESC1);
                //    this.TXT01_Text11.SetValue(popup.fsPRM2080);
                //    this.TXT01_Text12.SetValue(popup.fsDTDESC2);
                //    this.TXT01_Text13.SetValue(popup.fsPRM2070);
                //    this.TXT01_Text15.SetValue(popup.fsPRM5130);
                //    this.TXT01_Text16.SetValue(popup.fsOPM1040);
                //    this.TXT01_Text17.SetValue(popup.fsPRM2100);
                //    this.TXT01_Text19.SetValue(popup.fsPRM2110);
                //    this.TXT01_Text20.SetValue(popup.fsPRM2050);
                //}
            }
        }

        #region Description : 장기계약 품목 코드 박스
        private void CBH01_PRN1051_CodeBoxDataBinded(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            // 귀속부서
            this.CBH01_PRN1040.SetValue(this.CBH01_PRN1051.GetText().Substring(0, 6));
            // 예산구분
            this.CBH01_PRN1060.SetValue(this.CBH01_PRN1051.GetText().Substring(6, 1));

            // 적용계정
            this.TXT01_PRN1070.SetValue(this.CBH01_PRN1051.GetText().Substring(7, 8));

            // 적용계정명
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_GB_24391302",
                this.TXT01_PRN1070.GetValue().ToString(),
                ""
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_PRN1070NM.SetValue(dt.Rows[0]["Code_Name"].ToString());
            }

            // 비품코드
            this.TXT01_PRN1080.SetValue(this.CBH01_PRN1051.GetText().Substring(15, 3));

            if (this.TXT01_PRN1080.GetValue().ToString() != "")
            {
                // 비품코드명
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_GB_2423M259",
                    "BP",
                    this.TXT01_PRN1080.GetValue().ToString(),
                    ""
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_PRN1080NM.SetValue(dt.Rows[0]["Code_Name"].ToString());
                }
            }

            // 순번
            this.TXT01_PRN1090.SetValue(this.CBH01_PRN1051.GetText().Substring(18, 3));

            // 품목명
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B631138",
                this.CBH01_PRN1051.GetValue().ToString().Substring(0, 1),
                this.CBH01_PRN1051.GetValue().ToString().Substring(1, 3),
                this.CBH01_PRN1051.GetValue().ToString().Substring(4, 3),
                this.CBH01_PRN1051.GetValue().ToString().Substring(7, 5)
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_PRN1051.SetText(dt.Rows[0]["Z105013"].ToString());
            }

            // 발주 단가 및 자산분류코드 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2C7AA977",
                this.TXT01_PRM5120_1.GetValue().ToString(),
                this.TXT01_PRM5130_1.GetValue().ToString(),
                this.CBH01_PRN1040.GetValue().ToString(),
                this.CBH01_PRN1060.GetValue().ToString(),
                this.TXT01_PRN1070.GetValue().ToString(),
                this.TXT01_PRN1080.GetValue().ToString(),
                this.TXT01_PRN1090.GetValue().ToString(),
                this.CBH01_PRN1051.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 청구거래처
                this.CBH01_PRN1100.SetValue(dt.Rows[0]["PON1100"].ToString());

                // 부가세구분
                this.CBH01_PRN1110.SetValue(dt.Rows[0]["PON1110"].ToString());

                // 자산분류코드
                this.CBH01_PRN6020.SetValue(dt.Rows[0]["PON1620"].ToString());

                // 발주단가
                this.TXT01_PRN1160.SetValue(dt.Rows[0]["PON1160"].ToString());
            }

            this.SetFocus(this.CBO01_PRN1120);
        }
        #endregion

        private void TXT01_PRN1170_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if(this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
                else if (this.BTN61_EDIT.Visible == true)
                {
                    SetFocus(this.BTN61_EDIT);
                }
            }
        }

        #region Description : 예산 구분
        private void CBH01_PRN1060_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_PRN1060.GetValue().ToString() == "4")
            {
                this.TXT01_PRN1070.SetReadOnly(true);
                this.TXT01_PRN1070NM.SetReadOnly(true);
                this.BTN61_PRN10701.Enabled = false;

                // 황성환 대리 요청 20190101 이후부터 12210000(건설중인자산)계정으로 사용
                if (Convert.ToInt32(TXT01_PRN1020.GetValue().ToString()) >= 201901)
                {
                    this.TXT01_PRN1070.SetValue("12210000");
                }
                else
                {
                    this.TXT01_PRN1070.SetValue("11101001");
                }

                this.TXT01_PRN1080.SetValue("");
                this.TXT01_PRN1080NM.SetValue("");
                this.TXT01_PRN1090.SetValue("");
                this.TXT01_PRN1090NM.SetValue("");
            }
            else
            {
                this.TXT01_PRN1070.SetReadOnly(false);
                this.TXT01_PRN1070NM.SetReadOnly(false);
                this.BTN61_PRN10701.Enabled = true;

                this.TXT01_PRN1070.SetValue("");
            }
        }
        #endregion

        private void CBH01_PRN1050_CodeBoxDataBinded(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            
            // 품목별 최근 구매단가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_32DAH053",
                this.CBH01_PRN1050.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_GAMOUNT.SetValue(dt.Rows[0]["RRN1210"].ToString());
            }
        }

        #region Description : 품목 사진 버튼
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            string sJPCODE = string.Empty;

            // 장기계약일 경우 체크
            if (this.TXT01_PRM5120_1.GetValue().ToString() == "" && Get_Numeric(this.TXT01_PRM5130_1.GetValue().ToString()) == "0")
            {
                if (this.CBH01_PRN1050.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA2388");
                    this.CBH01_PRN1050.Focus();

                    return;
                }
                else
                {
                    sJPCODE = this.CBH01_PRN1050.GetValue().ToString();
                }
            }
            else
            {
                if (this.CBH01_PRN1051.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_2BGA2388");
                    this.CBH01_PRN1051.Focus();

                    return;
                }
                else
                {
                    sJPCODE = this.CBH01_PRN1051.GetValue().ToString();
                }
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

                this.SetFocus(this.CBH01_PRN1100);
            }
        }
        #endregion

        #region Description : 승인 제출 버튼
        private void BTN61_SEND_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_36P3J879",
                this.TXT01_PRM1000.GetValue().ToString(),
                this.TXT01_PRM1010.GetValue().ToString(),
                this.TXT01_PRM1020.GetValue().ToString(),
                this.TXT01_PRM1030.GetValue().ToString()
                );

            this.DbConnector.ExecuteNonQueryList();

            // 메일 발송
            UP_Mail_Send();

            this.ShowMessage("TY_M_MR_3649D806");
        }
        #endregion

        #region Description : 메일 발송
        private void UP_Mail_Send()
        {
            // 원본
            //string sFirst       = string.Empty;
            //string sLine        = string.Empty;


            //string sMailFrom    = string.Empty;
            //string sMailFromNM  = string.Empty;
            //string sMailTo      = string.Empty;
            //string sMailToNM    = string.Empty;

            //string sPRMNUM  = string.Empty;

            //sPRMNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            //DataTable dt = new DataTable();

            //// 발신자
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_35T1I774",
            //    TYUserInfo.EmpNo
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    sMailFrom   = dt.Rows[0]["KBMAILID"].ToString() + "@taeyoung.co.kr";
            //    sMailFromNM = dt.Rows[0]["KBHANGL"].ToString();
            //}

            //// 수신자
            //sMailTo = "hjyoon" + "@taeyoung.co.kr";
            //sMailToNM = "윤홍준";

            //// 메일 발신자
            //MailAddress from    = new MailAddress(sMailFrom.ToString(), sMailFromNM.ToString());
            //// 메일 수신자
            //MailAddress to      = new MailAddress(sMailTo.ToString(), sMailToNM.ToString());            
            //MailMessage message = new MailMessage(from, to);

            //message.Subject = "구매요청 승인제출건 - 요청번호:" + sPRMNUM.ToString();
            
            //sLine = "요청번호 : " + sPRMNUM.ToString() + ", 구매명 : " + this.TXT01_PRM2120.GetValue().ToString();

            //// HTML 사용
            //message.IsBodyHtml = true;

            //sFirst = "-//W3C//DTD XHTML 1.0 Transitional//EN             ";
            //sLine = sLine + " <!DOCTYPE HTML PUBLIC '" + sFirst + "'>    ";
            //sLine = sLine + " <html>                                     ";
            //sLine = sLine + "    <head>                                  ";
            //sLine = sLine + "       <title> 구매 요청 화면 </title>      ";
            //sLine = sLine + "    </head>                                 ";
            //sLine = sLine + "    <body>                                  ";
            //sLine = sLine + "      <table width = 500px height = 150px border = 1 cellpadding = 0 cellspacing = 0 > ";
            //sLine = sLine + "         <tr>                               ";
            //sLine = sLine + "            <td align = 'Left'>구   분      ";
            //sLine = sLine + "            </td>                           ";
            //sLine = sLine + "            <td align = 'Left'>내   용      ";
            //sLine = sLine + "            </td>                           ";
            //sLine = sLine + "         </tr>                              ";
            //sLine = sLine + "         <tr>                               ";
            //sLine = sLine + "            <td ALIGN = 'Left'>요청 번호    ";
            //sLine = sLine + "            </td>                           ";
            //sLine = sLine + "            <td ALIGN = 'Left'>" + sPRMNUM.ToString() + " ";
            //sLine = sLine + "            </td>                           ";
            //sLine = sLine + "         </tr>                              ";
            //sLine = sLine + "         <tr>                               ";
            //sLine = sLine + "            <td ALIGN = 'Left'>구 매 명     ";
            //sLine = sLine + "            </td>                           ";
            //sLine = sLine + "            <td ALIGN = 'Left'>" + this.TXT01_PRM2120.GetValue().ToString() + " ";
            //sLine = sLine + "            </td>                           ";
            //sLine = sLine + "         </tr>                              ";
            //sLine = sLine + "      </table>                              ";
            //sLine = sLine + "    </body>                                 ";
            //sLine = sLine + " </html>                                    ";

            //message.Body = sLine.ToString();

            //SmtpClient client = new SmtpClient("gw.taeyoung.co.kr");
            ////SmtpClient client = new SmtpClient("192.168.100.72");
            //client.Send(message);







            string sMailFrom = string.Empty;
            string sMailTo   = string.Empty;

            string sPRMNUM   = string.Empty;

            sPRMNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            DataTable dt = new DataTable();

            // 발신자
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35T1I774",
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sMailFrom = dt.Rows[0]["KBMAILID"].ToString() + "@taeyoung.co.kr";
            }


            string sLine = "";

            MailMessage mess = new MailMessage();

            //mess.From = sMailFrom.ToString();
            mess.From = "appr_admin@taeyoung.co.kr";
            mess.To = "hjyoon@taeyoung.co.kr";

            mess.Subject = "구매요청 승인제출건 - 요청번호:" + sPRMNUM.ToString();

            try
            {

                sLine = "";
                sLine += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' > ";
                sLine += "<HTML>";
                sLine += "<HEAD>";
                sLine += "<TITLE> New Document </TITLE>";
                sLine += "<style type='text/css'>";
                sLine += "<!-- /* Global Css */ ";
                sLine += "body {background-color:#ffffff;margin:0 10px 0 10px;}";
                sLine += "body,td,input,textarea,select{color:#333333;font-family:굴림,Gulim,sans-serif;font-size:12px}";
                sLine += "img{border:none} --> </style> </HEAD>";
                sLine += "<BODY>";
                sLine += "<table width= 400 cellspacing=2 cellpadding=1 border=0.5>";
                sLine += "<tr>";
                sLine += "      <td colspan=5 style='height: 12px'></td></tr>";
                sLine += "<tr>";
                sLine += "      <td colspan=4 bgcolor=#D6C7B5 style='height: 40px; font-weight: bold; font-size: 12pt; color: #cc0033; font-family: 굴림;' align='center'>";
                sLine += "      ERP 구매요청 승인제출";
                sLine += "      </td>";
                sLine += "</tr>";
                sLine += "<tr bgcolor=#E7E3DE align=center>";
                sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 84px;'>요청번호</td>";
                sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 150px;'>구매명</td>";                
                sLine += "</tr>";
                sLine += "<tr bgcolor=#F7F7F7 align=center>";
                sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 84px;'> " + sPRMNUM.ToString() + "</td>";
                sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 150px;'> " + this.TXT01_PRM2120.GetValue().ToString() + " </td>";
                sLine += "</tr>";
                sLine += "</table>";
                sLine += "</BODY>";
                sLine += "</HTML> ";

                mess.Body = sLine;
                mess.BodyFormat = MailFormat.Html;

                SmtpMail.SmtpServer = "mail.taeyoung.co.kr";

                // 메일 발송
                SmtpMail.Send(mess);
            }
            catch
            {
                return;
            }

            return;
        }
        #endregion

        private void TXT01_PRM2120_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
                else if (this.BTN61_EDIT.Visible == true)
                {
                    SetFocus(this.BTN61_EDIT);
                }
            }
        }

        #region Description : 자산분류코드
        private void CBH01_PRN6020_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string sPRN6020NM_JASAN  = string.Empty;
            string sPRN6020NM_SAUPBU = string.Empty;
            string sPRN6020NM_LARGE  = string.Empty;
            string sPRN6020NM_MIDDLE = string.Empty;
            string sPRN6020NM_SMALL  = string.Empty;
            string sPRN6020NM = string.Empty;

            if (this.CBH01_PRN6020.GetValue().ToString() != "")
            {
                DataTable dt = new DataTable();

                // 적용계정명
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BM1D571",
                    this.CBH01_PRN6020.GetValue().ToString().Substring(0, 1),
                    this.CBH01_PRN6020.GetValue().ToString().Substring(0, 1),
                    this.CBH01_PRN6020.GetValue().ToString().Substring(1,10),
                    ""
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sPRN6020NM_JASAN  = dt.Rows[0]["ASDESC1"].ToString();
                    sPRN6020NM_SAUPBU = dt.Rows[0]["CDCODE"].ToString();
                    sPRN6020NM_LARGE  = dt.Rows[0]["FXLMDESC"].ToString();
                    sPRN6020NM_MIDDLE = dt.Rows[0]["FXMMDESC"].ToString();
                    sPRN6020NM_SMALL  = dt.Rows[0]["FXSMDESC"].ToString();

                    sPRN6020NM = sPRN6020NM_JASAN.ToString() + "-" + sPRN6020NM_SAUPBU + "-" + sPRN6020NM_LARGE.ToString() + "-" + sPRN6020NM_MIDDLE.ToString();
                }

                this.TXT01_PRN60201NM.SetValue(sPRN6020NM.ToString());

            }
        }
        #endregion

        private void BTN61_UTTCODEHELP9_Click(object sender, EventArgs e)
        {
            TYMRCO006I popup = new TYMRCO006I("","","","","","","");

            popup.ShowDialog();
        }

        #region Description : 청구거래처 입력 이벤트
        private void CBH01_PRN1100_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (CBH01_PRN1100.GetValue().ToString().Length == 6)
            {
                // 최근 구매정보 조회 후 부가세 구분 자동입력
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_BCAAT904",
                    this.CBH01_PRN1100.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_PRN1110.SetValue(dt.Rows[0]["PRN1110"].ToString());
                }
            }
        }
        #endregion
    }
}