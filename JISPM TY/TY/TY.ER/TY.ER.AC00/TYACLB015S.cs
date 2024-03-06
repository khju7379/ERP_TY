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
    /// 부문별계정별예산집계표 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.04 13:55
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2947L776 : 부문별계정별예산집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2948N801 : 부문별계정별예산집계표
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  E6PRGN : 출력구분
    ///  GPRTGN : 출력구분
    ///  GYESAN : 예산구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACLB015S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB015S()
        {
            InitializeComponent();
        }

        private void TYACLB015S_Load(object sender, System.EventArgs e)
        {
            UP_Spread_Load();

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2947L776",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBO01_E6PRGN.GetValue(),
                this.CBO01_GYESAN.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2948N801.SetValue(dt);

                // 특정 COLUMN 색깔 입히기
                this.FPS91_TY_S_AC_2948N801.ActiveSheet.Columns["UHAP"].BackColor  = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_2948N801.ActiveSheet.Columns["PHAP"].BackColor  = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_2948N801.ActiveSheet.Columns["UPHAP"].BackColor = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_2948N801.ActiveSheet.Columns["AHAP"].BackColor  = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_2948N801.ActiveSheet.Columns["BHAP"].BackColor  = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_2948N801.ActiveSheet.Columns["ABHAP"].BackColor = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_2948N801.ActiveSheet.Columns["TOTAL"].BackColor = Color.FromArgb(218, 239, 244);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_2948N801.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_2948N801.GetValue(i, "A1ABAC").ToString() == "총   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_2948N801.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
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
                "TY_P_AC_2947L776",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBO01_E6PRGN.GetValue(),
                this.CBO01_GYESAN.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt1 = new TYACLB015R1();
                rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt1, dt)).ShowDialog();

                SectionReport rpt2 = new TYACLB015R2();
                rpt2.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt2, dt)).ShowDialog();
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
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_2948N801_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);


            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 3);  // 운영비

            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 3);  // 판매비

            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);  // 운영-판매 소계

            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 6); // 일반관리비
            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 16, 1, 3); // 무역판매비

            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 19, 2, 1); // 일반-무역판매비 소계

            this.FPS91_TY_S_AC_2948N801_Sheet1.AddColumnHeaderSpanCell(0, 20, 2, 1); // 총계

            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 0].Value  = "예산구분";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 1].Value  = "계정구분";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 2].Value  = "계정";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 3].Value  = "운영비";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 6].Value  = "판매비";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 9].Value  = "운영-판매 소계";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 10].Value = "일반관리비";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 16].Value = "무역판매비";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 19].Value = "일반-무역판매비 소계";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[0, 20].Value = "총계";

            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 0].Value  = "";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 1].Value  = "";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 2].Value  = "";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 3].Value  = "UTT운영";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 4].Value  = "SILO운영";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 5].Value  = "소계";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 6].Value  = "UTT영업";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 7].Value  = "SILO영업";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 8].Value  = "소계";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 9].Value  = "";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 10].Value = "관리";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 11].Value = "기획";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 12].Value = "울산공통";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 13].Value = "서울공통";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 14].Value = "공통";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 15].Value = "소계";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 16].Value = "석유화학";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 17].Value = "농업자원";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 18].Value = "소계";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 19].Value = "";
            this.FPS91_TY_S_AC_2948N801_Sheet1.ColumnHeader.Cells[1, 20].Value = "";

            for (int i = 0; i < this.FPS91_TY_S_AC_2948N801_Sheet1.RowCount; i++)
            {
                // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                GeneralCellType tmpCellType = new GeneralCellType();
                tmpCellType.FormatString = "#,###";

                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 3].CellType  = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 4].CellType  = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 5].CellType  = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 6].CellType  = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 7].CellType  = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 8].CellType  = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 9].CellType  = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 10].CellType = tmpCellType;

                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 11].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 12].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 13].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 14].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 15].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 16].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 17].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 18].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 19].CellType = tmpCellType;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 20].CellType = tmpCellType;

                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.FPS91_TY_S_AC_2948N801_Sheet1.Cells[i, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }

            if (this.FPS91_TY_S_AC_2948N801_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_2948N801_Sheet1.AlternatingRows[0].BackColor = Color.White;
        }
        #endregion
    }
}