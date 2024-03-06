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
    ///  TY_S_US_9BLDX533 : 곡종별 작업현황 조회
    ///  TY_S_US_9BLDX533 : 모선별 작업현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSPR010S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR010S()
        {
            InitializeComponent();
        }

        private void TYUSPR010S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_9BLDX533.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9BLDW531", this.CBH01_STHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_US_9BLDX533.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}