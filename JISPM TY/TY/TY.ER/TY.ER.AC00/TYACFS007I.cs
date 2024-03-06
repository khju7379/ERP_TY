using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대손충당금 설정관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.10 16:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28A4P370 : 대손충당금 설정관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28A4Q371 : 대손충당금 설정관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  BTNJUNPYO : 설정전표발행
    ///  INQ : 조회
    ///  BJMHCDAC : 상위계정
    ///  BJMSAUP : 사업부
    ///  BJMYYMM : 기준년월
    /// </summary>
    public partial class TYACFS007I : TYBase
    {
        #region Description : 페이지 로드 이벤트
        public TYACFS007I()
        {
            InitializeComponent();
        }

        private void TYACFS007I_Load(object sender, System.EventArgs e)
        {
            this.DTP01_BJMYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_BJMSAUP.DummyValue = this.DTP01_BJMYYMM.GetValue() + "01";

            SetStartingFocus(DTP01_BJMYYMM);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS007B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 설정전표발행 버튼 이벤트
        private void BTN61_BTNJUNPYO_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS002B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_28A4Q371.Initialize(); 

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_28A4P370", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_28A4Q371.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_28A4Q371.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_28A4Q371, "BJMHCDACNM", "합 계", SumRowType.Total);
            }
        }
        #endregion

        private void FPS91_TY_S_AC_28A4Q371_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACFS007S(this.FPS91_TY_S_AC_28A4Q371.GetValue("BJMYYMM").ToString(),
                              this.FPS91_TY_S_AC_28A4Q371.GetValue("BJMSAUP").ToString(),
                              this.FPS91_TY_S_AC_28A4Q371.GetValue("BJMHCDAC").ToString()
                              )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }

        
    }
}
