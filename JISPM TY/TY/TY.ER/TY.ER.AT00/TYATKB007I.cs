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
    /// 세대별 수선이력 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.22 16:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88MEF613 : 세대별 수선이력 등록
    ///  TY_P_HR_88MEG614 : 세대별 수선이력 수정
    ///  TY_P_HR_88MEG615 : 세대별 수선이력 삭제
    ///  TY_P_HR_88MGK621 : 세대별 수선이력 확인
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
    ///  APLDATE : 수선일자
    ///  APLBIGO : 비고
    ///  APLCONTENTS : 수선내용
    ///  APLHOSU : 호수
    ///  APLREPAMOUNT : 수선비용
    ///  APLSEQ : 순번
    ///  APLVEND : 수선업체
    /// </summary>
    public partial class TYATKB007I : TYBase
    {
        private string fsAPLHOSU = string.Empty;
        private string fsAPLDATE = string.Empty;
        private string fsAPLSEQ = string.Empty;

        #region Description : 폼 로드
        public TYATKB007I(string sAPLHOSU, string sAPLDATE, string sAPLSEQ)
        {
            InitializeComponent();

            fsAPLHOSU = sAPLHOSU;
            fsAPLDATE = sAPLDATE;
            fsAPLSEQ = sAPLSEQ;
        }

        private void TYATKB007I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            if (string.IsNullOrEmpty(fsAPLHOSU) && string.IsNullOrEmpty(fsAPLDATE) && string.IsNullOrEmpty(fsAPLSEQ))
            {
                UP_FiledLock(false);
                this.DTP01_APLDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
                SetStartingFocus(this.TXT01_APLHOSU);
            }
            else
            {
                UP_FiledLock(true);
                UP_Run();
                SetStartingFocus(this.TXT01_APLCONTENTS);
            }
            this.TXT01_APLSEQ.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            // 신규
            if (string.IsNullOrEmpty(fsAPLHOSU) && string.IsNullOrEmpty(fsAPLDATE) && string.IsNullOrEmpty(fsAPLSEQ))
            {
                

                this.DbConnector.Attach("TY_P_HR_88MEF613", this.TXT01_APLHOSU.GetValue().ToString(),
                                                            this.DTP01_APLDATE.GetString(),
                                                            this.TXT01_APLSEQ.GetValue().ToString(),
                                                            this.TXT01_APLCONTENTS.GetValue().ToString(),
                                                            this.TXT01_APLREPAMOUNT.GetValue().ToString(),
                                                            this.CBH01_APLVEND.GetValue().ToString(),
                                                            this.TXT01_APLBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            // 수정
            else
            {
                this.DbConnector.Attach("TY_P_HR_88MEG614", this.TXT01_APLCONTENTS.GetValue().ToString(),
                                                            this.TXT01_APLREPAMOUNT.GetValue().ToString(),
                                                            this.CBH01_APLVEND.GetValue().ToString(),
                                                            this.TXT01_APLBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            this.TXT01_APLHOSU.GetValue().ToString(),
                                                            this.DTP01_APLDATE.GetString(),
                                                            this.TXT01_APLSEQ.GetValue().ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQuery();

            fsAPLHOSU = this.TXT01_APLHOSU.GetValue().ToString();
            fsAPLDATE = this.DTP01_APLDATE.GetString();
            fsAPLSEQ = this.TXT01_APLSEQ.GetValue().ToString();

            UP_FiledLock(true);
            UP_Run();
            this.TXT01_APLCONTENTS.Focus();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_8AFAM948", this.TXT01_APLHOSU.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("존재하지않는 호수 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (string.IsNullOrEmpty(fsAPLHOSU) && string.IsNullOrEmpty(fsAPLDATE) && string.IsNullOrEmpty(fsAPLSEQ))
            {
                // 순번가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_88ND3631", this.TXT01_APLHOSU.GetValue().ToString(),
                                                            this.DTP01_APLDATE.GetString()
                                                            );
                Int16 iSeq = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                TXT01_APLSEQ.SetValue(Set_Fill3(iSeq.ToString()));
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88MEG615", this.TXT01_APLHOSU.GetValue().ToString(),
                                                        this.DTP01_APLDATE.GetString(),
                                                        this.TXT01_APLSEQ.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();

            UP_FieldClear();

            fsAPLHOSU = "";
            fsAPLDATE = "";
            fsAPLSEQ = "";

            UP_FiledLock(false);
            this.TXT01_APLHOSU.Focus();
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

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 확인 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88MGK621", fsAPLHOSU, fsAPLDATE, fsAPLSEQ);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                //this.TXT01_APLSEQ.SetValue(Set_Fill3(dt.Rows[0]["APLSEQ"].ToString()));
            }
        }
        #endregion

        #region Description : key 잠금
        private void UP_FiledLock(bool b)
        {
            if (b == true)
            {
                this.TXT01_APLHOSU.SetReadOnly(true);
                this.DTP01_APLDATE.SetReadOnly(true);
                this.BTN61_REM.Visible = true;
            }
            else
            {
                this.TXT01_APLHOSU.SetReadOnly(false);
                this.DTP01_APLDATE.SetReadOnly(false);
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 화면 초기화
        private void UP_FieldClear()
        {
            this.TXT01_APLHOSU.SetValue("");
            this.DTP01_APLDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_APLSEQ.SetValue("");
            this.TXT01_APLCONTENTS.SetValue("");
            this.TXT01_APLREPAMOUNT.SetValue("");
            this.CBH01_APLVEND.SetValue("");
            this.TXT01_APLBIGO.SetValue("");
        }
        #endregion
    }
}
