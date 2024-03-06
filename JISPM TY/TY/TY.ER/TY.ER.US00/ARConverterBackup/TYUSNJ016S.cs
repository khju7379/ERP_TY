using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 후생복지비 지출명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.03 13:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_943DO229 : 후생복지비 지불명세서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_943DO230 : 후생복지비 지불명세서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CHHANGCHA : 항 차
    ///  INQOPTION : 조회구분
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSNJ016S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSNJ016S()
        {
            InitializeComponent();
        }

        private void TYUSNJ016S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_943DO230.Visible = true;            

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));            

            SetStartingFocus(this.CBH01_CHHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_943DO230.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                    "TY_P_US_943DO229",
                    this.CBH01_CHHANGCHA.GetValue().ToString(),
                    this.CBH02_CHHANGCHA.GetValue().ToString(),
                    this.DTP01_SDATE.GetString().ToString().Substring(0,6),
                    this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                    this.CKB01_INQOPTION.Checked == true ? "1" : "" 
                );

            this.FPS91_TY_S_US_943DO230.SetValue(UP_InsertRowTotal(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_US_943DO230.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_US_943DO230, "HWHANGCHANM", "[소  계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_943DO230, "HWHANGCHA", "[합  계]", SumRowType.Total);
            }
           
        }
        #endregion        

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                    "TY_P_US_943DO229",
                    this.CBH01_CHHANGCHA.GetValue().ToString(),
                    this.CBH02_CHHANGCHA.GetValue().ToString(),
                    this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                    this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                    this.CKB01_INQOPTION.Checked == true ? "1" : ""
                );
            DataTable dt = UP_InsertRowSubTotal(this.DbConnector.ExecuteDataTable());

            if (this.CKB01_INQOPTION.Checked != true)
            {
                ActiveReport rpt = new TYUSNJ016R1();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                ActiveReport rpt = new TYUSNJ016R1();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 소계, 합계 넣기
        private DataTable UP_InsertRowTotal(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;        

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["HWYYMM"].ToString() != table.Rows[i]["HWYYMM"].ToString() ||
                    table.Rows[i - 1]["GUBN"].ToString() != table.Rows[i]["GUBN"].ToString() 
                    )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["HWHANGCHANM"] = "[소  계]";
                    
                    sFilter = "  HWYYMM  = " + table.Rows[i - 1]["HWYYMM"].ToString() + " ";
                    sFilter = sFilter + " AND  GUBN  = '" + table.Rows[i - 1]["GUBN"].ToString() + "'";                    

                    //취급톤수
                    table.Rows[i]["HWWKQTY"] = Convert.ToDouble(table.Compute("SUM(HWWKQTY)", sFilter).ToString());
                    //금액
                    table.Rows[i]["HWHSBJAMT"] = Convert.ToDouble(table.Compute("SUM(HWHSBJAMT)", sFilter).ToString());

                    nNum = nNum + 1;

                    i = i + 1;

                    //월 합계 라인 넣기
                    if (table.Rows[i - 2]["HWYYMM"].ToString() != table.Rows[i]["HWYYMM"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        table.Rows[i]["HWHANGCHA"] = "[합  계]";

                        sFilter = "  HWYYMM  = " + table.Rows[i - 2]["HWYYMM"].ToString() + "";

                        //취급톤수
                        table.Rows[i]["HWWKQTY"] = Convert.ToDouble(table.Compute("SUM(HWWKQTY)", sFilter).ToString());
                        //금액
                        table.Rows[i]["HWHSBJAMT"] = Convert.ToDouble(table.Compute("SUM(HWHSBJAMT)", sFilter).ToString());

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }              

            }

            if (nNum > 0)
            {
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                // 합 계 이름 넣기
                table.Rows[i]["HWHANGCHANM"] = "[소  계]";

                sFilter = "  HWYYMM  = " + table.Rows[i - 1]["HWYYMM"].ToString() + " ";
                sFilter = sFilter + " AND  GUBN  = '" + table.Rows[i - 1]["GUBN"].ToString() + "'";

                //취급톤수
                table.Rows[i]["HWWKQTY"] = Convert.ToDouble(table.Compute("SUM(HWWKQTY)", sFilter).ToString());
                //금액
                table.Rows[i]["HWHSBJAMT"] = Convert.ToDouble(table.Compute("SUM(HWHSBJAMT)", sFilter).ToString());

                nNum = nNum + 1;

                i = i + 1;

                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                // 합 계 이름 넣기
                table.Rows[i]["HWHANGCHA"] = "[합  계]";

                sFilter = "  HWYYMM  = '" + table.Rows[i - 2]["HWYYMM"].ToString() + "'";

                //취급톤수
                table.Rows[i]["HWWKQTY"] = table.Compute("SUM(HWWKQTY)", sFilter).ToString();
                //금액
                table.Rows[i]["HWHSBJAMT"] = table.Compute("SUM(HWHSBJAMT)", sFilter).ToString();          
            }

            return table;
        }
        #endregion   

        #region Description : 소계 넣기
        private DataTable UP_InsertRowSubTotal(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["HWYYMM"].ToString() != table.Rows[i]["HWYYMM"].ToString() ||
                    table.Rows[i - 1]["GUBN"].ToString() != table.Rows[i]["GUBN"].ToString()
                    )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["HWHANGCHANM"] = "[소  계]";

                    sFilter = "  HWYYMM  = " + table.Rows[i - 1]["HWYYMM"].ToString() + " ";
                    sFilter = sFilter + " AND  GUBN  = '" + table.Rows[i - 1]["GUBN"].ToString() + "'";

                    table.Rows[i]["HWYYMM"] = table.Rows[i - 1]["HWYYMM"].ToString();

                    //취급톤수
                    table.Rows[i]["HWWKQTY"] = Convert.ToDouble(table.Compute("SUM(HWWKQTY)", sFilter).ToString());
                    //금액
                    table.Rows[i]["HWHSBJAMT"] = Convert.ToDouble(table.Compute("SUM(HWHSBJAMT)", sFilter).ToString());

                    nNum = nNum + 1;

                    i = i + 1;

                  
                }

            }

            if (nNum > 0)
            {
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                // 합 계 이름 넣기
                table.Rows[i]["HWHANGCHANM"] = "[소  계]";

                sFilter = "  HWYYMM  = " + table.Rows[i - 1]["HWYYMM"].ToString() + " ";
                sFilter = sFilter + " AND  GUBN  = '" + table.Rows[i - 1]["GUBN"].ToString() + "'";

                table.Rows[i]["HWYYMM"] = table.Rows[i - 1]["HWYYMM"].ToString();

                //취급톤수
                table.Rows[i]["HWWKQTY"] = Convert.ToDouble(table.Compute("SUM(HWWKQTY)", sFilter).ToString());
                //금액
                table.Rows[i]["HWHSBJAMT"] = Convert.ToDouble(table.Compute("SUM(HWHSBJAMT)", sFilter).ToString());

                nNum = nNum + 1;

                i = i + 1;

            }

            return table;
        }
        #endregion   

     

    }
}