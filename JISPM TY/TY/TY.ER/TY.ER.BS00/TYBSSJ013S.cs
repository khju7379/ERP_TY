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
    ///  TY_S_AC_7AJEZ850 : 전년대비 영업비용 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  BCJDPAC : 귀속부서
    ///  BCJYYMM : 실적생성년월
    /// </summary>
    public partial class TYBSSJ013S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ013S()
        {
            InitializeComponent();
        }

        private void TYBSSJ013S_Load(object sender, System.EventArgs e)
        {
            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            RDB01_CHK1.Checked = true;
            RDB01_CHK2.Checked = false;
            RDB01_CHK3.Checked = false;

            UP_Set_HapTitleSetting();
            UP_Set_UYTitleSetting();
            UP_Set_CMTitleSetting();

            this.SetStartingFocus(this.TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {
                UP_Set_HapTitleSetting();

                this.FPS91_TY_S_AC_7AJEZ850.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7AJEV849", this.TXT01_SDATE.GetValue().ToString().Substring(0, 4));
                this.FPS91_TY_S_AC_7AJEZ850.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_AC_7AJEZ850.CurrentRowCount > 0)
                {
                    for (int i = 0; i < this.FPS91_TY_S_AC_7AJEZ850.CurrentRowCount; i++)
                    {
                        if (this.FPS91_TY_S_AC_7AJEZ850.GetValue(i, "ROWNUM").ToString() == "0")
                        {
                            this.FPS91_TY_S_AC_7AJEZ850.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                        }

                        if (this.FPS91_TY_S_AC_7AJEZ850.GetValue(i, "ROWNUM").ToString() == "-1")
                        {
                            this.FPS91_TY_S_AC_7AJEZ850.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                        }
                    }
                }
            }
            else
            {
                if (RDB01_CHK1.Checked == true || RDB01_CHK2.Checked == true)
                {
                    UP_Set_UYTitleSetting();

                    FPS91_TY_S_AC_7AJI1854.Visible = true;
                    FPS91_TY_S_AC_7AJI2855.Visible = false;

                    string sGubn = RDB01_CHK1.Checked == true ? "T1" : "T4" ;

                    this.FPS91_TY_S_AC_7AJI1854.Initialize();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_7AJHM852", this.TXT01_SDATE.GetValue().ToString().Substring(0, 4), sGubn);
                    this.FPS91_TY_S_AC_7AJI1854.SetValue(this.DbConnector.ExecuteDataTable());

                    if (this.FPS91_TY_S_AC_7AJI1854.CurrentRowCount > 0)
                    {
                        for (int i = 0; i < this.FPS91_TY_S_AC_7AJI1854.CurrentRowCount; i++)
                        {
                            if (this.FPS91_TY_S_AC_7AJI1854.GetValue(i, "ROWNUM").ToString() == "0")
                            {
                                this.FPS91_TY_S_AC_7AJI1854.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                            }

                            if (this.FPS91_TY_S_AC_7AJI1854.GetValue(i, "ROWNUM").ToString() == "-1")
                            {
                                this.FPS91_TY_S_AC_7AJI1854.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                            }
                        }
                    }
                }
                else
                {
                    UP_Set_CMTitleSetting();

                    FPS91_TY_S_AC_7AJI1854.Visible = false;
                    FPS91_TY_S_AC_7AJI2855.Visible = true;

                    this.FPS91_TY_S_AC_7AJI2855.Initialize();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_7AJI0853", this.TXT01_SDATE.GetValue().ToString().Substring(0, 4));
                    this.FPS91_TY_S_AC_7AJI2855.SetValue(this.DbConnector.ExecuteDataTable());

                    if (this.FPS91_TY_S_AC_7AJI2855.CurrentRowCount > 0)
                    {
                        for (int i = 0; i < this.FPS91_TY_S_AC_7AJI2855.CurrentRowCount; i++)
                        {
                            if (this.FPS91_TY_S_AC_7AJI2855.GetValue(i, "ROWNUM").ToString() == "0")
                            {
                                this.FPS91_TY_S_AC_7AJI2855.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                            }

                            if (this.FPS91_TY_S_AC_7AJI2855.GetValue(i, "ROWNUM").ToString() == "-1")
                            {
                                this.FPS91_TY_S_AC_7AJI2855.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                            }
                        }
                    }
                }
            }

        }
        #endregion      

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_HapTitleSetting()
        {

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 15; i++)
            {
                this.FPS91_TY_S_AC_7AJEZ850_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 0].Value = "ROWNUM";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업년도";            
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정과목";

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정세목";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 6].Value = "계정세목";            

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.AddColumnHeaderSpanCell(0, 7 , 1, 2);
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 7].Value = "운영원가";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 7].Value = "계 획";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 8].Value = "실 적";

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 9].Value = "판매비";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 9].Value = "계 획";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 10].Value = "실 적";

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 11].Value = "관리공통비";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 11].Value = "계 획";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 12].Value = "실 적";

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2);
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 13].Value = "전 사";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 13].Value = "계 획";
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[1, 14].Value = "실 적";           

            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJEZ850_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;                       

        }

        private void UP_Set_UYTitleSetting()
        {
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 14; i++)
            {
                this.FPS91_TY_S_AC_7AJI1854_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 0].Value = "ROWNUM";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업년도";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정세목";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 6].Value = "계정세목";

            this.FPS91_TY_S_AC_7AJI1854_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 7].Value = "UTT";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[1, 7].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[1, 8].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI1854_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 9].Value = "SILO";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[1, 9].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[1, 10].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI1854_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 11].Value = "운영원가계";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[1, 11].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[1, 12].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJI1854_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;            
        }

        private void UP_Set_CMTitleSetting()
        {
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 19; i++)
            {
                this.FPS91_TY_S_AC_7AJI2855_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 0].Value = "ROWNUM";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업년도";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정과목";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정세목";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 6].Value = "계정세목";

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 7].Value = "관리팀";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 7].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 8].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 9].Value = "재무파트";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 9].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 10].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 11].Value = "울산총괄";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 11].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 12].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2);
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 13].Value = "사장단";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 13].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 14].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 2);
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 15].Value = "회장단";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 15].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 16].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.AddColumnHeaderSpanCell(0, 17, 1, 2);
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 17].Value = "공통관리비 계";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 17].Value = "계 획";
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[1, 18].Value = "실 적";

            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7AJI2855_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;    
        }
        #endregion
    }
}
