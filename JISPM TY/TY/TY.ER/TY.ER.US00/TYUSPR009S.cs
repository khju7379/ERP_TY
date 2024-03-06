using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 인수도 확인증 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.19 15:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_94JHE404 : 인수도 확인증 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_94JHF406 : 인수도 확인증 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  IHHWAJU : 화주
    /// </summary>
    public partial class TYUSPR009S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR009S()
        {
            InitializeComponent();
        }

        private void TYUSPR009S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_IHHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "N")
            {
                sProcedure = "TY_P_US_94JHE404";
                this.FPS91_TY_S_US_94JHF406_Sheet1.ColumnHeader.Cells[0, 14].Value = "배정량";
            }
            else
            {
                sProcedure = "TY_P_US_C45AA225";
                this.FPS91_TY_S_US_94JHF406_Sheet1.ColumnHeader.Cells[0, 14].Value = "양수량";
            }

            this.FPS91_TY_S_US_94JHF406.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                    sProcedure,
                    this.CBH01_IHHANGCHA.GetValue().ToString(),
                    this.CBH02_IHHANGCHA.GetValue().ToString(),
                    this.CBH01_IHHWAJU.GetValue().ToString(),
                    "1"
                );

            this.FPS91_TY_S_US_94JHF406.SetValue(UP_SumRowInSert(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_US_94JHF406.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_US_94JHF406, "HJNMTITLE", "[합  계]", SumRowType.SubTotal);
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_SumRowInSert(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;


            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 0; i < nNum; i++)
            {
                if (table.Rows[i]["ROWNUM"].ToString() == "1" && i > 0)
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["HJNMTITLE"] = "[합  계]";

                    sFilter = "  CHHWAJU  = '" + table.Rows[i - 1]["CHHWAJU"].ToString() + "'";
                    sFilter = sFilter + " AND  CHHANGCHA  = '" + table.Rows[i - 1]["CHHANGCHA"].ToString() + "'";
                    sFilter = sFilter + " AND  CHGOKJONG  = '" + table.Rows[i - 1]["CHGOKJONG"].ToString() + "'";
                    sFilter = sFilter + " AND  IHIPHANG  = '" + table.Rows[i - 1]["IHIPHANG"].ToString() + "'";

                    table.Rows[i]["CHMTQTY"] = table.Compute("SUM(CHMTQTY)", sFilter).ToString();
                    table.Rows[i]["CHMTQTYNU"] = table.Rows[i - 1]["CHMTQTYNU"].ToString();
                    table.Rows[i]["CHMTJGQTY"] = table.Rows[i - 1]["CHMTJGQTY"].ToString();

                    table.Rows[i]["GAMQTY"] = Math.Round(Convert.ToDouble(String.Format("{0:0.000}", Convert.ToDouble(table.Rows[i - 1]["JGBEJNQTY"].ToString()))) -
                                              Convert.ToDouble(String.Format("{0:0.000}", Convert.ToDouble(table.Rows[i - 1]["JGHWAKQTY"].ToString()))), 4);

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["HJNMTITLE"] = "[합  계]";

                sFilter = "  CHHWAJU  = '" + table.Rows[i - 1]["CHHWAJU"].ToString() + "'";
                sFilter = sFilter + " AND  CHHANGCHA  = '" + table.Rows[i - 1]["CHHANGCHA"].ToString() + "'";
                sFilter = sFilter + " AND  CHGOKJONG  = '" + table.Rows[i - 1]["CHGOKJONG"].ToString() + "'";
                sFilter = sFilter + " AND  IHIPHANG  = '" + table.Rows[i - 1]["IHIPHANG"].ToString() + "'";

                table.Rows[i]["CHMTQTY"] = Convert.ToDouble(table.Compute("SUM(CHMTQTY)", sFilter).ToString());
                table.Rows[i]["CHMTQTYNU"] = Convert.ToDouble(table.Rows[i - 1]["CHMTQTYNU"].ToString());
                table.Rows[i]["CHMTJGQTY"] = Convert.ToDouble(table.Rows[i - 1]["CHMTJGQTY"].ToString());

                table.Rows[i]["GAMQTY"] = Math.Round(Convert.ToDouble(String.Format("{0:0.000}", Convert.ToDouble(table.Rows[i - 1]["JGBEJNQTY"].ToString()))) -
                                              Convert.ToDouble(String.Format("{0:0.000}", Convert.ToDouble(table.Rows[i - 1]["JGHWAKQTY"].ToString()))), 4);

            }

            return table;
        }
        #endregion   

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "N")
            {
                sProcedure = "TY_P_US_94JHE404";
            }
            else
            {
                sProcedure = "TY_P_US_C45AA225";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                    sProcedure,
                    this.CBH01_IHHANGCHA.GetValue().ToString(),
                    this.CBH02_IHHANGCHA.GetValue().ToString(),
                    this.CBH01_IHHWAJU.GetValue().ToString(),
                    ""
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSPR009R1(this.CBO01_GPRTGN.GetValue().ToString());
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion





    }
}