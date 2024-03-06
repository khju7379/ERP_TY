using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 곡종,모선별 작업현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.07.01 13:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_971FD957 : 모선별 작업현황 조회
    ///  TY_P_US_971FE958 : 곡종별 작업현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_971FH960 : 곡종별 작업현황 조회
    ///  TY_S_US_971FK961 : 모선별 작업현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSPR003S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR003S()
        {
            InitializeComponent();
        }

        private void TYUSPR003S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_971FK961.Visible = true;
            this.FPS91_TY_S_US_971FH960.Visible = false;

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            if (CBO01_INQOPTION.GetValue().ToString() == "1") //모선별
            {
                this.FPS91_TY_S_US_971FK961.Visible = true;
                this.FPS91_TY_S_US_971FH960.Visible = false;

                this.FPS91_TY_S_US_971FK961.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_971FD957",
                        this.DTP01_SDATE.GetString().ToString(),
                        this.CBH01_IHHANGCHA.GetValue().ToString(),
                        this.CBH02_IHHANGCHA.GetValue().ToString()
                    );
                this.FPS91_TY_S_US_971FK961.SetValue(UP_SumRowAdd(this.DbConnector.ExecuteDataTable(), "VS"));

                if (this.FPS91_TY_S_US_971FK961.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_US_971FK961, "JGHANGCHATIT", "[합 계]", SumRowType.Sum);
                }

            }
            else  //곡종별
            {
                this.FPS91_TY_S_US_971FK961.Visible = false;
                this.FPS91_TY_S_US_971FH960.Visible = true;

                this.FPS91_TY_S_US_971FH960.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_971FE958",
                        this.DTP01_SDATE.GetString().ToString(),
                        this.CBH01_IHHANGCHA.GetValue().ToString(),
                        this.CBH02_IHHANGCHA.GetValue().ToString()
                    );
                this.FPS91_TY_S_US_971FH960.SetValue(UP_SumRowAdd(this.DbConnector.ExecuteDataTable(), "GK"));

                if (this.FPS91_TY_S_US_971FH960.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_US_971FH960, "JGGOKJONGTIT", "[합 계]", SumRowType.Sum);
                }
            }

        }
        #endregion            

        #region Description : 모선,곡종별 작업현황 집계 row 추가 함수
        private DataTable UP_SumRowAdd(DataTable dt, string sGubn)
        {
            int i = 0;

            string sFilter = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["JGHANGCHA"].ToString() != table.Rows[i]["JGHANGCHA"].ToString() ||
                      table.Rows[i - 1]["JGGOKJONG"].ToString() != table.Rows[i]["JGGOKJONG"].ToString() ||
                    table.Rows[i - 1]["IHIPHANG"].ToString() != table.Rows[i]["IHIPHANG"].ToString()
                   )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    if (sGubn == "VS")
                        table.Rows[i]["JGHANGCHATIT"] = "[합 계]";
                    else
                        table.Rows[i]["JGGOKJONGTIT"] = "[합 계]";


                    sFilter = "  JGHANGCHA  = '" + table.Rows[i - 1]["JGHANGCHA"].ToString() + "'";
                    sFilter = sFilter + " AND  JGGOKJONG  = '" + table.Rows[i - 1]["JGGOKJONG"].ToString() + "'";
                    sFilter = sFilter + " AND  IHIPHANG  = '" + table.Rows[i - 1]["IHIPHANG"].ToString() + "'";

                    table.Rows[i]["JGBEJNQTY"] = table.Compute("SUM(JGBEJNQTY)", sFilter).ToString();
                    table.Rows[i]["JGHWAKQTY"] = table.Compute("SUM(JGHWAKQTY)", sFilter).ToString();
                    table.Rows[i]["YDQTY"] = table.Compute("SUM(YDQTY)", sFilter).ToString();
                    table.Rows[i]["YSQTY"] = table.Compute("SUM(YSQTY)", sFilter).ToString();
                    table.Rows[i]["YSYDQTY"] = table.Compute("SUM(YSYDQTY)", sFilter).ToString();
                    table.Rows[i]["CHMTQTY"] = table.Compute("SUM(CHMTQTY)", sFilter).ToString();
                    table.Rows[i]["CHNUQTY"] = table.Compute("SUM(CHNUQTY)", sFilter).ToString();
                    table.Rows[i]["JNQTY"] = table.Compute("SUM(JNQTY)", sFilter).ToString();
                    table.Rows[i]["CARCNT"] = table.Compute("SUM(CARCNT)", sFilter).ToString();

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                if (sGubn == "VS")
                    table.Rows[i]["JGHANGCHATIT"] = "[합 계]";
                else
                    table.Rows[i]["JGGOKJONGTIT"] = "[합 계]";

                //  년월, 거래처
                sFilter = "  JGHANGCHA  = '" + table.Rows[i - 1]["JGHANGCHA"].ToString() + "'";
                sFilter = sFilter + " AND  JGGOKJONG  = '" + table.Rows[i - 1]["JGGOKJONG"].ToString() + "'";
                sFilter = sFilter + " AND  IHIPHANG  = '" + table.Rows[i - 1]["IHIPHANG"].ToString() + "'";

                table.Rows[i]["JGBEJNQTY"] = table.Compute("SUM(JGBEJNQTY)", sFilter).ToString();
                table.Rows[i]["JGHWAKQTY"] = table.Compute("SUM(JGHWAKQTY)", sFilter).ToString();
                table.Rows[i]["YDQTY"] = table.Compute("SUM(YDQTY)", sFilter).ToString();
                table.Rows[i]["YSQTY"] = table.Compute("SUM(YSQTY)", sFilter).ToString();
                table.Rows[i]["YSYDQTY"] = table.Compute("SUM(YSYDQTY)", sFilter).ToString();
                table.Rows[i]["CHMTQTY"] = table.Compute("SUM(CHMTQTY)", sFilter).ToString();
                table.Rows[i]["CHNUQTY"] = table.Compute("SUM(CHNUQTY)", sFilter).ToString();
                table.Rows[i]["JNQTY"] = table.Compute("SUM(JNQTY)", sFilter).ToString();
                table.Rows[i]["CARCNT"] = table.Compute("SUM(CARCNT)", sFilter).ToString();

            }

            return table;
        }
        #endregion  

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                    CBO01_INQOPTION.GetValue().ToString() == "1" ? "TY_P_US_971FD957" : "TY_P_US_971FE958",
                    this.DTP01_SDATE.GetString().ToString(),
                    this.CBH01_IHHANGCHA.GetValue().ToString(),
                    this.CBH02_IHHANGCHA.GetValue().ToString()
                );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (CBO01_INQOPTION.GetValue().ToString() == "1") //모선별
            {
                SectionReport rpt = new TYUSPR003R2();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                SectionReport rpt = new TYUSPR003R1();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion


    }
}