using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 출입자 관리(공사) 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.04.02 16:31
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_842GN788 : 출입자 관리(공사) 마스타 조회
    ///  TY_P_HR_84OF4865 : 출입자 관리(공사) 공사진행중 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_842HV794 : 출입자 관리(공사) 마스타 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CIWORKBTN : 공사진행조회
    ///  INQ : 조회
    ///  CICGUBUN : 출입장구분
    ///  CIGUBUN : 작업구분
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRGB001S : TYBase
    {
        #region Description : 폼 로드
        public TYHRGB001S()
        {
            InitializeComponent();
        }

        private void TYHRGB001S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_HR_842HV794.Sheets[0].Columns[20].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_842HV794, "PRT");
            //this.FPS91_TY_S_HR_842HV794.Initialize();
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyyMMdd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.CBO01_CIWORKGUBN.SetValue("G");
            this.BTN61_INQ_Click(null, null);
            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 공사진행조회 버튼
        private void BTN61_CIWORKBTN_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (this.CBO01_CIWORKGUBN.GetValue().ToString() == "G")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_84OF4865",
                    //this.DTP01_STDATE.GetString()
                    DateTime.Now.ToString("yyyyMMdd")
                    );
            }
            else
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_842GN788",
                    this.DTP01_STDATE.GetString(),
                    this.DTP01_EDDATE.GetString(),
                    this.CBO01_CIGUBUN.GetValue().ToString(),
                    this.CBO01_CICGUBUN.GetValue().ToString()
                    );

            }
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_842HV794.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_842HV794.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_842HV794.GetValue(i, "VNSANGHO").ToString() == "")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2445D438", this.FPS91_TY_S_HR_842HV794.GetValue(i, "CIVEND").ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_HR_842HV794.ActiveSheet.Cells[i, 5].Text = dt.Rows[0]["VNSANGHO"].ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 그리드 더블 클릭
        private void FPS91_TY_S_HR_842HV794_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRGB001I(this.FPS91_TY_S_HR_842HV794.GetValue("CIDATE").ToString(), this.FPS91_TY_S_HR_842HV794.GetValue("CISEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRGB001I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 출입허가서 출력 버튼
        private void FPS91_TY_S_HR_842HV794_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "20")
            {
                this.DbConnector.Attach
                        (
                        "TY_P_HR_85FL0044",
                        this.FPS91_TY_S_HR_842HV794.GetValue("CIDATE").ToString().Replace("-", ""),
                        this.FPS91_TY_S_HR_842HV794.GetValue("CISEQ").ToString()
                        );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYHRGB001R();

                // 세로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                if (dt.Rows.Count > 0)
                {
                    (new TYERGB001P(rpt, UP_DatatableChange(dt))).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : 출력 데이터 변환
        private DataTable UP_DatatableChange(DataTable dt)
        {
            DataTable dtRtn = new DataTable();
            DataTable dtVend = new DataTable();

            dtRtn.Columns.Add("CISDATE", typeof(System.String));
            dtRtn.Columns.Add("CIDATE", typeof(System.String));
            dtRtn.Columns.Add("CIVEND", typeof(System.String));
            dtRtn.Columns.Add("CIYSSTIME", typeof(System.String));
            dtRtn.Columns.Add("CIYSETIME", typeof(System.String));
            dtRtn.Columns.Add("CIWORK", typeof(System.String));
            dtRtn.Columns.Add("CIPLACE", typeof(System.String));
            dtRtn.Columns.Add("CIWKLIST", typeof(System.String));
            dtRtn.Columns.Add("CLNAME1", typeof(System.String));
            dtRtn.Columns.Add("CLJUMIN1", typeof(System.String));
            dtRtn.Columns.Add("CLJUSO1", typeof(System.String));
            dtRtn.Columns.Add("CLNAME2", typeof(System.String));
            dtRtn.Columns.Add("CLJUMIN2", typeof(System.String));
            dtRtn.Columns.Add("CLJUSO2", typeof(System.String));
            dtRtn.Columns.Add("CICSABUN", typeof(System.String));
            dtRtn.Columns.Add("CICNAME", typeof(System.String));
            dtRtn.Columns.Add("CISNAME", typeof(System.String));
            dtRtn.Columns.Add("CISJIKCHK", typeof(System.String));
            dtRtn.Columns.Add("CIVENDNM", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dtRtn.NewRow();

                row["CISDATE"] = dt.Rows[i]["CISDATE"].ToString();
                row["CIDATE"] = dt.Rows[i]["CIDATE"].ToString();
                row["CIVEND"] = dt.Rows[i]["CIVEND"].ToString();
                row["CIYSSTIME"] = dt.Rows[i]["CIYSSTIME"].ToString();
                row["CIYSETIME"] = dt.Rows[i]["CIYSETIME"].ToString();
                row["CIWORK"] = dt.Rows[i]["CIWORK"].ToString();
                row["CIPLACE"] = dt.Rows[i]["CIPLACE"].ToString();
                row["CIWKLIST"] = dt.Rows[i]["CIWKLIST"].ToString();
                row["CLNAME1"] = dt.Rows[i]["CLNAME1"].ToString();
                row["CLJUMIN1"] = dt.Rows[i]["CLJUMIN1"].ToString();
                row["CLJUSO1"] = dt.Rows[i]["CLJUSO1"].ToString();
                row["CLNAME2"] = dt.Rows[i]["CLNAME2"].ToString();
                row["CLJUMIN2"] = dt.Rows[i]["CLJUMIN2"].ToString();
                row["CLJUSO2"] = dt.Rows[i]["CLJUSO2"].ToString();
                row["CICSABUN"] = dt.Rows[i]["CICSABUN"].ToString();
                row["CICNAME"] = dt.Rows[i]["CICNAME"].ToString();
                row["CISNAME"] = dt.Rows[i]["CISNAME"].ToString();
                row["CISJIKCHK"] = dt.Rows[i]["CISJIKCHK"].ToString();

                if (dt.Rows[i]["CIVEND"].ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2445D438", dt.Rows[i]["CIVEND"].ToString());
                    dtVend = this.DbConnector.ExecuteDataTable();

                    if (dtVend.Rows.Count > 0)
                    {
                        row["CIVENDNM"] = dtVend.Rows[0]["VNSANGHO"].ToString();
                    }
                }
                dtRtn.Rows.Add(row);
            }

            return dtRtn;
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85AGK003", ds.Tables[0].Rows[i]["CIDATE"].ToString().Replace("-", ""),
                                                                ds.Tables[0].Rows[i]["CISEQ"]);

                    this.DbConnector.Attach("TY_P_HR_853HS949", ds.Tables[0].Rows[i]["CIDATE"].ToString().Replace("-", ""),
                                                                ds.Tables[0].Rows[i]["CISEQ"]);
                }
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_GB_23NAD874");

                this.BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_842HV794.GetDataSourceInclude(TSpread.TActionType.Remove, "CIDATE", "CISEQ"));

            if (ds.Tables[0].Rows.Count == 0)
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
    }
}
