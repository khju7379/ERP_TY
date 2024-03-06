using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Windows.Forms;


namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 전사 조직도 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.28 14:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28S5E603 : EIS 전사 조직도 TreeView 조회
    ///  TY_P_HR_28S5J604 : EIS 전사 조직도 조회
    ///  TY_P_HR_28S5Q608 : EIS 전사 조직도 등록
    ///  TY_P_HR_28S5S609 : EIS 전사 조직도 수정
    ///  TY_P_HR_28S5S610 : EIS 전사 조직도 삭제
    ///  TY_P_HR_28S5S611 : EIS 전사 조직도 하위조직 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_28S5M606 : EIS전사조직도 상세 조회
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
    ///  PRIOR_ORG_CD : 상위조직코드
    ///  EAYYMM : 기준년월
    /// </summary>
    public partial class TYHRES004S : TYBase
    {
        private DataTable _dataSource;

        private string fsENTER_CD;
        private string fsEAYYMM;
        private string fsLevel;
        
        #region Description : 폼로드 버튼 이벤트
        public TYHRES004S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28S5M606, "ORG_CD", "ORG_CDNM", "ORG_CD");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28S5M606, "EAJKCD", "NMEAJKCD", "EAJKCD");
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28S5M606, "EAJCCD", "NMEAJCCD", "EAJCCD");
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28S5M606, "EAJCCD", "", "EAJCCD");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28S5M606, "EASABUN", "EASABUNNM", "EASABUN");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28U4F685, "EMSABUN", "EMSABUNNM", "EMSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28U4F685, "EMJKCD", "NMEMJKCD", "EMJKCD");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28U4F685, "EMJCCD", "NMEMJCCD", "EMJCCD");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_28U4F685, "EMORG_CD", "EMORG_CDNM", "EMORG_CD");

        }

        private void TYHRES004S_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateINSA = new ToolStripMenuItem("인사기본사항");
            reateINSA.Click += new EventHandler(INSAKIBON_ToolStripMenuItem_Click);

            this.FPS91_TY_S_HR_28U4F685.CurrentContextMenu.Items.AddRange(
                  new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateINSA });

            
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_28S5M606, "SEQ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_28S5M606, "ORG_CD");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);


            this.DTP01_EAYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_EMORG_CD.DummyValue = this.DTP01_EAYYMM.GetValue().ToString() + "01";   

            this.DTP01_EMYYMM.SetReadOnly(true);
            this.CBH01_EMORG_CD.SetReadOnly(true); 
            this.CBH01_PRIOR_ORG_CD.SetReadOnly(true); 
            this.CBH01_PRIOR_ORG_CD.SetReadOnly(true);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_EAYYMM);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_TreeView_ORG(this.DTP01_EAYYMM.GetValue().ToString());

            this.FPS91_TY_S_HR_28S5M606.Initialize(); 
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S5S610", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            UP_TreeView_ORG(fsEAYYMM);

            UP_RightSpreadDisplay(); 
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S5Q608", ds.Tables[0]); // 등록
            this.DbConnector.Attach("TY_P_HR_28S5S609", ds.Tables[1]); // 수정
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_TreeView_ORG(fsEAYYMM);

            UP_RightSpreadDisplay(); 
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28VAJ701", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            UP_TreeView_ORG(fsEAYYMM);

            UP_RightSpreadDisplay();

            UP_RightSpreadMember(this.CBH01_EMORG_CD.GetValue().ToString());
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28VAF699", ds.Tables[0]); // 등록
            this.DbConnector.Attach("TY_P_HR_28VAH700", ds.Tables[1]); // 수정
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_TreeView_ORG(fsEAYYMM);

            UP_RightSpreadDisplay();

            UP_RightSpreadMember(this.CBH01_EMORG_CD.GetValue().ToString());
        }
        #endregion

        #region Description : 복사 버튼  이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRES004B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion


        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_28S5M606.GetDataSourceInclude(TSpread.TActionType.New, "EAYYMM", "ENTER_CD", "SEQ", "ORG_CD", "PRIOR_ORG_CD", "ROWNUM","COLNUM", "EAJKCD", "NMEAJKCD", "EAJCCD", "NMEAJCCD", "EASABUN", "CHKID"));
            ds.Tables.Add(this.FPS91_TY_S_HR_28S5M606.GetDataSourceInclude(TSpread.TActionType.Update, "ROWNUM","COLNUM", "EAJKCD", "NMEAJKCD", "EAJCCD", "NMEAJCCD", "EASABUN", "CHKID", "EAYYMM", "ENTER_CD", "ORG_CD"));
            

            if (ds.Tables[0].Rows.Count == 0 & ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_28U1M667", ds.Tables[0].Rows[i]["EAYYMM"].ToString(), ds.Tables[0].Rows[i]["ENTER_CD"].ToString(), ds.Tables[0].Rows[i]["ORG_CD"].ToString());
                Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                //if (iCnt > 0)
                //{
                //    this.ShowMessage("TY_M_GB_23S40973");
                //    e.Successed = false;
                //    return;
                //}
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
            if (this.FPS91_TY_S_HR_28S5M606.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_HR_28S5M606.GetDataSourceInclude(TSpread.TActionType.Remove, "EAYYMM", "ENTER_CD", "ORG_CD");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_28S5S611", dt.Rows[i]["EAYYMM"].ToString(), dt.Rows[i]["ENTER_CD"].ToString(), dt.Rows[i]["ORG_CD"].ToString());
                Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_HR_28T1D625");
                    e.Successed = false;
                    return;
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

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_28U4F685.GetDataSourceInclude(TSpread.TActionType.New, "EMYYMM", "EMENTER_CD", "EMPRIOR_ORG_CD", "EMORG_CD", "EMSABUN", "EMJKCD", "NMEMJKCD", "EMJCCD", "NMEMJCCD", "EMROWNUM", "EMBIGO", "EMCHKID"));
            ds.Tables.Add(this.FPS91_TY_S_HR_28U4F685.GetDataSourceInclude(TSpread.TActionType.Update, "EMYYMM", "EMENTER_CD", "EMPRIOR_ORG_CD", "EMORG_CD", "EMSABUN", "EMJKCD", "NMEMJKCD", "EMJCCD", "NMEMJCCD", "EMROWNUM", "EMBIGO", "EMCHKID"));


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
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_HR_28U4F685.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_HR_28U4F685.GetDataSourceInclude(TSpread.TActionType.Remove, "EMYYMM", "EMENTER_CD", "EMPRIOR_ORG_CD", "EMORG_CD", "EMSABUN");

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


        #region Description : TreeView 조직도 표현 이벤트
        private void UP_TreeView_ORG(string sEAYYMM )
        {
            string sCODE = "";
            string sCODE_NAME = "";
            string sPARENT_CODE = "";

            fsEAYYMM = sEAYYMM;
            fsENTER_CD = "TY";

            this.TRV01_ORG.Initialize();

            this._dataSource = new DataTable();
            this._dataSource.Columns.Add("CODE");
            this._dataSource.Columns.Add("CODE_NAME");
            this._dataSource.Columns.Add("PARENT_CODE");

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_28S5E603", "TY", sEAYYMM,  "TY", sEAYYMM);            
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

        #region Description : TRV01_ORG_MouseDoubleClick 이벤트
        private void TRV01_ORG_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            string sCODE = this.TRV01_ORG.SelectedNodeName;

            this.CBH01_PRIOR_ORG_CD.DummyValue = fsEAYYMM+"01";

            if (sCODE == "0")
            {
                this.CBH01_PRIOR_ORG_CD.SetValue("");
            }
            else
            {
                this.CBH01_PRIOR_ORG_CD.SetValue(sCODE);
            }

            this.FPS91_TY_S_HR_28S5M606.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S5J604", fsEAYYMM, fsENTER_CD, sCODE);

            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_28S5M606.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                fsLevel = dt.Rows[0]["SEQ"].ToString();
            }
            else
            {
                //하위부서가 없으면 현재부서의 레벨을 가져온다
                dt.Clear(); 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_28T4S631", fsEAYYMM, fsENTER_CD, sCODE);
                dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    fsLevel =  Convert.ToString(Convert.ToInt16(dt.Rows[0]["SEQ"].ToString())+1);
                }
            }

            //우측 인원현황 조회
            UP_RightSpreadMember(sCODE);            
        }
        #endregion

        #region Description : FPS91_TY_S_HR_28S5M606_RowInserted 이벤트
        private void FPS91_TY_S_HR_28S5M606_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_28S5M606.SetValue(e.RowIndex, "EAYYMM", fsEAYYMM);
            this.FPS91_TY_S_HR_28S5M606.SetValue(e.RowIndex, "ENTER_CD", fsENTER_CD);
            this.FPS91_TY_S_HR_28S5M606.SetValue(e.RowIndex, "SEQ", fsLevel);
            if (this.CBH01_PRIOR_ORG_CD.GetValue().ToString() == "")
            {
                this.FPS91_TY_S_HR_28S5M606.SetValue(e.RowIndex, "PRIOR_ORG_CD", "0");
            }
            else
            {
                this.FPS91_TY_S_HR_28S5M606.SetValue(e.RowIndex, "PRIOR_ORG_CD", this.CBH01_PRIOR_ORG_CD.GetValue());
            }

            this.FPS91_TY_S_HR_28S5M606.SetValue(e.RowIndex, "CHKID", Employer.EmpNo);

            string year = FPS91_TY_S_HR_28S5M606.GetValue(e.RowIndex, "EAYYMM").ToString()+"01";
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_HR_28S5M606, "ORG_CD");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : 우측 스프레드 조회 이벤트
        private void UP_RightSpreadDisplay()
        {
            string sCode = "";

            if( this.CBH01_PRIOR_ORG_CD.GetValue().ToString() == "" )
            {
                sCode= "0";
            }
            else
            {
                sCode = this.CBH01_PRIOR_ORG_CD.GetValue().ToString();
            }

            this.FPS91_TY_S_HR_28S5M606.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28S5J604", fsEAYYMM, fsENTER_CD, sCode);

            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_28S5M606.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                fsLevel = dt.Rows[0]["SEQ"].ToString();
            }            

        }
        #endregion

        #region Description : 우측 스프레드 인원 조회 이벤트
        private void UP_RightSpreadMember(string sCODE)
        {
            this.DTP01_EMYYMM.SetValue(fsEAYYMM);
            this.CBH01_EMORG_CD.SetValue(sCODE);

            this.FPS91_TY_S_HR_28U4F685.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28U4E684", this.DTP01_EMYYMM.GetValue(), fsENTER_CD, this.CBH01_EMORG_CD.GetValue());

            DataTable dtMem = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_28U4F685.SetValue(dtMem);
        }
        #endregion

        #region Description : DTP01_EMYYMM_ValueChanged 이벤트
        private void DTP01_EMYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_EMORG_CD.DummyValue = this.DTP01_EMYYMM.GetValue().ToString() + "01";
        }
        #endregion

        #region Description : FPS91_TY_S_HR_28U4F685_RowInserted 이벤트
        private void FPS91_TY_S_HR_28U4F685_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMYYMM", this.DTP01_EMYYMM.GetValue());
            this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMENTER_CD", fsENTER_CD);            
            this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMORG_CD", this.CBH01_EMORG_CD.GetValue());
            this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMCHKID", Employer.EmpNo);
                        
            //소속 사업부
            string sSAUPCODE = this.CBH01_EMORG_CD.GetValue().ToString();
            if (sSAUPCODE.Substring(0, 1) == "T")
            {
                this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMPRIOR_ORG_CD", "T00000");
            }
            else if (sSAUPCODE.Substring(0, 1) == "S")
            {
                this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMPRIOR_ORG_CD", "S00000");
            }
            else if (sSAUPCODE.Substring(0, 1) == "B")
            {
                this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMPRIOR_ORG_CD", "B00000");
            }
            else if (sSAUPCODE.Substring(0, 2) != "A5")
            {
                this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMPRIOR_ORG_CD", "A10000");
            }
            else if (sSAUPCODE.Substring(0, 2) == "A5")
            {
                this.FPS91_TY_S_HR_28U4F685.SetValue(e.RowIndex, "EMPRIOR_ORG_CD", "A50000");
            }

        }
        #endregion

        #region  Description : 인사기본사항 처리 팝업 이벤트
        private void INSAKIBON_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB002I(this.FPS91_TY_S_HR_28U4F685.GetValue("EMSABUN").ToString()
                     )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion




    }
}
