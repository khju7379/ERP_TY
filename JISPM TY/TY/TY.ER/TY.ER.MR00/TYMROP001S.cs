using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매발주 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.19 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2BQBW631 : 구매입고 마스터 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2BQBY632 : 구매발주 마스터 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BC51262 : 결재 완료 된 문서가 아닙니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  RRM1000 : 사업부
    ///  RRM1010 : 발주구분
    ///  RRM1020 : 년월
    ///  POM1180 : 공사및구매명
    /// </summary>
    public partial class TYMROP001S : TYBase
    {
        private string fsPOM1000 = string.Empty;
        private string fsPOM1010 = string.Empty;
        private string fsPOM1020 = string.Empty;
        private string fsPOM1030 = string.Empty;

        #region Description : 페이지 로드
        public TYMROP001S()
        {
            InitializeComponent();
        }

        private void TYMROP001S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_MR_2C48U911.Initialize();

            SetStartingFocus(TXT01_OPM1000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2C48U911.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2C48U910",
                this.TXT01_OPM1000.GetValue(),
                this.TXT01_OPM1040.GetValue()
                );

            this.FPS91_TY_S_MR_2C48U911.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYMROP001I("", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2C48U911_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMROP001I(this.FPS91_TY_S_MR_2C48U911.GetValue("OPM1000").ToString(), this.FPS91_TY_S_MR_2C48U911.GetValue("OPM1010").ToString()).ShowDialog() == System.Windows.Forms.DialogResult.OK))
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        
    }
}