using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 입고 취급량 현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.25 16:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_75PGL610 : 입고 취급량 현황 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  YODATE : 기준일자
    /// </summary>
    public partial class TYUTPR021P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR021P()
        {
            InitializeComponent();
        }

        private void TYUTPR021P_Load(object sender, System.EventArgs e)
        {   
            this.DTP01_YODATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            this.FPS91_TY_S_UT_7ABFE747.Initialize();

            this.BTN61_CLO_Click(null, null);

            SetStartingFocus(this.DTP01_YODATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_7ABFE747.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7B99O971", this.DTP01_YODATE.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7ABFE747.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                string sDATE = this.DTP01_YODATE.GetString().Substring(0, 4) + "년 " + this.DTP01_YODATE.GetString().Substring(4, 2) + "월";

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_75PGL610", this.DTP01_YODATE.GetString().Substring(0, 6));

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUTPR021R(sDATE);

                    // 가로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            catch
            {

            }		
        }
        #endregion

        
    }
}
