using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 품목 중분류코드관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2AV6W969 : 품목 중분류코드 조회
    ///  TY_P_MR_2B24W046 : 품목 중분류코드 등록
    ///  TY_P_MR_2B24W047 : 품목 중분류코드 수정
    ///  TY_P_MR_2B24X048 : 품목 중분류코드 삭제
    ///  TY_P_MR_2B24Y049 : 품목 중분류코드 체크
    ///  TY_P_MR_2B543080 : 품목 소분류코드 존재 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B24P045 : 품목 중분류코드 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_MR_2B68S148 : 중분류코드 3자리를 입력해야 합니다.
    ///  TY_M_MR_2B543081 : 소분류코드가 존재합니다.
    ///  TY_M_MR_2B559095 : 품목 대분류코드를 선택하세요.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  MLMCODE : 대분류 코드
    ///  MMDESC : 중분류명
    /// </summary>
    public partial class TYMRCO003I : TYBase
    {
        #region Description : 페이지 로드
        public TYMRCO003I()
        {
            InitializeComponent();
        }

        private void TYMRCO003I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2B24P045, "MMCODE");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2B24P045, "MMDESC");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.CBO01_MLMCODE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2AV6W969",
                this.CBO01_MLMCODE.GetValue(),
                this.TXT01_MMDESC.GetValue()
                );
            this.FPS91_TY_S_MR_2B24P045.SetValue(this.DbConnector.ExecuteDataTable());

            this.CBO01_MLMCODE.Focus();

            DataTable dt = new DataTable();

            //
            this.FPS91_TY_S_MR_3783P030.Initialize();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "MMHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "MMHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_MR_2B24W046", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_MR_2B24W047", ds.Tables[1]); //수정

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2B24X048", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sFilter = string.Empty;
            double dCOUNT = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2B24P045.GetDataSourceInclude(TSpread.TActionType.New, "MLMCODE", "MMCODE", "MMDESC", "MMBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2B24P045.GetDataSourceInclude(TSpread.TActionType.Update, "MLMCODE", "MMCODE", "MMDESC", "MMBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2B24Y049",
                                       ds.Tables[0].Rows[i]["MLMCODE"].ToString(),
                                       ds.Tables[0].Rows[i]["MMCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (ds.Tables[0].Rows[i]["MMCODE"].ToString().Length != 3)
                    {
                        this.ShowMessage("TY_M_MR_2B68S148");
                        e.Successed = false;
                        return;
                    }
                }

                sFilter = "";
                sFilter = sFilter + " MLMCODE = '" + ds.Tables[0].Rows[i]["MLMCODE"].ToString() + "' AND " ;
                sFilter = sFilter + " MMCODE  = '" + ds.Tables[0].Rows[i]["MMCODE"].ToString()  + "'     " ;

                dCOUNT = Convert.ToDouble(ds.Tables[0].Compute("COUNT(MMCODE)", sFilter).ToString());

                if (dCOUNT > 1)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["MMCODE"].ToString().Length != 3)
                {
                    this.ShowMessage("TY_M_MR_2B68S148");
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
            DataTable dt = this.FPS91_TY_S_MR_2B24P045.GetDataSourceInclude(TSpread.TActionType.Remove, "MLMCODE", "MMCODE");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2B543080",
                                       dt.Rows[i]["MLMCODE"].ToString(),
                                       dt.Rows[i]["MMCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2B543081");
                    e.Successed = false;
                    return;
                }
            }

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

        #region Description : 스프레드 추가 이벤트
        private void FPS91_TY_S_MR_2B24P045_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            if (this.CBO01_MLMCODE.GetValue().ToString() == "")
            {
                // 추가 ROW 삭제
                //this.FPS91_TY_S_MR_2B24P045.ActiveSheet.Rows[e.RowIndex].Remove();

                SetFocus(this.CBO01_MLMCODE);

                this.ShowMessage("TY_M_MR_2B559095");
                return;
            }
            else
            {
                this.FPS91_TY_S_MR_2B24P045.SetValue(e.RowIndex, "MLMCODE", this.CBO01_MLMCODE.GetValue().ToString());
                this.FPS91_TY_S_MR_2B24P045.SetValue(e.RowIndex, "LMDESC", this.CBO01_MLMCODE.GetText().ToString());
            }
        }
        #endregion

        private void FPS91_TY_S_MR_2B24P045_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_3783P029",
                this.FPS91_TY_S_MR_2B24P045.GetValue("MLMCODE").ToString(),
                this.FPS91_TY_S_MR_2B24P045.GetValue("MMCODE").ToString()
                );

            this.FPS91_TY_S_MR_3783P030.SetValue(this.DbConnector.ExecuteDataTable());
        }
    }
}