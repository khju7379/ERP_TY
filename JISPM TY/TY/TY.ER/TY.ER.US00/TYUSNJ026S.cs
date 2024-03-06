using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 항운노조 근로복기공단 임금자료 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.03.05 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_935BH980 : 항운노조 근로복지공단 임금자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_935BH981 : 항운노조 근로복지공단 임금자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSNJ026S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSNJ026S()
        {
            InitializeComponent();
        }

        private void TYUSNJ026S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_935BH981.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_935BH980", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", this.DTP01_SDATE.GetString().Substring(0, 6), this.DTP01_EDATE.GetString().Substring(0, 6));
            this.FPS91_TY_S_US_935BH981.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_US_935BH981, "HUJUSO", "합 계", SumRowType.Total, "UGUM", "SUGUM");
        }
        #endregion
    }
}