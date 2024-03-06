using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 장기재고현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.15 13:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37F21104 : EIS 장기재고현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37F22105 : EIS 장기재고현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  EROCDDP : 사업부
    ///  EROYYMM : 년월
    /// </summary>
    public partial class TYACPO009S : TYBase
    {
        #region  Description : 생성 버튼 이벤트
        public TYACPO009S()
        {
            InitializeComponent();
        }

        private void TYACPO009S_Load(object sender, System.EventArgs e)
        {           
            
            this.DTP01_EROYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_EROCDDP.DummyValue = this.DTP01_EROYYMM.GetString().ToString().Substring(0, 6) + "01";
            
            UP_Set_SpreadTitle(DateTime.Now.AddYears(-1).ToString("yyyy")+"12", DateTime.Now.ToString("yyyyMM"));

            this.SetStartingFocus(this.DTP01_EROYYMM);

        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO009B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //집계
            this.FPS91_TY_S_AC_37GBB115.Initialize(); 

            DateTime dt = Convert.ToDateTime(this.DTP01_EROYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_EROYYMM.GetString().ToString().Substring(4, 2) + "-" + "01");

            dt = dt.AddYears(-1);

            string sKIJUNDATE = Convert.ToString(dt.Year) + "12";

            UP_Set_SpreadTitle(sKIJUNDATE, this.DTP01_EROYYMM.GetString().ToString().Substring(0, 6));

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37GB4114", sKIJUNDATE, this.DTP01_EROYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_37GBB115.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_37GBB115.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_37GBB115, "EITCODE", "소 계", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_37GBB115, "EROCDDP", "합 계", SumRowType.Total);
            }

            //상세
            this.FPS91_TY_S_AC_37F22105.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37F21104", this.DTP01_EROYYMM.GetString().ToString().Substring(0, 6), this.CBH01_EROCDDP.GetValue());
            this.FPS91_TY_S_AC_37F22105.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : DTP01_EROYYMM_ValueChanged 이벤트
        private void DTP01_EROYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_EROCDDP.DummyValue = this.DTP01_EROYYMM.GetString().ToString().Substring(0, 6) + "01";
        }
        #endregion

        #region  Description : UP_Set_SpreadTitle() 이벤트
        private void UP_Set_SpreadTitle(string sJunYearDate, string sDangWolDate)
        {
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_37GBB115_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_37GBB115_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_37GBB115_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_37GBB115_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);

            this.FPS91_TY_S_AC_37GBB115_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 2);
            this.FPS91_TY_S_AC_37GBB115_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);
            this.FPS91_TY_S_AC_37GBB115_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 0].Value = "사업부";
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 1].Value = "품  목";
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 2].Value = "품  명";

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 3].Value = sJunYearDate.Substring(0, 4) + "년" + sJunYearDate.Substring(4, 2) + "월 기준 장기재고";

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[1, 3].Value = "수 량";
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[1, 4].Value = "금 액";

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 5].Value = sDangWolDate.Substring(0, 4) + "년 증(감) 장기재고";

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[1, 5].Value = "수 량";
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[1, 6].Value = "금 액";

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 7].Value = sDangWolDate.Substring(0, 4) + "년" + sDangWolDate.Substring(4, 2) + "월 기준 장기재고";

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[1, 7].Value = "수 량";
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[1, 8].Value = "금 액";

            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_37GBB115_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

    }
}
