using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 인건비 예산/실적 대비표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.10.02 15:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5AKHY019 : 인건비 예산 및 실적 대비표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5ALB7023 : 인건비 예산 및 실적 대비표
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EAYYMM : 기준년월
    /// </summary>
    public partial class TYHRPY020S : TYBase
    {
        #region Description : 폼 로드
        public TYHRPY020S()
        {
            InitializeComponent();
        }

        private void TYHRPY020S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_EAYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5ALB7023.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5AKHY019", this.DTP01_EAYYMM.GetString().ToString().Substring(0, 4),
                                                        this.DTP01_EAYYMM.GetString().ToString().Substring(4, 2)
                                                        );
            this.FPS91_TY_S_HR_5ALB7023.SetValue(this.DbConnector.ExecuteDataTable());

            UP_Spread_Title(this.DTP01_EAYYMM.GetString());

            this.SetSpreadSumRow(this.FPS91_TY_S_HR_5ALB7023, "GUBN", "합  계", SumRowType.Total);
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title(string sDATE)
        {
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_5ALB7023_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 3);
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3);

            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[0, 1].Value = "해당월" + "(" + sDATE.Substring(0, 4) + "년 " + sDATE.Substring(4, 2) + "월)";
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[0, 4].Value = "누적" + "(" + sDATE.Substring(0, 4) + "년 01월 ~ " + sDATE.Substring(0, 4) + "년 " + sDATE.Substring(4, 2) + "월)";

            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[1, 1].Value = "인원";
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[1, 2].Value = "예산";
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[1, 3].Value = "집행";

            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[1, 4].Value = "인원";
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[1, 5].Value = "예산";
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[1, 6].Value = "집행";

            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_5ALB7023_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
