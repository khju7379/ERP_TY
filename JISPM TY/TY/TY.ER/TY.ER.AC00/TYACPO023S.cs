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
    /// EIS 주요설비투자현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.23 10:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37N2H215 : EIS 주요설비투자현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37N2J217 : EIS 주요설비투자현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO023S : TYBase
    {
        private string fsCompanyCode;

        #region  Description : 폼 로드 이벤트
        public TYACPO023S()
        {
            InitializeComponent();
        }

        private void TYACPO023S_Load(object sender, System.EventArgs e)
        {
            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESCUSTGB.SetValue(fsCompanyCode);
                this.CBH01_ESCUSTGB.SetReadOnly(true);
            }

            //this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            // this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            UP_start_dsp_month();

            this.SetStartingFocus(this.CBH01_ESCUSTGB.CodeText);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_39N4V834.Initialize(); 

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_AC_39N4V833",
                                   this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4),
                                   this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                   this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                   this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                   this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                   this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                   this.CBH01_ESCUSTGB.GetValue()
                                   );

            this.FPS91_TY_S_AC_39N4V834.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_39N4V834.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_39N4V834.ActiveSheet.RowCount; i++)
                {
                    // 경영이슈
                    if (this.FPS91_TY_S_AC_39N4V834.GetValue(i, "GUBUN1").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_39N4V834_Sheet1.Cells[i, 3].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    // 계열사관리
                    if (this.FPS91_TY_S_AC_39N4V834.GetValue(i, "GUBUN3").ToString() == "" )
                    {
                        this.FPS91_TY_S_AC_39N4V834_Sheet1.Cells[i, 4].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    // 결산자료
                    if (this.FPS91_TY_S_AC_39N4V834.GetValue(i, "GUBUN5").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_39N4V834_Sheet1.Cells[i, 5].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    // 임원현황
                    if (this.FPS91_TY_S_AC_39N4V834.GetValue(i, "GUBUN6").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_39N4V834_Sheet1.Cells[i, 6].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    // 주주현황
                    if (this.FPS91_TY_S_AC_39N4V834.GetValue(i, "GUBUN7").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_39N4V834_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    // 인원현황
                    if (this.FPS91_TY_S_AC_39N4V834.GetValue(i, "GUBUN8").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_39N4V834_Sheet1.Cells[i, 8].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    // 시설현황
                    if (this.FPS91_TY_S_AC_39N4V834.GetValue(i, "GUBUN9").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_39N4V834_Sheet1.Cells[i, 9].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_39N4V834_ButtonClicked 이벤트
        private void FPS91_TY_S_AC_39N4V834_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.DbConnector.CommandClear();

            if (e.Column.ToString() == "3")      // 경영이슈
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BE51299",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACPO023R7();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else if (e.Column.ToString() == "4") // 계열사관리
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39P1W862",
                    this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACPO023R1();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else if (e.Column.ToString() == "5") // 결산자료
            {
                string sTHR_YEARS_AGO = string.Empty;
                string sTWO_YEARS_AGO = string.Empty;
                string sONE_YEARS_AGO = string.Empty;

                sONE_YEARS_AGO = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4)) - 1);
                sTWO_YEARS_AGO = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4)) - 2);
                sTHR_YEARS_AGO = Convert.ToString(int.Parse(this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4)) - 3);

                this.DbConnector.Attach
                    (
                    "TY_P_AC_39U2G922",
                    sTHR_YEARS_AGO.ToString(),
                    sTWO_YEARS_AGO.ToString(),
                    sONE_YEARS_AGO.ToString(),
                    this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACPO023R2();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else if (e.Column.ToString() == "6") // 임원현황
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39OBV846",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACPO023R3();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else if (e.Column.ToString() == "7") // 주주현황
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39O4V852",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString(),
                    this.DTP01_GSTYYMM.GetValue(),
                    this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACPO023R4();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else if (e.Column.ToString() == "8") // 인원현황
            {
                if (this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString() == "TH")
                {
                    this.DbConnector.Attach("TY_P_AC_39O6A853", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                }
                else if (this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString() == "TG")
                {
                    this.DbConnector.Attach("TY_P_AC_39O6B854", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                }
                else if (this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString() == "TS")
                {
                    this.DbConnector.Attach("TY_P_AC_39O6C855", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                }

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACPO023R5();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else if (e.Column.ToString() == "9") // 시설현황
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BD2B274",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.FPS91_TY_S_AC_39N4V834.GetValue(e.Row, "ESCUSTGB").ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACPO023R6();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : EIS 계열사 최종 마감 월 조회
        private void UP_start_dsp_month()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C94Q662");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_GSTYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion
    }
}
