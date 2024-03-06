using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 투하자금관리 내역 조회(자금용) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.03.13 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_33D8U291 : 투하자금관리 사업부별 조회(자금용)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_33D8W292 : 투하자금관리 사업부별 조회(자금용)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC034S : TYBase
    {
        public TYACNC034S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACNC034S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 조회 버튼 
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_33D8W292.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_33D8U291", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_33D8W292.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_AC_33D8W292.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_33D8W292, "AJDCDACNM", "합 계", SumRowType.Total);
            }
        } 
        #endregion
    }
}
