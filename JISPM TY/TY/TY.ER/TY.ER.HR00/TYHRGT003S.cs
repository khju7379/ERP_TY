using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 휴무관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.26 14:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BQF4543 : 휴무관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4BQFC544 : 휴무관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  GHCODE : 휴무코드
    ///  GHSABUN : 사   번
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    ///  KBBUSEO : 부서
    /// </summary>
    public partial class TYHRGT003S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRGT003S()
        {
            InitializeComponent();
        }

        private void TYHRGT003S_Load(object sender, System.EventArgs e)
        {
            BTN62_NEW.Text = "일괄등록";

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_KBBUSEO.DummyValue = this.DTP01_SDATE.GetString().ToString();

            this.SetStartingFocus(this.DTP01_SDATE);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            this.FPS91_TY_S_HR_4BQFC544.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BQF4543", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.CBH01_GHSABUN.GetValue(), this.CBH01_GHCODE.GetValue(), this.CBH01_KBBUSEO.GetValue());
            this.FPS91_TY_S_HR_4BQFC544.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BQFH545", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BQFC544.GetDataSourceInclude(TSpread.TActionType.Remove, "GHSABUN", "GHDATE", "GHCODE", "GHSEQ");

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

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRGT003I(string.Empty, string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_4BQFC544_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRGT003I(this.FPS91_TY_S_HR_4BQFC544.GetValue("GHSABUN").ToString(), this.FPS91_TY_S_HR_4BQFC544.GetValue("GHDATE").ToString(), this.FPS91_TY_S_HR_4BQFC544.GetValue("GHCODE").ToString(), this.FPS91_TY_S_HR_4BQFC544.GetValue("GHSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : DTP01_SDATE_ValueChanged 이벤트
        private void DTP01_SDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = this.DTP01_SDATE.GetString().ToString();
        }
        #endregion

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.DTP01_SDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion

        #region  Description : 일괄등록 버튼 이벤트
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRGT003B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
