using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// UTILITY 단가 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.04 10:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_674AM536 : UTILITY 단가 등록
    ///  TY_P_UT_674AN537 : UTILITY 단가 수정
    ///  TY_P_UT_674DE549 : UTILITY 단가 확인
    ///  TY_P_UT_674FJ555 : 가열료 조회(UTILITY 단가 등록)
    ///  TY_P_UT_674FM556 : SK 가열료 수정(UTILITY 단가 등록)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  DNYYMM : 년월
    ///  DNBKCU : 벙커C유
    ///  DNELECT : 전기료
    ///  DNJILSO : 질소사용료
    ///  DNKYUNG : 경유
    ///  DNMOTER1 : 모터용량
    ///  DNMOTER2 : 모터용량
    ///  DNSELAMT : 전기총사용금액
    ///  DNSELDANGA : 전기사용단가
    ///  DNSELECT : 전기료
    ///  DNSELTIM : 전기총사용시간
    ///  DNSKSTEAM : SK스팀
    ///  DNSKTAMT : SK스팀총사용금액
    ///  DNSTAMT : 스팀총금액
    ///  DNSTDANGA : 스팀단가
    ///  DNSTTIM : 스팀총사용시간
    ///  DNYUL : 효율
    /// </summary>
    public partial class TYUTPS001I : TYBase
    {
        private string fsRMREDATE = string.Empty;
        private string fsRMHMCODE = string.Empty;
        private string fsRMHWAJU  = string.Empty;

        public TYUTPS001I(string sRMREDATE, string sRMHMCODE, string sRMHWAJU)
        {
            InitializeComponent();

            fsRMREDATE = sRMREDATE;
            fsRMHMCODE = sRMHMCODE;
            fsRMHWAJU  = sRMHWAJU;
        }

        #region Description : 페이지 로드
        private void TYUTPS001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Initialize();

            if (string.IsNullOrEmpty(fsRMREDATE) && string.IsNullOrEmpty(fsRMHMCODE) && string.IsNullOrEmpty(fsRMHWAJU))
            {
                this.DTP01_RMREDATE.SetReadOnly(false);
                this.CBH01_RMHMCODE.SetReadOnly(false);
                this.CBH01_RMHWAJU.SetReadOnly(false);

                this.DTP01_RMREDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

                SetStartingFocus(this.DTP01_RMREDATE);
            }
            else
            {
                this.DTP01_RMREDATE.SetReadOnly(true);
                this.CBH01_RMHMCODE.SetReadOnly(true);
                this.CBH01_RMHWAJU.SetReadOnly(true);

                this.DTP01_RMREDATE.SetValue(Set_Date(fsRMREDATE.ToString()));
                this.CBH01_RMHMCODE.SetValue(fsRMHMCODE.ToString());
                this.CBH01_RMHWAJU.SetValue(fsRMHWAJU.ToString());

                UP_RUN(fsRMREDATE, fsRMHMCODE, fsRMHWAJU);

                SetStartingFocus(this.TXT01_RMCASNO);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsRMREDATE = "";
            fsRMHMCODE = "";
            fsRMHWAJU = "";

            this.DTP01_RMREDATE.SetReadOnly(false);
            this.CBH01_RMHMCODE.SetReadOnly(false);
            this.CBH01_RMHWAJU.SetReadOnly(false);

            SetStartingFocus(this.DTP01_RMREDATE);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sGMHIGB = string.Empty;

                if (string.IsNullOrEmpty(fsRMREDATE) && string.IsNullOrEmpty(fsRMHMCODE) && string.IsNullOrEmpty(fsRMHWAJU))
                {
                    sGMHIGB = "A";

                    fsRMREDATE = Get_Date(this.DTP01_RMREDATE.GetValue().ToString());
                    fsRMHMCODE = this.CBH01_RMHMCODE.GetValue().ToString();
                    fsRMHWAJU  = this.CBH01_RMHWAJU.GetValue().ToString();
                }
                else
                {
                    sGMHIGB = "C";
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B2IH9663", Get_Date(this.DTP01_RMREDATE.GetValue().ToString().Trim()),
                                                            this.CBH01_RMHMCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_RMHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_RMCASNO.GetValue().ToString().Trim(),
                                                            this.TXT01_RMMOLECULE.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_RMBURSTMIN.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_RMBURSTMAX.GetValue().ToString().Trim()),
                                                            this.TXT01_RMEXPOSE.GetValue().ToString().Trim(),
                                                            this.TXT01_RMTOXICO.GetValue().ToString().Trim(),
                                                            this.TXT01_RMTOXICP.GetValue().ToString().Trim(),
                                                            this.TXT01_RMTOXICI.GetValue().ToString().Trim(),
                                                            this.TXT01_RMFLASH.GetValue().ToString().Trim(),
                                                            this.TXT01_RMIGNITE.GetValue().ToString().Trim(),
                                                            this.TXT01_RMSTEAM.GetValue().ToString().Trim(),
                                                            this.TXT01_RMCORROSIVE.GetValue().ToString().Trim(),
                                                            this.TXT01_RMREACTION.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_RMDAYQTY.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_RMSAVEQTY.GetValue().ToString().Trim()),
                                                            this.TXT01_RMBIGO.GetValue().ToString().Trim(),
                                                            TYUserInfo.EmpNo,
                                                            Get_Date(this.DTP01_RMREDATE.GetValue().ToString().Trim()),
                                                            this.CBH01_RMHMCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_RMHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_RMCASNO.GetValue().ToString().Trim(),
                                                            this.TXT01_RMMOLECULE.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_RMBURSTMIN.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_RMBURSTMAX.GetValue().ToString().Trim()),
                                                            this.TXT01_RMEXPOSE.GetValue().ToString().Trim(),
                                                            this.TXT01_RMTOXICO.GetValue().ToString().Trim(),
                                                            this.TXT01_RMTOXICP.GetValue().ToString().Trim(),
                                                            this.TXT01_RMTOXICI.GetValue().ToString().Trim(),
                                                            this.TXT01_RMFLASH.GetValue().ToString().Trim(),
                                                            this.TXT01_RMIGNITE.GetValue().ToString().Trim(),
                                                            this.TXT01_RMSTEAM.GetValue().ToString().Trim(),
                                                            this.TXT01_RMCORROSIVE.GetValue().ToString().Trim(),
                                                            this.TXT01_RMREACTION.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_RMDAYQTY.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_RMSAVEQTY.GetValue().ToString().Trim()),
                                                            this.TXT01_RMBIGO.GetValue().ToString().Trim(),
                                                            TYUserInfo.EmpNo
                                                            );

                this.DbConnector.ExecuteNonQuery();

                UP_RUN(fsRMREDATE, fsRMHMCODE, fsRMHWAJU);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch(Exception ex)
            {
                string smsg = ex.ToString();
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 필드 값 입력
        private void UP_RUN(string sRMREDATE, string sRMHMCODE, string sRMHWAJU)
        {
            try
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B2IBG656", sRMREDATE.ToString(), sRMHMCODE.ToString(), sRMHWAJU.ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    this.SetFocus(this.TXT01_RMCASNO);

                    this.DTP01_RMREDATE.SetReadOnly(true);
                    this.CBH01_RMHMCODE.SetReadOnly(true);
                    this.CBH01_RMHWAJU.SetReadOnly(true);
                }
                else
                {
                    this.DTP01_RMREDATE.SetReadOnly(false);
                    this.CBH01_RMHMCODE.SetReadOnly(false);
                    this.CBH01_RMHWAJU.SetReadOnly(false);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_Initialize()
        {
            this.TXT01_RMCASNO.SetValue("");
            this.TXT01_RMMOLECULE.SetValue("");
            this.TXT01_RMBURSTMIN.SetValue("");
            this.TXT01_RMBURSTMAX.SetValue("");
            this.TXT01_RMEXPOSE.SetValue("");
            this.TXT01_RMTOXICO.SetValue("");
            this.TXT01_RMTOXICP.SetValue("");
            this.TXT01_RMTOXICI.SetValue("");
            this.TXT01_RMFLASH.SetValue("");
            this.TXT01_RMIGNITE.SetValue("");
            this.TXT01_RMSTEAM.SetValue("");
            this.TXT01_RMCORROSIVE.SetValue("");
            this.TXT01_RMREACTION.SetValue("");
            this.TXT01_RMDAYQTY.SetValue("");
            this.TXT01_RMSAVEQTY.SetValue("");
            this.TXT01_RMBIGO.SetValue("");
        }
        #endregion

        #region Description : 엔터키 이벤트(포커스 이동)
        private void TXT01_RMMOLECULE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_RMBURSTMIN);
            }
        }

        private void TXT01_RMBURSTMIN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_RMBURSTMAX);
            }
        }

        private void TXT01_RMBURSTMAX_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_RMEXPOSE);
            }
        }

        private void TXT01_RMEXPOSE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_RMTOXICO);
            }
        }

        private void TXT01_RMTOXICO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_RMTOXICP);
            }
        }

        private void TXT01_RMTOXICP_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_RMTOXICI);
            }
        }

        private void TXT01_RMTOXICI_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_RMFLASH);
            }
        }

        private void TXT01_RMBIGO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion
    }
}
