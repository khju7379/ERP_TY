using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 제출자정보 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.11.20 13:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BK25384 : 제출자정보 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3BK1M381 : 제출자정보 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  BADVEND : 거래처
    ///  VNGUBUN : 구분
    /// </summary>
    public partial class TYACTX004S : TYBase
    {
        public TYACTX004S()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYACTX004S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            SetStartingFocus(this.CBH01_BADVEND);
        }
        #endregion

        #region Description : 조회버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_3BK1M381.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3BK25384",
                "",
                this.CBH01_BADVEND.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_3BK1M381.SetValue(dt);
            }
        }
        #endregion

        #region Description : 신규버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACTX004I("")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C2AB492", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_AC_3BK1M381_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACTX004I(this.FPS91_TY_S_AC_3BK1M381.GetValue("ASMVNEDCD").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_3BK1M381.GetDataSourceInclude(TSpread.TActionType.Remove, "ASMBRANCH", "ASMVNEDCD");

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
    }
}
