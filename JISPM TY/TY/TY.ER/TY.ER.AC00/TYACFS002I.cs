using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 채권연령분석관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.20 10:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27KBA144 : 채권 연령분석Master 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27KBN146 : 채권 연령분석 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  BMCDAC : 상위계정
    ///  BMSAUP : 사업부
    ///  BMVEND : 거래처
    ///  BMYYMM : 기준년월
    /// </summary>
    public partial class TYACFS002I : TYBase
    {
        #region Description : 폼로드 이벤트
        public TYACFS002I()
        {
            InitializeComponent();
        }

        private void TYACFS002I_Load(object sender, System.EventArgs e)
        {
            this.DTP01_BMYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));

            this.CBH01_BMSAUP.DummyValue = this.DTP01_BMYYMM.GetValue() + "01";

            SetStartingFocus(DTP01_BMYYMM);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS002B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_27KBN146.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27KBA144", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_27KBN146.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_27KBN146, "BMCDACNM", "합 계", SumRowType.Total);
            
        }
        #endregion

        #region Description : DTP01_BMYYMM_ValueChanged 이벤트
        private void DTP01_BMYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_BMSAUP.DummyValue = this.DTP01_BMYYMM.GetValue() + "01";
        }
        #endregion

        #region Description : FPS91_TY_S_AC_27KBN146_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_27KBN146_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACFS002S( this.FPS91_TY_S_AC_27KBN146.GetValue("BMYYMM").ToString(),
                                 this.FPS91_TY_S_AC_27KBN146.GetValue("BMSAUP").ToString(),
                                 this.FPS91_TY_S_AC_27KBN146.GetValue("BMVEND").ToString(),
                                 this.FPS91_TY_S_AC_27KBN146.GetValue("BMCDAC").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
