using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 정산현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.12.12 17:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_7CD9H259 : 연말정산 정산현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_7CD9H260 : 연말정산 정산현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT013S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRNT013S()
        {
            InitializeComponent();
        }

        private void TYHRNT013S_Load(object sender, System.EventArgs e)
        {            
            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            UP_Spread_Title();

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_7CD9H260.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CD9H259", "TY", TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), CBO01_INQOPTION.GetValue().ToString(), CBO01_KBGUNMU.GetValue().ToString(), TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString());
            this.FPS91_TY_S_HR_7CD9H260.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_7CD9H260.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_7CD9H260, "WNSABUNNM", "합   계", SumRowType.Sum,"WNTAXTARGETPAY", "WNPAYMHAP", "WNPAYSHAP", "WNPAYEXHAP", "WNPREPAYMHAP", "WNPREPAYSHAP", "WNFIXTAX", "WNFIXRESIDENCETAX", "WNPEINCOMETAX", "WNPERESIDENCETAX", "WNCUINCOMETAX", "WNCURESIDENCETAX", "WNCOLLECTTAX", "WNCOLLECTRESIDENCETAX");                
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 8; i++)
            {
                this.FPS91_TY_S_HR_7CD9H260_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 0].Value = "사 번";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 1].Value = "성 명";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 2].Value = "주민번호";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 3].Value = "증번호";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 4].Value = "입사일";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 5].Value = "정산구분";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 6].Value = "정산구분";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 7].Value = "총 급여";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 8].Value = "근로소득공제";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 9].Value = "근로소득금액";            

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 3);

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 10].Value = "현 근무지 소득";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 10].Value = "급 여";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 11].Value = "상 여";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 12].Value = "기타소득";

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2);

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 13].Value = "전 근무지 소득";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 13].Value = "급 여";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 14].Value = "상 여";

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 3);

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 15].Value = "결정세액";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 15].Value = "소득세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 16].Value = "주민세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 17].Value = "농어촌세";

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.AddColumnHeaderSpanCell(0, 18, 1, 3);
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 18].Value = "전근무지 세액";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 18].Value = "소득세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 19].Value = "주민세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 20].Value = "농어촌세";

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.AddColumnHeaderSpanCell(0, 21, 1, 3);
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 21].Value = "현근무지 세액";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 21].Value = "소득세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 22].Value = "주민세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 23].Value = "농어촌세";

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.AddColumnHeaderSpanCell(0, 24, 1, 3);
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 24].Value = "차감세액";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 24].Value = "소득세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 25].Value = "주민세";
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[1, 26].Value = "농어촌세";

            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CD9H260_Sheet1.ColumnHeader.Cells[0, 24].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

    }
}
