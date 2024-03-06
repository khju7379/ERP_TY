using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 휴무관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.26 14:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BQF4543 : 휴무관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_B9DH1561 : 휴무관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  GHCODE : 휴무코드
    ///  GHSABUN : 사   번
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    ///  KBBUSEO : 부서
    /// </summary>
    public partial class TYHRGT013S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRGT013S()
        {
            InitializeComponent();
        }

        private void TYHRGT013S_Load(object sender, System.EventArgs e)
        {

            this.UP_Set_SpreadTitle();

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));           

            this.SetStartingFocus(this.DTP01_SDATE);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {           

            this.FPS91_TY_S_HR_B9DH1561.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_B9DH0559", this.DTP01_SDATE.GetString().Substring(0,4),   this.CBH01_GHSABUN.GetValue());
            this.FPS91_TY_S_HR_B9DH1561.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : UP_Set_SpreadTitle() 이벤트
        private void UP_Set_SpreadTitle()
        {
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.RowHeaderColumnCount = 1;

            
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);

            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 0].Value = "사  번";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 1].Value = "이  름";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 2].Value = "기준월";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 3].Value = "입사일자";

            this.FPS91_TY_S_HR_B9DH1561_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2);
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 4].Value = "발  생";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[1, 4].Value = "년    차";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[1, 5].Value = "하기휴가";

            this.FPS91_TY_S_HR_B9DH1561_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 3);
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 6].Value = "사  용";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[1, 6].Value = "년    차";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[1, 7].Value = "반 년 차";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[1, 8].Value = "하기휴가";

            this.FPS91_TY_S_HR_B9DH1561_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 9].Value = "잔  여";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[1, 9].Value = "년    차";
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[1, 10].Value = "하기휴가";



            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_B9DH1561_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

     

    }
}
