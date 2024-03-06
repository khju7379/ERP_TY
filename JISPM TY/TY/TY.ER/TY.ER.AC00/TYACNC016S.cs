using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 프로젝트 등록관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.04 17:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2945F757 : 프로젝트 관리 조회
    ///  TY_P_AC_2945G758 : 프로젝트관리 입력
    ///  TY_P_AC_2945H759 : 프로젝트관리 수정
    ///  TY_P_AC_2945H760 : 프로젝트관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2945J761 : 프로젝트관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  AJYDPAC : 귀속부서
    ///  AJYPJGB : 프로젝트구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC016S : TYBase
    {
        #region Description : 폼로드 이벤트
        public TYACNC016S()
        {
            InitializeComponent();
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2945J761, "AJYPJGB", "AJYPJGBNM", "AJYPJGB");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2945J761, "AJYDPAC", "AJYDPACNM", "AJYDPAC");
        }

        private void TYACNC016S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2945J761, "AJYDTAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2945J761, "AJYPJGB");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2945J761, "AJYDPAC");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2945J761, "AJYDTAC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_AJYDPAC.DummyValue = this.DTP01_GSTYYMM.GetValue() + "01"; 
            
            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.BTN61_INQ_Click(null, null);  

        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2945F757", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_2945J761.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2945H760", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2945G758", ds.Tables[0]); // 저장
            this.DbConnector.Attach("TY_P_AC_2945H759", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);  
            
            this.ShowMessage("TY_M_GB_23NAD873");            
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_2945J761.GetDataSourceInclude(TSpread.TActionType.New, "AJYDTAC", "AJYPJGB", "AJYDPAC", "AJYINAMT"));
            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_2945J761.GetDataSourceInclude(TSpread.TActionType.Update, "AJYDTAC", "AJYPJGB", "AJYDPAC", "AJYINAMT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2947Z781", ds.Tables[0].Rows[i]["AJYDTAC"].ToString(), ds.Tables[0].Rows[i]["AJYPJGB"].ToString(), ds.Tables[0].Rows[i]["AJYDPAC"].ToString()); // 저장
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_28D5W379");
                    e.Successed = false;
                    return;
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
            DataTable dt = this.FPS91_TY_S_AC_2945J761.GetDataSourceInclude(TSpread.TActionType.Remove, "AJYDTAC", "AJYPJGB", "AJYDPAC");

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

        #region Description : DTP01_GSTYYMM_ValueChanged 이벤트
        private void DTP01_GSTYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_AJYDPAC.DummyValue = this.DTP01_GSTYYMM.GetValue() + "01";
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2945J761_EnterCell 이벤트
        private void FPS91_TY_S_AC_2945J761_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 3)
                return;

            string year = FPS91_TY_S_AC_2945J761.GetValue(e.Row, "AJYDTAC").ToString() + "01";
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2945J761, "AJYDPAC");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
            return;
        }
        #endregion
    }
}
