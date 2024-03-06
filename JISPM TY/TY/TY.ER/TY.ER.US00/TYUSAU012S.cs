using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 차량조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.03.27 10:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_93RD0191 : 차량조회 (정문차량관리)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_93RD1193 : 차량조회 (정문차량관리)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  DATE : 일자
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUSAU012S : TYBase
    {
        #region Description : 폼 로드
        public TYUSAU012S()
        {
            InitializeComponent();
        }

        private void TYUSAU012S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.AddYears(10).ToString("yyyy-MM-dd"));
            this.DTP01_DATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_93RD1193.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_93RD0191", this.DTP01_STDATE.GetValue().ToString(),
                                                        this.DTP01_EDDATE.GetValue().ToString(),
                                                        this.DTP01_DATE.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_US_93RD1193.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}
