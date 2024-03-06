using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;


namespace TY.ER.US00
{
    /// <summary>
    /// 일일작업현황표 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.06.26 11:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_96QBW931 : 일일작업현황표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_96QDJ933 : 일일작업현황표 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSPR002S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR002S()
        {
            InitializeComponent();
        }

        private void TYUSPR002S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            //this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_96QDJ933.Initialize();

            DataTable dt = UP_Get_DataBinding();          

            this.FPS91_TY_S_US_96QDJ933.SetValue(dt);

            if (this.FPS91_TY_S_US_96QDJ933.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_US_96QDJ933, "JGSUNSA", "합  계", SumRowType.Sum, "JGBEJNQTY", "JGHWAKQTY", "JGTDIPQTY", "JGIPNUQTY", "JGVSJNQTY", "JGTDCHQTY", "JGCHNUQTY","JGJEGOQTY");
            }

        }
        #endregion        

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {           

            DataTable dt = UP_Get_DataBinding();
            if (dt.Rows.Count > 0)
            {
                DataTable Gkdt = UP_Get_DataGKTotal();

                ActiveReport rpt = new TYUSPR002R1(Gkdt);
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : DataBinding 함수
        private DataTable UP_Get_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_96QBW931",
                 this.DTP01_SDATE.GetString().ToString(),
                 this.CBH01_IHHANGCHA.GetValue().ToString()
                );

            return this.DbConnector.ExecuteDataTable();
        }
        #endregion

        #region Description : 곡종별 집계 
        private DataTable UP_Get_DataGKTotal()
        {
            this.DbConnector.CommandClear();

            if (Convert.ToDouble(this.CBH01_IHHANGCHA.GetValue().ToString()) >= 202101)
            {
                this.DbConnector.Attach
                    (
                     "TY_P_US_BAJDI640",
                     this.DTP01_SDATE.GetString().ToString(),
                     this.CBH01_IHHANGCHA.GetValue().ToString()
                    );
            }
            else
            {
                this.DbConnector.Attach
                    (
                     "TY_P_US_96SBM947",
                     this.DTP01_SDATE.GetString().ToString(),
                     this.CBH01_IHHANGCHA.GetValue().ToString()
                    );
            }

            return this.DbConnector.ExecuteDataTable();
        }
        #endregion
    }
}