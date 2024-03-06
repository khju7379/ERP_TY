using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 상조회코드조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.11.26 14:29
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5BQEM219 : 상조회코드관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5BQEM220 : 상조회코드관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  SJCODE : 상조회 계정과목
    ///  SJNMAC : 상조회 계정명
    /// </summary>
    public partial class TYHRSJ001S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRSJ001S()
        {
            InitializeComponent();
        }

        private void TYHRSJ001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetStartingFocus(this.TXT01_SJCODE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5BQEM220.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BQEM219", this.TXT01_SJCODE.GetValue().ToString(), this.TXT01_SJNMAC.GetValue());
            this.FPS91_TY_S_HR_5BQEM220.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRSJ001I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_5BQEM220_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRSJ001I(this.FPS91_TY_S_HR_5BQEM220.GetValue("SJCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {            

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BQF4225", dt);
            this.DbConnector.ExecuteTranQueryList();
                       
            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_5BQEM220.GetDataSourceInclude(TSpread.TActionType.Remove, "SJCODE");

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
