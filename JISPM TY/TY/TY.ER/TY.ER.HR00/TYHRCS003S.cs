using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연장관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPJQ529 : 연장관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_HR_616BB408 : 연장관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  GYSABUN : 사  번
    ///  GYGUBN : 신청형태
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRCS003S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRCS003S()
        {
            InitializeComponent();
        }

        private void TYHRCS003S_Load(object sender, System.EventArgs e)
        {
            this.UP_Spread_Title();

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_617BE430.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_617AY425", this.DTP01_STDATE.GetValue().ToString(),
                                                        this.DTP01_EDDATE.GetValue().ToString(),
                                                        this.DTP01_STDATE.GetValue().ToString(),
                                                        this.DTP01_EDDATE.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_617BE430.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_617BE430_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);

            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 2].Value = "총 무";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 6].Value = "전 산";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 9].Value = "회 계";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 12].Value = "관 리";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 13].Value = "SILO영업";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 16].Value = "SILO운영";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 19].Value = "SILO";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 21].Value = "UTT운영";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 24].Value = "UTT영업";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 27].Value = "UTT";


            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 4);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 2].Value = "조 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 3].Value = "중 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 4].Value = "석 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 5].Value = "간 식";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 3);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 6].Value = "조 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 7].Value = "중 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 8].Value = "석 식";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 3);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 9].Value = "조 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 10].Value = "중 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 11].Value = "석 식";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 1);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 12].Value = "외래";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 3);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 13].Value = "조 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 14].Value = "중 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 15].Value = "석 식";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 16, 1, 3);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 16].Value = "조 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 17].Value = "중 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 18].Value = "석 식";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 19, 1, 2);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 19].Value = "용 역";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 20].Value = "외 래";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 21, 1, 3);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 21].Value = "조 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 22].Value = "중 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 23].Value = "석 식";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 24, 1, 3);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 24].Value = "조 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 25].Value = "중 식";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 26].Value = "석 식";

            this.FPS91_TY_S_HR_617BE430_Sheet1.AddColumnHeaderSpanCell(0, 27, 1, 2);
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 27].Value = "용 역";
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[1, 28].Value = "외 래";



            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 24].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_617BE430_Sheet1.ColumnHeader.Cells[0, 27].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;            

        }
        #endregion
    }
}