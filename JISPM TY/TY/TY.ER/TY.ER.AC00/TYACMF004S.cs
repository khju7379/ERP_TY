using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 통장거래내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.02 08:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B28Y995 : 통장거래내역 Master 조회
    ///  TY_P_AC_2B29Y001 : 통장거래내역 Detail 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2B2AN003 : 통장거래내역 Master 조회
    ///  TY_S_AC_2B2AP004 : 통장거래내역 Detail 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  B1CDBK : 은  행
    ///  B1DATE : 거래일자
    /// </summary>
    public partial class TYACMF004S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACMF004S()
        {
            InitializeComponent();
        }

        private void TYACMF004S_Load(object sender, System.EventArgs e)
        {
           

            this.DTP01_B1DATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP02_B1DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_B1DATE);

            this.BTN61_INQ_Click(null, null);
            this.FPS91_TY_S_AC_2B2AN003_CellDoubleClick(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2B2AN003.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B28Y995", this.DTP01_B1DATE.GetString(), this.DTP02_B1DATE.GetString(), this.CBH01_B1CDBK.GetValue().ToString());
            this.FPS91_TY_S_AC_2B2AN003.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_2B2AN003.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2B2AN003, "B1CDBKNM", "합   계", SumRowType.Sum, "INPUT", "OUTPUT");
            }

        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2B2AN003_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2B2AN003_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_AC_2B2AP004.Initialize();


            if (this.FPS91_TY_S_AC_2B2AN003.CurrentRowCount > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B29Y001", this.FPS91_TY_S_AC_2B2AN003.GetValue("B1CDBK").ToString(),
                                                            this.FPS91_TY_S_AC_2B2AN003.GetValue("B1NOAC").ToString(),
                                                            this.FPS91_TY_S_AC_2B2AN003.GetValue("B1DATE").ToString());
                this.FPS91_TY_S_AC_2B2AP004.SetValue(this.DbConnector.ExecuteDataTable());
            }

        }
        #endregion

       

       

        
    }
}
