using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 이체자료생성 확인 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.20 11:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53KBF755 : 이체자료생성 결과확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53KBG757 : 이체자료 생성 결과확인
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYHRFR02C1 : TYBase
    {
        private string fsPYYYMM;
        private string fsPYGUBN;
        private string fsPYJIDATE;

        #region Description : Page_Load
        public TYHRFR02C1(string sPYYYMM, string sPYGUBN, string sPYJIDATE)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsPYYYMM = sPYYYMM;
            fsPYGUBN = sPYGUBN;
            fsPYJIDATE = sPYJIDATE;
        }

        private void TYHRFR02C1_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_53KBG757_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_53KBG757_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 4);

            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[0, 0].Value = "펌뱅킹 이체자료 결과";

            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[1, 0].Value = "이체구분";
            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[1, 1].Value = "이체내용";
            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[1, 2].Value = "건    수";
            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[1, 3].Value = "이체금액";

            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_53KBG757_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        
            this.UP_DataBinding();
        }
        #endregion

        #region Description : UP_DataBinding()
        private void UP_DataBinding()
        {
            this.FPS91_TY_S_HR_53KBG757.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53KBF755", fsPYYYMM, fsPYGUBN, fsPYJIDATE);
            this.FPS91_TY_S_HR_53KBG757.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_HR_53KBG757.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_53KBG757, "CDDESC1", "합    계", SumRowType.Sum, "E2AMT");
            }
        }
        #endregion     

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

       
    }
}
