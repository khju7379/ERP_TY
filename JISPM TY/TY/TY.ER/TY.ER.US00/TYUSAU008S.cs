using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 일별 BIN 총작업 현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.22 14:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92MEH864 : 일별 BIN 총작업현황 조회
    ///  TY_P_US_92MEH865 : 일별 BIN 총작업현황 상세 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92MEJ866 : 일별 BIN 총작업현황 조회
    ///  TY_S_US_92MEJ867 : 일별 BIN 총작업현황 상세 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    ///  CHBINNO : BIN NO
    /// </summary>
    public partial class TYUSAU008S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSAU008S()
        {
            InitializeComponent();
        }

        private void TYUSAU008S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92MEJ866.Initialize();
            this.FPS91_TY_S_US_92MEJ867.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92MEH864", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.TXT01_CHBINNO.GetValue());
            this.FPS91_TY_S_US_92MEJ866.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_US_92MEJ866_CellDoubleClick 이벤트
        private void FPS91_TY_S_US_92MEJ866_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            this.FPS91_TY_S_US_92MEJ867.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92MEH865", this.FPS91_TY_S_US_92MEJ866.GetValue("CHCHULDAT").ToString().Replace("-", "").Trim(),
                                                        this.FPS91_TY_S_US_92MEJ866.GetValue("CHBINNO").ToString()                                                        
                                                         );
            this.FPS91_TY_S_US_92MEJ867.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_US_92MEJ867.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_US_92MEJ867, "SCAR", "합  계", SumRowType.Sum, "CHMTQTY");
            }
        }
        #endregion


    }
}
