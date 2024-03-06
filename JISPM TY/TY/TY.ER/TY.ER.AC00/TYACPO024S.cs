using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 채권현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.09.27 10:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_39RA6879 : EIS 채권현황 조회[요약]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_39RA8882 : EIS 채권현황 조회[요약]
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO024S : TYBase
    {
        #region  Description : 폼로드 이벤트
        public TYACPO024S()
        {
            InitializeComponent();
        }

        private void TYACPO024S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            UP_Set_SpreadTitle(DateTime.Now.AddYears(-1).ToString("yyyy") + "12", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO024B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_39RA8882.Initialize(); 

            DateTime dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-" + "01");

            dt = dt.AddYears(-1);

            string sJWDATE = Convert.ToString(dt.Year) + "12";

            UP_Set_SpreadTitle(sJWDATE, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39RA6879", this.DTP01_GSTYYMM.GetString().Substring(0, 6), sJWDATE);

            DataTable dk = this.DbConnector.ExecuteDataTable();

            if (dk.Rows.Count > 2)
            {
                this.FPS91_TY_S_AC_39RA8882.SetValue(dk);

                if (this.FPS91_TY_S_AC_39RA8882.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_39RA8882, "EIMSAUP", "A00000", SumRowType.SubTotal);
                }
            }
            
        }
        #endregion

        #region  Description : UP_Set_SpreadTitle() 이벤트
        private void UP_Set_SpreadTitle(string sJunYearDate, string sDangWolDate)
        {
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_39RA8882_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);

            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 2);
            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2);
            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);
            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);
            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_39RA8882_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);

            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 0].Value = "년  월";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업부";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 2].Value = "사업부명";

            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 3].Value = sJunYearDate.Substring(0, 4) + "년(연말) 기준";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 5].Value = sDangWolDate.Substring(0, 4) + "년 " + sDangWolDate.Substring(4, 2) + "월 기준";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 7].Value = "증감액";
            
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[1, 3].Value = "채권금액";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[1, 4].Value = "대손충당금";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[1, 5].Value = "채권금액";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[1, 6].Value = "대손충당금";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[1, 7].Value = "채권금액";
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[1, 8].Value = "대손충당금";

            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39RA8882_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;            
            
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_39RA8882_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_39RA8882_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_39RA8882.GetValue("EIMSAUP").ToString() != "A00000")
            {
                DateTime dt = Convert.ToDateTime(this.FPS91_TY_S_AC_39RA8882.GetValue("EIMYYMM").ToString().Substring(0, 4) + "-" + this.FPS91_TY_S_AC_39RA8882.GetValue("EIMYYMM").ToString().Substring(4, 2) + "-" + "01");

                dt = dt.AddYears(-1);

                string sJWDATE = Convert.ToString(dt.Year) + "12";

                if (this.OpenModalPopup(new TYACPO024P(sJWDATE, this.FPS91_TY_S_AC_39RA8882.GetValue("EIMYYMM").ToString(), this.FPS91_TY_S_AC_39RA8882.GetValue("EIMSAUP").ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
