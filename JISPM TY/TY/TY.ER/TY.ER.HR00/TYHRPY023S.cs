using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여인상액관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.11.05 16:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5B5GJ106 : 급여인상액 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5B5GJ107 : 급여인상액 자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  PEJKCD : 직급
    ///  PESABUN : 사번
    ///  PEYEAR : 년도
    /// </summary>
    public partial class TYHRPY023S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY023S()
        {
            InitializeComponent();
        }

        private void TYHRPY023S_Load(object sender, System.EventArgs e)
        {
            this.SetStartingFocus(this.CBH01_PESABUN);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5B5GJ107.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5B5GJ106", this.CBH01_PESABUN.GetValue().ToString(), this.CBH01_PEJKCD.GetValue(), this.TXT01_PEYEAR.GetValue());
            this.FPS91_TY_S_HR_5B5GJ107.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY009S()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_5B5GJ107_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //this.FPS91_TY_S_AC_23T22133.GetValue("CDCODE").ToString()

            DataTable dt = new DataTable();

            if ((new TYHRPY009I(dt, this.FPS91_TY_S_HR_5B5GJ107.GetValue("PENUM").ToString().Substring(0,4),
                                    this.FPS91_TY_S_HR_5B5GJ107.GetValue("PENUM").ToString().Substring(5,3),
                                    this.FPS91_TY_S_HR_5B5GJ107.GetValue("PECRDATE").ToString(),
                                    this.FPS91_TY_S_HR_5B5GJ107.GetValue("PESABUN").ToString(),
                                    this.FPS91_TY_S_HR_5B5GJ107.GetValue("PEJKCD").ToString(),
                                    ""
                                    )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                   this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 호봉인상 조회 팝업 버튼
        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY23C1()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
