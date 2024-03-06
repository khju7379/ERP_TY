using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;
using System.Drawing;
using System.Linq;
using System.Text;
using GrapeCity.ActiveReports;

using TY.ER.GB00;


namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 계열사 관리카드 경영실적 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.11.14 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BE1T292 : EIS 계열사 관리카드 경영실적 조회
    ///  TY_P_AC_3BE1V293 : EIS 계열사 관리카드 경영실적 조회-(TYGT)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_39UB2916 : EIS 계열사 관리카드 경영실적 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27P47279 : 년도를 입력하세요.
    ///  TY_M_AC_3992B618 : 작업 할 권한이 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ESBMCUST : 계열사구분
    ///  ESBMYYHD : 처리년
    /// </summary>
    public partial class TYACPO027S : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYACPO027S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACPO027S_Load(object sender, System.EventArgs e)
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
                this.CBH01_ESBMCUST.SetValue(fsCompanyCode);
                this.CBH01_ESBMCUST.SetReadOnly(true);
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.DTP01_ESBMYYHD);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESBMCUST.CodeText);
            }

            //this.DTP01_ESBMYYHD.SetValue(DateTime.Now.ToString("yyyy-MM"));
            //this.DTP01_ESBMYYHD.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            UP_start_dsp_month();

            UP_Spread_Title(this.DTP01_ESBMYYHD.GetValue().ToString());

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            UP_Spread_Title(this.DTP01_ESBMYYHD.GetValue().ToString());

            this.DbConnector.CommandClear();
            if (this.CBH01_ESBMCUST.GetValue().ToString() == "TG") // 그레인터미널
            {
                this.DbConnector.Attach("TY_P_AC_3BE1V293",
                                       this.CBH01_ESBMCUST.GetValue(),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(0, 4),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(4, 2)
                                       );
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_3BE1T292",
                                       this.CBH01_ESBMCUST.GetValue(),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(0, 4),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(4, 2)
                                       );
            }

            this.FPS91_TY_S_AC_3BE23297.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_3BE23297.ActiveSheet.RowCount > 0)
            {
                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_3BE23297.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_3BE23297.GetValue(i, "ESMCDAC").ToString() == "01000000" || // 매출
                        this.FPS91_TY_S_AC_3BE23297.GetValue(i, "ESMCDAC").ToString() == "02000000" || // 원가
                        this.FPS91_TY_S_AC_3BE23297.GetValue(i, "ESMCDAC").ToString() == "03000000" || // 매출이익
                        this.FPS91_TY_S_AC_3BE23297.GetValue(i, "ESMCDAC").ToString() == "05000000" || // 영업이익
                        this.FPS91_TY_S_AC_3BE23297.GetValue(i, "ESMCDAC").ToString() == "10000000" || // 영업외손익
                        this.FPS91_TY_S_AC_3BE23297.GetValue(i, "ESMCDAC").ToString() == "11000000"    // 세전이익
                        )
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_3BE23297.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_3BE23297.GetValue(i, "ESMCDAC").ToString() == "13000000")  // 당기순이익
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_3BE23297.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
        }
        #endregion


        #region Description : 스프레드 타이틀
        private void UP_Spread_Title(string sDATE)
        {

            string s당월누계 = string.Empty;
            string s당월 = string.Empty;
            string s전월누계 = string.Empty;

            s당월누계 = " ( " + sDATE.Substring(0, 4) + " / 01 ~ " + sDATE.Substring(4, 2) + " )";
            s당월 = " ( "+ sDATE.Substring(0, 4) + " / " + sDATE.ToString().Substring(4, 2) + " )";
            s전월누계 = " ( "+ sDATE.Substring(0, 4) + " / 01 ~ " + Set_Fill2(Convert.ToString(int.Parse(sDATE.Substring(4, 2)) -1 )) + " )";

            if (Set_Fill2(Convert.ToString(int.Parse(sDATE.Substring(4, 2)) - 1)) == "00")
            {
                s전월누계 = "";
            }

            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.RowHeaderColumnCount = 1;

            // 세로 컬럼 합치기
            this.FPS91_TY_S_AC_3BE23297_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_3BE23297_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_3BE23297_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_3BE23297_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);

            // 가로 컬럼 합치기
            this.FPS91_TY_S_AC_3BE23297_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2);
            this.FPS91_TY_S_AC_3BE23297_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);
            this.FPS91_TY_S_AC_3BE23297_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);

            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[0, 4].Value = "전월누계" + s전월누계;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[0, 6].Value = "당월" + s당월;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[0, 8].Value = "당월누계" + s당월누계;

            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 4].Value = "계획";
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 5].Value = "실적";

            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 6].Value = "계획";
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 7].Value = "실적";

            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 8].Value = "계획";
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 9].Value = "실적";

            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3BE23297_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //for (int i = 0; i < this.FPS91_TY_S_AC_3BE23297_Sheet1.RowCount; i++)
            //{
            //    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
            //    GeneralCellType tmpCellType = new GeneralCellType();
            //    tmpCellType.FormatString = "#,###";

            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 4].CellType = tmpCellType;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 5].CellType = tmpCellType;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 6].CellType = tmpCellType;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 7].CellType = tmpCellType;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 8].CellType = tmpCellType;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 9].CellType = tmpCellType;

            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_3BE23297_Sheet1.Cells[i, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //}

        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            string sDATE = string.Empty;
            string s당월누계 = string.Empty;
            string s당월 = string.Empty;
            string s전월누계 = string.Empty;

            sDATE = this.DTP01_ESBMYYHD.GetValue().ToString();

            s당월누계 = "( " + sDATE.Substring(0, 4) + " / 01 ~ " + sDATE.Substring(4, 2) + " )";
            s당월 = "( " + sDATE.Substring(0, 4) + " / " + sDATE.ToString().Substring(4, 2) + " )";
            s전월누계 = "( " + sDATE.Substring(0, 4) + " / 01 ~ " + Set_Fill2(Convert.ToString(int.Parse(sDATE.Substring(4, 2)) - 1)) + " )";

            if (Set_Fill2(Convert.ToString(int.Parse(sDATE.Substring(4, 2)) - 1)) == "00")
            {
                s전월누계 = "";
            }


            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            if (this.CBH01_ESBMCUST.GetValue().ToString() == "TG") // 그레인터미널
            {
                this.DbConnector.Attach("TY_P_AC_3BL6F406",
                                       this.CBH01_ESBMCUST.GetValue(),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(0, 4),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(4, 2) ,
                                       s전월누계,
                                       s당월 ,
                                       s당월누계 
                                       );
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_3BL6G407",
                                       this.CBH01_ESBMCUST.GetValue(),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(0, 4),
                                       this.DTP01_ESBMYYHD.GetValue().ToString().Substring(4, 2),
                                       s전월누계,
                                       s당월,
                                       s당월누계 
                                       );
            }

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACPO027R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
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
                this.DTP01_ESBMYYHD.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_ESBMYYHD.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion
    }
}
