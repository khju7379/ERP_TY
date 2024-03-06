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
    /// 퇴충금 급여선택 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.02.21 11:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_72LBU747 : 퇴충금 급여선택 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_72LBV748 : 퇴충금 급여선택 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SEL : 선택
    ///  PAYGUBN : 급여구분
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRKB18C1 : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB18C1()
        {
            InitializeComponent();
        }

        private void TYHRKB18C1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_72LBV748.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_72LBU747", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), CBH01_PAYGUBN.GetValue() );
            this.FPS91_TY_S_HR_72LBV748.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
