using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 사업계획 손익계산서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.01 16:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_791GG520 : 사업계획 손익계산서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_7AH9Y810 : 사업계획 손익계산서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYBSCR008S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSCR008S()
        {
            InitializeComponent();
        }

        private void TYBSCR008S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));            

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_7AH9Y810.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AH9W809", this.TXT01_SDATE.GetValue().ToString());
            this.FPS91_TY_S_AC_7AH9Y810.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion             

        #region Description :  조회(귀속부서별) 그리드 타이트 조정 함수
        private void UP_Set_TitleSetting()
        {
            string sPYear = string.Empty;

            //전년도
            sPYear = Convert.ToString(Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) - 1);

            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
                       

            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 0].Value = "계정과목";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 1].Value = "계 정 명";

            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 3);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 2].Value = "UTT";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 2].Value = sPYear+"년";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 3].Value = TXT01_SDATE.GetValue().ToString()+"년";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 4].Value = "증(감)";

            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 3);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 5].Value = "SILO";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 5].Value = sPYear + "년";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 6].Value = TXT01_SDATE.GetValue().ToString() + "년";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 7].Value = "증(감)";

            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 8].Value = "전 사";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 8].Value = sPYear + "년";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 9].Value = TXT01_SDATE.GetValue().ToString() + "년";
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[1, 10].Value = "증(감)";


            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AH9Y810_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;



        }
        #endregion

    }
}
