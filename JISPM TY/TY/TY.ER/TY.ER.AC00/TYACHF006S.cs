using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.03 17:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2C352805 : 고정자산 Master 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2C354807 : 고정자산 Master 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  FLGUBN : 자산분류코드
    ///  FLNAME : 자산명
    ///  FLYEAR : 자산년도
    /// </summary>
    public partial class TYACHF006S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACHF006S()
        {
            InitializeComponent();
        }

        private void TYACHF006S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.MTB01_FLYEAR.SetValue(DateTime.Now.AddYears(-3).ToString("yyyy"));
            this.MTB02_FLYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.MTB01_FLYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_31U3P965.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31U3I964", this.MTB01_FLYEAR.GetValue(), this.MTB02_FLYEAR.GetValue(), this.TXT01_FLNAME.GetValue().ToString());
            this.FPS91_TY_S_AC_31U3P965.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACHF006I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_31U3P965_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_31U3P965_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACHF006I(this.FPS91_TY_S_AC_31U3P965.GetValue("FLYEAR").ToString(), this.FPS91_TY_S_AC_31U3P965.GetValue("FLSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_31U4H966", dt.Rows[i]["FLYEAR"].ToString(),
                                                            dt.Rows[i]["FLSEQ"].ToString()); //고정자산 토지 삭제

                this.DbConnector.Attach("TY_P_AC_3212M992", dt.Rows[i]["FLYEAR"].ToString(),
                                                            dt.Rows[i]["FLSEQ"].ToString()); //고정자산 토지 공시지가 이력 삭제
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_31U3P965.GetDataSourceInclude(TSpread.TActionType.Remove, "FLYEAR", "FLSEQ", "FLJPNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FLJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;
                    return;
                }
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
