using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

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
    ///  TY_S_AC_7AGG2801 : 전년대비 영업비용 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  BCJDPAC : 귀속부서
    ///  BCJYYMM : 실적생성년월
    /// </summary>
    public partial class TYBSSJ012S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ012S()
        {
            InitializeComponent();
        }

        private void TYBSSJ012S_Load(object sender, System.EventArgs e)
        {
            CBH01_BCJDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            TXT01_BCJYYMM.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            this.SetStartingFocus(this.TXT01_BCJYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_7AGG2801.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AGG0800", this.TXT01_BCJYYMM.GetValue().ToString().Substring(0, 4), this.CBH01_BCJDPAC.GetValue().ToString());
            this.FPS91_TY_S_AC_7AGG2801.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_7AGG2801.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_7AGG2801.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_7AGG2801.GetValue(i, "ROWNUM").ToString() == "0")
                    {
                        this.FPS91_TY_S_AC_7AGG2801.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                        for (int j = 9; j < 26; j++)
                        {
                            this.FPS91_TY_S_AC_7AGG2801_Sheet1.Cells[i, j].ForeColor = Color.Red;
                        }
                    }

                    if (this.FPS91_TY_S_AC_7AGG2801.GetValue(i, "ROWNUM").ToString() == "9999")
                    {
                        this.FPS91_TY_S_AC_7AGG2801.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);

                        for (int j = 9; j < 26; j++)
                        {
                            this.FPS91_TY_S_AC_7AGG2801_Sheet1.Cells[i, j].ForeColor = Color.Red;
                        }
                    }
                }
            }

        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AGG0800", this.TXT01_BCJYYMM.GetValue().ToString().Substring(0, 4), this.CBH01_BCJDPAC.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            string sPREYEAR = (Convert.ToDouble(this.TXT01_BCJYYMM.GetValue().ToString().Substring(0, 4)) - 1).ToString();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYBSSJ012R(sPREYEAR);
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_TitleSetting()
        {
            string sPreYear = Convert.ToString(Convert.ToInt16(this.TXT01_BCJYYMM.GetValue().ToString().Substring(0, 4)) - 1);

            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 30; i++)
            {
                this.FPS91_TY_S_AC_7AGG2801_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 0].Value = "ROWNUM";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업년도";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 2].Value = "귀속부서";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정코드";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정과목";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정코드";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 6].Value = "계정과목";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 7].Value = "계정세목";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 8].Value = "세 목 명";

            this.FPS91_TY_S_AC_7AGG2801_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 3);

            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 9].Value = sPreYear + "년";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 9].Value = "1 ~ 9 실적";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 10].Value = "4/4 예상";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 11].Value = "년간실적";

            this.FPS91_TY_S_AC_7AGG2801_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 13);

            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 12].Value = this.TXT01_BCJYYMM.GetValue().ToString().Substring(0, 4) + "년";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 12].Value = "1월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 13].Value = "2월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 14].Value = "3월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 15].Value = "4월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 16].Value = "5월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 17].Value = "6월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 18].Value = "7월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 19].Value = "8월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 20].Value = "9월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 21].Value = "10월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 22].Value = "11월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 23].Value = "12월";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 24].Value = "합 계";
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[1, 25].Value = "전년대비 증감";

            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


        }
        #endregion
    }
}
