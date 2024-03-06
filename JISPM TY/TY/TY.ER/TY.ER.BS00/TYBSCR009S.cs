using System;
using System.Data;
using System.Drawing;
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
    public partial class TYBSCR009S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSCR009S()
        {
            InitializeComponent();
        }

        private void TYBSCR009S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            UP_Set_TitleSetting();

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_7AIIB845.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AII5844", this.TXT01_SDATE.GetValue().ToString());
            this.FPS91_TY_S_AC_7AIIB845.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_7AIIB845.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_7AIIB845.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_7AIIB845.GetValue(i, "ROWNUM").ToString() == "0")
                    {
                        this.FPS91_TY_S_AC_7AIIB845.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                    }

                    if (this.FPS91_TY_S_AC_7AIIB845.GetValue(i, "ROWNUM").ToString() == "-1")
                    {
                        this.FPS91_TY_S_AC_7AIIB845.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);

                       
                    }
                }
            }
        }
        #endregion             

        #region Description :  조회 그리드 타이트 조정 함수
        private void UP_Set_TitleSetting()
        {
            string sPYear = string.Empty;
            string sDYear = string.Empty;

            //전년도
            sPYear = Convert.ToString(Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) - 1);
            sDYear = Convert.ToString(Convert.ToInt16(TXT01_SDATE.GetValue().ToString()));

            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 24; i++)
            {
                this.FPS91_TY_S_AC_7AIIB845_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 0].Value = "사업년도";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 1].Value = "계정과목";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정과목";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정세목";

            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정세목";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정세목";


            this.FPS91_TY_S_AC_7AIIB845_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 3);
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 6].Value = "전 사";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 6].Value = sPYear+"년 실적";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 7].Value = sDYear+ "년 예산";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 8].Value = "증(감)";


            this.FPS91_TY_S_AC_7AIIB845_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 3);
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 9].Value = "UTT";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 9].Value = sPYear + "년 실적";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 10].Value = sDYear + "년 예산";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 11].Value = "증(감)";


            this.FPS91_TY_S_AC_7AIIB845_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 3);
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 12].Value = "UTT영업";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 12].Value = sPYear + "년 실적";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 13].Value = sDYear + "년 예산";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 14].Value = "증(감)";

            this.FPS91_TY_S_AC_7AIIB845_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 3);
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 15].Value = "SILO";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 15].Value = sPYear + "년 실적";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 16].Value = sDYear + "년 예산";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 17].Value = "증(감)";

            this.FPS91_TY_S_AC_7AIIB845_Sheet1.AddColumnHeaderSpanCell(0, 18, 1, 3);
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 18].Value = "SILO영업";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 18].Value = sPYear + "년 실적";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 19].Value = sDYear + "년 예산";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 20].Value = "증(감)";

            this.FPS91_TY_S_AC_7AIIB845_Sheet1.AddColumnHeaderSpanCell(0, 21, 1, 3);
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 21].Value = "관리.공통";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 21].Value = sPYear + "년 실적";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 22].Value = sDYear + "년 예산";
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[1, 23].Value = "증(감)";

            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AIIB845_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

    }
}
