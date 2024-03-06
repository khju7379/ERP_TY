using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 인수도 확정량 통보서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.07.03 11:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_973BM987 : 인수도 확정량 통보서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_973BQ989 : 인수도 확정량 통보서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    /// </summary>
    public partial class TYUSPR006S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR006S()
        {
            InitializeComponent();
        }

        private void TYUSPR006S_Load(object sender, System.EventArgs e)
        {

            SetStartingFocus(this.CBH01_IHHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_973BQ989.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_973BM987",
                 this.CBH01_IHHANGCHA.GetValue().ToString(),
                 this.CBH02_IHHANGCHA.GetValue().ToString()
                );

            this.FPS91_TY_S_US_973BQ989.SetValue(UP_SumRowAdd(this.DbConnector.ExecuteDataTable(), "1"));

            if (this.FPS91_TY_S_US_973BQ989.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_US_973BQ989, "VNSANGHO", "[소 계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_973BQ989, "VNSANGHO", "[합 계]", SumRowType.Total);
            }

        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_973BM987",
                 this.CBH01_IHHANGCHA.GetValue().ToString(),
                 this.CBH02_IHHANGCHA.GetValue().ToString()
                );

            DataTable dt = UP_SumRowAdd(this.DbConnector.ExecuteDataTable(), "2");

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSPR006R1();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 소계, 합계 row 추가 함수
        private DataTable UP_SumRowAdd(DataTable dt, string sGubn)
        {
            int i = 0;

            double dJBBEJNQTYHap = 0;
            double dJBHWAKQTYHap = 0;

            double dHAP_JBBEJNQTYHap = 0;
            double dHAP_JBHWAKQTYHap = 0;
            double dHAP_INCRE = 0;

            string sFilter = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int iCnt = table.Rows.Count;

            int nNum = table.Rows.Count;


            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["JBHANGCHA"].ToString() != table.Rows[i]["JBHANGCHA"].ToString() ||
                        table.Rows[i - 1]["JBSOSOK"].ToString() != table.Rows[i]["JBSOSOK"].ToString()
                    )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["VNSANGHO"] = "[소 계]";

                    sFilter = "  JBHANGCHA  = '" + table.Rows[i - 1]["JBHANGCHA"].ToString() + "'";
                    sFilter = sFilter + " AND  JBSOSOK  = '" + table.Rows[i - 1]["JBSOSOK"].ToString() + "'";

                    if (sGubn != "1")
                    {
                        table.Rows[i]["ROWNUM"] = table.Rows[i - 1]["ROWNUM"].ToString();
                        table.Rows[i]["JBSOSOK"] = table.Rows[i - 1]["JBSOSOK"].ToString();
                        table.Rows[i]["JBSOSOKNM"] = table.Rows[i - 1]["JBSOSOKNM"].ToString();
                        table.Rows[i]["JBHANGCHA"] = table.Rows[i - 1]["JBHANGCHA"].ToString();
                        table.Rows[i]["JBHANGCHANM"] = table.Rows[i - 1]["JBHANGCHANM"].ToString();
                        table.Rows[i]["IHIPHANG"] = table.Rows[i - 1]["IHIPHANG"].ToString();
                        table.Rows[i]["JBGOKJONG"] = table.Rows[i - 1]["JBGOKJONG"].ToString();
                        table.Rows[i]["JBGOKJONGNM"] = table.Rows[i - 1]["JBGOKJONGNM"].ToString();
                        table.Rows[i]["IHJAKENDAT"] = table.Rows[i - 1]["IHJAKENDAT"].ToString();
                        table.Rows[i]["IPIPSTDAT"] = table.Rows[i - 1]["IPIPSTDAT"].ToString();
                        table.Rows[i]["WNDESC1"] = table.Rows[i - 1]["WNDESC1"].ToString();
                        table.Rows[i]["IHJUKHANO"] = table.Rows[i - 1]["IHJUKHANO"].ToString();
                    }

                    table.Rows[i]["JBBEJNQTY"] = table.Compute("SUM(JBBEJNQTY)", sFilter).ToString();
                    table.Rows[i]["JBHWAKQTY"] = table.Compute("SUM(JBHWAKQTY)", sFilter).ToString();
                    table.Rows[i]["INCRE"] = table.Compute("SUM(INCRE)", sFilter).ToString();

                    sFilter = "  JBHANGCHA  = '" + table.Rows[i - 1]["JBHANGCHA"].ToString() + "'";
                    dJBHWAKQTYHap = Convert.ToDouble(table.Compute("SUM(JBHWAKQTY)", sFilter).ToString()) * 100;
                    dJBBEJNQTYHap = Convert.ToDouble(table.Compute("SUM(JBBEJNQTY)", sFilter).ToString());
                    table.Rows[i]["JBBIGO"] = String.Format("{0:#,##0.000}", Math.Round(dJBHWAKQTYHap / dJBBEJNQTYHap, 2));

                    nNum = nNum + 1;

                    i = i + 1;

                    if (table.Rows[i - 2]["JBHANGCHA"].ToString() != table.Rows[i]["JBHANGCHA"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        table.Rows[i]["VNSANGHO"] = "[합 계]";

                        sFilter = "  JBHANGCHA  = '" + table.Rows[i - 2]["JBHANGCHA"].ToString() + "'";
                        sFilter += " AND VNSANGHO <> '" + "[소 계]" + "'";

                        if (sGubn != "1")
                        {
                            table.Rows[i]["ROWNUM"] = table.Rows[i - 2]["ROWNUM"].ToString();
                            table.Rows[i]["JBSOSOK"] = table.Rows[i - 2]["JBSOSOK"].ToString();
                            table.Rows[i]["JBSOSOKNM"] = table.Rows[i - 2]["JBSOSOKNM"].ToString();
                            table.Rows[i]["JBHANGCHA"] = table.Rows[i - 2]["JBHANGCHA"].ToString();
                            table.Rows[i]["JBHANGCHANM"] = table.Rows[i - 2]["JBHANGCHANM"].ToString();
                            table.Rows[i]["IHIPHANG"] = table.Rows[i - 2]["IHIPHANG"].ToString();
                            table.Rows[i]["JBGOKJONG"] = table.Rows[i - 2]["JBGOKJONG"].ToString();
                            table.Rows[i]["JBGOKJONGNM"] = table.Rows[i - 2]["JBGOKJONGNM"].ToString();
                            table.Rows[i]["IHJAKENDAT"] = table.Rows[i - 2]["IHJAKENDAT"].ToString();
                            table.Rows[i]["IPIPSTDAT"] = table.Rows[i - 2]["IPIPSTDAT"].ToString();
                            table.Rows[i]["WNDESC1"] = table.Rows[i - 2]["WNDESC1"].ToString();
                            table.Rows[i]["IHJUKHANO"] = table.Rows[i - 2]["IHJUKHANO"].ToString();
                        }

                        table.Rows[i]["JBBEJNQTY"] = table.Compute("SUM(JBBEJNQTY)", sFilter).ToString();
                        table.Rows[i]["JBHWAKQTY"] = table.Compute("SUM(JBHWAKQTY)", sFilter).ToString();
                        table.Rows[i]["INCRE"] = table.Compute("SUM(INCRE)", sFilter).ToString();

                        sFilter = "  JBHANGCHA  = '" + table.Rows[i - 2]["JBHANGCHA"].ToString() + "'";
                        dJBHWAKQTYHap = Convert.ToDouble(table.Compute("SUM(JBHWAKQTY)", sFilter).ToString()) * 100;
                        dJBBEJNQTYHap = Convert.ToDouble(table.Compute("SUM(JBBEJNQTY)", sFilter).ToString());
                        table.Rows[i]["JBBIGO"] = String.Format("{0:#,##0.000}", Math.Round(dJBHWAKQTYHap / dJBBEJNQTYHap, 2));

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }
            }

            if (nNum > 0)
            {
                #region Description : 합계를 구하기 위한 식

                dHAP_JBBEJNQTYHap = 0;
                dHAP_JBHWAKQTYHap = 0;
                dHAP_INCRE = 0;

                if (iCnt == 1)
                {
                    sFilter = "  JBHANGCHA  = '" + table.Rows[0]["JBHANGCHA"].ToString() + "'";
                }
                else
                {
                    sFilter = "  JBHANGCHA  = '" + table.Rows[i - 2]["JBHANGCHA"].ToString() + "'";
                }
                dHAP_JBBEJNQTYHap = Convert.ToDouble(table.Compute("SUM(JBBEJNQTY)", sFilter).ToString());
                dHAP_JBHWAKQTYHap = Convert.ToDouble(table.Compute("SUM(JBHWAKQTY)", sFilter).ToString()) * 100;
                dHAP_INCRE = Convert.ToDouble(table.Compute("SUM(INCRE)", sFilter).ToString());

                #endregion

                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["VNSANGHO"] = "[소 계]";

                if (iCnt == 1)
                {
                    sFilter = "  JBHANGCHA  = '" + table.Rows[0]["JBHANGCHA"].ToString() + "'";
                    sFilter = sFilter + " AND  JBSOSOK  = '" + table.Rows[0]["JBSOSOK"].ToString() + "'";
                }
                else
                {
                    sFilter = "  JBHANGCHA  = '" + table.Rows[i - 1]["JBHANGCHA"].ToString() + "'";
                    sFilter = sFilter + " AND  JBSOSOK  = '" + table.Rows[i - 1]["JBSOSOK"].ToString() + "'";
                }



                if (sGubn != "1")
                {
                    if (iCnt == 1)
                    {
                        table.Rows[i]["ROWNUM"] = table.Rows[0]["ROWNUM"].ToString();
                        table.Rows[i]["JBSOSOK"] = table.Rows[0]["JBSOSOK"].ToString();
                        table.Rows[i]["JBSOSOKNM"] = table.Rows[0]["JBSOSOKNM"].ToString();
                        table.Rows[i]["JBHANGCHA"] = table.Rows[0]["JBHANGCHA"].ToString();
                        table.Rows[i]["JBHANGCHANM"] = table.Rows[0]["JBHANGCHANM"].ToString();
                        table.Rows[i]["IHIPHANG"] = table.Rows[0]["IHIPHANG"].ToString();
                        table.Rows[i]["JBGOKJONG"] = table.Rows[0]["JBGOKJONG"].ToString();
                        table.Rows[i]["JBGOKJONGNM"] = table.Rows[0]["JBGOKJONGNM"].ToString();
                        table.Rows[i]["IHJAKENDAT"] = table.Rows[0]["IHJAKENDAT"].ToString();
                        table.Rows[i]["IPIPSTDAT"] = table.Rows[0]["IPIPSTDAT"].ToString();
                        table.Rows[i]["WNDESC1"] = table.Rows[0]["WNDESC1"].ToString();
                        table.Rows[i]["IHJUKHANO"] = table.Rows[0]["IHJUKHANO"].ToString();
                    }
                    else
                    {
                        table.Rows[i]["ROWNUM"] = table.Rows[i - 1]["ROWNUM"].ToString();
                        table.Rows[i]["JBSOSOK"] = table.Rows[i - 1]["JBSOSOK"].ToString();
                        table.Rows[i]["JBSOSOKNM"] = table.Rows[i - 1]["JBSOSOKNM"].ToString();
                        table.Rows[i]["JBHANGCHA"] = table.Rows[i - 1]["JBHANGCHA"].ToString();
                        table.Rows[i]["JBHANGCHANM"] = table.Rows[i - 1]["JBHANGCHANM"].ToString();
                        table.Rows[i]["IHIPHANG"] = table.Rows[i - 1]["IHIPHANG"].ToString();
                        table.Rows[i]["JBGOKJONG"] = table.Rows[i - 1]["JBGOKJONG"].ToString();
                        table.Rows[i]["JBGOKJONGNM"] = table.Rows[i - 1]["JBGOKJONGNM"].ToString();
                        table.Rows[i]["IHJAKENDAT"] = table.Rows[i - 1]["IHJAKENDAT"].ToString();
                        table.Rows[i]["IPIPSTDAT"] = table.Rows[i - 1]["IPIPSTDAT"].ToString();
                        table.Rows[i]["WNDESC1"] = table.Rows[i - 1]["WNDESC1"].ToString();
                        table.Rows[i]["IHJUKHANO"] = table.Rows[i - 1]["IHJUKHANO"].ToString();
                    }
                }

                table.Rows[i]["JBBEJNQTY"] = table.Compute("SUM(JBBEJNQTY)", sFilter).ToString();
                table.Rows[i]["JBHWAKQTY"] = table.Compute("SUM(JBHWAKQTY)", sFilter).ToString();
                table.Rows[i]["INCRE"] = table.Compute("SUM(INCRE)", sFilter).ToString();

                if (iCnt == 1)
                {
                    sFilter = "  JBHANGCHA  = '" + table.Rows[0]["JBHANGCHA"].ToString() + "'";
                }
                else
                {
                    sFilter = "  JBHANGCHA  = '" + table.Rows[i - 1]["JBHANGCHA"].ToString() + "'";
                }

                dJBHWAKQTYHap = Convert.ToDouble(table.Compute("SUM(JBHWAKQTY)", sFilter).ToString()) * 100;
                dJBBEJNQTYHap = Convert.ToDouble(table.Compute("SUM(JBBEJNQTY)", sFilter).ToString());
                table.Rows[i]["JBBIGO"] = String.Format("{0:#,##0.000}", Math.Round(dJBHWAKQTYHap / dJBBEJNQTYHap, 2));

                nNum = nNum + 1;
                i = i + 1;

                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["VNSANGHO"] = "[합 계]";

                sFilter = "  JBHANGCHA  = '" + table.Rows[i - 2]["JBHANGCHA"].ToString() + "'";
                sFilter += " AND VNSANGHO <> '" + "[소 계]" + "'";

                if (sGubn != "1")
                {
                    if (iCnt == 1)
                    {
                        table.Rows[i]["ROWNUM"] = table.Rows[0]["ROWNUM"].ToString();
                        table.Rows[i]["JBSOSOK"] = table.Rows[0]["JBSOSOK"].ToString();
                        table.Rows[i]["JBSOSOKNM"] = table.Rows[0]["JBSOSOKNM"].ToString();
                        table.Rows[i]["JBHANGCHA"] = table.Rows[0]["JBHANGCHA"].ToString();
                        table.Rows[i]["JBHANGCHANM"] = table.Rows[0]["JBHANGCHANM"].ToString();
                        table.Rows[i]["IHIPHANG"] = table.Rows[0]["IHIPHANG"].ToString();
                        table.Rows[i]["JBGOKJONG"] = table.Rows[0]["JBGOKJONG"].ToString();
                        table.Rows[i]["JBGOKJONGNM"] = table.Rows[0]["JBGOKJONGNM"].ToString();
                        table.Rows[i]["IHJAKENDAT"] = table.Rows[0]["IHJAKENDAT"].ToString();
                        table.Rows[i]["IPIPSTDAT"] = table.Rows[0]["IPIPSTDAT"].ToString();
                        table.Rows[i]["WNDESC1"] = table.Rows[0]["WNDESC1"].ToString();
                        table.Rows[i]["IHJUKHANO"] = table.Rows[0]["IHJUKHANO"].ToString();
                    }
                    else
                    {
                        table.Rows[i]["ROWNUM"] = table.Rows[i - 2]["ROWNUM"].ToString();
                        table.Rows[i]["JBSOSOK"] = table.Rows[i - 2]["JBSOSOK"].ToString();
                        table.Rows[i]["JBSOSOKNM"] = table.Rows[i - 2]["JBSOSOKNM"].ToString();
                        table.Rows[i]["JBHANGCHA"] = table.Rows[i - 2]["JBHANGCHA"].ToString();
                        table.Rows[i]["JBHANGCHANM"] = table.Rows[i - 2]["JBHANGCHANM"].ToString();
                        table.Rows[i]["IHIPHANG"] = table.Rows[i - 2]["IHIPHANG"].ToString();
                        table.Rows[i]["JBGOKJONG"] = table.Rows[i - 2]["JBGOKJONG"].ToString();
                        table.Rows[i]["JBGOKJONGNM"] = table.Rows[i - 2]["JBGOKJONGNM"].ToString();
                        table.Rows[i]["IHJAKENDAT"] = table.Rows[i - 2]["IHJAKENDAT"].ToString();
                        table.Rows[i]["IPIPSTDAT"] = table.Rows[i - 2]["IPIPSTDAT"].ToString();
                        table.Rows[i]["WNDESC1"] = table.Rows[i - 2]["WNDESC1"].ToString();
                        table.Rows[i]["IHJUKHANO"] = table.Rows[i - 2]["IHJUKHANO"].ToString();
                    }
                }

                // 원본소스
                table.Rows[i]["JBBEJNQTY"] = table.Compute("SUM(JBBEJNQTY)", sFilter).ToString();
                table.Rows[i]["JBHWAKQTY"] = table.Compute("SUM(JBHWAKQTY)", sFilter).ToString();
                table.Rows[i]["INCRE"] = table.Compute("SUM(INCRE)", sFilter).ToString();

                sFilter = "  JBHANGCHA  = '" + table.Rows[i - 2]["JBHANGCHA"].ToString() + "'";
                dJBHWAKQTYHap = Convert.ToDouble(table.Compute("SUM(JBHWAKQTY)", sFilter).ToString()) * 100;
                dJBBEJNQTYHap = Convert.ToDouble(table.Compute("SUM(JBBEJNQTY)", sFilter).ToString());

                table.Rows[i]["JBBIGO"] = String.Format("{0:#,##0.000}", Math.Round(dJBHWAKQTYHap / dJBBEJNQTYHap, 2));

                //table.Rows[i]["JBBEJNQTY"] = dHAP_JBBEJNQTYHap;
                //table.Rows[i]["JBHWAKQTY"] = dHAP_JBHWAKQTYHap / 100;
                //table.Rows[i]["INCRE"]     = dHAP_INCRE;

                //table.Rows[i]["JBBIGO"]    = String.Format("{0:#,##0.000}", Math.Round(dHAP_JBHWAKQTYHap / dHAP_JBBEJNQTYHap, 2));
            }

            return table;
        }
        #endregion


    }
}