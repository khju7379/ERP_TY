using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 부서코드 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.28 09:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28S9Q562 : 조직도 부서코드 조회
    ///  TY_P_HR_28S9S563 : 조직도 부서코드 등록
    ///  TY_P_HR_28S9T564 : 조직도 부서코드 수정
    ///  TY_P_HR_28S9V565 : 조직도 부서코드 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_28S9V568 : 조직도 부서코드 조회
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
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRES003S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYHRES003S()
        {
            InitializeComponent();
        }

        private void TYHRES003S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_28S9V568, "ORG_CD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_28S9V568, "SDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_28S9V568, "ORG_CD");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_28S9V568, "SDATE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_28S9V568, "EDATE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.TXT01_ORG_CD); 
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S9Q562", "TY", this.DTP01_SDATE.GetValue(), this.TXT01_ORG_CD.GetValue() );
            this.FPS91_TY_S_HR_28S9V568.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S9V565", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null); 
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S9S563", ds.Tables[0]); // 등록            
            this.DbConnector.Attach("TY_P_HR_28S9T564", ds.Tables[1]); // 수정
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_INQ_Click(null, null); 

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_28S9V568.GetDataSourceInclude(TSpread.TActionType.New, "ENTER_CD", "ORG_CD", "SDATE", "ORG_NM", "ORG_FULL_NM", "ORG_ENG_NM","CHKID"));
            ds.Tables.Add(this.FPS91_TY_S_HR_28S9V568.GetDataSourceInclude(TSpread.TActionType.Update, "ENTER_CD", "ORG_CD", "SDATE","EDATE", "ORG_NM", "ORG_FULL_NM", "ORG_ENG_NM", "CHKID"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_28SAH583", ds.Tables[0].Rows[i]["ENTER_CD"].ToString(), ds.Tables[0].Rows[i]["ORG_CD"].ToString(), ds.Tables[0].Rows[i]["SDATE"].ToString());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_AC_2733W949");
                        e.Successed = false;
                        return;
                    }
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
            if (this.FPS91_TY_S_HR_28S9V568.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_HR_28S9V568.GetDataSourceInclude(TSpread.TActionType.Remove, "ENTER_CD", "ORG_CD", "SDATE");

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

        #region Description : FPS91_TY_S_HR_28S9V568_RowInserted 이벤트
        private void FPS91_TY_S_HR_28S9V568_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_28S9V568.SetValue(e.RowIndex, "ENTER_CD", "TY");
        }
        #endregion
    }
}
