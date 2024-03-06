using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.US00
{
    /// <summary>
    /// 일별 작업현황관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.09 16:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_949GG301 : 항운노조 일별작업현황 삭제
    ///  TY_P_US_949GI302 : 항운노조 일별작업현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_949GM305 : 항운노조 일별작업현황 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SDATE : 시작일자
    ///  EDATE : 종료일자
    ///  HIGOKJONG : 곡　　종
    ///  HIHANGCHA : 항　　차
    /// </summary>
    public partial class TYUSNJ005S : TYBase
    {

        #region  Description : 폼 로드 이벤트
        public TYUSNJ005S()
        {
            InitializeComponent();            
        }

        private void TYUSNJ005S_Load(object sender, System.EventArgs e)
        {

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description :  데이터 바인딩 이벤트
        private void UP_DataBinding()
        {
            this.FPS91_TY_S_US_949GM305.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_949GI302", CBH01_HIHANGCHA.GetValue(), CBH01_HIGOKJONG.GetValue() , this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString() );

            this.FPS91_TY_S_US_949GM305.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_DataBinding();
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_949GG301", dt);
            this.DbConnector.ExecuteNonQueryList();

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_US_949GM305.GetDataSourceInclude(TSpread.TActionType.Remove, "HIHANGCHA", "HIGOKJONG", "HIWKDATE", "HISEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYUSNJ005I(string.Empty, string.Empty, string.Empty,string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_US_949GM305_CellDoubleClick 이벤트
        private void FPS91_TY_S_US_949GM305_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYUSNJ005I(this.FPS91_TY_S_US_949GM305.GetValue("HIHANGCHA").ToString(),
            this.FPS91_TY_S_US_949GM305.GetValue("HIGOKJONG").ToString(),
            this.FPS91_TY_S_US_949GM305.GetValue("HIWKDATE").ToString(),
            this.FPS91_TY_S_US_949GM305.GetValue("HISEQ").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion





    }
}

