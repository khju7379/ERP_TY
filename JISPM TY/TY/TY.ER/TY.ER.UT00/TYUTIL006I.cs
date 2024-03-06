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
    /// 전기료 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.14 16:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_67EGN751 : 전기료 조회
    ///  TY_P_UT_67EGO752 : 전기료 등록 (단가조회)
    ///  TY_P_UT_67EGT755 : 전기료 확인
    ///  TY_P_UT_67EH0756 : 전기료 등록
    ///  TY_P_UT_67EH1759 : 전기료 삭제
    ///  TY_P_UT_67EH8758 : 전기료 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_67EHP764 : 전기료 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ELHWAJU : 화주
    ///  ELYYMM : 년월
    /// </summary>
    public partial class TYUTIL006I : TYBase
    {
        double fdDNELECT = 0;

        #region Description : 페이지 로드
        public TYUTIL006I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_67EHP764, "ELHWAJU", "VNSANGHO", "ELHWAJU");
        }

        private void TYUTIL006I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_67EHP764, "ELHWAJU","ELAMPRE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.DTP01_ELYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_ELYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sDANGA = string.Empty;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_67EGO752", this.DTP01_ELYYMM.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDANGA = dt.Rows[0]["DNELECT"].ToString();
            }
            else
            {
                this.ShowCustomMessage("단가를 등록한 후 작업하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }

            this.FPS91_TY_S_UT_67EHP764.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_67EGN751", sDANGA,
                                                        this.DTP01_ELYYMM.GetString().Substring(0, 6));

            this.FPS91_TY_S_UT_67EHP764.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67EH1759", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_67EHP764.GetDataSourceInclude(TSpread.TActionType.Remove, "ELYYMM", "ELHWAJU", "ELAMPRE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
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
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_67EH0756", ds.Tables[0].Rows[i]["ELYYMM"].ToString().Substring(0,6),
                                                                ds.Tables[0].Rows[i]["ELHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["ELAMPRE"].ToString(),
                                                                ds.Tables[0].Rows[i]["ELDAY"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );

                    this.DbConnector.ExecuteTranQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_67EH8758", ds.Tables[1].Rows[i]["ELDAY"].ToString(),
                                                                "C",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["ELYYMM"].ToString().Substring(0, 6),
                                                                ds.Tables[1].Rows[i]["ELHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["ELAMPRE"].ToString()
                                                                );

                    this.DbConnector.ExecuteTranQuery();
                }
                this.BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
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
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_67EHP764.GetDataSourceInclude(TSpread.TActionType.New, "ELYYMM", "ELHWAJU", "ELAMPRE", "ELDAY"));
            ds.Tables.Add(this.FPS91_TY_S_UT_67EHP764.GetDataSourceInclude(TSpread.TActionType.Update, "ELYYMM", "ELHWAJU", "ELAMPRE", "ELDAY"));

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67EGO752", this.DTP01_ELYYMM.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fdDNELECT = Convert.ToDouble(dt.Rows[0]["DNELECT"].ToString());
            }
            else
            {
                this.ShowCustomMessage("단가를 등록한 후 작업하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
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

        #region Description : 행 추가
        private void FPS91_TY_S_UT_67EHP764_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_67EHP764.SetValue(e.RowIndex, "ELYYMM", this.DTP01_ELYYMM.GetString());
        }
        #endregion
    }
}
