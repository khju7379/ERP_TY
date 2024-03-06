using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 차입금 잔액조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2018.07.03 13:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_873I5320 : 차입금 잔액조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_87NES451 : 차입금 잔액조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  LOCCURRTYPE : 통화유형
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACLO006S : TYBase
    {
        #region Description : 폼 로드
        public TYACLO006S()
        {
            InitializeComponent();
        }

        private void TYACLO006S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_STYYMM);

            UP_Spread_Load("LOAD");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            string sSTYYMM = string.Empty;
            string sEDYYMM = string.Empty;

            sSTYYMM = Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4) + "01";
            sEDYYMM = Get_Date(this.DTP01_STYYMM.GetValue().ToString());


            UP_Spread_Load("INQ");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            // 20180827 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_87NER450", Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4),
            //                                            sSTYYMM.ToString(),
            //                                            sEDYYMM.ToString()
            //                                            );

            // 20190411 수정전 소스
            //if (int.Parse(Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4)) <= 2019)
            //{
            //    sProcedure = "TY_P_AC_897DI700";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88RGZ641";
            //}

            // 20190430 수정전 소스
            //sProcedure = "TY_P_AC_94UBB492";

            // 20220221 수정 소스
            sProcedure = "TY_P_AC_C2LHZ097";


            // 20180827 수정후 소스
            // 전년도 잔액        = 실행금액 - 상환금액            <= 로직 변경
            this.DbConnector.Attach(sProcedure.ToString(), Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4),
                                                           sSTYYMM.ToString(),
                                                           sEDYYMM.ToString()
                                                           );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_87NES451.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_87NES451.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_87NES451.GetValue(i, "LOCLOANTYPENM").ToString() == "소 계" || this.FPS91_TY_S_AC_87NES451.GetValue(i, "LOCLOANTYPENM").ToString() == "합 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_AC_87NES451.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_87NES451.GetValue(i, "LOCLOANTYPENM").ToString() == "소 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_AC_87NES451.ActiveSheet.Rows[i].BackColor = Color.YellowGreen;
                }

                if (this.FPS91_TY_S_AC_87NES451.GetValue(i, "LOCLOANTYPENM").ToString() == "합 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_AC_87NES451.ActiveSheet.Rows[i].BackColor = Color.ForestGreen;
                }
            }
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Load(string sGUBUN)
        {
            string sAGO_YEAR = string.Empty;
            string sYEAR     = string.Empty;
            string sYYMM     = string.Empty;

            if (sGUBUN.ToString() == "LOAD")
            {
                sAGO_YEAR = "전년도말";
                sYEAR     = "당해년도 증(감)";
                sYYMM     = "당해년도 잔액";
            }
            else
            {
                sAGO_YEAR = Convert.ToString(int.Parse(Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4)) - 1) + "년";
                sYEAR     = Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4) + "년 증(감)";
                sYYMM     = Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4) + "년" + Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(4, 2) + "월 잔액";
            }

            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_87NES451_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_87NES451_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_87NES451_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);

            this.FPS91_TY_S_AC_87NES451_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2);

            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[0, 3].Value = sAGO_YEAR.ToString();
            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[0, 4].Value = sYEAR.ToString();
            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[0, 6].Value = sYYMM.ToString();

            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[1, 3].Value = "잔 액";
            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[1, 4].Value = "차 입";
            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[1, 5].Value = "상 환";
            this.FPS91_TY_S_AC_87NES451_Sheet1.ColumnHeader.Cells[1, 6].Value = "기말잔액";
        }
        #endregion

        #region Descriptoin : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            string sDATE = this.DTP01_STYYMM.GetString().Substring(0, 6);
            string sSTYYMM = string.Empty;
            string sEDYYMM = string.Empty;

            sSTYYMM = Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4) + "01";
            sEDYYMM = Get_Date(this.DTP01_STYYMM.GetValue().ToString());

            this.DbConnector.CommandClear();

            // 20190411 수정전 소스
            //if (int.Parse(Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4)) <= 2019)
            //{
            //    sProcedure = "TY_P_AC_897DI700";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88RGZ641";
            //}

            // 20190430 수정전 소스
            if (int.Parse(Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4)) <= 2019)
            {
                sProcedure = "TY_P_AC_94BBG337";
            }
            else
            {
                sProcedure = "TY_P_AC_94BBJ338";
            }

            // 20220221 수정 소스
            sProcedure = "TY_P_AC_C2LHZ097";

            // 20180827 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_87NER450", Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4),
            //                                            sSTYYMM.ToString(),
            //                                            sEDYYMM.ToString()
            //                                            );

            this.DbConnector.Attach(sProcedure.ToString(), Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 4),
                                                           sSTYYMM.ToString(),
                                                           sEDYYMM.ToString()
                                                           );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLO006R(sDATE);

                // 세로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion
    }
}
