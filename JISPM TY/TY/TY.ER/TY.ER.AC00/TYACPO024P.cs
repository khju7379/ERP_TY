using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 채권현황 상세조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.09.27 14:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_39RA7880 : EIS 채권현황 조회[상세]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_39RA1883 : EIS 채권현황 조회[상세]
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYACPO024P : TYBase
    {
        private string fsJunYYMM;
        private string fsDangYYMM;
        private string fsSAUP;


        #region  Description : 폼 로드 이벤트
        public TYACPO024P(string sJunYYMM, string sDangYYMM, string sSAUP)
        {
            InitializeComponent();

            fsJunYYMM = sJunYYMM;
            fsDangYYMM = sDangYYMM;
            fsSAUP = sSAUP;
        }

        private void TYACPO024P_Load(object sender, System.EventArgs e)
        {
            UP_Set_SpreadTitle(fsJunYYMM, fsDangYYMM);

            UP_Search();
        }
        #endregion

        #region  Description : 조회 이벤트
        private void UP_Search()
        {
            this.FPS91_TY_S_AC_39RA1883.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39RA7880", fsDangYYMM, fsJunYYMM, fsDangYYMM, fsJunYYMM, fsSAUP, fsSAUP, fsSAUP);
            this.FPS91_TY_S_AC_39RA1883.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_39RA1883.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_39RA1883, "EISVENDNM", "합      계", SumRowType.Total);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_39RA1883, "EISVENDNM", "장기채권 계", SumRowType.SubTotal);
            }

        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region  Description : UP_Set_SpreadTitle() 이벤트
        private void UP_Set_SpreadTitle(string sJunYearDate, string sDangWolDate)
        {
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_39RA1883_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);

            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);

            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 2);
            this.FPS91_TY_S_AC_39RA1883_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);

            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 0].Value = "사업부";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업부명";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 2].Value = "구분";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 3].Value = "구분명";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 4].Value = "거래처";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 5].Value = "거래처명";

            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 6].Value = sJunYearDate.Substring(0, 4) + "년(연말) 기준";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 8].Value = sDangWolDate.Substring(0, 4) + "년 " + sDangWolDate.Substring(4, 2) + "월 기준";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 10].Value = "증감액";

            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[1, 6].Value = "채권금액";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[1, 7].Value = "대손충당금";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[1, 8].Value = "채권금액";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[1, 9].Value = "대손충당금";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[1, 10].Value = "채권금액";
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[1, 11].Value = "대손충당금";

            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39RA1883_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion
    }
}
