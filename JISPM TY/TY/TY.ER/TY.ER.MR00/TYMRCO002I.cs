using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 대분류코드관리 프로그램입니다.
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
    ///  TY_P_MR_2B22Y030 : 대분류코드 조회
    ///  TY_P_MR_2B22Z031 : 대분류코드 등록
    ///  TY_P_MR_2B23C034 : 대분류코드 삭제
    ///  TY_P_MR_2B23H037 : 대분류코드 수정
    ///  TY_P_MR_2B240039 : 대분류코드 체크
    ///  TY_P_MR_2B53Y077 : 품목 중분류코드 존재 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B23C035 : 대분류코드 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_MR_2B53Z079 : 품목 중분류코드가 존재합니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  LMDESC : 대분류명
    /// </summary>
    public partial class TYMRCO002I : TYBase
    {
        //private TYData DAT01_LMHISAB;

        #region Description : 페이지 로드
        public TYMRCO002I()
        {
            InitializeComponent();
        }

        private void TYMRCO002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2B23C035, "LMCODE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_LMDESC);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2B22Y030",
                this.TXT01_LMDESC.GetValue()
                );
            this.FPS91_TY_S_MR_2B23C035.SetValue(this.DbConnector.ExecuteDataTable());

            //
            this.FPS91_TY_S_MR_3783H028.Initialize();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "LMHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "LMHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2B22Z031", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_MR_2B23H037", ds.Tables[1]); //수정
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
            this.DbConnector.Attach("TY_P_MR_2B23C034", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sFilter = string.Empty;
            double dCOUNT  = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2B23C035.GetDataSourceInclude(TSpread.TActionType.New, "LMCODE", "LMDESC", "LMBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2B23C035.GetDataSourceInclude(TSpread.TActionType.Update, "LMCODE", "LMDESC", "LMBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2B240039",
                                       ds.Tables[0].Rows[i]["LMCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }

                sFilter = "";
                sFilter = ds.Tables[0].Rows[i]["LMCODE"].ToString();
                sFilter = " LMCODE = '" + sFilter.ToString() + "' " ;

                dCOUNT = Convert.ToDouble(ds.Tables[0].Compute("COUNT(LMCODE)", sFilter).ToString());

                if (dCOUNT > 1)
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_MR_2B23C035.GetDataSourceInclude(TSpread.TActionType.Remove, "LMCODE");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2B53Y077",
                                       dt.Rows[i]["LMCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2B53Z079");
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

        private void FPS91_TY_S_MR_2B23C035_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_3783G027",
                this.FPS91_TY_S_MR_2B23C035.GetValue("LMCODE").ToString()
                );

            this.FPS91_TY_S_MR_3783H028.SetValue(this.DbConnector.ExecuteDataTable());
        }
    }
}