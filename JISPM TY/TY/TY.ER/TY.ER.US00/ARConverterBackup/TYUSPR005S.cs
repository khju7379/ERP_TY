using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 곡종,모선별 작업현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.07.01 13:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_971FD957 : 모선별 작업현황 조회
    ///  TY_P_US_971FE958 : 곡종별 작업현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_971FH960 : 곡종별 작업현황 조회
    ///  TY_S_US_971FK961 : 모선별 작업현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSPR005S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR005S()
        {
            InitializeComponent();
        }

        private void TYUSPR005S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_IHHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
                this.FPS91_TY_S_US_972DZ971.Initialize();
                this.FPS92_TY_S_US_972DZ971.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_972DV970",
                        this.CBH01_IHHANGCHA.GetValue().ToString(),
                        this.CBH02_IHHANGCHA.GetValue().ToString(),
                        "1"
                    );
                this.FPS91_TY_S_US_972DZ971.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_US_972DZ971.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_US_972DZ971, "JGHWAJU", "[합  계]", SumRowType.Sum, "JGBEJNQTY01", "JGBEJNQTY02", "JGBEJNQTY03", "JGBEJNQTY04", "JGBEJNQTY05", "JGBEJNQTY06", "JGBEJNQTY07", "JGBEJNQTY08", "JGBEJNQTYHAP");
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_972DV970",
                        this.CBH01_IHHANGCHA.GetValue().ToString(),
                        this.CBH02_IHHANGCHA.GetValue().ToString(),
                        "2"
                    );
                this.FPS92_TY_S_US_972DZ971.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS92_TY_S_US_972DZ971.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS92_TY_S_US_972DZ971, "JGHWAJU", "[합  계]", SumRowType.Sum, "JGBEJNQTY01", "JGBEJNQTY02", "JGBEJNQTY03", "JGBEJNQTY04", "JGBEJNQTY05", "JGBEJNQTY06", "JGBEJNQTY07", "JGBEJNQTY08", "JGBEJNQTYHAP");
                }
        }
        #endregion            

        

       


    }
}