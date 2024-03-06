using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 월별 4대보험 현황표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.10.21 15:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29C7M958 : 자동순번 가져오기
    ///  TY_P_HR_5AMGS031 : 월별 4대보험 현황 임시테이블 생성
    ///  TY_P_HR_5ATF7049 : 월별 4대보험 현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5AMGS032 : 월별 4대보험 현황표
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRPY021S : TYBase
    {   
        private string fsSessionId = string.Empty;

        #region Description : 폼 로드
        public TYHRPY021S()
        {
            InitializeComponent();
        }

        private void TYHRPY021S_Load(object sender, System.EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            this.fsSessionId = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            UP_Spread_Title();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            // 임시테이블 생성
            string sRET_MSG = string.Empty;
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5AMGS031", this.fsSessionId,
                                                        this.DTP01_GSTYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_GEDYYMM.GetString().Substring(0, 6),
                                                        TYUserInfo.EmpNo,
                                                        sRET_MSG
                                                        );
            sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.FPS91_TY_S_HR_5AMGS032.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5ATF7049", this.fsSessionId,
                                                        this.DTP01_GSTYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_GEDYYMM.GetString().Substring(0, 6)
                                                        );

            this.FPS91_TY_S_HR_5AMGS032.SetValue(this.DbConnector.ExecuteDataTable());

            this.SetSpreadSumRow(this.FPS91_TY_S_HR_5AMGS032, "SUSOSOK", "합  계", SumRowType.Total);

        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_5AMGS032_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 2);
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 2);
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);

            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 1].Value = "고용보험";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 3].Value = "산재보험";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 5].Value = "건강보험";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 7].Value = "국민연금";

            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 1].Value = "인원";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 2].Value = "금액";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 3].Value = "인원";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 4].Value = "금액";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 5].Value = "인원";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 6].Value = "금액";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 7].Value = "인원";
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[1, 8].Value = "금액";

            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_5AMGS032_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
