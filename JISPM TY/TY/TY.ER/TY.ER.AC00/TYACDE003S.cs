using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 예금종류별잔액현황조회 프로그램입니다.
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
    ///  TY_P_AC_24IAG734 : 예금종류별잔액현황조회(마스터)
    ///  TY_P_AC_24IAQ745 : 예금종류별잔액현황조회(디테일)
    ///  TY_P_AC_24IAN742 : 예금종류별잔액현황조회(계정코드)
    ///  TY_P_AC_24IAK739 : 예금종류별잔액현황조회(사업장코드)    
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24IAV750 : 예금종류별잔액현황조회(마스터)
    ///  TY_S_AC_24IAX751 : 예금종류별잔액현황조회(디테일)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GCDAC : 계정코드
    ///  GCDDP : 사업장코드
    /// </summary>
    public partial class TYACDE003S : TYBase
    {
        #region Description : 페이지 로드
        public TYACDE003S()
        {
            InitializeComponent();
        }

        private void TYACDE003S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBO01_GCDDP);

            // 마스터 스프레드 조회
            this.BTN61_INQ_Click(null, null);

            // 디테일 스프레드 조회
            this.FPS91_TY_S_AC_24IAV750_CellDoubleClick(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24IAG734",
                this.CBO01_GCDDP.GetValue().ToString(),
                this.CBO01_GCDAC.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_24IAV750.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 사업자 코드에 따른 계정코드 가져오기
        private void CBO01_GCDDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24IAN742",
                this.CBO01_GCDDP.GetValue().ToString()
                );

            // 콤보박스에 바인드
            this.CBO01_GCDAC.DataBind(this.DbConnector.ExecuteDataTable());

            // 마스터 스프레드 조회
            this.BTN61_INQ_Click(null, null);

            // 디테일 스프레드 조회
            this.FPS91_TY_S_AC_24IAV750_CellDoubleClick(null, null);
        }
        #endregion

        #region Description : 계정코드 변경시 마스터 및 디테일 조회
        private void CBO01_GCDAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 마스터 스프레드 조회
            this.BTN61_INQ_Click(null, null);

            // 디테일 스프레드 조회
            this.FPS91_TY_S_AC_24IAV750_CellDoubleClick(null, null);
        }
        #endregion

        #region Description : 마스터 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_24IAV750_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24IAQ745",
                this.FPS91_TY_S_AC_24IAV750.GetValue("E3CDDP").ToString(),
                this.FPS91_TY_S_AC_24IAV750.GetValue("E3CDAC").ToString()
                );

            this.FPS91_TY_S_AC_24IAX751.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}