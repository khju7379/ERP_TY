using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_71VAB604 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUTIN034B : TYBase
    {
        private double fdAMOUNT   = 0;

        private string fsIPHANG   = string.Empty;
        private string fsBONSUN   = string.Empty;
        private string fsHWAJU    = string.Empty;
        private string fsHJDESC1  = string.Empty;
        private string fsHWAMUL   = string.Empty;
        private string fsHMDESC1  = string.Empty;
        private string fsTANKNO   = string.Empty;
        private string fsBLNO     = string.Empty;
        private string fsMSNSEQ   = string.Empty;
        private string fsHSNSEQ   = string.Empty;
        private string fsCUSTIL   = string.Empty;
        private string fsCHASU    = string.Empty;

        private string fsCMHWAPE  = string.Empty;
        private string fsCMFACT   = string.Empty;

        private string fsSVBIJUNG = string.Empty;
        private string fsSVMOGB   = string.Empty;
        private string fsSHOREQTY = string.Empty;

        private string fsCOCONTNO = string.Empty;
        private string fsCOOVAM   = string.Empty;

        private string fsIPSINOYY = string.Empty;
        private string fsIPSINO   = string.Empty;

        private string fsSEQCH    = string.Empty;
        private string fsSEQGB    = string.Empty;


        #region Description : 페이지 로드
        public TYUTIN034B()
        {
            InitializeComponent();
        }

        private void TYUTIN034B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SEL.ProcessCheck += new TButton.CheckHandler(BTN61_SEL_ProcessCheck);
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            this.FPS91_TY_S_UT_71VAB604.Initialize();
            this.FPS91_TY_S_UT_71VHY614.Initialize();

            SetStartingFocus(this.DTP01_STDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.TXT01_IPQTY_HAP.SetValue("0");
            this.TXT01_IPMTQTY.SetValue("0");

            fdAMOUNT = 0;

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_71VAB604.Initialize();
            this.FPS91_TY_S_UT_71VHY614.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_CARFF148",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString(),
                this.TXT01_CJTANKNO1.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_71VAB604.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_UT_71VAB604.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJJISINO1").ToString() == "입고")
                {
                    // 특정 ROW 색깔 입히기
                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[i, 18].BackColor = Color.Thistle;
                }
            }
        }
        #endregion

        #region Description : 선택 버튼
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            int i = 0;
            int iRow = 0;

            this.FPS91_TY_S_UT_71VHY614.Initialize();

            for (i = 0; i < this.FPS91_TY_S_UT_71VAB604.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[i, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["GUBUN"].Index].CellType != null)
                {
                    this.FPS91_TY_S_UT_71VHY614_Sheet1.AddRows(iRow, 1);

                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJCHULIL",  this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJCHULIL").ToString());

                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJTKNO",    this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJTKNO").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJHWAJU",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJHWAJU").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "HJDESC1",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "HJDESC1").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJHWAMUL",  this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJHWAMUL").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "HMDESC1",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "HMDESC1").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJTANKNO1", this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJTANKNO1").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJCARNO",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJCARNO").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJCONTAIN", this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJCONTAIN").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJSEALNUM", this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJSEALNUM").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJSOSOK",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJSOSOK").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "SKDESC1",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "SKDESC1").ToString());

                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJIPTIME",  this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJIPTIME").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJCHTIME",  this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJCHTIME").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJIPQTY",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJIPQTY").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJEMPTY",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJEMPTY").ToString());
                    this.FPS91_TY_S_UT_71VHY614.SetValue(iRow, "CJTOTAL",   this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJTOTAL").ToString());

                    iRow++;

                    this.CBH01_SVHWAJU.SetValue(this.FPS91_TY_S_UT_71VAB604.GetValue(i,  "CJHWAJU").ToString());
                    this.CBH01_SVHWAMUL.SetValue(this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJHWAMUL").ToString());
                    this.TXT01_SVTANKNO.SetValue(this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJTANKNO1").ToString());
                }
            }

            for (i = 0; i < this.FPS91_TY_S_UT_71VHY614.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_UT_71VHY614.ActiveSheet.Cells[i, this.FPS91_TY_S_UT_71VHY614.ActiveSheet.Columns["CJIPQTY"].Index].Font = new Font("굴림", 9);
            }

            this.CBO01_GGUBUN.SetValue("I");

            SetFocus(this.CBH01_CMBONSUN.CodeText);
        }
        #endregion

        #region Description : 생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sMTQTY     = string.Empty;
            string sKLQTY     = string.Empty;
            string sPUSTR     = string.Empty;
            string sPUEND     = string.Empty;
            string sCOSTR1    = string.Empty;
            string sCOEND1    = string.Empty;
            string sCOSTR2    = string.Empty;
            string sCOEND2    = string.Empty;
            string sCOSTR3    = string.Empty;
            string sCOEND3    = string.Empty;
            string sSABUN     = string.Empty;

            string sVESLGLOS  = string.Empty;
            string sVESLAJET  = string.Empty;

            string sCMBANIL   = string.Empty;
            string sCMOBQTY   = string.Empty;
            string sCMHYUKGB  = string.Empty;
            string sCMHYQTY   = string.Empty;
            string sCMBLQTY   = string.Empty;
            string sCMBOGODAT = string.Empty;

            string sSVBIJUNG  = string.Empty;
            string sSVVCF     = string.Empty;
            string sSVGUMJUNG = string.Empty;

            string sBLNO      = string.Empty;
            string sMSNSEQ    = string.Empty;


            string sCOOVQTY  = string.Empty;

            int i = 0;




            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_721ED631",
                this.CBH01_CMBONSUN.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sVESLGLOS = dt.Rows[0]["VESLGLOS"].ToString();
                sVESLAJET = dt.Rows[0]["VESLAJET"].ToString();
            }

            // 입고일자
            sCMBANIL   = Get_Date(this.DTP01_CMIPHANG.GetValue().ToString());
            // O/B량
            sCMOBQTY   = Get_Numeric(this.TXT01_CMOBQTY.GetValue().ToString());
            // B/L량
            sCMBLQTY = Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString());
            // 취급년월
            sCMHYUKGB  = this.TXT01_CMHYUKGB.GetValue().ToString();
            // 반입일자
            sCMBOGODAT = Get_Date(this.DTP01_CMIPHANG.GetValue().ToString());

            // 취급량
            if (this.TXT01_CMHYUKGB.GetValue().ToString().ToUpper() == "NO")
            {
                sCMHYQTY = "0";
            }
            else
            {
                sCMHYQTY = Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString());
            }

            // SHORE량
            sMTQTY = Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString());
            // KL량
            sKLQTY   = Get_Numeric(this.TXT01_CMKLQTY.GetValue().ToString());

            // 펌프 시작시간
            sPUSTR   = Set_Fill2(this.TXT01_CMPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUSTR2.GetValue().ToString());
            // 펌프 종료시간
            sPUEND   = Set_Fill2(this.TXT01_CMPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUEND2.GetValue().ToString());

            // 할증 시작시간1
            sCOSTR1 = Set_Fill2(this.TXT01_COOVSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR2.GetValue().ToString());
            // 할증 종료시간1
            sCOEND1 = Set_Fill2(this.TXT01_COOVEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND2.GetValue().ToString());

            // 할증 시작시간2
            sCOSTR2 = "0000";
            // 할증 종료시간2
            sCOEND2 = "0000";

            // 할증 시작시간3
            sCOSTR3 = "0000";
            // 할증 종료시간3
            sCOEND3 = "0000";

            // 할증량
            sCOOVQTY = Get_Numeric(this.TXT01_COOVQTY.GetValue().ToString());

            // 사용자
            sSABUN = TYUserInfo.EmpNo.ToString().Trim().ToUpper();


            // B/L번호
            sBLNO   = fsTANKNO.ToString().Trim();
            // MSN번호
            sMSNSEQ = fsTANKNO.ToString().Trim();

            // SURVEY - 검정사 가져오기(화주, 화물이 동일하면서 제일 마지막에 등록된 데이터 가져오기)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_77RCJ282",
                fsHWAJU.ToString(),
                fsHWAMUL.ToString(),
                this.DTP01_CMIPHANG.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVGUMJUNG = dt.Rows[0]["SVGUMJUNG"].ToString();
            }

            if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() == "1" && fsIPSINO != "0")
            {
                // 입고번호 체크
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_67JD3845",
                                        Get_Numeric(fsIPSINOYY.ToString()),
                                        Get_Numeric(fsSEQCH.ToString()));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_67JD6846");

                    return;
                }
            }

            // 20180112 고지파트에서 입항관리에 자료가 존재할 경우 등록 안되도록 요청
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66LC1314",
                fsIPHANG.ToString(),
                fsBONSUN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.DbConnector.CommandClear();

                // 선박입항관리 등록
                this.DbConnector.Attach("TY_P_UT_66LHE338",
                                        fsIPHANG.ToString(),         // 입항일자
                                        fsBONSUN.ToString(),         // 본선
                                        sVESLGLOS.ToString(),        // G/T
                                        sVESLAJET.ToString(),        // 대리점
                                        "0",                         // 외항입항일자
                                        "0000",                      // 입항시간
                                        "0",                         // 출항일자
                                        "0000",                      // 출항시간
                                        "",                          // 선박구분
                                        "",                          // 화물구분
                                        "",                          // 접안장소
                                        "",                          // 선원국적
                                        "",                          // 입항세관
                                        "",                          // 반입근거번호
                                        "",                          // 적하목록번호
                                        sSABUN.ToString()            // 작성사번
                                        );

                this.DbConnector.ExecuteTranQueryList();
            }


            
            this.DbConnector.CommandClear();

            // 김종술 과장 요청 입고화물 등록시 K/L 량 0으로 표기
            // 입고화물관리 등록
            this.DbConnector.Attach("TY_P_UT_66SEW433",
                                    fsIPHANG.ToString(),         // 입항일자
                                    fsBONSUN.ToString(),         // 본선
                                    fsHWAJU.ToString(),          // 화주
                                    fsHWAMUL.ToString(),         // 화물
                                    sCMBANIL.ToString(),         // 입고일자
                                    "",                          // 입고구분
                                    "",                          // 선상구분
                                    sCMBLQTY.ToString(),         // B/L
                                    sCMOBQTY.ToString(),         // O/B
                                    sMTQTY.ToString(),           // SHORE량
                                    "0",                         // BBLS량
                                    //sKLQTY.ToString(),           // K/L량
                                    "0",                         // K/L량
                                    "0",                         // 하역비
                                    "0",                         // 화물료
                                    "0",                         // 청구일자
                                    "0000",                      // HOST시작시간
                                    "0000",                      // HOST종료시간
                                    sPUSTR.ToString(),           // PUMP시작시간
                                    sPUEND.ToString(),           // PUMP종료시간
                                    "0",                         // TON/H
                                    "30",                        // 보험등급
                                    "0",                         // 보험공제일수
                                    "",                          // 보험번호
                                    sCMBOGODAT.ToString(),       // 보험반입일자
                                    "OK",                        // 보험반입사고
                                    fsTANKNO.ToString(),         // 탱크1
                                    "",                          // 탱크2
                                    "",                          // 탱크3
                                    "",                          // 탱크4
                                    "",                          // 탱크5
                                    "",                          // 탱크6
                                    "",                          // 탱크7
                                    "",                          // 탱크8
                                    "",                          // 탱크9
                                    "",                          // 탱크10
                                    "0",                         // 품목코드
                                    sCMHYUKGB.ToString(),        // 취급월
                                    sCMHYQTY,                    // 취급량
                                    sSABUN.ToString()            // 작성사번
                                    );
            
            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_675B7565",
                fsTANKNO.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVBIJUNG = dt.Rows[0]["TNBIJUNG"].ToString();
                sSVVCF    = dt.Rows[0]["TNVCF"].ToString();
            }

            // SURVEY 등록(자동화 포함)
            UP_UTISURVF_Batch("ADD",
                              fsIPHANG.ToString(),
                              fsBONSUN.ToString(),
                              fsHWAJU.ToString(),
                              fsHWAMUL.ToString(),
                              fsHMDESC1.ToString(),
                              sSVGUMJUNG.ToString(),            // 검정사
                              Set_TankNo(fsTANKNO.ToString()),  // 탱크
                              sMTQTY.ToString(),                // MT량
                              sKLQTY.ToString(),                // KL량
                              sPUSTR,                           // 펌프시간
                              sPUEND,                           // 펌프종료
                              sCOSTR1,                          // 할증시작1
                              sCOEND1,                          // 할증종료1
                              "0000",                           // 할증시작2
                              "0000",                           // 할증종료2
                              "0000",                           // 할증시작3
                              "0000",                           // 할증종료3
                              sCOOVQTY.ToString(),              // 할증량
                              fsCOCONTNO.ToString(),            // 계약번호
                              sSVBIJUNG.ToString(),             // 비중
                              sSVVCF.ToString(),                // 비중
                              "15",                             // 온도
                              "C",                              // 온도구분
                              "1",                              // 계산방법
                              "",                               // 이고구분
                              "",                               // 이고TANK
                              Get_Numeric(fsCOOVAM.ToString()), // 할증금액
                              fsHJDESC1.ToString(),             // 화주명
                              Get_Date(DateTime.Now.ToString("yyyy-MM-dd").ToString()), // 일자
                              sSABUN.ToString()                                         // 사번
                              );


            // B/L별 입고 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67JF6862",
                                    fsIPHANG.ToString(),
                                    fsBONSUN.ToString(),
                                    fsHWAJU.ToString(),
                                    fsHWAMUL.ToString(),
                                    sBLNO,                                                 // B/L번호
                                    sMSNSEQ,                                               // MSN
                                    "0",                                                   // HSN
                                    "",                                                    // 포장단위
                                    "0",                                                   // 반입갯수
                                    "",                                                    // H-BLNO
                                    Get_Numeric(sMTQTY.ToString()),                        // M/T량
                                    Get_Numeric(this.TXT01_CMKLQTY.GetValue().ToString()), // K/L량
                                    Get_Numeric(sMTQTY.ToString()),                        // B/S량
                                    "0",                                                   // 출고량
                                    Get_Numeric(sMTQTY.ToString()),                        // 통관량
                                    this.fsHWAJU.ToString().ToUpper(),                     // 통관화주
                                    Get_Numeric(fsIPSINOYY.ToString()),                    // 입고번호년도
                                    Get_Numeric(fsIPSINO.ToString()),                      // 입고번호
                                    this.TXT01_IPCHANGGO.GetValue().ToString(),            // 창고구분
                                    this.CBH01_IPHWAMULGB.GetValue().ToString().ToUpper(), // 화물구분
                                    this.CBH01_IPBANGUBUN.GetValue().ToString(),           // 반입구분
                                    "",                                                    // 분할반출체크
                                    "0",                                                   // 분할반출일자
                                    "0",                                                   // 분할반출수량
                                    "0",                                                   // 이전 HSN
                                    sSABUN.ToString()                                      // 작성사번    
                                    );

            if (fsSEQCH.ToString() != "1") // 업데이트
            {
                this.DbConnector.Attach("TY_P_UT_67JF7864",
                                        fsSEQCH.ToString(),
                                        "I",
                                        fsSEQGB.ToString(),
                                        this.fsIPSINOYY.ToString()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_67JF6863",
                                        "I",
                                        fsSEQGB.ToString(),
                                        this.fsIPSINOYY.ToString(),
                                        fsSEQCH.ToString()
                                        );
            }

            // 통관파일 등록
            this.DbConnector.Attach("TY_P_UT_689BM998",
                                    fsIPHANG.ToString(),          // 입항일자
                                    fsBONSUN.ToString(),          // 본선
                                    fsHWAJU.ToString(),           // 화주
                                    fsHWAMUL.ToString(),          // 화물
                                    fsBLNO.ToString(),            // B/L번호
                                    fsMSNSEQ.ToString(),          // MSN
                                    fsHSNSEQ.ToString(),          // HSN
                                    fsCUSTIL.ToString(),          // 통관일자
                                    fsCHASU.ToString(),           // 통관차수
                                    sMTQTY.ToString(),            // 통관량
                                    "0",                          // 출고량
                                    "0",                          // 감정가($)
                                    "0",                          // 감정가(\)
                                    "",                           // 신고사항(신고번호)
                                    sMTQTY.ToString(),            // 신고수량
                                    "",                           // 관세사
                                    fsHWAJU.ToString(),           // 통관화주
                                    "43",                         // 반출구분
                                    "0",                          // 출고종료일자
                                    "",                           // 반출기한근거번호
                                    "0",                          // 반출연장시작일자
                                    "0",                          // 반출연장종료일자
                                    sSABUN.ToString()             // 작성사번
                                    );

            // 통관화주파일 등록
            this.DbConnector.Attach("TY_P_UT_689CM001",
                                    fsHWAJU.ToString(),           // 통관화주
                                    fsHWAJU.ToString(),           // 재고화주
                                    fsIPHANG.ToString(),          // 입항일자
                                    fsBONSUN.ToString(),          // 본선
                                    fsHWAJU.ToString(),           // 화주
                                    fsHWAMUL.ToString(),          // 화물
                                    fsBLNO.ToString(),            // B/L번호
                                    fsMSNSEQ.ToString(),          // MSN
                                    fsHSNSEQ.ToString(),          // HSN
                                    fsCUSTIL.ToString(),          // 통관일자
                                    fsCHASU.ToString(),           // 통관차수
                                    "",                           // 양수화주
                                    "",                           // 양도화주
                                    "0",                          // 양수일자
                                    "0",                          // 양도차수
                                    "0",                          // 양수순번
                                    "0",                          // 양도량
                                    "0",                          // 양수량
                                    "0",                          // 양수분양도량
                                    "0",                          // 양수출고량
                                    sMTQTY.ToString(),            // 통관량
                                    "0",                          // 출고량
                                    sMTQTY.ToString(),            // 재고량
                                    sSABUN.ToString()             // 작성사번
                                    );


            if (this.CBO01_GGUBUN.GetValue().ToString() == "I") // 입고일 경우에만 적용
            {
                for (i = 0; i < this.FPS91_TY_S_UT_71VHY614.ActiveSheet.RowCount; i++)
                {
                    // 강제계근 입고 업데이트 - 입항일자, 본선
                    this.DbConnector.Attach("TY_P_UT_722IC636",
                                            fsIPHANG.ToString(),                                                   // 입항일자
                                            fsBONSUN.ToString(),                                                   // 본선
                                            this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJCHULIL").ToString().Trim(), // 일자
                                            this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJTKNO").ToString().Trim()    // 순번
                                            );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_UT_721E4630"); // 저장 메세지
        }
        #endregion

        #region Description : BATCH 처리
        private void UP_UTISURVF_Batch(string sWORKGN,    string sSVIPHANG,  string sSVBONSUN,
                                       string sSVHWAJU,   string sSVHWAMUL,  string sHMDESC1,
                                       string sSVGUMJUNG, string sSVTANKNO,  string sSVMTQTY,
                                       string sSVKLQTY,   string sCOPUSTR,   string sCOPUEND,
                                       string sCOOVSTR1,  string sCOOVEND1,  string sCOOVSTR2,
                                       string sCOOVEND2,  string sCOOVSTR3,  string sCOOVEND3,
                                       string sCOOVQTY,   string sCOCONTNO,  string sSVBIJUNG,
                                       string sSVVCF,     string sSVTEMP,    string sSVTEMPGB,
                                       string sSVKESANGB, string sSVMOGB,    string sSVMOTA,
                                       string sCOOVAM,    string sHJDESC1,   string sDATE,
                                       string sSABUN)
        {
            string sKBHANGL = string.Empty;
			string sVSHMGB  = string.Empty;
			string sCHECK   = string.Empty;
			string sWATER   = string.Empty;
			string sJSSEQ   = string.Empty;
			string sGUBUN   = string.Empty;

            DataTable dt = new DataTable();

            // UTISURVF
            if (sWORKGN.ToString() == "ADD")
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677FJ645",
                                        sSVIPHANG.ToString(),
                                        sSVBONSUN.ToString(),
                                        sSVHWAJU.ToString(),
                                        sSVHWAMUL.ToString(),
                                        sSVTANKNO.ToString(),
                                        sSVGUMJUNG.ToString(),
                                        sSVMTQTY.ToString(),
                                        sSVKLQTY.ToString(),
                                        sSVBIJUNG.ToString(),
                                        sSVVCF.ToString(),
                                        sSVTEMP.ToString(),
                                        sSVTEMPGB.ToString(),
                                        sSVKESANGB.ToString(),
                                        sSVMOGB.ToString(),
                                        sSVMOTA.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // 화물비중화일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677D4619",
                sSVTANKNO.ToString().Trim(),
                sSVHWAMUL.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            // UTAHMBDF
            if (sGUBUN == "INS")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DD620",
                                        sSVTANKNO,
                                        sSVHWAMUL,
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DE621",
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG,
                                        sSVTANKNO.ToString().Trim(),
                                        sSVHWAMUL
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // 화물비중 파일 Check
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677DN623",
                sSVHWAMUL.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            // UTAHMBJF
            if (sGUBUN == "INS")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DR631",
                                        sSVHWAMUL,
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677DQ630",
                                        sHMDESC1,
                                        sSVVCF,
                                        sSVBIJUNG,
                                        sSVHWAMUL
                                        );

                this.DbConnector.ExecuteNonQuery();
            }




            if (sSVTANKNO.ToString().Trim() != "5007")
            {
                // TANK 지시 번호 자동부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677DT632",
                    sDATE
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sGUBUN = "UPT";
                }
                else
                {
                    sGUBUN = "INS";
                }

                // UTAJINOF
                if (sGUBUN == "INS")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677DX634",
                                            sDATE
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677DY635",
                                            sDATE
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }

                // 사원명을 가져옴
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677ES637",
                    TYUserInfo.EmpNo.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sKBHANGL = dt.Rows[0]["KBHANGL"].ToString();
                }

                // 탱크 지시순번을 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677DT632",
                    sDATE
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sJSSEQ = dt.Rows[0]["JSSEQ"].ToString();
                }

                // 탱크지시 (일자,지시순번)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677F9639",
                    sDATE.ToString(),
                    sJSSEQ
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sGUBUN = "UPT";
                }
                else
                {
                    sGUBUN = "INS";
                }

                // UTATKJIF
                if (sGUBUN == "INS")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677F5641",
                                            sDATE,
                                            sJSSEQ,
                                            sSVHWAMUL,
                                            sHMDESC1,
                                            sSABUN,
                                            sKBHANGL,
                                            sSVTANKNO,
                                            sSVBIJUNG,
                                            sSVVCF
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677FD643",
                                            sSVHWAMUL,
                                            sHMDESC1,
                                            sSABUN,
                                            sKBHANGL,
                                            sSVTANKNO,
                                            sSVBIJUNG,
                                            sSVVCF,
                                            sDATE,
                                            sJSSEQ
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }

            // UTICOMEF(매출입고할증파일) 저장
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677FK646",
                sSVIPHANG,
                sSVBONSUN,
                sSVHWAJU,
                sSVHWAMUL,
                sSVTANKNO.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            #region Description : 작업시간 및 할증작업시간 구하기

            decimal dSVMTQTY = 0;
            string sCOWKTIME  = string.Empty;
            string sCOOWKTIME = string.Empty;

            

            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sStartTime = string.Empty;
            string sEndTime = string.Empty;

            string sENDATE = string.Empty;

            double dAppTime = 0;

            sSTDATE = this.DTP01_CMIPHANG.GetValue().ToString();
            sEDDATE = this.DTP01_CMIPHANG.GetValue().ToString();

            sENDATE = this.DTP01_CMIPHANG.GetValue().ToString();

            
            // 20180119일 고지파트 박선형 주임 
            //#region Description : 작업시간 구하기

            //sStartTime = "";
            //sEndTime = "";
            //string sParamDate = string.Empty;

            //DateTime dt_End = new DateTime();

            //sStartTime = "";
            //sEndTime   = "";

            //if (Set_Fill2(this.TXT01_CMPUSTR1.GetValue().ToString()) == "24")
            //{
            //    sStartTime = "00" + Set_Fill2(this.TXT01_CMPUSTR2.GetValue().ToString());
            //}
            //else
            //{
            //    sStartTime = Set_Fill2(this.TXT01_CMPUSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUSTR2.GetValue().ToString());
            //}

            //if (Set_Fill2(this.TXT01_CMPUEND1.GetValue().ToString()) == "24")
            //{
            //    sEndTime = "00" + Set_Fill2(this.TXT01_CMPUEND2.GetValue().ToString());
            //}
            //else
            //{
            //    sEndTime = Set_Fill2(this.TXT01_CMPUEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CMPUEND2.GetValue().ToString());
            //}

            //if (sStartTime.ToString() != "0000" && sEndTime.ToString() != "0000")
            //{
            //    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
            //    {
            //        sParamDate = sEDDATE.ToString().Substring(0, 4) + "-" +
            //                     sEDDATE.ToString().Substring(4, 2) + "-" +
            //                     sEDDATE.ToString().Substring(6, 2);

            //        dt_End = Convert.ToDateTime(sParamDate.ToString());

            //        dt_End = dt_End.AddDays(+1);

            //        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
            //    }

            //    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
            //    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);

            //    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

            //    dAppTime = Convert.ToDouble(timeSpan.TotalMinutes.ToString());
            //}

            //sCOWKTIME = Convert.ToString(dAppTime);

            //#endregion

            //#region Description : 할증작업시간 구하기

            //dAppTime = 0;

            //sStartTime = "";
            //sEndTime = "";


            //if (Set_Fill2(this.TXT01_COOVSTR1.GetValue().ToString()) == "24")
            //{
            //    sStartTime = "00" + Set_Fill2(this.TXT01_COOVSTR2.GetValue().ToString());
            //}
            //else
            //{
            //    sStartTime = Set_Fill2(this.TXT01_COOVSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVSTR2.GetValue().ToString());
            //}

            //if (Set_Fill2(this.TXT01_COOVEND1.GetValue().ToString()) == "24")
            //{
            //    sEndTime = "00" + Set_Fill2(this.TXT01_COOVEND2.GetValue().ToString());
            //}
            //else
            //{
            //    sEndTime = Set_Fill2(this.TXT01_COOVEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_COOVEND2.GetValue().ToString());
            //}
            
            //if (sStartTime.ToString() != "0000" && sEndTime.ToString() != "0000")
            //{
            //    if (int.Parse(sStartTime.ToString()) >= int.Parse(sEndTime.ToString()))
            //    {
            //        sParamDate = sEDDATE.ToString().Substring(0, 4) + "-" +
            //                     sEDDATE.ToString().Substring(4, 2) + "-" +
            //                     sEDDATE.ToString().Substring(6, 2);

            //        dt_End = Convert.ToDateTime(sParamDate.ToString());

            //        dt_End = dt_End.AddDays(+1);

            //        sENDATE = Convert.ToString(dt_End.Year) + Set_Fill2(Convert.ToString(dt_End.Month)) + Set_Fill2(Convert.ToString(dt_End.Day));
            //    }

            //    DateTime dtLatetime1 = new DateTime(int.Parse(sSTDATE.Substring(0, 4)), int.Parse(sSTDATE.Substring(4, 2)), int.Parse(sSTDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
            //    DateTime dtLatetime2 = new DateTime(int.Parse(sENDATE.Substring(0, 4)), int.Parse(sENDATE.Substring(4, 2)), int.Parse(sENDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);

            //    TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

            //    dAppTime = Convert.ToDouble(timeSpan.TotalMinutes.ToString());
            //}

            //sCOOWKTIME = Convert.ToString(dAppTime);

            //#endregion

            //// 할증량 구하기
            //// 할증량 = (MT * 총할증작업시간) * 총작업시간
            //decimal dCOWKTIME  = 0;
            //decimal dCOOWKTIME = 0;
            //decimal dCOOVQTY   = 0;

            //// 총작업시간
            //dCOWKTIME = decimal.Parse(Get_Numeric(sCOWKTIME.ToString()));
            //// 할증작업시간
            //dCOOWKTIME = decimal.Parse(Get_Numeric(sCOOWKTIME.ToString()));

            //// MT
            //dSVMTQTY = decimal.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString()));

            //// 할증량 = (MT * 총할증작업시간) * 총작업시간
            //dCOOVQTY = (Math.Round(((dSVMTQTY * dCOOWKTIME) / dCOWKTIME), 3) * 1000);
            //dCOOVQTY = decimal.Parse(UP_DotDelete(Convert.ToString(dCOOVQTY)));

            //// 할증량 구하기
            //dCOOVQTY = dCOOVQTY / 1000;

            //// 할증량
            //this.TXT01_COOVQTY.SetValue(Convert.ToString(dCOOVQTY));

            #endregion

            // UTICOMEF
            if (sGUBUN == "UPT")
            {
                if (fsSVMOGB.ToString() == "" || sSVMOGB.ToString() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677FO648",
                                            sSVMTQTY,
                                            sSVKLQTY,
                                            sCOCONTNO,
                                            sCOPUSTR,
                                            sCOPUEND,
                                            "0000",
                                            "0000",
                                            "0000",
                                            "0000",
                                            sCOWKTIME.ToString(),
                                            sCOOVSTR1,
                                            sCOOVEND1,
                                            sCOOVSTR2,
                                            sCOOVEND2,
                                            sCOOVSTR3,
                                            sCOOVEND3,
                                            sCOOWKTIME.ToString(),
                                            sCOOVQTY,
                                            sCOOVAM,
                                            sSABUN,
                                            sSVIPHANG,
                                            sSVBONSUN,
                                            sSVHWAJU,
                                            sSVHWAMUL,
                                            sSVTANKNO.ToString().Trim()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                    
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677FR650",
                                        sSVIPHANG,
                                        sSVBONSUN,
                                        sSVHWAJU,
                                        sSVHWAMUL,
                                        sSVTANKNO,
                                        sSVMTQTY,
                                        sSVKLQTY,
                                        sCOCONTNO,
                                        sCOPUSTR,
                                        sCOPUEND,
                                        "0000",
                                        "0000",
                                        "0000",
                                        "0000",
                                        sCOWKTIME.ToString(),
                                        sCOOVSTR1,
                                        sCOOVEND1,
                                        sCOOVSTR2,
                                        sCOOVEND2,
                                        sCOOVSTR3,
                                        sCOOVEND3,
                                        sCOOWKTIME.ToString(),
                                        sCOOVQTY,
                                        sCOOVAM,
                                        sSABUN
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            if (sSVHWAMUL.ToUpper() == "A12")
            {
                sCHECK = "*";
            }

            // 화물구분을 가져옴
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677FT651",
                sSVIPHANG,
                sSVBONSUN
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sVSHMGB = dt.Rows[0]["VSHMGB"].ToString();
            }

            // UTITANKF
            if (sWORKGN.ToString() == "ADD")
            {
                // 화주정보 추가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C1B9E996",
                                        sSVIPHANG,
                                        sSVBONSUN,
                                        sSVHWAJU,
                                        sHJDESC1,
                                        sSVHWAMUL,
                                        sVSHMGB,
                                        sSVBIJUNG,
                                        sSVVCF,
                                        sSVTEMP,
                                        sSVTEMPGB,
                                        sSVKESANGB,
                                        sSABUN,
                                        sSVTANKNO.ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();

                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_UT_677G2654",
                //                        sSVIPHANG,
                //                        sSVBONSUN,
                //                        sSVHWAMUL,
                //                        sVSHMGB,
                //                        sSVBIJUNG,
                //                        sSVVCF,
                //                        sSVTEMP,
                //                        sSVTEMPGB,
                //                        sSVKESANGB,
                //                        sSABUN,
                //                        sSVTANKNO.ToString().Trim()
                //                        );

                //this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677G5655",
                                        sSVBIJUNG,
                                        sSVVCF,
                                        sSVTEMP,
                                        sSVTEMPGB,
                                        sSVKESANGB,
                                        sSABUN,
                                        sSVTANKNO.ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }



            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_675B7565",
                sSVTANKNO.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sWATER = dt.Rows[0]["TNWATER"].ToString();
            }


            // UTISENDF
            if (sCHECK == "*")
            {
                // 8.자동화에 자료 전달
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_677GD656",
                    sSVTANKNO.ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677GG658",
                                            sSVBIJUNG,
                                            sWATER,
                                            sSVHWAJU,
                                            sHJDESC1,
                                            sSVHWAMUL,
                                            sHMDESC1,
                                            sSVTANKNO.ToString().Trim()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677GF657",
                                            sSVTANKNO,
                                            sSVBIJUNG,
                                            sWATER,
                                            sSVHWAJU,
                                            sHJDESC1,
                                            sSVHWAMUL,
                                            sHMDESC1
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }

            if (sSVTANKNO.ToString().Trim() != "5007")
            {
                /* 오라클 자동화 업데이트 */
                UP_Save_Oracle(sDATE, sJSSEQ);
            }
        }
        #endregion

        #region Description : 오라클 자동화 메소드
        private void UP_Save_Oracle(string sJISIIL, string sJSSEQ)
        {
            string sJISIHT = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677GK659",
                sJISIIL.ToString(),
                sJSSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sJISIHT = dt.Rows[i]["JISIHT"].ToString();

                    if (sJISIHT.ToString() == "6")
                    {
                        UP_ChangeDensity
                            (
                            dt.Rows[i]["JISIHM"].ToString(),
                            dt.Rows[i]["JISIHMNM"].ToString(),
                            dt.Rows[i]["JIVCF"].ToString(),
                            dt.Rows[i]["JIWCF"].ToString(),
                            dt.Rows[i]["JISITK"].ToString(),
                            dt.Rows[i]["JISIHJ"].ToString(),
                            dt.Rows[i]["JISIHJNM"].ToString()
                            );
                    }
                }
            }
        }
        #endregion

        #region Description : 오라클 - 비중 변경 메소드
        private void UP_ChangeDensity(string sJISIHM, string sJISIHMNM, string sJIVCF,
                                      string sJIWCF,  string sJISITK,   string sJISIHJ,
                                      string sJISIHJNM)
        {
            string sGUBUN = string.Empty;
            string sHWAMUL = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677GU663",
                sJISIHM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            if (sJISIHM.ToString() == "A27")
            {
                sHWAMUL = "무수초산";
            }
            else
            {
                sHWAMUL = sJISIHMNM.ToString();
            }

            // 오라클 HMBJ
            if (sGUBUN == "INS")
            {
                // 오라클 HMBJ 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677GY666",
                                        sJISIHM.ToString(),
                                        sHWAMUL.ToString(),
                                        sJIWCF.ToString(),
                                        sJIVCF.ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 오라클 HMBJ 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677GW665",
                                        sHWAMUL.ToString(),
                                        sJIWCF.ToString(),
                                        sJIVCF.ToString(),
                                        sJISIHM.ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }


            sGUBUN = "";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677HL669",
                sJISITK.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (sHWAMUL.Length > 14)
                    {
                        sHWAMUL = sHWAMUL.Substring(0, 15).ToString();
                    }

                    // 오라클 TKST 화물 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_677HN670",
                                            sJISIHM.ToString(),
                                            sHWAMUL.ToString(),
                                            sJIWCF.ToString(),
                                            sJIVCF.ToString(),
                                            sJISITK.ToString()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Description : 선택 ProcessCheck
        private void BTN61_SEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sCJCHULIL = string.Empty;
            string sCJTKNO   = string.Empty;
            string sCJHWAJU  = string.Empty;
            string sHJDESC1  = string.Empty;
            string sCJHWAMUL = string.Empty;
            string sHMDESC1  = string.Empty;
            string sCJTANKNO = string.Empty;
            string sCJIPQTY  = string.Empty;

            int i = 0;

            this.FPS91_TY_S_UT_71VHY614.Initialize();

            for (i = 0; i < this.FPS91_TY_S_UT_71VAB604.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[i, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["GUBUN"].Index].CellType != null)
                {
                    if (sCJHWAJU.ToString() == "")
                    {
                        sCJHWAJU = this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJHWAJU").ToString().Trim();
                        sHJDESC1 = this.FPS91_TY_S_UT_71VAB604.GetValue(i, "HJDESC1").ToString().Trim();
                    }

                    if (sCJHWAMUL.ToString() == "")
                    {
                        sCJHWAMUL = this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJHWAMUL").ToString().Trim();
                        sHMDESC1 = this.FPS91_TY_S_UT_71VAB604.GetValue(i, "HMDESC1").ToString().Trim();
                    }

                    if (sCJTANKNO.ToString() == "")
                    {
                        sCJTANKNO = this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJTANKNO1").ToString().Trim();
                    }

                    if (sCJHWAJU.ToString().Trim() != this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJHWAJU").ToString().Trim())
                    {
                        this.ShowMessage("TY_M_UT_721DX626");
                        e.Successed = false;
                        return;
                    }

                    if (sCJHWAMUL.ToString().Trim() != this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJHWAMUL").ToString().Trim())
                    {
                        this.ShowMessage("TY_M_UT_721DY627");
                        e.Successed = false;
                        return;
                    }

                    if (sCJTANKNO.ToString().Trim() != this.FPS91_TY_S_UT_71VAB604.GetValue(i, "CJTANKNO1").ToString().Trim())
                    {
                        this.ShowMessage("TY_M_UT_721DZ628");
                        e.Successed = false;
                        return;
                    }
                }
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_675B7565",
                sCJTANKNO.Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_SVBIJUNG.SetValue(dt.Rows[0]["TNBIJUNG"].ToString());
            }
        }
        #endregion

        #region Description : 생성 ProcessCheck
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sCJCHULIL = string.Empty;
            string sCJTKNO   = string.Empty;
            string sCJHWAJU  = string.Empty;
            string sHJDESC1  = string.Empty;
            string sCJHWAMUL = string.Empty;
            string sHMDESC1  = string.Empty;
            string sCJTANKNO = string.Empty;
            string sCJIPQTY  = string.Empty;

            int i = 0;

            DataTable dt = new DataTable();

           if (int.Parse(Get_Date(this.DTP01_CMIPHANG.GetValue().ToString())) > int.Parse(Get_Date(this.DTP01_CMBANIL.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_66SDD418");
                e.Successed = false;
                return;
            }

           // 시간 유효성 체크
           // PUMP 시작시간
           if ((double.Parse(Set_Fill2(TXT01_CMPUSTR1.GetValue().ToString())) > 24))
           {
               this.ShowMessage("TY_M_UT_676ES590");
               this.TXT01_CMPUSTR1.Focus();

               e.Successed = false;
               return;
           }

           if ((double.Parse(Set_Fill2(TXT01_CMPUSTR2.GetValue().ToString())) > 59))
           {
               this.ShowMessage("TY_M_UT_676ES590");
               this.TXT01_CMPUSTR2.Focus();

               e.Successed = false;
               return;
           }
           // PUMP 종료시간
           if ((double.Parse(Set_Fill2(TXT01_CMPUEND1.GetValue().ToString())) > 24))
           {
               this.ShowMessage("TY_M_UT_676ES592");
               this.TXT01_CMPUEND1.Focus();

               e.Successed = false;
               return;
           }

           if ((double.Parse(Set_Fill2(TXT01_CMPUEND2.GetValue().ToString())) > 59))
           {
               this.ShowMessage("TY_M_UT_676ES592");
               this.TXT01_CMPUEND2.Focus();

               e.Successed = false;
               return;
           }

           // 할증 시작시간
           if ((double.Parse(Set_Fill2(TXT01_COOVSTR1.GetValue().ToString())) > 24))
           {
               this.ShowMessage("TY_M_UT_676GR608");
               this.TXT01_COOVSTR1.Focus();

               e.Successed = false;
               return;
           }

           if ((double.Parse(Set_Fill2(TXT01_COOVSTR2.GetValue().ToString())) > 59))
           {
               this.ShowMessage("TY_M_UT_676GR608");
               this.TXT01_COOVSTR2.Focus();

               e.Successed = false;
               return;
           }
           // 할증 종료시간
           if ((double.Parse(Set_Fill2(TXT01_COOVEND1.GetValue().ToString())) > 24))
           {
               this.ShowMessage("TY_M_UT_676GR609");
               this.TXT01_COOVEND1.Focus();

               e.Successed = false;
               return;
           }

           if ((double.Parse(Set_Fill2(TXT01_COOVEND2.GetValue().ToString())) > 59))
           {
               this.ShowMessage("TY_M_UT_676GR609");
               this.TXT01_COOVEND2.Focus();

               e.Successed = false;
               return;
           }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "I") // 입고
            {
                if (double.Parse(Get_Numeric(this.TXT01_IPQTY_HAP.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_81CGH447");
                    e.Successed = false;
                    return;
                }
                else
                {
                    this.TXT01_IPMTQTY.SetValue(this.TXT01_IPQTY_HAP.GetValue().ToString());
                }

                if (this.CBH01_CMBONSUN.GetValue().ToString().Substring(0, 2) != "TK")
                {
                    this.ShowMessage("TY_M_UT_81F8X449");
                    e.Successed = false;
                    return;
                }
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "G") // 기타입고
            {
                if (this.CBH01_CMBONSUN.GetValue().ToString().Substring(0, 2) != "TK")
                {
                    this.ShowMessage("TY_M_UT_81F8X449");
                    e.Successed = false;
                    return;
                }
            }
            else // 송유
            {
                if (this.CBH01_CMBONSUN.GetValue().ToString().Substring(0, 2) == "TK")
                {
                    this.ShowMessage("TY_M_UT_81F8X449");
                    e.Successed = false;
                    return;
                }

                if (this.CBH01_CMBONSUN.GetValue().ToString().Substring(0, 2) != "PP")
                {
                    this.ShowMessage("TY_M_UT_81F8X449");
                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_UT_676EQ588");
                e.Successed = false;
                return;
            }


            if (this.CBO01_GGUBUN.GetValue().ToString() == "I") // 입고
            {
                if (this.FPS91_TY_S_UT_71VHY614.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_UT_721DD625");
                    e.Successed = false;
                    return;
                }
                else
                {
                    fsIPHANG = "";
                    fsBONSUN = "";
                    fsHWAJU = "";
                    fsHJDESC1 = "";
                    fsHWAMUL = "";
                    fsTANKNO = "";
                    fsHMDESC1 = "";

                    fsBLNO = "";
                    fsMSNSEQ = "0";
                    fsHSNSEQ = "0";
                    fsCUSTIL = "0";
                    fsCHASU = "0";

                    for (i = 0; i < this.FPS91_TY_S_UT_71VHY614.ActiveSheet.RowCount; i++)
                    {
                        if (sCJHWAJU.ToString() == "")
                        {
                            sCJHWAJU = this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJHWAJU").ToString().Trim();
                            sHJDESC1 = this.FPS91_TY_S_UT_71VHY614.GetValue(i, "HJDESC1").ToString().Trim();
                        }

                        if (sCJHWAMUL.ToString() == "")
                        {
                            sCJHWAMUL = this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJHWAMUL").ToString().Trim();
                            sHMDESC1 = this.FPS91_TY_S_UT_71VHY614.GetValue(i, "HMDESC1").ToString().Trim();
                        }

                        if (sCJTANKNO.ToString() == "")
                        {
                            sCJTANKNO = this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJTANKNO1").ToString().Trim();
                        }

                        if (sCJHWAJU.ToString().Trim() != this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJHWAJU").ToString().Trim())
                        {
                            this.ShowMessage("TY_M_UT_721DX626");
                            e.Successed = false;
                            return;
                        }

                        if (sCJHWAMUL.ToString().Trim() != this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJHWAMUL").ToString().Trim())
                        {
                            this.ShowMessage("TY_M_UT_721DY627");
                            e.Successed = false;
                            return;
                        }

                        if (sCJTANKNO.ToString().Trim() != this.FPS91_TY_S_UT_71VHY614.GetValue(i, "CJTANKNO1").ToString().Trim())
                        {
                            this.ShowMessage("TY_M_UT_721DZ628");
                            e.Successed = false;
                            return;
                        }
                    }

                    fsIPHANG = Get_Date(this.DTP01_CMIPHANG.GetValue().ToString());
                    fsBONSUN = this.CBH01_CMBONSUN.GetValue().ToString();
                    fsHWAJU = sCJHWAJU.ToString();
                    fsHJDESC1 = sHJDESC1.ToString();
                    fsHWAMUL = sCJHWAMUL.ToString();
                    fsTANKNO = Set_TankNo(sCJTANKNO.ToString());
                    fsHMDESC1 = sHMDESC1.ToString();

                    fsBLNO = sCJTANKNO.ToString().Trim();
                    fsMSNSEQ = sCJTANKNO.ToString().Trim();
                    fsHSNSEQ = "0";
                    fsCUSTIL = Get_Date(this.DTP01_CMBANIL.GetValue().ToString());
                    fsCHASU = "0";
                }
            }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "I") // 입고
            {
                if (fsHWAJU.ToString().Trim() != this.CBH01_SVHWAJU.GetValue().ToString().Trim())
                {
                    this.ShowMessage("TY_M_UT_71NBQ558");
                    e.Successed = false;
                    return;
                }

                if (fsHWAMUL.ToString().Trim() != this.CBH01_SVHWAMUL.GetValue().ToString().Trim())
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                if (fsTANKNO.ToString().Trim() != this.TXT01_SVTANKNO.GetValue().ToString().Trim())
                {
                    this.ShowMessage("TY_M_UT_676GD601");
                    e.Successed = false;
                    return;
                }
            }
            else // 송유 및 기타 입고일 경우
            {
                fsIPHANG  = Get_Date(this.DTP01_CMIPHANG.GetValue().ToString());
                fsBONSUN  = this.CBH01_CMBONSUN.GetValue().ToString();
                fsHWAJU   = this.CBH01_SVHWAJU.GetValue().ToString().Trim();
                fsHJDESC1 = this.CBH01_SVHWAJU.GetText().ToString().Trim();
                fsHWAMUL  = this.CBH01_SVHWAMUL.GetValue().ToString().Trim();
                fsHMDESC1 = this.CBH01_SVHWAMUL.GetText().ToString().Trim();
                fsTANKNO  = Set_TankNo(this.TXT01_SVTANKNO.GetValue().ToString().Trim());

                fsBLNO    = this.TXT01_SVTANKNO.GetValue().ToString().Trim();
                fsMSNSEQ  = this.TXT01_SVTANKNO.GetValue().ToString().Trim();
                fsHSNSEQ  = "0";
                fsCUSTIL  = Get_Date(this.DTP01_CMBANIL.GetValue().ToString());
                fsCHASU   = "0";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_675B7565",
                    fsTANKNO.Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_SVBIJUNG.SetValue(dt.Rows[0]["TNBIJUNG"].ToString());
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_SVBIJUNG.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_UT_81HG0480");
                e.Successed = false;
                return;
            }

            // 20180112 고지파트에서 입항관리에 자료가 존재할 경우 패스 시켜달라고 요청
            // 입항관리 체크
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_66LC1314",
            //    fsIPHANG.ToString(),
            //    fsBONSUN.ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.ShowMessage("TY_M_UT_722JC637");
            //    e.Successed = false;
            //    return;
            //}

            // 입고화물관리 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66SAJ413",
                fsIPHANG.ToString(),
                fsBONSUN.ToString(),
                fsHWAJU.ToString(),
                fsHWAMUL.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_722JD638");
                e.Successed = false;
                return;
            }

            // SURVEY 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_671HM529",
                fsIPHANG.ToString(),
                fsBONSUN.ToString(),
                fsHWAJU.ToString(),
                fsHWAMUL.ToString(),
                fsTANKNO.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_722JD639");
                e.Successed = false;
                return;
            }

            // B/L별 입고관리 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67FH0793",
                fsIPHANG.ToString(),
                fsBONSUN.ToString(),
                fsHWAJU.ToString(),
                fsHWAMUL.ToString(),
                fsBLNO.ToString(),
                fsMSNSEQ.ToString(),
                fsHSNSEQ.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_722JD640");
                e.Successed = false;
                return;
            }


            fsCOCONTNO = "";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_77RBC281",
                fsHWAJU.ToString(),
                fsHWAMUL.ToString(),
                fsTANKNO.ToString().Trim(),
                Get_Date(this.DTP01_CMIPHANG.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count >= 2)
                {
                    this.ShowMessage("TY_M_UT_721I4633");
                    e.Successed = false;
                    return;
                }
                else
                {
                    fsCOCONTNO = dt.Rows[0]["CNCONTNO"].ToString().ToUpper();
                }
            }
            else
            {
                this.ShowMessage("TY_M_UT_6CMGS176");
                e.Successed = false;
                return;
            }

            // 계산
            UP_UTISURVF_COMPUTE();


            fsIPSINOYY = Get_Date(this.DTP01_CMBANIL.GetValue().ToString()).Substring(0,4);
            fsIPSINO   = "0";

            if (this.TXT01_IPCHANGGO.GetValue().ToString().ToUpper() == "1")
            {
                fsSEQGB = "11011055";

                // 반출입순차번호 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_67JD1844",
                    fsSEQGB.ToString(),
                    Get_Numeric(this.fsIPSINOYY.ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsSEQCH = dt.Rows[0]["SEQCH"].ToString();
                    this.fsIPSINO = fsSEQCH.ToString();
                }
                else
                {
                    fsSEQCH = "1";
                    this.fsIPSINO = fsSEQCH.ToString();
                }
            }

            // 통관차수 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_685FS976",
                fsIPHANG.ToString(),
                fsBONSUN.ToString(),
                fsHWAJU.ToString(),
                fsHWAMUL.ToString(),
                fsBLNO.ToString(),
                fsMSNSEQ.ToString(),
                fsHSNSEQ.ToString(),
                fsCUSTIL.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsCHASU = dt.Rows[0]["CSCHASU"].ToString();
            }

            // 생성 하시겠습니까?
            if (!this.ShowMessage("TY_M_UT_721E3629"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 계산
        private void UP_UTISURVF_COMPUTE()
        {
            string sAMOUNT   = string.Empty;
            string sAMOUNT1  = string.Empty;

            string sCNHANDAM = string.Empty;
            string sCNHANDDA = string.Empty;
            string sCNHANDHP = string.Empty;
            string sCNIPAM   = string.Empty;
            string sCNCHAM   = string.Empty;
            string sCNIPDA   = string.Empty;
            string sCNCHDA   = string.Empty;
            string sCNIPHP   = string.Empty;
            string sCNCHHP   = string.Empty;
            string sCNHANDOV = string.Empty;


            sAMOUNT    = "0";
            sAMOUNT1   = "0";

            fsCMHWAPE  = "";
            fsCOOVAM   = "0";

            // 반올림
            fsCMFACT = Convert.ToString(double.Parse(Set_Numeric2(this.TXT01_CMKLQTY.GetValue().ToString(), 3)) / double.Parse(Set_Numeric2(this.TXT01_IPMTQTY.GetValue().ToString(), 3))).ToString();

            DataTable dt  = new DataTable();
            DataTable dt1 = new DataTable();

            // 할증량
            if (double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3)) != 0)
            {
                string sKESAN  = string.Empty;
                string sKESAN1 = string.Empty;

                sKESAN =
                    (
                    double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                    / double.Parse(Set_Numeric2(this.TXT01_IPMTQTY.GetValue().ToString(), 3))
                    * double.Parse(Set_Numeric2(this.TXT01_CMKLQTY.GetValue().ToString(), 3))
                    ).ToString("0.00000000");

                sKESAN1 = Convert.ToString(double.Parse(Set_Numeric2(sKESAN.ToString(), 3))).ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_676G5597",
                    fsCOCONTNO.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 취급료
                    sCNHANDAM = Get_Numeric(dt.Rows[0]["CNHANDAM"].ToString());
                    // 취급료단위
                    sCNHANDDA = dt.Rows[0]["CNHANDDA"].ToString().ToUpper();
                    // 취급료화폐
                    sCNHANDHP = dt.Rows[0]["CNHANDHP"].ToString().ToUpper();
                    // 입고금액
                    sCNIPAM   = dt.Rows[0]["CNIPAM"].ToString().ToUpper();
                    // 출고금액
                    sCNCHAM   = dt.Rows[0]["CNCHAM"].ToString().ToUpper();
                    // 입고단위
                    sCNIPDA   = dt.Rows[0]["CNIPDA"].ToString().ToUpper();
                    // 출고단위
                    sCNCHDA   = dt.Rows[0]["CNCHDA"].ToString().ToUpper();
                    // 입고화폐
                    sCNIPHP   = dt.Rows[0]["CNIPHP"].ToString().ToUpper();
                    // 출고화폐
                    sCNCHHP   = dt.Rows[0]["CNCHHP"].ToString().ToUpper();
                    // 취급료할증율
                    sCNHANDOV = dt.Rows[0]["CNHANDOV"].ToString().ToUpper();

                    if (sCNHANDAM.ToString() != "0")
                    {
                        if (sCNHANDDA.ToString() == "MT")
                        {
                            sAMOUNT = Convert.ToString(Set_Numeric2
                                (Convert.ToString
                                (Decimal.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                * Decimal.Parse(Set_Numeric2(sCNHANDAM.ToString(), 2))), 3));
                        }
                        else
                        {
                            sAMOUNT = Convert.ToString(Set_Numeric2
                                (Convert.ToString
                                (Decimal.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                * Decimal.Parse(Set_Numeric2(sCNHANDAM.ToString(), 2))), 3));
                        }

                        fsCMHWAPE = sCNHANDHP.ToString();
                    }
                    else
                    {

                        if (Get_Numeric(sCNIPAM.ToString()) != "0" && Get_Numeric(sCNCHAM.ToString()) != "0")
                        {
                            if (sCNIPDA.ToString().ToUpper() == "MT")
                            {
                                sAMOUNT =
                                    (
                                    double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                    ).ToString("0.000");
                            }
                            else
                            {
                                sAMOUNT =
                                    (
                                    double.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                    * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                    ).ToString("0.000");
                            }

                            fsCMHWAPE = sCNIPHP.ToString();
                        }
                        else
                        {
                            if (Get_Numeric(sCNIPAM.ToString()) == "0")
                            {
                                if (sCNCHDA.ToString().ToUpper() == "MT")
                                {
                                    if (Get_Numeric(sCNCHAM.ToString()) != "0")
                                    {
                                        sAMOUNT =
                                            (
                                            double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNCHAM.ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                            ).ToString("0.000");
                                    }
                                }
                                else
                                {
                                    if (Get_Numeric(sCNCHAM.ToString()) != "0")
                                    {
                                        sAMOUNT =
                                            (
                                            double.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNCHAM.ToString(), 3))
                                            * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                            ).ToString("0.000");
                                    }
                                }

                                fsCMHWAPE = sCNCHHP.ToString();
                            }
                            else
                            {
                                if (sCNIPDA.ToString().ToUpper() == "MT")
                                {
                                    if (Get_Numeric(sCNHANDOV.ToString()) != "0")
                                    {
                                        if (Get_Numeric(sCNIPAM.ToString()) != "0")
                                        {
                                            sAMOUNT =
                                                (
                                                double.Parse(Set_Numeric2(this.TXT01_COOVQTY.GetValue().ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                                ).ToString("0.000");
                                        }
                                    }
                                    else
                                    {
                                        if (Get_Numeric(sCNIPAM.ToString()) != "0")
                                        {
                                            sAMOUNT =
                                                (
                                                double.Parse(Set_Numeric2(sKESAN1.ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNIPAM.ToString(), 3))
                                                * double.Parse(Set_Numeric2(sCNHANDOV.ToString(), 3)) / 100
                                                ).ToString("0.000");
                                        }
                                    }

                                    fsCMHWAPE = sCNIPHP.ToString();
                                }
                            }
                        }
                    }
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_677BJ618",
                fsIPHANG.ToString(),
                fsBONSUN.ToString(),
                fsHWAJU.ToString(),
                fsHWAMUL.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsCMHWAPE = dt.Rows[0]["CMHWAPE"].ToString();
            }

            if (fsCMHWAPE.ToString() == "" || fsCMHWAPE.ToString() == "2")
            {
                fsCOOVAM = sAMOUNT.ToString();
            }
            else
            {
                sAMOUNT1 = UP_DotDelete(sAMOUNT.ToString()).ToString();
                fsCOOVAM = sAMOUNT1.ToString();
            }
        }
        #endregion

        #region Description : 화물 텍스트박스 이벤트
        private void CBH01_SHWAMUL_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            //{
            //    BTN61_INQ_Click(null, null);
            //}
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_71VAB604_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                if (this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["GUBUN"].Index].CellType == null)
                {
                    fdAMOUNT = fdAMOUNT + double.Parse(String.Format("{0,9:N3}", this.FPS91_TY_S_UT_71VAB604.GetValue(e.Row, "CJIPQTY").ToString()));

                    TButtonCellType tButtonCellType = new TButtonCellType();

                    tButtonCellType.Text = "선택";
                    tButtonCellType.TextAlign = FarPoint.Win.ButtonTextAlign.TextTopPictBottom;
                    tButtonCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal;
                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["GUBUN"].Index].CellType = tButtonCellType;
                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["GUBUN"].Index].Locked = true;

                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["CJIPQTY"].Index].Font = new Font("굴림", 9, FontStyle.Bold);

                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Rows[e.Row].BackColor = Color.SkyBlue;
                }
                else
                {
                    fdAMOUNT = fdAMOUNT - double.Parse(String.Format("{0,9:N3}", this.FPS91_TY_S_UT_71VAB604.GetValue(e.Row, "CJIPQTY").ToString()));

                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["GUBUN"].Index].CellType = null;

                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Columns["CJIPQTY"].Index].Font = new Font("굴림", 9);

                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_UT_71VAB604.ActiveSheet.Rows[e.Row].BackColor = Color.White;
                }

                this.TXT01_IPQTY_HAP.SetValue((fdAMOUNT).ToString("0.000"));
            }
        }
        #endregion

        #region Description : 창고구분 이벤트
        private void TXT01_IPCHANGGO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_CREATE);
            }
        }
        #endregion

        #region Description : 탱크 이벤트
        private void TXT01_SVTANKNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.TXT01_SVTANKNO.GetValue().ToString() != "")
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_675B7565",
                    this.TXT01_SVTANKNO.GetValue().ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_SVBIJUNG.SetValue(dt.Rows[0]["TNBIJUNG"].ToString());
                }
            }
        }
        
        private void TXT01_SVTANKNO_Leave(object sender, EventArgs e)
        {
            if (this.TXT01_SVTANKNO.GetValue().ToString() != "")
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_675B7565",
                    this.TXT01_SVTANKNO.GetValue().ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_SVBIJUNG.SetValue(dt.Rows[0]["TNBIJUNG"].ToString());
                }
            }
        }
        #endregion

        #region Description : 입고량 입력 이벤트
        private void TXT01_IPMTQTY_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())) != 0 && Convert.ToDouble(Get_Numeric(this.TXT01_SVBIJUNG.GetValue().ToString())) != 0)
            {
                // 입고량 / 비중 = K/L량 (소수점 넷째자리에서 반올림)
                double dIPKLQTY = Convert.ToDouble(Get_Numeric(this.TXT01_IPMTQTY.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_SVBIJUNG.GetValue().ToString()));

                this.TXT01_CMKLQTY.SetValue(dIPKLQTY.ToString("0.000"));
            }
        }
        #endregion

        #region Description : 탱크번호 텍스트 박스 이벤트
        private void TXT01_CJTANKNO1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}