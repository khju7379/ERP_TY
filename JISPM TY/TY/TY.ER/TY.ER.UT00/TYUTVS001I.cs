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
    /// 선박 코드관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.08 15:28
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_668DE089 : 코드관리 등록
    ///  TY_P_UT_668DG090 : 코드관리 수정
    ///  TY_P_UT_668DI091 : 코드관리 삭제
    ///  TY_P_UT_668DS093 : 코드관리 체크
    ///  TY_P_UT_6B8FN671 : 선박 코드관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6B8FK670 : 선박 코드관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  CDCODE : CODE
    /// </summary>
    public partial class TYUTVS001I : TYBase
    {
        #region Description : 페이지 로드
        public TYUTVS001I()
        {
            InitializeComponent();
        }

        private void TYUTVS001I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_6B8FK670, "CDCODE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_CDCODE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6B8FN671", "VS", this.TXT01_CDCODE.GetValue().ToString(), this.TXT01_CDDESC1.GetValue().ToString());
            this.FPS91_TY_S_UT_6B8FK670.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_668DI091", dt);
            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt1 = new DataTable();

            DataTable dt = this.FPS91_TY_S_UT_6B8FK670.GetDataSourceInclude(TSpread.TActionType.Remove, "CDINDEX", "CDCODE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["CDINDEX"].ToString() == "VS") // 본선
                    {
                        // 코드관리 삭제시 선박입항관리 자료 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_66SFB439",
                            dt.Rows[i]["CDCODE"].ToString()
                            );

                        dt1 = this.DbConnector.ExecuteDataTable();

                        if (dt1.Rows.Count > 0)
                        {
                            this.ShowMessage("TY_M_UT_66SFC440");

                            e.Successed = false;
                            return;
                        }
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

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_668DE089", ds.Tables[0].Rows[i][0].ToString(),
                                                                ds.Tables[0].Rows[i][1].ToString(),
                                                                ds.Tables[0].Rows[i][2].ToString(),
                                                                ds.Tables[0].Rows[i][3].ToString(),
                                                                ds.Tables[0].Rows[i][4].ToString(),
                                                                ds.Tables[0].Rows[i][5].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); //저장
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_668DG090", ds.Tables[1].Rows[i][2].ToString(),
                                                                ds.Tables[1].Rows[i][3].ToString(),
                                                                ds.Tables[1].Rows[i][4].ToString(),
                                                                ds.Tables[1].Rows[i][5].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i][0].ToString(),
                                                                ds.Tables[1].Rows[i][1].ToString()
                                                                ); //수정
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            // CODE 조회
            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_6B8FK670.GetDataSourceInclude(TSpread.TActionType.New, "CDINDEX", "CDCODE", "CDDESC1", "CDDESC2", "CDDESC3", "CDBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_6B8FK670.GetDataSourceInclude(TSpread.TActionType.Update, "CDINDEX", "CDCODE", "CDDESC1", "CDDESC2", "CDDESC3", "CDBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       ds.Tables[0].Rows[i]["CDINDEX"].ToString(),
                                       ds.Tables[0].Rows[i]["CDCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 스프레드 추가시 INDEX 자동 추가
        private void FPS91_TY_S_UT_6B8FK670_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_6B8FK670.SetValue(e.RowIndex, "CDINDEX", "VS");
        }
        #endregion

        #region Description : 선박코드 체크
        private void FPS91_TY_S_UT_6B8FK670_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            //string sCDCODE = this.FPS91_TY_S_UT_6B8FK670.GetValue("CDCODE").ToString();

            //if (sCDCODE.Length == 3)
            //{
            //    this.DbConnector.CommandClear();

            //    this.DbConnector.Attach("TY_P_UT_668DS093","VS", sCDCODE);

            //    DataTable dt = this.DbConnector.ExecuteDataTable();

            //    if (dt.Rows.Count > 0)
            //    {
            //        this.ShowCustomMessage("이미 등록된 코드입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        this.FPS91_TY_S_UT_6B8FK670.SetValue("CDCODE", "");
            //    }
            //}
        }
        #endregion
    }
}
