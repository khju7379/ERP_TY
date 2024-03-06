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
    /// 사용자 정의 값 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.10.12 13:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5ACDJ971 : 사용자정의값 삭제
    ///  TY_P_HR_5ACDM973 : 사용자정의값 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5ACDM974 : 사용자 정의값 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  USRGUBN : 사용구분
    ///  USRCDNAME : 사용정의코드명
    ///  USRCODE : 사용자정의코드
    /// </summary>
    public partial class TYHRPY022S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY022S()
        {
            InitializeComponent();
        }

        private void TYHRPY022S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetStartingFocus(this.CBO01_USRGUBN);
        }
        #endregion


        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5ACDM974.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5ACDM973", this.CBO01_USRGUBN.GetValue().ToString(), this.TXT01_USRCODE.GetValue(), this.TXT01_USRCDNAME.GetValue());
            this.FPS91_TY_S_HR_5ACDM974.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY022I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5ACDJ971", dt);
            this.DbConnector.ExecuteNonQueryList();         

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_5ACDM974.GetDataSourceInclude(TSpread.TActionType.Remove, "USRGUBN", "USRCODE", "USRSDATE" );

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

        #region  Description : FPS91_TY_S_HR_5ACDM974_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_5ACDM974_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRPY022I(this.FPS91_TY_S_HR_5ACDM974.GetValue("USRGUBN").ToString(),
                                this.FPS91_TY_S_HR_5ACDM974.GetValue("USRCODE").ToString(),
                                this.FPS91_TY_S_HR_5ACDM974.GetValue("USRSDATE").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        #endregion
    }
}
