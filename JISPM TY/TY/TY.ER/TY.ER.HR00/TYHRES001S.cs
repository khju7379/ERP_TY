using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;


namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 정년현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.22 13:44
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28M21486 : EIS 정년현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_28M2B487 : EIS 정년현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  EJSAUP : 사업부
    ///  EJYYMM : 기준년월
    ///  EJNAME : 성명
    /// </summary>
    public partial class TYHRES001S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYHRES001S()
        {
            InitializeComponent();
        }

        private void TYHRES001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_EJYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_EJSAUP.DummyValue = this.DTP01_EJYYMM.GetValue(); 

            this.SetStartingFocus(DTP01_EJYYMM);
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRES001B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_28M21486", this.ControlFactory, "01");

            this.FPS91_TY_S_HR_28M2B487.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28M5M501", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_HR_28M2B487.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_HR_28M2B487.GetDataSourceInclude(TSpread.TActionType.Remove, "EJYYMM","EJSAUP","EJSABUN");

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

        #region Description : DTP01_EJYYMM_ValueChanged 이벤트
        private void DTP01_EJYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_EJSAUP.DummyValue = this.DTP01_EJYYMM.GetValue();
        }
        #endregion
    }
}
