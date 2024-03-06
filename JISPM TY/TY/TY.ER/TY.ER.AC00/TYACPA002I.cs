using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
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
    ///  TY_P_AC_27CBP015 : 경영정보 코드관리 INDEX 조회
    ///  TY_P_AC_27CBP014 : 경영정보 코드관리 CODE 조회
    ///  TY_P_AC_27CBQ016 : 경영정보 코드관리 등록
    ///  TY_P_AC_27CBS018 : 경영정보 코드관리 수정
    ///  TY_P_AC_27CBR017 : 경영정보 코드관리 삭제
    ///  TY_P_AC_27CBV019 : 경영정보 코드관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27CBW021 : 코드관리 INDEX조회
    ///  TY_S_AC_27CBW020 : 코드관리 CODE 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26B9D824 : 인덱스를 확인하세요.
    ///  
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    /// </summary>
    public partial class TYACPA002I : TYBase
    {
        private TYData DAT01_EDHISAB;

        private string fsEDINDEX;

        #region Description : Page Load()
        public TYACPA002I()
        {
            InitializeComponent();
        }

        private void TYACPA002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27CBW020, "EDINDEX");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27CBW020, "EDCODE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27CBP015");
            this.FPS91_TY_S_AC_27CBW021.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_27CBW021.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "EDHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "EDHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();
            
            this.DbConnector.Attach("TY_P_AC_27CBQ016", ds.Tables[0]); // 저장
            this.DbConnector.Attach("TY_P_AC_27CBS018", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.FPS91_TY_S_AC_27CBW021_CellDoubleClick(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27CBR017", dt);
            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.FPS91_TY_S_AC_27CBW021_CellDoubleClick(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // CODE 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27CBW020.GetDataSourceInclude(TSpread.TActionType.New, "EDINDEX", "EDCODE", "EDDESC1", "EDDESC2", "EDBIGO"));
            // CODE 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27CBW020.GetDataSourceInclude(TSpread.TActionType.Update, "EDINDEX", "EDCODE", "EDDESC1", "EDDESC2", "EDBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_27CBV019",
                                       ds.Tables[0].Rows[i]["EDINDEX"].ToString(),
                                       ds.Tables[0].Rows[i]["EDCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }

                if (fsEDINDEX.ToString() != ds.Tables[0].Rows[i]["EDINDEX"].ToString())
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
            DataTable dt = this.FPS91_TY_S_AC_27CBW020.GetDataSourceInclude(TSpread.TActionType.Remove, "EDINDEX", "EDCODE");

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

        #region Description : INDEX 스프레드 더블 클릭 이벤트
        private void FPS91_TY_S_AC_27CBW021_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27CBP014", this.FPS91_TY_S_AC_27CBW021.GetValue("EDCODE").ToString());

            fsEDINDEX = this.FPS91_TY_S_AC_27CBW021.GetValue("EDCODE").ToString();

            this.FPS91_TY_S_AC_27CBW020.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_27CBW020.Focus();
        }
        #endregion

        #region Description : 스프레드 추가시 INDEX 자동 추가
        private void FPS91_TY_S_AC_27CBW020_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_27CBW020.SetValue(e.RowIndex, "EDINDEX", fsEDINDEX);
        }
        #endregion
    }
}