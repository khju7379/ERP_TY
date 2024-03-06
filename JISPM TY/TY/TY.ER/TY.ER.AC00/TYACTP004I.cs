using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 소득자료 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.10.02 15:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_4A1HU116 : 소득자료 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  VNSAUPNO : 사업자등록번호
    ///  WADDRESS : 주소
    ///  WBRANCH : 지점 구분
    ///  WBUSIGBN : 사업소득업종구분
    ///  WCOSTAMT : 필요경비
    ///  WDAYWORK : 근무일수
    ///  WECTGUBN : 기타소득구분
    ///  WGBINCOM : 소득자구분
    ///  WGIDATE : 지급년월일
    ///  WHOLDYUL : 원천징수세율
    ///  WINCOMAMT : 소득금액
    ///  WINCOME : 소득자 구분
    ///  WLOCALTAX : 지방소득세
    ///  WLSUMTAX : 합계
    ///  WNATIVEGB : 내외국인구분
    ///  WPAYGUBN : 소득종류
    ///  WRDEPART : 귀속부서
    ///  WREMARKS : 적요
    ///  WREYYMM : 귀속년월
    ///  WRJUMIN : 주민번호
    ///  WTAXINCOM : 소득세
    ///  WTOTALAMT : 지급총액
    ///  WTRADNAME : 이름(상호)
    ///  WTRADTEL : 전화번호
    /// </summary>
    public partial class TYACTP004I : TYBase
    {
        string fsWBRANCH  = string.Empty;
        string fsWREYYMM  = string.Empty;
        string fsWGIDATE  = string.Empty;
        string fsWINCOME  = string.Empty;
        string fsWRDEPART = string.Empty;
        string fsWRJUMIN  = string.Empty;
        string fsWRGUNMU  = string.Empty;

        #region Description : 페이지 로드
        public TYACTP004I(string WBRANCH, string WREYYMM, string WGIDATE, string WINCOME, string WRDEPART, string WRJUMIN, string WRGUNMU)
        {
            InitializeComponent();
            
            fsWBRANCH  = WBRANCH;
            fsWREYYMM  = WREYYMM;
            fsWGIDATE  = WGIDATE;
            fsWINCOME  = WINCOME;
            fsWRDEPART = WRDEPART;
            fsWRJUMIN  = WRJUMIN;
            fsWRGUNMU  = WRGUNMU;
        }
        
        private void TYACTP004I_Load(object sender, System.EventArgs e)
        {   
            this.CBH01_WRDEPART.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            if (fsWBRANCH != "" && fsWREYYMM != "" && fsWGIDATE != "" && fsWINCOME != "" && fsWRDEPART != "" && fsWRGUNMU != "")
            {
                UP_SELECT(fsWBRANCH, fsWREYYMM, fsWGIDATE, fsWINCOME, fsWRDEPART, fsWRJUMIN, fsWRGUNMU);

                this.CBO01_WRGUNMU.SetReadOnly(true);
                this.CBH01_WINCOME.SetReadOnly(true);
                this.DTP01_WREYYMM.SetReadOnly(true);
                this.DTP01_WGIDATE.SetReadOnly(true);
                this.TXT01_VNSAUPNO.SetReadOnly(true);
                this.TXT01_WRJUMIN.SetReadOnly(true);
                this.CBH01_WRDEPART.SetReadOnly(true);
            }
            else
            {
                fsWBRANCH = "1";

                this.CBO01_WRGUNMU.SetReadOnly(false);
                this.CBH01_WINCOME.SetReadOnly(false);
                this.DTP01_WREYYMM.SetReadOnly(false);
                this.DTP01_WGIDATE.SetReadOnly(false);
                this.TXT01_VNSAUPNO.SetReadOnly(false);
                this.TXT01_WRJUMIN.SetReadOnly(false);
                this.CBH01_WRDEPART.SetReadOnly(false);
                this.BTN61_REM.Visible = false;

                this.CBO01_WRGUNMU.Focus();
            }
            this.TXT01_WINCOMAMT.SetReadOnly(true);
            this.TXT01_WLSUMTAX.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (UP_FieldCheck() == true)
            {
                string sWRJUMIN = string.Empty;

                if (this.CBH01_WINCOME.GetValue().ToString() == "A50" || this.CBH01_WINCOME.GetValue().ToString() == "A60")
                {
                    sWRJUMIN = this.TXT01_VNSAUPNO.GetValue().ToString();
                }
                else
                {
                    sWRJUMIN = this.TXT01_WRJUMIN.GetValue().ToString();
                }

                if (string.IsNullOrEmpty(this.fsWRGUNMU)) //신규
                {
                    if (UP_KeyCehck(sWRJUMIN))
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_4A1HU116", fsWBRANCH.ToString(),                       // 지점구분
                                                                    this.DTP01_WREYYMM.GetValue().ToString(),   // 귀속년원
                                                                    this.DTP01_WGIDATE.GetValue().ToString(),   // 지급년월일
                                                                    this.CBH01_WINCOME.GetValue().ToString(),   // 소득구분
                                                                    this.CBH01_WRDEPART.GetValue().ToString(),  // 귀속부서
                                                                    sWRJUMIN,                                   // 주민번호(사업자등록번호)
                                                                    TYUserInfo.SecureKey,
                                                                    this.CBO01_WRGUNMU.GetValue().ToString(),   // 지역구분
                                                                    this.CBH01_WGBINCOM.GetValue().ToString(),  // 소득자구분
                                                                    this.CBH01_WPAYGUBN.GetValue().ToString(),  // 소득종류
                                                                    this.TXT01_WDAYWORK.GetValue().ToString(),  // 근무일수
                                                                    this.TXT01_WREMARKS.GetValue().ToString(),  // 적요
                                                                    this.TXT01_WTRADNAME.GetValue().ToString(), // 이름(상호)
                                                                    this.TXT01_WTRADTEL.GetValue().ToString(),  // 전화번호
                                                                    this.CBO01_WNATIVEGB.GetValue().ToString(), // 내외국인구분
                                                                    this.CBH01_WCOUNTRY.GetValue().ToString(),  // 거주지국
                                                                    this.TXT01_WTOTALAMT.GetValue().ToString(), // 지급총액
                                                                    this.TXT01_WHOLDYUL.GetValue().ToString(),  // 원천징수세율
                                                                    this.TXT01_WCOSTAMT.GetValue().ToString(),  // 필요경비
                                                                    this.TXT01_WINCOMAMT.GetValue().ToString(), // 소득금액
                                                                    this.TXT01_WTAXINCOM.GetValue().ToString(), // 소득세
                                                                    this.TXT01_WLOCALTAX.GetValue().ToString(), // 지방소득세
                                                                    this.TXT01_WLSUMTAX.GetValue().ToString(),  // 합계
                                                                    this.TXT01_WADDRESS.GetValue().ToString(),  // 주소
                                                                    this.TXT01_WBUSIGBN.GetValue().ToString(),  // 사업소득업종구분
                                                                    this.CBH01_WECTGUBN.GetValue().ToString()   // 기타소득구분
                                                                    ); //저장

                        this.DbConnector.ExecuteTranQuery();

                        this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

                        UP_FieldClear();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_3219C986"); // 오류 메세지
                    }
                }
                else  //수정
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_4A6EE147",
                                                                this.CBH01_WGBINCOM.GetValue().ToString(),   // 소득자구분
                                                                this.CBH01_WPAYGUBN.GetValue().ToString(),   // 소득종류
                                                                this.TXT01_WDAYWORK.GetValue().ToString(),   // 근무일수
                                                                this.TXT01_WREMARKS.GetValue().ToString(),   // 적요
                                                                this.TXT01_WTRADNAME.GetValue().ToString(),  // 이름(상호)
                                                                this.TXT01_WTRADTEL.GetValue().ToString(),   // 전화번호
                                                                this.CBO01_WNATIVEGB.GetValue().ToString(),  // 내외국인구분
                                                                this.CBH01_WCOUNTRY.GetValue().ToString(),   // 거주지국
                                                                this.TXT01_WTOTALAMT.GetValue().ToString(),  // 지급총액
                                                                this.TXT01_WHOLDYUL.GetValue().ToString(),   // 원천징수세율
                                                                this.TXT01_WCOSTAMT.GetValue().ToString(),   // 필요경비
                                                                this.TXT01_WINCOMAMT.GetValue().ToString(),  // 소득금액
                                                                this.TXT01_WTAXINCOM.GetValue().ToString(),  // 소득세
                                                                this.TXT01_WLOCALTAX.GetValue().ToString(),  // 지방소득세
                                                                this.TXT01_WLSUMTAX.GetValue().ToString(),   // 합계
                                                                this.TXT01_WADDRESS.GetValue().ToString(),   // 주소
                                                                this.TXT01_WBUSIGBN.GetValue().ToString(),   // 사업소득업종구분
                                                                this.CBH01_WECTGUBN.GetValue().ToString(),   // 기타소득구분
                                                                fsWBRANCH.ToString(),                        // 지점구분
                                                                this.DTP01_WREYYMM.GetValue().ToString(),    // 귀속년원
                                                                this.DTP01_WGIDATE.GetValue().ToString(),    // 지급년월일
                                                                this.CBH01_WINCOME.GetValue().ToString(),    // 소득구분
                                                                this.CBH01_WRDEPART.GetValue().ToString(),   // 귀속부서
                                                                TYUserInfo.SecureKey, "Y",
                                                                sWRJUMIN,                                    // 주민번호(사업자등록번호)
                                                                this.CBO01_WRGUNMU.GetValue().ToString()     // 지역구분
                                                                ); //저장

                    this.DbConnector.ExecuteTranQuery();

                    this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
                }
            }
        }
        #endregion

        #region Description : 삭제버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            
            // 생성 체크  WSUMMARYTF
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_BBG92747", "1", "", fsWREYYMM);

            if (this.DbConnector.ExecuteDataTable().Rows.Count == 0)
            {
                if (this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_4A6FB148", fsWBRANCH,
                                                                fsWREYYMM,
                                                                fsWGIDATE,
                                                                fsWINCOME,
                                                                fsWRDEPART,
                                                                TYUserInfo.SecureKey, "Y",
                                                                fsWRJUMIN,
                                                                fsWRGUNMU
                                                                );

                    this.DbConnector.ExecuteNonQuery();
                    this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Description : 닫기버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 소득자료 가져오기
        private void UP_SELECT(string WBRANCH, string WREYYMM, string WGIDATE, string WINCOME, string WRDEPART, string WRJUMIN, string WRGUNMU)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_BBGA6748",
                WBRANCH,
                WRGUNMU,
                WREYYMM
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                BTN61_REM.Visible = false;
                BTN61_SAV.Visible = false;
            }
            else
            {
                BTN61_REM.Visible = true;
                BTN61_SAV.Visible = true;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4A1HV117",
                TYUserInfo.SecureKey, "Y",
                WBRANCH,
                WRGUNMU,
                WREYYMM,
                WGIDATE,
                WINCOME,
                WRDEPART,
                TYUserInfo.SecureKey, "Y",
                WRJUMIN
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsWBRANCH = dt.Rows[0]["WBRANCH"].ToString();
                CBO01_WRGUNMU.SetValue(dt.Rows[0]["WRGUNMU"].ToString());
                DTP01_WREYYMM.SetValue(dt.Rows[0]["WREYYMM"].ToString());
                DTP01_WGIDATE.SetValue(dt.Rows[0]["WGIDATE"].ToString());
                CBH01_WINCOME.SetValue(dt.Rows[0]["WINCOME"].ToString());
                CBH01_WRDEPART.SetValue(dt.Rows[0]["WRDEPART"].ToString());

                if (CBH01_WINCOME.GetValue().ToString() == "A50" || CBH01_WINCOME.GetValue().ToString() == "A60")
                {
                    TXT01_VNSAUPNO.SetValue(dt.Rows[0]["WRJUMIN"].ToString());
                    TXT01_VNSAUPNO.Visible = true;
                    this.BTN61_GGUBUN.Visible = true;
                    TXT01_WRJUMIN.Visible = false;
                }
                else
                {
                    TXT01_WRJUMIN.SetValue(dt.Rows[0]["WRJUMIN"].ToString());
                    TXT01_VNSAUPNO.Visible = false;
                    this.BTN61_GGUBUN.Visible = false;
                    TXT01_WRJUMIN.Visible = true;
                }

                CBH01_WGBINCOM.SetValue(dt.Rows[0]["WGBINCOM"].ToString());
                CBH01_WPAYGUBN.SetValue(dt.Rows[0]["WPAYGUBN"].ToString());
                TXT01_WDAYWORK.SetValue(dt.Rows[0]["WDAYWORK"].ToString());
                TXT01_WREMARKS.SetValue(dt.Rows[0]["WREMARKS"].ToString());
                TXT01_WTRADNAME.SetValue(dt.Rows[0]["WTRADNAME"].ToString());
                TXT01_WTRADTEL.SetValue(dt.Rows[0]["WTRADTEL"].ToString());
                CBO01_WNATIVEGB.SetValue(dt.Rows[0]["WNATIVEGB"].ToString());
                CBH01_WCOUNTRY.SetValue(dt.Rows[0]["WCOUNTRY"].ToString());
                TXT01_WTOTALAMT.SetValue(dt.Rows[0]["WTOTALAMT"].ToString());
                TXT01_WHOLDYUL.SetValue(dt.Rows[0]["WHOLDYUL"].ToString());
                TXT01_WCOSTAMT.SetValue(dt.Rows[0]["WCOSTAMT"].ToString());
                TXT01_WINCOMAMT.SetValue(dt.Rows[0]["WINCOMAMT"].ToString());
                TXT01_WTAXINCOM.SetValue(dt.Rows[0]["WTAXINCOM"].ToString());
                TXT01_WLOCALTAX.SetValue(dt.Rows[0]["WLOCALTAX"].ToString());
                TXT01_WLSUMTAX.SetValue(dt.Rows[0]["WLSUMTAX"].ToString());
                TXT01_WADDRESS.SetValue(dt.Rows[0]["WADDRESS"].ToString());
                TXT01_WBUSIGBN.SetValue(dt.Rows[0]["WBUSIGBN"].ToString());
                CBH01_WECTGUBN.SetValue(dt.Rows[0]["WECTGUBN"].ToString());
            }
        }
        #endregion

        #region Description : 소득구분 선택 이벤트
        private void CBH01_WINCOME_TextChanged(object sender, EventArgs e)
        {
            this.TXT01_VNSAUPNO.Visible = false;
            this.BTN61_GGUBUN.Visible = false;
            this.TXT01_WRJUMIN.Visible = true;

            this.TXT01_WTRADTEL.SetReadOnly(false);
            this.TXT01_WDAYWORK.SetReadOnly(false);
            this.CBH01_WGBINCOM.SetReadOnly(false);
            this.CBH01_WPAYGUBN.SetReadOnly(false);
            this.TXT01_WBUSIGBN.SetReadOnly(false);
            this.CBH01_WECTGUBN.SetReadOnly(false);

            this.TXT01_WBUSIGBN.Text = "";

            if (this.CBH01_WINCOME.GetValue().ToString() == "A03")       //일용근로
            {
                this.CBH01_WGBINCOM.SetReadOnly(true);
                this.CBH01_WPAYGUBN.SetReadOnly(true);
                this.TXT01_WBUSIGBN.SetReadOnly(true);
                this.CBH01_WECTGUBN.SetReadOnly(true);

                this.TXT01_WHOLDYUL.Text = "6.0000";
            }
            else if (this.CBH01_WINCOME.GetValue().ToString() == "A25")  //사업소득
            {
                this.TXT01_WTRADTEL.SetReadOnly(true);
                this.TXT01_WDAYWORK.SetReadOnly(true);
                this.CBH01_WGBINCOM.SetReadOnly(true);
                this.CBH01_WPAYGUBN.SetReadOnly(true);
                this.CBH01_WECTGUBN.SetReadOnly(true);

                this.TXT01_WHOLDYUL.Text = "3.0000";
                this.TXT01_WBUSIGBN.Text = "940909";
            }
            else if (this.CBH01_WINCOME.GetValue().ToString() == "A42")  //기타소득
            {
                this.TXT01_WTRADTEL.SetReadOnly(true);
                this.TXT01_WDAYWORK.SetReadOnly(true);
                this.CBH01_WGBINCOM.SetReadOnly(true);
                this.CBH01_WPAYGUBN.SetReadOnly(true);
                this.TXT01_WBUSIGBN.SetReadOnly(true);

                this.TXT01_WHOLDYUL.Text = "20.000";
            }
            else if (this.CBH01_WINCOME.GetValue().ToString() == "A50" || this.CBH01_WINCOME.GetValue().ToString() == "A60") //이자,배당소득
            {
                this.TXT01_WTRADTEL.SetReadOnly(true);
                this.TXT01_WDAYWORK.SetReadOnly(true);
                this.TXT01_WBUSIGBN.SetReadOnly(true);
                this.CBH01_WECTGUBN.SetReadOnly(true);
                this.CBO01_WNATIVEGB.SetReadOnly(true);

                this.TXT01_VNSAUPNO.Visible = true;
                this.BTN61_GGUBUN.Visible = true;
                this.TXT01_WRJUMIN.Visible = false;
            }
        }
        #endregion

        #region Description : 사업자(주민)번호 선택 이벤트
        private void TXT01_VNSAUPNO_TextChanged(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_AC_49JGP986",
                "",
                "",
                TYUserInfo.SecureKey,
                "Y",
                TXT01_VNSAUPNO.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_WGBINCOM.SetValue(dt.Rows[0]["WSINCOME"].ToString());
                CBH01_WPAYGUBN.SetValue(dt.Rows[0]["WSICCODE"].ToString());
                TXT01_WTRADNAME.SetValue(dt.Rows[0]["WSCMNAME"].ToString());
                TXT01_WADDRESS.SetValue(dt.Rows[0]["WSADDRES"].ToString());
            }
        }
        #endregion

        #region Description : 유효성체크
        private bool UP_KeyCehck(string WRJUMIN)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_4A1HV117",
                TYUserInfo.SecureKey, "Y",
                fsWBRANCH.ToString(),
                this.CBO01_WRGUNMU.GetValue().ToString(),
                this.DTP01_WREYYMM.GetValue().ToString(),
                this.DTP01_WGIDATE.GetValue().ToString(),
                this.CBH01_WINCOME.GetValue().ToString(),
                this.CBH01_WRDEPART.GetValue().ToString(),
                TYUserInfo.SecureKey, "Y",
                WRJUMIN
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Description : 필드체크
        private bool UP_FieldCheck(){
            // 공통 항목
            if (this.TXT01_WTRADNAME.GetValue().ToString() == "")        //이름(상호)
            {
                this.ShowMessage("TY_M_AC_4A6BR130");
                this.TXT01_WTRADNAME.Focus();
                return false;
            }
            else if (this.TXT01_WREMARKS.GetValue().ToString() == "")    //적요
            {
                this.ShowMessage("TY_M_AC_4A6BS131");
                this.TXT01_WREMARKS.Focus();
                return false;
            }
            else if (this.TXT01_WHOLDYUL.GetValue().ToString() == "")    //원천징세율
            {
                this.ShowMessage("TY_M_AC_4A6BS132");
                this.TXT01_WHOLDYUL.Focus();
                return false;
            }
            else if (this.TXT01_WTOTALAMT.GetValue().ToString() == "")   //지급총액
            {
                this.ShowMessage("TY_M_AC_4A6BS133");
                this.TXT01_WTOTALAMT.Focus();
                return false;
            }
            else if (this.TXT01_WINCOMAMT.GetValue().ToString() == "")   //소득금액
            {
                this.ShowMessage("TY_M_AC_4A6BS134");
                this.TXT01_WINCOMAMT.Focus();
                return false;
            }
            else if (this.TXT01_WTAXINCOM.GetValue().ToString() == "")   //소득세
            {
                this.ShowMessage("TY_M_AC_4A6BS135");
                this.TXT01_WTAXINCOM.Focus();
                return false;
            }
            else if (this.TXT01_WLOCALTAX.GetValue().ToString() == "")   //지방소득세
            {
                this.ShowMessage("TY_M_AC_4A6BS136");
                this.TXT01_WLOCALTAX.Focus();
                return false;
            }
            else if (this.TXT01_WLSUMTAX.GetValue().ToString() == "")    //세액계
            {
                this.ShowMessage("TY_M_AC_4A6BS137");
                this.TXT01_WLSUMTAX.Focus();
                return false;
            }
            else if (this.TXT01_WADDRESS.GetValue().ToString() == "")    //주소
            {
                this.ShowMessage("TY_M_AC_4A6BT138");
                this.TXT01_WADDRESS.Focus();
                return false;
            }

            if (this.TXT01_VNSAUPNO.GetValue().ToString() == "" && this.TXT01_WRJUMIN.GetValue().ToString() == "")
            {
                if (this.CBH01_WINCOME.GetValue().ToString() == "A50" || this.CBH01_WINCOME.GetValue().ToString() == "A60")
                {
                    if (this.TXT01_VNSAUPNO.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_BC1FM853");
                        this.TXT01_WADDRESS.Focus();
                        return false;
                    }
                }
                else
                {
                    if (this.TXT01_WRJUMIN.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_2454S464");
                        this.TXT01_WADDRESS.Focus();
                        return false;
                    }
                }
            }


            if (this.CBH01_WINCOME.GetValue().ToString() == "A03")       //일용근로
            {
                if (this.TXT01_WTRADTEL.GetValue().ToString() == "")     
                {
                    this.ShowMessage("TY_M_AC_4A6BT141");
                    this.TXT01_WTRADTEL.Focus();
                    return false;
                }
                else if (this.CBO01_WNATIVEGB.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BT142");
                    this.CBO01_WNATIVEGB.Focus();
                    return false;
                }
                else if (this.TXT01_WDAYWORK.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BU143");
                    this.TXT01_WDAYWORK.Focus();
                    return false;
                }
            }
            else if (this.CBH01_WINCOME.GetValue().ToString() == "A25")  //사업소득
            {
                if (this.CBO01_WNATIVEGB.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BT142");
                    this.CBO01_WNATIVEGB.Focus();
                    return false;
                }
                else if (this.TXT01_WBUSIGBN.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BV145");
                    this.TXT01_WBUSIGBN.Focus();
                    return false;
                }

                if (  Convert.ToDouble(Get_Numeric(this.TXT01_WHOLDYUL.GetValue().ToString()))  != 3 && Convert.ToDouble(Get_Numeric(this.TXT01_WHOLDYUL.GetValue().ToString())) != 5)
                {
                    this.ShowCustomMessage("세율을 확인하세요.(3%,5%)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.TXT01_WHOLDYUL.Focus();
                    return false;
                }

                if (this.TXT01_WBUSIGBN.Text == "940909" && Convert.ToDouble(Get_Numeric(this.TXT01_WHOLDYUL.GetValue().ToString())) != 3)
                {
                    this.ShowCustomMessage("세율을 확인하세요.(3%)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.TXT01_WHOLDYUL.Focus();
                    return false;
                }
            }
            else if (this.CBH01_WINCOME.GetValue().ToString() == "A42")  //기타소득
            {
                if (this.CBO01_WNATIVEGB.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BT142");
                    this.CBO01_WNATIVEGB.Focus();
                    return false;
                }
                else if (this.CBH01_WECTGUBN.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BU144");
                    this.CBH01_WECTGUBN.CodeText.Focus();
                    return false;
                }


                //  필요경비
                if (( this.CBH01_WECTGUBN.GetValue().ToString() == "62" || this.CBH01_WECTGUBN.GetValue().ToString() == "64" ||
                      this.CBH01_WECTGUBN.GetValue().ToString() == "71" || this.CBH01_WECTGUBN.GetValue().ToString() == "72" ||
                      this.CBH01_WECTGUBN.GetValue().ToString() == "73" || this.CBH01_WECTGUBN.GetValue().ToString() == "74" ||
                      this.CBH01_WECTGUBN.GetValue().ToString() == "75" || this.CBH01_WECTGUBN.GetValue().ToString() == "76") &&
                    Convert.ToDouble(Get_Numeric(this.TXT01_WCOSTAMT.GetValue().ToString())) == 0)
                {

                    this.ShowCustomMessage("필요경비를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.TXT01_WCOSTAMT.Focus();
                    return false;
                }

                //  필요경비
                if ((this.CBH01_WECTGUBN.GetValue().ToString() == "60" || this.CBH01_WECTGUBN.GetValue().ToString() == "63" || this.CBH01_WECTGUBN.GetValue().ToString() == "68") &&
                    Convert.ToDouble(Get_Numeric(this.TXT01_WCOSTAMT.GetValue().ToString())) != 0)
                {

                    this.ShowCustomMessage("필요경비를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.TXT01_WCOSTAMT.Focus();
                    return false;
                }


                //  세율 20 %
                if ( (this.CBH01_WECTGUBN.GetValue().ToString() == "60" || this.CBH01_WECTGUBN.GetValue().ToString() == "62" ||
                      this.CBH01_WECTGUBN.GetValue().ToString() == "63" || this.CBH01_WECTGUBN.GetValue().ToString() == "64" ||
                      this.CBH01_WECTGUBN.GetValue().ToString() == "71" || this.CBH01_WECTGUBN.GetValue().ToString() == "72" ||
                      this.CBH01_WECTGUBN.GetValue().ToString() == "73" || this.CBH01_WECTGUBN.GetValue().ToString() == "74" ||
                      this.CBH01_WECTGUBN.GetValue().ToString() == "75" || this.CBH01_WECTGUBN.GetValue().ToString() == "76" ) &&
                    Convert.ToDouble(Get_Numeric(this.TXT01_WHOLDYUL.GetValue().ToString())) != 20) 
                {

                    this.ShowCustomMessage("세율을 확인하세요(20 %).", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.TXT01_WHOLDYUL.Focus();
                    return false;
                }

                //  세율 0 %
                if ((this.CBH01_WECTGUBN.GetValue().ToString() == "61" || this.CBH01_WECTGUBN.GetValue().ToString() == "68") &&
                    Convert.ToDouble(Get_Numeric(this.TXT01_WHOLDYUL.GetValue().ToString())) != 0)
                {
                    this.ShowCustomMessage("세율을 확인하세요(0 %).", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.TXT01_WHOLDYUL.Focus();
                    return false;
                }

                //  세율 20 , 30 %
                if (this.CBH01_WECTGUBN.GetValue().ToString() == "69")
                {
                    if (Convert.ToDouble(this.TXT01_WINCOMAMT.GetValue()) > 300000000 && Convert.ToDouble(Get_Numeric(this.TXT01_WHOLDYUL.GetValue().ToString())) != 30)
                    {
                        this.ShowCustomMessage("세율을 확인하세요(30 %).", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.TXT01_WHOLDYUL.Focus();
                        return false;
                    }
                    else if (Convert.ToDouble(Get_Numeric(this.TXT01_WHOLDYUL.GetValue().ToString())) != 20)
                    {
                        this.ShowCustomMessage("세율을 확인하세요(20 %).", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.TXT01_WHOLDYUL.Focus();
                        return false;
                    }
                }

            }
            else if (this.CBH01_WINCOME.GetValue().ToString() == "A50" || this.CBH01_WINCOME.GetValue().ToString() == "A60")  //이자소득, 배당소득
            {
                if (this.CBH01_WGBINCOM.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BT139");
                    this.CBH01_WGBINCOM.CodeText.Focus();
                    return false;
                }
                else if (this.CBH01_WPAYGUBN.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_4A6BT140");
                    this.CBH01_WPAYGUBN.CodeText.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Description 소득금액 자동계산
        private void TXT01_WTOTALAMT_TextChanged(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;
            if (this.TXT01_WTOTALAMT.GetValue().ToString() != "")
            {
                a = Convert.ToInt32(this.TXT01_WTOTALAMT.GetValue().ToString());
            }
            if (this.TXT01_WCOSTAMT.GetValue().ToString() != "")
            {
                b = Convert.ToInt32(this.TXT01_WCOSTAMT.GetValue().ToString());
            }
            this.TXT01_WINCOMAMT.SetValue((a - b).ToString());
        }

        private void TXT01_WCOSTAMT_TextChanged(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;
            if (this.TXT01_WTOTALAMT.GetValue().ToString() != "")
            {
                a = Convert.ToInt32(this.TXT01_WTOTALAMT.GetValue().ToString());
            }
            if (this.TXT01_WCOSTAMT.GetValue().ToString() != "")
            {
                b = Convert.ToInt32(this.TXT01_WCOSTAMT.GetValue().ToString());
            }

            this.TXT01_WINCOMAMT.SetValue((a - b).ToString());
        }
        #endregion

        #region Description 세액계 자동계산
        private void TXT01_WTAXINCOM_TextChanged(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;
            if (this.TXT01_WTAXINCOM.GetValue().ToString() != "")
            {
                a = Convert.ToInt32(this.TXT01_WTAXINCOM.GetValue().ToString());
            }
            if (this.TXT01_WLOCALTAX.GetValue().ToString() != "")
            {
                b = Convert.ToInt32(this.TXT01_WLOCALTAX.GetValue().ToString());
            }

            this.TXT01_WLSUMTAX.SetValue((a + b).ToString());
        }

        private void TXT01_WLOCALTAX_TextChanged(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;
            if (this.TXT01_WTAXINCOM.GetValue().ToString() != "")
            {
                a = Convert.ToInt32(this.TXT01_WTAXINCOM.GetValue().ToString());
            }
            if (this.TXT01_WLOCALTAX.GetValue().ToString() != "")
            {
                b = Convert.ToInt32(this.TXT01_WLOCALTAX.GetValue().ToString());
            }

            this.TXT01_WLSUMTAX.SetValue((a + b).ToString());
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear()
        {
            fsWBRANCH = "1";

            this.CBO01_WRGUNMU.SetValue("1");
            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));
            this.DTP01_WGIDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.CBH01_WINCOME.SetValue("");
            this.CBH01_WRDEPART.SetValue("");
            this.TXT01_VNSAUPNO.SetValue("");
            this.TXT01_WRJUMIN.SetValue("");

            this.CBH01_WGBINCOM.SetValue("");
            this.CBH01_WPAYGUBN.SetValue("");
            this.TXT01_WDAYWORK.SetValue("");
            this.TXT01_WREMARKS.SetValue("");
            this.TXT01_WTRADNAME.SetValue("");
            this.TXT01_WTRADTEL.SetValue("");
            this.CBO01_WNATIVEGB.SetValue("");
            this.CBH01_WCOUNTRY.SetValue("");
            this.TXT01_WTOTALAMT.SetValue("");
            this.TXT01_WHOLDYUL.SetValue("");
            this.TXT01_WCOSTAMT.SetValue("");
            this.TXT01_WINCOMAMT.SetValue("");
            this.TXT01_WTAXINCOM.SetValue("");
            this.TXT01_WLOCALTAX.SetValue("");
            this.TXT01_WLSUMTAX.SetValue("");
            this.TXT01_WADDRESS.SetValue("");
            this.TXT01_WBUSIGBN.SetValue("");
            this.CBH01_WECTGUBN.SetValue("");
        }
        #endregion

        #region Description : 코드박스 - 이자배당소득 추가입력(주민번호)
        private void BTN61_GGUBUN_Click(object sender, EventArgs e)
        {
            TYERGB024P popup = new TYERGB024P("TY_P_AC_4A69P128");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_VNSAUPNO.SetValue(popup.fsCode);
            }
        }
        #endregion


    }
}
