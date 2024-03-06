using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 시간대별 차량현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.18 14:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92IEN799 : 시간대별 차량출고 현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92IF4800 : 시간대별 차량출고 현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SDATE : 시작일자
    ///  CHCHTIME : 출문시간
    ///  CHIPTIME : 입문시간
    /// </summary>
    public partial class TYUSKB011S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSKB011S()
        {
            InitializeComponent();
        }

        private void TYUSKB011S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IF4800, "CHHWAJU");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IF4800, "HWAJUNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IF4800, "CHNUMBER");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IF4800, "CHTIME");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IF4800, "CNT");

            MTB01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.MTB01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92IF4800.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_92IEN799",
                 Get_Date(this.MTB01_SDATE.GetValue().ToString()),
                 this.MTB01_CHIPTIME.GetValue().ToString().Replace(":","").Trim(),
                 this.MTB01_CHCHTIME.GetValue().ToString().Replace(":", "").Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_US_92IF4800.SetValue(dt);

                if (this.FPS91_TY_S_US_92IF4800.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_US_92IF4800, "HWAJUNM", "[화주계]", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_US_92IF4800, "HWAJUNM", "[총 계]", SumRowType.Total);
                }
            }

            Timer tmr = new Timer();
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Interval = 100;
            tmr.Start();
        }
        #endregion        

        #region Description : 엔터 이벤트
        private void MTB01_SDATE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.MTB01_CHIPTIME);
            }
        }

        private void MTB01_CHCHTIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.BTN61_INQ);
            }
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            MTB01_SDATE.Focus();
        }

        void tmr_Tick1(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            MTB01_CHIPTIME.Focus();
        }
        #endregion
    }
}