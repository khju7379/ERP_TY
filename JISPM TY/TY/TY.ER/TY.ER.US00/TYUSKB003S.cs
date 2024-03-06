using System;
using System.Data;
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
    /// 거래처관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2441H411 : 거래처 관리 삭제
    ///  TY_P_AC_244BN404 : 거래처관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_US_8BJHK186 : 거래처관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  VNCODE : 거래처코드
    ///  VNSANGHO : 상호
    /// </summary>
    public partial class TYUSKB003S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSKB003S()
        {
            InitializeComponent();
        }

        private void TYUSKB003S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_8BJHX187.Sheets[0].Columns[24].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_8BJHX187, "BTN");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_STYEAR.SetValue(DateTime.Now.AddYears(-2).ToString("yyyy"));
            this.TXT01_EDYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_STYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_8BJGV182",
                this.TXT01_STYEAR.GetValue().ToString(),
                this.TXT01_EDYEAR.GetValue().ToString()
                );

            this.FPS91_TY_S_US_8BJHX187.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUSKB003I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
            this.DbConnector.Attach("TY_P_US_8BJHI185", dt);
            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt1 = new DataTable();

            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_US_8BJHX187.GetDataSourceInclude(TSpread.TActionType.Remove, "CNYEAR", "CNSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sCONTNO = string.Empty;

                sCONTNO = dt.Rows[i]["CNYEAR"].ToString() + Set_Fill2(dt.Rows[i]["CNSEQ"].ToString());

                // 계약순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91BD2480", sCONTNO.ToString().Trim());
                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_91BDB481");

                    e.Successed = false;
                    return;
                }

                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach(
                //                       "TY_P_UT_66FEK215",
                //                       dt.Rows[i]["CNCONTNO"].ToString()
                //                       );

                //if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                //{
                //    this.ShowMessage("TY_M_UT_66FEN218");
                //    e.Successed = false;
                //    return;
                //}

                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach(
                //                       "TY_P_UT_66FEL216",
                //                       dt.Rows[i]["CNCONTNO"].ToString()
                //                       );

                //if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                //{
                //    this.ShowMessage("TY_M_UT_66FEN217");
                //    e.Successed = false;
                //    return;
                //}
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_P_US_8BJHK186_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYUSKB003I(this.FPS91_TY_S_US_8BJHX187.GetValue("CNYEAR").ToString(), this.FPS91_TY_S_US_8BJHX187.GetValue("CNSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 계약대장 출력
        private void FPS91_TY_S_US_8BJHX187_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "24")
            {
                if (this.FPS91_TY_S_US_8BJHX187.GetValue("CNYEAR").ToString() != "" && this.FPS91_TY_S_US_8BJHX187.GetValue("CNSEQ").ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_975EH015", this.FPS91_TY_S_US_8BJHX187.GetValue("CNYEAR").ToString(), this.FPS91_TY_S_US_8BJHX187.GetValue("CNSEQ").ToString());

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    SectionReport rpt = new TYUSKB003R();

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
        }
        #endregion
    }
}