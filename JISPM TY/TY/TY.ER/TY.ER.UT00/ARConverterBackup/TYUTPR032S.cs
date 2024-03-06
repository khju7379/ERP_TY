using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 해안단지 입출고 예정사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.05.14 15:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_95EBV549 : 해안단지 입출고 예정사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_95EE9551 : 해안단지 입출고 예정사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR032S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR032S()
        {
            InitializeComponent();
        }

        private void TYUTPR032S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                this.FPS91_TY_S_UT_95EE9551.Initialize();

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_95EBV549", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            Get_Date(Convert.ToDateTime(this.DTP01_STDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
                                                            Get_Date(Convert.ToDateTime(this.DTP01_EDDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            Get_Date(Convert.ToDateTime(this.DTP01_STDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
                                                            Get_Date(Convert.ToDateTime(this.DTP01_EDDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")));

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_95EE9551.SetValue(dt);
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_95EBV549", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString(),
                                                        Get_Date(Convert.ToDateTime(this.DTP01_STDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
                                                        Get_Date(Convert.ToDateTime(this.DTP01_EDDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
                                                        this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString(),
                                                        Get_Date(Convert.ToDateTime(this.DTP01_STDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
                                                        Get_Date(Convert.ToDateTime(this.DTP01_EDDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")));

            dt = this.DbConnector.ExecuteDataTable();
            
            ActiveReport rpt = new TYUTPR032R();

            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

            (new TYERGB001P(rpt, dt)).ShowDialog();
            
        }
        #endregion
    }
}
