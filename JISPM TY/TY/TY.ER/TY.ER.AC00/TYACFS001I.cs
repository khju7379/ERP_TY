using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 채권회수기준일수관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.17 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H4Y053 : 채권회수기준일관리 조회
    ///  TY_P_AC_27H51054 : 채권회수기준일관리 등록
    ///  TY_P_AC_27H53055 : 채권회수기준일관리 수정
    ///  TY_P_AC_27H54056 : 채권회수기준일관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27H55058 : 채권회수기준일관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
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
    ///  BOSAUP : 사업부
    ///  BOVEND : 거래처
    ///  BOYYMM : 년월
    /// </summary>
    public partial class TYACFS001I : TYBase
    {

        #region Description : 페이지 로드 이벤트
        public TYACFS001I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27H55058, "BOSAUP", "BOSAUPNM", "BOSAUP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27H55058, "BOVEND", "BOVENDNM", "BOVEND");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27H55058, "BOCDAC", "BOCDACNM", "BOCDAC");

        }

        private void TYACFS001I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27H55058, "BOYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27H55058, "BOSAUP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27H55058, "BOSAUPNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27H55058, "BOVEND");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27H55058, "BOVENDNM");

            this.BTN51_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.DTP01_BOYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));

            this.CBH01_BOSAUP.DummyValue = this.DTP01_BOYYMM.GetValue()+"30";

            SetStartingFocus(DTP01_BOYYMM);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 조히 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27H4Y053", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_27H55058.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27H54056", dt);

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "BO1HISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "BO1HISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();            

            this.DbConnector.Attach("TY_P_AC_27H51054", ds.Tables[0]);
            this.DbConnector.Attach("TY_P_AC_27H53055", ds.Tables[1]);           
           
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);            
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_27H55058.GetDataSourceInclude(TSpread.TActionType.New, "BOYYMM", "BOSAUP", "BOVEND", "BOCDAC", "BOSTDAY"));

            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_27H55058.GetDataSourceInclude(TSpread.TActionType.Update, "BOYYMM", "BOSAUP", "BOVEND", "BOCDAC", "BOSTDAY"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["BOCDAC"].ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_27IB3074", ds.Tables[0].Rows[i]["BOYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BOSAUP"].ToString(),
                                                                ds.Tables[0].Rows[i]["BOVEND"].ToString());
                    iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iRowCnt <= 0)
                    {
                        this.ShowMessage("TY_M_AC_27IBC075");
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
            DataTable dt = this.FPS91_TY_S_AC_27H55058.GetDataSourceInclude(TSpread.TActionType.Remove, "BOYYMM", "BOSAUP", "BOVEND");

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

        #region Description : FPS91_TY_S_AC_27H55058_EnterCell 이벤트
        private void FPS91_TY_S_AC_27H55058_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            string year = FPS91_TY_S_AC_27H55058.GetValue(e.Row, "BOYYMM").ToString() + "30";
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_27H55058, "BOSAUP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_27H55058_RowInserted 이벤트
        private void FPS91_TY_S_AC_27H55058_RowInserted(object sender, TSpread.TAlterEventRow e)
        {

            this.FPS91_TY_S_AC_27H55058.SetValue(e.RowIndex, "BOYYMM", DateTime.Now.ToString("yyyy-MM"));

        }
        #endregion

        #region Description : DTP01_BOYYMM_ValueChanged 이벤트
        private void DTP01_BOYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_BOSAUP.DummyValue = this.DTP01_BOYYMM.GetValue()+"30";
        }
        #endregion

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS001B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
