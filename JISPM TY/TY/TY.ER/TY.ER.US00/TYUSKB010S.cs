using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 양수일자별 양수도 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.18 11:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92IB5789 : 양수일자별 양수 조회
    ///  TY_P_US_92IBE794 : 양수도관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92IBA790 : 양수일자별 양수도 조회
    ///  TY_S_US_92IBG795 : 양수일자별 양수도 내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  YNHANGCHA : 항차
    ///  SDATE : 시작일자
    ///  EDATE : 종료일자
    /// </summary>
    public partial class TYUSKB010S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSKB010S()
        {
            InitializeComponent();
        }

        private void TYUSKB010S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92IBA790.Initialize();
            this.FPS91_TY_S_US_92IBG795.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92IB5789", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.CBH01_YNHANGCHA.GetValue());
            this.FPS91_TY_S_US_92IBA790.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_US_92IBA790_CellDoubleClick 이벤트
        private void FPS91_TY_S_US_92IBA790_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {            

            this.FPS91_TY_S_US_92IBG795.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92IBE794", this.FPS91_TY_S_US_92IBA790.GetValue("YNHANGCHA").ToString(),
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNGOKJONG").ToString(),                                                        
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNBLNO").ToString(),
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNBLMSN").ToString(),
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNBLHSN").ToString(),
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNCUSTIL").ToString(),
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNCUSTCH").ToString(),
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNYNHWAJU").ToString(),
                                                        this.FPS91_TY_S_US_92IBA790.GetValue("YNYSSEQ").ToString()
                                                         );
            this.FPS91_TY_S_US_92IBG795.SetValue(this.DbConnector.ExecuteDataTable());     

        }
        #endregion


    }
}
