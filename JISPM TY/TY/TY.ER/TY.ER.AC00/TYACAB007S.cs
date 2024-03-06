using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 은행코드관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_243B7323 : 은행코드관리 CODE 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_243B8324 : 은행코드관리 CODE 조회
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
    ///  CDCODE : CODE
    ///  CDDESC1 : 내용1
    /// </summary>
    public partial class TYACAB007S : TYBase
    {
        private string sCODE = string.Empty;

        #region Description : 페이지 로드
        public TYACAB007S()
        {
            InitializeComponent();
        }

        private void TYACAB007S_Load(object sender, System.EventArgs e)
        {
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_CDCODE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_243B7323", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_243B8324.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 조회 
        private void UP_Sel_Display()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_243B7323", sCODE, "");
            this.FPS91_TY_S_AC_243B8324.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            TYACAB007I popup = new TYACAB007I("");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sCODE = popup.fsCODE;

                UP_Sel_Display();
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_243BO334", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_243B8324.GetDataSourceInclude(TSpread.TActionType.Remove, "CDCODE");

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

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_243B8324_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            TYACAB007I popup = new TYACAB007I(this.FPS91_TY_S_AC_243B8324.GetValue("CDCODE").ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sCODE = popup.fsCODE;

                UP_Sel_Display();
            }
            //if ((new TYACAB007I(this.FPS91_TY_S_AC_243B8324.GetValue("CDCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
