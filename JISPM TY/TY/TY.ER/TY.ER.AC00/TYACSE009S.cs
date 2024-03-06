using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 재무상태표 비 현금성자산 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.24 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_46OGR874 : 비 현금성자산 상세 조회
    ///  TY_P_AC_46OGS875 : 비 현금성자산 집계 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46OGV876 : 비 현금성자산 집계 조회
    ///  TY_S_AC_46OGW877 : 비 현금성자산 내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  Y2CDDP : 예산부서
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYACSE009S : TYBase
    {
        #region Description : 폼 Load
        public TYACSE009S()
        {
            InitializeComponent();
        }

        private void TYACSE009S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            //this.CBH01_Y2CDDP.DummyValue = this.DTP01_YYYYMM.GetString() + "01"; 

            this.DTP01_YYYYMM.Focus();

        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_46OGV876.Initialize();
            this.FPS91_TY_S_AC_46OGW877.Initialize();
            this.DbConnector.CommandClear();
            
            this.DbConnector.Attach("TY_P_AC_46OGS875", this.DTP01_YYYYMM.GetString().ToString().Substring(0,6));
            this.FPS91_TY_S_AC_46OGV876.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : Spread  CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_46OGV876_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {          
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_46OGR874", this.FPS91_TY_S_AC_46OGV876.GetValue("ANBSRYYMM").ToString(),
                                                        this.FPS91_TY_S_AC_46OGV876.GetValue("ANBSHCDAC").ToString());
            this.FPS91_TY_S_AC_46OGW877.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

    }
}
