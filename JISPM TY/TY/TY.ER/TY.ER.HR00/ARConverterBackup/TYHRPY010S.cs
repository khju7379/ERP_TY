using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using TY.ER.GB00;
using TY.ER.AC00;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여전표조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.18 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53J9Y716 : 급여전표조회
    ///  TY_P_HR_53JB9719 : 급여전표 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53J9Z718 : 급여전표관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRPY010S : TYBase
    {
        #region Description : 폼 로드
        public TYHRPY010S()
        {
            InitializeComponent();
        }

        private void TYHRPY010S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_HR_53J9Z718.Sheets[0].Columns[14].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53J9Z718, "BTNPRT");

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53J9Y716",
                                    this.DTP01_STDATE.GetString().Substring(0, 6),
                                    this.DTP01_EDDATE.GetString().Substring(0, 6),
                                    this.CBH01_PAYGUBN.GetValue().ToString(),
                                    this.CBH01_KBSABUN.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();                       

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_53J9Z718.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_HR_53J9Z718.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_53J9Z718.GetValue(i, "APMJPNO").ToString() == "")
                    {
                        this.FPS91_TY_S_HR_53J9Z718_Sheet1.Cells[i, 14].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }

        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_STDATE.GetString();
            string EDDATE = this.DTP01_EDDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.DTP01_STDATE.Focus();
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY010I(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //마스타 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53JB9719", ds.Tables[0]);
            this.DbConnector.ExecuteTranQueryList();

            //내역 삭제
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_53NHS840", ds.Tables[0].Rows[i]["APMYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["APMDPMK"].ToString(),
                                                            ds.Tables[0].Rows[i]["APMPYCODE"].ToString(),
                                                            ds.Tables[0].Rows[i]["APMGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["APMJIDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["APMDATE"].ToString(),
                                                            "");
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_53J9Z718.GetDataSourceInclude(TSpread.TActionType.Remove, "APMYYMM", "APMDPMK", "APMPYCODE", "APMJIDATE", "APMDATE", "APMGUBN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count ; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53JEN734",
                                            ds.Tables[0].Rows[i]["APMYYMM"].ToString(),
                                            ds.Tables[0].Rows[i]["APMDPMK"].ToString(),
                                            ds.Tables[0].Rows[i]["APMPYCODE"].ToString(),
                                            ds.Tables[0].Rows[i]["APMGUBN"].ToString(),
                                            ds.Tables[0].Rows[i]["APMJIDATE"].ToString(),
                                            ds.Tables[0].Rows[i]["APMDATE"].ToString());

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows[0]["APMJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_3174H522");
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_53J9Z718_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRPY010I(this.FPS91_TY_S_HR_53J9Z718.GetValue("APMYYMM").ToString(), 
                                this.FPS91_TY_S_HR_53J9Z718.GetValue("APMDPMK").ToString(), 
                                this.FPS91_TY_S_HR_53J9Z718.GetValue("APMPYCODE").ToString(),
                                this.FPS91_TY_S_HR_53J9Z718.GetValue("APMGUBN").ToString(), 
                                this.FPS91_TY_S_HR_53J9Z718.GetValue("APMJIDATE").ToString(),
                                this.FPS91_TY_S_HR_53J9Z718.GetValue("APMDATE").ToString() 
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 전표 출력 버튼 이벤트
        private void FPS91_TY_S_HR_53J9Z718_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "14")
            {
                if (this.FPS91_TY_S_HR_53J9Z718.GetValue("APMJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_HR_53J9Z718.GetValue("APMJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_HR_53J9Z718.GetValue("APMJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_HR_53J9Z718.GetValue("APMJPNO").ToString().Substring(14, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        ActiveReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        ActiveReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }
            }
        }
        #endregion

        #region Description : 고용.산재보험 요율관리 버튼 이벤트
        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY010P()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion


    }
}
