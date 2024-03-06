using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 출장비 정산관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.04.02 14:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_542I0017 : 출장비정산관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_542I0018 : 출장비정산관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  EDATE : 종료일자
    ///  GXGWDOCID : 출장문서 번호
    ///  GXSABUN : 사원번호
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRGT010S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRGT010S()
        {
            InitializeComponent();
        }

        private void TYHRGT010S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            this.FPS91_TY_S_HR_542I0018.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_542I0017", this.CBO01_INQOPTION.GetValue().ToString(), this.CBH01_GXSABUN.GetValue().ToString(), this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.TXT01_GXGWDOCID.GetValue().ToString());
            this.FPS91_TY_S_HR_542I0018.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRGT010I("","","")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region  Description : FPS91_TY_S_HR_542I0018_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_542I0018_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRGT010I(this.FPS91_TY_S_HR_542I0018.GetValue("GXGWDOCID").ToString(),
                                this.FPS91_TY_S_HR_542I0018.GetValue("GXCODE").ToString(),
                                this.FPS91_TY_S_HR_542I0018.GetValue("GXDATE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.DTP01_SDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}
