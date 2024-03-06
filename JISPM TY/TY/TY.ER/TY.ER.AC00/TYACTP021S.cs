using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 수정신고 가산세 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.09.19 11:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_49JDV965 : 원천세 수정신고(가산세) 조회
    ///  TY_P_AC_49JDW966 : 원천세 수정신고(가산세) 수정
    ///  TY_P_AC_49JDX967 : 원천세 수정신고(가산세) 등록
    ///  TY_P_AC_49JDY968 : 원천세 수정신고(가산세) 삭제
    ///  TY_P_AC_4B3GF298 : 원천세 생성 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_AC_BCAEO906 : 원천세 수정신고(가산세) 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_382BD291 : 금액을 입력하세요.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_26I24916 : 일자를 확인하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  WABRANCH : 지점구분
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP021S : TYBase
    {
        public TYACTP021S()
        {
            InitializeComponent();
            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_BCAEQ907, "WASABUN", "KBHANGL", "WASABUN");
        }

        #region Description : 페이지 로드
        private void TYACTP021S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            UP_Spread_Title();

            this.SetStartingFocus(this.DTP01_WREYYMM);

            this.FPS91_TY_S_AC_BCAEQ907.Initialize();
            //this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            // 원본 소스(21.12.16)
            //this.DbConnector.Attach("TY_P_AC_BCAEO906", "1", Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));
            this.DbConnector.Attach("TY_P_AC_BCGEN911", "1", Get_Date(this.DTP01_WREYYMM.GetValue().ToString()));

            this.FPS91_TY_S_AC_BCAEQ907.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion


        

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);


            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 3);
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 3);
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);

            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 0].Value = "순번";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 1].Value = "구분";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 2].Value = "울산";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 5].Value = "서울";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 8].Value = "계";

            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 2].Value = "인원";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 3].Value = "과세표준액(소득세액)";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 4].Value = "납부세액";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 5].Value = "인원";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 6].Value = "과세표준액(소득세액)";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 7].Value = "납부세액";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 8].Value = "인원";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 9].Value = "과세표준액(소득세액)";
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 10].Value = "납부세액";

            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BCAEQ907_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
