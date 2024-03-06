using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여항목별 급여코드관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.11 13:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CBDU705 : 급여항목별급여코드관리 등록
    ///  TY_P_HR_4CBDV706 : 급여항목별급여코드관리 수정
    ///  TY_P_HR_4CBDV707 : 급여항목별급여코드관리 삭제
    ///  TY_P_HR_4CBDX708 : 급여항목별급여코드관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CBDY710 : 급여항목별급여코드관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_HR_4CBDI704 : 동일한 급여코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PIGUBN : 급여구분
    ///  PIPAYCODE : 급여코드
    /// </summary>
    public partial class TYHRPY004I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY004I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBDY710, "PIGUBN", "PIGUBNNM", "PIGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBDY710, "PIPAYCODE", "PIPAYCODENM", "PIPAYCODE");

        }

        private void TYHRPY004I_Load(object sender, System.EventArgs e)
        {            
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBDY710, "PIGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBDY710, "PIPAYCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBDY710, "PISDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBDY710, "PIGUBN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBDY710, "PIGUBNNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBDY710, "PIPAYCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.UP_GetGridBinding();            
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.UP_GetGridBinding();
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBDV707", dt);  //삭제
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataTable dt = this.FPS91_TY_S_HR_4CBDY710.GetDataSourceInclude(TSpread.TActionType.Remove, "PIGUBN", "PIPAYCODE", "PISDATE");
            
            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CBFG719", dt.Rows[i]["PIPAYCODE"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("급여지급내역이 존재합니다! 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
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

            this.DataTableColumnAdd(ds.Tables[0], "PIHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PIHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CBDU705", ds.Tables[0].Rows[i]["PIGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PIPAYCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PISDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PIEDATE"].ToString().Replace("19000101","").ToString(),
                                                                ds.Tables[0].Rows[i]["PIHISAB"].ToString()
                                                                );  //등록
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CBDV706", ds.Tables[1].Rows[i]["PIEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[1].Rows[i]["PIHISAB"].ToString(),
                                                                ds.Tables[1].Rows[i]["PIGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["PIPAYCODE"].ToString(),
                                                                ds.Tables[1].Rows[i]["PISDATE"].ToString()
                                                                );
                }
            }           

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");   
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();
           
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBDY710.GetDataSourceInclude(TSpread.TActionType.New, "PIGUBN", "PIPAYCODE", "PISDATE", "PIEDATE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBDY710.GetDataSourceInclude(TSpread.TActionType.Update, "PIGUBN", "PIPAYCODE", "PISDATE", "PIEDATE"));           

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                //Key 체크 및 시작일자 체크
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["PISDATE"].ToString().Replace("19000101", "").ToString() == "")
                    {
                        this.ShowCustomMessage("시작일자를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CBFN721", ds.Tables[0].Rows[i]["PIGUBN"].ToString(), ds.Tables[0].Rows[i]["PIPAYCODE"].ToString(), ds.Tables[0].Rows[i]["PISDATE"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("동일한 급여코드가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  Description : 그리드 조회 
        private void UP_GetGridBinding()
        {
            this.FPS91_TY_S_HR_4CBDY710.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBDX708", this.CBH01_PIGUBN.GetValue().ToString() );
            this.FPS91_TY_S_HR_4CBDY710.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 스프레드 포커스 이동 이벤트
        private void FPS91_TY_S_HR_4CBDY710_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == 5)
            {
                if (this.FPS91_TY_S_HR_4CBDY710_Sheet1.Cells[e.Row, 5].Text.ToString() == "" || this.FPS91_TY_S_HR_4CBDY710_Sheet1.Cells[e.Row, 5].Text.ToString() == "1900-01-01")
                {
                    this.FPS91_TY_S_HR_4CBDY710_Sheet1.Cells[e.Row, 5].Value = "";
                }
            }
        }
        #endregion


    }
}
