using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매발주 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.19 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2BQBW631 : 구매입고 마스터 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2BQBY632 : 구매발주 마스터 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BC51262 : 결재 완료 된 문서가 아닙니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  RRM1000 : 사업부
    ///  RRM1010 : 발주구분
    ///  RRM1020 : 년월
    ///  POM1180 : 공사및구매명
    /// </summary>
    public partial class TYMRBP001S : TYBase
    {
        private string fsPOM1000 = string.Empty;
        private string fsPOM1010 = string.Empty;
        private string fsPOM1020 = string.Empty;
        private string fsPOM1030 = string.Empty;

        #region Description : 페이지 로드
        public TYMRBP001S()
        {
            InitializeComponent();
        }

        private void TYMRBP001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_MR_2CA3T011.Initialize();

            this.SetStartingFocus(this.TXT01_MAYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2CA3T011.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2CA3Q010",
                this.TXT01_MAYYMM.GetValue(),
                this.TXT01_MASEQ.GetValue(),
                this.CBH01_MABPCODE.GetValue(),
                this.TXT01_MABPDESC.GetValue()
                );

            this.FPS91_TY_S_MR_2CA3T011.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYMRBP001I("", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_MR_2CA5K016", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_MR_2CA3T011.GetDataSourceInclude(TSpread.TActionType.Remove, "MAYYMM", "MASEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            // 수선 자료 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_MR_2CA5E013",
                                   dt.Rows[0]["MAYYMM"].ToString(),
                                   dt.Rows[0]["MASEQ"].ToString()
                                   );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2CA5F015");
                e.Successed = false;
                return;
            }

            // 이력 자료 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_MR_2CA5D012",
                                   dt.Rows[0]["MAYYMM"].ToString(),
                                   dt.Rows[0]["MASEQ"].ToString()
                                   );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_2CA5F014");
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

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2CA3T011_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMRBP001I(this.FPS91_TY_S_MR_2CA3T011.GetValue("MAYYMM").ToString(), this.FPS91_TY_S_MR_2CA3T011.GetValue("MASEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}