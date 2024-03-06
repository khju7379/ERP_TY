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
    /// UTILITY 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.14 10:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_674AJ535 : UTILITY 단가 조회
    ///  TY_P_UT_674AO538 : UTILITY 단가 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_674AO539 : UTILITY 단가 등록
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  DNYYMM : 년월
    /// </summary>
    public partial class TYUTIL002S : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUTIL002S()
        {
            InitializeComponent();
        }

        private void TYUTIL002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_DNYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_DNYYMM);
        }
        #endregion

        #region Descriptino : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_674AO539.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_674AJ535", this.DTP01_DNYYMM.GetString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_674AO539.SetValue(dt);
        }
        #endregion

        #region Descriptino : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUTIL002I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Descriptino : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_674AO538", dt);
                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);
                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Descriptino : 그리드 더블클릭
        private void FPS91_TY_S_UT_674AO539_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYUTIL002I(this.FPS91_TY_S_UT_674AO539.GetValue("DNYYMM").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_674AO539.GetDataSourceInclude(TSpread.TActionType.Remove, "DNYYMM");

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
    }
}
