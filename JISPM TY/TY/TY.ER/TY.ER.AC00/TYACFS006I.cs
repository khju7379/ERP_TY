using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대손충당금 대상금액 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.09 18:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2896W342 : 대손충당금 대상금액 Master 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2896Y344 : 대손충당금 대상금액 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  BTMHCDAC : 상위계정
    ///  BTMSAUP : 사업부
    ///  BTMYYMM : 기준년월
    /// </summary>
    public partial class TYACFS006I : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACFS006I()
        {
            InitializeComponent();
        }

        private void TYACFS006I_Load(object sender, System.EventArgs e)
        {
            this.DTP01_BTMYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.CBH01_BTMSAUP.DummyValue = this.DTP01_BTMYYMM.GetValue() + "01";

            SetStartingFocus(DTP01_BTMYYMM);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS006B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2896W342", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_2896Y344.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_2896Y344.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2896Y344, "BTMHCDACNM", "합 계", SumRowType.Total,"BTMBONDAMT","BTMDSNORAMT","BTMCHNORAMT","BTMDSIDAYAMT","BTMCHIDAYAMT","BTMCHTOTAL","BTMBONDJANAMT" );
            }
        }
        #endregion

        #region Description : DTP01_BTMYYMM_ValueChanged 이벤트
        private void DTP01_BTMYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_BTMSAUP.DummyValue = this.DTP01_BTMYYMM.GetValue() + "01";
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2896Y344_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2896Y344_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACFS006S(this.FPS91_TY_S_AC_2896Y344.GetValue("BTMYYMM").ToString(),
                                this.FPS91_TY_S_AC_2896Y344.GetValue("BTMSAUP").ToString(),
                                this.FPS91_TY_S_AC_2896Y344.GetValue("BTMHCDAC").ToString(),
                                "" )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
