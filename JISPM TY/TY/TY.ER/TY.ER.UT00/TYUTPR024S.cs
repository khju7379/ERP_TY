using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using System.Windows.Forms;
using System.Drawing;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선박입고 할증내역 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.08.29 18:24
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_78TJ8510 : 선박입고 할증내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_78TJ9511 : 선박입고 할증내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  REFER : 참조인
    /// </summary>
    public partial class TYUTPR024S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR024S()
        {
            InitializeComponent();
        }

        private void TYUTPR024S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.CBH01_CHHWAJU.CodeText);

            UP_Spread_Load();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                this.FPS91_TY_S_UT_78TJ9511.Initialize();

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_78TJ8510", this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_JIBONSUN.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString());

                dt = UP_DataTableChange(this.DbConnector.ExecuteDataTable());

                this.FPS91_TY_S_UT_78TJ9511.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_UT_78TJ9511.ActiveSheet.RowCount; i++)
                {

                    if (this.FPS91_TY_S_UT_78TJ9511.GetValue(i, "COIPHANG").ToString() == "합   계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_UT_78TJ9511.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }

                UP_Spread_Load();
            }
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_78TJ8510", this.CBH01_CHHWAJU.GetValue().ToString(),
                                                        this.CBH01_JIBONSUN.GetValue().ToString(),
                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                        this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR024R(this.TXT01_REFER.GetValue().ToString(),
                                                  this.DTP01_STDATE.GetString(),
                                                  this.DTP01_EDDATE.GetString());

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2);
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 2);

            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[0, 13].Value = "입고량";
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[0, 15].Value = "할증량";

            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[1, 13].Value = "MT";
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[1, 14].Value = "KL";
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[1, 15].Value = "MT";
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[1, 16].Value = "KL";

            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_78TJ9511_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        private DataTable UP_DataTableChange(DataTable dt)
        {
            DataTable retDt = dt;

            if (dt.Rows.Count > 0)
            {
                DataRow row = retDt.NewRow();

                row["COIPHANG"] = "합   계";
                row["COBONSUN"] = "";
                row["COBONSUNNM"] = "";
                row["COHWAJU"] = "";
                row["VNSANGHO"] = "";
                row["COHWAMUL"] = "";
                row["COHWAMULNM"] = "";
                row["COPUSTR1"] = 0;
                row["COPUEND1"] = 0;
                row["COPUTIM"] = "";
                row["COOVSTR1"] = 0;
                row["COOVEND1"] = 0;
                row["COOVTIM"] = "";

                row["COMTQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(COMTQTY)", null));
                row["COKLQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(COKLQTY)", null));
                row["COOVQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(COOVQTY)", null));
                row["COOVKLQTY"] = string.Format("{0:#,##0.000}", dt.Compute("SUM(COOVKLQTY)", null));

                retDt.Rows.Add(row);
            }

            return retDt;
        }
    }
}
