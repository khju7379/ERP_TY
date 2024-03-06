using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 근태집계현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.12.11 13:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5CBDG304 : 근태집계현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5CBDG305 : 근태집계현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    /// </summary>
    public partial class TYHRGT011S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRGT011S()
        {
            InitializeComponent();
        }

        private void TYHRGT011S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.UP_Set_SpreadTitle();
            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5CBDG304", this.DTP01_SDATE.GetString().ToString());
            this.FPS91_TY_S_HR_5CBDG305.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : UP_Set_SpreadTitle() 이벤트
        private void UP_Set_SpreadTitle()
        {
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 1].Value = "소  속";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 3].Value = "부  서";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 5].Value = "직  위";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 7].Value = "성  명";

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 8].Value = "휴 가";

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 8].Value = "년 차";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 9].Value = "반 차";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 10].Value = "하 기";

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 12);
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 11].Value = "지 각";

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 11].Value = "1월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 12].Value = "2월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 13].Value = "3월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 14].Value = "4월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 15].Value = "5월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 16].Value = "6월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 17].Value = "7월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 18].Value = "8월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 19].Value = "9월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 20].Value = "10월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 21].Value = "11월";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[1, 22].Value = "12월";

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 23, 2, 1);
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.AddColumnHeaderSpanCell(0, 24, 2, 1);

            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 23].Value = "조 퇴";
            this.FPS91_TY_S_HR_5CBDG305_Sheet1.ColumnHeader.Cells[0, 24].Value = "결 근";

        }
        #endregion
    }
}
