using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 조직도 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.23 13:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28N1T512 : 조직도 관리 조회
    ///  TY_P_HR_28N1W513 : 조직도체계관리 조회
    ///  TY_P_HR_28N1Y514 : 조직도체계관리 입력
    ///  TY_P_HR_28N1Y515 : 조직도체계관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_28N20517 : 조직도관리 조회
    ///  TY_S_HR_28N21518 : 조직도체계관리 조회
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
    ///  ORGADD : 조직도저장
    ///  ORGDEL : 조직도삭제
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PRIOR_ORG_CD : 상위조직코드
    ///  ENTER_CD : 회사구분
    ///  ORG_CHART_NM : 조직도명
    /// </summary>
    public partial class TYHRES002S : TYBase
    {
        private DataTable _dataSource;

        private string fsENTER_CD;
        private string fsORG_CHART_NM;
        private string fsSDATE;
        private string fsLevel;

        #region Description :  폼 Load 이벤트
        public TYHRES002S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28N21518, "ORG_CD", "ORG_CDNM", "ORG_CD");
        }

        private void TYHRES002S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_28N21518, "SEQ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_28N21518, "SDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_28N21518, "ORG_CD");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_ORGADD.ProcessCheck += new TButton.CheckHandler(BTN61_ORGADD_ProcessCheck);
            this.BTN61_ORGDEL.ProcessCheck += new TButton.CheckHandler(BTN61_ORGDEL_ProcessCheck);
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.TXT01_ENTER_CD.SetReadOnly(true);
            this.TXT01_ORG_CHART_NM.SetReadOnly(true);
            this.CBH01_PRIOR_ORG_CD.SetReadOnly(true); 

            this.BTN61_INQ_Click(null, null); 
        }
        #endregion

        #region Description :  복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            string sENTER_CD = dt.Rows[0]["ENTER_CD"].ToString();
            string sORG_CHART_NM = dt.Rows[0]["ORG_CHART_NM"].ToString();
            string sSDATE = dt.Rows[0]["SDATE"].ToString();
            string sVERSION = dt.Rows[0]["VERSION"].ToString();

            if ((new TYHRES002B(sENTER_CD, sORG_CHART_NM, sSDATE, sVERSION)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description :  조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28N1T512", "TY");
            this.FPS91_TY_S_HR_28N20517.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description :  조직도저장 버튼 이벤트
        private void BTN61_ORGADD_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28O3S544", ds.Tables[0]);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_28O3W545", ds.Tables[0].Rows[i]["EDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["ENTER_CD"].ToString(),
                                                                ds.Tables[0].Rows[i]["SDATE"].ToString());
                }
            }
            
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_ORGDEL_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28O1R543", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28N1Y515", dt);
            this.DbConnector.ExecuteTranQueryList();

            UP_RightSpreadDisplay();

            UP_TreeView_ORG(fsENTER_CD, fsORG_CHART_NM, fsSDATE);   

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {            
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28N1Y514", ds.Tables[0]); //등록            
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_RightSpreadDisplay();

            UP_TreeView_ORG(fsENTER_CD, fsORG_CHART_NM, fsSDATE);   
        }
        #endregion

        #region Description : FPS91_TY_S_HR_28N20517_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_28N20517_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_TreeView_ORG(this.FPS91_TY_S_HR_28N20517.GetValue("ENTER_CD").ToString(),
                            this.FPS91_TY_S_HR_28N20517.GetValue("ORG_CHART_NM").ToString(),
                            this.FPS91_TY_S_HR_28N20517.GetValue("SDATE").ToString());
        }
        #endregion

        #region Description : TRV01_ORG_MouseDoubleClick 이벤트
        private void TRV01_ORG_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            string sCODE = this.TRV01_ORG.SelectedNodeName;

            this.CBH01_PRIOR_ORG_CD.DummyValue = fsSDATE; 

            this.TXT01_ENTER_CD.SetValue(fsENTER_CD);
            this.TXT01_ORG_CHART_NM.SetValue(fsORG_CHART_NM);
            if (sCODE == "0")
            {
                this.CBH01_PRIOR_ORG_CD.SetValue("");
            }
            else
            {
                this.CBH01_PRIOR_ORG_CD.SetValue(sCODE);
            }

            this.FPS91_TY_S_HR_28N21518.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28N1W513", this.TXT01_ENTER_CD.GetValue(), this.TXT01_ORG_CHART_NM.GetValue(), fsSDATE, sCODE);

            DataTable dt = this.DbConnector.ExecuteDataTable();            
            this.FPS91_TY_S_HR_28N21518.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                fsLevel = dt.Rows[0]["SEQ"].ToString();  
            }            

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_HR_28N21518.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_HR_28N21518.GetDataSourceInclude(TSpread.TActionType.Remove, "ENTER_CD", "ORG_CHART_NM","ORG_CD","SDATE");

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

        #region Description : 조직도 삭제 ProcessCheck 이벤트
        private void BTN61_ORGDEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_HR_28N20517.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_HR_28N20517.GetDataSourceInclude(TSpread.TActionType.Select, "ENTER_CD", "ORG_CHART_NM", "SDATE", "VERSION");

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

        #region Description : 조직도 복사 ProcessCheck 이벤트
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {           

            DataTable dt = this.FPS91_TY_S_HR_28N20517.GetDataSourceInclude(TSpread.TActionType.Select, "ENTER_CD", "ORG_CHART_NM", "SDATE", "VERSION");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_28N21518.GetDataSourceInclude(TSpread.TActionType.New, "ENTER_CD", "ORG_CHART_NM","ORG_CD","SDATE","PRIOR_ORG_CD","SEQ","ROWNUM","DIRECT_YN","CHKID"));

            if (ds.Tables[0].Rows.Count == 0 )
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

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_ORGADD_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_28N20517.GetDataSourceInclude(TSpread.TActionType.Select, "ENTER_CD", "ORG_CHART_NM", "SDATE", "VERSION", "EDATE"));

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

        #region Description : FPS91_TY_S_HR_28N21518_RowInserted 이벤트
        private void FPS91_TY_S_HR_28N21518_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "ENTER_CD", this.TXT01_ENTER_CD.GetValue());
            this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "ORG_CHART_NM", this.TXT01_ORG_CHART_NM.GetValue());
            this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "SDATE", fsSDATE);
            this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "SEQ", fsLevel);
            if (this.CBH01_PRIOR_ORG_CD.GetValue().ToString() == "")
            {
                this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "PRIOR_ORG_CD", "0");
            }
            else
            {
                this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "PRIOR_ORG_CD", this.CBH01_PRIOR_ORG_CD.GetValue());
            }
            this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "ROWNUM", 0);
            this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "DIRECT_YN", "N");
            this.FPS91_TY_S_HR_28N21518.SetValue(e.RowIndex, "CHKID", Employer.EmpNo );

            string year = FPS91_TY_S_HR_28N21518.GetValue(e.RowIndex, "SDATE").ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_HR_28N21518, "ORG_CD");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;

        }
        #endregion

        #region Description : FPS91_TY_S_HR_28N21518_EnterCell 이벤트
        private void FPS91_TY_S_HR_28N21518_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            string year = FPS91_TY_S_HR_28N21518.GetValue(e.Row, "SDATE").ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_HR_28N21518, "ORG_CD");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;

        }
        #endregion

        #region Description : 우측 스프레드 조회 이벤트
        private void UP_RightSpreadDisplay()
        {
            this.FPS91_TY_S_HR_28N21518.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28N1W513", this.TXT01_ENTER_CD.GetValue(), this.TXT01_ORG_CHART_NM.GetValue(), fsSDATE, this.CBH01_PRIOR_ORG_CD.GetValue());
            this.FPS91_TY_S_HR_28N21518.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : TreeView 조직도 표현 이벤트
        private void UP_TreeView_ORG(string sENTER_CD, string sORG_CHART_NM, string sSDATE)
        {
            string sCODE = "";
            string sCODE_NAME = "";
            string sPARENT_CODE = "";

            this.TRV01_ORG.Initialize();

            this._dataSource = new DataTable();
            this._dataSource.Columns.Add("CODE");
            this._dataSource.Columns.Add("CODE_NAME");
            this._dataSource.Columns.Add("PARENT_CODE");

            this.DbConnector.CommandClear();

            fsENTER_CD = sENTER_CD;
            fsORG_CHART_NM = sORG_CHART_NM;
            fsSDATE = sSDATE;

            this.DbConnector.Attach("TY_P_HR_28N3N526", sENTER_CD, sORG_CHART_NM, sSDATE, sENTER_CD, sORG_CHART_NM, sSDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sCODE = dt.Rows[i]["ORG_CD"].ToString();
                    sCODE_NAME = dt.Rows[i]["ORG_CDNM"].ToString() + "[" + dt.Rows[i]["ORG_CD"].ToString() + "]";
                    sPARENT_CODE = dt.Rows[i]["PRIOR_ORG_CD"].ToString();

                    this._dataSource.Rows.Add(sCODE, sCODE_NAME, sPARENT_CODE);
                }

                this.TRV01_ORG.SetValue(new object[] { "태영인더스트리", "0", this._dataSource });
            }
        }
        #endregion
    }
}
