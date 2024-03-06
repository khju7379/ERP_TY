using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 코드관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.08.19 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_48JEG506 : 원천세 코드관리 CODE 조회
    ///  TY_P_AC_48JEG507 : 원천세 코드관리 INDEX 조회
    ///  TY_P_AC_48JEG508 : 원천세 코드관리 등록
    ///  TY_P_AC_48JEG509 : 원천세 코드관리 삭제
    ///  TY_P_AC_48JEG510 : 원천세 코드관리 수정
    ///  TY_P_AC_48JEH511 : 원천세 코드관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_48JEH513 : 원천세 코드관리 CODE 조회
    ///  TY_S_AC_48JEH514 : 원천세 코드관리 INDEX 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_26B9D824 : 인덱스를 확인하세요.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  CDBIGO : 비고
    ///  CDCODE : CODE
    ///  CDDESC1 : 내용1
    ///  CDDESC2 : 내용2
    ///  CDHIDAT : 작성일자
    ///  CDHIGB : 작성구분
    ///  CDHISAB : 작성사번
    ///  CDHITIM : 작성시간
    ///  CDINDEX : INDEX
    /// </summary>
    public partial class TYACTP001I : TYBase
    {
        //private TYData DAT01_CDHISAB;

        private string fsCDINDEX;

        #region Description : Page Load()
        public TYACTP001I()
        {
            InitializeComponent();
        }

        private void TYACTP001I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_48JEH513, "CDINDEX");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_48JEH513, "CDCODE");

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

            this.DbConnector.Attach("TY_P_AC_48JEG507");
            this.FPS91_TY_S_AC_48JEH514.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "CDHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "CDHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_48JEG508", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_AC_48JEG510", ds.Tables[1]); //수정
            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.FPS91_TY_S_AC_48JEH514_CellDoubleClick(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_48JEG509", dt);
            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.FPS91_TY_S_AC_48JEH514_CellDoubleClick(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_48JEH513.GetDataSourceInclude(TSpread.TActionType.New, "CDINDEX", "CDCODE", "CDDESC1", "CDDESC2", "CDBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_48JEH513.GetDataSourceInclude(TSpread.TActionType.Update, "CDINDEX", "CDCODE", "CDDESC1", "CDDESC2", "CDBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_48JEH511",
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

            DataTable dt = this.FPS91_TY_S_AC_48JEH513.GetDataSourceInclude(TSpread.TActionType.Remove, "CDINDEX", "CDCODE");

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
        private void FPS91_TY_S_AC_48JEH514_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_48JEG506", this.FPS91_TY_S_AC_48JEH514.GetValue("CDCODE").ToString());

            fsCDINDEX = this.FPS91_TY_S_AC_48JEH514.GetValue("CDCODE").ToString();

            this.FPS91_TY_S_AC_48JEH513.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 스프레드 추가시 INDEX 자동 추가
        private void FPS91_TY_S_AC_48JEH513_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_48JEH513.SetValue(e.RowIndex, "CDINDEX", fsCDINDEX);
        }
        #endregion
    }
}