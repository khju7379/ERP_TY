using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.03 17:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2C352805 : 고정자산 Master 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2C354807 : 고정자산 Master 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  FXGUBN : 자산분류코드
    ///  FXNAME : 자산명
    ///  FXYEAR : 자산년도
    /// </summary>
    public partial class TYACSE003S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACSE003S()
        {
            InitializeComponent();
        }

        private void TYACSE003S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.CBO01_AINTAGUBN); 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_43E5B729.Initialize(); 

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_43E5A728", this.CBO01_AINTAGUBN.GetValue(), this.TXT01_AIASSETNM.GetValue().ToString());
            this.FPS91_TY_S_AC_43E5B729.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACSE003I(string.Empty, string.Empty, string.Empty, "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2C354807_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2C354807_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACSE003I(this.FPS91_TY_S_AC_43E5B729.GetValue("AINTAGUBN").ToString(), this.FPS91_TY_S_AC_43E5B729.GetValue("AINTAYEAR").ToString(),
                                this.FPS91_TY_S_AC_43E5B729.GetValue("AINTASEQ").ToString(), "UPT")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_43E5M731", dt);  //고정자산 마스타 삭제

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_43E5B729.GetDataSourceInclude(TSpread.TActionType.Remove, "AINTAGUBN", "AINTAYEAR", "AINTASEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            // 월상각 자료 유무 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                    "TY_P_AC_43E5K730",
                    dt.Rows[i]["AINTAGUBN"].ToString(),
                    dt.Rows[i]["AINTAYEAR"].ToString(),
                    dt.Rows[i]["AINTASEQ"].ToString()
                    );

                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                if (dt2.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_43E5N733");
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

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_43I3R768", this.CBO01_AINTAGUBN.GetValue(), this.TXT01_AIASSETNM.GetValue().ToString());
            
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE003R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion
    }
}
