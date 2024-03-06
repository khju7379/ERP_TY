using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 품목 소분류코드관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.05 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B54K086 : 품목 존재유무 체크
    ///  TY_P_MR_2B54M087 : 품목 소분류코드 등록
    ///  TY_P_MR_2B54M088 : 품목 소분류코드 수정
    ///  TY_P_MR_2B54M089 : 품목 소분류코드 삭제
    ///  TY_P_MR_2B54Q090 : 품목 소분류코드 조회
    ///  TY_P_MR_2B54T092 : 품목 소분류코드 체크
    ///  TY_P_MR_2B24D041 : 품목 중분류조회(콤보박스)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B54W094 : 품목 소분류코드 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_MR_2B68U149 : 소분류코드 3자리를 입력해야 합니다.
    ///  TY_M_MR_2B559095 : 품목 대분류코드를 선택하세요.
    ///  TY_M_MR_2B550096 : 품목 중분류코드를 선택하세요.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_MR_2B544082 : 품목코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  SLMCODE : 대분류 코드
    ///  SMMCODE : 중분류 코드
    ///  SMDESC : 소분류명
    /// </summary>
    public partial class TYMRCO005I : TYBase
    {
        #region Description : 페이지 로드
        public TYMRCO005I()
        {
            InitializeComponent();
        }

        private void TYMRCO005I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2B54W094, "SMCODE");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2B54W094, "SMDESC");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.CBO01_SLMCODE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2B54Q090",
                this.CBO01_SLMCODE.GetValue(),
                this.CBO01_SMMCODE.GetValue(),
                this.TXT01_SMDESC.GetValue()
                );
            this.FPS91_TY_S_MR_2B54W094.SetValue(this.DbConnector.ExecuteDataTable());

            this.CBO01_SLMCODE.Focus();

            //
            this.FPS91_TY_S_MR_3783X032.Initialize();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "SMHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "SMHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_MR_2B54M087", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_MR_2B54M088", ds.Tables[1]); //수정

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
            this.DbConnector.Attach("TY_P_MR_2B54M089", dt);
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
            ds.Tables.Add(this.FPS91_TY_S_MR_2B54W094.GetDataSourceInclude(TSpread.TActionType.New, "SLMCODE", "SMMCODE", "SMCODE", "SMDESC", "SMBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2B54W094.GetDataSourceInclude(TSpread.TActionType.Update, "SLMCODE", "SMMCODE", "SMCODE", "SMDESC", "SMBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 소분류유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2B54T092",
                                       ds.Tables[0].Rows[i]["SLMCODE"].ToString(),
                                       ds.Tables[0].Rows[i]["SMMCODE"].ToString(),
                                       ds.Tables[0].Rows[i]["SMCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (ds.Tables[0].Rows[i]["SMCODE"].ToString().Length != 3)
                    {
                        this.ShowMessage("TY_M_MR_2B68U149");
                        e.Successed = false;
                        return;
                    }
                }

                sFilter = "";
                sFilter = sFilter + " SLMCODE = '" + ds.Tables[0].Rows[i]["SLMCODE"].ToString() + "' AND " ;
                sFilter = sFilter + " SMMCODE = '" + ds.Tables[0].Rows[i]["SMMCODE"].ToString() + "' AND " ;
                sFilter = sFilter + " SMCODE  = '" + ds.Tables[0].Rows[i]["SMCODE"].ToString() + "'      " ;

                dCOUNT = Convert.ToDouble(ds.Tables[0].Compute("COUNT(SMCODE)", sFilter).ToString());

                if (dCOUNT > 1)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["SMCODE"].ToString().Length != 3)
                {
                    this.ShowMessage("TY_M_MR_2B68U149");
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
            DataTable dt = this.FPS91_TY_S_MR_2B54W094.GetDataSourceInclude(TSpread.TActionType.Remove, "SLMCODE", "SMMCODE", "SMCODE");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 품목코드 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2B54K086",
                                       dt.Rows[i]["SLMCODE"].ToString(),
                                       dt.Rows[i]["SMMCODE"].ToString(),
                                       dt.Rows[i]["SMCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2B544082");
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
        private void FPS91_TY_S_MR_2B54W094_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            string sCHECK = string.Empty;

            if (this.CBO01_SLMCODE.GetValue().ToString() == "")
            {
                sCHECK = "CHECK";

                //this.FPS91_TY_S_MR_2B54W094.ActiveSheet.Rows[e.RowIndex].Remove();

                SetFocus(this.CBO01_SLMCODE);

                this.ShowMessage("TY_M_MR_2B559095");
                return;
            }

            if (this.CBO01_SMMCODE.GetValue().ToString() == "")
            {
                sCHECK = "CHECK";

                //this.FPS91_TY_S_MR_2B54W094.ActiveSheet.Rows[e.RowIndex].Remove();

                SetFocus(this.CBO01_SMMCODE);

                this.ShowMessage("TY_M_MR_2B550096");
                return;
            }
            
            if(sCHECK == "")
            {
                this.FPS91_TY_S_MR_2B54W094.SetValue(e.RowIndex, "SLMCODE", this.CBO01_SLMCODE.GetValue().ToString());
                this.FPS91_TY_S_MR_2B54W094.SetValue(e.RowIndex, "LMDESC",  this.CBO01_SLMCODE.GetText().ToString());
                this.FPS91_TY_S_MR_2B54W094.SetValue(e.RowIndex, "SMMCODE", this.CBO01_SMMCODE.GetValue().ToString());
                this.FPS91_TY_S_MR_2B54W094.SetValue(e.RowIndex, "MMDESC",  this.CBO01_SMMCODE.GetText().ToString());
            }
        }
        #endregion

        #region Description : 대분류코드 이벤트
        private void CBO01_SLMCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B24D041",
                this.CBO01_SLMCODE.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_SMMCODE.DataBind(dt, true);
        }
        #endregion

        private void FPS91_TY_S_MR_2B54W094_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sJPCODE = string.Empty;

            sJPCODE = this.FPS91_TY_S_MR_2B54W094.GetValue("SLMCODE").ToString() + this.FPS91_TY_S_MR_2B54W094.GetValue("SMMCODE").ToString() + this.FPS91_TY_S_MR_2B54W094.GetValue("SMCODE").ToString();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_3783X031",
                sJPCODE.ToString()
                );

            this.FPS91_TY_S_MR_3783X032.SetValue(this.DbConnector.ExecuteDataTable());
        }
    }
}
