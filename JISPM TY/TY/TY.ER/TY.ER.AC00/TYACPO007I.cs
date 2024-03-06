using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 품목별분류관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 10:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37AAI053 : EIS 품목분류관리 등록
    ///  TY_P_AC_37AAJ055 : EIS 품목분류관리 조회
    ///  TY_P_AC_37AAK056 : EIS 품목분류관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37AAL057 : EIS 품목분류관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ERCDDP : 사업부
    ///  ERYYMM : 년월
    /// </summary>
    public partial class TYACPO007I : TYBase
    {
        #region  Description : 폼로드 이벤트
        public TYACPO007I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_37AAL057, "ERCDDP", "ERCDDPNM", "ERCDDP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_37AAL057, "ERGRCODE", "ERGRCODENM", "ERGRCODE");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_37AAL057, "ERITEMCODE", "ERITEMCODENM", "ERITEMCODE");
        }

        private void TYACPO007I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_37AAL057, "ERYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_37AAL057, "ERCDDP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_37AAL057, "ERGRCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_37AAL057, "ERITEMCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_ERYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_ERCDDP.DummyValue = this.DTP01_ERYYMM.GetString().ToString().Substring(0,6)  + "01";

            this.SetStartingFocus(this.DTP01_ERYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_37AAL057.Initialize();
 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37AAJ055", this.DTP01_ERYYMM.GetString().ToString().Substring(0,6), this.CBH01_ERCDDP.GetValue());
            this.FPS91_TY_S_AC_37AAL057.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37AAK056", dt); //삭제          
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874"); // 저장 메세지

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
                        
            this.DataTableColumnAdd(ds.Tables[0], "ERHIGB", "A");
            this.DataTableColumnAdd(ds.Tables[0], "ERHISAB", TYUserInfo.EmpNo);            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37AAI053", ds.Tables[0]); //저장            
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion
        
        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_37AAL057.GetDataSourceInclude(TSpread.TActionType.New, "ERYYMM", "ERCDDP", "ERGRCODE", "ERITEMCODE"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
                       
            
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_37AAL057.GetDataSourceInclude(TSpread.TActionType.Remove, "ERYYMM", "ERCDDP", "ERGRCODE", "ERITEMCODE");

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

        #region  Description : DTP01_ERYYMM_ValueChanged 이벤트
        private void DTP01_ERYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_ERCDDP.DummyValue = this.DTP01_ERYYMM.GetString().ToString();
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_37AAL057_EnterCell 이벤트
        private void FPS91_TY_S_AC_37AAL057_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            if (e.Column == 1)
            {                
                string year = FPS91_TY_S_AC_37AAL057.GetValue(e.Row, "ERYYMM").ToString() + "01";
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_37AAL057, "ERCDDP");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = year;
            }
        }
        #endregion

        #region  Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO007B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
