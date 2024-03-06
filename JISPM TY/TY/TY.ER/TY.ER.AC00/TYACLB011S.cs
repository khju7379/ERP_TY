using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 월별예산설정내역서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.28 09:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28S57601 : 월별예산설정내역서
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28S6P615 : 월별예산설정내역서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACLB011S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB011S()
        {
            InitializeComponent();
        }

        private void TYACLB011S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28S57601",
                this.TXT01_GDATE.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28S6P615.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28S6P615.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28S6P615.GetValue(i, "Y2DPSBNM").ToString() == "소   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28S6P615.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    else if (this.FPS91_TY_S_AC_28S6P615.GetValue(i, "Y2DPSBNM").ToString() == "사업부 소계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28S6P615.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_28S6P615.GetValue(i, "Y2DPSBNM").ToString() == "계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28S6P615.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28S57601",
                this.TXT01_GDATE.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLB011R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_ConvertDt(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            int i = 0;

            double dAMT1_SA  = 0;
            double dAMT2_SA  = 0;
            double dAMT3_SA  = 0;
            double dAMT4_SA  = 0;
            double dAMT5_SA  = 0;
            double dAMT6_SA  = 0;
            double dAMT7_SA  = 0;
            double dAMT8_SA  = 0;
            double dAMT9_SA  = 0;
            double dAMT10_SA = 0;
            double dAMT11_SA = 0;
            double dAMT12_SA = 0;
            double dHAP_SA   = 0;

            double dAMT1_TOT  = 0;
            double dAMT2_TOT  = 0;
            double dAMT3_TOT  = 0;
            double dAMT4_TOT  = 0;
            double dAMT5_TOT  = 0;
            double dAMT6_TOT  = 0;
            double dAMT7_TOT  = 0;
            double dAMT8_TOT  = 0;
            double dAMT9_TOT  = 0;
            double dAMT10_TOT = 0;
            double dAMT11_TOT = 0;
            double dAMT12_TOT = 0;
            double dHAP_TOT   = 0;

            string sNEWY2CDSAUP = string.Empty;
            string sOldY2CDSAUP = string.Empty;

            string sNEWY2DPSBNM = string.Empty;
            string sOldY2DPSBNM = string.Empty;

            string sY2NMDP   = string.Empty;
            string sY2CDSAUP = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["Y2CDSAUP"].ToString() != table.Rows[i]["Y2CDSAUP"].ToString())
                {
                    // 소계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["DATE"]     = table.Rows[i - 1]["DATE"].ToString();
                    table.Rows[i]["Y2CDSAUP"] = table.Rows[i - 1]["Y2CDSAUP"].ToString();
                    table.Rows[i]["Y2NMDP"]   = table.Rows[i - 1]["Y2NMDP"].ToString();

                    // 소 계 이름 넣기
                    table.Rows[i]["Y2DPSBNM"] = "소   계";

                    sY2NMDP = "Y2NMDP = '" + table.Rows[i - 1]["Y2NMDP"].ToString() + "' ";

                    table.Rows[i]["AMT1"]  = table.Compute("SUM(AMT1)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT2"]  = table.Compute("SUM(AMT2)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT3"]  = table.Compute("SUM(AMT3)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT4"]  = table.Compute("SUM(AMT4)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT5"]  = table.Compute("SUM(AMT5)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT6"]  = table.Compute("SUM(AMT6)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT7"]  = table.Compute("SUM(AMT7)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT8"]  = table.Compute("SUM(AMT8)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT9"]  = table.Compute("SUM(AMT9)",  sY2NMDP).ToString();
                    table.Rows[i]["AMT10"] = table.Compute("SUM(AMT10)", sY2NMDP).ToString();
                    table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sY2NMDP).ToString();
                    table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sY2NMDP).ToString();
                    table.Rows[i]["HAP"]   = table.Compute("SUM(HAP)",   sY2NMDP).ToString();

                    // 사업부계
                    dAMT1_SA  = dAMT1_SA  + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
                    dAMT2_SA  = dAMT2_SA  + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
                    dAMT3_SA  = dAMT3_SA  + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
                    dAMT4_SA  = dAMT4_SA  + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
                    dAMT5_SA  = dAMT5_SA  + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());
                    dAMT6_SA  = dAMT6_SA  + Convert.ToDouble(table.Rows[i]["AMT6"].ToString());
                    dAMT7_SA  = dAMT7_SA  + Convert.ToDouble(table.Rows[i]["AMT7"].ToString());
                    dAMT8_SA  = dAMT8_SA  + Convert.ToDouble(table.Rows[i]["AMT8"].ToString());
                    dAMT9_SA  = dAMT9_SA  + Convert.ToDouble(table.Rows[i]["AMT9"].ToString());
                    dAMT10_SA = dAMT10_SA + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
                    dAMT11_SA = dAMT11_SA + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                    dAMT12_SA = dAMT12_SA + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                    dHAP_SA   = dHAP_SA   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

                    // 총계
                    dAMT1_TOT  = dAMT1_TOT  + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
                    dAMT2_TOT  = dAMT2_TOT  + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
                    dAMT3_TOT  = dAMT3_TOT  + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
                    dAMT4_TOT  = dAMT4_TOT  + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
                    dAMT5_TOT  = dAMT5_TOT  + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());
                    dAMT6_TOT  = dAMT6_TOT  + Convert.ToDouble(table.Rows[i]["AMT6"].ToString());
                    dAMT7_TOT  = dAMT7_TOT  + Convert.ToDouble(table.Rows[i]["AMT7"].ToString());
                    dAMT8_TOT  = dAMT8_TOT  + Convert.ToDouble(table.Rows[i]["AMT8"].ToString());
                    dAMT9_TOT  = dAMT9_TOT  + Convert.ToDouble(table.Rows[i]["AMT9"].ToString());
                    dAMT10_TOT = dAMT10_TOT + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
                    dAMT11_TOT = dAMT11_TOT + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                    dAMT12_TOT = dAMT12_TOT + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                    dHAP_TOT   = dHAP_TOT   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

                    // 사업부 소계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i + 1);

                    table.Rows[i + 1]["DATE"]     = table.Rows[i - 2]["DATE"].ToString();
                    table.Rows[i + 1]["Y2CDSAUP"] = table.Rows[i - 2]["Y2CDSAUP"].ToString();
                    table.Rows[i + 1]["Y2NMDP"]   = table.Rows[i - 2]["Y2NMDP"].ToString();

                    // 사업부 소계 이름 넣기
                    table.Rows[i + 1]["Y2DPSBNM"] = "사업부 소계";

                    table.Rows[i + 1]["AMT1"]  = string.Format("{0:###0}", dAMT1_SA);
                    table.Rows[i + 1]["AMT2"]  = string.Format("{0:###0}", dAMT2_SA);
                    table.Rows[i + 1]["AMT3"]  = string.Format("{0:###0}", dAMT3_SA);
                    table.Rows[i + 1]["AMT4"]  = string.Format("{0:###0}", dAMT4_SA);
                    table.Rows[i + 1]["AMT5"]  = string.Format("{0:###0}", dAMT5_SA);
                    table.Rows[i + 1]["AMT6"]  = string.Format("{0:###0}", dAMT6_SA);
                    table.Rows[i + 1]["AMT7"]  = string.Format("{0:###0}", dAMT7_SA);
                    table.Rows[i + 1]["AMT8"]  = string.Format("{0:###0}", dAMT8_SA);
                    table.Rows[i + 1]["AMT9"]  = string.Format("{0:###0}", dAMT9_SA);
                    table.Rows[i + 1]["AMT10"] = string.Format("{0:###0}", dAMT10_SA);
                    table.Rows[i + 1]["AMT11"] = string.Format("{0:###0}", dAMT11_SA);
                    table.Rows[i + 1]["AMT12"] = string.Format("{0:###0}", dAMT12_SA);
                    table.Rows[i + 1]["HAP"]   = string.Format("{0:###0}", dHAP_SA);

                    dAMT1_SA  = 0;
                    dAMT2_SA  = 0;
                    dAMT3_SA  = 0;
                    dAMT4_SA  = 0;
                    dAMT5_SA  = 0;
                    dAMT6_SA  = 0;
                    dAMT7_SA  = 0;
                    dAMT8_SA  = 0;
                    dAMT9_SA  = 0;
                    dAMT10_SA = 0;
                    dAMT11_SA = 0;
                    dAMT12_SA = 0;
                    dHAP_SA   = 0;

                    nNum = nNum + 2;

                    i = i + 2;
                }
                else
                {
                    if (table.Rows[i - 1]["Y2NMDP"].ToString() != table.Rows[i]["Y2NMDP"].ToString())
                    {
                        // 소계
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["DATE"]     = table.Rows[i - 1]["DATE"].ToString();
                        table.Rows[i]["Y2CDSAUP"] = table.Rows[i - 1]["Y2CDSAUP"].ToString();
                        table.Rows[i]["Y2NMDP"]   = table.Rows[i - 1]["Y2NMDP"].ToString();

                        // 소 계 이름 넣기
                        table.Rows[i]["Y2DPSBNM"] = "소   계";

                        sY2NMDP = "Y2NMDP = '" + table.Rows[i - 1]["Y2NMDP"].ToString() + "' ";

                        table.Rows[i]["AMT1"]  = table.Compute("SUM(AMT1)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT2"]  = table.Compute("SUM(AMT2)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT3"]  = table.Compute("SUM(AMT3)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT4"]  = table.Compute("SUM(AMT4)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT5"]  = table.Compute("SUM(AMT5)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT6"]  = table.Compute("SUM(AMT6)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT7"]  = table.Compute("SUM(AMT7)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT8"]  = table.Compute("SUM(AMT8)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT9"]  = table.Compute("SUM(AMT9)",  sY2NMDP).ToString();
                        table.Rows[i]["AMT10"] = table.Compute("SUM(AMT10)", sY2NMDP).ToString();
                        table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sY2NMDP).ToString();
                        table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sY2NMDP).ToString();
                        table.Rows[i]["HAP"]   = table.Compute("SUM(HAP)",   sY2NMDP).ToString();

                        // 사업부계
                        dAMT1_SA  = dAMT1_SA  + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
                        dAMT2_SA  = dAMT2_SA  + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
                        dAMT3_SA  = dAMT3_SA  + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
                        dAMT4_SA  = dAMT4_SA  + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
                        dAMT5_SA  = dAMT5_SA  + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());
                        dAMT6_SA  = dAMT6_SA  + Convert.ToDouble(table.Rows[i]["AMT6"].ToString());
                        dAMT7_SA  = dAMT7_SA  + Convert.ToDouble(table.Rows[i]["AMT7"].ToString());
                        dAMT8_SA  = dAMT8_SA  + Convert.ToDouble(table.Rows[i]["AMT8"].ToString());
                        dAMT9_SA  = dAMT9_SA  + Convert.ToDouble(table.Rows[i]["AMT9"].ToString());
                        dAMT10_SA = dAMT10_SA + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
                        dAMT11_SA = dAMT11_SA + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                        dAMT12_SA = dAMT12_SA + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                        dHAP_SA   = dHAP_SA   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

                        // 총계
                        dAMT1_TOT  = dAMT1_TOT  + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
                        dAMT2_TOT  = dAMT2_TOT  + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
                        dAMT3_TOT  = dAMT3_TOT  + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
                        dAMT4_TOT  = dAMT4_TOT  + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
                        dAMT5_TOT  = dAMT5_TOT  + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());
                        dAMT6_TOT  = dAMT6_TOT  + Convert.ToDouble(table.Rows[i]["AMT6"].ToString());
                        dAMT7_TOT  = dAMT7_TOT  + Convert.ToDouble(table.Rows[i]["AMT7"].ToString());
                        dAMT8_TOT  = dAMT8_TOT  + Convert.ToDouble(table.Rows[i]["AMT8"].ToString());
                        dAMT9_TOT  = dAMT9_TOT  + Convert.ToDouble(table.Rows[i]["AMT9"].ToString());
                        dAMT10_TOT = dAMT10_TOT + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
                        dAMT11_TOT = dAMT11_TOT + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                        dAMT12_TOT = dAMT12_TOT + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                        dHAP_TOT   = dHAP_TOT   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

                        nNum = nNum + 1;

                        i = i + 1;

                    }
                }
            }

            // 소계
            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            table.Rows[i]["DATE"]     = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["Y2CDSAUP"] = table.Rows[i - 1]["Y2CDSAUP"].ToString();
            table.Rows[i]["Y2NMDP"]   = table.Rows[i - 1]["Y2NMDP"].ToString();

            // 소 계 이름 넣기
            table.Rows[i]["Y2DPSBNM"] = "소   계";

            sY2NMDP = "Y2NMDP = '" + table.Rows[i - 1]["Y2NMDP"].ToString() + "' ";

            table.Rows[i]["AMT1"]  = table.Compute("SUM(AMT1)",  sY2NMDP).ToString();
            table.Rows[i]["AMT2"]  = table.Compute("SUM(AMT2)",  sY2NMDP).ToString();
            table.Rows[i]["AMT3"]  = table.Compute("SUM(AMT3)",  sY2NMDP).ToString();
            table.Rows[i]["AMT4"]  = table.Compute("SUM(AMT4)",  sY2NMDP).ToString();
            table.Rows[i]["AMT5"]  = table.Compute("SUM(AMT5)",  sY2NMDP).ToString();
            table.Rows[i]["AMT6"]  = table.Compute("SUM(AMT6)",  sY2NMDP).ToString();
            table.Rows[i]["AMT7"]  = table.Compute("SUM(AMT7)",  sY2NMDP).ToString();
            table.Rows[i]["AMT8"]  = table.Compute("SUM(AMT8)",  sY2NMDP).ToString();
            table.Rows[i]["AMT9"]  = table.Compute("SUM(AMT9)",  sY2NMDP).ToString();
            table.Rows[i]["AMT10"] = table.Compute("SUM(AMT10)", sY2NMDP).ToString();
            table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sY2NMDP).ToString();
            table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sY2NMDP).ToString();
            table.Rows[i]["HAP"]   = table.Compute("SUM(HAP)",   sY2NMDP).ToString();

            // 사업부계
            dAMT1_SA  = dAMT1_SA  + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
            dAMT2_SA  = dAMT2_SA  + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
            dAMT3_SA  = dAMT3_SA  + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
            dAMT4_SA  = dAMT4_SA  + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
            dAMT5_SA  = dAMT5_SA  + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());
            dAMT6_SA  = dAMT6_SA  + Convert.ToDouble(table.Rows[i]["AMT6"].ToString());
            dAMT7_SA  = dAMT7_SA  + Convert.ToDouble(table.Rows[i]["AMT7"].ToString());
            dAMT8_SA  = dAMT8_SA  + Convert.ToDouble(table.Rows[i]["AMT8"].ToString());
            dAMT9_SA  = dAMT9_SA  + Convert.ToDouble(table.Rows[i]["AMT9"].ToString());
            dAMT10_SA = dAMT10_SA + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
            dAMT11_SA = dAMT11_SA + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
            dAMT12_SA = dAMT12_SA + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
            dHAP_SA   = dHAP_SA   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

            // 총계
            dAMT1_TOT  = dAMT1_TOT  + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
            dAMT2_TOT  = dAMT2_TOT  + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
            dAMT3_TOT  = dAMT3_TOT  + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
            dAMT4_TOT  = dAMT4_TOT  + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
            dAMT5_TOT  = dAMT5_TOT  + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());
            dAMT6_TOT  = dAMT6_TOT  + Convert.ToDouble(table.Rows[i]["AMT6"].ToString());
            dAMT7_TOT  = dAMT7_TOT  + Convert.ToDouble(table.Rows[i]["AMT7"].ToString());
            dAMT8_TOT  = dAMT8_TOT  + Convert.ToDouble(table.Rows[i]["AMT8"].ToString());
            dAMT9_TOT  = dAMT9_TOT  + Convert.ToDouble(table.Rows[i]["AMT9"].ToString());
            dAMT10_TOT = dAMT10_TOT + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
            dAMT11_TOT = dAMT11_TOT + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
            dAMT12_TOT = dAMT12_TOT + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
            dHAP_TOT   = dHAP_TOT   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

            // 사업부 소계
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);

            table.Rows[i + 1]["DATE"]     = table.Rows[i - 2]["DATE"].ToString();
            table.Rows[i + 1]["Y2CDSAUP"] = table.Rows[i - 2]["Y2CDSAUP"].ToString();
            table.Rows[i + 1]["Y2NMDP"]   = table.Rows[i - 2]["Y2NMDP"].ToString();

            // 사업부 소계 이름 넣기
            table.Rows[i + 1]["Y2DPSBNM"] = "사업부 소계";

            table.Rows[i + 1]["AMT1"]  = string.Format("{0:###0}", dAMT1_SA);
            table.Rows[i + 1]["AMT2"]  = string.Format("{0:###0}", dAMT2_SA);
            table.Rows[i + 1]["AMT3"]  = string.Format("{0:###0}", dAMT3_SA);
            table.Rows[i + 1]["AMT4"]  = string.Format("{0:###0}", dAMT4_SA);
            table.Rows[i + 1]["AMT5"]  = string.Format("{0:###0}", dAMT5_SA);
            table.Rows[i + 1]["AMT6"]  = string.Format("{0:###0}", dAMT6_SA);
            table.Rows[i + 1]["AMT7"]  = string.Format("{0:###0}", dAMT7_SA);
            table.Rows[i + 1]["AMT8"]  = string.Format("{0:###0}", dAMT8_SA);
            table.Rows[i + 1]["AMT9"]  = string.Format("{0:###0}", dAMT9_SA);
            table.Rows[i + 1]["AMT10"] = string.Format("{0:###0}", dAMT10_SA);
            table.Rows[i + 1]["AMT11"] = string.Format("{0:###0}", dAMT11_SA);
            table.Rows[i + 1]["AMT12"] = string.Format("{0:###0}", dAMT12_SA);
            table.Rows[i + 1]["HAP"]   = string.Format("{0:###0}", dHAP_SA);

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 2);

            table.Rows[i + 2]["DATE"]     = table.Rows[i - 3]["DATE"].ToString();
            table.Rows[i + 2]["Y2CDSAUP"] = table.Rows[i - 3]["Y2CDSAUP"].ToString();
            table.Rows[i + 2]["Y2NMDP"]   = table.Rows[i - 3]["Y2NMDP"].ToString();

            // 계 이름 넣기
            table.Rows[i + 2]["Y2DPSBNM"] = "계";

            table.Rows[i + 2]["AMT1"]  = string.Format("{0:###0}", dAMT1_TOT);
            table.Rows[i + 2]["AMT2"]  = string.Format("{0:###0}", dAMT2_TOT);
            table.Rows[i + 2]["AMT3"]  = string.Format("{0:###0}", dAMT3_TOT);
            table.Rows[i + 2]["AMT4"]  = string.Format("{0:###0}", dAMT4_TOT);
            table.Rows[i + 2]["AMT5"]  = string.Format("{0:###0}", dAMT5_TOT);
            table.Rows[i + 2]["AMT6"]  = string.Format("{0:###0}", dAMT6_TOT);
            table.Rows[i + 2]["AMT7"]  = string.Format("{0:###0}", dAMT7_TOT);
            table.Rows[i + 2]["AMT8"]  = string.Format("{0:###0}", dAMT8_TOT);
            table.Rows[i + 2]["AMT9"]  = string.Format("{0:###0}", dAMT9_TOT);
            table.Rows[i + 2]["AMT10"] = string.Format("{0:###0}", dAMT10_TOT);
            table.Rows[i + 2]["AMT11"] = string.Format("{0:###0}", dAMT11_TOT);
            table.Rows[i + 2]["AMT12"] = string.Format("{0:###0}", dAMT12_TOT);
            table.Rows[i + 2]["HAP"]   = string.Format("{0:###0}", dHAP_TOT);

            DataTable Condt = new DataTable();

            Condt.Columns.Add("Y2NMSAUP", typeof(System.String));
            Condt.Columns.Add("Y2DPSBNM", typeof(System.String));
            Condt.Columns.Add("Y2NMAC",   typeof(System.String));
            Condt.Columns.Add("AMT1",     typeof(System.String));
            Condt.Columns.Add("AMT2",     typeof(System.String));
            Condt.Columns.Add("AMT3",     typeof(System.String));
            Condt.Columns.Add("AMT4",     typeof(System.String));
            Condt.Columns.Add("AMT5",     typeof(System.String));
            Condt.Columns.Add("AMT6",     typeof(System.String));
            Condt.Columns.Add("AMT7",     typeof(System.String));
            Condt.Columns.Add("AMT8",     typeof(System.String));
            Condt.Columns.Add("AMT9",     typeof(System.String));
            Condt.Columns.Add("AMT10",    typeof(System.String));
            Condt.Columns.Add("AMT11",    typeof(System.String));
            Condt.Columns.Add("AMT12",    typeof(System.String));
            Condt.Columns.Add("HAP",      typeof(System.String));
            Condt.Columns.Add("DATE",     typeof(System.String));

            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWY2CDSAUP = table.Rows[i]["Y2CDSAUP"].ToString();
                sNEWY2DPSBNM = table.Rows[i]["Y2DPSBNM"].ToString();

                row = Condt.NewRow();

                if (sNEWY2CDSAUP != sOldY2CDSAUP)
                {
                    row["Y2NMSAUP"] = table.Rows[i]["Y2NMSAUP"].ToString();

                    sOldY2CDSAUP = sNEWY2CDSAUP;
                }
                else
                {
                    row["Y2NMSAUP"] = "";
                }

                if (sNEWY2DPSBNM != sOldY2DPSBNM)
                {
                    row["Y2DPSBNM"] = table.Rows[i]["Y2DPSBNM"].ToString();

                    sOldY2DPSBNM = sNEWY2DPSBNM;
                }
                else
                {
                    row["Y2DPSBNM"] = "";
                }

                row["Y2NMAC"] = table.Rows[i]["Y2NMAC"].ToString();
                row["AMT1"]   = table.Rows[i]["AMT1"].ToString();
                row["AMT2"]   = table.Rows[i]["AMT2"].ToString();
                row["AMT3"]   = table.Rows[i]["AMT3"].ToString();
                row["AMT4"]   = table.Rows[i]["AMT4"].ToString();
                row["AMT5"]   = table.Rows[i]["AMT5"].ToString();
                row["AMT6"]   = table.Rows[i]["AMT6"].ToString();
                row["AMT7"]   = table.Rows[i]["AMT7"].ToString();
                row["AMT8"]   = table.Rows[i]["AMT8"].ToString();
                row["AMT9"]   = table.Rows[i]["AMT9"].ToString();
                row["AMT10"]  = table.Rows[i]["AMT10"].ToString();
                row["AMT11"]  = table.Rows[i]["AMT11"].ToString();
                row["AMT12"]  = table.Rows[i]["AMT12"].ToString();
                row["HAP"]    = table.Rows[i]["HAP"].ToString();
                row["DATE"]   = table.Rows[i]["DATE"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion
    }
}