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
    /// 계정별 예산 집계표 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.14 09:48
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29E9T014 : 계정별 예산 집계표
    ///  TY_P_AC_29EB1016 : 계정별 예산 명세서
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29E1K017 : 계정별 예산 집계표
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GPRTGN : 출력구분
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACLB020S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB020S()
        {
            InitializeComponent();
        }

        private void TYACLB020S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.TXT01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            string sEXISTS = string.Empty;

            if(this.CBO01_GPRTGN.GetValue().ToString() == "1") // 명세서
            {
                sProcedure = "TY_P_AC_29EB1016";
            }
            else
            {
                sProcedure = "TY_P_AC_29E9T014";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                this.TXT01_GDATE.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_29E1K017.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_29E1K017.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_29E1K017.GetValue(i, "A1ABAC").ToString() == "총 계")
                    {
                        sEXISTS = "EXISTS";

                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29E1K017.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }

                    if (sEXISTS != "")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29E1K017.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
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
            string sProcedure = string.Empty;

            string sEXISTS = string.Empty;

            if(this.CBO01_GPRTGN.GetValue().ToString() == "1") // 명세서
            {
                sProcedure = "TY_P_AC_29EB1016";
            }
            else
            {
                sProcedure = "TY_P_AC_29E9T014";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                this.TXT01_GDATE.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.CBO01_GPRTGN.GetValue().ToString() == "1") // 명세서
                {
                    SectionReport rpt1 = new TYACLB020R1();

                    // 가로 출력
                    rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt1, UP_ConvertDt(dt))).ShowDialog();
                }
                else
                {
                    SectionReport rpt2 = new TYACLB020R2();

                    // 가로 출력
                    rpt2.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt2, UP_ConvertDt(dt))).ShowDialog();
                }
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

            string sNEWA1ABAC = string.Empty;
            string sOLDA1ABAC = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            DataTable Condt = new DataTable();

            Condt.Columns.Add("MMYEAR",  typeof(System.String));
            Condt.Columns.Add("CDAC",    typeof(System.String));
            Condt.Columns.Add("A1ABAC",  typeof(System.String));
            Condt.Columns.Add("GBN",     typeof(System.String));
            Condt.Columns.Add("MMCDAC",  typeof(System.String));
            Condt.Columns.Add("CDDESC1", typeof(System.String));
            Condt.Columns.Add("CHK",     typeof(System.String));
            Condt.Columns.Add("GUBUN",   typeof(System.String));
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
            Condt.Columns.Add("YYYY",    typeof(System.String));
            Condt.Columns.Add("CDCM",    typeof(System.String));

            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWA1ABAC = table.Rows[i]["A1ABAC"].ToString();

                row = Condt.NewRow();

                if (sNEWA1ABAC != sOLDA1ABAC)
                {
                    row["A1ABAC"] = table.Rows[i]["A1ABAC"].ToString();

                    sOLDA1ABAC = sNEWA1ABAC;
                }
                else
                {
                    row["A1ABAC"] = "";
                }

                row["MMYEAR"]  = table.Rows[i]["MMYEAR"].ToString();
                row["CDAC"]    = table.Rows[i]["CDAC"].ToString();
                row["GBN"]     = table.Rows[i]["GBN"].ToString();
                row["MMCDAC"]  = table.Rows[i]["MMCDAC"].ToString();
                row["CDDESC1"] = table.Rows[i]["CDDESC1"].ToString();
                row["CHK"]     = table.Rows[i]["CHK"].ToString();
                row["GUBUN"]   = table.Rows[i]["GUBUN"].ToString();
                row["YYYY"]    = table.Rows[i]["YYYY"].ToString();
                row["CDCM"]    = table.Rows[i]["CDCM"].ToString();
                row["AMT01"]   = table.Rows[i]["AMT01"].ToString();
                row["AMT02"]   = table.Rows[i]["AMT02"].ToString();
                row["AMT03"]   = table.Rows[i]["AMT03"].ToString();
                row["AMT04"]   = table.Rows[i]["AMT04"].ToString();
                row["AMT05"]   = table.Rows[i]["AMT05"].ToString();
                row["AMT06"]   = table.Rows[i]["AMT06"].ToString();
                row["AMT07"]   = table.Rows[i]["AMT07"].ToString();
                row["AMT08"]   = table.Rows[i]["AMT08"].ToString();
                row["AMT09"]   = table.Rows[i]["AMT09"].ToString();
                row["AMT10"]   = table.Rows[i]["AMT10"].ToString();
                row["AMT11"]   = table.Rows[i]["AMT11"].ToString();
                row["AMT12"]   = table.Rows[i]["AMT12"].ToString();
                row["HAP"]     = table.Rows[i]["HAP"].ToString();

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