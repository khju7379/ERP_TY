using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 투자현황 배당내역 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.09.12 13:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_39A5S651 : EIS 투자배당내역 등록
    ///  TY_P_AC_39A5T652 : EIS 투자배당내역 수정
    ///  TY_P_AC_39A66653 : EIS 투자배당내역 삭제
    ///  TY_P_AC_39B3S670 : EIS 투자현황 배당조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_39C1S695 : EIS 투자현황 배당내역 조회
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
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ETVNCODE : 거래처
    /// </summary>
    public partial class TYACPO014I : TYBase
    {
        private string fsETVNCODE;

        #region  Description : 폼 로드 이벤트
        public TYACPO014I(string sVNCODE)
        {
            InitializeComponent();
            this.SetPopupStyle();

            this.fsETVNCODE = sVNCODE;

        }

        private void TYACPO014I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);            
            this.CBH01_ETVNCODE.SetValue(this.fsETVNCODE);

            this.CBH01_ETVNCODE.SetReadOnly(true);

            this.BTN61_INQ_Click(null, null);
            
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_39C1S695.Initialize(); 

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_39B3S670", this.CBH01_ETVNCODE.GetValue().ToString());
            this.FPS91_TY_S_AC_39C1S695.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_39C1S695.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_39C1S695, "ETDATE", "", SumRowType.Sum, "ETDIVAMOUNT");                
            }

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_39C1S695.GetDataSourceInclude(TSpread.TActionType.New, "ETVNCODE", "ETDATE", "ETDIVAMOUNT", "ETYEAR", "ETBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_AC_39C1S695.GetDataSourceInclude(TSpread.TActionType.Update, "ETVNCODE", "ETDATE", "ETDIVAMOUNT", "ETYEAR", "ETBIGO"));
            
            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 )
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

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            //저장
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_39A5S651", ds.Tables[0].Rows[i]["ETVNCODE"].ToString(),
                                                            ds.Tables[0].Rows[i]["ETDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["ETDIVAMOUNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["ETYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["ETBIGO"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_39A5T652", ds.Tables[1].Rows[i]["ETDIVAMOUNT"].ToString(),
                                                            ds.Tables[1].Rows[i]["ETYEAR"].ToString(),
                                                            ds.Tables[1].Rows[i]["ETBIGO"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["ETVNCODE"].ToString(),
                                                            ds.Tables[1].Rows[i]["ETDATE"].ToString()
                                                            );
            }          
            
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39A66653", ds.Tables[0]);
            
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_39C1S695.GetDataSourceInclude(TSpread.TActionType.Remove, "ETVNCODE", "ETDATE"));

            if (ds.Tables[0].Rows.Count == 0 )
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

            e.ArgData = ds;

        }
        #endregion

        #region Description : FPS91_TY_S_AC_39C1S695_RowInserted 이벤트
        private void FPS91_TY_S_AC_39C1S695_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_39C1S695.SetValue(e.RowIndex, "ETVNCODE", this.CBH01_ETVNCODE.GetValue());
        }
        #endregion
    }
}
