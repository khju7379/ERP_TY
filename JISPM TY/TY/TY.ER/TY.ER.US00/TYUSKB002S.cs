using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 거래처관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.02.25 14:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92PHS899 : 거래처관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92PHV901 : 거래처관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  VNCODE : 거래처코드
    ///  VNIRUM : 대표자명
    ///  VNSANGHO : 거래처명
    ///  VNSAUPNO : 사업자등록번호
    /// </summary>
    public partial class TYUSKB002S : TYBase
    {
        #region Description : 폼 로드
        public TYUSKB002S()
        {
            InitializeComponent();
        }

        private void TYUSKB002S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_92PHV901.Sheets[0].Columns[24].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92PHV901, "BTN");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.TXT01_VNSANGHO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_US_92PHS899",
                this.TXT01_VNSANGHO.GetValue().ToString(),
                this.TXT01_VNSAUPNO.GetValue().ToString(),
                this.TXT01_VNIRUM.GetValue().ToString(),
                this.TXT01_VNCODE.GetValue().ToString()
                );

            this.FPS91_TY_S_US_92PHV901.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUSKB002I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_US_92PHM896", dt);
            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_US_92PHV901.GetDataSourceInclude(TSpread.TActionType.Remove, "VNCODE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                /******************************************************************
			     * 1. 등록, 수정시 동일한 사업자 번호는 안들어가짐.               *
			     * 2. 사업자번호 업데이트 안됨.                                   *
			     *    2-1. 수정시 한번이라도 거래된 경우(재고테이블)              *
			     *    2-2. 수정시 한번이라도 전표가 발행된 경우                   *
			     * 3. 삭제 불가                                                   *
			     *    3-1. 한번이라도 거래된 경우(재고테이블)                     *
			     *    3-2. 한번이라도 전표가 발행된 경우                          *
                 *    3-3. 회계에 존재하는 사업자일경우                           * 
			     ******************************************************************/

                #region Description : 3-1경우

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_92SGJ966",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }

                #endregion

                #region Description : 3-2경우

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_92SGN967",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }

                #endregion

                #region Dsecription : 3-3경우

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_935EB990",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }
                #endregion
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 더블 클릭
        private void FPS91_TY_S_US_92PHV901_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYUSKB002I(this.FPS91_TY_S_US_92PHV901.GetValue("VNCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 거래처대장 출력
        private void FPS91_TY_S_US_92PHV901_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "24")
            {
                if (this.FPS91_TY_S_US_92PHV901.GetValue("VNCODE").ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_975BK014", this.FPS91_TY_S_US_92PHV901.GetValue("VNCODE").ToString());

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    SectionReport rpt = new TYUSKB002R();

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
        }
        #endregion
    }
}
