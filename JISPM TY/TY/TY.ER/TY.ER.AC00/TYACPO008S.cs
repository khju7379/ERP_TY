using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

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
    public partial class TYACPO008S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACPO008S()
        {
            InitializeComponent();
        }

        private void TYACPO008S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_AC_37A35071.Visible = true;
            this.FPS91_TY_S_AC_37C9S091.Visible = false; 

            //월별기준
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_37A35071_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);    
        
            this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 3);            
            this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);

            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 0].Value = "년  월";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 1].Value = "부  서";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 2].Value = "부서명";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 3].Value = "분류코드";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 4].Value = "분류명";            
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 5].Value = "월   계";

            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 5].Value = "매출액";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 6].Value = "매출원가";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 7].Value = "매출이익";

            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 8].Value = "누   계";

            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 8].Value = "매출액";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 9].Value = "매출원가";
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 10].Value = "매출이익";

            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //전년기준
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_37C9S091_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);

            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);
            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_37C9S091_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 0].Value = "년  월";
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 1].Value = "부  서";
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 2].Value = "부서명";
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 3].Value = "분류코드";
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 4].Value = "분류명";

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 5].Value = "전년도 누계";

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[1, 5].Value = "매출액";
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[1, 6].Value = "매출이익";

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 7].Value = "당해년도 누계";

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[1, 7].Value = "매출액";
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[1, 8].Value = "매출이익";

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 9].Value = "전년대비(증감)";

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[1, 9].Value = "매출액";
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[1, 10].Value = "매출이익";

            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_37C9S091_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            
            this.DTP01_ERSYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_ERSCDDP.DummyValue = this.DTP01_ERSYYMM.GetString().ToString().Substring(0,6)  + "01";

            this.SetStartingFocus(this.DTP01_ERSYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            
            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.FPS91_TY_S_AC_37A35071.Visible = true;
                this.FPS91_TY_S_AC_37C9S091.Visible = false; 
 
                this.FPS91_TY_S_AC_37A35071.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_37A2X065", this.DTP01_ERSYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ERSCDDP.GetValue());
                this.FPS91_TY_S_AC_37A35071.SetValue(this.DbConnector.ExecuteDataTable());

                if (FPS91_TY_S_AC_37A35071.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_37A35071, "ERSCDDP", "소 계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_37A35071, "ERSYYMM", "합 계", SumRowType.Total);
                }
            }
            else
            {
                this.FPS91_TY_S_AC_37A35071.Visible = false;
                this.FPS91_TY_S_AC_37C9S091.Visible = true;

                this.FPS91_TY_S_AC_37C9S091.Initialize();

                string sPreYear = Convert.ToString(Convert.ToInt16(this.DTP01_ERSYYMM.GetString().ToString().Substring(0, 4)) - 1);
                string sPreMonth = this.DTP01_ERSYYMM.GetString().ToString().Substring(4, 2);
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_37C9K090", this.DTP01_ERSYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ERSCDDP.GetValue(), this.DTP01_ERSYYMM.GetString().ToString().Substring(0, 6),
                                                            sPreYear + sPreMonth);
                this.FPS91_TY_S_AC_37C9S091.SetValue(this.DbConnector.ExecuteDataTable());

                if (FPS91_TY_S_AC_37C9S091.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_37C9S091, "ERCDDP", "소 계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_37C9S091, "ERYYMM", "합 계", SumRowType.Total);
                }
            }

        }
        #endregion

        #region  Description : DTP01_ERSYYMM_ValueChanged 이벤트
        private void DTP01_ERSYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_ERSCDDP.DummyValue = this.DTP01_ERSYYMM.GetString().ToString().Substring(0,6)  + "01";
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO008B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
