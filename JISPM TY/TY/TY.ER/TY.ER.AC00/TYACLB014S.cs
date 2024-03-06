using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 예산계획대실적현황 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.03 09:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_293AD722 : 예산계획대실적현황
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2936R733 : 예산계획대실적현황
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GCDAC : 계정코드
    ///  GGUBUN : 구분
    ///  GPRTGN : 출력구분
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACLB014S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB014S()
        {
            InitializeComponent();
        }

        private void TYACLB014S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = TXT01_GDATE.GetValue() + "0101";

            UP_Spread_Load();

            SetStartingFocus(this.TXT01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_293AD722",
                this.TXT01_GDATE.GetValue(),
                this.CBH01_GCDDP.GetValue(),
                this.CBO01_GGUBUN.GetValue(),
                this.CBO01_GPRTGN.GetValue(),
                this.CBO01_GCDAC.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2936R733.SetValue(UP_ConvertDt(dt, "SEL"));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_2936R733.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_2936R733.GetValue(i, "A1ABAC").ToString() == "부 서 계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_2936R733.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }

            UP_Spread_Load();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_293AD722",
                this.TXT01_GDATE.GetValue(),
                this.CBH01_GCDDP.GetValue(),
                this.CBO01_GGUBUN.GetValue(),
                this.CBO01_GPRTGN.GetValue(),
                this.CBO01_GCDAC.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLB014R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_ConvertDt(dt, "PRT"))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_2936R733_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2);  //=1월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);  //=2월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);  //=3월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 2); //=4월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 2); //=5월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 14, 1, 2); //=6월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 16, 1, 2); //=7월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 18, 1, 2); //=8월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 20, 1, 2); //=9월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 22, 1, 2); //=10월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 24, 1, 2); //=11월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 26, 1, 2); //=12월
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 28, 1, 2); //=년계
            this.FPS91_TY_S_AC_2936R733_Sheet1.AddColumnHeaderSpanCell(0, 30, 2, 1); //=증감액

            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 0].Value  = "예산구분";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 1].Value  = "부서명";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 2].Value  = "계정";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 3].Value  = "계정명";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 4].Value  = "1월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 6].Value  = "2월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 8].Value  = "3월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 10].Value = "4월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 12].Value = "5월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 14].Value = "6월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 16].Value = "7월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 18].Value = "8월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 20].Value = "9월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 22].Value = "10월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 24].Value = "11월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 26].Value = "12월";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 28].Value = "년계";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[0, 30].Value = "증감액";

            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 0].Value = "";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 1].Value = "";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 4].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 5].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 6].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 7].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 8].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 9].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 10].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 11].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 12].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 13].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 14].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 15].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 16].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 17].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 18].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 19].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 20].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 21].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 22].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 23].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 24].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 25].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 26].Value = "예산";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 27].Value = "실적";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 28].Value = "예산년계";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 29].Value = "실적년계";
            this.FPS91_TY_S_AC_2936R733_Sheet1.ColumnHeader.Cells[1, 30].Value = "";

            for (int i = 0; i < this.FPS91_TY_S_AC_2936R733_Sheet1.RowCount; i++)
            {
                //if (i % 2 == 0)
                //{
                //    this.FPS91_TY_S_AC_2936R733_Sheet1.AddSpanCell(0, 0, 2, 1);
                //    this.FPS91_TY_S_AC_2936R733_Sheet1.AddSpanCell(0, 1, 2, 1);
                //    this.FPS91_TY_S_AC_2936R733_Sheet1.AddSpanCell(0, 2, 2, 1);
                //    this.FPS91_TY_S_AC_2936R733_Sheet1.AddSpanCell(0, 3, 2, 1);

                    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                    GeneralCellType tmpCellType = new GeneralCellType();
                    tmpCellType.FormatString = "#,###";

                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 4].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 5].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 6].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 7].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 8].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 9].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 10].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 11].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 12].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 13].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 14].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 15].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 16].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 17].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 18].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 19].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 20].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 21].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 22].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 23].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 24].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 25].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 26].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 27].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 28].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 29].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 30].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 22].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 23].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 24].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 25].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 26].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 27].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 28].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 29].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_2936R733_Sheet1.Cells[i, 30].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                //}
            }

            if (this.FPS91_TY_S_AC_2936R733_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_2936R733_Sheet1.AlternatingRows[0].BackColor = Color.White;
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt, string sGUBUN)
        {
            int i = 0;

            string sCDDESC1 = string.Empty;

            string sNEWSGBN    = string.Empty;
            string sOldSGBN    = string.Empty;
            string sNEWCDDESC1 = string.Empty;
            string sOldCDDESC1 = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["CDDESC1"].ToString() != table.Rows[i]["CDDESC1"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["SYYYY"]   = table.Rows[i - 1]["SYYYY"].ToString();
                    table.Rows[i]["SGBN"]    = table.Rows[i - 1]["SGBN"].ToString();
                    table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();
                    table.Rows[i]["MMCDAC"]  = "";
                    // 소 계 이름 넣기
                    table.Rows[i]["A1ABAC"]  = "부 서 계";

                    // 사업장
                    sCDDESC1 = "CDDESC1 = '" + table.Rows[i - 1]["CDDESC1"].ToString() + "' ";

                    if (this.CBO01_GCDAC.GetValue().ToString() == "1") // 상위계정
                    {
                        sCDDESC1 += " AND GBN = '' " ;
                    }
                    else // 하위계정
                    {
                        sCDDESC1 += " AND GBN = 'Y' " ;
                    }

                    table.Rows[i]["YSAMT01"]   = table.Compute("SUM(YSAMT01)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT02"]   = table.Compute("SUM(YSAMT02)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT03"]   = table.Compute("SUM(YSAMT03)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT04"]   = table.Compute("SUM(YSAMT04)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT05"]   = table.Compute("SUM(YSAMT05)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT06"]   = table.Compute("SUM(YSAMT06)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT07"]   = table.Compute("SUM(YSAMT07)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT08"]   = table.Compute("SUM(YSAMT08)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT09"]   = table.Compute("SUM(YSAMT09)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT10"]   = table.Compute("SUM(YSAMT10)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT11"]   = table.Compute("SUM(YSAMT11)",   sCDDESC1).ToString();
                    table.Rows[i]["YSAMT12"]   = table.Compute("SUM(YSAMT12)",   sCDDESC1).ToString();

                    table.Rows[i]["YSAMTSANG"] = table.Compute("SUM(YSAMTSANG)", sCDDESC1).ToString();
                    table.Rows[i]["YSAMTHA"]   = table.Compute("SUM(YSAMTHA)",   sCDDESC1).ToString();
                    table.Rows[i]["YSHAP"]     = table.Compute("SUM(YSHAP)",     sCDDESC1).ToString();

                    table.Rows[i]["USAMT01"]   = table.Compute("SUM(USAMT01)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT02"]   = table.Compute("SUM(USAMT02)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT03"]   = table.Compute("SUM(USAMT03)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT04"]   = table.Compute("SUM(USAMT04)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT05"]   = table.Compute("SUM(USAMT05)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT06"]   = table.Compute("SUM(USAMT06)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT07"]   = table.Compute("SUM(USAMT07)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT08"]   = table.Compute("SUM(USAMT08)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT09"]   = table.Compute("SUM(USAMT09)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT10"]   = table.Compute("SUM(USAMT10)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT11"]   = table.Compute("SUM(USAMT11)",   sCDDESC1).ToString();
                    table.Rows[i]["USAMT12"]   = table.Compute("SUM(USAMT12)",   sCDDESC1).ToString();

                    table.Rows[i]["USAMTSANG"] = table.Compute("SUM(USAMTSANG)", sCDDESC1).ToString();
                    table.Rows[i]["USAMTHA"]   = table.Compute("SUM(USAMTHA)",   sCDDESC1).ToString();
                    table.Rows[i]["USHAP"]     = table.Compute("SUM(USHAP)",     sCDDESC1).ToString();

                    table.Rows[i]["TOTAL"]     = table.Compute("SUM(TOTAL)",     sCDDESC1).ToString();

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            /******* 마지막 거래처의 대한 로우 생성*****/
            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            
            table.Rows[i]["SYYYY"]   = table.Rows[i - 1]["SYYYY"].ToString();
            table.Rows[i]["SGBN"]    = table.Rows[i - 1]["SGBN"].ToString();
            table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();
            table.Rows[i]["MMCDAC"]  = "";
            // 소 계 이름 넣기
            table.Rows[i]["A1ABAC"]  = "부 서 계";

            // 사업장
            sCDDESC1 = "CDDESC1 = '" + table.Rows[i - 1]["CDDESC1"].ToString() + "' ";

            if (this.CBO01_GCDAC.GetValue().ToString() == "1") // 상위계정
            {
                sCDDESC1 += " AND GBN = '' ";
            }
            else // 하위계정
            {
                sCDDESC1 += " AND GBN = 'Y' " ;
            }

            table.Rows[i]["YSAMT01"]   = table.Compute("SUM(YSAMT01)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT02"]   = table.Compute("SUM(YSAMT02)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT03"]   = table.Compute("SUM(YSAMT03)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT04"]   = table.Compute("SUM(YSAMT04)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT05"]   = table.Compute("SUM(YSAMT05)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT06"]   = table.Compute("SUM(YSAMT06)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT07"]   = table.Compute("SUM(YSAMT07)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT08"]   = table.Compute("SUM(YSAMT08)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT09"]   = table.Compute("SUM(YSAMT09)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT10"]   = table.Compute("SUM(YSAMT10)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT11"]   = table.Compute("SUM(YSAMT11)",   sCDDESC1).ToString();
            table.Rows[i]["YSAMT12"]   = table.Compute("SUM(YSAMT12)",   sCDDESC1).ToString();

            table.Rows[i]["YSAMTSANG"] = table.Compute("SUM(YSAMTSANG)", sCDDESC1).ToString();
            table.Rows[i]["YSAMTHA"]   = table.Compute("SUM(YSAMTHA)",   sCDDESC1).ToString();
            table.Rows[i]["YSHAP"]     = table.Compute("SUM(YSHAP)",     sCDDESC1).ToString();

            table.Rows[i]["USAMT01"]   = table.Compute("SUM(USAMT01)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT02"]   = table.Compute("SUM(USAMT02)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT03"]   = table.Compute("SUM(USAMT03)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT04"]   = table.Compute("SUM(USAMT04)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT05"]   = table.Compute("SUM(USAMT05)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT06"]   = table.Compute("SUM(USAMT06)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT07"]   = table.Compute("SUM(USAMT07)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT08"]   = table.Compute("SUM(USAMT08)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT09"]   = table.Compute("SUM(USAMT09)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT10"]   = table.Compute("SUM(USAMT10)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT11"]   = table.Compute("SUM(USAMT11)",   sCDDESC1).ToString();
            table.Rows[i]["USAMT12"]   = table.Compute("SUM(USAMT12)",   sCDDESC1).ToString();

            table.Rows[i]["USAMTSANG"] = table.Compute("SUM(USAMTSANG)", sCDDESC1).ToString();
            table.Rows[i]["USAMTHA"]   = table.Compute("SUM(USAMTHA)",   sCDDESC1).ToString();
            table.Rows[i]["USHAP"]     = table.Compute("SUM(USHAP)",     sCDDESC1).ToString();

            table.Rows[i]["TOTAL"]     = table.Compute("SUM(TOTAL)",     sCDDESC1).ToString();


            DataTable Condt = new DataTable();

            Condt.Columns.Add("SYYYY",     typeof(System.String));
            Condt.Columns.Add("SGBN",      typeof(System.String));
            Condt.Columns.Add("CDDESC1",   typeof(System.String));
            Condt.Columns.Add("MMCDAC",    typeof(System.String));
            Condt.Columns.Add("A1ABAC",    typeof(System.String));

            Condt.Columns.Add("YSAMT01",   typeof(System.String));
            Condt.Columns.Add("YSAMT02",   typeof(System.String));
            Condt.Columns.Add("YSAMT03",   typeof(System.String));
            Condt.Columns.Add("YSAMT04",   typeof(System.String));
            Condt.Columns.Add("YSAMT05",   typeof(System.String));
            Condt.Columns.Add("YSAMT06",   typeof(System.String));
            Condt.Columns.Add("YSAMT07",   typeof(System.String));
            Condt.Columns.Add("YSAMT08",   typeof(System.String));
            Condt.Columns.Add("YSAMT09",   typeof(System.String));
            Condt.Columns.Add("YSAMT10",   typeof(System.String));
            Condt.Columns.Add("YSAMT11",   typeof(System.String));
            Condt.Columns.Add("YSAMT12",   typeof(System.String));
            Condt.Columns.Add("YSAMTSANG", typeof(System.String));
            Condt.Columns.Add("YSAMTHA",   typeof(System.String));
            Condt.Columns.Add("YSHAP",     typeof(System.String));

            Condt.Columns.Add("USAMT01",   typeof(System.String));
            Condt.Columns.Add("USAMT02",   typeof(System.String));
            Condt.Columns.Add("USAMT03",   typeof(System.String));
            Condt.Columns.Add("USAMT04",   typeof(System.String));
            Condt.Columns.Add("USAMT05",   typeof(System.String));
            Condt.Columns.Add("USAMT06",   typeof(System.String));
            Condt.Columns.Add("USAMT07",   typeof(System.String));
            Condt.Columns.Add("USAMT08",   typeof(System.String));
            Condt.Columns.Add("USAMT09",   typeof(System.String));
            Condt.Columns.Add("USAMT10",   typeof(System.String));
            Condt.Columns.Add("USAMT11",   typeof(System.String));
            Condt.Columns.Add("USAMT12",   typeof(System.String));
            Condt.Columns.Add("USAMTSANG", typeof(System.String));
            Condt.Columns.Add("USAMTHA",   typeof(System.String));
            Condt.Columns.Add("USHAP",     typeof(System.String));

            Condt.Columns.Add("TOTAL",     typeof(System.String));

            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWSGBN    = table.Rows[i]["SGBN"].ToString();
                sNEWCDDESC1 = table.Rows[i]["CDDESC1"].ToString();

                row = Condt.NewRow();

                if (sGUBUN == "SEL")
                {
                    if (sNEWSGBN != sOldSGBN)
                    {
                        row["SGBN"] = table.Rows[i]["SGBN"].ToString();

                        sOldSGBN    = sNEWSGBN;
                    }
                    else
                    {
                        row["SGBN"] = "";
                    }

                    if (sNEWCDDESC1 != sOldCDDESC1)
                    {
                        row["CDDESC1"] = table.Rows[i]["CDDESC1"].ToString();

                        sOldCDDESC1    = sNEWCDDESC1;
                    }
                    else
                    {
                        row["CDDESC1"] = "";
                    }
                }
                else
                {
                    row["SGBN"]    = table.Rows[i]["SGBN"].ToString();
                    row["CDDESC1"] = table.Rows[i]["CDDESC1"].ToString();
                }

                row["SYYYY"]     = table.Rows[i]["SYYYY"].ToString();
                row["MMCDAC"]    = table.Rows[i]["MMCDAC"].ToString();
                row["A1ABAC"]    = table.Rows[i]["A1ABAC"].ToString();

                row["YSAMT01"]   = table.Rows[i]["YSAMT01"].ToString();
                row["YSAMT02"]   = table.Rows[i]["YSAMT02"].ToString();
                row["YSAMT03"]   = table.Rows[i]["YSAMT03"].ToString();
                row["YSAMT04"]   = table.Rows[i]["YSAMT04"].ToString();
                row["YSAMT05"]   = table.Rows[i]["YSAMT05"].ToString();
                row["YSAMT06"]   = table.Rows[i]["YSAMT06"].ToString();
                row["YSAMT07"]   = table.Rows[i]["YSAMT07"].ToString();
                row["YSAMT08"]   = table.Rows[i]["YSAMT08"].ToString();
                row["YSAMT09"]   = table.Rows[i]["YSAMT09"].ToString();
                row["YSAMT10"]   = table.Rows[i]["YSAMT10"].ToString();
                row["YSAMT11"]   = table.Rows[i]["YSAMT11"].ToString();
                row["YSAMT12"]   = table.Rows[i]["YSAMT12"].ToString();
                row["YSAMTSANG"] = table.Rows[i]["YSAMTSANG"].ToString();
                row["YSAMTHA"]   = table.Rows[i]["YSAMTHA"].ToString();
                row["YSHAP"]     = table.Rows[i]["YSHAP"].ToString();
                

                row["USAMT01"]   = table.Rows[i]["USAMT01"].ToString();
                row["USAMT02"]   = table.Rows[i]["USAMT02"].ToString();
                row["USAMT03"]   = table.Rows[i]["USAMT03"].ToString();
                row["USAMT04"]   = table.Rows[i]["USAMT04"].ToString();
                row["USAMT05"]   = table.Rows[i]["USAMT05"].ToString();
                row["USAMT06"]   = table.Rows[i]["USAMT06"].ToString();
                row["USAMT07"]   = table.Rows[i]["USAMT07"].ToString();
                row["USAMT08"]   = table.Rows[i]["USAMT08"].ToString();
                row["USAMT09"]   = table.Rows[i]["USAMT09"].ToString();
                row["USAMT10"]   = table.Rows[i]["USAMT10"].ToString();
                row["USAMT11"]   = table.Rows[i]["USAMT11"].ToString();
                row["USAMT12"]   = table.Rows[i]["USAMT12"].ToString();
                row["USAMTSANG"] = table.Rows[i]["USAMTSANG"].ToString();
                row["USAMTHA"]   = table.Rows[i]["USAMTHA"].ToString();
                row["USHAP"]     = table.Rows[i]["USHAP"].ToString();

                row["TOTAL"]     = table.Rows[i]["TOTAL"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion

        #region Description : 예산년도 이벤트
        private void TXT01_GDATE_TextChanged(object sender, EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = TXT01_GDATE.GetValue() + "0101";
        }
        #endregion
    }
}