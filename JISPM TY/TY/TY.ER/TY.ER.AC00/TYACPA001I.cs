using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 경영정보 권한관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.09 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2794C996 : 경영정보 권한관리 조회
    ///  TY_P_AC_27C91006 : 경영정보 권한관리 등록
    ///  TY_P_AC_27C95007 : 경영정보 권한관리 수정
    ///  TY_P_AC_27C96008 : 경영정보 권한관리 삭제
    ///  TY_P_AC_27O6X262 : 경영정보 마감 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27C97009 : 경영정보 권한관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_AC_27O70264 : 이후 월에 마감된 자료가 존재합니다. 마감 구분을 확인하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ECMONTH : 월
    ///  ECYEAR : 년도
    /// </summary>
    public partial class TYACPA001I : TYBase
    {
        #region Description : 페이지 로드
        public TYACPA001I()
        {
            InitializeComponent();
        }

        private void TYACPA001I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27C97009, "ECYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27C97009, "ECMONTH");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.TXT01_ECYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2794C996",
                this.TXT01_ECYEAR.GetValue().ToString(),
                this.TXT01_ECMONTH.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_27C97009.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_27C97009.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sECGUBUN = string.Empty;
            string sECSBBUN = string.Empty;

            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["ECGUBUN"].ToString() == "Y")
                {
                    sECGUBUN = "N";
                }
                else
                {
                    sECGUBUN = "Y";
                }

                // 계열사
                if (ds.Tables[0].Rows[i]["ECSBBUN"].ToString() == "Y")
                {
                    sECSBBUN = "N";
                }
                else
                {
                    sECSBBUN = "Y";
                }
                

                this.DbConnector.Attach("TY_P_AC_27C91006", ds.Tables[0].Rows[i]["ECYEAR"].ToString(),
                                                            Set_Fill2(ds.Tables[0].Rows[i]["ECMONTH"].ToString()),
                                                            ds.Tables[0].Rows[i]["ECGUBUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSBBUN"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[0].Rows[i]["ECYEAR"].ToString(),
                                                            Set_Fill2(ds.Tables[0].Rows[i]["ECMONTH"].ToString())
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["ECGUBUN"].ToString() == "Y")
                {
                    sECGUBUN = "N";
                }
                else
                {
                    sECGUBUN = "Y";
                }

                if (ds.Tables[1].Rows[i]["ECSBBUN"].ToString() == "Y")
                {
                    sECSBBUN = "N";
                }
                else
                {
                    sECSBBUN = "Y";
                }

                this.DbConnector.Attach("TY_P_AC_27C95007", ds.Tables[1].Rows[i]["ECGUBUN"].ToString(),
                                                            ds.Tables[1].Rows[i]["ECSBBUN"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["ECYEAR"].ToString(),
                                                            Set_Fill2(ds.Tables[1].Rows[i]["ECMONTH"].ToString())
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27C96008", dt);

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_27C97009.GetDataSourceInclude(TSpread.TActionType.New, "ECYEAR", "ECMONTH", "ECGUBUN", "ECSBBUN"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27C97009.GetDataSourceInclude(TSpread.TActionType.Update, "ECYEAR", "ECMONTH", "ECGUBUN", "ECSBBUN"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27O6X262",
                    ds.Tables[1].Rows[i]["ECYEAR"].ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(ds.Tables[1].Rows[i]["ECMONTH"].ToString()) < int.Parse(dt.Rows[0]["ECMONTH"].ToString()))
                    {
                        this.ShowMessage("TY_M_AC_27O70264");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_27C97009.GetDataSourceInclude(TSpread.TActionType.Remove, "ECYEAR", "ECMONTH");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27O6X262",
                    dt.Rows[i]["ECYEAR"].ToString()
                    );

                DataTable dtchk = this.DbConnector.ExecuteDataTable();

                if (dtchk.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[i]["ECMONTH"].ToString()) < int.Parse(dtchk.Rows[0]["ECMONTH"].ToString()))
                    {
                        this.ShowMessage("TY_M_AC_27O70264");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion
    }
}