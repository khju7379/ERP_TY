using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 원산지/곡종별 출고현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.22 14:51
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92MF1874 : 원산지/곡종별 출고현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92MF2875 : 원산지/곡종별 출고현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSAU009S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSAU009S()
        {
            InitializeComponent();
        }

        private void TYUSAU009S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92MF2875.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92MF1874", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), CBH01_CHWONSAN.GetValue(), CBH01_CHGOKJONG.GetValue() );
            this.FPS91_TY_S_US_92MF2875.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_US_92MF2875.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_US_92MF2875, "CHGOKJONGNM", "[소 계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_92MF2875, "CHWONSANNM", "[합 계]", SumRowType.Total);
            }

        }
        #endregion     


    }
}
