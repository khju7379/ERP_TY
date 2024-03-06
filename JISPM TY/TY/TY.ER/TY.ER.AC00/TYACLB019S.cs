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
    /// 기타예산계획/실적명세서 프로그램입니다.
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
    ///  TY_P_AC_29A4W905 : 기타예산계획/실적명세서
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29A5I906 : 기타예산계획/실적명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GCDAC : 계정코드
    ///  GYESAN : 예산구분
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACLB019S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB019S()
        {
            InitializeComponent();
        }

        private void TYACLB019S_Load(object sender, System.EventArgs e)
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
                "TY_P_AC_29A4W905",
                this.TXT01_GDATE.GetValue(),
                this.CBO01_GCDAC.GetValue(),
                this.CBO01_GCDAC.GetText(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetText(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_29A5I906.SetValue(UP_ConvertDt(dt, "SEL"));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_29A5I906.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_29A5I906.GetValue(i, "CHK").ToString() == "잔   액")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29A5I906.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
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
                "TY_P_AC_29A4W905",
                this.TXT01_GDATE.GetValue(),
                this.CBO01_GCDAC.GetValue(),
                this.CBO01_GCDAC.GetText(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetText(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt1 = new TYACLB019R();

                // 가로 출력
                rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt1, UP_ConvertDt(dt, "PRT"))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt, string sGUBUN)
        {
            int i = 0;

            string sNEWP2CDDP  = string.Empty;
            string sOLDP2CDDP  = string.Empty;

            string sNEWDSCHK = string.Empty;
            string sOldDSCHK = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            DataTable Condt = new DataTable();

            Condt.Columns.Add("CDMKNM",  typeof(System.String));
            Condt.Columns.Add("CDACNM",  typeof(System.String));
            Condt.Columns.Add("P2CDDP",  typeof(System.String));
            Condt.Columns.Add("CDDESC1", typeof(System.String));
            Condt.Columns.Add("P2CDAC",  typeof(System.String));
            Condt.Columns.Add("A1ABAC",  typeof(System.String));
            Condt.Columns.Add("GUBUN",   typeof(System.String));
            Condt.Columns.Add("P1RKAC",  typeof(System.String));
            Condt.Columns.Add("CHK",     typeof(System.String));
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
            Condt.Columns.Add("TOTAL",   typeof(System.String));
            Condt.Columns.Add("DATE",    typeof(System.String));

            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWDSCHK  = table.Rows[i]["DSCHK"].ToString();
                sNEWP2CDDP = table.Rows[i]["P2CDDP"].ToString();

                row = Condt.NewRow();

                if (sGUBUN == "SEL")
                {
                    if (i == 0)
                    {
                        row["CDMKNM"] = table.Rows[i]["CDMKNM"].ToString();
                        row["CDACNM"] = table.Rows[i]["CDACNM"].ToString();
                    }
                    else
                    {
                        row["CDMKNM"] = "";
                        row["CDACNM"] = "";
                    }

                    if (sNEWDSCHK != sOldDSCHK)
                    {
                        if (sNEWP2CDDP != sOLDP2CDDP)
                        {
                            row["P2CDDP"]  = table.Rows[i]["P2CDDP"].ToString();
                            row["CDDESC1"] = table.Rows[i]["CDDESC1"].ToString();

                            sOLDP2CDDP     = sNEWP2CDDP;
                        }
                        else
                        {
                            row["P2CDDP"]  = "";
                            row["CDDESC1"] = "";
                        }

                        row["P2CDAC"] = table.Rows[i]["P2CDAC"].ToString();
                        row["A1ABAC"] = table.Rows[i]["A1ABAC"].ToString();
                        row["GUBUN"]  = table.Rows[i]["GUBUN"].ToString();
                        row["P1RKAC"] = table.Rows[i]["P1RKAC"].ToString();

                        sOldDSCHK = sNEWDSCHK;
                    }
                    else
                    {
                        row["P2CDAC"] = "";
                        row["A1ABAC"] = "";
                        row["GUBUN"]  = "";
                        row["P1RKAC"] = "";
                    }
                }
                else
                {
                    row["DATE"]    = table.Rows[i]["DATE"].ToString();
                    row["CDMKNM"]  = table.Rows[i]["CDMKNM"].ToString();
                    row["CDACNM"]  = table.Rows[i]["CDACNM"].ToString();
                    row["P2CDDP"]  = table.Rows[i]["P2CDDP"].ToString();
                    row["CDDESC1"] = table.Rows[i]["CDDESC1"].ToString();
                    row["P2CDAC"]  = table.Rows[i]["P2CDAC"].ToString();
                    row["A1ABAC"]  = table.Rows[i]["A1ABAC"].ToString();

                    if (sNEWDSCHK != sOldDSCHK)
                    {
                        row["GUBUN"]  = table.Rows[i]["GUBUN"].ToString();
                        row["P1RKAC"] = table.Rows[i]["P1RKAC"].ToString();

                        sOldDSCHK     = sNEWDSCHK;
                    }
                    else
                    {
                        row["GUBUN"]  = "";
                        row["P1RKAC"] = "";
                    }
                }

                row["CHK"]   = table.Rows[i]["CHK"].ToString();
                row["AMT01"] = table.Rows[i]["AMT01"].ToString();
                row["AMT02"] = table.Rows[i]["AMT02"].ToString();
                row["AMT03"] = table.Rows[i]["AMT03"].ToString();
                row["AMT04"] = table.Rows[i]["AMT04"].ToString();
                row["AMT05"] = table.Rows[i]["AMT05"].ToString();
                row["AMT06"] = table.Rows[i]["AMT06"].ToString();
                row["AMT07"] = table.Rows[i]["AMT07"].ToString();
                row["AMT08"] = table.Rows[i]["AMT08"].ToString();
                row["AMT09"] = table.Rows[i]["AMT09"].ToString();
                row["AMT10"] = table.Rows[i]["AMT10"].ToString();
                row["AMT11"] = table.Rows[i]["AMT11"].ToString();
                row["AMT12"] = table.Rows[i]["AMT12"].ToString();
                row["TOTAL"] = table.Rows[i]["TOTAL"].ToString();

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