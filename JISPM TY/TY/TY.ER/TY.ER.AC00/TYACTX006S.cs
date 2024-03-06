using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세코드 옵션 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.11.27 16:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BR4V468 : 부가세코드 옵션 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3BR5N475 : 부가세 코드 옵션 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  MET2040 : 부가세코드
    /// </summary>
    public partial class TYACTX006S : TYBase
    {
        #region Description : 페이지 로드
        public TYACTX006S()
        {
            InitializeComponent();
        }

        private void TYACTX006S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.SetStartingFocus(this.CBH01_FXYSVATGB.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_3BR5N475.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
            (
            "TY_P_AC_3BR4V468",
            CBH01_FXYSVATGB.GetValue()
            );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_3BR5N475.SetValue(dt);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACTX006I("")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C21Q498", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 스프레드 클릭
        private void FPS91_TY_S_AC_3BR5N475_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACTX006I(this.FPS91_TY_S_AC_3BR5N475.GetValue("ATTAXCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_3BR5N475.GetDataSourceInclude(TSpread.TActionType.Remove, "ATTAXCODE");

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
