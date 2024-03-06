using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 사업계획 항운노조 요율.단가관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.06 10:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_796B6545 : 사업계획 항운노조 요율.단가관리 등록
    ///  TY_P_AC_796B7546 : 사업계획 항운노조 요율.단가관리 삭제
    ///  TY_P_AC_796B9547 : 사업계획 항운노조 요율.단가관리 수정
    ///  TY_P_AC_796BG548 : 사업계획 항운노조 요율.단가관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_796BH549 : 사업계획 항운노조 요율.단가관리 조회
    /// 
    ///  # 알림문자 정보 ####
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
    ///  HUCODE : 구분
    ///  HUYEAR : 년도
    /// </summary>
    public partial class TYBSHU001I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSHU001I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_796BH549, "HUCODE", "HUCODENM", "HUCODE");  
        }

        private void TYBSHU001I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_796BH549, "HUYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_796BH549, "HUCODE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_796BH549, "HUYEAR");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_796BH549, "HUCODE");


            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_HUYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_796BH549, "HUCODE");
            if (tyCodeBox != null)
            {
                tyCodeBox.DummyValue = this.TXT01_HUYEAR.GetValue().ToString() + "HU";
            }

            this.FPS91_TY_S_AC_796BH549.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_796BG548", this.TXT01_HUYEAR.GetValue().ToString());
            this.FPS91_TY_S_AC_796BH549.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_796B7546", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_796BH549.GetDataSourceInclude(TSpread.TActionType.Remove, "HUYEAR", "HUCODE");

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

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            if (ds.Tables[0].Rows.Count > 0)
            {                
                this.DataTableColumnAdd(ds.Tables[0], "HUHISAB", TYUserInfo.EmpNo);
                this.DbConnector.Attach("TY_P_AC_796B6545", ds.Tables[0]); //저장
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DataTableColumnAdd(ds.Tables[1], "HUHISAB", TYUserInfo.EmpNo);
                this.DbConnector.Attach("TY_P_AC_796B9547", ds.Tables[1]); //저장
            }

            if (this.DbConnector.CommandCount > 0 )
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");

        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_796BH549.GetDataSourceInclude(TSpread.TActionType.New, "HUYEAR", "HUCODE", "HUBSAMT", "HUEXAMT", "HUINAMT", "HUMEMO"));
            ds.Tables.Add(this.FPS91_TY_S_AC_796BH549.GetDataSourceInclude(TSpread.TActionType.Update, "HUYEAR", "HUCODE", "HUBSAMT", "HUEXAMT", "HUINAMT", "HUMEMO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

        #region  Description : FPS91_TY_S_AC_796BH549_RowInserted  이벤트
        private void FPS91_TY_S_AC_796BH549_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_796BH549.SetValue(e.RowIndex, "HUYEAR", this.TXT01_HUYEAR.GetValue().ToString());
        }
        #endregion

        #region  Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if ((new TYBSHU001B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
