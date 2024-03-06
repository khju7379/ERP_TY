using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 인사기본사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.16 17:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_51GAM167 : 용역직 인사기본사항 조회(그리드)
    ///  TY_P_HR_51JHE187 : 용역직 인사기본사항 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_858BC961 : 용역직 인사기본사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  EDDATE : 종료일자
    ///  KBHANGL : 한글이름
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRGB003S : TYBase
    {
        #region Description : 폼 로드
        public TYHRGB003S()
        {
            InitializeComponent();
        }

        private void TYHRGB003S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_HR_858BC961.Sheets[0].Columns[8].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_858BC961, "PRT");

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_HR_858BC961.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_858BC961.Initialize();

            this.DbConnector.CommandClear();

            if (this.CBH01_BMSABUN.GetValue().ToString() != "")
            {
                this.DbConnector.Attach("TY_P_HR_858B6957", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                            this.CBH01_BMSABUN.GetValue().ToString()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_858B3960", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                                            );
            }

            this.FPS91_TY_S_HR_858BC961.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRGB003I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 방문대장 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.Attach
                (
                "TY_P_HR_85AAP978",
                Set_Date(Get_Date(this.DTP01_STDATE.GetValue().ToString())),
                Set_Date(Get_Date(this.DTP01_EDDATE.GetValue().ToString())),
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_BMSABUN.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYHRGB003R1();

            // 가로 출력
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            if (dt.Rows.Count > 0)
            {
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_858BF962", ds.Tables[0].Rows[i]["BMDATE"].ToString().Replace("-", ""),
                                                            ds.Tables[0].Rows[i]["BMSEQ"]);

                this.DbConnector.Attach("TY_P_HR_858BF963", ds.Tables[0].Rows[i]["BMDATE"].ToString().Replace("-", ""),
                                                            ds.Tables[0].Rows[i]["BMSEQ"]);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_858BC961.GetDataSourceInclude(TSpread.TActionType.Remove, "BMDATE", "BMSEQ"));

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

        #region Description : 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_858BC961_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRGB003I(this.FPS91_TY_S_HR_858BC961.GetValue("BMDATE").ToString(), this.FPS91_TY_S_HR_858BC961.GetValue("BMSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 방문증 출력
        private void FPS91_TY_S_HR_858BC961_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "8")
            {
                this.DbConnector.Attach
                        (
                        "TY_P_HR_858EH968",
                        this.FPS91_TY_S_HR_858BC961.GetValue("BMDATE").ToString().Replace("-", ""),
                        this.FPS91_TY_S_HR_858BC961.GetValue("BMSEQ").ToString()
                        );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYHRGB003R();

                // 세로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                if (dt.Rows.Count > 0)
                {
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : 방문자 텍스트 이벤트
        private void CBH01_BMSABUN_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion
    }
}
