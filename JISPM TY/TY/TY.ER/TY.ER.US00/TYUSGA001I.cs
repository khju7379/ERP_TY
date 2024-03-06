using GrapeCity.ActiveReports;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;
using GrapeCity.ActiveReports;
using ThoughtWorks.QRCode.Codec;

namespace TY.ER.US00
{
    /// <summary>
    /// 수동 계근 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.09.03 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_995EI188 : 수동 계근 관리 조회
    ///  TY_P_US_995EU190 : 수동 계근 관리 확인
    ///  TY_P_US_995F6191 : 수동 계근 관리 공차등록
    ///  TY_P_US_995F8192 : 수동 계근 관리 BIN 번호 변경
    ///  TY_P_US_995FA193 : 수동 계근 관리 실차처리
    ///  TY_P_US_995FD194 : 수동 계근 관리 출고 로그 파일 등록
    ///  TY_P_US_995FE195 : 수동 계근 관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_995EJ189 : 수동 계근 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
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
    ///  CHGOKJONG : 곡종
    ///  CHHANGCHA : 항 차
    ///  CHHWAJU : 화주
    ///  CHCUSTIL : 통관일자
    ///  STDATE : 시작일자
    ///  CHBINNO : BIN NO
    ///  CHBLHSN : ＨＳＮ
    ///  CHBLMSN : ＭＳＮ
    ///  CHBLNO : B/L번호
    ///  CHCHTIME : 출문시간
    ///  CHCHULDAT : 출고일자
    ///  CHDCHULNUM : D연결번호
    ///  CHEMPTY : 공차무계
    ///  CHGEGUNGB : 계근대　구분
    ///  CHHMNO1 : 화물번호１
    ///  CHHMNO2 : 화물번호２
    ///  CHIPDATE : 입고일자
    ///  CHIPSEQ : 입고순번
    ///  CHIPTIME : 입문시간
    ///  CHJANGB : 잔량처리여부
    ///  CHJGGUBUN : 중개수수료구분
    ///  CHMTQTY : 출고M/T량
    ///  CHNUMBER1 : 트럭번호1
    ///  CHSEQ : 통관차수
    ///  CHSIKBAEL : 식별자
    ///  CHTKNO : TICKET번호
    ///  CHTOTAL : 실차무계
    ///  CHTRANS : 운송업체
    ///  CHWONHWAJU : 원화주
    ///  CHWONSAN : 원산지
    ///  CHYDSEQ : 양도차수
    ///  CHYNCHASU : 양수차수
    ///  CHYNCHQTY : 양도출고량
    ///  CHYNGUBUN : 양수구분
    ///  CHYNHWAJU : 양도화주
    ///  CHYNILJA : 양수일자
    ///  CHYSSEQ : 양수순번
    /// </summary>
    public partial class TYUSGA001I : TYBase
    {
        private string fsWKGUBUN = string.Empty;

        private string fsCHANGE_BIN = string.Empty;
        private string fsPREBINNO = string.Empty;
        private string fsRFID = string.Empty;
        private string fsJCIMCHQTY = string.Empty;

        private string fsJBSOSOK = string.Empty;
        private string fsCHWONHWAJU = string.Empty;
        private string fsCHHMNO1 = string.Empty;
        private string fsCHHMNO2 = string.Empty;
        private string fsCHSIKBAEL = string.Empty;

        private string fsCLINKBIN1 = string.Empty;   // 연결 BIN 1
        private string fsCLINKBIN2 = string.Empty;   // 연결 BIN 2

        // 일별 통관재고
        private double fdJCCHQTY = 0;
        private double fdJCYSCHQTY = 0;
        private double fdJCCSQTY = 0;
        private double fdJCYSQTY = 0;
        private double fdJCYDQTY = 0;
        private double fdJCYSYDQTY = 0;
        private double fdJCJEGOQTY = 0;

        // 화주별 재고
        private double fdJGCHQTY = 0;
        private double fdJGYSCHQTY = 0;
        private double fdJGHWAKQTY = 0;
        private double fdJGYDQTY = 0;
        private double fdJGYSQTY = 0;
        private double fdJGYSYDQTY = 0;
        private double fdJGJEGOQTY = 0;
        private double fdJGYSJANQTY = 0;
        private double fdJGJANQTY = 0;

        // 통관파일
        private double fdCSQTY = 0;
        private double fdCSCHQTY = 0;
        private double fdCSJGQTY = 0;

        // B/L 별 재고
        private double fdJBCHQTY = 0;
        private double fdJBYSCHQTY = 0;
        private double fdJBHWAKQTY = 0;
        private double fdJBYDQTY = 0;
        private double fdJBYSQTY = 0;
        private double fdJBYSYDQTY = 0;
        private double fdJBCSQTY = 0;
        private double fdJBJEGOQTY = 0;
        private double fdJBYSJANQTY = 0;
        private double fdJBJANQTY = 0;

        // BIN 상태관리
        private double fdSCHULQTY_M = 0;
        private double fdSJEGOQTY_M = 0;
        private double fdSCHULQTY_L1 = 0;
        private double fdSJEGOQTY_L1 = 0;
        private double fdSCHULQTY_L2 = 0;
        private double fdSJEGOQTY_L2 = 0;

        #region Description : 폼 로드
        public TYUSGA001I()
        {
            InitializeComponent();
        }

