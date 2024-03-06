using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 품목별매출현황 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37A2X065 : EIS 품목별매출현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37A35071 : EIS 품목별매출현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ERSCDDP : 사업부
    ///  ERSYYMM : 년월
    /// </summary>
    public partial class TYACPO025S : TYBase
    {
        private string fsCompanyCode;

        #region Description : 폼 로드 이벤트
        public TYACPO025S()
        {
            InitializeComponent();
        }

        private void TYACPO025S_Load(object sender, System.EventArgs e)
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
                this.CBH01_ESLSCUST.SetValue(fsCompanyCode);
                this.CBH01_ESLSCUST.SetReadOnly(true);
            }

            //this.DTP01_ESLSYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            //this.DTP01_ESLSYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            UP_start_dsp_month();

            UP_Spread_Title(this.DTP01_ESLSYYMM.GetValue().ToString());

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.DTP01_ESLSYYMM);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESLSCUST.CodeText);
            }
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sESLSYYMM_AGO = string.Empty;
            string sSTESLSYYMM = string.Empty;
            string sEDESLSYYMM = string.Empty;

            string sYears_Ago = string.Empty;
            string sNow_Date = string.Empty;
            string sDATE = string.Empty;

            sDATE = this.DTP01_ESLSYYMM.GetString().ToString();

            sYears_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1).Substring(2, 2) + "12";
            sNow_Date = sDATE.ToString().Substring(2, 2) + Set_Fill2(Convert.ToString(sDATE.ToString().Substring(4, 2)));

            sESLSYYMM_AGO = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "12";

            if (sDATE.ToString().Substring(4, 2) == "01")
            {
                sSTESLSYYMM = Convert.ToString(int.Parse(sDATE.ToString().ToString().Substring(0, 4)) - 1) + "01";
                sEDESLSYYMM = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "12";
            }
            else
            {
                sSTESLSYYMM = sDATE.ToString().Substring(0, 4) + "01";
                sEDESLSYYMM = sDATE.ToString().Substring(0, 4) + Set_Fill2(Convert.ToString(int.Parse(sDATE.ToString().Substring(4, 2)) - 1));
            }

            this.FPS91_TY_S_AC_3AH3F069.Initialize();

            UP_Spread_Title(this.DTP01_ESLSYYMM.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3AH3K071",
                sYears_Ago.ToString(),
                sNow_Date.ToString(),
                sESLSYYMM_AGO.ToString(), // 전년도 12월
                sSTESLSYYMM.ToString(),
                sEDESLSYYMM.ToString(),
                this.CBH01_ESLSCUST.GetValue(),
                this.DTP01_ESLSYYMM.GetString().ToString().Substring(0, 6)
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3AH3F069.SetValue(dt);

            this.DTP01_ESLSYYMM.Focus();















            //string sESLSYYMM_AGO = string.Empty;
            //string sSTESLSYYMM = string.Empty;
            //string sEDESLSYYMM = string.Empty;

            //string sYears_Ago = string.Empty;
            //string sNow_Date = string.Empty;
            //string sDATE = string.Empty;

            //sDATE = this.DTP01_ESLSYYMM.GetString().ToString();

            //sYears_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1).Substring(2, 2) + "년 12월말 잔액";
            //sNow_Date = sDATE.ToString().Substring(2, 2) + "년 " +
            //             Set_Fill2(Convert.ToString(int.Parse(sDATE.ToString().Substring(4, 2)) - 1)) + "월말 잔액";

            //sESLSYYMM_AGO = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "12";

            //if (sDATE.ToString().Substring(4, 2) == "01")
            //{
            //    sSTESLSYYMM = Convert.ToString(int.Parse(sDATE.ToString().ToString().Substring(0, 4)) - 1) + "01";
            //    sEDESLSYYMM = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "12";
            //}
            //else
            //{
            //    sSTESLSYYMM = sDATE.ToString().Substring(0, 4) + "01";
            //    sEDESLSYYMM = sDATE.ToString().Substring(0, 4) + Set_Fill2(Convert.ToString(int.Parse(sDATE.ToString().Substring(4, 2)) - 1));
            //}

            //this.FPS91_TY_S_AC_3AH3F069.Initialize();

            //UP_Spread_Title(this.DTP01_ESLSYYMM.GetValue().ToString());

            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_3AH5V075",
            //    this.tyTextBox1.Text.Trim(),
            //    this.tyTextBox2.Text.Trim(),
            //    sESLSYYMM_AGO.ToString(), // 전년도 12월
            //    sSTESLSYYMM.ToString(),
            //    sEDESLSYYMM.ToString(),
            //    this.CBH01_ESLSCUST.GetValue(),
            //    this.DTP01_ESLSYYMM.GetString().ToString().Substring(0, 6)
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //this.FPS91_TY_S_AC_3AH3F069.SetValue(dt);

            //this.DTP01_ESLSYYMM.Focus();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sESLSYYMM_AGO = string.Empty;
            string sSTESLSYYMM   = string.Empty;
            string sEDESLSYYMM   = string.Empty;

            string sYears_Ago = string.Empty;
            string sNow_Date  = string.Empty;
            string sDATE      = string.Empty;

            sDATE = this.DTP01_ESLSYYMM.GetString().ToString();

            sYears_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1).Substring(2, 2) + "12";
            sNow_Date  = sDATE.ToString().Substring(2, 2) + Set_Fill2(Convert.ToString(sDATE.ToString().Substring(4, 2)));

            sESLSYYMM_AGO = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "12";

            if (sDATE.ToString().Substring(4, 2) == "01")
            {
                sSTESLSYYMM = Convert.ToString(int.Parse(sDATE.ToString().ToString().Substring(0, 4)) - 1) + "01";
                sEDESLSYYMM = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "12";
            }
            else
            {
                sSTESLSYYMM = sDATE.ToString().Substring(0, 4) + "01";
                sEDESLSYYMM = sDATE.ToString().Substring(0, 4) + Set_Fill2(Convert.ToString(int.Parse(sDATE.ToString().Substring(4, 2)) - 1));
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3AH3K071",
                sYears_Ago.ToString(),
                sNow_Date.ToString(),
                sESLSYYMM_AGO.ToString(), // 전년도 12월
                sSTESLSYYMM.ToString(),
                sEDESLSYYMM.ToString(),
                this.CBH01_ESLSCUST.GetValue(),
                this.DTP01_ESLSYYMM.GetString().ToString().Substring(0, 6)
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACPO025R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title(string sDATE)
        {
            string sYears_Ago     = string.Empty;
            string sNow_Date      = string.Empty;

            sYears_Ago     = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1).Substring(2, 2) + "년 12월말 잔액";
            sNow_Date      = sDATE.ToString().Substring(2, 2) + "년 " +
                             Set_Fill2(Convert.ToString(sDATE.ToString().Substring(4, 2))) + "월말 잔액";

            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_3AH3F069_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 4);
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);

            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 0].Value = "계정과목";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 1].Value = "대출과목";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 2].Value = "금융기관";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 3].Value = "최초차입내역";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 7].Value = sYears_Ago;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 8].Value = sNow_Date;
            
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 3].Value = "원화";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 4].Value = "차입일";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 5].Value = "만기일";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 6].Value = "이율";

            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 8].Value = "당기증(감)액";
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 9].Value = "잔액";

            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3AH3F069_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            for (int i = 0; i < this.FPS91_TY_S_AC_3AH3F069_Sheet1.RowCount; i++)
            {
                // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                GeneralCellType tmpCellType = new GeneralCellType();
                tmpCellType.FormatString = "#,###";

                this.FPS91_TY_S_AC_3AH3F069_Sheet1.Cells[i, 7].CellType = tmpCellType;
                this.FPS91_TY_S_AC_3AH3F069_Sheet1.Cells[i, 8].CellType = tmpCellType;
                this.FPS91_TY_S_AC_3AH3F069_Sheet1.Cells[i, 9].CellType = tmpCellType;

                this.FPS91_TY_S_AC_3AH3F069_Sheet1.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_3AH3F069_Sheet1.Cells[i, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_3AH3F069_Sheet1.Cells[i, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
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
                this.DTP01_ESLSYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_ESLSYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion
    }
}