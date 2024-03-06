using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음 등록 팝업(미승인 등록) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.11.06 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23N3L884 : 계정 과목 코드 상세
    ///  TY_P_AC_29DA5966 : 미승인전표 임시파일 등록(TMAC1102)
    ///  TY_P_AC_2AO4D818 : 미승인 임시화일 라인번호 구하기(미승인 등록)
    ///  TY_P_AC_2B512055 : 받을어음 등록 조회(미승인등록)
    ///  TY_P_AC_2B53S076 : 받을어음 등록 체크(미승인등록)
    ///  TY_P_AC_2B56T098 : 받을어음 조회 단일건(코드헬프)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2B513057 : 받을어음 등록 조회(미승인등록)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  SAV : 저장
    ///  E6CDCL : 거래처코드
    /// </summary>
    public partial class TYAZBJ01C2 : TYBase, IPopupHelper
    {

        private string fsSessionId = string.Empty;
        private string fsDPMK = string.Empty;
        private string fsDTMK = string.Empty;
        private string fsNOSQ = string.Empty;
        private string fsDPAC = string.Empty;
        private string fsIDJP = string.Empty;
        private string fsHISAB = string.Empty;
        private string fsCDCL = string.Empty;
        private string fsNONR = string.Empty;
        private string fsCDAC = string.Empty;

        private string fsA1CDMI1 = string.Empty;
        private string fsA1VLMI1 = string.Empty;
        private string fsA1CDMI2 = string.Empty;
        private string fsA1VLMI2 = string.Empty;
        private string fsA1CDMI3 = string.Empty;
        private string fsA1VLMI3 = string.Empty;
        private string fsA1CDMI4 = string.Empty;
        private string fsA1VLMI4 = string.Empty;
        private string fsA1CDMI5 = string.Empty;
        private string fsA1VLMI5 = string.Empty;
        private string fsA1CDMI6 = string.Empty;
        private string fsA1VLMI6 = string.Empty;
        private string fsB2RKCU = string.Empty;
        private string fsB2CDAC = string.Empty;

        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;



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

        public TYAZBJ01C2()
        {
            this.SetPopupStyle();
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(TYAZBJ01C2_FormClosing);

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

        private void TYAZBJ01C2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClose();
        }

        private void FormClose()
        {
            Control current = this._TComboHelper;
            TYACBJ001I form = null;

            for (; ; )
            {
                if (current.Parent == null)
                    break;


                if (current.Parent is TYACBJ001I)
                {
                    form = current.Parent as TYACBJ001I;
                    break;
                }

                current = current.Parent;
            }

            if (form != null)
                form.PopupClosing(this._TComboHelper as TYCodeBox);
        }

        #region Descriontion : Page_Load
        private void TYAZBJ01C2_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 2)
                {
                    this.fsSessionId = value[0];  //ssid
                    this.fsDPMK = value[1];  // 작성부서
                    this.fsDTMK = value[2];  // 작성일자
                    this.fsNOSQ = value[3];  // 작성순분
                    this.fsDPAC = value[4];  // 귀속부서
                    this.fsIDJP = value[5];  // 전표구분
                    this.fsHISAB = value[6]; // 사번
                    this.fsCDCL = value[7]; // 거래처
                    this.fsCDAC = value[8]; // 계정과목
                    //this.fsNONR = value[8]; // 어음번호
                }
            }

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

            this.CBH01_E6CDCL.SetValue(this.fsCDCL);

            if (this.fsCDAC.Substring(0, 6) == "111005")
            {
                this.CBO01_GBASGRR.Visible = false;
            }

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Descriontion : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            //버튼 잠금
            this.BTN61_SAV.Visible = true; // 라인저장
            this.BTN61_INQ.Visible = true; // 라인조회
            this.FormClose();
            this.Close();
        } 
        #endregion

        #region Descriontion : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.fsCDAC.Substring(0, 6) == "111005")
            {
                this.DbConnector.CommandClear(); // ASGRRMF ( 받을어음 = 10)
                this.DbConnector.Attach("TY_P_AC_2B512055", this.CBH01_E6CDCL.GetValue().ToString());
                this.FPS91_TY_S_AC_2B513057.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else
            {
                if (this.CBO01_GBASGRR.GetValue().ToString().Trim() == "1")
                {
                    this.DbConnector.CommandClear(); // ASGRRMF ( 부도어음(부도) = 14)
                    this.DbConnector.Attach("TY_P_AC_31TB9937", this.CBH01_E6CDCL.GetValue().ToString());
                    this.FPS91_TY_S_AC_2B513057.SetValue(this.DbConnector.ExecuteDataTable());
                }
                else
                {
                    this.DbConnector.CommandClear(); // ASGRRMF ( 부도어음(회수) = 17)
                    this.DbConnector.Attach("TY_P_AC_31TBB938", this.CBH01_E6CDCL.GetValue().ToString());
                    this.FPS91_TY_S_AC_2B513057.SetValue(this.DbConnector.ExecuteDataTable());
                }
            }
        } 
        #endregion

        #region Descriontion : 저장 버튼

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //UP_ScreenFile_Save();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_29DA5966", this.ControlFactory, "02"); // 저장
            //this.DbConnector.ExecuteNonQuery();

            //버튼 잠금
            this.BTN61_SAV.Visible = false; // 라인저장
            this.BTN61_INQ.Visible = false; // 라인조회
        }


        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            //DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //DataSet ds1 = (this.FPS91_TY_S_AC_2B513057.s2

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_2B513057.GetDataSourceInclude(TSpread.TActionType.Select, "E6NONR", "E6CDCL", "VNSANGHO", "E6NMBK", "CDDESC1", "E6AMNR", "E6GUBUN"));

            string sCheckStr = "";
            string s어음번호 = "";
            string s거래처코드 = "";
            string sMessage = "";
            //int iCnt = 0;
            Int32 iB2NOLN = 0;
            //Int32 iDataCnt = 0;            

            int iRowCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            //UP_GetACDMIMF("11100501"); 

            UP_GetACDMIMF(this.fsCDAC);
            

            // 임시화일에 마직막 전표 번호를 가지고 옮
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AO4D818", fsSessionId, fsDPMK, fsDTMK, fsNOSQ); // TMAC1102F
            iB2NOLN = Convert.ToInt32(this.DbConnector.ExecuteScalar().ToString());

            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        s어음번호 = ds.Tables[0].Rows[i]["E6NONR"].ToString().Trim();
                        s거래처코드 = this.CBH01_E6CDCL.GetValue().ToString().Trim();
                        //관리항목 선택
                        UP_CDMIToVLMI(s거래처코드, ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim(), s어음번호);

                        //계정과목
                        if (this.fsCDAC.Substring(0, 6) == "111005")
                        {
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["E6GUBUN"].ToString().Trim()) == "1")
                            {
                                fsB2CDAC = "11100501";
                            }
                            else
                            {
                                fsB2CDAC = "11100502";
                            }
                        }
                        else
                        {
                            fsB2CDAC = this.fsCDAC; // 12400200
                        }

                        //이중등록 체크 
                        this.DbConnector.CommandClear(); // TMAC1102F
                        this.DbConnector.Attach("TY_P_AC_2B53S076", fsSessionId, fsDPMK, fsDTMK, fsNOSQ, fsB2CDAC, fsA1CDMI1, fsA1VLMI1, fsA1CDMI2, fsA1VLMI2);
                        sCheckStr = Convert.ToString(this.DbConnector.ExecuteScalar());

                        if (Convert.ToInt32(sCheckStr) > 0)
                        {
                            sMessage = "어음번호:" + s어음번호 + " " + "  이미 등록된 자료입니다 삭제후 등록하세요!";
                            this.ShowCustomMessage(sMessage, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            SetStartingFocus(this.BTN61_INQ);
                            e.Successed = false;
                            return;
                        }

                        iRowCnt = iRowCnt + 1;
                    }
                }

                if (iB2NOLN + iRowCnt > 98)
                {
                    this.ShowCustomMessage("전표의 라인수가 98라인을 초과합니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    SetStartingFocus(this.BTN61_INQ);
                    e.Successed = false;
                    return;
                }

                DataTable dt = new DataTable();

                string sW2AMDR = string.Empty;
                string sW2AMCR = string.Empty;
                string sW2RKAC = string.Empty;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dt.Clear();

                        sW2AMDR = "";
                        sW2AMCR = "";
                        sW2RKAC = "";

                        s어음번호 = ds.Tables[0].Rows[i]["E6NONR"].ToString().Trim();
                        s거래처코드 = this.CBH01_E6CDCL.GetValue().ToString().Trim();
                        //관리항목 선택
                        UP_CDMIToVLMI(s거래처코드, ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim(), s어음번호);

                        iB2NOLN = iB2NOLN + 1;

                        this.DAT02_W2SSID.SetValue(fsSessionId);
                        this.DAT02_W2DPMK.SetValue(fsDPMK);
                        this.DAT02_W2DTMK.SetValue(fsDTMK);
                        this.DAT02_W2NOSQ.SetValue(fsNOSQ);

                        this.DAT02_W2NOLN.SetValue(Set_Fill2(iB2NOLN.ToString()));
                        this.DAT02_W2IDJP.SetValue(fsIDJP);
                        this.DAT02_W2NOJP.SetValue("");
                        //계정과목
                        if (this.fsCDAC.Substring(0, 6) == "111005")
                        {
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["E6GUBUN"].ToString().Trim()) == "1")
                            {
                                fsB2CDAC = "11100501";
                            }
                            else
                            {
                                fsB2CDAC = "11100502";
                            }

                            sW2AMDR = ds.Tables[0].Rows[i]["E6AMNR"].ToString().Trim();
                            sW2AMCR = "0";
                            sW2RKAC = "어음입금";

                        }
                        else
                        {
                            fsB2CDAC = this.fsCDAC; // 12400200

                            // 부도일땐 차변에 금액 처리
                            if (this.CBO01_GBASGRR.GetValue().ToString().Trim() == "1")
                            {
                                sW2AMDR = ds.Tables[0].Rows[i]["E6AMNR"].ToString().Trim();
                                sW2AMCR = "0";
                                sW2RKAC = "어음만기부도인출";
                            }
                            else // 회수일땐 대변에 금액 처리
                            {
                                sW2AMDR = "0";
                                sW2AMCR = ds.Tables[0].Rows[i]["E6AMNR"].ToString().Trim();
                                sW2RKAC = "부도어음회수";
                            }
                        }

                        this.DAT02_W2CDAC.SetValue(fsB2CDAC);
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(fsDPAC);
                        this.DAT02_W2CDMI1.SetValue(fsA1CDMI1);
                        this.DAT02_W2VLMI1.SetValue(fsA1VLMI1);
                        this.DAT02_W2CDMI2.SetValue(fsA1CDMI2);
                        this.DAT02_W2VLMI2.SetValue(fsA1VLMI2);
                        this.DAT02_W2CDMI3.SetValue(fsA1CDMI3);
                        this.DAT02_W2VLMI3.SetValue(fsA1VLMI3);
                        this.DAT02_W2CDMI4.SetValue(fsA1CDMI4);
                        this.DAT02_W2VLMI4.SetValue(fsA1VLMI4);
                        this.DAT02_W2CDMI5.SetValue(fsA1CDMI5);
                        this.DAT02_W2VLMI5.SetValue(fsA1VLMI5);
                        this.DAT02_W2CDMI6.SetValue(fsA1CDMI6);
                        this.DAT02_W2VLMI6.SetValue(fsA1VLMI6);

                        this.DAT02_W2AMDR.SetValue(sW2AMDR);
                        this.DAT02_W2AMCR.SetValue(sW2AMCR);
                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(fsB2RKCU);
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(fsHISAB);
                        this.DAT02_W2GUBUN.SetValue("");
                        this.DAT02_W2TXAMT.SetValue("0");
                        this.DAT02_W2TXVAT.SetValue("0");
                        this.DAT02_W2HWAJU.SetValue("");

                        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                        this.DAT02_W2DPMK.GetValue().ToString(),
                                        this.DAT02_W2DTMK.GetValue().ToString(),
                                        this.DAT02_W2NOSQ.GetValue().ToString(),
                                        this.DAT02_W2NOLN.GetValue().ToString(),
                                        this.DAT02_W2IDJP.GetValue().ToString(),
                                        this.DAT02_W2NOJP.GetValue().ToString(),
                                        this.DAT02_W2CDAC.GetValue().ToString(),
                                        this.DAT02_W2DTAC.GetValue().ToString(),
                                        this.DAT02_W2DTLI.GetValue().ToString(),
                                        this.DAT02_W2DPAC.GetValue().ToString(),
                                        this.DAT02_W2CDMI1.GetValue().ToString(),
                                        this.DAT02_W2VLMI1.GetValue().ToString(),
                                        this.DAT02_W2CDMI2.GetValue().ToString(),
                                        this.DAT02_W2VLMI2.GetValue().ToString(),
                                        this.DAT02_W2CDMI3.GetValue().ToString(),
                                        this.DAT02_W2VLMI3.GetValue().ToString(),
                                        this.DAT02_W2CDMI4.GetValue().ToString(),
                                        this.DAT02_W2VLMI4.GetValue().ToString(),
                                        this.DAT02_W2CDMI5.GetValue().ToString(),
                                        this.DAT02_W2VLMI5.GetValue().ToString(),
                                        this.DAT02_W2CDMI6.GetValue().ToString(),
                                        this.DAT02_W2VLMI6.GetValue().ToString(),
                                        this.DAT02_W2AMDR.GetValue().ToString(),
                                        this.DAT02_W2AMCR.GetValue().ToString(),
                                        this.DAT02_W2CDFD.GetValue().ToString(),
                                        this.DAT02_W2AMFD.GetValue().ToString(),
                                        this.DAT02_W2RKAC.GetValue().ToString(),
                                        this.DAT02_W2RKCU.GetValue().ToString(),
                                        this.DAT02_W2WCJP.GetValue().ToString(),
                                        this.DAT02_W2PRGB.GetValue().ToString(),
                                        this.DAT02_W2HIGB.GetValue().ToString(),
                                        this.DAT02_W2HISAB.GetValue().ToString(),
                                        this.DAT02_W2GUBUN.GetValue().ToString(),
                                        this.DAT02_W2TXAMT.GetValue().ToString(),
                                        this.DAT02_W2TXVAT.GetValue().ToString(),
                                        this.DAT02_W2HWAJU.GetValue().ToString()});
                    }

                    if (datas.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                        }

                        this.DbConnector.ExecuteNonQueryList();

                        string sCount = Convert.ToString(datas.Count) + " 건의 자료가 등록되었습니다";

                        this.ShowCustomMessage(sCount, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        this.SetFocus(this.BTN61_CLO);
                        e.Successed = true;
                        return;

                    }
                }
            }
            else
            {
                this.ShowCustomMessage("선택된 자료가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetStartingFocus(this.BTN61_INQ);
                e.Successed = false;
                return;
            }
        } 
        #endregion

        #region Description : 관리항목 코드 매칭 (거래처 코드 , 거래처 명 , 어음 번호)
        private void UP_CDMIToVLMI(string sVLMI1, string sVLMINM1, string sVLMI2)
        {

            switch (fsA1CDMI1.Trim())
            {
                //거래처
                case "01":
                    fsA1VLMI1 = sVLMI1.Trim();
                    fsB2RKCU = sVLMINM1.Trim();
                    break;
                //어음번호
                case "29":
                    fsA1VLMI1 = sVLMI2.Trim();
                    break;
                default:
                    fsA1VLMI1 = "";
                    break;
            }
            switch (fsA1CDMI2.Trim())
            {
                //거래처
                case "01":
                    fsA1VLMI2 = sVLMI1.Trim();
                    fsB2RKCU = sVLMINM1.Trim();
                    break;
                //어음번호
                case "29":
                    fsA1VLMI2 = sVLMI2.Trim();
                    break;
                default:
                    fsA1VLMI2 = "";
                    break;
            }
            switch (fsA1CDMI3.Trim())
            {
                //거래처
                case "01":
                    fsA1VLMI3 = sVLMI1.Trim();
                    fsB2RKCU = sVLMINM1.Trim();
                    break;
                //어음번호
                case "29":
                    fsA1VLMI3 = sVLMI2.Trim();
                    break;
                default:
                    fsA1VLMI3 = "";
                    break;
            }
            switch (fsA1CDMI4.Trim())
            {
                //거래처
                case "01":
                    fsA1VLMI4 = sVLMI1.Trim();
                    fsB2RKCU = sVLMINM1.Trim();
                    break;
                //어음번호
                case "29":
                    fsA1VLMI4 = sVLMI2.Trim();
                    break;
                default:
                    fsA1VLMI4 = "";
                    break;
            }
            switch (fsA1CDMI5.Trim())
            {
                //거래처
                case "01":
                    fsA1VLMI5 = sVLMI1.Trim();
                    fsB2RKCU = sVLMINM1.Trim();
                    break;
                //어음번호
                case "29":
                    fsA1VLMI5 = sVLMI2.Trim();
                    break;
                default:
                    fsA1VLMI5 = "";
                    break;
            }
            switch (fsA1CDMI6.Trim())
            {
                //거래처
                case "01":
                    fsA1VLMI6 = sVLMI1.Trim();
                    fsB2RKCU = sVLMINM1.Trim();
                    break;
                //어음번호
                case "29":
                    fsA1VLMI6 = sVLMI2.Trim();
                    break;
                default:
                    fsA1VLMI6 = "";
                    break;
            }

        }
        #endregion

        #region Description : 계정과목코드 => 관리항목명 받아오기 함수
        private void UP_GetACDMIMF(string sB2CDAC)
        {
            //계정과목 조회(상세)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3L884", sB2CDAC);
            DataSet ds = this.DbConnector.ExecuteDataSet();
            if (ds.Tables[0].Rows.Count != 0)
            {
                /*  관리항목코드  */
                fsA1CDMI1 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI1"].ToString());
                fsA1CDMI2 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI2"].ToString());
                fsA1CDMI3 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI3"].ToString());
                fsA1CDMI4 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI4"].ToString());
                fsA1CDMI5 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI5"].ToString());
                fsA1CDMI6 = SetDefaultValue(ds.Tables[0].Rows[0]["A1CDMI6"].ToString());

            }

        }
        #endregion


        ////필수..시작
        #region Description : 필수 선택 조회
        public void ConfirmEventInterface()
        {
            int row = 0;
            string sNum = "";
            string code = "";
            string name = "";

            row = FPS91_TY_S_AC_2B513057.ActiveSheet.ActiveRowIndex;
            //어음번호
            sNum = this.FPS91_TY_S_AC_2B513057.GetValue(row, "E6NONR").ToString();

            code = sNum;
            name = sNum;

            this._SelectedDataRow = this.FPS91_TY_S_AC_2B513057.GetDataRow(row);


            if (this._TComboHelper != null)
            {
                this._TComboHelper.SetValue(code, name);
                this._TComboHelper.BindedDataRow = _SelectedDataRow;
            }

            this.Close();
        }

        public DataTable GetDataSource(params string[] parameters)
        {

            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 2)
                {
                    this.fsSessionId = value[0];  //ssid
                    this.fsDPMK = value[1];  // 작성부서
                    this.fsDTMK = value[2];  // 작성일자
                    this.fsNOSQ = value[3];  // 작성순분
                    this.fsDPAC = value[4];  // 귀속부서
                    this.fsIDJP = value[5]; // 전표구분
                    this.fsHISAB = value[6]; // 사번
                    this.fsCDCL = value[7]; // 거래처
                    this.fsCDAC = value[8]; // 계정코드
                    //this.fsNONR = value[8]; // 어음번호
                }
            }


            this.fsNONR = parameters[1]; // 어음번호
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B56T098", this.fsNONR);
            this.FPS91_TY_S_AC_2B513057.SetValue(this.DbConnector.ExecuteDataTable());

            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.CBH01_E6CDCL.Initialize();

        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYAZBJ01C2_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_AC_2B513057_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion
        //필수...끝
    }
}
