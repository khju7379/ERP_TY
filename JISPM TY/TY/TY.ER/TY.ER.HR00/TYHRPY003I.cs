using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여코드기본사항관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.10 19:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CAJL681 : 급여코드기본사항 등록
    ///  TY_P_HR_4CAJM682 : 급여코드기본사항 수정
    ///  TY_P_HR_4CAJM683 : 급여코드기본사항 삭제
    ///  TY_P_HR_4CAJW690 : 급여코드기본사항 TREE 조회
    ///  TY_P_HR_4CAJY691 : 급여코드기본사항관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CAJY692 : 급여코드기본사항관리 조회(지급)
    ///  TY_S_HR_4CAJZ693 : 급여코드기본사항관리 조회(공제)
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
    /// </summary>
    public partial class TYHRPY003I : TYBase
    {
        private DataTable _dataSource;

        private string fsNodeCode;
        private string fsNodeGroup;

        #region  Description : 폼 로드 이벤트
        public TYHRPY003I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CAJY692, "PSDCODE", "PSDNAME", "PSDCODE");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CAJY692, "PSDGROPCODE", "PSDGROPCODENM", "PSDGROPCODE");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CAJZ693, "PSDCODE", "PSDNAME", "PSDCODEMI");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CAJZ693, "PSDGROPCODE", "PSDGROPCODENM", "PSDGROPCODEMI");
        }

        private void TYHRPY003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CAJY692, "PSDCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CAJZ693, "PSDCODE");

            this.UP_TreeView_ORG();

            this.UP_SetPayCodeHelp(true, false);

            this.FPS91_TY_S_HR_4CAJY692.Visible = true;
            this.FPS91_TY_S_HR_4CAJZ693.Visible = false;

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.UP_TreeView_ORG();

            if (this.CBH01_PSDCODE.Visible)
            {
                this.FPS91_TY_S_HR_4CAJY692.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CAJY691", "1", this.CBH01_PSDCODE.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                this.FPS91_TY_S_HR_4CAJY692.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_HR_4CAJZ693.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CAJY691", "2", this.CBH01_PSDCODE.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                this.FPS91_TY_S_HR_4CAJZ693.SetValue(dt);
            }


        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CAJM683", dt);  //삭제
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.FPS91_TY_S_HR_4CAJY692.Visible == true)
            {
                dt = this.FPS91_TY_S_HR_4CAJY692.GetDataSourceInclude(TSpread.TActionType.Remove, "PSDCODE");
            }
            else
            {
                dt = this.FPS91_TY_S_HR_4CAJZ693.GetDataSourceInclude(TSpread.TActionType.Remove, "PSDCODE");
            }
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


            this.DataTableColumnAdd(ds.Tables[0], "PSDHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PSDHISAB", TYUserInfo.EmpNo);

            //지급
            this.DbConnector.CommandClear();
            if (this.FPS91_TY_S_HR_4CAJY692.Visible == true)
            {
                this.DbConnector.Attach("TY_P_HR_4CAJL681", ds.Tables[0]);  //등록
                this.DbConnector.Attach("TY_P_HR_4CAJM682", ds.Tables[1]);  //수정
            }
            else
            {
                //공제
                this.DbConnector.Attach("TY_P_HR_4CALI697", ds.Tables[0]);  //등록
                this.DbConnector.Attach("TY_P_HR_4CALJ698", ds.Tables[1]);  //수정
            }
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");            
        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            //지급
            if (this.FPS91_TY_S_HR_4CAJY692.Visible == true)
            {
                ds.Tables.Add(this.FPS91_TY_S_HR_4CAJY692.GetDataSourceInclude(TSpread.TActionType.New, "PSDCODE", "PSDNAME", "PSDRPNAME", "PSDORDGN", "PSDAVGGN", "PSDSOKGN", "PSDOTGN", "PSDMONAVGGN", "PSDYUNCHAGN","PSDUNIONGN","PSDSOCGN", "PSDGROPCODE", "PSDJIKYN"));
                ds.Tables.Add(this.FPS91_TY_S_HR_4CAJY692.GetDataSourceInclude(TSpread.TActionType.Update, "PSDCODE", "PSDNAME", "PSDRPNAME", "PSDORDGN", "PSDAVGGN", "PSDSOKGN", "PSDOTGN", "PSDMONAVGGN", "PSDYUNCHAGN", "PSDUNIONGN","PSDSOCGN", "PSDGROPCODE", "PSDJIKYN"));
            }
            else
            {
                //공제
                ds.Tables.Add(this.FPS91_TY_S_HR_4CAJZ693.GetDataSourceInclude(TSpread.TActionType.New, "PSDCODE", "PSDNAME", "PSDRPNAME", "PSDICTAXGN", "PSDRSTAXGN", "PSDINSGN", "PSDSANGJOGN", "PSDNOJOGN", "PSDTAXADJGN", "PSDGROPCODE", "PSDJIKYN"));
                ds.Tables.Add(this.FPS91_TY_S_HR_4CAJZ693.GetDataSourceInclude(TSpread.TActionType.Update, "PSDCODE", "PSDNAME", "PSDRPNAME", "PSDICTAXGN", "PSDRSTAXGN", "PSDINSGN", "PSDSANGJOGN", "PSDNOJOGN", "PSDTAXADJGN", "PSDGROPCODE", "PSDJIKYN"));
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                //등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CBDF703", ds.Tables[0].Rows[i]["PSDCODE"].ToString());  //등록
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_HR_4CBDI704");
                        e.Successed = false;
                        return;
                    }                  
                    if (this.FPS91_TY_S_HR_4CAJY692.Visible == true)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_53A9J582", ds.Tables[0].Rows[i]["PSDCODE"].ToString());  //등록
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt > 0)
                        {
                            if (ds.Tables[0].Rows[i]["PSDORDGN"].ToString() == "Y" ||
                                ds.Tables[0].Rows[i]["PSDAVGGN"].ToString() == "Y" ||
                                ds.Tables[0].Rows[i]["PSDSOKGN"].ToString() == "Y" ||
                                ds.Tables[0].Rows[i]["PSDOTGN"].ToString() == "Y" ||
                                ds.Tables[0].Rows[i]["PSDMONAVGGN"].ToString() == "Y" ||
                                ds.Tables[0].Rows[i]["PSDYUNCHAGN"].ToString() == "Y" ||
                                ds.Tables[0].Rows[i]["PSDUNIONGN"].ToString() == "Y" ||
                                ds.Tables[0].Rows[i]["PSDSOCGN"].ToString() == "Y")
                            {
                                this.ShowCustomMessage("하위코드 존재시 옵션선택을 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        //하위코드가 존재하면 실지급코드는 Y가 될수 없다
                        if (ds.Tables[0].Rows[i]["PSDJIKYN"].ToString() == "Y")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_53A9J582", ds.Tables[0].Rows[i]["PSDCODE"].ToString());  //등록
                            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                            if (iCnt > 0)
                            {
                                this.ShowCustomMessage("하위코드 존재시 실지급코드가 될수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                    }
                }

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (this.FPS91_TY_S_HR_4CAJY692.Visible == true)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_53A9J582", ds.Tables[1].Rows[i]["PSDCODE"].ToString());  //등록
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt > 0)
                        {
                            if (ds.Tables[1].Rows[i]["PSDORDGN"].ToString() == "Y" ||
                                ds.Tables[1].Rows[i]["PSDAVGGN"].ToString() == "Y" ||
                                ds.Tables[1].Rows[i]["PSDSOKGN"].ToString() == "Y" ||
                                ds.Tables[1].Rows[i]["PSDOTGN"].ToString() == "Y" ||
                                ds.Tables[1].Rows[i]["PSDMONAVGGN"].ToString() == "Y" ||
                                ds.Tables[1].Rows[i]["PSDYUNCHAGN"].ToString() == "Y" ||
                                ds.Tables[1].Rows[i]["PSDUNIONGN"].ToString() == "Y" ||
                                ds.Tables[1].Rows[i]["PSDSOCGN"].ToString() == "Y")
                            {
                                this.ShowCustomMessage("하위코드 존재시 옵션선택을 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        //하위코드가 존재하면 실지급코드는 Y가 될수 없다
                        if (ds.Tables[1].Rows[i]["PSDJIKYN"].ToString() == "Y")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_53A9J582", ds.Tables[1].Rows[i]["PSDCODE"].ToString());  //등록
                            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                            if (iCnt > 0)
                            {
                                this.ShowCustomMessage("하위코드 존재시 실지급코드가 될수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
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

        #region Description : TreeView 표현 이벤트
        private void UP_TreeView_ORG()
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
            this.DbConnector.Attach("TY_P_HR_4CAJW690");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sCODE = dt.Rows[i]["PSDCODE"].ToString();
                    sCODE_NAME = dt.Rows[i]["PSDNAME"].ToString() + "[" + dt.Rows[i]["PSDCODE"].ToString() + "]";
                    sPARENT_CODE = dt.Rows[i]["INDEXCODE"].ToString();

                    this._dataSource.Rows.Add(sCODE, sCODE_NAME, sPARENT_CODE);
                }

                this.TRV01_ORG.SetValue(new object[] { "급여코드기본관리", "", this._dataSource });
            }
        }
        #endregion

        #region Description : Tree 마우스 더블클릭 이벤트
        private void TRV01_ORG_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Int16 iCnt = 0;
            string sNodeGroup = string.Empty;
            string sNodeCODE = this.TRV01_ORG.SelectedNodeName;

            if (sNodeCODE == "") return;
            
            if (sNodeCODE.Substring(0, 1) == "1" || sNodeCODE.Substring(0, 2) == "PL")  //지급
            {
                this.UP_SetPayCodeHelp(true, false);

                
                this.CBH01_PSDCODE.SetValue(sNodeCODE.Substring(0, 1) == "1" ? sNodeCODE : "");

                this.FPS91_TY_S_HR_4CAJZ693.Visible = false;
                this.FPS91_TY_S_HR_4CAJY692.Visible = true;

                sNodeGroup = sNodeCODE;

                if (sNodeCODE.Substring(0, 2) == "PL")
                {
                    sNodeGroup = "";
                    sNodeCODE = "1";
                }
                
                this.FPS91_TY_S_HR_4CAJY692.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CAJY691",sNodeCODE.Substring(0, 1), sNodeGroup);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                this.FPS91_TY_S_HR_4CAJY692.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_HR_4CAJY692.ActiveSheet.RowCount; i++)
                {

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53A9J582", this.FPS91_TY_S_HR_4CAJY692.ActiveSheet.Cells[i, 0].Text);  
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.FPS91_TY_S_HR_4CAJY692.ActiveSheet.Rows[i].Locked = true;
                        this.FPS91_TY_S_HR_4CAJY692.ActiveSheet.Rows[i].BackColor = Color.FromArgb(223, 229, 215);           
                    }
                }
               
            }
            else //공제
            {
                this.UP_SetPayCodeHelp(false, true);

                this.CBH01_PSDCODEMI.SetValue(sNodeCODE.Substring(0, 1) == "2" ? sNodeCODE : "");

                this.FPS91_TY_S_HR_4CAJZ693.Visible = true;
                this.FPS91_TY_S_HR_4CAJY692.Visible = false;

                sNodeGroup = sNodeCODE;

                if (sNodeCODE.Substring(0, 2) == "MI")
                {
                    sNodeGroup = "";
                    sNodeCODE = "2";
                }

                this.FPS91_TY_S_HR_4CAJZ693.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CAJY691", sNodeCODE.Substring(0, 1), sNodeGroup);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                this.FPS91_TY_S_HR_4CAJZ693.SetValue(dt);
            }

            fsNodeCode = sNodeCODE;
            fsNodeGroup = sNodeGroup;
        }
        #endregion

        #region Description : 급여코드 표현 함수
        private void UP_SetPayCodeHelp(bool bvalue1, bool bvalue2)
        {
            this.LBL51_PSDCODE.Visible = bvalue1;
            this.CBH01_PSDCODE.Visible = bvalue1;
            this.LBL51_PSDCODEMI.Visible = bvalue2;
            this.CBH01_PSDCODEMI.Visible = bvalue2;
        }
        #endregion

        #region Description : 스프레드 추가 이벤트
        private void FPS91_TY_S_HR_4CAJY692_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDORDGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDAVGGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDSOKGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDOTGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDMONAVGGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDYUNCHAGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDUNIONGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDSOCGN", "N");
            this.FPS91_TY_S_HR_4CAJY692.SetValue(e.RowIndex, "PSDJIKYN", "N");            
        }       

        private void FPS91_TY_S_HR_4CAJZ693_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CAJZ693.SetValue(e.RowIndex, "PSDICTAXGN", "N");
            this.FPS91_TY_S_HR_4CAJZ693.SetValue(e.RowIndex, "PSDRSTAXGN", "N");
            this.FPS91_TY_S_HR_4CAJZ693.SetValue(e.RowIndex, "PSDINSGN", "N");
            this.FPS91_TY_S_HR_4CAJZ693.SetValue(e.RowIndex, "PSDSANGJOGN", "N");
            this.FPS91_TY_S_HR_4CAJZ693.SetValue(e.RowIndex, "PSDNOJOGN", "N");
            this.FPS91_TY_S_HR_4CAJZ693.SetValue(e.RowIndex, "PSDTAXADJGN", "N");
            this.FPS91_TY_S_HR_4CAJZ693.SetValue(e.RowIndex, "PSDJIKYN", "N");           

        }
        #endregion

    }
}
