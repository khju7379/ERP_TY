using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 소속별 배정 집계표 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.03.14 11:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_93EBP093 : 소속별 배정량 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_93EBP095 : 소속별 배정량 집계표
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  IHHANGCHA : 항차
    /// </summary>
    public partial class TYUSPR007S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR007S()
        {
            InitializeComponent();
        }

        private void TYUSPR007S_Load(object sender, System.EventArgs e)
        {        

            SetStartingFocus(this.CBH01_IHHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_93EBP095.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_HR_93EBP093",
                 this.CBH01_IHHANGCHA.GetValue().ToString(),
                 this.CBH02_IHHANGCHA.GetValue().ToString()
                );

            this.FPS91_TY_S_US_93EBP095.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_US_93EBP095, "IHSOSOKNM", "합  계", SumRowType.Sum, "GOKHAP1", "GOKHAP2", "GOKHAP3", "GOKHAP4", "GOKHAP5", "GOKHAP");
        }
        #endregion        

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
             DataTable dt = new DataTable();

          
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_HR_93EBP093",
                 this.CBH01_IHHANGCHA.GetValue().ToString(),
                 this.CBH02_IHHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                
                ActiveReport rpt = new TYUSPR007R();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }        
        #endregion



    }
}