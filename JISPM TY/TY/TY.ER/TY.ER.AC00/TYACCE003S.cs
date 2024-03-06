using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 신용카드결재관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.10 11:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29A35896 : 신용카드결재관리 전표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29A3A897 : 신용카드결재관리 전표조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  A6CDBK : 은행코드
    ///  A6NOAC : 계좌번호
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  B2DPMK :  작성부서
    /// </summary>
    public partial class TYACCE003S : TYBase
    {
        private bool _Isloaded = false;

        public TYACCE003S()
        {
            InitializeComponent();
        }

        private void TYACCE003S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_A6CDBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_A6CDBK_CodeBoxDataBinded);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetValue();

            this.SetStartingFocus(this.DTP01_GSTDATE);

            this.BTN61_INQ_Click(null, null);

            this.CBH01_A6CDBK_CodeBoxDataBinded(null, null);
        }

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_29A35896", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_29A3A897.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_29A3A897.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_29A3A897, "B2CDAC", "전표합계", SumRowType.Total);  
            }
        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACCE003I(string.Empty, string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 계좌번호 코드 헬프 처리
        private void CBH01_A6CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_A6CDBK.GetValue().ToString();
            this.CBH01_A6NOAC.DummyValue = groupCode;
            this.CBH01_A6NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_A6NOAC.Initialize();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29A3A897_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_29A3A897_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYACCE003I(this.FPS91_TY_S_AC_29A3A897.GetValue("B2DPMK").ToString(),
                                                   this.FPS91_TY_S_AC_29A3A897.GetValue("B2DTMK").ToString(),
                                                   this.FPS91_TY_S_AC_29A3A897.GetValue("B2NOSQ").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        #endregion
    }
}
