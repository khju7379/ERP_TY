using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부도어음 회수관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.20 18:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25F8N480 : 받을어음 내역 등록
    ///  TY_P_AC_28K5C448 : 받을어음기타관리 삭제
    ///  TY_P_AC_28K6S456 : 부도어음 회수관리 조회
    ///  TY_P_AC_28L2A459 : 부도어음 회수내역 조회
    ///  TY_P_AC_28L2F461 : 부도어음 회수관리 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28L22458 : 부도어음 회수관리 조회
    ///  TY_S_AC_28L2B460 : 부도어음 회수내역 조회
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
    ///  E6NONR : 어음번호
    /// </summary>
    public partial class TYACEI009I : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI009I()
        {
            InitializeComponent();
        }

        private void TYACEI009I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_28L2B460, "E7NONR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_28L2B460, "E7IDBG");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_28L2B460, "E7DTBG");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.SetStartingFocus(TXT01_E6NONR);  
        }
        #endregion

        #region Description : 조회 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_28L22458.Initialize();
            this.FPS91_TY_S_AC_28L2B460.Initialize();
 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_28K6S456", this.ControlFactory, "01");

            this.FPS91_TY_S_AC_28L22458.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28K5C448", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_28L3Z466", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_AC_28L2F461", ds.Tables[1]); //수정
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");       
        }
        #endregion

        #region Description : FPS91_TY_S_AC_28L22458_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_28L22458_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sE6NONR = this.FPS91_TY_S_AC_28L22458.GetValue("E6NONR").ToString();

            this.FPS91_TY_S_AC_28L2B460.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_28L2A459", sE6NONR);

            this.FPS91_TY_S_AC_28L2B460.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : FPS91_TY_S_AC_28L2B460_RowInserted 이벤트
        private void FPS91_TY_S_AC_28L2B460_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_28L2B460.SetValue(e.RowIndex, "E7NONR", this.FPS91_TY_S_AC_28L22458.GetValue("E6NONR").ToString());
            this.FPS91_TY_S_AC_28L2B460.SetValue(e.RowIndex, "E7IDBG", "17");
            this.FPS91_TY_S_AC_28L2B460.SetValue(e.RowIndex, "E7DTBG", DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion


        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCnt = 0;

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_28L2B460.GetDataSourceInclude(TSpread.TActionType.New, "E7NONR", "E7IDBG", "E7DTBG", "E7INNR"));
            ds.Tables.Add(this.FPS91_TY_S_AC_28L2B460.GetDataSourceInclude(TSpread.TActionType.Update, "E7NONR", "E7IDBG", "E7DTBG", "E7INNR"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25G2Z500", ds.Tables[0].Rows[i]["E7NONR"].ToString(), ds.Tables[0].Rows[i]["E7IDBG"].ToString() , ds.Tables[0].Rows[i]["E7DTBG"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());  
                if ( iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_28L3G465");
                    e.Successed = false;
                    return;
                }
            }

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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_28L2B460.GetDataSourceInclude(TSpread.TActionType.Remove, "E7NONR", "E7IDBG", "E7DTBG");

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
