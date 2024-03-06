using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자본금 배부관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.04 18:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2946U763 : 자본금 배부관리 조회
    ///  TY_P_AC_2946V764 : 자본금 배부관리 입력
    ///  TY_P_AC_2946Y765 : 자본금 배부관리 수정
    ///  TY_P_AC_2946Y766 : 자본금 배부관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29470767 : 자본금 배부관리 조회
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
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC017S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACNC017S()
        {
            InitializeComponent();
        }

        private void TYACNC017S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_29470767, "AYFYYMM");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_29470767, "AYFYYMM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_29470767, "AYFYLTT");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_29470767, "AYFYLSS");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_29470767, "AYRATETOTAL");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            
            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2946U763", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_29470767.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2946Y766", dt);
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

            ds.Tables[0].Columns.Remove("AYFYYMM");  
            this.DbConnector.Attach("TY_P_AC_2946V764", ds.Tables[0]); // 저장
            this.DbConnector.Attach("TY_P_AC_2946Y765", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_29470767.GetDataSourceInclude(TSpread.TActionType.New, "AYFYYMM", "AYFYEAR", "AYFMONTH", "AYFYLTT", "AYFYLSS" ));
            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_29470767.GetDataSourceInclude(TSpread.TActionType.Update, "AYFYYMM","AYFYLTT", "AYFYLSS"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            decimal dRateTotal = 0;

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dRateTotal = Convert.ToDecimal(ds.Tables[0].Rows[i]["AYFYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["AYFYLSS"].ToString());
                if (dRateTotal != 100)
                {
                    this.ShowMessage("TY_M_AC_2943X749");
                    e.Successed = false;
                    return;
                }
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2947N779", ds.Tables[0].Rows[i]["AYFYYMM"].ToString().Substring(0,4) , ds.Tables[0].Rows[i]["AYFYYMM"].ToString().Substring(4,2)); // 저장
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_28D5W379");
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dRateTotal = Convert.ToDecimal(ds.Tables[1].Rows[i]["AYFYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["AYFYLSS"].ToString());
                if (dRateTotal != 100)
                {
                    this.ShowMessage("TY_M_AC_2943X749");
                    e.Successed = false;
                    return;
                }
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
            DataTable dt = this.FPS91_TY_S_AC_29470767.GetDataSourceInclude(TSpread.TActionType.Remove, "AYFYYMM");

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

        #region Description : FPS91_TY_S_AC_29470767_RowInserted 이벤트
        private void FPS91_TY_S_AC_29470767_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_29470767.SetValue(e.RowIndex, "AYFYLTT", 0.00);
            this.FPS91_TY_S_AC_29470767.SetValue(e.RowIndex, "AYFYLSS", 0.00);
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29470767_EnterCell 이벤트
        private void FPS91_TY_S_AC_29470767_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            this.FPS91_TY_S_AC_29470767.SetValue(e.Row, "AYFYEAR", FPS91_TY_S_AC_29470767.GetValue(e.Row, "AYFYYMM").ToString().Substring(0, 4));
            this.FPS91_TY_S_AC_29470767.SetValue(e.Row, "AYFMONTH", FPS91_TY_S_AC_29470767.GetValue(e.Row, "AYFYYMM").ToString().Substring(4, 2));

            //사업부 합계
            decimal dSaupTotal = 0;
            dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_29470767.GetValue(e.Row, "AYFYLTT").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_29470767.GetValue(e.Row, "AYFYLSS").ToString());

            this.FPS91_TY_S_AC_29470767.SetValue(e.Row, "AYRATETOTAL", dSaupTotal);
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29470767_EnterCell 이벤트
        private void FPS91_TY_S_AC_29470767_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //사업부 합계
            decimal dSaupTotal = 0;
            dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_29470767.GetValue(e.Row, "AYFYLTT").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_29470767.GetValue(e.Row, "AYFYLSS").ToString());

            this.FPS91_TY_S_AC_29470767.SetValue(e.Row, "AYRATETOTAL", dSaupTotal);

        }
        #endregion
    }
}