        private void TYUSGA001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_CHCHULDAT.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));

            CBO01_GONGCHAGB.SetValue("G");

            CBH01_CHGOKJONG.SetReadOnly(true);
            CBH01_CHHANGCHA.SetReadOnly(true);
            CBH01_CHWONSAN.SetReadOnly(true);
            CBH01_TRUNSONG.SetReadOnly(true);
            CBH01_TRHYUNGT.SetReadOnly(true);
            CBH01_CHYNHWAJU.SetReadOnly(true);

            MTB01_CHCUSTIL.SetReadOnly(true);
            MTB01_CHCUSTIL.Enabled = false;
            MTB01_CHYNILJA.SetReadOnly(true);
            MTB01_CHYNILJA.Enabled = false;

            UP_Lock_Field("INIT");

            SetStartingFocus(DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_A17EP696",
                Get_Date(DTP01_STDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                CBO01_GEGUNGB.GetValue().ToString(),
                CBO01_GONGCHAGB.GetValue().ToString(),
                TXT01_CJNUMBER.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_995EJ189.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                SetFocus(FPS91_TY_S_US_995EJ189);
            }

            // 실차/공차 색깔 변경 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_995EJ189.GetValue(i, "CHTOTAL").ToString())) > 0)
                {
                    this.FPS91_TY_S_US_995EJ189.ActiveSheet.Rows[i].ForeColor = Color.Blue;
                }
                else
                {
                    this.FPS91_TY_S_US_995EJ189.ActiveSheet.Rows[i].ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsWKGUBUN = "NEW";
            UP_Lock_Field(fsWKGUBUN);

            UP_FieldClear(fsWKGUBUN);
            UP_Var_Clear();

            SetFocus(CBO01_CHGEGUNGB);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sSEQFGB = string.Empty;
            string sSEQFCHGB = string.Empty;
            string sCHNUFGB = string.Empty;
            string sHWAJUFGB = string.Empty;
            string sBTKILL = string.Empty;

            string sMSG = string.Empty;

            // 공차
            if (fsWKGUBUN == "NEW")
            {
                #region Description : 공차
                // 순번 가져오기
                UP_Create_SEQ();

                // BIN 작업 파일 등록 체크
                sSEQFGB = UP_USIBNSEQF_UPTCHECK("NEW");

                // BIN 상태파일 등록 체크
                sBTKILL = UP_USIBNSTF_UPTCHECK();

                // --------------------------------------------

                this.DbConnector.CommandClear();
                // 출고 파일 공차 등록 OK
                UP_USICHULF_UPDATE();

                // BIN 작업 파일 등록 OK
                UP_USIBNSEQF_UPDATE("NEW", sSEQFGB);

                // BIN 상태파일 등록 OK
                UP_USIBNSTF_UPDATE("NEW", sBTKILL);

                // 통관일별 재고파일 가상 출고량 가산
                UP_JCIMCHQTY_UPDATE();

                this.DbConnector.ExecuteTranQueryList();
                #endregion
            }
            else if (fsWKGUBUN == "UPT")
            {

                // BIN 변경작업
                if (fsCHANGE_BIN == "CHANGE")
                {
                    #region Descripton : BIN 변경작업
                    // BIN 작업 파일 등록 체크 OK
                    sSEQFCHGB = UP_USIBNSEQF_UPTCHECK("CHANGE");

                    // BIN 작업 파일 등록 체크 OK
                    sSEQFGB = UP_USIBNSEQF_UPTCHECK("NEW");

                    // BIN 상태파일 등록 체크 OK
                    sBTKILL = UP_USIBNSTF_UPTCHECK();

                    // ----------------------------------

                    this.DbConnector.CommandClear();
                    // 출고 파일 BIN 변경 OK
                    UP_USICHULF_UPDATE();

                    // 이전 BIN 작업 파일 삭제 OK
                    UP_USIBNSEQF_UPDATE("CHANGE", sSEQFCHGB);

                    // BIN 작업 파일 등록 OK
                    UP_USIBNSEQF_UPDATE("NEW", sSEQFGB);

                    // 이전 BIN 상태파일 삭제 OK
                    UP_USIBNSTF_UPDATE("CHANGE", sBTKILL);

                    // BIN 상태파일 등록 OK
                    UP_USIBNSTF_UPDATE("NEW", sBTKILL);

                    this.DbConnector.ExecuteTranQueryList();
                    #endregion
                }
                else
                {
                    #region Descriptoin : 실차
                    // 실차 체크
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_US_9AGAK356",
                                            Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                            this.TXT01_CHTKNO.GetValue().ToString(),    // 순번
                                            this.TXT01_CHNUMBER.GetValue().ToString()   // 차량번호2
                                            );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 실차 처리된 자료 입니다.
                        this.ShowCustomMessage("실차 처리된 자료 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.TXT01_CHTOTAL);
                        return;
                    }
                    else
                    {
                        // 저장 전 재고량 다시 계산
                        UP_SILCHA_Check();

                        // 연결 BIN 조회 OK
                        UP_LINKBIN_SEL();

                        // BIN 상태관리 업데이트 체크 (프로시저 변경필요 BIN_STATUSMF1 -> BIN_STATUSMF)
                        UP_BIN_STATUSMF_UPTCHECK("M");
                        if (fsCLINKBIN1 != "")
                        {
                            UP_BIN_STATUSMF_UPTCHECK("L1");
                        }
                        if (fsCLINKBIN2 != "")
                        {
                            UP_BIN_STATUSMF_UPTCHECK("L2");
                        }
                        // 출고 누계파일 업데이트 체크
                        sCHNUFGB = UP_USICHNUF_UPTCHECK();

                        // BIN 작업 파일 등록 체크 OK
                        sSEQFCHGB = UP_USIBNSEQF_UPTCHECK("DEL");

                        // 계근관련 화주정보 업데이트 체크
                        sHWAJUFGB = UP_USAHWAJUF_UPTCHECK();

                        // -----------------------------------------

                        this.DbConnector.CommandClear();

                        // 출고 파일 실차 처리 OK
                        UP_USICHULF_UPDATE();

                        // 출고 로그 파일 등록 OK
                        UP_USICHLOGNF_UPDATE();

                        // BIN 상태관리 업데이트 OK (프로시저 변경필요 BIN_STATUSMF1 -> BIN_STATUSMF)
                        UP_BIN_STATUSMF_UPDATE("M");
                        if (fsCLINKBIN1 != "")
                        {
                            UP_BIN_STATUSMF_UPDATE("L1");  // OK
                        }
                        if (fsCLINKBIN2 != "")
                        {
                            UP_BIN_STATUSMF_UPDATE("L2");
                        }
                        // 재고 업데이트
                        // B/L 별 재고파일 업데이트 OK
                        UP_USIJEBLF_UPDATE();
                        // 통관일별 재고파일 업데이트 OK
                        UP_USIJECSNF_UPDATE();
                        // 재고파일 업데이트 OK
                        UP_USIJEGOF_UPDATE();
                        // 통관파일 업데이트 OK
                        UP_USICUSTF_UPDATE();

                        // 양수도 파일 업데이트 OK
                        if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
                        {
                            UP_USIYANGNF_UPDATE();
                        }
                        // 출고 누계 파일 업데이트 다시확인
                        UP_USICHNUF_UPDATE(sCHNUFGB);

                        // BIN 입고 파일 업데이트 OK
                        UP_USIBNIPF_UPDATE();

                        // BIN 작업 파일 삭제 OK
                        UP_USIBNSEQF_UPDATE("DEL", sSEQFCHGB);

                        // BIN 상태파일 삭제 OK
                        UP_USIBNSTF_UPDATE("DEL", "");

                        // 계근관련 화주정보 업데이트 OK
                        UP_USAHWAJUF_UPDATE(sHWAJUFGB);

                        // 통관일별 재고파일 가상출고량 감산 OK
                        UP_JCIMCHQTY_UPDATE();

                        this.DbConnector.ExecuteTranQueryList();

                        // 출고증 출력
                        UP_SiloPrint(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                    this.TXT01_CHTKNO.GetValue().ToString(),    // 순번
                                    "");
                    }
                    #endregion
                }
            }

            if (fsWKGUBUN == "NEW" || fsCHANGE_BIN == "CHANGE")
            {
                UP_Lock_Field("GONG");
            }
            else if (fsWKGUBUN == "UPT")
            {
                UP_Lock_Field("SIL");
            }

            if (fsWKGUBUN == "NEW")
            {
                sMSG = "TY_M_US_9ALKS397";
            }
            else if (fsWKGUBUN == "UPT")
            {
                if (fsCHANGE_BIN == "CHANGE")
                {
                    sMSG = "TY_M_US_9ALKS399";
                }
                else
                {
                    sMSG = "TY_M_US_9ALKS398";
                    // 실차 시 신규 등록 모드로 전환
                    BTN61_NEW_Click(null, null);
                }
            }

            this.ShowMessage(sMSG);

            this.BTN61_INQ_Click(null, null);

            UP_Run();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            string sMSG = string.Empty;

            #region Description : 공차,실차,BIN 변경 작업 공통 체크
            // 화주코드 거래처 파일 체크 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ASGF426",
                                    this.TXT01_CHHWAJU.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("화주 코드를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHHWAJU);
                return;
            }

            // 항차(모선) 체크 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ASGI427",
                                    "VS",
                                    this.CBH01_CHHANGCHA.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("항차 코드를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.CBH01_CHHANGCHA);
                return;
            }

            // 입항관리 등록 체크 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ASGM428",
                                    this.CBH01_CHHANGCHA.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("미등록된 모선 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.CBH01_CHHANGCHA);
                return;
            }

            // 곡종 체크 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ASGI427",
                                    "GK",
                                    this.CBH01_CHGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("미등록된 곡종입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.CBH01_CHGOKJONG);
                return;
            }

            // 원산지 체크 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ASGI427",
                                    "WN",
                                    this.CBH01_CHWONSAN.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("미등록된 원산지입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.CBH01_CHWONSAN);
                return;
            }

            // 양도화주 체크 OK
            if (this.CBH01_CHYNHWAJU.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9ASGF426",
                                        this.CBH01_CHYNHWAJU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("양도화주 코드를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.CBH01_CHYNHWAJU);
                    return;
                }
            }

            // 차량조회 여부 체크 OK
            if (this.TXT01_CHNUMBER1.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("차량번호 조회 후 등록하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHNUMBER);
                return;
            }
            // BIN 입고파일 확인 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AAID310",
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),
                                    this.TXT01_CHBINNO.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 원산지 체크 OK
                if (this.CBH01_CHWONSAN.GetValue().ToString() != dt.Rows[0]["BNWONSAN"].ToString())
                {
                    this.ShowCustomMessage("출고 BIN의 원산지를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHBINNO);
                    return;
                }
                // 출고가능 확인 OK
                if (dt.Rows[0]["BNCHGN"].ToString() != "Y")
                {
                    this.ShowCustomMessage("BIN 출고가능 상태가 아닙니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHBINNO);
                    return;
                }
            }
            else
            {
                this.ShowCustomMessage("BIN 입고파일에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHBINNO);
                return;
            }

            // 차량파일 확인 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AAIS312",
                                    this.TXT01_CHNUMBER1.GetValue().ToString(),
                                    this.TXT01_CHNUMBER.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("미등록 차량입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHNUMBER);
                return;
            }
            // 44톤 적용 시작
            // 챠량중량 파일 확인 OK
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AAJY314",
                                    this.TXT01_CHNUMBER1.GetValue().ToString(),
                                    this.TXT01_CHNUMBER.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //this.TXT01_TGJUNGRY.SetValue(dt.Rows[0]["TGJUNGRY"].ToString());
                this.TXT01_TGJUNGRY.SetValue(string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[0]["TGJUNGRY"].ToString()))));
            }
            else
            {
                this.ShowCustomMessage("차량 중량파일에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHNUMBER);
                return;
            }

            // 차량출고 OK
            if (this.TXT01_CHNUMBER.GetValue().ToString() != "0000")
            {

                fsJCIMCHQTY = String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(this.TXT01_TGJUNGRY.GetValue().ToString())) - Convert.ToDouble(Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString())));

                if (fsWKGUBUN == "UPT")
                {
                    this.TXT01_CHMTQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(this.TXT01_CHTOTAL.GetValue().ToString())) - Convert.ToDouble(Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString()))));
                }

                // 공차중량 > 차량중량 체크 OK
                if (Convert.ToDouble(Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString())) > Convert.ToDouble(Get_Numeric(this.TXT01_TGJUNGRY.GetValue().ToString())))
                {
                    this.ShowCustomMessage("공차 중량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHEMPTY);
                    return;
                }
            }
            else if (this.TXT01_CHNUMBER.GetValue().ToString() == "0000")
            {
                // 차량번호 '0000' 체크  I02, D06, J21 화주만 사용가능 OK
                if (this.TXT01_CHHWAJU.GetValue().ToString() != "I02" &&
                    this.TXT01_CHHWAJU.GetValue().ToString() != "D06" &&
                    this.TXT01_CHHWAJU.GetValue().ToString() != "J21")
                {
                    this.ShowCustomMessage("사용할 수 없는 차량번호 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHNUMBER);
                    return;
                }
            }

            // 과적 체크 OK
            if (Convert.ToDouble(Get_Numeric(fsJCIMCHQTY)) > 0)
            {
                if (Convert.ToDouble(Get_Numeric(fsJCIMCHQTY)) < Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())))
                {
                    this.ShowCustomMessage("과적 여부를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHTOTAL);
                    return;
                }
            }
            // 44톤 적용 종료

            // 재고조회 여부 체크 OK 
            if (Get_Date(MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim()) == "" ||
                Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()) == "0")
            {
                this.ShowCustomMessage("재고조회 후 등록하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHHWAJU);
                return;
            }

            // 통관일자 > 출고일자 체크 OK
            if (Convert.ToDouble(Get_Numeric(Get_Date(MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim()))) > Convert.ToDouble(Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""))))
            {
                this.ShowCustomMessage("통관일자가 출고일자보다 큽니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.DTP01_CHCHULDAT);
                return;
            }
            #endregion

            #region Description : BIN 변경 작업 체크

            if (fsPREBINNO != "")
            {
                if (this.TXT01_CHBINNO.GetValue().ToString() != fsPREBINNO)
                {
                    fsCHANGE_BIN = "CHANGE";
                }
            }

            if (fsCHANGE_BIN == "CHANGE")
            {
                // 공차내역 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9AAIO311",
                                        fsPREBINNO,   // 이전 빈 번호
                                        this.TXT01_CHNUMBER.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("BIN 작업현황 내역이 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHBINNO);
                    return;
                }

                // RFID 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9AHIE364",
                                        this.TXT01_CHNUMBER1.GetValue().ToString(),
                                        this.TXT01_CHNUMBER.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsRFID = dt.Rows[0]["TRRFID"].ToString();
                }
            }
            #endregion
            #region Description : 공차, 실차 체크
            else
            {
                #region Description : 공차 체크

                if (fsWKGUBUN == "NEW") // 신규 OK
                {
                    sMSG = UP_GONGCHA_Check();

                    if (sMSG != "OK")
                    {
                        if (sMSG == "1")
                        {
                            this.ShowCustomMessage("이 차량에 대해 공차할 수 없는 화주입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHHWAJU);
                        }
                        else if (sMSG == "2")
                        {
                            this.ShowCustomMessage("미등록 차량입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHNUMBER);
                        }
                        else if (sMSG == "3")
                        {
                            this.ShowCustomMessage("RFID 카드 발급후 작업하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHNUMBER);
                        }
                        else if (sMSG == "4")
                        {
                            this.ShowCustomMessage("BIN 출고 MAST 파일에 등록된 자료가 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHHWAJU);
                        }
                        else if (sMSG == "5")
                        {
                            this.ShowCustomMessage("SILO내에 같은번호의 차량이 작업중입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHNUMBER);
                        }
                        else if (sMSG == "6")
                        {
                            this.ShowCustomMessage("출고일자가 현재일자보다 큽니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.DTP01_CHCHULDAT);
                        }

                        e.Successed = false;
                        return;
                    }
                }
                #endregion

                #region Description : 실차 체크
                else if (fsWKGUBUN == "UPT")
                {
                    sMSG = UP_SILCHA_Check();

                    if (sMSG != "OK")
                    {
                        if (sMSG == "1")
                        {
                            this.ShowCustomMessage("실차무계를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHTOTAL);
                        }
                        else if (sMSG == "2")
                        {
                            this.ShowCustomMessage("공차무계가 실차무계보다 큽니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHTOTAL);
                        }
                        else if (sMSG == "3")
                        {
                            this.ShowCustomMessage("공차 내역이 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHHWAJU);
                        }
                        else if (sMSG == "4")
                        {
                            this.ShowCustomMessage("일자별 통관재고 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHTOTAL);
                        }
                        else if (sMSG == "5")
                        {
                            this.ShowCustomMessage("출고가능 수량을 초과했습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHTOTAL);
                        }
                        else if (sMSG == "6")
                        {
                            this.ShowCustomMessage("차량 중량 초과입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHTOTAL);
                        }
                        else if (sMSG == "7")
                        {
                            this.ShowCustomMessage("화주별 재고(USIJEGOF) 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHHWAJU);
                        }
                        else if (sMSG == "8")
                        {
                            this.ShowCustomMessage("통관 자료가 존재하지 않습니다.(USICUSTF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHHWAJU);
                        }
                        else if (sMSG == "9")
                        {
                            this.ShowCustomMessage("B/L별 재고 자료가 존재하지 않습니다.(USIJEBLF)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.TXT01_CHHWAJU);
                        }

                        e.Successed = false;
                        return;
                    }
                }
                #endregion

                #region Description : 공차. 실차 공통 체크
                // 모선재고 OK
                if (!UP_Sel_USIJEBLF())
                {
                    this.ShowCustomMessage("USIJEBLF 모선 재고파일이 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHHWAJU);
                    return;
                }
                // 통관재고 OK
                if (!UP_Sel_USIJECSF())
                {
                    this.ShowCustomMessage("USIJECSF 통관 일별 재고파일이 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHHWAJU);
                    return;
                }
                // 통관관리 조회 OK
                string sCHHWAJU = string.Empty;

                if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
                {
                    sCHHWAJU = fsCHWONHWAJU;
                }
                else
                {
                    sCHHWAJU = this.TXT01_CHHWAJU.GetValue().ToString();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_95FGD565",
                                        this.CBH01_CHHANGCHA.GetValue().ToString(),
                                        this.CBH01_CHGOKJONG.GetValue().ToString(),
                                        sCHHWAJU,
                                        this.TXT01_CHBLNO.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),
                                        Get_Numeric(Get_Date(MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),
                                        Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count < 0)
                {
                    this.ShowCustomMessage("통관내역이 존재하지 않습니다(USICUSTF).", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHBINNO);
                    return;
                }
                // 양수도체크 OK
                if (this.TXT01_CHYNGUBUN.GetValue().ToString() != "" && this.TXT01_CHYNGUBUN.GetValue().ToString() != "R")
                {
                    this.ShowCustomMessage("양수출고 구분 항목에는 'R' 또는 공백만 입력가능합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_CHYNGUBUN);
                    return;
                }
                else if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
                {
                    // 양수도 체크 OK

                    sMSG = UP_USIYANGNF_Check();

                    if (sMSG != "OK")
                    {
                        if (sMSG == "1")
                        {
                            this.ShowCustomMessage("양도화주를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.CBH01_CHYNHWAJU);
                        }
                        else if (sMSG == "2")
                        {
                            this.ShowCustomMessage("양수일자를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.MTB01_CHYNILJA);
                        }
                        else if (sMSG == "3")
                        {
                            this.ShowCustomMessage("양수 잔량이 부족합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.CBH01_CHYNHWAJU);
                        }
                        else if (sMSG == "4")
                        {
                            this.ShowCustomMessage("양수도 내역이 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetFocus(this.CBH01_CHYNHWAJU);
                        }
                        e.Successed = false;
                        return;
                    }
                }

                if (Convert.ToDouble(Get_Numeric(this.TXT01_JCJEGOQTY.GetValue().ToString())) <= 0)
                {
                    this.ShowCustomMessage("재고량을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    SetFocus(this.TXT01_JCJEGOQTY);
                    return;
                }


                #endregion
            }
            #endregion

            if (fsWKGUBUN == "NEW")
            {
                sMSG = "TY_M_US_9ALKP394";
            }
            else if (fsWKGUBUN == "UPT")
            {
                if (fsCHANGE_BIN == "CHANGE")
                {
                    sMSG = "TY_M_US_9ALKP395";
                }
                else
                {
                    sMSG = "TY_M_US_9ALKP396";
                }
            }

            if (!this.ShowMessage(sMSG))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 공차 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sSEQFCHGB = string.Empty;

            fsWKGUBUN = "DEL";

            // BIN 작업 파일 체크
            sSEQFCHGB = UP_USIBNSEQF_UPTCHECK(fsWKGUBUN);

            // ----------------------

            this.DbConnector.CommandClear();
            // 출고 파일 공차 삭제 OK
            UP_USICHULF_UPDATE();

            // 출고 로그 파일 등록 OK
            UP_USICHLOGNF_UPDATE();

            // BIN 작업 파일 삭제 OK
            UP_USIBNSEQF_UPDATE(fsWKGUBUN, sSEQFCHGB);

            // BIN 상태파일 삭제 OK
            UP_USIBNSTF_UPDATE(fsWKGUBUN, "");

            // 통관일자별 재고 파일 가상출고량 감산 OK
            UP_JCIMCHQTY_UPDATE();

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            BTN61_INQ_Click(null, null);

            UP_Lock_Field("INIT");
            UP_FieldClear(fsWKGUBUN);
            UP_Var_Clear();

            this.CBO01_CHGEGUNGB.Focus();
        }
        #endregion

        #region Description : 공차 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsPREBINNO != "")
            {
                if (this.TXT01_CHBINNO.GetValue().ToString() != fsPREBINNO)
                {
                    fsCHANGE_BIN = "CHANGE";
                }
            }

            // BIN 변경여부 체크
            if (fsCHANGE_BIN == "CHANGE")
            {
                this.ShowCustomMessage("BIN 변경시 삭제할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHBINNO);
                return;
            }

            // 차량출고
            if (this.TXT01_CHNUMBER.GetValue().ToString() != "0000")
            {
                fsJCIMCHQTY = String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(this.TXT01_TGJUNGRY.GetValue().ToString())) - Convert.ToDouble(Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString())));
            }

            // BIN 출고 MAST 파일 체크 (USABINF) 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ABEP319",
                                    this.TXT01_CHBINNO.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("BIN 출고 MAST 파일에 등록된 자료가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHHWAJU);
                return;
            }

            // 실차 체크
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_9AGAK356",
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                    Get_Numeric(this.TXT01_CHTKNO.GetValue().ToString()),    // 순번
                                    this.TXT01_CHNUMBER.GetValue().ToString()   // 차량번호2
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("실차 처리된 자료 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                SetFocus(this.TXT01_CHTOTAL);
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭 / 엔터 이벤트
        private void FPS91_TY_S_US_995EJ189_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_CHCHULDAT.SetValue(this.FPS91_TY_S_US_995EJ189.GetValue("CHCHULDAT").ToString());
            this.TXT01_CHTKNO.SetValue(this.FPS91_TY_S_US_995EJ189.GetValue("CHTKNO").ToString());
            this.TXT01_CHNUMBER.SetValue(this.FPS91_TY_S_US_995EJ189.GetValue("CHNUMBER").ToString());

            UP_Run();
        }

        private void FPS91_TY_S_US_995EJ189_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.DTP01_CHCHULDAT.SetValue(this.FPS91_TY_S_US_995EJ189.GetValue("CHCHULDAT").ToString());
                this.TXT01_CHTKNO.SetValue(this.FPS91_TY_S_US_995EJ189.GetValue("CHTKNO").ToString());
                this.TXT01_CHNUMBER.SetValue(this.FPS91_TY_S_US_995EJ189.GetValue("CHNUMBER").ToString());

                UP_Run();
            }
        }
        #endregion

        #region Descriptoin : 출고 데이터 확인
        private void UP_Run()
        {
            string sIHWONSAN = string.Empty;

            DataTable dt = new DataTable();

            UP_Var_Clear();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_9AU9Q436",
                Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),
                Get_Numeric(TXT01_CHTKNO.GetValue().ToString()),
                TXT01_CHNUMBER.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                // BIN 번호
                fsPREBINNO = dt.Rows[0]["CHBINNO"].ToString();
                // 원화주
                fsCHWONHWAJU = dt.Rows[0]["CHWONHWAJU"].ToString();

                // 양수일자
                if (dt.Rows[0]["CHYNILJA"].ToString() == "0")
                {
                    MTB01_CHYNILJA.SetValue("");
                }
                else
                {
                    MTB01_CHYNILJA.SetValue(dt.Rows[0]["CHYNILJA"].ToString());
                }

                // 출고 시간(초)
                TXT01_BTCHTIME.SetValue(Convert.ToDouble(dt.Rows[0]["BTCHTIME"].ToString()) > 0 ? dt.Rows[0]["BTCHTIME"].ToString() : "");
                // GATE OPEN
                TXT01_BTCLICK.SetValue(Set_Fill2(dt.Rows[0]["BTCLICK"].ToString()));
                TXT01_BTCOUNT.SetValue(Set_Fill2(dt.Rows[0]["BTCOUNT"].ToString()));

                // 화물번호1
                fsCHHMNO1 = dt.Rows[0]["JBHMNO1"].ToString();
                // 화물번호2
                fsCHHMNO2 = dt.Rows[0]["JBHMNO2"].ToString();
                // 소속
                fsJBSOSOK = dt.Rows[0]["JBSOSOK"].ToString();

                fsWKGUBUN = "UPT";

                if (Convert.ToDouble(Get_Numeric(TXT01_CHTOTAL.GetValue().ToString())) > 0)
                {
                    UP_Lock_Field("SIL");
                    // 실차 처리된 자료 입니다.
                }
                else
                {
                    UP_Lock_Field("GONG");
                    // 공차 등록된 자료 입니다.
                }

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.TXT01_CHTOTAL.Focus();
                };

                tmr.Interval = 100;
                tmr.Start();
            }
            else
            {
                fsWKGUBUN = "NEW";

                UP_FieldClear("CLR");
                UP_Lock_Field(fsWKGUBUN);

                // 출고관련 화주정보 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9A8FG296",
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),  // 출고일자
                                        this.TXT01_CHHWAJU.GetValue().ToString()); // 출고화주

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_CHHWAJU.SetValue(dt.Rows[0]["AHWAJU"].ToString());       // 화주
                    this.TXT01_CHHWAJUNM.SetValue(dt.Rows[0]["VNSANGHO"].ToString());   // 화주명
                    this.CBH01_CHGOKJONG.SetValue(dt.Rows[0]["AGOKJONG"].ToString());   // 곡종
                    this.CBH01_CHHANGCHA.SetValue(dt.Rows[0]["AHANGCHA"].ToString());   // 항차
                    this.MTB01_CHCUSTIL.SetValue(dt.Rows[0]["ACUSTIL"].ToString());     // 통관일자
                    this.TXT01_CHSEQ.SetValue(dt.Rows[0]["ACUSTCH"].ToString());        // 통관차수
                    this.TXT01_CHBLMSN.SetValue(Set_Fill4(dt.Rows[0]["ABLMSN"].ToString()));    // MSN
                    this.TXT01_CHBLHSN.SetValue(Set_Fill4(dt.Rows[0]["ABLHSN"].ToString()));    // HSN
                    this.TXT01_CHBLNO.SetValue(dt.Rows[0]["ABLNO"].ToString());         // B/L 번호
                    this.CBH01_CHYNHWAJU.SetValue(dt.Rows[0]["AYNHWAJU"].ToString());   // 양도화주
                    if (dt.Rows[0]["AYNILJA"].ToString() != "0")
                    {
                        this.MTB01_CHYNILJA.SetValue(dt.Rows[0]["AYNILJA"].ToString());     // 양수일자
                    }
                    else
                    {
                        this.MTB01_CHYNILJA.SetValue("");     // 양수일자
                    }
                    this.TXT01_CHHMNO1.SetValue(dt.Rows[0]["AYHMNO1"].ToString());      // 화물번호1
                    this.TXT01_CHHMNO2.SetValue(dt.Rows[0]["AYHMNO2"].ToString());      // 화물번호2

                    // 원산지
                    if (this.CBH01_CHGOKJONG.GetValue().ToString() == dt.Rows[0]["IHGOKJONG1"].ToString())
                    {
                        sIHWONSAN = dt.Rows[0]["IHWONSAN1"].ToString();
                    }
                    else if (this.CBH01_CHGOKJONG.GetValue().ToString() == dt.Rows[0]["IHGOKJONG2"].ToString())
                    {
                        sIHWONSAN = dt.Rows[0]["IHWONSAN2"].ToString();
                    }
                    else if (this.CBH01_CHGOKJONG.GetValue().ToString() == dt.Rows[0]["IHGOKJONG3"].ToString())
                    {
                        sIHWONSAN = dt.Rows[0]["IHWONSAN3"].ToString();
                    }
                    this.CBH01_CHWONSAN.SetValue(sIHWONSAN);

                    // 협회(소속)
                    fsJBSOSOK = dt.Rows[0]["JBSOSOK"].ToString();
                }

                // 출고가능 BIN 조회
                UP_Sel_USIBNSEQF();
            }
        }
        #endregion

        #region Description : 순번 생성
        private void UP_Create_SEQ()
        {
            DataTable dt = new DataTable();

            string sNOWDATE = System.DateTime.Now.ToString("yyyyMMdd");
            string sCNDATE = Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "");
            string sSEQ = string.Empty;

            if (sNOWDATE == sCNDATE)
            {
                // 출고데이터 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9AF9H334",
                                        sCNDATE);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    // 순번 초기화
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_9AF9W338");
                    this.DbConnector.ExecuteTranQueryList();
                }

                // 순번 조회 현재일자
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9AF9S335");

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSEQ = dt.Rows[0]["SEQUENCE"].ToString();
                }
            }
            else if (Convert.ToDouble(Get_Numeric(sNOWDATE)) > Convert.ToDouble(Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""))))
            {
                // 순번 조회 이전일자
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9AFAG339",
                                        sCNDATE);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSEQ = dt.Rows[0]["CNSEQ"].ToString();
                    sSEQ = (Convert.ToDouble(sSEQ) + 1).ToString();
                }
                else
                {
                    sSEQ = "1";
                }
            }

            if (sSEQ == "1")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9AFAG339",
                                        sCNDATE);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    // 순번 등록
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_9AF9V336",
                                            sCNDATE,
                                            sSEQ);
                    this.DbConnector.ExecuteTranQueryList();
                }
            }
            else
            {
                // 순번 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9AF9V337",
                                        sSEQ,
                                        sCNDATE);
                this.DbConnector.ExecuteTranQueryList();
            }

            this.TXT01_CHTKNO.SetValue(sSEQ);
        }
        #endregion

        #region Description : 연결 BIN 조회
        private void UP_LINKBIN_SEL()
        {
            DataTable dt = new DataTable();

            // 연결 BIN 조회
            // BIN 작업차량 파일 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AIEN372",
                                    this.TXT01_CHBINNO.GetValue().ToString());  // BIN 번호                                    

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsCLINKBIN1 = dt.Rows[0]["CLINKBIN1"].ToString();
                fsCLINKBIN2 = dt.Rows[0]["CLINKBIN2"].ToString();
            }
        }
        #endregion

        #region Description : 출고 파일 업데이트
        private void UP_USICHULF_UPDATE()
        {
            string sCHYNCHQTY = string.Empty;
            string sCHWONHWAJU = string.Empty;

            if (this.TXT01_CHYNGUBUN.GetValue().ToString() != "R")
            {
                sCHWONHWAJU = "";
            }
            else
            {
                sCHWONHWAJU = fsCHWONHWAJU;
            }


            if (fsWKGUBUN == "NEW")
            {
                this.DbConnector.Attach("TY_P_US_9AFEC344",
                                        this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                        this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                        this.TXT01_CHHWAJU.GetValue().ToString(),   // 화주
                                        this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                        Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                        Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                        Get_Numeric(this.TXT01_CHTKNO.GetValue().ToString()),    // 순번
                                        Get_Numeric(this.TXT01_CHHMNO1.GetValue().ToString()),   // 화물번호1
                                        Get_Numeric(this.TXT01_CHHMNO2.GetValue().ToString()),   // 화물번호2
                                        this.CBO01_CHGEGUNGB.GetValue().ToString(), // 계근대 구분
                                        Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                        Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),     // 통관차수
                                        Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString()),   // 공차중량
                                        this.TXT01_CHYNGUBUN.GetValue().ToString(), // 양수출고구분
                                        this.CBH01_CHYNHWAJU.GetValue().ToString(), // 양도화주
                                        Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),    // 양수일자
                                        sCHWONHWAJU,                                // 원화주
                                        Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),   // 양수순번
                                        Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),   // 양도차수
                                        this.TXT01_CHNUMBER1.GetValue().ToString(), // 차량번호1
                                        this.TXT01_CHNUMBER.GetValue().ToString(),  // 차량번호2
                                        this.CBH01_TRUNSONG.GetValue().ToString(),  // 운송사
                                        this.CBH01_CHWONSAN.GetValue().ToString(),  // 원산지
                                        this.TXT01_CHBINNO.GetValue().ToString(),   // BIN 번호
                                        this.TXT01_CHSIKBAEL.GetValue().ToString(), // 식별자
                                        TYUserInfo.EmpNo                            // 등록자 사번
                                        );
            }
            else if (fsWKGUBUN == "UPT")
            {
                if (fsCHANGE_BIN == "CHANGE")
                {
                    this.DbConnector.Attach("TY_P_US_9AGA0355",
                                            this.TXT01_CHBINNO.GetValue().ToString(),   // BIN 번호
                                            TYUserInfo.EmpNo,                           // 사번
                                            Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                            Get_Numeric(this.TXT01_CHTKNO.GetValue().ToString()),    // 순번
                                            this.TXT01_CHNUMBER.GetValue().ToString()
                                            );
                }
                else
                {
                    if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
                    {
                        sCHYNCHQTY = this.TXT01_CHMTQTY.GetValue().ToString();
                    }
                    else
                    {
                        sCHYNCHQTY = "0";
                    }

                    this.DbConnector.Attach("TY_P_US_9AGAZ357",
                                            this.TXT01_CHTOTAL.GetValue().ToString(),   // 실차 중량
                                            this.TXT01_CHMTQTY.GetValue().ToString(),   // 출고 M/T 량
                                            sCHYNCHQTY,                                 // 양수 출고 M/T 량
                                            this.TXT01_CHSIKBAEL.GetValue().ToString(), // 식별자
                                            TYUserInfo.EmpNo,                           // 사번
                                            Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                            Get_Numeric(this.TXT01_CHTKNO.GetValue().ToString()),    // 순번
                                            this.TXT01_CHNUMBER.GetValue().ToString()   // 차량번호2
                                            );
                }
            }
            else if (fsWKGUBUN == "DEL")
            {
                this.DbConnector.Attach("TY_P_US_9AM8Z400",
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                        Get_Numeric(this.TXT01_CHTKNO.GetValue().ToString()),    // 순번
                                        this.TXT01_CHNUMBER.GetValue().ToString()   // 차량번호2
                                        );
            }
        }
        #endregion

        #region Description : 출고 로그 파일 업데이트
        private void UP_USICHLOGNF_UPDATE()
        {
            string sCHYNCHQTY = string.Empty;
            string sCHWONHWAJU = string.Empty;

            if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
            {
                sCHYNCHQTY = this.TXT01_CHMTQTY.GetValue().ToString();
                sCHWONHWAJU = fsCHWONHWAJU;
            }
            else
            {
                sCHYNCHQTY = "0";
                sCHWONHWAJU = "";
            }

            this.DbConnector.Attach("TY_P_US_995FD194",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),
                                    this.TXT01_CHHWAJU.GetValue().ToString(),
                                    this.TXT01_CHBLNO.GetValue().ToString(),
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                    Get_Numeric(this.TXT01_CHTKNO.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHHMNO1.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHHMNO2.GetValue().ToString()),
                                    this.CBO01_CHGEGUNGB.GetValue().ToString(), // 계근대 구분
                                    Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHTOTAL.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()),
                                    Get_Numeric(sCHYNCHQTY),
                                    this.TXT01_CHYNGUBUN.GetValue().ToString(),
                                    this.CBH01_CHYNHWAJU.GetValue().ToString(),
                                    Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),    // 양수일자
                                    sCHWONHWAJU,
                                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),
                                    this.TXT01_CHNUMBER1.GetValue().ToString(),
                                    this.TXT01_CHNUMBER.GetValue().ToString(),
                                    this.CBH01_TRUNSONG.GetValue().ToString(),
                                    this.CBH01_CHWONSAN.GetValue().ToString(),
                                    this.TXT01_CHBINNO.GetValue().ToString(),   // BIN 번호
                                    TYUserInfo.EmpNo                            // 사번
                                    );
        }
        #endregion

        #region Description : BIN 상태관리 업데이트 체크
        private void UP_BIN_STATUSMF_UPTCHECK(string sBINGUBUN)
        {
            double dSJUNILQTY = 0;
            double dSIPGOQTY = 0;
            double dSEPGOQTY = 0;
            double dSEPCHQTY = 0;
            double dCHMTQTY = 0;

            string sBINNO = string.Empty;

            DataTable dt = new DataTable();

            if (sBINGUBUN == "M")
            {
                sBINNO = this.TXT01_CHBINNO.GetValue().ToString();
            }
            else if (sBINGUBUN == "L1")
            {
                sBINNO = fsCLINKBIN1;
            }
            else if (sBINGUBUN == "L2")
            {
                sBINNO = fsCLINKBIN2;
            }

            dCHMTQTY = Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()));

            // BIN 상태관리 재고량 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AIEM371",
                                    Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                    sBINNO);  // BIN 번호

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dSJUNILQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SJUNILQTY"].ToString()));
                dSIPGOQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SIPGOQTY"].ToString()));
                dSEPGOQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SEPGOQTY"].ToString()));
                dSEPCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SEPCHQTY"].ToString()));

                if (sBINGUBUN == "M")
                {
                    fdSCHULQTY_M = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SCHULQTY"].ToString()));

                    // 재고량 = 전일재고 + (입고량 + 이고입고량) - (이고출고량 + 출고량 + 현재출고량)
                    fdSJEGOQTY_M = Convert.ToDouble(String.Format("{0,9:N3}", dSJUNILQTY + (dSIPGOQTY + dSEPGOQTY) - (dSEPCHQTY + fdSCHULQTY_M + dCHMTQTY)));
                    // 출고량 = 출고량 + 현재출고량
                    fdSCHULQTY_M = Convert.ToDouble(String.Format("{0,9:N3}", fdSCHULQTY_M + dCHMTQTY));
                }
                else if (sBINGUBUN == "L1")
                {
                    fdSCHULQTY_L1 = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SCHULQTY"].ToString()));

                    // 재고량 = 전일재고 + (입고량 + 이고입고량) - (이고출고량 + 출고량 + 현재출고량)
                    fdSJEGOQTY_L1 = Convert.ToDouble(String.Format("{0,9:N3}", dSJUNILQTY + (dSIPGOQTY + dSEPGOQTY) - (dSEPCHQTY + fdSCHULQTY_L1 + dCHMTQTY)));
                    // 출고량 = 출고량 + 현재출고량
                    fdSCHULQTY_L1 = Convert.ToDouble(String.Format("{0,9:N3}", fdSCHULQTY_L1 + dCHMTQTY));
                }
                else if (sBINGUBUN == "L2")
                {
                    fdSCHULQTY_L2 = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SCHULQTY"].ToString()));

                    // 재고량 = 전일재고 + (입고량 + 이고입고량) - (이고출고량 + 출고량 + 현재출고량)
                    fdSJEGOQTY_L2 = Convert.ToDouble(String.Format("{0,9:N3}", dSJUNILQTY + (dSIPGOQTY + dSEPGOQTY) - (dSEPCHQTY + fdSCHULQTY_L2 + dCHMTQTY)));
                    // 출고량 = 출고량 + 현재출고량
                    fdSCHULQTY_L2 = Convert.ToDouble(String.Format("{0,9:N3}", fdSCHULQTY_L2 + dCHMTQTY));
                }
            }
        }
        #endregion

        #region Description : BIN 상태관리 업데이트
        private void UP_BIN_STATUSMF_UPDATE(string sBINGUBUN)
        {
            string sBINNO = string.Empty;
            double dSJEGOQTY = 0;
            double dSCHULQTY = 0;

            // 재고 업데이트
            if (sBINGUBUN == "M")
            {
                sBINNO = this.TXT01_CHBINNO.GetValue().ToString();
                dSJEGOQTY = fdSJEGOQTY_M;
                dSCHULQTY = fdSCHULQTY_M;
            }
            else if (sBINGUBUN == "L1")
            {
                sBINNO = fsCLINKBIN1;
                dSJEGOQTY = fdSJEGOQTY_L1;
                dSCHULQTY = fdSCHULQTY_L1;
            }
            else if (sBINGUBUN == "L2")
            {
                sBINNO = fsCLINKBIN2;
                dSJEGOQTY = fdSJEGOQTY_L2;
                dSCHULQTY = fdSCHULQTY_L2;
            }

            this.DbConnector.Attach("TY_P_US_9AIFC373",
                                    dSCHULQTY,  // 출고량
                                    dSJEGOQTY,  // 재고량
                                    dSJEGOQTY,  // 마감재고량
                                    Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                    sBINNO);    // BIN 번호
        }
        #endregion

        #region Description : B/L별 재고파일 업데이트
        private void UP_USIJEBLF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9ALH2388",
                                    fdJBCHQTY,
                                    fdJBYSCHQTY,
                                    fdJBJANQTY,
                                    fdJBYSJANQTY,
                                    fdJBJEGOQTY,
                                    this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.TXT01_CHHWAJU.GetValue().ToString(),   // 화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                    Get_Numeric(this.TXT01_CHHMNO1.GetValue().ToString()),   // 화물번호1
                                    Get_Numeric(this.TXT01_CHHMNO2.GetValue().ToString())    // 화물번호2
                                    );
        }
        #endregion

        #region Description : 통관일별 재고파일 업데이트
        private void UP_USIJECSNF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9ALH5389",
                                    fdJCCHQTY,
                                    fdJCYSCHQTY,
                                    fdJCJEGOQTY,
                                    this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.TXT01_CHHWAJU.GetValue().ToString(),   // 화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                    Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),     // 통관차수
                                    this.CBH01_CHYNHWAJU.GetValue().ToString(), // 양수화주
                                    Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),    // 양수일자
                                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),   // 양수순번
                                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),   // 양도차수
                                    fsCHWONHWAJU                                // 원화주
                                    );
        }
        #endregion

        #region Description : 재고파일 업데이트
        private void UP_USIJEGOF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9ALHS390",
                                    fdJGCHQTY,
                                    fdJGYSCHQTY,
                                    fdJGJANQTY,
                                    fdJGYSJANQTY,
                                    fdJGJEGOQTY,
                                    this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.TXT01_CHHWAJU.GetValue().ToString()    // 화주
                                    );
        }
        #endregion

        #region Description : 통관파일 업데이트
        private void UP_USICUSTF_UPDATE()
        {
            string sCSHWAJU = string.Empty;

            if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
            {
                sCSHWAJU = fsCHWONHWAJU;
            }
            else
            {
                sCSHWAJU = this.TXT01_CHHWAJU.GetValue().ToString();
            }

            this.DbConnector.Attach("TY_P_US_9ALIK391",
                                    fdCSCHQTY,
                                    fdCSJGQTY,
                                    this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    sCSHWAJU,                                   // 화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                    Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString())      // 통관차수
                                    );
        }
        #endregion

        #region Description : 양수도 파일 업데이트
        private void UP_USIYANGNF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9ALGF387",
                                    this.TXT01_CHMTQTY.GetValue().ToString(),   // 출고량
                                    this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.CBH01_CHYNHWAJU.GetValue().ToString(), // 양도화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                    Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),     // 통관차수
                                    this.TXT01_CHHWAJU.GetValue().ToString(),   // 양수화주
                                    Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),    // 양수일자
                                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),   // 양수순번
                                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),   // 양도차수
                                    fsCHWONHWAJU                                // 원화주
                                    );
        }
        #endregion

        #region Descriptoin : 출고 누계 파일 업데이트 체크
        private string UP_USICHNUF_UPTCHECK()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ALER381",
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "").Substring(0, 4)),   // 년
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "").Substring(4, 2)),   // 월
                                    this.TXT01_CHHWAJU.GetValue().ToString(),   // 출고화주
                                    this.CBH01_CHGOKJONG.GetValue().ToString()  // 곡종
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                return "UPT";
            }
            else
            {
                return "NEW";
            }
        }
        #endregion

        #region Description : 출고 누계 파일 업데이트
        private void UP_USICHNUF_UPDATE(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.DbConnector.Attach("TY_P_US_9ALET382",
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "").Substring(0, 4)),   // 년
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "").Substring(4, 2)),   // 월
                                        this.TXT01_CHHWAJU.GetValue().ToString(),   // 출고화주
                                        this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                        this.TXT01_CHMTQTY.GetValue().ToString()    // 출고량
                                        );
            }
            else if (sGUBUN == "UPT")
            {
                this.DbConnector.Attach("TY_P_US_9ALEW383",
                                        this.TXT01_CHMTQTY.GetValue().ToString(),   // 출고량
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "").Substring(0, 4)),   // 년
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "").Substring(4, 2)),   // 월
                                        this.TXT01_CHHWAJU.GetValue().ToString(),   // 출고화주
                                        this.CBH01_CHGOKJONG.GetValue().ToString()  // 곡종
                                        );
            }
        }
        #endregion

        #region Description : BIN 입고파일 업데이트
        private void UP_USIBNIPF_UPDATE()
        {
            //this.DbConnector.Attach("TY_P_US_9ALEL380",
            //                        this.TXT01_CHMTQTY.GetValue().ToString(),   // 출고량
            //                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
            //                        this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
            //                        this.TXT01_CHBINNO.GetValue().ToString()    // BIN 번호
            //                        );

            this.DbConnector.Attach("TY_P_US_A34E6005",
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.TXT01_CHBINNO.GetValue().ToString()    // BIN 번호
                                    );
        }
        #endregion

        #region Description : BIN 작업 파일 업데이트 체크
        private string UP_USIBNSEQF_UPTCHECK(string sGUBUN)
        {
            string sBINNO = string.Empty;

            if (sGUBUN == "GHANGE")
            {
                sBINNO = fsPREBINNO;
            }
            else
            {
                sBINNO = this.TXT01_CHBINNO.GetValue().ToString();
            }

            DataTable dt = new DataTable();

            // BIN 작업차량 파일 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AFFP346",
                                    Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                    sBINNO);                                                                                                // BIN 번호

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                return "UPT";
            }
            else
            {
                return "NEW";
            }
        }
        #endregion

        #region Description : BIN 작업 파일 업데이트
        private void UP_USIBNSEQF_UPDATE(string sGUBUN, string sSEQFGUBUN)
        {
            string sBINNO = string.Empty;

            if (sGUBUN == "CHANGE")
            {
                sBINNO = fsPREBINNO;
            }
            else
            {
                sBINNO = this.TXT01_CHBINNO.GetValue().ToString();
            }

            if (sSEQFGUBUN == "UPT")
            {
                // UPDATE
                if (sGUBUN == "NEW")
                {
                    // 차량 대수 1 증가
                    this.DbConnector.Attach("TY_P_US_9AFFQ347",
                                            Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                            sBINNO);                                                                                                // BIN 번호
                }
                else
                {
                    // 차량 대수 1 감소
                    this.DbConnector.Attach("TY_P_US_9AIDD368",
                                            Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                            sBINNO);                                                                                                // BIN 번호
                }
            }
            else
            {
                // 신규 등록
                this.DbConnector.Attach("TY_P_US_9AFFS348",
                                        Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                        sBINNO,                                                                                                 // BIN 번호
                                        "1");                                                                                                   // 차량대수
            }
        }
        #endregion

        #region Description : BIN 상태파일 업데이트 체크
        private string UP_USIBNSTF_UPTCHECK()
        {
            DataTable dt = new DataTable();

            // KILL TIME 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AHJT365",
                                    this.CBH01_TRHYUNGT.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["CDDESC2"].ToString();
            }
            else
            {
                return "0000";
            }
        }
        #endregion

        #region Descriptoin : BIN 상태파일 업데이트
        private void UP_USIBNSTF_UPDATE(string sGUBUN, string sBTKILL)
        {
            string sBINNO = string.Empty;

            if (sGUBUN == "CHANGE")
            {
                sBINNO = fsPREBINNO;
            }
            else
            {
                sBINNO = this.TXT01_CHBINNO.GetValue().ToString();
            }


            if (sGUBUN == "NEW")
            {
                this.DbConnector.Attach("TY_P_US_9AFG6350",
                                        sBINNO,                                     // BIN 번호
                                        this.TXT01_CHNUMBER.GetValue().ToString(),  // 차량번호2
                                        fsRFID,                                     // RFID
                                        Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),   // 출고일자
                                        this.TXT01_CHTKNO.GetValue().ToString(),    // 순번
                                        this.TXT01_BTCHTIME.GetValue().ToString(),  // 출고시간(초)
                                        this.CBH01_TRHYUNGT.GetValue().ToString(),  // 차량형태
                                        sBTKILL,                                    // KILL TIME
                                        this.TXT01_BTCOUNT.GetValue().ToString(),   // GATE 허용 횟수
                                        this.TXT01_CHHWAJU.GetValue().ToString(),   // 화주
                                        this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                        this.CBH01_CHWONSAN.GetValue().ToString()   // 원산지
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_US_9AG9V354",
                                        sBINNO,                                     // BIN 번호
                                        this.TXT01_CHNUMBER.GetValue().ToString()   // 차량번호2
                                        );
            }

        }
        #endregion

        #region Description : 계근관련 화주정보 파일 업데이트 체크
        private string UP_USAHWAJUF_UPTCHECK()
        {
            DataTable dt = new DataTable();

            // 출고관련 화주정보 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9A8FG296",
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),  // 출고일자
                                    this.TXT01_CHHWAJU.GetValue().ToString()); // 출고화주

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                return "UPT";
            }
            else
            {
                return "NEW";
            }
        }
        #endregion

        #region Descriptoin : 계근관련 화주정보 파일 업데이트
        private void UP_USAHWAJUF_UPDATE(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.DbConnector.Attach("TY_P_US_9ALFZ384",
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                        this.TXT01_CHHWAJU.GetValue().ToString(),   // 화주
                                        this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                        this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                        this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                        Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                        Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                        Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                        Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),     // 통관차수
                                        this.CBH01_CHYNHWAJU.GetValue().ToString(), // 양도화주
                                        Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),    // 양수일자
                                        Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),   // 양도차수
                                        Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),   // 양수순번
                                        "",                                         // 양수도차수
                                        Get_Numeric(this.TXT01_CHHMNO1.GetValue().ToString()),   // 화물번호1
                                        Get_Numeric(this.TXT01_CHHMNO2.GetValue().ToString())    // 화물번호2
                                        );
            }
            else if (sGUBUN == "UPT")
            {
                this.DbConnector.Attach("TY_P_US_9ALG0385",
                                        this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                        this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                        this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                        Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                        Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                        Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                        Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),     // 통관차수
                                        this.CBH01_CHYNHWAJU.GetValue().ToString(), // 양도화주
                                        Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),    // 양수일자
                                        Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),   // 양도차수
                                        Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),   // 양수순번
                                        "",                                         // 양수도차수
                                        Get_Numeric(this.TXT01_CHHMNO1.GetValue().ToString()),   // 화물번호1
                                        Get_Numeric(this.TXT01_CHHMNO2.GetValue().ToString()),   // 화물번호2
                                        Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),   // 출고일자
                                        this.TXT01_CHHWAJU.GetValue().ToString()    // 화주
                                        );
            }
        }
        #endregion

        #region Description : 통관일별 재고파일 가상 출고량 계산
        private void UP_JCIMCHQTY_UPDATE()
        {
            string sProcedureNO = string.Empty;

            if (fsWKGUBUN == "NEW")
            {
                // 출고량 증가 
                sProcedureNO = "TY_P_US_9AFGL352";
            }
            else
            {
                // 출고량 감소
                sProcedureNO = "TY_P_US_9AFGM353";
            }

            this.DbConnector.Attach(sProcedureNO,
                                    fsJCIMCHQTY,                                // 가상출고량
                                    this.CBH01_CHHANGCHA.GetValue().ToString(), // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.TXT01_CHHWAJU.GetValue().ToString(),   // 화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),    // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),   // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),   // HSN
                                    Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),    // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),     // 통관차수
                                    this.CBH01_CHYNHWAJU.GetValue().ToString(), // 양도화주
                                    Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),    // 양수일자
                                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),   // 양수순번
                                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),   // 양도차수
                                    fsCHWONHWAJU);                              // 원화주
        }
        #endregion

        #region Description : 양수도내역 체크
        private string UP_USIYANGNF_Check()
        {
            string sMsg = "OK";

            double dYNYSQTY = 0;
            double dYNYSCHQTY = 0;
            double dMTQTY = 0;
            double dAFTER_YNYSQTY = 0;

            DataTable dt = new DataTable();

            if (this.CBH01_CHYNHWAJU.GetValue().ToString() == "")
            {
                // "1" 양도화주를 입력하세요./CBH01_CHYNHWAJU
                return "1";
            }
            if (Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim()) == "")
            {
                // "2" 양수일자를 입력하세요./MTB01_CHYNILJA
                return "2";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ABD0315",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),     // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),     // 곡종
                                    this.CBH01_CHYNHWAJU.GetValue().ToString(),     // 양도화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),        // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),       // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),       // HSN
                                    Get_Numeric(Get_Date(MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())), // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),         // 통관차수
                                    this.TXT01_CHHWAJU.GetValue().ToString(),       // 화주
                                    Get_Numeric(Get_Date(MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())), // 양수일자
                                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),       // 양수순번
                                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),       // 양도차수
                                    fsCHWONHWAJU);                                  // 원화주

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dYNYSQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["YNYSQTY"].ToString()));
                dYNYSCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["YNYSCHQTY"].ToString()));
                fsCHSIKBAEL = dt.Rows[0]["YNSIKBAEL"].ToString();
                this.TXT01_CHSIKBAEL.SetValue(fsCHSIKBAEL);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9ABDM316",
                                        this.CBH01_CHHANGCHA.GetValue().ToString(),     // 항차
                                        this.CBH01_CHGOKJONG.GetValue().ToString(),     // 곡종
                                        this.TXT01_CHHWAJU.GetValue().ToString(),       // 화주
                                        this.TXT01_CHBLNO.GetValue().ToString(),        // B/L 번호
                                        Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),       // MSN
                                        Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),       // HSN
                                        Get_Numeric(Get_Date(MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())), // 통관일자
                                        Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),         // 통관차수
                                        this.CBH01_CHYNHWAJU.GetValue().ToString(),     // 양도화주
                                        Get_Numeric(Get_Date(MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())), // 양수일자
                                        Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),       // 양도차수
                                        fsCHWONHWAJU);                                  // 원화주

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dAFTER_YNYSQTY = Convert.ToDouble(String.Format("{0,9:N3}", dAFTER_YNYSQTY + Convert.ToDouble(Get_Numeric(dt.Rows[i]["YNYSCHQTY"].ToString()))));
                    }
                }
                if (fsWKGUBUN == "UPT")
                {
                    dMTQTY = Convert.ToDouble(String.Format("{0,9:N3}", dYNYSQTY - (dYNYSCHQTY + dAFTER_YNYSQTY)));
                    this.TXT01_CHMTQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(TXT01_CHTOTAL.GetValue().ToString().Trim())) - Convert.ToDouble(Get_Numeric(TXT01_CHEMPTY.GetValue().ToString().Trim()))))));

                    if (dMTQTY < Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())))
                    {
                        // "3" sMsg = "양수 잔량이 부족합니다./CBH01_CHYNHWAJU";
                        return "3";
                    }
                }
            }
            else
            {
                // "4" sMsg = "양수도 내역이 존재하지 않습니다./CBH01_CHYNHWAJU";
                return "4";
            }

            return sMsg;
        }
        #endregion

        #region Description : 공차 체크
        private string UP_GONGCHA_Check()
        {
            string sMSG = "OK";

            double dBIAREA = 0;
            double dBICHQTY = 0;
            string sBTCHTIME = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AHIE364",
                                    this.TXT01_CHNUMBER1.GetValue().ToString(),
                                    this.TXT01_CHNUMBER.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.TXT01_CHHWAJU.GetValue().ToString() != dt.Rows[0]["TRHWAJU1"].ToString() &&
                    this.TXT01_CHHWAJU.GetValue().ToString() != dt.Rows[0]["TRHWAJU2"].ToString() &&
                    this.TXT01_CHHWAJU.GetValue().ToString() != dt.Rows[0]["TRHWAJU3"].ToString())
                {
                    // "1" sMSG = "이 차량에 대해 공차할 수 없는 화주입니다./TXT01_CHHWAJU";
                    return "1";
                }

                fsRFID = dt.Rows[0]["TRRFID"].ToString();
            }
            else
            {
                // "2" sMSG = "미등록 차량입니다./TXT01_CHNUMBER";
                return "2";
            }
            if (fsRFID == "")
            {
                // "3" "RFID 카드 발급후 작업하세요./TXT01_CHNUMBER";
                return "3";
            }

            // BIN 출고 MAST 파일 체크 (USABINF) 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ABEP319",
                                    this.TXT01_CHBINNO.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dBIAREA = Convert.ToDouble(Get_Numeric(dt.Rows[0]["BIAREA"].ToString()));
                dBICHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["BICHQTY"].ToString()));

                // 출고시간 
                if (dBIAREA == 0 || dBICHQTY == 0 || Convert.ToDouble(Get_Numeric(fsJCIMCHQTY)) == 0)
                {
                    sBTCHTIME = "0";
                }
                else
                {
                    sBTCHTIME = String.Format("{0:#0}", (Convert.ToDouble(Get_Numeric(fsJCIMCHQTY)) * 1000) / (dBIAREA * dBICHQTY) - 0.5);
                }
                if (sBTCHTIME.Length > 4)
                {
                    this.TXT01_BTCHTIME.SetValue(sBTCHTIME.Substring(sBTCHTIME.Length - 4, 4));
                }
                else
                {
                    this.TXT01_BTCHTIME.SetValue(sBTCHTIME);
                }
            }
            else
            {
                // "4" sMSG = "BIN 출고 MAST 파일에 등록된 자료가 없습니다./TXT01_CHHWAJU";
                return "4";
            }


            // BIN 작업현황 파일 동일 차량 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ABF4320",
                                    this.TXT01_CHNUMBER.GetValue().ToString(),
                                    fsRFID);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // "5" sMSG = "SILO내에 같은번호의 차량이 작업중입니다./TXT01_CHNUMBER";
                return "5";
            }

            string sNOWDATE = System.DateTime.Now.ToString("yyyyMMdd");
            if (Convert.ToDouble(Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""))) > Convert.ToDouble(Get_Numeric(sNOWDATE)))
            {
                // "6" "출고일자가 현재일자보다 큽니다.DTP01_CHCHULDAT
                return "6";
            }



            return sMSG;
        }
        #endregion

        #region Description : 실차 체크
        private string UP_SILCHA_Check()
        {
            string sMSG = "OK";

            string sCSHWAJU = string.Empty;

            double dMTQTY = 0;

            DataTable dt = new DataTable();

            if (Convert.ToDouble(Get_Numeric(this.TXT01_CHTOTAL.GetValue().ToString())) <= 0)
            {
                // "1" "실차무계를 입력하세요./TXT01_CHTOTAL"
                return "1";
            }

            if (Convert.ToDouble(Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString())) > Convert.ToDouble(Get_Numeric(this.TXT01_CHTOTAL.GetValue().ToString())))
            {
                // "2" "공차무계가 실차무계보다 큽니다./TXT01_CHTOTAL"
                return "2";
            }

            // 공차등록 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AAJ2313",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),
                                    this.TXT01_CHHWAJU.GetValue().ToString(),
                                    this.TXT01_CHBLNO.GetValue().ToString(),
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),
                                    Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),
                                    Get_Numeric(this.TXT01_CHTKNO.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                // "3" "공차 내역이 존재하지 않습니다./TXT01_CHHWAJU"
                return "3";
            }

            // 실중량 = 실차 - 공차
            this.TXT01_CHMTQTY.SetValue(String.Format("{0,9:N3}", Convert.ToDouble(Get_Numeric(this.TXT01_CHTOTAL.GetValue().ToString())) - Convert.ToDouble(Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString()))));

            // 통관재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ABFW321",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),     // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),     // 곡종
                                    this.TXT01_CHHWAJU.GetValue().ToString(),       // 화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),        // B/L번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),       // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),       // HSN
                                    Get_Numeric(Get_Date(MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())), // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),         // 통관차수
                                    this.CBH01_CHYNHWAJU.GetValue().ToString(),     // 양도화주
                                    Get_Numeric(Get_Date(MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())), // 양수일자
                                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),       // 양수순번
                                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),       // 양도차수
                                    fsCHWONHWAJU);                                  // 원화주

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
                {
                    dMTQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCYSJANQTY"].ToString()));
                }
                else
                {
                    dMTQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCCSJANQTY"].ToString()));
                }

                fdJCCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCCHQTY"].ToString()));
                fdJCYSCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCYSCHQTY"].ToString()));
                fdJCCSQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCCSQTY"].ToString()));
                fdJCYSQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCYSQTY"].ToString()));
                fdJCYDQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCYDQTY"].ToString()));
                fdJCYSYDQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JCYSYDQTY"].ToString()));
            }
            else
            {
                // "4" "일자별 통관재고 자료가 존재하지 않습니다./TXT01_CHTOTAL"
                return "4";
            }

            if (dMTQTY < Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())))
            {
                // "5" "출고가능 수량을 초과했습니다./TXT01_CHTOTAL"
                return "5";
            }

            if (Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())) > Convert.ToDouble(Get_Numeric(fsJCIMCHQTY)))
            {
                // "6" "차량 중량 초과입니다./TXT01_CHTOTAL"
                return "6";
            }

            // 화주별 재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AEBX325",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),     // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),     // 곡종
                                    this.TXT01_CHHWAJU.GetValue().ToString()        // 화주
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fdJGCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JGCHQTY"].ToString()));
                fdJGYSCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JGYSCHQTY"].ToString()));
                fdJGHWAKQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JGHWAKQTY"].ToString()));
                fdJGYDQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JGYDQTY"].ToString()));
                fdJGYSQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JGYSQTY"].ToString()));
                fdJGYSYDQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JGYSYDQTY"].ToString()));
            }
            else
            {
                // "7" "화주별 재고(USIJEGOF) 자료가 존재하지 않습니다./TXT01_CHHWAJU"
                return "7";
            }

            // 통관 내역 체크
            if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
            {
                sCSHWAJU = fsCHWONHWAJU;
            }
            else
            {
                sCSHWAJU = this.TXT01_CHHWAJU.GetValue().ToString();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AEDW326",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),     // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),     // 곡종
                                    sCSHWAJU,                                       // 화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),        // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),       // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),       // HSN
                                    Get_Numeric(Get_Date(MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())), // 통관일자
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString())
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fdCSQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["CSQTY"].ToString()));
                fdCSCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["CSCHQTY"].ToString()));
            }
            else
            {
                // "8" "통관(USICUSTF) 자료가 존재하지 않습니다./TXT01_CHHWAJU"
                return "8";
            }

            // B/L별 재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AEF8327",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),     // 항차
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),     // 곡종
                                    this.TXT01_CHHWAJU.GetValue().ToString(),       // 화주
                                    this.TXT01_CHBLNO.GetValue().ToString(),        // B/L 번호
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),       // MSN
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),       // HSN
                                    Get_Numeric(this.TXT01_CHHMNO1.GetValue().ToString()),       // 화물번호1
                                    Get_Numeric(this.TXT01_CHHMNO2.GetValue().ToString())        // 화물번호2
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fdJBCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JBCHQTY"].ToString()));
                fdJBYSCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JBYSCHQTY"].ToString()));
                fdJBHWAKQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JBHWAKQTY"].ToString()));
                fdJBYDQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JBYDQTY"].ToString()));
                fdJBYSQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JBYSQTY"].ToString()));
                fdJBYSYDQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JBYSYDQTY"].ToString()));
                fdJBCSQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["JBCSQTY"].ToString()));
            }
            else
            {
                // "9" "B/L별 재고(USIJEBLF) 자료가 존재하지 않습니다./TXT01_CHHWAJU"
                return "9";
            }

            if (this.TXT01_CHYNGUBUN.GetValue().ToString() == "R")
            {
                fdJBYSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJBYSCHQTY + Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()))));
                fdJGYSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJGYSCHQTY + Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()))));
                fdJCYSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJCYSCHQTY + Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()))));
            }
            else
            {
                fdJBCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJBCHQTY + Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()))));
                fdJGCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJGCHQTY + Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()))));
                fdJCCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJCCHQTY + Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()))));
            }
            fdCSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCSCHQTY + Convert.ToDouble(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()))));

            // B/L별 재고파일        
            fdJBJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJBHWAKQTY - (fdJBYDQTY + fdJBCHQTY)));
            fdJBYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJBYSQTY - (fdJBYSCHQTY + fdJBYSYDQTY)));
            fdJBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdJBHWAKQTY + fdJBYSQTY) - (fdJBYDQTY + fdJBYSYDQTY + fdJBCHQTY + fdJBYSCHQTY)));

            // 통관일별 재고파일
            fdJCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdJCCSQTY + fdJCYSQTY) - (fdJCYDQTY + fdJCYSYDQTY + fdJCCHQTY + fdJCYSCHQTY)));

            // 재고파일
            fdJGJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJGHWAKQTY - (fdJGYDQTY + fdJGCHQTY)));
            fdJGYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdJGYSQTY - (fdJGYSCHQTY + fdJGYSYDQTY)));
            fdJGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (fdJGHWAKQTY + fdJGYSQTY) - (fdJGYDQTY + fdJGYSYDQTY + fdJGCHQTY + fdJGYSCHQTY)));

            // 통관파일
            fdCSJGQTY = Convert.ToDouble(String.Format("{0,9:N3}", fdCSQTY - fdCSCHQTY));

            return sMSG;
        }
        #endregion

        #region Description : 출고증 출력
        private void UP_SiloPrint(string sCHCHULDAT, string sCHTKNO, string sCHDCHULNUM)
        {
            try
            {
                string sTKNO = string.Empty;
                double dCHEMPTY = 0;
                double dCHMTQTY = 0;
                double dCHTOTAL = 0;

                DataTable dt = new DataTable();

                if (Get_Numeric(sCHDCHULNUM) != "0")
                {
                    // 잔량처리

                    // 잔량 처리 데이터 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_98EGB120",
                                            sCHCHULDAT,
                                            sCHDCHULNUM);

                    dt = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (sTKNO != "")
                        {
                            sTKNO += "," + dt.Rows[i]["CHTKNO"].ToString();
                        }
                        else
                        {
                            sTKNO = dt.Rows[i]["CHTKNO"].ToString();
                            dCHEMPTY = Convert.ToDouble(dt.Rows[i]["CHEMPTY"].ToString());
                        }
                        dCHMTQTY += Convert.ToDouble(dt.Rows[i]["CHMTQTY"].ToString());
                    }

                    dCHTOTAL = dCHEMPTY + dCHMTQTY;
                }

                // 출고 데이터 조회
                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_US_98EGY121",
                this.DbConnector.Attach("TY_P_US_B1E9N308",
                                        sTKNO,
                                        sTKNO,
                                        dCHTOTAL,
                                        dCHTOTAL,
                                        dCHMTQTY,
                                        dCHMTQTY,
                                        sCHCHULDAT,
                                        sCHTKNO);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    System.Drawing.Image qrimage = null;

                    if (dt.Rows[0]["CHHWAJUCODE"].ToString() == "J29")
                    {
                        qrimage = UP_SetQrcode(dt.Rows[0]["ID"].ToString(),           // 차량번호
                                               dt.Rows[0]["TRUNNAME"].ToString(),     // 운전자명
                                               dt.Rows[0]["CHBONSUN"].ToString(),     // 본선명
                                               dt.Rows[0]["CHHWAJUCODE"].ToString(),  // 화주코드
                                               dt.Rows[0]["TARE"].ToString(),         // 공차중량
                                               dt.Rows[0]["GROSS"].ToString(),        // 실차중량
                                               dt.Rows[0]["CHCHULDAT"].ToString(),    // 출고일자
                                               dt.Rows[0]["CHGOKJONGCODE"].ToString(),// 곡종코드
                                               dt.Rows[0]["CHGOKJONGNM"].ToString(),  // 곡종명
                                               dt.Rows[0]["NET"].ToString()           // 실중량
                                                   );
                    }

                    // 특정 프린터 출력
                    SectionReport rpt = new TYUSGA004R(qrimage);

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    rpt.DataSource = dt;
                    rpt.Run(false);

                    this.AVW01_REPORT.Document = rpt.Document;

                    if (this.CKB01_CHPRTGB.Checked == false)
                    {
                        // 외부출력
                        this.AVW01_REPORT.Document.Printer.PrinterName = "SILOGAGUN";
                    }

                    this.AVW01_REPORT.Print(false, false, false);
                }
            }
            catch
            {
                this.ShowCustomMessage("출고증 출력 중 오류가 발생하였습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region Description : QRCODE 생성 OK
        private System.Drawing.Image UP_SetQrcode(string sCARNO, string sDRIVERNM, string sBONSUN, string sHWAJU, string sTARE, string sGROSS,
                                                  string sCHULDAT, string sGOKJONG, string sGOKJONGNM, string sNET)
        {
            // 차량번호, 운전자명, 본선명, 화주코드, 공차중량, 실차중량, 곡종, 곡종명, 총중량 
            // 생성할 이미지 사이즈 정함.
            int sizeNum = 1;
            int level = 0;

            string sCHEMPY = string.Empty;
            string sCHTOTAL = string.Empty;
            string sCHMTQTY = string.Empty;
            string sCHGOKJONG = string.Empty;

            if (sGOKJONG == "12")
            {
                sCHGOKJONG = "31-0010";
            }
            else if (sGOKJONG == "15")
            {
                sCHGOKJONG = "31-0020";
            }
            else
            {
                sCHGOKJONG = sGOKJONG;
            }

            sCHEMPY = string.Format("{0:#}", (Convert.ToDouble(Get_Numeric(sTARE)) * 1000));
            sCHTOTAL = string.Format("{0:#}", (Convert.ToDouble(Get_Numeric(sGROSS)) * 1000));
            sCHMTQTY = string.Format("{0:#}", (Convert.ToDouble(Get_Numeric(sNET)) * 1000));

            StringBuilder cardeFormat = new StringBuilder();

            // 차량 운행 종류(고정:0) , 차량 번호, 차량 종류(고정:2), 운전자 이름, 업체 코드(고정:21012), 업체 이름(고정:(주)태영인더스트리), 모선명, 
            // 차량 공차 중량, 차량 총 중량, 발행일자(년월일시분초), 
            // 창고코드(고정:105), 원료코드(고정/옥수수:31-0010 소맥:31-0020), 원료 이름, 원료 유형(고정:RM), 단위 코드(고정:KG), 무게(실중량), 수량(고정:1), 포장 코드(고정:BLK)
            cardeFormat.Append("0~" + sCARNO + "~2~" + sDRIVERNM + "~21012~(주)태영인더스트리~" + sBONSUN + "~" + sCHEMPY + "~" + sCHTOTAL + "~" + sCHULDAT.Replace("-", "").Replace(".", "") +
                               "~105^" + sCHGOKJONG + "^" + sGOKJONGNM + "(수입)^RM^KG^" + sCHMTQTY + "^1^BLK^");

            // QRCodeEncoder 인스턴스 생성
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            // 인코딩모드는 바이트로 설정.
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            // QRCode 사이즈 지정.
            qrCodeEncoder.QRCodeScale = sizeNum;

            qrCodeEncoder.QRCodeVersion = level;
            // 에러 보정 레벨을 지정.
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            System.Drawing.Image rtnImage = null;

            string data = cardeFormat.ToString();
            try
            {
                // QRCode 이미지를 생성해 줌.
                rtnImage = qrCodeEncoder.Encode(data);
            }
            catch (Exception ex)
            {

            }

            return rtnImage;
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_FieldClear(string sGUBUN)
        {
            //CKB01_CHPRTGB.Checked = false;

            if (sGUBUN.ToString() != "CLR")
            {
                CBO01_CHGEGUNGB.SetValue("A");
                TXT01_CHTKNO.SetValue("");
                TXT01_CHNUMBER1.SetValue("");
                TXT01_CHNUMBER.SetValue("");

                TXT01_CHHWAJU.SetValue("");
                TXT01_CHHWAJUNM.SetValue("");

                CBH01_TRUNSONG.SetValue("");
                CBH01_TRHYUNGT.SetValue("");
                TXT01_BTCOUNT.SetValue("00");
            }

            CBH01_CHGOKJONG.SetValue("");
            CBH01_CHHANGCHA.SetValue("");
            CBH01_CHWONSAN.SetValue("");
            MTB01_CHCUSTIL.SetValue("");
            TXT01_CHSEQ.SetValue("");
            TXT01_CHBLNO.SetValue("");
            TXT01_CHBLMSN.SetValue("");
            TXT01_CHBLHSN.SetValue("");
            TXT01_CHHMNO1.SetValue("");
            TXT01_CHHMNO2.SetValue("");

            TXT01_CHBINNO.SetValue("");
            TXT01_CHYNGUBUN.SetValue("");
            TXT01_CHSIKBAEL.SetValue("");
            CBH01_CHYNHWAJU.SetValue("");
            MTB01_CHYNILJA.SetValue("");
            TXT01_CHYSSEQ.SetValue("");
            TXT01_CHYDSEQ.SetValue("");

            TXT01_TGJUNGRY.SetValue("0");
            TXT01_BTCHTIME.SetValue("");
            TXT01_BTCLICK.SetValue("00");
            MTB01_CHIPTIME.SetValue("");
            TXT01_CHHITIM.SetValue("");
            MTB01_CHCHTIME.SetValue("");

            TXT01_JBHWAKQTY.SetValue("");
            TXT01_JBCHQTY.SetValue("");
            TXT01_JBCSJANQTY.SetValue("");
            TXT01_JBCSQTY.SetValue("");
            TXT01_JBYDQTY.SetValue("");
            TXT01_JBYSQTY.SetValue("");

            TXT01_JCIMCHQTY.SetValue("");
            TXT01_JCCSQTY.SetValue("");
            TXT01_JCCHQTY.SetValue("");
            TXT01_JCJEGOQTY.SetValue("");
            TXT01_JCYSQTY.SetValue("");
            TXT01_JCYDQTY.SetValue("");
            TXT01_JCYSCHQTY.SetValue("");

            TXT01_CHEMPTY.SetValue("");
            TXT01_CHTOTAL.SetValue("");
            TXT01_CHMTQTY.SetValue("");
        }
        #endregion

        #region Description : 변수 초기화
        private void UP_Var_Clear()
        {
            fsWKGUBUN = string.Empty;

            fsCHANGE_BIN = string.Empty;
            fsPREBINNO = string.Empty;
            fsRFID = string.Empty;
            fsJCIMCHQTY = string.Empty;

            fsCHWONHWAJU = string.Empty;
            fsCHHMNO1 = string.Empty;
            fsCHHMNO2 = string.Empty;
            fsCHSIKBAEL = string.Empty;

            fsJBSOSOK = string.Empty;

            fsCLINKBIN1 = string.Empty;   // 연결 BIN 1
            fsCLINKBIN2 = string.Empty;   // 연결 BIN 2

            // 일별 통관재고
            fdJCCHQTY = 0;
            fdJCYSCHQTY = 0;
            fdJCCSQTY = 0;
            fdJCYSQTY = 0;
            fdJCYDQTY = 0;
            fdJCYSYDQTY = 0;
            fdJCJEGOQTY = 0;

            // 화주별 재고
            fdJGCHQTY = 0;
            fdJGYSCHQTY = 0;
            fdJGHWAKQTY = 0;
            fdJGYDQTY = 0;
            fdJGYSQTY = 0;
            fdJGYSYDQTY = 0;
            fdJGJEGOQTY = 0;
            fdJGYSJANQTY = 0;
            fdJGJANQTY = 0;

            // 통관파일
            fdCSQTY = 0;
            fdCSCHQTY = 0;
            fdCSJGQTY = 0;

            // B/L 별 재고
            fdJBCHQTY = 0;
            fdJBYSCHQTY = 0;
            fdJBHWAKQTY = 0;
            fdJBYDQTY = 0;
            fdJBYSQTY = 0;
            fdJBYSYDQTY = 0;
            fdJBCSQTY = 0;
            fdJBJEGOQTY = 0;
            fdJBYSJANQTY = 0;
            fdJBJANQTY = 0;

            // BIN 상태관리
            fdSCHULQTY_M = 0;
            fdSJEGOQTY_M = 0;
            fdSCHULQTY_L1 = 0;
            fdSJEGOQTY_L1 = 0;
            fdSCHULQTY_L2 = 0;
            fdSJEGOQTY_L2 = 0;
        }
        #endregion

        #region Description : 차량번호 조회 버튼
        private void BTN61_INQCARNUM_Click(object sender, EventArgs e)
        {
            TYUSGA01C4 popup = new TYUSGA01C4(this.TXT01_CHNUMBER.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TXT01_CHNUMBER1.SetValue(popup.fsTRNUMNO1);
                TXT01_CHNUMBER.SetValue(popup.fsTRNUMNO2);
                TXT01_CHHWAJU.SetValue(popup.fsTRHWAJU1);
                CBH01_TRUNSONG.SetValue(popup.fsTRUNSONG);
                CBH01_TRHYUNGT.SetValue(popup.fsTRHYUNGT);
                TXT01_BTCOUNT.SetValue(Set_Fill2(popup.fsTRCOUNT));

                UP_Run();

                SetFocus(TXT01_CHHWAJU);
            }
        }
        #endregion 

        #region Description : 재고조회 버튼
        private void BTN61_INQJEGO_Click(object sender, EventArgs e)
        {
            if (this.TXT01_CHHWAJU.GetValue().ToString() != "")
            {
                TYUSGA01C3 popup = new TYUSGA01C3(this.TXT01_CHHWAJU.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TXT01_CHHWAJU.SetValue(popup.fsJCHWAJU);
                    TXT01_CHHWAJUNM.SetValue(popup.fsJCHWAJUNM);
                    CBH01_CHGOKJONG.SetValue(popup.fsJCGOKJONG);
                    CBH01_CHHANGCHA.SetValue(popup.fsJCHANGCHA);
                    MTB01_CHCUSTIL.SetValue(popup.fsJCCUSTIL);
                    TXT01_CHSEQ.SetValue(popup.fsJCSEQ);
                    TXT01_CHBLMSN.SetValue(Set_Fill4(popup.fsJCBLMSN));
                    TXT01_CHBLHSN.SetValue(Set_Fill3(popup.fsJCBLHSN));
                    TXT01_CHBLNO.SetValue(popup.fsJCBLNO);
                    CBH01_CHYNHWAJU.SetValue(popup.fsJCYDHWAJU);
                    TXT01_CHHMNO1.SetValue(popup.fsJCHMNO1);
                    TXT01_CHHMNO2.SetValue(popup.fsJCHMNO2);
                    MTB01_CHYNILJA.SetValue(popup.fsJCYSDATE);
                    TXT01_CHYSSEQ.SetValue(popup.fsJCYSSEQ);
                    TXT01_CHYDSEQ.SetValue(popup.fsJCYDSEQ);

                    if (CBH01_CHYNHWAJU.GetValue().ToString() != "")
                    {
                        TXT01_CHYNGUBUN.SetValue("R");
                        fsCHWONHWAJU = popup.fsJCWNHWAJU;     //원화주
                    }
                    else
                    {
                        TXT01_CHYNGUBUN.SetValue("");
                        CBH01_CHYNHWAJU.SetValue("");
                        TXT01_CHYSSEQ.SetValue("0");
                        TXT01_CHYDSEQ.SetValue("0");
                        fsCHWONHWAJU = this.TXT01_CHHWAJU.GetValue().ToString();
                    }

                    CBH01_CHWONSAN.SetValue(popup.fsJCWONSAN);

                    // 삼양사 울산 2공장 식용옥수수인 경우 게이트 오픈회수 1
                    //if (TXT01_CHHWAJU.GetValue().ToString() == "S70" && CBH01_CHGOKJONG.GetValue().ToString() == "11")
                    //{
                    //    TXT01_BTCOUNT.SetValue("01");
                    //}

                    // 모선재고
                    if (!UP_Sel_USIJEBLF())
                    {
                        this.ShowCustomMessage("USIJEBLF 모선 재고파일이 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.TXT01_CHHWAJU);
                        return;
                    }
                    // 통관재고
                    if (!UP_Sel_USIJECSF())
                    {
                        this.ShowCustomMessage("USIJECSF 통관 일별 재고파일이 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.TXT01_CHHWAJU);
                        return;
                    }
                    // BIN 번호 자동부여
                    if (!UP_Sel_USIBNSEQF())
                    {
                        this.ShowCustomMessage("BIN 입고가 없거나, 일치하는 곡종이 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        SetFocus(this.TXT01_CHHWAJU);
                        return;
                    }
                    SetFocus(this.TXT01_CHEMPTY);
                }
            }
            else
            {
                this.ShowCustomMessage("화주를 입력하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.TXT01_CHHWAJU);
                return;
            }
        }
        #endregion

        #region Description : 출고가능 BIN 조회(USIBNSEQF)
        private bool UP_Sel_USIBNSEQF()
        {
            bool b = true;

            string sTRBINNO = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9A8HH300",
                                    Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),  // 출고일자
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.CBH01_CHWONSAN.GetValue().ToString(),  // 원산지
                                    this.TXT01_CHHWAJU.GetValue().ToString(),   // 화주
                                    Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),  // 출고일자
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.CBH01_CHWONSAN.GetValue().ToString(),  // 원산지
                                    fsJBSOSOK,                                  // 협회
                                    Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),  // 출고일자
                                    this.CBH01_CHGOKJONG.GetValue().ToString(), // 곡종
                                    this.CBH01_CHWONSAN.GetValue().ToString()); // 원산지


            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_CHBINNO.SetValue(dt.Rows[0]["BSBINNO"].ToString());
            }
            else
            {
                b = false;
            }

            // 지정 BIN 배정
            // 차량파일 지정 BIN 번호 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9B5G4479",
                                    this.TXT01_CHNUMBER1.GetValue().ToString(), // 차량번호1
                                    this.TXT01_CHNUMBER.GetValue().ToString()   // 차량번호2
                                    );


            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["TRBINNO"].ToString() != "")
                {
                    sTRBINNO = dt.Rows[0]["TRBINNO"].ToString();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_9AAID310",
                                            Get_Numeric(Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", "")),
                                            this.CBH01_CHGOKJONG.GetValue().ToString(),
                                            sTRBINNO);

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 원산지 체크, 출고가능 여부 체크 
                        if (this.CBH01_CHWONSAN.GetValue().ToString() == dt.Rows[0]["BNWONSAN"].ToString() && dt.Rows[0]["BNCHGN"].ToString() == "Y")
                        {
                            this.TXT01_CHBINNO.SetValue(sTRBINNO);
                        }
                    }
                }
            }

            return b;
        }
        #endregion

        #region Description : 모선 재고 조회(USIJEBLF)
        private bool UP_Sel_USIJEBLF()
        {
            bool b = true;
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AA99305",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),
                                    this.TXT01_CHHWAJU.GetValue().ToString(),
                                    this.TXT01_CHBLNO.GetValue().ToString(),
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHHMNO1.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHHMNO2.GetValue().ToString())
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_JBHWAKQTY.SetValue(dt.Rows[0]["JBHWAKQTY"].ToString());      // 확정량
                this.TXT01_JBCHQTY.SetValue(dt.Rows[0]["JBCHQTY"].ToString());          // 출고량
                this.TXT01_JBCSJANQTY.SetValue(dt.Rows[0]["JBCSJANQTY"].ToString());    // 통관잔량
                this.TXT01_JBCSQTY.SetValue(dt.Rows[0]["JBCSQTY"].ToString());          // 통관량
                this.TXT01_JBYDQTY.SetValue(dt.Rows[0]["JBYDQTY"].ToString());          // 양도량
                this.TXT01_JBYSQTY.SetValue(dt.Rows[0]["JBYSQTY"].ToString());          // 양수량
                fsCHHMNO1 = dt.Rows[0]["JBHMNO1"].ToString();                           // 화물번호1
                fsCHHMNO2 = dt.Rows[0]["JBHMNO2"].ToString();                           // 화물번호2
                fsJBSOSOK = dt.Rows[0]["JBSOSOK"].ToString();                           // 소속(협회)
            }
            else
            {
                b = false;
            }

            return b;
        }
        #endregion

        #region Description : 통관 재고 조회(USIJECSF)
        private bool UP_Sel_USIJECSF()
        {
            bool b = true;
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AA9F306",
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString(),
                                    this.TXT01_CHHWAJU.GetValue().ToString(),
                                    this.TXT01_CHBLNO.GetValue().ToString(),
                                    Get_Numeric(this.TXT01_CHBLMSN.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHBLHSN.GetValue().ToString()),
                                    Get_Numeric(Get_Date(this.MTB01_CHCUSTIL.GetValue().ToString().Replace(" ", "").Trim())),
                                    Get_Numeric(this.TXT01_CHSEQ.GetValue().ToString()),
                                    this.CBH01_CHYNHWAJU.GetValue().ToString(),
                                    Get_Numeric(Get_Date(this.MTB01_CHYNILJA.GetValue().ToString().Replace(" ", "").Trim())),
                                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()),
                                    fsCHWONHWAJU
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_JCCSQTY.SetValue(dt.Rows[0]["JCCSQTY"].ToString());      // 통관량
                this.TXT01_JCCHQTY.SetValue(dt.Rows[0]["JCCHQTY"].ToString());      // 출고량
                this.TXT01_JCJEGOQTY.SetValue(dt.Rows[0]["JCJEGOQTY"].ToString());  // 재고량
                this.TXT01_JCYSQTY.SetValue(dt.Rows[0]["JCYSQTY"].ToString());      // 양수량
                this.TXT01_JCYDQTY.SetValue(dt.Rows[0]["JCYDQTY"].ToString());      // 양도량
                this.TXT01_JCYSCHQTY.SetValue(dt.Rows[0]["JCYSCHQTY"].ToString());  // 양수출고량
                this.TXT01_JCIMCHQTY.SetValue(dt.Rows[0]["JCIMCHQTY"].ToString());  // 가상출고량
            }
            else
            {
                b = false;
            }

            return b;
        }
        #endregion

        #region Description : BIN 별 작업현황 버튼
        private void BTN61_SILOCODEHELP07_Click(object sender, EventArgs e)
        {
            TYUSGA01C1 popup = new TYUSGA01C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_CHCHULDAT.SetValue(popup.fsBTCHULDAT);
                this.TXT01_CHTKNO.SetValue(popup.fsBTTKNO);
                this.TXT01_CHNUMBER.SetValue(popup.fsBTNUMBER);

                UP_Run();
            }
        }
        #endregion

        #region Description : 출고지시 관리 버튼
        private void BTN61_SILOCODEHELP08_Click(object sender, EventArgs e)
        {
            TYUSAU003I popup = new TYUSAU003I("POPUP");
            popup.Width = 1500;
            popup.Height = 850;
            popup.ShowDialog();
        }
        #endregion

        #region Description : 출고우선순위 버튼
        private void BTN61_SILOCODEHELP09_Click(object sender, EventArgs e)
        {
            TYUSGA003I popup = new TYUSGA003I("POPUP");
            popup.Width = 1200;
            popup.Height = 850;
            popup.ShowDialog();
        }
        #endregion

        #region Description : 출고증 출력 버튼
        private void BTN61_SILOCODEHELP10_Click(object sender, EventArgs e)
        {
            TYUSGA004P popup = new TYUSGA004P("POPUP");
            popup.Width = 1500;
            popup.Height = 850;
            popup.ShowDialog();
        }
        #endregion

        #region Description : 계근 현황 버튼
        private void BTN61_SILOCODEHELP11_Click(object sender, EventArgs e)
        {
            TYUSGA01C2 popup = new TYUSGA01C2();

            popup.ShowDialog();
        }
        #endregion

        #region Description : 필드/버튼 잠금
        private void UP_Lock_Field(string sGUBUN)
        {
            if (sGUBUN == "INIT")
            {
                this.CBO01_CHGEGUNGB.SetReadOnly(false);
                this.DTP01_CHCHULDAT.SetReadOnly(false);
                this.TXT01_CHTKNO.SetReadOnly(false);
                this.TXT01_CHNUMBER.SetReadOnly(false);
                this.TXT01_CHHWAJU.SetReadOnly(false);
                this.TXT01_CHEMPTY.SetReadOnly(false);

                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
                this.BTN61_CLICKBTN.Visible = false;
                this.BTN61_INQCARNUM.Visible = true;
                this.BTN61_INQJEGO.Visible = false;
            }
            else if (sGUBUN == "NEW")
            {
                this.CBO01_CHGEGUNGB.SetReadOnly(false);
                this.DTP01_CHCHULDAT.SetReadOnly(false);
                this.TXT01_CHTKNO.SetReadOnly(false);
                this.TXT01_CHNUMBER.SetReadOnly(false);
                this.TXT01_CHHWAJU.SetReadOnly(false);
                this.TXT01_CHEMPTY.SetReadOnly(false);

                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;
                this.BTN61_CLICKBTN.Visible = false;
                this.BTN61_INQCARNUM.Visible = true;
                this.BTN61_INQJEGO.Visible = true;
            }
            else if (sGUBUN == "GONG")
            {
                this.CBO01_CHGEGUNGB.SetReadOnly(true);
                this.DTP01_CHCHULDAT.SetReadOnly(true);
                this.TXT01_CHTKNO.SetReadOnly(true);
                this.TXT01_CHNUMBER.SetReadOnly(true);
                this.TXT01_CHHWAJU.SetReadOnly(true);
                this.TXT01_CHEMPTY.SetReadOnly(true);

                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
                this.BTN61_CLICKBTN.Visible = true;
                this.BTN61_INQCARNUM.Visible = false;
                this.BTN61_INQJEGO.Visible = false;
            }
            else if (sGUBUN == "SIL")
            {
                this.CBO01_CHGEGUNGB.SetReadOnly(true);
                this.DTP01_CHCHULDAT.SetReadOnly(true);
                this.TXT01_CHTKNO.SetReadOnly(true);
                this.TXT01_CHNUMBER.SetReadOnly(true);
                this.TXT01_CHHWAJU.SetReadOnly(true);
                this.TXT01_CHEMPTY.SetReadOnly(true);

                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
                this.BTN61_CLICKBTN.Visible = false;
                this.BTN61_INQCARNUM.Visible = false;
                this.BTN61_INQJEGO.Visible = false;
            }
        }
        #endregion

        #region Description : 포커스 이동
        private void TXT01_CHNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQCARNUM);
            }
        }

        private void TXT01_CHHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQJEGO);
            }
        }

        private void TXT01_CHHWAJUNM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQJEGO);
            }
        }

        private void CBO01_GONGCHAGB_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TXT01_CHBINNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsWKGUBUN == "NEW")
                {
                    SetFocus(this.TXT01_CHEMPTY);
                }
                else if (fsWKGUBUN == "UPT")
                {
                    if (fsPREBINNO != this.TXT01_CHBINNO.GetValue().ToString())
                    {
                        SetFocus(this.BTN61_SAV);
                    }
                    else
                    {
                        SetFocus(this.TXT01_CHTOTAL);
                    }
                }
            }
        }

        private void TXT01_CHSIKBAEL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.CBH01_CHYNHWAJU.CodeText);
            }
        }

        private void CBH01_CHYNHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.MTB01_CHYNILJA);
            }
        }

        private void TXT01_CHEMPTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsWKGUBUN == "NEW")
                {
                    SetFocus(this.BTN61_SAV);
                }
                else
                {
                    SetFocus(this.TXT01_CHTOTAL);
                }
            }
        }

        private void TXT01_CHTOTAL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }

        private void TXT01_CJNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion

        #region Description : F1 key 이벤트
        private void TXT01_CHNUMBER1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                if (this.TXT01_CHNUMBER1.ReadOnly == false)
                {
                    BTN61_INQCARNUM_Click(null, null);
                }
            }
        }

        private void TXT01_CHNUMBER_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                if (this.TXT01_CHNUMBER.ReadOnly == false)
                {
                    BTN61_INQCARNUM_Click(null, null);
                }
            }
        }

        private void TXT01_CHHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                if (this.TXT01_CHHWAJU.ReadOnly == false && BTN61_SAV.Visible == true)
                {
                    BTN61_INQJEGO_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 실차삭제 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            TYUSGA005I popup = new TYUSGA005I();
            popup.Width = 1500;
            popup.Height = 850;
            popup.ShowDialog();
        }
        #endregion

        #region Description : 게이트 오픈 초기화 버튼
        private void BTN61_CLICKBTN_Click(object sender, EventArgs e)
        {
            if (!this.ShowMessage("TY_M_US_A1EBQ734"))
            {
                return;
            }
            else
            {
                UP_BTCLICK_INIT();
            }
        }
        #endregion

        #region Descriptoin : 게이트 오픈 카운트 초기화(USIBNSTF)
        private void UP_BTCLICK_INIT()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_A1EBP732",
                                    this.TXT01_CHBINNO.GetValue().ToString().Trim(),
                                    this.TXT01_CHNUMBER.GetValue().ToString().Trim());
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_US_A1EBR735");

            this.BTN61_INQ_Click(null, null);

            UP_Run();
        }
        #endregion
    }
}

