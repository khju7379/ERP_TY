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
    /// 연말정산 기부금 명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.12.07 10:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_7C7AG200 : 연말정산 기부금 명세서 조회(당해년도)
    ///  TY_P_HR_7C7AI201 : 연말정산 기부금 조정명세서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_7C7AJ202 : 연말정산 기부금 명세서 조회(당해년도)
    ///  TY_S_HR_7C7AK203 : 연말정산 기부금 조정명세서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT012S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRNT012S()
        {
            InitializeComponent();
        }

        private void TYHRNT012S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            //UP_Spread_Title();

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //당해년도
            this.FPS91_TY_S_HR_7C7AJ202.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C7AG200", "TY", TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), CBH01_DODONATION_CD.GetValue(), TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString() );
            this.FPS91_TY_S_HR_7C7AJ202.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_7C7AJ202.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_7C7AJ202, "DOSABUNNM", "합   계", SumRowType.Sum, "DOAMOUNT", "DOENCAMOUNT", "DOCONB_SUM");
            }

            //조정명세서(당해년도)
            this.FPS91_TY_S_HR_7C7AK203.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C7AI201", "TY", TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), CBH01_DODONATION_CD.GetValue(), TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString());
            this.FPS91_TY_S_HR_7C7AK203.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_7C7AK203.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_7C7AK203, "DASABUNNM", "합   계", SumRowType.Sum, "DADONATION_AMOUNT", "DABFDEDAMOUNT", "DADEDAMOUNT", "DACURRAMOUNT", "DAEXPIREDEDAMOUNT", "DATRANSDEDAMOUNT");
            }

            //조정명세서(전년도)
            this.FPS91_TY_S_HR_7C7D7206.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C7D6205", "TY", TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), CBH01_DODONATION_CD.GetValue());
            this.FPS91_TY_S_HR_7C7D7206.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_7C7D7206.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_7C7D7206, "DASABUNNM", "합   계", SumRowType.Sum, "DADONATION_AMOUNT", "DABFDEDAMOUNT", "DADEDAMOUNT", "DACURRAMOUNT", "DAEXPIREDEDAMOUNT", "DATRANSDEDAMOUNT");
            }

        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 0].Value = "회사";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 1].Value = "귀속년도";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 2].Value = "사 번";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 3].Value = "성 명";

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 4].Value = "사업자번호";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 5].Value = "상      호";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 6].Value = "의료증빙";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 7].Value = "의료증빙명";

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 8].Value = "건 수";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 9].Value = "의료비금액";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 10].Value = "난임시술";

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 11].Value = "주민번호";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 12].Value = "성  명";
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[1, 13].Value = "본인등해당여부";            

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 3);
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 1].Value = "소득자[연말정산 신청자]";

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 4);
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 4].Value = "[지 급 처]";

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 8].Value = "[지급명세]";

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 3);
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 11].Value = "[의료비 공제 대상자]";

            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7C7AJ202_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
