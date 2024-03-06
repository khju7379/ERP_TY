using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 전자세금계산서 발행 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.04.17 08:55
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_34H2D515 : 전자세금계산서 발행 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_34H2E516 : 전자세금계산서 발행자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  B2DPMK : 작성부서
    ///  VNCODE : 거래처코드
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYAZZZ07C1 : TYBase
    {
        public string fsJUNNO = "";

        #region  Description : 폼 로드
        public TYAZZZ07C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYAZZZ07C1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString().ToString();

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.CBH01_B2DPMK);
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_34H2E516.Initialize();
 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_34H2D515", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBH01_B2DPMK.GetValue(), this.CBH01_VNCODE.GetValue());

            this.FPS91_TY_S_AC_34H2E516.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : DTP01_GSTDATE_ValueChanged
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString().ToString(); 
        }
        #endregion

        #region  Description : DTP01_GEDDATE_ValueChanged
        private void DTP01_GEDDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_GEDDATE.GetString().ToString(); 
        }
        #endregion

        #region  Description : DTP01_GEDDATE_ValueChanged
        private void FPS91_TY_S_AC_34H2E516_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsJUNNO = this.FPS91_TY_S_AC_34H2E516.GetValue("JUNNO").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region  Description : DTP01_GEDDATE_ValueChanged
        private void FPS91_TY_S_AC_34H2E516_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.FPS91_TY_S_AC_34H2E516_CellDoubleClick(null, null);
        }
        #endregion
    }
}
