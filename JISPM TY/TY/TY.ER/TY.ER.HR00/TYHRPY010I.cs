using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.AC00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여전표관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.19 14:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53IHK706 : 급여전표조회(팝업)
    ///  TY_P_HR_53JEV736 : 급여전표내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53JGS747 : 급여전표내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_2CDB0166 : 취소 하시겠습니까?
    ///  TY_M_AC_2CDB1167 : 취소 되었습니다!
    ///  TY_M_AC_2CDB1168 : 취소 작업에 실패했습니다!
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  JPNO_CRE : 전표생성
    ///  JUNPYO_CANCEL : 전표취소
    ///  REM : 삭제
    ///  SAV : 저장
    ///  SEL : 선택
    ///  APTCDAC : 계정과목
    ///  APTCDSB : 사번
    ///  APTDPAC : 귀속부서
    ///  PAYGUBN : 급여구분
    ///  APMGUBN : 발행구분
    ///  APMYUNCHA : 지급구분
    ///  APTDCGB : 차/대변 구분
    ///  APMDATE : 발행일자
    ///  APMJPNO : 전표번호
    ///  PAYJIDATE : 지급일자
    ///  PAYYYMM : 급여년월
    /// </summary>
    public partial class TYHRPY010I : TYBase
    {   
        string fsAPTYYMM = string.Empty;
        string fsAPTDPMK = string.Empty;
        string fsAPTPYCODE = string.Empty;
        string fsAPTGUBN = string.Empty;
        string fsAPTJIDATE = string.Empty;
        string fsAPTBALDATE = string.Empty;
        int fiSEQ = 0;

        #region Description : 폼 로드
        public TYHRPY010I(string sAPTYYMM, string sAPTDPMK, string sAPTPYCODE, string sAPTGUBN, string sAPTJIDATE, string sAPTBALDATE)
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_53JGS747, "APTCDAC", "A1NMAC", "APTCDAC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_53JGS747, "APTCDSB", "APTCDSBNM", "APTCDSB");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_53JGS747, "APTDPAC", "APTDPACNM", "APTDPAC");
            this.GetSpreadCodeHelper(this.FPS91_TY_S_HR_53JGS747, "APTDPAC").DummyValue = DateTime.Now.ToString("yyyyMMdd"); // 부서 기준일자 처리

            fsAPTYYMM = sAPTYYMM;
            fsAPTDPMK = sAPTDPMK;
            fsAPTPYCODE = sAPTPYCODE;
            fsAPTGUBN = sAPTGUBN;
            fsAPTJIDATE = sAPTJIDATE;
            fsAPTBALDATE = sAPTBALDATE;
        }

        private void TYHRPY010I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_CRE_ProcessCheck);
            this.BTN61_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_JUNPYO_CANCEL_ProcessCheck);


            this.DTP01_APMDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            CBH01_PAYGUBN.SetReadOnly(true);
            DTP01_PAYYYMM.SetReadOnly(true);
            DTP01_PAYJIDATE.SetReadOnly(true);
            TXT01_APMJPNO.SetReadOnly(true);

            FPS91_TY_S_HR_53JGS747.Initialize();

            if (fsAPTYYMM == "")
            {
                BTN61_REM.Visible = false;
                BTN61_JPNO_CRE.Visible = false;
                BTN61_JUNPYO_CANCEL.Visible = false;
            }
            else
            {   
                CBO01_APMGUBN.SetReadOnly(true);
                DTP01_APMDATE.SetReadOnly(true);
                BTN61_SEL.Visible = false;
                UP_Select();
            }

            SetStartingFocus(CBH01_PAYGUBN);
            
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 전표생성 버튼 이벤트
        private void BTN61_JPNO_CRE_Click(object sender, EventArgs e)
        {
            string sProcedureid = string.Empty;

            try
            {
                sProcedureid = this.CBO01_APMGUBN.GetValue().ToString() == "1" ? "TY_P_HR_53BFM648" : "TY_P_HR_5AGBH992";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProcedureid,
                                        this.IPAdresss,
                                        this.ProgramNo,
                                        DTP01_PAYYYMM.GetString().Substring(0, 6),
                                        CBH01_PAYGUBN.GetValue().ToString(),
                                        DTP01_PAYJIDATE.GetString(),
                                        DTP01_APMDATE.GetString(),
                                        CBO01_APMGUBN.GetValue().ToString(),
                                        TYUserInfo.EmpNo,
                                        "A",
                                        "");
                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) == "ER")
                {   
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    this.ShowMessage("TY_M_AC_25O8K620");

                    BTN61_JUNPYO_CANCEL.Visible = true;
                    BTN61_JPNO_CRE.Visible = false;

                    UP_Select();
                }
            }
            catch( Exception ex )
            {
                this.ShowCustomMessage(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Description : 전표생성 체크
        private void BTN61_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 전표취소 버튼 이벤트
        private void BTN61_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_53BFM648",
                                        this.IPAdresss,
                                        this.ProgramNo,
                                        DTP01_PAYYYMM.GetString().Substring(0, 6),
                                        CBH01_PAYGUBN.GetValue().ToString(),
                                        DTP01_PAYJIDATE.GetString(),
                                        DTP01_APMDATE.GetString(),
                                        CBO01_APMGUBN.GetValue().ToString(),
                                        TYUserInfo.EmpNo,
                                        "D",
                                        "");
                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    this.ShowMessage("TY_M_MR_3194D581");

                    BTN61_JUNPYO_CANCEL.Visible = false;
                    BTN61_JPNO_CRE.Visible = true;

                    UP_Select();
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 전표취소 체크
        private void BTN61_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (this.TXT01_APMJPNO.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("전표번호를 확인해주세요.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            string sJunPyoNum = this.TXT01_APMJPNO.GetValue().ToString().Replace("-","");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B7BT153", sJunPyoNum.Substring(0, 6), sJunPyoNum.Substring(6, 8), sJunPyoNum.Substring(14, 3)); // ADSLGLF
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count == 0)
            {
                this.ShowCustomMessage("미승인전표가 존재 하지 않습니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            else
            {
                if (dt_adsl.Rows[0]["B2NOJP"].ToString().Trim() != "")
                {
                    this.ShowCustomMessage("승인된 전표이므로 삭제 할수 없음!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                };
            }

            if (!this.ShowMessage("TY_M_MR_3194D580"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53NHS840", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            UP_Select();
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_53JGS747.GetDataSourceInclude(TSpread.TActionType.Remove, "APTYYMM", "APTDPMK", "APTPYCODE", "APTGUBN", "APTJIDATE", "APTBALDATE", "APTSEQ");

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sAPMCDDP = string.Empty;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_53KDX759", CBH01_APTCDSB.GetValue().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sAPMCDDP = dt.Rows[0]["KBBUSEO"].ToString();
                }

                this.DbConnector.CommandClear();
                //마스타 등록
                if (fsAPTYYMM == "")
                {
                    this.DbConnector.Attach("TY_P_HR_53KDM758",
                                            DTP01_PAYYYMM.GetString().Substring(0, 6),
                                            "1",
                                            CBH01_PAYGUBN.GetValue().ToString(),
                                            DTP01_PAYJIDATE.GetString(),
                                            DTP01_APMDATE.GetString(),
                                            CBO01_APMGUBN.GetValue().ToString(),
                                            CBO01_APMYUNCHA.GetValue().ToString(),
                                            CBH01_APTCDSB.GetValue().ToString(),
                                            sAPMCDDP,
                                            TXT01_APMJPNO.GetValue().ToString(),
                                            TXT01_APEMPPAYTOTAL.GetValue().ToString(),
                                            TXT01_APINDPAYTOTAL.GetValue().ToString(),
                                            TYUserInfo.EmpNo);

                    this.DbConnector.ExecuteTranQueryList();

                    fsAPTYYMM = DTP01_PAYYYMM.GetString().Substring(0, 6);
                    fsAPTDPMK = "1";
                    fsAPTPYCODE = CBH01_PAYGUBN.GetValue().ToString();
                    fsAPTGUBN = CBO01_APMGUBN.GetValue().ToString();
                    fsAPTJIDATE = DTP01_PAYJIDATE.GetString();
                    fsAPTBALDATE = DTP01_APMDATE.GetString();

                    BTN61_SEL.Visible = false;
                    BTN61_REM.Visible = true;
                    BTN61_JPNO_CRE.Visible = true;
                }
                //마스타 수정
                else
                {
                    this.DbConnector.Attach("TY_P_HR_53KE6760",
                                            CBO01_APMYUNCHA.GetValue().ToString(),
                                            CBH01_APTCDSB.GetValue().ToString(),
                                            sAPMCDDP,
                                            TXT01_APMJPNO.GetValue().ToString(),
                                            TXT01_APEMPPAYTOTAL.GetValue().ToString(),
                                            TXT01_APINDPAYTOTAL.GetValue().ToString(),
                                            TYUserInfo.EmpNo,
                                            DTP01_PAYYYMM.GetString().Substring(0, 6),
                                            "1",
                                            CBH01_PAYGUBN.GetValue().ToString(),
                                            DTP01_PAYJIDATE.GetString(),
                                            DTP01_APMDATE.GetString(),
                                            CBO01_APMGUBN.GetValue().ToString());

                    this.DbConnector.ExecuteTranQueryList();
                }

                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                //내역 등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53NBI779", ds.Tables[0].Rows[i]["APTYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTDPMK"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTPYCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTJIDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTBALDATE"].ToString(),
                                                                Set_Fill3(UP_getSEQ().ToString()),
                                                                ds.Tables[0].Rows[i]["APTCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTCDSB"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTWNCH"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTRKAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTJPNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["APTDCGB"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQueryList();
                }


                //내역 수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53NBK781", ds.Tables[1].Rows[i]["APTCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTDPAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTCDSB"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTWNCH"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTRKAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTJPNO"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTDCGB"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["APTYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTDPMK"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTPYCODE"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTJIDATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTBALDATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["APTSEQ"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQueryList();
                }
                

                UP_Select();

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_53JGS747.GetDataSourceInclude(TSpread.TActionType.New, "APTYYMM", "APTDPMK", "APTPYCODE", "APTGUBN", "APTJIDATE", "APTBALDATE", "APTCDAC",
                                                                                                    "APTDPAC", "APTCDSB", "APTWNCH", "APTAMT", "APTRKAC", "APTJPNO", "APTDCGB"));
            ds.Tables.Add(this.FPS91_TY_S_HR_53JGS747.GetDataSourceInclude(TSpread.TActionType.Update, "APTYYMM", "APTDPMK", "APTPYCODE", "APTGUBN", "APTJIDATE", "APTBALDATE", "APTSEQ", "APTCDAC",
                                                                                                       "APTDPAC", "APTCDSB", "APTWNCH", "APTAMT", "APTRKAC", "APTJPNO", "APTDCGB"));
            //4대보험일 경우
            if (this.CBO01_APMGUBN.GetValue().ToString() != "1")
            {
                if (Convert.ToDouble(Get_Numeric(this.TXT01_APEMPPAYTOTAL.GetValue().ToString())) <= 0)
                {
                    this.TXT01_APEMPPAYTOTAL.Focus();
                    this.ShowCustomMessage("고용보험납부액을 입력하세요", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;

                }
                if (Convert.ToDouble(Get_Numeric(this.TXT01_APINDPAYTOTAL.GetValue().ToString())) <= 0)
                {
                    this.TXT01_APINDPAYTOTAL.Focus();
                    this.ShowCustomMessage("산재보험납부액을 입력하세요", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                //고용보험,산재보험 요율이 등록되어 있는지 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5AJJJ008", "01", "1" );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("고용보험 요율관리 자료가 등록되지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[0]["EIRSDATE"].ToString()) > Convert.ToInt32(this.DTP01_PAYJIDATE.GetString().ToString()))
                    {
                        this.ShowCustomMessage("고용보험 요율관리 자료가 등록되지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5AJJJ008", "02", "1");
                DataTable dk = this.DbConnector.ExecuteDataTable();
                if (dk.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("산재보험 요율관리 자료가 등록되지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (Convert.ToInt32(dk.Rows[0]["EIRSDATE"].ToString()) > Convert.ToInt32(this.DTP01_PAYJIDATE.GetString().ToString()))
                    {
                        this.ShowCustomMessage("산재보험 요율관리 자료가 등록되지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                if (fsAPTYYMM == "")
                {
                    //급여전표 발행 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_B42EQ108", DTP01_PAYYYMM.GetString().Substring(0, 6), "1", CBH01_PAYGUBN.GetValue(), DTP01_PAYJIDATE.GetString(), CBO01_APMGUBN.GetValue());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("급여전표 자료가 존재합니다. 등록 할 수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

                this.TXT01_APEMPPAYTOTAL.SetValue(0);
                this.TXT01_APINDPAYTOTAL.SetValue(0);
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            TYHRPY006P popup = new TYHRPY006P();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_PAYYYMM.SetValue(popup.fsPAYYYMM);
                this.CBH01_PAYGUBN.SetValue(popup.fsPAYGUBN);
                this.DTP01_PAYJIDATE.SetValue(popup.fsPAYJIDATE);
                this.DTP01_APMDATE.SetValue(popup.fsPAYJIDATE);
            }          

            this.CBO01_APMGUBN.Focus();
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Select()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53IHK706",
                                    fsAPTYYMM,
                                    fsAPTDPMK,
                                    fsAPTPYCODE,
                                    fsAPTGUBN,
                                    fsAPTJIDATE,
                                    fsAPTBALDATE);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            CBH01_PAYGUBN.SetValue(dt.Rows[0]["APMPYCODE"].ToString());
            DTP01_PAYYYMM.SetValue(dt.Rows[0]["APMYYMM"].ToString());
            DTP01_PAYJIDATE.SetValue(dt.Rows[0]["APMJIDATE"].ToString());
            CBO01_APMGUBN.SetValue(dt.Rows[0]["APMGUBN"].ToString());
            CBO01_APMYUNCHA.SetValue(dt.Rows[0]["APMYUNCHA"].ToString());
            CBH01_APTCDSB.SetValue(dt.Rows[0]["APMCDSB"].ToString());
            DTP01_APMDATE.SetValue(dt.Rows[0]["APMDATE"].ToString());
            TXT01_APMJPNO.SetValue(dt.Rows[0]["APMJPNO"].ToString());
            TXT01_APEMPPAYTOTAL.SetValue(dt.Rows[0]["APEMPPAYTOTAL"].ToString());
            TXT01_APINDPAYTOTAL.SetValue(dt.Rows[0]["APINDPAYTOTAL"].ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53JEV736",
                                    fsAPTYYMM,
                                    fsAPTDPMK,
                                    fsAPTPYCODE,
                                    fsAPTGUBN,
                                    fsAPTJIDATE,
                                    fsAPTBALDATE);

            dt = this.DbConnector.ExecuteDataTable();

            FPS91_TY_S_HR_53JGS747.Initialize();

            this.FPS91_TY_S_HR_53JGS747.SetValue(dt);

            if (TXT01_APMJPNO.GetValue().ToString() == "")
            {
                BTN61_JUNPYO_CANCEL.Visible = false;
            }
            else
            {
                BTN61_SAV.Visible = false;
                BTN61_REM.Visible = false;
                BTN61_JPNO_CRE.Visible = false;
            }
        }
        #endregion

        #region Description : Row 추가 이벤트(순번생성)
        private void FPS91_TY_S_HR_53JGS747_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            if (fiSEQ == 0)
            {
                fiSEQ = UP_getSEQ();

                this.FPS91_TY_S_HR_53JGS747.SetValue("APTSEQ", Set_Fill3(fiSEQ.ToString()));
            }
            else
            {
                fiSEQ++;

                this.FPS91_TY_S_HR_53JGS747.SetValue("APTSEQ", Set_Fill3(fiSEQ.ToString()));
            }
            this.FPS91_TY_S_HR_53JGS747.SetValue("APTYYMM", DTP01_PAYYYMM.GetString().Substring(0,6));
            this.FPS91_TY_S_HR_53JGS747.SetValue("APTDPMK", "1");
            this.FPS91_TY_S_HR_53JGS747.SetValue("APTPYCODE", CBH01_PAYGUBN.GetValue().ToString());
            this.FPS91_TY_S_HR_53JGS747.SetValue("APTGUBN", CBO01_APMGUBN.GetValue().ToString());
            this.FPS91_TY_S_HR_53JGS747.SetValue("APTJIDATE", DTP01_PAYJIDATE.GetString());
            this.FPS91_TY_S_HR_53JGS747.SetValue("APTBALDATE", DTP01_APMDATE.GetString());
        }
        #endregion

        #region Description : 원천번호 코드박스
        private void FPS91_TY_S_HR_53JGS747_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1 && FPS91_TY_S_HR_53JGS747.ActiveSheet.ActiveColumnIndex == 12)
            {
                TYHRPY10C1 popup = new TYHRPY10C1(FPS91_TY_S_HR_53JGS747.GetValue(FPS91_TY_S_HR_53JGS747.ActiveSheet.ActiveRowIndex, "APTCDAC").ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FPS91_TY_S_HR_53JGS747.SetValue(FPS91_TY_S_HR_53JGS747.ActiveSheet.ActiveRowIndex, "APTWNCH", popup.fsSJJPNO);
                    FPS91_TY_S_HR_53JGS747.SetValue(FPS91_TY_S_HR_53JGS747.ActiveSheet.ActiveRowIndex, "APTRKAC", popup.fsB2RKAC);
                    FPS91_TY_S_HR_53JGS747.SetValue(FPS91_TY_S_HR_53JGS747.ActiveSheet.ActiveRowIndex, "APTAMT", popup.fsB7AMJN);
                    FPS91_TY_S_HR_53JGS747.SetValue(FPS91_TY_S_HR_53JGS747.ActiveSheet.ActiveRowIndex, "APTDPAC", popup.fsB2DPAC);
                }
            }
        }
        #endregion

        #region Description : 급여전표내역 순번 가져오기
        private int UP_getSEQ()
        {
            int rtnValue;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53NBM783", fsAPTYYMM,
                                                        fsAPTDPMK,
                                                        fsAPTPYCODE,
                                                        fsAPTGUBN,
                                                        fsAPTJIDATE,
                                                        fsAPTBALDATE);

            rtnValue = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            return rtnValue;
        }
        #endregion
    }
}
