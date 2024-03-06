using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using TY.ER.GB00;


namespace TY.ER.HR00
{
    /// <summary>
    /// 상해보험 출력관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.29 08:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73LC1027 : 상해보험 지급내역 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  SDATE : 시작일자
    ///  EDATE : 종료일자
    ///  INMBIGO : 비고
    /// </summary>
    public partial class TYHRKB022P : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB022P()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYHRKB022P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.UP_Set_JuminAuthCheck(CBO01_INQOPTION);

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73T90133", TYUserInfo.SecureKey, CBO01_INQOPTION.GetValue().ToString(), this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            ActiveReport rpt = new TYHRKB022R(dt, TXT01_INMBIGO.GetValue().ToString() );
            (new TYERGB001P(rpt, dt)).ShowDialog();
        }
        #endregion      
        
        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}
