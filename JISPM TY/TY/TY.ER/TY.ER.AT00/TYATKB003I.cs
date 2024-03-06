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
    /// 사택월별 요금 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.31 13:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88VEE670 : 사택월별 요금관리 마스타 등록
    ///  TY_P_HR_88VEG671 : 사택월별 요금관리 마스타 수정
    ///  TY_P_HR_88VEH672 : 사택월별 요금관리 마스타 삭제
    ///  TY_P_HR_88VEK673 : 사택월별 요금관리 내역 등록
    ///  TY_P_HR_88VEL674 : 사택월별 요금관리 내역 수정
    ///  TY_P_HR_88VEN675 : 사택월별 요금관리 내역 삭제
    ///  TY_P_HR_88VEP676 : 사택월별 요금관리 내역 조회
    ///  TY_P_HR_88VEQ677 : 사택월별 요금관리 마스타 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_893DJ681 : 사택월별 요금 내역사항 조회
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
    ///  APMYYMM : 년월
    ///  APMELECAMT : 공동전기사용금액
    ///  APMGASDANGA : 가스료 단가
    ///  APMHADANGA : 하수도료 단가
    ///  APMHOUSECNT : 세대수
    ///  APMOMDANGA : 오물수거비 단가
    ///  APMSADANGA : 상수도료 단가
    ///  APMSPCOLDANGA : 분리수거비 단가
    /// </summary>
    public partial class TYATKB003I : TYBase
    {
        private string fsAPMYYMM = string.Empty;

        #region Description : 폼 로드
        public TYATKB003I(string sAPMYYMM)
        {
            InitializeComponent();

            fsAPMYYMM = sAPMYYMM;
        }

        private void TYATKB003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_APMHOUSECNT.SetReadOnly(true);

            if (string.IsNullOrEmpty(fsAPMYYMM))
            {
                UP_FiledLock(false);
                UP_FieldClear();
                SetStartingFocus(this.DTP01_APMYYMM);
            }
            else
            {
                UP_FiledLock(true);
                UP_Run();
                SetStartingFocus(this.TXT01_APMELECAMT);
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
            try
            {
                // 마스타 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_88VEH672", this.DTP01_APMYYMM.GetString().Substring(0, 6));
                this.DbConnector.ExecuteTranQuery();

                // 내역 삭제 TY_P_HR_88VEN675
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_88VEN675", this.DTP01_APMYYMM.GetString().Substring(0, 6));
                this.DbConnector.ExecuteTranQuery();

                UP_FieldClear();
                UP_FiledLock(false);

                fsAPMYYMM = "";
                this.DTP01_APMYYMM.Focus();

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
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
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                this.DbConnector.CommandClear();

                // 마스타 신규등록
                if (string.IsNullOrEmpty(fsAPMYYMM))
                {
                    string sAPMHOUSECNT = string.Empty;
                    // 총 세대수 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_8AIBN981");
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    sAPMHOUSECNT = dt.Rows[0]["CNT"].ToString();

                    this.DbConnector.Attach("TY_P_HR_88VEE670", this.DTP01_APMYYMM.GetString().Substring(0, 6),
                                                                this.TXT01_APMELECAMT.GetValue().ToString(),        // 공동전기
                                                                this.TXT01_APMHADANGA.GetValue().ToString(),        // 하수도
                                                                this.TXT01_APMSADANGA.GetValue().ToString(),        // 상수도
                                                                this.TXT01_APMGASDANGA.GetValue().ToString(),       // 가스
                                                                this.TXT01_APMOMDANGA.GetValue().ToString(),        // 오물수거
                                                                this.TXT01_APMSPCOLDANGA.GetValue().ToString(),     // 분리수거
                                                                sAPMHOUSECNT,       // 세대 수
                                                                TYUserInfo.EmpNo
                                                                );
                }
                // 마스타 수정
                else
                {
                    this.DbConnector.Attach("TY_P_HR_88VEG671", this.TXT01_APMELECAMT.GetValue().ToString(),        // 공동전기
                                                                this.TXT01_APMHADANGA.GetValue().ToString(),        // 하수도
                                                                this.TXT01_APMSADANGA.GetValue().ToString(),        // 상수도
                                                                this.TXT01_APMGASDANGA.GetValue().ToString(),       // 가스
                                                                this.TXT01_APMOMDANGA.GetValue().ToString(),        // 오물수거
                                                                this.TXT01_APMSPCOLDANGA.GetValue().ToString(),     // 분리수거
                                                                this.TXT01_APMHOUSECNT.GetValue().ToString(),       // 세대 수
                                                                TYUserInfo.EmpNo,
                                                                this.DTP01_APMYYMM.GetString().Substring(0, 6)
                                                                );
                }

                this.DbConnector.ExecuteTranQuery();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    // 내역 신규등록
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_HR_88VEK673", this.DTP01_APMYYMM.GetString().Substring(0, 6),
                                                                    ds.Tables[0].Rows[i]["APSCODE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["APSCODEAMT"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    // 내역 수정
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_HR_88VEL674", ds.Tables[1].Rows[i]["APSCODEAMT"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["APSYYMM"].ToString(),
                                                                    ds.Tables[1].Rows[i]["APSCODE"].ToString()
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_GB_23NAD873");
                fsAPMYYMM = this.DTP01_APMYYMM.GetString().Substring(0, 6);
                UP_Run();
                UP_FiledLock(true);
                this.TXT01_APMELECAMT.Focus();
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
            DataTable dt = new DataTable();

            if (string.IsNullOrEmpty(fsAPMYYMM))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_88VEQ677", this.DTP01_APMYYMM.GetString().Substring(0,6));
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 자료입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_893DJ681.GetDataSourceInclude(TSpread.TActionType.New, "APSYYMM", "APSCODE", "APSCODEAMT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_893DJ681.GetDataSourceInclude(TSpread.TActionType.Update, "APSYYMM", "APSCODE", "APSCODEAMT"));
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88VEQ677", this.DTP01_APMYYMM.GetString().Substring(0,6));
            dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.CurrentDataTableRowMapping(dt, "01");
            //}

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 확인 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88VEQ677", fsAPMYYMM);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }

            // 내역 조회
            this.FPS91_TY_S_HR_893DJ681.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_88VEP676", fsAPMYYMM);

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_893DJ681.SetValue(dt);
            
        }
        #endregion

        #region Description : key 잠금
        private void UP_FiledLock(bool b)
        {
            if (b == true)
            {   
                this.DTP01_APMYYMM.SetReadOnly(true);
                this.BTN61_REM.Visible = true;
            }
            else
            {   
                this.DTP01_APMYYMM.SetReadOnly(false);
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 화면 초기화
        private void UP_FieldClear()
        {
            this.DTP01_APMYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.TXT01_APMELECAMT.SetValue("");     // 공동전기
            this.TXT01_APMHADANGA.SetValue("");     // 하수도
            this.TXT01_APMSADANGA.SetValue("");     // 상수도
            this.TXT01_APMGASDANGA.SetValue("");    // 가스
            this.TXT01_APMOMDANGA.SetValue("");     // 오물수거
            this.TXT01_APMSPCOLDANGA.SetValue("");  // 분리수거

            this.TXT01_APMHOUSECNT.SetValue("");

            this.FPS91_TY_S_HR_893DJ681.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_893GA685");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_893DJ681.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_HR_893DJ681.CurrentRowCount; i++)
            {
                this.FPS91_TY_S_HR_893DJ681.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
            }
        }
        #endregion
    }
}
