using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.29 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_B1LDE411 : 코드관리 INDEX 조회
    ///  TY_P_UT_B1LDD410 : 코드관리 CODE 조회
    ///  TY_P_MR_2B21D026 : 코드관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_B1LDJ417 : 코드관리 INDEX 조회
    ///  TY_S_UT_B1LDJ418 : 코드관리 CODE  조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26B9D824 : 인덱스를 확인하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    /// </summary>
    public partial class TYUTPS019I : TYBase
    {
        private string fsCDINDEX;

        #region Description : Page Load()
        public TYUTPS019I()
        {
            InitializeComponent();
        }

        private void TYUTPS019I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B1LDJ418, "CDINDEX");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B1LDJ418, "CDCODE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            fsCDINDEX = "";

            this.BTN61_INQ_FXM_Click(null, null);
        }
        #endregion

        #region Description : INDEX 조회 버튼
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B1LDJ417.Initialize();
            this.FPS91_TY_S_UT_B1LDJ418.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_B1LDE411");
            this.FPS91_TY_S_UT_B1LDJ417.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (fsCDINDEX.ToString().Trim() == "")
            {
                fsCDINDEX = "00";
            }

            this.FPS91_TY_S_UT_B1LDJ418.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_B1LDD410", fsCDINDEX,
                                                        this.TXT01_CDCODE.GetValue().ToString(),
                                                        this.TXT01_CDDESC1.GetValue().ToString());

            this.FPS91_TY_S_UT_B1LDJ418.SetValue(this.DbConnector.ExecuteDataTable());
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
                    this.DbConnector.Attach("TY_P_UT_B1LDF412", ds.Tables[0].Rows[i][0].ToString(),
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
                    this.DbConnector.Attach("TY_P_UT_B1LDH415", ds.Tables[1].Rows[i][2].ToString(),
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
            this.FPS91_TY_S_UT_B1LDJ417_CellDoubleClick(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B1LDG413", dt);
            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.FPS91_TY_S_UT_B1LDJ417_CellDoubleClick(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_B1LDJ418.GetDataSourceInclude(TSpread.TActionType.New,    "CDINDEX", "CDCODE", "CDDESC1", "CDDESC2", "CDDESC3", "CDBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_B1LDJ418.GetDataSourceInclude(TSpread.TActionType.Update, "CDINDEX", "CDCODE", "CDDESC1", "CDDESC2", "CDDESC3", "CDBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_B1LDH416",
                                       ds.Tables[0].Rows[i]["CDINDEX"].ToString(),
                                       ds.Tables[0].Rows[i]["CDCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }

                if (fsCDINDEX.ToString() != ds.Tables[0].Rows[i]["CDINDEX"].ToString())
                {
                    this.ShowMessage("TY_M_AC_26B9D824");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt1 = new DataTable();

            DataTable dt = this.FPS91_TY_S_UT_B1LDJ418.GetDataSourceInclude(TSpread.TActionType.Remove, "CDINDEX", "CDCODE");

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
                    if (dt.Rows[i]["CDINDEX"].ToString() == "00")
                    {
                        // 하위 코드관리 존재 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_B1LDG414",
                            dt.Rows[i]["CDCODE"].ToString()
                            );

                        dt1 = this.DbConnector.ExecuteDataTable();

                        if (dt1.Rows.Count > 0)
                        {
                            this.ShowMessage("TY_M_UT_66SF4438");

                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
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

        #region Description : INDEX 스프레드 더블 클릭 이벤트
        private void FPS91_TY_S_UT_B1LDJ417_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_UT_B1LDJ418.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_B1LDD410", this.FPS91_TY_S_UT_B1LDJ417.GetValue("CDCODE").ToString(),"","");

            fsCDINDEX = this.FPS91_TY_S_UT_B1LDJ417.GetValue("CDCODE").ToString();

            this.FPS91_TY_S_UT_B1LDJ418.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 스프레드 추가시 INDEX 자동 추가
        private void FPS91_TY_S_UT_B1LDJ418_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_B1LDJ418.SetValue(e.RowIndex, "CDINDEX", fsCDINDEX);
        }
        #endregion

        #region Description : 엔터키 이벤트(포커스 이동)
        private void TXT01_CDDESC1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion
    }
}