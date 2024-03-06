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
    /// 상해보험 지급내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.21 11:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73LC1027 : 상해보험 지급내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73LD0030 : 상해보험 지급내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  INMSABUN : 계약자
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    ///  INMPNAME : 피보험자
    /// </summary>
    public partial class TYHRKB022S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB022S()
        {
            InitializeComponent();
        }

        private void TYHRKB022S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.UP_Set_JuminAuthCheck(CBO01_INQOPTION);

            this.SetStartingFocus(this.DTP01_SDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73LD0030.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73LC1027", TYUserInfo.SecureKey, CBO01_INQOPTION.GetValue().ToString(), this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString(), CBH01_INMSABUN.GetValue(), TXT01_INMPNAME.GetValue());
            this.FPS91_TY_S_HR_73LD0030.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_73LD0030.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_73LD0030, "INMSABUNNM", "합   계", SumRowType.Sum, "INMREQAMOUNT", "INMPYAMOUNT");                
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRKB022I(string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_73LD0030_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_73LD0030_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_HR_73LD0030.GetValue("INMDATE").ToString() != "")
            {
                if (this.OpenModalPopup(new TYHRKB022I(this.FPS91_TY_S_HR_73LD0030.GetValue("INMDATE").ToString(),
                                                       this.FPS91_TY_S_HR_73LD0030.GetValue("INMSABUN").ToString()
                                                      )) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73LDE035", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_73LD0030.GetDataSourceInclude(TSpread.TActionType.Remove, "INMDATE", "INMSABUN");

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

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRKB022P()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

      
    }
}
