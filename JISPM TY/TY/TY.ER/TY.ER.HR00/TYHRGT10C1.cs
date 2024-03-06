using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 휴무관리 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.04.03 14:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BQF4543 : 휴무관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_543EP062 : 휴무관리 조회(출장비정산관리용)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GHCODE : 휴무코드
    ///  GHSABUN : 사   번
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRGT10C1 : TYBase
    {
        public string fsPopGHCODE = string.Empty;
        public string fsPopGHDATE = string.Empty;
        public string fsPopGHGWID = string.Empty;
        
        public TYHRGT10C1()
        {
            InitializeComponent();
        }

        private void TYHRGT10C1_Load(object sender, System.EventArgs e)
        {
            this.CBH01_GHCODE.SetValue("205");

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_543EP062.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BQF4543", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.CBH01_GHSABUN.GetValue(), this.CBH01_GHCODE.GetValue(), "" );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_543EP062.SetValue(dt);
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_543EP062_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_543EP062_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsPopGHCODE = this.FPS91_TY_S_HR_543EP062.GetValue("GHCODE").ToString();
            fsPopGHDATE = this.FPS91_TY_S_HR_543EP062.GetValue("GHDATE").ToString();
            fsPopGHGWID = this.FPS91_TY_S_HR_543EP062.GetValue("GHGWID").ToString(); 

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
