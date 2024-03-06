using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.AT00
{
    /// <summary>
    /// 사택기본사항 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.16 18:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88EI5571 : 아파트 세대별 관리 등록
    ///  TY_P_HR_88GG3580 : 아파트 세대별 관리 수정
    ///  TY_P_HR_88GG4581 : 아파트 세대별 관리 삭제
    ///  TY_P_HR_88GGB582 : 아파트 기본사항 등록
    ///  TY_P_HR_88GGD583 : 아파트 기본사항 수정
    ///  TY_P_HR_88GGD584 : 아파트 기본사항 삭제
    ///  TY_P_HR_88GHY587 : 아파트 기본사항 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  APSABUN : 사번
    ///  APGUBN : 직원구분
    ///  APEDATE : 퇴거일자
    ///  APIDATE : 입주일자
    ///  APDEPOAMT : 보증금
    ///  APHANGL : 성명
    ///  APHOSU : 호수
    ///  APJUMIN : 생년월일
    ///  APTEL : 연락처
    ///  APWKCOMPY : 근무처
    /// </summary>
    public partial class TYATKB002I : TYBase
    {
        private string fsAPHOSU = string.Empty;
        private string fsAPIDATE = string.Empty;

        #region Description : 폼 로드
        public TYATKB002I(string sAPHOSU, string sAPIDATE)
        {
            InitializeComponent();

            fsAPHOSU = sAPHOSU;
            fsAPIDATE = sAPIDATE;
        }

        private void TYATKB002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            // 신규 등록
            if (string.IsNullOrEmpty(fsAPHOSU) && string.IsNullOrEmpty(fsAPIDATE))
            {
                UP_FiledLock(false);
                this.DTP01_APIDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
                this.DTP01_APEDATE.SetValue("");
                SetStartingFocus(this.TXT01_APHOSU);
            }
            else if (fsAPHOSU != "" && string.IsNullOrEmpty(fsAPIDATE))
            {
                UP_FiledLock(false);
                this.TXT01_APHOSU.SetValue(fsAPHOSU);
                SetStartingFocus(this.TXT01_APHOSU);
            }
            else
            {
                UP_FiledLock(true);
                UP_Run();
                SetStartingFocus(this.DTP01_APEDATE);
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

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88GGD584", this.TXT01_APHOSU.GetValue().ToString(),
                                                        this.DTP01_APIDATE.GetString());
            this.DbConnector.ExecuteTranQuery();

            this.DbConnector.CommandClear();
            // 세대별 관리 업데이트(입주여부)
            this.DbConnector.Attach("TY_P_HR_88GG3580", "N",
                                                        TYUserInfo.EmpNo,
                                                        this.TXT01_APHOSU.GetValue().ToString()
                                                        );
            this.DbConnector.ExecuteTranQuery();

            UP_FieldClear();

            fsAPHOSU = "";
            fsAPIDATE = "";

            UP_FiledLock(false);
            SetStartingFocus(this.TXT01_APHOSU);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion  

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sAPTCHECK = string.Empty;

                string sAPEDATE = this.DTP01_APEDATE.GetString();

                if (this.DTP01_APEDATE.GetValue().ToString() == "19000101" || this.DTP01_APEDATE.GetValue().ToString() == "")
                {
                    sAPEDATE = "";
                }

                this.DbConnector.CommandClear();

                // 신규
                if (string.IsNullOrEmpty(fsAPIDATE))
                {
                    this.DbConnector.Attach("TY_P_HR_88GGB582", this.TXT01_APHOSU.GetValue().ToString(),
                                                                this.DTP01_APIDATE.GetString(),
                                                                sAPEDATE,
                                                                this.TXT01_APJUMIN.GetValue().ToString(),
                                                                this.TXT01_APHANGL.GetValue().ToString(),
                                                                this.TXT01_APDEPOAMT.GetValue().ToString(),
                                                                this.TXT01_APTEL.GetValue().ToString(),
                                                                this.TXT01_APWKCOMPY.GetValue().ToString(),
                                                                this.CBO01_APGUBN.GetValue().ToString(),
                                                                this.CBH01_APSABUN.GetValue().ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                }
                // 수정
                else
                {
                    this.DbConnector.Attach("TY_P_HR_88GGD583", sAPEDATE,
                                                                this.TXT01_APJUMIN.GetValue().ToString(),
                                                                this.TXT01_APHANGL.GetValue().ToString(),
                                                                this.TXT01_APDEPOAMT.GetValue().ToString(),
                                                                this.TXT01_APTEL.GetValue().ToString(),
                                                                this.TXT01_APWKCOMPY.GetValue().ToString(),
                                                                this.CBO01_APGUBN.GetValue().ToString(),
                                                                this.CBH01_APSABUN.GetValue().ToString(),
                                                                TYUserInfo.EmpNo,
                                                                this.TXT01_APHOSU.GetValue().ToString(),
                                                                this.DTP01_APIDATE.GetString()
                                                                );
                }
                this.DbConnector.ExecuteTranQuery();

                // 현재 입주여부 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_8AMA3997", this.TXT01_APHOSU.GetValue().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sAPTCHECK = "Y";
                }
                else
                {
                    sAPTCHECK = "N";
                }

                this.DbConnector.CommandClear();
                // 세대별 관리 업데이트(입주여부)
                this.DbConnector.Attach("TY_P_HR_88GG3580", sAPTCHECK,
                                                            TYUserInfo.EmpNo,
                                                            this.TXT01_APHOSU.GetValue().ToString()
                                                            );
                this.DbConnector.ExecuteTranQuery();

                this.ShowMessage("TY_M_GB_23NAD873");

                fsAPHOSU = this.TXT01_APHOSU.GetValue().ToString();
                fsAPIDATE = this.DTP01_APIDATE.GetString();

                UP_FiledLock(true);
                UP_Run();
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_8ACHV943", this.TXT01_APHOSU.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (string.IsNullOrEmpty(fsAPIDATE))
            {
                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("입주중인 호수 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }   
            
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_8AFAM948", this.TXT01_APHOSU.GetValue().ToString());
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("존재하지않는 호수 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 확인 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88GHY587", fsAPHOSU, fsAPIDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion

        #region Description : key 잠금
        private void UP_FiledLock(bool b)
        {
            if (b == true)
            {
                this.TXT01_APHOSU.SetReadOnly(true);
                this.DTP01_APIDATE.SetReadOnly(true);
                this.BTN61_REM.Visible = true;
            }
            else
            {
                this.TXT01_APHOSU.SetReadOnly(false);
                this.DTP01_APIDATE.SetReadOnly(false);
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 화면 초기화
        private void UP_FieldClear()
        {   
            this.TXT01_APHOSU.SetValue("");
            this.DTP01_APIDATE.SetValue("");
            this.DTP01_APEDATE.SetValue("");
            this.TXT01_APJUMIN.SetValue("");
            this.TXT01_APHANGL.SetValue("");
            this.TXT01_APDEPOAMT.SetValue("");
            this.TXT01_APTEL.SetValue("");
            this.TXT01_APWKCOMPY.SetValue("");
            this.CBO01_APGUBN.SetValue("");
            this.CBH01_APSABUN.SetValue("");
        }
        #endregion

        #region Description : 직원구분 콤보박스 변경 이벤트
        private void CBO01_APGUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_APGUBN.GetValue().ToString() == "1")
            {
                this.CBH01_APSABUN.SetReadOnly(false);
            }
            else
            {
                this.CBH01_APSABUN.SetReadOnly(true);
                this.CBH01_APSABUN.SetValue("");
            }
        }
        #endregion
    }
}
