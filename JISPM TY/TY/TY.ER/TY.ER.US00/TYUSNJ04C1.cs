using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 하역료 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_919HR470 : 하역료 단가 관리
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
    /// </summary>
    public partial class TYUSNJ04C1 : TYBase
    {
        string fsHDYYMM = string.Empty;

        #region Descriptino : 페이지 로드
        public TYUSNJ04C1(string sHDYYMM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsHDYYMM = sHDYYMM;
        }

        private void TYUSNJ04C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(this.fsHDYYMM.ToString());

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AOF2419", Get_Date(this.DTP01_GEDYYMM.GetValue().ToString()),
                                                        Get_Date(this.DTP01_GSTYYMM.GetValue().ToString())
                                                        );
            
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_AC_27J83134");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 복사 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AOEV415", Get_Date(this.DTP01_GSTYYMM.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_US_9AOEW417");
                this.SetFocus(this.DTP01_GSTYYMM);

                e.Successed = false;
                return;
            }


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AOEV415", Get_Date(this.DTP01_GEDYYMM.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_9AOEX418");
                this.SetFocus(this.DTP01_GEDYYMM);

                e.Successed = false;
                return;
            }


            // 처리 하시겠습니까?
            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
