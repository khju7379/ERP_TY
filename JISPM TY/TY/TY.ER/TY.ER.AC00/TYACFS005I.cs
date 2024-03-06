using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 개별분석 대상 거래처관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.24 19:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27O7J265 : 개별분석 대상 거래처 조회
    ///  TY_P_AC_27O7M266 : 개별분석 대상 거래처 등록
    ///  TY_P_AC_27O7M267 : 개별분석 대상 거래처 수정
    ///  TY_P_AC_27O7N268 : 개별분석 대상 거래처 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27O7O269 : 개별분석 대상 거래처 조회
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
    ///  COPY : 복사
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BVCODE : 개별분석코드
    ///  BVSAUP : 사업부
    ///  BVVEND : 거래처
    ///  BVYYMM : 기준년월
    /// </summary>
    public partial class TYACFS005I : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACFS005I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27O7O269, "BVSAUP", "BVSAUPNM", "BVSAUP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27O7O269, "BVVEND", "BVVENDNM", "BVVEND");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27O7O269, "BVCODE", "BVCODENM", "BVCODE");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27O7O269, "BVCDAC", "BVCDACNM", "BVCDAC");
        }

        private void TYACFS005I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27O7O269, "BVYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27O7O269, "BVSAUP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27O7O269, "BVSAUPNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27O7O269, "BVVEND");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27O7O269, "BVVENDNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27O7O269, "BVCDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27O7O269, "BVCDACNM");               

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_BVYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.CBH01_BVSAUP.DummyValue = this.DTP01_BVYYMM.GetValue() + "01"; 

            SetStartingFocus(DTP01_BVYYMM);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 가져오기 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS005B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27O7J265", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_27O7O269.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "BVHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "BVHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27O7M266", ds.Tables[0]); //ADD
            this.DbConnector.Attach("TY_P_AC_27O7M267", ds.Tables[1]); //UPDATE

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27O7N268", dt);

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
           

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_27O7O269.GetDataSourceInclude(TSpread.TActionType.New, "BVYYMM", "BVSAUP", "BVVEND", "BVCODE", "BVCDAC", "BVRATE", "BVAMOUNT"));

            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_27O7O269.GetDataSourceInclude(TSpread.TActionType.Update, "BVYYMM", "BVSAUP", "BVVEND", "BVCODE", "BVCDAC", "BVRATE", "BVAMOUNT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            //// 저장 체크
            //DataSet dsChk = ds.Copy();

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    iRowEqual = 0;

            //    if (ds.Tables[0].Rows[i]["BYYYMM"].ToString() != "" && ds.Tables[0].Rows[i]["BYCODE"].ToString() != "")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_AC_27N7P236", ds.Tables[0].Rows[i]["BYYYMM"].ToString(),
            //                                                    ds.Tables[0].Rows[i]["BYCODE"].ToString());
            //        iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            //        if (iRowCnt > 0)
            //        {
            //            this.ShowMessage("TY_M_GB_23S40973");
            //            e.Successed = false;
            //            return;
            //        }
            //    }

            //    for (int j = 0; j < dsChk.Tables[0].Rows.Count; j++)
            //    {
            //        if (ds.Tables[0].Rows[i]["BYYYMM"].ToString() == dsChk.Tables[0].Rows[j]["BYYYMM"].ToString() &&
            //             ds.Tables[0].Rows[i]["BYCODE"].ToString() == dsChk.Tables[0].Rows[j]["BYCODE"].ToString())
            //        {
            //            iRowEqual = iRowEqual + 1;
            //        }
            //    }

            //    if (iRowEqual > 1)
            //    {
            //        this.ShowMessage("TY_M_GB_23S40973");
            //        e.Successed = false;
            //        return;
            //    }
            //}

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            //dsChk.Dispose();

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_27O7O269.GetDataSourceInclude(TSpread.TActionType.Remove, "BVYYMM", "BVSAUP", "BVVEND", "BVCDAC");

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

        #region Description : FPS91_TY_S_AC_27O7O269_RowInserted 이벤트
        private void FPS91_TY_S_AC_27O7O269_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_27O7O269.SetValue(e.RowIndex, "BVYYMM", DateTime.Now.ToString("yyyy-MM"));
        }
        #endregion

        #region Description : DTP01_BVYYMM_ValueChanged 이벤트
        private void DTP01_BVYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_BVSAUP.DummyValue = this.DTP01_BVYYMM.GetValue() + "01";
        }
        #endregion

        #region Description : FPS91_TY_S_AC_27O7O269_EnterCell 이벤트
        private void FPS91_TY_S_AC_27O7O269_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            string year = FPS91_TY_S_AC_27O7O269.GetValue(e.Row, "BVYYMM").ToString() + "01";
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_27O7O269, "BVSAUP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

    }
}
