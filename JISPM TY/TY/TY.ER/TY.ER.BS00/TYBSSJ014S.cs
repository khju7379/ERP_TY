using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 전년대비 영업비용 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.10.13 16:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_7ADGA779 : 전년대비 영업비용 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_7AK9Y857 : 전년대비 영업비용 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  BCJDPAC : 귀속부서
    ///  BCJYYMM : 실적생성년월
    /// </summary>
    public partial class TYBSSJ014S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ014S()
        {
            InitializeComponent();
        }

        private void TYBSSJ014S_Load(object sender, System.EventArgs e)
        {           

            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            UP_Set_TitleSetting();

            this.SetStartingFocus(this.TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_7AK9Y857.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AK9T856", this.TXT01_SDATE.GetValue().ToString().Substring(0, 4), this.CBO01_INQOPTION.GetValue().ToString());
            this.FPS91_TY_S_AC_7AK9Y857.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_7AK9Y857.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_7AK9Y857.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_7AK9Y857.GetValue(i, "ROWNUM").ToString() == "0")
                    {
                        this.FPS91_TY_S_AC_7AK9Y857.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                       
                    }

                    if (this.FPS91_TY_S_AC_7AK9Y857.GetValue(i, "ROWNUM").ToString() == "-1")
                    {
                        this.FPS91_TY_S_AC_7AK9Y857.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);

                    }
                }
            }

        }
        #endregion     

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_TitleSetting()
        {

            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 13; i++)
            {
                this.FPS91_TY_S_AC_7AK9Y857_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 0].Value = "ROWNUM";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업년도";            
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정코드";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정코드";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정과목";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정세목";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 6].Value = "세 목 명";

            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 7].Value = "UTT";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[1, 7].Value = "계 획";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[1, 8].Value = "실 적";

            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 9].Value = "SILO";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[1, 9].Value = "계 획";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[1, 10].Value = "실 적";

            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 11].Value = "합 계";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[1, 11].Value = "계 획";
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[1, 12].Value = "실 적";

            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AK9Y857_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;                       
        }
        #endregion
    }
}
