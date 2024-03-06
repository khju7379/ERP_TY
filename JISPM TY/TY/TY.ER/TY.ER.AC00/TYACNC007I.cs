using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
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
    ///  TY_P_AC_23N3L884 : 계정 과목 코드 상세
    ///  TY_P_AC_23N3M888 : 계정 과목 코드 조회
    ///  TY_P_AC_29C7M958 : 자동순번 가져오기
    ///  TY_P_AC_2AO52819 : 미승인 전표 번호 생성(미승인 등록)
    ///  TY_P_AC_31G6H764 : 관리미승인 임시화일 라인번호 구하기(미승인 등록)
    ///  TY_P_AC_31G6K766 : 관리 미승인 임시화일 라인번호 삭제(미승인 등록)
    ///  TY_P_AC_31H2P791 : 관리 미승인 전표 삭제
    ///  TY_P_AC_31H2V792 : 관리 미승인전표 등록 TMAC7001F->AMSLGLF
    ///  TY_P_AC_31HAJ771 : 관리 미승인전표 임시파일 등록(TMAC7001F)
    ///  TY_P_AC_31HAP772 : 관리 미승인전표 임시파일 입력
    ///  TY_P_AC_31HAS773 : 관리 미승인전표 임시화일 삭제(TMAC7001F)  ?????
    ///  TY_P_AC_31HAT774 : 관리 미승인전표 임시화일 수정(TMAC7001F)
    ///  TY_P_AC_31HAV777 : 관리 미승인전표 임시화일 전체삭제(TMAC7001F)
    ///  TY_P_AC_31HBH778 : 관리 미승인전표 임시화일 조회(TMAC7001F)
    ///  TY_P_AC_31HBJ779 : 관리 미승인전표 임시화일 조회(그리드_요약)
    ///  TY_P_AC_31HBK780 : 관리 미승인전표 조회 라인 (미승인등록)AMSLGLF  ??????
    ///  TY_P_AC_31HBL781 : 관리 미승인전표 조회(미승인등록)AMSLGLF
    ///  TY_P_AC_31HBR782 : 관리 미승인전표 존재확인(미승인등록)
    ///  TY_P_AC_31I54824 : 관리 미승인전표 존재확인(미승인등록)
    ///  TY_P_AC_31I57825 : 관리 미승인전표 삭제(미승인등록)
    ///  TY_P_GB_24G9S659 : 사용자 부서 정보
    ///  TY_P_HR_28S9Q562 : 조직도 부서코드 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_31G6G763 : 관리 미승인전표 임시화일 조회(그리드)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_25O8K620 : 전표처리가  완료되었습니다!
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CONFIRM : 확인
    ///  EDIT : 수정
    ///  INP : 입력
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  B2CDAC : 계정코드
    ///  B2DPAC :  귀속부서
    ///  B2DPMK : 작성부서
    ///  B2HISAB : 작성사번
    ///  B2IDJP :  전표구분
    ///  B2NOLN : 순번
    ///  B2DTMK : 작성일자
    ///  B2AMCR : 대변금액
    ///  B2AMCRTOTAL : 대변합계
    ///  B2AMDR : 차변금액
    ///  B2AMDRTOTAL : 차변합계
    ///  B2NOSQ : 일련번호
    ///  B2RKAC :  적요
    ///  B2RKCU :  상대처
    ///  B2WCJP :  원천전표번호
    /// </summary>
    public partial class TYACNC007I : TYBase
    {
        #region 팝업에서 부모창 메소드 호출 
        //private delegate void PopupClosingDelegate(TYCodeBox codeBox);
        //private PopupClosingDelegate _popupClosingDelegate;

        //public void PopupClosing(TYCodeBox codeBox)
        //{
        //    if (codeBox == null)
        //        return;

        //    this._popupClosingDelegate = new PopupClosingDelegate(this.PopupClosingByDelegate);
        //    this.Invoke(this._popupClosingDelegate, codeBox);
        //}
        //// 코드 박스 처리 이후  ---> PopupClosingByDelegate()
        //private void PopupClosingByDelegate(TYCodeBox codeBox)
        //{
        //    //this.ShowCustomMessage(codeBox.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    UP_SetGridMaster();
        //}
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
        private string HiddenOK = string.Empty;

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


        private string _DPMK;
        private string _DTMK;
        private string _NOSQ;

        public TYACNC007I(string LDPMK, string LDTMK, string LNOSQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._DPMK = LDPMK;
            this._DTMK = LDTMK;
            this._NOSQ = LNOSQ;

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

        }

        #region Description : Page Load
        private void TYACNC007I_Load(object sender, System.EventArgs e)
        {

            this.CBH01_B2DPMK.CodeText.Enter += new EventHandler(this.CBH01_B2DPMK_CodeText_Enter); // 코드박스 커스 포커스 정의(Enter)
            this.CBH01_B2HISAB.CodeText.Enter += new EventHandler(this.CBH01_2HISAB_CodeText_Enter); // 코드박스 커스 포커스 정의(Enter)

            // 미승인전표 등록시 처리버튼 체크  이벤트 정의 
            this.BTN61_INP.ProcessCheck += new TButton.CheckHandler(this.BTN61_INP_ProcessCheck);
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(this.BTN61_INQ_ProcessCheck);
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(this.BTN61_EDIT_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(this.BTN61_REM_ProcessCheck);

            this.BTN61_CONFIRM.ProcessCheck += new TButton.CheckHandler(this.BTN61_CONFIRM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_ProcessCheck);
            this.BTN61_CANCEL.ProcessCheck += new TButton.CheckHandler(this.BTN61_CANCEL_ProcessCheck);


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

            this.HiddenOK = "false";

            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString();
            this.CBH01_B2DPAC.DummyValue = this.DTP01_B2DTMK.GetString();

            ImgBtnFocusEvent();

            UP_ComBoLineClear(); //라인번호 클리어

            UP_FieldClear();

            UP_FieldLock(true);

            if (string.IsNullOrEmpty(this._DPMK) && string.IsNullOrEmpty(this._DTMK) && string.IsNullOrEmpty(this._NOSQ))
            {
                UP_Page_SabunInit();
                this.SetFocus(this.CBH01_B2DPMK.CodeText);
            }
            else
            {
                this.CBH01_B2DPMK.SetValue(_DPMK);
                this.DTP01_B2DTMK.SetValue(_DTMK);
                this.TXT01_B2NOSQ.SetValue(_NOSQ);

                this.BTN61_CONFIRM_ProcessCheck(null, null);
                //this.BTN61_CONFIRM_Click(null, null);
            }
        } 
        #endregion
        
  
        #region Description : 계정과목 필드 변경시 이벤트  ----> CBH01_B2CDAC_CodeBoxDataBinded() 
        private void CBH01_B2CDAC_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_B2CDAC.GetValue().ToString().Trim().Length == 8)
            {

                //계정과목 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH01_B2CDAC.GetValue(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    

                }

            }
        }
              #endregion

        #region Description : 선택 라인번호 조회 ---> UP_DisPlayLineNoSq()
        private string UP_DisPlayLineNoSq()
        {

            string sRetrunParam = "";
            //string sNOLN1 = this.CBO01_B2NOLN.SelectedIndex.ToString(); // 2013.01.31

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

            this.DbConnector.CommandClear(); //  TMAC7001F
            this.DbConnector.Attach("TY_P_AC_31HBH778", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim(), sNOLN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_B2CDAC.SetValue(dt.Rows[0]["W2CDAC"].ToString().Trim());
                this.CBH01_B2DPAC.DummyValue = dt.Rows[0]["W2DTMK"].ToString().Trim();
                this.CBH01_B2DPAC.SetValue(dt.Rows[0]["W2DPAC"].ToString().Trim());

                this.TXT01_B2AMDR.SetValue(dt.Rows[0]["W2AMDR"].ToString().Trim());
                this.TXT01_B2AMCR.SetValue(dt.Rows[0]["W2AMCR"].ToString().Trim());
                this.TXT01_B2RKCU.SetValue(dt.Rows[0]["W2RKCU"].ToString().Trim());
                this.TXT01_B2WCJP.SetValue(dt.Rows[0]["W2WCJP"].ToString().Trim());
                this.TXT01_B2RKAC.SetValue(dt.Rows[0]["W2RKAC"].ToString().Trim());

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

            sB2DPMK = this.CBH01_B2DPMK.GetValue().ToString();
            sB2DTMK = this.DTP01_B2DTMK.GetString().ToString();
            sB2NOSQ = this.TXT01_B2NOSQ.GetValue().ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31HBJ779", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ); //TMAC7001F
            this.FPS91_TY_S_AC_31G6G763.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_AC_31G6G763.CurrentRowCount > 0)
            {
                this.FPS91_TY_S_AC_31G6G763.Select();
                this.FPS91_TY_S_AC_31G6G763_Sheet1.ActiveRowIndex = 0;

                // 차대변 합계구하기
                for (int i = 0; i < this.FPS91_TY_S_AC_31G6G763.CurrentRowCount; i++)
                {
                    dDRAmt = dDRAmt + Convert.ToDouble(this.FPS91_TY_S_AC_31G6G763.GetValue(i, "W2AMDR").ToString());
                    dCRAmt = dCRAmt + Convert.ToDouble(this.FPS91_TY_S_AC_31G6G763.GetValue(i, "W2AMCR").ToString());
                }
                this.TXT01_B2AMDRTOTAL.SetValue(dDRAmt);
                this.TXT01_B2AMCRTOTAL.SetValue(dCRAmt);
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31HBJ779", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ); //TMAC7001F
            DataSet ds = this.DbConnector.ExecuteDataSet();

            UP_SetCombo(this.CBO01_B2NOLN, ds);

            UP_FieldClear(); // 필더 Clear 
            this.CBH01_B2CDAC.SetReadOnly(true); //계정과목 잠금

            this.CBO01_B2NOLN.SelectedIndex = 0; // 순번을 "추가" 상태로 만들기 위함
        }
        #endregion

        #region Description : 그리드 선택시 처리 메소드 ----> FPS91_TY_S_AC_31G6G763_CellDoubleClick
        private void FPS91_TY_S_AC_31G6G763_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sBtValue = "";

            //this.CBO01_B2NOLN.SetValue(this.FPS91_TY_S_AC_31G6G763.GetValue("W2NOLN").ToString()); // 2013.01.31
            //this.CBO01_B2NOLN.SelectedIndex = Convert.ToInt16(this.FPS91_TY_S_AC_31G6G763.GetValue("W2NOLN").ToString());

            this.CBO01_B2NOLN.SelectedIndex = this.FPS91_TY_S_AC_31G6G763.ActiveRowIndex + 1;  // 
            if (this.CBO01_B2NOLN.SelectedItem != null) { this.CBO01_B2NOLN.SelectedItem = this.CBO01_B2NOLN.SelectedItem.ToString(); };

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

        // --------------------------------   전표 생성,취소,출력 관련 사항  ------------------------------------ //
        #region Description : 전표발행 처리(전표발생)
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //전표 생성 함수 호출 (TMAC7001F ----> AMSLGLF   미승인 전표 생성시 사용)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31H2V792", fsSessionId ,this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim());
            this.DbConnector.ExecuteNonQuery();

            this.SetFocus(this.DTP01_B2DTMK);  
        } 
        #endregion

        #region Description : 전표발행 전 체크 로직(전표발생)
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 1.기존 미승인전표 존재시 기존 미승인 전표 삭제 후 전표 생성처리 함
            // 2.기존 미승인전표 미존재시 전표 생성 처리 함
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31I54824", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim()); // ADSLGLF
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count > 0)
            {
                //미승인전표 삭제함수 호출
                this.DbConnector.CommandClear(); // AMSLGLF
                this.DbConnector.Attach("TY_P_AC_31I57825", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));
                this.DbConnector.ExecuteNonQuery();
            }
        } 
        #endregion


        #region Description : 전표취소 버튼 처리(전표취소)
        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            //미승인전표 삭제 함수 호출 (실제 삭제)
            this.DbConnector.CommandClear(); // AMSLGLF
            this.DbConnector.Attach("TY_P_AC_31H2P791", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString().Trim()));
            this.DbConnector.ExecuteNonQuery();

            this.SetFocus(this.DTP01_B2DTMK);
        } 
        #endregion

        #region Description : 전표취소 전 체크 로직(전표취소)
        private void BTN61_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 1. 미승인 전표 존재 체크 
            // 2. 미승인 전표 삭제

            bool bMsg = this.ShowCustomMessage("삭제하시겠습니까?", "확인", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

            if (bMsg == false)
            {
                e.Successed = false;
                return;
            }

            //미승인 존재 체크
            this.DbConnector.CommandClear(); // AMSLGLF
            this.DbConnector.Attach("TY_P_AC_31HBR782", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString().Trim(), this.TXT01_B2NOSQ.GetValue().ToString().Trim()); // ADSLGLF
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count == 0)
            {
                this.SetFocus(this.DTP01_B2DTMK);
                this.ShowCustomMessage("전표가 존재 하지 않습니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

            //int iCnt = 0;

            this.HiddenOK = "true";
            this.txtJunPyoGubn = "1";

            sB2DPMK = this.CBH01_B2DPMK.GetValue().ToString();
            sB2DTMK = this.DTP01_B2DTMK.GetString().ToString();
            sB2NOSQ = this.TXT01_B2NOSQ.GetValue().ToString();

            // 미승인전표 임시화일 전체삭제(TMAC7001F)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31HAV777", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
            this.DbConnector.ExecuteNonQuery();

            // 미승인 전표조회  
            this.DbConnector.CommandClear();  // AMSLGLF
            this.DbConnector.Attach("TY_P_AC_31HBL781", sB2DPMK, sB2DTMK, sB2NOSQ);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //미승인전표 -> 임시파일 입력 (미승인 -> 임시파일 등록 AMSLGLF -> TMAC7001F)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_31HAP772", fsSessionId, sB2DPMK, sB2DTMK, sB2NOSQ);
                this.DbConnector.ExecuteNonQuery();

                this.CBH01_B2HISAB.SetValue(dt.Rows[0]["B2HISAB"].ToString());
                this.CBO01_B2IDJP.SetValue(dt.Rows[0]["B2IDJP"].ToString());

                if (dt.Rows[0]["B2NOJP"].ToString() != "")
                {
                    txtJunPyoGubn = "2";
                }

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
                    this.DbConnector.Attach("TY_P_AC_2AO52819", "ML", sB2DPMK, sB2DTMK, ""); // SP 호출 TYSCMLIB.SP_GB_AC_AUTOPFSEQ
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
                        this.FPS91_TY_S_AC_31G6G763.Initialize();

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
                this.DbConnector.CommandClear();  // TMAC7001F
                this.DbConnector.Attach("TY_P_AC_31G6H764", fsSessionId, this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetString(), this.TXT01_B2NOSQ.GetValue());
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

            this.DbConnector.CommandClear();  // 저장 (TMAC7001F)
            this.DbConnector.Attach("TY_P_AC_31HAJ771", this.ControlFactory, "02");
            this.DbConnector.ExecuteNonQuery();

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

            this.DbConnector.CommandClear(); // 수정 (TMAC7001F)
            this.DbConnector.Attach("TY_P_AC_31HAT774", this.ControlFactory, "02"); 
            this.DbConnector.ExecuteNonQuery();


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

            this.DbConnector.CommandClear(); //TMAC7001F
            this.DbConnector.Attach("TY_P_AC_31G6K766", this.DAT02_W2SSID.GetValue().ToString().Trim(), this.DAT02_W2DPMK.GetValue().ToString().Trim(), this.DAT02_W2DTMK.GetValue().ToString().Trim(),
                                                        //this.DAT02_W2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedIndex.ToString()); // 2012.01.31
                                                        this.DAT02_W2NOSQ.GetValue().ToString().Trim(), this.CBO01_B2NOLN.SelectedItem.ToString()); // 
            this.DbConnector.ExecuteNonQuery();


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

         // ------------------------------------------------------------------------------------- //

        #region Description : 입력,수정시 필드 입력값 체크 루틴  ------------------------------> UP_FieldValueCheck()
        private bool UP_FieldValueCheck(string sOption)
        {

            /* 작업 변수 정의 */
            //bool bFlag = false;
            //bool bTrueAndFalse = false;

            //string sMessage;
            string sMyCode = string.Empty, sSubCode = string.Empty;
            string sWK_NONR = string.Empty;
            string sWK_W2CDAC = string.Empty;
            string sWK_HISAB = string.Empty;
            string sReturnValue = string.Empty;


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
            //sB2NOLN = Set_Fill2(this.CBO01_B2NOLN.SelectedIndex.ToString()); // 2013.01.31
            sB2NOLN = Set_Fill2(this.CBO01_B2NOLN.SelectedItem.ToString());

            sB2CDAC = this.CBH01_B2CDAC.GetValue().ToString();  // 계정과목코드
            sB2DPAC = this.CBH01_B2DPAC.GetValue().ToString();  // 귀속부서
            sB2IDJP = this.CBO01_B2IDJP.GetValue().ToString();  // 전표구분

            sB2HISAB = CBH01_B2HISAB.GetValue().ToString();

            sB2AMDR = Get_Numeric(this.TXT01_B2AMDR.GetValue().ToString());
            sB2AMCR = Get_Numeric(this.TXT01_B2AMCR.GetValue().ToString());

 
            //계정과목
            if (SetDefaultValue(this.CBH01_B2CDAC.GetValue().ToString()) == "")
            {
                this.ShowCustomMessage("계정과목을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }

            //귀속부서
            if (SetDefaultValue(this.CBH01_B2DPAC.GetValue().ToString()) == "")
            {
                this.ShowCustomMessage("귀속부서를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2DPAC.CodeText);
                return false;
            }

            /* 1- 계정과목 확인 */
            UP_GetACDMIMF(sB2CDAC);

            if (SetDefaultValue(this.txtfsA1TAG02.Trim()) != "Y")
            {
                this.ShowCustomMessage("실계정 전표가 아닙니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2CDAC.CodeText);
                return false;
            }

            /* 14- 귀속부서 확인   */
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S9Q562","TY", sB2DTMK , this.CBH01_B2DPAC.GetValue().ToString());
            DataTable dt_dpac = this.DbConnector.ExecuteDataTable();
            if (dt_dpac.Rows.Count == 0)
            {
                this.ShowCustomMessage("귀속부서를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBH01_B2DPAC.CodeText);
                return false;
            }
            if (Convert.ToDouble(sB2AMDR) != 0  && Convert.ToDouble(sB2AMCR) != 0)
            {
                this.ShowCustomMessage("차대변 금액이 동시에 올 수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_B2AMDR);
                return false;
            }

            /* 32- 적요확인      */
            if (this.TXT01_B2RKAC.GetValue().ToString().Trim() == "")
            {
                this.ShowCustomMessage("적요를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_B2RKAC);
                return false;
            }

            return true;
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
        } 
        #endregion

        #region Description : 미승인등록 필드 CLEAR  --> UP_FieldClear()
        private void UP_FieldClear()
        {
            this.CBH01_B2CDAC.SetValue("");
            this.CBH01_B2DPAC.SetValue("");

            this.TXT01_B2AMDR.SetValue("");
            this.TXT01_B2AMCR.SetValue("");
            this.TXT01_B2RKCU.SetValue("");
            this.TXT01_B2RKAC.SetValue("");
            this.TXT01_B2WCJP.SetValue("");
        } 
        #endregion

        #region Description : 미승인등록 화면값 --> 저장변수 담기  ---> UP_ScreenFile_Save()
        private void UP_ScreenFile_Save()
        {
            this.DAT02_W2SSID.SetValue(fsSessionId);
            this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue().ToString());
            this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString());
            this.DAT02_W2NOSQ.SetValue(this.TXT01_B2NOSQ.GetValue().ToString());
            //this.DAT02_W2NOLN.SetValue(this.CBO01_B2NOLN.SelectedIndex.ToString()); // 2013.01.31
            this.DAT02_W2NOLN.SetValue(this.CBO01_B2NOLN.SelectedItem.ToString());

            this.DAT02_W2IDJP.SetValue(this.CBO01_B2IDJP.GetValue().ToString());
            this.DAT02_W2NOJP.SetValue("");
            this.DAT02_W2CDAC.SetValue(this.CBH01_B2CDAC.GetValue().ToString());
            this.DAT02_W2DTAC.SetValue("");
            this.DAT02_W2DTLI.SetValue("");
            this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPAC.GetValue().ToString());

            this.DAT02_W2CDMI1.SetValue("");
            this.DAT02_W2VLMI1.SetValue("");
            this.DAT02_W2CDMI2.SetValue("");
            this.DAT02_W2VLMI2.SetValue("");
            this.DAT02_W2CDMI3.SetValue("");
            this.DAT02_W2VLMI3.SetValue("");
            this.DAT02_W2CDMI4.SetValue("");
            this.DAT02_W2VLMI4.SetValue("");
            this.DAT02_W2CDMI5.SetValue("");
            this.DAT02_W2VLMI5.SetValue("");
            this.DAT02_W2CDMI6.SetValue("");
            this.DAT02_W2VLMI6.SetValue("");

            this.DAT02_W2AMDR.SetValue(this.TXT01_B2AMDR.GetValue().ToString());
            this.DAT02_W2AMCR.SetValue(this.TXT01_B2AMCR.GetValue().ToString());
            this.DAT02_W2CDFD.SetValue("");
            this.DAT02_W2AMFD.SetValue("0");
            this.DAT02_W2RKAC.SetValue(this.TXT01_B2RKAC.GetValue().ToString());
            this.DAT02_W2RKCU.SetValue(this.TXT01_B2RKCU.GetValue().ToString());
            this.DAT02_W2WCJP.SetValue("");
            this.DAT02_W2PRGB.SetValue("");
            this.DAT02_W2HIGB.SetValue("A");
            this.DAT02_W2HISAB.SetValue(CBH01_B2HISAB.GetValue().ToString());
            this.DAT02_W2GUBUN.SetValue("");
            this.DAT02_W2TXAMT.SetValue("0");
            this.DAT02_W2TXVAT.SetValue("0");
            this.DAT02_W2HWAJU.SetValue("");
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
            //임경화
            this.CBO01_B2NOLN.SetReadOnly(false);

            //HiddField.Value = sHiddValue;
            //ImgClear.Visible = bClear;    // 라인 입력
            //ImgSave.Visible = bSave;      // 라인수정
            //ImgDelete.Visible = bDelete;   // 라인삭제
            //ImgSlipLeave.Visible = bVisble;  // 전표발행

            this.BTN61_INP.Visible = bClear; // 라인 입력
            this.BTN61_EDIT.Visible = bSave;// 라인수정
            this.BTN61_REM.Visible = bDelete; // 라인삭제
            this.BTN61_SAV.Visible = bVisble; // 전표발행

            this.BTN61_CANCEL.Visible = bVisble; // 전표삭제

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
                    this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_B2DTMK.GetString(), this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
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
                return;
            }
        }

        //  Description : 텍스트박스 Enter (순번)
        private void TXT01_B2NOSQ_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                return;
            }
        }

        //  Description : 콤보박스 Enter (전표구분)
        private void CBO01_B2IDJP_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                return;
            }
        }

        // Description : 사번 처리
        private void CBH01_B2HISAB_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                return;
            }
        }
        private void CBH01_2HISAB_CodeText_Enter(object sender, EventArgs e)
        {
            if (this.HiddenOK == "true")
            {
                UP_Screen_Initialize();
                return;
            }
        } 
        #endregion


        #region Description : 화면 초기화 & 임시화일 삭제(TMAC7001F) ---> UP_Screen_Initialize()
        private void UP_Screen_Initialize()
        {
            if (this.CBH01_B2DPMK.GetValue().ToString().Trim() != "" && this.DTP01_B2DTMK.GetString().ToString() != "" && this.TXT01_B2NOSQ.GetValue().ToString() != "")
            {
                // 임시화일전체삭제 (TMAC7001F - DELETE )
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_31HAV777", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString()); // TMAC1102F
                this.DbConnector.ExecuteNonQuery();
            }

            this.TXT01_B2AMDRTOTAL.SetValue("");
            this.TXT01_B2AMCRTOTAL.SetValue("");

            ImgBtnFocusEvent();
            UP_ComBoLineClear(); //라인번호 클리어
            UP_FieldClear();
            UP_FieldLock(true);

            this.FPS91_TY_S_AC_31G6G763.Initialize();

            //BATID번호 부여(SSID) 키부분에 도달 할때 새로운 SSID를 부여 받음
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            this.fsSessionId = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            this.SetFocus(this.DTP01_B2DTMK);

            this.HiddenOK = "false";
        }
        #endregion

        #region Description : FormClosing  --->  TYACBJ001I_FormClosing() (미승인전표 닫기 (X))
        private void TYACNC007I_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CBH01_B2DPMK.GetValue().ToString().Trim() != "" && this.DTP01_B2DTMK.GetString().ToString() != "" && this.TXT01_B2NOSQ.GetValue().ToString() != "")
            {
                // 임시화일전체삭제 (TMAC7001F - DELETE )
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_31HAV777", fsSessionId, this.CBH01_B2DPMK.GetValue().ToString(), this.DTP01_B2DTMK.GetString().ToString(), this.TXT01_B2NOSQ.GetValue().ToString()); // TMAC1102F
                this.DbConnector.ExecuteNonQuery();
            }
        } 
        #endregion

        //--------------------------------------------------------------------------

        #region Description : Page_Load 시 사번,귀속부서 세팅
        private void UP_Page_SabunInit()
        {
            //사번 조회
            this.CBH01_B2HISAB.SetValue(Employer.EmpNo.ToString().Trim());
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_B2DTMK.GetString(), this.CBH01_B2HISAB.GetValue().ToString().Trim());  //INKIBNMF
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

        #region Description : 내부매출 생성 
        private void BTN61_MAECHUL_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_33544239", this.CBH01_B2DPMK.GetValue().ToString().Trim(), this.DTP01_B2DTMK.GetString().ToString(), this.CBH01_B2HISAB.GetValue().ToString().Trim(), "1", ""); // SP CALL
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.ShowMessage("TY_M_AC_25O8K620");
            }
        } 
        #endregion

        #region  Description : DTP01_B2DTMK_ValueChanged 이벤트
        private void DTP01_B2DTMK_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString();
        }
        #endregion

    }                 
}                     
