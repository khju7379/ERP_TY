using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 월별 급여총액 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.09.22 15:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_59UES938 : 월별 급여총액 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_59P9J905 : 월별 급여총액 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PEYEAR : 년도
    /// </summary>
    public partial class TYHRPY019S : TYBase
    {
        #region Descriptoin : 폼 로드 이벤트
        public TYHRPY019S()
        {
            InitializeComponent();
        }

        private void TYHRPY019S_Load(object sender, System.EventArgs e)
        {
            UP_Spread_Title();

            this.SetStartingFocus(this.TXT01_PEYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_59P9J905.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_59UES938", this.TXT01_PEYEAR.GetValue().ToString() + "01",
                                                            this.TXT01_PEYEAR.GetValue().ToString() + "12"
                                   );
            this.FPS91_TY_S_HR_59P9J905.SetValue(this.DbConnector.ExecuteDataTable());


            //for (int i = 0; i < this.FPS91_TY_S_HR_59P9J905.ActiveSheet.RowCount; i++)
            //{
            //    if (this.FPS91_TY_S_HR_59P9J905.GetValue(i, "B2NOLN").ToString() != "01")
            //    {
            //        this.FPS91_TY_S_HR_59P9J905_Sheet1.Cells[i, 30].CellType = new FarPoint.Win.Spread.CellType.ButtonCellType();
            //    }
            //    else
            //    {
            //        //this.FPS91_TY_S_AC_2BF55357_Sheet1.Cells[i, 28].Image = global::TY.Service.Library.Properties.Resources.magnifier;
            //    }
            //}

            
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_59P9J905_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_59P9J905_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_59P9J905_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 2);
            this.FPS91_TY_S_HR_59P9J905_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 3);
            this.FPS91_TY_S_HR_59P9J905_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 4);
            this.FPS91_TY_S_HR_59P9J905_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 4);
            this.FPS91_TY_S_HR_59P9J905_Sheet1.AddColumnHeaderSpanCell(0, 14, 1, 4);

            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 1].Value = "임 원";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 3].Value = "연봉직";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 6].Value = "관리직";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 10].Value = "운영직";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 14].Value = "계";

            //this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 0].Value = "구분";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 1].Value = "급여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 2].Value = "상여";

            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 3].Value = "급여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 4].Value = "상여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 5].Value = "보전";

            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 6].Value = "급여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 7].Value = "상여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 8].Value = "OT";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 9].Value = "보전";

            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 10].Value = "급여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 11].Value = "상여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 12].Value = "OT";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 13].Value = "보전";

            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 14].Value = "급여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 15].Value = "상여";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 16].Value = "OT";
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[1, 17].Value = "보전";

            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59P9J905_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
