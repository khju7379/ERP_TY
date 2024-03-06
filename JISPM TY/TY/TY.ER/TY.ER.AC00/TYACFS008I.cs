using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대손처리 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.14 11:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28E1P391 : 대손처리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28E1X392 : 대손처리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  BADHCDAC : 상위계정
    ///  BADSAUP : 사업부
    ///  BADVEND : 거래처
    ///  BADYYMM : 기준년월
    /// </summary>
    public partial class TYACFS008I : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACFS008I()
        {
            InitializeComponent();
        }

        private void TYACFS008I_Load(object sender, System.EventArgs e)
        {
            this.DTP01_BADYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_BADSAUP.DummyValue = this.DTP01_BADYYMM.GetValue() + "01";

            SetStartingFocus(DTP01_BADYYMM);

            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS008B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_28E1X392.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_28E1P391", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_28E1X392.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_28E1X392.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_28E1X392, "BADHCDACNM", "합 계", SumRowType.Total);
            }
        }
        #endregion

        #region Description :  대손전표 발행 버튼 이벤트
        private void BTN61_BTNJUNPYO_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
