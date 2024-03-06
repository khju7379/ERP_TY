using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 고객정보관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.26 14:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92QF1912 : 고객정보관리 조회(SILO)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92QF1913 : 고객정보관리 조회
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
    ///  EMISANGHO : 거래처상호
    /// </summary>
    public partial class TYUSKB014S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSKB014S()
        {
            InitializeComponent();
        }

        private void TYUSKB014S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);                      

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_EMISANGHO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92QF1913.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_92QF1912",
                 this.TXT01_EMISANGHO.GetValue().ToString()
                );

            this.FPS91_TY_S_US_92QF1913.SetValue(this.DbConnector.ExecuteDataTable());          
        }
        #endregion        

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYUSKB014I(string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_66OGY399", dt.Rows[i]["EMUSERID"].ToString());
                }
                this.DbConnector.ExecuteTranQueryList();
            }           
           
            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            
            DataTable dt = this.FPS91_TY_S_US_92QF1913.GetDataSourceInclude(TSpread.TActionType.Remove, "EMUSERID");

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

        #region Description : FPS91_TY_S_US_92QF1913_CellDoubleClick 버튼
        private void FPS91_TY_S_US_92QF1913_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYUSKB014I(this.FPS91_TY_S_US_92QF1913.GetValue("EMUSERID").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}