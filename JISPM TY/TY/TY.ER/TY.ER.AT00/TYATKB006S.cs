using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AT00
{
    /// <summary>
    /// 세대별 월별요금관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.09.20 14:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_89KFB733 : 세대별 월별요금관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_89KFF737 : 세대별 월별요금관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  AMRYYMM : 작업년월
    ///  AMRHOSU : 호 수
    ///  INQOPTION : 조회구분
    /// </summary>
    public partial class TYATKB006S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYATKB006S()
        {
            InitializeComponent();
        }

        private void TYATKB006S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_AMRYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_AMRYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_89KFF737.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_89KFB733", this.DTP01_AMRYYMM.GetString().ToString().Substring(0,6), this.TXT01_AMRHOSU.GetValue(), this.CBO01_INQOPTION.GetValue().ToString());
            this.FPS91_TY_S_HR_89KFF737.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_89KFF737.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_89KFF737, "AMRHOSU", "합  계", SumRowType.Sum, "AMRTOTALAMT", "AMRLATEAMT", "SF1001", "SF1002", "SF1003", "SF1004", "SF1005", "SF1006", "SF1007", "SF1008", "SF1009", "SF1010", "SF1011", "SF1012", "SF1013", "SF1100");                

            }

        }
        #endregion

        #region  Description : FPS91_TY_S_HR_89KFF737_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_89KFF737_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB006I(this.FPS91_TY_S_HR_89KFF737.GetValue("AMRYYMM").ToString(),
                                                   this.FPS91_TY_S_HR_89KFF737.GetValue("AMRHOSU").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
