using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 화주별 입출고 집계표 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.03.15 13:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_93FEI139 : 화주별 입출고 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_93FEJ141 : 화주별 입출고 집계표
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSPR004S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR004S()
        {
            InitializeComponent();
        }

        private void TYUSPR004S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_93FEJ141.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_93FEI139",
                 this.DTP01_SDATE.GetString().ToString(),
                 this.DTP01_EDATE.GetString().ToString(),
                 this.CBH01_IHHANGCHA.GetValue().ToString(),
                 this.CBH02_IHHANGCHA.GetValue().ToString(),
                 this.CBH01_IHHWAJU.GetValue().ToString()
                );

            this.FPS91_TY_S_US_93FEJ141.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_US_93FEJ141.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_US_93FEJ141, "CHGOKJONGNM", "[곡종소계]", SumRowType.SubTotal);
            }

        }
        #endregion        

       



    }
}