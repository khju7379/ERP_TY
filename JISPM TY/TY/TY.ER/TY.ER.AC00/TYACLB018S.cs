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
    /// 소모품비세목예산명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.10 10:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29AAG884 : 소모품비세목예산명세서
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29AAH885 : 소모품비세목예산명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GYESAN : 예산구분
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACLB018S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB018S()
        {
            InitializeComponent();
        }

        private void TYACLB018S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.TXT01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_29AAG884",
                this.TXT01_GDATE.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_29AAH885.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_29AAH885.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_29AAH885.GetValue(i, "J2CDAC").ToString() == "계 정 계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29AAH885.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    else if (this.FPS91_TY_S_AC_29AAH885.GetValue(i, "J2CDAC").ToString() == "부 서 계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29AAH885.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_29AAH885.GetValue(i, "J2CDAC").ToString() == "총     계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29AAH885.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
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
                "TY_P_AC_29AAG884",
                this.TXT01_GDATE.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt1 = new TYACLB018R();

                // 가로 출력
                rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt1, UP_ConvertDt(dt))).ShowDialog();
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

            string sNEWCDDESC1 = string.Empty;
            string sOldCDDESC1 = string.Empty;

            string sNEWJ2NMAC  = string.Empty;
            string sOldJ2NMAC  = string.Empty;

            string sNEWJ2CDDP  = string.Empty;
            string sOldJ2CDDP  = string.Empty;

            string sNEWJ2CDAC = string.Empty;
            string sOldJ2CDAC = string.Empty;

            string sCDAC       = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["J2CDDP"].ToString() != table.Rows[i]["J2CDDP"].ToString())
                {
                    // 소계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["J2GUBN"]  = table.Rows[i - 1]["J2GUBN"].ToString();
                    table.Rows[i]["J2CDDP"]  = table.Rows[i - 1]["J2CDDP"].ToString();
                    table.Rows[i]["BUSEO"]   = table.Rows[i - 1]["BUSEO"].ToString();
                    table.Rows[i]["DATE"]    = table.Rows[i - 1]["DATE"].ToString();
                    table.Rows[i]["CDAC"]    = table.Rows[i - 1]["CDAC"].ToString();
                    table.Rows[i]["J2NMAC"]  = table.Rows[i - 1]["J2NMAC"].ToString();
                    table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();

                    // 소 계 이름 넣기
                    table.Rows[i]["J2CDAC"] = "계 정 계";

                    sCDAC = "CDAC = '" + table.Rows[i - 1]["CDAC"].ToString() + "' ";
                    sCDAC += " AND J2CDDP = '" + table.Rows[i - 1]["J2CDDP"].ToString() + "'  ";

                    table.Rows[i]["AMT01"] = table.Compute("SUM(AMT01)", sCDAC).ToString();
                    table.Rows[i]["AMT02"] = table.Compute("SUM(AMT02)", sCDAC).ToString();
                    table.Rows[i]["AMT03"] = table.Compute("SUM(AMT03)", sCDAC).ToString();
                    table.Rows[i]["AMT04"] = table.Compute("SUM(AMT04)", sCDAC).ToString();
                    table.Rows[i]["AMT05"] = table.Compute("SUM(AMT05)", sCDAC).ToString();
                    table.Rows[i]["AMT06"] = table.Compute("SUM(AMT06)", sCDAC).ToString();
                    table.Rows[i]["AMT07"] = table.Compute("SUM(AMT07)", sCDAC).ToString();
                    table.Rows[i]["AMT08"] = table.Compute("SUM(AMT08)", sCDAC).ToString();
                    table.Rows[i]["AMT09"] = table.Compute("SUM(AMT09)", sCDAC).ToString();
                    table.Rows[i]["AMT10"] = table.Compute("SUM(AMT10)", sCDAC).ToString();
                    table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sCDAC).ToString();
                    table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sCDAC).ToString();
                    table.Rows[i]["HAP"]   = table.Compute("SUM(HAP)",   sCDAC).ToString();

                    // 사업부계
                    dAMT1_SA  = dAMT1_SA  + Convert.ToDouble(table.Rows[i]["AMT01"].ToString());
                    dAMT2_SA  = dAMT2_SA  + Convert.ToDouble(table.Rows[i]["AMT02"].ToString());
                    dAMT3_SA  = dAMT3_SA  + Convert.ToDouble(table.Rows[i]["AMT03"].ToString());
                    dAMT4_SA  = dAMT4_SA  + Convert.ToDouble(table.Rows[i]["AMT04"].ToString());
                    dAMT5_SA  = dAMT5_SA  + Convert.ToDouble(table.Rows[i]["AMT05"].ToString());
                    dAMT6_SA  = dAMT6_SA  + Convert.ToDouble(table.Rows[i]["AMT06"].ToString());
                    dAMT7_SA  = dAMT7_SA  + Convert.ToDouble(table.Rows[i]["AMT07"].ToString());
                    dAMT8_SA  = dAMT8_SA  + Convert.ToDouble(table.Rows[i]["AMT08"].ToString());
                    dAMT9_SA  = dAMT9_SA  + Convert.ToDouble(table.Rows[i]["AMT09"].ToString());
                    dAMT10_SA = dAMT10_SA + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
                    dAMT11_SA = dAMT11_SA + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                    dAMT12_SA = dAMT12_SA + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                    dHAP_SA   = dHAP_SA   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

                    // 총계
                    dAMT1_TOT  = dAMT1_TOT  + Convert.ToDouble(table.Rows[i]["AMT01"].ToString());
                    dAMT2_TOT  = dAMT2_TOT  + Convert.ToDouble(table.Rows[i]["AMT02"].ToString());
                    dAMT3_TOT  = dAMT3_TOT  + Convert.ToDouble(table.Rows[i]["AMT03"].ToString());
                    dAMT4_TOT  = dAMT4_TOT  + Convert.ToDouble(table.Rows[i]["AMT04"].ToString());
                    dAMT5_TOT  = dAMT5_TOT  + Convert.ToDouble(table.Rows[i]["AMT05"].ToString());
                    dAMT6_TOT  = dAMT6_TOT  + Convert.ToDouble(table.Rows[i]["AMT06"].ToString());
                    dAMT7_TOT  = dAMT7_TOT  + Convert.ToDouble(table.Rows[i]["AMT07"].ToString());
                    dAMT8_TOT  = dAMT8_TOT  + Convert.ToDouble(table.Rows[i]["AMT08"].ToString());
                    dAMT9_TOT  = dAMT9_TOT  + Convert.ToDouble(table.Rows[i]["AMT09"].ToString());
                    dAMT10_TOT = dAMT10_TOT + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
                    dAMT11_TOT = dAMT11_TOT + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                    dAMT12_TOT = dAMT12_TOT + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                    dHAP_TOT   = dHAP_TOT   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

                    i = i + 1;

                    // 부 서 계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["J2GUBN"]  = table.Rows[i - 1]["J2GUBN"].ToString();
                    table.Rows[i]["J2CDDP"]  = table.Rows[i - 1]["J2CDDP"].ToString();
                    table.Rows[i]["BUSEO"]   = table.Rows[i - 1]["BUSEO"].ToString();
                    table.Rows[i]["DATE"]    = table.Rows[i - 1]["DATE"].ToString();
                    table.Rows[i]["CDAC"]    = table.Rows[i - 1]["CDAC"].ToString();
                    table.Rows[i]["J2NMAC"]  = table.Rows[i - 1]["J2NMAC"].ToString();
                    table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();

                    // 사업부 소계 이름 넣기
                    table.Rows[i]["J2CDAC"] = "부 서 계";

                    table.Rows[i]["AMT01"] = string.Format("{0:###0}", dAMT1_SA);
                    table.Rows[i]["AMT02"] = string.Format("{0:###0}", dAMT2_SA);
                    table.Rows[i]["AMT03"] = string.Format("{0:###0}", dAMT3_SA);
                    table.Rows[i]["AMT04"] = string.Format("{0:###0}", dAMT4_SA);
                    table.Rows[i]["AMT05"] = string.Format("{0:###0}", dAMT5_SA);
                    table.Rows[i]["AMT06"] = string.Format("{0:###0}", dAMT6_SA);
                    table.Rows[i]["AMT07"] = string.Format("{0:###0}", dAMT7_SA);
                    table.Rows[i]["AMT08"] = string.Format("{0:###0}", dAMT8_SA);
                    table.Rows[i]["AMT09"] = string.Format("{0:###0}", dAMT9_SA);
                    table.Rows[i]["AMT10"] = string.Format("{0:###0}", dAMT10_SA);
                    table.Rows[i]["AMT11"] = string.Format("{0:###0}", dAMT11_SA);
                    table.Rows[i]["AMT12"] = string.Format("{0:###0}", dAMT12_SA);
                    table.Rows[i]["HAP"]   = string.Format("{0:###0}", dHAP_SA);

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
                    if (table.Rows[i - 1]["CDAC"].ToString() != table.Rows[i]["CDAC"].ToString())
                    {
                        // 소계
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["J2GUBN"]  = table.Rows[i - 1]["J2GUBN"].ToString();
                        table.Rows[i]["J2CDDP"]  = table.Rows[i - 1]["J2CDDP"].ToString();
                        table.Rows[i]["BUSEO"]   = table.Rows[i - 1]["BUSEO"].ToString();
                        table.Rows[i]["DATE"]    = table.Rows[i - 1]["DATE"].ToString();
                        table.Rows[i]["CDAC"]    = table.Rows[i - 1]["CDAC"].ToString();
                        table.Rows[i]["J2NMAC"]  = table.Rows[i - 1]["J2NMAC"].ToString();
                        table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();

                        // 소 계 이름 넣기
                        table.Rows[i]["J2CDAC"] = "계 정 계";

                        sCDAC = "CDAC = '" + table.Rows[i - 1]["CDAC"].ToString() + "' " ;
                        sCDAC += " AND J2CDDP = '" + table.Rows[i - 1]["J2CDDP"].ToString() + "'  ";

                        table.Rows[i]["AMT01"] = table.Compute("SUM(AMT01)", sCDAC).ToString();
                        table.Rows[i]["AMT02"] = table.Compute("SUM(AMT02)", sCDAC).ToString();
                        table.Rows[i]["AMT03"] = table.Compute("SUM(AMT03)", sCDAC).ToString();
                        table.Rows[i]["AMT04"] = table.Compute("SUM(AMT04)", sCDAC).ToString();
                        table.Rows[i]["AMT05"] = table.Compute("SUM(AMT05)", sCDAC).ToString();
                        table.Rows[i]["AMT06"] = table.Compute("SUM(AMT06)", sCDAC).ToString();
                        table.Rows[i]["AMT07"] = table.Compute("SUM(AMT07)", sCDAC).ToString();
                        table.Rows[i]["AMT08"] = table.Compute("SUM(AMT08)", sCDAC).ToString();
                        table.Rows[i]["AMT09"] = table.Compute("SUM(AMT09)", sCDAC).ToString();
                        table.Rows[i]["AMT10"] = table.Compute("SUM(AMT10)", sCDAC).ToString();
                        table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sCDAC).ToString();
                        table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sCDAC).ToString();
                        table.Rows[i]["HAP"]   = table.Compute("SUM(HAP)",   sCDAC).ToString();

                        // 사업부계
                        dAMT1_SA  = dAMT1_SA  + Convert.ToDouble(table.Rows[i]["AMT01"].ToString());
                        dAMT2_SA  = dAMT2_SA  + Convert.ToDouble(table.Rows[i]["AMT02"].ToString());
                        dAMT3_SA  = dAMT3_SA  + Convert.ToDouble(table.Rows[i]["AMT03"].ToString());
                        dAMT4_SA  = dAMT4_SA  + Convert.ToDouble(table.Rows[i]["AMT04"].ToString());
                        dAMT5_SA  = dAMT5_SA  + Convert.ToDouble(table.Rows[i]["AMT05"].ToString());
                        dAMT6_SA  = dAMT6_SA  + Convert.ToDouble(table.Rows[i]["AMT06"].ToString());
                        dAMT7_SA  = dAMT7_SA  + Convert.ToDouble(table.Rows[i]["AMT07"].ToString());
                        dAMT8_SA  = dAMT8_SA  + Convert.ToDouble(table.Rows[i]["AMT08"].ToString());
                        dAMT9_SA  = dAMT9_SA  + Convert.ToDouble(table.Rows[i]["AMT09"].ToString());
                        dAMT10_SA = dAMT10_SA + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
                        dAMT11_SA = dAMT11_SA + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                        dAMT12_SA = dAMT12_SA + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                        dHAP_SA   = dHAP_SA   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

                        // 총계
                        dAMT1_TOT  = dAMT1_TOT  + Convert.ToDouble(table.Rows[i]["AMT01"].ToString());
                        dAMT2_TOT  = dAMT2_TOT  + Convert.ToDouble(table.Rows[i]["AMT02"].ToString());
                        dAMT3_TOT  = dAMT3_TOT  + Convert.ToDouble(table.Rows[i]["AMT03"].ToString());
                        dAMT4_TOT  = dAMT4_TOT  + Convert.ToDouble(table.Rows[i]["AMT04"].ToString());
                        dAMT5_TOT  = dAMT5_TOT  + Convert.ToDouble(table.Rows[i]["AMT05"].ToString());
                        dAMT6_TOT  = dAMT6_TOT  + Convert.ToDouble(table.Rows[i]["AMT06"].ToString());
                        dAMT7_TOT  = dAMT7_TOT  + Convert.ToDouble(table.Rows[i]["AMT07"].ToString());
                        dAMT8_TOT  = dAMT8_TOT  + Convert.ToDouble(table.Rows[i]["AMT08"].ToString());
                        dAMT9_TOT  = dAMT9_TOT  + Convert.ToDouble(table.Rows[i]["AMT09"].ToString());
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

            table.Rows[i]["J2GUBN"]  = table.Rows[i - 1]["J2GUBN"].ToString();
            table.Rows[i]["J2CDDP"]  = table.Rows[i - 1]["J2CDDP"].ToString();
            table.Rows[i]["BUSEO"]   = table.Rows[i - 1]["BUSEO"].ToString();
            table.Rows[i]["DATE"]    = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["CDAC"]    = table.Rows[i - 1]["CDAC"].ToString();
            table.Rows[i]["J2NMAC"]  = table.Rows[i - 1]["J2NMAC"].ToString();
            table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();

            // 소 계 이름 넣기
            table.Rows[i]["J2CDAC"] = "계 정 계";

            sCDAC = "CDAC = '" + table.Rows[i - 1]["CDAC"].ToString() + "' " ;
            sCDAC += " AND J2CDDP = '" + table.Rows[i - 1]["J2CDDP"].ToString() + "'  ";

            table.Rows[i]["AMT01"] = table.Compute("SUM(AMT01)", sCDAC).ToString();
            table.Rows[i]["AMT02"] = table.Compute("SUM(AMT02)", sCDAC).ToString();
            table.Rows[i]["AMT03"] = table.Compute("SUM(AMT03)", sCDAC).ToString();
            table.Rows[i]["AMT04"] = table.Compute("SUM(AMT04)", sCDAC).ToString();
            table.Rows[i]["AMT05"] = table.Compute("SUM(AMT05)", sCDAC).ToString();
            table.Rows[i]["AMT06"] = table.Compute("SUM(AMT06)", sCDAC).ToString();
            table.Rows[i]["AMT07"] = table.Compute("SUM(AMT07)", sCDAC).ToString();
            table.Rows[i]["AMT08"] = table.Compute("SUM(AMT08)", sCDAC).ToString();
            table.Rows[i]["AMT09"] = table.Compute("SUM(AMT09)", sCDAC).ToString();
            table.Rows[i]["AMT10"] = table.Compute("SUM(AMT10)", sCDAC).ToString();
            table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sCDAC).ToString();
            table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sCDAC).ToString();
            table.Rows[i]["HAP"]   = table.Compute("SUM(HAP)",   sCDAC).ToString();

            // 사업부계
            dAMT1_SA  = dAMT1_SA  + Convert.ToDouble(table.Rows[i]["AMT01"].ToString());
            dAMT2_SA  = dAMT2_SA  + Convert.ToDouble(table.Rows[i]["AMT02"].ToString());
            dAMT3_SA  = dAMT3_SA  + Convert.ToDouble(table.Rows[i]["AMT03"].ToString());
            dAMT4_SA  = dAMT4_SA  + Convert.ToDouble(table.Rows[i]["AMT04"].ToString());
            dAMT5_SA  = dAMT5_SA  + Convert.ToDouble(table.Rows[i]["AMT05"].ToString());
            dAMT6_SA  = dAMT6_SA  + Convert.ToDouble(table.Rows[i]["AMT06"].ToString());
            dAMT7_SA  = dAMT7_SA  + Convert.ToDouble(table.Rows[i]["AMT07"].ToString());
            dAMT8_SA  = dAMT8_SA  + Convert.ToDouble(table.Rows[i]["AMT08"].ToString());
            dAMT9_SA  = dAMT9_SA  + Convert.ToDouble(table.Rows[i]["AMT09"].ToString());
            dAMT10_SA = dAMT10_SA + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
            dAMT11_SA = dAMT11_SA + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
            dAMT12_SA = dAMT12_SA + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
            dHAP_SA   = dHAP_SA   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

            // 총계
            dAMT1_TOT  = dAMT1_TOT  + Convert.ToDouble(table.Rows[i]["AMT01"].ToString());
            dAMT2_TOT  = dAMT2_TOT  + Convert.ToDouble(table.Rows[i]["AMT02"].ToString());
            dAMT3_TOT  = dAMT3_TOT  + Convert.ToDouble(table.Rows[i]["AMT03"].ToString());
            dAMT4_TOT  = dAMT4_TOT  + Convert.ToDouble(table.Rows[i]["AMT04"].ToString());
            dAMT5_TOT  = dAMT5_TOT  + Convert.ToDouble(table.Rows[i]["AMT05"].ToString());
            dAMT6_TOT  = dAMT6_TOT  + Convert.ToDouble(table.Rows[i]["AMT06"].ToString());
            dAMT7_TOT  = dAMT7_TOT  + Convert.ToDouble(table.Rows[i]["AMT07"].ToString());
            dAMT8_TOT  = dAMT8_TOT  + Convert.ToDouble(table.Rows[i]["AMT08"].ToString());
            dAMT9_TOT  = dAMT9_TOT  + Convert.ToDouble(table.Rows[i]["AMT09"].ToString());
            dAMT10_TOT = dAMT10_TOT + Convert.ToDouble(table.Rows[i]["AMT10"].ToString());
            dAMT11_TOT = dAMT11_TOT + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
            dAMT12_TOT = dAMT12_TOT + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
            dHAP_TOT   = dHAP_TOT   + Convert.ToDouble(table.Rows[i]["HAP"].ToString());

            i = i + 1;
            // 사업부 소계
            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            table.Rows[i]["J2GUBN"]  = table.Rows[i - 1]["J2GUBN"].ToString();
            table.Rows[i]["J2CDDP"]  = table.Rows[i - 1]["J2CDDP"].ToString();
            table.Rows[i]["BUSEO"]   = table.Rows[i - 1]["BUSEO"].ToString();
            table.Rows[i]["DATE"]    = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["CDAC"]    = table.Rows[i - 1]["CDAC"].ToString();
            table.Rows[i]["J2NMAC"]  = table.Rows[i - 1]["J2NMAC"].ToString();
            table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();

            // 사업부 소계 이름 넣기
            table.Rows[i]["J2CDAC"] = "부 서 계";

            table.Rows[i]["AMT01"] = string.Format("{0:###0}", dAMT1_SA);
            table.Rows[i]["AMT02"] = string.Format("{0:###0}", dAMT2_SA);
            table.Rows[i]["AMT03"] = string.Format("{0:###0}", dAMT3_SA);
            table.Rows[i]["AMT04"] = string.Format("{0:###0}", dAMT4_SA);
            table.Rows[i]["AMT05"] = string.Format("{0:###0}", dAMT5_SA);
            table.Rows[i]["AMT06"] = string.Format("{0:###0}", dAMT6_SA);
            table.Rows[i]["AMT07"] = string.Format("{0:###0}", dAMT7_SA);
            table.Rows[i]["AMT08"] = string.Format("{0:###0}", dAMT8_SA);
            table.Rows[i]["AMT09"] = string.Format("{0:###0}", dAMT9_SA);
            table.Rows[i]["AMT10"] = string.Format("{0:###0}", dAMT10_SA);
            table.Rows[i]["AMT11"] = string.Format("{0:###0}", dAMT11_SA);
            table.Rows[i]["AMT12"] = string.Format("{0:###0}", dAMT12_SA);
            table.Rows[i]["HAP"]   = string.Format("{0:###0}", dHAP_SA);

            i = i + 1;
            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            table.Rows[i]["J2GUBN"]  = table.Rows[i - 1]["J2GUBN"].ToString();
            table.Rows[i]["J2CDDP"]  = table.Rows[i - 1]["J2CDDP"].ToString();
            table.Rows[i]["BUSEO"]   = table.Rows[i - 1]["BUSEO"].ToString();
            table.Rows[i]["DATE"]    = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["CDAC"]    = table.Rows[i - 1]["CDAC"].ToString();
            table.Rows[i]["J2NMAC"]  = table.Rows[i - 1]["J2NMAC"].ToString();
            table.Rows[i]["CDDESC1"] = table.Rows[i - 1]["CDDESC1"].ToString();

            // 계 이름 넣기
            table.Rows[i]["J2CDAC"] = "총     계";

            table.Rows[i]["AMT01"] = string.Format("{0:###0}", dAMT1_TOT);
            table.Rows[i]["AMT02"] = string.Format("{0:###0}", dAMT2_TOT);
            table.Rows[i]["AMT03"] = string.Format("{0:###0}", dAMT3_TOT);
            table.Rows[i]["AMT04"] = string.Format("{0:###0}", dAMT4_TOT);
            table.Rows[i]["AMT05"] = string.Format("{0:###0}", dAMT5_TOT);
            table.Rows[i]["AMT06"] = string.Format("{0:###0}", dAMT6_TOT);
            table.Rows[i]["AMT07"] = string.Format("{0:###0}", dAMT7_TOT);
            table.Rows[i]["AMT08"] = string.Format("{0:###0}", dAMT8_TOT);
            table.Rows[i]["AMT09"] = string.Format("{0:###0}", dAMT9_TOT);
            table.Rows[i]["AMT10"] = string.Format("{0:###0}", dAMT10_TOT);
            table.Rows[i]["AMT11"] = string.Format("{0:###0}", dAMT11_TOT);
            table.Rows[i]["AMT12"] = string.Format("{0:###0}", dAMT12_TOT);
            table.Rows[i]["HAP"]   = string.Format("{0:###0}", dHAP_TOT);

            DataTable Condt = new DataTable();

            Condt.Columns.Add("J2GUBN",  typeof(System.String));
            Condt.Columns.Add("J2CDDP",  typeof(System.String));
            Condt.Columns.Add("BUSEO",   typeof(System.String));
            Condt.Columns.Add("CDAC",    typeof(System.String));
            Condt.Columns.Add("J2CDAC",  typeof(System.String));
            Condt.Columns.Add("J2CDJJ",  typeof(System.String));
            Condt.Columns.Add("J2NMAC",  typeof(System.String));
            Condt.Columns.Add("J2SEQ",   typeof(System.String));
            Condt.Columns.Add("CDDESC1", typeof(System.String));
            Condt.Columns.Add("J2NMJJ",  typeof(System.String));
            Condt.Columns.Add("J1RKAC",  typeof(System.String));
            Condt.Columns.Add("AMT01",   typeof(System.String));
            Condt.Columns.Add("AMT02",   typeof(System.String));
            Condt.Columns.Add("AMT03",   typeof(System.String));
            Condt.Columns.Add("AMT04",   typeof(System.String));
            Condt.Columns.Add("AMT05",   typeof(System.String));
            Condt.Columns.Add("AMT06",   typeof(System.String));
            Condt.Columns.Add("AMT07",   typeof(System.String));
            Condt.Columns.Add("AMT08",   typeof(System.String));
            Condt.Columns.Add("AMT09",   typeof(System.String));
            Condt.Columns.Add("AMT10",   typeof(System.String));
            Condt.Columns.Add("AMT11",   typeof(System.String));
            Condt.Columns.Add("AMT12",   typeof(System.String));
            Condt.Columns.Add("HAP",     typeof(System.String));
            Condt.Columns.Add("DATE",    typeof(System.String));

            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWCDDESC1 = table.Rows[i]["CDDESC1"].ToString();
                sNEWJ2NMAC  = table.Rows[i]["J2NMAC"].ToString();
                sNEWJ2CDDP  = table.Rows[i]["J2CDDP"].ToString();
                sNEWJ2CDAC  = table.Rows[i]["J2CDAC"].ToString();
                

                row = Condt.NewRow();

                if (sNEWJ2CDDP != sOldJ2CDDP)
                {
                    row["CDDESC1"] = table.Rows[i]["CDDESC1"].ToString();
                    sOldCDDESC1    = sNEWCDDESC1;

                    row["J2CDDP"]  = table.Rows[i]["J2CDDP"].ToString();
                    sOldJ2CDDP     = sNEWJ2CDDP;

                }
                else
                {
                    row["CDDESC1"] = "";
                    row["J2CDDP"]  = "";
                }

                if (sNEWJ2CDAC != sOldJ2CDAC)
                {
                    if (table.Rows[i]["J2CDAC"].ToString() == "계 정 계" || table.Rows[i]["J2CDAC"].ToString() == "부 서 계" || table.Rows[i]["J2CDAC"].ToString() == "총     계")
                    {
                        row["J2NMAC"] = "";
                    }
                    else
                    {
                        row["J2NMAC"] = table.Rows[i]["J2NMAC"].ToString();
                    }

                    sOldJ2NMAC = sNEWJ2NMAC;

                    row["J2CDAC"] = table.Rows[i]["J2CDAC"].ToString();
                    sOldJ2CDAC    = sNEWJ2CDAC;
                }
                else
                {
                    row["J2NMAC"] = "";

                    row["J2CDAC"] = "";
                }

                row["J2GUBN"] = table.Rows[i]["J2GUBN"].ToString();
                row["BUSEO"]  = table.Rows[i]["BUSEO"].ToString();
                row["CDAC"]   = table.Rows[i]["CDAC"].ToString();
                row["J2CDJJ"] = table.Rows[i]["J2CDJJ"].ToString();
                row["J2SEQ"]  = table.Rows[i]["J2SEQ"].ToString();
                row["J2NMJJ"] = table.Rows[i]["J2NMJJ"].ToString();
                row["J1RKAC"] = table.Rows[i]["J1RKAC"].ToString();
                row["AMT01"]  = table.Rows[i]["AMT01"].ToString();
                row["AMT02"]  = table.Rows[i]["AMT02"].ToString();
                row["AMT03"]  = table.Rows[i]["AMT03"].ToString();
                row["AMT04"]  = table.Rows[i]["AMT04"].ToString();
                row["AMT05"]  = table.Rows[i]["AMT05"].ToString();
                row["AMT06"]  = table.Rows[i]["AMT06"].ToString();
                row["AMT07"]  = table.Rows[i]["AMT07"].ToString();
                row["AMT08"]  = table.Rows[i]["AMT08"].ToString();
                row["AMT09"]  = table.Rows[i]["AMT09"].ToString();
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

        #region Description : 예산년도 이벤트
        private void TXT01_GDATE_TextChanged(object sender, EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = this.TXT01_GDATE.GetValue() + "0101";
        }
        #endregion
    }
}