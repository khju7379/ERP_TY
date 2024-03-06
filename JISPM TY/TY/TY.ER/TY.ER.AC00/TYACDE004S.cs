using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 정기적금불입현황조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.18 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24I1R758 : 정기적금불입현황조회(마스터)
    ///  TY_P_AC_24I1S759 : 정기적금불입현황조회(디테일)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24I1T760 : 정기적금불입현황조회(마스터)
    ///  TY_S_AC_24I26761 : 정기적금불입현황조회(디테일)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GNOAC : 계좌번호
    /// </summary>
    public partial class TYACDE004S : TYBase
    {
        #region Description : 페이지 로드
        public TYACDE004S()
        {
            InitializeComponent();
        }

        private void TYACDE004S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_GNOAC);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24I1R758",
                this.TXT01_GNOAC.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_24I1T760.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 마스터 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_24I1T760_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24I1S759",
                this.FPS91_TY_S_AC_24I1T760.GetValue("E3CDBK").ToString(),
                this.FPS91_TY_S_AC_24I1T760.GetValue("E3NOAC").ToString()
                );

            this.FPS91_TY_S_AC_24I26761.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}